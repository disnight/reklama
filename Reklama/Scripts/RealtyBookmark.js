$(document).ready(function () {

    // добавление в закладки
    $('#addToBookmarks').bind('click', function () {
        var id = $("#addToBookmarks").attr('identifier');
        var category = $('#addToBookmarks').attr('category');

        $.post(
            "/AddToBookmarks",
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
});