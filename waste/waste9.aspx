<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="waste9.aspx.cs" Inherits="waste_waste9" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table cellpadding="5" cellspacing="5" style="width:100%;">
        <tr>
            <td>
                <table>
                    <tr>
                        <td><div runat="server" >
                            <table><tr>
                        <td>Select Location : </td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:DropDownList ID="ddlLocation1" runat="server"  AutoPostBack="True"  
                                OnSelectedIndexChanged="ddlLocation1_SelectedIndexChanged">
                                
                            </asp:DropDownList>
                        </td>
                                </tr></table></div></td>
                    </tr>
                    <tr>
                        <td><div id="d1" runat="server" visible="false">
                            <table>
                                <tr>
                        <td>Select Location2:</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:DropDownList ID="ddlLocation2" runat="server" AutoPostBack="True" 
                                OnSelectedIndexChanged="ddlLocation2_SelectedIndexChanged" >
                                
                            </asp:DropDownList>
                        </td>
                                    </tr></table></div>
                        </td>
                    </tr>
                    <tr>
                        <td><div id="d2" runat="server" visible="false">
                            <table>
                                <tr>
                        <td>Select Location3:</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:DropDownList ID="ddlLocation3" runat="server" AutoPostBack="True" 
                                 OnSelectedIndexChanged="ddlLocation3_SelectedIndexChanged" >
                            </asp:DropDownList>
                        </td>
                                    </tr>
                                </table>
                            </div>
                            </td>
                    </tr>
                    <tr>
                        <td><div id="d3" runat="server" visible="false">
                            <table>
                                <tr>
                        <td>Select Location4:</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:DropDownList ID="ddlLocation4" runat="server" AutoPostBack="True" 
                                OnSelectedIndexChanged="ddlLocation4_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                                    </tr></table></div></td>
                    </tr>  
                   <tr>
                        <td><div id="d4" runat="server" visible="false">
                            <table>
                                <tr>
                        <td>Select Location5:</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:DropDownList ID="ddlLocation5" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                                    </tr></table></div></td>
                    </tr>  
                  </table>
                </td>
            </tr>
    </table>
      
</asp:Content>



