<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SampleReturnNew.aspx.cs" Inherits="Modules_SCM_SampleReturnNew" %>

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

function Include() {
    var Inc, Gross, Tot;
    Gross = parseFloat(document.getElementById('<%=txtAfteronemonthHidden.ClientID %>').value);

    inc = document.getElementById('<%=txtIncludeVat.ClientID %>').value;

    if (Inc == "" || Inc == "0" || isNaN(Inc)) {
        Inc = "0";
    }
    if (Gross == "" || Gross == "0" || isNaN(Gross)) {
        Gross = "0";
    }
    Tot = (Number(Gross)) * ((Number(inc)) / 100)
    var Tot = Tot.toFixed(2);
    document.getElementById('<%=txtTotalAmt.ClientID %>').value = (Number(Gross) + Number(Tot));

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
    //        if(document.getElementById('<%=rbVAT.ClientID %>').checked==true)
       //        {
       //            document.getElementById('<%=txtVAT.ClientID %>').style.display=document.getElementById('<%=lblVAT.ClientID %>').style.display ="";
       //            document.getElementById('<%=txtCST.ClientID %>').style.display=document.getElementById('<%=lblCSTax.ClientID %>').style.display ="none";
       //            document.getElementById('<%=txtVAT.ClientID %>').focus();
       //        }  
       //        if(document.getElementById('<%=rbCST.ClientID %>').checked==true)
       //        {
       //            document.getElementById('<%=txtVAT.ClientID %>').style.display=document.getElementById('<%=lblVAT.ClientID %>').style.display ="none";
       //            document.getElementById('<%=txtCST.ClientID %>').style.display = document.getElementById('<%=lblCSTax.ClientID %>').style.display ="";
       //            document.getElementById('<%=txtCST.ClientID %>').focus();       
       //        } 
       document.getElementById('<%=txtVAT.ClientID %>').value = "";
       document.getElementById('<%=txtCST.ClientID %>').value = "";
       grosscalc();
   }
    </script>
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead" style="text-align: left;">
        <tr>
            <td colspan="3" style="text-align: left">Sample & Cash Sales Return</td>
        </tr>
    </table>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                    <td colspan="3" style="height: 7px">
                        <table id="tblsr" runat="server" border="0" cellpadding="0" cellspacing="0"
                            visible="true" width="100%">
                            <tr>
                                <td class="profilehead" colspan="4" style="text-align: left">Sales Return Details</td>
                            </tr>
                            <tr>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                                <td style="text-align: right">&nbsp;</td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label5" runat="server" Text="Sales Return No"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtSalesReturnNo" runat="server">
                                    </asp:TextBox></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label6" runat="server" Text="Sales Return Date"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtSalesReturndate" runat="server">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: left" class="profilehead" colspan="4">General Details</td>
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
                                <td style="font-size: 12pt; font-family: Times New Roman; text-align: left">
                                    <asp:TextBox ID="txtChallanDate" runat="server" ReadOnly="True">
                                    </asp:TextBox><cc1:CalendarExtender ID="CeChallanDate" runat="server"
                                        Enabled="False" Format="dd/MM/yyyy" PopupButtonID="imgChallanDate" TargetControlID="txtChallanDate">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender
                                        ID="MeeChallanDate" runat="server" DisplayMoney="Left" Enabled="True" Mask="99/99/9999"
                                        MaskType="Date" TargetControlID="txtChallanDate" UserDateFormat="MonthDayYear">
                                    </cc1:MaskedEditExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label8" runat="server" Text="Sales Invoice No"></asp:Label></td>
                                <td style="height: 19px; text-align: left">
                                    <asp:DropDownList ID="ddlSalesInvoiceNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSalesInvoiceNo_SelectedIndexChanged">
                                    </asp:DropDownList></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label12" runat="server" Text="Sales Invoice Date"></asp:Label></td>
                                <td style="font-size: 12pt; font-family: Times New Roman; height: 19px; text-align: left">
                                    <asp:TextBox ID="txtSalesInvoiceDate" runat="server">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblCustomer" runat="server" Text="Customer Name"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox
                                        ID="txtCustomerName" runat="server" ReadOnly="True"></asp:TextBox></td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblRegion" runat="server" Text="Region"></asp:Label></td>
                                <td style="font-size: 12pt; font-family: Times New Roman; text-align: left">
                                    <asp:TextBox ID="txtRegion" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtAddress" runat="server" ReadOnly="True" TextMode="MultiLine">
                                    </asp:TextBox></td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblPhone" runat="server" Text="Phone"></asp:Label></td>
                                <td style="text-align: left">
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
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right"></td>
                                <td style="text-align: left">
                                    <asp:Label ID="lblDCType" runat="server" Visible="False"></asp:Label>
                                </td>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td class="profilehead" colspan="4" style="text-align: left; height: 20px;">Delivery Challan Items</td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                    <asp:GridView ID="gvDeliveryChallanItems" runat="server" AutoGenerateColumns="False"
                                        Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="DC No" HeaderText="DC No"></asp:BoundField>
                                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                            <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                                            <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                            <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <span style="color: #ff0033">No Data to Dispaly</span>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td class="profilehead" colspan="4" style="text-align: left">Sales Invoice</td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <asp:GridView ID="gvSalesInvoice" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSalesInvoice_RowDataBound" Width="100%">
                                        <Columns>
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
                                            <asp:BoundField DataField="SPPrice" HeaderText="Special Price"></asp:BoundField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <span style="color: #ff0033">No Data to Dispaly</span>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td class="profilehead" colspan="4">Sales Return</td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <asp:GridView ID="gvItmDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvItmDetails_RowDataBound"
                                        OnRowDeleting="gvItmDetails_RowDeleting" OnRowEditing="gvItmDetails_RowEditing" Width="100%">
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
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <span style="color: #ff0033">No Data to Dispaly</span>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td class="profilehead" colspan="4" style="text-align: left">Items Details</td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: right"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label4" runat="server" Text="Model No :"></asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="ddlModelNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModelNo_SelectedIndexChanged"></asp:DropDownList></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label7" runat="server" Text="Item Name" Width="76px"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtItemname" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right" class="auto-style1">
                                    <asp:Label ID="Label27" runat="server" Text="UOM"></asp:Label></td>
                                <td style="text-align: left" class="auto-style1">
                                    <asp:TextBox ID="txtItemUOM" runat="server" ReadOnly="True"></asp:TextBox></td>
                                <td style="text-align: right" class="auto-style1">
                                    <asp:Label ID="Label59" runat="server" Text="Color :"></asp:Label></td>
                                <td class="auto-style1">
                                    <asp:DropDownList ID="ddlColor" runat="server">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label47" runat="server" Text="Company"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" meta:resourcekey="ddlCompanyResource1"
                                        OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged1">
                                    </asp:DropDownList></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label45" runat="server" Text="Godown"></asp:Label></td>
                                <%--<td style="text-align: left">
                                                    <%--<asp:DropDownList id="ddllocation" runat="server">
                                                    </asp:DropDownList></td>--%>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddllocation" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlDataSource22" DataTextField="whname" DataValueField="wh_id">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource22" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="USP_Warehouse_Loc_Select" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="lblCompany" DefaultValue="0" Name="LocID" PropertyName="Text" Type="String" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <asp:Label ID="lblCompany" runat="server" Visible="False"></asp:Label>
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
                                        Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtRate"
                                        ErrorMessage="Please Enter the Rate" ValidationGroup="id" Visible="False">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                            ID="ftxteRate" runat="server" Enabled="False" TargetControlID="txtRate" ValidChars=".0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtQuantity" runat="server" Width="139px"></asp:TextBox>
                                    <asp:Label ID="Label18" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtQuantity"
                                        ErrorMessage="Please Enter the Quantity" ValidationGroup="id" Visible="False">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                            ID="ftxteQuantity" runat="server" Enabled="False" FilterType="Numbers" TargetControlID="txtQuantity"
                                            ValidChars=".">
                                        </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblAmount" runat="server" Text="Amount"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtAmount" runat="server" ReadOnly="True"></asp:TextBox></td>
                                <td style="text-align: right;"></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtSpprice" runat="server" Visible="False"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                                <td style="text-align: right"></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtDeliDate" runat="server" Visible="False">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                        CssClass="loginbutton" EnableTheming="False" OnClick="btnAdd_Click" Text="Add"
                                        ValidationGroup="id" /><asp:Button ID="btnItemRefresh" runat="server"
                                            BackColor="Transparent" BorderStyle="None" CausesValidation="False" CssClass="loginbutton"
                                            EnableTheming="False" OnClick="btnItemRefresh_Click" Text="Refresh" /></td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center; height: 19px;">&nbsp;
                                    <asp:GridView ID="gvsales" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvsales_RowDataBound"
                                        OnRowDeleting="gvsales_RowDeleting" ShowFooter="True" Width="100%">
                                        <FooterStyle BackColor="#1AA8BE" BorderStyle="None" />
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
                                            <asp:BoundField DataField="SPPrice" HeaderText="Special Price"></asp:BoundField>
                                            <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                                            <asp:BoundField DataField="ColorId" HeaderText="ColorId"></asp:BoundField>
                                            <asp:BoundField DataField="GodownId" HeaderText="GodownId"></asp:BoundField>
                                            <asp:BoundField DataField="CompanyId" HeaderText="CompanyId"></asp:BoundField>
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
                                <td style="text-align: right">
                                    <asp:Label ID="Label24" runat="server" Text="After One Month"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtTotalAmt" runat="server" ReadOnly="True">
                                    </asp:TextBox><cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                        TargetControlID="txtMiscelleneous" ValidChars=".0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    <asp:HiddenField ID="txtAfteronemonthHidden" runat="server"></asp:HiddenField>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label13" runat="server" Text="Include Vat"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtIncludeVat" runat="server">
                                    </asp:TextBox>
                                    <asp:Label ID="Label15" runat="server" EnableTheming="False" Font-Bold="False"
                                        Font-Names="Verdana" Font-Size="Smaller" Text="%"></asp:Label>
                                    <asp:RadioButton ID="rbVAT" runat="server" Checked="True" GroupName="vatcst" Text="VAT" Visible="False"></asp:RadioButton><asp:RadioButton ID="rbCST" runat="server" GroupName="vatcst" Text="C.S. Tax" Visible="False"></asp:RadioButton></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label10" runat="server" Text="Within One Month"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtGrossAmount" runat="server" Width="349px">
                                    </asp:TextBox><asp:HiddenField ID="txtGrossTotalAmtHidden" runat="server"></asp:HiddenField>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblVAT" runat="server" Text="VAT"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtVAT" runat="server">
                                    </asp:TextBox>
                                    <asp:Label ID="Label3" runat="server" EnableTheming="False" Font-Bold="False"
                                        Font-Names="Verdana" Font-Size="Smaller" Text="%"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label11" runat="server" Text="Miscelleneous"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtMiscelleneous" runat="server">
                                    </asp:TextBox><cc1:FilteredTextBoxExtender ID="ftxteMiscelleneous" runat="server" TargetControlID="txtMiscelleneous"
                                        ValidChars=".0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblCSTax"
                                        runat="server" Text="C.S. Tax"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtCST" runat="server" Width="149px"></asp:TextBox><asp:Label ID="Label25" runat="server" EnableTheming="False" Font-Bold="False"
                                        Font-Names="Verdana" Font-Size="Smaller" Text="%"></asp:Label><cc1:FilteredTextBoxExtender ID="ftxteVat" runat="server"
                                            TargetControlID="txtVAT" ValidChars=".0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    <cc1:FilteredTextBoxExtender
                                        ID="ftxteCST" runat="server" TargetControlID="txtCST" ValidChars=".0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right"></td>
                                <td style="text-align: left">&nbsp;
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label9" runat="server" Text="Discount"></asp:Label></td>
                                <td style="text-align: left">
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
                                    <asp:Label ID="lblRemarks" runat="server" Text="Remarks"></asp:Label></td>
                                <td colspan="3" style="text-align: left">
                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="textbox" EnableTheming="False"
                                        Height="53px" TextMode="MultiLine" Width="673px">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="profilehead" colspan="4" style="text-align: left">Reference Details</td>
                            </tr>
                            <tr>
                                <td style="height: 19px; text-align: right"></td>
                                <td style="height: 19px; text-align: left"></td>
                                <td style="height: 19px; text-align: right"></td>
                                <td style="height: 19px; text-align: left"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                                    </asp:DropDownList></td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlApprovedBy" runat="server" Enabled="False">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table id="tblButtons" align="center">
                                        <tr>
                                            <td style="height: 26px">
                                                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" /></td>
                                            <td style="height: 26px">
                                                <asp:Button ID="btnApprove" runat="server" CausesValidation="False" OnClick="btnApprove_Click"
                                                    Text="Approve" /></td>
                                            <td style="height: 26px">
                                                <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" OnClick="btnRefresh_Click"
                                                    Text="Refresh" /></td>
                                            <td style="height: 26px">
                                                <asp:Button ID="btnClose" runat="server" CausesValidation="False" OnClick="btnClose_Click"
                                                    Text="Close" />
                                            </td>
                                            <td style="height: 26px"></td>
                                            <td style="width: 3px; height: 26px;"></td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="txtVatHidden" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="txtCstHidden" runat="server"></asp:HiddenField>
                                </td>
                            </tr>
                        </table>
                        <asp:Button ID="btnGo" runat="server" CausesValidation="False"
                            Text="Go" Visible="False" />
                        <asp:Button ID="btngo1" runat="server" Text="Go" Visible="False" /></td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


 
