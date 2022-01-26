<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/InventoryMP1.master" AutoEventWireup="true" CodeFile="InternalOrderApprovalDetails.aspx.cs" Inherits="Modules_Inventory_InternalOrderApprovalDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <%-- <script>
        $(function () {
            $("[name$='txtReqByDate']").datepicker();
        });
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td style="text-align:left">
                Internal
                Order Approval Details</td>
            
            </tr>
        </table>
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
                    ErrorMessage="Please Select The Order Acceptance No." InitialValue="0" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
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
                <asp:TextBox ID="txtSpecification" runat="server" CssClass="multilinetext" EnableTheming="True"
                   ></asp:TextBox></td>
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
                <asp:TextBox ID="txtReqByDate" runat="server" type="date">
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
                                OnClick="btnReserve_Click" ValidationGroup="a" /></td>
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
    <asp:ValidationSummary ID="vs1" runat="server" ShowMessageBox="True" ShowSummary="False" />
    <asp:ValidationSummary ID="vs2" runat="server" ValidationGroup="id" ShowMessageBox="True" ShowSummary="False" />
    <asp:ValidationSummary ID="vs3" runat="server" ValidationGroup="a" ShowMessageBox="True" ShowSummary="False" />
    <cc1:ModalPopupExtender
        ID="ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="False"
        PopupControlID="Panel1" RepositionMode="None" TargetControlID="btnReserve">
    </cc1:ModalPopupExtender>
    <asp:SqlDataSource ID="sdsWorkOrder" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
        SelectCommand="SP_SM_WORKORDER_SEARCH_SELECT" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="EMPID" ControlID="lblEmpIdHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
        Visible="False"></asp:Label>
    <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
        Visible="False"></asp:Label>
    <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
</asp:Content>


 
