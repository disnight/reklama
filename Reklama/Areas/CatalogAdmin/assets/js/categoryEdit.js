(function ($) {
    $.extend({
        CategoryEdit: {
            saveSectionResult: 0,
            sectionIDAttribute: "data-sectionid",
            parameterIDAttribute: "data-parameterid",
            commonContainerId: "#common-section-container",
            commonParametersId: ".common-parameters-container",

            addSection: function (el) {
                var nameInput = $(el.currentTarget).closest(".parameters-section.create").find(".section-name");
                var name = nameInput.val();
                if (!name || name.replace(" ", "") == "") {
                    alert("Введите название секции");
                    return false;
                }

                var categoryId = $("#Category_ID").val();

                $.post(
					"/Category/AddSection",
					{ categoryID: categoryId, name: name },
					function (data) {
					    if (data == 0) {
					        alert("Что-то пошло не так!");
					        return false;
					    }

					    $("#newSectionTemplate").tmpl({ name: name, sectionID: data }).appendTo($.CategoryEdit.commonContainerId);
					    nameInput.val("");
					    return false;
					}
				);
                return false;
            },

            updateSection: function (el) {
                var nameInput = $(el.currentTarget).closest(".parameters-section").find(".section-name");
                var name = nameInput.val();
                if (!name || name.replace(" ", "") == "") {
                    alert("Название секции не может быть пустым");
                    return false;
                }

                var sectionId = $(el.currentTarget).attr($.CategoryEdit.sectionIDAttribute);

                $.post(
					"/Category/UpdateSection",
					{ sectionID: sectionId, name: name },
					function (data) {
					    alert(data);
					    return false;
					}
				);
                return false;
            },

            removeSection: function (el) {

                var sectionConfirm = confirm("Вы уверены, что хотите удалить выбранную секцию?");

                if (sectionConfirm) {
                    var sectionId = $(el.currentTarget).attr($.CategoryEdit.sectionIDAttribute);

                    $.post(
                        "/Category/RemoveSection",
                        { sectionID: sectionId },
                        function (data) {
                            alert(data.Message);

                            if (data.Result)
                                $(el.currentTarget).closest(".row").remove();

                            return false;
                        }
                    );
                }

                return false;
            },

            addParametr: function (el) {
                var nameInput = $(el.currentTarget).closest(".parameter-container.create").find(".param-name");
                var name = nameInput.val();

                var descInput = $(el.currentTarget).closest(".parameter-container.create").find(".param-desc");
                var desc = descInput.val();

                if (!name || name.replace(" ", "") == "") {
                    alert("Введите название параметра");
                    return false;
                }

                var sectionId = $(el.currentTarget).attr($.CategoryEdit.sectionIDAttribute);

                $.post(
                   "/Category/AddParametr",
                   { sectionID: sectionId, name: name, desc: desc },
                   function (data) {
                       if (data == 0) {
                           alert("Что-то пошло не так!");
                           return false;
                       }

                       var currentContainer = $(el.currentTarget).closest(".row").find($.CategoryEdit.commonParametersId);
                       $("#newParameterTemplate").tmpl({ name: name, desc: desc, paramID: data }).appendTo(currentContainer);
                       nameInput.val("");
                       descInput.val("");
                       return false;
                   }
               );
                return false;
            },

            updateParametr: function (el) {
                var nameInput = $(el.currentTarget).closest(".parameter-container").find(".param-name");
                var name = nameInput.val();

                var descInput = $(el.currentTarget).closest(".parameter-container").find(".param-desc");
                var desc = descInput.val();

                if (!name || name.replace(" ", "") == "") {
                    alert("Название параметра не может быть пустым");
                    return false;
                }

                var paramId = $(el.currentTarget).attr($.CategoryEdit.parameterIDAttribute);

                $.post(
                   "/Category/UpdateParametr",
                   { paramID: paramId, name: name, desc: desc },
                   function (data) {
                       alert(data);
                       return false;
                   }
               );
                return false;
            },

            removeParametr: function (el) {

                var sectionConfirm = confirm("Вы уверены, что хотите удалить выбранный параметр?");

                if (sectionConfirm) {
                    var paramId = $(el.currentTarget).attr($.CategoryEdit.parameterIDAttribute);

                    $.post(
                        "/Category/RemoveParametr",
                        { paramID: paramId },
                        function (data) {
                            alert(data.Message);

                            if (data.Result)
                                $(el.currentTarget).closest(".parameter-container").remove();
                            return false;
                        }
                    );
                }
                return false;
            },
        }
    });
})(jQuery);

jQuery(document).ready(function () {
    jQuery(document).on("click", ".create-section", $.CategoryEdit.addSection);
    jQuery(document).on("click", ".addParametr", $.CategoryEdit.addParametr);
    jQuery(document).on("click", ".param-edit-button", $.CategoryEdit.updateParametr);
    jQuery(document).on("click", ".section-edit-button", $.CategoryEdit.updateSection);
    jQuery(document).on("click", ".param-remove-button", $.CategoryEdit.removeParametr);
    jQuery(document).on("click", ".section-remove-button", $.CategoryEdit.removeSection);
});