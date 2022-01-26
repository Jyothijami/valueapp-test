﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/SalesMP1.master" AutoEventWireup="true" CodeFile="WalkIn.aspx.cs" Inherits="Modules_SM_WalkIn" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../css/londinium-theme.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">

    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        </telerik:RadStyleSheetManager>
        <%--<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js"></asp:ScriptReference>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js"></asp:ScriptReference>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js"></asp:ScriptReference>
            </Scripts>
        </telerik:RadScriptManager>--%>
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
            <telerik:RadGrid ID="RadGrid1" EnableTheming ="False" runat="server" AllowFilteringByColumn ="true" AllowPaging="True" AllowSorting="True" AutoGenerateEditColumn="True" AutoGenerateDeleteColumn="True"    DataSourceID="SqlDataSource1" ShowGroupPanel="True" AllowAutomaticDeletes="True" AllowAutomaticInserts="True" AllowAutomaticUpdates="True" CellSpacing="-1" GridLines="Both" Skin="Sunset">

                <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                <ExportSettings Pdf-AllowPrinting="true" OpenInNewWindow="true" IgnorePaging="True">
                        <Pdf PaperSize="A4">
                        </Pdf>
                    </ExportSettings>
                <ClientSettings AllowDragToGroup="True"></ClientSettings>
                <MasterTableView DataSourceID="SqlDataSource1" AutoGenerateColumns="False" DataKeyNames="ID"  CommandItemDisplay="Top" Font-Size="Larger" BorderColor="Red" EditMode="PopUp" EditFormSettings-PopUpSettings-Modal="true"  >
                    <Columns>
                        <%--<telerik:GridBoundColumn DataField="Date" DataType="System.DateTime" FilterControlAltText="Filter Date column" HeaderText="Date" SortExpression="Date" UniqueName="Date">
        </telerik:GridBoundColumn>--%>
                        <%--<telerik:GridBoundColumn DataField="Source" DataType="System.Int64" FilterControlAltText="Filter Source column" HeaderText="Source" SortExpression="Source" UniqueName="Source">
        </telerik:GridBoundColumn>--%>
                        <%--<telerik:GridBoundColumn DataField="Assisted" DataType="System.Int64" FilterControlAltText="Filter Assisted column" HeaderText="Assisted" SortExpression="Assisted" UniqueName="Assisted">
                    </telerik:GridBoundColumn>--%>
                        <telerik:GridEditCommandColumn ButtonType="LinkButton"   ></telerik:GridEditCommandColumn>
                        
                        <telerik:GridBoundColumn DataField="ID" DataType="System.Int64" FilterControlAltText="Filter ID column"  FilterControlWidth="40px" ShowFilterIcon ="false" CurrentFilterFunction ="EqualTo" AutoPostBackOnFilter="false" FilterDelay="1000" HeaderText="ID" ReadOnly="True" SortExpression="ID" UniqueName="ID">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="WalkIn No" AllowFiltering ="false"  >
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "WalkIn_No")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtWalkInNo" runat ="server" Text ='<%# Bind("WalkIn_No") %>' ></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Width="240px" />
                        </telerik:GridTemplateColumn>
                        <%--<telerik:GridBoundColumn DataField="WalkIn_No"  FilterControlAltText="Filter WalkIn_No column" HeaderText="WalkIn No"  SortExpression="ContactPerson" UniqueName="WalkIn_No">
                        </telerik:GridBoundColumn>--%>
                        <telerik:GridTemplateColumn DataField="Date" HeaderStyle-Font-Bold="true" AllowFiltering ="false"  GroupByExpression ="Date Dt Group By Date" ItemStyle-Font-Bold="true" DataType="System.DateTime" HeaderText="Date" SortExpression="Date" UniqueName="Date">
                            <EditItemTemplate>
                                <%-- <asp:TextBox ID="MfgTextBox" runat="server" Text='<%# Bind("Mfg") %>'></asp:TextBox>--%>

                                <telerik:RadDatePicker RenderMode="Lightweight" ID="BirthDatePicker" runat="server" MinDate="1/1/1900" DbSelectedDate='<%# Bind("Date") %>'
                                    TabIndex="4">
                                </telerik:RadDatePicker>



                            </EditItemTemplate>
                            <ItemTemplate>
                                <telerik:RadDatePicker RenderMode="Lightweight" ID="BirthDatePicker" runat="server" MinDate="1/1/1900" DbSelectedDate='<%# Bind("Date") %>'
                                    TabIndex="4">
                                </telerik:RadDatePicker>
                            </ItemTemplate>

                            <HeaderStyle Font-Bold="True"></HeaderStyle>

                            <ItemStyle Font-Bold="True"></ItemStyle>
                        </telerik:GridTemplateColumn>
                         <telerik:GridBoundColumn DataField="CustName" GroupByExpression ="CustName Customer Group By CustName" FilterControlWidth="60px"   FilterControlAltText="Filter CustName column" HeaderText="CustName"  SortExpression="CustName" UniqueName="CustName">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Billing_Name" GroupByExpression ="Billing_Name BillingName Group By Billing_Name" FilterControlWidth="60px" FilterControlAltText="Filter Billing_Name column"   HeaderText="Billing_Name" SortExpression="Billing_Name" UniqueName="Billing_Name">
                        </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn DataField="ContactPerson" GroupByExpression ="ContactPerson ContactPersonName Group By ContactPerson" FilterControlWidth="60px" FilterControlAltText="Filter ContactPerson column" HeaderText="ContactPerson"  SortExpression="ContactPerson" UniqueName="ContactPerson">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Phone" GroupByExpression ="Phone Phone Group By Phone" FilterControlAltText="Filter Phone column" FilterControlWidth="60px"  HeaderText="Phone" SortExpression="Phone" UniqueName="Phone">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Email" GroupByExpression ="Email Email Group By Email" FilterControlAltText="Filter Email column" FilterControlWidth="60px"  HeaderText="Email" SortExpression="Email" UniqueName="Email">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Address" GroupByExpression ="Address Address Group By Address" FilterControlAltText="Filter Address column" FilterControlWidth="60px"   HeaderText="Address" SortExpression="Address" UniqueName="Address">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Requirement" GroupByExpression ="Requirement Requirement Group By Requirement" FilterControlWidth="60px"  FilterControlAltText="Filter Requirement column"   HeaderText="Requirement" SortExpression="Requirement" UniqueName="Requirement">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn DataField="ENQM_NAME" HeaderText="Source" UniqueName="ENQM_NAME">
                            <FilterTemplate>
                                <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboBox1" DataSourceID="SourceDs" DataTextField="ENQM_NAME"
                                    DataValueField="ENQM_NAME" Height="200px" AppendDataBoundItems="true"
                                    SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("ENQM_NAME").CurrentFilterValue %>'
                                    runat="server" OnClientSelectedIndexChanged="SelectedIndexChanged">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Select a Source" />
                                    </Items>
                                </telerik:RadComboBox>
                                <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">
                                    <script type="text/javascript">
                                        function SelectedIndexChanged(sender, args) {
                                            var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                            tableView.filter("ENQM_NAME", args.get_item().get_value(), "EqualTo");
                                        }
                                    </script>
                                </telerik:RadScriptBlock>
                            </FilterTemplate>
                            <ItemTemplate>
                                <%# Eval("ENQM_NAME") %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="InTime" GroupByExpression ="InTime InTime Group By InTime" AllowFiltering ="false"  FilterControlAltText="Filter InTime column" HeaderText="InTime" SortExpression="InTime" UniqueName="InTime">
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="OutTime" GroupByExpression ="OutTime OutTime Group By OutTime" AllowFiltering ="false"  FilterControlAltText="Filter OutTime column" HeaderText="OutTime" SortExpression="OutTime" UniqueName="OutTime">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Attendent"  FilterControlWidth="60px"   GroupByExpression ="AttendedBy Attendent Group By AttendedBy">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "AttendedBy")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <telerik:RadDropDownList RenderMode="Lightweight" runat="server" ID="ddlAttendent" DataTextField="Emp_First_Name"
                                    DataValueField="Emp_ID" DataSourceID="AttendentDS" SelectedValue='<%#Bind("Attendent") %>'>
                                </telerik:RadDropDownList>
                            </EditItemTemplate>
                            <ItemStyle Width="240px" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn HeaderText="Assisted" FilterControlWidth="60px"  GroupByExpression ="AssistedBy Assisted Group By AssistedBy">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "AssistedBy")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <telerik:RadDropDownList RenderMode="Lightweight" runat="server" ID="ddlAssisted" DataTextField="Emp_First_Name"
                                    DataValueField="Emp_ID" DataSourceID="AssistedDs" SelectedValue='<%#Bind("Assisted") %>'>
                                </telerik:RadDropDownList>
                            </EditItemTemplate>
                            <ItemStyle Width="240px" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn HeaderText="Backup Support" FilterControlWidth="60px"   GroupByExpression ="BackupSBy BackupSupport Group By BackupSBy" >
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "BackupSBy")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <telerik:RadDropDownList RenderMode="Lightweight" runat="server" ID="ddlBackup" DataTextField="Emp_First_Name"
                                    DataValueField="Emp_ID" DataSourceID="AssistedDs" SelectedValue='<%#Bind("Backup_Support") %>'>
                                </telerik:RadDropDownList>
                            </EditItemTemplate>
                            <ItemStyle Width="240px" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn HeaderText="Additional Support" FilterControlWidth="60px"  GroupByExpression ="AddBackupBy AdditionalSupp Group By AddBackupBy">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "AddBackupBy")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <telerik:RadDropDownList RenderMode="Lightweight" runat="server" ID="ddlAddBackup" DataTextField="Emp_First_Name"
                                    DataValueField="Emp_ID" DataSourceID="AddBackupDs" SelectedValue='<%#Bind("Additional_Support") %>'>
                                </telerik:RadDropDownList>
                            </EditItemTemplate>
                            <ItemStyle Width="240px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Automation Explained" FilterControlWidth="40px"  GroupByExpression ="Automation_Exp Automation Group By Automation_Exp">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "Automation_Exp")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <telerik:RadDropDownList RenderMode="Lightweight" runat="server" ID="ddlAutomation" DataTextField="Automation_Exp"
                                    DataValueField="Automation_Exp" SelectedValue='<%#Bind("Automation_Exp") %>'>
                                    <Items>
                                        <telerik:DropDownListItem Text="--" Value="Not Selected" />
                                        <telerik:DropDownListItem Text="Yes" Value="Yes" />
                                        <telerik:DropDownListItem Text="No" Value="No" />
                                    </Items>
                                </telerik:RadDropDownList>
                            </EditItemTemplate>
                            <ItemStyle Width="240px" />
                        </telerik:GridTemplateColumn>
                       
                    </Columns>
                    <EditFormSettings CaptionFormatString="Walkin Details" EditFormType="Template" PopUpSettings-Width="800" PopUpSettings-Height="100%">
                        <EditColumn UniqueName="EditCommandColumn1"></EditColumn>
                        <FormTemplate>
                            <div class="form-horizontal ">
                                <div class="panel panel-default">
                                    <div class ="panel-body">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <h6 class="panel-title">WalkIn Details</h6>
                                            </div>
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <label class="col-sm-1 control-label text-right"></label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtWalkInNo" Visible ="false"  runat ="server"  Text ='<%# Bind("WalkIn_No") %>'></asp:TextBox>
                                                    </div>
                                                    <label class="col-sm-1 control-label text-right">Walkin Date :</label>
                                                    <div class="col-sm-2">
                                                        <telerik:RadDatePicker RenderMode="Lightweight" ID="BirthDatePicker" runat="server" MinDate="1/1/1900" DbSelectedDate='<%# Bind("Date") %>'
                                                            TabIndex="4">
                                                        </telerik:RadDatePicker>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-1 control-label text-right">Name :</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtCustName" runat ="server"  Text ='<%# Bind("CustName") %>'></asp:TextBox>
                                                    </div>
                                                    <label class="col-sm-1 control-label text-right">Billing Name :</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtBillingName" runat ="server"  Text ='<%# Bind("Billing_Name") %>'></asp:TextBox>
                                                    </div>
                                                </div>
                                                 <div class="form-group">
                                                    <label class="col-sm-1 control-label text-right">Contact Person :</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtContactPerson" runat ="server"  Text ='<%# Bind("ContactPerson") %>'></asp:TextBox>
                                                    </div>
                                                    <label class="col-sm-1 control-label text-right">Phone :</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtPhone" runat ="server"  Text ='<%# Bind("Phone") %>'></asp:TextBox>

                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-1 control-label text-right">Address :</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtAddress" runat ="server" TextMode ="MultiLine"   Text ='<%# Bind("Address") %>'></asp:TextBox>
                                                    </div>
                                                    <label class="col-sm-1 control-label text-right">Requirement :</label>
                                                    <div class="col-sm-3">
                                                          <asp:TextBox ID="txtReq" runat ="server" TextMode ="MultiLine" Text ='<%# Bind("Requirement") %>'></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-1 control-label text-right">Source :</label>
                                                    <div class="col-sm-3">
                                                        <telerik:RadDropDownList RenderMode="Lightweight" DefaultMessage ="Select Source"  runat="server" ID="ddlSOurce" DataTextField="ENQM_NAME"
                                                            DataValueField="ENQM_ID" DataSourceID="SourceDs" SelectedValue='<%#Bind("Source") %>'>
                                                           
                                                        </telerik:RadDropDownList>
                                                    </div>
                                                    <label class="col-sm-1 control-label text-right">Email :</label>
                                                    <div class="col-sm-3">
                                                          <asp:TextBox ID="txtEmail" runat ="server"  Text ='<%# Bind("Email") %>'></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-1 control-label text-right">In Time :</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtInTime" runat ="server"  Text ='<%# Bind("InTime") %>'></asp:TextBox>
                                                    </div>
                                                     <label class="col-sm-1 control-label text-right">Out Time :</label>
                                                    <div class="col-sm-3">
                                                          <asp:TextBox ID="txtOuttime" runat ="server"  Text ='<%# Bind("OutTime") %>'></asp:TextBox>
                                                    </div>
                                                </div>
                                                 <div class="form-group">
                                                    <label class="col-sm-1 control-label text-right">Attended By :</label>
                                                    <div class="col-sm-3">
                                                        <telerik:RadDropDownList RenderMode="Lightweight" runat="server" ID="ddlAttendent" DataTextField="Emp_First_Name"
                                                            DataValueField="Emp_ID" DefaultMessage ="Select Attendent"   DataSourceID="AttendentDS" SelectedValue='<%#Bind("Attendent") %>'>
                                                        </telerik:RadDropDownList>
                                                    </div>
                                                    <label class="col-sm-1 control-label text-right">Assisted By :</label>

                                                    <div class="col-sm-3">
                                                        <telerik:RadDropDownList RenderMode="Lightweight" runat="server" ID="ddlAssisted" DataTextField="Emp_First_Name"
                                                            DataValueField="Emp_ID" DataSourceID="AssistedDs" DefaultMessage ="Select Assisted"  SelectedValue='<%#Bind("Assisted") %>'>
                                                        </telerik:RadDropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                   
                                                    <label class="col-sm-1 control-label text-right">Backup Support:</label>
                                                    <div class="col-sm-3">
                                                        <telerik:RadDropDownList RenderMode="Lightweight" runat="server" ID="ddlBackup" DataTextField="Emp_First_Name"
                                                            DataValueField="Emp_ID" DataSourceID="AssistedDs" DefaultMessage ="Select Backup"  SelectedValue='<%#Bind("Backup_Support") %>'>
                                                        </telerik:RadDropDownList>
                                                    </div>
                                                    <label class="col-sm-2 control-label text-right">Additional Backup Support:</label>
                                                    <div class="col-sm-3">
                                                        <telerik:RadDropDownList RenderMode="Lightweight" runat="server" ID="ddlAddBackup" DataTextField="Emp_First_Name"
                                                            DataValueField="Emp_ID" DataSourceID="AddBackupDs" DefaultMessage ="Select Additional Backup"  SelectedValue='<%#Bind("Additional_Support") %>'>
                                                        </telerik:RadDropDownList>
                                                    </div>
                                                </div>
                                                <div>
                                                    <label class="col-sm-1 control-label text-right">Automation Explained:</label>
                                                    <div class="col-sm-3">
                                                         <telerik:RadDropDownList RenderMode="Lightweight" runat="server" ID="ddlAutomation" DataTextField="Automation_Exp"
                                    DataValueField="Automation_Exp" DefaultMessage ="Select Automation"  SelectedValue='<%#Bind("Automation_Exp") %>'>
                                    <Items>
                                        <telerik:DropDownListItem Text="--" Value="Not Selected" />
                                        <telerik:DropDownListItem Text="Yes" Value="Yes" />
                                        <telerik:DropDownListItem Text="No" Value="No" />
                                    </Items>
                                </telerik:RadDropDownList>
                                                    </div >
                                                    <div class="col-sm-4">
                                                    <telerik:RadButton ID="Button3" runat="server" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                            CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                            Icon-PrimaryIconCssClass="rbOk">
                                        </telerik:RadButton>
                                        <telerik:RadButton ID="Button4" runat="server" Text="Cancel" CausesValidation="false"
                                            CommandName="Cancel" Icon-PrimaryIconCssClass="rbCancel">
                                        </telerik:RadButton>
                                                        </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </FormTemplate>

<PopUpSettings Modal="True" Height="100%" Width="800px"></PopUpSettings>
                    </EditFormSettings>
                    <EditItemStyle Font-Bold="True" HorizontalAlign="Justify" VerticalAlign="Middle" />


                </MasterTableView>

                <PagerStyle Position="Top" />

            </telerik:RadGrid>

            <asp:SqlDataSource ID="SourceDs" runat ="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand ="SELECT ENQM_ID,ENQM_NAME FROM [YANTRA_LKUP_ENQ_MODE] "  ></asp:SqlDataSource>

        <asp:SqlDataSource ID="BackUpDs" runat ="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand ="SELECT a.EMP_ID,EMP_FIRST_NAME FROM [YANTRA_EMPLOYEE_MAST] a left outer join dbo.YANTRA_EMPLOYEE_DET b on a.EMP_ID=b.EMP_ID inner join YANTRA_DEPT_MAST c on c.DEPT_ID=b.DEPT_ID WHERE a.EMP_ID<>0   and (b.DEPT_ID=1 or b.Dept_Id=9 or  b.DEPT_ID=11  or  b.DEPT_ID=16 or  b.DEPT_ID=17 or  b.DEPT_ID=19 or  b.DEPT_ID=24 or  b.DEPT_ID=30 or b.DEPT_ID =32 or b.Dept_id=16  or c.DEPT_NAME like '%Sales%' or b.DEPT_ID=11) and STATUS !=0  ORDER BY EMP_FIRST_NAME"  ></asp:SqlDataSource>
        <asp:SqlDataSource ID="AttendentDS" runat ="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand ="SELECT a.EMP_ID,EMP_FIRST_NAME FROM [YANTRA_EMPLOYEE_MAST] a inner join dbo.YANTRA_EMPLOYEE_DET b on a.EMP_ID=b.EMP_ID inner join YANTRA_DEPT_MAST c on c.DEPT_ID=b.DEPT_ID WHERE a.EMP_ID<>0   and (b.DEPT_ID=1 or b.Dept_Id=9 or  b.DEPT_ID=11  or  b.DEPT_ID=16 or  b.DEPT_ID=17 or  b.DEPT_ID=19 or  b.DEPT_ID=24 or  b.DEPT_ID=30 or b.DEPT_ID =32 or b.Dept_id=16  or c.DEPT_NAME like '%Sales%' or b.DEPT_ID=11) and STATUS !=0  ORDER BY EMP_FIRST_NAME"  ></asp:SqlDataSource>
        <asp:SqlDataSource ID="AddBackupDs" runat ="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand ="SELECT a.EMP_ID,EMP_FIRST_NAME FROM [YANTRA_EMPLOYEE_MAST] a inner join dbo.YANTRA_EMPLOYEE_DET b on a.EMP_ID=b.EMP_ID inner join YANTRA_DEPT_MAST c on c.DEPT_ID=b.DEPT_ID WHERE a.EMP_ID<>0   and (b.DEPT_ID=1 or b.Dept_Id=9 or  b.DEPT_ID=11  or  b.DEPT_ID=16 or  b.DEPT_ID=17 or  b.DEPT_ID=19 or  b.DEPT_ID=24 or  b.DEPT_ID=30 or b.DEPT_ID =32 or b.Dept_id=16  or c.DEPT_NAME like '%Sales%' or b.DEPT_ID=11) and STATUS !=0  ORDER BY EMP_FIRST_NAME"  ></asp:SqlDataSource>

        <asp:SqlDataSource ID="AssistedDs" runat ="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand ="SELECT a.EMP_ID,EMP_FIRST_NAME FROM [YANTRA_EMPLOYEE_MAST] a inner join dbo.YANTRA_EMPLOYEE_DET b on a.EMP_ID=b.EMP_ID inner join YANTRA_DEPT_MAST c on c.DEPT_ID=b.DEPT_ID WHERE a.EMP_ID<>0   and (b.DEPT_ID=1 or b.Dept_Id=9 or  b.DEPT_ID=11  or  b.DEPT_ID=16 or  b.DEPT_ID=17 or  b.DEPT_ID=19 or  b.DEPT_ID=24 or  b.DEPT_ID=30 or b.DEPT_ID =32 or b.Dept_id=16  or c.DEPT_NAME like '%Sales%' or b.DEPT_ID=11) and STATUS !=0  ORDER BY EMP_FIRST_NAME"  ></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" DeleteCommand="DELETE FROM [WalkIn_tbl] WHERE [ID] = @original_ID" InsertCommand="INSERT INTO [WalkIn_tbl] ([Date], [Billing_Name], [Phone], [Email], [Address], [Requirement], [Source], [InTime], [OutTime], [Attendent], [Assisted], [Backup_Support], [Additional_Support], [Automation_Exp], [CustName], [WalkIn_No], [ContactPerson],[CP_ID]) VALUES (@Date, @Billing_Name, @Phone, @Email, @Address, @Requirement, @Source, @InTime, @OutTime, @Attendent, @Assisted, @Backup_Support, @Additional_Support, @Automation_Exp, @CustName, @WalkIn_No, @ContactPerson,@CP_ID)"
                SelectCommand="select walkin_tbl.*,ENQM_NAME,Attendent.EMP_FIRST_NAME +' '+Attendent.EMP_LAST_NAME as AttendedBy
,Assisted.EMP_FIRST_NAME +' '+Assisted.EMP_LAST_NAME as AssistedBy
,BackupS.EMP_FIRST_NAME +' '+BackupS.EMP_LAST_NAME as BackupSBy
,AddBackup.EMP_FIRST_NAME +' '+AddBackup.EMP_LAST_NAME as AddBackupBy from WalkIn_tbl
left outer join YANTRA_LKUP_ENQ_MODE on walkin_tbl.source=YANTRA_LKUP_ENQ_MODE.ENQM_ID 
left outer join YANTRA_EMPLOYEE_MAST Attendent on walkin_tbl.Attendent=Attendent.EMP_ID 
left outer join YANTRA_EMPLOYEE_MAST Assisted on walkin_tbl.Assisted=Assisted .EMP_ID 
left outer join YANTRA_EMPLOYEE_MAST BackupS on walkin_tbl.Backup_Support=BackupS .EMP_ID 
left outer join YANTRA_EMPLOYEE_MAST AddBackup on walkin_tbl.Additional_Support=AddBackup.emp_id where Attendent.Status!=0 ORDER BY walkin_tbl.ID DESC"
                UpdateCommand="UPDATE [WalkIn_tbl] SET [Date] = @Date, [Billing_Name] = @Billing_Name, [Phone] = @Phone, [Email] = @Email, [Address] = @Address, [Requirement] = @Requirement, [Source] = @Source, [InTime] = @InTime, [OutTime] = @OutTime, [Attendent] = @Attendent, [Assisted] = @Assisted, [Backup_Support] = @Backup_Support, [Additional_Support] = @Additional_Support, [Automation_Exp] = @Automation_Exp, [CustName] = @CustName, [WalkIn_No] = @WalkIn_No, [ContactPerson] = @ContactPerson WHERE [ID] = @original_ID" OldValuesParameterFormatString="original_{0}">
                <DeleteParameters>
                    <asp:Parameter Name="original_ID" Type="Int64" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="Date" Type="DateTime" />
                    <asp:Parameter Name="Billing_Name" Type="String" />
                    <asp:Parameter Name="Phone" Type="String" />
                    <asp:Parameter Name="Email" Type="String" />
                    <asp:Parameter Name="Address" Type="String" />
                    <asp:Parameter Name="Requirement" Type="String" />
                    <asp:Parameter Name="Source" Type="Int64" />
                    <asp:Parameter Name="InTime" Type="String" />
                    <asp:Parameter Name="OutTime" Type="String" />
                    <asp:Parameter Name="Attendent" Type="Int64" />
                    <asp:Parameter Name="Assisted" Type="Int64" />
                    <asp:Parameter Name="Backup_Support" Type="Int64" />
                    <asp:Parameter Name="Additional_Support" Type="Int64" />
                    <asp:Parameter Name="Automation_Exp" Type="String" />
                    <asp:Parameter Name="CustName" Type="String" />
                    <asp:Parameter Name="WalkIn_No" Type="String" />
                    <asp:Parameter Name="ContactPerson" Type="String" />
                    <asp:Parameter Name="CP_ID" Type="Int64" />

                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Date" Type="DateTime" />
                    <asp:Parameter Name="Billing_Name" Type="String" />
                    <asp:Parameter Name="Phone" Type="String" />
                    <asp:Parameter Name="Email" Type="String" />
                    <asp:Parameter Name="Address" Type="String" />
                    <asp:Parameter Name="Requirement" Type="String" />
                    <asp:Parameter Name="Source" Type="Int64" />
                    <asp:Parameter Name="InTime" Type="String" />
                    <asp:Parameter Name="OutTime" Type="String" />
                    <asp:Parameter Name="Attendent" Type="Int64" />
                    <asp:Parameter Name="Assisted" Type="Int64" />
                    <asp:Parameter Name="Backup_Support" Type="Int64" />
                    <asp:Parameter Name="Additional_Support" Type="Int64" />
                    <asp:Parameter Name="Automation_Exp" Type="String" />
<asp:Parameter Name="CustName" Type="String"></asp:Parameter>
<asp:Parameter Name="WalkIn_No" Type="String"></asp:Parameter>
<asp:Parameter Name="ContactPerson" Type="String"></asp:Parameter>
                    <%--<asp:Parameter Name="CP_ID" Type="Int64" />--%>

                    <asp:Parameter Name="original_ID" Type="Int64" />

                </UpdateParameters>
            </asp:SqlDataSource>
        </div>


</asp:Content>

