var hostName = 'http://tongji.71.com/';
document.writeln('<img src=\"' + hostName + 'images/c.gif\" alt=\"泛亚统计\" border=\"0\" style=\"width:20px;height:20px;cursor:pointer;\" />');


var JSON = function () {
    var m = {
        '\b': '\\b',
        '\t': '\\t',
        '\n': '\\n',
        '\f': '\\f',
        '\r': '\\r',
        '"': '\\"',
        '\\': '\\\\'
    },
    s = {
        'boolean': function (x) {
            return String(x);
        },
        number: function (x) {
            return isFinite(x) ? String(x) : 'null';
        },
        string: function (x) {
            if (/["\\\x00-\x1f]/.test(x)) {
                x = x.replace(/([\x00-\x1f\\"])/g, function (a, b) {
                    var c = m[b];
                    if (c) {
                        return c;
                    }
                    c = b.charCodeAt();
                    return '\\u00' +
                        Math.floor(c / 16).toString(16) +
                        (c % 16).toString(16);
                });
            }
            return '"' + x + '"';
        },
        object: function (x) {
            if (x) {
                var a = [], b, f, i, l, v;
                if (x instanceof Array) {
                    a[0] = '[';
                    l = x.length;
                    for (i = 0; i < l; i += 1) {
                        v = x[i];
                        f = s[typeof v];
                        if (f) {
                            v = f(v);
                            if (typeof v == 'string') {
                                if (b) {
                                    a[a.length] = ',';
                                }
                                a[a.length] = v;
                                b = true;
                            }
                        }
                    }
                    a[a.length] = ']';
                } else if (x instanceof Object) {
                    a[0] = '{';
                    for (i in x) {
                        v = x[i];
                        f = s[typeof v];
                        if (f) {
                            v = f(v);
                            if (typeof v == 'string') {
                                if (b) {
                                    a[a.length] = ',';
                                }
                                a.push(s.string(i), ':', v);
                                b = true;
                            }
                        }
                    }
                    a[a.length] = '}';
                } else {
                    return;
                }
                return a.join('');
            }
            return 'null';
        }
    };

    return {
        copyright: '(c)2005 JSON.org',
        license: 'http://www.crockford.com/JSON/license.html',
        stringify: function (v) {
            var f = s[typeof v];
            if (f) {
                v = f(v);
                if (typeof v == 'string') {
                    return v;
                }
            }
            return null;
        },
        parse: function (text) {
            try {
                return !(/[^,:{}\[\]0-9.\-+Eaeflnr-u \n\r\t]/.test(
                        text.replace(/"(\\.|[^"\\])*"/g, ''))) &&
                    eval('(' + text + ')');
            } catch (e) {
                return false;
            }
        }
    };
} ();

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

var Site = {
    Track: function () {
        // 初始化实体
        var entity = this.NewEntity();
        this.Save(entity);
        //        var preVisitingPage = this.GetPreVisitingPage();
        //        var refSite = this.GetHost(document.referrer);
        //        var vstSite = this.GetHost(document.URL);
        //        var visitingUrl = this.GetUrlWithoutParams(document.URL);
        //        // 访问
        //        if (refSite != vstSite && preVisitingPage != visitingUrl) {
        //            // 初始化实体
        //            var entity = this.NewEntity();
        //            entity.VisitType = 1;
        //            entity.VisitPeriodTime = 0;
        //            this.Save(entity);
        //            // 记录着陆URL，通过这个URL产生的PV值，计算访问深度
        //            this.SetLoginPage(visitingUrl);
        //        }
        //        // 浏览定义：离开ref页，浏览当前页
        //        else if (refSite == vstSite && preVisitingPage != visitingUrl) {
        //            var entity = this.NewEntity();
        //            entity.VisitType = 2;
        //            entity.VisitPeriodTime = this.GetPeriodTime();
        //            this.Save(entity);
        //        }
        //        // 刷新定义：离开当前页，再次浏览当前页
        //        else if (preVisitingPage == visitingUrl) {
        //            var entity = this.NewEntity();
        //            entity.VisitType = 3;
        //            entity.VisitPeriodTime = this.GetPeriodTime();
        //            this.Save(entity);
        //        }
        //        // 定义：未知访问类型
        //        else {
        //            // 初始化实体
        //            var entity = this.NewEntity();
        //            // 记录浏览数据
        //            entity.VisitType = -99;
        //            entity.VisitPeriodTime = 0;
        //            this.Save(entity);
        //        }

        //        // 更新缓存
        //        this.UpdateCache(visitingUrl);
    },
    UpdateCache: function (url) {
        // 记录当前Url
        this.SetPreVisitingPage(url);
        // 记录当前时间
        this.SetStartTime();
    },
    NewEntity: function () {
        var entity = {
            // 会员用户编号
            UserId: this.GetUserId(),
            // 操作系统
            ConfigOS: this.SearchString(this.DataOS) || "",
            // 浏览器名称
            ConfigBrowserName: this.SearchString(this.DataBrowser) || "",
            // 浏览器版本
            ConfigBrowserVersion: this.SearchVersion(navigator.userAgent) || this.SearchVersion(navigator.appVersion) || "",
            // 浏览器语言
            ConfigBrowserLang: (navigator.browserLanguage || navigator.language).toLowerCase(),
            // 分辨率
            ConfigResolution: window.screen.width + "x" + window.screen.height,
            // 跳转类型(如：搜索引擎，自行输入等等)
            RefererType: this.GetSearchEngine(document.referrer) == "" ? 0 : 1,
            // 跳转名称(如：从百度跳转，为baidu;谷歌为google)
            RefererName: this.GetSearchEngine(document.referrer),
            // 跳转链接
            RefererUrl: document.referrer,
            // 跳转关键词
            RefererKeyword: this.GetKeyword(document.referrer),
            // 跳转的站点
            RefererSite: this.GetHost(document.referrer),
            // 跳转的页面，不带参数
            RefererPage: this.GetUrlWithoutParams(document.referrer),
            // 来访IP
            LocationIP: null,
            // 国家
            LocationCountry: null,
            // 地区
            LocationRegion: null,
            // 城市
            LocationCity: null,
            // 纬度
            LocationLatitude: null,
            // 经度
            LocationLongitude: null,
            // 来访时间
            VisitTime: DateTime.GetTime(),
            // 访问页面
            VisitingUrl: this.GetUrlWithoutParams(document.URL),
            // 停留时间(毫秒)
            VisitPeriodTime: this.GetPeriodTime(),
            // 访客Id
            VisitId: this.GetVisitId(),
            // 受访域名
            VisitingSite: this.GetHost(document.URL),
            // 起始页面
            LoginPage: this.GetLoginPage(),
            // 访问类型：1-访问，2-浏览,，3-刷新，4-离开
            VisitType: null
        };

        return entity;
    },
    Save: function (data) {
        // 没有UID不统计
        if (data.UserId > 0) {
            var strData = JSON.stringify(data);
            var strUrl = hostName + 'SetVisitData.ashx?callback=?';
            $.ajax({
                url: strUrl,
                type: "get",
                dataType: "jsonp",
                data: { params: strData },
                success: function (response) {
                    alert('Success');
                },
                error: function (response, msg, ex) {
                    alert('Failed');
                }
            });
        }
    },
    SetLoginPage: function (url) {
        Cookie.Set('LoginPage', url);
    },
    GetLoginPage: function () {
        return Cookie.Get('LoginPage');
    },
    SetPreVisitingPage: function (url) {
        Cookie.Set('PreVisitingPage', url == null ? document.URL : url);
    },
    GetPreVisitingPage: function () {
        return Cookie.Get('PreVisitingPage');
    },
    SetStartTime: function () {
        var st = DateTime.New().getTime();
        Cookie.Set('StartTime', st);
    },
    GetStartTime: function () {
        return Cookie.Get('StartTime');
    },
    GetPeriodTime: function () {
        var start = this.GetStartTime();
        var end = DateTime.New().getTime();
        var gap = (start == null) ? 0 : (end - start);
        return gap;
    },
    GetVisitId: function () {
        var vid = Cookie.Get('VisitId');
        if (vid == null) {
            vid = Guid.New();
            Cookie.Set('VisitId', vid);
        }
        return vid;
    },
    GetUserId: function () {
        var uid = $('#srtVisit').attr('uid');
        return uid ? uid : 0;
    },
    SearchString: function (data) {
        for (var i = 0; i < data.length; i++) {
            var dataString = data[i].string;
            var dataProp = data[i].prop;
            this.versionSearchString = data[i].versionSearch || data[i].identity;
            if (dataString) {
                if (dataString.indexOf(data[i].subString) != -1)
                    return data[i].identity;
            }
            else if (dataProp)
                return data[i].identity;
        }
    },
    SearchVersion: function (dataString) {
        var index = dataString.indexOf(this.versionSearchString);
        if (index == -1) return;
        return parseFloat(dataString.substring(index + this.versionSearchString.length + 1));
    },
    SearchOSVersion: function () {
        var sUserAgent = navigator.userAgent;
        var isWin = (navigator.platform == "Win32") || (navigator.platform == "Windows");
        var isMac = (navigator.platform == "Mac68K") || (navigator.platform == "MacPPC") || (navigator.platform == "Macintosh") || (navigator.platform == "MacIntel");
        if (isMac) return "Mac OS";
        var isUnix = (navigator.platform == "X11") && !isWin && !isMac;
        if (isUnix) return "Unix";
        var isLinux = (String(navigator.platform).indexOf("Linux") > -1);
        if (isLinux) return "Linux";
        if (isWin) {
            var isWin2K = sUserAgent.indexOf("Windows NT 5.0") > -1 || sUserAgent.indexOf("Windows 2000") > -1;
            if (isWin2K) return "Windows 2000";
            var isWinXP = sUserAgent.indexOf("Windows NT 5.1") > -1 || sUserAgent.indexOf("Windows XP") > -1;
            if (isWinXP) return "Windows XP";
            var isWin2003 = sUserAgent.indexOf("Windows NT 5.2") > -1 || sUserAgent.indexOf("Windows 2003") > -1;
            if (isWin2003) return "Windows 2003";
            var isWinVista = sUserAgent.indexOf("Windows NT 6.0") > -1 || sUserAgent.indexOf("Windows Vista") > -1;
            if (isWinVista) return "Windows Vista";
            var isWin7 = sUserAgent.indexOf("Windows NT 6.1") > -1 || sUserAgent.indexOf("Windows 7") > -1;
            if (isWin7) return "Windows 7";
            var isWin8 = sUserAgent.indexOf("Windows NT 6.3") > -1 || sUserAgent.indexOf("Windows 8") > -1;
            if (isWin8) return "Windows 8";
        }
        return "";
    },
    GetHost: function (url) {
        var host = "";
        if (typeof url == "undefined" || null == url) {
            url = window.location.href;
        }

        var regex = /.*\:\/\/([^\/]*).*/;
        var match = url.match(regex);
        if (typeof match != "undefined" && null != match) {
            host = match[1];
        }

        return host;
    },
    GetKeyword: function (url) {
        var keyword = null;
        var host = this.GetHost(url);
        var fields = host.split('.');
        if (fields.length > 1) {
            switch (fields[1]) {
                case "baidu":
                    keyword = this.GetQueryParamValue(url, "wd");
                    break;
                case "google":
                    keyword = this.GetQueryParamValue(url, "q");
                    break;
                case "so":
                    keyword = this.GetQueryParamValue(url, "q");
                    break;
                case "sogou":
                    keyword = this.GetQueryParamValue(url, "query");
                    break;
                case "yahoo":
                    keyword = this.GetQueryParamValue(url, "p");
                    break;
                case "soso":
                    keyword = this.GetQueryParamValue(url, "w");
                    break;
                case "yodao":
                    keyword = this.GetQueryParamValue(url, "q");
                    break;
            }
        }

        return keyword;
    },
    GetSearchEngine: function (url) {
        var host = this.GetHost(url);
        var fields = host.split('.');
        if (fields.length > 1) {
            switch (fields[1]) {
                case "baidu": return "baidu";
                case "google": return "google";
                case "so": return "360";
                case "sogou": return "sogou";
                case "yahoo": return "yahoo";
                case "soso": return "soso";
                case "yodao": return "yodao";
                default: return "";
            }
        }

        return "";
    },
    GetQueryParamValue: function (url, name) {
        var v = "";
        var query = url.split('?').length > 1 ? url.split('?')[1] : null;
        var fields = query.split('&');
        for (var i = 0; i < fields.length; i++) {
            var field = fields[i];
            if (field.indexOf(name) > -1) {
                v = field.split('=')[1];
            }
        }
        return v;
    },
    GetUrlWithoutParams: function (url) {
        var params = url.split('?')[0];
        return params;
    },
    DataBrowser: [
		    {
		        string: navigator.userAgent,
		        subString: "Chrome",
		        identity: "Chrome"
		    },
		    { string: navigator.userAgent,
		        subString: "OmniWeb",
		        versionSearch: "OmniWeb/",
		        identity: "OmniWeb"
		    },
		    {
		        string: navigator.vendor,
		        subString: "Apple",
		        identity: "Safari",
		        versionSearch: "Version"
		    },
		    {
		        prop: window.opera,
		        identity: "Opera",
		        versionSearch: "Version"
		    },
		    {
		        string: navigator.vendor,
		        subString: "iCab",
		        identity: "iCab"
		    },
		    {
		        string: navigator.vendor,
		        subString: "KDE",
		        identity: "Konqueror"
		    },
		    {
		        string: navigator.userAgent,
		        subString: "Firefox",
		        identity: "Firefox"
		    },
		    {
		        string: navigator.vendor,
		        subString: "Camino",
		        identity: "Camino"
		    },
		    {
		        string: navigator.userAgent,
		        subString: "Netscape",
		        identity: "Netscape"
		    },
		    {
		        string: navigator.userAgent,
		        subString: "MSIE",
		        identity: "Internet Explorer",
		        versionSearch: "MSIE"
		    },
		    {
		        string: navigator.userAgent,
		        subString: "Gecko",
		        identity: "Mozilla",
		        versionSearch: "rv"
		    },
		    {
		        string: navigator.userAgent,
		        subString: "Mozilla",
		        identity: "Netscape",
		        versionSearch: "Mozilla"
		    }
	    ],
    DataOS: [
		    {
		        string: navigator.platform,
		        subString: "Win",
		        identity: "Windows"
		    },
		    {
		        string: navigator.platform,
		        subString: "Mac",
		        identity: "Mac"
		    },
		    {
		        string: navigator.userAgent,
		        subString: "iPhone",
		        identity: "iPhone/iPod"
		    },
		    {
		        string: navigator.platform,
		        subString: "Linux",
		        identity: "Linux"
		    },
		    {
		        string: navigator.platform,
		        subString: "Unix",
		        identity: "Unix"
		    }
	    ]
};

// 统计
Site.Track();