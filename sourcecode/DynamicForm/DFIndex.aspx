<%@ Page Title="" Language="C#" MasterPageFile="DFSite.Master" AutoEventWireup="true" CodeBehind="DFIndex.aspx.cs" Inherits="DynamicForm.DFIndex" %>

<%@ Register Assembly="DynamicForm.Core" Namespace="DynamicForm.Core" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dfLayout;
        $(document).ready(function () {
            dfLayout = $('body').layout({ applyDemoStyles: true });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <cc1:ucForm ID="UcForm1" runat="server" />
</asp:Content>
