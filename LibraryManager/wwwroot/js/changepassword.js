$(document).ready(function () {
    $("#SaveChange").click(function () {
        var error = $("#Error").empty();
        var success = $("#Success").empty();
        var CurrentPassowrd = $("#CurrentPassword").val();
        var NewPassword = $("#NewPassword").val();
        var ConfirmPassword = $("#ConfirmPassword").val();
        var request = { Current: CurrentPassowrd, New: NewPassword, Confirm: ConfirmPassword };
        $.ajax({
            url: "/Employee/ChangePassword",
            type: "post",
            data: JSON.stringify(request),
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            // data: { Current: CurrentPassowrd, New: NewPassword,Confirm : ConfirmPassword },
            success: function (response) {
                if (response.valid) {
                    success.html(response.successMessage);
                }
                else {
                    for (var i = 0; i < response.validationsMessage.length; i++) {
                        error.append(
                            "<li  class='text-danger'>" + response.validationsMessage[i] + "</li>"
                        );
                    }
                }
            }
        });
    });
});