<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/SalesMP1.master" AutoEventWireup="true" Async="true" CodeFile="SalesOrderDetails.aspx.cs" Inherits="Modules_SM_SalesOrderDetails" %>
<%@ Register Assembly="FUA" Namespace="Subgurim.Controles" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .profilehead {
            text-align: left;
        }
        .auto-style1 {
            width: 69px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" id="tblSalesOrderDetails" runat="server"
        visible="false" width="100%">
        <tr>
            <td class="profilehead" colspan="4">general details</td>
        </tr>
        <tr>
            <td style="text-align: right"></td>
            <td style="text-align: left"></td>
            <td style="text-align: right"></td>
            <td style="text-align: left"></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label1" runat="server" Text="Quotation No" Width="86px"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlQuotationNo" runat="server" OnSelectedIndexChanged="ddlQuotationNo_SelectedIndexChanged"
                    AutoPostBack="True" Enabled="False">
                </asp:DropDownList><asp:Label ID="Label35" runat="server" EnableTheming="False" ForeColor="Red"
                    Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlQuotationNo"
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
            <td id="TD18" style="height: 22px; text-align: right">
                <asp:Label ID="lblCustomer" runat="server" Text="Customer"></asp:Label></td>
            <td style="height: 22px; text-align: left">
                <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="True" Enabled="False"
                    OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                </asp:DropDownList><asp:Label ID="Label25" runat="server" EnableTheming="False" ForeColor="Red"
                    Text="*">
                </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlCustomer"
                    ErrorMessage="Please Select the Customer" InitialValue="0">*</asp:RequiredFieldValidator></td>
            <td style="height: 22px; text-align: right">
                <asp:Label ID="lblRegion" runat="server" Text="Region"></asp:Label></td>
            <td style="height: 22px; text-align: left">
                <asp:TextBox ID="txtRegion" runat="server" ReadOnly="True">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="height: 22px; text-align: right">
                <asp:Label ID="Label26" runat="server" Text="Industry Type"></asp:Label></td>
            <td style="height: 22px; text-align: left">
                <asp:TextBox ID="txtIndustryType" runat="server" ReadOnly="True">
                </asp:TextBox></td>
            <td style="height: 22px; text-align: right">
                <asp:Label ID="lblInitName" runat="server" Text="Unit Name" Width="74px"></asp:Label></td>
            <td style="height: 22px; text-align: left">
                <asp:DropDownList ID="ddlUnitName" runat="server" AutoPostBack="True" Enabled="False"
                    OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged">
                    <asp:ListItem Value="0">--</asp:ListItem>
                    <asp:ListItem Value="0">--Select Customer--</asp:ListItem>
                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvUnitName" runat="server" ControlToValidate="ddlUnitName"
                    ErrorMessage="Please Select the Unit Name" InitialValue="0">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="text-align: right; height: 31px;">
                <asp:Label ID="lblUnitAddress" runat="server" Text="Address" Width="106px"></asp:Label></td>
            <td colspan="3" style="text-align: left; height: 31px;">
                <asp:TextBox ID="txtUnitAddress" runat="server" CssClass="multilinetext" EnableTheming="False"
                    Font-Names="Verdana" Font-Size="8pt" TextMode="MultiLine" Width="660px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtContactPerson" runat="server" ReadOnly="True">
                </asp:TextBox><asp:DropDownList ID="ddlContactPerson" runat="server" AutoPostBack="True"
                    Enabled="False" OnSelectedIndexChanged="ddlContactPerson_SelectedIndexChanged"
                    Visible="False">
                    <asp:ListItem Value="0">--</asp:ListItem>
                    <asp:ListItem Value="0">--Select Unit Name--</asp:ListItem>
                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvContactPerson" runat="server"
                    ControlToValidate="ddlContactPerson" ErrorMessage="Please Select the Contact Person"
                    InitialValue="0">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right">
                <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td id="TD28" style="text-align: right">
                <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No" Width="74px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtPhoneNo" runat="server" ReadOnly="True">
                </asp:TextBox></td>
            <td style="text-align: right">
                <asp:Label ID="Label55" runat="server" Text="Mobile" Width="74px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right;"></td>
            <td style="text-align: left;"></td>
            <td style="text-align: right;"></td>
            <td style="text-align: left;"></td>
        </tr>
        <tr>
            <td colspan="4">&nbsp;<asp:GridView ID="gvQuotationProducts" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvQuotationProducts_RowDeleting" OnRowEditing="gvQuotationProducts_RowEditing" OnRowDataBound="gvQuotationProducts_RowDataBound1" Width="100%">
                <Columns>
                    <asp:CommandField ShowEditButton="True"></asp:CommandField>
                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                    <asp:BoundField DataField="ItemName" HeaderText="Item Name">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="UOM" HeaderText="UOM">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>

                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Currency" HeaderText="Currency"></asp:BoundField>
                    <asp:BoundField DataField="Rate" HeaderText="Rate">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>

                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Discount" NullDisplayText="-" HeaderText="Disc %"></asp:BoundField>
                    <asp:BoundField DataField="SpPrice" NullDisplayText="-" HeaderText="Special Price">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>

                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Room" HeaderText="Room"></asp:BoundField>
                    <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                    <asp:BoundField DataField="ColorId" HeaderText="ColorId"></asp:BoundField>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="CheckBox1"></asp:CheckBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkItemSelect" runat="server" OnCheckedChanged="chkItemSelect_CheckedChanged"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
                <asp:Button ID="btnGo" runat="server" CausesValidation="False" OnClick="btnGo_Click"
                    Text="Go" /></td>
        </tr>
        <tr>
            <td class="profilehead" colspan="4">Sales Order details</td>
        </tr>
        <tr>
            <td style="text-align: right"></td>
            <td style="text-align: left"></td>
            <td style="text-align: right"></td>
            <td style="text-align: left"></td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label ID="lblQuotationNo" runat="server" Text="Purchase Order No" Width="124px"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtSalesOrderNo" runat="server" ReadOnly="True"></asp:TextBox></td>
            <td style="text-align: right;">
                <asp:Label ID="lblQuotationDate" runat="server" Text="Purchase Order Date"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtSalesOrderDate" runat="server" CssClass="datetext"></asp:TextBox><asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>
                <asp:Label ID="Label60" runat="server" EnableTheming="False" ForeColor="Red"
                    Text="*">
                </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtSalesOrderDate"
                    ErrorMessage="Please Enter Purchase Order Date">*</asp:RequiredFieldValidator><asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="DateCustomValidate"
                        ControlToValidate="txtSalesOrderDate" ErrorMessage="Please Enter the Sales Order Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                        SetFocusOnError="True">*</asp:CustomValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtSalesOrderDate" Mask="99/99/9999" MaskType="Date" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" PopupButtonID="Image1"
                    TargetControlID="txtSalesOrderDate">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label57" runat="server" Text="Customer PO No."></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtCustPONo" runat="server"></asp:TextBox><asp:Label ID="Label61" runat="server" EnableTheming="False" ForeColor="Red"
                    Text="*"> </asp:Label><asp:RequiredFieldValidator ID="rfvCustPONo" runat="server" ControlToValidate="txtCustPONo"
                        ErrorMessage="Please Enter Customer PO No">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right">
                <asp:Label ID="Label58" runat="server" Text="Customer PO Dated"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtCustPODated" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox><asp:Image ID="imgCustPoDated" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><asp:Label ID="Label59" runat="server" EnableTheming="False" ForeColor="Red"
                    Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtCustPODated"
                        ErrorMessage="Please Enter Customer PO Dated">*</asp:RequiredFieldValidator><asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="DateCustomValidate"
                            ControlToValidate="txtCustPODated" ErrorMessage="Please Enter the Customer PO Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                            SetFocusOnError="True">*</asp:CustomValidator><cc1:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender1" runat="server" PopupButtonID="imgCustPoDated"
                                TargetControlID="txtCustPODated">
                            </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" DisplayMoney="Left"
                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtCustPODated" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
            </td>
        </tr>
        <tr>
            <td class="profilehead" colspan="4" style="text-align: left">PO Items</td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <asp:GridView ID="gvDonepo" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDataBound="gvDonepo_RowDataBound" ShowFooter="True">
                    <FooterStyle BackColor="#1AA8BE" />
                    <Columns>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDelete" runat="server" __designer:wfdid="w5" CausesValidation="false" OnClick="lbtnDelete_Click">Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Slno" HeaderText="Slno"></asp:BoundField>
                        <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                        <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                        <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                        <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                        <asp:BoundField DataField="Currency" HeaderText="Currency"></asp:BoundField>
                        <asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
                        <asp:BoundField HeaderText="UnitPrice"></asp:BoundField>
                        <asp:BoundField DataField="Price" HeaderText="Spl Price"></asp:BoundField>
                        <asp:BoundField DataField="Specifications" HeaderText="Specifications"></asp:BoundField>
                        <asp:BoundField DataFormatString="{0:dd/MM/YYYY}" DataField="DeliveryDate" HeaderText="Delivery Date">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Room" HeaderText="Room"></asp:BoundField>
                        <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                        <asp:BoundField DataField="ColorId" HeaderText="Color Id"></asp:BoundField>
                        <asp:BoundField DataField="Sales" HeaderText="Sales"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="profilehead" colspan="4">Item Details</td>
        </tr>
        <tr>
            <td style="height: 19px; text-align: right">
                <asp:Label ID="lblSelectModel" runat="server" Text="Select Model" Visible="False"></asp:Label></td>
            <td style="height: 19px; text-align: left">
                <asp:RadioButton ID="rdbAll" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="rdbAll_CheckedChanged"
                    Text="All" Visible="False"></asp:RadioButton></td>
            <td style="height: 19px; text-align: right"></td>
            <td style="height: 19px; text-align: left"></td>
        </tr>
        <tr>
            <td style="height: 19px; text-align: right">
                <asp:Label ID="lblSearch" runat="server" Text="Search:" Visible="False" Width="84px">
                </asp:Label></td>
            <td style="height: 19px; text-align: left">
                <asp:TextBox ID="txtSearchModel" runat="server" Visible="False">
                </asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server"
                    ControlToValidate="txtSearchModel" ErrorMessage="Please Enter ModelNo For Search"
                    ValidationGroup="Search" Visible="False">*</asp:RequiredFieldValidator><asp:Button
                        ID="btnSearchModelNo" runat="server" BorderStyle="None" CausesValidation="False"
                        CssClass="gobutton" EnableTheming="False" OnClick="btnSearchModelNo_Click" Text="Go"
                        ValidationGroup="Search" Visible="False" /></td>
            <td style="height: 19px; text-align: right">
                <asp:Label ID="lblSearchBrand" runat="server" Text="Search By Brand" Visible="False">
                </asp:Label></td>
            <td style="height: 19px; text-align: left">
                <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged"
                    Visible="False">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="text-align: right; height: 19px;">
                <asp:Label ID="Label3" runat="server" Text="Model No :"></asp:Label></td>
            <td style="text-align: left; height: 19px;">
                <asp:DropDownList ID="ddlModelNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModelNo_SelectedIndexChanged">
                </asp:DropDownList></td>
            <td style="text-align: right; height: 19px;">
                <asp:Label ID="Label4" runat="server" Text="Item Name" Width="69px"></asp:Label></td>
            <td style="text-align: left; height: 19px;">
                <asp:TextBox ID="txtItemname" runat="server">
                </asp:TextBox></td>
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
                </asp:TextBox><asp:Label ID="Label37" runat="server" EnableTheming="False" ForeColor="Red"
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
                <asp:Label ID="Label24" runat="server" Text="Rate"></asp:Label></td>
            <td style="height: 21px; text-align: left">
                <asp:TextBox ID="txtItemRate" runat="server">
                </asp:TextBox></td>
            <td style="height: 21px; text-align: right">
                <asp:Label ID="Label71" runat="server" Text="Special Price"></asp:Label></td>
            <td style="height: 21px; text-align: left">
                <asp:TextBox ID="txtSpecialPrice" runat="server">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="height: 21px; text-align: right">
                <asp:Label ID="Label38" runat="server" Text="Dicount"></asp:Label></td>
            <td style="height: 21px; text-align: left">
                <asp:TextBox ID="txtDiscount" runat="server">
                </asp:TextBox></td>
            <td style="height: 21px; text-align: right">
                <asp:Label ID="lblPriority" runat="server" Text="Priority"></asp:Label></td>
            <td style="height: 21px; text-align: left">
                <asp:DropDownList ID="ddlItemPriority" runat="server">
                    <asp:ListItem Value="0">--</asp:ListItem>
                    <asp:ListItem>Low</asp:ListItem>
                    <asp:ListItem>Medium</asp:ListItem>
                    <asp:ListItem>High</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="height: 21px; text-align: right">
                <asp:Label ID="Label20" runat="server" Text="Specifications"></asp:Label></td>
            <td style="height: 21px; text-align: left" colspan="3">
                <asp:TextBox ID="txtItemSpecifications" runat="server" TextMode="MultiLine" CssClass="multilinetext"
                    EnableTheming="False" Width="94%">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="height: 21px; text-align: right">
                <asp:Label ID="Label21" runat="server" Text="Remarks"></asp:Label></td>
            <td colspan="3" style="height: 21px; text-align: left">
                <asp:TextBox ID="txtItemRemarks" runat="server" TextMode="MultiLine" CssClass="multilinetext"
                    EnableTheming="False" Width="94%">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label54" runat="server" Text="Room"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtRoom" runat="server">
                </asp:TextBox><asp:Label ID="Label67" runat="server" EnableTheming="False" ForeColor="Red"
                    Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ControlToValidate="txtItemRate" ErrorMessage="Please Enter the Rate" ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right">
                <asp:Label ID="Label36" runat="server" Text="Delivery Date"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtDeliveryDate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox><asp:Image
                    ID="imgDelieryDate" runat="server" ImageUrl="~/Images/Calendar.png" /><asp:Label
                        ID="Label39" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                    </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtDeliveryDate" ErrorMessage="Please Enter Delivery Date" ValidationGroup="ip">*</asp:RequiredFieldValidator><asp:CustomValidator
                            ID="CustomValidator3" runat="server" ClientValidationFunction="DateCustomValidate"
                            ControlToValidate="txtDeliveryDate" ErrorMessage="Please Enter the Delivery Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                            SetFocusOnError="True" ValidationGroup="ip">*</asp:CustomValidator>&nbsp;&nbsp;
                            <cc1:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender2" runat="server" PopupButtonID="imgDelieryDate"
                                TargetControlID="txtDeliveryDate">
                            </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" DisplayMoney="Left"
                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtDeliveryDate" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
            </td>
        </tr>
        <tr>
            <td style="height: 19px; text-align: right">
                <asp:Label ID="Label65" runat="server" Text="Color"></asp:Label></td>
            <td style="height: 19px; text-align: left">
                <asp:DropDownList ID="ddlColor" runat="server">
                </asp:DropDownList></td>
            <td style="height: 19px; text-align: right">
                <asp:Label ID="Label8" runat="server" Text="Sales"></asp:Label></td>
            <td style="height: 19px; text-align: left">
                <asp:DropDownList ID="ddlSales" runat="server">
                    <asp:ListItem>Sales</asp:ListItem>
                    <asp:ListItem>Extra</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="text-align: right; height: 19px;"></td>
            <td style="text-align: right; height: 19px;">
                <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                    CssClass="loginbutton" EnableTheming="False" Text="Add" ValidationGroup="ip"
                    OnClick="btnAdd_Click" /></td>
            <td style="text-align: left; height: 19px;">
                <asp:Button ID="btnRefreshItems" runat="server" BackColor="Transparent" BorderStyle="None"
                    CausesValidation="False" CssClass="loginbutton" EnableTheming="False" Text="Refresh"
                    OnClick="btnItemRefresh_Click" /></td>
            <td style="text-align: left; height: 19px;"></td>
        </tr>
        <tr>
            <td style="height: 19px; text-align: right"></td>
            <td style="height: 19px; text-align: left"></td>
            <td style="height: 19px; text-align: right"></td>
            <td style="height: 19px; text-align: left"></td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvSalesOrderItems" runat="server" AutoGenerateColumns="False" Width="100%"
                    OnRowDeleting="gvSalesOrderItems_RowDeleting" OnRowDataBound="gvSalesOrderItems_RowDataBound"
                    OnRowEditing="gvSalesOrderItems_RowEditing" ShowFooter="True">
                    <FooterStyle BackColor="#1AA8BE" />
                    <Columns>
                        <asp:CommandField ShowEditButton="True"></asp:CommandField>
                        <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                        <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                        <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                        <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                        <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                        <asp:BoundField DataField="Currency" HeaderText="Currency"></asp:BoundField>
                        <asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
                        <asp:BoundField HeaderText="UnitPrice"></asp:BoundField>
                        <asp:BoundField DataField="Price" HeaderText="Spl Price"></asp:BoundField>
                        <asp:BoundField DataField="Specifications" HeaderText="Specifications"></asp:BoundField>
                        <asp:BoundField DataFormatString="{0:dd/MM/YYYY}" DataField="DeliveryDate" HeaderText="Delivery Date">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Room" HeaderText="Room"></asp:BoundField>
                        <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                        <asp:BoundField DataField="ColorId" HeaderText="Color Id"></asp:BoundField>
                        <asp:BoundField DataField="Sales" HeaderText="Sales"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="profilehead" colspan="4">CST &amp; TIN</td>
        </tr>
        <tr>
            <td style="height: 5px; text-align: right"></td>
            <td style="height: 5px; text-align: left"></td>
            <td style="height: 5px; text-align: right"></td>
            <td style="height: 5px; text-align: left"></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label66" runat="server" Text="CST No."></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtCSTNo" runat="server">
                </asp:TextBox></td>
            <td style="text-align: right">
                <asp:Label ID="Label68" runat="server" Text="TIN No."></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtTINNo" runat="server">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td class="profilehead" colspan="4">Terms &amp; Conditions</td>
        </tr>
        <tr>
            <td style="height: 19px; text-align: right"></td>
            <td style="height: 19px; text-align: left"></td>
            <td style="height: 19px; text-align: right"></td>
            <td style="height: 19px; text-align: left"></td>
        </tr>
        <tr>
            <td style="text-align: right; height: 23px;">
                <asp:Label ID="Label5" runat="server" Text="Delivery"></asp:Label></td>
            <td style="text-align: left; height: 23px;">
                <asp:TextBox ID="txtDelivery" runat="server">
                </asp:TextBox><asp:Label ID="Label40" runat="server" EnableTheming="False" ForeColor="Red"
                    Text="*"> </asp:Label>
                <asp:RequiredFieldValidator ID="rfvDelivery" runat="server" ControlToValidate="txtDelivery"
                    ErrorMessage="Please Enter the Delivery">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right; height: 23px;">
                <asp:Label ID="Label28" runat="server" Text="Currency Type"></asp:Label></td>
            <td style="text-align: left; height: 23px;">
                <asp:DropDownList ID="ddlCurrencyType" runat="server">
                </asp:DropDownList>&nbsp;<asp:Label ID="Label63" runat="server" EnableTheming="False" ForeColor="Red"
                    Text="*"> </asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCurrencyType"
                    ErrorMessage="Please Enter the Currency " InitialValue="0">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label19" runat="server" Text="packing charges" Width="98px"></asp:Label>
            </td>
            <td style="text-align: left">
                <asp:TextBox ID="txtPackingCharges" runat="server">
                </asp:TextBox><asp:Label ID="Label41" runat="server" EnableTheming="False" ForeColor="Red"
                    Text="*"> </asp:Label>
                <asp:RequiredFieldValidator ID="rfvPackingChrgs" runat="server" ControlToValidate="txtPackingCharges"
                    ErrorMessage="Please Enter the Packing Charges">*</asp:RequiredFieldValidator>
                <cc1:FilteredTextBoxExtender ID="ftxtePackingCharges" runat="server" FilterType="Numbers"
                    TargetControlID="txtPackingCharges">
                </cc1:FilteredTextBoxExtender>
            </td>
            <td style="text-align: right">
                <asp:Label ID="Label18" runat="server" Text="payment terms"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtPaymentTerms" runat="server">
                </asp:TextBox><asp:Label ID="Label42" runat="server" EnableTheming="False" ForeColor="Red"
                    Text="*"> </asp:Label>
                <asp:RequiredFieldValidator ID="rfvPayTerms" runat="server" ControlToValidate="txtPaymentTerms"
                    ErrorMessage="Please Enter the Payment Terms">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="text-align: right"></td>
            <td style="text-align: left">
                <table class="stacktable">
                    <tr>
                        <td class="auto-style1">
                <asp:RadioButton ID="rbVAT" runat="server" Checked="True" GroupName="vatcst" Text="VAT" />
                        </td>
                        <td>
                <asp:RadioButton ID="rbCST" runat="server" GroupName="vatcst" Text="C.S. Tax" /></td>
                    </tr>
                </table>
            </td>
            <td style="text-align: right">
                <asp:Label ID="lblHighseaSale" runat="server">Sales Status</asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlsalestatus" runat="server">
                    <asp:ListItem Value="0">---</asp:ListItem>
                    <asp:ListItem Value="1">Normal</asp:ListItem>
                    <asp:ListItem Value="2">High Sale</asp:ListItem>
                </asp:DropDownList>&nbsp;
                            <asp:Label ID="lblTotalamount" runat="server" Visible="False"></asp:Label></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="lblVATCST" runat="server" Text="VAT"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtVAT" runat="server">
                </asp:TextBox><asp:Label ID="Label50" runat="server" EnableTheming="True" Text="%"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtVAT"
                    ErrorMessage="Please Enter the VAT or C.S. Tax ">*</asp:RequiredFieldValidator>
                &nbsp;&nbsp;
            </td>
            <td style="text-align: right">
                <asp:Label ID="Label7" runat="server" Text="Excise Dutry" Visible="False"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtExciseDuty" runat="server" Visible="False"></asp:TextBox><asp:Label ID="Label44" runat="server" EnableTheming="True" Text="%" Visible="False"></asp:Label><cc1:FilteredTextBoxExtender
                    ID="ftxteExciseDuty" runat="server" FilterType="Numbers" TargetControlID="txtExciseDuty">
                </cc1:FilteredTextBoxExtender>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label10" runat="server" Text="Guarantee"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtGuarantee" runat="server">
                </asp:TextBox><asp:Label ID="Label45" runat="server" EnableTheming="False" ForeColor="Red"
                    Text="*"> </asp:Label>
                <asp:RequiredFieldValidator ID="rfvGuarantee" runat="server" ControlToValidate="txtGuarantee"
                    ErrorMessage="Please Enter the Guarantee">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right">
                <asp:Label ID="Label9" runat="server" Text="Despatch Mode"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlDespatchMode" runat="server">
                </asp:DropDownList><asp:Label ID="Label46" runat="server" EnableTheming="False" ForeColor="Red"
                    Text="*"> </asp:Label><asp:RequiredFieldValidator ID="rfvDespatchMode" runat="server" ControlToValidate="ddlDespatchMode"
                        ErrorMessage="Please Enter the Despatch Mode">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label13" runat="server" Text="Insurance"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtInsurance" runat="server">
                </asp:TextBox><asp:Label ID="Label47" runat="server" EnableTheming="False" ForeColor="Red"
                    Text="*"> </asp:Label>
                <asp:RequiredFieldValidator ID="rfvInsurance" runat="server" ControlToValidate="txtInsurance"
                    ErrorMessage="Please Enter the Insurance">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right">
                <asp:Label ID="Label11" runat="server" Text="Transportation Charges" Width="143px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtTransCharges" runat="server">
                </asp:TextBox><asp:Label ID="Label48" runat="server" EnableTheming="False" ForeColor="Red"
                    Text="*"> </asp:Label>
                <asp:RequiredFieldValidator ID="rfvTransCharges" runat="server" ControlToValidate="txtTransCharges"
                    ErrorMessage="Please Enter the Transportation Charges">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label16" runat="server" Text="jurisdiction" Visible="False"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtJurisdiction" runat="server" Visible="False">
                </asp:TextBox></td>
            <td style="text-align: right">
                <asp:Label ID="Label14" runat="server" Text="Erection/Commisioning" Visible="False">
                </asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtErrection" runat="server" Visible="False">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label17" runat="server" Text="inspection" Visible="False"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtInspection" runat="server" Visible="False">
                </asp:TextBox></td>
            <td style="text-align: right">
                <asp:Label ID="Label15" runat="server" Text="validity" Visible="False"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtValidity" runat="server" Visible="False">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right"></td>
            <td style="text-align: left"></td>
            <td style="text-align: right"></td>
            <td style="text-align: left"></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label52" runat="server" Text="Advance Amt, If any"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtAdvanceAmt" runat="server">
                </asp:TextBox><cc1:FilteredTextBoxExtender ID="ftxteAdvanceAmt" runat="server" TargetControlID="txtAdvanceAmt"
                    ValidChars=".0123456789">
                </cc1:FilteredTextBoxExtender>
            </td>
            <td style="text-align: right">
                <asp:Label ID="Label69" runat="server" Text="DeliveryDate For All Items" Visible="False"
                    Width="168px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtDeliveryDateForAll" runat="server" Visible="False">
                </asp:TextBox>
                <asp:Image ID="imgDeliveryDateForAll" runat="server" ImageUrl="~/Images/Calendar.png"
                    Visible="False"></asp:Image><asp:Label ID="Label70" runat="server" EnableTheming="False" ForeColor="Red"
                        Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator13"
                            runat="server" ControlToValidate="txtDeliveryDateForAll" ErrorMessage="Please Enter the Delivery Date For All"
                            Visible="False">*</asp:RequiredFieldValidator><asp:CustomValidator ID="CustomValidator4"
                                runat="server" ClientValidationFunction="DateCustomValidate" ControlToValidate="txtDeliveryDateForAll"
                                ErrorMessage="Please Enter the Delivery Date For All in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                SetFocusOnError="True" Visible="False">*</asp:CustomValidator><asp:CompareValidator
                                    ID="CompareValidator1" runat="server" ControlToCompare="txtQuotationDate" ControlToValidate="txtDeliveryDateForAll"
                                    ErrorMessage="Delivery Date For All Should be greterthan Quotation Date" Operator="LessThanEqual"
                                    SetFocusOnError="True" Type="Date" Visible="False">*</asp:CompareValidator>
                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="False" Format="dd/MM/yyyy"
                    PopupButtonID="imgDeliveryDateForAll" TargetControlID="txtDeliveryDateForAll">
                </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" DisplayMoney="Left"
                    Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDeliveryDateForAll"
                    UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label43" runat="server" Text="Accessories "></asp:Label></td>
            <td colspan="3" style="text-align: left">
                <asp:TextBox ID="txtAccessories" runat="server" CssClass="multilinetext" EnableTheming="False"
                    TextMode="MultiLine" Width="94%">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label49" runat="server" Text="Extra spares "></asp:Label></td>
            <td colspan="3" style="text-align: left">
                <asp:TextBox ID="txtExtraSpares" runat="server" CssClass="multilinetext" EnableTheming="False"
                    TextMode="MultiLine" Width="94%">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label6" runat="server" Text="Other Details"></asp:Label></td>
            <td colspan="3" style="text-align: left">
                <asp:TextBox ID="txtOtherSpecs" runat="server" CssClass="multilinetext" EnableTheming="False"
                    TextMode="MultiLine" Width="94%">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td class="profilehead" colspan="4" style="text-align: left; height: 39px;">Follow Up Details (at customer place)</td>
        </tr>
        <tr>
            <td style="height: 19px; text-align: right"></td>
            <td style="height: 19px; text-align: left"></td>
            <td style="height: 19px; text-align: right"></td>
            <td style="height: 19px; text-align: left"></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="lblResponsiblePerson" runat="server" Text="Contact Name" Width="96px">
                </asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtContactName1" runat="server" TabIndex="1">
                </asp:TextBox><asp:Label ID="Label62" runat="server" EnableTheming="False" ForeColor="Red"
                    Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtContactName1"
                        ErrorMessage="Please Enter Contact Name" SetFocusOnError="True">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtContactName1" FilterMode="InvalidChars" InvalidChars="`1234567890-=+_)(*&^%$#@!~">
                        </cc1:FilteredTextBoxExtender>
            </td>
            <td style="text-align: right">
                <asp:Label ID="Label29" runat="server" Text="Contact Name" Width="98px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtContactName2" runat="server" TabIndex="5">
                </asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtContactName2" FilterMode="InvalidChars" InvalidChars="`1234567890-=+_)(*&^%$#@!~">
                </cc1:FilteredTextBoxExtender>
            </td>
        </tr>
        <tr>
            <td style="height: 19px; text-align: right">
                <asp:Label ID="lblDesignation1" runat="server" Text="Designation"></asp:Label></td>
            <td style="height: 19px; text-align: left">
                <asp:DropDownList ID="ddlDesignation1" runat="server" Width="154px" TabIndex="2">
                </asp:DropDownList></td>
            <td style="height: 19px; text-align: right">
                <asp:Label ID="lblDesignation2" runat="server" Text="Designation"></asp:Label></td>
            <td style="height: 19px; text-align: left">
                <asp:DropDownList ID="ddlDesignation2" runat="server" Width="154px" TabIndex="6">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="height: 19px; text-align: right">
                <asp:Label ID="Label33" runat="server" Text="Phone No" Width="73px"></asp:Label></td>
            <td style="height: 19px; text-align: left">
                <asp:TextBox ID="txtPhone1" runat="server" TabIndex="3">
                </asp:TextBox><asp:Label ID="Label64" runat="server" EnableTheming="False" ForeColor="Red"
                    Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtPhone1"
                        ErrorMessage="Please Enter Phone No." SetFocusOnError="True">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtPhone2"
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
                </asp:TextBox></td>
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
                <asp:Label ID="lblFileNameHidden" runat="server"></asp:Label>
                <asp:Label ID="Label51" runat="server" Text="Attachment"></asp:Label></td>
            <td colspan="3" style="height: 19px; text-align: left">
                <cc2:FileUploaderAJAX id="FileUploaderAJAX1" runat="server" backcolor="#E7F4F6" directory_createifnotexists="True" font-names="Verdana" font-size="8pt" showdeletedfilesonpostback="False" text_x="[Remove]" />
                <asp:HiddenField ID="HiddenField1" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="height: 19px; text-align: right">
                <asp:Label ID="Label56" runat="server" Text="Attached File"></asp:Label></td>
            <td colspan="3" style="height: 19px; text-align: left">&nbsp;<asp:LinkButton ID="lbtnAttachedFiles" runat="server" Visible="False" OnClick="lbtnAttachedFiles_Click"></asp:LinkButton>
                <asp:Repeater ID="UploadsRepeater" runat="server" DataSourceID="sdsUploads">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnFileOpener" CausesValidation="False" runat="server" OnClick="lbtnFileOpener_Click" Text='<%# bind("SO_UPLOAD_FILENAME") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:SqlDataSource ID="sdsUploads" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SELECT * FROM [YANTRA_SO_UPLOADS] WHERE SO_ID=@SO_IDpara">
                    <SelectParameters>
                        <asp:ControlParameter PropertyName="Text" DefaultValue="0" Name="SO_IDpara" ControlID="lblSOIdHidden"></asp:ControlParameter>
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:Label ID="lblSOIdHidden" runat="server" Visible="False"></asp:Label></td>
        </tr>
        <tr>
            <td class="profilehead" colspan="4" style="text-align: left">Reference Details</td>
        </tr>
        <tr>
            <td style="height: 19px; text-align: right"></td>
            <td style="height: 19px; text-align: left"></td>
            <td style="height: 19px; text-align: right"></td>
            <td style="height: 19px; text-align: left"></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                </asp:DropDownList></td>
            <td style="text-align: right">
                <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlApprovedBy" runat="server" Enabled="False">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="height: 19px; text-align: right">
                <asp:Label ID="lblCheckedBy" runat="server" Text="Checked By" Visible="False"></asp:Label></td>
            <td style="height: 19px; text-align: left">
                <asp:DropDownList ID="ddlCheckedBy" runat="server" Enabled="False" Visible="False">
                </asp:DropDownList></td>
            <td style="height: 19px; text-align: right"></td>
            <td style="height: 19px; text-align: left"></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btnNew" runat="server" CausesValidation="False" OnClick="btnNew_Click"
                    Text="New" Visible="False" />
                <table id="tblButtons" align="center">
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                        <td>
                            <asp:Button ID="btnApprove" runat="server" CausesValidation="False" OnClick="btnApprove_Click"
                                Text="Approve" /></td>
                        <td>
                            <asp:Button ID="btnEdit" runat="server" CausesValidation="False" OnClick="btnEdit_Click"
                                Text="Edit" /></td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" CausesValidation="False" OnClick="btnDelete_Click"
                                Text="Delete" /></td>
                        <td>
                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click"
                                CausesValidation="False" /></td>
                        <td style="width: 3px">
                            <asp:Button ID="btnAcceptence" runat="server" Text="Acceptence" OnClick="btnAcceptence_Click" /></td>
                        <td>
                            <asp:Button ID="btnSendWorkOrder" runat="server" CausesValidation="False" OnClick="btnSendWorkOrder_Click"
                                Text="Internal Order" EnableTheming="True" /></td>
                        <td>
                            <asp:Button ID="btnSend" runat="server" CausesValidation="False" OnClick="btnSend_Click"
                                Text="Send" /></td>
                        <td>
                            <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" /></td>
                        <td>
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="test" />
                        </td>
                        <td>
                            <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server"></asp:ValidationSummary>
    <asp:ValidationSummary ID="ValidationSummary2" runat="server"
        ValidationGroup="ip"></asp:ValidationSummary>
    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="Search" ShowMessageBox="True"></asp:ValidationSummary>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
        SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter Type="Int64" DefaultValue="0" Name="BranbId"></asp:Parameter>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtSearchModel"></asp:ControlParameter>
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsSalesOrderDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
        SelectCommand="SP_SM_SALESORDER_SEARCH_SELECT" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="EMPID" ControlID="lblEmpIdHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
        Visible="False"></asp:Label>
    <asp:Label ID="lblEmpId" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
        Visible="False"></asp:Label>
    <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False">0</asp:Label>
    <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
</asp:Content>


 
