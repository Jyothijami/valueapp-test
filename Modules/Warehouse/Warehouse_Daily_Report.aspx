<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="Warehouse_Daily_Report.aspx.cs" Inherits="Modules_Warehouse_Warehouse_Daily_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
<%--    <asp:UpdatePanel runat="server">
        <ContentTemplate>--%>
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center; font-weight: bold;">&nbsp;
                            <asp:LinkButton ID="lnkInward" runat="server" OnClick="lnkInward_Click" Font-Underline="True">Inward Report</asp:LinkButton>
                        &nbsp;||&nbsp;
                            <asp:LinkButton ID="lnkOutward" runat="server" OnClick="lnkOutward_Click" Font-Underline="True">Outward Report</asp:LinkButton>
                        &nbsp;||&nbsp;
                            <asp:LinkButton ID="lnkMovingDc" runat="server" OnClick="lnkMovingDc_Click" Font-Underline="True">Moving DC Report</asp:LinkButton>
                         &nbsp;||&nbsp;
                            <asp:LinkButton ID="lnkstock" runat="server" OnClick="lnkstock_Click" Font-Underline="True">Stock Report Analysis</asp:LinkButton>
                        &nbsp;||&nbsp;
                            <asp:LinkButton ID="lnkDayBasisStk" runat="server" OnClick="lnkDayBasisStk_Click" Font-Underline="True">Day Basis Stock Report Analysis</asp:LinkButton>
                         &nbsp;||&nbsp;
                            <asp:LinkButton ID="lnkYeardata" runat="server" OnClick="lnkYeardata_Click" Font-Underline="True">Yearly Stock report</asp:LinkButton>

                    </td>
                </tr>
            </table>
            <br />
            <asp:Panel runat="server" ID="pnlInward" Visible="false">
                <div id="body" style="width: 100%" runat="server">
                    <table style="width: 100%">
                        <tr>
                            <td class="profilehead" style="text-align: left">Warehouse Inward Report :</td>

                        </tr>
                        <tr>
                            <td style="text-align: right">Go To Page :
                                    <asp:TextBox ID="txtPageNo1" Width="100px" runat="server"></asp:TextBox>
                                <asp:Button ID="btnPageNoSearch1" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />

                            </td>
                            <td style="text-align: right">
                                <asp:DropDownList ID="ddlNoOfRecords1" runat="server" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>25</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>75</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table style="width: 100%; text-align: center;">
                        <tr>
                            <td style="text-align: right">Location :
                    <asp:DropDownList ID="ddlLocations1" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="locsds1" DataTextField="locname" DataValueField="locid" OnSelectedIndexChanged="ddlLocations1_SelectedIndexChanged">
                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                    </asp:DropDownList>
                                <asp:SqlDataSource ID="locsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [locid], [locname] FROM [location_tbl]"></asp:SqlDataSource>

                            </td>
                            <td style="width: 5%"></td>
                            <td style="width: 10%">From Branch :
                            </td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="ddlBranch1" runat="server" AutoPostBack="True" DataSourceID="branchsds1" DataTextField="whname" DataValueField="wh_id" AppendDataBoundItems="True" >
                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="branchsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [wh_id], [whname] FROM [warehouse_tbl] WHERE ([locid] = @locid)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ddlLocations1" Name="locid" PropertyName="SelectedValue" Type="String" DefaultValue="1" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>

                        <tr>
                            <td>&nbsp;</td>
                        </tr>

                        <tr>
                            <td style="text-align: right">From Date :
                    <asp:TextBox ID="txtFromDate1" type="datepic" runat="server"></asp:TextBox>
                            </td>
                            <td style="width: 5%"></td>
                            <td style="width: 10%">To Date : </td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtToDate1" type="datepic" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            
                            <td style="text-align:center" colspan="4">
                                <asp:Button ID="btnSearch1" runat="server" Text="Search" OnClick="btnSearch1_Click" BackColor="#9966FF" EnableTheming="True" ForeColor="White" Width="120px" />
                                &nbsp;
                    &nbsp;
                    &nbsp;
                    <asp:Button ID="btnExprot1" runat="server" Text="Export To Excel" OnClick="btnExprot_Click" BackColor="#9966FF" EnableTheming="True" ForeColor="White" Width="120px" />

                            </td>
                        </tr>

                    </table>
                </div>
                <br />
                <div id="grid" style="width: 100%">
                    <asp:GridView ID="gvInwardReport" Width="100%" runat="server" AutoGenerateColumns="false" SelectedRowStyle-BackColor="#c0c0c0" EmptyDataText="No Records To Display" AllowPaging="True" OnPageIndexChanging="gvInwardReport_PageIndexChanging" PageSize="8">
                        <HeaderStyle HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="Silver"></SelectedRowStyle>
                        <Columns>
                            <asp:BoundField DataField="SUP_NAME" HeaderText="Name of the Party" SortExpression="SUP_NAME" />
                            <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="Model No" SortExpression="ITEM_MODEL_NO" />
                            <asp:BoundField DataField="ITEM_SPEC" HeaderText="Name of the Material" SortExpression="ITEM_SPEC" />
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlOutward" Visible="false">
                <div id="Div1" style="width: 100%" runat="server">
                    <table style="width: 100%">
                        <tr>
                            <td class="profilehead" style="text-align: left">Warehouse Outward Report :</td>

                        </tr>
                        <tr>
                            <td style="text-align: right">Go To Page :
                                    <asp:TextBox ID="txtGo2" Width="100px" runat="server"></asp:TextBox>
                                <asp:Button ID="btnPageNoSearch2" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch2_Click" />

                            </td>
                            <td style="text-align: right">
                                <asp:DropDownList ID="ddlNoOfRecords2" runat="server" OnSelectedIndexChanged="ddlNoOfRecords2_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>25</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>75</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table style="width: 100%; text-align: center;">
                        <tr>
                            <td style="text-align: right">Location :
                    <asp:DropDownList ID="ddlLocation2" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="locsds2" DataTextField="locname" DataValueField="locid" OnSelectedIndexChanged="ddlLocation2_SelectedIndexChanged">
                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                    </asp:DropDownList>
                                <asp:SqlDataSource ID="locsds2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [locid], [locname] FROM [location_tbl]"></asp:SqlDataSource>

                            </td>
                            <td style="width: 5%"></td>
                            <td style="width: 10%">Branch :
                            </td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="ddlBranch2" runat="server" AutoPostBack="True" DataSourceID="branchsds2" DataTextField="whname" DataValueField="wh_id" AppendDataBoundItems="True" >
                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="branchsds2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [wh_id], [whname] FROM [warehouse_tbl] WHERE ([locid] = @locid)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ddlLocation2" Name="locid" PropertyName="SelectedValue" Type="String" DefaultValue="1" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>

                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">From Date :
                    <asp:TextBox ID="txtFromDate2" type="datepic" runat="server"></asp:TextBox>
                            </td>
                            <td style="width: 5%"></td>
                            <td style="width: 10%">To Date : </td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtToDate2" type="datepic" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            
                            <td style="text-align:center" colspan="4">
                                <asp:Button ID="btnSearch2" runat="server" Text="Search" OnClick="btnSearch2_Click" BackColor="#9966FF" EnableTheming="True" ForeColor="White" Width="120px" />
                                &nbsp;
                    &nbsp;
                    &nbsp;
                    <asp:Button ID="btnExprot2" runat="server" Text="Export To Excel" OnClick="btnExprot2_Click" BackColor="#9966FF" EnableTheming="True" ForeColor="White" Width="120px" />

                            </td>
                        </tr>

                    </table>
                </div>
                <br />
                <div id="grid2" style="width: 100%">
                    <asp:GridView ID="gvOutwardReport" Width="100%" runat="server" AutoGenerateColumns="false" SelectedRowStyle-BackColor="#c0c0c0" EmptyDataText="No Records To Display" AllowPaging="True" OnPageIndexChanging="gvOutwardReport_PageIndexChanging" PageSize="8">
                        <HeaderStyle HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="Silver"></SelectedRowStyle>
                        <Columns>
                            <asp:BoundField DataField="DC_NO" HeaderText="DC No." SortExpression="DC_NO" />
                            <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="Model No" SortExpression="ITEM_MODEL_NO" />
                            <asp:BoundField DataField="ITEM_SPEC" HeaderText="Name of the Material" SortExpression="ITEM_SPEC" />
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                            <asp:BoundField DataField="CUST_NAME" HeaderText="Party Name" SortExpression="CUST_NAME" />
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>

            <asp:Panel runat="server" ID="pnlMovingDc" Visible="false">
                <div id="Div2" style="width: 100%" runat="server">
                    <table style="width: 100%">
                        <tr>
                            <td class="profilehead" style="text-align: left">Warehouse Moving Dc Report :</td>

                        </tr>
                        <tr>
                            <td style="text-align: right">Go To Page :
                                    <asp:TextBox ID="txtPageNo3" Width="100px" runat="server"></asp:TextBox>
                                <asp:Button ID="btnPageNoSearch3" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch3_Click" />

                            </td>
                            <td style="text-align: right">
                                <asp:DropDownList ID="ddlNoOfRecords3" runat="server" OnSelectedIndexChanged="ddlNoOfRecords3_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>25</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>75</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table style="width: 100%; text-align: center;">
                        <tr>
                            <td style="text-align: right">From Location :
                    <asp:DropDownList ID="ddlLocation3" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="locsds3" DataTextField="locname" DataValueField="locid" OnSelectedIndexChanged="ddlLocation3_SelectedIndexChanged">
                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                    </asp:DropDownList>
                                <asp:SqlDataSource ID="locsds3" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [locid], [locname] FROM [location_tbl]"></asp:SqlDataSource>

                            </td>
                            <td style="width: 5%"></td>
                            <td style="width: 10%">From Branch :
                            </td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="ddlBranch3" runat="server" AutoPostBack="True" DataSourceID="branchsds3" DataTextField="whname" DataValueField="wh_id" AppendDataBoundItems="True" >
                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="branchsds3" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [wh_id], [whname] FROM [warehouse_tbl] WHERE ([locid] = @locid)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ddlLocation3" Name="locid" PropertyName="SelectedValue" Type="String" DefaultValue="1" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                        
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <%--<td style="text-align: right">To Location :
                    <asp:DropDownList ID="ddlLocation4" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="locsds4" DataTextField="locname" DataValueField="locid" OnSelectedIndexChanged="ddlLocation4_SelectedIndexChanged">
                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                    </asp:DropDownList>
                                <asp:SqlDataSource ID="locsds4" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [locid], [locname] FROM [location_tbl]"></asp:SqlDataSource>

                            </td>--%>
                            <td style="text-align: right">&nbsp;</td>
                            <td style="width: 5%"></td>
                            <td style="width: 10%">To Branch :
                            </td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="ddlBranch4" runat="server" AutoPostBack="True" DataSourceID="branchsds4" DataTextField="whname" DataValueField="wh_id" AppendDataBoundItems="True" >
                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="branchsds4" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [wh_id], [whname] FROM [warehouse_tbl] WHERE ([locid] = @locid)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ddlLocation3" Name="locid" PropertyName="SelectedValue" Type="String" DefaultValue="1" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">From Date :
                    <asp:TextBox ID="txtFromDate3" type="datepic" runat="server"></asp:TextBox>
                            </td>
                            <td style="width: 5%"></td>
                            <td style="width: 10%">To Date : </td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtToDate3" type="datepic" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            
                            <td style="text-align:center" colspan="4">
                                <asp:Button ID="btnSearch3" runat="server" Text="Search" OnClick="btnSearch3_Click" BackColor="#9966FF" EnableTheming="True" ForeColor="White" Width="120px" />
                                &nbsp;
                    &nbsp;
                    &nbsp;
                    <asp:Button ID="btnExprot3" runat="server" Text="Export To Excel" OnClick="btnExprot3_Click" BackColor="#9966FF" EnableTheming="True" ForeColor="White" Width="120px" />

                            </td>
                        </tr>

                    </table>
                </div>
                <br />
                <div id="grid3" style="width: 100%">
                    <asp:GridView ID="gvMovingDc" Width="100%" runat="server" AutoGenerateColumns="false" SelectedRowStyle-BackColor="#c0c0c0" EmptyDataText="No Records To Display" AllowPaging="True" OnPageIndexChanging="gvMovingDc_PageIndexChanging"   PageSize="8">
                        <HeaderStyle HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="Silver"></SelectedRowStyle>
                        <Columns>
                            <asp:BoundField DataField="INT_DC_NO" HeaderText="DC No." SortExpression="INT_DC_NO" />
                            <asp:BoundField DataField="IND_NO" HeaderText="Indent No." SortExpression="IND_NO" />

                            <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="Model No" SortExpression="ITEM_MODEL_NO" />
                            <asp:BoundField DataField="ITEM_SPEC" HeaderText="Name of the Material" SortExpression="ITEM_SPEC" />
                            <asp:BoundField DataField ="Colour_Name" HeaderText ="Colour" />
                            <asp:BoundField DataField="ReceivedQty" HeaderText="Quantity" SortExpression="ReceivedQty" />
                            <asp:BoundField DataField="MovingLoc" HeaderText="Moving From" SortExpression="MovingLoc" />
                            <asp:BoundField DataField="ToLoc" HeaderText="Moving To" SortExpression="ToLoc" />
                            <asp:BoundField DataField="CLIENT_NAME" HeaderText="Client Name" SortExpression="CLIENT_NAME" />
                            <asp:BoundField DataField="MOVINGDC_DATE" HeaderText="Moving Dc Date" SortExpression="MOVINGDC_DATE"  DataFormatString="{0:dd/MM/yyyy}" />
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
    <asp:Panel ID="pnlstk" runat ="server" Visible ="false" >
        <div id="Div3" style="width: 100%" runat="server">
            <table style="width: 100%">
                 <tr>
                    <td class="profilehead" style="text-align: left">Warehouse Stock report Analysis :</td>
                 </tr>
                <tr>
                            <td style="text-align: right">Go To Page :
                                    <asp:TextBox ID="txtpage4" Width="100px" runat="server"></asp:TextBox>
                                <asp:Button ID="btnPageNoSearch4" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch4_Click" />

                            </td>
                            <td style="text-align: right">
                                <asp:DropDownList ID="ddlNoOfRecords4" runat="server" OnSelectedIndexChanged="ddlNoOfRecords4_SelectedIndexChanged" AutoPostBack="True">
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
                            <asp:Button ID="btnGo0" runat="server" OnClick="btnGo_Click" Text="Search" Width="100px" />
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
                            <asp:DropDownList ID="ddlLocation" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="whname" DataValueField="whname">
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [wh_id], [whname] FROM [warehouse_tbl] "></asp:SqlDataSource>

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
                            <asp:TextBox ID="txtFromDate" type="datepic" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right;">To Date :
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtToDate" type="datepic" runat="server"></asp:TextBox>
                        </td>
                        <td colspan="3"></td>
                    </tr>

                    <tr>
                        <td colspan="5" style="text-align: center">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                            <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click" Visible="False" />
                            <asp:Button ID="btnExport2" runat="server" Text="Export To Excel" OnClick="btnExport2_Click" Visible="False" />
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
                            <asp:GridView ID="gvStockReport" runat="server" Width="100%" OnRowDataBound="gvStockReport_RowDataBound" GridLines="None" AllowPaging="true" PageSize="25" OnPageIndexChanging="gvStockReport_PageIndexChanging" Visible="False" ></asp:GridView>
                            <asp:GridView ID="gvModelNoSearch" runat="server" Width="100%" OnRowDataBound="gvModelNoSearch_RowDataBound" GridLines="None" AllowPaging="true" PageSize="25" OnPageIndexChanging="gvModelNoSearch_PageIndexChanging" Visible="False" ></asp:GridView>
                        
                        </td>
                    </tr>
                </table>
            </div>
    </asp:Panel>

    <asp:Panel ID="pnlDailyBasisStk" runat ="server" Visible ="false"  >
        <div style="width: 100%" runat="server">
            <table style="width: 100%; text-align: center;">
                <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>

            <td>DC No :
            </td>
            <td>
                <asp:TextBox ID="txtDcNo" runat="server"></asp:TextBox>

            </td>

            <td>DC Date :
            </td>
            <td>
                <asp:TextBox ID="txtDcDate" CssClass="datetimetxt" runat="server"></asp:TextBox>
            </td>


        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>


        <tr>

            <td>Item Model No :
            </td>
            <td>
                <asp:TextBox ID="txtItmModelNo" runat="server"></asp:TextBox>

            </td>
            <td>Item Code :
            </td>
            <td>
                <asp:TextBox ID="txtItemCode" runat="server"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>

        <tr>
            <td>Customer Name :
            </td>
            <td>
                <asp:TextBox ID="txtCustname" runat="server"></asp:TextBox>
            </td>
            <td></td>
            <td>
                <asp:Button ID="btnDCSearch" runat="server" Text="Search" BackColor="#FF9900" Font-Bold="True" OnClick="btnDCSearch_Click" />
            </td>

        </tr>
               
            </table>
           <div>
                <asp:GridView id="gvDCDet" runat ="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns ="false"  BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" Width ="100%" CellPadding="4" DataKeyNames="Item_Code" OnPageIndexChanging ="gvDCDet_PageIndexChanging">
                    <Columns >
                        <asp:BoundField DataField ="DC_NO" HeaderText ="Dc No" />
                        <asp:BoundField DataField ="DCDate" HeaderText ="Dc Dt" />
                        <asp:BoundField DataField ="Item_Code" HeaderText ="Item Code" />
                        <asp:BoundField DataField ="Item_Model_No" HeaderText ="Model No" />
                        <asp:BoundField DataField ="Item_Name" HeaderText ="Serice" />
                        <asp:BoundField DataField ="DC_Det_Qty" HeaderText ="DC Qty" />
                        <asp:BoundField DataField ="Qty" HeaderText ="Out Qty" />
                        <asp:BoundField DataField ="WhName" HeaderText ="loc Name" />

                    </Columns>
                </asp:GridView>
           </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlyrstk" runat ="server" Visible ="false"  >
        <div id="Div4" style="width: 100%" runat="server">
            <table style="width: 100%">
                 <tr>
                    <td colspan ="4" class="profilehead" style="text-align: left">Yearly Stock Report :</td>
                 </tr>
                <tr>
                            <td colspan ="3" style="text-align: right">Go To Page :
                                    <asp:TextBox ID="txtgo" Width="100px" runat="server"></asp:TextBox>
                                <asp:Button ID="btnGosearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnGosearch_Click" />

                            </td>
                            <td  style="text-align: right">
                                <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>25</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>75</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                <tr>
                    <td style="text-align: right">
                                    <asp:Label ID="Label38" runat="server" Text="Search By ModelNo :" Width="135px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtSearchModel" runat="server">
                                    </asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server"
                                        ControlToValidate="txtSearchModel" ErrorMessage="Please Enter ModelNo For Search"
                                        ValidationGroup="Search">*</asp:RequiredFieldValidator><asp:Button ID="btnSearchModelNo"
                                            runat="server" BorderStyle="None" CausesValidation="False" CssClass="gobutton"
                                            EnableTheming="False" OnClick="btnSearchModelNo_Click1" Text="Go" ValidationGroup="Search" /></td>
                        
                </tr>
                <tr>
                    <td style="text-align: right">Model No :</td>
                        <td>
                            <%--<asp:TextBox ID="txtMdlNo" runat="server"></asp:TextBox>--%>
                            <asp:DropDownList ID="ddlMdlNo" runat ="server" ></asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource21" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                        SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:ControlParameter PropertyName="SelectedValue" Type="Int64" DefaultValue="0" Name="BranbId" ControlID="ddlBrand"></asp:ControlParameter>
                                            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtSearchModel"></asp:ControlParameter>
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                        </td>
                    <td style="text-align: right">Company Name :</td>
                    <td><asp:DropDownList ID="ddlComp" runat ="server" ></asp:DropDownList>

                    </td>
                </tr>
                <tr>
                    <td colspan ="4" style="text-align : center ">
                            <asp:Button ID="btnGoMdl" runat="server" OnClick="btnGoMdl_Click" Text="Search" Width="100px" />

                    </td>
                </tr>
                </table> 
            </div> 
        <div id="divStockReport12">
            <table>
        <tr>
            <td>
                Total No Of Inward Quantity :
            </td>
            <td>
                <asp:Label ID="lblInwardQty" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;
            </td>
            <td>
                Total No Of Outward Quantity :
            </td>
            <td>
                <asp:Label ID="lblOutwardQty" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;
            </td>
        </tr>
    </table>
        <br />
            <table style="width: 100%">
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvStockReportyear"  runat="server" Width="100%" GridLines="None" AutoGenerateColumns ="false"  OnRowDataBound ="gvStockReportyear_RowDataBound" OnPageIndexChanging="gvStockReportyear_PageIndexChanging" AllowPaging="true" PageSize="25" Visible="False" >
                                <Columns >
                                    <asp:BoundField DataField ="CP_SHORT_NAME" HeaderText="Comp Name" />
                                    <asp:BoundField DataField ="ITEM_MODEL_NO" HeaderText="Model No" />
                                    <asp:BoundField DataField ="ITEM_CODE" HeaderText="ITEM CODE" />
                                    <asp:BoundField DataField ="year" HeaderText="year" />
                                    <asp:BoundField DataField ="CP_ID" HeaderText="CP_ID" />
                                    <asp:BoundField DataField ="inqty1" HeaderText="In Qty" />
                                    <asp:BoundField DataField ="outqty1" HeaderText="Out Qty" />
                                    <asp:BoundField HeaderText ="Balance" />
                                    <asp:BoundField DataField ="MRN" HeaderText="MRN" />
                                    <asp:BoundField DataField ="Returns" HeaderText="Returns" />
                                    <asp:BoundField DataField ="OS" HeaderText="OS" />
                                    <%--<asp:BoundField DataField ="MRNT" HeaderText="Type Of Inward" />--%>

                                </Columns>
                            </asp:GridView>
                            
                        </td>
                    </tr>
                </table>
        </div>
    </asp:Panel>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>


 
