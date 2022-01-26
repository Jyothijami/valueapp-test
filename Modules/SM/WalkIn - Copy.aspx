﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/SalesMP1.master" AutoEventWireup="true" CodeFile="WalkIn - Copy.aspx.cs" Inherits="Modules_SM_WalkIn" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
            <telerik:RadGrid ID="RadGrid1" EnableTheming ="false" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateEditColumn="True" AutoGenerateDeleteColumn="True"    DataSourceID="SqlDataSource1" ShowGroupPanel="True" AllowAutomaticDeletes="True" AllowAutomaticInserts="True" AllowAutomaticUpdates="True" PageSize="20" CellSpacing="-1" GridLines="Both" Skin="Sunset">

                <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                <ExportSettings Pdf-AllowPrinting="true" OpenInNewWindow="true" IgnorePaging="True">
                        <Pdf PaperSize="A4">
                        </Pdf>
                    </ExportSettings>
                <ClientSettings AllowDragToGroup="True"></ClientSettings>
                <MasterTableView DataSourceID="SqlDataSource1" AutoGenerateColumns="False" DataKeyNames="ID" CommandItemDisplay="Top" Font-Size="Larger" BorderColor="Red">
                    <Columns>
                        <telerik:GridBoundColumn DataField="ID" DataType="System.Int64" FilterControlAltText="Filter ID column" HeaderText="ID" ReadOnly="True" SortExpression="ID" UniqueName="ID">
                        </telerik:GridBoundColumn>
                        <%--<telerik:GridBoundColumn DataField="Date" DataType="System.DateTime" FilterControlAltText="Filter Date column" HeaderText="Date" SortExpression="Date" UniqueName="Date">
        </telerik:GridBoundColumn>--%>
                        <telerik:GridTemplateColumn DataField="Date" HeaderStyle-Font-Bold="true" GroupByExpression ="Date Dt Group By Date" ItemStyle-Font-Bold="true" DataType="System.DateTime" HeaderText="Date" SortExpression="Date" UniqueName="Date">
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
                        <telerik:GridBoundColumn DataField="Billing_Name" FilterControlAltText="Filter Billing_Name column"   HeaderText="Billing_Name" SortExpression="Billing_Name" UniqueName="Billing_Name">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Phone" FilterControlAltText="Filter Phone column"  HeaderText="Phone" SortExpression="Phone" UniqueName="Phone">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Email" FilterControlAltText="Filter Email column"  HeaderText="Email" SortExpression="Email" UniqueName="Email">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Address" FilterControlAltText="Filter Address column"   HeaderText="Address" SortExpression="Address" UniqueName="Address">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Requirement" FilterControlAltText="Filter Requirement column"   HeaderText="Requirement" SortExpression="Requirement" UniqueName="Requirement">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Source" ItemStyle-Width="240px" GroupByExpression ="ENQM_NAME Enquiry Group By ENQM_NAME" >
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "ENQM_NAME")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <telerik:RadDropDownList RenderMode="Lightweight" runat="server" ID="ddlSOurce" DataTextField="ENQM_NAME"
                                    DataValueField="ENQM_ID" DataSourceID="SourceDs" SelectedValue='<%#Bind("Source") %>'>
                                    <%-- <Items>
                                    <telerik:DropDownListItem Text="--" Value="Not Selected" />
                                </Items> --%>
                                </telerik:RadDropDownList>
                            </EditItemTemplate>
                            <ItemStyle Width="240px" />
                        </telerik:GridTemplateColumn>
                        <%--<telerik:GridBoundColumn DataField="Source" DataType="System.Int64" FilterControlAltText="Filter Source column" HeaderText="Source" SortExpression="Source" UniqueName="Source">
        </telerik:GridBoundColumn>--%>
                        <telerik:GridBoundColumn DataField="InTime" FilterControlAltText="Filter InTime column" HeaderText="InTime" SortExpression="InTime" UniqueName="InTime">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="OutTime" FilterControlAltText="Filter OutTime column" HeaderText="OutTime" SortExpression="OutTime" UniqueName="OutTime">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Attendent" ItemStyle-Width="240px"  GroupByExpression ="AttendedBy Attendent Group By AttendedBy">
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
                        <%--<telerik:GridBoundColumn DataField="Assisted" DataType="System.Int64" FilterControlAltText="Filter Assisted column" HeaderText="Assisted" SortExpression="Assisted" UniqueName="Assisted">
                    </telerik:GridBoundColumn>--%>

                        <telerik:GridTemplateColumn HeaderText="Assisted" ItemStyle-Width="240px" GroupByExpression ="AssistedBy Assisted Group By AssistedBy">
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

                        <telerik:GridTemplateColumn HeaderText="Backup Support" ItemStyle-Width="240px" GroupByExpression ="BackupSBy BackupSupport Group By BackupSBy" >
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

                        <telerik:GridTemplateColumn HeaderText="Additional Support" ItemStyle-Width="240px" GroupByExpression ="AddBackupBy AdditionalSupp Group By AddBackupBy">
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
                        <telerik:GridTemplateColumn HeaderText="Automation Explained" ItemStyle-Width="240px" GroupByExpression ="Automation_Exp Automation Group By Automation_Exp">
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
                    <EditItemStyle Font-Bold="True" HorizontalAlign="Justify" VerticalAlign="Middle" />

                </MasterTableView>

            </telerik:RadGrid>

            <asp:SqlDataSource ID="SourceDs" runat ="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand ="SELECT ENQM_ID,ENQM_NAME FROM [YANTRA_LKUP_ENQ_MODE] "  ></asp:SqlDataSource>

        <asp:SqlDataSource ID="BackUpDs" runat ="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand ="Select EMP_ID,EMP_FIRST_NAME from Yantra_Employee_Mast where STATUS !=0"  ></asp:SqlDataSource>
        <asp:SqlDataSource ID="AttendentDS" runat ="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand ="Select EMP_ID,EMP_FIRST_NAME from Yantra_Employee_Mast where STATUS !=0"  ></asp:SqlDataSource>
        <asp:SqlDataSource ID="AddBackupDs" runat ="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand ="Select EMP_ID,EMP_FIRST_NAME from Yantra_Employee_Mast where STATUS !=0"  ></asp:SqlDataSource>

        <asp:SqlDataSource ID="AssistedDs" runat ="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand ="Select EMP_ID,EMP_FIRST_NAME from Yantra_Employee_Mast where STATUS !=0"  ></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" DeleteCommand="DELETE FROM [WalkIn_tbl] WHERE [ID] = @ID" InsertCommand="INSERT INTO [WalkIn_tbl] ([Date], [Billing_Name], [Phone], [Email], [Address], [Requirement], [Source], [InTime], [OutTime], [Attendent], [Assisted], [Backup_Support], [Additional_Support], [Automation_Exp]) VALUES (@Date, @Billing_Name, @Phone, @Email, @Address, @Requirement, @Source, @InTime, @OutTime, @Attendent, @Assisted, @Backup_Support, @Additional_Support, @Automation_Exp)"
                SelectCommand="select walkin_tbl.*,ENQM_NAME,Attendent.EMP_FIRST_NAME +' '+Attendent.EMP_LAST_NAME as AttendedBy
,Assisted.EMP_FIRST_NAME +' '+Assisted.EMP_LAST_NAME as AssistedBy
,BackupS.EMP_FIRST_NAME +' '+BackupS.EMP_LAST_NAME as BackupSBy
,AddBackup.EMP_FIRST_NAME +' '+AddBackup.EMP_LAST_NAME as AddBackupBy from WalkIn_tbl
left outer join YANTRA_LKUP_ENQ_MODE on walkin_tbl.source=YANTRA_LKUP_ENQ_MODE.ENQM_ID 
left outer join YANTRA_EMPLOYEE_MAST Attendent on walkin_tbl.Attendent=Attendent.EMP_ID 
left outer join YANTRA_EMPLOYEE_MAST Assisted on walkin_tbl.Assisted=Assisted .EMP_ID 
left outer join YANTRA_EMPLOYEE_MAST BackupS on walkin_tbl.Backup_Support=BackupS .EMP_ID 
left outer join YANTRA_EMPLOYEE_MAST AddBackup on walkin_tbl.Additional_Support=AddBackup.emp_id where Attendent.Status!=0"
                UpdateCommand="UPDATE [WalkIn_tbl] SET [Date] = @Date, [Billing_Name] = @Billing_Name, [Phone] = @Phone, [Email] = @Email, [Address] = @Address, [Requirement] = @Requirement, [Source] = @Source, [InTime] = @InTime, [OutTime] = @OutTime, [Attendent] = @Attendent, [Assisted] = @Assisted, [Backup_Support] = @Backup_Support, [Additional_Support] = @Additional_Support, [Automation_Exp] = @Automation_Exp WHERE [ID] = @ID">
                <DeleteParameters>
                    <asp:Parameter Name="original_ID" Type="Int64" />
                    <asp:Parameter Name="original_Date" Type="DateTime" />
                    <asp:Parameter Name="original_Billing_Name" Type="String" />
                    <asp:Parameter Name="original_Phone" Type="String" />
                    <asp:Parameter Name="original_Email" Type="String" />
                    <asp:Parameter Name="original_Address" Type="String" />
                    <asp:Parameter Name="original_Requirement" Type="String" />
                    <asp:Parameter Name="original_Source" Type="Int64" />
                    <asp:Parameter Name="original_InTime" Type="String" />
                    <asp:Parameter Name="original_OutTime" Type="String" />
                    <asp:Parameter Name="original_Attendent" Type="Int64" />
                    <asp:Parameter Name="original_Assisted" Type="Int64" />
                    <asp:Parameter Name="original_Backup_Support" Type="Int64" />
                    <asp:Parameter Name="original_Additional_Support" Type="Int64" />
                    <asp:Parameter Name="original_Automation_Exp" Type="String" />
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
                    <asp:Parameter Name="original_ID" Type="Int64" />
                    <asp:Parameter Name="original_Date" Type="DateTime" />
                    <asp:Parameter Name="original_Billing_Name" Type="String" />
                    <asp:Parameter Name="original_Phone" Type="String" />
                    <asp:Parameter Name="original_Email" Type="String" />
                    <asp:Parameter Name="original_Address" Type="String" />
                    <asp:Parameter Name="original_Requirement" Type="String" />
                    <asp:Parameter Name="original_Source" Type="Int64" />
                    <asp:Parameter Name="original_InTime" Type="String" />
                    <asp:Parameter Name="original_OutTime" Type="String" />
                    <asp:Parameter Name="original_Attendent" Type="Int64" />
                    <asp:Parameter Name="original_Assisted" Type="Int64" />
                    <asp:Parameter Name="original_Backup_Support" Type="Int64" />
                    <asp:Parameter Name="original_Additional_Support" Type="Int64" />
                    <asp:Parameter Name="original_Automation_Exp" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>


</asp:Content>
