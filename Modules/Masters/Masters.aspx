<%@ Page Language="C#" MasterPageFile="~/MPs/AdminMP1.master" AutoEventWireup="true"
    CodeFile="Masters.aspx.cs" Inherits="Modules_Masters_Masters" Title="|| ValueApp : Masters : Masters ||" %>


<%@ Register Src="PurchaseItemType.ascx" TagName="PurchaseItemType" TagPrefix="uc1" %>
<%@ Register Src="~/Modules/Masters/CompanyMachinaryMaster.ascx" TagPrefix="UC" TagName="CompanyMachinery" %>
<%@ Register Src="~/Modules/Masters/CompanyProfile.ascx" TagPrefix="UC" TagName="CompanyProfile" %>
<%@ Register Src="~/Modules/Masters/DepartmentMaster.ascx" TagPrefix="UC" TagName="Department" %>
<%@ Register Src="~/Modules/Masters/DesignationMaster.ascx" TagPrefix="UC" TagName="Designation" %>
<%@ Register Src="~/Modules/Masters/EnquiryMode.ascx" TagPrefix="UC" TagName="EnquiryMode" %>
<%@ Register Src="~/Modules/Masters/IdleCodes.ascx" TagPrefix="UC" TagName="IdleCode" %>
<%@ Register Src="~/Modules/Masters/OperationsMaster.ascx" TagPrefix="UC" TagName="Operations" %>
<%@ Register Src="~/Modules/Masters/PaymentMode.ascx" TagPrefix="UC" TagName="PaymentMode" %>
<%@ Register Src="~/Modules/Masters/ReviewCategory.ascx" TagPrefix="UC" TagName="ReviewCategory" %>
<%@ Register Src="~/Modules/Masters/Review_Questions.ascx" TagPrefix="UC" TagName="ReviewQuestions" %>

<%@ Register Src="~/Modules/Masters/RegionalMaster.ascx" TagPrefix="UC" TagName="Regional" %>
<%@ Register Src="~/Modules/Masters/SubContractorMaster.ascx" TagPrefix="UC" TagName="SubContractor" %>
<%@ Register Src="~/Modules/Masters/TransportMaster.ascx" TagPrefix="UC" TagName="Transporter" %>
<%@ Register Src="~/Modules/Masters/UnitMaster.ascx" TagPrefix="UC" TagName="UOM" %>
<%@ Register Src="~/Modules/Masters/DespatchMode.ascx" TagPrefix="UC" TagName="DespatchMode" %>
<%@ Register Src="~/Modules/Masters/IndustryType.ascx" TagPrefix="UC" TagName="IndustryType" %>
<%@ Register Src="~/Modules/Masters/ItemType.ascx" TagPrefix="UC" TagName="ItemType" %>
<%@ Register Src="~/Modules/Masters/AssetType.ascx" TagPrefix="UC" TagName="AssetType" %>

<%@ Register Src="~/Modules/Masters/ShiftMaster.ascx" TagPrefix="UC" TagName="ShiftMaster" %>
<%@ Register Src="~/Modules/Masters/RegisterMaster.ascx" TagPrefix="UC" TagName="RegisterMaster" %>
<%@ Register Src="~/Modules/Masters/Architect.ascx" TagPrefix ="UC" TagName ="Architect" %>
<%@ Register Src="~/Modules/Masters/CurrencyType.ascx" TagPrefix="UC" TagName="CurrencyType" %>
<%@ Register Src="~/Modules/Masters/GodownMaster.ascx" TagPrefix="UC" TagName="Godown" %>
<%@ Register Src="~/Modules/Masters/Prefix.ascx" TagPrefix="UC" TagName="Prefix" %>
<%@ Register Src="~/Modules/Masters/ProductCompany.ascx" TagPrefix="UC" TagName="ProductCompany" %>
<%@ Register Src="~/Modules/Masters/ItemCategoryMaster.ascx" TagPrefix="UC" TagName="ItemCategory" %>
<%@ Register Src="~/Modules/Masters/AssetCategory.ascx" TagPrefix="UC" TagName="AssetCategory" %>

<%@ Register Src="~/Modules/Masters/CountryMaster.ascx" TagPrefix="UC" TagName="CountryMaster" %>
<%@ Register Src="~/Modules/Masters/ColorMaster.ascx" TagPrefix="UC" TagName="ColorMaster" %>
<%@ Register Src="~/Modules/Masters/ForwarderDetails.ascx" TagPrefix="UC" TagName="ForwarderDetails" %>
<%@ Register Src="~/modules/masters/insurancemaster.ascx" TagPrefix="UC" TagName="InsuranceMaster" %>
<%@ Register Src="~/modules/masters/DeliveryAddress.ascx" TagPrefix="UC" TagName="DeliveryAddress" %>
<%@ Register Src="~/Modules/Masters/ForwarderDetails1.ascx" TagPrefix="UC" TagName="ForwarderDetails1" %>
<%--<%@ Register Src="~/Modules/SM/Customer_Info_Log_Activity1.aspx" TagPrefix="UC" TagName ="CustlogActivity" %>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <%--    <script>
        function InIEvent() {
            $('input[type=date]').datepicker({
                changeMonth: true,
                changeYear: true,
                onSelect: function () { $(".ui-datepicker  a").removeAttr("href"); }
            });
            return false;
        Sub
        $(document).ready(InIEvent);
    </script>--%>
    <style type="text/css">
        .auto-style2 {
            text-decoration: underline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <table class="pagehead">
                <tr>
                    <td style="text-align: center">MASTER</td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="pagehead"></td>
                </tr>
            </table>
            <table style="width: 100%; height: 100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="text-align: left" valign="top"></td>
                    <td style="text-align: left" valign="top"></td>
                </tr>
                <tr>
                    <td style="text-align: left" valign="top">
                        <br />
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="border-bottom: 1px solid #c0c0c0; font-size: 17px; padding-bottom: 11px; text-align: center;">
                                    <span style="font-size: larger; text-decoration: underline; padding-bottom: 10px">Masters</span></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnCompanyMachinary" runat="server" CausesValidation="False" CssClass="leftmenu" EnableTheming="False" OnClick="lbtnMenuLinks_Click" Visible="False">Company Machinery</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnCompanyProfile" runat="server" CssClass="leftmenu" EnableTheming="False"
                                        OnClick="lbtnMenuLinks_Click" CausesValidation="False">Company Profile</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnDepartment" runat="server" CssClass="leftmenu" EnableTheming="False"
                                        OnClick="lbtnMenuLinks_Click" CausesValidation="False">Department</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnDesignation" runat="server" CssClass="leftmenu" EnableTheming="False"
                                        OnClick="lbtnMenuLinks_Click" CausesValidation="False">Designation</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnEnquiryMode" runat="server" CssClass="leftmenu" EnableTheming="False"
                                        OnClick="lbtnMenuLinks_Click" CausesValidation="False">Enquiry Mode</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnIdleCode" runat="server" CssClass="leftmenu" EnableTheming="False"
                                        OnClick="lbtnMenuLinks_Click" CausesValidation="False" Visible="False">Idle Code</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnOperations" runat="server" CssClass="leftmenu" EnableTheming="False"
                                        OnClick="lbtnMenuLinks_Click" CausesValidation="False" Visible="False">Operations</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnPayMode" runat="server" CssClass="leftmenu" EnableTheming="False"
                                        OnClick="lbtnMenuLinks_Click" CausesValidation="False">Payment Mode</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnReviewCategory" runat="server" CssClass="leftmenu" EnableTheming="False"
                                        OnClick="lbtnMenuLinks_Click" CausesValidation="False">Review Category</asp:LinkButton></td>
                            </tr>
                             <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnReviewQuestion" runat="server" CssClass="leftmenu" EnableTheming="False"
                                        OnClick="lbtnMenuLinks_Click" CausesValidation="False">Review Questions</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnRegional" runat="server" CssClass="leftmenu" EnableTheming="False"
                                        OnClick="lbtnMenuLinks_Click" CausesValidation="False">Regional</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnUnit" runat="server" CssClass="leftmenu" EnableTheming="False"
                                        OnClick="lbtnMenuLinks_Click" CausesValidation="False">UOM</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnSubContractor" runat="server" CssClass="leftmenu" EnableTheming="False"
                                        OnClick="lbtnMenuLinks_Click" CausesValidation="False" Visible="False">Sub Contractor</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnTransporter" runat="server" CssClass="leftmenu" EnableTheming="False"
                                        OnClick="lbtnMenuLinks_Click" CausesValidation="False">Transporter</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnDespatchMode" runat="server" CssClass="leftmenu" EnableTheming="False"
                                        OnClick="lbtnMenuLinks_Click" CausesValidation="False">Despatch Mode</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnIndustryType" runat="server" CssClass="leftmenu" EnableTheming="False"
                                        OnClick="lbtnMenuLinks_Click" CausesValidation="False">Industry Type</asp:LinkButton></td>

                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnPItemType" runat="server" CausesValidation="False" CssClass="leftmenu"
                                        EnableTheming="False" OnClick="lbtnMenuLinks_Click">Purchase Item Type</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td>

                                    <asp:LinkButton ID="lbtnCurrencyType" runat="server" CausesValidation="False" CssClass="leftmenu"
                                        EnableTheming="False" OnClick="lbtnMenuLinks_Click">Currency Type</asp:LinkButton></td>

                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnProductCompanyMaster" runat="server" CausesValidation="False" CssClass="leftmenu"
                                        EnableTheming="False" OnClick="lbtnMenuLinks_Click">Brand Master</asp:LinkButton></td>



                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnItemCategoryMaster" runat="server" CausesValidation="False" CssClass="leftmenu"
                                        EnableTheming="False" OnClick="lbtnMenuLinks_Click">Item Category</asp:LinkButton></td>

                            </tr>
                             <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnAssetCategoryMaster" runat="server" CausesValidation="False" CssClass="leftmenu"
                                        EnableTheming="False" OnClick="lbtnMenuLinks_Click">Asset Category</asp:LinkButton></td>

                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnArchitect" runat="server" CausesValidation="False" CssClass="leftmenu"
                                        EnableTheming="False" OnClick="lbtnMenuLinks_Click">Architect</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td>

                                    <asp:LinkButton ID="lbtnItemType" runat="server" CssClass="leftmenu" EnableTheming="False"
                                        OnClick="lbtnMenuLinks_Click" CausesValidation="False">Item Sub Category</asp:LinkButton></td>



                            </tr>
                            <tr>
                                <td>

                                    <asp:LinkButton ID="lbtnAssetType" runat="server" CssClass="leftmenu" EnableTheming="False"
                                        OnClick="lbtnMenuLinks_Click" CausesValidation="False">Asset Sub Category</asp:LinkButton></td>



                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnShiftMaster" runat="server" CssClass="leftmenu" EnableTheming="False"
                                        OnClick="lbtnMenuLinks_Click" CausesValidation="False" Visible="False">Shift Master</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnRegisterMaster" runat="server" CssClass="leftmenu" EnableTheming="False"
                                        OnClick="lbtnMenuLinks_Click" CausesValidation="False" Visible="False">Register Master</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnCompanyLogos" Visible="false" runat="server" CausesValidation="False" CssClass="leftmenu"
                                        EnableTheming="False" OnClick="lbtnMenuLinks_Click">Company Logos</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnCountryMaster" runat="server" CssClass="leftmenu" EnableTheming="False"
                                        OnClick="lbtnMenuLinks_Click" CausesValidation="False">Country Master</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnColorMaster" runat="server" CssClass="leftmenu" EnableTheming="False"
                                        OnClick="lbtnMenuLinks_Click" CausesValidation="False">Colour Master</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnGodown" Visible="false" runat="server" CausesValidation="False" CssClass="leftmenu"
                                        EnableTheming="False" OnClick="lbtnMenuLinks_Click">Godown</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnPrefix" runat="server" CausesValidation="False" CssClass="leftmenu"
                                        EnableTheming="False" OnClick="lbtnMenuLinks_Click">Prefix</asp:LinkButton></td>




                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnForwardDetails" runat="server" CausesValidation="False" CssClass="leftmenu"
                                        EnableTheming="False" OnClick="lbtnMenuLinks_Click">Terms and Conditions</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnInsuranceMaster" runat="server" Visible="true" CausesValidation="False" CssClass="leftmenu"
                                        EnableTheming="False" OnClick="lbtnMenuLinks_Click">Insurance Master</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnDeliveryAddress" runat="server" CausesValidation="False" CssClass="leftmenu"
                                        EnableTheming="False" OnClick="lbtnMenuLinks_Click">Delivery Address</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnForwardDetails1" runat="server" CausesValidation="False" CssClass="leftmenu"
                                        EnableTheming="False" OnClick="lbtnMenuLinks_Click">Forwarder Details</asp:LinkButton></td>
                            </tr>
                            <%--<tr>
                                <td>
                                    <asp:LinkButton ID="lbtnCustlogActivity" runat="server" CausesValidation="False" CssClass="leftmenu"
                                        EnableTheming="False" OnClick="lbtnMenuLinks_Click">Customer Info log Activity</asp:LinkButton></td>
                            </tr>--%>
                            <%--<tr>
                        <td>
                            <asp:HyperLink ID="HyperLink1" runat="server" CssClass="leftmenu" Target="_blank"  NavigateUrl="~/Modules/SCM/Manage_SupplierPO_Templates.aspx">Supplier PO Templates</asp:HyperLink>
                        </td>
                    </tr>--%>
                            <tr>
                                <td>
                                    <asp:HyperLink ID="HyperLink2" runat="server" CssClass="leftmenu" Target="_blank" NavigateUrl="~/Modules/Masters/user_Activity_Log.aspx">Log Activity</asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HyperLink ID="HyperLink3" runat="server" CssClass="leftmenu" Target="_blank" NavigateUrl="~/Modules/Masters/Manage_Locations.aspx">New Warehouse Location</asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HyperLink ID="HyperLink4" runat="server" CssClass="leftmenu" Target="_blank" NavigateUrl="~/Modules/Warehouse/Manage_Warehouse.aspx">Warehouse Address</asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HyperLink ID="HyperLink5" runat="server" CssClass="leftmenu" Target="_blank" NavigateUrl="~/Modules/Masters/Attendance_Report.aspx">Attendance Report</asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HyperLink ID="HyperLink6" runat="server" CssClass="leftmenu" Target="_blank" NavigateUrl="~/Modules/HR/Employee_Images.aspx">Employee Details</asp:HyperLink>

                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="leftmenu" Target="_blank" NavigateUrl="~/dev_pages/ServiceRequest.aspx">Raise service Request/Ticket</asp:HyperLink>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HyperLink ID="HyperLink7" runat="server" CssClass="leftmenu" Target="_blank" NavigateUrl="~/Modules/SM/Customer_Info_Log_Activity1.aspx">Cust Info Log Activity</asp:HyperLink>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HyperLink ID="HyperLink8" runat="server" CssClass="leftmenu" Target="_blank" NavigateUrl="~/Modules/Profiles/ManageMenuLinks.aspx">Menu Links</asp:HyperLink>

                                </td>
                            </tr>

                            <%--<tr>
                                <td>
                                    
                                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="leftmenu" Target="_blank" NavigateUrl="~/Modules/Warehouse/Stock_Delete.aspx">Delete Available Stock</asp:HyperLink>

                                </td>
                            </tr>--%>
                            <tr>
                                <td style="width: 175px">&nbsp;</td>
                            </tr>
                        </table>
                        <br />
                    </td>
                    <td style="text-align: left" valign="top">
                        <UC:CompanyMachinery runat="server" Visible="false" ID="ucCompanyMachinery" />
                        <UC:CompanyProfile runat="server" Visible="false" ID="ucCompanyProfile" />
                        <UC:Department runat="server" Visible="false" ID="ucDepartment" />
                        <UC:Designation runat="server" Visible="false" ID="ucDesignation" />
                        <UC:EnquiryMode runat="server" Visible="false" ID="ucEnquiryMode" />
                        <UC:IdleCode runat="server" Visible="false" ID="ucIdleCode" />
                        <UC:Operations runat="server" Visible="false" ID="ucOperations" />
                        <UC:PaymentMode runat="server" Visible="false" ID="ucPaymentMode" />
                        <UC:ReviewCategory runat ="server" Visible ="false" ID="ucReviewCategory" />
                        <UC:ReviewQuestions runat ="server" Visible ="false" ID="ucReviewQuestions" />
                        
                        <UC:Regional runat="server" Visible="false" ID="ucRegional" />
                        <UC:SubContractor runat="server" Visible="false" ID="ucSubContractor" />
                        <UC:Transporter runat="server" Visible="false" ID="ucTransporter" />
                        <UC:UOM runat="server" Visible="false" ID="ucUOM" />
                        <UC:DespatchMode runat="server" Visible="false" ID="ucDespatchMode" />
                        <UC:ItemType runat="server" Visible="false" ID="ucItemType" />
                        <UC:AssetType runat="server" Visible="false" ID="ucAssetType" />

                        <UC:IndustryType runat="server" Visible="false" ID="ucIndustryType" />
                        <uc1:PurchaseItemType ID="ucPurchaseItemType" runat="server" Visible="false" />
                        <UC:ShiftMaster runat="server" Visible="false" ID="ucShiftMaster" />
                        <UC:RegisterMaster runat="server" Visible="false" ID="ucRegisterMaster" />
                        
                        <UC:Architect runat ="server" Visible ="false" ID ="ucArchitect" />
                        <UC:ItemCategory runat="server" Visible="false" ID="ucItemCategory" />
                        <UC:AssetCategory runat="server" Visible="false" ID="ucAssetCategory" />

                        <UC:ProductCompany runat="server" Visible="false" ID="ucProductCompany" />
                        <iframe id="iframeCompanyLogos" frameborder="no" height="700" src="CompanyLogos.aspx"
                            scrolling="no" visible="false" runat="server" style="width: 976px"></iframe>
                        <UC:CurrencyType runat="server" Visible="false" ID="ucCurrencyType" />
                        <UC:Godown runat="server" Visible="false" ID="ucGodown" />
                        <UC:Prefix ID="ucPrefix" runat="server" Visible="false" />
                        <UC:CountryMaster runat="server" Visible="false" ID="ucCountryMaster" />
                        <UC:ColorMaster runat="server" Visible="false" ID="ucColorMaster" />

                        <UC:ForwarderDetails runat="server" Visible="false" ID="ucForwarderDetails" />
                        <UC:InsuranceMaster runat="server" Visible="false" ID="ucInsuranceMaster" />
                        <UC:DeliveryAddress runat="server" Visible="false" ID="ucDeliveryAddress" />
                        <UC:ForwarderDetails1 runat="server" Visible="false" ID="ucForwarderDetails1" />
                        <%--<UC:CustlogActivity runat="server" Visible="false" ID="ucCustlogActivity" />--%>
                    </td>
                </tr>
            </table>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

 
