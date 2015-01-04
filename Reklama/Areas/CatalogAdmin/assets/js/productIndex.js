(function ($) {
    $.extend({
        pruductIndex: {
            inputInit: function () {
                /*----------- BEGIN chosen CODE -------------------------*/
                $(".chzn-select").chosen({ no_results_text: "Oops, nothing found!" }).change(function () {
                    var getCollectionUrl = "/CatalogAdmin/Product/GetProduct";
                    var result = $.ajaxService.GetJson(getCollectionUrl, { categoryID: this.value });
                    if (result && result.length > 0) {
                        $(".no-results").hide();
                        $("#result-container").empty();
                        for (var i = 0; i < result.length; i++) {
                            $("#product-item-template").tmpl({ categoryName: result[i].CategoryName, manufacturerName: result[i].ManufacturerName, title: result[i].Title, isActive: result[i].IsActive, isPopular: result[i].IsPopular, ID: result[i].ID }, {
                                getButtonClass: function (isActive) {
                                    if (isActive) {
                                        return "btn-danger";
                                    }
                                    return "btn-success";
                                },
                                getIconClass: function (isActive) {
                                    if (isActive) {
                                        return "icon-ban-circle";
                                    }
                                    return "icon-ok";
                                },
                                getCheckbox: function (value) {
                                    if (value) {
                                        return "checked='checked'";
                                    }
                                    return "";
                                }
                            }).appendTo("#result-container");
                        }
                        $(".table-responsive").show();
                    } else {
                        $(".no-results").show();
                        $(".table-responsive").hide();
                    }


                });
                /*----------- END chosen CODE -------------------------*/
            },

            init: function () {
                this.inputInit();
            }
        }
    });
})(jQuery);

jQuery(document).ready(function () {
    $.pruductIndex.init();
});