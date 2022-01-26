<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/InventoryMP1.master" AutoEventWireup="true" CodeFile="CheckingFormatDetails2.aspx.cs" Inherits="Modules_SCM_CheckingFormatDetails2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--  <script>
        $(function () {
            $("[name$='txtCheckingDate'],[name$='txtSupplierDate'],[name$='txtReceivedOn']").datepicker();
        });
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <span class="subh1">General Details</span>
    <table cellpadding="3" cellspacing="3" id="tblCheckDetails" runat="server"
        visible="false" width="100%">

        <tr>
            <td style="text-align: right; height: 20px;"></td>
            <td style="text-align: left; height: 20px;">
                <table class="stacktable">
                    <tr>
                        <td class="auto-style1">
                            <asp:RadioButton ID="rdbWithPo" runat="server" AutoPostBack="True"
                                GroupName="as" OnCheckedChanged="rdbWithPo_CheckedChanged" Text="With PO"></asp:RadioButton>
                        </td>
                        <td>
                            <asp:RadioButton ID="rdbWithoutPo" runat="server" AutoPostBack="True" GroupName="as"
                                OnCheckedChanged="rdbWithoutPo_CheckedChanged" Text="WithOut PO" Checked="True"></asp:RadioButton>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="text-align: right; height: 20px;"></td>
            <td style="text-align: left; height: 20px;"></td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="GridView2" runat="server"></asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label ID="lblCkNo" runat="server" Text="MRN REF No"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtCkNo" runat="server">
                </asp:TextBox></td>
            <td style="text-align: right;">
                <asp:Label ID="lblDate" runat="server" Text="MRN Date"></asp:Label></td>
            <td style="text-align: left;">&nbsp;<asp:TextBox ID="txtCheckingDate" runat="server" type="date">
            </asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCheckingDate"
                    ErrorMessage="Please Select the Date" SetFocusOnError="True">*</asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="lblPoNo" runat="server" Text="PO NO . SLPL"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlPONO" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPONO_SelectedIndexChanged">
                </asp:DropDownList><asp:TextBox ID="txtSlpl" runat="server" Visible="False"></asp:TextBox><asp:TextBox
                    ID="txtwithoutpono" runat="server" Visible="False"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlPONO" ErrorMessage="Please Select the PO No."
                    InitialValue="0" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right">
                <asp:Label ID="lblOrderDate" runat="server" Text="PO Date"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtPODate" runat="server" ReadOnly="True">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="lblSupplierName" runat="server" Text="Supplier Name"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlSupplierName" runat="server" Width="207px">
                </asp:DropDownList></td>
            <td style="text-align: right">
                <asp:Label ID="Label14" runat="server" Text="Company Name"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlCompanyName" runat="server" OnSelectedIndexChanged="ddlCompanyName_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ControlToValidate="ddlCompanyName" ErrorMessage="Please Select the Company Name"
                    InitialValue="0" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="lblSupplier" runat="server" Text="Supplier Invoice No/DC"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlSupInvNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSupInvNo_SelectedIndexChanged">
                </asp:DropDownList><asp:TextBox ID="txtSupplier" runat="server" Visible="False"></asp:TextBox><asp:TextBox
                    ID="txtSuplierInvoiceNowithoutpo" runat="server" Visible="False"></asp:TextBox></td>
            <td style="text-align: right">
                <asp:Label ID="lblSupplierDate" runat="server" Text="Supplier Invoice Date" Width="138px">
                </asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtSupplierDate" runat="server" type="date"></asp:TextBox>
                <br />

            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label10" runat="server" Text="MRN No"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtGatepass" runat="server">
                </asp:TextBox></td>
            <td style="text-align: right">
                <asp:Label ID="Label11" runat="server" Text="L.R. No"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtlrno" runat="server">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right; height: 79px;"></td>
            <td style="text-align: left; height: 79px;"></td>
            <td style="text-align: right; height: 79px;">
                <asp:Label ID="lblReceived" runat="server" Text="Received On" Width="86px"></asp:Label></td>
            <td style="text-align: left; height: 79px;">
                <asp:TextBox ID="txtReceivedOn" runat="server" type="date"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <table hidden="hidden">
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">PO Items Details</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:GridView ID="gvItDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvItDetails_RowDataBound"
                                OnRowEditing="gvItDetails_RowEditing" Width="100%">
                                <Columns>
                                    <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                    <asp:BoundField DataField="ItemType" HeaderText="Model No"></asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Model Name"></asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                    <asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                    <asp:BoundField HeaderText="Amount"></asp:BoundField>
                                    <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="DeliveryDate" HeaderText="Delivery Date"></asp:BoundField>
                                    <asp:BoundField DataField="Specifications" HeaderText="Specifications">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ItemTypeId" HeaderText="Item Type Id"></asp:BoundField>
                                </Columns>
                                <EmptyDataTemplate>
                                    &nbsp; <span style="color: #ff0033">No Data Found</span>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>

                </table>
            </td>
        </tr>

        <tr>
            <td class="profilehead" colspan="4" style="text-align: left">MRN Items Details</td>
        </tr>
        <tr>
            <td colspan="4" style="height: 19px; text-align: center">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound"
                    Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDelete2" OnClick="lbtnDelete2_Click" runat="server" __designer:wfdid="w2">Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ItemCode" HeaderText="ItemCode"></asp:BoundField>
                        <asp:BoundField DataField="ItemType" HeaderText="Model No"></asp:BoundField>
                        <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                        <asp:BoundField DataField="Godown" HeaderText="Location"></asp:BoundField>
                        <asp:BoundField DataField="OQuantity" HeaderText="Ordered Quantity"></asp:BoundField>
                        <asp:BoundField DataField="RQuantity" HeaderText="Received Qty"></asp:BoundField>
                        <asp:BoundField DataField="AQuantity" HeaderText="Accepted Qty"></asp:BoundField>
                        <asp:BoundField DataField="ReQuantity" HeaderText="Rejected Qty"></asp:BoundField>
                        <asp:BoundField DataField="NetQty" HeaderText="Net Qty"></asp:BoundField>
                        <asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
                        <asp:BoundField HeaderText="Amount"></asp:BoundField>
                        <asp:BoundField DataField="Godownid" HeaderText="Godownid"></asp:BoundField>
                        <asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
                        <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                        <asp:BoundField DataField="Colorid" HeaderText="Colorid"></asp:BoundField>
                        <asp:BoundField DataField="Chkid" HeaderText="Chkid"></asp:BoundField>
                        <asp:BoundField DataField="CHKDET_ID" HeaderText="CHKDET_ID"></asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        <span style="color: #ff0033">MRN Are Not Yet Done</span>
                    </EmptyDataTemplate>
                </asp:GridView>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="profilehead" colspan="4" style="height: 22px; text-align: left">Item Details
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label ID="Label12" runat="server" Text="Search By Brand"></asp:Label></td>
            <td style="text-align: left;">
                <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
                </asp:DropDownList></td>
            <td style="text-align: right;">
                <asp:Label ID="Label39" runat="server" Text="Search:" Width="84px"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtSearchModel" runat="server">
                </asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server"
                    ControlToValidate="txtSearchModel" ErrorMessage="Please Enter ModelNo For Search"
                    ValidationGroup="Search">*</asp:RequiredFieldValidator><asp:Button ID="btnSearchModelNo"
                        runat="server" BorderStyle="None" CausesValidation="False" CssClass="gobutton"
                        EnableTheming="False" OnClick="btnSearchModelNo_Click" Text="Go" ValidationGroup="Search" /><asp:SqlDataSource
                            ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                            SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter PropertyName="SelectedValue" Type="Int64" DefaultValue="0" Name="BranbId" ControlID="ddlBrand"></asp:ControlParameter>
                                <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtSearchModel"></asp:ControlParameter>
                            </SelectParameters>
                        </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="lblModel" runat="server" Text="ModelNo / Model Name" Width="157px"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlItemName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged">
                    <asp:ListItem Value="0">--</asp:ListItem>
                    <asp:ListItem Value="0">-- Select Item Type --</asp:ListItem>
                </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                    ControlToValidate="ddlItemName" ErrorMessage="Please Select the Model/Item Name"
                    InitialValue="0" SetFocusOnError="True" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right">
                <asp:Label ID="lblInstrument" runat="server" Text="Item Name"
                    Width="90px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtItemName" runat="server">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label ID="Label32" runat="server" Text="Item Category :"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtItemCategory" runat="server" ReadOnly="True">
                </asp:TextBox></td>
            <td style="text-align: right;">
                <asp:Label ID="Label33" runat="server" Text="Item SubCategory :"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtItemSubCategory" runat="server" ReadOnly="True">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right; height: 10px;">
                <asp:Label ID="Label54" runat="server" Text="Color :"></asp:Label></td>
            <td style="text-align: left; height: 10px;">&nbsp;<asp:DropDownList ID="ddlcolor" runat="server"></asp:DropDownList></td>
            <td style="text-align: right; height: 10px;">
                <asp:Label ID="Label55" runat="server" Text="Brand :"></asp:Label></td>
            <td style="text-align: left; height: 10px;">
                <asp:TextBox ID="txtManufacturer" runat="server">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right; height: 9px;">
                <asp:Label ID="lblSerialNo" runat="server" Text="Serial No" Visible="False"></asp:Label></td>
            <td style="text-align: left; height: 9px;">
                <asp:TextBox ID="txtSerialNo" runat="server" Visible="False"></asp:TextBox></td>
            <td style="text-align: right; height: 9px;"></td>
            <td style="text-align: left; height: 9px;">&nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label ID="Label8" runat="server" Text="Rate"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtRate" runat="server">
                </asp:TextBox></td>
            <td style="text-align: right;">
                <asp:Label ID="Label9" runat="server" Text="Location"></asp:Label></td>
            <td style="text-align: left;">
                <asp:DropDownList ID="ddlgodown" runat="server" AutoPostBack="True">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label ID="Label1" runat="server" Text="Ordered Quantity:"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtQty" runat="server"></asp:TextBox>
            </td>
            <td style="text-align: right;">
                <asp:Label ID="Label5" runat="server" Text="Received Quantity:"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtRecqty" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label6" runat="server" Text="Accepted Quantity:"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtAcceptedqty" runat="server">
                </asp:TextBox></td>
            <td style="text-align: right">
                <asp:Label ID="Label7" runat="server" Text="Rejected Quantity:"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtRejectedqty" runat="server">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label13" runat="server" Text="Remarks"></asp:Label></td>
            <td style="text-align: left" colspan="3">
                <asp:TextBox ID="txtRemarks" runat="server" Height="53px" TextMode="MultiLine" Width="606px" EnableTheming="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                    CssClass="loginbutton" EnableTheming="False" OnClick="btnAdd_Click" Text="Add" ValidationGroup="a" /><asp:Button
                        ID="btnItemRefresh" runat="server" BackColor="Transparent" BorderStyle="None"
                        CssClass="loginbutton" EnableTheming="False" OnClick="btnItemRefresh_Click" Text="Refresh" CausesValidation="False" /></td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <asp:GridView ID="gvItemDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvItemDetails_RowDataBound"
                    OnRowDeleting="gvItemDetails_RowDeleting" OnRowEditing="gvItemDetails_RowEditing"
                    Width="100%">
                    <Columns>
                        <asp:CommandField ShowEditButton="True"></asp:CommandField>
                        <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                        <asp:BoundField DataField="ItemCode" HeaderText="ItemCode"></asp:BoundField>
                        <asp:BoundField DataField="ItemType" HeaderText="Model No"></asp:BoundField>
                        <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                        <asp:BoundField DataField="Godown" HeaderText="Location"></asp:BoundField>
                        <asp:BoundField DataField="OQuantity" HeaderText="Ordered Quantity"></asp:BoundField>
                        <asp:BoundField DataField="RQuantity" HeaderText="Received Qty"></asp:BoundField>
                        <asp:BoundField DataField="AQuantity" HeaderText="Accepted Qty"></asp:BoundField>
                        <asp:BoundField DataField="ReQuantity" HeaderText="Rejected Qty"></asp:BoundField>
                        <asp:BoundField DataField="NetQty" HeaderText="Net Qty"></asp:BoundField>
                        <asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
                        <asp:BoundField HeaderText="Amount"></asp:BoundField>
                        <asp:BoundField DataField="Godownid" HeaderText="Godownid"></asp:BoundField>
                        <asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
                        <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                        <asp:BoundField DataField="Colorid" HeaderText="Colorid"></asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        <span style="color: #ff0033">No Data Found</span>
                    </EmptyDataTemplate>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: right;"></td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: left;" class="profilehead">Reference Details</td>
        </tr>
        <tr>
            <td style="height: 34px; text-align: right;"></td>
            <td style="height: 34px; text-align: left;"></td>
            <td style="height: 34px; text-align: right;"></td>
            <td style="height: 34px; text-align: left;"></td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <table width="100%">
                    <tr>
                        <td style="text-align: right; height: 12px;">
                            <asp:Label ID="lblPreparedBy" runat="server" Text="Material Executed " Width="135px"></asp:Label></td>
                        <td style="text-align: left; height: 12px;">
                            <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                        <td style="text-align: right; height: 12px;">
                            <asp:Label ID="lblApprovedBy" runat="server" Text="Checked By" Width="96px"></asp:Label></td>
                        <td style="text-align: left; height: 12px;">
                            <asp:DropDownList ID="ddlApprovedBy" runat="server">
                            </asp:DropDownList></td>
                        <td style="text-align: right; height: 12px;">
                            <asp:Label ID="Label2" runat="server" Text="Store Incharge"></asp:Label></td>
                        <td style="text-align: left; height: 12px;">
                            <asp:DropDownList ID="ddlStoreIncharge" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label3" runat="server" Text="Accounts"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlAccounts" runat="server">
                            </asp:DropDownList></td>
                        <td colspan="2" style="text-align: right"></td>
                        <td style="text-align: right">&nbsp;<asp:Label ID="Label4" runat="server" Text="Authorised Payments" Width="130px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlApayments" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <details>
                    <summary>Down</summary>
                    <table id="tblButtons" align="center">
                        <tr>
                            <td>
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                            <td>
                                <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" /></td>
                            <td>
                                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" CausesValidation="False" /></td>
                            <td>
                                <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" CausesValidation="False" /></td>
                            <td>
                                <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" /></td>
                        </tr>
                    </table>
                </details>
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server"
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="a"></asp:ValidationSummary>
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
        ShowSummary="False"></asp:ValidationSummary>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

 
