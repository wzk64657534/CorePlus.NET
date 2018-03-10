$.datepicker.setDefaults($.datepicker.regional['zh-CN']);

var CoreDate = {
    Create: function () {
        return new Date();
    },
    AddDays: function (days) {
        var nd = new Date();
        nd = nd.valueOf();
        nd = nd + (days * 24 * 60 * 60 * 1000);
        nd = new Date(nd);
        var y = nd.getFullYear();
        var m = nd.getMonth() + 1;
        var d = nd.getDate();
        if (m <= 9) m = "0" + m;
        if (d <= 9) d = "0" + d;
        var cdate = y + "/" + m + "/" + d;
        return cdate;
    },
    AddDaysByDate: function (date, days) {
        var nd = new Date(date);
        nd = nd.valueOf();
        nd = nd + (days * 24 * 60 * 60 * 1000);
        nd = new Date(nd);
        var y = nd.getFullYear();
        var m = nd.getMonth() + 1;
        var d = nd.getDate();
        if (m <= 9) m = "0" + m;
        if (d <= 9) d = "0" + d;
        var cdate = y + "/" + m + "/" + d;
        return cdate;
    },
    DateDiff: function (d1, d2) {
        var day = 24 * 60 * 60 * 1000;
        try {
            var dateArr = d1.split("-");
            var checkDate = new Date();
            checkDate.setFullYear(dateArr[0], dateArr[1] - 1, dateArr[2]);
            var checkTime = checkDate.getTime();

            var dateArr2 = d2.split("-");
            var checkDate2 = new Date();
            checkDate2.setFullYear(dateArr2[0], dateArr2[1] - 1, dateArr2[2]);
            var checkTime2 = checkDate2.getTime();

            var cha = (checkTime - checkTime2) / day;
            return cha;
        } catch (e) {
            return null;
        }
    },
    GetDate: function (date) {
        var d = date ? new Date(date) : new Date();
        var year = d.getFullYear();
        var month = d.getMonth() + 1;
        var day = d.getDate();

        return year + '/' + month + '/' + day;
    },
    GetTime: function (date) {
        var d = date ? new Date(date) : new Date();
        var year = d.getFullYear();
        var month = d.getMonth() + 1;
        var day = d.getDate();
        var hour = d.getHours();
        var minute = d.getMinutes();
        var second = d.getSeconds();

        return year + '/' + month + '/' + day + ' ' + hour + ':' + minute + ':' + second;
    },
    GetDaysInMonth: function (y, m) {
        m = parseInt(m);
        var temp = new Date(y, m, 0);
        return temp.getDate();
    }
};