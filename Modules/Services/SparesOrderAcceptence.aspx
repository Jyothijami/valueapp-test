<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SparesOrderAcceptence.aspx.cs" Inherits="Modules_Services_SparesOrderAcceptence" Title="||ERP:Services:SparesOrderAcceptence||" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                order confirmation</td>
        </tr>
    </table>
    <br />
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">
                            confirmed Orders</td>
                        <td>
                        </td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Label id="Label3" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:DropDownList id="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="SPOP_NO">Work OrderNo</asp:ListItem>
                                            <asp:ListItem Value="SPOA_NO"> Order Acceptance No</asp:ListItem>
                                            <asp:ListItem Value="SPOA_DATE">Order Acceptance Date</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME">Customer</asp:ListItem>
                                            <asp:ListItem Value="CUST_CONTACT_PERSON">Contact Person</asp:ListItem>
                                            <asp:ListItem Value="CUST_EMAIL">EMail</asp:ListItem>
                                            <asp:ListItem Value="SPOA_STATUS">Status</asp:ListItem>
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
                                        <cc1:CalendarExtender  Format="dd/MM/yyyy" ID="ceSearchFrom" runat="server" enabled="False" popupbuttonid="imgFromDate"
                                            targetcontrolid="txtSearchValueFromDate"></cc1:calendarextender>
                                        <cc1:maskededitextender id="MaskedEditSearchFromDate" runat="server" displaymoney="Left"
                                            enabled="False" mask="99/99/9999" masktype="Date" targetcontrolid="txtSearchValueFromDate"
                                            userdateformat="MonthDayYear"></cc1:maskededitextender>
                                    </td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Label id="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:TextBox id="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><asp:Image id="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender  Format="dd/MM/yyyy" ID="ceSearchValueToDate" runat="server" enabled="False" popupbuttonid="imgToDate"
                                            targetcontrolid="txtSearchText"></cc1:calendarextender>
                                        <cc1:maskededitextender id="MaskedEditSearchToDate" runat="server" displaymoney="Left"
                                            enabled="False" mask="99/99/9999" masktype="Date" targetcontrolid="txtSearchText"
                                            userdateformat="MonthDayYear"></cc1:maskededitextender>
                                    </td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Button id="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" onclick="btnSearchGo_Click" Text="Go" /></td>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                </tr>
                            </table>
                            <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView id="gvOrderAcceptanceDetails" runat="server" AllowPaging="True"
                    AutoGenerateColumns="False" DataSourceID="sdsOrderAcc" OnRowDataBound="gvOrderAcceptanceDetails_RowDataBound">
                    <columns>
<asp:BoundField DataField="SPOA_ID" HeaderText="OrderAcceptanceIdHidden"></asp:BoundField>
<asp:BoundField DataField="SPOP_ID" SortExpression="SPOP_ID" HeaderText="WOrkIdHidden"></asp:BoundField>
<asp:TemplateField SortExpression="SPOP_NO" HeaderText="Work Order No"><EditItemTemplate>
<asp:TextBox id="TextBox2" runat="server" Text='<%# Bind("SPOP_NO") %>' __designer:wfdid="w12"></asp:TextBox> 
</EditItemTemplate>
<ItemTemplate>
<asp:LinkButton id="lbtnWorkOrderNo" onclick="lbtnWorkOrderNo_Click" runat="server" Text='<%# Bind("SPOP_NO") %>' CausesValidation="False" __designer:wfdid="w1"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText=" Order Acceptance No"><EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("SPOA_NO") %>' ID="TextBox1"></asp:TextBox>
                        
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnOrderAcceptanceNo" onclick="lbtnOrderAcceptanceNo_Click" runat="server" Text='<%# Bind("SPOA_NO") %>' CausesValidation="False"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True" DataField="SPOA_DATE" HeaderText=" Order Acceptance Date"></asp:BoundField>
<asp:BoundField DataField="CUST_NAME" HeaderText="Customer">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CUST_CONTACT_PERSON" HeaderText="Contact Person">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CUST_EMAIL" HeaderText="Email">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="PREPAREDBY" SortExpression="PREPAREDBY" HeaderText="Prepared By">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="APPROVEDBY" SortExpression="APPROVEDBY" HeaderText="Approved By">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="SPOA_STATUS" HeaderText="Status">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
</columns>
                    <emptydatatemplate>
No Data Exist!
</emptydatatemplate>
                    <selectedrowstyle backcolor="LightSteelBlue" />
                </asp:GridView><asp:SqlDataSource id="sdsOrderAcc" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SERVICES_SPARESORDERACCEPTANCE_SEARCH_SELECT" SelectCommandType="StoredProcedure"><selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="EMPID" ControlID="lblEmpIdHidden"></asp:ControlParameter>
</selectparameters>
                </asp:SqlDataSource>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 19px; text-align: left">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 49px; text-align: center">
                <table id="Table1">
                    <tr>
                        <td>
                            <asp:Button id="btnNew" runat="server" CausesValidation="False" onclick="btnNew_Click"
                                Text="New" Visible="False" /></td>
                        <td>
                            <asp:Button id="btnEdit" runat="server" CausesValidation="False" onclick="btnEdit_Click"
                                Text="Edit" /></td>
                        <td>
                            <asp:Button id="btnDelete" runat="server" CausesValidation="False" onclick="btnDelete_Click"
                                Text="Delete" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table border="0" cellpadding="0" cellspacing="0" id="tblOrderAcceptanceDetails" runat="server" visible="false" width="100%">
                    <tr>
            <td colspan="4" style="text-align: left" class="profilehead">
                General Details</td>
                    </tr>
                    <tr>
            <td style="height: 19px; text-align: right">
            </td>
            <td style="height: 19px; text-align: left">
            </td>
            <td style="height: 19px; text-align: right">
            </td>
            <td style="height: 19px; text-align: left;">
            </td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                <asp:Label id="lblSalesOrderNo" runat="server" Text="Order Profile No"></asp:Label></td>
            <td style="text-align: left"><asp:DropDownList id="ddlSONo" runat="server" OnSelectedIndexChanged="ddlSONo_SelectedIndexChanged" AutoPostBack="True">
            </asp:DropDownList><asp:Label id="Label35" runat="server" EnableTheming="False" ForeColor="Red" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSONo"
                    ErrorMessage="Please Select the Sales Order No" InitialValue="0">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right;">
                <asp:Label id="lblSalesOrderDate" runat="server" Text="Order Profile  Date" Width="114px"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox id="txtSODate" runat="server" CssClass="datetext" EnableTheming="False" ReadOnly="True"></asp:TextBox>&nbsp;<asp:Image id="imgSODate" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False"
                    ></asp:Image><cc1:CalendarExtender  Format="dd/MM/yyyy" ID="ceSODate" runat="server" PopupButtonID="imgSODate"
                        TargetControlID="txtSODate" Enabled="False">
                    </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="meeSoDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                    MaskType="Date" TargetControlID="txtSODate" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
            </td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                <asp:Label id="lblCustomer" runat="server" Text="Customer Name"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtCustName" runat="server" ReadOnly="True"></asp:TextBox></td>
            <td style="text-align: right">
                <asp:Label ID="Label45" runat="server" Text="Unit Name"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtUnitName" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
            <td style="text-align: right" rowspan="2">
                <asp:Label id="lblAddress" runat="server" Text="Address"></asp:Label></td>
            <td style="text-align: left" rowspan="2">
                <asp:TextBox id="txtAddress" runat="server" TextMode="MultiLine" ReadOnly="True"></asp:TextBox></td>
            <td style="text-align: right">
                <asp:Label id="lblCity" runat="server" Text="Region"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox id="txtRegion" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                <asp:Label id="Label25" runat="server" Text="Phone"></asp:Label></td>
                        <td style="text-align: left">
                <asp:TextBox id="txtPhone" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                <asp:Label id="lblEmail" runat="server" Text="Email"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtEmail" runat="server" ReadOnly="True"></asp:TextBox></td>
            <td style="text-align: right">
                <asp:Label id="Label26" runat="server" Text="Mobile"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox id="txtMobile" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
            <td colspan="4" style="text-align: left">
                </td>
                    </tr>
                    <tr>
            <td colspan="4" style="text-align: left" class="profilehead">
                Ordered Items</td>
                    </tr>
                    <tr>
            <td colspan="4">
                <asp:GridView id="gvSalesOrderItems" runat="server" AutoGenerateColumns="False"
                    Width="100%" OnRowDataBound="gvSalesOrderItems_RowDataBound"><columns>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
<asp:BoundField HeaderText="Amount"></asp:BoundField>
<asp:BoundField DataField="Specifications" HeaderText="Specifications"></asp:BoundField>
<asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
<asp:BoundField DataField="Priority" HeaderText="Priority"></asp:BoundField>
</columns>
                </asp:GridView>
            </td>
                    </tr>
                    <tr>
            <td class="profilehead" colspan="4">
                Ordered Details</td>
                    </tr>
                    <tr>
            <td style="height: 19px; text-align: right">
            </td>
            <td style="height: 19px; text-align: left">
            </td>
            <td style="height: 19px; text-align: right">
            </td>
            <td style="height: 19px; text-align: left;">
            </td>
                    </tr>
                    <tr>
            <td style="height: 19px; text-align: right">
                <asp:Label ID="Label1" runat="server" Text="Order Acceptance No" Width="141px"></asp:Label></td>
            <td style="height: 19px; text-align: left">
                <asp:TextBox ID="txtOANo" runat="server" ReadOnly="True"></asp:TextBox></td>
            <td style="height: 19px; text-align: right">
                <asp:Label ID="Label2" runat="server" Text=" Order Acceptance  Date" Width="150px"></asp:Label></td>
            <td style="height: 19px; text-align: left;">
                <asp:TextBox ID="txtOADate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox><asp:Image ID="imgOADate" runat="server"
                    ImageUrl="~/Images/Calendar.png" />
                <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="DateCustomValidate"
                    ControlToValidate="txtOADate" ErrorMessage="Please Enter the Order Acceptance Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                    SetFocusOnError="True">*</asp:CustomValidator>
                <cc1:CalendarExtender  Format="dd/MM/yyyy" ID="ceOADate" runat="server"
                        PopupButtonID="imgOADate" TargetControlID="txtOADate">
                    </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="meeOADate" runat="server" DisplayMoney="Left"
                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtOADate" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
            </td>
                    </tr>
                    <tr>
            <td class="profilehead" colspan="4">
                terms &amp; conditions</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left;">
                        </td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                <asp:Label id="Label10" runat="server" Text="Delivery"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtDelivery" runat="server">
                </asp:TextBox><asp:Label id="Label9" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                </asp:Label>
                <asp:RequiredFieldValidator id="rfvDelivery" runat="server" ControlToValidate="txtDelivery"
                    ErrorMessage="Please Enter the Delivery">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right">
                <asp:Label id="Label5" runat="server" Text="currency type"></asp:Label></td>
            <td style="text-align: left;"><asp:DropDownList ID="ddlCurrencyType" runat="server">
            </asp:DropDownList><asp:Label id="Label17" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
            </asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCurrencyType"
                    ErrorMessage="Please Select the Currency Type" InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                <asp:Label id="Label12" runat="server" Text="packing charges"></asp:Label>
            </td>
            <td style="text-align: left">
                <asp:TextBox id="txtPackingCharges" runat="server">
                </asp:TextBox><asp:Label id="Label14" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                </asp:Label>
                <asp:RequiredFieldValidator id="rfvPackingChrgs" runat="server" ControlToValidate="txtPackingCharges"
                    ErrorMessage="Please Enter the Packing Charges">*</asp:RequiredFieldValidator>
                <cc1:FilteredTextBoxExtender ID="ftxtePackingCharges" runat="server" FilterType="Numbers"
                    TargetControlID="txtPackingCharges">
                </cc1:FilteredTextBoxExtender>
            </td>
            <td style="text-align: right">
                <asp:Label ID="Label11" runat="server" Text="payment terms"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox id="txtPaymentTerms" runat="server">
                </asp:TextBox><asp:Label id="Label18" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                </asp:Label>
                <asp:RequiredFieldValidator id="rfvPayTerms" runat="server" ControlToValidate="txtPaymentTerms"
                    ErrorMessage="Please Enter the Payment Terms">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                <asp:Label id="Label15" runat="server" Text="C.S. Tax"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtCST" runat="server">
                </asp:TextBox><asp:Label ID="Label32" runat="server" Text="%"></asp:Label><asp:Label id="Label19" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                </asp:Label><asp:RequiredFieldValidator id="rfvCST" runat="server" ControlToValidate="txtCST"
                    ErrorMessage="Please Enter the CS Tax">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                    TargetControlID="txtCST">
                    </cc1:FilteredTextBoxExtender>
            </td>
            <td style="text-align: right">
                <asp:Label id="Label13" runat="server" Text="Excise Duty"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox id="txtExciseDuty" runat="server">
                </asp:TextBox><asp:Label ID="Label38" runat="server" Text="%"></asp:Label><asp:Label id="Label21" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                </asp:Label><asp:RequiredFieldValidator id="rfvExciseDuty" runat="server" ControlToValidate="txtExciseDuty"
                    ErrorMessage="Please Enter the Excise Duty">*</asp:RequiredFieldValidator>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                    TargetControlID="txtExciseDuty">
                </cc1:FilteredTextBoxExtender>
            </td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                <asp:Label id="Label20" runat="server" Text="Guarantee"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtGuarantee" runat="server">
                </asp:TextBox><asp:RequiredFieldValidator id="rfvGuarantee" runat="server" ControlToValidate="txtGuarantee"
                    ErrorMessage="Please Enter the Guarantee">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right">
                <asp:Label id="Label16" runat="server" Text="Despatch Mode"></asp:Label></td>
            <td style="text-align: left;"><asp:DropDownList ID="ddlDespatchMode" runat="server">
            </asp:DropDownList><asp:Label id="Label23" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
            </asp:Label>
                <asp:RequiredFieldValidator ID="rfvDespatchMode" runat="server"
                ControlToValidate="ddlDespatchMode" ErrorMessage="Please Select the Despatch Mode"
                InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                <asp:Label ID="Label6" runat="server" Text="Transporter"></asp:Label></td>
            <td style="text-align: left"><asp:DropDownList ID="ddlTransporter" runat="server">
            </asp:DropDownList><asp:Label id="Label22" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
            </asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                ControlToValidate="ddlTransporter" ErrorMessage="Please Select the Transporter"
                InitialValue="0">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right">
                <asp:Label id="Label27" runat="server" Text="Inspection"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox id="txtInspection" runat="server">
                </asp:TextBox><asp:Label id="Label29" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                </asp:Label>
                <asp:RequiredFieldValidator id="rfvInspection" runat="server" ControlToValidate="txtInspection"
                    ErrorMessage="Please Enter the Inspection">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label8" runat="server" Text="Delivery Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDeliveryDate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox><asp:Image
                                ID="imgDeliveryDate" runat="server" ImageUrl="~/Images/Calendar.png" /><asp:Label id="Label24" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                                </asp:Label><asp:RequiredFieldValidator
                                    ID="rfvDeliveryDate" runat="server" ControlToValidate="txtDeliveryDate" ErrorMessage="Please Select the Delivery Date">*</asp:RequiredFieldValidator><asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="DateCustomValidate"
                                ControlToValidate="txtDeliveryDate" ErrorMessage="Please Enter the Delivery Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                SetFocusOnError="True">*</asp:CustomValidator><cc1:CalendarExtender
                                      Format="dd/MM/yyyy"  ID="ceDeliveryDate" runat="server" PopupButtonID="imgDeliveryDate" TargetControlID="txtDeliveryDate">
                                    </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeDEliveryDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtDeliveryDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                <asp:Label id="Label28" runat="server" Text="Other specifications"></asp:Label></td>
                        <td colspan="3" style="text-align: left" valign="middle">
                            <asp:TextBox ID="txtOtherSpecs" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="94%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Consignee Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left;">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 22px; text-align: right">
                            <asp:Label ID="Label7" runat="server" Text="Consignment To"></asp:Label></td>
                        <td colspan="3" style="height: 22px; text-align: left">
                            <asp:TextBox ID="txtConsignee" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="91%" ReadOnly="True"></asp:TextBox><asp:Label id="Label30" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                                </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                    runat="server" ControlToValidate="txtConsignee" ErrorMessage="Please Enter the Consignee Details">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="height: 22px; text-align: right">
                            <asp:Label ID="Label33" runat="server" Text="Invoice To"></asp:Label></td>
                        <td colspan="3" style="height: 22px; text-align: left">
                            <asp:TextBox ID="txtInvoiceTo" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="91%" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
            <td class="profilehead" colspan="4" style="text-align: left">
                follow Up &nbsp;Details &nbsp;&nbsp;
            </td>
                    </tr>
                    <tr>
            <td style="height: 19px; text-align: right">
            </td>
            <td style="height: 19px; text-align: left">
            </td>
            <td style="height: 19px; text-align: right">
            </td>
            <td style="height: 19px; text-align: left;">
            </td>
                    </tr>
                    <tr>
            <td style="height: 19px; text-align: right" colspan="4"><table border="0" cellpadding="0" cellspacing="0" id="Table2" visible="true" width="100%">
                <tr>
            <td style="text-align: right; height: 22px;">
                <asp:Label id="lblResponsiblePerson" runat="server" Text="Responsible Person" Width="122px"></asp:Label></td>
            <td style="text-align: left; height: 22px;">
                <asp:DropDownList id="ddlResponsiblePerson" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlResponsiblePerson_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
                    <td style="height: 22px; text-align: right">
                        <asp:Label id="Label34" runat="server" Text="Phone No."></asp:Label></td>
                    <td style="height: 22px; text-align: left">
                        <asp:TextBox id="txtResponsiblePersonPhNo" runat="server" ReadOnly="True"></asp:TextBox></td>
            <td style="text-align: right; height: 22px;">
                <asp:Label id="lblFollowupEmail" runat="server" Text="Email"></asp:Label></td>
            <td style="text-align: left; height: 22px; width: 196px;">
                <asp:TextBox id="txtFollowupEmail" runat="server" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr>
            <td style="text-align: right">
                &nbsp;<asp:Label id="lblSalesPerson" runat="server" Text="Sales Person"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList id="ddlSalesPerson" runat="server" Enabled="False" OnSelectedIndexChanged="ddlSalesPerson_SelectedIndexChanged">
                </asp:DropDownList></td>
                    <td style="text-align: right">
                        <asp:Label id="Label36" runat="server" Text="Phone No."></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox id="txtSalesPersonPhNo" runat="server" ReadOnly="True"></asp:TextBox></td>
            <td style="text-align: right"><asp:Label id="Label37" runat="server" Text="Email"></asp:Label></td>
            <td style="text-align: left; width: 196px;"><asp:TextBox id="txtSalesPersonEMail" runat="server" ReadOnly="True"></asp:TextBox></td>
                </tr>
            </table>
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
                        <td class="profilehead" colspan="4" style="text-align: left">
                Reference Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlPreparedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label id="Label4" runat="server" Text="Approved By"></asp:Label></td>
                        <td style="text-align: left;">
                <asp:DropDownList id="ddlApprovedBy" runat="server" Enabled="False">
                </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label id="lblCheckedBy" runat="server" Text="Checked By" Visible="False"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:DropDownList id="ddlCheckedBy" runat="server" Enabled="False" Visible="False">
                            </asp:DropDownList></td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left;">
                        </td>
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
            <td colspan="4" style="height: 47px">
                <table id="tblButtons">
                    <tr>
                        <td>
                            <asp:Button id="btnSave" runat="server" onclick="btnSave_Click" Text="Save" /></td>
                        <td>
                            <asp:Button ID="btnApprove" runat="server" CausesValidation="False" OnClick="btnApprove_Click"
                                Text="Approve" /></td>
                        <td>
                            <asp:Button id="btnRefresh" runat="server" onclick="btnRefresh_Click" Text="Refresh" CausesValidation="False" /></td>
                        <td>
                            <asp:Button ID="btnSend" runat="server" CausesValidation="False" OnClick="btnSend_Click"
                                Text="Send" /></td>
                        <td>
                            <asp:Button id="btnPrint" runat="server" Text="Print" CausesValidation="False" OnClick="btnPrint_Click" /></td>
                        <td>
                            <asp:Button id="btnClose" runat="server" onclick="btnClose_Click" Text="Close" CausesValidation="False" /></td>
                    </tr>
                </table>
            </td>
                    </tr>
                </table>
                <asp:ValidationSummary id="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False">
                </asp:ValidationSummary>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td style="text-align: right;">
            </td>
            <td style="width: 231px">
            </td>
        </tr>
    </table>
</asp:Content>


 
