function loadPartialView(e, data) {
    e.preventDefault();
    console.log('/Home/' + data);
    $('#sections').hide();
    $.ajax({
        url: '/Home/' + data,
        type: 'GET',
        success: function (data) {
            $('#partialContainer').html(data);
        },
        error: function () {
            alert('Partial View yüklenirken bir hata oluştu.');
        }
    });
}

function closePartialView() {
    $("#partialContainer").html("");
    $("#sections").css("display", "block");
}