<%@ Page Language="C#" MasterPageFile="~/MPs/InventoryMP1.master" AutoEventWireup="true" CodeFile="ItemMaster.aspx.cs"
     Inherits="Modules_SCM_ItemMaster" Title="|| Value Appp : Purchasing  : Stock Entry ||" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
   <table border="0" cellpadding="0" cellspacing="0" width="100%" class="pagehead">
        <tr>
            <td class="pagehead" align="right" style="text-align: left;">
                Stock Entry
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
    <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left; width: 272px; height: 41px;">
                            Stock Entry&nbsp; Master</td>
                        <td style="text-align: right; height: 41px;">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td>
                                        <asp:Label id="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By" Width="86px"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList id="ddlSearchBy" runat="server" CssClass="textbox">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="ITEM_MODEL_NO">Model No</asp:ListItem>
                                            <asp:ListItem Value="ITEM_CATEGORY_NAME">Category Name</asp:ListItem>
                                            <asp:ListItem Value="IT_TYPE_ID">Sub Category Name</asp:ListItem>
                                            <asp:ListItem Value="PRODUCT_COMPANY_NAME">Brand</asp:ListItem>
                                            <asp:ListItem Value="CP_FULL_NAME">Company Name</asp:ListItem>
                                            <asp:ListItem Value="GODOWN_NAME">GoDown</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:TextBox id="txtSearchText" runat="server" CssClass="textbox">
                                        </asp:TextBox><asp:Image id="imgCurrentDayTasksToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image></td>
                                    <td>
                                        <asp:Button id="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" onclick="btnSearchGo_Click" Text="Go" /></td>
                                </tr>
                                </table>
                            <asp:Label id="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label>
                            <asp:Label id="lblCPID" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: left;">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <asp:GridView id="gvItemsMasterDetails" SelectedRowStyle-BackColor="#c0c0c0" runat="server" AllowPaging="True"
                    AutoGenerateColumns="False" DataSourceID="sdsItemMasterDetails" OnRowDataBound="gvItemsMasterDetails_RowDataBound"
                    Width="100%" AllowSorting="True">
                    <columns>
<asp:BoundField DataField="ITEM_NAME" SortExpression="ITEM_NAME" HeaderText="ItemMasterNameHidden"></asp:BoundField>
<asp:BoundField DataField="ITEM_CODE" SortExpression="ITEM_CODE" HeaderText="ItemCode">
<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ITEM_MODEL_NO" SortExpression="ITEM_MODEL_NO" HeaderText="ModelNo"></asp:BoundField>
<asp:TemplateField HeaderText="ModelNo" SortExpression="ITEM_MODEL_NO"><EditItemTemplate>
&nbsp;
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnItemMasterName" onclick="lbtnItemMasterName_Click" ForeColor="#0066ff" runat="server" Text="<%# Bind('ITEM_MODEL_NO') %>" CausesValidation="False" __designer:wfdid="w10"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="ITEM_NAME" SortExpression="ITEM_NAME" HeaderText="SeriesName"></asp:BoundField>
<asp:BoundField DataField="ITEM_CATEGORY_NAME" SortExpression="ITEM_CATEGORY_NAME" HeaderText="CategoryName">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="IT_TYPE" SortExpression="IT_TYPE" HeaderText="SubCategory">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="PRODUCT_COMPANY_NAME" SortExpression="PRODUCT_COMPANY_NAME" HeaderText="Brand">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ITEM_QTY_IN_HAND" SortExpression="ITEM_QTY_IN_HAND" HeaderText="Qty"></asp:BoundField>
<asp:BoundField DataField="ITEM_RES_QTY" SortExpression="ITEM_RES_QTY" HeaderText="ResQty"></asp:BoundField>
<asp:BoundField DataField="CP_ID" SortExpression="CP_ID" HeaderText="Cp_Id">
<HeaderStyle Width="0px"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="BRAND_ID" SortExpression="BRAND_ID"></asp:BoundField>
<asp:BoundField DataField="GODOWN_ID" SortExpression="GODOWN_ID"></asp:BoundField>
<asp:BoundField DataField="COLOUR_ID" SortExpression="COLOUR_ID"></asp:BoundField>
<asp:BoundField DataField="COLOUR_NAME" SortExpression="COLOUR_NAME" HeaderText="Colour"></asp:BoundField>
<asp:BoundField DataField="GODOWN_NAME" SortExpression="GODOWN_NAME" HeaderText="Godown"></asp:BoundField>
<asp:BoundField DataField="CP_FULL_NAME" SortExpression="CP_FULL_NAME" HeaderText="Company"></asp:BoundField>
<asp:BoundField DataField="ITEM_QTY_ID" SortExpression="ITEM_QTY_ID" HeaderText="QtyId"></asp:BoundField>
</columns>
                    <selectedrowstyle backcolor="LightSteelBlue" />
                </asp:GridView>
                <asp:SqlDataSource id="sdsItemMasterDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                   SelectCommand="SP_MASTER_ITEMMASTER_SEARCH_SELECT1" SelectCommandType="StoredProcedure"><selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="CpId" ControlID="lblCPID"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>
</selectparameters></asp:SqlDataSource>&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; height: 36px;">
                <table id="Table1" align="center">
                    <tr>
                        <td>
                            <asp:Button id="btnNew" runat="server" Text="Add" CausesValidation="False" OnClick="btnNew_Click" /></td>
                        <td>
                            <asp:Button id="btnEdit" runat="server" Text="Edit" CausesValidation="False" OnClick="btnEdit_Click" /></td>
                        <td>
                            <asp:Button id="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" CausesValidation="False" /></td>
                        <td>
                            <asp:Button id="btnEditQty" runat="server" Text="Update" OnClick="btnEditQty_Click" CausesValidation="False" EnableTheming="True" ToolTip="Updae all Quantity's" Width="62px" Visible="False" /></td>
                        <td style="width: 3px">
                            <asp:Button id="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" CausesValidation="False" EnableTheming="True" Width="62px" /></td>
                        <td style="width: 3px">
                            <asp:Button id="btnClose1" runat="server" Text="Close" CausesValidation="False" OnClick="btnClose1_Click1" Visible="False" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <table id="tblStockEntry" runat ="server" width="80%" visible="false">
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left; height: 15px;">
                            Stock Entry
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                        <td style="text-align: right"><asp:Label id="Label39" runat="server" Text="Search:" Width="84px"></asp:Label></td>
                        <td style="text-align: left"><asp:TextBox ID="txtSearchModel" runat="server">
                        </asp:TextBox>
                            <asp:RequiredFieldValidator id="RequiredFieldValidator17" runat="server"
                                ControlToValidate="txtSearchModel" ErrorMessage="Please Enter ModelNo For Search" ValidationGroup="Search">*</asp:RequiredFieldValidator><asp:Button id="btnSearchModelNo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" onclick="btnSearchModelNo_Click1" Text="Go" ValidationGroup="Search" /></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 67px;"><asp:Label ID="Label25" runat="server" Text="Brand :" Width="45px"></asp:Label></td>
                        <td style="text-align: left; height: 67px;">
                            <asp:DropDownList id="ddlBrandStock" runat="server"  AutoPostBack="True" 
                                OnSelectedIndexChanged="ddlBrandStock_SelectedIndexChanged">
                        </asp:DropDownList><asp:Label ID="Label26" runat="server" EnableTheming="False" ForeColor="Red" Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlBrandStock"
                                ErrorMessage="Please Select the Brand" InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right; height: 67px;">
                            <asp:Label id="Label28" runat="server" Text="Model No:" Width="84px"></asp:Label></td>
                        <td style="text-align: left; height: 67px;">
                            <asp:DropDownList id="ddlModelnoStock" runat="server" AutoPostBack="True"
                                 OnSelectedIndexChanged="ddlModelnoStock_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label id="Label29" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*">
                            </asp:Label><asp:RequiredFieldValidator id="RequiredFieldValidator13" runat="server"
                                ControlToValidate="ddlModelnoStock" ErrorMessage="Please Select the Model No" InitialValue="0">*</asp:RequiredFieldValidator>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                    SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure"><selectparameters>
<asp:ControlParameter PropertyName="SelectedValue" Type="Int64" DefaultValue="0" Name="BranbId" ControlID="ddlBrandStock"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtSearchModel"></asp:ControlParameter>
</selectparameters>
                                </asp:SqlDataSource></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label id="Label32" runat="server" meta:resourcekey="Label26Resource1" Text="Company :">
                            </asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlCompany" runat="server" meta:resourcekey="ddlCompanyResource1" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                            </asp:DropDownList><asp:Label id="Label35" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label><asp:RequiredFieldValidator id="RequiredFieldValidator15" runat="server"
                                ControlToValidate="ddlCompany" ErrorMessage="Please select Company" InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label id="Label30" runat="server" Text="Godown:" Width="84px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlGoDown" runat="server" AutoPostBack="True"
                                >
                            </asp:DropDownList><asp:Label id="Label31" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*">
                            </asp:Label><asp:RequiredFieldValidator id="RequiredFieldValidator14" runat="server"
                                ControlToValidate="ddlGoDown" ErrorMessage="Please Select the GoDown" InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 26px;">
                            <asp:Label id="Label33" runat="server" meta:resourcekey="Label26Resource1" Text="Qty :">
                            </asp:Label></td>
                        <td style="text-align: left; height: 26px;"><asp:TextBox ID="txtQty" runat="server"></asp:TextBox><asp:Label ID="Label34" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                        </asp:Label><asp:RequiredFieldValidator id="RequiredFieldValidator16" runat="server"
                                ControlToValidate="txtQty" ErrorMessage="Please Enter the Qty">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right; height: 26px;"><asp:Label id="Label37" runat="server" Text="Colour:" Width="84px"></asp:Label></td>
                        <td style="text-align: left; height: 26px;"><asp:DropDownList id="ddlColor" runat="server" AutoPostBack="True"
                                >
                        </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"><asp:Label id="Label44" runat="server" meta:resourcekey="Label26Resource1" Text="Res Qty :">
                        </asp:Label></td>
                        <td style="text-align: left">
                            <asp:Label id="lblResqty" runat="server"></asp:Label></td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Button id="btnStockSave" runat="server" Text="Save" OnClick="btnStockSave_Click"  /></td>
                                    <td>
                                        <asp:Button id="btnStockRefresh" runat="server" Text="Refresh" CausesValidation="False" OnClick="btnStockRefresh_Click" /></td>
                                    <td>
                                        <asp:Button id="btnCloseStock" runat="server" Text="Close" OnClick="btnCloseStock_Click" CausesValidation="False" /></td>
                                </tr>
                            </table>
                            <asp:ValidationSummary id="ValidationSummary3" runat="server">
                            </asp:ValidationSummary><asp:ValidationSummary id="ValidationSummary4" runat="server" ValidationGroup="Search"></asp:ValidationSummary>
                        </td>
                    </tr>
                </table>
                <table id="tblPrint" runat ="server" width="80%" visible="false">
                    <tr>
                        <td class="profilehead" colspan="2" style="text-align: left">
                            Print Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 419px;">
                        </td>
                        <td style="text-align: left; width: 484px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 419px;" align="right">
                            <asp:Label id="Label40" runat="server" meta:resourcekey="Label26Resource1" Text="Company :">
                            </asp:Label></td>
                        <td style="text-align: left; width: 484px;">
                            <asp:DropDownList id="ddlCmpPrint" runat="server" meta:resourcekey="ddlCompanyResource1" AutoPostBack="True" OnSelectedIndexChanged="ddlCmpPrint_SelectedIndexChanged">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="width: 419px;" align="right">
                            <asp:Label id="Label42" runat="server" Text="Godown :" Width="61px"></asp:Label></td>
                        <td style="text-align: left; width: 484px;">
                            <asp:DropDownList id="ddlGodownPrint" runat="server" AutoPostBack="True"
                                >
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="width: 419px;" align="right">
                            <asp:Label ID="Label36" runat="server" Text="Brand :" Width="44px"></asp:Label></td>
                        <td style="text-align: left; width: 484px;">
                            <asp:DropDownList id="ddlBrandPrint" runat="server" Width="148px" AutoPostBack="True" OnSelectedIndexChanged="ddlBrandPrint_SelectedIndexChanged">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="width: 419px; height: 71px;" align="right">
                            <asp:Label id="Label41" runat="server" Text="Search :" Width="51px"></asp:Label></td>
                        <td style="width: 484px; text-align: left; height: 71px;">
                            <asp:TextBox ID="txtPrintSearch" runat="server">
                            </asp:TextBox><asp:RequiredFieldValidator id="RequiredFieldValidator18" runat="server"
                                ControlToValidate="txtPrintSearch" ErrorMessage="Please Enter ModelNo For Search" ValidationGroup="Search">*</asp:RequiredFieldValidator><asp:Button id="btnSearchPrint" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" onclick="btnSearchModelNo_Click" Text="Go" ValidationGroup="Search" /><br />
                            <asp:SqlDataSource id="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                <selectparameters>
<asp:ControlParameter PropertyName="SelectedValue" Type="Int64" DefaultValue="0" Name="BranbId" ControlID="ddlBrandPrint"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtPrintSearch"></asp:ControlParameter>
</selectparameters>
                            </asp:SqlDataSource></td>
                    </tr>
                    <tr>
                        <td style="width: 419px;" align="right"><asp:Label id="Label38" runat="server" Text="Model No:" Width="84px"></asp:Label></td>
                        <td style="width: 484px; text-align: left"><asp:DropDownList id="ddlModelnoPrint" runat="server" AutoPostBack="True"
                                 >
                        </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="width: 419px; text-align: right">
                        </td>
                        <td style="width: 484px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button id="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="*"  /></td>
                                    <td>
                                        <asp:Button id="btnClosePrint" runat="server" Text="Close" OnClick="btnClosePrint_Click" CausesValidation="False" /></td>
                                </tr>
                            </table>
                            <asp:ValidationSummary id="ValidationSummary2" runat="server" ValidationGroup="*">
                            </asp:ValidationSummary>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; height: 412px;">
                <table  runat ="server" width="100%" id="tblNew" visible="false" >
                    <tr>
                        <td colspan="3"><asp:GridView id="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" DataSourceID="SqlDataSource1" OnPageIndexChanged="GridView1_PageIndexChanged" Visible="False" >
                            <columns>
<asp:BoundField DataField="ITEM_NAME" HeaderText="ItemMasterNameHidden"></asp:BoundField>
<asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code">
<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="Model No">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Item  Name"><EditItemTemplate>
<asp:TextBox runat="server" Text='<%# Bind("Item_name") %>' id="TextBox1"></asp:TextBox>
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnItemMasterName" onclick="lbtnItemMasterName_Click" runat="server" Text="<%# Bind('Item_Name') %>" CausesValidation="False"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="ITEM_CATEGORY_NAME" HeaderText="Category Name">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="IT_TYPE" HeaderText="Sub Category">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="UOM_SHORT_DESC" HeaderText="UOM">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="PRODUCT_COMPANY_NAME" HeaderText="Brand">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ITEM_MIN_STOCK_QTY" HeaderText="Re-Order Level">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Edit Quantity"><ItemTemplate>
<asp:TextBox id="txtEditQty" runat="server" Text='<%# Bind("ITEM_QTY_IN_HAND") %>'></asp:TextBox> <asp:RegularExpressionValidator id="RegularExpressionValidator3" runat="server" ErrorMessage="Please Enter Numbers Only In Quantiy" ControlToValidate="txtEditQty" ValidationExpression="^[0-9]+$">*</asp:RegularExpressionValidator> 
</ItemTemplate>
</asp:TemplateField>
</columns>
                            <selectedrowstyle backcolor="LightSteelBlue" />
                        </asp:GridView>
                            <asp:SqlDataSource id="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                SelectCommand="SP_MASTER_NEW_ITEMMASTER_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                <selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="CpId" ControlID="lblCPID"></asp:ControlParameter>
</selectparameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="width: 3px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
             <td colspan="2" style="text-align: center; width: 725px;">
                <table border="0" cellpadding="0" cellspacing="0" id="tblItDetails" runat="server" visible="false" width="80%" style="text-align: left">
                    <tr>
            <td colspan="4" style="text-align: left; height: 20px;" class="profilehead">
                General Details</td>
                        <td class="profilehead" colspan="1" style="height: 20px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblModelNo" runat="server" Text="Model No :"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtModelNo" runat="server"></asp:TextBox>
                            <asp:Label ID="Label21" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                            </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtModelNo"
                                ErrorMessage="Please Enter the Model No">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right;">
                            <asp:Label id="lblItemName" runat="server" Text="Model Name/Series:"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtItemName" runat="server">
                            </asp:TextBox>
                            <asp:Label ID="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RFVDespName" runat="server" ControlToValidate="txtItemName"
                                ErrorMessage="Please Enter the Model Name/Series">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label16" runat="server" Text="Item Category :" Width="100px"></asp:Label></td>
                        <td style="text-align: left"><asp:DropDownList id="ddlItemCategory" runat="server" Width="151px" AutoPostBack="True" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged">
                        </asp:DropDownList>
                            <asp:Label ID="Label22" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                            </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlItemCategory"
                                ErrorMessage="Please Select the Item Category" InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblItemType" runat="server" Text="Item SubCategory :" Width="122px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlType" runat="server" Width="151px" >
                            </asp:DropDownList><asp:Label ID="Label3" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlType"
                                ErrorMessage="Please Select the Item SubCategory" InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label17" runat="server" Text="Color :" Width="100px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtColor" runat="server"></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label19" runat="server" Text="Brand :" Width="95px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlBrandName" runat="server" Width="148px">
                        </asp:DropDownList>
                            <asp:Label ID="Label23" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                            </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlBrandName"
                                ErrorMessage="Please Select the Brand" InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                <asp:Label id="lblItemSpecification" runat="server" Text="Item Specification"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox id="txtSpecification" runat="server" CssClass="multilinetext" EnableTheming="False" TextMode="MultiLine" Width="449px"></asp:TextBox>
                            <asp:Label ID="Label4" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSpecification"
                                ErrorMessage="Please Enter Item Specification">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label10" runat="server" Text="Purchase Specification:"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtPurchaseSpec" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="449px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label8" runat="server" Text="Principal Name"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPrincipalName" runat="server"></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label18" runat="server" Text="Financial Year :" Width="95px"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtFinacalYear" runat="server">
                            </asp:TextBox>
                            <asp:Label ID="Label24" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                            </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtFinacalYear"
                                ErrorMessage="Please Enter the Financial Year">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
            <td style="text-align: right;">
                <asp:Label id="lblMaterialType" runat="server" Text="Material Type"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtMatirealType" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label id="ddlItemUOMShort" runat="server" Text="UOM" Width="48px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlItemUOM" runat="server" Width="148px">
                            </asp:DropDownList>
                            <asp:Label ID="Label5" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlItemUOM"
                                ErrorMessage="Please Select the UOM" InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
            <td style="text-align: right;">
                <asp:Label id="Label1" runat="server" Text="Min. Stock Quantity" Width="140px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtMinimum" runat="server">
                            </asp:TextBox>
                            <asp:Label ID="Label2" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMinimum"
                                ErrorMessage="Please Enter the Min Stock Quantity">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label11" runat="server" Text="Purchase Type"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList id="ddlPurchaseItemType" runat="server" Width="151px">
                            </asp:DropDownList><asp:Label ID="Label12" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlPurchaseItemType" ErrorMessage="Please Select the Purchase Item Type"
                                    InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label9" runat="server" Text="RSP"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtRate" runat="server">
                            </asp:TextBox><asp:Label ID="Label20" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                            </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtSeries"
                                ErrorMessage="Please Enter the RSP">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtSeries"
                                ErrorMessage="Please Enter only Numbers for RSP" ValidationExpression="[0-9.]*$">*</asp:RegularExpressionValidator>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="lblRate" runat="server" Text="MRP" Width="30px"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtSeries" runat="server">
                            </asp:TextBox><asp:Label ID="Label27" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*">
                            </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="txtRate" ErrorMessage="Please Enter the MRP">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtRate"
                                ErrorMessage="Please Enter only Numbers for MRP" ValidationExpression="[0-9.]*$">*</asp:RegularExpressionValidator>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblMinimum" runat="server" Text="Attachments"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:FileUpload ID="FileUpload1" runat="server" EnableTheming="True" Font-Names="Verdana"
                                        Font-Size="8pt" ForeColor="#404040" Style="margin-left: 2px; margin-right: 3px"
                                        Width="478px" /></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label6" runat="server" Text="Attached Files"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:LinkButton ID="lbtnAttachedFile" runat="server"></asp:LinkButton>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 6px; text-align: right">
                        </td>
                        <td colspan="3" style="height: 6px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label13" runat="server" Text="Item Image :"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:Image ID="Image1" runat="server" Height="85px" ImageUrl="~/Images/noimage400x300.gif"
                                Width="140px" /><asp:Button ID="btnUpload" runat="server" CausesValidation="False"
                                    OnClientClick="window.open('../Masters/ItemImage.aspx','resume','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')"
                                    Text="Upload" /></td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label15" runat="server" Text="Technical Drawings" Width="78px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:Image ID="Image2" runat="server" Height="85px" ImageUrl="~/Images/noimage400x300.gif"
                                Width="140px" /><asp:Button ID="Button1" runat="server" CausesValidation="False" OnClientClick="window.open('../Masters/ItemSpecificationImage.aspx','resume','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')"
                                Text="Upload" /></td>
                    </tr>
                    <tr>
                        <td colspan="5" style="text-align: center">
                            <table>
                                <tr>
                                    <td>
                            <asp:Button id="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"  /></td>
                                    <td>
                            <asp:Button id="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" CausesValidation="False" /></td>
                                    <td>
                            <asp:Button id="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    </table>
            </td>
        </tr>
        </table>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
    ShowSummary="False" Visible="False" />



</asp:Content>


 
