<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TransportMaster.ascx.cs"
    Inherits="Modules_Masters_TransportMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td class="searchhead" colspan="4" style="text-align: left">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="text-align: left">
                        Transporter Master</td>
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
                                        <asp:ListItem Value="TRANS_ID">S.No</asp:ListItem>
                                        <asp:ListItem Value="TRANS_LONG_NAME">Transport Name</asp:ListItem>
                                        <asp:ListItem Value="TRANS_CONTACT_PERSON">Contact Person</asp:ListItem>
                                        <asp:ListItem Value="TRANS_ADDRESS">Address</asp:ListItem>
                                        <asp:ListItem Value="TRANS_CONTACT_NO">Contact No</asp:ListItem>
                                        <asp:ListItem Value="TRANS_MOBILE_NO">Mobile No</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td rowspan="3">
                                    </td>
                                <td rowspan="3">
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
        <td colspan="4" style="text-align: center">
            <asp:GridView ID="gvTransporterDetails" SelectedRowStyle-BackColor="#c0c0c0" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                DataSourceID="sdsTransporterMasterDetails" OnRowDataBound="gvTransporterDetails_RowDataBound"
                Width="100%">
                <Columns>
                    <asp:BoundField DataField="TRANS_LONG_NAME" SortExpression="TRANS_LONG_NAME" HeaderText="TransportNameHidden">
                    </asp:BoundField>
                    <asp:BoundField ReadOnly="True" DataField="TRANS_ID" SortExpression="TRANS_ID" HeaderText="S.No.">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                    </asp:BoundField>
                    <asp:TemplateField SortExpression="TRANS_LONG_NAME" HeaderText="Transport Name">
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("TRANS_LONG_NAME") %>' ID="TextBox1"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemTemplate>
                            &nbsp;<asp:LinkButton ID="lbtnTranportName" ForeColor="#0066ff" runat="server" Text='<%# Bind("TRANS_LONG_NAME") %>'
                                OnClick="lbtnTranportName_Click" CausesValidation="False"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="TRANS_CONTACT_PERSON" SortExpression="TRANS_CONTACT_PERSON"
                        HeaderText="Contact Person">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="TRANS_ADDRESS" SortExpression="TRANS_ADDRESS" HeaderText="Address">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="TRANS_CONTACT_NO" SortExpression="TRANS_CONTACT_NO" HeaderText="Contact No.">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="TRANS_MOBILE_NO" SortExpression="TRANS_MOBILE_NO" HeaderText="Mobile No.">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                    </asp:BoundField>
                </Columns>
                <EmptyDataTemplate>
                    No Record Found
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsTransporterMasterDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                SelectCommand="SP_MASTER_TRANSPORT_SEARCH_SELECT" SelectCommandType="StoredProcedure">
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
        <td colspan="4" style="height: 21px; text-align: left">
        </td>
    </tr>
    <tr>
        <td colspan="4" style="text-align: center">
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
        <td colspan="4">
            <table border="0" cellpadding="0" cellspacing="0" id="tblTransporterDetails"
                runat="server" visible="false" width="100%">
                <tr>
                    <td colspan="4" style="text-align: left" class="profilehead">
                        General Details</td>
                </tr>
                <tr>
                    <td style="height: 21px; text-align: right">
                    </td>
                    <td style="height: 21px; text-align: left">
                    </td>
                    <td style="height: 21px; text-align: right">
                    </td>
                    <td style="height: 21px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblTransportLongName" runat="server" Text="Transport Name" Width="108px"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtTransportLongName" runat="server" MaxLength="50"></asp:TextBox>
                        <asp:Label ID="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator
                            ID="RFVTransportLongName" runat="server" ControlToValidate="txtTransportLongName"
                            ErrorMessage="Please Enter the Transport Long Name">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                ID="REVTransLongName" runat="server" ControlToValidate="txtTransportLongName"
                                ErrorMessage="Please Enter only Alphabets in Transport Long Name" ValidationExpression="^[a-zA-Z. ]*$">*</asp:RegularExpressionValidator></td>
                    <td style="text-align: right">
                        <asp:Label ID="lblContactPersonName" runat="server" Text="Contact Person Name" Width="153px"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtContactPersonName" runat="server" MaxLength="20"></asp:TextBox>
                        <asp:Label ID="Label3" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator
                            ID="RFVContactPerson" runat="server" ControlToValidate="txtContactPersonName"
                            ErrorMessage="Please Enter the Contact Person Name">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                ID="REVCPName" runat="server" ControlToValidate="txtContactPersonName" ErrorMessage="Please Enter only Alphabets in  Contact Person Name"
                                ValidationExpression="^[a-zA-Z. ]*$">*</asp:RegularExpressionValidator></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblAddress" runat="server" Text="Address" Width="105px"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
                        <asp:Label ID="Label1" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator
                            ID="RFVAddress" runat="server" ControlToValidate="txtAddress" ErrorMessage="Please Enter the Address">*</asp:RequiredFieldValidator></td>
                    <td style="text-align: right">
                        <asp:Label ID="lblContactNo" runat="server" Text="Contact No" Width="105px"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtContactNo" runat="server" MaxLength="20"></asp:TextBox>
                        <asp:Label ID="Label4" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator
                            ID="RFVContactNo" runat="server" ControlToValidate="txtContactNo" ErrorMessage="Please Enter the Contact No">*</asp:RequiredFieldValidator><cc1:filteredtextboxextender
                                id="ContactNo" runat="server" targetcontrolid="txtContactNo" validchars="0123456789-()"></cc1:filteredtextboxextender></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblMobileNo" runat="server" Text="Mobile No" Width="91px"></asp:Label>&nbsp;
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtMobileNo" runat="server" MaxLength="20"></asp:TextBox>
                        <asp:Label ID="Label2" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator
                            ID="RFVMobileNo" runat="server" ControlToValidate="txtMobileNo" ErrorMessage="Please Enter the Mobile No">*</asp:RequiredFieldValidator><cc1:filteredtextboxextender
                                id="MobileNo" runat="server" targetcontrolid="txtMobileNo" validchars="0123456789-()"></cc1:filteredtextboxextender></td>
                    <td style="text-align: right">
                        </td>
                    <td style="text-align: left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right; height: 19px;">
                    </td>
                    <td style="text-align: left; height: 19px;">
                    </td>
                    <td style="text-align: right; height: 19px;">
                    </td>
                    <td style="text-align: left; height: 19px;">
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center">
                        <table id="tblButtons" align="center">
                            <tr>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                <td>
                                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click"
                                        CausesValidation="False" /></td>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>
        </td>
    </tr>
    <tr>
        <td style="height: 21px">
        </td>
        <td style="height: 21px;">
        </td>
        <td style="height: 21px;">
        </td>
        <td style="height: 21px">
        </td>
    </tr>
</table>
