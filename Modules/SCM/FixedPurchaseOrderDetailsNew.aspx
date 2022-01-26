<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/AdminMP1.master" AutoEventWireup="true" CodeFile="FixedPurchaseOrderDetailsNew.aspx.cs" Inherits="Modules_SCM_FixedPurchaseOrderDetailsNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
        function DueAmt() {
            var install = (document.getElementsById('<%=txtItemRate.ClientID %>').value);

            alert(install);
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
        document.getElementById('<%=txtNetAmount.ClientID %>').value = parseFloat(subtot) + parseFloat(tax * subtot / 100) - parseFloat(disc * subtot / 100) + parseFloat(cif) + parseFloat(fob) + parseFloat(ins) + parseFloat(paking);
    }
}




        $(function totalCalc() { });
        $("[id*=txtRate]").live("change", function () {
            if (isNaN(parseInt($(this).val()))) {
                $(this).val('0');
            } else {
                $(this).val(parseInt($(this).val()).toString());
            }
        });
        $("[id*=txtRate]").live("keyup", function () {
            if (!jQuery.trim($(this).val()) == '') {
                if (!isNaN(parseFloat($(this).val()))) {
                    var row = $(this).closest("tr");
                    $("[id*=lblAmount]", row).html(parseFloat($("[id*=lblQuantity]", row).html()) * parseFloat($(this).val()));
                }
            } else {
                $(this).val('');
            }
        }); 
            

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>

       
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>Purchase Order Details</td>
            <td></td>
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
                        <td colspan="5">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 20px;"></td>
                        <td style="text-align: left; height: 20px;">
                            <asp:RadioButton ID="rbByIndentApproval" runat="server" AutoPostBack="True" GroupName="s"
                                OnCheckedChanged="rbByIndentApproval_CheckedChanged" Text="By Indent" Width="97px" /><asp:RadioButton
                                    ID="rbByQuotation" runat="server" AutoPostBack="True" GroupName="s" OnCheckedChanged="rbByQuotation_CheckedChanged"
                                    Text="By Proforma Invoice" Width="183px" /></td>
                        <td style="text-align: right; height: 20px;"></td>
                        <td style="text-align: right; height: 20px;"></td>
                        <td style="text-align: left; height: 20px;"></td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            &nbsp;
                        </td>
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
                            </asp:TextBox>&nbsp;<asp:Image ID="imgPODate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender
                                Format="dd/MM/yyyy" ID="cePODate" runat="server" PopupButtonID="imgPODate" TargetControlID="txtPODate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meePODate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtPODate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>

                    
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblQuotNo" runat="server" Text="Indent No"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlQuotationNo" runat="server" OnSelectedIndexChanged="ddlQuotationNo_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblQuotDate" runat="server" Text="Indent Approval Date"></asp:Label></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtQuotationDate" runat="server" ReadOnly="True">
                            </asp:TextBox>
                            <cc1:MaskedEditExtender ID="meeQuotationDate" runat="server" DisplayMoney="Left"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtQuotationDate" UserDateFormat="MonthDayYear">
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
                            <asp:Label ID="lblPOType" runat="server" Text="Reference" Width="61px"></asp:Label></td>
                        <td style="text-align: right;"></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtSuppReference" runat="server">
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
                        <td style="text-align: right">
                            <asp:Label ID="Label25" runat="server" Text="Currency Type" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlCurrencyTypeForOrder" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCurrencyTypeForOrder_SelectedIndexChanged" Visible="False">
                            </asp:DropDownList><asp:Label ID="Label27" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                    ControlToValidate="ddlCurrencyTypeForOrder" ErrorMessage="Please Select the Currency Type" InitialValue="0" SetFocusOnError="True" Visible="False">*</asp:RequiredFieldValidator></td>
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
                        <td colspan="5" style="text-align: center; margin-left: 80px;">
                            <asp:GridView ID="gvQuotationItems" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvQuotationItems_RowDataBound" OnRowEditing="gvQuotationItems_RowEditing">
                                <Columns>
                                    <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Model No"></asp:BoundField>
                                    <asp:BoundField DataField="ModelName" HeaderText="Model Name"></asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                    <asp:BoundField DataField="Brand" HeaderText="Brand"></asp:BoundField>
                                    <asp:BoundField DataField="Curency" HeaderText="Currency"></asp:BoundField>
                                    <asp:BoundField DataField="Rate" HeaderText="Rate">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Amount"></asp:BoundField>
                                    <asp:BoundField DataField="Disc" HeaderText="Disc %"></asp:BoundField>
                                    <asp:BoundField DataField="SpRate" HeaderText="Special Amount"></asp:BoundField>
                                    <asp:BoundField DataField="Specification" HeaderText="Specifications"></asp:BoundField>
                                    <asp:BoundField DataField="CurencyId" HeaderText="CurrencyId"></asp:BoundField>
                                     <%--<asp:BoundField DataField="oldrate" HeaderText="Old Rate"></asp:BoundField>--%>
                                    <asp:BoundField DataField="Delivarydate" HeaderText="Delivary Date"></asp:BoundField>                                   
                                     <%--<asp:BoundField DataField="EnqNo" HeaderText="EnqNo"></asp:BoundField>--%>
                                    <asp:BoundField DataField="IndentId" HeaderText="IndentId"></asp:BoundField>
                                    <asp:BoundField DataField="IndentDetId" HeaderText="IndentDetId"></asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkQuot" runat="server" __designer:wfdid="w2"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:GridView ID="gvApprlItemDetails" runat="server" AutoGenerateColumns="False"
                                OnRowDataBound="gvApprlItemDetails_RowDataBound" OnRowEditing="gvApprlItemDetails_RowEditing">
                                <Columns>
                                    <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ModelNo" HeaderText="Model No">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Model Name"></asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <%--<asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                    </asp:BoundField>--%>
                                    <asp:TemplateField HeaderText="Quantity" ControlStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Rate" ControlStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRate" runat="server" AutoPostBack="true" OnTextChanged="txtRate_TextChanged"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Amount" ControlStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <%--<asp:BoundField HeaderText="Amount"></asp:BoundField>--%>
                                    <asp:BoundField DataField="Priority" HeaderText="Priority"></asp:BoundField>
                                    <asp:BoundField DataField="Brand" HeaderText="Brand"></asp:BoundField>
                                    <asp:BoundField DataField="SuggestedParty" HeaderText="Supplier Name">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                     <asp:TemplateField HeaderText="Disc%" ControlStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDiscount" runat="server" Text="0" AutoPostBack="true" OnTextChanged="txtDiscount_TextChanged"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Special Amount"  ControlStyle-Width="80px">
                                        <ItemTemplate>
                                            <%--<asp:TextBox ID="txtSplAmt" runat="server"></asp:TextBox>--%>
                                            <asp:Label ID="lblSplAmt" runat="server"></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ReqFor" HeaderText="Room">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HtmlEncode="False" DataFormatString="{0:/MM/dd/yyyy}" DataField="ReqDate" SortExpression="ReqDate" HeaderText="Required by Date"></asp:BoundField>
                                    <asp:BoundField DataField="Specification" HeaderText="Specification">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Priority" HeaderText="Priority"></asp:BoundField>
                                     <asp:BoundField DataField="IndentId" HeaderText="Indent Id"></asp:BoundField>
                                    <asp:BoundField DataField="IndentDetId" HeaderText="INdent Det Id"></asp:BoundField>

                                     <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkQuot" runat="server" __designer:wfdid="w2"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Button ID="btnGoIndent" runat="server" CausesValidation="False" Text="Go" OnClick="btnGoIndent_Click" Visible="False" />
                            <asp:Button ID="btnGo" runat="server" CausesValidation="False" Text="Go" OnClick="btnGo_Click" Visible="False" /></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="5">Item Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td colspan="4" style="text-align: left">
                            <asp:Label ID="lblCurrenctAlert" runat="server" EnableTheming="False" ForeColor="Red" Font-Bold="False" Font-Names="Verdana" Font-Size="8pt"></asp:Label>
                        </td>
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
                            <asp:TextBox ID="txtItemRate" runat="server" onkeyup="DueAmt()">
                            </asp:TextBox>
                            <asp:Label ID="Label4" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtItemRate"
                                ErrorMessage="Please Enter the Rate" ValidationGroup="ip">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                    ID="ftxteItemRate" runat="server" TargetControlID="txtItemRate" ValidChars=".0123456789">
                                </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label71" runat="server" Text="Special Price"></asp:Label></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtTotal" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 77px;">
                            <asp:Label ID="Label38" runat="server" Text="Dicount"></asp:Label></td>
                        <td style="text-align: left; height: 77px;">
                            <asp:TextBox ID="txtDisc" runat="server"></asp:TextBox></td>
                        <td style="text-align: right; height: 77px;">
                            <asp:Label ID="Label7" runat="server" Text="Delivery Date"></asp:Label></td>
                        <td style="text-align: right; height: 77px;"></td>
                        <td style="text-align: left; height: 77px;">
                            <asp:TextBox ID="txtDeliveryDate" runat="server" EnableTheming="True">
                            </asp:TextBox><asp:Image ID="imgItemDelDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtItemQuantity"
                                ErrorMessage="Please Enter the Delivery Date" ValidationGroup="ip">*</asp:RequiredFieldValidator>
                            <cc1:CalendarExtender
                                Format="dd/MM/yyyy" ID="CalendarExtender1" runat="server" PopupButtonID="imgItemDelDate"
                                TargetControlID="txtDeliveryDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" DisplayMoney="Left"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtDeliveryDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="Label20" runat="server" Text="Specifications"></asp:Label></td>
                        <td style="height: 21px; text-align: left" colspan="4">
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
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label29" runat="server" Text="ItemImage"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:Image ID="Image1" runat="server" Height="85px" ImageUrl="~/Images/noimage400x300.gif"
                                Width="140px"></asp:Image></td>
                        <td style="height: 19px; text-align: left"></td>
                        <td style="height: 19px; text-align: left"></td>
                        <td style="height: 19px; text-align: left"></td>
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
                                    <asp:BoundField HtmlEncode="False" DataFormatString="{0:MM/dd/yyyy}" DataField="DeliveryDate" HeaderText="Delivery Date"></asp:BoundField>
                                    <asp:BoundField DataField="Specifications" NullDisplayText="-" HeaderText="Specifications"></asp:BoundField>
                                    <asp:BoundField DataField="Remarks" NullDisplayText="-" HeaderText="Remarks"></asp:BoundField>
                                    <asp:BoundField DataField="ItemTypeId" HeaderText="ItemTypeIdHidden"></asp:BoundField>
                                    <asp:BoundField DataField="IndentId" HeaderText="IndentId"></asp:BoundField>
                                    <asp:BoundField DataField="IndentDetId" HeaderText="IndentDetId"></asp:BoundField>


                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="5">Terms &amp; conditions</td>
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
                            </asp:TextBox></td>
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
                            </asp:TextBox><asp:Label ID="Label26" runat="server" Text="%" Width="17px"></asp:Label>
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
                            <asp:DropDownList ID="ddlCurrencyType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCurrencyType_SelectedIndexChanged">
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
                                       </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 23px"></td>
            <td style="height: 23px"></td>
            <td style="height: 23px"></td>
            <td  style="height: 23px">
                <asp:Label ID="Label6" runat="server" Text="Tax" Width="37px" Visible="False"></asp:Label><asp:TextBox
                    ID="txtTax" runat="server" Visible="False"></asp:TextBox><asp:Label ID="Label8" runat="server"
                        Text="%" Visible="False"></asp:Label>
                &nbsp;
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="ip"></asp:ValidationSummary>
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                    ShowSummary="False"></asp:ValidationSummary>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"
                    runat="server" TargetControlID="txtTax" ValidChars=".0123456789">
                </cc1:FilteredTextBoxExtender>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                    Visible="False"></asp:Label><asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                    Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
             </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


 
