<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/AdminMP1.master" AutoEventWireup="true" CodeFile="Purchase_Dashboard.aspx.cs" Inherits="dboards_Purchase_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
     <table style="width:100%;" cellpadding="5" cellspacing="5">
        <tr>
            <td style="vertical-align: top; width: 33%;">
                 <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SUP_ID" DataSourceID="Supplierinfosds" PageSize="5" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="SUP_ID" HeaderText="ID" ReadOnly="True" SortExpression="SUP_ID" />
                        <asp:TemplateField HeaderText="Supplier Name" SortExpression="SUP_NAME">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("SUP_NAME") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" NavigateUrl='<%# Eval("SUP_ID", "~/Modules/SCM/SupplierMasterNew.aspx?supplierId={0}") %>' Text='<%# Eval("SUP_NAME") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PRODUCT_COMPANY_NAME" HeaderText="BRAND" ReadOnly="True" SortExpression="SUPPRODUCT_COMPANY_NAME_ID" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="Supplierinfosds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" 
                    SelectCommand="SELECT a.SUP_ID, a.SUP_NAME, b.PRODUCT_COMPANY_NAME FROM YANTRA_SUPPLIER_MAST AS a INNER JOIN YANTRA_LKUP_PRODUCT_COMPANY AS b ON b.PRODUCT_COMPANY_ID = a.SUP_BRAND ORDER BY a.SUP_ID desc"></asp:SqlDataSource>
            </td>
            <td style="vertical-align: top; width: 33%;">
                  <asp:GridView ID="GridView4" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SUP_ENQ_ID" DataSourceID="SupQuotationsds" OnRowDataBound="GridView4_RowDataBound" PageSize="5" Width="100%">
                    <Columns>
                           <asp:TemplateField HeaderText="SUPPLIER ENQUIRY NO" SortExpression="SUP_ENQ_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("SUP_ENQ_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" NavigateUrl='<%# Eval("SUP_ENQ_ID", "~/Modules/SCM/SupplierQuation.aspx?enqNo={0}") %>' Text='<%# Eval("SUP_ENQ_NO") %>'></asp:HyperLink>

                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:BoundField DataField="SUP_ENQ_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="SUPPLIER ENQUIRY DATE" SortExpression="SUP_ENQ_DATE" />
                        <asp:TemplateField HeaderText=" STATUS" SortExpression="SUP_ENQ_STATUS">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("SUP_ENQ_STATUS") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblQuotStatus1" runat="server" Text='<%# Bind("SUP_ENQ_STATUS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SupQuotationsds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" 
                    SelectCommand="select SUP_ENQ_ID, SUP_ENQ_NO, SUP_ENQ_DATE, SUP_ENQ_STATUS from YANTRA_SUP_ENQ_MAST order by SUP_ENQ_NO desc"></asp:SqlDataSource>
            </td>
            <td style="vertical-align: top; width: 33%;">
                  <asp:GridView ID="GridView5" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SUP_QUOT_ID" DataSourceID="ProformaInvoicesds" PageSize="5" Width="100%">
                    <Columns>
                           <asp:TemplateField HeaderText="PROFORMA NO" SortExpression="SUP_QUOT_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("SUP_QUOT_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink4" runat="server" Target="_blank" NavigateUrl='<%# Eval("SUP_QUOT_ID", "~/Modules/SCM/Quotation.aspx") %>' Text='<%# Eval("SUP_QUOT_NO") %>'></asp:HyperLink>

                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:BoundField DataField="SUP_QUOT_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="PROFORMA DATE" SortExpression="SUP_QUOT_DATE" />
                        <asp:BoundField DataField="SUP_NAME"  HeaderText="SUPPLIER NAME" SortExpression="SUP_NAME" />
                       <%-- <asp:TemplateField HeaderText=" STATUS" SortExpression="SUP_ENQ_STATUS">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("SUP_ENQ_STATUS") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblQuotStatus2" runat="server" Text='<%# Bind("SUP_ENQ_STATUS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="ProformaInvoicesds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" 
                    SelectCommand="select a.SUP_QUOT_ID, a.SUP_QUOT_NO, a.SUP_QUOT_DATE, b.SUP_NAME  from YANTRA_SUP_QUOT_MAST a 
inner join YANTRA_SUPPLIER_MAST b 
on a.SUP_ID=b.SUP_ID order by SUP_QUOT_NO desc"></asp:SqlDataSource>

            </td>
            </tr>
         <tr>
            <td style="vertical-align: top; width: 33%;">

                 <asp:GridView ID="GridView6" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="FPO_ID" DataSourceID="Purchaseordersds"  PageSize="5" Width="100%" OnRowDataBound="GridView6_RowDataBound1">
                    <Columns>
                        <asp:BoundField DataField="FPO_ID" HeaderText="ID" ReadOnly="True" SortExpression="FPO_ID" />
                        <asp:TemplateField HeaderText="PURCHSE ORDER NO." SortExpression="FPO_NO">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("FPO_ID") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink5" runat="server" Target="_blank" NavigateUrl='<%# Eval("FPO_ID", "~/Modules/SCM/PurchaseOrder.aspx?poNo={0}") %>' Text='<%# Eval("FPO_NO") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="FPO_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="PURCHSE ORDER  DATE" SortExpression="FPO_DATE" />
                        <asp:TemplateField HeaderText="STATUS" SortExpression="FPO_PO_STATUS">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("FPO_PO_STATUS") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPurchaseStatus" runat="server" Text='<%# Bind("FPO_PO_STATUS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="Purchaseordersds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" 
                    SelectCommand="SELECT FPO_ID, FPO_NO, FPO_DATE, FPO_PO_STATUS FROM YANTRA_FIXED_PO_MAST ORDER BY FPO_NO desc"></asp:SqlDataSource>

            </td>
            <td style="vertical-align: top; width: 33%;">

                  <asp:GridView ID="GridView7" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="PI_ID" DataSourceID="PurchaseInvoicesds" OnRowDataBound="GridView7_RowDataBound" PageSize="5" Width="100%">
                    <Columns>
                           <asp:TemplateField HeaderText="PURCHASE INVOICE NO" SortExpression="PI_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("PI_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl='<%# getUrl(Eval("PI_ID").ToString()) %>' Text='<%# Eval("PI_NO") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:BoundField DataField="PI_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="PURCHASE INVOICE DATE" SortExpression="PI_DATE" />
                        <asp:TemplateField HeaderText=" STATUS" SortExpression="PI_STATUS">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("PI_STATUS") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPIStatus" runat="server" Text='<%# Bind("PI_STATUS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="PurchaseInvoicesds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" 
                    SelectCommand="select PI_ID, PI_NO, PI_DATE, PI_STATUS  from YANTRA_PURCHASE_INVOICE_MAST order by PI_NO desc"></asp:SqlDataSource>
             </td>

            <td style="vertical-align: top; width: 33%;">

                  <asp:GridView ID="GridView8" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SD_ID" DataSourceID="Shipmentsds" PageSize="5" Width="100%">
                    <Columns>
                           <asp:TemplateField HeaderText="SHIPMENT NO" SortExpression="SD_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("SD_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink7" runat="server" Target="_blank" NavigateUrl='<%# Eval("SD_ID", "~/Modules/SCM/ShipmentDetailsNew.aspx?shipmentNo={0}") %>' Text='<%# Eval("SD_NO") %>'></asp:HyperLink>

                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:BoundField DataField="SD_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="SHIPMENT DATE" SortExpression="SD_DATE" />
                        <asp:BoundField DataField="SUP_NAME"  HeaderText="SUPPLIER NAME" SortExpression="SUP_NAME" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="Shipmentsds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" 
                    SelectCommand="select a.SD_ID, a.SD_NO, a.SD_DATE,
 b.SUP_NAME from YANTRA_SHIPPING_DETAILS_MASTER A
inner join YANTRA_SUPPLIER_MAST b 
on a.SUP_ID=b.SUP_ID order by SD_NO desc"></asp:SqlDataSource>

             </td>
            </tr>
         <tr>
            <td style="vertical-align: top; width: 33%;">

            </td>
            </tr>
         <tr>
            <td style="vertical-align: top; width: 33%;">

            </td>
            </tr>
         </table>
</asp:Content>


 
