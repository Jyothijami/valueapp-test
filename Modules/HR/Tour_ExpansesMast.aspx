<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true" CodeFile="Tour_ExpansesMast.aspx.cs" Inherits="Modules_HR_Tour_ExpansesMast" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td class="profilehead" style="text-align: left" colspan="2">Tour Expanses
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
            <td style="width: 10%">Tour No :

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
                        <asp:BoundField DataField="TourId" HeaderText="SI.No." SortExpression="TourId">
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Voucher No">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ControlStyle Width="100px"></ControlStyle>
                            <ItemStyle Width="100px" HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle Width="100px" HorizontalAlign="Left"></HeaderStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnVoucherNo" OnClick="lbtnVoucherNo_Click" ForeColor="#0066ff" runat="server" Text='<%# Bind("TourId") %>' CausesValidation="False"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Tour_Date" HeaderText="Date" DataFormatString="{0:d}" SortExpression="Tour_Date">
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PlaceOfVisit" HeaderText="PlaceOfVisit" SortExpression="PlaceOfVist">
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
                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click"  />
                <asp:Button ID="btnEdit" runat="server" Visible="false" Text="Edit" OnClick="btnEdit_Click"  />
                <asp:Button ID="btnDelete" runat="server" Text="Delete"  />
                <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" />

            </td>
        </tr>
    </table>
</asp:Content>

