<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="SparesStockReport.aspx.cs" Inherits="Modules_Warehouse_SparesStockReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table style="width:100%">
        <tr>
            <td colspan="4" class="profilehead" style="text-align: left">Spares Items Report</td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: right"> No.Of Records :<asp:DropDownList ID="ddlNoOfRecords" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged">
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>75</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>                   
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="text-align: right">Model No :</td>
            <td>
                <asp:TextBox ID="txtModelNo" runat="server"></asp:TextBox>
            </td>
            <td style="text-align: right">Spare Model No :</td>
            <td>
                <asp:TextBox ID="txtSpareModelNo" runat="server"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvSparesStock" EmptyDataText="No Records To Display" OnRowDataBound ="gvSparesStock_RowDataBound" OnPageIndexChanging ="gvSparesStock_PageIndexChanging" AllowPaging="True" runat="server" Width="100%">
                   <HeaderStyle HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="Silver"></SelectedRowStyle>
                    <Columns >
                        <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnDelete" CausesValidation="false" ForeColor="Blue" runat="server" __designer:wfdid="w5" OnClick="lbtnDelete_Click">Delete</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>


 
