var productSearch = {
    init: function () {
        productSearch.registerEvent();
    },
    registerEvent: function () {
        $("#txtProductID").autocomplete({
            minLength: 0,
            source: function (request, response) {
                $.ajax({
                    url: "/Product/ListName",
                    dataType: "json",
                    data: {
                        q: request.term
                    },
                    success: function (res) {
                        response(res.data);
                    }
                });
            },
            focus: function (event, ui) {
                $("#txtProductID").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#txtProductID").val(ui.item.label);
                return false;
            }
        })
     .autocomplete("instance")._renderItem = function (ul, item) {
         return $("<li>")
           .append("<a>" + item.label + "</a>")
           .appendTo(ul);
     };
    }
}
productSearch.init();

var warehouseSearch = {
    init: function () {
        warehouseSearch.registerEvent();
    },
    registerEvent: function () {
        $("#txtWarehouseID").autocomplete({
            minLength: 0,
            source: function (request, response) {
                $.ajax({
                    url: "/Warehouse/ListName",
                    dataType: "json",
                    data: {
                        q: request.term
                    },
                    success: function (res) {
                        response(res.data);
                    }
                });
            },
            focus: function (event, ui) {
                $("#txtWarehouseID").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#txtWarehouseID").val(ui.item.label);
                return false;
            }
        })
     .autocomplete("instance")._renderItem = function (ul, item) {
         return $("<li>")
           .append("<a>" + item.label + "</a>")
           .appendTo(ul);
     };
    }
}
warehouseSearch.init();