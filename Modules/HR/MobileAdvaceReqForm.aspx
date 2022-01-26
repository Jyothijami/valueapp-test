<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true" CodeFile="MobileAdvaceReqForm.aspx.cs" Inherits="Modules_HR_MobileAdvaceReqForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <asp:UpdatePanel ID="staffAdvancepnl" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tr class="pagehead">
                    <td style="text-align: left">Mobile Advance Form 
                    </td>
                    <td style="text-align: right"></td>
                </tr>
            </table>
            <br />
            <table style="width: 100%">
                <tr>
                    <td style="text-align: right">Employee Name :
                        <asp:DropDownList ID="ddlEmployee" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource2" DataTextField="Column1" DataValueField="EMP_ID">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT     dbo.YANTRA_EMPLOYEE_MAST.EMP_ID, dbo.YANTRA_EMPLOYEE_MAST.EMP_FIRST_NAME + ' ' +  dbo.YANTRA_EMPLOYEE_MAST.EMP_LAST_NAME
FROM         dbo.YANTRA_EMPLOYEE_DET INNER JOIN
dbo.YANTRA_EMPLOYEE_MAST ON dbo.YANTRA_EMPLOYEE_DET.EMP_ID = dbo.YANTRA_EMPLOYEE_MAST.EMP_ID 
where YANTRA_EMPLOYEE_DET.EMP_DET_DOT &gt;= GETDATE()"></asp:SqlDataSource>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="text-align: right">Date : 
                    </td>
                    <td>

                        <asp:TextBox ID="txtDate" type="date" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr><td colspan="5">&nbsp;</td></tr>
                <tr>
                    <td style="text-align: right">Designation :
                        <asp:TextBox ID="txtDesg" runat="server" ></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="text-align: right"> </td>
                <td style="text-align:left">
                    <%--<asp:TextBox runat="server" ID="txtEx"></asp:TextBox>--%>
                </td>
                </tr>
                <tr><td colspan="5">&nbsp;</td></tr>

                <tr>
                    <td style="text-align: right"> Amount :
                        <asp:TextBox ID="txtAmount" runat="server" ></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="text-align: right" >EMI per Month : 
                    </td>
                    <td>
                        <asp:TextBox ID="txtEMI" runat="server"></asp:TextBox>
                    </td>
                </tr>
                </tr>
                <tr><td colspan="5">&nbsp;</td></tr>
            <tr>
                    <td style="text-align: right"> Extra Field :
                        <asp:TextBox ID="txtExtraField" runat="server" ></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="text-align: right" > 
                    </td>
                    <td>
                    </td>
                </tr>
                <tr><td colspan="5">&nbsp;</td></tr>
                <tr><td colspan="5" style="text-align:center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" />

                    </td></tr>
            </table>
            <br />
            <table style="width:100%">
                <tr>
                    <td>
                        <asp:GridView ID="gvMobileAdv" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" PageSize="8" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Satff Id">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnId" runat="server" ForeColor="#0066ff" OnClick="lbtnId_Click" Text="<%# Bind('Id') %>"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Emp_Name" HeaderText="Emp_Name" SortExpression="Emp_Name">
                                </asp:BoundField>
                                <asp:BoundField DataField="Designation" HeaderText="Designation" SortExpression="Designation">
                                </asp:BoundField>
                                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" ReadOnly="True">
                                </asp:BoundField>
                                <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount">
                                </asp:BoundField>
                                <asp:BoundField DataField="EMI_Amount" HeaderText="EMI_Amount" SortExpression="EMI_Amount" />
                                <asp:BoundField DataField="Extra" HeaderText="Extra Field">
                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Id" HeaderText="Hudden" />
                            </Columns>
                            <SelectedRowStyle BackColor="#99CCFF" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand=" select c.Id, c.Emp_Name, c.Designation, convert(varchar(10), c.Date,103) as Date, c.Amount, c.EMI_Amount,c.Extra from dbo.YANTRA_DESG_MAST a inner join dbo.YANTRA_EMPLOYEE_DET b on a.DESG_ID=b.DESG_ID inner join 
                       dbo.Staff_Mobile_Advance_tbl c on b.EMP_ID= c.Emp_Id order by c.Id desc"></asp:SqlDataSource>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

