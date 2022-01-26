<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true" CodeFile="RH_Ratings.aspx.cs" Inherits="Modules_HR_RH_Ratings" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="sds" Namespace="Telerik.Web.SessionDS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link href="../../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../css/londinium-theme.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
     <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        </telerik:RadStyleSheetManager>

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
    <div class="row">
            <div class="panel-body"></div>
        </div>
    <div class="row">
         <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid1" runat="server" Width="550" AllowPaging="true" AllowSorting="true"
            OnNeedDataSource="RadGrid1_NeedDataSource" PageSize="10" CssClass="gridAuto">
             <MasterTableView AutoGenerateColumns="False" CommandItemDisplay="None" TableLayout="Auto">
                 <Columns>
                    <telerik:GridBoundColumn AllowSorting="true" DataField="RV_Question" HeaderText="Performance Criteria"
                        SortExpression="RV_Question" UniqueName="RV_Question">
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn HeaderText="Justification">
                        <ItemTemplate>
                            <telerik:RadRating RenderMode="Lightweight" ID="RadRating1" runat="server" AutoPostBack="true" Value='<%# Convert.ToDouble(Eval("AverageRating")) %>'
                                OnRate="RadRating1_Rate" Precision="Exact" ReadOnly='<%# Convert.ToBoolean(Eval("Status")) %>'>
                            </telerik:RadRating>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn AllowSorting="true" DataField="Status" HeaderText="Status"
                        SortExpression="Status" UniqueName="Status">
                    </telerik:GridBoundColumn>
                </Columns>
                 <PagerStyle Mode="NumericPages"></PagerStyle>
             </MasterTableView>
         </telerik:RadGrid>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [RV_Question],RV_Question_Desc FROM [YANTRA_LKUP_REVIEW_QUESTIONS] WHERE ([RV_CAT_ID] = @RV_CAT_ID)">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="1" Name="RV_CAT_ID" Type="Int64" />
                        </SelectParameters>
                    </asp:SqlDataSource>
    </div>
</asp:Content>

