<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CompanyProfile.ascx.cs"
    Inherits="Modules_Masters_CompanyProfile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td class="searchhead" colspan="2" style="text-align: center">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="text-align: left">
                        Company Master</td>
                    <td>
                    </td>
                    <td style="text-align: right">
                        <table border="0" cellpadding="0" cellspacing="0" align="right">
                            <tr>
                                <td>
                                    <asp:Label ID="Label27" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                        Text="Search By"></asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                        OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--</asp:ListItem>
                                        <asp:ListItem Value="CP_ID">Sl No</asp:ListItem>
                                        <asp:ListItem Value="CP_FULL_NAME">Company Full Name</asp:ListItem>
                                        <asp:ListItem Value="CP_SHORT_NAME">Company Short Name</asp:ListItem>
                                        <asp:ListItem Value="CP_ADDRESS">Address</asp:ListItem>
                                        <asp:ListItem Value="CP_CONTACT_NO1">Contact No</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSearchText" runat="server" CssClass="textbox"></asp:TextBox>&nbsp;
                                </td>
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
            <asp:GridView ID="gvCompanyDetails" runat="server" SelectedRowStyle-BackColor="#c0c0c0" AllowPaging="True" AutoGenerateColumns="False"
                DataKeyNames="CP_ID" DataSourceID="sdsCompanyProfile" OnRowDataBound="gvCompanyDetails_RowDataBound" Width="100%">
                <Columns>
                    <asp:BoundField DataField="CP_FULL_NAME" HeaderText="CompanyNameHidden" SortExpression="CP_FULL_NAME" />
                    <asp:BoundField DataField="CP_ID" HeaderText="Sl.No" ReadOnly="True" SortExpression="CP_ID" >
                        <ItemStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Company Name" SortExpression="CP_FULL_NAME">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CP_FULL_NAME") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnCompanyName" ForeColor="#0066ff" runat="server" OnClick="lbtnCompanyName_Click"
                                Text='<%# Bind("CP_FULL_NAME") %>' CausesValidation="False"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="CP_SHORT_NAME" HeaderText="Company Short Name" SortExpression="CP_SHORT_NAME" >
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CP_ADDRESS" HeaderText="Address" SortExpression="CP_ADDRESS" >
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CP_CONTACT_NO1" HeaderText="Contact No" SortExpression="CP_CONTACT_NO1" >
                        <ItemStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CP_FAXNO" HeaderText="Fax No" ReadOnly="True" SortExpression="CP_FAXNO" />
                    <asp:BoundField DataField="CP_CONTACT_NO2" HeaderText="CON_NO2" SortExpression="CP_CONTACT_NO2" />
                    <asp:BoundField DataField="CP_EMAIL" HeaderText="EMAIL" SortExpression="CP_EMAIL" />
                    <asp:BoundField DataField="CP_TELEX_NO" HeaderText="TEL_NO" SortExpression="CP_TELEX_NO" />
                    <asp:BoundField DataField="CP_APGST_NO" HeaderText="APGST_NO" SortExpression="CP_APGST_NO" />
                    <asp:BoundField DataField="CP_CST_NO" HeaderText="CST_NO" ReadOnly="True" SortExpression="CP_CST_NO" />
                    <asp:BoundField DataField="CP_ECC_NO" HeaderText="ECC_NO" SortExpression="CP_ECC_NO" />
                    <asp:BoundField DataField="CP_VAT_NO" HeaderText="VAT_NO" SortExpression="CP_VAT_NO" />
                    <asp:BoundField DataField="CP_PAN_NO" HeaderText="PAN_NO" SortExpression="CP_PAN_NO" />
                    <asp:BoundField DataField="CP_EST_YEAR" HeaderText="EST_YEAR" SortExpression="CP_EST_YEAR" />
                    <asp:BoundField DataField="CP_CF_YEAR" HeaderText="CF_YEAR" ReadOnly="True" SortExpression="CP_CF_YEAR" />
                    <asp:BoundField DataField="CP_CPO_NO" HeaderText="CPO_NO" SortExpression="CP_CPO_NO" />
                    <asp:BoundField DataField="CP_CI_NO" HeaderText="CI_NO" SortExpression="CP_CI_NO" />
                    <asp:BoundField DataField="CP_CDC_NO" HeaderText="CDC_NO" SortExpression="CP_CDC_NO" />
                    <asp:BoundField DataField="CP_YEAR_STARTDATE" HeaderText="STARTDATE" SortExpression="CP_YEAR_STARTDATE" />
                    <asp:BoundField DataField="CP_YEAR_ENDDATE" HeaderText="ENDDATE" ReadOnly="True"
                        SortExpression="CP_YEAR_ENDDATE" />
                    <asp:BoundField DataField="CP_INVOICE_PREFIX" HeaderText="INVOICE_PREFIX" SortExpression="CP_INVOICE_PREFIX" />
                    <asp:BoundField DataField="CP_INVOICE_SUFFIX" HeaderText="INVOICE_SUFFIX" SortExpression="CP_INVOICE_SUFFIX" />
                    <asp:BoundField DataField="CP_PO_PREFIX" HeaderText="PO_PREFIX" SortExpression="CP_PO_PREFIX" />
                    <asp:BoundField DataField="CP_PO_SUFFIX" HeaderText="PO_SUFFIX" SortExpression="CP_PO_SUFFIX" />
                    <asp:BoundField DataField="CP_DC_PREFIX" HeaderText="DC_PREFIX" ReadOnly="True" SortExpression="CP_DC_PREFIX" />
                    <asp:BoundField DataField="CP_DC_SUFFIX" HeaderText="DC_SUFFIX" SortExpression="CP_DC_SUFFIX" />
                    <asp:TemplateField HeaderText="LOGO" SortExpression="CP_LOGO">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("CP_LOGO") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Image ID="Image" runat="server" Height="46px" ImageUrl="~/Images/noimage400x300.gif"
                                Width="67px" />
                            <asp:HiddenField ID="hfcplogo1" runat="server" Value='<%# Eval("CP_LOGO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="locid" HeaderText="Location" />
                    <asp:BoundField DataField="Technical_No" HeaderText="Technical_No" />
                    <asp:BoundField DataField="Despatch_No" HeaderText="Despatch_No" />
                    <asp:BoundField DataField ="Status" HeaderText ="Status" />
                </Columns>
                <EmptyDataTemplate>
                    No Record Found
                </EmptyDataTemplate>
                <SelectedRowStyle BackColor="LightSteelBlue" />
            </asp:GridView>
            <asp:SqlDataSource ID="sdsCompanyProfile" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                SelectCommand="SP_MASTER_COMPANYPROFILE_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName"
                        PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue"
                        PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            <br />
        </td>
    </tr>
    <tr>
        <td colspan="4" style="text-align: center">
            <table id="Table1" align="center">
                <tr>
                    <td>
                        <asp:Button ID="btnNew" runat="server" CausesValidation="False" OnClick="btnNew_Click"
                            Text="New" /></td>
                    <td>
                        <asp:Button ID="btnEdit" runat="server" CausesValidation="False" OnClick="btnEdit_Click"
                            Text="Edit" /></td>
                    <td>
                        <asp:Button ID="btnDelete" runat="server" CausesValidation="False" OnClick="btnDelete_Click"
                            Text="Delete" /></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="4" style="text-align: center;">
            <table id="tblCompanyDetails" runat="server" border="0" cellpadding="0" cellspacing="0"
                style="width: 100%" visible="false">
                <tr>
                    <td class="profilehead" colspan="2" style="text-align: left">
            General Details</td>
                    <td class="profilehead" colspan="1" style="text-align: left">
                    </td>
                    <td class="profilehead" colspan="1" style="text-align: left">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
            <asp:Label ID="lblCompanyFullName" runat="server" Text="Company Full Name : " ></asp:Label></td>
                    <td style="text-align: left">
            <asp:TextBox ID="txtFullName" runat="server" MaxLength="50"></asp:TextBox>
            <asp:Label ID="Label2" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
            <asp:RequiredFieldValidator
                ID="RFVFullName" runat="server" ControlToValidate="txtFullName" ErrorMessage="Please Enter the Company Full  Name">*</asp:RequiredFieldValidator>
                        <%--<asp:RegularExpressionValidator
                    ID="REVFullName" runat="server" ControlToValidate="txtFullName" ErrorMessage="Please Enter only Alphabets in Company  Full Name"
                    ValidationExpression="^[a-zA-Z. ]*$">*</asp:RegularExpressionValidator>--%>

                    </td>
                    <td style="text-align: right; width:142px;">
            <asp:Label ID="lblCompanyShortName" runat="server" Text="Company Short Name : "></asp:Label></td>
                    <td style="text-align: left; width:142px;">
            <asp:TextBox ID="txtShortName" runat="server" MaxLength="50"></asp:TextBox>
            <asp:Label ID="Label25" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
            <asp:RequiredFieldValidator
                ID="RFVShortName" runat="server" ControlToValidate="txtShortName" ErrorMessage="Please Enter the Company Short Name">*</asp:RequiredFieldValidator>
                       <%-- <asp:RegularExpressionValidator
                    ID="REVShortName" runat="server" ControlToValidate="txtShortName" ErrorMessage="Please Enter only Alphabets in Company Short Name"
                    ValidationExpression="^[a-zA-Z. ]*$">*</asp:RegularExpressionValidator>--%>

                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblAddress" runat="server" Text="Address : " ></asp:Label></td>
                    <td style="text-align: left">
            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
            <asp:Label ID="Label1" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
            <asp:RequiredFieldValidator
                ID="RFVAddress" runat="server" ControlToValidate="txtAddress" ErrorMessage="Please Enter the Address">*</asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlLocation1" runat="server" DataSourceID="locsds1" DataTextField="locname" DataValueField="locid">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="locsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT * FROM [location_tbl] ORDER BY [locname]"></asp:SqlDataSource>
                    </td>
                    <td style="text-align: right">
            <asp:Label ID="lblContactNo1" runat="server" Text="Contact No 1 : " ></asp:Label></td>
                    <td style="text-align: left">
            <asp:TextBox ID="txtContactNo1" runat="server" MaxLength="50"></asp:TextBox>
            <asp:Label ID="Label24" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
            <asp:RequiredFieldValidator
                ID="RFVContactNo" runat="server" ControlToValidate="txtContactNo1" ErrorMessage="Please Enter the Contact No1">*</asp:RequiredFieldValidator>
                        <%--<asp:RegularExpressionValidator
                    ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtContactNo1" ErrorMessage="Please Enter only Numbers"
                    ValidationExpression="^[0-9. ]*$">*</asp:RegularExpressionValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
            <asp:Label ID="lblFaxNo" runat="server" Text="Fax No : " ></asp:Label></td>
                    <td style="text-align: left">
            <asp:TextBox ID="txtFaxNo" runat="server" MaxLength="50"></asp:TextBox>
            <asp:RequiredFieldValidator
                ID="RFVFaxNo" runat="server" ControlToValidate="txtFaxNo" ErrorMessage="Please Enter the FAX No">*</asp:RequiredFieldValidator>
                         <%-- <asp:RegularExpressionValidator
                    ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtFaxNo" ErrorMessage="Please Enter only Numbers"
                    ValidationExpression="^[0-9. ]*$">*</asp:RegularExpressionValidator>--%>
                    </td>
                    <td style="text-align: right">
            <asp:Label ID="lblContactNo2" runat="server" Text="Contact No 2 : " ></asp:Label></td>
                    <td style="text-align: left">
            <asp:TextBox ID="txtContactNo2" runat="server" MaxLength="50"></asp:TextBox>
                        <%--<asp:RegularExpressionValidator
                    ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtContactNo2" ErrorMessage="Please Enter only Numbers"
                    ValidationExpression="^[0-9. ]*$">*</asp:RegularExpressionValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
            <asp:Label ID="lblEmail" runat="server" Text="Email : " ></asp:Label></td>
                    <td style="text-align: left">
            <asp:TextBox ID="txtEmail" runat="server" MaxLength="50"></asp:TextBox>
            <asp:Label ID="Label4" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
            <asp:RequiredFieldValidator ID="RFVEmail" runat="server" ControlToValidate="txtEmail"
                ErrorMessage="Please Enter the Email">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                    ID="REVEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Please Enter Email in Correct Format(Eg : abc@def.com)"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>

                    </td>
                    <td style="text-align: right">
            <asp:Label ID="lblTelexNo" runat="server" Text="Telex No : "></asp:Label></td>
                    <td style="text-align: left">
            <asp:TextBox ID="txtTelexNo" runat="server" MaxLength="50" Height="22px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RFVTelexNo" runat="server" ControlToValidate="txtTelexNo"
                ErrorMessage="Please Enter the Telex No">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="text-align: right">
            <asp:Label ID="lblAPGSTNo" runat="server" Text="APGST No : " ></asp:Label></td>
                    <td style="text-align: left">
            <asp:TextBox ID="txtAPGSTNo" runat="server" MaxLength="50"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RFVAPGSTNo" runat="server" ControlToValidate="txtAPGSTNo"
                ErrorMessage="Please Enter the APGST No">*</asp:RequiredFieldValidator></td>
                    <td style="text-align: right">
            <asp:Label ID="lblCSTNo" runat="server" Text="CST No : " ></asp:Label></td>
                    <td style="text-align: left">
            <asp:TextBox ID="txtCSTNo" runat="server" MaxLength="50"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RFVCSTNo" runat="server" ControlToValidate="txtCSTNo"
                ErrorMessage="Please Enter the CST No">*</asp:RequiredFieldValidator>
                        <%-- <asp:RegularExpressionValidator
                    ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtCSTNo" ErrorMessage="Please Enter only Numbers"
                    ValidationExpression="^[0-9. ]*$">*</asp:RegularExpressionValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
            <asp:Label ID="lblECCNo" runat="server" Text="ECC No : "></asp:Label></td>
                    <td style="text-align: left">
            <asp:TextBox ID="txtECCNo" runat="server" MaxLength="50"> </asp:TextBox></td>
                    <td style="text-align: right" >
            <asp:Label ID="lblVATNo" runat="server" Text="TIN No  : " ></asp:Label></td>
                    <td style="text-align: left">
            <asp:TextBox ID="txtVATNo" runat="server" MaxLength="50"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RFVVATNo" runat="server" ControlToValidate="txtVATNo"
                ErrorMessage="Please Enter the VAT No">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="text-align: right">
            <asp:Label ID="lblPANNo" runat="server" Text="PAN No : " ></asp:Label></td>
                    <td style="text-align: left">
            <asp:TextBox ID="txtPANNo" runat="server" MaxLength="50"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RFVPANNo" runat="server" ControlToValidate="txtPANNo"
                ErrorMessage="Please Enter the  PAN No">*</asp:RequiredFieldValidator></td>
                    <td style="text-align: right">
            <asp:Label ID="lblEstablishmentYear" runat="server" Text="Year of Establishment : "
               ></asp:Label></td>
                    <td style="text-align: left">
            <asp:TextBox ID="txtEstablishmentYear" runat="server" MaxLength="20"></asp:TextBox>
            <asp:RequiredFieldValidator
                ID="RFVEstYear" runat="server" ControlToValidate="txtEstablishmentYear" ErrorMessage="Please Enter the Year of Establishment">*</asp:RequiredFieldValidator>
            <cc1:FilteredTextBoxExtender ID="ftxteYearOfEstablishment" runat="server" FilterType="Numbers"
                TargetControlID="txtEstablishmentYear">
            </cc1:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td class="profilehead" colspan="4" style="text-align: left">
            Reference Details</td>
                </tr>
                <tr>
                    <td style="text-align: right">
            <asp:Label ID="lblCurrentFinancialYear" runat="server" Text="Current Financial Year : "
               ></asp:Label></td>
                    <td style="text-align: left">
            <asp:TextBox ID="txtCurrentFinancialYear" runat="server" MaxLength="5"></asp:TextBox>
            <asp:Label ID="Label8" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
            <asp:RequiredFieldValidator
                ID="RFVCFYear" runat="server" ControlToValidate="txtCurrentFinancialYear" ErrorMessage="Please Enter the the Current Financial Year">*</asp:RequiredFieldValidator><cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99-99" MaskType="Number"
                TargetControlID="txtCurrentFinancialYear" ClearMaskOnLostFocus="False">
                </cc1:MaskedEditExtender>
                    </td>
                    <td style="text-align: right">
            <asp:Label ID="lblCurrentPurchaseOrderNo" runat="server" Text="Current Purchase Order No : "
                Visible="False"></asp:Label></td>
                    <td style="text-align: left">
            <asp:TextBox ID="txtCurrentPurchaseOrderNo" runat="server" MaxLength="20" Visible="False"> </asp:TextBox>
            <asp:Label ID="Label19" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label>
            <asp:RequiredFieldValidator ID="RFVCPONo" runat="server" ControlToValidate="txtCurrentPurchaseOrderNo"
                ErrorMessage="Please Enter the Current Purchase Order No" Visible="False">*</asp:RequiredFieldValidator></td>
                </tr>

                <tr>
                    <td style="text-align: right">
            <asp:Label ID="lblTechNo" runat="server" Text="Tech Guidance No:"
               ></asp:Label></td>
                    <td style="text-align: left">
            <asp:TextBox ID="txtTechNo" runat="server" ></asp:TextBox>
            <asp:Label ID="Label5" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
            <asp:RequiredFieldValidator
                ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTechNo" ErrorMessage="Please Enter the Technical Guidance No">*</asp:RequiredFieldValidator>
                        <%--<cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99-99" MaskType="Number"
                TargetControlID="txtCurrentFinancialYear" ClearMaskOnLostFocus="False">
                </cc1:MaskedEditExtender>--%>
                    </td>
                    <td style="text-align: right">
            <asp:Label ID="lblDespatchNo" runat="server" Text="Despatch No : "
                Visible="true"></asp:Label></td>
                    <td style="text-align: left">
            <asp:TextBox ID="txtDespNo" runat="server" Visible="true"> </asp:TextBox>
            <asp:Label ID="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="true"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDespNo"
                ErrorMessage="Please Enter the Deapatchr No" Visible="False">*</asp:RequiredFieldValidator></td>
                </tr>

                <tr>
                    <td style="text-align: right;">
            <asp:Label ID="lblCurrentInvoiceNo" runat="server" Text="Current Invoice No : "  Visible="False"></asp:Label></td>
                    <td style="text-align: left;">
            <asp:TextBox ID="txtCurrentInvoiceNo" runat="server" Visible="False"> </asp:TextBox>
            <asp:Label ID="Label9" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label>
            <asp:RequiredFieldValidator
                ID="RFVCINo" runat="server" ControlToValidate="txtCurrentInvoiceNo" ErrorMessage="Please Enter the Current Invoice No" Visible="False">*</asp:RequiredFieldValidator></td>
                    <td style="text-align: right;">
            <asp:Label ID="lblCurrentDCNo" runat="server" Text="Current DC No : "  Visible="False"></asp:Label></td>
                    <td style="text-align: left;">
            <asp:TextBox ID="txtCurrentDCNo" runat="server" MaxLength="20" Visible="False"> </asp:TextBox>
            <asp:Label ID="Label18" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label>
            <asp:RequiredFieldValidator ID="RFVCDCNo" runat="server" ControlToValidate="txtCurrentDCNo"
                ErrorMessage="Please Enter the Current DC No" Visible="False">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="text-align: right">
            <asp:Label ID="lblYearStartDate" runat="server" Text="Year Start Date : "></asp:Label></td>
                    <td style="text-align: left">
            <asp:TextBox ID="txtYearStartDate" runat="server" type="datepic"></asp:TextBox><asp:Label ID="Label10" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*" ></asp:Label><asp:RequiredFieldValidator
                    ID="RFVYearStartDate" runat="server" ControlToValidate="txtYearStartDate" ErrorMessage="Please Select the Year Start Date" >*</asp:RequiredFieldValidator>
                        </td>
                    <td style="text-align: right">
            <asp:Label ID="lblYearEndDate" runat="server" Text="Year End Date : "></asp:Label></td>
                    <td style="text-align: left">
            <asp:TextBox ID="txtYearEndDate" runat="server" type="datepic">
            </asp:TextBox>
            <asp:Label ID="Label17" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
            <asp:RequiredFieldValidator ID="RFVYearEndDate" runat="server" ControlToValidate="txtYearEndDate"
                ErrorMessage="Please Select the Year End Date">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
            <asp:Label ID="lblInvoicePrefix" runat="server" Text="Invoice Prefix : "  Visible="False"></asp:Label></td>
                    <td style="text-align: left">
            <asp:TextBox ID="txtInvoicePrefix" runat="server" MaxLength="20" Visible="False"> </asp:TextBox>
            <asp:Label ID="Label11" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label>
            <asp:RequiredFieldValidator
                ID="RFVInvoicePrefix" runat="server" ControlToValidate="txtInvoicePrefix" ErrorMessage="Please Enter the Invoice Prefix" Visible="False">*</asp:RequiredFieldValidator></td>
                    <td style="text-align: right">
            <asp:Label ID="lblInvoiceSuffix" runat="server" Text="Invoice Suffix : "  Visible="False"></asp:Label></td>
                    <td style="text-align: left">
            <asp:TextBox ID="txtInvoiceSuffix" runat="server" MaxLength="20" Visible="False"> </asp:TextBox>
            <asp:Label ID="Label16" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label>
            <asp:RequiredFieldValidator ID="RFVInvoiceSuffix" runat="server" ControlToValidate="txtInvoiceSuffix"
                ErrorMessage="Please Enter the Invoice Suffix" Visible="False">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="text-align: right">
            <asp:Label ID="lblPOPrefix" runat="server" Text="PO Prefix : "  Visible="False"></asp:Label></td>
                    <td style="text-align: left">
            <asp:TextBox ID="txtPOPrefix" runat="server" MaxLength="20" Visible="False"> </asp:TextBox>
            <asp:Label ID="Label12" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label>
            <asp:RequiredFieldValidator ID="RFVPOPrefix" runat="server" ControlToValidate="txtPOPrefix"
                ErrorMessage="Please Enter the PO Prefix" Visible="False">*</asp:RequiredFieldValidator></td>
                    <td style="text-align: right">
            <asp:Label ID="lblPOSuffix" runat="server" Text="PO Suffix : " Visible="False"></asp:Label></td>
                    <td style="text-align: left">
            <asp:TextBox ID="txtPOSuffix" runat="server" MaxLength="20" Visible="False"> </asp:TextBox>
            <asp:Label ID="Label15" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label>
            <asp:RequiredFieldValidator ID="RFVPOSuffix" runat="server" ControlToValidate="txtPOSuffix"
                ErrorMessage="Please Enter the PO Suffix" Visible="False">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="text-align: right">
            <asp:Label ID="lblDCPrefix" runat="server" Text="DC Prefix : "  Visible="False"></asp:Label></td>
                    <td style="text-align: left">
            <asp:TextBox ID="txtDCPrefix" runat="server" MaxLength="20" Visible="False"> </asp:TextBox>
            <asp:Label ID="Label13" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label>
            <asp:RequiredFieldValidator
                ID="RFVDCPrefix" runat="server" ControlToValidate="txtDCPrefix" ErrorMessage="Please Enter the DC Prefix" Visible="False">*</asp:RequiredFieldValidator>

                    </td>
                    <td style="text-align: right">
            <asp:Label ID="lblDCSuffix" runat="server" Text="DC Suffix : "  Visible="False"></asp:Label>

                    </td>
                    <td style="text-align: left">
            <asp:TextBox ID="txtDCSuffix" runat="server" MaxLength="20" Visible="False"> </asp:TextBox>
            <asp:Label ID="Label14" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label>
            <asp:RequiredFieldValidator ID="RFVDCSuffix" runat="server" ControlToValidate="txtDCSuffix"
                ErrorMessage="Please Enter the DC Suffix" Visible="False">*</asp:RequiredFieldValidator>

                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                    </td>
                    <td style="text-align: left;">
                    </td>
                    <td style="text-align: right;">
            <asp:Label ID="Label26" runat="server" CssClass="label" Text="Logo : " ></asp:Label></td>
                    <td style="text-align: left;">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/noimage400x300.gif" Width="100px"
                /><asp:Button ID="btnUpload" runat="server" CausesValidation="False" OnClientClick="window.open('../Masters/ImageUpload.aspx','resume','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')"
                            Text="Upload" OnClick="btnUpload_Click" Visible="False" />
                        <br />
                        <asp:HiddenField ID="hfpicname1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; height: 24px;">
                                    <asp:Label ID="Label39" runat="server" Text="Status :"></asp:Label></td>
                                <td style="text-align: left; height: 24px;">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:RadioButton ID="rdbActive" runat="server" Text="Active" GroupName="abc"  Checked="True"></asp:RadioButton>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="rdbInactive" runat="server" Text="Inactive" GroupName="abc" ></asp:RadioButton></td>
                                        </tr>
                                    </table>
                                </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        &nbsp;</td>
                    <td style="text-align: left;">
                        &nbsp;</td>
                    <td style="text-align: right;">
                        &nbsp;</td>
                    <td style="text-align: left;">
                        <asp:FileUpload ID="FileUp1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center">
                        <table id="Table2" align="center">
                            <tr>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" /></td>
                                <td>
                                    <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" OnClick="btnRefresh_Click"
                                        Text="Refresh" /></td>
                                <td>
                                    <asp:Button ID="btnClose" runat="server" CausesValidation="False" OnClick="btnClose_Click"
                                        Text="Close" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center">
                    </td>
                </tr>
            </table>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server"></asp:ValidationSummary>
        </td>
    </tr>
    <tr>
        <td >
        </td>
        <td >
        </td>
        <td >
        </td>
        <td >
        </td>
    </tr>
</table>
