<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/PurchaseMP1.master" AutoEventWireup="true" CodeFile="SupplierQuation.aspx.cs" Inherits="Modules_SCM_SupplierQuation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 25px;
            width: 206px;
        }

        .auto-style2 {
            height: 28px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" id="tblIndentApprovalDetails" runat="server"
        visible="true" width="100%">

        <tr>
            <td colspan="4" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" id="Table2" runat="server" visible="true"
                    width="100%">
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">Indent Approval Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left; width: 324px;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblIndentApprovalNo" runat="server" Text="Indent Approval  No :" Width="128px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtApprovalNo" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblApprovalDate" runat="server" Text="Indent Approval Date :" Width="139px"></asp:Label></td>
                        <td style="text-align: left; width: 324px;">
                            <asp:TextBox ID="txtIndentApprovalDate" type="datepic" runat="server">
                            </asp:TextBox><%--<asp:Image ID="imgIndentApprovalDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>&nbsp;&nbsp;&nbsp;
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="CeIndentApprovalDate" runat="server"
                                            Enabled="True" PopupButtonID="imgIndentApprovalDate" TargetControlID="txtIndentApprovalDate">
                                        </cc1:CalendarExtender>--%>
                            <%-- <cc1:MaskedEditExtender ID="MaskedEditIndentApprovalDate" runat="server" DisplayMoney="Left"
                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtIndentApprovalDate"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblDepartment" runat="server" Text="Department :" Width="103px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlDepart" runat="server">
                            </asp:DropDownList><asp:Label ID="Label3" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlDepart"
                                    ErrorMessage="Please Select the Department" InitialValue="0" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblFollowUp" runat="server" Text="Employee Name :" Width="111px"></asp:Label></td>
                        <td style="text-align: left; width: 324px;">
                            <asp:DropDownList ID="ddlFollowUp" runat="server">
                                <asp:ListItem>--</asp:ListItem>
                                <asp:ListItem>sdfsf</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="Label4" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlFollowUp"
                                ErrorMessage="Please Select the Follow Up" InitialValue="0" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left; width: 324px;"></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: left" class="profilehead">&nbsp;Item Details</td>
        </tr>
        <tr>
            <td style="text-align: right; height: 19px;"></td>
            <td style="text-align: left; height: 19px;"></td>
            <td style="text-align: right; height: 19px; width: 157px;"></td>
            <td style="text-align: left; height: 19px;"></td>
        </tr>
        <%-- <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblPriority" runat="server" Text="Priority" Width="65px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlItemPriority" runat="server">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem>Low</asp:ListItem>
                                <asp:ListItem>Medium</asp:ListItem>
                                <asp:ListItem>High</asp:ListItem>
                            </asp:DropDownList><asp:Label ID="Label10" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label></td>
                        <td style="text-align: right; width: 157px;">
                            <asp:Label ID="lblSpecification" runat="server" Text="Specification"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtSpecification" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="47%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblForDate" runat="server" Text="Required by Date"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtReqByDate" runat="server"></asp:TextBox><asp:Image ID="imgReqByDate"
                                runat="server" ImageUrl="~/Images/Calendar.png" /><asp:Label ID="Label11" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtReqByDate"
                                ErrorMessage="Please Select Required By Date" ValidationGroup="id">*</asp:RequiredFieldValidator><asp:CustomValidator
                                    id="CustomValidator5" runat="server" ClientValidationFunction="DateCustomValidate"
                                    ControlToValidate="txtReqByDate" ErrorMessage="Please Enter the Required By Date in DD/MM/YYYY Format or Check  Year in 2009 to 2099 Range or not"
                                    SetFocusOnError="True" ValidationGroup="id">*</asp:CustomValidator><cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceReqByDate" runat="server" Enabled="True"
                                PopupButtonID="imgReqByDate" TargetControlID="txtReqByDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskededitReqByDate" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtReqByDate"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                            &nbsp;</td>
                        <td style="text-align: right; width: 157px;">
                            <asp:Label ID="Label12" runat="server" Text="Requried For"></asp:Label>
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList id="ddlSuppliers" runat="server">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtSupplierName" runat="server" ReadOnly="True" Visible="False"></asp:TextBox></td>
                    </tr>--%>
        <tr>
            <td style="text-align: right"></td>
            <td style="text-align: left;"></td>
            <td style="text-align: right; width: 157px;"></td>
            <td style="text-align: left;"></td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvApprlItemDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvApprlItemDetails_RowDataBound" Width="100%" OnRowDeleting="gvApprlItemDetails_RowDeleting">
                    <Columns>
                        <%--<asp:BoundField DataField="ReqFor" HeaderText="Room">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:/MM/dd/yyyy}" DataField="ReqDate" SortExpression="ReqDate" HeaderText="Required by Date"></asp:BoundField>
<asp:BoundField DataField="Specification" HeaderText="Specification">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Priority" HeaderText="Priority"></asp:BoundField>
<asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
<asp:BoundField DataField="ColorId" HeaderText="ColorId"></asp:BoundField>--%>
                        <asp:CommandField ShowEditButton="True"></asp:CommandField>
                        <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                        <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="Item Model No">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="IT_TYPE" HeaderText="Item Type" />
                        <asp:BoundField DataField="IND_ID" HeaderText="Indent Id">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="UOM_SHORT_DESC" HeaderText="UOM" />
                        <%--<asp:BoundField DataField="IND_DET_QTY" HeaderText="Item Quantity"></asp:BoundField>--%>
                        <asp:TemplateField HeaderText="Quantity" ControlStyle-Width="80px">
                            <ItemTemplate>
                                <asp:TextBox ID="txtQuantity" runat="server" Text='<%#Bind("IND_DET_QTY") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="IND_DET_BRAND" HeaderText="Item Brand"></asp:BoundField>
                        <asp:BoundField DataField="IND_DET_REQ_FOR" HeaderText="Required For"></asp:BoundField>
                        <asp:BoundField DataField="COLOUR_NAME" HeaderText="Color">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>

                        <%-- <asp:BoundField DataField="COLOR_ID" HeaderText="ColorId">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>--%>
                        <%--<asp:BoundField DataField="IND_DET_ID" HeaderText="Indentdet Id">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>--%>
                        <asp:BoundField DataField="IND_DET_ID" HeaderText="Color Id"></asp:BoundField>
                        <asp:BoundField DataField="COLOR_ID" HeaderText="Indentdet Id"></asp:BoundField>

                        <asp:BoundField DataField="IND_DET_REM_QTY" HeaderText="Enq Qty"></asp:BoundField>
                        <asp:BoundField DataField="IND_DET_ORD_QTY" HeaderText="OrdQty"></asp:BoundField>

                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="select a.ITEM_CODE,c.ITEM_NAME,g.IND_ID,a.IND_DET_QTY,a.IND_DET_BRAND,a.IND_DET_SUGG_PARTY,a.IND_DET_REQ_FOR,a.IND_DET_SPECS,
b.COLOUR_NAME,a.IND_DET_ID,c.ITEM_MODEL_NO,a.IND_DET_STATUS,a.COLOR_ID,d.IT_TYPE,e.UOM_SHORT_DESC,g.Ind_Det_Id,f.IND_DATE,a.IND_DET_REQ_BY_DATE,a.IND_DET_REM_QTY,a.IND_DET_ORD_QTY from
IND_DET_ITEMS as g inner join 
 dbo.YANTRA_INDENT_DET as a on a.IND_DET_ID=g.Ind_Det_Id
 inner join 
 dbo.YANTRA_LKUP_COLOR_MAST as b on a.COLOR_ID=b.COLOUR_ID inner join
 dbo.YANTRA_ITEM_MAST as c on a.ITEM_CODE=c.ITEM_CODE inner join
 dbo.YANTRA_LKUP_ITEM_TYPE as d on c.IT_TYPE_ID=d.IT_TYPE_ID inner join
 dbo.YANTRA_LKUP_UOM e on c.UOM_ID=e.UOM_ID inner join
 YANTRA_INDENT_MAST f on  a.IND_ID = f.IND_ID
where a.IND_DET_STATUS='New'"></asp:SqlDataSource>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table style="width: 100%">
                    <tr>
                        <td id="TD16" class="profilehead" colspan="4" style="text-align: left">Supplier Details</td>
                    </tr>
                    <tr>
                        <td id="TD5" style="text-align: right" class="auto-style1"></td>
                        <td style="text-align: left" class="auto-style1"></td>
                        <td style="text-align: right" class="auto-style1"></td>
                        <td style="text-align: left" class="auto-style1"></td>
                    </tr>
                    <tr>
                        <td id="TD18" style="text-align: right;">
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
                        <td id="TD28" style="text-align: right">
                            <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No" Width="74px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPhoneNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td id="TD2" style="text-align: right;">
                            <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblCity" runat="server" Text="Mobile No" Width="81px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td id="TD36"></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="auto-style2"></td>
                        <td style="text-align: right" class="auto-style2">
                            <asp:Button ID="btnSuppDetails" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" Text="Add" OnClick="btnSuppDetails_Click" ValidationGroup="s" Visible="False" /></td>
                        <td style="text-align: left" class="auto-style2">
                            <asp:Button ID="btnSuppRefresh" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" Text="Refresh" OnClick="btnSuppRefresh_Click" CausesValidation="False" Visible="False" /></td>
                        <td class="auto-style2"></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td id="TD17" class="profilehead" colspan="4" style="text-align: left">&nbsp;</td>
                    </tr>
                    <tr>
                        <td id="TD6" runat="server" colspan="4">
                            <asp:GridView ID="gvSupplierDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSupplierDetails_RowDataBound" OnRowDeleting="gvSupplierDetails_RowDeleting" Width="100%" Visible="False">
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
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: left" class="profilehead">Reference Details</td>
        </tr>
        <tr>
            <td style="text-align: right"></td>
            <td style="text-align: left;"></td>
            <td style="text-align: right; width: 157px;"></td>
            <td style="text-align: left;"></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
            <td style="text-align: left;">
                <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                    <asp:ListItem>--</asp:ListItem>
                    <asp:ListItem>werwer</asp:ListItem>
                    <asp:ListItem></asp:ListItem>
                </asp:DropDownList></td>
            <td style="text-align: right; width: 157px;">
                <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By" Width="96px" Visible="False"></asp:Label></td>
            <td style="text-align: left;">
                <asp:DropDownList ID="ddlApprovedBy" runat="server" Enabled="False" Visible="False">
                    <asp:ListItem>--</asp:ListItem>
                    <asp:ListItem>abc</asp:ListItem>
                    <asp:ListItem></asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="text-align: right; height: 19px;"></td>
            <td style="text-align: left; height: 19px;"></td>
            <td style="text-align: right; height: 19px; width: 157px;"></td>
            <td style="text-align: left; height: 19px;"></td>
        </tr>
        <tr>
            <td colspan="4">
                <table id="tblButtons" align="center">
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="a" /></td>
                        <td>
                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click"
                                CausesValidation="False" /></td>
                        <td>
                            <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" CausesValidation="False" /></td>
                        <td>
                            <asp:Button ID="btnPrint" runat="server" Text="Print" CausesValidation="False" Visible="False" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False"></asp:ValidationSummary>
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="s"></asp:ValidationSummary>
    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="a"></asp:ValidationSummary>

    <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1" Visible="False"></asp:Label>
    <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1" Visible="False"></asp:Label>
    <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
</asp:Content>


 
