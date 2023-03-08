$('.toggle').click(function (e) {
    e.preventDefault();
    $(this).toggleClass('toggle-on');
    if ($(this).hasClass("toggle-on"))
        alert("ON")
    else
        alert("OFF")
});