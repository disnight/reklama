$(document).ready(function () {
	
	$(".previewLink").click( function(){
		if ($("#formCreate").valid()) {
			var images = [];
			$('.images').each(function(index){
				images.push($(this).val());
			});
			$.ajax({
                url: '/Realty/Preview',
				type: "POST",
				data: "formData=" + $("#formCreate").serialize() + "&images=" + images,
                success: function (result) {
                    $('#preview').html(result);
					$("div#preview").slideDown();
                }
			});
		}
	});
	
});
