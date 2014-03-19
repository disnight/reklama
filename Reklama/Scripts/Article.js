$(document).ready(function() {

    $('.SectionList').change(function () {
        var selectedItemValue = $.attr(this, 'value');
        var url = "/AjaxServices/GetArticleSubsections";

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

    $('.delete_article_logo').bind('click', function () {
        var src = $('.imgLogo').attr('src');
        var articleId = $("#Id").val();
        
        $.post(
            "/AjaxServices/RemoveArticleImage",
            { articleId: articleId, image: src },
            function (data) {
                if (data.status == 'success') {
                    $('.article_logo').html('<div class="editor-label"><label for="Logo">Изображение</label></div><div class="editor-field"><input name="ArticleLogo" type="file"><input id="Logo" name="Logo" value="" type="hidden"><span class="field-validation-valid" data-valmsg-for="Logo" data-valmsg-replace="true"></span></div>');
                }
                else {
                    alert('Возникла ошибка при удалении фото');
                }
            },
            "json"
        );

        return false;
    });
});