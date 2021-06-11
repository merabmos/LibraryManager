function OnChange(action, controller, changersectionId, replaceablesectionId) {
    let replaceableSection = $("#" + replaceablesectionId);
    let changerSectionId = $("#" + changersectionId).val();
    replaceableSection.html("");
    $.ajax({
        url: '/' + controller + '/' + action,
        type: "post",
        data: JSON.stringify(changerSectionId),
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            for (let i = 0; i < response.length; i++) {
                replaceableSection.append("<option value='" + response[i].id + "'>" + response[i].name + "</option>")
            }
        }, error: function (response) {
            alert("Something Went wrong!");
        }
    });
}
