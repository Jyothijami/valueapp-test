<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OperationsMaster.ascx.cs"
    Inherits="Modules_Masters_OperationsMaster" %>
<table style="width: 778px" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td class="searchhead" colspan="2" style="text-align: left">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="text-align: left">
                        Operations Master</td>
                    <td>
                    </td>
                    <td style="text-align: right; width: 544px;">
                        <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                            Text="Search By"></asp:Label><asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="textbox" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="OPR_ID">S.No</asp:ListItem>
                                <asp:ListItem Value="OPR_NAME">Operation Name</asp:ListItem>
                                <asp:ListItem Value="OPR_DESC">Description</asp:ListItem>
                            </asp:DropDownList><asp:TextBox
                                    ID="txtSearchText" runat="server" CssClass="textbox" Width="111px"></asp:TextBox><asp:Image
                                        ID="imgCurrentDayTasksToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                        Visible="False"></asp:Image><asp:Button ID="btnSearchGo" runat="server" BorderStyle="None"
                                            CausesValidation="False" CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click"
                                            Text="Go" /><asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label><asp:Label
                                                ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2" id="gvOperationDetails" style="text-align: center">
            <asp:GridView ID="gvOperationDetails" SelectedRowStyle-BackColor="#c0c0c0" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                DataSourceID="sdsOperationDetails" OnRowDataBound="gvOperationDetails_RowDataBound"
                Width="100%">
                <Columns>
                    <asp:BoundField DataField="OPR_NAME" HeaderText="OprationNameHidden"></asp:BoundField>
                    <asp:BoundField DataField="OPR_ID" HeaderText="S.No.">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Opertaion Name">
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("Opr_name") %>' ID="TextBox1"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnOperationName" ForeColor="#0066ff" runat="server" Text="<%# Bind('Opr_Name') %>"
                                OnClick="lbtnOperationName_Click" CausesValidation="False"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="OPR_DESC" HeaderText="Description">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:BoundField>
                </Columns>
                <SelectedRowStyle BackColor="LightSteelBlue" />
                <EmptyDataTemplate>
                    No Record Found
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsOperationDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                SelectCommand="SP_MASTER_OPERATIONS_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName"
                        ControlID="lblSearchItemHidden"></asp:ControlParameter>
                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue"
                        ControlID="lblSearchValueHidden"></asp:ControlParameter>
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: center; height: 49px;">
            <table id="Table1">
                <tr>
                    <td>
                        <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CausesValidation="False" /></td>
                    <td>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CausesValidation="False" /></td>
                    <td>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"
                            CausesValidation="False" /></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: center; height: 159px;" id="tblOprDetails">
            <table border="0" cellpadding="0" cellspacing="0" id="tblOprDetails" runat="server"
                visible="false" width="100%">
                <tr>
                    <td colspan="2" style="text-align: left" class="profilehead">
                        General Details</td>
                </tr>
                <tr>
                    <td style="height: 19px; text-align: right">
                    </td>
                    <td style="height: 19px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; height: 24px;">
                        <asp:Label ID="lblOperationCode" runat="server" Text="Operation Code" Width="153px"></asp:Label></td>
                    <td style="text-align: left; height: 24px;">
                        <asp:TextBox ID="txtOperationCode" runat="server" Width="175px" MaxLength="20"></asp:TextBox>
                        <asp:Label ID="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator ID="RFVOperationCode" runat="server" ControlToValidate="txtOperationCode"
                            ErrorMessage="Please Enter the Operation Code">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="text-align: right; height: 40px;">
                        <asp:Label ID="lblDescription" runat="server" Text="Description" Width="105px"></asp:Label></td>
                    <td style="text-align: left; height: 40px;">
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 19px">
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <table id="tblButtons">
                            <tr>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                <td>
                                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click"
                                        CausesValidation="False" /></td>
                                <td>
                                    <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="height: 20px" colspan="2">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ShowSummary="False"></asp:ValidationSummary>
        </td>
    </tr>
</table>
