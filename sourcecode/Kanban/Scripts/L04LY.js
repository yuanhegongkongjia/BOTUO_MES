var chart1_LY = null;
var chart2_LY = null;
var chart3_LY = null;
var chart4_LY = null;
var chart5_LY = null;
var chart6_LY = null;

function initChart1_LY() {
    // 图表配置
    chart1_LY = Highcharts.chart('container1_LY', {
        chart: {
            type: 'column'
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
                pointWidth: 20 ,
                dataLabels: {
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
function initChart2_LY() {
    // 图表配置
    chart2_LY = Highcharts.chart('container2_LY', {
        chart: {
            type: 'column'
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
                pointWidth: 20,
                dataLabels: {
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

function initChart3_LY() {
    // 图表配置
    chart3_LY = Highcharts.chart('container3_LY', {
        chart: {
            type: 'column'
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
                pointWidth: 20,
                dataLabels: {
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
function initChart4_LY() {
    // 图表配置
    chart4_LY = Highcharts.chart('container4_LY', {
        chart: {
            type: 'column'
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
                pointWidth: 20,
                dataLabels: {
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
function initChart5_LY() {
    // 图表配置
    chart5_LY = Highcharts.chart('container5_LY', {
        chart: {
            type: 'column'
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
                pointWidth: 20,
                dataLabels: {
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

function requestLYData() {
    $('#dMar').css("display", "none");
    $.getJSON('../Handler/ProcessHandler.ashx?action=l04fenquly', function (data) {
        if (data.hasError === true) {
            alert(data.error);
            return;
        }
        else {
            var error = data.data1;
            if (error === '') {
                $('#dMar').css("display", "none");
            } else {
                //<marquee width=600  direction = left align = middle > <font id="fMAr" color="red" size="5" height="20" face=楷体_GB2312><STRONG>弹来弹去跑马灯!</STRONG></font></marquee >
                $('#dMar').css("display", "inline-flex");
                $('#dMar').empty();
                $('#dMar').append("<marquee width=600  direction = left align = middle > <font color='red' size='4'  face=楷体_GB2312><STRONG>" + error + "</STRONG></font></marquee >");
            }
            //initFirstLine();
            d = JSON.parse(data.data);
            var names = [];
            var series1 = [{ name: '实际值', data: [] }];
            var series2 = [{ name: '实际值', data: [] }];
            var series3 = [{ name: '实际值', data: [] }];
            var series4 = [{ name: '实际值', data: [] }];
            var series5 = [{ name: '实际值', data: [] }];
            var series6 = [{ name: '实际值', data: [] }];
            cs1 = [];
            cs2 = [];
            cs3 = [];
            cs4 = [];
            cs5 = [];
            for (m in d) {
                var name = d[m].SeriesName;
                if (name === "4号线炉压1") {
                    series1[0].data.push([new Date(d[m].time).getTime(), d[m].value]);
                    cs1.push(d[m].value);
                }
                if (name === "4号线炉压2") {
                    series2[0].data.push([new Date(d[m].time).getTime(), d[m].value]);
                    cs2.push(d[m].value);
                }
                if (name === "4号线炉压3") {

                    series3[0].data.push([new Date(d[m].time).getTime(), d[m].value]);
                    cs3.push(d[m].value);
                }
                if (name === "4号线炉压4") {

                    series4[0].data.push([new Date(d[m].time).getTime(), d[m].value]);
                    cs4.push(d[m].value);
                }
                if (name === "4号线炉压5") {

                    series5[0].data.push([new Date(d[m].time).getTime(), d[m].value]);
                    cs5.push(d[m].value);
                }
              


            }

            initChart1_LY();
            initChart2_LY();
            initChart3_LY();
            initChart4_LY();
            initChart5_LY();
      
            if (chart1_LY.series.length === 0) {
                for (i in series1) {
                    chart1_LY.addSeries({
                        name: series1[i].name,
                        data: series1[i].data
                    });
                }
            } else {
                chart1_LY.update({
                    series: series1
                });
            }
            let max = Math.max(...cs1);
            chart1_LY.yAxis[0].setExtremes(0, max + 100);

            if (chart2_LY.series.length == 0) {
                for (i in series2) {
                    chart2_LY.addSeries({
                        name: series2[i].name,
                        data: series2[i].data
                    });
                }
            } else {
                chart2_LY.update({
                    series: series2
                });
            }
            let max2 = Math.max(...cs2);
            chart2_LY.yAxis[0].setExtremes(0, max2 + 100);
            if (chart3_LY.series.length == 0) {
                for (i in series3) {
                    chart3_LY.addSeries({
                        name: series3[i].name,
                        data: series3[i].data
                    });
                }
            } else {
                chart3_LY.update({
                    series: series3
                });
            }
            let max3 = Math.max(...cs3);
            chart3_LY.yAxis[0].setExtremes(0, max3 + 100);
            if (chart4_LY.series.length === 0) {
                for (i in series4) {
                    chart4_LY.addSeries({
                        name: series4[i].name,
                        data: series4[i].data
                    });
                }
            } else {
                chart4_LY.update({
                    series: series4
                });
            }

            let max4 = Math.max(...cs4);
            chart4_LY.yAxis[0].setExtremes(0, max4 + 100);
            if (chart5_LY.series.length === 0) {
                for (i in series5) {
                    chart5_LY.addSeries({
                        name: series5[i].name,
                        data: series5[i].data
                    });
                }
            } else {
                chart5_LY.update({
                    series: series5
                });
            }
            let max5 = Math.max(...cs5);
            chart5_LY.yAxis[0].setExtremes(0, max5 + 100);
         
        }
    });
}