var Visit = {
    SetTimePeriod: function (period) {
        this.StartDate = CoreDate.AddDays(period).replace(/\//g, '-');
        this.EndDate = period == -1 ? CoreDate.AddDays(period).replace(/\//g, '-') : CoreDate.GetDate().replace(/\//g, '-');
        var p = '(' + this.StartDate + '至' + this.EndDate + ')';
        $('#timePeriod').html(p);
    },
    SetTimePeriodOneDay: function (period) {
        this.StartDate = CoreDate.AddDays(period).replace(/\//g, '-');
        this.EndDate = CoreDate.AddDays(period).replace(/\//g, '-');
        var p = '(' + this.StartDate + '至' + this.EndDate + ')';
        $('#timePeriod').html(p);
    },
    SetSelected: function (tabs, cls, v) {
        $(tabs).removeClass(cls);
        $(tabs).each(function () {
            var item = $(this).find('a:eq(0)');
            var v2 = item.attr('info');
            if (v2 == v) {
                $(this).addClass(cls);
            }
        });
    }
};

