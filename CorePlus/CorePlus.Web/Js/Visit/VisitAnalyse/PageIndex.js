$(function () {
    var startDate, endDate;
    var tabs = '#tabs_report li[class^="item"]';
    var tag = '1';

    function SetBanner(v) {
        Visit.SetTimePeriod(v);
        Visit.SetSelected('#time_tool_quick_key li', 'selected', v);
        startDate = Visit.StartDate;
        endDate = Visit.EndDate;

        $.ajax({
            url: '/VisitPage/Banner',
            data: { start: startDate, end: endDate },
            dataType: 'json',
            success: function (response) {
                $('#banner_pv').html(response.PV);
                $('#banner_uv').html(response.UV);
                $('#banner_ip').html(response.IP);
                $('#banner_pvavg').html(response.PVAVG);
            },
            error: function (xhr, msg, exp) {
                $('#banner_pv').html('-1');
                $('#banner_uv').html('-1');
                $('#banner_ip').html('-1');
                $('#banner_pvavg').html('-1');
            }
        });

        Report(startDate, endDate, tag, '');
    }

    $('#time_tool_quick_key li').css('cursor', 'pointer');
    $('#time_tool_quick_key li').click(function () {
        var item = $(this).find('a').eq(0);
        var v = item.attr('info');
        SetBanner(v);
    });
    ////////////////////////////////////////////////////////////////////////////////////////////////
    function Report(s, e, i, t) {
        var cols = [];
        switch (tag) {
            case '1':
                cols.push({ field: 'VisitingUrl', title: '受访页面' });
                cols.push({ field: 'PV', title: '浏览次数(PV)' });
                cols.push({ field: 'UV', title: '独立访客(UV)' });
                cols.push({ field: 'IP', title: 'IP' });
                cols.push({ field: 'PvAvg', title: '人均浏览页数' });
                cols.push({ field: 'OutPV', title: '输出PV' });
                cols.push({ field: 'TotalTimePeriod', title: '页面停留总时长' });
                cols.push({ field: 'AvgTimePeriod', title: '平均页面停留时间' });
                break;
            case '2':
                cols.push({ field: 'VisitingUrl', title: '站内入口' });
                cols.push({ field: 'PV', title: '浏览次数' });
                cols.push({ field: 'InCnt', title: '进入次数' });
                //cols.push({ field: 'JumpOutRate', title: '跳出率' });
                cols.push({ field: 'Deep', title: '平均访问深度&nbsp;<span title="每个访问次数给统计对象所带来的页面浏览量的均值，平均访问深度=浏览量(PV)/访问次数"><img alt="" src="../../Content/images/wh.png" border="0" width="12" height="12" /></span>' });
                cols.push({ field: 'AvgTimePeriod', title: '平均访问时长' });
                cols.push({ field: 'InCntRate', title: '入口页次数占比(%)&nbsp;<span title="入口页次数占比=进入次数/全站访问次数。该页面与所有站内入口相比，成为入口页面的几率，占比高的入口页面可能拥有更多的站外链接或更高权重。"><img alt="" src="../../Content/images/wh.png" border="0" width="12" height="12" /></span>' });
                break;
        }

        $('#dg').datagrid({
            //页面加载完成之后，表格会自动往后台发送一个请求，请求中会自动带上：  page【请求的当前页】，rows【请求的行数】。返回的数据格式：{total:40,rows:[{},{}]}
            url: '/VisitPage/GetDataOfGrid',
            width: function () { return document.body.clientWidth * 0.9 },
            height: 310,
            fitColumns: true, //列自适应。
            remoteSort: false,
            idField: 'VisitingUrl', //主键列
            pagination: true, //允许分页。
            singleSelect: true, //单行选择。
            pageSize: 10, //默认的一页大小，这个值会随着第一次请求的时候做完rows的参数发送到后台。
            pageNumber: 1, //默认的当前页
            pageList: [10], //一页多少条数据的选择项。
            queryParams: { start: s, end: e, tag: i, title: t }, //请求的额外的参数。
            columns: [cols]
        });
    }

    $(tabs).css('cursor', 'pointer');
    $(tabs).click(function () {
        $(tabs).removeClass('selected');
        $(tabs).addClass('link');

        $(this).addClass('selected');
        $(this).removeClass('link');

        tag = $(this).attr('tag');
        Report(startDate, endDate, tag, '');
    });

    $('#btnSearch').click(function () {
        var t = $('#txbSearch').val();

        Report(startDate, endDate, tag, t);
    });

    // 执行默认设置
    $(tabs).removeClass('selected');
    $(tabs).addClass('link');
    $(tabs).eq(0).addClass('selected');
    $(tabs).eq(0).removeClass('link');

    SetBanner(0);
});