<%@ Page Title="" Language="C#" MasterPageFile="~/DFSite.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="DynamicForm.Index" %>
<%@ Import Namespace="WFCommon.Utility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/style.css?time=132344" rel="stylesheet" type="text/css" />
    <link href="css/ddz_bui.css?time=1233" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body {
            overflow: hidden;
            word-break: break-all;
        }
    </style>
    <%--<script type="text/javascript" src="<%= ClientScript.GetWebResourceUrl(typeof(WFMessager.WFMessagerHandler), "WFMessager.jquery-poll-message.js") %>"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="bigtupdiv">
        <div class="topdivL"><%= WFCommon.Utility.WFCorePublicCodeHelper.GetPublicCode("DF_SYSTEM", "COMPANY_NAME","公司名称").GetRes() %></div>
        <div class="topdivR">
            <div class="topdivR_one">
                <%--<div class="topdivR_oneI">
                    <img src="images/index3.png" width="40" height="40" />
                </div>--%>
                <div class="topdivR_oneN">
                    <span runat="server" id="userName"></span>
                </div>
                <div class="topdivR_oneE">
                    <a href="#" onclick="exit()">
                        <img src="images/exit.png" width="16" height="18" /></a>
                </div>
                <div style="clear: both"></div>
            </div>
            <%--<div class="topdivR_four"><a href="#" id="btnMsg"><%= "我的消息".GetRes() %></a></div>--%>
            <div style="clear: both"></div>
        </div>
        <div style="clear: both"></div>
    </div>
    <script type="text/javascript">
        function deleteCookie(name) {
            var date = new Date();
            date.setTime(date.getTime() - 10000);
            document.cookie = name + "=-1; expire=" + date.toGMTString() + "; path=/";
        }
        function exit() {
            deleteCookie("<%= SystemConstants.DF_LANG %>");
            deleteCookie("<%= SystemConstants.DF_TOKEN %>");
            deleteCookie("<%= SystemConstants.DF_USER %>");
            window.location.href = "Login.aspx";
        }

        $('#btnChangePassword').on("click", function () {
            $('#ddz_mainFrame').attr('src', 'DFIndex.aspx?DF_FORMNAME=WF_ChangePassword');
        });
    </script>

    <div id="ddz_bigmaindiv" class="bigmaindiv">
        <div class="bigmainL" id="m2" style="height: 9999px;">
        </div>
        <div class="bigmainR_main">
            <iframe id="ddz_mainFrame" style="border: 0px; height: 9999px;" width="100%"></iframe>
        </div>
    </div>


    <script type="text/javascript">

        var iframe = document.getElementById('ddz_mainFrame');
        var leftMenu = document.getElementById('m2');
        function resizeIframe() {
            var height = document.documentElement.clientHeight - 60;
            height = (height < 0) ? 0 : height;
            iframe.style.height = height + 'px';
            leftMenu.style.height = height + 'px';
        }
        // .onload doesn't work with IE8 and older.
        if (iframe.attachEvent) {
            iframe.attachEvent("onload", resizeIframe);
        } else {
            iframe.onload = resizeIframe;
        }
        window.onresize = resizeIframe;

        BUI.use('bui/menu', function (Menu) {
            var config = BUI.JSON.parse('<%= WFCommon.Utility.JsonSerializeHelper.SerializeObject(WFDataAccess.MenuLoader.GetConfig(DynamicForm.Util.GetCurrentUser().UserId)) %>');
            BUI.each(config, function (a) {
                if (a.items.length > 0) {
                    var m2 = a.items;
                    BUI.each(m2, function (b) {
                        if (b.childs.length > 0) {
                            b.subMenu = new Menu.ContextMenu({
                                children: b.childs, autoHideType: 'leave', listeners: {
                                    itemclick: function (ev) {
                                        $('#ddz_mainFrame').attr('src', ev.item.get('href'));
                                    }
                                }
                            });
                            b.subMenuAlign = {
                                points: ['tr', 'tl'],
                                offset: [0, 0]
                            };
                        }
                    })
                }
            });

            var sideMenu = new Menu.SideMenu({
                render: '#m2',
                itemTpl: '<div class="bui-menu-title"><div class="ddz-icon"><img src="{icon}" class="ddz-side-menu-img" width="16" height="16"></div><span class="bui-menu-title-text">{text}</span></div>',
                items: config
            });
            sideMenu.render();
            sideMenu.on('menuclick', function (e) {
                $('#ddz_mainFrame').attr('src', e.item.get('href'));
            });
            sideMenu.on('itemclick', function (e) {
                $('.bui-menu-title').css('background-position-x', $('.bui-menu-title').width() - 18);
            });
            resizeIframe();
            $('.bui-menu-title').css('background-position-x', $('.bui-menu-title').width() - 18);
        });

        //$('#btnMsg').pollMessage({
        //    'url': 'Handlers/WFMessagerHandler.ashx?action=queryMessage',
        //    'duration': 10,
        //    'click': function (msg) {
        //        $('#ddz_mainFrame').attr('src', 'DFIndex.aspx?DF_FORMNAME=WF_MyMsg');
        //    }
        //}).start();
    </script>

</asp:Content>
