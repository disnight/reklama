$(document).ready(function () {
	
	$('#CategoryId').change(function () {
	    var groupId = $.attr(this, 'value');
		var shopId = $('#ShopId').val();
        var url = "/Shops/GetCategories";

        $.post(
            url,
            { shopId: shopId, groupId: groupId },
            function (data) {
                $.fn.loadSecondCategoryBox(data);
				var categoryId = data[0].Id;
				$.post(
					"/Shops/GetManufacturers",
					{ categoryId: categoryId },
					function (data) {
						$.fn.loadThirdCategoryBox(data);
					}
				);
            }
        );
    });
	
	$('#SecondCategoryId').change(function() {
	    var categoryId = $.attr(this, 'value');
		
		$.post(
			"/Shops/GetManufacturers",
			{ categoryId: categoryId },
			function (data) {
				$.fn.loadThirdCategoryBox(data);
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
	
	$.fn.loadThirdCategoryBox = function (data) {		
		$(".persProdSection").html("");
		if (data.length > 0) {
			$(".persProdSection").append('<span>Производитель:</span> <select id="ThirdCategoryId" name="ThirdCategoryId">');
			$("#ThirdCategoryId").html("");
			for(var i = 0; i < data.length; i++) {
				$("#ThirdCategoryId").append("<option value='" + data[i].Id + "'>" + data[i].Name + "</option>");
			}
			$(".persProdSection").append('</select>');
		}
	};
	
	
	$('.but').bind('click', function () {
		var t = $(this);
		var button = $('.addBut'), interval;
		var productId = t.attr('ProductId');
		var shopId = $('#ShopId').attr('value');
		var parent = t.parent();
		
		if(t.val())
		{
			var price = parent.children('.PriceProduct').val();
			$.post(
			"/Shops/AddProductToShop",
			{ productId: productId, shopId: shopId, price: price},
			function (data) {
				if (data.status == 'success') {
					t.attr('class', 'delBut').val('');
					parent.children('.PriceProduct').css('display', 'none');
					parent.children('.text').html("товар в магазине");
				}
				else {
					alert('Возникла ошибка при добавлении товара в магазин');
				}
			},
			"json"
			);
		}
		else
		{
			var _confirm = confirm('Удалить товар?');
			if(_confirm)
			{
				$.post(
				"/Shops/RemoveProductFromShop",
				{ productId: productId, shopId: shopId },
				function (data) {
					if (data.status == 'success') {
						t.attr('class', 'addBut').val('Добавить');
						parent.children('.PriceProduct').removeAttr('style');
						parent.children('.text').html("цена: ");
					}
					else {
						alert('Возникла ошибка при удалении товара из магазина');
					}
				},
				"json"
				);
			}
		}

		return false;
	});

});