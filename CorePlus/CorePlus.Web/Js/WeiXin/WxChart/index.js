$(function () {
      // 初始化开始
      $('#ddlMonth').change(function () {
            var m = $(this).val();
            var t = $('#ddlType').val();
            LoadData(t, currentYear, m);
      });

      $('#ddlType').change(function () {
            var t = $(this).val();
            var m = $('#ddlMonth').val();
            LoadData(t, currentYear, m);
      });

      var currentDate = CoreDate.Create();
      var currentYear = currentDate.getFullYear();
      var currentMonth = currentDate.getMonth() + 1;
      var type = $('#ddlType').val();

      $('#ddlMonth').val(currentMonth);
      LoadData(type, currentYear, currentMonth);
      // 初始化结束
      var startDate, endDate;

      function LoadData(t, y, m) {
            var cnt = CoreDate.GetDaysInMonth(y, m);

            startDate = y + '-' + m + '-1';
            endDate = y + '-' + m + '-' + String(cnt);

            var itemX = [];
            if (cnt > 0) {
                  for (var i = 1; i <= cnt; i++) {
                        //var d = CoreDate.AddDaysByDate(startDate, i);
                        itemX.push(i + '日');
                  }
            }

            switch (t) {
                  case "message":
                        GetChartMessage(startDate, endDate, itemX);
                        break;
                  case "subscribe":
                        GetChartSubscribe(startDate, endDate, itemX);
                        break;
            }
      }

      function GetChartMessage(startDate, endDate, itemX) {
            $.ajax({
                  url: '/WxChart/GetChartMessage',
                  data: { start: startDate, end: endDate },
                  dataType: 'json',
                  success: function (response) {
                        // chart
                        var datas = response;
                        // create a chart
                        $('#divChart').highcharts({
                              title: {
                                    text: '',
                                    x: -20
                              },
                              subtitle: {
                                    text: '',
                                    x: -20
                              },
                              xAxis: {
                                    //tickInterval: 2,
                                    categories: itemX
                              },
                              yAxis: {
                                    title: {
                                          text: ''
                                    },
                                    min: 0,
                                    plotLines: [{
                                          value: 0,
                                          width: 1,
                                          color: '#808080'
                                    }]
                              },
                              tooltip: {
                                    valueSuffix: '',
                                    crosshairs: true,
                                    shared: true
                              },
                              legend: {
                                    layout: 'vertical',
                                    align: 'right',
                                    verticalAlign: 'middle',
                                    borderWidth: 0
                              },
                              series: datas
                        });
                  },
                  error: function (xhr, msg, ex) {
                        $('#divChart').html('error:' + msg);
                  }
            });
      }

      function GetChartSubscribe(startDate, endDate, itemX) {
            $.ajax({
                  url: '/WxChart/GetChartSubscribe',
                  data: { start: startDate, end: endDate },
                  dataType: 'json',
                  success: function (response) {
                        // chart
                        var datas = response;
                        // create a chart
                        $('#divChart').highcharts({
                              title: {
                                    text: '',
                                    x: -20
                              },
                              subtitle: {
                                    text: '',
                                    x: -20
                              },
                              xAxis: {
                                    //tickInterval: 2,
                                    categories: itemX
                              },
                              yAxis: {
                                    title: {
                                          text: ''
                                    },
                                    min: 0,
                                    plotLines: [{
                                          value: 0,
                                          width: 1,
                                          color: '#808080'
                                    }]
                              },
                              tooltip: {
                                    valueSuffix: '',
                                    crosshairs: true,
                                    shared: true
                              },
                              legend: {
                                    layout: 'vertical',
                                    align: 'right',
                                    verticalAlign: 'middle',
                                    borderWidth: 0
                              },
                              series: datas
                        });
                  },
                  error: function (xhr, msg, ex) {
                        $('#divChart').html('error:' + msg);
                  }
            });
      }
});