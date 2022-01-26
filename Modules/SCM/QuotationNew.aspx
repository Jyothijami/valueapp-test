<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/PurchaseMP1.master" AutoEventWireup="true" CodeFile="QuotationNew.aspx.cs" Inherits="Modules_SCM_QuotationNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function rbVATCSTEnableDisable() {
            if (document.getElementById('<%=rbVAT.ClientID %>').checked == true) {
        document.getElementById('<%=lblVATCST.ClientID %>').innerHTML = "VAT"
    }
    if (document.getElementById('<%=rbCST.ClientID %>').checked == true) {
        document.getElementById('<%=lblVATCST.ClientID %>').innerHTML = "C.S. Tax";
    }
    if (document.getElementById('<%=rbincluding.ClientID %>').checked == true) {
        document.getElementById('<%=lblVATCST.ClientID %>').innerHTML = "Including VAT";
    }
}

function amtcalc() {
    var req_qty, rate, disc;
    req_qty = document.getElementById('<%=txtQunatity.ClientID %>').value;
    rate = document.getElementById('<%=txtRate.ClientID %>').value;
    disc = document.getElementById('<%=txtDiscount.ClientID %>').value;


    if (req_qty == "" || req_qty == "0") {
        document.getElementById('<%=txtSpPrice.ClientID %>').value = "0";
    }
    else if (rate == "" || rate == "0") {
        document.getElementById('<%=txtSpPrice.ClientID %>').value = "0";
    }
    else if (rate > 0 && req_qty > 0) {
        document.getElementById('<%=txtSpPrice.ClientID %>').value = (rate * req_qty);
    }
    if (disc != "" && disc != "0") {
        document.getElementById('<%=txtSpPrice.ClientID %>').value = parseFloat((rate * req_qty)) - parseFloat((((rate * req_qty) * disc) / 100));
    }
}

function amtcalcDisc() {
    var req_qty, rate, spprice;
    req_qty = document.getElementById('<%=txtQunatity.ClientID %>').value;
    rate = document.getElementById('<%=txtRate.ClientID %>').value;
    spprice = document.getElementById('<%=txtSpPrice.ClientID %>').value;

    if (req_qty == "" || req_qty == "0") {
        document.getElementById('<%=txtSpPrice.ClientID %>').value = "0";
    }
    else if (rate == "" || rate == "0") {
        document.getElementById('<%=txtSpPrice.ClientID %>').value = "0";
    }
    else if (rate > 0 && req_qty > 0) {
        document.getElementById('<%=txtDiscount.ClientID %>').value = (((rate * req_qty) - spprice) * 100) / (rate * req_qty);
    }
}

    </script>
        <script>
            $(function () {
                $("[name$='txtEnquiryDate'],[name$='txtQuotationDate']").datepicker({
                    changeMonth: true,
                    changeYear: true
                });
            });
    </script>
    <style type="text/css">
        .profilehead {
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>Proforma Invoice</td>
            <td>  <asp:Label id="lblEmpIdHidden" runat="server" Visible="False" meta:resourcekey="lblEmpIdHiddenResource1"></asp:Label><asp:Label
                                id="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label><asp:Label id="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                    Visible="False"></asp:Label></td>

        </tr>
    </table>
    <table style="width:100%">
        <tr>
            <td colspan="4">
                <table border="0" cellpadding="0" cellspacing="0" id="tblSupQuotationDetails" runat="server" visible="true" width="100%">
                    <tr>
                        <td class="profilehead" colspan="4">General Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblCustomer" runat="server" Text="Supplier Name" meta:resourcekey="lblCustomerResource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlSupplierName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSupplierName_SelectedIndexChanged" meta:resourcekey="ddlSupplierNameResource1">
                            </asp:DropDownList><asp:Label ID="Label26" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*" meta:resourcekey="Label26Resource1"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                    ControlToValidate="ddlSupplierName" ErrorMessage="Please Select The Supplier Name" meta:resourcekey="RequiredFieldValidator2Resource1">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label22" runat="server" Text="Enquiry Date" Width="96px" meta:resourcekey="Label22Resource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtEnquiryDate" runat="server" CssClass="datetext" EnableTheming="False" meta:resourcekey="txtEnquiryDateResource1" ReadOnly="True"></asp:TextBox>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label21" runat="server" Text="Enquiry No" Width="72px" meta:resourcekey="Label21Resource1"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlEnquiryNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEnquiryNo_SelectedIndexChanged" meta:resourcekey="ddlEnquiryNoResource1">
                            </asp:DropDownList><asp:Label ID="Label36" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*" meta:resourcekey="Label36Resource1"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlEnquiryNo"
                                    ErrorMessage="Please Select  the Enquiry No." InitialValue="0" meta:resourcekey="RequiredFieldValidator4Resource1">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td rowspan="2" style="text-align: right">
                            <asp:Label ID="lblAddress" runat="server" Text="Address" meta:resourcekey="lblAddressResource1"></asp:Label></td>
                        <td rowspan="2" style="text-align: left">
                            <asp:TextBox ID="txtAddress" runat="server" Height="40px" TextMode="MultiLine" ReadOnly="True" meta:resourcekey="txtAddressResource1"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label23" runat="server" Text="Phone" meta:resourcekey="Label23Resource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPhone" runat="server" ReadOnly="True" meta:resourcekey="txtPhoneResource1"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label24" runat="server" Text="Mobile" meta:resourcekey="Label24Resource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True" meta:resourcekey="txtMobileResource1"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblEmail" runat="server" Text="Email" meta:resourcekey="lblEmailResource1"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True" meta:resourcekey="txtEmailResource1"></asp:TextBox></td>
                        <td style="text-align: right;"></td>
                        <td style="text-align: left;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="gvEnquiryProducts" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvEnquiryProducts_RowDeleting" OnRowEditing="gvEnquiryProducts_RowEditing" Width="100%" meta:resourcekey="gvEnquiryProductsResource1">
                                <Columns>
                                    <asp:CommandField ShowEditButton="True" meta:resourcekey="CommandFieldResource1"></asp:CommandField>
                                    <asp:CommandField ShowDeleteButton="True" meta:resourcekey="CommandFieldResource2"></asp:CommandField>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code" meta:resourcekey="BoundFieldResource6">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Model No" meta:resourcekey="BoundFieldResource7">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ItemType" HeaderText="Model Name" meta:resourcekey="BoundFieldResource8"></asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM" meta:resourcekey="BoundFieldResource9">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Brand" HeaderText="Brand" meta:resourcekey="BoundFieldResource10"></asp:BoundField>
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" meta:resourcekey="BoundFieldResource11">
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Specification" HeaderText="Specifications" meta:resourcekey="BoundFieldResource12">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Priority" HeaderText="Priority" meta:resourcekey="BoundFieldResource13">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left" class="profilehead" colspan="4">Quotation Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblQuotationNo" runat="server" Text="PI No" meta:resourcekey="lblQuotationNoResource1"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtQuotationNo" runat="server" ReadOnly="True" meta:resourcekey="txtQuotationNoResource1"></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblQuotationDate" runat="server" Text="PI Date" Width="152px" meta:resourcekey="lblQuotationDateResource1"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtQuotationDate" runat="server" meta:resourcekey="txtQuotationDateResource1"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label12" runat="server" Text="Transport" Visible="False" meta:resourcekey="Label12Resource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlTransport" runat="server" Visible="False" meta:resourcekey="ddlTransportResource1">
                                <asp:ListItem meta:resourcekey="ListItemResource13">--</asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblPOType" runat="server" Text="PO Type" Width="129px" Visible="False" meta:resourcekey="lblPOTypeResource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlPOType" runat="server" Visible="False" meta:resourcekey="ddlPOTypeResource1">
                                <asp:ListItem meta:resourcekey="ListItemResource14">--</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">&nbsp;Product Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="Label16" runat="server" Text="Brand"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="text-align: left;"></td>
                        <td style="text-align: left;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label1" runat="server" Text="Model No" meta:resourcekey="Label1Resource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlItemCode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemCode_SelectedIndexChanged" meta:resourcekey="ddlItemCodeResource1" Width="154px">
                                <asp:ListItem meta:resourcekey="ListItemResource15">--</asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="text-align: right">&nbsp;</td>
                        <td style="text-align: left">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">&nbsp;<asp:Label ID="Label27" runat="server" Text="Item Category " Width="107px" meta:resourcekey="Label27Resource1"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtItemCategory" runat="server" ReadOnly="True" meta:resourcekey="txtItemCategoryResource1"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblItemName" runat="server" Text="Model Name" Width="95px" meta:resourcekey="lblItemNameResource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtItemName" runat="server" ReadOnly="True" meta:resourcekey="txtItemNameResource1"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="Label59" runat="server" Text="Color " meta:resourcekey="Label59Resource1"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlColor" runat="server" Width="154px">
                            </asp:DropDownList></td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label58" runat="server" Text="Item SubCategory " meta:resourcekey="Label58Resource1"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtItemSubCategory" runat="server" ReadOnly="True" meta:resourcekey="txtItemSubCategoryResource1"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label2" runat="server" Text="UOM" meta:resourcekey="Label2Resource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtUOM" runat="server" ReadOnly="True" meta:resourcekey="txtUOMResource1"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label60" runat="server" Text="Brand " meta:resourcekey="Label60Resource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtBrand" runat="server" ReadOnly="True" meta:resourcekey="txtBrandResource1"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label18" runat="server" Text="Specifications" meta:resourcekey="Label18Resource1"></asp:Label></td>
                        <td style="text-align: left" colspan="3">
                            <asp:TextBox ID="txtSpecifications" runat="server" TextMode="MultiLine" EnableTheming="False" Width="559px" meta:resourcekey="txtSpecificationsResource1"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblQuantity" runat="server" Text="Quantity" Width="54px" meta:resourcekey="lblQuantityResource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtQunatity" runat="server" meta:resourcekey="txtQunatityResource1"></asp:TextBox><asp:Label ID="Label4" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*" meta:resourcekey="Label4Resource1"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ControlToValidate="txtQunatity" ErrorMessage="Please Enter the Quantity" ValidationGroup="qi" meta:resourcekey="RequiredFieldValidator1Resource1">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                        ID="ftxteQuantity" runat="server" FilterType="Numbers" TargetControlID="txtQunatity" Enabled="True">
                                    </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="lblRate" runat="server" Text="New Rate" Width="76px" meta:resourcekey="lblRateResource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlRate" runat="server" CssClass="dropdownlist"
                                EnableTheming="False" Width="67px" meta:resourcekey="ddlRateResource1">
                            </asp:DropDownList><asp:TextBox ID="txtRate" runat="server" CssClass="textboxqt"
                                EnableTheming="False" Width="88px" meta:resourcekey="txtRateResource1"></asp:TextBox><asp:Label ID="Label6" runat="server"
                                    EnableTheming="False" ForeColor="Red" Text="*" meta:resourcekey="Label6Resource1"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="txtRate" ErrorMessage="Please Enter the Rate" ValidationGroup="qi" meta:resourcekey="RequiredFieldValidator3Resource1">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                            ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtRate" ValidChars=".0123456789" Enabled="True">
                                        </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label33" runat="server" Text="Discount" Width="54px" meta:resourcekey="Label33Resource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDiscount" runat="server" meta:resourcekey="txtDiscountResource1"></asp:TextBox><asp:Label ID="Label55" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*" meta:resourcekey="Label55Resource1"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server"
                                    ControlToValidate="txtDiscount" ErrorMessage="Please Enter the Discount" ValidationGroup="qi" meta:resourcekey="RequiredFieldValidator15Resource1">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                        ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtDiscount" ValidChars=".0123456789" Enabled="True">
                                    </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label56" runat="server" Text="Special Price" meta:resourcekey="Label56Resource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtSpPrice" runat="server" meta:resourcekey="txtSpPriceResource1"></asp:TextBox><asp:Label ID="Label57" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*" meta:resourcekey="Label57Resource1"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server"
                                    ControlToValidate="txtSpPrice" ErrorMessage="Please Enter the Special Price"
                                    ValidationGroup="qi" meta:resourcekey="RequiredFieldValidator16Resource1">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3"
                                        runat="server" TargetControlID="txtSpPrice" ValidChars=".0123456789" Enabled="True">
                                    </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label25" runat="server" Text="Item Image" meta:resourcekey="Label25Resource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:Image ID="Image1" runat="server" Height="85px" ImageUrl="~/Images/noimage400x300.gif"
                                Width="140px" meta:resourcekey="Image1Resource1"></asp:Image></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label7" runat="server" Text="Old Rate(RSP)" Width="95px" meta:resourcekey="Label7Resource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtOldRate" runat="server" CssClass="textboxqt"
                                EnableTheming="False" Width="88px" ReadOnly="True" meta:resourcekey="txtOldRateResource1"></asp:TextBox><cc1:FilteredTextBoxExtender
                                    ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtRate" ValidChars=".0123456789" Enabled="True">
                                </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left">
                            <asp:Button ID="btnAdd" runat="server" Text="Add" BackColor="Transparent" BorderStyle="None" CssClass="loginbutton" EnableTheming="False" OnClick="btnAdd_Click" ValidationGroup="qi" meta:resourcekey="btnAddResource1" />
                            <asp:Button ID="btnItemsRefresh" runat="server" Text="Refresh" BackColor="Transparent" BorderStyle="None" CssClass="loginbutton" EnableTheming="False" OnClick="btnItemsRefresh_Click" meta:resourcekey="btnItemsRefreshResource1" /></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                           
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:GridView ID="gvProductDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvProductDetails_RowDataBound" OnRowDeleting="gvProductDetails_RowDeleting" meta:resourcekey="gvProductDetailsResource1" ShowFooter="True" Width="100%">
                                <FooterStyle BackColor="#1AA8BE" />
                                <Columns>
                                    <asp:CommandField ShowDeleteButton="True" meta:resourcekey="CommandFieldResource3"></asp:CommandField>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code" meta:resourcekey="BoundFieldResource14">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Model No" meta:resourcekey="BoundFieldResource15"></asp:BoundField>
                                    <asp:BoundField DataField="ModelName" HeaderText="Model Name" meta:resourcekey="BoundFieldResource16"></asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM" meta:resourcekey="BoundFieldResource17"></asp:BoundField>
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" meta:resourcekey="BoundFieldResource18"></asp:BoundField>
                                    <asp:BoundField DataField="Brand" HeaderText="Brand" meta:resourcekey="BoundFieldResource19"></asp:BoundField>
                                    <asp:BoundField DataField="Curency" HeaderText="Currency" meta:resourcekey="BoundFieldResource20"></asp:BoundField>
                                    <asp:BoundField DataField="Rate" HeaderText="Rate" meta:resourcekey="BoundFieldResource21">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Amount" meta:resourcekey="BoundFieldResource22"></asp:BoundField>
                                    <asp:BoundField DataField="Disc" HeaderText="Disc %" meta:resourcekey="BoundFieldResource23"></asp:BoundField>
                                    <asp:BoundField DataField="SpRate" HeaderText="Special Amount" meta:resourcekey="BoundFieldResource24"></asp:BoundField>
                                    <asp:BoundField DataField="Specification" HeaderText="Specifications" meta:resourcekey="BoundFieldResource25"></asp:BoundField>
                                    <asp:BoundField DataField="CurencyId" HeaderText="CurrencyId" meta:resourcekey="BoundFieldResource26"></asp:BoundField>
                                    <asp:BoundField DataField="oldrate" HeaderText="Old Rate" meta:resourcekey="BoundFieldResource27"></asp:BoundField>
                                    <asp:BoundField DataField="EnqNo" HeaderText="Enq No" meta:resourcekey="BoundFieldResource28"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="profilehead">Terms &amp; Conditions</td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;">
                            <asp:Label ID="Label5" runat="server" Text="Delivery" meta:resourcekey="Label5Resource1"></asp:Label></td>
                        <td style="height: 19px" align="left">
                            <asp:TextBox ID="txtDelivery"
                                runat="server" TextMode="MultiLine" meta:resourcekey="txtDeliveryResource1"></asp:TextBox><asp:Label ID="Label28" runat="server"
                                    EnableTheming="False" ForeColor="Red" Text="*" meta:resourcekey="Label28Resource1"></asp:Label><asp:RequiredFieldValidator ID="rfvDelivery" runat="server" ControlToValidate="txtDelivery"
                                        ErrorMessage="Please Enter the Delivery" meta:resourcekey="rfvDeliveryResource1">*</asp:RequiredFieldValidator></td>
                        <td style="height: 19px" align="right">
                            <asp:Label
                                ID="Label3" runat="server" Text="Payment Terms" meta:resourcekey="Label3Resource1"></asp:Label></td>
                        <td style="height: 19px" align="left">
                            <asp:TextBox ID="txtPaymentTerms"
                                runat="server" TextMode="MultiLine" meta:resourcekey="txtPaymentTermsResource1"></asp:TextBox><asp:Label ID="Label35" runat="server"
                                    EnableTheming="False" ForeColor="Red" Text="*" meta:resourcekey="Label35Resource1"></asp:Label><asp:RequiredFieldValidator ID="rfvPayTerms" runat="server" ControlToValidate="txtPaymentTerms"
                                        ErrorMessage="Please Enter the Payment Terms" meta:resourcekey="rfvPayTermsResource1">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="height: 10px; text-align: right">
                            <asp:Label
                                ID="Label19" runat="server" Text="packing charges" meta:resourcekey="Label19Resource1"></asp:Label></td>
                        <td align="left" style="height: 10px">
                            <asp:TextBox ID="txtPackingCharges"
                                runat="server" meta:resourcekey="txtPackingChargesResource1"></asp:TextBox><asp:Label ID="Label29" runat="server" EnableTheming="False"
                                    ForeColor="Red" Text="*" meta:resourcekey="Label29Resource1"></asp:Label><asp:RequiredFieldValidator ID="rfvPackingChrgs" runat="server" ControlToValidate="txtPackingCharges"
                                        ErrorMessage="Please Enter the Packing Charges" meta:resourcekey="rfvPackingChrgsResource1">*</asp:RequiredFieldValidator></td>
                        <td align="right" style="height: 10px">
                            <asp:Label
                                ID="Label10" runat="server" Text="Guarantee" meta:resourcekey="Label10Resource1"></asp:Label></td>
                        <td align="left" style="height: 10px">
                            <asp:TextBox ID="txtGuarantee"
                                runat="server" meta:resourcekey="txtGuaranteeResource1"></asp:TextBox><asp:Label ID="Label31" runat="server" EnableTheming="False"
                                    ForeColor="Red" Text="*" meta:resourcekey="Label31Resource1"></asp:Label><asp:RequiredFieldValidator ID="rfvGuarantee" runat="server" ControlToValidate="txtGuarantee"
                                        ErrorMessage="Please Enter the Guarantee" meta:resourcekey="rfvGuaranteeResource1">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="right" style="height: 19px; text-align: right"></td>
                        <td align="left" style="height: 19px">
                            <asp:RadioButton
                                ID="rbVAT" runat="server" Checked="True" GroupName="vatcst" Text="VAT" meta:resourcekey="rbVATResource1"></asp:RadioButton><asp:RadioButton
                                    ID="rbCST" runat="server" GroupName="vatcst" Text="C.S. Tax" meta:resourcekey="rbCSTResource1"></asp:RadioButton><asp:RadioButton
                                        ID="rbincluding" runat="server" GroupName="vatcst" Text="Including" meta:resourcekey="rbincludingResource1"></asp:RadioButton></td>
                        <td align="right" style="height: 19px">
                            <asp:Label
                                ID="Label13" runat="server" Text="Insurance" meta:resourcekey="Label13Resource1"></asp:Label></td>
                        <td align="left" style="height: 19px">
                            <asp:TextBox ID="txtInsurance"
                                runat="server" meta:resourcekey="txtInsuranceResource1"></asp:TextBox><asp:Label ID="Label32" runat="server" EnableTheming="False"
                                    ForeColor="Red" Text="*" meta:resourcekey="Label32Resource1"></asp:Label><asp:RequiredFieldValidator ID="rfvInsurance" runat="server" ControlToValidate="txtInsurance"
                                        ErrorMessage="Please Enter the Insurance" meta:resourcekey="rfvInsuranceResource1">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="right" style="height: 19px; text-align: right">
                            <asp:Label
                                ID="lblVATCST" runat="server" Text="VAT" meta:resourcekey="lblVATCSTResource1"></asp:Label></td>
                        <td align="left" style="height: 19px">
                            <asp:TextBox ID="txtVAT" runat="server" meta:resourcekey="txtVATResource1"></asp:TextBox>
                            <asp:Label ID="lblPercent" runat="server" EnableTheming="True" Text="%" meta:resourcekey="lblPercentResource1"></asp:Label>
                            <cc1:FilteredTextBoxExtender ID="ftxteVAT" runat="server" TargetControlID="txtVAT"
                                ValidChars=".0123456789" Enabled="True">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td align="right" style="height: 19px">
                            <asp:Label ID="Label15" runat="server" Text="validity" meta:resourcekey="Label15Resource1"></asp:Label></td>
                        <td align="left" style="height: 19px">
                            <asp:TextBox ID="txtValidity"
                                runat="server" meta:resourcekey="txtValidityResource1"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" style="height: 19px; text-align: right">
                            <asp:Label ID="Label9" runat="server" Text="Despatch Mode" meta:resourcekey="Label9Resource1"></asp:Label></td>
                        <td align="left" style="height: 19px">
                            <asp:DropDownList
                                ID="ddlDespatchMode" runat="server" meta:resourcekey="ddlDespatchModeResource1">
                            </asp:DropDownList><asp:Label ID="Label39" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*" meta:resourcekey="Label39Resource1"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                ControlToValidate="ddlDespatchMode" ErrorMessage="Please Enter the Despatch Mode" meta:resourcekey="RequiredFieldValidator5Resource1">*</asp:RequiredFieldValidator></td>
                        <td align="right" style="height: 19px">
                            <asp:Label
                                ID="Label11" runat="server" Text="Transportation Charges" meta:resourcekey="Label11Resource1"></asp:Label></td>
                        <td align="left" style="height: 19px">
                            <asp:TextBox
                                ID="txtTransCharges" runat="server" TextMode="MultiLine" meta:resourcekey="txtTransChargesResource1"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" style="text-align: right">
                            <asp:Label
                                ID="Label110" runat="server" Text="Other Details" meta:resourcekey="Label110Resource1"></asp:Label></td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="txtOtherSpecs"
                                runat="server" CssClass="multilinetext" EnableTheming="False" TextMode="MultiLine"
                                Width="613px" meta:resourcekey="txtOtherSpecsResource1"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" style="text-align: right">
                            <asp:Label ID="lblFOB" runat="server" Text="FOB" meta:resourcekey="lblFOBResource1"></asp:Label></td>
                        <td align="left">
                            <asp:TextBox
                                ID="txtFOB" runat="server" meta:resourcekey="txtFOBResource1"></asp:TextBox><cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                    TargetControlID="txtFOB" ValidChars=".0123456789" Enabled="True">
                                </cc1:FilteredTextBoxExtender>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblCIF" runat="server" Text="CIF" meta:resourcekey="lblCIFResource1"></asp:Label></td>
                        <td align="left">
                            <asp:TextBox ID="txtCIF" runat="server" meta:resourcekey="txtCIFResource1"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender
                                ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtCIF" ValidChars=".0123456789" Enabled="True">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="text-align: right">
                            <asp:Label ID="Label8" runat="server" Text="Total Ex-Works" Width="114px" meta:resourcekey="Label8Resource1"></asp:Label></td>
                        <td align="left">
                            <asp:TextBox ID="txtSubTotal" runat="server" meta:resourcekey="txtSubTotalResource1"></asp:TextBox></td>
                        <td align="right">
                            <asp:Label ID="Label14" runat="server" Text="Discount" Width="88px" meta:resourcekey="Label14Resource1"></asp:Label></td>
                        <td align="left">
                            <asp:TextBox ID="txtDisc" runat="server" meta:resourcekey="txtDiscResource1"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <br />
                            <table id="tblButtons" align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" meta:resourcekey="btnSaveResource1" /></td>
                                    <td>
                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" CausesValidation="False" meta:resourcekey="btnRefreshResource1" /></td>
                                    <td><asp:Button id="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" meta:resourcekey="btnCloseResource1" /></td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnPurchase" runat="server" Text="PO" OnClick="btnPurchase_Click" CausesValidation="False" meta:resourcekey="btnPurchaseResource1" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                &nbsp;&nbsp;
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" meta:resourcekey="ValidationSummary1Resource1"></asp:ValidationSummary>
                <asp:ValidationSummary ID="ValidationSummary2" runat="server"
                    ValidationGroup="qi" meta:resourcekey="ValidationSummary2Resource1"></asp:ValidationSummary>
            </td>
        </tr>
        <tr>
            <td style="height: 21px"></td>
            <td style="height: 21px"></td>
            <td style="height: 21px;"></td>
            <td style="height: 21px"></td>
        </tr>
    </table>
</asp:Content>


 
