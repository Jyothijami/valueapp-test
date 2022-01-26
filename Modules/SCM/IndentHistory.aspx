<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/PurchaseMP1.master" AutoEventWireup="true" CodeFile="IndentHistory.aspx.cs" Inherits="Modules_SCM_IndentHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                Indent History
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
    <table class="stacktable" style="width:100%" >
        <tr>
            <td style="text-align: left" >
                                    Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
                                </td>

            <td colspan="2" style="text-align: right">
                                        <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True" Text="Search By"></asp:Label>
                                    
                                    
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="ITEM_NAME">Model No</asp:ListItem>
                                            <asp:ListItem Value="IND_DET_REQ_FOR">Customer Name</asp:ListItem>
                                            <asp:ListItem Value="IND_DET_BRAND">Brand</asp:ListItem>
                                            <asp:ListItem Value="IND_DATE">Indent Date</asp:ListItem>
                                            <asp:ListItem Value="IND_DET_REQ_BY_DATE">Required By Date</asp:ListItem>
                                            <asp:ListItem Value="COLOUR_NAME">Color</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlSymbols" runat="server" AutoPostBack="True" EnableTheming="False" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged" Visible="False" Width="50px">
                                            <asp:ListItem Selected="True">=</asp:ListItem>
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem>&gt;</asp:ListItem>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <asp:ListItem>&gt;=</asp:ListItem>
                                            <asp:ListItem>R</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label>
                                        <asp:TextBox ID="txtSearchValueFromDate" runat="server" type="date" EnableTheming="True" Visible="False" Width="106px">
                                        </asp:TextBox>
                                       <%-- <cc1:CalendarExtender ID="ceSearchFrom" runat="server" Enabled="False" Format="dd/MM/yyyy" PopupButtonID="imgFromDate" TargetControlID="txtSearchValueFromDate">
                                        </cc1:CalendarExtender>
                                        <asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False" />--%>
                                        <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label>
                  <asp:TextBox ID="txtSearchValueToDate" runat="server" type="date" EnableTheming="True" Visible="False" Width="106px">
                                        </asp:TextBox>
                                        <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox>
                                       <%-- <cc1:CalendarExtender ID="ceSearchValueToDate" runat="server" Enabled="False" Format="dd/MM/yyyy" PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                                        </cc1:CalendarExtender>
                                        <asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False" />--%>
                                        <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False" CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" />
                                    

            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:GridView ID="gvIndentDetails" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="100%" AllowSorting="True" AllowPaging="True" PageSize="8">
                    <Columns>
                        <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" SortExpression="ITEM_CODE" />
                        <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="Model No" SortExpression="ITEM_MODEL_NO" />
                        <asp:BoundField DataField="ITEM_NAME" HeaderText="Item Name"  SortExpression="ITEM_NAME"/>
                        <asp:BoundField DataField="IT_TYPE" HeaderText="Sub Category" SortExpression="IT_TYPE" />
                        <asp:BoundField DataField="IND_DET_BRAND" HeaderText="Brand" SortExpression="IND_DET_BRAND" />
                        <asp:BoundField DataField="COLOUR_NAME" HeaderText="Color"  SortExpression="COLOUR_NAME"/>
                        <asp:BoundField DataField="IND_DET_SUGG_PARTY" HeaderText="Customer Name" SortExpression="IND_DET_SUGG_PARTY"/>
                        <asp:BoundField DataField="UOM_SHORT_DESC" HeaderText="UOM"  SortExpression="UOM_SHORT_DESC"/>
                        <asp:BoundField DataField="IND_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Indent Date" SortExpression="IND_DATE"/>
                        <asp:BoundField DataField="IND_DET_REQ_BY_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Indent Req Date" HtmlEncode="False"  SortExpression="IND_DET_REQ_BY_DATE"/>
                        <asp:BoundField DataField="IND_DET_QTY" HeaderText="Indent Qty"  SortExpression="IND_DET_QTY"/>
                        <asp:BoundField DataField="IND_DET_REM_QTY" HeaderText="Enquired Qty"  SortExpression="IND_DET_REM_QTY" Visible="False"/>
                        <asp:BoundField DataField="IND_DET_ORD_QTY" HeaderText="Ordered Qty"  SortExpression="IND_DET_ORD_QTY"/>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_SCM_INDENTED_ITEMS_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="lblSearchTypeHidden" DefaultValue="0" Name="SearchType" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="lblSearchValueFromHidden" DefaultValue="0" Name="SearchValueFrom" PropertyName="Text" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                 <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1" Visible="False"></asp:Label>
             <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1" Visible="False"></asp:Label>
             <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
             <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
             <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
             <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
             <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
 
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        
    </table>
</asp:Content>


 
