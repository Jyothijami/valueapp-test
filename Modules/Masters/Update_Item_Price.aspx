<%@ Page Title="||Value App : Masters : Update Item Price ||" Language="C#" MasterPageFile="~/MPs/AdminMP1.master" AutoEventWireup="true" CodeFile="Update_Item_Price.aspx.cs" Inherits="Modules_Masters_Update_Item_Price" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div>
        <table class="pagehead">
            <tr>
                <td style="text-align:left">Update Items Price 
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div>
        <table style="width: 100%">
            <tr>
                <td>Brand :
                </td>
                <td>
                    <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td>Category :
                </td>
                <td>
                    <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td>Sub-Category :
                </td>
                <td>
                    <asp:DropDownList ID="ddlSubCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td>Model :
                </td>
                <td>
                    <asp:DropDownList ID="ddlModel" runat="server"></asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <table style="width: 100%">
            <tr>
                <td colspan="2" style="text-align:right">
                    Change In Price : 
                
                    <asp:DropDownList ID="ddlChangeInPrice" runat="server">
                        <asp:ListItem Value="0">Increase</asp:ListItem>
                        <asp:ListItem Value="1">Decrease</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;
                </td>
                <td  colspan="2">Percentage Change (%) :
                
                    <asp:TextBox ID="txtPercentage" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align:center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>


 
