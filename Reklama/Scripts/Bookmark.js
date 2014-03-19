$(document).ready(function () {

    // добавление в закладки
    $('#addToBookmarks').bind('click', function () {
        var id = $("#addToBookmarks").attr('identifier');
        var category = $('#addToBookmarks').attr('category');

        $.post(
            "/AjaxServices/AddToBookmarks",
            { Id: id, Category: category },
            function (data) {
                if (data.status == 'success') {
                    $('#addToBookmarks').html('<p>Добавлено</p>');
                }
                else {
                    alert('Возникла ошибка при добавлении в закладки');
                }
            },
            "json"
        );

        return false;
    });


    // удаление из закладок
    $('.delBut').bind('click', function (data) {
        var id = $(this).attr('identifier');
        var category = $(this).attr('category');
        var parentElement = $(this).parent();

        $.post(
            "/AjaxServices/RemoveFromBookmarks",
            { Id: id, Category: category },
            function (data) {
                if (data.status == 'success') {
                    parentElement.html('Удалено');
                } else {
                    alert('Возникла ошибка при удалении из закладок');
                }
            },
            "json"
        );

        return false;
    });


    $('.AnnouncementUp').bind('click', function(data) {
        var announcementId = $(this).attr("announcementId");
        var parentDiv = $(this).parent();

        $.post(
            "/AjaxServices/AnnouncementUp",
            { announcementId: announcementId },
            function (data) {
                if (data.status == 'success') {
                    parentDiv.html('<p>В следующий раз поднять объявление можно будет через ' + data.timeToUp + ' часов</p>');
                } else {
                    alert('Возникла ошибка при поднятии объявления. Возможно у вас не хватает прав');
                }
            },
            "json"
        );

        return false;
    });
	
	$('.RealtyUp').bind('click', function(data) {
        var realtyId = $(this).attr("realtyId");
        var parentDiv = $(this).parent();
        $.post(
            "/AjaxServices/RealtyUp",
            { realtyId: realtyId },
            function (data) {
                if (data.status == 'success') {
                    parentDiv.html('<p>В следующий раз поднять объявление можно будет через ' + data.timeToUp + ' часов</p>');
                } else {
                    alert('Возникла ошибка при поднятии объявления. Возможно у вас не хватает прав');
                }
            },
            "json"
        );

        return false;
    });
});