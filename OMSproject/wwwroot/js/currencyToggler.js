currency = " LYD"
arr = [".currency"]
for (let i = 0; i < arr.length; i++) {

    document.querySelectorAll(arr[i]).forEach(function (obj) {
        x = obj.innerText
        obj.innerHTML = new Intl.NumberFormat().format(x) + currency
    })
}