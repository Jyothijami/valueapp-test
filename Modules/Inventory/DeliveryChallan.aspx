<%@ Page Language="C#" MasterPageFile="~/MPs/InventoryMP1.master" AutoEventWireup="true" Async="true"
    CodeFile="DeliveryChallan.aspx.cs" Inherits="Modules_CRM_DeliveryChallan" Title="|| Value App : Inventory : Delivery Challan ||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

     <script lang="javascript" type="text/javascript">
         function OpenPopupCenter(pageURL, title, w, h) {
             var left = (screen.width - w) / 2;
             var top = (screen.height - h) / 4;  // for 25% - devide by 4  |  for 33% - devide by 3
             var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
         }
    </script>

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
    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="pagehead">
        <tr>
            <td class="pagehead" align="right" style="text-align: left;">Delivery Challan
            </td>
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
            <td class="searchhead" colspan="4" style="text-align: left; width: 100%;">
                <table id="TABLE2" border="0" cellpadding="0" cellspacing="0"
                    width="100%">
                    <tr>
                        <td style="text-align: left">Delivery &nbsp;Challan
                        </td>
                        <td style="width: 23px"></td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSearchBy" runat="server" CssClass="label" EnableTheming="False"
                                            Font-Bold="True" Text="Search By" Width="71px"></asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="SO_NO">PO No</asp:ListItem>
                                            <asp:ListItem Value="DC_NO">DC No</asp:ListItem>
                                            <asp:ListItem Value="DC_DATE">DC Date</asp:ListItem>
                                            <asp:ListItem Value="FORSALESCUST">Customer Name</asp:ListItem>
                                            <asp:ListItem Value="DC_TYPE">DC Type</asp:ListItem>
                                            <asp:ListItem Value ="STATUS">Status</asp:ListItem>
                                            <%--<asp:ListItem Value="ITEM_MODEL_NO">Model NO</asp:ListItem>--%>
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
                                        </asp:TextBox><%--<asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchFrom" runat="server" Enabled="False"
                                            PopupButtonID="imgFromDate" TargetControlID="txtSearchValueFromDate">
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
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchValueToDate" runat="server"
                                            Enabled="False" PopupButtonID="imgToDate" TargetControlID="txtSearchText">
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

                            <asp:Label ID="lblUserName" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblUserId" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblCp_Ids" runat="server" Visible="false"></asp:Label>

                            <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label>
                            <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label><asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                            
                            <asp:Label ID="lblSearchItemHidden" Text="0" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueHidden" Text="0" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; width: 1242px;">
                <asp:GridView ID="gvDeliveryChallanDetails" SelectedRowStyle-BackColor="#c0c0c0" runat="server"
                    
                    AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gvDeliveryChallanDetails_PageIndexChanging"
                    OnRowDataBound="gvDeliveryChallanDetails_RowDataBound"
                    Width="100%" AllowSorting="True">
                    <Columns>
                        <asp:BoundField DataField="DC_ID" SortExpression="DC_ID" HeaderText="DCIdHidden"></asp:BoundField>
                        <asp:BoundField DataField="SO_ID" SortExpression="SO_ID" HeaderText="SONoHidden">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="SPO_ID" SortExpression="SPO_ID" HeaderText="SPONoHidden"></asp:BoundField>
                        <asp:TemplateField HeaderText="PO.NO" SortExpression="SO_NO">
                            <ItemStyle Width="300px" HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle Width="300px" HorizontalAlign="Left"></HeaderStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnSONo" OnClick="lbtnSONo_Click" ForeColor="#0066ff" runat="server" Text='<%# Bind("SO_NO") %>' CausesValidation="False" __designer:wfdid="w16"></asp:LinkButton>&nbsp; 
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SPO No" SortExpression="SPO_NO">
                            <ItemStyle Wrap="True"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnSPONo" OnClick="lbtnSPONo_Click" runat="server" Text='<%# Bind("SPO_NO") %>' CausesValidation="False" __designer:wfdid="w82"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DCNo" SortExpression="DC_NO">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("DC_NO") %>' __designer:wfdid="w6"></asp:TextBox>
                            </EditItemTemplate>

                            <ItemStyle Width="300px" HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle Width="300px" HorizontalAlign="Left"></HeaderStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDCNo" OnClick="lbtnDCNo_Click" runat="server" ForeColor="#0066ff" Text='<%# Bind("DC_NO") %>' CausesValidation="False" __designer:wfdid="w85"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HtmlEncode="False" SortExpression="DC_DATE" DataFormatString="{0:dd/MM/yyyy}" DataField="DC_DATE" HeaderText="DCDate"></asp:BoundField>
                        <asp:BoundField DataField="FORSALESCUST" SortExpression="FORSALESCUST" HeaderText="Customer">
                            <ItemStyle Width="300px" HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle Width="300px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="FORSPARESCUST" SortExpression="FORSPARESCUST" HeaderText="Customer">
                            <ItemStyle Width="200px" HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle Width="200px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="DC_Type" SortExpression="DC_Type" HeaderText="DCType">
                            <ItemStyle Width="200px"></ItemStyle>

                            <HeaderStyle Width="200px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="TRANS_LONG_NAME" SortExpression="TRANS_LONG_NAME" HeaderText="TransporterName">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PreparedBy" SortExpression="PreparedBy" HeaderText="PreparedBy"></asp:BoundField>
                        <asp:BoundField DataField="ApprovedBy" SortExpression="ApprovedBy" HeaderText="ApprovedBy"></asp:BoundField>
                        <asp:BoundField DataField="dc_for" SortExpression="dc_for" HeaderText="dc_forHidden"></asp:BoundField>
                        <asp:BoundField DataField="STATUS" SortExpression="STATUS" HeaderText="Status"></asp:BoundField>
                        <asp:BoundField DataField="COMPANY" SortExpression="COMPANY" HeaderText="COMPANY"></asp:BoundField>

                        <asp:TemplateField HeaderText="Documents" >
                                    <ItemTemplate >
                                        <span class="text-center">
                        <a runat="server" class="btn btn-icon btn-primary" href='<%# "~/Modules/Inventory/DCDoc1.aspx?Cid=" + Eval("DC_ID") %>' onclick="OpenPopupCenter(this.href, 'newwindow', 800, 500);return false; ">
                            <span style="color:white"><strong> Docs</strong> </span>
                                <asp:Label ID="Label2"  CssClass="label label-danger"  runat="server" Text='<%#Eval("DocCou") %>'></asp:Label>
                        </a>

                    </span>
                                    </ItemTemplate>
                                </asp:TemplateField>

                    </Columns>
                    <EmptyDataTemplate>
                        No Data Exist!
                    
                    </EmptyDataTemplate>
                </asp:GridView>
                <asp:SqlDataSource ID="sdsDeliveryChallanDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_INVENTORY_DELIVERY_SEARCH_SELECT_2" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="EMPID" ControlID="lblEmpIdHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>
                        <asp:ControlParameter ControlID="lblCPID" Name="CPID" DefaultValue="0" PropertyName="Text" Type="Int64" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="4" style="width: 1242px"></td>
        </tr>
        <tr>
            <td colspan="4" style="width: 100%">
                <table id="Table1" style="text-align: center">
                    <tr style="text-align: center">
                        <td>
                            <asp:Button ID="btnNew" runat="server" Text="New" CausesValidation="False" OnClick="btnNew_Click" />

                        </td>
                        <td>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" /></td>
                        <td>
                            <asp:Button ID="btnAssign" runat="server" CausesValidation="False" OnClick="btnAssign_Click"
                                Visible="false" Text="Assign" />

                        </td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CausesValidation="False" OnClick="btnDelete_Click" />

                        </td>

                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="width: 1242px">
                <table id="tblAssignTasks" runat="server" border="0" cellpadding="0" cellspacing="0"
                    visible="false" width="100%">
                    <tr>
                        <td></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="Label19" runat="server" CssClass="label" Font-Bold="False" Text="Assign Task No">
                            </asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtAssignTaskNo" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 24px;">&nbsp;<asp:Label ID="lblDc" runat="server" Text="Delivery Challan No"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
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
                        <td style="text-align: left">
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
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlEmpNameForAssign" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpNameForAssign_SelectedIndexChanged">
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
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtAssignDate" runat="server" CssClass="datetext" EnableTheming="False"
                                Width="130px">
                            </asp:TextBox><asp:Image ID="imgAssignDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><asp:Label ID="Label20" runat="server" EnableTheming="False" ForeColor="Red"
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
                            <asp:TextBox ID="txtDueDate" runat="server" CssClass="datetext" EnableTheming="False"
                                Width="130px">
                            </asp:TextBox><asp:Image ID="imgDueDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><asp:Label ID="Label21" runat="server" EnableTheming="False" ForeColor="Red"
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
                                        <asp:Button ID="btnAssignTask" runat="server" OnClick="btnAssignTask_Click" Text="Assign"
                                            ValidationGroup="assign" /></td>
                                    <td>
                                        <asp:Button ID="btnCloseAssignTask" runat="server" CausesValidation="False" OnClick="btnCancelTask_Click"
                                            Text="Close" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="width: 1242px">
                <table border="0" cellpadding="0" cellspacing="0" id="tblDCDetails" runat="server"
                    visible="true" width="100%">
                    <tr>
                        <td colspan="4" style="text-align: left; height: 21px;" class="profilehead">General Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right; width: 192px;"></td>
                        <td style="height: 19px; text-align: left; width: 362px;"></td>
                        <td style="height: 19px; text-align: right; width: 398px;"></td>
                        <td style="height: 19px; text-align: left; width: 332px;"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label41" runat="server" Text="DC For" Width="103px"></asp:Label></td>
                        <td style="text-align: left; width: 362px; height: 24px;">
                            <asp:RadioButton ID="rbSales" runat="server" AutoPostBack="True" GroupName="dc"
                                OnCheckedChanged="rbSales_CheckedChanged" Text="Sales" /></td>
                        <td style="text-align: right; height: 24px; width: 398px;"></td>
                        <td style="text-align: left; height: 24px; width: 332px;">
                            <asp:TextBox ID="txtAdvanceAmount" runat="server" Visible="False">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label50" runat="server" Text="Purchase Order Search" Width="169px"></asp:Label></td>
                        <td style="width: 362px; text-align: left">
                            <asp:TextBox ID="txtSearchModel" runat="server">
                            </asp:TextBox><asp:Button ID="btnSearchModelNo" runat="server" BorderStyle="None"
                                CausesValidation="False" CssClass="gobutton" EnableTheming="False" OnClick="btnSearchModelNo_Click"
                                Text="Go" ValidationGroup="Search" />
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                SelectCommand="SP_PONO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:Parameter Type="Int64" DefaultValue="0" Name="BranbId"></asp:Parameter>
                                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtSearchModel"></asp:ControlParameter>
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                        <td style="text-align: right; width: 398px;"></td>
                        <td style="text-align: left; width: 332px;"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblSalesOrderNo" runat="server" Text="Purchase Order No" Width="133px"></asp:Label></td>
                        <td style="text-align: left; width: 362px;">
                            <asp:DropDownList ID="ddlSalesOrderNo" runat="server" OnSelectedIndexChanged="ddlSalesOrderNo_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList><asp:Label ID="Label38" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label></td>
                        <td style="text-align: right; width: 398px;">
                            <asp:Label ID="lblSalesOrderDate" runat="server" Text="Purchase Order Date" Width="144px"></asp:Label></td>
                        <td style="text-align: left; width: 332px;">
                            <asp:TextBox ID="txtSalesOrderDate" runat="server" CssClass="datetext"
                                EnableTheming="False" Width="146px"></asp:TextBox><asp:Image ID="imgSalesOrderDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>&nbsp;
                            <cc1:CalendarExtender Format="dd/MM/yyyy" ID="CeSalesOrderDate" runat="server" Enabled="False"
                                PopupButtonID="imgSalesOrderDate" TargetControlID="txtSalesOrderDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MeeSalesOrderDate" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSalesOrderDate"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblCustomer" runat="server" Text="Customer Name"></asp:Label></td>
                        <td style="text-align: left; width: 362px;">
                            <asp:TextBox ID="txtCustomerName" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="text-align: right; width: 398px;">
                            <asp:Label ID="lblRegion" runat="server" Text="Region"></asp:Label></td>
                        <td style="text-align: left; width: 332px;">
                            <asp:TextBox ID="txtRegion" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label></td>
                        <td style="text-align: left; width: 362px; height: 49px;">
                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" ReadOnly="True" Height="41px" Width="150px"></asp:TextBox></td>
                        <td style="text-align: right; height: 49px; width: 398px;">
                            <asp:Label ID="lblPhone" runat="server" Text="Phone"></asp:Label></td>
                        <td style="text-align: left; height: 49px; width: 332px;">
                            <asp:TextBox ID="txtPhone" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
                        <td style="text-align: left; width: 362px;">
                            <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="text-align: right; width: 398px;">
                            <asp:Label ID="lblMobile" runat="server" Text="Mobile"></asp:Label></td>
                        <td style="text-align: left; width: 332px;">
                            <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 192px;"></td>
                        <td style="text-align: left; width: 362px;">
                            <asp:TextBox ID="txtTotalAmount" runat="server" Visible="False"></asp:TextBox></td>
                        <td style="text-align: right; width: 398px;"></td>
                        <td style="text-align: left; width: 332px;"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label51" runat="server" Text="Unit Name"></asp:Label></td>
                        <td style="width: 362px; text-align: left">
                            <asp:DropDownList ID="ddlUnitName" runat="server" AutoPostBack="True" EnableTheming="False" OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged" Width="200px">
                            </asp:DropDownList></td>
                        <td style="text-align: right; width: 398px;">
                            <asp:Label ID="Label53" runat="server" Text="Unit Address"></asp:Label></td>
                        <td style="text-align: left; width: 332px;">
                            <asp:TextBox ID="txtunitaddress" runat="server" TextMode="MultiLine" EnableTheming="False" Width="193px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            <asp:Label ID="lblOrderedItemsHeading" runat="server" EnableTheming="False"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:GridView ID="gvItemDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvItemDetails_RowDataBound"
                                Width="100%" OnRowDeleting="gvItemDetails_RowDeleting" OnRowEditing="gvItemDetails_RowEditing">
                                <Columns>
                                    <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                    <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                    <asp:BoundField DataField="Rate" HeaderText="MrpRate"></asp:BoundField>
                                    <asp:BoundField HeaderText="UnitPrice"></asp:BoundField>
                                    <asp:BoundField HeaderText="Amount"></asp:BoundField>
                                    <asp:BoundField DataField="DeliveryStatus" NullDisplayText="-" HeaderText="Status"></asp:BoundField>
                                    <asp:BoundField DataField="SODetId" HeaderText="SODetIdHidden"></asp:BoundField>
                                    <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                                    <asp:BoundField DataField="ColorId" HeaderText="Color Id"></asp:BoundField>
                                    <asp:BoundField DataField="Price" HeaderText="POPrice"></asp:BoundField>
                                    <asp:BoundField DataField="BalanceQty" HeaderText="BalanceQty"></asp:BoundField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <span style="color: #ff0033">No Data to Display</span>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left; height: 12px;">Delivered Items</td>
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
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                    <asp:BoundField DataField="DcId" HeaderText="DcIdHidden"></asp:BoundField>
                                    <asp:BoundField DataField="SubCategory" HeaderText="SubCategory"></asp:BoundField>
                                    <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                                    <asp:BoundField DataField="Godown" HeaderText="Godown"></asp:BoundField>
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
                                    <asp:BoundField DataField="Extra" HeaderText="Extra"></asp:BoundField>
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
                        <td style="text-align: right; width: 192px; height: 24px;">
                            <asp:Label ID="lblRevisedFrom" runat="server" Text="Revised From"></asp:Label></td>
                        <td style="text-align: left; width: 362px; height: 24px;">
                            <asp:TextBox ID="txtRevisedFrom" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right; width: 398px; height: 24px;"></td>
                        <td style="text-align: left; width: 332px; height: 24px;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 192px;">
                            <asp:Label ID="Label11" runat="server" Text="Delivery Challan Type"></asp:Label></td>
                        <td style="text-align: left; width: 362px;">
                            <asp:DropDownList ID="ddlDCType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDCType_SelectedIndexChanged">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem>Returnable</asp:ListItem>
                                <asp:ListItem>Non Returnable</asp:ListItem>
                            </asp:DropDownList><asp:Label ID="Label25" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                    ControlToValidate="ddlDCType" ErrorMessage="Please Select the Delivery Challan Type"
                                    InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right; width: 398px;">
                            <asp:Label ID="lblInwardDate" runat="server" Text="Inward Date" Visible="False"></asp:Label></td>
                        <td style="text-align: left; width: 332px;">
                            <asp:TextBox ID="txtInwardDate" runat="server" CssClass="datetext" EnableTheming="False"
                                Visible="False"></asp:TextBox><asp:Image ID="imgInwardDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                    Visible="False"></asp:Image><asp:Label ID="lblInwardDateValInd" runat="server" EnableTheming="False"
                                        ForeColor="Red" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator
                                            ID="rfvInwardDate" runat="server" ControlToValidate="txtInwardDate" ErrorMessage="Please Select the Inward Date"
                                            Enabled="False">*</asp:RequiredFieldValidator><asp:CustomValidator ID="custValInwardDate"
                                                runat="server" ClientValidationFunction="DateCustomValidate" ControlToValidate="txtInwardDate"
                                                ErrorMessage="Please Enter the Inward Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                                SetFocusOnError="True" Enabled="False">*</asp:CustomValidator>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 192px;">
                            <asp:Label ID="Label3" runat="server" Text="Delivery Challan No"></asp:Label></td>
                        <td style="text-align: left; width: 362px;">
                            <asp:TextBox ID="txtDeliveryChallanNo" runat="server" ReadOnly="True">
                            </asp:TextBox><asp:Label ID="Label29" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label><asp:RequiredFieldValidator ID="rfvDCNo" runat="server" ControlToValidate="txtDeliveryChallanNo"
                                    ErrorMessage="Please Enter the Delivery Challan No">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right; width: 398px;">
                            <asp:Label ID="Label6" runat="server" Text="Delivery Challan Date"></asp:Label></td>
                        <td style="text-align: left; width: 332px;">
                            <asp:TextBox ID="txtDeliveryChallanDate" runat="server" CssClass="datetext" EnableTheming="False">
                            </asp:TextBox><asp:Image ID="imgDeliveryChallanDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><asp:Label ID="Label24" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label><asp:RequiredFieldValidator ID="rfvDcDate" runat="server" ControlToValidate="txtDeliveryChallanDate"
                                    ErrorMessage="Please Select the Delivery Challan Date">*</asp:RequiredFieldValidator><asp:CustomValidator
                                        ID="CustomValidator3" runat="server" ClientValidationFunction="DateCustomValidate"
                                        ControlToValidate="txtDeliveryChallanDate" ErrorMessage="Please Enter the Delivery Challan Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                        SetFocusOnError="True">*</asp:CustomValidator>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 30px; width: 192px;">
                            <asp:Label ID="Label8" runat="server" Text="Transporter  Name"></asp:Label></td>
                        <td style="text-align: left; width: 362px; height: 30px;">
                            <asp:DropDownList ID="ddlTransPorterName" runat="server" Width="93px">
                            </asp:DropDownList><asp:Label ID="Label31" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                    ControlToValidate="ddlTransPorterName" ErrorMessage="Please Select theTransporter Name"
                                    InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right; height: 30px; width: 398px;">
                            <asp:Label ID="Label33" runat="server" Text="Despatch Mode"></asp:Label></td>
                        <td style="text-align: left; height: 30px; width: 332px;">
                            <asp:DropDownList ID="ddlDespatchMode" runat="server" Width="87px">
                            </asp:DropDownList><asp:Label ID="Label39" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                                    ControlToValidate="ddlDespatchMode" ErrorMessage="Please Select the Despatch Mode"
                                    InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 192px; height: 40px;">
                            <asp:Label ID="Label32" runat="server" Text="LR No"></asp:Label></td>
                        <td style="text-align: left; width: 362px; height: 40px;">
                            <asp:TextBox ID="txtLRNo" runat="server"></asp:TextBox></td>
                        <td style="text-align: right; width: 398px; height: 40px;">
                            <asp:Label ID="Label36" runat="server" Text="LR Date" Width="80px"></asp:Label></td>
                        <td style="text-align: left; width: 332px; height: 40px;">
                            <asp:TextBox ID="txtLRDate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox><asp:Image ID="imgLRDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 192px;"></td>
                        <td style="text-align: left; width: 362px;"></td>
                        <td style="text-align: right; width: 398px;">
                            <asp:Label ID="Label48" runat="server" Text="Company Name"></asp:Label></td>
                        <td style="text-align: left; width: 332px;">
                            <asp:DropDownList ID="ddlCompany1" runat="server">
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
                        <td style="height: 22px; text-align: right; width: 192px;">
                            <asp:Label ID="Label4" runat="server" Text="Search By Brand"></asp:Label></td>
                        <td style="width: 362px; height: 22px; text-align: left">
                            <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="height: 22px; text-align: right; width: 398px;"></td>
                        <td style="height: 22px; text-align: left; width: 332px;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 22px; width: 192px;">
                            <asp:Label ID="Label1" runat="server" Text="Model No"></asp:Label></td>
                        <td style="text-align: left; height: 22px; width: 362px;">
                            <asp:DropDownList ID="ddlModelNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModelNo_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label ID="Label34" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ControlToValidate="ddlModelNo" ErrorMessage="Please Select the Model No" InitialValue="0"
                                    ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right; height: 22px; width: 398px;">
                            <asp:Label ID="Label46" runat="server" Text="Status"></asp:Label></td>
                        <td style="text-align: left; height: 22px; width: 332px;">
                            <asp:DropDownList ID="ddlStatus" runat="server">
                                <asp:ListItem Value="0">Half-Part</asp:ListItem>
                                <asp:ListItem Value="1">Full-Part</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 192px;">
                            <asp:Label ID="Label7" runat="server" Text="Remarks"></asp:Label></td>
                        <td style="text-align: left;" colspan="3">
                            <asp:TextBox ID="txtRemarks" runat="server" EnableTheming="False" Height="69px" TextMode="MultiLine"
                                Width="805px" MaxLength="300"></asp:TextBox>&nbsp;<asp:Label ID="Label13" runat="server" EnableTheming="False" ForeColor="Red"
                                    Text="*"> </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                ControlToValidate="txtRemarks" ErrorMessage="Please Enter the Remarks"
                                ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 192px;">
                            <asp:Label ID="Label12" runat="server" Text="Description"></asp:Label></td>
                        <td style="width: 362px; text-align: left">
                            <asp:TextBox ID="txtDescription" runat="server" EnableTheming="False" Height="47px"
                                TextMode="MultiLine" Width="384px">
                            </asp:TextBox></td>
                        <td style="text-align: right; width: 398px;">
                            <asp:Label ID="Label2" runat="server" Text="Item Name" Width="76px"></asp:Label></td>
                        <td style="text-align: left; width: 332px;">
                            <asp:TextBox ID="txtItemName" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 24px; width: 192px;">
                            <asp:Label ID="Label59" runat="server" Text="Color :"></asp:Label></td>
                        <td style="text-align: left; height: 24px; width: 362px;">
                            <asp:DropDownList ID="ddlColor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlColor_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="text-align: right; height: 24px; width: 398px;">
                            <asp:Label ID="Label58" runat="server" Text="Item SubCategory :"></asp:Label></td>
                        <td style="text-align: left; height: 24px; width: 332px;">
                            <asp:TextBox ID="txtItemSubCategory" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 192px;">
                            <asp:Label ID="Label47" runat="server" Text="Company"></asp:Label></td>
                        <td style="text-align: left; width: 362px;">
                            <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" meta:resourcekey="ddlCompanyResource1"
                                OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="text-align: right; width: 398px;">
                            <asp:Label ID="Label60" runat="server" Text="Brand :"></asp:Label></td>
                        <td style="text-align: left; width: 332px;">
                            <asp:TextBox ID="txtBrand" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 192px;">
                            <asp:Label ID="Label45" runat="server" Text="Godown"></asp:Label></td>
                        <td style="text-align: left; width: 362px;">
                            <asp:DropDownList ID="ddllocation" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddllocation_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="text-align: right; width: 398px;">
                            <asp:Label ID="Label40" runat="server" Text="Qty In Hand"></asp:Label></td>
                        <td style="text-align: left; width: 332px;">
                            <asp:TextBox ID="txtQtyInHand" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 192px;">
                            <asp:Label ID="Label27" runat="server" Text="UOM"></asp:Label>&nbsp;</td>
                        <td style="text-align: left; width: 362px;">
                            <asp:TextBox ID="txtItemUOM" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="text-align: right; width: 398px;">
                            <asp:Label ID="Label22" runat="server" Text="Quantity" Width="57px"></asp:Label></td>
                        <td style="text-align: left; width: 332px;">
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
                        <td style="text-align: right; width: 192px;">
                            <asp:Label ID="Label30" runat="server" Text="ItemCategory :"></asp:Label>&nbsp;</td>
                        <td style="text-align: left; width: 362px;">
                            <asp:TextBox ID="txtItemCategory" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="text-align: right; width: 398px;">
                            <asp:Label ID="Label23" runat="server" Text="Balance Quantity"></asp:Label></td>
                        <td style="text-align: left; width: 332px;">
                            <asp:TextBox ID="txtBalanceQty" runat="server" ReadOnly="True">
                            </asp:TextBox><asp:TextBox ID="txtBalanceQtyHidden" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 192px;">
                            <asp:Label ID="Label14" runat="server" Text="Ordered Quantity"></asp:Label>&nbsp;</td>
                        <td style="text-align: left; width: 362px;">
                            <asp:TextBox ID="txtOrderedQty" runat="server"></asp:TextBox></td>
                        <td style="text-align: right; width: 398px;"></td>
                        <td style="text-align: left; width: 332px;">
                            <asp:TextBox ID="txtInhand" runat="server" Visible="False"></asp:TextBox><asp:TextBox ID="txtresqty" runat="server" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 192px;">
                            <asp:Label ID="Label49" runat="server" Text="Dc For :"></asp:Label></td>
                        <td style="text-align: left; width: 362px;">
                            <asp:DropDownList ID="ddlDcFor" runat="server">
                                <asp:ListItem Value="0">-----</asp:ListItem>
                                <asp:ListItem>Extra</asp:ListItem>
                                <asp:ListItem>HighSeaSale</asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="text-align: right; width: 398px;">
                            <asp:Label ID="Label42" runat="server" Text="Serial No." Visible="False"></asp:Label></td>
                        <td style="text-align: left; width: 332px;">
                            <asp:TextBox ID="txtSerialNo" runat="server" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 192px; text-align: right">
                            <asp:Label ID="Label52" runat="server" Text="Remarks2 :"></asp:Label></td>
                        <td style="width: 362px; text-align: left">
                            <asp:TextBox ID="txtremarks2" runat="server" EnableTheming="False" TextMode="MultiLine"
                                Width="382px">-</asp:TextBox></td>
                        <td style="width: 398px; text-align: right"></td>
                        <td style="width: 332px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: center;" colspan="4">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                            CssClass="loginbutton" EnableTheming="False" OnClick="btnAdd_Click" Text="Add"
                                            ValidationGroup="ip" /></td>
                                    <td>
                                        <asp:Button ID="btnItemRefresh" runat="server" BackColor="Transparent" BorderStyle="None"
                                            CssClass="loginbutton" EnableTheming="False" OnClick="btnItemRefresh_Click" Text="Refresh"
                                            CausesValidation="False" /></td>
                                </tr>
                            </table>
                            &nbsp;&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 192px;"></td>
                        <td style="text-align: right; width: 362px;"></td>
                        <td style="text-align: left; width: 398px;"></td>
                        <td style="text-align: left; width: 332px;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="4">
                            <asp:GridView ID="gvItmDetails" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvItmDetails_RowDeleting"
                                Width="100%" OnRowDataBound="gvItmDetails_RowDataBound" OnRowEditing="gvItmDetails_RowEditing">
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
                                    <asp:BoundField DataField="DCfor" HeaderText="Dc For"></asp:BoundField>
                                    <asp:BoundField DataField="Remarks2" HeaderText="Remarks2"></asp:BoundField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <span style="color: #ff0033">No Data Exist</span>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left; height: 27px;">Reference Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 192px;"></td>
                        <td style="text-align: left; width: 362px;"></td>
                        <td style="text-align: right; width: 398px;"></td>
                        <td style="text-align: left; width: 332px;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 192px;">
                            <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left; width: 362px;">
                            <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                        <td style="text-align: right; width: 398px;">
                            <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By" Visible="False"></asp:Label></td>
                        <td style="text-align: left; width: 332px;">
                            <asp:DropDownList ID="ddlApprovedBy" runat="server" Enabled="False" Visible="False">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 2px; text-align: right; width: 192px;"></td>
                        <td style="height: 2px; text-align: left; width: 362px;"></td>
                        <td style="height: 2px; text-align: right; width: 398px;"></td>
                        <td style="height: 2px; text-align: left; width: 332px;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 27px;" colspan="4">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table id="tblButtons">
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
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" CausesValidation="False" OnClick="btnPrint_Click" /></td>
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
                <asp:GridView ID="gvPreviousPayments" runat="server" AutoGenerateColumns="False"
                    OnRowDataBound="gvPreviousPayments_RowDataBound" ShowFooter="True" Width="1005px" Visible="False">
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
            <td colspan="4" style="height: 23px; width: 1242px;">&nbsp;<asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="ip" />
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
                <cc1:CalendarExtender
                    Format="dd/MM/yyyy" ID="CalendarExtender1" runat="server" Enabled="True" PopupButtonID="imgInwardDate"
                    TargetControlID="txtInwardDate">
                </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" DisplayMoney="Left"
                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtInwardDate"
                    UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
                <cc1:CalendarExtender Format="dd/MM/yyyy"
                    ID="CeDeliveryChallanDate" runat="server" Enabled="True" PopupButtonID="imgDeliveryChallanDate"
                    TargetControlID="txtDeliveryChallanDate">
                </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="MeeDeliveryChallanDate" runat="server" DisplayMoney="Left"
                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDeliveryChallanDate"
                    UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
                <cc1:CalendarExtender Format="dd/MM/yyyy" ID="CeLRDate" runat="server" Enabled="True"
                    PopupButtonID="imgLRDate" TargetControlID="txtLRDate">
                </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="MeeLRDate" runat="server" DisplayMoney="Left" Enabled="True"
                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtLRDate" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
                <cc1:MaskedEditExtender ID="meeEnquiryDateForAssing" runat="server" DisplayMoney="Left"
                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtDeliveryDateForAssign"
                    UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
                <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceAssignDate" runat="server" PopupButtonID="imgAssignDate"
                    TargetControlID="txtAssignDate">
                </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="meeAssingDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                    MaskType="Date" TargetControlID="txtAssignDate" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
                <cc1:CalendarExtender Format="dd/MM/yyyy"
                    ID="ceDueDate" runat="server" PopupButtonID="imgDueDate" TargetControlID="txtDueDate">
                </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="meeDueDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                    MaskType="Date" TargetControlID="txtDueDate" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
                <table border="0" cellpadding="0" cellspacing="0" id="Table4" runat="server"
                    visible="true" width="100%">
                    <tr>
                        <td style="text-align: left;">&nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
                        </td>
                    </tr>
                </table>
                <asp:RadioButton ID="rbSpares" runat="server" AutoPostBack="True" GroupName="dc"
                    OnCheckedChanged="rbSpares_CheckedChanged" Text="Spares" Visible="False" /></td>
        </tr>
    </table>
</asp:Content>


