$(document).ready(function () {
    
    $('.SectionList').change(function () {
        var selectedItemValue = $.attr(this, 'value');
        var url = "/AjaxServices/GetSubsections";

        $.post(
            url,
            { sectionId: selectedItemValue },
            function (data) {
                $.fn.loadSubsectionBox(data);
            }
        );
    });
	
	
    $.fn.loadSubsectionBox = function (data) {
        $("#SubsectionId").html("");
        $("#SubsectionId").attr("disabled", "disabled");

        if (data.length > 0) {
            $("#SubsectionId").attr("disabled", false);

            for (var i = 0; i < data.length; i++) {
                $("#SubsectionId").append("<option value='" + data[i].Id + "'>" + data[i].Name + "</option>");
            }
        }
    };


    $.fn.removeImage = function (link) {
        var prnt = link.parent();
        var image = prnt.find('.announcementImage');
        var announcementImage = image.attr('src');
        var announcementId = $('.announcementId').val();

        $.post(
            "/AjaxServices/RemoveAnnouncementImage",
            { announcementId: announcementId, image: announcementImage },
            function (data) {
                if (data.status == 'success') {
                    prnt.remove();
                }
                else {
                    alert('Возникла ошибка при удалении фото');
                }
            },
            "json"
        );
        
        return false;
    };
	
    var button = $('.regBut1'), interval;

    //$.ajax_upload(button, {
    //    action: '/AjaxServices/UploadFile',
    //    name: 'myfile',
    //    data: { x: 3 },
    //    onSubmit: function (file, ext) {
    //        // показываем картинку загрузки файла
    //        $("img#load").attr("src", "/Images/System/loader.gif").attr("class", "visible");
    //        $("#uploadButton font").text('Загрузка');

    //        /*
    //         * Выключаем кнопку на время загрузки файла
    //         */
    //        this.disable();

    //    },
    //    onComplete: function (file, response) {
    //        var resp = JSON.parse(response);
            
    //        // убираем картинку загрузки файла
    //        $("img#load").attr("class", "unvisible");
    //        $("#uploadButton font").text('Загрузить');

    //        // снова включаем кнопку
    //        this.enable();
    //        // показываем что файл загружен
    //        $('#imagePreview').removeClass('unvisible').addClass('visible');

    //        $('#imagePreview').append('<div class="image_fields"><img class="announcementImage" src="/Images/Users/' + resp.newFilename + '"/><input type="hidden" name="images[]" value="' + resp.newFilename + '" /><br /><a href="#" class="image_remove" onClick="return $.fn.removeImage($(this));">Удалить</a></div>');
    //    }
    //});
});