<%@ Page Language="C#" MasterPageFile="~/MPs/SalesMP1.master" AutoEventWireup="true"
    CodeFile="WorkOrder.aspx.cs" Inherits="Modules_SM_WorkOrder" Title="|| Value App : S&M : Order Profile ||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            height: 50px;
        }
        .auto-style2 {
            width: 228px;
            height: 50px;
        }
        .auto-style3 {
            width: 289px;
            height: 50px;
        }
        .auto-style4 {
            height: 46px;
        }
        .auto-style5 {
            width: 228px;
            height: 46px;
        }
        .auto-style6 {
            width: 289px;
            height: 46px;
        }
    </style>

    <script type = "text/javascript">
<!--
    function Check_Click(objRef) {
        //Get the Row based on checkbox
        var row = objRef.parentNode.parentNode;

        //Get the reference of GridView
        var GridView = row.parentNode;

        //Get all input elements in Gridview
        var inputList = GridView.getElementsByTagName("input");

        for (var i = 0; i < inputList.length; i++) {
            //The First element is the Header Checkbox
            var headerCheckBox = inputList[0];

            //Based on all or none checkboxes
            //are checked check/uncheck Header Checkbox
            var checked = true;
            if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                if (!inputList[i].checked) {
                    checked = false;
                    break;
                }
            }
        }
        headerCheckBox.checked = checked;

    }
    function checkAll(objRef) {
        var GridView = objRef.parentNode.parentNode.parentNode;
        var inputList = GridView.getElementsByTagName("input");
        for (var i = 0; i < inputList.length; i++) {
            var row = inputList[i].parentNode.parentNode;
            if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                if (objRef.checked) {
                    inputList[i].checked = true;
                }
                else {
                    if (row.rowIndex % 2 == 0) {
                        row.style.backgroundColor = "#C2D69B";
                    }
                    else {
                        row.style.backgroundColor = "white";
                    }
                    inputList[i].checked = false;
                }
            }
        }
    }
    //-->
</script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td style="text-align:left">Internal
                Order</td>
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
    <asp:UpdatePanel runat ="server"  >
        <ContentTemplate >
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left" colspan="2">
                                    Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
                                </td>

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
                                            <asp:ListItem Value="WO_NO">IO No</asp:ListItem>
                                            <asp:ListItem Value="WO_DATE">IO Date</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME">Customer Name</asp:ListItem>
                                            <asp:ListItem Value="CUST_CONTACT_PERSON">Contact Person</asp:ListItem>
                                            <asp:ListItem Value="CUST_EMAIL">EMail Address</asp:ListItem>
                                            <asp:ListItem Value="WO_DELIVERY_DATE">Delivery Date</asp:ListItem>
                                            <%--<asp:ListItem Value="WO_FLAG">Status</asp:ListItem>--%>
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
                                        <asp:TextBox ID="txtSearchValueFromDate" runat="server" type="datepic" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox><%--<asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
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
                                        <asp:TextBox ID="txtSearchValueToDate" runat="server" type="datepic" EnableTheming="True" Visible="False"
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
                            </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <asp:GridView ID="gvWorkOrderDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="sdsWorkOrder" OnRowDataBound="gvWorkOrderDetails_RowDataBound1" SelectedRowStyle-BackColor="#c0c0c0" AllowSorting="True" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="WO_ID" SortExpression="WO_ID" HeaderText="WOIdHidden">
                            <ItemStyle Width="200px"></ItemStyle>

                            <HeaderStyle Width="200px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="IO No">
                            <ControlStyle Width="150px"></ControlStyle>

                            <ItemStyle Width="150px" Wrap="True" HorizontalAlign="Center"></ItemStyle>

                            <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnWorkOrderNo" OnClick="lbtnWorkOrderNo_Click" ForeColor="#0066ff" runat="server" Text='<%# Eval("WO_NO") %>' CausesValidation="False" __designer:wfdid="w63"></asp:LinkButton>
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
                    width="100%" visible="false">
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">General Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left; width: 228px;"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left; width: 289px;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 75px;">
                            <asp:Label ID="lblSalesOrderNo" runat="server" Text="Purchase Order No" Width="132px"></asp:Label></td>
                        <td style="text-align: left; width: 228px; height: 75px;">
                            <asp:DropDownList ID="ddlOrderAcceptance" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOrderAcceptance_SelectedIndexChanged" Enabled="False">
                            </asp:DropDownList><asp:Label ID="Label36" runat="server" EnableTheming="False" ForeColor="Red" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlOrderAcceptance"
                                ErrorMessage="Please Select The Order Acceptance No." InitialValue="0" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right; height: 75px;">
                            <asp:Label ID="lblSalesOrderDate" runat="server" Text="Purchase Order Date" Width="131px"></asp:Label></td>
                        <td style="text-align: left; width: 289px; height: 75px;">
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
                        <td style="text-align: right; height: 26px;">
                            <asp:Label ID="Label25" runat="server" Text="Phone"></asp:Label></td>
                        <td style="text-align: left; width: 289px; height: 26px;">
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
                        <td colspan="4" style="text-align: left" class="profilehead">Purchase Order Items</td>
                    </tr>

                    <tr>
                        <td colspan="4" style="height: 21px">
                            <asp:GridView ID="gvSo" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDataBound ="gvSo_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);"
                                                AutoPostBack="true" OnCheckedChanged="CheckBox_CheckChanged" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" onclick="Check_Click(this)"
                                                AutoPostBack="true" OnCheckedChanged="CheckBox_CheckChanged" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                    <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                   <asp:BoundField DataField ="Annexure_Qty" HeaderText ="Annexure Qty" />
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
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">Internal Order / Annexure Indent</td>
                    </tr>

                    <tr>
                        <td colspan="4" style="height: 21px">
                            <asp:GridView ID="gvOrderAcceptanceItems" runat="server" AutoGenerateColumns="False"
                                OnRowDataBound="gvOrderAcceptanceItems_RowDataBound1" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Res Status">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" __designer:wfdid="w5"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                    <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                    <asp:TemplateField HeaderText ="Annexure Qty" >
                                        <ItemTemplate >
                                            <asp:TextBox ID="txtAnnexQty" Text='<%# Bind("Annexure_Qty") %>' runat ="server" ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
                        <td colspan="4" class="profilehead">Order Profile Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left; width: 228px;"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left; width: 289px;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label1" runat="server" Text="Order Profile No"></asp:Label></td>
                        <td style="text-align: left; width: 228px;">
                            <asp:TextBox ID="txtWONo" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label2" runat="server" Text="Order Profile Date" Width="114px"></asp:Label></td>
                        <td style="text-align: left; width: 289px;">
                            <asp:TextBox ID="txtWODate" runat="server" type="datepic"></asp:TextBox>
                            <asp:Label ID="Label10" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                            </asp:Label>
                            <asp:RequiredFieldValidator
                                ID="rfvWODate" runat="server" ControlToValidate="txtWODate" ErrorMessage="Please Select the Work Order Date" ValidationGroup="a">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="DateCustomValidate"
                                ControlToValidate="txtWODate" ErrorMessage="Please Enter the Work Order Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                SetFocusOnError="True">*</asp:CustomValidator>
                            <%--<cc1:MaskedEditExtender ID="meeWODate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtWODate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="auto-style1">
                            <asp:Label ID="lblModeofDelivery" runat="server" Text="Mode of Delivery"></asp:Label></td>
                        <td style="text-align: left; " class="auto-style2">
                            <asp:DropDownList ID="ddlDeliveryMode" runat="server">
                            </asp:DropDownList><asp:Label ID="Label11" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                            </asp:Label>
                            <asp:RequiredFieldValidator ID="rfvDeliveryMode" runat="server" ControlToValidate="ddlDeliveryMode"
                                ErrorMessage="Please Select the Mode of  Delivery" InitialValue="0" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right" class="auto-style1">
                            <asp:Label ID="lblDeliveryDate" runat="server" Text="Delivery Date" Height="17px"></asp:Label>&nbsp;</td>
                        <td style="text-align: left; " class="auto-style3">
                            <%--<asp:TextBox ID="txtDeliveryDate" type="date" runat="server"></asp:TextBox>--%>
                            <asp:TextBox ID="txtDeliveryDate" runat="server" type="datepic"></asp:TextBox>
                            <asp:Label ID="Label16" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                            </asp:Label><asp:RequiredFieldValidator
                                ID="rfvDeliveryDate" runat="server" ControlToValidate="txtDeliveryDate" ErrorMessage="Please Select the Delivery  Date" ValidationGroup="a">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="DateCustomValidate"
                                    ControlToValidate="txtDeliveryDate" ErrorMessage="Please Enter the Delivery Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                    SetFocusOnError="True">*</asp:CustomValidator>
                            <%--<cc1:MaskedEditExtender ID="meeDeliveryDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtDeliveryDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="auto-style4">
                            <asp:Label ID="lblVATCSTNo" runat="server" Text="C.S. Tax"></asp:Label></td>
                        <td style="text-align: left; " class="auto-style5">
                            <asp:TextBox ID="txtCST" runat="server">
                            </asp:TextBox><asp:Label ID="Label32" runat="server" Text="%"></asp:Label><asp:Label ID="Label14" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                            </asp:Label><asp:RequiredFieldValidator ID="rfvCST" runat="server" ControlToValidate="txtCST"
                                ErrorMessage="Please Enter the CS Tax" ValidationGroup="a">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtCST"
                                    ValidChars=".0123456789">
                                </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right" class="auto-style4">
                            <asp:Label ID="Label8" runat="server" Text="Order Taken In Hs/Trade"></asp:Label></td>
                        <td style="text-align: left; " class="auto-style6">
                            <asp:TextBox ID="txtRoadPermit" runat="server"></asp:TextBox><asp:Label ID="Label18" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                            </asp:Label><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRoadPermit"
                                ErrorMessage="Please Enter the Road Permit" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label7" runat="server" Text="Frieght"></asp:Label></td>
                        <td style="text-align: left; width: 228px;">
                            <asp:TextBox ID="txtFrieght" runat="server"></asp:TextBox><asp:Label ID="Label17" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                            </asp:Label>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFrieght" ErrorMessage="Please Enter the Frieght" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left; width: 289px;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label3" runat="server" Text="Packing & Forwarding Instructions" Width="138px"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtPackingInstrs" runat="server" CssClass="multilinetext" EnableTheming="False" TextMode="MultiLine" Width="92%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label4" runat="server" Text="Accessories "></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtAccessories" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="92%" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="Label5" runat="server" Text="Extra spares "></asp:Label>
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <asp:TextBox ID="txtExtraSpace" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="92%" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label52" runat="server" Text="Advance Amt"></asp:Label></td>
                        <td colspan="3" style="height: 19px; text-align: left">
                            <asp:TextBox ID="txtAdvanceAmt" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label22" runat="server" Text="Attachment"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                <ContentTemplate>
                                    <asp:FileUpload ID="FileUpload1" runat="server" Font-Size="8pt" Font-Names="Verdana" Width="510px" Font-Italic="False"></asp:FileUpload>
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
                            <asp:LinkButton ID="lbtnAttachedFiles" runat="server" OnClick="lbtnAttachedFiles_Click"></asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">Reference Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left; width: 228px;"></td>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left; width: 289px;"></td>
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
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left; width: 289px;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;"></td>
                        <td style="text-align: left; height: 19px; width: 228px;"></td>
                        <td style="text-align: right; height: 19px;"></td>
                        <td style="text-align: left; height: 19px; width: 289px;"></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 47px">
                            <table id="tblButtons" align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="a" /></td>
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
                                            Text="Send" /></td>
                                    <td>
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" CausesValidation="False"
                                            OnClick="btnPrint_Click" /></td>
                                    <td style="width: 52px">
                                        <asp:Button ID="btnClose" runat="server" Text="Close" CausesValidation="False" OnClick="btnClose_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnReserve" runat="server" Text="Reserve" CausesValidation="False"
                                            OnClick="btnReserve_Click" Visible="False" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label>
                            <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label>
                            <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblRespId" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
                            
                        </td>
                        
                    </tr>
                </table>
                <table style="width: 189px" id="tblHiddenFields" runat="server" visible="false">
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnNew" runat="server" Text="New" CausesValidation="False" OnClick="btnNew_Click" Visible="False" /></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
                <asp:Panel ID="Panel1" runat="server" meta:resourcekey="Panel1Resource1">
                    <table>
                        <tr>
                            <td style="width: 100px"></td>
                        </tr>
                    </table>
                    <table id="tblPopUp1" runat="server" border="0" cellpadding="0" cellspacing="0" style="font-size: 8pt; background-image: url(Images/ConfirmBox2.PNG); background-repeat: repeat; font-family: Verdana"
                        visible="false">
                        <tr>
                            <td background="../../Images/ConfirmBox1Sa.PNG" style="height: 300px; text-align: left"
                                width="55"></td>
                            <td align="center" background="../../Images/ConfirmBox2sa.PNG" style="height: 300px"
                                valign="top">
                                <table>
                                    <tr>
                                        <td colspan="3" rowspan="1" style="width: 400px; height: 40px; text-align: left"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="width: 400px; text-align: left">
                                            <asp:Label ID="lblMessage" runat="server" meta:resourcekey="lblMessageResource1"
                                                Text="Reserve Stock Position."></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="width: 400px; text-align: left">
                                            <asp:Label ID="lblData" runat="server" meta:resourcekey="lblDataResource1"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" rowspan="1" style="width: 400px; text-align: left">
                                            <asp:Label ID="lblDo" runat="server" meta:resourcekey="lblDoResource1" Text="What Should I Do?">
                                            </asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="width: 400px; text-align: center">
                                            <asp:Button ID="btnConfirmYes" runat="server" CausesValidation="False" EnableTheming="False"
                                                Font-Names="Verdana" Font-Size="8pt" Height="23px" meta:resourcekey="btnConfirmYesResource1"
                                                Text="Reserve" Width="80px" OnClick="btnConfirmYes_Click" />
                                            &nbsp;
                                            <asp:Button ID="btnConfirmNo" runat="server" CausesValidation="False" EnableTheming="False"
                                                Font-Names="Verdana" Font-Size="8pt" Height="23px" meta:resourcekey="btnConfirmNoResource1"
                                                Text="Cancel" Width="80px" OnClick="btnConfirmNo_Click" /></td>
                                    </tr>
                                </table>
                                &nbsp; &nbsp; &nbsp;
                            </td>
                            <td background="../../Images/ConfirmBox3sa.PNG" style="width: 27px; height: 300px"></td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:ValidationSummary ID="vs1" runat="server" ShowMessageBox="True" ShowSummary="False" />
                <asp:ValidationSummary ID="vs2" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="a" />

                <asp:Label ID="Label6" runat="server" Text="Expected Date" Visible="False"></asp:Label><asp:TextBox ID="txtInspectionDate" runat="server" CssClass="datetext" EnableTheming="False" Visible="False"></asp:TextBox><asp:Image ID="imgInspectionDate" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False" /><asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="DateCustomValidate"
                    ControlToValidate="txtInspectionDate" ErrorMessage="Please Enter the Inspection Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                    SetFocusOnError="True" Visible="False">*</asp:CustomValidator><cc1:CalendarExtender
                        Format="dd/MM/yyyy" ID="ceInspectionDate" runat="server" PopupButtonID="imgInspectionDate" TargetControlID="txtInspectionDate">
                    </cc1:CalendarExtender>
                <asp:Label ID="Label9" runat="server" Text="Insurance" Visible="False"></asp:Label><asp:TextBox ID="txtInsurance" runat="server" Visible="False" Width="146px"></asp:TextBox><asp:Label ID="Label19" runat="server" EnableTheming="False" ForeColor="Red" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtInsurance"
                    ErrorMessage="Please Enter the Insurance" Visible="False">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="meeeInspectionDate" runat="server" DisplayMoney="Left"
                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtInspectionDate" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
                <cc1:ModalPopupExtender
                    ID="ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="False"
                    PopupControlID="Panel1" RepositionMode="None" TargetControlID="btnReserve">
                </cc1:ModalPopupExtender>
            </td>
        </tr>
    </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    
</asp:Content>



 
