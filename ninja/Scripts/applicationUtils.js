// To make javaScript functions

/*Invoice utils*/

//This method is used to add new record on invoice detail
function addDetail() {
    var rowId = $("#detailsCount").val();
    var urlGetRow = "/Invoice/GetRow/" + rowId;
    $.getJSON(urlGetRow, function (data) {
        $("#detailsCount").val(data.rowId + 1);
        $("#invoicerows").append(data.rowData);
    });
}

//This method is used to calculate the row values
function updateRow(index) {
    var amount = $("#Details_" + index + "__Amount").val();
    var unitPrice = $("#Details_" + index + "__UnitPrice").val();
    var taxes = $("#Details_" + index + "__Taxes").val();
    var total = amount * unitPrice * taxes;
    $("#Details_" + index + "__TotalPrice").val(total);
}