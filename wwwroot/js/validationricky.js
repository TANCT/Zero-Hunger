var errorMessage = "";

function ValidateDryFood() {
    var message = document.getElementById('errorMessagediv');
    errorMessage = "";
    message.innerHTML = "";
    var has_error = false;
    if (!validateName()) {
        
        has_error = true;
    }
    if (!validateDate()) {
        
        has_error = true;
        
    }
    

    if (has_error) {
        message.innerHTML = errorMessage;
        message.style.display = "unset";
        
        return false;
    }
    else {
        return true;
    }

    
    
}
function validateDate() {

    if (document.getElementById("pickDate").value == "") {
        return false;
    }
    
    var todaysDate = new Date();
    var deliveryMethod = document.getElementById('deliveryMethod');

    var month;
    if ((todaysDate.getMonth() + 1) < 10) {
        month = "0" + (todaysDate.getMonth() + 1);
    }
    else {
        month = (todaysDate.getMonth() + 1);
    }
    var birthDate = new Date(document.getElementById("pickDate").value);
    var datetime = todaysDate.getFullYear() + "-" + month + "-" + todaysDate.getDate() + "T" + todaysDate.getHours() + ":" + todaysDate.getMinutes();
    var currYear = todaysDate.getFullYear();
    var birthYear, birthString;
    if (document.getElementById("pickDate").value != "") {
        if ((birthDate.getMonth() + 1) < 10) {
            month = "0" + (birthDate.getMonth() + 1);
        } else {
            month = (birthDate.getMonth() + 1);
        }
        birthString = birthDate.getFullYear() + "-" + month + "-" + birthDate.getDate() + "T" + birthDate.getHours() + ":" + birthDate.getMinutes();

        birthYear = birthDate.getFullYear();
        if (birthYear < currYear) {

            errorMessage += '<ul><li>Pick-up date should be after the current date</li></ul>';
            return false;
        }
        else {
            if (birthDate.getMonth() + 1 < todaysDate.getMonth() + 1) {
                errorMessage += '<ul><li>Pick-up date should be after the current date</li></ul>';
                return false
            }
            else {
                if (birthDate.getMonth() == todaysDate.getMonth()) {
                    if (birthDate.getDate() <= todaysDate.getDate()) {
                        if (deliveryMethod.value == "448 Delivery") {
                            if (birthDate.getDate() == todaysDate.getDate()) {


                                errorMessage += '<ul><li>Since 448 Delivery is choosed, pick-up date should be one day after the current date.</li></ul>';

                                return false;
                            }
                            else {
                                errorMessage += '<ul><li>Pick-up date should be after the current date</li></ul>';

                                return false
                            }
                        }
                        else {
                            if (birthDate.getDate() == todaysDate.getDate()) {
                                return true;
                            }
                            else {
                                errorMessage += '<ul><li>Pick-up date should be after the current date</li></ul>';

                                return false;
                            }
                        }


                    }
                }
                
            }
        }
        
        
            return true;
        

    }
}
function validateName() {
    var name = document.getElementById('dryName').value;
    if (name != "") {
        if (name.match(/^[0-9]+$/) != null) {
            errorMessage += '<ul><li>Name cannot be number only</li></ul>';
            
            return false;
        }
        else {
            return true;
        }
    }
    else {
        return true;
    }
}