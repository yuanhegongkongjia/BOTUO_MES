<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="L01Kanban.aspx.cs" Inherits="Kanban.L01Kanban" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>一号生产线工艺看板</title>
    <script type="text/javascript" src="Scripts/jquery-3.3.1.js"></script>
    <script type="text/javascript" src="bui/bui.js"></script>
    <link href="bui/css/dpl.css" rel="stylesheet" type="text/css" />
    <link href="bui/css/bui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="chart/highcharts.js?time=1"></script>
</head>
<body style="background-color:#293c55;">
    <div class="page-header">
        <div class="logo">
            <img src="img/sumin_log.JPG" style="width:60px;height:60px;" />
            <span style="font-size:28px;color:white;">一号线MF功率看板</span>
         <span style="color: white;" id="timeDiv"></span>
            <button class="button-primary" onclick="prePage()">上一页</button>
           
            <button class="button-info" onclick="homePage()">回到首页</button>
            <button class="button-success" onclick="nextPage()">下一页</button>
        </div>
    </div>
    <div class="row" style="margin-top: 10px; margin-left: 20px;" id="row1">
        <div class="span12">
            <div id="container" style="min-width: 400px; height: 400px;"></div>
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
            window.location.href = "L01LY.aspx";
        }

        function nextPage() {
            window.location.href = "L01MFUA.aspx";
        }

        function homePage() {
            window.location.href = "Index1.aspx";
        }



        //load();
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
            //setTimeout(function () {
            //    init2();
               
            //    //initFirstLine1();
            //}, 60000);

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
                },
                title: {
                    text: 'MF功率'                 // 标题
                },
                subtitle: {
                    text: '最近一小时工艺数据'
                },

                xAxis: {
                    type: 'datetime',
                    dateTimeLabelFormats: { minute: '%H:%M' }//,
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
                            enabled: true
                        }
                    }
                }
            });

        }

        function requestData() {
            $.getJSON('../Handler/ProcessHandler.ashx?action=l01mf', function (data) {
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


                        //if (d[m].SeriesName == "A1功率") {
                        //    dA1.push(d[m]);
                        //}
                        //if (d[m].SeriesName == "B1功率") {
                        //    dB1.push(d[m]);
                        //}
                        //if (d[m].SeriesName == "A2功率") {
                        //    dA2.push(d[m]);
                        //}
                        //if (d[m].SeriesName == "B2功率") {
                        //    dB2.push(d[m]);
                        //}
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

        function initFirstLine1() {
            // 图表配置
            chartSpeed = Highcharts.chart('container1', {
                chart: {
                    type: 'line',//指定图表的类型，默认是折线图（line）
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
            $.getJSON('../Handler/ProcessHandler.ashx?action=l01productspeed', function (data) {
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
                            s.data.push([new Date(d[m].time).getTime(),d[m].value]);
                            series.push(s);
                        }
                        else {
                            series[index].data.push([new Date(d[m].time).getTime(), d[m].value]);
                        }
                    }

                    if (chartSpeed.series.length == 0) {
                        for (i in series) {
                            chartSpeed.addSeries({
                                name:series[i].name,
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
            $.getJSON('../Handler/ProcessHandler.ashx?action=l01lw', function (data) {
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
