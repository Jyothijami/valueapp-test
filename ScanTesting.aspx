<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ScanTesting.aspx.cs" Inherits="ScanTesting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
<tr>
<td align="right">
Enter path to store file:
</td>
<td>
<asp:TextBox ID="txtpath" runat="server"></asp:TextBox>
</td>
</tr>
<tr>
<td>
</td>
<td>
<asp:Label ID="lbltxt" runat="server" ForeColor="Red"></asp:Label>
</td>
</tr>
<tr>
<td>
</td>
<td>
<asp:FileUpload ID="fileupload1" runat="server" />
</td>
</tr>
<tr>
<td></td>
<td>
<asp:Button ID="btnRead" runat="server" onclick="btnRead_Click" Text="Read" />
</td>
</tr>
<tr>
<td valign="top">
Message of Created Text file :
</td>
<td>
<asp:TextBox ID="textBoxContents" runat="server" tabIndex="0" height="100px" textMode="MultiLine" width="250px"></asp:TextBox>
</td>
</tr>
</table>
        <asp:TextBox ID="txtItemCode" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtBrand" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtModelNo" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtColorName" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtQty" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtRemarks" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtClientName" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtBrandId" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtColorId" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtRemarks1" runat ="server" Visible ="false" ></asp:TextBox>


        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </div>
        <div>
            <asp:GridView ID="gvMovingItems" runat="server" AutoGenerateColumns="False" Style="text-align: center" Width="100%">
                                        <Columns>

                                            <asp:BoundField DataField="ItemCode" HeaderText="Itemcode" />
                                            <asp:BoundField DataField="Brand" HeaderText="Brand" />
                                            <asp:BoundField DataField="ModelNo" HeaderText="ModelNo" />
                                            <asp:BoundField DataField="Color" HeaderText="Color" />
                                            <%--<asp:BoundField DataField="Qty" HeaderText="Qty" />--%>
                                            <asp:TemplateField HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Bind("Qty") %>'>></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="Moving Location">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddllocation" runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                            <asp:BoundField DataField="Remarks" HeaderText="Description" >
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ClientName" HeaderText="ClientName" />
                                            <asp:BoundField DataField="BrandId" HeaderText="BrandId" />
                                            <asp:BoundField DataField="ColorId" HeaderText="ColorId" />
                                            <asp:TemplateField HeaderText="Remarks" ControlStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRemarks" runat="server" Text='<%# Bind("Remark") %>'>></asp:TextBox>
                                                </ItemTemplate>
                                                <ControlStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                        </Columns>
                                    </asp:GridView>
        </div>
    </form>
</body>
</html>
