$('#tabs li').click(function () {
    $('#tabs li').attr('id','');
    $(this).attr('id', 'current');
});