<%@ Page Language="C#" MasterPageFile="~/MPs/FinanceMP1.master" AutoEventWireup="true"
    CodeFile="Invoice.aspx.cs" Inherits="Modules_CRM_Invoice" Title="|| Value App : Finance : Invoice ||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <script type="text/javascript" language="javascript">
        function amtcalc() {
            var req_qty, rate;
            req_qty = document.getElementById('<%=txtQuantity.ClientID %>').value;
    rate = document.getElementById('<%=txtRate.ClientID %>').value;
    disc1 = document.getElementById('<%=txtDiscount1.ClientID %>').value;


    if (req_qty == "" || req_qty == "0") {
        document.getElementById('<%=txtAmount.ClientID %>').value = "0";
    }
    else if (rate == "" || rate == "0") {
        document.getElementById('<%=txtAmount.ClientID %>').value = "0";
    }
    else if (rate > 0 && req_qty > 0) {
        document.getElementById('<%=txtAmount.ClientID %>').value = (rate * req_qty);
        document.getElementById('<%=txtSpPrice.ClientID %>').value = (rate * req_qty);
    }
    if (disc1 != "" && disc1 != "0") {
        document.getElementById('<%=txtSpPrice.ClientID %>').value = parseFloat((rate * req_qty)) - parseFloat((((rate * req_qty) * disc1) / 100));
    }
}
function Unitamtcalc() {
    var Spprice, req_qty;
    req_qty = document.getElementById('<%=txtQuantity.ClientID %>').value;
    Spprice = document.getElementById('<%=txtSpPrice.ClientID %>').value;


    if (req_qty == "" || req_qty == "0") {
        document.getElementById('<%=txtQuantity.ClientID %>').value = "0";
    }
    else if (Spprice == "" || Spprice == "0") {
        document.getElementById('<%=txtSpPrice.ClientID %>').value = "0";
    }
    else if (Spprice > 0 && req_qty > 0) {
        document.getElementById('<%=txtUnitprice.ClientID %>').value = (Spprice / req_qty);
        //document.getElementById('<%=txtSpPrice.ClientID %>').value=(rate*req_qty);
    }

}
        function grosscalc() {
            var vat, cst, disc, grossamt, misc, transchargs, TOTAL;
            cst = document.getElementById('<%=txtCST.ClientID %>').value;
        vat = document.getElementById('<%=txtVAT.ClientID %>').value;
       misc = parseFloat(document.getElementById('<%=txtMiscelleneous.ClientID %>').value);
       disc = parseFloat(document.getElementById('<%=txtDiscount.ClientID %>').value);
       grossamt = parseFloat(document.getElementById('<%=txtGrossTotalAmtHidden.ClientID %>').value);
       if (cst == "" || cst == "0" || isNaN(cst)) { cst = "0"; }
       if (vat == "" || vat == "0" || isNaN(vat)) { vat = "0"; }
       if (grossamt == "" || grossamt == "0" || isNaN(grossamt)) { grossamt = "0"; }
       if (misc == "" || misc == "0" || isNaN(misc)) { misc = "0"; }
       if (disc == "" || disc == "0" || isNaN(disc)) { disc = "0"; }
       TOTAL = parseFloat(grossamt);
       TOTAL = TOTAL + ((vat * TOTAL) / 100);
       TOTAL = TOTAL + ((cst * TOTAL) / 100);
       TOTAL = TOTAL + parseFloat(misc);
       TOTAL = TOTAL - ((disc * TOTAL) / 100);
       document.getElementById('<%=txtGrossAmount.ClientID %>').value = parseInt(TOTAL);
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
        grosscalc();
    }
    </script>

    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td style="text-align:left">Invoice</td>
            <td style="text-align:right">
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
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left;">
                <table id="TABLE2" border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left;">Invoice</td>
                        <td></td>
                        <td style="text-align: right;">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td style="height: 25px">
                                        <asp:Label ID="Label20" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="DC_NO">DC No</asp:ListItem>
                                            <asp:ListItem Value="SI_NO">SalesInvoice No</asp:ListItem>
                                            <%--<asp:ListItem Value="SI_DATE">Sales Invoice Date</asp:ListItem>--%>
                                            <asp:ListItem Value="CUST_NAME">Customer</asp:ListItem>
                                            <asp:ListItem Value="CUST_CONTACT_PERSON">Contact Person</asp:ListItem>
                                            <asp:ListItem Value="SI_GROSS_AMT">Amount</asp:ListItem>
                                            <asp:ListItem Value="SO_ACCEPTANCE_FLAG">Status</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList ID="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                            EnableTheming="False" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged"
                                            Visible="False" Width="50px">
                                            <asp:ListItem Selected="True">=</asp:ListItem>
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem>&gt;</asp:ListItem>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <asp:ListItem>&gt;=</asp:ListItem>
                                            <asp:ListItem>R</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="height: 25px">
                                        <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchValueFromDate" runat="server" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox><asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchFrom" runat="server" Enabled="False"
                                            PopupButtonID="imgFromDate" TargetControlID="txtSearchValueFromDate">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchValueToDate" runat="server"
                                            Enabled="False" PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                                </tr>
                            </table>
                            <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1" Visible="False"></asp:Label>
                            <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1" Visible="False"></asp:Label>
                            <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <asp:GridView ID="gvSalesInvoiceDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="sdsSalesInvoiceDetails" SelectedRowStyle-BackColor="#c0c0c0" OnRowDataBound="gvSalesInvoiceDetails_RowDataBound"
                    Width="100%" AllowSorting="True">
                    <Columns>
                        <asp:BoundField DataField="SI_ID" SortExpression="SI_ID" HeaderText="SalesInvoiceIdHidden"></asp:BoundField>
                        <asp:TemplateField HeaderText="DC No" SortExpression="DC_NO">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDCNo" OnClick="lbtnDCNo_Click" ForeColor="#0066ff" runat="server" Text='<%# Bind("DC_NO") %>' CausesValidation="False" __designer:wfdid="w24"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SalesInvoiceNo" SortExpression="SI_NO">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("SI_NO") %>'></asp:TextBox>

                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnSalesInvoiceNo" OnClick="lbtnSalesInvoiceNo_Click" ForeColor="#0066ff" runat="server"
                                    Text='<%# Bind("SI_NO") %>' CausesValidation="False"></asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HtmlEncode="False" SortExpression="SI_DATE" DataFormatString="{0:dd/MM/yyyy}" DataField="SI_DATE" HeaderText="InvoiceDate"></asp:BoundField>
                        <asp:BoundField DataField="DC_ID" SortExpression="DC_ID" HeaderText="DCIdHidden"></asp:BoundField>
                        <asp:BoundField DataField="DC_FOR" SortExpression="DC_FOR" HeaderText="DCfORHidden"></asp:BoundField>
                        <asp:BoundField DataField="FORSALESCUST" HeaderText="Customer" SortExpression="FORSALESCUST">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="FORSPARESCUST" SortExpression="FORSPARESCUST" HeaderText="Customer">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="FORSALESCUSTCON" SortExpression="FORSALESCUSTCON" HeaderText="ContactPerson">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="FORSPARESCUSTCON" HeaderText="ContactPerson">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="SI_GROSS_AMT" SortExpression="SI_GROSS_AMT" HeaderText="Amount"></asp:BoundField>
                        <asp:BoundField DataField="PREPAREDBY" SortExpression="PREPAREDBY" HeaderText="PreparedBy"></asp:BoundField>
                        <asp:BoundField DataField="APPROVEDBY" SortExpression="APPROVEDBY" HeaderText="ApprovedBy"></asp:BoundField>
                        <asp:BoundField DataField="CP_SHORT_NAME" SortExpression="CP_SHORT_NAME" HeaderText="Company Name"></asp:BoundField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="sdsSalesInvoiceDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_INVENTORY_SALESINVOICE_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="EMPID" ControlID="lblEmpIdHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>
                        <asp:ControlParameter ControlID="lblCPID" DefaultValue="0" Name="CPID" PropertyName="Text" Type="Int64" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="4"></td>
        </tr>
        <tr>
            <td colspan="4">
                <table id="Table1">
                    <tr>
                        <td>
                            <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CausesValidation="False" /></td>
                        <td>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CausesValidation="False" Visible="False" /></td>
                        <td style="width: 58px">
                            <asp:Button ID="btnDelete" runat="server" Visible="false" Text="Delete" OnClick="btnDelete_Click"
                                CausesValidation="False" />

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table border="0" cellpadding="0" cellspacing="0" id="tblSIDetails" runat="server"
                    visible="true" width="100%">
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">General Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblSearch" runat="server" Text="Search"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtSearchModel" runat="server">
                            </asp:TextBox><asp:Button ID="btnSearchModelNo" runat="server" BorderStyle="None"
                                CausesValidation="False" CssClass="gobutton" EnableTheming="False" OnClick="btnSearchModelNo_Click"
                                Text="Go" />
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                SelectCommand="SP_Customer_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtSearchModel" Name="SearchValue" PropertyName="Text"
                                        Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left; width: 439px;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblCustomer" runat="server" Text="Customer Name"></asp:Label>
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomerName_SelectedIndexChanged">
                            </asp:DropDownList>&nbsp;<asp:Label ID="Label16" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>&nbsp;<asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCustomerName"
                                    ErrorMessage="Please Enter the Customer Name" InitialValue="0" ValidationGroup="main">*</asp:RequiredFieldValidator><asp:TextBox ID="txtCustomerName" runat="server" ReadOnly="True" Visible="False"></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblRegion" runat="server" Text="Region"></asp:Label></td>
                        <td style="text-align: left; width: 439px;">
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
                        <td style="text-align: left; width: 439px;">
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
                        <td style="text-align: left; width: 439px;">
                            <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 22px; text-align: right">
                            <asp:Label ID="Label35" runat="server" Text="UnitName"></asp:Label></td>
                        <td style="height: 22px; text-align: left">
                            <asp:DropDownList ID="ddlunitname" runat="server" OnSelectedIndexChanged="ddlunitname_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList></td>
                        <td style="height: 22px; text-align: right">
                            <asp:Label ID="Label36" runat="server" Text="UnitName"></asp:Label></td>
                        <td style="width: 439px; height: 22px; text-align: left">
                            <asp:TextBox ID="txtUnitaddress" runat="server" TextMode="MultiLine">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 5px;">
                            <asp:Label ID="lblSalesOrderNo" runat="server" Text="Sales Order No" Width="101px"></asp:Label></td>
                        <td style="text-align: left; height: 5px;">
                            <asp:DropDownList ID="ddlSalesOrderNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSalesOrderNo_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label ID="Label14" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSalesOrderNo"
                                ErrorMessage="Please Enter the Sales Order No" InitialValue="0" ValidationGroup="main">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right; height: 5px;">
                            <asp:Label ID="lblSalesOrderDate" runat="server" Text="Sales Order Date" Width="114px">
                            </asp:Label></td>
                        <td style="text-align: left; height: 5px; width: 439px;">
                            <asp:TextBox ID="txtSalesOrderDate" runat="server" ReadOnly="True">
                            </asp:TextBox><asp:Image ID="imgSalesOrderDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                Visible="False"></asp:Image><cc1:CalendarExtender Format="dd/MM/yyyy" ID="CeSalesOrderDate" runat="server"
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
                            <asp:Label ID="Label1" runat="server" Text="Delivery Challan No" Width="127px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlDeviveryNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDeviveryNo_SelectedIndexChanged">
                            </asp:DropDownList><asp:Label ID="Label26" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlDeviveryNo"
                                    ErrorMessage="Please Select the Delivery Challan No" InitialValue="0" ValidationGroup="main">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label2" runat="server" Text="Delivery Challan Date" Width="146px"></asp:Label></td>
                        <td style="text-align: left; width: 439px;">
                            <asp:TextBox ID="txtChallanDate" runat="server" ReadOnly="True">
                            </asp:TextBox><asp:Image ID="imgChallanDate" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False" /><cc1:CalendarExtender
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
                            <asp:Label ID="Label33" runat="server" Text="InvoiceNo" Width="101px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtInno" runat="server">
                            </asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtInno"
                                    ErrorMessage="Please enter Invoice No." InitialValue="0" ValidationGroup="main">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left; width: 439px;"></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            <asp:Label ID="lblOrderedItemsHeading" runat="server" EnableTheming="False"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:GridView ID="gvItemDetails" runat="server" AutoGenerateColumns="False" Width="100%"
                                OnRowDataBound="gvItemDetails_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                    <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                    <asp:BoundField DataField="Rate" HeaderText="MRP"></asp:BoundField>
                                    <asp:BoundField HeaderText="UnitPrice"></asp:BoundField>
                                    <asp:BoundField DataField="Price" HeaderText="Amount"></asp:BoundField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <span style="color: #ff0033">No Data to Dispaly</span>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left">
                            <asp:Label ID="Label31" runat="server" Text="PO Terms & Conditions :"></asp:Label>
                            <asp:Label ID="lblTerms" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4">Invoiced Items</td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:GridView ID="gvInvoicedItems" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvItmDetails_RowDeleting"
                                OnRowDataBound="gvInvoicedItems_RowDataBound" OnRowEditing="gvItmDetails_RowEditing">
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
                                    <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="DeliveryDate" HeaderText="Delivery Date"></asp:BoundField>
                                    <asp:BoundField DataField="DC_NO" HeaderText="DC_NO"></asp:BoundField>
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <span style="color: #ff0033">No Data to Dispaly</span>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">Delivery Challan Items</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:GridView ID="gvDeliveryChallanItems" runat="server" AutoGenerateColumns="False"
                                Width="100%" OnRowDataBound="gvDeliveryChallanItems_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="DC No" HeaderText="DC No"></asp:BoundField>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                    <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                    <asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
                                    <asp:BoundField HeaderText="UnitPrice"></asp:BoundField>
                                    <asp:BoundField HeaderText="Amount"></asp:BoundField>
                                    <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="DeliveryDate" HeaderText="Delivery Date"></asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkItemSelect" runat="server" __designer:wfdid="w31"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SPPrice" HeaderText="SPPrice"></asp:BoundField>
                                    <asp:BoundField DataField="POQty" HeaderText="POQty"></asp:BoundField>
                                    <asp:BoundField DataField="ColorId" HeaderText="ColorId"></asp:BoundField>
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <span style="color: #ff0033">No Data to Dispaly</span>
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <asp:Button ID="btnGo" runat="server" CausesValidation="False" OnClick="btnGo_Click"
                                Text="Go" /></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">Invoice Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right;"></td>
                        <td style="text-align: left;"></td>
                        <td style="text-align: right;"></td>
                        <td style="text-align: left; width: 439px;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label3" runat="server" Text="Invoice No"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtInvoiceNo" runat="server" >
                            </asp:TextBox><asp:Label ID="Label12" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                    ID="rfvInvoiceNo" runat="server" ControlToValidate="txtInvoiceNo" ErrorMessage="Please Enter the Delivery Challan No">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label6" runat="server" Text="Invoice Date"></asp:Label></td>
                        <td style="text-align: left; width: 439px;">
                            <asp:TextBox ID="txtInvoiceDate" runat="server">
                            </asp:TextBox>&nbsp;<asp:Image ID="imgInvoiceDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>
                            <asp:Label ID="Label15" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvInvoiceDate" runat="server" ControlToValidate="txtInvoiceDate"
                                ErrorMessage="Please Enter the Delivery Challan No">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="DateCustomValidate"
                                ControlToValidate="txtInvoiceDate" ErrorMessage="Please Enter the Invoice Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                SetFocusOnError="True">*</asp:CustomValidator>
                            <cc1:CalendarExtender Format="dd/MM/yyyy" ID="CeInvoiceDate" runat="server" Enabled="True"
                                PopupButtonID="imgInvoiceDate" TargetControlID="txtInvoiceDate">
                            </cc1:CalendarExtender>
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
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem>Tax Invoice</asp:ListItem>
                                <asp:ListItem>Pro-Forma Tax Invoice</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="Label13" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlInvoiceType"
                                ErrorMessage="Please Select the Invoice Type">*</asp:RequiredFieldValidator></td>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label8" runat="server" Text="Delivery Type"></asp:Label></td>
                        <td style="height: 19px; text-align: left; width: 439px;">
                            <asp:DropDownList ID="ddlDeliveryType" runat="server">
                            </asp:DropDownList><asp:Label ID="Label22" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlDeliveryType"
                                ErrorMessage="Please Select the Delivery Type " InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">&nbsp;</td>
                        <td style="text-align: left">&nbsp;</td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left; width: 439px;">&nbsp;
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
                        <td style="text-align: left; width: 439px;">&nbsp;<asp:TextBox ID="txtItemname" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label27" runat="server" Text="UOM"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtItemUOM" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label59" runat="server" Text="Color :"></asp:Label></td>
                        <td style="text-align: left; width: 439px;">
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
                        <td style="text-align: left; width: 439px;">
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
                        <td style="text-align: right;">
                            <asp:Label ID="Label17" runat="server" Text="Discount"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtDiscount1" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblAmount" runat="server" Text="Amount"></asp:Label></td>
                        <td style="text-align: left; width: 439px;">
                            <asp:TextBox ID="txtAmount" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label28" runat="server" Text="Delivery Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDeliveryDate" runat="server"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label30" runat="server" Text="Spl Price"></asp:Label></td>
                        <td style="text-align: left; width: 439px;">
                            <asp:TextBox ID="txtSpPrice" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label32" runat="server" Text="Remarks"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label34" runat="server" Text="UnitPrice"></asp:Label></td>
                        <td style="width: 439px; text-align: left">
                            <asp:TextBox ID="txtUnitprice" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 18px">
                            <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" Text="Add" OnClick="btnAdd_Click"
                                ValidationGroup="id" /><asp:Button ID="btnItemRefresh" runat="server" BackColor="Transparent"
                                    BorderStyle="None" CssClass="loginbutton" EnableTheming="False" OnClick="btnItemRefresh_Click"
                                    Text="Refresh" CausesValidation="False" /></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:GridView ID="gvItmDetails" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvItmDetails_RowDeleting"
                                OnRowDataBound="gvItmDetails_RowDataBound" OnRowEditing="gvItmDetails_RowEditing">
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
                                    <asp:BoundField DataField="SPPrice" HeaderText="UnitPrice"></asp:BoundField>
                                    <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="DeliveryDate" HeaderText="Delivery Date"></asp:BoundField>
                                    <asp:BoundField DataField="DCId" HeaderText="DCId"></asp:BoundField>
                                    <asp:BoundField DataField="ColorId" HeaderText="ColorId"></asp:BoundField>
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <span style="color: #ff0033">No Data to Dispaly</span>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4">Other Charges</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left; width: 439px;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;"></td>
                        <td style="text-align: left;"></td>
                        <td style="text-align: right;"></td>
                        <td style="text-align: left; width: 439px;">
                            <asp:RadioButton ID="rbVAT" runat="server" Checked="True" GroupName="vatcst" Text="VAT" /><asp:RadioButton
                                ID="rbCST" runat="server" GroupName="vatcst" Text="C.S. Tax" /></td>
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
                            <asp:Label ID="lblVAT" runat="server" Text="VAT"></asp:Label><asp:Label ID="lblCSTax"
                                runat="server" Text="C.S. Tax"></asp:Label></td>
                        <td style="text-align: left; width: 439px;">
                            <asp:TextBox ID="txtVAT" runat="server">
                            </asp:TextBox><asp:TextBox ID="txtCST" runat="server">
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
                        <td style="text-align: left; width: 439px;">
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
                            <asp:TextBox ID="txtRemarks1" runat="server" Height="53px" TextMode="MultiLine" Width="673px"
                                CssClass="textbox" EnableTheming="False">_</asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left; height: 14px;">Reference Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left; width: 439px;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By" Visible="False"></asp:Label></td>
                        <td style="text-align: left; width: 439px;">
                            <asp:DropDownList ID="ddlApprovedBy" runat="server" Enabled="False" Visible="False">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;"></td>
                        <td style="text-align: left;"></td>
                        <td style="text-align: right;"></td>
                        <td style="text-align: left; width: 439px;"></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table id="tblButtons">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="main" /></td>
                                    <td>
                                        <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click"
                                            CausesValidation="False" Visible="False" /></td>
                                    <td>
                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click"
                                            CausesValidation="False" /></td>
                                    <td>
                                        <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" /></td>
                                    <td>
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" CausesValidation="False" /></td>
                                    <td style="width: 3px">
                                        <asp:Button ID="btnStatement" runat="server" Text="Statement"
                                            CausesValidation="False" OnClick="btnStatement_Click" Visible="False" /></td>
                                </tr>
                            </table>
                            <asp:RadioButtonList ID="rbtnListStatement" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbtnListStatement_SelectedIndexChanged"
                                RepeatDirection="Horizontal" Visible="False">
                                <asp:ListItem>Delivered</asp:ListItem>
                                <asp:ListItem>Yet to Deliver</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                </table>
                <asp:TextBox ID="txtExcise" runat="server" Visible="False">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPaymentreceived" runat="server" Visible="False"></asp:Label></td>
            <td></td>
            <td style="text-align: right;"></td>
            <td></td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="id" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="main" ShowMessageBox="true" ShowSummary="false" />

</asp:Content>

 
