$(document).ready(function () {

    $('#CategoryId').change(function () {
        var groupId = $(this).val();
        var url = "/CatalogAdmin/Product/GetCategories";

        $.post(
            url,
            { groupId: groupId },
            function (data) {
                $.fn.loadSecondCategoryBox(data);
                var categoryId = data[0].Id;
                $.post(
					"TODO",
					{ categoryId: categoryId },
					function (data) {
					    $.fn.loadParameters(data);
					}
				);
            }
        );
    });

    $('#SecondCategoryId').change(function () {
        var categoryId = $.attr(this, 'value');
        var shopId = $('#ShopId').val();

        $.post(
			"TODO",
			{ shopId: shopId, categoryId: categoryId },
			function (data) {
			    $.fn.loadParameters(data);
			}
		);
    });

    $.fn.loadSecondCategoryBox = function (data) {
        $("#SecondCategoryId").html("");

        if (data.length > 0) {
            for (var i = 0; i < data.length; i++) {
                $("#SecondCategoryId").append("<option value='" + data[i].Id + "'>" + data[i].Name + "</option>");
            }
        }
    };

    $.fn.loadParameters = function (data) {
        //$(".persProdSection").html("");
        //if (data.length > 0) {
        //    $(".persProdSection").append('<span>Производитель:</span> <select id="ThirdCategoryId" name="ThirdCategoryId">');
        //    $("#ThirdCategoryId").html("");
        //    for (var i = 0; i < data.length; i++) {
        //        $("#ThirdCategoryId").append("<option value='" + data[i].Id + "'>" + data[i].Name + "</option>");
        //    }
        //    $(".persProdSection").append('</select>');
        //}
    };


   

 
});