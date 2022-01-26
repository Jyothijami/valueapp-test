<%@ Page Language="C#" MasterPageFile="~/MPs/InventoryMP1.master" AutoEventWireup="true" 
    CodeFile="ReserveStockHistory.aspx.cs" Inherits="Modules_Inventory_ReserveStockHistory" Title="|| Value App : Inventory : Reserve Stock History ||" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="pagehead">
        <tr>
            <td class="pagehead" align="right" style="text-align: left;">
                Reserve Stock History
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
    <table width="100%" style="text-align: right">
        
        <tr class="searchhead">
            <td style="text-align:left">
                 Reserve Stock History
            </td>
            <td  colspan="2" style="text-align: right">
               
                <asp:Label id="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                    Text="Search By"></asp:Label>
                <asp:DropDownList id="ddlSearchBy" runat="server" AutoPostBack="True"
                        CssClass="textbox" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged"><asp:ListItem
                            Value="0">--</asp:ListItem>
                        <asp:ListItem Value="ITEM_MODEL_NO">Model No</asp:ListItem>
                        <asp:ListItem Value="CUST_NAME">Customer Name</asp:ListItem>
                    </asp:DropDownList>
                <asp:TextBox id="txtSearchText" runat="server" CssClass="textbox">
                                    </asp:TextBox>
                <%--<asp:Button id="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False" CssClass="gobutton" EnableTheming="False" onclick="btnSearchGo_Click" Text="Go" />--%>
                <asp:Button ID="btnSearchGo" runat="server" Text="Go"  CausesValidation="false" CssClass="gobutton" EnableTheming="false" OnClick="btnSearchGo_Click"/>
                <br />
                <asp:Label id="lblSearchItemHidden" runat="server" Visible="False"></asp:Label><asp:Label
                    id="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: center">
                <asp:GridView id="gvStockHistory" runat="server" AllowPaging="True" Width="100%" AutoGenerateColumns="False"
                    DataKeyNames="RESERVE_ID,COLOUR_ID1,ITEM_CODE1" DataSourceID="SqlDataSource"
                    AllowSorting="true" OnRowDataBound="gvStockHistory_RowDataBound">
                    <columns>
<asp:BoundField DataField="CUST_NAME" SortExpression="CUST_NAME" HeaderText="Customer"></asp:BoundField>
<asp:BoundField DataField="SO_CONTACT_PHONE1" SortExpression="SO_CONTACT_PHONE1" HeaderText="Phone No"></asp:BoundField>
<asp:BoundField DataField="ITEM_MODEL_NO" SortExpression="ITEM_MODEL_NO" HeaderText="Model No"></asp:BoundField>
<asp:BoundField DataField="ITEM_NAME" SortExpression="ITEM_NAME" HeaderText="Item Name"></asp:BoundField>
<asp:BoundField DataField="PRODUCT_COMPANY_NAME" SortExpression="PRODUCT_COMPANY_NAME" HeaderText="Brand"></asp:BoundField>
<asp:BoundField DataField="COLOUR_NAME" SortExpression="COLOUR_NAME" HeaderText="Color"></asp:BoundField>
<asp:BoundField DataField="RES_QTY" SortExpression="RES_QTY" HeaderText="Res Qty"></asp:BoundField>
<asp:BoundField DataField="GODOWN_NAME" SortExpression="GODOWN_NAME" HeaderText="Godown"></asp:BoundField>
</columns>
                    <emptydatatemplate>
<SPAN style="COLOR: #ff0000">No Data to Display</SPAN>  
</emptydatatemplate>
                </asp:GridView>
                <asp:SqlDataSource id="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_Inventory_reserveHistory" SelectCommandType="StoredProcedure"><selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
</selectparameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td style="width: 25px">
            </td>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
        </tr>
    </table>
</asp:Content>


 
