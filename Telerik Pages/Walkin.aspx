<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Walkin.aspx.cs" Inherits="Telerik_Pages_Walkin" %>
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
    <div>
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

        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings >
                 <telerik:AjaxSetting AjaxControlID="RadGrid1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
         <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
        <telerik:RadFormDecorator RenderMode="Lightweight" ID="RadFormDecorator1" runat="server" DecorationZoneID="demo" DecoratedControls="All" EnableRoundedCorners="false"  />
    <div id="demo" class="demo-container no-bg">
        <telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="True" DataSourceID="SqlDataSource1" AllowAutomaticInserts ="true" AllowAutomaticUpdates ="true" AllowAutomaticDeletes ="true"   >
<GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource1" CommandItemDisplay="Top" AllowAutomaticInserts="true" AllowAutomaticDeletes ="true" AllowAutomaticUpdates ="true" EditMode="InPlace" >
                <Columns>
                    <telerik:GridBoundColumn DataField="ID" DataType="System.Int64" FilterControlAltText="Filter ID column" HeaderText="ID" ReadOnly="True" SortExpression="ID" UniqueName="ID">
                    </telerik:GridBoundColumn>
                    <%--<telerik:GridBoundColumn DataField="Date" DataType="System.DateTime" FilterControlAltText="Filter Date column" HeaderText="Date" SortExpression="Date" UniqueName="Date">
                    </telerik:GridBoundColumn>--%>
                     <telerik:GridTemplateColumn DataField="Date" HeaderStyle-Font-Bold="true" ItemStyle-Font-Bold="true" DataType="System.DateTime"  HeaderText="Date" SortExpression="Date" UniqueName="Date">
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
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="Billing_Name" FilterControlAltText="Filter Billing_Name column" HeaderText="Billing_Name" SortExpression="Billing_Name" UniqueName="Billing_Name">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Phone" FilterControlAltText="Filter Phone column" HeaderText="Phone" SortExpression="Phone" UniqueName="Phone">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Email" FilterControlAltText="Filter Email column" HeaderText="Email" SortExpression="Email" UniqueName="Email">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Address" FilterControlAltText="Filter Address column" HeaderText="Address" SortExpression="Address" UniqueName="Address">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Requirement" FilterControlAltText="Filter Requirement column" HeaderText="Requirement" SortExpression="Requirement" UniqueName="Requirement">
                    </telerik:GridBoundColumn>
                    <%--<telerik:GridBoundColumn DataField="Source" FilterControlAltText="Filter Source column" HeaderText="Source" SortExpression="Source" UniqueName="Source">
                    </telerik:GridBoundColumn>--%>
                    <telerik:GridTemplateColumn HeaderText="Source"  FilterControlAltText="Filter Source column" ColumnGroupName="Source" FilterControlWidth="60px" ItemStyle-Width="240px" >
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Source")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:RadDropDownList RenderMode="Lightweight" runat="server" ID="ddlSOurce"  DataTextField="ENQM_NAME"
                                DataValueField="ENQM_ID" DataSourceID="SourceDs" SelectedValue='<%#Bind("Source") %>'>
                               <%-- <Items>
                                    <telerik:DropDownListItem Text="--" Value="Not Selected" />
                                </Items> --%>
                            </telerik:RadDropDownList>
                        </EditItemTemplate>
                        <ItemStyle Width="240px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="InTime" FilterControlAltText="Filter InTime column" HeaderText="InTime" SortExpression="InTime" UniqueName="InTime">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="OutTime" FilterControlAltText="Filter OutTime column" HeaderText="OutTime" SortExpression="OutTime" UniqueName="OutTime">
                    </telerik:GridBoundColumn>
                    <%--<telerik:GridBoundColumn DataField="Attendent" DataType="System.Int64" FilterControlAltText="Filter Attendent column" HeaderText="Attendent" SortExpression="Attendent" UniqueName="Attendent">
                    </telerik:GridBoundColumn>--%>
                    <telerik:GridTemplateColumn HeaderText="Attendent" ItemStyle-Width="240px" >
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Attendent")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:RadDropDownList RenderMode="Lightweight" runat="server" ID="ddlAttendent"  DataTextField="Emp_First_Name"
                                DataValueField="Emp_ID" DataSourceID="AttendentDS" SelectedValue='<%#Bind("Attendent") %>'>
                            </telerik:RadDropDownList>
                        </EditItemTemplate>
                        <ItemStyle Width="240px" />
                    </telerik:GridTemplateColumn>
                    <%--<telerik:GridBoundColumn DataField="Assisted" DataType="System.Int64" FilterControlAltText="Filter Assisted column" HeaderText="Assisted" SortExpression="Assisted" UniqueName="Assisted">
                    </telerik:GridBoundColumn>--%>

                      <telerik:GridTemplateColumn HeaderText="Assisted" ItemStyle-Width="240px" >
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Assisted")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:RadDropDownList RenderMode="Lightweight" runat="server" ID="ddlAssisted"  DataTextField="Emp_First_Name"
                                DataValueField="Emp_ID" DataSourceID="AssistedDs" SelectedValue='<%#Bind("Assisted") %>'>
                            </telerik:RadDropDownList>
                        </EditItemTemplate>
                        <ItemStyle Width="240px" />
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn HeaderText="Backup Support" ItemStyle-Width="240px" >
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Backup_Support")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:RadDropDownList RenderMode="Lightweight" runat="server" ID="ddlBackup"  DataTextField="Emp_First_Name"
                                DataValueField="Emp_ID" DataSourceID="AssistedDs" SelectedValue='<%#Bind("Backup_Support") %>'>
                            </telerik:RadDropDownList>
                        </EditItemTemplate>
                        <ItemStyle Width="240px" />
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn HeaderText="Additional Support" ItemStyle-Width="240px" >
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Additional_Support")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:RadDropDownList RenderMode="Lightweight" runat="server" ID="ddlAddBackup"  DataTextField="Emp_First_Name"
                                DataValueField="Emp_ID" DataSourceID="AddBackupDs" SelectedValue='<%#Bind("Additional_Support") %>'>
                            </telerik:RadDropDownList>
                        </EditItemTemplate>
                        <ItemStyle Width="240px" />
                    </telerik:GridTemplateColumn>
                    <%--<telerik:GridBoundColumn DataField="Backup_Support" DataType="System.Int64" FilterControlAltText="Filter Backup_Support column" HeaderText="Backup_Support" SortExpression="Backup_Support" UniqueName="Backup_Support">
                    </telerik:GridBoundColumn>--%>
                    <%--<telerik:GridBoundColumn DataField="Additional_Support" DataType="System.Int64" FilterControlAltText="Filter Additional_Support column" HeaderText="Additional_Support" SortExpression="Additional_Support" UniqueName="Additional_Support">
                    </telerik:GridBoundColumn>--%>
                    <%--<telerik:GridBoundColumn DataField="Automation_Exp" FilterControlAltText="Filter Automation_Exp column" HeaderText="Automation_Exp" SortExpression="Automation_Exp" UniqueName="Automation_Exp">
                    </telerik:GridBoundColumn>--%>
                    <telerik:GridTemplateColumn HeaderText="Additional Support" ItemStyle-Width="240px" >
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Automation_Exp")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:RadDropDownList RenderMode="Lightweight" runat="server" ID="ddlAutomation"  DataTextField="Automation_Exp"
                                DataValueField="Automation_Exp"  SelectedValue='<%#Bind("Automation_Exp") %>'>
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
                  <EditFormSettings>
                    <EditColumn FilterControlAltText="Filter EditCommandColumn1 column" UniqueName="EditCommandColumn1">
                    </EditColumn>
                </EditFormSettings>
            </MasterTableView>
        </telerik:RadGrid>
        <asp:SqlDataSource ID="SourceDs" runat ="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand ="SELECT ENQM_ID,ENQM_NAME FROM [YANTRA_LKUP_ENQ_MODE] "  ></asp:SqlDataSource>

        <asp:SqlDataSource ID="BackUpDs" runat ="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand ="Select EMP_ID,EMP_FIRST_NAME from Yantra_Employee_Mast where STATUS !=0"  ></asp:SqlDataSource>
        <asp:SqlDataSource ID="AttendentDS" runat ="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand ="Select EMP_ID,EMP_FIRST_NAME from Yantra_Employee_Mast where STATUS !=0"  ></asp:SqlDataSource>
        <asp:SqlDataSource ID="AddBackupDs" runat ="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand ="Select EMP_ID,EMP_FIRST_NAME from Yantra_Employee_Mast where STATUS !=0"  ></asp:SqlDataSource>

        <asp:SqlDataSource ID="AssistedDs" runat ="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand ="Select EMP_ID,EMP_FIRST_NAME from Yantra_Employee_Mast where STATUS !=0"  ></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" DeleteCommand="DELETE FROM [WalkIn_tbl] WHERE [ID] = @ID" InsertCommand="INSERT INTO [WalkIn_tbl] ([Date], [Billing_Name], [Phone], [Email], [Address], [Requirement], [Source], [InTime], [OutTime], [Attendent], [Assisted], [Backup_Support], [Additional_Support], [Automation_Exp]) VALUES (@Date, @Billing_Name, @Phone, @Email, @Address, @Requirement, @Source, @InTime, @OutTime, @Attendent, @Assisted, @Backup_Support, @Additional_Support, @Automation_Exp)" SelectCommand="SELECT * FROM [WalkIn_tbl]" UpdateCommand="UPDATE [WalkIn_tbl] SET [Date] = @Date, [Billing_Name] = @Billing_Name, [Phone] = @Phone, [Email] = @Email, [Address] = @Address, [Requirement] = @Requirement, [Source] = @Source, [InTime] = @InTime, [OutTime] = @OutTime, [Attendent] = @Attendent, [Assisted] = @Assisted, [Backup_Support] = @Backup_Support, [Additional_Support] = @Additional_Support, [Automation_Exp] = @Automation_Exp WHERE [ID] = @ID">
            <DeleteParameters>
                <asp:Parameter Name="ID" Type="Int64" />
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
                <asp:Parameter Name="ID" Type="Int64" />
            </UpdateParameters>
        </asp:SqlDataSource>
    </div>
        
    </div>
    </form>

</body>
</html>
