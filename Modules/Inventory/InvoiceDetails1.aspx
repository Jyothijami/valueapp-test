<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/FinanceMP1.master" AutoEventWireup="true" CodeFile="InvoiceDetails1.aspx.cs" Inherits="Modules_Inventory_InvoiceDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    <%--<asp:Label ID="Label31" runat="server" Text="PO Terms & Conditions :"></asp:Label>--%>
    <script>
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



            //new calculation


        <%--    function amtcalc() {

            var req_qty, rate, disc, spPrice, unitPrice;
            req_qty = document.getElementById('<%=txtQuantity.ClientID %>').value;
            rate = document.getElementById('<%=txtRate.ClientID %>').value;
            disc = document.getElementById('<%=txtDiscount1.ClientID %>').value;
            if (req_qty == "" || req_qty == "0") {
                document.getElementById('<%=txtSpPrice.ClientID %>').value = "0";
              }
              else if (rate == "" || rate == "0") {
                  document.getElementById('<%=txtSpPrice.ClientID %>').value = "0";
            }
            else if (rate > 0 && req_qty > 0) {
                document.getElementById('<%=txtAmount.ClientID %>').value = (rate * req_qty);
                document.getElementById('<%=txtSpPrice.ClientID %>').value = (rate * req_qty);
                document.getElementById('<%=txtUnitprice.ClientID %>').value = (rate * 1);
            }
    if (disc != "" && disc != "0") {
        document.getElementById('<%=txtSpPrice.ClientID %>').value = parseFloat((rate * req_qty)) - parseFloat((((rate * req_qty) * disc) / 100));
        document.getElementById('<%=txtUnitprice.ClientID %>').value = parseFloat((rate * 1)) - parseFloat((((rate * 1) * disc) / 100));
    }
}

//discount calculation

        function amtcalcDisc() {

    var req_qty, rate, spprice;
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
                 document.getElementById('<%=txtDiscount1.ClientID %>').value = (((rate * req_qty) - (spprice)) * 100) / (rate * req_qty);
            }
    amtcalc();

}

        function amtcalcDisc1() {

    var rate, unitPrice, req_qty;
    req_qty = document.getElementById('<%=txtQuantity.ClientID %>').value;
    rate = document.getElementById('<%=txtRate.ClientID %>').value;
            unitPrice = document.getElementById('<%=txtUnitprice.ClientID %>').value;

            if (req_qty == "" || req_qty == "0") {
                document.getElementById('<%=txtSpPrice.ClientID %>').value = "0";
             }
             else if (rate == "" || rate == "0") {
                 document.getElementById('<%=txtSpPrice.ClientID %>').value = "0";
             }
             else if (rate > 0 && req_qty > 0) {
                 document.getElementById('<%=txtDiscount1.ClientID %>').value = (((rate) - (unitPrice * 1)) * 100) / (rate);
             }
     amtcalc();
 }--%>




    </script>
    <style type="text/css">
        .auto-style1 {
            height: 25px;
        }

        .auto-style2 {
            width: 439px;
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
                <tr>
                    <td style="text-align: left">Invoice Details</td>
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
                                    <asp:TextBox ID="txtSalesOrderDate" runat="server" ReadOnly="True" type="date">
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
                                    <asp:TextBox ID="txtChallanDate" runat="server" ReadOnly="True" type="date">
                                    </asp:TextBox><%--<asp:Image ID="imgChallanDate" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False" /><cc1:CalendarExtender
                                Format="dd/MM/yyyy" ID="CeChallanDate" runat="server" Enabled="False" PopupButtonID="imgChallanDate"
                                TargetControlID="txtChallanDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MeeChallanDate" runat="server" DisplayMoney="Left" Enabled="True"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtChallanDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right" class="auto-style1">
                                    <asp:Label ID="Label33" runat="server" Text="InvoiceNo" Width="101px" Visible="False"></asp:Label></td>
                                <td style="text-align: left" class="auto-style1">
                                    <asp:TextBox ID="txtInno" runat="server" CausesValidation="True" Visible="False">0</asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtInno" ErrorMessage="Please Enter Invoice No." ValidationGroup="main">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="text-align: right" class="auto-style1"></td>
                                <td style="text-align: left;" class="auto-style2"></td>
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
                                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ModelNo" HeaderText="Model No">
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
                                            <asp:BoundField HeaderText="UnitPrice">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                            <asp:BoundField DataField="Price" HeaderText="Amount">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
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
                                    <asp:Label ID="lblTerms" ForeColor="Black" Font-Bold="true" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="profilehead" colspan="4">Invoiced Items</td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center">
                                    <asp:GridView ID="gvInvoicedItems" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvItmDetails_RowDeleting"
                                        OnRowDataBound="gvInvoicedItems_RowDataBound" OnRowEditing="gvItmDetails_RowEditing" Width="100%">
                                        <Columns>
                                            <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                            <asp:CommandField ShowDeleteButton="True"></asp:CommandField>

                                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ModelNo" HeaderText="Model No">
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
                                            <asp:BoundField DataField="Vat" HeaderText="Vat">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                            <asp:BoundField DataField="CST" HeaderText="CST">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                            <asp:BoundField DataField="Excise" HeaderText="Excise">
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Amount">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                            <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="DeliveryDate" HeaderText="DC Date">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                            <asp:BoundField DataField="DC_NO" HeaderText="DC_NO">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                            <asp:BoundField DataField="DetId" HeaderText="DetId">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnDelete" CausesValidation="false" ForeColor="Blue" runat="server" __designer:wfdid="w5" OnClick="lbtnDelete_Click">Delete</asp:LinkButton>
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
                                <td class="profilehead" colspan="4" style="text-align: left">Delivery Challan Items</td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <asp:GridView ID="gvDeliveryChallanItems" runat="server" AutoGenerateColumns="False"
                                        Width="100%" OnRowDataBound="gvDeliveryChallanItems_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="DC No" HeaderText="DC No">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                            <asp:BoundField DataField="ModelNo" HeaderText="Model No">
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
                                            <asp:BoundField DataField="Rate" HeaderText="Rate">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="UnitPrice">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Amount">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                            <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="DeliveryDate" HeaderText="Delivery Date">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>

                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkhdr" runat="server" AutoPostBack="True" OnClick="selectAll(this)" />
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />

                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkItemSelect" runat="server" __designer:wfdid="w31"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="SPPrice" HeaderText="SPPrice">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                            <asp:BoundField DataField="POQty" HeaderText="POQty">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                            <asp:BoundField DataField="ColorId" HeaderText="ColorId">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                            <asp:BoundField DataField="DetId" HeaderText="DetId">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <span style="color: #ff0033">No Data to Dispaly</span>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                    <asp:Button ID="btnGo" runat="server" CausesValidation="False" OnClick="btnGo_Click"
                                        Text="Go" /></td>
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
                                    <asp:TextBox ID="txtInvoiceNo" runat="server" >
                                    </asp:TextBox><asp:Label ID="Label12" runat="server" EnableTheming="False" Font-Bold="False"
                                        Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                            ID="rfvInvoiceNo" runat="server" ControlToValidate="txtInvoiceNo" ErrorMessage="Please Enter the Delivery Challan No">*</asp:RequiredFieldValidator></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label6" runat="server" Text="Invoice Date"></asp:Label></td>
                                <td style="text-align: left; width: 439px;">
                                    <asp:TextBox ID="txtInvoiceDate" runat="server" type="date">
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
                                <td style="height: 19px; text-align: right">
                                    <asp:Label ID="Label5" runat="server" Text="Invoice Type"></asp:Label></td>
                                <td style="height: 19px; text-align: left">
                                    <asp:DropDownList ID="ddlInvoiceType" runat="server">
                                        <asp:ListItem Value="0">--</asp:ListItem>
                                        <asp:ListItem>Tax Invoice</asp:ListItem>
                                        <%--<asp:ListItem>Pro-Forma Tax Invoice</asp:ListItem>--%>
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
                                <td style="text-align: left;">&nbsp;<asp:DropDownList ID="ddlModelNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModelNo_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:Label ID="lblTtlAmt" runat="server" Visible="False"></asp:Label>
                                </td>
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
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlColor" ErrorMessage="Please Select The item Color" InitialValue="0" ValidationGroup="id">*</asp:RequiredFieldValidator>
                                </td>
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
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtDiscount1" ErrorMessage="Please Enter the Discount" ValidationGroup="id">*</asp:RequiredFieldValidator>
                                </td>
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
                                    <asp:TextBox ID="txtDeliveryDate" runat="server" type="date"></asp:TextBox></td>
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
                                <td colspan="4" style="height: 18px; text-align: center;">
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
                                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                            <asp:BoundField DataField="ModelNo" HeaderText="Model No">
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
                                            <asp:BoundField DataField="Vat" HeaderText="Vat">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                            <asp:BoundField DataField="CST" HeaderText="CST">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                            <asp:BoundField DataField="Excise" HeaderText="Excise">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Amount">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                            <asp:BoundField DataField="SPPrice" HeaderText="UnitPrice">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                            <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="DeliveryDate" HeaderText="Delivery Date">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                            <asp:BoundField DataField="DCId" HeaderText="DCId">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                            <asp:BoundField DataField="ColorId" HeaderText="ColorId">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                            <asp:BoundField DataField="DetId" HeaderText="DetId">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
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
                                <td style="text-align: left; width: 439px;">
                                    <asp:RadioButton ID="rbVAT" runat="server" Checked="True" OnCheckedChanged="rbVAT_CheckedChanged" GroupName="vatcst" Text="VAT" /><asp:RadioButton
                                        ID="rbCST" runat="server" OnCheckedChanged="rbCST_CheckedChanged" GroupName="vatcst" Text="C.S. Tax" />
                                    <asp:RadioButton ID="rbBranchTransfer" runat="server" OnCheckedChanged="rbBranchTransfer_CheckedChanged" GroupName="vatcst" Text="Branch Transfer" />
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
                                        runat="server" Text="C.S. Tax"></asp:Label></td>
                                <td style="text-align: left; width: 439px;">
                                    <asp:TextBox ID="txtVAT" runat="server" autocomplete="off">
                                    </asp:TextBox><asp:TextBox ID="txtCST" runat="server" autocomplete="off">
                                    </asp:TextBox>
                                    <asp:TextBox ID="txtBranchTransfer" runat="server" autocomplete="off">
                                    </asp:TextBox>
                                    <asp:Label ID="Label25" runat="server" EnableTheming="False" Font-Bold="False"
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
                                                    CausesValidation="False" OnClick="btnStatement_Click" Visible="False" />
                                                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"
                                                    CausesValidation="False" />
                                            </td>
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
            <asp:SqlDataSource ID="sdsSalesInvoiceDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                SelectCommand="SP_INVENTORY_SALESINVOICE_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
                    <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="EMPID" ControlID="lblEmpIdHidden"></asp:ControlParameter>
                    <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                Visible="False"></asp:Label>
            <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                Visible="False"></asp:Label><asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


 
