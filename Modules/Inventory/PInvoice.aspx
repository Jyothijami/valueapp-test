<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/FinanceMP1.master" AutoEventWireup="true" CodeFile="PInvoice.aspx.cs" Inherits="Modules_Inventory_PInvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
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
    <script>
        function grosscalc() {


            var gst, vat, cst, disc, grossamt, misc, transchargs, TOTAL, bt;

            gst = document.getElementById('<%=txtGSTTotal.ClientID %>').value;
            misc = parseFloat(document.getElementById('<%=txtMiscelleneous.ClientID %>').value);
            disc = parseFloat(document.getElementById('<%=txtDiscount.ClientID %>').value);
            grossamt = parseFloat(document.getElementById('<%=txtTotalAmt.ClientID %>').value);
            //grossamt = parseFloat(document.getElementById('<%=txtGrossTotalAmtHidden.ClientID %>').value);

            if (gst == "" || gst == "0" || isNaN(gst)) { gst = "0"; }
            if (grossamt == "" || grossamt == "0" || isNaN(grossamt)) { grossamt = "0"; }
            if (misc == "" || misc == "0" || isNaN(misc)) { misc = "0"; }
            if (disc == "" || disc == "0" || isNaN(disc)) { disc = "0"; }

            bt = parseFloat(gst);

            TOTAL = parseFloat(grossamt);
            TOTAL = bt + TOTAL;

            TOTAL = TOTAL + parseFloat(misc);

            //alert(TOTAL);

            TOTAL = TOTAL - ((disc * TOTAL) / 100);

            TOTAL = Math.round(TOTAL * 100) / 100;

            document.getElementById('<%=txtGrossAmount.ClientID %>').value = parseFloat(TOTAL);

        }

        function amtcalcDisc() {

            var req_qty, rate, spprice, splamt;

            req_qty = document.getElementById('<%=txtQuantity.ClientID %>').value;
            rate = document.getElementById('<%=txtRate.ClientID %>').value;
            spprice = document.getElementById('<%=txtSpPrice.ClientID %>').value;

            if (req_qty == "" || req_qty == "0") {
                document.getElementById('<%=txtSpPrice.ClientID %>').value = "0";
            }
            else if (rate == "" || rate == "0") {
                document.getElementById('<%=txtSpPrice.ClientID %>').value = "0";
            }
            else if (rate > 0 && req_qty > 0) {
                document.getElementById('<%=txtDiscount1.ClientID %>').value = (((rate * req_qty) - spprice) * 100) / (rate * req_qty);
      }
    splamt = document.getElementById('<%=txtSpPrice.ClientID %>').value;
            document.getElementById('<%=txtUnitprice.ClientID %>').value = splamt / req_qty;

        }



        function amtcalcDisc1() {

            var req_qty, rate, spprice, splamt, unitprice, amt;

            req_qty = document.getElementById('<%=txtQuantity.ClientID %>').value;
            rate = document.getElementById('<%=txtRate.ClientID %>').value;
            spprice = document.getElementById('<%=txtSpPrice.ClientID %>').value;
            unitprice = document.getElementById('<%=txtUnitprice.ClientID %>').value;

            if (req_qty == "" || req_qty == "0") {
                document.getElementById('<%=txtSpPrice.ClientID %>').value = "0";
            }
            else if (rate == "" || rate == "0") {
                document.getElementById('<%=txtSpPrice.ClientID %>').value = "0";
      }
      else if (rate > 0 && req_qty > 0) {
          document.getElementById('<%=txtDiscount1.ClientID %>').value = (((rate) - unitprice) * 100) / (rate);

           disc = document.getElementById('<%=txtDiscount1.ClientID %>').value;
           amt = unitprice * req_qty;

                <%--document.getElementById('<%=txtSpPrice.ClientID %>').value = amt - (disc * amt) / 100;--%>
           document.getElementById('<%=txtSpPrice.ClientID %>').value = amt;

       }

}

        function amtcalc() {

            var req_qty, rate, amt, disc, splamt;

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
    amt = document.getElementById('<%=txtAmount.ClientID %>').value;
    disc = document.getElementById('<%=txtDiscount1.ClientID %>').value;

    if (amt == "" || amt == "0") {
        document.getElementById('<%=txtSpPrice.ClientID %>').value = "0";
        document.getElementById('<%=txtUnitprice.ClientID %>').value = "0";
    }
    else if (disc == "" || disc == "0") {
        document.getElementById('<%=txtSpPrice.ClientID %>').value = amt;
      }
      else if (disc > 0 && amt > 0) {
          document.getElementById('<%=txtSpPrice.ClientID %>').value = amt - (disc * amt) / 100;
    }
    splamt = document.getElementById('<%=txtSpPrice.ClientID %>').value;
    document.getElementById('<%=txtUnitprice.ClientID %>').value = splamt / req_qty;
}

    </script>
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

                </asp:DropDownList></td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">

        <tr>
            <td id="TD1"></td>
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
                                        <asp:Label ID="Label20" CssClass="label" runat="server" EnableTheming="False" Font-Bold="True" Text="Search By" meta:resourcekey="Label20Resource1" Height="16px"></asp:Label>

                                    </td>
                                    <td style="height: 25px">
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged" meta:resourcekey="ddlSearchByResource1">
                                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource1">--</asp:ListItem>
                                            <asp:ListItem Value="PI_NO" meta:resourcekey="ListItemResource2">PI NO.</asp:ListItem>
                                            <asp:ListItem Value="PI_DATE" meta:resourcekey="ListItemResource3">PI Date</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME" meta:resourcekey="ListItemResource4">Customer Name</asp:ListItem>
                                            <asp:ListItem Value="CUST_CONTACT_PERSON" meta:resourcekey="ListItemResource5">Contact Person</asp:ListItem>
                                            <%--<asp:ListItem Value="SUP_EMAIL" meta:resourcekey="ListItemResource6">Enquiry From</asp:ListItem>--%>
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
                                        <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False" meta:resourcekey="lblCurrentFromDateResource1"></asp:Label>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchValueFromDate" runat="server" type="datepic" EnableTheming="True" Visible="False"
                                            Width="106px" meta:resourcekey="txtSearchValueFromDateResource1"></asp:TextBox></td>
                                    <td style="height: 25px">
                                        <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False" meta:resourcekey="lblCurrentToDateResource1"></asp:Label>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchValueToDate" runat="server" type="datepic" EnableTheming="True" Visible="False"
                                            Width="106px" meta:resourcekey="txtSearchValueFromDateResource1"></asp:TextBox></td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px" meta:resourcekey="txtSearchTextResource1"></asp:TextBox>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" meta:resourcekey="btnSearchGoResource1" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False" meta:resourcekey="lblEmpIdHiddenResource1"></asp:Label>
                            <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1" Visible="False"></asp:Label>
                            <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1" Visible="False"></asp:Label><asp:Label ID="lblSearchItemHidden" runat="server" Visible="False" meta:resourcekey="lblSearchItemHiddenResource1"></asp:Label>
                            <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False" meta:resourcekey="lblSearchTypeHiddenResource1"></asp:Label>
                            <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False" meta:resourcekey="lblSearchValueFromHiddenResource1"></asp:Label>
                            <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False" meta:resourcekey="lblSearchValueHiddenResource2"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td id="TD3" runat="server" colspan="4" style="text-align: center">
                <asp:GridView ID="gvProformaInvoice" runat="server" AllowPaging="True" AutoGenerateColumns="false"
                    Width="100%" DataSourceID="sqlPISearchDetails" SelectedRowStyle-BackColor="#c0c0c0"
                    AllowSorting="True" OnPageIndexChanging ="gvProformaInvoice_PageIndexChanging"
                    OnRowDataBound="gvProformaInvoice_RowDataBound" >
                    <Columns>
                        <asp:BoundField DataField="PI_ID" SortExpression="PI_ID" HeaderText="PIIdHidden" meta:resourceKey="BoundFieldResource1" />
                        <asp:TemplateField SortExpression="PI_NO" HeaderText="Proforma Invoice No" meta:resourceKey="TemplateFieldResource1">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("PI_NO") %>' ForeColor="#0066ff" ID="TextBox1" meta:resourceKey="TextBox1Resource1">"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnPINO" OnClick="lbtnPINO_Click" runat="server" ForeColor="#0066ff" Text='<%# Eval("PI_NO") %>' CausesValidation="False" meta:resourcekey="lbtnPINOResource1" __designer:wfdid="w1"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="PI_DATE" SortExpression="PI_DATE" HeaderText="PI Date" meta:resourceKey="BoundFieldResource2">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CUST_NAME" SortExpression="CUST_NAME" HeaderText="Customer Name" meta:resourceKey="BoundFieldResource3">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PI_TYPE" SortExpression="PI_TYPE" HeaderText="PI Type" meta:resourceKey="BoundFieldResource4">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="EMP_FIRST_NAME" SortExpression="EMP_FIRST_NAME" HeaderText="Prepared By" meta:resourceKey="BoundFieldResource11" />
                        <asp:BoundField DataField="CP_SHORT_NAME" SortExpression="CP_SHORT_NAME" HeaderText="Company" meta:resourceKey="BoundFieldResource5">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        <span style="color: #ff0000">No Record Found! </span>
                    </EmptyDataTemplate>
                </asp:GridView>
                <asp:SqlDataSource ID="sqlPISearchDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="[SP_INVENTORY_PI_SEARCH_SELECT]" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="EMPID" ControlID="lblEmpIdHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="CPID" ControlID="lblCPID" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td id="TD4" colspan="4" style="text-align: center">
                <table id="Table1" align="center">
                    <tr>
                        <td>
                            <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CausesValidation="False" meta:resourcekey="btnNewResource1" /></td>
                        <td>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CausesValidation="False" meta:resourcekey="btnEditResource1" /></td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>

                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td colspan="4">
                                    <table border="0" cellpadding="0" cellspacing="0" id="tblSIDetails" runat="server"
                                        visible="true" width="100%">
                                        <tr>
                                            <td colspan="4" style="text-align: left" class="profilehead">General Details</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4"><%--<asp:RadioButton ID="rdbQuot" runat="server" AutoPostBack="True" OnCheckedChanged="rdbQuot_CheckedChanged" Text="With Quot" Checked="false" />--%>
                                                <asp:RadioButton ID="rdbPO" runat="server" AutoPostBack="True" OnCheckedChanged="rdbPO_CheckedChanged" Text="With PO" GroupName="as" />
                                                <asp:RadioButton ID="rdbSelf" runat="server" AutoPostBack="True" OnCheckedChanged="rdbSelf_CheckedChanged" Text="WithOut PO" GroupName="as" /></td>
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
                                                <asp:Label ID="Label36" runat="server" Text="Unit Address"></asp:Label></td>
                                            <td style="width: 439px; height: 22px; text-align: left">
                                                <asp:TextBox ID="txtUnitaddress" runat="server" TextMode="MultiLine">
                                                </asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; height: 5px;">
                                                <asp:Label ID="lblSalesOrderNo" runat="server" Text="Sales Order No" Width="101px" Visible="false"></asp:Label></td>
                                            <td style="text-align: left; height: 5px;">
                                                <asp:DropDownList ID="ddlSalesOrderNo" Visible="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSalesOrderNo_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <%--<asp:Label ID="Label14" runat="server" EnableTheming="False" Font-Bold="False"
                                        Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSalesOrderNo"
                                        ErrorMessage="Please Enter the Sales Order No" InitialValue="0" ValidationGroup="main">*</asp:RequiredFieldValidator>--%></td>
                                            <td style="text-align: right; height: 5px;">
                                                <asp:Label ID="lblSalesOrderDate" Visible="false" runat="server" Text="Sales Order Date" Width="114px">
                                                </asp:Label></td>
                                            <td style="text-align: left; height: 5px; width: 439px;">
                                                <asp:TextBox ID="txtSalesOrderDate" Visible="false" runat="server" ReadOnly="True" type="datepic">
                                                </asp:TextBox>
                                                <%-- <asp:Image ID="imgSalesOrderDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                Visible="False"></asp:Image>
                            <cc1:CalendarExtender Format="dd/MM/yyyy" ID="CeSalesOrderDate" runat="server"
                                    Enabled="False" PopupButtonID="imgSalesOrderDate" TargetControlID="txtSalesOrderDate">
                                </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MeeSalesOrderDate" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSalesOrderDate"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                                            </td>
                                        </tr>

                                        <%--<tr>
                                <td style="text-align: right" class="auto-style1">
                                    <asp:Label ID="Label33" runat="server" Text="InvoiceNo" Width="101px" Visible="False"></asp:Label></td>
                                <td style="text-align: left" class="auto-style1">
                                    <asp:TextBox ID="txtInno" runat="server" CausesValidation="True" Visible="False">0</asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtInno" ErrorMessage="Please Enter Invoice No." ValidationGroup="main">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="text-align: right" class="auto-style1"></td>
                                <td style="text-align: left;" class="auto-style2"></td>
                            </tr>--%>
                                        <tr>
                                            <td class="profilehead" colspan="4" style="text-align: left">
                                                <asp:Label ID="lblOrderedItemsHeading" runat="server" EnableTheming="False"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: center">
                                                <asp:GridView ID="gvItemDetails_Invoiced" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    OnRowDataBound="gvItemDetails_Invoiced_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ModelNo" HeaderText="Model No">
                                                            <HeaderStyle HorizontalAlign="Center" />

                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="HSN_CODE" HeaderText="HSN Code">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ItemName" HeaderText="Item Name">
                                                            <HeaderStyle HorizontalAlign="Center" />

                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="UOM" HeaderText="UOM">
                                                            <HeaderStyle HorizontalAlign="Center" />

                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                                            <HeaderStyle HorizontalAlign="Center" />

                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Rate" HeaderText="MRP">
                                                            <HeaderStyle HorizontalAlign="Center" />

                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="GST_TAX" HeaderText="GST Rate">
                                                            <HeaderStyle HorizontalAlign="Center" />

                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="UnitPrice">
                                                            <HeaderStyle HorizontalAlign="Center" />

                                                        </asp:BoundField>
                                                        <%--<asp:BoundField HeaderText ="Amount">
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>--%>
                                                        <asp:BoundField DataField="Price" HeaderText="SPL Price">
                                                            <HeaderStyle HorizontalAlign="Center" />

                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                                                        <asp:BoundField DataField="ColorId" HeaderText="ColorId"></asp:BoundField>
                                                        <%--<asp:BoundField DataField="Remarks" HeaderText="Specifications">
                                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                                                 <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                                        </asp:BoundField>--%>

                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkhdr" runat="server" AutoPostBack="True" OnClick="selectAll(this)" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk" runat="server" __designer:wfdid="w4"></asp:CheckBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <span style="color: #ff0033">No Data to Dispaly</span>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: left">
                                                <%--<asp:Label ID="Label31" runat="server" Text="PO Terms & Conditions :"></asp:Label>--%>
                                    Remarks :
                                                <asp:Label ID="lblTerms" ForeColor="Black" Font-Bold="true" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: center">
                                                <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click" CausesValidation="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="profilehead" colspan="4">Invoiced Items</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: center">
                                                <asp:GridView ID="gvInvoicedItems" runat="server" AutoGenerateColumns="False"
                                                    OnRowDataBound="gvInvoicedItems_RowDataBound" Width="100%">
                                                    <Columns>
                                                        <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                                        <asp:CommandField ShowDeleteButton="True"></asp:CommandField>

                                                        <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ModelNo" HeaderText="Model No">
                                                            <HeaderStyle HorizontalAlign="Center" />

                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="HSN_CODE" HeaderText="HSNCode">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ItemName" HeaderText="Item Name">
                                                            <HeaderStyle HorizontalAlign="Center" />

                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="UOM" HeaderText="UOM">
                                                            <HeaderStyle HorizontalAlign="Center" />

                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                                            <HeaderStyle HorizontalAlign="Center" />

                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Rate" HeaderText="Rate">
                                                            <HeaderStyle HorizontalAlign="Center" />

                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="GST_RATE" HeaderText="GST Rate">
                                                            <HeaderStyle HorizontalAlign="Center" />

                                                        </asp:BoundField>
                                                        <%-- <asp:BoundField DataField="CST" HeaderText="CST">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                                        --%>
                                                        <asp:BoundField HeaderText="Amount">
                                                            <HeaderStyle HorizontalAlign="Center" />

                                                        </asp:BoundField>
                                                        <%--<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="DeliveryDate" HeaderText="DC Date">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                            <asp:BoundField DataField="DC_NO" HeaderText="DC_NO">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>--%>

                                                        <asp:BoundField DataField="UnitPrice" HeaderText="Unit Price">
                                                            <HeaderStyle HorizontalAlign="Center" />

                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SPPrice" HeaderText="Special Price" />
                                                        <asp:BoundField HeaderText="GSTTaxValue" />
                                                        <asp:BoundField DataField="DetId" HeaderText="DetId">
                                                            <HeaderStyle HorizontalAlign="Center" />

                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnDelete" CausesValidation="false" ForeColor="Blue" runat="server"
                                                                     __designer:wfdid="w5" OnClick="lbtnDelete_Click">Delete</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
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
                                            <td style="text-align: left; width: 439px;"></td>
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
                                            <td style="text-align: left; width: 439px;">
                                                <asp:TextBox ID="txtInvoiceDate" runat="server" type="datepic">
                                                </asp:TextBox>&nbsp;
                            <asp:Label ID="Label15" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                                                <asp:RequiredFieldValidator ID="rfvInvoiceDate" runat="server" ControlToValidate="txtInvoiceDate"
                                                    ErrorMessage="Please Enter the Delivery Challan No">*</asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="DateCustomValidate"
                                                    ControlToValidate="txtInvoiceDate" ErrorMessage="Please Enter the Invoice Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                                    SetFocusOnError="True">*</asp:CustomValidator>
                                                <%--   <cc1:CalendarExtender Format="dd/MM/yyyy" ID="CeInvoiceDate" runat="server" Enabled="True"
                                PopupButtonID="imgInvoiceDate" TargetControlID="txtInvoiceDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MeeInvoiceDate" runat="server" DisplayMoney="Left" Enabled="True"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtInvoiceDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                                            </td>
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
                                            <td colspan="4" style="text-align: left">
                                                Search Model No :&nbsp;
                                                 <asp:TextBox ID="txtSearchModel1" Visible="true" runat="server"></asp:TextBox>
                                                <asp:Button ID="btnSearch_ItemModel_No" runat="server" Visible="true" BorderStyle="None"
                                                    CausesValidation="False" CssClass="gobutton" EnableTheming="False" OnClick="btnSearch_ItemModel_No_Click"
                                                    Text="Go" />
                                                <asp:SqlDataSource
                                                    ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                                    SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                                    <SelectParameters>
                                                        <asp:Parameter Type="Int64" DefaultValue="0" Name="BranbId"></asp:Parameter>
                                                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0"
                                                             Name="SearchValue" ControlID="txtSearchModel1"></asp:ControlParameter>
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: left"></td>
                                        </tr>

                                        <tr>
                                            <td style="text-align: right;">
                                                <asp:Label ID="Label4" runat="server" Text="Model No : "></asp:Label></td>
                                            <td style="text-align: left;">
                                                <asp:DropDownList ID="ddlModelNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModelNo_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:Label ID="lblTtlAmt_Previous" runat="server" Visible="False"></asp:Label>
                                                <asp:Label ID="lblTtlGSTAmt_Previous" runat="server" Visible="false"></asp:Label>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="Label7" runat="server" Text="Item Name : " Width="76px"></asp:Label></td>
                                            <td style="text-align: left; width: 439px;">
                                                <asp:TextBox ID="txtItemname" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblHSN" Text="HSN Code : " runat="server"></asp:Label></td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtHSN" runat="server"></asp:TextBox></td>
                                            <td style="text-align: right">
                                                <asp:Label runat="server" ID="lblGST" Text="GST rate : "></asp:Label></td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtGST" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label27" runat="server" Text="UOM : "></asp:Label></td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtItemUOM" runat="server" ReadOnly="True"></asp:TextBox></td>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label59" runat="server" Text="Color : "></asp:Label></td>
                                            <td style="text-align: left; width: 439px;">
                                                <asp:DropDownList ID="ddlColor" runat="server" AutoPostBack="True">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlColor" ErrorMessage="Please Select The item Color" InitialValue="0" ValidationGroup="id">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label23" runat="server" Text="Item Specification : "></asp:Label></td>
                                            <td colspan="3" style="text-align: left">
                                                <asp:TextBox ID="txtItemSpec" runat="server" CssClass="multilinetext" EnableTheming="False"
                                                    ReadOnly="True" TextMode="MultiLine" Width="90%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblRate" runat="server" Text="Rate : "></asp:Label></td>
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
                                                <asp:Label ID="lblQuantity" runat="server" Text="Quantity : "></asp:Label></td>
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
                                                <asp:Label ID="Label17" runat="server" Text="Discount : "></asp:Label></td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtDiscount1" runat="server">
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtDiscount1" ErrorMessage="Please Enter the Discount" ValidationGroup="id">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblAmount" runat="server" Text="Amount : "></asp:Label></td>
                                            <td style="text-align: left; width: 439px;">
                                                <asp:TextBox ID="txtAmount" runat="server" ReadOnly="True">
                                                </asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label28" runat="server" Visible="false" Text="Delivery Date : "></asp:Label></td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtDeliveryDate" runat="server" type="datepic" Visible="false"></asp:TextBox></td>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label30" runat="server" Text="Special Price : "></asp:Label></td>
                                            <td style="text-align: left; width: 439px;">
                                                <asp:TextBox ID="txtSpPrice" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label32" runat="server" Text="Remarks : "></asp:Label></td>
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
                                            <td colspan="4" style="height: 18px; text-align: center;">
                                                <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    CssClass="loginbutton" EnableTheming="False" Text="Add" OnClick="btnAdd_Click"
                                                    ValidationGroup="id" />
                                                <asp:Button ID="btnItemRefresh" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" CssClass="loginbutton" EnableTheming="False" 
                                                        OnClick="btnItemRefresh_Click"
                                                        Text="Refresh" CausesValidation="False" /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: center">
                                                <asp:GridView ID="gvItmDetails" runat="server" AutoGenerateColumns="False" 
                                                    OnRowDeleting="gvItmDetails_RowDeleting"
                                                    OnRowDataBound="gvItmDetails_RowDataBound"
                                                     OnRowEditing="gvItmDetails_RowEditing" Width="100%">
                                                    <Columns>
                                                        <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                                        <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                                        <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                                            <HeaderStyle HorizontalAlign="Center" />

                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ModelNo" HeaderText="Model No">
                                                            <HeaderStyle HorizontalAlign="Center" />

                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="HSN_CODE" HeaderText="HSN Code">
                                                            <HeaderStyle HorizontalAlign="Center" />

                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="ItemName" HeaderText="Item Name">
                                                            <HeaderStyle HorizontalAlign="Center" />

                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="UOM" HeaderText="UOM">
                                                            <HeaderStyle HorizontalAlign="Center" />

                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        </asp:BoundField>

                                                        <%--7--%>
                                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                                            <HeaderStyle HorizontalAlign="Center" />

                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="Rate" HeaderText="Rate">
                                                            <HeaderStyle HorizontalAlign="Center" />

                                                        </asp:BoundField>
                                                        <%--<asp:TemplateField HeaderText="Price" ControlStyle-Width="70px">
                                                                          <ItemTemplate>
                                                                            <asp:TextBox ID="txtPrice" runat="server" AutoPostBack="true" Text='<%# Bind("Rate") %>'></asp:TextBox>
                                                                          </ItemTemplate>
                                                                    </asp:TemplateField>--%>
                                                        <asp:BoundField DataField="GST_RATE" HeaderText="GST Rate">
                                                            <HeaderStyle HorizontalAlign="Center" />

                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="UnitPrice" HeaderText="UnitPrice">
                                                            <HeaderStyle HorizontalAlign="Center" />

                                                        </asp:BoundField>

                                                        <%--11--%>
                                                        <asp:BoundField HeaderText="Spl Amount"  >
                                                            <HeaderStyle HorizontalAlign="Center" />

                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="GST Value" />
                                                        <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                                                        <asp:BoundField DataField="ColorId" HeaderText="ColorId">
                                                            <HeaderStyle HorizontalAlign="Center" />

                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                            <HeaderStyle HorizontalAlign="Center" />

                                                        </asp:BoundField>
                                                        <%--<asp:BoundField DataField="PI_DET_ID" HeaderText="DetId">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>--%>
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
                                            <td style="height: 19px; text-align: left; width: 439px;"></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;"></td>
                                            <td style="text-align: left;"></td>
                                            <td style="text-align: right;"></td>
                                            <td style="text-align: left; width: 439px;"></td>

                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">
                                                <asp:Label ID="Label24" runat="server" Text="Total"></asp:Label></td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtTotalAmt" runat="server" ReadOnly="True"></asp:TextBox>
                                                <asp:Label ID="lbl_Current_Total" runat="server" Visible="false"></asp:Label>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtMiscelleneous"
                                                    ValidChars=".0123456789">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblGSTType" runat="server" Text="GST Type"></asp:Label></td>
                                            <td style="text-align: left; width: 439px;">
                                                <asp:DropDownList runat="server" ID="ddlGSTType">
                                                    <asp:ListItem Value="0">--</asp:ListItem>
                                                    <asp:ListItem Value="1">IGST</asp:ListItem>
                                                    <asp:ListItem Value="2">CGST/SGST</asp:ListItem>
                                                </asp:DropDownList>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblGSTtotal" runat="server" Text="GST Total Amount"></asp:Label></td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtGSTTotal" runat="server" ReadOnly="true"></asp:TextBox>
                                                <asp:Label ID="lbl_Current_Gst" runat="server" Visible="false"></asp:Label>
                                                
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
                                            <td colspan ="3" style="text-align: left">
                                                <asp:TextBox ID="txtGrossAmount" runat="server" Width="349px">
                                                </asp:TextBox>
                                                <asp:HiddenField ID="txtGrossTotalAmtHidden" runat="server" />
                                                <asp:Label ID="lblTemp_Gross_WithoutDisc" runat="server" Visible="false"></asp:Label>
                                            </td>
                                           
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblRemarks" runat="server" Text="Remarks"></asp:Label></td>
                                            <td colspan ="3"  style="text-align: left">
                                                <asp:TextBox ID="txtRemarks1" runat="server" Height="53px" TextMode="MultiLine" Width="673px"
                                                    CssClass="textbox" EnableTheming="False">_</asp:TextBox></td>
                                            
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label52" runat="server" Text="Advance Amt, If any"></asp:Label></td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtAdvanceAmt" runat="server"></asp:TextBox></td>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label21" runat="server" Text="Fright Charges"></asp:Label></td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtFright" runat="server"></asp:TextBox></td>
                                           <%-- <td style="text-align: right">
                                                <asp:Label ID="Label31" runat="server" Text="Payment Terms"></asp:Label></td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtPaymentTerms" runat="server"></asp:TextBox><asp:Label ID="Label42" runat="server" EnableTheming="False" ForeColor="Red" Text="*"></asp:Label>
                                                <asp:RequiredFieldValidator ID="rfvPayTerms" runat="server" ControlToValidate="txtPaymentTerms" ErrorMessage="Please Enter the Payment Terms">*</asp:RequiredFieldValidator>
                                            </td>--%>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label37" runat="server" Text="packing charges" Width="98px"></asp:Label></td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtPackingCharges" runat="server"></asp:TextBox><asp:Label ID="Label41" runat="server" EnableTheming="False" ForeColor="Red" Text="*"></asp:Label>
                                                <asp:RequiredFieldValidator ID="rfvPackingChrgs" runat="server" ControlToValidate="txtPackingCharges" ErrorMessage="Please Enter the Packing Charges">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label33" runat="server" Text="Transportation charges" Width="98px"></asp:Label></td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtTransportcharges" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                             <td style="text-align: right">
                                                <asp:Label ID="Label31" runat="server" Text="Payment Terms"></asp:Label></td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtPaymentTerms" runat="server"></asp:TextBox><asp:Label ID="Label42" runat="server" EnableTheming="False" ForeColor="Red" Text="*"></asp:Label>
                                                <asp:RequiredFieldValidator ID="rfvPayTerms" runat="server" ControlToValidate="txtPaymentTerms" ErrorMessage="Please Enter the Payment Terms">*</asp:RequiredFieldValidator>
                                            </td>
                                            <%--<td style="text-align: right">
                                                <asp:Label ID="Label38" runat="server" Text="Fright Charges"></asp:Label></td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtFright" runat="server"></asp:TextBox></td>--%>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label39" runat="server" Text="Other Details"></asp:Label></td>
                                            <td colspan="3" style="text-align: left">
                                                <asp:TextBox ID="txtTerms" runat="server" Height="53px" TextMode="MultiLine" Width="673px"
                                                    CssClass="textbox" EnableTheming="False"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                             <td style="text-align: right"><asp:Label ID="Label14" runat="server" Text="Terms & Conditions"></asp:Label></td>
                                            <td colspan ="3"><asp:CheckBoxList ID="chkTerms" runat="server" RepeatDirection="Vertical" >
                            </asp:CheckBoxList>
                                                <asp:Label ID="lblchkllist" runat="server" Text="Please Check atleast 3 Terms"
                                                    Visible="False" Font-Bold="True" ForeColor="Red" EnableTheming="False"></asp:Label>
                                            </td>
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
                                            <%--<td style="text-align: right">
                                    <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By" Visible="true"></asp:Label></td>
                                <td style="text-align: left; width: 439px;">
                                    <asp:DropDownList ID="ddlApprovedBy" runat="server"  Visible="true">
                                    </asp:DropDownList></td>--%>
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
                                                        <%--<td>
                                                <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click"
                                                    CausesValidation="False" Visible="False" /></td>--%>
                                                        <td>
                                                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click"
                                                                CausesValidation="False" /></td>

                                                        <td>
                                                            <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" CausesValidation="False" /></td>

                                                    </tr>
                                                </table>
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
                        <asp:SqlDataSource ID="sdsSalesInvoiceDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                            SelectCommand="SP_INVENTORY_PROFORMAINVOICE_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
                                <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
                                <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
                                <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
                                <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="EMPID" ControlID="lblEmpIdHidden"></asp:ControlParameter>
                                <%--<asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>--%>
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <asp:Label ID="Label1" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                            Visible="False"></asp:Label>
                        <asp:Label ID="Label2" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                            Visible="False"></asp:Label><asp:Label ID="Label5" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="Label8" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="Label13" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="Label22" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="Label26" runat="server" Visible="False"></asp:Label>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>

