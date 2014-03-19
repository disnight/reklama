/**
    Created by Alex Kozlov, alexssource@mail.by
    Minsk, 2013
*/

$(document).ready(function () {
    $('.currency').bind('click', function () {
        var productMinPriceElement = $(this).parent().parent().parent().find('.productMinPrice');
        var productMaxPriceElement = $(this).parent().parent().parent().find('.productMaxPrice');
        var rate = parseFloat($(this).attr('rate'));
        var minPrice = parseFloat(productMinPriceElement.attr('value'));
        var maxPrice = parseFloat(productMaxPriceElement.attr('value'));
        var costMin = minPrice * rate;
        var costMax = maxPrice * rate;

        productMinPriceElement.html(costMin.toFixed(2));
        productMaxPriceElement.html(costMax.toFixed(2));
    });
});