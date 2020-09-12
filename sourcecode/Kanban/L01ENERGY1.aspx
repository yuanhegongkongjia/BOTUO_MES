<%@ Page Title="" Language="C#" MasterPageFile="~/KanbanSite.Master" AutoEventWireup="true" CodeBehind="L01ENERGY1.aspx.cs" Inherits="Kanban.WebForm4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <div class="logo">
            <%--<img src="img/sumin_log.JPG" style="width: 60px; height: 60px;" onclick="test();" />--%>
            <span style="font-size: 28px; color: white;" onclick="requestFullScreen();" id="span1">一号线炉压/能源看板</span>
            <%--  <h1 style="/*margin-left:600px;*/">一号线生产看板</h1>--%>
            <span style="color: white;width:200px;" id="timeDiv"></span>
            <button class="button-primary" onclick="prePage()">上一页</button>

            <button class="button-info" onclick="homePage()">回到首页</button>
            <button class="button-success" onclick="nextPage()">下一页</button>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6 col-sm-12">
            <div id="container1" style="min-width: 400px; height: 280px;"></div>
        </div>
        <div class="col-md-6 col-sm-12">
            <div id="container2" style="min-width: 400px; height: 280px;"></div>
        </div>
    </div>
    <div class="row" style="margin-top: 10px;">
        <div class="col-md-6 col-sm-12">
            <div id="container3" style="min-width: 400px; height: 280px;"></div>
        </div>
        <div class="col-md-6 col-sm-12">
            <div id="container4" style="min-width: 400px; height: 280px;"></div>
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
            window.location.href = "L01MFUA.aspx";
        }

        function nextPage() {
            window.location.href="L01LW.aspx";
            //window.resizeTo(screen.availWidth, screen.availHeight);
        }

        function homePage() {
            window.location.href = "Index1.aspx";
        }

        var powerChart = null;
        var lyChart = null;
        var zhengqiChart = null;
        var tianranqiChart = null;

       

        init();
        function init() {

            requestpowerData();
            requestLYData();
            requestZhengQiData();
            requestTianRanQiData();
            window.resizeTo(screen.availWidth, screen.availHeight);
            //initTianRanQiChart()
            //initZhengQiChart()
            setTimeout(function () {
                window.location.href = "L01LW.aspx";
                //w = window.open('L01LW.aspx', 'test', 'top=100,left=0');
                //w.resizeTo(screen.availWidth, screen.availHeight);
                //self.close();
                //window.location.ose();href = "L01LW.aspx";
            }, 60000);
        }
        function initLYChart() {
            lyChart = Highcharts.chart('container2', {
                chart: {
                    type: 'column',//指定图表的类型，默认是折线图（line）
                },
                title: {
                    text: '总炉压'                 // 标题
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
        function requestLYData() {
            $.getJSON('../Handler/ProcessHandler.ashx?action=l02ly', function (data) {
                if (data.hasError == true) {
                    alert(data.error);
                    return;
                }
                else {
                    d = JSON.parse(data.data);
                    initLYChart();
                    var sd = lyChart.series.length;
                    if (sd == 0) {
                        for (i in d) {
                            lyChart.addSeries({
                                name: d[i].name,
                                data: d[i].data
                            });
                        }

                    }

                }
            });

        }


        function initPowerChart() {
            powerChart = Highcharts.chart('container1', {

                chart: {
                    type: 'column',
                    //events: {
                    //    load: requestpowerData
                    //}
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
        function requestpowerData() {
            $.getJSON('../Handler/EnergyHandler.ashx?action=getpower', { 'Line': 'L01' }, function (data) {
                if (data.hasError == true) {
                    alert(data.error);
                    return;
                }
                else {
                    d = JSON.parse(data.data);
                    initPowerChart();
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


        function initZhengQiChart() {
            zhengqiChart = Highcharts.chart('container3', {
                chart: {
                    type: 'bar',
                    //events: {
                    //    load: requestZhengQiData // 图表加载完毕后执行的回调函数
                    //}


                },
                title: {
                    text: '蒸汽用量'
                },

                xAxis: {
                    categories: ['L01'],
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
        function requestZhengQiData() {

            $.getJSON('../Handler/EnergyHandler.ashx?action=gettzhengqi', { 'Line': 'L01' }, function (data) {
                if (data.hasError == true) {
                    alert(data.error);
                    return;
                }
                else {
                    d = JSON.parse(data.data);
                    initZhengQiChart();
                    var sd = zhengqiChart.series.length;
                    if (sd == 0) {
                        for (i in d) {
                            zhengqiChart.addSeries({
                                name: d[i].name,
                                data: d[i].data
                            });
                        }

                    }


                }
            });




        }



        function initTianRanQiChart() {
            tianranqiChart = Highcharts.chart('container4', {
                chart: {
                    type: 'bar',
                    //events: {
                    //    load: requestTianRanQiData // 图表加载完毕后执行的回调函数
                    //}


                },
                title: {
                    text: '天然气用量'
                },

                xAxis: {
                    categories: ['L01'],
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

        function requestTianRanQiData() {
            $.getJSON('../Handler/EnergyHandler.ashx?action=getTianRanQi', { 'Line': 'L01' }, function (data) {
                if (data.hasError == true) {
                    alert(data.error);
                    return;
                }
                else {
                    d = JSON.parse(data.data);
                    initTianRanQiChart();
                    var sd = tianranqiChart.series.length;
                    if (sd == 0) {
                        for (i in d) {
                            tianranqiChart.addSeries({
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
