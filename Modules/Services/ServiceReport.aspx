<%@ Page Language="C#" MasterPageFile="~/MPs/ServicesMP1.master" AutoEventWireup="true"
    CodeFile="ServiceReport.aspx.cs" Inherits="Modules_Services_ServiceReport" Title="|| Value Appp : Services : Service Report ||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td style="text-align: left">Service Report</td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="4" style="text-align: left" class="searchhead">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">Service Report</td>
                        <td></td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td style="height: 25px">
                                        <asp:Label ID="Label12" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td style="height: 25px; width: 115px;">
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="SR_NO">SR No</asp:ListItem>
                                            <asp:ListItem Value="SR_DATE">SR Date</asp:ListItem>
                                            <asp:ListItem Value="SR_SERVICE_TYPE">Call Type</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME">Customer</asp:ListItem>
                                            <asp:ListItem Value="CUST_CONTACT_PERSON">Contact Person</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList ID="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                            EnableTheming="False" Visible="False" Width="50px" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged">
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
                                        <asp:TextBox ID="txtSearchValueFromDate" runat="server" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox><asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender Format="MM/dd/yyyy" ID="ceSearchFrom" runat="server" Enabled="False" PopupButtonID="imgFromDate"
                                            TargetControlID="txtSearchValueFromDate">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="meeSearchFromDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender Format="MM/dd/yyyy" ID="ceSearchValueToDate" runat="server" Enabled="False" PopupButtonID="imgToDate"
                                            TargetControlID="txtSearchText">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="meeSearchToDate" runat="server" DisplayMoney="Left" Enabled="False"
                                            Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText" UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnSearchGo_Click" /></td>
                                </tr>
                            </table>
                            <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label>
                            <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label><asp:Label
                                ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 19px; text-align: center">
                <asp:GridView ID="gvServiceReport" runat="server" AllowPaging="True" AutoGenerateColumns="False" AllowSorting="True"
                    DataSourceID="sdsServiceReport" SelectedRowStyle-BackColor="#c0c0c0" OnRowDataBound="gvServiceReport_RowDataBound" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="SR_ID" HeaderText="ServiceReportIdHidden"></asp:BoundField>
                        <asp:TemplateField HeaderText="SR No">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnSRNo" OnClick="lbtnSRNo_Click" runat="server" ForeColor="#0066ff" Text="<%# BIND('SR_NO') %>" CausesValidation="False"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CR_NO" HeaderText="CR No" SortExpression="CR_NO"></asp:BoundField>

                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True" DataField="SR_DATE" HeaderText="SR Date"></asp:BoundField>
                        <asp:BoundField DataField="CR_CALL_TYPE" HeaderText="Call Type">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CUST_NAME" SortExpression="CUST_NAME" HeaderText="Customer">
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
                        <asp:BoundField DataField="SR_STATUS" HeaderText="Status">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CUST_CONTACT_PERSON" HeaderText="Customer Unit Contact Person"></asp:BoundField>
                        <asp:BoundField DataField="SR_NO" HeaderText="SR No 2"></asp:BoundField>
                    </Columns>
                    <SelectedRowStyle BackColor="LightSteelBlue" />
                </asp:GridView>
                <asp:SqlDataSource ID="sdsServiceReport" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SERVICES_SERVICE_REPORT_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="EMPID" ControlID="lblEmpIdHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 19px"></td>
        </tr>
        <tr>
            <td colspan="4">
                <table id="Table1" align="center">
                    <tr>
                        <td>
                            <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CausesValidation="False" /></td>
                        <td>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CausesValidation="False" /></td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"
                                CausesValidation="False" /></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 19px"></td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="lblMessage" runat="server" EnableTheming="False" Font-Bold="True"
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="Red"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="4">
                <table border="0" cellpadding="0" cellspacing="0" id="tblServiceReport" runat="server"
                    visible="False" width="100%">
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
                            <asp:TextBox ID="txtSRDate1" runat="server" CssClass="datetext" EnableTheming="False"
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
                            <asp:TextBox ID="txtCRDate" runat="server" CssClass="datetext" EnableTheming="False">
                            </asp:TextBox><asp:Image
                                ID="imgCRDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>&nbsp;
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
                        <td class="profilehead" colspan="4" style="text-align: left; height: 3px;">customer details</td>
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
                            <asp:TextBox ID="txtCustName" runat="server" ReadOnly="True" Visible="False"></asp:TextBox><asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomerName_SelectedIndexChanged" Enabled="False" Visible="False">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblInitName" runat="server" Text="Unit Name" Width="74px" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtUnitName" runat="server" ReadOnly="True" Visible="False"></asp:TextBox><asp:DropDownList ID="ddlUnitName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged" Enabled="False" Visible="False">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="0">--Select Customer--</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlContactPerson" runat="server">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="0">--Select Unit Name--</asp:ListItem>
                            </asp:DropDownList><asp:TextBox ID="txtContactPerson" runat="server" ReadOnly="True" Visible="False"></asp:TextBox></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label6" runat="server" Text="Customer Unit Address"></asp:Label></td>
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
                        <td class="profilehead" colspan="4" style="text-align: left">service details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: center" colspan="4">
                            <asp:GridView ID="gvQuotationItems" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvQuotationItems_RowDataBound"
                                OnRowDeleting="gvQuotationItems_RowDeleting" OnRowEditing="gvQuotationItems_RowEditing">
                                <Columns>
                                    <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ItemType" HeaderText="SubCategory">
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
                                <EmptyDataTemplate>
                                    <span style="color: #ff0000">No Data Found</span>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
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
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label14" runat="server" Text="Model No" Width="76px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblItemName" runat="server" Text="Item Name" Width="76px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtModelName" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label32" runat="server" Text="Item Category :"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtItemCategory" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label33" runat="server" Text="Item SubCategory :"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtItemSubCategory" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label19" runat="server" Text="Color :"></asp:Label></td>
                        <td style="text-align: left">&nbsp;<asp:DropDownList ID="ddlColor" runat="server">
                        </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label55" runat="server" Text="Brand :"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtBrand" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label9" runat="server" Text="Item SL No" Width="88px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtItemSLNo" runat="server"></asp:TextBox></td>
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
                            <asp:TextBox ID="txtSRDate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox><asp:Image
                                ID="imgSRDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender
                                    ID="ceSRDate" runat="server" PopupButtonID="imgSRDate" TargetControlID="txtSRDate" Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
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
                            <asp:Label ID="lblVisitDate" runat="server" Text="AMC Visit Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtAMCVisitDate" runat="server" CssClass="datetext" EnableTheming="False">
                            </asp:TextBox><asp:Image
                                ID="imgAMCVisitDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender
                                    ID="ceAMCVisitDate" runat="server" PopupButtonID="imgAMCVisitDate" TargetControlID="txtAMCVisitDate" Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeAMCVisitDate" runat="server" DisplayMoney="Left"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtAMCVisitDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label1" runat="server" Text="Date of Completion"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCompletionDate" runat="server" CssClass="datetext" EnableTheming="False">
                            </asp:TextBox><asp:Image
                                ID="imgCompletionDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender
                                    ID="ceCompletionDate" runat="server" PopupButtonID="imgCompletionDate" TargetControlID="txtCompletionDate" Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
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
                                OnRowDeleting="gvServiceItems_RowDeleting" OnRowEditing="gvServiceItems_RowEditing">
                                <Columns>
                                    <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                    <asp:BoundField DataField="ItemType" HeaderText="Model No">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name">
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
                                    No Items Details have been Prepared
                                
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
                            <table id="tblButtons" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="height: 24px">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CausesValidation="False" /></td>
                                    <td style="height: 24px"></td>
                                    <td style="height: 24px">
                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click"
                                            CausesValidation="False" /></td>
                                    <td style="height: 24px">
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" CausesValidation="False" Width="60px" Visible="False" /></td>
                                    <td style="height: 24px">
                                        <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" Width="57px" /></td>
                                    <td style="height: 24px">
                                        <asp:Button ID="btnCr" runat="server" Text="Complaint Register" OnClick="btnCr_Click"
                                            CausesValidation="False" Width="119px" Visible="False" /></td>
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
                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" DisplayMoney="Left"
                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtSRDate1" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
                <cc1:CalendarExtender
                    ID="ceCRDate" runat="server" PopupButtonID="imgCRDate" TargetControlID="txtCRDate" Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="meeCRDate" runat="server" DisplayMoney="Left"
                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtCRDate" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
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

</asp:Content>


 
