
function hasClass(elem, className) {
    return elem.classList.contains(className);
}

function AddItem(btn) {

    var table = document.getElementById('tableHa');

    if (btn.id == "addOrder")
        table = document.getElementById('OrderTable')
    else 
        table = document.getElementById('tableHa')


    
    var rows = document.getElementsByTagName('tr');

    var lastrowIdx = rows.length - 1;
    var rowHtml = rows[lastrowIdx].outerHTML;


    lastrowIdx = lastrowIdx - 1;

    var nextrowIdx = eval(lastrowIdx) + 1;

    rowHtml = rowHtml.replaceAll('_' + lastrowIdx + '_', '_' + nextrowIdx + '_');
    rowHtml = rowHtml.replaceAll('[' + lastrowIdx + ']', '[' + nextrowIdx + ']');
    rowHtml = rowHtml.replaceAll('-' + lastrowIdx, '-' + nextrowIdx);


    var newRow = table.insertRow();

    newRow.innerHTML = rowHtml;

    var x = document.getElementsByTagName("input");


    for (var cnt = 0; cnt < x.length; cnt++)
    {

      //    if (x[cnt].type == "text" && x[cnt].id.indexOf('_' + nextrowIdx + '_') > 0)
      //        x[cnt].value = "";
        if (x[cnt].type == "number" && x[cnt].id.indexOf('_' + nextrowIdx + '_') > 0)
            x[cnt].value = 0;
        else if (x[cnt].type == "hidden" && x[cnt].id.indexOf('_' + nextrowIdx + '_') > 0) {
            x[cnt].value = "false";
            if (x[cnt].className == "hidden" && x[cnt].id.indexOf('_' + nextrowIdx + '_') > 0)
                x[cnt].className = "nothidden";
        }
 
    }
    idGrapper()
}

function DeleteItem(btn) {

    var table = document.getElementById('tableHa')

    if (btn.id.indexOf("Orde") > 0)
        table = document.getElementById('OrderTable')
    else 
        table = document.getElementById('tableHa')


    var rows = table.getElementsByTagName('tr');
    var notHidItems = document.getElementsByClassName('nothidden');


    if (rows.length == 2 || notHidItems.length <= 1 ) {
        alert("This row cannot be deleted")
        return;
    }

    
    var btnIdx = btn.id.replaceAll('btnremove-', '');

    var idofIsDeleted = btnIdx + "__IsDeleted";

    if (btn.id.indexOf("Orde") > 0) {
        btnIdx = btn.id.replaceAll('btnremoveOrder-', '');
    }
      
    var idofIsDeleted = btnIdx + "__IsDeleted";

    if (btn.id.indexOf("Orde") > 0) {
        idofIsDeleted = btnIdx + "__IsHidden";
    }
       
    var hidIsDelId = document.querySelector("[id$='" + idofIsDeleted + "']").id;


    document.getElementById(hidIsDelId).value = true;
    document.getElementById(hidIsDelId).className ="hidden";

    
    $(btn).closest('tr').hide();

    if (btn.id.indexOf("Orde") > 0)
        CalculatePrice();
    else
        CalculateQuantity();
    
}



function CalculateQuantity() {

    var x = document.getElementsByClassName('Quantity')
    var totalQuantity = 0;
    var i;

    for (i = 0; i < x.length; i++) {

        var idofIsDeleted = i + "__IsDeleted";

        var hidIsDelId = document.querySelector("[id$='" + idofIsDeleted + "']").id;
       if( document.getElementById(hidIsDelId).value != "true")
        totalQuantity = totalQuantity + eval(x[i].value);
    }

    document.getElementById('total').value = totalQuantity;

    return;
}

document.addEventListener('change', function (e) {
    if (hasClass(e.target, 'Quantity')) {
        CalculateQuantity();
    }
}, false);


function CalculatePrice() {

    var x = document.getElementsByClassName('Price')
    var totalPrice = 0;
    var i;

    for (i = 0; i < x.length; i++) {

        idofIsDeleted = i + "__IsHidden";

        var hidIsDelId = document.querySelector("[id$='" + idofIsDeleted + "']").id;
        if (document.getElementById(hidIsDelId).value != "true")
            totalPrice = totalPrice + eval(x[i].value);
    }

    document.getElementById('totalPrice').value = totalPrice;

    return;
}

document.addEventListener('change', function (e) {
    if (hasClass(e.target, 'Price')) {
        CalculatePrice();
    }
}, false);
//for (i = 0; i < x.; i++) {
//var quntity = document.getElementById("Quantity-id-");
//    if (quntity) {
//        quntity.addEventListener('click', function () {
//            LoudProduct(colorId)
//        });
//        quntity.addEventListener('focus', function () {
//            searchBoxClicked();
//        });
//    }

//function getSelectValues(select) {
//    var result = [];
//    var options = select && select.options;
//    var opt;

//    for (var i = 0, iLen = options.length; i < iLen; i++) {
//        opt = options[i];

//        if (opt.selected) {
//            result.push(opt.value || opt.text);
//        }
//    }
//    return result;
//}
function idGrapper() {

    const selected = document.querySelectorAll('#ProductName');
    const values = Array.from(selected).map(el => el.value);

    const selectedTwo = document.querySelectorAll('#ColorName');
    const valuesTwo = Array.from(selectedTwo).map(el => el.value);

    let select = document.getElementById("ProductName");
    let choices = document.getElementById("ColorName");

    //options.forEach(function addOption(item) {
    //    let option = document.createElement("option");
    //    option.text = item;
    //    option.value = item;
    //    select.appendChild(option);
    //});
    function addToChoices(values) {
        values.forEach(function (item) {
            let option = document.createElement("option");
            option.text = item;
            option.value = item;
            choices.appendChild(option);
        });
    }
    select.onchange = function () {
        choices.innerHTML = choices + values;
        if (this.value == values) {
            addToChoices(valuesTwo);
        }
        else {
            alert("Nothing to Show in here");
        }
    };
    console.log(values)
    console.log(valuesTwo)
    console.log(select.innerHTML)
    console.log(choices.innerHTML)
    
    
}

function LoudProduct(element) {
    var colorId = eval(element.value);
    console.log(colorId);
    var productId = $('#' + element.id.replaceAll('ProductName', 'ColorName'));
    console.log(productId );
    productId.empty();
    $.ajax({
        url: '/Order/getcolors?productId=' + colorId,
        success: function (colors) {
            $.each(colors, function (i, color) {
                productId.append($('<option></option>').attr('value', color.product_Id).text(color.colorName));
            });
        },
        error: function () {
            alert('Error>...  ');
        }
    });
    //window.onload = (event) => {
    //    //console.log('page is fully loaded');
    //    //LoudProduct(ProductName - @i)
    //    LoudProduct(colorId)
    //};
}

//function quntom(element) {

//}
function searchBoxClicked() {
    console.log('focus');
}

function startSearch() {
    console.log('click');
}
window.onload = (event) => {
    //console.log('page is fully loaded');
    //LoudProduct(ProductName - @i)
    idGrapper()
};