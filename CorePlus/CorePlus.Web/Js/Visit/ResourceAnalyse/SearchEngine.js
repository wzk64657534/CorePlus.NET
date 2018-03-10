$(function () {
    var startDate, endDate;
    var tabs = '#tabs_report li[class^="item"]';
    var tag = '1';


    function SetBanner(v) {
        Visit.SetTimePeriod(v);
        Visit.SetSelected('#time_tool_quick_key li', 'selected', v);
        startDate = Visit.StartDate;
        endDate = Visit.EndDate;

        // banner
        $.ajax({
            url: '/SearchEngine/Banner',
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

        var itemX = [];
        var cnt = CoreDate.DateDiff(endDate, startDate);
        if (cnt > 0) {
            for (var i = 0; i < cnt; i++) {
                var d = CoreDate.AddDaysByDate(startDate, i);
                itemX.push(d);
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
            url: '/SearchEngine/GetDataOfChartForXY',
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
                        categories: itemX
                    },
                    yAxis: {
                        title: {
                            text: 'PV'
                        },
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

        Report(startDate, endDate, 1, '');
    }

    ////////////////////////////
    function Report(s, e, i, t) {
        var cols = [];
        switch (tag) {
            case '1':
                cols.push({ field: 'RefererName', title: '搜索引擎', width: 200, align: 'center', formatter: function (val, rec) {
                    switch (val) {
                        case "baidu": return "百度";
                        case "google": return "谷歌";
                        case "so": return "360";
                        case "360":
                            return "360搜索";
                        case "sogou": return "搜狗";
                        case "yahoo": return "雅虎";
                        case "soso": return "搜搜";
                        case "yodao": return "有道";
                        default: return "未知";
                    }
                }
                });
                cols.push({ field: 'VisitCount', title: '来访次数', width: 200, align: 'center' });
                cols.push({ field: 'PV', title: '浏览次数(PV)', width: 200, align: 'center' });
                cols.push({ field: 'UV', title: '独立访客(UV)', width: 200, align: 'center' });
                cols.push({ field: 'IP', title: 'IP', width: 200, align: 'center' });
                cols.push({ field: 'KeywordCount', title: '关键词数', width: 200, align: 'center' });
                break;
        }




        $('#dg').datagrid({
            //页面加载完成之后，表格会自动往后台发送一个请求，请求中会自动带上：  page【请求的当前页】，rows【请求的行数】。返回的数据格式：{total:40,rows:[{},{}]}
            url: '/SearchEngine/GetDataOfGrid',
            width: function () { return document.body.clientWidth * 0.9 },
            height: 310,
            fitColumns: true, //列自适应。
            remoteSort: false,
            idField: 'RefererName', //主键列
            pagination: false, //允许分页。
            singleSelect: true, //单行选择。
            pageSize: 10, //默认的一页大小，这个值会随着第一次请求的时候做完rows的参数发送到后台。
            pageNumber: 1, //默认的当前页
            pageList: [10], //一页多少条数据的选择项。
            queryParams: { start: s, end: e, tag: i, title: t }, //请求的额外的参数。
            columns: [cols]
        });
    }

    $('#time_tool_quick_key li').css('cursor', 'pointer');
    $('#time_tool_quick_key li').click(function () {
        var item = $(this).find('a').eq(0);
        var v = item.attr('info');
        SetBanner(v);
    });

    $('#btnSearch').click(function () {
        var t = $('#txbSearch').val();

        Report(startDate, endDate, 1, t);
    });
    SetBanner(0);
});