<%@ Page Title="" Language="C#" MasterPageFile="~/KanbanSite.Master" AutoEventWireup="true" CodeBehind="BT-POWERKANBAN.aspx.cs" Inherits="Kanban.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
<%--<div class="page-header">
    <div class="logo" style="width:100%">
        <img src="img/zhongding.jpg" style="width:60px;height:60px;margin-left: 20px;" />
    <span style="font-size: 32px; color: white;display:inline-block;width:75%;text-align:center;">博拓电能监控看板</span>
        <span style="color: white;width:200px;font-size: 16px;" id="timeDiv"></span>
    </div>
</div> --%>
        <div class="header">
     <div class="a-access" style="width:100%">
     <img src="img/zhongding.png" style="width:48px;height:48px;margin-left: 20px; top:1%; left:5%;" />
    
     <span style="color: #00ffff;width:200px;font-size: 35px;text-align:center;" >博拓车间电能监控看板</span>
       
     <span style="color: #00ffff;width:200px;font-size: 16px;position: absolute; top:25%; right:3%;" id="timeDiv">2016-12-21 上午13:00:00</span>
            
     </div>
     </div>
<%--<div class="row">
    <div class="col-md-6 col-sm-12" style="position: absolute;
    padding-top: 114px;
    margin-top: -114px;
    height: 100%;padding-bottom:20px">
        <div id="container1" style="width:100%; height:100%;margin-left:10px"></div>
    </div>
    <div class="col-md-6 col-sm-12" style="position: absolute;
    padding-top: 114px;
    margin-top: -114px;
    margin-left :640px;
    height: 100%;padding-bottom:20px">
        <div id="container2" style="width:100%; height:100%;margin-left:10px"></div>
    </div>
</div>   --%>
         <div class="main clearfix">
        <div class="main-left">
            <div class="border-container">
                <div class="name-title"></div>
                <div id="container1"></div>
                <span class="top-left border-span"></span>
                <span class="top-right border-span"></span>
                <span class="bottom-left border-span"></span>
                <span class="bottom-right border-span"></span>
            </div> 
           
        </div>
     <div class="main-right">
            <div class="border-container">
                <div class="name-title"></div>
                <div id="container2"></div>
                <span class="top-left border-span"></span>
                <span class="top-right border-span"></span>
                <span class="bottom-left border-span"></span>
                <span class="bottom-right border-span"></span>
            </div> 
           
        </div>
</div>
  <script>
      function current() {
          var d = new Date(), str = '';//在盒子里面，margin和padding作用相同，飞出盒子用margin，absolute是脱离文档，充当整个页面里面
          str += d.getFullYear() + '/'; //获取当前年份 
          str += d.getMonth() + 1 + '/'; //获取当前月份（0——11） 
          str += d.getDate() + ' ';
          str += d.getHours() + ':';
          str += d.getMinutes() + ':';
          str += d.getSeconds() + '';
          return str;
      }
      Highcharts.setOptions({ global: { useUTC: false } });
      setInterval(function () {
          $("#timeDiv").html(current());
      }, 1000);
      init1();
      init2();
      function init1() {
              requestData();
      }
      function init2() {
              requestData1();
      }
      function initChart1() {
          // 图表配置
          chart1 = Highcharts.chart('container1', {
              chart: {
                  type: 'line',//指定图表的类型，默认是折线图（line）
                  backgroundColor:
                      'rgba(0,0,0,0)'
              },
              colors: [
                  'rgba(255,69,0,0.99)', 'rgba(173,255,47,0.99)', '#FFF263', '#6AF9C4'],
              title: {
                  text: '车间当天电量监控',          // 标题
                  style: {
                      color: 'white'
                  }
              },
              subtitle: {
                  text: '电量分时统计',
                  style: {
                      color: 'white'
                  }
              },

              xAxis: {
                  type: 'datetime',
                  dateTimeLabelFormats: { hour: "%m-%e %H:%M" }//,
              },
              yAxis: {
                  title: {
                      text: '度(Kwh)',              // y 轴标题
                      style: {
                          color: 'rgba(173,255,47,0.99)'
                      }
                  }
              },
              legend: {

                  itemStyle: {
                      color: 'white',

                  },

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
      function initChart2() {
          // 图表配置
          chart2 = Highcharts.chart('container2', {
              chart: {
                  type: 'line',//指定图表的类型，默认是折线图（line）
                  backgroundColor:
                      'rgba(0,0,0,0)'
              },
              title: {
                  text: '车间每日电量监控',              // 标题
                  style: {
                      color: 'white'
                  }
              },
              subtitle: {
                  text: '电量每日统计',
                  style: {
                      color: 'white'
                  }
              },

              xAxis: {
                  type: 'datetime',
                  dateTimeLabelFormats: { day: "%m-%e" }//需要修改成几号
              },
              yAxis: {
                  title: {
                      text: '度(Kwh)'                // y 轴标题
                  }
              },
              legend: {

                  itemStyle: {
                      color: 'white',

                  },

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
          function requestData(){
              $.getJSON('../Handler/ProcessHandler.ashx?action=l01dianneng', function (data) {
                  if (data.hasError == true) {
                      alert(data.error);
                      return;
                  }
                  else {
                      //initFirstLine();
                      d = JSON.parse(data.data);
                      var names = [];
                      var series1 = [{ name: '高压球磨机', data: [] }, { name: '低压车间', data: [] }];
                      cs1 = [];
                      cs2 = [];
                      cs3 = [];
                      cs4 = [];
                      for (m in d) {
                          var name = d[m].SeriesName;
                          if (name == "高压球磨机") {
                          series1[0].data.push([new Date(d[m].time).getTime(), d[m].value]);
                          }
                          if (name == "低压车间") {
                              series1[1].data.push([new Date(d[m].time).getTime(), d[m].value]);
                          }
                                             
                      }
                      initChart1();
                      if (chart1.series.length == 0) {
                          for (i in series1) {
                              chart1.addSeries({
                                  name: series1[i].name,
                                  data: series1[i].data
                              });
                          }
                      } else {
                          chart1.update({
                              series: series1
                          });
                      }
                      setTimeout(requestData, 3610000);
                  }
              });
      }
      function requestData1() {
          $.getJSON('../Handler/ProcessHandler.ashx?action=l01diannengDAY', function (data) {
              if (data.hasError == true) {
                  alert(data.error);
                  return;
              }
              else {
                  //initFirstLine();
                  d = JSON.parse(data.data);
                  var names = [];
                  var series2 = [{ name: '高压球磨机', data: [] }, { name: '低压车间', data: [] }];
                  cs1 = [];
                  cs2 = [];
                  cs3 = [];
                  cs4 = [];
                  for (m in d) {
                      var name = d[m].SeriesName;
                      if (name == "高压球磨机") {
                          series2[0].data.push([new Date(d[m].time).getTime(), d[m].value]);
                      }
                      if (name == "低压车间") {
                          series2[1].data.push([new Date(d[m].time).getTime(), d[m].value]);
                      }

                  }
                  initChart2();
                  if (chart2.series.length == 0) {
                      for (i in series2) {
                          chart2.addSeries({
                              name: series2[i].name,
                              data: series2[i].data
                          });
                      }
                  } else {
                      chart2.update({
                          series: series2
                      });
                  }
                  setTimeout(requestData, 86400000);
              }
          });
      }
     
     
  </script>
</asp:Content>
