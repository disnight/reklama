/**
    Created by Alex Kozlov, alexssource@mail.by
    Minsk, 2013
*/

$(document).ready(function () {
    $('.currency').bind('click', function () {
        var rate = parseFloat($(this).attr('rate'));
        var price = parseFloat($('.price_value').attr('value'));
        var cost = price * rate;
        $('.price_value').html(cost.toFixed(2));
    });
	$('.mPriceList').mouseleave(function(){
		$('.alSelect').removeClass('a');
	});
	
});