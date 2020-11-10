<%@ Page Title="" Language="C#" MasterPageFile="~/KanbanSite.Master" AutoEventWireup="true" CodeBehind="HJLookBand.aspx.cs" Inherits="Kanban.HJLookBand" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="page-header">
 <div class="logo" style="width:100%">
    <img src="img/BTLOG.png" style="width:60px;height:60px;margin-left: 20px;" />
    <span style="font-size: 32px; color: white;display:inline-block;width:75%;text-align:center;">博拓车间环境监控看板</span>
     <span style="color: white;width:200px;font-size: 16px;" id="cg">2016-12-21 上午13:00:00</span>
 </div>
</div> 

 <div class="row" style="margin-top: 30px;">
        
   <div class="span15 offset1">
   <div id="container01" style="min-width: 300px; height: 400px;"></div>
   </div>

    <div class="span15">
    <div id="container02" style="min-width: 400px; height: 400px;"></div>
    </div>
 </div>

  <script>
    <!--这个cg就是span的id，初始化Date时间并转化为字符string类型,每1000毫秒，setInterval() 就会调用函数，直到被关闭。-->
    setInterval("cg.innerHTML=new Date().toLocaleString()", 1000);
  </script>

 <script>
     //基于准备好的dom，初始化echarts实例
     Highcharts.setOptions({
         chart: {
             backgroundColor: {
                 linearGradient: [0, 0, 500, 500],
                 stops: [
                     [0, 'rgb(255, 255, 255)'],
                     [1, 'rgb(70,130,180)']
                 ]
             },
             borderWidth: 2,
             plotBackgroundColor: 'rgba(70,130,180, .2)',
             plotShadow: true,
             plotBorderWidth: 1
         },
         global: { useUTC: false }
     });

     // 指定图表的配置项和数据
     requestDeviceData();

     function initDeviceChart() {
         chartWD = Highcharts.chart('container01', {
             chart: {
                 type: 'areaspline',
                 zoomType: 'xy'
             },
             title: {
                 text: '车间温湿度环境(东北区域)'
             },
             subtitle: {
                 text: '数据来源:车间温湿度采集器一'
             },
             xAxis: {
                 title: {
                     text: '时间',

                 },
                 //type: 'String',
                 //dateTimeLabelFormats: { day: '%e of %b' },
                 type: 'datetime',
                 //tickmarkPlacement: 'on',
                 tickInterval: 1000 * 60 * 5, // 每隔五分钟显示一个刻度
                 formatter: function () {
                     //dateTimeLabelFormats: { day: '%e of %b' }
                     dateTimeLabelFormats: { second: ' %H:%M:%S' }
                 },// 每



                 startOnTick: true,
                 endOnTick: true,
                 showLastLabel: true
             },
             yAxis: [
                 { // 第一条Y轴
                     labels: {
                         format: '{value}\xB0C',
                         style: {
                             color: Highcharts.getOptions().colors[1]
                         }
                     },
                     title: {
                         text: '温度',
                         style: {
                             color: Highcharts.getOptions().colors[1]
                         }
                     }
                 }, { // 第二条Y轴
                     title: {
                         text: '湿度',
                         style: {
                             color: Highcharts.getOptions().colors[1]
                         }
                     },
                     labels: {
                         format: '{value}%RH ',
                         style: {
                             color: Highcharts.getOptions().colors[2]
                         }
                     },
                     opposite: true
                 },
                 { // 第二条Y轴
                     title: {
                         text: 'PM2.5',
                         style: {
                             color: Highcharts.getOptions().colors[1]
                         }
                     },
                     labels: {
                         format: '{value} ',
                         style: {
                             color: Highcharts.getOptions().colors[3]
                         }
                     },
                     opposite: true
                 }
             ],

             tooltip: {
                 shared: true
             },
             colors: [
                 'rgba(255,182,193,0.99)', 'rgba(144,238,144,0.99)', 'rgba(255,150,85,0.5)', '#FFF263', '#6AF9C4'],
             legend: {
                 layout: 'vertical',
                 align: 'top',
                 verticalAlign: 'top',
                 x: 445,
                 y: -4,
                 floating: false,
                 backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColor) || 'rgba(173,216,230, .2)',
                 borderWidth: 1
             },
             plotOptions: {
                 //scatter: {
                 //    marker: {
                 //        radius: 5,
                 //        states: {
                 //            hover: {
                 //                enabled: true,
                 //                lineColor: 'rgb(100,100,100)'
                 //            }
                 //        }
                 //    },


                 areaspline:
                 {
                     //dataLabels: {

                     //    enabled: true,
                     //    allowOverlap: true
                     // },
                     fillOpacity: 0.3,
                     //fillColor: {
                     ////        colors:['red','blue']
                     ////        linearGradient: { x1: 0, y1: 0, x2: 0, y2: 0.9 },
                     //          // colors: rgba(255,182,193,0.1),

                     ////        stops: [
                     ////            [0, Highcharts.getOptions().colors[0]],
                     ////            [1, Highcharts.Color(Highcharts.getOptions().colors[0]).setOpacity(0).get('rgba')]
                     ////        ]
                     //    },

                     //colors:['pink','blue'],



                     states: {
                         hover: {
                             marker: {
                                 enabled: false
                             }
                         }
                     },

                 }


             },





             series: [{
                 name: 'PM2.5',
                 type: 'column',
                 tooltip: {
                     valueSuffix: ' ug/M3'
                 },
                 yAxis: 2
             },
             {
                 name: '湿度',

                 tooltip: {
                     valueSuffix: ' %RH'
                 },
                 yAxis: 1
             },
             {
                 name: '温度',

                 tooltip: {
                     valueSuffix: ' \xB0C'
                 },
                 yAxis: 0
             },


             ],
         });

         window.setInterval(requestDeviceData, 600000);
     }


     function requestDeviceData() {
         $.getJSON('../Handler/ProcessHandler.ashx?action=l002wd', function (data) {

             if (data.hasError == true) {
                 alert(data.error);
                 return;
             }
             else {
                 initDeviceChart();
                 d = JSON.parse(data.data);
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


                 if (chartWD.series.length == 0) {
                     for (i in series) {
                         chartWD.addSeries({
                             name: series[i].name,
                             data: series[i].data
                         });
                     }
                 } else {
                     chartWD.update({
                         series: series
                     });
                 }




                 //var sl = chartMF.series.length;
                 //for (i = 0; i < sl; i++) {
                 //    var sname = chartMF.series[i].name;
                 //    var dd = [];
                 //    if (sname == "A1功率") {
                 //        dd = dA1;
                 //    }
                 //    if (sname == "B1功率") {
                 //        dd = dB1;
                 //    }
                 //    if (sname == "A2功率") {
                 //        dd = dA2;
                 //    }
                 //    if (sname == "B2功率") {
                 //        dd = dB2;
                 //    }
                 //    //var sd = chartMF.series[i].data.length;
                 //    //if (sd == 0) {
                 //    //    for (j in dd) {
                 //    //        //console.log(new Date(d[i].time));
                 //    //        //console.log(new Date(d[i].time).getTime());
                 //    //        var point = [new Date(dd[j].time).getTime(), dd[j].value];
                 //    //        chartMF.series[i].addPoint(point, true);
                 //    //    }

                 //    //}
                 //    //else {
                 //    //    lastPoint = chartMF.series[i].data[sd - 1];

                 //    //    if (lastPoint.x != new Date(dd[dd.length - 1].time).getTime()) {
                 //    //        chartMF.series[i].addPoint([new Date(dd[dd.length - 1].time).getTime(), dd[dd.length - 1].value], true, true);
                 //    //    }
                 //    //}
                 //}





             }
         });
     }

     requestWSDData();

     function initWSDChart() {
         chartWSD = Highcharts.chart('container2', {
             chart: {
                 type: 'areaspline',
                 zoomType: 'xy'
             },
             title: {
                 text: '车间温湿度环境（西南区域）'
             },
             subtitle: {
                 text: '数据来源:车间温湿度采集器二'
             },
             xAxis: {
                 title: {
                     text: '时间',

                 },
                 //type: 'String',
                 //dateTimeLabelFormats: { day: '%e of %b' },
                 type: 'datetime',
                 //tickmarkPlacement: 'on',
                 tickInterval: 1000 * 60 * 5, // 每隔几个点显示一个刻度
                 formatter: function () {
                     //dateTimeLabelFormats: { day: '%e of %b' }
                     dateTimeLabelFormats: { second: ' %H:%M:%S' }
                 },// 每



                 startOnTick: true,
                 endOnTick: true,
                 showLastLabel: true
             },
             yAxis: [
                 { // 第一条Y轴
                     labels: {
                         format: '{value}\xB0C',
                         style: {
                             color: Highcharts.getOptions().colors[1]
                         }
                     },
                     title: {
                         text: '温度',
                         style: {
                             color: Highcharts.getOptions().colors[1]
                         }
                     }
                 },
                 { // 第二条Y轴
                     title: {
                         text: '湿度',
                         style: {
                             color: Highcharts.getOptions().colors[1]
                         }
                     },
                     labels: {
                         format: '{value}%RH ',
                         style: {
                             color: Highcharts.getOptions().colors[2]
                         }
                     },
                     opposite: true
                 },
                 { // 第二条Y轴
                     title: {
                         text: 'PM2.5',
                         style: {
                             color: Highcharts.getOptions().colors[1]
                         }
                     },
                     labels: {
                         format: '{value} ',
                         style: {
                             color: Highcharts.getOptions().colors[3]
                         }
                     },
                     opposite: true
                 }
             ],

             tooltip: {
                 shared: true
             },
             // colors: [
             //      'rgba(255,182,193,0.99)' , 'rgba(144,238,144,0.99)' , 'rgba(255,150,85,0.1)', '#FF9655', '#FFF263', '#6AF9C4'],
             legend: {
                 layout: 'vertical',
                 align: 'top',
                 verticalAlign: 'top',
                 x: 445,
                 y: -4,
                 floating: false,
                 backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColor) || 'rgba(173,216,230, .2)',
                 borderWidth: 1
             },
             plotOptions: {
                 //scatter: {
                 //    marker: {
                 //        radius: 5,
                 //        states: {
                 //            hover: {
                 //                enabled: true,
                 //                lineColor: 'rgb(100,100,100)'
                 //            }
                 //        }
                 //    },


                 areaspline:
                 {
                     //dataLabels: {

                     //    enabled: true,
                     //    allowOverlap: true
                     // },
                     fillOpacity: 0.3,
                     //fillColor: {
                     //        colors:['red','blue']
                     //        linearGradient: { x1: 0, y1: 0, x2: 0, y2: 0.9 },

                     //        stops: [
                     //            [0, Highcharts.getOptions().colors[0]],
                     //            [1, Highcharts.Color(Highcharts.getOptions().colors[0]).setOpacity(0).get('rgba')]
                     //        ]
                     //     },

                     //colors:['pink','blue'],



                     states: {
                         hover: {
                             marker: {
                                 enabled: false
                             }
                         }
                     },

                 }


             },





             series: [
                 {
                     name: '',
                     color: 'rgba(255,182,193,0.99)',
                     type: 'column',
                     tooltip: {
                         valueSuffix: ' ug/M3'
                         // valueSuffix: ' \xB0C'
                     },
                     yAxis: 2,
                 },
                 {
                     name: '',
                     color: 'rgba(144,238,144,0.99)',
                     //color: 'rgba(255,150,85,0.5)',
                     tooltip: {
                         valueSuffix: ' %RH'
                         //valueSuffix: ' \xB0C'
                     },
                     yAxis: 1,
                 },

                 {
                     name: '',
                     color: 'rgba(255,150,85,0.5)',
                     // color: 'rgba(144,238,144,0.99)',
                     //type: 'column',
                     tooltip: {
                         valueSuffix: ' \xB0C'
                         // valueSuffix: ' ug/M3'
                         //valueSuffix: ' %RH'
                     },
                     yAxis: 0,
                 },




             ],
         });

         window.setInterval(requestWSDData, 600000);
     }


     function requestWSDData() {
         $.getJSON('../Handler/ProcessHandler.ashx?action=l003wd', function (data) {

             if (data.hasError == true) {
                 alert(data.error);
                 return;
             }
             else {
                 initWSDChart();
                 d = JSON.parse(data.data);
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


                 if (chartWSD.series.length == 0) {
                     for (i in series) {
                         chartWSD.addSeries({
                             name: series[i].name,
                             data: series[i].data
                         });
                     }
                 } else {
                     chartWSD.update({
                         series: series
                     });
                 }




                 //var sl = chartMF.series.length;
                 //for (i = 0; i < sl; i++) {
                 //    var sname = chartMF.series[i].name;
                 //    var dd = [];
                 //    if (sname == "A1功率") {
                 //        dd = dA1;
                 //    }
                 //    if (sname == "B1功率") {
                 //        dd = dB1;
                 //    }
                 //    if (sname == "A2功率") {
                 //        dd = dA2;
                 //    }
                 //    if (sname == "B2功率") {
                 //        dd = dB2;
                 //    }
                 //    //var sd = chartMF.series[i].data.length;
                 //    //if (sd == 0) {
                 //    //    for (j in dd) {
                 //    //        //console.log(new Date(d[i].time));
                 //    //        //console.log(new Date(d[i].time).getTime());
                 //    //        var point = [new Date(dd[j].time).getTime(), dd[j].value];
                 //    //        chartMF.series[i].addPoint(point, true);
                 //    //    }

                 //    //}
                 //    //else {
                 //    //    lastPoint = chartMF.series[i].data[sd - 1];

                 //    //    if (lastPoint.x != new Date(dd[dd.length - 1].time).getTime()) {
                 //    //        chartMF.series[i].addPoint([new Date(dd[dd.length - 1].time).getTime(), dd[dd.length - 1].value], true, true);
                 //    //    }
                 //    //}
                 //}





             }
         });
     }










     //function current() {
     //    var d = new Date(), str = '';
     //    str += d.getFullYear() + '/'; //获取当前年份 
     //    str += d.getMonth() + 1 + '/'; //获取当前月份（0——11） 
     //    str += d.getDate() + ' ';
     //    str += d.getHours() + ':';
     //    str += d.getMinutes() + ':';
     //    str += d.getSeconds() + '';
     //    return str;
     //}
     //setInterval(function () {
     //    $("#timeDiv").html(current());
     //}, 1000);












 </script>
</asp:Content>
