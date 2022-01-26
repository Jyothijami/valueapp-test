<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CashInvoice.ascx.cs" Inherits="Modules_widgets_CashInvoice" %>
<table>
        <tr>
            <td>
                <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1" Visible="False"></asp:Label>
                <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1" Visible="False"></asp:Label><asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
<strong>Sample & Cash Invoice :</strong>
<asp:GridView ID="GridView4" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SI_ID" DataSourceID="cashinvoicesds" PageSize="5" Width="100%" OnRowDataBound="GridView4_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Cash Invoice NO" SortExpression="SI_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("SI_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink3" runat="server" Text='<%# Eval("SI_NO") %>'></asp:HyperLink>
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
                    SelectCommand="SELECT YANTRA_SALES_INVOICE_MAST.SI_ID, YANTRA_SALES_INVOICE_MAST.SI_NO, YANTRA_SALES_INVOICE_MAST.SI_DATE, YANTRA_SALES_INVOICE_MAST.SI_TYPE, YANTRA_SALES_INVOICE_MAST.DC_ID, YANTRA_SALES_INVOICE_MAST.DESPM_ID, YANTRA_SALES_INVOICE_MAST.SO_ID, YANTRA_SALES_INVOICE_MAST.SI_MISS_CHRGS, YANTRA_SALES_INVOICE_MAST.SI_DISCOUNT, YANTRA_SALES_INVOICE_MAST.SI_GROSS_AMT, YANTRA_SALES_INVOICE_MAST.SI_REMARKS, YANTRA_SALES_INVOICE_MAST.SI_PREPARED_BY, YANTRA_SALES_INVOICE_MAST.SI_APPROVED_BY, YANTRA_SALES_INVOICE_MAST.SPO_ID, YANTRA_SALES_INVOICE_MAST.SI_VAT, YANTRA_SALES_INVOICE_MAST.SI_CSTAX, YANTRA_SALES_INVOICE_MAST.CP_ID, YANTRA_SALES_INVOICE_MAST.SI_STATUS, YANTRA_DELIVERY_CHALLAN_MAST.DC_ID AS Expr1, YANTRA_DELIVERY_CHALLAN_MAST.DC_NO, YANTRA_DELIVERY_CHALLAN_MAST.DC_DATE, YANTRA_DELIVERY_CHALLAN_MAST.TRANS_ID, YANTRA_DELIVERY_CHALLAN_MAST.DC_LR_NO, YANTRA_DELIVERY_CHALLAN_MAST.DC_LR_DATE, YANTRA_DELIVERY_CHALLAN_MAST.SO_ID AS Expr2, YANTRA_DELIVERY_CHALLAN_MAST.DC_PREPARED_BY, YANTRA_DELIVERY_CHALLAN_MAST.DC_APPROVED_BY, YANTRA_DELIVERY_CHALLAN_MAST.DC_TYPE, YANTRA_DELIVERY_CHALLAN_MAST.DC_CST_NO, YANTRA_DELIVERY_CHALLAN_MAST.DC_TIN_NO, YANTRA_DELIVERY_CHALLAN_MAST.DC_REVISED_KEY, YANTRA_DELIVERY_CHALLAN_MAST.DC_INWARD_DATE, YANTRA_DELIVERY_CHALLAN_MAST.DC_FOR, YANTRA_DELIVERY_CHALLAN_MAST.SPO_ID AS Expr3, YANTRA_DELIVERY_CHALLAN_MAST.DESPM_ID AS Expr4, YANTRA_DELIVERY_CHALLAN_MAST.CP_ID AS Expr5, YANTRA_DELIVERY_CHALLAN_MAST.STATUS, YANTRA_DELIVERY_CHALLAN_MAST.DC_COMPANY, YANTRA_DELIVERY_CHALLAN_MAST.DC_CUSTOMER_ID, YANTRA_CUSTOMER_MAST.CUST_ID, YANTRA_CUSTOMER_MAST.CUST_CODE, YANTRA_CUSTOMER_MAST.CUST_NAME, YANTRA_CUSTOMER_MAST.CUST_COMPANY_NAME, YANTRA_CUSTOMER_MAST.CUST_CONTACT_PERSON, YANTRA_CUSTOMER_MAST.CUST_PHONE, YANTRA_CUSTOMER_MAST.CUST_MOBILE, YANTRA_CUSTOMER_MAST.CUST_FAX, YANTRA_CUSTOMER_MAST.CUST_EMAIL, YANTRA_CUSTOMER_MAST.CUST_WEBSITE, YANTRA_CUSTOMER_MAST.CUST_PAN, YANTRA_CUSTOMER_MAST.CUST_ECC, YANTRA_CUSTOMER_MAST.CUST_CST, YANTRA_CUSTOMER_MAST.CUST_LOCAL_ST_NO, YANTRA_CUSTOMER_MAST.REG_ID, YANTRA_CUSTOMER_MAST.IND_TYPE_ID, YANTRA_CUSTOMER_MAST.CUST_ADDRESS, YANTRA_CUSTOMER_MAST.CUST_SPL_INSTRS, YANTRA_CUSTOMER_MAST.CUST_CORP_CONTACT_PERSON, YANTRA_CUSTOMER_MAST.CUST_CORP_PHONE, YANTRA_CUSTOMER_MAST.CUST_CORP_MOBILE, YANTRA_CUSTOMER_MAST.CUST_CORP_EMAIL, YANTRA_CUSTOMER_MAST.CUST_CORP_ADDRESS, YANTRA_CUSTOMER_MAST.CUST_DESG_ID, YANTRA_CUSTOMER_MAST.CUST_CORP_DESG_ID, YANTRA_CUSTOMER_MAST.CUST_CORP_FAX, YANTRA_CUSTOMER_MAST.ISNEWOREXISTING, YANTRA_CUSTOMER_MAST.CUST_STATUS, YANTRA_CUSTOMER_MAST.CP_ID AS Expr6, YANTRA_EMPLOYEE_MAST.EMP_FIRST_NAME FROM YANTRA_SALES_INVOICE_MAST INNER JOIN YANTRA_DELIVERY_CHALLAN_MAST ON YANTRA_SALES_INVOICE_MAST.DC_ID = YANTRA_DELIVERY_CHALLAN_MAST.DC_ID INNER JOIN YANTRA_CUSTOMER_MAST ON YANTRA_DELIVERY_CHALLAN_MAST.DC_CUSTOMER_ID = YANTRA_CUSTOMER_MAST.CUST_ID INNER JOIN YANTRA_EMPLOYEE_MAST ON YANTRA_SALES_INVOICE_MAST.SI_APPROVED_BY = YANTRA_EMPLOYEE_MAST.EMP_ID WHERE (YANTRA_DELIVERY_CHALLAN_MAST.DC_FOR = 'Sample') OR (YANTRA_DELIVERY_CHALLAN_MAST.DC_FOR = 'Cash') ORDER BY YANTRA_SALES_INVOICE_MAST.SI_ID DESC"></asp:SqlDataSource>