const token = sessionStorage.getItem("accessToken");

async function getData(url) {
    const response = await fetch(url, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + token
        }
    });
    if (response.ok === true) {
        const model = await response.json();
        console.log(model);
        const items = model.toDoItems;
        let rows = document.getElementById("taskTable");
        items.forEach(task => {
            rows.append(row(task));
        });

        const forEditId = document.getElementById("hiddenId");
        forEditId.value = model.editableItem.id;

        const btn = document.getElementById("createUpdate-btn");
        btn.innerHTML = "Add";
    }
}

function row(task) {
    const tr = document.createElement("tr");
    tr.setAttribute("data-rowid", task.id);

    const mainTd = document.createElement("td");

    let input = document.createElement("input");
    input.type = "checkbox";
    input.name = "isDone";
    input.checked = task.isDone; // TODO: calling isDone method

    mainTd.append(input);

    let title = document.createElement("a");
    title.name = "itemTitle";
    title.innerHTML = task.title;

    mainTd.append(title);
    tr.append(mainTd);

    const dateTd = document.createElement("td");
    dateTd.className = "text-right";
    let str = new Date(task.addDate).toISOString().substring(0, 10);
    dateTd.innerHTML = str;

    tr.append(dateTd);

    const delTd = document.createElement("td");
    delTd.className = "text-center";

    let del = document.createElement("a");
    del.className = "btn btn-sm btn-danger" // TODO: onclick confirming for delete
    del.innerHTML = "Delete";
    del.name = "delBtn";
    del.setAttribute("data-taskid", task.id);

    delTd.append(del);

    tr.append(delTd);

    return tr;
}

function UpdateRow(id, title) {
    const row = document.querySelector(`[data-rowid="${id}"]`);
    row.getElementsByTagName("a")[0].innerHTML = title;
}

function ResetTable() {
    let body = document.getElementById("taskTable");
    body.innerHTML = " ";
}

function ResetForm() {
    const form = document.forms["createUpdate"];
    form.elements["id"].value = 0;
    const btn = document.getElementById("createUpdate-btn");
    btn.innerHTML = "Add";
    const title = form.elements["title"].value = "";
}

async function Delete(url, id) {
    const response = await fetch(url + id, {
        method: "DELETE",
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + token }
    });
        if (response.ok === true) {
            ResetTable();
            getData("/api/task");
        }
}

async function CreateUser(url, title) {
    const response = await fetch(url, {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },
        body: JSON.stringify(title)
    });
    if (response.ok === true) {
        const model = await response.json();
        let rows = document.getElementById("taskTable");
        rows.append(row(model));
    }
}

async function FlagIsDone(id) {
    const response = await fetch("/api/task/" + id, {
        method: "PUT",
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + token
        }
    });
    if (response.ok === true) {} //TODO: SMTH
}

async function EditUser(id, title) {
    const response = await fetch("/api/task/edit/" + id, {
        method: "PUT",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },
        body: JSON.stringify(title)
    });
    if (response.ok === true) {
        ResetForm();
    }
}

document.addEventListener('click', function (e) { //adding event for dynamic element(delete button)
    if (e.target.name == "delBtn") {
        Delete("/api/task/", parseInt(e.target.getAttribute("data-taskid")));
    }
    else if (e.target.name == "isDone") {
        let n = parseInt(e.target.closest("tr").getAttribute("data-rowid"));
        FlagIsDone(n);
    }
    else if (e.target.name == "itemTitle") {
        const form = document.forms["createUpdate"];
        const id = parseInt(e.target.closest("tr").getAttribute("data-rowid"));
        const title = e.target.innerHTML;
        form.elements["id"].value = id;
        form.elements["title"].value = title;
        const btn = document.getElementById("createUpdate-btn");
        btn.innerHTML = "Update";
    }
});


document.forms["createUpdate"].addEventListener("submit", e => {
    e.preventDefault();
    const form = document.forms["createUpdate"];
    const id = form.elements["id"].value;
    const title = form.elements["title"].value;
    if (id == 0) {
        CreateUser("/api/task", title);
    }
    else {
        EditUser(id, title);
        UpdateRow(id, title);
        ResetForm();
    }
})

getData("/api/task");
