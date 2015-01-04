(function ($) {
    $.extend({
        ajaxService: {
            GetJson: function (url, request, callback) {
                var results;
                $.ajax({
                    async: callback != undefined,
                    type: "post",
                    contentType: "application/json; charset=utf-8",
                    url: url,
                    data: JSON.stringify(request || {}),
                    dataType: "json",
                    success: function (data) {
                        results = data;
                        if (callback != undefined) {
                            callback(results);
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.log(jqHXR);
                        console.log(textStatus);
                        console.log(errorThrown);
                    }
                });
                return results;
            }
        }
    });
})(jQuery);