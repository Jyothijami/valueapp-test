<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="WarehosueIndent.aspx.cs" Inherits="Modules_Warehouse_WarehosueIndent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <asp:UpdatePanel runat="server"><ContentTemplate>
    <div class="container">
    
        <table width="100%">
            <tr>
                <td>Internal Indent</td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvInternalIndent" runat="server" Width="100%" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="INT_INDID" HeaderText="Slno" />
                            <asp:TemplateField HeaderText="Indent No">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Text="<%# Bind('IND_NO') %>"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="IND_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date" HtmlEncode="False" />
                            <asp:BoundField DataField="whname" HeaderText="Indent To" />
                            <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Prepared By" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <table align="center">
                        <tr>
                            <td>
                                <asp:Button ID="btnAddnew" runat="server" Text="Add New" OnClick="btnAddnew_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" runat="server" visible="false" id="tblmain">
                        <tr>
                            <td colspan="4" style="text-align: left">Add Internal Details</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="Label1" runat="server" Text="Indent No :"></asp:Label>
                            </td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtIndentno" runat="server"></asp:TextBox>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="Label2" runat="server" Text="Indent Date :"></asp:Label>
                            </td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtindentdate" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="Label3" runat="server" Text="Indent From :"></asp:Label>
                            </td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="ddlfrom" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="Label4" runat="server" Text="Indent To :"></asp:Label>
                            </td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="ddlto" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="text-left">Add Indent Details</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="Label5" runat="server" Text="Brand :"></asp:Label>
                            </td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="ddlBrand" runat="server" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="Label6" runat="server" Text="Model No :"></asp:Label>
                            </td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="ddlModelno" runat="server" OnSelectedIndexChanged="ddlModelno_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="Label7" runat="server" Text="Color :"></asp:Label>
                            </td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="ddlColor" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="Label8" runat="server" Text="Qty :"></asp:Label>
                            </td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtQty" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="Label11" runat="server" Text="Client Name :"></asp:Label>
                            </td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtClientName" runat="server"></asp:TextBox>
                            </td>
                            <td style="text-align: right">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="Label9" runat="server" Text="Remarks :"></asp:Label>
                            </td>
                            <td colspan="3" style="text-align: left">
                                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="494px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: right">
                                <table  width="100%">
                                    <tr>
                                        <td align="right">
                                            <asp:Button ID="btnadd" runat="server" Text="Add" OnClick="btnadd_Click" />
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Button ID="btnrefresh" runat="server" Text="Refresh" OnClick="btnrefresh_Click" />
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td colspan="2">
                    <asp:GridView ID="gvIndentdetails" runat="server" AutoGenerateColumns="False" style="text-align: center" OnRowDeleting="gvIndentdetails_RowDeleting">
                        <Columns>
                            <asp:CommandField ShowDeleteButton="True" />
                            <asp:BoundField DataField="ItemCode" HeaderText="Itemcode" />
                            <asp:BoundField DataField="Brand" HeaderText="Brand" />
                            <asp:BoundField DataField="ModelNo" HeaderText="ModelNo" />
                            <asp:BoundField DataField="Color" HeaderText="Color" />
                            <asp:BoundField DataField="Qty" HeaderText="Qty" />
                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                            <asp:BoundField DataField="ClientName" HeaderText="ClientName" />
                            <asp:BoundField DataField="BrandId" HeaderText="BrandId" />
                            <asp:BoundField DataField="ColorId" HeaderText="ColorId" />
                        </Columns>
                    </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td colspan="2">
                                            <table class="auto-style1">
                                                <tr>
                                                    <td style="text-align: right">
                                <asp:Label ID="Label10" runat="server" Text="Prepared By :"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                <asp:DropDownList ID="ddlPreparedBy" runat="server" style="margin-left: 0px" Enabled="False">
                                </asp:DropDownList>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="text-align: right">
                                <table align="center" >
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnrefresh2" runat="server" Text="Refresh" OnClick="btnrefresh2_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnExit" runat="server" Text="Exit" OnClick="btnExit_Click" />
                                        </td>
                                    </tr>
                                </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
           
        </table>
    
    </div>
   </ContentTemplate></asp:UpdatePanel>
</asp:Content>


 
