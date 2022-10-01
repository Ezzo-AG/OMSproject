


function AddItem(btn) {

    var table = document.getElementById('tableHa');
    var rows = document.getElementsByTagName('tr');


    var rowHtml = rows[rows.length - 1].outerHTML;


    var lastrowIdx = document.getElementById('hdnLastIndex').value;

    

    var nextrowIdx = eval(lastrowIdx) + 1;

    document.getElementById('hdnLastIndex').value = nextrowIdx;

    var iden = document.getElementById('ids-'+lastrowIdx).value;
    var tota = document.getElementById('total').value;
    if (tota == null) {
        tota = 0;
    }
    var cos = eval(iden) + eval(tota);
    var qty = document.getElementById('total')
    qty.value = cos;
    //tota
    console.log(cos);


    rowHtml = rowHtml.replaceAll('_' + lastrowIdx + '_', '_' + nextrowIdx + '_');
    rowHtml = rowHtml.replaceAll('[' + lastrowIdx + ']', '[' + nextrowIdx + ']');
    rowHtml = rowHtml.replaceAll('-' + lastrowIdx, '-' + nextrowIdx);


    var newRow = table.insertRow();

    newRow.innerHTML = rowHtml;

    var btnAddID = btn.id;
    var btnDeleteid = btnAddID.replaceAll('btnadd', 'btnremove');

    var delbtn = document.getElementById(btnDeleteid);
    delbtn.classList.add("visible");
    delbtn.classList.remove("invisible");


    var addbtn = document.getElementById(btnAddID);
    addbtn.classList.remove("visible");
    addbtn.classList.add("invisible");



}

function DeleteItem(btn,id) {

    //var iden = document.getElementById('ids-' + lastrowIdx).value;
    var iden = document.getElementById(id).value;
    var tota = document.getElementById('total').value;
    var cos = eval(tota) - eval(iden);

    var qty = document.getElementById('total')
    qty.value = cos;

    $(btn).closest('tr').remove();
}