function CheckPassword() {
    var inputpwd = document.getElementById("pwd").value;
    var passw = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$/;
    if (inputpwd.match(passw)) {
        return true;
    }
    else {
        return false;
    }
}

function ValidateEmail() {
    if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(document.getElementById("email").value)) {
        return true;
    }
    else {
        return false;
    }
    
}

function checkDate() {
    var todaysDate = new Date();

    var month;
    if ((todaysDate.getMonth() + 1) < 10) {
        month = "0" + (todaysDate.getMonth() + 1);
    } else {
        month = (todaysDate.getMonth() + 1);
    }
    var birthDate = new Date(document.getElementById("birth").value);
    

    var datetime = todaysDate.getFullYear() + "-" + month + "-" + todaysDate.getDate() + "T" + todaysDate.getHours() + ":" + todaysDate.getMinutes();


    var currYear = todaysDate.getFullYear();
    var birthYear,birthString;
    if (document.getElementById("birth").value != "") {
        if ((birthDate.getMonth() + 1) < 10) {
            month = "0" + (birthDate.getMonth() + 1);
        } else {
            month = (birthDate.getMonth() + 1);
        }
        birthString = birthDate.getFullYear() + "-" + month + "-" + birthDate.getDate() + "T" + birthDate.getHours() + ":" + birthDate.getMinutes();

        birthYear = birthDate.getFullYear();
        if (((currYear - birthYear) < 18) || birthString >= datetime) {
            return false;
        }
        return true;
    }
    else if (document.getElementById("birth").value == "") {
        return false;
    } else {
        return true;
    }
}

    $("#myform").submit(
        function (event) {
            if (document.getElementById("fname").value == "" || document.getElementById("lname").value == "" || document.getElementById("pwd").value == "" || document.getElementById("email").value == "" || document.getElementById("birth").value == "" ||
                document.getElementById("add1").value == "" || document.getElementById("add2").value == "" || document.getElementById("zip").value == "" || document.getElementById("city").value == "" || document.getElementById("code").value == "" || document.getElementById("phone").value == "" ||
                !CheckPassword() || !checkDate() || !ValidateEmail() 
                ) {
                var ul = document.getElementById("error");
                while (ul.hasChildNodes()) {
                    ul.removeChild(ul.firstChild);
                }


                if (document.getElementById("fname").value == "") {
                    var ul = document.getElementById("error");
                    var li = document.createElement("li");
                    li.appendChild(document.createTextNode("Please enter your first name."));
                    ul.appendChild(li);
                }
                if (document.getElementById("lname").value == "") {
                    var ul = document.getElementById("error");
                    var li = document.createElement("li");
                    li.appendChild(document.createTextNode("Please enter your last name."));
                    ul.appendChild(li);
                }
                if (!CheckPassword()) {
                    var ul = document.getElementById("error");
                    var li = document.createElement("li");
                    li.appendChild(document.createTextNode("Password should have 6 to 20 characters which contain at least one numeric digit, one uppercase and one lowercase letter."));
                    ul.appendChild(li);
                }
                if (!ValidateEmail()) {
                    var ul = document.getElementById("error");
                    var li = document.createElement("li");
                    li.appendChild(document.createTextNode("Email format is invalid."));
                    ul.appendChild(li);
                }

                if (!checkDate()) {
                    var ul = document.getElementById("error");
                    var li = document.createElement("li");
                    li.appendChild(document.createTextNode("Birthdate is invalid. You should be at least 18 years old."));
                    ul.appendChild(li);
                }

                if ((document.getElementById("add1").value == "") || (document.getElementById("add2").value == "")) {
                    var ul = document.getElementById("error");
                    var li = document.createElement("li");
                    li.appendChild(document.createTextNode("Please enter both addresses."));
                    ul.appendChild(li);
                }

                if (document.getElementById("zip").value == "") {
                    var ul = document.getElementById("error");
                    var li = document.createElement("li");
                    li.appendChild(document.createTextNode("Please enter the postal zip code."));
                    ul.appendChild(li);
                }
                if (document.getElementById("city").value == "") {
                    var ul = document.getElementById("error");
                    var li = document.createElement("li");
                    li.appendChild(document.createTextNode("Please enter your city."));
                    ul.appendChild(li);
                }

                if ((document.getElementById("code").value == "") || (document.getElementById("phone").value == "")) {
                    var ul = document.getElementById("error");
                    var li = document.createElement("li");
                    li.appendChild(document.createTextNode("Please enter country code and contact number."));
                    ul.appendChild(li);
                }
            event.preventDefault();
        }
        else {

        }
});
