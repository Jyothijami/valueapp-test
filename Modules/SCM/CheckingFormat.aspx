<%@ Page Language="C#" MasterPageFile="~/MPs/InventoryMP1.master" AutoEventWireup="true"
    CodeFile="CheckingFormat.aspx.cs" Inherits="Modules_SCM_CheckingFormat" Title="||Value App : Inventory : CHECKING FORMAT||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td style="text-align:left">
                MRN(Material Receipt Note)</td>
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
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left;">
                            MRN</td>
                        <td>
                        </td>
                        <td style="text-align: right;">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td>
                                        <asp:Label id="lblSearch" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList id="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged1">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="CHK_NO">MRN No</asp:ListItem>
                                            <asp:ListItem Value="CHK_DATE">MRN Date</asp:ListItem>
                                            <%--<asp:ListItem Value="CHK_EQUIPMENT_NAME">Equipment Name</asp:ListItem>--%>
                                            <%--<asp:ListItem Value="CHK_MANUFACTURER_NAME">Suplier Id</asp:ListItem>--%>
                                            <asp:ListItem Value="CHK_RECEIVED_ON">Received On</asp:ListItem>
                                           <%-- <asp:ListItem Value="ITEM_MODEL_NO">Model No</asp:ListItem>--%>
                                        </asp:DropDownList></td>
                                    <td>
                                        <asp:DropDownList id="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                            EnableTheming="False" Visible="False" Width="50px" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged1">
                                            <asp:ListItem Selected="True">=</asp:ListItem>
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem>&gt;</asp:ListItem>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <asp:ListItem>&gt;=</asp:ListItem>
                                            <asp:ListItem>R</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td>
                                        <asp:Label id="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                                    <td>
                                        <asp:TextBox id="txtSearchValueFromDate" runat="server" type="date" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox><%--<asp:Image id="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender ID="ceSearchFrom" runat="server" Enabled="False" Format="dd/MM/yyyy"
                                            PopupButtonID="imgFromDate" TargetControlID="txtSearchValueFromDate">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>--%>
                                    </td>
                                    <td>
                                        <asp:Label id="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                    <td>
                                        <asp:TextBox id="txtSearchValueToDate" runat="server" type="date" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox></td>
                                    <td>
                                        <asp:TextBox id="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><%--<asp:Image id="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender ID="ceSearchValueToDate" runat="server" Enabled="False" Format="dd/MM/yyyy"
                                            PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>--%>
                                    </td>
                                    <td>
                                        <asp:Button id="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnSearchGo_Click1" /></td>
                                </tr>
                                </table>
                             <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label>
                             <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblUserType" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblUserId" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center;">
                <asp:GridView id="gvCheckForm" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="sdsCheckingDetails" SelectedRowStyle-BackColor="#c0c0c0" OnRowDataBound="gvCheckForm_RowDataBound" Width="100%" AllowSorting="True">
                    <columns>
<asp:BoundField DataField="CHK_ID" SortExpression="CHK_ID" HeaderText="ChkIdHidden">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="MRN No" SortExpression="CHK_NO"><EditItemTemplate>
<asp:TextBox id="TextBox1" runat="server" Text='<%# Bind("CHK_NO") %>'></asp:TextBox> 
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnCheckingNo" onclick="lbtnCheckingNo_Click" ForeColor="#0066ff" runat="server" Text='<%# Bind("CHK_NO") %>' CausesValidation="False" __designer:wfdid="w6"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HtmlEncode="False" SortExpression="CHK_DATE" DataFormatString="{0:dd/MM/yyyy}" DataField="CHK_DATE" HeaderText="MRN Date">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CHK_PO_NO" SortExpression="CHK_PO_NO" HeaderText="PO No">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField HtmlEncode="False" SortExpression="CHK_RECEIVED_ON" DataFormatString="{0:dd/MM/yyyy}" DataField="CHK_RECEIVED_ON" HeaderText="Received On"></asp:BoundField>
<asp:BoundField DataField="CHK_MODEL" SortExpression="CHK_MODEL" HeaderText="Model Name"></asp:BoundField>
<asp:BoundField DataField="CHK_PREPARED_BY" SortExpression="CHK_PREPARED_BY" HeaderText="Prepared By"></asp:BoundField>
<asp:BoundField DataField="CHK_APPROVED_BY" SortExpression="CHK_APPROVED_BY" HeaderText="Checked By"></asp:BoundField>
<asp:BoundField DataField="Full_CompName" SortExpression="Full_CompName" HeaderText="Company Name">
    <ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

</asp:BoundField>
</columns>
                </asp:GridView>
                <asp:SqlDataSource id="sdsCheckingDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SCM_CHECKING_FORMAT_SEARCH_SELECT" SelectCommandType="StoredProcedure"><selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
                        <%--<asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>--%>
                        <%--<asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="EmpId" ControlID="lblEmpIdHidden"></asp:ControlParameter>--%>
</selectparameters></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center;">
                <table id="Table1" align="center">
                    <tr>
                        <td style="height: 26px">
                            <asp:Button id="btnNew" runat="server" CausesValidation="False" Text="New" OnClick="btnNew_Click" /></td>
                        <td style="height: 26px">
                            <asp:Button id="btnEdit" runat="server" CausesValidation="False" Text="Edit" OnClick="btnEdit_Click" /></td>
                        <td style="height: 26px">
                            <asp:Button id="btnDelete" runat="server" CausesValidation="False" Text="Delete"
                                OnClick="btnDelete_Click" /></td>
                        <td style="height: 26px">
                        </td>
                        <td style="height: 26px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" title="|| YANTRA:SCM:CHECKINGFORMAT||">
                <table border="0" cellpadding="0" cellspacing="0" id="tblCheckDetails" runat="server"
                    visible="false" width="100%">
                  <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">
                            General Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                        </td>
                        <td style="text-align: left; ">
                            <table class="stacktable">
                                <tr>
                                    <td class="auto-style1">
                            <asp:RadioButton id="rdbWithPo" runat="server" AutoPostBack="True"
                                GroupName="as" OnCheckedChanged="rdbWithPo_CheckedChanged" Text="With Po">
                            </asp:RadioButton>
                                    </td>
                                    <td>
                            <asp:RadioButton id="rdbWithoutPo" runat="server" AutoPostBack="True" GroupName="as"
                                OnCheckedChanged="rdbWithoutPo_CheckedChanged" Text="WithOut Po" Checked="True">
                            </asp:RadioButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="text-align: right; ">
                        </td>
                        <td style="text-align: left; ">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label id="lblCkNo" runat="server" Text="MRN REF No"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox id="txtCkNo" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label id="lblDate" runat="server" Text="MRN Date"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox id="txtCheckingDate" runat="server">
                            </asp:TextBox><asp:Image id="imgDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><asp:RequiredFieldValidator
                                id="RequiredFieldValidator7" runat="server" ControlToValidate="txtCheckingDate"
                                ErrorMessage="Please Select the Date" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                            <cc1:CalendarExtender
                                ID="CeDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgDate" TargetControlID="txtCheckingDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MeeDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtCheckingDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblPoNo" runat="server" Text="PO NO . SLPL"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlPONO" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPONO_SelectedIndexChanged">
                            </asp:DropDownList><asp:TextBox id="txtSlpl" runat="server" Visible="False"></asp:TextBox><asp:TextBox
                                id="txtwithoutpono" runat="server" Visible="False"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                id="RequiredFieldValidator6" runat="server" ControlToValidate="ddlPONO" ErrorMessage="Please Select the PO No."
                                InitialValue="0" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label id="lblOrderDate" runat="server" Text="PO Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtPODate" runat="server" ReadOnly="True">
                            </asp:TextBox><asp:Image id="imgOrderDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>
                            <cc1:MaskedEditExtender ID="meeOrderDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtPODate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblSupplierName" runat="server" Text="Supplier Name"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlSupplierName" runat="server" Width="207px">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label id="Label14" runat="server" Text="Company Name"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlCompanyName" runat="server" OnSelectedIndexChanged="ddlCompanyName_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server"
                                ControlToValidate="ddlCompanyName" ErrorMessage="Please Select the Company Name"
                                InitialValue="0" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblSupplier" runat="server" Text="Supplier Invoice No/DC"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlSupInvNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSupInvNo_SelectedIndexChanged">
                            </asp:DropDownList><asp:TextBox id="txtSupplier" runat="server" Visible="False"></asp:TextBox><asp:TextBox
                                id="txtSuplierInvoiceNowithoutpo" runat="server" Visible="False"></asp:TextBox>&nbsp;</td>
                        <td style="text-align: right">
                            <asp:Label id="lblSupplierDate" runat="server" Text="Supplier Invoice Date" Width="138px">
                            </asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtSupplierDate" runat="server"></asp:TextBox><asp:Image id="imgSupplier" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>
                            <br />
                            <cc1:CalendarExtender
                                ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgSupplier" TargetControlID="txtSupplierDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtSupplierDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label10" runat="server" Text="MRN No"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtGatepass" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label id="Label11" runat="server" Text="L.R. No"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtlrno" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 79px;">
                        </td>
                        <td style="text-align: left; height: 79px;">
                        </td>
                        <td style="text-align: right; height: 79px;">
                            <asp:Label id="lblReceived" runat="server" Text="Received On" Width="86px"></asp:Label></td>
                        <td style="text-align: left; height: 79px;">
                            <asp:TextBox id="txtReceivedOn" runat="server"></asp:TextBox><asp:Image id="imgOnDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender ID="ceReceivedDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgOnDate"
                                TargetControlID="txtReceivedOn">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeReceivedDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtReceivedOn" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            PO Items Details</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:GridView id="gvItDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvItDetails_RowDataBound"
                                OnRowEditing="gvItDetails_RowEditing" Width="100%">
                                <columns>
<asp:CommandField ShowEditButton="True"></asp:CommandField>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
<asp:BoundField DataField="ItemType" HeaderText="Model No"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Model Name"></asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField HeaderText="Amount"></asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="DeliveryDate" HeaderText="Delivery Date"></asp:BoundField>
<asp:BoundField DataField="Specifications" HeaderText="Specifications">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Remarks" HeaderText="Remarks">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ItemTypeId" HeaderText="Item Type Id"></asp:BoundField>
</columns>
                                <emptydatatemplate>
&nbsp; <SPAN style="COLOR: #ff0033">No Data Found</SPAN>
</emptydatatemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            MRN Items Details</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 19px; text-align: center">
                            <asp:GridView id="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound"
                                Width="100%">
                                <columns>
<asp:TemplateField HeaderText="Delete"><ItemTemplate>
<asp:LinkButton id="lbtnDelete2" onclick="lbtnDelete2_Click" runat="server" __designer:wfdid="w2">Delete</asp:LinkButton>
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="ItemCode" HeaderText="ItemCode"></asp:BoundField>
<asp:BoundField DataField="ItemType" HeaderText="Model No"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
<asp:BoundField DataField="Godown" HeaderText="Location"></asp:BoundField>
<asp:BoundField DataField="OQuantity" HeaderText="Ordered Quantity"></asp:BoundField>
<asp:BoundField DataField="RQuantity" HeaderText="Received Qty"></asp:BoundField>
<asp:BoundField DataField="AQuantity" HeaderText="Accepted Qty"></asp:BoundField>
<asp:BoundField DataField="ReQuantity" HeaderText="Rejected Qty"></asp:BoundField>
<asp:BoundField DataField="NetQty" HeaderText="Net Qty"></asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
<asp:BoundField HeaderText="Amount"></asp:BoundField>
<asp:BoundField DataField="Godownid" HeaderText="Godownid"></asp:BoundField>
<asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
<asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
<asp:BoundField DataField="Colorid" HeaderText="Colorid"></asp:BoundField>
<asp:BoundField DataField="Chkid" HeaderText="Chkid"></asp:BoundField>
<asp:BoundField DataField="CHKDET_ID" HeaderText="CHKDET_ID"></asp:BoundField>
</columns>
                                <emptydatatemplate>
<SPAN style="COLOR: #ff0033">MRN Are Not Yet Done</SPAN>
</emptydatatemplate>
                            </asp:GridView>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="height: 22px; text-align: left">
                            Item Details
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label id="Label12" runat="server" Text="Search By Brand"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList id="ddlBrand" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="text-align: right;">
                            <asp:Label id="Label39" runat="server" Text="Search:" Width="84px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox id="txtSearchModel" runat="server">
                        </asp:TextBox><asp:RequiredFieldValidator id="RequiredFieldValidator17" runat="server"
                                ControlToValidate="txtSearchModel" ErrorMessage="Please Enter ModelNo For Search"
                                ValidationGroup="Search">*</asp:RequiredFieldValidator><asp:Button id="btnSearchModelNo"
                                    runat="server" BorderStyle="None" CausesValidation="False" CssClass="gobutton"
                                    EnableTheming="False" onclick="btnSearchModelNo_Click" Text="Go" ValidationGroup="Search" /><asp:SqlDataSource
                                        id="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                        SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure"><selectparameters>
<asp:ControlParameter PropertyName="SelectedValue" Type="Int64" DefaultValue="0" Name="BranbId" ControlID="ddlBrand"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtSearchModel"></asp:ControlParameter>
</selectparameters>
                                    </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblModel" runat="server" Text="ModelNo / Model Name" Width="157px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlItemName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="0">-- Select Item Type --</asp:ListItem>
                            </asp:DropDownList><asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server"
                                ControlToValidate="ddlItemName" ErrorMessage="Please Select the Model/Item Name"
                                InitialValue="0" SetFocusOnError="True" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label id="lblInstrument" runat="server" Text="Item Name"
                                Width="90px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtItemName" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label id="Label32" runat="server" Text="Item Category :"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox id="txtItemCategory" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label id="Label33" runat="server" Text="Item SubCategory :"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox id="txtItemSubCategory" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 10px;">
                            <asp:Label id="Label54" runat="server" Text="Color :"></asp:Label></td>
                        <td style="text-align: left; height: 10px;">
                            &nbsp;<asp:DropDownList id="ddlcolor" runat="server"></asp:DropDownList></td>
                        <td style="text-align: right; height: 10px;">
                            <asp:Label id="Label55" runat="server" Text="Brand :"></asp:Label></td>
                        <td style="text-align: left; height: 10px;">
                            <asp:TextBox id="txtManufacturer" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 9px;">
                            <asp:Label id="lblSerialNo" runat="server" Text="Serial No" Visible="False"></asp:Label></td>
                        <td style="text-align: left; height: 9px;">
                            <asp:TextBox id="txtSerialNo" runat="server" Visible="False"></asp:TextBox></td>
                        <td style="text-align: right; height: 9px;">
                            </td>
                        <td style="text-align: left; height: 9px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label id="Label8" runat="server" Text="Rate"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox id="txtRate" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label id="Label9" runat="server" Text="Location"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList id="ddlgodown" runat="server" AutoPostBack="True">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;"><asp:Label ID="Label1" runat="server" Text="Ordered Quantity:"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtQty" runat="server"></asp:TextBox>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label5" runat="server" Text="Received Quantity:"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox id="txtRecqty" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label6" runat="server" Text="Accepted Quantity:"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtAcceptedqty" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label7" runat="server" Text="Rejected Quantity:"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtRejectedqty" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label13" runat="server" Text="Remarks"></asp:Label></td>
                        <td style="text-align: left" colspan="3">
                            <asp:TextBox id="txtRemarks" runat="server" Height="53px" TextMode="MultiLine" Width="606px" EnableTheming="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:Button id="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" onclick="btnAdd_Click" Text="Add" ValidationGroup="a" /><asp:Button
                                    id="btnItemRefresh" runat="server" BackColor="Transparent" BorderStyle="None"
                                    CssClass="loginbutton" EnableTheming="False" onclick="btnItemRefresh_Click" Text="Refresh" CausesValidation="False" /></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:GridView id="gvItemDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvItemDetails_RowDataBound"
                                OnRowDeleting="gvItemDetails_RowDeleting" OnRowEditing="gvItemDetails_RowEditing"
                                Width="100%">
                                <columns>
<asp:CommandField ShowEditButton="True"></asp:CommandField>
<asp:CommandField ShowDeleteButton="True"></asp:CommandField>
<asp:BoundField DataField="ItemCode" HeaderText="ItemCode"></asp:BoundField>
<asp:BoundField DataField="ItemType" HeaderText="Model No"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
<asp:BoundField DataField="Godown" HeaderText="Location"></asp:BoundField>
<asp:BoundField DataField="OQuantity" HeaderText="Ordered Quantity"></asp:BoundField>
<asp:BoundField DataField="RQuantity" HeaderText="Received Qty"></asp:BoundField>
<asp:BoundField DataField="AQuantity" HeaderText="Accepted Qty"></asp:BoundField>
<asp:BoundField DataField="ReQuantity" HeaderText="Rejected Qty"></asp:BoundField>
<asp:BoundField DataField="NetQty" HeaderText="Net Qty"></asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
<asp:BoundField HeaderText="Amount"></asp:BoundField>
<asp:BoundField DataField="Godownid" HeaderText="Godownid"></asp:BoundField>
<asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
<asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
<asp:BoundField DataField="Colorid" HeaderText="Colorid"></asp:BoundField>
</columns>
                                <emptydatatemplate>
<SPAN style="COLOR: #ff0033">No Data Found</SPAN>
</emptydatatemplate>
                            </asp:GridView></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: right;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left;" class="profilehead">
                            Reference Details</td>
                    </tr>
                    <tr>
                        <td style="height: 34px; text-align: right;">
                        </td>
                        <td style="height: 34px; text-align: left;">
                        </td>
                        <td style="height: 34px; text-align: right;">
                        </td>
                        <td style="height: 34px; text-align: left;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <table width="100%">
                                <tr>
                                    <td style="text-align: right; height: 12px;">
                            <asp:Label id="lblPreparedBy" runat="server" Text="Material Executed " Width="135px"></asp:Label></td>
                                    <td style="text-align: left; height: 12px;">
                            <asp:DropDownList id="ddlPreparedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                                    <td style="text-align: right; height: 12px;">
                            <asp:Label id="lblApprovedBy" runat="server" Text="Checked By" Width="96px"></asp:Label></td>
                                    <td style="text-align: left; height: 12px;">
                            <asp:DropDownList id="ddlApprovedBy" runat="server">
                            </asp:DropDownList></td>
                                    <td style="text-align: right; height: 12px;">
                                        <asp:Label id="Label2" runat="server" Text="Store Incharge"></asp:Label></td>
                                    <td style="text-align: left; height: 12px;">
                                        <asp:DropDownList id="ddlStoreIncharge" runat="server">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label id="Label3" runat="server" Text="Accounts"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:DropDownList id="ddlAccounts" runat="server">
                                        </asp:DropDownList></td>
                                    <td colspan="2" style="text-align: right">
                                    </td>
                                    <td style="text-align: right">
                                        &nbsp;<asp:Label id="Label4" runat="server" Text="Authorised Payments" Width="130px"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:DropDownList id="ddlApayments" runat="server">
                                        </asp:DropDownList></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table id="tblButtons" align="center">
                                <tr>
                                    <td>
                                        <asp:Button id="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                    <td>
                                        <asp:Button id="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" /></td>
                                    <td>
                                        <asp:Button id="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" CausesValidation="False" /></td>
                                    <td>
                                        <asp:Button id="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" CausesValidation="False" /></td>
                                    <td>
                                        <asp:Button id="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" /></td>
                                    <td style="width: 3px">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:ValidationSummary id="ValidationSummary1" runat="server"
                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="a">
                </asp:ValidationSummary>
                <asp:ValidationSummary id="ValidationSummary2" runat="server" ShowMessageBox="True"
                    ShowSummary="False">
                </asp:ValidationSummary>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    
    <style type="text/css">
        .auto-style1 {
            width: 67px;
        }
    </style>
    
</asp:Content>


 
