<%@ Page Language="C#" MasterPageFile="~/MPs/SalesMP1.master" AutoEventWireup="true" CodeFile="SalesAssignments.aspx.cs" Inherits="Modules_SM_SalesAssignments" Title="|| Vale App : S&M : Sales Assignments ||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .profilehead {
            text-align: left;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>

    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td style="text-align: left">Sales Assignments</td>
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
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="searchhead">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left" colspan="2">Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                            <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
                        </td>

                        <td style="text-align: right;">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="ENQ_NO">Enquiry No</asp:ListItem>
                                            <asp:ListItem Value="ENQ_DATE">Enquiry Date</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME">Customer Name</asp:ListItem>
                                            <asp:ListItem Value="ASSIGN_DATE">Assign Date</asp:ListItem>
                                            <asp:ListItem Value="DUE_DATE">Due Date</asp:ListItem>
                                            <%--<asp:ListItem Value="EMP_FIRST_NAME">Assigned To</asp:ListItem>--%>
                                            <asp:ListItem Value="ASSIGN_STATUS">Status</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td>
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
                                    <td>
                                        <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtSearchValueFromDate" runat="server" type="date" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox>
                                        <%-- <asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False" />
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchFrom" runat="server" Enabled="False" PopupButtonID="imgFromDate"
                                            TargetControlID="txtSearchValueFromDate">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>--%>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtSearchValueToDate" runat="server" type="date" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><%--<asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False" />
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchValueToDate" runat="server" Enabled="False" PopupButtonID="imgToDate"
                                            TargetControlID="txtSearchText">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>--%>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                                </tr>
                            </table>
                            <%-- <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label>--%>
                            <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label>
                            <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False">
                            </asp:Label>
                            <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvEnqAssignDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="sdsEnqAssignDetails" OnRowDataBound="gvEnqAssignDetails_RowDataBound"
                    Width="100%" AllowSorting="True" SelectedRowStyle-BackColor="#c0c0c0">
                    <Columns>
                        <asp:BoundField DataField="ASSIGN_TASK_ID" SortExpression="ASSIGN_TASK_ID" HeaderText="AssignTaskIdHidden"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" DataField="ENQ_ID" SortExpression="ENQ_ID" HeaderText="EnqIdHidden"></asp:BoundField>
                        <asp:TemplateField SortExpression="ENQ_NO" HeaderText="Enquiry No">
                            <EditItemTemplate>
                                &nbsp; 
                            </EditItemTemplate>

                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnEnquiryNo" OnClick="lbtnEnquiryNo_Click" ForeColor="#0066ff" runat="server" Text='<%# Eval("ENQ_NO") %>' CausesValidation="False"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="ENQ_DATE" SortExpression="ENQ_DATE" HeaderText="Enq Date">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CUST_NAME" SortExpression="CUST_NAME" HeaderText="Customer Name">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="ASSIGN_DATE" SortExpression="ASSIGN_DATE" HeaderText="Assign Date"></asp:BoundField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="DUE_DATE" SortExpression="DUE_DATE" HeaderText="Due Date"></asp:BoundField>
                        <%-- <asp:BoundField NullDisplayText="EDP Department"  HeaderText="Assigned To">
                            <ItemStyle  HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>--%>
                        <asp:TemplateField HeaderText="Assigned To">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text="EDP Department"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ASSIGN_STATUS" SortExpression="ASSIGN_STATUS" HeaderText="Status">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        No Data Exist!
                    </EmptyDataTemplate>
                </asp:GridView>
                <asp:SqlDataSource ID="sdsEnqAssignDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SM_SALESASSIGNMENTS_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
                        <%--<asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="CpId" ControlID="lblCPID"></asp:ControlParameter>--%>
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="USERTYPE" ControlID="lblUserType"></asp:ControlParameter>
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table3">
                    <tr>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" CausesValidation="False" OnClick="btnDelete_Click"
                                Text="Delete" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 6px">
                <table id="tblAssignmentDetails" runat="server" visible="false" border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="profilehead" colspan="4">Sales Assignments Details</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 18px;">Enquiry No

                        </td>
                        <td style="text-align: left; height: 18px;">
                            <asp:TextBox ID="txtEnquiryNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right; height: 18px;">Enquiry Date

                        </td>
                        <td style="text-align: left; height: 18px;">
                            <asp:TextBox ID="txtEnquiryDate" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Customer Name

                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCustomerName" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">Assigned To

                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtAssignedTo" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Assigned Date

                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtAssignedDate" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">Due Date

                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDueDate" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Status

                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtStatus" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td></td>
                        <td></td>
                    </tr>

                    <tr>
                        <td style="text-align: right">Remarks

                        </td>
                        <td style="text-align: left" colspan="3">
                            <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Width="400px" runat="server" ReadOnly="True"></asp:TextBox></td>

                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>
            <td style="height: 49px">
                <table id="Table1" align="center">
                    <tr>
                        <td>&nbsp;<asp:Button ID="btnFollowUp" runat="server" CausesValidation="False" Text="Follow Up" OnClick="btnFollowUp_Click" Visible="False" /></td>
                        <td>
                            <asp:Button ID="btnSendQuotation" runat="server" CausesValidation="False" Text="Set Quotation" OnClick="btnSendQuotation_Click" Visible="False" /></td>
                        <td>
                            <asp:Button ID="btnViewEnquiry" runat="server" CausesValidation="False" Text="View Enquiry" OnClick="btnViewEnquiry_Click" Visible="False" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 34px">
                <table id="tblSalesEnquiry" runat="server" border="0" cellpadding="0" cellspacing="0"
                    visible="false" width="100%">
                    <tr>
                        <td id="TD10" class="profilehead" colspan="4" style="text-align: left">Enquiry Details</td>
                    </tr>
                    <tr>
                        <td id="TD27" style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td id="TD14" style="text-align: right">Enquiry No

                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtSalesEnqNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">Enquiry Date

                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtSalesEnqDate" runat="server" CssClass="datetext" EnableTheming="False"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td id="TD23" style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right">Enquiry Source

                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlEnquirySource" runat="server" AutoPostBack="True" Enabled="False"
                                OnSelectedIndexChanged="ddlEnquirySource_SelectedIndexChanged">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Employee Name

                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlOriginatedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblReferenceCode" runat="server" Text="Reference Code" Width="108px"></asp:Label>

                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtReferenceCode" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td id="tdTenderDate1" runat="server" style="text-align: right" visible="false">Tender Date

                        </td>
                        <td id="tdTenderDate2" runat="server" style="text-align: left" visible="false">
                            <asp:TextBox ID="txtTenderDate" runat="server" CssClass="datetext" EnableTheming="False"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="lblEnquiryDueDate" runat="server" Text="Enquiry Due Date"></asp:Label>


                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtEnquiryDueDate" runat="server" CssClass="datetext" EnableTheming="False"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td id="tdOpeningDate1" runat="server" style="text-align: right" visible="false">Opening Date
                        </td>
                        <td id="tdOpeningDate2" runat="server" style="text-align: left" visible="false">
                            <asp:TextBox ID="txtOpeningDate" runat="server" CssClass="datetext" EnableTheming="False"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                        <td id="tdOpeningTime1" runat="server" style="text-align: right" visible="false">Opening Time
                        </td>
                        <td id="tdOpeningTime2" runat="server" style="text-align: left" visible="false">
                            <asp:TextBox ID="txtOpeningTime" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td id="tdSubmissionTime2" runat="server" style="text-align: right" visible="false">Submission Time
                        </td>
                        <td id="tdSubmissionTime1" runat="server" style="text-align: left" visible="false">
                            <asp:TextBox ID="txtSubmissionTime" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td id="Td1" runat="server" style="text-align: right" visible="false"></td>
                        <td id="Td2" runat="server" style="text-align: left" visible="false"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblPromotionType" runat="server" Text="Promotion Type" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPromotionType" runat="server" ReadOnly="True" Visible="False"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblPromotionActivity" runat="server" Text="Promotion Activity" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPromotionActivity" runat="server" ReadOnly="True" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblCriteria" runat="server" Text="Follow Up Criteria" Visible="False"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtFollowUpCriteria" runat="server" CssClass="multilinetext" EnableTheming="False"
                                ReadOnly="True" TextMode="MultiLine" Visible="False" Width="89%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td id="TD11" style="text-align: right">Description
                        </td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtDescription" runat="server" CssClass="multilinetext" EnableTheming="False"
                                ReadOnly="True" TextMode="MultiLine" Width="89%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td id="TD15" style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td id="TD16" class="profilehead" colspan="4" style="text-align: left">Customer Details</td>
                    </tr>
                    <tr>
                        <td id="TD5" style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td id="TD18" style="height: 22px; text-align: right">Customer
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="True" Enabled="False"
                                OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="height: 22px; text-align: right">Region
                        </td>
                        <td style="height: 22px; text-align: left">
                            <asp:TextBox ID="txtRegion" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 22px; text-align: right">Industry Type

                        </td>
                        <td style="height: 22px; text-align: left">
                            <asp:TextBox ID="txtIndustryType" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="height: 22px; text-align: right">Unit Name

                        </td>
                        <td style="height: 22px; text-align: left">
                            <asp:DropDownList ID="ddlUnitName" runat="server" AutoPostBack="True" Enabled="False"
                                OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="0">--Select Customer--</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblUnitAddress" runat="server" Text="Unit Address" Width="106px"></asp:Label>

                        </td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtUnitAddress" runat="server" CssClass="multilinetext" EnableTheming="False"
                                Font-Names="Verdana" Font-Size="8pt" ReadOnly="True" TextMode="MultiLine" Width="569px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Contact Person

                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtContactPerson" runat="server" ReadOnly="True"></asp:TextBox><asp:DropDownList
                                ID="ddlContactPerson" runat="server" AutoPostBack="True" Enabled="False" OnSelectedIndexChanged="ddlContactPerson_SelectedIndexChanged"
                                Visible="False">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="0">--Select Unit Name--</asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="text-align: right">Email

                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td id="TD28" style="text-align: right">Phone No
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPhoneNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">Mobile
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td id="TD19" class="profilehead" colspan="4" style="height: 20px; text-align: left">Interested Product</td>
                    </tr>
                    <tr>
                        <td id="TD12" runat="server" colspan="4" style="text-align: center">
                            <asp:GridView ID="gvInterestedProducts" runat="server" AutoGenerateColumns="False"
                                OnRowDataBound="gvInterestedProducts_RowDataBound" Width="100%">
                                <Columns>
                                    <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ModelNo" HeaderText="Model No">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Brand" HeaderText="Brand">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Specifications" NullDisplayText="-" HeaderText="Specifications"></asp:BoundField>
                                    <asp:BoundField DataField="Remarks" NullDisplayText="-" HeaderText="Remarks"></asp:BoundField>
                                    <asp:BoundField DataField="Priority" HeaderText="Priority"></asp:BoundField>
                                    <asp:BoundField DataField="DocCharges" NullDisplayText="-" HeaderText="Doc Charges"></asp:BoundField>
                                    <asp:BoundField DataField="DocInFavourOf" NullDisplayText="-" HeaderText="Doc In Favour Of"></asp:BoundField>
                                    <asp:BoundField DataField="EMDCharges" NullDisplayText="-" HeaderText="EMD Charges"></asp:BoundField>
                                    <asp:BoundField DataField="EMDInFavourOf" NullDisplayText="-" HeaderText="EMD In Favour Of"></asp:BoundField>
                                    <asp:BoundField DataField="Room" HeaderText="Room"></asp:BoundField>
                                    <asp:BoundField DataField="Color" HeaderText="Color "></asp:BoundField>
                                    <asp:BoundField DataField="ColorId" HeaderText="ColorId"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="tblFollowUp" runat="server" border="0" cellpadding="0" cellspacing="0"
                    visible="false" width="100%">
                    <tr>
                        <td class="profilehead" colspan="2">follow up details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Name

                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlFollowEmpName" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Follow Up Description
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtFollowUpDesc" runat="server" CssClass="multilinetext" EnableTheming="False"
                                Height="40px" TextMode="MultiLine" Width="560px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table id="Table2" align="center">
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
                    <tr>
                        <td colspan="2">
                            <table id="tblFollowUpHistory" runat="server" border="0" cellpadding="0" cellspacing="0"
                                width="100%" visible="false">
                                <tr>
                                    <td class="profilehead" colspan="3">Follow Up History</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvFollowUp" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            DataSourceID="sdsFollowUp" Width="100%">
                                            <Columns>
                                                <asp:BoundField DataField="FU_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date"
                                                    HtmlEncode="False" SortExpression="FU_DATE" />
                                                <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Name" SortExpression="EMP_FIRST_NAME">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FU_DESC" HeaderText="Description" SortExpression="FU_DESC">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                            </Columns>
                                            <SelectedRowStyle BackColor="LightSteelBlue" />
                                        </asp:GridView>
                                        <asp:Label ID="lblAssignTaskIdHiddenForFollowUp" runat="server" Visible="False"></asp:Label>
                                        <br />
                                        <asp:SqlDataSource ID="sdsFollowUp" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                            SelectCommand="SELECT   [YANTRA_ENQ_ASSIGN_FOLLOWUP_DET].[FU_DATE],[YANTRA_ENQ_ASSIGN_FOLLOWUP_DET].[FU_DESC],[YANTRA_EMPLOYEE_MAST].[EMP_FIRST_NAME] FROM [YANTRA_EMPLOYEE_MAST],[YANTRA_ENQ_ASSIGN_FOLLOWUP_DET]&#13;&#10;WHERE [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_ENQ_ASSIGN_FOLLOWUP_DET].EMP_ID AND [YANTRA_ENQ_ASSIGN_FOLLOWUP_DET].ASSIGN_TASK_ID=@ASSIGNID&#13;&#10; ORDER BY [YANTRA_ENQ_ASSIGN_FOLLOWUP_DET].[FU_DATE] DESC">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="lblAssignTaskIdHiddenForFollowUp" DefaultValue="0"
                                                    Name="ASSIGNID" PropertyName="Text" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>





 
