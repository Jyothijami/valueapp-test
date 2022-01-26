<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmpImage.aspx.cs" Inherits="Modules_Masters_EmpImage" Title="Untitled Page" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
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
                                Employee Images</td>
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
                    &nbsp;<asp:GridView ID="gvProductMasterDetails" runat="server" AllowPaging="True"
                        AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
                        OnRowDataBound="gvProductMasterDetails_RowDataBound" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="EMP_ID" HeaderText="Slno" ReadOnly="True" />
                            <asp:BoundField DataField="ASSIGNED_EMPID" HeaderText="Assigned Id" >
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Employee Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ITEM_NAME") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Text='<%# Eval("EMP_FIRST_NAME") %>'></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="EMP_GENDER" HeaderText="Gender" >
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Image">
                                <ItemTemplate>
                                    <asp:Image ID="Image" runat="server" EnableTheming="False" Height="132px" ImageUrl="~/Images/noimage400x300.gif"
                                        Width="141px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <SelectedRowStyle BackColor="LightSteelBlue" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                        SelectCommand="SELECT * FROM [YANTRA_EMPLOYEE_MAST] WHERE ([EMP_ID] > @EMP_ID)">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="0" Name="EMP_ID" Type="Int64" />
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