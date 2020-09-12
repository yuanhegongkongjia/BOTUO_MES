<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index1.aspx.cs" Inherits="Kanban.Index1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="Content/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/jquery-3.3.1.js"></script>
    <script type="text/javascript" src="chart/highcharts.js?time=1"></script>
</head>
<body id="app">
    <header class="animated fadeInDown" data-nav="">
        <nav class="navbar navbar-default">
            <div class="container-fluid">
                <div class="navbar-header" style="display: inline">
                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false"><span class="sr-only">Toggle navigation</span><span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></button>
                    <a class="navbar-brand" href="https://www.highcharts.com.cn/">
                        <%--<img src="img/sumin_log.JPG" title="SUMIN" alt="HIGHCHARTS">--%></a>
                </div>
                <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1" style="display: inline">
                    <ul class="nav navbar-nav navbar-right">
                        <%-- <li class="dropdown"><a href="javascript:void(0)" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">关于我们 <i class="fa fa-angle-down" aria-hidden="true"></i></a>
                            <ul class="dropdown-menu">
                                <li data-nav="about"><a href="https://www.highcharts.com.cn/about">关于我们</a></li>
                                <li><a href="https://www.highcharts.com.cn/about#contact">联系方式</a></li>
                                <li data-nav="news"><a href="https://www.highcharts.com.cn/news">新闻动态</a></li>
                                <li data-nav="parter"><a href="https://www.highcharts.com.cn/about/parter">合作伙伴</a></li>
                            </ul>
                        </li>--%>
                        <%--<li data-nav="shop-" class="nav-shop"><a href="https://highcharts.com.cn/">在线商店</a></li>--%>
                    </ul>
                    <ul class="nav navbar-nav" id="menu">
                        <li class="dropdown" data-nav="demo" id="l1"><a onclick="menutoggle('l1');" class="dropdown-toggle" data-toggle="dropdown" role="button">L01 <i class="fa fa-angle-down"></i></a>
                            <ul class="dropdown-menu">
                                <li><a href="Kanban_L01.aspx">看板</a></li>
                          

                            </ul>
                        </li>
                        <li class="dropdown" id="l2"><a onclick="menutoggle('l2');" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">L02 <i class="fa fa-angle-down" aria-hidden="true"></i></a>
                            <ul class="dropdown-menu">
                                <li><a href="Kanban_L02.aspx">看板</a></li>
                             <%--   <li><a href="L02LY">炉压</a></li>
                                <li><a href="L02MFUA.aspx">MF</a></li>
                                <li><a href="L02ENERGY1.aspx">能源</a></li>--%>
                            </ul>
                        </li>
                        <li class="dropdown" id="l3"><a onclick="menutoggle('l3');" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">L03 <i class="fa fa-angle-down" aria-hidden="true"></i></a>
                            <ul class="dropdown-menu">
                                <li><a href="Kanban_L03.aspx">看板</a></li>
                              <%--  <li><a href="L03LY">炉压</a></li>
                                <li><a href="L03MFUA.aspx">MF</a></li>
                                <li><a href="L03ENERGY1.aspx">能源</a></li>--%>
                            </ul>
                        </li>
                        <li class="dropdown" id="l4"><a onclick="menutoggle('l4');" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">L04 <i class="fa fa-angle-down" aria-hidden="true"></i></a>
                            <ul class="dropdown-menu">
                                <li><a href="Kanban_L04.aspx">看板</a></li>
                           <%--     <li><a href="L04LY">炉压</a></li>
                                <li><a href="L04MFUA.aspx">MF</a></li>
                                <li><a href="L04ENERGY1.aspx">能源1</a></li>
                                <li><a href="L04ENERGY2.aspx">能源2</a></li>--%>
                            </ul>
                        </li>
                        <%-- <li><a href="https://blog.jianshukeji.com/categories/Highcharts/" target="_blank">博客</a></li>--%>
                    </ul>
                    <%-- <div  style="margin-top:15px;margin-left:900px;">
                        <span style="color:white;" id="s1">123434</span> 
                    </div>
                    --%>
                </div>

            </div>
        </nav>
        <script>

            function menutoggle(liName) {

                var menu = $('#menu');
                var lis = menu.find('.dropdown');
                lis.each(function () {
                    //alert($(this).attr("id"));  //打印子div的ID
                    if ($(this).attr("id") == liName) {
                        if ($(this).hasClass('active open')) {
                            $(this).removeClass('active open');
                        } else {
                            $(this).addClass('active open')
                        }
                    } else {
                        $(this).removeClass('active open')
                    }

                });



            }

        </script>
    </header>

    <section class="feature">
        <div class="container">
            <div class="row" style="height: 450px">
                <div class="col-md-6 col-sm-12">
                    <h1 class="images-title" id="h1">L01停线中</h1>

                    <table class="table table-striped" id="t1" style="display: none;">
                        <thead>
                            <tr>
                                <th>AQ</th>
                                <th>线径</th>
                                <th>钢号</th>
                                <th>型号</th>
                                <th>速度</th>
                                <th>DV</th>
                                <th>客户代码</th>
                                <th>成品规格</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>

                    </table>
                   
                    <a title="Highcharts 学习交流 3 群" id="a1"></a>
                    <div id="c1" style="width: 300px; height: 350px;"></div>
                </div>
                <div class="col-md-6 col-sm-12">
                    <h1 class="images-title" id="h2">L02停线中</h1>
                    <table class="table table-striped" id="t2">
                        <thead>
                            <tr>
                                <th>AQ</th>
                                <th>线径</th>
                                <th>钢号</th>
                                <th>型号</th>
                                <th>速度</th>
                                <th>DV</th>
                                <th>客户代码</th>
                                <th>成品规格</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                 <%--   <a  id="a2"></a>--%>
                    <div id="c2" style="width: 300px; height: 350px;"></div>
                   
                </div>
            </div>
        </div>
    </section>
    <section class="clients-show" style="padding:0px;">
        <h3 class="section-title" id="environment"></h3>
    </section>
    <script>
        String.format = function () {
            if (arguments.length == 0)
                return null;
            var str = arguments[0];
            for (var i = 1; i < arguments.length; i++) {
                var re = new RegExp('\\{' + (i - 1) + '\\}', 'gm');
                str = str.replace(re, arguments[i]);
            }
            return str;
        }
        /*
        调用方式：
            var info = "我喜欢吃{0}，也喜欢吃{1}，但是最喜欢的还是{0},偶尔再买点{2}。";
            var msg=String.format(info , "苹果","香蕉","香梨")
            alert(msg);
            输出:我喜欢吃苹果，也喜欢吃香蕉，但是最喜欢的还是苹果,偶尔再买点香梨。
        */

        init();
        //function init1() {
        //    init();
        //    setInterval(
        //        init(), 120000);
        //}

        


        var chart1 = null;
        var chart2 = null;
        function initchart1(data) {
            chart1= Highcharts.chart('c1', {
                chart: {
                    type: 'column',//指定图表的类型，默认是折线图（line）

                },
                title: {
                    text: 'L01最近三天产量'                 // 标题
                },

                xAxis: {
                    type: 'datetime',
                    dateTimeLabelFormats: { day: '%m-%d' },
                    //pointInterval: 60 * 1000 // 间隔一天
                },
                yAxis: {
                    title: {
                        text: '下盘重量(KG)'                // y 轴标题
                    }
                },
                series: data,
                plotOptions: {
                    column: {
                        dataLabels: {
                            inside: true
                        }
                    }
                }
            });


        }

        function initchart2(data) {
            chart2= Highcharts.chart('c2', {
                chart: {
                    type: 'column',//指定图表的类型，默认是折线图（line）

                },
                title: {
                    text: 'L02最近三天产量'                 // 标题
                },

                xAxis: {
                    type: 'datetime',
                    dateTimeLabelFormats: { day: '%m-%d' },

                },
                yAxis: {
                    title: {
                        text: '下盘重量(KG)'                // y 轴标题
                    }
                },
                series: data,
                plotOptions: {
                    column: {
                        dataLabels: {
                            inside: true
                        }
                    }
                }
            });


        }

        function initchart3(data) {
            var chart = Highcharts.chart('c3', {
                chart: {
                    type: 'column',//指定图表的类型，默认是折线图（line）

                },
                title: {
                    text: 'L03最近三天产量'                 // 标题
                },


                xAxis: {
                    type: 'datetime',
                    dateTimeLabelFormats: { minute: '%H:%M' },
                },
                yAxis: {
                    title: {
                        text: '下盘重量'                // y 轴标题
                    }
                },
                series: data,
                plotOptions: {
                    column: {
                        dataLabels: {
                            inside: true
                        }
                    }
                }
            });


        }

        function initchart4(data) {
            var chart = Highcharts.chart('c4', {
                chart: {
                    type: 'column',//指定图表的类型，默认是折线图（line）

                },
                title: {
                    text: 'L04最近三天产量'                 // 标题
                },

                xAxis: {
                    type: 'datetime',
                    dateTimeLabelFormats: { minute: '%H:%M' },

                },
                yAxis: {
                    title: {
                        text: '下盘重量(KG)'                // y 轴标题
                    }
                },
                series: data,
                plotOptions: {
                    column: {
                        dataLabels: {
                            inside: true
                        }
                    }
                }
            });


        }







        function init() {
            $.getJSON('Handler/ProcessHandler.ashx?action=process', function (data) {
                if (data.hasError == true) {
                    alert(data.error);
                    return;
                }
                else {
                    var data1 = JSON.parse(data.data1);
                    $('#environment').html(data.data2);
                    var data = JSON.parse(data.data);

                   

                    var l1 = [];
                    var l2 = [];
                    var l3 = [];
                    var l4 = [];
                    var t1 = "";
                    var t2 = "";
                    var t3 = "";
                    var t4 = "";
                    for (m in data) {
                        if (data[m].Line == "L01") {
                            l1.push(data[m]);
                            t1 = "工艺单编号：" + data[m].InstanceId + ",工艺单名称：" + data[m].ProcessName;
                        }
                        if (data[m].Line == "L02") {
                            l2.push(data[m]);
                            t2 = "工艺单编号：" + data[m].InstanceId + ",工艺单名称：" + data[m].ProcessName;
                        }
                        if (data[m].Line == "L03") {
                            l3.push(data[m]);
                            t3 = "工艺单编号：" + data[m].InstanceId + ",工艺单名称：" + data[m].ProcessName;
                        }
                        if (data[m].Line == "L04") {
                            l4.push(data[m]);
                            t4 = "工艺单编号：" + data[m].InstanceId + ",工艺单名称：" + data[m].ProcessName;
                        }
                    }
                    var count = 2;
                    show(count, l1, l2, l3, l4, data1);
                    count = count - 1;
                    interval = setInterval(function () {

                        if (count <= 0) {
                            clearInterval(interval);
                            init();
                            return;
                        }
                        show(count, l1, l2, l3, l4,data1);
                        count = count - 1;
                    }, 60000);
                }
            })
        }

        function show(count, l1, l2, l3, l4,data1) {
            if (count == 2) {
                if (l1.length == 0) {
                    $('#h1').html("L01停线中");
                    $('#h1').css("color", "red");
                    $('#t1').css('display', 'none');
                    $('#a1').css('display', 'none');
                    $('#c1').css('display', 'block');

                    var series = [{ name: '产量', data: [] }];
                  
                    for (i in data1) {

                        var name = data1[i].CTH;
                        n = name;
                        if (name == "L01") {
                            series[0].data.push([new Date(data1[i].XPRQ).getTime(), data1[i].Weight]);
                        }
                    }
                    initchart1(series);
                    chart1.setTitle({ text: 'L01最近七天产量 '});
                }
                else {
                    $('#h1').html("L01生产中");
                    $('#h1').css("color", "green");
                    $('#t1').css('display', 'block');
                    $('#a1').css('display', 'block');
                    $('#c1').css('display', 'none');
                    //$('#a1').html(t1);
                    $("#t1 tbody").empty();
                    for (m in l1) {
                        $("#t1 tbody").prepend(String.format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td></tr>", l1[m].LinePosition, l1[m].XianJing, l1[m].GangHao, l1[m].Spec, l1[m].Speed, l1[m].DV, l1[m].CustomerCode, l1[m].ProductSpec));
                    }
                }

                if (l2.length == 0) {
                    $('#h2').html("L02停线中");
                    $('#h2').css("color", "red");
                    $('#t2').css('display', 'none');
                    $('#a2').css('display', 'none');
                    $('#c2').css('display', 'block');
                    var series = [{ name: '产量', data: [] }];
        
                    for (i in data1) {

                        var name = data1[i].CTH;
                        n = name;
                        if (name == "L02") {
                            series[0].data.push([new Date(data1[i].XPRQ).getTime(), data1[i].Weight]);
                        }
                    }
                    initchart2(series);
                    chart2.setTitle({ text:'L02最近七天产量 ' });
                }
                else {
                    $('#h2').html("L02生产中");
                    $('#h2').css("color", "green");
                    $('#t2').css('display', 'block');
                    $('#a2').css('display', 'block');
                    $('#c2').css('display', 'none');
                    //$('#a2').html(t1);
                    $("#t2 tbody").empty();
                    for (m in l2) {
                        $("#t2 tbody").prepend(String.format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td></tr>", l2[m].LinePosition, l2[m].XianJing, l2[m].GangHao, l2[m].Spec, l2[m].Speed, l2[m].DV, l2[m].CustomerCode, l2[m].ProductSpec));
                    }


                }

          
            }

            if (count == 1) {
                if (l3.length == 0) {
                    $('#h1').html("L03停线中");
                    $('#h1').css("color", "red");
                    $('#t1').css('display', 'none');
                    $('#a1').css('display', 'none');
                    $('#c1').css('display', 'block');

                    var series = [{ name: '产量', data: [] }];
                    for (i in data1) {

                        var name = data1[i].CTH;
                        if (name == "L03") {
                            //series[0].data.push(data1[i].Weight);
                            series[0].data.push([new Date(data1[i].XPRQ).getTime(), data1[i].Weight]);
                            //[new Date(d[m].time).getTime(), d[m].value]
                        }
                    }
                    initchart1(series);
                    chart1.setTitle({ text: 'L03最近七天产量 ' });
                }
                else {
                    $('#h1').html("L03生产中");
                    $('#h1').css("color", "green");
                    $('#t1').css('display', 'block');
                    $('#a1').css('display', 'block');
                    $('#c1').css('display', 'none');
                    $('#a1').html(t1);
                    $("#t1 tbody").empty();
                    for (m in l3) {
                        $("#t1 tbody").prepend(String.format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td></tr>", l3[m].LinePosition, l3[m].XianJing, l3[m].GangHao, l3[m].Spec, l3[m].Speed, l3[m].DV, l3[m].CustomerCode, l3[m].ProductSpec));
                    }
                }


                if (l4.length == 0) {
                    $('#h2').html("L04停线中");
                    $('#h2').css("color", "red");
                    $('#t2').css('display', 'none');
                    $('#a2').css('display', 'none');
                    $('#c2').css('display', 'block');

                    var series = [{ name: '产量', data: [] }];
                    for (i in data1) {

                        var name = data1[i].CTH;
                        if (name == "L04") {
                            series[0].data.push([new Date(data1[i].XPRQ).getTime(), data1[i].Weight]);
                        }
                    }
                    initchart2(series);
                    chart2.setTitle({ text: 'L04最近七天产量 ' });
                }
                else {
                    $('#h2').html("L04生产中");
                    $('#h2').css("color", "green");
                    $('#t2').css('display', 'block');
                    $('#a2').css('display', 'block');
                    $('#c2').css('display', 'none');
                   // $('#a2').html(t1);
                    $("#t2 tbody").empty();
                    for (m in l4) {
                        $("#t2 tbody").prepend(String.format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td></tr>", l4[m].LinePosition, l4[m].XianJing, l4[m].GangHao, l4[m].Spec, l1[m].Speed, l4[m].DV, l4[m].CustomerCode, l4[m].ProductSpec));
                    }
                }
                
            }
        }
    </script>

</body>

</html>
