<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Add_User1.aspx.cs" Inherits="Modules_Masters_Add_User1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
      <script type="text/javascript">
          $(document).ready(function () {
              $(document).on("click", ".table_border td", function () {
                  $(this).parent().first("a").trigger('click');
              });
          });

          function MSCAll(btn, n) {
              var ss;
          }
          function TABLE4_onclick() {

          }

    </script>
    <table class="pagehead">
        <tr>
            <td style="text-align: left">Adding User Details</td>
            <td style="text-align: right">
                 <asp:DropDownList ID="ddlNoOfRecords" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged">
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>75</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>                   
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td class="searchhead" style="text-align: left;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="TABLE4" language="javascript" onclick="return TABLE4_onclick()">
                    <tr>
                        <td style="text-align: left;">User Details</td>
                        <td></td>
                        <td style="text-align: right;">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td style="color: #ffffff">
                                        <asp:DropDownList ID="ddlCurrentDayTaskSearchBy" runat="server" CssClass="textbox" OnSelectedIndexChanged="ddlCurrentDayTaskSearchBy_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem>-----</asp:ListItem>
                                            <asp:ListItem>User Name</asp:ListItem>
                                            <asp:ListItem>Designation</asp:ListItem>
                                            <asp:ListItem>Company Name</asp:ListItem>
                                            <asp:ListItem>Department</asp:ListItem>
                                            <asp:ListItem>Assign Date</asp:ListItem>
                                            <asp:ListItem>Expiry Date</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td>
                                        <asp:DropDownList ID="ddlCompanySearch" runat="server" Width="154px"
                                            AutoPostBack="True" Visible="False">
                                        </asp:DropDownList></td>
                                    <td>
                                        <asp:TextBox ID="txtDateAssign" runat="server" CssClass="textbox" OnTextChanged="txtSearchText_TextChanged" Visible="False">
                                        </asp:TextBox><asp:Image ID="imgAssignFrom" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False"></asp:Image>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSearchText" runat="server" CssClass="textbox" OnTextChanged="txtSearchText_TextChanged"></asp:TextBox><asp:Image ID="imgAssignTo" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False"></asp:Image>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCurrentDayTasksGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnCurrentDayTasksGo_Click" /></td>
                                </tr>
                            </table>
                            <asp:Label ID="lblSearchValueHidden1" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="text-align: center;">
                <asp:GridView ID="gvAddUserDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    meta:resourcekey="gvItemTypeDetailsResource1"
                    OnRowDataBound="gvAddUserDetails_RowDataBound" DataSourceID="sdsUserDetailsFill" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="USER_ID" HeaderText="Sl.No">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="USER_NAME" SortExpression="USER_NAME" HeaderText="UserNameHidden" meta:resourceKey="BoundFieldResource1"></asp:BoundField>
                        <asp:TemplateField HeaderText="User Name" meta:resourceKey="TemplateFieldResource1">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" meta:resourcekey="TextBox1Resource1" Text='<%# Bind("It_type") %>'></asp:TextBox>

                            </EditItemTemplate> 

                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnUserName" OnClick="lbtnUserName_Click" ForeColor="#0066ff"  runat="server" Text='<%# Eval("USER_NAME") %>' CausesValidation="False" meta:resourcekey="lbtnItemTypeNameResource1" __designer:wfdid="w10"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DESG_NAME" HeaderText="Designation">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CP_FULL_NAME" HeaderText="Company Name">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="DEPT_NAME" HeaderText="Department">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField SortExpression="PRIVILEGE_NAME" HeaderText="Privelege" meta:resourceKey="BoundFieldResource2">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="ASSIGN_DATE" SortExpression="ASSIGN_DATE" HeaderText="Assign Date" meta:resourceKey="BoundFieldResource3">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="EXPIRY_DATE" SortExpression="EXPIRY_DATE" HeaderText="Expiry Date">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="User_Id" HeaderText="UserIdHidden">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="USER_TYPE" HeaderText="User Type"></asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        No Record Found
                    
                    </EmptyDataTemplate>
                    <PagerStyle CssClass="gpager" />
                    <SelectedRowStyle BackColor="LightSteelBlue" />
                </asp:GridView>
                <asp:SqlDataSource ID="sdsUserDetailsFill" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_YANTRA_ADDUSER_SEARCH_SELECT" SelectCommandType="StoredProcedure" OnSelecting="sdsUserDetailsFill_Selecting">
                    <SelectParameters>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue1" ControlID="lblSearchValueHidden1"></asp:ControlParameter>
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td style="text-align: center;">
                <table id="Table1">
                    <tr>
                        <td>
                            <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CausesValidation="False" /></td>
                        <td>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CausesValidation="False" /></td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" CausesValidation="False" /></td>
                        <td style="width: 3px"></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="text-align: center;">
                <table id="tblUserDetails" runat="server" border="0" cellpadding="0" cellspacing="0"
                    style="width: 100%" visible="false">
                    <tr>
                        <td id="tblItDetails" runat="server" colspan="2" style="text-align: right;">
                            <table id="Table2" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 100%"
                                visible="true">
                                <tr>
                                    <td class="profilehead" colspan="4" style="text-align: left">
                                    General Details</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        User Name</td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlUserName" runat="server" Width="154px" OnSelectedIndexChanged="ddlUserName_SelectedIndexChanged"
                                            AutoPostBack="True">
                                        </asp:DropDownList><asp:Label ID="Label35" runat="server" EnableTheming="False" ForeColor="Red"
                                            Text="*">
                                        </asp:Label><asp:RequiredFieldValidator ID="rfvddlUserName" runat="server" ControlToValidate="ddlUserName"
                                            ErrorMessage="Please Select the User Name" InitialValue="0" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                    <td style="text-align: right">
                                        Company Name</td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtCompanyName" runat="server" ReadOnly="True" OnTextChanged="txtUserId_TextChanged"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; height: 24px;">User Id / Email ID</td>
                                    <td style="text-align: left; height: 24px;">
                                        <asp:TextBox ID="txtUserId" runat="server" ReadOnly="True" OnTextChanged="txtUserId_TextChanged"></asp:TextBox></td>
                                    <td style="text-align: right; height: 24px;">
                                        Department</td>
                                    <td style="text-align: left; height: 24px;">
                                        <asp:TextBox ID="txtDepartment" runat="server" ReadOnly="True" OnTextChanged="txtUserId_TextChanged"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        Assign Date</td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtAssignDate" runat="server" OnTextChanged="txtAssignDate_TextChanged"></asp:TextBox><asp:Image ID="imgAssignDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><asp:Label
                                            ID="Label2" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                                        </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="txtAssignDate" ErrorMessage="Please Select the Assign Date"
                                            SetFocusOnError="True">*</asp:RequiredFieldValidator><asp:CustomValidator ID="CustomValidator8"
                                                runat="server" ClientValidationFunction="DateCustomValidate" ControlToValidate="txtAssignDate"
                                                ErrorMessage="Please Enter the Assign Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                                SetFocusOnError="True">*</asp:CustomValidator><cc1:CalendarExtender
                                                    Format="dd/MM/yyyy" ID="ceAssignDate" runat="server" Enabled="True" PopupButtonID="imgAssignDate"
                                                    TargetControlID="txtAssignDate">
                                                </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditAssignDate" runat="server" DisplayMoney="Left"
                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtAssignDate"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td style="text-align: right">
                                        Designation</td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtDesignation" runat="server" ReadOnly="True" OnTextChanged="txtUserId_TextChanged"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        Expiry Date</td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtExpiryDate" runat="server"></asp:TextBox><asp:Image ID="imgExpiryDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><asp:Label
                                            ID="Label3" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                                        </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="txtExpiryDate" ErrorMessage="Please Select the Expiry Date"
                                            SetFocusOnError="True">*</asp:RequiredFieldValidator><asp:CustomValidator ID="CustomValidator1"
                                                runat="server" ClientValidationFunction="DateCustomValidate" ControlToValidate="txtExpiryDate"
                                                ErrorMessage="Please Enter the Expiry Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                                SetFocusOnError="True">*</asp:CustomValidator><cc1:CalendarExtender
                                                    Format="dd/MM/yyyy" ID="ceExpiryDATE" runat="server" Enabled="True" PopupButtonID="imgExpiryDate"
                                                    TargetControlID="txtExpiryDate">
                                                </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditExpiryDate" runat="server" DisplayMoney="Left"
                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtExpiryDate"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td style="text-align: right">
                                        User Type</td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlUsertype" runat="server" Width="154px" OnSelectedIndexChanged="ddlUserName_SelectedIndexChanged">
                                            <asp:ListItem>--Select--</asp:ListItem>
                                            <asp:ListItem Value="0">Admin</asp:ListItem>
                                            <asp:ListItem Value="1">User</asp:ListItem>
                                        </asp:DropDownList><asp:Label ID="Label12" runat="server" EnableTheming="False" ForeColor="Red"
                                            Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlUsertype"
                                                ErrorMessage="Please Select the User type" InitialValue="--Select--" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; visibility:hidden;">
                                        Privilege</td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlPriveleges" runat="server" OnSelectedIndexChanged="ddlPriveleges_SelectedIndexChanged" Visible="False">
                                            <asp:ListItem>Sales Cordinator</asp:ListItem>
                                            <asp:ListItem>Sales Person</asp:ListItem>
                                            <asp:ListItem>Sales Manager</asp:ListItem>
                                            <asp:ListItem>M.D</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: right">
                                        &nbsp;</td>
                                    <td style="text-align: left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: right"></td>
                                </tr>
                                <tr>
                                    <td class="profilehead" colspan="4">Privileges</td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: left">
                                        <cc1:TabContainer ID="TabCon" runat="server" ActiveTabIndex="0" BackColor="#E7F4F6"
                                            CssClass="visoft__tab_xpie7" Font-Bold="False" Height="150px" Width="100%">
                                            <cc1:TabPanel ID="TabPanel1" runat="server" BackColor="#E7F4F6" HeaderText="TabPanel1"
                                                Width="100%">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnMasterSAll" runat="server" CausesValidation="False" EnableTheming="False"
                                                        Font-Names="Verdana" Font-Size="8pt" OnClick="btnMasterSAll_Click" Text="Select All"
                                                        UseSubmitBehavior="False" />
                                                    <asp:Button ID="btnMasterCAll" runat="server" CausesValidation="False" EnableTheming="False"
                                                        Font-Names="Verdana" Font-Size="8pt" OnClick="btnMasterCAll_Click" Text="Clear All"
                                                        UseSubmitBehavior="False" />
                                                    <asp:CheckBoxList ID="chklMaster" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                                        Width="100%">
                                                        <asp:ListItem Value="Master_1">Users</asp:ListItem>
                                                        <asp:ListItem Value="Master_2">Masters</asp:ListItem>
                                                        <asp:ListItem Value="Master_3">Item Master</asp:ListItem>
                                                        <asp:ListItem Value="Master_4">Product Master</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </ContentTemplate>
                                                <HeaderTemplate>
                                                    Master
                                                </HeaderTemplate>
                                            </cc1:TabPanel>
                                            <cc1:TabPanel ID="TabPanel2" runat="server" BackColor="#E7F4F6" HeaderText="TabPanel2"
                                                Width="100%">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnSMSAll" runat="server" CausesValidation="False" EnableTheming="False"
                                                        Font-Names="Verdana" Font-Size="8pt" OnClick="btnSMSAll_Click" Text="Select All"
                                                        UseSubmitBehavior="False" />
                                                    <asp:Button ID="btnSMCAll" runat="server" CausesValidation="False" EnableTheming="False"
                                                        Font-Names="Verdana" Font-Size="8pt" OnClick="btnSMCAll_Click" Text="Clear All"
                                                        UseSubmitBehavior="False" />
                                                    <asp:CheckBoxList ID="chklSM" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                                        Width="100%">
                                                        <asp:ListItem Value="SM_1">Customer Info</asp:ListItem>
                                                        <asp:ListItem Value="SM_2">Sales Lead</asp:ListItem>
                                                        <asp:ListItem Value="SM_3">Sales Assignments</asp:ListItem>
                                                        <asp:ListItem Value="SM_4">Sales Quotation</asp:ListItem>
                                                        <asp:ListItem Value="SM_5">Purchase Order</asp:ListItem>
                                                        <asp:ListItem Value="SM_6">Internal Order</asp:ListItem>
                                                        <asp:ListItem Value="SM_7">Process Status</asp:ListItem>
                                                        <asp:ListItem Value="SM_8">Payments Received</asp:ListItem>
                                                        <asp:ListItem Value="SM_9">Daily Report</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </ContentTemplate>
                                                <HeaderTemplate>
                                                    Sales &amp; Marketing
                                                </HeaderTemplate>
                                            </cc1:TabPanel>
                                            <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3" Width="100%">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnSCMSAll" runat="server" CausesValidation="False" EnableTheming="False"
                                                        Font-Names="Verdana" Font-Size="8pt" OnClick="btnSCMSAll_Click" Text="Select All"
                                                        UseSubmitBehavior="False" />
                                                    <asp:Button ID="btnSCMCAll" runat="server" CausesValidation="False" EnableTheming="False"
                                                        Font-Names="Verdana" Font-Size="8pt" OnClick="btnSCMCAll_Click" Text="Clear All"
                                                        UseSubmitBehavior="False" />
                                                    <asp:CheckBoxList ID="chklSCM" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                                        Width="100%">
                                                        <asp:ListItem Value="SCM_1">Supplier Master</asp:ListItem>
                                                        <asp:ListItem Value="SCM_2">Suppliers Enquiry</asp:ListItem>
                                                        <asp:ListItem Value="SCM_3">Proforma Invoice</asp:ListItem>
                                                        <asp:ListItem Value="SCM_4">Purchase Order</asp:ListItem>
                                                        <asp:ListItem Value="SCM_6">Shipment Details</asp:ListItem>
                                                        <asp:ListItem Value="SCM_5">Purchase Invoice</asp:ListItem>
                                                        <asp:ListItem Value="SCM_7">Insurance Claim Form</asp:ListItem>
                                                        <asp:ListItem Value="SCM_8">Item History</asp:ListItem>
                                                        <asp:ListItem Value="SCM_9">Purchase Order Search</asp:ListItem>
                                                        <asp:ListItem Value="SCM_10">Indent History</asp:ListItem>
                                                        <asp:ListItem Value="SCM_11">Indent Records</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </ContentTemplate>


                                                <HeaderTemplate>
                                                    Purchases
                                                </HeaderTemplate>
                                            </cc1:TabPanel>
                                            <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="TabPanel4" Width="100%">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnInventorySAll" runat="server" CausesValidation="False" EnableTheming="False"
                                                        Font-Names="Verdana" Font-Size="8pt" OnClick="btnInventorySAll_Click" Text="Select All"
                                                        UseSubmitBehavior="False" />
                                                    <asp:Button ID="btnInventoryCAll" runat="server" CausesValidation="False" EnableTheming="False"
                                                        Font-Names="Verdana" Font-Size="8pt" OnClick="btnInventoryCAll_Click" Text="Clear All"
                                                        UseSubmitBehavior="False" />
                                                    <asp:CheckBoxList ID="chklInventorys" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                                        Width="100%">
                                                        <asp:ListItem Value="Inventory_5">Delivery Challan</asp:ListItem>
                                                        <asp:ListItem Value="Inventory_2">MRN</asp:ListItem>
                                                        <asp:ListItem Value="Inventory_1">Indent</asp:ListItem>
                                                        <asp:ListItem Value="Inventory_6">Indent Order Approval</asp:ListItem>
                                                        <asp:ListItem Value="Inventory_4">Stock Entry</asp:ListItem>
                                                        <asp:ListItem Value="Inventory_3">Stock Statement</asp:ListItem>
                                                        <asp:ListItem Value="Inventory_7">Reserve Stock History</asp:ListItem>
                                                        <asp:ListItem Value="Inventory_8">Sample &amp; Cash</asp:ListItem>
                                                        <asp:ListItem Value="Inventory_9">UnbilledDc</asp:ListItem>
                                                        <asp:ListItem Value="Inventory_10">Dispatch</asp:ListItem>
                                                    </asp:CheckBoxList>

                                                </ContentTemplate>













                                                <HeaderTemplate>
                                                    Inventory
                                                </HeaderTemplate>
                                            </cc1:TabPanel>
                                            <cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="TabPanel5" Width="100%">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnSupSAll" runat="server" CausesValidation="False" EnableTheming="False"
                                                        Font-Names="Verdana" Font-Size="8pt" OnClick="btnSupSAll_Click" Text="Select All"
                                                        UseSubmitBehavior="False" />
                                                    <asp:Button ID="btnSupCAll" runat="server" CausesValidation="False" EnableTheming="False"
                                                        Font-Names="Verdana" Font-Size="8pt" OnClick="btnSupCAll_Click" Text="Clear All"
                                                        UseSubmitBehavior="False" />
                                                    <asp:CheckBoxList ID="chklServices" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                                        Width="100%">
                                                        <asp:ListItem Value="Support_12">Courier Master</asp:ListItem>
                                                        <asp:ListItem Value="Support_13">Service Customer Information</asp:ListItem>
                                                        <asp:ListItem Value="Support_1">Complaint Register</asp:ListItem>
                                                        <asp:ListItem Value="Support_3">Service Report</asp:ListItem>
                                                        <asp:ListItem Value="Support_2">Site Inspection Report</asp:ListItem>
                                                        <asp:ListItem Value="Support_4">Spare Parts Indent</asp:ListItem>
                                                        <asp:ListItem Value="Support_5">AMC Quotation</asp:ListItem>
                                                        <asp:ListItem Value="Support_6">AMC Order</asp:ListItem>
                                                        <asp:ListItem Value="Support_7">AMC Order Profile</asp:ListItem>
                                                        <asp:ListItem Value="Support_8">AMC Order Acceptance</asp:ListItem>
                                                        <asp:ListItem Value="Support_9">Warranty Claim</asp:ListItem>
                                                        <asp:ListItem Value="Support_10">AMC Invoice</asp:ListItem>
                                                        <asp:ListItem Value="Support_11">AMC Payments Received</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </ContentTemplate>
                                                <HeaderTemplate>
                                                    Services
                                                </HeaderTemplate>
                                            </cc1:TabPanel>


                                            <cc1:TabPanel ID="TabPanel8" runat="server" HeaderText="TabPanel8" Width="100%">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnFinanceSAll" runat="server" CausesValidation="False" EnableTheming="False"
                                                        Font-Names="Verdana" Font-Size="8pt" OnClick="btnFinanceSAll_Click" Text="Select All"
                                                        UseSubmitBehavior="False" />
                                                    <asp:Button ID="btnFinanceCAll" runat="server" CausesValidation="False" EnableTheming="False"
                                                        Font-Names="Verdana" Font-Size="8pt" OnClick="btnFinanceCAll_Click" Text="Clear All"
                                                        UseSubmitBehavior="False" />
                                                    <asp:CheckBoxList ID="chklFinance" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                                        Width="100%">
                                                        <asp:ListItem Value="Finance_1">Sales Invoice</asp:ListItem>
                                                        <asp:ListItem Value="Finance_2">Sales Return</asp:ListItem>
                                                        <asp:ListItem Value="Finance_3">Payments Received From Sales</asp:ListItem>
                                                        <asp:ListItem Value="Finance_4">Statement Of Account</asp:ListItem>
                                                        <asp:ListItem Value="Finance_5">Sample &amp; Cash Invoice</asp:ListItem>
                                                        <asp:ListItem Value="Finance_6">Sample Return</asp:ListItem>
                                                        <asp:ListItem Value="Finance_7">Vouchers</asp:ListItem>

                                                    </asp:CheckBoxList>

                                                </ContentTemplate>
                                                <HeaderTemplate>
                                                    Finanace
                                                </HeaderTemplate>
                                            </cc1:TabPanel>









                                            <cc1:TabPanel ID="TabPanel6" runat="server" HeaderText="TabPanel6" Width="100%">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnHRSAll" runat="server" CausesValidation="False" EnableTheming="False"
                                                        Font-Names="Verdana" Font-Size="8pt" OnClick="btnHRSAll_Click" Text="Select All"
                                                        UseSubmitBehavior="False" />
                                                    <asp:Button ID="btnHRCAll" runat="server" CausesValidation="False" EnableTheming="False"
                                                        Font-Names="Verdana" Font-Size="8pt" OnClick="btnHRCAll_Click" Text="Clear All"
                                                        UseSubmitBehavior="False" />
                                                    <asp:CheckBoxList ID="chklHR" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                                        Width="100%">
                                                        <asp:ListItem Value="HR_1">Employee Master</asp:ListItem>
                                                        <asp:ListItem Value="HR_2">Memo</asp:ListItem>
                                                        <asp:ListItem Value="HR_3">Circular</asp:ListItem>
                                                        <asp:ListItem Value="HR_4">Offer Letter</asp:ListItem>
                                                        <asp:ListItem Value="HR_5">Salary Breakups</asp:ListItem>
                                                        <asp:ListItem Value="HR_6">Employee Enrollment</asp:ListItem>
                                                        <asp:ListItem Value="HR_7">Self Help</asp:ListItem>
                                                    </asp:CheckBoxList>

                                                </ContentTemplate>
                                                <HeaderTemplate>
                                                    HR
                                                </HeaderTemplate>
                                            </cc1:TabPanel>



                                            <cc1:TabPanel ID="TabPanel7" runat="server" HeaderText="TabPanel7">

                                                <ContentTemplate>
                                                    <asp:Button ID="btnInvSAll" runat="server" CausesValidation="False" EnableTheming="False"
                                                        Font-Names="Verdana" Font-Size="8pt" OnClick="btnInvSAll_Click" Text="Select All"
                                                        UseSubmitBehavior="False" />
                                                    <asp:Button ID="btnInvCAll" runat="server" CausesValidation="False" EnableTheming="False"
                                                        Font-Names="Verdana" Font-Size="8pt" OnClick="btnInvCAll_Click" Text="Clear All"
                                                        UseSubmitBehavior="False" />
                                                    <asp:CheckBoxList ID="chklInventory" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                                        Width="100%">
                                                        <asp:ListItem Value="Reports_1">SM EOD Reports</asp:ListItem>
                                                        <asp:ListItem Value="Reports_2">SCM EOD Reports</asp:ListItem>
                                                        <asp:ListItem Value="Reports_3">Services EOD Reports</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </ContentTemplate>
                                                <HeaderTemplate>
                                                    Reports
                                                </HeaderTemplate>
                                            </cc1:TabPanel>
                                             <cc1:TabPanel ID="TabPanel9" runat="server" HeaderText="TabPanel7">

                                                <ContentTemplate>
                                                    <asp:Button ID="btnWarSAll" runat="server" CausesValidation="False" EnableTheming="False"
                                                        Font-Names="Verdana" Font-Size="8pt" OnClick="btnWarSAll_Click" Text="Select All"
                                                        UseSubmitBehavior="False" />
                                                    <asp:Button ID="btnWarCAll" runat="server" CausesValidation="False" EnableTheming="False"
                                                        Font-Names="Verdana" Font-Size="8pt" OnClick="btnWarCAll_Click" Text="Clear All"
                                                        UseSubmitBehavior="False" />
                                                    <asp:CheckBoxList ID="chkWarehouse" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                                        Width="100%">
                                                        <asp:ListItem Value="Warehouse_1">Warehouse Stock Report</asp:ListItem>
                                                        <asp:ListItem Value="Warehouse_2">Manage Locations</asp:ListItem>
                                                        <asp:ListItem Value="Warehouse_3">Inward Stock</asp:ListItem>
                                                        <asp:ListItem Value="Warehouse_4">Spare Inward</asp:ListItem>
                                                        <asp:ListItem Value="Warehouse_5">Outward Stock</asp:ListItem>
                                                        <asp:ListItem Value="Warehouse_6">Damage Report</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </ContentTemplate>
                                                <HeaderTemplate>
                                                    Warehouse
                                                </HeaderTemplate>
                                            </cc1:TabPanel>
                                        </cc1:TabContainer><asp:HiddenField ID="hfPrivilegesList" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="2" style="text-align: left">Permissions</td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:CheckBoxList ID="chklPermissions" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                Width="100%">
                                <asp:ListItem Value="Permission_Add">Add Record</asp:ListItem>
                                <asp:ListItem Value="Permission_Edit">Edit Record</asp:ListItem>
                                <asp:ListItem Value="Permission_Approve">Approve Record</asp:ListItem>
                                <asp:ListItem Value="Permission_Regret">Regret Record</asp:ListItem>
                                <asp:ListItem Value="Permission_Delete">Delete Record</asp:ListItem>
                                <asp:ListItem Value="Permission_Print">Print Record</asp:ListItem>
                                <asp:ListItem Value="Permission_E-Mail">Send E-Mail</asp:ListItem>
                                <asp:ListItem Value="Permission_ShowAll">Show All Records</asp:ListItem>
                                <asp:ListItem Value="Permission_ShowIndividual">Show Individual Records</asp:ListItem>
                            </asp:CheckBoxList>
                            <asp:CheckBoxList ID="chklFullControl" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                Width="100%" AutoPostBack="True" OnSelectedIndexChanged="chklFullControl_SelectedIndexChanged">
                                <asp:ListItem Value="Permission_FullControl">Full Control</asp:ListItem>
                            </asp:CheckBoxList></td>
                    </tr>
                    <tr>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="2" style="text-align: left">Password</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table style="width: 512px; height: 26px">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Do you want change password ?"></asp:Label></td>
                                    <td>
                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem>Yes</asp:ListItem>
                                            <asp:ListItem Selected="True">No</asp:ListItem>
                                        </asp:RadioButtonList></td>
                                    <td style="width: 183px">
                                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Visible="False"></asp:TextBox><asp:Label
                                            ID="lblPasswordValidator" runat="server" EnableTheming="False" ForeColor="Red"
                                            Text="*"></asp:Label><asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                                                ControlToValidate="txtPassword" ErrorMessage="Please Enter Password" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <table id="Table3">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" meta:resourcekey="btnSaveResource1" Text="Save"
                                            OnClick="btnSave_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" meta:resourcekey="btnRefreshResource1"
                                            Text="Refresh" /></td>
                                    <td>
                                        <asp:Button ID="btnClose" runat="server" CausesValidation="False" meta:resourcekey="btnCloseResource1"
                                            Text="Close" OnClick="btnClose_Click" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center"></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <cc1:CalendarExtender
        Format="MM/dd/yyyy" ID="CalendarExtender1" runat="server" Enabled="True" PopupButtonID="imgAssignFrom"
        TargetControlID="txtDateAssign">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender
        Format="MM/dd/yyyy" ID="CalendarExtender2" runat="server" Enabled="True" PopupButtonID="imgAssignTo"
        TargetControlID="txtDateAssign">
    </cc1:CalendarExtender>

</asp:Content>


 
