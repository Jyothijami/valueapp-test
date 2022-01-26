<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/PurchaseMP1.master" AutoEventWireup="true" CodeFile="ShipmentDetailsNew1.aspx.cs" Inherits="Modules_SCM_ShipmentDetailsNew1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--  <script>
        $(function () {
            $("[name$='txtPIDate'],[name$='txtPoDate'],[name$='txtFollowDate'],[name$='txtShippingdate'],[name$='txtdateofshipping'],[name$='txtdateArrival'],[name$='txtMaterialReceiptDate'],[name$='txtRemittenceDate'],[name$='txtInsuranceDate'],[name$='txtInvoicedate']").datepicker();
        });
    </script>--%>
    <style type="text/css">
        .profilehead {
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div >
        <table class="pagehead">
            <tr>
                <td style="text-align:left" >
        Shipment Details

                </td>
            </tr>
        </table>
    </div>
    <div class="subh2">
        General Details 
    </div>
    <table id="tblDetails" runat="server" width="100%">
        <tr>
            <td style="text-align: right">
                <asp:Label ID="lblSalesOrderNo" runat="server" Text="PI No" Width="54px"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlPONo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPONo_SelectedIndexChanged"
                    Width="147px">
                </asp:DropDownList><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlPONo"
                        ErrorMessage="Please Select the Purchase Invoice No." InitialValue="0" ValidationGroup="main">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right;">
                <asp:Label ID="lblSalesOrderDate" runat="server" Text="PI Date" Width="64px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtPIDate" runat="server" type="datepic">

                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; height: 24px;">
                <asp:Label ID="Label20" runat="server" Text="PI Amount"></asp:Label></td>
            <td style="text-align: left; height: 24px;">
                <asp:TextBox ID="txtPiAount" runat="server">
                </asp:TextBox></td>
            <td style="text-align: right; height: 24px;">
                <asp:Label ID="Label22" runat="server" Text="PO Date"></asp:Label></td>
            <td style="text-align: left; height: 24px;">
                <asp:TextBox ID="txtPoDate" runat="server" type="datepic">
                </asp:TextBox>

            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label21" runat="server" Text="PO No"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtPoNo" runat="server">
                </asp:TextBox>
                <asp:Label ID="lblPONo" runat="server" Visible="False"></asp:Label>
            </td>
            <td style="text-align: right;">
                <asp:Label ID="Label23" runat="server" Text="PO Amount"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtPOamount" runat="server">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="lblSupplierName" runat="server" Text="Supplier Name" Width="98px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtSupplierName" runat="server" ReadOnly="True">
                </asp:TextBox><asp:DropDownList ID="ddlSupplierName" runat="server" Visible="False">
                </asp:DropDownList></td>
            <td style="text-align: right;">
                <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person" Width="102px">
                </asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtContactPerson" runat="server" ReadOnly="True">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtAddress" runat="server" ReadOnly="True" TextMode="MultiLine">
                </asp:TextBox></td>
            <td style="text-align: right;">
                <asp:Label ID="lblPhone" runat="server" Text="Phone No."></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtPhoneNo" runat="server" ReadOnly="True">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True">
                </asp:TextBox></td>
            <td style="text-align: right;">
                <asp:Label ID="lblMobileNo" runat="server" Text="Mobile No"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtMobileNo" runat="server" ReadOnly="True">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label29" runat="server" Text="Invoice No"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtInvoiceno" runat="server">
                </asp:TextBox></td>
            <td style="text-align: right">
                <asp:Label ID="Label30" runat="server" Text="Invoice Date"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtInvoicedate" runat="server" type="datepic">
                </asp:TextBox>

            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label35" runat="server" Text="Invoice Value"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtInvoiceValue" runat="server">
                </asp:TextBox></td>
            <td style="text-align: right"></td>
            <td style="text-align: left"></td>
        </tr>
        <tr>
            <td colspan="4">&nbsp;<asp:GridView ID="gvItemDetails" runat="server" AutoGenerateColumns="False"
                OnRowDataBound="gvItemDetails_RowDataBound" OnRowDeleting="gvItemDetails_RowDeleting"
                OnRowEditing="gvItemDetails_RowEditing" Width="100%">
                <Columns>
                    <asp:CommandField ShowEditButton="True"></asp:CommandField>
                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                    <asp:BoundField DataField="ItemCode" HeaderText="ItemCode"></asp:BoundField>
                    <asp:BoundField DataField="ItemType" HeaderText="Model No"></asp:BoundField>
                    <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                    <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                    <asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
                    <asp:BoundField DataField="Discount" HeaderText="Discount"></asp:BoundField>
                   <%-- <asp:BoundField DataField="cst" HeaderText="CST"></asp:BoundField>
                    <asp:BoundField DataField="Excise" HeaderText="Excise"></asp:BoundField>--%> 
                   <asp:BoundField DataField="Amount" HeaderText="Amount"></asp:BoundField>
                    <asp:BoundField DataField="ItemTypeId" HeaderText="ItemTypeIdHidden"></asp:BoundField>
                    <asp:BoundField DataField ="Customer" HeaderText ="Customer" />
                </Columns>
            </asp:GridView>
            </td>
        </tr>
    
        <tr><td colspan ="4">
    <div class="subh2">
        Forwarding details
    </div>
            </td></tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label ID="Label31" runat="server" Text="Forwarder"></asp:Label></td>
            <td style="text-align: left;">
                <asp:DropDownList ID="ddlForwarder" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlForwarder_SelectedIndexChanged">
                </asp:DropDownList><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlForwarder"
                        ErrorMessage="Please Select the Forwarder Name" InitialValue="0" ValidationGroup="main">*</asp:RequiredFieldValidator></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label17" runat="server" Text="Phone/Mobile"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtshipingDetailsPhone" runat="server"></asp:TextBox></td>
            <td style="text-align: right;">
                <asp:Label ID="Label19" runat="server" Text="E Mail"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtShippingdetailsEmail" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label18" runat="server" Text="Address"></asp:Label></td>
            <td style="text-align: left; text-decoration: underline;">
                <asp:TextBox ID="txtShipmentDetailsAddress" runat="server" EnableTheming="False" Height="54px" TextMode="MultiLine"
                    Width="226px"></asp:TextBox></td>
            <td style="text-align: right;">
                <asp:Label ID="Label3" runat="server" Text="Forwarding Through"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtFortrough" runat="server" EnableTheming="False" Height="54px" TextMode="MultiLine"
                    Width="226px">
                </asp:TextBox></td>
        </tr>
        <tr><td colspan ="4">
    <div class="subh2">
        Shipment Details
    </div>
            </td></tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label1" runat="server" Text="Bill of Lading/Air Way Bill"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtShipmentNo" runat="server">
                </asp:TextBox></td>
            <td style="text-align: right;">
                <asp:Label ID="Label2" runat="server" Text="datepic"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtShippingdate" runat="server" type="datepic">
                </asp:TextBox>

            </td>
        </tr>
        <tr>
            <td rowspan="1" style="text-align: right">
                <asp:Label ID="Label26" runat="server" Text="Container"></asp:Label></td>
            <td rowspan="1" style="text-align: left">
                <asp:TextBox ID="txtContainer" runat="server">
                </asp:TextBox></td>
            <td style="text-align: right;">
                <asp:Label ID="Label9" runat="server" Text="Shipment Details "></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtShipmentDetails" runat="server" EnableTheming="False" Height="52px" TextMode="MultiLine"
                    Width="212px"></asp:TextBox></td>
        </tr>
        <tr>
            <td rowspan="1" style="text-align: right">
                <asp:Label ID="Label28" runat="server" Text="Volume"></asp:Label></td>
            <td rowspan="1" style="text-align: left">
                <asp:TextBox ID="txtVolume" runat="server">
                </asp:TextBox></td>
            <td style="height: 19px; text-align: right;">
                <asp:Label ID="Label13" runat="server" Text="Date Of Shipping"></asp:Label></td>
            <td style="height: 19px; text-align: left">
                <asp:TextBox ID="txtdateofshipping" runat="server" type="datepic">
                </asp:TextBox>

            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label27" runat="server" Text="Weight"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtWeight" runat="server">
                </asp:TextBox></td>
            <td style="text-align: right;">
                <asp:Label ID="Label25" runat="server" Text="Remittence Date"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtRemittenceDate" runat="server" type="datepic"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td style="text-align: right; height: 24px;">
                <asp:Label ID="Label10" runat="server" Text="Packing Charges"></asp:Label></td>
            <td style="text-align: left; height: 24px;">
                <asp:TextBox ID="txtPackingCharges" runat="server"></asp:TextBox></td>
            <td style="text-align: right; height: 24px;">
                <asp:Label ID="Label6" runat="server" Text="Exp Date of Arrival"></asp:Label></td>
            <td style="text-align: left; height: 24px;">
                <asp:TextBox ID="txtdateArrival" runat="server" type="datepic"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label24" runat="server" Text="Remittence Amount"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtRemittenceAmount" runat="server"></asp:TextBox></td>
            <td style="text-align: right;">
                <asp:Label ID="Label12" runat="server" Text="Material Receipt Date"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtMaterialReceiptDate" runat="server" type="datepic"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label5" runat="server" Text="Duty Excise"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtDutyExcise" runat="server"></asp:TextBox></td>
            <td style="text-align: right;">
                <asp:Label ID="Label11" runat="server" Text="Custom Clearance"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtCustomClearance" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label4" runat="server" Text="Insurance  Policy No"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtInsurance" runat="server"></asp:TextBox></td>
            <td style="text-align: right;">
                <asp:Label ID="Label32" runat="server" Text="Insurance Name"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlInsuranceCompany" runat="server">
                </asp:DropDownList><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlInsuranceCompany"
                        ErrorMessage="Please Select the Insurance Company Name" InitialValue="0" ValidationGroup="main">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label33" runat="server" Text="Insurance Date"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtInsuranceDate" runat="server" type="datepic">
                </asp:TextBox>

            </td>
            <td style="text-align: right">
                <asp:Label ID="Label34" runat="server" Text="Insurance Amount"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtInsuranceAmount" runat="server">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; height: 49px;">
                <table align="center">
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="main" /></td>
                        <td>
                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" /></td>
                        <td>
                            <asp:Button ID="btnclose" runat="server" OnClick="btnclose_Click" Text="Close" /></td>
                         <td>
                            <asp:Button ID="btnFollowUp" runat="server" OnClick="btnFollowUp_Click" Text="Follow Up" Visible="False" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4">
    <table id="tblFollowup" runat="server" border="0" cellpadding="0" cellspacing="0"
        visible="false" align="center">
        <tr>
            <td>
                <%--<div class="subh2">
        Follow up details
    </div>--%>
                <asp:ValidationSummary ID="vs1" runat="server" ShowSummary="False" />
                <asp:ValidationSummary ID="vs2" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="main" />
            </td>
        </tr>
        <tr>
            <td class="profilehead" colspan="4" style="height: 19px; text-align: left;">Follow up details</td>
        </tr>
        <tr>
            <td colspan="4" style="height: 6px">&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label14" runat="server" Text="Name"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtFollowname" runat="server">
                </asp:TextBox></td>
            <td style="text-align: right">
                <asp:Label ID="Label16" runat="server" Text="datepic"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtFollowDate" runat="server" type="datepic">
                </asp:TextBox>

            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label15" runat="server" Text="Follow Up Details"></asp:Label></td>
            <td colspan="3" style="text-align: left">
                <asp:TextBox ID="txtFollowupDetails" runat="server" EnableTheming="False" TextMode="MultiLine"
                    Width="559px" Height="200px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right"></td>
            <td colspan="3" style="text-align: left"></td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnFollowSave" runat="server" Text="Save" OnClick="btnFollowSave_Click" /></td>
                        <td>
                            <asp:Button ID="btnFollowRefresh" runat="server" Text="Refresh" OnClick="btnFollowRefresh_Click" /></td>
                        <td>
                            <asp:Button ID="btnHistory" runat="server" OnClick="btnHistory_Click" Text="History" /></td>
                        <td>
                            <asp:Button ID="btnFollowClose" runat="server" OnClick="btnFollowClose_Click" Text="Close" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <table id="tblfollowupgrid" runat="server" border="0" cellpadding="0" cellspacing="0"
                    visible="false">
                    <tr>
                        <td class="profilehead" colspan="3" style="width: 540px">follow up history</td>
                    </tr>
                    <tr>
                        <td colspan="3" style="width: 540px"></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="width: 540px">
                            <asp:GridView ID="gvFollowuphistory" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" Width="98%">
                                <Columns>
                                    <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="FU_DATE" HeaderText="datepic"></asp:BoundField>
                                    <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Name"></asp:BoundField>
                                    <asp:BoundField DataField="FU_DESC" HeaderText="Description"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                SelectCommand="SELECT YANTRA_EMPLOYEE_MAST.EMP_FIRST_NAME, YANTRA_SHIPPING_DETAILS_FOLLOWUP.FU_DATE, YANTRA_SHIPPING_DETAILS_FOLLOWUP.FU_DESC, YANTRA_SHIPPING_DETAILS_FOLLOWUP.SHIPPING_DETAILS_FOLLOWUP_DET_ID FROM YANTRA_SHIPPING_DETAILS_FOLLOWUP INNER JOIN YANTRA_EMPLOYEE_MAST ON YANTRA_SHIPPING_DETAILS_FOLLOWUP.FU_NAME = YANTRA_EMPLOYEE_MAST.EMP_ID"></asp:SqlDataSource>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="BtnClose1" runat="server" Text="Close" OnClick="BtnClose1_Click" /></td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr><td>
    <table runat="server" width="100%">
        <tr>
            <td colspan="3" style="height: 19px">

            </td>
        </tr>
    </table>
            </td></tr>
        </table>
</asp:Content>


 
