// let btn = document.querySelector("#btn");
// let sidebar = document.querySelector(".sidebar");
// let searchBtn = document.querySelector(".bx-search");

// btn.onclick = function() {
//     sidebar.classList.toggle("active");
// }
// searchBtn.onclick = function() {
//     sidebar.classList.toggle("active");
// }

const body = document.querySelector('body')
let table = document.querySelector('table')
    sidebar = body.querySelector('nav')
    toggle = body.querySelector(".toggle")
    searchBtn = body.querySelector(".search-box")
    modeSwitch = body.querySelector(".toggle-switch")
    modeText = body.querySelector(".mode-text");


toggle.addEventListener("click", () => {
    sidebar.classList.toggle("close");
})

searchBtn.addEventListener("click", () => {
    sidebar.classList.remove("close");
})

modeSwitch.addEventListener("click", () => {
    body.classList.toggle("dark");
    table.classList.toggle("table-dark");

    if (body.classList.contains("dark")) {
        modeText.innerText = "Light mode";
    } else {
        modeText.innerText = "Dark mode";

    }
});