<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/SalesMP1.master" AutoEventWireup="true" CodeFile="NewLead1.aspx.cs" Inherits="Modules_SM_NewLead1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../css/londinium-theme.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    
        <div class="demo-container no-bg">
            <%--<telerik:RadFormDecorator RenderMode="Lightweight" ID="FormDecorator1" runat="server" Skin="Metro" />--%>
 
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
                                                                AllowCustomEntry="True" OnEntryAdded="RadAutoCompleteBox1_EntryAdded" OnEntryRemoved="RadAutoCompleteBox1_EntryRemoved" OnTextChanged="RadAutoCompleteBox1_TextChanged" Width="320px" DropDownWidth="318px" 
                                                                 DropDownHeight="300px" Filter="StartsWith">
                                                                 <DropDownItemTemplate>
                                                                     <table cellpadding="0" cellspacing="0"  >
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
                                                                                         <td>Name:
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
                                                                                     <%--<tr>
                                                                                         <td>Brand:
                                                                                         </td>
                                                                                         <td>
                                                                                             <%# DataBinder.Eval(Container.DataItem, "PRODUCT_COMPANY_NAME")%>
                                                                                         </td>
                                                                                     </tr>--%>
                                                                                     <tr>
                                                                                         <td>Code:
                                                                                         </td>
                                                                                         <td>
                                                                                             <%# DataBinder.Eval(Container.DataItem, "ITEM_CODE")%>
                                                                                         </td>
                                                                                     </tr>
                                                                                 </table>
                                                                             </td>
                                                                             <td align="right" style="width: 25%; padding-left: 10px;">
                                                                                   <%--<asp:Image ID="Image" runat="server" EnableTheming="False" Height="132px" ImageUrl='<%# Eval("Item_Image","~/Content/ItemImage/{0}") %>' Width="141px" />--%>
                               
                                                                             </td>
                                                                         </tr>
                                                                     </table>
                                                                     </table>
                                                                 </DropDownItemTemplate>
                                                             </telerik:RadAutoCompleteBox>
                                                             <asp:SqlDataSource ID="dsItem" runat ="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommandType="Text" SelectCommand ="select * from YANTRA_ITEM_MAST I left outer join YANTRA_ITEM_IMAGE on i.ITEM_CODE =YANTRA_ITEM_IMAGE .Item_Code where I.BRAND_ID=@BranbId and I.IC_ID=@CateID and I.IT_TYPE_ID=@SubCateId ORDER BY I.ITEM_MODEL_NO"   >
                <SelectParameters>
                                            <asp:ControlParameter PropertyName="SelectedValue" Type="Int64" DefaultValue="0" Name="BranbId" ControlID="RadComboBox1"></asp:ControlParameter>
                                            <asp:ControlParameter PropertyName="SelectedValue" Type="Int64" DefaultValue="0" Name="CateID" ControlID="RadComboBox2"></asp:ControlParameter>
                                            <asp:ControlParameter PropertyName="SelectedValue" Type="Int64" DefaultValue="0" Name="SubCateID" ControlID="RadComboBox3"></asp:ControlParameter>

                                        </SelectParameters>
            </asp:SqlDataSource>
                                                         </div>
                                                         <label class="col-sm-1 control-label text-right">Colour :</label>
                                                         
                                                         <div class ="col-sm-3">
                                                             <telerik:RadRadioButtonList ID="ddlColor" runat="server"></telerik:RadRadioButtonList>
                                                         </div>


                                                     </div>
                                                 <div class="form-group">
                                                     <label class="col-sm-1 control-label text-right">Model Name :</label>
                                                     <div class ="col-sm-3">
                                                         <asp:TextBox ID ="txtCode" TextMode ="MultiLine"  runat ="server" ></asp:TextBox>
                                                         </div>
                                                 </div>
                                             </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
            </div> 
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadComboBox1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadComboBox2" />
                    <telerik:AjaxUpdatedControl ControlID="RadComboBox3" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadComboBox2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadComboBox3" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
</asp:Content>
