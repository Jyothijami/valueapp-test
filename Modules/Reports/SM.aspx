<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SM.aspx.cs" Inherits="Modules_Reports_SM" Title="|| Value App   : Reports : SM||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <script language="javascript" type="text/javascript">
        function PrintDivContent(printdiv) {
            var printContent = document.getElementById(printdiv);
            var WinPrint = window.open('', '', 'left=0,top=0,toolbar=0,sta­tus=0');
            WinPrint.document.write(printContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
        }
    </script>

    <script language="javascript" type="text/javascript">
        function PrintDivContent(Reservestock) {
            var printContent = document.getElementById(Reservestock);
            var WinPrint = window.open('', '', 'left=0,top=0,toolbar=0,sta­tus=0');
            WinPrint.document.write(printContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
        }
    </script>
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td style="text-align: left">SM Reports</td>
        </tr>
    </table>
    <table id="Table2">
        <tr>
            <td valign="top" style="height: 2930px">
                <table style="width:175px;height:60px;font-weight:bolder;" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="border-bottom: 1px solid #c0c0c0; font-size: 17px; padding-bottom: 11px; text-align: center;">
                            </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnMIDS" runat="server" CausesValidation="False"
                                CssClass="leftmenu" EnableTheming="False" OnClick="lbtnMenuLinks_Click">Sales Quotation</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnMPDS" runat="server" CausesValidation="False"
                                CssClass="leftmenu" EnableTheming="False" OnClick="lbtnMenuLinks_Click" Visible="False">MPDS</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnEMDReceived" runat="server" CausesValidation="False"
                                CssClass="leftmenu" EnableTheming="False" OnClick="lbtnMenuLinks_Click" Visible="False">EMD Received</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnEMDList" runat="server" CausesValidation="False"
                                CssClass="leftmenu" EnableTheming="False" OnClick="lbtnMenuLinks_Click" Visible="False">EMD List</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnPendingPayments" runat="server" CausesValidation="False"
                                CssClass="leftmenu" EnableTheming="False" OnClick="lbtnMenuLinks_Click" Visible="False">Sales Out Standing</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnSalesOutStanding" runat="server" CausesValidation="False"
                                CssClass="leftmenu" EnableTheming="False" OnClick="lbtnMenuLinks_Click" Visible="False">Sales Out Standing (Cust Wise)</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnAdsOutStanding" runat="server" CausesValidation="False" CssClass="leftmenu"
                                EnableTheming="False" OnClick="lbtnMenuLinks_Click" Visible="False">Advertising Outstanding</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnSDBGStatement" runat="server" CausesValidation="False" CssClass="leftmenu"
                                EnableTheming="False" OnClick="lbtnMenuLinks_Click" Visible="False">SD/BG Statement</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnSalesLeadStatement" runat="server" CausesValidation="False" CssClass="leftmenu"
                                EnableTheming="False" OnClick="lbtnMenuLinks_Click" Visible="False">Sales Lead Statement</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnSalesAssignment" runat="server" CausesValidation="False" CssClass="leftmenu"
                                EnableTheming="False" OnClick="lbtnMenuLinks_Click">Sales Assignment Stmt</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnPurchaseOrder" runat="server" CausesValidation="False" CssClass="leftmenu"
                                EnableTheming="False" OnClick="lbtnMenuLinks_Click">Purchase Order Stmt</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnInternalOrder" runat="server" CausesValidation="False" CssClass="leftmenu"
                                EnableTheming="False" OnClick="lbtnMenuLinks_Click">Internal Order Stmt</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnDailyReport" runat="server" CausesValidation="False" CssClass="leftmenu"
                                EnableTheming="False" OnClick="lbtnMenuLinks_Click">Daily Report Stmt</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnProformaInvoice" runat="server" CausesValidation="False" 
                                CssClass="leftmenu" EnableTheming="False" OnClick="lbtnMenuLinks_Click">Customer Analysis</asp:LinkButton></td>
                    </tr>
                     <tr>
                        <td>
                            <asp:LinkButton ID="lbtnResverveStock" runat="server" CausesValidation="False"
                                CssClass="leftmenu" EnableTheming="False" OnClick="lbtnMenuLinks_Click">Sales Report</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnArchitect" runat="server" CausesValidation="False"
                                CssClass="leftmenu" EnableTheming="False" OnClick="lbtnMenuLinks_Click">Architects Report</asp:LinkButton></td>
                   
                    </tr>
                </table>
            </td>
            <td style="text-align: center; height: 2930px;" width="100%" valign="top">
                <table width="600" border="0" cellpadding="0" cellspacing="0" id="tblMIDS" runat="server" visible="false">
                    <tr>
                        <td class="profilehead" colspan="3" style="text-align: left">Sales Quotation Statement</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label31" runat="server" Text="Company Name" Width="103px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlCompanyMIDS" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblPRDate" runat="server" Text="From Date" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtMIDSYear" runat="server" EnableTheming="False" type="datepic" CssClass="datetext" Width="124px"></asp:TextBox><%--<asp:Image
                                ID="imgFromMids" runat="server" ImageUrl="~/Images/Calendar.png" />--%><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtMIDSYear"
                                    ErrorMessage="Please Select the Quotation From Date" ValidationGroup="mids">*</asp:RequiredFieldValidator><asp:CustomValidator
                                        ID="CustomValidator10" runat="server" ClientValidationFunction="DateCustomValidate"
                                        ControlToValidate="txtMIDSYear" ErrorMessage="Please Enter the Quotation Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                        SetFocusOnError="True" ValidationGroup="mids">*</asp:CustomValidator>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMIDSYear" ErrorMessage="Please Enter Year"
                                ValidationGroup="mids" Visible="False">*</asp:RequiredFieldValidator>
                            <%--<cc1:CalendarExtender
                                            ID="CalendarExtender9" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgFromMids"
                                            TargetControlID="txtMIDSYear">
                </cc1:CalendarExtender>--%>
                            <%--<cc1:MaskedEditExtender ID="MaskedEditExtender12" runat="server" DisplayMoney="Left" Enabled="True"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtMIDSYear" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                    Enabled="False" Mask="9999" MaskType="Number" TargetControlID="txtMIDSYear">
                    </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label5" runat="server" Text="To Date" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtToDateMids" runat="server" type="datepic" CssClass="datetext" EnableTheming="False"></asp:TextBox><%--<asp:Image
                                ID="imgToDateMids" runat="server" ImageUrl="~/Images/Calendar.png" />--%><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtToDateMids"
                                    ErrorMessage="Please Select the Quotation To Date" ValidationGroup="emd">*</asp:RequiredFieldValidator><asp:CustomValidator
                                        ID="CustomValidator9" runat="server" ClientValidationFunction="DateCustomValidate"
                                        ControlToValidate="txtToDateMids" ErrorMessage="Please Enter the Quotation To Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                        SetFocusOnError="True" ValidationGroup="emd">*</asp:CustomValidator><%--<cc1:CalendarExtender
                                            ID="CalendarExtender8" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgToDateMids"
                                            TargetControlID="txtToDateMids">
                    </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="MaskedEditExtender11" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtToDateMids"
                                UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>--%>
                            <asp:DropDownList ID="ddlMIDSMonth" runat="server" Visible="False">
                                <asp:ListItem Value="0">All</asp:ListItem>
                                <asp:ListItem Value="1">January</asp:ListItem>
                                <asp:ListItem Value="2">February</asp:ListItem>
                                <asp:ListItem Value="3">March</asp:ListItem>
                                <asp:ListItem Value="4">April</asp:ListItem>
                                <asp:ListItem Value="5">May</asp:ListItem>
                                <asp:ListItem Value="6">June</asp:ListItem>
                                <asp:ListItem Value="7">July</asp:ListItem>
                                <asp:ListItem Value="8">August</asp:ListItem>
                                <asp:ListItem Value="9">September</asp:ListItem>
                                <asp:ListItem Value="10">October</asp:ListItem>
                                <asp:ListItem Value="11">November</asp:ListItem>
                                <asp:ListItem Value="12">December</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label32" runat="server" Text="Department" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" meta:resourcekey="ddlDepartmentResource1"
                                OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" Width="154px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlDepartment"
                                ErrorMessage="Please Select the Quotation Department" ValidationGroup="mids">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label6" runat="server" Text="Employee Name" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlMIDSEmployeeName" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlMIDSEmployeeName"
                                ErrorMessage="Please Select the Quotation Employee Name" ValidationGroup="mids">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="Label7" runat="server" Text="Customer Status" Width="117px" Visible="False"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlMIDSCustomerStatus" runat="server" Visible="False">
                                <asp:ListItem Value="All">All</asp:ListItem>
                                <asp:ListItem>New</asp:ListItem>
                                <asp:ListItem>Existing</asp:ListItem>
                            </asp:DropDownList>
                            <asp:CheckBox ID="chkIsExpectedOrder" runat="server" Text="Expected Orders"></asp:CheckBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnMIDSRpt" runat="server" OnClick="btnMIDSRpt_Click" Text="Run Report" ValidationGroup="mids" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="mids" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">

                            <table width="600" border="0" cellpadding="0" cellspacing="0" id="tblMIDS2" runat="server" visible="false">
                                <tr>
                                    <td class="profilehead" colspan="3" style="text-align: left">Sales Quotation Statement Prepared by</td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right"></td>
                                    <td style="text-align: left"></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblPRDate2" runat="server" Text="From Date" Width="117px"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtMIDSYear2" runat="server" EnableTheming="False" type="datepic" CssClass="datetext" Width="124px"></asp:TextBox><%--<asp:Image
                                ID="imgFromMids2" runat="server" ImageUrl="~/Images/Calendar.png" />--%><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator142" runat="server" ControlToValidate="txtMIDSYear"
                                    ErrorMessage="Please Select the Quotation From Date" ValidationGroup="mids">*</asp:RequiredFieldValidator><asp:CustomValidator
                                        ID="CustomValidator102" runat="server" ClientValidationFunction="DateCustomValidate"
                                        ControlToValidate="txtMIDSYear2" ErrorMessage="Please Enter the Quotation Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                        SetFocusOnError="True" ValidationGroup="mids2">*</asp:CustomValidator>
                                        <asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtMIDSYear2" ErrorMessage="Please Enter Year"
                                            ValidationGroup="mids" Visible="False">*</asp:RequiredFieldValidator>
                                        <%-- <cc1:CalendarExtender
                                            ID="CalendarExtender162" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgFromMids2"
                                            TargetControlID="txtMIDSYear2">
                </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="MaskedEditExtender192" runat="server" DisplayMoney="Left" Enabled="True"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtMIDSYear2" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
                <cc1:MaskedEditExtender ID="MaskedEditExtender202" runat="server"
                    Enabled="False" Mask="9999" MaskType="Number" TargetControlID="txtMIDSYear2">
                    </cc1:MaskedEditExtender>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label52" runat="server" Text="To Date" Width="117px"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtToDateMids2" runat="server" type="datepic" CssClass="datetext" EnableTheming="False"></asp:TextBox><%--<asp:Image
                                ID="imgToDateMids2" runat="server" ImageUrl="~/Images/Calendar.png" />--%><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator132" runat="server" ControlToValidate="txtToDateMids"
                                    ErrorMessage="Please Select the Quotation To Date" ValidationGroup="emd">*</asp:RequiredFieldValidator><asp:CustomValidator
                                        ID="CustomValidator92" runat="server" ClientValidationFunction="DateCustomValidate"
                                        ControlToValidate="txtToDateMids2" ErrorMessage="Please Enter the Quotation To Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                        SetFocusOnError="True" ValidationGroup="emd">*</asp:CustomValidator><%--<cc1:CalendarExtender
                                            ID="CalendarExtender172" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgToDateMids2"
                                            TargetControlID="txtToDateMids2">
                    </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="MaskedEditExtender212" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtToDateMids2"
                                UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label62" runat="server" Text="Employee Name" Width="117px"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlMIDSEmployeeName2" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator1722" runat="server" ControlToValidate="ddlMIDSEmployeeName2"
                                            ErrorMessage="Please Select the Quotation Employee Name" ValidationGroup="mids">*</asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;"></td>
                                    <td style="text-align: left;">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right"></td>
                                    <td style="text-align: left"></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="btnMIDSRpt2" runat="server" OnClick="btnMIDSRpt2_Click" Text="Run Report" ValidationGroup="mids2" /></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:ValidationSummary ID="ValidationSummary12" runat="server" ValidationGroup="mids2" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>




                <table width="600" border="0" cellpadding="0" cellspacing="0" id="tblMPDS" runat="server" visible="false">
                    <tr>
                        <td class="profilehead" colspan="3" style="text-align: left">MPDS</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label1" runat="server" Text="Year" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtMPDSYear" runat="server" EnableTheming="True"></asp:TextBox><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMPDSYear" ErrorMessage="Please Enter Year"
                                ValidationGroup="mpds">*</asp:RequiredFieldValidator><cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                    Enabled="True" Mask="9999" MaskType="Number" TargetControlID="txtMPDSYear">
                                </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label2" runat="server" Text="Month" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlMPDSMonth" runat="server">
                                <asp:ListItem Value="0">All</asp:ListItem>
                                <asp:ListItem Value="1">January</asp:ListItem>
                                <asp:ListItem Value="2">February</asp:ListItem>
                                <asp:ListItem Value="3">March</asp:ListItem>
                                <asp:ListItem Value="4">April</asp:ListItem>
                                <asp:ListItem Value="5">May</asp:ListItem>
                                <asp:ListItem Value="6">June</asp:ListItem>
                                <asp:ListItem Value="7">July</asp:ListItem>
                                <asp:ListItem Value="8">August</asp:ListItem>
                                <asp:ListItem Value="9">September</asp:ListItem>
                                <asp:ListItem Value="10">October</asp:ListItem>
                                <asp:ListItem Value="11">November</asp:ListItem>
                                <asp:ListItem Value="12">December</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="Label3" runat="server" Text="Employee Name" Width="117px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:DropDownList ID="ddlMPDSEmployeeName" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="Label4" runat="server" Text="Customer Status" Width="117px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:DropDownList ID="ddlMPDSCustomerStatus" runat="server">
                                <asp:ListItem Value="All">All</asp:ListItem>
                                <asp:ListItem>New</asp:ListItem>
                                <asp:ListItem>Existing</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 24px">
                            <asp:Button ID="btnMPDSRpt" runat="server" OnClick="btnMPDSRpt_Click" Text="Run Report" ValidationGroup="mpds" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="mpds" />
                        </td>
                    </tr>
                </table>
                <table width="600" border="0" cellpadding="0" cellspacing="0" id="tblEMDReceived" runat="server" visible="false">
                    <tr>
                        <td class="profilehead" colspan="3" style="text-align: left">EMD received</td>
                    </tr>
                    <tr>
                        <td style="height: 19px"></td>
                        <td style="height: 19px"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label12" runat="server" Text="From" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtEMDReceivedFromDate" runat="server" type="datepic" CssClass="datetext" EnableTheming="False"></asp:TextBox><%--<asp:Image
                                ID="imgEMDReceivedFromDate" runat="server" ImageUrl="~/Images/Calendar.png" />--%><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtEMDReceivedFromDate"
                                    ErrorMessage="Please Select the Invoice Date" ValidationGroup="emd">*</asp:RequiredFieldValidator><asp:CustomValidator
                                        ID="CustomValidator1" runat="server" ClientValidationFunction="DateCustomValidate"
                                        ControlToValidate="txtEMDReceivedFromDate" ErrorMessage="Please Enter the Invoice Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                        SetFocusOnError="True" ValidationGroup="emd">*</asp:CustomValidator><%--<cc1:CalendarExtender
                                            ID="ceDate" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgEMDReceivedFromDate"
                                            TargetControlID="txtEMDReceivedFromDate">
                                        </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditDate" runat="server" DisplayMoney="Left" Enabled="True"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtEMDReceivedFromDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label8" runat="server" Text="To" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtEMDReceivedToDate" runat="server" type="datepic" CssClass="datetext" EnableTheming="False"></asp:TextBox><%--<asp:Image
                                ID="imgEMDReceivedToDate" runat="server" ImageUrl="~/Images/Calendar.png" />--%><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEMDReceivedToDate"
                                    ErrorMessage="Please Select the Invoice Date" ValidationGroup="emd">*</asp:RequiredFieldValidator><asp:CustomValidator
                                        ID="CustomValidator2" runat="server" ClientValidationFunction="DateCustomValidate"
                                        ControlToValidate="txtEMDReceivedToDate" ErrorMessage="Please Enter the Invoice Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                        SetFocusOnError="True" ValidationGroup="emd">*</asp:CustomValidator><%--<cc1:CalendarExtender
                                            ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgEMDReceivedToDate"
                                            TargetControlID="txtEMDReceivedToDate">
                                        </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtEMDReceivedToDate"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 24px">
                            <asp:Button ID="btnEMDReceivedRpt" runat="server" OnClick="btnEMDReceivedRpt_Click" Text="Run Report" ValidationGroup="emd" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="emdlist" />
                        </td>
                    </tr>
                </table>
                <table width="600" border="0" cellpadding="0" cellspacing="0" id="tblEMDList" runat="server" visible="false">
                    <tr>
                        <td class="profilehead" colspan="3" style="text-align: left; height: 20px;">EMD List</td>
                    </tr>
                    <tr>
                        <td style="height: 19px"></td>
                        <td style="height: 19px"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label9" runat="server" Text="From" Width="96px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtEMDListFromDate" runat="server" type="datepic" CssClass="datetext" EnableTheming="False"></asp:TextBox><%--<asp:Image
                                ID="imgEMDToFromDate" runat="server" ImageUrl="~/Images/Calendar.png" />--%><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEMDListFromDate"
                                    ErrorMessage="Please Select the Invoice Date" ValidationGroup="emdlist">*</asp:RequiredFieldValidator><asp:CustomValidator
                                        ID="CustomValidator3" runat="server" ClientValidationFunction="DateCustomValidate"
                                        ControlToValidate="txtEMDListFromDate" ErrorMessage="Please Enter the Invoice Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                        SetFocusOnError="True" ValidationGroup="emdlist">*</asp:CustomValidator><%--<cc1:CalendarExtender
                                            ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgEMDToFromDate"
                                            TargetControlID="txtEMDListFromDate">
                                        </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" DisplayMoney="Left" Enabled="True"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtEMDListFromDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label10" runat="server" Text="To" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtEMDListToDate" runat="server" type="datepic" CssClass="datetext" EnableTheming="False"></asp:TextBox><%--<asp:Image
                                ID="imgEMDListToDate" runat="server" ImageUrl="~/Images/Calendar.png" />--%><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEMDListToDate"
                                    ErrorMessage="Please Select the Invoice Date" ValidationGroup="emd">*</asp:RequiredFieldValidator><asp:CustomValidator
                                        ID="CustomValidator4" runat="server" ClientValidationFunction="DateCustomValidate"
                                        ControlToValidate="txtEMDListToDate" ErrorMessage="Please Enter the Invoice Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                        SetFocusOnError="True" ValidationGroup="emd">*</asp:CustomValidator><%--<cc1:CalendarExtender
                                            ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgEMDListToDate"
                                            TargetControlID="txtEMDListToDate">
                                        </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtEMDListToDate"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 24px">
                            <asp:Button ID="btnEMDListRpt" runat="server" OnClick="btnEMDListRpt_Click" Text="Run Report" ValidationGroup="emdlist" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="emd" />
                        </td>
                    </tr>
                </table>
                <table width="600" border="0" cellpadding="0" cellspacing="0" id="tblPendingPayments" runat="server" visible="false">
                    <tr>
                        <td class="profilehead" colspan="3" style="text-align: left">sales out standing</td>
                    </tr>
                    <tr>
                        <td style="height: 19px"></td>
                        <td style="height: 19px"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label11" runat="server" Text="Year" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPendingPaymentsYear" runat="server" EnableTheming="True"></asp:TextBox><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPendingPaymentsYear"
                                ErrorMessage="Please Enter Year" ValidationGroup="pendingpayments">*</asp:RequiredFieldValidator><cc1:MaskedEditExtender ID="MaskedEditExtender6" runat="server"
                                    Enabled="True" Mask="9999" MaskType="Number" TargetControlID="txtMIDSYear">
                                </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label13" runat="server" Text="Month" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlPendingPaymentsMonth" runat="server">
                                <asp:ListItem Value="0">All</asp:ListItem>
                                <asp:ListItem Value="1">January</asp:ListItem>
                                <asp:ListItem Value="2">February</asp:ListItem>
                                <asp:ListItem Value="3">March</asp:ListItem>
                                <asp:ListItem Value="4">April</asp:ListItem>
                                <asp:ListItem Value="5">May</asp:ListItem>
                                <asp:ListItem Value="6">June</asp:ListItem>
                                <asp:ListItem Value="7">July</asp:ListItem>
                                <asp:ListItem Value="8">August</asp:ListItem>
                                <asp:ListItem Value="9">September</asp:ListItem>
                                <asp:ListItem Value="10">October</asp:ListItem>
                                <asp:ListItem Value="11">November</asp:ListItem>
                                <asp:ListItem Value="12">December</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label14" runat="server" Text="Employee Name" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlPendingPaymentsEmployeeName" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="Label15" runat="server" Text="Customer Status" Width="117px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:DropDownList ID="ddlPendingPaymentsCustomerStatus" runat="server">
                                <asp:ListItem Value="All">All</asp:ListItem>
                                <asp:ListItem>New</asp:ListItem>
                                <asp:ListItem>Existing</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="Label16" runat="server" Text="Department" Width="117px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:DropDownList ID="ddlPendingPaymentsDepartment" runat="server">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem>Sales</asp:ListItem>
                                <asp:ListItem>Services</asp:ListItem>
                            </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                ControlToValidate="ddlPendingPaymentsDepartment" ErrorMessage="Please Select Department"
                                ValidationGroup="pendingpayments" InitialValue="0" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnPendingPaymentsRpt" runat="server" OnClick="btnPendingPaymentsRpt_Click" Text="Run Report" ValidationGroup="pendingpayments" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="ValidationSummary5" runat="server" ValidationGroup="pendingpayments" />
                        </td>
                    </tr>
                </table>
                <table width="600" border="0" cellpadding="0" cellspacing="0" id="tblSalesOutStanding" runat="server" visible="false">
                    <tr>
                        <td class="profilehead" colspan="3" style="text-align: left">sales out standing (customer wise)&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="height: 19px"></td>
                        <td style="height: 19px"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label17" runat="server" Text="Customer Name" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlSOSCustomerName" runat="server">
                                <asp:ListItem Value="0">All</asp:ListItem>
                                <asp:ListItem Value="1">January</asp:ListItem>
                                <asp:ListItem Value="2">February</asp:ListItem>
                                <asp:ListItem Value="3">March</asp:ListItem>
                                <asp:ListItem Value="4">April</asp:ListItem>
                                <asp:ListItem Value="5">May</asp:ListItem>
                                <asp:ListItem Value="6">June</asp:ListItem>
                                <asp:ListItem Value="7">July</asp:ListItem>
                                <asp:ListItem Value="8">August</asp:ListItem>
                                <asp:ListItem Value="9">September</asp:ListItem>
                                <asp:ListItem Value="10">October</asp:ListItem>
                                <asp:ListItem Value="11">November</asp:ListItem>
                                <asp:ListItem Value="12">December</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="Label19" runat="server" Text="No. Of Days" Width="117px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:DropDownList ID="ddlSOSNoOfDays" runat="server">
                                <asp:ListItem Value="All">All</asp:ListItem>
                                <asp:ListItem Value="&lt;=30">Up To 30 Days</asp:ListItem>
                                <asp:ListItem Value="30-60">From 30 Days To 60 Days</asp:ListItem>
                                <asp:ListItem Value="60-90">From 60 Days To 90 Days</asp:ListItem>
                                <asp:ListItem Value="90-120">From 90 Days To 120 Days</asp:ListItem>
                                <asp:ListItem Value="&gt;120">More Than 120 Days</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 24px">
                            <asp:Button ID="btnSalesOutStandingRpt" runat="server" OnClick="btnSalesOutStandingRpt_Click" Text="Run Report" ValidationGroup="sos" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                </table>
                <table width="600" border="0" cellpadding="0" cellspacing="0" id="tblAdsOutStanding" runat="server" visible="false">
                    <tr>
                        <td class="profilehead" colspan="3" style="text-align: left">Advertising Outstanding Statement</td>
                    </tr>
                    <tr>
                        <td style="height: 19px"></td>
                        <td style="height: 19px"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label18" runat="server" Text="Advertising Mode" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlAdsOSAdsMode" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="Label20" runat="server" Text="Advertising Agency" Width="123px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:DropDownList ID="ddlAdsOSAdsAgency" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="Label21" runat="server" Text="No. Of Days" Width="117px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:DropDownList ID="ddlAdsOSAdsNoOfDays" runat="server">
                                <asp:ListItem Value="All">All</asp:ListItem>
                                <asp:ListItem Value="&lt;=30">Up To 30 Days</asp:ListItem>
                                <asp:ListItem Value="30-60">From 30 Days To 60 Days</asp:ListItem>
                                <asp:ListItem Value="60-90">From 60 Days To 90 Days</asp:ListItem>
                                <asp:ListItem Value="90-120">From 90 Days To 120 Days</asp:ListItem>
                                <asp:ListItem Value="&gt;120">More Than 120 Days</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 24px">
                            <asp:Button ID="btnAdsOutStandingRpt" runat="server" OnClick="btnAdsOutStandingRpt_Click" Text="Run Report" ValidationGroup="sos" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                </table>
                <table width="600" border="0" cellpadding="0" cellspacing="0" id="tblSDBGStatement" runat="server" visible="false">
                    <tr>
                        <td class="profilehead" colspan="3" style="text-align: left">SD/BG statement</td>
                    </tr>
                    <tr>
                        <td style="height: 19px"></td>
                        <td style="height: 19px"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label22" runat="server" Text="Customer Name" Width="103px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlSDBGCustomerName" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="Label23" runat="server" Text="Statement Of" Width="83px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:DropDownList ID="ddlSDBGStatementOf" runat="server">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem>Security Deposit</asp:ListItem>
                                <asp:ListItem>Bank Guarantees</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 24px">
                            <asp:Button ID="btnSDBGStatementRpt" runat="server" OnClick="btnSDBGStatementRpt_Click" Text="Run Report" ValidationGroup="sos" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                </table>
                <table width="600" border="0" cellpadding="0" cellspacing="0" id="tblSalesLeadsStatement" runat="server" visible="false">
                    <tr>
                        <td class="profilehead" colspan="3" style="text-align: left; height: 20px;">Sales Lead Statement</td>
                    </tr>
                    <tr>
                        <td style="height: 19px"></td>
                        <td style="height: 19px"></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label26" runat="server" Text="Company Name" Width="103px"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:DropDownList ID="ddlCompanyName" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label24" runat="server" Text="From" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtSalesLeadFrom" runat="server" type="datepic" CssClass="datetext" EnableTheming="False">
                            </asp:TextBox><%--<asp:Image
                                ID="imgSalesLeadFrom" runat="server" ImageUrl="~/Images/Calendar.png" />--%><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtSalesLeadFrom"
                                    ErrorMessage="Please Select the Sales Lead Date" ValidationGroup="SalesLead">*</asp:RequiredFieldValidator><asp:CustomValidator
                                        ID="CustomValidator5" runat="server" ClientValidationFunction="DateCustomValidate"
                                        ControlToValidate="txtSalesLeadFrom" ErrorMessage="Please Enter the Sales Lead Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                        SetFocusOnError="True" ValidationGroup="SalesLead">*</asp:CustomValidator><%--<cc1:CalendarExtender
                                            ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgSalesLeadFrom"
                                            TargetControlID="txtSalesLeadFrom">
                                    </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender7" runat="server" DisplayMoney="Left" Enabled="True"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtSalesLeadFrom" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label25" runat="server" Text="To" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtSalesLeadTo" runat="server" type="datepic" CssClass="datetext" EnableTheming="False">
                            </asp:TextBox><%--<asp:Image
                                ID="imgSalesLeadTo" runat="server" ImageUrl="~/Images/Calendar.png" />--%><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtSalesLeadTo"
                                    ErrorMessage="Please Select the Sales Lead Date" ValidationGroup="SalesLead">*</asp:RequiredFieldValidator><asp:CustomValidator
                                        ID="CustomValidator6" runat="server" ClientValidationFunction="DateCustomValidate"
                                        ControlToValidate="txtSalesLeadTo" ErrorMessage="Please Enter the Sales Lead Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                        SetFocusOnError="True" ValidationGroup="SalesLead">*</asp:CustomValidator><%--<cc1:CalendarExtender
                                            ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgSalesLeadTo"
                                            TargetControlID="txtSalesLeadTo">
                                    </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender8" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSalesLeadTo"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label33" runat="server" Text="Department" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlDeptSaleLead" runat="server" AutoPostBack="True" meta:resourcekey="ddlDepartmentResource1"
                                OnSelectedIndexChanged="ddlDeptSaleLead_SelectedIndexChanged" Width="154px">
                            </asp:DropDownList>&nbsp;<asp:RequiredFieldValidator
                                ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlDeptSaleLead"
                                ErrorMessage="Please Select the Quotation Department" ValidationGroup="SalesLead">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label34" runat="server" Text="Employee Name" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlEmployeeNameSalesLead" runat="server" Width="147px">
                            </asp:DropDownList>&nbsp;<asp:RequiredFieldValidator
                                ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlEmployeeNameSalesLead"
                                ErrorMessage="Please Select the Quotation Employee Name" ValidationGroup="SalesLead">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnSalesLeadStatement" runat="server" OnClick="btnSalesLeadStatement_Click" Text="Run Report" ValidationGroup="SalesLead" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="ValidationSummary6" runat="server" ValidationGroup="SalesLead" />
                        </td>
                    </tr>
                </table>
                <table width="600" border="0" cellpadding="0" cellspacing="0" id="tblSalesAssignment" runat="server" visible="false">
                    <tr>
                        <td class="profilehead" colspan="3" style="text-align: left;">Sales Assignment Statement</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label27" runat="server" Text="Company Name" Width="103px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlCompanyNameSalesAssign" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label28" runat="server" Text="From" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtSalesAssignFrom" runat="server" type="datepic" CssClass="datetext" EnableTheming="False"></asp:TextBox><%--<asp:Image
                                ID="imgSalesAssignFrom" runat="server" ImageUrl="~/Images/Calendar.png" />--%><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtSalesLeadFrom"
                                    ErrorMessage="Please Select the Sales Lead Date" ValidationGroup="SalesAssign">*</asp:RequiredFieldValidator><asp:CustomValidator
                                        ID="CustomValidator7" runat="server" ClientValidationFunction="DateCustomValidate"
                                        ControlToValidate="txtSalesLeadFrom" ErrorMessage="Please Enter the Sales Lead Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                        SetFocusOnError="True" ValidationGroup="SalesAssign">*</asp:CustomValidator><%--<cc1:CalendarExtender
                                            ID="CalendarExtender6" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgSalesAssignFrom"
                                            TargetControlID="txtSalesAssignFrom">
                                    </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender9" runat="server" DisplayMoney="Left" Enabled="True"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtSalesAssignFrom" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label29" runat="server" Text="To" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtSalesAssignTo" runat="server" type="datepic" CssClass="datetext" EnableTheming="False"></asp:TextBox><%--<asp:Image
                                ID="imgSalesAssignTo" runat="server" ImageUrl="~/Images/Calendar.png" />--%>&nbsp;<asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtSalesLeadTo"
                                    ErrorMessage="Please Select the Sales Lead Date" ValidationGroup="SalesAssign">*</asp:RequiredFieldValidator><asp:CustomValidator
                                        ID="CustomValidator8" runat="server" ClientValidationFunction="DateCustomValidate"
                                        ControlToValidate="txtSalesLeadTo" ErrorMessage="Please Enter the Sales Lead Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                        SetFocusOnError="True" ValidationGroup="SalesAssign">*</asp:CustomValidator><%--<cc1:CalendarExtender
                                            ID="CalendarExtender7" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgSalesAssignTo"
                                            TargetControlID="txtSalesAssignTo">
                                    </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender10" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSalesAssignTo"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label35" runat="server" Text="Department" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlDepSalesAssign" runat="server" AutoPostBack="True" meta:resourcekey="ddlDepartmentResource1"
                                OnSelectedIndexChanged="ddlDepSalesAssign_SelectedIndexChanged" Width="154px">
                            </asp:DropDownList>&nbsp;<asp:RequiredFieldValidator
                                ID="RequiredFieldValidator20" runat="server" ControlToValidate="ddlDepSalesAssign"
                                ErrorMessage="Please Select the Quotation Department" ValidationGroup="SalesAssign">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label36" runat="server" Text="Employee Name" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlEmpSalesAssign" runat="server" Width="147px">
                            </asp:DropDownList>&nbsp;<asp:RequiredFieldValidator
                                ID="RequiredFieldValidator21" runat="server" ControlToValidate="ddlEmployeeNameSalesLead"
                                ErrorMessage="Please Select the Quotation Employee Name" ValidationGroup="SalesAssign">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label30" runat="server" Text="Status" Width="103px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlStatus" runat="server">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="ALL">ALL</asp:ListItem>
                                <asp:ListItem Value="New">New</asp:ListItem>
                                <asp:ListItem>Open</asp:ListItem>
                                <asp:ListItem>Close</asp:ListItem>
                                <asp:ListItem>Regret</asp:ListItem>
                            </asp:DropDownList>&nbsp;<asp:RequiredFieldValidator
                                ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlStatus"
                                ErrorMessage="Please Select the Status" ValidationGroup="SalesAssign" InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnSalesAssignment" runat="server" OnClick="btnSalesAssignment_Click" Text="Run Report" ValidationGroup="SalesAssign" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="ValidationSummary7" runat="server" ValidationGroup="SalesAssign" />
                        </td>
                    </tr>
                </table>
                <table width="600" border="0" cellpadding="0" cellspacing="0" id="tblPurchase" runat="server" visible="false">
                    <tr>
                        <td class="profilehead" colspan="3" style="text-align: left;">Purchase Order Statement</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label37" runat="server" Text="Company Name" Width="103px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlPurchaseCmpName" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label138" runat="server" Text="From" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPurchaseFrom" runat="server" type="datepic" CssClass="datetext" EnableTheming="False">
                            </asp:TextBox><%--<asp:Image
                                ID="imgPurchaseFrom" runat="server" ImageUrl="~/Images/Calendar.png" />--%><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator223" runat="server" ControlToValidate="txtPurchaseFrom"
                                    ErrorMessage="Please Select the Purchase Order From Date" ValidationGroup="Purchase">*</asp:RequiredFieldValidator><asp:CustomValidator
                                        ID="CustomValidator11" runat="server" ClientValidationFunction="DateCustomValidate"
                                        ControlToValidate="txtPurchaseFrom" ErrorMessage="Please Enter the Sales Lead Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                        SetFocusOnError="True" ValidationGroup="Purchase">*</asp:CustomValidator><%--<cc1:CalendarExtender
                                            ID="CalendarExtender10" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgPurchaseFrom"
                                            TargetControlID="txtPurchaseFrom">
                                    </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender13" runat="server" DisplayMoney="Left" Enabled="True"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtPurchaseFrom" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label39" runat="server" Text="To" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPurchaseTo" runat="server" type="datepic" CssClass="datetext" EnableTheming="False">
                            </asp:TextBox><%--<asp:Image
                                ID="imgPurchaseTo" runat="server" ImageUrl="~/Images/Calendar.png" />--%>&nbsp;<asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtPurchaseTo"
                                    ErrorMessage="Please Select the Purchase Order To Date" ValidationGroup="Purchase">*</asp:RequiredFieldValidator><asp:CustomValidator
                                        ID="CustomValidator12" runat="server" ClientValidationFunction="DateCustomValidate"
                                        ControlToValidate="txtPurchaseTo" ErrorMessage="Please Enter the Sales Lead Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                        SetFocusOnError="True" ValidationGroup="Purchase">*</asp:CustomValidator><%--<cc1:CalendarExtender
                                            ID="CalendarExtender11" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgPurchaseTo"
                                            TargetControlID="txtPurchaseTo">
                                    </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender14" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtPurchaseTo"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label40" runat="server" Text="Department" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlPurchaseDepartment" runat="server" AutoPostBack="True" meta:resourcekey="ddlDepartmentResource1"
                                OnSelectedIndexChanged="ddlPurchaseDepartment_SelectedIndexChanged" Width="154px">
                            </asp:DropDownList>&nbsp;<asp:RequiredFieldValidator
                                ID="RequiredFieldValidator24" runat="server" ControlToValidate="ddlPurchaseDepartment"
                                ErrorMessage="Please Select the Quotation Department" ValidationGroup="Purchase">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label41" runat="server" Text="Employee Name" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlPurchaseEmpName" runat="server" Width="147px">
                            </asp:DropDownList>&nbsp;<asp:RequiredFieldValidator
                                ID="RequiredFieldValidator25" runat="server" ControlToValidate="ddlPurchaseEmpName"
                                ErrorMessage="Please Select the Quotation Employee Name" ValidationGroup="Purchase">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnPurchaseOrder" runat="server" Text="Run Report" ValidationGroup="SalesAssign" OnClick="btnPurchaseOrder_Click" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="ValidationSummary8" runat="server" ValidationGroup="Purchase" />
                        </td>
                    </tr>


                    <tr>
                        <td colspan="2">
                            
<table width="600" border="0" cellpadding="0" cellspacing="0" id="tblPurchase2" runat="server" visible="true">
                    <tr>
                        <td class="profilehead" colspan="3" style="text-align: left;">Purchase Order Statement By Sales Executive</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label42" runat="server" Text="Company Name" Width="103px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlPurchaseCmpName2" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label47" runat="server" Text="From" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPurchaseFrom2" runat="server" type="datepic" CssClass="datetext" EnableTheming="False">
                            </asp:TextBox>
							<asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator27" runat="server" ControlToValidate="txtPurchaseFrom2"
                                    ErrorMessage="Please Select the Purchase Order From Date" ValidationGroup="Purchase">*</asp:RequiredFieldValidator>
									<asp:CustomValidator
                                        ID="CustomValidator13" runat="server" ClientValidationFunction="DateCustomValidate"
                                        ControlToValidate="txtPurchaseFrom2" ErrorMessage="Please Enter the Sales Lead Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                        SetFocusOnError="True" ValidationGroup="Purchase2">*</asp:CustomValidator>
										
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label48" runat="server" Text="To" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPurchaseTo2" runat="server" type="datepic" CssClass="datetext" EnableTheming="False">
                            </asp:TextBox>
							&nbsp;<asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator30" runat="server" ControlToValidate="txtPurchaseTo2"
                                    ErrorMessage="Please Select the Purchase Order To Date" ValidationGroup="Purchase2">*</asp:RequiredFieldValidator>
									<asp:CustomValidator
                                        ID="CustomValidator15" runat="server" ClientValidationFunction="DateCustomValidate"
                                        ControlToValidate="txtPurchaseTo2" ErrorMessage="Please Enter the Sales Lead Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                        SetFocusOnError="True" ValidationGroup="Purchase2">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label49" runat="server" Text="Department" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlPurchaseDepartment2" runat="server" AutoPostBack="True" meta:resourcekey="ddlDepartmentResource2"
                                OnSelectedIndexChanged="ddlPurchaseDepartment2_SelectedIndexChanged" Width="154px">
                            </asp:DropDownList>&nbsp;<asp:RequiredFieldValidator
                                ID="RequiredFieldValidator31" runat="server" ControlToValidate="ddlPurchaseDepartment2"
                                ErrorMessage="Please Select the Quotation Department" ValidationGroup="Purchase2">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label50" runat="server" Text="Employee Name" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlPurchaseEmpName2" runat="server" Width="147px">
                            </asp:DropDownList>&nbsp;<asp:RequiredFieldValidator
                                ID="RequiredFieldValidator32" runat="server" ControlToValidate="ddlPurchaseEmpName2"
                                ErrorMessage="Please Select the Quotation Employee Name" ValidationGroup="Purchase2">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnPurchaseOrder2" runat="server" Text="Run Report" ValidationGroup="Purchase2" OnClick="btnPurchaseOrder2_Click" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="ValidationSummary10" runat="server" ValidationGroup="Purchase2" />
                        </td>
                    </tr>

        </table>
                        </td>
                    </tr>



                </table>
                <table width="600" border="0" cellpadding="0" cellspacing="0" id="tblInternalOrderstmt" runat="server" visible="false">
                    <tr>
                        <td class="profilehead" colspan="3" style="text-align: left; height: 20px;">Internal Order Statement</td>
                    </tr>
                    <tr>
                        <td style="height: 19px"></td>
                        <td style="height: 19px"></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label137" runat="server" Text="Company Name" Width="103px"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:DropDownList ID="ddlCompanyNameInternal" runat="server" AutoPostBack="True" CausesValidation="True">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator122" runat="server" ControlToValidate="ddlCompanyNameInternal"
                                ErrorMessage="Please Select the Company Name " InitialValue="0" ValidationGroup="Internal ">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label38" runat="server" Text="From" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtinternalsalsend" runat="server" type="datepic" CssClass="datetext" EnableTheming="False" CausesValidation="True"></asp:TextBox><%--<asp:Image
                                ID="Image3" runat="server" ImageUrl="~/Images/Calendar.png" />&nbsp;--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator123" runat="server" ControlToValidate="txtinternalsalsend"
                                ErrorMessage="Please Select the From Date " ValidationGroup="Internal ">*</asp:RequiredFieldValidator>
                            <%-- <cc1:CalendarExtender
                                            ID="CalendarExtender12" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="Image3"
                                            TargetControlID="txtinternalsalsend">
                                    </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender15" runat="server" DisplayMoney="Left" Enabled="True"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtinternalsalsend" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label139" runat="server" Text="To" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtinternalsalesto" runat="server" type="datepic" CssClass="datetext" EnableTheming="False" CausesValidation="True"></asp:TextBox><%--<asp:Image
                                ID="Image4" runat="server" ImageUrl="~/Images/Calendar.png" />&nbsp;--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator126" runat="server" ControlToValidate="txtinternalsalesto"
                                ErrorMessage="Please Select the ToDate " InitialValue="0" ValidationGroup="Internal ">*</asp:RequiredFieldValidator>
                            <%--<cc1:CalendarExtender
                                            ID="CalendarExtender13" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="Image4"
                                            TargetControlID="txtinternalsalesto">
                                    </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender16" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtinternalsalesto"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 22px;">
                            <asp:Label ID="Label140" runat="server" Text="Department" Width="117px"></asp:Label></td>
                        <td style="text-align: left; height: 22px;">
                            <asp:DropDownList ID="ddlDepartmentInternal" runat="server" AutoPostBack="True" meta:resourcekey="ddlDepartmentResource1"
                                OnSelectedIndexChanged="ddlDepartmentInternal_SelectedIndexChanged" Width="154px" CausesValidation="True">
                            </asp:DropDownList>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator84"
                                runat="server" ControlToValidate="ddlDepartmentInternal" ErrorMessage="Please select the Department Name "
                                ValidationGroup="Internal ">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 22px;">
                            <asp:Label ID="Label141" runat="server" Text="Employee Name" Width="117px"></asp:Label></td>
                        <td style="text-align: left; height: 22px;">
                            <asp:DropDownList ID="ddlEmployeeNameInternal" runat="server" Width="147px" AutoPostBack="True" CausesValidation="True">
                            </asp:DropDownList>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator45"
                                runat="server" ControlToValidate="ddlEmployeeNameInternal" ErrorMessage="Please Select the Employee Name "
                                InitialValue="0" ValidationGroup="Internal ">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnInternal" runat="server" OnClick="btnInternal_Click" Text="Order Report" ValidationGroup="Internal Order " /></td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;<asp:ValidationSummary ID="ValidationSummary28" runat="server" ValidationGroup="Internal "></asp:ValidationSummary>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"></td>
                    </tr>
                </table>
                <table width="600" border="0" cellpadding="0" cellspacing="0" id="tblDailyReport" runat="server" visible="false">
                    <tr>
                        <td class="profilehead" colspan="3" style="text-align: left; height: 20px;">Daily Report</td>
                    </tr>
                    <tr>
                        <td style="height: 19px"></td>
                        <td style="height: 19px"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label43" runat="server" Text="From" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtFromDailyRep" runat="server" type="datepic" CssClass="datetext" EnableTheming="False">
                            </asp:TextBox><%--<asp:Image
                                ID="imgFromDaily" runat="server" ImageUrl="~/Images/Calendar.png" />--%><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator26" runat="server" ControlToValidate="txtFromDailyRep"
                                    ErrorMessage="Please Select  Date" ValidationGroup="SalesLead">*</asp:RequiredFieldValidator><%--<cc1:CalendarExtender
                                            ID="CalendarExtender14" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgFromDaily"
                                            TargetControlID="txtFromDailyRep">
                                    </cc1:CalendarExtender>--%>
                            <%-- <cc1:MaskedEditExtender ID="MaskedEditExtender17" runat="server" DisplayMoney="Left" Enabled="True"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtFromDailyRep" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label44" runat="server" Text="To" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtToDailyRep" runat="server" type="datepic" CssClass="datetext" EnableTheming="False">
                            </asp:TextBox><%--<asp:Image
                                ID="imgToDaily" runat="server" ImageUrl="~/Images/Calendar.png" />--%><asp:CustomValidator
                                    ID="CustomValidator14" runat="server" ClientValidationFunction="DateCustomValidate"
                                    ControlToValidate="txtToDailyRep" ErrorMessage="Please Enter the TO  Daily Report Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                    SetFocusOnError="True" ValidationGroup="SalesLead">*</asp:CustomValidator><%--<cc1:CalendarExtender
                                            ID="CalendarExtender15" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgToDaily"
                                            TargetControlID="txtToDailyRep">
                                    </cc1:CalendarExtender>--%>
                            <%--<cc1:MaskedEditExtender ID="MaskedEditExtender18" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSalesLeadTo"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label45" runat="server" Text="Department" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlDepDaily" runat="server" AutoPostBack="True" meta:resourcekey="ddlDepartmentResource1"
                                OnSelectedIndexChanged="ddlDepDaily_SelectedIndexChanged" Width="154px">
                            </asp:DropDownList>&nbsp;<asp:RequiredFieldValidator
                                ID="RequiredFieldValidator28" runat="server" ControlToValidate="ddlDeptSaleLead"
                                ErrorMessage="Please Select the  Department" ValidationGroup="SalesLead">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label46" runat="server" Text="Employee Name" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlEmpDaily" runat="server" Width="147px">
                            </asp:DropDownList>&nbsp;<asp:RequiredFieldValidator
                                ID="RequiredFieldValidator29" runat="server" ControlToValidate="ddlEmployeeNameSalesLead"
                                ErrorMessage="Please Select the  Employee Name" ValidationGroup="SalesLead">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 23px">
                            <asp:Button ID="runDailyReport" runat="server" OnClick="runDailyReport_Click" Text="Run Report" ValidationGroup="SalesLead" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="ValidationSummary9" runat="server" ValidationGroup="SalesLead" />
                        </td>
                    </tr>
                </table>



                   <table id="tblReserveStockHistory" runat="server"  border="0" cellpadding="0" cellspacing="0" visible="false"
                    width="600">

                    <tr>
                        <td class="profilehead" style="text-align: left">Sales Report</td>
                    </tr>
                    <tr>
                        <td>
                            <table >
        <tr>
            <td style="width:150px; text-align :right">Location Name:</td>
                 <td><asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource3" DataTextField="locname" DataValueField="locid">
                </asp:DropDownList>
                     <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [locid], [locname] FROM [location_tbl]"></asp:SqlDataSource>
                 </td>
                 <td style="text-align :right; width:150px  ">From</td>    
                 <td style="text-align:left"><asp:TextBox ID="txtfrom" runat ="server" type="datepic" ></asp:TextBox></td>        
                     
        </tr>
        <tr>
                 <td style="text-align :right" >Company Name : </td>
                 <td style="text-align :left "><asp:DropDownList ID="ddlComp" runat="server" AutoPostBack="True"></asp:DropDownList></td>   
                 <td style="text-align :right ">To</td>   
                 <td>
                     <asp:TextBox ID="txtTo" runat ="server" type="datepic" ></asp:TextBox>
                </td>       
        </tr>
                                <tr>
                                    <td style="text-align: right">
                            <asp:Label ID="Label58" runat="server" Text="Department :"  ></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="True" CausesValidation="True"
                                OnSelectedIndexChanged="ddlDept_SelectedIndexChanged"
                                 >
                            </asp:DropDownList>
                            </td>
                                <td style="text-align: right">
                            <asp:Label ID="Label59" runat="server" Text="Employee Name"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlEmp" runat="server">
                            </asp:DropDownList>
                           </td>
                                </tr>
                                <tr>
                                <td style="text-align :right" >Type of Sales : </td>
                                    <td style ="text-align :left ">
                                        <asp:DropDownList ID="ddlSaleType" runat ="server" >
                                            <asp:ListItem Value ="0">All</asp:ListItem>
                                            <asp:ListItem Value ="1">Purchase order</asp:ListItem>
                                            <asp:ListItem Value ="2">Sample & Cash</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align :right" >Customer Name : </td>
                                    <td style ="text-align :left ">
                                        <asp:DropDownList ID="ddlCustSales" AutoPostBack ="true"  runat ="server" >
                                           
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="Button2" runat="server" Text="Run Report" OnClick="Button2_Click" /></td>

                                </tr>
        </table>
                        </td>
                    </tr>
                </table>


                <table id="tblArchitect" runat="server"  border="0" cellpadding="0" cellspacing="0" visible="false"
                    width="600">

                    <tr>
                        <td class="profilehead" style="text-align: left">Architects / Interior / Builders Report</td>
                    </tr>
                    <tr>
                        <td>
                            <table >
        <tr>
            <td style="width:150px; text-align :right">City:</td>
                 <td style="text-align :right" ><asp:DropDownList ID="txtCity" runat ="server" ></asp:DropDownList></td>
                         
                     
        </tr>
                <tr>
            <td style="width:150px; text-align :right">Pincode:</td>
                 <td style="text-align :right" ><asp:DropDownList ID="txtPincode" runat ="server" ></asp:DropDownList></td>
                         
                     
        </tr>   
                                <tr>
                                <td style="text-align :right" >Category : </td>
                                    <td style ="text-align :left ">
                                        <asp:DropDownList ID="ddlCategory" runat ="server" >
                                          </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnRunReport" runat="server" OnClick ="btnRunReport_Click" Text="Run Report" /></td>

                                </tr>
        </table>
                        </td>
                    </tr>
                </table>












                <table id="tblProforma" runat="server" border="0" cellpadding="0" cellspacing="0"
                    visible="false" width="600">
                    <tr>
                        <td class="profilehead" colspan="3" style="height: 20px; text-align: left">Customer Data Analysis</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label51" runat="server" Text="Requriment" Width="103px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlCompanyNameProforma" runat="server" AutoPostBack="True"
                                CausesValidation="True">
                                <asp:ListItem Value ="All">All</asp:ListItem>
                                <asp:ListItem Value ="Others">Others</asp:ListItem>
                            <asp:ListItem Value ="Renovation" >Renovation</asp:ListItem>
                            <asp:ListItem Value ="Replacement" >Replacement</asp:ListItem>
                            <asp:ListItem Value ="New Constructions" >New Constructions</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="ddlCompanyNameProforma"
                                ErrorMessage="Please Select the Company Name " InitialValue="0" ValidationGroup="Proforma" Visible="False">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label53" runat="server" Text="From" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtProformaFrom" runat="server" CausesValidation="True" type="datepic" CssClass="datetext"
                                EnableTheming="False">
                            </asp:TextBox><%--<asp:Image ID="imgProformaFrom" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>&nbsp;--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="txtProformaFrom"
                                ErrorMessage="Please Select the From Date " ValidationGroup="Proforma">*</asp:RequiredFieldValidator>
                            <%-- <cc1:CalendarExtender ID="CalendarExtender12" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                PopupButtonID="imgProformaFrom" TargetControlID="txtProformaFrom">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender15" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtProformaFrom"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label54" runat="server" Text="To" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtproformaTo" runat="server" CausesValidation="True" type="datepic" CssClass="datetext"
                                EnableTheming="False">
                            </asp:TextBox><%--<asp:Image ID="imgProformaTo" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>&nbsp;--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="txtproformaTo"
                                ErrorMessage="Please Select the ToDate " InitialValue="0" ValidationGroup="Proforma">*</asp:RequiredFieldValidator>
                            <%--  <cc1:CalendarExtender ID="CalendarExtender13" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                PopupButtonID="imgProformaTo" TargetControlID="txtproformaTo">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender16" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtproformaTo"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label55" runat="server" Text="Looking For" Width="117px" ></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlDepartmentProforma" runat="server" AutoPostBack="True" CausesValidation="True"
                                meta:resourcekey="ddlDepartmentResource1" 
                                Width="154px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="ddlDepartmentProforma"
                                ErrorMessage="Please select the Department Name " ValidationGroup="Proforma" Visible="False">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label56" runat="server" Text="Brand" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlEmployeeNameProforma" runat="server" AutoPostBack="True"
                                CausesValidation="True" Width="147px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="ddlEmployeeNameProforma"
                                ErrorMessage="Please Select the Employee Name " InitialValue="0" ValidationGroup="Proforma" Visible="False">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label57" runat="server" Text="Reference" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlReference" runat="server" AutoPostBack="True"
                                CausesValidation="True" Width="147px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="ddlReference"
                                ErrorMessage="Please Select the Reference Name " InitialValue="0" ValidationGroup="Proforma" Visible="False">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label60" runat="server" Text="Region" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True"
                                CausesValidation="True" Width="147px">
                            </asp:DropDownList>
                    </tr>
                    <tr>
                                <td colspan="4">
                                    <table id="tblpRint" runat="server" visible="false">
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkOriginal" runat="server" OnCheckedChanged="chkOriginal_CheckedChanged"
                                                    Text="Pie Chart" AutoPostBack="True"></asp:CheckBox></td>
                                            <td>
                                                <asp:CheckBox ID="chkDuplicate" runat="server" Text="Bar Chart" AutoPostBack="True" OnCheckedChanged="chkDuplicate_CheckedChanged"></asp:CheckBox></td>
                                            <td>
                                                <asp:CheckBox ID="chktriplicate"  runat="server" Text="Brand Pie Chart" AutoPostBack="True" OnCheckedChanged="chktriplicate_CheckedChanged"></asp:CheckBox></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="Button1" runat="server" Text="Run Report"
                                ValidationGroup="Proforma" OnClick="Button1_Click" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;<asp:ValidationSummary ID="ValidationSummary11" runat="server" ValidationGroup="Proforma"></asp:ValidationSummary>
                        </td>
                    </tr>
                </table>

                
                    
                <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label
                    ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                    Visible="False"></asp:Label>
                <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                    Visible="False"></asp:Label>
                <asp:Label ID="lblEmpId" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                    Visible="False"></asp:Label>
                <asp:Label ID="lblDeptId" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                    Visible="False"></asp:Label>
            </td>
            <td style="height: 2930px; width: 3px;"></td>
        </tr>
    </table>
</asp:Content>


 
