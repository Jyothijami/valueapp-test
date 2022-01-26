<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/InventoryMP1.master" AutoEventWireup="true" CodeFile="Inventory_Dashboard.aspx.cs" Inherits="dboards_Inventory_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
     <table style="width:100%;" cellpadding="5" cellspacing="5">
        <tr>
            <td style="vertical-align: top; width: 33%;">
                 <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="IND_ID" DataSourceID="Indentsds" OnRowDataBound="GridView1_RowDataBound" PageSize="5" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="INDENT NO" SortExpression="IND_NO">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("IND_ID") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# getUrl1(Eval("IND_ID").ToString()) %>' Target="_blank" Text='<%# Eval("IND_NO") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="IND_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="INDENT DATE" SortExpression="IND_DATE" />
                        <asp:TemplateField HeaderText="Status" SortExpression="STATUS">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("STATUS") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIndentStatus" runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="Indentsds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="select IND_ID, IND_NO, IND_DATE, STATUS from YANTRA_INDENT_MAST order by IND_NO desc"></asp:SqlDataSource>
            </td>
            <td style="vertical-align: top; width: 33%;">
                 <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Dispatch_id" DataSourceID="Dispatchsds" OnRowDataBound="GridView2_RowDataBound" PageSize="5" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="CUSTOMER NAME" SortExpression="CUST_NAME">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Dispatch_id") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# getUrl2(Eval("Dispatch_id").ToString()) %>' Target="_blank" Text='<%# Eval("CUST_NAME") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DeliveryDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="DELIVERY DATE" SortExpression="DeliveryDate" />
                        <asp:TemplateField HeaderText="STATUS" SortExpression="Status">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Status") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDispatchStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="Dispatchsds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="select a.Dispatch_id, b.CUST_NAME, a.DeliveryDate, a.Status   from Dispatch a inner join YANTRA_CUSTOMER_MAST b on a.Cust_id=b.CUST_ID order by a.Dispatch_id desc"></asp:SqlDataSource>
            </td>
                <td style="vertical-align: top; width: 33%;">

                  <asp:GridView ID="GridView3" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="CHK_ID" DataSourceID="MRNsds" PageSize="5" Width="100%">
                    <Columns>
                           <asp:TemplateField HeaderText="MRN NO" SortExpression="CHK_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("CHK_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink7" runat="server" Target="_blank" NavigateUrl='<%# Eval("CHK_ID", "~/Modules/SCM/CheckingFormatDetails.aspx?ChkId={0}") %>' Text='<%# Eval("CHK_NO") %>'></asp:HyperLink>

                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:BoundField DataField="CHK_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="MRN DATE" SortExpression="CHK_DATE" />
                        <asp:BoundField DataField="CHK_PO_NO"  HeaderText="MRN PO NO" SortExpression="CHK_PO_NO" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="MRNsds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" 
                    SelectCommand="select CHK_ID, CHK_NO, CHK_DATE, CHK_PO_NO from YANTRA_CHECKING_FORMAT order by CHK_ID desc"></asp:SqlDataSource>

            </td>
            </tr>
         <tr>
             <td style="vertical-align: top; width: 33%;">
                                  <asp:GridView ID="GridView4" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="DC_ID" DataSourceID="DCsds" OnRowDataBound="GridView4_RowDataBound" PageSize="5" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="DC NO" SortExpression="DC_NO">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("DC_ID") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# getUrl3(Eval("DC_ID").ToString()) %>' Target="_blank" Text='<%# Eval("DC_NO") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DC_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="DC DATE" SortExpression="DC_DATE" />
                        <asp:TemplateField HeaderText="STATUS" SortExpression="STATUS">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("STATUS") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDCStatus" runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DC_FOR"  HeaderText="DC FOR" SortExpression="DC_FOR" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="DCsds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="select DC_ID, DC_NO, DC_DATE, STATUS, DC_FOR from YANTRA_DELIVERY_CHALLAN_MAST where DC_FOR='Sales'  order by DC_ID desc"></asp:SqlDataSource>
            </td>
             <td style="vertical-align: top; width: 33%;">
                <asp:GridView ID="GridView5" AllowSorting="true" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="WO_ID" DataSourceID="WorkOrdersds" OnRowDataBound="GridView5_RowDataBound" PageSize="5" Width="100%">
                    <Columns>
                         <asp:TemplateField HeaderText="IO NO" SortExpression="WO_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("WO_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink7" runat="server" Target="_blank" NavigateUrl='<%# Eval("WO_ID", "~/Modules/Inventory/InternalOrderApprovalDetails.aspx?WoNo={0}") %>' Text='<%# Eval("WO_NO") %>'></asp:HyperLink>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="WO_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="IO DATE" SortExpression="WO_DATE" />
                        <asp:TemplateField HeaderText=" STATUS" SortExpression="WO_FLAG">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("WO_FLAG") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIOStatus" runat="server" Text='<%# Bind("WO_FLAG") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="WorkOrdersds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="
select WO_ID, WO_NO, WO_DATE, WO_APPROVED_BY, WO_FLAG  from YANTRA_WO_MAST
order by WO_NO desc"></asp:SqlDataSource>
            </td>
             <td style="vertical-align: top; width: 33%;">
                           <asp:GridView ID="GridView6" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="DC_ID" DataSourceID="DCCashsds" PageSize="5" Width="100%" OnRowDataBound="GridView6_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="DC NO" SortExpression="DC_NO">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("DC_ID") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink7" runat="server" Target="_blank" NavigateUrl='<%# Eval("DC_ID", "~/Modules/Inventory/SampleDc.aspx") %>' Text='<%# Eval("DC_NO") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DC_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="DC DATE" SortExpression="DC_DATE" />
                        <asp:TemplateField HeaderText="STATUS" SortExpression="STATUS">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("STATUS") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDCCashStatus" runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DC_FOR"  HeaderText="DC FOR" SortExpression="DC_FOR" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="DCCashsds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="select DC_ID, DC_NO, DC_DATE, STATUS, DC_FOR from YANTRA_DELIVERY_CHALLAN_MAST where DC_FOR='Cash'  order by DC_ID desc"></asp:SqlDataSource>
            </td>
         </tr>
         </table>
</asp:Content>


 
