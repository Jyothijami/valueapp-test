<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/InventoryMP1.master" AutoEventWireup="true" CodeFile="Invoice_Search.aspx.cs" Inherits="Modules_Inventory_Invoice_Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
     <div id="Div1" style="width: 100%" runat="server">
            <table style="width: 100%">
                <tr>
                    <td class="profilehead" style="text-align: left">Inventory DC Details Search
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">Go To Page :
                                    <asp:TextBox ID="txtGo2" Width="100px" runat="server"></asp:TextBox>
                        <asp:Button ID="btnGo2" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnGo2_Click" />

                    </td>
                    <td style="text-align: right">
                        <asp:DropDownList ID="ddlNoOfRecord2" runat="server" OnSelectedIndexChanged="ddlNoOfRecord2_SelectedIndexChanged" AutoPostBack="True">
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
            <table>
                <tr>
                <td class="auto-style1">
                    &nbsp;
                </td>
                <td colspan="3" style="text-align:center" class="auto-style1">
                    <asp:RadioButtonList ID="rbBranchTransfer" runat="server" RepeatDirection="Horizontal" >
                        <asp:ListItem Value="0" Selected="True">Exclude BranchTransfer</asp:ListItem>
                        <asp:ListItem Value="1">Include BranchTransfer</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
                <tr>
                    <td style="text-align: right">Brand :
                    <%--<asp:DropDownList ID="ddlBrand" runat="server"></asp:DropDownList>--%>
                        <asp:TextBox ID="txtBrand2" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">Model No :

                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtModel2" runat="server"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">From Date :
                    <asp:TextBox ID="txtFromDate2" type="datepic" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">To Date : </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtToDate2" type="datepic" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">Invoice No :
                    <asp:TextBox ID="txtMRN2" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">DC No :</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtColor2" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">Category :
                    <asp:TextBox ID="txtCat2" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">Sub Category :</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtSubCat2" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">Tally Invoice No :
                    <asp:TextBox ID="txtRemark2" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">Company :</td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlCompany2" runat="server" DataSourceID="SqlDataSource2" DataTextField="COMP_NAME" DataValueField="CP_ID" AppendDataBoundItems="True">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT a.CP_ID, a.CP_FULL_NAME + ',' + b.locname AS COMP_NAME FROM YANTRA_COMP_PROFILE AS a INNER JOIN location_tbl AS b ON a.locid = b.locid"></asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">Customer Name :
                    <asp:TextBox ID="txtCust" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="text-align: left" colspan="2">
                        <asp:Button ID="btnSearch2" runat="server" Text="Search" OnClick="btnSearch2_Click" BackColor="#9966FF" EnableTheming="True" ForeColor="White" Width="18%" />
                        &nbsp;
                    &nbsp;
                    &nbsp;
                    <asp:Button ID="btnDCExport" runat="server" Text="Export To Excel" OnClick="btnDCExport_Click" BackColor="#9966FF" EnableTheming="True" ForeColor="White" Width="28%" />

                    </td>
                </tr>

            </table>
        </div>
     <table>
        <tr>
            <td>
                Total No Of Outward Quantity :
            </td>
            <td>
                <asp:Label ID="lblOutwardQty" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;
            </td>
        </tr>
    </table>
        <br />
        <div id="gridDC" style="width: 100%">
            <asp:GridView ID="gvDCDetails" Width="100%" runat="server" SelectedRowStyle-BackColor="#c0c0c0" EmptyDataText="No Records To Display" AllowPaging="true" OnPageIndexChanging="gvDCDetails_PageIndexChanging" PageSize="10" OnRowDataBound ="gvDCDetails_RowDataBound">
                <HeaderStyle HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="Silver"></SelectedRowStyle>
            </asp:GridView>
        </div>
</asp:Content>

