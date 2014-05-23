$(document).ready(function () {
	
	$(".Personal").bind("mousedown", function () {
		$("div#AgencyNameDiv").slideUp();
		$("#AgencyName").val('');
    });

	
	$(".Agency").bind("mousedown", function () {
		$("div#AgencyNameDiv").slideDown();
    });
	
	
	$(".SectionList").change(function(){
		var name = $(".SectionList option:selected").text();
		if(name == 'Сниму' || name == 'Сдам'){
			$("label#ForDaysLabel").slideDown();
		}else{
			$("label#ForDaysLabel").slideUp();
			$("#ForDays").removeAttr("checked");
		}
	});
	
	//$.fn.removeImage = function (link) {
    //    var prnt = link.parent();
    //    var image = prnt.find('.realtyImage');
    //    var realtyImage = image.attr('src');

    //    $.post(
    //        "/AjaxServices/RemoveRealtyOnlyImage",
    //        { image: realtyImage },
    //        function (data) {
    //            if (data.status == 'success') {
    //                prnt.remove();
    //            }
    //            else {
    //                alert('Возникла ошибка при удалении фото');
    //            }
    //        },
    //        "json"
    //    );
        
    //    return false;
    //};
	
	var button = $('.regBut1'), interval;
	
	//$.ajax_upload(button, {
	//	action: '/AjaxServices/UploadRealtyImage',
	//	name: 'myfile',
	//	data: { x: 3 },
	//	onSubmit: function (file, ext) {
	//		// показываем картинку загрузки файла
	//		$("img#load").attr("src", "/Images/System/loader.gif").attr("class", "visible");
	//		$("#uploadButton font").text('Загрузка');

	//		/*
	//		 * Выключаем кнопку на время загрузки файла
	//		 */
	//		this.disable();

	//	},
	//	onComplete: function (file, response) {
	//		var resp = JSON.parse(response);
			
	//		// убираем картинку загрузки файла
	//		$("img#load").attr("class", "unvisible");
	//		$("#uploadButton font").text('Загрузить');

	//		// снова включаем кнопку
	//		this.enable();
	//	    // показываем что файл загружен
	//		$('#imagePreview').removeClass('unvisible').addClass('visible');

	//		$('#imagePreview').append('<div class="image_fields"><img class="realtyImage" src="/Images/Realty/' + resp.newFilename + '"/><input type="hidden" name="images[]" value="' + resp.newFilename + '" /><br /><a href="#" class="image_remove" onClick="return $.fn.removeImage($(this));">Удалить</a></div>');
	//	}
	//});


	$("#regRad").bind('click', function () {
	    window.location.href = "http://reklama.tm/Announcement/Create";
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