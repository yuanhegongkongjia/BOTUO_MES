var chunshuiChart = null;
var zilaishuiChart = null;
var zhengqiChart = null;
var tianranqiChart = null;

function requestEnergy1ata() {
    $('#dMar').css("display", "none");
    requestalarm();
    requestchunshuiData();
    requestzilaishuiData();
    requestZhengQiData();
    requestTianRanQiData();
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


function initchunshuiChart() {
    chunshuiChart = Highcharts.chart('container1_Energy', {
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
    zilaishuiChart = Highcharts.chart('container2_Energy', {
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
    zhengqiChart = Highcharts.chart('container3_Energy', {
        chart: {
            type: 'bar'


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
    tianranqiChart = Highcharts.chart('container4_Energy', {
        chart: {
            type: 'bar'

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