function initChart1_MFUA() {
    // 图表配置
    chart1 = Highcharts.chart('container1_MFUA', {
        chart: {

        },
        title: {
            text: 'MF-A1电压/功率'                 // 标题
        },
        subtitle: {
            text: '最近一小时工艺数据'
        },

        xAxis: {
            crosshair: true,
            type: 'datetime',
            dateTimeLabelFormats: { minute: '%H:%M' }//,
        },
        yAxis: [
            {
                title: {
                    text: '电压(V)',
                    style: {
                        color: '#89A54E'
                    }
                }

            }, {
                title: {
                    text: '功率(KW)',
                    style: {
                        color: '#4572A7'
                    }
                },

                opposite: true
            }],
        series: [

        ],
        plotOptions: {
            column: {
                dataLabels: {
                    enabled: true,
                    allowOverlap: true,
                    rotation: 300,
                    x: 0,
                    y: -12
                }
            },
            spline: {
                dataLabels: {
                    enabled: true
                }
            }
        }
    });

}
function initChart2_MFUA() {
    // 图表配置
    chart2 = Highcharts.chart('container2_MFUA', {
        chart: {
            type: 'column'
        },
        title: {
            text: 'MF-A2电压/功率'                 // 标题
        },
        subtitle: {
            text: '最近一小时工艺数据'
        },

        xAxis: {
            type: 'datetime',
            dateTimeLabelFormats: { minute: '%H:%M' }//,
        },
        yAxis: [{
            title: {
                text: '电压(V)',
                style: {
                    color: '#89A54E'
                }
            }

        }, {
            title: {
                text: '功率(KW)',
                style: {
                    color: '#4572A7'
                }
            },

            opposite: true
        }],
        series: [

        ],
        plotOptions: {
            column: {
                dataLabels: {
                    enabled: true,
                    allowOverlap: true,
                    rotation: 300,
                    x: 0,
                    y: -12
                }
            },
            spline: {
                dataLabels: {
                    enabled: true
                }
            }
        }
    });

}

function initChart3_MFUA() {
    // 图表配置
    chart3 = Highcharts.chart('container3_MFUA', {
        chart: {
            type: 'column'
        },
        title: {
            text: 'MF-B1电压/功率'                 // 标题
        },
        subtitle: {
            text: '最近一小时工艺数据'
        },

        xAxis: {
            type: 'datetime',
            dateTimeLabelFormats: { minute: '%H:%M' }//,
        },
        yAxis: [{
            title: {
                text: '电压(V)',
                style: {
                    color: '#89A54E'
                }
            }

        }, {
            title: {
                text: '功率(KW)',
                style: {
                    color: '#4572A7'
                }
            },

            opposite: true
        }],
        series: [

        ],
        plotOptions: {
            column: {
                min: 100,
                dataLabels: {
                    enabled: true,
                    allowOverlap: true,
                    rotation: 300,
                    x: 0,
                    y: -12
                }
            },
            spline: {
                dataLabels: {
                    enabled: true
                }
            }
        }
    });

}
function initChart4_MFUA() {
    // 图表配置
    chart4 = Highcharts.chart('container4_MFUA', {
        chart: {
            type: 'column'
        },
        title: {
            text: 'MF-B2电压/功率'                 // 标题
        },
        subtitle: {
            text: '最近一小时工艺数据'
        },

        xAxis: {
            type: 'datetime',
            dateTimeLabelFormats: { minute: '%H:%M' }//,
        },
        yAxis: [{
            title: {
                text: '电压(V)',
                style: {
                    color: '#89A54E'
                }
            }

        }, {
            title: {
                text: '功率(KW)',
                style: {
                    color: '#4572A7'
                }
            },

            opposite: true
        }],
        series: [

        ],
        plotOptions: {
            column: {
                dataLabels: {
                    enabled: true,
                    allowOverlap: true,
                    rotation: 300,
                    x: 0,
                    y: -12
                }
            },
            spline: {
                dataLabels: {
                    enabled: true
                }
            }
        }
    });

}

function requestMFUAData() {
    $('#dMar').css("display", "none");
    $.getJSON('../Handler/ProcessHandler.ashx?action=l01ua', function (data) {
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
            var series1 = [{ name: '设定值', data: [] }, { name: '实际值', data: [] }, { name: 'A1功率', data: [] }];
            var series2 = [{ name: '设定值', data: [] }, { name: '实际值', data: [] }, { name: 'A2功率', data: [] }];
            var series3 = [{ name: '设定值', data: [] }, { name: '实际值', data: [] }, { name: 'B1功率', data: [] }];
            var series4 = [{ name: '设定值', data: [] }, { name: '实际值', data: [] }, { name: 'B2功率', data: [] }];

            cs1 = [];
            cs2 = [];
            cs3 = [];
            cs4 = [];
            g1 = [];
            g2 = [];
            g3 = [];
            g4 = [];
            for (m in d) {
                var name = d[m].SeriesName;
                if (name === "A1电压") {
                    if (d[m].ParamType === "设定值") {
                        series1[0].data.push([new Date(d[m].time).getTime(), d[m].value]);
                        cs1.push(d[m].value);
                    }
                    if (d[m].ParamType === "实际值") {
                        series1[1].data.push([new Date(d[m].time).getTime(), d[m].value]);
                        cs1.push(d[m].value);
                    }
                }
                if (name === "A1功率") {
                    series1[2].data.push([new Date(d[m].time).getTime(), d[m].value / 1000.0]);
                    g1.push(d[m].value / 1000.0);
                }
                if (name === "A2电压") {
                    if (d[m].ParamType === "设定值") {
                        series2[0].data.push([new Date(d[m].time).getTime(), d[m].value]);
                        cs2.push(d[m].value);
                    }
                    if (d[m].ParamType === "实际值") {
                        series2[1].data.push([new Date(d[m].time).getTime(), d[m].value]);
                        cs2.push(d[m].value);
                    }
                }
                if (name === "A2功率") {
                    series2[2].data.push([new Date(d[m].time).getTime(), d[m].value / 1000.0]);
                    g2.push(d[m].value / 1000.0);
                }
                if (name === "B1电压") {
                    if (d[m].ParamType === "设定值") {
                        series3[0].data.push([new Date(d[m].time).getTime(), d[m].value]);
                        cs3.push(d[m].value);
                    }
                    if (d[m].ParamType === "实际值") {
                        series3[1].data.push([new Date(d[m].time).getTime(), d[m].value]);
                        cs3.push(d[m].value);
                    }
                }
                if (name === "B1功率") {
                    series3[2].data.push([new Date(d[m].time).getTime(), d[m].value / 1000.0]);
                    g3.push(d[m].value / 1000.0);
                }

                if (name === "B2电压") {
                    if (d[m].ParamType === "设定值") {
                        series4[0].data.push([new Date(d[m].time).getTime(), d[m].value]);
                        cs4.push(d[m].value);
                    }
                    if (d[m].ParamType === "实际值") {
                        series4[1].data.push([new Date(d[m].time).getTime(), d[m].value]);
                        cs4.push(d[m].value);
                    }
                }
                if (name === "B2功率") {
                    series4[2].data.push([new Date(d[m].time).getTime(), d[m].value / 1000.0]);
                    g4.push(d[m].value / 1000.0);
                }


            }

            initChart1_MFUA();
            initChart2_MFUA();
            initChart3_MFUA();
            initChart4_MFUA();

            if (chart1.series.length === 0) {
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
                    type: 'spline',
                    yAxis: 1,
                    data: series1[2].data
                });
                let max = Math.max(...cs1);
                let min = Math.min(...cs1);
                chart1.yAxis[0].setExtremes(0, max + 200);

                let max1 = Math.max(...g1);
                let min1 = Math.min(...g1);
                chart1.yAxis[1].setExtremes(0, max1 + 5);

            } else {
                chart1.update({
                    series: series1
                });
            }

            if (chart2.series.length === 0) {
                chart2.addSeries({
                    name: series2[0].name,
                    type: 'column',
                    yAxis: 0,
                    data: series2[0].data
                });
                chart2.addSeries({
                    name: series2[1].name,
                    type: 'column',
                    yAxis: 0,
                    data: series2[1].data
                });
                chart2.addSeries({
                    name: series2[2].name,
                    type: 'spline',
                    yAxis: 1,
                    data: series2[2].data
                });

                let max = Math.max(...cs2);
                let min = Math.min(...cs2);
                chart2.yAxis[0].setExtremes(0, max + 200);

                let max1 = Math.max(...g2);
                let min1 = Math.min(...g2);
                chart2.yAxis[1].setExtremes(0, max1 + 5);

            } else {
                chart2.update({
                    series: series2
                });
            }

            if (chart3.series.length === 0) {
                chart3.addSeries({
                    name: series3[0].name,
                    type: 'column',
                    yAxis: 0,
                    data: series3[0].data
                });
                chart3.addSeries({
                    name: series3[1].name,
                    type: 'column',
                    yAxis: 0,
                    data: series3[1].data
                });
                chart3.addSeries({
                    name: series3[2].name,
                    type: 'spline',
                    yAxis: 1,
                    data: series3[2].data
                });

                let max = Math.max(...cs3);
                let min = Math.min(...cs3);
                chart3.yAxis[0].setExtremes(0, max + 200);

                let max1 = Math.max(...g3);
                let min1 = Math.min(...g3);
                chart3.yAxis[1].setExtremes(0, max1 + 5);
              
            } else {
                chart3.update({
                    series: series3
                });
            }

            if (chart4.series.length === 0) {
                chart4.addSeries({
                    name: series4[0].name,
                    type: 'column',
                    yAxis: 0,
                    data: series4[0].data
                });
                chart4.addSeries({
                    name: series4[1].name,
                    type: 'column',
                    yAxis: 0,
                    data: series4[1].data
                });
                chart4.addSeries({
                    name: series4[2].name,
                    type: 'spline',
                    yAxis: 1,
                    data: series4[2].data
                });

                let max = Math.max(...cs4);
                let min = Math.min(...cs4);
                chart4.yAxis[0].setExtremes(0, max + 200);

                let max1 = Math.max(...g4);
                let min1 = Math.min(...g4);
                chart4.yAxis[1].setExtremes(0, max1 + 5);
            } else {
                chart4.update({
                    series: series4
                });
            }
            $('#span1').html("一号线MF电压/功率看板");
        }
    });
}