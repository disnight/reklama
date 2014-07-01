$(document).ready(function(){
    
    $(window).load(function(){     
        if($('.middle').width() > 1100){
        $('.howUp').addClass('a');
    }
    else {
        $('.howUp').removeClass('a');
    }          
    });
    
    $(window).resize(function(){      
        if($('.middle').width() > 1100){
        $('.howUp').addClass('a');
    }
    else {
        $('.howUp').removeClass('a');
    }          
    });
    
    var timeuot1 = null;
    $('.topBlocks').hover(function(){
            var jThis = $(this);
            timeout1 = setTimeout(function(){
                jThis.find(' > a > img, .topImg').slideDown(500);
                jThis.
                timeout1 = null;
            }, 1000);
        }, function(){
            if(timeout1){
                clearTimeout(timeout1);
                timeout1 = null;
            }
            var jImages = $(this).find(' > a > img:visible, .topImg:visible');
            if(jImages.size()){
                jImages.stop().slideUp(500);
            }
        }
    
    );
/*    
    setTimeout(function() {
        $('.topBlocks').hover(function(){
        $(' > a .topImg',this).slideDown('1000');
        $(' > a > img',this).slideDown('1000');
    }, function(){
        $(' > a .topImg',this).slideUp('1000');
        $(' > a > img',this).slideUp('1000');
    });
    }, 1000); // время в мс
*/    
    $('.hMenu table td').hover(function(){
        $('.hMenu table td').removeClass('cur');
        $(' + td',this).addClass('cur');
    }, function(){
        $('.hMenu table td').removeClass('cur'); 
    });
    
    $('.hMenu table td.a + td').addClass('f');

    $('.hMenu table td').first().addClass('f');
    
    /*
     $('.hMenu table td a, .hMenu table td.a a').textShadow();
     */
    
    $('.loginLink, .regLink, .remLink').fancybox({
        padding: 5,
        autoCenter: false
    });

    //$('.regLink').fancybox({
    //        padding: 5,
    //        autocenter: false,
    //}, function () { return false; });
    
    //if($('.mainAdsWrap').size()){
    //$('.mainAdsWrap, .mainRealtyWrap').equalHeightColumns();
    //};
    
    $('.ciSort li').click(function(){
        $('.ciSort li').removeClass('a');
        $(this).addClass('a');
    });
    
    $('.ciShowBy li').click(function(){
        $('.ciShowBy li').removeClass('a');
        $(this).addClass('a');
    });
    
    //$('.retSort').click(function(){
    //    if($(this).is('.down')) {
    //        $(this).removeClass('down').addClass('up');
    //        $('.retSort span').html('<span>по возрастанию</span>');
    //    }
    //    else {
    //        $(this).removeClass('up').addClass('down');
    //        $('.retSort span').html('<span>по убыванию</span>');
    //    }
    //    return false;
    //});
    
    $('.alSelect').each(function(){
        var jRoot = $(this);
        jRoot.find('> span').click(function(){
            jRoot.toggleClass('a');
            return false;
        });
        jRoot.find('li, li span').click(function(){
            var val = $(this).text();
            jRoot.find('> span').html(val);
            jRoot.removeClass('a');
            jRoot.find('li').removeClass('a');
            $(this).closest('li').addClass('a');
            jRoot.change();
            return false;
        });
        $('body').click(function(){
            jRoot.removeClass('a');
        });
    });
    
    $('.fastNav li a').click(function(){
        $('.fastNav li a').removeClass('hr');
        $(this).addClass('hr');
    });
    
    $('.exppages').click(function(){
        $('.pageslider').fadeIn();
    });
    
   
    
    $('.pageslider').each(function(){
        var jRoot = $(this),
            jWindow = jRoot.find('.pagesliderTop'),
            jContent = jRoot.find('.pagesliderTop ul'),
            jBar = jRoot.find('.alHScrollBar'),
            jSlider = jBar.find('.alHScrollSlider'),
            lastPos = 0;

        var ScrollContent = function(pos){
            var left = pos * (jContent.width() - jWindow.width());
            left = Math.min(Math.max(0, left), Math.max(0, jContent.width() - jWindow.width()));
            jContent.css('left', -left + 'px');
            lastPos = pos;
        };
        var ScrollSlider = function(pos){
            var left = pos * (jBar.width() - jSlider.width());
            left = Math.min(Math.max(0, left), jBar.width() - jSlider.width());
            jSlider.css('left', left + 'px');
        };
        
        jRoot.find('.alHScrollSlider').mousedown(function(e){
            var jSlider = $(this);
            var MousePos = e.clientX - jSlider.offset().left;

            $('html').bind('dragstart.al selectstart.al', function(){return false;});            
            
            var onMouseMove = function(e){
                var left = jSlider.position().left + (e.clientX - jSlider.offset().left - MousePos);

                if(left < 0) 
                    left = 0;
                else if(left + jSlider.width() > jSlider.parent().width()) 
                    left = jSlider.parent().width() - jSlider.width();
                    
                jSlider.css('left', left+'px');
                ScrollContent(left / (jSlider.parent().width() - jSlider.width()));
                
                return false;
            };
            var onMouseUp = function(){
                $(this).unbind('mouseup', onMouseUp);
                $('html').unbind('dragstart.al selectstart.al mousemove.al mouseup.al mouseleave.al');
                return false;
            };
            $('html')
                .bind('mouseup.al mouseleave.al', onMouseUp)
                .bind('mousemove.al', onMouseMove);

            return false;
        }).click(function(){return false;});
        
        jRoot.find('.alHScrollBar').click(function(e){
            var pos = (e.clientX - $(this).offset().left - $(this).find('.alHScrollSlider').width()/2)
                        / ($(this).width() - $(this).find('.alHScrollSlider').width());
            ScrollContent(pos);
            ScrollSlider(pos);
        });
        
        $(window).resize(function(){
            ScrollContent(lastPos);
            ScrollSlider(lastPos);
        });

        jContent.children().focus(function(){
            var 
                focusLeft = $(this).position().left,
                focusRight = focusLeft + $(this).outerWidth(true),
                currentLeft = - jContent.position().left,
                left = currentLeft;

            if(focusRight > currentLeft + jWindow.width()){
                left = focusRight + $(this).outerWidth(true)/2 - jWindow.width();
            };
            if(focusLeft < currentLeft){
                left = focusLeft - $(this).outerWidth(true)/2;
            };
            left = Math.min(Math.max(0, left), Math.max(0, jContent.width() - jWindow.width()));
            if(currentLeft != left){
                jContent.animate({left: -left});
                var pos = left / (jContent.width() - jWindow.width());
                lastPos = pos;
                sliderLeft = pos * (jBar.width() - jSlider.width());
                jSlider.animate({'left': sliderLeft});
            };
            return false;
        });
    });
    
    $('.pageslider').mouseleave(function () {
        $(this).delay(1500).fadeOut();
        $(this).parent().find('.exppagesTitle').removeClass('hr');
    });
	
    
     /* by Alena */
        var siz = $(".itemPicsList li").size();
        $(".itemPicsList ul").css("width", 112*siz+"px");    
        var siz2 = $(".prodListIn li").size();
        $(".prodListIn ul").css("width", 112*siz2+"px"); 

    
        //if($(".itemPicsList2").size()){
        //$('.itemPicsList2').jcarousel({
        //    buttonNextHTML: "<a></a>",
 	    //    buttonPrevHTML: "<a></a>",
        //    visible:1, 
        //    scroll:1
        //});
        //}; 
        
        if($(".itemPicsList").size()){
        jQuery(".itemPicsList").tinyscrollbar({ axis: 'x',size: 760, sizethumb: 38});
        }; 
        
        if($(".itemPicsList").size()){
        jQuery(".itemPicsList").tinyscrollbar({ axis: 'x',size: 760, sizethumb: 38}); }; 
        if($(".itemPicsList").size()){
        jQuery(".prodListIn").tinyscrollbar({ axis: 'x',size: 760, sizethumb: 38}); }; 
    
       
        
        $('.siteMapIn').first().css('border-top','none');
        
        $('.borderReg').first().css('border-top','none');
        $('.borderReg').first().find('span').css('color','#546d7f');
        $('.borderReg').eq(3).css('padding-bottom','40px');
        $('.borderReg').eq(4).find('label').css({'margin-top':'4px','width':'145px'});
        $('.borderReg').eq(4).find('label.chLabel').css({'margin-top':'4px','width':'24px'});
        $('.borderReg').eq(4).find('span').css('color','#546d7f');
         
        $(".adsChooseL .adsBut, .adsChooseL a").live("click", function(){
          $('.adsChoose').children().addClass('inAct');
          $(this).parent().removeClass('inAct');
          $('.adsTable').slideUp('slow');
          $('.adsText').slideDown('slow');
          $('.adsSelect input').css('display','block');
        });
        $(".adsChooseR .adsBut, .adsChooseR a").live("click", function(){
          $('.adsChoose').children().addClass('inAct');
          $(this).parent().removeClass('inAct');
          $('.adsText').slideUp('slow');
          $('.adsTable').slideDown('slow');
          $('.adsSelect input').css('display','none');
        });

    /* by Alena    
    
        $("a.fLink").fancybox();*/
    /*if($("select").size()){
    var params = {
	        changedEl: "select",
	        visRows: 25,
	        scrollArrows: true
	    }
	    cuSel(params);
    }; */
    
    
    
    /* Aleksey */       
    
    $('.lMenu2 > ul > li').live("click", function(){
        $('.lMenu2 > ul > li').removeClass('a');
        $(this).addClass('a');
    });
    $('.lMenu2 > ul > li.a').live("click", function(){
        $('.lMenu2 > ul > li').removeClass('a');
        
        });
    
    $('.faqList li span').click(function(){
        $(' + .faqAnswer',this).slideToggle('fast');    
    });
    
    $('.searchBlock ul li').click(function(){
        $('.searchBlock ul li').removeClass('a');
        $(this).addClass('a');
    });
    
    $('.booksTabs li').click(function(){
        $('.booksTabs li').removeClass('a');
        $(this).addClass('a');
    });
    
    $('.starsInput').each(function(){
        var jRoot = $(this);
        jRoot.find('input:hidden').val('');
        
        $(this).mouseleave(function(){
            var val = jRoot.find('input:hidden').val() || 0;
            jRoot.find('a:gt('+ (val-1) +')').removeClass('on');
            jRoot.find('a:lt('+ val +')').addClass('on');
        })
        .find('a')
            .hover(
                function(){
                    $(this).nextAll('a').removeClass('on');
                    $(this).prevAll('a').andSelf().addClass('on');
                },
                function(){
                    $(this).siblings('a').andSelf().removeClass('on');
                }
            )
            .click(function(){
                var val = jRoot.find('a').index(this) + 1;
                jRoot.find('input:hidden').val(val);
                jRoot.find('a:gt('+ (val-1) +')').removeClass('on');
                jRoot.find('a:lt('+ val +')').addClass('on');
                return false;
            });
    });
    
    $('.adsTar a').fancybox({
        padding: 0,
        autoCenter: false,
        closeBtn: false,
        closeClick: true
    });
    
    /*
     $(".persTabs ul li a").click(function(){
             $(".persTabs ul li").removeClass("a");
             $(this).closest('li').addClass("a");
             var t = $(this).closest('li').attr("rel");
             $(".persBlock").hide();
             $('.persBlock[rel="'+t+'"]').show();  
         });
     */
     
     $(".backTop").hide();
 
    $(function () {
        $(window).scroll(function () {
            if ($(this).scrollTop() > 600) // количество прокрученных пикселей, после которых начинает отображаться кнопка вверх на сайте.
                        {
                $('.backTop').fadeIn();
            } else {
                $('.backTop').fadeOut();
            }
        });
 
        // возвращаемся при клике вверх на 0-й пиксель
        $('.backTop').click(function () {
            $('body,html').animate({
                scrollTop: 0
            }, 800); // скорость прокрутки
            return false;
        });
    });
    
   
    
    $('.hmlWind > a').click(function(){
        $('.hmlWind').hide();
    });
     $('.hMenuList > a').click(function(){
        $('.hmlWind').show();
     });

     $('.hmlWind').mouseleave(function () {
         $(this).hide();
     });
     
    
    $(window).load(function(){     
        if($('.middle').width() > 1200){
        $('.hmlWind').addClass('a');
    }
    else {
        $('.hmlWind').removeClass('a');
    }          
    });
    
    $(window).resize(function(){     
        if($('.middle').width() > 1200){
        $('.hmlWind').addClass('a');
    }
    else {
        $('.hmlWind').removeClass('a');
    }          
    });
    
    if($("#slides").size()){
    $('#slides').slides({
				play: 5000,
				pause: 2500,
				hoverPause: true,
				animationStart: function(current){
					$('.caption').animate({
						bottom:-500
					},100);
					if (window.console && console.log) {
						// example return of current slide number
						console.log('animationStart on slide: ', current);
					};
				},
				animationComplete: function(current){
					$('.caption').animate({
						bottom:0
					},200);
					if (window.console && console.log) {
						// example return of current slide number
						console.log('animationComplete on slide: ', current);
					};
				},
				slidesLoaded: function() {
					$('.caption').animate({
						bottom:0
					},200);
				}
			});
    };
    
   /* if ($(".hedList ul li").size() > 10) {
        $('body').find('.more10').css('display','block');
    };
    $('.hiddenList').live("click", function () {
        $('.hedList ul').css('height', 'auto');
        $(this).text('Свернуть');
        $(this).addClass('shownList');
        $(this).removeClass('hiddenList');
    });

    $('.shownList').live("click", function () {
        $('.hedList ul').css('height', '60px');
        $(this).text('Остальные');
        $(this).addClass('hiddenList');
        $(this).removeClass('shownList');
        
    }); */
   
   


    /// for Profile:
    $(".userMoreInfoTabs a").live("click", function () {
        var slNum = $(this).attr("rel");
        $(".userMoreInfoTabs a").parents().find('li.a').removeClass('a');
        $(this).parent().addClass('a');
        $(".showBlock").hide();
        $("#" + slNum).show();
        return false;
    });


    
});