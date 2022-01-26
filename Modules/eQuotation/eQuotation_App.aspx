<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/SalesMP1.master" AutoEventWireup="true" CodeFile="eQuotation_App.aspx.cs" Inherits="Modules_eQuotation_eQuotation_App" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <div style="text-align:left;">
    <table style="width:100%;">
        <tr>
            <td><h1>Executive Quotation App</h1></td>
        </tr>
        <tr>
            <td>Click on the download button and install it in Windows PC</td>
        </tr>
        <tr>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td><h3>System Requirements</h3></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width:400px;">
                            <table class="table" style="width:100%;">
                                <tr>
                                    <td>Operating System</td>
                                    <td>Windows 7, 8, Server 2008, Server 2012</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>Harddisk</td>
                                    <td>Minimum 1 GB</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>Ram</td>
                                    <td>Minimum 2 GB</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>.Net Framework</td>
                                    <td>Framework 4/4.5 Full</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <asp:Button ID="btnDownload1" runat="server" Text="Download" Font-Size="Larger" OnClick="btnDownload1_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
        </div>
</asp:Content>


 
