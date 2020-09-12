<%@ Page Title="" Language="C#" MasterPageFile="~/KanbanSite.Master" AutoEventWireup="true" CodeBehind="L04ENERGY1.aspx.cs" Inherits="Kanban.L04ENERGY1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <div class="logo">
            <%--<img src="img/sumin_log.JPG" style="width: 60px; height: 60px;" onclick="test();" />--%>
            <span style="font-size: 28px; color: white;" onclick="requestFullScreen();" id="span1">四号线能源看板</span>
            <%--  <h1 style="/*margin-left:600px;*/">一号线生产看板</h1>--%>
            <span style="color: white;width:200px;" id="timeDiv"></span>
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
    <div class="row" style="margin-top: 10px;">
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
            window.location.href = "L04MFUA.aspx";
        }

        function nextPage() {
            window.location.href = "L04ENERGY2.aspx";
            //window.resizeTo(screen.availWidth, screen.availHeight);
        }

        function homePage() {
            window.location.href = "Index1.aspx";
        }

        var chunshuiChart = null;
        var zilaishuiChart = null;
        var zhengqiChart = null;
        var tianranqiChart = null;



        init();
        function init() {

            requestchunshuiData();
            requestzilaishuiData();
            requestZhengQiData();
            requestTianRanQiData();
            window.resizeTo(screen.availWidth, screen.availHeight);
            //initTianRanQiChart()
            //initZhengQiChart()
            setTimeout(function () {
                //window.location.href = "L01LW.aspx";
                w = window.open('L04ENERGY2.aspx', 'test', 'top=100,left=0');
                w.resizeTo(screen.availWidth, screen.availHeight);
                self.close();
                //window.location.ose();href = "L01LW.aspx";
            }, 60000);
        }


        function initchunshuiChart() {
            chunshuiChart = Highcharts.chart('container1', {
                chart: {
                    type: 'bar',
                    //events: {
                    //    load: requestZhengQiData // 图表加载完毕后执行的回调函数
                    //}


                },
                title: {
                    text: '纯水用量'
                },

                xAxis: {
                    categories: ['L04'],
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
        }
        function requestchunshuiData() {
            $.getJSON('../Handler/EnergyHandler.ashx?action=getchunshui', { 'Line': 'L04' }, function (data) {
                if (data.hasError == true) {
                    alert(data.error);
                    return;
                }
                else {
                    d = JSON.parse(data.data);
                    initchunshuiChart();
                    var sd = chunshuiChart.series.length;
                    if (sd == 0) {
                        for (i in d) {
                            chunshuiChart.addSeries({
                                name: d[i].name,
                                data: d[i].data
                            });
                        }

                    }


                }
            });

        }

        function initzilaishuiChart() {
            zilaishuiChart = Highcharts.chart('container2', {
                chart: {
                    type: 'bar',
                    //events: {
                    //    load: requestZhengQiData // 图表加载完毕后执行的回调函数
                    //}


                },
                title: {
                    text: '自来水用量'
                },

                xAxis: {
                    categories: ['L04'],
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
        }
        function requestzilaishuiData() {
            $.getJSON('../Handler/EnergyHandler.ashx?action=getzilaishui', { 'Line': 'L04' }, function (data) {
                if (data.hasError == true) {
                    alert(data.error);
                    return;
                }
                else {
                    d = JSON.parse(data.data);
                    initzilaishuiChart();
                    var sd = zilaishuiChart.series.length;
                    if (sd == 0) {
                        for (i in d) {
                            zilaishuiChart.addSeries({
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
                    categories: ['L04'],
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

            $.getJSON('../Handler/EnergyHandler.ashx?action=gettzhengqi', { 'Line': 'L04' }, function (data) {
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
                    categories: ['L04'],
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
            $.getJSON('../Handler/EnergyHandler.ashx?action=getTianRanQi', { 'Line': 'L04' }, function (data) {
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
