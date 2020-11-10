var myChart = echarts.init(document.getElementById('container1'));
//var myChart1 = echarts.init(document.getElementById('container1'));
initChart();

//initChart1();
function initChart(){

const colorList = ["#9E87FF", '#73DDFF', '#fe9a8b', '#F56948', '#9E87FF']	
var option =  {
    xAxis: {
        type: 'category',
        data: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun']
    },
    yAxis: {
        type: 'value',
        splitLine:{
        	show:false}
    },
    series: [{
        data: [820, 932, 901, 934, 1290, 1330, 1320],
        type: 'line',
        smooth: true,
        showSymbol: false,
        lineStyle: {
                width: 5,
                color: new echarts.graphic.LinearGradient(1, 1, 0, 0, [{
                        offset: 0,
                        color: '#73DD39'
                    },
                    {
                        offset: 1,
                        color: '#73DDFF'
                    }
                ]),
                shadowColor: 'rgba(115,221,255, 0.3)',
                shadowBlur: 10,
                shadowOffsetY: 20
            },
            itemStyle: {
                normal: {
                    color: colorList[1],
                    borderColor: colorList[1]
                }
            }
        },
      
        {
        data: [820, 932, 901, 934, 1290, 1330, 1320],
        type: 'line',
        smooth: true,
        showSymbol: false,
             lineStyle: {
                width: 5,
                color: new echarts.graphic.LinearGradient(0, 0, 1, 0, [{
                        offset: 0,
                        color: '#fe9a'
                    },
                    {
                        offset: 1,
                        color: '#fe9a8b'
                    }
                ]),
                shadowColor: 'rgba(254,154,139, 0.3)',
                shadowBlur: 10,
                shadowOffsetY: 20
            },
            itemStyle: {
                normal: {
                    color: colorList[4],
                    borderColor: colorList[4]
                }
            }
        },
          {
        data: [820, 932, 901, 934, 1290, 1330, 1320],
        type: 'line',
        smooth: true,
        showSymbol: false,
              lineStyle: {
                width: 5,
                color: new echarts.graphic.LinearGradient(0, 1, 0, 0, [{
                        offset: 0,
                        color: '#9effff'
                    },
                    {
                        offset: 1,
                        color: '#9E87FF'
                    }
                ]),
                shadowColor: 'rgba(254,154,139, 0.3)',
                shadowBlur: 10,
                shadowOffsetY: 20
            },
            itemStyle: {
                normal: {
                    color: colorList[3],
                    borderColor: colorList[3]
                }
            }
        }
        
    ]

};
 myChart.setOption(option);

 initChartOEE();

}




function initChartOEE(){
var labelOption = {
    normal: {
        show: true,
        align: 'left',
        verticalAlign: 'middle',
        formater:function(a,b,c){ return c;},
        fontSize: 16

    }
};

var Position=$('#MachineName').val()
var legend=[Position];
var yName="OEE";
var xName="日期";

$.getJSON('../DataAnalyzeHandler.ashx?action=oeequerycurrent',
      {Position:Position,
      CollectDateFrom:$('#TimeFrom').val(),
      CollectDateTo:$('#TimeTo').val()
             }, function (data) {
                if (data.hasError == true) {
                    alert(data.error);
                    return;
                }
                else {
                   var d1=JSON.parse(data.data1);
                    var d=JSON.parse(data.data);
                    var categories=[];
                    var s=[];
                    //var s = [{ name: '订单计划量', data: [] }, { name: '订单完成量', data: [] }];
                    var lengde=[];
                    for(m in d1){
                      categories[m]=d1[m];
                    }

                    if(Position.length==0){
                    	for(m in d){
						dm=[];
                        for(n in d[m].data_vm){
 							dm.push([d[m].data_vm[n].CTime,d[m].data_vm[n].MachineOEE]);
                        }
                        s.push({name:d[m].name,data:dm,type: 'line',label:labelOption});
                    }
                    for(m in s){
                      lengde[m]=s[m].name;
                    }

                  myChart.setOption({
                      legend: {
                         data:lengde,
                         right:'20'
                       },
					title:[{
						text:'所有设备OEE分析',
						left:'center'
					}],
                      xAxis: {
                       type: 'category',
                       axisTick: {show: false},
                        data: categories,
                        name:xName
                      },
                      yAxis: [{
                        type: 'value',
                        name:yName
                      }],
                    series: 
                       s
                   });





                    }
                    else{
                    for(m in d){
						dm=[];
                        for(n in d[m].data_vm){
 							dm.push([d[m].data_vm[n].CTime,d[m].data_vm[n].MachineOEE]);
                        }
                        s.push({name:d[m].name,data:dm,type: 'line',label:labelOption});
                      }

                   myChart.setOption({
                      legend: {
                         data:legend,
                         right:'20'
                       },
					title:[{
						text:$('#MachineName').val()+'设备OEE分析',
						left:'center'
					}],
                      xAxis: {
                       type: 'category',
                       axisTick: {show: false},
                        data: categories,
                        name:xName
                      },
                      yAxis: [{
                        type: 'value',
                        name:yName
                      }],
                    series: 
                       s
                  });



                     }
     //                 myChart.setOption({
     //                  legend: {
     //                     data:legend,
     //                     right:'20'
     //                   },
					// title:[{
					// 	text:$('#MachineName').val()+'设备OEE分析',
					// 	left:'center'
					// }],
     //                  xAxis: {
     //                   type: 'category',
     //                   axisTick: {show: false},
     //                    data: categories,
     //                    name:xName
     //                  },
     //                  yAxis: [{
     //                    type: 'value',
     //                    name:yName
     //                  }],
     //                series: 
     //                   s
     //              });



                }

     });
}