<%@ Page Title="" Language="C#" MasterPageFile="~/NLMain.master" AutoEventWireup="true" CodeFile="SoAmountUpdate.aspx.cs" Inherits="TestingCodes_SoAmountUpdate" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


     <telerik:RadAjaxManager runat="server" UpdateInitiatorPanelsOnly="True">
     </telerik:RadAjaxManager>


     <div class="page-header">
        <div class="page-title">
            <h3>Sales Order Update</h3>
        </div>
    </div>

     <div class="form-horizontal">

         <div class="panel panel-default">
            <div class="panel-heading">
            <h6 class="panel-title"><i class="icon-file"></i>Update Sales Order</h6>
           
        </div>
            <div class="panel-body">


                <telerik:RadGrid ID="RadGrid1" runat="server" CellSpacing="-1" DataSourceID="SqlDataSource1" GridLines="Both" AllowAutomaticUpdates="True" AutoGenerateColumns="False" ShowGroupPanel="True" ClientSettings-EnableClientPrint="true" AllowSorting="True" >
                    <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                    <ExportSettings Pdf-AllowPrinting="true" OpenInNewWindow="true" IgnorePaging="True">
                        <Pdf PaperSize="A4">
                        </Pdf>
                    </ExportSettings>
                    <ClientSettings AllowDragToGroup="True">
                    </ClientSettings>
                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="SalesOrder_Id" DataSourceID="SqlDataSource1" EditMode="InPlace">
                        <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="false" ShowPrintButton="true" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="SalesOrder_Id" DataType="System.Int64" FilterControlAltText="Filter SalesOrder_Id column" HeaderText="SalesOrder_Id" ReadOnly="True" SortExpression="SalesOrder_Id" UniqueName="SalesOrder_Id">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SalesOrder_No" FilterControlAltText="Filter SalesOrder_No column" HeaderText="SalesOrder_No" SortExpression="SalesOrder_No" UniqueName="SalesOrder_No">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SalesOrder_Date" DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}" FilterControlAltText="Filter SalesOrder_Date column" HeaderText="SalesOrder_Date" SortExpression="SalesOrder_Date" UniqueName="SalesOrder_Date">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ProjectCode" FilterControlAltText="Filter ProjectCode column" HeaderText="ProjectCode" SortExpression="ProjectCode" UniqueName="ProjectCode">
                            </telerik:GridBoundColumn>
                              <telerik:GridBoundColumn DataField="CUST_UNIT_NAME" FilterControlAltText="Filter CUST_UNIT_NAME column" HeaderText="Customer" SortExpression="CUST_UNIT_NAME" UniqueName="CUST_UNIT_NAME">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="SalesPerson" FilterControlAltText="Filter SalesPerson column" HeaderText="Sales Incharge" SortExpression="SalesPerson" UniqueName="SalesPerson">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DesignPerson"  FilterControlAltText="Filter DesignPerson column" HeaderText="Design InCharge" SortExpression="DesignPerson" UniqueName="DesignPerson">
                            </telerik:GridBoundColumn>

                         <%--  <telerik:GridBoundColumn DataField="PurchaseCondtions_Id"  FilterControlAltText="Filter PurchaseCondtions_Id column" HeaderText="Payment Terms" SortExpression="PurchaseCondtions_Id" UniqueName="PurchaseCondtions_Id">
                            </telerik:GridBoundColumn>--%>

                              


                            <telerik:GridBoundColumn DataField="Total_Sales_Value" DataType="System.Decimal" FilterControlAltText="Filter Total_Sales_Value column" HeaderText="Total_Sales_Value" SortExpression="Total_Sales_Value" UniqueName="Total_Sales_Value">
                            </telerik:GridBoundColumn>


                            <telerik:GridEditCommandColumn FooterText="EditCommand footer" UniqueName="EditCommandColumn"
                                HeaderText="Edit" HeaderStyle-Width="100px" UpdateText="Update">
                                <HeaderStyle Width="100px" />
                            </telerik:GridEditCommandColumn>


                        </Columns>

                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn1 column" UniqueName="EditCommandColumn1">
                            </EditColumn>
                        </EditFormSettings>



                    </MasterTableView>
                </telerik:RadGrid>


                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SELECT   Sales_Order.SalesOrder_Id, Sales_Order.SalesOrder_No, Sales_Order.SalesOrder_Date, Sales_Order.ProjectCode, Sales_Order.SalesIncharge, Sales_Order.DesigninCharge, Sales_Order.PurchaseCondtions_Id,
                          Sales_Order.Total_Sales_Value, Employee_Master.EMP_FIRST_NAME+'  '+ Employee_Master.EMP_LAST_NAME as SalesPerson, Employee_Master_1.EMP_FIRST_NAME +'  '+Employee_Master_1.EMP_LAST_NAME AS DesignPerson,
                          Customer_Units.CUST_UNIT_NAME
FROM            Sales_Order INNER JOIN
                         Employee_Master ON Sales_Order.SalesIncharge = Employee_Master.EMP_ID INNER JOIN
                         Employee_Master AS Employee_Master_1 ON Sales_Order.DesigninCharge = Employee_Master_1.EMP_ID INNER JOIN
                         Customer_Units ON Sales_Order.CustSiteId = Customer_Units.CUST_UNIT_ID"
                    UpdateCommand="UPDATE [Sales_Order] SET  [Total_Sales_Value] = @Total_Sales_Value WHERE [SalesOrder_Id] = @SalesOrder_Id">

                    <UpdateParameters>

                        <asp:Parameter Name="Total_Sales_Value" Type="Decimal" />
                        <asp:Parameter Name="SalesOrder_Id" Type="Int64" />
                    </UpdateParameters>
                </asp:SqlDataSource>


            </div>
         </div>
</asp:Content>

