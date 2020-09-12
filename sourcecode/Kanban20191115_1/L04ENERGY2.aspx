<%@ Page Title="" Language="C#" MasterPageFile="~/KanbanSite.Master" AutoEventWireup="true" CodeBehind="L04ENERGY2.aspx.cs" Inherits="Kanban.L04ENERGY2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="page-header">
        <div class="logo">
            <%--<img src="img/sumin_log.JPG" style="width: 60px; height: 60px;" onclick="test();" />--%>
            <span style="font-size: 28px; color: white;">四号线能源看板</span>
            <%--  <h1 style="/*margin-left:600px;*/">一号线生产看板</h1>--%>
            <span style="color: white;" id="timeDiv"></span>
            <button class="button-primary" onclick="prePage()">上一页</button>
           
            <button class="button-info" onclick="homePage()">回到首页</button>
            <button class="button-success" onclick="nextPage()">下一页</button>
        </div>
    </div>
   <div class="row">
        <div class="col-md-6 col-sm-12">
            <div id="container1" style="min-width: 400px; height: 300px;"></div>
        </div>
        <div class="col-md-6 col-sm-12">
            <div id="container2" style="min-width: 400px; height: 300px;"></div>
        </div>
    </div>
    <div class="row" style="margin-top:10px;">
        <div class="col-md-6 col-sm-12">
            <div id="container3" style="min-width: 400px; height: 300px;"></div>
        </div>
        <div class="col-md-6 col-sm-12">
            <div id="container4" style="min-width: 400px; height: 300px;"></div>
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
             window.location.href = "L04ENERGY1.aspx";
         }

         function nextPage() {
             window.location.href = "L04LW.aspx";
         }

         function homePage() {
             window.location.href = "Index1.aspx";
         }
         init();
         function init() {
             //initLinSuanChart()
             //initYanSuanChart()
             //initJianChart()
             //initpowerChart()
             requestLinSuanData();
             requestYanSuanData();
             requestJianData();
             requestpowerData();
             setTimeout(function () {
                 window.location.href = "L04LW.aspx";
             }, 60000);
         }

         var linsuanChart = null;
         var yansuanChart = null;
         var jianChart = null;
         var powerChart = null;
     
         function initLinSuanChart() {
             linsuanChart = Highcharts.chart('container1', {
                 chart: {
                     type: 'bar',
                     //events: {
                     //    load: requestLinSuanData // 图表加载完毕后执行的回调函数
                     //}


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

             $.getJSON('../Handler/EnergyHandler.ashx?action=getlinsuan', {'Line':'L04'}, function (data) {
                 if (data.hasError == true) {
                     alert(data.error);
                     return;
                 }
                 else {
                     d = JSON.parse(data.data);
                     initLinSuanChart();
                     var sd = linsuanChart.series.length;
                     if (sd == 0) {
                         for (i in d) {
                             linsuanChart.addSeries({
                                 name: d[i].name,
                                 data: d[i].data
                             });
                         }

                     }
                  


                 }
             });



         
         }
         function initYanSuanChart() {
             yansuanChart = Highcharts.chart('container2', {
                 chart: {
                     type: 'bar',
                     //events: {
                     //    load: requestYanSuanData // 图表加载完毕后执行的回调函数
                     //}


                 },
                 title: {
                     text: '盐酸用量'
                 },

                 xAxis: {
                     categories: [ 'L04'],
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

             $.getJSON('../Handler/EnergyHandler.ashx?action=getyansuan', {'Line':'L04'},function (data) {
                 if (data.hasError == true) {
                     alert(data.error);
                     return;
                 }
                 else {
                     d = JSON.parse(data.data);
                     initYanSuanChart();
                     var sd = yansuanChart.series.length;
                     if (sd == 0) {
                         for (i in d) {
                             yansuanChart.addSeries({
                                 name: d[i].name,
                                 data: d[i].data
                             });
                         }

                     }
                   

                 }
             });



             
         }
         function initJianChart() {
             jianChart = Highcharts.chart('container3', {
                 chart: {
                     type: 'bar',
                     //events: {
                     //    load: requestJianQiData // 图表加载完毕后执行的回调函数
                     //}


                 },
                 title: {
                     text: '碱用量'
                 },

                 xAxis: {
                     categories: [ 'L04'],
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

         function requestJianData() {
             $.getJSON('../Handler/EnergyHandler.ashx?action=getjian', {'Line':'L04'},function (data) {
                 if (data.hasError == true) {
                     alert(data.error);
                     return;
                 }
                 else {
                     d = JSON.parse(data.data);
                     initJianChart();
                     var sd = jianChart.series.length;
                     if (sd == 0) {
                         for (i in d) {
                             jianChart.addSeries({
                                 name: d[i].name,
                                 data: d[i].data
                             });
                         }

                     }

                 }
             });

         }
         function initpowerChart() {
             powerChart = Highcharts.chart('container4', {

                 chart: {
                     type: 'column',
                     //events: {
                     //    load: requestpowerData
                     //}
                 },
                 title: {
                     text: '电用量'
                     // 该功能依赖 data.js 模块，详见https://www.hcharts.cn/docs/data-modules
                 },
                 xAxis: {
                     categories: [ 'L04']
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
             $.getJSON('../Handler/EnergyHandler.ashx?action=getpower', {'Line':'L04'}, function (data) {
                 if (data.hasError == true) {
                     alert(data.error);
                     return;
                 }
                 else {
                     d = JSON.parse(data.data);
                     initpowerChart();
                     var sd = powerChart.series.length;
                     if (sd == 0) {
                         for (i in d) {
                             powerChart.addSeries({
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
