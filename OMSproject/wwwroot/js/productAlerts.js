let rows = document.getElementById("myTable").getElementsByTagName("tbody")[0].getElementsByTagName("tr");
for (i = 0; i < rows.length; i++) {
    cells = rows[i].getElementsByTagName('td')
    if (cells[4].innerHTML <= 10) {
        rows[i].className = "alertTable";
    }
}