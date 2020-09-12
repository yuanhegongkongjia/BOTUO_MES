<%@ Page Title="" Language="C#" MasterPageFile="~/KanbanSite.Master" AutoEventWireup="true" CodeBehind="Kanban_L04.aspx.cs" Inherits="Kanban.Kanban_L04" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <div class="logo">
            
            <span style="font-size: 28px; color: white;" onclick="requestFullScreen();" id="span1">四号线炉温看板</span>

            <span style="color: white;" id="timeDiv"></span>
            <button class="button-primary" onclick="prePage()">上一页</button>

            <button class="button-info" onclick="homePage()">回到首页</button>
            <button class="button-success" onclick="nextPage()">下一页</button>
            <div style="display:none" id="dMar"></div>
        </div>
    </div>
    <div class="row" id="LWrow1">
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
    <div class="row" id="LWrow2">
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
        <div class="row" id="LYrow1" style="display:none">
        <div class="col-md-4 col-sm-6">
            <div id="container1_LY" style="min-width: 400px; height: 300px;"></div>
        </div>
        <div class="col-md-4 col-sm-6">
            <div id="container2_LY" style="min-width: 400px; height: 300px;"></div>
        </div>
        <div class="col-md-4 col-sm-6">
            <div id="container3_LY" style="min-width: 400px; height: 300px;"></div>
        </div>
    </div>
    <div class="row" style="margin-top: 10px;display:none" id="LYrow2">
        <div class="col-md-4 col-sm-6">
            <div id="container4_LY" style="min-width: 400px; height: 300px;"></div>
        </div>
        <div class="col-md-4 col-sm-6">
            <div id="container5_LY" style="min-width: 400px; height: 300px;"></div>
        </div>
        <div class="col-md-4 col-sm-6">
            <div id="container6_LY" style="min-width: 400px; height: 300px;"></div>
        </div>
    </div>
    <div class="row" id="MFUArow1" style="display:none;">
        <div class="col-md-6 col-sm-12">
            <div id="container1_MFUA" style="min-width: 400px; height: 300px;"></div>
        </div>
        <div class="col-md-6 col-sm-12">
            <div id="container2_MFUA" style="min-width: 400px; height: 300px;"></div>
        </div>
    </div>
    <div class="row" style="margin-top: 10px;display:none" id="MFUArow2">
        <div class="col-md-6 col-sm-12">
            <div id="container3_MFUA" style="min-width: 400px; height: 300px;"></div>
        </div>
        <div class="col-md-6 col-sm-12">
            <div id="container4_MFUA" style="min-width: 400px; height: 300px;"></div>
        </div>
    </div>
    <div class="row" id="Energyrow1" style="display:none">
        <div class="col-md-6 col-sm-12">
            <div id="container1_Energy" style="min-width: 400px; height: 300px;"></div>
        </div>
        <div class="col-md-6 col-sm-12">
            <div id="container2_Energy" style="min-width: 400px; height: 300px;"></div>
        </div>
    </div>
    <div class="row" style="margin-top: 10px;display:none" id="Energyrow2">
        <div class="col-md-6 col-sm-12">
            <div id="container3_Energy" style="min-width: 400px; height: 300px;"></div>
        </div>
        <div class="col-md-6 col-sm-12">
            <div id="container4_Energy" style="min-width: 400px; height: 300px;"></div>
        </div>
    </div>
     <div class="row" id="Energyrow3" style="display:none">
        <div class="col-md-6 col-sm-12">
            <div id="container5_Energy" style="min-width: 400px; height: 300px;"></div>
        </div>
        <div class="col-md-6 col-sm-12">
            <div id="container6_Energy" style="min-width: 400px; height: 300px;"></div>
        </div>
    </div>
    <div class="row" style="margin-top:10px;display:none;" id="Energyrow4">
        <div class="col-md-6 col-sm-12">
            <div id="container7_Energy" style="min-width: 400px; height: 300px;"></div>
        </div>
        <div class="col-md-6 col-sm-12">
            <div id="container8_Energy" style="min-width: 400px; height: 300px;"></div>
        </div>
    </div>
    <script type="text/javascript" src="Scripts/L04LY.js?time=5"></script>
        <script type="text/javascript" src="Scripts/L04MFUA.js?time=5"></script>
    <script type="text/javascript" src="Scripts/L04Energy1.js?time=3"></script>
    <script type="text/javascript" src="Scripts/L04Energy2.js?time=4"></script>
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
            //window.location.href = "L04ENERGY1.aspx";
            if (flag == 1) {
                flag = flag - 1;
                toggleShow(flag);
                return;
            }
            if (flag == 2) {
                flag = flag - 1;
                toggleShow(flag);
                return;

            }
            if (flag == 3) {
                flag = flag - 1;
                toggleShow(flag);
                return;

            }

            if (flag == 4) {
                flag = flag - 1;
                toggleShow(flag);
                return;

            }

            if (flag == 0) {
                flag = 4;
                toggleShow(flag);
                return;
            }
        }

        function nextPage() {
            //window.location.href = "L04LY.aspx";
            if (flag == 1) {
                flag = flag + 1;
                toggleShow(flag);
                return;
            }
            if (flag == 2) {
                flag = flag + 1;
                toggleShow(flag);
                return;
            }

            if (flag == 3) {
                flag = flag + 1;
                toggleShow(flag);
                return;
            }


            if (flag == 4) {
                flag = 0;
                toggleShow(flag);
                return;
            }

            if (flag == 0) {
                flag = flag + 1;
                toggleShow(flag);
                return;
            }
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

        function toggleShow(f) {
            if (f == 1) {
                $('#LWrow1').css("display", "none");
                $('#LWrow2').css("display", "none");
                $('#LYrow1').css("display", "block");
                $('#LYrow2').css("display", "block");
                $('#MFUArow1').css("display", "none");
                $('#MFUArow2').css("display", "none");
                $('#Energyrow1').css("display", "none");
                $('#Energyrow2').css("display", "none");
                $('#Energyrow3').css("display", "none");
                $('#Energyrow4').css("display", "none");
                requestLYData();
               // requestMFUAData();
                $('#span1').html("四号线炉压看板");
                return;
            }
            if (f == 2) {
                $('#LWrow1').css("display", "none");
                $('#LWrow2').css("display", "none");
                $('#LYrow1').css("display", "none");
                $('#LYrow2').css("display", "none");
                $('#MFUArow1').css("display", "block");
                $('#MFUArow2').css("display", "block");
                $('#Energyrow1').css("display", "none");
                $('#Energyrow2').css("display", "none");
                $('#Energyrow3').css("display", "none");
                $('#Energyrow4').css("display", "none");
                //requestLYData();
                requestMFUAData();
                $('#span1').html("四号线MF电压/功率看板");
                return;
            }

            if (f == 3) {
                $('#LWrow1').css("display", "none");
                $('#LWrow2').css("display", "none");
                $('#LYrow1').css("display", "none");
                $('#LYrow2').css("display", "none");
                $('#MFUArow1').css("display", "none");
                $('#MFUArow2').css("display", "none");
                $('#Energyrow1').css("display", "block");
                $('#Energyrow2').css("display", "block");
                $('#Energyrow3').css("display", "none");
                $('#Energyrow4').css("display", "none");
                //requestLYData();
                requestEnergy1ata();
                $('#span1').html("四号线能源看板1");
                return;
            }

            if (f == 4) {
                $('#LWrow1').css("display", "none");
                $('#LWrow2').css("display", "none");
                $('#LYrow1').css("display", "none");
                $('#LYrow2').css("display", "none");
                $('#MFUArow1').css("display", "none");
                $('#MFUArow2').css("display", "none");
                $('#Energyrow1').css("display", "none");
                $('#Energyrow2').css("display", "none");
                $('#Energyrow3').css("display", "block");
                $('#Energyrow4').css("display", "block");
                //requestLYData();
                requestEnergy2ata();
                $('#span1').html("四号线能源看板2");
                return;
            }


            if (f == 0) {

                $('#LWrow1').css("display", "block");
                $('#LWrow2').css("display", "block");
                $('#LYrow1').css("display", "none");
                $('#LYrow2').css("display", "none");
                $('#MFUArow1').css("display", "none");
                $('#MFUArow2').css("display", "none");
                $('#Energyrow1').css("display", "none");
                $('#Energyrow2').css("display", "none");
                $('#Energyrow3').css("display", "none");
                $('#Energyrow4').css("display", "none");
                requestData();
                $('#span1').html("四号线炉温看板");

                return;
            }
        }

        var flag = 0;
        $("body").parent().css("overflow-y", "hidden");
        $("body").parent().css("overflow-x", "hidden");


        init();
        function init() {
            requestData();
            //setTimeout(function () {
            //    window.location.href = "L03LY.aspx";
            //}, 60000);

            setInterval(function () {
                if (flag == 0) {

                    flag = flag + 1;
                    toggleShow(flag);
                    return;
                }
                if (flag == 1) {
                    flag = flag + 1;
                    toggleShow(flag);
                    return;
                }

                if (flag == 2) {
                    flag = flag + 1;

                    toggleShow(flag);
                    return;
                }
                if (flag == 3) {
                    flag = flag + 1;

                    toggleShow(flag);
                    return;
                }
                if (flag == 4) {
                    flag = 0;

                    toggleShow(flag);
                    return;
                }

            }, 180000);
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
                            overflow: 'none',
                            enabled: true,
                            allowOverlap: true,
                            rotation: 300,
                            x: 0,
                            y: -12
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
                            overflow: 'none',
                            enabled: true,
                            allowOverlap: true,
                            rotation: 300,
                            x: 0,
                            y: -12
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
                            overflow: 'none',
                            enabled: true,
                            allowOverlap: true,
                            rotation: 300,
                            x: 0,
                            y: -12
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
                            overflow: 'none',
                            enabled: true,
                            allowOverlap: true,
                            rotation: 300,
                            x: 0,
                            y: -12
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
                            overflow: 'none',
                            enabled: true,
                            allowOverlap: true,
                            rotation: 300,
                            x: 0,
                            y: -12
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
                            overflow: 'none',
                            enabled: true,
                            allowOverlap: true,
                            rotation: 300,
                            x: 0,
                            y: -12
                        }
                    }
                }
            });

        }

        function requestData() {
            $('#dMar').css("display", "none");
            $.getJSON('../Handler/ProcessHandler.ashx?action=l04lw', function (data) {
                if (data.hasError == true) {
                    alert(data.error);
                    return;
                }
                else {
                    //initFirstLine();
                    var error = data.data1;
                    if (error === '') {
                        $('#dMar').css("display", "none");
                    } else {
                        //<marquee width=600  direction = left align = middle > <font id="fMAr" color="red" size="5" height="20" face=楷体_GB2312><STRONG>弹来弹去跑马灯!</STRONG></font></marquee >
                        $('#dMar').css("display", "inline-flex");
                        $('#dMar').empty();
                        $('#dMar').append("<marquee width=600  direction = left align = middle > <font color='red' size='4'  face=楷体_GB2312><STRONG>" + error + "</STRONG></font></marquee >");
                    }
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
