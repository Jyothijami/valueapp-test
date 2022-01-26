<%@ Page Title="|| ValuelineApp : Internal Indent.aspx ||" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="Internal_indent.aspx.cs" Inherits="Modules_Warehouse_Internal_indent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div>

        <table class="auto-style1">
            <tr>
                <td class="profilehead">Internal Indent</td>
                <td style="text-align: right">
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
        <table class="auto-style1">
            <tr>
                <td>
                    <table class="auto-style1" style="text-align: right;">
                        <tr>
                            <td style="text-align: left" colspan="2">Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                                <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
                            </td>
                            <td>
                                <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                    Text="Search By"></asp:Label><%--</td>
                                    <td>--%>
                                <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                    OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                    <asp:ListItem Value="0">--</asp:ListItem>
                                    <asp:ListItem Value="IND_NO">Indent No.</asp:ListItem>
                                    <asp:ListItem Value="IND_DATE">Indent Date</asp:ListItem>
                                    <asp:ListItem Value="ITEM_MODEL_NO">Model No.</asp:ListItem>
                                </asp:DropDownList><%--</td>
                                    <td>--%>
                                <asp:DropDownList ID="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                    EnableTheming="False" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged"
                                    Visible="False" Width="50px">
                                    <asp:ListItem Selected="True">=</asp:ListItem>
                                    <asp:ListItem>&lt;</asp:ListItem>
                                    <asp:ListItem>&gt;</asp:ListItem>
                                    <asp:ListItem>&lt;=</asp:ListItem>
                                    <asp:ListItem>&gt;=</asp:ListItem>
                                    <asp:ListItem>R</asp:ListItem>
                                </asp:DropDownList><%--</td>
                                    <td>--%>
                                <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                </asp:Label><%--</td>
                                    <td>--%>
                                <asp:TextBox ID="txtSearchValueFromDate" runat="server" type="datepic" EnableTheming="True" Visible="False"
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
                                <%-- </td>
                                    <td>--%>
                                <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                </asp:Label><%--</td>
                                    <td style="text-align: left;">--%>
                                <asp:TextBox ID="txtSearchValueToDate" runat="server" type="datepic" EnableTheming="True" Visible="False"
                                    Width="106px">
                                </asp:TextBox>
                                <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True">
                                </asp:TextBox><%--<asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchValueToDate" runat="server"
                                            Enabled="False" PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>--%>
                                <%-- </td>
                                    <td>--%>
                                <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                    CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnSearchGo_Click" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
        </table>
        <table class="auto-style1">
            <tr>
                <td>
                    <asp:GridView ID="gvInternalIndent" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="true" SelectedRowStyle-BackColor="#c0c0c0" DataSourceID="SqlDataSource2" AllowSorting="True">
                        <Columns>
                            <asp:BoundField DataField="INT_INDID" HeaderText="Indent Id" SortExpression="INT_INDID" />
                            <asp:TemplateField SortExpression="IND_NO" HeaderText="Indent No.">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnIndNo" OnClick="lbtnIndNo_Click" ForeColor="#0066ff" runat="server" Text='<%# Bind("IND_NO") %>'
                                        CausesValidation="False" __designer:wfdid="w12"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="IND_DATE" HeaderText="Indent Date" DataFormatString="{0:d}" SortExpression="IND_DATE" />
                            <asp:BoundField DataField="From_Loc" HeaderText="From Location" SortExpression="From_Loc" />
                            <asp:BoundField DataField="To_Loc" HeaderText="To Location" SortExpression="To_Loc" />
                            <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Employee Name" SortExpression="EMP_FIRST_NAME" />
                        </Columns>
                    </asp:GridView>

                    <br />
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="USP_Internal_Indent_Search_Select" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="lblSearchTypeHidden" DefaultValue="0" Name="SearchType" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="lblSearchValueFromHidden" DefaultValue="0" Name="SearchValueFrom" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                </td>
            </tr>
            <tr>
                <td>
                    <table align="center">
                        <tr>
                            <td>
                                <asp:Button ID="btnAddnew" runat="server" Text="Add New" OnClick="btnAddnew_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnprint" runat="server" OnClick="btnprint_Click" Text="Print" />

                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <table width="100%" runat="server" visible="false" id="tblmain">
                                <tr>
                                    <td colspan="4" class="profilehead">Add Internal Details</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label1" runat="server" Text="Indent No :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIndentno" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblCompany" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label2" runat="server" Text="Indent Date :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtindentdate" type="datepic" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label3" runat="server" Text="Indent From :"></asp:Label>
                                    </td>
                                    <td>
                                        <%-- <asp:DropDownList ID="ddlfrom" runat="server">
                                    </asp:DropDownList>--%>
                                        <asp:DropDownList ID="ddlfrom" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource1" DataTextField="whname" DataValueField="wh_id">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="USP_Warehouse_Loc_Select" SelectCommandType="StoredProcedure">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="lblCompany" DefaultValue="0" Name="LocID" PropertyName="Text" Type="String" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlfrom" ErrorMessage="Please Select the From Location " InitialValue="0" ValidationGroup="id">*</asp:RequiredFieldValidator>
                                        <asp:Label ID="Label15" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label4" runat="server" Text="Indent To :"></asp:Label>
                                    </td>
                                    <td>
                                        <%--  <asp:DropDownList ID="ddlto" runat="server">
                                    </asp:DropDownList>--%>
                                        <asp:DropDownList ID="ddlto" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource1" DataTextField="whname" DataValueField="wh_id">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlto" ErrorMessage="Please Select the To Location " InitialValue="0" ValidationGroup="id">*</asp:RequiredFieldValidator>
                                        <asp:Label ID="Label17" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="profilehead">Add Indent Details</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" class="auto-style2"></td>
                                    <td class="auto-style2">
                                        <asp:SqlDataSource
                                            ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                            SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                            <SelectParameters>
                                                <asp:Parameter Type="Int64" DefaultValue="0" Name="BranbId"></asp:Parameter>
                                                <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtModelNo"></asp:ControlParameter>
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </td>
                                    <td style="text-align: right" class="auto-style2">Search Model No :</td>
                                    <td class="auto-style2">
                                        <asp:TextBox ID="txtModelNo" runat="server"></asp:TextBox>
                                        <asp:Button ID="btnSearchModelNo"
                                            runat="server" BorderStyle="None" CausesValidation="False" CssClass="gobutton"
                                            EnableTheming="False" OnClick="btnSearchModelNo_Click" Text="Go" ValidationGroup="Search" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label5" runat="server" Text="Brand :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlBrand" runat="server" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label6" runat="server" Text="Model No :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlModelno" runat="server" OnSelectedIndexChanged="ddlModelno_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlModelno" ErrorMessage="Please Select Model No " InitialValue="0" ValidationGroup="Add" ForeColor="Red">*</asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" class="auto-style3"></td>
                                    <td class="auto-style3">
                                        <asp:Label ID="lblBrandId" runat="server" Visible="False"></asp:Label>
                                    </td>
                                    <td style="text-align: right" class="auto-style3">Brand :</td>
                                    <td class="auto-style3">
                                        <asp:TextBox ID="txtBrand" runat="server"></asp:TextBox>

                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label7" runat="server" Text="Color :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlColor" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlColor" ErrorMessage="Please Select Colour" InitialValue="0" ValidationGroup="Add" ForeColor="Red">*</asp:RequiredFieldValidator>

                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label8" runat="server" Text="Qty :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQty" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtQty" ErrorMessage="Please Select Quantity"  ValidationGroup="Add" ForeColor="Red">*</asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label11" runat="server" Text="Client Name :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtClientName" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtClientName" ErrorMessage="Please Select Client Name"  ValidationGroup="Add" ForeColor="Red">*</asp:RequiredFieldValidator>

                                    </td>
                                    <td style="text-align: right">Remarks :</td>
                                    <td>
                                        <asp:TextBox ID="txtRemark" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label9" runat="server" Text="Description :"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="494px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: right">
                                        <table class="auto-style1">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnadd" runat="server" Text="Add" OnClick="btnadd_Click" ValidationGroup="Add" />
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:Button ID="btnrefresh" runat="server" Text="Refresh" OnClick="btnrefresh_Click" />
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td colspan="2">
                                                    <asp:GridView ID="gvIndentdetails" runat="server" AutoGenerateColumns="False" Style="text-align: center" OnRowDeleting="gvIndentdetails_RowDeleting" Width="100%" OnRowDataBound="gvIndentdetails_RowDataBound">
                                                        <Columns>
                                                            <asp:CommandField ShowDeleteButton="True" />
                                                            <asp:BoundField DataField="ItemCode" HeaderText="Itemcode" />
                                                            <asp:BoundField DataField="Brand" HeaderText="Brand" />
                                                            <asp:BoundField DataField="ModelNo" HeaderText="ModelNo" />
                                                            <asp:BoundField DataField="Color" HeaderText="Color" />
                                                            <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                                            <asp:BoundField DataField="Remarks" HeaderText="Description">
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:BoundField>

                                                            <asp:BoundField DataField="ClientName" HeaderText="ClientName" />
                                                            <asp:BoundField DataField="BrandId" HeaderText="BrandId" />
                                                            <asp:BoundField DataField="ColorId" HeaderText="ColorId" />
                                                            <asp:BoundField DataField="Remark" HeaderText="Remarks" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td colspan="2">
                                                    <table class="auto-style1">
                                                        <tr>
                                                            <td style="text-align: right">
                                                                <asp:Label ID="Label10" runat="server" Text="Prepared By :"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left">
                                                                <asp:DropDownList ID="ddlPreparedBy" runat="server" Style="margin-left: 0px" Enabled="False">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" style="text-align: right">
                                                                <table align="center">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="id" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Button ID="btnrefresh2" runat="server" Text="Refresh" OnClick="btnrefresh2_Click" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Button ID="btnExit" runat="server" Text="Exit" OnClick="btnExit_Click" />
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="id" ShowMessageBox="true" ShowSummary="false" />
                                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="main" ShowMessageBox="true" ShowSummary="false" />

                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>

        </table>

    </div>
</asp:Content>



 
