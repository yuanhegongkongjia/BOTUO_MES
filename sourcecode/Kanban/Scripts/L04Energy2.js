var linsuanChart = null;
var yansuanChart = null;
var jianChart = null;
var powerChart = null;

function requestEnergy2ata() {
    $('#dMar').css("display", "none");
    requestalarm();
    requestLinSuanData();
    requestYanSuanData();
    requestJianData();
    requestpowerData();
}

function requestalarm() {
    $.getJSON('../Handler/EnergyHandler.ashx?action=requestenergyalarm', { 'Line': 'L04' }, function (data) {
        if (data.hasError === true) {
            alert(data.error);
            return;
        }
        else {
            if (data.data === "") {
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

function initLinSuanChart() {
    linsuanChart = Highcharts.chart('container5_Energy', {
        chart: {
            type: 'bar',
            //events: {
            //    load: requestLinSuanData // 图表加载完毕后执行的回调函数
            //}


        },
        title: {
            text: '磷酸用量'
        },

        xAxis: {
            categories: ['L01', 'L02', 'L03', 'L04'],
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
function requestLinSuanData() {

    $.getJSON('../Handler/EnergyHandler.ashx?action=getlinsuan', { 'Line': 'L04' }, function (data) {
        if (data.hasError == true) {
            alert(data.error);
            return;
        }
        else {
            d = JSON.parse(data.data);
            initLinSuanChart();
            var sd = linsuanChart.series.length;
            if (sd == 0) {
                for (i in d) {
                    linsuanChart.addSeries({
                        name: d[i].name,
                        data: d[i].data
                    });
                }

            }



        }
    });




}
function initYanSuanChart() {
    yansuanChart = Highcharts.chart('container6_Energy', {
        chart: {
            type: 'bar',
            //events: {
            //    load: requestYanSuanData // 图表加载完毕后执行的回调函数
            //}


        },
        title: {
            text: '盐酸用量'
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
function requestYanSuanData() {

    $.getJSON('../Handler/EnergyHandler.ashx?action=getyansuan', { 'Line': 'L04' }, function (data) {
        if (data.hasError == true) {
            alert(data.error);
            return;
        }
        else {
            d = JSON.parse(data.data);
            initYanSuanChart();
            var sd = yansuanChart.series.length;
            if (sd == 0) {
                for (i in d) {
                    yansuanChart.addSeries({
                        name: d[i].name,
                        data: d[i].data
                    });
                }

            }


        }
    });




}
function initJianChart() {
    jianChart = Highcharts.chart('container7_Energy', {
        chart: {
            type: 'bar'


        },
        title: {
            text: '碱用量'
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
        }

    });

    //window.setInterval(requestPowerData,60000);
}

function requestJianData() {
    $.getJSON('../Handler/EnergyHandler.ashx?action=getjian', { 'Line': 'L04' }, function (data) {
        if (data.hasError == true) {
            alert(data.error);
            return;
        }
        else {
            d = JSON.parse(data.data);
            initJianChart();
            var sd = jianChart.series.length;
            if (sd == 0) {
                for (i in d) {
                    jianChart.addSeries({
                        name: d[i].name,
                        data: d[i].data
                    });
                }

            }

        }
    });

}
function initpowerChart() {
    powerChart = Highcharts.chart('container8_Energy', {

        chart: {
            type: 'column'
        },
        title: {
            text: '电用量'
            
        },
        xAxis: {
            categories: ['L04']
        },
        yAxis: {
            allowDecimals: false,
            title: {
                text: '度',
                rotation: 0
            }
        },
        plotOptions: {
            column: {
                dataLabels: {
                    enabled: true,
                    allowOverlap: true // 允许数据标签重叠
                }
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
    $.getJSON('../Handler/EnergyHandler.ashx?action=getpower', { 'Line': 'L04' }, function (data) {
        if (data.hasError == true) {
            alert(data.error);
            return;
        }
        else {
            d = JSON.parse(data.data);
            initpowerChart();
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