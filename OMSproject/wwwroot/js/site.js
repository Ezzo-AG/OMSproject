

function AddItem(btn) {

    var table = document.getElementById('tableHa');
    var rows = document.getElementsByTagName('tr');


    var rowHtml = rows[rows.length - 1].outerHTML;


    var lastrowIdx = rows.length - 2;



    var nextrowIdx = eval(lastrowIdx) + 1;

    rowHtml = rowHtml.replaceAll('_' + lastrowIdx + '_', '_' + nextrowIdx + '_');
    rowHtml = rowHtml.replaceAll('[' + lastrowIdx + ']', '[' + nextrowIdx + ']');
    rowHtml = rowHtml.replaceAll('-' + lastrowIdx, '-' + nextrowIdx);


    var newRow = table.insertRow();

    newRow.innerHTML = rowHtml;


}

function DeleteItem(btn) {

    var table = document.getElementById('tableHa')
    var rows = table.getElementsByTagName('tr')

    if (rows.length == 2) {
        alert("This row cannot be deleted")
        return;
    }

    
    var btnIdx = btn.id.replaceAll('btnremove', '');
    var idofIsDeleted = btnIdx + "__IsDeleted";

    var hidIsDelId = document.querySelector("[id$='" + idofIsDeleted + "']").id;
    document.getElementById(hidIsDelId).value ="true";

    //$(btn).closest('tr').remove();
    
    $(btn).closest('tr').hide();
       
    CalculateQuantity();
}



function CalculateQuantity() {

    var x = document.getElementsByClassName('Quantity')
    var totalQuantity = 0;
    var i;

    for (i = 0; i < x.length; i++) {
        totalQuantity = totalQuantity + eval(x[i].value);
    }

    document.getElementById('total').value = totalQuantity;

    return;
}


document.addEventListener('change', function (e) {
    if (e.target.classList.contains('Quantity')) {
        CalculateQuantity();
    }
}

    , false);



