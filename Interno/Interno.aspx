<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Interno.aspx.cs" Inherits="Interno_Interno" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:GridView ID="gvInterno" runat ="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="Id" DataSourceID="SqlDataSource1" >
            <Columns >
                <asp:BoundField DataField ="Id" HeaderText ="Id" />

                <asp:TemplateField HeaderText="Id" SortExpression="Id"><EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("Id") %>' ID="TextBox1"></asp:TextBox>
                            
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnIndentNo" onclick="lbtnIndentNo_Click" runat="server" ForeColor="#0066ff" Text='<%# Eval("Id") %>' CausesValidation="False" __designer:wfdid="w11"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>

                <asp:BoundField DataField ="Code" HeaderText ="Code" />
                <asp:BoundField DataField ="Date" HeaderText ="Date" />
                <asp:BoundField DataField ="Name" HeaderText ="Cust name" />
            </Columns>
        </asp:GridView>
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" 
           SelectCommand="SELECT * FROM [Interno_Mast] order by ID desc"></asp:SqlDataSource>
        <table >
            <tr>
            <td style="text-align: right; font-weight: bold">Upload Biometric Excel :</td>
            <td style="text-align: left" colspan="2">
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td></td>
            <td colspan="3" style="text-align:left">
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Upload" />
                <asp:Button ID="btnPrint" runat ="server" OnClick ="btnPrint_Click" Text="Print" />

            </td>
        </tr>
            <tr>
                                <td colspan="4">
                                    <table id="tblpRint" runat="server" visible="false">
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkIn" runat="server" OnCheckedChanged="chkIn_CheckedChanged"
                                                    Text="Invoice" AutoPostBack="True"></asp:CheckBox></td>
                                            <td>
                                                <asp:CheckBox ID="chkPL" runat="server" Text="Packing List" AutoPostBack="True" OnCheckedChanged="chkPL_CheckedChanged"></asp:CheckBox></td>
                                              </tr>
                                    </table>
                                </td>
                            </tr>
        </table>
    </div>
    </form>
</body>
</html>
