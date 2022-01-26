<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/PurchaseMP1.master" AutoEventWireup="true" CodeFile="SelfPurchaseOrderDetailsNew.aspx.cs" Inherits="Modules_SCM_SelfPurchaseOrderDetailsNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <script>
            $(function () {
                $("[name$='txtPODate'],[name$='txtDeliveryDate'],[name$='txtPIDate'],[name$='txtPIDate'],[name$='txtPIDate']").datepicker();
            });
    </script>
    <script type="text/javascript">
        function amtcalc() {
            var req_qty, rate, disc;
            req_qty = document.getElementById('<%=txtItemQuantity.ClientID %>').value;
    rate = document.getElementById('<%=txtItemRate.ClientID %>').value;
    disc = document.getElementById('<%=txtDisc.ClientID %>').value;
    if (req_qty == "" || req_qty == "0") {
        document.getElementById('<%=txtTotal.ClientID %>').value = "0";
    }
    else if (rate == "" || rate == "0") {
        document.getElementById('<%=txtTotal.ClientID %>').value = "0";
    }
    else if (rate > 0 && req_qty > 0) {
        document.getElementById('<%=txtTotal.ClientID %>').value = (rate * req_qty);
    }
    if (disc != "" && disc != "0") {
        document.getElementById('<%=txtTotal.ClientID %>').value = parseFloat((rate * req_qty)) - parseFloat((((rate * req_qty) * disc) / 100));
     }
 }

 function netamtcalc() {
     var disc, subtot, tax, cif, fob, ins, paking;
     try { tax = document.getElementById('<%=txtCSTTax.ClientID %>').value; } catch (e) { tax = "0"; }
        try { cif = document.getElementById('<%=txtCIF.ClientID %>').value; } catch (e) { cif = "0"; }
    try { fob = document.getElementById('<%=txtFOB.ClientID %>').value; } catch (e) { fob = "0"; }
    disc = document.getElementById('<%=txtDiscount.ClientID %>').value;
    subtot = document.getElementById('<%=txtSubTotal.ClientID %>').value;
    ins = document.getElementById('<%=txtInsurance.ClientID %>').value;
    paking = document.getElementById('<%=txtFreight.ClientID %>').value;
    if (disc == "" || disc == "0" || isNaN(disc)) { disc = "0"; }
    if (tax == "" || tax == "0" || isNaN(tax)) { tax = "0"; }
    if (ins == "" || ins == "0" || isNaN(ins)) { ins = "0"; }
    if (paking == "" || paking == "0" || isNaN(paking)) { paking = "0"; }
    if (subtot > 0) {
        document.getElementById('<%=txtNetAmount.ClientID %>').value = parseFloat(subtot) + parseFloat(tax * subtot / 100) - parseFloat(disc * subtot / 100) + parseFloat(cif) + parseFloat(fob) + parseFloat(ins * subtot / 100) + parseFloat(paking);
    }
}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>Self Purchase Order Details</td>
            <td>
                 <asp:Label id="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label><asp:Label id="lblEmpIdHidden" runat="server" Visible="False"></asp:Label><asp:Label
                                    id="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                    Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
    <table style="width:100%">
        <tr>
            <td colspan="4">
                <table border="0" cellpadding="0" cellspacing="0" id="tblFixedPODetails" runat="server"
                    visible="true" width="100%">
                   
                  
                    <tr>
                        <td colspan="5" style="text-align: left" class="profilehead">General Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblPONo" runat="server" Text="PO No"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPONo" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblQuotationDate" runat="server" Text="PO Date"></asp:Label></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPODate" runat="server">
                            </asp:TextBox>&nbsp;
                           
                            <cc1:MaskedEditExtender ID="meePODate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtPODate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblSupplierName" runat="server" Text="Supplier Name"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlSupplierName" runat="server" Enabled="False" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlSupplierName_SelectedIndexChanged">
                            </asp:DropDownList><asp:Label ID="Label28" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                    ControlToValidate="ddlSupplierName" ErrorMessage="Please Select the Supplier Name" InitialValue="0" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblPOType" runat="server" Text="Customer Code" Width="101px"></asp:Label></td>
                        <td style="text-align: right;"></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtCustomerCode" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 11px;">
                            <asp:Label ID="Label1" runat="server" Text="Delivery Type"></asp:Label></td>
                        <td style="text-align: left; height: 11px;">
                            <asp:DropDownList ID="ddlDespatchMode" runat="server">
                            </asp:DropDownList></td>
                        <td style="text-align: right; height: 11px;">
                            <asp:Label ID="Label2" runat="server" Text="Contact Person"></asp:Label></td>
                        <td style="text-align: right; height: 11px;"></td>
                        <td style="text-align: left; height: 11px;">
                            <asp:TextBox ID="txtSuppliersContactPerson" runat="server">
                            </asp:TextBox><asp:DropDownList ID="ddlContactPerson" runat="server" Visible="False">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 11px; text-align: right">
                            <asp:Label ID="Label39" runat="server" Text="Company :"></asp:Label></td>
                        <td style="height: 11px; text-align: left">
                            <asp:DropDownList ID="ddlCompany" runat="server">
                            </asp:DropDownList></td>
                        <td style="height: 11px; text-align: right">
                            <asp:Label ID="Label37" runat="server" Text="Email :"></asp:Label></td>
                        <td style="height: 11px; text-align: right"></td>
                        <td style="height: 11px; text-align: left">
                            <asp:TextBox ID="txtSupEmail" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label40" runat="server" Text="Delivery Address :"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlDeliveryAddress" runat="server">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label25" runat="server" Text="Currency Type" Visible="False"></asp:Label></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlCurrencyTypeForOrder" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCurrencyTypeForOrder_SelectedIndexChanged" Visible="False">
                            </asp:DropDownList><asp:Label ID="Label27" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                    ControlToValidate="ddlCurrencyTypeForOrder" ErrorMessage="Please Select the Currency Type" InitialValue="0" SetFocusOnError="True" Visible="False">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label42" runat="server" Text="Invoice Address :"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlInvoiceAddress" runat="server">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label43" runat="server" Text="Reference" Width="61px"></asp:Label></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtSuppReference" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="5">Item Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left">
                            <asp:Label ID="lblCurrenctAlert" runat="server" EnableTheming="False" ForeColor="Red" Font-Bold="False" Font-Names="Verdana" Font-Size="8pt"></asp:Label></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label44" runat="server" Text="Select Brand"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label36" runat="server" Text="Search by Model No"></asp:Label></td>
                        <td colspan="4" style="text-align: left">&nbsp;<asp:TextBox ID="txtSearchModel" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtSearchModel"
                            ErrorMessage="Please Enter ModelNo For Search" ValidationGroup="Search">*</asp:RequiredFieldValidator><asp:Button
                                ID="btnSearchModelNo" runat="server" BorderStyle="None" CausesValidation="False"
                                CssClass="gobutton" EnableTheming="False" OnClick="btnSearchModelNo_Click" Text="Go"
                                ValidationGroup="Search" /></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 36px;">
                            <asp:Label ID="Label11" runat="server" Text="Model No"></asp:Label></td>
                        <td style="text-align: left; height: 36px;">
                            <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged">
                            </asp:DropDownList><asp:Label ID="Label32" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                    ControlToValidate="ddlItemType" ErrorMessage="Please Select the Model No" InitialValue="0"
                                    ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right; height: 36px;">
                            <asp:Label ID="Label3" runat="server" Text="Model Name" Width="90px"></asp:Label></td>
                        <td style="text-align: right; height: 36px;"></td>
                        <td style="text-align: left; height: 36px;">
                            <asp:TextBox ID="txtModelName" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label ID="Label30" runat="server" Text="Item Category"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                            <asp:TextBox ID="txtItemCategory" runat="server">
                            </asp:TextBox></td>
                        <td style="height: 24px; text-align: right">
                            <asp:Label ID="Label31" runat="server" Text="ItemSubCategory"></asp:Label></td>
                        <td style="height: 24px; text-align: right"></td>
                        <td style="height: 24px; text-align: left">
                            <asp:TextBox ID="txtItemSubCategory" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label33" runat="server" Text="Color"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlcolor" runat="server">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label34" runat="server" Text="Brand"></asp:Label></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtBrand" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label22" runat="server" Text="UOM"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtItemUOM" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label23" runat="server" Text="Quantity"></asp:Label></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtItemQuantity" runat="server">
                            </asp:TextBox><asp:Label ID="Label5" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtItemQuantity"
                                    ErrorMessage="Please Enter the Quantity" ValidationGroup="ip">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                        ID="ftxtQuantity" runat="server" FilterType="Numbers" TargetControlID="txtItemQuantity">
                                    </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label24" runat="server" Text="Rate"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtItemRate" runat="server">
                            </asp:TextBox>
                            <asp:Label ID="Label4" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtItemRate"
                                ErrorMessage="Please Enter the Rate" ValidationGroup="ip">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                    ID="ftxteItemRate" runat="server" TargetControlID="txtItemRate" ValidChars=".0123456789">
                                </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label38" runat="server" Text="Discount"></asp:Label></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left">&nbsp;<asp:TextBox ID="txtDisc" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 77px;">&nbsp;<asp:Label ID="Label71" runat="server" Text="Special Price"></asp:Label></td>
                        <td style="text-align: left; height: 77px;">
                            <asp:TextBox ID="txtTotal" runat="server"></asp:TextBox></td>
                        <td style="text-align: right; height: 77px;">
                            <asp:Label ID="Label7" runat="server" Text="Delivery Date"></asp:Label></td>
                        <td style="text-align: right; height: 77px;"></td>
                        <td style="text-align: left; height: 77px;">
                            <asp:TextBox ID="txtDeliveryDate" runat="server" EnableTheming="True">
                            </asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtItemQuantity"
                                ErrorMessage="Please Enter the Delivery Date" ValidationGroup="ip">*</asp:RequiredFieldValidator>
                            
                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" DisplayMoney="Left"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtDeliveryDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label20" runat="server" Text="Specifications"></asp:Label></td>
                        <td style="text-align: left" colspan="4">
                            <asp:TextBox ID="txtItemSpecifications" runat="server" TextMode="MultiLine" EnableTheming="False"
                                Width="846px" CssClass="multilinetext" Height="80px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label21" runat="server" Text="Remarks"></asp:Label></td>
                        <td style="text-align: left" colspan="4">
                            <asp:TextBox ID="txtItemRemarks" runat="server" TextMode="MultiLine" EnableTheming="False"
                                Width="850px" CssClass="multilinetext" Height="84px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label29" runat="server" Text="ItemImage"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:Image ID="Image1" runat="server" Height="85px" ImageUrl="~/Images/noimage400x300.gif"
                                Width="140px"></asp:Image></td>
                        <td align="right">
                            <asp:Label ID="Label35" runat="server" Text="Customer Name"></asp:Label></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCustomerName" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                        <td style="height: 19px; text-align: left"></td>
                        <td style="height: 19px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: right">
                            <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" OnClick="btnAdd_Click" Text="Add"
                                ValidationGroup="ip" /></td>
                        <td style="text-align: left">
                            <asp:Button ID="btnRefreshItems" runat="server" BackColor="Transparent" BorderStyle="None"
                                CausesValidation="False" CssClass="loginbutton" EnableTheming="False" OnClick="btnItemRefresh_Click"
                                Text="Refresh" /></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:GridView ID="gvPOItems" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvPOItems_RowDeleting"
                                Width="100%" OnRowDataBound="gvPOItems_RowDataBound" OnRowEditing="gvPOItems_RowEditing" ShowFooter="True">
                                <FooterStyle BackColor="#1AA8BE" />
                                <Columns>
                                    <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                    <asp:BoundField DataField="ItemType" HeaderText="Model No"></asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Model Name"></asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                    <asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
                                    <asp:BoundField DataField="Tax" HeaderText="Tax"></asp:BoundField>
                                    <asp:BoundField HeaderText="Amount"></asp:BoundField>
                                    <asp:BoundField DataField="Disc" HeaderText="Disc %"></asp:BoundField>
                                    <asp:BoundField DataField="SpPrice" HeaderText="Special Amount"></asp:BoundField>
                                    <asp:BoundField HtmlEncode="False" DataFormatString="{0:MM/dd/yyyy}" DataField="DeliveryDate" HeaderText="Exp Arrival Date"></asp:BoundField>
                                    <asp:BoundField DataField="Specifications" NullDisplayText="-" HeaderText="Specifications"></asp:BoundField>
                                    <asp:BoundField HtmlEncode="False" DataField="Remarks" ConvertEmptyStringToNull="False" NullDisplayText="-" HeaderText="Remarks"></asp:BoundField>
                                    <asp:BoundField DataField="ItemTypeId" HeaderText="ItemTypeIdHidden"></asp:BoundField>
                                    <asp:BoundField DataField="Customer" HeaderText="Customer"></asp:BoundField>
                                    <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="5" style="height: 20px">Terms &amp; conditions</td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td colspan="4" style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label13" runat="server" Text="Terms & Conditions"></asp:Label></td>
                        <td colspan="4" style="text-align: left">
                            <asp:TextBox ID="txtTermsConditions" runat="server" EnableTheming="False" Height="42px"
                                TextMode="MultiLine" Width="762px" CssClass="multilinetext"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="Label17" runat="server" Text="Terms Of Delivery"></asp:Label></td>
                        <td style="text-align: left;" colspan="4">
                            <asp:TextBox ID="txtTermsOfDelivery" runat="server" CssClass="multilinetext" EnableTheming="False"
                                Height="42px" TextMode="MultiLine" Width="762px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label14" runat="server" Text="Destination"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtDestination" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="5" style="height: 20px">Payment terms</td>
                    </tr>
                    <tr>
                        <td colspan="5" style="height: 19px; text-align: left">
                            <asp:CheckBoxList ID="chklIndigenous" runat="server" Width="100%" Visible="False">
                                <asp:ListItem>100% along with PO</asp:ListItem>
                                <asp:ListItem>100% against delivery</asp:ListItem>
                                <asp:ListItem>PDC</asp:ListItem>
                            </asp:CheckBoxList>
                            <asp:CheckBoxList ID="chklForeign" runat="server" Width="100%" Visible="False">
                                <asp:ListItem>75% wire transfer against Pro-forma Invoice and 25% against Delivery</asp:ListItem>
                                <asp:ListItem>100% wire transfer against Pro-forma Invoice</asp:ListItem>
                                <asp:ListItem>100% wire transfer against Delivery</asp:ListItem>
                            </asp:CheckBoxList>
                            <asp:Label ID="Label9" runat="server" Text="Remitance in(%)"></asp:Label>
                            <asp:TextBox ID="txtRemitance" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="5">Payments</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label16" runat="server" Text="Packing Charges" Width="123px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtFreight" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label15" runat="server" Text="Insurance"></asp:Label></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtInsurance" runat="server">
                            </asp:TextBox><asp:Label ID="Label41" runat="server" Text="%" Width="17px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label18" runat="server" Text="Total Ex-Works"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtSubTotal" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label19" runat="server" Text="Discount" Width="88px"></asp:Label>
                        </td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDiscount" runat="server">
                            </asp:TextBox><asp:Label ID="Label26" runat="server" Text="%" Width="17px"></asp:Label><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDiscount"
                                ErrorMessage="Please Enter Only Numbers" ValidationExpression="[0-9]*$">*</asp:RegularExpressionValidator>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                TargetControlID="txtDiscount">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblFOB" runat="server" Text="FOB Charges" Width="88px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtFOB" runat="server">
                            </asp:TextBox><cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers"
                                TargetControlID="txtFOB">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="lblCIF" runat="server" Text="CIF Charges" Width="88px"></asp:Label></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCIF" runat="server">
                            </asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers"
                                TargetControlID="txtCIF">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblTaxCST" runat="server" Text="Taxes -CST" Width="88px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCSTTax" runat="server">
                            </asp:TextBox><asp:Label ID="lblTaxCSTPercent" runat="server" Text="%" Width="17px"></asp:Label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCSTTax"
                                ErrorMessage="Please Enter Only Numbers" ValidationExpression="[0-9]*$">*</asp:RegularExpressionValidator>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="NUMBERS"
                                TargetControlID="txtCSTTax">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="lblNetAmount" runat="server" Text="Net Amount" Width="88px"></asp:Label></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtNetAmount" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblAmountInWords" runat="server" Text="Amount In Words" Width="119px"
                                Visible="False"></asp:Label></td>
                        <td colspan="4" style="text-align: left">
                            <asp:TextBox ID="txtAmountInWords" runat="server" Width="90%" CssClass="textbox"
                                EnableTheming="False" ReadOnly="True" Visible="False">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 21px;">
                            <asp:Label ID="Label10" runat="server" Text="Currency Type"></asp:Label></td>
                        <td style="height: 21px; text-align: left;">
                            <asp:DropDownList ID="ddlCurrencyType" runat="server" OnSelectedIndexChanged="ddlCurrencyType_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="height: 21px; text-align: right;">
                            <asp:Label ID="lblOtherCurrencyValue" runat="server" Text="Net Amount In Other Currency"
                                Width="196px" Visible="False"></asp:Label></td>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left;">
                            <asp:TextBox ID="txtNetAmtInOtherCurrency" runat="server" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="5" style="text-align: left">Reference Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                        <td style="height: 19px; text-align: right"></td>
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
                        <td style="text-align: right"></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlApprovedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <table id="tblButtons" align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnApprove" runat="server" CausesValidation="False" OnClick="btnApprove_Click"
                                            Text="Approve" /></td>
                                    <td>
                                        <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" OnClick="btnRefresh_Click"
                                            Text="Refresh" /></td>
                                    <td>
                                        <asp:Button ID="btnClose" runat="server" CausesValidation="False" OnClick="btnClose_Click"
                                            Text="Close" />
                                        <asp:Button ID="btnSend" runat="server" CausesValidation="False" OnClick="btnSend_Click"
                                            Text="Send E-Mail" />
                                        <asp:Button ID="btnSendImEmail" runat="server" CausesValidation="False" OnClick="btnSendImEmail_Click"
                                            Text="Send Import E-Mail" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 21px"></td>
            <td style="height: 21px"></td>
            <td style="height: 21px"></td>
            <td width="750" style="height: 21px">
                <asp:Label ID="Label6" runat="server" Text="Tax" Width="37px" Visible="False"></asp:Label><asp:TextBox
                    ID="txtTax" runat="server" Visible="False"></asp:TextBox><asp:Label ID="Label8" runat="server"
                        Text="%" Visible="False"></asp:Label>
                &nbsp;
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="ip"></asp:ValidationSummary>
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                    ShowSummary="False"></asp:ValidationSummary>
                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="Search"></asp:ValidationSummary>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"
                    runat="server" TargetControlID="txtTax" ValidChars=".0123456789">
                </cc1:FilteredTextBoxExtender>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter PropertyName="SelectedValue" Type="Int64" DefaultValue="0" Name="BranbId" ControlID="ddlBrand"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtSearchModel"></asp:ControlParameter>
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>


 
