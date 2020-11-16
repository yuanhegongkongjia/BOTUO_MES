
var myDate = new Date();

var geoCoordMap = {
    '北碚': [106.50, 29.81],
    '城口': [108.6520475, 31.90676506],
    '大足': [105.7692868, 29.65392091],
    '垫江': [107.4004904, 30.24903189],
    '丰都': [107.7461781, 29.91492542],
    '奉节': [109.3758974, 30.98202119],
    '合川': [106.2833096, 30.09766756],
    '江北': [106.6214893, 29.64821182],
    '江津': [106.2647885, 28.98483627],
    '开州': [108.1818518, 31.29466521],
    '南岸': [106.6379653, 29.47704825],
    '南川': [107.1406799, 29.12047319],
    '彭水': [108.2573507, 29.36444557],
    '温度-②': [106.8036647, 28.96872774],
    '黔江': [108.7559876, 29.44290625],
    '湿度-①': [108.2438988, 30.07512144],
    '市区': [106.4377397, 29.52648606],
    '铜梁': [106.0616035, 29.81036286],
    '潼南': [105.8116920, 30.13795513],
    '温度-①': [108.0828876, 30.73353669],
    'PM2.5-①': [109.2779184, 31.09568937],
    '巫溪': [109.2970739, 31.48090521],
    '武隆': [107.6831317, 29.36831708],
    'PM2.5-②': [108.9997005, 28.49462861],
    '湿度-②': [105.8347961, 29.41042605],
    '酉阳': [108.7911679, 28.88330557],
    '云阳': [108.7533957, 30.96025875],
    '长寿': [107.24, 29.95],
    '忠县': [107.9279014, 30.33522658],
    '川东': [107.3488646, 29.68233099],
    '采集终端': [106.5, 31.5],
};


/* 设备参数OEE数据连接*/
function requestDeviceOeeData() {
    $.getJSON('../Handler/ProcessHandler.ashx?action=getdeviceoeestaus', function (data) {
        //debugger;
        if (data.hasError == true) {
            alert(data.error);
            return;
        }
        else {
            initechartrandar()
        
            d = JSON.parse(data.data);
            
            juniorservice_option.series = [{ name: '停机时长', data: [] }, { name: '重点关注', data: [] }];
          
            /* 关于输入参数一定要对应的指定位置选好才能对应的输入 */
            juniorservice_option.calendar[0].range = [new Date(d[0].yuechu).Format('yyyy-MM-dd'), new Date(d[0].yuemo).Format('yyyy-MM-dd') ];
        

           


            for (m in d) {
                var name = d[m].ParamType;
                if (name == "1") {
                    
                    //var date = new Date(d[m].WorkDate);
                    //var dd = Date.UTC(date.getFullYear(), date.getMonth(), date.getDate());
                    juniorservice_option.series[0].data.push([new Date(d[m].time).Format("yyyy-MM-dd"),d[m].value]);
                    //juniorservice_option.xAxis[0].data.push(new Date(d[m].time).Format("yyyy-MM-dd"));
                    //series[1].data.push([d[m].TSNumber, d[m].FinishQty]);
                }
                if (d[m].value>=0.7) {
                    //var date = new Date(d[m].WorkDate);
                    //var dd = Date.UTC(date.getFullYear(), date.getMonth(), date.getDate());
                    juniorservice_option.series[1].data.push([new Date(d[m].time).Format("yyyy-MM-dd"), d[m].value]);
                    //juniorservice_option.xAxis[1].data.push(new Date(d[m].time).Format("HH:mm:ss"));
                    //series[1].data.push([d[m].TSNumber, d[m].FinishQty]);
                }
            }

         
            juniorservice.setOption(juniorservice_option)
         

        }
    });
}


/* 电能数据连接*/
function requestEnergyData() {
    $.getJSON('../Handler/ProcessHandler.ashx?action=getenergystaus', function (data) {
        //debugger;
        if (data.hasError == true) {
            alert(data.error);
            return;
        }
        else {
            initdlechart()

            d = JSON.parse(data.data);

            //juniorservice_option.series = [{ name: '停机时长', data: [] }, { name: '重点关注', data: [] }];
            radar_option.series[0].data = [{ name: [], value: [] }, { name: [], value: [] }];
            radar_option.series[1].data = [{ name: [], value: [] }, { name: [], value: [] }];
            //var labelData = [{ name: [], value: [] }]
           /* 关于输入参数一定要对应的指定位置选好才能对应的输入 */
            for (m in d) {
                time = d[m].time;
                
                radar_option.series[0].data[m].name.push(new Date(d[m].time).Format("HH") + ":00");
                radar_option.series[0].data[m].value.push (d[m].value);
                //radar_option.series[0].data.value.push = (d[m].value)    ;
                //radar_option.series[1].data.push = 1;
                radar_option.series[1].data[m].name.push (new Date(d[m].time).Format("HH") + ":00");
                radar_option.series[1].data[m].value.push(1);
            }




            //for (m in d) {
            //    var name = d[m].ParamType;
            //    if (name == "1") {

            //        //var date = new Date(d[m].WorkDate);
            //        //var dd = Date.UTC(date.getFullYear(), date.getMonth(), date.getDate());
            //        juniorservice_option.series[0].data.push([new Date(d[m].time).Format("yyyy-MM-dd"), d[m].value]);
            //        //juniorservice_option.xAxis[0].data.push(new Date(d[m].time).Format("yyyy-MM-dd"));
            //        //series[1].data.push([d[m].TSNumber, d[m].FinishQty]);
            //    }
            //    if (d[m].value >= 0.7) {
            //        //var date = new Date(d[m].WorkDate);
            //        //var dd = Date.UTC(date.getFullYear(), date.getMonth(), date.getDate());
            //        juniorservice_option.series[1].data.push([new Date(d[m].time).Format("yyyy-MM-dd"), d[m].value]);
            //        //juniorservice_option.xAxis[1].data.push(new Date(d[m].time).Format("HH:mm:ss"));
            //        //series[1].data.push([d[m].TSNumber, d[m].FinishQty]);
            //    }
            //}


            //juniorservice.setOption(juniorservice_option)
            juniorservice_radar.setOption(radar_option)

        }
    });
}




/* 环境数据连接*/
function requestTemData() {
    $.getJSON('../Handler/ProcessHandler.ashx?action=gettemstaus', function (data) {
        //debugger;
        if (data.hasError == true) {
            alert(data.error);
            return;
        }
        else {
            inittemechart()
            //var graduateyear = graduateyear_option.getOption();
            //graduateyear.setOption(graduateyear_option);
            d = JSON.parse(data.data);
            var hjDatas = [{ name: [], value: [] }, { name: [], value: [] }, { name: [], value: [] }, { name: [], value: [] }, { name: [], value: [] }, { name: [], value: [] }];
            for (m in d) {
                var name = d[m].ParamType;
                var position = d[m].Position;
                if (name == "PM2.5" && position == "西南角") {
                    hjDatas[0].name.push('PM2.5-①');
                    hjDatas[0].value.push(d[m].value);
                }
                if (name == "温度" && position == "西南角") {
                    hjDatas[1].name.push('温度-①');
                    hjDatas[1].value.push(d[m].value);
                    //hjDatas[1].push('温度-①', d[m].value);
                }
                if (name == "湿度" && position == "西南角") {
                    hjDatas[2].name.push('湿度-①');
                    hjDatas[2].value.push(d[m].value);
                   // hjDatas[2].push('湿度-①', d[m].value);
                }
                if (name == "PM2.5" && position == "东北角") {
                    hjDatas[3].name.push('PM2.5-②');
                    hjDatas[3].value.push(d[m].value);
                    //hjDatas[3].push('PM2.5-②', d[m].value);
                }
                if (name == "温度" && position == "东北角") {
                    hjDatas[4].name.push('温度-②');
                    hjDatas[4].value.push(d[m].value);
                    //hjDatas[4].push('PM2.5-②', d[m].value);
                }
                if (name == "湿度" && position == "东北角") {
                    hjDatas[5].name.push('湿度-②');
                    hjDatas[5].value.push(d[m].value);
                    //hjDatas[5].push('PM2.5-②', d[m].value);
                }

            }

            
             var geoCoordMap = {
                '北碚': [106.50, 29.81],
                '城口': [108.6520475, 31.90676506],
                '大足': [105.7692868, 29.65392091],
                '垫江': [107.4004904, 30.24903189],
                '丰都': [107.7461781, 29.91492542],
                '奉节': [109.3758974, 30.98202119],
                '合川': [106.2833096, 30.09766756],
                '江北': [106.6214893, 29.64821182],
                '江津': [106.2647885, 28.98483627],
                '开州': [108.1818518, 31.29466521],
                '南岸': [106.6379653, 29.47704825],
                '南川': [107.1406799, 29.12047319],
                '彭水': [108.2573507, 29.36444557],
                '温度-②': [106.8036647, 28.96872774],
                '黔江': [108.7559876, 29.44290625],
                '湿度-①': [108.2438988, 30.07512144],
                '市区': [106.4377397, 29.52648606],
                '铜梁': [106.0616035, 29.81036286],
                '潼南': [105.8116920, 30.13795513],
                '温度-①': [108.0828876, 30.73353669],
                'PM2.5-①': [108.5779184, 30.99568937],
                '巫溪': [109.2970739, 31.48090521],
                '武隆': [107.6831317, 29.36831708],
                'PM2.5-②': [108.9997005, 28.49462861],
                '湿度-②': [105.8347961, 29.41042605],
                '酉阳': [108.7911679, 28.88330557],
                '云阳': [108.7533957, 30.96025875],
                '长寿': [107.24, 29.95],
                '忠县': [107.9279014, 30.33522658],
                '川东': [107.3488646, 29.68233099],
                '采集终端': [106.5, 31.5],
            };

            console.log(geoCoordMap)

     courserate_option.series = [];
    [
        ['采集终端', hjDatas]
    ].forEach(function (item) {
        console.log(item[1][0])
        //console.log(geoCoordMap[item(1)]),
        courserate_option.series.push({
            type: 'lines',
            zlevel: 2,
            effect: {
                show: true,
                period: 4, //箭头指向速度，值越小速度越快
                trailLength: 0.02, //特效尾迹长度[0,1]值越大，尾迹越长重
                symbol: 'arrow', //箭头图标
                symbolSize: 12, //图标大小
            },
            lineStyle: {
                normal: {
                    width: 1, //尾迹线条宽度
                    opacity: 1, //尾迹线条透明度
                    curveness: .1 //尾迹线条曲直度
                }
            },
            data: convertData(item[1])
        }, {
            type: 'effectScatter',
            coordinateSystem: 'geo',
            zlevel: 2,
            rippleEffect: { //涟漪特效
                period: 4, //动画时间，值越小速度越快
                brushType: 'stroke', //波纹绘制方式 stroke, fill
                scale: 8 //波纹圆环最大限制，值越大波纹越大
            },
            label: {
                normal: {
                    show: true,
                    position: 'right', //显示位置
                    offset: [3, 0], //偏移设置
                    formatter: function (params) { //圆环显示文字
                        return params.data.name+'\n'+params.data.value[2];
                    },
                  
                    fontSize: 12
                },
                emphasis: {
                    show: true
                }
            },
            symbol: 'circle',
            symbolSize: function (val) {
                return 2 + val[2] * 1; //圆环大小
            },
            itemStyle: {
                normal: {
                    show: false,
                    color: '#f00'
                }
                },
           
            data: item[1].map(function (dataItem) {
                return {
                    name: dataItem.name,
                    
                    value: this.geoCoordMap[dataItem.name].concat([dataItem.value])
                };
            }),
        },
            //被攻击点
            {
                type: 'effectScatter',
                coordinateSystem: 'geo',
                zlevel: 2,
                rippleEffect: {
                    period: 4,
                    brushType: 'stroke',
                    scale: 8
                },
                label: {
                    normal: {
                        show: true,
                        position: 'right', //显示位置
                        offset: [5, 0], //偏移设置
                        formatter: function (params) { //圆环显示文字
                            return params.data.name;
                            //return res;
                        },
                        fontSize: 12
                    },
                    emphasis: {
                        show: true
                    }
                },
                symbol: 'circle',
                symbolSize: function (val) {
                    return 1 + val[2] * 1; //圆环大小
                },
                itemStyle: {
                    normal: {
                        show: false,
                        color: '#0f0'
                    }
                },
                data: [{
                    name: item[0],
                    value: this.geoCoordMap[item[0]].concat([10]),
                }],
            }
        );
    });


            courserate.setOption(courserate_option)
            
      


        }
    });
}





/* 设备监控数据连接*/
function requestDeviceData() {
    $.getJSON('../Handler/ProcessHandler.ashx?action=getdevicestaus', function (data) {
            //debugger;
            if (data.hasError == true) {
                alert(data.error);
                return;
            }
            else {
                initechart()
               
                d = JSON.parse(data.data);
              
                graduateyear_option.series = [{ name: '压力', data: [] }, { name: '电流', data: [] }];
                graduateyear_option.xAxis = [{  data: [] }, { data: [] }];
               

                for (m in d) {
                    var name = d[m].ParamType;
                    if (name == "压力") {
                        //var date = new Date(d[m].WorkDate);
                        //var dd = Date.UTC(date.getFullYear(), date.getMonth(), date.getDate());
                        graduateyear_option.series[0].data.push(d[m].value);
                        graduateyear_option.xAxis[0].data.push(new Date(d[m].time).Format("HH:mm"));
                        //series[1].data.push([d[m].TSNumber, d[m].FinishQty]);
                    }
                    if (name == "电流") {
                        //var date = new Date(d[m].WorkDate);
                        //var dd = Date.UTC(date.getFullYear(), date.getMonth(), date.getDate());
                        graduateyear_option.series[1].data.push(d[m].value);
                        graduateyear_option.xAxis[1].data.push(new Date(d[m].time).Format("HH:mm"));
                        //series[1].data.push([d[m].TSNumber, d[m].FinishQty]);
                    }
                }

                //graduateyear_option.series[0].data = d.value;
                ////graduateyear_option.series[1].data = d.FinishQty;
                //graduateyear_option.yAxis[0].data = d.time;
                //juniorservice.setOption(juniorservice_option)
                graduateyear.setOption(graduateyear_option)
            


            }
        });
    }

/* 原材料数据连接*/
function requestCusData() {
    $.getJSON('../Handler/ProcessHandler.ashx?action=getcusstaus', function (data) {
        //debugger;
        if (data.hasError == true) {
            alert(data.error);
            return;
        }
        else {
            initcbechart()

            d = JSON.parse(data.data);

            professionrate_option.series[2].data = [{ name: '石灰', value: [] }, { name: '水泥', value: [] }, { name: '石膏', value: [] }, { name: '废浆', value: [] }, { name: '料浆', value: [] }];
         
            var sum = 0
            for (var i = 0; i < d.length; i++) {
                sum = sum + d[i].value
            }
            for (m in d) {
                var name = d[m].SeriesName;
                if (name == "石灰") {
                    //var date = new Date(d[m].WorkDate);
                    //var dd = Date.UTC(date.getFullYear(), date.getMonth(), date.getDate());
                    professionrate_option.series[2].data[0].value.push((d[m].value / sum).toFixed(4) * 100);
                    //graduateyear_option.xAxis[0].data.push(new Date(d[m].time).Format("HH:mm:ss"));
                    //series[1].data.push([d[m].TSNumber, d[m].FinishQty]);
                }
                if (name == "水泥") {
                    //var date = new Date(d[m].WorkDate);
                    //var dd = Date.UTC(date.getFullYear(), date.getMonth(), date.getDate());
                    professionrate_option.series[2].data[1].value.push((d[m].value / sum).toFixed(4) * 100);
                    //graduateyear_option.xAxis[1].data.push(new Date(d[m].time).Format("HH:mm:ss"));
                    //series[1].data.push([d[m].TSNumber, d[m].FinishQty]);
                }
                if (name == "石膏") {
                    //var date = new Date(d[m].WorkDate);
                    //var dd = Date.UTC(date.getFullYear(), date.getMonth(), date.getDate());
                    professionrate_option.series[2].data[2].value.push((d[m].value / sum).toFixed(4) * 100);
                   // graduateyear_option.xAxis[1].data.push(new Date(d[m].time).Format("HH:mm:ss"));
                    //series[1].data.push([d[m].TSNumber, d[m].FinishQty]);
                }
                if (name == "废浆") {
                    //var date = new Date(d[m].WorkDate);
                    //var dd = Date.UTC(date.getFullYear(), date.getMonth(), date.getDate());
                    professionrate_option.series[2].data[3].value.push((d[m].value / sum).toFixed(4) * 100);
                    //graduateyear_option.xAxis[1].data.push(new Date(d[m].time).Format("HH:mm:ss"));
                    //series[1].data.push([d[m].TSNumber, d[m].FinishQty]);
                }
                if (name == "料浆") {
                    //var date = new Date(d[m].WorkDate);
                    //var dd = Date.UTC(date.getFullYear(), date.getMonth(), date.getDate());
                    professionrate_option.series[2].data[4].value.push((d[m].value / sum).toFixed(4) * 100);
                    //graduateyear_option.xAxis[1].data.push(new Date(d[m].time).Format("HH:mm:ss"));
                    //series[1].data.push([d[m].TSNumber, d[m].FinishQty]);
                }
            }

         
            professionrate.setOption(professionrate_option)



        }
    });
}








function requestProductData() {
    $.getJSON('../Handler/ProcessHandler.ashx?action=1033zz', function (data) {
        //debugger;
        if (data.hasError == true) {
            alert(data.error);
            return;
        }
        else {
           
            d = JSON.parse(data.data);
          
            $("tr:eq(1) td:eq(0)").eq(0).text(new Date(myDate.getTime()).Format("yyyy.MM.dd"));
            $("tr:eq(2) td:eq(0)").eq(0).text(new Date(myDate.getTime() - 1 * 24 * 60 * 60 * 1000).Format("yyyy.MM.dd"));
            $("tr:eq(3) td:eq(0)").eq(0).text(new Date(myDate.getTime() - 2 * 24 * 60 * 60 * 1000).Format("yyyy.MM.dd"));
            $("tr:eq(4) td:eq(0)").eq(0).text(new Date(myDate.getTime() - 3 * 24 * 60 * 60 * 1000).Format("yyyy.MM.dd"));
            $("tr:eq(5) td:eq(0)").eq(0).text(new Date(myDate.getTime() - 4 * 24 * 60 * 60 * 1000).Format("yyyy.MM.dd"));
            $("tr:eq(6) td:eq(0)").eq(0).text(new Date(myDate.getTime() - 5 * 24 * 60 * 60 * 1000).Format("yyyy.MM.dd"));
            $("tr:eq(7) td:eq(0)").eq(0).text(new Date(myDate.getTime() - 6 * 24 * 60 * 60 * 1000).Format("yyyy.MM.dd"));
            $("tr:eq(8) td:eq(0)").eq(0).text(new Date(myDate.getTime() - 7 * 24 * 60 * 60 * 1000).Format("yyyy.MM.dd"));
            $("tr:eq(9) td:eq(0)").eq(0).text(new Date(myDate.getTime() - 8 * 24 * 60 * 60 * 1000).Format("yyyy.MM.dd"));
            $("tr:eq(10) td:eq(0)").eq(0).text(new Date(myDate.getTime() - 9 * 24 * 60 * 60 * 1000).Format("yyyy.MM.dd"));
            $("tr:eq(11) td:eq(0)").eq(0).text(new Date(myDate.getTime() - 10 * 24 * 60 * 60 * 1000).Format("yyyy.MM.dd"));
            $("tr:eq(12) td:eq(0)").eq(0).text(new Date(myDate.getTime() - 11 * 24 * 60 * 60 * 1000).Format("yyyy.MM.dd"));
            $("tr:eq(13) td:eq(0)").eq(0).text(new Date(myDate.getTime() - 12 * 24 * 60 * 60 * 1000).Format("yyyy.MM.dd"));
            $("tr:eq(14) td:eq(0)").eq(0).text(new Date(myDate.getTime() - 13 * 24 * 60 * 60 * 1000).Format("yyyy.MM.dd"));
            for (m in d) {
                var name = d[m].SeriesName;
                var time = new Date(d[m].time).Format("yyyy.MM.dd");
                
                    if (time == new Date(myDate.getTime() - 0 * 24 * 60 * 60 * 1000).Format("yyyy.MM.dd")) {
                        if (name == '当日订单完成率') {
                            $("tr:eq(1) td:eq(4)").eq(0).text(d[m].value.toFixed(2) * 100 + "%");
                        }
                        if (name == '配料设定浇注模数') {
                            $("tr:eq(1) td:eq(2)").eq(0).text(d[m].value);
                        }
                        if (name == '每天生产总模数') {
                            $("tr:eq(1) td:eq(3)").eq(0).text(d[m].value);
                        }
                        if (name == '配料备料总模数') {
                            $("tr:eq(1) td:eq(1)").eq(0).text(d[m].value);
                        }
                    }
                
                if (time == new Date(myDate.getTime() - 1 * 24 * 60 * 60 * 1000).Format("yyyy.MM.dd")) {
                    if (name == '当日订单完成率') {
                        $("tr:eq(2) td:eq(4)").eq(0).text(d[m].value.toFixed(2) * 100 + "%");
                    }
                    if (name == '配料设定浇注模数') {
                        $("tr:eq(2) td:eq(2)").eq(0).text(d[m].value);
                    }
                    if (name == '每天生产总模数') {
                        $("tr:eq(2) td:eq(3)").eq(0).text(d[m].value);
                    }
                    if (name == '配料备料总模数') {
                        $("tr:eq(2) td:eq(1)").eq(0).text(d[m].value);
                    }
                }
                if (time == new Date(myDate.getTime() - 2 * 24 * 60 * 60 * 1000).Format("yyyy.MM.dd")) {
                    if (name == '当日订单完成率') {
                        $("tr:eq(3) td:eq(4)").eq(0).text(d[m].value.toFixed(2) * 100 + "%");
                    }
                    if (name == '配料设定浇注模数') {
                        $("tr:eq(3) td:eq(2)").eq(0).text(d[m].value);
                    }
                    if (name == '每天生产总模数') {
                        $("tr:eq(3) td:eq(3)").eq(0).text(d[m].value);
                    }
                    if (name == '配料备料总模数') {
                        $("tr:eq(3) td:eq(1)").eq(0).text(d[m].value);
                    }
                }
                if (time == new Date(myDate.getTime() - 3 * 24 * 60 * 60 * 1000).Format("yyyy.MM.dd")) {
                    if (name == '当日订单完成率') {
                        $("tr:eq(4) td:eq(4)").eq(0).text(d[m].value.toFixed(2) * 100 + "%");
                    }
                    if (name == '配料设定浇注模数') {
                        $("tr:eq(4) td:eq(2)").eq(0).text(d[m].value);
                    }
                    if (name == '每天生产总模数') {
                        $("tr:eq(4) td:eq(3)").eq(0).text(d[m].value);
                    }
                    if (name == '配料备料总模数') {
                        $("tr:eq(4) td:eq(1)").eq(0).text(d[m].value);
                    }
                }
                if (time == new Date(myDate.getTime() - 4 * 24 * 60 * 60 * 1000).Format("yyyy.MM.dd")) {
                    if (name == '当日订单完成率') {
                        $("tr:eq(5) td:eq(4)").eq(0).text(d[m].value.toFixed(2) * 100 + "%");
                    }
                    if (name == '配料设定浇注模数') {
                        $("tr:eq(5) td:eq(2)").eq(0).text(d[m].value);
                    }
                    if (name == '每天生产总模数') {
                        $("tr:eq(5) td:eq(3)").eq(0).text(d[m].value);
                    }
                    if (name == '配料备料总模数') {
                        $("tr:eq(5) td:eq(1)").eq(0).text(d[m].value);
                    }
                }
                if (time == new Date(myDate.getTime() - 5 * 24 * 60 * 60 * 1000).Format("yyyy.MM.dd")) {
                    if (name == '当日订单完成率') {
                        $("tr:eq(6) td:eq(4)").eq(0).text(d[m].value.toFixed(2) * 100 + "%");
                    }
                    if (name == '配料设定浇注模数') {
                        $("tr:eq(6) td:eq(2)").eq(0).text(d[m].value);
                    }
                    if (name == '每天生产总模数') {
                        $("tr:eq(6) td:eq(3)").eq(0).text(d[m].value);
                    }
                    if (name == '配料备料总模数') {
                        $("tr:eq(6) td:eq(1)").eq(0).text(d[m].value);
                    }
                }
                if (time == new Date(myDate.getTime() - 6 * 24 * 60 * 60 * 1000).Format("yyyy.MM.dd")) {
                    if (name == '当日订单完成率') {
                        $("tr:eq(7) td:eq(4)").eq(0).text(d[m].value.toFixed(2) * 100 + "%");
                    }
                    if (name == '配料设定浇注模数') {
                        $("tr:eq(7) td:eq(2)").eq(0).text(d[m].value);
                    }
                    if (name == '每天生产总模数') {
                        $("tr:eq(7) td:eq(3)").eq(0).text(d[m].value);
                    }
                    if (name == '配料备料总模数') {
                        $("tr:eq(7) td:eq(1)").eq(0).text(d[m].value);
                    }
                }
                if (time == new Date(myDate.getTime() - 7 * 24 * 60 * 60 * 1000).Format("yyyy.MM.dd")) {
                    if (name == '当日订单完成率') {
                        $("tr:eq(8) td:eq(4)").eq(0).text(d[m].value.toFixed(2) * 100 + "%");
                    }
                    if (name == '配料设定浇注模数') {
                        $("tr:eq(8) td:eq(2)").eq(0).text(d[m].value);
                    }
                    if (name == '每天生产总模数') {
                        $("tr:eq(8) td:eq(3)").eq(0).text(d[m].value);
                    }
                    if (name == '配料备料总模数') {
                        $("tr:eq(8) td:eq(1)").eq(0).text(d[m].value);
                    }
                }
                if (time == new Date(myDate.getTime() - 8 * 24 * 60 * 60 * 1000).Format("yyyy.MM.dd")) {
                    if (name == '当日订单完成率') {
                        $("tr:eq(9) td:eq(4)").eq(0).text(d[m].value.toFixed(2) * 100 + "%");
                    }
                    if (name == '配料设定浇注模数') {
                        $("tr:eq(9) td:eq(2)").eq(0).text(d[m].value);
                    }
                    if (name == '每天生产总模数') {
                        $("tr:eq(9) td:eq(3)").eq(0).text(d[m].value);
                    }
                    if (name == '配料备料总模数') {
                        $("tr:eq(9) td:eq(1)").eq(0).text(d[m].value);
                    }
                }
                if (time == new Date(myDate.getTime() - 9 * 24 * 60 * 60 * 1000).Format("yyyy.MM.dd")) {
                    if (name == '当日订单完成率') {
                        $("tr:eq(10) td:eq(4)").eq(0).text(d[m].value.toFixed(2) * 100 + "%");
                    }
                    if (name == '配料设定浇注模数') {
                        $("tr:eq(10) td:eq(2)").eq(0).text(d[m].value);
                    }
                    if (name == '每天生产总模数') {
                        $("tr:eq(10) td:eq(3)").eq(0).text(d[m].value);
                    }
                    if (name == '配料备料总模数') {
                        $("tr:eq(10) td:eq(1)").eq(0).text(d[m].value);
                    }
                }
                if (time == new Date(myDate.getTime() - 10 * 24 * 60 * 60 * 1000).Format("yyyy.MM.dd")) {
                    if (name == '当日订单完成率') {
                        $("tr:eq(11) td:eq(4)").eq(0).text(d[m].value.toFixed(2) * 100 + "%");
                    }
                    if (name == '配料设定浇注模数') {
                        $("tr:eq(11) td:eq(2)").eq(0).text(d[m].value);
                    }
                    if (name == '每天生产总模数') {
                        $("tr:eq(11) td:eq(3)").eq(0).text(d[m].value);
                    }
                    if (name == '配料备料总模数') {
                        $("tr:eq(11) td:eq(1)").eq(0).text(d[m].value);
                    }
                }
                if (time == new Date(myDate.getTime() - 11 * 24 * 60 * 60 * 1000).Format("yyyy.MM.dd")) {
                    if (name == '当日订单完成率') {
                        $("tr:eq(12) td:eq(4)").eq(0).text(d[m].value.toFixed(2) * 100 + "%");
                    }
                    if (name == '配料设定浇注模数') {
                        $("tr:eq(12) td:eq(2)").eq(0).text(d[m].value);
                    }
                    if (name == '每天生产总模数') {
                        $("tr:eq(12) td:eq(3)").eq(0).text(d[m].value);
                    }
                    if (name == '配料备料总模数') {
                        $("tr:eq(12) td:eq(1)").eq(0).text(d[m].value);
                    }
                }
                if (time == new Date(myDate.getTime() - 12 * 24 * 60 * 60 * 1000).Format("yyyy.MM.dd")) {
                    if (name == '当日订单完成率') {
                        $("tr:eq(13) td:eq(4)").eq(0).text(d[m].value.toFixed(2) * 100 + "%");
                    }
                    if (name == '配料设定浇注模数') {
                        $("tr:eq(13) td:eq(2)").eq(0).text(d[m].value);
                    }
                    if (name == '每天生产总模数') {
                        $("tr:eq(13) td:eq(3)").eq(0).text(d[m].value);
                    }
                    if (name == '配料备料总模数') {
                        $("tr:eq(13) td:eq(1)").eq(0).text(d[m].value);
                    }
                }
                if (time == new Date(myDate.getTime() - 13 * 24 * 60 * 60 * 1000).Format("yyyy.MM.dd")) {
                    if (name == '当日订单完成率') {
                        $("tr:eq(14) td:eq(4)").eq(0).text(d[m].value.toFixed(2) * 100 + "%");
                    }
                    if (name == '配料设定浇注模数') {
                        $("tr:eq(14) td:eq(2)").eq(0).text(d[m].value);
                    }
                    if (name == '每天生产总模数') {
                        $("tr:eq(14) td:eq(3)").eq(0).text(d[m].value);
                    }
                    if (name == '配料备料总模数') {
                        $("tr:eq(14) td:eq(1)").eq(0).text(d[m].value);
                    }
                }
            }
         


            }
        });
}

//function current() {
//    var d = new Date(), str = '';
//    str += d.getFullYear() + '/'; //获取当前年份 
//    str += d.getMonth() + 1 + '/'; //获取当前月份（0——11） 
//    str += d.getDate() + ' ';
//    str += d.getHours() + ':';
//    str += d.getMinutes() + ':';
//    str += d.getSeconds() + '';
//    return str;
//}
//setInterval(function () {
//    $("#timeDiv").html(current());
//}, 1000);





Date.prototype.Format = function (fmt) {
        var o = {
            "M+": this.getMonth() + 1, //月份 
            "d+": this.getDate(), //日 
            "H+": this.getHours(), //小时 
            "m+": this.getMinutes(), //分 
            "s+": this.getSeconds(), //秒 
            "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
            "S": this.getMilliseconds() //毫秒 
        };
        if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        for (var k in o)
            if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        return fmt;

    }



convertData = function (data) {
    var res = [];
    for (var i = 0; i < data.length; i++) {
        var dataItem = data[i];
        var fromCoord = geoCoordMap[dataItem.name];
        var toCoord = [106.5, 31.5];
        if (fromCoord && toCoord) {
            res.push([{
                coord: fromCoord,
                value: dataItem.value
            }, {
                coord: toCoord
            }]);
        }
    }
    return res;
};














requestProductData()
setInterval(requestProductData, 60000*60*30)

requestDeviceData()
setInterval(requestDeviceData, 60000 * 60 * 30)

requestDeviceOeeData()
setInterval(requestDeviceOeeData, 60000 * 60 * 30)


requestTemData()
setInterval(requestTemData, 60000 * 60 * 30)

requestCusData()
setInterval(requestCusData, 60000 * 60 * 30)

requestEnergyData()
setInterval(requestEnergyData, 60000 * 60 * 30)