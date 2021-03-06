<%@ Page Language="C#" MasterPageFile="~/MPs/InventoryMP1.master" AutoEventWireup="true" CodeFile="SampleDc2.aspx.cs"
    Inherits="Modules_Inventory_SampleDc" Title="|| Value Appp : Inventory : Sample Dc ||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                function DeliveryItemsCheck() {
                    var ReqQty, BalQty;
                    ReqQty = document.getElementById('<%=txtItemQuantity.ClientID%>').value;
                    BalQty = document.getElementById('<%=txtBalanceQtyHidden.ClientID%>').value;
                    document.getElementById('<%=txtBalanceQty.ClientID%>').value = BalQty - ReqQty;
                    qtyinhandItemsCheck();
                    resqtyinhandItemsCheck();
                }



                function qtyinhandItemsCheck() {
                    var iReqQty, iBalQty;
                    iReqQty = document.getElementById('<%=txtQtyInHand.ClientID%>').value;
            iBalQty = document.getElementById('<%=txtItemQuantity.ClientID%>').value;
            document.getElementById('<%=txtInhand.ClientID%>').value = iReqQty - iBalQty;
        }

        function resqtyinhandItemsCheck() {
            var resiReqQty, resiBalQty;
            resiReqQty = document.getElementById('<%=txtOrderedQty.ClientID%>').value;
    resiBalQty = document.getElementById('<%=txtItemQuantity.ClientID%>').value;
    document.getElementById('<%=txtresqty.ClientID%>').value = resiReqQty - resiBalQty;
}

function Serialno() {
    if (document.getElementById('<%=txtSerialNo.ClientID %>').value != "") {
        document.getElementById('<%=txtItemQuantity.ClientID %>').value = "1";
        document.getElementById('<%=txtItemQuantity.ClientID %>').readOnly = true;
    }
    else {
        document.getElementById('<%=txtItemQuantity.ClientID %>').value = "";
        document.getElementById('<%=txtItemQuantity.ClientID %>').readOnly = false;
    }
}
            </script>
            <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
                <tr>
                    <td>Sample/Cash DC</td>
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
                        <table id="TABLE2" border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="text-align: left">
                                </td>
                                <td style="width: 23px;"></td>
                                <td style="text-align: right">
                                    <table border="0" cellpadding="0" cellspacing="0" align="right">
                                        <tr>
                                            <td rowspan="1" style="height: 25px">
                                                <asp:Label ID="lblSearchBy" runat="server" CssClass="label" EnableTheming="False"
                                                    Font-Bold="True" Text="Search By" Width="71px"></asp:Label></td>
                                            <td rowspan="1" style="height: 25px">
                                                <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                                    OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">--</asp:ListItem>
                                                    <asp:ListItem Value="CUST_NAME">Customer</asp:ListItem>
                                                    <asp:ListItem Value="ITEM_MODEL_NO">Model No</asp:ListItem>
                                                    <asp:ListItem Value="DC_NO">DcNo</asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td rowspan="1" style="height: 25px"></td>
                                            <td rowspan="1" style="height: 25px"></td>
                                            <td rowspan="1" style="height: 25px">&nbsp;&nbsp;
                                            </td>
                                            <td rowspan="1" style="height: 25px"></td>
                                            <td rowspan="1" style="height: 25px">
                                                <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                                </asp:TextBox>
                                                &nbsp;
                                            </td>
                                            <td rowspan="1" style="height: 25px">
                                                <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                                    CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                                        </tr>
                                    </table>
                                    <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1" Visible="False">

                                    </asp:Label>

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gvDeliveryChallanDetails" SelectedRowStyle-BackColor="#c0c0c0" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            DataSourceID="SqlDataSource1"
                            Width="100%" AllowSorting="True">
                            <Columns>
                                <asp:BoundField DataField="DC_ID" SortExpression="DC_ID" HeaderText="DC Id"></asp:BoundField>
                                <asp:TemplateField HeaderText="Dc_No" SortExpression="Dc_No">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDc" OnClick="lbtnDc_Click" ForeColor="#0066ff" runat="server" Text='<%# Bind("DC_NO") %>' CausesValidation="False" __designer:wfdid="w14"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HtmlEncode="False" SortExpression="DC_DATE" DataFormatString="{0:dd/MM/yyyy}" DataField="DC_DATE" HeaderText="Dc Date"></asp:BoundField>
                                <asp:BoundField DataField="DC_TYPE" SortExpression="DC_TYPE" HeaderText="Dc Type"></asp:BoundField>
                                <asp:BoundField DataField="cust_name" SortExpression="cust_name" HeaderText="DC Customer"></asp:BoundField>
                                <asp:BoundField DataField="EMP_FIRST_NAME" SortExpression="EMP_FIRST_NAME" HeaderText="Prepared By"></asp:BoundField>
                                <asp:BoundField DataField="STATUS" SortExpression="STATUS" HeaderText="Status"></asp:BoundField>
                                <asp:BoundField DataField="company" SortExpression="company" HeaderText="Company"></asp:BoundField>
                            </Columns>
                            <EmptyDataTemplate>
                                No Data Exist!
                    
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                            SelectCommand="SP_INVENTORY_CASHDELIVERY_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
                                <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
                            </SelectParameters>
                        </asp:SqlDataSource>
                        &nbsp;
                <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="width: 1242px"></td>
                </tr>
                <tr>
                    <td colspan="4" style="width: 1242px; height: 49px;">
                        <table id="Table1" align="center">
                            <tr>
                                <td>
                                    <asp:Button ID="btnNew" runat="server" Text="New" CausesValidation="False" OnClick="btnNew_Click" /></td>
                                <td>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CausesValidation="False" OnClick="btnEdit_Click" /></td>
                                <td>
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CausesValidation="False"
                                        OnClick="btnDelete_Click" /></td>
                                <td>
                                    <asp:Button ID="btnAssign" runat="server" CausesValidation="False"
                                        Visible="false" Text="Assign" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="tblAssignTasks" runat="server" border="0" cellpadding="0" cellspacing="0"
                            visible="false" align="center">
                            <tr>
                                <td></td>
                                <td style="text-align: left; width: 388px;"></td>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label19" runat="server" CssClass="label" Font-Bold="False" Text="Assign Task No">
                                    </asp:Label></td>
                                <td style="text-align: left; width: 388px;">
                                    <asp:TextBox ID="txtAssignTaskNo" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 24px;">&nbsp;<asp:Label ID="lblDc" runat="server" Text="Delivery Challan No"></asp:Label></td>
                                <td style="text-align: left; height: 24px; width: 388px;">
                                    <asp:TextBox ID="txtDeliveryNoForAssign" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                                <td style="text-align: right; height: 24px;">&nbsp;
                            <asp:Label ID="Label10" runat="server" Text="Delivery Challan Date"></asp:Label></td>
                                <td style="text-align: left; height: 24px;">
                                    <asp:TextBox ID="txtDeliveryDateForAssign" runat="server" ReadOnly="True"></asp:TextBox>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label15" runat="server" CssClass="label" Font-Bold="False" Text="Customer Name">
                                    </asp:Label></td>
                                <td style="text-align: left; width: 388px;">
                                    <asp:TextBox ID="txtCustomerNameForAssingn" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label16" runat="server" CssClass="label" Font-Bold="False" Text="Contact E-Mail">
                                    </asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtCustomerEmailForAssingn" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label17" runat="server" CssClass="label" Font-Bold="False" Text="Employee Name">
                                    </asp:Label></td>
                                <td style="text-align: left; width: 388px;">
                                    <asp:DropDownList ID="ddlEmpNameForAssign" runat="server" AutoPostBack="True">
                                    </asp:DropDownList><asp:Label ID="Label35" runat="server" EnableTheming="False" ForeColor="Red"
                                        Text="*"> </asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlEmpNameForAssign"
                                        ErrorMessage="Please Select the Employee Name" InitialValue="0" ValidationGroup="assign">*</asp:RequiredFieldValidator></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label18" runat="server" CssClass="label" Font-Bold="False" Text="Employee EMail">
                                    </asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtEmpEmailId" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 39px;">
                                    <asp:Label ID="Label28" runat="server" CssClass="label" Font-Bold="False" Text="Remarks">
                                    </asp:Label></td>
                                <td colspan="3" style="text-align: left; height: 39px;">
                                    <asp:TextBox ID="txtRemarksForAssingn" runat="server" CssClass="multilinetext" EnableTheming="False"
                                        TextMode="MultiLine" Width="83%">
                                    </asp:TextBox><asp:Label ID="Label5" runat="server" EnableTheming="False" ForeColor="Red"
                                        Text="*"> </asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtRemarksForAssingn"
                                        ErrorMessage="Please Enter Remarks" ValidationGroup="assign">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label9" runat="server" CssClass="label" Font-Bold="False" Text="Assign Date">
                                    </asp:Label></td>
                                <td style="text-align: left; width: 388px;">
                                    <asp:TextBox ID="txtAssignDate" runat="server" CssClass="datetext" type="date" EnableTheming="False"
                                        Width="130px">
                                    </asp:TextBox><asp:Label ID="Label20" runat="server" EnableTheming="False" ForeColor="Red"
                                        Text="*"> </asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtAssignDate"
                                        ErrorMessage="Please Select Assign Date" ValidationGroup="assign">*</asp:RequiredFieldValidator>
                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="DateCustomValidate"
                                        ControlToValidate="txtAssignDate" ErrorMessage="Please Enter the Assign Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                        SetFocusOnError="True">*</asp:CustomValidator>
                                    &nbsp;
                                </td>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label26" runat="server" CssClass="label" Font-Bold="False" Text="Due Date">
                                    </asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtDueDate" runat="server" CssClass="datetext" type="date" EnableTheming="False"
                                        Width="130px">
                                    </asp:TextBox><asp:Label ID="Label21" runat="server" EnableTheming="False" ForeColor="Red"
                                        Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                            ControlToValidate="txtDueDate" ErrorMessage="Please Select Due Date">*</asp:RequiredFieldValidator><asp:CompareValidator
                                                ID="CompareValidator1" runat="server" ControlToCompare="txtAssignDate" ControlToValidate="txtDueDate"
                                                ErrorMessage="Due Date should not be less than Assign Date" Operator="GreaterThanEqual"
                                                SetFocusOnError="True" Type="Date" ValidationGroup="assign">*</asp:CompareValidator><asp:CustomValidator
                                                    ID="CustomValidator2" runat="server" ClientValidationFunction="DateCustomValidate"
                                                    ControlToValidate="txtDueDate" ErrorMessage="Please Enter the Due Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                                    SetFocusOnError="True">*</asp:CustomValidator>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center;" colspan="4">
                                    <table id="Table3">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnAssignTask" runat="server" Text="Assign"
                                                    ValidationGroup="assign" /></td>
                                            <td>
                                                <asp:Button ID="btnCloseAssignTask" runat="server" CausesValidation="False"
                                                    Text="Close" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table border="0" cellpadding="0" cellspacing="0" id="tblDCDetails" runat="server"
                            visible="true" width="800">
                            <tr>
                                <td colspan="4" style="text-align: left; height: 21px;" class="profilehead">General Details
                                    <asp:Label ID="lblDCID" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 19px; text-align: right"></td>
                                <td style="height: 19px; text-align: left;"></td>
                                <td style="height: 19px; text-align: right"></td>
                                <td style="height: 19px; text-align: left"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label41" runat="server" Text="DC For" Width="103px"></asp:Label></td>
                                <td style="text-align: left;">&nbsp;<asp:RadioButton ID="rdbSample" runat="server" Text="Sample" GroupName="a" OnCheckedChanged="rdbSample_CheckedChanged"></asp:RadioButton><asp:RadioButton ID="rdbCash" runat="server" Text="Cash" GroupName="a" Checked="True"></asp:RadioButton>
                                </td>
                                <td style="text-align: right;"></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtAdvanceAmount" runat="server" Visible="False">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label51" runat="server" Text="Search Customer" Width="117px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="TextBox1" runat="server">
                                    </asp:TextBox><asp:Button ID="Button1" runat="server" BorderStyle="None" CausesValidation="False"
                                        CssClass="gobutton" EnableTheming="False" OnClick="btnSearchModelNo_Click" Text="Go" />
                                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                        SelectCommand="SP_Customer_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="TextBox1"></asp:ControlParameter>
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    &nbsp;</td>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label49" runat="server" Text="Customer Name"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomerName_SelectedIndexChanged">
                                    </asp:DropDownList></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label50" runat="server" Text="Order Date"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtCustOrderDate" runat="server" type="date" CssClass="datetext"
                                        EnableTheming="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblCustomer" runat="server" Text="Customer Name"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtCustomerName" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblRegion" runat="server" Text="Region"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtRegion" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" ReadOnly="True" Height="41px"></asp:TextBox></td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblPhone" runat="server" Text="Phone"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtPhone" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblMobile" runat="server" Text="Mobile"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">Customer PO&#39;s :</td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlCustomerPo" runat="server" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: right;"></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtTotalAmount" runat="server" Visible="False"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="height: 22px; text-align: right">
                                    <asp:Label ID="Label52" runat="server" Text="Unit Name"></asp:Label></td>
                                <td style="height: 22px; text-align: left">
                                    <asp:DropDownList ID="ddlUnitName" runat="server" OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList></td>
                                <td style="height: 22px; text-align: right">
                                    <asp:Label ID="Label53" runat="server" Text="Unit Address"></asp:Label></td>
                                <td style="height: 22px; text-align: left">
                                    <asp:TextBox ID="txtunitaddress" runat="server" TextMode="MultiLine">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="profilehead" colspan="4" style="text-align: left;">Delivered Items</td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                    <asp:GridView ID="gvDeliveryChallanItems" runat="server" AutoGenerateColumns="False"
                                        Width="100%" OnRowDataBound="gvDeliveryChallanItems_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="DCNo" HeaderText="DC No."></asp:BoundField>
                                            <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="DCDate" HeaderText="DC Date"></asp:BoundField>
                                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                            <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                                            <asp:BoundField DataField="ItemName" HeaderText="Item Type"></asp:BoundField>
                                            <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                            <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                            <asp:BoundField DataField="DcId" HeaderText="DcIdHidden"></asp:BoundField>
                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
                                            <asp:BoundField DataField="DetId" HeaderText="DetId"></asp:BoundField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnDelete" CausesValidation="false" ForeColor="Blue" runat="server" __designer:wfdid="w5" OnClick="lbtnDelete_Click">Delete</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ColorId" HeaderText="ColorId"></asp:BoundField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <span style="font-size: 8pt; color: #ff0033; font-family: Verdana">No Items Delivered</span>

                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td class="profilehead" colspan="4" style="text-align: left">Delivery Challan Details</td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label11" runat="server" Text="Delivery Challan Type" Width="148px"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlDCType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDCType_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--</asp:ListItem>
                                        <asp:ListItem>Returnable</asp:ListItem>
                                        <asp:ListItem>Non Returnable</asp:ListItem>
                                    </asp:DropDownList><asp:Label ID="Label25" runat="server" EnableTheming="False" ForeColor="Red"
                                        Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                            ControlToValidate="ddlDCType" ErrorMessage="Please Select the Delivery Challan Type"
                                            InitialValue="0">*</asp:RequiredFieldValidator></td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblInwardDate" runat="server" Text="Inward Date" Visible="False"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtInwardDate" runat="server" CssClass="datetext" type="date" EnableTheming="False"
                                        Visible="False"></asp:TextBox><asp:Label ID="lblInwardDateValInd" runat="server" EnableTheming="False"
                                            ForeColor="Red" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator
                                                ID="rfvInwardDate" runat="server" ControlToValidate="txtInwardDate" ErrorMessage="Please Select the Inward Date"
                                                Enabled="False">*</asp:RequiredFieldValidator><asp:CustomValidator ID="custValInwardDate"
                                                    runat="server" ClientValidationFunction="DateCustomValidate" ControlToValidate="txtInwardDate"
                                                    ErrorMessage="Please Enter the Inward Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                                    SetFocusOnError="True" Enabled="False">*</asp:CustomValidator>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label3" runat="server" Text="Delivery Challan No" Width="140px"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtDeliveryChallanNo" runat="server" ReadOnly="True">
                                    </asp:TextBox><asp:Label ID="Label29" runat="server" EnableTheming="False" ForeColor="Red"
                                        Text="*"> </asp:Label><asp:RequiredFieldValidator ID="rfvDCNo" runat="server" ControlToValidate="txtDeliveryChallanNo"
                                            ErrorMessage="Please Enter the Delivery Challan No">*</asp:RequiredFieldValidator></td>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label6" runat="server" Text="Delivery Challan Date"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtDeliveryChallanDate" runat="server" type="date" CssClass="datetext" EnableTheming="False">
                                    </asp:TextBox><asp:Label ID="Label24" runat="server" EnableTheming="False" ForeColor="Red"
                                        Text="*"> </asp:Label><asp:RequiredFieldValidator ID="rfvDcDate" runat="server" ControlToValidate="txtDeliveryChallanDate"
                                            ErrorMessage="Please Select the Delivery Challan Date">*</asp:RequiredFieldValidator><asp:CustomValidator
                                                ID="CustomValidator3" runat="server" ClientValidationFunction="DateCustomValidate"
                                                ControlToValidate="txtDeliveryChallanDate" ErrorMessage="Please Enter the Delivery Challan Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                                SetFocusOnError="True">*</asp:CustomValidator>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label8" runat="server" Text="Transporter  Name" Width="126px"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlTransPorterName" runat="server">
                                    </asp:DropDownList><asp:Label ID="Label31" runat="server" EnableTheming="False" ForeColor="Red"
                                        Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                            ControlToValidate="ddlTransPorterName" ErrorMessage="Please Select theTransporter Name"
                                            InitialValue="0">*</asp:RequiredFieldValidator></td>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label33" runat="server" Text="Despatch Mode"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlDespatchMode" runat="server">
                                    </asp:DropDownList><asp:Label ID="Label39" runat="server" EnableTheming="False" ForeColor="Red"
                                        Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                                            ControlToValidate="ddlDespatchMode" ErrorMessage="Please Select the Despatch Mode"
                                            InitialValue="0">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label32" runat="server" Text="DC No:" Width="55px"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtLRNo" runat="server"></asp:TextBox></td>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label36" runat="server" Text="LR Date" Width="80px"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtLRDate" runat="server" CssClass="datetext" type="date" EnableTheming="False"></asp:TextBox>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblRevisedFrom" runat="server" Text="Revised From" Width="98px"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtRevisedFrom" runat="server" ReadOnly="True"></asp:TextBox></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label48" runat="server" Text="Company Name"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlCompany1" runat="server" Enabled="False">
                                    </asp:DropDownList>
                                    <asp:Label ID="Label44" runat="server" EnableTheming="False" ForeColor="Red"
                                        Text="*">
                                    </asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="ddlCompany1" ErrorMessage="Please Select the CompanyName"
                                        InitialValue="0">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: left" class="profilehead">Items Details</td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label4" runat="server" Text="Search By Brand :"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
                                    </asp:DropDownList></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label38" runat="server" Text="Search By ModelNo :" Width="135px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtSearchModel" runat="server">
                                    </asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server"
                                        ControlToValidate="txtSearchModel" ErrorMessage="Please Enter ModelNo For Search"
                                        ValidationGroup="Search">*</asp:RequiredFieldValidator><asp:Button ID="btnSearchModelNo"
                                            runat="server" BorderStyle="None" CausesValidation="False" CssClass="gobutton"
                                            EnableTheming="False" OnClick="btnSearchModelNo_Click1" Text="Go" ValidationGroup="Search" /></td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 22px;">
                                    <asp:Label ID="Label1" runat="server" Text="Model No :"></asp:Label></td>
                                <td style="text-align: left; height: 22px;">
                                    <asp:DropDownList ID="ddlModelNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModelNo_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:Label ID="Label34" runat="server" EnableTheming="False" ForeColor="Red"
                                        Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="ddlModelNo" ErrorMessage="Please Select the Model No" InitialValue="0"
                                            ValidationGroup="ip">*</asp:RequiredFieldValidator>
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                        SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:ControlParameter PropertyName="SelectedValue" Type="Int64" DefaultValue="0" Name="BranbId" ControlID="ddlBrand"></asp:ControlParameter>
                                            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtSearchModel"></asp:ControlParameter>
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                                <td style="text-align: right; height: 22px;">
                                    <asp:Label ID="Label46" runat="server" Text="Status :"></asp:Label></td>
                                <td style="text-align: left; height: 22px;">
                                    <asp:DropDownList ID="ddlStatus" runat="server">
                                        <asp:ListItem Value="0">Half-Part</asp:ListItem>
                                        <asp:ListItem Value="1">Full-Part</asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label7" runat="server" Text="DC Description :"></asp:Label></td>
                                <td style="text-align: left;" colspan="3">
                                    <asp:TextBox ID="txtRemarks" runat="server" EnableTheming="False" Height="69px" TextMode="MultiLine"
                                        Width="805px" MaxLength="300"></asp:TextBox>&nbsp;<asp:Label ID="Label13" runat="server" EnableTheming="False" ForeColor="Red"
                                            Text="*"> </asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                        ControlToValidate="txtRemarks" ErrorMessage="Please Enter the Remarks"
                                        ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label12" runat="server" Text="Description :"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtDescription" runat="server" EnableTheming="False" Height="47px"
                                        TextMode="MultiLine" Width="384px">
                                    </asp:TextBox></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label2" runat="server" Text="Item Name :" Width="76px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtItemName" runat="server">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label47" runat="server" Text="Company :"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" meta:resourcekey="ddlCompanyResource1"
                                        OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                    </asp:DropDownList></td>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label58" runat="server" Text="Item SubCategory :"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtItemSubCategory" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label59" runat="server" Text="Color :"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlColor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlColor_SelectedIndexChanged">
                                    </asp:DropDownList></td>

                                <td style="text-align: right">
                                    <asp:Label ID="Label60" runat="server" Text="Brand :"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtBrand" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label45" runat="server" Text="Godown :"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddllocation" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlDataSource22" DataTextField="whname" DataValueField="wh_id" OnSelectedIndexChanged="ddllocation_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource22" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="USP_Warehouse_Loc_Select" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="lblCompany" DefaultValue="0" Name="LocID" PropertyName="Text" Type="String" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <asp:Label ID="lblCompany" runat="server" Visible="False"></asp:Label>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label40" runat="server" Text="Qty In Hand :"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtQtyInHand" runat="server" ReadOnly="True"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label27" runat="server" Text="UOM :"></asp:Label>&nbsp;</td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtItemUOM" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label22" runat="server" Text="Quantity :" Width="57px"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtItemQuantity" runat="server">
                                    </asp:TextBox><asp:Label ID="Label37" runat="server" EnableTheming="False" ForeColor="Red"
                                        Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                            ControlToValidate="txtItemQuantity" ErrorMessage="Please Enter the Quantity"
                                            ValidationGroup="ip">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                                ID="ftxtQuantity" runat="server" FilterType="Numbers" TargetControlID="txtItemQuantity">
                                            </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label30" runat="server" Text="ItemCategory :"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtItemCategory" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label23" runat="server" Text="Balance Quantity :" Visible="False"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtBalanceQty" runat="server" ReadOnly="True" Visible="False"></asp:TextBox><asp:TextBox ID="txtBalanceQtyHidden" runat="server" ReadOnly="True" Visible="False"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 24px;">
                                    <asp:Label ID="Label14" runat="server" Text="Ordered Quantity :" Visible="False"></asp:Label></td>
                                <td style="text-align: left; height: 24px;">
                                    <asp:TextBox ID="txtOrderedQty" runat="server" Visible="False"></asp:TextBox></td>
                                <td style="text-align: right; height: 24px;"></td>
                                <td style="text-align: left; height: 24px;">
                                    <asp:TextBox ID="txtInhand" runat="server" Visible="False"></asp:TextBox><asp:TextBox ID="txtresqty" runat="server" Visible="False"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label54" runat="server" Text="Remarks :"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtremarks2" runat="server" EnableTheming="False">-</asp:TextBox></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label42" runat="server" Text="Serial No :" Visible="False"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtSerialNo" runat="server" Visible="False"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td  colspan="4">
                            <table style="width:100%">
                                <tr>
                                    <td colspan="2" style="text-align: right">
                                        <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                           CssClass="loginbutton"  EnableTheming="False" OnClick="btnAdd_Click" Text="Add"
                                            ValidationGroup="ip" /></td>
                                    <td colspan="2">
                                        <asp:Button ID="btnItemRefresh" runat="server" BackColor="Transparent" BorderStyle="None"
                                            CssClass="loginbutton" EnableTheming="False" OnClick="btnItemRefresh_Click" Text="Refresh"
                                            CausesValidation="False" /></td>
                                </tr>
                            </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right"></td>
                                <td style="text-align: right;"></td>
                                <td style="text-align: left"></td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td style="text-align: center" colspan="4">
                                    <asp:GridView ID="gvItmDetails" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvItmDetails_RowDeleting"
                                        Width="100%" OnRowDataBound="gvItmDetails_RowDataBound">
                                        <Columns>
                                            <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                            <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                            <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                                            <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                            <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                            <asp:BoundField DataField="ItemStatus" NullDisplayText="-" HeaderText="ItemStatusHidden"></asp:BoundField>
                                            <asp:BoundField DataField="SerialNo" NullDisplayText="-" HeaderText="Serial No."></asp:BoundField>
                                            <asp:BoundField DataField="Location" HeaderText="Location"></asp:BoundField>
                                            <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                                            <asp:BoundField DataField="ColorId" HeaderText="Color Id"></asp:BoundField>
                                            <asp:BoundField DataField="GodownId" HeaderText="GodownId"></asp:BoundField>
                                            <asp:BoundField DataField="InHand" HeaderText="In hand"></asp:BoundField>
                                            <asp:BoundField DataField="resqty" HeaderText="Res qty"></asp:BoundField>
                                            <asp:BoundField DataField="Status" HeaderText="Status"></asp:BoundField>
                                            <asp:BoundField DataField="StatusId" HeaderText="StatusId"></asp:BoundField>
                                            <asp:BoundField DataField="Companyid" HeaderText="Company"></asp:BoundField>
                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
                                            <asp:BoundField DataField="Remarks2" HeaderText="Remarks2"></asp:BoundField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <span style="color: #ff0033">No Data Exist</span>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                                    <tr><td>&nbsp;</td></tr>
                               <tr>

                        <td colspan="4" style="text-align: center">
                           <%-- <asp:UpdatePanel runat="server">
                                <ContentTemplate>--%>
                                    <table id="tblBlocked" runat="server" style="width: 100%">
                                        <tr>
                                            <td class="profilehead" colspan="4" style="text-align: left; height: 12px;">Blocked Items</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gvReservedStock" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDataBound="gvReservedStock_RowDataBound">
                                                    <Columns>
                                                        <%--<asp:BoundField DataField="Item_ID" HeaderText="Item_ID" />--%>

                                                        <asp:BoundField DataField="Item_Code" HeaderText="Item Code" />
                                                        <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="Model No" />
                                                        <%--<asp:BoundField DataField="PO_Id" HeaderText="PO No" />--%>
                                                        <%--<asp:BoundField DataField="dt_added" HeaderText="Blocked Date" />--%>
                                                        <asp:BoundField DataField="COLOUR_NAME" HeaderText="Color" />
                                                        <asp:BoundField DataField="Quantity" HeaderText="Blocked Quantity" />

                                                        <%-- <asp:TemplateField HeaderText="Total Reserved Stock">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQty" Text='<%# Bind("Total_Block_Stock") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                                        <asp:BoundField DataField="CUST_NAME" HeaderText="Customer Name" />
                                                        <asp:BoundField DataField="Delivery_Date" HeaderText="Delivery Date" />

                                                        <asp:TemplateField HeaderText="Quantity">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk" AutoPostBack="true" runat="server" OnCheckedChanged="chk_CheckedChanged"></asp:CheckBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="COLOUR_ID" HeaderText="COLOUR ID" />
                                                        <asp:BoundField DataField="Customer_Id" HeaderText="Customer Id" />

                                                    </Columns>
                                                </asp:GridView>
                                                <br />
                                                <asp:Button ID="btnReleaseBlock" runat="server" Visible="false" Text="Release Items" OnClick="btnReleaseBlock_Click" />
                                            </td>
                                        </tr>
                                    </table>
                               <%-- </ContentTemplate>
                            </asp:UpdatePanel>--%>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                           <%-- <asp:UpdatePanel runat="server">
                                <ContentTemplate>--%>
                                    <table id="tblReleasedItems" runat="server" visible="false">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gvReleasedItems" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDataBound="gvReleasedItems_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="Item_Code" HeaderText="Item Code" />
                                                        <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="Model No" />
                                                        <asp:BoundField DataField="COLOUR_NAME" HeaderText="Color" />
                                                        <asp:BoundField DataField="Quantity" HeaderText="Blocked Quantity" />
                                                        <asp:BoundField DataField="CUST_NAME" HeaderText="Customer Name" />
                                                        <asp:BoundField DataField="Delivery_Date" HeaderText="Delivery Date" />
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                               <%-- </ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                            <tr>
                                <td class="profilehead" colspan="4" style="text-align: left; height: 27px;">Reference Details</td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 19px;"></td>
                                <td style="text-align: left; height: 19px;"></td>
                                <td style="text-align: right; height: 19px;"></td>
                                <td style="text-align: left; height: 19px;"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                                    </asp:DropDownList></td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By" Visible="False"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlApprovedBy" runat="server" Enabled="False" Visible="False">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="height: 2px; text-align: right"></td>
                                <td style="height: 2px; text-align: left;"></td>
                                <td style="height: 2px; text-align: right"></td>
                                <td style="height: 2px; text-align: left;"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 19px;" colspan="4">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table id="tblButtons" align="center">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                            <td>
                                                <asp:Button ID="btnRevise" runat="server" Text="Modify" OnClick="btnRevise_Click"
                                                    Visible="False" /></td>
                                            <td></td>
                                            <td>
                                                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CausesValidation="False"
                                                    OnClick="btnRefresh_Click" /></td>
                                            <td>
                                                <asp:Button ID="btnClose" runat="server" Text="Close" CausesValidation="False" OnClick="btnClose_Click" /></td>
                                            <td>
                                                <asp:Button ID="btnPrint" runat="server" Text="Print" CausesValidation="False" OnClick="btnPrint_Click" />
                                                <asp:Button ID="btnSendSMS" runat="server" Text="Send SMS" OnClick="btnSendSMS_Click" Visible="False" />
                                            
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table id="tblpRint" runat="server" visible="false">
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkOriginal" runat="server" OnCheckedChanged="chkOriginal_CheckedChanged"
                                                    Text="Original" AutoPostBack="True"></asp:CheckBox></td>
                                            <td>
                                                <asp:CheckBox ID="chkDuplicate" runat="server" Text="Duplicate" AutoPostBack="True" OnCheckedChanged="chkDuplicate_CheckedChanged"></asp:CheckBox></td>
                                            <td>
                                                <asp:CheckBox ID="chktriplicate" runat="server" Text="Triplicate" AutoPostBack="True" OnCheckedChanged="chktriplicate_CheckedChanged"></asp:CheckBox></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="gvPreviousPayments" runat="server" AutoGenerateColumns="False" ShowFooter="True" Width="1005px" Visible="False">
                            <FooterStyle BackColor="#1AA8BE" Font-Bold="True" ForeColor="Black" />
                            <Columns>
                                <asp:BoundField DataField="PR_ID" HeaderText="PRIDHidden"></asp:BoundField>
                                <asp:BoundField DataField="PR_NO" HeaderText="Receipt No."></asp:BoundField>
                                <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="PR_DATE" HeaderText="Receipt Date"></asp:BoundField>
                                <asp:BoundField DataField="SO_NO" HeaderText="Purchase Order No"></asp:BoundField>
                                <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="SO_DATE" HeaderText="Purchase Date"></asp:BoundField>
                                <asp:BoundField DataField="PR_AMT_RECEIVED" HeaderText="Amount Received"></asp:BoundField>
                                <asp:BoundField DataField="PR_PAYMODE_TYPE" HeaderText="Pay Mode"></asp:BoundField>
                            </Columns>
                            <EmptyDataTemplate>
                                No Records Found<br />

                            </EmptyDataTemplate>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="height: 23px; width: 1242px;">&nbsp;<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="ip" ShowMessageBox="True" ShowSummary="False" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="width: 1242px">&nbsp;&nbsp;<asp:Button ID="btnForApproveHidden" runat="server" CausesValidation="False"
                        Text="for approve hidden" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender" runat="server" PopupControlID="Panel1"
                            RepositionMode="None" TargetControlID="btnForApproveHidden">
                        </cc1:ModalPopupExtender>
                        <cc1:FilteredTextBoxExtender
                            ID="ftxteLRNo" runat="server" FilterMode="InvalidChars" InvalidChars="!@#$%^&*()_+|=\./-{}[]:&quot;;'<>?,./"
                            TargetControlID="txtLRNo">
                        </cc1:FilteredTextBoxExtender>
                        <cc1:MaskedEditExtender ID="meeEnquiryDateForAssing" runat="server" DisplayMoney="Left"
                            Mask="99/99/9999" MaskType="Date" TargetControlID="txtDeliveryDateForAssign"
                            UserDateFormat="MonthDayYear">
                        </cc1:MaskedEditExtender>
                        <table border="0" cellpadding="0" cellspacing="0" id="Table4" runat="server"
                            visible="true" width="100%">
                            <tr>
                                <td style="text-align: left;">&nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
                                </td>
                            </tr>
                        </table>
                        <asp:RadioButton ID="rbSales" runat="server" AutoPostBack="True" GroupName="dc" Text="Sales" Visible="False" /><asp:RadioButton ID="rdbHighsale" runat="server" AutoPostBack="True" GroupName="dc" Text="HighSeaSale" Visible="False" /><asp:RadioButton ID="rdbCashaccount" runat="server" AutoPostBack="True" GroupName="dc" Text="Extra" Visible="False" /><asp:RadioButton ID="rbSpares" runat="server" AutoPostBack="True" GroupName="dc"
                            Text="Spares" Visible="False" /></td>
                </tr>
            </table>



        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


 
