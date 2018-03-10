$(function () {
    var startDate, endDate;
    var tabs = '#tabs_report li[class^="item"]';
    var data = CoreDate.GetDate();

    SetBanner(0);


    $("#tabs_report li:eq(4)").text(GetDay(-3)).next().text(GetDay(-4)).next().text(GetDay(-5)).next().text(GetDay(-6));

    // 执行默认设置
    $(tabs).removeClass('selected');
    $(tabs).addClass('link');
    $(tabs).eq(0).addClass('selected');
    $(tabs).eq(0).removeClass('link');

    $(tabs).css('cursor', 'pointer');
    $(tabs).click(function () {

        $(tabs).removeClass('selected');
        $(tabs).addClass('link');
        //        $(this).removeClass('link');
        $(this).addClass('selected');


        var v = $(this).attr('info');


        Visit.SetTimePeriodOneDay(v);
        Visit.SetSelected('#tabs_report li', 'item selected', v);
        startDate = Visit.StartDate;
        endDate = Visit.EndDate;

        Report(startDate, endDate, '');
    });

    function SetBanner(v) {
        Visit.SetTimePeriodOneDay(v);
        Report(startDate, endDate, '');
    }

    $('#btnSearch').click(function () {
        var t = $('#txbSearch').val();
        Report(startDate, endDate, t);
    });
});

function Report(s, e, t) {
    $('#dg').datagrid({
        //页面加载完成之后，表格会自动往后台发送一个请求，请求中会自动带上：  page【请求的当前页】，rows【请求的行数】。返回的数据格式：{total:40,rows:[{},{}]}
        url: '/RecentSearch/GetDataOfGrid',
        width: function () { return document.body.clientWidth * 0.9 },
        height: 310,
        fitColumns: true, //列自适应。
        remoteSort: false,
        idField: 'VisitTime', //主键列
        pagination: true, //允许分页。
        singleSelect: true, //单行选择。
        pageSize: 10, //默认的一页大小，这个值会随着第一次请求的时候做完rows的参数发送到后台。
        pageNumber: 1, //默认的当前页
        pageList: [10], //一页多少条数据的选择项。
        queryParams: { start: s, end: e, title: t }, //请求的额外的参数。
        columns: [[
                        { field: 'VisitTime', title: '浏览时间', width: 100, align: 'center' },
                        { field: 'ResourcePage', title: '页面来源', width: 100, align: 'center'},
                        { field: 'VisitingUrl', title: '受访', width: 300, align: 'center' },
                        { field: 'IP', title: 'IP', width: 100, align: 'center' }
                 ]]
    });
}


function GetDay(i) {
    var str = CoreDate.AddDays(i).replace(/\//g, '-');
    return str;
}
