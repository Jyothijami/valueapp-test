<%@ Page Language="C#" MasterPageFile="~/MPs/InventoryMP1.master" AutoEventWireup="true" CodeFile="StockStatement.aspx.cs"
     Inherits="Modules_Inventory_StockStatement" Title="||Value App ::Stock Statement:: ||" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td style="text-align:left">
                Stock Statement Details</td>
            
            </tr>
        </table>
    
    <table width="100%">
        <tr>
            <td colspan="3" style="text-align: left" class="profilehead">
                Stock Statement
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: center">
                <table>
                    <tr>
                        <td style="text-align: right" class="auto-style1">
                            <asp:Label id="Label1" runat="server" Text="Brand :"></asp:Label></td>
                        <td style="text-align: left" class="auto-style1">
                            <asp:DropDownList id="ddlBrand" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="Please Select Brand " InitialValue="0" ValidationGroup="a" ControlToValidate="ddlBrand">*</asp:RequiredFieldValidator>
                        </td>
                        <td class="auto-style1">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px; text-align: right">
                            <asp:Label id="Label5" runat="server" Text="Category :"></asp:Label></td>
                        <td style="width: 100px; text-align: left">
                            <asp:DropDownList id="ddlCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                            </asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Category" InitialValue="0" ValidationGroup="Search">*</asp:RequiredFieldValidator>--%>
                        </td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px; text-align: right">
                            <asp:Label id="Label4" runat="server" Text="SubCategory :"></asp:Label></td>
                        <td style="width: 100px; text-align: left">
                            <asp:DropDownList id="ddlSubcategory" runat="server" OnSelectedIndexChanged="ddlSubcategory_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select SubCategory" InitialValue="0" ValidationGroup="Search">*</asp:RequiredFieldValidator>--%>
                        </td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px; text-align: right">
                            <asp:Label id="Label39" runat="server" Text="Search :" Width="57px"></asp:Label></td>
                        <td style="width: 100px; text-align: left">
                            <asp:TextBox id="txtSearchModel" runat="server">
                        </asp:TextBox></td>
                        <td style="width: 100px; text-align: left">
                            <asp:Button id="btnSearchModelNo" runat="server" BorderStyle="None" CausesValidation="False"
                                CssClass="gobutton" EnableTheming="False" onclick="btnSearchModelNo_Click1" Text="Go"
                                ValidationGroup="Search" /></td>
                    </tr>
                    <tr>
                        <td style="width: 100px; text-align: right">
                            <asp:Label id="Label2" runat="server" Text="Model No :"></asp:Label></td>
                        <td style="width: 100px; text-align: left">
                            <asp:DropDownList id="ddlModelNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModelNo_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select Model No." InitialValue="0" ValidationGroup="a" ControlToValidate="ddlModelNo">*</asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 100px">
                            <asp:SqlDataSource id="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                <selectparameters>
<asp:ControlParameter PropertyName="SelectedValue" Type="Int64" DefaultValue="0" Name="BranbId" ControlID="ddlBrand"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtSearchModel"></asp:ControlParameter>
</selectparameters>
                            </asp:SqlDataSource></td>
                    </tr>
                    <tr>
                        <td style="width: 100px; text-align: right">
                            <asp:Label id="Label3" runat="server" Text="Color :"></asp:Label></td>
                        <td style="width: 100px; text-align: left">
                            <asp:DropDownList id="ddlColor" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px; text-align: right">
                        </td>
                        <td style="width: 100px; text-align: left">
                            <asp:Button id="Button1" runat="server" Text="Go" OnClick="Button1_Click" Width="79px" ValidationGroup="a" /></td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                </table>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:GridView id="gvItemsMasterDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="sdsItemMasterDetails" OnRowDataBound="gvItemsMasterDetails_RowDataBound"
                    ShowFooter="True" Width="100%">
                    <footerstyle backcolor="#1AA8BE" font-bold="True" />
                    <columns>
<asp:BoundField DataField="ITEM_CODE" HeaderText="ItemCode">
<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="ModelNo"></asp:BoundField>
<asp:BoundField DataField="ITEM_NAME" HeaderText="SeriesName"></asp:BoundField>
<asp:TemplateField HeaderText="ModelNo"><EditItemTemplate>
&nbsp;
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
&nbsp;
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="ITEM_CATEGORY_NAME" HeaderText="CategoryName">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="IT_TYPE" HeaderText="SubCategory">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="PRODUCT_COMPANY_NAME" HeaderText="Brand">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ITEM_QTY_IN_HAND" HeaderText="Qty"></asp:BoundField>
<asp:BoundField DataField="ITEM_RES_QTY" HeaderText="ResQty"></asp:BoundField>
<asp:BoundField DataField="CP_ID" HeaderText="Cp_Id">
<HeaderStyle Width="0px"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="BRAND_ID"></asp:BoundField>
<asp:BoundField DataField="GODOWN_ID"></asp:BoundField>
<asp:BoundField DataField="COLOUR_ID"></asp:BoundField>
<asp:BoundField DataField="COLOUR_NAME" HeaderText="Colour"></asp:BoundField>
<asp:BoundField DataField="GODOWN_NAME" HeaderText="Godown"></asp:BoundField>
<asp:BoundField DataField="CP_FULL_NAME" HeaderText="Company"></asp:BoundField>
<asp:BoundField DataField="ITEM_QTY_ID" HeaderText="Qty Id"></asp:BoundField>
</columns>
                    <selectedrowstyle backcolor="LightSteelBlue" />
                </asp:GridView>
                <asp:SqlDataSource id="sdsItemMasterDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_MASTER_Statementofaccount" SelectCommandType="StoredProcedure">
                    <selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="ColorId" ControlID="lblColorid"></asp:ControlParameter>
</selectparameters>
                </asp:SqlDataSource><br />
                <asp:GridView id="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound"
                    ShowFooter="True" Width="100%">
                    <footerstyle backcolor="#1AA8BE" font-bold="True" />
                    <columns>
<asp:BoundField DataField="ITEM_CODE" HeaderText="ItemCode">
<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="ModelNo"></asp:BoundField>
<asp:BoundField DataField="ITEM_NAME" HeaderText="SeriesName"></asp:BoundField>
<asp:TemplateField HeaderText="ModelNo"><EditItemTemplate>
&nbsp;
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
&nbsp; 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="ITEM_CATEGORY_NAME" HeaderText="CategoryName">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="IT_TYPE" HeaderText="SubCategory">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="PRODUCT_COMPANY_NAME" HeaderText="Brand">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ITEM_QTY_IN_HAND" HeaderText="Qty"></asp:BoundField>
<asp:BoundField DataField="ITEM_RES_QTY" HeaderText="ResQty"></asp:BoundField>
<asp:BoundField DataField="CP_ID" HeaderText="Cp_Id">
<HeaderStyle Width="0px"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="BRAND_ID"></asp:BoundField>
<asp:BoundField DataField="GODOWN_ID"></asp:BoundField>
<asp:BoundField DataField="COLOUR_ID"></asp:BoundField>
<asp:BoundField DataField="COLOUR_NAME" HeaderText="Colour"></asp:BoundField>
<asp:BoundField DataField="GODOWN_NAME" HeaderText="Godown"></asp:BoundField>
<asp:BoundField DataField="CP_FULL_NAME" HeaderText="Company"></asp:BoundField>
<asp:BoundField DataField="ITEM_QTY_ID" HeaderText="Qty Id"></asp:BoundField>
</columns>
                    <emptydatatemplate>
<SPAN style="COLOR: #ff0000">No Data Exist</SPAN>
</emptydatatemplate>
                    <selectedrowstyle backcolor="LightSteelBlue" />
                </asp:GridView>
                <asp:Label id="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                    Visible="False"></asp:Label><asp:Label id="lblCPID" runat="server" Visible="False"></asp:Label><asp:Label
                        id="lblSearchItemHidden" runat="server" Visible="False"></asp:Label><asp:Label id="lblSearchValueHidden"
                            runat="server" Visible="False"></asp:Label>
                <asp:Label id="lblColorid" runat="server" Visible="False"></asp:Label>
                <asp:Label id="lblSubCategory" runat="server" Visible="False"></asp:Label>
                <asp:Label id="lblBrand" runat="server" Visible="False"></asp:Label>
                <asp:Label id="lblCategory" runat="server" Visible="False"></asp:Label></td>
        </tr>
        <tr><td>
            <asp:ValidationSummary ID="vs1" runat="server" ShowMessageBox="True" ShowSummary="False" />
            <asp:ValidationSummary ID="vs2" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="a" />
            <asp:ValidationSummary ID="vs3" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Search" />
            </td></tr>
    </table>
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            width: 100px;
            height: 52px;
        }
    </style>
</asp:Content>



 
