let tokenKey = "accessToken";

async function LoginUser(userName, userPassword) {
    const response = await fetch("api/account/login", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            UserName: userName,
            Password: userPassword
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
        let errOutput = document.getElementById('errorOutput');

        errOutput.innerHTML = '';
        if (error.errorText) {
            errOutput.innerHTML = error['errorText'];
        }
        else if (error.errors) {
            if (error.errors["Username"]) {
                errOutput.innerHTML += error.errors["Username"] + '</br>';
            }
            if (error.errors["Password"]) {
                errOutput.innerHTML += error.errors["Password"] + '</br>';
            }
        }

        document.getElementById("errorOutput").style.display = "block";
    }
}

document.forms["userForm"].addEventListener("submit", e => {
    e.preventDefault();
    const form = document.forms["userForm"];
    const name = form.elements["name"].value;
    const password = form.elements["password"].value;
    LoginUser(name, password);
});
