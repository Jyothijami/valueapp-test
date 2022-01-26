<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/AdminMP1.master" AutoEventWireup="true" CodeFile="Sales_Dashboard.aspx.cs" Inherits="dboards_Sales_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table style="width:100%;" cellpadding="5" cellspacing="5">
        <tr>
            <td style="vertical-align: top; width: 33%;">
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="CUST_ID" DataSourceID="customerinfosds" OnRowDataBound="GridView1_RowDataBound" PageSize="5" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="CUST_ID" HeaderText="ID" ReadOnly="True" SortExpression="CUST_ID" />
                        <asp:TemplateField HeaderText="Customer Name" SortExpression="CUST_NAME">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("CUST_NAME") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# getUrl(Eval("CUST_ID").ToString()) %>' Target="_blank" Text='<%# Eval("CUST_NAME") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" SortExpression="CUST_STATUS">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CUST_STATUS") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCustStatus1" runat="server" Text='<%# Bind("CUST_STATUS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="customerinfosds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT CUST_ID, CUST_NAME, CUST_STATUS, CUST_CODE FROM YANTRA_CUSTOMER_MAST WHERE (CUST_ID &lt;&gt; - 1) AND (CUST_ID &lt;&gt; 0) ORDER BY CUST_ID DESC"></asp:SqlDataSource>
            </td>
            <td style="vertical-align: top; width: 33%;">
                <asp:GridView ID="GridView2" runat="server" AllowSorting="true" AutoGenerateColumns="False" DataKeyNames="ENQ_ID" DataSourceID="salesleadsds1" OnRowDataBound="GridView2_RowDataBound" Width="100%" AllowPaging="True" PageSize="5">
                    <Columns>
                        <asp:TemplateField HeaderText="Enquiry NO" SortExpression="ENQ_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("ENQ_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" NavigateUrl='<%# Eval("ENQ_ID", "~/Modules/SM/SalesEnquiryDetails.aspx?Eid={0}&Edit=edit") %>' Text='<%# Eval("ENQ_NO") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CUST_NAME" HeaderText="CUSTOMER NAME" SortExpression="CUST_NAME" />
                        <asp:TemplateField HeaderText=" STATUS" SortExpression="ENQ_STATUS">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ENQ_STATUS") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblEnqStatus1" runat="server" Text='<%# Bind("ENQ_STATUS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="salesleadsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT YANTRA_ENQ_MAST.ENQ_ID, YANTRA_ENQ_MAST.ENQ_NO, YANTRA_CUSTOMER_MAST.CUST_NAME, YANTRA_ENQ_MAST.ENQ_DATE, YANTRA_ENQ_MAST.ENQ_STATUS FROM YANTRA_ENQ_MAST INNER JOIN YANTRA_CUSTOMER_MAST ON YANTRA_ENQ_MAST.CUST_ID = YANTRA_CUSTOMER_MAST.CUST_ID ORDER BY YANTRA_ENQ_MAST.ENQ_DATE DESC"></asp:SqlDataSource>
            </td>
            <td style="vertical-align: top; width: 33%;">
                <asp:GridView ID="GridView3" runat="server" AllowPaging="True" AutoGenerateColumns="False" AllowSorting="true" DataKeyNames="ASSIGN_TASK_ID" DataSourceID="assignsds1" OnRowDataBound="GridView3_RowDataBound" PageSize="5" Width="100%">
                    <Columns>
                        
                        <asp:TemplateField HeaderText="Enquiry NO" SortExpression="ASSIGN_TASK_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("ASSIGN_TASK_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%# Eval("ASSIGN_TASK_ID", "~/Modules/SM/SalesAssignments.aspx") %>' Target="_blank" Text='<%# Eval("ASSIGN_TASK_NO") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>                        
                        <asp:BoundField DataField="ASSIGN_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="ASSIGNMENT DATE" SortExpression="ASSIGN_DATE" />
                        <asp:TemplateField HeaderText=" STATUS" SortExpression="ASSIGN_STATUS">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ASSIGN_STATUS") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAssignStatus1" runat="server" Text='<%# Bind("ASSIGN_STATUS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="assignsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" 
                    SelectCommand="SELECT ASSIGN_TASK_ID, ASSIGN_DATE, ASSIGN_STATUS, ASSIGN_TASK_NO FROM YANTRA_ENQ_ASSIGN_TASKS ORDER BY ASSIGN_DATE DESC"></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView4" runat="server" AllowPaging="True" AllowSorting="true" AutoGenerateColumns="False" DataKeyNames="QUOT_ID" DataSourceID="Quotationsds" OnRowDataBound="GridView4_RowDataBound" PageSize="5" Width="100%">
                    <Columns>
                           <asp:TemplateField HeaderText="QUOTATION NO" SortExpression="QUOT_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("QUOT_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl='<%# getUrl1(Eval("QUOT_ID").ToString()) %>' Text='<%# Eval("QUOT_NO") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:BoundField DataField="QUOT_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="QUOTATION DATE" SortExpression="QUOT_DATE" />
                        <asp:TemplateField HeaderText=" STATUS" SortExpression="QUOT_PO_FLAG">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("QUOT_PO_FLAG") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblQuotStatus1" runat="server" Text='<%# Bind("QUOT_PO_FLAG") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="Quotationsds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" 
                    SelectCommand="SELECT QUOT_ID, QUOT_NO, QUOT_DATE, QUOT_APPROVED_BY,  QUOT_PO_FLAG FROM YANTRA_QUOT_MAST ORDER BY QUOT_NO desc"></asp:SqlDataSource>
            </td>
            <td>
                <asp:GridView ID="GridView5" runat="server" AllowSorting="true" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="SO_ID" DataSourceID="SalesOrdersds" OnRowDataBound="GridView5_RowDataBound" PageSize="5" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="PO NO" SortExpression="SO_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("SO_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" NavigateUrl='<%# Eval("SO_ID", "~/Modules/SM/SalesOrder.aspx?Eid={0}&Edit=edit") %>' Text='<%# Eval("SO_NO") %>'></asp:HyperLink>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SO_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="PO DATE" SortExpression="SO_DATE" />
                        <asp:TemplateField HeaderText=" STATUS" SortExpression="SO_ACCEPTANCE_FLAG">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("SO_ACCEPTANCE_FLAG") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPOStatus" runat="server" Text='<%# Bind("SO_ACCEPTANCE_FLAG") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SalesOrdersds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="
select SO_ID, SO_NO, SO_DATE, SO_ACCEPTANCE_FLAG  from YANTRA_SO_MAST 
order by SO_NO desc"></asp:SqlDataSource>
            </td>
            <td>
                <asp:GridView ID="GridView6" AllowSorting="true" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="WO_ID" DataSourceID="WorkOrdersds" OnRowDataBound="GridView6_RowDataBound" PageSize="5" Width="100%">
                    <Columns>
                         <asp:TemplateField HeaderText="IO NO" SortExpression="WO_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("WO_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl='<%# getUrl2(Eval("WO_ID").ToString()) %>' Text='<%# Eval("WO_NO") %>'></asp:HyperLink>
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
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>


 
