<%@ Page Language="C#" MasterPageFile="~/MPs/InventoryMP1.master" AutoEventWireup="true" 
    CodeFile="UnbilledDC - Copy.aspx.cs" Inherits="Modules_Inventory_UnbilledDC" Title="|| Value Appp : Inventory : Untitled Page ||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="pagehead">
        <tr>
            <td class="pagehead" align="right" style="text-align: left;">
                UnBilled Delivery Challan
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
    <table style="width:100%">
        <tr>
            <td style="text-align: right;font-size:15px">  <asp:LinkButton ID="lnkSales" runat="server" OnClick="lnkSales_Click">Sales DC</asp:LinkButton>  </td>
            <td style="width:10%"></td>
             <td style="font-size:15px"> <asp:LinkButton ID="lnkCash" runat="server" OnClick="lnkCash_Click">Sample DC</asp:LinkButton> </td>
        </tr>
    </table>
    <br />
    <asp:Panel ID="pnlSales" runat="server">
    <table style="width:100%">
        <tr>
            <td colspan="3">
                <table id="TABLE2" border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="searchhead" style="text-align: left">
                            UnBilled Delivery Challan For Sales
                        </td>
                        <td style="width: 23px">
                        </td>
                        <td class="searchhead" style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="height: 25px">
                                        <asp:Label id="lblSearchBy" runat="server" CssClass="label" EnableTheming="False"
                                            Font-Bold="True" Text="Search By" Width="71px"></asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList id="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="SO_NO">PO No</asp:ListItem>
                                            <asp:ListItem Value="DC_NO">DC No</asp:ListItem>
                                            <asp:ListItem Value="DC_DATE">DC Date</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME">Customer Name</asp:ListItem>
                                            <asp:ListItem Value="EMP_FIRST_NAME">Executive Name</asp:ListItem>
                                            <asp:ListItem Value="CP_ID">Company Search</asp:ListItem>

                                            <%--<asp:ListItem Value="TRANS_LONG_NAME">Transporter Name</asp:ListItem>
                                            <asp:ListItem Value="ITEM_MODEL_NO">Model NO</asp:ListItem>--%>
                                        </asp:DropDownList></td>
                                    <td style="height: 25px">
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
                                    <td style ="height :25px">
                                        <asp:DropDownList ID="ddlCpId" runat ="server" AutoPostBack ="true" CssClass ="textbox" EnableTheming ="false" Visible ="false" Width ="80px">

                                        </asp:DropDownList>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Label id="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:TextBox id="txtSearchValueFromDate" runat="server" type="date" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox><%--<asp:Image id="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:calendarextender id="ceSearchFrom" runat="server" enabled="False" format="dd/MM/yyyy"
                                            popupbuttonid="imgFromDate" targetcontrolid="txtSearchValueFromDate"></cc1:calendarextender>
                                        <cc1:maskededitextender id="MaskedEditSearchFromDate" runat="server" displaymoney="Left"
                                            enabled="False" mask="99/99/9999" masktype="Date" targetcontrolid="txtSearchValueFromDate"
                                            userdateformat="MonthDayYear"></cc1:maskededitextender>--%>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Label id="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                     <td style="height: 25px">
                                        <asp:TextBox id="txtSearchValueToDate" runat="server" type="date" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox></td>
                                    <td style="height: 25px">
                                        <asp:TextBox id="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><%--<asp:Image id="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:calendarextender id="ceSearchValueToDate" runat="server" enabled="False" format="dd/MM/yyyy"
                                            popupbuttonid="imgToDate" targetcontrolid="txtSearchText"></cc1:calendarextender>
                                        <cc1:maskededitextender id="MaskedEditSearchToDate" runat="server" displaymoney="Left"
                                            enabled="False" mask="99/99/9999" masktype="Date" targetcontrolid="txtSearchText"
                                            userdateformat="MonthDayYear"></cc1:maskededitextender>--%>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Button id="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" onclick="btnSearchGo_Click" Text="Go" /></td>
                                </tr>
                                </table>
                            <asp:Label ID="lblUserName" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblUserId" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblCp_Ids" runat="server" Visible="false"></asp:Label>
                            <asp:Label id="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label>
                            <asp:Label id="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label><asp:Label id="lblEmpIdHidden" runat="server" Visible="False"></asp:Label><asp:Label
                                    id="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="width: 100%">
                <asp:GridView id="gvDeliveryChallanDetails" SelectedRowStyle-BackColor="#c0c0c0" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="sdsDeliveryChallanDetails" OnRowDataBound="gvDeliveryChallanDetails_RowDataBound"
                    Width="100%" AllowSorting="True" PageSize="50">
                    <columns>
<asp:BoundField DataField="DC_ID" SortExpression="DC_ID" HeaderText="DCIdHidden"></asp:BoundField>
<asp:BoundField DataField="SO_ID" SortExpression="SO_ID" HeaderText="SONoHidden">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="DC_NO" SortExpression="DC_NO" HeaderText="DC NO"></asp:BoundField>
<asp:BoundField DataField="SO_NO" SortExpression="SO_NO" HeaderText="SO NO"></asp:BoundField>

<asp:BoundField HtmlEncode="False" SortExpression="DC_DATE" DataFormatString="{0:dd/MM/yyyy}" DataField="DC_DATE" HeaderText="DCDate"></asp:BoundField>
<asp:BoundField DataField="CUST_NAME" SortExpression="CUST_NAME" HeaderText="Customer">
<ItemStyle Width="300px" HorizontalAlign="Left"></ItemStyle>

<HeaderStyle Width="300px"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="EMP_FIRST_NAME" SortExpression="EMP_FIRST_NAME" HeaderText="Executive">
<ItemStyle Width="200px" HorizontalAlign="Left"></ItemStyle>

<HeaderStyle Width="200px"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Amount" SortExpression="Amount" HeaderText="DC Amount">
<ItemStyle Width="200px"></ItemStyle>

<HeaderStyle Width="200px"></HeaderStyle>
</asp:BoundField>
<%--<asp:BoundField DataField="TRANS_LONG_NAME" SortExpression="TRANS_LONG_NAME" HeaderText="TransporterName">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PreparedBy" SortExpression="PreparedBy" HeaderText="PreparedBy"></asp:BoundField>
<asp:BoundField DataField="ApprovedBy" SortExpression="ApprovedBy" HeaderText="ApprovedBy"></asp:BoundField>
<asp:BoundField DataField="dc_for" SortExpression="dc_for" HeaderText="dc_forHidden"></asp:BoundField>
<asp:BoundField DataField="STATUS" SortExpression="STATUS" HeaderText="Status"></asp:BoundField>--%>
</columns>
                    <emptydatatemplate>
                        No Data Exist!
                    
</emptydatatemplate>
                </asp:GridView><asp:SqlDataSource id="sdsDeliveryChallanDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="USP_UnbilledDCSales" SelectCommandType="StoredProcedure">
                    <selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
                        <asp:ControlParameter ControlID="lblCp_Ids" Name="CPID" DefaultValue="0" PropertyName="Text" Type="Int64" />

</selectparameters>
                </asp:SqlDataSource></td>
        </tr>
        <tr>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
        </tr>
    </table>
        </asp:Panel>
    <%--DC For Sample--%>
    <asp:Panel ID="pnlSample" runat="server" Visible="False">
    <table style="width:100%">
        <tr>
            <td colspan="3">
                <table id="TABLE2" border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="searchhead" style="text-align: left">
                            UnBilled Delivery Challan For Sample 
                        </td>
                        <td style="width: 23px">
                        </td>
                        <td class="searchhead" style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="height: 25px">
                                        <asp:Label id="Label1" runat="server" CssClass="label" EnableTheming="False"
                                            Font-Bold="True" Text="Search By" Width="71px"></asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList id="ddlSearchBy1" runat="server" AutoPostBack="True" CssClass="textbox" OnSelectedIndexChanged="ddlSearchBy1_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <%--<asp:ListItem Value="SO_NO">PO No</asp:ListItem>--%>
                                            <asp:ListItem Value="DC_NO">DC No</asp:ListItem>
                                            <asp:ListItem Value="DC_DATE">DC Date</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME">Customer Name</asp:ListItem>
                                            <%--<asp:ListItem Value="EMP_FIRST_NAME">Executive Name</asp:ListItem>--%>

                                            <%--<asp:ListItem Value="TRANS_LONG_NAME">Transporter Name</asp:ListItem>
                                            <asp:ListItem Value="ITEM_MODEL_NO">Model NO</asp:ListItem>--%>
                                        </asp:DropDownList></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList id="ddlSymbol1" runat="server" AutoPostBack="True" CssClass="textbox"
                                            EnableTheming="False"
                                            Visible="False" Width="50px" OnSelectedIndexChanged="ddlSymbol1_SelectedIndexChanged">
                                            <asp:ListItem Selected="True">=</asp:ListItem>
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem>&gt;</asp:ListItem>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <asp:ListItem>&gt;=</asp:ListItem>
                                            <asp:ListItem>R</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="height: 25px">
                                        <asp:Label id="Label2" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:TextBox id="txtSearchValueFromDate1" runat="server" type="date" EnableTheming="True" Visible="False"
                                            Width="106px"></asp:TextBox><%--<asp:Image id="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:calendarextender id="ceSearchFrom" runat="server" enabled="False" format="dd/MM/yyyy"
                                            popupbuttonid="imgFromDate" targetcontrolid="txtSearchValueFromDate"></cc1:calendarextender>
                                        <cc1:maskededitextender id="MaskedEditSearchFromDate" runat="server" displaymoney="Left"
                                            enabled="False" mask="99/99/9999" masktype="Date" targetcontrolid="txtSearchValueFromDate"
                                            userdateformat="MonthDayYear"></cc1:maskededitextender>--%>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Label id="Label3" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                     <td style="height: 25px">
                                        <asp:TextBox id="txtSearchValueToDate1" runat="server" type="date" EnableTheming="True" Visible="False"
                                            Width="106px"></asp:TextBox></td>
                                    <td style="height: 25px">
                                        <asp:TextBox id="txtSearchText1" runat="server" EnableTheming="True" Width="118px"></asp:TextBox><%--<asp:Image id="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:calendarextender id="ceSearchValueToDate" runat="server" enabled="False" format="dd/MM/yyyy"
                                            popupbuttonid="imgToDate" targetcontrolid="txtSearchText"></cc1:calendarextender>
                                        <cc1:maskededitextender id="MaskedEditSearchToDate" runat="server" displaymoney="Left"
                                            enabled="False" mask="99/99/9999" masktype="Date" targetcontrolid="txtSearchText"
                                            userdateformat="MonthDayYear"></cc1:maskededitextender>--%>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Button id="btnSearchGo1" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" onclick="btnSearchGo1_Click" Text="Go" /></td>
                                </tr>
                                </table>
                           <%-- <asp:Label id="Label4" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label>
                            <asp:Label id="Label5" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label><asp:Label id="Label6" runat="server" Visible="False"></asp:Label><asp:Label
                                    id="Label7" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="Label8" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="Label9" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="Label10" runat="server" Visible="False"></asp:Label></td>--%>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="width: 100%">
                <asp:GridView id="gvSampleDC" SelectedRowStyle-BackColor="#c0c0c0" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="sdsSampleDC"
                    Width="100%" AllowSorting="True" PageSize="50" OnRowDataBound="gvSampleDC_RowDataBound">
                    <columns>
<%--<asp:BoundField DataField="TRANS_LONG_NAME" SortExpression="TRANS_LONG_NAME" HeaderText="TransporterName">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PreparedBy" SortExpression="PreparedBy" HeaderText="PreparedBy"></asp:BoundField>
<asp:BoundField DataField="ApprovedBy" SortExpression="ApprovedBy" HeaderText="ApprovedBy"></asp:BoundField>
<asp:BoundField DataField="dc_for" SortExpression="dc_for" HeaderText="dc_forHidden"></asp:BoundField>
<asp:BoundField DataField="STATUS" SortExpression="STATUS" HeaderText="Status"></asp:BoundField>--%>
                        <asp:BoundField DataField="DC_ID" HeaderText="DCIdHidden" SortExpression="DC_ID" />
                        <asp:BoundField DataField="DC_NO" HeaderText="DC NO" SortExpression="DC_NO" />
                        <asp:BoundField DataField="DC_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="DCDate" HtmlEncode="False" SortExpression="DC_DATE" />
                        <asp:BoundField DataField="CUST_NAME" HeaderText="Customer" SortExpression="CUST_NAME">
                        <ItemStyle HorizontalAlign="Left" Width="300px" />
                        <HeaderStyle Width="300px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="STATUS" HeaderText="DC Status" SortExpression="Amount">
                        <HeaderStyle Width="200px" />
                        <ItemStyle Width="200px" />
                        </asp:BoundField>
</columns>
                    <emptydatatemplate>
                        No Data Exist!
                    
</emptydatatemplate>
                    <SelectedRowStyle BackColor="Silver" />
                </asp:GridView><asp:SqlDataSource id="sdsSampleDC" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="USP_UnbilledDCSample" SelectCommandType="StoredProcedure">
                    <selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
</selectparameters>
                </asp:SqlDataSource></td>
        </tr>
        <tr>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
        </tr>
    </table>
        </asp:Panel>
</asp:Content>


 
