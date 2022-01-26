<%@ Page Language="C#" MasterPageFile="~/MPs/PurchaseMP1.master" AutoEventWireup="true"
    CodeFile="Quotation.aspx.cs" Inherits="Modules_PurchasingManagement_Quotation" Title="|| Value App : Purchasing Management : Proforma Invoice ||" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <script type="text/javascript">
        // Let's use a lowercase function name to keep with JavaScript conventions
        function selectAll(invoker) {
            // Since ASP.NET checkboxes are really HTML input elements
            //  let's get all the inputs 
            var inputElements = document.getElementsByTagName('input');

            for (var i = 0 ; i < inputElements.length ; i++) {
                var myElement = inputElements[i];

                // Filter through the input types looking for checkboxes
                if (myElement.type === "checkbox") {

                    // Use the invoker (our calling element) as the reference 
                    //  for our checkbox status
                    myElement.checked = invoker.checked;
                }
            }
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {


            $(document).on("keyup", "[id$='txtPackingCharges'],[id$='txtTransCharges'],[id$='txtVAT'],[id$='txtInsurance'],[id$='txtFOB'],[id$='txtCIF'],[id$='txtDisc']", function () {

                var Total = parseFloat($("[id$='lblTtlAmt']").html(), 10) || 0;
                var PackingCharges = parseFloat($("[id$='txtPackingCharges']").val(), 10) || 0;
                var TransCharges = parseFloat($("[id$='txtTransCharges']").val(), 10) || 0;
                var fob = parseFloat($("[id$='txtFOB']").val(), 10) || 0;
                var cif = parseFloat($("[id$='txtCIF']").val(), 10) || 0;

                var Per = parseFloat($("[id$='txtVAT']").val(), 10) || 0;
                var VAT = (Total * Per) / 100;

                var Ins = parseFloat($("[id$='txtInsurance']").val(), 10) || 0;
                var Insurance = (Total * Ins) / 100;

                var disc = parseFloat($("[id$='txtDisc']").val(), 10) || 0;
                var discount = (Total * disc) / 100;
                var tot = (PackingCharges + TransCharges + Total + VAT + Insurance + cif + fob - discount) || 0;

                $("[id$='hfSum']").val(tot);
                $("[id$='lblsum']").text(tot);
                //$('.sum').text((PackingCharges + TransCharges + Total + VAT + Insurance + cif + fob - discount) || '')

                <%--   var Ttl = document.getElementById('<%=lblsum.ClientID %>').value;
                alert(Ttl);
                document.getElementById('<%=lblTtl.ClientID %>').value = parseFloat(Ttl);
  --%>
            });
        });
    </script>
    <script type="text/javascript">

        function rbVATCSTEnableDisable() {
            if (document.getElementById('<%=rbVAT.ClientID %>').checked == true) {
                document.getElementById('<%=lblVATCST.ClientID %>').innerHTML = "VAT"
            }
            if (document.getElementById('<%=rbCST.ClientID %>').checked == true) {
                document.getElementById('<%=lblVATCST.ClientID %>').innerHTML = "C.S. txtVAT";
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
    <%-- <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>--%>



    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>Proforma Invoice</td>
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
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td id="Td20"></td>
            <td>
                <asp:Label ID="lblTtl" runat="server" Visible="false"></asp:Label></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td id="TD9" class="searchhead" colspan="4">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left" colspan="2">Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                            <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
                        </td>

                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td style="height: 25px">
                                        <asp:Label ID="Label20" CssClass="label" runat="server" EnableTheming="False" Font-Bold="True"
                                            Text="Search By" meta:resourcekey="Label20Resource1"></asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged" meta:resourcekey="ddlSearchByResource1">
                                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource1">--</asp:ListItem>
                                            <asp:ListItem Value="SUP_QUOT_NO" meta:resourcekey="ListItemResource2">Sup Quotation No.</asp:ListItem>
                                            <asp:ListItem Value="SUP_QUOT_DATE" meta:resourcekey="ListItemResource3">Sup Quotation Date</asp:ListItem>
                                            <asp:ListItem Value="SUP_NAME" meta:resourcekey="ListItemResource4">Supplier Name</asp:ListItem>
                                            <asp:ListItem Value="SUP_QUOT_PO_TYPE" meta:resourcekey="ListItemResource5">PO Type</asp:ListItem>
                                            <asp:ListItem Value="SUP_EMAIL" meta:resourcekey="ListItemResource6">Enquiry From</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList ID="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                            EnableTheming="False" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged"
                                            Visible="False" Width="50px" meta:resourcekey="ddlSymbolsResource1">
                                            <asp:ListItem Selected="True" meta:resourcekey="ListItemResource7">=</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource8">&lt;</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource9">&gt;</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource10">&lt;=</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource11">&gt;=</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource12">R</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="height: 25px">
                                        <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False" meta:resourcekey="lblCurrentFromDateResource1"></asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchValueFromDate" runat="server" type="datpice" EnableTheming="True" Visible="False"
                                            Width="106px" meta:resourcekey="txtSearchValueFromDateResource1"></asp:TextBox>
                                        <%--<asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                                Visible="False" meta:resourcekey="imgFromDateResource1"></asp:Image>
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchFrom" runat="server" Enabled="False" PopupButtonID="imgFromDate"
                                            TargetControlID="txtSearchValueFromDate">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate"
                                            UserDateFormat="MonthDayYear" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="">
                                        </cc1:MaskedEditExtender>--%>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False" meta:resourcekey="lblCurrentToDateResource1"></asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchValueToDate" runat="server" type="datepic" EnableTheming="True" Visible="False"
                                            Width="106px" meta:resourcekey="txtSearchValueFromDateResource1"></asp:TextBox></td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px" meta:resourcekey="txtSearchTextResource1"></asp:TextBox>
                                        <%--<asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False" meta:resourcekey="imgToDateResource1"></asp:Image>
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchValueToDate" runat="server" Enabled="False" PopupButtonID="imgToDate"
                                            TargetControlID="txtSearchText">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                            UserDateFormat="MonthDayYear" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="">
                                        </cc1:MaskedEditExtender>--%>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" meta:resourcekey="btnSearchGoResource1" /></td>
                                </tr>
                            </table>
                            <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False" meta:resourcekey="lblEmpIdHiddenResource1"></asp:Label><asp:Label
                                ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label><asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                    Visible="False"></asp:Label><asp:Label ID="lblSearchItemHidden" runat="server" Visible="False" meta:resourcekey="lblSearchItemHiddenResource1"></asp:Label>
                            <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False" meta:resourcekey="lblSearchTypeHiddenResource1"></asp:Label>
                            <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False" meta:resourcekey="lblSearchValueFromHiddenResource1"></asp:Label>
                            <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False" meta:resourcekey="lblSearchValueHiddenResource2"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td id="TD34" runat="server" colspan="4" style="text-align: center">
                <asp:GridView ID="gvSupQuotationDetails" runat="server" AllowPaging="True"
                    AutoGenerateColumns="False" DataKeyNames="SUP_QUOT_ID" DataSourceID="sdsSupQuotDetails"
                    Width="100%" OnRowDataBound="gvSupQuotationDetails_RowDataBound" SelectedRowStyle-BackColor="#c0c0c0" meta:resourcekey="gvSupQuotationDetailsResource1" AllowSorting="True" PageSize="8">
                    <Columns>
                        <asp:BoundField DataField="SUP_QUOT_ID" SortExpression="SUP_QUOT_ID" HeaderText="QuotationIdHidden" meta:resourceKey="BoundFieldResource1"></asp:BoundField>
                        <asp:TemplateField SortExpression="SUP_QUOT_NO" HeaderText="Proforma Invoice No" meta:resourceKey="TemplateFieldResource1">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("SUP_QUOT_NO") %>' ForeColor="#0066ff" ID="TextBox1" meta:resourcekey="TextBox1Resource1"></asp:TextBox>

                            </EditItemTemplate>

                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnSupQuotNo" OnClick="lbtnSupQuotNo_Click" ForeColor="#0066ff" runat="server" Text='<%# Eval("SUP_QUOT_NO") %>' CausesValidation="False" meta:resourcekey="lbtnSupQuotNoResource1" __designer:wfdid="w1"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="SUP_QUOT_DATE" SortExpression="SUP_QUOT_DATE" HeaderText="PI Date" meta:resourceKey="BoundFieldResource2">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="SUP_NAME" SortExpression="SUP_NAME" HeaderText="Supplier Name" meta:resourceKey="BoundFieldResource3">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="SUP_QUOT_PO_TYPE" SortExpression="SUP_QUOT_PO_TYPE" HeaderText="PO Type" meta:resourceKey="BoundFieldResource4">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HtmlEncode="False" DataField="SUP_EMAIL" SortExpression="SUP_EMAIL" HeaderText="Enquiry From" meta:resourceKey="BoundFieldResource5">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        <span style="color: #ff0000">No Record Found! </span>
                    </EmptyDataTemplate>
                </asp:GridView>
                <asp:SqlDataSource ID="sdsSupQuotDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SCM_SUPPLIERQUOTATION_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="CPID" ControlID="lblCPID"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td id="TD24" colspan="4" style="text-align: center">
                <table id="Table1" align="center">
                    <tr>
                        <td>
                            <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CausesValidation="False" meta:resourcekey="btnNewResource1" /></td>
                        <td>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CausesValidation="False" meta:resourcekey="btnEditResource1" /></td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" meta:resourcekey="btnDeleteResource1" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" id="tblSupQuotationDetails" runat="server" visible="false" width="100%">
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
                                            ControlToValidate="ddlSupplierName" ErrorMessage="Please Select The Supplier Name" meta:resourcekey="RequiredFieldValidator2Resource1" ValidationGroup="main">*</asp:RequiredFieldValidator></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label22" runat="server" Text="Enquiry Date" Width="96px" meta:resourcekey="Label22Resource1"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtEnquiryDate" runat="server" CssClass="datetext" type="datepic" EnableTheming="False" meta:resourcekey="txtEnquiryDateResource1"></asp:TextBox>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label21" runat="server" Text="Enquiry No" Width="72px" meta:resourcekey="Label21Resource1"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlEnquiryNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEnquiryNo_SelectedIndexChanged" meta:resourcekey="ddlEnquiryNoResource1">
                                    </asp:DropDownList><asp:Label ID="Label36" runat="server" EnableTheming="False" ForeColor="Red"
                                        Text="*" meta:resourcekey="Label36Resource1"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlEnquiryNo"
                                            ErrorMessage="Please Select  the Enquiry No." InitialValue="0" meta:resourcekey="RequiredFieldValidator4Resource1" ValidationGroup="main">*</asp:RequiredFieldValidator></td>
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
                                    <asp:GridView ID="gvApprlItemDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvApprlItemDetails_RowDataBound" Width="100%">
                                        <Columns>
                                            <%--<asp:BoundField DataField="ReqFor" HeaderText="Room">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:/MM/dd/yyyy}" DataField="ReqDate" SortExpression="ReqDate" HeaderText="Required by Date"></asp:BoundField>
<asp:BoundField DataField="Specification" HeaderText="Specification">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Priority" HeaderText="Priority"></asp:BoundField>
<asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
<asp:BoundField DataField="ColorId" HeaderText="ColorId"></asp:BoundField>--%>
                                            <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                            <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Itemname" HeaderText="Item Model No">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Itemtype" HeaderText="Item Type" />
                                            <asp:BoundField DataField="Indentid" HeaderText="Indent Id">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="UOM" HeaderText="UOM" />
                                            <asp:BoundField DataField="Quantity" HeaderText="Item Quantity"></asp:BoundField>
                                            <asp:BoundField DataField="Brand" HeaderText="Item Brand"></asp:BoundField>
                                            <asp:BoundField DataField="Requiredfor" HeaderText="Indent Detail Required For"></asp:BoundField>
                                            <asp:BoundField DataField="Color" HeaderText="Color">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </asp:BoundField>

                                            <asp:BoundField DataField="ColorId" HeaderText="ColorId">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IndentdetId" HeaderText="Indentdet Id">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                <asp:CheckBox ID="chkhdr" runat="server" AutoPostBack="True" OnClick="selectAll(this)" />
                                            </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" __designer:wfdid="w4"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click" CausesValidation="False" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table id="tblHidden" runat="server" visible="false">


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

                                    </table>
                                </td>
                            </tr>
                            </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>

                            <tr>
                                <td colspan="4" style="text-align: center;">
                                    <asp:GridView ID="gvProductDetails" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDataBound="gvProductDetails_RowDataBound1" OnRowDeleting="gvProductDetails_RowDeleting1">
                                        <Columns>
                                            <%--<asp:BoundField DataField="ReqFor" HeaderText="Room">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:/MM/dd/yyyy}" DataField="ReqDate" SortExpression="ReqDate" HeaderText="Required by Date"></asp:BoundField>
<asp:BoundField DataField="Specification" HeaderText="Specification">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Priority" HeaderText="Priority"></asp:BoundField>
<asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
<asp:BoundField DataField="ColorId" HeaderText="ColorId"></asp:BoundField>--%>
                                            <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                            <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Itemname" HeaderText="Model No">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Itemtype" HeaderText="Item Type" ItemStyle-Width="150px" />
                                            <asp:BoundField DataField="Indentid" HeaderText="Indent Id">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="UOM" HeaderText="UOM" />
                                            <%--<asp:BoundField DataField="Quantity" HeaderText="Item Quantity"></asp:BoundField>--%>
                                            <asp:TemplateField HeaderText="Quantity" ControlStyle-Width="80px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Brand" HeaderText="Item Brand"></asp:BoundField>
                                            <asp:BoundField DataField="Requiredfor" HeaderText="Required For"></asp:BoundField>
                                            <asp:BoundField DataField="Color" HeaderText="Color">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </asp:BoundField>

                                            <asp:BoundField DataField="ColorId" HeaderText="ColorId">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IndentdetId" HeaderText="Indentdet Id">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Currency" ControlStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlCurrency" runat="server">
                                                    </asp:DropDownList>
                                                     <asp:HiddenField ID="cthf1" runat="server" Value='<%# Eval("Currency") %>' />
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Price" ControlStyle-Width="70px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPrice" runat="server" AutoPostBack="true" OnTextChanged="txtPrice_TextChanged" Text='<%# Bind("Price") %>'></asp:TextBox>
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Discount" ControlStyle-Width="70px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDiscount" runat="server" Text='<%# Bind("Discount") %>' AutoPostBack="true" OnTextChanged="txtDiscount_TextChanged"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Special Amount" ControlStyle-Width="70px">
                                                <ItemTemplate>
                                                    <%--<asp:TextBox ID="txtSpecialPrice" runat="server"></asp:TextBox>--%>
                                                    <asp:Label ID="lblSpecialPrice" Text='<%# Bind("SplAmt") %>' runat="server"></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Excepted Arrival Date" ControlStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtArrivalDate" type="datepic" Text='<%# Bind("ArrivalDate") %>' runat="server"></asp:TextBox>
                                                    <%--<cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtArrivalDate" runat="server"></cc1:CalendarExtender>--%>
                                                </ItemTemplate>


                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Proforma Inv No" ControlStyle-Width="80px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtInvoiceNo" Text='<%# Bind("InvoiceNo") %>' runat="server"></asp:TextBox>
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr style="background-color: #98b7e9">
                                <td></td>
                                <td></td>
                                <td colspan="2">
                                    <table id="tblSumAmt" runat="server" style="text-align: center; width: 100%;">
                                        <tr>
                                            <td style="font-weight: bold">Total Amount :
                                <asp:Label ID="lblTtlAmt" ForeColor="Red" runat="server" Text="0.00"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
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
                                                ErrorMessage="Please Enter the Delivery" meta:resourcekey="rfvDeliveryResource1" ValidationGroup="main">*</asp:RequiredFieldValidator></td>
                                <td style="height: 19px" align="right">
                                    <asp:Label
                                        ID="Label3" runat="server" Text="Payment Terms" meta:resourcekey="Label3Resource1"></asp:Label></td>
                                <td style="height: 19px" align="left">
                                    <asp:TextBox ID="txtPaymentTerms"
                                        runat="server" TextMode="MultiLine" meta:resourcekey="txtPaymentTermsResource1"></asp:TextBox><asp:Label ID="Label35" runat="server"
                                            EnableTheming="False" ForeColor="Red" Text="*" meta:resourcekey="Label35Resource1"></asp:Label><asp:RequiredFieldValidator ID="rfvPayTerms" runat="server" ControlToValidate="txtPaymentTerms"
                                                ErrorMessage="Please Enter the Payment Terms" meta:resourcekey="rfvPayTermsResource1" ValidationGroup="main">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td style="height: 10px; text-align: right">
                                    <asp:Label
                                        ID="Label19" runat="server" Text="packing charges" meta:resourcekey="Label19Resource1"></asp:Label></td>
                                <td align="left" style="height: 10px">
                                    <asp:TextBox ID="txtPackingCharges"
                                        runat="server" meta:resourcekey="txtPackingChargesResource1"></asp:TextBox><asp:Label ID="Label29" runat="server" EnableTheming="False"
                                            ForeColor="Red" Text="*" meta:resourcekey="Label29Resource1"></asp:Label><asp:RequiredFieldValidator ID="rfvPackingChrgs" runat="server" ControlToValidate="txtPackingCharges"
                                                ErrorMessage="Please Enter the Packing Charges" meta:resourcekey="rfvPackingChrgsResource1" ValidationGroup="main">*</asp:RequiredFieldValidator></td>
                                <td align="right" style="height: 10px">
                                    <asp:Label
                                        ID="Label10" runat="server" Text="Guarantee" meta:resourcekey="Label10Resource1"></asp:Label></td>
                                <td align="left" style="height: 10px">
                                    <asp:TextBox ID="txtGuarantee"
                                        runat="server" meta:resourcekey="txtGuaranteeResource1"></asp:TextBox><asp:Label ID="Label31" runat="server" EnableTheming="False"
                                            ForeColor="Red" Text="*" meta:resourcekey="Label31Resource1"></asp:Label><asp:RequiredFieldValidator ID="rfvGuarantee" runat="server" ControlToValidate="txtGuarantee"
                                                ErrorMessage="Please Enter the Guarantee" meta:resourcekey="rfvGuaranteeResource1" ValidationGroup="main">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td align="right" style="height: 19px; text-align: right"></td>
                                <td align="left" style="height: 19px">
                                    <asp:RadioButton
                                        ID="rbVAT" runat="server" Checked="True" GroupName="vatcst" Text="VAT" meta:resourcekey="rbVATResource1"></asp:RadioButton><asp:RadioButton
                                            ID="rbCST" runat="server" GroupName="vatcst" Text="C.S. txtVAT" meta:resourcekey="rbCSTResource1"></asp:RadioButton><asp:RadioButton
                                                ID="rbincluding" runat="server" GroupName="vatcst" Text="Including" meta:resourcekey="rbincludingResource1"></asp:RadioButton></td>
                                <td align="right" style="height: 19px">
                                    <asp:Label
                                        ID="Label13" runat="server" Text="Insurance" meta:resourcekey="Label13Resource1"></asp:Label></td>
                                <td align="left" style="height: 19px">
                                    <asp:TextBox ID="txtInsurance"
                                        runat="server" meta:resourcekey="txtInsuranceResource1"></asp:TextBox>%<asp:Label ID="Label32" runat="server" EnableTheming="False"
                                            ForeColor="Red" Text="*" meta:resourcekey="Label32Resource1"></asp:Label><asp:RequiredFieldValidator ID="rfvInsurance" runat="server" ControlToValidate="txtInsurance"
                                                ErrorMessage="Please Enter the Insurance" meta:resourcekey="rfvInsuranceResource1" ValidationGroup="main">*</asp:RequiredFieldValidator></td>
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
                                        ControlToValidate="ddlDespatchMode" ErrorMessage="Please Enter the Despatch Mode" meta:resourcekey="RequiredFieldValidator5Resource1" ValidationGroup="main">*</asp:RequiredFieldValidator></td>
                                <td align="right" style="height: 19px">
                                    <asp:Label
                                        ID="Label11" runat="server" Text="Transportation Charges" meta:resourcekey="Label11Resource1"></asp:Label></td>
                                <td align="left" style="height: 19px">
                                    <asp:TextBox
                                        ID="txtTransCharges" runat="server" TextMode="SingleLine" meta:resourcekey="txtTransChargesResource1"></asp:TextBox></td>
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
                                <td style="text-align: right">Attachments :</td>
                                <td>
                                    <asp:FileUpload ID="Uploadattach" runat="server" AllowMultiple="true" />

                                </td>
                                <td style="text-align: right">Total Amount :
                                </td>
                                <td style="font-weight: bold">
                                    <%--<asp:Label ID="lblsum" Class="sum" runat="server"></asp:Label>--%>
                                    <asp:Label ID="lblsum" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hfSum" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:SqlDataSource ID="sdsUploads" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT * FROM YANTRA_SUP_QUOT_ATTACHMENTS WHERE SUP_QUOT_ID=@SO_IDpara">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="lblSOIdHidden" DefaultValue="0" Name="SO_IDpara" PropertyName="Text" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <asp:Label ID="lblSOIdHidden" runat="server" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:Repeater ID="UploadsRepeater" runat="server" DataSourceID="sdsUploads">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnFileOpener" runat="server" CausesValidation="False" OnClick="lbtnFileOpener_Click" Text='<%# Bind("QUOT_ATTACHMENT") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </td>
                                <td style="text-align: right">&nbsp;</td>
                                <td style="font-weight: bold">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    
                                    <%--<table id="tblButtons" align="center">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" meta:resourcekey="btnSaveResource1" /></td>
                                            <td>
                                                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" CausesValidation="False" meta:resourcekey="btnRefreshResource1" /></td>
                                            <td>
                                                <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" meta:resourcekey="btnCloseResource1" /></td>
                                            <td>
                                                <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" CausesValidation="False" meta:resourcekey="btnPrintResource1" Visible="False" /></td>
                                            <td>
                                                <asp:Button ID="btnPurchase" runat="server" Text="PO" OnClick="btnPurchase_Click" CausesValidation="False" meta:resourcekey="btnPurchaseResource1" Visible="False" /></td>
                                        </tr>
                                    </table>--%>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                &nbsp;&nbsp;
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" meta:resourcekey="ValidationSummary1Resource1"></asp:ValidationSummary>
                <asp:ValidationSummary ID="ValidationSummary2" runat="server"
                    ValidationGroup="qi" meta:resourcekey="ValidationSummary2Resource1" ShowMessageBox="true"></asp:ValidationSummary>
                <asp:ValidationSummary ID="ValidationSummary3" runat="server"
                    ValidationGroup="main" meta:resourcekey="ValidationSummary2Resource1" ShowMessageBox="true"></asp:ValidationSummary>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table id="tblButtons" align="center" runat="server" visible="false">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" meta:resourcekey="btnSaveResource1" ValidationGroup="main" /></td>
                                            <td>
                                                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" CausesValidation="False" meta:resourcekey="btnRefreshResource1" /></td>
                                            <td>
                                                <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" meta:resourcekey="btnCloseResource1" /></td>
                                            <td>
                                                <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" CausesValidation="False" meta:resourcekey="btnPrintResource1" Visible="False" /></td>
                                            <td>
                                                <asp:Button ID="btnPurchase" runat="server" Text="PO" OnClick="btnPurchase_Click" CausesValidation="False" meta:resourcekey="btnPurchaseResource1" Visible="False" /></td>
                                        </tr>
                                    </table>
            </td>
        </tr>
        <tr>
            <td style="height: 21px"></td>
            <td style="height: 21px"></td>
            <td style="height: 21px;"></td>
            <td style="height: 21px"></td>
        </tr>
        <tr>
            <td colspan="4" style="height: 21px"></td>
        </tr>
    </table>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>


 
