<%@ Page Language="C#" MasterPageFile="~/MPs/SalesMP1.master" AutoEventWireup="true" Async="true"
    CodeFile="SalesOrderNew.aspx.cs" Inherits="Modules_SM_SalesOrder" Title="|| Value App : S&M : Sales Order ||" %>

<%@ Register Assembly="FUA" Namespace="Subgurim.Controles" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">

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
    <script language="javascript" type="text/javascript">
        function CheckNumeric(e) {

            if (window.event) // IE 
            {
                if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8 & e.keyCode != 46) {
                    event.returnValue = false;
                    return false;

                }
            }
            else { // Fire Fox
                if ((e.which < 48 || e.which > 57) & e.which != 8 & e.which != 46) {
                    e.preventDefault();
                    return false;

                }
            }
        }

    </script>
    <style type="text/css">
        .auto-style1 {
            height: 20px;
        }

        .auto-style2 {
            height: 27px;
        }
    </style>
    <script type="text/javascript">

        function othernextfocus() {
            document.getElementById('<%=txtContactName1.ClientID%>').focus();
        }
        function email2nextfocus() {
            document.getElementById('<%=txtConsinment.ClientID%>').focus();
        }
        function rbVATCSTEnableDisable() {
            if (document.getElementById('<%=rbVAT.ClientID %>').checked == true) {
                document.getElementById('<%=lblVATCST.ClientID %>').innerHTML = "TAX";
                var textBox = document.getElementById("<%=txtOtherSpecs.ClientID %>");
                textBox.innerText = "-";
            }
            if (document.getElementById('<%=rbCST.ClientID %>').checked == true) {
                document.getElementById('<%=lblVATCST.ClientID %>').innerHTML = "TAX";
                var textBox = document.getElementById("<%=txtOtherSpecs.ClientID %>");
                textBox.innerText = "-";
            }

        }
       <%-- function Radio_Click_VAT_CST() {
            var radio1 = document.getElementById("<%=rbVAT.ClientID %>");
            var radio1 = document.getElementById("<%=rbCST.ClientID %>");
            var textBox = document.getElementById("<%=txtOtherSpecs.ClientID %>");
            textBox.innerText = "-";
        }--%>

        function Radio_Click() {
            var radio1 = document.getElementById("<%=rbIncluding.ClientID %>");
            var textBox = document.getElementById("<%=txtOtherSpecs.ClientID %>");
            textBox.innerText = "Including Tax";
        }

        function amtcalc() {

            var req_qty, rate, disc;
            req_qty = document.getElementById('<%=txtItemQuantity.ClientID %>').value;
            rate = document.getElementById('<%=txtItemRate.ClientID %>').value;
            disc = document.getElementById('<%=txtDiscount.ClientID %>').value;


            if (req_qty == "" || req_qty == "0") {
                document.getElementById('<%=txtSpecialPrice.ClientID %>').value = "0";
            }
            else if (rate == "" || rate == "0") {
                document.getElementById('<%=txtSpecialPrice.ClientID %>').value = "0";
            }
            else if (rate > 0 && req_qty > 0) {
                document.getElementById('<%=txtSpecialPrice.ClientID %>').value = (rate * req_qty);
            }
    if (disc != "" && disc != "0") {
        document.getElementById('<%=txtSpecialPrice.ClientID %>').value = parseFloat((rate * req_qty)) - parseFloat((((rate * req_qty) * disc) / 100));
    }

}
        function GVAmtCalc() {
            var req_qty, rate, disc;
            req_qty = document.getElementById('<%=txtItemQuantity.ClientID %>').value;
            rate = document.getElementById('<%=txtItemRate.ClientID %>').value;
            disc = document.getElementById('<%=txtDiscount.ClientID %>').value;
        }

    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <%--    <asp:UpdatePanel runat="server">
        <ContentTemplate>--%>

    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td style="text-align: left">Purchase Order</td>
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
            <td class="searchhead" colspan="4" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left" colspan="2">Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                            <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
                        </td>

                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="SO_NO">PO No</asp:ListItem>
                                            <asp:ListItem Value="SO_DATE">PO Date</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME">Customer</asp:ListItem>
                                            <asp:ListItem Value="CUST_CONTACT_PERSON">Contact Person</asp:ListItem>
                                            <asp:ListItem Value="CUST_EMAIL">EMail</asp:ListItem>
                                            <%--<asp:ListItem Value="QUOT_PO_FLAG">Status</asp:ListItem>--%>
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
                                        <asp:TextBox ID="txtSearchValueFromDate" type="datepic" runat="server" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox><%--<asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchFrom" runat="server" Enabled="False"
                                            PopupButtonID="imgFromDate" TargetControlID="txtSearchValueFromDate">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>--%>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchValueToDate" type="datepic" runat="server" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><%--<asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchValueToDate" runat="server"
                                            Enabled="False" PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>--%>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                                </tr>
                            </table>


                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvSalesOrderDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    OnRowDataBound="gvSalesOrderDetails_RowDataBound" Width="100%" SelectedRowStyle-BackColor="#c0c0c0" OnPageIndexChanging ="gvSalesOrderDetails_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="SO_ID" HeaderText="SalesOrderIdHidden"></asp:BoundField>
                        <asp:TemplateField HeaderText="PO No">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("SO_NO") %>' ID="TextBox1"></asp:TextBox>

                            </EditItemTemplate>

                            <ControlStyle Width="100px"></ControlStyle>

                            <ItemStyle Width="100px" HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle Width="100px" HorizontalAlign="Left"></HeaderStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnSalesOrderNo" OnClick="lbtnSalesOrderNo_Click" ForeColor="#0066ff" runat="server" Text='<%# Bind("SO_NO") %>' CausesValidation="False"></asp:LinkButton>
                            </ItemTemplate>

                            <FooterStyle Width="100px"></FooterStyle>
                        </asp:TemplateField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True" DataField="SO_DATE" HeaderText="PurchaseOrderDate"></asp:BoundField>
                        <asp:BoundField DataField="CUST_NAME" HeaderText="Customer">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CUST_CONTACT_PERSON" HeaderText="ContactPerson">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Executive" HeaderText="Executive Name">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PREPAREDBY" HeaderText="PreparedBy">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="APPROVEDBY" HeaderText="ApprovedBy">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="SO_ACCEPTANCE_FLAG" HeaderText="Status">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Full_CompName" HeaderText="Company Name">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        No Data Exist!
                    </EmptyDataTemplate>
                    <SelectedRowStyle BackColor="LightSteelBlue" />
                </asp:GridView>
                <asp:SqlDataSource ID="sdsSalesOrderDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SM_SALESORDER_SEARCH_SELECT_2" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="EmpId" ControlID="lblEmpIdHidden"></asp:ControlParameter>
                        <asp:ControlParameter ControlID="lblCPID" Name="CPID" DefaultValue="0" PropertyName="Text" Type="Int64" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table border="0" cellpadding="0" cellspacing="0" id="tblSalesOrderDetails" runat="server"
                    visible="false" width="100%">
                    <tr>
                        <td class="profilehead" colspan="3">General Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label1" runat="server" Text="Quotation No" Width="86px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlQuotationNo" runat="server" OnSelectedIndexChanged="ddlQuotationNo_SelectedIndexChanged"
                                AutoPostBack="True" Enabled="False">
                            </asp:DropDownList><asp:Label ID="Label35" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlQuotationNo"
                                    ErrorMessage="Please Select the Quotation No" InitialValue="0" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label2" runat="server" Text="Quotation Date" Width="96px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtQuotationDate" runat="server" CssClass="datetext" EnableTheming="False" ReadOnly="True"></asp:TextBox>&nbsp;<asp:Image ID="imgQuotationDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                Visible="False" /><cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceQuotationDate"
                                    runat="server" PopupButtonID="imgQuotationDate" TargetControlID="txtQuotationDate"
                                    Enabled="False">
                                </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeQuotationDate" runat="server" DisplayMoney="Left"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtQuotationDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td id="TD18" style="height: 22px; text-align: right">
                            <asp:Label ID="lblCustomer" runat="server" Text="Customer"></asp:Label></td>
                        <td style="height: 22px; text-align: left">
                            <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="True" Enabled="False"
                                OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                            </asp:DropDownList><asp:Label ID="Label25" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*">
                            </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlCustomer"
                                ErrorMessage="Please Select the Customer" InitialValue="0" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                        <td style="height: 22px; text-align: right">
                            <asp:Label ID="lblRegion" runat="server" Text="Region"></asp:Label></td>
                        <td style="height: 22px; text-align: left">
                            <asp:TextBox ID="txtRegion" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 22px; text-align: right">
                            <asp:Label ID="Label26" runat="server" Text="Industry Type"></asp:Label></td>
                        <td style="height: 22px; text-align: left">
                            <asp:TextBox ID="txtIndustryType" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="height: 22px; text-align: right">
                            <asp:Label ID="lblInitName" runat="server" Text="Unit Name" Width="74px"></asp:Label></td>
                        <td style="height: 22px; text-align: left">
                            <asp:DropDownList ID="ddlUnitName" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="0">--Select Customer--</asp:ListItem>
                            </asp:DropDownList><asp:RequiredFieldValidator ID="rfvUnitName" runat="server" ControlToValidate="ddlUnitName"
                                ErrorMessage="Please Select the Unit Name" InitialValue="0" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 31px;">
                            <asp:Label ID="lblUnitAddress" runat="server" Text="Address" Width="106px"></asp:Label></td>
                        <td colspan="3" style="text-align: left; height: 31px;">
                            <asp:TextBox ID="txtUnitAddress" runat="server" CssClass="multilinetext" EnableTheming="False"
                                Font-Names="Verdana" Font-Size="8pt" TextMode="MultiLine" Width="620px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtContactPerson" runat="server" ReadOnly="True">
                            </asp:TextBox><asp:DropDownList ID="ddlContactPerson" runat="server" AutoPostBack="True"
                                Enabled="False" OnSelectedIndexChanged="ddlContactPerson_SelectedIndexChanged"
                                Visible="False">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="0">--Select Unit Name--</asp:ListItem>
                            </asp:DropDownList><asp:RequiredFieldValidator ID="rfvContactPerson" runat="server"
                                ControlToValidate="ddlContactPerson" ErrorMessage="Please Select the Contact Person"
                                InitialValue="0" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td id="TD28" style="text-align: right">
                            <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No" Width="74px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPhoneNo" runat="server" ReadOnly="True">
                            </asp:TextBox>
                            <asp:Label ID="lblQuotRespId" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label55" runat="server" Text="Mobile" Width="74px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;"></td>
                        <td style="text-align: left;"></td>
                        <td style="text-align: right;"></td>
                        <td style="text-align: left;"></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="3">Sales Executive Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">&nbsp;</td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Executive Name :</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtExecutiveName" runat="server"></asp:TextBox></td>
                        <td style="text-align: right">Phone No :</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtExePhoneNo" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">&nbsp;
                            <asp:GridView ID="gvQuotationProducts" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvQuotationProducts_RowDeleting" OnRowEditing="gvQuotationProducts_RowEditing" OnRowDataBound="gvQuotationProducts_RowDataBound1" Width="100%" OnSelectedIndexChanged="gvQuotationProducts_SelectedIndexChanged">
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
                                    <asp:BoundField DataField="Discount" NullDisplayText="-" HeaderText="Disc %"></asp:BoundField>
                                    <asp:BoundField DataField="SpPrice" NullDisplayText="-" HeaderText="Special Price">
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Room" HeaderText="Room"></asp:BoundField>
                                    <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                                    <asp:BoundField DataField="ColorId" HeaderText="ColorId"></asp:BoundField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkhdr" runat="server" OnClick="selectAll(this)" />
                                            <%--<asp:CheckBox ID="chkhdr" runat="server" AutoPostBack="True" OnClick="selectAll(this)" OnCheckedChanged="chkhdr_CheckedChanged" />--%>
                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkItemSelect" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Button ID="btnGo" runat="server" CausesValidation="False" OnClick="btnGo_Click"
                                Text="Go" /></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4">Sales Order details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblQuotationNo" runat="server" Text="Purchase Order No" Width="124px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtSalesOrderNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblQuotationDate" runat="server" Text="Purchase Order Date"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtSalesOrderDate" runat="server" CssClass="datetext" type="datepic"></asp:TextBox>
                            <asp:Label ID="Label60" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*">
                            </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtSalesOrderDate"
                                ErrorMessage="Please Enter Purchase Order Date" ValidationGroup="a">*</asp:RequiredFieldValidator><asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="DateCustomValidate"
                                    ControlToValidate="txtSalesOrderDate" ErrorMessage="Please Enter the Sales Order Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                    SetFocusOnError="True">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label57" runat="server" Text="Customer PO No."></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCustPONo" runat="server"></asp:TextBox><asp:Label ID="Label61" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label><asp:RequiredFieldValidator ID="rfvCustPONo" runat="server" ControlToValidate="txtCustPONo"
                                    ErrorMessage="Please Enter Customer PO No" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label58" runat="server" Text="Customer PO Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCustPODated" runat="server" CssClass="datetext" EnableTheming="False" type="datepic"></asp:TextBox>
                            <asp:Label ID="Label59" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtCustPODated"
                                ErrorMessage="Please Enter Customer PO Dated" ValidationGroup="a">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="DateCustomValidate"
                                ControlToValidate="txtCustPODated" ErrorMessage="Please Enter the Customer PO Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                SetFocusOnError="True">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4">Billing And Shipping Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">&nbsp;</td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Billing Address :</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtBillingAdd" runat="server" TextMode="MultiLine" Height="60px" Width="400px">-</asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right">Delivery Address :</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtDeliveryAdd" runat="server" TextMode="MultiLine" Height="60px" Width="400px">-</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">PO Items</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:GridView ID="gvDonepo" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDataBound="gvDonepo_RowDataBound" ShowFooter="True">
                                <FooterStyle BackColor="#1AA8BE" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnDelete" CausesValidation="false" runat="server" __designer:wfdid="w5" OnClick="lbtnDelete_Click1">Delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Slno" HeaderText="Slno"></asp:BoundField>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                    <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                    <%--<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>--%>
                                    <asp:TemplateField HeaderText="Quantity" ControlStyle-Width="50px">
                                        <ItemTemplate>
                                                    <asp:TextBox ID="txtQuantity" runat="server" onkeyup="DueAmt()" Text='<%# Bind("Quantity") %>' AutoPostBack="true" 
                                                        OnTextChanged="txtQuantity_TextChanged"></asp:TextBox>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Currency" HeaderText="Currency"></asp:BoundField>
                                    <%--<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>--%>
                                    <asp:TemplateField HeaderText="Rate" ControlStyle-Width="70px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtMRP" runat="server" Text='<%# Bind("Rate") %>' AutoPostBack="true" OnTextChanged ="txtMRP_TextChanged" ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField HeaderText="UnitPrice"></asp:BoundField>--%>
                                    <asp:TemplateField HeaderText="Unit Price" ControlStyle-Width="70px" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnitPrice"  runat="server"  ></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <%--<asp:BoundField HeaderText="UnitPrice"></asp:BoundField>--%>
                                    <asp:TemplateField HeaderText="Spl Price" ControlStyle-Width="70px" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSplPrice" Text='<%#Bind("Price") %>' runat="server"  ></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="Price" HeaderText="Spl Price"></asp:BoundField>--%>
                                    <asp:BoundField DataField="Specifications" HeaderText="Specifications"></asp:BoundField>
                                    <asp:BoundField DataFormatString="{0:dd/MM/YYYY}" DataField="DeliveryDate" HeaderText="Delivery Date">
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Room" HeaderText="Brand"></asp:BoundField>
                                    <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                                    <asp:BoundField DataField="ColorId" HeaderText="Color Id"></asp:BoundField>
                                    <asp:BoundField DataField="Sales" HeaderText="Sales"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Discount" ControlStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvDiscount" runat="server" AutoPostBack="true" OnTextChanged="txtDiscount_TextChanged" ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Balance Qty" ControlStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtBalanceQty" Text='<%#Bind("BalanceQty") %>' runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:UpdatePanel runat="server">

                                <ContentTemplate>
                                    <table style="width: 100%">


                                        <tr>
                                            <td class="profilehead" colspan="4">Item Details</td>
                                        </tr>
                                        <tr>
                                            <td style="height: 19px; text-align: right">
                                                <asp:Label ID="lblSelectModel" runat="server" Text="Select Model" Visible="False"></asp:Label></td>
                                            <td style="height: 19px; text-align: left">
                                                <asp:RadioButton ID="rdbAll" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="rdbAll_CheckedChanged"
                                                    Text="All" Visible="False"></asp:RadioButton></td>
                                            <td style="height: 19px; text-align: right"></td>
                                            <td style="height: 19px; text-align: left"></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 19px; text-align: right">
                                                <asp:Label ID="lblSearch" runat="server" Text="Search:" Visible="False" Width="84px">
                                                </asp:Label></td>
                                            <td style="height: 19px; text-align: left">
                                                <asp:TextBox ID="txtSearchModel" runat="server" Visible="False">
                                                </asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server"
                                                    ControlToValidate="txtSearchModel" ErrorMessage="Please Enter ModelNo For Search"
                                                    ValidationGroup="Search" Visible="False">*</asp:RequiredFieldValidator><asp:Button
                                                        ID="btnSearchModelNo" runat="server" BorderStyle="None" CausesValidation="False"
                                                        CssClass="gobutton" EnableTheming="False" OnClick="btnSearchModelNo_Click" Text="Go"
                                                        ValidationGroup="Search" Visible="False" /></td>
                                            <td style="height: 19px; text-align: right">
                                                <asp:Label ID="lblSearchBrand" runat="server" Text="Search By Brand" Visible="False">
                                                </asp:Label></td>
                                            <td style="height: 19px; text-align: left">
                                                <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged"
                                                    Visible="False">
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 19px; text-align: right"></td>
                                            <td style="height: 19px; text-align: left"></td>
                                            <td style="height: 19px; text-align: right"></td>
                                            <td style="height: 19px; text-align: left"></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; height: 19px;">
                                                <asp:Label ID="Label3" runat="server" Text="Model No :"></asp:Label></td>
                                            <td style="text-align: left; height: 19px;">
                                                <asp:DropDownList ID="ddlModelNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModelNo_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" InitialValue="0"
                                                    ControlToValidate="ddlModelNo" ErrorMessage="Please select the Model No." ValidationGroup="item">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td style="text-align: right; height: 19px;">
                                                <asp:Label ID="Label4" runat="server" Text="Item Name" Width="69px"></asp:Label></td>
                                            <td style="text-align: left; height: 19px;">
                                                <asp:TextBox ID="txtItemname" runat="server">
                                                </asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label27" runat="server" Text="UOM"></asp:Label></td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtItemUOM" runat="server" ReadOnly="True">
                                                </asp:TextBox></td>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label22" runat="server" Text="Quantity" Width="57px"></asp:Label></td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtItemQuantity" runat="server">
                                                </asp:TextBox><asp:Label ID="Label37" runat="server" EnableTheming="False" ForeColor="Red"
                                                    Text="*"> </asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtItemQuantity"
                                                    ErrorMessage="Please Enter the Quantity" ValidationGroup="item">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                                        ID="ftxtQuantity" runat="server" FilterType="Numbers" TargetControlID="txtItemQuantity">
                                                    </cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 21px; text-align: right">
                                                <asp:Label ID="Label23" runat="server" Text="Item Specification"></asp:Label></td>
                                            <td colspan="3" style="height: 21px; text-align: left">
                                                <asp:TextBox ID="txtItemSpec" runat="server" CssClass="multilinetext" EnableTheming="False"
                                                    ReadOnly="True" TextMode="MultiLine" Width="50%">
                                                </asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 21px; text-align: right">
                                                <asp:Label ID="Label24" runat="server" Text="Rate"></asp:Label></td>
                                            <td style="height: 21px; text-align: left">
                                                <asp:TextBox ID="txtItemRate" runat="server">
                                                </asp:TextBox></td>
                                            <td style="height: 21px; text-align: right">
                                                <asp:Label ID="Label71" runat="server" Text="Special Price"></asp:Label></td>
                                            <td style="height: 21px; text-align: left">
                                                <asp:TextBox ID="txtSpecialPrice" runat="server">
                                                </asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 21px; text-align: right">
                                                <asp:Label ID="Label38" runat="server" Text="Dicount"></asp:Label></td>
                                            <td style="height: 21px; text-align: left">
                                                <asp:TextBox ID="txtDiscount" runat="server">
                                                </asp:TextBox></td>
                                            <td style="height: 21px; text-align: right">
                                                <asp:Label ID="lblPriority" runat="server" Text="Priority"></asp:Label></td>
                                            <td style="height: 21px; text-align: left">
                                                <asp:DropDownList ID="ddlItemPriority" runat="server">
                                                    <asp:ListItem Value="0">--</asp:ListItem>
                                                    <asp:ListItem>Low</asp:ListItem>
                                                    <asp:ListItem>Medium</asp:ListItem>
                                                    <asp:ListItem>High</asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 21px; text-align: right">
                                                <asp:Label ID="Label20" runat="server" Text="Specifications"></asp:Label></td>
                                            <td style="height: 21px; text-align: left" colspan="3">
                                                <asp:TextBox ID="txtItemSpecifications" runat="server" TextMode="MultiLine" CssClass="multilinetext"
                                                    EnableTheming="False" Width="50%">
                                                </asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 21px; text-align: right">
                                                <asp:Label ID="Label21" runat="server" Text="Remarks"></asp:Label></td>
                                            <td colspan="3" style="height: 21px; text-align: left">
                                                <asp:TextBox ID="txtItemRemarks" runat="server" TextMode="MultiLine" CssClass="multilinetext"
                                                    EnableTheming="False" Width="50%">
                                                </asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right" class="auto-style2">
                                                <asp:Label ID="Label54" runat="server" Visible="false" Text="Room"></asp:Label></td>
                                            <td style="text-align: left" class="auto-style2">
                                                <asp:TextBox ID="txtRoom" runat="server" Visible="false">
                                                </asp:TextBox><%--<asp:Label ID="Label67" runat="server" EnableTheming="False" ForeColor="Red"
                                                    Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                        ControlToValidate="txtItemRate" ErrorMessage="Please Enter the Rate" ValidationGroup="ip">*</asp:RequiredFieldValidator>--%></td>
                                            <td style="text-align: right" class="auto-style2">
                                                <asp:Label ID="Label36" runat="server" Text="Delivery Date"></asp:Label></td>
                                            <td style="text-align: left" class="auto-style2">
                                                <asp:TextBox ID="txtDeliveryDate" runat="server" type="datepic" CssClass="datetext" EnableTheming="False"></asp:TextBox><asp:Label
                                                    ID="Label39" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                                                </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                    ControlToValidate="txtDeliveryDate" ErrorMessage="Please Enter Delivery Date" ValidationGroup="ip">*</asp:RequiredFieldValidator><asp:CustomValidator
                                                        ID="CustomValidator3" runat="server" ClientValidationFunction="DateCustomValidate"
                                                        ControlToValidate="txtDeliveryDate" ErrorMessage="Please Enter the Delivery Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                                        SetFocusOnError="True" ValidationGroup="ip">*</asp:CustomValidator>&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 19px; text-align: right">
                                                <asp:Label ID="Label65" runat="server" Text="Color"></asp:Label></td>
                                            <td style="height: 19px; text-align: left">
                                                <asp:DropDownList ID="ddlColor" runat="server">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" InitialValue="0"
                                                    ControlToValidate="ddlColor" ErrorMessage="Please select the Color" ValidationGroup="item">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td style="height: 19px; text-align: right">
                                                <asp:Label ID="Label8" runat="server" Text="Sales"></asp:Label></td>
                                            <td style="height: 19px; text-align: left">
                                                <asp:DropDownList ID="ddlSales" runat="server">
                                                    <asp:ListItem>Sales</asp:ListItem>
                                                    <asp:ListItem>Extra</asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; height: 19px;"></td>
                                            <td style="text-align: right; height: 19px;">
                                                <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    CssClass="loginbutton" EnableTheming="False" Text="Add" ValidationGroup="item"
                                                    OnClick="btnAdd_Click" /></td>
                                            <td style="text-align: left; height: 19px;">
                                                <asp:Button ID="btnRefreshItems" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    CausesValidation="False" CssClass="loginbutton" EnableTheming="False" Text="Refresh"
                                                    OnClick="btnItemRefresh_Click" /></td>
                                            <td style="text-align: left; height: 19px;"></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 19px; text-align: right"></td>
                                            <td style="height: 19px; text-align: left"></td>
                                            <td style="height: 19px; text-align: right"></td>
                                            <td style="height: 19px; text-align: left"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <asp:GridView ID="gvSalesOrderItems" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    OnRowDeleting="gvSalesOrderItems_RowDeleting" OnRowDataBound="gvSalesOrderItems_RowDataBound"
                                                    OnRowEditing="gvSalesOrderItems_RowEditing" ShowFooter="True">
                                                    <FooterStyle BackColor="#1AA8BE" />
                                                    <Columns>
                                                        <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                                        <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                                        <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                                        <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                                                        <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                                        <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                                        <asp:BoundField DataField="Currency" HeaderText="Currency"></asp:BoundField>
                                                        <asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
                                                        <asp:BoundField HeaderText="UnitPrice"></asp:BoundField>
                                                        <asp:BoundField DataField="Price" HeaderText="Spl Price"></asp:BoundField>
                                                        <asp:BoundField DataField="Specifications" HeaderText="Specifications"></asp:BoundField>
                                                        <asp:BoundField DataFormatString="{0:dd/MM/YYYY}" DataField="DeliveryDate" HeaderText="Delivery Date">
                                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Room" HeaderText="Room"></asp:BoundField>
                                                        <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                                                        <asp:BoundField DataField="ColorId" HeaderText="Color Id"></asp:BoundField>
                                                        <asp:BoundField DataField="Sales" HeaderText="Sales"></asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>

                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4">CST &amp; TIN</td>
                    </tr>
                    <tr>
                        <td style="height: 5px; text-align: right"></td>
                        <td style="height: 5px; text-align: left"></td>
                        <td style="height: 5px; text-align: right"></td>
                        <td style="height: 5px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label66" runat="server" Text="CST No."></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCSTNo" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label68" runat="server" Text="TIN No."></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtTINNo" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4">Terms &amp; Conditions</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 23px;">
                            <asp:Label ID="Label5" runat="server" Text="Delivery"></asp:Label></td>
                        <td style="text-align: left; height: 23px;">
                            <asp:TextBox ID="txtDelivery" runat="server">
                            </asp:TextBox><asp:Label ID="Label40" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label>
                            <asp:RequiredFieldValidator ID="rfvDelivery" runat="server" ControlToValidate="txtDelivery"
                                ErrorMessage="Please Enter the Delivery" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right; height: 23px;">
                            <asp:Label ID="Label28" runat="server" Text="Currency Type"></asp:Label></td>
                        <td style="text-align: left; height: 23px;">
                            <asp:DropDownList ID="ddlCurrencyType" runat="server">
                            </asp:DropDownList>&nbsp;<asp:Label ID="Label63" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCurrencyType"
                                ErrorMessage="Please Enter the Currency " InitialValue="0" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label19" runat="server" Text="packing charges" Width="98px"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPackingCharges" runat="server">0</asp:TextBox><asp:Label ID="Label41" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label>
                            <asp:RequiredFieldValidator ID="rfvPackingChrgs" runat="server" ControlToValidate="txtPackingCharges"
                                ErrorMessage="Please Enter the Packing Charges" ValidationGroup="a">*</asp:RequiredFieldValidator>
                            <cc1:FilteredTextBoxExtender ID="ftxtePackingCharges" runat="server" FilterType="Numbers"
                                TargetControlID="txtPackingCharges">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label18" runat="server" Text="payment terms"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPaymentTerms" runat="server">
                            </asp:TextBox><asp:Label ID="Label42" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label>
                            <asp:RequiredFieldValidator ID="rfvPayTerms" runat="server" ControlToValidate="txtPaymentTerms"
                                ErrorMessage="Please Enter the Payment Terms" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left">
                            <asp:RadioButton ID="rbVAT" runat="server" Checked="True" GroupName="vatcst" Text="VAT" />
                            <asp:RadioButton ID="rbCST" runat="server" GroupName="vatcst" Text="C.S. Tax" />
                            <asp:RadioButton ID="rbIncluding" runat="server" GroupName="vatcst" Text="Including" onclick="Radio_Click()" />

                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="lblHighseaSale" runat="server">Sales Status</asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlsalestatus" runat="server">
                                <asp:ListItem Value="0">---</asp:ListItem>
                                <asp:ListItem Value="1">Normal</asp:ListItem>
                                <asp:ListItem Value="2">High Sale</asp:ListItem>
                            </asp:DropDownList>&nbsp;
                            <asp:Label ID="lblTotalAmt1" runat="server" Text="0" Visible="False"></asp:Label>
                            <asp:Label ID="lblTotalAmt2" runat="server" Text="0" Visible="False"></asp:Label>
                            <asp:Label ID="lblTotalamount" runat="server" Text="0" Visible="False"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblVATCST" runat="server" Text="VAT"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtVAT" runat="server" onkeypress="CheckNumeric(event);">
                            </asp:TextBox>
                            <asp:TextBox ID="txtInspection" runat="server" onkeypress="CheckNumeric(event);" Visible="False">
                            </asp:TextBox>
                            <asp:Label ID="Label50" runat="server" EnableTheming="True" Text="%"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtVAT"
                                ErrorMessage="Please Enter the VAT or C.S. Tax " ValidationGroup="a">*</asp:RequiredFieldValidator>
                            &nbsp;&nbsp;
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label7" runat="server" Text="Excise Dutry" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtExciseDuty" runat="server" Visible="False"></asp:TextBox><asp:Label ID="Label44" runat="server" EnableTheming="True" Text="%" Visible="False"></asp:Label><cc1:FilteredTextBoxExtender
                                ID="ftxteExciseDuty" runat="server" FilterType="Numbers" TargetControlID="txtExciseDuty">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label10" runat="server" Text="Guarantee"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtGuarantee" runat="server">
                            </asp:TextBox><asp:Label ID="Label45" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label>
                            <asp:RequiredFieldValidator ID="rfvGuarantee" runat="server" ControlToValidate="txtGuarantee"
                                ErrorMessage="Please Enter the Guarantee" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label9" runat="server" Text="Despatch Mode"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlDespatchMode" runat="server">
                            </asp:DropDownList><asp:Label ID="Label46" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label><asp:RequiredFieldValidator ID="rfvDespatchMode" runat="server" ControlToValidate="ddlDespatchMode"
                                    ErrorMessage="Please Enter the Despatch Mode" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label13" runat="server" Text="Insurance"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtInsurance" runat="server">
                            </asp:TextBox><asp:Label ID="Label47" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label>
                            <asp:RequiredFieldValidator ID="rfvInsurance" runat="server" ControlToValidate="txtInsurance"
                                ErrorMessage="Please Enter the Insurance" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label11" runat="server" Text="Transportation Charges" Width="143px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtTransCharges" runat="server">
                            </asp:TextBox><asp:Label ID="Label48" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label>
                            <asp:RequiredFieldValidator ID="rfvTransCharges" runat="server" ControlToValidate="txtTransCharges"
                                ErrorMessage="Please Enter the Transportation Charges" ValidationGroup="a">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label16" runat="server" Text="jurisdiction" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtJurisdiction" runat="server" Visible="False">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label14" runat="server" Text="Erection/Commisioning" Visible="False">
                            </asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtErrection" runat="server" Visible="False">
                            </asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label17" runat="server" Text="inspection" Visible="False"></asp:Label></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label15" runat="server" Text="validity" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtValidity" runat="server" Visible="False">
                            </asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label52" runat="server" Text="Advance Amt, If any"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtAdvanceAmt" runat="server">
                            </asp:TextBox><cc1:FilteredTextBoxExtender ID="ftxteAdvanceAmt" runat="server" TargetControlID="txtAdvanceAmt"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label69" runat="server" Text="DeliveryDate For All Items" Visible="False"
                                Width="168px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDeliveryDateForAll" runat="server" type="datepic" Visible="False">
                            </asp:TextBox>
                            <asp:Label ID="Label70" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator13"
                                    runat="server" ControlToValidate="txtDeliveryDateForAll" ErrorMessage="Please Enter the Delivery Date For All"
                                    Visible="False">*</asp:RequiredFieldValidator><asp:CustomValidator ID="CustomValidator4"
                                        runat="server" ClientValidationFunction="DateCustomValidate" ControlToValidate="txtDeliveryDateForAll"
                                        ErrorMessage="Please Enter the Delivery Date For All in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                        SetFocusOnError="True" Visible="False">*</asp:CustomValidator><asp:CompareValidator
                                            ID="CompareValidator1" runat="server" ControlToCompare="txtQuotationDate" ControlToValidate="txtDeliveryDateForAll"
                                            ErrorMessage="Delivery Date For All Should be greterthan Quotation Date" Operator="LessThanEqual"
                                            SetFocusOnError="True" Type="Date" Visible="False">*</asp:CompareValidator>
                        </td>
                    </tr>
                    <tr hidden="hidden">
                        <td style="text-align: right">
                            <asp:Label ID="Label43" runat="server" Text="Accessories "></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtAccessories" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="50%">0</asp:TextBox></td>
                    </tr>
                    <tr hidden="hidden">
                        <td style="text-align: right">
                            <asp:Label ID="Label49" runat="server" Text="Extra spares "></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtExtraSpares" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="50%">0</asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label6" runat="server" Text="Other Details"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtOtherSpecs" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="50%">-</asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left; height: 39px;">Follow Up Details (at customer place)</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblResponsiblePerson" runat="server" Text="Contact Name" Width="96px">
                            </asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtContactName1" runat="server" TabIndex="1">
                            </asp:TextBox><asp:Label ID="Label62" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtContactName1"
                                    ErrorMessage="Please Enter Contact Name" SetFocusOnError="True" ValidationGroup="a">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtContactName1" FilterMode="InvalidChars" InvalidChars="`1234567890-=+_)(*&^%$#@!~">
                                    </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label29" runat="server" Text="Contact Name" Width="98px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtContactName2" runat="server" TabIndex="5">
                            </asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtContactName2" FilterMode="InvalidChars" InvalidChars="`1234567890-=+_)(*&^%$#@!~">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="lblDesignation1" runat="server" Text="Designation"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:DropDownList ID="ddlDesignation1" runat="server" Width="154px" TabIndex="2">
                            </asp:DropDownList></td>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="lblDesignation2" runat="server" Text="Designation"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:DropDownList ID="ddlDesignation2" runat="server" Width="154px" TabIndex="6">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label33" runat="server" Text="Phone No" Width="73px"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:TextBox ID="txtPhone1" runat="server" TabIndex="3">
                            </asp:TextBox><asp:Label ID="Label64" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtPhone1"
                                    ErrorMessage="Please Enter Phone No." SetFocusOnError="True" ValidationGroup="a">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtPhone2"
                                        ValidChars="-0123456789">
                                    </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label32" runat="server" Text="Phone No" Width="71px"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:TextBox ID="txtPhone2" runat="server" TabIndex="7">
                            </asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtPhone1"
                                ValidChars="-0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label30" runat="server" Text="E-Mail" Width="96px"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:TextBox ID="txtEmail1" runat="server" TabIndex="4">
                            </asp:TextBox></td>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label31" runat="server" Text="E-Mail" Width="96px"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:TextBox ID="txtEmail2" runat="server" TabIndex="8">
                            </asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                ControlToValidate="txtEmail2" ErrorMessage="Please Enter  Valid Email" SetFocusOnError="True"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="a">*</asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label34" runat="server" Text="Consignment To" Width="108px"></asp:Label></td>
                        <td colspan="3" style="height: 19px; text-align: left">
                            <asp:TextBox ID="txtConsinment" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="50%">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label53" runat="server" Text="Invoice To" Width="108px"></asp:Label></td>
                        <td colspan="3" style="height: 19px; text-align: left">
                            <asp:TextBox ID="txtInvoiceTo" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="50%">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="lblFileNameHidden" runat="server"></asp:Label>
                            <asp:Label ID="Label51" runat="server" Text="Attachment"></asp:Label></td>
                        <td colspan="3" style="height: 19px; text-align: left">
                            <asp:FileUpload ID="Uploadattach" runat="server" AllowMultiple="true" />

                            <%--<cc2:FileUploaderAJAX ID="FileUploaderAJAX1" runat="server" BackColor="#E7F4F6" Directory_CreateIfNotExists="True" Font-Names="Verdana" Font-Size="8pt" showDeletedFilesOnPostBack="False" text_X="[Remove]" />
                                    <asp:HiddenField ID="HiddenField1" runat="server" />--%>

                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label56" runat="server" Visible="false" Text="Attached File"></asp:Label></td>
                        <td colspan="3" style="height: 19px; text-align: left">&nbsp;
                            <asp:LinkButton ID="lbtnAttachedFiles" runat="server" Visible="False"
                                OnClick="lbtnAttachedFiles_Click"></asp:LinkButton>
                            <asp:Repeater ID="UploadsRepeater" Visible="false" runat="server" DataSourceID="sdsUploads">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnFileOpener" CausesValidation="False" runat="server"
                                        OnClick="lbtnFileOpener_Click" Text='<%# Bind("SO_UPLOAD_FILENAME") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:SqlDataSource ID="sdsUploads" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                SelectCommand="SELECT * FROM [YANTRA_SO_UPLOADS] WHERE (SO_ID=@SO_IDpara) Order by SO_UPLOAD_ID Desc">
                                <SelectParameters>
                                    <asp:ControlParameter PropertyName="Text" DefaultValue="0" Name="SO_IDpara"
                                        ControlID="lblSOIdHidden"></asp:ControlParameter>
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:Label ID="lblSOIdHidden" runat="server" Visible="False"></asp:Label></td>

                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">Attachments :
                        </td>
                        <td colspan="3" style="height: 19px; text-align: left">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:Repeater ID="Repeater1" DataSourceID="sdsUploads" runat="server">
                                        <HeaderTemplate>
                                            <table cellspacing="0" rules="all" border="1">
                                                <tr>
                                                    <th scope="col" style="width: 120px">File Id
                                                    </th>
                                                    <th scope="col" style="width: 100px">File Name
                                                    </th>
                                                    <th scope="col" style="width: 80px"></th>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_So_Upload_Id" runat="server" Text='<%# Eval("SO_UPLOAD_ID") %>' Visible="true" />

                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lbtnFileOpener2" CausesValidation="False" runat="server"
                                                        OnClick="lbtnFileOpener2_Click" Text='<%# Bind("SO_UPLOAD_FILENAME") %>'></asp:LinkButton>

                                                    <%--<asp:Label ID="lblCountry" runat="server" Text='<%# Eval("Country") %>' />--%>
                                            
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lnkDelete" OnClientClick='javascript:return confirm("Are you sure you want to delete?")'  Text="Delete" runat="server" OnClick="lnkDelete_Click" CausesValidation="false" />


                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <%--<tr>
                        <td colspan="4">Attachements : 
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:DataList ID="DataList1" runat="server" CellPadding="10"
                                        DataKeyField="SO_UPLOAD_ID" DataSourceID="po_Attachments"
                                        RepeatColumns="2" OnDeleteCommand="DataList2_DeleteCommand"
                                        RepeatDirection="Horizontal" Width="100%">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "SO_UPLOAD_ID") %>
                                            <asp:LinkButton ID="lbtnFileOpener" CausesValidation="False" runat="server"
                                                OnClick="lbtnFileOpener_Click" Text='<%# Bind("SO_UPLOAD_FILENAME") %>'></asp:LinkButton>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="delete"> Delete </asp:LinkButton>

                                        </ItemTemplate>

                                    </asp:DataList>
                                    <asp:SqlDataSource ID="po_Attachments" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                        SelectCommand="SELECT * FROM [YANTRA_SO_UPLOADS] WHERE (SO_ID=@SO_IDpara)">
                                        <SelectParameters>
                                            <asp:ControlParameter PropertyName="Text" DefaultValue="0" Name="SO_IDpara"
                                                ControlID="lblSOIdHidden"></asp:ControlParameter>
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>--%>
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
                        <td style="text-align: right" class="auto-style1">
                            <asp:Label ID="lblCheckedBy" runat="server" Text="Checked By" Visible="False"></asp:Label></td>
                        <td style="text-align: left" class="auto-style1">
                            <asp:DropDownList ID="ddlCheckedBy" runat="server" Enabled="False" Visible="False">
                            </asp:DropDownList></td>
                        <td style="text-align: right" class="auto-style1"></td>
                        <td style="text-align: left" class="auto-style1"></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnNew" runat="server" CausesValidation="False" OnClick="btnNew_Click"
                                Text="New" Visible="False" Width="100px" />
                            <table id="tblButtons" align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Width="100px" ValidationGroup="a" /></td>
                                    <td>
                                        <asp:Button ID="btnApprove" runat="server" CausesValidation="False" OnClick="btnApprove_Click"
                                            Text="Approve" Width="100px" /></td>
                                    <td>
                                        <asp:Button ID="btnEdit" runat="server" CausesValidation="False" OnClick="btnEdit_Click"
                                            Text="Edit" Width="100px" Visible="true" /></td>
                                    <td>
                                        <asp:Button ID="btnDelete" runat="server" CausesValidation="False" OnClick="btnDelete_Click"
                                            Text="Delete" Width="100px" />
                                        <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" Width="100px" />
                                    </td>

                                </tr>
                                <tr>

                                    <td style="width: 3px">
                                        <asp:Button ID="btnAcceptence" runat="server" Text="Acceptence" OnClick="btnAcceptence_Click" Width="100px" /></td>
                                    <td>
                                        <asp:Button ID="btnSendWorkOrder" runat="server" CausesValidation="False" OnClick="btnSendWorkOrder_Click"
                                            Text="Internal Order" EnableTheming="True" Width="100px" /></td>

                                    <td>
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" Width="100px" /></td>
                                    <td>

                                        <asp:Button ID="btnsatte" runat="server" OnClick="btnsatte_Click" Text="statement" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click"
                                            CausesValidation="False" Width="100px" /></td>
                                    <td>
                                        <asp:Button ID="btnSend" runat="server" CausesValidation="False" OnClick="btnSend_Click"
                                            Text="Send" Width="100px" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:ValidationSummary ID="vs1" runat="server" ShowSummary="False"></asp:ValidationSummary>
                <asp:ValidationSummary ID="vs2" runat="server"
                    ValidationGroup="a" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>
                <asp:ValidationSummary ID="vs3" runat="server"
                    ValidationGroup="item" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>
                <asp:ValidationSummary ID="vs4" runat="server"
                    ValidationGroup="Search" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>
            </td>
        </tr>
        <tr>
            <td>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter Type="Int64" DefaultValue="0" Name="BranbId"></asp:Parameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtSearchModel"></asp:ControlParameter>
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
    </table>

    <table>
        <tr>
            <td>
                <asp:Label ID="lblUserName" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblUserId" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblCp_Ids" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                    Visible="False"></asp:Label>
                <asp:Label ID="lblEmpId" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                    Visible="False"></asp:Label>
                <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False">0</asp:Label>
                <asp:Label ID="lblSearchItemHidden"  Text="0" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchTypeHidden"  runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchValueHidden"  Text="0" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblCP_ID_confirm" runat="server" Visible="False"></asp:Label>

            </td>
        </tr>
    </table>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>




