<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/AdminMP1.master" AutoEventWireup="true" CodeFile="Item_Price_History.aspx.cs" Inherits="Modules_Masters_Item_Price_History" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div>
        <table class="pagehead">
            <tr>
                <td style="text-align: left">Item Price History
                </td>
                 <td style="text-align: right" align="right">
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
    </div>
    <br />
    <div>
        <table style="width:100%">

            <tr>
                <td>
                    <asp:GridView ID="gvItemPriceHistory" runat="server" Width="100%" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" >
                        <Columns>
                            <asp:BoundField DataField="Item Code" HeaderText="Item Code" SortExpression="Item Code" />
                            <asp:BoundField DataField="Item Name" HeaderText="Item Name" SortExpression="Item Name" />
                            <asp:BoundField DataField="Last Modified" HeaderText="Last Modified" ReadOnly="True" SortExpression="Last Modified" />
                            <asp:BoundField DataField="Current Price" HeaderText="Current Price" ReadOnly="True" SortExpression="Current Price" />
                            <asp:BoundField DataField="Last Change 1" HeaderText="Last Change 1" ReadOnly="True" SortExpression="Last Change 1" />
                            <asp:BoundField DataField="Last Change 2" HeaderText="Last Change 2" ReadOnly="True" SortExpression="Last Change 2" />
                            <asp:BoundField DataField="Last Change 3" HeaderText="Last Change 3" ReadOnly="True" SortExpression="Last Change 3" />
                            <asp:BoundField DataField="Last Change 4" HeaderText="Last Change 4" ReadOnly="True" SortExpression="Last Change 4" />
                            <asp:BoundField DataField="Last Change 5" HeaderText="Last Change 5" ReadOnly="True" SortExpression="Last Change 5" />
                        </Columns>
                    </asp:GridView>

                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="USP_ItemPriceHistory" SelectCommandType="StoredProcedure"></asp:SqlDataSource>

                </td>
            </tr>
        </table>
    </div>
    <div>
        <table class="stacktable">
            <tr>
                <td colspan="2" style="text-align: left" class="profilehead">Price Update By Brand</td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:Label ID="Label15" runat="server" Text="Brand :"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:Label ID="Label34" runat="server" Text="Category :"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:Label ID="Label35" runat="server" Text="Sub Category :"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlSubCategory" runat="server" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:Label ID="Label36" runat="server" Text="Model No :"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlModelNo" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:Label ID="Label31" runat="server" Text="Percentage :"></asp:Label></td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtPercentage" runat="server"></asp:TextBox>%
                                       
                </td>

            </tr>
            <tr>
                <td>
                   
                </td>
                <td>
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    (OR)

                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:Label ID="lblAmonut" runat="server" Text="Amount :"></asp:Label></td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>

                </td>

            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="btnBrandUpdate" runat="server" OnClick="btnBrandUpdate_Click" Text="Update" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>


 
