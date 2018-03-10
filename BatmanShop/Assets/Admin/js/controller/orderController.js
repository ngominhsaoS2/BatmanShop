var orderDetail = {
    init: function () {
        orderDetail.regEvents();
    },
    regEvents: function () {
        $('#btnUpdate').off('click').on('click', function () {
            var orderId = $("td:first").text();
            var listProduct = $('.txtQuantity');
            var cartList = [];
            $.each(listProduct, function (i, item) {
                cartList.push({
                    Quantity: $(item).val(),
                    Product: {
                        ID: $(item).data('id')
                    }
                });
            });

            $.ajax({
                url: '/Order/UpdateQuantity',
                data: { cartModel: JSON.stringify(cartList), orderId: orderId },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        alert("Update row successfully");
                    }
                }
            })
        });

        $('#btnWareUpdate').off('click').on('click', function () {
            //Update phần giao diện
            var table = document.getElementById("tableBody");
            var warehouseID = $('#txtWarehouseID').val();
            $('.txtWarehouseId').val(warehouseID);

            //Update phần database
            var listWarehouse = $('.txtWarehouseId');
            var list = [];
            var orderID, productID, quantity, str;
            
            $.each(listWarehouse, function (i, item) {
                orderID = $(item).text();
                productID = $(item).next().text();
                quantity = $(item).next().next().next().text();
                if (quantity > 0) {
                    list.push({
                        OrderID: orderID,
                        WarehouseID: warehouseID,
                        ProductID: productID,
                        Quantity: quantity
                    });
                }
            });

            $.ajax({
                url: '/Order/UpdateWarehouseID',
                data: { list: JSON.stringify(list) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        alert("Update database successfully");
                        window.location.reload();
                    }
                }
            })

        });

    }
}
orderDetail.init();