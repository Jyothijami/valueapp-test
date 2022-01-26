 <%@ Page Title="" Language="C#" MasterPageFile="~/MPs/SalesMP1.master" AutoEventWireup="true" CodeFile="WorkOrderDetails2.aspx.cs" Inherits="Modules_SM_WorkOrderDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 186px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    
    <asp:UpdatePanel runat="server">
        <ContentTemplate>

    <table id="tblWorkOrderDetails" runat="server"
        width="100%" visible="true">
        <tr>
            <td colspan="4" style="text-align: left" class="profilehead">General Details</td>
        </tr>
        <tr>
            <td colspan="4">&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label ID="lblSalesOrderNo" runat="server" Text="Purchase Order No"></asp:Label></td>
            <td style="text-align: left;">
                <asp:DropDownList ID="ddlOrderAcceptance" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOrderAcceptance_SelectedIndexChanged" Enabled="False">
                </asp:DropDownList><asp:Label ID="Label36" runat="server" EnableTheming="False" ForeColor="Red" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlOrderAcceptance"
                    ErrorMessage="Please Select The Order Acceptance No." InitialValue="0" ValidationGroup="a">*</asp:RequiredFieldValidator>

            </td>
            <td style="text-align: right;">
                <asp:Label ID="lblSalesOrderDate" runat="server" Text="Purchase Order Date"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtOADate" runat="server" ReadOnly="True" CssClass="datetext" EnableTheming="False"></asp:TextBox>&nbsp;<asp:Image ID="imgOADate"
                    runat="server" ImageUrl="~/Images/Calendar.png" Visible="False"></asp:Image><cc1:CalendarExtender
                        Format="dd/MM/yyyy" ID="ceOADate" runat="server" PopupButtonID="imgOADate" TargetControlID="txtOADate" Enabled="False">
                    </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="meeOADate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                    MaskType="Date" TargetControlID="txtOADate" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="lblCustomer" runat="server" Text="Customer Name"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtCustName" runat="server" ReadOnly="True"></asp:TextBox></td>
            <td style="text-align: right">
                <asp:Label ID="Label45" runat="server" Text="Unit Name"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtUnitName" runat="server" ReadOnly="True"></asp:TextBox></td>
        </tr>
        <tr>
            <td rowspan="2" style="text-align: right">
                <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>

            </td>
            <td rowspan="2" style="text-align: left;">
                <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" ReadOnly="True"></asp:TextBox></td>
            <td style="text-align: right">
                <asp:Label ID="lblCity" runat="server" Text="Region"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtRegion" runat="server" ReadOnly="True"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label ID="Label25" runat="server" Text="Phone"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtPhone" runat="server" ReadOnly="True"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True"></asp:TextBox>
                <asp:Label ID="lblRespId" runat="server" Visible="False"></asp:Label>
            </td>
            <td style="text-align: right">
                <asp:Label ID="Label26" runat="server" Text="Mobile"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: left" class="profilehead">Purchase Order Items</td>
        </tr>
        <tr>
            <td colspan="4" class="auto-style1">
                <asp:GridView ID="gvOrderAcceptanceItems" runat="server" AutoGenerateColumns="False"
                    OnRowDataBound="gvOrderAcceptanceItems_RowDataBound1" Width="100%">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkhdr" runat="server" AutoPostBack="True" OnCheckedChanged="chkhdr_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chk" runat="server" __designer:wfdid="w5"></asp:CheckBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                        <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                        <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                        <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                        <%--<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>--%>
                        <asp:TemplateField HeaderText="Quantity" ControlStyle-Width="80px">
                            <ItemTemplate>
                                <asp:TextBox ID="txtQuantity" Text='<%# Bind("Quantity") %>' runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
                        <asp:BoundField HeaderText="Amount"></asp:BoundField>
                        <asp:BoundField DataField="Specifications" HeaderText="Specifications"></asp:BoundField>
                        <asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
                        <asp:BoundField DataField="Priority" HeaderText="Priority"></asp:BoundField>
                        <asp:BoundField DataField="DeliveryDate" HeaderText="Delivery Date"></asp:BoundField>
                        <asp:BoundField DataField="Room" HeaderText="Room"></asp:BoundField>
                        <asp:BoundField DataField="Price" HeaderText="Price"></asp:BoundField>
                        <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                        <asp:BoundField DataField="ColorId" HeaderText="ColorId"></asp:BoundField>
                        <asp:BoundField DataField="SO_RES_STATUS" HeaderText="ReserveStatus"></asp:BoundField>
                        <asp:BoundField DataField="SODetId" HeaderText="SoDetId"></asp:BoundField>
                    </Columns>
                </asp:GridView>
                &nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;
            <asp:Label ID="lblItemCode" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblColor" runat="server" Visible="false"></asp:Label>

                <asp:Label ID="lblCustQty" runat="server" Visible="false"></asp:Label>

            </td>
            <td style="text-align: right">
                <asp:Button ID="btnCheck" runat="server" Text="Check Item Avaliability" OnClick="btnCheck_Click" Visible="False" /></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvAvailQty" runat="server" AutoGenerateColumns="False" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                        <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                        <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                        <asp:BoundField DataField="TtlQty" HeaderText="Total Qty"></asp:BoundField>
                        <asp:BoundField DataField="BlockQty" HeaderText="Blocked Qty"></asp:BoundField>
                        <asp:BoundField DataField="AvailQty" HeaderText="Avaliable Qty"></asp:BoundField>
                        <asp:BoundField DataField="CustQty" HeaderText="Cust Blocked Qty"></asp:BoundField>
                    </Columns>
                </asp:GridView>

            </td>
        </tr>
        <tr hidden="hidden">
            <td style="text-align: center">Total Quantity:<asp:Label ID="lblTtlQty" runat="server"></asp:Label></td>
            <td style="text-align: center">Blocked Quantity:<asp:Label ID="lblBlockQty" runat="server"></asp:Label></td>
            <td colspan="2" style="text-align: center">Avaliable Quantity:<asp:Label ID="lblAvaliableQty" runat="server"></asp:Label></td>

        </tr>
        <tr>
            <td class="profilehead" colspan="4">Order Profile Details</td>
        </tr>
        <tr>
            <td colspan="4">&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label1" runat="server" Text="Order Profile No"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtWONo" runat="server" ReadOnly="True"></asp:TextBox></td>
            <td style="text-align: right">
                <asp:Label ID="Label2" runat="server" Text="Order Profile Date"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtWODate" runat="server" CssClass="datetext" type="datepic" EnableTheming="False"></asp:TextBox><%--<asp:Image ID="imgWODate" runat="server" ImageUrl="~/Images/Calendar.png" />--%><asp:Label ID="Label10" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                </asp:Label>
                <asp:RequiredFieldValidator
                    ID="rfvWODate" runat="server" ControlToValidate="txtWODate" ErrorMessage="Please Select the Work Order Date">*</asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="DateCustomValidate"
                    ControlToValidate="txtWODate" ErrorMessage="Please Enter the Work Order Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                    SetFocusOnError="True">*</asp:CustomValidator>
                <%--<cc1:CalendarExtender
                    ID="ceWODate" runat="server" PopupButtonID="imgWODate" TargetControlID="txtWODate" Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="meeWODate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                    MaskType="Date" TargetControlID="txtWODate" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>--%>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="lblModeofDelivery" runat="server" Text="Mode of Delivery"></asp:Label></td>
            <td style="text-align: left;">
                <asp:DropDownList ID="ddlDeliveryMode" runat="server">
                </asp:DropDownList><asp:Label ID="Label11" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                </asp:Label>
                <asp:RequiredFieldValidator ID="rfvDeliveryMode" runat="server" ControlToValidate="ddlDeliveryMode"
                    ErrorMessage="Please Select the Mode of  Delivery" InitialValue="0" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right">
                <asp:Label ID="lblDeliveryDate" runat="server" Text="Delivery Date" Height="17px"></asp:Label>&nbsp;</td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtDeliveryDate" runat="server" CssClass="datetext" type="datepic" EnableTheming="False"></asp:TextBox><%--<asp:Image ID="imgDeliveryDate" runat="server" ImageUrl="~/Images/Calendar.png" />--%><asp:Label ID="Label16" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                </asp:Label><asp:RequiredFieldValidator
                    ID="rfvDeliveryDate" runat="server" ControlToValidate="txtDeliveryDate" ErrorMessage="Please Select the Delivery  Date" ValidationGroup="a">*</asp:RequiredFieldValidator><asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="DateCustomValidate"
                        ControlToValidate="txtDeliveryDate" ErrorMessage="Please Enter the Delivery Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                        SetFocusOnError="True">*</asp:CustomValidator><%--<cc1:CalendarExtender
                            Format="dd/MM/yyyy" ID="ceDeliveryDate" runat="server" PopupButtonID="imgDeliveryDate" TargetControlID="txtDeliveryDate">
                        </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="meeDeliveryDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                    MaskType="Date" TargetControlID="txtDeliveryDate" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>--%>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="lblVATCSTNo" runat="server" Text="C.S. Tax"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtCST" runat="server">
                </asp:TextBox><asp:Label ID="Label32" runat="server" Text="%"></asp:Label><asp:Label ID="Label14" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                </asp:Label><asp:RequiredFieldValidator ID="rfvCST" runat="server" ControlToValidate="txtCST"
                    ErrorMessage="Please Enter the CS Tax" ValidationGroup="a">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtCST"
                        ValidChars=".0123456789">
                    </cc1:FilteredTextBoxExtender>
            </td>
            <td style="text-align: right">
                <asp:Label ID="Label8" runat="server" Text="Order Taken In Hs/Trade"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtRoadPermit" runat="server"></asp:TextBox><asp:Label ID="Label18" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                </asp:Label><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRoadPermit"
                    ErrorMessage="Please Enter the Road Permit" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label7" runat="server" Text="Frieght"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtFrieght" runat="server"></asp:TextBox><asp:Label ID="Label17" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                </asp:Label>
                <asp:RequiredFieldValidator
                    ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFrieght" ErrorMessage="Please Enter the Frieght" Visible="False" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right"></td>
            <td style="text-align: left;">&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label3" runat="server" Text="Packing & Forwarding Instructions"></asp:Label></td>
            <td colspan="3" style="text-align: left">
                <asp:TextBox ID="txtPackingInstrs" runat="server" CssClass="multilinetext" EnableTheming="False" TextMode="MultiLine" Width="92%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label4" runat="server" Text="Accessories "></asp:Label></td>
            <td colspan="3" style="text-align: left">
                <asp:TextBox ID="txtAccessories" runat="server" CssClass="multilinetext" EnableTheming="False"
                    TextMode="MultiLine" Width="92%" ReadOnly="True"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label ID="Label5" runat="server" Text="Extra spares "></asp:Label>
            </td>
            <td style="text-align: left;" colspan="3">
                <asp:TextBox ID="txtExtraSpace" runat="server" CssClass="multilinetext" EnableTheming="False"
                    TextMode="MultiLine" Width="92%" ReadOnly="True"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="height: 19px; text-align: right">
                <asp:Label ID="Label52" runat="server" Text="Advance Amt"></asp:Label></td>
            <td colspan="3" style="height: 19px; text-align: left">
                <asp:TextBox ID="txtAdvanceAmt" runat="server" ReadOnly="True"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label22" runat="server" Text="Attachment" Visible="False"></asp:Label></td>
            <td colspan="3" style="text-align: left">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                    <ContentTemplate>
                        <asp:FileUpload ID="FileUpload1" runat="server" Font-Size="8pt" Font-Names="Verdana" Width="300px" Font-Italic="False" Visible="False"></asp:FileUpload>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSave"></asp:PostBackTrigger>
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label23" runat="server" Text="Attached File" Visible="False"></asp:Label></td>
            <td colspan="3" style="text-align: left">
                <asp:LinkButton ID="lbtnAttachedFiles" runat="server" OnClick="lbtnAttachedFiles_Click" Visible="False"></asp:LinkButton></td>
        </tr>
        <tr>
            <td class="profilehead" colspan="4" style="text-align: left">Reference Details</td>
        </tr>
        <tr>
            <td colspan="4">&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
            <td style="text-align: left;">
                <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False" EnableTheming="True">
                </asp:DropDownList></td>
            <td style="text-align: right;">
                <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By"></asp:Label></td>
            <td style="text-align: left;">
                <asp:DropDownList ID="ddlApprovedBy" runat="server" Enabled="False" EnableTheming="True">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label ID="lblCheckedBy" runat="server" Text="Checked By" Visible="False"></asp:Label></td>
            <td style="text-align: left;">
                <asp:DropDownList ID="ddlCheckedBy" runat="server" EnableTheming="False" Visible="False">
                </asp:DropDownList></td>
            <td style="text-align: right"></td>
            <td style="text-align: left;"></td>
        </tr>
        <tr>
            <td colspan="4">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="4">
                <table id="tblButtons" align="center">
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="a" />
                            <asp:Button ID="btnApprove" runat="server" CausesValidation="False" OnClick="btnApprove_Click"
                                Text="Approve" />
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CausesValidation="False" OnClick="btnEdit_Click" />
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CausesValidation="False"
                                OnClick="btnDelete_Click" />
                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CausesValidation="False"
                                OnClick="btnRefresh_Click" />
                            <asp:Button ID="btnSend" runat="server" CausesValidation="False" OnClick="btnSend_Click"
                                Text="Send" />
                            <asp:Button ID="btnPrint" runat="server" Text="Print" CausesValidation="False"
                                OnClick="btnPrint_Click" />
                            <asp:Button ID="btnClose" runat="server" Text="Close" CausesValidation="False" OnClick="btnClose_Click" />
                            <asp:Button ID="btnReserve" runat="server" Text="Reserve" CausesValidation="False"
                                OnClick="btnReserve_Click" Visible="False" />
                            <asp:Button ID="btnIndent" runat="server" OnClick="btnIndent_Click" Text="Indent" />
                            <asp:Label ID="lblCust_Id" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4">&nbsp;</td>

        </tr>
    </table>

    <table id="tblHiddenFields" runat="server" visible="false">
        <tr>
            <td colspan="3">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Button ID="btnNew" runat="server" Text="New" CausesValidation="False" OnClick="btnNew_Click" Visible="False" /></td>

        </tr>
        <tr>
            <td colspan="3">&nbsp;</td>
        </tr>
    </table>

    <asp:Panel ID="Panel1" runat="server" meta:resourcekey="Panel1Resource1">
        <table>
            <tr>
                <td>&nbsp;</td>
            </tr>
        </table>
        <table id="tblPopUp1" runat="server" border="0" cellpadding="0" cellspacing="0" style="font-size: 8pt; background-image: url(Images/ConfirmBox2.PNG); background-repeat: repeat; font-family: Verdana"
            visible="false">
            <tr>
                <td background="../../Images/ConfirmBox1Sa.PNG" style="height: 300px; text-align: left; width: 55px;"></td>
                <td align="center" background="../../Images/ConfirmBox2sa.PNG" style="height: 300px"
                    valign="top">
                    <table>
                        <tr>
                            <td colspan="3" rowspan="1" style="text-align: left">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: left">
                                <asp:Label ID="lblMessage" runat="server" meta:resourcekey="lblMessageResource1"
                                    Text="Reserve Stock Position."></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: left">
                                <asp:Label ID="lblData" runat="server" meta:resourcekey="lblDataResource1"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" rowspan="1" style="text-align: left">
                                <asp:Label ID="lblDo" runat="server" meta:resourcekey="lblDoResource1" Text="What Should I Do?">
                                </asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="3" style="width: 400px; text-align: center">
                                <asp:Button ID="btnConfirmYes" runat="server" CausesValidation="False" EnableTheming="False"
                                    Font-Names="Verdana" Font-Size="8pt" Height="23px" meta:resourcekey="btnConfirmYesResource1"
                                    Text="Reserve" Width="80px" OnClick="btnConfirmYes_Click" />
                                &nbsp;
                                            <asp:Button ID="btnConfirmNo" runat="server" CausesValidation="False" EnableTheming="False"
                                                Font-Names="Verdana" Font-Size="8pt" Height="23px" meta:resourcekey="btnConfirmNoResource1"
                                                Text="Cancel" Width="80px" OnClick="btnConfirmNo_Click" /></td>
                        </tr>
                    </table>
                    &nbsp; &nbsp; &nbsp;
                </td>
                <td background="../../Images/ConfirmBox3sa.PNG" style="width: 27px; height: 300px"></td>
            </tr>
        </table>
    </asp:Panel>

    <asp:ValidationSummary ID="vs1" runat="server" ShowMessageBox="True" ShowSummary="False" />
    <asp:ValidationSummary ID="vs2" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="a" />
    <asp:Label ID="Label6" runat="server" Text="Expected Date" Visible="False"></asp:Label><asp:TextBox ID="txtInspectionDate" runat="server" CssClass="datetext" EnableTheming="False" Visible="False"></asp:TextBox><asp:Image ID="imgInspectionDate" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False" /><asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="DateCustomValidate"
        ControlToValidate="txtInspectionDate" ErrorMessage="Please Enter the Inspection Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
        SetFocusOnError="True" Visible="False">*</asp:CustomValidator><cc1:CalendarExtender
            Format="dd/MM/yyyy" ID="ceInspectionDate" runat="server" PopupButtonID="imgInspectionDate" TargetControlID="txtInspectionDate">
        </cc1:CalendarExtender>
    <asp:Label ID="Label9" runat="server" Text="Insurance" Visible="False"></asp:Label>
    <asp:TextBox ID="txtInsurance" runat="server" Visible="False"></asp:TextBox>
    <asp:Label ID="Label19" runat="server" EnableTheming="False" ForeColor="Red" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator
        ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtInsurance"
        ErrorMessage="Please Enter the Insurance" Visible="False">*</asp:RequiredFieldValidator>
    <cc1:MaskedEditExtender ID="meeeInspectionDate" runat="server" DisplayMoney="Left"
        Mask="99/99/9999" MaskType="Date" TargetControlID="txtInspectionDate" UserDateFormat="MonthDayYear">
    </cc1:MaskedEditExtender>
    <cc1:ModalPopupExtender
        ID="ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="False"
        PopupControlID="Panel1" RepositionMode="None" TargetControlID="btnReserve">
    </cc1:ModalPopupExtender>
    <asp:SqlDataSource ID="sdsWorkOrder" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
        SelectCommand="SP_SM_WORKORDER_SEARCH_SELECT" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="EMPID" ControlID="lblEmpIdHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
        Visible="False"></asp:Label>
    <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
        Visible="False"></asp:Label>
    <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
    
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


 
