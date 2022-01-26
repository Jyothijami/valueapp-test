<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="POSchedulingDetails.aspx.cs" Inherits="Modules_PurchasingManagement_POSchedulingDetails"
    Title="|| YANTRA : Purchasing Management : Fixed Purchase Order Details ||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <script type="text/javascript" language="javascript">
function Grandamtcalc()
{
    var sch_qty,rate;
    sch_qty=document.getElementById('<%=txtSchQty.ClientID %>').value;
    rate=document.getElementById('<%=txtRate.ClientID %>').value;
    
       
    if(sch_qty=="" ||sch_qty=="0")
    {
        document.getElementById('<%=txtGrandAmt.ClientID %>').value="0";
    }
    else if(rate=="" || rate=="0")
    {
        document.getElementById('<%=txtGrandAmt.ClientID %>').value="0";
    }
    else if(rate>0 && sch_qty>0)
    {
        document.getElementById('<%=txtGrandAmt.ClientID %>').value=(rate*sch_qty);
        
    }
}   

   

    </script>

    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                PO SCHEDULING DETAILS</td>
        </tr>
    </table>
    <table style="width: 88px" border="0" cellpadding="0" cellspacing="0">
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
            <td colspan="4" style="height: 19px" class="searchhead">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">
                            Scheduling Details</td>
                        <td>
                        </td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Label ID="lblSearchBy" runat="server" CssClass="label" EnableTheming="False"
                                            Font-Bold="True" Text="Search By"></asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="POS_NO">POS  No.</asp:ListItem>
                                            <asp:ListItem Value="POS_DATE">POS Date</asp:ListItem>
                                            <asp:ListItem Value="FPO_NO">PO NO</asp:ListItem>
                                            <asp:ListItem Value="FPO_DATE">PO Date</asp:ListItem>
                                            <asp:ListItem Value="POS_STATUS">PO Status</asp:ListItem>
                                            <asp:ListItem Value="SUP_NAME">Supplier Name</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td rowspan="3" style="height: 25px">
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
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:TextBox ID="txtSearchValueFromDate" runat="server" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox><asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchFrom" runat="server" Enabled="False"
                                            PopupButtonID="imgFromDate" TargetControlID="txtSearchValueFromDate">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchValueToDate" runat="server"
                                            Enabled="False" PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                </tr>
                            </table>
                            <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 19px" colspan="4">
                <asp:GridView ID="gvPOSchedulingDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="sdsPOSchedulingDetails" OnRowDataBound="gvPOSchedulingDetails_RowDataBound"
                    Width="100%">
                    <Columns>
                        <asp:BoundField DataField="POS_ID" SortExpression="POS_ID" HeaderText="POSIdHidden">
                        </asp:BoundField>
                        <asp:TemplateField SortExpression="POS_NO" HeaderText="POSNO">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("POS_NO") %>' ID="TextBox1"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnPOScheduleNo" OnClick="lbtnPOScheduleNo_Click" runat="server"
                                    Text='<%# Eval("POS_NO") %>' __designer:wfdid="w11"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:MM/dd/yyyy}" DataField="POS_DATE"
                            SortExpression="POS_DATE" HeaderText="POS Date">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="FPO_NO" SortExpression="FPO_NO" HeaderText="PO No">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:MM/dd/yyyy}" DataField="FPO_DATE"
                            SortExpression="FPO_DATE" HeaderText="PO Date">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="POS_STATUS" SortExpression="POS_STATUS" HeaderText="PO Status">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField ReadOnly="True" DataField="SUP_NAME" SortExpression="SUP_NAME" HeaderText="Supplier Name">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="sdsPOSchedulingDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DbCon %>"
                    SelectCommand="SP_SCM_POSCHEDULING_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName"
                            ControlID="lblSearchItemHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType"
                            ControlID="lblSearchTypeHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue"
                            ControlID="lblSearchValueHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom"
                            ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td id="Td20" style="height: 19px">
            </td>
            <td style="height: 19px">
            </td>
            <td style="height: 19px">
            </td>
            <td style="height: 19px">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 21px">
                <table id="Table1">
                    <tr>
                        <td>
                            <asp:Button ID="btnNew" runat="server" OnClick="btnNew_Click" Text="New" /></td>
                        <td>
                            <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Edit" /></td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 21px">
            </td>
            <td style="height: 21px">
            </td>
            <td style="height: 21px;">
            </td>
            <td style="height: 21px">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 21px">
                <table border="0" cellpadding="0" cellspacing="0" id="tblPOSchedulingDetails" runat="server"
                    visible="false">
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">
                            General Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right;">
                        </td>
                        <td style="height: 21px; text-align: left;">
                        </td>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblPOScheduleNo" runat="server" Text="PO Schedule No" Width="119px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtPOScheduleNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblPOScheduleDate" runat="server" Text="PO Schedule Date" Width="152px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtPOScheduleDate" runat="server"></asp:TextBox>&nbsp;<asp:Image
                                ID="imgPOScheduleDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender
                                    Format="dd/MM/yyyy" ID="cePOScheduleDate" runat="server" Enabled="True" PopupButtonID="imgPOScheduleDate"
                                    TargetControlID="txtPOScheduleDate">
                                </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meePOScheduleDate" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtPOScheduleDate"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblPONo" runat="server" Text="PO No"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlPONo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPONo_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="text-align: right;">
                            &nbsp;<asp:Label ID="lblPODate" runat="server" Text="PO Date" Width="99px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtPODate" runat="server">
                            </asp:TextBox>&nbsp;<asp:Image ID="imgPODate" runat="server" ImageUrl="~/Images/Calendar.png">
                            </asp:Image><cc1:CalendarExtender Format="dd/MM/yyyy" ID="cePODate" runat="server"
                                Enabled="True" PopupButtonID="imgPODate" TargetControlID="txtPODate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meePODate" runat="server" DisplayMoney="Left" Enabled="True"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtPODate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblPOType" runat="server" Text="PO Type" Width="129px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlPOType" runat="server" AutoPostBack="True">
                                <asp:ListItem>--</asp:ListItem>
                                <asp:ListItem>Fixed</asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblSupplierName" runat="server" Text="Supplier Name" Width="119px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlSupplierName" runat="server" AutoPostBack="True">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                        </td>
                        <td style="text-align: left;">
                        </td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left">
                            <asp:GridView ID="gvPOItemsDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvPOItemsDetails_RowDataBound"
                                Width="100%">
                                <Columns>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
<asp:BoundField HeaderText="Amount"></asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:MM/dd/yyyy}" DataField="DeliveryDate" HeaderText="Delivery Date"></asp:BoundField>
<asp:BoundField DataField="Specifications" HeaderText="Specifications"></asp:BoundField>
<asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
</Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left; height: 20px;" class="profilehead">
                            &nbsp;Item Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right;">
                        </td>
                        <td style="height: 19px; text-align: left;">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            &nbsp;<asp:Label ID="lblItemType" runat="server" Text="Item Type"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label ID="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvItemType" runat="server" ControlToValidate="ddlItemType"
                                ErrorMessage="Please Select the Item Type" InitialValue="0" ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblItemName" runat="server" Text="Item Name"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlItemName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="0">-- Select Item Type --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="Label2" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvItemName" runat="server" ControlToValidate="ddlItemName"
                                ErrorMessage="Please Select the Item Name" InitialValue="0" ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblUOM" runat="server" Text="UOM" Width="96px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtUOM" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblPOQuantity" runat="server" Text="PO Quantity" Width="96px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPOQty" runat="server" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label3" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvPOQTY" runat="server" ControlToValidate="txtPOQty"
                                ErrorMessage="Please Enter the Quantity" ValidationGroup="ip">*</asp:RequiredFieldValidator>
                            <cc1:FilteredTextBoxExtender ID="ftxtQuantity" runat="server" FilterType="Numbers"
                                TargetControlID="txtPOQty">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" valign="top">
                            <asp:Label ID="lblRate" runat="server" Text="Rate" Width="86px"></asp:Label><br />
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtRate" runat="server" OnTextChanged="txtRate_TextChanged" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label1" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvRate" runat="server" ControlToValidate="txtRate"
                                ErrorMessage="Please Enter the Rate" ValidationGroup="ip">*</asp:RequiredFieldValidator>
                            <cc1:FilteredTextBoxExtender ID="ftxteItemRate" runat="server" FilterType="Numbers"
                                TargetControlID="txtRate">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="lblSchQuantity" runat="server" Text="Sch. Quantity" Width="96px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtSchQty" runat="server" OnTextChanged="txtSchQty_TextChanged"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblGrossAmount" runat="server" Text="G. Amount" Width="96px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtGrandAmt" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblSheduledDate" runat="server" Text="Scheduled Date" Width="96px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtScheduledDate" runat="server"></asp:TextBox>&nbsp;<asp:Image
                                ID="imgScheduledDate" runat="server" ImageUrl="~/Images/Calendar.png" /><cc1:CalendarExtender
                                    Format="dd/MM/yyyy" ID="ceScheduledDate" runat="server" Enabled="True" PopupButtonID="imgScheduledDate"
                                    TargetControlID="txtScheduledDate">
                                </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeScheduledDate" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtScheduledDate"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right;">
                        </td>
                        <td style="height: 19px; text-align: left;">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                        </td>
                        <td style="text-align: right;">
                            <asp:Button ID="btnAdd" runat="server" Text="Add" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" OnClick="btnAdd_Click" /></td>
                        <td style="text-align: left">
                            <asp:Button ID="btnItemsRefresh" runat="server" Text="Refresh" BackColor="Transparent"
                                BorderStyle="None" CssClass="loginbutton" EnableTheming="False" OnClick="btnItemsRefresh_Click" /></td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right;">
                        </td>
                        <td style="height: 19px; text-align: left;">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 21px">
                            <asp:GridView ID="gvScheduledItems" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvScheduledItems_RowDeleting"
                                Width="100%" OnRowDataBound="gvScheduledItems_RowDataBound">
                                <Columns>
                                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                    <asp:BoundField DataField="ItemType" HeaderText="Item Type"></asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                    <asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
                                    <asp:BoundField DataField="POQuantity" HeaderText="PO Qty"></asp:BoundField>
                                    <asp:BoundField DataField="SchQuantity" HeaderText="Sch Quantity"></asp:BoundField>
                                    <asp:BoundField HeaderText="Grand Amt"></asp:BoundField>
                                    <asp:BoundField DataField="SchDate" HeaderText="Sch Date"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">
                            Reference Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right;">
                        </td>
                        <td style="height: 21px; text-align: left;">
                        </td>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlPreparedBy" runat="server">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By" Width="96px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlApprovedBy" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;">
                        </td>
                        <td style="height: 19px;">
                        </td>
                        <td style="height: 19px">
                        </td>
                        <td style="height: 19px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table id="tblButtons">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 21px">
            </td>
        </tr>
    </table>
</asp:Content>

 
