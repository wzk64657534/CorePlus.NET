$(function () {
    $("#jq_topmenu li").hover(function () { $(this).addClass("hover").find("div.jq_hidebox").show(); }, function () { $(this).removeClass("hover").find("div.jq_hidebox").hide(); });
});