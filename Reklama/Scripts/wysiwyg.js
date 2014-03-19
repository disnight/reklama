$(document).ready(function () {
    CKEDITOR.replace("Description", {
        filebrowserBrowseUrl: "/Scripts/ckfinder/ckfinder.html",
        filebrowserImageBrowseUrl: "/Scripts/ckfinder/ckfinder.html?type=Images",
        filebrowserFlashBrowseUrl: "/Scripts/ckfinder/ckfinder.html?type=Flash",
        filebrowserUploadUrl: "/Scripts/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files",
        filebrowserImageUploadUrl: "/Scripts/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images",
        filebrowserFlashUploadUrl: "/Scripts/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash",
        filebrowserWindowWidth: "1000",
        filebrowserWindowHeight: "700"
    });
});