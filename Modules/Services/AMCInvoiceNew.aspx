<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/ServicesMP1.master" AutoEventWireup="true" CodeFile="AMCInvoiceNew.aspx.cs" Inherits="Modules_Services_AMCInvoiceNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <script>
            $(function () {
                $("[name$='txtInvoiceDate']").datepicker();
            });
    </script>
    <script type="text/javascript" language="javascript">
        function amtcalc() {
            var req_qty, rate, vat, cst, excise;
            req_qty = document.getElementById('<%=txtQuantity.ClientID %>').value;
            rate = document.getElementById('<%=txtRate.ClientID %>').value;
            cst = document.getElementById('<%=txtCST.ClientID %>').value;
            vat = document.getElementById('<%=txtVAT.ClientID %>').value;
            excise = document.getElementById('<%=txtExcise.ClientID %>').value;
            if (cst == "" || cst == "0" || isNaN(cst)) { cst = "0"; }
            if (vat == "" || vat == "0" || isNaN(vat)) { vat = "0"; }
            if (excise == "" || excise == "0" || isNaN(excise)) { excise = "0"; }

            if (req_qty == "" || req_qty == "0") {
                document.getElementById('<%=txtAmount.ClientID %>').value = "0";
    }
    else if (rate == "" || rate == "0") {
        document.getElementById('<%=txtAmount.ClientID %>').value = "0";
    }
    else if (rate > 0 && req_qty > 0) {
        document.getElementById('<%=txtAmount.ClientID %>').value = (rate * req_qty) + parseFloat(cst * (rate * req_qty) / 100) + parseFloat(vat * (rate * req_qty) / 100) + parseFloat(excise * (rate * req_qty) / 100);
    }
}

function grosscalc() {
    var disc, grossamt, misc, transchargs, TOTAL;
    misc = parseFloat(document.getElementById('<%=txtMiscelleneous.ClientID %>').value);
    disc = parseFloat(document.getElementById('<%=txtDiscount.ClientID %>').value);
    grossamt = parseFloat(document.getElementById('<%=txtGrossTotalAmtHidden.ClientID %>').value);
    if (grossamt == "" || grossamt == "0" || isNaN(grossamt)) { grossamt = "0"; }
    if (misc == "" || misc == "0" || isNaN(misc)) { misc = "0"; }
    if (disc == "" || disc == "0" || isNaN(disc)) { disc = "0"; }
    TOTAL = parseFloat(grossamt) + parseFloat(misc);
    TOTAL = TOTAL - ((disc * TOTAL) / 100);
    document.getElementById('<%=txtGrossAmount.ClientID %>').value = TOTAL;
   }

   function rbVATCSTEnableDisable() {
       if (document.getElementById('<%=rbVAT.ClientID %>').checked == true) {
            document.getElementById('<%=txtVAT.ClientID %>').style.display = document.getElementById('<%=lblVAT.ClientID %>').style.display = "";
            document.getElementById('<%=txtCST.ClientID %>').style.display = document.getElementById('<%=lblCSTax.ClientID %>').style.display = "none";
            document.getElementById('<%=txtVAT.ClientID %>').focus();
        }
        if (document.getElementById('<%=rbCST.ClientID %>').checked == true) {
            document.getElementById('<%=txtVAT.ClientID %>').style.display = document.getElementById('<%=lblVAT.ClientID %>').style.display = "none";
            document.getElementById('<%=txtCST.ClientID %>').style.display = document.getElementById('<%=lblCSTax.ClientID %>').style.display = "";
            document.getElementById('<%=txtCST.ClientID %>').focus();
        }
        document.getElementById('<%=txtVAT.ClientID %>').value = "";
        document.getElementById('<%=txtCST.ClientID %>').value = "";
        amtcalc();
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td style="text-align:left">AMC Invoice</td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td colspan="4">
                <table border="0" cellpadding="0" cellspacing="0" id="tblSIDetails" runat="server"
                    visible="true" width="100%">
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">General Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="Label1" runat="server" Text="Delivery Challan No" Width="127px" Visible="False"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlDeviveryNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDeviveryNo_SelectedIndexChanged"
                                Visible="False">
                            </asp:DropDownList><asp:Label ID="Label26" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlDeviveryNo"
                                    ErrorMessage="Please Select the Delivery Challan No" InitialValue="0" Visible="False">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label2" runat="server" Text="Delivery Challan Date" Width="146px"
                                Visible="False"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtChallanDate" runat="server" ReadOnly="True" Visible="False"></asp:TextBox>
                            <asp:Image ID="imgChallanDate" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False" />
                            <cc1:CalendarExtender
                                Format="dd/MM/yyyy" ID="CeChallanDate" runat="server" Enabled="False" PopupButtonID="imgChallanDate"
                                TargetControlID="txtChallanDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MeeChallanDate" runat="server" DisplayMoney="Left" Enabled="True"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtChallanDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblSalesOrderNo" runat="server" Text="AMC Order No" Width="101px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlSalesOrderNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSalesOrderNo_SelectedIndexChanged">
                            </asp:DropDownList><asp:Label ID="Label14" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSalesOrderNo"
                                    ErrorMessage="Please Enter the Delivery Challan No">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblSalesOrderDate" runat="server" Text="AMC Order Date" Width="114px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtSalesOrderDate" runat="server" ReadOnly="True"></asp:TextBox>&nbsp;
                            <asp:Image  ID="imgSalesOrderDate" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False"></asp:Image>
                            <cc1:CalendarExtender Format="dd/MM/yyyy" ID="CeSalesOrderDate" runat="server"
                                    Enabled="False" PopupButtonID="imgSalesOrderDate" TargetControlID="txtSalesOrderDate">
                                </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MeeSalesOrderDate" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSalesOrderDate"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblCustomer" runat="server" Text="Customer Name"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCustomerName" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblRegion" runat="server" Text="Region"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtRegion" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblPhone" runat="server" Text="Phone"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtPhone" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblMobile" runat="server" Text="Mobile"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">AMC Items</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:GridView ID="gvItemDetails" runat="server" AutoGenerateColumns="False" Width="100%"
                                OnRowDataBound="gvItemDetails_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                    <asp:BoundField DataField="ItemType" HeaderText="Item Type" />
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                    <asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
                                    <asp:BoundField HeaderText="Amount"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">Invoice Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;"></td>
                        <td style="text-align: left; height: 19px;"></td>
                        <td style="text-align: right; height: 19px;"></td>
                        <td style="text-align: left; height: 19px;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label3" runat="server" Text="Invoice No"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtInvoiceNo" runat="server" ReadOnly="True"></asp:TextBox><asp:Label
                                ID="Label12" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                    ID="rfvInvoiceNo" runat="server" ControlToValidate="txtInvoiceNo" ErrorMessage="Please Enter the Delivery Challan No">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label6" runat="server" Text="Invoice Date"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtInvoiceDate" runat="server"></asp:TextBox>&nbsp;
                           
                            <asp:Label ID="Label15" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvInvoiceDate" runat="server" ControlToValidate="txtInvoiceDate"
                                ErrorMessage="Please Enter the Delivery Challan No">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="DateCustomValidate"
                                ControlToValidate="txtInvoiceDate" ErrorMessage="Please Enter the Invoice Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                SetFocusOnError="True">*</asp:CustomValidator>
                            
                            <cc1:MaskedEditExtender ID="MeeInvoiceDate" runat="server" DisplayMoney="Left" Enabled="True"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtInvoiceDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label5" runat="server" Text="Invoice Type"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:DropDownList ID="ddlInvoiceType" runat="server">
                                <asp:ListItem>Invoice</asp:ListItem>
                                <asp:ListItem>Pro-Forma Invoice</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="Label13" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlInvoiceType"
                                ErrorMessage="Please Select the Invoice Type">*</asp:RequiredFieldValidator></td>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label8" runat="server" Text="Delivery Type" Visible="False"></asp:Label></td>
                        <td style="height: 19px; text-align: left;">
                            <asp:DropDownList ID="ddlDeliveryType" runat="server" Visible="False">
                            </asp:DropDownList><asp:Label ID="Label22" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlDeliveryType"
                                ErrorMessage="Please Select the Delivery Type " InitialValue="0" Visible="False">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">&nbsp;</td>
                        <td style="text-align: left">&nbsp;</td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left;">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">Items Details</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 19px; text-align: right"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label4" runat="server" Text="Item Type"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label ID="Label16" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlItemType"
                                ErrorMessage="Please Select the Item Type" InitialValue="0" ValidationGroup="id">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label7" runat="server" Text="Item Name" Width="76px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlItemName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="0">-- Select Item Type --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="Label17" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlItemName"
                                ErrorMessage="Please Select the Item Name" InitialValue="0" ValidationGroup="id">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label27" runat="server" Text="UOM"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtItemUOM" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label23" runat="server" Text="Item Specification" Width="126px"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtItemSpec" runat="server" CssClass="multilinetext" EnableTheming="False"
                                ReadOnly="True" TextMode="MultiLine" Width="600px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblRate" runat="server" Text="Rate"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtRate" runat="server">
                            </asp:TextBox>
                            <asp:Label ID="Label19" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtRate"
                                ErrorMessage="Please Enter the Rate" ValidationGroup="id">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                    ID="ftxteRate" runat="server" TargetControlID="txtRate" ValidChars=".0123456789">
                                </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtQuantity" runat="server" Width="139px">
                            </asp:TextBox>
                            <asp:Label ID="Label18" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtQuantity"
                                ErrorMessage="Please Enter the Quantity" ValidationGroup="id">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                    ID="ftxteQuantity" runat="server" FilterType="Numbers" TargetControlID="txtQuantity"
                                    ValidChars=".">
                                </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label24" runat="server" Text="Excise"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtExcise" runat="server">
                            </asp:TextBox><asp:Label ID="Label28" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" Text="%"></asp:Label><cc1:FilteredTextBoxExtender
                                    ID="ftxteExcise" runat="server" TargetControlID="txtExcise" ValidChars=".0123456789">
                                </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left">&nbsp;<asp:RadioButton ID="rbVAT" runat="server" Checked="True" GroupName="vatcst" Text="VAT" Visible="False" /><asp:RadioButton
                            ID="rbCST" runat="server" GroupName="vatcst" Text="C.S. Tax" Visible="False" /></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblVAT" runat="server" Text="Service Tax"></asp:Label><asp:Label ID="lblCSTax"
                                runat="server" Text="C.S. Tax"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtVAT" runat="server">
                            </asp:TextBox><asp:TextBox ID="txtCST" runat="server">
                            </asp:TextBox><asp:Label ID="Label25" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" Text="%"></asp:Label><asp:Label ID="Label21"
                                    runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana" Font-Size="Smaller"
                                    ForeColor="Red" Text="*"></asp:Label><br />
                            <cc1:FilteredTextBoxExtender ID="ftxteVat" runat="server" TargetControlID="txtVAT"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:FilteredTextBoxExtender ID="ftxteCST" runat="server" TargetControlID="txtCST"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblAmount" runat="server" Text="Amount"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtAmount" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left;"></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: right">
                            <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" Text="Add" OnClick="btnAdd_Click"
                                ValidationGroup="id" /></td>
                        <td style="height: 19px; text-align: left">
                            <asp:Button ID="btnItemRefresh" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" OnClick="btnItemRefresh_Click" Text="Refresh"
                                CausesValidation="False" /></td>
                        <td style="height: 19px; text-align: left;"></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 19px; text-align: center">
                            <asp:GridView ID="gvItmDetails" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvItmDetails_RowDeleting"
                                OnRowDataBound="gvItmDetails_RowDataBound" OnRowEditing="gvItmDetails_RowEditing">
                                <Columns>
                                    <asp:CommandField ShowEditButton="True" />
                                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                    <asp:BoundField DataField="ItemType" HeaderText="Item Type" />
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
                                    <asp:BoundField DataField="Vat" HeaderText="Service Tax"></asp:BoundField>
                                    <asp:BoundField DataField="CST" HeaderText="CST"></asp:BoundField>
                                    <asp:BoundField DataField="Excise" HeaderText="Excise"></asp:BoundField>
                                    <asp:BoundField HeaderText="Amount"></asp:BoundField>
                                    <asp:BoundField DataField="ItemTypeId" HeaderText="ItemTypeIdHidden" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 14px; text-align: right"></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4">Other Charges</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label11" runat="server" Text="Miscelleneous"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtMiscelleneous" runat="server"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftxteMiscelleneous" runat="server" TargetControlID="txtMiscelleneous"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label9" runat="server" Text="Discount"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtDiscount" runat="server"></asp:TextBox>
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
                            <asp:TextBox ID="txtGrossAmount" runat="server" Width="349px"></asp:TextBox>
                            <asp:HiddenField ID="txtGrossTotalAmtHidden" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblRemarks" runat="server" Text="Remarks"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtRemarks" runat="server" Height="53px" TextMode="MultiLine" Width="600px"
                                CssClass="textbox" EnableTheming="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">Reference Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlApprovedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;"></td>
                        <td style="text-align: left; height: 19px;"></td>
                        <td style="text-align: right; height: 19px;"></td>
                        <td style="text-align: left; height: 19px;"></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table id="tblButtons">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click"
                                            CausesValidation="False" /></td>
                                    <td>
                                        <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" /></td>
                                    <td>
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" CausesValidation="False" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td style="text-align: right;"></td>
            <td></td>
        </tr>
    </table>
</asp:Content>


 
