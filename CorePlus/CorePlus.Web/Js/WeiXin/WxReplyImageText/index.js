$(function () {
    $("#addDiv").css("display", "none");
    $("#editDiv").css("display", "none");
    initTb();
});

var matchTypeList = null;
InitListMatchType();

function InitListMatchType() {
    $.getJSON('/WxReplyImageText/GetMatchTypeList', function (data) {
        matchTypeList = data;
    });
}

function GetTextFromList(list, v) {
    if (list) {
        for (var i = 0; i < list.length; i++) {
            var item = list[i];
            if (item.Value == v) {
                v = item.Text;
                break;
            }
        }
    }
    return v;
}

//初始化表格
function initTb(queryParam) {
    $('#dg').datagrid({
        //页面加载完成之后，表格会自动往后台发送一个请求，请求中会自动带上：  page【请求的当前页】，rows【请求的行数】。返回的数据格式：{total:40,rows:[{},{}]}
        url: '/WxReplyImageText/GetDataByPager',
        width: function () { return document.body.clientWidth * 0.9 },
        height: 470,
        fitColumns: true, //列自适应。
        remoteSort: false,
        nowrap: true, // false时，当字数过多是换行
        collapsible: true,
        loadMsg: "数据加载中，请稍后...",
        idField: 'ID',
        pagination: true, //允许分页。
        singleSelect: false, //单行选择。
        pageSize: 15, //默认的一页大小，这个值会随着第一次请求的时候做完rows的参数发送到后台。
        pageNumber: 1, //默认的当前页
        queryParams: queryParam, //请求的额外的参数。
        pageList: [15], //一页多少条数据的选择项。
        columns: [[
        { field: 'ck', checkbox: true, align: 'center' },
        { field: 'ID', title: '编号', sortable: true },
        { field: 'Keyword', title: '关键词', sortable: true },
        {
            field: 'MatchType',
            title: '关键词类型',
            formatter: function (val, data, index) {
                val = GetTextFromList(matchTypeList, val);
                return val;
            }
        },
        { field: 'Title', title: '标题' },
        { field: 'PicUrl', title: '图片URL' },
        { field: 'Url', title: '跳转URL' },
        { field: 'Description', title: '描述' },
        { field: 'WithIds', title: '多图文' }
    ]],
        toolbar: [{
            id: 'btnAdd',
            text: '添加',
            iconCls: 'icon-add',
            handler: addEvent
        }, {
            id: 'btnDelete',
            text: '删除',
            iconCls: 'icon-remove',
            handler: deleteEvent
        }, {
            id: 'btnEdit',
            text: '修改',
            iconCls: 'icon-edit',
            handler: editEvent
        }]
    });
}

//添加被点击的时候执行的代码
function addEvent() {
    //添加的表单出来。
    $("#frameAdd").attr("src", "/WxReplyImageText/Add/");

    $("#addDiv").css("display", "block");

    $("#addDiv").dialog({
        width: 700,
        height: 500,
        top: 15,
        modal: true,
        title: "新增",
        collapsible: true,
        minimizable: true,
        maximizable: true,
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

//添加成功之后执行的，事件响应方法，方法与方法之间用空行隔开。
function afterAdd() {
    $("#addDiv").dialog("close");
    $('#dg').datagrid("reload"); //代用表格的重新加载的方法。
}

//删除的事件响应方法
function deleteEvent() {
    //把表格中选中的行的id都弹出来的。
    //调用方法：getSelections 获取所有的选中的行
    var allSelectedRows = $('#dg').datagrid("getSelections");
    if (allSelectedRows.length <= 0) {
        $.messager.alert("提醒", "请选中要删除行！");
        return;
    }

    //先让用户确认是否要删除。
    $.messager.confirm("Messager：", "您确定要删除吗？", function (r) {
        if (r) {
            //发送异步请求道后台，删除数据
            //1,5,8  
            var strIds = "";
            for (var i in allSelectedRows) {
                strIds += allSelectedRows[i].ID + ",";
            }
            strIds = strIds.substr(0, strIds.lastIndexOf(','));

            postDeleteData(strIds);
        }
    });
}

//修改的事件响应方法
function editEvent() {
    //弹出对话框之前先给iframe标签的src属性设置值
    //拿到用户选择的要修改的数据
    var allSelectedRows = $('#dg').datagrid("getSelections");
    if (allSelectedRows.length != 1) {
        $.messager.alert("提醒", "请选中一条数据进行修改");
        return;
    }
    var id = allSelectedRows[0].ID;
    $("#frameEdit").attr("src", "/WxReplyImageText/Edit/" + id);
    //弹出修改的对话框。
    $("#editDiv").css("display", "block");

    $("#editDiv").dialog({
        width: 700,
        height: 500,
        top: 15,
        modal: true,
        title: "修改",
        collapsible: true,
        minimizable: true,
        maximizable: true,
        resizable: true,
        buttons: [{
            id: 'btnAddSbu',
            text: '修改',
            iconCls: 'icon-add',
            handler: function () {
                //让子容器的表单提交
                $("#frameEdit")[0].contentWindow.submitFrm();
            }
        }, {
            id: 'btnCancelAdd',
            text: '取消',
            iconCls: 'icon-cancel',
            handler: function () {
                $("#editDiv").dialog("close");
            }
        }]
    });
}

//当子容器修改成功之后调用此方法，关闭对话框，刷新表格
function afterEdit() {
    $("#editDiv").dialog("close");
    $('#dg').datagrid("reload"); //代用表格的重新加载的方法。
}

//处理删除的表单提交
function postDeleteData(ids) {
    $.post("/WxReplyImageText/RemoveServant", { Ids: ids }, function (data) {
        if (data == "ok") {
            //清除之前的选中的行。
            $('#dg').datagrid("clearSelections");
            //刷新表格
            $('#dg').datagrid("reload"); //代用表格的重新加载的方法。
        } else {
            $.messager.alert("错误提醒", data);
        }
    });
}