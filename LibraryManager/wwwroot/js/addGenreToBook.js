function AddGenreToBook(action, controller, genreId,ForSavedIds) {
    let genre = $("#" + genreId).val();
    let savedIds = $("#" + ForSavedIds)
    $.ajax({
        url: '/' + controller + '/' + action,
        type: "post",
        data: JSON.stringify(genre),
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            console.log(response);
            for (let i = 0; i < response.length; i++) {
                    if(genre === response[i].id)
                    {
                        savedIds.append("<p id="+ response[i].id +">"+ response[i].name +"</p>");
                    }
            }
        }, error: function (response) {
            alert("Something Went wrong!");
        }
    });
}
