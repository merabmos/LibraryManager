$(document).ready(function () {
    let DropDown = $("#SectionId");
    $("#SectorId").change(function () {
        let Id = $("#SectorId").val();
        $.ajax({
            url: '/' + "BooksShelf" + '/' + "GetSectionBySector",
            type: "post",
            data: JSON.stringify(Id),
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                DropDown.html("<option selected=\"selected\">\n" +
                    "Select Section\n" +
                    "</option>");
                console.log(response);
                for (let i = 0; i < response.length; i++) {
                    DropDown.append("<option value='" + response[i].id + "'>" + response[i].name + "</option>")
                }
            }, error: function (response) {
                alert("Something Went wrong!");
            }
        });
    });
});