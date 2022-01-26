<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImageUpload.aspx.cs" Inherits="Modules_Masters_ImageUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script language ="javascript"  type="text/javascript" >
    function f1()
    {
    window.close ()
    }
    
    </script> 
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%" cellpadding="5" cellspacing="5">
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 148px; width: 100%;">
                    <asp:GridView ID="gvCompanyDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        DataKeyNames="CP_ID" DataSourceID="sdsCompanyProfile" OnRowDataBound="gvCompanyDetails_RowDataBound" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="CP_FULL_NAME" HeaderText="CompanyNameHidden" SortExpression="CP_FULL_NAME" />
                            <asp:BoundField DataField="CP_ID" HeaderText="Sl.No" ReadOnly="True" SortExpression="CP_ID" />
                            <asp:TemplateField HeaderText="Company Name" SortExpression="CP_FULL_NAME">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CP_FULL_NAME") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnCompanyName" runat="server" OnClick="lbtnCompanyName_Click"
                                        Text='<%# Bind("CP_FULL_NAME") %>'></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="300px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="CP_SHORT_NAME" HeaderText="Company Short Name" SortExpression="CP_SHORT_NAME" >
                                <ItemStyle Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CP_ADDRESS" HeaderText="Address" SortExpression="CP_ADDRESS" >
                                <ItemStyle Width="300px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CP_CONTACT_NO1" HeaderText="Contact No" SortExpression="CP_CONTACT_NO1" />
                            <asp:BoundField DataField="CP_FAXNO" HeaderText="Fax No" ReadOnly="True" SortExpression="CP_FAXNO" />
                            <asp:BoundField DataField="CP_CONTACT_NO2" HeaderText="CON_NO2" SortExpression="CP_CONTACT_NO2" />
                            <asp:BoundField DataField="CP_EMAIL" HeaderText="EMAIL" SortExpression="CP_EMAIL" />
                            <asp:BoundField DataField="CP_TELEX_NO" HeaderText="TEL_NO" SortExpression="CP_TELEX_NO" />
                            <asp:BoundField DataField="CP_APGST_NO" HeaderText="APGST_NO" SortExpression="CP_APGST_NO" />
                            <asp:BoundField DataField="CP_CST_NO" HeaderText="CST_NO" ReadOnly="True" SortExpression="CP_CST_NO" />
                            <asp:BoundField DataField="CP_ECC_NO" HeaderText="ECC_NO" SortExpression="CP_ECC_NO" />
                            <asp:BoundField DataField="CP_VAT_NO" HeaderText="VAT_NO" SortExpression="CP_VAT_NO" />
                            <asp:BoundField DataField="CP_PAN_NO" HeaderText="PAN_NO" SortExpression="CP_PAN_NO" />
                            <asp:BoundField DataField="CP_EST_YEAR" HeaderText="EST_YEAR" SortExpression="CP_EST_YEAR" />
                            <asp:BoundField DataField="CP_CF_YEAR" HeaderText="CF_YEAR" ReadOnly="True" SortExpression="CP_CF_YEAR" />
                            <asp:BoundField DataField="CP_CPO_NO" HeaderText="CPO_NO" SortExpression="CP_CPO_NO" />
                            <asp:BoundField DataField="CP_CI_NO" HeaderText="CI_NO" SortExpression="CP_CI_NO" />
                            <asp:BoundField DataField="CP_CDC_NO" HeaderText="CDC_NO" SortExpression="CP_CDC_NO" />
                            <asp:BoundField DataField="CP_YEAR_STARTDATE" HeaderText="STARTDATE" SortExpression="CP_YEAR_STARTDATE" />
                            <asp:BoundField DataField="CP_YEAR_ENDDATE" HeaderText="ENDDATE" ReadOnly="True"
                                SortExpression="CP_YEAR_ENDDATE" />
                            <asp:BoundField DataField="CP_INVOICE_PREFIX" HeaderText="INVOICE_PREFIX" SortExpression="CP_INVOICE_PREFIX" />
                            <asp:BoundField DataField="CP_INVOICE_SUFFIX" HeaderText="INVOICE_SUFFIX" SortExpression="CP_INVOICE_SUFFIX" />
                            <asp:BoundField DataField="CP_PO_PREFIX" HeaderText="PO_PREFIX" SortExpression="CP_PO_PREFIX" />
                            <asp:BoundField DataField="CP_PO_SUFFIX" HeaderText="PO_SUFFIX" SortExpression="CP_PO_SUFFIX" />
                            <asp:BoundField DataField="CP_DC_PREFIX" HeaderText="DC_PREFIX" ReadOnly="True" SortExpression="CP_DC_PREFIX" />
                            <asp:BoundField DataField="CP_DC_SUFFIX" HeaderText="DC_SUFFIX" SortExpression="CP_DC_SUFFIX" />
                            <asp:TemplateField HeaderText="LOGO" SortExpression="CP_LOGO">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("CP_LOGO") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Image ID="Image" runat="server" ImageUrl="~/Images/noimage400x300.gif" Width="89px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            No Record Found
                        </EmptyDataTemplate>
                        <SelectedRowStyle BackColor="LightSteelBlue" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsCompanyProfile" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                        SelectCommand="SELECT * FROM [YANTRA_COMP_PROFILE]"></asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="527px" /></td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload" /></td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

 
