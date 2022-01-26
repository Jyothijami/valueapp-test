<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CashInvoiceNew1.aspx.cs" Inherits="Modules_Inventory_CashInvoiceNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function amtcalc() {
            var req_qty, rate;
            req_qty = document.getElementById('<%=txtQuantity.ClientID %>').value;
            rate = document.getElementById('<%=txtRate.ClientID %>').value;


            if (req_qty == "" || req_qty == "0") {
                document.getElementById('<%=txtAmount.ClientID %>').value = "0";
            }
            else if (rate == "" || rate == "0") {
                document.getElementById('<%=txtAmount.ClientID %>').value = "0";
            }
            else if (rate > 0 && req_qty > 0) {
                document.getElementById('<%=txtAmount.ClientID %>').value = (rate * req_qty);

    }

}

function grosscalc() {
    var vat, cst, disc, grossamt, misc, transchargs, TOTAL, bt;
    bt = document.getElementById('<%=txtBranchTransfer.ClientID %>').value;
    cst = document.getElementById('<%=txtCST.ClientID %>').value;
    vat = document.getElementById('<%=txtVAT.ClientID %>').value;
    misc = parseFloat(document.getElementById('<%=txtMiscelleneous.ClientID %>').value);
    disc = parseFloat(document.getElementById('<%=txtDiscount.ClientID %>').value);
    grossamt = parseFloat(document.getElementById('<%=txtGrossTotalAmtHidden.ClientID %>').value);
    if (bt == "" || bt == "0" || isNaN(bt)) { bt = "0"; }
    if (cst == "" || cst == "0" || isNaN(cst)) { cst = "0"; }
    if (vat == "" || vat == "0" || isNaN(vat)) { vat = "0"; }
    if (grossamt == "" || grossamt == "0" || isNaN(grossamt)) { grossamt = "0"; }
    if (misc == "" || misc == "0" || isNaN(misc)) { misc = "0"; }
    if (disc == "" || disc == "0" || isNaN(disc)) { disc = "0"; }
    TOTAL = parseFloat(grossamt);
    TOTAL = TOTAL + ((vat * TOTAL) / 100);
    TOTAL = TOTAL + ((cst * TOTAL) / 100);
    TOTAL = TOTAL + ((bt * TOTAL) / 100);
    TOTAL = TOTAL + parseFloat(misc);
    TOTAL = TOTAL - ((disc * TOTAL) / 100);
    document.getElementById('<%=txtGrossAmount.ClientID %>').value = parseFloat(TOTAL);
        }

        function rbVATCSTEnableDisable() {
            if (document.getElementById('<%=rbVAT.ClientID %>').checked == true) {
        document.getElementById('<%=txtVAT.ClientID %>').style.display = document.getElementById('<%=lblVAT.ClientID %>').style.display = "";
        document.getElementById('<%=txtCST.ClientID %>').style.display = document.getElementById('<%=lblCSTax.ClientID %>').style.display = "none";
        document.getElementById('<%=txtBranchTransfer.ClientID %>').style.display = document.getElementById('<%=lblBranchTransfer.ClientID %>').style.display = "none";
        document.getElementById('<%=txtVAT.ClientID %>').focus();
    }
    if (document.getElementById('<%=rbCST.ClientID %>').checked == true) {
        document.getElementById('<%=txtVAT.ClientID %>').style.display = document.getElementById('<%=lblVAT.ClientID %>').style.display = "none";
           document.getElementById('<%=txtCST.ClientID %>').style.display = document.getElementById('<%=lblCSTax.ClientID %>').style.display = "";
           document.getElementById('<%=txtBranchTransfer.ClientID %>').style.display = document.getElementById('<%=lblBranchTransfer.ClientID %>').style.display = "none";
           document.getElementById('<%=txtCST.ClientID %>').focus();
       }
       if (document.getElementById('<%=rbBranchTransfer.ClientID %>').checked == true) {
        document.getElementById('<%=txtVAT.ClientID %>').style.display = document.getElementById('<%=lblVAT.ClientID %>').style.display = "none";
            document.getElementById('<%=txtCST.ClientID %>').style.display = document.getElementById('<%=lblCSTax.ClientID %>').style.display = "none";
            document.getElementById('<%=txtBranchTransfer.ClientID %>').style.display = document.getElementById('<%=lblBranchTransfer.ClientID %>').style.display = "";
            document.getElementById('<%=txtBranchTransfer.ClientID %>').focus();
        }
        document.getElementById('<%=txtVAT.ClientID %>').value = "";
    document.getElementById('<%=txtCST.ClientID %>').value = "";
    document.getElementById('<%=txtBranchTransfer.ClientID %>').value = "";
    grosscalc();
}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>Cash &amp; Sample
                Invoice</td>
            <td>
                <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                    Visible="False"></asp:Label>
                <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                    Visible="False"></asp:Label><asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label><asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                    <td colspan="4" rowspan="3">
                        <table border="0" cellpadding="0" cellspacing="0" id="tblSIDetails" runat="server"
                            visible="true" width="100%">
                            <tr>
                                <td colspan="4" style="text-align: left" class="profilehead">General Details</td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label1" runat="server" Text="Delivery Challan No" Width="127px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlDeviveryNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDeviveryNo_SelectedIndexChanged">
                                    </asp:DropDownList><asp:Label ID="Label26" runat="server" EnableTheming="False" Font-Bold="False"
                                        Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlDeviveryNo"
                                            ErrorMessage="Please Select the Delivery Challan No" InitialValue="0">*</asp:RequiredFieldValidator></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label2" runat="server" Text="Delivery Challan Date" Width="146px"></asp:Label></td>
                                <td style="text-align: left; width: 423px;">
                                    <asp:TextBox ID="txtChallanDate" runat="server" ReadOnly="True">
                                    </asp:TextBox><cc1:CalendarExtender
                                        Format="dd/MM/yyyy" ID="CeChallanDate" runat="server" Enabled="False" PopupButtonID="imgChallanDate"
                                        TargetControlID="txtChallanDate">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="MeeChallanDate" runat="server" DisplayMoney="Left" Enabled="True"
                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtChallanDate" UserDateFormat="MonthDayYear">
                                    </cc1:MaskedEditExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblCustomer" runat="server" Text="Customer Name"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtCustomerName" runat="server" ReadOnly="True"></asp:TextBox></td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblRegion" runat="server" Text="Region"></asp:Label></td>
                                <td style="text-align: left; width: 423px;">
                                    <asp:TextBox ID="txtRegion" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" ReadOnly="True">
                                    </asp:TextBox></td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblPhone" runat="server" Text="Phone"></asp:Label></td>
                                <td style="text-align: left; width: 423px;">
                                    <asp:TextBox ID="txtPhone" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblMobile" runat="server" Text="Mobile"></asp:Label></td>
                                <td style="text-align: left; width: 423px;">
                                    <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label35" runat="server" Text="UnitName"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlunitname" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlunitname_SelectedIndexChanged">
                                    </asp:DropDownList></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label36" runat="server" Text="UnitName"></asp:Label></td>
                                <td style="text-align: left; width: 423px;">
                                    <asp:TextBox ID="txtUnitaddress" runat="server" TextMode="MultiLine">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center"></td>
                            </tr>
                            <tr>
                                <td class="profilehead" colspan="4" style="text-align: left">Delivery Challan Extra Items</td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <asp:GridView ID="gvExtraItems" runat="server" AutoGenerateColumns="False"
                                        Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="DC No" HeaderText="DC No"></asp:BoundField>
                                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                            <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                                            <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                            <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <span style="color: #ff0033">No Data to Dispaly</span>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td class="profilehead" colspan="4" style="text-align: left">invoice Details</td>
                            </tr>
                            <tr>
                                <td style="text-align: right;"></td>
                                <td style="text-align: left;"></td>
                                <td style="text-align: right;"></td>
                                <td style="text-align: left; width: 423px;"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label3" runat="server" Text="Invoice No"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtInvoiceNo" runat="server" ReadOnly="True">
                                    </asp:TextBox><asp:Label ID="Label12" runat="server" EnableTheming="False" Font-Bold="False"
                                        Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                            ID="rfvInvoiceNo" runat="server" ControlToValidate="txtInvoiceNo" ErrorMessage="Please Enter the Delivery Challan No">*</asp:RequiredFieldValidator></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label6" runat="server" Text="Invoice Date"></asp:Label></td>
                                <td style="text-align: left; width: 423px;">
                                    <asp:TextBox ID="txtInvoiceDate" runat="server" type="date">
                                    </asp:TextBox>&nbsp;<asp:Label ID="Label15" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvInvoiceDate" runat="server" ControlToValidate="txtInvoiceDate"
                                        ErrorMessage="Please Enter the Delivery Challan No">*</asp:RequiredFieldValidator>
                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="DateCustomValidate"
                                        ControlToValidate="txtInvoiceDate" ErrorMessage="Please Enter the Invoice Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                        SetFocusOnError="True">*</asp:CustomValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label5" runat="server" Text="Invoice Type"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlInvoiceType" runat="server">
                                        <asp:ListItem Value="0">--</asp:ListItem>
                                        <asp:ListItem>Tax Invoice</asp:ListItem>
                                        <asp:ListItem>Pro-Forma Tax Invoice</asp:ListItem>
                                        <asp:ListItem>Branch Transfer</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="Label13" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlInvoiceType"
                                        ErrorMessage="Please Select the Invoice Type">*</asp:RequiredFieldValidator></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label8" runat="server" Text="Delivery Type"></asp:Label></td>
                                <td style="text-align: left; width: 423px;">
                                    <asp:DropDownList ID="ddlDeliveryType" runat="server">
                                    </asp:DropDownList><asp:Label ID="Label22" runat="server" EnableTheming="False" Font-Bold="False"
                                        Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlDeliveryType"
                                        ErrorMessage="Please Select the Delivery Type " InitialValue="0">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td style="text-align: right" align="right">
                                    <asp:Label ID="Label16" Visible="false" runat="server" Text="InvoiceNo"></asp:Label>
                                    &nbsp;</td>
                                <td style="text-align: left">&nbsp;<asp:TextBox ID="txtInvo" runat="server" Visible="False">0</asp:TextBox></td>
                                <td style="text-align: right"></td>
                                <td style="text-align: left; width: 423px;">&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: left" class="profilehead">Items Details</td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: right"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label4" runat="server" Text="Model No :"></asp:Label></td>
                                <td style="text-align: left;">&nbsp;<asp:DropDownList ID="ddlModelNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModelNo_SelectedIndexChanged"></asp:DropDownList></td>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label7" runat="server" Text="Item Name" Width="76px"></asp:Label></td>
                                <td style="text-align: left; width: 423px;">&nbsp;<asp:TextBox ID="txtItemname" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label27" runat="server" Text="UOM"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtItemUOM" runat="server" ReadOnly="True"></asp:TextBox></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label59" runat="server" Text="Color :"></asp:Label></td>
                                <td style="text-align: left; width: 423px;">
                                    <asp:DropDownList ID="ddlColor" runat="server" AutoPostBack="True">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label23" runat="server" Text="Item Specification"></asp:Label></td>
                                <td colspan="3" style="text-align: left">
                                    <asp:TextBox ID="txtItemSpec" runat="server" CssClass="multilinetext" EnableTheming="False"
                                        ReadOnly="True" TextMode="MultiLine" Width="90%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblRate" runat="server" Text="Rate"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtRate" runat="server"></asp:TextBox>
                                    <asp:Label ID="Label19" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtRate"
                                        ErrorMessage="Please Enter the Rate" ValidationGroup="id">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                            ID="ftxteRate" runat="server" TargetControlID="txtRate" ValidChars=".0123456789" Enabled="False">
                                        </cc1:FilteredTextBoxExtender>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label></td>
                                <td style="text-align: left; width: 423px;">
                                    <asp:TextBox ID="txtQuantity" runat="server" Width="139px"></asp:TextBox>
                                    <asp:Label ID="Label18" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtQuantity"
                                        ErrorMessage="Please Enter the Quantity" ValidationGroup="id">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                            ID="ftxteQuantity" runat="server" FilterType="Numbers" TargetControlID="txtQuantity"
                                            ValidChars="." Enabled="False">
                                        </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 24px;">
                                    <asp:Label ID="lblAmount" runat="server" Text="Amount"></asp:Label></td>
                                <td style="text-align: left; height: 24px;">
                                    <asp:TextBox ID="txtAmount" runat="server" ReadOnly="True"></asp:TextBox></td>
                                <td style="text-align: right; height: 24px;">
                                    <asp:Label ID="Label14" runat="server" Text="Remarks"></asp:Label>
                                </td>
                                <td style="text-align: left; width: 423px; height: 24px;">
                                    <asp:TextBox ID="txtRemarks1" runat="server" TextMode="MultiLine">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                                <td style="text-align: right"></td>
                                <td style="text-align: left; width: 423px;"></td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                        CssClass="loginbutton" EnableTheming="False" Text="Add" OnClick="btnAdd_Click"
                                        ValidationGroup="id" /><asp:Button ID="btnItemRefresh" runat="server" BackColor="Transparent"
                                            BorderStyle="None" CssClass="loginbutton" EnableTheming="False" OnClick="btnItemRefresh_Click"
                                            Text="Refresh" CausesValidation="False" /></td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <asp:GridView ID="gvItmDetails" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvItmDetails_RowDeleting"
                                        OnRowDataBound="gvItmDetails_RowDataBound" OnRowEditing="gvItmDetails_RowEditing" Width="100%">
                                        <Columns>
                                            <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                            <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                            <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                                            <asp:BoundField DataField="ItemName" HeaderText="Item Name">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="UOM" HeaderText="UOM">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                            <asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
                                            <asp:BoundField DataField="Vat" HeaderText="Vat"></asp:BoundField>
                                            <asp:BoundField DataField="CST" HeaderText="CST"></asp:BoundField>
                                            <asp:BoundField DataField="Excise" HeaderText="Excise"></asp:BoundField>
                                            <asp:BoundField HeaderText="Amount"></asp:BoundField>
                                            <asp:BoundField DataField="ColorId" HeaderText="ColorId"></asp:BoundField>
                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
                                            <asp:BoundField DataField="DcId" HeaderText="DCId"></asp:BoundField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <span style="color: #ff0033">No Data to Dispaly</span>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td class="profilehead" colspan="4">other charges</td>
                            </tr>
                            <tr>
                                <td style="height: 19px; text-align: right"></td>
                                <td style="height: 19px; text-align: left"></td>
                                <td style="height: 19px; text-align: right"></td>
                                <td style="height: 19px; text-align: left; width: 423px;"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;"></td>
                                <td style="text-align: left;"></td>
                                <td style="text-align: right;"></td>
                                <td style="text-align: left; width: 423px;">
                                    <asp:RadioButton ID="rbVAT" runat="server" Checked="True" GroupName="vatcst" Text="VAT" /><asp:RadioButton
                                        ID="rbCST" runat="server" GroupName="vatcst" Text="C.S. Tax" />
                                    <asp:RadioButton ID="rbBranchTransfer" runat="server" GroupName="vatcst" Text="Branch Transfer" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label24" runat="server" Text="Total"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtTotalAmt" runat="server" ReadOnly="True"></asp:TextBox><cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtMiscelleneous"
                                        ValidChars=".0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblBranchTransfer" runat="server" Text="Branch Transfer"></asp:Label>
                                    <asp:Label ID="lblVAT" runat="server" Text="VAT"></asp:Label><asp:Label ID="lblCSTax"
                                        runat="server" Text="C.S. Tax"></asp:Label>&nbsp;</td>
                                <td style="text-align: left; width: 423px;">
                                    <asp:TextBox ID="txtVAT" runat="server" autocomplete="off">
                                    </asp:TextBox>
                                    <asp:TextBox ID="txtBranchTransfer" runat="server" autocomplete="off">
                                    </asp:TextBox>
                                    <asp:TextBox ID="txtCST" runat="server" autocomplete="off">
                                    </asp:TextBox><asp:Label ID="Label25" runat="server" EnableTheming="False" Font-Bold="False"
                                        Font-Names="Verdana" Font-Size="Smaller" Text="%"></asp:Label><asp:Label ID="Label21"
                                            runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana" Font-Size="Smaller"
                                            ForeColor="Red" Text="*"></asp:Label><cc1:FilteredTextBoxExtender ID="ftxteVat" runat="server"
                                                TargetControlID="txtVAT" ValidChars=".0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                    <cc1:FilteredTextBoxExtender ID="ftxteCST" runat="server" TargetControlID="txtCST"
                                        ValidChars=".0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label11" runat="server" Text="Miscelleneous"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtMiscelleneous" runat="server">
                                    </asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="ftxteMiscelleneous" runat="server" TargetControlID="txtMiscelleneous"
                                        ValidChars=".0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label9" runat="server" Text="Discount"></asp:Label></td>
                                <td style="text-align: left; width: 423px;">
                                    <asp:TextBox ID="txtDiscount" runat="server">
                                    </asp:TextBox>
                                    <asp:Label ID="Label29" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" Text="%"></asp:Label>
                                    <cc1:FilteredTextBoxExtender ID="ftxteDiscount" runat="server" TargetControlID="txtDiscount"
                                        ValidChars=".0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label10" runat="server" Text="Gross Amount"></asp:Label></td>
                                <td colspan="3" style="text-align: left">
                                    <asp:TextBox ID="txtGrossAmount" runat="server" Width="349px">
                                    </asp:TextBox>
                                    <asp:HiddenField ID="txtGrossTotalAmtHidden" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblRemarks" runat="server" Text="Remarks"></asp:Label></td>
                                <td colspan="3" style="text-align: left">
                                    <asp:TextBox ID="txtRemarks" runat="server" Height="53px" TextMode="MultiLine" Width="673px"
                                        CssClass="textbox" EnableTheming="False">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="profilehead" colspan="4" style="text-align: left; height: 14px;">Reference Details</td>
                            </tr>
                            <tr>
                                <td style="height: 19px; text-align: right"></td>
                                <td style="height: 19px; text-align: left"></td>
                                <td style="height: 19px; text-align: right"></td>
                                <td style="height: 19px; text-align: left; width: 423px;"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                                    </asp:DropDownList></td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By"></asp:Label></td>
                                <td style="text-align: left; width: 423px;">
                                    <asp:DropDownList ID="ddlApprovedBy" runat="server" Enabled="False">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 19px;"></td>
                                <td style="text-align: left; height: 19px;"></td>
                                <td style="text-align: right; height: 19px;"></td>
                                <td style="text-align: left; height: 19px; width: 423px;"></td>
                            </tr>
                            <tr>
                                <td colspan="4" style="height: 47px">
                                    <table id="tblButtons" align="center">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                            <td>
                                                <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click"
                                                    CausesValidation="False" /></td>
                                            <td>
                                                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click"
                                                    CausesValidation="False" /></td>
                                            <td>
                                                <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" /></td>
                                            <td style="width: 3px">
                                                <asp:Button ID="btnPrint" runat="server" CausesValidation="False" OnClick="btnPrint_Click"
                                                    Text="Print" /></td>
                                            <td style="width: 3px"></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <asp:TextBox ID="txtExcise" runat="server" Visible="False">
                        </asp:TextBox>
                        <asp:Label ID="lblPaymentreceived" runat="server" Visible="False"></asp:Label></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td></td>
                    <td style="text-align: right;"></td>
                    <td></td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="id" />
</asp:Content>


 
