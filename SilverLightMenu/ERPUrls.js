// JScript File
var hfp;
 var ValidOrNot; 
function HiddenFieldP()
{ 
  hfp=document.getElementById('ctl00$HiddenFieldDoNotRemove').value;
}

function NavigateToUrl(UrlLabel)
{
    if(UrlLabel.split("_",1)=="Master")
    {
        MasterNavigation(UrlLabel); 
        window.navigate(setUrl);   
    }
    else if(UrlLabel.split("_",1)=="SM")
    {
        SMNavigation(UrlLabel); 
        window.navigate(setUrl);   
    }
    else if(UrlLabel.split("_",1)=="SCM")
    {
        SCMNavigation(UrlLabel); 
        window.navigate(setUrl);   
    }
    else if(UrlLabel.split("_",1)=="Inventory")
    {
        InventoryNavigation(UrlLabel); 
        window.navigate(setUrl);   
    }
    else if(UrlLabel.split("_",1)=="Support")
    {
        SupportNavigation(UrlLabel); 
        window.navigate(setUrl);   
    }
   
     else if(UrlLabel.split("_",1)=="Finance")
    {
         FinanceNavigation(UrlLabel); 
        window.navigate(setUrl);   
    }
    else if(UrlLabel.split("_",1)=="HR")
    {
        HRNavigation(UrlLabel); 
        window.navigate(setUrl);   
    }
   else if(UrlLabel.split("_",1)=="Reports")
    {
        ReportsNavigation(UrlLabel); 
        window.navigate(setUrl);   
    }
     else if(UrlLabel.split("_",1)=="Help")
    {
        HelpNavigation(UrlLabel); 
        window.navigate(setUrl);   
    } 
}

function MasterMenuLoad(rootElement)
{
    rootElement.findName("Master_1").Text="Users";
    rootElement.findName("Master_2").Text="Masters"; 
    rootElement.findName("Master_3").Text="Item Master"; 
    rootElement.findName("Master_4").Text="Product Master";
   
    for (var i=1;i<=4;i++)
    {  
        MasterNavigation("Master_"+i);
        if(setUrl.toLowerCase()=="../"+currentUrl.toLowerCase())
        {   
            Key="Master"; 
            SubKey="Master_"+i;
            MainMenuSelect(rootElement,Key);
            SubMenuItemsSelect(rootElement,SubKey);
            if(hfp!="")
            {
                PrivilegesList=hfp.split("#|#");
            
                for(var i=0;i<=PrivilegesList.length;i++)
                {
                    if(PrivilegesList[i]==SubKey)
                    {
                        ValidOrNot=true;
                        break;
                    }
                    else
                    {
                        ValidOrNot=false;
                    } 
                }
            
                if(ValidOrNot==false)
                {
                    MainMenuSelect(rootElement,Key);
                    alert("Sorry ......... ! You Are Not Having Privilege To Access This Page. Please Contact Administrator");
                    window.navigate("../Home/Default.aspx")
                    return;
                }
            }
            return;
        }
    }
}
function MasterNavigation(ItemName)
{  
    if(ItemName=="Master_1")
    {
        setUrl ="../Masters/Add_User.aspx";
    }   
    else if(ItemName=="Master_2")
    {
        setUrl ="../Masters/Masters.aspx";
    }    
    else if(ItemName=="Master_3")
    {
        setUrl ="../Masters/Default2.aspx";
    }
    else if(ItemName=="Master_4")
    {
        setUrl ="../Masters/Default3.aspx";
    }    
}

function SMMenuLoad(rootElement)
{
    rootElement.findName("SM_1").Text="Customer Info";   
    rootElement.findName("SM_2").Text="Sales Lead";
    rootElement.findName("SM_3").Text="Sales Assignments";   
    rootElement.findName("SM_4").Text="Sales Quotation";   
    rootElement.findName("SM_5").Text="Purchase Order";
    rootElement.findName("SM_6").Text="Internal Order";
    rootElement.findName("SM_7").Text="Process Status";
    rootElement.findName("SM_8").Text="Payments Received";
    rootElement.findName("SM_9").Text="Daily Report";
  for (var i=1;i<=9;i++)
    {  
        SMNavigation("SM_"+i);
        if(setUrl.toLowerCase()=="../"+currentUrl.toLowerCase())
        {   
            Key="SM"; 
            SubKey="SM_"+i;
            MainMenuSelect(rootElement,Key);
            SubMenuItemsSelect(rootElement,SubKey);
            if(hfp!="")
            {
                PrivilegesList=hfp.split("#|#");
            
                for(var i=0;i<=PrivilegesList.length;i++)
                {
                    if(PrivilegesList[i]==SubKey)
                    {
                        ValidOrNot=true;
                        break;
                    }
                    else
                    {
                        ValidOrNot=false;
                    } 
                }
            
                if(ValidOrNot==false)
                {  
                    MainMenuSelect(rootElement,Key);
                    alert("Sorry ......... ! You Are Not Having Privilege To Access This Page. Please Contact Administrator");
                    window.navigate("../Home/Default.aspx")
                    return;
                }
            }
            return;
        }
    }
}

function SMNavigation(ItemName)
{  
    if(ItemName=="SM_1")
    {
        setUrl ="../SM/CustomerInformation.aspx";
    }
    else if(ItemName=="SM_2")
    {
        setUrl ="../SM/SalesEnquiry.aspx";
    }
    else if(ItemName=="SM_3")
    {
        setUrl ="../SM/SalesAssignments.aspx";
    }
     else if(ItemName=="SM_4")
    {
        setUrl ="../SM/SalesQuotation.aspx";
    }
    else if(ItemName=="SM_5")
    {
        setUrl ="../SM/SalesOrder.aspx";
    }
    else if(ItemName=="SM_6")
    {
         setUrl ="../SM/WorkOrder.aspx";
    }
    else if(ItemName=="SM_7")
    {
        setUrl ="../SM/ProcessStatus.aspx";
    }
   else if(ItemName=="SM_8")
    {
        setUrl ="../SM/PaymentsReceived.aspx";
    } 
     else if(ItemName=="SM_9")
    {
        setUrl ="../SM/DailyReport.aspx";
    } 
}

function SCMMenuLoad(rootElement)
{
   rootElement.findName("SCM_1").Text="Supplier Master";
   rootElement.findName("SCM_2").Text="Suppliers Enquiry";
   rootElement.findName("SCM_3").Text="Proforma Invoice";
   rootElement.findName("SCM_4").Text="Purchase Order";
   rootElement.findName("SCM_5").Text="Purchase Invoice";
   rootElement.findName("SCM_6").Text="Shipment Details";
   rootElement.findName("SCM_7").Text="Insurance Claim Form";
   rootElement.findName("SCM_8").Text="Item History";
     rootElement.findName("SCM_9").Text="Self Purchase Order";
    rootElement.findName("SCM_10").Text="POrder Search";
   
   for (var i=1;i<=10;i++)
    {  
        SCMNavigation("SCM_"+i);
      if(setUrl.toLowerCase()=="../"+currentUrl.toLowerCase())
        {   
            Key="SCM"; 
            SubKey="SCM_"+i;
            MainMenuSelect(rootElement,Key);
            SubMenuItemsSelect(rootElement,SubKey);
             if(hfp!="")
            {
                PrivilegesList=hfp.split("#|#");
            
                for(var i=0;i<=PrivilegesList.length;i++)
                {
                    if(PrivilegesList[i]==SubKey)
                    {
                        ValidOrNot=true;
                        break;
                    }
                    else
                    {
                        ValidOrNot=false;
                    } 
                }
            
                if(ValidOrNot==false)
                {
                    MainMenuSelect(rootElement,Key);
                    alert("Sorry ......... ! You Are Not Having Privilege To Access This Page. Please Contact Administrator");
                    window.navigate("../Home/Default.aspx")
                    return;
                }
            }
            return;
        }
    }
}

function SCMNavigation(ItemName)
{  
    if(ItemName=="SCM_1")
    {
        setUrl ="../SCM/SupplierMaster.aspx";
    }
    else if(ItemName=="SCM_2")
    {
        setUrl ="../SCM/SuppliersEnquiry.aspx";
    }
    else if(ItemName=="SCM_3")
    {
      setUrl ="../SCM/Quotation.aspx";
    }
    else if(ItemName=="SCM_4")
    {
        setUrl ="../SCM/FixedPurchaseOrderDetails.aspx";
    }
    else if(ItemName=="SCM_5")
    {
        setUrl ="../SCM/PurchaseInvoice.aspx";
    }
    else if(ItemName=="SCM_6")
    {
         setUrl ="../SCM/ShipmentDetails.aspx";
    }
    else if(ItemName=="SCM_7")
    {
         setUrl ="../SM/ClaimForm.aspx";
    }
    else if(ItemName=="SCM_8")
    {
         setUrl ="../SCM/ItemHistory.aspx";
    }  
     else if(ItemName=="SCM_9")
    {
         setUrl ="../SCM/SelfPurchaseOrderDetails.aspx";
    }        
      else if(ItemName=="SCM_10")
    {
         setUrl ="../SCM/SelfPurchaseOrderq.aspx";
    }        
}

function InventoryMenuLoad(rootElement)
{
   rootElement.findName("Inventory_1").Text="Indent";
//   rootElement.findName("Inventory_2").Text="Indent Approval";
   rootElement.findName("Inventory_2").Text="MRN";
   rootElement.findName("Inventory_3").Text="Stock Statement";
   rootElement.findName("Inventory_4").Text="Stock Entry";
   rootElement.findName("Inventory_5").Text="Delivery Challan";
  rootElement.findName("Inventory_6").Text="InternalOrder Approval";
   rootElement.findName("Inventory_7").Text="Reserve Stock History";
    rootElement.findName("Inventory_8").Text="Sample&Cash";
     rootElement.findName("Inventory_9").Text="Unbilled DCs";
    rootElement.findName("Inventory_10").Text="Dispatch";
 
   for (var i=1;i<=10;i++)
    {  
       InventoryNavigation("Inventory_"+i);
        if(setUrl.toLowerCase()=="../"+currentUrl.toLowerCase())
        {   
            Key="Inventory"; 
            SubKey="Inventory_"+i;
            MainMenuSelect(rootElement,Key);
            SubMenuItemsSelect(rootElement,SubKey);
            if(hfp!="")
            {
                PrivilegesList=hfp.split("#|#");
            
                for(var i=0;i<=PrivilegesList.length;i++)
                {
                    if(PrivilegesList[i]==SubKey)
                    {
                        ValidOrNot=true;
                        break;
                    }
                    else
                    {
                        ValidOrNot=false;
                    } 
                }
            
                if(ValidOrNot==false)
                {  
                    MainMenuSelect(rootElement,Key);
                    alert("Sorry ......... ! You Are Not Having Privilege To Access This Page. Please Contact Administrator");
                    window.navigate("../Home/Default.aspx")
                    return;
                }
            }
            return;
        }
    }
}


function InventoryNavigation(ItemName)
{  
    if(ItemName=="Inventory_1")
    {
    setUrl ="../SCM/ChangedIndent.aspx";
        
    }
//    else if(ItemName=="Inventory_2")
//    {
//          setUrl ="../SCM/IndentApprovedList.aspx";
//    }
    else if(ItemName=="Inventory_2")
    {
       setUrl ="../SCM/CheckingFormat.aspx";
    }
    else if(ItemName=="Inventory_3")
    {
       // setUrl ="../SCM/ItemHistory.aspx";
        setUrl ="../Inventory/StockStatement.aspx";
      // setUrl ="../Inventory/InternalOrderApproval.aspx";
    }
    else if(ItemName=="Inventory_4")
    {
       setUrl ="../SCM/ItemMaster.aspx";
    }
    else if(ItemName=="Inventory_5")
    {
       setUrl ="../Inventory/DeliveryChallan.aspx";
        
    }
     else if(ItemName=="Inventory_6")
    {
       setUrl ="../Inventory/InternalOrderApproval.aspx";
        
    }
     else if(ItemName=="Inventory_7")
    {
       setUrl ="../Inventory/ReserveStockHistory.aspx";
        
    }
     else if(ItemName=="Inventory_8")
    {
       setUrl ="../Inventory/SampleDc.aspx";
    }
     else if(ItemName=="Inventory_9")
    {
       setUrl ="../Inventory/UnbilledDC.aspx";
    }
     else if(ItemName=="Inventory_10")
    {
       setUrl ="../SM/Dispatchform.aspx";
    }
}

function SupportMenuLoad(rootElement)
{  
   rootElement.findName("Support_1").Text="Complaint Register";
   rootElement.findName("Support_2").Text="Services Assignments";
   rootElement.findName("Support_3").Text="Service Report";
   rootElement.findName("Support_4").Text="Customer Info";
   rootElement.findName("Support_5").Text="AMC Quotation";
   rootElement.findName("Support_6").Text="AMC Order";
   rootElement.findName("Support_7").Text="AMC Order Profile";
   rootElement.findName("Support_8").Text="AMC Order Acceptance ";
   rootElement.findName("Support_9").Text="Warranty Claim";
   rootElement.findName("Support_10").Text="AMC Invoice";
   rootElement.findName("Support_11").Text="AMC Payments Received";
   

   for (var i=1;i<=11;i++)
    {  
        SupportNavigation("Support_"+i);
       if(setUrl.toLowerCase()=="../"+currentUrl.toLowerCase())
        {   
            Key="Support"; 
            SubKey="Support_"+i;
            MainMenuSelect(rootElement,Key);
            SubMenuItemsSelect(rootElement,SubKey);
             if(hfp!="")
            {
                PrivilegesList=hfp.split("#|#");
            
                for(var i=0;i<=PrivilegesList.length;i++)
                {
                    if(PrivilegesList[i]==SubKey)
                    {
                        ValidOrNot=true;
                        break;
                    }
                    else
                    {
                        ValidOrNot=false;
                    } 
                }
            
                if(ValidOrNot==false)
                {
                    MainMenuSelect(rootElement,Key);
                    alert("Sorry ......... ! You Are Not Having Privilege To Access This Page. Please Contact Administrator");
                    window.navigate("../Home/Default.aspx")
                    return;
                }
            }
            return;
        }
    }
}

function SupportNavigation(ItemName)
{   
    if(ItemName=="Support_2")
    {
        setUrl ="../Services/ServicesAssignments.aspx";
    }
    else if(ItemName=="Support_3")
    {
        setUrl ="../Services/ServiceReport.aspx";       
    }
    else if(ItemName=="Support_4")
    {
        setUrl ="../Services/CustomerInformation.aspx";
    }
    else if(ItemName=="Support_5")
    {
        setUrl ="../Services/AMCQuotation.aspx";
    }
    else if(ItemName=="Support_6")
    {
        setUrl ="../Services/AMCOrder.aspx";
    }
     else if(ItemName=="Support_7")
    {
        setUrl ="../Services/AMCWorkOrder.aspx";
    } 
    else if(ItemName=="Support_8")
    {
        setUrl ="../Services/AMCOrderAcceptance.aspx";
    }  
    else if(ItemName=="Support_9")
    {
        setUrl ="../Services/WarrantyClaim.aspx";
    }
      else if(ItemName=="Support_1")
    {
        setUrl ="../Services/ComplaintRegister.aspx";
    }

       else if(ItemName=="Support_10")
    {
        setUrl ="../Services/AMCInvoice.aspx";
    }
       else if(ItemName=="Support_11")
    {
        setUrl ="../Services/AMCPaymentsReceived.aspx";
    }
    
}



function FinanceMenuLoad(rootElement)
{
   rootElement.findName("Finance_1").Text="Sales Invoice";
   rootElement.findName("Finance_2").Text="Sales Return";
   rootElement.findName("Finance_3").Text="Payments Received from Sales";
   rootElement.findName("Finance_4").Text="Payments Received from Services";
   rootElement.findName("Finance_5").Text="Statement Of Account";
    rootElement.findName("Finance_6").Text="Sample&Cash Invoice";
     rootElement.findName("Finance_7").Text="Sample Return";
   
 
   for (var i=1;i<=7;i++)
    {  
        FinanceNavigation("Finance_"+i);
        if(setUrl.toLowerCase()=="../"+currentUrl.toLowerCase())
        {   
            Key="Finance"; 
            SubKey="Finance_"+i;
            MainMenuSelect(rootElement,Key);
            SubMenuItemsSelect(rootElement,SubKey);
             if(hfp!="")
            {
                PrivilegesList=hfp.split("#|#");
            
                for(var i=0;i<=PrivilegesList.length;i++)
                {
                    if(PrivilegesList[i]==SubKey)
                    {
                        ValidOrNot=true;
                        break;
                    }
                    else
                    {
                        ValidOrNot=false;
                    } 
                }
            
                if(ValidOrNot==false)
                {
                    MainMenuSelect(rootElement,Key);
                    alert("Sorry ......... ! You Are Not Having Privilege To Access This Page. Please Contact Administrator");
                    window.navigate("../Home/Default.aspx")
                    return;
                }
            }
            return;
        }
    }
}

function FinanceNavigation(ItemName)
{  
    if(ItemName=="Finance_1")
    {
        setUrl ="../Inventory/Invoice.aspx";
    }
    else if(ItemName=="Finance_2")
    {
        setUrl ="../SCM/SalesReturn.aspx";
    }
     else if(ItemName=="Finance_3")
    {
        setUrl ="../Inventory/PaymentsReceived.aspx";
    }
     else if(ItemName=="Finance_4")
    {
        setUrl ="../Inventory/Payments.aspx";
    }
     else if(ItemName=="Finance_5")
    {
        setUrl ="../Inventory/StatementOfAccount1.aspx";
    }
    
     else if(ItemName=="Finance_6")
    {
        setUrl ="../Inventory/CashInvoice.aspx";
    }
    
     else if(ItemName=="Finance_7")
    {
        setUrl ="../SCM/SampleReturn.aspx";
    }
    
    
    
    
    
}

function HRMenuLoad(rootElement)
{
   rootElement.findName("HR_1").Text="Employee Master";
   rootElement.findName("HR_2").Text="Memo";
   rootElement.findName("HR_3").Text="Circular";
   rootElement.findName("HR_4").Text="Offer Letter";
   rootElement.findName("HR_5").Text="Salary Breakups";
   
   
   for (var i=1;i<=5;i++)
    {  
        HRNavigation("HR_"+i);
        if(setUrl.toLowerCase()=="../"+currentUrl.toLowerCase())
        {   
            Key="HR"; 
            SubKey="HR_"+i;
            MainMenuSelect(rootElement,Key);
            SubMenuItemsSelect(rootElement,SubKey);
             if(hfp!="")
            {
                PrivilegesList=hfp.split("#|#");
            
                for(var i=0;i<=PrivilegesList.length;i++)
                {
                    if(PrivilegesList[i]==SubKey)
                    {
                        ValidOrNot=true;
                        break;
                    }
                    else
                    {
                        ValidOrNot=false;
                    } 
                }
            
                if(ValidOrNot==false)
                {
                    MainMenuSelect(rootElement,Key);
                    alert("Sorry ......... ! You Are Not Having Privilege To Access This Page. Please Contact Administrator");
                    window.navigate("../Home/Default.aspx")
                    return;
                }
            }
            return;
        }
    }
}

function HRNavigation(ItemName)
{  
    if(ItemName=="HR_1")
    {
        setUrl ="../HR/EmployeeMaster.aspx";
    }
    else if(ItemName=="HR_2")
    {
        setUrl ="../HR/Memo.aspx";
    }
    else if(ItemName=="HR_3")
    {
        setUrl ="../HR/Circular.aspx";
    }  
     else if(ItemName=="HR_4")
    {
        setUrl ="../HR/OfferLetter.aspx";
    }  
    else if(ItemName=="HR_5")
    {
        setUrl ="../HR/SalaryPrint.aspx";
    }  

}
function ReportsMenuLoad(rootElement)
{
    rootElement.findName("Reports_1").Text="SM EOD Reports";
    
    rootElement.findName("Reports_2").Text="SCM EOD Reports";
    rootElement.findName("Reports_3").Text="Services EOD Reports";
    for (var i=1;i<=3;i++)
    {  
        ReportsNavigation("Reports_"+i);
        if(setUrl.toLowerCase()=="../"+currentUrl.toLowerCase())
        {   
            Key="Reports"; 
            SubKey="Reports_"+i;
            MainMenuSelect(rootElement,Key);
            SubMenuItemsSelect(rootElement,SubKey);
            if(hfp!="")
            {
                PrivilegesList=hfp.split("#|#");
            
                for(var i=0;i<=PrivilegesList.length;i++)
                {
                    if(PrivilegesList[i]==SubKey)
                    {
                        ValidOrNot=true;
                        break;
                    }
                    else
                    {
                        ValidOrNot=false;
                    } 
                }
            
                if(ValidOrNot==false)
                {
                    MainMenuSelect(rootElement,Key);
                    alert("Sorry ......... ! You Are Not Having Privilege To Access This Page. Please Contact Administrator");
                    window.navigate("../Home/Default.aspx")
                    return;
                }
            }
            return;
        }
    }
}

function ReportsNavigation(ItemName)
{  
    if(ItemName=="Reports_1")
    {
        setUrl ="../Reports/SM.aspx";
    }
    else if(ItemName=="Reports_2")
    {
        setUrl ="../Reports/SCM.aspx";
    }
     else if(ItemName=="Reports_3")
    {
        setUrl ="../Reports/Services.aspx";
    }
}

function HelpMenuLoad(rootElement)
{
    rootElement.findName("Help_1").Text="Help";
    
    for (var i=1;i<=1;i++)
    {  
      HelpNavigation("Help_"+i);
        if(setUrl.toLowerCase()=="../"+currentUrl.toLowerCase())
        {   
            Key="Help"; 
            SubKey="Help_"+i;
            MainMenuSelect(rootElement,Key);
            SubMenuItemsSelect(rootElement,SubKey);
            if(hfp!="")
            {
                PrivilegesList=hfp.split("#|#");
            
                for(var i=0;i<=PrivilegesList.length;i++)
                {
                    if(PrivilegesList[i]==SubKey)
                    {
                        ValidOrNot=true;
                        break;
                    }
                    else
                    {
                        ValidOrNot=false;
                    } 
                }
            
                if(ValidOrNot==false)
                {
                    MainMenuSelect(rootElement,Key);
                    alert("Sorry ......... ! You Are Not Having Privilege To Access This Page. Please Contact Administrator");
                    window.navigate("../Home/Default.aspx")
                    return;
                }
            }
            return;
        }
    }
}

function HelpNavigation(ItemName)
{  
    if(ItemName=="Help_1")
    {
        setUrl ="../Reports/YANTRA Help.pdf";
    }
   
}










// To De-Select all the Labels and its rectangles
function DeSelect(sender)
{
    sender.findName("Master_Rectangle").width="0";   
    sender.findName("SM_Rectangle").width="0";   
    sender.findName("SCM_Rectangle").width="0";   
    sender.findName("Inventory_Rectangle").width="0";   
    sender.findName("Support_Rectangle").width="0";   
     sender.findName("Finance_Rectangle").width="0";    
    sender.findName("HR_Rectangle").width="0";   
    sender.findName("Reports_Rectangle").width="0"; 
   sender.findName("Help_Rectangle").width="0";  
    
    sender.findName("Master_Menu_Canvas").Opacity=0;   
    sender.findName("SM_Menu_Canvas").Opacity=0;   
    sender.findName("SCM_Menu_Canvas").Opacity=0;   
    sender.findName("Inventory_Menu_Canvas").Opacity=0;   
    sender.findName("Support_Menu_Canvas").Opacity=0;   
   sender.findName("Finance_Menu_Canvas").Opacity=0;    
    sender.findName("HR_Menu_Canvas").Opacity=0;   
    sender.findName("Reports_Menu_Canvas").Opacity=0; 
   sender.findName("Help_Menu_Canvas").Opacity=0;  
    
    sender.findName("Master").Foreground="White";
    sender.findName("SM").Foreground="White";
    sender.findName("SCM").Foreground="White";
    sender.findName("Inventory").Foreground="White";
    sender.findName("Support").Foreground="White";
    sender.findName("Finance").Foreground="White";
    sender.findName("HR").Foreground="White";
    sender.findName("Reports").Foreground="White";
     sender.findName("Help").Foreground="White";
     
    
    sender.findName("Master_Menu_Canvas")["Canvas.Top"]="140";  
    sender.findName("SM_Menu_Canvas")["Canvas.Top"]="140";  
    sender.findName("SCM_Menu_Canvas")["Canvas.Top"]="140";  
    sender.findName("Inventory_Menu_Canvas")["Canvas.Top"]="140";  
    sender.findName("Support_Menu_Canvas")["Canvas.Top"]="140";  
   sender.findName("Finance_Menu_Canvas")["Canvas.Top"]="140";   
    sender.findName("HR_Menu_Canvas")["Canvas.Top"]="140";  
    sender.findName("Reports_Menu_Canvas")["Canvas.Top"]="140";
     sender.findName("Help_Menu_Canvas")["Canvas.Top"]="140"; 
}

function LoadMainMenuHeads(rootElement)
{  
    var widthcount;
    widthcount = rootElement.findName("Master").Actualwidth;
    widthcount=widthcount+10;
    rootElement.findName("Pipe1")["Canvas.Left"]=widthcount;
    widthcount=widthcount+5;    
    rootElement.findName("SM_Canvas")["Canvas.Left"]=widthcount;
     rootElement.findName("Pipe2")["Canvas.Left"]=widthcount-5;
    widthcount=widthcount+15;
    widthcount =widthcount +rootElement.findName("SM").Actualwidth;
    rootElement.findName("SCM_Canvas")["Canvas.Left"]=widthcount;
    rootElement.findName("Pipe3")["Canvas.Left"]=widthcount-5;
    widthcount=widthcount+15;
    widthcount =widthcount +rootElement.findName("SCM").Actualwidth;
    rootElement.findName("Inventory_Canvas")["Canvas.Left"]=widthcount;
    rootElement.findName("Pipe4")["Canvas.Left"]=widthcount-5;
    widthcount=widthcount+15;
    widthcount =widthcount +rootElement.findName("Inventory").Actualwidth;
    rootElement.findName("Support_Canvas")["Canvas.Left"]=widthcount;
    rootElement.findName("Pipe5")["Canvas.Left"]=widthcount-5;
    widthcount=widthcount+15;
    widthcount =widthcount +rootElement.findName("Support").Actualwidth;
    rootElement.findName("HR_Canvas")["Canvas.Left"]=widthcount;
    rootElement.findName("Pipe6")["Canvas.Left"]=widthcount-5;
    widthcount=widthcount+15;
     widthcount =widthcount +rootElement.findName("HR").Actualwidth;
    rootElement.findName("Finance_Canvas")["Canvas.Left"]=widthcount;
    rootElement.findName("Pipe7")["Canvas.Left"]=widthcount-5;
    widthcount=widthcount+15; 
    widthcount =widthcount +rootElement.findName("Finance").Actualwidth;
    rootElement.findName("Reports_Canvas")["Canvas.Left"]=widthcount;
    rootElement.findName("Pipe8")["Canvas.Left"]=widthcount-5;
    widthcount=widthcount+15;
    widthcount =widthcount +rootElement.findName("Reports").Actualwidth;
    rootElement.findName("Help_Canvas")["Canvas.Left"]=widthcount;
    rootElement.findName("Pipe9")["Canvas.Left"]=widthcount-5;
    widthcount=widthcount+15;
    widthcount =widthcount +rootElement.findName("Help").Actualwidth;
}
