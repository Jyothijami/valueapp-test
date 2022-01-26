<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true" CodeFile="Staff_Tour_Advance.aspx.cs" Inherits="Modules_HR_Staff_Tour_Advance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="staffAdvancepnl" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tr class="pagehead">
                    <td style="text-align: left">Staff Tour Advance
                    </td>
                    <td style="text-align: right"></td>
                </tr>
            </table>
            <br />
            <table style="width: 100%">
                <tr>
                    <td style="text-align: right">Employee Name :
                        <asp:DropDownList ID="ddlEmployee" runat="server">
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

                        <asp:TextBox ID="txtDate" type="datepic" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr><td colspan="5">&nbsp;</td></tr>
                <tr>
                    <td style="text-align: right">Amount :
                        <asp:TextBox ID="txtAmount" runat="server" ></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="text-align: right">Date Of Travel : 
                    </td>
                    <td>
                        <asp:TextBox ID="txtDateOfArrival" type="datepic" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr><td colspan="5">&nbsp;</td></tr>

                <tr>
                    <td style="text-align: right">Destination :
                        <asp:TextBox ID="txtDestination" runat="server" ></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="text-align: right" >Tour Tenure<br /> &nbsp;(No Of Days) : 
                    </td>
                    <td>
                        <asp:TextBox ID="txtNoOfDays" runat="server"></asp:TextBox>
                    </td>
                </tr>
                 <tr><td colspan="5">&nbsp;</td></tr>
                <tr>
                      <td style="text-align: right">Purpose :
                        <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" ></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                <tr><td colspan="5">&nbsp;</td></tr>
                <tr><td colspan="5" style="text-align:center">
                    <asp:Label ID="lblUserType" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" />

                    </td></tr>
            </table>
            <br />
            <table style="width:100%">
                <tr>
                    <td>
                        <asp:GridView ID="gvStaffTour" runat="server" SelectedRowStyle-BackColor="#c0c0c0" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" PageSize="8" Width="100%" OnRowDataBound="gvStaffTour_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Staff Id">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                            <asp:LinkButton ID="lbtnId" runat="server" ForeColor="#0066ff"  OnClick="lbtnId_Click" Text="<%# Bind('ID') %>"></asp:LinkButton>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name">
                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DESG_NAME" HeaderText="Designation">
                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Date" HeaderText="Date" />
                                <asp:BoundField DataField="Amount" HeaderText="Amount">
                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Date_Of_Travel" HeaderText="Date Of Travel">
                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Destination" HeaderText="Destination">
                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Tour_Tenure" HeaderText="Tour_Tenure(No Of Days)">
                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Comment" HeaderText="Purpose">
                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ID" HeaderText="Hudden" />
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="USP_StaffTourAdvance" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="lblEmpIdHidden" DefaultValue="0" Name="EmpId" PropertyName="Text" Type="Int64" />
                                <asp:ControlParameter ControlID="lblUserType" DefaultValue="0" Name="userType" PropertyName="Text" Type="Int64" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

