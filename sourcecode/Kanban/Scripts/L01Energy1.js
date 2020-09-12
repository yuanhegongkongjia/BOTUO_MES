var powerChart = null;
var lyChart = null;
var zhengqiChart = null;
var tianranqiChart = null;
function requestEnergy1Data() {
    $('#dMar').css("display", "none");
    requestalarm();
    requestpowerData();
    requestLYData();
    requestZhengQiData();
    requestTianRanQiData();
    $('#span1').html("一号线能源看板");
}

function requestalarm() {
    $.getJSON('../Handler/EnergyHandler.ashx?action=requestenergyalarm', { 'Line': 'L01' }, function (data) {
        if (data.hasError === true) {
            alert(data.error);
            return;
        }
        else {
            if (data.data == "") {
                $('#dMar').css("display", "none");
            }
            else {
                $('#dMar').css("display", "inline-flex");
                $('#dMar').empty();
                $('#dMar').append("<marquee width=600  direction = left align = middle > <font color='red' size='4'  face=楷体_GB2312><STRONG>" + data.data + "</STRONG></font></marquee >");
            }
        }
        });
}

function initLYChart() {
    lyChart = Highcharts.chart('container2_Energy1', {
        chart: {
            type: 'column'
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
    $.getJSON('../Handler/ProcessHandler.ashx?action=l01ly', function (data) {
        if (data.hasError == true) {
            alert(data.error);
            return;
        }
        else {
            d = JSON.parse(data.data);
            initLYChart();
            var serices = [{name:'炉压',data:[]}];
            for (m in d) {
                serices[0].data.push([new Date(d[m].time).getTime(), d[m].value]);
            }
            var sd = lyChart.series.length;
            if (sd == 0) {
                lyChart.addSeries({
                    name: serices[0].name,
                    type: 'column',
                    yAxis: 0,
                    data: serices[0].data
                });
                //for (i in d) {
                //    lyChart.addSeries({
                //        data: d[i].value,
                //        data: d[i].data
                //    });
                //}

            }

        }
    });

}


function initPowerChart() {
    powerChart = Highcharts.chart('container1_Energy1', {

        chart: {
            type: 'column'
            
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
        },
        plotOptions: {
            column: {
                dataLabels: {
                    enabled: true
                }
            }
        }
    });
}
function requestpowerData() {
    $.getJSON('../Handler/EnergyHandler.ashx?action=getpower', { 'Line': 'L01' }, function (data) {
        if (data.hasError === true) {
            alert(data.error);
            return;
        }
        else {
            d = JSON.parse(data.data);
            initPowerChart();
            var sd = powerChart.series.length;
            if (sd === 0) {
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
    zhengqiChart = Highcharts.chart('container3_Energy1', {
        chart: {
            type: 'bar'


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
                text: '公斤(Kg)',
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
            if (sd === 0) {
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
    tianranqiChart = Highcharts.chart('container4_Energy1', {
        chart: {
            type: 'bar'


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
                text: '立方米(m³)',
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
        }

    });


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
