$(document).ready(function() {
    $('.but').bind('click', function () {
		var t = $(this);
		var button = $('.addBut'), interval;
		var categoryId = t.attr('CategoryId');
		var shopId = $('#ShopId').attr('value');
		
		if(t.val())
		{
			$.post(
			"/AjaxServices/AddCategoryToShop",
			{ categoryId: categoryId, shopId: shopId },
			function (data) {
				if (data.status == 'success') {
					t.attr('class', 'delBut').val('');
				}
				else {
					alert('Возникла ошибка при добавлении категории в магазин');
				}
			},
			"json"
			);
		}
		else
		{
			$.post(
			"/AjaxServices/RemoveCategoryFromShop",
			{ categoryId: categoryId, shopId: shopId },
			function (data) {
				if (data.status == 'success') {
					t.attr('class', 'addBut').val('Добавить');
				}
				else {
					alert('Возникла ошибка при удалении категории из магазина');
				}
			},
			"json"
			);
		}

		return false;
	});
});