$(document).ready(function() {
    var button = $('.avatar'), interval;
    var userId = $('#UserProfile').attr('identifier');

    $.ajax_upload(button, {
        action: '/AjaxServices/UploadAvatar',
        name: 'myfile',
        data: { id: userId },
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
            
            if(resp.status == "fail") {
                alert("Возникла ошибка");
            }
            else {
                // показываем что файл загружен
                $('.avatar').attr('src', '/Images/Users/Avatars/' + resp.newFilename);
            }
        }
    });
});