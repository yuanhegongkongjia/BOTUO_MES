<%@ Page Title="" Language="C#" MasterPageFile="~/KanbanSite.Master" AutoEventWireup="true" CodeBehind="BT_ProductionKANBAN.aspx.cs" Inherits="Kanban.WebForm6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<div class="page-header">--%>

    <div class="header">
     <div class="a-access" style="width:100%">
     <img src="img/zhongding.png" style="width:48px;height:48px;margin-left: 20px; top:1%; left:5%;" />
    
     <span style="color: #00ffff;width:200px;font-size: 35px;text-align:center;" >博拓生产情况看板</span>
       
     <span style="color: #00ffff;width:200px;font-size: 16px;position: absolute; top:25%; right:3%;" id="timeDiv">2016-12-21 上午13:00:00</span>
            
     </div>
     </div>




 <%--   <div class="logo" style="width:100%">
        <img src="img/zhongding.jpg" style="width:60px;height:60px;margin-left: 20px;" />
    <span style="font-size: 32px; color: white;display:inline-block;width:75%;text-align:center;">博拓生产情况看板</span>
        <span style="color: white;width:200px;font-size: 16px;" id="timeDiv"></span>
    </div>
</div> --%>
 <%--   <div class="row">
    <div class="col-md-6 col-sm-12" style="position: absolute;
    padding-top: 114px;
    margin-top: -114px;
    height: 100%;padding-bottom:20px">
    <div id="container1" style="width:100%; height:100%;margin-left:10px"></div>
    </div>--%>
   
     <div class="main clearfix">
        <div class="main-left">
            <div class="border-container">
                <div class="name-title"></div>
                <div id="container1"></div>
                <span class="top-left border-span"></span>
                <span class="top-right border-span"></span>
                <span class="bottom-left border-span"></span>
                <span class="bottom-right border-span"></span>
            </div> 
           
        </div>
     <div class="main-right">
            <div class="border-container">
                <div class="name-title"></div>
                <div id="container2"></div>
                <span class="top-left border-span"></span>
                <span class="top-right border-span"></span>
                <span class="bottom-left border-span"></span>
                <span class="bottom-right border-span"></span>
            </div> 
           
        </div>
</div>

  <%--  <div class="col-md-6 col-sm-12" style="position: absolute;
    padding-top: 114px;
    margin-top: -114px;
    margin-left :640px;
    height: 100%;padding-bottom:20px">
        <div id="container2" style="width:100%; height:100%;margin-left:10px"></div>
    </div>
</div> --%>  
    <script>
        function current() {
            var d = new Date(), str = '';//在盒子里面，margin和padding作用相同，飞出盒子用margin，absolute是脱离文档，充当整个页面里面
            str += d.getFullYear() + '/'; //获取当前年份 
            str += d.getMonth() + 1 + '/'; //获取当前月份（0——11） 
            str += d.getDate() + ' ';
            str += d.getHours() + ':';
            str += d.getMinutes() + ':';
            str += d.getSeconds() + '';
            return str;
        }
        Highcharts.setOptions({ global: { useUTC: false } });
        setInterval(function () {
            $("#timeDiv").html(current());
        }, 1000);
        init();
        function init() {
            requestData();
           
        }
        function initChart1() {
            // 图表配置
            chart1 = Highcharts.chart('container1', {
                chart: {
                    //type: 'column',//指定图表的类型，默认是折线图（line）
                    backgroundColor:
                        'rgba(0,0,0,0)'
                },
                colors: [
                    'rgba(255,69,0,0.99)', 'rgba(173,255,47,0.99)', 'rgba(100,149,237,0.5)', '#FFF263', '#6AF9C4'],

                title: {
                    text: '当日生产情况看板',             // 标题
                    style: {
                        color: 'white'
                        }
                },
                subtitle: {
                    text: '生产计划/完成/达成率生产情况',
                    style: {
                        color: 'white'
                    }
                },

                xAxis: {
                    crosshair: true,
                    type: 'datetime',
                    dateTimeLabelFormats: { hour: '%H:%M', }
                },

                yAxis: [{
                    title: {
                        text: '模(个)',
                        style: {
                            color: 'rgba(255,69,0,0.99)'
                        }
                    },
                },
                    {
                    title: {
                        text: '当日订单完成率(%)',
                        style: {
                            color: 'rgba(173,255,47,0.99)'
                        }
                    },

                    opposite: true
                    }],
                legend: {

                    itemStyle: {
                        color: 'white',

                    },

                },
                series: [
                   
                ],
                plotOptions: {
                    column: {
                        dataLabels: {
                            enabled: true
                        }

                    },
                   line: {
                        dataLabels: {
                           enabled: true,
                           format: '{y} %',
                           allowOverlap: true,
                           //format: '{point.y:.1f}%',

                        }

                    }

                }
            });

        }
        function initChart2() {
            // 图表配置
            chart2 = Highcharts.chart('container2', {
                chart: {
                    type: 'areapline',//指定图表的类型，默认是折线图（line）
                    backgroundColor: 
                        'rgba(0,0,0,0)'
                  
                },
                colors: [
                    'rgba(255,69,0,0.55)', 'rgba(173,255,47,0.55)', 'rgba(100,149,237,0.5)', '#FFF263', '#6AF9C4'],
                title: {
                    text: '当日生产总情况看板',                 // 标题
                    style: {
                        color: 'white'
                    }
                },
                subtitle: {
                    text: '总模数/总备料数情况',
                    style: {
                        color: 'white'
                    }
                },

                xAxis: {
                    type: 'datetime',
                    dateTimeLabelFormats: { hour: '%H:%M' }//,
                },
                yAxis: {
                    title: {
                        text: '模(个)'                // y 轴标题
                    }
                },
                legend: {
                    itemStyle: {
                        color: 'white',

                    },
                   
                },
                series: [

                ],
                plotOptions: {
                    line: {
                        dataLabels: {
                            enabled: true
                        },
                        enableMouseTracking: false
                    }
                },
            });
        }

        function requestData() {
            $.getJSON('../Handler/ProcessHandler.ashx?action=l01shengchan', function (data) {
                if (data.hasError == true) {
                    alert(data.error);
                    return;
                }
                else {
                    //initFirstLine();
                    d = JSON.parse(data.data);
                    var names = [];
                    var series1 = [{ name: '配料设定浇注模数', data: [] }, { name: '当前班次已浇注模数', data: [] }, { name: '当日订单完成率', data: [] }];
                    var series2 = [{ name: '生产运行总模数', data: [] }, { name: '生产运行总备料数', data: [] }];
                    cs1 = [];
                    cs2 = [];
                    cs3 = [];
                    cs4 = [];
                    for (m in d) {
                        var name = d[m].SeriesName;
                        if (name == "配料设定浇注模数") {
                            series1[0].data.push([new Date(d[m].time).getTime(), d[m].value]);
                        }
                        if (name == "当前班次已浇注模数") {
                            series1[1].data.push([new Date(d[m].time).getTime(), d[m].value]);
                        }
                        if (name == "当日订单完成率") {
                          
                        /*const formated = Number(d[m].value * 100).toFixed(2);*/
                            var value = d[m].value * 100;
                            var value1 = Math.floor(value * 100) / 100
                            series1[2].data.push([new Date(d[m].time).getTime(), value1]);
                        }
                        if (name == "配料浇注总模数") {

                            series2[0].data.push([new Date(d[m].time).getTime(), d[m].value]);
                        }
                        if (name == "配料备料总模数") {
                            series2[1].data.push([new Date(d[m].time).getTime(), d[m].value]);
                        }
                       // series1[2].data = series1[1].data / series1[0].data;
                        //series1[2].data.push([new Date(d[m].time).getTime(), series1[2].data]);
                    }
                    initChart1();
                    initChart2();
                    if (chart1.series.length == 0) {
                        chart1.addSeries({
                            name: series1[0].name,
                            type: 'column',
                            yAxis: 0,
                            data: series1[0].data
                        });
                        chart1.addSeries({
                            name: series1[1].name,
                            type: 'column',
                            yAxis: 0,
                            data: series1[1].data
                        });
                        chart1.addSeries({
                            name: series1[2].name,
                            type: 'line',
                            yAxis: 0,
                            data: series1[2].data
                        });
                        }
                    else {
                        chart1.update({
                            series: series1
                        });
                    }
                    if (chart2.series.length == 0) {
                        chart2.addSeries({
                            name: series2[0].name,
                            type: 'line',
                            yAxis: 0,
                            data: series2[0].data
                        });
                        chart2.addSeries({
                            name: series2[1].name,
                            type: 'line',
                            yAxis: 0,
                            data: series2[1].data
                        });
                    }
                    else {
                        chart2.update({
                            series: series2
                        });
                    }
                    //setTimeout(requestData, 6000);
                    setTimeout(requestData, 3600000);
                }
            });
        }
    </script>
</asp:Content>
