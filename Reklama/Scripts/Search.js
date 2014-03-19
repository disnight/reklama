$(document).ready(function () {
    $('.SearchUlBlock li').bind('click', function() {
        var clickedLi = $.attr(this, "name");
        $('#SearchCategory').val(clickedLi);
    });
});