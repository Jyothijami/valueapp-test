<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="WastePage.aspx.cs" Inherits="waste_WastePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <table>
                <tr>
                    <td>Main Location :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMain" runat="server" OnSelectedIndexChanged="ddlMain_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Sub Location :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSubLoc1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubLoc1_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Sub Location :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSubLoc2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubLoc2_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Sub Location :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSubLoc3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubLoc3_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Sub Location :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSubLoc4" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubLoc4_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>

              <%--  <tr>
                    <td>
                        <asp:TextBox ID="txtTimeFromDate" runat="server" Width="60px" ></asp:TextBox>
					<cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99" TargetControlID="txtTimeFromDate" Filtered=":" AcceptAMPM="true" />
                    </td>
                </tr>--%>
            </table>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



