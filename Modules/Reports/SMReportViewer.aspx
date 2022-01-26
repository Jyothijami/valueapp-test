<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SMReportViewer.aspx.cs" Inherits="Modules_Reports_SMReportViewer" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link rel="stylesheet" type="text/css" />
   <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <script src="/crystalreportviewers13/js/crviewer/crv.js" type="text/javascript"></script>
      
  
</head>
<body>
    <form id="form1" runat="server">
 <div style="text-align: left ; background-color: #ffffff;">
       <br />
     &nbsp;&nbsp;
        </div> 
        <div style="text-align: center; background-color: #ffffff;">
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ReuseParameterValuesOnRefresh="True" />
            <%-- <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
             <%--EnableDatabaseLogonPrompt="False" EnableDrillDown="False"
            EnableParameterPrompt="False" HasCrystalLogo="False" HasDrillUpButton="False"
            HasGotoPageButton="False" HasPageNavigationButtons="False" HasRefreshButton="True"
            HasSearchButton="False" PrintMode="ActiveX" Height="50px" SeparatePages="False" ShowAllPageIds="True" Width="350px"--%></div>
    </form>
</body>
</html>

 
