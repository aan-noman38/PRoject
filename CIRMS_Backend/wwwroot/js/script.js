// Show / Hide Password

const password = document.getElementById("password");
const eye = document.getElementById("eye");

eye.addEventListener("click", function () {

    if (password.type === "password") {
        password.type = "text";
        eye.innerHTML = '<i class="fa-solid fa-eye-slash"></i>';
    } else {
        password.type = "password";
        eye.innerHTML = '<i class="fa-solid fa-eye"></i>';
    }

});


// Show Sign Up only for User

const roles = document.querySelectorAll('input[name="role"]');
const signupArea = document.getElementById("signupArea");

roles.forEach(role => {

    role.addEventListener("change", function () {

        if (this.value === "user") {
            signupArea.style.display = "block";
        } else {
            signupArea.style.display = "none";
        }

    });

});


