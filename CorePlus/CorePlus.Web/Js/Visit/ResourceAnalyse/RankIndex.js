$(function () {
    $('#inputDate1').datepicker({ changeYear: true, changeMonth: true, dateFormat: 'yy-mm-dd' });
    $('#inputDate2').datepicker({ changeYear: true, changeMonth: true, dateFormat: 'yy-mm-dd' });

    var inputDate1 = $('#inputDate1');
    var inputDate2 = $('#inputDate2');

    $('#btnDate').click(function () {
        var date1 = inputDate1.val();
        var date2 = inputDate2.val();
        SetDate(date1, date2);
    });

    $('#btnSearch').click(function () {
        var t = $('#txbSearch').val();
        var date1 = inputDate1.val();
        var date2 = inputDate2.val();
        Report(date1, date2, t);
    });
    //////////// Functions ////////////////////////////
    function SetDate(date1, date2) {
        inputDate1.val(date1);
        inputDate2.val(date2);
        $('#timePeriod').html(' (' + date1 + ' 对比 ' + date2 + ')');
        $('#tdDate1').html(date1);
        $('#tdDate2').html(date2);

        Banner(date1, date2);

        Report(date1, date2, '');
    }

    function Banner(d1, d2) {
        $.ajax({
            url: '/ResourceRank/Banner',
            data: { start: d1, end: d2 },
            dataType: 'json',
            success: function (response) {
                $('#tdPv1').html(response.PV);
                $('#tdPv2').html(response.UV);
                $('#tdChanged').html(response.REMARK);
            },
            error: function (xhr, msg, exp) {
                $('#tdPv1').html('-1');
                $('#tdPv2').html('-1');
                $('#tdChanged').html('-1');
            }
        });
    }

    function Report(d1, d2, t) {
        var cols = [];

        cols.push({ field: 'RefererSite', title: '受访页面', width: 200, align: 'center', formatter: function (val) {
            if (val == "") {
                return "直接输入网址或书签";
            }
            return val;
        }
        });
        cols.push({ field: 'First', title: d1, width: 200, align: 'center' });
        cols.push({ field: 'Second', title: d2, width: 200, align: 'center' });
        cols.push({ field: 'Changed', title: '变化情况', width: 200, align: 'center' });

        $('#dg').datagrid({
            //页面加载完成之后，表格会自动往后台发送一个请求，请求中会自动带上：  page【请求的当前页】，rows【请求的行数】。返回的数据格式：{total:40,rows:[{},{}]}
            url: '/ResourceRank/GetDataOfGrid',
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
            queryParams: { start: d1, end: d2, title: t }, //请求的额外的参数。
            columns: [cols]
        });
    }

    ////////////////// Default //////////////////////////////
    SetDate(CoreDate.GetDate().replace(/\//g, '-'), CoreDate.AddDays(-1).replace(/\//g, '-'));
});