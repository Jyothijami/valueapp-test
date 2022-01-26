<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="GRNDetails.aspx.cs" Inherits="Modules_Inventory_GRNDetails" Title="|| YANTRA : Inventory : GRN Details ||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                grn details</td>
        </tr>
    </table>
    <table style="width: 88px" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="4" style="text-align: left" class="searchhead">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">
                            GRN<br />
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
                                            <asp:ListItem Value="SUP_QUOT_NO">Sup Quotation No.</asp:ListItem>
                                            <asp:ListItem Value="SUP_QUOT_DATE">Sup Quotation Date</asp:ListItem>
                                            <asp:ListItem Value="SUP_NAME">Supplier Name</asp:ListItem>
                                            <asp:ListItem Value="SUP_QUOT_PO_TYPE">PO Type</asp:ListItem>
                                            <asp:ListItem Value="SUP_EMAIL">Enquiry From</asp:ListItem>
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
                                            CssClass="gobutton" EnableTheming="False" Text="Go" /></td>
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
            <td colspan="4" style="text-align: center">
                <asp:GridView ID="gvGRNDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="sdsGRNDetails" OnRowDataBound="gvGRNDetails_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="GRN_ID" HeaderText="GRNIdHidden"></asp:BoundField>
                        <asp:TemplateField HeaderText="GRNNo">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("GRN_NO") %>' ID="TextBox1"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemTemplate>
                                &nbsp;<asp:LinkButton ID="lbtnGRNNo" OnClick="lbtnGRNNo_Click" runat="server" Text='<%# Eval("GRN_NO") %>'
                                    __designer:wfdid="w3"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="GRN_DATE"
                            HeaderText="GRN Date"></asp:BoundField>
                        <asp:BoundField DataField="MRIR_NO" SortExpression="MRIR_NO" HeaderText="MRIR No"></asp:BoundField>
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
                <asp:SqlDataSource ID="sdsGRNDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DbCon %>"
                    SelectCommand="SP_INVENTORY_GRNDETAILS_SEARCH_SELECT" SelectCommandType="StoredProcedure">
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
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: left">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 49px; text-align: center">
                <table id="Table1">
                    <tr>
                        <td>
                            <asp:Button ID="btnNew" runat="server" OnClick="btnNew_Click" Text="New" /></td>
                        <td>
                            <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Edit" /></td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" /></td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <table border="0" cellpadding="0" cellspacing="0" id="tblGRNDetails" runat="server"
                    visible="false">
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">
                            General Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right; width: 155px;">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 155px;">
                            <asp:Label ID="lblGRNType" runat="server" Text="GRN Type" Width="83px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:RadioButton ID="rbPurchaseOrder" runat="server" Text="Purchase Order" GroupName="GRNType">
                            </asp:RadioButton></td>
                        <td style="text-align: left">
                            <asp:RadioButton ID="rbMRIR" runat="server" Text="MRIR" GroupName="GRNType"></asp:RadioButton></td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 155px;">
                            <asp:Label ID="lblGRNNo" runat="server" Text="GRN No" Width="76px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtGRNNo" runat="server"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblGRNDate" runat="server" Text="GRN Date" Width="94px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtGRNDate" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imgGRNDate"
                                runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender
                                    Format="dd/MM/yyyy" ID="ceGRNDate" runat="server" Enabled="True" PopupButtonID="imgGRNDate"
                                    TargetControlID="txtGRNDate">
                                </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeGRNDate" runat="server" DisplayMoney="Left" Enabled="True"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtGRNDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 155px; height: 22px;">
                            <asp:Label ID="Label9" runat="server" Text="MRIR No" Width="119px"></asp:Label></td>
                        <td style="text-align: left; height: 22px;">
                            <asp:DropDownList ID="ddlMRIRNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMRIRNo_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="text-align: right; height: 22px;">
                        </td>
                        <td style="text-align: left; height: 22px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 155px;">
                            <asp:Label ID="lblSupplierName" runat="server" Text="Supplier Name" Width="119px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtSupplierName" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblSupplierAddress" runat="server" Text="Supplier Address" Width="119px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtSupplierAddress" runat="server" TextMode="MultiLine" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 155px; height: 24px;">
                            <asp:Label ID="lblPONo" runat="server" Text="PO No" Width="119px"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox ID="txtPONo" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label ID="lblPODate" runat="server" Text="PO Date" Width="119px"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox ID="txtPODate" runat="server" ReadOnly="True"></asp:TextBox>&nbsp;<asp:Image
                                ID="imgPODate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>
                            <cc1:CalendarExtender Format="dd/MM/yyyy" ID="cePODate" runat="server" Enabled="True"
                                PopupButtonID="imgPODate" TargetControlID="txtPODate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meePODate" runat="server" DisplayMoney="Left" Enabled="True"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtPODate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 155px;">
                            <asp:Label ID="lblScheduleNo" runat="server" Text="Schedule No" Width="119px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtScheduleNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblScheduleDate" runat="server" Text="Schedule Date" Width="99px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtScheduleDate" runat="server" ReadOnly="True"></asp:TextBox>
                            <asp:Image ID="imgScheduleDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender
                                Format="dd/MM/yyyy" ID="ceScheduleDate" runat="server" Enabled="True" PopupButtonID="imgScheduleDate"
                                TargetControlID="txtScheduleDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeScheduleDate" runat="server" DisplayMoney="Left" Enabled="True"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtScheduleDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px; width: 155px;">
                        </td>
                        <td style="text-align: left; height: 19px;">
                        </td>
                        <td style="text-align: right; height: 19px;">
                        </td>
                        <td style="text-align: left; height: 19px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">
                            Item Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px; width: 155px;">
                        </td>
                        <td style="text-align: left; height: 19px;">
                        </td>
                        <td style="text-align: right; height: 19px;">
                        </td>
                        <td style="text-align: left; height: 19px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 155px;">
                            <asp:Label ID="Label2" runat="server" Text="Item Name"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlItemName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label3" runat="server" Text="UOM"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtUOM" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 155px; height: 24px;">
                            <asp:Label ID="Label4" runat="server" Text="Order Quantity"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox ID="txtOrderedQty" runat="server"></asp:TextBox></td>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label ID="Label5" runat="server" Text="Received Quantity"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox ID="txtReceivedQty" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 155px;">
                        </td>
                        <td style="text-align: left">
                        </td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 155px;">
                        </td>
                        <td style="text-align: right">
                            <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" Text="Add" OnClick="btnAdd_Click" /></td>
                        <td style="text-align: left">
                            <asp:Button ID="btnItemsRefresh" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" Text="Refresh" OnClick="btnItemsRefresh_Click" /></td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right; width: 155px;">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 21px">
                            &nbsp;<asp:GridView ID="gvItemsDetails" runat="server" AutoGenerateColumns="False"
                                OnRowDeleting="gvItemsDetails_RowDeleting" Width="100%" OnRowDataBound="gvItemsDetails_RowDataBound">
                                <Columns>
                                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                    <asp:BoundField DataField="OrderedQty" HeaderText="Ordered Qty"></asp:BoundField>
                                    <asp:BoundField DataField="ReceivedQty" HeaderText="Received Qty"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">
                            Other Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right; width: 155px;">
                        </td>
                        <td colspan="3" style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 155px;">
                            <asp:Label ID="lblRemarks" runat="server" Text="Remarks" Width="73px"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="451px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 155px;">
                        </td>
                        <td style="text-align: left">
                        </td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">
                            Reference Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right; width: 155px;">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 155px;">
                            <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlPreparedBy" runat="server">
                            </asp:DropDownList></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By" Width="96px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlApprovedBy" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px; width: 155px;">
                        </td>
                        <td style="height: 19px">
                        </td>
                        <td style="height: 19px">
                        </td>
                        <td style="height: 19px">
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
                                    </td>
                                    <td>
                                        <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
            </td>
        </tr>
        <tr>
            <td style="height: 21px; width: 155px;">
            </td>
            <td style="height: 21px">
            </td>
            <td style="height: 21px;">
            </td>
            <td style="height: 21px">
            </td>
        </tr>
    </table>
</asp:Content>

 
