<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SparesOrderProfile.aspx.cs" Inherits="Modules_Services_SparesOrderProfile" Title="||ERP:Services:SparesOrderProfile||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                order profile</td>
        </tr>
    </table>
    <br />
    <table border="0" cellpadding="0" cellspacing="0" width="770">
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">
                            Order Profile</td>
                        <td>
                        </td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Label ID="Label12" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="SPOP_NO"> Order No</asp:ListItem>
                                            <asp:ListItem Value="SPOP_DATE"> Order Date</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME">Customer</asp:ListItem>
                                            <asp:ListItem Value="CUST_CONTACT_PERSON">Contact Person</asp:ListItem>
                                            <asp:ListItem Value="CUST_EMAIL">EMail Address</asp:ListItem>
                                            <asp:ListItem Value="SPOP_DELIVERY_DATE">Delivery Date</asp:ListItem>
                                            <asp:ListItem Value="SPOP_STATUS">Status</asp:ListItem>
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
                                        <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:TextBox ID="txtSearchValueFromDate" runat="server" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox><asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False" />
                                        <cc1:CalendarExtender  Format="dd/MM/yyyy" ID="ceSearchFrom" runat="server" Enabled="False" PopupButtonID="imgFromDate"
                                            TargetControlID="txtSearchValueFromDate">
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
                                            Visible="False" />
                                        <cc1:CalendarExtender  Format="dd/MM/yyyy" ID="ceSearchValueToDate" runat="server" Enabled="False" PopupButtonID="imgToDate"
                                            TargetControlID="txtSearchText">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                            UserDateFormat="MonthDayYear">
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
                            <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvWorkOrderDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="CR_ID,SPARES_QUOT_ID,SPO_ID,SPOP_ID" DataSourceID="sdsProfileDetails" AllowPaging="True" OnRowDataBound="gvWorkOrderDetails_RowDataBound" Width="100%">
                    <Columns>
<asp:BoundField DataField="SPOP_ID" SortExpression="SPOP_ID" HeaderText="Order IdHidden"></asp:BoundField>
<asp:TemplateField SortExpression="SPOP_NO" HeaderText="order  No."><EditItemTemplate>
<asp:TextBox id="TextBox1" runat="server" Text='<%# Bind("SPOP_NO") %>'></asp:TextBox> 
</EditItemTemplate>

<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnWorkOrderNo" onclick="lbtnWorkOrderNo_Click" runat="server" Text='<%# Bind("SPOP_NO") %>' CausesValidation="False"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="SPOP_DATE" SortExpression="SPOP_DATE" HeaderText="Order  Date">
<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CUST_NAME" SortExpression="CUST_NAME" HeaderText="Customer ">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CUST_CONTACT_PERSON" SortExpression="CUST_CONTACT_PERSON" HeaderText="Contact Person">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CUST_EMAIL" SortExpression="CUST_EMAIL" HeaderText="E-Mail Address">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="SPOP_DELIVERY_DATE" SortExpression="SPOP_DELIVERY_DATE" HeaderText="Delivery Date">
<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
<asp:BoundField ReadOnly="True" DataField="PREPAREDBY" SortExpression="PREPAREDBY" HeaderText="Prepared By">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField ReadOnly="True" DataField="APPROVEDBY" SortExpression="APPROVEDBY" HeaderText="Approved By">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="SPOP_STATUS" SortExpression="SPOP_STATUS" HeaderText="Status">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
</Columns>
                    <emptydatatemplate>
No Data Exist!
</emptydatatemplate>
                </asp:GridView><asp:SqlDataSource id="sdsProfileDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SERVICES_ORDERPROFILE_SEARCH_SELECT" SelectCommandType="StoredProcedure"><selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="EMPID" ControlID="lblEmpIdHidden"></asp:ControlParameter>
</selectparameters>
                </asp:SqlDataSource>
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: right; height: 19px;">
            </td>
            <td style="text-align: left; height: 19px;">
            </td>
            <td style="text-align: right; height: 19px;">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table id="Table1">
                    <tr>
                        <td>
                            </td>
                        <td>
                            </td>
                        <td>
                            </td>
                        <td>
                            </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table border="0" cellpadding="0" cellspacing="0" id="tblWorkOrderDetails" runat="server"
                    width="100%" visible="false">
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">
                            General Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left; width: 228px;">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left; width: 289px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblSalesOrderNo" runat="server" Text="Spares Order No" Width="115px"></asp:Label></td>
                        <td style="text-align: left; width: 228px;">
                            <asp:DropDownList ID="ddlOrderAcceptance" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOrderAcceptance_SelectedIndexChanged" Enabled="False">
                            </asp:DropDownList><asp:Label id="Label36" runat="server" EnableTheming="False" ForeColor="Red" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlOrderAcceptance"
                                ErrorMessage="Please Select The Order Acceptance No." InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblSalesOrderDate" runat="server" Text="Spares Order Date" Width="121px"></asp:Label></td>
                        <td style="text-align: left; width: 289px;">
                            <asp:TextBox ID="txtOADate" runat="server" ReadOnly="True" CssClass="datetext" EnableTheming="False"></asp:TextBox>&nbsp;<asp:Image ID="imgOADate"
                                runat="server" ImageUrl="~/Images/Calendar.png" Visible="False"></asp:Image><cc1:CalendarExtender
                                   Format="dd/MM/yyyy" ID="ceOADate" runat="server" PopupButtonID="imgOADate" TargetControlID="txtOADate" Enabled="False">
                                </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeOADate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtOADate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblCustomer" runat="server" Text="Customer Name"></asp:Label></td>
                        <td style="text-align: left; width: 228px;">
                            <asp:TextBox ID="txtCustName" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label45" runat="server" Text="Unit Name"></asp:Label></td>
                        <td style="text-align: left; width: 289px;">
                            <asp:TextBox ID="txtUnitName" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td rowspan="2" style="text-align: right">
                            <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label></td>
                        <td rowspan="2" style="text-align: left; width: 228px;">
                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblCity" runat="server" Text="Region"></asp:Label></td>
                        <td style="text-align: left; width: 289px;">
                            <asp:TextBox ID="txtRegion" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label25" runat="server" Text="Phone"></asp:Label></td>
                        <td style="text-align: left; width: 289px;">
                            <asp:TextBox ID="txtPhone" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
                        <td style="text-align: left; width: 228px;">
                            <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label26" runat="server" Text="Mobile"></asp:Label></td>
                        <td style="text-align: left; width: 289px;">
                            <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">
                            Sales Order Items</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 21px">
                            <asp:GridView ID="gvOrderAcceptanceItems" runat="server" AutoGenerateColumns="False"
                                Width="100%" OnRowDataBound="gvOrderAcceptanceItems_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code" >
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Rate" HeaderText="Rate" >
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Amount" >
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Specifications" HeaderText="Specifications" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Priority" HeaderText="Priority" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4">
                            Order Profile details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left; width: 228px;">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left; width: 289px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label1" runat="server" Text="Order Profile No"></asp:Label></td>
                        <td style="text-align: left; width: 228px;">
                            <asp:TextBox ID="txtWONo" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label2" runat="server" Text="Order Profile Date" Width="114px"></asp:Label></td>
                        <td style="text-align: left; width: 289px;">
                            <asp:TextBox ID="txtWODate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox><asp:Image ID="imgWODate" runat="server" ImageUrl="~/Images/Calendar.png" /><asp:Label id="Label10" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                            </asp:Label>
                            <asp:RequiredFieldValidator
                                ID="rfvWODate" runat="server" ControlToValidate="txtWODate" ErrorMessage="Please Select the Work Order Date">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="DateCustomValidate"
                                ControlToValidate="txtWODate" ErrorMessage="Please Enter the Work Order Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                SetFocusOnError="True">*</asp:CustomValidator>
                            <cc1:CalendarExtender
                                    ID="ceWODate" runat="server" PopupButtonID="imgWODate" TargetControlID="txtWODate" Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeWODate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtWODate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblModeofDelivery" runat="server" Text="Mode of Delivery"></asp:Label></td>
                        <td style="text-align: left; width: 228px;">
                            <asp:DropDownList ID="ddlDeliveryMode" runat="server">
                            </asp:DropDownList><asp:Label id="Label11" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                            </asp:Label>
                            <asp:RequiredFieldValidator ID="rfvDeliveryMode" runat="server" ControlToValidate="ddlDeliveryMode"
                                ErrorMessage="Please Select the Mode of  Delivery" InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label6" runat="server" Text="Inspection Date"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 289px;">
                            <asp:TextBox ID="txtInspectionDate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox><asp:Image ID="imgInspectionDate" runat="server" ImageUrl="~/Images/Calendar.png" /><asp:Label id="Label13" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                            </asp:Label>
                            <asp:RequiredFieldValidator
                                ID="rfvInspectionDate" runat="server" ControlToValidate="txtInspectionDate" ErrorMessage="Please Select the Inspection Date">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="DateCustomValidate"
                                ControlToValidate="txtInspectionDate" ErrorMessage="Please Enter the Inspection Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                SetFocusOnError="True">*</asp:CustomValidator>
                            <cc1:CalendarExtender
                                  Format="dd/MM/yyyy"  ID="ceInspectionDate" runat="server" PopupButtonID="imgInspectionDate" TargetControlID="txtInspectionDate">
                                </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeeInspectionDate" runat="server" DisplayMoney="Left"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtInspectionDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label15" runat="server" Text="C.S. Tax"></asp:Label></td>
                        <td style="text-align: left; width: 228px;">
                            <asp:TextBox ID="txtCST" runat="server">
                </asp:TextBox><asp:Label ID="Label32" runat="server" Text="%"></asp:Label><asp:Label id="Label14" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                </asp:Label><asp:RequiredFieldValidator ID="rfvCST" runat="server" ControlToValidate="txtCST"
                                ErrorMessage="Please Enter the CS Tax">*</asp:RequiredFieldValidator>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="lblDeliveryDate" runat="server" Text="Delivery Date" Height="17px"></asp:Label></td>
                        <td style="text-align: left; width: 289px;">
                            <asp:TextBox ID="txtDeliveryDate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox><asp:Image ID="imgDeliveryDate" runat="server" ImageUrl="~/Images/Calendar.png" /><asp:Label id="Label16" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                            </asp:Label>
                            <asp:RequiredFieldValidator
                                ID="rfvDeliveryDate" runat="server" ControlToValidate="txtDeliveryDate" ErrorMessage="Please Select the Delivery  Date">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="DateCustomValidate"
                                ControlToValidate="txtDeliveryDate" ErrorMessage="Please Enter the Delivery Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                SetFocusOnError="True">*</asp:CustomValidator>
                            <cc1:CalendarExtender
                                 Format="dd/MM/yyyy"   ID="ceDeliveryDate" runat="server" PopupButtonID="imgDeliveryDate" TargetControlID="txtDeliveryDate">
                                </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeDeliveryDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtDeliveryDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label7" runat="server" Text="Frieght"></asp:Label></td>
                        <td style="text-align: left; width: 228px;">
                            <asp:TextBox ID="txtFrieght" runat="server"></asp:TextBox><asp:Label id="Label17" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                            </asp:Label>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFrieght" ErrorMessage="Please Enter the Frieght" Visible="False">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label8" runat="server" Text="Road Permit"></asp:Label></td>
                        <td style="text-align: left; width: 289px;">
                            <asp:TextBox ID="txtRoadPermit" runat="server"></asp:TextBox><asp:Label id="Label18" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                            </asp:Label>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRoadPermit"
                                ErrorMessage="Please Enter the Road Permit">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label9" runat="server" Text="Insurance" Visible="False"></asp:Label></td>
                        <td style="text-align: left; width: 228px;">
                            <asp:TextBox ID="txtInsurance" runat="server" Visible="False" Width="146px"></asp:TextBox><asp:Label id="Label19" runat="server" EnableTheming="False" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtInsurance"
                                ErrorMessage="Please Enter the Insurance" Visible="False">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left; width: 289px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label3" runat="server" Text="packing & forwarding Instructions" Width="138px"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtPackingInstrs" runat="server" CssClass="multilinetext" EnableTheming="False" TextMode="MultiLine" Width="92%"></asp:TextBox><asp:Label id="Label20" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                            </asp:Label>
                            <asp:RequiredFieldValidator
                                ID="rfvPackingInstr" runat="server" ControlToValidate="txtPackingInstrs" ErrorMessage="Please Enter the Packing nad Forwarding Instructions">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label4" runat="server" Text="Accessories "></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtAccessories" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="92%" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 52px;">
                            <asp:Label ID="Label5" runat="server" Text="Extra spares "></asp:Label>
                        </td>
                        <td style="text-align: left; height: 52px;" colspan="3">
                            <asp:TextBox ID="txtExtraSpace" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="92%" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label id="Label52" runat="server" Text="Advance Amt"></asp:Label></td>
                        <td colspan="3" style="height: 19px; text-align: left">
                            <asp:TextBox id="txtAdvanceAmt" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label22" runat="server" Text="Attachment" Visible="False"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False" Visible="False">
                                <ContentTemplate>
<asp:FileUpload id="FileUpload1" runat="server" Font-Size="8pt" Font-Names="Verdana" Width="510px" Visible="False" Font-Italic="False"></asp:FileUpload> 
</ContentTemplate>
                                <Triggers>
<asp:PostBackTrigger ControlID="btnSave"></asp:PostBackTrigger>
</Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label23" runat="server" Text="Attached File"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:LinkButton ID="lbtnAttachedFiles" runat="server"></asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Reference Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left; width: 228px;">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left; width: 289px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 22px;">
                            <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left; height: 22px; width: 228px;">
                            <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False" EnableTheming="True">
                            </asp:DropDownList></td>
                        <td style="text-align: right; height: 22px;">
                            <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By"></asp:Label></td>
                        <td style="text-align: left; height: 22px; width: 289px;">
                            <asp:DropDownList ID="ddlApprovedBy" runat="server" Enabled="False" EnableTheming="True">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="lblCheckedBy" runat="server" Text="Checked By" Visible="False"></asp:Label></td>
                        <td style="height: 19px; text-align: left; width: 228px;">
                            <asp:DropDownList ID="ddlCheckedBy" runat="server" EnableTheming="False" Visible="False">
                            </asp:DropDownList></td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left; width: 289px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;">
                        </td>
                        <td style="text-align: left; height: 19px; width: 228px;">
                        </td>
                        <td style="text-align: right; height: 19px;">
                        </td>
                        <td style="text-align: left; height: 19px; width: 289px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 47px">
                            <table id="tblButtons">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnApprove" runat="server" CausesValidation="False" OnClick="btnApprove_Click"
                                            Text="Approve" /></td>
                                    <td>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CausesValidation="False" OnClick="btnEdit_Click" /></td>
                                    <td>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CausesValidation="False"
                                OnClick="btnDelete_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CausesValidation="False"
                                            OnClick="btnRefresh_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnSend" runat="server" CausesValidation="False" OnClick="btnSend_Click"
                                            Text="Order Accept" Width="113px" Height="24px" /></td>
                                    <td>
                            <asp:Button ID="btnPrint" runat="server" Text="Print" CausesValidation="False"
                                OnClick="btnPrint_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnClose" runat="server" Text="Close" CausesValidation="False" OnClick="btnClose_Click" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="width: 228px">
                        </td>
                        <td style="text-align: right;">
                        </td>
                        <td style="width: 289px">
                        </td>
                    </tr>
                </table>
                <table style="width: 189px" id="tblHiddenFields" runat="server" visible="false">
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnNew" runat="server" Text="New" CausesValidation="False" OnClick="btnNew_Click" Visible="False" /></td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>

 
