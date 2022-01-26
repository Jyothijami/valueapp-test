﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true" CodeFile="MobileAdvanceReqForm.aspx.cs" Inherits="Modules_HR_MobileAdvaceReqForm" %>

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
                    <td style="text-align: right">Designation :
                        <%--<asp:DropDownList ID="ddlDesg" runat="server" OnSelectedIndexChanged="ddlDesg_SelectedIndexChanged"></asp:DropDownList><br />--%>
                        <asp:TextBox ID="txtDesg" runat="server"></asp:TextBox>
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
                    <td style="text-align: right"> Purpose :
                        <asp:TextBox ID="txtExtraField" runat="server" TextMode="MultiLine" ></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="text-align: right" > 
                    </td>
                    <td>
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
                        <asp:GridView ID="gvMobileAdv" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" SelectedRowStyle-BackColor="#c0c0c0" DataSourceID="SqlDataSource1" PageSize="8" Width="100%" OnRowDataBound="gvMobileAdv_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Satff Id">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnId" runat="server" ForeColor="#0066ff" OnClick="lbtnId_Click" Text="<%# Bind('Id') %>"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Emp_Name" HeaderText="Emp_Name" SortExpression="Emp_Name">
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Designation" HeaderText="Designation" SortExpression="Designation">
                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount">
                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="EMI_Amount" HeaderText="EMI_Amount" SortExpression="EMI_Amount">
                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Extra" HeaderText="Purpose">
                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Id" HeaderText="Hudden" >
                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="USP_MobileAdvance" SelectCommandType="StoredProcedure">
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

