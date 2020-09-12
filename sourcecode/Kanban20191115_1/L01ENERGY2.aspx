<%@ Page Title="" Language="C#" MasterPageFile="~/KanbanSite.Master" AutoEventWireup="true" CodeBehind="L01ENERGY2.aspx.cs" Inherits="Kanban.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="page-header">
        <div class="logo">
            <%--<img src="img/sumin_log.JPG" style="width: 60px; height: 60px;" onclick="test();" />--%>
            <span style="font-size: 28px; color: white;">一号线电能源看板</span>
            <%--  <h1 style="/*margin-left:600px;*/">一号线生产看板</h1>--%>
            <span style="color: white;" id="timeDiv"></span>
            <button class="button-primary" onclick="prePage()">上一页</button>
           
            <button class="button-info" onclick="homePage()">回到首页</button>
            <button class="button-success" onclick="nextPage()">下一页</button>
        </div>
    </div>
   <div class="row">
        <div class="col-md-4 col-sm-12">
            <div id="container1" style="min-width: 400px; height: 280px;"></div>
        </div>
        <div class="col-md-4 col-sm-12">
            <div id="container2" style="min-width: 400px; height: 280px;"></div>
        </div>
        <div class="col-md-4 col-sm-12">
            <div id="container3" style="min-width: 400px; height: 280px;"></div>
        </div>
    </div>
    <div class="row" style="margin-top:10px;">
        <div class="col-md-4 col-sm-12">
            <div id="container4" style="min-width: 400px; height: 280px;"></div>
        </div>
        <div class="col-md-4 col-sm-12">
            <div id="container5" style="min-width: 400px; height: 280px;"></div>
        </div>
    </div>
     <script>
         Highcharts.setOptions({
             chart: {
                 backgroundColor: {
                     linearGradient: [0, 0, 500, 500],
                     stops: [
                         [0, 'rgb(255, 255, 255)'],
                         [1, 'rgb(240, 240, 255)']
                     ]
                 },
                 plotBackgroundColor: 'rgba(208, 208, 208, .5)',
                 borderWidth: 1,
                 //plotBackgroundColor: 'rgba(255, 255, 255, .9)',
                 plotShadow: false,
                 plotBorderWidth: 1
             },
             global: { useUTC: false }
         });

         function current() {
             var d = new Date(), str = '';
             str += d.getFullYear() + '/'; //获取当前年份 
             str += d.getMonth() + 1 + '/'; //获取当前月份（0——11） 
             str += d.getDate() + ' ';
             str += d.getHours() + ':';
             str += d.getMinutes() + ':';
             str += d.getSeconds() + '';
             return str;
         }
         setInterval(function () {
             //$("#timeDiv").html( "123343");
             $("#timeDiv").html(current());
         }, 1000);

         function prePage() {
             window.location.href = "L01ENERGY1.aspx";
         }

         function nextPage() {
             window.location.href = "L01LW.aspx";
         }

         function homePage() {
             window.location.href = "Index.aspx";
         }
         var chart1 = null;
         var chart2 = null;
         var chart3 = null;
         var chart4 = null;
         var chart5 = null;
         init();
         function init() {
             //initLinSuanChart()
             //initYanSuanChart()
             //initJianChart()
             //initpowerChart()
             requestData();
             setTimeout(function () {
                 window.location.href = "L01LW.aspx";
             }, 60000);
         }

         function initChart1() {
             chart1 = Highcharts.chart('container1', {

                 chart: {
                     type: 'column',
                    
                 },
                 title: {
                     text: '电用量'

                 },
                 xAxis: {
                     categories: ['L01']
                 },
                 yAxis: {
                     allowDecimals: false,
                     title: {
                         text: '度',
                         rotation: 0
                     }
                 },
                 tooltip: {
                     formatter: function () {
                         return '<b>' + this.series.name + '</b><br/>' +
                             this.point.y + ' 度';
                     }
                 }
             });

         }

         function initChart2() {
             chart2 = Highcharts.chart('container2', {

                 chart: {
                     type: 'column',

                 },
                 title: {
                     text: '电用量'

                 },
                 xAxis: {
                     categories: ['L01']
                 },
                 yAxis: {
                     allowDecimals: false,
                     title: {
                         text: '度',
                         rotation: 0
                     }
                 },
                 tooltip: {
                     formatter: function () {
                         return '<b>' + this.series.name + '</b><br/>' +
                             this.point.y + ' 度';
                     }
                 }
             });

         }

         function initChart3() {
             chart3 = Highcharts.chart('container3', {

                 chart: {
                     type: 'column',

                 },
                 title: {
                     text: '电用量'

                 },
                 xAxis: {
                     categories: ['L01']
                 },
                 yAxis: {
                     allowDecimals: false,
                     title: {
                         text: '度',
                         rotation: 0
                     }
                 },
                 tooltip: {
                     formatter: function () {
                         return '<b>' + this.series.name + '</b><br/>' +
                             this.point.y + ' 度';
                     }
                 }
             });

         }


         function initChart4() {
             chart4 = Highcharts.chart('container4', {

                 chart: {
                     type: 'column',

                 },
                 title: {
                     text: '电用量'

                 },
                 xAxis: {
                     categories: ['L01']
                 },
                 yAxis: {
                     allowDecimals: false,
                     title: {
                         text: '度',
                         rotation: 0
                     }
                 },
                 tooltip: {
                     formatter: function () {
                         return '<b>' + this.series.name + '</b><br/>' +
                             this.point.y + ' 度';
                     }
                 }
             });

         }


         function initChart5() {
             chart5 = Highcharts.chart('container5', {

                 chart: {
                     type: 'column',

                 },
                 title: {
                     text: '电用量'

                 },
                 xAxis: {
                     categories: ['L01']
                 },
                 yAxis: {
                     allowDecimals: false,
                     title: {
                         text: '度',
                         rotation: 0
                     }
                 },
                 tooltip: {
                     formatter: function () {
                         return '<b>' + this.series.name + '</b><br/>' +
                             this.point.y + ' 度';
                     }
                 }
             });

         }

         function requestData() {

             $.getJSON('../Handler/EnergyHandler.ashx?action=getpowerdata', {'Line':'L01'}, function (data) {
                 if (data.hasError == true) {
                     alert(data.error);
                     return;
                 }
                 else {
                     d = JSON.parse(data.data);
                     var s1 = [];
                     var s2 = [];
                     var s3 = [];
                     var s4 = [];
                     var s5 = [];
                     var name1 = [];
                     var name2 = [];
                     var name3 = [];
                     var name4 = [];
                     var name5 = [];
                     for (m in d) {
                         if (d[m].Position == "MF1") {
                             s1.push({name:d[m].CollectDate,data:[d[m].DataValue]})
                         }
                         if (d[m].Position == "MF2") {
                             s2.push({ name: d[m].CollectDate, data: [d[m].DataValue] })
                         }
                         if (d[m].Position == "MF3") {
                             s3.push({ name: d[m].CollectDate, data: [d[m].DataValue] })
                         }
                         if (d[m].Position == "MF4") {
                             s4.push({ name: d[m].CollectDate, data: [d[m].DataValue] })
                         }
                         if (d[m].Position == "MF5") {
                             s5.push({ name: d[m].CollectDate, data: [d[m].DataValue] })
                         }
                         
                     }
                     initChart1();
                     initChart2();
                     initChart3();
                     initChart4();
                     initChart5();

                     for (i in s1) {
                         chart1.addSeries({
                             name: s1[i].name,
                             data: s1[i].data
                         });
                     }

                     for (i in s2) {
                         chart2.addSeries({
                             name: s2[i].name,
                             data: s2[i].data
                         });
                     }

                     for (i in s3) {
                         chart3.addSeries({
                             name: s3[i].name,
                             data: s3[i].data
                         });
                     }


                     for (i in s4) {
                         chart4.addSeries({
                             name: s4[i].name,
                             data: s4[i].data
                         });
                     }

                     for (i in s5) {
                         chart5.addSeries({
                             name: s5[i].name,
                             data: s5[i].data
                         });
                     }

                    
 


                 }
             });
         }

     
         function initLinSuanChart() {
             var chart = Highcharts.chart('container1', {
                 chart: {
                     type: 'bar',
                     events: {
                         load: requestLinSuanData // 图表加载完毕后执行的回调函数
                     }


                 },
                 title: {
                     text: '磷酸用量'
                 },

                 xAxis: {
                     categories: ['L01', 'L02', 'L03', 'L04'],
                     title: {
                         text: null
                     }
                 },
                 yAxis: {
                     min: 0,
                     title: {
                         text: '升(L)',
                         align: 'high'
                     },
                     labels: {
                         overflow: 'justify'
                     }
                 },
                 tooltip: {
                     valueSuffix: ' 百万'
                 },
                 plotOptions: {
                     bar: {
                         dataLabels: {
                             enabled: true,
                             allowOverlap: true // 允许数据标签重叠
                         }
                     }
                 },
                 legend: {
                     layout: 'vertical',
                     align: 'right',
                     verticalAlign: 'top',
                     x: -40,
                     y: 100,
                     floating: false,
                     borderWidth: 1,
                     backgroundColor: ((Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'),
                     shadow: true
                 },

             });

             //window.setInterval(requestPowerData,60000);
         }
         function requestLinSuanData() {

             $.getJSON('../Handler/EnergyHandler.ashx?action=getlinsuan', function (data) {
                 if (data.hasError == true) {
                     alert(data.error);
                     return;
                 }
                 else {
                     d = JSON.parse(data.data);
                     var sd = Highcharts.charts[0].series.length;
                     if (sd == 0) {
                         for (i in d) {
                             Highcharts.charts[0].addSeries({
                                 name: d[i].name,
                                 data: d[i].data
                             });
                         }

                     }
                     //else {
                     //    lastPoint = Highcharts.charts[0].series[0].data[sd - 1];

                     //    if (lastPoint.x != new Date(d[d.length - 1].time).getTime()) {
                     //        Highcharts.charts[0].series[0].addPoint([new Date(d[d.length - 1].time).getTime(), d[d.length - 1].value], true, true);
                     //    }

                     //}


                 }
             });



             //var sd = Highcharts.charts[0].series.length;
             //d = [{ name: '2019-07-10', data: [12, 13, 14, 18] }, { name: '2019-07-11', data: [12, 13, 14, 20] }, { name: '2019-07-12', data: [12, 13, 14, 20] }];
             //if (sd == 0) {
             //    for (i in d) {
             //        Highcharts.charts[0].addSeries({
             //            name: d[i].name,
             //            data: d[i].data
             //        });
             //    }



             //}
         }
         function initYanSuanChart() {
             var chart = Highcharts.chart('container2', {
                 chart: {
                     type: 'bar',
                     events: {
                         load: requestYanSuanData // 图表加载完毕后执行的回调函数
                     }


                 },
                 title: {
                     text: '盐酸用量'
                 },

                 xAxis: {
                     categories: ['L01', 'L02', 'L03', 'L04'],
                     title: {
                         text: null
                     }
                 },
                 yAxis: {
                     min: 0,
                     title: {
                         text: '升(L)',
                         align: 'high'
                     },
                     labels: {
                         overflow: 'justify'
                     }
                 },
                 tooltip: {
                     valueSuffix: ' 百万'
                 },
                 plotOptions: {
                     bar: {
                         dataLabels: {
                             enabled: true,
                             allowOverlap: true // 允许数据标签重叠
                         }
                     }
                 },
                 legend: {
                     layout: 'vertical',
                     align: 'right',
                     verticalAlign: 'top',
                     x: -40,
                     y: 100,
                     floating: false,
                     borderWidth: 1,
                     backgroundColor: ((Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'),
                     shadow: true
                 },

             });

             //window.setInterval(requestPowerData,60000);
         }
         function requestYanSuanData() {

             $.getJSON('../Handler/EnergyHandler.ashx?action=getyansuan', function (data) {
                 if (data.hasError == true) {
                     alert(data.error);
                     return;
                 }
                 else {
                     d = JSON.parse(data.data);
                     var sd = Highcharts.charts[1].series.length;
                     if (sd == 0) {
                         for (i in d) {
                             Highcharts.charts[1].addSeries({
                                 name: d[i].name,
                                 data: d[i].data
                             });
                         }

                     }
                     //else {
                     //    lastPoint = Highcharts.charts[0].series[0].data[sd - 1];

                     //    if (lastPoint.x != new Date(d[d.length - 1].time).getTime()) {
                     //        Highcharts.charts[0].series[0].addPoint([new Date(d[d.length - 1].time).getTime(), d[d.length - 1].value], true, true);
                     //    }

                     //}


                 }
             });



             //var sd = Highcharts.charts[0].series.length;
             //d = [{ name: '2019-07-10', data: [12, 13, 14, 18] }, { name: '2019-07-11', data: [12, 13, 14, 20] }, { name: '2019-07-12', data: [12, 13, 14, 20] }];
             //if (sd == 0) {
             //    for (i in d) {
             //        Highcharts.charts[0].addSeries({
             //            name: d[i].name,
             //            data: d[i].data
             //        });
             //    }



             //}
         }
         function initJianChart() {
             var chart = Highcharts.chart('container3', {
                 chart: {
                     type: 'bar',
                     events: {
                         load: requestJianQiData // 图表加载完毕后执行的回调函数
                     }


                 },
                 title: {
                     text: '碱用量'
                 },

                 xAxis: {
                     categories: ['L01', 'L02', 'L03', 'L04'],
                     title: {
                         text: null
                     }
                 },
                 yAxis: {
                     min: 0,
                     title: {
                         text: '升(L)',
                         align: 'high'
                     },
                     labels: {
                         overflow: 'justify'
                     }
                 },
                 tooltip: {
                     valueSuffix: ' 百万'
                 },
                 plotOptions: {
                     bar: {
                         dataLabels: {
                             enabled: true,
                             allowOverlap: true // 允许数据标签重叠
                         }
                     }
                 },
                 legend: {
                     layout: 'vertical',
                     align: 'right',
                     verticalAlign: 'top',
                     x: -40,
                     y: 100,
                     floating: false,
                     borderWidth: 1,
                     backgroundColor: ((Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'),
                     shadow: true
                 },

             });

             //window.setInterval(requestPowerData,60000);
         }

         function requestJianQiData() {
             $.getJSON('../Handler/EnergyHandler.ashx?action=getjian', function (data) {
                 if (data.hasError == true) {
                     alert(data.error);
                     return;
                 }
                 else {
                     d = JSON.parse(data.data);
                     var sd = Highcharts.charts[2].series.length;
                     if (sd == 0) {
                         for (i in d) {
                             Highcharts.charts[2].addSeries({
                                 name: d[i].name,
                                 data: d[i].data
                             });
                         }

                     }

                 }
             });

         }
         function initpowerChart() {
             var chart = Highcharts.chart('container4', {

                 chart: {
                     type: 'column',
                     events: {
                         load: requestpowerData
                     }
                 },
                 title: {
                     text: '电用量'
                     // 该功能依赖 data.js 模块，详见https://www.hcharts.cn/docs/data-modules
                 },
                 xAxis: {
                     categories: ['L01', 'L02', 'L03', 'L04']
                 },
                 yAxis: {
                     allowDecimals: false,
                     title: {
                         text: '度',
                         rotation: 0
                     }
                 },
                 tooltip: {
                     formatter: function () {
                         return '<b>' + this.series.name + '</b><br/>' +
                             this.point.y + ' 度';
                     }
                 }
             });
         }
         function requestpowerData() {
             $.getJSON('../Handler/EnergyHandler.ashx?action=getpower', function (data) {
                 if (data.hasError == true) {
                     alert(data.error);
                     return;
                 }
                 else {
                     d = JSON.parse(data.data);
                     var sd = Highcharts.charts[3].series.length;
                     if (sd == 0) {
                         for (i in d) {
                             Highcharts.charts[3].addSeries({
                                 name: d[i].name,
                                 data: d[i].data
                             });
                         }

                     }

                 }
             });

         }
         </script>
</asp:Content>
