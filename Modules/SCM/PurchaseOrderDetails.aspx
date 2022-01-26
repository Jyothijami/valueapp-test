<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/PurchaseMP1.master" AutoEventWireup="true" CodeFile="PurchaseOrderDetails.aspx.cs" Inherits="Modules_SCM_PurchaseOrderDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td style="text-align:left">Purchase Order Details</td>
            <td style="text-align:right">
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
            <td>
                <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1" Visible="False"></asp:Label>
                <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1" Visible="False"></asp:Label>

            </td>
            <td></td>
            <td></td>
            <td width="750"></td>
        </tr>
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
                                    <td style="height: 25px">
                                        <asp:Label ID="Label12" runat="server" CssClass="label" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="textbox" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="FPO_NO">PO No</asp:ListItem>
                                            <asp:ListItem Value="FPO_DATE">PO Date</asp:ListItem>
                                            <asp:ListItem Value="SUP_NAME">Supplier Name</asp:ListItem>
                                        </asp:DropDownList></td>
                                     <td style="height: 25px">
                                                <asp:DropDownList ID="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                                    EnableTheming="False" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged"
                                                    Visible="False" Width="50px" meta:resourcekey="ddlSymbolsResource1">
                                                    <asp:ListItem Selected="True" meta:resourcekey="ListItemResource7">=</asp:ListItem>
                                                    <asp:ListItem meta:resourcekey="ListItemResource8">&lt;</asp:ListItem>
                                                    <asp:ListItem meta:resourcekey="ListItemResource9">&gt;</asp:ListItem>
                                                    <asp:ListItem meta:resourcekey="ListItemResource10">&lt;=</asp:ListItem>
                                                    <asp:ListItem meta:resourcekey="ListItemResource11">&gt;=</asp:ListItem>
                                                    <asp:ListItem meta:resourcekey="ListItemResource12">R</asp:ListItem>
                                                </asp:DropDownList></td>
                                    <td style="height: 25px">
                                                <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False" meta:resourcekey="lblCurrentFromDateResource1"></asp:Label></td>
                                            <td style="height: 25px">
                                                <asp:TextBox ID="txtSearchValueFromDate" runat="server" type="date" EnableTheming="True" Visible="False"
                                                    Width="106px" meta:resourcekey="txtSearchValueFromDateResource1"></asp:TextBox>
                                                <%--<asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                                        Visible="False" meta:resourcekey="imgFromDateResource1"></asp:Image>
                                                <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchFrom" runat="server" Enabled="False" PopupButtonID="imgFromDate"
                                                    TargetControlID="txtSearchValueFromDate">
                                                </cc1:CalendarExtender>
                                                <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server" DisplayMoney="Left"
                                                    Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate"
                                                    UserDateFormat="MonthDayYear" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="">
                                                </cc1:MaskedEditExtender>--%>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False" meta:resourcekey="lblCurrentToDateResource1"></asp:Label></td>
                                     <td style="height: 25px">
                                                <asp:TextBox ID="txtSearchValueToDate" runat="server" type="date" EnableTheming="True" Visible="False"
                                                    Width="106px" meta:resourcekey="txtSearchValueFromDateResource1"></asp:TextBox></td>
                                            <td style="height: 25px">
                                                <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px" meta:resourcekey="txtSearchTextResource1"></asp:TextBox>
                                                <%--<asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                                    Visible="False" meta:resourcekey="imgToDateResource1"></asp:Image>
                                                <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchValueToDate" runat="server" Enabled="False" PopupButtonID="imgToDate"
                                                    TargetControlID="txtSearchText">
                                                </cc1:CalendarExtender>
                                                <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                                    Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                                    UserDateFormat="MonthDayYear" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="">
                                                </cc1:MaskedEditExtender>--%>
                                            </td>
                                    <td style="height: 25px">
                                        <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False"  Text="Go" OnClick="btnSearchGo_Click" /></td>
                                </tr>
                            </table>
                            <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                              <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False" meta:resourcekey="lblSearchTypeHiddenResource1"></asp:Label>
                                    <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False" meta:resourcekey="lblSearchValueFromHiddenResource1"></asp:Label>
                            <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td id="Td20" colspan="4">
                <asp:GridView ID="gvFixedPODetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="SqlDataSource1" Width="100%"
                    AllowSorting="True" OnRowDataBound="gvFixedPODetails_RowDataBound" PageSize="8">
                    <Columns>
                        <asp:BoundField DataField="FPO_ID" SortExpression="FPO_ID" HeaderText="POIdHidden"></asp:BoundField>
                        <asp:TemplateField HeaderText="PO No" SortExpression="FPO_NO">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("SO_NO") %>' ID="TextBox1"></asp:TextBox>

                            </EditItemTemplate>

                            <ItemStyle Width="100px" HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle Width="100px" HorizontalAlign="Left"></HeaderStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnFixedPONo" runat="server" ForeColor="#0066ff" Text="<%# BIND('FPO_NO') %>" CausesValidation="False"
                                    OnClick="lbtnFixedPONo_Click"></asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True" SortExpression="FPO_DATE" DataField="FPO_DATE" HeaderText="PODate">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <%--<asp:BoundField DataField="INDIGENOUS_FOREIGN" SortExpression="INDIGENOUS_FOREIGN" HeaderText="IndigenousOrForeign"></asp:BoundField>--%>
                        <asp:BoundField DataField="SUP_NAME" SortExpression="SUP_NAME" HeaderText="Supplier Name">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <%--<asp:BoundField DataField="SUP_CONTACT_PERSON" SortExpression="SUP_CONTACT_PERSON" HeaderText="ContactPerson">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>--%>
                        <asp:BoundField DataField="SUP_EMAIL" SortExpression="SUP_EMAIL" HeaderText="Email">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <%--  <asp:BoundField DataField="FPO_PO_STATUS" SortExpression="FPO_PO_STATUS" HeaderText="Status">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Epreparedby" SortExpression="Epreparedby" HeaderText="PreparedBy"></asp:BoundField>
                        <asp:BoundField DataField="Eapprovedby" SortExpression="Eapprovedby" HeaderText="ApprovedBy"></asp:BoundField>--%>
                        <asp:BoundField DataField="CP_FULL_NAME" SortExpression="CP_FULL_NAME" HeaderText="Company Name">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <%--<asp:BoundField DataField="FPO_FOB" SortExpression="Eapprovedby" HeaderText="Billing Unit"></asp:BoundField>--%>
                        <asp:BoundField DataField="Delivery_Name" SortExpression="Delivery_Name" HeaderText="Shipping Unit">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                         <asp:BoundField DataField="FPO_PO_STATUS" SortExpression="FPO_PO_STATUS" HeaderText="Status">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        No Record Found
                    
                    </EmptyDataTemplate>
                    <SelectedRowStyle BackColor="LightSteelBlue" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SCM_PurchaseOrder" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchTypeHidden" DefaultValue="0" Name="SearchType" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueFromHidden" DefaultValue="0" Name="SearchValueFrom" PropertyName="Text" Type="String" />
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="CpId" ControlID="lblCPID"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="EmpId" ControlID="lblEmpIdHidden"></asp:ControlParameter>

                    </SelectParameters>
                </asp:SqlDataSource>
                <%--<asp:SqlDataSource ID="sdsFixedPODetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SCM_SUPPLIER_FIXEDPO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="CPID" ControlID="lblCPID"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="EMPID" ControlID="lblEmpIdHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>
                    </SelectParameters>
                </asp:SqlDataSource>--%>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table id="Table1" align="center">
                    <tr>
                        <td>
                            <asp:Button ID="btnNew" runat="server" CausesValidation="False" Text="New" OnClick="btnNew_Click" /></td>
                        <td>
                            <asp:Button ID="btnEdit" runat="server" CausesValidation="False" Text="Edit" OnClick="btnEdit_Click" /></td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" CausesValidation="False" Text="Delete" OnClick="btnDelete_Click" /></td>
                        
                    </tr>
                    <tr>

                        <td>
                            <asp:Button ID="btnPrint" runat="server" Text="PO" CausesValidation="False" OnClick="btnPrint_Click" /></td>
                        <td>
                            <asp:Button ID="btnp2" runat="server" OnClick="btnp2_Click" Text="Without Prices" />
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="INR PO" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>


 
