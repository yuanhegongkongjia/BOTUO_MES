<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StationTempKanban.aspx.cs" Inherits="DynamicForm.StationTempKanban" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>温度实时看板</title>
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
                <div id="main" style="height:350px;"></div>
            </div>
            <div class="span16">
                <div id="main1" style="height:350px;"></div>
            </div>
        </div>
        <div class="row">
            <div class="span16">
                <div id="main2" style="height:350px;"></div>
            </div>
            <div class="span16">
                <div id="main3" style="height:350px;"></div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById('main'));
        var myChart1 = echarts.init(document.getElementById('main1'));
        var myChart2 = echarts.init(document.getElementById('main2'));
        var myChart3 = echarts.init(document.getElementById('main3'));
        option1 = {
            title: {
                text: '线1-实时温度'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: ['水浴', '酸洗', '碱洗', '热水洗', '皂浸']
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
                    name: '水浴',
                    type: 'line',
                    stack: '总量',
                    data: [1220, 7645, 5600, 1977, 7800, 2700, 10000]
                },
                {
                    name: '酸洗',
                    type: 'line',
                    stack: '总量',
                    data: [2240, 3120, 5623, 4627, 7656, 3545, 7600]
                },
                {
                    name: '碱洗',
                    type: 'line',
                    stack: '总量',
                    data: [1578, 2512, 7835, 4525, 2364, 6737, 4010]
                },
                {
                    name: '热水洗',
                    type: 'line',
                    stack: '总量',
                    data: [2723, 3300, 3631, 6756, 8700, 3633, 7010]
                },
                {
                    name: '皂浸',
                    type: 'line',
                    stack: '总量',
                    data: [2900, 3345, 9000, 5634, 6734, 3523, 7800]
                }
            ]
        };

        option2 = {
            title: {
                text: '线2-实时温度'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: ['水浴', '酸洗', '碱洗', '热水洗', '皂浸']
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
                    name: '水浴',
                    type: 'line',
                    stack: '总量',
                    data: [1220, 7645, 5600, 1977, 7800, 2700, 10000]
                },
                {
                    name: '酸洗',
                    type: 'line',
                    stack: '总量',
                    data: [2240, 3120, 5623, 4627, 7656, 3545, 7600]
                },
                {
                    name: '碱洗',
                    type: 'line',
                    stack: '总量',
                    data: [1578, 2512, 7835, 4525, 2364, 6737, 4010]
                },
                {
                    name: '热水洗',
                    type: 'line',
                    stack: '总量',
                    data: [2723, 3300, 3631, 6756, 8700, 3633, 7010]
                },
                {
                    name: '皂浸',
                    type: 'line',
                    stack: '总量',
                    data: [2900, 3345, 9000, 5634, 6734, 3523, 7800]
                }
            ]
        };


        option3 = {
            title: {
                text: '线3-实时温度'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: ['水浴', '酸洗', '碱洗', '热水洗', '皂浸']
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
                    name: '水浴',
                    type: 'line',
                    stack: '总量',
                    data: [1220, 7645, 5600, 1977, 7800, 2700, 10000]
                },
                {
                    name: '酸洗',
                    type: 'line',
                    stack: '总量',
                    data: [2240, 3120, 5623, 4627, 7656, 3545, 7600]
                },
                {
                    name: '碱洗',
                    type: 'line',
                    stack: '总量',
                    data: [1578, 2512, 7835, 4525, 2364, 6737, 4010]
                },
                {
                    name: '热水洗',
                    type: 'line',
                    stack: '总量',
                    data: [2723, 3300, 3631, 6756, 8700, 3633, 7010]
                },
                {
                    name: '皂浸',
                    type: 'line',
                    stack: '总量',
                    data: [2900, 3345, 9000, 5634, 6734, 3523, 7800]
                }
            ]
        };


        option4 = {
            title: {
                text: '线4-实时温度'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: ['水浴', '酸洗', '碱洗', '热水浴', '皂浸']
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
                    name: '水浴',
                    type: 'line',
                    stack: '总量',
                    data: [1220, 7645, 5600, 1977, 7800, 2700, 10000]
                },
                {
                    name: '酸洗',
                    type: 'line',
                    stack: '总量',
                    data: [2240, 3120, 5623, 4627, 7656, 3545, 7600]
                },
                {
                    name: '碱洗',
                    type: 'line',
                    stack: '总量',
                    data: [1578, 2512, 7835, 4525, 2364, 6737, 4010]
                },
                {
                    name: '热水浴',
                    type: 'line',
                    stack: '总量',
                    data: [2723, 3300, 3631, 6756, 8700, 3633, 7010]
                },
                {
                    name: '皂浸',
                    type: 'line',
                    stack: '总量',
                    data: [2900, 3345, 9000, 5634, 6734, 3523, 7800]
                }
            ]
        };
        // 使用刚指定的配置项和数据显示图表。
        myChart.setOption(option1);
        myChart1.setOption(option2);
        myChart2.setOption(option3);
        myChart3.setOption(option4);

    </script>
</body>
</html>
