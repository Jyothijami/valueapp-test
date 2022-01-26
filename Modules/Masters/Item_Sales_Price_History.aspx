<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/AdminMP1.master" AutoEventWireup="true" CodeFile="Item_Sales_Price_History.aspx.cs" Inherits="Modules_Masters_Item_Sales_Price_History" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div>
        <table class="pagehead">
            <tr>
                <td style="text-align: left">Item Sales Price History
                </td>
                <td>
                    <asp:HyperLink ID="HyperLink6" runat="server" CssClass="leftmenu" Target="_blank" NavigateUrl="~/dev_Pages/ExcelPriceUodate.aspx">Upload Excel for Prices</asp:HyperLink>

                </td>
            </tr>
        </table>
    </div>
    <div>
        <table class="stacktable">
            <tr>
                <td colspan="5" style="text-align: left" class="profilehead">Update Item Sales Price :</td>
            </tr>
            <tr>
                <td colspan="5">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="5">Search For ModelNo:<asp:TextBox ID="txtSearchModel" runat="server">  </asp:TextBox>
                    <asp:Button ID="btnSearchModelNo" runat="server" BorderStyle="None" CausesValidation="False" CssClass="gobutton" EnableTheming="False" OnClick="btnSearchModelNo_Click" Text="Go" />
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                        SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:Parameter Type="Int64" DefaultValue="0" Name="BranbId"></asp:Parameter>
                            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtSearchModel"></asp:ControlParameter>
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td colspan="5">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:Label ID="Label15" runat="server" Text="Brand :"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="width: 5%"></td>
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
                <td style="width: 5%"></td>

                <td style="text-align: right">
                    <asp:Label ID="Label36" runat="server" Text="Model No :"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlModelNo" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="5">
                    <asp:Label ID="Label31" runat="server" Text="Percentage :"></asp:Label>
                    <asp:TextBox ID="txtPercentage" runat="server"></asp:TextBox>%&nbsp;&nbsp; (OR)&nbsp;&nbsp;&nbsp;&nbsp;
                   
                    <asp:Label ID="lblAmonut" runat="server" Text="Amount :"></asp:Label>

                    <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>

                </td>
            </tr>
            <tr>

                <td colspan="5" style="text-align: left">&nbsp;</td>

            </tr>
            <tr>
                <td colspan="5" style="text-align: center">
                    <asp:Button ID="btnBrandUpdate" runat="server" OnClick="btnBrandUpdate_Click" Text="Update" />
                </td>
            </tr>
            <%-- <tr>
                <td style="text-align: right">Model No :</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtModelNo" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Go" OnClick="btnSearch_Click"/>
                </td>
            </tr>--%>
            <tr>
                <td style="text-align: left" colspan="2">Model No :<asp:TextBox ID="txtModelNo" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Go" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" OnClick="btnSearch_Click" />

                </td>
                <td style="text-align: right" colspan="3">Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                    <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
                </td>

            </tr>
        </table>
    </div>
    <div>
        <table style="width: 100%">

            <tr>
                <td>

                    <asp:GridView ID="gvItemPriceHistory" runat="server" Width="100%" ShowFooter="true" AutoGenerateColumns="False" AllowSorting="false" AllowPaging="true" OnPageIndexChanging="gvItemPriceHistory_PageIndexChanging" OnRowDataBound="gvItemPriceHistory_RowDataBound">
                        <FooterStyle ForeColor="#0066ff" />
                        <Columns>
                            <asp:BoundField DataField="Item Code" HeaderText="Item Code" SortExpression="Item Code" />
                            <asp:BoundField DataField="Model No" HeaderText="Model No" SortExpression="Model No" />
                            <asp:BoundField DataField="Item Name" HeaderText="Item Name" SortExpression="Item Name" />
                            <asp:BoundField DataField="Color" HeaderText="Item Color" SortExpression="Color" />
                            <asp:BoundField DataField="Currency" HeaderText="Currency Type" SortExpression="Currency" />
                            <asp:BoundField DataField="Gross" HeaderText="Gross" SortExpression="Gross" />
                            <asp:BoundField DataField="Coefficient" HeaderText="%Coefficient" SortExpression="Coefficient" />
                            <asp:BoundField DataField="Factor" HeaderText="*Factor" SortExpression="Factor" />
                            <asp:BoundField DataField="Last Modified" HeaderText="Last Modified" ReadOnly="True" SortExpression="Last Modified" />
                            <asp:BoundField DataField="Current Price" HeaderText="Current Price" ReadOnly="True" SortExpression="Current Price" />
                            <asp:BoundField DataField="Last Change 1" HeaderText="Last Change 1" ReadOnly="True" SortExpression="Last Change 1" />
                            <asp:BoundField DataField="Last Change 2" HeaderText="Last Change 2" ReadOnly="True" SortExpression="Last Change 2" />
                            <asp:BoundField DataField="Last Change 3" HeaderText="Last Change 3" ReadOnly="True" SortExpression="Last Change 3" />
                            <asp:BoundField DataField="Last Change 4" HeaderText="Last Change 4" ReadOnly="True" SortExpression="Last Change 4" />
                            <asp:BoundField DataField="Last Change 5" HeaderText="Last Change 5" ReadOnly="True" SortExpression="Last Change 5" />
                        </Columns>
                    </asp:GridView>

                    <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="USP_ItemPriceHistory" SelectCommandType="StoredProcedure"></asp:SqlDataSource>--%>

                </td>
            </tr>
        </table>
    </div>


</asp:Content>


 
