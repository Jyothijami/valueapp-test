<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/PurchaseMP1.master" AutoEventWireup="true" CodeFile="PurchaseInvoiceNew1.aspx.cs" Inherits="Modules_SCM_PurchaseInvoiceNew" %>

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
    <script type="text/javascript" language="javascript">
        function amtcalc() {

            var req_qty, rate, vat, cst, excise;
            req_qty = document.getElementById('<%=txtQuantity.ClientID %>').value;
            rate = document.getElementById('<%=txtRate.ClientID %>').value;
            <%--cst = document.getElementById('<%=txtCST.ClientID %>').value;
            vat = document.getElementById('<%=txtVAT.ClientID %>').value;--%>
            //excise=document.getElementById('<%=txtExcise.ClientID %>').value;
            gst = document.getElementById('<%=txtGST.ClientID%>').value;
            if (gst == "" || gst == "0" || isNaN(gst)) { gst = "0"; }
            //if (vat == "" || vat == "0" || isNaN(vat)) { vat = "0"; }
            //if (excise == "" || excise == "0" || isNaN(excise)) { excise = "0"; }

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
    var disc, grossamt, misc, transchargs, cst, vat, TOTAL,gst,bt;
    misc = parseFloat(document.getElementById('<%=txtMiscelleneous.ClientID %>').value);
    disc = parseFloat(document.getElementById('<%=txtDiscount.ClientID %>').value);
    grossamt = parseFloat(document.getElementById('<%=txtGrossTotalAmtHidden.ClientID %>').value);
    gst = parseFloat(document.getElementById('<%=txtGST.ClientID %>').value);
   

    if (grossamt == "" || grossamt == "0" || isNaN(grossamt)) { grossamt = "0"; }
    if (misc == "" || misc == "0" || isNaN(misc)) { misc = "0"; }
    if (disc == "" || disc == "0" || isNaN(disc)) { disc = "0"; }
    if (gst == "" || gst == "0" || isNaN(gst)) { gst = "0"; }
    //if (vat == "" || vat == "0" || isNaN(vat)) { vat = "0"; }
    bt=parse (gst)
    TOTAL = parseFloat(grossamt);
    TOTAL = bt + TOTAL;
    TOTAL = TOTAL + parseFloat(misc);
    TOTAL = TOTAL - ((disc * TOTAL) / 100);
    TOTAL = Math.round(TOTAL * 100) / 100;
    
    document.getElementById('<%=txtGrossAmount.ClientID %>').value = TOTAL;
}


    </script>
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
    </style>

    <link href="/jquery-easyui-1.4.1/themes/default/easyui.css" rel="stylesheet" />
    <link href="/jquery-easyui-1.4.1/themes/icon.css" rel="stylesheet" />
    <link href="/jquery-easyui-1.4.1/demo/demo.css" rel="stylesheet" />
    <script src="/jquery-easyui-1.4.1/jquery.easyui.min.js"></script>

    <%--<script type="text/javascript"> 
        $(document).ready(function () {
            $("#<%=gvItDetails.ClientID%> input[id*='chk']:checkbox").click(function () {
                //Get number of checkboxes in list either checked or not checked
                var totalCheckboxes = $("#<%=gvItDetails.ClientID%> input[id*='chk']:checkbox").size();
                //Get number of checked checkboxes in list
                var checkedCheckboxes = $("#<%=gvItDetails.ClientID%> input[id*='chk']:checkbox:checked").size();
                //Check / Uncheck top checkbox if all the checked boxes in list are checked
                $("#<%=gvItDetails.ClientID%> input[id*='CheckBox_Header']:checkbox").attr('checked', totalCheckboxes == checkedCheckboxes);
            });

            $("#<%=gvItDetails.ClientID%> input[id*='CheckBox_Header']:checkbox").click(function () {
                //Check/uncheck all checkboxes in list according to main checkbox 
                $("#<%=gvItDetails.ClientID%> input[id*='chk']:checkbox").attr('checked', $(this).is(':checked'));
            });


            // Header Checkbox click function
            $('[id$=CheckBox_Header]').click(function () {
                if ($('[id$=CheckBox_Header]:checked').length > 0) {
                    $('[id$=chk]').parent().parent().addClass('highlightRow');
                }
                else {
                    $('[id$=chk]').parent().parent().removeClass('highlightRow');
                }
                $("[id$=chk]").attr('checked', this.checked);
            });

            // Child Checkbox click function
            $("[id$=chk]").click(function () {
                if (this.checked) {
                    $(this).parent().parent().addClass('highlightRow');
                }
                else {
                    $(this).parent().parent().removeClass('highlightRow');
                }

                if ($('[id$=chk]').length == $('[id$=chk]:checked').length) {
                    $('[id$=CheckBox_Header]').attr("checked", "checked");
                }
                else {
                    $('[id$=CheckBox_Header]').removeAttr("checked");
                }
            });


        });
    </script>--%>
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
    <style type="text/css">
        .highlightRow {
            background-color: bisque !important;
            Color: black !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>Purchase
                Invoice</td>
            <td>
                <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                    Visible="False"></asp:Label><asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label><asp:Label
                        ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                        Visible="False"></asp:Label><asp:Label ID="lblErrorItemCode" runat="server" Visible="false"></asp:Label>
            </td>
        </tr>
    </table>
    <table runat="server" visible="true" width="100%">
        <tr>
            <td></td>
            <td></td>
            <td style="text-align: right;"></td>
            <td style="text-align: center;">
                <table border="0" cellpadding="0" cellspacing="0" id="tblPIDetails" runat="server" visible="true" width="100%">
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">General Details</td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align :right"><asp:Label ID="lblSearchSupp" runat="server" Text="Search Supplier Name" ></asp:Label></td>
                        <td style="text-align:left"><asp:TextBox ID ="txtSearchSupp" runat="server" ></asp:TextBox>
                            <asp:Button ID="btnSupSearch" runat ="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton"  OnClick ="btnSupSearch_Click" Text="Go"/>
                            <asp:SqlDataSource ID="SqlDataSource" runat ="server" ConnectionString ="<%$ ConnectionStrings:DBCon %>" SelectCommand ="SCM_SEARCH_SELECT" SelectCommandType ="StoredProcedure"  >
                                <SelectParameters ><asp:ControlParameter PropertyName ="Text" Type="String" DefaultValue ="0" Name="SearchValue" ControlID ="txtSearchSupp" /></SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblSupplierName" runat="server" Text="Supplier Name"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlSupplierName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSupplierName_SelectedIndexChanged" >
                            </asp:DropDownList></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtContactPerson" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align:right"><asp:Label ID="lblSUPName" runat="server" Text="Supplier Name"></asp:Label></td>
                        <td style="text-align: left"><asp:TextBox ID="txtSupplierName" runat="server" Visible ="true" ReadOnly ="true"  ></asp:TextBox></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblPhone" runat="server" Text="Phone No."></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtPhoneNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblMobileNo" runat="server" Text="Mobile No"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtMobileNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;"><asp:Label ID="lblSupItems" Visible="false" runat ="server" Text="Item Code"></asp:Label></td>
                        <td style="text-align: left; height: 19px;">
                            <asp:DropDownList ID="ddlSupItems" runat ="server" AutoPostBack ="true" Visible ="false" OnSelectedIndexChanged ="ddlSupItems_SelectedIndexChanged" ></asp:DropDownList>
                        </td>
                        
                        
                    </tr>
                    <tr>
                        <td style="text-align :right"><asp:Label ID="lblSearchPO" runat ="server" Text="Search PO Reference No." ></asp:Label></td>
                        <td style="text-align :left"><asp:TextBox ID="txtSearchPo" runat ="server" ></asp:TextBox>
                        <asp:Button ID="btnSearchPO" runat ="server" BorderStyle ="None" CausesValidation ="false" EnableTheming ="false" CssClass ="gobutton" OnClick ="btnSearchPO_Click" Text ="Go" />
                           <asp:SqlDataSource ID="SqlDS1" runat ="server" ConnectionString ="<%$ ConnectionStrings:DBCon %>" SelectCommand ="SCM_SO_SEARCH_SELECT" SelectCommandType ="StoredProcedure"  >
                                <SelectParameters ><asp:ControlParameter PropertyName ="Text" Type="String" DefaultValue ="0" Name="SearchValue" ControlID ="txtSearchPo" /></SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblSalesOrderNo"  runat="server" Text="SPO No" Width="54px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlPONo" runat="server" Width="147px" AutoPostBack="True" OnSelectedIndexChanged="ddlPONo_SelectedIndexChanged">
                            </asp:DropDownList> <asp:TextBox ID="txtSPONo" runat ="server" ReadOnly ="true"  ></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblSalesOrderDate" runat="server" Text="PO Date" Width="64px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtPODate" runat="server" type="datepic"></asp:TextBox>&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr >
                        <td style="text-align: right">Purchase Orders :
                        </td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtPOOrders" runat="server" Enabled="False" TextMode="MultiLine" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                   <%-- <tr>
                        <td colspan="4" style="text-align: right">&nbsp;</td>
                    </tr>--%>

                    <tr>
                        <td colspan="4" style="text-align: center"></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">Invoice Details</td>
                    </tr>
                    <tr>
                        <td colspan="4" class="auto-style1"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label3" runat="server" Text="Pur. Voc. No."></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtInvoiceNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label6" runat="server" Text="Pur. Voc.  Date"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtInvoiceDate" runat="server" type="datepic"></asp:TextBox>&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label8" runat="server" Text="Supp. Inv. No."></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCustInvNo" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label29" runat="server" Text="Supp. Inv. Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCustInvDate" runat="server" type="datepic">
                            </asp:TextBox>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label5" runat="server" Text="Invoice Type"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:DropDownList ID="ddlInvoiceType" runat="server">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem>Tax Invoice</asp:ListItem>
                                <asp:ListItem>Commercial Invoice</asp:ListItem>
                                <asp:ListItem>Transport Document</asp:ListItem>
                                <asp:ListItem>Proforma Invoice</asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="height: 19px; text-align: right;">
                            <asp:Label ID="Label1" runat="server" Text="Despatch Mode"></asp:Label></td>
                        <td style="height: 19px; text-align: left;">
                            <asp:DropDownList ID="ddlDespatchMode"  runat="server">
                            </asp:DropDownList>
                            <asp:Label ID="Label36" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="ddlDespatchMode" ErrorMessage="Please Select the Despatch Mode" InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style ="text-align : right ">
                            <asp:Label ID ="lbl1" runat ="server" Text ="Vehicle No"></asp:Label>
                        </td>
                        <td style ="text-align :left">
                            <asp:TextBox ID="txtVehicleNo" runat ="server" ></asp:TextBox>
                        </td>
                        <td style ="text-align : right ">
                            <asp:Label ID ="Label20" runat ="server" Text ="Arrival Dt"></asp:Label>
                        </td>
                        <td style ="text-align :left">
                            <asp:TextBox ID="txtVehicalArrDt" runat ="server" type="datepic" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label2" runat="server" Text="Terms of Delivery"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtTermsOfDelivery" runat="server" CssClass="multilinetext" EnableTheming="False" TextMode="MultiLine" Width="81%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">Items Details</td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;</td>
                    </tr>

                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:GridView ID="gvItDetails" runat="server" AutoGenerateColumns="False"
                                Width="100%" OnRowDataBound="gvItDetails_RowDataBound" OnRowEditing="gvItDetails_RowEditing">
                                <Columns>
                                    <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                    <asp:BoundField DataField="ItemType" HeaderText="Model No"></asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Model Name"></asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                    <asp:BoundField DataField ="HSNCode" HeaderText="HSN Code" />
                                    <asp:BoundField DataField ="GST" HeaderText="GST(%)" />
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                    <asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
                                    
                                    <asp:BoundField HeaderText="Amount"></asp:BoundField>
                                    <asp:BoundField HeaderText="GST Amount" />
                                    <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="DeliveryDate" HeaderText="Delivery Date"></asp:BoundField>
                                    <asp:BoundField DataField="Specifications" HeaderText="Specifications">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ItemTypeId" HeaderText="Item Type Id"></asp:BoundField>
                                    <asp:BoundField DataField="Customer" HeaderText="Suggested Party"></asp:BoundField>
                                    <asp:BoundField DataField="Disc" HeaderText="Discount"></asp:BoundField>
                                    <asp:BoundField DataField ="BalanceQty" HeaderText="Balance Quantity" />
                                    <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox runat="server" ID="CheckBox_Header" Width="25px" OnClick="selectAll(this)"/>
                                </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" __designer:wfdid="w5" ></asp:CheckBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ColorId" HeaderText="Color Id"></asp:BoundField>
                                    <asp:BoundField DataField="FPO_DET_ID" HeaderText="Det Id"></asp:BoundField>
                                    <asp:BoundField DataField="FPO_ID" HeaderText="Id"></asp:BoundField>

                                </Columns>
                                <EmptyDataTemplate>
                                    <span style="color: #ff0000">No Data Exists</span>&nbsp; 
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" ValidationGroup="ip1" />
                            <asp:Button ID="btnItemRefresh" runat="server" OnClick="btnItemRefresh_Click" Text="Refresh" /></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left; height: 12px;">Purchase Invoiced Items</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:GridView ID="gvPIDetails" runat="server" AutoGenerateColumns ="false" Width="100%" OnRowDataBound ="gvPIDetails_RowDataBound" OnRowDeleting ="gvPIDetails_RowDeleting" SelectedRowStyle-BackColor="#c0c0c0" >
                                <FooterStyle BackColor="#1AA8BE" />
                                <Columns >
                                    <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                            <asp:BoundField DataField="ItemType" HeaderText="Model No">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ItemName" HeaderText="Item Name">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="UOM" HeaderText="UOM">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField ="GST" HeaderText="GST" />
                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>
                                    <asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
                                    <asp:BoundField DataField="Discount" HeaderText="Discount"></asp:BoundField>
                                    <asp:BoundField DataField="Amount" HeaderText="Amount"></asp:BoundField>
                                    <asp:BoundField HeaderText ="GST Amount" />
                                    <asp:BoundField DataField="SugParty" HeaderText="Suggested Party"></asp:BoundField>
                                    <asp:BoundField DataField ="PONo" HeaderText ="PO No" />
                                  <asp:BoundField DataField ="PIID" HeaderText="PIIDHidden" />
                                    <%--<asp:BoundField DataField ="DetId" HeaderText ="DetId" />--%>
                                    <asp:BoundField DataField ="ColorId" HeaderText="ColorId" />
                                    <asp:TemplateField HeaderText="Status" ControlStyle-Width="100px">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlStatus" runat="server">
                                    <asp:ListItem Value="Shipped">Shipped</asp:ListItem>
                                    <asp:ListItem Value="In-Transit">In-Transit</asp:ListItem>
                                    <asp:ListItem Value="Delivered">Delivered</asp:ListItem>
                                    <asp:ListItem Value="Arrived">Arrived</asp:ListItem>

                                </asp:DropDownList>
                                <asp:HiddenField ID="cthf1" runat="server" Value='<%# Eval("Status") %>' />
                            </ItemTemplate>
                          

<ControlStyle Width="100px"></ControlStyle>
                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="App DeliveryDt" ControlStyle-Width="80px">
                            <ItemTemplate>
                                <asp:TextBox ID="txtAppDeliveryDt" runat="server"   Text='<%# Convert.ToDateTime(Eval("AppDeliveryDt")).ToString("dd/MM/yyyy") %>' type="datepic" ></asp:TextBox>
                            </ItemTemplate>

<ControlStyle Width="80px"></ControlStyle>
                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Update">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnUpdate" CausesValidation="false" ForeColor="Blue" runat="server" OnClick="lbtnUpdate_Click">Update</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnDelete" CausesValidation="false" ForeColor="Blue" runat="server"  OnClick="lbtnDelete_Click">Delete</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>  
                                           
                                </Columns>
                                <EmptyDataTemplate>
                                            <span style="font-size: 8pt; color: #ff0033; font-family: Verdana">No Items Delivered</span>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                                <td>&nbsp;<asp:Label ID="lblTtlAmt" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblTtlGSTAmt" runat="server" Visible ="false"  ></asp:Label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                    <tr>
                        <td>
                            <table id="tblItemDtls" runat="server" visible="false">
                                <tr>
                                    <td colspan="4" style="text-align: left" class="profilehead">Items Details</td>
                                </tr>
                                <tr>
                                    <td colspan="4">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="height: 20px; text-align: right"></td>
                                    <td style="height: 20px; text-align: left"></td>
                                    <td style="height: 20px; text-align: right;">
                                        <asp:Label ID="Label35" runat="server" Text="Search By Brand"></asp:Label></td>
                                    <td style="height: 20px; text-align: left">
                                        <asp:DropDownList ID="ddlBrandselect" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrandselect_SelectedIndexChanged">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td style="height: 24px; text-align: right">
                                        <asp:Label ID="lblItemCode" runat="server" Text="Model No"></asp:Label>
                                    </td>
                                    <td style="height: 24px; text-align: left;">
                                        <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Label ID="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="ddlItemType" ErrorMessage="Please Select the Item Type"
                                            ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
                                    <td style="height: 24px; text-align: right;">
                                        <asp:Label ID="Label13" runat="server" Text="Model Name"></asp:Label></td>
                                    <td style="height: 24px; text-align: left;">
                                        <asp:TextBox ID="txtModelName" runat="server">
                                        </asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="height: 24px; text-align: right">
                                        <asp:Label ID="Label30" runat="server" Text="Item Category"></asp:Label></td>
                                    <td style="height: 24px; text-align: left">
                                        <asp:TextBox ID="txtItemCategory" runat="server">
                                        </asp:TextBox></td>
                                    <td style="height: 24px; text-align: right;">
                                        <asp:Label ID="Label31" runat="server" Text="ItemSubCategory"></asp:Label></td>
                                    <td style="height: 24px; text-align: left">
                                        <asp:TextBox ID="txtItemSubCategory" runat="server">
                                        </asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; height: 21px;">
                                        <asp:Label ID="lblUOM" runat="server" Text="UOM" Width="50px"></asp:Label></td>
                                    <td style="height: 21px; text-align: left;">
                                        <asp:TextBox ID="txtUOM" runat="server" ReadOnly="True"></asp:TextBox></td>
                                    <td style="text-align: right; height: 21px;">
                                        <asp:Label ID="Label33" runat="server" Text="Color"></asp:Label></td>
                                    <td style="height: 21px; text-align: left;">
                                        <asp:TextBox ID="txtColor" runat="server" ReadOnly="true" Visible="false"></asp:TextBox>
                                        <asp:DropDownList ID="ddlColor" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 21px; text-align: right">
                                        <asp:Label ID="Label34" runat="server" Text="Brand"></asp:Label></td>
                                    <td style="height: 21px; text-align: left">
                                        <asp:TextBox ID="txtBrand" runat="server">
                                        </asp:TextBox></td>
                                    <td style="height: 21px; text-align: right">
                                        <asp:Label ID="Label32" runat="server" Text="Ordered Quantity" Visible="False"></asp:Label></td>
                                    <td style="height: 21px; text-align: left">
                                        <asp:TextBox ID="txtOrderedQuantity" runat="server" Visible="False"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="height: 21px; text-align: right">
                                        <asp:Label ID="Label23" runat="server" Text="Item Specification"></asp:Label></td>
                                    <td colspan="3" style="height: 21px; text-align: left">
                                        <asp:TextBox ID="txtItemSpec" runat="server" CssClass="multilinetext" EnableTheming="False"
                                            ReadOnly="True" TextMode="MultiLine" Width="81%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <%--<td style="text-align: right"></td>
                        <td style="text-align: left">
                            <asp:RadioButton ID="rbVAT" runat="server" Checked="True" GroupName="vatcst" Text="VAT"></asp:RadioButton><asp:RadioButton ID="rbCST" runat="server" GroupName="vatcst" Text="C.S. Tax"></asp:RadioButton></td>
                                    --%>
                                    <td style="text-align: right;">
                                        <asp:Label ID="Label24" runat="server" Text="Excise" Visible="False"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtExcise" runat="server" Visible="False">0</asp:TextBox><asp:Label ID="Label27" runat="server" EnableTheming="False" Font-Bold="False"
                                            Font-Names="Verdana" Font-Size="Smaller" Text="%" Visible="False"></asp:Label>
                                        <br />
                                        <cc1:FilteredTextBoxExtender ID="ftxteExcise" runat="server" TargetControlID="txtExcise"
                                            ValidChars=".0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <%-- <td style="text-align: right">
                            <asp:Label ID="lblCSTax" runat="server" Text="CST"></asp:Label><asp:Label ID="lblVAT" runat="server" Text="VAT"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtVAT" runat="server">0</asp:TextBox><asp:TextBox ID="txtCST" runat="server">0</asp:TextBox><asp:Label ID="Label25" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" Text="%"></asp:Label><asp:Label ID="Label17"
                                    runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana" Font-Size="Smaller"
                                    ForeColor="Red" Text="*"></asp:Label>&nbsp;
                        <cc1:FilteredTextBoxExtender ID="ftxteVat" runat="server" TargetControlID="txtVAT"
                            ValidChars=".0123456789">
                        </cc1:FilteredTextBoxExtender>
                            <cc1:FilteredTextBoxExtender ID="ftxteCST" runat="server" TargetControlID="txtCST"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>--%>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblRate" runat="server" Text="Rate"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtRate" runat="server">
                                        </asp:TextBox><asp:Label ID="Label21" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                ControlToValidate="txtRate" ErrorMessage="Please Enter the Rate" ValidationGroup="ip">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender ID="ftxteRate" runat="server" TargetControlID="txtRate"
                                                    ValidChars=".0123456789">
                                                </cc1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblQuantity" runat="server" Text="Receiving Quantity"></asp:Label></td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtQuantity" runat="server" Width="139px">
                                        </asp:TextBox><asp:Label ID="Label19" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                ControlToValidate="txtQuantity" ErrorMessage="Please Enter the Quantity" ValidationGroup="ip">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender ID="ftxteQuantity" runat="server" FilterType="Numbers"
                                                    TargetControlID="txtQuantity" ValidChars=".">
                                                </cc1:FilteredTextBoxExtender>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblAmount" runat="server" Text="Amount"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtAmount" runat="server" ReadOnly="True"></asp:TextBox>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 19px; text-align: right;">
                                        <asp:Label ID="Label14" runat="server" Text="ItemImage"></asp:Label></td>
                                    <td style="height: 19px; text-align: left;">
                                        <asp:Image ID="Image1" runat="server" Height="85px" ImageUrl="~/Images/noimage400x300.gif"
                                            Width="140px"></asp:Image></td>
                                    <td style="height: 19px;"></td>
                                    <td style="height: 19px"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    
                    <tr>
                        <td colspan="4">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: right"></td>
                    </tr>
                    <tr>
                        <td style="text-align: center;" colspan="4">
                            <asp:GridView ID="gvItemDetails" runat="server" AutoGenerateColumns="False"
                                OnRowDeleting="gvItemDetails_RowDeleting" Width="100%" OnRowDataBound="gvItemDetails_RowDataBound" OnRowEditing="gvItemDetails_RowEditing" >
                                <Columns>
                                    <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                    <asp:BoundField DataField="ItemCode" HeaderText="ItemCode"></asp:BoundField>
                                    <asp:BoundField DataField="ItemType" HeaderText="Model No"></asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                    <asp:BoundField DataField ="HSN_Code" HeaderText ="HSN Code" />
                                    <asp:TemplateField HeaderText="GST(%)" ControlStyle-Width ="80px" >
                                        <ItemTemplate >
                                            <asp:TextBox ID="txtDetGST" Text='<%# Bind("GST") %>' runat ="server" AutoPostBack ="true" OnTextChanged ="txtDetGST_TextChanged" ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>--%>
                                    <asp:TemplateField HeaderText="Quantity" ControlStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQty" Text='<%# Bind("Quantity") %>' runat="server" AutoPostBack="true" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>--%>
                                    <asp:TemplateField HeaderText="Rate" ControlStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRate" Text='<%# Bind("Rate") %>' runat="server" AutoPostBack="true" OnTextChanged="txtRate_TextChanged"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--   <asp:BoundField DataField="VAT" HeaderText="VAT"></asp:BoundField>
                                    <asp:BoundField DataField="cst" HeaderText="CST"></asp:BoundField>
                                    <asp:BoundField DataField="Excise" HeaderText="Excise"></asp:BoundField>
                                    --%>
                                    <asp:TemplateField HeaderText="Discount" ControlStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDiscount" Text='<%# Bind("Discount") %>' runat="server" AutoPostBack="true" OnTextChanged="txtDiscount_TextChanged"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField HeaderText="Amount"></asp:BoundField>--%>
                                    <asp:TemplateField HeaderText="Amount" ControlStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText ="GST Amount" ControlStyle-Width ="80px" >
                                        <ItemTemplate >
                                            <asp:Label ID="lblGSTAmount" runat ="server" ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SugParty" HeaderText="Suggested Party"></asp:BoundField>
                                    <asp:BoundField DataField="ItemTypeId" HeaderText="ItemTypeIdHidden"></asp:BoundField>
                                    <asp:BoundField DataField="PONo" HeaderText="PONO"></asp:BoundField>
                                    <asp:BoundField DataField="ColorId" HeaderText="ColorId"></asp:BoundField>
                                    <asp:BoundField DataField="FPO_DET_ID" HeaderText="Det Id"></asp:BoundField>
                                    <asp:BoundField DataField="FPO_ID" HeaderText="Id"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Status" ControlStyle-Width="100px">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlStatus" runat="server">
                                    <asp:ListItem Value="Arriving">Arriving</asp:ListItem>
                                    <asp:ListItem Value="In-Transit">In-Transit</asp:ListItem>
                                    <asp:ListItem Value="Arrived">Arrived</asp:ListItem>

                                </asp:DropDownList>
                                <asp:HiddenField ID="cthf1" runat="server" Value='<%# Eval("Status") %>' />
                            </ItemTemplate>
                          

<ControlStyle Width="100px"></ControlStyle>
                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="App DeliveryDt" ControlStyle-Width="80px">
                            <ItemTemplate>
                                <asp:TextBox ID="txtAppDeliveryDt" runat="server" Text='<%# Bind("AppDeliveryDt") %>' type="datepic" ></asp:TextBox>
                            </ItemTemplate>

<ControlStyle Width="80px"></ControlStyle>
                        </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <span style="color: #ff0033">No Data Exits</span>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <%--<tr style="background-color:#98b7e9">
                        <td style=" width:20%;"></td>
                        <td style="text-align :right;width:60%;color:black;font-weight:bold">
                                    Total Amount :
                                </td>
                                <td style=" text-align :center; color:red; font-weight:bold; width:10%"><asp:Label ID="lblTtlAmt" runat="server">0</asp:Label></td>
                                
                                <td style=" text-align :left;width:10%;color:red;font-weight:bold"><asp:Label ID="lblGSTTtlAmt" runat="server">0</asp:Label></td>
                                
                            </tr>--%>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">Invoice Details</td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;<asp:Label ID="lblCurrent_ttl" runat ="server" Visible ="false"></asp:Label>
                            <asp:Label  ID="lblCurrent_GST" runat ="server"  Visible ="false"></asp:Label>
                            <asp:Label id="lbltempGross_woDisc" runat ="server" Visible ="false" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label37" runat="server" Text="Pay By LC"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtpaybylc" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label38" runat="server" Text="LC Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtLCdate" runat="server" type="datepic">
                            </asp:TextBox>
                            <%-- <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>
                            <cc1:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender2" runat="server" Enabled="True" PopupButtonID="Image2"
                                TargetControlID="txtLCdate">
                            </cc1:CalendarExtender>--%>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label39" runat="server" Text="LC Exp Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtlcexpdate" runat="server" type="datepic">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label40" runat="server" Text="Pay By TT"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPaybytt" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label41" runat="server" Text="TT Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtttdate" runat="server" type="datepic">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label18" runat="server" Text="Cheque No"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtChequeNo" runat="server"></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label26" runat="server" Text="Cheque Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDate" runat="server" type="datepic">
                            </asp:TextBox>&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label28" runat="server" Text="Bank"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtBank" runat="server" CssClass="multilinetext" EnableTheming="False" TextMode="MultiLine" Width="81%">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td colspan="3" style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4">Other Charges</td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label11" runat="server" Text="Miscelleneous"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtMiscelleneous" runat="server"></asp:TextBox><cc1:FilteredTextBoxExtender ID="ftxteMiscelleneous" runat="server" TargetControlID="txtMiscelleneous"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label9" runat="server" Text="Discount"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtDiscount" runat="server"></asp:TextBox><asp:Label ID="Label22" runat="server" Text="%"></asp:Label><cc1:FilteredTextBoxExtender ID="ftxteDiscount" runat="server" TargetControlID="txtDiscount"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left">
                            <asp:RadioButton ID="rbVAT" runat="server" Checked="True" GroupName="vatcst" Text="IGST"></asp:RadioButton><asp:RadioButton ID="rbCST" runat="server" GroupName="vatcst" Text="CGST/SCST"></asp:RadioButton></td>
                        <td style="text-align: right;">
                                    <asp:Label ID="Label17" runat="server" Text="Total"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtTotalAmt" runat="server" ReadOnly="True"></asp:TextBox><cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtMiscelleneous"
                                        ValidChars=".0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblCSTax" runat="server" Text="CGST/SGST"></asp:Label><asp:Label ID="lblVAT" runat="server" Text="IGST"></asp:Label></td>
                        <td style="text-align: left"><asp:TextBox ID="txtGST" runat="server" ReadOnly ="true"  ></asp:TextBox>
                            <%--<asp:TextBox ID="txtVAT" runat="server">0</asp:TextBox><asp:TextBox ID="txtCST" runat="server">0</asp:TextBox><asp:Label ID="Label25" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" Text="%"></asp:Label><asp:Label ID="Label17"
                                    runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana" Font-Size="Smaller"
                                    ForeColor="Red" Text="*"></asp:Label>&nbsp;
                        <cc1:FilteredTextBoxExtender ID="ftxteVat" runat="server" TargetControlID="txtVAT"
                            ValidChars=".0123456789">
                        </cc1:FilteredTextBoxExtender>
                            <cc1:FilteredTextBoxExtender ID="ftxteCST" runat="server" TargetControlID="txtCST"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>--%>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label10" runat="server" Text="Gross Amount"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtGrossAmount" runat="server" ReadOnly="True"></asp:TextBox><asp:HiddenField ID="txtGrossTotalAmtHidden" runat="server" />
                        </td>

                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label4" runat="server" Text="Remarks"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="multilinetext" EnableTheming="False" TextMode="MultiLine" Width="81%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label12" runat="server" Text="Packing Charges  " Visible="False"></asp:Label></td>
                        <td colspan="1" style="text-align: left">
                            <asp:TextBox ID="txtPackingCharges" runat="server" Visible="False"></asp:TextBox><asp:Label ID="Label16" runat="server" Text="Insurance" Visible="False"></asp:Label><asp:TextBox ID="txtInsurance" runat="server" Visible="False"></asp:TextBox><cc1:FilteredTextBoxExtender ID="ftxtePackingCharges" runat="server" TargetControlID="txtPackingCharges"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td colspan="3" style="text-align: left">
                            <asp:Label ID="Label15" runat="server" Text="Transportation Charges" Width="148px" Visible="False"></asp:Label><asp:TextBox ID="txtTranportationCharges" runat="server" Visible="False"></asp:TextBox><cc1:FilteredTextBoxExtender ID="ftxteTrasncharges" runat="server" TargetControlID="txtTranportationCharges"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="4">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">Reference Details</td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;
                          <asp:Label ID="lblPIItemCode" runat="server" Visible ="false"></asp:Label>
                            <asp:Label ID="lblPIColorId" runat="server" Visible ="false"></asp:Label>
                            <asp:Label ID="lblPIQty" runat="server" Visible ="false" ></asp:Label>
                            <asp:Label ID="lblPIDetId" runat="server" Visible ="false"></asp:Label>
                            <asp:Label ID="lblPIId" runat ="server" Visible ="false" ></asp:Label>
                            <asp:Label ID="lblFPODetID" runat ="server" Visible ="false" ></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlApprovedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right;"></td>
                        <td style="text-align: left;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;"></td>
                        <td style="text-align: left; height: 19px;"></td>
                        <td style="text-align: right; height: 19px;"></td>
                        <td style="text-align: left; height: 19px;"></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 47px">
                            <table id="tblButtons" align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CausesValidation="False" OnClick="btnSave_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnApprove" runat="server" Text="Approve" CausesValidation="False" OnClick="btnApprove_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CausesValidation="False" OnClick="btnRefresh_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnClose" runat="server" Text="Close" CausesValidation="False" OnClick="btnClose_Click" /></td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False"></asp:ValidationSummary>
                <asp:ValidationSummary ID="VS2" runat="server" ValidationGroup="ip" ShowMessageBox="True"
                    ShowSummary="False"></asp:ValidationSummary>
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td style="text-align: right"></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td style="text-align: right"></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td style="text-align: right"></td>
            <td></td>
        </tr>
    </table>
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


 
