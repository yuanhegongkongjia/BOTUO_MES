<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Kanban._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>苏闽生产看板</h1>
        <p class="lead">苏闽生产看板系统是专门为流水线车间研发的一个智能展示系统，主要展示工艺数据和产线能源消耗情况.</p>
        <%--<p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>--%>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>一号线工艺看板</h2>
            <p>
                展示一号线的炉温、MF功率、生产速度情况.
            </p>
            <p>
                <a class="btn btn-default" href="L01Kanban.aspx">打开看板</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>二号线工艺看板</h2>
            <p>
                展示一号线的炉温、MF功率、生产速度情况.
            </p>
            <p>
                <a class="btn btn-default" href="L02Kanban.aspx">打开看板</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>三号线工艺看板</h2>
            <p>
                展示一号线的炉温、MF功率、生产速度情况.
            </p>
            <p>
                <a class="btn btn-default" href="L03Kanban.aspx">打开看板</a>
            </p>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <h2>四号线工艺看板</h2>
            <p>
                展示一号线的炉温、MF功率、生产速度情况.
            </p>
            <p>
                <a class="btn btn-default" href="L04Kanban.aspx">打开看板</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>能源管理看板</h2>
            <p>
                展示流水线车间主要能源的使用情况.
            </p>
            <p>
                <a class="btn btn-default" href="EnergyKanban.aspx">打开看板</a>
            </p>
        </div>
    </div>
    <script>
        function test() {
            window.open("L01Kanban.aspx", 'newwindow', "height=100, width=400,top=0,left=0,toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no")
        }
    </script>
</asp:Content>
