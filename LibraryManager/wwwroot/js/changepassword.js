$(document).ready(function () {
    $("#SaveChange").click(function () {
        let error = $("#Error").empty();
        let success = $("#Success").empty();
        let CurrentPassowrd = $("#CurrentPassword").val();
        let NewPassword = $("#NewPassword").val();
        let ConfirmPassword = $("#ConfirmPassword").val();
        let request = { Current: CurrentPassowrd, New: NewPassword, Confirm: ConfirmPassword };
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
                    for (let i = 0; i < response.validationsMessage.length; i++) {
                        error.append(
                            "<li  class='text-danger'>" + response.validationsMessage[i] + "</li>"
                        );
                    }
                }
            }, error: function (response) {
                alert("Something Went wrong!");
        }
        });
    });
});