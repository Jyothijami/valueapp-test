<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="POvsBilling.aspx.cs" Inherits="Modules_Reports_SM_POvsBilling" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
     

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
    <div id="demo" class="demo-container no-bg">
         <telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="True" DataSourceID="SqlDataSource1" CellSpacing="-1" GridLines="Both">
       <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
              <MasterTableView DataSourceID="SqlDataSource1" AutoGenerateColumns="False" CommandItemDisplay="Top">
            <Columns>
                <telerik:GridBoundColumn DataField="SO_ID" HeaderText="SO_ID" SortExpression="SO_ID" UniqueName="SO_ID" DataType="System.Int64" FilterControlAltText="Filter SO_ID column"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SO_NO" HeaderText="SO_NO" SortExpression="SO_NO" UniqueName="SO_NO" FilterControlAltText="Filter SO_NO column"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PODate" ReadOnly="True" HeaderText="PODate" SortExpression="PODate" UniqueName="PODate" FilterControlAltText="Filter PODate column"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SI_NO" HeaderText="SI_NO" SortExpression="SI_NO" UniqueName="SI_NO" FilterControlAltText="Filter SI_NO column"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="InvoiceDt" ReadOnly="True" HeaderText="InvoiceDt" SortExpression="InvoiceDt" UniqueName="InvoiceDt" FilterControlAltText="Filter InvoiceDt column"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="custname" ReadOnly="True" HeaderText="custname" SortExpression="custname" UniqueName="custname" FilterControlAltText="Filter custname column"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="EstimatedValue" HeaderText="EstimatedValue" SortExpression="EstimatedValue" UniqueName="EstimatedValue" FilterControlAltText="Filter EstimatedValue column"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="POValueWithGST" ReadOnly="True" HeaderText="POValueWithGST" SortExpression="POValueWithGST" UniqueName="POValueWithGST" DataType="System.Decimal" FilterControlAltText="Filter POValueWithGST column"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="POValueWOGST" HeaderText="POValueWOGST" SortExpression="POValueWOGST" UniqueName="POValueWOGST" DataType="System.Decimal" FilterControlAltText="Filter POValueWOGST column"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="POAdvanceValue" HeaderText="POAdvanceValue" SortExpression="POAdvanceValue" UniqueName="POAdvanceValue" DataType="System.Decimal" FilterControlAltText="Filter POAdvanceValue column"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="InvoiceValueWithGST" HeaderText="InvoiceValueWithGST" SortExpression="InvoiceValueWithGST" UniqueName="InvoiceValueWithGST" DataType="System.Decimal" FilterControlAltText="Filter InvoiceValueWithGST column"></telerik:GridBoundColumn>
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
   
</asp:Content>

