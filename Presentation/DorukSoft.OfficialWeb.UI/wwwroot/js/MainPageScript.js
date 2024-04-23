$(function () {
    $('.js-nav a, .js-connect').click(function (e) {
        e.preventDefault();
        $('body, html').animate({
            scrollTop: $($.attr(this, 'href')).offset().top
        }, 750);
    });
});

$(document).ready(function () {
    $.get("/Home/Contact", function (data) {
        $("#contact").html(data);
    });
});