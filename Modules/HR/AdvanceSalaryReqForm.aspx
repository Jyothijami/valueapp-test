<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true" CodeFile="AdvanceSalaryReqForm.aspx.cs" Inherits="Modules_HR_AdvanceSalaryReqForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 36px;
        }
        .auto-style2 {
            width: 5%;
            height: 36px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <asp:UpdatePanel ID="staffAdvancepnl" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tr class="pagehead">
                    <td style="text-align: left">Salary Advance Request Form&nbsp;
                    </td>
                    <td style="text-align: right"></td>
                </tr>
            </table>
            <br />
            <table style="width: 100%">
                <tr>
                    <td style="text-align: right">Employee Name :
                        <asp:DropDownList ID="ddlEmployee" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
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
                    <td style="text-align: right">VL / SALADV /: </td>
                <td style="text-align:left">
                    <asp:TextBox runat="server" ID="txtEx"></asp:TextBox>
                </td>
                </tr>
                <tr><td colspan="5">&nbsp;</td></tr>

                <tr>
                    <td style="text-align: right">Designation :
                        <asp:TextBox ID="txtDesg" runat="server" ></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="text-align: right" >Deduction In Amount : 
                    </td>
                    <td>
                        <asp:TextBox ID="txtDeductionAmount" runat="server"></asp:TextBox>
                    </td>
                </tr>
                </tr>
                <tr><td colspan="5">&nbsp;</td></tr>
                <tr>
                      <td style="text-align: right" class="auto-style1">Purpose :
                        <asp:TextBox ID="txtPurpose" runat="server" TextMode="MultiLine" ></asp:TextBox>
                    </td>
                    <td class="auto-style2"></td>
                    <td style="text-align: right" class="auto-style1" >Amount Old Due : 
                    </td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtOldDue" runat="server"></asp:TextBox>
                    </td>
                </tr>
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
                        <asp:GridView ID="gvSalAdv" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" SelectedRowStyle-BackColor="#c0c0c0" DataSourceID="SqlDataSource1" PageSize="8" Width="100%" OnRowDataBound="gvSalAdv_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Satff Id">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnId" runat="server" ForeColor="#0066FF" OnClick="lbtnId_Click" Text="<%# Bind('Sal_Adv_Id') %>"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Employee_name" HeaderText="Employee Name" SortExpression="Employee_name">
                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Designation" HeaderText="Designation" SortExpression="Designation" >
                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Date" HeaderText="Date" ReadOnly="True" SortExpression="Date">
                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount">
                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Purpose" HeaderText="Purpose" SortExpression="Purpose">
                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Deduction Salary" HeaderText="Deduction Salary" SortExpression="Deduction Salary">
                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="sal_Adv_Old_Due" HeaderText="Old Due" SortExpression="sal_Adv_Old_Due">
                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="VL_SALADV" HeaderText="VL/SALADV" SortExpression="VL_SALADV" >
                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>                                
                                <asp:BoundField DataField="Sal_Adv_Id" HeaderText="Hudden" >
                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="USP_SalaryAdvance" SelectCommandType="StoredProcedure">
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


 
