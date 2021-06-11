function AddGenreToBook(action, controller, genreId,GenresForBook) {
    let replaceableSection = $("#" + genreId).val();
    let placeForNewSection = $("#" + GenresForBook).val();
    
    $.ajax({
        url: '/' + controller + '/' + action,
        type: "post",
        data: JSON.stringify(replaceableSection),
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            placeForNewSection.html();
            placeForNewSection.append("<section id='newSection'>" + "</section>")
            for (let i = 0; i < response.length; i++) {
                $("#newSection").append("<option value='" + response[i].id + "'>" + response[i].name + "</option>")
            }
        }, error: function (response) {
            alert("Something Went wrong!");
        }
    });
}
