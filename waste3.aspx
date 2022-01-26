<%@ Page Language="C#" AutoEventWireup="true" CodeFile="waste3.aspx.cs" Inherits="waste3" %>

<%@ Register src="Modules/widgets/SalesLead_OpenVsClose.ascx" tagname="SalesLead_OpenVsClose" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="/js/jquery-1.9.1.js"></script>
    <script src="/js/jquery-ui-1.10.0.custom.js"></script>
    <link href="/js/ui/1.10.2/themes/smoothness/jquery-ui.css" rel="stylesheet" />
    <script src="/js/ui/1.10.2/jquery-ui.js"></script>
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <link href="/js/ui/1.10.1/themes/base/jquery-ui.css" rel="stylesheet" />
        
    <link href="BAT_files/sticky.css" rel="stylesheet" />
    <script src="BAT_files/sticky.min.js"></script>
    <script src="BAT_files/beoro_notifications.js"></script>

    <script type="text/javascript">
        $(function () {
            $.sticky("Lorem ipsum dolor sit&hellip;", { autoclose: 3000, position: "top-right", type: "st-success" });
            alert("asdasd");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    Testing this page
        <br />
    </div>
    </form>
</body>
</html>
