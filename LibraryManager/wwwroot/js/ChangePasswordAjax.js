$(document).ready(function () {
    $("#SaveChange").click(function () {
        var error = $("#Error").empty();
        var success = $("#Success").empty();
        var CurrentPassowrd = $("#CurrentPassword").val();
        var NewPassword = $("#NewPassword").val();
        var ConfirmPassword = $("#ConfirmPassword").val();
        $("#ModelOnly").html("");
        $.ajax({
            type: "post",
            url: "/Employee/ChangePassword?Current=" + CurrentPassowrd + "&New=" + NewPassword + "&Confirm=" + ConfirmPassword,
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