<%@ Page Title="" Language="C#" MasterPageFile="~/KanbanSite.Master" AutoEventWireup="true" CodeBehind="L04LY.aspx.cs" Inherits="Kanban.L04LY" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="page-header">
        <div class="logo">
            <%--<img src="img/sumin_log.JPG" style="width: 60px; height: 60px;" onclick="test();" />--%>
            <span style="font-size: 28px; color: white;">四号线炉压看板</span>
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
    <div class="row" style="margin-top: 10px;">
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
            //$("#timeDiv").html( "123343");
            $("#timeDiv").html(current());
        }, 1000);


        function prePage() {
            window.location.href = "L04LW.aspx";
        }

        function nextPage() {
            window.location.href = "L04MFUA.aspx";
        }

        function homePage() {
            window.location.href = "Index1.aspx";
        }

        init();
        function init() {
            requestData();
            setTimeout(function () {
                window.location.href = "L04MFUA.aspx";
            }, 60000);
        }

        //requestData();

        function initChart1() {
            // 图表配置
            chart1 = Highcharts.chart('container1', {
                chart: {
                    type: 'column',//指定图表的类型，默认是折线图（line）
                },
                title: {
                    text: '1区炉压'                 // 标题
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
                        text: '帕斯卡(Pa)'                // y 轴标题
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
                    text: '2区炉压'                 // 标题
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
                        text: '帕斯卡(Pa)'                // y 轴标题
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
                    text: '3区炉压'                 // 标题
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
                        text: '帕斯卡(Pa)'                // y 轴标题
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
                    text: '4区炉压'                 // 标题
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
                        text: '帕斯卡(Pa)'                // y 轴标题
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
                    text: '5区炉压'                 // 标题
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
                        text: '帕斯卡(Pa)'                // y 轴标题
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
                    text: '6区炉压'                 // 标题
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
                        text: '帕斯卡(Pa)'                // y 轴标题
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
            $.getJSON('../Handler/ProcessHandler.ashx?action=l04ly', function (data) {
                if (data.hasError == true) {
                    alert(data.error);
                    return;
                }
                else {
                    //initFirstLine();
                    d = JSON.parse(data.data);
                    var names = [];
                    var series1 = [{ name: '', data: [] }];
                    
                    cs1 = [];
                    cs2 = [];
                    cs3 = [];
                    cs4 = [];
                    for (m in d) {
                        var name = d[m].SeriesName;
                        if (name == "1楼1#电表") {
                           
                                series1[0].data.push([new Date(d[m].time).getTime(), d[m].value]);
                            } 
                    }
                    initChart1();
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
                }
            });
        }


    </script>
</asp:Content>
