<%@ Page Language="C#" MasterPageFile="~/MPs/PurchaseMP1.master" AutoEventWireup="true"
     CodeFile="SelfPurchaseOrderq.aspx.cs" Inherits="Modules_SCM_SelfPurchaseOrderq" Title="|| Value App : Purchasing Management : Self Purchase Order Search||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    &nbsp;
    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="pagehead">
        <tr>
            <td class="pagehead" align="right" style="text-align: left;">Purchase Order Search
            </td>
            <td>
                <asp:DropDownList ID="ddlNoOfRecords" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged">
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>75</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>

                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="text-align: right;" class="searchhead" id="TD9">

                <table style="width:100%" align="right">
                    <tr>
                        <td style="text-align: left;">&nbsp;</td>
                        <td colspan="4"></td>
                        <td style="text-align: right;">
                            <table border="0" cellpadding="0" cellspacing="0" align="right"><tr><td>
                            <asp:Label ID="Label2" runat="server" CssClass="label" Text="Search By"></asp:Label></td>
                        <td >
                            <asp:DropDownList ID="ddlSearchBy" runat="server">
                                <asp:ListItem Value="0">------</asp:ListItem>
                                <asp:ListItem>Model No</asp:ListItem>
                                <asp:ListItem>Customer</asp:ListItem>
                                <asp:ListItem>PO No</asp:ListItem>
                                <asp:ListItem>Status</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left;" >
                            <asp:TextBox ID="txtSearch" runat="server">
                            </asp:TextBox>
                        </td>
                        <td style="text-align: left;">
                            <%--<asp:Button ID="btnSearch" runat="server" CssClass="gobutton" OnClick="btnSearch_Click" Text="GO" Width="80px" />--%>
                            <asp:Button ID="btnSearch" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" OnClick="btnSearch_Click" Text="Go" />
                            </td></tr></table>
                        </td>
                        
                    </tr>
                </table>
            </td>

        </tr>
        <tr>
            <td colspan="3">
                <asp:GridView ID="gvPOOrdersearch" runat="server" AutoGenerateColumns="False" OnRowEditing="gvPOOrdersearch_RowEditing"
                    OnRowUpdating="gvPOOrdersearch_RowUpdating" HorizontalAlign="Left" OnRowCancelingEdit="gvPOOrdersearch_RowCancelingEdit"
                    OnRowDataBound="gvPOOrdersearch_RowDataBound" Width="100%">
                    <Columns>
                        <asp:CommandField ShowEditButton="True"></asp:CommandField>
                        <asp:BoundField DataField="FPO_NO" SortExpression="FPO_NO" HeaderText="Order No">
                        <ControlStyle Width="10%" />
                        <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ITEM_MODEL_NO" SortExpression="ITEM_MODEL_NO" HeaderText="Model No"></asp:BoundField>
                        <asp:BoundField DataField="ITEM_NAME" SortExpression="ITEM_NAME" HeaderText="Item Name"></asp:BoundField>
                        <asp:BoundField DataField="IT_TYPE" SortExpression="IT_TYPE" HeaderText="SubCategory"></asp:BoundField>
                        <asp:BoundField DataField="FPO_DET_COLOR" SortExpression="FPO_DET_COLOR" HeaderText="Color"></asp:BoundField>
                        <asp:BoundField DataField="FPO_DET_QTY" SortExpression="FPO_DET_QTY" HeaderText="Qty"></asp:BoundField>
                        <asp:TemplateField SortExpression="FPO_DET_DELIVERY_DATE" HeaderText="Exp Date of Delivery">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtExpDate" runat="server" Text='<%# Bind("FPO_DET_DELIVERY_DATE") %>' __designer:wfdid="w10"></asp:TextBox><asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False" __designer:dtid="281474976710720" __designer:wfdid="w11"></asp:Image><cc1:CalendarExtender ID="CalendarExtender2" runat="server" __designer:dtid="281474976710721" __designer:wfdid="w12" TargetControlID="txtExpDate" PopupButtonID="Image2" Format="MM/dd/yyyy" Enabled="True"></cc1:CalendarExtender>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" __designer:dtid="281474976710722" __designer:wfdid="w13" TargetControlID="txtExpDate" Enabled="True" UserDateFormat="MonthDayYear" MaskType="Date" Mask="99/99/9999" DisplayMoney="Left"></cc1:MaskedEditExtender>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("FPO_DET_DELIVERY_DATE", "{0:dd/MM/yyyy}") %>' __designer:wfdid="w9"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="FPO_DET_EXPDATE" HeaderText="Arrived Date">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtArrivedDate" runat="server" Text='<%# Bind("FPO_DET_EXPDATE") %>' __designer:wfdid="w24"></asp:TextBox>
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False" __designer:dtid="281474976710720" __designer:wfdid="w25"></asp:Image><cc1:CalendarExtender ID="CalendarExtender1" runat="server" __designer:dtid="281474976710721" __designer:wfdid="w26" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="Image1" TargetControlID="txtArrivedDate"></cc1:CalendarExtender>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" __designer:dtid="281474976710722" __designer:wfdid="w27" Enabled="True" TargetControlID="txtArrivedDate" DisplayMoney="Left" Mask="99/99/9999" MaskType="Date" UserDateFormat="MonthDayYear"></cc1:MaskedEditExtender>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("FPO_DET_EXPDATE", "{0:dd/MM/yyyy}") %>' __designer:wfdid="w23"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="FPO_DET_CUSTOMER" SortExpression="FPO_DET_CUSTOMER" HeaderText="Customer"></asp:BoundField>
                        <asp:TemplateField HeaderText="Status">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlStatus" runat="server" __designer:wfdid="w3">
                                    <asp:ListItem>Pending</asp:ListItem>
                                    <asp:ListItem>Received</asp:ListItem>
                                    <asp:ListItem>Close</asp:ListItem>
                                </asp:DropDownList>&nbsp; 
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("FPO_DET_STATUS") %>' __designer:wfdid="w2"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="FPOS_DET_ID" HeaderText="Id">
                            <EditItemTemplate>
                                <asp:Label ID="lblId" runat="server" Text='<%# Bind("FPOS_DET_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblId" runat="server" Text='<%# Bind("FPOS_DET_ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle ForeColor="Transparent" Width="50%" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    </asp:Content>



 
