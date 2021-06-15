function createTable(headers, data, id){
    const table = document.createElement("table");
    table.id = id;
    table.appendChild(_createHeader(headers));
    _fillTable(data, table);
    return table;
}

function _createHeader(headers){
    const row = document.createElement('tr');
    
    for(let i = 0; i < headers.length; i++){
        const cell = document.createElement('th');
        cell.innerHTML = headers[i];
        row.appendChild(cell);
    }

    return row;
}

function _fillTable(data, table){
    const props = _extractProps(data[0]);
   
    for(let i = 0; i < data.length; i++){
        const row = document.createElement('tr');
        for(let j = 0; j < props.length; j++){
            const cell = document.createElement('td');
            cell.innerHTML = data[i][props[j]];
            row.appendChild(cell);
        }

        table.appendChild(row);
    }

    const idColIndex = props.indexOf("id");
    if(idColIndex != -1){
        _hideColumnByIndex(table, idColIndex);
    }
   
}

function _extractProps(dataRow){
    var props = [];

    for (const key in dataRow) {
        props.push(key);
    }

    return props;
}

function _hideColumnByIndex(table, index){
    for(const row of table.rows){
        row.cells[index].style.display = "none";
    }
}

function addButtonColumn(table, headerName, buttonName, onClickFunction){
    const rowCount = table.rows.length;

    if(rowCount <= 0){
        throw Error("Table must have at least one row");
    }

    const cell = document.createElement("th");
    cell.innerHTML = headerName;
    table.firstChild.appendChild(cell);

    const rows = table.rows;
    for(let i = 1; i < rows.length; i++){
        const cell = createButtonCell(buttonName, onClickFunction);
        rows[i].appendChild(cell);
    }
}

function createButtonCell(buttonName, onClickFunction){
    const cell = document.createElement("td");
    const button = document.createElement("button");
    button.innerHTML = buttonName;
    button.onclick = onClickFunction.bind(this, button);
    button.classList.add("button");
    cell.appendChild(button);
    return cell;
}