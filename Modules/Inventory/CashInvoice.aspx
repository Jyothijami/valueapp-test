<%@ Page Language="C#" MasterPageFile="~/MPs/FinanceMP1.master" AutoEventWireup="true" CodeFile="CashInvoice.aspx.cs"
    Inherits="Modules_Inventory_CashInvoice" Title="|| Value App : Finance : CashInvoice ||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <script>
        $(function () {
            $("[name$='txtChallanDate'],[name$='txtInvoiceDate']").datepicker();
        });
    </script>
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
        function GST_amtcalc() {
            var spprice;
            var gst_Amt, gst_Tax;

            gst_Tax = document.getElementById('<%=txtDetGST.ClientID %>').value;
            spprice = document.getElementById('<%=txtAmount.ClientID %>').value;

    if (gst_Tax == "" || gst_Tax == "0") {
        document.getElementById('<%=txtDetGSTAmt.ClientID %>').value = "0";
            }
            else if (spprice == "" || spprice == "0") {
                document.getElementById('<%=txtDetGST.ClientID %>').value = "0";
            }
            else if (spprice > 0 && gst_Tax > 0) {
                document.getElementById('<%=txtDetGSTAmt.ClientID %>').value = parseFloat((spprice * gst_Tax) / 100).toFixed(2);
    }
        }
        function Gst_Disc_calc() {

            var spprice;
            var gst_Amt, gst_Tax;

            gst_Amt = document.getElementById('<%=txtDetGSTAmt.ClientID %>').value;
            spprice = document.getElementById('<%=txtAmount.ClientID %>').value;

            if (gst_Amt == "" || gst_Amt == "0") {
                document.getElementById('<%=txtDetGST.ClientID %>').value = "0";
            }
            else if (spprice == "" || spprice == "0") {
                document.getElementById('<%=txtDetGST.ClientID %>').value = "0";
            }
            else if (spprice > 0 && gst_Amt > 0) {

                document.getElementById('<%=txtDetGST.ClientID %>').value = parseFloat((gst_Amt * 100) / spprice).toFixed(2);
            }
        }
            function grosscalc() {
                var vat, cst, disc, grossamt, misc, transchargs, TOTAL, bt, oldtax;
                bt = document.getElementById('<%=txtBranchTransfer.ClientID %>').value;
                cst = document.getElementById('<%=txtCST.ClientID %>').value;
                vat = document.getElementById('<%=txtVAT.ClientID %>').value;
                misc = parseFloat(document.getElementById('<%=txtMiscelleneous.ClientID %>').value);
                disc = parseFloat(document.getElementById('<%=txtDiscount.ClientID %>').value);
                grossamt = parseFloat(document.getElementById('<%=txtGrossTotalAmtHidden.ClientID %>').value);
                oldtax = parseFloat(document.getElementById('<%=txtTax_Old.ClientID %>').value);
                if (bt == "" || bt == "0" || isNaN(bt)) { bt = "0"; }
                if (cst == "" || cst == "0" || isNaN(cst)) { cst = "0"; }
                if (vat == "" || vat == "0" || isNaN(vat)) { vat = "0"; }
                if (grossamt == "" || grossamt == "0" || isNaN(grossamt)) { grossamt = "0"; }
                if (misc == "" || misc == "0" || isNaN(misc)) { misc = "0"; }
                if (disc == "" || disc == "0" || isNaN(disc)) { disc = "0"; }
                if (oldtax == "" || oldtax == "0" || isNan(oldtax)) { oldtax = "0"; }
                TOTAL = parseFloat(grossamt);
                TOTAL = TOTAL + parseFloat(vat);
                TOTAL = TOTAL + parseFloat(cst);
                TOTAL = TOTAL + parseFloat(bt);
                TOTAL = TOTAL + parseFloat(misc);
                TOTAL = TOTAL + ((oldtax * TOTAL) / 100);
                TOTAL = TOTAL - ((disc * TOTAL) / 100);

                TOTAL = Math.round(TOTAL * 100) / 100;
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
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
                <tr>
                    <td style="text-align: left">Cash & Sample  Invoice</td>
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
            <table style="width: 100%">
                <tr>
                    <td colspan="3">
                        <table id="Table2" runat="server" border="0" cellpadding="0" cellspacing="0"
                            visible="true" width="100%">
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tblMain">
                                        <tr>
                                            <td class="searchhead">
                                                <table id="TABLE2" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td style="text-align: left"></td>
                                                        <td></td>
                                                        <td style="text-align: right">
                                                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                                                <tr>
                                                                    <td style="height: 25px">
                                                                        <asp:Label ID="Label20" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                                                            Text="Search By"></asp:Label></td>
                                                                    <td style="height: 25px">
                                                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">--</asp:ListItem>
                                                                            <asp:ListItem Value="SI_NO">Sales Invoice No.</asp:ListItem>
                                                                            <asp:ListItem Value="SI_DATE">Invoice Date</asp:ListItem>
                                                                            <asp:ListItem Value="CUST_NAME">Contact Person</asp:ListItem>

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
                                                                        <asp:TextBox ID="txtSearchValueFromDate" type="date" runat="server" EnableTheming="True" Visible="False"
                                                                            Width="106px">
                                                                        </asp:TextBox>
                                                                        <cc1:CalendarExtender ID="ceSearchFrom" runat="server" Enabled="False" Format="dd/MM/yyyy"
                                                                            PopupButtonID="imgFromDate" TargetControlID="txtSearchValueFromDate">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                    <td style="height: 25px">
                                                                        <asp:TextBox ID="txtSearchValueToDate" type="date" runat="server" EnableTheming="True" Visible="False"
                                                                            Width="106px">
                                                                        </asp:TextBox></td>
                                                                    <td style="height: 25px">
                                                                        <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                                                        </asp:Label></td>
                                                                    <td style="height: 25px">
                                                                        <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                                                        </asp:TextBox>
                                                                        <cc1:CalendarExtender ID="ceSearchValueToDate" runat="server" Enabled="False" Format="dd/MM/yyyy"
                                                                            PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                    <td style="height: 25px">
                                                                        <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                                                            CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                                                                </tr>
                                                            </table>

                                                            <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                                                Visible="False"></asp:Label>
                                                            <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                                                Visible="False"></asp:Label><asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label><asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                                                            <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                                                            <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                                                            <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gvSalesInvoiceDetails" runat="server" DataSourceID="SqlDataSource1" SelectedRowStyle-BackColor="#c0c0c0" Width="100%" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" OnRowDataBound="gvSalesInvoiceDetails_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="SI_ID" HeaderText="SalesInvoiceIdHidden"></asp:BoundField>
                                                        <asp:TemplateField HeaderText="SalesInvoiceNo">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("SI_NO") %>'></asp:TextBox>

                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnSalesInvoiceNo" ForeColor="#0066ff" OnClick="lbtnSalesInvoiceNo_Click" runat="server" Text='<%# Bind("SI_NO") %>' CausesValidation="False" __designer:wfdid="w13"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="SI_DATE" HeaderText="InvoiceDate"></asp:BoundField>
                                                        <asp:BoundField DataField="CUST_NAME" HeaderText="ContactPerson">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SI_GROSS_AMT" HeaderText="Amount"></asp:BoundField>
                                                        <asp:BoundField DataField="EMP_FIRST_NAME" SortExpression="EMP_FIRST_NAME" HeaderText="ApprovedBy"></asp:BoundField>
                                                        <asp:BoundField DataField="CP_SHORT_NAME" SortExpression="CP_SHORT_NAME" HeaderText="COMPANY"></asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                                    SelectCommand="USP_INVENTORY_SampleSALEINVOICE_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName" PropertyName="Text" Type="String" />
                                                        <asp:ControlParameter ControlID="lblSearchTypeHidden" DefaultValue="0" Name="SearchType" PropertyName="Text" Type="String" />
                                                        <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue" PropertyName="Text" Type="String" />
                                                        <asp:ControlParameter ControlID="lblSearchValueFromHidden" DefaultValue="0" Name="SearchValueFrom" PropertyName="Text" Type="String" />
                                                        <asp:ControlParameter ControlID="lblEmpIdHidden" DefaultValue="0" Name="EMPID" PropertyName="Text" Type="Int64" />
                                                        <asp:ControlParameter ControlID="lblUserType" DefaultValue="0" Name="UserType" PropertyName="Text" Type="Int64" />
                                                        <asp:ControlParameter ControlID="lblCPID" DefaultValue="0" Name="CPID" PropertyName="Text" Type="Int64" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </td>
                                        </tr>
                                        <tr>

                                            <td style="text-align: center">
                                                <table id="Table1">
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CausesValidation="False" />

                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CausesValidation="False" OnClick="btnEdit_Click" />

                                                        </td>
                                                        <td style="width: 58px">
                                                            <asp:Button ID="btnDelete" runat="server" CausesValidation="False" OnClick="btnDelete_Click" Text="Delete" /></td>
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
                                                            </asp:TextBox>

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
                                                                    <asp:BoundField DataField="GST_TAX" HeaderText="GST(%)"></asp:BoundField>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    <span style="color: #ff0033">No Data to Dispaly</span>
                                                                </EmptyDataTemplate>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="profilehead" colspan="4" style="text-align: left">Invoice Details</td>
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
                                                            <asp:TextBox ID="txtInvoiceDate" runat="server">
                                                            </asp:TextBox>&nbsp;
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
                                                        <td style="text-align: right">
                                                            <asp:Label ID="Label5" runat="server" Text="Invoice Type"></asp:Label></td>
                                                        <td style="text-align: left">
                                                            <asp:DropDownList ID="ddlInvoiceType" runat="server">
                                                                <asp:ListItem Value="0">--</asp:ListItem>
                                                                <asp:ListItem>Tax Invoice</asp:ListItem>
                                                                <asp:ListItem>Pro-Forma Tax Invoice</asp:ListItem>
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
                                                            <asp:Label ID="Label16" runat="server" Text="InvoiceNo"></asp:Label>
                                                            &nbsp;</td>
                                                        <td style="text-align: left">&nbsp;<asp:TextBox ID="txtInvo" runat="server"></asp:TextBox></td>
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
                                                        <td style="text-align:right">
                                                            <asp:Label ID="lblDetGST" runat="server" Text="GST(%)"></asp:Label>
                                                        </td>
                                                        <td style="text-align:left">
                                                            <asp:TextBox id="txtDetGST" runat="server" ></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right"><asp:Label ID="lblDetGSTAmount" runat ="server" Text="GST Amount"></asp:Label></td>
                                                        <td style="text-align: left"><asp:TextBox ID="txtDetGSTAmt" runat ="server"></asp:TextBox></td>
                                                       <td style="text-align: right; height: 24px;">
                                                            <asp:Label ID="Label14" runat="server" Text="Remarks"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 423px; height: 24px;">
                                                            <asp:TextBox ID="txtRemarks1" runat="server" TextMode="MultiLine">
                                                            </asp:TextBox></td>
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
                                                            <asp:GridView ID="gvItmDetails" SelectedRowStyle-BackColor="#c0c0c0" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvItmDetails_RowDeleting"
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
                                                                    <asp:BoundField DataField="Vat" HeaderText="GST(%)"></asp:BoundField>
                                                                    <asp:BoundField DataField="CST" HeaderText="CST"></asp:BoundField>
                                                                    <asp:BoundField DataField="Excise" HeaderText="Excise"></asp:BoundField>
                                                                    <asp:BoundField HeaderText="Amount"></asp:BoundField>
                                                                    <asp:BoundField HeaderText="GST Amount"></asp:BoundField>
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
                                                        <td class="profilehead" colspan="4">Other Charges</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 19px; text-align: right"></td>
                                                        <td style="height: 19px; text-align: left"></td>
                                                        <td style="height: 19px; text-align: right">&nbsp;</td>
                                                        <td style="height: 19px; text-align: left; width: 423px;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;"></td>
                                                        <td style="text-align: left;"></td>
                                                        <td style="text-align: right;"></td>
                                                        <td style="text-align: left; width: 423px;">
                                                            <asp:RadioButton ID="rbVAT" runat="server" Checked="True" GroupName="vatcst" Text="CGST/IGST" /><asp:RadioButton
                                                                ID="rbCST" runat="server" GroupName="vatcst" Text="IGST" />
                                                            <asp:RadioButton ID="rbBranchTransfer" runat="server" GroupName="vatcst" Text="Branch Transfer" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="Label24" runat="server" Text="Total"></asp:Label></td>
                                                        <td style="text-align: left;">
                                                            <asp:TextBox ID="txtTotalAmt" runat="server" ReadOnly="True"></asp:TextBox><br/>
                                    <asp:TextBox ID="txtTax_Old" runat ="server" ReadOnly ="true" ></asp:TextBox><asp:Label ID="lbltax" runat ="server" Text="(Old VAT/C.s Tax Details if any)" ForeColor="#CC3300"></asp:Label>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtMiscelleneous"
                                                                ValidChars=".0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="lblBranchTransfer" runat="server" Text="Branch Transfer"></asp:Label>
                                                            <asp:Label ID="lblVAT" runat="server" Text="CGST/SGST"></asp:Label><asp:Label ID="lblCSTax"
                                                                runat="server" Text="IGST"></asp:Label></td>
                                                        <td style="text-align: left; width: 423px;">
                                                            <asp:TextBox ID="txtBranchTransfer" runat="server" autocomplete="off">
                                                            </asp:TextBox>
                                                            <asp:TextBox ID="txtVAT" runat="server" autocomplete="off">
                                                            </asp:TextBox><asp:TextBox ID="txtCST" runat="server" autocomplete="off">
                                                            </asp:TextBox><asp:Label ID="Label25" runat="server" EnableTheming="False" Font-Bold="False"
                                                                Font-Names="Verdana" Font-Size="Smaller" Text="(GST Value)"></asp:Label><asp:Label ID="Label21"
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
                                                            <asp:Label ID="lblApprovedBy" runat="server" Text="Sales Executive Name"></asp:Label></td>
                                                        <td style="text-align: left; width: 423px;">
                                                            <asp:DropDownList ID="ddlApprovedBy" runat="server" >
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
                                                            <table id="tblButtons">
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
                                                </asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>

                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPaymentreceived" runat="server" Visible="False"></asp:Label></td>
                    <td></td>
                    <td style="text-align: right;"></td>
                    <td></td>
                </tr>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="id" />
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


 
