<%@ Page Title="" Language="C#" MasterPageFile="~/KanbanSite.Master" AutoEventWireup="true" CodeBehind="L01MFUA.aspx.cs" Inherits="Kanban.L01MFUA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <div class="logo">
            <%--<img src="img/sumin_log.JPG" style="width: 60px; height: 60px;" onclick="test();" />--%>
            <span style="font-size: 28px; color: white;" onclick="requestFullScreen();">一号线MF电压/功率看板</span>
            <%--  <h1 style="/*margin-left:600px;*/">一号线生产看板</h1>--%>
            <span style="color: white;" id="timeDiv"></span>
            <button class="button-primary" onclick="prePage()">上一页</button>
           
            <button class="button-info" onclick="homePage()">回到首页</button>
            <button class="button-success" onclick="nextPage()">下一页</button>
        </div>
    </div>
   <div class="row">
        <div class="col-md-6 col-sm-12">
            <div id="container1" style="min-width: 400px; height: 280px;"></div>
        </div>
        <div class="col-md-6 col-sm-12">
            <div id="container2" style="min-width: 400px; height: 280px;"></div>
        </div>
    </div>
    <div class="row" style="margin-top:10px;">
        <div class="col-md-6 col-sm-12">
            <div id="container3" style="min-width: 400px; height: 280px;"></div>
        </div>
        <div class="col-md-6 col-sm-12">
            <div id="container4" style="min-width: 400px; height: 280px;"></div>
        </div>
    </div>
     <script>
         Highcharts.setOptions({
             chart: {
                 backgroundColor: {
                     linearGradient: [0, 0, 500, 500],
                     stops: [
                         [0, 'rgb(255, 255, 255)'],
                         [1, 'rgb(240, 240, 255)']
                     ]
                 },
                 plotBackgroundColor: 'rgba(208, 208, 208, .5)',
                 borderWidth: 1,
                 //plotBackgroundColor: 'rgba(255, 255, 255, .9)',
                 plotShadow: false,
                 plotBorderWidth: 1
             },
             global: { useUTC: false }
         });
         var chart1 = null;
         var chart2 = null;
         var chart3 = null;
         var chart4 = null;
         function current() {
             var d = new Date(), str = '';
             str += d.getFullYear() + '/'; //获取当前年份 
             str += d.getMonth() + 1 + '/'; //获取当前月份（0——11） 
             str += d.getDate() + ' ';
             str += d.getHours() + ':';
             str += d.getMinutes() + ':';
             str += d.getSeconds() + '';
             return str;
         }
         setInterval(function () {
            
             //$("#timeDiv").html(current());
         }, 1000);

  


         function prePage() {
             window.location.href = "L01LW.aspx";
         }

         function nextPage() {
             window.location.href = "L01ENERGY1.aspx";
         }

         function homePage() {
             window.location.href = "Index1.aspx";
         }

         init();
         function init() {

             requestData();
             window.resizeTo(screen.availWidth, screen.availHeight);
             setTimeout(function () {
                 window.location.href = "L01ENERGY1.aspx";
             }, 60000);
         }
         //requestData();
         function initChart1() {
             // 图表配置
             chart1 = Highcharts.chart('container1', {
                 chart: {
                     //type: 'column',//指定图表的类型，默认是折线图（line）
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
                 yAxis: [{ 
                     title: {
                         text: '电压(V)',
                         style: {
                             color: '#89A54E'
                         }
                     },
                    
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
                             enabled: true
                         }
                     }
                 }
             });

         }
         function initChart2() {
             // 图表配置
             chart2 = Highcharts.chart('container2', {
                 chart: {
                     type: 'column',//指定图表的类型，默认是折线图（line）
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
                     },

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
                             enabled: true
                         }
                     }
                 }
             });

         }

         function initChart3() {
             // 图表配置
             chart3 = Highcharts.chart('container3', {
                 chart: {
                     type: 'column',//指定图表的类型，默认是折线图（line）
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
                     },

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
                             enabled: true
                         }
                     }
                 }
             });

         }
         function initChart4() {
             // 图表配置
             chart4 = Highcharts.chart('container4', {
                 chart: {
                     type: 'column',//指定图表的类型，默认是折线图（line）
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
                     },

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
                             enabled: true
                         }
                     }
                 }
             });

         }

         function  requestData()
         {
             $.getJSON('../Handler/ProcessHandler.ashx?action=l01ua', function (data) {
                 if (data.hasError == true) {
                     alert(data.error);
                     return;
                 }
                 else {
                     //initFirstLine();
                     d = JSON.parse(data.data);
                     var names = [];
                     var series1 = [{name: '设定值', data: [] }, { name: '实际值', data: [] }, { name: 'A1功率',data:[]}];
                     var series2 = [{ name: '设定值',  data: [] }, { name: '实际值', data: [] }, { name: 'A2功率', data: [] }];
                     var series3 = [{ name: '设定值', data: [] }, { name: '实际值', data: [] }, { name: 'B1功率', data: [] }];
                     var series4 = [{ name: '设定值', data: [] }, { name: '实际值',  data: [] }, { name: 'B2功率', data: [] }];
                     
                     cs1 = [];
                     cs2 = [];
                     cs3 = [];
                     cs4 = [];
                     for (m in d) {
                         var name = d[m].SeriesName;
                         if (name == "A1电压") {
                             if (d[m].ParamType == "设定值") {
                                 series1[0].data.push([new Date(d[m].time).getTime(), d[m].value]);
                             }
                             if (d[m].ParamType == "实际值") {
                                 series1[1].data.push([new Date(d[m].time).getTime(), d[m].value]);
                             }
                         }
                         if (name == "A1功率") {
                             series1[2].data.push([new Date(d[m].time).getTime(), d[m].value/1000.0]);
                         }
                         if (name == "A2电压") {
                             if (d[m].ParamType == "设定值") {
                                 series2[0].data.push([new Date(d[m].time).getTime(), d[m].value]);
                             }
                             if (d[m].ParamType == "实际值") {
                                 series2[1].data.push([new Date(d[m].time).getTime(), d[m].value]);
                             }
                         }
                         if (name == "A2功率") {
                             series2[2].data.push([new Date(d[m].time).getTime(), d[m].value / 1000.0]);
                         }
                         if (name == "B1电压") {
                             if (d[m].ParamType == "设定值") {
                                 series3[0].data.push([new Date(d[m].time).getTime(), d[m].value]);
                             }
                             if (d[m].ParamType == "实际值") {
                                 series3[1].data.push([new Date(d[m].time).getTime(), d[m].value]);
                             }
                         }
                         if (name == "B1功率") {
                             series3[2].data.push([new Date(d[m].time).getTime(), d[m].value / 1000.0]);
                         }

                         if (name == "B2电压") {
                             if (d[m].ParamType == "设定值") {
                                 series4[0].data.push([new Date(d[m].time).getTime(), d[m].value]);
                             }
                             if (d[m].ParamType == "实际值") {
                                 series4[1].data.push([new Date(d[m].time).getTime(), d[m].value]);
                             }
                         }
                         if (name == "B2功率") {
                             series4[2].data.push([new Date(d[m].time).getTime(), d[m].value / 1000.0]);
                         }
                   
                       
                     }

                     initChart1();
                     initChart2();
                     initChart3();
                     initChart4();
                  
                     if (chart1.series.length == 0) {
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

                        
                     } else {
                         chart1.update({
                             series: series1
                         });
                     }

                     if (chart2.series.length == 0) {
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

                         //for (i in series2) {
                         //    chart2.addSeries({
                         //        name: series2[i].name,
                         //        data: series2[i].data
                         //    });
                         //}
                     } else {
                         chart2.update({
                             series: series2
                         });
                     }

                     if (chart3.series.length == 0) {
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
                         //for (i in series3) {
                         //    chart3.addSeries({
                         //        name: series3[i].name,
                         //        data: series3[i].data
                         //    });
                         //}
                     } else {
                         chart3.update({
                             series: series3
                         });
                     }

                     if (chart4.series.length == 0) {
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
                         //for (i in series4) {
                         //    chart4.addSeries({
                         //        name: series4[i].name,
                         //        data: series4[i].data
                         //    });
                         //}
                     } else {
                         chart4.update({
                             series: series4
                         });
                     }          
                 }
             });
         }
         </script>
</asp:Content>
