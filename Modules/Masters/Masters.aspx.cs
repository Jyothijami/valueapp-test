using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using vllib;

public partial class Modules_Masters_Masters : basePage 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setControlsVisibility();

        }

    }

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "2");
        
    }
    protected void lbtnMenuLinks_Click(object sender, EventArgs e)
    {
        LinkButton lbtnMenuLinks;
        lbtnMenuLinks = (LinkButton)sender;

        lbtnCompanyMachinary.CssClass = "leftmenu";
        lbtnCompanyProfile.CssClass = "leftmenu";
        lbtnDepartment.CssClass = "leftmenu";
        lbtnDesignation.CssClass = "leftmenu";
        lbtnEnquiryMode.CssClass = "leftmenu";
        lbtnIdleCode.CssClass = "leftmenu";
        lbtnOperations.CssClass = "leftmenu";
        lbtnPayMode.CssClass = "leftmenu";
        lbtnReviewCategory.CssClass = "leftmenu";
        lbtnRegional.CssClass = "leftmenu";
        lbtnSubContractor.CssClass = "leftmenu";
        lbtnTransporter.CssClass = "leftmenu";
        lbtnUnit.CssClass = "leftmenu";
        lbtnDespatchMode.CssClass = "leftmenu";
        lbtnItemType.CssClass = "leftmenu";
        lbtnIndustryType.CssClass = "leftmenu";
        lbtnShiftMaster.CssClass = "leftmenu";
        lbtnRegisterMaster.CssClass = "leftmenu";
        lbtnArchitect.CssClass = "leftmenu";
        lbtnCurrencyType.CssClass = "leftmenu";
        lbtnCompanyLogos.CssClass = "leftmenu";
        lbtnPrefix.CssClass = "leftmenu";
        lbtnPItemType.CssClass = "leftmenu";
       
        lbtnProductCompanyMaster.CssClass = "leftmenu";
        lbtnCountryMaster.CssClass = "leftmenu";
        lbtnItemCategoryMaster.CssClass = "leftmenu";
        lbtnGodown.CssClass = "leftmenu";
        lbtnColorMaster.CssClass = "leftmenu";

        lbtnForwardDetails.CssClass = "leftmenu";
        lbtnInsuranceMaster.CssClass = "leftmenu";
        lbtnDeliveryAddress.CssClass = "leftmenu";
        //lbtnCustlogActivity.CssClass = "leftmenu";

        lbtnAssetCategoryMaster.CssClass = "leftmenu";
        lbtnAssetType.CssClass = "leftmenu";
        lbtnReviewQuestion.CssClass = "leftmenu";
        //ucCustlogActivity.Visible = false;
        ucCompanyMachinery.Visible = false;
        ucCompanyProfile.Visible = false;
        ucDepartment.Visible = false;
        ucDesignation.Visible = false;
        ucEnquiryMode.Visible = false;
        ucIdleCode.Visible = false;
        ucOperations.Visible = false;
        ucPaymentMode.Visible = false;
        ucReviewCategory.Visible = false;
        ucRegional.Visible = false;
        ucSubContractor.Visible = false;
        ucTransporter.Visible = false;
        ucUOM.Visible = false;
        ucDespatchMode.Visible = false;
        ucItemType.Visible = false;
        ucIndustryType.Visible = false;
        ucShiftMaster.Visible = false;
        ucRegisterMaster.Visible = false;
       // ucProductMaster.Visible = false;
        ucItemCategory.Visible = false;
        //iframeItemMaster.Visible = false;
        ucCurrencyType.Visible = false;
        iframeCompanyLogos.Visible = false;
        ucPrefix.Visible = false;
        ucPurchaseItemType.Visible = false;
      //  ucItemMaster.Visible = false;
        ucReviewQuestions.Visible = false;
        ucProductCompany.Visible = false;
        ucCountryMaster.Visible = false;
        ucGodown.Visible = false;
        ucColorMaster.Visible = false;
        ucInsuranceMaster.Visible = false;
        ucForwarderDetails.Visible = false;
        ucDeliveryAddress.Visible = false;
        ucAssetCategory.Visible = false;
        ucAssetType.Visible = false;
        switch (lbtnMenuLinks.Text)
        {
            case "Colour Master":
                {
                    ucColorMaster.Visible = true;
                    lbtnColorMaster.CssClass = "leftmenuhighlight";
                    break;
                }
            case "Country Master":
                {
                    ucCountryMaster.Visible = true;
                    lbtnCountryMaster.CssClass = "leftmenuhighlight";
                    break;
                }
            case "Company Machinery":
                {
                    ucCompanyMachinery.Visible = true;
                    lbtnCompanyMachinary.CssClass = "leftmenuhighlight";
                    break;
                }
            case "Company Profile":
                {
                    ucCompanyProfile.Visible = true;
                    lbtnCompanyProfile.CssClass = "leftmenuhighlight";
                    break;
                }
            case "Department":
                {
                    ucDepartment.Visible = true;
                    lbtnDepartment.CssClass = "leftmenuhighlight";
                    break;
                }
            case "Designation":
                {
                    ucDesignation.Visible = true;
                    lbtnDesignation.CssClass = "leftmenuhighlight";
                    break;
                }
            case "Enquiry Mode":
                {
                    ucEnquiryMode.Visible = true;
                    lbtnEnquiryMode.CssClass = "leftmenuhighlight";
                    break;
                }
            case "Idle Code":
                {
                    ucIdleCode.Visible = true;
                    lbtnIdleCode.CssClass = "leftmenuhighlight";
                    break;
                }
            case "Operations":
                {
                    ucOperations.Visible = true;
                    lbtnOperations.CssClass = "leftmenuhighlight";
                    break;
                }
            case "Payment Mode":
                {
                    ucPaymentMode.Visible = true;
                    lbtnPayMode.CssClass = "leftmenuhighlight";
                    break;
                }
            
            case "Review Category":
                {
                    ucReviewCategory .Visible = true;
                    lbtnReviewCategory.CssClass = "leftmenuhighlight";
                    break;
                }
            case "Review Questions":
                {
                    ucReviewQuestions.Visible = true;
                    lbtnReviewQuestion.CssClass = "leftmenuhighlight";
                    break;
                }
            case "Regional":
                {
                    ucRegional.Visible = true;
                    lbtnRegional.CssClass = "leftmenuhighlight";
                    break;
                }
            case "UOM":
                {
                    ucUOM.Visible = true;
                    lbtnUnit.CssClass = "leftmenuhighlight";
                    break;
                }
            case "Sub Contractor":
                {
                    ucSubContractor.Visible = true;
                    lbtnSubContractor.CssClass = "leftmenuhighlight";
                    break;
                }
            case "Transporter":
                {
                    ucTransporter.Visible = true;
                    lbtnTransporter.CssClass = "leftmenuhighlight";
                    break;
                }
            case "Despatch Mode":
                {
                    ucDespatchMode.Visible = true;
                    lbtnDespatchMode.CssClass = "leftmenuhighlight";
                    break;
                }
            case "Item Sub Category":
                {
                    ucItemType.Visible = true;
                    lbtnItemType.CssClass = "leftmenuhighlight";
                    break;
                }
            case "Asset Sub Category":
                {
                    ucAssetType.Visible = true;
                    lbtnAssetType.CssClass = "leftmenuhighlight";
                    break;
                }
            case "Purchase Item Type":
                {
                    ucPurchaseItemType.Visible = true;
                    lbtnPItemType.CssClass = "leftmenuhighlight";
                    break;
                }
            case "Industry Type":
                {
                    ucIndustryType.Visible = true;
                    lbtnIndustryType.CssClass = "leftmenuhighlight";
                    break;
                }
            case "Shift Master":
                {
                    ucShiftMaster.Visible = true;
                    lbtnShiftMaster.CssClass = "leftmenuhighlight";
                    break;
                }
            case "Register Master":
                {
                    ucRegisterMaster.Visible = true;
                    lbtnRegisterMaster.CssClass = "leftmenuhighlight";
                    break;
                }
         
            case "Currency Type":
                {
                    ucCurrencyType.Visible = true;
                    lbtnCurrencyType.CssClass = "leftmenuhighlight";
                    break;
                }
            case "Company Logos":
                {
                    iframeCompanyLogos.Visible = true;
                    //ucItemMaster.Visible = true;
                    lbtnCompanyLogos.CssClass = "leftmenuhighlight";
                    break;
                }
            case "Prefix":
                {
                    ucPrefix.Visible = true;
                    //ucItemMaster.Visible = true;
                    lbtnPrefix.CssClass = "leftmenuhighlight";
                    break;
                }
          
            case "Brand Master":
                {
                    ucProductCompany.Visible = true;
                    lbtnProductCompanyMaster.CssClass = "leftmenuhighlight";
                    break;
                }
            
            case "Item Category":
                {
                    ucItemCategory.Visible = true;
                    lbtnItemCategoryMaster.CssClass = "leftmenuhighlight";
                    break;
                }
            case "Asset Category":
                {
                    ucAssetCategory.Visible = true;
                    lbtnAssetCategoryMaster .CssClass = "leftmenuhighlight";
                    break;
                }
            case "Architect":
                {
                    ucArchitect.Visible = true;
                    lbtnArchitect.CssClass = "leftmenuhighlight";
                    break;
                }
            case "Godown":
                {
                    ucGodown.Visible = true;
                    lbtnGodown.CssClass = "leftmenuhighlight";
                    break;
                }

            case "Insurance Master":
                {
                    ucInsuranceMaster.Visible = true;
                    lbtnInsuranceMaster.CssClass = "leftmenuhighlight";
                    break;
                }
            case "Terms and Conditions":
                {
                    ucForwarderDetails.Visible = true;
                    lbtnForwardDetails.CssClass = "leftmenuhighlight";
                    break;
                }
            case "Forwarder Details":
                {
                    ucForwarderDetails1.Visible = true;
                    lbtnForwardDetails1.CssClass = "leftmenuhighlight";
                    break;
                }
            case "Delivery Address":
                {
                    ucDeliveryAddress.Visible = true;
                    lbtnDeliveryAddress.CssClass = "leftmenuhighlight";
                    break;
                }
            case "Customer Info log Activity":
                {
                    //lbtnCustlogActivity.CssClass  = "leftmenuhighlight";
                    break;
                }

            default:
                {
                    break;
                }
        }

    }

    
}

 
