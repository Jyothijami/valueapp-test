<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdvertisementAgenciesInfo.ascx.cs" Inherits="Modules_SM_AdvertisementAgenciesInfo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="../../App_Themes/Master/Master.css" rel="stylesheet" type="text/css" /> 
 <table style="width: 783px" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="searchhead" colspan="2" style="text-align: left;">
                &nbsp;<table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">
                            AdvertisingAgenciesInformation&nbsp;</td>
                        <td>
                        </td>
                        <td style="width: 837px; text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td rowspan="3">
                                        <asp:Label ID="Label1" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By" Width="95px"></asp:Label></td>
                                    <td rowspan="3" style="width: 142px;">
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged1">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="AAI_ORG_NAME">Organization Name</asp:ListItem>
                                            <asp:ListItem Value="AAI_NO">Advertise No</asp:ListItem>
                                            <asp:ListItem Value="AAI_DATE">Advertising Date</asp:ListItem>
                                            <asp:ListItem Value="ADVM_NAME">Advertising Mode</asp:ListItem>
                                            <asp:ListItem Value="AA_NAME">Advertising Agency</asp:ListItem>
                                            <asp:ListItem Value="AM_NAME">Advertising Magzine</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td rowspan="3">
                                        <asp:DropDownList ID="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                            EnableTheming="False" Visible="False" Width="50px" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged1">
                                            <asp:ListItem Selected="True">=</asp:ListItem>
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem>&gt;</asp:ListItem>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <asp:ListItem>&gt;=</asp:ListItem>
                                            <asp:ListItem>R</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td rowspan="3">
                                        <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                                    <td rowspan="3">
                                        <asp:TextBox ID="txtSearchValueFromDate" runat="server" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox><asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False" />
                                        <cc1:CalendarExtender ID="ceSearchFrom" runat="server" Enabled="False" Format="dd/MM/yyyy"
                                            PopupButtonID="imgFromDate" TargetControlID="txtSearchValueFromDate">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td rowspan="3">
                                        <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                    <td rowspan="3">
                                        <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False" />
                                        <cc1:CalendarExtender ID="ceSearchValueToDate" runat="server" Enabled="False" Format="dd/MM/yyyy"
                                            PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td rowspan="3">
                                        <asp:Button ID="Button1" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                </tr>
                            </table>
                            <asp:Label id="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>&nbsp;
                            <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 19px; text-align: center">
                <asp:GridView ID="gvAdvertisingInfo" runat="server" AllowPaging="True" AutoGenerateColumns="False"  DataSourceID="sdssingAgencyInfo" OnRowDataBound="gvAdvertisingInfo_RowDataBound">
                   
                    <Columns>
                        <asp:BoundField DataField="AAI_ID" HeaderText="AdvertiseNoIdHidden" />
                        <asp:BoundField DataField="AAI_ORG_NAME" HeaderText="Organization Name">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Advertise No">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("AAI_NO") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnSizeOfAdvertising" runat="server" CausesValidation="False"
                                    OnClick="lbtnSizeOfAdvertising_Click" Text="<%# Bind('AAI_NO') %>"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="AAI_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Advertising Date"
                            HtmlEncode="False" SortExpression="AAI_DATE" />
                        <asp:BoundField DataField="ADVM_NAME" HeaderText="Advertising Mode" NullDisplayText="-">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AA_NAME" HeaderText="Advertising Agency" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AM_NAME" HeaderText="Advertising Magzine" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        No Record Found
                    </EmptyDataTemplate>
                    <SelectedRowStyle BackColor="LightSteelBlue" />
                </asp:GridView>
                <asp:SqlDataSource ID="sdssingAgencyInfo" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SM_ADVERTISING_AGENCY_INFORAMTION_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName"
                            PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchTypeHidden" DefaultValue="0" Name="SearchType"
                            PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue"
                            PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueFromHidden" DefaultValue="0" Name="SearchValueFrom"
                            PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <table id="Table1">
                    <tr>
                        <td>
                            <asp:Button id="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CausesValidation="False" /></td>
                        <td>
                            <asp:Button id="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CausesValidation="False" /></td>
                        <td>
                            <asp:Button id="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" CausesValidation="False" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                &nbsp;<table id="tblAdvertiseInfo" runat="server" border="0" cellpadding="0" cellspacing="0"
                    visible="false">
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                General Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="width: 244px; height: 21px; text-align: left">
                        </td>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label ID="lblAdvertisingNo" runat="server" Text="Advertising No"></asp:Label></td>
                        <td style="width: 244px; height: 24px; text-align: left">
                            <asp:TextBox ID="txtAdvertisingNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="height: 24px; text-align: right">
                            <asp:Label ID="lblAdvertisingDate" runat="server" Text="Advertising Date"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                            <asp:TextBox ID="txtAdvertisingDate" runat="server" ReadOnly="True"></asp:TextBox><asp:Image
                                ID="imgAdvertisingDate" runat="server" ImageUrl="~/Images/Calendar.png" /><cc1:CalendarExtender
                                    ID="ceAdvertisingDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgAdvertisingDate"
                                    TargetControlID="txtAdvertisingDate">
                                </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeAdvertisingDate" runat="server" DisplayMoney="Left"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtAdvertisingDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblAdvertisingMode" runat="server" Text="Advertising Mode" Width="119px"></asp:Label></td>
                        <td style="width: 244px; text-align: left">
                            <asp:DropDownList ID="ddlAdvertisingMode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAdvertisingMode_SelectedIndexChanged" >
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblAgency" runat="server" Text="Advertising Agency" Width="119px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlAdvertisingAgency" runat="server" >
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 37px;">
                            <asp:Label ID="lblMagzines" runat="server" Text="Magzines" Width="119px"></asp:Label></td>
                        <td style="width: 244px; text-align: left; height: 37px;">
                            <asp:DropDownList ID="ddlMagzines" runat="server" >
                            </asp:DropDownList></td>
                        <td style="text-align: right; height: 37px;">
                            <asp:Label ID="lblName" runat="server" Text="Name Of Organization" Width="138px"></asp:Label></td>
                        <td style="text-align: left; height: 37px;">
                            <asp:TextBox ID="txtOrganization" runat="server"></asp:TextBox>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 89px; text-align: right">
                            <asp:Label ID="lblSubscription" runat="server" Text="Subscription Date" Width="119px"></asp:Label></td>
                        <td style="width: 244px; height: 89px; text-align: left">
                            <asp:TextBox ID="txtSubscriptionDate" runat="server"></asp:TextBox><asp:Image
                                ID="imgSubscriptionDate" runat="server" ImageUrl="~/Images/Calendar.png" /><cc1:CalendarExtender
                                    ID="ceSubscriptionDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgSubscriptionDate"
                                    TargetControlID="txtSubscriptionDate">
                                </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeSubscriptionDate" runat="server" DisplayMoney="Left"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtSubscriptionDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                        <td style="height: 89px; text-align: right">
                            <asp:Label ID="lblEventName" runat="server" Text="Event Name"></asp:Label></td>
                        <td style="height: 89px; text-align: left">
                            <asp:TextBox ID="txtEventName" runat="server">
                </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblOnDate" runat="server" Text="Event On Date"></asp:Label></td>
                        <td style="width: 244px; text-align: left">
                            <asp:TextBox ID="txtOnDate" runat="server"></asp:TextBox><asp:Image
                                ID="imgOnDate" runat="server" ImageUrl="~/Images/Calendar.png" /><cc1:CalendarExtender
                                    ID="ceOnDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgOnDate" TargetControlID="txtOnDate">
                                </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeOnDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtOnDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="lblTillDate" runat="server" Text="Event Till Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtTillDate" runat="server"></asp:TextBox><asp:Image
                                ID="imgTillDate" runat="server" ImageUrl="~/Images/Calendar.png" /><cc1:CalendarExtender
                                    ID="ceTillDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgTillDate"
                                    TargetControlID="txtTillDate">
                                </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeTillDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtTillDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="lblSponsor" runat="server" Text="Sponsorship Mode"></asp:Label></td>
                        <td style="width: 244px; height: 19px; text-align: left">
                            &nbsp;<asp:TextBox ID="txtSponsorship" runat="server"></asp:TextBox></td>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="lblSponsorshipDate" runat="server" Text="Sponsorship Date"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:TextBox ID="txtSponsorDate" runat="server">
                </asp:TextBox><asp:Image ID="imgSponsorDate" runat="server" ImageUrl="~/Images/Calendar.png" /><cc1:CalendarExtender
                                ID="ceSponsorDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgSponsorDate"
                                TargetControlID="txtSponsorDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeSponsorDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtSponsorDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="lblAdd" runat="server" Text="Advertisement"></asp:Label></td>
                        <td style="width: 244px; height: 19px; text-align: left">
                            <asp:RadioButton ID="rbApproved" runat="server" GroupName="Advertisement" Text="Approved" AutoPostBack="True" OnCheckedChanged="rbApproved_CheckedChanged" /><asp:RadioButton
                                ID="rbNotApproved" runat="server" GroupName="Advertisement" Text="Not Approved" AutoPostBack="True" OnCheckedChanged="rbNotApproved_CheckedChanged" /></td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td id="tblApprovedDetails" colspan="4" rowspan="2" style="text-align: center">
                            <table id="tblApproved" runat="server" border="0" cellpadding="0" cellspacing="0" visible="false">
                                <tr>
                                    <td class="profilehead" colspan="4" style="height: 20px; text-align: left">
                                        Approved Details</td>
                                </tr>
                                <tr>
                                    <td style="width: 190px; height: 19px; text-align: right">
                                    </td>
                                    <td style="height: 19px; text-align: left">
                                    </td>
                                    <td style="height: 19px; text-align: right">
                                    </td>
                                    <td style="height: 19px; text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 190px; text-align: right">
                                        <asp:Label ID="lblApprovedDate" runat="server" Text="Advertising Approved  Date" Width="172px"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtApprovedDate" runat="server"></asp:TextBox><asp:Image
                                            ID="imgApprovedDate" runat="server" ImageUrl="~/Images/Calendar.png" /><cc1:CalendarExtender
                                                ID="ceApprovedDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgApprovedDate"
                                                TargetControlID="txtApprovedDate">
                                            </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="meeApprovedDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                            MaskType="Date" TargetControlID="txtApprovedDate" UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblSize" runat="server" Text="Size Of Advertisement"
                                            Width="139px"></asp:Label></td>
                                    <td style="text-align: left">
                                        &nbsp;<asp:DropDownList ID="ddlSizeOfAdvertisement" runat="server">
                                        </asp:DropDownList>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 190px; text-align: right">
                                        <asp:Label ID="lblPublish" runat="server" Text="Advertisement Publishing Date"
                                            Width="186px"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtPublishDate" runat="server"></asp:TextBox>&nbsp;<asp:Image
                                            ID="imgPublishDate" runat="server" ImageUrl="~/Images/Calendar.png" /><cc1:CalendarExtender
                                                ID="cePublishDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgPublishDate"
                                                TargetControlID="txtPublishDate">
                                            </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="meePublishDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                            MaskType="Date" TargetControlID="txtPublishDate" UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
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
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="width: 244px; text-align: left">
                        </td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: right; height: 227px;" id="tblInvoice" rowspan="2">
                            <table id="tblInvoiceDetails" runat="server" border="0" cellpadding="0" cellspacing="0"
                                visible="false">
                                <tr>
                                    <td class="profilehead" colspan="4" style="height: 20px; text-align: left">
                                        Invoice Details</td>
                                </tr>
                                <tr>
                                    <td style="width: 190px; height: 19px; text-align: right">
                                    </td>
                                    <td style="width: 244px; height: 19px; text-align: left">
                                    </td>
                                    <td style="height: 19px; text-align: right">
                                    </td>
                                    <td style="height: 19px; text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 190px; text-align: right">
                                        <asp:Label ID="lblInvoiceNo" runat="server" Text="Invoice No" Width="81px"></asp:Label></td>
                                    <td style="width: 244px; text-align: left">
                                        <asp:TextBox ID="txtInvoiceNo" runat="server"></asp:TextBox></td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblInvoiceDate" runat="server" Text="Invoice Date"
                                            Width="139px"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtInvoiceDate" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imgInvoiceDate" runat="server" ImageUrl="~/Images/Calendar.png" /><cc1:CalendarExtender ID="ceInvoiceDate" runat="server" Format="dd/MM/yyyy"
                                                PopupButtonID="imgInvoiceDate" TargetControlID="txtInvoiceDate">
                                            </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="meeInvoiceDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                            MaskType="Date" TargetControlID="txtInvoiceDate" UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 190px; text-align: right">
                                        <asp:Label ID="lblInvoiceAmount" runat="server" Text="Invoice Amount"
                                            Width="186px"></asp:Label></td>
                                    <td style="width: 244px; text-align: left">
                                        <asp:TextBox ID="txtInvoiceAmount" runat="server"></asp:TextBox>
                                        &nbsp;
                                    </td>
                                    <td style="text-align: right">
                                    </td>
                                    <td style="text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 190px; text-align: right">
                                    </td>
                                    <td style="width: 244px; text-align: left">
                                    </td>
                                    <td style="text-align: right">
                                    </td>
                                    <td style="text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 190px; text-align: right">
                                    </td>
                                    <td colspan="2" style="text-align: center">
                                        <asp:Button ID="btnAdvance" runat="server" Text="Advance" OnClick="btnAdvance_Click" /><asp:Button ID="btnFullPayment"
                                            runat="server" Text="Full Payment" Width="89px" OnClick="btnFullPayment_Click" /></td>
                                    <td style="text-align: left">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="width: 244px; text-align: left">
                        </td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <table id="tblAdvanceDetails" runat="server" border="0" cellpadding="0" cellspacing="0"
                                visible="false">
                                <tr>
                                    <td class="profilehead" colspan="4" style="height: 20px; text-align: left">
                                        Advance Details</td>
                                </tr>
                                <tr>
                                    <td style="height: 19px; text-align: right">
                                    </td>
                                    <td style="width: 244px; height: 19px; text-align: left">
                                    </td>
                                    <td style="height: 19px; text-align: right">
                                    </td>
                                    <td style="height: 19px; text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblPaymentMode" runat="server" Text="Payment Mode" Width="108px"></asp:Label></td>
                                    <td style="width: 244px; text-align: left">
                                        <asp:DropDownList ID="ddlPaymentMode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged">
                                            <asp:ListItem>--</asp:ListItem>
                                            <asp:ListItem>Cash</asp:ListItem>
                                            <asp:ListItem>Cheque</asp:ListItem>
                                            <asp:ListItem>DD</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="text-align: right">
                                    </td>
                                    <td style="text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblAdvance" runat="server" Text="Advance"></asp:Label></td>
                                    <td style="width: 244px; text-align: left">
                                        <asp:TextBox ID="txtAdvance" runat="server">
                            </asp:TextBox></td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblPayment" runat="server" Text="PaymentDate"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtPaymentDate" runat="server"></asp:TextBox><asp:Image ID="imgPaymentDate" runat="server" ImageUrl="~/Images/Calendar.png" /><cc1:CalendarExtender ID="cePaymentDate" runat="server" Format="dd/MM/yyyy"
                                                PopupButtonID="imgPaymentDate" TargetControlID="txtPaymentDate">
                                            </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="meePaymentDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                            MaskType="Date" TargetControlID="txtPaymentDate" UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                </tr>
                            </table>
                            &nbsp; &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="width: 244px; text-align: left">
                        </td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <table id="tblChekDetails" runat="server" border="0" cellpadding="0" cellspacing="0" visible="false">
                                <tr>
                                    <td class="profilehead" colspan="4" style="height: 20px; text-align: left">
                                        Cheque Details</td>
                                </tr>
                                <tr>
                                    <td style="height: 19px; text-align: right">
                                    </td>
                                    <td style="width: 244px; height: 19px; text-align: left">
                                    </td>
                                    <td style="height: 19px; text-align: right">
                                    </td>
                                    <td style="height: 19px; text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; height: 89px;">
                                        <asp:Label ID="lblDDChequeNo" runat="server" Text="DD/Cheque No"
                                            Width="114px"></asp:Label></td>
                                    <td style="width: 244px; text-align: left; height: 89px;">
                                        <asp:TextBox ID="txtDDChequeNo" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="ftxteDDChequeNo" runat="server" TargetControlID="txtDDChequeNo"
                                            ValidChars=".0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                    <td style="text-align: right; height: 89px;">
                                        <asp:Label ID="lblDDChequeDate" runat="server" Text="DD/Cheque Date"
                                            Width="123px"></asp:Label></td>
                                    <td style="text-align: left; height: 89px;">
                                        <asp:TextBox ID="txtDDChequeDate" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imgDDChequeDate" runat="server" ImageUrl="~/Images/Calendar.png" /><cc1:CalendarExtender ID="ceDDChequeDate" runat="server" Format="dd/MM/yyyy"
                                                PopupButtonID="imgDDChequeDate" TargetControlID="txtDDChequeDate">
                                            </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="meeDDChequeDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                            MaskType="Date" TargetControlID="txtDDChequeDate" UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblBankDetails" runat="server" Text="Bank Details"
                                            Width="101px"></asp:Label></td>
                                    <td colspan="3" style="text-align: left">
                                        <asp:TextBox ID="txtBankDetails" runat="server" EnableTheming="False" TextMode="MultiLine" Width="491px"></asp:TextBox></td>
                                </tr>
                            </table>
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="width: 244px; text-align: left">
                        </td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <table id="tblPaymentDetails" runat="server" border="0" cellpadding="0" cellspacing="0"
                                visible="false">
                                <tr>
                                    <td class="profilehead" colspan="4" style="height: 20px; text-align: left">
                                        Pyment &nbsp;Details</td>
                                </tr>
                                <tr>
                                    <td style="height: 19px; text-align: right">
                                    </td>
                                    <td style="width: 244px; height: 19px; text-align: left">
                                    </td>
                                    <td style="height: 19px; text-align: right">
                                    </td>
                                    <td style="height: 19px; text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblMode" runat="server" Text="Payment Mode"></asp:Label></td>
                                    <td style="width: 244px; text-align: left">
                                        <asp:DropDownList ID="ddlModeOfPayment" runat="server" OnSelectedIndexChanged="ddlModeOfPayment_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem>--</asp:ListItem>
                                            <asp:ListItem>Cash</asp:ListItem>
                                            <asp:ListItem>Cheque</asp:ListItem>
                                            <asp:ListItem>DD</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblBalance" runat="server" Text="Balance Amount"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtBalance" runat="server">
                            </asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblPaid" runat="server" Text="Amount Paid"></asp:Label></td>
                                    <td style="width: 244px; text-align: left">
                                        <asp:TextBox ID="txtAmountPaid" runat="server">
                            </asp:TextBox></td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblDateOfPayment" runat="server" Text="Payment Date"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtDateOfPayment" runat="server">
                            </asp:TextBox><asp:Image ID="imgDateOfPayment" runat="server" ImageUrl="~/Images/Calendar.png" /><cc1:CalendarExtender ID="ceeDateOfPayment" runat="server" Format="dd/MM/yyyy"
                                                PopupButtonID="imgDateOfPayment" TargetControlID="txtDateOfPayment">
                                            </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="meeDateOfPayment" runat="server" DisplayMoney="Left"
                                            Mask="99/99/9999" MaskType="Date" TargetControlID="txtDateOfPayment" UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td colspan="3" style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <table id="tblCheque" runat="server" border="0" cellpadding="0" cellspacing="0" visible="false">
                                <tr>
                                    <td class="profilehead" colspan="4" style="height: 20px; text-align: left">
                                        Cheque Details</td>
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
                                    <td style="text-align: right">
                                        <asp:Label ID="lblDD" runat="server" Text="DD/Cheque No" Width="114px"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtDDNO" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="ftxteDDNO" runat="server" TargetControlID="txtDDNO"
                                            ValidChars=".0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblDDDate" runat="server" Text="DD/Cheque Date" Width="123px"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtDDDate" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imgDDDate" runat="server" ImageUrl="~/Images/Calendar.png" /><cc1:CalendarExtender ID="ceDDDate" runat="server" Format="dd/MM/yyyy"
                                                PopupButtonID="imgDDDate" TargetControlID="txtDDDate">
                                            </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="meeDDDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                            MaskType="Date" TargetControlID="txtDDDate" UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; height: 38px;">
                                        <asp:Label ID="lblDetails" runat="server" Text="Bank Details" Width="101px"></asp:Label></td>
                                    <td colspan="3" style="text-align: left; height: 38px;">
                                        <asp:TextBox ID="txtDetailsOfBank" runat="server" EnableTheming="False" TextMode="MultiLine" Width="491px"></asp:TextBox></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" rowspan="2" style="text-align: right">
                        </td>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Reference Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="width: 244px; height: 21px; text-align: left">
                        </td>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="width: 244px; text-align: left">
                            <asp:DropDownList ID="ddlPreparedBy" runat="server">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By" Width="96px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlApprovedBy" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="width: 244px; text-align: left">
                        </td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="width: 244px">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <br />
                            <table id="tblButtons">
                                <tr>
                                    <td style="width: 49px">
                                        <asp:Button ID="btnInvoice" runat="server" Text="Invoice Details" Width="93px" OnClick="btnInvoice_Click" /></td>
                                    <td style="width: 3px">
                                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" /></td>
                                    <td style="width: 4px">
                                        <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" Text="Refresh" /></td>
                                    <td style="width: 3px">
                                        <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="height: 21px">
            </td>
            <td style="height: 21px;">
            </td>
        </tr>
    </table>