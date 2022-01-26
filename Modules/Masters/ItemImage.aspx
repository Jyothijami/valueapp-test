<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ItemImage.aspx.cs" Inherits="Modules_Masters_ItemImage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 725px">
            <tr>
                <td class="searchhead" colspan="4" style="height: 38px; text-align: left">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 272px; text-align: left">
                                Item&nbsp; Image</td>
                            <td style="text-align: right">
                                &nbsp;</td>
                        </tr>
                    </table>
                    <asp:Label ID="lblCPID" runat="server" Visible="False"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="4" style="height: 19px; text-align: right">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td rowspan="3">
                                <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                    Text="Search By" Width="86px"></asp:Label></td>
                            <td rowspan="3">
                                <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="textbox">
                                    <asp:ListItem Value="0">--</asp:ListItem>
                                    <asp:ListItem Value="ITEM_MODEL_NO">ModelNo</asp:ListItem>
                                    <asp:ListItem Value="ITEM_CATEGORY_NAME">Category</asp:ListItem>
                                    <asp:ListItem Value="IT_TYPE_ID">SubCategory</asp:ListItem>
                                    <asp:ListItem Value="PRODUCT_COMPANY_NAME">Brand</asp:ListItem>
                                </asp:DropDownList></td>
                            <td rowspan="3">
                            </td>
                            <td rowspan="3">
                                <asp:TextBox ID="txtSearchText" runat="server" CssClass="textbox">
                                        </asp:TextBox></td>
                            <td rowspan="3">
                                <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                    CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                        </tr>
                        <tr>
                        </tr>
                        <tr>
                        </tr>
                    </table>
                    <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="4" style="height: 21px; text-align: center">
                    <asp:GridView ID="gvProductMasterDetails" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" DataSourceID="sdsProductMaster" OnRowDataBound="gvProductMasterDetails_RowDataBound"
                        Width="100%">
                        <Columns>
                            <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" ReadOnly="True" />
                            <asp:TemplateField HeaderText="Model No">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ITEM_NAME") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Text='<%# Eval("ITEM_MODEL_NO") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ITEM_NAME" HeaderText="Item Name" />
                            <asp:BoundField DataField="ITEM_CATEGORY_NAME" HeaderText="Category" />
                            <asp:BoundField DataField="IT_TYPE" HeaderText="SubCategory" />
                            <asp:BoundField DataField="PRODUCT_COMPANY_NAME" HeaderText="Brand" />
                            <asp:BoundField DataField="ITEM_SPEC" HeaderText="Specification" />
                            <asp:TemplateField HeaderText="Item Image">
                                <ItemTemplate>
                                    <asp:Image ID="Image" runat="server" Height="46px" ImageUrl='~/Images/noimage400x300.gif'
                                        Width="67px" EnableTheming="False" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <SelectedRowStyle BackColor="LightSteelBlue" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsProductMaster" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                        SelectCommand="SP_MASTER_ITEMMASTERIMAGE_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName"
                                PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue"
                                PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <table id="tblPMDetails" runat="server" border="0" cellpadding="0" cellspacing="0"
                        visible="true" width="100%">
                        <tr>
                            <td style="height: 27px; text-align: right">
                            </td>
                            <td style="height: 27px; text-align: left">
                                &nbsp;<asp:FileUpload ID="FileUpload1" runat="server" Width="527px" /></td>
                            <td style="height: 27px; text-align: right">
                            </td>
                            <td style="height: 27px; text-align: left">
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                            </td>
                            <td style="text-align: left">
                                &nbsp;<asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload" /></td>
                            <td style="text-align: right">
                            </td>
                            <td style="text-align: left">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

 
