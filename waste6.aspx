<%@ Page Language="C#" AutoEventWireup="true" CodeFile="waste6.aspx.cs" Inherits="waste6" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
        <script src="js/jquery-1.9.1.js"></script>
        <script src="bootstrap/js/bootstrap.min.js"></script>
        <script src="site_resources/scripts/bootstrap-multiselect.js"></script>
        <link href="site_resources/css/bootstrap-multiselect.css" rel="stylesheet" /><!-- Initialize the plugin: -->
<script type="text/javascript">
    $(document).ready(function () {
        $('.multiselect').multiselect();
    });
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        

 
<!-- Build your select: -->
<select class="multiselect" multiple="multiple">
<option value="cheese">Cheese</option>
<option value="tomatoes">Tomatoes</option>
<option value="mozarella">Mozzarella</option>
<option value="mushrooms">Mushrooms</option>
<option value="pepperoni">Pepperoni</option>
<option value="onions">Onions</option>
</select>
 

        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
 

    </div>
    </form>
</body>
</html>
