var Cookie = {
    Get: function (name) {
        var cookies = document.cookie;
        var aryCookies = cookies.split(';');
        for (var i = 0; i < aryCookies.length; i++) {
            var arr = aryCookies[i].split('=');
            var key = arr[0].replace(/[ ]/g, '');
            if (name == key) {
                return arr[1];
            }
        }

        return null;
    },
    Set: function (n, v) {
        document.cookie = n + '=' + v;
    },
    Clear: function () {
        var cookies = document.cookie;
        var aryCookies = cookies.split(';');
        for (var i = 0; i < aryCookies.length; i++) {
            var arr = aryCookies[i].split('=');
            var key = arr[0].replace(/[ ]/g, '');
            var date = new Date();
            date.setTime(date.getTime() - 10000);
            document.cookie = key + "=none; expires=" + date.toGMTString();
        }
    }
};

var Guid = {
    New: function () {
        var guid = '';
        for (var i = 1; i <= 32; i++) {
            var n = Math.floor(Math.random() * 16.0).toString(16);
            guid += n;
            if ((i == 8) || (i == 12) || (i == 16) || (i == 20)) {
                guid += "-";
            }
        }

        return guid;
    }
};

var DateTime = {
    New: function () {
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
    DateDiffByPeriod: function (d1, d2, period) {
        period = period ? period : 1;
        try {
            var dateArr = d1.split("-");
            var checkDate = new Date();
            checkDate.setFullYear(dateArr[0], dateArr[1] - 1, dateArr[2]);
            var checkTime = checkDate.getTime();

            var dateArr2 = d2.split("-");
            var checkDate2 = new Date();
            checkDate2.setFullYear(dateArr2[0], dateArr2[1] - 1, dateArr2[2]);
            var checkTime2 = checkDate2.getTime();

            var cha = (checkTime - checkTime2) / period;
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
    }
};