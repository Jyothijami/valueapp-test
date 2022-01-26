<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Copy of ComplaintRegister.ascx.cs"
    Inherits="Modules_Services_ascxComplaintRegister" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="../../App_Themes/Master/Master.css" rel="stylesheet" type="text/css" />

<script type="text/javascript"> 
function Serialno()
    {
      if(document.getElementById('<%=txtSerialNo.ClientID %>').value!="")
      {
        document.getElementById('<%=txtQuantity.ClientID %>').value="1";
         document.getElementById('<%=txtQuantity.ClientID %>').readOnly=false;
      }
      else
      {
        document.getElementById('<%=txtQuantity.ClientID %>').value="";
        document.getElementById('<%=txtQuantity.ClientID %>').readOnly=false;
      }
    }
</script>

<table border="0" cellpadding="0" cellspacing="0" width="750">
    <tr>
        <td colspan="4" style="text-align: left" class="searchhead">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="text-align: left">
                        Complaint Register</td>
                    <td>
                    </td>
                    <td style="text-align: right">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td rowspan="3" style="height: 25px">
                                    <asp:Label ID="Label112" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                        Text="Search By"></asp:Label></td>
                                <td rowspan="3" style="height: 25px">
                                    <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                        OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--</asp:ListItem>
                                        <asp:ListItem Value="CR_NO">CR No</asp:ListItem>
                                        <asp:ListItem Value="CR_DATE">CR Date</asp:ListItem>
                                        <asp:ListItem Value="CR_CALL_TYPE">Call Type</asp:ListItem>
                                        <asp:ListItem Value="CUST_NAME">Customer</asp:ListItem>
                                        <asp:ListItem Value="CUST_CONTACT_PERSON">Contact Person</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td rowspan="3" style="height: 25px">
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
                                <td rowspan="3" style="height: 25px">
                                    <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False"></asp:Label></td>
                                <td rowspan="3" style="height: 25px">
                                    <asp:TextBox ID="txtSearchValueFromDate" runat="server" EnableTheming="True" Visible="False"
                                        Width="106px"></asp:TextBox><asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                    <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchFrom" runat="server" Enabled="False"
                                        PopupButtonID="imgFromDate" TargetControlID="txtSearchValueFromDate">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server" Enabled="False"
                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate" UserDateFormat="MonthDayYear"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="">
                                    </cc1:MaskedEditExtender>
                                </td>
                                <td rowspan="3" style="height: 25px">
                                    <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False"></asp:Label></td>
                                <td rowspan="3" style="height: 25px">
                                    <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px"></asp:TextBox><asp:Image
                                        ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False"></asp:Image>
                                    <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchValueToDate" runat="server"
                                        Enabled="False" PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" Enabled="False"
                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText" UserDateFormat="MonthDayYear"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="">
                                    </cc1:MaskedEditExtender>
                                </td>
                                <td rowspan="3" style="height: 25px">
                                    <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                        CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                            </tr>
                            <tr>
                            </tr>
                            <tr>
                            </tr>
                        </table>
                        <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                            Visible="False"></asp:Label><asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                            Visible="False"></asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="4" align="center">
            <asp:GridView ID="gvComplaintRegister" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                DataSourceID="sdsComplaintRegister" OnRowDataBound="gvComplaintRegister_RowDataBound" Width="100%">
                <SelectedRowStyle BackColor="LightSteelBlue" />
                <EmptyDataTemplate>
                    No Data Exist!
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="CR_ID" HeaderText="CRIdHidden"></asp:BoundField>
                    <asp:TemplateField HeaderText="CR  No">
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("CR_NO") %>' ID="TextBox1"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Left" Width="100px"></HeaderStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnCRNo" runat="server" Text="<%# BIND('CR_NO') %>" CausesValidation="False"
                                OnClick="lbtnCRNo_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True"
                        DataField="CR_DATE" HeaderText="CR Date"></asp:BoundField>
                    <asp:BoundField DataField="CR_CALL_TYPE" SortExpression="CR_CALL_TYPE" HeaderText="Call Type">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="CUST_NAME" HeaderText="Customer">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="CUST_CONTACT_PERSON" HeaderText="Contact Person">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="PREPAREDBY" HeaderText="Prepared By">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="CUST_CORP_CONTACT_PERSON1" HeaderText="Customer Unit Contact Person" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsComplaintRegister" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                SelectCommand="SP_SERVICES_COMPLAINT_REGISTER_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName"
                        ControlID="lblSearchItemHidden"></asp:ControlParameter>
                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType"
                        ControlID="lblSearchTypeHidden"></asp:ControlParameter>
                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue"
                        ControlID="lblSearchValueHidden"></asp:ControlParameter>
                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom"
                        ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
                    <asp:ControlParameter ControlID="lblEmpIdHidden" DefaultValue="0" Name="EMPID" PropertyName="Text"
                        Type="String" />
                    <asp:ControlParameter ControlID="lblUserType" DefaultValue="0" Name="UserType" PropertyName="Text"
                        Type="Int64" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
    </tr>
    <tr>
        <td colspan="4" align="center">
            <table id="Table1">
                <tr>
                    <td>
                        <asp:Button ID="btnNew" runat="server" CausesValidation="False" OnClick="btnNew_Click"
                            Text="New" /></td>
                    <td>
                        <asp:Button ID="btnEdit" runat="server" CausesValidation="False" OnClick="btnEdit_Click"
                            Text="Edit" /></td>
                    <td>
                        <asp:Button ID="btnDelete" runat="server" CausesValidation="False" OnClick="btnDelete_Click"
                            Text="Delete" /></td>
                    <td style="width: 3px">
                        <asp:Button ID="btnAssign" runat="server" CausesValidation="False" OnClick="btnAssign_Click"
                            Text="Assign" /></td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <table id="tblAssignTasks" runat="server" border="0" cellpadding="0" cellspacing="0"
                visible="False" width="100%">
                <tr id="Tr1" runat="server">
                    <td id="Td1">
                    </td>
                    <td id="Td2" style="text-align: left">
                    </td>
                    <td id="Td3" style="text-align: right">
                    </td>
                    <td id="Td4" style="text-align: left">
                    </td>
                </tr>
                <tr id="Tr2" runat="server">
                    <td id="Td151" style="text-align: right">
                        <asp:Label ID="Label19" runat="server" CssClass="label" Font-Bold="False" Text="Assign Task No"></asp:Label></td>
                    <td id="Td6" style="text-align: left">
                        <asp:TextBox ID="txtAssignTaskNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                    <td id="Td7" style="text-align: right">
                    </td>
                    <td id="Td8" style="text-align: left">
                    </td>
                </tr>
                <tr id="Tr3" runat="server">
                    <td id="Td9" style="text-align: right">
                        <asp:Label ID="Label5" runat="server" CssClass="label" Font-Bold="False" Text="Register No"></asp:Label></td>
                    <td id="Td10" style="text-align: left">
                        <asp:TextBox ID="txtEnquiryNoForAssign" runat="server" ReadOnly="True"></asp:TextBox></td>
                    <td id="Td11" style="text-align: right">
                        &nbsp;<asp:Label ID="Label13" runat="server" CssClass="label" Font-Bold="False" Text="Register Date"></asp:Label></td>
                    <td id="Td12" style="text-align: left">
                        <asp:TextBox ID="txtEnquiryDateForAssign" runat="server" ReadOnly="True"></asp:TextBox>
                        <cc1:MaskedEditExtender ID="meeEnquiryDateForAssing" runat="server" Mask="99/99/9999"
                            MaskType="Date" TargetControlID="txtEnquiryDateForAssign" UserDateFormat="MonthDayYear"
                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                            CultureTimePlaceholder="" Enabled="True">
                        </cc1:MaskedEditExtender>
                    </td>
                </tr>
                <tr id="Tr4" runat="server">
                    <td id="Td13" style="text-align: right">
                        <asp:Label ID="Label15" runat="server" CssClass="label" Font-Bold="False" Text="Customer Name"></asp:Label></td>
                    <td id="Td14" style="text-align: left">
                        <asp:TextBox ID="txtCustomerNameForAssingn" runat="server" ReadOnly="True"></asp:TextBox></td>
                    <td id="Td15" style="text-align: right">
                        <asp:Label ID="Label16" runat="server" CssClass="label" Font-Bold="False" Text="Contact E-Mail"></asp:Label></td>
                    <td id="Td16" style="text-align: left">
                        <asp:TextBox ID="txtCustomerEmailForAssingn" runat="server" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr id="Tr5" runat="server">
                    <td id="Td17" style="text-align: right">
                        <asp:Label ID="Label17" runat="server" CssClass="label" Font-Bold="False" Text="Employee Name"></asp:Label></td>
                    <td id="Td118" style="text-align: left">
                        <asp:DropDownList ID="ddlEmpNameForAssign" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpNameForAssign_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Label ID="Label20" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlEmpNameForAssign"
                            ErrorMessage="Please Select the Employee Name" InitialValue="0" ValidationGroup="assign">*</asp:RequiredFieldValidator></td>
                    <td id="Td19" style="text-align: right">
                        <asp:Label ID="Label18" runat="server" CssClass="label" Font-Bold="False" Text="Employee EMail"></asp:Label></td>
                    <td id="Td20" style="text-align: left">
                        <asp:TextBox ID="txtEmpEmailId" runat="server" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr id="Tr6" runat="server">
                    <td id="Td21" style="height: 52px; text-align: right">
                        <asp:Label ID="Label28" runat="server" CssClass="label" Font-Bold="False" Text="Remarks"></asp:Label></td>
                    <td id="Td22" colspan="3" style="height: 52px; text-align: left">
                        <asp:TextBox ID="txtRemarksForAssingn" runat="server" CssClass="multilinetext" EnableTheming="False"
                            TextMode="MultiLine" Width="83%"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator9"
                                runat="server" ControlToValidate="txtRemarksForAssingn" ErrorMessage="Please Enter Remarks"
                                ValidationGroup="assign">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr id="Tr7" runat="server">
                    <td id="Td23" style="text-align: right">
                    </td>
                    <td id="Td24" style="text-align: left">
                    </td>
                    <td id="Td25" style="text-align: right">
                    </td>
                    <td id="Td26" style="text-align: left">
                    </td>
                </tr>
                <tr id="Tr8" runat="server">
                    <td id="Td27" style="text-align: right">
                        <asp:Label ID="Label27" runat="server" CssClass="label" Font-Bold="False" Text="Assign Date"></asp:Label></td>
                    <td id="Td281" style="text-align: left">
                        <asp:TextBox ID="txtAssignDate" runat="server" CssClass="datetext" EnableTheming="False"
                            Width="130px"></asp:TextBox><asp:Image ID="imgAssignDate" runat="server" ImageUrl="~/Images/Calendar.png">
                            </asp:Image>
                        <asp:Label ID="Label21" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtAssignDate"
                            ErrorMessage="Please Select Assign Date" ValidationGroup="assign">*</asp:RequiredFieldValidator><cc1:CalendarExtender
                                ID="ceAssignDate" runat="server" PopupButtonID="imgAssignDate" TargetControlID="txtAssignDate"
                                Enabled="True" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="meeAssingDate" runat="server" Mask="99/99/9999" MaskType="Date"
                            TargetControlID="txtAssignDate" UserDateFormat="MonthDayYear" CultureAMPMPlaceholder=""
                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                            Enabled="True">
                        </cc1:MaskedEditExtender>
                    </td>
                    <td id="Td29" style="text-align: right">
                        <asp:Label ID="Label7" runat="server" CssClass="label" Font-Bold="False" Text="Due Date"></asp:Label></td>
                    <td id="Td30" style="text-align: left">
                        <asp:TextBox ID="txtDueDate" runat="server" CssClass="datetext" EnableTheming="False"
                            Width="130px"></asp:TextBox><asp:Image ID="imgDueDate" runat="server" ImageUrl="~/Images/Calendar.png">
                            </asp:Image>
                        <asp:Label ID="Label22" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDueDate"
                            ErrorMessage="Please Select Due Date">*</asp:RequiredFieldValidator><asp:CompareValidator
                                ID="CompareValidator1" runat="server" ControlToCompare="txtAssignDate" ControlToValidate="txtDueDate"
                                ErrorMessage="Due Date should not be less than Assign Date" Operator="GreaterThanEqual"
                                SetFocusOnError="True" Type="Date" ValidationGroup="assign">*</asp:CompareValidator><cc1:CalendarExtender
                                    ID="ceDueDate" runat="server" PopupButtonID="imgDueDate" TargetControlID="txtDueDate"
                                    Enabled="True" Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="meeDueDate" runat="server" Mask="99/99/9999" MaskType="Date"
                            TargetControlID="txtDueDate" UserDateFormat="MonthDayYear" CultureAMPMPlaceholder=""
                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                            Enabled="True">
                        </cc1:MaskedEditExtender>
                    </td>
                </tr>
                <tr id="Tr9" runat="server">
                    <td id="Td31" style="text-align: right">
                    </td>
                    <td id="Td32" style="text-align: left">
                    </td>
                    <td id="Td33" style="text-align: right">
                    </td>
                    <td id="Td34" style="text-align: left">
                    </td>
                </tr>
                <tr id="Tr10" runat="server">
                    <td id="Td35" colspan="4" style="text-align: center">
                        <table id="Table2">
                            <tr>
                                <td>
                                    <asp:Button ID="btnAssignTask" runat="server" OnClick="btnAssignTask_Click" Text="Assign"
                                        ValidationGroup="assign" /></td>
                                <td>
                                    <asp:Button ID="btnCloseAssignTask" runat="server" CausesValidation="False" OnClick="btnCancelTask_Click"
                                        Text="Close" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="Tr11" runat="server">
                    <td id="Td36" style="text-align: right">
                    </td>
                    <td id="Td37" style="text-align: left">
                    </td>
                    <td id="Td38" style="text-align: right">
                    </td>
                    <td id="Td39" style="text-align: left">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="4" id="TD116" style="height: 417px">
            <table border="0" cellpadding="0" cellspacing="0" id="tblComplaintRegister" runat="server"
                visible="False" width="100%">
                <tr id="Tr12" runat="server">
                    <td class="profilehead" colspan="4" id="Td40">
                        general details</td>
                </tr>
                <tr runat="server" id="Tr16">
                    <td style="text-align: right" id="Td41">
                    </td>
                    <td style="text-align: left" id="Td42">
                    </td>
                    <td style="text-align: right" id="Td43">
                    </td>
                    <td style="text-align: left" id="Td44">
                    </td>
                </tr>
                <tr runat="server" id="Tr17">
                    <td style="text-align: right" id="Td45">
                        <asp:Label ID="lblQuotationNo" runat="server" Text="CR  No" Width="102px"></asp:Label></td>
                    <td style="text-align: left" id="Td46">
                        <asp:TextBox ID="txtCRNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                    <td style="text-align: right;" id="Td47">
                        <asp:Label ID="lblCRDate" runat="server" Text="CR Date"></asp:Label></td>
                    <td style="text-align: left" id="Td48">
                        <asp:TextBox ID="txtCRDate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox><asp:Image
                            ID="imgCRDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceCRDate" runat="server" PopupButtonID="imgCRDate"
                            TargetControlID="txtCRDate" Enabled="True">
                        </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="meeCRDate" runat="server" Mask="99/99/9999" MaskType="Date"
                            TargetControlID="txtCRDate" UserDateFormat="MonthDayYear" CultureAMPMPlaceholder=""
                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                            Enabled="True">
                        </cc1:MaskedEditExtender>
                    </td>
                </tr>
                <tr runat="server" id="Tr18">
                    <td style="text-align: right;" id="Td49">
                        <asp:Label ID="Label1" runat="server" Text="Call Type" Width="102px"></asp:Label></td>
                    <td style="text-align: left;" id="Td50">
                        <asp:DropDownList ID="ddlCallType" runat="server">
                            <asp:ListItem>--</asp:ListItem>
                            <asp:ListItem>AMC</asp:ListItem>
                            <asp:ListItem>Installation</asp:ListItem>
                            <asp:ListItem>Non Warranty</asp:ListItem>
                            <asp:ListItem>Warranty</asp:ListItem>
                        </asp:DropDownList></td>
                    <td style="text-align: right;" id="Td51">
                    </td>
                    <td style="text-align: left;" id="Td52">
                    </td>
                </tr>
                <tr runat="server" id="Tr13">
                    <td id="TD5" style="text-align: right">
                        <asp:Label ID="Label14" runat="server" Text="Select" Width="102px"></asp:Label></td>
                    <td style="text-align: left" id="Td53">
                        <asp:RadioButtonList ID="rbCustomerType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbCustomerType_SelectedIndexChanged"
                            RepeatDirection="Horizontal" Width="374px" TabIndex="1">
                            <asp:ListItem>New</asp:ListItem>
                            <asp:ListItem>Existing</asp:ListItem>
                            <asp:ListItem>PO</asp:ListItem>
                            <asp:ListItem>DC</asp:ListItem>
                            <asp:ListItem>Invoice</asp:ListItem>
                        </asp:RadioButtonList></td>
                    <td style="text-align: right" id="Td54">
                    </td>
                    <td style="text-align: left" id="Td55">
                    </td>
                </tr>
                <tr runat="server" id="Tr28">
                    <td style="text-align: right">
                        <asp:Label ID="lblSelect" runat="server" Visible="False"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlSelect" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSelect_SelectedIndexChanged" Visible="False">
                        </asp:DropDownList></td>
                    <td style="text-align: right">
                    </td>
                    <td style="text-align: left">
                    </td>
                </tr>
                <tr id="Tr41" runat="server">
                    <td style="text-align: right">
                        <asp:Label ID="lblSearch" runat="server" Enabled="False" Text="Search"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtSearchModel" runat="server" Enabled="False"></asp:TextBox><asp:Button
                            ID="btnSearchModelNo" runat="server" BorderStyle="None" CausesValidation="False"
                            CssClass="gobutton" EnableTheming="False" OnClick="btnSearchModelNo_Click" Text="Go" /></td>
                    <td style="text-align: right">
                    </td>
                    <td style="text-align: left">
                    </td>
                </tr>
                <tr runat="server" id="Tr14">
                    <td id="TD18" style="text-align: right">
                        <asp:Label ID="lblCustomer" runat="server" Text="Customer"></asp:Label></td>
                    <td style="text-align: left" id="Td56">
                        <asp:TextBox ID="txtCustomer" runat="server" Visible="False"></asp:TextBox><asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomerName_SelectedIndexChanged">
                        </asp:DropDownList><asp:Label ID="Label31" runat="server" EnableTheming="False" ForeColor="Red"
                            Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCustomerName"
                            ErrorMessage="Please Select the Customer" InitialValue="0">*</asp:RequiredFieldValidator><br />
                    </td>
                    <td style="text-align: right" id="Td57">
                        <asp:Label ID="lblRegion" runat="server" Text="Region"></asp:Label></td>
                    <td style="text-align: left" id="Td58">
                        <asp:TextBox ID="txtRegion" runat="server" ReadOnly="True"></asp:TextBox><asp:DropDownList
                            ID="ddlRegion" runat="server" meta:resourcekey="ddlRegionResource1" Visible="False">
                        </asp:DropDownList><asp:Label ID="Label32" runat="server" EnableTheming="False" ForeColor="Red"
                            meta:resourcekey="Label8Resource1" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlRegion" ErrorMessage="Please Select Region "
                                InitialValue="0" Visible="False">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr runat="server" id="Tr19">
                    <td style="text-align: right" id="Td59">
                        <asp:Label ID="Label9" runat="server" Text="Industry Type"></asp:Label></td>
                    <td style="text-align: left" id="Td60">
                        <asp:TextBox ID="txtIndustryType" runat="server" ReadOnly="True"></asp:TextBox><asp:DropDownList
                            ID="ddlIndustryType" runat="server" meta:resourcekey="ddlIndustryTypeResource1"
                            Visible="False" Width="79px">
                        </asp:DropDownList><asp:Label ID="Label30" runat="server" EnableTheming="False" ForeColor="Red"
                            meta:resourcekey="Label22Resource1" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlIndustryType"
                                ErrorMessage="Please Select the Industry Type" InitialValue="0" meta:resourcekey="RequiredFieldValidator9Resource1"
                                Text="*" Visible="False"></asp:RequiredFieldValidator></td>
                    <td style="text-align: right" id="Td61">
                        <asp:Label ID="lblInitName" runat="server" Text="Unit Name" Width="74px"></asp:Label></td>
                    <td style="text-align: left" id="Td62">
                        <asp:TextBox ID="txtUnitName" runat="server" Visible="False"></asp:TextBox><asp:DropDownList ID="ddlUnitName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged">
                            <asp:ListItem Value="0">--</asp:ListItem>
                            <asp:ListItem Value="0">--Select Customer--</asp:ListItem>
                        </asp:DropDownList><asp:RequiredFieldValidator ID="rfvUnitName" runat="server" ControlToValidate="ddlUnitName"
                            ErrorMessage="Please Select the Unit Name" InitialValue="0">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr runat="server" id="Tr20">
                    <td style="text-align: right" id="Td63">
                        <asp:Label ID="lblUnitAddress" runat="server" Text="Unit Address" Width="106px"></asp:Label></td>
                    <td colspan="3" style="text-align: left" id="Td64">
                        <asp:TextBox ID="txtUnitAddress" runat="server" CssClass="multilinetext" EnableTheming="False"
                            Font-Names="Verdana" Font-Size="8pt" TextMode="MultiLine" Width="569px" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr runat="server" id="Tr21">
                    <td style="text-align: right" id="Td65">
                        <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person"></asp:Label></td>
                    <td style="text-align: left" id="Td66">
                        <asp:TextBox ID="txtContactPerson" runat="server" Visible="False"></asp:TextBox><asp:DropDownList
                            ID="ddlContactPerson" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlContactPerson_SelectedIndexChanged">
                            <asp:ListItem Value="0">--</asp:ListItem>
                            <asp:ListItem Value="0">--Select Unit Name--</asp:ListItem>
                        </asp:DropDownList><asp:RequiredFieldValidator ID="rfvContactPerson" runat="server"
                            ControlToValidate="ddlContactPerson" ErrorMessage="Please Select the Contact Person"
                            InitialValue="0">*</asp:RequiredFieldValidator></td>
                    <td style="text-align: right" id="Td67">
                        <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
                    <td style="text-align: left" id="Td68">
                        <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr runat="server" id="Tr15">
                    <td id="TD28" style="text-align: right">
                        <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No" Width="74px"></asp:Label></td>
                    <td style="text-align: left" id="Td69">
                        <asp:TextBox ID="txtPhoneNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                    <td style="text-align: right" id="Td70">
                        <asp:Label ID="Label8" runat="server" Text="Mobile" Width="74px"></asp:Label></td>
                    <td style="text-align: left" id="Td71">
                        <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr runat="server" id="Tr22">
                    <td style="text-align: right;" id="Td72">
                    </td>
                    <td style="text-align: left;" id="Td73">
                    </td>
                    <td style="text-align: right;" id="Td74">
                    </td>
                    <td style="text-align: left;" id="Td75">
                    </td>
                </tr>
                <tr runat="server" id="Tr27">
                    <td colspan="4" style="text-align: right">
                        <asp:GridView ID="gvOrderAcceptanceItems" runat="server" AutoGenerateColumns="False"
                            OnRowDataBound="gvOrderAcceptanceItems_RowDataBound1" Width="100%" OnRowDeleting="gvOrderAcceptanceItems_RowDeleting" OnRowEditing="gvOrderAcceptanceItems_RowEditing">
                            <Columns>
                                <asp:CommandField ShowEditButton="True" />
                                <asp:CommandField ShowDeleteButton="True" />
                                <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                                <asp:BoundField DataField="ModelNo" HeaderText="Model No" />
                                <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                <asp:BoundField DataField="UOM" HeaderText="UOM" />
                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                <asp:BoundField DataField="Rate" HeaderText="Rate" />
                                <asp:BoundField HeaderText="Amount" />
                                <asp:BoundField DataField="Specifications" HeaderText="Specifications" />
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                <asp:BoundField DataField="Priority" HeaderText="Priority" />
                                <asp:BoundField DataField="DeliveryDate" HeaderText="Delivery Date" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" />
                                <asp:BoundField DataField="Room" HeaderText="Room" />
                                <asp:BoundField DataField="Price" HeaderText="Price" />
                            </Columns>
                        </asp:GridView>
                        <asp:GridView ID="gvDeliveryChallanItems" runat="server" AutoGenerateColumns="False"
                            Width="100%" OnRowDataBound="gvDeliveryChallanItems_RowDataBound" OnRowDeleting="gvDeliveryChallanItems_RowDeleting" OnRowEditing="gvDeliveryChallanItems_RowEditing">
                            <Columns>
                                <asp:CommandField ShowEditButton="True" />
                                <asp:CommandField ShowDeleteButton="True" />
                                <asp:BoundField DataField="DC No" HeaderText="DC No" />
                                <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                                <asp:BoundField DataField="ModelNo" HeaderText="Model No" />
                                <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                <asp:BoundField DataField="UOM" HeaderText="UOM" />
                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                <asp:BoundField DataField="Rate" HeaderText="Rate" />
                                <asp:BoundField DataField="SPPrice" HeaderText="Special Price " />
                                <asp:BoundField DataField="DeliveryDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Delivery Date"
                                    HtmlEncode="False" />
                            </Columns>
                        </asp:GridView>
                        <asp:GridView ID="gvItmDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvItmDetails_RowDataBound" OnRowDeleting="gvItmDetails_RowDeleting" OnRowEditing="gvItmDetails_RowEditing">
                            <Columns>
                                <asp:CommandField ShowEditButton="True" />
                                <asp:CommandField ShowDeleteButton="True" />
                                <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                                <asp:BoundField DataField="ModelNo" HeaderText="Model No" />
                                <asp:BoundField DataField="ItemName" HeaderText="Item Name">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="UOM" HeaderText="UOM">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                <asp:BoundField DataField="Rate" HeaderText="Rate" />
                                <asp:BoundField DataField="Vat" HeaderText="Vat" />
                                <asp:BoundField DataField="CST" HeaderText="CST" />
                                <asp:BoundField DataField="Excise" HeaderText="Excise" />
                                <asp:BoundField HeaderText="Amount" />
                                <asp:BoundField DataField="SPPrice" HeaderText="Special Price" />
                                <asp:BoundField DataField="DeliveryDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Delivery Date"
                                    HtmlEncode="False" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr runat="server" id="Tr38">
                    <td colspan="4" style="text-align: right">
                    </td>
                </tr>
                <tr runat="server" id="Tr23">
                    <td class="profilehead" colspan="4" id="Td76">
                        item details</td>
                </tr>
                <tr runat="server" id="Tr39">
                    <td style="text-align: right">
                    </td>
                    <td style="text-align: left">
                    </td>
                    <td style="text-align: right">
                    </td>
                    <td style="text-align: left">
                    </td>
                </tr>
                <tr runat="server" id="Tr24">
                    <td style="text-align: right" id="Td77">
                        <asp:Label ID="Label23" runat="server" Text="Brand :" Width="95px"></asp:Label></td>
                    <td style="text-align: left" id="Td78">
                        <asp:DropDownList ID="ddlBrandName" runat="server" Width="148px" OnSelectedIndexChanged="ddlBrandName_SelectedIndexChanged">
                        </asp:DropDownList><asp:Label ID="Label24" runat="server" EnableTheming="False" ForeColor="Red"
                            Text="*">
                            </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                            ControlToValidate="ddlBrandName" ErrorMessage="Please Select the Brand" InitialValue="0"
                            ValidationGroup="items">*</asp:RequiredFieldValidator></td>
                    <td style="text-align: right" id="Td79">
                        <asp:Label ID="Label25" runat="server" Text="Item Category :" Width="100px"></asp:Label></td>
                    <td style="text-align: left" id="Td80">
                        <asp:DropDownList ID="ddlItemCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged"
                            Width="151px">
                        </asp:DropDownList><asp:Label ID="Label26" runat="server" EnableTheming="False" ForeColor="Red"
                            Text="*">
                            </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                            ControlToValidate="ddlItemCategory" ErrorMessage="Please Select the Item Category"
                            InitialValue="0" ValidationGroup="items">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr runat="server" id="Tr40">
                    <td style="text-align: right">
                        <asp:Label ID="lblItemType" runat="server" Text="Item SubCategory :" Width="122px"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"
                            Width="151px">
                        </asp:DropDownList><asp:Label ID="Label29" runat="server" EnableTheming="False" Font-Bold="False"
                            Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlType" ErrorMessage="Please Select the Item SubCategory"
                                InitialValue="0" ValidationGroup="items">*</asp:RequiredFieldValidator></td>
                    <td style="text-align: right">
                        <asp:Label ID="Label3" runat="server" Text="Model No" Width="98px"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged">
                        </asp:DropDownList><asp:Label ID="Label54" runat="server" EnableTheming="False" ForeColor="Red"
                            Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlItemType"
                            ErrorMessage="Please Select the Item Type" InitialValue="0" ValidationGroup="items">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr runat="server" id="Tr25">
                    <td style="text-align: right" id="Td81">
                        <asp:Label ID="Label4" runat="server" Text="Model Name :" Width="100px"></asp:Label></td>
                    <td style="text-align: left" id="Td82">
                        <asp:TextBox ID="txtItemName" runat="server">
                            </asp:TextBox></td>
                    <td style="text-align: right" id="Td83">
                        <asp:Label ID="Label12" runat="server" Text="Color :"></asp:Label></td>
                    <td style="text-align: left" id="Td84">
                        <asp:TextBox ID="txtColor" runat="server" ReadOnly="True">
                            </asp:TextBox>
                        <asp:DropDownList ID="ddlcolor" runat="server">
                        </asp:DropDownList>
                        <asp:Label ID="lblDontDelete" runat="server" Text="Label" Visible="False"></asp:Label></td>
                </tr>
                <tr runat="server" id="Tr26">
                    <td style="text-align: right" id="Td85">
                        <asp:Label ID="Label2" runat="server" Text="Serial No." Width="63px"></asp:Label></td>
                    <td id="Td86">
                        <asp:TextBox ID="txtSerialNo" runat="server"></asp:TextBox></td>
                    <td style="text-align: right" id="Td87">
                        <asp:Label ID="Label10" runat="server" Text="Quantity" Width="63px"></asp:Label></td>
                    <td style="text-align: left" id="Td88">
                        <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox><asp:Label ID="Label11"
                            runat="server" EnableTheming="False" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Please Enter the Quantity"
                                ValidationGroup="items">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr runat="server" id="Tr29">
                    <td style="text-align: right;" id="Td94">
                        <asp:Label ID="Label43" runat="server" Text="Nature of Complaint"></asp:Label></td>
                    <td colspan="3" style="text-align: left;" id="Td95">
                        <asp:TextBox ID="txtNatureofComplaint" runat="server" CssClass="multilinetext" EnableTheming="False"
                            TextMode="MultiLine" Width="94%">-</asp:TextBox></td>
                </tr>
                <tr runat="server" id="Tr30">
                    <td style="text-align: right" id="Td96">
                        <asp:Label ID="Label49" runat="server" Text="Root Cause Noticed"></asp:Label></td>
                    <td colspan="3" style="text-align: left" id="Td97">
                        <asp:TextBox ID="txtRootCause" runat="server" CssClass="multilinetext" EnableTheming="False"
                            TextMode="MultiLine" Width="94%">-</asp:TextBox></td>
                </tr>
                <tr runat="server" id="Tr31">
                    <td style="text-align: right" id="Td98">
                        <asp:Label ID="Label6" runat="server" Text="Corrective Action Need to be Taken"></asp:Label></td>
                    <td colspan="3" style="text-align: left" id="Td99">
                        <asp:TextBox ID="txtCorrectiveAction" runat="server" CssClass="multilinetext" EnableTheming="False"
                            TextMode="MultiLine" Width="94%">-</asp:TextBox></td>
                </tr>
                <tr id="Tr42" runat="server">
                    <td colspan="4" style="text-align: center;">
                        &nbsp;<asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                            CssClass="loginbutton" EnableTheming="False" OnClick="btnAdd_Click" Text="Add"
                            ValidationGroup="items" /><asp:Button ID="btnItemRefresh" runat="server" BackColor="Transparent"
                                BorderStyle="None" CausesValidation="False" CssClass="loginbutton" EnableTheming="False"
                                OnClick="btnItemRefresh_Click" Text="Refresh" /></td>
                </tr>
                <tr runat="server" id="Tr32">
                    <td colspan="4" style="text-align: center">
                        <asp:GridView ID="gvQuotationItems" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvQuotationItems_RowDataBound"
                            OnRowDeleting="gvQuotationItems_RowDeleting" OnRowEditing="gvQuotationItems_RowEditing">
                            <Columns>
                                <asp:CommandField ShowEditButton="True" />
                                <asp:CommandField ShowDeleteButton="True" />
                                <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ItemType" HeaderText="Item Type">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ItemName" HeaderText="Item Name">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SerialNo" HeaderText="Serial No." NullDisplayText="-" />
                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                <asp:BoundField DataField="ItemTypeId" HeaderText="ItemTypeIdHidden" />
                                <asp:BoundField DataField="NatureofComplaint" HeaderText="NatureofComplaint" />
                                <asp:BoundField DataField="RootCausedNotice" HeaderText="RootCausedNotice" />
                                <asp:BoundField DataField="CorrectiveActionTaken" HeaderText="CorrectiveActionTaken" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr runat="server" id="Tr33">
                    <td class="profilehead" colspan="4" style="text-align: left" id="Td102">
                        Reference Details</td>
                </tr>
                <tr runat="server" id="Tr34">
                    <td style="text-align: right" id="Td103">
                    </td>
                    <td style="text-align: left" id="Td104">
                    </td>
                    <td style="text-align: right" id="Td105">
                    </td>
                    <td style="text-align: left" id="Td106">
                    </td>
                </tr>
                <tr runat="server" id="Tr35">
                    <td style="text-align: right" id="Td107">
                        <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                    <td style="text-align: left" id="Td108">
                        <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                        </asp:DropDownList></td>
                    <td style="text-align: right" id="Td109">
                    </td>
                    <td style="text-align: left" id="Td110">
                    </td>
                </tr>
                <tr runat="server" id="Tr36">
                    <td id="Td111">
                    </td>
                    <td id="Td112">
                    </td>
                    <td id="Td113">
                    </td>
                    <td id="Td114">
                    </td>
                </tr>
                <tr runat="server" id="Tr37">
                    <td colspan="4" align="center" id="Td115">
                        <table id="tblButtons">
                            <tr>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                <td>
                                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click"
                                        CausesValidation="False" /></td>
                                <td>
                                    <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server"></asp:ValidationSummary>
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="items" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                SelectCommand="SP_Customer_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtSearchModel" Name="SearchValue" PropertyName="Text"
                        Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
</table>
