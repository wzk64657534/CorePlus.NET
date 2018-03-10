$(function () {

    SetBanner(0);


    $('#time_tool_quick_key li').css('cursor', 'pointer');
    $('#time_tool_quick_key li').click(function () {
        var item = $(this).find('a').eq(0);
        var v = item.attr('info');
        SetBanner(v);
    });


    $("#sel").change(function () {
        var type = $("#sel").get(0).selectedIndex;
        //        var v = $('#time_tool_quick_key li').find('a').eq(0).attr('info');
        //        Visit.SetSelected('#time_tool_quick_key li', 'selected', v);
        var startDate = Visit.StartDate;
        var endDate = Visit.EndDate;
        GetChartsData(startDate, endDate, type);
    });


});




function SetBanner(v) {
    Visit.SetTimePeriod(v);
    Visit.SetSelected('#time_tool_quick_key li', 'selected', v);
    var startDate = Visit.StartDate;
    var endDate = Visit.EndDate;

    // banner
    $.ajax({
        url: '/TerminalDetail/Banner',
        data: { start: startDate, end: endDate },
        dataType: 'json',
        success: function (response) {
            $('#banner_vc').html(response.VISITCOUNT);
            $('#banner_uv').html(response.UV);
            $('#banner_ip').html(response.IP);
            $('#banner_newuv').html(response.NEWUV);
            $('#banner_pv').html(response.PV);
            $('#banner_percent').html(String(response.PERCENT) + '%');
        },
        error: function (xhr, msg, exp) {
            $('#banner_vc').html('-1');
            $('#banner_pv').html('-1');
            $('#banner_uv').html('-1');
            $('#banner_ip').html('-1');
            $('#banner_newuv').html('-1');
            $('#banner_percent').html('-1');
        }
    });
    var type = $("#sel").get(0).selectedIndex; //获取Select选择的索引值
    console.log(type);
    GetChartsData(startDate, endDate, type);
}


function GetChartsData(startDate, endDate, type) {
    $.ajax({
        url: '/TerminalDetail/GetDataOfChartForXY',
        data: { start: startDate, end: endDate, sourceType: type },
        dataType: 'json',
        success: function (response) {
            // chart
            var serie = $("#sel").find("option:selected").text();

            var names = Array();
            var data = Array();
            for (var i = 0; i < response.length; i++) {
                var name = response[i].name;
                if (type == 3) {
                    name = GetVersion(response[i].name);
                    console.log(name);
                    names.push(name);
                }
                names.push(name);

                data.push(response[i].data[0]);
            }


            $('#divChart').highcharts({
                chart: {
                    type: 'bar'
                },
                title: {
                    text: '终端详情'
                },
                subtitle: {
                    text: ''
                },
                xAxis: {
                    categories: names,
                    title: {
                        text: null
                    }
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: '次',
                        align: 'high'
                    },
                    labels: {
                        overflow: 'justify'
                    }
                },
                tooltip: {
                    valueSuffix: ' 次'
                },
                plotOptions: {
                    bar: {
                        dataLabels: {
                            enabled: true
                        }
                    }
                },
                legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'top',
                    x: -40,
                    y: 100,
                    floating: true,
                    borderWidth: 1,
                    backgroundColor: '#FFFFFF',
                    shadow: true
                },
                credits: {
                    enabled: false
                },
                series: [{
                    name: serie,
                    data: data
                }]
            });
        }
    });
}

function GetVersion(value) {
    switch (value) {
        case "en-us": return "英语(美国)";
        case "zh-cn": return "简体中文(中国)";
        case "zh-tw": return "繁体中文(台湾地区)";
        case "en-hk": return "英语(香港)";
        case "ru": return "俄语";
        case "es": return "西班牙语";
        case "c": return "未知";
        default: return "";
    }
}


        