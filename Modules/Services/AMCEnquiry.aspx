<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AMCEnquiry.aspx.cs" Inherits="Modules_Services_AMCEnquiry" Title="|| YANTRA : Services : AMC Enquiry ||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                AMC enquiry</td>
        </tr>
    </table>
    <br />
    <table border="0" cellpadding="0" cellspacing="0" style="width: 780px">
        <tr>
            <td colspan="4" class="searchhead" id="TD9">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">
                            Enquiries</td>
                        <td>
                        </td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Label id="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:DropDownList id="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="ENQ_NO">Enquiry No.</asp:ListItem>
                                            <asp:ListItem Value="ENQ_DATE">Enquiry Date</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME">Customer Name</asp:ListItem>
                                            <asp:ListItem Value="ENQ_ORIG_BY">Orginated By</asp:ListItem>
                                            <asp:ListItem Value="ENQ_DELIVERY_DATE">Delivery Date</asp:ListItem>
                                            <asp:ListItem Value="ENQ_STATUS">Status</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:DropDownList id="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
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
                                        <asp:Label id="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:TextBox id="txtSearchValueFromDate" runat="server" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox><asp:Image id="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender  Format="MM/dd/yyyy" ID="ceSearchFrom" runat="server" Enabled="False" PopupButtonID="imgFromDate"
                                            TargetControlID="txtSearchValueFromDate">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Label id="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:TextBox id="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><asp:Image id="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender  Format="MM/dd/yyyy" ID="ceSearchValueToDate" runat="server" Enabled="False" PopupButtonID="imgToDate"
                                            TargetControlID="txtSearchText">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Button id="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnSearchGo_Click" /></td>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                </tr>
                            </table>
                            <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" id="TD34" runat="server">
                <asp:GridView id="gvSalesEnquiry" runat="server" AutoGenerateColumns="False" DataKeyNames="ENQ_ID"
                    DataSourceID="sdsSalesEnquiry" OnRowDataBound="gvSalesEnquiry_RowDataBound" Width="100%" AllowPaging="True">
                    <columns>
<asp:BoundField DataField="ENQ_ID" SortExpression="ENQ_ID" HeaderText="EnqIdHidden"></asp:BoundField>
<asp:TemplateField SortExpression="ENQ_NO" HeaderText="Enq No"><EditItemTemplate>
    &nbsp;
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnEnqNo" runat="server" Text='<%# Eval("ENQ_NO") %>' OnClick="lbtnEnqNo_Click" CausesValidation="False"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:MM/dd/yyyy}" DataField="ENQ_DATE" SortExpression="ENQ_DATE" HeaderText="Enq Date">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CUST_NAME" SortExpression="CUST_NAME" HeaderText="Customer Name">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ENQ_ORIG_BY" SortExpression="ENQ_ORIG_BY" HeaderText="Orginated By">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:MM/dd/yyyy}" DataField="ENQ_DELIVERY_DATE" SortExpression="ENQ_DELIVERY_DATE" HeaderText="Delivery Date">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ENQ_STATUS" SortExpression="ENQ_STATUS" HeaderText="Status">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
</columns>
                    <emptydatatemplate>
No Data Exist
</emptydatatemplate>
                </asp:GridView>
                <asp:SqlDataSource id="sdsSalesEnquiry" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SM_SALESENQUIRY_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
</selectparameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td id="TD13" style="height: 19px; text-align: right">
            </td>
            <td style="height: 19px; text-align: left">
            </td>
            <td style="height: 19px; text-align: right">
            </td>
            <td style="height: 19px; text-align: left">
            </td>
        </tr>
        <tr>
            <td id="TD24" colspan="4" style="height: 19px; text-align: center">
                <table id="Table1">
                    <tr>
                        <td>
                            <asp:Button id="btnNew" runat="server" Text="New" CausesValidation="False" OnClick="btnNew_Click" /></td>
                        <td>
                            <asp:Button id="btnEdit" runat="server" Text="Edit" CausesValidation="False" OnClick="btnEdit_Click" /></td>
                        <td>
                            <asp:Button id="btnDelete" runat="server" Text="Delete" CausesValidation="False" OnClick="btnDelete_Click" /></td>
                        <td>
                            <asp:Button id="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Print" CausesValidation="False" /></td>
                        <td>
                            <asp:Button id="btnAssign" runat="server" OnClick="btnAssign_Click" Text="Assign" CausesValidation="False" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style=" text-align: center">
                &nbsp;<table id="tblAssignTasks" runat="server" border="0" cellpadding="0" cellspacing="0"
                    visible="false" width="100%">
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: left">
                        </td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label19" runat="server" CssClass="label" Font-Bold="False" Text="Assign Task No"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtAssignTaskNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label12" runat="server" CssClass="label" Font-Bold="False" Text="Enquiry No"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtEnquiryNoForAssign" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">
                            &nbsp;<asp:Label ID="Label13" runat="server" CssClass="label" Font-Bold="False" Text="Enquiry Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtEnquiryDateForAssign" runat="server" ReadOnly="True"></asp:TextBox>
                            <cc1:MaskedEditExtender ID="meeEnquiryDateForAssing" runat="server" DisplayMoney="Left"
                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtEnquiryDateForAssign" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label15" runat="server" CssClass="label" Font-Bold="False" Text="Customer Name"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCustomerNameForAssingn" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label16" runat="server" CssClass="label" Font-Bold="False" Text="Contact E-Mail"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCustomerEmailForAssingn" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label17" runat="server" CssClass="label" Font-Bold="False" Text="Employee Name"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlEmpNameForAssign" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpNameForAssign_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label id="Label20" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                ControlToValidate="ddlEmpNameForAssign" ErrorMessage="Please Select the Employee Name"
                                InitialValue="0" ValidationGroup="assign">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label18" runat="server" CssClass="label" Font-Bold="False" Text="Employee EMail"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtEmpEmailId" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 52px;">
                            <asp:Label ID="Label28" runat="server" CssClass="label" Font-Bold="False" Text="Remarks"></asp:Label></td>
                        <td colspan="3" style="text-align: left; height: 52px;">
                            <asp:TextBox ID="txtRemarksForAssingn" runat="server" CssClass="multilinetext" EnableTheming="False" TextMode="MultiLine" Width="83%"></asp:TextBox><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtRemarksForAssingn"
                                ErrorMessage="Please Enter Remarks" ValidationGroup="assign">*</asp:RequiredFieldValidator></td>
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
                        <td style="text-align: right">
                            <asp:Label ID="Label27" runat="server" CssClass="label" Font-Bold="False" Text="Assign Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtAssignDate" runat="server" CssClass="datetext" EnableTheming="False"
                                Width="130px"></asp:TextBox><asp:Image ID="imgAssignDate" runat="server" ImageUrl="~/Images/Calendar.png" />
                            <asp:Label id="Label21" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtAssignDate"
                                    ErrorMessage="Please Select Assign Date" ValidationGroup="assign">*</asp:RequiredFieldValidator><cc1:CalendarExtender
                        ID="ceAssignDate" runat="server" PopupButtonID="imgAssignDate" TargetControlID="txtAssignDate">
                                    </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeAssingDate" runat="server" DisplayMoney="Left"
                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtAssignDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label26" runat="server" CssClass="label" Font-Bold="False" Text="Due Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDueDate" runat="server" CssClass="datetext" EnableTheming="False"
                                Width="130px"></asp:TextBox><asp:Image ID="imgDueDate" runat="server" ImageUrl="~/Images/Calendar.png" />
                            <asp:Label id="Label22" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDueDate" ErrorMessage="Please Select Due Date">*</asp:RequiredFieldValidator><asp:CompareValidator
                                        ID="CompareValidator1" runat="server" ControlToCompare="txtAssignDate" ControlToValidate="txtDueDate"
                                        ErrorMessage="Due Date should not be less than Assign Date" Operator="GreaterThanEqual"
                                        SetFocusOnError="True" Type="Date" ValidationGroup="assign">*</asp:CompareValidator><cc1:CalendarExtender
                        ID="ceDueDate" runat="server" PopupButtonID="imgDueDate" TargetControlID="txtDueDate">
                                        </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeDueDate" runat="server" DisplayMoney="Left"
                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtDueDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
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
                        <td colspan="4" style="text-align: center; height: 49px;">
                            <table id="Table2">
                                <tr>
                                    <td>
                                        <asp:Button id="btnAssignTask" runat="server" OnClick="btnAssignTask_Click" Text="Assign" ValidationGroup="assign" /></td>
                                    <td>
                                        <asp:Button id="btnCloseAssignTask" runat="server" OnClick="btnCancelTask_Click" Text="Close" CausesValidation="False" /></td>
                                </tr>
                            </table>
                        </td>
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
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 19px; text-align: center">
                <table border="0" cellpadding="0" cellspacing="0" id="tblSalesEnquiry" runat="server" visible="false" width="100%">
                    <tr>
            <td id="TD10" class="profilehead" colspan="4" style="text-align: left">
                Enquiry Details</td>
                    </tr>
                    <tr>
            <td id="TD27" style="height: 19px; text-align: right">
            </td>
            <td style="height: 19px; text-align: left">
            </td>
            <td style="height: 19px; text-align: right">
            </td>
            <td style="height: 19px; text-align: left">
            </td>
                    </tr>
                    <tr>
            <td style="text-align: right" id="TD14">
                <asp:Label id="lblEnquiryNo" runat="server" Text="Enquiry No" Width="76px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtEnquiryNo" runat="server" ReadOnly="True"></asp:TextBox>
                <asp:Label id="Label23" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                <asp:RequiredFieldValidator
                    ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEnquiryNo"
                    ErrorMessage="Please Enter Enquiry No." SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right;">
                <asp:Label id="lblEnquiryDate" runat="server" Text="Enquiry Date"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtEnquiryDate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox><asp:Image id="imgEnquiryDate" runat="server" ImageUrl="~/Images/Calendar.png"
                    ></asp:Image><cc1:CalendarExtender  Format="MM/dd/yyyy" ID="ceEnquiryDate" runat="server" PopupButtonID="imgEnquiryDate"
                        TargetControlID="txtEnquiryDate"> </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="meeEnquiryDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                    MaskType="Date" TargetControlID="txtEnquiryDate" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
            </td>
                    </tr>
                    <tr>
            <td style="text-align: right;" id="TD23">
                <asp:Label id="lblOriginatedBy" runat="server" Text="Enquiry Originated By" Width="136px"></asp:Label></td>
            <td style="text-align: left;">
                <asp:RadioButton id="rbEmployee" runat="server" GroupName="rbtOriginatedBy" Text="Employee" AutoPostBack="True" Checked="True" OnCheckedChanged="rbEmployeeAgent_CheckedChanged">
                </asp:RadioButton><asp:RadioButton id="rbAgent" runat="server" GroupName="rbtOriginatedBy" Text="Agent" AutoPostBack="True" OnCheckedChanged="rbEmployeeAgent_CheckedChanged">
                </asp:RadioButton></td>
            <td style="text-align: right;">
                <asp:Label id="lblEnquirySource" runat="server" Text="Enquiry Source"></asp:Label></td>
            <td style="text-align: left;">
                <asp:DropDownList id="ddlEnquirySource" runat="server">
            </asp:DropDownList>
                <asp:Label id="Label29" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                <asp:RequiredFieldValidator id="rfvEnqSource" runat="server" ControlToValidate="ddlEnquirySource"
                    ErrorMessage="Please Select the Enquiry Source">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label ID="lblOrginatedList" runat="server" Text="Employee Name"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                <asp:DropDownList id="ddlOriginatedBy" runat="server">
                </asp:DropDownList>
                            <asp:Label id="Label24" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator id="rfvOriginatedBy" runat="server" ControlToValidate="ddlOriginatedBy"
                    ErrorMessage="Please Select the Enquiry Originated By" InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="height: 24px; text-align: right">
                        </td>
                        <td style="height: 24px; text-align: left">
                        </td>
                    </tr>
                    <tr>
            <td style="text-align: right; height: 26px;" id="TD32">
                <asp:Label id="lblReferenceCode" runat="server" Text="Reference Code" Width="108px"></asp:Label></td>
            <td style="height: 26px; text-align: left"><asp:TextBox id="txtReferenceCode" runat="server">
            </asp:TextBox></td>
            <td style="height: 26px; text-align: right;">
                <asp:Label id="lblEnquiryDueDate" runat="server" Text="Enquiry Due Date"></asp:Label></td>
            <td style="height: 26px; text-align: left">
                <asp:TextBox id="txtEnquiryDueDate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox><asp:Image id="imgEnquiryDueDate" runat="server" ImageUrl="~/Images/Calendar.png"
                    ></asp:Image>
                <asp:Label id="Label30" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                <asp:RequiredFieldValidator id="rfvEnquiryDueDate" runat="server" ControlToValidate="txtEnquiryDueDate"
                    ErrorMessage="Please Select the Enquiry Due Date">*</asp:RequiredFieldValidator><cc1:CalendarExtender
                        ID="ceEnquiryDueDate" runat="server" PopupButtonID="imgEnquiryDueDate" TargetControlID="txtEnquiryDueDate">
                    </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="meeEnquiryDueDate" runat="server" DisplayMoney="Left"
                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtEnquiryDueDate" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                <asp:Label id="lblPromotionType" runat="server" Text="Promotion Type"></asp:Label></td>
                        <td style="text-align: left">
                <asp:TextBox id="txtPromotionType" runat="server"></asp:TextBox>
                            <asp:Label id="Label25" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator id="rfvPromotionType" runat="server" ControlToValidate="txtPromotionType"
                    ErrorMessage="Please Enter the Promotion Type">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                <asp:Label id="lblPromotionActivity" runat="server" Text="Promotion Activity"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtPromotionActivity" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                <asp:Label id="lblCriteria" runat="server" Text="Follow Up Criteria"></asp:Label></td>
                        <td style="text-align: left" colspan="3">
                <asp:TextBox id="txtFollowUpCriteria" runat="server" CssClass="multilinetext" EnableTheming="False" TextMode="MultiLine" Width="89%"></asp:TextBox></td>
                    </tr>
                    <tr>
            <td style="text-align: right" id="TD11">
                <asp:Label id="lblDescription" runat="server" Text="Description" Width="73px"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                <asp:TextBox id="txtDescription" runat="server" TextMode="MultiLine" CssClass="multilinetext" EnableTheming="False" Width="89%"></asp:TextBox></td>
                    </tr>
                    <tr>
            <td style="text-align: right" id="TD15">
                </td>
            <td style="text-align: left"></td>
            <td style="text-align: right">
                </td>
            <td style="text-align: left">
            </td>
                    </tr>
                    <tr>
            <td colspan="4" style="text-align: left" class="profilehead" id="TD16">
                Customer Details</td>
                    </tr>
                    <tr>
            <td style="height: 19px; text-align: right" id="TD5">
            </td>
            <td style="height: 19px; text-align: left">
            </td>
            <td style="height: 19px; text-align: right">
            </td>
            <td style="height: 19px; text-align: left">
            </td>
                    </tr>
                    <tr>
            <td style="text-align: right" id="TD18">
                <asp:Label id="lblCustomer" runat="server" Text="Customer"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList id="ddlCustomer" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:Label id="Label31" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                <asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCustomer"
                    ErrorMessage="Please Select the Customer" InitialValue="0">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right">
                <asp:Label id="lblContactPerson" runat="server" Text="Contact Person"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtContactPerson" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
            <td style="text-align: right" id="TD21">
                <asp:Label id="lblRegion" runat="server" Text="Region"></asp:Label></td>
            <td style="text-align: left"><asp:TextBox id="txtRegion" runat="server" ReadOnly="True"></asp:TextBox></td>
            <td style="text-align: right"><asp:Label id="Label9" runat="server" Text="Industry Type"></asp:Label></td>
            <td style="text-align: left"><asp:TextBox id="txtIndustryType" runat="server" ReadOnly="True">
            </asp:TextBox></td>
                    </tr>
                    <tr>
            <td style="height: 21px; text-align: right" id="TD31">
                <asp:Label id="lblAddress" runat="server" Text="Address"></asp:Label></td>
            <td style="height: 21px; text-align: left">
                <asp:TextBox id="txtAddress" runat="server" TextMode="MultiLine" ReadOnly="True"></asp:TextBox></td>
            <td style="height: 21px; text-align: right">
                <asp:Label id="lblEmail" runat="server" Text="Email"></asp:Label></td>
            <td style="height: 21px; text-align: left">
                <asp:TextBox id="txtEmail" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
            <td style="text-align: right" id="TD28"><asp:Label id="lblPhoneNo" runat="server" Text="Phone No" Width="74px"></asp:Label></td>
            <td style="text-align: left"><asp:TextBox id="txtPhoneNo" runat="server" ReadOnly="True">
            </asp:TextBox></td>
            <td style="text-align: right"><asp:Label id="Label8" runat="server" Text="Mobile" Width="74px"></asp:Label></td>
            <td style="text-align: left"><asp:TextBox id="txtMobile" runat="server" ReadOnly="True">
            </asp:TextBox></td>
                    </tr>
                    <tr>
            <td colspan="4" style="text-align: left; height: 20px;" class="profilehead" id="TD19">
                Interested Product</td>
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
                <asp:Label id="Label11" runat="server" Text="Item Type"></asp:Label></td>
                                            <td style="text-align: left">
                                                <asp:DropDownList id="ddlItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:Label id="Label32" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                    ControlToValidate="ddlItemType" ErrorMessage="Please Select the Item Type" InitialValue="0"
                                                    ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
                                            <td style="text-align: right">
                <asp:Label id="Label4" runat="server" Text="Item Name" Width="76px"></asp:Label></td>
                                            <td style="text-align: left">
                                                <asp:DropDownList id="ddlItemName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">--</asp:ListItem>
                                                    <asp:ListItem Value="0">-- Select Item Type --</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:Label id="Label33" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                    ControlToValidate="ddlItemName" ErrorMessage="Please Select the Item Name" InitialValue="0"
                                                    ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                <asp:Label id="Label5" runat="server" Text="UOM"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtItemUOM" runat="server" ReadOnly="True"></asp:TextBox></td>
            <td style="text-align: right">
                <asp:Label id="Label6" runat="server" Text="Quantity" Width="57px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtItemQuantity" runat="server">
                </asp:TextBox>
                <asp:Label id="Label34" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtItemQuantity"
                    ErrorMessage="Please Enter the Quantity" ValidationGroup="ip">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender ID="ftxtQuantity" runat="server" FilterType="Numbers"
                    TargetControlID="txtItemQuantity">
                </cc1:FilteredTextBoxExtender>
            </td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="Label3" runat="server" Text="Item Specification"></asp:Label></td>
                        <td colspan="3" style="height: 21px; text-align: left">
                            <asp:TextBox ID="txtItemSpec" runat="server" CssClass="multilinetext" EnableTheming="False"
                                ReadOnly="True" TextMode="MultiLine" Width="89%"></asp:TextBox></td>
                    </tr>
                    <tr>
            <td style="height: 21px; text-align: right">
                <asp:Label id="Label7" runat="server" Text="Specifications"></asp:Label></td>
            <td style="height: 21px; text-align: left" colspan="3">
                <asp:TextBox id="txtItemSpecifications" runat="server" TextMode="MultiLine" CssClass="multilinetext" EnableTheming="False" Width="89%"></asp:TextBox></td>
                    </tr>
                    <tr>
                                            <td style="height: 21px; text-align: right">
                                                <asp:Label id="Label10" runat="server" Text="Remarks"></asp:Label></td>
                                            <td colspan="3" style="height: 21px; text-align: left">
                                                <asp:TextBox id="txtRemarks" runat="server" TextMode="MultiLine" CssClass="multilinetext" EnableTheming="False" Width="89%"></asp:TextBox></td>
                    </tr>
                    <tr>
            <td style="height: 19px; text-align: right">
                <asp:Label id="lblPriority" runat="server" Text="Priority"></asp:Label>
            </td>
            <td style="height: 19px; text-align: left">
                <asp:DropDownList id="ddlPriority" runat="server">
                    <asp:ListItem Value="0">--</asp:ListItem>
                    <asp:ListItem>Low</asp:ListItem>
                    <asp:ListItem>Medium</asp:ListItem>
                    <asp:ListItem>High</asp:ListItem>
                </asp:DropDownList><asp:RequiredFieldValidator id="rfvPriority" runat="server" ControlToValidate="ddlPriority"
                    ErrorMessage="Please Select the Priority" InitialValue="0" ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
            <td style="height: 19px; text-align: right">
            </td>
            <td style="height: 19px; text-align: left">
            </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
            <td style="text-align: right; height: 19px;">
            </td>
            <td style="text-align: right; height: 19px;">
                <asp:Button id="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                    CssClass="loginbutton" EnableTheming="False" onclick="btnAdd_Click" Text="Add" ValidationGroup="ip" /></td>
            <td style="text-align: left; height: 19px;">
                <asp:Button id="btnRefreshItems" runat="server" BackColor="Transparent" BorderStyle="None"
                    CssClass="loginbutton" EnableTheming="False" Text="Refresh" CausesValidation="False" OnClick="btnRefreshItems_Click" /></td>
            <td style="text-align: left; height: 19px;">
            </td>
                    </tr>
                    <tr>
            <td style="height: 19px; text-align: right">
            </td>
            <td style="height: 19px; text-align: left">
            </td>
            <td style="height: 19px; text-align: right">
            </td>
            <td style="height: 19px; text-align: left">
            </td>
                    </tr>
                    <tr>
            <td style="text-align: center" colspan="4" id="TD12" runat="server">
                <asp:GridView id="gvInterestedProducts" runat="server" AutoGenerateColumns="False"
                    OnRowDataBound="gvInterestedProducts_RowDataBound" OnRowDeleting="gvInterestedProducts_RowDeleting">
                    <columns>
<asp:CommandField ShowDeleteButton="True"></asp:CommandField>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity">
<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Specifications" HeaderText="Specifications">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
<asp:BoundField DataField="Priority" HeaderText="Priority"></asp:BoundField>
</columns>
                </asp:GridView>
            </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left; height: 20px;" class="profilehead" id="Td2">
                            Delivery Details</td>
                    </tr>
                    <tr>
            <td runat="server" style="height: 19px; text-align: right">
            </td>
            <td style="height: 19px; text-align: left">
            </td>
            <td style="height: 19px; text-align: right">
            </td>
            <td style="height: 19px; text-align: left">
            </td>
                    </tr>
                    <tr>
            <td style="text-align: right" id="TD29">
                <asp:Label ID="Label1" runat="server" Text="Delivery Date"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtDeliveryDate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox><asp:Image ID="imgDeliveryDate" runat="server" ImageUrl="~/Images/Calendar.png" />
                <asp:Label id="Label35" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                <asp:RequiredFieldValidator id="rfvDeliveryDate" runat="server" ControlToValidate="txtDeliveryDate"
                    ErrorMessage="Please Select the Delivery Date">*</asp:RequiredFieldValidator><cc1:CalendarExtender
                        ID="ceDeliveryDate" runat="server" PopupButtonID="imgDeliveryDate" TargetControlID="txtDeliveryDate">
                    </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="meeDEliveryDate" runat="server" DisplayMoney="Left"
                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtDeliveryDate" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
            </td>
            <td style="text-align: right">
                <asp:Label ID="Label2" runat="server" Text="Delivery Type"></asp:Label></td>
            <td style="text-align: left"><asp:DropDownList id="ddlDeliveryType" runat="server">
            </asp:DropDownList>
                <asp:Label id="Label36" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                <asp:RequiredFieldValidator id="rfvDeliveryType" runat="server" ControlToValidate="ddlDeliveryType"
                    ErrorMessage="Please Select the Delivery Type">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
            <td style="height: 19px" id="TD26">
            </td>
            <td style="height: 19px">
            </td>
            <td style="height: 19px">
            </td>
            <td style="height: 19px">
            </td>
                    </tr>
                    <tr>
            <td colspan="4" style="height: 49px" id="TD37">
                <table id="tblButtons">
                    <tr>
                        <td>
                            <asp:Button id="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                        <td>
                            <asp:Button id="btnRefresh" runat="server" Text="Refresh" CausesValidation="False" OnClick="btnRefresh_Click" /></td>
                        <td>
                            <asp:Button id="btnClose" runat="server" Text="Close" CausesValidation="False" OnClick="btnClose_Click" /></td>
                    </tr>
                </table>
            </td>
                    </tr>
                    <tr>
            <td style="height: 21px" id="TD8">
            </td>
            <td style="height: 21px">
            </td>
            <td style="height: 21px;">
            </td>
            <td style="height: 21px">
            </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:ValidationSummary id="ValidationSummary1" runat="server">
    </asp:ValidationSummary>
    <asp:ValidationSummary id="ValidationSummary2" runat="server" ValidationGroup="ip">
    </asp:ValidationSummary>
    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="assign" />
</asp:Content>


 
