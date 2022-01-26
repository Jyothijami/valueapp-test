<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Employee_Images.aspx.cs" Inherits="Modules_HR_Employee_Images" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
                <td colspan="4" style="text-align: left">
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 21px; text-align: center">
                    &nbsp;<asp:GridView ID="gvProductMasterDetails" runat="server"
                        AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
                        OnRowDataBound="gvProductMasterDetails_RowDataBound" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="EMP_ID" HeaderText="Slno" SortExpression="EMP_ID" ReadOnly="True" />
                            <asp:BoundField DataField="ASSIGNED_EMPID" HeaderText="Assigned Id" SortExpression="ASSIGNED_EMPID">
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Employee Name" SortExpression="EMP_FIRST_NAME" >
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EMP_GENDER" HeaderText="Gender" >
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EMP_MOBILE" HeaderText="Employee Mobile Number" >
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Image">
                                <ItemTemplate>
                                    <asp:Image ID="Image" runat="server" EnableTheming="False" Height="132px" ImageUrl='<%# Eval("EMP_PHOTO","~/Content/EmployeeImage/{0}") %>'
                                        Width="141px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="STATUS" HeaderText="Status" SortExpression="STATUS" ReadOnly="True" />

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
            </table>
    </div>
    </form>
</body>
</html>
