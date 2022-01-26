<%@ Page Language="C#" AutoEventWireup="true" CodeFile="waste10.aspx.cs" Inherits="waste10" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="jquery-easyui-1.4.1/themes/default/easyui.css" rel="stylesheet" />
    <link href="jquery-easyui-1.4.1/themes/icon.css" rel="stylesheet" />
    <link href="jquery-easyui-1.4.1/demo/demo.css" rel="stylesheet" />
    <script src="js/jquery-1.9.1.js"></script>
    <script src="jquery-easyui-1.4.1/jquery.easyui.min.js"></script>
<%--    <script src="jquery-easyui-1.4.1/jquery.min.js"></script>--%>

<%--    <link href="js/combotree/demo.css" rel="stylesheet" />
    <link href="js/combotree/easyui.css" rel="stylesheet" />
    <link href="js/combotree/icon.css" rel="stylesheet" />
    <script src="js/combotree/jquery.min.js"></script>
    <script src="js/combotree/jquery.easyui.min.js"></script>--%>

    	<script type="text/javascript">
    	    function getValue() {
    	        var val = $('[id$="TextBox2"]').combotree('getValue');
    	        //alert(val);
    	    }
    	    function setValue() {
    	        $('[id$="TextBox2"]').combotree('setValue', $('[id$="TextBox2_value"]').val());
    	    }
    	    function disable() {
    	        $('[id$="TextBox2"]').combotree('disable');
    	    }
    	    function enable() {
    	        $('[id$="TextBox2"]').combotree('enable');
    	    }

    	    $(document).ready(function () {
    	        $('[id$="TextBox2"]').combotree({
    	            url: '/tree_data1.json',
    	            method: 'get',
    	            required: true
    	        });

    	        $('[id$="TextBox2"]').combotree('setValue', "161");
    	    });

	</script>
</head>
<body>
    <form id="form1" runat="server">
<h2>ComboTree Actions</h2>
	<p>Click the buttons below to perform actions</p>
	<div style="margin:20px 0">
		<a href="javascript:void(0)" class="easyui-linkbutton" onclick="getValue()">GetValue</a>
		<a href="javascript:void(0)" class="easyui-linkbutton" onclick="setValue()">SetValue</a>
		<a href="javascript:void(0)" class="easyui-linkbutton" onclick="disable()">Disable</a>
		<a href="javascript:void(0)" class="easyui-linkbutton" onclick="enable()">Enable</a>
	</div>
<%--        <asp:DropDownList ID="DropDownList1" runat="server" class="easyui-combotree" data-options="url:'/tree_data1.json',method:'get',required:true" style="width:200px;"></asp:DropDownList>
        <asp:HiddenField ID="ddltxtval" runat="server" />
        <select id="cc2" class="easyui-combotree" data-options="url:'/tree_data1.json',method:'get',required:true" style="width:200px;"></select>--%>
<%--	<input id="cc" class="easyui-combotree" data-options="url:'/tree_data1.json',method:'get',required:true" style="width:200px;">--%>
        <asp:TextBox ID="TextBox2" runat="server" style="width:200px;" Text="eloi"></asp:TextBox>
        <asp:HiddenField ID="TextBox2_value" runat="server" />
        <asp:HiddenField ID="TextBox2_text" runat="server" />
	<script type="text/javascript">
	    $(document).ready(function () {
	        $('.textbox-text').val($("[name$='TextBox2_text']").val());
	    });

	</script>

        <br />
        <br />

        <asp:TextBox ID="TextBox1" runat="server" Height="330px" TextMode="MultiLine" Width="642px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        <br />
        <asp:TextBox ID="txtval1" runat="server"></asp:TextBox>
    </form>
</body>
</html>
