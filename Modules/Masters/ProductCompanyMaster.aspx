<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductCompanyMaster.aspx.cs" Inherits="Modules_Masters_ProductCompanyMaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Modules/Masters/ProductCompany.ascx"  TagPrefix="UC" TagName="ProductCompany" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Yantra : Product : Company</title>
    <link href="../../App_Themes/Master/Master.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: left">
                <UC:ProductCompany runat="server" ID="ucProductCompany"  />
                
    </div>
    </form>
</body>
</html>

 
