<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/PurchaseMP1.master" AutoEventWireup="true" CodeFile="SuppliersEnquiryNew.aspx.cs" Inherits="Modules_SCM_SuppliersEnquiryNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td style="text-align:left">suppliers enquiry</td>
        </tr>
    </table>

  
    <table border="0" cellpadding="0" style="width:100%" cellspacing="0" id="tblSupEnqDetails" runat="server" visible="true">
        <tr>
            <td class="profilehead" colspan="4" style="text-align: left">
                <span>Supplier Enquiry Details</span></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label3" runat="server" Text="Enquiry No"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtEnquiryNo" runat="server" ReadOnly="True"></asp:TextBox></td>
            <td style="text-align: right">
                <asp:Label ID="Label4" runat="server" Text="Enquiry Date"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtEnquiryDate" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imgEnquiryDate"
                    runat="server" ImageUrl="~/Images/Calendar.png" />
                <asp:Label ID="Label11" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEnquiryDate"
                        ErrorMessage="Please Select the Enquiry Date">*</asp:RequiredFieldValidator><cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceEnquiryDate"
                            runat="server" Enabled="True" PopupButtonID="imgEnquiryDate" TargetControlID="txtEnquiryDate">
                        </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="MaskedEditEnquiryDate" runat="server" DisplayMoney="Left"
                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtEnquiryDate"
                    UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="lblOriginatedBy" runat="server" Text="Enquiry Originated By" Visible="False"></asp:Label></td>
            <td style="text-align: left">
                <asp:RadioButton ID="rdoDirectSupp" runat="server" GroupName="rbtOriginatedBy" Text="Direct Supplier" Checked="True" Visible="False" />
                <asp:RadioButton ID="rdoConsult" runat="server" GroupName="rbtOriginatedBy" Text="Consultancy" Visible="False" /></td>
            <td style="text-align: right"></td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlOriginatedBy" runat="server" AutoPostBack="True" Visible="False">
                    <asp:ListItem Value="0">--</asp:ListItem>
                    <asp:ListItem>DirectSupplier</asp:ListItem>
                    <asp:ListItem>Consultancy</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="text-align: right"></td>
            <td style="text-align: left"></td>
            <td style="text-align: right">
                <asp:Label ID="lblEnquiryStatus" runat="server" Text="Enquiry Status" Visible="False"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtEnquiryStatus" runat="server" Visible="False"></asp:TextBox>
                <asp:Label ID="Label12" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtEnquiryStatus"
                        ErrorMessage="Please Enter Enquiry Status" Visible="False">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="lblCriteria" runat="server" Text="Employee Name"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlCriteria" runat="server">
                </asp:DropDownList>
                <asp:Label ID="Label1" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlCriteria"
                        ErrorMessage="Please Select the Employee Name" InitialValue="0">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right">
                <asp:Label ID="lblEnquiryDueDate" runat="server" Text="Enquiry Due Date"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtEnquiryDueDate" runat="server">
                </asp:TextBox>&nbsp;<asp:Image ID="imgEnquiryDueDate" runat="server" ImageUrl="~/Images/Calendar.png" />
                <asp:Label ID="Label13" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEnquiryDueDate"
                        ErrorMessage="Please Select the Enquiry Due Date">*</asp:RequiredFieldValidator>
                <cc1:CalendarExtender Format="dd/MM/yyyy"
                    ID="ceEnquiryDueDate" runat="server" Enabled="True" PopupButtonID="imgEnquiryDueDate"
                    TargetControlID="txtEnquiryDueDate">
                </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="MaskededitEnquiryDueDate" runat="server" DisplayMoney="Left"
                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtEnquiryDueDate"
                    UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label23" runat="server" Text="Brand"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
                </asp:DropDownList></td>
            <td style="text-align: right">
                <asp:Label ID="Label2" runat="server" Text="Delivery Type"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlDeliveryType" runat="server">
                </asp:DropDownList>
                <asp:Label ID="Label15" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlDeliveryType"
                        ErrorMessage="Please Select the Delivery type" InitialValue="0">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="lblIndentApprovalNo" runat="server" Text="Indent  No" Visible="False"></asp:Label>
            </td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlIndentApprovel" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlIndentApprovel_SelectedIndexChanged" Visible="False">
                </asp:DropDownList>&nbsp;<asp:Label ID="Label22" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td class="profilehead" colspan="4" style="text-align: left">Supplier Details</td>
        </tr>
        <tr>
            <td style="height: 19px; text-align: right"></td>
            <td style="height: 19px; text-align: left"></td>
            <td style="height: 19px; text-align: right"></td>
            <td style="height: 19px; text-align: left"></td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label ID="lblCustomer" runat="server" Text="Supplier Name"></asp:Label></td>
            <td style="text-align: left;">
                <asp:DropDownList ID="ddlSupplierName" runat="server" OnSelectedIndexChanged="ddlSupplierName_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
                <asp:Label ID="Label10" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlSupplierName"
                    ErrorMessage="Please Select the Supplier Name" ValidationGroup="s" InitialValue="0">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right;">
                <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtContactPerson" runat="server" ReadOnly="True"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" ReadOnly="True"></asp:TextBox></td>
            <td style="text-align: right">
                <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtPhoneNo" runat="server" ReadOnly="True"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True"></asp:TextBox></td>
            <td style="text-align: right;">
                <asp:Label ID="lblCity" runat="server" Text="Mobile No"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td style="text-align: right">
                <asp:Button ID="btnSuppDetails" runat="server" BackColor="Transparent" BorderStyle="None"
                    CssClass="loginbutton" EnableTheming="False" Text="Add" OnClick="btnSuppDetails_Click" ValidationGroup="s" /></td>
            <td style="text-align: left">
                <asp:Button ID="btnSuppRefresh" runat="server" BackColor="Transparent" BorderStyle="None"
                    CssClass="loginbutton" EnableTheming="False" Text="Refresh" OnClick="btnSuppRefresh_Click" CausesValidation="False" /></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td class="profilehead" colspan="4" style="text-align: left">Supplier Contact Person Details</td>
        </tr>
        <tr>
            <td runat="server" colspan="4">
                <asp:GridView ID="gvSupplierDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSupplierDetails_RowDataBound" OnRowDeleting="gvSupplierDetails_RowDeleting" Width="100%">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                        <asp:BoundField DataField="SuppId" HeaderText="SuppIdHidden"></asp:BoundField>
                        <asp:BoundField DataField="Name" HeaderText="Name">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PhoneNo" HeaderText="Phone No"></asp:BoundField>
                        <asp:BoundField DataField="Email" HeaderText="Email">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        <span style="color: #ff0000">No Data to Display </span>
                    </EmptyDataTemplate>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td class="profilehead" colspan="4">Indent Approved Items
            </td>
        </tr>
        <tr>
            <td colspan="4"></td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvItem" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvItem_RowDataBound">
                    <Columns>
                        <asp:CommandField ShowEditButton="True"></asp:CommandField>
                        <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                        <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ItemName" HeaderText="Model No">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ItemType" HeaderText="Model Name"></asp:BoundField>
                        <asp:BoundField DataField="UOM" HeaderText="UOM">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Priority" HeaderText="Priority"></asp:BoundField>
                        <asp:BoundField DataField="Brand" HeaderText="Brand"></asp:BoundField>
                        <asp:BoundField DataField="ReqFor" HeaderText="Requried for">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ReqFor" HeaderText="Room">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:MM/dd/yyyy}" DataField="ReqDate" HeaderText="Required by Date"></asp:BoundField>
                        <asp:BoundField DataField="Specification" HeaderText="Specification">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Priority" HeaderText="Priority"></asp:BoundField>
                        <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                        <asp:BoundField DataField="ColorId" HeaderText="ColorId"></asp:BoundField>
                        <asp:BoundField DataField="Indetid" HeaderText="Indetid"></asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chk" runat="server" __designer:wfdid="w8"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <span style="color: #ff0033">No Data to Display</span>
                    </EmptyDataTemplate>
                </asp:GridView>
                <asp:Button ID="btnGo" runat="server" CausesValidation="False" OnClick="btnGo_Click"
                    Text="Go" /></td>
        </tr>
        <tr>
            <td class="profilehead" colspan="4" style="text-align: left; height: 14px;">Interested Product</td>
        </tr>
        <tr>
            <td style="text-align: right"></td>
            <td style="text-align: left"></td>
            <td style="text-align: right"></td>
            <td style="text-align: left"></td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: right">
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="gvItemDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvItemDetails_RowDataBound"
                                OnRowDeleted="gvItemDetails_RowDeleted" OnRowDeleting="gvItemDetails_RowDeleting"
                                OnRowEditing="gvItemDetails_RowEditing">
                                <Columns>
                                    <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Model No">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ItemType" HeaderText="Model Name"></asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Priority" HeaderText="Priority"></asp:BoundField>
                                    <asp:BoundField DataField="Brand" HeaderText="Brand"></asp:BoundField>
                                    <asp:BoundField DataField="ReqFor" HeaderText="Room">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Specification" HeaderText="Specification">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Priority" HeaderText="Priority"></asp:BoundField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No Data to Display
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label ID="Label5" runat="server" Text="Model No" Visible="False"></asp:Label></td>
            <td style="text-align: left;">
                <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged" Visible="False">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlItemType"
                    ErrorMessage="Please Select the No" InitialValue="0" ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right;">
                <asp:Label ID="Label6" runat="server" Text="Model Name" Visible="False"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtModelName" runat="server" Visible="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label16" runat="server" Text="Item Category" Visible="False"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtCategory" runat="server" Visible="False"></asp:TextBox></td>
            <td style="text-align: right">
                <asp:Label ID="Label17" runat="server" Text="Item SubCategory" Visible="False"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtItemSubCategory" runat="server" Visible="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label18" runat="server" Text="Color" Visible="False"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtColor" runat="server" Visible="False"></asp:TextBox></td>
            <td style="text-align: right">
                <asp:Label ID="Label19" runat="server" Text="Brand" Visible="False"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtBrand" runat="server" Visible="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label ID="Label8" runat="server" Text="UOM" Visible="False"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtUOM" runat="server" ReadOnly="True" Visible="False"></asp:TextBox></td>
            <td style="text-align: right;">
                <asp:Label ID="Label7" runat="server" Text="Quantity" Visible="False"></asp:Label></td>
            <td style="text-align: left;" valign="bottom">
                <asp:TextBox ID="txtQuantity" runat="server" Visible="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQuantity"
                    ErrorMessage="Please Enter the Quantity" ValidationGroup="ip" Visible="False">*</asp:RequiredFieldValidator><br />
                <cc1:FilteredTextBoxExtender ID="ftxteQuantity" runat="server" FilterType="Numbers"
                    TargetControlID="txtQuantity">
                </cc1:FilteredTextBoxExtender>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="lblForDate" runat="server" Text="Required by Date" Visible="False"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtReqByDate" runat="server" Visible="False"></asp:TextBox><asp:Image ID="imgReqByDate" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False"></asp:Image><asp:Label
                    ID="Label20" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtReqByDate"
                        ErrorMessage="Please Select Required By Date" ValidationGroup="id" Visible="False">*</asp:RequiredFieldValidator><asp:CustomValidator
                            ID="CustomValidator5" runat="server" ClientValidationFunction="DateCustomValidate"
                            ControlToValidate="txtReqByDate" ErrorMessage="Please Enter the Required By Date in DD/MM/YYYY Format or Check  Year in 2009 to 2099 Range or not"
                            SetFocusOnError="True" ValidationGroup="id" Visible="False">*</asp:CustomValidator><cc1:CalendarExtender
                                ID="ceReqByDate" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgReqByDate"
                                TargetControlID="txtReqByDate">
                            </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="MaskededitReqByDate" runat="server" DisplayMoney="Left"
                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtReqByDate"
                    UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
            </td>
            <td style="text-align: right">
                <asp:Label ID="lblPriority" runat="server" Text="Priority" Visible="False"></asp:Label>

                <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1" Visible="False"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlItemPriority" runat="server" Visible="False">
                    <asp:ListItem Value="0">--</asp:ListItem>
                    <asp:ListItem>Low</asp:ListItem>
                    <asp:ListItem>Medium</asp:ListItem>
                    <asp:ListItem>High</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label21" runat="server" Text="Item Image" Visible="False"></asp:Label></td>
            <td style="text-align: left">
                <asp:Image ID="Image1" runat="server" Height="85px" ImageUrl="~/Images/noimage400x300.gif"
                    Visible="False"></asp:Image></td>
            <td style="text-align: right">
                <asp:Label ID="Label9" runat="server" Text="Specifications" Visible="False"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtSpecifications" runat="server" TextMode="MultiLine" ReadOnly="True" Visible="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right"></td>
            <td style="text-align: right">
                <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                    CssClass="loginbutton" EnableTheming="False" Text="Add" OnClick="btnAdd_Click" ValidationGroup="ip" Visible="False" /></td>
            <td style="text-align: left">
                <asp:Button ID="btnIntrstProduct" runat="server" BackColor="Transparent" BorderStyle="None"
                    CssClass="loginbutton" EnableTheming="False" Text="Refresh" OnClick="btnIntrstProduct_Click" CausesValidation="False" Visible="False" /></td>
            <td style="text-align: left"></td>
        </tr>
        <tr>
            <td class="profilehead" colspan="4">Reference Details</td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                </asp:DropDownList></td>
            <td style="text-align: right">
                <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlApprovedBy" runat="server" Enabled="False">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                    Visible="False"></asp:Label><asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label><asp:Label ID="Label14" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                        Visible="False"></asp:Label>
                <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td colspan="4">
                <table id="tblButtons" align="center">
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                        <td>
                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" CausesValidation="False" /></td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" CausesValidation="False" />
                            <asp:Button ID="btnPrint" runat="server" Text="Print" CausesValidation="False" OnClick="btnPrint_Click" />

                        </td>
                        <td>
                            <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
 <asp:ValidationSummary ID="ValidationSummary1" runat="server"></asp:ValidationSummary>
 <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="ip"></asp:ValidationSummary>
 <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="s"></asp:ValidationSummary>

  
</asp:Content>


 
