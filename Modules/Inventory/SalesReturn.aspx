<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SalesReturn.aspx.cs" Inherits="Modules_Inventory_SalesReturn" Title="|| YANTRA : Inventory : Sales Return ||" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                Sales Return</td>
        </tr>
    </table>
    <br />  
    <table style="width: 88px" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="4" style="text-align: left" class="profilehead">
                General Details</td>
        </tr>
        <tr>
            <td style="height: 21px; text-align: right">
            </td>
            <td style="height: 21px; text-align: left">
            </td>
            <td style="height: 21px; text-align: right">
            </td>
            <td style="height: 21px; text-align: left">
            </td>
        </tr>
        <tr>
            <td style="text-align: right; height: 24px;">
                <asp:Label id="lblSRVNo" runat="server" Text="SRV No"></asp:Label></td>
            <td style="text-align: left; height: 24px;">
                <asp:TextBox id="txtSRVNo" runat="server"></asp:TextBox></td>
            <td style="text-align: right; height: 24px;"><asp:Label id="lblSRVDate" runat="server" Text="SRV Date" Width="152px"></asp:Label></td>
            <td style="text-align: left; height: 24px;">
                <asp:TextBox id="txtSRVDate" runat="server"></asp:TextBox>&nbsp;<asp:Image id="Image2" runat="server" ImageUrl="~/Images/Calendar.png"
                    >
                </asp:Image></td>
        </tr>
        <tr>
            <td style="text-align: right"><asp:Label id="lblChallanNo" runat="server" Text="Challan No" Width="119px"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList id="ddlChallanNo" runat="server">
                </asp:DropDownList></td>
            <td style="text-align: right">
                <asp:Label id="lblChallanDate" runat="server" Text="Challan Date" Width="103px"></asp:Label></td>
            <td style="text-align: left"><asp:TextBox id="txtChallanDate" runat="server">
            </asp:TextBox>&nbsp;<asp:Image id="Image1" runat="server" ImageUrl="~/Images/Calendar.png"
                    >
                </asp:Image></td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label id="lblInvoiceNo" runat="server" Text="Invoice No" Width="119px"></asp:Label></td>
            <td style="text-align: left;">
                <asp:DropDownList id="ddlInvoiceNo" runat="server">
                </asp:DropDownList></td>
            <td style="text-align: right;"><asp:Label id="lblInvoiceDate" runat="server" Text="Invoice Date" Width="94px"></asp:Label></td>
            <td style="text-align: left;"><asp:TextBox id="txtInvoiceDate" runat="server">
            </asp:TextBox>&nbsp;<asp:Image id="Image3" runat="server" ImageUrl="~/Images/Calendar.png"
                    >
            </asp:Image></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label id="lblCustomer" runat="server" Text="Customer" Width="119px"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList id="ddlCustomer" runat="server">
                </asp:DropDownList></td>
            <td style="text-align: right"><asp:Label id="lblCustomerLocation" runat="server" Text="Customer Location" Width="129px">
            </asp:Label></td>
            <td style="text-align: left"><asp:DropDownList id="ddlCustomerLocation" runat="server">
            </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="text-align: right"><asp:Label id="lblEPNo" runat="server" Text="Ep No" Width="73px"></asp:Label></td>
            <td style="text-align: left"><asp:TextBox id="txtEPNo" runat="server"></asp:TextBox></td>
            <td style="text-align: right"><asp:Label id="lblEPDate" runat="server" Text="EP Date" Width="73px"></asp:Label></td>
            <td style="text-align: left"><asp:TextBox id="txtEPDate" runat="server"></asp:TextBox>&nbsp;<asp:Image id="Image4" runat="server" ImageUrl="~/Images/Calendar.png"
                    >
            </asp:Image></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label id="lblDespatchMode" runat="server" Text="Despatch Mode" Width="107px"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList id="ddlDespatchMode" runat="server">
                </asp:DropDownList></td>
            <td style="text-align: right">
                <asp:Label id="lblFlightShipmentNo" runat="server" Text="LR/Flight Shipment No" Width="150px">
                </asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="TXTFlightShipmentNo" runat="server">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">
            </td>
            <td style="text-align: left">
            </td>
            <td style="text-align: right">
            </td>
            <td style="text-align: left">
            </td>
        </tr>
        <tr>
            <td class="profilehead" colspan="4" style="height: 20px; text-align: left">
                Item Details</td>
        </tr>
        <tr>
            <td style="height: 19px; text-align: right">
            </td>
            <td style="height: 19px; text-align: left">
            </td>
            <td style="height: 19px; text-align: right">
            </td>
            <td style="height: 19px; text-align: left">
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label id="Label1" runat="server" Text="Item Code" Width="73px"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList id="DropDownList1" runat="server">
                </asp:DropDownList></td>
            <td style="text-align: right">
                <asp:Label id="Label6" runat="server" Text="Item Type" Width="73px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="TextBox8" runat="server">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label id="Label2" runat="server" Text="Invoice Qty" Width="73px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="TextBox6" runat="server">
                </asp:TextBox></td>
            <td style="text-align: right">
                <asp:Label id="Label7" runat="server" Text="UOM" Width="73px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="TextBox7" runat="server">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label id="Label4" runat="server" Text="Received Qty" Width="96px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="TextBox2" runat="server">
                </asp:TextBox></td>
            <td style="text-align: right">
                <asp:Label id="Label8" runat="server" Text="Rate" Width="73px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="TextBox4" runat="server">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label id="Label5" runat="server" Text="Total Amount" Width="101px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="TextBox3" runat="server">
                </asp:TextBox></td>
            <td style="text-align: right">
                <asp:Label id="Label9" runat="server" Text="Reason" Width="73px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="TextBox5" runat="server">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label id="Label10" runat="server" Text="Remarks" Width="73px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="TextBox9" runat="server">
                </asp:TextBox></td>
            <td style="text-align: right">
            </td>
            <td style="text-align: left">
            </td>
        </tr>
        <tr>
            <td style="height: 19px; text-align: right">
            </td>
            <td style="height: 19px; text-align: left">
            </td>
            <td style="height: 19px; text-align: right">
            </td>
            <td style="height: 19px; text-align: left">
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button id="Button5" runat="server" BackColor="Transparent" BorderStyle="None"
                    CssClass="loginbutton" EnableTheming="False" Text="Add" /><asp:Button id="Button6" runat="server" BackColor="Transparent" BorderStyle="None"
                    CssClass="loginbutton" EnableTheming="False" Text="Refresh" /></td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: left">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="4" style="height: 21px">
                <table border="0" cellpadding="4" cellspacing="0" style="font-weight: normal; font-size: 8pt;
                    color: black; font-family: Verdana; text-align: left; text-decoration: none"
                    width="750">
                    <tr>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Item Code</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Item Type</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: white">Invoice Qty.</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: #ffffff">UOM</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Received Qty.</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Rate</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Total Amt</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Reason</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: white">Remarks</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: white">Action</span></strong></td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                            PR00857</td>
                        <td style="background-color: #eff3fb">
                            SOCB SCHOLAR</td>
                        <td style="background-color: #eff3fb">
                            200</td>
                        <td style="background-color: #eff3fb">
                            NOS</td>
                        <td style="background-color: #eff3fb">
                            100</td>
                        <td style="background-color: #eff3fb">
                            540</td>
                        <td style="background-color: #eff3fb">
                            54000</td>
                        <td style="background-color: #eff3fb">
                            </td>
                        <td style="background-color: #eff3fb">
                            </td>
                        <td style="background-color: #eff3fb">
                            <asp:Button id="Button10" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" Text="Del" /></td>
                    </tr>
                </table>
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp;
                <asp:Label id="Label3" runat="server" Text="Total Amount" Width="96px"></asp:Label><asp:TextBox
                    id="TextBox1" runat="server">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label id="lblRemarks" runat="server" Text="Remarks" Width="73px"></asp:Label></td>
            <td colspan="3" style="text-align: left">
                <asp:TextBox id="txtRemarks" runat="server" TextMode="MultiLine" Width="451px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">
            </td>
            <td style="text-align: left">
            </td>
            <td style="text-align: right">
            </td>
            <td style="text-align: left">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: left" class="profilehead">
                Reference Details</td>
        </tr>
        <tr>
            <td style="height: 21px; text-align: right">
            </td>
            <td style="height: 21px; text-align: left">
            </td>
            <td style="height: 21px; text-align: right">
            </td>
            <td style="height: 21px; text-align: left">
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label id="lblApprovedBy" runat="server" Text="Approved By" Width="96px"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList id="ddlApprovedBy" runat="server">
                </asp:DropDownList></td>
            <td style="text-align: right;">
                <asp:Label id="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList id="ddlPreparedBy" runat="server">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label id="lblCheckedBy" runat="server" Text="Checked By" Width="96px"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList id="ddlCheckedBy" runat="server">
                </asp:DropDownList></td>
            <td style="text-align: right">
                </td>
            <td style="text-align: left">
                </td>
        </tr>
        <tr>
            <td style="text-align: right">
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <br />
                <table id="tblButtons">
                    <tr>
                        <td>
                            <asp:Button id="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
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
        <tr>
            <td colspan="4" style="height: 21px">
            </td>
        </tr>
    </table>
</asp:Content>

 
