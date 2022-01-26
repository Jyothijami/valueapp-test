<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/AdminMP1.master" AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="Modules_Masters_User" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">        
    <table class="pagehead">
        <tr>
            <td style="text-align:left">Adding User Details</td>
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
                        <asp:BoundField DataField="USER_NAME" SortExpression="USER_NAME" HeaderText="UserNameHidden"  meta:resourceKey="BoundFieldResource1"></asp:BoundField>
                        <asp:TemplateField HeaderText="User Name" meta:resourceKey="TemplateFieldResource1">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" meta:resourcekey="TextBox1Resource1" Text='<%# Bind("It_type") %>'></asp:TextBox>

                            </EditItemTemplate>

                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnUserName" OnClick="lbtnUserName_Click" ForeColor="#0066ff" runat="server" Text='<%# Eval("USER_NAME") %>' CausesValidation="False" meta:resourcekey="lbtnItemTypeNameResource1" __designer:wfdid="w10"></asp:LinkButton>
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
                        <asp:BoundField DataField="Emp_id" HeaderText="EMPId" />
                    </Columns>
                    <EmptyDataTemplate>
                        No Record Found
                    
                    </EmptyDataTemplate>
                    <SelectedRowStyle BackColor="LightSteelBlue" />
                </asp:GridView>
                <asp:SqlDataSource ID="sdsUserDetailsFill" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_YANTRA_ADDUSER_SEARCH_SELECT" SelectCommandType="StoredProcedure" >
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
                <table id="Table1" align="center">
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
                        <td id="tblItDetails" runat="server" style="text-align: right;">
                            <table id="Table2" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%" visible="true">
                                <tr>
                                    <td class="profilehead" colspan="4" style="text-align: left">
                                    General Details
                                        </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblEmployeeName" runat="server" Text="User Name :"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlUserName" runat="server" Width="154px" OnSelectedIndexChanged="ddlUserName_SelectedIndexChanged"
                                            AutoPostBack="True">
                                        </asp:DropDownList><asp:Label ID="Label35" runat="server" EnableTheming="False" ForeColor="Red"
                                            Text="*">
                                        </asp:Label>
                                        <%--<asp:RequiredFieldValidator ID="rfvddlUserName" runat="server" ControlToValidate="ddlUserName"
                                            ErrorMessage="Please Select the User Name" InitialValue="0" SetFocusOnError="True">*</asp:RequiredFieldValidator>--%>

                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label4" runat="server" Text="Company Name :" Width="113px"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtCompanyName" runat="server" ReadOnly="True"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; height: 24px;">&nbsp; &nbsp;&nbsp;<asp:Label ID="Label5" runat="server" Text="User ID/E-Mail ID :" Width="122px"></asp:Label>
                                    </td>
                                    <td style="text-align: left; height: 24px;">
                                        <asp:TextBox ID="txtUserId" runat="server" ReadOnly="True"></asp:TextBox></td>
                                    <td style="text-align: right; height: 24px;">
                                        <asp:Label ID="Label7" runat="server" Text="Department :"></asp:Label></td>
                                    <td style="text-align: left; height: 24px;">
                                        <asp:TextBox ID="txtDepartment" runat="server" ReadOnly="True"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label6" runat="server" Text="Privilege" Visible="False"></asp:Label>
                                        <asp:Label ID="Label10" runat="server" Text="Assign Date :"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtAssignDate" runat="server"></asp:TextBox><asp:Image ID="imgAssignDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><asp:Label
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
                                     
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label8" runat="server" Text="Designation :"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtDesignation" runat="server" ReadOnly="True"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label9" runat="server" Text="Expiry Date :" Width="78px"></asp:Label></td>
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
                              
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label11" runat="server" Text="Type :"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlUsertype" runat="server" Width="154px">
                                            <asp:ListItem>--Select--</asp:ListItem>
                                            <asp:ListItem Value="0">Admin</asp:ListItem>
                                            <asp:ListItem Value="1">User</asp:ListItem>
                                        </asp:DropDownList><asp:Label ID="Label12" runat="server" EnableTheming="False" ForeColor="Red"
                                            Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlUsertype"
                                                ErrorMessage="Please Select the User Name" InitialValue="--Select--" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label36" runat="server" Text="Password :" Width="78px"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                                        <asp:Label ID="Label1" runat="server" EnableTheming="False" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label37" runat="server" Text="User Type :"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlUsersType" runat="server">
                                        </asp:DropDownList>
                                        <asp:Label ID="Label13" runat="server" EnableTheming="False" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        &nbsp;</td>
                                    <td style="text-align: left">
                                        &nbsp;</td>
                                    <td style="text-align: right">
                                        &nbsp;</td>
                                    <td style="text-align: left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: left" class="auto-style1">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <table id="Table3" align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" meta:resourcekey="btnSaveResource1" Text="Save"
                                            OnClick="btnSave_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" meta:resourcekey="btnRefreshResource1"
                                            Text="Refresh" OnClick="btnRefresh_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnClose" runat="server" CausesValidation="False" meta:resourcekey="btnCloseResource1"
                                            Text="Close" OnClick="btnClose_Click" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center"></td>
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


 
