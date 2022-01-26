<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="Warehouse2.aspx.cs" Inherits="Modules_Warehouse_Warehouse2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%--    <script src="/js/xbreadcrumbs/jquery-1.3.2.min.js"></script>--%>
    <link href="/js/xbreadcrumbs/xbreadcrumbs.css" rel="stylesheet" />
<%--    <script src="/js/xbreadcrumbs/xbreadcrumbs.js"></script>--%>
    <script src="../../js/xbreadcrumbs/jquery-xbreadcrumbs-2.1.0.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#breadcrumbs').xBreadcrumbs();

        });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <asp:Literal ID="litbc1" runat="server"></asp:Literal>
    <br />
    <asp:TreeView ID="tview1" runat="server" LineImagesFolder="~/TreeLineImages">
    </asp:TreeView>
    <br />
</asp:Content>


 
