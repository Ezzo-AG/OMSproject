
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

      //    if (x[cnt].type == "Select" && x[cnt].id.indexOf('_' + nextrowIdx + '_') > 0)
      //        x[cnt].value = "" ;
        if (x[cnt].type == "number" && x[cnt].id.indexOf('_' + nextrowIdx + '_') > 0)
            x[cnt].value = 0;
        else if (x[cnt].type == "hidden" && x[cnt].id.indexOf('_' + nextrowIdx + '_') > 0) {
            x[cnt].value = "false";
            if (x[cnt].className == "hidden" && x[cnt].id.indexOf('_' + nextrowIdx + '_') > 0)
                x[cnt].className = "nothidden";
        }
 
    }
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
    document.getElementById('sellprice').value = totalPrice;

    return;
}

document.addEventListener('change', function (e) {
    if (hasClass(e.target, 'Price')) {
        CalculatePrice();
    }
}, false);



function LoudProduct(element) {
    var colorId = eval(element.value);
    var productId = $('#' + element.id.replaceAll('ProductName', 'ColorName'));
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
}

function GetProductPrice(element) {
   /* if (element.id.indexOf("ProductN") > 0) {*/

        var ProductId = eval(element.value);
        var priceId = document.getElementById(element.id.replaceAll('ProductName', 'Price'));
        //var QuantityId = document.getElementById(element.id.replaceAll('ProductName', 'Quantity'));

        $.ajax({
            url: '/Order/getProductPrice?productId=' + ProductId,
            success: function (price) {
                priceId.value = price;
                CalculatePrice();
            },
            error: function () {
                alert('Error>...  ');
            }
        });
  /*  }*/
   
}


function ChangePrice(element) {
    var ProductId = document.getElementById(element.id.replaceAll('Quantity', 'ProductName')).value;
    var priceId = document.getElementById(element.id.replaceAll('Quantity', 'Price'));
    


    $.ajax({
        url: '/Order/getProductPrice?productId=' + ProductId,
        success: function (price) {
            priceId.value = price * element.value;
            CalculatePrice();
        },
        error: function () {
            alert('Error>...  ');
        }
    });
}

