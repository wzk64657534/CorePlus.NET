$(function () {
$("#addDiv").css("display", "none");
$("#editDiv").css("display", "none");
    intiCkbDiv(1, '#RegionTarget',270,260);
    initTb();
});
//初始化表格
function initTb(queryParam) {
    $('#dg').datagrid({
        //页面加载完成之后，表格会自动往后台发送一个请求，请求中会自动带上：  page【请求的当前页】，rows【请求的行数】。返回的数据格式：{total:40,rows:[{},{}]}
        url: '/Account/GetDataByPager',
        width: function () { return document.body.clientWidth * 0.9 },
        height: 470,
        fitColumns: true, //列自适应。
        remoteSort: false,
        nowrap: true, // false时，当字数过多是换行
        collapsible: true,
        idField: 'AccountId',
        // loadMsg: '加载中...', //当请求后台数据的时候，前台页面上显示的内容。
        pagination: true, //允许分页。
        singleSelect: false, //单行选择。
        pageSize: 15, //默认的一页大小，这个值会随着第一次请求的时候做完rows的参数发送到后台。
        pageNumber: 1, //默认的当前页
        pageList: [15, 20], //一页多少条数据的选择项。
        queryParams: queryParam, //请求的额外的参数。

        columns: [[
            { field: 'ck', checkbox: true, align: 'left' },
            { field: 'AccountId', title: '账户编号', sortable: true ,align: 'center',width:50 },
            { field: 'AccountName', title: '账户名称' ,align: 'center',width:50 },
            { field: 'AccountChnName', title: '中文名称' ,align: 'center',width:50},
            { field: 'LastUpdateTime', title: '更新时间',align: 'center' ,
              formatter: function (value, row, index) {//  /Date(1377327062000)/
						        return (eval(value.replace(/\/Date\((\d+)\)\//gi, "new Date($1)")))
						            .pattern("yyyy-M-d H:m:s");
						    }
            },
            { field: 'Balance', title: '账户余额',width:50 },
            { field: 'Cost', title: '账户累计消费',width:50 },
            { field: 'Payment', title: '账户投资',width:50 },
            { field: 'Budget', title: '账户预算', sortable: true ,width:50},
            { field: 'Type', title: '账户财务数据类型', sortable: true ,width:50,
            formatter: function(value, row, index) {
                if (value=="1") {
                    return "凤巢";
                } else if (value=="2") {
                    return "网盟";
                }
            }
            },
            { field: 'RegionTarget', 
                title: '推广地域列表' ,width:50,
            formatter: function(value,rowdata,rowindex) {
//                if (value.length>4) {
//                    var result = value.split(',');
//                    return result[0] + '...';
//                }
//                return value;
                return '<span title=" '+ value +' ">'+value+'</span>';
                
            }
            },
            //{ field: 'ExcludeIp', title: 'ip排除列表',width:100 },
            { field: 'OpenDomains', title: '开发域名列表',width:50 }
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
                },{
                    id: 'btnEdit',
                    text: '修改',
                    iconCls: 'icon-edit',
                    handler: editEvent
                }],
    });
}

//添加被点击的时候执行的代码
        function addEvent() {
            //添加的表单出来。
            //initbindvalidateform();
            $("#addDiv").css("display", "block");
            
            $("#addDiv").dialog({
                width: 400,
                height: 500,
                modal: true,
                title: "注册用户",
                collapsible: true,
                minimizable: true,
                maximizable: true,
                resizable: true,
                buttons: [{
                    id: 'btnAddSbu',
                    text: '添加',
                    iconCls: 'icon-add',
                    handler: function () {
                        //定位到表单然后  submit
                        $("#addDiv form").submit();
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
              $('#btnShow').click(function() {
                GetCkbDiv('#RegionTarget');
            });
           
        }

        //添加成功之后执行的 事件响应方法    方法与方法之间用空行隔开。
        function afterAdded(data) {
            if (data == "ok") {
                //关闭对话框，刷新表格
                $("#addDiv").dialog("close");
                $('#dg').datagrid("reload");//代用表格的重新加载的方法。
            } else {
                //alert(data);
                $.messager.alert("错误信息", "添加败了！");
            }
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
        

          //处理删除的表单提交
        function postDeleteData(ids) {
            $.post("/Account/Delete", { Ids: ids }, function (data) {
                if (data == "ok") {
                    //清除之前的选中的行。
                    $('#dg').datagrid("clearSelections");
                    //刷新表格
                    $('#dg').datagrid("reload");//代用表格的重新加载的方法。
                } else {
                    $.messager.alert("错误提醒", data);
                }
            });
        }
        

         ///修改的事件响应方法
        function editEvent() {
            //弹出对话框之前先给iframe标签的src属性设置值
            //拿到用户选择的要修改的数据
            var allSelectedRows = $('#dg').datagrid("getSelections");
            if (allSelectedRows.length !=1) {
                $.messager.alert("提醒", "请选中一条数据进行修改");
                return;
            }
            var id = allSelectedRows[0].ID;
            $("#frameEdit").attr("src", "/Account/Edit/" + id);
            //弹出修改的对话框。
            $("#editDiv").css("display", "block");
             
            $("#editDiv").dialog({
                width: 620,
                height: 610,
                top:15,
                modal: true,
                title: "修改用户",
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
            $('#dg').datagrid("reload");//代用表格的重新加载的方法。
        }



        //获取ckbboxDiv
        function GetCkbDiv(obj) {
            $("<div id=\"ckbDiv\"></div>").appendTo($('body'));
            //console.log($('#ckbDiv').children().length);
            if ($('#ckbDiv').children().length=0) {
                
            }
            $.getJSON("/Account/GetCkbData", {'id':'1'}, function(data) {
                if (data) {
                    for (var i = 0; i < data.length; i++) {
//                        console.log(data[i].ParamValue);
//                        console.log(data[i].ParamDtsName);
                        if (i%4==0) {
                            ($('#ckbDiv')).append("</br>");
                        }
                        ($('#ckbDiv')).append($("<input type=\"checkbox\" name=\"name\" value=\"" + data[i].ParamDtsName + "\"   id=\"" + data[i].ParamValue + "\"/> "));
                    $("<label for=\""+data[i].ParamValue+"\">"+data[i].ParamDtsName+"</label>").appendTo($('#ckbDiv'));
                    }
                    
                    var chk_value = $(obj).val();
                    if (chk_value) {
                        chk_value = chk_value.split(',');
                    for (var j = 0; j < chk_value.length; j++) { 
                        $('input[value=\"'+chk_value[j]+'\"]:checkbox').attr('checked', 'true');
                    }
                    }
                }
                $("#ckbDiv").dialog({
                    width: 270,
                    height: 260,
                    modal: true,

                    title: "选择地区",
                    collapsible: true,
                    minimizable: true,
                    maximizable: true,
                    resizable: true,
                    closable: true,
                buttons: [{
                    id: 'btnAddCkb',
                    text: '确定',
                    iconCls: 'icon-add',
                    handler: function () {
                       chk_value    =[];    
                         $('input[name="name"]:checked').each(function(){    
                            chk_value.push($(this).val());    
                             });

                        $(obj).val("").val(chk_value);
                       $("#ckbDiv").dialog("close");
                        $("#ckbDiv").remove();
                    }
                }, {
                    id: 'btnCancelCkb',
                    text: '取消',
                    iconCls: 'icon-cancel',
                    handler: function () {
                        $("#ckbDiv").dialog("close");
                        $("#ckbDiv").remove();

                    }
                }]
            });
            });
        }

//        加载类型选项
//        function LoadTypeData(){
//			$('#selType').combobox({
//				url:'/Account/LoadTypeData',
//				valueField:'ParamValue',
//				textField:'ParamDtsName'
//			});
//            $('#selType').combobox('setValue','1');
//		}

        //初始化表单验证方法

       
       $(function () {
    
 $("#addDiv").css("display", "none");
    initTb();
});
//初始化表格
function initTb(queryParam) {
    $('#dg').datagrid({
        //页面加载完成之后，表格会自动往后台发送一个请求，请求中会自动带上：  page【请求的当前页】，rows【请求的行数】。返回的数据格式：{total:40,rows:[{},{}]}
        url: '/Account/GetDataByPager',
        width: function () { return document.body.clientWidth * 0.9 },
        height: 470,
        fitColumns: true, //列自适应。
        remoteSort: false,
        nowrap: true, // false时，当字数过多是换行
        collapsible: true,
        idField: 'AccountId',
        // loadMsg: '加载中...', //当请求后台数据的时候，前台页面上显示的内容。
        pagination: true, //允许分页。
        singleSelect: false, //单行选择。
        pageSize: 15, //默认的一页大小，这个值会随着第一次请求的时候做完rows的参数发送到后台。
        pageNumber: 1, //默认的当前页
        pageList: [15, 20], //一页多少条数据的选择项。
        queryParams: queryParam, //请求的额外的参数。

        columns: [[
            { field: 'ck', checkbox: true, align: 'left' },
            { field: 'AccountId', title: '账户编号', sortable: true ,align: 'center',width:50 },
            { field: 'AccountName', title: '账户名称' ,align: 'center',width:50 },
            { field: 'AccountChnName', title: '中文名称' ,align: 'center',width:50},
            { field: 'LastUpdateTime', title: '更新时间',align: 'center' ,
              formatter: function (value, row, index) {//  /Date(1377327062000)/
						        return (eval(value.replace(/\/Date\((\d+)\)\//gi, "new Date($1)")))
						            .pattern("yyyy-M-d H:m:s");
						    }
            },
            { field: 'Balance', title: '账户余额',width:50 },
            { field: 'Cost', title: '账户累计消费',width:50 },
            { field: 'Payment', title: '账户投资',width:50 },
            { field: 'Budget', title: '账户预算', sortable: true ,width:50},
            { field: 'Type', title: '账户财务数据类型', sortable: true ,width:50,
            formatter: function(value, row, index) {
                if (value=="1") {
                    return "凤巢";
                } else if (value=="2") {
                    return "网盟";
                }
            }
            },
            { field: 'RegionTarget', title: '推广地域列表' ,width:50},
            //{ field: 'ExcludeIp', title: 'ip排除列表',width:100 },
            { field: 'OpenDomains', title: '开发域名列表',width:50 }
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
                },{
                    id: 'btnEdit',
                    text: '修改',
                    iconCls: 'icon-edit',
                    handler: editEvent
                }],
    });
}

//添加被点击的时候执行的代码
        function addEvent() {
            //添加的表单出来。
            //initbindvalidateform();
            $("#addDiv").css("display", "block");
            
            $("#addDiv").dialog({
                width: 560,
                height: 550,
                top:15,
                modal: true,
                title: "添加用户",
                collapsible: true,
                minimizable: true,
                maximizable: true,
                resizable: true,
                buttons: [{
                    id: 'btnAddSbu',
                    text: '添加',
                    iconCls: 'icon-add',
                    handler: function () {
                        //定位到表单然后  submit
                        $("#addDiv form").submit();
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
        function afterAdded(data) {
            if (data == "ok") {
                //关闭对话框，刷新表格
                $("#addDiv").dialog("close");
                $('#dg').datagrid("reload");//代用表格的重新加载的方法。
            } else {
                //alert(data);
                $.messager.alert("错误信息", "添加败了！");
            }
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
        

          //处理删除的表单提交
        function postDeleteData(ids) {
            $.post("/Account/Delete", { Ids: ids }, function (data) {
                if (data == "ok") {
                    //清除之前的选中的行。
                    $('#dg').datagrid("clearSelections");
                    //刷新表格
                    $('#dg').datagrid("reload");//代用表格的重新加载的方法。
                } else {
                    $.messager.alert("错误提醒", data);
                }
            });
        }
        

         ///修改的事件响应方法
        function editEvent() {
            //弹出对话框之前先给iframe标签的src属性设置值
            //拿到用户选择的要修改的数据
            var allSelectedRows = $('#dg').datagrid("getSelections");
            if (allSelectedRows.length !=1) {
                $.messager.alert("提醒", "请选中一条数据进行修改");
                return;
            }
            var id = allSelectedRows[0].ID;
            $("#frameEdit").attr("src", "/Account/Edit/" + id);
            //弹出修改的对话框。
            $("#editDiv").css("display", "block");
             
            $("#editDiv").dialog({
                width: 620,
                height: 610,
                modal: true,
                title: "修改用户",
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
            $('#dg').datagrid("reload");//代用表格的重新加载的方法。
        }

