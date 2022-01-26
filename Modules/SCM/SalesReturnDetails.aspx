<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/FinanceMP1.master" AutoEventWireup="true" CodeFile="SalesReturnDetails.aspx.cs" Inherits="Modules_SCM_SalesReturnDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--   <script>
            $(function () {
                $("[name$='txtSalesReturndate'],[name$='txtSalesOrderDate'],[name$='txtChallanDate'],[name$='txtSalesInvoiceDate']").datepicker();
            });
    </script>--%>
    <script>
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
    </script>

     <script>
         function Grossamtcalc() {
             var amt, vat;
             amt = document.getElementById('<%=txtTotalAmt.ClientID %>').value;
             vat = document.getElementById('<%=txtIncludeVat.ClientID %>').value;


             if (vat == "" || vat == "0") {
                document.getElementById('<%=txtGrossAmount.ClientID %>').value = amt;
            }
             else if (amt == "" || amt == "0") {
                document.getElementById('<%=txtGrossAmount.ClientID %>').value = "0";
             }
             else if (rate > 0 && req_qty > 0) {
                 document.getElementById('<%=txtGrossAmount.ClientID %>').value = (amt + (amt*vat/100));

               }

   }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table class="pagehead" style="width: 100%">
        <tr>
            <td colspan="4" style="text-align: left;">Sales Return Details</td>
        </tr>
    </table>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                    <td colspan="3" style="height: 7px; text-align: center;">
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
                                    <asp:TextBox ID="txtSalesReturndate" runat="server" type="datepic">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: left" class="profilehead" colspan="4">General Details</td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblSearch" runat="server" Text="Search"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtSearchModel" runat="server">
                                    </asp:TextBox><asp:Button ID="btnSearchModelNo" runat="server" BorderStyle="None"
                                        CausesValidation="False" CssClass="gobutton" EnableTheming="False" OnClick="btnSearchModelNo_Click"
                                        Text="Go" />
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                        SelectCommand="SP_Customer_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtSearchModel"></asp:ControlParameter>
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                                <td style="font-size: 12pt; color: #000000; font-family: Times New Roman; text-align: right"></td>
                                <td style="font-size: 12pt; font-family: Times New Roman; text-align: left"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblCustomer" runat="server" Text="Customer Name"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomerName_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:Label ID="Label16" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCustomerName"
                                        ErrorMessage="Please Enter the Customer Name" InitialValue="0">*</asp:RequiredFieldValidator><asp:TextBox
                                            ID="txtCustomerName" runat="server" ReadOnly="True" Visible="False"></asp:TextBox></td>
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
                                <td style="text-align: right">
                                    <asp:Label ID="lblSalesOrderNo" runat="server" Text="Sales Order No" Width="101px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlSalesOrderNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSalesOrderNo_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:Label ID="Label14" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSalesOrderNo"
                                        ErrorMessage="Please Enter the Sales Order No" InitialValue="0">*</asp:RequiredFieldValidator></td>
                                <td style="text-align: right">
                                    <%-- <asp:Label ID="lblSalesOrderDate" runat="server" Text="Sales Order Date">
                            </asp:Label>
                                    --%>
                                    <asp:Label ID="lblSalesOrderDate" runat="server" Text="Sales Order Date" Width="146px"></asp:Label>
                                </td>
                                <td style="font-size: 12pt; font-family: Times New Roman; text-align: left">
                                    <asp:TextBox ID="txtSalesOrderDate" runat="server" ReadOnly="false" type="datepic">
                                    </asp:TextBox>

                                    <%--                            <cc1:MaskedEditExtender
                                ID="MeeSalesOrderDate" runat="server" DisplayMoney="Left" Enabled="True" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtSalesOrderDate" UserDateFormat="MonthDayYear">
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
                                            ErrorMessage="Please Select the Delivery Challan No" InitialValue="0">*</asp:RequiredFieldValidator></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label2" runat="server" Text="Delivery Challan Date" Width="146px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtChallanDate" runat="server" ReadOnly="false" type="datepic">
                                    </asp:TextBox>

                                    <%--<cc1:MaskedEditExtender
                                ID="MeeChallanDate" runat="server" DisplayMoney="Left" Enabled="True" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtChallanDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label8" runat="server" Text="Sales Invoice No"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlSalesInvoiceNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSalesInvoiceNo_SelectedIndexChanged">
                                    </asp:DropDownList></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label12" runat="server" Text="Sales Invoice Date"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtSalesInvoiceDate" runat="server" type="datepic">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: left" class="profilehead">
                                    <asp:Label ID="lblOrderedItemsHeading" runat="server" EnableTheming="False">Sales Ordered Items</asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                    <asp:GridView ID="gvItemDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvItemDetails_RowDataBound"
                                        Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                            <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                                            <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                            <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                            <asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
                                            <asp:BoundField HeaderText="Amount"></asp:BoundField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <span style="color: #ff0000">No Data to Dispaly</span>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td class="profilehead" colspan="4" style="text-align: left;">Delivery Challan Items</td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
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
                                            <asp:BoundField DataField="SPPrice" HeaderText="Special Price "></asp:BoundField>
                                            <asp:BoundField DataField="Vat" HeaderText="Vat"></asp:BoundField>
                                            <asp:BoundField DataField="Cst" HeaderText="Cst"></asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkItemSelect" runat="server" __designer:wfdid="w6"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                                            <asp:BoundField DataField="Godown" HeaderText="Godown"></asp:BoundField>
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
                                            <asp:BoundField DataField="Rate" HeaderText="Amount"></asp:BoundField>
                                            <asp:BoundField DataField="Vat" HeaderText="Vat"></asp:BoundField>
                                            <asp:BoundField DataField="CST" HeaderText="CST"></asp:BoundField>
                                            <asp:BoundField DataField="Excise" HeaderText="Excise"></asp:BoundField>
                                            <asp:BoundField HeaderText="GrossAmount"></asp:BoundField>
                                            <asp:BoundField DataField="SPPrice" HeaderText="Special Price"></asp:BoundField>
                                            <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="DeliveryDate" HeaderText="Delivery Date"></asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkItemSelect" runat="server" __designer:wfdid="w5"></asp:CheckBox>
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
                                            <asp:BoundField DataField="SPPrice" HeaderText="Special Price"></asp:BoundField>
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
                                <td style="text-align: left">&nbsp;<asp:DropDownList ID="ddlModelNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModelNo_SelectedIndexChanged"></asp:DropDownList></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label7" runat="server" Text="Item Name" Width="76px"></asp:Label></td>
                                <td style="text-align: left">&nbsp;<asp:TextBox ID="txtItemname" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label27" runat="server" Text="UOM"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtItemUOM" runat="server" ReadOnly="True"></asp:TextBox></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label59" runat="server" Text="Color :"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlColor" runat="server" AutoPostBack="True">
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
                            <asp:DropDownList ID="ddllocation" runat="server" AutoPostBack="True">
                            </asp:DropDownList></td>--%>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddllocation" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource22" DataTextField="whname" DataValueField="wh_id">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddllocation"
                                        ErrorMessage="Please Select Godown" InitialValue="0" ValidationGroup="id" Visible="False" Font-Bold="True" ForeColor="Red">*</asp:RequiredFieldValidator>

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
                                        ErrorMessage="Please Enter the Quantity" ValidationGroup="id" Visible="False">*</asp:RequiredFieldValidator>
                                    <cc1:FilteredTextBoxExtender
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
                                <td style="text-align: left">
                                    <asp:Label ID="lblGrossAmt" runat="server" Visible="false"></asp:Label></td>
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
                                <td colspan="4" style="text-align: center; height: 19px;">&nbsp;<asp:GridView ID="gvsales" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvsales_RowDataBound"
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
                                <td class="profilehead" colspan="4">Other Charges</td>
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
                                    <asp:Label ID="Label13" runat="server" Text="Include Vat" Visible="False"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtIncludeVat" runat="server" Visible="False"></asp:TextBox>
                                    <asp:Label ID="Label15" runat="server" EnableTheming="False" Font-Bold="False"
                                        Font-Names="Verdana" Font-Size="Smaller" Text="%" Visible="False"></asp:Label>
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
                                    <asp:Label ID="lblTtlAmt" runat="server" Text="0" Visible="false"></asp:Label>
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
                                    <table id="tblButtons">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" /></td>
                                            <td>
                                                <asp:Button ID="btnApprove" runat="server" CausesValidation="False" OnClick="btnApprove_Click"
                                                    Text="Approve" /></td>
                                            <td>
                                                <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" OnClick="btnRefresh_Click"
                                                    Text="Refresh" />
                                                <asp:Button ID="btnDelete" runat="server" CausesValidation="False" OnClick="btnDelete_Click"
                                                    Text="Delete" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnClose" runat="server" CausesValidation="False" OnClick="btnClose_Click" Text="Close" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnPrint" runat="server" CausesValidation="False" OnClick="btnPrint_Click"
                                                    Text="Print" /></td>
                                            <td style="width: 3px"></td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="txtVatHidden" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="txtCstHidden" runat="server"></asp:HiddenField>
                                </td>
                            </tr>
                        </table>
                        <asp:Button ID="btnGo" runat="server" CausesValidation="False" OnClick="btnGo_Click"
                            Text="Go" Visible="False" />
                        <asp:Button ID="btngo1" runat="server" OnClick="btngo1_Click" Text="Go" Visible="False" /></td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
        Visible="False"></asp:Label><asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label><asp:Label
            ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
        SelectCommand="SP_INVENTORY_SALESRETURN_SEARCH_SELECT" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="EMPID" ControlID="lblEmpIdHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>


 
