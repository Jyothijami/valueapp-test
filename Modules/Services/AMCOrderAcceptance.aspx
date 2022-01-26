<%@ Page Language="C#" MasterPageFile="~/MPs/ServicesMP1.master" AutoEventWireup="true" 
    CodeFile="AMCOrderAcceptance.aspx.cs" Inherits="Modules_Services_AMCOrderAcceptance" Title="|| Value App : Services : AMC Order Acceptance ||" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                AMC
                Order Confirmation</td>
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
    <table border="0" cellpadding="0" cellspacing="0" style="Width:100%">
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">
                            Confirmed Orders</td>
                        <td>
                        </td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td style="height: 25px">
                                        <asp:Label id="Label3" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList id="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="OA_NO"> Order Acceptance No</asp:ListItem>
                                            <asp:ListItem Value="OA_DATE">Order Acceptance Date</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME">Customer</asp:ListItem>
                                            <asp:ListItem Value="CUST_CONTACT_PERSON">Contact Person</asp:ListItem>
                                            <asp:ListItem Value="CUST_EMAIL">EMail</asp:ListItem>
                                            <asp:ListItem Value="OA_FLAG">Status</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="height: 25px">
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
                                    <td style="height: 25px">
                                        <asp:Label id="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:TextBox id="txtSearchValueFromDate" runat="server" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox><asp:Image id="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender  Format="MM/dd/yyyy" ID="ceSearchFrom" runat="server" enabled="False" popupbuttonid="imgFromDate"
                                            targetcontrolid="txtSearchValueFromDate"></cc1:calendarextender>
                                        <cc1:maskededitextender id="MaskedEditSearchFromDate" runat="server" displaymoney="Left"
                                            enabled="False" mask="99/99/9999" masktype="Date" targetcontrolid="txtSearchValueFromDate"
                                            userdateformat="MonthDayYear"></cc1:maskededitextender>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Label id="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:TextBox id="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><asp:Image id="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender  Format="MM/dd/yyyy" ID="ceSearchValueToDate" runat="server" enabled="False" popupbuttonid="imgToDate"
                                            targetcontrolid="txtSearchText"></cc1:calendarextender>
                                        <cc1:maskededitextender id="MaskedEditSearchToDate" runat="server" displaymoney="Left"
                                            enabled="False" mask="99/99/9999" masktype="Date" targetcontrolid="txtSearchText"
                                            userdateformat="MonthDayYear"></cc1:maskededitextender>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Button id="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" onclick="btnSearchGo_Click" Text="Go" /></td>
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
                <asp:GridView id="gvOrderAcceptanceDetails" SelectedRowStyle-BackColor="#c0c0c0" runat="server" Width="100%" AllowPaging="True"
                    AutoGenerateColumns="False" DataSourceID="sdsOrderAcceptanceDetails" 
                    OnRowDataBound="gvOrderAcceptanceDetails_RowDataBound" AllowSorting="True">
                    <columns>
<asp:BoundField DataField="OA_ID" SortExpression="OA_ID" HeaderText="OrderScheduleIdHidden"></asp:BoundField>
<asp:TemplateField HeaderText=" Order Schedule No" SortExpression="OA_NO"><EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("OA_NO") %>' ID="TextBox1"></asp:TextBox>
                        
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnOrderAcceptanceNo" onclick="lbtnOrderAcceptanceNo_Click" ForeColor="#0066ff" runat="server" Text='<%# Bind("OA_NO") %>' CausesValidation="False"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HtmlEncode="False" SortExpression="OA_DATE" DataFormatString="{0:MM/dd/yyyy}" ReadOnly="True" DataField="OA_DATE" HeaderText=" Order Schedule Date"></asp:BoundField>
<asp:BoundField DataField="CUST_NAME" SortExpression="CUST_NAME" HeaderText="Customer">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CUST_CONTACT_PERSON" SortExpression="CUST_CONTACT_PERSON" HeaderText="Contact Person">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CUST_EMAIL" SortExpression="CUST_EMAIL" HeaderText="Email">
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
<asp:BoundField DataField="OA_FLAG" SortExpression="OA_FLAG" HeaderText="Status">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
</columns>
                    <emptydatatemplate>
No Data Exist!
</emptydatatemplate>
                    <selectedrowstyle backcolor="LightSteelBlue" />
                </asp:GridView><asp:SqlDataSource id="sdsOrderAcceptanceDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SERVICES_AMCORDERACCEPTANCE_SEARCH_SELECT" SelectCommandType="StoredProcedure"><selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="EMPID" ControlID="lblEmpIdHidden"></asp:ControlParameter>
</selectparameters>
                </asp:SqlDataSource>
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
                                Text="New" /></td>
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
            <td style="height: 19px; text-align: left; width: 200px;">
            </td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                <asp:Label id="lblSalesOrderNo" runat="server" Text="AMC OP No" Width="103px"></asp:Label></td>
            <td style="text-align: left"><asp:DropDownList id="ddlSONo" runat="server" OnSelectedIndexChanged="ddlSONo_SelectedIndexChanged" AutoPostBack="True">
            </asp:DropDownList>
                <asp:Label id="Label6" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSONo"
                    ErrorMessage="Please Select the Sales Order No" InitialValue="0">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right;">
                <asp:Label id="lblSalesOrderDate" runat="server" Text="AMC OP Date" Width="114px"></asp:Label></td>
            <td style="text-align: left; width: 200px;">
                <asp:TextBox id="txtSODate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox>&nbsp;<asp:Image id="imgSODate" runat="server" ImageUrl="~/Images/Calendar.png"
                    ></asp:Image><cc1:CalendarExtender  Format="dd/MM/yyyy" ID="ceSODate" runat="server" PopupButtonID="imgSODate"
                        TargetControlID="txtSODate">
                    </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="meeSoDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                    MaskType="Date" TargetControlID="txtSODate" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
            </td>
                    </tr>
                    <tr>
                        <td id="TD18" style="height: 22px; text-align: right">
                            <asp:Label ID="lblCustomer" runat="server" Text="Customer"></asp:Label></td>
                        <td style="height: 22px; text-align: left">
                            <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="True" Enabled="False"
                                OnSelectedIndexChanged="ddlCustomerName_SelectedIndexChanged">
                            </asp:DropDownList><asp:Label ID="Label25" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCustomerName"
                                ErrorMessage="Please Select the Customer" InitialValue="0">*</asp:RequiredFieldValidator>
                            <asp:Label id="lblAMCOIdHidden" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Black" Visible="False"></asp:Label></td>
                        <td style="height: 22px; text-align: right">
                            <asp:Label ID="lblRegion" runat="server" Text="Region"></asp:Label></td>
                        <td style="height: 22px; text-align: left">
                            <asp:TextBox ID="txtRegion" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 22px; text-align: right">
                            <asp:Label ID="Label26" runat="server" Text="Industry Type"></asp:Label></td>
                        <td style="height: 22px; text-align: left">
                            <asp:TextBox ID="txtIndustryType" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="height: 22px; text-align: right">
                            <asp:Label ID="lblInitName" runat="server" Text="Unit Name" Width="74px"></asp:Label></td>
                        <td style="height: 22px; text-align: left">
                            <asp:DropDownList ID="ddlUnitName" runat="server" AutoPostBack="True" Enabled="False"
                                OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="0">--Select Customer--</asp:ListItem>
                            </asp:DropDownList><asp:RequiredFieldValidator ID="rfvUnitName" runat="server" ControlToValidate="ddlUnitName"
                                ErrorMessage="Please Select the Unit Name" InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblUnitAddress" runat="server" Text="Unit Address" Width="106px"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtUnitAddress" runat="server" CssClass="multilinetext" EnableTheming="False"
                                Font-Names="Verdana" Font-Size="8pt" ReadOnly="True" TextMode="MultiLine" Width="493px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtContactPerson" runat="server" ReadOnly="True">
                            </asp:TextBox><asp:DropDownList ID="ddlContactPerson" runat="server" AutoPostBack="True"
                                Enabled="False" OnSelectedIndexChanged="ddlContactPerson_SelectedIndexChanged"
                                Visible="False">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="0">--Select Unit Name--</asp:ListItem>
                            </asp:DropDownList><asp:RequiredFieldValidator ID="rfvContactPerson" runat="server"
                                ControlToValidate="ddlContactPerson" ErrorMessage="Please Select the Contact Person"
                                InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td id="TD28" style="text-align: right">
                            <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No" Width="74px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPhoneNo" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label47" runat="server" Text="Mobile" Width="74px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
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
            <td colspan="4" style="height: 21px">
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
            <td style="height: 19px; text-align: left; width: 200px;">
            </td>
                    </tr>
                    <tr>
            <td style="height: 19px; text-align: right">
                <asp:Label ID="Label1" runat="server" Text="Order Schedule No" Width="141px"></asp:Label></td>
            <td style="height: 19px; text-align: left">
                <asp:TextBox ID="txtOANo" runat="server" ReadOnly="True"></asp:TextBox></td>
            <td style="height: 19px; text-align: right">
                <asp:Label ID="Label2" runat="server" Text=" Order Schedule  Date" Width="150px"></asp:Label></td>
            <td style="height: 19px; text-align: left; width: 200px;">
                <asp:TextBox ID="txtOADate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox><asp:Image ID="imgOADate" runat="server"
                    ImageUrl="~/Images/Calendar.png" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOADate"
                    ErrorMessage="Please Select the Order Schedule Date">*</asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator5" runat="server" ClientValidationFunction="DateCustomValidate"
                    ControlToValidate="txtOADate" ErrorMessage="Please Enter the Order Schedule Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
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
                order Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left; width: 200px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="lblVisits" runat="server" Text="No  Of  PM Visits"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:TextBox ID="txtPmVisits" runat="server">
                            </asp:TextBox></td>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label31" runat="server" Text="No. Of BD Calls"></asp:Label></td>
                        <td style="width: 200px; height: 19px; text-align: left">
                            <asp:TextBox ID="txtBDCalls" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label id="lblSchedule" runat="server" Text="PM Visit Schedule"></asp:Label></td>
                        <td colspan="3" style="height: 24px; text-align: left" valign="middle">
                            <asp:TextBox id="txtPMSchedule" runat="server" TextMode="MultiLine" Width="92%" CssClass="multilinetext" EnableTheming="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label id="lblPayment" runat="server" Text="Payment"></asp:Label></td>
                        <td colspan="3" style="height: 24px; text-align: left" valign="middle">
                            <asp:TextBox id="txtPayment" runat="server" TextMode="MultiLine" Width="92%" CssClass="multilinetext" EnableTheming="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label id="lblPaymentStatus" runat="server" Text="Payment Status"></asp:Label></td>
                        <td colspan="3" style="height: 24px; text-align: left" valign="middle">
                            <asp:TextBox id="txtPaymentStatus" runat="server" TextMode="MultiLine" Width="92%" CssClass="multilinetext" EnableTheming="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblInvoiceStatus" runat="server" Text="Invoice Status" Visible="False"></asp:Label></td>
                        <td colspan="3" style="text-align: left" valign="middle">
                            <asp:TextBox id="txtInvoiceStatus" runat="server" TextMode="MultiLine" Width="92%" CssClass="multilinetext" EnableTheming="False" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblInvoiceDetails" runat="server" Text="Invoice Details" Visible="False"></asp:Label></td>
                        <td colspan="3" style="text-align: left" valign="middle">
                            <asp:TextBox id="txtInvoiceDetails" runat="server" TextMode="MultiLine" Width="92%" CssClass="multilinetext" EnableTheming="False" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                        <td style="text-align: right">
                        </td>
                        <td style="width: 200px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label35" runat="server" Text="AMC Amount"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtAmcAmt" runat="server" ReadOnly="True"></asp:TextBox><asp:Label
                                ID="Label36" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtAmcAmt" ErrorMessage="Please Enter the AMC Amount">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                        </td>
                        <td style="width: 200px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label15" runat="server" Text="Service Tax"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCST" runat="server">
                </asp:TextBox><asp:Label ID="Label17" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                    ID="rfvCST" runat="server" ControlToValidate="txtCST" ErrorMessage="Please Enter the Service Tax">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label33" runat="server" Text="Total Amount"></asp:Label></td>
                        <td style="text-align: left; width: 200px;">
                            <asp:TextBox ID="txtAmcTotAmt" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            PM Calls Schedule</td>
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
                            <asp:Label id="Label32" runat="server" Text="Call Name"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtCallName" runat="server">
                            </asp:TextBox><asp:Label id="Label34" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                    id="RequiredFieldValidator8" runat="server" ControlToValidate="txtCallName" ErrorMessage="Please Enter the PM Calls"
                                    ValidationGroup="call">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label id="Label37" runat="server" Text="Call Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtCallDate" runat="server" CssClass="datetext" EnableTheming="False">
                            </asp:TextBox><asp:Image id="imgCallDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><asp:Label
                                id="Label38" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                    id="RequiredFieldValidator9" runat="server" ControlToValidate="txtCallDate" ErrorMessage="Please Enter the Call Date"
                                    ValidationGroup="call">*</asp:RequiredFieldValidator><cc1:CalendarExtender ID="CalendarExtender1"
                                        runat="server" Format="dd/MM/yyyy" PopupButtonID="imgCallDate" TargetControlID="txtCallDate">
                                    </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" DisplayMoney="Left"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtCallDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
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
                        <td colspan="4">
                            <asp:Button id="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" onclick="btnAdd_Click" Text="Add"
                                ValidationGroup="call" /><asp:Button id="btnItemRefresh" runat="server" BackColor="Transparent"
                                    BorderStyle="None" CausesValidation="False" CssClass="loginbutton" EnableTheming="False"
                                    onclick="btnItemRefresh_Click" Text="Refresh" /></td>
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
                        <td colspan="4">
                            <asp:GridView id="gvQuotationItems" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvQuotationItems_RowDataBound"
                                OnRowDeleting="gvQuotationItems_RowDeleting" OnRowEditing="gvQuotationItems_RowEditing">
                                <columns>
<asp:CommandField ShowEditButton="True"></asp:CommandField>
<asp:CommandField ShowDeleteButton="True"></asp:CommandField>
<asp:BoundField DataField="callname" HeaderText="Call Name">
</asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="calldate" HeaderText="Call Date"></asp:BoundField>
</columns>
                            </asp:GridView>
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
                        <td style="height: 19px; text-align: left; width: 200px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 22px; text-align: right">
                            <asp:Label ID="Label7" runat="server" Text="Consigneement To"></asp:Label></td>
                        <td colspan="3" style="height: 22px; text-align: left">
                            <asp:TextBox ID="txtConsignee" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="92%"></asp:TextBox><asp:Label id="Label30" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                    runat="server" ControlToValidate="txtConsignee" ErrorMessage="Please Enter the Consignee Details">*</asp:RequiredFieldValidator></td>
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
            <td style="height: 19px; text-align: left; width: 200px;">
            </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblResponsiblePerson" runat="server" Text="Responsible Person" Width="134px">
                            </asp:Label></td>
                        <td style="width: 269px; text-align: left">
                            <asp:TextBox ID="txtResponsiblePerson" runat="server"></asp:TextBox>
                            <asp:Label ID="Label49" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvResPerson" runat="server" ControlToValidate="txtResponsiblePerson"
                                ErrorMessage="Please Enter the Responsible Person" InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblFollowupEmail" runat="server" Text="Email"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtFollowupEmail" runat="server"></asp:TextBox>
                            <asp:Label ID="Label50" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtFollowupEmail"
                                    ErrorMessage="Please Enter the Responsible Person Email" InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
            <td style="height: 19px; text-align: right">
                </td>
            <td style="height: 19px; text-align: left">
                </td>
            <td style="height: 19px; text-align: right">
            </td>
            <td style="height: 19px; text-align: left; width: 200px;">
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
                        <td style="height: 19px; text-align: left; width: 200px;">
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
                        <td style="text-align: left; width: 200px;">
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
                        <td style="height: 19px; text-align: left; width: 200px;">
                        </td>
                    </tr>
                    <tr>
            <td style="text-align: right; height: 19px;">
            </td>
            <td style="text-align: left; height: 19px;">
            </td>
            <td style="text-align: right; height: 19px;">
            </td>
            <td style="text-align: left; height: 19px; width: 200px;">
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
                </table><table border="0" cellpadding="0" cellspacing="0" id="Table2" runat="server" visible="false" width="100%">
                    <tr>
            <td style="text-align: right">
                <asp:Label id="Label10" runat="server" Text="Delivery"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtDelivery" runat="server">
                </asp:TextBox>
                <asp:Label id="Label8" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                <asp:RequiredFieldValidator id="rfvDelivery" runat="server" ControlToValidate="txtDelivery"
                    ErrorMessage="Please Enter the Delivery">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right">
                <asp:Label id="Label5" runat="server" Text="currency type"></asp:Label></td>
            <td style="text-align: left; width: 200px;"><asp:DropDownList ID="ddlCurrencyType" runat="server">
            </asp:DropDownList>
                <asp:Label id="Label14" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCurrencyType"
                    ErrorMessage="Please Select the Currency Type" InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                <asp:Label id="Label12" runat="server" Text="packing charges"></asp:Label>
            </td>
            <td style="text-align: left">
                <asp:TextBox id="txtPackingCharges" runat="server">
                </asp:TextBox>
                <asp:Label id="Label9" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                <asp:RequiredFieldValidator id="rfvPackingChrgs" runat="server" ControlToValidate="txtPackingCharges"
                    ErrorMessage="Please Enter the Packing Charges">*</asp:RequiredFieldValidator>
                <cc1:FilteredTextBoxExtender ID="ftxtePackingCharges" runat="server" FilterType="Numbers"
                    TargetControlID="txtPackingCharges">
                </cc1:FilteredTextBoxExtender>
                <asp:DropDownList id="ddlSalesPerson" runat="server">
                </asp:DropDownList></td>
            <td style="text-align: right">
                <asp:Label ID="Label11" runat="server" Text="payment terms"></asp:Label></td>
            <td style="text-align: left; width: 200px;">
                <asp:TextBox id="txtPaymentTerms" runat="server">
                </asp:TextBox>
                <asp:Label id="Label18" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                <asp:RequiredFieldValidator id="rfvPayTerms" runat="server" ControlToValidate="txtPaymentTerms"
                    ErrorMessage="Please Enter the Payment Terms">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                </td>
            <td style="text-align: left">
                &nbsp;
            </td>
            <td style="text-align: right">
                <asp:Label id="Label13" runat="server" Text="Excise Duty"></asp:Label></td>
            <td style="text-align: left; width: 200px;">
                <asp:TextBox id="txtExciseDuty" runat="server">
                </asp:TextBox>
                <asp:Label id="Label19" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                <asp:RequiredFieldValidator id="rfvExciseDuty" runat="server" ControlToValidate="txtExciseDuty"
                    ErrorMessage="Please Enter the Excise Duty">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                <asp:Label id="Label20" runat="server" Text="Guarantee"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtGuarantee" runat="server">
                </asp:TextBox>
                <asp:Label id="Label22" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                <asp:RequiredFieldValidator id="rfvGuarantee" runat="server" ControlToValidate="txtGuarantee"
                    ErrorMessage="Please Enter the Guarantee">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right">
                <asp:Label id="Label16" runat="server" Text="Despatch Mode"></asp:Label></td>
            <td style="text-align: left; width: 200px;"><asp:DropDownList ID="ddlDespatchMode" runat="server">
            </asp:DropDownList>
                <asp:Label id="Label21" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvDespatchMode" runat="server"
                ControlToValidate="ddlDespatchMode" ErrorMessage="Please Select the Despatch Mode"
                InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                <asp:Label id="Label24" runat="server" Text="Erection/Commissioning"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtErrection" runat="server">
                </asp:TextBox>
                <asp:Label id="Label23" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                <asp:RequiredFieldValidator id="rfvErrection" runat="server" ControlToValidate="txtErrection"
                    ErrorMessage="Please Enter the Erection/Commissioning">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right">
                <asp:Label id="Label27" runat="server" Text="Inspection"></asp:Label></td>
            <td style="text-align: left; width: 200px;">
                <asp:TextBox id="txtInspection" runat="server">
                </asp:TextBox>
                <asp:Label id="Label29" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                <asp:RequiredFieldValidator id="rfvInspection" runat="server" ControlToValidate="txtInspection"
                    ErrorMessage="Please Enter the Inspection">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                <asp:Label id="Label28" runat="server" Text="Other specifications"></asp:Label></td>
                        <td colspan="3" style="text-align: left" valign="middle">
                            <asp:TextBox ID="txtOtherSpecs" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="94%"></asp:TextBox>
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

 
