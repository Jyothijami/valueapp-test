<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Barcodetest.aspx.cs" Inherits="Barcodetest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       
         <asp:TextBox ID="txtCode" runat="server"></asp:TextBox> <br />
          <asp:TextBox ID="txt2" runat="server"></asp:TextBox> <br />
          <asp:TextBox ID="txt3" runat="server"></asp:TextBox> <br />
          <asp:TextBox ID="txt4" runat="server"></asp:TextBox> <br />



    <asp:Button ID="btnGenerate" runat="server" Text="Generate" OnClick="btnGenerate_Click" />
    <hr />
    <asp:PlaceHolder ID="plBarCode" runat="server" />






     <%--    <asp:gridview ID="Gridview1" runat="server" ShowFooter="true" AutoGenerateColumns="false">
            <Columns>
            <asp:BoundField DataField="RowNumber" HeaderText="Row Number" />
            <asp:TemplateField HeaderText="Item Code">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Model No">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Brand">
                <ItemTemplate>
                     <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                 <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" 
                        onclick="ButtonAdd_Click" />
                </FooterTemplate>
            </asp:TemplateField>
            </Columns>
        </asp:gridview>--%>


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
                <asp:TextBox ID="txtPeriod" OnTextChanged="txtPeriod_TextChanged" runat="server" Text='<%# Eval("Period") %>'></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<asp:Button ID="btnAddRow" runat="server" OnClick="btnAddRow_Click" Text="Add Row" />


      
    </div>
    </form>
</body>
</html>
