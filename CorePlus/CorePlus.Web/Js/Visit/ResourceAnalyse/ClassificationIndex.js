$(function () {
    var startDate, endDate;

    function SetBanner(v) {
        Visit.SetTimePeriod(v);
        Visit.SetSelected('#time_tool_quick_key li', 'selected', v);
        startDate = Visit.StartDate;
        endDate = Visit.EndDate;

        // banner
        $.ajax({
            url: '/ResourceClassification/Banner',
            data: { start: startDate, end: endDate },
            dataType: 'json',
            success: function (response) {
                $('#banner_vc').html(response.VISITCOUNT);
                $('#banner_uv').html(response.UV);
                $('#banner_ip').html(response.IP);
                $('#banner_newuv').html(response.NEWUV);
                $('#banner_pv').html(response.PV);
                $('#banner_percent').html(String(response.PERCENT) + '%');
            },
            error: function (xhr, msg, exp) {
                $('#banner_vc').html('-1');
                $('#banner_pv').html('-1');
                $('#banner_uv').html('-1');
                $('#banner_ip').html('-1');
                $('#banner_newuv').html('-1');
                $('#banner_percent').html('-1');
            }
        });

        //x轴刻度
        var xtick = 4;
        var itemX = [];
        var cnt = CoreDate.DateDiff(endDate, startDate);
        if (cnt > 0) {
            for (var i = 0; i <= cnt; i++) {
                var d = CoreDate.AddDaysByDate(startDate, i);
                itemX.push(d);
            }
            if (cnt <= 7) {
                xtick = 1;
            }
        }
        else {
            itemX.push('0:00');
            itemX.push('1:00');
            itemX.push('2:00');
            itemX.push('3:00');
            itemX.push('4:00');
            itemX.push('5:00');
            itemX.push('6:00');
            itemX.push('7:00');
            itemX.push('8:00');
            itemX.push('9:00');
            itemX.push('10:00');
            itemX.push('11:00');
            itemX.push('12:00');
            itemX.push('13:00');
            itemX.push('14:00');
            itemX.push('15:00');
            itemX.push('16:00');
            itemX.push('17:00');
            itemX.push('18:00');
            itemX.push('19:00');
            itemX.push('20:00');
            itemX.push('21:00');
            itemX.push('22:00');
            itemX.push('23:00');
        }

        $.ajax({
            url: '/ResourceClassification/GetDataOfChartForXY',
            data: { start: startDate, end: endDate },
            dataType: 'json',
            success: function (response) {
                // chart
                var datas = response;

                // create a chart
                $('#divChart').highcharts({
                    title: {
                        text: '',
                        x: -20
                    },

                    subtitle: {
                        text: '',
                        x: -20
                    },
                    xAxis: {
                        tickInterval: xtick,
                        categories: itemX
                    },
                    yAxis: {
                        title: {
                            text: 'PV'
                        },
                        min: 0,
                        plotLines: [{
                            value: 0,
                            width: 1,
                            color: '#808080'
                        }]
                    },

                    tooltip: {
                        crosshairs: true,
                        shared: true
                    },
                    legend: {
                        layout: 'vertical',
                        align: 'right',
                        verticalAlign: 'middle',
                        borderWidth: 0
                    },
                    series: datas
                });
            },
            error: function (xhr, msg, ex) {
                $('#divChart').html('error:' + msg);
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