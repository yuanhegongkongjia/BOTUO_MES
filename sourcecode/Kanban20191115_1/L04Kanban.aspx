<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="L04Kanban.aspx.cs" Inherits="Kanban.L04Kanban" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>四号生产线工艺看板</title>
    <script type="text/javascript" src="Scripts/jquery-3.3.1.js"></script>
    <script type="text/javascript" src="bui/bui.js"></script>
    <link href="bui/css/dpl.css" rel="stylesheet" type="text/css" />
    <link href="bui/css/bui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="chart/highcharts.js?time=1"></script>
</head>
<body>
    <div class="page-header">
        <div class="logo">
            <img src="img/sumin_log.JPG" style="width:60px;height:60px;" />
            <span style="font-size:28px;color:darkblue;">四号线生产看板</span>
          <%--  <h1 style="/*margin-left:600px;*/">一号线生产看板</h1>--%>
        </div>
    </div>
    <div class="row" style="margin-top: 10px; margin-left: 20px;" id="row1">
        <div class="span24">
            <div id="container" style="min-width: 1000px; height: 400px;"></div>
        </div>

       
    </div>
    <div class="row" style="margin-top: 10px; margin-left: 20px;" id="row2">
         <div class="span24">
            <div id="container1" style="min-width: 1000px; height: 400px;"></div>
        </div>
    </div>
    <div class="row" style="margin-top: 10px; margin-left: 20px;" id="row3">
        <div class="span24">
            <div id="container2" style="min-width: 400px; height: 400px;"></div>
        </div>
    </div>
    <script>
         // 全局配置，针对页面上所有图表有效
         Highcharts.setOptions({
             chart: {
                 backgroundColor: {
                     linearGradient: [0, 0, 500, 500],
                     stops: [
                         [0, 'rgb(255, 255, 255)'],
                         [1, 'rgb(240, 240, 255)']
                     ]
                 },
                 borderWidth: 2,
                 plotBackgroundColor: 'rgba(255, 255, 255, .9)',
                 plotShadow: true,
                 plotBorderWidth: 1
             },
             global: { useUTC: false }
         });

         


         var chartMF = null;
         var chartSpeed = null;
         var chartLW = null;
         init1();



         function init1() {
             $('#row1').css('display', 'block');
             $('#row2').css('display', 'none');
             $('#row3').css('display', 'none');
             requestData();
             //initFirstLine1();
             setTimeout(function () {
                 init2();

                 //initFirstLine1();
             }, 60000);

         }

         function init2() {
             $('#row1').css("display", "none");
             $('#row2').css("display", "block");
             $('#row3').css('display', 'none');
             requestData1();
             setTimeout(function () {
                 init3();
             }, 60000);
         }

         function init3() {
             $('#row1').css("display", "none");
             $('#row2').css("display", "none");
             $('#row3').css('display', 'block');
             //initFirstLine2();
             requestData2();
             setTimeout(function () {


                 init1();
             }, 60000);
         }



         function initFirstLine() {
             // 图表配置
             chartMF = Highcharts.chart('container', {
                 chart: {
                     type: 'column',//指定图表的类型，默认是折线图（line）
                     //events: {
                     //    load: requestData // 图表加载完毕后执行的回调函数
                     //}
                 },
                 title: {
                     text: 'MF功率'                 // 标题
                 },
                 subtitle: {
                     text: '最近一小时工艺数据'
                 },

                 xAxis: {
                     type: 'datetime',
                     dateTimeLabelFormats: { minute: '%H:%M' },
                     pointInterval: 60 * 1000 // 间隔一天
                 },
                 yAxis: {
                     title: {
                         text: 'MF功率(KW)'                // y 轴标题
                     }
                 },
                 series: [
                     //{
                     //    name: 'A1功率',

                     //},
                     //{
                     //    name: 'A2功率',

                     //},
                     //{
                     //    name: 'B1功率',

                     //},
                     //{
                     //    name: 'B2功率',

                     //}
                 ],
                 plotOptions: {
                     column: {
                         dataLabels: {
                             inside: true
                         }
                     }
                 }
             });

         }

         function requestData() {
             $.getJSON('../Handler/ProcessHandler.ashx?action=l04mf', function (data) {
                 if (data.hasError == true) {
                     alert(data.error);
                     return;
                 }
                 else {
                     initFirstLine();
                     d = JSON.parse(data.data);
                     var names = [];
                     var series = [];
                     dA1 = [];
                     dA2 = [];
                     dB1 = [];
                     dB2 = [];
                     for (m in d) {
                         var name = d[m].SeriesName;
                         var index = $.inArray(name, names);
                         if (index == -1) {
                             names.push(name);
                             var s = {};
                             s.name = name;
                             s.data = [];
                             s.data.push([new Date(d[m].time).getTime(), d[m].value]);
                             series.push(s);
                         }
                         else {
                             series[index].data.push([new Date(d[m].time).getTime(), d[m].value]);
                         }
                     }


                     if (chartMF.series.length == 0) {
                         for (i in series) {
                             chartMF.addSeries({
                                 name: series[i].name,
                                 data: series[i].data
                             });
                         }
                     } else {
                         chartMF.update({
                             series: series
                         });
                     }

                 }
             });
         }

         function initFirstLine1() {
             // 图表配置
             chartSpeed = Highcharts.chart('container1', {
                 chart: {
                     type: 'line',//指定图表的类型，默认是折线图（line）
                     //events: {
                     //    load: requestData1 // 图表加载完毕后执行的回调函数
                     //}
                 },
                 title: {
                     text: '生产速度'                 // 标题
                 },
                 subtitle: {
                     text: '最近一小时工艺数据'
                 },

                 xAxis: {
                     type: 'datetime',
                     dateTimeLabelFormats: { minute: '%H:%M' },

                 },
                 yAxis: {
                     title: {
                         text: '生产速度'                // y 轴标题
                     }
                 },

             });



         }

         function requestData1() {
             $.getJSON('../Handler/ProcessHandler.ashx?action=l04productspeed', function (data) {
                 if (data.hasError == true) {
                     alert(data.error);
                     return;
                 }
                 else {
                     d = JSON.parse(data.data);
                     initFirstLine1();
                     var names = [];
                     var series = [];
                     for (m in d) {
                         var name = d[m].SeriesName;
                         //var s = {};
                         //s.name = name;
                         //s.data = [];
                         //s.data.push(d[m]);
                         var index = $.inArray(name, names);
                         if (index == -1) {
                             //不存在
                             names.push(name);
                             var s = {};
                             s.name = name;
                             s.data = [];
                             s.data.push([new Date(d[m].time).getTime(), d[m].value]);
                             series.push(s);
                         }
                         else {
                             series[index].data.push([new Date(d[m].time).getTime(), d[m].value]);
                         }
                     }

                     if (chartSpeed.series.length == 0) {
                         for (i in series) {
                             chartSpeed.addSeries({
                                 name: series[i].name,
                                 data: series[i].data
                             });
                         }
                     } else {
                         chartSpeed.update({
                             series: series
                         });
                     }

                 }
             });

         }

         function initFirstLine2() {
             // 图表配置
             chartLW = Highcharts.chart('container2', {
                 chart: {
                     type: 'line',//指定图表的类型，默认是折线图（line）
                     //events: {
                     //    load: requestData2 // 图表加载完毕后执行的回调函数
                     //}
                 },
                 title: {
                     text: '炉温'                 // 标题
                 },
                 subtitle: {
                     text: '最近一小时工艺数据'
                 },

                 xAxis: {
                     type: 'datetime',
                     dateTimeLabelFormats: { minute: '%H:%M' },

                 },
                 yAxis: {
                     title: {
                         text: '炉温'                // y 轴标题
                     }
                 },

                 plotOptions: {
                     line: {
                         dataLabels: {
                             enabled: true
                         }
                     }
                 }
             });



         }

         function requestData2() {
             $.getJSON('../Handler/ProcessHandler.ashx?action=l04lw', function (data) {
                 if (data.hasError == true) {
                     alert(data.error);
                     return;
                 }
                 else {
                     d = JSON.parse(data.data);
                     initFirstLine2();
                     var names = [];
                     var series = [];
                     for (m in d) {
                         var name = d[m].SeriesName;

                         var index = $.inArray(name, names);
                         if (index == -1) {
                             //不存在
                             names.push(name);
                             var s = {};
                             s.name = name;
                             s.data = [];
                             s.data.push([new Date(d[m].time).getTime(), d[m].value]);
                             series.push(s);
                         }
                         else {
                             series[index].data.push([new Date(d[m].time).getTime(), d[m].value]);
                         }
                     }

                     if (chartLW.series.length == 0) {
                         for (i in series) {
                             chartLW.addSeries({
                                 name: series[i].name,
                                 data: series[i].data
                             });
                         }
                     } else {
                         chartLW.update({
                             series: series
                         });
                     }

                     //d1 = [];
                     //d2 = [];
                     //d3 = [];
                     //d4 = [];
                     //d5 = [];
                     //d6 = [];

                     //for (m in d) {
                     //    if (d[m].SeriesName == "1区温度") {
                     //        d1.push(d[m]);
                     //    }
                     //    if (d[m].SeriesName == "2区温度") {
                     //        d2.push(d[m]);
                     //    }
                     //    if (d[m].SeriesName == "3区温度") {
                     //        d3.push(d[m]);
                     //    }
                     //    if (d[m].SeriesName == "4区温度") {
                     //        d4.push(d[m]);
                     //    }
                     //    if (d[m].SeriesName == "5区温度") {
                     //        d5.push(d[m]);
                     //    }
                     //    if (d[m].SeriesName == "6区温度") {
                     //        d6.push(d[m]);
                     //    }
                     //}
                     //var sl = chartLW.series.length;
                     //for (var i = 0; i < sl; i++) {
                     //    var sd = chartLW.series[i].data.length;
                     //    var dd = [];
                     //    if (chartLW.series[i].name == "1区温度") {
                     //        dd = d1;
                     //    }
                     //    if (chartLW.series[i].name == "2区温度") {
                     //        dd = d2;
                     //    }
                     //    if (chartLW.series[i].name == "3区温度") {
                     //        dd = d3;
                     //    }
                     //    if (chartLW.series[i].name == "4区温度") {
                     //        dd = d4;
                     //    }
                     //    if (chartLW.series[i].name == "5区温度") {
                     //        dd = d5;
                     //    }
                     //    if (chartLW.series[i].name == "6区温度") {
                     //        dd = d6;
                     //    }
                     //    if (sd == 0) {
                     //        for (j in dd) {
                     //            var point = [new Date(dd[j].time).getTime(), dd[j].value];
                     //            chartLW.series[i].addPoint(point, true);
                     //        }
                     //    }
                     //    else {
                     //        lastPoint = chartLW.series[i].data[sd - 1];
                     //        if (lastPoint.x != new Date(d[dd.length - 1].time).getTime()) {
                     //            chartLW.series[i].addPoint([new Date(d[dd.length - 1].time).getTime(), d[dd.length - 1].value], true, true);
                     //        }
                     //    }
                     //}
                     //var sd = chartLW.series[0].data.length;



                     //if (sd == 0) {
                     //    for (i in d) {
                     //        var point = [new Date(d[i].time).getTime(), d[i].value];
                     //        chartLW.series[0].addPoint(point, true);
                     //    }

                     //}
                     //else {
                     //    lastPoint = chartLW.series[0].data[sd - 1];
                     //    if (lastPoint.x != new Date(d[d.length - 1].time).getTime()) {
                     //        chartLW.series[0].addPoint([new Date(d[d.length - 1].time).getTime(), d[d.length - 1].value], true, true);
                     //    }

                     //}


                 }
             });





             //获取数据
             //var data = [3, 4, 5, 6, 7, 8];
             //for (var i = 0; i < 6; i++)
             //{
             //    data[i] = data[i] + Math.random();
             //}

             //Highcharts.charts[0].series[0].setData(data);

             //var date = new Date;
             //var currentYear = date.getFullYear();
             //var currentMonth = date.getMonth() + 1;
             //var currentDay = date.getDay();
             //var currentHour = date.getHours() - 1;
             //var currentMinute = date.getMinutes();
             //console.log(currentMinute);
             //Highcharts.charts[0].options.plotOptions.series.pointStart = Date.UTC(currentYear, currentMonth, currentDay, currentHour, currentMinute);

             //Highcharts.charts[0].redraw();    

             //setTimeout(requestData, 60000);
         }
    </script>
</body>
</html>
