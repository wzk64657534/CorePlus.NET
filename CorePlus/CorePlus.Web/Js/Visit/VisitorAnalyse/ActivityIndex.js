﻿$(function () {
    var startDate, endDate;
    var tabs = '#tabs_report li[class^="item"]';
    var tag = '1';

    function SelectedDate(v) {
        
        Visit.SetTimePeriod(v);
        Visit.SetSelected('#time_tool_quick_key li', 'selected', v);
        startDate = Visit.StartDate;
        endDate = Visit.EndDate;

        $('#time_tool_quick_key li').removeClass('selected');
        $('#time_tool_quick_key li').each(function () {
            var item = $(this).find('a:eq(0)');
            var v2 = item.attr('info');
            if (v2 == v) {
                $(this).addClass('selected');
            }
        });

        // banner
        $.ajax({
            url: '/VisitorActivity/Banner',
            data: { start: startDate, end: endDate },
            dataType: 'json',
            success: function (response) {
                $('#banner_vc').html(response.VISITCOUNT);
                $('#banner_pv').html(response.PV);
                $('#banner_avgtime').html(response.AVGTIME);
            },
            error: function (xhr, msg, exp) {

                $('#banner_vc').html('-1');
                $('#banner_pv').html('-1');
                $('#banner_avgtime').html('-1');
            }
        });

        Report(startDate, endDate, 1, '');
    }

    function Report(s, e, i, t) {
        var cols = [];
        switch (tag) {
            case '1':
                cols.push({ field: 'NAME', title: '访问时长', width: 200, align: 'center' });
                cols.push({ field: 'VC', title: '访问次数', width: 200, align: 'center' });
                cols.push({ field: 'VCPRE', title: '占比)', width: 200, align: 'center' });
                cols.push({ field: 'PV', title: '浏览次数（PV）', width: 200, align: 'center' });
                cols.push({ field: 'PVPRE', title: '占比', width: 200, align: 'center' });
                break;
        }

        $('#dg').datagrid({
            //页面加载完成之后，表格会自动往后台发送一个请求，请求中会自动带上：  page【请求的当前页】，rows【请求的行数】。返回的数据格式：{total:40,rows:[{},{}]}
            url: '/VisitorActivity/GetDataOfGrid',
            width: function () { return document.body.clientWidth * 0.9 },
            height: 310,
            fitColumns: true, //列自适应。
            remoteSort: false,
            idField: 'RefererSite', //主键列
            pagination: true, //允许分页。
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
        SelectedDate(v);
    });

    $('#btnSearch').click(function () {
        var t = $('#txbSearch').val();

        Report(startDate, endDate, 1, t);
    });

    SelectedDate(0);
}); 