<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Modules_Home" Title="|| YANTRA : Home ||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="pagehead">
                
            </td>
        </tr>
        <tr>
            <td style="height: 330px; font-size: 8pt; color: black; font-family: Verdana;">              
                <strong style="line-height: 20pt">
                Operation failed due to some illegal access.<br />
                Please<asp:LinkButton ID="lbtnLogout" runat="server"
                        CausesValidation="False" CssClass="linkinmaster" EnableTheming="False" Font-Bold="True"
                        PostBackUrl="~/Default.aspx">Login</asp:LinkButton>again</strong></td>
        </tr>
    </table>
</asp:Content>


 
