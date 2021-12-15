const token = sessionStorage.getItem("accessToken");

async function getAllTasks() {
    const response = await fetch("api/cabinet/tasks", {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + token
        }
    });
    if (response.ok === true) {
        let result = await response.json();
        let num = document.getElementById("numOfTasks");
        num.innerHTML = result.number;
        getWeekTasks();
    }
}

async function getWeekTasks() {
    const response = await fetch("api/cabinet/week-tasks", {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + token
        }
    });
    if (response.ok === true) {
        let result = await response.json();
        let num = document.getElementById("numOfTasksWeek");
        num.innerHTML = result;
        getProductivity();
    }
}

async function getProductivity() {
    const response = await fetch("api/cabinet/productivity", {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + token
        }
    });
    if (response.ok === true) {
        let result = await response.json();
        console.log(result);
        let bar = document.getElementById("progressbar");
        bar.style.width = result + "%";
    }
}

let calendar = document.getElementById("home");
calendar.addEventListener("click", function (e) {
    window.location.href = '../task.html';
});

let logout = document.getElementById("logout");
logout.addEventListener("click", function (e) {
    sessionStorage.removeItem("accessToken");
    window.location.href = '../index.html';
});

getAllTasks();