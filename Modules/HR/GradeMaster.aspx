<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GradeMaster.aspx.cs" Inherits="Modules_HRManagement_GradeMaster" Title="|| YANTRA : HR Management : Grade Master ||" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table class="pagehead">
        <tr>
            <td colspan="3" style="text-align: left">
                ADDING GRADE</td>
        </tr>
    </table>
    <br />
    <table style="width: 242px" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="3">
            </td>
            <td colspan="1">
            </td>
            <td colspan="1">
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: left" class="profilehead">
                &nbsp;General Details</td>
            <td class="profilehead" colspan="1" style="text-align: left">
            </td>
            <td class="profilehead" colspan="1" style="text-align: left">
            </td>
        </tr>
        <tr>
            <td style="width: 18px; height: 19px">
            </td>
            <td style="height: 19px; text-align: right">
            </td>
            <td style="width: 44201px; height: 19px; text-align: left">
            </td>
            <td style="width: 44201px; height: 19px; text-align: left">
            </td>
            <td style="width: 44201px; height: 19px; text-align: left">
            </td>
        </tr>
        <tr>
            <td style="width: 18px">
             
            </td>
            <td style="text-align: right;">
               <asp:Label id="lblGradeName" runat="server" Text="Grade Name" Width="92px"></asp:Label>
                
            </td>
            <td style="width: 44201px; text-align: left;">
                <asp:TextBox id="txtGrade" runat="server">
                </asp:TextBox></td>
            <td style="width: 44201px; text-align: left">
            </td>
            <td style="width: 44201px; text-align: left">
            </td>
        </tr>
        <tr>
            <td style="width: 18px">
            </td>
            <td>
            </td>
            <td style="width: 44201px">
            </td>
            <td style="width: 44201px">
            </td>
            <td style="width: 44201px">
            </td>
        </tr>
        <tr>
            <td style="width: 18px">
            </td>
            <td class="profilehead" colspan="4" style="text-align: left">
                &nbsp;Salary Details</td>
        </tr>
        <tr>
            <td style="width: 18px; height: 19px">
            </td>
            <td style="height: 19px">
            </td>
            <td style="width: 44201px; height: 19px">
            </td>
            <td style="width: 44201px; height: 19px">
            </td>
            <td style="width: 44201px; height: 19px">
            </td>
        </tr>
        <tr>
            <td style="width: 18px">
            </td>
            <td style="text-align: right">
                <asp:Label id="Label1" runat="server" Text="Code No"></asp:Label></td>
            <td style="width: 44201px; text-align: left">
                <asp:TextBox id="TextBox27" runat="server">
                </asp:TextBox></td>
            <td style="width: 44201px; text-align: right">
                <asp:Label id="Label3" runat="server" Text="Salary Head" Width="86px"></asp:Label></td>
            <td style="width: 44201px">
                <asp:TextBox id="TextBox29" runat="server">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 18px; height: 19px">
            </td>
            <td style="height: 19px; text-align: right">
                <asp:Label id="Label2" runat="server" Text="Amount/Formula" Width="115px"></asp:Label></td>
            <td style="width: 44201px; height: 19px">
                <asp:TextBox id="TextBox28" runat="server" TextMode="MultiLine">
                </asp:TextBox></td>
            <td style="width: 44201px; height: 19px">
            </td>
            <td style="width: 44201px; height: 19px">
            </td>
        </tr>
        <tr>
            <td style="width: 18px; height: 18px">
            </td>
            <td style="height: 18px; text-align: right">
            </td>
            <td style="width: 44201px; height: 18px">
            </td>
            <td style="width: 44201px; height: 18px">
            </td>
            <td style="width: 44201px; height: 18px">
            </td>
        </tr>
        <tr>
            <td style="width: 18px; height: 19px">
            </td>
            <td style="height: 19px; text-align: right">
            </td>
            <td style="width: 44201px; height: 19px">
            </td>
            <td style="width: 44201px; height: 19px">
                <asp:Button id="Button1" runat="server" BackColor="Transparent" BorderStyle="None"
                    CssClass="loginbutton" EnableTheming="False" Text="Include" /></td>
            <td style="width: 44201px; height: 19px">
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: left; height: 19px;">
                </td>
            <td colspan="1" style="height: 19px; text-align: left">
            </td>
            <td colspan="1" style="height: 19px; text-align: left">
            </td>
        </tr>
        <tr>
            <td style="width: 18px">
            </td>
            <td colspan="4">
                <table border="0" cellpadding="4" cellspacing="0" style="font-weight: normal; font-size: 8pt;
                    color: black; font-family: Verdana; text-align: left; text-decoration: none;" width="100%">
                    <tr>
                        <td style="height: 21px; background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Code No</span></strong></td>
                        <td style="height: 21px; background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Salary Head</span></strong></td>
                        <td style="height: 21px; background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Amount/Formula</span></strong></td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                            1001</td>
                        <td style="background-color: #eff3fb">
                            BASIC</td>
                        <td style="background-color: #eff3fb">
                            4500</td>
                    </tr>
                    <tr style="font-weight: bold">
                        <td style="background-color: white">
                            42</td>
                        <td style="background-color: white">
                            HRA</td>
                        <td style="background-color: white">
                            (1001)*20/100</td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                            40</td>
                        <td style="background-color: #eff3fb">
                            CONVAYENCE</td>
                        <td style="background-color: #eff3fb">
                            (1001)*12.5/100</td>
                    </tr>
                    <tr>
                        <td style="background-color: white">
                            47</td>
                        <td style="background-color: white">
                            OTHER ALLOWANCES</td>
                        <td style="background-color: white">
                            (1001)*12.5/100</td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                            43</td>
                        <td style="background-color: #eff3fb">
                            DA</td>
                        <td style="background-color: #eff3fb">
                            (1001)*25/100</td>
                    </tr>
                    <tr>
                        <td style="background-color: white">
                            1002</td>
                        <td style="background-color: white">
                            PROFESSIONAL TAX</td>
                        <td style="background-color: white">
                            0</td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                            1000</td>
                        <td style="background-color: #eff3fb">
                            LOAN/ADVANCES</td>
                        <td style="background-color: #eff3fb">
                            0</td>
                    </tr>
                    <tr>
                        <td style="background-color: white">
                            45</td>
                        <td style="background-color: white">
                            SLA</td>
                        <td style="background-color: white">
                            1001</td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                            11</td>
                        <td style="background-color: #eff3fb">
                            TDS</td>
                        <td style="background-color: #eff3fb">
                            0</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 18px; height: 19px;">
            </td>
            <td style="height: 19px">
            </td>
            <td style="width: 44201px; height: 19px;">
            </td>
            <td style="width: 44201px; height: 19px">
            </td>
            <td style="width: 44201px; height: 19px">
            </td>
        </tr>
        <tr>
            <td style="width: 18px; height: 62px;">
            </td>
            <td colspan="4" style="height: 62px">
                <br />
                <asp:Button id="btnSave" runat="server" Text="Save" Width="50px" />
                <asp:Button id="btnCancel"
                    runat="server" Text="Cancel" /><br />
                <br />
            </td>
        </tr>
    </table>
</asp:Content>

