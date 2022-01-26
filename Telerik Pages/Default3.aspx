<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="Default3" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="styles.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        </telerik:RadStyleSheetManager>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js">
                </asp:ScriptReference>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js">
                </asp:ScriptReference>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js">
                </asp:ScriptReference>
            </Scripts>
        </telerik:RadScriptManager>
        <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="true" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function RowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }

            function onPopUpShowing(sender, args) {
                args.get_popUp().className += " popUpEditForm";
            }
        </script>
    </telerik:RadCodeBlock>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings >
                <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ConfiguratorPanel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
         <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
        <telerik:RadFormDecorator RenderMode="Lightweight" ID="RadFormDecorator1" runat="server" DecorationZoneID="demo" DecoratedControls="All" EnableRoundedCorners="false" />
    <div id="demo" class="demo-container no-bg">

        <telerik:RadGrid ID="RadGrid1" RenderMode="Lightweight" runat="server" CellSpacing="-1" OnPreRender="RadGrid1_PreRender" OnNeedDataSource="RadGrid1_NeedDataSource" OnInsertCommand="RadGrid1_InsertCommand"  GridLines="Both" Skin="Simple">
<GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
            <MasterTableView AutoGenerateColumns="False" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="QUOT_DET_ID" >
                <Columns>
                    <telerik:GridEditCommandColumn UniqueName="EditCommandColumn">
                    </telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn DataField="QUOT_DET_ID" DataType="System.Int64" FilterControlAltText="Filter QUOT_DET_ID column" HeaderText="QUOT_DET_ID" ReadOnly="True" SortExpression="QUOT_DET_ID" UniqueName="QUOT_DET_ID">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="QUOT_ID" DataType="System.Int64" FilterControlAltText="Filter QUOT_ID column" HeaderText="QUOT_ID" SortExpression="QUOT_ID" UniqueName="QUOT_ID">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ITEM_CODE" DataType="System.Int64" FilterControlAltText="Filter ITEM_CODE column" HeaderText="ITEM_CODE" SortExpression="ITEM_CODE" UniqueName="ITEM_CODE">
                    </telerik:GridBoundColumn>

                    <%--<telerik:GridTemplateColumn >
                        <ItemTemplate >
                            <asp:DropDownList ID="ddlModelNo" DataSourceID ="SqlDatasource2"  DataTextField ="ITEM_MODEL_NO" DataValueField ="Itemcode1" runat ="server" ></asp:DropDownList>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>--%>





                    <telerik:GridBoundColumn DataField="QUOT_DET_QTY" FilterControlAltText="Filter QUOT_DET_QTY column" HeaderText="QUOT_DET_QTY" SortExpression="QUOT_DET_QTY" UniqueName="QUOT_DET_QTY">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="QUOT_RATE" DataType="System.Decimal" FilterControlAltText="Filter QUOT_RATE column" HeaderText="QUOT_RATE" SortExpression="QUOT_RATE" UniqueName="QUOT_RATE">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="QUOT_DISC" DataType="System.Decimal" FilterControlAltText="Filter QUOT_DISC column" HeaderText="QUOT_DISC" SortExpression="QUOT_DISC" UniqueName="QUOT_DISC">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="QUOT_SPPRICE" FilterControlAltText="Filter QUOT_SPPRICE column" HeaderText="QUOT_SPPRICE" SortExpression="QUOT_SPPRICE" UniqueName="QUOT_SPPRICE">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="QUOT_ROOM" FilterControlAltText="Filter QUOT_ROOM column" HeaderText="QUOT_ROOM" SortExpression="QUOT_ROOM" UniqueName="QUOT_ROOM">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="QUOT_CURRENCY" DataType="System.Int64" FilterControlAltText="Filter QUOT_CURRENCY column" HeaderText="QUOT_CURRENCY" SortExpression="QUOT_CURRENCY" UniqueName="QUOT_CURRENCY">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="COLOR_ID" DataType="System.Int64" FilterControlAltText="Filter COLOR_ID column" HeaderText="COLOR_ID" SortExpression="COLOR_ID" UniqueName="COLOR_ID">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="OPTIONALID" DataType="System.Int64" FilterControlAltText="Filter OPTIONALID column" HeaderText="OPTIONALID" SortExpression="OPTIONALID" UniqueName="OPTIONALID">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="REMARKS" FilterControlAltText="Filter REMARKS column" HeaderText="REMARKS" SortExpression="REMARKS" UniqueName="REMARKS">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ITEM_TYPE" FilterControlAltText="Filter ITEM_TYPE column" HeaderText="ITEM_TYPE" SortExpression="ITEM_TYPE" UniqueName="ITEM_TYPE">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="QUOT_FLOOR" FilterControlAltText="Filter QUOT_FLOOR column" HeaderText="QUOT_FLOOR" SortExpression="QUOT_FLOOR" UniqueName="QUOT_FLOOR">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Quot_OrderNo" DataType="System.Int64" FilterControlAltText="Filter Quot_OrderNo column" HeaderText="Quot_OrderNo" SortExpression="Quot_OrderNo" UniqueName="Quot_OrderNo">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="QUOT_DET_GST" DataType="System.Decimal" FilterControlAltText="Filter QUOT_DET_GST column" HeaderText="QUOT_DET_GST" SortExpression="QUOT_DET_GST" UniqueName="QUOT_DET_GST">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="QUOT_DET_GSTRATE" DataType="System.Decimal" FilterControlAltText="Filter QUOT_DET_GSTRATE column" HeaderText="QUOT_DET_GSTRATE" SortExpression="QUOT_DET_GSTRATE" UniqueName="QUOT_DET_GSTRATE">
                    </telerik:GridBoundColumn>
                </Columns>
                <EditFormSettings UserControlName="EditUser.ascx" EditFormType="WebUserControl">
                    <EditColumn UniqueName="EditCommandColumn1">
                    </EditColumn>
                </EditFormSettings>
            </MasterTableView>
            <ClientSettings>
                <ClientEvents OnRowDblClick="RowDblClick" OnPopUpShowing="onPopUpShowing" />
            </ClientSettings>
        </telerik:RadGrid>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" DeleteCommand="DELETE FROM [YANTRA_QUOT_DET] WHERE [QUOT_DET_ID] = @QUOT_DET_ID" InsertCommand="INSERT INTO [YANTRA_QUOT_DET] ([QUOT_DET_ID], [QUOT_ID], [ITEM_CODE], [QUOT_DET_QTY], [QUOT_RATE], [QUOT_DISC], [QUOT_SPPRICE], [QUOT_ROOM], [QUOT_CURRENCY], [COLOR_ID], [OPTIONALID], [REMARKS], [ITEM_TYPE], [QUOT_FLOOR], [Quot_OrderNo], [QUOT_DET_GST], [QUOT_DET_GSTRATE]) VALUES (@QUOT_DET_ID, @QUOT_ID, @ITEM_CODE, @QUOT_DET_QTY, @QUOT_RATE, @QUOT_DISC, @QUOT_SPPRICE, @QUOT_ROOM, @QUOT_CURRENCY, @COLOR_ID, @OPTIONALID, @REMARKS, @ITEM_TYPE, @QUOT_FLOOR, @Quot_OrderNo, @QUOT_DET_GST, @QUOT_DET_GSTRATE)" SelectCommand="SELECT * FROM [YANTRA_QUOT_DET] where quot_id =6474" UpdateCommand="UPDATE [YANTRA_QUOT_DET] SET [QUOT_ID] = @QUOT_ID, [ITEM_CODE] = @ITEM_CODE, [QUOT_DET_QTY] = @QUOT_DET_QTY, [QUOT_RATE] = @QUOT_RATE, [QUOT_DISC] = @QUOT_DISC, [QUOT_SPPRICE] = @QUOT_SPPRICE, [QUOT_ROOM] = @QUOT_ROOM, [QUOT_CURRENCY] = @QUOT_CURRENCY, [COLOR_ID] = @COLOR_ID, [OPTIONALID] = @OPTIONALID, [REMARKS] = @REMARKS, [ITEM_TYPE] = @ITEM_TYPE, [QUOT_FLOOR] = @QUOT_FLOOR, [Quot_OrderNo] = @Quot_OrderNo, [QUOT_DET_GST] = @QUOT_DET_GST, [QUOT_DET_GSTRATE] = @QUOT_DET_GSTRATE WHERE [QUOT_DET_ID] = @QUOT_DET_ID">
            <DeleteParameters>
                <asp:Parameter Name="QUOT_DET_ID" Type="Int64" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="QUOT_DET_ID" Type="Int64" />
                <asp:Parameter Name="QUOT_ID" Type="Int64" />
                <asp:Parameter Name="ITEM_CODE" Type="Int64" />
                <asp:Parameter Name="QUOT_DET_QTY" Type="String" />
                <asp:Parameter Name="QUOT_RATE" Type="Decimal" />
                <asp:Parameter Name="QUOT_DISC" Type="Decimal" />
                <asp:Parameter Name="QUOT_SPPRICE" Type="String" />
                <asp:Parameter Name="QUOT_ROOM" Type="String" />
                <asp:Parameter Name="QUOT_CURRENCY" Type="Int64" />
                <asp:Parameter Name="COLOR_ID" Type="Int64" />
                <asp:Parameter Name="OPTIONALID" Type="Int64" />
                <asp:Parameter Name="REMARKS" Type="String" />
                <asp:Parameter Name="ITEM_TYPE" Type="String" />
                <asp:Parameter Name="QUOT_FLOOR" Type="String" />
                <asp:Parameter Name="Quot_OrderNo" Type="Int64" />
                <asp:Parameter Name="QUOT_DET_GST" Type="Decimal" />
                <asp:Parameter Name="QUOT_DET_GSTRATE" Type="Decimal" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="QUOT_ID" Type="Int64" />
                <asp:Parameter Name="ITEM_CODE" Type="Int64" />
                <asp:Parameter Name="QUOT_DET_QTY" Type="String" />
                <asp:Parameter Name="QUOT_RATE" Type="Decimal" />
                <asp:Parameter Name="QUOT_DISC" Type="Decimal" />
                <asp:Parameter Name="QUOT_SPPRICE" Type="String" />
                <asp:Parameter Name="QUOT_ROOM" Type="String" />
                <asp:Parameter Name="QUOT_CURRENCY" Type="Int64" />
                <asp:Parameter Name="COLOR_ID" Type="Int64" />
                <asp:Parameter Name="OPTIONALID" Type="Int64" />
                <asp:Parameter Name="REMARKS" Type="String" />
                <asp:Parameter Name="ITEM_TYPE" Type="String" />
                <asp:Parameter Name="QUOT_FLOOR" Type="String" />
                <asp:Parameter Name="Quot_OrderNo" Type="Int64" />
                <asp:Parameter Name="QUOT_DET_GST" Type="Decimal" />
                <asp:Parameter Name="QUOT_DET_GSTRATE" Type="Decimal" />
                <asp:Parameter Name="QUOT_DET_ID" Type="Int64" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDatasource2" runat ="server"  ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand ="Select  ITEM_MODEL_NO,ITEM_CODE as Itemcode1 from YANTRA_ITEM_MAST " ></asp:SqlDataSource>
    </div> 

    </form>
   
</body>
</html>
