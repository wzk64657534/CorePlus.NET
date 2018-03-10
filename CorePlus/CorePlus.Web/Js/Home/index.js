$(function () {
    $("#divUpload").css("display", "none");
    $('#btnUpload').click(function () {
        UploadEvent();
    });

    $('#imgSmall').click(function () {
        window.open($(this).attr("src"));
    });

    $('#PicUrl').click(function () {
        var src = $(this).val();
        if (src != "") {
            GetSmallImage(src);
        }
    }).blur(function () {
        var src = $(this).val();
        if (src != "") {
            GetSmallImage(src);
        }
    });
});

function GetSmallImage(data) {
    $('#imgSmall').attr("src", data).attr("alt", "").attr("style", "width:120px;height:60px;cursor:pointer;");
}

//添加被点击的时候执行的代码
function UploadEvent() {
    //添加的表单出来。
    $("#frmUpload").attr("src", "/Home/Upload/");

    $("#divUpload").css("display", "block");

    $("#divUpload").dialog({
        width: 400,
        height: 200,
        top: 15,
        modal: true,
        title: "上传文件",
        buttons: [{
            id: 'btnSave',
            text: '上传',
            iconCls: 'icon-save',
            handler: function () {
                $("#frmUpload")[0].contentWindow.submitFrm();
            }
        }, {
            id: 'btnCancel',
            text: '取消',
            iconCls: 'icon-cancel',
            handler: function () {
                $("#divUpload").dialog("close");
            }
        }]
    });
}

//添加成功之后执行的，事件响应方法，方法与方法之间用空行隔开。
function afterUpload(data) {
    $("#divUpload").dialog("close");
    $('#PicUrl').val(data);
    GetSmallImage(data);
}