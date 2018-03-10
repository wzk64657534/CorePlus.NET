$(function () {
    $.ajax({
        url: '/VisitTrack/GetTrackJs',
        dataType: 'text',
        success: function (response) {
            $('#trackJs').text(response);
        },
        error: function (xhr, msg, exp) {
            $('#trackJs').text(msg);
        }
    });
});