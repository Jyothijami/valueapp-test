<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/SalesMP1.master" AutoEventWireup="true" CodeFile="SalesQuotationDetails1 - Copy (2).aspx.cs" Inherits="Modules_SM_SalesQuotationDetails1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .auto-style1 {
            width: 151px;
        }

        .profilehead {
            text-align: left;
        }

        .auto-style2 {
            height: 19px;
        }

        .auto-style3 {
            width: 357px;
            height: 19px;
        }

        .auto-style4 {
            width: 141px;
        }

        .auto-style5 {
            text-align: left;
            height: 20px;
        }
    </style>
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
        function amtcalc() {

            var req_qty, rate, disc, SPprice;
            var gst_Amt, gst_Tax;

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

    SPprice = document.getElementById('<%=txtSpPrice.ClientID %>').value;
            gst_Tax = document.getElementById('<%=txtGST_Perc.ClientID %>').value;
            //txtGST_Amt txtGST_Perc
            if (SPprice > 0) {
                document.getElementById('<%=txtGST_Amt.ClientID %>').value = parseFloat((SPprice * gst_Tax) / 100).toFixed(2);
            }
            else if (SPprice == "" || SPprice == "0") {
                document.getElementById('<%=txtGST_Amt.ClientID %>').value = "0";
            }

    }

    function Optamtcalc() {

        var req_qty, rate, disc, SPprice;
        req_qty = document.getElementById('<%=txtOptQty.ClientID %>').value;
        rate = document.getElementById('<%=txtOptRate.ClientID %>').value;
        disc = document.getElementById('<%=txtOptDisc.ClientID %>').value;


        if (req_qty == "" || req_qty == "0") {
            document.getElementById('<%=txtOptSpPrice.ClientID %>').value = "0";
        }
        else if (rate == "" || rate == "0") {
            document.getElementById('<%=txtOptSpPrice.ClientID %>').value = "0";
        }
        else if (rate > 0 && req_qty > 0) {
            document.getElementById('<%=txtOptSpPrice.ClientID %>').value = (rate * req_qty);
        }
    if (disc != "" && disc != "0") {
        document.getElementById('<%=txtOptSpPrice.ClientID %>').value = parseFloat((rate * req_qty)) - parseFloat((((rate * req_qty) * disc) / 100));



    }

}

function amtcalcDisc() {

    var req_qty, rate, spprice;
    var gst_Amt, gst_Tax;

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

    gst_Tax = document.getElementById('<%=txtGST_Perc.ClientID %>').value;
    //txtGST_Amt txtGST_Perc
    if (spprice > 0) {
        document.getElementById('<%=txtGST_Amt.ClientID %>').value = parseFloat((spprice * gst_Tax) / 100).toFixed(2);
    }
    else if (spprice == "" || spprice == "0") {
        document.getElementById('<%=txtGST_Amt.ClientID %>').value = "0";
    }


}


function Gst_amtcalc() {

    var spprice;
    var gst_Amt, gst_Tax;

    gst_Tax = document.getElementById('<%=txtGST_Perc.ClientID %>').value;
    spprice = document.getElementById('<%=txtSpPrice.ClientID %>').value;

    if (gst_Tax == "" || gst_Tax == "0") {
        document.getElementById('<%=txtGST_Amt.ClientID %>').value = "0";
    }
    else if (spprice == "" || spprice == "0") {
        document.getElementById('<%=txtGST_Amt.ClientID %>').value = "0";
    }
    else if (spprice > 0 && gst_Tax > 0) {
        document.getElementById('<%=txtGST_Amt.ClientID %>').value = parseFloat((spprice * gst_Tax) / 100).toFixed(2);
    }

}

function Gst_Disc_calc() {

    var spprice;
    var gst_Amt, gst_Tax;

    gst_Amt = document.getElementById('<%=txtGST_Amt.ClientID %>').value;
    spprice = document.getElementById('<%=txtSpPrice.ClientID %>').value;

    if (gst_Amt == "" || gst_Amt == "0") {
        document.getElementById('<%=txtGST_Perc.ClientID %>').value = "0";
    }
    else if (spprice == "" || spprice == "0") {
        document.getElementById('<%=txtGST_Perc.ClientID %>').value = "0";
    }
    else if (spprice > 0 && gst_Amt > 0) {

        document.getElementById('<%=txtGST_Perc.ClientID %>').value = parseFloat((gst_Amt * 100) / spprice).toFixed(2);
            }

}


function OptamtcalcDisc() {

    var req_qty, rate, spprice;
    req_qty = document.getElementById('<%=txtOptQty.ClientID %>').value;
    rate = document.getElementById('<%=txtOptRate.ClientID %>').value;
    spprice = document.getElementById('<%=txtOptSpPrice.ClientID %>').value;

    if (req_qty == "" || req_qty == "0") {
        document.getElementById('<%=txtOptSpPrice.ClientID %>').value = "0";
    }
    else if (rate == "" || rate == "0") {
        document.getElementById('<%=txtOptSpPrice.ClientID %>').value = "0";
    }
    else if (rate > 0 && req_qty > 0) {
        document.getElementById('<%=txtOptDisc.ClientID %>').value = (((rate * req_qty) - spprice) * 100) / (rate * req_qty);
    }

}


function summarycalc() {

    var stotal, spdiscount, ssubtotal, svat, scst, sgrandvat, sgrandcst;
    stotal = document.getElementById('<%=txttotal.ClientID %>').value;
    spdiscount = document.getElementById('<%=txtspldiscount.ClientID %>').value;
    ssubtotal = document.getElementById('<%=txtsubtotal.ClientID %>').value;
    svat = document.getElementById('<%=txtsummaryvat.ClientID %>').value;
    scst = document.getElementById('<%=txtsummaryCst.ClientID %>').value;



    if (spdiscount != "" && spdiscount != "0") {
        document.getElementById('<%=txtsubtotal.ClientID %>').value = parseFloat(stotal) - parseFloat((((stotal) * spdiscount) / 100));
        ssubtotal = document.getElementById('<%=txtsubtotal.ClientID %>').value;
        document.getElementById('<%=txtsummaryvat.ClientID %>').value = parseFloat((((ssubtotal) * 18) / 100));
        document.getElementById('<%=txtsummaryCst.ClientID %>').value = parseFloat((((ssubtotal) * 2) / 100));
        svat = document.getElementById('<%=txtsummaryvat.ClientID %>').value;
        scst = document.getElementById('<%=txtsummaryCst.ClientID %>').value;
        document.getElementById('<%=txtsummaryGtoalvat.ClientID %>').value = parseFloat(ssubtotal) + parseFloat(svat);
        document.getElementById('<%=txtsummaryGtotalcst.ClientID %>').value = parseFloat(ssubtotal) + parseFloat(scst);

    }

}


    </script>
    <script language="javascript" type="text/javascript">
        function check(e) {
            var keynum
            var keychar
            var numcheck
            // For Internet Explorer
            if (window.event) {
                keynum = e.keyCode;
            }
                // For Netscape/Firefox/Opera
            else if (e.which) {
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);
            //List of special characters you want to restrict
            if (keychar == "'" || keychar == "`" || keychar == "&" || keychar == "¬" || keychar == '"') {
                return false;
            } else {
                return true;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>


            <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
                <tr>
                    <td style="text-align: left;">Sales Quotation Details </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" id="tblQuotationDetails" runat="server"
                visible="true" width="100%">
                <tr>
                    <td class="profilehead" colspan="4">General Details</td>
                </tr>
                <tr>
                    <td style="height: 19px; text-align: right"></td>
                    <td style="height: 19px; text-align: left; width: 357px;"></td>
                    <td style="height: 19px; text-align: right"></td>
                    <td style="height: 19px; text-align: left"></td>
                </tr>
                <tr>
                    <td style="text-align: right">Enquiry No
                :
                <%--<asp:Label ID="lblUOM" runat="server" Text="UOM"></asp:Label>--%>

                    </td>
                    <td style="text-align: left; width: 357px;">
                        <asp:DropDownList ID="ddlEnquiryNo" runat="server" OnSelectedIndexChanged="ddlEnquiryNo_SelectedIndexChanged"
                            AutoPostBack="True" Enabled="False">
                        </asp:DropDownList>
                        <asp:Label ID="Label36" runat="server" EnableTheming="False" ForeColor="Red"
                            Text="*" Visible="False"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                            runat="server" ControlToValidate="ddlEnquiryNo" ErrorMessage="Please Select  the Enquiry No."
                            InitialValue="0" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                    <td style="text-align: right;">Enquiry Date
                :
                <%--<asp:Label ID="Label61" runat="server" Text="Room "></asp:Label>--%>

                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtEnquiryDate" runat="server" type="Datepic" CssClass="datetext" EnableTheming="False"
                            ReadOnly="True"></asp:TextBox>&nbsp;
               
                    </td>
                </tr>
                <tr>
                    <td id="TD18" style="height: 22px; text-align: right">Customer:
                <%--<asp:Label ID="Label22" runat="server" Text="Item Specification"></asp:Label>--%>

                    </td>
                    <td style="height: 22px; text-align: left; width: 357px;">
                        <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged"
                            Enabled="False">
                        </asp:DropDownList>&nbsp;<%--<asp:Label ID="lblQuantity" runat="server" Text="Quantity" Width="54px"></asp:Label>--%><asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlCustomer"
                            ErrorMessage="Please Select the Customer" InitialValue="0" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                    <td style="height: 22px; text-align: right">
                        <%--<asp:Label ID="Label26" runat="server" EnableTheming="False" ForeColor="Red"
                    Text="*">
                </asp:Label>--%>Region
                :
                    </td>
                    <td style="height: 22px; text-align: left">
                        <asp:TextBox ID="txtRegion" runat="server" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="height: 22px; text-align: right">
                        <%--<asp:Label ID="Label65" runat="server" Text="Select Model"></asp:Label>--%>Industry Type
                :
                    </td>
                    <td style="height: 22px; text-align: left; width: 357px;">
                        <asp:TextBox ID="txtIndustryType" runat="server" ReadOnly="True">
                        </asp:TextBox></td>
                    <td style="height: 22px; text-align: right">
                        <%--<asp:Label ID="Label4" runat="server" Text="Model No "></asp:Label>--%>Unit Name
                :
                    </td>
                    <td style="height: 22px; text-align: left">
                        <asp:DropDownList ID="ddlUnitName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged">
                            <asp:ListItem Value="0">--</asp:ListItem>
                            <asp:ListItem Value="0">--Select Customer--</asp:ListItem>
                        </asp:DropDownList><asp:RequiredFieldValidator ID="rfvUnitName" runat="server" ControlToValidate="ddlUnitName"
                            ErrorMessage="Please Select the Unit Name" InitialValue="0" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblUnitAddress" runat="server" Text="Unit Address :" Width="106px"></asp:Label>

                    </td>
                    <td colspan="3" style="text-align: left">
                        <asp:TextBox ID="txtUnitAddress" runat="server" CssClass="multilinetext" EnableTheming="False"
                            Font-Names="Verdana" Font-Size="8pt" TextMode="MultiLine" Width="569px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <%--<asp:Label ID="lblItemName" runat="server" Text="Model Name" Width="95px"></asp:Label>--%>Contact Person
                :
                    </td>
                    <td style="text-align: left; width: 357px;">
                        <asp:TextBox ID="txtContactPerson" runat="server" ReadOnly="True"></asp:TextBox><asp:DropDownList
                            ID="ddlContactPerson" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlContactPerson_SelectedIndexChanged"
                            Visible="False">
                            <asp:ListItem Value="0">--</asp:ListItem>
                            <asp:ListItem Value="0">--Select Unit Name--</asp:ListItem>
                        </asp:DropDownList><asp:RequiredFieldValidator ID="rfvContactPerson" runat="server"
                            ControlToValidate="ddlContactPerson" ErrorMessage="Please Select the Contact Person"
                            InitialValue="0">*</asp:RequiredFieldValidator></td>
                    <td style="text-align: right">
                        <%--<asp:Label ID="Label25" runat="server" Text="Item Category "></asp:Label>--%>Email
                :
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr>
                    <td id="TD28" style="text-align: right">
                        <%--<asp:Label ID="Label58" runat="server" Text="Item SubCategory "></asp:Label>--%>Phone No
                :
                    </td>
                    <td style="text-align: left; width: 357px;">
                        <asp:TextBox ID="txtPhoneNo" runat="server" ReadOnly="True">
                        </asp:TextBox></td>
                    <td style="text-align: right">
                        <%--<asp:Label ID="Label60" runat="server" Text="Brand "></asp:Label>--%>Mobile
                :
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True">
                        </asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right; height: 19px;"></td>
                    <td style="text-align: left; height: 19px; width: 357px;"></td>
                    <td style="text-align: right; height: 19px;"></td>
                    <td style="text-align: left; height: 19px;"></td>
                </tr>
                <tr>
                    <td class="profilehead" colspan="4">Quotation Details</td>
                </tr>
                <tr>
                    <td style="height: 19px; text-align: right"></td>
                    <td style="height: 19px; text-align: left; width: 357px;"></td>
                    <td style="height: 19px; text-align: right"></td>
                    <td style="height: 19px; text-align: left"></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <%--<asp:Label ID="lblUOM" runat="server" Text="UOM"></asp:Label>--%>Quotation No
                :
                    </td>
                    <td style="text-align: left; width: 357px;">
                        <asp:TextBox ID="txtQuotationNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                    <td style="text-align: right;">
                        <%--<asp:Label ID="Label61" runat="server" Text="Room "></asp:Label>--%>Quotation Date

                :

                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtQuotationDate" runat="server" type="Datepic" CssClass="datetext" EnableTheming="False" Enabled="false"></asp:TextBox>&nbsp;
                
                <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="DateCustomValidate"
                    ControlToValidate="txtQuotationDate" ErrorMessage="Please Enter the Quotation Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                    SetFocusOnError="True" ValidationGroup="a">*</asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <%--<asp:Label ID="Label22" runat="server" Text="Item Specification"></asp:Label>--%>Company :
                    </td>
                    <td style="text-align: left; width: 357px;">
                        <asp:DropDownList ID="ddlCompany" runat="server">
                        </asp:DropDownList>&nbsp;<%--<asp:Label ID="lblQuantity" runat="server" Text="Quantity" Width="54px"></asp:Label>--%><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCompany"
                            ErrorMessage="Select Company Name" InitialValue="0" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                    <td style="text-align: right">
                        <%--<asp:Label ID="Label26" runat="server" EnableTheming="False" ForeColor="Red"
                    Text="*">
                </asp:Label>--%>Quotation Type
                :
                    </td>
                    <td style="text-align: left">
                        <table class="stacktable">
                            <tr>
                                <td class="auto-style1">
                                    <asp:RadioButton ID="rbIndividual" runat="server" GroupName="i"
                                        Text="Discount Structure" AutoPostBack="True" OnCheckedChanged="rbProject_CheckedChanged" Checked="True"></asp:RadioButton>
                                </td>
                                <td>
                                    <asp:RadioButton ID="rbProject" runat="server" GroupName="i" OnCheckedChanged="rbProject_CheckedChanged"
                                        Text="Special Rate" AutoPostBack="True"></asp:RadioButton></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class ="profilehead" colspan ="4" style ="text-align:left">Quotation Items</td>
                </tr>
                <tr>
                    <td colspan ="4">
                        <asp:GridView ID="gvQuot" runat="server" AutoGenerateColumns="False"  ShowFooter="True" Width="100%">
                            <FooterStyle BackColor="SkyBlue" />
                            <Columns>
                                <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                                <asp:BoundField DataField="ItemName" HeaderText="Item Name">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="UOM" HeaderText="UOM">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDetQty" runat="server" OnTextChanged="txtDetQty_TextChanged" Text='<%# Bind("Quantity") %>' AutoPostBack="true"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="Currency" HeaderText="Currency"></asp:BoundField>
                                <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDetRate" runat="server"  Text='<%# Bind("Rate") %>' OnTextChanged="txtDetRate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="Rate" HeaderText="Rate">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                </asp:BoundField>--%>
                                <asp:BoundField HeaderText="Amount">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Discount" NullDisplayText="-" HeaderText="Disc %"></asp:BoundField>
                                <asp:BoundField DataField="SpPrice" NullDisplayText="-" DataFormatString="{0:0.00}" HeaderText="Special Price">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField ="GSTperc" HeaderText ="GST(%)" > <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="GSTRate" HeaderText="GST Rate"> <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Room" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDetRoom" runat="server" Width ="100%"  Text='<%# Bind("Room") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="Room" HeaderText="Room"></asp:BoundField>--%>
                                <asp:BoundField DataField="CurrencyId" HeaderText="Currency Id"></asp:BoundField>
                                <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                                <asp:BoundField DataField="ColorId" HeaderText="ColorId"></asp:BoundField>
                                <asp:BoundField DataField="OptId" HeaderText="Optional Id"></asp:BoundField>
                                <asp:BoundField DataField="ItemType" HeaderText="Item Type"></asp:BoundField>
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
                                <asp:TemplateField HeaderText="Floor" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDetFloor" runat="server"  Text='<%# Bind("Floor") %>' ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="Floor" HeaderText="Floor"></asp:BoundField>--%>
                                <asp:TemplateField HeaderText="Discount%" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDisc" runat="server" OnTextChanged="txtDisc_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="SrlOrder" HeaderText="SrlOrder"></asp:BoundField>--%>
                                <asp:TemplateField HeaderText="Srl Order" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSrlOrder" runat="server" Text='<%# Bind("SrlOrder") %>'>></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td class="profilehead" colspan="4" style="text-align: left">Add Item details</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <%-- <asp:UpdatePanel runat="server">
                            <ContentTemplate>--%>
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblSearch" runat="server" Text="Search:" Width="84px" Visible="False"></asp:Label></td>
                                <td style="width: 357px; text-align: left">
                                    <asp:TextBox ID="txtSearchModel" runat="server" Visible="False"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                        ServiceMethod="AutoCompleteAjaxRequest"
                                        ServicePath="~/Search.asmx"
                                        MinimumPrefixLength="2"
                                        CompletionInterval="100"
                                        EnableCaching="false"
                                        CompletionSetCount="10"
                                        TargetControlID="txtSearchModel"
                                        FirstRowSelected="false">
                                    </cc1:AutoCompleteExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server"
                                        ControlToValidate="txtSearchModel" ErrorMessage="Please Enter ModelNo For Search"
                                        ValidationGroup="Search" Visible="False">*</asp:RequiredFieldValidator>

                                    <asp:Button ID="btnSearchModelNo"
                                        runat="server" BorderStyle="None" CausesValidation="False" CssClass="gobutton"
                                        EnableTheming="False" OnClick="btnSearchModelNo_Click" Text="Go" ValidationGroup="Search"
                                        Visible="False" />

                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblSearchBrand" runat="server" Text="Search By Brand :" Visible="False"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged"
                                        Visible="False">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 19px;">
                                    <%--<asp:Label ID="Label65" runat="server" Text="Select Model"></asp:Label>--%>
                Select Model
                :
                                </td>
                                <td style="text-align: left; height: 19px; width: 357px;">
                                    <asp:RadioButton ID="rdbAll" runat="server" Text="All" AutoPostBack="True" GroupName="a" OnCheckedChanged="rdbAll_CheckedChanged" Checked="false"></asp:RadioButton>
                                    <asp:RadioButton ID="rdbOnlyfromLead" runat="server" Text="Only from Lead" AutoPostBack="True" GroupName="a" OnCheckedChanged="rdbOnlyfromLead_CheckedChanged" Checked="false"></asp:RadioButton></td>
                                <td style="text-align: right; height: 19px;">
                                    <%--<asp:Label ID="Label4" runat="server" Text="Model No "></asp:Label>--%>
                Model No

                :

                                </td>
                                <td style="text-align: left; height: 19px;">
                                    <asp:DropDownList ID="ddlModelNo" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlModelNo_SelectedIndexChanged">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 42px;">
                                    <asp:Label ID="lblEssentials" runat="server" Text="Essentials :"></asp:Label>

                                </td>
                                <td style="text-align: left; width: 357px; height: 42px;">
                                    <asp:CheckBoxList ID="chklitemcolor" runat="server" CellPadding="0" CellSpacing="0"
                                        RepeatColumns="4" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="chklitemcolor_SelectedIndexChanged">
                                    </asp:CheckBoxList>
                                    <asp:DropDownList ID="ddlEssentials" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEssentials_SelectedIndexChanged">
                                    </asp:DropDownList></td>
                                <td style="text-align: right; height: 42px;">
                                    <%--<asp:Label ID="lblItemName" runat="server" Text="Model Name" Width="95px"></asp:Label>--%>
                Model Name
                :
                                </td>
                                <td style="text-align: left; height: 42px;">
                                    <asp:TextBox ID="txtItemName" runat="server">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <%--<asp:Label ID="Label25" runat="server" Text="Item Category "></asp:Label>--%>
                Item Category
                :
                                </td>
                                <td style="text-align: left; width: 357px;">
                                    <asp:TextBox ID="txtItemCategory" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                                <td style="text-align: right">
                                    <%--<asp:Label ID="Label58" runat="server" Text="Item SubCategory "></asp:Label>--%>
                Item SubCategory
                :
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtItemSubCategory" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label59" runat="server" Text="Color "></asp:Label>
                                    &nbsp;:
                                </td>
                                <td style="text-align: left; width: 357px;">
                                    <asp:TextBox ID="txtColor" runat="server" ReadOnly="True" Visible="False"></asp:TextBox>
                                    <asp:DropDownList ID="ddlColor" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvColor" runat="server" ControlToValidate="ddlColor" InitialValue="0" ErrorMessage="Please Select Item Color" Text="*" ValidationGroup="qi"></asp:RequiredFieldValidator>
                                </td>
                                <td style="text-align: right">
                                    <%--<asp:Label ID="Label60" runat="server" Text="Brand "></asp:Label>--%>
                Brand :
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtBrand" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <%--<asp:Label ID="lblUOM" runat="server" Text="UOM"></asp:Label>--%>
                UOM
                :
                                </td>
                                <td style="text-align: left; width: 357px;">
                                    <asp:TextBox ID="txtItemUOM" runat="server" ReadOnly="True">
                                    </asp:TextBox>
                                    <asp:Label ID="lblItemCode" runat="server" Visible="False"></asp:Label>
                                </td>
                                <td style="text-align: right">
                                    <%--<asp:Label ID="Label61" runat="server" Text="Room "></asp:Label>--%>
                Room
                :
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtRoom" onkeypress="return check(event)" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv5" runat="server" ControlToValidate="txtRoom"
                                        ErrorMessage="Please Enter the Room" ValidationGroup="qi">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: right">
                                    <%--<asp:Label ID="Label22" runat="server" Text="Item Specification"></asp:Label>--%>
                Item Specification
                                </td>
                                <td style="height: 21px; text-align: left">
                                    <asp:TextBox ID="txtItemSpec" runat="server" CssClass="multilinetext" EnableTheming="False"
                                        ReadOnly="True" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                    <asp:Label ID="lblOptId" runat="server" Visible="false"></asp:Label>
                                </td>
                                <td style="text-align: right">Floor :</td>
                                <td>
                                    <asp:TextBox ID="txtFloor" onkeypress="return check(event)" runat="server">-</asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="height: 19px; text-align: right">
                                    <%--<asp:Label ID="lblQuantity" runat="server" Text="Quantity" Width="54px"></asp:Label>--%>
                Quantity
                                </td>
                                <td style="height: 19px; text-align: left; width: 357px;">
                                    <asp:TextBox ID="txtQunatity" runat="server">       </asp:TextBox>
                                    <%--<asp:Label ID="Label26" runat="server" EnableTheming="False" ForeColor="Red"
                    Text="*">
                </asp:Label>--%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQunatity"
                                        ErrorMessage="Please Enter the Quantity" ValidationGroup="qi">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                            ID="ftxteQuantity" runat="server" FilterType="Numbers" TargetControlID="txtQunatity">
                                        </cc1:FilteredTextBoxExtender>
                                </td>
                                <td style="height: 19px; text-align: right">
                                    <%--<asp:Label ID="lblRate" runat="server" Text="Rate" Width="30px"></asp:Label>--%>
                Rate
                :
                                </td>
                                <td style="height: 19px; text-align: left">
                                    <asp:DropDownList ID="ddlRate" runat="server" EnableTheming="False" Width="67px" CssClass="dropdownlist" Enabled="False">
                                    </asp:DropDownList><asp:TextBox ID="txtRate" runat="server" Width="88px" CssClass="textboxqt" EnableTheming="False"></asp:TextBox>
                                    <%--<asp:Label ID="Label27" runat="server" EnableTheming="False" ForeColor="Red"
                    Text="*">
                </asp:Label>--%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRate"
                                        ErrorMessage="Please Enter the Rate" ValidationGroup="qi">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                            ID="ftxteRate" runat="server" TargetControlID="txtRate" ValidChars=".0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>

                            <tr>
                                <td style="text-align: right;">
                                    <%--<asp:Label ID="Label33" runat="server" Text="Discount" Width="54px"></asp:Label>--%>
                Discount
                :
                                </td>
                                <td style="text-align: left; width: 357px;">
                                    <asp:TextBox ID="txtDiscount" runat="server">0</asp:TextBox>&nbsp;
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server"
                                        ControlToValidate="txtDiscount" ErrorMessage="Please Enter the Discount" ValidationGroup="qi">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                            ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtDiscount" ValidChars=".0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                </td>
                                <td style="text-align: right;">
                                    <%--<asp:Label ID="Label56" runat="server" Text="Special Price"></asp:Label>--%>
                Special Price
                :
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtSpPrice" runat="server"></asp:TextBox>&nbsp;
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server"
                                        ControlToValidate="txtSpPrice" ErrorMessage="Please Enter the Special Price"
                                        ValidationGroup="qi">*</asp:RequiredFieldValidator>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2"
                                        runat="server" TargetControlID="txtSpPrice" ValidChars=".0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>


                            <tr>
                                <td style="text-align: right;">GST (%) :
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtGST_Perc" runat="server">0</asp:TextBox>&nbsp;
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server"
                                        ControlToValidate="txtGST_Perc" ErrorMessage="Please Enter the GST" ValidationGroup="qi">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                            ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtGST_Perc" ValidChars=".0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                </td>
                                <td style="text-align: right;">GST Amount :
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtGST_Amt" runat="server" Text="0"></asp:TextBox>&nbsp;
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server"
                                        ControlToValidate="txtGST_Amt" ErrorMessage="Please Enter the GST Tax Amount"
                                        ValidationGroup="qi">*</asp:RequiredFieldValidator>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10"
                                        runat="server" TargetControlID="txtGST_Amt" ValidChars=".0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>



                            <tr>
                                <td style="height: 21px; text-align: right">
                                    <%--<asp:Label ID="Label62" runat="server" Text="Item Image "></asp:Label>--%>
                Item Image
                :
                                </td>
                                <td style="height: 21px; text-align: left; width: 357px;">
                                    <asp:Image ID="Image1" runat="server" Height="85px" ImageUrl="~/Images/noimage400x300.gif"
                                        Width="140px"></asp:Image>

                                </td>
                                <td style="height: 21px; text-align: right">
                                    <%--<asp:Label ID="Label63" runat="server" Text="Technical Drawings " Width="78px"></asp:Label>--%>
                Technical Drawings

                :

                                </td>
                                <td style="height: 21px; text-align: left">
                                    <asp:Image ID="Image2" runat="server" Height="85px" ImageUrl="~/Images/noimage400x300.gif"
                                        Width="140px"></asp:Image></td>
                            </tr>
                        </table>
                        <%--  </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </td>
                </tr>

                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">Remarks :</td>
                    <td>
                        <asp:TextBox ID="txtorgRemarks" onkeypress="return check(event)" runat="server">-</asp:TextBox>
                    </td>
                    <td style="text-align: right">Serial Order No :</td>
                    <td>
                        <asp:TextBox ID="txtSrlOrderNo" runat="server">0</asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td style="height: 26px; text-align: right"></td>
                    <td style="height: 26px; text-align: right; width: 357px;">
                        <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                            CssClass="loginbutton" EnableTheming="False" Text="Add" OnClick="btnAdd_Click"
                            ValidationGroup="qi" />
                    </td>
                    <td style="height: 26px; text-align: left">
                        <asp:Button ID="btnItemRefresh" runat="server" BackColor="Transparent" BorderStyle="None"
                            CssClass="loginbutton" EnableTheming="False" Text="Refresh" OnClick="btnItemRefresh_Click"
                            CausesValidation="False" /></td>
                    <td style="height: 26px; text-align: left"></td>
                </tr>
                <tr>
                    <td style="height: 13px; text-align: right"></td>
                    <td style="height: 13px; text-align: left; width: 357px;"></td>
                    <td style="height: 13px; text-align: right"></td>
                    <td style="height: 13px; text-align: left"></td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center">
                        <asp:GridView ID="gvQuotationItems" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvQuotationItems_RowDataBound"
                            OnRowDeleting="gvQuotationItems_RowDeleting" OnRowEditing="gvQuotationItems_RowEditing" ShowFooter="True" Width="100%">
                            <FooterStyle BackColor="SkyBlue" />
                            <Columns>
                                <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                                <asp:BoundField DataField="ItemName" HeaderText="Item Name">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="UOM" HeaderText="UOM">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDetQty" runat="server" OnTextChanged="txtDetQty_TextChanged" Text='<%# Bind("Quantity") %>' AutoPostBack="true"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="Currency" HeaderText="Currency"></asp:BoundField>
                                <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDetRate" runat="server"  Text='<%# Bind("Rate") %>' OnTextChanged="txtDetRate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="Rate" HeaderText="Rate">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                </asp:BoundField>--%>
                                <asp:BoundField HeaderText="Amount">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Discount" NullDisplayText="-" HeaderText="Disc %"></asp:BoundField>
                                <asp:BoundField DataField="SpPrice" NullDisplayText="-" DataFormatString="{0:0.00}" HeaderText="Special Price">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField ="GSTperc" HeaderText ="GST(%)" > <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="GSTRate" HeaderText="GST Rate"> <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Room" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDetRoom" runat="server" Width ="100%"  Text='<%# Bind("Room") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="Room" HeaderText="Room"></asp:BoundField>--%>
                                <asp:BoundField DataField="CurrencyId" HeaderText="Currency Id"></asp:BoundField>
                                <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                                <asp:BoundField DataField="ColorId" HeaderText="ColorId"></asp:BoundField>
                                <asp:BoundField DataField="OptId" HeaderText="Optional Id"></asp:BoundField>
                                <asp:BoundField DataField="ItemType" HeaderText="Item Type"></asp:BoundField>
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
                                <asp:TemplateField HeaderText="Floor" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDetFloor" runat="server"  Text='<%# Bind("Floor") %>' ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="Floor" HeaderText="Floor"></asp:BoundField>--%>
                                <asp:TemplateField HeaderText="Discount%" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDisc" runat="server" OnTextChanged="txtDisc_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="SrlOrder" HeaderText="SrlOrder"></asp:BoundField>--%>
                                <asp:TemplateField HeaderText="Srl Order" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSrlOrder" runat="server" Text='<%# Bind("SrlOrder") %>'>></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <table id="tblgv" runat="server" visible="false">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvSubItems" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvSubItems_RowDeleting" OnRowDataBound="gvSubItems_RowDataBound">
                                        <Columns>
                                            <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                            <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code"></asp:BoundField>
                                            <asp:BoundField DataField="SUBITEM_CODE" HeaderText="SubItem Code"></asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center">
                        <asp:Button ID="btnOptAddItems" runat="server" Text="Add Optional Items" OnClick="btnOptAddItems_Click" /></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table id="tblOptional" runat="server" visible="false" style="width: 100%">
                            <tr>
                                <td class="profilehead" colspan="4" style="text-align: left">Add Optional Item details</td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblOptSearch" runat="server" Text="Search:" Width="84px" Visible="False"></asp:Label></td>
                                <td style="width: 357px; text-align: left">
                                    <asp:TextBox ID="txtOptSearch" runat="server" Visible="False"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                        ServiceMethod="AutoCompleteAjaxRequest"
                                        ServicePath="~/Search.asmx"
                                        MinimumPrefixLength="2"
                                        CompletionInterval="100"
                                        EnableCaching="false"
                                        CompletionSetCount="10"
                                        TargetControlID="txtSearchModel"
                                        FirstRowSelected="false">
                                    </cc1:AutoCompleteExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                        ControlToValidate="txtOptSearch" ErrorMessage="Please Enter ModelNo For Search"
                                        ValidationGroup="Search" Visible="False">*</asp:RequiredFieldValidator><asp:Button ID="btnOptSearch"
                                            runat="server" BorderStyle="None" CausesValidation="False" CssClass="gobutton"
                                            EnableTheming="False" OnClick="btnOptSearch_Click" Text="Go" ValidationGroup="Search" Visible="False" /></td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblOptBrand" runat="server" Text="Search By Brand :" Visible="False"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlOptBrand" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOptBrand_SelectedIndexChanged"
                                        Visible="False">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="auto-style2">
                                    <%--<asp:Label ID="Label31"
                    runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                </asp:Label>--%>Select Model
                :
                                </td>
                                <td style="text-align: left;" class="auto-style3">
                                    <asp:RadioButton ID="rbnOptAll" runat="server" Text="All" AutoPostBack="True" GroupName="a" OnCheckedChanged="rbnOptAll_CheckedChanged" Checked="false"></asp:RadioButton>
                                    <asp:RadioButton ID="rbnOptFromLead" runat="server" Text="Only from Lead" AutoPostBack="True" GroupName="a" OnCheckedChanged="rbnOptFromLead_CheckedChanged" Checked="false" Visible="False"></asp:RadioButton></td>
                                <td style="text-align: right;" class="auto-style2">
                                    <%--<asp:Label ID="Label13" runat="server" Text="Insurance"></asp:Label>--%>Model No

                :

                                </td>
                                <td style="text-align: left;" class="auto-style2">
                                    <asp:DropDownList ID="ddlOptModelNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOptModelNo_SelectedIndexChanged">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 42px;">
                                    <asp:Label ID="Label4" runat="server" Text="Essentials :"></asp:Label>

                                </td>
                                <td style="text-align: left; width: 357px; height: 42px;">
                                    <asp:CheckBoxList ID="chkOptItemColor" runat="server" CellPadding="0" CellSpacing="0"
                                        RepeatColumns="4" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="chkOptItemColor_SelectedIndexChanged">
                                    </asp:CheckBoxList>
                                    <asp:DropDownList ID="ddlOptEssentials" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOptEssentials_SelectedIndexChanged">
                                    </asp:DropDownList></td>
                                <td style="text-align: right; height: 42px;">
                                    <%--<asp:Label ID="Label32"
                    runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                </asp:Label>--%>Model Name
                :
                                </td>
                                <td style="text-align: left; height: 42px;">
                                    <asp:TextBox ID="txtOptItemName" runat="server">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <%--<asp:Label ID="lblPercent" runat="server" EnableTheming="True" Text="%"></asp:Label>--%>Item Category
                :
                                </td>
                                <td style="text-align: left; width: 357px;">
                                    <asp:TextBox ID="txtOptItemCat" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                                <td style="text-align: right">
                                    <%--<asp:Label ID="Label15" runat="server" Text="validity"></asp:Label>--%>Item SubCategory
                :
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtOptItemSubCat" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label5" runat="server" Text="Color "></asp:Label>
                                    &nbsp;:
                                </td>
                                <td style="text-align: left; width: 357px;">
                                    <asp:TextBox ID="txtOptColor" runat="server" ReadOnly="True" Visible="False"></asp:TextBox>
                                    <asp:DropDownList ID="ddlOptColor" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlOptColor" InitialValue="0" ErrorMessage="Please Select Item Color" Text="*" ValidationGroup="opt"></asp:RequiredFieldValidator>
                                </td>
                                <td style="text-align: right">
                                    <%--<asp:Label ID="Label9" runat="server" Text="Despatch Mode"></asp:Label>--%>Brand :
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtOptBrand" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <%-- <asp:Label ID="Label39" runat="server" EnableTheming="False" ForeColor="Red"
                    Text="*">
                </asp:Label>--%>UOM
                :
                                </td>
                                <td style="text-align: left; width: 357px;">
                                    <asp:TextBox ID="txtOptItemUOM" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                                <td style="text-align: right">
                                    <%--<asp:Label ID="Label11" runat="server" Text="Transportation Charges"></asp:Label>--%>Room
                :
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtOptRoom" onkeypress="return check(event)" runat="server">-</asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtOptRoom"
                                        ErrorMessage="Please Enter the Room" ValidationGroup="opt">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: right">
                                    <%--<asp:Label ID="Label40"
                    runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                </asp:Label>--%>Item Specification
                                </td>
                                <td colspan="3" style="height: 21px; text-align: left">
                                    <asp:TextBox ID="txtOptItemSpec" runat="server" CssClass="multilinetext" EnableTheming="False"
                                        ReadOnly="True" TextMode="MultiLine" Width="613px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="height: 19px; text-align: right">
                                    <%--<asp:Label ID="Label6" runat="server" Text="Other Details"></asp:Label>--%>Quantity
                                </td>
                                <td style="height: 19px; text-align: left; width: 357px;">
                                    <asp:TextBox ID="txtOptQty" runat="server">       </asp:TextBox>
                                    <%--<asp:Label ID="lblResponsiblePerson" runat="server" Text="Responsible Person" Width="121px"></asp:Label>--%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtOptQty"
                                        ErrorMessage="Please Enter the Quantity" ValidationGroup="opt">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                            ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers" TargetControlID="txtOptQty">
                                        </cc1:FilteredTextBoxExtender>
                                </td>
                                <td style="height: 19px; text-align: right">
                                    <%--<asp:Label ID="Label43" runat="server" EnableTheming="False" ForeColor="Red"
                    Text="*">
                </asp:Label>
                                    --%>Rate
                :
                                </td>
                                <td style="height: 19px; text-align: left">
                                    <asp:DropDownList ID="ddlOptRate" runat="server" EnableTheming="False" Width="67px" CssClass="dropdownlist" Enabled="False">
                                    </asp:DropDownList><asp:TextBox ID="txtOptRate" runat="server" Width="88px" CssClass="textboxqt" EnableTheming="False"></asp:TextBox>
                                    <%--<asp:Label ID="lblFollowupEmail" runat="server" Text="Assigned EmailId"></asp:Label>--%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtOptRate"
                                        ErrorMessage="Please Enter the Rate" ValidationGroup="opt">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                            ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtOptRate" ValidChars=".0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 94px;">
                                    <%--<asp:Label ID="lblSalesPerson" runat="server" Text="Sales Person"></asp:Label>--%>Discount
                :
                                </td>
                                <td style="text-align: left; width: 357px; height: 94px;">
                                    <asp:TextBox ID="txtOptDisc" runat="server">0</asp:TextBox>&nbsp;<%--<asp:Label ID="Label44" runat="server" EnableTheming="False" ForeColor="Red"
                    Text="*">
                </asp:Label>--%><asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server"
                    ControlToValidate="txtOptDisc" ErrorMessage="Please Enter the Discount" ValidationGroup="opt">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                        ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtOptDisc" ValidChars=".0123456789">
                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td style="text-align: right; height: 94px;">
                                    <%--<asp:CheckBox ID="chkIsExpectedOrder" runat="server" Text="Expected Order"></asp:CheckBox>--%>Special Price
                :
                                </td>
                                <td style="text-align: left; height: 94px;">
                                    <asp:TextBox ID="txtOptSpPrice" runat="server"></asp:TextBox>&nbsp;<%--<asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label>--%><asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server"
                                        ControlToValidate="txtOptSpPrice" ErrorMessage="Please Enter the Special Price"
                                        ValidationGroup="opt">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8"
                                            runat="server" TargetControlID="txtOptSpPrice" ValidChars=".0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: right">
                                    <%--<asp:Label ID="Label24" runat="server" Text="Name" Width="76px"></asp:Label>--%>Item Image
                :
                                </td>
                                <td style="height: 21px; text-align: left; width: 357px;">
                                    <asp:Image ID="OptImage3" runat="server" Height="85px" ImageUrl="~/Images/noimage400x300.gif"
                                        Width="140px"></asp:Image>

                                </td>
                                <td style="height: 21px; text-align: right">
                                    <%--<asp:Label ID="Label23" runat="server" Text="Follow Up Description" Width="150px"></asp:Label>--%>Technical Drawings

                :

                                </td>
                                <td style="height: 21px; text-align: left">
                                    <asp:Image ID="OptImage4" runat="server" Height="85px" ImageUrl="~/Images/noimage400x300.gif"
                                        Width="140px"></asp:Image></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: right">Remarks :</td>
                                <td>
                                    <asp:TextBox ID="txtOptRemarks" onkeypress="return check(event)" runat="server">-</asp:TextBox>
                                </td>
                                <td style="text-align: right">Serial Order No :</td>
                                <td>
                                    <asp:TextBox ID="txtOptSrlOrder" runat="server">0</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 26px; text-align: right"></td>
                                <td style="height: 26px; text-align: right; width: 357px;">
                                    <asp:Button ID="btnOptAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                        CssClass="loginbutton" EnableTheming="False" Text="Add" OnClick="btnOptAdd_Click"
                                        ValidationGroup="opt" CausesValidation="false" />
                                </td>
                                <td style="height: 26px; text-align: left">
                                    <asp:Button ID="btnOptRefresh" runat="server" BackColor="Transparent" BorderStyle="None"
                                        CssClass="loginbutton" EnableTheming="False" Text="Refresh" OnClick="btnOptRefresh_Click"
                                        CausesValidation="False" /></td>
                                <td style="height: 26px; text-align: left"></td>
                            </tr>
                            <tr>
                                <td style="height: 13px; text-align: right"></td>
                                <td style="height: 13px; text-align: left; width: 357px;"></td>
                                <td style="height: 13px; text-align: right"></td>
                                <td style="height: 13px; text-align: left"></td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <asp:GridView ID="gvOptQuatationItems" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvOptQuatationItems_RowDataBound"
                                        OnRowDeleting="gvOptQuatationItems_RowDeleting" OnRowEditing="gvOptQuatationItems_RowEditing" ShowFooter="True" Width="100%">
                                        <FooterStyle BackColor="#1AA8BE" />
                                        <Columns>
                                            <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                            <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                                            <asp:BoundField DataField="ItemName" HeaderText="Item Name">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="UOM" HeaderText="UOM">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Currency" HeaderText="Currency"></asp:BoundField>
                                            <asp:BoundField DataField="Rate" HeaderText="Rate">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Amount">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Discount" NullDisplayText="-" HeaderText="Disc %"></asp:BoundField>
                                            <asp:BoundField DataField="SpPrice" NullDisplayText="-" HeaderText="Special Price">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Room" HeaderText="Room"></asp:BoundField>
                                            <asp:BoundField DataField="CurrencyId" HeaderText="Currency Id"></asp:BoundField>
                                            <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                                            <asp:BoundField DataField="ColorId" HeaderText="ColorId"></asp:BoundField>
                                            <asp:BoundField DataField="OptId" HeaderText="Optional Id"></asp:BoundField>
                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
                                            <asp:BoundField DataField="OptSrlOrder" HeaderText="Srl Order"></asp:BoundField>

                                        </Columns>
                                    </asp:GridView>
                                    <asp:GridView ID="gvOptSubItems" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvOptSubItems_RowDeleting" OnRowDataBound="gvOptSubItems_RowDataBound">
                                        <Columns>
                                            <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                            <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code"></asp:BoundField>
                                            <asp:BoundField DataField="SUBITEM_CODE" HeaderText="SubItem Code"></asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>


                        </table>

                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="width: 100%">
                        <table class="stacktable">
                            <tr>
                                <td colspan="4">Summary</td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label60" runat="server" Text="Total :"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txttotal" runat="server"></asp:TextBox>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label61" runat="server" Text="Special Discount :"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtspldiscount" runat="server" Text="0"></asp:TextBox>
                                    <asp:Label ID="Label62" runat="server" Text="%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label63" runat="server" Text="Sub Total :"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtsubtotal" runat="server"></asp:TextBox>
                                    <asp:Label ID="Label64" runat="server" Text="After SPl Discount"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label65" runat="server" Text="GST 18% :"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtsummaryvat" runat="server"></asp:TextBox>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label66" Visible ="false" runat="server" Text="CST 2% :"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtsummaryCst" Visible ="false" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label67" runat="server" Text="GrandToal(GST) :"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtsummaryGtoalvat" runat="server"></asp:TextBox>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label1" runat="server" Visible ="false" Text="GrandToal(CST) :"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtsummaryGtotalcst" Visible ="false" runat="server"></asp:TextBox></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="profilehead" colspan="4" id="tdEMDHeader" runat="server" visible="false">EMD Details</td>
                </tr>
                <tr>
                    <td style="height: 19px; text-align: right"></td>
                    <td style="height: 19px; text-align: left; width: 357px;"></td>
                    <td style="height: 19px; text-align: right"></td>
                    <td style="height: 19px; text-align: left"></td>
                </tr>
                <tr>
                    <td style="text-align: right" id="tdDDNolbl" runat="server" visible="false">
                        <%--<asp:Label ID="lblTechnicalDiscussions" runat="server" Text="Technical Discussions"
                                Width="146px"></asp:Label>--%>DD No
                :
                    </td>
                    <td style="text-align: left; width: 357px;" id="tdDDNoField" runat="server" visible="false">
                        <asp:TextBox ID="txtDDNo" runat="server">
                        </asp:TextBox>*
                <%--<asp:Label ID="Label52" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label>--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                            ControlToValidate="txtDDNo" ErrorMessage="Please Enter the DD No">*</asp:RequiredFieldValidator>
                        <cc1:FilteredTextBoxExtender ID="ftxteDDNo" runat="server" TargetControlID="txtDDNo"
                            ValidChars=".0123456789">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                    <td style="text-align: right" id="tdDDDatelbl" runat="server" visible="false">
                        <%--<asp:Label ID="lblCommercialNegociations" runat="server" Text="Commercial Negotiations"
                                Width="173px"></asp:Label>--%>DD Date

                :

                    </td>
                    <td style="text-align: left" id="tdDDDateField" runat="server" visible="false">
                        <asp:TextBox ID="txtDDDate" runat="server" type="Date" CssClass="datetext" EnableTheming="False">
                        </asp:TextBox>*
                <%--<asp:Label ID="Label53" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label>--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                            ControlToValidate="txtDDDate" ErrorMessage="Please Select  the DD Date">*</asp:RequiredFieldValidator><asp:CustomValidator
                                ID="CustomValidator2" runat="server" ClientValidationFunction="DateCustomValidate"
                                ControlToValidate="txtDDDate" ErrorMessage="Please Enter the DD Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                SetFocusOnError="True">*</asp:CustomValidator><cc1:CalendarExtender Format="dd/MM/yyyy"
                                    ID="ceDDDate" runat="server" PopupButtonID="imgDDDate" TargetControlID="txtDDDate">
                                </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="meeDDDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                            MaskType="Date" TargetControlID="txtDDDate" UserDateFormat="MonthDayYear">
                        </cc1:MaskedEditExtender>
                    </td>
                </tr>
                <tr>
                    <td style="height: 19px; text-align: right" id="tdBankNamelbl" runat="server" visible="false">
                        <%--<asp:Label ID="lblCompetatorsExistance" runat="server" Text="Competators Existance"
                                Width="155px"></asp:Label>--%>Bank Name

                :

                    </td>
                    <td colspan="3" style="height: 19px; text-align: left" id="tdBankNameField" runat="server"
                        visible="false">&nbsp;<asp:TextBox ID="txtBankName" runat="server" EnableTheming="False" Width="521px"
                            CssClass="textbox"></asp:TextBox>*
                <%-- <asp:Label ID="Label54" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label>--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9"
                            runat="server" ControlToValidate="txtBankName" ErrorMessage="Please Enter the Bank Name">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="text-align: right" id="tdEMDChargeslbl" runat="server" visible="false">
                        <asp:Label ID="lbl" runat="server" Text="EMD Charges"></asp:Label>&nbsp;:</td>
                    <td style="text-align: left; width: 357px;" id="tdEMDChargesField" runat="server"
                        visible="false">
                        <asp:TextBox ID="txtEMDCharges" runat="server" ReadOnly="True">
                        </asp:TextBox></td>
                    <td style="text-align: right" id="tdInfavourOflbl" runat="server" visible="false">
                        <%--<asp:Label ID="lblRemarks" runat="server" Text="Remarks" Width="62px"></asp:Label>--%>In Favour Of :

                    </td>
                    <td style="text-align: left" id="tdInfavourofField" runat="server"
                        visible="false">
                        <asp:TextBox ID="txtInFavourofEMD" runat="server" ReadOnly="True">
                        </asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right; height: 19px;"></td>
                    <td style="text-align: left; width: 357px; height: 19px;"></td>
                    <td style="text-align: right; height: 19px;"></td>
                    <td style="text-align: left; height: 19px;"></td>
                </tr>
                <tr>
                    <td class="profilehead" colspan="4" style="height: 19px; text-align: left">Terms &amp; Conditions</td>
                </tr>
                <tr>
                    <td style="height: 19px; text-align: right"></td>
                    <td style="height: 19px; text-align: left; width: 357px;"></td>
                    <td style="height: 19px; text-align: right"></td>
                    <td style="height: 19px; text-align: left"></td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <%--                            <asp:Label ID="lblRemarksMandatory"
                                    runat="server" EnableTheming="False" ForeColor="Red" Text="*"></asp:Label>--%>Delivery
                :
                    </td>
                    <td style="text-align: left; width: 357px;">
                        <asp:TextBox ID="txtDelivery" runat="server" TextMode="MultiLine"></asp:TextBox>&nbsp;<%--<asp:Label ID="lblExpectedFwpDate" runat="server" Text="Order Expected Date" Width="137px"></asp:Label>--%><asp:RequiredFieldValidator ID="rfvDelivery" runat="server" ControlToValidate="txtDelivery"
                            ErrorMessage="Please Enter the Delivery" ValidationGroup="a">*</asp:RequiredFieldValidator>&nbsp;
                    </td>
                    <td style="text-align: right;">
                        <%--<asp:Label
                                ID="lblExpectedDateMandatory" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label>--%>Payment Terms
                :
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtPaymentTerms" runat="server" TextMode="MultiLine"></asp:TextBox>&nbsp;<%-- <cc1:CalendarExtender Format="dd/MM/yyyy"
                                ID="CalendarExtender1" runat="server" PopupButtonID="Image3" TargetControlID="txtExpectedFwpDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtExpectedFwpDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%><asp:RequiredFieldValidator ID="rfvPayTerms" runat="server" ControlToValidate="txtPaymentTerms"
                                ErrorMessage="Please Enter the Payment Terms" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <%--<asp:Label ID="lblTechnicalDiscussions" runat="server" Text="Technical Discussions"
                                Width="146px"></asp:Label>--%>Packing Charges :
                    </td>
                    <td style="text-align: left; width: 357px;">
                        <asp:TextBox ID="txtPackingCharges" runat="server">0</asp:TextBox>&nbsp;<%--<asp:Label ID="Label52" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label>--%><asp:RequiredFieldValidator ID="rfvPackingChrgs" runat="server" ControlToValidate="txtPackingCharges"
                                    ErrorMessage="Please Enter the Packing Charges" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                    <td style="text-align: right" hidden="hidden">
                        <%--<asp:Label ID="lblCommercialNegociations" runat="server" Text="Commercial Negotiations"
                                Width="173px"></asp:Label>--%>Guarantee
                :
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtGuarantee" runat="server" Visible="False">0</asp:TextBox>&nbsp;<%--<asp:Label ID="Label53" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label>--%><asp:RequiredFieldValidator ID="rfvGuarantee" runat="server" ControlToValidate="txtGuarantee"
                                    ErrorMessage="Please Enter the Guarantee" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="text-align: right"></td>
                    <td style="text-align: left; width: 357px;">
                        <asp:RadioButton ID="rbVAT" runat="server" Checked="True" GroupName="vatcst" Text="CGST/SGST" />
                        <asp:RadioButton
                            ID="rbCST" runat="server" GroupName="vatcst" Text="IGST" /><asp:RadioButton
                                ID="rbincluding" runat="server" GroupName="vatcst" Text="Including GST" /></td>
                    <td align="right" hidden="hidden">
                        <%--<asp:Label ID="lblCompetatorsExistance" runat="server" Text="Competators Existance"
                                Width="155px"></asp:Label>--%>Insurance
                :
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtInsurance" runat="server" Visible="False">0</asp:TextBox>
                        <%-- <asp:Label ID="Label54" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label>--%>&nbsp;<asp:RequiredFieldValidator ID="rfvInsurance" runat="server" ControlToValidate="txtInsurance"
                                    ErrorMessage="Please Enter the Insurance" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblVATCST" runat="server" Text="TAX :"></asp:Label>

                    </td>
                    <td style="text-align: left; width: 357px;">
                        <asp:TextBox ID="txtVAT" runat="server">
                        </asp:TextBox>%
                <%--<asp:Label ID="lblRemarks" runat="server" Text="Remarks" Width="62px"></asp:Label>--%>
                        <cc1:FilteredTextBoxExtender ID="ftxteVAT" runat="server" TargetControlID="txtVAT"
                            ValidChars=".0123456789">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                    <td style="text-align: right">
                        <%--                            <asp:Label ID="lblRemarksMandatory"
                                    runat="server" EnableTheming="False" ForeColor="Red" Text="*"></asp:Label>--%>Validity

                :

                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtValidity" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <%--<asp:Label ID="lblExpectedFwpDate" runat="server" Text="Order Expected Date" Width="137px"></asp:Label>--%>&nbsp;</td>
                    <td style="text-align: left; width: 357px;">
                        <asp:DropDownList ID="ddlDespatchMode" runat="server" Visible="False">
                        </asp:DropDownList>&nbsp;<%--<asp:Label
                                ID="lblExpectedDateMandatory" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label>--%><asp:RequiredFieldValidator ID="rfvDespatchMode" runat="server" ControlToValidate="ddlDespatchMode"
                                    ErrorMessage="Please Select the Despatch Mode" InitialValue="0" ValidationGroup="a">*</asp:RequiredFieldValidator>&nbsp;</td>
                    <td style="text-align: right">
                        <%-- <cc1:CalendarExtender Format="dd/MM/yyyy"
                                ID="CalendarExtender1" runat="server" PopupButtonID="Image3" TargetControlID="txtExpectedFwpDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtExpectedFwpDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>Transportation Charges
                :
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtTransCharges" runat="server" TextMode="MultiLine"></asp:TextBox>&nbsp;<%--<asp:Label ID="lblTechnicalDiscussions" runat="server" Text="Technical Discussions"
                                Width="146px"></asp:Label>--%><asp:RequiredFieldValidator ID="rfvTransCharges" runat="server" ControlToValidate="txtTransCharges"
                                    ErrorMessage="Please Enter the Transportation Charges" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <%--<asp:Label ID="Label52" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label>--%>Other Details
                :
                    </td>
                    <td style="text-align: left" colspan="3">
                        <asp:TextBox ID="txtOtherSpecs" runat="server" CssClass="multilinetext" EnableTheming="False"
                            TextMode="MultiLine" Width="613px">-</asp:TextBox>&nbsp;&nbsp;&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblFOB" runat="server" Text="FOB :"></asp:Label>

                    </td>
                    <td style="width: 357px; text-align: left">
                        <asp:TextBox ID="txtFOB" runat="server">
                        </asp:TextBox><cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtFOB"
                            ValidChars=".0123456789">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                    <td style="text-align: right">
                        <asp:Label ID="lblCIF" runat="server" Text="CIF :"></asp:Label>

                    </td>
                    <td style="text-align: left">
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtCIF"
                            ValidChars=".0123456789">
                        </cc1:FilteredTextBoxExtender>
                        <asp:TextBox ID="txtCIF" runat="server">
                        </asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: left" class="profilehead">Follow Up Details</td>
                </tr>
                <tr>
                    <td style="height: 19px; text-align: right"></td>
                    <td style="height: 19px; text-align: left; width: 357px;"></td>
                    <td style="height: 19px; text-align: right"></td>
                    <td style="height: 19px; text-align: left"></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <%--<asp:Label ID="lblCommercialNegociations" runat="server" Text="Commercial Negotiations"
                                Width="173px"></asp:Label>--%>Responsible Person
                :
                    </td>
                    <td style="text-align: left; width: 357px;">
                        <asp:DropDownList ID="ddlResponsiblePerson" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlResponsiblePerson_SelectedIndexChanged">
                        </asp:DropDownList>&nbsp;<%--<asp:Label ID="Label53" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label>--%><asp:RequiredFieldValidator ID="rfvResPerson" runat="server" ControlToValidate="ddlResponsiblePerson"
                                    ErrorMessage="Please Select the Responsible Person" InitialValue="0" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                    <td style="text-align: right">
                        <%--<asp:Label ID="lblCompetatorsExistance" runat="server" Text="Competators Existance"
                                Width="155px"></asp:Label>--%>Assigned EmailId
                :
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtFollowupEmail" runat="server" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; height: 22px;">
                        <%-- <asp:Label ID="Label54" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label>--%>Sales Person
                :
                    </td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddlSalesPerson" runat="server">
                        </asp:DropDownList>&nbsp;<%--<asp:Label ID="lblRemarks" runat="server" Text="Remarks" Width="62px"></asp:Label>--%><asp:RequiredFieldValidator ID="rfvSalesPerson" runat="server" ControlToValidate="ddlSalesPerson"
                            ErrorMessage="Please Select the Sales Person" InitialValue="0" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                    <td style="text-align: right; height: 22px;">
                        <asp:Label ID="Label49" runat="server" Text="Assigned PhoneNo :" Width="121px"></asp:Label></td>
                    <td style="text-align: left; height: 22px;">
                        <asp:TextBox ID="txtFollowupPhoneNo" runat="server" ReadOnly="True">
                        </asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right"></td>
                    <td style="text-align: left; width: 357px;"></td>
                    <td style="text-align: right"></td>
                    <td style="text-align: left"></td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: left" class="auto-style5">Other Details</td>
                </tr>
                <tr>
                    <td style="text-align: right"></td>
                    <td style="text-align: left; width: 357px;"></td>
                    <td style="text-align: right"></td>
                    <td style="text-align: left"></td>
                </tr>
                <tr>
                    <td style="text-align: right"></td>
                    <td style="text-align: left; width: 357px;">
                        <%--                            <asp:Label ID="lblRemarksMandatory"
                                    runat="server" EnableTheming="False" ForeColor="Red" Text="*"></asp:Label>--%>
                        <asp:CheckBox ID="chkIsExpectedOrder" runat="server" Text="Expected Order" />
                    </td>
                    <td style="text-align: right"></td>
                    <td style="text-align: left"></td>
                </tr>
                <tr>
                    <td class="profilehead" colspan="4" style="text-align: left">Reference Details</td>
                </tr>
                <tr>
                    <td style="height: 19px; text-align: right"></td>
                    <td style="height: 19px; text-align: left; width: 357px;"></td>
                    <td style="height: 19px; text-align: right"></td>
                    <td style="height: 19px; text-align: left"></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <%--<asp:Label ID="lblExpectedFwpDate" runat="server" Text="Order Expected Date" Width="137px"></asp:Label>--%>Prepared By
                :
                    </td>
                    <td style="text-align: left; width: 357px;">
                        <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                        </asp:DropDownList></td>
                    <td style="text-align: right">
                        <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By :"></asp:Label>

                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlApprovedBy" runat="server" Enabled="False">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblCheckedBy" runat="server" Text="Checked By :" Visible="False"></asp:Label></td>
                    <td style="text-align: left; width: 357px;">
                        <asp:DropDownList ID="ddlCheckedBy" runat="server" Enabled="False" Visible="False">
                        </asp:DropDownList></td>
                    <td style="text-align: right"></td>
                    <td style="text-align: left"></td>
                </tr>
                <tr>
                    <td style="height: 19px"></td>
                    <td style="height: 19px; width: 357px;"></td>
                    <td style="height: 19px"></td>
                    <td style="height: 19px"></td>
                </tr>
                <tr>
                    <td colspan="4" style="height: 49px">
                        <table id="tblButtons" border="0" cellpadding="0" cellspacing="0" align="center">
                            <tr>
                                <td style="height: 24px">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Width="100px" ValidationGroup="a" /></td>
                                <td style="height: 24px">
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CausesValidation="False" Width="100px" Visible="False" /></td>
                                <td style="height: 24px">
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"
                                        CausesValidation="False" Width="100px" /></td>
                                <td style="height: 24px">
                                    <asp:Button ID="btnFollowUp" runat="server" Text="Follow Up" CausesValidation="False"
                                        OnClick="btnFollowUp_Click" Width="100px" /></td>
                                <td style="height: 24px"></td>
                                <td style="height: 24px">
                                    <asp:Button ID="btnRevise" runat="server" Text="Revise" OnClick="btnRevise_Click"
                                        CausesValidation="False" Width="100px" /></td>
                                <td style="height: 24px">
                                    <asp:Button ID="btnApprove" runat="server" OnClick="Button1_Click" Text="Approve" Width="100px" /></td>
                                <td style="height: 24px">
                                    <asp:Button ID="btnRegret" runat="server" Text="Obsolete" OnClick="btnRegret_Click" Width="100px" /></td>
                                <td style="height: 24px">
                                    <asp:Button ID="btnSalesOrder" runat="server" Text="Purchase Order" OnClick="btnSalesOrder_Click"
                                        CausesValidation="False" Width="120px" /></td>
                                <td style="height: 24px">
                                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click"
                                        CausesValidation="False" Width="100px" /></td>
                                <td style="height: 24px">
                                    <asp:Button ID="btnSend" runat="server" Text="Send E-Mail" OnClick="btnSend_Click"
                                        CausesValidation="False" Width="100px" Visible="False" /></td>
                                <td style="height: 24px">
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" CausesValidation="False" Width="100px" /></td>
                                <td style="height: 24px">
                                    <asp:Button ID="btnPrintTB" runat="server" Text="Print W/O Room" OnClick="btnPrintTB_Click" CausesValidation="False" Visible="False" Width="100px" /></td>
                                <td style="height: 24px">
                                    <asp:Button ID="btnPrintCB" runat="server" Text="Print C.B." OnClick="btnPrintCB_Click" CausesValidation="False"  Width="100px" /></td>
                                <td style="height: 24px">
                                    <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" Width="100px" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                
                <tr>
                    <td colspan="4">
                        <table id="tblPrint" runat="server" border="0" cellpadding="0" cellspacing="0" visible="false" width="100%">
                            <tr>
                                <td colspan="3">
                                    <table align="center">
                                        <tr>
                                            <td class="profilehead" colspan="4" style="text-align: left">Special Price With Model Number 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 22px">
                                                <asp:RadioButton ID="rdbProject" runat="server" AutoPostBack="True" GroupName="a"
                                                    OnCheckedChanged="rdbProject_CheckedChanged" Text="Project Price" Width="102px"></asp:RadioButton></td>
                                            <td style="height: 22px">
                                                <asp:RadioButton ID="rdbPrDrawings" runat="server" AutoPostBack="True" GroupName="a"
                                                    OnCheckedChanged="rdbPrDrawings_CheckedChanged" Text="Project Price With Drawings"></asp:RadioButton></td>
                                            <td style="height: 22px">
                                                <asp:RadioButton ID="rdbSpecialPrice" runat="server" AutoPostBack="True" GroupName="a"
                                                    OnCheckedChanged="RadioButton3_CheckedChanged" Text="Special Price"></asp:RadioButton></td>
                                            <td style="height: 22px">
                                                <asp:RadioButton ID="rdbSpWithDrawings" runat="server" AutoPostBack="True" GroupName="a"
                                                    OnCheckedChanged="rdbSpWithDrawings_CheckedChanged" Text="Special Price With Drawings"></asp:RadioButton></td>
                                        </tr>
                                        <tr>
                                            <td class="profilehead" colspan="4" style="text-align: left">With Out Model Number</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton ID="rdbProjectpriceMn" runat="server" AutoPostBack="True" GroupName="a"
                                                    OnCheckedChanged="rdbProjectpriceMn_CheckedChanged" Text="Project Price" Width="102px"></asp:RadioButton></td>
                                            <td>
                                                <asp:RadioButton ID="rdbProjectdrawingsandmn" runat="server" AutoPostBack="True" GroupName="a"
                                                    OnCheckedChanged="rdbProjectdrawingsandmn_CheckedChanged" Text="Project Price With Drawings"></asp:RadioButton></td>
                                            <td>
                                                <asp:RadioButton ID="rdbspecialpricemn" runat="server" AutoPostBack="True" GroupName="a"
                                                    OnCheckedChanged="rdbspecialpricemn_CheckedChanged" Text="Special Price"></asp:RadioButton></td>
                                            <td>
                                                <asp:RadioButton ID="rdbspwdmn" runat="server" AutoPostBack="True" GroupName="a"
                                                    OnCheckedChanged="rdbspwdmn_CheckedChanged" Text="Special Price With Drawings"></asp:RadioButton></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table id="tblWoroomprint" runat="server" visible="false" width="100%" align="center">
                            <tr>
                                <td class ="profilehead " colspan ="4">with out Room Wise</td>
                                <td class="profilehead">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style4">
                                    <asp:RadioButton ID="rdbwithPrice" runat ="server" AutoPostBack ="true" GroupName ="a" 
                                       OnCheckedChanged ="rdbwithPrice_CheckedChanged" Text="With Price" />
                                </td>
                                <td>
                                    <asp:RadioButton ID="rdbWPSP" runat ="server" AutoPostBack ="true" GroupName ="a"
                                        OnCheckedChanged ="rdbWPSP_CheckedChanged" Text="With Price & Spl Price" />
                                </td>
                            </tr>

                        </table>
                        <table id="tblDiscountSelect" runat="server" visible="false" width="100%" align="center">
                            <tr>
                                <td class="profilehead" colspan="4">Room Wise</td>
                                <td class="profilehead">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style4">

                                <asp:RadioButton ID="rdbPert" runat="server" AutoPostBack="True" GroupName="a"
                                        OnCheckedChanged="rdbPert_CheckedChanged" Text="Pert Print" Width="200px"></asp:RadioButton></td>
                            </tr>
                            <tr>
                                <td class="auto-style4">
                                    <asp:RadioButton ID="rdbDiscountPrice" runat="server" AutoPostBack="True" GroupName="a"
                                        OnCheckedChanged="rdbDiscountPrice_CheckedChanged" Text="MRP + Spl Price + Unit Price" Width="200px"></asp:RadioButton></td>
                                <td>
                                    <asp:RadioButton ID="rdbDiscountWithoutDrawings" runat="server" AutoPostBack="True"
                                        GroupName="a" OnCheckedChanged="rdbDiscountWithoutDrawings_CheckedChanged" Text="MRP + Spl Price + Without  Unit Price"></asp:RadioButton></td>
                                <td style="text-align: left">
                                    <asp:RadioButton ID="rdbWithoutRatesDiscount" runat="server" AutoPostBack="True"
                                        GroupName="a" OnCheckedChanged="rdbWithoutRatesDiscount_CheckedChanged" Text="MRP + Without Spl Price + Unit Price" Width="250px"></asp:RadioButton></td>
                                <td style="text-align: left">
                                    <asp:RadioButton ID="rdbWithoutImagesDr" runat="server" AutoPostBack="True"
                                        GroupName="a" OnCheckedChanged="rdbWithoutImagesDr_CheckedChanged" Text="With Drawings" Visible="False"></asp:RadioButton>&nbsp;<asp:RadioButton ID="rdbCOmpare" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="rdbCOmpare_CheckedChanged" Text="Compare" />
                                </td>
                                <td style="text-align: left">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="profilehead" colspan="4">With out Roomwise</td>
                                <td class="profilehead">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style4">
                                    <asp:RadioButton ID="rdbWithoutModelnoandDrwings" runat="server" AutoPostBack="True"
                                        GroupName="a" OnCheckedChanged="rdbWithoutModelnoandDrwings_CheckedChanged" Text="with Code + Mrp"></asp:RadioButton></td>
                                <td>
                                    <asp:RadioButton ID="rdbWithoutratesandMn" runat="server" AutoPostBack="True"
                                        GroupName="a" OnCheckedChanged="rdbWithoutratesandMn_CheckedChanged" Text="Without Code + MRP"></asp:RadioButton></td>
                                <td style="text-align: left">
                                    <asp:RadioButton ID="rdbWithoutDrawingsmn" runat="server" AutoPostBack="True"
                                        GroupName="a" OnCheckedChanged="rdbWithoutDrawingsmn_CheckedChanged" Text="Without Code + Mrp + Spl Price"></asp:RadioButton>
                                </td>
                                <td style="text-align: left">
                                    <asp:RadioButton ID="rdbQC2" runat="server" AutoPostBack="True"
                                        GroupName="a" OnCheckedChanged="rdbQC2_CheckedChanged" Text="WithoutCode+WithoutUnitprice+Spl Price"></asp:RadioButton>
                                    <td style="text-align: left">&nbsp;</td>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style4">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td style="text-align: left">
                                    <asp:RadioButton ID="rdbqt5" runat="server" AutoPostBack="True"
                                        GroupName="a" OnCheckedChanged="rdbqt5_CheckedChanged" Text="With Codes + Spl Price + MRP"></asp:RadioButton>
                                </td>

                                <td style="text-align: left">
                                    <asp:RadioButton ID="rdbTechDrawings" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="rdbTechDrawings_CheckedChanged" Text="With Tech Drawings" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="profilehead" colspan="4">Generate e-Quotation</td>
                                <td class="profilehead">&nbsp;</td>

                            </tr>
                            <tr>
                                <td class="auto-style4">
                                    <asp:RadioButton ID="rbEquotation" runat="server" AutoPostBack="True"
                                        GroupName="a" OnCheckedChanged="rbEquotation_CheckedChanged" Text="e-Quotation"></asp:RadioButton></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan ="4">
                        <table id="tblprintColumns" visible ="false"  runat ="server" width="100%">
                            <tr>
                                <td colspan="4">
                                    <table id="Table1" runat="server" >
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkwp" runat="server" OnCheckedChanged="chkwp_CheckedChanged"
                                                    Text="Unit Price" AutoPostBack="True"></asp:CheckBox></td>
                                            <td><asp:CheckBox ID="chkmrptotal" runat="server" OnCheckedChanged="chkmrptotal_CheckedChanged"
                                                    Text="MRP Total" AutoPostBack="True"></asp:CheckBox></td>
                                            <td>
                                                <asp:CheckBox ID="chkwpsp" runat="server" Text="Spl Price" AutoPostBack="True" OnCheckedChanged="chkwpsp_CheckedChanged"></asp:CheckBox></td>
                                            <td>
                                                <asp:CheckBox ID="chkwpspta" runat="server" Text="Total Price" AutoPostBack="True" OnCheckedChanged ="chkwpspta_CheckedChanged"></asp:CheckBox></td>
                                            <td><asp:CheckBox ID="chkGST" runat="server" OnCheckedChanged="chkGST_CheckedChanged"
                                                    Text="GST Total" AutoPostBack="True"></asp:CheckBox></td>
                                            <td><asp:CheckBox ID="chk3gst" runat ="server" Text="Total Amount (Footer)" AutoPostBack ="true" OnCheckedChanged ="chk3gst_CheckedChanged" /></td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chk3wc" runat="server" OnCheckedChanged="chk3wc_CheckedChanged"
                                                    Text="Add GST (Footer)" AutoPostBack="True"></asp:CheckBox></td>
                                            <td>
                                                <asp:CheckBox ID="chkwt" runat="server" Text="Grand Total (Footer)" AutoPostBack="True" OnCheckedChanged="chkwpsp_CheckedChanged"></asp:CheckBox></td>
                                            <td>
                                                <asp:CheckBox ID="chkWoPrices" runat="server" Text="Codes" AutoPostBack="True" OnCheckedChanged ="chkWoPrices_CheckedChanged"></asp:CheckBox></td>
                                            <td><asp:CheckBox ID="chkwsp" runat ="server" Text="Techinical Drawings" AutoPostBack ="true" OnCheckedChanged ="chkwsp_CheckedChanged" /></td>
                                             <td><asp:CheckBox ID="chkterms" runat ="server" Text="Terms & Conditions" AutoPostBack ="true" OnCheckedChanged ="chkterms_CheckedChanged" /></td>

                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvitemsgrid" runat ="server" ShowFooter="true" EnableTheming ="false"   OnRowDataBound ="gvitemsgrid_RowDataBound" DataSourceID="SqlDataSource3" AutoGenerateColumns ="false" >
                                        <HeaderStyle BackColor ="White"  />
                                        <%--<FooterStyle ForeColor ="Red" />--%>
                                        <Columns >
                                            <asp:BoundField DataField ="Quot_OrderNo" HeaderText ="S.No" />
                                            <asp:BoundField DataField ="PRODUCT_COMPANY_NAME" HeaderText ="Brand"/>
                                            <asp:BoundField DataField ="ITEM_NAME" HeaderText ="Model"  />
                                            <asp:BoundField DataField ="ITEM_MODEL_NO" HeaderText ="Codes" />
                                            <asp:BoundField DataField ="ITEM_SPEC" HeaderText ="Description"   />
                                            <asp:BoundField DataField ="COLOUR_NAME" HeaderText ="Colour"  />
                                            <asp:BoundField DataField ="QUOT_DET_QTY" HeaderText ="Qty"  />
                                            <asp:BoundField DataField="QUOT_RATE" HeaderText ="Unit Price" />
                                            <asp:BoundField DataField ="QUOT_DISC" HeaderText ="Disc%"  />
                                            <asp:BoundField HeaderText ="Spl Price"   />
                                            <asp:BoundField DataField ="QUOT_SPPRICE" HeaderText ="TotalPrice"  />
                                            <asp:BoundField HeaderText ="Total Price" />
                                            <asp:BoundField DataField ="QUOT_DET_GSTRATE" HeaderText ="GST"  />
                                            <asp:BoundField DataField ="QUOT_FLOOR" HeaderText ="Floor"  />
                                            <asp:BoundField DataField ="QUOT_ROOM" HeaderText ="Room"  />
                                            <asp:TemplateField HeaderText="Image"   >
                            <ItemTemplate>
                                <asp:Label ID="lblPath" Visible="false" Text='<%#Eval("Item_Image") %>' runat="server" />
                                <asp:Image ID="Image1" runat="server" EnableTheming="False" Height="100px" ImageUrl = '<%# Eval("Item_Path") %>'
                                    Width="100px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Image"  HeaderStyle-Width="13%"  >
                                                <ItemTemplate>
                                                    <asp:Image runat ="server" EnableTheming="False" Height="100px" ImageUrl = '<%# Eval("Item_Image","http://183.82.108.55/Content/Images/{0}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                 SelectCommand="SP_PRINT_QUOT" SelectCommandType="StoredProcedure">
                                        <SelectParameters >
                                            <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="QuotId" ControlID="lblQuotIdHiddenForFollowUp"></asp:ControlParameter>
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <asp:GridView ID="gvterms"  Visible ="False" EnableTheming="False"   runat ="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" OnRowDataBound ="gvterms_RowDataBound"  >
                                        <Columns>
                                            <asp:BoundField DataField="Message" HeaderText="Message" SortExpression="Message" />
                                            <%--<asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />--%>
                                        </Columns>
                                    </asp:GridView>

                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [Message] FROM [Terms_Conditions] WHERE ([Title] LIKE '%' + @Title + '%')">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="Quotation Terms" Name="Title" Type="String" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>

                                    <asp:Label ID="lblItmcode" runat ="server" Visible ="false"   ></asp:Label>
                                    <asp:Label ID="lblGstrate" runat ="server" Visible ="false"  ></asp:Label>
                                    <asp:Label ID="lblFlr" runat ="server" Visible ="false"  ></asp:Label>
                                    <asp:Label ID="lblrt" runat ="server" Visible ="false"  ></asp:Label>
                                    <asp:Label ID="lblrom" runat ="server" Visible ="false"  ></asp:Label>
                                    <asp:Label ID="lblsp" runat ="server" Visible ="false"  ></asp:Label>
                                    <%--<asp:Label ID="Label11" runat ="server" Visible ="false"  ></asp:Label>--%>

                                </td>
                            </tr>
                           
            </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table id="tblFollowUp" runat="server" border="0" cellpadding="0" cellspacing="0"
                            visible="false" width="100%">
                            <tr>
                                <td class="profilehead" colspan="2">Follow Up Details</td>
                            </tr>
                            <tr>
                                <td style="height: 19px; text-align: right"></td>
                                <td style="height: 19px; text-align: left"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <%--<asp:Label
                                ID="lblExpectedDateMandatory" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label>--%>Name
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtFollowUpName" runat="server" ReadOnly="True"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <%-- <cc1:CalendarExtender Format="dd/MM/yyyy"
                                ID="CalendarExtender1" runat="server" PopupButtonID="Image3" TargetControlID="txtExpectedFwpDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtExpectedFwpDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>Follow Up Description
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtFollowUpDesc" runat="server" CssClass="multilinetext" EnableTheming="False"
                                        Height="40px" TextMode="MultiLine" Width="560px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="height: 22px; text-align: right">
                                    <%--<asp:Label ID="lblTechnicalDiscussions" runat="server" Text="Technical Discussions"
                                Width="146px"></asp:Label>--%>
                            Technical Discussions
                                </td>
                                <td style="height: 22px; text-align: left">
                                    <asp:DropDownList ID="ddlTechnicalDiscussions" runat="server" AutoPostBack="True">
                                        <asp:ListItem>--</asp:ListItem>
                                        <asp:ListItem>Open</asp:ListItem>
                                        <asp:ListItem>Closed</asp:ListItem>
                                    </asp:DropDownList>*
                            <%--<asp:Label ID="Label52" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label>--%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                                        ControlToValidate="ddlTechnicalDiscussions" ErrorMessage="Please Select Technical Discussions"
                                        InitialValue="--" ValidationGroup="follow">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td style="height: 22px; text-align: right">
                                    <%--<asp:Label ID="lblCommercialNegociations" runat="server" Text="Commercial Negotiations"
                                Width="173px"></asp:Label>--%>
                            Commercial Negotiations
                                </td>
                                <td style="height: 22px; text-align: left">
                                    <asp:DropDownList ID="ddlCommercialNegociations" runat="server" AutoPostBack="True">
                                        <asp:ListItem>--</asp:ListItem>
                                        <asp:ListItem>Open</asp:ListItem>
                                        <asp:ListItem>Closed</asp:ListItem>
                                    </asp:DropDownList>
                                    <%--<asp:Label ID="Label53" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label>--%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"
                                        ControlToValidate="ddlCommercialNegociations" ErrorMessage="Please Select Commerical Negotiations"
                                        InitialValue="--" ValidationGroup="follow">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <%--<asp:Label ID="lblCompetatorsExistance" runat="server" Text="Competators Existance"
                                Width="155px"></asp:Label>--%>
                            Competators Existance
                                </td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlCompetatorsExistance" runat="server" OnSelectedIndexChanged="ddlCompetatorsExistance_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem>--</asp:ListItem>
                                        <asp:ListItem>Exist</asp:ListItem>
                                        <asp:ListItem>Does Not Exist</asp:ListItem>
                                    </asp:DropDownList>*
                           <%-- <asp:Label ID="Label54" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label>--%>

                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right" id="tdRemarkslbl" runat="server" visible="false">
                                    <%--<asp:Label ID="lblRemarks" runat="server" Text="Remarks" Width="62px"></asp:Label>--%>
                            Remarks
                                </td>
                                <td style="text-align: left" id="tdRemarksField" runat="server" visible="false">
                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="multilinetext" EnableTheming="False"
                                        Height="40px" TextMode="MultiLine" Width="560px"></asp:TextBox>*
                            <%--                            <asp:Label ID="lblRemarksMandatory"
                                    runat="server" EnableTheming="False" ForeColor="Red" Text="*"></asp:Label>--%>
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtRemarks" ErrorMessage="Please Enter Remarks"
                                        ValidationGroup="follow">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 35px;" id="tdOrderExpDatelbl" runat="server"
                                    visible="false">
                                    <%--<asp:Label ID="lblExpectedFwpDate" runat="server" Text="Order Expected Date" Width="137px"></asp:Label>--%>
                            Order Expected Date
                                </td>
                                <td style="text-align: left; height: 35px;" id="tdOrderExpDateField" runat="server"
                                    visible="false">
                                    <asp:TextBox ID="txtExpectedFwpDate" runat="server" type="Date" CssClass="datetext" EnableTheming="False"></asp:TextBox>*
                            <%--<asp:Label
                                ID="lblExpectedDateMandatory" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label>--%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                        ControlToValidate="txtExpectedFwpDate" ErrorMessage="Please Enter Order Expected Date"
                                        ValidationGroup="follow">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 21px;"></td>
                                <td style="height: 21px">
                                    <%-- <cc1:CalendarExtender Format="dd/MM/yyyy"
                                ID="CalendarExtender1" runat="server" PopupButtonID="Image3" TargetControlID="txtExpectedFwpDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtExpectedFwpDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table id="Table2" align="center">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnFollowUpSave" runat="server" Text="Save" OnClick="btnFollowUpSave_Click"
                                                    ValidationGroup="follow" /></td>
                                            <td>
                                                <asp:Button ID="btnFollowUpRefresh" runat="server" Text="Refresh" OnClick="btnFollowUpRefresh_Click"
                                                    CausesValidation="False" /></td>
                                            <td>
                                                <asp:Button ID="btnFollowUpHistory" runat="server" Text="History" OnClick="btnFollowUpHistory_Click"
                                                    CausesValidation="False" /></td>
                                            <td>
                                                <asp:Button ID="btnFollowUpClose" runat="server" Text="Close" OnClick="btnFollowUpClose_Click"
                                                    CausesValidation="False" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 9px">
                                    <table id="tblFollowUpHistory" runat="server" border="0" cellpadding="0" cellspacing="0"
                                        width="100%" visible="false" align="center">
                                        <tr>
                                            <td class="profilehead" colspan="3">Follow Up History</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gvFollowUp" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                    DataSourceID="sdsFollowUp" OnRowDataBound="gvFollowUp_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="FU_DATE" SortExpression="FU_DATE" HeaderText="Date"></asp:BoundField>
                                                        <asp:BoundField DataField="EMP_FIRST_NAME" SortExpression="EMP_FIRST_NAME" HeaderText="Name">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FU_DESC" SortExpression="FU_DESC" HeaderText="Description">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FU_TECH_DISCUSSIONS" HeaderText="Tech Diss.">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FU_COMM_NEGOS" HeaderText="Comm. Negos">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FU_COMP_EXISTANCE" HeaderText="Comp. Existance">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FU_REMARKS" SortExpression="FU_REMARKS" HeaderText="Remarks">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="FU_FWP_EXPEXTED_DATE" SortExpression="FU_FWP_EXPEXTED_DATE" HeaderText="Expected Date">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <SelectedRowStyle BackColor="LightSteelBlue" />
                                                </asp:GridView>
                                                <asp:Label ID="lblQuotIdHiddenForFollowUp" runat="server" Visible="False"></asp:Label>
                                                <asp:SqlDataSource ID="sdsFollowUp" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                                    SelectCommand="SELECT   [YANTRA_QUOT_FOLLOWUP_DET] .[FU_DATE],&#13;&#10;[YANTRA_QUOT_FOLLOWUP_DET] .[FU_DESC],&#13;&#10;[YANTRA_QUOT_FOLLOWUP_DET] .[FU_TECH_DISCUSSIONS],&#13;&#10;[YANTRA_QUOT_FOLLOWUP_DET] .[FU_COMM_NEGOS],&#13;&#10;[YANTRA_QUOT_FOLLOWUP_DET] .[FU_COMP_EXISTANCE],&#13;&#10;[YANTRA_QUOT_FOLLOWUP_DET] .[FU_REMARKS],&#13;&#10;[YANTRA_QUOT_FOLLOWUP_DET] .[FU_FWP_EXPEXTED_DATE],&#13;&#10;[YANTRA_EMPLOYEE_MAST].[EMP_FIRST_NAME] &#13;&#10;FROM [YANTRA_EMPLOYEE_MAST],[YANTRA_QUOT_FOLLOWUP_DET] &#13;&#10;WHERE [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_QUOT_FOLLOWUP_DET] .EMP_ID&#13;&#10; AND [YANTRA_QUOT_FOLLOWUP_DET] .QUOT_ID=@QUOT_ID &#13;&#10;ORDER BY [YANTRA_QUOT_FOLLOWUP_DET] .[FU_DATE] DESC">
                                                    <SelectParameters>
                                                        <asp:ControlParameter PropertyName="Text" DefaultValue="0" Name="QUOT_ID" ControlID="lblQuotIdHiddenForFollowUp"></asp:ControlParameter>
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4"></td>
                </tr>
            </table>
            <table id="tblHiddenFields" runat="server" visible="false">
                <tr>
                    <td>
                        <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CausesValidation="False"
                            Visible="False" /></td>
                    <td>
                        <asp:Label ID="Label16" runat="server" Text="Jurisdiction" Visible="False"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtJurisdiction" runat="server" Visible="False"></asp:TextBox></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Label ID="Label14" runat="server" Text="Erection/Commissioning" Visible="False"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtErrection" runat="server" Visible="False"></asp:TextBox></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Label ID="Label17" runat="server" Text="Inspection" Visible="False"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtInspection" runat="server" Visible="False"></asp:TextBox></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="C.S. Tax" Visible="False"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtCST" runat="server" Visible="False"></asp:TextBox>
                        <asp:Label ID="Label30" runat="server" EnableTheming="False" Text="%" Visible="False"></asp:Label>
                        <cc1:FilteredTextBoxExtender
                            ID="ftxteCST" runat="server" TargetControlID="txtCST" ValidChars=".0123456789">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
            <asp:ValidationSummary ID="vs1" runat="server" ShowMessageBox="True"
                ShowSummary="False"></asp:ValidationSummary>
            <asp:ValidationSummary ID="vs2" runat="server" ValidationGroup="a"
                ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>
            <asp:ValidationSummary ID="vs3" runat="server" ValidationGroup="qi"
                ShowMessageBox="True" ShowSummary="False" ShowModelStateErrors="False"></asp:ValidationSummary>
            <asp:ValidationSummary ID="vs4" runat="server" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="follow" ShowModelStateErrors="False"></asp:ValidationSummary>
            <br />
            <asp:Label ID="Label3" runat="server" Text="Currency Type" Visible="False"></asp:Label>
            <asp:DropDownList ID="ddlCurrencyType" runat="server" AutoPostBack="True" Visible="False">
            </asp:DropDownList>
            <asp:Label ID="Label37" runat="server" EnableTheming="False" ForeColor="Red"
                Text="*" Visible="False"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlCurrencyType"
                ErrorMessage="Please Select Currency Type" InitialValue="0" Visible="False">*</asp:RequiredFieldValidator><br />
            <asp:Label ID="Label7" runat="server" Text="Excise Duty" Visible="False"></asp:Label>
            <asp:TextBox ID="txtExciseDuty" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="Label38" runat="server" EnableTheming="False" Text="%" Visible="False"></asp:Label>
            <cc1:FilteredTextBoxExtender
                ID="ftxteExciseDuty" runat="server" TargetControlID="txtExciseDuty" ValidChars=".0123456789" Enabled="True">
            </cc1:FilteredTextBoxExtender>
            <asp:Button ID="btnApproves" runat="server" Text="Approves" OnClick="btnApprove_Click" Visible="False" />
            <asp:SqlDataSource
                ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:Parameter Type="Int64" DefaultValue="0" Name="BranbId"></asp:Parameter>
                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue"
                        ControlID="txtSearchModel"></asp:ControlParameter>
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource
                ID="SqlDataSourceopt" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:Parameter Type="Int64" DefaultValue="0" Name="BranbId"></asp:Parameter>
                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtOptSearch"></asp:ControlParameter>
                </SelectParameters>
            </asp:SqlDataSource>
            <cc1:FilteredTextBoxExtender
                ID="ftxtePackingCharges" runat="server" TargetControlID="txtPackingCharges" ValidChars=".0123456789">
            </cc1:FilteredTextBoxExtender>
            <asp:SqlDataSource ID="sdsQuotationDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                SelectCommand="SP_SM_SALESQUOTATION_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
                    <asp:ControlParameter PropertyName="Text" DefaultValue="0" Name="EMPID" ControlID="lblEmpIdHidden"></asp:ControlParameter>
                    <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserId" ControlID="lblUserId"></asp:ControlParameter>
                    <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:RadioButton ID="rdbDiscountPriceNOModelno" runat="server" AutoPostBack="True" GroupName="a"
                OnCheckedChanged="rdbDiscountPriceNOModelno_CheckedChanged" Text="With Normal Price" Visible="False"></asp:RadioButton>
            <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                Visible="False"></asp:Label>
            <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblUserId" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblUserType" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label><asp:Label
                ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
     <asp:Button ID="btnExport2"   runat="server" Text="Export To Excel" OnClick="btnExport2_Click"/>
         <asp:Button ID="btnExportPdf"   runat="server" Text="Export To PDF" OnClick="btnExportPdf_Click"/>

</asp:Content>



