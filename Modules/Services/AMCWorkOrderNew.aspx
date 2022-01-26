<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/ServicesMP1.master" AutoEventWireup="true" CodeFile="AMCWorkOrderNew.aspx.cs" Inherits="Modules_Services_AMCWorkOrderNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        $(function () {
            $("[name$='txtOADate'],[name$='txtWODate'],[name$='txtInspectionDate'],[name$='txtCallDate'],[name$='txtDeliveryDate']").datepicker();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td style="text-align:left">AMC
               Order Profile</td>
        </tr>
    </table>
    <table>
        <tr>
            <td colspan="4">
                <table border="0" cellpadding="0" cellspacing="0" id="tblWorkOrderDetails" runat="server"
                    width="100%" visible="true">
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
                        <td style="height: 19px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblSalesOrderNo" runat="server" Text="AMC Order No"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlOrderAcceptance" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOrderAcceptance_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label ID="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlOrderAcceptance"
                                ErrorMessage="Please Select The Order Acceptance No." InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblSalesOrderDate" runat="server" Text="AMC Order Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtOADate" runat="server" ReadOnly="True" CssClass="datetext" EnableTheming="False"></asp:TextBox>&nbsp;
                        
                            <cc1:MaskedEditExtender ID="meeOADate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtOADate" UserDateFormat="MonthDayYear">
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
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlCustomerName"
                                ErrorMessage="Please Select the Customer" InitialValue="0">*</asp:RequiredFieldValidator></td>
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
                        <td colspan="4" style="text-align: left" class="profilehead">Ordered Items</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 21px">
                            <asp:GridView ID="gvOrderAcceptanceItems" runat="server" AutoGenerateColumns="False"
                                Width="100%" OnRowDataBound="gvOrderAcceptanceItems_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Rate" HeaderText="Rate">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Amount">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Specifications" HeaderText="Specifications">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Priority" HeaderText="Priority">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4">Work Order Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label1" runat="server" Text="Work Order  No" Width="141px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtWONo" runat="server"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label2" runat="server" Text="Work  Order  Date" Width="150px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtWODate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox>
                            <asp:Label ID="Label8" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator
                                ID="rfvWODate" runat="server" ControlToValidate="txtWODate" ErrorMessage="Please Select the Work Order Date">*</asp:RequiredFieldValidator>
                            
                            <cc1:MaskedEditExtender ID="meeWODate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtWODate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label15" runat="server" Text="Service Tax"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCST" runat="server">
                            </asp:TextBox><asp:Label ID="Label11" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="rfvCST" runat="server" ControlToValidate="txtCST"
                                    ErrorMessage="Please Enter the Service Tax">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label6" runat="server" Text="Inspection Date"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtInspectionDate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox>
                            
                            <cc1:MaskedEditExtender ID="meeeInspectionDate" runat="server" DisplayMoney="Left"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtInspectionDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">&nbsp;<asp:Label ID="Label3" runat="server" Text="PM Calls"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPMCalls" runat="server"></asp:TextBox><asp:Label ID="Label5"
                                runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana" Font-Size="Smaller"
                                ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                    runat="server" ControlToValidate="txtPMCalls" ErrorMessage="Please Enter the PM Calls">*</asp:RequiredFieldValidator>
                            <cc1:FilteredTextBoxExtender ID="ftxteRate" runat="server" TargetControlID="txtPMCalls"
                                ValidChars="0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label4" runat="server" Text="BD Calls"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtBDCalls" runat="server"></asp:TextBox><asp:Label ID="Label9"
                                runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana" Font-Size="Smaller"
                                ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                    runat="server" ControlToValidate="txtBDCalls" ErrorMessage="Please Enter the Break Down Calls">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
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
                            <asp:Label ID="Label10" runat="server" Text="Call Name"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCallName" runat="server">
                            </asp:TextBox><asp:Label ID="Label13"
                                runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana" Font-Size="Smaller"
                                ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                    runat="server" ControlToValidate="txtCallName" ErrorMessage="Please Enter the PM Calls" ValidationGroup="call">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label14" runat="server" Text="Call Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCallDate" runat="server" CssClass="datetext" EnableTheming="False">
                            </asp:TextBox>
                            
                            <asp:Label ID="Label16"
                                runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana" Font-Size="Smaller"
                                ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                    runat="server" ControlToValidate="txtCallDate" ErrorMessage="Please Enter the Call Date" ValidationGroup="call">*</asp:RequiredFieldValidator>
                            
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
                        <td class="profilehead" colspan="4" style="text-align: left">Reference Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 22px;">
                            <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left; height: 22px;">
                            <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False" EnableTheming="True">
                            </asp:DropDownList></td>
                        <td style="text-align: right; height: 22px;">
                            <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By"></asp:Label></td>
                        <td style="text-align: left; height: 22px;">
                            <asp:DropDownList ID="ddlApprovedBy" runat="server" Enabled="False" EnableTheming="True">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="lblCheckedBy" runat="server" Text="Checked By" Visible="False"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:DropDownList ID="ddlCheckedBy" runat="server" EnableTheming="False" Visible="False">
                            </asp:DropDownList></td>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;"></td>
                        <td style="text-align: left; height: 19px;"></td>
                        <td style="text-align: right; height: 19px;"></td>
                        <td style="text-align: left; height: 19px;"></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table id="tblButtons">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnApprove" runat="server" CausesValidation="False" OnClick="btnApprove_Click"
                                            Text="Approve" /></td>
                                    <td>
                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CausesValidation="False"
                                            OnClick="btnRefresh_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnSend" runat="server" CausesValidation="False" OnClick="btnSend_Click"
                                            Text="Send" Visible="False" /></td>
                                    <td>
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" CausesValidation="False"
                                            OnClick="btnPrint_Click" Visible="False" /></td>
                                    <td>
                                        <asp:Button ID="btnClose" runat="server" Text="Close" CausesValidation="False" OnClick="btnClose_Click" /></td>
                                </tr>
                            </table>
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="call"></asp:ValidationSummary>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px"></td>
                        <td style="height: 19px"></td>
                        <td style="text-align: right; height: 19px;"></td>
                        <td style="height: 19px"></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td style="text-align: right"></td>
                        <td></td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0" id="Table2" runat="server"
                    width="100%" visible="false">
                    <tr>
                        <td colspan="4">
                            <asp:DropDownList ID="ddlDeliveryMode" runat="server" Visible="False">
                            </asp:DropDownList><asp:TextBox ID="txtDeliveryDate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox>
                           
                            <asp:TextBox ID="txtPackingInstrs" runat="server" CssClass="multilinetext" EnableTheming="False" TextMode="MultiLine"></asp:TextBox><asp:TextBox ID="txtAccessories" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine"></asp:TextBox><asp:TextBox ID="txtExtraSpace" runat="server" CssClass="multilinetext" EnableTheming="False"
                                    TextMode="MultiLine"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>


 
