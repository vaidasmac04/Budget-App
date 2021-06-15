const API_BASE_URL = "https://localhost:5001/api";

//asynchronous communication with API
async function authenticateClientAsync(username, password){
	
    const response = await fetch(`${API_BASE_URL}/authentication/authenticate`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
              },
            body: JSON.stringify({username: username, password: password})
        });

	if (!response.ok) {

        if(response.status == 401){
            throw new Error("Incorrect username or password");
        }
        else{
            throw new Error (`An error has occured: ${response.status}`);
        }
    }
	
	const client = await response.json();
	return client;
}

//events
function onLoginClicked(){
    const username = document.getElementById("username").value;
    const password = document.getElementById('password').value;

    let isValid = true;
    if(username == "" || password == "") {
        isValid = false;
        const loginValidationField = document.getElementById("login-validation");
        showError(loginValidationField, "Please enter username and password");
    }
    
    if(isValid){
        showLoader();
        
        authenticateClientAsync(username, password)
        .then(data =>  {
            window.localStorage["token"] = data["token"];
            window.localStorage["id"] = data["id"];
            window.location.href = "home.html";
        })
        .catch(error => {
            const loginValidationField = document.getElementById("login-validation");
            showError(loginValidationField, error.message);
        })
        .finally(() => {
            hideLoader();
        });
    }
}

function onNotRegisteredClicked(){
    const forms = getForms();
    forms.registerForm.style.display = "block";
    forms.loginForm.style.display = "none";
}

function onAlreadyRegisteredClicked(){
    const forms = getForms();
    forms.registerForm.style.display = "none";
    forms.loginForm.style.display = "block";
}

function onRegisterClicked(){
    alert("onRegisterClicked()");
}

//helpers
function getForms(){
    return {
        loginForm: document.getElementById("login-form"), 
        registerForm: document.getElementById("register-form")
    }
}

