function changeTabByIndex (index) {
    var $tab = $(window.parent.document).find('.ui_tab ul li');
    $tab.each(function () {
        $(this).removeClass('active first');
    });
    $tab.eq(index).addClass('active first');
}