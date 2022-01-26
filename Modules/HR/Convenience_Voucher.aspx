<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true" CodeFile="Convenience_Voucher.aspx.cs" Inherits="Modules_HR_Convenience_Voucher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <%--<asp:UpdatePanel runat="server">
        <ContentTemplate>--%>
    <table style="width: 100%">
        <tr>
            <td class="profilehead" style="text-align: left" colspan="2">Conveyance Voucher
            </td>

        </tr>
        <tr>
            <td style="text-align: left">Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />

            </td>
            <td style="text-align: right">No Of Records :
                <asp:DropDownList ID="ddlNoOfRecords" runat="server" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>75</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    <table style="width: 100%">
        <tr style="text-align: center">
            <td>Employee Name :
                <asp:TextBox ID="txtEmployeeName" runat="server"></asp:TextBox>

            </td>
            <td style="width: 5%"></td>
            <td style="width: 10%">Voucher No :

            </td>
            <td style="text-align: left">

                <asp:TextBox ID="txtVoucher" runat="server"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="5" style="text-align: center">
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
            </td>

        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr style="width: 100%">
            <td colspan="5">
                <asp:GridView ID="gvConvenienceVoucher" runat="server" AutoGenerateColumns="False" SelectedRowStyle-BackColor="#c0c0c0" Width="100%" EmptyDataText="No Records Found" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvConvenienceVoucher_PageIndexChanging">
                    <FooterStyle BackColor="#1AA8BE" />
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="SI.No." SortExpression="Id">
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Voucher No">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ControlStyle Width="100px"></ControlStyle>
                            <ItemStyle Width="100px" HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle Width="100px" HorizontalAlign="Left"></HeaderStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnVoucherNo" OnClick="lbtnVoucherNo_Click" ForeColor="#0066ff" runat="server" Text='<%# Bind("Voucher_No") %>' CausesValidation="False"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:d}" SortExpression="Date">
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Exe_Name" HeaderText="Execute Name" SortExpression="Exe_Name">
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Employee Name" SortExpression="EMP_FIRST_NAME">
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>

                </asp:GridView>
                <asp:Label ID="lblCPID" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblEmpIdHidden" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblUserType" runat="server" Visible="false"></asp:Label>
            </td>
        </tr>
    </table>
    <br />

    <table style="width: 100%">
        <tr style="text-align: center">
            <td>
                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                <asp:Button ID="btnEdit" runat="server" Visible="false" Text="Edit" OnClick="btnEdit_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
            </td>
        </tr>
    </table>
    <br />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div id="tblDetails" runat="server" width="100%" visible="false">
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: right">Voucher No :
                        <asp:TextBox ID="txtVoucherNo" Enabled="false" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 5%"></td>
                        <td style="text-align: right">Date : 
                        </td>
                        <td style="text-align: left">

                            <asp:TextBox ID="txtDate" type="datepic" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Executive Name :
                        <asp:TextBox ID="txtExeName" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 5%"></td>
                        <td style="text-align: right">&nbsp;
                        </td>
                        <td style="text-align: left">
                            <asp:Label runat="server" ID="Label1" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">&nbsp;</td>
                    </tr>

                    <tr>
                        <td colspan="5" style="text-align: left; height: 20px;">
                            <h3>Voucher Details : </h3>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Site Name :
                        <asp:TextBox ID="txtSiteName" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 5%"></td>
                        <td style="text-align: right">Purpose : 
                        </td>
                        <td style="text-align: left">

                            <asp:TextBox ID="txtPurpose" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">From :
                        <asp:TextBox ID="txtFromLoc" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 5%"></td>
                        <td style="text-align: right">To : 
                        </td>
                        <td style="text-align: left">

                            <asp:TextBox ID="txtToLoc" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">From Time :
                        <asp:TextBox ID="txtFromTime" runat="server"></asp:TextBox>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" DisplayMoney="Left"
                                Mask="99:99" AcceptAMPM="true" ClearMaskOnLostFocus="false" AutoComplete="false" MaskType="Time" TargetControlID="txtFromTime" UserDateFormat="None" Enabled="True">
                            </cc1:MaskedEditExtender>
                        </td>
                        <td style="width: 5%"></td>
                        <td style="text-align: right">To Time : 
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtToTime" runat="server"></asp:TextBox>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" DisplayMoney="Left"
                                Mask="99:99" AcceptAMPM="true" ClearMaskOnLostFocus="false" AutoComplete="false" MaskType="Time" TargetControlID="txtToTime" UserDateFormat="None" Enabled="True">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">KMs :
                        <asp:TextBox ID="txtKMs" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 5%"></td>
                        <td style="text-align: right;">On Date : </td>
                        <td>
                            <asp:TextBox ID="txtOnDate" runat="server" type="datepic"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="5" style="text-align: center">
                            <asp:Button ID="btnAddDet" runat="server" Text="Add" OnClick="btnAddDet_Click" />
                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" />
                            <asp:Label ID="lblTotalKMs" runat="server" Text="" Visible="False"></asp:Label>
                        </td>
                    </tr>

                </table>

                <br />
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:GridView ID="gvVoucherDetails" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDeleting="gvVoucherDetails_RowDeleting" ShowFooter="true" OnRowDataBound="gvVoucherDetails_RowDataBound">
                                <Columns>
                                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                    <asp:BoundField DataField="Site_Name" HeaderText="Site Name" />
                                    <asp:BoundField DataField="Purpose" HeaderText="Purpose" />
                                    <asp:BoundField DataField="From_Loc_Id" HeaderText="From" />
                                    <asp:BoundField DataField="To_Loc_Id" HeaderText="To" />
                                    <asp:BoundField DataField="From_Time" HeaderText="From Time" />
                                    <asp:BoundField DataField="To_Time" HeaderText="To Time" />
                                    <asp:BoundField DataField="KMs" HeaderText="KMs" />
                                    <asp:BoundField DataField="On_Date" HeaderText="On Date" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:Label ID="Label10" runat="server" Text="Prepared By :"></asp:Label>
                            <asp:DropDownList ID="ddlPreparedBy" runat="server" Style="margin-left: 0px" Enabled="False">
                            </asp:DropDownList>
                            <asp:Label ID="lblVoucherID" Visible="false" runat="server"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="5">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="5" style="text-align: center">
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                            <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" />

                            <asp:Button ID="btnMainRefresh" runat="server" Text="Refresh" OnClick="btnMainRefresh_Click" />

                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

