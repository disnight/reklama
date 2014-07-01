(function ($) {
    $.extend({

        /*CUSTOM SELECT BOXES INIT*/
        initAutoFieldFill: function () {
            var mess = 'textarea.message';
            var desc = 'textarea.desc';

            if ($(mess).length > 0) {

                var limit = 100; //parseInt($(desc).attr('maxlength'));

                $(document).on('focus', desc, function () {
                    $(desc).addClass('active');
                });

                $(document).on('keyup', mess, function () {
                    if (!$(desc).hasClass('active')) {
                        var s = $(this).val();
                        while (s.indexOf("\n") > -1)
                            s = s.replace("\n", " ");
                        $(desc).val(s);
                        var text = $(desc).val();
                        var chars = text.length;
                        if (chars > limit) {
                            var new_text = text.substr(0, limit);
                            $(desc).val(new_text);
                        }
                    }
                });
            }
        },
        
        initCommon: function () {
            $("#rules").change(function () {
                var sub = $("#createSubmit");
                if (sub.prop('disabled')) {
                    $(sub).removeClass("disabled");
                    $(sub).prop('disabled', false);
                } else {
                    $(sub).addClass("disabled");
                    $(sub).prop('disabled', true);
                }
            });
        },
        
        initPager: function () {
            /* 
				get snap amount programmatically or just set it directly (e.g. "273") 
				in this example, the snap amount is list item's (li) outer-width (width+margins)
				*/
            var amount = Math.max.apply(Math, $(".pagesliderTop li").map(function () { return $(this).outerWidth(true); }).get());

            $(".pagesliderTop").mCustomScrollbar({
                axis: "x",
                theme: "inset",
                advanced: {
                    autoExpandHorizontalScroll: true
                },
                scrollButtons: {
                    enable: false,
                    scrollType: "stepped"
                },
                //keyboard:{scrollType:"stepped"},
                //snapAmount:amount,
                //mouseWheel:{scrollAmount:amount}
            });
        },
        
        removeTags: function(text) {
        var regex = /(<([^>]+)>)/ig;
        return text.replace(regex, "");
        },

        init: function () {
            $.initCommon();
            $.initPager();
            $.initAutoFieldFill();
        }
    });
})(jQuery);

jQuery(function ($) {
    $.init();
});