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
                url: '/Order/UpdateRow',
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
    }
}
orderDetail.init();