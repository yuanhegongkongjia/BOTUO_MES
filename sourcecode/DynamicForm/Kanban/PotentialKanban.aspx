<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PotentialKanban.aspx.cs" Inherits="DynamicForm.Kanban.PotentialKanban" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>超点频电压数据看板</title>
    <script src="../scripts/jquery-1.8.1.min.js" type="text/javascript"></script>
    <script src="../echarts/echarts.js" type="text/javascript"></script>
    <script src="../bui/bui.js" type="text/javascript"></script>
    <link href="../bui/css/dpl.css" rel="stylesheet" type="text/css" />
    <link href="../bui/css/bui.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <div class="span16">
                <div id="main" style="height: 350px;"></div>
            </div>
            <div class="span16">
                <div id="main1" style="height: 350px;"></div>
            </div>
        </div>
        <div class="row">
            <div class="span16">
                <div id="main2" style="height: 350px;"></div>
            </div>
            <div class="span16">
                <div id="main3" style="height: 350px;"></div>
            </div>
        </div>

    </form>
    <script type="text/javascript">
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById('main'));
        var myChart1 = echarts.init(document.getElementById('main1'));
        var myChart2 = echarts.init(document.getElementById('main2'));
        var myChart3 = echarts.init(document.getElementById('main3'));

        option = {
            title: {
                text: '线1-实时电压'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: ['1~28/1', '1~28/2', '29~56/1', '29~56/2'],
                y: 'top'
            },
            grid: {
                left: '3%',
                right: '4%',
                bottom: '3%',
                containLabel: true
            },
            toolbox: {
                feature: {
                    saveAsImage: {}
                }
            },
            xAxis: {
                type: 'category',
                boundaryGap: false,
                data: ['8:00', '9:00', '10:00', '11:00', '12:00', '13:00', '14:00']
            },
            yAxis: {
                type: 'value'
            },
            label: {
                textStyle: {
                    color: 'rgba(255, 255, 255, 0.3)'
                }
            },
            series: [
                {
                    name: '1~28/1',
                    type: 'line',
                    stack: '总量',
                    data: [20, 35, 56, 10, 78, 27, 100]
                },
                {
                    name: '1~28/2',
                    type: 'line',
                    stack: '总量',
                    data: [220, 120, 23, 46, 76, 35, 76]
                },
                {
                    name: '29~56/1',
                    type: 'line',
                    stack: '总量',
                    data: [15, 12, 35, 25, 64, 37, 41]
                },
                {
                    name: '29~56/2',
                    type: 'line',
                    stack: '总量',
                    data: [23, 33, 31, 56, 87, 33, 71]
                }
            ]
        };

        option1 = {
            title: {
                text: '线2-实时电压'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: ['1~28/1', '1~28/2', '29~56/1', '29~56/2'],
                y: 'top'
            },
            grid: {
                left: '3%',
                right: '4%',
                bottom: '3%',
                containLabel: true
            },
            toolbox: {
                feature: {
                    saveAsImage: {}
                }
            },
            xAxis: {
                type: 'category',
                boundaryGap: false,
                data: ['8:00', '9:00', '10:00', '11:00', '12:00', '13:00', '14:00']
            },
            yAxis: {
                type: 'value'
            },
            label: {
                textStyle: {
                    color: 'rgba(255, 255, 255, 0.3)'
                }
            },
            series: [
                {
                    name: '1~28/1',
                    type: 'line',
                    stack: '总量',
                    data: [20, 35, 56, 10, 78, 27, 100]
                },
                {
                    name: '1~28/2',
                    type: 'line',
                    stack: '总量',
                    data: [220, 120, 23, 46, 76, 35, 76]
                },
                {
                    name: '29~56/1',
                    type: 'line',
                    stack: '总量',
                    data: [15, 12, 35, 25, 64, 37, 41]
                },
                {
                    name: '29~56/2',
                    type: 'line',
                    stack: '总量',
                    data: [23, 33, 31, 56, 87, 33, 71]
                }
            ]
        };

        option2 = {
            title: {
                text: '线3-实时电压'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: ['1~28/1', '1~28/2', '29~56/1', '29~56/2'],
                y: 'top'
            },
            grid: {
                left: '3%',
                right: '4%',
                bottom: '3%',
                containLabel: true
            },
            toolbox: {
                feature: {
                    saveAsImage: {}
                }
            },
            xAxis: {
                type: 'category',
                boundaryGap: false,
                data: ['8:00', '9:00', '10:00', '11:00', '12:00', '13:00', '14:00']
            },
            yAxis: {
                type: 'value'
            },
            label: {
                textStyle: {
                    color: 'rgba(255, 255, 255, 0.3)'
                }
            },
            series: [
                {
                    name: '1~28/1',
                    type: 'line',
                    stack: '总量',
                    data: [20, 35, 56, 10, 78, 27, 100]
                },
                {
                    name: '1~28/2',
                    type: 'line',
                    stack: '总量',
                    data: [220, 120, 23, 46, 76, 35, 76]
                },
                {
                    name: '29~56/1',
                    type: 'line',
                    stack: '总量',
                    data: [15, 12, 35, 25, 64, 37, 41]
                },
                {
                    name: '29~56/2',
                    type: 'line',
                    stack: '总量',
                    data: [23, 33, 31, 56, 87, 33, 71]
                }
            ]
        };


        option3 = {
            title: {
                text: '线4-实时电压'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: ['1~28/1', '1~28/2', '29~56/1', '29~56/2'],
                y: 'top'
            },
            grid: {
                left: '3%',
                right: '4%',
                bottom: '3%',
                containLabel: true
            },
            toolbox: {
                feature: {
                    saveAsImage: {}
                }
            },
            xAxis: {
                type: 'category',
                boundaryGap: false,
                data: ['8:00', '9:00', '10:00', '11:00', '12:00', '13:00', '14:00']
            },
            yAxis: {
                type: 'value'
            },
            label: {
                textStyle: {
                    color: 'rgba(255, 255, 255, 0.3)'
                }
            },
            series: [
                {
                    name: '1~28/1',
                    type: 'line',
                    stack: '总量',
                    data: [20, 35, 56, 10, 78, 27, 100]
                },
                {
                    name: '1~28/2',
                    type: 'line',
                    stack: '总量',
                    data: [220, 120, 23, 46, 76, 35, 76]
                },
                {
                    name: '29~56/1',
                    type: 'line',
                    stack: '总量',
                    data: [15, 12, 35, 25, 64, 37, 41]
                },
                {
                    name: '29~56/2',
                    type: 'line',
                    stack: '总量',
                    data: [23, 33, 31, 56, 87, 33, 71]
                }
            ]
        };

        // 使用刚指定的配置项和数据显示图表。
        myChart.setOption(option);
        myChart1.setOption(option1);
        myChart2.setOption(option2);

        myChart3.setOption(option3);
    </script>
</body>
</html>
