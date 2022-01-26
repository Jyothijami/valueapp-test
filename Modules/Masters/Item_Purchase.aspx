<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Item_Purchase.aspx.cs" Inherits="Modules_Masters_Item_Purchase" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 22px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    
    <div>
        <table class="pagehead">
            <tr>
                <td style="text-align: left">Item Purchase Price
                </td>
            </tr>
        </table>
    </div> 
    
    <table width="100%">
 
        <tr  style="text-align: right">
            <td style="text-align: left" colspan="2">
                                    Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
                                </td>

            <td style="text-align: right" colspan="2">
                <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label>
               
                <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="textbox" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                    <asp:ListItem Value="0">--</asp:ListItem>
                    <asp:ListItem Value="ITEM_NAME">Item Name</asp:ListItem>
                    <asp:ListItem Value="ITEM_MODEL_NO">Model No</asp:ListItem>

                    <asp:ListItem Value="PRODUCT_COMPANY_NAME">Brand</asp:ListItem>

                </asp:DropDownList>
                <asp:TextBox ID="txtSearchText" runat="server" CssClass="textbox" Width="111px"></asp:TextBox>
                <asp:Button ID="btnSearchGo" runat="server" CausesValidation="False" Text="Go" OnClick="btnSearchGo_Click" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center" colspan="4">
                <asp:GridView ID="gvItemPriceUpdate" runat="server" ShowFooter="true" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" OnRowDataBound="gvItemPriceUpdate_RowDataBound" Width="100%" AllowPaging="True">
                    <FooterStyle ForeColor="#0066ff" />
                    <Columns>
                        <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
                        <asp:BoundField DataField="IC_ID" HeaderText="IC_ID" SortExpression="IC_ID" />
                        <asp:BoundField DataField="BRAND_ID" HeaderText="BRAND_ID" SortExpression="BRAND_ID" />
                        <asp:BoundField DataField="PRODUCT_COMPANY_NAME" HeaderText="Brand" />
                        <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="Model No" />
                        <asp:BoundField DataField="ITEM_NAME" HeaderText="Item Name" />
                        <asp:TemplateField HeaderText="Currency">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlCurrency" runat="server">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="International Price">
                            <ItemTemplate>
                                <asp:TextBox ID="txtIP" runat="server" ></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RPP">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRpp" runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="IT_TYPE_ID" HeaderText="SubCat" />
                    </Columns>
                    <EmptyDataTemplate>
                        <span class="auto-style1"><strong>No Items found to be Updated</strong></span>
                    </EmptyDataTemplate>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_MASTER_ITEM_PRICE_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue" PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td style="text-align: right" class="auto-style1">
            </td>
            <td class="auto-style1">
                </td>
            <td style="text-align: right" class="auto-style1"></td>
            <td class="auto-style1"></td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: right">
                <table align="center" >
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                   &nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>


 
