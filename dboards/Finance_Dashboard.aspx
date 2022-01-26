<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/FinanceMP1.master" AutoEventWireup="true" CodeFile="Finance_Dashboard.aspx.cs" Inherits="dboards_Finance_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">

     <table style="width:100%;" cellpadding="5" cellspacing="5">
        <tr>
            <td style="vertical-align: top; width: 33%;">
                 
                  <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SI_ID" DataSourceID="SalesInvoicesds" OnRowDataBound="GridView1_RowDataBound" PageSize="5" Width="100%">
                    <Columns>
                           <asp:TemplateField HeaderText="Sales INVOICE NO" SortExpression="SI_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("SI_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl='<%# getUrl(Eval("SI_ID").ToString()) %>' Text='<%# Eval("SI_NO") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:BoundField DataField="SI_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="SALES INVOICE DATE" SortExpression="SI_DATE" />
                        <asp:TemplateField HeaderText=" STATUS" SortExpression="SI_STATUS">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("SI_STATUS") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSIStatus" runat="server" Text='<%# Bind("SI_STATUS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SalesInvoicesds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" 
                    SelectCommand="select SI_ID, SI_NO, SI_DATE, SI_STATUS from YANTRA_SALES_INVOICE_MAST order by SI_ID desc"></asp:SqlDataSource>
</td>
            <td style="vertical-align: top; width: 33%;">
                 <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SR_ID" DataSourceID="SalesReturnsds"  PageSize="5" Width="100%">
                    <Columns>
                           <asp:TemplateField HeaderText="SALES RETURN NO" SortExpression="SR_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("SR_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" NavigateUrl='<%# Eval("SR_ID", "~/Modules/SCM/SalesReturnDetails.aspx?SrNo={0}") %>' Text='<%# Eval("SR_NO") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField HeaderText="DC NO" SortExpression="DC_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("SR_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" NavigateUrl='<%# Eval("SR_ID", "~/Modules/SCM/SalesReturnDetails.aspx?SrNo={0}") %>' Text='<%# Eval("DC_NO") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:BoundField DataField="SR_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="SALES RETURN DATE" SortExpression="SR_DATE" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SalesReturnsds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" 
                    SelectCommand="select a.SR_ID,  b.DC_NO, a.SR_NO, a.SR_DATE from YANTRA_SALES_RETURN_MAST a inner join YANTRA_DELIVERY_CHALLAN_MAST b on a.DC_ID=b.DC_ID order by a.SR_ID desc"></asp:SqlDataSource>
            </td>
                        <td style="vertical-align: top; width: 33%;">
                             <asp:GridView ID="GridView3" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="PR_ID" DataSourceID="SalesPaymentssds" OnRowDataBound="GridView3_RowDataBound" PageSize="5" Width="100%">
                    <Columns>
                           <asp:TemplateField HeaderText="PURCHASE INVOICE NO" SortExpression="PR_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("PR_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl='<%# getUrl1(Eval("PR_ID").ToString()) %>' Text='<%# Eval("PR_NO") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:BoundField DataField="SO_NO" HeaderText="PO NO" SortExpression="SO_NO" />
                        <asp:TemplateField HeaderText=" STATUS" SortExpression="PR_PAYMENT_STATUS">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("PR_PAYMENT_STATUS") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPRStatus" runat="server" Text='<%# Bind("PR_PAYMENT_STATUS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SalesPaymentssds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" 
                    SelectCommand="select a.PR_ID, a.PR_NO, b.SO_NO, a.PR_PAYMENT_STATUS from YANTRA_PAYMENTS_RECEIVED a inner join YANTRA_SO_MAST b on a.SO_ID=b.SO_ID order by a.PR_ID DESC"></asp:SqlDataSource>
            </td>
            </tr>
         <tr>
                         <td style="vertical-align: top; width: 33%;">
                             <asp:GridView ID="GridView4" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SI_ID" DataSourceID="cashinvoicesds"  PageSize="5" Width="100%" OnRowDataBound="GridView4_RowDataBound">
                    <Columns>
                           <asp:TemplateField HeaderText="Cash Invoice NO" SortExpression="SI_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("SI_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" NavigateUrl='<%# Eval("SI_ID", "~/Modules/Inventory/CashInvoice.aspx") %>' Text='<%# Eval("SI_NO") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>    
                        <asp:BoundField DataField="SI_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="CASH INVOICE DATE" SortExpression="SI_DATE" />
                        <asp:TemplateField HeaderText=" STATUS" SortExpression="SI_STATUS">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("SI_STATUS") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSIStatus" runat="server" Text='<%# Bind("SI_STATUS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="cashinvoicesds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" 
                    SelectCommand="select a.SI_ID, a.SI_NO, a.SI_DATE, a.SI_STATUS from YANTRA_SALES_INVOICE_MAST a inner join YANTRA_DELIVERY_CHALLAN_MAST b 
on a.DC_ID=b.DC_ID where b.DC_FOR='Sample' or b.DC_FOR='Cash' order by a.SI_ID desc"></asp:SqlDataSource>
            </td>
                         <td style="vertical-align: top; width: 33%;">
                              <asp:GridView ID="GridView5" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SR_ID" DataSourceID="CashReturnsds"  PageSize="5" Width="100%" >
                    <Columns>
                                <asp:TemplateField HeaderText="DC NO" SortExpression="DC_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("SR_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" NavigateUrl='<%# Eval("SR_ID", "~/Modules/SCM/SampleReturnNew.aspx?dcNo={0}") %>' Text='<%# Eval("DC_NO") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                           <asp:TemplateField HeaderText="Cash RETURN NO" SortExpression="SI_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("SR_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" NavigateUrl='<%# Eval("SR_ID", "~/Modules/SCM/SampleReturnNew.aspx?dcNo={0}") %>' Text='<%# Eval("SR_NO") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>    
                        <asp:BoundField DataField="SR_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="CASH RETURN DATE" SortExpression="SR_DATE" />
                        
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="CashReturnsds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" 
                    SelectCommand="select a.SR_ID, b.DC_NO, a.SR_NO, a.SR_DATE, a.DC_ID from YANTRA_SALES_RETURN_MAST a inner join YANTRA_DELIVERY_CHALLAN_MAST b 
on a.DC_ID=b.DC_ID where b.DC_FOR='Sample' or b.DC_FOR='Cash' order by a.SR_ID DESC"></asp:SqlDataSource>
            </td>
                         <td style="vertical-align: top; width: 33%;">
            </td>
         </tr>
         </table>
</asp:Content>


 

