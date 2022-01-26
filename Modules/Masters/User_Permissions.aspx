<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="User_Permissions.aspx.cs" Inherits="Modules_Masters_User_Permissions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("click", ".table_border td", function () {
                $(this).parent().first("a").trigger('click');
            });
            $("[id$='cbSelectAll1']").on("click", function () {

                $(this).parent().parent().find(':checkbox').prop('checked', this.checked);
            });
        });

        function MSCAll(btn, n) {
            var ss;
        }
        function TABLE4_onclick() {
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
    <table style="width: 100%">
        <tr>
            <td style="text-align: center; font-size: medium;">
                <asp:LinkButton ID="lnk1" runat="server" OnClick="lnk1_Click" Font-Underline="True">Masters</asp:LinkButton>
                &nbsp;||&nbsp;
                            <asp:LinkButton ID="lnk2" runat="server" OnClick="lnk2_Click" Font-Underline="True">SM</asp:LinkButton>
                &nbsp;||&nbsp;
                            <asp:LinkButton ID="lnk3" runat="server" OnClick="lnk3_Click" Font-Underline="True">SCM</asp:LinkButton>
                &nbsp;||&nbsp;
                            <asp:LinkButton ID="lnk4" runat="server" OnClick="lnk4_Click" Font-Underline="True">Inventory</asp:LinkButton>
                &nbsp;||&nbsp;
                            <asp:LinkButton ID="lnk5" runat="server" OnClick="lnk5_Click" Font-Underline="True">Services</asp:LinkButton>
                &nbsp;||&nbsp;
                            <asp:LinkButton ID="lnk6" runat="server" OnClick="lnk6_Click" Font-Underline="True">Finance</asp:LinkButton>
                &nbsp;||&nbsp;
                            <asp:LinkButton ID="lnk7" runat="server" OnClick="lnk7_Click" Font-Underline="True">HR</asp:LinkButton>
                &nbsp;||&nbsp;
                            <asp:LinkButton ID="lnk8" runat="server" OnClick="lnk8_Click" Font-Underline="True">Reports</asp:LinkButton>
                &nbsp;||&nbsp;
                            <asp:LinkButton ID="lnk9" runat="server" OnClick="lnk9_Click" Font-Underline="True">Warehouse</asp:LinkButton>

            </td>
        </tr>
    </table>
    <table style="width: 100%" id="tblpriv" visible="true" runat="server">

        <tr>

            <td>
                <asp:HiddenField ID="hfPrivilegesList" runat="server" />
                <div id="tabs">
                    <asp:Panel runat="server" ID="Panel1" Visible="false">
                        <div class="profilehead">Master Privileges</div>
                        <div style="float: left">
                            <asp:Button ID="btnSaveMasters" runat="server" Text="Update Master" OnClick="btnSaveMasters_Click" />

                        </div>
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
                    </asp:Panel>
                </div>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Panel runat="server" ID="Panel2" Visible="false">
                    <div class="profilehead">SM Privileges</div>

                    <div style="float: left">
                        <asp:Button ID="btnSaveSM" runat="server" Text="Update SM" OnClick="btnSaveSM_Click" />
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
                </asp:Panel>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Panel runat="server" ID="Panel3" Visible="false">
                    <div class="profilehead">SCM Privileges</div>

                    <div style="float: left">
                        <asp:Button ID="btnSaveSCM" runat="server" Text="Update SCM" OnClick="btnSaveSCM_Click" />
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
                            <asp:SqlDataSource ID="masterprivsds3" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [PRIVILEGE_ID], [PRIVILEGE_NAME], [pagename] FROM [YANTRA_LKUP_PRIVILEGES] WHERE ([catename] = @catename)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="Purchases" Name="catename" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                        </p>
                    </div>
                </asp:Panel>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Panel runat="server" ID="Panel4" Visible="false">
                    <div class="profilehead">Inventory Privileges</div>

                    <div style="float: left">
                        <asp:Button ID="btnSaveInv" runat="server" Text="Update Inventory" OnClick="btnSaveInv_Click" />
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
                </asp:Panel>
            </td>
        </tr>


        <tr>
            <td>
                <asp:Panel runat="server" ID="Panel5" Visible="false">
                    <div class="profilehead">Services Privileges</div>

                    <div style="float: left">
                        <asp:Button ID="btnSaveServ" runat="server" Text="Update Services" OnClick="btnSaveServ_Click" />
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
                </asp:Panel>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Panel runat="server" ID="Panel6" Visible="false">
                    <div class="profilehead">Finance Privileges</div>

                    <div style="float: left">
                        <asp:Button ID="btnFinance" runat="server" Text="Update Finance" OnClick="btnFinance_Click" />
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
                </asp:Panel>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Panel runat="server" ID="Panel7" Visible="false">
                    <div class="profilehead">HR Privileges</div>

                    <div style="float: left">
                        <asp:Button ID="btnSaveHR" runat="server" Text="Update HR" OnClick="btnSaveHR_Click" />
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
                </asp:Panel>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Panel runat="server" ID="Panel8" Visible="false">
                    <div class="profilehead">Reports Privileges</div>

                    <div style="float: left">
                        <asp:Button ID="btnReports" runat="server" Text="Update Reports" OnClick="btnReports_Click" />
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
                </asp:Panel>
            </td>
        </tr>


        <tr>
            <td>
                <asp:Panel runat="server" ID="Panel9" Visible="false">
                    <div class="profilehead">Warehouse Privileges</div>

                    <div style="float: left">
                        <asp:Button ID="btnWarehouse" runat="server" Text="Update Warehouse" OnClick="btnWarehouse_Click" />
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
                </asp:Panel>
            </td>
        </tr>


    </table>
    <asp:Label ID="lblUserId" runat="server" Visible="false"></asp:Label>

            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


 
