function do_filter(controller) {
    var creatorEmployeeId = $("#CreatorEmployeeId option:selected").val();
    var modifierEmployeeId = $("#ModifierEmployeeId option:selected").val();
    var insertStartDate = $("#InsertStartDate").val();
    var insertEndDate = $("#InsertEndDate").val();
    var modifyStartDate = $("#ModifyStartDate").val();
    var modifyEndDate = $("#ModifyEndDate").val();
    console.log(controller);
    var request = {
        CreatorId: creatorEmployeeId,
        ModifierId: modifierEmployeeId,
        InsertStartDate: insertStartDate,
        InsertEndDate: insertEndDate,
        ModifyStartDate: modifyStartDate,
        ModifyEndDate: modifyEndDate,
    };
    var DataTBody = $("#Data");
    DataTBody.html("");
    $.ajax({
        url: '/' + controller + '/SearchRecord',
        type: "post",
        data: JSON.stringify(request),
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            for (var i = 0; i < response.length; i++) {
                //$.ajax({
                //    url: '/' + controller + '/CatchData',
                //    type: "post",
                //    contentType: "application/json; charset=utf-8",
                //    dataType: 'json',
                //    data: { response: response[i].name } 
                //});
                var row = "<tr>" +
                    "<td>" + response[i].name + "</td>" +
                    "<td>" + response[i].insertDate + "</td>" +
                    "<td>" + response[i].modifyDate + "</td>" +
                    "<td>" + response[i].creatorEmployee + "</td>" +
                    "<td>" + response[i].modifierEmployee + "</td>" +
                    "<td>" +
                    "<a" + " href=" + "/Sector/Edit?Id=" + response[i].id + ">" + "Edit |" + "</a>" +
                    "<a" + " href=" + "/Sector/Delete?Id=" + response[i].id + ">" + "Delete" + "</a>" +
                    "</td>" +
                    "</tr>";
                DataTBody.append(row);
            }
        }, error: function (response) {
            alert("Something Went wrong!");
        }
    });
}
//$(document).ready(function () {
//});
