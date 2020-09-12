<%@ Page Title="" Language="C#" MasterPageFile="~/DFSite.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DynamicForm.Login" %>

<%@ Import Namespace="WFCommon.Utility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        * {
            padding: 0px;
            margin: 0px;
            font-family: '微软雅黑';
        }

        body {
            padding: 0px;
            margin: 0px;
        }

        #ddz-loginbig {
            width: 100%;
            height: 550px;
            position: absolute;
            left: 0%;
            top: 50%;
            background: #093f75;
            margin-top: -275px;
        }

        #ddz-loginmain {
            width: 1024px;
            margin-left: 50px;
            margin-right: auto;
            height: 550px;
            background: url(<%=WFCommon.Utility.WFCorePublicCodeHelper.GetPublicCode("DF_SYSTEM", "LOGIN_BACKGROUND", "images/company1.png") %>) no-repeat left top;
        }

        #ddz-logindiv {
            width: 220px;
            height: 330px;
            padding-left: 30px;
            padding-right: 30px;
            background: url(images/kuang.jpg) no-repeat;
            float: right;
            margin-right: -126px;
            margin-top: 102px;
            clear: both;
        }

        #ddz-logindivT {
            height: 24px;
            border-bottom: #034893 solid 1px;
            font-size: 18px;
            color: #034893;
            padding-top: 30px;
            font-family: Arial, Helvetica, sans-serif;
            margin-bottom: 22px;
            margin-left: -20px;
            margin-right: -20px;
            text-align: center;
        }

            #ddz-logindivT span {
                font-size: 16px;
            }

        .ddz-logindivM1 {
            height: 37px;
            margin-bottom: 10px;
            background: url(images/user.jpg) no-repeat;
        }


        #ContentPlaceHolder1_user {
            margin-left: 45px;
            margin-top: 3px;
            height: 29px;
            width: 163px;
            border: none;
            background-color: none;
            font-size: 16px;
        }

        #ContentPlaceHolder1_password {
            margin-left: 45px;
            margin-top: 3px;
            height: 29px;
            width: 163px;
            border: none;
            background-color: none;
            font-size: 16px;
        }

        .ddz-logindivM2 {
            height: 37px;
            margin-bottom: 10px;
            background: url(images/password.png) no-repeat;
        }

        .ddz-logindivM2L {
            float: left;
            width: 163px;
            height: 37px;
            margin-bottom: 10px;
            background: url(images/yanzheng.jpg) no-repeat;
        }

        .ddz-logindivM2R {
            float: right;
            width: 85px;
            height: 35px;
            padding-left: 5px;
            padding-top: 5px;
            background: url(images/mak.jpg) no-repeat;
        }

        #ContentPlaceHolder1_yanzheng {
            margin-left: 45px;
            margin-top: 3px;
            height: 29px;
            width: 68px;
            border: none;
            background-color: none;
            font-size: 16px;
        }

        #btnLogin {
            height: 37px;
            width: 220px;
            background: url(images/login.jpg) no-repeat;
            border: none;
            color: #fff;
            line-height: 37px;
            text-align: center;
            font-family: '微软雅黑';
            outline: medium;
        }

        #ContentPlaceHolder1_btnLogin {
            height: 37px;
            width: 220px;
            background: url(images/login.jpg) no-repeat;
            border: none;
            color: #fff;
            line-height: 37px;
            text-align: center;
        }

        .ddz-foot {
            width: 1024px;
            margin-left: auto;
            margin-right: auto;
            height: 21px;
            line-height: 21px;
            font-size: 12px;
            color: #666;
            margin-top: 15px;
        }

        .css_dpLanguage {
            width: 178px;
            margin-left: 42px;
            height: 36px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="ddz-loginbig">
        <div id="ddz-loginmain">
            <div id="ddz-logindiv">
                <div id="ddz-logindivT"><%=WFCommon.Utility.WFCorePublicCodeHelper.GetPublicCode("DF_SYSTEM", "SYSTEM_NAME", "系统名称").GetRes() %></div>
                <div class="ddz-logindivM1">
                    <input type="text" id="user" runat="server" />
                </div>
                <div class="ddz-logindivM2">
                    <input type="password" id="password" runat="server" />
                </div>
                <div class="ddz-logindivM2L">
                    <asp:DropDownList ID="dpLanguage" CssClass="css_dpLanguage" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="dpLanguage_SelectedIndexChanged">
                                               <%-- <asp:ListItem Value="English">English</asp:ListItem>--%>
                        <asp:ListItem Value="ChineseSimplified">简体中文</asp:ListItem>
<%--                        <asp:ListItem Value="ChineseTraditional">繁体中文</asp:ListItem>--%>
                    </asp:DropDownList>
                </div>
                <div style="clear: both"></div>
                <div id="btnLoginDiv">
                    <input type="button" runat="server" id="btnLogin" name="btnLogin" value="Login" />
                </div>
            </div>
        </div>

        <div class="ddz-foot">
            <p>
                <span><%= WFCommon.Utility.WFCorePublicCodeHelper.GetPublicCode("DF_SYSTEM", "SYSTEM_COPYRIGHT", "版权所有 @ 2016 版权信息").GetRes() %></span>
            </p>
        </div>
    </div>
    <script type="text/javascript">
        $('#ContentPlaceHolder1_imExPwd').on("click", function () {
            $('#ContentPlaceHolder1_imExPwd').attr("src", "ShowExPwd.aspx?" + Date());
        });

        $("body").bind('keyup', function (event) {
            if (event.keyCode == 13) {
                //document.form.submit();
                $('#ContentPlaceHolder1_btnLogin').click();
            }
        });
    </script>
</asp:Content>
