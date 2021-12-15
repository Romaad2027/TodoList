let tokenKey = "accessToken";

async function RegisterUser(userName, userPassword, confirmPassword) {
    const response = await fetch("api/account/register", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            UserName: userName,
            Password: userPassword,
            ConfirmPassword: confirmPassword
        })
    });
    if (response.ok === true) {
        const user = await response.json();
        console.log(user);
        sessionStorage.setItem(tokenKey, user.access_token);
        window.location.href = '../task.html';
    }
    else {
        const error = await response.json();
        console.log(error.errors.Username[0]);
        let a = document.getElementById("main-title");
        a.innerHTML = error.errors.Username[0];
    }
}

document.forms["userForm"].addEventListener("submit", e => {
    e.preventDefault();
    const form = document.forms["userForm"];
    console.log(form);
    const name = form.elements["name"].value;
    const password = form.elements["password"].value;
    const confirmPassword = form.elements["confirmPassword"].value;
    RegisterUser(name, password, confirmPassword);
});

