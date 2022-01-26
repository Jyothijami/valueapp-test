<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="Stock_Report.aspx.cs" Inherits="Modules_Warehouse_Stock_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
     <%-- <asp:UpdatePanel runat="server">
        <ContentTemplate>--%>
    <h3>Please Click on Search Button to View The Report</h3>
            <div style="width: 100%">
                <table style="width: 100%" class="pagehead">
                    <tr>
                        <td style="text-align: left;">Stock Report :
                        </td>

                        <td style="text-align: right">
                            <asp:DropDownList ID="ddlNoOfRecords" runat="server" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged">
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>75</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divSearch" style="width: 100%">
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: right">Model No :</td>
                        <td>
                            <asp:TextBox ID="txtModelNo" runat="server"></asp:TextBox>
                            <asp:Button ID="btnGo0" runat="server" OnClick="btnGo_Click" Text="Model No Search" Width="150px" />
                        </td>
                        <td>
                            &nbsp;</td>
                        <asp:SqlDataSource
                            ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                            SelectCommand="USP_MODEL_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:Parameter Type="Int64" DefaultValue="0" Name="BranbId"></asp:Parameter>
                                <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtModelNo"></asp:ControlParameter>
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </tr>
                    <tr>
                        <td style="text-align: right;">Company Name :
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlCompany" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="compsds1" DataTextField="COMP_NAME" DataValueField="CP_ID">
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="compsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT a.CP_ID, a.CP_FULL_NAME + ',' + b.locname AS COMP_NAME FROM YANTRA_COMP_PROFILE AS a INNER JOIN location_tbl AS b ON a.locid = b.locid"></asp:SqlDataSource>
                        </td>
                        <td></td>
                        <td style="text-align: right">Location : 
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlLocation" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="locsds1" DataTextField="wh_name" DataValueField="wh_name">
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="locsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT DISTINCT [wh_name] from v_inward"></asp:SqlDataSource>

                        </td>
                    </tr>                    

                    <tr>
                        <td style="text-align: right;">Brand :
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlBrand" runat="server" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                        </td>
                        <td></td>
                        <td style="text-align: right">Model No : 
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlModelNo" runat="server" OnSelectedIndexChanged="ddlModelNo_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">Category :
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlCategory" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                        </td>
                        <td></td>
                        <td style="text-align: right">Sub Category : 
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlSubCat" runat="server"></asp:DropDownList>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">Color :
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlColor" runat="server"></asp:DropDownList>
                        </td>
                        <td></td>
                        <td style="text-align: right">From Date : 
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtFromDate" type="date" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right;">To Date :
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtToDate" type="date" runat="server"></asp:TextBox>
                        </td>
                        <td colspan="3"></td>
                    </tr>

                    <tr>
                        <td colspan="5" style="text-align: center">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                            <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click" />
                        </td>
                    </tr>


                </table>
            </div>
            <div id="divStockReport">
                <table style="width: 100%">
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvStockReport" runat="server" Width="100%" OnRowDataBound="gvStockReport_RowDataBound" GridLines="None" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvStockReport_PageIndexChanging"></asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
 <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>


 
