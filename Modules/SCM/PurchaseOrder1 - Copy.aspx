<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/PurchaseMP1.master" AutoEventWireup="true" CodeFile="PurchaseOrder1 - Copy.aspx.cs" Inherits="Modules_SCM_PurchaseOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
  <%-- function TotalPrice() {
            var UnitPrice, Qty;
            var GV = document.getElementById('<%=gvProductDetails$ctl02$txtQuantity.ClientID%>').value;
            gdRows$ctl02$txtID
            Qty = document.getElementByName('<%=ctl00$ctl00$ContentPlaceHolderBody$ContentPlaceHolderBody$gvProductDetails$ctl0n$txtQuantity.ClientID%>').value;
        UnitPrice = document.getElementById('<%=txtUnitPrice.ClientID%>').value;
        if (Qty == "" || UnitPrice == "") {
            document.getElementById('<%=txtTotalPrice.ClientID %>').value = "0";
        }
        else if (Qty > 0) {
            if (UnitPrice > 0) {
                document.getElementById('<%=txtTotalPrice.ClientID %>').value = parseFloat(Qty) * parseFloat(UnitPrice);
            }
            else if (UnitPrice == 0) {
                document.getElementById('<%=txtTotalPrice.ClientID %>').value = "0";
            }
            var getText = ((TextBox)gvProductDetails.Rows[e.RowIndex].FindControl("txtBoxID")).Text;
    }
}
    --%>
       <%-- function DueAmt() {
            var install = document.getElementsByName("ctl00$ctl00$ContentPlaceHolderBody$ContentPlaceHolderBody$gvProductDetails$ctl02$txtQuantity")[0].value;
            //var GV = document.getElementByName("<%=gvProductDetails.ClientID %>");
            alert(install);
        }--%>
    

        $(document).ready(function () {



            $(document).on("keyup", "[id$='txtPackingCharges'],[id$='txtFobCharges'],[id$='txtCifCharges'],[id$='txtTaxes'],[id$='txtInsurance'],[id$='txtFreight']", function () {

                var Total = parseFloat($("[id$='lblTtlAmt']").html(), 10) || 0;
                //var GSTTotal = parseFloat($("[id$='lblTtlGSTAmt']").html(), 10) || 0;
                var PackingCharges = parseFloat($("[id$='txtPackingCharges']").val(), 10) || 0;
                var Fob = parseFloat($("[id$='txtFobCharges']").val(), 10) || 0;
                var Cif = parseFloat($("[id$='txtCifCharges']").val(), 10) || 0;

                var Tax = parseFloat($("[id$='txtTaxes']").val(), 10) || 0;
                //var Tax = (Total * Per) / 100;

                var Ins = parseFloat($("[id$='txtInsurance']").val(), 10) || 0;
                var Insurance = (Total * Ins) / 100;

                var frei = parseFloat($("[id$='txtFreight']").val(), 10) || 0;
                var freight = (Total * frei) / 100;

                var tot = (PackingCharges + Fob + Cif + Total + Tax + Insurance + freight) || 0;

                $("[id$='hfNetAmount']").val(tot);
                $("[id$='lblNetAmount']").text(tot);
               // $('.lblNetAmount').text((PackingCharges + Fob + Cif + Total + Tax + Insurance) || '')

               
               // $('#lblNetAmount').val(sum);
            });
        });
     
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
        <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
    <table border="0" cellpadding="0" cellspacing="0" id="tblPurchaseOrder" runat="server"
        visible="true" width="100%">
        <tr>
            <td colspan="4" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" id="Table1" runat="server" visible="true"
                    width="100%">
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">General Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="lblCPID" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td style="height: 21px; text-align: left"></td>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left; width: 324px;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:RadioButton ID="rbnIndent" runat="server"  Text="By Indent" />
                        </td>

                        <td style="text-align: left">
                            <asp:RadioButton ID="rbnProformaIV" Text="By Proforma Invoice" runat="server" />
                        </td>
                        <td style="text-align: left; width: 324px;"></td>
                        <td style="text-align: left; width: 324px;"></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">Purchase Order No
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>
                        </td>
                        <td style="text-align: right;">Purchase Order Date
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtPODate" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">&nbsp;</td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left; width: 324px;"></td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td colspan="4" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" id="Table2" runat="server" visible="true"
                    width="100%">
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">Supplier Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left; width: 324px;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Supplier Name :
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlSupplierName" runat="server" OnSelectedIndexChanged="ddlSupplierName_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                        </td>

                        <td style="text-align: right">Supplier Unit Name :
                        </td>
                        <td style="text-align: left; width: 324px;">
                            <asp:DropDownList ID="ddlSupUnitName" runat="server" AutoPostBack="True"></asp:DropDownList>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">Supplier Address :
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtSupplierAddress" TextMode="MultiLine" runat="server" Height="50px" Width="250px"></asp:TextBox>
                        </td>
                         <td style="text-align: right">
                             <asp:Label ID="lblInvoiceNo" runat="server" Text="Proforma Inv No :"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 324px;">
                            <asp:DropDownList ID="ddlInvoiceNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInvoiceNo_SelectedIndexChanged"></asp:DropDownList>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left; width: 324px;"></td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td colspan="4" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" id="Table3" runat="server" visible="true"
                    width="100%">
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">Billing & Shipping Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left; width: 324px;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Company Name :
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlCompanyName" runat="server"></asp:DropDownList>
                        </td>

                        <td style="text-align: right"></td>
                        <td style="text-align: left; width: 324px;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Billing Unit :
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlBillingUnit" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBillingUnit_SelectedIndexChanged"></asp:DropDownList>

                        </td>

                        <td style="text-align: right">Shipping Unit :
                        </td>
                        <td style="text-align: left; width: 324px;">
                            <asp:DropDownList ID="ddlShippingUnit" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlShippingUnit_SelectedIndexChanged"></asp:DropDownList>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">Billing Address
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtBillingAdd" TextMode="MultiLine" runat="server" Height="50px" Width="250px"></asp:TextBox>
                        </td>
                        <td style="text-align: right;">Shipping Address
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtShippingAdd" TextMode="MultiLine" runat="server" Height="50px" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left; width: 324px;"></td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td colspan="4" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" id="Table4" runat="server" visible="true"
                    width="100%">
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">Reference Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left; width: 324px;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Reference No:
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtRefNo" runat="server"></asp:TextBox>
                            <asp:Label ID="Label29" runat="server" EnableTheming="False" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfv2" runat="server" ErrorMessage="Please Enter Reference Number" ControlToValidate="txtRefNo" ValidationGroup="Sa">*</asp:RequiredFieldValidator>
                        </td>

                        <td style="text-align: right">Customer Code :
                        </td>
                        <td style="text-align: left; width: 324px;">
                            <asp:TextBox ID="txtCustCode" runat="server"></asp:TextBox>
                            <asp:Label ID="Label1" runat="server" EnableTheming="False" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Customer Code" ControlToValidate="txtCustCode" ValidationGroup="Sa">*</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Delivery Date :
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtItemDate" type="datepic" runat="server"></asp:TextBox>
                            <asp:Label ID="Label2" runat="server" EnableTheming="False" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Item Date" ControlToValidate="txtItemDate" ValidationGroup="Sa">*</asp:RequiredFieldValidator>
                            <%--<cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtItemDate" runat="server"></cc1:CalendarExtender>--%>

                        </td>

                        <td style="text-align: right">Remarks :
                        </td>
                        <td style="text-align: left; width: 324px;">
                            <asp:TextBox ID="txtRemarks" runat="server">-</asp:TextBox>

                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left; width: 324px;"></td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td colspan="4" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" id="Table5" runat="server" visible="true"
                    width="100%">
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">Item Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left; width: 324px;"></td>
                    </tr>
                    <tr>
                        <td colspan="4">

                                    <asp:GridView ID="gvProductDetails" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDataBound="gvProductDetails_RowDataBound" OnRowDeleting="gvProductDetails_RowDeleting">
                                <Columns>
                                            <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ModelNo" HeaderText="Item Model No">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>

                                    <asp:TemplateField HeaderText="Description" ControlStyle-Width="150px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDesc" runat="server" Text='<%#Bind("Desc")%>' Width="180"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Color" HeaderText="Color">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="GST" ControlStyle-Width="80px" >
                                        <ItemTemplate >
                                            <asp:TextBox ID="txtDetGST" runat="server" Text='<%# Bind("GST") %>' AutoPostBack ="true" OnTextChanged ="txtDetGST_TextChanged"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity" ControlStyle-Width="80px">
                                        <ItemTemplate>
                                                    <asp:TextBox ID="txtQuantity" runat="server" onkeyup="DueAmt()" Text='<%# Bind("Quantity") %>' AutoPostBack="true" OnTextChanged="txtQuantity_TextChanged"></asp:TextBox>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:BoundField DataField="Currency" HeaderText="Currency Type">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                               
                                    <asp:TemplateField HeaderText="Price" ControlStyle-Width="70px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtMRP" runat="server" Text='<%# Bind("MRP") %>' AutoPostBack="true" OnTextChanged="txtMRP_TextChanged"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate" ControlStyle-Width="70px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRate" Text='<%# Bind("Rate") %>' runat="server"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Discount%" ControlStyle-Width="70px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDiscount" runat="server" Text='<%# Bind("Discount") %>' AutoPostBack="true" OnTextChanged="txtDiscount_TextChanged"></asp:TextBox>
                                            <asp:Label ID="Label9" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" ControlStyle-Width="70px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" Text='<%# Bind("Amount") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText ="GST Amount" ControlStyle-Width="70px">
                                        <ItemTemplate >
                                            <asp:Label ID="lblGSTAmount" runat="server" ></asp:Label>

                                            <%--<asp:Label ID="lblGSTAmount" runat="server"  ></asp:Label>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="IndId" HeaderText="Ind Id" />
                                    <asp:BoundField DataField="IndDetId" HeaderText="Ind Det Id" />
                                    <asp:BoundField DataField="ColorId" HeaderText="Color Id" />
                                    <asp:BoundField DataField="Customer" HeaderText="Customer" />


                                </Columns>
                            </asp:GridView>

                        </td>
                    </tr>
                            <tr style="background-color:#98b7e9"><td style="width:42%"></td>
                                <td style="width:25%"></td>
                                <td style="width:10%;color:black;font-weight:bold">
                                    Total Amount :
                                </td>
                                <td style="width:20%;color:red;font-weight:bold">
                                    <asp:Label ID="lblTtlAmt" runat="server">0</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTtlGSTAmt" runat ="server"  >0</asp:Label>
                                </td>
                            </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left; width: 324px;"></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>

        <tr>
                                <td style="text-align: right">Packing Charges :
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtPackingCharges" runat="server"></asp:TextBox>&nbsp;
                                    <asp:Label ID="Label4" runat="server" EnableTheming="False" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Enter Packing Charges" ControlToValidate="txtPackingCharges" ValidationGroup="Sa">*</asp:RequiredFieldValidator>
                                </td>

                                <td style="text-align: right">Insurance :
                                </td>
                                <td style="text-align: left; width: 324px;">
                                    <asp:TextBox ID="txtInsurance" runat="server">0</asp:TextBox>%
                                    <asp:Label ID="Label5" runat="server" EnableTheming="False" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter Insurance" ControlToValidate="txtInsurance" ValidationGroup="Sa">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
         <tr>
                                <td style="text-align: right">FOB Charges:
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtFobCharges" runat="server">0</asp:TextBox>
                                <asp:Label ID="Label6" runat="server" EnableTheming="False" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Enter FOB Charges" ControlToValidate="txtFobCharges" ValidationGroup="Sa">*</asp:RequiredFieldValidator>
                                </td>

                                <td style="text-align: right">CIF Charges :
                                </td>
                                <td style="text-align: left; width: 324px;">
                                    <asp:TextBox ID="txtCifCharges" runat="server">0</asp:TextBox>
                                    <asp:Label ID="Label7" runat="server" EnableTheming="False" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Enter CIF Charges" ControlToValidate="txtCifCharges" ValidationGroup="Sa">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>

                <tr>
                                <td style="text-align: right">Taxes-IGST :
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtTaxes" runat="server">0</asp:TextBox>
                                    <asp:Label ID="Label3" runat="server" EnableTheming="False" ForeColor="Red" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter Taxes-CST field" ControlToValidate="txtTaxes" ValidationGroup="Sa">*</asp:RequiredFieldValidator>
                                </td>

                                <td style="text-align: right">Net Amount :
                                </td>
                                <td style="text-align: left; width: 324px;">
                                    <asp:Label ID="lblNetAmount" Text="0"  runat="server"></asp:Label>
                                    <asp:HiddenField ID="hfNetAmount" runat="server" />                                    
                                </td>
                            </tr>

                 <tr>
                    <td style="text-align: right">Freight :
                    </td>
                    <td colspan="3" style="text-align: left">
                        <asp:TextBox ID="txtFreight" Text="0" runat="server"></asp:TextBox>%
                   <asp:Label ID="Label8" runat="server" EnableTheming="False" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please Enter Freight" ControlToValidate="txtFreight" ValidationGroup="Sa">*</asp:RequiredFieldValidator>
                         </td>
                </tr>

        <tr>
                    <td style="text-align: right">Terms  & Conditions :
            </td>
            <td colspan="3" style="text-align: left">
                <asp:TextBox ID="txtTerms" TextMode="MultiLine" runat="server" Height="100px" Width="700px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        </table>
      </ContentTemplate>
    </asp:UpdatePanel>
    <table style="width:100%">
        <tr>
            <td></td>
            <td style="text-align: right">
                <asp:Button ID="btnPO1" runat="server" Text="Download PO Template" OnClick="btnPO1_Click" />
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" ValidationGroup="Sa" />
            </td>
            <td><asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" /></td>
            <td></td>
        </tr>
            <tr>
                <td>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Sa"></asp:ValidationSummary>
                </td>
            </tr>
    </table>            
       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
     
</asp:Content>


 
