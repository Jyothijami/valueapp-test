<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/PurchaseMP1.master" AutoEventWireup="true" CodeFile="SupplierMasterNew.aspx.cs" Inherits="Modules_SCM_SupplierMasterNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--    <script src="/bootstrap/js/bootstrap.min.js"></script>
    <link href="../../site_resources/css/prettify.css" rel="stylesheet" />
    <script src="../../site_resources/scripts/prettify.js"></script>
    <link href="/site_resources/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="/site_resources/scripts/bootstrap-multiselect.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <table border="0" style="width: 100%" cellpadding="0" cellspacing="0" class="pagehead">
                <tr>
                    <td style="text-align: left">Supplier Master Details
                    </td>
                    <td></td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td id="tblSupDetails" runat="server" visible="true" colspan="4" style="text-align: center">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td colspan="4" style="text-align: left" class="profilehead">General Details</td>
                            </tr>
                            <tr>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">&nbsp;</td>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblSupplierName" runat="server" Text="Supplier Name" Width="119px"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtSupplierName" runat="server">
                                    </asp:TextBox>
                                    <asp:Label ID="Label36" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                                    </asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSupplierName"
                                        ErrorMessage="Please Enter the Supplier Name">*</asp:RequiredFieldValidator></td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person" Width="119px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtContactPerson" runat="server">
                                    </asp:TextBox>
                                    <asp:Label ID="Label4" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                                    </asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtContactPerson"
                                        ErrorMessage="Please enter the Contact Person">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblAddress" runat="server" Text="Address" Width="105px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine">
                                    </asp:TextBox></td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblContactPersonDetails" runat="server" Text="Contact Person Details" Width="152px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtContactPersonDetails" runat="server" TextMode="MultiLine">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label10" runat="server" Text="Country" Width="96px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlCountry" runat="server" Width="148px">
                                    </asp:DropDownList></td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblContactNo2" runat="server" Text="Mobile No." Width="96px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtContactNo2" runat="server"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="ftxteMobileNo" runat="server" TargetControlID="txtContactNo2"
                                        ValidChars="-0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblContactNo1" runat="server" Text="Phone No." Width="96px"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtContactNo1" runat="server"></asp:TextBox><asp:Label ID="Label6" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                                    </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtContactNo1"
                                        ErrorMessage="Please Enter the Phone No">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender ID="ftxtePhoneNo" runat="server" TargetControlID="txtContactNo1"
                                            ValidChars=" +()-0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                </td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblFaxNo" runat="server" Text="Fax No" Width="119px"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtFaxNo" runat="server"></asp:TextBox><%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"
                                        TagPrefix="cc1" %>
                                    <cc1:FilteredTextBoxExtender ID="ftxteFaxNo" runat="server"
                                        TargetControlID="txtFaxNo" ValidChars="0123456789 -()">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>   
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtEmail" runat="server">
                                    </asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                        ErrorMessage="Please enter the email  in  correct format" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblCSTNo" runat="server" Text="CST No" Width="148px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtCSTNo" runat="server"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="ftxteCSTNo" runat="server"
                                        TargetControlID="txtCSTNo" ValidChars="QWERTYUIOPASDFGHJKLZXCVBNM0123456789-/ ">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblPANNo" runat="server" Text="PAN No" Width="108px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtPANNo" runat="server">
                                    </asp:TextBox><cc1:FilteredTextBoxExtender ID="ftxtePANNo" runat="server"
                                        TargetControlID="txtPANNo" ValidChars="QWERTYUIOPASDFGHJKLZXCVBNM0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblECCNo" runat="server" Text="ECC No" Width="96px"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtECCNo" runat="server"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="ftxteECCNo" runat="server" FilterType="Numbers"
                                        TargetControlID="txtECCNo">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblVATNo" runat="server" Text="VAT/TIN No" Width="96px"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtVATNo" runat="server">
                                    </asp:TextBox><cc1:FilteredTextBoxExtender ID="ftxteVATNo" runat="server"
                                        TargetControlID="txtVATNo" ValidChars="QWERTYUIOPASDFGHJKLZXCVBNM0123456789-/ ">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label1" runat="server" Text="Brand" Width="96px"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlBrandName" runat="server" Width="148px">
                                    </asp:DropDownList>
                                    <asp:Label ID="Label7" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                                    </asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlBrandName"
                                        ErrorMessage="Please enter the Brand" InitialValue="0">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblRanking" runat="server" Text="Ranking" Width="96px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtRanking" runat="server"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="ftxteRanking" runat="server" FilterType="Numbers"
                                        TargetControlID="txtRanking">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label8" runat="server" Text="ST NO"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtSTNO" runat="server">
                                    </asp:TextBox><br />
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                        TargetControlID="txtSTNO" ValidChars="QWERTYUIOPASDFGHJKLZXCVBNM0123456789-/ ">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">PO Template</td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlPOTemplate1" runat="server" AppendDataBoundItems="True" DataSourceID="sptsds1" DataTextField="sptname" DataValueField="sptid">
                                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="sptsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [sptid], [sptname] FROM [sup_po_templates_tbl] ORDER BY [sptname]"></asp:SqlDataSource>
                                </td>
                                <td style="text-align: right">&nbsp;</td>
                                <td style="text-align: left">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label9" runat="server" Text="Basis Of Approval"></asp:Label></td>
                                <td style="text-align: left" colspan="3">
                                    <asp:CheckBoxList ID="chklBasisOfApproval" runat="server" RepeatColumns="4">
                                        <asp:ListItem>Experience</asp:ListItem>
                                        <asp:ListItem>Past Performance</asp:ListItem>
                                        <asp:ListItem>Brand Name</asp:ListItem>
                                        <asp:ListItem>Price</asp:ListItem>
                                        <asp:ListItem>Long Standing in the International Market</asp:ListItem>
                                        <asp:ListItem>Product Past Performance</asp:ListItem>
                                        <asp:ListItem>ISO 9001 Certified</asp:ListItem>
                                        <asp:ListItem>Compliance 21 CFR Park 11 (Electronic &amp; Signature)</asp:ListItem>
                                        <asp:ListItem>Recognition by Scientific Community &amp; Others</asp:ListItem>
                                        <asp:ListItem>Company Reputation</asp:ListItem>
                                        <asp:ListItem>US EPA Approval</asp:ListItem>
                                        <asp:ListItem>FDA Approval</asp:ListItem>
                                    </asp:CheckBoxList></td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: right"></td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: left; height: 19px;">
                                    <table class="stacktable">
                                        <tr>
                                            <td colspan="4" class="profilehead">Unit Details</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label37" runat="server" Text="Unit Name :"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtUnitName" runat="server"></asp:TextBox>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label38" runat="server" Text="Unit Address :"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtUnitAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td style="text-align: right">
                                                <asp:Button ID="btnadd" runat="server" OnClick="btnadd_Click" Text="Add" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnunitrefresh" runat="server" OnClick="btnunitrefresh_Click" Text="Refresh" />
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:GridView ID="gvUnitDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvUnitDetails_RowDataBound"
                                                    OnRowDeleting="gvUnitDetails_RowDeleting" Width="100%" OnRowEditing="gvUnitDetails_RowEditing" meta:resourcekey="gvUnitDetailsResource1">
                                                    <Columns>
                                                        <asp:CommandField ShowEditButton="True" meta:resourceKey="CommandFieldResource3"></asp:CommandField>
                                                        <asp:CommandField ShowDeleteButton="True" meta:resourceKey="CommandFieldResource4"></asp:CommandField>
                                                        <asp:TemplateField HeaderText="Unit Name " meta:resourceKey="TemplateFieldResource2">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnUnitName" runat="server" Text='<%# Bind("UnitName") %>' CausesValidation="False" meta:resourceKey="lbtnUnitNameResource1" __designer:wfdid="w4"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="UnitAddress" HeaderText="Unit Address" meta:resourceKey="BoundFieldResource19">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="UnitName" HeaderText="UnitName" meta:resourceKey="BoundFieldResource20"></asp:BoundField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <div class="text-center">
                                                            <span style="color: #ff0000">No Data to Display</span>
                                                        </div>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label3" runat="server" Text="Item Type" Width="96px" Visible="False"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged" Visible="False">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label2" runat="server" Text="Item Name" Width="96px" Visible="False"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlItemName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" Visible="False">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label5" runat="server" Text="UOM" Visible="False"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtItemUOM" runat="server" ReadOnly="True" Visible="False"></asp:TextBox></td>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <table id="tblButtons" align="center">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                            <td>
                                                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" CausesValidation="False" /></td>
                                            <td>
                                                <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" />
                                                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" CausesValidation="False" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center">
                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ></asp:ValidationSummary>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="a"></asp:ValidationSummary>
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="unit"></asp:ValidationSummary>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
 
