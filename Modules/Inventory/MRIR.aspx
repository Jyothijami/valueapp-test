<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="MRIR.aspx.cs" Inherits="Modules_Inventory_MRIR" Title="|| YANTRA : Inventory : MRIR Details ||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                MRIR</td>
        </tr>
    </table>
    <table style="width: 88px" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="height: 19px">
            </td>
            <td style="height: 19px">
            </td>
            <td style="height: 19px">
            </td>
            <td style="height: 19px">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 19px" class="searchhead">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">
                            MRIR<br />
                            &nbsp;Details</td>
                        <td>
                        </td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Label ID="Label20" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="MRIR_ID">MRIR ID</asp:ListItem>
                                            <asp:ListItem Value="MRIR_NO">MRIR NO</asp:ListItem>
                                            <asp:ListItem Value="MRIR_DATE">MRIR DATE</asp:ListItem>
                                            <asp:ListItem Value="SUP_QUOT_PO_TYPE">PO Type</asp:ListItem>
                                            <asp:ListItem Value="FPO_NO">PO NO</asp:ListItem>
                                            <asp:ListItem Value="FPO_DATE">PO DATE</asp:ListItem>
                                            <asp:ListItem Value="MRIR_PDC_NO">PDC NO</asp:ListItem>
                                            <asp:ListItem Value="MRIR_PDC_DATE">PDC DATE</asp:ListItem>
                                            <asp:ListItem Value="FPO_PO_STATUS">STATUS</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:DropDownList ID="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                            EnableTheming="False" Visible="False" Width="50px">
                                            <asp:ListItem Selected="True">=</asp:ListItem>
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem>&gt;</asp:ListItem>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <asp:ListItem>&gt;=</asp:ListItem>
                                            <asp:ListItem>R</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:TextBox ID="txtSearchValueFromDate" runat="server" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox><asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchFrom" runat="server" Enabled="False"
                                            PopupButtonID="imgFromDate" TargetControlID="txtSearchValueFromDate">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchValueToDate" runat="server"
                                            Enabled="False" PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnSearchGo_Click" /></td>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                </tr>
                            </table>
                            <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 19px; text-align: center;" colspan="4">
                <asp:GridView ID="gvMRIRDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="sdsMRIRDetails" OnRowDataBound="gvMRIRDetails_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="MRIR_ID" HeaderText="MRIRIdHidden"></asp:BoundField>
                        <asp:TemplateField HeaderText="MRIR Id">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("MRIR_ID") %>' ID="TextBox1"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnMRIRId" runat="server" Text='<%# Bind("MRIR_ID") %>' CausesValidation="False"
                                    OnClick="lbtnMRIRId_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="MRIR_NO" SortExpression="MRIR_NO" HeaderText="MRIR No"></asp:BoundField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True"
                            DataField="MRIR_DATE" HeaderText="MRIR Date"></asp:BoundField>
                        <asp:BoundField DataField="FPO_NO" HeaderText="PO No">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="FPO_DATE"
                            HeaderText="PO Date">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="MRIR_PDC_NO" SortExpression="MRIR_PDC_NO" HeaderText="PDC No">
                        </asp:BoundField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="MRIR_PDC_DATE"
                            SortExpression="MRIR_PDC_DATE" HeaderText="PDC Date"></asp:BoundField>
                        <asp:BoundField DataField="SUP_NAME" HeaderText="Supplier Name">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="FPO_PO_STATUS" HeaderText="Status">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        No Record Found
                    </EmptyDataTemplate>
                    <SelectedRowStyle BackColor="LightSteelBlue" />
                </asp:GridView>
                <asp:SqlDataSource ID="sdsMRIRDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DbCon %>"
                    SelectCommand="SP_INVENTORY_MRIR_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName"
                            ControlID="lblSearchItemHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType"
                            ControlID="lblSearchTypeHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue"
                            ControlID="lblSearchValueHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom"
                            ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
                    </SelectParameters>
                </asp:SqlDataSource>
                &nbsp;</td>
        </tr>
        <tr>
            <td id="Td20" style="height: 19px">
            </td>
            <td style="height: 19px">
            </td>
            <td style="height: 19px">
            </td>
            <td style="height: 19px">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 21px">
                <table id="Table1">
                    <tr>
                        <td>
                            <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" /></td>
                        <td>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" /></td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" /></td>
                        <td>
                            <asp:Button ID="btnInspect" runat="server" Text="Inspection" OnClick="btnInspect_Click" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 21px">
            </td>
            <td style="height: 21px">
            </td>
            <td style="height: 21px;">
            </td>
            <td style="height: 21px">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 21px">
                <table border="0" cellpadding="0" cellspacing="0" id="tblMRIRDetails" runat="server"
                    visible="true">
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">
                            General Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: left">
                        </td>
                        <td style="height: 21px; text-align: right;">
                        </td>
                        <td style="height: 21px; text-align: left;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblMRIRNo" runat="server" Text="MRIR No"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtMRIRNo" runat="server"></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblMRIRDate" runat="server" Text="MRIR  Date"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtMRIRDate" runat="server" Width="135px"></asp:TextBox>&nbsp;<asp:Image
                                ID="imgMRIRDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>
                            &nbsp;&nbsp;
                            <cc1:MaskedEditExtender ID="meeMRIRDate" runat="server" DisplayMoney="Left" Enabled="False"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtMRIRDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                            <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceMRIRDate" runat="server" Enabled="True"
                                PopupButtonID="imgMRIRDate" TargetControlID="txtMRIRDate">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblPONo" runat="server" Text="PO No"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlPONo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPONo_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right;">
                            &nbsp;<asp:Label ID="lblPODate" runat="server" Text="PO Date" Width="99px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtPODate" runat="server" ReadOnly="True" Width="107px"></asp:TextBox><asp:Image
                                ID="imgPODate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>&nbsp;
                            <cc1:CalendarExtender Format="dd/MM/yyyy" ID="cePODate" runat="server" Enabled="True"
                                PopupButtonID="imgPODate" TargetControlID="txtPODate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meePODate" runat="server" DisplayMoney="Left" Enabled="False"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtPODate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label9" runat="server" Text="PDC No"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPDCNo" runat="server">
                            </asp:TextBox>&nbsp;</td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label10" runat="server" Text="PDC Date"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtPDCDate" runat="server" Width="119px"></asp:TextBox><asp:Image
                                ID="imgPDCDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>&nbsp;
                            <cc1:CalendarExtender Format="dd/MM/yyyy" ID="cePDCDate" runat="server" Enabled="True"
                                PopupButtonID="imgPDCDate" TargetControlID="txtPDCDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meePDCDate" runat="server" DisplayMoney="Left" Enabled="False"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtPDCDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                        <td style="text-align: right;">
                        </td>
                        <td style="text-align: left;">
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Supplier Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                        <td style="text-align: right;">
                        </td>
                        <td style="text-align: left;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblSupplierName1" runat="server" Text="Supplier Name" Width="119px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlSupplierName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSupplierName_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblContactPerson1" runat="server" Text="Contact Person" Width="99px"></asp:Label>&nbsp;&nbsp;
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtContactPerson" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label112" runat="server" Text="PhoneNo"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPhoneNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblEmail2" runat="server" Text="Email"></asp:Label>&nbsp;
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblAddress2" runat="server" Text="Address" Width="105px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right;">
                        </td>
                        <td style="text-align: left;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 6px;">
                        </td>
                        <td style="text-align: left; height: 6px;">
                        </td>
                        <td style="text-align: right; height: 6px;">
                        </td>
                        <td style="text-align: left; height: 6px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left" class="profilehead" colspan="4">
                            Transportation Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                        <td style="text-align: right;">
                        </td>
                        <td style="text-align: left;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 85px;">
                            <asp:Label ID="lblInvoiceNo" runat="server" Text="Invoice No" Width="119px"></asp:Label></td>
                        <td style="text-align: left; height: 85px;">
                            <asp:TextBox ID="txtInvoiceNo" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right; height: 85px;">
                            <asp:Label ID="lblInvoiceDate" runat="server" Text="Invoice Date" Width="119px"></asp:Label>&nbsp;</td>
                        <td style="text-align: left; height: 85px;">
                            <asp:TextBox ID="txtInvoiceDate" runat="server">
                            </asp:TextBox>&nbsp;<asp:Image ID="imgInvoiceDate" runat="server" ImageUrl="~/Images/Calendar.png">
                            </asp:Image><cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceInvoiceDate" runat="server"
                                Enabled="True" PopupButtonID="imgInvoiceDate" TargetControlID="txtInvoiceDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeInvoiceDate" runat="server" DisplayMoney="Left" Enabled="True"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtInvoiceDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label ID="lblLRNo" runat="server" Text="LR No" Width="119px"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox ID="txtLRNo" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label ID="lblVehicleNo" runat="server" Text="Vehicle No" Width="119px"></asp:Label>&nbsp;
                        </td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox ID="txtVehicleNo" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblFromStation" runat="server" Text="From Station" Width="119px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtFromStation" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblTransportName" runat="server" Text="Transport Name" Width="119px">
                            </asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtTransportName" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblChallanNo" runat="server" Text="Challan No" Width="119px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtChallanNo" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblChallanDate" runat="server" Text="Challan Date" Width="119px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtChallanDate" runat="server">
                            </asp:TextBox>&nbsp;<asp:Image ID="imgChallanDate" runat="server" ImageUrl="~/Images/Calendar.png">
                            </asp:Image><cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceChallanDate" runat="server"
                                Enabled="True" PopupButtonID="imgChallanDate" TargetControlID="txtChallanDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeChallanDate" runat="server" DisplayMoney="Left" Enabled="True"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtChallanDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblGatePassNo" runat="server" Text="MRN No" Width="119px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtGatePassNo" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblGatePassDate" runat="server" Text="Gate Pass Date" Width="119px">
                            </asp:Label>&nbsp;
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtGatePassDate" runat="server">
                            </asp:TextBox>&nbsp;<asp:Image ID="imgGatePassDate" runat="server" ImageUrl="~/Images/Calendar.png">
                            </asp:Image><cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceGatePassDate" runat="server"
                                Enabled="True" PopupButtonID="imgGatePassDate" TargetControlID="txtGatePassDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeGatePassDate" runat="server" DisplayMoney="Left" Enabled="True"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtGatePassDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblNotInStock" runat="server" Text="Not In Stock" Width="119px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:CheckBox ID="chkNotInStock" runat="server" Text=" "></asp:CheckBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblInExcisble" runat="server" Text="In Excisble" Width="119px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:CheckBox ID="chkInExcisble" runat="server" Text=" "></asp:CheckBox></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: right;">
                        </td>
                        <td style="height: 19px; text-align: left;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">
                            &nbsp;Item Details</td>
                    </tr>
                    <tr>
                        <td style="height: 2px; text-align: right">
                        </td>
                        <td style="height: 2px; text-align: left">
                        </td>
                        <td style="height: 2px; text-align: right;">
                        </td>
                        <td style="height: 2px; text-align: left;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label1" runat="server" Text="Item Type"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="True">
                            </asp:DropDownList></td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label2" runat="server" Text="Item Name"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlItemName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label3" runat="server" Text="UOM" Width="46px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtUOM" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right;">
                        </td>
                        <td style="text-align: left;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblOrderedQty" runat="server" Text="Ordered Qty" Width="80px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtOrderedQty" runat="server"></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label4" runat="server" Text="Received Qty" Width="96px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtReceivedQty" runat="server" AutoPostBack="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblAcceptedQty" runat="server" Text="Accept Quantity" Visible="False">
                            </asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtAcceptedQty" runat="server" Visible="False">
                            </asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblRejectQty" runat="server" Text="Reject Quantity" Visible="False"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtRejectQty" runat="server" Visible="False">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;">
                        </td>
                        <td style="text-align: left; height: 19px;">
                        </td>
                        <td style="text-align: right; height: 19px;">
                        </td>
                        <td style="text-align: left; height: 19px;">
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <table border="0" cellpadding="0" cellspacing="0" id="tblInspectionDetails" runat="server"
                                visible="false" width="100%">
                                <tr>
                                    <td class="profilehead" colspan="4" style="text-align: left">
                                        Inspection Details</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                    </td>
                                    <td style="text-align: left">
                                    </td>
                                    <td style="text-align: right">
                                    </td>
                                    <td style="text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblVisual" runat="server" Text="Visual" Width="41px"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtVisual" runat="server">
                                        </asp:TextBox></td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblHardness" runat="server" Text="Hardness"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtHardness" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <asp:Label ID="Label11" runat="server" Text="Surface Finish"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtSurfaceFinish" runat="server"></asp:TextBox></td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblEmail" runat="server" Text="Others"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtOthers" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; height: 22px;">
                                        <asp:Label ID="Label7" runat="server" Text="STC"></asp:Label></td>
                                    <td colspan="3" style="text-align: left; height: 22px;">
                                        <asp:FileUpload ID="fuSTC" runat="server"></asp:FileUpload></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; height: 22px;">
                                        <asp:Label ID="Label5" runat="server" Text="Inspected Status"></asp:Label></td>
                                    <td style="text-align: left; height: 22px;">
                                        <asp:DropDownList ID="ddlInspStatus" runat="server">
                                            <asp:ListItem>--</asp:ListItem>
                                            <asp:ListItem>New</asp:ListItem>
                                            <asp:ListItem>Open</asp:ListItem>
                                            <asp:ListItem>Close</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="text-align: right; height: 22px;">
                                        <asp:Label ID="Label6" runat="server" Text="Inspected By"></asp:Label></td>
                                    <td style="text-align: left; height: 22px;">
                                        <asp:DropDownList ID="ddlInspectedBy" runat="server">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblRemarks" runat="server" Text="Remarks"></asp:Label></td>
                                    <td colspan="3" style="text-align: left">
                                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                    </td>
                                    <td style="text-align: left">
                                    </td>
                                    <td style="text-align: right">
                                    </td>
                                    <td style="text-align: left">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: right">
                            <asp:Button ID="btnAdd" runat="server" Text="Add" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" OnClick="btnAdd_Click" /></td>
                        <td style="height: 21px; text-align: left;">
                            <asp:Button ID="btnItemsRefresh" runat="server" Text="Refresh" BackColor="Transparent"
                                BorderStyle="None" CssClass="loginbutton" EnableTheming="False" OnClick="btnItemsRefresh_Click" /></td>
                        <td style="height: 21px; text-align: left;">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: left">
                        </td>
                        <td style="height: 21px; text-align: right;">
                        </td>
                        <td style="height: 21px; text-align: left;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 21px; text-align: right">
                            <asp:GridView ID="gvItemsDetails" runat="server" AutoGenerateColumns="False" Width="100%"
                                OnRowDeleting="gvItemsDetails_RowDeleting" OnRowDataBound="gvItemsDetails_RowDataBound">
                                <Columns>
                                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                    <asp:BoundField DataField="ItemCode" HeaderText="ItemCode"></asp:BoundField>
                                    <asp:BoundField DataField="ItemType" HeaderText="Item Type"></asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                    <asp:BoundField DataField="ReceivedQty" HeaderText="Received Qty"></asp:BoundField>
                                    <asp:BoundField DataField="OrderedQty" HeaderText="OrderedQty"></asp:BoundField>
                                    <asp:BoundField DataField="AcceptedQty" HeaderText="Accepted Qty"></asp:BoundField>
                                    <asp:BoundField DataField="RejectedQty" HeaderText="Rejected Qty"></asp:BoundField>
                                    <asp:BoundField DataField="Visual" HeaderText="Visual"></asp:BoundField>
                                    <asp:BoundField DataField="Hardness" HeaderText="Hardness"></asp:BoundField>
                                    <asp:BoundField DataField="SurfaceFinish" HeaderText="Surface Finish"></asp:BoundField>
                                    <asp:BoundField DataField="Others" HeaderText="Others"></asp:BoundField>
                                    <asp:BoundField DataField="STC" HeaderText="STC"></asp:BoundField>
                                    <asp:BoundField DataField="InspectedStatus" HeaderText="Inspected Status"></asp:BoundField>
                                    <asp:BoundField DataField="InspectedBy" HeaderText="Inspected By"></asp:BoundField>
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: left">
                        </td>
                        <td style="height: 21px; text-align: right;">
                        </td>
                        <td style="height: 21px; text-align: left;">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: left" class="profilehead" colspan="4">
                            Reference Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: left">
                        </td>
                        <td style="height: 21px; text-align: right;">
                        </td>
                        <td style="height: 21px; text-align: left;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlPreparedBy" runat="server">
                            </asp:DropDownList></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By" Width="96px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlApprovedBy" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;">
                        </td>
                        <td style="height: 19px">
                        </td>
                        <td style="height: 19px;">
                        </td>
                        <td style="height: 19px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table id="tblButtons">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 21px">
            </td>
        </tr>
    </table>
</asp:Content>

 
