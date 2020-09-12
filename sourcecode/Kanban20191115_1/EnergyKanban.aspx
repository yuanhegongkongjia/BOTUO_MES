<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnergyKanban.aspx.cs" Inherits="Kanban.EnergyKanban" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>能源看板</title>
    <script type="text/javascript" src="Scripts/jquery-3.3.1.js"></script>
    <script type="text/javascript" src="bui/bui.js"></script>
    <link href="bui/css/dpl.css" rel="stylesheet" type="text/css" />
    <link href="bui/css/bui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="chart/highcharts.js?time=1"></script>
</head>
<body>
    <div class="page-header">
        <h1>能源看板</h1>
    </div>
    <div class="row" style="margin-top: 10px;">
        <div class="span16 offset1">
            <div id="container" style="min-width: 400px; height: 400px;"></div>
        </div>

        <div class="span16">
            <div id="container1" style="min-width: 400px; height: 400px;"></div>
        </div>
    </div>
    <div class="row" style="margin-top: 10px;">
        <div class="span16 offset1">
            <div id="container2" style="min-width: 400px; height: 400px;"></div>
        </div>

        <div class="span16">
            <div id="container3" style="min-width: 400px; height: 400px;"></div>
        </div>
    </div>
    <div class="row" style="margin-top: 10px;">
        <div class="span23 offset1">
            <div id="container4" style="min-width: 400px; height: 400px;"></div>
        </div>
    </div>
    <script>
        initPowerChart();
        initChunAndZilaiShui();
        initTianRanQi();

        initZhengQi();
        initYanJianLin();
        function initPowerChart() {
            var chart = Highcharts.chart('container', {
                chart: {
                    type: 'bar',
                    events: {
                        load: requestPowerData // 图表加载完毕后执行的回调函数
                    }


                },
                title: {
                    text: '电能使用情况'
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
                        text: '电能(KWS)',
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

        function requestPowerData() {

            $.getJSON('../Handler/EnergyHandler.ashx?action=getpower', function (data) {
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

        function initChunAndZilaiShui() {
            var chart = Highcharts.chart('container1', {
                chart: {
                    type: 'column',
                    events: {
                        load: requestChunAndZiLaiShui
                    }
                },
                title: {
                    text: '自来水纯水用量'
                },
                xAxis: {
                    categories: ['自来水', '纯水']
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: '自来水/纯水消耗量'
                    },
                    stackLabels: {  // 堆叠数据标签
                        enabled: true,
                        style: {
                            fontWeight: 'bold',
                            color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                        }
                    }
                },
                legend: {
                    align: 'right',
                    x: -30,
                    verticalAlign: 'top',
                    y: 25,
                    floating: false,
                    backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
                    borderColor: '#CCC',
                    borderWidth: 1,
                    shadow: false
                },
                tooltip: {
                    formatter: function () {
                        return '<b>' + this.x + '</b><br/>' +
                            this.series.name + ': ' + this.y + '<br/>' +
                            '总量: ' + this.point.stackTotal;
                    }
                },
                plotOptions: {
                    column: {
                        stacking: 'normal',
                        dataLabels: {
                            enabled: true,
                            color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white',
                            style: {
                                // 如果不需要数据标签阴影，可以将 textOutline 设置为 'none'
                                textOutline: '1px 1px black'
                            }
                        }
                    }
                },
                //series: [{
                //    name: '小张',
                //    data: [5, 3, 4, 7, 2]
                //}, {
                //    name: '小彭',
                //    data: [2, 2, 3, 2, 1]
                //}, {
                //    name: '小潘',
                //    data: [3, 4, 4, 2, 5]
                //}]
            });
        }

        function requestChunAndZiLaiShui() {
            $.getJSON('../Handler/EnergyHandler.ashx?action=getChunAndZiLaiShui', function (data) {
                if (data.hasError == true) {
                    alert(data.error);
                    return;
                }
                else {
                    d = JSON.parse(data.data);
                    var sd = Highcharts.charts[1].series.length;
                    if (sd == 0) {
                        for (i in d) {
                            var s = {
                                name: d[i].name,
                                data: d[i].data
                            };
                           
                            Highcharts.charts[1].addSeries(s);
                        }

                    }

                }
            });

        }

        function initYanJianLin() {
            var chart = Highcharts.chart('container4', {
                chart: {
                    type: 'column',
                    events: {
                        load: requestYanJianLin
                    }
                },
                title: {
                    text: '盐酸、碱、磷酸用量'
                },
                xAxis: {
                    categories: ['盐酸', '碱','磷酸']
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: '盐酸、碱、磷酸消耗量'
                    },
                    stackLabels: {  // 堆叠数据标签
                        enabled: true,
                        style: {
                            fontWeight: 'bold',
                            color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                        }
                    }
                },
                legend: {
                    align: 'right',
                    x: -30,
                    verticalAlign: 'top',
                    y: 25,
                    floating: false,
                    backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
                    borderColor: '#CCC',
                    borderWidth: 1,
                    shadow: false
                },
                tooltip: {
                    formatter: function () {
                        return '<b>' + this.x + '</b><br/>' +
                            this.series.name + ': ' + this.y + '<br/>' +
                            '总量: ' + this.point.stackTotal;
                    }
                },
                plotOptions: {
                    column: {
                        stacking: 'normal',
                        dataLabels: {
                            enabled: true,
                            color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white',
                            style: {
                                // 如果不需要数据标签阴影，可以将 textOutline 设置为 'none'
                                textOutline: '1px 1px black'
                            }
                        }
                    }
                },
                //series: [{
                //    name: '小张',
                //    data: [5, 3, 4, 7, 2]
                //}, {
                //    name: '小彭',
                //    data: [2, 2, 3, 2, 1]
                //}, {
                //    name: '小潘',
                //    data: [3, 4, 4, 2, 5]
                //}]
            });
        }

        function requestYanJianLin() {
            $.getJSON('../Handler/EnergyHandler.ashx?action=getYanJianLin', function (data) {
                if (data.hasError == true) {
                    alert(data.error);
                    return;
                }
                else {
                    d = JSON.parse(data.data);
                    var sd = Highcharts.charts[4].series.length;
                    if (sd == 0) {
                        for (i in d) {
                            Highcharts.charts[4].addSeries({
                                name: d[i].name,
                                data: d[i].data
                            });
                        }

                    }

                }
            });

        }

        function initTianRanQi() {
            var chart = Highcharts.chart('container2', {

                chart: {
                    type: 'column',
                    events: {
                        load: requestTianRanQi
                    }
                },
                title: {
                    text: '天然气耗用量'
                    // 该功能依赖 data.js 模块，详见https://www.hcharts.cn/docs/data-modules
                },
                xAxis: {
                    categories: ['L01', 'L02', 'L03', 'L04']
                },
                yAxis: {
                    allowDecimals: false,
                    title: {
                        text: '立方米',
                        rotation: 0
                    }
                },
                tooltip: {
                    formatter: function () {
                        return '<b>' + this.series.name + '</b><br/>' +
                            this.point.y + ' 立方米' ;
                    }
                }
            });
        }

        function requestTianRanQi() {
            $.getJSON('../Handler/EnergyHandler.ashx?action=getTianRanQi', function (data) {
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

        function initZhengQi() {
            var chart = Highcharts.chart('container3', {

                chart: {
                    type: 'column',
                    events: {
                        load: requestZhengQi
                    }
                },
                title: {
                    text: '蒸汽耗用量'
                    // 该功能依赖 data.js 模块，详见https://www.hcharts.cn/docs/data-modules
                },
                xAxis: {
                    categories: ['L01', 'L02', 'L03', 'L04']
                },
                yAxis: {
                    allowDecimals: false,
                    title: {
                        text: '立方米',
                        rotation: 0
                    }
                },
                tooltip: {
                    formatter: function () {
                        return '<b>' + this.series.name + '</b><br/>' +
                            this.point.y + ' 立方米' ;
                    }
                }
            });
        }

        function requestZhengQi() {
            $.getJSON('../Handler/EnergyHandler.ashx?action=gettZhengQi', function (data) {
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
</body>
</html>
