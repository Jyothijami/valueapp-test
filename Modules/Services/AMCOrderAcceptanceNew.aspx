<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/ServicesMP1.master" AutoEventWireup="true" CodeFile="AMCOrderAcceptanceNew.aspx.cs" Inherits="Modules_Services_AMCOrderAcceptanceNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        $(function () {
            $("[name$='txtSODate'],[name$='txtOADate'],[name$='txtCallDate']").datepicker();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td style="text-align:left">AMC
                Order Confirmation</td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td colspan="4">
                <table border="0" cellpadding="0" cellspacing="0" id="tblOrderAcceptanceDetails" runat="server" visible="true" width="100%">
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead"></td>
                    </tr>

                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">General Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left; width: 200px;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblSalesOrderNo" runat="server" Text="AMC OP No" Width="103px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlSONo" runat="server" OnSelectedIndexChanged="ddlSONo_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:Label ID="Label6" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSONo"
                                ErrorMessage="Please Select the Sales Order No" InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblSalesOrderDate" runat="server" Text="AMC OP Date" Width="114px"></asp:Label></td>
                        <td style="text-align: left; width: 200px;">
                            <asp:TextBox ID="txtSODate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox>&nbsp;
                            
                            <cc1:MaskedEditExtender ID="meeSoDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtSODate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td id="TD18" style="height: 22px; text-align: right">
                            <asp:Label ID="lblCustomer" runat="server" Text="Customer"></asp:Label></td>
                        <td style="height: 22px; text-align: left">
                            <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="True" Enabled="False"
                                OnSelectedIndexChanged="ddlCustomerName_SelectedIndexChanged">
                            </asp:DropDownList><asp:Label ID="Label25" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCustomerName"
                                ErrorMessage="Please Select the Customer" InitialValue="0">*</asp:RequiredFieldValidator>
                            <asp:Label ID="lblAMCOIdHidden" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Black" Visible="False"></asp:Label></td>
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
                            <asp:DropDownList ID="ddlUnitName" runat="server" AutoPostBack="True" Enabled="False"
                                OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="0">--Select Customer--</asp:ListItem>
                            </asp:DropDownList><asp:RequiredFieldValidator ID="rfvUnitName" runat="server" ControlToValidate="ddlUnitName"
                                ErrorMessage="Please Select the Unit Name" InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblUnitAddress" runat="server" Text="Unit Address" Width="106px"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtUnitAddress" runat="server" CssClass="multilinetext" EnableTheming="False"
                                Font-Names="Verdana" Font-Size="8pt" ReadOnly="True" TextMode="MultiLine" Width="493px"></asp:TextBox></td>
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
                                InitialValue="0">*</asp:RequiredFieldValidator></td>
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
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label47" runat="server" Text="Mobile" Width="74px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">Ordered Items</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 21px">
                            <asp:GridView ID="gvSalesOrderItems" runat="server" AutoGenerateColumns="False"
                                Width="100%" OnRowDataBound="gvSalesOrderItems_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                    <asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
                                    <asp:BoundField HeaderText="Amount"></asp:BoundField>
                                    <asp:BoundField DataField="Specifications" HeaderText="Specifications"></asp:BoundField>
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4">Ordered Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left; width: 200px;"></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label1" runat="server" Text="Order Schedule No" Width="141px"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:TextBox ID="txtOANo" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label2" runat="server" Text=" Order Schedule  Date" Width="150px"></asp:Label></td>
                        <td style="height: 19px; text-align: left; width: 200px;">
                            <asp:TextBox ID="txtOADate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOADate"
                                ErrorMessage="Please Select the Order Schedule Date">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator5" runat="server" ClientValidationFunction="DateCustomValidate"
                                ControlToValidate="txtOADate" ErrorMessage="Please Enter the Order Schedule Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                SetFocusOnError="True">*</asp:CustomValidator>
                       
                            <cc1:MaskedEditExtender ID="meeOADate" runat="server" DisplayMoney="Left"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtOADate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4">Order Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left; width: 200px;"></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="lblVisits" runat="server" Text="No  Of  PM Visits"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:TextBox ID="txtPmVisits" runat="server">
                            </asp:TextBox></td>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label31" runat="server" Text="No. Of BD Calls"></asp:Label></td>
                        <td style="width: 200px; height: 19px; text-align: left">
                            <asp:TextBox ID="txtBDCalls" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label ID="lblSchedule" runat="server" Text="PM Visit Schedule"></asp:Label></td>
                        <td colspan="3" style="height: 24px; text-align: left" valign="middle">
                            <asp:TextBox ID="txtPMSchedule" runat="server" TextMode="MultiLine" Width="92%" CssClass="multilinetext" EnableTheming="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label ID="lblPayment" runat="server" Text="Payment"></asp:Label></td>
                        <td colspan="3" style="height: 24px; text-align: left" valign="middle">
                            <asp:TextBox ID="txtPayment" runat="server" TextMode="MultiLine" Width="92%" CssClass="multilinetext" EnableTheming="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label ID="lblPaymentStatus" runat="server" Text="Payment Status"></asp:Label></td>
                        <td colspan="3" style="height: 24px; text-align: left" valign="middle">
                            <asp:TextBox ID="txtPaymentStatus" runat="server" TextMode="MultiLine" Width="92%" CssClass="multilinetext" EnableTheming="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblInvoiceStatus" runat="server" Text="Invoice Status" Visible="False"></asp:Label></td>
                        <td colspan="3" style="text-align: left" valign="middle">
                            <asp:TextBox ID="txtInvoiceStatus" runat="server" TextMode="MultiLine" Width="92%" CssClass="multilinetext" EnableTheming="False" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblInvoiceDetails" runat="server" Text="Invoice Details" Visible="False"></asp:Label></td>
                        <td colspan="3" style="text-align: left" valign="middle">
                            <asp:TextBox ID="txtInvoiceDetails" runat="server" TextMode="MultiLine" Width="92%" CssClass="multilinetext" EnableTheming="False" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="width: 200px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label35" runat="server" Text="AMC Amount"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtAmcAmt" runat="server" ReadOnly="True"></asp:TextBox><asp:Label
                                ID="Label36" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtAmcAmt" ErrorMessage="Please Enter the AMC Amount">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right"></td>
                        <td style="width: 200px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label15" runat="server" Text="Service Tax"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCST" runat="server">
                            </asp:TextBox><asp:Label ID="Label17" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                    ID="rfvCST" runat="server" ControlToValidate="txtCST" ErrorMessage="Please Enter the Service Tax">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label33" runat="server" Text="Total Amount"></asp:Label></td>
                        <td style="text-align: left; width: 200px;">
                            <asp:TextBox ID="txtAmcTotAmt" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">PM Calls Schedule</td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label32" runat="server" Text="Call Name"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCallName" runat="server">
                            </asp:TextBox><asp:Label ID="Label34" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtCallName" ErrorMessage="Please Enter the PM Calls"
                                    ValidationGroup="call">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label37" runat="server" Text="Call Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCallDate" runat="server" CssClass="datetext" EnableTheming="False">
                            </asp:TextBox>
                           
                            <asp:Label
                                ID="Label38" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtCallDate" ErrorMessage="Please Enter the Call Date"
                                    ValidationGroup="call">*</asp:RequiredFieldValidator>
                            
                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" DisplayMoney="Left"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtCallDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" OnClick="btnAdd_Click" Text="Add"
                                ValidationGroup="call" /><asp:Button ID="btnItemRefresh" runat="server" BackColor="Transparent"
                                    BorderStyle="None" CausesValidation="False" CssClass="loginbutton" EnableTheming="False"
                                    OnClick="btnItemRefresh_Click" Text="Refresh" /></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="gvQuotationItems" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvQuotationItems_RowDataBound"
                                OnRowDeleting="gvQuotationItems_RowDeleting" OnRowEditing="gvQuotationItems_RowEditing">
                                <Columns>
                                    <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                    <asp:BoundField DataField="callname" HeaderText="Call Name"></asp:BoundField>
                                    <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="calldate" HeaderText="Call Date"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">Consignee Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left; width: 200px;"></td>
                    </tr>
                    <tr>
                        <td style="height: 22px; text-align: right">
                            <asp:Label ID="Label7" runat="server" Text="Consigneement To"></asp:Label></td>
                        <td colspan="3" style="height: 22px; text-align: left">
                            <asp:TextBox ID="txtConsignee" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="92%"></asp:TextBox><asp:Label ID="Label30" runat="server" EnableTheming="False" Font-Bold="False"
                                    Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                        runat="server" ControlToValidate="txtConsignee" ErrorMessage="Please Enter the Consignee Details">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">follow Up &nbsp;Details &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left; width: 200px;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblResponsiblePerson" runat="server" Text="Responsible Person" Width="134px">
                            </asp:Label></td>
                        <td style="width: 269px; text-align: left">
                            <asp:TextBox ID="txtResponsiblePerson" runat="server"></asp:TextBox>
                            <asp:Label ID="Label49" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvResPerson" runat="server" ControlToValidate="txtResponsiblePerson"
                                ErrorMessage="Please Enter the Responsible Person" InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblFollowupEmail" runat="server" Text="Email"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtFollowupEmail" runat="server"></asp:TextBox>
                            <asp:Label ID="Label50" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtFollowupEmail"
                                    ErrorMessage="Please Enter the Responsible Person Email" InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left; width: 200px;"></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">Reference Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left; width: 200px;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label4" runat="server" Text="Approved By"></asp:Label></td>
                        <td style="text-align: left; width: 200px;">
                            <asp:DropDownList ID="ddlApprovedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="lblCheckedBy" runat="server" Text="Checked By" Visible="False"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:DropDownList ID="ddlCheckedBy" runat="server" Enabled="False" Visible="False">
                            </asp:DropDownList></td>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left; width: 200px;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;"></td>
                        <td style="text-align: left; height: 19px;"></td>
                        <td style="text-align: right; height: 19px;"></td>
                        <td style="text-align: left; height: 19px; width: 200px;"></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 47px">
                            <table id="tblButtons">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" /></td>
                                    <td>
                                        <asp:Button ID="btnApprove" runat="server" CausesValidation="False" OnClick="btnApprove_Click"
                                            Text="Approve" /></td>
                                    <td>
                                        <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" Text="Refresh" CausesValidation="False" /></td>
                                    <td>
                                        <asp:Button ID="btnSend" runat="server" CausesValidation="False" OnClick="btnSend_Click"
                                            Text="Send" /></td>
                                    <td>
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" CausesValidation="False" OnClick="btnPrint_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" CausesValidation="False" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0" id="Table2" runat="server" visible="false" width="100%">
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label10" runat="server" Text="Delivery"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDelivery" runat="server">
                            </asp:TextBox>
                            <asp:Label ID="Label8" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvDelivery" runat="server" ControlToValidate="txtDelivery"
                                ErrorMessage="Please Enter the Delivery">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label5" runat="server" Text="currency type"></asp:Label></td>
                        <td style="text-align: left; width: 200px;">
                            <asp:DropDownList ID="ddlCurrencyType" runat="server">
                            </asp:DropDownList>
                            <asp:Label ID="Label14" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCurrencyType"
                                ErrorMessage="Please Select the Currency Type" InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label12" runat="server" Text="packing charges"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPackingCharges" runat="server">
                            </asp:TextBox>
                            <asp:Label ID="Label9" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvPackingChrgs" runat="server" ControlToValidate="txtPackingCharges"
                                ErrorMessage="Please Enter the Packing Charges">*</asp:RequiredFieldValidator>
                            <cc1:FilteredTextBoxExtender ID="ftxtePackingCharges" runat="server" FilterType="Numbers"
                                TargetControlID="txtPackingCharges">
                            </cc1:FilteredTextBoxExtender>
                            <asp:DropDownList ID="ddlSalesPerson" runat="server">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label11" runat="server" Text="payment terms"></asp:Label></td>
                        <td style="text-align: left; width: 200px;">
                            <asp:TextBox ID="txtPaymentTerms" runat="server">
                            </asp:TextBox>
                            <asp:Label ID="Label18" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvPayTerms" runat="server" ControlToValidate="txtPaymentTerms"
                                ErrorMessage="Please Enter the Payment Terms">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left">&nbsp;
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label13" runat="server" Text="Excise Duty"></asp:Label></td>
                        <td style="text-align: left; width: 200px;">
                            <asp:TextBox ID="txtExciseDuty" runat="server">
                            </asp:TextBox>
                            <asp:Label ID="Label19" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvExciseDuty" runat="server" ControlToValidate="txtExciseDuty"
                                ErrorMessage="Please Enter the Excise Duty">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label20" runat="server" Text="Guarantee"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtGuarantee" runat="server">
                            </asp:TextBox>
                            <asp:Label ID="Label22" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvGuarantee" runat="server" ControlToValidate="txtGuarantee"
                                ErrorMessage="Please Enter the Guarantee">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label16" runat="server" Text="Despatch Mode"></asp:Label></td>
                        <td style="text-align: left; width: 200px;">
                            <asp:DropDownList ID="ddlDespatchMode" runat="server">
                            </asp:DropDownList>
                            <asp:Label ID="Label21" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvDespatchMode" runat="server"
                                ControlToValidate="ddlDespatchMode" ErrorMessage="Please Select the Despatch Mode"
                                InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label24" runat="server" Text="Erection/Commissioning"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtErrection" runat="server">
                            </asp:TextBox>
                            <asp:Label ID="Label23" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvErrection" runat="server" ControlToValidate="txtErrection"
                                ErrorMessage="Please Enter the Erection/Commissioning">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label27" runat="server" Text="Inspection"></asp:Label></td>
                        <td style="text-align: left; width: 200px;">
                            <asp:TextBox ID="txtInspection" runat="server">
                            </asp:TextBox>
                            <asp:Label ID="Label29" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvInspection" runat="server" ControlToValidate="txtInspection"
                                ErrorMessage="Please Enter the Inspection">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label28" runat="server" Text="Other specifications"></asp:Label></td>
                        <td colspan="3" style="text-align: left" valign="middle">
                            <asp:TextBox ID="txtOtherSpecs" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="94%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False"></asp:ValidationSummary>
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td style="text-align: right;"></td>
            <td style="width: 231px"></td>
        </tr>
    </table>
</asp:Content>


 
