<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QRCodeReader.aspx.cs" Inherits="Modules_Inventory_QRCodeReader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.5.1.min.js"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $("#GridView1 tr td:last").live("keydown", function (e) {
                var keyCode = e.keyCode || e.which;
                if (keyCode == 9) {
                    $("#GridView1 tr:last").after("<tr><td><input type='text' /></td><td><input type='text' /></td><td><input type='text' /></td></tr>");
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            EmptyDataText="There are no data records to display.">
            <Columns>
                <asp:BoundField HeaderText="id" SortExpression="id" />
                <asp:BoundField HeaderText="time" SortExpression="time" />
                <asp:BoundField HeaderText="min" SortExpression="min" />
            </Columns>
        </asp:GridView>
       <asp:GridView ID="grd" runat="server" DataKeyNames="PayScale" AutoGenerateColumns="false">
    <Columns>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Pay Scale">
            <ItemTemplate>
                <asp:TextBox ID="txtPayScale" runat="server" Text='<%# Eval("PayScale") %>'></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Increment Amount">
            <ItemTemplate>
                <asp:TextBox ID="txtIncrementAmount" runat="server" Text='<%# Eval("IncrementAmount") %>'></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Period">
            <ItemTemplate>
                <asp:TextBox ID="txtPeriod" runat="server" Text='<%# Eval("Period") %>'></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
    </div>
    </form>
</body>
</html>
