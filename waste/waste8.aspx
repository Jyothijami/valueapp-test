<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="waste8.aspx.cs" Inherits="waste_waste8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
<table cellpadding="5" cellspacing="5" style="width:100%;">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>Select Location : </td>
                        <td>
                            <asp:DropDownList ID="ddlLocations" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="locsds1" DataTextField="locname" DataValueField="locid" OnSelectedIndexChanged="ddlLocations_SelectedIndexChanged">
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="locsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [locid], [locname] FROM [location_tbl]"></asp:SqlDataSource>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Select Branch:</td>
                        <td>
                            <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="True" DataSourceID="branchsds1" DataTextField="whname" DataValueField="wh_id" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="branchsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [wh_id], [whname] FROM [warehouse_tbl] WHERE ([locid] = @locid)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddlLocations" Name="locid" PropertyName="SelectedValue" Type="String" DefaultValue="1" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Select Sub Branch Name:</td>
                        <td>
                            <asp:DropDownList ID="ddlSubBranch" runat="server" AutoPostBack="True" DataSourceID="SubBranchsds1" DataTextField="whLocName" DataValueField="whLocId" OnSelectedIndexChanged="ddlSubBranch_SelectedIndexChanged">
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SubBranchsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [whLocId], [whLocName] FROM [vltn_13_11].[dbo].[WH_Locations] where wh_id=@wh_id">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddlBranch" Name="wh_id" PropertyName="SelectedValue" Type="String" DefaultValue="1" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Select Section Name:</td>
                        <td>
                            <asp:DropDownList ID="ddlSection" runat="server" AutoPostBack="True" DataSourceID="Sectionsds1" DataTextField="Sec_Name" DataValueField="Sec_Name" >
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="Sectionsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [Sec_Name] FROM [vltn_13_11].[dbo].[v_WH_Location_Link] where [whLocId]=@whLocId">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddlSubBranch" Name="whLocId" PropertyName="SelectedValue" Type="String" DefaultValue="1" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                        <td>&nbsp;</td>
                    </tr>  
                    <tr>
                        <td>Warehouse Location Id : </td>
                        <td>
                            <asp:TextBox ID="txtWhLocId" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                  </table>
                </td>
            </tr>
    </table>

</asp:Content>


