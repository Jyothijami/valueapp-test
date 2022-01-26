<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Services.aspx.cs" Inherits="Modules_Reports_Services" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td style="text-align: left">Service Reports</td>
        </tr>
    </table>
    <table id="Table2">
        <tr>
            <td valign="top">
                <table style="width: 175px; height: 60px; font-weight: bolder;" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="border-bottom: 1px solid #c0c0c0; font-size: 17px; padding-bottom: 11px; text-align: center;"></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnComplaintRegisterList" runat="server" CausesValidation="False"
                                CssClass="leftmenu" EnableTheming="False" OnClick="lbtnMenuLinks_Click">Complaint Record List</asp:LinkButton></td>

                        
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False"
                                CssClass="leftmenu" EnableTheming="False" OnClick="lbtnMenuLinks_Click">Courier Record List</asp:LinkButton></td>

                    </tr>

                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False"
                                CssClass="leftmenu" EnableTheming="False" OnClick="lbtnMenuLinks_Click">Complaint With Customer Details Report</asp:LinkButton></td>

                    </tr>
                     <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False"
                                CssClass="leftmenu" EnableTheming="False" OnClick="lbtnMenuLinks_Click">PO with Technical Drawings</asp:LinkButton></td>

                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="text-align: center" align="top" width="100%">
                <table id="tblComplaintRecordList" runat="server" border="0" cellpadding="0" cellspacing="0"
                    visible="false" width="600">
                    <tr>
                        <td class="profilehead" colspan="2" style="text-align: left">complaint record list</td>
                    </tr>
                    <tr>
                        <td style="height: 19px"></td>
                        <td style="height: 19px"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label6" runat="server" Text="From" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCRLFromDate" runat="server" type="datepic" EnableTheming="False"></asp:TextBox>
                            <%--<asp:Image
                                ID="imgCRLToFromDate" runat="server" ImageUrl="~/Images/Calendar.png" />--%>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtCRLFromDate"
                                ErrorMessage="Please Select the Invoice Date" ValidationGroup="crl">*</asp:RequiredFieldValidator><asp:CustomValidator
                                    ID="CustomValidator7" runat="server" ClientValidationFunction="DateCustomValidate"
                                    ControlToValidate="txtCRLFromDate" ErrorMessage="Please Enter the Invoice Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                    SetFocusOnError="True" ValidationGroup="crl">*</asp:CustomValidator><cc1:CalendarExtender
                                        ID="CalendarExtender7" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgCRLToFromDate"
                                        TargetControlID="txtCRLFromDate">
                                    </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender8" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtCRLFromDate"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label7" runat="server" Text="To" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCRLToDate" runat="server" type="datepic" EnableTheming="False"></asp:TextBox>
                            <%--<asp:Image
                                ID="imgCRLToDate" runat="server" ImageUrl="~/Images/Calendar.png" />--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtCRLToDate"
                                ErrorMessage="Please Select the Invoice Date" ValidationGroup="crl">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator
                                ID="CustomValidator8" runat="server" ClientValidationFunction="DateCustomValidate"
                                ControlToValidate="txtCRLToDate" ErrorMessage="Please Enter the Invoice Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                SetFocusOnError="True" ValidationGroup="crl">*</asp:CustomValidator><cc1:CalendarExtender
                                    ID="CalendarExtender8" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgCRLToDate"
                                    TargetControlID="txtCRLToDate">
                                </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender9" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtCRLToDate"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Location</td>
                        <td style="text-align: left"><asp:DropDownList ID="ddlCompanyMIDS0" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 24px">
                            <asp:Button ID="btnComplaintRecordListRpt" runat="server" OnClick="btnComplaintRecordListRpt_Click"
                                Text="Run Report" ValidationGroup="emdlist" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="crl" />
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2"></td>
                    </tr>
                </table>

                <table id="Table1" runat="server" border="0" cellpadding="0" cellspacing="0"
                    visible="false" width="600">
                    <tr>
                        <td class="profilehead" colspan="2" style="text-align: left">Courier Records list</td>
                    </tr>
                    <tr>
                        <td style="height: 19px"></td>
                        <td style="height: 19px"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label1" runat="server" Text="From" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtFrom2" runat="server" type="datepic" EnableTheming="False"></asp:TextBox>

                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFrom2"
                                ErrorMessage="Please Select the Invoice Date" ValidationGroup="crl1">*</asp:RequiredFieldValidator><asp:CustomValidator
                                    ID="CustomValidator1" runat="server" ClientValidationFunction="DateCustomValidate"
                                    ControlToValidate="txtFrom2" ErrorMessage="Please Enter the Invoice Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                    SetFocusOnError="True" ValidationGroup="crl">*</asp:CustomValidator><cc1:CalendarExtender
                                        ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="Image3"
                                        TargetControlID="txtFrom2">
                                    </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFrom2"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label2" runat="server" Text="To" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtTo2" runat="server" type="datepic" EnableTheming="False"></asp:TextBox>

                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTo2"
                                ErrorMessage="Please Select the Invoice Date" ValidationGroup="crl">*</asp:RequiredFieldValidator><asp:CustomValidator
                                    ID="CustomValidator2" runat="server" ClientValidationFunction="DateCustomValidate"
                                    ControlToValidate="txtTo2" ErrorMessage="Please Enter the Invoice Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                    SetFocusOnError="True" ValidationGroup="crl1">*</asp:CustomValidator><cc1:CalendarExtender
                                        ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="Image4"
                                        TargetControlID="txtTo2">
                                    </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtTo2"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 24px">
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click"
                                Text="Run Report" ValidationGroup="emdlist" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="crl1" />
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2"></td>
                    </tr>
                </table>

                <table id="tblCompRegByClient" runat="server" border="0" cellpadding="0"
                    cellspacing="0"
                    visible="false" width="600">
                    <tr>
                        <td class="profilehead" colspan="2" style="text-align: left">Complaint Register By Client's Name</td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                                                <asp:Label ID="lblSearch" runat="server" Text="Customer Search"></asp:Label></td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtSearchModel" runat="server"></asp:TextBox><asp:Button
                                                    ID="btnSearchModelNo" runat="server" BorderStyle="None" CausesValidation="False"
                                                    CssClass="gobutton" EnableTheming="False" OnClick="btnSearchModelNo_Click" Text="Go" /></td>
                    </tr>
                    <tr>
                    <td></td>
                    <td>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                            SelectCommand="USP_ServiceCustomer_SEARCH" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="txtSearchModel" Name="SearchValue" PropertyName="Text"
                                    Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                    <tr>
                        <td style="height: 19px; text-align: right;">
                            <asp:Label
                                ID="Label5" runat="server" Text="Client Name" Width="117px"></asp:Label></td>
                        <td style="height: 19px; text-align: left;">
                            <asp:DropDownList ID="ddlCust" runat="server" AutoPostBack="true" >
                            </asp:DropDownList>
                            <%--<asp:DropDownList ID="ddlCust" runat="server"></asp:DropDownList>--%>
                        </td>
                    </tr>
                    <%--<tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label8"
                                runat="server" Text="PO NO" Width="117px"></asp:Label></td>
                        <td style="text-align: left">--%>
                            <%--<asp:TextBox ID="txtClientId" runat="server" type="Text" EnableTheming="False"></asp:TextBox>--%>
                          <%--  <asp:DropDownList ID="ddlPONO" runat="server">
                                <asp:ListItem Value="0">ALL</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                    </tr>--%>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label3" runat="server" Text="From"
                                Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtFromdt" runat="server"
                                type="datepic" EnableTheming="False"></asp:TextBox>

                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator3" runat="server"
                                ControlToValidate="txtFromdt"
                                ErrorMessage="Please Select the Invoice Date"
                                ValidationGroup="crl1">*</asp:RequiredFieldValidator><asp:CustomValidator
                                    ID="CustomValidator3" runat="server"
                                    ClientValidationFunction="DateCustomValidate"
                                    ControlToValidate="txtFromdt"
                                    ErrorMessage="Please Enter the Invoice Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                    SetFocusOnError="True"
                                    ValidationGroup="crl">*</asp:CustomValidator><cc1:CalendarExtender
                                        ID="CalendarExtender3"
                                        runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="Image3"
                                        TargetControlID="txtFromdt">
                                    </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender3"
                                runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date"
                                TargetControlID="txtFromdt"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label4" runat="server" Text="To"
                                Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtTodt" runat="server" type="datepic"
                                EnableTheming="False"></asp:TextBox>

                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator4" runat="server"
                                ControlToValidate="txtTodt"
                                ErrorMessage="Please Select the Invoice Date"
                                ValidationGroup="crl">*</asp:RequiredFieldValidator><asp:CustomValidator
                                    ID="CustomValidator4" runat="server"
                                    ClientValidationFunction="DateCustomValidate"
                                    ControlToValidate="txtTo2"
                                    ErrorMessage="Please Enter the Invoice Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                    SetFocusOnError="True"
                                    ValidationGroup="crl1">*</asp:CustomValidator><cc1:CalendarExtender
                                        ID="CalendarExtender4"
                                        runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="Image4"
                                        TargetControlID="txtTodt">
                                    </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender4"
                                runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date"
                                TargetControlID="txtTodt"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label30"
                                runat="server" Text="Status" Width="103px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList
                                ID="ddlStatus" runat="server">
                                <%--<asp:ListItem Value="0">--</asp:ListItem>--%>
                                <asp:ListItem Value="ALL">All</asp:ListItem>
                                <asp:ListItem Value="Open">Open</asp:ListItem>
                                <asp:ListItem
                                    Value="Closed">Closed</asp:ListItem>
                            </asp:DropDownList>&nbsp;<asp:RequiredFieldValidator
                                ID="RequiredFieldValidator12" runat="server"
                                ControlToValidate="ddlStatus"
                                ErrorMessage="Please Select the Status"
                                ValidationGroup="SalesAssign"
                                InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Location</td>
                        <td style="text-align: left"><asp:DropDownList ID="ddlCompanyMIDS" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style ="text-align :right ">Run report with technician time duration</td>
                        <td style ="text-align :left"><asp:CheckBox ID="chktechrpt" runat ="server" OnCheckedChanged ="chktechrpt_CheckedChanged" AutoPostBack ="true"  /></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 24px">
                            <asp:Button ID="btnRunrpt" runat="server"
                                OnClick="btnRunrpt_Click"
                                Text="Run Report" ValidationGroup="emdlist" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="ValidationSummary2"
                                runat="server" ValidationGroup="crl1" />
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2"></td>
                    </tr>
                </table>
                <table id="tblPO" runat="server" border="0" cellpadding="0"
                    cellspacing="0"
                    visible="false" width="600">
                    <tr>
                        <td class="profilehead" colspan="2" style="text-align: left">Purchase Order with Technical Drawings</td>
                    </tr>
                    <tr>
                        <td>
                            <table>

                            
                    <tr>
                        <td style="height: 19px; text-align: right;">
                            <asp:Label ID="Label12" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="SO_NO">PO No</asp:ListItem>
                                            <asp:ListItem Value="SO_DATE">PO Date</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME">Customer</asp:ListItem>
                                            <asp:ListItem Value="CUST_CONTACT_PERSON">Contact Person</asp:ListItem>
                                            <asp:ListItem Value="CUST_EMAIL">EMail</asp:ListItem>
                                            <%--<asp:ListItem Value="QUOT_PO_FLAG">Status</asp:ListItem>--%>
                                        </asp:DropDownList></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList ID="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                            EnableTheming="False" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged"
                                            Visible="False" Width="50px">
                                            <asp:ListItem Selected="True">=</asp:ListItem>
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem>&gt;</asp:ListItem>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <asp:ListItem>&gt;=</asp:ListItem>
                                            <asp:ListItem>R</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="height: 25px">
                                        <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchValueFromDate" type="datepic" runat="server" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox>
                        </td>
                        <td style="height: 25px">
                                        <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchValueToDate" type="datepic" runat="server" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><%--<asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchValueToDate" runat="server"
                                            Enabled="False" PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>--%>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" />
                                        <asp:Button ID="btnPrint" runat ="server" OnClick="btnPrint_Click" Text="Print" />
                                    </td>
                    </tr>
                                </table>
                        </td>
                    </tr>
                    <tr>
            <td colspan="4">
                <asp:GridView ID="gvSalesOrderDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceId="sdsSalesOrderDetails"
                     Width="100%" SelectedRowStyle-BackColor="#c0c0c0" OnPageIndexChanging ="gvSalesOrderDetails_PageIndexChanging" OnRowDataBound ="gvSalesOrderDetails_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="SO_ID" HeaderText="SalesOrderIdHidden"></asp:BoundField>
                        <%--<asp:TemplateField HeaderText="PO No">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("SO_NO") %>' ID="TextBox1"></asp:TextBox>

                            </EditItemTemplate>

                            <ControlStyle Width="100px"></ControlStyle>

                            <ItemStyle Width="100px" HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle Width="100px" HorizontalAlign="Left"></HeaderStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnSalesOrderNo"  ForeColor="#0066ff" runat="server" Text='<%# Bind("SO_NO") %>' CausesValidation="False"></asp:LinkButton>
                            </ItemTemplate>

                            <FooterStyle Width="100px"></FooterStyle>
                        </asp:TemplateField>--%>
                        <asp:BoundField DataField ="SO_NO" HeaderText ="So No" />
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True" DataField="SO_DATE" HeaderText="PurchaseOrderDate"></asp:BoundField>
                        <asp:BoundField DataField="CUST_NAME" HeaderText="Customer">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CUST_CONTACT_PERSON" HeaderText="ContactPerson">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Executive" HeaderText="Executive Name">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PREPAREDBY" HeaderText="PreparedBy">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="APPROVEDBY" HeaderText="Obsoleted By">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="SO_ACCEPTANCE_FLAG" HeaderText="Status">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Full_CompName" HeaderText="Company Name">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                         <asp:BoundField DataField ="balanceqty" HeaderText ="Status" />
                       <%-- <asp:BoundField DataField ="balance_qty" HeaderText ="Bal Qty" />
                        <asp:BoundField DataField ="balance_qty" HeaderText ="Bal Qty" />--%>
                        <asp:TemplateField>
                  <HeaderTemplate>
                      <asp:CheckBox ID="checkbox" class="checkbox1" OnClick="selectAll(this)" runat="server" />
                  </HeaderTemplate>
                      <ItemTemplate>
                           <asp:CheckBox ID="checkbox1" class="checkbox1" runat="server"></asp:CheckBox>
                       </ItemTemplate>
                     </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        No Data Exist!
                    </EmptyDataTemplate>
                    <SelectedRowStyle BackColor="LightSteelBlue" />
                </asp:GridView>
                <asp:SqlDataSource ID="sdsSalesOrderDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SM_SALESORDER_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="EmpId" ControlID="lblEmpIdHidden"></asp:ControlParameter>
                        <asp:ControlParameter ControlID="lblCPID" Name="CPID" DefaultValue="0" PropertyName="Text" Type="Int64" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False">0</asp:Label>
                <asp:Label ID="lblSearchItemHidden"  Text="0" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchTypeHidden"  runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchValueHidden"  Text="0" runat="server" Visible="False"></asp:Label>
                 <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                    Visible="False"></asp:Label>
                            <asp:Label ID="lblCp_Ids" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                    Visible="False"></asp:Label>
            </td>
        </tr>
                </table>
            </td>

        </tr>
    </table>

</asp:Content>



