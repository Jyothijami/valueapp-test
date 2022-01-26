<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SubContractorMaster.ascx.cs"
    Inherits="Modules_Masters_SubContractorMaster" %>
<style type="text/css">
    .auto-style1 {
        height: 19px;
    }
</style>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td class="searchhead" colspan="4" style="text-align: left">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="text-align: left">
                        Sub Contractor Details</td>
                    <td>
                    </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" align="right">
                            <tr>
                                <td>
                                    <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                        Text="Search By"></asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="textbox" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--</asp:ListItem>
                                        <asp:ListItem Value="SC_NAME">Name</asp:ListItem>
                                        <asp:ListItem Value="SC_CONTACT_PERSON">Contact Person</asp:ListItem>
                                        <asp:ListItem Value="SC_ADDRESS">Address</asp:ListItem>
                                        <asp:ListItem Value="SC_CONTACT_NO">Contact No</asp:ListItem>
                                        <asp:ListItem Value="SC_EMAIL">EMail</asp:ListItem>
                                        <asp:ListItem Value="SC_RANKING">Ranking</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td>
                                    </td>
                                <td>
                                    <asp:TextBox ID="txtSearchText" runat="server" CssClass="textbox">
                                    </asp:TextBox><asp:Image ID="imgCurrentDayTasksToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                        Visible="False"></asp:Image></td>
                                <td>
                                    <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                        CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
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
            <asp:GridView ID="gvSubContractorDetails" SelectedRowStyle-BackColor="#c0c0c0" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                DataSourceID="sdsSubContractorDetails" OnRowDataBound="gvSubContractorDetails_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="SC_NAME" HeaderText="SubContractorHidden"></asp:BoundField>
                    <asp:BoundField DataField="SC_ID" HeaderText="S.No">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Sub Contrator Name">
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("SC_NAME") %>' ID="TextBox1"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnSubContractor" ForeColor="#0066ff" runat="server" Text="<%# Bind('SC_NAME') %>"
                                OnClick="lbtnSubContractor_Click" CausesValidation="False"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SC_CONTACT_PERSON" HeaderText="Contact Person">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="SC_ADDRESS" HeaderText="Address">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="SC_CONTACT_PERSON_DET" HeaderText="Contact Person Details">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="SC_CONTACT_NO1" HeaderText="Contact No.">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="SC_CONTACT_NO2" HeaderText="Contact No 2">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="SC_EMAIL" HeaderText="Email">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="SC_FAX_NO" HeaderText="Fax No.">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="SC_PAN_NO" HeaderText="PAN No.">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="SC_CST_NO" HeaderText="CST  No.">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="SC_VAT_NO" HeaderText="VAT No.">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="SC_ECC_NO" HeaderText="ECC No.">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="SC_RANKING" HeaderText="Ranking">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                    </asp:BoundField>
                </Columns>
                <SelectedRowStyle BackColor="LightSteelBlue" />
                <EmptyDataTemplate>
                    No Record Found
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsSubContractorDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                SelectCommand="SP_MASTER_SUBCONTRACTOR_SEARCH_SELECT" SelectCommandType="StoredProcedure">
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
            <table border="0" cellpadding="0" cellspacing="0" id="tblSubContractorDetails" runat="server"
                visible="false" width="100%">
             
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
                        <asp:Label ID="lblSubContractorName" runat="server" Text="SubContractor  Name" Width="137px"></asp:Label></td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtSubContractorName" runat="server" MaxLength="20"></asp:TextBox>
                        <asp:Label ID="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator
                            ID="RFVSCName" runat="server" ControlToValidate="txtSubContractorName" ErrorMessage="Please Enter the Sub Contractor  Name">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                ID="REVSCName" runat="server" ControlToValidate="txtSubContractorName" ErrorMessage="Please Enter only Alphabets in Sub Contractor Name"
                                ValidationExpression="^[a-zA-Z. ]*$">*</asp:RegularExpressionValidator></td>
                    <td style="text-align: right;">
                        <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person" Width="119px"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtContactPerson" runat="server" MaxLength="20"></asp:TextBox>
                        <asp:Label ID="Label8" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator ID="RFVContactPerson" runat="server" ControlToValidate="txtContactPerson"
                            ErrorMessage="Please Enter the Contact Person">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                ID="REVContactPerson" runat="server" ControlToValidate="txtContactPerson" ErrorMessage="Please Enter only Alphabets in Contact Person Name"
                                ValidationExpression="^[a-zA-Z. ]*$">*</asp:RegularExpressionValidator></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblAddress" runat="server" Text="Address" Width="105px"></asp:Label></td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine">
                        </asp:TextBox>
                        <asp:Label ID="Label1" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator ID="RFVAddress" runat="server" ControlToValidate="txtAddress"
                            ErrorMessage="Please Enter the Address">*</asp:RequiredFieldValidator></td>
                    <td style="text-align: right;">
                        <asp:Label ID="lblContactPersonDetails" runat="server" Text="Contact Person Details"
                            Width="140px"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtContactPersonDetails" runat="server" TextMode="MultiLine">
                        </asp:TextBox>
                        <asp:Label ID="Label9" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator ID="RFVContactPersonDet" runat="server"
                            ControlToValidate="txtContactPersonDetails" ErrorMessage="Please Enter the Contact Person Details">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtEmail" runat="server" MaxLength="20"></asp:TextBox>
                        <asp:Label ID="Label2" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator ID="RFVEmail" runat="server" ControlToValidate="txtEmail"
                            ErrorMessage="Please Enter the Email">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                ID="REVEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Please Enter Email in Correct Format(Eg : abc@def.com)"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td>
                    <td style="text-align: right">
                        <asp:Label ID="lblFaxNo" runat="server" Text="Fax No" Width="119px"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtFaxNo" runat="server" MaxLength="20"></asp:TextBox>
                        <asp:Label ID="Label10" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator
                            ID="RFVFAXNo" runat="server" ControlToValidate="txtFaxNo" ErrorMessage="Please Enter the FAX No">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblContactNo1" runat="server" Text="Contact No 1" Width="96px"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtContactNo1" runat="server" MaxLength="20"></asp:TextBox>
                        <asp:Label ID="Label3" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator
                            ID="RFVContactNo1" runat="server" ControlToValidate="txtContactNo1" ErrorMessage="Please Enter the Contact No1">*</asp:RequiredFieldValidator></td>
                    <td style="text-align: right">
                        <asp:Label ID="lblContactNo2" runat="server" Text="Contact No 2" Width="96px"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtContactNo2" runat="server" MaxLength="20"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="height: 21px; text-align: right">
                        <asp:Label ID="lblPANNo" runat="server" Text="PAN No" Width="108px"></asp:Label></td>
                    <td style="height: 21px; text-align: left">
                        <asp:TextBox ID="txtPANNo" runat="server" MaxLength="20"></asp:TextBox>
                        <asp:Label ID="Label4" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator ID="RFVPANNo" runat="server" ControlToValidate="txtPANNo"
                            ErrorMessage="Please Enter the PAN No">*</asp:RequiredFieldValidator></td>
                    <td style="height: 21px; text-align: right">
                        <asp:Label ID="lblCSTNo" runat="server" Text="CST No" Width="78px"></asp:Label></td>
                    <td style="height: 21px; text-align: left">
                        <asp:TextBox ID="txtCSTNo" runat="server" MaxLength="20"></asp:TextBox>
                        <asp:Label ID="Label11" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator
                            ID="RFVCSTNo" runat="server" ControlToValidate="txtCSTNo" ErrorMessage="Please Enter the CST No">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="height: 21px; text-align: right">
                        <asp:Label ID="lblVATNo" runat="server" Text="VAT No" Width="96px"></asp:Label></td>
                    <td style="height: 21px; text-align: left">
                        <asp:TextBox ID="txtVATNo" runat="server" MaxLength="20"></asp:TextBox>
                        <asp:Label ID="Label5" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator ID="RFVVATNo" runat="server" ControlToValidate="txtVATNo"
                            ErrorMessage="Please Enter the VAT No">*</asp:RequiredFieldValidator></td>
                    <td style="height: 21px; text-align: right">
                        <asp:Label ID="lblECCNo" runat="server" Text="ECC No" Width="96px"></asp:Label></td>
                    <td style="height: 21px; text-align: left;">
                        <asp:TextBox ID="txtECCNo" runat="server" MaxLength="20"></asp:TextBox>
                        <asp:Label ID="Label12" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator
                            ID="RFVECCNo" runat="server" ControlToValidate="txtECCNo" ErrorMessage="Please Enter the ECC No">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="height: 21px; text-align: right">
                        <asp:Label ID="lblRanking" runat="server" Text="Ranking" Width="96px"></asp:Label></td>
                    <td style="height: 21px; text-align: left">
                        <asp:TextBox ID="txtRanking" runat="server" MaxLength="20"></asp:TextBox>
                        <asp:Label ID="Label6" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator
                            ID="RFVRanking" runat="server" ControlToValidate="txtRanking" ErrorMessage="Please Enter the Ranking">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                ID="REVRanking" runat="server" ControlToValidate="txtRanking" ErrorMessage="Please Enter only Numerics in Ranking"
                                ValidationExpression="[0-9]*$">*</asp:RegularExpressionValidator></td>
                    <td style="height: 21px; text-align: right">
                    </td>
                    <td style="height: 21px; text-align: left;">
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
            <asp:ValidationSummary ID="ValidationSummary1" runat="server"></asp:ValidationSummary>
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
