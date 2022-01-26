<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/ServicesMP1.master" AutoEventWireup="true" CodeFile="AMCQuotationNew.aspx.cs" Inherits="Modules_Services_AMCQuotationNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
       <%-- <script>
            $(function () {
                $("[name$='txtQuotationDate'],[name$='txtCustPODate'],[name$='txtCallDate']").datepicker();
            });
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead" style="Width:100%">
        <tr>
            <td style="text-align:left">
                AMC quotation</td>
            </tr>
        </table>
    <table border="0" cellpadding="0" cellspacing="0" style="Width:100%">
        <tr>
            <td colspan="4">
                &nbsp;<table id="tblFollowUp" runat="server" border="0" cellpadding="0" cellspacing="0"
                    visible="true" width="100%">
                    <tr>
                        <td class="profilehead" colspan="2">Follow Up Details</td>
                            
                   
                        <td class="profilehead" colspan="2">
                            </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label24" runat="server" Text="Name" Width="76px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtFollowUpName" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label23" runat="server" Text="Follow Up Description" Width="76px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtFollowUpDesc" runat="server" CssClass="multilinetext" EnableTheming="False"
                                Height="40px" TextMode="MultiLine" Width="560px">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table id="Table2">
                                <tr  >
                                    <td style="text-align:center">
                                        <asp:Button ID="btnFollowUpSave" runat="server" Text="Save" OnClick="btnFollowUpSave_Click"
                                            CausesValidation="False" />
                                        <asp:Button ID="btnFollowUpRefresh" runat="server" Text="Refresh" OnClick="btnFollowUpRefresh_Click"
                                            CausesValidation="False" />
                                        <asp:Button ID="btnFollowUpHistory" runat="server" Text="History" OnClick="btnFollowUpHistory_Click"
                                            CausesValidation="False" />
                                        <asp:Button ID="btnFollowUpClose" runat="server" Text="Close" OnClick="btnFollowUpClose_Click"
                                            CausesValidation="False" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table id="tblFollowUpHistory" runat="server" border="0" cellpadding="0" cellspacing="0"
                                width="100%" visible="false">
                                <tr>
                                    <td class="profilehead" colspan="3">
                                        Follow Up History</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvFollowUp" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            DataSourceID="sdsFollowUp">
                                            <columns>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:MM/dd/yyyy}" DataField="AMCQT_FOLLOWUP_DET_DATE" SortExpression="AMCQT_FOLLOWUP_DET_DATE" HeaderText="Date"></asp:BoundField>
<asp:BoundField DataField="EMP_FIRST_NAME" SortExpression="EMP_FIRST_NAME" HeaderText="Name">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="AMCQT_FOLLOWUP_DET_DESC" SortExpression="AMCQT_FOLLOWUP_DET_DESC" HeaderText="Description">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
</columns>
                                            <selectedrowstyle backcolor="LightSteelBlue" />
                                        </asp:GridView>
                                        <asp:Label ID="lblQuotIdHiddenForFollowUp" runat="server" Visible="False"></asp:Label>
                                        <asp:SqlDataSource ID="sdsFollowUp" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                            SelectCommand="SELECT YANTRA_AMC_QUOT_FOLLOWUP_DET.AMCQT_FOLLOWUP_DET_DATE, YANTRA_AMC_QUOT_FOLLOWUP_DET.AMCQT_FOLLOWUP_DET_DESC, YANTRA_EMPLOYEE_MAST.EMP_FIRST_NAME FROM YANTRA_AMC_QUOT_FOLLOWUP_DET INNER JOIN YANTRA_AMC_QUOTATION_MAST ON YANTRA_AMC_QUOT_FOLLOWUP_DET.AMCQT_ID = YANTRA_AMC_QUOTATION_MAST.AMCQT_ID INNER JOIN YANTRA_EMPLOYEE_MAST ON YANTRA_AMC_QUOT_FOLLOWUP_DET.EMP_ID = YANTRA_EMPLOYEE_MAST.EMP_ID where&#13;&#10;YANTRA_AMC_QUOT_FOLLOWUP_DET .AMCQT_ID =@QUOT_ID">
                                            <selectparameters>
<asp:ControlParameter PropertyName="Text" DefaultValue="0" Name="QUOT_ID" ControlID="lblQuotIdHiddenForFollowUp"></asp:ControlParameter>
</selectparameters>
                                        </asp:SqlDataSource>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
           <tr>
            <td colspan="4">
                <table border="0" cellpadding="0" cellspacing="0" id="tblQuotationDetails" runat="server"
                    visible="true" width="100%">
                    <tr>
                        <td class="profilehead" colspan="4">
                            Quotation Details</td>
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
                            <asp:Label ID="lblAMCQTNo" runat="server" Text="AMC Quot No" Width="108px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtQuotationNo" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblQuotationDate" runat="server" Text="AMC Quot Date" Width="126px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtQuotationDate" runat="server" type="date" CssClass="datetext" EnableTheming="False">
                            </asp:TextBox>&nbsp;
                            
                            </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label ID="Label45" runat="server" Text="AMC Period" Width="88px"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox ID="txtAMCPeriod" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label ID="Label46" runat="server" Text="Ref No" Width="69px"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:DropDownList id="ddlCRNo" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label47" runat="server" Text="PM Calls" Width="87px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPMCalls" runat="server">
                            </asp:TextBox>
                            <cc1:FilteredTextBoxExtender
                                    ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtPMCalls" ValidChars="0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label48" runat="server" Text="BreakDown Calls" Width="115px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtBreakDownCalls" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label49" runat="server" Text="Payment Terms"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtPaymentTerms" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="497px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label50" runat="server" Text="Cust PO No" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCustPONo" runat="server" Visible="False">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label51" runat="server" Text="Cust PO Date" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCustPODate" runat="server" CssClass="datetext" EnableTheming="False"
                                Visible="False">
                            </asp:TextBox>
                            <%--<asp:Image ID="imgCustPODate" runat="server" ImageUrl="~/Images/Calendar.png"
                                Visible="False"></asp:Image><cc1:CalendarExtender ID="ceCustPODate" runat="server"
                                    PopupButtonID="imgCustPODate" TargetControlID="txtCustPODate" Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>--%>
                            <cc1:MaskedEditExtender ID="meeCustPODate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtCustPODate" UserDateFormat="MonthDayYear">
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
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Dustomer Details</td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                        </td>
                        <td style="height: 24px; text-align: left">
                        </td>
                        <td style="height: 24px; text-align: right">
                        </td>
                        <td style="height: 24px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td id="TD18" style="height: 22px; text-align: right">
                            <asp:Label ID="lblCustomer" runat="server" Text="Customer"></asp:Label></td>
                        <td style="height: 22px; text-align: left">
                            <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="True" Enabled="False"
                                OnSelectedIndexChanged="ddlCustomerName_SelectedIndexChanged">
                            </asp:DropDownList><asp:Label ID="Label31" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCustomerName"
                                ErrorMessage="Please Select the Customer" InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="height: 22px; text-align: right">
                            <asp:Label ID="lblRegion" runat="server" Text="Region"></asp:Label></td>
                        <td style="height: 22px; text-align: left">
                            <asp:TextBox ID="txtRegion" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 22px; text-align: right">
                            <asp:Label ID="Label9" runat="server" Text="Industry Type"></asp:Label></td>
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
                            <asp:Label ID="Label8" runat="server" Text="Mobile" Width="74px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Item Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label20" runat="server" Text="Brand"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlBrand" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label id="lblSearch" runat="server" Text="Search:" Width="84px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtSearchModel" runat="server">
                            </asp:TextBox><asp:RequiredFieldValidator id="RequiredFieldValidator17" runat="server"
                                ControlToValidate="txtSearchModel" ErrorMessage="Please Enter ModelNo For Search"
                                ValidationGroup="Search" Visible="False">*</asp:RequiredFieldValidator><asp:Button
                                    id="btnSearchModelNo" runat="server" BorderStyle="None" CausesValidation="False"
                                    CssClass="gobutton" EnableTheming="False" onclick="btnSearchModelNo_Click" Text="Go"
                                    ValidationGroup="Search" /><asp:SqlDataSource id="SqlDataSource2" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_MODELNO_SEARCH_SELECT"
                                        SelectCommandType="StoredProcedure"><selectparameters>
<asp:Parameter Type="Int64" DefaultValue="0" Name="BranbId"></asp:Parameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtSearchModel"></asp:ControlParameter>
</selectparameters>
                                    </asp:SqlDataSource></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: left">
                        </td>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label id="Label5" runat="server" Text="Model No" Width="76px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:DropDownList id="ddlModelNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModelNo_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="lblItemName" runat="server" Text="Item Name" Width="76px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:TextBox id="txtItemName" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label6" runat="server" Text="Item Category"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtItemCategory" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label id="Label7" runat="server" Text="Item Subcategory"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtItemSubCategory" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblItemCode" runat="server" Text="Item Type" Width="72px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label id="Label26" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                        <td style="text-align: right">
                            </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlItemName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="0">-- Select Item Type --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label id="Label27" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="Label22" runat="server" Text="Item Specification"></asp:Label></td>
                        <td colspan="3" style="height: 21px; text-align: left">
                            <asp:TextBox ID="txtItemSpec" runat="server" CssClass="multilinetext" EnableTheming="False"
                                ReadOnly="True" TextMode="MultiLine" Width="500px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label3" runat="server" Text="Serial No."></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtSerialNo" runat="server"></asp:TextBox>
                        </td>
                        <td style="text-align: right">
                            </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="lblQuantity" runat="server" Text="Quantity" Width="54px"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:TextBox ID="txtQunatity" runat="server">
                            </asp:TextBox>
                            <asp:Label id="Label29" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQunatity"
                                ErrorMessage="Please Enter the Quantity" ValidationGroup="qi">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                    ID="ftxteQuantity" runat="server" FilterType="Numbers" TargetControlID="txtQunatity">
                                </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="lblRate" runat="server" Text="Rate" Width="36px"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:TextBox ID="txtRate" runat="server">
                            </asp:TextBox>
                            <asp:Label id="Label28" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRate"
                                ErrorMessage="Please Enter the Rate" ValidationGroup="qi">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                    ID="ftxteRate" runat="server" TargetControlID="txtRate" ValidChars=".0123456789">
                                </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: left">
                        </td>
                        <td style="height: 21px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" Text="Add" OnClick="btnAdd_Click"
                                ValidationGroup="qi" />
                            <asp:Button ID="btnItemRefresh" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" Text="Refresh" OnClick="btnItemRefresh_Click"
                                CausesValidation="False" /></td>
                    </tr>
                    <tr>
                        <td style="height: 13px; text-align: right">
                        </td>
                        <td style="height: 13px; text-align: left">
                        </td>
                        <td style="height: 13px; text-align: right">
                        </td>
                        <td style="height: 13px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView id="gvQuotationItems" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvQuotationItems_RowDataBound"
                                OnRowDeleting="gvQuotationItems_RowDeleting" OnRowEditing="gvQuotationItems_RowEditing">
                                <columns>
<asp:CommandField ShowEditButton="True"></asp:CommandField>
<asp:CommandField ShowDeleteButton="True"></asp:CommandField>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ItemType" HeaderText="Item Type"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
                                    <asp:BoundField DataField="SerialNo" HeaderText="Serial No." NullDisplayText="-" />
<asp:BoundField DataField="Quantity" HeaderText="Quantity">
<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate">
<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
<asp:BoundField HeaderText="Amount">
<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ItemTypeId" HeaderText="ItemTypeIdHidden"></asp:BoundField>
</columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 1px; text-align: right">
                        </td>
                        <td style="height: 1px; text-align: left">
                        </td>
                        <td style="height: 1px; text-align: right">
                        </td>
                        <td style="height: 1px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label2" runat="server" Text="Sub Total" Width="74px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtSubTotal" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label52" runat="server" Text="Service Tax" Width="88px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtServiceTax" runat="server">
                            </asp:TextBox>
                            <asp:Label id="Label53" runat="server" Text="%"></asp:Label>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                TargetControlID="txtServiceTax" ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label1" runat="server" Text="Total" Width="74px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtTotalAmt" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Other Details</td>
                    </tr>
                    <tr>
                        <td style="height: 16px; text-align: right">
                        </td>
                        <td style="height: 16px; text-align: left">
                        </td>
                        <td style="height: 16px; text-align: right">
                        </td>
                        <td style="height: 16px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label4" runat="server" Text="Validity"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:TextBox ID="txtValidity" runat="server">
                            </asp:TextBox></td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
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
                        <td class="profilehead" colspan="4" style="height: 19px; text-align: left">
                            PM Calls Schedule</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 19px; text-align: right">
                            <table width="100%">
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
                                <tr>
                                    <td>
                                        <asp:Label id="Label10" runat="server" Text="Call Name"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox id="txtCallName" runat="server">
                            </asp:TextBox><asp:Label id="Label13" runat="server" EnableTheming="False" Font-Bold="False"
                                            Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                                id="RequiredFieldValidator6" runat="server" ControlToValidate="txtCallName" ErrorMessage="Please Enter the PM Calls"
                                                ValidationGroup="call">*</asp:RequiredFieldValidator></td>
                                    <td>
                                        <asp:Label id="Label14" runat="server" Text="Call Date"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox id="txtCallDate" runat="server" type="date" CssClass="datetext" EnableTheming="False">
                            </asp:TextBox>
                                        <asp:Label
                                            id="Label16" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                                id="RequiredFieldValidator7" runat="server" ControlToValidate="txtCallDate" ErrorMessage="Please Enter the Call Date"
                                                ValidationGroup="call">*</asp:RequiredFieldValidator>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: center">
                                        <asp:Button id="btnpmadd" runat="server" BackColor="Transparent" BorderStyle="None"
                                            CssClass="loginbutton" EnableTheming="False" OnClick="btnpmadd_Click" Text="Add"
                                            ValidationGroup="call" /><asp:Button id="btnpmrefresh" runat="server" BackColor="Transparent"
                                                BorderStyle="None" CausesValidation="False" CssClass="loginbutton" EnableTheming="False"
                                                OnClick="btnpmrefresh_Click" Text="Refresh" /></td>
                                </tr>
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
                                <tr>
                                    <td colspan="4" style="text-align: center">
                                        <asp:GridView id="gvpm" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvpm_RowDataBound"
                                            OnRowDeleting="gvpm_RowDeleting" OnRowEditing="gvpm_RowEditing">
                                            <columns>
<asp:CommandField ShowEditButton="True"></asp:CommandField>
<asp:CommandField ShowDeleteButton="True"></asp:CommandField>
<asp:BoundField DataField="callname" HeaderText="Call Name"></asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="calldate" HeaderText="Call Date"></asp:BoundField>
</columns>
                                        </asp:GridView></td>
                                </tr>
                            </table>
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
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlApprovedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
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
                        <td style="height: 19px">
                        </td>
                        <td style="height: 19px">
                        </td>
                        <td style="height: 19px">
                        </td>
                        <td style="height: 19px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 49px">
                            <table id="tblButtons" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 43px">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                    <td style="width: 70px">
                                        <asp:Button ID="btnApprove" runat="server" Text="Approve" CausesValidation="False"
                                            OnClick="btnApprove_Click" /></td>
                                    <td style="width: 70px">
                                        <asp:Button ID="btnRegret" runat="server" OnClick="btnRegret_Click" Text="Regret"
                                            Width="69px" /></td>
                                    <td>
                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click"
                                            CausesValidation="False" /></td>
                                    <td>
                                        <asp:Button ID="btnSend" runat="server" Text="Send E-Mail" OnClick="btnSend_Click"
                                            CausesValidation="False" /></td>
                                    <td>
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" CausesValidation="False" /></td>
                                    <td>
                                        <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Panel ID="Panel1" runat="server" Width="400px">
                                <table border="0" cellpadding="0" cellspacing="0" style="font-size: 8pt; font-family: Verdana;
                                    background-image: url(Images/ConfirmBox2.PNG); background-repeat: repeat;">
                                    <tr>
                                        <td style="text-align: left; height: 126px;" background="../../Images/ConfirmBox1.PNG"
                                            width="55">
                                        </td>
                                        <td style="height: 126px;" background="../../Images/ConfirmBox2.PNG" align="center"
                                            valign="top">
                                            <table>
                                                <tr>
                                                    <td colspan="3" rowspan="1" style="height: 43px; text-align: left">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="text-align: left">
                                                        <asp:Label ID="lblMessage" runat="server" Width="300px">After Approve the Quotation Do You Want To Send Quotation To Customer ?</asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="height: 17px; text-align: left"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="text-align: center">
                                                        <asp:Button ID="btnConfirmYes" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                                            Height="23px" Text="Yes" Width="80px" EnableTheming="False" OnClick="btnConfirmYes_Click" />
                                                        &nbsp;
                                                        <asp:Button ID="btnConfirmNo" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                                            Height="23px" Text="No" Width="80px" EnableTheming="False" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="height: 126px" background="../../Images/ConfirmBox3.PNG" width="27">
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="ModalPopupExtender" runat="server" PopupControlID="Panel1"
                                TargetControlID="btnApprove" RepositionMode="None">
                            </cc1:ModalPopupExtender>
                        </td>
                    </tr>
                </table>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False">
                </asp:ValidationSummary>
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="qi"
                    ShowMessageBox="True" ShowSummary="False">
                </asp:ValidationSummary><asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="call"></asp:ValidationSummary>
            </td>
        </tr>
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
</asp:Content>


 
