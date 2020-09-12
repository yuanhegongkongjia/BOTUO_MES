<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="L02Kanban.aspx.cs" Inherits="Kanban.L02Kanban" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="mobile-web-app-capable" content="yes" />
    <title>二号生产线工艺看板</title>
    <script type="text/javascript" src="Scripts/jquery-3.3.1.js"></script>
    <script type="text/javascript" src="bui/bui.js"></script>
    <link href="bui/css/dpl.css" rel="stylesheet" type="text/css" />
    <link href="bui/css/bui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="chart/highcharts.js?time=1"></script>
</head>
<body>
    <div class="page-header">
        <div class="logo">
            <img src="img/sumin_log.JPG" style="width:60px;height:60px;" onclick="test();"/>
            <span style="font-size:28px;color:darkblue;">二号线生产看板</span>
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

        function test() {
            requestFullScreen();
            //5秒钟自动退出全屏
            //setTimeout(function () {
            //    exitFullscreen();
            //}, 5000);
        }
        function hideAddressBar_ios() {
            if (document.documentElement.scrollHeight <= document.documentElement.clientHeight) {
                bodyTag = document.getElementsByTagName('body')[0];
                bodyTag.style.height = document.documentElement.clientWidth / screen.width * screen.height + 'px';
            }
            setTimeout(function () {
                window.scrollTo(0, 1);
            }, 100);
        };
        function hideAddressBar_android() {
            var n = navigator.userAgent;
            if (n.match(/UCBrowser/i)) {
                //uc浏览器
                hideAddressBar_ios();
                return false;
            }
            var self = document.getElementsByTagName('body')[0];
            if (self.requestFullscreen) {
                self.requestFullscreen();
            } else if (self.mozRequestFullScreen) {
                self.mozRequestFullScreen();
            } else if (self.webkitRequestFullScreen) {
                self.webkitRequestFullScreen();
            }
        };
        //window.addEventListener("load", function () {
        //    navigator.userAgent.match(/Android/i) ? hideAddressBar_android() : hideAddressBar_ios();
        //});


        //进入全屏
        function requestFullScreen() {
            var de = document.documentElement;
            if (de.requestFullscreen) {
                de.requestFullscreen();
            } else if (de.mozRequestFullScreen) {
                de.mozRequestFullScreen();
            } else if (de.webkitRequestFullScreen) {
                de.webkitRequestFullScreen();
            }
        }
        //退出全屏
        function exitFullscreen() {
            var de = document;
            if (de.exitFullscreen) {
                de.exitFullscreen();
            } else if (de.mozCancelFullScreen) {
                de.mozCancelFullScreen();
            } else if (de.webkitCancelFullScreen) {
                de.webkitCancelFullScreen();
            }
        }
        
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
             $.getJSON('../Handler/ProcessHandler.ashx?action=l02mf', function (data) {
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
             $.getJSON('../Handler/ProcessHandler.ashx?action=l02productspeed', function (data) {
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
             $.getJSON('../Handler/ProcessHandler.ashx?action=l02lw', function (data) {
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

                 

                 }
             });





         }
    </script>
</body>
</html>
