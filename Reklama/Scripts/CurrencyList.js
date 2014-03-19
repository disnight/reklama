/**
    Created by Alex Kozlov, alexssource@mail.by
    Minsk, 2013
*/

$(document).ready(function () {
    $('.price_list_click').bind('click', function () {
        var jRoot = $(this);
        jRoot.find('.mPriceList').addClass('visible').removeClass('unvisible');
    });
    
    $('.currency').bind('click', function (){
        var mPriceList = $(this).parent().parent();
        var priceValueElem = mPriceList.parent().parent().find('.price_value');
        var rate = parseFloat($(this).attr('rate'));
        var priceValue = parseFloat(priceValueElem.attr('value'));
        var cost = rate * priceValue;
        priceValueElem.html(cost.toFixed(2));
        
        mPriceList.addClass('unvisible').removeClass('visible');
        mPriceList.parent().find('span').html($(this).html());
        
        return false;
    });
	
	
	$('.mPriceList').mouseleave(function(){
		$('.mPriceList.visible').removeClass('visible').addClass('unvisible');
	});
	
});