var openInventory = {
    init: function () {
        openInventory.registerEvents();
    },
    registerEvents: function () {
        $('#btnCreate').off('click').on('click', function () {
            var max = $("tr").eq(1).attr("id");
            var warehouseID, year, month, productID, quantity, str;
            var list = [];
            for(i = 1; i <= max; i++){
                str = "#warehouse" + i;
                warehouseID = $(str).text();
                str = "#year" + i;
                year = $(str).text();
                str = "#month" + i;
                month = $(str).text();
                str = "#product" + i;
                productID = $(str).text();
                str = "#quantity" + i;
                quantity = $(str).text();
                list.push({
                    WarehouseID: warehouseID,
                    Year: year,
                    Month: month,
                    ProductID: productID,
                    Quantity: quantity
                });
            }

            $.ajax({
                url: '/Admin/OpenInventory/Create',
                data: { listOpenInventory: JSON.stringify(list) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Admin/OpenInventory/Index";
                    }
                }
            })

        });
    }
}
openInventory.init();