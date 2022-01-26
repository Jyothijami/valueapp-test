<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReadingBarcode.aspx.cs" Inherits="ReadingBarcode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <div>
            <asp:TextBox ID="txtItemCode" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtModelNo" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtChkNo" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtChkDt" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtChKDetColor" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtColorName" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtQty" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtPrintQty" runat ="server" Visible ="false" ></asp:TextBox>

        </div>

    <div>
        <asp:Label ID="Label3" runat="server" Text="Notext"  ></asp:Label>
        <%--<asp:Button ID="btnAddRow" runat="server" OnClick="btnAddRow_Click" Text="Add Row" />--%>
    <asp:TextBox ID="txtQR" runat ="server" OnTextChanged ="txtQR_TextChanged" AutoPostBack ="true"  ></asp:TextBox>
        <%--<asp:Button ID="btnAdd" Visible ="false"  runat="server" BackColor="Transparent" BorderStyle="None"
                                                    CssClass="loginbutton" EnableTheming="False" OnClick="btnAdd_Click" Text="Add"
                                                    ValidationGroup="ip" />--%>

        <asp:GridView ID="grd" runat ="server" AutoGenerateColumns ="false" >
            <Columns >
                <asp:BoundField DataField ="Item_Code" HeaderText ="Item Code" />
                <asp:BoundField DataField ="ITEM_Model_No" HeaderText ="ITEM_Model_No" />
                <asp:BoundField DataField ="CHK_NO" HeaderText ="CHK_NO" />
                <asp:BoundField DataField ="CHK_DATE" HeaderText ="CHK_DATE" />
                <asp:BoundField DataField ="CHK_DET_Color" HeaderText ="CHK_DET_Color" />
                <asp:BoundField DataField ="COlour_NAme" HeaderText ="COlour_NAme" />
                <asp:BoundField DataField ="Qty" HeaderText ="Quantity" />
                <asp:BoundField DataField ="PrintQty" HeaderText ="No.of Prints taken" />

            </Columns>
        </asp:GridView>



           <%--<asp:GridView ID="grd" runat="server" DataKeyNames="Item_Code" AutoGenerateColumns="false">
    <Columns>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Item Code">
            <ItemTemplate>
                <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("Item_Code") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Model No">
            <ItemTemplate>
                <asp:Label ID="lblModelNo" runat="server" Text='<%# Eval("ITEM_Model_No") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="MRN NO">
            <ItemTemplate>
                <asp:label ID="lblMRNNo"  runat="server" Text='<%# Eval("CHK_NO") %>'></asp:label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="MRN Dt">
            <ItemTemplate>
                <asp:Label ID="lblMRNDt"  runat="server" Text='<%# Eval("CHK_DATE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="ColorID">
            <ItemTemplate>
                <asp:label ID="lblColorId"  runat="server" Text='<%# Eval("CHK_DET_Color") %>'></asp:label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Colour">
            <ItemTemplate>
                <asp:label ID="lblColour"  runat="server" Text='<%# Eval("COlour_NAme") %>'></asp:label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Qty">
            <ItemTemplate>
                <asp:Textbox ID="txtQty"  runat="server" Text='<%# Eval("CHK_DET_NetQty") %>'></asp:Textbox>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>--%>

        <asp:Button ID="btnSave" runat ="server" Text ="Save" />
        
        <%--<asp:FileUpload ID="fileupload" runat="server" AllowMultiple="true" />--%>  
<%--<asp:Button ID="createzip" Text="ZipFile" runat="server" OnClick="createzip_Click" />--%>  
<asp:Label ID="Label1" runat="server" ForeColor="#CC0000" />  
<asp:Label ID="Label2" runat="server" ForeColor="#CC0000" />  


    </div>
    </form>
</body>
</html>
