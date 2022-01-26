<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SparesOrder.aspx.cs" Inherits="Modules_Services_SparesOrder" Title="||ERP:Services:SparesOrder||" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <script type="text/javascript">

function othernextfocus()
{
document.getElementById('<%=txtContactName1.ClientID%>').focus();
}

function email2nextfocus()
{
document.getElementById('<%=txtConsinment.ClientID%>').focus();
}
function rbVATCSTEnableDisable()
{
    if(document.getElementById('<%=rbVAT.ClientID %>').checked==true)
    {
    document.getElementById('<%=lblVATCST.ClientID %>').innerHTML="VAT"
    }  
    if(document.getElementById('<%=rbCST.ClientID %>').checked==true)
    {
    document.getElementById('<%=lblVATCST.ClientID %>').innerHTML="C.S. Tax";
    }
}
    </script>

    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                Spares &nbsp;order</td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="4" style="text-align: left" class="searchhead">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">
                            Spares Order</td>
                        <td>
                        </td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Label id="Label12" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:DropDownList id="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="SPO_NO">Spares Order No</asp:ListItem>
                                            <asp:ListItem Value="SPO_DATE">Spares Order Date</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME">Customer</asp:ListItem>
                                            <asp:ListItem Value="CUST_CONTACT_PERSON">Contact Person</asp:ListItem>
                                            <asp:ListItem Value="CUST_EMAIL">EMail</asp:ListItem>
                                            <asp:ListItem Value="SPO_ACCEPTANCE_FLAG">Status</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:DropDownList id="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                            EnableTheming="False" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged"
                                            Visible="False" Width="50px">
                                            <asp:ListItem Selected="True">=</asp:ListItem>
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem>&gt;</asp:ListItem>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <asp:ListItem>&gt;=</asp:ListItem>
                                            <asp:ListItem>R</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Label id="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:TextBox id="txtSearchValueFromDate" runat="server" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox><asp:Image id="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchFrom" runat="server" Enabled="False"
                                            PopupButtonID="imgFromDate" TargetControlID="txtSearchValueFromDate">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Label id="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:TextBox id="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><asp:Image id="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchValueToDate" runat="server"
                                            Enabled="False" PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Button id="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" onclick="btnSearchGo_Click" Text="Go" /></td>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                </tr>
                            </table>
                            <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 19px">
                <asp:GridView id="gvSalesOrderDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="sdsSparesOrderDetails" OnRowDataBound="gvSalesOrderDetails_RowDataBound">
                    <columns>
<asp:BoundField DataField="SPO_ID" HeaderText="SparesOrderIdHidden"></asp:BoundField>
<asp:TemplateField HeaderText="Spares Order No"><EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("SPO_NO") %>' ID="TextBox1"></asp:TextBox>
                        
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnSalesOrderNo" runat="server" Text="<%# BIND('SPO_NO') %>" CausesValidation="False" OnClick="lbtnSalesOrderNo_Click"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True" DataField="SPO_DATE" HeaderText="Spares Order Date"></asp:BoundField>
<asp:BoundField DataField="CUST_NAME" HeaderText="Customer">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CUST_CONTACT_PERSON" HeaderText="Contact Person">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CUST_EMAIL" HeaderText="Email">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="PREPAREDBY" HeaderText="Prepared By">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="APPROVEDBY" HeaderText="Approved By">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="SPO_ACCEPTANCE_FLAG" HeaderText="Status">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
</columns>
                    <emptydatatemplate>
No Data Exist!
</emptydatatemplate>
                    <selectedrowstyle backcolor="LightSteelBlue" />
                </asp:GridView><asp:SqlDataSource id="sdsSparesOrderDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SERVICES_SPARESORDER_SEARCH_SELECT" SelectCommandType="StoredProcedure"><selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="EMPID" ControlID="lblEmpIdHidden"></asp:ControlParameter>
</selectparameters></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 49px">
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table border="0" cellpadding="0" cellspacing="0" id="tblSalesOrderDetails" runat="server"
                    visible="false" width="100%">
                    <tr>
                        <td class="profilehead" colspan="4">
                            general details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label1" runat="server" Text="Quotation No" Width="86px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlQuotationNo" runat="server" OnSelectedIndexChanged="ddlQuotationNo_SelectedIndexChanged"
                                AutoPostBack="True" Enabled="False">
                            </asp:DropDownList><asp:Label id="Label35" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" ControlToValidate="ddlQuotationNo"
                                ErrorMessage="Please Select the Quotation No" InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label2" runat="server" Text="Quotation Date" Width="96px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtQuotationDate" runat="server" CssClass="datetext" EnableTheming="False" ReadOnly="True"></asp:TextBox>&nbsp;<asp:Image ID="imgQuotationDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                Visible="False" /><cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceQuotationDate"
                                    runat="server" PopupButtonID="imgQuotationDate" TargetControlID="txtQuotationDate"
                                    Enabled="False">
                                </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeQuotationDate" runat="server" DisplayMoney="Left"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtQuotationDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblCustomer" runat="server" Text="Customer Name"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCustName" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label55" runat="server" Text="Unit Name"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtUnitName" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right" rowspan="2">
                            <asp:Label id="lblAddress" runat="server" Text="Address"></asp:Label></td>
                        <td style="text-align: left" rowspan="2">
                            <asp:TextBox id="txtAddress" runat="server" TextMode="MultiLine" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label id="lblCity" runat="server" Text="Region"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtRegion" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label25" runat="server" Text="Phone"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtPhone" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label26" runat="server" Text="Mobile"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtMobile" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label id="lblEmail" runat="server" Text="Email"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtEmail" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;">
                        </td>
                        <td style="text-align: left; height: 19px;">
                        </td>
                        <td style="text-align: right; height: 19px;">
                        </td>
                        <td style="text-align: left; height: 19px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="gvQuotationProducts" runat="server" AutoGenerateColumns="False"
                                OnRowDataBound="gvQuotationItems_RowDataBound">
                                <columns>
                        <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ItemName" HeaderText="Item Name">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UOM" HeaderText="UOM">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Rate" HeaderText="Rate">
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Amount">
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4">
                            Spares Order details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblQuotationNo" runat="server" Text="Spares Order No" Width="112px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtSalesOrderNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label id="lblQuotationDate" runat="server" Text="Spares Order Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtSalesOrderDate" runat="server" CssClass="datetext" EnableTheming="False">
                            </asp:TextBox>&nbsp;<asp:Image id="imgSalesOrderDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="DateCustomValidate"
                                ControlToValidate="txtSalesOrderDate" ErrorMessage="Please Enter the Sales Order Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                SetFocusOnError="True">*</asp:CustomValidator>
                            <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSalesOrderDate" runat="server" PopupButtonID="imgSalesOrderDate"
                                TargetControlID="txtSalesOrderDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeSalesOrderDate" runat="server" DisplayMoney="Left"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtSalesOrderDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4">
                            item details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label3" runat="server" Text="Item Type" Width="63px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged">
                            </asp:DropDownList><asp:Label id="Label54" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlItemType"
                                ErrorMessage="Please Select the Item Type" InitialValue="0" ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label4" runat="server" Text="Item Name" Width="69px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlItemName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="0">-- Select Item Type --</asp:ListItem>
                            </asp:DropDownList><asp:Label id="Label36" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlItemName"
                                ErrorMessage="Please Select the Item Name" InitialValue="0" ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label27" runat="server" Text="UOM"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtItemUOM" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label22" runat="server" Text="Quantity" Width="57px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtItemQuantity" runat="server">
                            </asp:TextBox><asp:Label id="Label37" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtItemQuantity"
                                ErrorMessage="Please Enter the Quantity" ValidationGroup="ip">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                    ID="ftxtQuantity" runat="server" FilterType="Numbers" TargetControlID="txtItemQuantity">
                                </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="Label23" runat="server" Text="Item Specification"></asp:Label></td>
                        <td colspan="3" style="height: 21px; text-align: left">
                            <asp:TextBox ID="txtItemSpec" runat="server" CssClass="multilinetext" EnableTheming="False"
                                ReadOnly="True" TextMode="MultiLine" Width="94%">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label id="Label24" runat="server" Text="Rate"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:TextBox id="txtItemRate" runat="server">
                            </asp:TextBox><asp:Label id="Label38" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label>
                            <asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" ControlToValidate="txtItemRate"
                                ErrorMessage="Please Enter the Rate" ValidationGroup="ip">*</asp:RequiredFieldValidator>
                            <cc1:FilteredTextBoxExtender ID="ftxteItemRate" runat="server" FilterType="Numbers"
                                TargetControlID="txtItemRate">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="height: 21px; text-align: right">
                            <asp:Label id="lblPriority" runat="server" Text="Priority"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:DropDownList id="ddlItemPriority" runat="server">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem>Low</asp:ListItem>
                                <asp:ListItem>Medium</asp:ListItem>
                                <asp:ListItem>High</asp:ListItem>
                            </asp:DropDownList><asp:Label id="Label39" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label>
                            <asp:RequiredFieldValidator id="rfvPriority" runat="server" ControlToValidate="ddlItemPriority"
                                ErrorMessage="Please Select the Priority" InitialValue="0" ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label id="Label20" runat="server" Text="Specifications"></asp:Label></td>
                        <td style="height: 21px; text-align: left" colspan="3">
                            <asp:TextBox id="txtItemSpecifications" runat="server" TextMode="MultiLine" CssClass="multilinetext"
                                EnableTheming="False" Width="94%">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label id="Label21" runat="server" Text="Remarks"></asp:Label></td>
                        <td colspan="3" style="height: 21px; text-align: left">
                            <asp:TextBox id="txtItemRemarks" runat="server" TextMode="MultiLine" CssClass="multilinetext"
                                EnableTheming="False" Width="94%">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: right">
                            <asp:Button id="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" Text="Add" ValidationGroup="ip"
                                OnClick="btnAdd_Click" /></td>
                        <td style="text-align: left">
                            <asp:Button id="btnRefreshItems" runat="server" BackColor="Transparent" BorderStyle="None"
                                CausesValidation="False" CssClass="loginbutton" EnableTheming="False" Text="Refresh"
                                OnClick="btnItemRefresh_Click" /></td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView id="gvSalesOrderItems" runat="server" AutoGenerateColumns="False" Width="100%"
                                OnRowDeleting="gvSalesOrderItems_RowDeleting" OnRowDataBound="gvSalesOrderItems_RowDataBound"
                                OnRowEditing="gvSalesOrderItems_RowEditing">
                                <columns>
                        <asp:CommandField ShowEditButton="True" />
<asp:CommandField ShowDeleteButton="True"></asp:CommandField>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                        <asp:BoundField DataField="ItemType" HeaderText="Item Type" />
<asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
<asp:BoundField HeaderText="Amount"></asp:BoundField>
<asp:BoundField DataField="Specifications" HeaderText="Specifications"></asp:BoundField>
<asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
<asp:BoundField DataField="Priority" HeaderText="Priority"></asp:BoundField>
                        <asp:BoundField DataField="ItemTypeId" HeaderText="ItemTypeIdHidden" />
</columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4">
                            terms &amp; conditions</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label5" runat="server" Text="Delivery"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtDelivery" runat="server">
                            </asp:TextBox><asp:Label id="Label40" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label>
                            <asp:RequiredFieldValidator id="rfvDelivery" runat="server" ControlToValidate="txtDelivery"
                                ErrorMessage="Please Enter the Delivery">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label id="Label28" runat="server" Text="Currenct Type"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlCurrencyType" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label19" runat="server" Text="packing charges" Width="98px"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtPackingCharges" runat="server">
                            </asp:TextBox><asp:Label id="Label41" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label>
                            <asp:RequiredFieldValidator id="rfvPackingChrgs" runat="server" ControlToValidate="txtPackingCharges"
                                ErrorMessage="Please Enter the Packing Charges">*</asp:RequiredFieldValidator>
                            <cc1:FilteredTextBoxExtender ID="ftxtePackingCharges" runat="server" FilterType="Numbers"
                                TargetControlID="txtPackingCharges">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label18" runat="server" Text="payment terms"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtPaymentTerms" runat="server">
                            </asp:TextBox><asp:Label id="Label42" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label>
                            <asp:RequiredFieldValidator id="rfvPayTerms" runat="server" ControlToValidate="txtPaymentTerms"
                                ErrorMessage="Please Enter the Payment Terms">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                            <asp:RadioButton ID="rbVAT" runat="server" Checked="True" GroupName="vatcst" Text="VAT" />
                            <asp:RadioButton ID="rbCST" runat="server" GroupName="vatcst" Text="C.S. Tax" /></td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblVATCST" runat="server" Text="VAT"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtVAT" runat="server">
                            </asp:TextBox><asp:Label id="Label50" runat="server" EnableTheming="True" Text="%"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtVAT"
                                ErrorMessage="Please Enter the VAT or C.S. Tax ">*</asp:RequiredFieldValidator>
                            &nbsp;
                        </td>
                        <td style="text-align: right">
                            <asp:Label id="Label7" runat="server" Text="Excise Dutry"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtExciseDuty" runat="server">
                            </asp:TextBox><asp:Label id="Label44" runat="server" EnableTheming="True" Text="%"></asp:Label><cc1:FilteredTextBoxExtender
                                ID="ftxteExciseDuty" runat="server" FilterType="Numbers" TargetControlID="txtExciseDuty">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label10" runat="server" Text="Guarantee"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtGuarantee" runat="server">
                            </asp:TextBox><asp:Label id="Label45" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label>
                            <asp:RequiredFieldValidator id="rfvGuarantee" runat="server" ControlToValidate="txtGuarantee"
                                ErrorMessage="Please Enter the Guarantee">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label id="Label9" runat="server" Text="Despatch Mode"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlDespatchMode" runat="server">
                            </asp:DropDownList><asp:Label id="Label46" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label>
                            <asp:RequiredFieldValidator id="rfvDespatchMode" runat="server" ControlToValidate="ddlDespatchMode"
                                ErrorMessage="Please Enter the Despatch Mode">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label13" runat="server" Text="Insurance"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtInsurance" runat="server">
                            </asp:TextBox><asp:Label id="Label47" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label>
                            <asp:RequiredFieldValidator id="rfvInsurance" runat="server" ControlToValidate="txtInsurance"
                                ErrorMessage="Please Enter the Insurance">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label id="Label11" runat="server" Text="Transportation Charges" Width="143px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtTransCharges" runat="server">
                            </asp:TextBox><asp:Label id="Label48" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label>
                            <asp:RequiredFieldValidator id="rfvTransCharges" runat="server" ControlToValidate="txtTransCharges"
                                ErrorMessage="Please Enter the Transportation Charges">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                    ID="ftxteTransCharges" runat="server" FilterType="Numbers" TargetControlID="txtTransCharges">
                                </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label16" runat="server" Text="jurisdiction" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtJurisdiction" runat="server" Visible="False">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label id="Label14" runat="server" Text="Erection/Commisioning" Visible="False">
                            </asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtErrection" runat="server" Visible="False">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label17" runat="server" Text="inspection" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtInspection" runat="server" Visible="False">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label id="Label15" runat="server" Text="validity" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtValidity" runat="server" Visible="False">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label52" runat="server" Text="Advance Amt, If any"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtAdvanceAmt" runat="server">
                            </asp:TextBox><cc1:FilteredTextBoxExtender ID="ftxteAdvanceAmt" runat="server" TargetControlID="txtAdvanceAmt"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label43" runat="server" Text="Accessories "></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox id="txtAccessories" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="94%">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label49" runat="server" Text="Extra spares "></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox id="txtExtraSpares" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="94%">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label6" runat="server" Text="Other Details"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtOtherSpecs" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="94%">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Follow Up Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="lblResponsiblePerson" runat="server" Text="Contact Name" Width="96px">
                            </asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:TextBox ID="txtContactName1" runat="server" TabIndex="1">
                            </asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtContactName1" FilterMode="InvalidChars" InvalidChars="`1234567890-=+_)(*&^%$#@!~">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label29" runat="server" Text="Contact Name" Width="98px"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:TextBox ID="txtContactName2" runat="server" TabIndex="5">
                            </asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtContactName2" FilterMode="InvalidChars" InvalidChars="`1234567890-=+_)(*&^%$#@!~">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label id="lblDesignation1" runat="server" Text="Designation"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:DropDownList id="ddlDesignation1" runat="server" Width="154px" TabIndex="2">
                            </asp:DropDownList></td>
                        <td style="height: 19px; text-align: right">
                            <asp:Label id="lblDesignation2" runat="server" Text="Designation"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:DropDownList id="ddlDesignation2" runat="server" Width="154px" TabIndex="6">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label33" runat="server" Text="Phone No" Width="73px"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:TextBox ID="txtPhone1" runat="server" TabIndex="3">
                            </asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtPhone2"
                                ValidChars="-0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label32" runat="server" Text="Phone No" Width="71px"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:TextBox ID="txtPhone2" runat="server" TabIndex="7">
                            </asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtPhone1"
                                ValidChars="-0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label30" runat="server" Text="E-Mail" Width="96px"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:TextBox ID="txtEmail1" runat="server" TabIndex="4">
                            </asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                ControlToValidate="txtEmail1" ErrorMessage="Please Enter Valid Email" SetFocusOnError="True"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label31" runat="server" Text="E-Mail" Width="96px"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:TextBox ID="txtEmail2" runat="server" TabIndex="8">
                            </asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                ControlToValidate="txtEmail2" ErrorMessage="Please Enter  Valid Email" SetFocusOnError="True"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label34" runat="server" Text="Consignment To" Width="108px"></asp:Label></td>
                        <td colspan="3" style="height: 19px; text-align: left">
                            <asp:TextBox ID="txtConsinment" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="94%">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label53" runat="server" Text="Invoice To" Width="108px"></asp:Label></td>
                        <td colspan="3" style="height: 19px; text-align: left">
                            <asp:TextBox ID="txtInvoiceTo" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="94%">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label id="Label51" runat="server" Text="Attachment"></asp:Label></td>
                        <td colspan="3" style="height: 19px; text-align: left">
                            <asp:UpdatePanel id="UpdatePanel2" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                <contenttemplate>
<asp:FileUpload id="FileUpload1" runat="server" Font-Size="8pt" Font-Names="Verdana" Width="510px" Font-Italic="False"></asp:FileUpload> 
</contenttemplate>
                                <triggers>
<asp:PostBackTrigger ControlID="btnSave"></asp:PostBackTrigger>
</triggers>
                            </asp:UpdatePanel></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label id="Label56" runat="server" Text="Attached File"></asp:Label></td>
                        <td colspan="3" style="height: 19px; text-align: left">
                            <asp:LinkButton id="lbtnAttachedFiles" runat="server" Visible="False">
                            </asp:LinkButton>
                            <asp:Repeater id="UploadsRepeater" runat="server" DataSourceID="sdsUp">
                                <itemtemplate>
                                            <asp:LinkButton id="lbtnFileOpener" CausesValidation="False"  runat="server" OnClick="lbtnFileOpener_Click" Text='<%# bind("SO_UPLOAD_FILENAME") %>'></asp:LinkButton>
                                        </itemtemplate>
                            </asp:Repeater>&nbsp;
                            <asp:SqlDataSource id="sdsUp" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                SelectCommand="SELECT * FROM [YANTRA_SPARES_UPLOAD] WHERE SPO_ID=@SPO_IDpara">
                                <selectparameters>
<asp:ControlParameter PropertyName="Text" DefaultValue="0" Name="SPO_IDpara" ControlID="lblSPOIdHidden"></asp:ControlParameter>
</selectparameters>
                            </asp:SqlDataSource>
                            <asp:Label id="lblSPOIdHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            </td>
                        <td colspan="3" style="height: 19px; text-align: left">
                            </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            </td>
                        <td colspan="3" style="height: 19px; text-align: left">
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Reference Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlPreparedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label id="lblApprovedBy" runat="server" Text="Approved By"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlApprovedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label id="lblCheckedBy" runat="server" Text="Checked By" Visible="False"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:DropDownList id="ddlCheckedBy" runat="server" Enabled="False" Visible="False">
                            </asp:DropDownList></td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px">
                        </td>
                        <td style="height: 19px">
                        </td>
                        <td style="height: 19px">
                        </td>
                        <td style="height: 19px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 35px">
                            <asp:Button id="btnNew" runat="server" CausesValidation="False" onclick="btnNew_Click"
                                Text="New" Visible="False" />
                            <table id="tblButtons">
                                <tr>
                                    <td>
                                        <asp:Button id="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnApprove" runat="server" CausesValidation="False" OnClick="btnApprove_Click"
                                            Text="Approve" /></td>
                                    <td>
                                        <asp:Button id="btnEdit" runat="server" CausesValidation="False" onclick="btnEdit_Click"
                                            Text="Edit" /></td>
                                    <td>
                                        <asp:Button id="btnDelete" runat="server" CausesValidation="False" onclick="btnDelete_Click"
                                            Text="Delete" /></td>
                                    <td>
                                        <asp:Button id="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click"
                                            CausesValidation="False" /></td>
                                    <td>
                                        <asp:Button id="btnSendWorkOrder" runat="server" CausesValidation="False" onclick="btnSendWorkOrder_Click"
                                            Text="Order Profile" EnableTheming="True" /></td>
                                    <td>
                                        <asp:Button ID="btnSend" runat="server" CausesValidation="False" OnClick="btnSend_Click"
                                            Text="Send" Visible="False" /></td>
                                    <td>
                                        <asp:Button id="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" /></td>
                                    <td>
                                        <asp:Button id="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table id="tblNotVisibleDetails" runat="server" style="width: 131px" visible="false">
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="CS Tax" Visible="False"></asp:Label></td>
                        <td>
                            <asp:TextBox id="txtCST" runat="server" Visible="False">
                            </asp:TextBox></td>
                        <td>
                        </td>
                    </tr>
                </table>
                <asp:DropDownList id="ddlResponsiblePerson" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlResponsiblePerson_SelectedIndexChanged"
                    Visible="False">
                </asp:DropDownList><asp:TextBox id="txtFollowupEmail" runat="server" ReadOnly="True"
                    Visible="False"></asp:TextBox><asp:DropDownList id="ddlSalesPerson" runat="server"
                        Visible="False">
                    </asp:DropDownList><asp:ValidationSummary id="ValidationSummary1" runat="server">
                    </asp:ValidationSummary><asp:ValidationSummary id="ValidationSummary2" runat="server"
                        ValidationGroup="qi">
                    </asp:ValidationSummary></td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>


 
