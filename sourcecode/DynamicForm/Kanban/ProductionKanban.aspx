<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductionKanban.aspx.cs" Inherits="DynamicForm.ProductionKanban" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>生产情况看板</title>
    <script src="../scripts/jquery-1.8.1.min.js" type="text/javascript"></script>
    <script src="../echarts/echarts.js" type="text/javascript"></script>
    <script src="../bui/bui.js" type="text/javascript"></script>
    <link href="../bui/css/dpl.css" rel="stylesheet" type="text/css" />
    <link href="../bui/css/bui.css" rel="stylesheet" type="text/css" />
</head>
<body>

    <div class="detail-section" style="margin-top: 10px;">
        <p>
            <div class="tips tips-small  tips-success">
                <span class="x-icon x-icon-small x-icon-success"><i class="icon icon-white icon-ok"></i></span>
                <div class="tips-content">订单日排程</div>
            </div>
        </p>

        <div class="detail-row">
            <table cellspacing="0" class="table table-head-bordered">
                <thead>
                    <tr>
                        <th>生产订单</th>
                        <th>产品代码</th>
                        <th>订单数</th>
                        <th>投入数</th>
                        <th>产出数</th>
                        <th>计划开始时间</th>
                        <th>计划结束时间</th>
                        <th>实际开始时间</th>
                        <th>实际结束时间</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>52005044</td>
                        <td>2170984A</td>
                        <td>100</td>
                        <td>120</td>
                        <td>90</td>
                        <td>5-8 7:30</td>
                        <td>5-8 9:30</td>
                        <td>5-8 7:50</td>
                        <td>5-8 10:30</td>
                    </tr>
                    <tr>
                        <td>52005045</td>
                        <td>2170984B</td>
                        <td>150</td>
                        <td>100</td>
                        <td>80</td>
                        <td>5-8 11:00</td>
                        <td>5-8 15:30</td>
                        <td>5-8 11:50</td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>52005046</td>
                        <td>2170936A</td>
                        <td>200</td>
                        <td>120</td>
                        <td>100</td>
                        <td>5-8 16:30</td>
                        <td>5-9 9:30</td>
                        <td>5-8 16:50</td>
                        <td></td>
                    </tr>
                </tbody>
            </table>
        </div>
        <p>
            <div class="tips tips-small  tips-success">
                <span class="x-icon x-icon-small x-icon-success"><i class="icon icon-white icon-ok"></i></span>
                <div class="tips-content">产线产出日分布图</div>
            </div>
        </p>
        <div class="row">
            <div class="span12">
                <div id="main" style=" height: 350px;"></div>
            </div>
            <div class="span12">
                <div id="main1" style="height: 350px;margin-left:100px; "></div>
            </div>
        </div>
        
         
        <p>
            <div class="tips tips-small  tips-success">
                <span class="x-icon x-icon-small x-icon-success"><i class="icon icon-white icon-ok"></i></span>
                <div class="tips-content">班组信息</div>
            </div>
        </p>
        <form action="" class="form-horizontal form-horizontal-simple">
            <div class="row detail-row">
                <div class="span8">
                    <label>班组名称：</label><span class="detail-text">高温炉一组</span>
                </div>
                <div class="span8">
                    <label>班组人数：</label><span class="detail-text">4人</span>
                </div>
                <div class="span8">
                    <label>上岗人数：</label><span class="detail-text">4人</span>
                </div>
                <div class="span8">
                    <label>班组负责人：</label><span class="detail-text">XXX</span>
                </div>
            </div>


        </form>
    </div>
    <script>
        var myChart = echarts.init(document.getElementById('main'));
        myChart.title = '生产日分布';
        var myChart1 = echarts.init(document.getElementById('main1'));
        myChart1.title = '故障代码分析';
        option = {
            tooltip: {
                trigger: 'axis',
                axisPointer: {
                    type: 'cross',
                    crossStyle: {
                        color: '#999'
                    }
                }
            },
            toolbox: {
                feature: {
                    dataView: { show: true, readOnly: false },
                    magicType: { show: true, type: ['line', 'bar'] },
                    restore: { show: true },
                    saveAsImage: { show: true }
                }
            },
            legend: {
                data: ['产出数', '不良数', '良率']
            },
            xAxis: [
                {
                    type: 'category',
                    data: ['07:30-08:30', '08:30-09:30', '09:30-10:30', '10:30-11:30', '11:30-12:30', '12:30-13:30', '13:30-14:30', '14:30-15:30', '15:30-16:30', '16:30-17:30', '17:30-18:30', '18:30-19:30'],
                    axisPointer: {
                        type: 'shadow'
                    }
                }
            ],
            yAxis: [
                {
                    type: 'value',
                    name: '产出数',
                    min: 0,
                    max: 250,
                    interval: 50,
                    axisLabel: {
                        formatter: '{value} 米'
                    }
                },
                {
                    type: 'value',
                    name: '良率',
                    min: 0,
                    max: 100,
                    interval: 5,
                    axisLabel: {
                        formatter: '{value} %'
                    }
                }
            ],
            series: [
                {
                    name: '产出数',
                    type: 'bar',
                    data: [100, 150, 200, 180, 190, 185, 187, 170, 190, 180, 200, 210]
                },
                {
                    name: '不良数',
                    type: 'bar',
                    data: [0, 0, 3, 5, 1, 2, 0, 0, 0, 4, 0, 2]
                },
                {
                    name: '良率',
                    type: 'line',
                    yAxisIndex: 1,
                    data: [100, 100, 99, 97, 93, 100, 100, 100, 94, 95, 100, 90]
                }
            ]
        };


        // 使用刚指定的配置项和数据显示图表。
        myChart.setOption(option);

        var data = genData(5);

        var option1 = {
            title: {
                text: '产品缺陷分析',
              
                x: 'center'
            },
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                type: 'scroll',
                orient: 'vertical',
                right: 80,
                top: 20,
                bottom: 20,
                data: data.legendData,

                selected: data.selected
            },
            series: [
                {
                    name: '缺陷代码',
                    type: 'pie',
                    radius: '55%',
                    center: ['40%', '50%'],
                    data: data.seriesData,
                    itemStyle: {
                        emphasis: {
                            shadowBlur: 10,
                            shadowOffsetX: 0,
                            shadowColor: 'rgba(0, 0, 0, 0.5)'
                        }
                    }
                }
            ]
        };




        function genData(count) {
            var nameList = [
                '气泡', '斑点', '裂纹', '酸碱度超标', '韧度'
            ];
            var legendData = [];
            var seriesData = [];
            var selected = {};
            for (var i = 0; i < 5; i++) {
                name = Math.random() > 0.65
                    ? makeWord(4, 1) + '·' + makeWord(3, 0)
                    : makeWord(2, 1);
                legendData.push(name);
                seriesData.push({
                    name: nameList[i],
                    value: Math.round(Math.random() * 100000)
                });
                selected[name] = i < 3;
            }

            return {
                legendData: legendData,
                seriesData: seriesData,
                selected: selected
            };

            function makeWord(max, min) {
                var nameLen = Math.ceil(Math.random() * max + min);
                var name = [];
                for (var i = 0; i < nameLen; i++) {
                    name.push(nameList[Math.round(Math.random() * nameList.length - 1)]);
                }
                return name.join('');
            }
        }


        myChart1.setOption(option1);
    </script>



</body>
</html>
