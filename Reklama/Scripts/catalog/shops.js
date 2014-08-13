$(document).ready(function () {
    var button = $('.shopsUploadLogo'), interval;
    var previousLogo = $('.logoShops').attr('src');
    var shopId = $('#Id').attr('value');
    $.ajax_upload(button, {
        action: '/Shops/UploadShopLogo',
        name: 'myfile',
        data: { logo: previousLogo, id: shopId },
        onSubmit: function (file, ext) {
            // показываем картинку загрузки файла
            $("img#load").attr("src", "/Images/System/loader.gif").attr("class", "visible");

            /*
             * Выключаем кнопку на время загрузки файла
             */
            this.disable();

        },
        onComplete: function (file, response) {
            var resp = JSON.parse(response);

            // убираем картинку загрузки файла
            $("img#load").attr("class", "unvisible");

            // снова включаем кнопку
            this.enable();

            if (resp.status == "fail") {
                alert("Возникла ошибка");
            }
            else {
                // показываем что файл загружен
                $('.logoShops').attr('src', '/Images/Catalog/ShopLogos/' + resp.newFilename);
                $('.shopsDeleteLogo').removeAttr('style');
            }
        }
    });

    $('.shopsDeleteLogo').bind('click', function () {
        var logo = $('.logoShops').attr('src');
        $.post(
			"/Shops/RemoveShopLogo",
			{ shopId: shopId, image: logo },
			function (data) {
			    if (data.status == 'success') {
			        $('.shopsDeleteLogo').css('display', 'none');
			        $('.logoShops').attr('src', '/Images/System/no_logo.png');
			    }
			    else {
			        alert('Возникла ошибка при удалении логотипа');
			    }
			},
			"json"
		);

        return false;
    });
});