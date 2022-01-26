<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SalaryHeads.aspx.cs" Inherits="Modules_HRManagement_SalaryHeads" Title="|| YANTRA : HR Management : Salary Heads ||" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
<script language="javascript" type="text/javascript">
// <!CDATA[

function TABLE1_onclick() {

}

// ]]>
</script>

    <table id="Table2" onclick="return TABLE1_onclick()" class="pagehead">
        <tr>
            <td colspan="3">
                ADDING SALARY HEADS</td>
        </tr>
    </table>
    <table style="width: 659px" id="TABLE1" onclick="return TABLE1_onclick()" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="3" style="height: 23px">
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: left; height: 20px;" class="profilehead">
                Standard Earning</td>
        </tr>
        <tr>
            <td rowspan="1" style="height: 19px">
            </td>
            <td style="height: 19px; text-align: right">
            </td>
            <td style="width: 79px; height: 19px; text-align: left">
            </td>
        </tr>
        <tr>
            <td rowspan="3">
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            </td>
            <td style="text-align: right">
                <asp:Label id="Label1" runat="server" Text="Basic Salary" Width="95px" Height="2px"></asp:Label></td>
            <td style="width: 79px; text-align: left">
                <asp:CheckBox id="chkBasicSalary" runat="server" Width="125px" OnCheckedChanged="chkBasicSalary_CheckedChanged" Checked="True" Text=" "></asp:CheckBox></td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label id="lblOverTime" runat="server" Text="Over Time"></asp:Label></td>
            <td style="width: 79px; text-align: left">
                <asp:CheckBox id="chkOverTime" runat="server" Width="138px" Checked="True" Text=" ">
                </asp:CheckBox></td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label id="lblBonus" runat="server" Text="Bonus"></asp:Label></td>
            <td style="width: 79px; text-align: left;">
                <asp:CheckBox id="chkBonus" runat="server" Width="110px" Checked="True" Text=" ">
                </asp:CheckBox></td>
        </tr>
        <tr>
            <td colspan="3">
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: left" class="profilehead">
                Standard Deduction</td>
        </tr>
        <tr>
            <td colspan="1" rowspan="1" style="height: 19px">
            </td>
            <td colspan="1" style="height: 19px; text-align: right">
            </td>
            <td colspan="3" style="width: 231px; height: 19px; text-align: left">
            </td>
        </tr>
        <tr>
            <td colspan="1" rowspan="5">
            </td>
            <td colspan="1" style="text-align: right;">
                &nbsp;
                <asp:Label id="lblProfessiona" runat="server" Text="Professional Tax"></asp:Label></td>
            <td colspan="3" style="width: 231px; text-align: left;">
                <asp:CheckBox id="chkProfessionalTax" runat="server" Checked="True" Text=" ">
                </asp:CheckBox></td>
        </tr>
        <tr>
            <td colspan="1" style="text-align: right;">
                <asp:Label id="lblProvident" runat="server" Text="Provident Fund"></asp:Label></td>
            <td colspan="3" style="width: 231px; text-align: left;">
                <asp:CheckBox id="chkProvident" runat="server" Checked="True" Text=" ">
                </asp:CheckBox></td>
        </tr>
        <tr>
            <td colspan="1" style="text-align: right;">
                <asp:Label id="lblLabour" runat="server" Text="Labour Welfare Fund"></asp:Label></td>
            <td colspan="3" style="width: 231px; text-align: left;">
                <asp:CheckBox id="chkLabour" runat="server" Checked="True" Text=" ">
                </asp:CheckBox></td>
        </tr>
        <tr>
            <td colspan="1" style="text-align: right;">
                <asp:Label id="lblTSIC" runat="server" Text="TSIC"></asp:Label></td>
            <td colspan="3" style="width: 231px; text-align: left;">
                <asp:CheckBox id="chkTSIC" runat="server" Checked="True" Text=" ">
                </asp:CheckBox></td>
        </tr>
        <tr>
            <td colspan="1" style="text-align: right;">
                <asp:Label id="lblLoan" runat="server" Text="Loan/Advances"></asp:Label></td>
            <td colspan="3" style="width: 231px; text-align: left;">
                <asp:CheckBox id="chkLoan" runat="server" Checked="True" Text=" ">
                </asp:CheckBox></td>
        </tr>
        <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td colspan="5" style="text-align: left" class="profilehead">
                Salary Heads</td>
        </tr>
        <tr>
            <td rowspan="1" style="height: 19px; text-align: right">
            </td>
            <td style="height: 19px; text-align: right">
            </td>
            <td style="width: 79px; height: 19px; text-align: left">
            </td>
        </tr>
        <tr>
            <td rowspan="1" style="height: 19px; text-align: right;">
                <asp:Label id="Label2" runat="server" Text="Salary  Head"></asp:Label></td>
            <td style="height: 19px; text-align: left">
                <asp:TextBox id="TextBox9" runat="server">
                </asp:TextBox></td>
            <td style="width: 79px; height: 19px; text-align: left">
            </td>
        </tr>
        <tr>
            <td rowspan="1" style="height: 19px; text-align: right">
                <asp:Label id="Label3" runat="server" Text="Type"></asp:Label></td>
            <td style="height: 19px; text-align: left">
                <asp:DropDownList id="DropDownList10" runat="server">
                </asp:DropDownList></td>
            <td style="width: 79px; height: 19px; text-align: left">
            </td>
        </tr>
        <tr>
            <td rowspan="1" style="height: 19px; text-align: right">
            </td>
            <td style="height: 19px; text-align: left">
            </td>
            <td style="width: 79px; height: 19px; text-align: left">
            </td>
        </tr>
        <tr>
            <td rowspan="1" style="height: 19px; text-align: right">
            </td>
            <td style="height: 19px; text-align: left">
                <asp:Button id="Button1" runat="server" BackColor="Transparent" BorderStyle="None"
                    CssClass="loginbutton" EnableTheming="False" Text="Delete" /></td>
            <td style="width: 79px; height: 19px; text-align: left">
            </td>
        </tr>
        <tr>
            <td rowspan="1" style="height: 19px">
            </td>
            <td style="height: 19px; text-align: right">
            </td>
            <td style="width: 79px; height: 19px; text-align: left">
            </td>
        </tr>
        <tr>
            <td colspan="5" style="height: 23px">
                <table border="0" cellpadding="4" cellspacing="0" style="font-weight: normal; font-size: 8pt;
                    color: black; font-family: Verdana; text-align: left; text-decoration: none" width="100%">
                    <tr>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: #ffffff">
                Salary Head</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Type</span></strong></td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                            BASIC</td>
                        <td style="background-color: #eff3fb">
                            Earning</td>
                    </tr>
                    <tr style="font-weight: bold">
                        <td style="background-color: white">
                            HRA</td>
                        <td style="background-color: white">
                            Earning</td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                            Deduction1</td>
                        <td style="background-color: #eff3fb">
                            Deduction</td>
                    </tr>
                    <tr>
                        <td style="background-color: white">
                            DA</td>
                        <td style="background-color: white">
                            Earning</td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                            Deduction2</td>
                        <td style="background-color: #eff3fb">
                            Deduction</td>
                    </tr>
                    <tr>
                        <td style="background-color: white">
                            RENT</td>
                        <td style="background-color: white">
                            Earning</td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                            MEDICLAIM</td>
                        <td style="background-color: #eff3fb">
                            Earning</td>
                    </tr>
                    <tr>
                        <td style="background-color: white">
                            TRAVELLING</td>
                        <td style="background-color: white">
                            Earning</td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                            REEMBESMENT</td>
                        <td style="background-color: #eff3fb">
                            Earning</td>
                    </tr>
                </table>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="7" style="height: 23px">
                &nbsp;<asp:Button id="btnSave" runat="server" Text="Save" />
                <asp:Button id="btnCancel" runat="server" Text="Cancel" /><br />
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="5" style="height: 23px">
            </td>
        </tr>
    </table>
</asp:Content>

