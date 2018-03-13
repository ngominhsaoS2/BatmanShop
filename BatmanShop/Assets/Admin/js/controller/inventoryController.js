var orderDetail = {
    init: function () {
        orderDetail.regEvents();
    },
    regEvents: function () {
        $('#btnSave').off('click').on('click', function () {
            //Update phần database
            var listRow = $('.txtRow');
            var list = [];
            var warehouseID, productID, quantity, str;
            var docCode = $('#txtDocCode').val();
            var docNo = $('#txtDocNo').val();
            var date = $('#txtDate').val();

            $.each(listRow, function (i, item) {
                warehouseID = $(item).text();
                productID = $(item).next().text();
                quantity = $(item).next().next().text();
                if (quantity > 0) {
                    list.push({
                        DocCode: docCode,
                        DocNo: docNo,
                        Date: date,
                        WarehouseID: warehouseID,
                        ProductID: productID,
                        Quantity: quantity
                    });
                }
            });

            $.ajax({
                url: '/Inventory/ImportDetail',
                data: { list: JSON.stringify(list) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        alert("Update database successfully");
                    }
                }
            })

        });
    }
}
orderDetail.init();

$(function () {
    $("#txtDate").datepicker();
});

var i = 0;
function addNewRow() {
    i = i + 1;
    var table = document.getElementById("tableBody");
    var warehouseID = $('#txtWarehouseID').val();
    var productID = $('#txtProductID').val();
    var quantity = $('#txtQuantity').val();
    var pre = "<tr id=" + i + ">" +
              "    <td id=warehouse" + i + " class=\"txtRow\">" + warehouseID + "</td>" +
              "    <td id=product" + i + ">" + productID + "</td>" +
              "    <td id=quantity" + i + ">" + quantity + "</td>" +
              "    <td id=action" + i + "><button id=\"button" + i + "\" class=\"btn btn-danger\" onclick=\"deleteRow(" + i + ")\" >Delete</button></td>" +
              "</tr>";

    $("#tableBody").prepend(pre);
}

function deleteRow(i) {
    var parent = document.getElementById("tableBody");
    var child = document.getElementById(i);
    parent.removeChild(child);
}

$(function () {
    var docNo = $('#txtDocNo').val();
    if (docNo != "") {
        $("#addDetail").show();
    }
    else {
        $("#addDetail").hide();
    }
});






