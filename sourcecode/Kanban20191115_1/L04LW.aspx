<%@ Page Title="" Language="C#" MasterPageFile="~/KanbanSite.Master" AutoEventWireup="true" CodeBehind="L04LW.aspx.cs" Inherits="Kanban.L04LW" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <div class="logo">
            <%--<img src="img/sumin_log.JPG" style="width: 60px; height: 60px;" onclick="test();" />--%>
            <span style="font-size: 28px; color: white;">四号线炉温看板</span>
            <%--  <h1 style="/*margin-left:600px;*/">一号线生产看板</h1>--%>
            <span style="color: white;" id="timeDiv"></span>
            <button class="button-primary" onclick="prePage()">上一页</button>

            <button class="button-info" onclick="homePage()">回到首页</button>
            <button class="button-success" onclick="nextPage()">下一页</button>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4 col-sm-6">
            <div id="container1" style="min-width: 400px; height: 300px;"></div>
        </div>
        <div class="col-md-4 col-sm-6">
            <div id="container2" style="min-width: 400px; height: 300px;"></div>
        </div>
        <div class="col-md-4 col-sm-6">
            <div id="container3" style="min-width: 400px; height: 300px;"></div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4 col-sm-6">
            <div id="container4" style="min-width: 400px; height: 300px;"></div>
        </div>
        <div class="col-md-4 col-sm-6">
            <div id="container5" style="min-width: 400px; height: 300px;"></div>
        </div>
        <div class="col-md-4 col-sm-6">
            <div id="container6" style="min-width: 400px; height: 300px;"></div>
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

        function prePage() {
            window.location.href = "L04ENERGY1.aspx";
        }

        function nextPage() {
            window.location.href = "L04LY.aspx";
        }

        function homePage() {
            window.location.href = "Index1.aspx";
        }
        var chart1 = null;
        var chart2 = null;
        var chart3 = null;
        var chart4 = null;
        var chart5 = null;
        var chart6 = null;

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
            $("#timeDiv").html(current());
        }, 1000);
        init();
        function init() {
            requestData();
            setTimeout(function () {
                window.location.href = "L03LY.aspx";
            }, 60000);
        }


        function initChart1() {
            // 图表配置
            chart1 = Highcharts.chart('container1', {
                chart: {
                    type: 'column',//指定图表的类型，默认是折线图（line）
                },
                title: {
                    text: '一区炉温'                 // 标题
                },
                subtitle: {
                    text: '最近一小时工艺数据'
                },

                xAxis: {
                    type: 'datetime',
                    dateTimeLabelFormats: { minute: '%H:%M' }//,
                },
                yAxis: {
                    min: 100,
                    startOnTick: false,
                    title: {
                        text: '温度(℃)'                // y 轴标题
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

        function initChart2() {
            // 图表配置
            chart2 = Highcharts.chart('container2', {
                chart: {
                    type: 'column',//指定图表的类型，默认是折线图（line）
                },
                title: {
                    text: '二区温度'                 // 标题
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
                        text: '温度(℃)'                // y 轴标题
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

        function initChart3() {
            // 图表配置
            chart3 = Highcharts.chart('container3', {
                chart: {
                    type: 'column',//指定图表的类型，默认是折线图（line）
                },
                title: {
                    text: '三区温度'                 // 标题
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
                        text: '温度(℃)'                // y 轴标题
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

        function initChart4() {
            // 图表配置
            chart4 = Highcharts.chart('container4', {
                chart: {
                    type: 'column',//指定图表的类型，默认是折线图（line）
                },
                title: {
                    text: '四区温度'                 // 标题
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
                        text: '温度(℃)'                // y 轴标题
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


        function initChart5() {
            // 图表配置
            chart5 = Highcharts.chart('container5', {
                chart: {
                    type: 'column',//指定图表的类型，默认是折线图（line）
                },
                title: {
                    text: '五区温度'                 // 标题
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
                        text: '温度(℃)'                // y 轴标题
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


        function initChart6() {
            // 图表配置
            chart6 = Highcharts.chart('container6', {
                chart: {
                    type: 'column',//指定图表的类型，默认是折线图（line）
                },
                title: {
                    text: '六区温度'                 // 标题
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
                        text: '温度(℃)'                // y 轴标题
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
            $.getJSON('../Handler/ProcessHandler.ashx?action=l04lw', function (data) {
                if (data.hasError == true) {
                    alert(data.error);
                    return;
                }
                else {
                    //initFirstLine();
                    d = JSON.parse(data.data);
                    var names = [];
                    var series1 = [{ name: '设定值', data: [] }, { name: '实际值', data: [] }];
                    var series2 = [{ name: '设定值', data: [] }, { name: '实际值', data: [] }];
                    var series3 = [{ name: '设定值', data: [] }, { name: '实际值', data: [] }];
                    var series4 = [{ name: '设定值', data: [] }, { name: '实际值', data: [] }];
                    var series5 = [{ name: '设定值', data: [] }, { name: '实际值', data: [] }];
                    var series6 = [{ name: '设定值', data: [] }, { name: '实际值', data: [] }];
                    cs1 = [];
                    cs2 = [];
                    cs3 = [];
                    cs4 = [];
                    for (m in d) {
                        var name = d[m].SeriesName;
                        if (name == "1区温度") {
                            if (d[m].ParamType == "设定值") {
                                series1[0].data.push([new Date(d[m].time).getTime(), d[m].value]);
                            }
                            if (d[m].ParamType == "实际值") {
                                series1[1].data.push([new Date(d[m].time).getTime(), d[m].value]);
                            }
                        }
                        if (name == "2区温度") {
                            if (d[m].ParamType == "设定值") {
                                series2[0].data.push([new Date(d[m].time).getTime(), d[m].value]);
                            }
                            if (d[m].ParamType == "实际值") {
                                series2[1].data.push([new Date(d[m].time).getTime(), d[m].value]);
                            }
                        }
                        if (name == "3区温度") {
                            if (d[m].ParamType == "设定值") {
                                series3[0].data.push([new Date(d[m].time).getTime(), d[m].value]);
                            }
                            if (d[m].ParamType == "实际值") {
                                series3[1].data.push([new Date(d[m].time).getTime(), d[m].value]);
                            }
                        }
                        if (name == "4区温度") {
                            if (d[m].ParamType == "设定值") {
                                series4[0].data.push([new Date(d[m].time).getTime(), d[m].value]);
                            }
                            if (d[m].ParamType == "实际值") {
                                series4[1].data.push([new Date(d[m].time).getTime(), d[m].value]);
                            }
                        }
                        if (name == "5区温度") {
                            if (d[m].ParamType == "设定值") {
                                series5[0].data.push([new Date(d[m].time).getTime(), d[m].value]);
                            }
                            if (d[m].ParamType == "实际值") {
                                series5[1].data.push([new Date(d[m].time).getTime(), d[m].value]);
                            }
                        }
                        if (name == "6区温度") {
                            if (d[m].ParamType == "设定值") {
                                series6[0].data.push([new Date(d[m].time).getTime(), d[m].value]);
                            }
                            if (d[m].ParamType == "实际值") {
                                series6[1].data.push([new Date(d[m].time).getTime(), d[m].value]);
                            }
                        }
                        //var index = $.inArray(name, names);
                        //if (index == -1) {
                        //    names.push(name);
                        //    var s = {};
                        //    s.name = name;
                        //    s.data = [];
                        //    s.data.push([new Date(d[m].time).getTime(), d[m].value]);
                        //    series.push(s);
                        //}
                        //else {
                        //    series[index].data.push([new Date(d[m].time).getTime(), d[m].value]);
                        //}

                    }

                    initChart1();
                    initChart2();
                    initChart3();
                    initChart4();
                    initChart5();
                    initChart6();
                    if (chart1.series.length == 0) {
                        for (i in series1) {
                            chart1.addSeries({
                                name: series1[i].name,
                                data: series1[i].data
                            });
                        }
                    } else {
                        chart1.update({
                            series: series1
                        });
                    }

                    if (chart2.series.length == 0) {
                        for (i in series2) {
                            chart2.addSeries({
                                name: series2[i].name,
                                data: series2[i].data
                            });
                        }
                    } else {
                        chart2.update({
                            series: series2
                        });
                    }

                    if (chart3.series.length == 0) {
                        for (i in series3) {
                            chart3.addSeries({
                                name: series3[i].name,
                                data: series3[i].data
                            });
                        }
                    } else {
                        chart3.update({
                            series: series3
                        });
                    }

                    if (chart4.series.length == 0) {
                        for (i in series4) {
                            chart4.addSeries({
                                name: series4[i].name,
                                data: series4[i].data
                            });
                        }
                    } else {
                        chart4.update({
                            series: series4
                        });
                    }


                    if (chart5.series.length == 0) {
                        for (i in series5) {
                            chart5.addSeries({
                                name: series5[i].name,
                                data: series5[i].data
                            });
                        }
                    } else {
                        chart5.update({
                            series: series5
                        });
                    }

                    if (chart6.series.length == 0) {
                        for (i in series6) {
                            chart6.addSeries({
                                name: series6[i].name,
                                data: series6[i].data
                            });
                        }
                    } else {
                        chart6.update({
                            series: series6
                        });
                    }
                }
            });
        }




    </script>
</asp:Content>
