<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ServicesAssignments.aspx.cs" Inherits="Modules_Services_ServicesAssignments" Title="||ERP:Services:ServicesAssignments||" %>

<%@ Register Src="AMCServiceAssignments.ascx" TagName="AMCServiceAssignments" TagPrefix="uc1" %>
<%@ Register Src="InstallServiceAssignments.ascx" TagName="InstallServiceAssignments"
    TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <script>
        $(function () {
            $("[name$='txtCRDate'],[name$='txtPIDate'],[name$='txtPIDate'],[name$='txtPIDate']").datepicker();
        });
    </script>
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td style="height: 24px">Service Assignments</td>
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
        <tr>
            <td style="height: 24px">
                <asp:Button ID="Button1" runat="server" CssClass="midtab" OnClick="btnWarantytab_Click"
                    Text="Service" />
                <asp:Button ID="Button2" runat="server" OnClick="BtnAmc_Click" Text="AMC" />
                <asp:Button ID="Button3" runat="server" CssClass="midtab" OnClick="btnInstaltab_Click"
                    Text="Installation" /></td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td class="searchhead">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">Assigned
                                        enquiries list</td>
                        <td></td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="height: 25px">
                                        <asp:Label ID="Label1" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="CR_NO">Register No</asp:ListItem>
                                            <asp:ListItem Value="CR_DATE">CR Date</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME">Customer Name</asp:ListItem>
                                            <asp:ListItem Value="SERVICE_ASSIGN_DATE">Assign Date</asp:ListItem>
                                            <asp:ListItem Value="DUE_DATE">Due Date</asp:ListItem>
                                            <asp:ListItem Value="EMP_FIRST_NAME">Assigned To</asp:ListItem>
                                            <asp:ListItem Value="SERVICE_ASSIGN_STATUS">Status</asp:ListItem>
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
                                        <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False"></asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchValueFromDate" runat="server" EnableTheming="True" Visible="False"
                                            Width="106px"></asp:TextBox>
                                        <asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False" />
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchFrom" runat="server" Enabled="False" PopupButtonID="imgFromDate"
                                            TargetControlID="txtSearchValueFromDate">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate"
                                            UserDateFormat="MonthDayYear" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False"></asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px"></asp:TextBox><asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False" />
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchValueToDate" runat="server" Enabled="False" PopupButtonID="imgToDate"
                                            TargetControlID="txtSearchText">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                            UserDateFormat="MonthDayYear" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                                </tr>
                            </table>
                            <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label><asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label><asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label><asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:GridView ID="gvEnqAssignDetails" runat="server" AllowPaging="True"
                    AutoGenerateColumns="False" DataSourceID="sdsServiceDetails" AllowSorting="true"
                    OnRowDataBound="gvEnqAssignDetails_RowDataBound"
                    Width="100%">
                    <Columns>
                        <asp:BoundField DataField="SERVICE_ASSIGN_TASK_ID" SortExpression="SERVICE_ASSIGN_TASK_ID" HeaderText="AssignTaskIdHidden"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" DataField="CR_ID" SortExpression="CR_ID" HeaderText="CrIdHidden"></asp:BoundField>
                        <asp:TemplateField SortExpression="CR_NO" HeaderText="Register No">
                            <EditItemTemplate>
                                &nbsp; 
                            </EditItemTemplate>

                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnEnquiryNo" OnClick="lbtnEnquiryNo_Click" runat="server" Text='<%# Bind("CR_NO") %>' CausesValidation="False"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="CR_DATE" SortExpression="CR_DATE" HeaderText="Cr Date">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CR_CALL_TYPE" SortExpression="CR_CALL_TYPE" HeaderText="Call Type">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CUST_NAME" SortExpression="CUST_NAME" HeaderText="Customer Name">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="SERVICE_ASSIGN_DATE" SortExpression="SERVICE_ASSIGN_DATE" HeaderText="Assign Date"></asp:BoundField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="DUE_DATE" SortExpression="DUE_DATE" HeaderText="Due Date"></asp:BoundField>
                        <asp:BoundField DataField="EMP_FIRST_NAME" SortExpression="EMP_FIRST_NAME" HeaderText="Assigned To">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="SERVICE_ASSIGN_STATUS" SortExpression="SERVICE_ASSIGN_STATUS" HeaderText="Status">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        No Data Exist!
                    </EmptyDataTemplate>
                </asp:GridView>
                <asp:SqlDataSource ID="sdsServiceDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SERVICES_SERVICESASSIGNMENTS_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="EMPID" ControlID="lblEmpIdHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>
                    </SelectParameters>
                </asp:SqlDataSource>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="height: 19px">
                <table id="tblAssignmentDetails" runat="server" visible="False" border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr runat="server" id="Tr1">
                        <td class="profilehead" colspan="4" runat="server" id="Td1">Service Assignments Details</td>
                    </tr>
                    <tr runat="server" id="Tr2">
                        <td runat="server" id="Td2"></td>
                        <td runat="server" id="Td3"></td>
                        <td runat="server" id="Td4"></td>
                        <td runat="server" id="Td5"></td>
                    </tr>
                    <tr runat="server" id="Tr3">
                        <td style="text-align: right;" runat="server" id="Td6">
                            <asp:Label ID="Label2" runat="server" Text="Register No"></asp:Label></td>
                        <td style="text-align: left;" runat="server" id="Td7">
                            <asp:TextBox ID="txtEnquiryNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right;" runat="server" id="Td8">
                            <asp:Label ID="Label5" runat="server" Text="Register Date"></asp:Label></td>
                        <td style="text-align: left;" runat="server" id="Td9">
                            <asp:TextBox ID="txtEnquiryDate" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr runat="server" id="Tr4">
                        <td style="text-align: right" runat="server" id="Td10">
                            <asp:Label ID="Label3" runat="server" Text="Customer Name"></asp:Label></td>
                        <td style="text-align: left" runat="server" id="Td11">
                            <asp:TextBox ID="txtCustomerName" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right" runat="server" id="Td12">
                            <asp:Label ID="Label6" runat="server" Text="Assigned To"></asp:Label></td>
                        <td style="text-align: left" runat="server" id="Td13">
                            <asp:TextBox ID="txtAssignedTo" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr runat="server" id="Tr5">
                        <td style="text-align: right; height: 24px;" runat="server" id="Td14">
                            <asp:Label ID="Label4" runat="server" Text="Assigned Date"></asp:Label></td>
                        <td style="text-align: left; height: 24px;" runat="server" id="Td15">
                            <asp:TextBox ID="txtAssignedDate" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right; height: 24px;" runat="server" id="Td16">
                            <asp:Label ID="Label7" runat="server" Text="Due Date"></asp:Label></td>
                        <td style="text-align: left; height: 24px;" runat="server" id="Td17">
                            <asp:TextBox ID="txtDueDate" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr runat="server" id="Tr6">
                        <td style="text-align: right" runat="server" id="Td18">
                            <asp:Label ID="Label8" runat="server" Text="Status"></asp:Label></td>
                        <td style="text-align: left" runat="server" id="Td19">
                            <asp:TextBox ID="txtStatus" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right" runat="server" id="Td20">
                            <asp:Label ID="Label9" runat="server" Text="Call Type" Width="102px"></asp:Label></td>
                        <td style="text-align: left" runat="server" id="Td21">
                            <asp:TextBox ID="txtCallType" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 19px"></td>
        </tr>
        <tr>
            <td style="text-align: center">
                <table id="Table1">
                    <tr>
                        <td>&nbsp;<asp:Button ID="btnFollowUp" runat="server" CausesValidation="False" Text="Follow Up" OnClick="btnFollowUp_Click" Visible="False" /></td>
                        <td>
                            <asp:Button ID="btnSparesQuotation" runat="server" CausesValidation="False" Text="Spares Quotation"
                                Visible="False" Width="135px" OnClick="btnSparesQuotation_Click" /></td>
                        <td>
                            <asp:Button ID="btnSendQuotation" runat="server" CausesValidation="False" Text="AMC Quotation" OnClick="btnSendQuotation_Click" Visible="False" /></td>
                        <td>
                            <asp:Button ID="btnViewComplaint" runat="server" CausesValidation="False" Text="Complaint" OnClick="btnViewComplaint_Click" Visible="False" /></td>
                        <td>
                            <asp:Button ID="btnServiceRe" runat="server" Text="Service Report" OnClick="Button4_Click" CausesValidation="False" Visible="False" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <table id="tblComplaintRegister" runat="server" border="0" cellpadding="0" cellspacing="0"
                    visible="False" width="100%">
                    <tr id="Tr12" runat="server">
                        <td id="Td40" class="profilehead" colspan="4" runat="server">general details</td>
                    </tr>
                    <tr id="Tr16" runat="server">
                        <td id="Td41" style="text-align: right" runat="server"></td>
                        <td id="Td42" style="text-align: left" runat="server"></td>
                        <td id="Td43" style="text-align: right" runat="server"></td>
                        <td id="Td44" style="text-align: left" runat="server"></td>
                    </tr>
                    <tr id="Tr17" runat="server">
                        <td id="Td45" style="text-align: right" runat="server">
                            <asp:Label ID="lblQuotationNo" runat="server" Text="CR  No" Width="102px"></asp:Label></td>
                        <td id="Td46" style="text-align: left" runat="server">
                            <asp:TextBox ID="txtCRNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td id="Td47" style="text-align: right" runat="server">
                            <asp:Label ID="lblCRDate" runat="server" Text="CR Date"></asp:Label></td>
                        <td id="Td48" style="text-align: left" runat="server">
                            <asp:TextBox ID="txtCRDate" runat="server" CssClass="datetext" EnableTheming="False" ReadOnly="True"></asp:TextBox>

                        </td>
                    </tr>
                    <tr id="Tr18" runat="server">
                        <td id="Td49" style="text-align: right" runat="server">
                            <asp:Label ID="Label12" runat="server" Text="Call Type" Width="102px"></asp:Label></td>
                        <td id="Td50" style="text-align: left" runat="server">
                            <asp:DropDownList ID="ddlCallType" runat="server" Enabled="False">
                                <asp:ListItem>--</asp:ListItem>
                                <asp:ListItem>AMC</asp:ListItem>
                                <asp:ListItem>Installation</asp:ListItem>
                                <asp:ListItem>Non Warranty</asp:ListItem>
                                <asp:ListItem>Warranty</asp:ListItem>
                            </asp:DropDownList></td>
                        <td id="Td51" style="text-align: right" runat="server"></td>
                        <td id="Td52" style="text-align: left" runat="server"></td>
                    </tr>
                    <tr id="Tr13" runat="server">
                        <td id="Td22" style="height: 19px; text-align: right" runat="server"></td>
                        <td id="Td53" style="height: 19px; text-align: left" runat="server"></td>
                        <td id="Td54" style="height: 19px; text-align: right" runat="server"></td>
                        <td id="Td55" style="height: 19px; text-align: left" runat="server"></td>
                    </tr>
                    <tr id="Tr14" runat="server">
                        <td id="Td23" style="height: 22px; text-align: right" runat="server">
                            <asp:Label ID="lblCustomer" runat="server" Text="Customer"></asp:Label></td>
                        <td id="Td56" style="height: 22px; text-align: left" runat="server">
                            <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomerName_SelectedIndexChanged" Enabled="False">
                            </asp:DropDownList></td>
                        <td id="Td57" style="height: 22px; text-align: right" runat="server">
                            <asp:Label ID="lblRegion" runat="server" Text="Region"></asp:Label></td>
                        <td id="Td58" style="height: 22px; text-align: left" runat="server">
                            <asp:TextBox ID="txtRegion" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr id="Tr19" runat="server">
                        <td id="Td59" style="height: 22px; text-align: right" runat="server">
                            <asp:Label ID="Label13" runat="server" Text="Industry Type"></asp:Label></td>
                        <td id="Td60" style="height: 22px; text-align: left" runat="server">
                            <asp:TextBox ID="txtIndustryType" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td id="Td61" style="height: 22px; text-align: right" runat="server">
                            <asp:Label ID="lblInitName" runat="server" Text="Unit Name" Width="74px"></asp:Label></td>
                        <td id="Td62" style="height: 22px; text-align: left" runat="server">
                            <asp:DropDownList ID="ddlUnitName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged" Enabled="False">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="0">--Select Customer--</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr id="Tr20" runat="server">
                        <td id="Td63" style="text-align: right" runat="server">
                            <asp:Label ID="lblUnitAddress" runat="server" Text="Unit Address" Width="106px"></asp:Label></td>
                        <td id="Td64" colspan="3" style="text-align: left" runat="server">
                            <asp:TextBox ID="txtUnitAddress" runat="server" CssClass="multilinetext" EnableTheming="False"
                                Font-Names="Verdana" Font-Size="8pt" TextMode="MultiLine" Width="569px" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr id="Tr21" runat="server">
                        <td id="Td65" style="text-align: right" runat="server">
                            <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person"></asp:Label></td>
                        <td id="Td66" style="text-align: left" runat="server">
                            <asp:TextBox ID="txtContactPerson" runat="server" ReadOnly="True"></asp:TextBox><asp:DropDownList
                                ID="ddlContactPerson" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlContactPerson_SelectedIndexChanged"
                                Visible="False" Enabled="False">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="0">--Select Unit Name--</asp:ListItem>
                            </asp:DropDownList></td>
                        <td id="Td67" style="text-align: right" runat="server">
                            <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
                        <td id="Td68" style="text-align: left" runat="server">
                            <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr id="Tr15" runat="server">
                        <td id="TD28" style="text-align: right" runat="server">
                            <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No" Width="74px"></asp:Label></td>
                        <td id="Td69" style="text-align: left" runat="server">
                            <asp:TextBox ID="txtPhoneNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td id="Td70" style="text-align: right" runat="server">
                            <asp:Label ID="Label14" runat="server" Text="Mobile" Width="74px"></asp:Label></td>
                        <td id="Td71" style="text-align: left" runat="server">
                            <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr id="Tr22" runat="server">
                        <td id="Td72" style="height: 19px; text-align: right" runat="server"></td>
                        <td id="Td73" style="height: 19px; text-align: left" runat="server"></td>
                        <td id="Td74" style="height: 19px; text-align: right" runat="server"></td>
                        <td id="Td75" style="height: 19px; text-align: left" runat="server"></td>
                    </tr>
                    <tr id="Tr23" runat="server">
                        <td id="Td76" class="profilehead" colspan="4" runat="server">item details</td>
                    </tr>
                    <tr id="Tr7" runat="server">
                        <td colspan="4" style="text-align: center" runat="server" id="Td24">&nbsp;<asp:GridView ID="gvQuotationItems" runat="server" AutoGenerateColumns="False"
                            OnRowDataBound="gvQuotationItems_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ItemType" HeaderText="Item Type">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ItemName" HeaderText="Item Name">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SerialNo" NullDisplayText="-" HeaderText="Serial No."></asp:BoundField>
                                <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                <asp:BoundField DataField="ItemTypeId" HeaderText="ItemTypeIdHidden"></asp:BoundField>
                                <asp:BoundField DataField="NatureofComplaint" HeaderText="NatureofComplaint"></asp:BoundField>
                                <asp:BoundField DataField="RootCausedNotice" HeaderText="RootCausedNotice"></asp:BoundField>
                                <asp:BoundField DataField="CorrectiveActionTaken" HeaderText="CorrectiveActionTaken"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        </td>
                    </tr>
                    <tr id="Tr27" runat="server">
                        <td id="Td89" class="profilehead" colspan="4" runat="server" visible="false">complaint Details</td>
                    </tr>
                    <tr id="Tr28" runat="server">
                        <td id="Td90" style="text-align: right" runat="server" visible="false"></td>
                        <td id="Td91" style="text-align: left" runat="server" visible="false"></td>
                        <td id="Td92" style="text-align: right" runat="server" visible="false"></td>
                        <td id="Td93" style="text-align: left" runat="server" visible="false"></td>
                    </tr>
                    <tr id="Tr29" runat="server">
                        <td id="Td94" style="text-align: right" runat="server" visible="false">
                            <asp:Label ID="Label43" runat="server" Text="Nature of Complaint"></asp:Label></td>
                        <td id="Td95" colspan="3" style="text-align: left" runat="server" visible="false">
                            <asp:TextBox ID="txtNatureofComplaint" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="94%" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr id="Tr30" runat="server">
                        <td id="Td96" style="text-align: right" runat="server" visible="false">
                            <asp:Label ID="Label49" runat="server" Text="Root Cause Noticed"></asp:Label></td>
                        <td id="Td97" colspan="3" style="text-align: left" runat="server" visible="false">
                            <asp:TextBox ID="txtRootCause" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="94%" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr id="Tr31" runat="server">
                        <td id="Td98" style="text-align: right" runat="server" visible="false">
                            <asp:Label ID="Label18" runat="server" Text="Corrective Action Taken"></asp:Label></td>
                        <td id="Td99" colspan="3" style="text-align: left" runat="server" visible="false">
                            <asp:TextBox ID="txtCorrectiveAction" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="94%" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr id="Tr32" runat="server">
                        <td id="Td100" style="text-align: right" runat="server" visible="false"></td>
                        <td id="Td101" colspan="3" style="text-align: left" runat="server" visible="false"></td>
                    </tr>
                    <tr id="Tr33" runat="server">
                        <td id="Td102" class="profilehead" colspan="4" style="text-align: left" runat="server">Reference Details</td>
                    </tr>
                    <tr id="Tr34" runat="server">
                        <td id="Td103" style="height: 19px; text-align: right" runat="server"></td>
                        <td id="Td104" style="height: 19px; text-align: left" runat="server"></td>
                        <td id="Td105" style="height: 19px; text-align: right" runat="server"></td>
                        <td id="Td106" style="height: 19px; text-align: left" runat="server"></td>
                    </tr>
                    <tr id="Tr35" runat="server">
                        <td id="Td107" style="text-align: right" runat="server">
                            <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td id="Td108" style="text-align: left" runat="server">
                            <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                        <td id="Td109" style="text-align: right" runat="server"></td>
                        <td id="Td110" style="text-align: left" runat="server"></td>
                    </tr>
                    <tr id="Tr36" runat="server">
                        <td id="Td111" style="height: 19px" runat="server"></td>
                        <td id="Td112" style="height: 19px" runat="server"></td>
                        <td id="Td113" style="height: 19px" runat="server"></td>
                        <td id="Td114" style="height: 19px" runat="server"></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="tblFollowUp" runat="server" border="0" cellpadding="0" cellspacing="0"
                    visible="False" width="100%">
                    <tr runat="server" id="Tr8">
                        <td class="profilehead" colspan="2" runat="server" id="Td25">follow up details</td>
                    </tr>
                    <tr runat="server" id="Tr9">
                        <td style="height: 19px; text-align: right" runat="server" id="Td26"></td>
                        <td style="height: 19px; text-align: left" runat="server" id="Td27"></td>
                    </tr>
                    <tr runat="server" id="Tr10">
                        <td style="text-align: right" runat="server" id="Td29">
                            <asp:Label ID="Label24" runat="server" Text="Name" Width="76px"></asp:Label></td>
                        <td style="text-align: left" runat="server" id="Td30">
                            <asp:TextBox ID="txtFollowUpName" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr runat="server" id="Tr11">
                        <td style="text-align: right" runat="server" id="Td31">
                            <asp:Label ID="Label23" runat="server" Text="Follow Up Description" Width="76px"></asp:Label></td>
                        <td style="text-align: left" runat="server" id="Td32">
                            <asp:TextBox ID="txtFollowUpDesc" runat="server" CssClass="multilinetext" EnableTheming="False"
                                Height="40px" TextMode="MultiLine" Width="560px"></asp:TextBox></td>
                    </tr>
                    <tr runat="server" id="Tr24">
                        <td style="text-align: right" runat="server" id="Td33"></td>
                        <td runat="server" id="Td34"></td>
                    </tr>
                    <tr runat="server" id="Tr25">
                        <td colspan="2" runat="server" style="text-align: center" id="Td35">
                            <table id="Table2">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnFollowUpSave" runat="server" CausesValidation="False" OnClick="btnFollowUpSave_Click"
                                            Text="Save" /></td>
                                    <td>
                                        <asp:Button ID="btnFollowUpRefresh" runat="server" CausesValidation="False" OnClick="btnFollowUpRefresh_Click"
                                            Text="Refresh" /></td>
                                    <td>
                                        <asp:Button ID="btnFollowUpHistory" runat="server" CausesValidation="False" OnClick="btnFollowUpHistory_Click"
                                            Text="History" /></td>
                                    <td>
                                        <asp:Button ID="btnFollowUpClose" runat="server" CausesValidation="False" OnClick="btnFollowUpClose_Click"
                                            Text="Close" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server" id="Tr38">
                        <td colspan="2" runat="server" style="height: 376px" id="Td38">
                            <table id="tblFollowUpHistory" runat="server" border="0" cellpadding="0" cellspacing="0"
                                width="100%" visible="False">
                                <tr runat="server" id="Tr26">
                                    <td class="profilehead" colspan="3" runat="server" id="Td36" style="height: 17px">follow up history</td>
                                </tr>
                                <tr runat="server" id="Tr37">
                                    <td runat="server" style="text-align: center; height: 333px;" id="Td37">
                                        <asp:GridView ID="gvFollowUp" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            DataSourceID="sdsFollowDetails" OnRowDataBound="gvFollowUp_RowDataBound">
                                            <SelectedRowStyle BackColor="LightSteelBlue" />
                                            <Columns>
                                                <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="SFU_DATE" SortExpression="SFU_DATE" HeaderText="Date"></asp:BoundField>
                                                <asp:BoundField DataField="EMP_ID" SortExpression="EMP_ID" HeaderText="Name">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SFU_DESC" SortExpression="SFU_DESC" HeaderText="Description">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="sdsFollowDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                            SelectCommand="SELECT [SFU_DESC], [SFU_DATE], [EMP_ID] FROM [YANTRA_SERVICE_ASSIGN_FOLLOWUP_DET]"></asp:SqlDataSource>
                                        <asp:Label ID="lblAssignTaskIdHiddenForFollowUp" runat="server" Visible="False"></asp:Label>&nbsp;<br />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server" id="Tr39">
                        <td style="text-align: right" runat="server" id="Td39"></td>
                        <td runat="server" id="Td77"></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
</asp:Content>

 
