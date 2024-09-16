



function addUpdateUsers() {

    let name = $('#name').val();
    let email = $('#email').val();
    let mobile = $('#mobile').val();
    let password = $('#password').val();
    var numbers = /^[0-9]+$/;
    var Emailregex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    var Nameregex = /\b.*[a-zA-Z ].\b/;


    $(".error").remove();
    if (name == '') {
        toastr.error("name field is required");
        return false;
    }
    else if (name.length < 3 || name.length > 20) {
        toastr.error("name length should be between minlength 3 and maxlength 20");
        return false;
    }
    else if (!name.match(Nameregex))
    {
        toastr.error("name length should be between minlength 3 and maxlength 20");
        return false;
    }
    else if (email == '') {
        toastr.error("Email field is required");
        return false;
    }
    else if (!email.match(Emailregex)) {
        toastr.error("invalid email filed");
        return false;
    }
    else if (email.length < 10 || email.length > 100) {
        toastr.error("Email length should be between minlength 10 and maxlength 100");
        return false;
    }
    else if (mobile == '') {
        toastr.error("Mobile field is required");
        return false;
    }
    else if (!mobile.match(numbers)) {
        toastr.error("invalid phone number");
        return false;
    }
    else if (mobile.length > 10 || mobile.length < 10) {
        toastr.error("Mobile number should be 10 digits");
        return false;
    }
    else if (password == '') {
        toastr.error("Password field is required");
        return false;
    }
    else if (password.length < 6 || password.length > 30) {
        toastr.error("password length should be between minlength 6 and maxlength 30");
        return false;
    }
}



function login() {
    let email = $('#email').val();
    let password = $('#password').val();
    var regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    $(".error").remove();

    if (email == '') {
        toastr.error("email field is required");
        return false;
    }
    else if (!email.match(regex)) {
        toastr.error("invalid email filed");
        return false;
    }
    else if (email.length < 10 || email.length > 100) {
        toastr.error("Email length should be between minlength 10 and maxlength 100");
        return false;
    }
    else if (password == '') {
        toastr.error("password field is required");
        return false;
    }
    else if (password.length < 6 || password.length > 30) {
        toastr.error("password length should be between minlength 6 and maxlength 30");
        return false;
    }
    
}


function role() {
    let rolename = $('#rolename').val();

    $(".error").remove();
    if (rolename == '') {
        toastr.error("rolename field is required");
        return false;
    }
    else if (rolename.length < 3 || rolename.length > 50) {
        toastr.error("rolename length should be between minlength 3 and maxlength 50");
        return false;
    }
}