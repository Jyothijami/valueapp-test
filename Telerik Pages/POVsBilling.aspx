<%@ Page Language="C#" AutoEventWireup="true" CodeFile="POVsBilling.aspx.cs" Inherits="Telerik_Pages_Default" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="styles.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        </telerik:RadStyleSheetManager>
    <div>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js">
                </asp:ScriptReference>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js">
                </asp:ScriptReference>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js">
                </asp:ScriptReference>
            </Scripts>
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings >
                 <telerik:AjaxSetting AjaxControlID="RadGrid1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
         <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
        <telerik:RadFormDecorator RenderMode="Lightweight" ID="RadFormDecorator1" runat="server" DecorationZoneID="demo" DecoratedControls="All" EnableRoundedCorners="false"  />
    <div id="demo" >
       <telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="True" DataSourceID="SqlDataSource1" CellSpacing="-1" GridLines="Both" AllowFilteringByColumn="True" AllowSorting="True" ShowGroupPanel="True">
       <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
              <ClientSettings AllowDragToGroup="True"></ClientSettings>
              <MasterTableView DataSourceID="SqlDataSource1" AutoGenerateColumns="False" CommandItemDisplay="Top">
            <Columns>
                <telerik:GridBoundColumn DataField="SO_ID"  HeaderText="SO_ID" SortExpression="SO_ID" UniqueName="SO_ID" ShowFilterIcon ="false" AllowFiltering ="false"   DataType="System.Int64" ></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SO_NO"  HeaderText="SO_NO" SortExpression="SO_NO" UniqueName="SO_NO" FilterControlWidth="70%" FilterControlAltText="Filter SO_NO column"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PODate" ReadOnly="True" HeaderText="PODate" SortExpression="PODate" UniqueName="PODate" FilterControlWidth="70%" FilterControlAltText="Filter PODate column"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SI_NO" HeaderText="SI_NO" SortExpression="SI_NO" UniqueName="SI_NO" FilterControlWidth="70%" FilterControlAltText="Filter SI_NO column"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="InvoiceDt" ReadOnly="True" HeaderText="InvoiceDt" SortExpression="InvoiceDt" FilterControlWidth="70%" UniqueName="InvoiceDt" FilterControlAltText="Filter InvoiceDt column"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="custname" ReadOnly="True" HeaderText="custname" SortExpression="custname" UniqueName="custname" FilterControlAltText="Filter custname column"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="EstimatedValue" HeaderText="EstimatedValue" SortExpression="EstimatedValue" UniqueName="EstimatedValue" ShowFilterIcon ="false" AllowFiltering ="false"  FilterControlAltText="Filter EstimatedValue column"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="POValueWithGST" ReadOnly="True" HeaderText="POValueWithGST" SortExpression="POValueWithGST" UniqueName="POValueWithGST" ShowFilterIcon ="false" AllowFiltering ="false"  DataType="System.Decimal" FilterControlAltText="Filter POValueWithGST column"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="POValueWOGST" HeaderText="POValueWOGST" SortExpression="POValueWOGST" UniqueName="POValueWOGST" DataType="System.Decimal" ShowFilterIcon ="false" AllowFiltering ="false"  FilterControlAltText="Filter POValueWOGST column"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="POAdvanceValue" HeaderText="POAdvanceValue" SortExpression="POAdvanceValue" UniqueName="POAdvanceValue" DataType="System.Decimal" ShowFilterIcon ="false" AllowFiltering ="false"  FilterControlAltText="Filter POAdvanceValue column"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="InvoiceValueWithGST" HeaderText="InvoiceValueWithGST" SortExpression="InvoiceValueWithGST" UniqueName="InvoiceValueWithGST" DataType="System.Decimal" ShowFilterIcon ="false" AllowFiltering ="false"  FilterControlAltText="Filter InvoiceValueWithGST column"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="salesExecutive" ReadOnly="True" HeaderText="salesExecutive" SortExpression="salesExecutive" UniqueName="salesExecutive" FilterControlAltText="Filter salesExecutive column"></telerik:GridBoundColumn>
            </Columns>
                  
        </MasterTableView>
    </telerik:RadGrid>

    <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:DBCon %>' SelectCommand="usp_POvsInvoice" SelectCommandType="StoredProcedure">
       <%-- <SelectParameters>
            <asp:Parameter DefaultValue="5541" Name="CustID" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="EmpID" Type="Int32"></asp:Parameter>
            <asp:Parameter DbType="Date" Name="FromDt"></asp:Parameter>
            <asp:Parameter DbType="Date" Name="ToDt"></asp:Parameter>
        </SelectParameters>--%>
    </asp:SqlDataSource>
    </div>
    </div>
    </form>
</body>
</html>
