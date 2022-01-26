<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmpImage.aspx.cs" Inherits="Modules_Masters_EmpImage" Title="Untitled Page" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
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
                <td colspan="4" style="text-align: right">
                                <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                    Text="Search By" Width="86px"></asp:Label>
                                <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="textbox">
                                    <asp:ListItem Value="0">--</asp:ListItem>
                                    <asp:ListItem Value="EMP_FIRST_NAME">Emp Name</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="txtSearchText" runat="server" CssClass="textbox">
                                        </asp:TextBox>
                                <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                    CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" />
                    <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 21px; text-align: center">
                    
                    <asp:GridView ID="gvProductMasterDetails" runat="server" AllowPaging="True"
                        AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource2"
                        OnRowDataBound="gvProductMasterDetails_RowDataBound" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="EMP_ID" HeaderText="Emp Id" ReadOnly="True" />
                            <asp:BoundField DataField="ASSIGNED_EMPID" HeaderText="Assigned Id" >
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Employee Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ITEM_NAME") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" ForeColor="#0066ff" Text='<%# Eval("EMP_FIRST_NAME") %>'></asp:LinkButton>
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
                                    <asp:Image ID="Image1" runat="server" EnableTheming="False" Height="132px" ImageUrl='<%# Eval("EMP_PHOTO","~/Content/EmployeeImage/{0}") %>' Width="141px" />
                                    <%--<asp:Image ID="Image1" runat="server" EnableTheming="False" Height="132px" ImageUrl="~/Images/noimage400x300.gif" Width="141px" />--%>

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <SelectedRowStyle BackColor="LightSteelBlue" />
                    </asp:GridView>

                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_EmpImage_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue" PropertyName="Text" Type="String" />
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
            </table>
    
    </div>
    </form>
</body>
</html>
 
