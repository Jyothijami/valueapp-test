<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OrderSearch.aspx.cs" Inherits="Modules_SCM_OrderSearch" Title="Self Order Search" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="text-align: left">
                Self purchase order</td>
            <td>
            </td>
            <td style="text-align: right">
                <table>
                    <tr>
                        <td>
                            <asp:Label id="Label12" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                Text="Search By"></asp:Label></td>
                        <td>
                            <asp:DropDownList id="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="ITEM_MODEL_NO">Model No</asp:ListItem>
                                <asp:ListItem Value="FPO_DATE">Po Date</asp:ListItem>
                                <asp:ListItem Value="FPO_DET_CUSTOMER">Customer</asp:ListItem>
                                <asp:ListItem Value="FPO_DET_STATUS">Status</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>
                            <asp:DropDownList id="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
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
                            <asp:Label id="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                        <td>
                            <asp:TextBox id="txtSearchValueFromDate" runat="server" EnableTheming="True" Visible="False"
                                Width="106px">
                                        </asp:TextBox><asp:Image id="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                Visible="False"></asp:Image><cc1:CalendarExtender ID="ceSearchFrom" runat="server"
                                    Enabled="False" Format="dd/MM/yyyy" PopupButtonID="imgFromDate" TargetControlID="txtSearchValueFromDate">
                                </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeSearchFromDate" runat="server" DisplayMoney="Left"
                                Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                        <td>
                            <asp:Label id="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                        <td>
                            <asp:TextBox id="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><asp:Image id="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                Visible="False"></asp:Image><cc1:CalendarExtender ID="ceSearchValueToDate" runat="server"
                                    Enabled="False" Format="dd/MM/yyyy" PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                                </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeSearchToDate" runat="server" DisplayMoney="Left" Enabled="False"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                        <td>
                            <asp:Button id="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                CssClass="gobutton" EnableTheming="False" onclick="btnSearchGo_Click" Text="Go" /></td>
                    </tr>
                </table>
                <asp:Label id="lblSearchItemHidden" runat="server" Visible="False"></asp:Label><asp:Label
                    id="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label><asp:Label id="lblSearchValueFromHidden"
                        runat="server" Visible="False"></asp:Label><asp:Label id="lblSearchValueHidden" runat="server"
                            Visible="False"></asp:Label>
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td style="height: 19px; text-align: left">
            </td>
            <td style="height: 19px">
            </td>
            <td style="height: 19px; text-align: right">
            </td>
        </tr>
    </table>
    <asp:GridView id="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        DataSourceID="SqlDataSource1" PageSize="15" OnRowDataBound="GridView1_RowDataBound" EnableViewState="False">
        <columns>
<asp:TemplateField HeaderText="Edit"><ItemTemplate>
<asp:LinkButton id="lbtnEdit" runat="server" __designer:wfdid="w76" OnClick="lbtnEdit_Click">Edit</asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="FPO_NO" SortExpression="FPO_NO" HeaderText="Order No"></asp:BoundField>
<asp:BoundField DataField="ITEM_MODEL_NO" SortExpression="ITEM_MODEL_NO" HeaderText="Model No"></asp:BoundField>
<asp:BoundField DataField="ITEM_NAME" SortExpression="ITEM_NAME" HeaderText="Item Name"></asp:BoundField>
<asp:BoundField DataField="IT_TYPE" SortExpression="IT_TYPE" HeaderText="SubCategory"></asp:BoundField>
<asp:BoundField DataField="FPO_DET_COLOR" SortExpression="FPO_DET_COLOR" HeaderText="Color"></asp:BoundField>
<asp:BoundField DataField="FPO_DET_QTY" SortExpression="FPO_DET_QTY" HeaderText="Qty"></asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="FPO_DET_DELIVERY_DATE" SortExpression="FPO_DET_DELIVERY_DATE" HeaderText="Exp Date of Delivery"></asp:BoundField>
<asp:BoundField DataField="FPO_DET_CUSTOMER" SortExpression="FPO_DET_CUSTOMER" HeaderText="Customer"></asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="FPO_DET_EXPDATE" SortExpression="FPO_DET_EXPDATE" HeaderText="Arrived Date"></asp:BoundField>
<asp:BoundField DataField="FPO_DET_STATUS" SortExpression="FPO_DET_STATUS" HeaderText="Status"></asp:BoundField>
<asp:BoundField DataField="FPOS_DET_ID" SortExpression="FPOS_DET_ID" HeaderText="Id"></asp:BoundField>
</columns>
    </asp:GridView>
    <asp:SqlDataSource id="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
        SelectCommand="SP_SELF_ORDER_SEARCH_SELECT" SelectCommandType="StoredProcedure">
        <selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
</selectparameters>
    </asp:SqlDataSource>
    <table id="tblSub" runat="server" width="100%">
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
            <td align="right">
                <asp:Label id="Label1" runat="server" Text="Exp Date of Arrival :" Width="135px"></asp:Label></td>
            <td align="left">
                <asp:TextBox id="txtExpdateofarrival" runat="server">
                </asp:TextBox><asp:Image id="Image1" runat="server" ImageUrl="~/Images/Calendar.png"
                                Visible="False"></asp:Image><cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                    Enabled="True" Format="dd/MM/yyyy" PopupButtonID="Image1" TargetControlID="txtExpdateofarrival">
                    </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtExpdateofarrival"
                                UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
            </td>
            <td align="right">
                <asp:Label id="Label2" runat="server" Text="Arrived Date :"></asp:Label></td>
            <td align="left">
                <asp:TextBox id="txtArriveddate" runat="server">
                </asp:TextBox><asp:Image id="Image2" runat="server" ImageUrl="~/Images/Calendar.png"
                                Visible="False"></asp:Image><cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                    Enabled="True" Format="dd/MM/yyyy" PopupButtonID="Image2" TargetControlID="txtArriveddate">
                    </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtArriveddate"
                                UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label id="Label3" runat="server" Text="Status :"></asp:Label></td>
            <td align="left">
                <asp:DropDownList id="ddlstatus" runat="server">
                    <asp:ListItem>Pending</asp:ListItem>
                    <asp:ListItem>Received</asp:ListItem>
                    <asp:ListItem>Close</asp:ListItem>
                </asp:DropDownList></td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td align="right">
                <asp:Button id="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update" /></td>
            <td align="left">
                <asp:Button id="btnexit" runat="server" Text="Exit" /></td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>


 
