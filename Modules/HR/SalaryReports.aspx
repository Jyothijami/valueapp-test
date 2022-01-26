<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SalaryReports.aspx.cs" Inherits="Modules_HR_SalaryReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>HR || SalaryCalculations</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <asp:Panel ID="Panel2" runat="server" Width="100%">
    <table width="100%">
        <tr>
            <td colspan="3">
            </td>
        </tr>
        <tr>
            <td colspan="3">
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table width="100%">
                    <tr>
                        <td align="left">
                            <asp:Label id="Label1" runat="server" Text="Employee Name :" Width="110px"></asp:Label></td>
                        <td style="width: 100px; height: 21px; text-align: left">
                            <asp:DropDownList id="ddlEmpName" runat="server" OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList></td>
                        <td align="left">
                            <asp:Label id="Label2" runat="server" Text="Age :"></asp:Label></td>
                        <td style="width: 100px; height: 21px; text-align: left">
                            <asp:TextBox id="txtAge" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label id="Label3" runat="server" Text="Enter Gross Amount :" Width="131px"></asp:Label></td>
                        <td style="width: 100px; text-align: left">
                            <asp:TextBox id="txtGrossAmount" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: left;" colspan="2">
                            <asp:Button id="Button1" runat="server" OnClick="Button1_Click" Text="Calculate" />
                            &nbsp;<asp:Button id="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Print" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:Panel id="Panel1" runat="server"  Height="50px" Width="125px">
                            <table id="tblMainas">
                                <tr>
                                    <td colspan="2" style="text-align: center">
                                        <table>
                                            <tr>
                                                <td style="text-align: right; width: 113px;">
                                                    <asp:Label id="Label4" runat="server" Text="Employee Name :" Width="110px"></asp:Label></td>
                                                <td style="text-align: left">
                                                    <asp:Label id="lblEmpname" runat="server"></asp:Label></td>
                                                <td style="text-align: right">
                                                    <asp:Label id="Label5" runat="server" Text="Age :"></asp:Label></td>
                                                <td style="text-align: left">
                                                    <asp:Label id="lblAge" runat="server"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center">
                            <table border="3" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td style="width: 100px">
                                        Basic</td>
                                    <td style="width: 100px">
                                        <asp:TextBox id="txtBasic1" runat="server">
                                        </asp:TextBox></td>
                                    <td style="width: 100px">
                                        <asp:TextBox id="txtBasic2" runat="server">
                                        </asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        HRA</td>
                                    <td style="width: 100px">
                                        <asp:TextBox id="txtHRA1" runat="server">
                                        </asp:TextBox></td>
                                    <td style="width: 100px">
                                        <asp:TextBox id="txtHRA2" runat="server">
                                        </asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        Other</td>
                                    <td style="width: 100px">
                                        <asp:TextBox id="txtOther1" runat="server">
                                        </asp:TextBox></td>
                                    <td style="width: 100px">
                                        <asp:TextBox id="txtOther2" runat="server">
                                        </asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; background-color: #9999cc">
                                        Gross</td>
                                    <td style="width: 100px; background-color: #9999cc">
                                        <asp:TextBox id="txtGrossTotal1" runat="server">
                                        </asp:TextBox></td>
                                    <td style="width: 100px; background-color: #9999cc">
                                        <asp:TextBox id="txtGrossTotal2" runat="server">
                                        </asp:TextBox></td>
                                </tr>
                            </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                            <table border="3" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td colspan="3" style="background-color: #99ccff; text-align: left">
                                        Employee Contribution</td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        PF</td>
                                    <td style="width: 100px">
                                        <asp:TextBox id="txtPF1" runat="server">
                                        </asp:TextBox></td>
                                    <td style="width: 100px">
                                        <asp:TextBox id="txtPF2" runat="server">
                                        </asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        Medi</td>
                                    <td style="width: 100px">
                                        <asp:TextBox id="txtMedi1" runat="server">
                                        </asp:TextBox></td>
                                    <td style="width: 100px">
                                        <asp:TextBox id="txtMedi2" runat="server">
                                        </asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        Bonus</td>
                                    <td style="width: 100px">
                                        <asp:TextBox id="txtBonus1" runat="server">
                                        </asp:TextBox></td>
                                    <td style="width: 100px">
                                        <asp:TextBox id="txtBonus2" runat="server">
                                        </asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; background-color: #6699cc">
                                        Total</td>
                                    <td style="width: 100px; background-color: #6699cc">
                                        <asp:TextBox id="txtECTotal1" runat="server">
                                        </asp:TextBox></td>
                                    <td style="width: 100px; background-color: #6699cc">
                                        <asp:TextBox id="txtECTotal2" runat="server">
                                        </asp:TextBox></td>
                                </tr>
                            </table>
                                    </td>
                                    </tr>
                                <tr>
                                    <td valign="top" style="text-align: right">
                            <table border="3" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td colspan="3" style="background-color: #33ccff; text-align: left">
                                        Employee Deduction</td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        PF</td>
                                    <td colspan="2">
                                        <asp:TextBox id="txtEDpf" runat="server">
                                        </asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        Ptax</td>
                                    <td colspan="2">
                                        <asp:TextBox id="txtEDptax" runat="server">
                                        </asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        Total
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox id="txtEDTotal" runat="server">
                                        </asp:TextBox></td>
                                </tr>
                            </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table border="3" cellpadding="2" cellspacing="2" style="width: 303px">
                                            <tr>
                                                <td style="width: 119px">Total CTC(PM)</td>
                                                <td style="width: 100px">
                                                    <asp:TextBox ID="txtCTCPM" runat="server">
                                        </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 119px">Total CTC(PA)</td>
                                                <td style="width: 100px">
                                                    <asp:TextBox ID="txtCTCPA" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 100px">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table border="3" cellpadding="2" cellspacing="2">
                                            <tr>
                                                <td style="width: 100px">Net</td>
                                                <td style="width: 100px">
                                                    <asp:TextBox ID="txtNetSal" runat="server">
                                        </asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 100px">
                                    </td>
                                </tr>
                            </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;" colspan="4">
                            

                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                        </td>
                        <td style="width: 100px">
                        </td>
                        <td style="width: 100px">
                        </td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 100px; height: 21px">
            </td>
            <td style="width: 100px; height: 21px">
            </td>
            <td style="width: 100px; height: 21px">
            </td>
        </tr>
    </table>
     </asp:Panel>
</asp:Content>

