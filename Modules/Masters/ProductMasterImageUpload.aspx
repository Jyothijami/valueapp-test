<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductMasterImageUpload.aspx.cs" Inherits="Modules_Masters_ProductMasterImageUpload" %>

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
                            <td style="width: 272px; text-align: left">
                                Product&nbsp; Master</td>
                            <td style="text-align: right">
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
                    <asp:GridView ID="gvProductMasterDetails" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" DataSourceID="sdsProductMaster" OnRowDataBound="gvProductMasterDetails_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="Product_Name" HeaderText="ProductMasterNameHidden" />
                            <asp:BoundField DataField="Product_Id" HeaderText="Sl No">
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Product_Code" HeaderText="Product Code" />
                            <asp:TemplateField HeaderText="Product Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Item_name") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnProductName" runat="server" CausesValidation="False" OnClick="lbtnProductName_Click"
                                        Text="<%# Bind('Product_Name') %>"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ReorderLevel" HeaderText="Min Stock Quantity">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Rate" HeaderText="Rate">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Image">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Image") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Image ID="Image" runat="server" Height="41px" ImageUrl="~/Images/noimage400x300.gif"
                                        Width="62px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <SelectedRowStyle BackColor="LightSteelBlue" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsProductMaster" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                        SelectCommand="select * from YANTRA_LKUP_PRODUCT_MASTER">
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
                            <td style="text-align: left">
                                &nbsp;<asp:FileUpload ID="FileUpload1" runat="server" Width="527px" /></td>
                            <td style="text-align: right">
                            </td>
                            <td style="text-align: left">
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                            </td>
                            <td style="text-align: left">
                                &nbsp;<asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload" /></td>
                            <td style="text-align: right">
                            </td>
                            <td style="text-align: left">
                                &nbsp;
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

 
