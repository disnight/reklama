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

        init: function () {
            $.initAutoFieldFill();
        }
    });
})(jQuery);

jQuery(function ($) {
    $.init();
});