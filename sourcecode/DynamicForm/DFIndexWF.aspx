<%@ Page Title="" Language="C#" MasterPageFile="DFSite.Master" AutoEventWireup="true" CodeBehind="DFIndexWF.aspx.cs" Inherits="DynamicForm.DFIndexWF" %>

<%@ Register Assembly="DynamicForm.Core" Namespace="DynamicForm.Core" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <cc1:ucForm ID="UcForm4" runat="server" DFFormName="WFCore_Panel_RequestorInfo" />
    <cc1:ucForm ID="UcForm1" runat="server" />
    <cc1:ucForm ID="UcForm2" runat="server" DFFormName="WFCore_Panel_Approve" />
    <cc1:ucForm ID="UcForm3" runat="server" DFFormName="WFCore_Panel_ApproveHistory" />
</asp:Content>
