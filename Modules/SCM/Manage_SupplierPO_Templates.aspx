<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/PurchaseMP1.master" AutoEventWireup="true" CodeFile="Manage_SupplierPO_Templates.aspx.cs" Inherits="Modules_SCM_Manage_SupplierPO_Templates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <div class="subh2">
        Supplier PO Templates</div>
    <div>
        <table class="tablelayout1">
            <tr>
                <td>
                    <table>
            <tr>
                <td>Template Name:</td>
                <td>
                    <asp:TextBox ID="tbxTname1" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Template File:</td>
                <td>
                    <asp:FileUpload ID="FileUp1" runat="server" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Model Cell:</td>
                <td>
                    <asp:TextBox ID="tbxTMcell1" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Description Cell:</td>
                <td>
                    <asp:TextBox ID="tbxTDcell1" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Quantity:</td>
                <td>
                    <asp:TextBox ID="tbxTQcell1" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Color:</td>
                <td>
                    <asp:TextBox ID="tbxTCcell1" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:CheckBox ID="cbxIsGeneral1" runat="server" AutoPostBack="True" OnCheckedChanged="cbxIsGeneral1_CheckedChanged" Text="General" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnAddTemplate1" runat="server" Text="Add Template" OnClick="btnAddTemplate1_Click" />
                &nbsp;<asp:Button ID="btnReset1" runat="server" OnClick="btnReset1_Click" Text="Reset" />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="sptid" DataSourceID="suppotsds1">
                        <Columns>
                            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                            <asp:BoundField DataField="sptid" HeaderText="sptid" ReadOnly="True" SortExpression="sptid" />
                            <asp:BoundField DataField="sptname" HeaderText="sptname" SortExpression="sptname" />
                            <asp:BoundField DataField="spotpath" HeaderText="spotpath" SortExpression="spotpath" />
                            <asp:BoundField DataField="itemmodel_cell" HeaderText="itemmodel_cell" SortExpression="itemmodel_cell" />
                            <asp:BoundField DataField="itemdesc_cell" HeaderText="itemdesc_cell" SortExpression="itemdesc_cell" />
                            <asp:BoundField DataField="itemcolor_cell" HeaderText="itemcolor_cell" SortExpression="itemcolor_cell" />
                            <asp:BoundField DataField="itemqty_cell" HeaderText="itemqty_cell" SortExpression="itemqty_cell" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="suppotsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [sptid], [sptname], [spotpath], [itemmodel_cell], [itemdesc_cell], [itemcolor_cell], [itemqty_cell] FROM [sup_po_templates_tbl] ORDER BY [sptname]" ConflictDetection="CompareAllValues" DeleteCommand="DELETE FROM [sup_po_templates_tbl] WHERE [sptid] = @original_sptid" InsertCommand="INSERT INTO [sup_po_templates_tbl] ([sptid], [sptname], [spotpath], [itemmodel_cell], [itemdesc_cell], [itemcolor_cell], [itemqty_cell]) VALUES (@sptid, @sptname, @spotpath, @itemmodel_cell, @itemdesc_cell, @itemcolor_cell, @itemqty_cell)" OldValuesParameterFormatString="original_{0}" UpdateCommand="UPDATE [sup_po_templates_tbl] SET [sptname] = @sptname, [spotpath] = @spotpath, [itemmodel_cell] = @itemmodel_cell, [itemdesc_cell] = @itemdesc_cell, [itemcolor_cell] = @itemcolor_cell, [itemqty_cell] = @itemqty_cell WHERE [sptid] = @original_sptid">
                        <DeleteParameters>
                            <asp:Parameter Name="original_sptid" Type="String" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="sptid" Type="String" />
                            <asp:Parameter Name="sptname" Type="String" />
                            <asp:Parameter Name="spotpath" Type="String" />
                            <asp:Parameter Name="itemmodel_cell" Type="String" />
                            <asp:Parameter Name="itemdesc_cell" Type="String" />
                            <asp:Parameter Name="itemcolor_cell" Type="String" />
                            <asp:Parameter Name="itemqty_cell" Type="String" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="sptname" Type="String" />
                            <asp:Parameter Name="spotpath" Type="String" />
                            <asp:Parameter Name="itemmodel_cell" Type="String" />
                            <asp:Parameter Name="itemdesc_cell" Type="String" />
                            <asp:Parameter Name="itemcolor_cell" Type="String" />
                            <asp:Parameter Name="itemqty_cell" Type="String" />
                            <asp:Parameter Name="original_sptid" Type="String" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
        
    </div>
</asp:Content>


 
