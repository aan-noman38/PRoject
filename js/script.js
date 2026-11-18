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


// Login Button (Temporary)

const form = document.querySelector("form");

form.addEventListener("submit", function (e) {

    e.preventDefault();

    const username = document.querySelector('input[type="text"]').value;
    const pass = password.value;
    const role = document.querySelector('input[name="role"]:checked').value;

    const error = document.getElementById("errorMsg");

    if (username === "" || pass === "") {

        error.innerHTML = "Please enter username and password.";
        return;

    }

    error.innerHTML = "";

    if (role === "user") {

        window.location.href = "pages/user-dashboard.html";

    }

    else if (role === "soc") {

        window.location.href = "pages/soc-dashboard.html";

    }

    else {

        window.location.href = "pages/admin-dashboard.html";

    }

});