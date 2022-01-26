<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="QuotationComparison.aspx.cs" Inherits="Modules_PurchasingManagement_QuotationComparison" Title="|| YANTRA : Purchasing Management : Quotation Comparison ||" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                Quotation COMPARISON</td>
        </tr>
    </table>
    <table style="width: 88px" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td id="Td20" style="height: 19px">
            </td>
            <td style="height: 19px">
            </td>
            <td style="height: 19px">
            </td>
            <td style="height: 19px">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: left" class="profilehead">
                General Details</td>
        </tr>
        <tr>
            <td style="height: 21px; text-align: right">
            </td>
            <td style="height: 21px; text-align: left">
            </td>
            <td style="height: 21px; text-align: left">
            </td>
            <td style="height: 21px; text-align: left">
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label id="lblQCFactor" runat="server" Text="QC Factor"></asp:Label></td>
            <td style="text-align: left">
                <asp:RadioButton id="RadioButton1" runat="server" Text="Rate">
                </asp:RadioButton></td>
            <td style="text-align: left">
                <asp:RadioButton id="RadioButton2" runat="server" Text="Delivery Time">
                </asp:RadioButton></td>
            <td style="text-align: left">
                <asp:RadioButton id="RadioButton3" runat="server" Text="Payment Terms">
                </asp:RadioButton></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label id="lblPOEnquiryNo" runat="server" Text="PO Enquiry No" Width="119px"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList id="DropDownList2" runat="server">
                </asp:DropDownList></td>
            <td style="text-align: right">
                <asp:Label ID="Label2" runat="server" Text="Enquiry Date"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="Image1"
                    runat="server" ImageUrl="~/Images/Calendar.png" /></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label id="lblQComNo" runat="server" Text="QCom. No"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtQComNo" runat="server"></asp:TextBox></td>
            <td style="text-align: right"><asp:Label id="lblQuotationDate" runat="server" Text="Quo.Com. Date" Width="152px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtQuotationDate" runat="server"></asp:TextBox>&nbsp;<asp:Image id="Image2" runat="server" ImageUrl="~/Images/Calendar.png"
                    >
                </asp:Image></td>
        </tr>
        <tr>
            <td style="text-align: right"></td>
            <td style="text-align: left">
                </td>
            <td style="text-align: right">
                <asp:Label id="lblQuComparison" runat="server" Text="Quo. Comparison" Width="129px"></asp:Label></td>
            <td style="text-align: left">
                <asp:Button id="btnAutomotive" runat="server" Text="Automotive" />&nbsp;<asp:Button
                    id="btnMaterial" runat="server" Text="Material" /></td>
        </tr>
        <tr>
            <td style="text-align: right">
                </td>
            <td style="text-align: left">
                </td>
            <td style="text-align: right"></td>
            <td style="text-align: left"></td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: left" class="profilehead">
                &nbsp;Enquiry Items List</td>
        </tr>
        <tr>
            <td colspan="4" style="height: 21px">
                <table border="0" cellpadding="0" cellspacing="0" style="font-weight: normal; font-size: 8pt;
                    color: black; font-family: Verdana; text-align: left; text-decoration: none"
                    width="100%">
                    <tr>
                        <td style="background-color: #1aa8be; height: 20px;">
                            <strong><span style="color: #ffffff">Item Name</span></strong></td>
                        <td style="background-color: #1aa8be; height: 20px;">
                            <strong><span style="color: #ffffff">Enquiry No.</span></strong></td>
                        <td style="background-color: #1aa8be; height: 20px;">
                            <strong><span style="color: white">Enquiry Date</span></strong></td>
                        <td style="background-color: #1aa8be; height: 20px;">
                            <strong><span style="color: white">Quantity</span></strong></td>
                        <td style="background-color: #1aa8be; height: 20px;">
                            <strong><span style="color: white">Rate</span></strong></td>
                        <td style="height: 20px">
                            <strong>&nbsp;&nbsp; Supplier List:</strong></td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </td>
                        <td style="background-color: #eff3fb">
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </td>
                        <td style="background-color: #eff3fb">
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp;&nbsp;
                        </td>
                        <td style="background-color: #eff3fb">
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                        </td>
                        <td style="background-color: #eff3fb">
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                        </td>
                        <td>
                            &nbsp;
                            <asp:DropDownList id="DropDownList1" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr style="font-size: 8pt">
                        <td style="background-color: white">
                            &nbsp;</td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td>
                            &nbsp;
                            <asp:Button id="Button1" runat="server" Text="Generate PO" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 21px">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 21px; text-align: left" class="profilehead">
                Quotation List For</td>
        </tr>
        <tr>
            <td colspan="4" style="height: 21px">
                <table border="0" cellpadding="4" cellspacing="0" style="font-weight: normal; font-size: 8pt;
                    color: black; font-family: Verdana; text-align: left; text-decoration: none"
                    width="100%">
                    <tr>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Item Name</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Supplier Name</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: white">UOM</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: white">Rate</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: white">Per</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: white">Min Del Period</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: white">Max Del Period</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Dis Rate</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Dis Type</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Taxes</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Taxes Type</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: #ffffff">P &amp; F Rate</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Exc Rate</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Transport</span></strong></td>
                        <td style="width: 84px; background-color: #1aa8be">
                            <strong><span style="color: white">Pay Terms</span></strong></td>
                        <td style="width: 84px; background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Control</span></strong></td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                            &nbsp;</td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="width: 84px; background-color: #eff3fb; text-align: center">
                        </td>
                        <td style="width: 84px; background-color: #eff3fb; text-align: center">
                        </td>
                    </tr>
                    <tr style="font-size: 8pt">
                        <td style="background-color: white">
                            &nbsp;</td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="width: 84px; background-color: white; text-align: center">
                        </td>
                        <td style="width: 84px; background-color: white; text-align: center">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 21px; text-align: left" class="profilehead">
                PO Item List</td>
        </tr>
        <tr>
            <td colspan="4">
                <table border="0" cellpadding="4" cellspacing="0" style="font-weight: normal; font-size: 8pt;
                    color: black; font-family: Verdana; text-align: left; text-decoration: none"
                    width="100%">
                    <tr>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Vendor Name</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Item Name</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: white">Item Rate</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: white">Per</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: white">Order Qty</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: white">Gross Amount</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: white">Specification</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Delivery Perios</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Control</span></strong></td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                            &nbsp;</td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                    </tr>
                    <tr style="font-size: 8pt">
                        <td style="background-color: white">
                            &nbsp;</td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; height: 19px;">
            </td>
            <td style="height: 19px">
            </td>
            <td style="height: 19px">
            </td>
            <td style="height: 19px">
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table id="tblButtons">
                    <tr>
                        <td>
                            <asp:Button id="btnSave" runat="server" Text="Save" /></td>
                        <td>
                            <asp:Button id="btnCancel" runat="server" Text="Cancel" /></td>
                        <td>
                            <asp:Button id="btnDelete" runat="server" Text="Delete" /></td>
                        <td>
                            <asp:Button id="btnPrint" runat="server" Text="Print" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 21px">
            </td>
            <td style="height: 21px">
            </td>
            <td style="height: 21px;">
            </td>
            <td style="height: 21px">
            </td>
        </tr>
    </table>
</asp:Content>

 
