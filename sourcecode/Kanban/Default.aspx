<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Kanban._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>博拓生产看板</h1>
        <p class="lead">博拓生产看板系统是专门为流水线车间研发的一个智能展示系统，主要展示生产数据和产线环境及能源消耗情况.</p>
        <%--<p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>--%>
    </div>

    <div class="row">
        <div class="col-md-3">
            <h2>总看板</h2>
            <p>
                展示生产、设备、车间环境、能源等情况.
            </p>
            <p>
                <a class="btn btn-default" href="index.aspx">打开看板</a>
            </p>
        </div>
        <div class="col-md-3">
            <h2>生产看板</h2>
            <p>
                展示车间的生产进度以及完成率情况.
            </p>
            <p>
                <a class="btn btn-default" href="L02Kanban.aspx">打开看板</a>
            </p>
        </div>
        <div class="col-md-3">
            <h2>环境看板</h2>
            <p>
                展示流水线车间主要环境变化情况.
            </p>
            <p>
                <a class="btn btn-default" href="L01_WSD.aspx">打开看板</a>
            </p>
        </div>
         <div class="col-md-3">
            <h2>能源管理看板</h2>
            <p>
                展示流水线车间主要能源的使用情况.
            </p>
            <p>
                <a class="btn btn-default" href="L04Kanban.aspx">打开看板</a>
            </p>
        </div>
    </div>
  <%--  <div class="row">
        <div class="col-md-4">
            <h2>能源管理看板</h2>
            <p>
                展示流水线车间主要能源的使用情况.
            </p>
            <p>
                <a class="btn btn-default" href="L04Kanban.aspx">打开看板</a>
            </p>
        </div>
        
    </div>--%>
    <script>
        function test() {
            window.open("index.aspx", 'newwindow', "height=100, width=400,top=0,left=0,toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no")
        }
    </script>
</asp:Content>
