$(function () {
      $('#btnReset').val('重置验证').click(function () {
            $('#btnReset').val('操作中...');
            $.getJSON("/WxAccount/ResetWeiXinValidating", function (data) {
                  $('#btnReset').val('重置验证');
                  $('#result').html(data);
            });
      });
});