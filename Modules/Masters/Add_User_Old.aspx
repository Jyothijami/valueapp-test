<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Add_User_Old.aspx.cs" Inherits="Modules_Admin_Add_User_Old" Title="|| ValueApp : Admin : Add User ||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <script type="text/javascript">
        // Let's use a lowercase function name to keep with JavaScript conventions
        function selectAll(invoker) {
            // Since ASP.NET checkboxes are really HTML input elements
            //  let's get all the inputs 
            var inputElements = document.getElementsByTagName('input');

            for (var i = 0 ; i < inputElements.length ; i++) {
                var myElement = inputElements[i];

                // Filter through the input types looking for checkboxes
                if (myElement.type === "checkbox") {

                    // Use the invoker (our calling element) as the reference 
                    //  for our checkbox status
                    myElement.checked = invoker.checked;
                }
            }
        }
    </script>
    <%--<asp:UpdatePanel runat="server">
        <ContentTemplate>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("click", ".table_border td", function () {
                $(this).parent().first("a").trigger('click');
            });
            $("[id$='cbSelectAll1']").on("click", function () {

                $(this).parent().parent().find(':checkbox').prop('checked', this.checked);
                //$(this).prop('checked', true);
                //$(this).parent().parent().parent().children().each(function () {
                //    //alert($(this).html());
                //    $(":checked").attr('checked', true);
                //});
            });
        });

        function MSCAll(btn, n) {
            var ss;
        }
        function TABLE4_onclick() {
        }

    </script>
    <script>
        $(function () {
            $("#tabs").tabs({
                event: "mouseover"
            });
        });
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
    <br />
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="text-align: left;">
                
                <asp:Label ID="lblSearchValueHidden1" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left" colspan="2">Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                            <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
                        </td>
                    </tr>

                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">User Name :
                            <asp:TextBox ID="txtUser1" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 5%"></td>
                        <td style="width: 10%">Designation :

                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDesig1" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    
                    <tr>
                        <td style="text-align: right">Assign Date :
                            <asp:TextBox ID="txtFromDate" type="datepic" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 5%"></td>
                        <td style="width: 10%">Expiry Date : </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtToDate" type="datepic" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="text-align: right">Department :
                            <asp:TextBox ID="txtDept1" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 5%"></td>
                        <td style="width: 10%">Company Name :</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtComp1" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="text-align: center" colspan="5">

                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="text-align: center;">
                <asp:GridView ID="gvAddUserDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    meta:resourcekey="gvItemTypeDetailsResource1"
                    OnRowDataBound="gvAddUserDetails_RowDataBound" Width="100%" OnPageIndexChanging="gvAddUserDetails_PageIndexChanging">
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
                                    <td class="profilehead" colspan="4" style="text-align: left">General Details</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">Emp Name</td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlUserName" runat="server" Width="154px" OnSelectedIndexChanged="ddlUserName_SelectedIndexChanged"
                                            AutoPostBack="True">
                                        </asp:DropDownList><asp:Label ID="Label35" runat="server" EnableTheming="False" ForeColor="Red"
                                            Text="*"> </asp:Label><asp:RequiredFieldValidator ID="rfvddlUserName" runat="server" ControlToValidate="ddlUserName"
                                                ErrorMessage="Please Select the User Name" InitialValue="0" SetFocusOnError="True" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                                    <td style="text-align: right">Company Name</td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtCompanyName" runat="server" ReadOnly="True" OnTextChanged="txtUserId_TextChanged"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; height: 24px;">User Id / Email ID</td>
                                    <td style="text-align: left; height: 24px;">
                                        <asp:TextBox ID="txtUserId" runat="server" ReadOnly="True" OnTextChanged="txtUserId_TextChanged"></asp:TextBox></td>
                                    <td style="text-align: right; height: 24px;">Department</td>
                                    <td style="text-align: left; height: 24px;">
                                        <asp:TextBox ID="txtDepartment" runat="server" ReadOnly="True" OnTextChanged="txtUserId_TextChanged"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">Assign Date</td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtAssignDate" runat="server" type="datepic"></asp:TextBox>
                                        <%--<asp:Image ID="imgAssignDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>--%>
                                        <asp:Label ID="Label2" runat="server" EnableTheming="False" ForeColor="Red" Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="txtAssignDate" ErrorMessage="Please Select the Assign Date"
                                            SetFocusOnError="True" ValidationGroup="a">*</asp:RequiredFieldValidator><asp:CustomValidator ID="CustomValidator8"
                                                runat="server" ClientValidationFunction="DateCustomValidate" ControlToValidate="txtAssignDate"
                                                ErrorMessage="Please Enter the Assign Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                                SetFocusOnError="True">*</asp:CustomValidator><%--<cc1:CalendarExtender
                                                    Format="dd/MM/yyyy" ID="ceAssignDate" runat="server" Enabled="True" PopupButtonID="imgAssignDate"
                                                    TargetControlID="txtAssignDate">
                                                </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditAssignDate" runat="server" DisplayMoney="Left"
                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtAssignDate"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>--%>
                                    </td>
                                    <td style="text-align: right">Designation</td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtDesignation" runat="server" ReadOnly="True" OnTextChanged="txtUserId_TextChanged"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">Expiry Date</td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtExpiryDate" runat="server" type="datepic"></asp:TextBox><%--<asp:Image ID="imgExpiryDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>--%><asp:Label
                                            ID="Label3" runat="server" EnableTheming="False" ForeColor="Red" Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                ControlToValidate="txtExpiryDate" ErrorMessage="Please Select the Expiry Date"
                                                SetFocusOnError="True" ValidationGroup="a">*</asp:RequiredFieldValidator><asp:CustomValidator ID="CustomValidator1"
                                                    runat="server" ClientValidationFunction="DateCustomValidate" ControlToValidate="txtExpiryDate"
                                                    ErrorMessage="Please Enter the Expiry Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                                    SetFocusOnError="True">*</asp:CustomValidator><%--<cc1:CalendarExtender
                                                    Format="dd/MM/yyyy" ID="ceExpiryDATE" runat="server" Enabled="True" PopupButtonID="imgExpiryDate"
                                                    TargetControlID="txtExpiryDate">
                                                </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditExpiryDate" runat="server" DisplayMoney="Left"
                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtExpiryDate"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>--%>
                                    </td>
                                    <td style="text-align: right">User Type</td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlUsertype" runat="server" Width="154px" OnSelectedIndexChanged="ddlUserName_SelectedIndexChanged">
                                            <asp:ListItem>--Select--</asp:ListItem>
                                            <asp:ListItem Value="0">Admin</asp:ListItem>
                                            <asp:ListItem Value="1">User</asp:ListItem>
                                        </asp:DropDownList><asp:Label ID="Label12" runat="server" EnableTheming="False" ForeColor="Red"
                                            Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlUsertype"
                                                ErrorMessage="Please Select the User type" InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; visibility: hidden;">Privilege</td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlPriveleges" runat="server" OnSelectedIndexChanged="ddlPriveleges_SelectedIndexChanged" Visible="False">
                                            <asp:ListItem>Sales Cordinator</asp:ListItem>
                                            <asp:ListItem>Sales Person</asp:ListItem>
                                            <asp:ListItem>Sales Manager</asp:ListItem>
                                            <asp:ListItem>M.D</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: right">User Dashboard Type:</td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddluserSubType1" runat="server" DataSourceID="usertypesds1" DataTextField="userTypeName" DataValueField="userTypeId">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="usertypesds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT * FROM [usertype_tbl]"></asp:SqlDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: right"></td>
                                </tr>
                                <%--<tr>
                                    <td class="profilehead" colspan="4">Privileges</td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: left">



                                        <div id="tabs">
                                            <ul>
                                                <li><a href="#tabs-1">Masters</a></li>
                                                <li><a href="#tabs-2">Sales & Marketing</a></li>
                                                <li><a href="#tabs-3">Purchases</a></li>
                                                <li><a href="#tabs-4">Inventory</a></li>
                                                <li><a href="#tabs-5">Services</a></li>
                                                <li><a href="#tabs-6">Finance</a></li>
                                                <li><a href="#tabs-7">HR</a></li>
                                                <li><a href="#tabs-8">Reports</a></li>
                                                <li><a href="#tabs-9">Warehouse</a></li>
                                            </ul>
                                            <div id="tabs-1">
                                                <p>
                                                    <asp:GridView ID="gvMastersPriv1" runat="server" AutoGenerateColumns="False" DataKeyNames="PRIVILEGE_ID" DataSourceID="masterprivsds1">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Privilege" SortExpression="pagename">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("pagename") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("pagename") %>'></asp:Label>
                                                                    <asp:HiddenField ID="hfPRIVILEGE_ID1" runat="server" Value='<%# Eval("PRIVILEGE_ID") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Add">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxAdd1" runat="server" Checked='<%# getPermission("1", Eval("PRIVILEGE_ID").ToString()) %>'></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Update">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxUpdate1" runat="server" Checked='<%# getPermission("2", Eval("PRIVILEGE_ID").ToString()) %>'></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxDelete1" runat="server" Checked='<%# getPermission("3", Eval("PRIVILEGE_ID").ToString()) %>'></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Approve">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxApprove1" runat="server" Checked='<%# getPermission("4", Eval("PRIVILEGE_ID").ToString()) %>'></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Print">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxPrint1" runat="server" Checked='<%# getPermission("5", Eval("PRIVILEGE_ID").ToString()) %>'></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Email">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxEmail1" runat="server" Checked='<%# getPermission("6", Eval("PRIVILEGE_ID").ToString()) %>'></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="View">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxFull1" runat="server" Checked='<%# getPermission("7", Eval("PRIVILEGE_ID").ToString()) %>' ToolTip="ReadOnly" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Full">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbSelectAll1" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>

                                                    <asp:SqlDataSource ID="masterprivsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [PRIVILEGE_ID], [PRIVILEGE_NAME], [pagename] FROM [YANTRA_LKUP_PRIVILEGES] WHERE ([catename] = @catename)">
                                                        <SelectParameters>
                                                            <asp:Parameter DefaultValue="Master" Name="catename" Type="String" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>

                                                </p>
                                            </div>
                                            <div id="tabs-2">
                                                <p>
                                                    <asp:GridView ID="gvSMPriv1" runat="server" AutoGenerateColumns="False" DataKeyNames="PRIVILEGE_ID" DataSourceID="masterprivsds2">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Privilege" SortExpression="pagename">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("pagename") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label36" runat="server" Text='<%# Bind("pagename") %>'></asp:Label>
                                                                    <asp:HiddenField ID="hfPRIVILEGE_ID1" runat="server" Value='<%# Eval("PRIVILEGE_ID") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Add">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxAdd1" runat="server" Checked='<%# getPermission("1", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Update">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxUpdate1" runat="server" Checked='<%# getPermission("2", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxDelete1" runat="server" Checked='<%# getPermission("3", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Approve">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxApprove1" runat="server" Checked='<%# getPermission("4", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Print">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxPrint1" runat="server" Checked='<%# getPermission("5", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Email">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxEmail1" runat="server" Checked='<%# getPermission("6", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxFull1" runat="server" Checked='<%# getPermission("7", Eval("PRIVILEGE_ID").ToString()) %>' ToolTip="ReadOnly" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Full">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbSelectAll1" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:SqlDataSource ID="masterprivsds2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [PRIVILEGE_ID], [PRIVILEGE_NAME], [pagename] FROM [YANTRA_LKUP_PRIVILEGES] WHERE ([catename] = @catename)">
                                                        <SelectParameters>
                                                            <asp:Parameter DefaultValue="Sales &amp; Marketing" Name="catename" Type="String" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                                </p>
                                            </div>
                                            <div id="tabs-3">
                                                <p>
                                                                                                        <asp:GridView ID="gvSCMPriv1" runat="server" AutoGenerateColumns="False" DataKeyNames="PRIVILEGE_ID" DataSourceID="masterprivsds3">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Privilege" SortExpression="pagename">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("pagename") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label37" runat="server" Text='<%# Bind("pagename") %>'></asp:Label>
                                                                    <asp:HiddenField ID="hfPRIVILEGE_ID1" runat="server" Value='<%# Eval("PRIVILEGE_ID") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Add">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxAdd1" runat="server" Checked='<%# getPermission("1", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Update">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxUpdate1" runat="server" Checked='<%# getPermission("2", Eval("PRIVILEGE_ID").ToString()) %>'/>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxDelete1" runat="server" Checked='<%# getPermission("3", Eval("PRIVILEGE_ID").ToString()) %>'/>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Approve">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxApprove1" runat="server" Checked='<%# getPermission("4", Eval("PRIVILEGE_ID").ToString()) %>'/>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Print">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxPrint1" runat="server" Checked='<%# getPermission("5", Eval("PRIVILEGE_ID").ToString()) %>'/>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Email">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxEmail1" runat="server" Checked='<%# getPermission("6", Eval("PRIVILEGE_ID").ToString()) %>'/>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxFull1" runat="server" Checked='<%# getPermission("7", Eval("PRIVILEGE_ID").ToString()) %>' ToolTip="ReadOnly" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Full">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbSelectAll1" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:SqlDataSource ID="masterprivsds3" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [PRIVILEGE_ID], [PRIVILEGE_NAME], [pagename] FROM [YANTRA_LKUP_PRIVILEGES] WHERE ([catename] = @catename)">
                                                        <SelectParameters>
                                                            <asp:Parameter DefaultValue="Purchases" Name="catename" Type="String" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>

                                                </p>
                                            </div>
                                            <div id="tabs-4">
                                                <p>
                                                                                                        <asp:GridView ID="gvInventoryPriv1" runat="server" AutoGenerateColumns="False" DataKeyNames="PRIVILEGE_ID" DataSourceID="masterprivsds4">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Privilege" SortExpression="pagename">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("pagename") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label38" runat="server" Text='<%# Bind("pagename") %>'></asp:Label>
                                                                    <asp:HiddenField ID="hfPRIVILEGE_ID1" runat="server" Value='<%# Eval("PRIVILEGE_ID") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Add">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxAdd1" runat="server" Checked='<%# getPermission("1", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Update">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxUpdate1" runat="server" Checked='<%# getPermission("2", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxDelete1" runat="server" Checked='<%# getPermission("3", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Approve">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxApprove1" runat="server" Checked='<%# getPermission("4", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Print">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxPrint1" runat="server" Checked='<%# getPermission("5", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Email">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxEmail1" runat="server" Checked='<%# getPermission("6", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxFull1" runat="server" Checked='<%# getPermission("7", Eval("PRIVILEGE_ID").ToString()) %>' ToolTip="ReadOnly" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Full">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbSelectAll1" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:SqlDataSource ID="masterprivsds4" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [PRIVILEGE_ID], [PRIVILEGE_NAME], [pagename] FROM [YANTRA_LKUP_PRIVILEGES] WHERE ([catename] = @catename)">
                                                        <SelectParameters>
                                                            <asp:Parameter DefaultValue="Inventory" Name="catename" Type="String" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>

                                                </p>
                                            </div>
                                            <div id="tabs-5">
                                                <p>
                                                                                                        <asp:GridView ID="gvServicesPriv1" runat="server" AutoGenerateColumns="False" DataKeyNames="PRIVILEGE_ID" DataSourceID="masterprivsds5">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Privilege" SortExpression="pagename">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("pagename") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label39" runat="server" Text='<%# Bind("pagename") %>'></asp:Label>
                                                                    <asp:HiddenField ID="hfPRIVILEGE_ID1" runat="server" Value='<%# Eval("PRIVILEGE_ID") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Add">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxAdd1" runat="server" Checked='<%# getPermission("1", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Update">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxUpdate1" runat="server" Checked='<%# getPermission("2", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxDelete1" runat="server" Checked='<%# getPermission("3", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Approve">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxApprove1" runat="server" Checked='<%# getPermission("4", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Print">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxPrint1" runat="server" Checked='<%# getPermission("5", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Email">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxEmail1" runat="server" Checked='<%# getPermission("6", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxFull1" runat="server" Checked='<%# getPermission("7", Eval("PRIVILEGE_ID").ToString()) %>' ToolTip="ReadOnly" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Full">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbSelectAll1" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:SqlDataSource ID="masterprivsds5" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [PRIVILEGE_ID], [PRIVILEGE_NAME], [pagename] FROM [YANTRA_LKUP_PRIVILEGES] WHERE ([catename] = @catename)">
                                                        <SelectParameters>
                                                            <asp:Parameter DefaultValue="Services" Name="catename" Type="String" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>

                                                </p>
                                            </div>
                                            <div id="tabs-6">
                                                <p>
                                                                                                        <asp:GridView ID="gvFinancePriv1" runat="server" AutoGenerateColumns="False" DataKeyNames="PRIVILEGE_ID" DataSourceID="masterprivsds6">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Privilege" SortExpression="pagename">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("pagename") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label40" runat="server" Text='<%# Bind("pagename") %>'></asp:Label>
                                                                    <asp:HiddenField ID="hfPRIVILEGE_ID1" runat="server" Value='<%# Eval("PRIVILEGE_ID") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Add">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxAdd1" runat="server" Checked='<%# getPermission("1", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Update">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxUpdate1" runat="server" Checked='<%# getPermission("2", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxDelete1" runat="server" Checked='<%# getPermission("3", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Approve">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxApprove1" runat="server" Checked='<%# getPermission("4", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Print">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxPrint1" runat="server" Checked='<%# getPermission("5", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Email">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxEmail1" runat="server" Checked='<%# getPermission("6", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxFull1" runat="server" Checked='<%# getPermission("7", Eval("PRIVILEGE_ID").ToString()) %>' ToolTip="ReadOnly" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Full">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbSelectAll1" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:SqlDataSource ID="masterprivsds6" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [PRIVILEGE_ID], [PRIVILEGE_NAME], [pagename] FROM [YANTRA_LKUP_PRIVILEGES] WHERE ([catename] = @catename)">
                                                        <SelectParameters>
                                                            <asp:Parameter DefaultValue="Finance" Name="catename" Type="String" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>

                                                </p>
                                            </div>
                                            <div id="tabs-7">
                                                <p>
                                                                                                        <asp:GridView ID="gvHRPriv1" runat="server" AutoGenerateColumns="False" DataKeyNames="PRIVILEGE_ID" DataSourceID="masterprivsds7">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Privilege" SortExpression="pagename">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("pagename") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label41" runat="server" Text='<%# Bind("pagename") %>'></asp:Label>
                                                                    <asp:HiddenField ID="hfPRIVILEGE_ID1" runat="server" Value='<%# Eval("PRIVILEGE_ID") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Add">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxAdd1" runat="server" Checked='<%# getPermission("1", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Update">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxUpdate1" runat="server" Checked='<%# getPermission("2", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxDelete1" runat="server" Checked='<%# getPermission("3", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Approve">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxApprove1" runat="server" Checked='<%# getPermission("4", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Print">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxPrint1" runat="server" Checked='<%# getPermission("5", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Email">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxEmail1" runat="server" Checked='<%# getPermission("6", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxFull1" runat="server" Checked='<%# getPermission("7", Eval("PRIVILEGE_ID").ToString()) %>' ToolTip="ReadOnly" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Full">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbSelectAll1" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:SqlDataSource ID="masterprivsds7" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT PRIVILEGE_ID, PRIVILEGE_NAME, pagename FROM YANTRA_LKUP_PRIVILEGES WHERE (catename = @catename)">
                                                        <SelectParameters>
                                                            <asp:Parameter DefaultValue="HR" Name="catename" Type="String" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>

                                                </p>
                                            </div>
                                            <div id="tabs-8">
                                                <p>
                                                                                                        <asp:GridView ID="gvReportsPriv1" runat="server" AutoGenerateColumns="False" DataKeyNames="PRIVILEGE_ID" DataSourceID="masterprivsds8">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Privilege" SortExpression="pagename">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("pagename") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label42" runat="server" Text='<%# Bind("pagename") %>'></asp:Label>
                                                                    <asp:HiddenField ID="hfPRIVILEGE_ID1" runat="server" Value='<%# Eval("PRIVILEGE_ID") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Add">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxAdd1" runat="server" Checked='<%# getPermission("1", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Update">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxUpdate1" runat="server" Checked='<%# getPermission("2", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxDelete1" runat="server" Checked='<%# getPermission("3", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Approve">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxApprove1" runat="server" Checked='<%# getPermission("4", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Print">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxPrint1" runat="server" Checked='<%# getPermission("5", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Email">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxEmail1" runat="server" Checked='<%# getPermission("6", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxFull1" runat="server" Checked='<%# getPermission("7", Eval("PRIVILEGE_ID").ToString()) %>' ToolTip="ReadOnly" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Full">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbSelectAll1" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:SqlDataSource ID="masterprivsds8" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [PRIVILEGE_ID], [PRIVILEGE_NAME], [pagename] FROM [YANTRA_LKUP_PRIVILEGES] WHERE ([catename] = @catename)">
                                                        <SelectParameters>
                                                            <asp:Parameter DefaultValue="Reports" Name="catename" Type="String" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>

                                                </p>
                                            </div>
                                            <div id="tabs-9">
                                                <p>
                                                                                                        <asp:GridView ID="gvWarehousePriv1" runat="server" AutoGenerateColumns="False" DataKeyNames="PRIVILEGE_ID" DataSourceID="masterprivsds9">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Privilege" SortExpression="pagename">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("pagename") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label43" runat="server" Text='<%# Bind("pagename") %>'></asp:Label>
                                                                    <asp:HiddenField ID="hfPRIVILEGE_ID1" runat="server" Value='<%# Eval("PRIVILEGE_ID") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Add">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxAdd1" runat="server" Checked='<%# getPermission("1", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Update">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxUpdate1" runat="server" Checked='<%# getPermission("2", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxDelete1" runat="server" Checked='<%# getPermission("3", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Approve">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxApprove1" runat="server" Checked='<%# getPermission("4", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Print">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxPrint1" runat="server" Checked='<%# getPermission("5", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Email">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxEmail1" runat="server" Checked='<%# getPermission("6", Eval("PRIVILEGE_ID").ToString()) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbxFull1" runat="server" Checked='<%# getPermission("7", Eval("PRIVILEGE_ID").ToString()) %>' ToolTip="ReadOnly" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Full">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbSelectAll1" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:SqlDataSource ID="masterprivsds9" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [PRIVILEGE_ID], [PRIVILEGE_NAME], [pagename] FROM [YANTRA_LKUP_PRIVILEGES] WHERE ([catename] = @catename)">
                                                        <SelectParameters>
                                                            <asp:Parameter DefaultValue="Warehouse" Name="catename" Type="String" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>

                                                </p>
                                            </div>

                                        </div>


                                        <asp:HiddenField ID="hfPrivilegesList" runat="server" />
                                    </td>
                                </tr>--%>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="2" style="text-align: left">Company Access</td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" style="text-align: left">
                            <asp:DataList ID="dlCPAccess1" runat="server" DataKeyField="CP_ID" DataSourceID="Companysds1">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbxCP1" runat="server" Text='<%# Eval("CP_SHORT_NAME") %>' Checked='<%# Convert.ToBoolean(getCPPermission(Eval("CP_ID").ToString())) %>' />
                                    <asp:HiddenField ID="hfcpid1" runat="server" Value='<%# Eval("CP_ID") %>' />
                                </ItemTemplate>
                            </asp:DataList>
                            <asp:SqlDataSource ID="Companysds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [CP_ID], [CP_SHORT_NAME] FROM [v_YANTRA_COMP_PROFILE] where Status=1"></asp:SqlDataSource>
                        </td>
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
                                                ControlToValidate="txtPassword" ErrorMessage="Please Enter Password" SetFocusOnError="True" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
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
                                            OnClick="btnSave_Click" ValidationGroup="a" />
                                        <asp:Button ID="btnPermissions" runat="server" meta:resourcekey="btnSaveResource1" Text="Permissions"
                                            OnClick="btnPermissions_Click" OnClientClick="document.forms[0].target ='_blank';" ValidationGroup="a" Visible="False" />
                                    </td>
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

    <br />
    <table style ="width :100%">
        <tr>
            <td style="text-align: center; font-weight: bold;">&nbsp;
                  <asp:LinkButton ID="lnkUserValidity" runat="server" OnClick="lnkUserValidity_Click" Font-Underline="True">Add User Validity Update</asp:LinkButton>
                &nbsp;||&nbsp;
                   <asp:LinkButton ID="lnkEmpValidity" runat="server" OnClick="lnkEmpValidity_Click" Font-Underline="True">Employee App Validity Update</asp:LinkButton>
            </td>
        </tr>
    </table>
    <br />
    <asp:Panel ID="pnlEmp" runat ="server" Visible ="false"  >
        <div id="Div1" style="width: 100%" runat="server">
            <table style="width: 100%">
                <tr>
                    <td class="profilehead" style="text-align: left">Emp Validity Date Update
                    </td>

                </tr>
                <tr>
                    
                    <td style="text-align: right">
                        <asp:DropDownList ID="DropDownList2" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>25</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>75</asp:ListItem>
                            <asp:ListItem>100</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td style="text-align: right">Employee App Expiry Date :
                    <%--<asp:DropDownList ID="ddlBrand" runat="server"></asp:DropDownList>--%>
                        <asp:TextBox ID="TextBox3" type="datepic" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">
                        <asp:Button ID="btnEmpExpdt" runat="server" Text="Update" OnClick="btnEmpExpdt_Click" BackColor="#9966FF" EnableTheming="True" ForeColor="White" Width="188%" />
                    </td>
                    
                </tr>
            </table>
        </div>
            <br /> <asp:Label ID="lblEmpDate" runat ="server" Visible ="false" ></asp:Label>
        <div>
            <asp:GridView ID="gvEmp" OnPageIndexChanging ="gvEmp_PageIndexChanging"  Width="100%" runat ="server" AutoGenerateColumns="False" DataKeyNames="EMP_ID" DataSourceID="SqlDataSource2" SelectedRowStyle-BackColor="#c0c0c0" EmptyDataText="No Records To Display" AllowPaging="True" >
                <Columns>
                    <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkhdr" runat="server" AutoPostBack="True" OnClick="selectAll(this)" />
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="Chk" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    <asp:BoundField DataField="EMP_ID" HeaderText="EMP_ID" ReadOnly="True" SortExpression="EMP_ID" />
                    <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="EMP_FIRST_NAME" SortExpression="EMP_FIRST_NAME" />
                    <asp:BoundField DataField="EMP_LAST_NAME" HeaderText="EMP_LAST_NAME" SortExpression="EMP_LAST_NAME" />
                    <asp:BoundField DataField="EMP_DET_DOJ" HeaderText="EMP_DET_DOJ" SortExpression="EMP_DET_DOJ" />
                    <asp:BoundField DataField="EMP_DET_DOT" HeaderText="EMP_DET_DOT" SortExpression="EMP_DET_DOT" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT YANTRA_EMPLOYEE_MAST.EMP_ID, YANTRA_EMPLOYEE_MAST.EMP_FIRST_NAME, YANTRA_EMPLOYEE_MAST.EMP_LAST_NAME, YANTRA_EMPLOYEE_DET.EMP_DET_DOJ, YANTRA_EMPLOYEE_DET.EMP_DET_DOT FROM YANTRA_EMPLOYEE_MAST INNER JOIN YANTRA_EMPLOYEE_DET ON YANTRA_EMPLOYEE_MAST.EMP_ID = YANTRA_EMPLOYEE_DET.EMP_ID Where YANTRA_EMPLOYEE_MAST.Status!=0 ORDER By YANTRA_EMPLOYEE_MAST.EMP_ID DESC"></asp:SqlDataSource>
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlUser" Visible="false">
        <div id="body" style="width: 100%" runat="server">
            <table style="width: 100%">
                <tr>
                    <td class="profilehead" style="text-align: left">Add User Expiry Date Update
                    </td>

                </tr>
                <tr>
                    <%--<td style="text-align: right">Go To Page :
                                    <asp:TextBox ID="TextBox2" Width="100px" runat="server"></asp:TextBox>
                        <asp:Button ID="Button1" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click1" />

                    </td>--%>
                    <td style="text-align: right">
                        <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>25</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>75</asp:ListItem>
                            <asp:ListItem>100</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td style="text-align: right">User App Expiry Date :
                    <%--<asp:DropDownList ID="ddlBrand" runat="server"></asp:DropDownList>--%>
                        <asp:TextBox ID="txtExDate" type="datepic" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">
                        <asp:Button ID="btnExpDt" runat="server" Text="Update" OnClick="btnExpDt_Click" BackColor="#9966FF" EnableTheming="True" ForeColor="White" Width="188%" />
                    </td>
                    
                </tr>
            </table>
        </div>
        <br /> <asp:Label ID="lblDate" runat ="server" Visible ="false" ></asp:Label>
        <div>
            <asp:GridView ID="gvUser" Width="100%" runat="server" OnPageIndexChanging ="gvUser_PageIndexChanging" SelectedRowStyle-BackColor="#c0c0c0" EmptyDataText="No Records To Display" AllowPaging="True"  DataSourceID="SqlDataSource1" AutoGenerateColumns="False"  >
                <Columns >
                     <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkhdr" runat="server" AutoPostBack="True" OnClick="selectAll(this)" />
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="Chk" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    <asp:BoundField DataField="USER_ID" HeaderText="USER_ID" SortExpression="USER_ID" />
                    <asp:BoundField DataField="USER_NAME" HeaderText="USER_NAME" SortExpression="USER_NAME" />
                    <asp:BoundField DataField="ASSIGN_DATE" HeaderText="ASSIGN_DATE" SortExpression="ASSIGN_DATE" />
                    <asp:BoundField DataField="EXPIRY_DATE" HeaderText="EXPIRY_DATE" SortExpression="EXPIRY_DATE" />

                </Columns>
                <SelectedRowStyle BackColor="Silver" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="select [USER_ID], [USER_NAME], [ASSIGN_DATE], [EXPIRY_DATE] from YANTRA_USER_DETAILS,YANTRA_EMPLOYEE_MAST  where YANTRA_USER_DETAILS .Emp_id =YANTRA_EMPLOYEE_MAST .EMP_ID and STATUS =1 AND ([EXPIRY_DATE] = @EXPIRY_DATE) ORDER BY YANTRA_USER_DETAILS. User_ID Desc">
                <SelectParameters>
                    <asp:ControlParameter ControlID="lblDate" Name="EXPIRY_DATE" PropertyName="Text" Type="DateTime" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
    </asp:Panel>
    
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ShowValidationErrors="true" />
    <asp:ValidationSummary ID="ValidationSummary2" ValidationGroup="a" runat="server" ShowMessageBox="true" ShowSummary="false" ShowValidationErrors="true" />
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

 
