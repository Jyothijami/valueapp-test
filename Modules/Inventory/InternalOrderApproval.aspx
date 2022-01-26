<%@ Page Language="C#" MasterPageFile="~/MPs/InventoryMP1.master" AutoEventWireup="true"
    CodeFile="InternalOrderApproval.aspx.cs" Inherits="Modules_Inventory_InternalOrderApproval" Title="|| Value App : Inventory : InternalOrder Approval ||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <script>
        $(function () {
            $("[name$='txtOADate'],[name$='txtReqByDate']").datepicker();
        });
    </script>
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>Internal
                Order Approval</td>
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
            <td class="searchhead" colspan="4" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">Internal Order</td>
                        <td></td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <%--<asp:ListItem Value="WO_NO">Work Order No</asp:ListItem>--%>
                                            <asp:ListItem Value="WO_DATE">Work Order Date</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME">Customer</asp:ListItem>
                                            <asp:ListItem Value="CUST_CONTACT_PERSON">Contact Person</asp:ListItem>
                                            <asp:ListItem Value="CUST_EMAIL">EMail Address</asp:ListItem>
                                            <asp:ListItem Value="WO_DELIVERY_DATE">Delivery Date</asp:ListItem>
                                            <asp:ListItem Value="WO_FLAG">Status</asp:ListItem>
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
                                        <cc1:CalendarExtender  Format="dd/MM/yyyy" ID="ceSearchFrom" runat="server" Enabled="False" PopupButtonID="imgFromDate"
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
                                        </asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><%--<asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False" />
                                        <cc1:CalendarExtender  Format="dd/MM/yyyy" ID="ceSearchValueToDate" runat="server" Enabled="False" PopupButtonID="imgToDate"
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
                            <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label>
                            <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label>
                            <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <asp:GridView ID="gvWorkOrderDetails" SelectedRowStyle-BackColor="#c0c0c0" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="sdsWorkOrder" OnRowDataBound="gvWorkOrderDetails_RowDataBound1" AllowSorting="True" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="WO_ID" SortExpression="WO_ID" HeaderText="WOIdHidden">
                            <ItemStyle Width="200px"></ItemStyle>

                            <HeaderStyle Width="200px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="IO No" SortExpression="WO_NO">
                            <ControlStyle Width="150px"></ControlStyle>

                            <ItemStyle Width="150px" Wrap="True" HorizontalAlign="Center"></ItemStyle>

                            <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnWorkOrderNo" OnClick="lbtnWorkOrderNo_Click" ForeColor="#0066ff" runat="server" Text='<%# Eval("WO_NO") %>' CausesValidation="False" __designer:wfdid="w62"></asp:LinkButton>
                            </ItemTemplate>

                            <FooterStyle Width="150px"></FooterStyle>
                        </asp:TemplateField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="WO_DATE" SortExpression="WO_DATE" HeaderText="IODate">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CUST_NAME" SortExpression="CUST_NAME" HeaderText="Customer">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

                            <FooterStyle HorizontalAlign="Left"></FooterStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CUST_CONTACT_PERSON" SortExpression="CUST_CONTACT_PERSON" HeaderText="ContactPerson">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CUST_EMAIL" SortExpression="CUST_EMAIL" HeaderText="E-MailAddress"></asp:BoundField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="WO_DELIVERY_DATE" SortExpression="WO_DELIVERY_DATE" HeaderText="DeliveryDate">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PREPAREDBY" SortExpression="PREPAREDBY" HeaderText="PreparedBy"></asp:BoundField>
                        <asp:BoundField DataField="APPROVEDBY" SortExpression="APPROVEDBY" HeaderText="ApprovedBy"></asp:BoundField>
                        <asp:BoundField DataField="WO_FLAG" SortExpression="WO_FLAG" HeaderText="Status"></asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:LinkButton ID="lbtnWorkOrderNo" runat="server" Text='<%# Eval("WO_NO") %>'></asp:LinkButton>
                    </EmptyDataTemplate>
                </asp:GridView>
                <asp:SqlDataSource ID="sdsWorkOrder" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SM_WORKORDER_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="EmpId" ControlID="lblEmpIdHidden"></asp:ControlParameter>
                    </SelectParameters>
                </asp:SqlDataSource>


            </td>
        </tr>
        <tr>
            <td style="text-align: right;"></td>
            <td style="text-align: left;"></td>
            <td style="text-align: right;"></td>
            <td style="width: 249px;"></td>
        </tr>
        <tr>
            <td colspan="4"></td>
        </tr>
        <tr>
            <td colspan="4">
                <table border="0" cellpadding="0" cellspacing="0" id="tblWorkOrderDetails" runat="server"
                    width="100%" visible="true">

                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">General Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left;"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left; width: 289px;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 75px;">
                            <asp:Label ID="lblSalesOrderNo" runat="server" Text="Purchase Order No" Width="132px"></asp:Label></td>
                        <td style="text-align: left; height: 75px;">
                            <asp:DropDownList ID="ddlOrderAcceptance" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOrderAcceptance_SelectedIndexChanged" Enabled="False">
                            </asp:DropDownList><asp:Label ID="Label36" runat="server" EnableTheming="False" ForeColor="Red" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlOrderAcceptance"
                                ErrorMessage="Please Select The Order Acceptance No." InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right; height: 75px;">
                            <asp:Label ID="lblSalesOrderDate" runat="server" Text="Purchase Order Date" Width="131px"></asp:Label></td>
                        <td style="text-align: left; width: 289px; height: 75px;">
                            <asp:TextBox ID="txtOADate" runat="server" ReadOnly="True" CssClass="datetext" EnableTheming="False"></asp:TextBox>&nbsp;
                            
                            <cc1:MaskedEditExtender ID="meeOADate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtOADate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblCustomer" runat="server" Text="Customer Name"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtCustName" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label45" runat="server" Text="Unit Name"></asp:Label></td>
                        <td style="text-align: left; width: 289px;">
                            <asp:TextBox ID="txtUnitName" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td rowspan="2" style="text-align: right">
                            <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label></td>
                        <td rowspan="2" style="text-align: left;">
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
                        <td style="text-align: right; height: 38px;">
                            <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
                        <td style="text-align: left; height: 38px;">
                            <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right; height: 38px;">
                            <asp:Label ID="Label26" runat="server" Text="Mobile"></asp:Label></td>
                        <td style="text-align: left; width: 289px; height: 38px;">
                            <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label10" runat="server" Text="Specification" Visible="False"></asp:Label></td>
                        <td style="width: 289px; text-align: left">
                            <asp:TextBox ID="txtSpecification" runat="server" CssClass="multilinetext" EnableTheming="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">Purchase Order Items</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="gvOrderAcceptanceItems" runat="server" AutoGenerateColumns="False"
                                OnRowDataBound="gvOrderAcceptanceItems_RowDataBound1" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Res Status">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" __designer:wfdid="w1" OnCheckedChanged="chk_CheckedChanged"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                    <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                    <asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
                                    <asp:BoundField HeaderText="Amount"></asp:BoundField>
                                    <asp:BoundField DataField="Specifications" HeaderText="Specifications"></asp:BoundField>
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
                                    <asp:BoundField DataField="Priority" HeaderText="Priority"></asp:BoundField>
                                    <asp:BoundField DataField="DeliveryDate" HeaderText="Delivery Date"></asp:BoundField>
                                    <asp:BoundField DataField="Room" HeaderText="Room"></asp:BoundField>
                                    <asp:BoundField DataField="Price" HeaderText="Price"></asp:BoundField>
                                    <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                                    <asp:BoundField DataField="ColorId" HeaderText="ColorId"></asp:BoundField>
                                    <asp:BoundField DataField="SO_RES_STATUS" HeaderText="ReserveStatus"></asp:BoundField>
                                    <asp:BoundField DataField="SODetId" HeaderText="SoDetId"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblSearch" runat="server" Text="Search:" Width="84px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtSearchModel" runat="server">
                            </asp:TextBox><asp:Button ID="btnSearchModelNo" runat="server" BorderStyle="None"
                                CausesValidation="False" CssClass="gobutton" EnableTheming="False" OnClick="btnSearchModelNo_Click"
                                Text="Go" ValidationGroup="Search" /><asp:SqlDataSource ID="SqlDataSource2" runat="server"
                                    ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_MODELNO_SEARCH_SELECT"
                                    SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:Parameter Type="Int64" DefaultValue="0" Name="BranbId"></asp:Parameter>
                                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtSearchModel"></asp:ControlParameter>
                                    </SelectParameters>
                                </asp:SqlDataSource>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label8" runat="server" Text="Search By Brand"></asp:Label></td>
                        <td style="text-align: left; width: 289px;">
                            <asp:DropDownList ID="ddlBrandselect" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrandselect_SelectedIndexChanged">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblModelName" runat="server" Text="Model No" Width="81px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged">
                            </asp:DropDownList><asp:Label ID="Label13" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlItemType" ErrorMessage="Please Select the Model No"
                                    InitialValue="0" ValidationGroup="id">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label2" runat="server" Text="Model Name"></asp:Label></td>
                        <td style="text-align: left; width: 289px;">
                            <asp:TextBox ID="txtModelName" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label ID="Label15" runat="server" Text="Item Category"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox ID="txtItemCategory" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label ID="lblItemGroup" runat="server" Text="Item SubCategory" Width="117px"></asp:Label></td>
                        <td style="text-align: left; width: 289px; height: 24px;">
                            <asp:TextBox ID="txtItemSubCategory" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 53px;">&nbsp;<asp:Label ID="Label19" runat="server" Text=" Color" Width="44px"></asp:Label></td>
                        <td style="text-align: left; height: 53px;">
                            <asp:DropDownList ID="ddlColor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlColor_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="text-align: right; height: 53px;">
                            <asp:Label ID="Label4" runat="server" Text="Quantity"></asp:Label></td>
                        <td style="width: 289px; text-align: left; height: 53px;">
                            <asp:TextBox ID="txtQuantity" runat="server">
                            </asp:TextBox><asp:Label ID="Label16" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Please Enter the Quantity Required"
                                    ValidationGroup="id">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender ID="ftxtQuantity"
                                        runat="server" FilterType="Numbers" TargetControlID="txtQuantity">
                                    </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label5" runat="server" Text="Company :"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label3" runat="server" Text="UOM"></asp:Label></td>
                        <td style="width: 289px; text-align: left">
                            <asp:TextBox ID="txtItemUOM" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label ID="Label7" runat="server" Text="Godown :"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                            <asp:DropDownList ID="ddlGodown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged2">
                            </asp:DropDownList></td>
                        <td style="height: 24px; text-align: right">
                            <asp:Label ID="Label1" runat="server" Text="Quantity In Hand"></asp:Label></td>
                        <td style="width: 289px; height: 24px; text-align: left">
                            <asp:TextBox ID="txtQuantityInHand" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="lblPriority" runat="server" Text="Priority :" Width="65px"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:DropDownList ID="ddlItemPriority" runat="server">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem>Low</asp:ListItem>
                                <asp:ListItem>Medium</asp:ListItem>
                                <asp:ListItem>High</asp:ListItem>
                            </asp:DropDownList><asp:Label ID="Label17" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label></td>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label6" runat="server" Text="Brand"></asp:Label></td>
                        <td style="width: 289px; height: 19px; text-align: left">
                            <asp:TextBox ID="txtBrand" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblItemImage" runat="server" Text="Item Image"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:Image ID="Image1" runat="server" Height="85px" ImageUrl="~/Images/noimage400x300.gif"
                                Width="140px"></asp:Image></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label9" runat="server" Text="Required by Date"></asp:Label></td>
                        <td style="width: 289px; text-align: left">
                            <asp:TextBox ID="txtReqByDate" runat="server">
                            </asp:TextBox>

                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtReqByDate"
                                ErrorMessage="Please Select Required By Date" ValidationGroup="id">*</asp:RequiredFieldValidator><asp:CustomValidator
                                    ID="CustomValidator5" runat="server" ClientValidationFunction="DateCustomValidate"
                                    ControlToValidate="txtReqByDate" ErrorMessage="Please Enter Required By Date not Less Than the Present Date"
                                    SetFocusOnError="True" ValidationGroup="id">*</asp:CustomValidator><asp:CustomValidator
                                        ID="CustomValidator1" runat="server" ClientValidationFunction="DateCustomValidate"
                                        ControlToValidate="txtReqByDate" ErrorMessage="Please Enter the Required By Date in DD/MM/YYYY Format or Check  Year in 2009 to 2099 Range or not"
                                        SetFocusOnError="True" ValidationGroup="id">*</asp:CustomValidator>
                            <cc1:MaskedEditExtender ID="MaskededitReqByDate" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtReqByDate"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="width: 289px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: right">
                            <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" OnClick="btnAdd_Click" Text="Add"
                                ValidationGroup="id" /></td>
                        <td style="height: 19px; text-align: left">
                            <asp:Button ID="btnItemRefresh" runat="server" BackColor="Transparent" BorderStyle="None"
                                CausesValidation="False" CssClass="loginbutton" EnableTheming="False" OnClick="btnItemRefresh_Click"
                                Text="Refresh" /></td>
                        <td style="width: 289px; height: 19px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="height: 27px; text-align: right"></td>
                        <td style="height: 27px; text-align: right"></td>
                        <td style="height: 27px; text-align: left"></td>
                        <td style="width: 289px; height: 27px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:GridView ID="gvItemDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvItemDetails_RowDataBound"
                                OnRowDeleting="gvItemDetails_RowDeleting" OnRowEditing="gvItemDetails_RowEditing">
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
                                    <asp:BoundField DataField="ItemName" HeaderText="Model Name"></asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Priority" HeaderText="Priority"></asp:BoundField>
                                    <asp:BoundField DataField="Brand" HeaderText="Brand"></asp:BoundField>
                                    <asp:BoundField DataField="ReqFor" HeaderText="Requried for">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ReqFor" HeaderText="Room">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HtmlEncode="False" DataFormatString="{0:MM/dd/yyyy}" DataField="ReqDate" HeaderText="Required by Date"></asp:BoundField>
                                    <asp:BoundField DataField="Specification" HeaderText="Specification">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Priority" HeaderText="Priority"></asp:BoundField>
                                    <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                                    <asp:BoundField DataField="ColorId" HeaderText="ColorId"></asp:BoundField>
                                    <asp:BoundField DataField="CPID" HeaderText="CPID"></asp:BoundField>
                                    <asp:BoundField DataField="GDID" HeaderText="GDID"></asp:BoundField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <span style="color: #ff0000">No Data to Display</span>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="width: 289px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table id="tblButtons">
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td style="width: 3px"></td>
                                    <td>
                                        <asp:Button ID="btnReserve" runat="server" Text="Reserve" CausesValidation="False"
                                            OnClick="btnReserve_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CausesValidation="False"
                                            OnClick="btnRefresh_Click" Visible="False" /></td>
                                    <td>
                                        <asp:Button ID="btnClose" runat="server" Text="Close" CausesValidation="False" OnClick="btnClose_Click" /></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td style="text-align: right;"></td>
                        <td style="width: 289px"></td>
                    </tr>
                </table>
                <asp:Panel ID="Panel1" runat="server" meta:resourcekey="Panel1Resource1">
                </asp:Panel>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="id" />
                <cc1:ModalPopupExtender
                    ID="ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="False"
                    PopupControlID="Panel1" RepositionMode="None" TargetControlID="btnReserve">
                </cc1:ModalPopupExtender>
            </td>
        </tr>
    </table>
</asp:Content>


 
