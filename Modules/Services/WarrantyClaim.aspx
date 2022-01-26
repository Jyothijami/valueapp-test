<%@ Page Language="C#" MasterPageFile="~/MPs/ServicesMP1.master" AutoEventWireup="true"
     CodeFile="WarrantyClaim.aspx.cs" Inherits="Modules_Services_WarrantyClaim" Title="|| Value App : Services : Warranty Claim || " %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

  

    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td title="|| ERP: SM :CLAIMFORM ||">
                Warranty Claim</td>
             <td>
                <asp:DropDownList ID="ddlNoOfRecords" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged">
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>75</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                   
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" >
                    <tr>
                        <td style="text-align: left;">
                            Warranty Claim
                        </td>
                        <td>
                        </td>
                        <td style="text-align: right;">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td>
                                        <asp:Label id="lblSearch" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList id="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged1">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="WC_NO">WC NO</asp:ListItem>
                                            <asp:ListItem Value="WC_DATE">WF DATE</asp:ListItem>
                                            <asp:ListItem Value="WC_SUPPLIER_NAME">Supplier Name</asp:ListItem>
                                            <asp:ListItem Value="WC_INVOICE_NO">Supplier Invoice No</asp:ListItem>
                                            <asp:ListItem Value="WC_INVOICE_DATE">Supplier Invoice Date</asp:ListItem>
                                            <asp:ListItem Value="WC_DATE_OF_INSTAL">Install Date</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td>
                                        <asp:DropDownList id="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                            EnableTheming="False" Visible="False" Width="50px" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged1">
                                            <asp:ListItem Selected="True">=</asp:ListItem>
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem>&gt;</asp:ListItem>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <asp:ListItem>&gt;=</asp:ListItem>
                                            <asp:ListItem>R</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td>
                                        <asp:Label id="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                                    <td>
                                        <asp:TextBox id="txtSearchValueFromDate" runat="server" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox><asp:Image id="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image><cc1:CalendarExtender ID="ceSearchFrom" runat="server" Enabled="False" Format="dd/MM/yyyy"
                                            PopupButtonID="imgFromDate" TargetControlID="txtSearchValueFromDate">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td>
                                        <asp:Label id="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                    <td>
                                        <asp:TextBox id="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><asp:Image id="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image><cc1:CalendarExtender ID="ceSearchValueToDate" runat="server" Enabled="False" Format="dd/MM/yyyy"
                                            PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td>
                                        <asp:Button id="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnSearchGo_Click1" /></td>
                                </tr>
                                </table>
                            <asp:Label id="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center;">
                <asp:GridView id="gvWarrantyClaim" SelectedRowStyle-BackColor="#c0c0c0" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="sdsWarrantyClaim" OnRowDataBound="gvWarrantyClaim_RowDataBound" AllowSorting="True">
                    <columns>
<asp:BoundField DataField="WC_ID" SortExpression="WC_ID" HeaderText="WCIdHidden"></asp:BoundField>
<asp:TemplateField HeaderText="WC  No" SortExpression="WC_NO"><EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("CR_NO") %>' ID="TextBox1"></asp:TextBox>
                        
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnWCNo" runat="server" Text="<%# BIND('WC_NO') %>" ForeColor="#0066ff" CausesValidation="False" OnClick="lbtnWCNo_Click"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HtmlEncode="False" SortExpression="WC_DATE" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True" DataField="WC_DATE" HeaderText="WC Date"></asp:BoundField>
<asp:BoundField DataField="WC_SUPPLIER_NAME" SortExpression="WC_SUPPLIER_NAME" HeaderText="Supplier Name">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="WC_INVOICE_NO" SortExpression="WC_INVOICE_NO" HeaderText="Sup Inv No">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:BoundField>
<asp:BoundField HtmlEncode="False" SortExpression="WC_INVOICE_DATE" DataFormatString="{0:dd/MM/yyyy}" DataField="WC_INVOICE_DATE" HeaderText="Sup Inv Date">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField HtmlEncode="False" SortExpression="WC_DATE_OF_INSTAL" DataFormatString="{0:dd/MM/yyyy}" DataField="WC_DATE_OF_INSTAL" HeaderText="Inst Date"></asp:BoundField>
<asp:BoundField DataField="PREPAREDBY" SortExpression="PREPAREDBY" HeaderText="Prepared By">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
</columns>
                    <emptydatatemplate>
No Data Exist!
</emptydatatemplate>
                    <selectedrowstyle backcolor="LightSteelBlue" />
                </asp:GridView><asp:SqlDataSource id="sdsWarrantyClaim" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SERVICES_WARRANTY_CLAIM_SEARCH_SELECT" SelectCommandType="StoredProcedure"><selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
</selectparameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; height: 49px;">
                <table id="Table1">
                    <tr>
                        <td>
                            <asp:Button id="btnNew" runat="server" CausesValidation="False" onclick="btnNew_Click"
                                Text="New" /></td>
                        <td>
                            <asp:Button id="btnEdit" runat="server" CausesValidation="False"
                                Text="Edit" OnClick="btnEdit_Click" /></td>
                        <td>
                            <asp:Button id="btnDelete" runat="server" CausesValidation="False"
                                Text="Delete" OnClick="btnDelete_Click" /></td>
                        <td><asp:Button id="btnPrint" runat="server" CausesValidation="False"
                                Text="Print" OnClick="btnPrint_Click" /></td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table border="0" cellpadding="0" cellspacing="0" id="tblWarrantyDetails" runat="server"
                    visible="false">
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">
                            General Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right;">
                        </td>
                        <td style="height: 21px; text-align: left;">
                        </td>
                        <td style="height: 21px; text-align: right;">
                        </td>
                        <td style="height: 21px; text-align: left;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label id="lblWarrantyNo" runat="server" Text="Warranty Claim No"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox id="txtWCNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label id="lblDate" runat="server" Text="Warranty Date"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox id="txtWCDate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox><asp:Image id="imgWCDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender
                                ID="ceWCDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgWCDate" TargetControlID="txtWCDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeWCDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtWCDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 22px; text-align: right">
                            <asp:Label id="lblSupplier" runat="server" Text="Supplier Name" Width="119px"></asp:Label></td>
                        <td style="height: 22px; text-align: left"><asp:TextBox id="txtSupplierName" runat="server">Smart LabTech Pvt. Ltd.</asp:TextBox></td>
                        <td style="height: 22px; text-align: right">
                            <asp:Label id="lblDateOfInstallation" runat="server" Text="Date Of Installation"></asp:Label></td>
                        <td style="height: 22px; text-align: left">
                            <asp:TextBox id="txtInstallationDate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox><asp:Image id="imgInstallationDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender
                                ID="ceInstallationDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgInstallationDate" TargetControlID="txtInstallationDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeInstallationDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtInstallationDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 22px;"><asp:Label id="Label1" runat="server" Text="Supplier Invoice  No"></asp:Label></td>
                        <td style="text-align: left; height: 22px;"><asp:TextBox id="txtSupInvoiceNo" runat="server">
                        </asp:TextBox></td>
                        <td style="text-align: right; height: 22px;">
                            <asp:Label id="lblInvoiceDate" runat="server" Text="Supplier Invoice Date" Width="140px"></asp:Label></td>
                        <td style="text-align: left; height: 22px;"><asp:TextBox id="txtSupplierDate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox><asp:Image id="imgSupplierDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender
                                ID="ceSupplierDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgSupplierDate" TargetControlID="txtSupplierDate">
                        </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeSupplierDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtSupplierDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            </td>
                        <td style="text-align: left;">
                            </td>
                        <td style="text-align: right;">
                            </td>
                        <td style="text-align: left;">
                            </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            supplier details (communication for warranty claim)</td>
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
                        <td style="text-align: right">
                            <asp:Label ID="Label6" runat="server" Text="Supplier Company Name"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            &nbsp;<asp:TextBox ID="txtSupplierCompanyName" runat="server" CssClass="textbox"
                                EnableTheming="False" Width="523px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label4" runat="server" Text="Supplier Address For Communication" Width="153px"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtSupplierToAddress" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="523px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label5" runat="server" Text="Attention To"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            &nbsp;<asp:TextBox ID="txtAttentionTo" runat="server" CssClass="textbox" EnableTheming="False"
                                Width="523px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            customer details</td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                        </td>
                        <td style="height: 24px; text-align: left">
                        </td>
                        <td style="height: 24px; text-align: right">
                        </td>
                        <td style="height: 24px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label id="lblCustomer" runat="server" Text="Customer Name"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                            <asp:DropDownList id="ddlCustomerName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomerName_SelectedIndexChanged" >
                            </asp:DropDownList></td>
                        <td style="height: 24px; text-align: right">
                            <asp:Label id="lblInitName" runat="server" Text="Unit Name" Width="74px"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                            <asp:DropDownList id="ddlUnitName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="0">--Select Customer--</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblContactPerson" runat="server" Text="Contact Person"></asp:Label></td>
                        <td style="text-align: left"><asp:DropDownList id="ddlContactPerson" runat="server">
                            <asp:ListItem Value="0">--</asp:ListItem>
                            <asp:ListItem Value="0">--Select Unit Name--</asp:ListItem>
                        </asp:DropDownList></td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label id="Label2" runat="server" Text="Customer Unit Address"></asp:Label></td>
                        <td colspan="3" style="height: 24px; text-align: left">
                            <asp:TextBox id="txtCustUnitAddress" runat="server" EnableTheming="False" TextMode="MultiLine" Width="523px" CssClass="multilinetext"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Item Details</td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                        </td>
                        <td style="height: 24px; text-align: left">
                        </td>
                        <td style="height: 24px; text-align: right">
                        </td>
                        <td style="height: 24px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right"><asp:Label id="Label3" runat="server" Text="Item Type" Width="119px"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                            <asp:DropDownList id="ddlItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="height: 24px; text-align: right">
                            <asp:Label id="lblModel" runat="server" Text="Model"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                            <asp:DropDownList id="ddlModel" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModel_SelectedIndexChanged" >
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label id="lblItemDescription" runat="server" Text="Item Description" Width="119px"></asp:Label></td>
                        <td style="text-align: left; height: 24px;" colspan="3">
                            <asp:TextBox id="txtDescription" runat="server" EnableTheming="False" TextMode="MultiLine" Width="523px" CssClass="multilinetext"></asp:TextBox>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label id="lblSLNo" runat="server" Text="SL No" Width="50px"></asp:Label></td>
                        <td style="height: 24px; text-align: left;">
                            <asp:TextBox id="txtSLNo" runat="server"></asp:TextBox></td>
                        <td style="height: 24px; text-align: right;">
                            <asp:Label id="lblQuantity" runat="server" Text="Quantity"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                            <asp:TextBox id="txtQuantity" runat="server"></asp:TextBox>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label id="lblExpires" runat="server" Text="Warranty Expires On" Width="142px"></asp:Label></td>
                        <td style="height: 24px; text-align: left;">
                            <asp:TextBox id="txtExpires" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox><asp:Image id="imgExpires" runat="server" ImageUrl="~/Images/Calendar.png">
                            </asp:Image><cc1:CalendarExtender
                                ID="ceExpires" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgExpires" TargetControlID="txtExpires">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeExpires" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtExpires" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                        <td style="height: 24px; text-align: right;">
                            <asp:Label id="lblClaimed" runat="server" Text="Warranty Claimed On" Width="143px"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                            <asp:TextBox id="txtClaimed" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox><asp:Image id="imgClaimed" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender
                                ID="ceClaimed" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgClaimed" TargetControlID="txtClaimed">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeClaimed" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtClaimed" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label id="lblProblem" runat="server" Text="Nature Of the Problem" Width="148px"></asp:Label></td>
                        <td colspan="3" style="height: 24px; text-align: left">
                            <asp:TextBox id="txtProblemNature" runat="server" EnableTheming="False" TextMode="MultiLine"
                                Width="525px" CssClass="multilinetext"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label id="lblService" runat="server" Text="Service Engineer's Remarks" Width="180px"></asp:Label></td>
                        <td style="height: 24px; text-align: left" colspan="3">
                            <asp:TextBox id="txtServiceRemarks" runat="server" EnableTheming="False" TextMode="MultiLine"
                                Width="525px" CssClass="multilinetext"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label id="lblSmpl" runat="server" Text="SMPL ServiceEngineer's Remarks" Width="172px"></asp:Label></td>
                        <td style="height: 24px; text-align: left" colspan="3">
                            <asp:TextBox id="txtSmplRemarks" runat="server" EnableTheming="False" TextMode="MultiLine"
                                Width="526px" CssClass="multilinetext"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblUrgently" runat="server" Text="Remarks Needs Warranty Replacement Urgently"></asp:Label></td>
                        <td style="text-align: left" colspan="3">
                            <asp:TextBox id="txtRemarksNeeds" runat="server" EnableTheming="False" TextMode="MultiLine"
                                Width="526px" CssClass="multilinetext"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: right; height: 26px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left;" class="profilehead">
                            Reference Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right;">
                        </td>
                        <td style="height: 21px; text-align: left;">
                        </td>
                        <td style="height: 21px; text-align: right;">
                        </td>
                        <td style="height: 21px; text-align: left;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label id="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList id="ddlPreparedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                        <td style="text-align: right;">
                            <asp:Label id="lblApprovedBy" runat="server" Text="Approved By" Width="96px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList id="ddlApprovedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
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
                        <td colspan="4">
                            <table id="tblButtons">
                                <tr>
                                    <td>
                                        <asp:Button id="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                    <td>
                                        <asp:Button id="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" /></td>
                                    <td>
                                        <asp:Button id="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" /></td>
                                    <td style="width: 3px">
                                    </td>
                                </tr>
                            </table>
                        </td>
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
            <td colspan="4">
            </td>
        </tr>
    </table>
</asp:Content>


 
