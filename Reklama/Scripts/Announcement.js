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

        $.post(
            "/AjaxServices/RemoveAnnouncementOnlyImage",
            { image: announcementImage },
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



    $("#regRad2").bind('click', function () {
        window.location.href = "http://reklama.tm/Realty/Create";
    });
    
    $("select.requared").change(function () {
        validateForm();
    });

    $(document).on('keyup', "#Phone", function () {
        validateForm();
    });

    $(document).on('keyup', "#ContactEmail", function () {
        validateForm();
    });

});

function validateForm() {
    var result = true;

    if (!ValidateTextInput("#Phone") && !ValidateTextInput("#ContactEmail")) {
        $("#Phone").addClass("input-validation-error");
        $("#ContactEmail").addClass("input-validation-error");
        $("#emailandphone").show();
        result = false;
    } else {
        $("#Phone").removeClass("input-validation-error");
        $("#ContactEmail").removeClass("input-validation-error");
        $("#emailandphone").hide();
    }

    if (!ValidateSelectInput('#newSectionId')) {
        $("#newSectionId").addClass("input-validation-error");
        $("#sectionError").show();
        result = false;
    } else {
        $("#newSectionId").removeClass("input-validation-error");
        $("#sectionError").hide();
    }



    if (result) {
        $("#customErrorsContainer").hide();
    } else {
        $("#customErrorsContainer").show();
    }

    return result;
}

function ValidateTextInput(selector) {
    var input = $(selector);
    if (input && input.val() != "" && input.val().trim() != "") {
        return true;
    }
    return false;
}

function ValidateSelectInput(selector) {
    if ($(selector).val() && $(selector).val() != "") {
        return true;
    } else {
        return false;
    }
}