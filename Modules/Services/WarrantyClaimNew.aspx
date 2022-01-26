<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/ServicesMP1.master" AutoEventWireup="true" CodeFile="WarrantyClaimNew.aspx.cs" Inherits="Modules_Services_WarrantyClaimNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        $(function () {
            $("[name$='txtWCDate'],[name$='txtInstallationDate'],[name$='txtSupplierDate'],[name$='txtExpires'],[name$='txtClaimed']").datepicker();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td title="|| ERP: SM :CLAIMFORM ||" style="text-align:left">Warranty Claim</td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td colspan="4">
                <table border="0" cellpadding="0" cellspacing="0" id="tblWarrantyDetails" runat="server"
                    visible="true">
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">General Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right;"></td>
                        <td style="height: 21px; text-align: left;"></td>
                        <td style="height: 21px; text-align: right;"></td>
                        <td style="height: 21px; text-align: left;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label ID="lblWarrantyNo" runat="server" Text="Warranty Claim No"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox ID="txtWCNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label ID="lblDate" runat="server" Text="Warranty Date"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox ID="txtWCDate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox>
                            
                            <cc1:MaskedEditExtender ID="meeWCDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtWCDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 22px; text-align: right">
                            <asp:Label ID="lblSupplier" runat="server" Text="Supplier Name" Width="119px"></asp:Label></td>
                        <td style="height: 22px; text-align: left">
                            <asp:TextBox ID="txtSupplierName" runat="server">Smart LabTech Pvt. Ltd.</asp:TextBox></td>
                        <td style="height: 22px; text-align: right">
                            <asp:Label ID="lblDateOfInstallation" runat="server" Text="Date Of Installation"></asp:Label></td>
                        <td style="height: 22px; text-align: left">
                            <asp:TextBox ID="txtInstallationDate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox>
                            
                            <cc1:MaskedEditExtender ID="meeInstallationDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtInstallationDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 22px;">
                            <asp:Label ID="Label1" runat="server" Text="Supplier Invoice  No"></asp:Label></td>
                        <td style="text-align: left; height: 22px;">
                            <asp:TextBox ID="txtSupInvoiceNo" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right; height: 22px;">
                            <asp:Label ID="lblInvoiceDate" runat="server" Text="Supplier Invoice Date" Width="140px"></asp:Label></td>
                        <td style="text-align: left; height: 22px;">
                            <asp:TextBox ID="txtSupplierDate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox>
                            
                            <cc1:MaskedEditExtender ID="meeSupplierDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtSupplierDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;"></td>
                        <td style="text-align: left;"></td>
                        <td style="text-align: right;"></td>
                        <td style="text-align: left;"></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">Supplier Details (Communication For Warranty Claim)</td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;"></td>
                        <td style="text-align: left; height: 19px;"></td>
                        <td style="text-align: right; height: 19px;"></td>
                        <td style="text-align: left; height: 19px;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label6" runat="server" Text="Supplier Company Name"></asp:Label></td>
                        <td colspan="3" style="text-align: left">&nbsp;<asp:TextBox ID="txtSupplierCompanyName" runat="server" CssClass="textbox"
                            EnableTheming="False" Width="523px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label4" runat="server" Text="Supplier Address For Communication" Width="153px"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtSupplierToAddress" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="523px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label5" runat="server" Text="Attention To"></asp:Label></td>
                        <td colspan="3" style="text-align: left">&nbsp;<asp:TextBox ID="txtAttentionTo" runat="server" CssClass="textbox" EnableTheming="False"
                            Width="523px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">Customer Details</td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right"></td>
                        <td style="height: 24px; text-align: left"></td>
                        <td style="height: 24px; text-align: right"></td>
                        <td style="height: 24px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label ID="lblCustomer" runat="server" Text="Customer Name"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                            <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomerName_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="height: 24px; text-align: right">
                            <asp:Label ID="lblInitName" runat="server" Text="Unit Name" Width="74px"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                            <asp:DropDownList ID="ddlUnitName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="0">--Select Customer--</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlContactPerson" runat="server">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="0">--Select Unit Name--</asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label ID="Label2" runat="server" Text="Customer Unit Address"></asp:Label></td>
                        <td colspan="3" style="height: 24px; text-align: left">
                            <asp:TextBox ID="txtCustUnitAddress" runat="server" EnableTheming="False" TextMode="MultiLine" Width="523px" CssClass="multilinetext"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">Item Details</td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right"></td>
                        <td style="height: 24px; text-align: left"></td>
                        <td style="height: 24px; text-align: right"></td>
                        <td style="height: 24px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label ID="Label3" runat="server" Text="Item Type" Width="119px"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                            <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="height: 24px; text-align: right">
                            <asp:Label ID="lblModel" runat="server" Text="Model"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                            <asp:DropDownList ID="ddlModel" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModel_SelectedIndexChanged">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label ID="lblItemDescription" runat="server" Text="Item Description" Width="119px"></asp:Label></td>
                        <td style="text-align: left; height: 24px;" colspan="3">
                            <asp:TextBox ID="txtDescription" runat="server" EnableTheming="False" TextMode="MultiLine" Width="523px" CssClass="multilinetext"></asp:TextBox>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label ID="lblSLNo" runat="server" Text="SL No" Width="50px"></asp:Label></td>
                        <td style="height: 24px; text-align: left;">
                            <asp:TextBox ID="txtSLNo" runat="server"></asp:TextBox></td>
                        <td style="height: 24px; text-align: right;">
                            <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                            <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label ID="lblExpires" runat="server" Text="Warranty Expires On" Width="142px"></asp:Label></td>
                        <td style="height: 24px; text-align: left;">
                            <asp:TextBox ID="txtExpires" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox>
                            
                            <cc1:MaskedEditExtender ID="meeExpires" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtExpires" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                        <td style="height: 24px; text-align: right;">
                            <asp:Label ID="lblClaimed" runat="server" Text="Warranty Claimed On" Width="143px"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                            <asp:TextBox ID="txtClaimed" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox>
                            
                            <cc1:MaskedEditExtender ID="meeClaimed" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtClaimed" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label ID="lblProblem" runat="server" Text="Nature Of the Problem" Width="148px"></asp:Label></td>
                        <td colspan="3" style="height: 24px; text-align: left">
                            <asp:TextBox ID="txtProblemNature" runat="server" EnableTheming="False" TextMode="MultiLine"
                                Width="525px" CssClass="multilinetext"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label ID="lblService" runat="server" Text="Service Engineer's Remarks" Width="180px"></asp:Label></td>
                        <td style="height: 24px; text-align: left" colspan="3">
                            <asp:TextBox ID="txtServiceRemarks" runat="server" EnableTheming="False" TextMode="MultiLine"
                                Width="525px" CssClass="multilinetext"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label ID="lblSmpl" runat="server" Text="SMPL ServiceEngineer's Remarks" Width="172px"></asp:Label></td>
                        <td style="height: 24px; text-align: left" colspan="3">
                            <asp:TextBox ID="txtSmplRemarks" runat="server" EnableTheming="False" TextMode="MultiLine"
                                Width="526px" CssClass="multilinetext"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblUrgently" runat="server" Text="Remarks Needs Warranty Replacement Urgently"></asp:Label></td>
                        <td style="text-align: left" colspan="3">
                            <asp:TextBox ID="txtRemarksNeeds" runat="server" EnableTheming="False" TextMode="MultiLine"
                                Width="526px" CssClass="multilinetext"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: right; height: 26px;"></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left;" class="profilehead">Reference Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right;"></td>
                        <td style="height: 21px; text-align: left;"></td>
                        <td style="height: 21px; text-align: right;"></td>
                        <td style="height: 21px; text-align: left;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By" Width="96px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlApprovedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
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
                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" /></td>
                                    <td style="width: 3px"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 21px"></td>
            <td style="height: 21px"></td>
            <td style="height: 21px;"></td>
            <td style="height: 21px"></td>
        </tr>
    </table>
</asp:Content>


 
