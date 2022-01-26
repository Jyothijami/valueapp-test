<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalesLeadItemImage.aspx.cs" Inherits="Modules_SM_SalesLeadItemImage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 725px">
            <tr>
                <td class="searchhead" colspan="4" style="height: 38px; text-align: left">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 272px; height: 19px; text-align: left">
                                Sales Lead</td>
                            <td style="height: 19px; text-align: right">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: left">
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 21px; text-align: center">
                    <asp:GridView ID="gvSalesEnquiry" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        DataKeyNames="ENQ_ID" DataSourceID="sdsSalesEnquiry" OnRowDataBound="gvSalesEnquiry_RowDataBound"
                        Width="100%">
                        <Columns>
                            <asp:BoundField DataField="ENQ_ID" HeaderText="EnqIdHidden" SortExpression="ENQ_ID" />
                            <asp:TemplateField HeaderText="Enq No" SortExpression="ENQ_NO">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ENQ_NO") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnEnqNo" runat="server" CausesValidation="False" Text='<%# Bind("ENQ_NO") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ENQ_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Enq Date"
                                HtmlEncode="False" SortExpression="ENQ_DATE">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CUST_NAME" HeaderText="Customer Name" SortExpression="CUST_NAME">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ENQ_DUE_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Due Date"
                                HtmlEncode="False" SortExpression="ENQ_DUE_DATE">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ENQ_STATUS" HeaderText="Status" SortExpression="ENQ_STATUS">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Image">
                                <ItemTemplate>
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/noimage400x300.gif" Width="70px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            No Data Exist!
                        </EmptyDataTemplate>
                    </asp:GridView>
                    &nbsp;
                    <asp:SqlDataSource ID="sdsSalesEnquiry" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                        SelectCommand="SP_SM_SALESENQUIRY_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName"
                                PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="lblSearchTypeHidden" DefaultValue="0" Name="SearchType"
                                PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue"
                                PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="lblSearchValueFromHidden" DefaultValue="0" Name="SearchValueFrom"
                                PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <table id="tblPMDetails" runat="server" border="0" cellpadding="0" cellspacing="0"
                        visible="true" width="100%">
                        <tr>
                            <td style="text-align: right">
                            </td>
                            <td colspan="3" style="text-align: left">
                                &nbsp;<asp:FileUpload ID="FileUpload1" runat="server" Width="527px" /></td>
                        </tr>
                        <tr>
                            <td style="height: 24px; text-align: right">
                            </td>
                            <td colspan="3" style="height: 24px; text-align: left">
                                &nbsp;<asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload" />&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

 
