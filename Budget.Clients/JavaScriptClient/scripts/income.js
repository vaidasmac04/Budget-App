const API_BASE_URL = "https://localhost:5001/api";

window.onload = function() {
    createIncomeTable();
};

class Income {
    constructor(value, date, source, id = 0) {
      this._value = value;
      this._date = date;
      this._source = source;
      this._id = id;
    }
    
    set value(value){
        this._value = value;
    }

    get value() {
      return this._value;
    }

    set date(date){
        this._date = date;
    }

    get date() {
        return this._date;
    }

    set source(source){
        this._source = source;
    }

    get source() {
        return this._source;
    }

    set id(id){
        this._id = id;
    }

    get id(){
        return this._id;
    }

    static equal(incomeA, incomeB) {
        if(incomeA.value == incomeB.value
            && incomeA.date == incomeB.date
            && incomeA.source == incomeB.source
            && incomeA.id == incomeB.id){
                return true;
        }

        return false;
    }
}

class IncomeValidator {
    validate(income){
        this._validateId(income.id);
        this._validateValue(income.value);
        this._validateDate(income.date);
        this._validateSource(income.source);
        return true;
    }

    _validateId(id){
        if(id == null){
            throw Error("Id is required");
        }

        if(isNaN(parseInt(id) || id < 0)){
            throw Error("Income id must be 0 or any other positive integer");
        }
    }

    _validateValue(value){
        if(value == ""){
            throw Error("Value is required");
        }

        if(isNaN(value)){
            throw Error("Value must be a number");
        }

        if(value <= 0){
            throw Error("Value must be a positive number");
        }

        const regex = new RegExp("^[0-9]+(\.[0-9]{1,2})?$");
        const result = regex.test(value);

        if(!result){
            throw Error("Value format is incorrect. Valid example: 99.99");
        }
    }

    _validateDate(date){
        if(date == ""){
            throw Error("Date is required");
        }

        if(isNaN(Date.parse(date))){
            throw Error("Date format is incorrect");
        }
    }

    _validateSource(source){
        if(source == ""){
            throw Error("Source is required");
        }
    }
}

function createIncomeTable(){
    showLoader();
    getIncomesAsync()
    .then(data =>  {
        const table = createTable(["id", "Value", "Date", "Source"], data, 'income-table');
        addButtonColumn(table, "Update", "Update", onUpdateClicked);
        addButtonColumn(table, "Delete", "Delete", onDeleteClicked);
        table.className = "pretty-table";
        document.getElementById("income-section").appendChild(table);
    })
    .catch(error => {
        if(error instanceof UnauthorizedError){
            window.location.href = "index.html";
        }
        else {
            const actionMessageField = document.getElementById("action-message");
            if(error instanceof TypeError){
                showActionResultMessage(actionMessageField, 
                    "Service unavailable. Try to refresh the page.", false);
            }
            else{
                showActionResultMessage(actionMessageField, error.message, false);
            }
        }
        console.log(error);
    })
    .finally(() => {
        hideLoader();
    });
}

//asynchronous communication with API
async function getIncomesAsync(){
    const token = window.localStorage["token"];

    const response = await fetch(`${API_BASE_URL}/income`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
              },
    });

	if (!response.ok) {

        if(response.status == 401){
            throw new UnauthorizedError("Unauthorized");
        }
        else{
            throw new Error (`An error has occured: ${response.status}`);
        }
    }
	
	const incomes = await response.json();
	return incomes;
}

async function updateIncomeAsync(income){
    const clientId = window.localStorage["id"];
    const token = window.localStorage["token"];
    const response = await fetch(`${API_BASE_URL}/income/${clientId}/${income.id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify({
                id: income.id, 
                value: income.value,
                date: income.date,
                sourceName: income.source
            })
    });

	if (!response.ok) {
        throw new Error (`An error has occured: ${response.status}`);
    }

    return await response.text();
}

async function deleteIncomeAsync(id){
    const token = window.localStorage["token"];

    const response = await fetch(`${API_BASE_URL}/income/${id}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
    });

    if (!response.ok) {

        if (!response.ok) {
            throw new Error (`An error has occured: ${response.status}`);
        }
    }

    return await response.text();
}

async function addIncomeAsync(income){
    const clientId = window.localStorage["id"];
    const token = window.localStorage["token"];

    const response = await fetch(`${API_BASE_URL}/income/${clientId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify({
            id: income.id, 
            value: income.value,
            date: income.date,
            sourceName: income.source
        })
    });

    if (!response.ok) {

        if (!response.ok) {
            throw new Error (`An error has occured: ${response.status}`);
        }
    }

    return await response.text();
}

//events
function onDeleteClicked(sourceElement){

    if(window.confirm("Do you really want to delete this income?")){
        const row = sourceElement.parentNode.parentNode;
        const id = parseInt(row.cells[0].innerHTML);

        const actionMessageField = document.getElementById("action-message");
        showLoader();
        deleteIncomeAsync(id)
        .then(() => {
            showActionResultMessage(actionMessageField, "Deleted successfully", true);
            deleteTableRow(row);
        })
        .catch(error => {
            if(error instanceof TypeError){
                showActionResultMessage(actionMessageField, "Service unavailable. Try later.", false);
            }
        })
        .finally(() => {
            hideLoader();
        });
    }
}

function onUpdateClicked(sourceElement){
    const validationField = document.getElementById("income-validation");
    hideError(validationField);

    const incomeModal = document.getElementById("income-modal");
    
    const cells = sourceElement.parentNode.parentNode.cells;
    const id = parseInt(cells[0].innerHTML);
    const value = cells[1].innerHTML;
    const date = cells[2].innerHTML;
    const sourceName = cells[3].innerHTML;

    const income = new Income(value, date, sourceName, id);

    document.getElementById("value").value = income.value;
    document.getElementById("date").value = income.date;
    document.getElementById("source").value = income.source;

    incomeModal.style.display = "block";
    const submitModalButton = document.getElementById("submit-modal-button");
    submitModalButton.onclick = onSubmitClicked_update.bind(this, income, cells);
}

function onAddClicked(){
    const validationField = document.getElementById("income-validation");
    hideError(validationField);
    resetDataOnIncomeModal();

    const incomeModal = document.getElementById("income-modal");
    incomeModal.style.display = "block";
    const submitModalButton = document.getElementById("submit-modal-button");
    submitModalButton.onclick = onSubmitClicked_add;
}

function onSubmitClicked_update(currentIncome, activeCells){
    const validationField = document.getElementById("income-validation");

    const income = getDataFromIncomeModal();
    income.id = currentIncome.id;
    let valid;

    //client side validation
    const incomeValidator = new IncomeValidator();
    try{
        valid = incomeValidator.validate(income);
    }
    catch(error){
        showError(validationField, error.message);
    }

    if(Income.equal(currentIncome, income)){
        valid = false;
        showError(validationField, "Nothing to update");
    }

    if(valid){
        //call to API
        showLoader();
        updateIncomeAsync(income)
        .then(() => {
            const actionMessageField = document.getElementById("action-message");
            showActionResultMessage(actionMessageField, "Updated successfully", true);
            onCloseIncomeModalClicked();
            updateTableRow(activeCells, income);
        })
        .catch(error => {
            if(error instanceof TypeError){
                showError(validationField, "Service unavailable. Try later.");
            }
        })
        .finally(() => {
            hideLoader();
        });
    }
}

function onSubmitClicked_add(){
    const validationField = document.getElementById("income-validation");

    const income = getDataFromIncomeModal();
    let valid;
    //client side validation
    const incomeValidator = new IncomeValidator();
    try{
        valid = incomeValidator.validate(income);
    }
    catch(error){
        showError(validationField, error.message);
    }

    if(valid){
         //call to API
        showLoader();
        addIncomeAsync(income)
        .then(() => {
            const actionMessageField = document.getElementById("action-message");
            showActionResultMessage(actionMessageField, "Added successfully", true);
            onCloseIncomeModalClicked();
            addTableRow(income);
        })
        .catch(error => {
            if(error instanceof TypeError){
                showError(validationField, "Service unavailable. Try later.");
            }
            else{
                console.log(error);
            }
        })
        .finally(() => {
            hideLoader();
        });
    }
}

function onCloseIncomeModalClicked(){
    const updateModal = document.getElementById("income-modal");
    updateModal.style.display = "none";
}

function onFilterClicked(){
    alert("onFilterClicked()");
}

function onSortClicked(){
    alert("onSortClicked()");
}

//helpers
function getDataFromIncomeModal(){
    const value = document.getElementById("value").value;
    const date = document.getElementById("date").value;
    const source = document.getElementById("source").value;
    const income = new Income(value, date, source);
    return income;
}

function resetDataOnIncomeModal(){
    document.getElementById("value").value = "";
    const now = new Date(Date.now());
    const dateString = now.getFullYear().toString() + "-" +
        now.getMonth().toString().padStart(2, 0) + "-" + now.getDate().toString().padStart(2, 0);
    document.getElementById("date").value = dateString;
    document.getElementById("source").value = "";
}

function updateTableRow(cellsToUpdate, updatedIncome){
    cellsToUpdate[1].innerHTML = updatedIncome.value;
    cellsToUpdate[2].innerHTML = updatedIncome.date;
    cellsToUpdate[3].innerHTML = updateIncome.source;
}

function addTableRow(income){
    const table = document.getElementById("income-table");
    
    if(table != null){
        const row = document.createElement("tr");

        const idCell = document.createElement("td");
        idCell.innerHTML = income.id;
        idCell.style.display = "none";
        row.appendChild(idCell);

        const valueCell = document.createElement("td");
        valueCell.innerHTML = income.value;
        row.appendChild(valueCell);

        const dateCell = document.createElement("td");
        dateCell.innerHTML = income.date;
        row.appendChild(dateCell);

        const sourceCell = document.createElement("td");
        sourceCell.innerHTML = income.source;
        row.appendChild(sourceCell);

        const updateCell = createButtonCell("Update", onUpdateClicked);
        row.append(updateCell);

        const deleteCell = createButtonCell("Delete", onDeleteClicked);
        row.append(deleteCell);

        table.appendChild(row);
    }
}

function deleteTableRow(row){
    const table = document.getElementById("income-table");
    
    if(table != null){
        table.removeChild(row);
    }
}


