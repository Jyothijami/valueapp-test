<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Prefix.ascx.cs" Inherits="Modules_Masters_Prefix" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%--<link href="../../App_Themes/Master/Master.css" rel="stylesheet" type="text/css" />--%>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td colspan="4" style="text-align: left" class="profilehead">
            Prefix Details</td>
    </tr>
    <tr>
        <td style="text-align: right; height: 19px;">
        </td>
        <td style="text-align: left; height: 19px; width: 183px;">
        </td>
        <td style="text-align: right; height: 19px;">
        </td>
        <td style="text-align: left; height: 19px;">
        </td>
    </tr>
    <tr>
        <td style="text-align: right">
            <asp:Label ID="lblCompanyFullName" runat="server" Text="Customer Information" Width="142px"></asp:Label></td>
        <td style="text-align: left; width: 183px;">
            <asp:TextBox ID="txtCustomerInformation" runat="server" MaxLength="50"></asp:TextBox><asp:Label ID="Label2" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                ID="RFVFullName" runat="server" ControlToValidate="txtCustomerInformation" ErrorMessage="Please Enter the Customer Information">*</asp:RequiredFieldValidator></td>
        <td style="text-align: right;">
            <asp:Label ID="lblCompanyShortName" runat="server" Text="Sales Lead" Width="76px"></asp:Label></td>
        <td style="text-align: left">
            <asp:TextBox ID="txtSalesLead" runat="server" MaxLength="50"></asp:TextBox><asp:Label ID="Label25" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                ID="RFVShortName" runat="server" ControlToValidate="txtSalesLead" ErrorMessage="Please Enter the Sales Lead">*</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td style="text-align: right">
            &nbsp;<asp:Label ID="lblAddress" runat="server" Text="Sales Assignments" Width="114px"></asp:Label></td>
        <td style="text-align: left; width: 183px;">
            <asp:TextBox ID="txtSalesAssignments" runat="server"></asp:TextBox><asp:Label ID="Label1" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                ID="RFVAddress" runat="server" ControlToValidate="txtSalesAssignments" ErrorMessage="Please Enter the Sales Assignments">*</asp:RequiredFieldValidator></td>
        <td style="text-align: right;">
            <asp:Label ID="lblContactNo1" runat="server" Text="Sales Quotation" Width="102px"></asp:Label></td>
        <td style="text-align: left">
            <asp:TextBox ID="txtSalesQuotation" runat="server" MaxLength="50"></asp:TextBox><asp:Label ID="Label24" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                ID="RFVContactNo" runat="server" ControlToValidate="txtSalesQuotation" ErrorMessage="Please Enter the Sales Quotation">*</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td style="text-align: right; height: 24px;">
            <asp:Label ID="lblFaxNo" runat="server" Text="Purchase Order " Width="113px"></asp:Label></td>
        <td style="text-align: left; height: 24px; width: 183px;">
            <asp:TextBox ID="txtSalesOrder" runat="server" MaxLength="50"></asp:TextBox><asp:Label ID="Label3" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                ID="RFVFaxNo" runat="server" ControlToValidate="txtSalesOrder" ErrorMessage="Please Enter the Sales Order">*</asp:RequiredFieldValidator></td>
        <td style="text-align: right; height: 24px;">
            <asp:Label ID="lblContactNo2" runat="server" Text="Internal  Order" Width="91px"></asp:Label></td>
        <td style="text-align: left; height: 24px;">
            <asp:TextBox ID="txtOrderProfile" runat="server" MaxLength="50"></asp:TextBox><asp:Label ID="Label42" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="rfvOrderProfile" runat="server" ControlToValidate="txtOrderProfile"
                ErrorMessage="Please Enter the Order Profile">*</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td style="text-align: right">
            <asp:Label ID="lblEmail" runat="server" Text="Sales Order Acceptance" Width="175px"></asp:Label></td>
        <td style="text-align: left; width: 183px;">
            <asp:TextBox ID="txtSalesOrderAcceptance" runat="server" MaxLength="50"></asp:TextBox><asp:Label ID="Label4" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RFVEmail" runat="server" ControlToValidate="txtSalesOrderAcceptance"
                ErrorMessage="Please Enter the Sales Order Acceptance">*</asp:RequiredFieldValidator></td>
        <td style="text-align: right">
            <asp:Label ID="lblTelexNo" runat="server" Text="Delivery Challan" Width="113px"></asp:Label></td>
        <td style="text-align: left">
            <asp:TextBox ID="txtDelliveryChallan" runat="server" MaxLength="50"></asp:TextBox><asp:Label ID="Label23" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RFVTelexNo" runat="server" ControlToValidate="txtDelliveryChallan"
                ErrorMessage="Please Enter the Dellivery Challan">*</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td style="text-align: right">
            <asp:Label ID="lblAPGSTNo" runat="server" Text="Sales Invoice" Width="140px"></asp:Label></td>
        <td style="text-align: left; width: 183px;">
            <asp:TextBox ID="txtSalesInvoice" runat="server" MaxLength="50"></asp:TextBox><asp:Label ID="Label5" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RFVAPGSTNo" runat="server" ControlToValidate="txtSalesInvoice"
                ErrorMessage="Please Enter the Sales Invoice">*</asp:RequiredFieldValidator></td>
        <td style="text-align: right">
            <asp:Label ID="lblEstablishmentYear" runat="server" Text="Supplier Master"
                Width="114px"></asp:Label></td>
        <td style="text-align: left">
            <asp:TextBox ID="txtSupplierMaster" runat="server" MaxLength="20"></asp:TextBox><asp:Label ID="Label20" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                ID="RFVEstYear" runat="server" ControlToValidate="txtSupplierMaster" ErrorMessage="Please Enter the Supplier Master">*</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td style="text-align: right">
            <asp:Label ID="lblECCNo" runat="server" Text="Purchase Order Details" Width="143px"></asp:Label></td>
        <td style="text-align: left; width: 183px;">
            <asp:TextBox ID="txtPurchaseOrderDetails" runat="server" MaxLength="50"></asp:TextBox><asp:Label ID="Label6" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RFVECCNo" runat="server" ControlToValidate="txtPurchaseOrderDetails"
                ErrorMessage="Please Enter the Purchase Order Details">*</asp:RequiredFieldValidator></td>
        <td style="text-align: right">
            <asp:Label ID="lblDCSuffix" runat="server" Text="Complaint Register" Width="119px"></asp:Label></td>
        <td style="text-align: left">
            <asp:TextBox ID="txtComplaintRegister" runat="server" MaxLength="20"></asp:TextBox><asp:Label ID="Label14" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RFVDCSuffix" runat="server" ControlToValidate="txtComplaintRegister"
                ErrorMessage="Please Enter the Complaint Register">*</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td style="text-align: right">
            <asp:Label ID="lblPANNo" runat="server" Text="Purchase Invoice" Width="114px"></asp:Label></td>
        <td style="text-align: left; width: 183px;">
            <asp:TextBox ID="txtPurchaseInvoice" runat="server" MaxLength="50"></asp:TextBox><asp:Label ID="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RFVPANNo" runat="server" ControlToValidate="txtPurchaseInvoice"
                ErrorMessage="Please Enter the  Purchase Invoice">*</asp:RequiredFieldValidator></td>
        <td style="text-align: right">
            <asp:Label ID="lblInvoiceSuff" runat="server" Text="Service Report" Width="121px"></asp:Label></td>
        <td style="text-align: left">
            <asp:TextBox ID="txtServiceReport" runat="server" MaxLength="20"></asp:TextBox><asp:Label ID="Label1436" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RFVInvoiceSu" runat="server" ControlToValidate="txtServiceReport"
                ErrorMessage="Please Enter the Service Report">*</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td style="text-align: right">
            <asp:Label ID="lblCurrentInvoiceNo" runat="server" Text="Claim Form" Width="121px"></asp:Label></td>
        <td style="text-align: left; width: 183px;">
            <asp:TextBox ID="txtClaimForm" runat="server"></asp:TextBox><asp:Label ID="Label9" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                ID="RFVCINo" runat="server" ControlToValidate="txtClaimForm" ErrorMessage="Please Enter the Claim Form">*</asp:RequiredFieldValidator></td>
        <td style="text-align: right">
            <asp:Label ID="lblPOSuffixss" Visible=false runat="server" Text="AMC Order" Width="124px"></asp:Label></td>
        <td style="text-align: left">
            <asp:TextBox ID="txtAmcOrder" runat="server" Visible=false MaxLength="20"></asp:TextBox>
            <asp:Label ID="Label152" runat="server" EnableTheming="False"  Visible=false Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RFVPOSuffixwe" runat="server" ControlToValidate="txtAmcOrder"
                ErrorMessage="Please Enter the AMC Order">*</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td style="text-align: right">
            <asp:Label ID="lblPOPrefix" runat="server" Text="Sales Payments Received" Width="156px"></asp:Label></td>
        <td style="text-align: left; width: 183px;">
            <asp:TextBox ID="txtSalesPaymentsReceived" runat="server" MaxLength="20"></asp:TextBox><asp:Label ID="Label12" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RFVPOPrefix" runat="server" ControlToValidate="txtSalesPaymentsReceived"
                ErrorMessage="Please Enter the Sales Payments Received">*</asp:RequiredFieldValidator></td>
        <td style="text-align: right">
            <asp:Label ID="Label10" runat="server" Visible=false Text="AMC Order Profile" Width="124px"></asp:Label></td>
        <td style="text-align: left">
            <asp:TextBox ID="txtAmcOrderProfile" runat="server" Visible=false MaxLength="20"></asp:TextBox><asp:Label ID="Label37" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"  Visible=false></asp:Label><asp:RequiredFieldValidator ID="rfvAmcOrderProfile" runat="server" ControlToValidate="txtAmcOrderProfile"
                ErrorMessage="Please Enter the AMC Order Profile">*</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td style="text-align: right">
            <asp:Label ID="Label8" runat="server" Text="Service Assignments" Visible=false Width="139px"></asp:Label></td>
        <td style="text-align: left; width: 183px;">
            <asp:TextBox ID="txtServiceAssignments" runat="server"  Visible=false MaxLength="20"></asp:TextBox><asp:Label ID="Label131" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*" Visible=false></asp:Label><asp:RequiredFieldValidator ID="RFVInvoice" runat="server" ControlToValidate="txtServiceAssignments"
                ErrorMessage="Please Enter the Service Assignments">*</asp:RequiredFieldValidator></td>
        <td style="text-align: right;">
            <asp:Label ID="Label27" runat="server" Text="AMC Inoice" Visible=false Width="124px"></asp:Label></td>
        <td style="text-align: left">
            <asp:TextBox ID="txtAmcInvoice" Visible=false runat="server" MaxLength="20"></asp:TextBox><asp:Label ID="Label40" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"  Visible=false></asp:Label><asp:RequiredFieldValidator ID="rfvAmcInvoice" runat="server" ControlToValidate="txtAmcInvoice"
                ErrorMessage="Please Enter the AMC Invoice">*</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td style="text-align: right">
            <asp:Label ID="lblPOPrefixsd" runat="server" Visible=false Text="AMC Quotation" Width="156px"></asp:Label></td>
        <td style="text-align: left; width: 183px;">
            <asp:TextBox ID="txtAmcQuotation" runat="server" Visible=false MaxLength="20"></asp:TextBox><asp:Label ID="Label124" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"  Visible=false></asp:Label><asp:RequiredFieldValidator ID="RFVPOPrefixsad" runat="server" ControlToValidate="txtAmcQuotation"
                ErrorMessage="Please Enter the AMC Quotation">*</asp:RequiredFieldValidator></td>
        <td style="text-align: right">
            <asp:Label ID="Label30" runat="server" Text="AMC Payments Received" Visible=false Width="159px"></asp:Label></td>
        <td style="text-align: left">
            <asp:TextBox ID="txtAmcPaymentsReceived" Visible=false runat="server" MaxLength="20"></asp:TextBox><asp:Label ID="Label36" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"  Visible=false></asp:Label><asp:RequiredFieldValidator ID="rfvAmcPaymentsReceivedd" runat="server" ControlToValidate="txtAmcPaymentsReceived"
                ErrorMessage="Please Enter the AMC Payments Received">*</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td style="text-align: right;">
            <asp:Label ID="lblDCPre" runat="server" Visible=false Text="AMC Order Acceptance" Width="157px"></asp:Label></td>
        <td style="text-align: left; width: 183px;">
            <asp:TextBox ID="txtAmcOrderAcceptance" Visible=false runat="server" MaxLength="20"></asp:TextBox><asp:Label ID="Label31" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*" Visible=false></asp:Label><asp:RequiredFieldValidator ID="RFVAmcOrderAcceptance" runat="server" ControlToValidate="txtAmcOrderAcceptance"
                ErrorMessage="Please Enter The AMC Order Acceptance">*</asp:RequiredFieldValidator></td>
        <td style="text-align: right;">
            <asp:Label ID="Label17" runat="server" Visible=false Text="Warranty  Claim" Width="124px"></asp:Label></td>
        <td style="text-align: left;">
            <asp:TextBox ID="txtWarrantyClaim" runat="server" Visible=false MaxLength="20"></asp:TextBox><asp:Label ID="Label32" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*" Visible=false></asp:Label><asp:RequiredFieldValidator ID="rfvWarrantyClaim" runat="server" ControlToValidate="txtWarrantyClaim"
                ErrorMessage="Please Enter the Warranty Claim">*</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td style="text-align: right">
            <asp:Label ID="lblInvoicePrefix" runat="server" Text="SD & BG" Width="121px" Visible="False"></asp:Label></td>
        <td style="text-align: left; width: 183px;">
            <asp:TextBox ID="txtSDBG" runat="server" MaxLength="20" Visible="False"></asp:TextBox><asp:Label ID="Label11" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator
                ID="RFVInvoicePrefix" runat="server" ControlToValidate="txtSDBG" ErrorMessage="Please Enter the SD & BG" Visible="False">*</asp:RequiredFieldValidator></td>
        <td style="text-align: right">
            <asp:Label ID="Label33" runat="server" Text="Employee Master" Width="112px" Visible="False"></asp:Label></td>
        <td style="text-align: left">
            <asp:TextBox ID="txtEmployeeMaster" runat="server" MaxLength="20" Visible="False"></asp:TextBox><asp:Label ID="Label41" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="rfvEmployeeMaster" runat="server" ControlToValidate="txtEmployeeMaster"
                ErrorMessage="Please Enter the Employee Master" Visible="False">*</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td style="height: 19px; text-align: right">
            <asp:Label ID="Label43" runat="server" Text="Spares Order Acceptance" Width="159px" Visible="False"></asp:Label></td>
        <td style="height: 19px; text-align: left; width: 183px;">
            <asp:TextBox ID="txtSparesOrderACCEPTANCE" runat="server" MaxLength="20" Visible="False"></asp:TextBox><asp:RequiredFieldValidator ID="rfvSparesOrderAcceptance" runat="server" ControlToValidate="txtSparesOrderACCEPTANCE"
                ErrorMessage="Please Enter the Spares Order Acceptance" Visible="False">*</asp:RequiredFieldValidator><asp:Label ID="Label35" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label></td>
        <td style="height: 19px; text-align: right">
            <asp:Label ID="lblVATNo" runat="server" Text="Work Order Details" Width="123px" Visible="False"></asp:Label></td>
        <td style="height: 19px; text-align: left">
            <asp:TextBox ID="txtWorkOrderDetails" runat="server" MaxLength="50" Visible="False"></asp:TextBox><asp:Label ID="Label21" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="RFVVATNo" runat="server" ControlToValidate="txtWorkOrderDetails"
                ErrorMessage="Please Enter the Work Order Details" Visible="False">*</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td style="height: 19px; text-align: right">
            <asp:Label ID="lblCurrentDCNo" runat="server" Text="Agent Master" Width="121px" Visible="False"></asp:Label></td>
        <td style="height: 19px; text-align: left; width: 183px;">
            <asp:TextBox ID="txtAgentMaster" runat="server" MaxLength="20" Visible="False"></asp:TextBox><asp:Label ID="Label18" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="RFVCDCNo" runat="server" ControlToValidate="txtAgentMaster"
                ErrorMessage="Please Enter the Agent Master" Visible="False">*</asp:RequiredFieldValidator></td>
        <td style="height: 19px; text-align: right">
            <asp:Label ID="Label28" runat="server" Text="Spares Order Profile" Width="129px" Visible="False"></asp:Label></td>
        <td style="height: 19px; text-align: left">
            <asp:TextBox ID="txtSparesOrderProfile" runat="server" MaxLength="20" Visible="False"></asp:TextBox><asp:Label ID="Label39" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="rfvSparesOrderProfile" runat="server" ControlToValidate="txtSparesOrderProfile"
                ErrorMessage="Please Enter the Spares Order Profile" Visible="False">*</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td style="height: 19px; text-align: right">
            <asp:Label ID="lblInvoiceSuffix" runat="server" Text="FE Order Profile" Width="121px" Visible="False"></asp:Label></td>
        <td style="height: 19px; text-align: left; width: 183px;">
            <asp:TextBox ID="txtFEOrderprofile" runat="server" MaxLength="20" Visible="False"></asp:TextBox><asp:Label ID="Label16" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="RFVInvoiceSuffix" runat="server" ControlToValidate="txtFEOrderprofile"
                ErrorMessage="Please Enter the FE Order Profile" Visible="False">*</asp:RequiredFieldValidator></td>
        <td style="height: 19px; text-align: right">
            <asp:Label ID="lblCSTNo" runat="server" Text="Checking Format" Width="113px" Visible="False"></asp:Label></td>
        <td style="height: 19px; text-align: left">
            <asp:TextBox ID="txtCheckingFormat" runat="server" MaxLength="50" Visible="False"></asp:TextBox><asp:Label ID="Label22" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label>
            <asp:RequiredFieldValidator ID="RFVCSTNo" runat="server" ControlToValidate="txtCheckingFormat"
                ErrorMessage="Please Enter the Checking Format" Visible="False">*</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td style="height: 19px; text-align: right">
            <asp:Label ID="lblPOSuffix" runat="server" Text="SD & BG Receipts" Width="124px" Visible="False"></asp:Label></td>
        <td style="height: 19px; text-align: left; width: 183px;">
            <asp:TextBox ID="txtSDBGReceipts" runat="server" MaxLength="20" Visible="False"></asp:TextBox><asp:Label ID="Label15" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="RFVPOSuffix" runat="server" ControlToValidate="txtSDBGReceipts"
                ErrorMessage="Please Enter the SD & BG Receipts" Visible="False">*</asp:RequiredFieldValidator></td>
        <td style="height: 19px; text-align: right">
            <asp:Label ID="lblDCPrefix" runat="server" Text="EMDS Received" Width="113px" Visible="False"></asp:Label></td>
        <td style="height: 19px; text-align: left">
            <asp:TextBox ID="txtEmdsReceived" runat="server" MaxLength="20" Visible="False"></asp:TextBox><asp:Label ID="Label13" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator
                ID="RFVDCPrefix" runat="server" ControlToValidate="txtEmdsReceived" ErrorMessage="Please Enter the EMDS Received" Visible="False">*</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td style="text-align: right; height: 19px;">
            <asp:Label ID="Label19" runat="server" Text="Spares Order" Width="124px" Visible="False"></asp:Label></td>
        <td style="text-align: left; width: 183px; height: 19px;">
            <asp:TextBox ID="txtSparesOrder" runat="server" MaxLength="20" Visible="False"></asp:TextBox><asp:Label ID="Label34" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="rfvSparesOrder" runat="server" ControlToValidate="txtSparesOrder"
                ErrorMessage="Please Enter the Spares Order" Visible="False">*</asp:RequiredFieldValidator></td>
        <td style="text-align: right; height: 19px;">
            <asp:Label ID="Label29" runat="server" Text="Spares Quotation" Width="124px" Visible="False"></asp:Label></td>
        <td style="text-align: left; height: 19px;">
            <asp:TextBox ID="txtSparesQuotation" runat="server" MaxLength="20" Visible="False"></asp:TextBox>&nbsp;
            <asp:Label ID="Label38" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="rfvSparesQuotation" runat="server" ErrorMessage="Please Enter the Spares Quotation" ControlToValidate="txtSparesQuotation" Visible="False">*</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td style="height: 19px; text-align: right">
        </td>
        <td style="width: 183px; height: 19px; text-align: left">
            &nbsp;
        </td>
        <td style="height: 19px; text-align: right">
        </td>
        <td style="height: 19px; text-align: left">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4" style="text-align: center">
            <table id="tblButtons" align="center">
                <tr>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                </tr>
            </table>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server"></asp:ValidationSummary>
        </td>
    </tr>
    <tr>
        <td style="height: 21px">
        </td>
        <td style="height: 21px; width: 183px;">
        </td>
        <td style="height: 21px;">
        </td>
        <td style="height: 21px">
        </td>
    </tr>
</table>