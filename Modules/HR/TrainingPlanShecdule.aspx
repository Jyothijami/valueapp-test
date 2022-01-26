<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TrainingPlanShecdule.aspx.cs" Inherits="Modules_HRManagement_TrainingPlanShecdule" Title="|| YANTRA : HR Management : Training Plan Schedule ||" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table class="pagehead">
        <tr>
            <td style="height: 12px" colspan="4">
                training plan schedule</td>
        </tr>
    </table>
    <br />
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">
                            Training Plan Schedule&nbsp; Details</td>
                        <td>
                        </td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td rowspan="3">
                                        <asp:Label id="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td rowspan="3">
                                        <asp:DropDownList id="ddlCurrentDayTaskSearchBy" runat="server" CssClass="textbox">
                                        </asp:DropDownList></td>
                                    <td rowspan="3">
                                        <asp:DropDownList id="ddlCurrentTasksSymbols" runat="server" AutoPostBack="True"
                                            CssClass="textbox" Visible="False" Width="50px">
                                            <asp:ListItem Selected="True">=</asp:ListItem>
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem>&gt;</asp:ListItem>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <asp:ListItem>&gt;=</asp:ListItem>
                                            <asp:ListItem>R</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td rowspan="3">
                                        <asp:Label id="lblCurrentFromDate" runat="server" CssClass="label" Font-Bold="True"
                                            ForeColor="White" Text="From" Visible="False"></asp:Label></td>
                                    <td rowspan="3">
                                        <asp:TextBox id="txtCurrentDayTasksFromDate" runat="server" CssClass="textbox" Visible="False">
                                        </asp:TextBox><asp:Image id="Image9" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image></td>
                                    <td rowspan="3">
                                        <asp:Label id="lblCurrentToDate" runat="server" CssClass="label" Font-Bold="True"
                                            ForeColor="White" Text="To " Visible="False"></asp:Label></td>
                                    <td rowspan="3">
                                        <asp:TextBox id="txtCurrentDayTaskSearchText" runat="server" CssClass="textbox">
                                        </asp:TextBox><asp:Image id="imgCurrentDayTasksToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image></td>
                                    <td rowspan="3">
                                        <asp:Button id="btnCurrentDayTasksGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" Text="Go" /></td>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: left">
                <table border="0" cellpadding="4" cellspacing="0" style="font-weight: normal; font-size: 8pt;
                    color: black; font-family: Verdana; text-align: left; text-decoration: none"
                    width="100%">
                    <tr>
                        <td style="background-color: #1aa8be">
                            <span style="color: #ffffff"><strong>Planning Type</strong></span></td>
                        <td style="color: #000000; background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Plan Month</span></strong></td>
                        <td style="color: #000000; background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Training Category</span></strong></td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                            Training</td>
                        <td style="background-color: #eff3fb">
                            January</td>
                        <td style="background-color: #eff3fb">
                            Temperory</td>
                    </tr>
                    <tr>
                        <td style="background-color: white">
                            Real Time</td>
                        <td style="background-color: white">
                            March</td>
                        <td style="background-color: white">
                            Permanent</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 21px; text-align: left">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 51px; text-align: center">
                <table id="Table1">
                    <tr>
                        <td>
                            <asp:Button id="btnNew" runat="server" Text="New" /></td>
                        <td>
                            <asp:Button id="btnEdit" runat="server" Text="Edit" /></td>
                        <td>
                            <asp:Button id="Button11" runat="server" Text="Delete" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; height: 20px;" colspan="4" class="profilehead">
                General Details</td>
        </tr>
        <tr>
            <td style="height: 19px; text-align: right">
            </td>
            <td style="height: 19px; text-align: left">
            </td>
            <td style="height: 19px; text-align: right">
            </td>
            <td style="height: 19px">
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label id="Label1" runat="server" Height="21px" Text="Planning Type" Width="109px"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList id="ddlPlanningType" runat="server">
                </asp:DropDownList></td>
            <td style="text-align: right">
                
                <asp:Label id="Label3" runat="server" Text="Plan Month" Width="89px"></asp:Label>
                
            </td>
            <td style="text-align: left;">
              
                <asp:TextBox id="txtMonth" runat="server" Width="87px"></asp:TextBox>
                <asp:Image id="Image2" runat="server" ImageUrl="~/Images/Calendar.png">
                </asp:Image></td>
        </tr>
        <tr>
            <td style="height: 26px; text-align: right;">
                <asp:Label id="Label2" runat="server" Text="Training  Category" Width="136px"></asp:Label></td>
            <td style="height: 26px; text-align: left;">
                <asp:DropDownList id="ddlCategory" runat="server">
                </asp:DropDownList></td>
            <td style="height: 26px; text-align: left;">
                <asp:Button id="btnGo" runat="server" Text="Go" BackColor="Transparent" BorderStyle="None" CssClass="gobutton" EnableTheming="False" /></td>
            <td style="height: 26px; text-align: left;">
                </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td style="text-align: left;">
                </td>
        </tr>
        <tr>
            <td style="text-align: left;" colspan="4" class="profilehead">
                Reference Details</td>
        </tr>
        <tr>
            <td style="height: 12px">
            </td>
            <td style="height: 12px">
            </td>
            <td style="height: 12px">
            </td>
            <td style="height: 12px;">
            </td>
        </tr>
        <tr>
            <td style="height: 12px; text-align: right">
                &nbsp;<asp:Label id="Label5" runat="server" Text="Approved By"></asp:Label></td>
            <td style="height: 12px; text-align: left;">
                <asp:DropDownList id="ddlApprovedBy" runat="server">
                </asp:DropDownList></td>
            <td style="height: 12px; text-align: right;">
                <asp:Label id="Label6" runat="server" Text="Prepared By" Width="132px"></asp:Label></td>
            <td style="height: 12px; text-align: left;">
                <asp:DropDownList id="ddlPreparedBy" runat="server">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="height: 12px">
            </td>
            <td style="height: 12px">
            </td>
            <td style="height: 12px">
            </td>
            <td style="height: 12px">
            </td>
        </tr>
        <tr>
            <td style="height: 12px" colspan="4">
                <asp:Button id="btnSave" runat="server" Text="Save" Width="59px" />
                <asp:Button id="btnCancel"
                    runat="server" Text="Cancel" /><br />
            </td>
        </tr>
        <tr>
            <td style="height: 12px">
            </td>
            <td style="height: 12px">
            </td>
            <td style="height: 12px">
            </td>
            <td style="height: 12px">
            </td>
        </tr>
    </table>
</asp:Content>

