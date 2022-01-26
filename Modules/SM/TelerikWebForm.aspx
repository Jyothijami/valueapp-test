﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TelerikWebForm.aspx.cs" Inherits="TelerikWebForm" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../css/londinium-theme.css" rel="stylesheet" />
    <telerik:RadStyleSheetManager id="RadStyleSheetManager1" runat="server" />
 
      
      
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
        </Scripts>
    </telerik:RadScriptManager>

    <script type="text/javascript">
        //Put your JavaScript code here.
    </script>
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
    <div>
        <telerik:RadGrid ID="RadGrid1" EnableTheming ="False" runat="server" AllowPaging="True" AllowSorting="True" GroupPanelPosition="Top" CommandItemSettings-AddNewRecordText="Add WalkIn" AutoGenerateEditColumn="True" AutoGenerateDeleteColumn="True"    DataSourceID="SqlDataSource1" ShowGroupPanel="True" AllowAutomaticDeletes="True" AllowAutomaticInserts="True" AllowAutomaticUpdates="True" PageSize="10" CellSpacing="-1" GridLines="Both" Skin="Sunset">
 <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                <ExportSettings Pdf-AllowPrinting="true" OpenInNewWindow="true" IgnorePaging="True">
                        <Pdf PaperSize="A4">
                        </Pdf>
                    </ExportSettings>
                <ClientSettings AllowDragToGroup="True"></ClientSettings>
            <MasterTableView DataSourceID="SqlDataSource1" AutoGenerateColumns="False" DataKeyNames="ENQ_DET_ID" CommandItemDisplay="Top" Font-Size="Larger" BorderColor="Red" EditMode="PopUp" EditFormSettings-PopUpSettings-Modal="true"  >
                <Columns>
                    <telerik:GridBoundColumn DataField="ENQ_DET_ID" DataType="System.Int64" FilterControlAltText="Filter ENQ_DET_ID column" HeaderText="ENQ_DET_ID" ReadOnly="True" SortExpression="ENQ_DET_ID" UniqueName="ENQ_DET_ID">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ENQ_ID" DataType="System.Int64" FilterControlAltText="Filter ENQ_ID column" HeaderText="ENQ_ID" SortExpression="ENQ_ID" UniqueName="ENQ_ID">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ITEM_CODE" DataType="System.Int64" FilterControlAltText="Filter ITEM_CODE column" HeaderText="ITEM_CODE" SortExpression="ITEM_CODE" UniqueName="ITEM_CODE">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ENQ_DET_QTY" FilterControlAltText="Filter ENQ_DET_QTY column" HeaderText="ENQ_DET_QTY" SortExpression="ENQ_DET_QTY" UniqueName="ENQ_DET_QTY">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ENQ_DET_SPEC" FilterControlAltText="Filter ENQ_DET_SPEC column" HeaderText="ENQ_DET_SPEC" SortExpression="ENQ_DET_SPEC" UniqueName="ENQ_DET_SPEC">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ENQ_DET_REMARKS" FilterControlAltText="Filter ENQ_DET_REMARKS column" HeaderText="ENQ_DET_REMARKS" SortExpression="ENQ_DET_REMARKS" UniqueName="ENQ_DET_REMARKS">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ENQ_DET_PRIORITY" FilterControlAltText="Filter ENQ_DET_PRIORITY column" HeaderText="ENQ_DET_PRIORITY" SortExpression="ENQ_DET_PRIORITY" UniqueName="ENQ_DET_PRIORITY">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ENQ_DOC_CHARGES" FilterControlAltText="Filter ENQ_DOC_CHARGES column" HeaderText="ENQ_DOC_CHARGES" SortExpression="ENQ_DOC_CHARGES" UniqueName="ENQ_DOC_CHARGES">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ENQ_DOC_INFAVOUROF" FilterControlAltText="Filter ENQ_DOC_INFAVOUROF column" HeaderText="ENQ_DOC_INFAVOUROF" SortExpression="ENQ_DOC_INFAVOUROF" UniqueName="ENQ_DOC_INFAVOUROF">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ENQ_EMD_CHARGES" FilterControlAltText="Filter ENQ_EMD_CHARGES column" HeaderText="ENQ_EMD_CHARGES" SortExpression="ENQ_EMD_CHARGES" UniqueName="ENQ_EMD_CHARGES">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ENQ_EMD_INFAVOUROF" FilterControlAltText="Filter ENQ_EMD_INFAVOUROF column" HeaderText="ENQ_EMD_INFAVOUROF" SortExpression="ENQ_EMD_INFAVOUROF" UniqueName="ENQ_EMD_INFAVOUROF">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ENQ_DET_ROOM" FilterControlAltText="Filter ENQ_DET_ROOM column" HeaderText="ENQ_DET_ROOM" SortExpression="ENQ_DET_ROOM" UniqueName="ENQ_DET_ROOM">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="COLOR_ID" DataType="System.Int64" FilterControlAltText="Filter COLOR_ID column" HeaderText="COLOR_ID" SortExpression="COLOR_ID" UniqueName="COLOR_ID">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Model_No" FilterControlAltText="Filter Model_No column" HeaderText="Model_No" SortExpression="Model_No" UniqueName="Model_No">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Colorname" FilterControlAltText="Filter Colorname column" HeaderText="Colorname" SortExpression="Colorname" UniqueName="Colorname">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Brand" FilterControlAltText="Filter Brand column" HeaderText="Brand" SortExpression="Brand" UniqueName="Brand">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ItemName" FilterControlAltText="Filter ItemName column" HeaderText="ItemName" SortExpression="ItemName" UniqueName="ItemName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="UOM" FilterControlAltText="Filter UOM column" HeaderText="UOM" SortExpression="UOM" UniqueName="UOM">
                    </telerik:GridBoundColumn>
                </Columns>
                <EditFormSettings CaptionFormatString="Walkin Details" EditFormType="Template" PopUpSettings-Width="800" PopUpSettings-Height="100%">
                     <EditColumn UniqueName="EditCommandColumn1"></EditColumn>
                       <FormTemplate>
                            <div class="form-horizontal ">
                                <div class="panel panel-default">
                                    <div class ="panel-body">
                                        <div class="panel panel-default">
                                             <div class="panel-body">
                                                 <div class="form-group">
                                                    <label class="col-sm-1 control-label text-right">Brand :</label>
                                                    <div class="col-sm-2">
                                                        <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboBox1" runat="server" Width="186px"
                                                            AutoPostBack="true" EmptyMessage="- Select a Brand -"
                                                            OnSelectedIndexChanged="RadComboBox1_SelectedIndexChanged"
                                                            Skin="Metro">
                                                        </telerik:RadComboBox>
                                                    </div>
                                                    <label class="col-sm-1 control-label text-right">Category :</label>
                                                    <div class="col-sm-2">
                                                        <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboBox2" runat="server" Width="186px"
                                                            AutoPostBack="true" EmptyMessage="- Select a Category -"
                                                            OnSelectedIndexChanged="RadComboBox2_SelectedIndexChanged"
                                                            Skin="Metro">
                                                        </telerik:RadComboBox>
                                                    </div>
                                                     <label class="col-sm-1 control-label text-right">Sub Category :</label>
                                                    <div class="col-sm-2">
                                                        <telerik:RadComboBox  AutoPostBack="true" RenderMode="Lightweight" ID="RadComboBox3" OnSelectedIndexChanged ="RadComboBox3_SelectedIndexChanged" runat="server" Width="186px" Skin="Metro"
                                                            EmptyMessage="- Select a Subcategory -">
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </div >
                                                 <div class="form-group">
                                                         <label class="col-sm-1 control-label text-right">Model Name :</label>
                                                         <div class="col-sm-4">
                                                             <telerik:RadAutoCompleteBox RenderMode="Lightweight" DataSourceID ="dsItem" DataTextField ="ITEM_NAME"  runat="server" ID="RadAutoCompleteBox1" InputType="Token" EmptyMessage="Select Item"
                                                                 Width="320px" DropDownWidth="318px" 
                                                                 DropDownHeight="300px" Filter="StartsWith">
                                                                 <DropDownItemTemplate>
                                                                     <table cellpadding="0" cellspacing="0" border ="1" >
                                                                         <tr>
                                                                             <td>
                                                                                 <table cellpadding="0" cellspacing="0">
                                                                                     <tr>
                                                                                         <td style="width: 25%">Model No:
                                                                                         </td>
                                                                                         <td style="width: 50%">
                                                                                             <%# DataBinder.Eval(Container.DataItem, "ITEM_MODEL_NO")%>
                                                                                         </td>
                                                                                     </tr>
                                                                                     <tr>
                                                                                         <td>Model Name:
                                                                                         </td>
                                                                                         <td>
                                                                                             <%# DataBinder.Eval(Container.DataItem, "ITEM_NAME")%>
                                                                                         </td>
                                                                                     </tr>
                                                                                     <tr>
                                                                                         <td>Spec:
                                                                                         </td>
                                                                                         <td>
                                                                                             <%# DataBinder.Eval(Container.DataItem, "Item_SPEC")%>
                                                                                         </td>
                                                                                     </tr>
                                                                                     <tr>
                                                                                         <td>Brand:
                                                                                         </td>
                                                                                         <td>
                                                                                             <%# DataBinder.Eval(Container.DataItem, "PRODUCT_COMPANY_NAME")%>
                                                                                         </td>
                                                                                     </tr>
                                                                                     <tr>
                                                                                         <td>Type:
                                                                                         </td>
                                                                                         <td>
                                                                                             <%# DataBinder.Eval(Container.DataItem, "ITEM_CODE")%>
                                                                                         </td>
                                                                                     </tr>
                                                                                 </table>
                                                                             </td>
                                                                             <td align="right" style="width: 25%; padding-left: 10px;">
                                                                                 <%--  <asp:Image ID="Image" runat="server" EnableTheming="False" Height="132px" ImageUrl='<%# Eval("Item_Image","~/Content/ItemImage/{0}") %>'
                                        Width="141px" />--%>
                               
                                                                             </td>
                                                                         </tr>
                                                                     </table>
                                                                     </table>
                                                                 </DropDownItemTemplate>
                                                             </telerik:RadAutoCompleteBox>
                                                             <asp:SqlDataSource ID="dsItem" runat ="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommandType="Text" SelectCommand ="SELECT ITEM_NAME,ITEM_MODEL_NO ,ITEM_SPEC ,I.ITEM_CODE,PRODUCT_COMPANY_NAME  FROM YANTRA_ITEM_MAST I,YANTRA_LKUP_PRODUCT_COMPANY B where B.PRODUCT_COMPANY_ID = I.BRAND_ID AND I.BRAND_ID=@BranbId and I.IC_ID=@CateID and I.IT_TYPE_ID=@SubCateId ORDER BY I.ITEM_MODEL_NO"   >
                <SelectParameters>
                                            <asp:ControlParameter PropertyName="SelectedValue" Type="Int64" DefaultValue="0" Name="BranbId" ControlID="RadComboBox1"></asp:ControlParameter>
                                            <asp:ControlParameter PropertyName="SelectedValue" Type="Int64" DefaultValue="0" Name="CateID" ControlID="RadComboBox2"></asp:ControlParameter>
                                            <asp:ControlParameter PropertyName="SelectedValue" Type="Int64" DefaultValue="0" Name="SubCateID" ControlID="RadComboBox3"></asp:ControlParameter>

                                        </SelectParameters>
            </asp:SqlDataSource>
                                                         </div>



                                                     </div>
                                                    
									
                                             </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                       </FormTemplate>

                </EditFormSettings>

            </MasterTableView>
        </telerik:RadGrid>
               <asp:SqlDataSource ID="dsBrand" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="Select PRODUCT_COMPANY_ID,PRODUCT_COMPANY_NAME from YANTRA_LKUP_PRODUCT_COMPANY order by PRODUCT_COMPANY_NAME "></asp:SqlDataSource>
      
        <asp:SqlDataSource ID="dsItem" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="select Distinct  ITEM_NAME,ITEM_MODEL_NO ,ITEM_SPEC ,Item_Image ,YANTRA_ITEM_MAST.ITEM_CODE,IT_TYPE,PRODUCT_COMPANY_NAME   from YANTRA_ITEM_MAST inner join YANTRA_LKUP_ITEM_TYPE on YANTRA_ITEM_MAST .IT_TYPE_ID =YANTRA_LKUP_ITEM_TYPE .IT_TYPE_ID 
	inner join YANTRA_LKUP_PRODUCT_COMPANY on YANTRA_ITEM_MAST .BRAND_ID =YANTRA_LKUP_PRODUCT_COMPANY .PRODUCT_COMPANY_ID 
	inner join YANTRA_ITEM_IMAGE on YANTRA_ITEM_MAST .ITEM_CODE =YANTRA_ITEM_IMAGE .Item_Code where YANTRA_ITEM_MAST .BRAND_ID=@PRODUCT_COMPANY_ID  ">
            <SelectParameters>
                <%--<asp:ControlParameter ControlId ="RadGrid1$ddlBrand" Name ="PRODUCT_COMPANY_ID" />--%>
            </SelectParameters>
        </asp:SqlDataSource>

                                                </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" DeleteCommand="DELETE FROM [YANTRA_ENQ_DET] WHERE [ENQ_DET_ID] = @ENQ_DET_ID" InsertCommand="INSERT INTO [YANTRA_ENQ_DET] ([ENQ_DET_ID], [ENQ_ID], [ITEM_CODE], [ENQ_DET_QTY], [ENQ_DET_SPEC], [ENQ_DET_REMARKS], [ENQ_DET_PRIORITY], [ENQ_DOC_CHARGES], [ENQ_DOC_INFAVOUROF], [ENQ_EMD_CHARGES], [ENQ_EMD_INFAVOUROF], [ENQ_DET_ROOM], [COLOR_ID], [Model_No], [Colorname], [Brand], [ItemName], [UOM]) VALUES (@ENQ_DET_ID, @ENQ_ID, @ITEM_CODE, @ENQ_DET_QTY, @ENQ_DET_SPEC, @ENQ_DET_REMARKS, @ENQ_DET_PRIORITY, @ENQ_DOC_CHARGES, @ENQ_DOC_INFAVOUROF, @ENQ_EMD_CHARGES, @ENQ_EMD_INFAVOUROF, @ENQ_DET_ROOM, @COLOR_ID, @Model_No, @Colorname, @Brand, @ItemName, @UOM)" SelectCommand="SELECT * FROM [YANTRA_ENQ_DET] WHERE ([ENQ_ID] = @ENQ_ID)" UpdateCommand="UPDATE [YANTRA_ENQ_DET] SET [ENQ_ID] = @ENQ_ID, [ITEM_CODE] = @ITEM_CODE, [ENQ_DET_QTY] = @ENQ_DET_QTY, [ENQ_DET_SPEC] = @ENQ_DET_SPEC, [ENQ_DET_REMARKS] = @ENQ_DET_REMARKS, [ENQ_DET_PRIORITY] = @ENQ_DET_PRIORITY, [ENQ_DOC_CHARGES] = @ENQ_DOC_CHARGES, [ENQ_DOC_INFAVOUROF] = @ENQ_DOC_INFAVOUROF, [ENQ_EMD_CHARGES] = @ENQ_EMD_CHARGES, [ENQ_EMD_INFAVOUROF] = @ENQ_EMD_INFAVOUROF, [ENQ_DET_ROOM] = @ENQ_DET_ROOM, [COLOR_ID] = @COLOR_ID, [Model_No] = @Model_No, [Colorname] = @Colorname, [Brand] = @Brand, [ItemName] = @ItemName, [UOM] = @UOM WHERE [ENQ_DET_ID] = @ENQ_DET_ID">
            <DeleteParameters>
                <asp:Parameter Name="ENQ_DET_ID" Type="Int64" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="ENQ_DET_ID" Type="Int64" />
                <asp:Parameter Name="ENQ_ID" Type="Int64" />
                <asp:Parameter Name="ITEM_CODE" Type="Int64" />
                <asp:Parameter Name="ENQ_DET_QTY" Type="String" />
                <asp:Parameter Name="ENQ_DET_SPEC" Type="String" />
                <asp:Parameter Name="ENQ_DET_REMARKS" Type="String" />
                <asp:Parameter Name="ENQ_DET_PRIORITY" Type="String" />
                <asp:Parameter Name="ENQ_DOC_CHARGES" Type="String" />
                <asp:Parameter Name="ENQ_DOC_INFAVOUROF" Type="String" />
                <asp:Parameter Name="ENQ_EMD_CHARGES" Type="String" />
                <asp:Parameter Name="ENQ_EMD_INFAVOUROF" Type="String" />
                <asp:Parameter Name="ENQ_DET_ROOM" Type="String" />
                <asp:Parameter Name="COLOR_ID" Type="Int64" />
                <asp:Parameter Name="Model_No" Type="String" />
                <asp:Parameter Name="Colorname" Type="String" />
                <asp:Parameter Name="Brand" Type="String" />
                <asp:Parameter Name="ItemName" Type="String" />
                <asp:Parameter Name="UOM" Type="String" />
            </InsertParameters>
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="86" Name="ENQ_ID" QueryStringField="lblEnqID" Type="Int64" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="ENQ_ID" Type="Int64" />
                <asp:Parameter Name="ITEM_CODE" Type="Int64" />
                <asp:Parameter Name="ENQ_DET_QTY" Type="String" />
                <asp:Parameter Name="ENQ_DET_SPEC" Type="String" />
                <asp:Parameter Name="ENQ_DET_REMARKS" Type="String" />
                <asp:Parameter Name="ENQ_DET_PRIORITY" Type="String" />
                <asp:Parameter Name="ENQ_DOC_CHARGES" Type="String" />
                <asp:Parameter Name="ENQ_DOC_INFAVOUROF" Type="String" />
                <asp:Parameter Name="ENQ_EMD_CHARGES" Type="String" />
                <asp:Parameter Name="ENQ_EMD_INFAVOUROF" Type="String" />
                <asp:Parameter Name="ENQ_DET_ROOM" Type="String" />
                <asp:Parameter Name="COLOR_ID" Type="Int64" />
                <asp:Parameter Name="Model_No" Type="String" />
                <asp:Parameter Name="Colorname" Type="String" />
                <asp:Parameter Name="Brand" Type="String" />
                <asp:Parameter Name="ItemName" Type="String" />
                <asp:Parameter Name="UOM" Type="String" />
                <asp:Parameter Name="ENQ_DET_ID" Type="Int64" />
            </UpdateParameters>
        </asp:SqlDataSource>
    </div>
    </form>
</body>

</html>
