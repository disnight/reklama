$(document).ready(function () {

    var selectedItemValue = $("#CategoryId").attr('value');
    var url = "/AjaxServices/GetSecondCategory";

    $.post(
        url,
        { categoryId: selectedItemValue },
        function (data) {
            $.fn.loadSecondCategoryId(data);
        }
    );
    
    
    $('#CategoryId').change(function () {
        var selectedItemValue = $.attr(this, 'value');
        var url = "/AjaxServices/GetSecondCategory";

        $.post(
            url,
            { categoryId: selectedItemValue },
            function (data) {
                $.fn.loadSecondCategoryId(data);
                
                selectedItemValue = $(".SecondCategoryId").attr('value');
                url = "/AjaxServices/GetThirdCategory";

                $.post(
                    url,
                    { category2Id: selectedItemValue },
                    function (data) {
                        $.fn.loadThirdCategoryId(data);
                    }
                );
            }
        );
    });

    $('#SecondCategoryId').change(function () {
        var selectedItemValue = $.attr(this, 'value');
        var url = "/AjaxServices/GetThirdCategory";

        $.post(
            url,
            { category2Id: selectedItemValue },
            function (data) {
                $.fn.loadThirdCategoryId(data);
            }
        );
    });
    
    $.fn.loadSecondCategoryId = function (data) {
        $("#SecondCategoryId").html("");
        $("#SecondCategoryId").attr("disabled", "disabled");

        if (data.length > 0) {
            $("#SecondCategoryId").attr("disabled", false);

            for (var i = 0; i < data.length; i++) {
                $("#SecondCategoryId").append("<option value='" + data[i].Id + "'>" + data[i].Name + "</option>");
            }
        }
    };
    
    $.fn.loadThirdCategoryId = function (data) {
        $("#ThirdCategoryId").html("");
        $("#ThirdCategoryId").attr("disabled", "disabled");

        if (data.length > 0) {
            $("#ThirdCategoryId").attr("disabled", false);

            for (var i = 0; i < data.length; i++) {
                $("#ThirdCategoryId").append("<option value='" + data[i].Id + "'>" + data[i].Name + "</option>");
            }
        }
    };
});