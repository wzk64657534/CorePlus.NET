$(function () {
    var pageCount = parseInt($('#srtPager').attr('PageCount'));
    var pageIndex = parseInt($('#srtPager').attr('PageIndex'));
    var recordCount = parseInt($('#srtPager').attr('RecordCount'));
    var url = $('#srtPager').attr('Href');

    if (recordCount > 0) {
        var pager = '<ul>';

        pager += '<li title="记录数">' + recordCount + '</li>'
        pager += '<li title="当前页/总页数"><b style="color:red">' + pageIndex + '</b>/' + pageCount + '</li>';

        if (pageIndex > 1) {
            pager += '<li title="首页"><a href="' + url + '/1" >|<< </a></li>';
            pager += '<li title="上一页"><a href="' + url + '/' + (pageIndex - 1) + '" ><<< </a></li>';
        }

        ////// Pages ///////
        var start = (pageCount >= 10 && pageIndex >= 10) ? (pageIndex - 5) : 1;
        var end = (pageCount > 1 && pageCount < 10) ? pageCount : ((pageIndex == pageCount) ? pageCount : (start + 10 - 1))
        for (var i = start; i <= end; i++) {
            pager += '<li><a href="' + url + '/' + i + '">' + i + '</a></li>';
        }
        ////// Pages ///////

        if (pageCount > 1 && pageIndex < pageCount) {
            pager += '<li title="下一页"><a href="' + url + '/' + (pageIndex + 1) + '" > >>></a></li>';
            pager += '<li title="末页"><a href="' + url + '/' + pageCount + '" > >>|</a></li>';
        }

        pager += '</ul>';

        $('#pager').html(pager).css('float', 'left');
        $('#pager ul').css('float', 'left').css('padding', '0');
        $('#pager ul li a').attr('style', 'color:dimGray;');
        $('#pager ul li').attr('style', ' list-style-type:none; padding-left:0px; padding-right:3px; padding-top:5px; padding-bottom:5px; float:left; width:30px; height:20px; border:1px solid #E8EEF4; text-align:center; cursor:pointer;').mouseover(function () {
            $(this).css('background-color', '#E8EEF4');
        }).mouseout(function () {
            $(this).css('background-color', '#FFFFFF');
        });

        var rcl = String(recordCount).length + 30;
        var pci = (String(pageIndex).length + String(pageCount).length) * 5 + 30;
        $('#pager ul li').eq(0).css('width', rcl + 'px');
        $('#pager ul li').eq(1).css('width', pci + 'px');
    }
});