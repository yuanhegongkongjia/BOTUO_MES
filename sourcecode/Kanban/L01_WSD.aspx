<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="L01_WSD.aspx.cs" Inherits="Kanban.L01_WSD" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>车间温湿度环境情况看板</title>
    <!-- 引入 echarts.js -->
    <script type="text/javascript" src="Scripts/jquery-3.3.1.js"></script>
    <script type="text/javascript" src="bui/bui.js"></script>
    <link href="bui/css/dpl.css" rel="stylesheet" type="text/css"  />
    <link href="bui/css/bui.css" rel="stylesheet" type="text/css"  />
    <link rel="stylesheet" href="assets/css/LookBand.css"/>
    <script type="text/javascript" src="chart/highcharts.js?time=1"></script>
    <style>
    body { 

        background-color:rgba(70,130,180,0.6);

      }
   </style>
</head>

<body style="background-color:#040f3c;">
   
    <div class="header">
    <div class="a-access" style="width:100%">
    <img src="img/BTLOG.png" style="width:48px;height:48px;margin-left: 20px; top:1%; left:5%;" />
    
    <span style="color: #00ffff;width:200px;font-size: 35px;text-align:center;" >博拓车间环境监控看板</span>
       
     <span style="color: #00ffff;width:200px;font-size: 16px;position: absolute; top:25%; right:3%;" id="currentTime">2016-12-21 上午13:00:00</span>
            
     </div>
     </div>
    
      <div class="main clearfix">
        <div class="main-left">
            <div class="border-container">
                <div class="name-title">环境监控-东北角</div>
                <div id="container"></div>
                <span class="top-left border-span"></span>
                <span class="top-right border-span"></span>
                <span class="bottom-left border-span"></span>
                <span class="bottom-right border-span"></span>
            </div> 
            <div class="border-container">
                <div class="name-title">环境监控-西北角</div>
                <div id="container02"></div>
                <span class="top-left border-span"></span>
                <span class="top-right border-span"></span>
                <span class="bottom-left border-span"></span>
                <span class="bottom-right border-span"></span>
            </div>
        </div>
       <div class="main-right">
            <div class="border-container">
                <div class="name-title">环境监控-西南角</div>
                <div id="container03"></div>
                <span class="top-left border-span"></span>
                <span class="top-right border-span"></span>
                <span class="bottom-left border-span"></span>
                <span class="bottom-right border-span"></span>
            </div> 
            <div class="border-container">
                <div class="name-title">环境监控-东南角</div>
                <div id="container05"></div>
                <span class="top-left border-span"></span>
                <span class="top-right border-span"></span>
                <span class="bottom-left border-span"></span>
                <span class="bottom-right border-span"></span>
            </div>
        </div>

      </div>



   
    <script>
        //基于准备好的dom，初始化echarts实例
        Highcharts.setOptions({
            chart: {
                backgroundColor: {
                    //linearGradient: [0, 0, 100, 100],
                    stops: [
                        [0, 'rgb(255, 255, 255)'],
                       // [1, 'rgb(70,130,180)']
                        [1,'#293c55']
                    ]
                },
                //borderWidth: 2,
                //plotBackgroundColor: 'rgba(70,130,180, .2)',
                //plotBackgroundColor: '#293c55',
                //plotShadow: true,
                //plotBorderWidth: 1
            },
            global: { useUTC: false },
            marginBottom:-5
        });

        // 指定图表的配置项和数据
        requestDeviceData();

        function initDeviceChart() {
            chartWD = Highcharts.chart('container', {
                chart: {
                    type: 'areaspline',
                    zoomType: 'xy'
                },
                title: {
                    
                    text: '',
                },
                //subtitle: {
                //    text: '数据来源:车间温湿度采集器一'
                //},
                xAxis: {
                    title: {
                        text: '时间',

                        style: {
                            color: 'white'
                        },


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
                yAxis: 
                    [

                        { // 第一条Y轴
                            labels: {
                                format: '{value}\xB0C',
                                style: {
                                    color: 'rgba(255,69,0,0.99)'
                                },
                                x: 5,
                                y: 0
                            },
                            title: {
                                text: '温度',
                                style: {
                                    color: ' white'
                                },
                                margin: 5
                            },
                            gridLineColor: 'grey',
                            gridLineWidth: 0,
                        }, { // 第二条Y轴
                            title: {
                                text: '湿度',
                                style: {
                                    color: ' white'
                                },
                                margin: 5
                            },
                            labels: {
                                format: '{value}%RH ',
                                style: {
                                    //color: Highcharts.requestDeviceData().colors[2]
                                    color: 'rgba(173,255,47,0.99)'
                                },
                                x: -10,
                                y: 0
                            },
                            gridLineColor: 'grey',
                            gridLineWidth: 0,
                           // labels:{
                           //     Number:3
                           //},
                           
                            opposite: true
                        },
                        { // 第二条Y轴
                            title: {
                                text: 'PM2.5',
                                style: {
                                    color: 'white'
                                }, 
                                 margin: 5
                               
                            },
                            labels: {
                                format: '{value} ',
                                style: {
                                    color: 'rgba(100,149,237,0.5)'
                                },
                                x: -10,
                                y: 0
                            },
                            gridLineColor: 'grey',
                            gridLineWidth: 0,
                            //labels: {
                            //    Number: 3
                            //},
                            
                            opposite: true
                        }
                    ],
           
                tooltip: {
                    shared: true
                },
                colors: [
                    'rgba(255,69,0,0.99)', 'rgba(173,255,47,0.99)', 'rgba(100,149,237,0.5)', '#FFF263', '#6AF9C4'],
                legend: {
                    //layout: 'vertical',
                    //layout:'horizontal',
                    ////type:'scroll',
                    align: 'top',
                    verticalAlign: 'top',
                    x: 80,
                    y: -15,
                    itemStyle: {
                        color: 'white',
                       
                    },
                    //floating: false,
                    //backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColor) || 'rgba(173,216,230, .2)',
                    //borderWidth: 1
                    //itemMarginTop: 1,
                    //itemMarginBottom: 1
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





                series: [
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
                   
                   
                    {
                    name: 'PM2.5',
                    type: 'column',
                    tooltip: {
                        valueSuffix: ' ug/M3'
                    },
                    yAxis: 2
                },
                


                ],
            });

            window.setInterval(requestDeviceData, 600000);
        }


        function requestDeviceData() {
            $.getJSON('../Handler/ProcessHandler.ashx?action=1002wd', function (data) {

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
                        var name = d[m].ParamType.slice(3);
                        //var a = name;
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
            chartWSD = Highcharts.chart('container01', {
                chart: {
                    type: 'areaspline',
                    zoomType: 'xy'
                },
                title: {
                    text: ''
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
            $.getJSON('../Handler/ProcessHandler.ashx?action=1002wde', function (data) {

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
                        var name = d[m].ParamType.slice(3);

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

        requestDeviceData02();

        function initDevice02Chart() {
            chartWD02 = Highcharts.chart('container02', {
                chart: {
                    type: 'areaspline',
                    zoomType: 'xy'
                },
                title: {
                    text: ''
                },
                //subtitle: {
                //    text: '数据来源:车间温湿度采集器一'
                //},
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
                yAxis:
                    [

                        { // 第一条Y轴
                            labels: {
                                format: '{value}\xB0C',
                                style: {
                                    color: 'rgba(255,69,0,0.99)'
                                },
                                x: 5,
                                y: 0
                            },
                            title: {
                                text: '温度',
                                style: {
                                    color: ' white'
                                },
                                margin: 5
                            },
                            gridLineColor: 'grey',
                            gridLineWidth: 0,
                        }, { // 第二条Y轴
                            title: {
                                text: '湿度',
                                style: {
                                    color: ' white'
                                },
                                margin: 5
                            },
                            labels: {
                                format: '{value}%RH ',
                                style: {
                                    //color: Highcharts.requestDeviceData().colors[2]
                                    color: 'rgba(173,255,47,0.99)'
                                },
                                x: -10,
                                y: 0
                            },
                            gridLineColor: 'grey',
                            gridLineWidth: 0,
                            // labels:{
                            //     Number:3
                            //},

                            opposite: true
                        },
                        { // 第二条Y轴
                            title: {
                                text: 'PM2.5',
                                style: {
                                    color: 'white'
                                },
                                margin: 5

                            },
                            labels: {
                                format: '{value} ',
                                style: {
                                    color: 'rgba(100,149,237,0.5)'
                                },
                                x: -10,
                                y: 0
                            },
                            gridLineColor: 'grey',
                            gridLineWidth: 0,
                            //labels: {
                            //    Number: 3
                            //},

                            opposite: true
                        }
                    ],

                tooltip: {
                    shared: true
                },
                colors: [
                    'rgba(255,69,0,0.99)', 'rgba(173,255,47,0.99)', 'rgba(100,149,237,0.5)', '#FFF263', '#6AF9C4'],
                legend: {
                    //layout: 'vertical',
                    //align: 'top',
                    //verticalAlign: 'top',
                    //x: 445,
                    //y: -4,
                    //floating: false,
                    align: 'top',
                    verticalAlign: 'top',
                    x: 80,
                    y: -15,
                    itemStyle: {
                        color: 'white',

                    },
                    //backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColor) || 'rgba(173,216,230, .2)',
                    //borderWidth: 1
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





                series: [
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
                  
                    
                    {
                        name: 'PM2.5',
                        type: 'column',
                        tooltip: {
                            valueSuffix: ' ug/M3'
                        },
                        yAxis: 2
                    },


                ],
            });

            window.setInterval(requestDeviceData02, 600000);
        }


        function requestDeviceData02() {
            $.getJSON('../Handler/ProcessHandler.ashx?action=1013sde', function (data) {

                if (data.hasError == true) {
                    alert(data.error);
                    return;
                }
                else {
                    initDevice02Chart();
                    d = JSON.parse(data.data);
                    var names = [];
                    var series = [];
                    for (m in d) {
                        var name = d[m].ParamType.slice(3);

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


                    if (chartWD02.series.length == 0) {
                        for (i in series) {
                            chartWD02.addSeries({
                                name: series[i].name,
                                data: series[i].data
                            });
                        }
                    } else {
                        chartWD02.update({
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

        requestDeviceData05();

        function initDevice05Chart() {
            chartWD05 = Highcharts.chart('container05', {
                chart: {
                    //type: 'areaspline',
                    type: 'line',
                    zoomType: 'xy'
                },
                title: {
                    text: ''
                },
                //subtitle: {
                //    text: '数据来源:车间温湿度采集器一'
                //},
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
                yAxis:
                    [

                        { // 第一条Y轴
                            labels: {
                                format: '{value}\xB0C',
                                style: {
                                    color: 'rgba(255,69,0,0.99)'
                                },
                                x: 5,
                                y: 0
                            },
                            title: {
                                text: '温度',
                                style: {
                                    color: ' white'
                                },
                                margin: 5
                            },
                            gridLineColor: 'grey',
                            gridLineWidth: 0,
                        }, { // 第二条Y轴
                            title: {
                                text: '湿度',
                                style: {
                                    color: ' white'
                                },
                                margin: 5
                            },
                            labels: {
                                format: '{value}%RH ',
                                style: {
                                    //color: Highcharts.requestDeviceData().colors[2]
                                    color: 'rgba(173,255,47,0.99)'
                                },
                                x: -10,
                                y: 0
                            },
                            gridLineColor: 'grey',
                            gridLineWidth: 0,
                            // labels:{
                            //     Number:3
                            //},

                            opposite: true
                        },
                        { // 第二条Y轴
                            title: {
                                text: 'PM2.5',
                                style: {
                                    color: 'white'
                                },
                                margin: 5

                            },
                            labels: {
                                format: '{value} ',
                                style: {
                                    color: 'rgba(100,149,237,0.5)'
                                },
                                x: -10,
                                y: 0
                            },
                            gridLineColor: 'grey',
                            gridLineWidth: 0,
                            //labels: {
                            //    Number: 3
                            //},

                            opposite: true
                        }
                    ],

                tooltip: {
                    shared: true
                },
                colors: [
                    'rgba(255,69,0,0.99)', 'rgba(173,255,47,0.99)', 'rgba(100,149,237,0.5)', '#FFF263', '#6AF9C4'],
                legend: {
                    //layout: 'vertical',
                    //align: 'top',
                    //verticalAlign: 'top',
                    //x: 445,
                    //y: -4,
                    //floating: false,
                    align: 'top',
                    verticalAlign: 'top',
                    x: 80,
                    y: -15,
                    itemStyle: {
                        color: 'white',

                    },
                    //backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColor) || 'rgba(173,216,230, .2)',
                    //borderWidth: 1
                },
                //plotOptions: {
                //    //scatter: {
                //    //    marker: {
                //    //        radius: 5,
                //    //        states: {
                //    //            hover: {
                //    //                enabled: true,
                //    //                lineColor: 'rgb(100,100,100)'
                //    //            }
                //    //        }
                //    //    },


                //    areaspline:
                //    {
                //        //dataLabels: {

                //        //    enabled: true,
                //        //    allowOverlap: true
                //        // },
                //        fillOpacity: 0.3,
                //        //fillColor: {
                //        ////        colors:['red','blue']
                //        ////        linearGradient: { x1: 0, y1: 0, x2: 0, y2: 0.9 },
                //        //          // colors: rgba(255,182,193,0.1),

                //        ////        stops: [
                //        ////            [0, Highcharts.getOptions().colors[0]],
                //        ////            [1, Highcharts.Color(Highcharts.getOptions().colors[0]).setOpacity(0).get('rgba')]
                //        ////        ]
                //        //    },

                //        //colors:['pink','blue'],



                //        states: {
                //            hover: {
                //                marker: {
                //                    enabled: false
                //                }
                //            }
                //        },

                //    }


                //},





                series: [
                    
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
                    {
                        name: 'PM2.5',
                        type: 'column',
                        tooltip: {
                            valueSuffix: ' ug/M3'
                        },
                        yAxis: 2
                    },

                ],
            });

            window.setInterval(requestDeviceData02, 600000);
        }


        function requestDeviceData05() {
            $.getJSON('../Handler/ProcessHandler.ashx?action=1002wde', function (data) {

                if (data.hasError == true) {
                    alert(data.error);
                    return;
                }
                else {
                    initDevice05Chart();
                    d = JSON.parse(data.data);
                    var names = [];
                    var series = [];
                    for (m in d) {
                        var name = d[m].ParamType.slice(3);

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


                    if (chartWD05.series.length == 0) {
                        for (i in series) {
                            chartWD05.addSeries({
                                name: series[i].name,
                                data: series[i].data
                            });
                        }
                    } else {
                        chartWD05.update({
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


        requestDeviceData03();

        function initDevice03Chart() {
            chartWD03 = Highcharts.chart('container03', {
                chart: {
                    type: 'areaspline',
                    zoomType: 'xy'
                },
                title: {
                    text: ''
                },
                //subtitle: {
                //    text: '数据来源:车间温湿度采集器一'
                //},
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
                yAxis:
                    [

                        { // 第一条Y轴
                            labels: {
                                format: '{value}\xB0C',
                                style: {
                                    color: 'rgba(255,69,0,0.99)'
                                },
                                x: 5,
                                y: 0
                            },
                            title: {
                                text: '温度',
                                style: {
                                    color: ' white'
                                },
                                margin: 5
                            },
                            gridLineColor: 'grey',
                            gridLineWidth: 0,
                        }, { // 第二条Y轴
                            title: {
                                text: '湿度',
                                style: {
                                    color: ' white'
                                },
                                margin: 5
                            },
                            labels: {
                                format: '{value}%RH ',
                                style: {
                                    //color: Highcharts.requestDeviceData().colors[2]
                                    color: 'rgba(173,255,47,0.99)'
                                },
                                x: -10,
                                y: 0
                            },
                            gridLineColor: 'grey',
                            gridLineWidth: 0,
                            // labels:{
                            //     Number:3
                            //},

                            opposite: true
                        },
                        { // 第二条Y轴
                            title: {
                                text: 'PM2.5',
                                style: {
                                    color: 'white'
                                },
                                margin: 5

                            },
                            labels: {
                                format: '{value} ',
                                style: {
                                    color: 'rgba(100,149,237,0.5)'
                                },
                                x: -10,
                                y: 0
                            },
                            gridLineColor: 'grey',
                            gridLineWidth: 0,
                            //labels: {
                            //    Number: 3
                            //},

                            opposite: true
                        }
                    ],

                tooltip: {
                    shared: true
                },
                colors: [
                    'rgba(255,69,0,0.99)', 'rgba(173,255,47,0.99)', 'rgba(100,149,237,0.5)', '#FFF263', '#6AF9C4'],
                legend: {
                    //layout: 'vertical',
                    //align: 'top',
                    //verticalAlign: 'top',
                    //x: 445,
                    //y: -4,
                    //floating: false,
                    align: 'top',
                    verticalAlign: 'top',
                    x: 80,
                    y: -15,
                    itemStyle: {
                        color: 'white',

                    },
                    //backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColor) || 'rgba(173,216,230, .2)',
                    //borderWidth: 1
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





                series: [
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
                  
                    {
                        name: 'PM2.5',
                        type: 'column',
                        tooltip: {
                            valueSuffix: ' ug/M3'
                        },
                        yAxis: 2
                    },


                ],
            });

            window.setInterval(requestDeviceData02, 600000);
        }


        function requestDeviceData03() {
            $.getJSON('../Handler/ProcessHandler.ashx?action=1013sd', function (data) {

                if (data.hasError == true) {
                    alert(data.error);
                    return;
                }
                else {
                    initDevice03Chart();
                    d = JSON.parse(data.data);
                    var names = [];
                    var series = [];
                    for (m in d) {
                        var name = d[m].ParamType.slice(3);

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


                    if (chartWD03.series.length == 0) {
                        for (i in series) {
                            chartWD03.addSeries({
                                name: series[i].name,
                                data: series[i].data
                            });
                        }
                    } else {
                        chartWD03.update({
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

        function fnDate() {
            var oDiv = document.getElementById("currentTime");
            var date = new Date();
            var year = date.getFullYear();
            var month = date.getMonth();
            var data = date.getDate();
            var hours = date.getHours();
            var minute = date.getMinutes();
            var second = date.getSeconds();
            var time = year + "-" + fnW((month + 1)) + "-" + fnW(data) + " " + fnW(hours) + ":" + fnW(minute) + ":" + fnW(second);
            var a = new Array("日", "一", "二", "三", "四", "五", "六");
            var week = new Date().getDay();
            var str = "周" + a[week];
            oDiv.innerHTML = time + "　" + str;
        }
        //补位 当某个字段不是两位数时补0
        function fnW(str) { return str > 9 ? str : "0" + str; }

        setInterval(function () {
            fnDate();
        }, 1000);

    </script>
</body>
</html>
   