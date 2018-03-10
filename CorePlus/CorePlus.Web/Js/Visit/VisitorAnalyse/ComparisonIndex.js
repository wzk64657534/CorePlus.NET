$(function () {
    var startDate, endDate;

    function SetBanner(v) {
        Visit.SetTimePeriod(v);
        Visit.SetSelected('#time_tool_quick_key li', 'selected', v);
        startDate = Visit.StartDate;
        endDate = Visit.EndDate;

        // banner
        $.ajax({
            url: '/VisitorComparison/GetListVisitor',
            data: { start: startDate, end: endDate, tag: 1 },
            dataType: 'json',
            success: function (response) {

                var newvis = response[0];
                var oldvis = response[1];
                $('#td_vc').html(newvis.VC);
                $('#td_uv').html(newvis.UV);
                $('#td_pv').html(newvis.PV);
                $('#td_pvavg').html(newvis.PVAVG);
                $('#td_avgtime').html(newvis.AVGTIME);


                $('#td_new_vc').html(oldvis.VC);
                $('#td_new_uv').html(oldvis.UV);
                $('#td_new_pv').html(oldvis.PV);
                $('#td_new_pvavg').html(oldvis.PVAVG);
                $('#td_new_avgtime').html(oldvis.AVGTIME);


            },
            error: function (xhr, msg, exp) {
                $('#td_vc').html('-1');
                $('#td_uv').html('-1');
                $('#td_pv').html('-1');

                $('#td_new_vc').html('-1');
                $('#td_new_uv').html('-1');
                $('#td_new_pv').html('-1');
                $('#td_new_pvavg').html('-1');
                $('#td_new_avgtime').html('-1');
            }
        });
    }

    $('#time_tool_quick_key li').css('cursor', 'pointer');
    $('#time_tool_quick_key li').click(function () {
        var item = $(this).find('a').eq(0);
        var v = item.attr('info');
        SetBanner(v);
    });

    SetBanner(0);
});