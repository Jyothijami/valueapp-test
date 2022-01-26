<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LoanManagment.aspx.cs" Inherits="Modules_HRManagement_LoanManagment" Title="|| YANTRA : HR Management : Loan Management ||" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table class="pagehead">
        <tr>
            <td colspan="3" style="text-align: left">
                ADDING &nbsp;LOAN&nbsp; DETAILS</td>
        </tr>
    </table>
    <br />
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="3" style="height: 19px">
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: left" class="profilehead">
                &nbsp;General Details</td>
        </tr>
        <tr>
            <td colspan="3" style="height: 19px">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="text-align: right">
                <asp:Label id="lblEmployee" runat="server" Text="Employee Name"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList id="ddlEmployee" runat="server">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="text-align: right">
                <asp:Label id="Label1" runat="server" Text="Enter the Amount of Loan Sanctioned"
                    Width="283px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtAmount" runat="server">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="text-align: right">
                &nbsp;<asp:Label id="Label2" runat="server" Text="Description of the Loan" Width="179px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="TXTdESCRIPTION" runat="server" TextMode="MultiLine"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="height: 26px">
            </td>
            <td style="height: 26px; text-align: right">
                <asp:Label id="Label3" runat="server" Text="Date On Which Loan Sanctioned"></asp:Label></td>
            <td style="height: 26px; text-align: left">
                <asp:TextBox id="TextBox1" runat="server" Height="16px" Width="127px">
                </asp:TextBox>&nbsp;<asp:Image id="Image2" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image></td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="text-align: right">
                <asp:Label id="Label4" runat="server" Text="Type Of Deduction"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList id="DropDownList1" runat="server">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="text-align: right">
                <asp:Label id="Label5" runat="server" Text="Deduct Amount Per Instalation"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtDeduct" runat="server">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="text-align: right">
                <asp:Label id="Label6" runat="server" Text="No Of Instalments"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtInstalments" runat="server">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Label id="Label7" runat="server" Text="The Month From Which  Deduction Is Going To Start"
                    Width="331px"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList id="ddlDeductionStart" runat="server">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="height: 19px">
            </td>
            <td style="height: 19px">
            </td>
            <td style="text-align: left; height: 19px;">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2" style="text-align: left" class="profilehead">
                &nbsp;Installments</td>
        </tr>
        <tr>
            <td style="height: 45px">
            </td>
            <td colspan="2" style="height: 45px">
                <table border="0" cellpadding="4" cellspacing="0" style="font-weight: normal; font-size: 8pt;
                    color: black; font-family: Verdana; text-align: left; text-decoration: none" width="100%">
                    <tr>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Month-Year</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Installments</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: white">Paid</span></strong></td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                            January-2007</td>
                        <td style="background-color: #eff3fb">
                            5000.00</td>
                        <td style="background-color: #eff3fb">
                            <span>0</span></td>
                    </tr>
                    <tr >
                        <td style="background-color: white">
                            July-2007</td>
                        <td style="background-color: white">
                            5000.00</td>
                        <td style="background-color: white">
                            <span >0</span></td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                            January-2008</td>
                        <td style="background-color: #eff3fb">
                            5000.00</td>
                        <td style="background-color: #eff3fb">
                          <span>0</span></td>
                    </tr>
                    <tr>
                        <td style="background-color: white">
                            July-2007</td>
                        <td style="background-color: white">
                            5000.00</td>
                        <td style="background-color: white">
                            <span >0</span></td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                            January-2009</td>
                        <td style="background-color: #eff3fb">
                            5000.00</td>
                        <td style="background-color: #eff3fb">
                           <span >0</span></td>
                    </tr>
                    <tr>
                        <td style="background-color: white">
                            July-2007</td>
                        <td style="background-color: white">
                            5000.00</td>
                        <td style="background-color: white">
                           <span >0</span></td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                            January-2010</td>
                        <td style="background-color: #eff3fb">
                            5000.00</td>
                        <td style="background-color: #eff3fb">
                           <span>0</span></td>
                    </tr>
                    <tr>
                        <td style="background-color: white">
                            July-2007</td>
                        <td style="background-color: white">
                            5000.00</td>
                        <td style="background-color: white">
                            <span >0</span></td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                            January-2011</td>
                        <td style="background-color: #eff3fb">
                            5000.00</td>
                        <td style="background-color: #eff3fb">
                            <span >0</span></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 19px">
            </td>
            <td style="height: 19px">
            </td>
            <td style="height: 19px">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2">
                <asp:Button id="btnSave" runat="server" Text="Save" Width="59px" />
                <asp:Button id="btnCancel"
                    runat="server" Text="Cancel" /><br />
                <br />
            </td>
        </tr>
    </table>
</asp:Content>

