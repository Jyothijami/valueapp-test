<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EnquiryMode.ascx.cs" Inherits="Modules_Masters_EnquiryMode" %>
<style type="text/css">
    .auto-style1 {
        width: 64px;
    }
</style>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td class="searchhead" colspan="2">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="text-align: left">
                        Enquiry Mode</td>
                    <td>
                    </td>
                    <td style="text-align: right">
                        <table border="0" cellpadding="0" cellspacing="0" align="right">
                            <tr>
                                <td rowspan="3">
                                    <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                        Text="Search By"></asp:Label></td>
                                <td rowspan="3">
                                    <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="textbox" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--</asp:ListItem>
                                        <asp:ListItem Value="ENQM_ID">S No</asp:ListItem>
                                        <asp:ListItem Value="ENQM_NAME">Enquiry Mode</asp:ListItem>
                                        <asp:ListItem Value="ENQM_DESC">Description</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td rowspan="3">
                                    </td>
                                <td rowspan="3" style="width: 172px">
                                    <asp:TextBox ID="txtSearchText" runat="server" CssClass="textbox">
                                    </asp:TextBox><asp:Image ID="imgCurrentDayTasksToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                        Visible="False"></asp:Image></td>
                                <td rowspan="3">
                                    <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                        CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                            </tr>
                            <tr>
                            </tr>
                            <tr>
                            </tr>
                        </table>
                        <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: center">
            <asp:GridView ID="gvEnquiryMode" runat="server" SelectedRowStyle-BackColor="#c0c0c0" AllowPaging="True" AutoGenerateColumns="False"
                DataSourceID="sdsEnquiryMode" OnRowDataBound="gvEnquiryMode_RowDataBound" AllowSorting="True" Width="100%">
                <Columns>
                    <asp:BoundField DataField="ENQM_NAME" HeaderText="EnquiryModeHidden"></asp:BoundField>
                    <asp:BoundField DataField="ENQM_ID" HeaderText="S.No">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Enquiry Mode">
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("Enqm_name") %>' ID="TextBox1"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnDepartmentName" ForeColor="#0066ff" runat="server" Text="<%# Bind('Enqm_Name') %>"
                                OnClick="lbtnEnquiryMode_Click" CausesValidation="False"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ENQM_DESC" HeaderText="Description" NullDisplayText="-">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:BoundField>
                </Columns>
                <SelectedRowStyle BackColor="LightSteelBlue" />
                <EmptyDataTemplate>
                    No Record Found
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsEnquiryMode" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                SelectCommand="SP_MASTER_ENQUIRYMODE_SEARCH_SELECT" SelectCommandType="StoredProcedure">
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
        <td colspan="2" style="text-align: left; height: 19px;">
        </td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: center; height: 49px;">
            <table id="Table1" align="center">
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
        <td style="text-align: right; height: 19px;">
        </td>
        <td>
            <table border="0" cellpadding="0" cellspacing="0" id="tblEquiryModeDetails" runat="server"
                visible="false" width="100%">
                <tr>
                    <td colspan="2" style="text-align: left" class="profilehead">
                        General Details</td>
                </tr>
                <tr>
                    <td style="height: 21px; text-align: right">
                    </td>
                    <td style="height: 21px; text-align: left;">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblEnquiryMode" runat="server" Text="Enquiry Mode" Width="111px"></asp:Label></td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtEnquiryMode" runat="server" ></asp:TextBox><asp:Label ID="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator
                            ID="RFVEnquiryMode" runat="server" ControlToValidate="txtEnquiryMode" ErrorMessage="Please Enter the Enquiry Mode">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label></td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="height: 19px; text-align: right">
                    </td>
                    <td style="height: 19px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 8px; text-align: center">
                        <table id="tblButtons" align="center">
                            <tr>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                <td class="auto-style1">
                                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click"
                                        CausesValidation="False" /></td>
                                <td>
                                    <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 8px; text-align: center">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False"></asp:ValidationSummary>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
