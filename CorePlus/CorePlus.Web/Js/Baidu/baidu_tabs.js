$(function () {
    $('.ui_tab ul li').click(function () {
        $('.ui_tab ul li').each(function () {
            $(this).removeClass('active first');
        });
        $(this).addClass('active first');
        var link = $(this).attr('id');
        $('#ifrmBody').attr('src', "/" + link + "/Index");
    });
});