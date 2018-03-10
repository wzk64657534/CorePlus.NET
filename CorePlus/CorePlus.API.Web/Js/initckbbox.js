function intiCkbDiv(id, obj,width,height) {
    
    $(obj).after('<td><a href="javascript:void(0);" class="easyui-linkbutton 1-btn 1-btn-plain" iconcls="icon-add" plain="true" id="btnShow">添加</a></td>');
    $('#btnShow').click(function(parameters) {
        $("<div id=\"ckbDiv\"></div>").appendTo($('body'));
        //console.log($('#ckbDiv').children().length);
        $.getJSON("/Account/GetCkbData", { 'id': id }, function (data) {
            if (data) {
                for (var i = 0; i < data.length; i++) {
                    //                        console.log(data[i].ParamValue);
                    //                        console.log(data[i].ParamDtsName);
                    if (i % 4 == 0) {
                        ($('#ckbDiv')).append("</br>");
                    }
                    ($('#ckbDiv')).append($("<input type=\"checkbox\" name=\"name\" value=\"" + data[i].ParamDtsName + "\"   id=\"" + data[i].ParamValue + "\"/> "));
                    $("<label for=\"" + data[i].ParamValue + "\">" + data[i].ParamDtsName + "</label>").appendTo($('#ckbDiv'));
                }

                var chk_value = $(obj).val();
                if (chk_value) {
                    chk_value = chk_value.split(',');
                    for (var j = 0; j < chk_value.length; j++) {
                        $('input[value=\"' + chk_value[j] + '\"]:checkbox').attr('checked', 'true');
                    }
                }
            }

            $("#ckbDiv").dialog({
                width: width,
                height: height,
                modal: true,

                title: "选择地区",
                collapsible: true,
                minimizable: true,
                maximizable: true,
                resizable: true,
                closable: false,
                buttons: [{
                    id: 'btnEditAddCkb',
                    text: '确定',
                    iconCls: 'icon-add',
                    handler: function () {
                        chk_value = [];
                        $('input[name="name"]:checked').each(function () {
                            chk_value.push($(this).val());
                        });

                        $(obj).val("").val(chk_value);
                        $("#ckbDiv").dialog("close");
                        $("#ckbDiv").remove();
                    }
                }, {
                    id: 'btnEditCancle',
                    text: '取消',
                    iconCls: 'icon-cancel',
                    handler: function () {
                        $("#ckbDiv").dialog("close");
                        $("#ckbDiv").remove();

                    }
                }]
            });
        });

    });
}