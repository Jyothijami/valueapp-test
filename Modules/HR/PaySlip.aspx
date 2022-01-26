<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PaySlip.aspx.cs" Inherits="Modules_HR_PaySlip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table class="pagehead" style="width: 100%">
        <tr>
            <td colspan="4" style="text-align: left;">Employee PaySlip</td>
        </tr>
    </table>
    <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left; width: 100%;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">Pay Slip Generation</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
                <asp:Label ID="lblEmpId" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblGrossSalary" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblAge" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblDOB" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblAccNo" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblDOJ" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblSal" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblSal1" runat="server" Visible="false"></asp:Label>


            </td>
        </tr>
        <tr>
            <td class="text-right">Employee Name :</td>
            <td>
                <asp:TextBox ID="txtName" Enabled="false" runat="server"></asp:TextBox>
            </td>
            <td class="text-right">Department :</td>
            <td>
                <asp:TextBox ID="txtDepartment" Enabled="false" runat="server"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td class="text-right">Designation :</td>
            <td>
                <asp:TextBox ID="txtDesignation" Enabled="false" runat="server"></asp:TextBox>
            </td>

            <td class="text-right">Year :</td>
            <td>
                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="False"></asp:DropDownList>
            </td>
        </tr>

        <tr>
           

            <td>
                <asp:TextBox ID="txtNoOfDays" Visible="false" Enabled="true" runat="server"></asp:TextBox>

                <%--Paid Days--%>

            </td>
            <td>
                <asp:TextBox ID="txtPaidDays" Visible="false" Enabled="true" runat="server"></asp:TextBox>

            </td>
             <td style="text-align: right">
                Month :

            </td>
            <td>
                <asp:DropDownList ID="ddlMonth" runat="server"  AppendDataBoundItems="True"  DataTextField="Month" DataValueField="ID" DataSourceID="SqlDataSource1">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                    
                </asp:DropDownList>

                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [Month], [ID] FROM [Month_Calendar_tbl]"></asp:SqlDataSource>

            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
        <tr>
            <td colspan="4" style="text-align:center">
                <asp:Button ID="btnDownload" runat="server" Text="Download Pay Slip" Visible="false" OnClick="btnDownload_Click" />
                <asp:Button ID="btnGenerate" runat="server" Text="Generate PaySlip" OnClick="btnGenerate_Click" />
            </td>
        </tr>
    </table>

    <table id="tblSalarybreakup" runat="server" visible="false">
        <tr>
            <td>
                <h3>Employee Earnings</h3>
                <table cellpadding="5" cellspacing="0">
                    <tr>
                        <td style="width: 150px">Basic</td>
                        <td>
                            <asp:TextBox ID="txtBasic1" runat="server">
                            </asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBasic2" runat="server">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px">HRA</td>
                        <td>
                            <asp:TextBox ID="txtHRA1" runat="server">
                            </asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHRA2" runat="server">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Other</td>
                        <td>
                            <asp:TextBox ID="txtOther1" runat="server">
                            </asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOther2" runat="server">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-top-style: solid; border-top-width: 1px; border-top-color: #CCCCCC"><b>Gross</b></td>
                        <td style="border-top-style: solid; border-top-width: 1px; border-top-color: #CCCCCC">
                            <asp:TextBox ID="txtGrossTotal1" runat="server">
                            </asp:TextBox>
                        </td>
                        <td style="border-top-style: solid; border-top-width: 1px; border-top-color: #CCCCCC">
                            <asp:TextBox ID="txtGrossTotal2" runat="server">
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />

                <h3>Employee Contribution</h3>
                <table cellpadding="5" cellspacing="0">
                    <tr>
                        <td style="width: 150px">PF</td>
                        <td>
                            <asp:TextBox ID="txtPF1" runat="server">
                            </asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPF2" runat="server">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Medi</td>
                        <td>
                            <asp:TextBox ID="txtMedi1" runat="server">
                            </asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMedi2" runat="server">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Bonus</td>
                        <td>
                            <asp:TextBox ID="txtBonus1" runat="server">
                            </asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBonus2" runat="server">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-top-style: solid; border-top-width: 1px; border-top-color: #CCCCCC">Total</td>
                        <td style="border-top-style: solid; border-top-width: 1px; border-top-color: #CCCCCC">
                            <asp:TextBox ID="txtECTotal1" runat="server">
                            </asp:TextBox>
                        </td>
                        <td style="border-top-style: solid; border-top-width: 1px; border-top-color: #CCCCCC">
                            <asp:TextBox ID="txtECTotal2" runat="server">
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <h3>Employee Deductions</h3>
                <table cellpadding="5" cellspacing="0">
                    <tr>
                        <td style="width: 150px">PF</td>
                        <td>
                            <asp:TextBox ID="txtEDpf" runat="server">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Ptax</td>
                        <td>
                            <asp:TextBox ID="txtEDptax" runat="server">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-top-style: solid; border-top-width: 1px; border-top-color: #CCCCCC">Total </td>
                        <td style="border-top-style: solid; border-top-width: 1px; border-top-color: #CCCCCC">
                            <asp:TextBox ID="txtEDTotal" runat="server">
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <table cellpadding="5" cellspacing="0">
                    <tr>
                        <td style="width: 150px">Total CTC(PM)</td>
                        <td>
                            <asp:TextBox ID="txtCTCPM" runat="server">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Total CTC(PA)</td>
                        <td>
                            <asp:TextBox ID="txtCTCPA" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <table cellpadding="5" cellspacing="0">
                    <tr>
                        <td style="width: 150px"><b>Net</b></td>
                        <td>
                            <asp:TextBox ID="txtNetSal" runat="server">
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

</asp:Content>

