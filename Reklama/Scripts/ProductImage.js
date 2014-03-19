$(document).ready(function() {
    // Upload an image
    var button = $('.regBut1'), interval;
    var id = $("#ProductId").attr('value');
    
    $.ajax_upload(button, {
        action: '/AjaxServices/ProductAddPhoto',
        name: 'myfile',
        data: { id: id },
        onSubmit: function(file, ext) {
            // показываем картинку загрузки файла
            $("img#load").attr("src", "/Images/System/loader.gif").attr("class", "visible");
            $("#uploadButton font").text('Загрузка');

            /*
             * Выключаем кнопку на время загрузки файла
             */
            this.disable();

        },
        onComplete: function(file, response) {
            var resp = JSON.parse(response);

            // убираем картинку загрузки файла
            $("img#load").attr("class", "unvisible");
            $("#uploadButton font").text('Загрузить');

            // снова включаем кнопку
            this.enable();
            // показываем что файл загружен
            $('#ProductImages').append('<div class="productImage"><img class="productImage" src="/Images/Catalog/Product/' + resp.newFilename + '"/><br /><a href="#" class="productImageRemove" onClick="return $.fn.removeImage($(this));">Удалить</a></div>');
        }
    });


    $.fn.removeImage = function(link) {
        var prnt = link.parent();
        var image = prnt.find('.productImage');
        var id = $("#ProductId").attr('value');
        var announcementImage = image.attr('src');

        $.post(
            "/AjaxServices/RemoveProductImage",
            { image: announcementImage, id: id },
            function(data) {
                if (data.status == 'success') {
                    prnt.remove();
                } else {
                    alert('Возникла ошибка при удалении фото');
                }
            },
            "json"
        );

        return false;
    };
});