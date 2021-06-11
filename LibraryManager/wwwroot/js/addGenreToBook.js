function AddGenreToBook(action, controller, genreId,PlaceGenresForBook,ForSavedIds) {
    let genre = $("#" + genreId).val();
    let placeForNewSection = $("#" + PlaceGenresForBook).val();
    let savedIds = $("#" + ForSavedIds)
    $.ajax({
        url: '/' + controller + '/' + action,
        type: "post",
        data: JSON.stringify(genre),
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            
            placeForNewSection.html();
            placeForNewSection.append("<section id='newSection'>" + "</section>")
            for (let i = 0; i < response.length; i++) {
              
                    if(genre == response[i].id)
                    {
                        savedIds.append("<p id="+ response[i].id +">"+ response[i].name +"</p>");
                    }
                $("#newSection").append("<option value='" + response[i].id + "'>" + response[i].name + "</option>")
            }
        }, error: function (response) {
            alert("Something Went wrong!");
        }
    });
}
