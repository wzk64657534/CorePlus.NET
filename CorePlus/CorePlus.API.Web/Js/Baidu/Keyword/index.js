﻿$(function () {
    var id = $('#SelectedId').val();
    var folderId = $('#FolderId').val();

    $('#creativeA').hide();
    $('#sublinkA').hide();
    if (id != "") {
        $('#creativeA').show().attr('href', '/Creative/Index/' + id).click(function () {
            changeTabByIndex(3);
        });
        $('#sublinkA').show().attr('href', '/Sublink/Index/' + id).click(function () {
            changeTabByIndex(4);
        });
    }

    $('#tbAdvanced').hide();
    $('#spanAdvanced').css('color', 'blue').click(function () {
        $(this).text($(this).text() == '[+]高级' ? '[-]高级' : '[+]高级');
        $('input[type="radio"]').removeAttr('checked');
        $('#tbAdvanced').fadeToggle('500');
    });

    $('#btnSearch').click(function () {
        var params = {
            highId: id,
            folderId: folderId,
            datepick: $('#ddlDatePick').combobox('getValue'),
            name: $('#txbName').val(),
            state: $('#ddlStatus').combobox('getValue'),
            isExact: $('#cbxIsExact').attr('checked') == "checked" ? true : false,
            cost: $('input:radio[name="Cost"]:checked').val(),
            price: $('input:radio[name="Price"]:checked').val(),
            clicked: $('input:radio[name="Clicked"]:checked').val()
        };

        initTb(params);
    });

    initTb({ highId: id, folderId: folderId });
});
//初始化表格
function initTb(queryParam) {
    $('#dg').datagrid({
        //页面加载完成之后，表格会自动往后台发送一个请求，请求中会自动带上：  page【请求的当前页】，rows【请求的行数】。返回的数据格式：{total:40,rows:[{},{}]}
        url: '/Keyword/GetDataByPager',
        width: function () { return document.body.clientWidth * 0.9 },
        height: 310,
        fitColumns: true, //列自适应。
        remoteSort: false,
        //striped: true, // 隔行显示颜色
        //nowrap: true, // false时，当字数过多是换行
        //collapsible: true,
        idField: 'ID', //主键列
        // loadMsg: '加载中...', //当请求后台数据的时候，前台页面上显示的内容。
        pagination: true, //允许分页。
        singleSelect: true, //单行选择。
        pageSize: 10, //默认的一页大小，这个值会随着第一次请求的时候做完rows的参数发送到后台。
        pageNumber: 1, //默认的当前页
        pageList: [10, 20], //一页多少条数据的选择项。
        queryParams: queryParam, //请求的额外的参数。
        frozenColumns: [[
			{ field: 'Keyword', title: '关键词' }
		]],
        columns: [[
            { field: 'AdgroupName', title: '推广单元' },
            { field: 'CampaignName', title: '推广计划' },
            { field: 'Status', title: '状态' },
            { field: 'Price', title: '出价', sortable: true },
            { field: 'MatchType', title: '匹配模式' },
            { field: 'Quality', title: '质量度' },
            { field: 'DestinationUrl', title: '目标URL' },
            { field: 'AvgClickedPrice', title: '平均点击价格', sortable: true },
            { field: 'Clicked', title: '点击量', sortable: true },
            { field: 'ClickedRate', title: '点击率', sortable: true },
            { field: 'ShowCnt', title: '展现量', sortable: true },
            { field: 'ThousandCost', title: '千次展现消费', sortable: true },
            { field: 'TotalCost', title: '消费', sortable: true },
            { field: 'TransformCnt', title: '转化', sortable: true }
        ]],
        onLoadSuccess: function (data) {
            //表格加载完成之后，计算汇总值
            var recordCount = data.total;
            $('#sRecordCount').html(recordCount);
            var rows = data.rows;
            var showCnt = 0;
            var clicked = 0;
            var totalCost = 0.00;
            var transformCnt = 0;
            var clickedRate = 0.00
            var avgClickedPrice = 0.00;
            $.each(rows, function (index, row) {
                showCnt += row.ShowCnt;
                clicked += row.Clicked;
                totalCost += row.TotalCost;
                transformCnt += row.TransformCnt;
                clickedRate += row.ClickedRate;
                avgClickedPrice += row.AvgClickedPrice;
            });
            $('#sShowCnt').text(showCnt);
            $('#sClicked').text(clicked);
            $('#sTotalCost').text("￥" + totalCost.toFixed(2));
            $('#sTransformCnt').text(transformCnt);
            $('#sClickedRate').text(clickedRate.toFixed(2) + "%");
            $('#sAvgClickedPrice').text("￥" + avgClickedPrice.toFixed(2));
        },
        toolbar: [{
            id: 'btnAdd',
            text: '添加',
            iconCls: 'icon-add',
            handler: addEvent
        }]
    });
}

//添加被点击的时候执行的代码
function addEvent() {
    //添加的表单出来。
    $("#frameAdd").attr("src", "/Keyword/Create/");

    $("#addDiv").css("display", "block");

    $("#addDiv").dialog({
        width: 700,
        height: 450,
        top: 15,
        modal: true,
        title: "新增",
        resizable: true,
        buttons: [{
            id: 'btnAddSbu',
            text: '添加',
            iconCls: 'icon-add',
            handler: function () {
                $("#frameAdd")[0].contentWindow.submitFrm();
            }
        }, {
            id: 'btnCancelAdd',
            text: '取消',
            iconCls: 'icon-cancel',
            handler: function () {
                $("#addDiv").dialog("close");
            }
        }]
    });
}

//添加成功之后执行的 事件响应方法    方法与方法之间用空行隔开。
function afterAdd() {
    $("#addDiv").dialog("close");
    $('#dg').datagrid("reload"); //代用表格的重新加载的方法。
}