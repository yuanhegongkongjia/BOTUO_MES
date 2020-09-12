<%@ Page Title="" Language="C#" MasterPageFile="DFSite.Master" AutoEventWireup="true" CodeBehind="DFIndexReport.aspx.cs" Inherits="DynamicForm.DFIndexReport" %>

<%@ Register Assembly="DynamicForm.Core" Namespace="DynamicForm.Core" TagPrefix="cc1" %>

<%@ Register Assembly="FastReport.Web" Namespace="FastReport.Web" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <cc1:ucForm ID="UcForm1" runat="server" />
    <div class="text-align-center">
        <cc2:WebReport ID="WebReport1" runat="server" Width="100%" Height="100%" OnStartReport="WebReport1_StartReport" />
    </div>
</asp:Content>
