function do_filter(controller) {
    let creatorEmployeeId = $("#CreatorEmployeeId option:selected").val();
    let modifierEmployeeId = $("#ModifierEmployeeId option:selected").val();
    let insertStartDate = $("#InsertStartDate").val();
    let insertEndDate = $("#InsertEndDate").val();
    let modifyStartDate = $("#ModifyStartDate").val();
    let modifyEndDate = $("#ModifyEndDate").val();
    console.log(controller);
    let request = {
        CreatorId: creatorEmployeeId,
        ModifierId: modifierEmployeeId,
        InsertStartDate: insertStartDate,
        InsertEndDate: insertEndDate,
        ModifyStartDate: modifyStartDate,
        ModifyEndDate: modifyEndDate,
    };
    let DataTBody = $("#Data");
    DataTBody.html("");
    $.ajax({
        url: '/' + controller + '/SearchRecord',
        type: "post",
        data: JSON.stringify(request),
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            for (let i = 0; i < response.length; i++) {
                let row = "<tr>" +
                    "<td>" + response[i].name + "</td>" +
                    "<td>" + response[i].insertDate + "</td>" +
                    "<td>" + response[i].modifyDate + "</td>" +
                    "<td>" + response[i].creatorEmployee + "</td>" +
                    "<td>" + response[i].modifierEmployee + "</td>" +
                    "<td>" +
                    "<a" + "href=" + "/Sector/Edit?Id=" + response[i].id + ">" + "Edit |" + "</a>" +
                    "<a" + "href=" + "/Sector/Delete?Id=" + response[i].id + ">" + "Delete" + "</a>" +
                    "</td>" +
                    "</tr>";
                DataTBody.append(row);
            }
        }, error: function (response) {
            alert("Something Went wrong!");
        }
    });
}
