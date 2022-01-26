<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/ServicesMP1.master" AutoEventWireup="true" CodeFile="ServiceReportNew.aspx.cs" Inherits="Modules_Services_ServiceReportNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--   <script>
        $(function () {
            $("[name$='txtCRDate'],[name$='txtSRDate'],[name$='txtAMCVisitDate'],[name$='txtCompletionDate']").datepicker();
        });
    </script>--%>
    <style type="text/css">
        .auto-style1 {
            color: #FF0000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
                <tr>
                    <td>Service Report</td>
                    <td>
                        <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                            Visible="False"></asp:Label>
                        <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                            Visible="False"></asp:Label>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td colspan="4">
                        <asp:Label ID="lblMessage" runat="server" EnableTheming="False" Font-Bold="True"
                            Font-Names="Verdana" Font-Size="8pt" ForeColor="Red"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table border="0" cellpadding="0" cellspacing="0" id="tblServiceReport" runat="server"
                            visible="true" width="100%">
                            <tr>
                                <td class="profilehead" colspan="4">General &nbsp;details</td>
                            </tr>
                            <tr>
                                <td style="height: 19px; text-align: right"></td>
                                <td style="height: 19px; text-align: left"></td>
                                <td style="height: 19px; text-align: right">
                                    <asp:Label ID="lblUserName1" runat="server" Text="Label" Visible="False"></asp:Label></td>
                                <td style="height: 19px; text-align: left"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label11" runat="server" Text="Call Type"></asp:Label></td>
                                <td colspan="3" style="text-align: left" valign="middle">
                                    <asp:RadioButtonList ID="rbServiceType" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbServiceType_SelectedIndexChanged" RepeatLayout="Flow">
                                        <asp:ListItem>Installation</asp:ListItem>
                                        <asp:ListItem>Complaint</asp:ListItem>
                                        <asp:ListItem>Technical Guidance</asp:ListItem>
                                        <asp:ListItem>AMC</asp:ListItem>
                                        <asp:ListItem>Non Warranty</asp:ListItem>
                                        <asp:ListItem>Warranty</asp:ListItem>
                                    </asp:RadioButtonList></td>
                            </tr>
                            <tr>
                                <td style="text-align: right"></td>
                                <td colspan="3" style="text-align: left">
                                    <asp:RadioButtonList ID="rbWarrantySelect" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                        Width="300px" OnSelectedIndexChanged="rbWarrantySelect_SelectedIndexChanged" Visible="False" RepeatLayout="Flow">
                                        <asp:ListItem>By Complaint No</asp:ListItem>
                                        <asp:ListItem>By Service Report No</asp:ListItem>
                                    </asp:RadioButtonList></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblSRNo1" runat="server" Text="SR No." Visible="False"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlSRNo" runat="server" AutoPostBack="True" Visible="False" OnSelectedIndexChanged="ddlSRNo_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblAMCAId" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblInsAId" runat="server" Visible="False"></asp:Label></td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblSRDate1" runat="server" Text="SR Date" Visible="False"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtSRDate1" runat="server" CssClass="datetext" type="date" EnableTheming="False"
                                        ReadOnly="True" Visible="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblCRNo" runat="server" Text="CR No"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlCRNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCRNo_SelectedIndexChanged">
                                    </asp:DropDownList></td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblCRDate" runat="server" Text="CR Date"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtCRDate" runat="server" CssClass="datetext" type="date" EnableTheming="False">
                                    </asp:TextBox>

                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label13" runat="server" Text="Call Type"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtCallType" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblOrderNo" runat="server" Text="Order  No" Visible="False"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlOrderNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOrderNo_SelectedIndexChanged" Visible="False">
                                    </asp:DropDownList></td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblOrderDate" runat="server" Text="Order Date" Visible="False"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtOrderDate" runat="server" CssClass="datetext" EnableTheming="False" ReadOnly="True" Visible="False"></asp:TextBox>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td class="profilehead" colspan="4" style="text-align: left; height: 3px;">Customer details</td>
                            </tr>
                            <tr>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label5" runat="server" Text="Customer Name" Visible="False"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomerName_SelectedIndexChanged" Enabled="False" Visible="False">
                                    </asp:DropDownList></td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblInitName" runat="server" Text="Unit Name" Width="74px" Visible="False"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlUnitName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged" Enabled="False" Visible="False">
                                        <asp:ListItem Value="0">--</asp:ListItem>
                                        <asp:ListItem Value="0">--Select Customer--</asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtContactPerson" runat="server" ReadOnly="True"></asp:TextBox></td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblContact" runat="server" Text="Contact No :" Width="74px"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:Label ID="lblCustMobile" runat="server" Visible="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblUnitadd" runat="server" Text="Customer Unit Address"></asp:Label></td>
                                <td colspan="3" style="text-align: left">
                                    <asp:TextBox ID="txtCustUnitAddress" runat="server" CssClass="multilinetext" EnableTheming="False"
                                        TextMode="MultiLine" Width="523px" ReadOnly="True"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 19px;"></td>
                                <td style="text-align: left; height: 19px;">&nbsp;</td>
                                <td style="text-align: right; height: 19px;"></td>
                                <td style="text-align: left; height: 19px;"></td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <table border="0" cellpadding="0" cellspacing="0" id="tblPreviousServiceRecords" runat="server"
                                        visible="true" width="100%">
                                        <tr>
                                            <td class="profilehead" colspan="4" style="text-align: left">Previous Service Records</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <asp:GridView ID="gvPreviousServiceRecords" runat="server" AutoGenerateColumns="False" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="SR_NO" SortExpression="SR_NO" HeaderText="SR No."></asp:BoundField>
                                                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="SR_DATE" SortExpression="SR_DATE" HeaderText="SR Date"></asp:BoundField>
                                                        <asp:BoundField DataField="SR_SERVICE_TYPE" SortExpression="SR_SERVICE_TYPE" HeaderText="Service Type"></asp:BoundField>
                                                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="SR_AMC_VISIT_DATE" SortExpression="SR_AMC_VISIT_DATE" HeaderText="Visit Date"></asp:BoundField>
                                                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="SR_COMPLETION_DATE" SortExpression="SR_COMPLETION_DATE" HeaderText="Completion Date"></asp:BoundField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <span style="color: #ff0033">No Records Found</span>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                                    SelectCommand="SELECT * FROM [YANTRA_SERVICE_REPORT_MAST] WHERE SO_ID=@SOID">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="ddlOrderNo" DefaultValue="0" Name="SOID" PropertyName="SelectedValue" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="profilehead" colspan="4" style="text-align: left">Service details</td>
                            </tr>
                            <tr>
                                <td style="height: 19px; text-align: center" colspan="4">
                                    <asp:GridView ID="gvQuotationItems" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvQuotationItems_RowDataBound" Width="100%">
                                        <Columns>
                                            <asp:CommandField ShowEditButton="True" />
                                            <asp:CommandField ShowDeleteButton="True" />
                                            <asp:BoundField DataField="ItemName" HeaderText="Item Name">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                            <asp:BoundField DataField="NatureofComplaint" HeaderText="NatureofComplaint" />
                                            <asp:BoundField DataField="RootCausedNotice" HeaderText="Customer Feed Back" />
                                            <asp:BoundField DataField="CorrectiveActionTaken" HeaderText="CorrectiveActionTaken" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <span class="auto-style1">No Data Found</span>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr style="visibility: hidden">
                                <td style="text-align: right">
                                    <asp:Label ID="Label20" runat="server" Text="Brand"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
                                    </asp:DropDownList></td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblSearch" runat="server" Text="Search:" Width="84px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtSearchModel" runat="server">
                                    </asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server"
                                        ControlToValidate="txtSearchModel" ErrorMessage="Please Enter ModelNo For Search"
                                        ValidationGroup="Search" Visible="False">*</asp:RequiredFieldValidator><asp:Button
                                            ID="btnSearchModelNo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" OnClick="btnSearchModelNo_Click" Text="Go"
                                            ValidationGroup="Search" />
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                        SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:Parameter Type="Int64" DefaultValue="0" Name="BranbId"></asp:Parameter>
                                            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtSearchModel"></asp:ControlParameter>
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                            <tr style="visibility: hidden">
                                <td style="text-align: right">
                                    <asp:Label ID="Label32" runat="server" Text="Item Category :"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtItemCategory" Text="0" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label33" runat="server" Text="Item SubCategory :"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtItemSubCategory" Text="0" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
                            <tr style="visibility: hidden">
                                <td style="text-align: right">
                                    <asp:Label ID="Label19" runat="server" Text="Color :"></asp:Label></td>
                                <td style="text-align: left">&nbsp;<asp:DropDownList ID="ddlColor" runat="server">
                                </asp:DropDownList></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label55" runat="server" Text="Brand :"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtBrand" Text="0" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label14" runat="server" Text="Brand" Width="76px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlItemType" Visible="false" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtBrandName" runat="server"></asp:TextBox>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblItemName" runat="server" Text="Model No" Width="76px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtModelNo" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="txtModelName" Visible="false" Text="0" runat="server">
                                    </asp:TextBox></td>
                            </tr>

                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label9" runat="server" Text="Item SL No" Visible="false" Width="88px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtItemSLNo" Text="0" runat="server" Visible="false"></asp:TextBox></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label16" runat="server" Text="Quantity" Width="63px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtQuantity" runat="server">
                                    </asp:TextBox><asp:Label ID="Label17" runat="server" EnableTheming="False" ForeColor="Red"
                                        Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                            ControlToValidate="txtQuantity" ErrorMessage="Please Enter the Quantity">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblSRNo" runat="server" Text="SR No" Width="108px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtSRNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblSRDate" runat="server" Text="SR Date" Width="126px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtSRDate" runat="server" type="date" CssClass="datetext" EnableTheming="False"></asp:TextBox>

                                    <cc1:MaskedEditExtender ID="meeSRDate" runat="server" DisplayMoney="Left"
                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtSRDate" UserDateFormat="MonthDayYear">
                                    </cc1:MaskedEditExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label45" runat="server" Text="Service Center" Width="115px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtServiceCenter" runat="server"></asp:TextBox></td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblAMCRefNo" runat="server" Text="AMC Ref No"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtAMCRefNo" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblVisitDate" runat="server" Text="Visit Date"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtAMCVisitDate" runat="server" CssClass="datetext" type="date" EnableTheming="False">
                                    </asp:TextBox>

                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label1" runat="server" Text="Date of Completion"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtCompletionDate" runat="server" type="date" CssClass="datetext" EnableTheming="False">
                                    </asp:TextBox>

                                    <cc1:MaskedEditExtender ID="meeCompletionDate" runat="server" DisplayMoney="Left"
                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtCompletionDate" UserDateFormat="MonthDayYear">
                                    </cc1:MaskedEditExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 39px;">
                                    <asp:Label ID="Label49" runat="server" Text="Description"></asp:Label></td>
                                <td colspan="3" style="text-align: left; height: 39px;">
                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="multilinetext" EnableTheming="False" TextMode="MultiLine" Width="91%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 52px;">
                                    <asp:Label ID="Label2" runat="server" Text="Action Taken"></asp:Label></td>
                                <td colspan="3" style="text-align: left; height: 52px;">
                                    <asp:TextBox ID="txtActionTaken" runat="server" CssClass="multilinetext" EnableTheming="False" TextMode="MultiLine" Width="91%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label3" runat="server" Text="Further Action Required"></asp:Label></td>
                                <td colspan="3" style="text-align: left">
                                    <asp:TextBox ID="txtActionRequired" runat="server" CssClass="multilinetext" EnableTheming="False" TextMode="MultiLine" Width="91%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 52px;">
                                    <asp:Label ID="Label4" runat="server" Text="Customer Feedback"></asp:Label></td>
                                <td colspan="3" style="text-align: left; height: 52px;">
                                    <asp:TextBox ID="txtCustFeedback" runat="server" CssClass="multilinetext" EnableTheming="False" TextMode="MultiLine" Width="91%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label7" runat="server" Text="Is Document Submitted" Width="157px">
                                    </asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlIsDocSubmitted" runat="server">
                                        <asp:ListItem>No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                        <asp:ListItem>NA</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label8" runat="server" Text="Service Completed" Width="128px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtServiceCompleted" runat="server">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label10" runat="server" Text="Attachments"></asp:Label></td>
                                <td style="text-align: left;" colspan="3">&nbsp;<asp:FileUpload ID="FileUpload1" runat="server" Font-Italic="False" Font-Names="Verdana"
                                    Font-Size="8pt" Width="510px" /></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label15" runat="server" Text="Visited By" Width="76px"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlVisitedBy" runat="server">
                                    </asp:DropDownList></td>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label18" runat="server" Text="Status" Width="76px"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlServiceStatus" runat="server">
                                        <asp:ListItem>Open</asp:ListItem>
                                        <asp:ListItem>Close</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtSER_DET_DETID" runat="server" Visible="False"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: right">
                                    <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                        CssClass="loginbutton" EnableTheming="False" OnClick="btnAdd_Click" Text="Add"
                                        ValidationGroup="items" /></td>
                                <td colspan="2" style="text-align: left">
                                    <asp:Button ID="btnItemRefresh" runat="server" BackColor="Transparent" BorderStyle="None"
                                        CausesValidation="False" CssClass="loginbutton" EnableTheming="False" Text="Refresh" /></td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                    <asp:GridView ID="gvServiceItems" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvServiceItems_RowDataBound"
                                        OnRowDeleting="gvServiceItems_RowDeleting" OnRowEditing="gvServiceItems_RowEditing" OnSelectedIndexChanged="gvServiceItems_SelectedIndexChanged" Width="100%">
                                        <Columns>
                                            <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                            <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                            <asp:BoundField DataField="ItemType" HeaderText="Model No">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ItemName" HeaderText="Brand">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Description" NullDisplayText="-" HeaderText="Description"></asp:BoundField>
                                            <asp:BoundField DataField="ActionTaken" HeaderText="Action Taken"></asp:BoundField>
                                            <asp:BoundField DataField="FurtherAction" HeaderText="Further Action"></asp:BoundField>
                                            <asp:BoundField DataField="Status" HeaderText="Status"></asp:BoundField>
                                            <asp:BoundField DataField="ItemTypeId" HeaderText="ItemTypeIdHidden"></asp:BoundField>
                                            <asp:BoundField DataField="SER_DET_ID" HeaderText="SER_DET_ID"></asp:BoundField>
                                            <asp:BoundField DataField="Date Of Comp" HeaderText="Date Of Comp"></asp:BoundField>
                                            <asp:BoundField DataField="CustomerFeedBack" HeaderText="CustomerFeedBack"></asp:BoundField>
                                            <asp:TemplateField HeaderText="Print" ShowHeader="False">
                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" ID="TextBox1"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnPrint" runat="server" CausesValidation="False" OnClick="lbtnPrint_Click">Print</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <span class="auto-style1">No Items Details have been Prepared</span>

                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td class="profilehead" colspan="4" style="text-align: left">Reference Details</td>
                            </tr>
                            <tr>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                                    </asp:DropDownList></td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By" Visible="False"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlApprovedBy" runat="server" Enabled="False" Visible="False">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table id="tblButtons" border="0" cellpadding="0" cellspacing="0" align="center">
                                        <tr>
                                            <td style="height: 24px">
                                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CausesValidation="False" /></td>
                                            <td style="height: 24px"></td>
                                            <td style="height: 24px">
                                                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click"
                                                    CausesValidation="False" /></td>
                                            <td style="height: 24px">
                                                <asp:Button ID="btnPrint" runat="server" Text="Send SMS" OnClick="btnPrint_Click" CausesValidation="False" /></td>
                                            <td style="height: 24px">
                                                <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" /></td>
                                            <td style="height: 24px">
                                                <asp:Button ID="btnCr" runat="server" Text="Complaint Register" OnClick="btnCr_Click"
                                                    CausesValidation="False" Width="119px" Visible="False" /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" style="text-align:center">
                                                <asp:Button ID="btnSMS" runat="server" Text="Send SMS Test" OnClick="btnSMS_Click" Visible="False" />
                                            </td>
                                        </tr>
                                    </table>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="height: 10px">&nbsp;
                                </td>
                            </tr>
                        </table>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False"></asp:ValidationSummary>
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="qi"
                            ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>
                        <cc1:MaskedEditExtender ID="meeOrderDate" runat="server" DisplayMoney="Left"
                            Mask="99/99/9999" MaskType="Date" TargetControlID="txtOrderDate" UserDateFormat="MonthDayYear" Enabled="False">
                        </cc1:MaskedEditExtender>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>

        </ContentTemplate>
    </asp:UpdatePanel>
    <table>
        <tr style="text-align: center">
            <td colspan="5" style="text-align: center">
                <asp:Button ID="btnSendSMS" runat="server" Text="Send SMS" Enabled="False" OnClick="btnSendSMS_Click" Visible="False" />
            </td>
        </tr>
    </table>
</asp:Content>


 
