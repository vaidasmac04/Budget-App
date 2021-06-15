function showLoader(){
    document.getElementById("loader").style.display="block";
}

function hideLoader(){
    document.getElementById("loader").style.display="none";
}

function showActionResultMessage(messageField, message, success){
    messageField.innerHTML = message;

    if(success){
        if(messageField.classList.contains("action-failed")){
            messageField.classList.remove("action-failed");
        }
        messageField.classList.add("action-success");
        
    }
    else{
        if(messageField.classList.contains("action-success")){
            messageField.classList.remove("action-success");
        }
        messageField.classList.add("action-failed");
    }

    messageField.style.visibility = "visible";

    setTimeout(function(){
        messageField.style.visibility = "hidden";
    }, 5000)
}

function showError(validationField, message){
    validationField.innerHTML = message;
    validationField.style.visibility = "visible";
}

function hideError(validationField){
    validationField.style.visibility = "hidden";
}