<%@ Page Language="C#" MasterPageFile="~/MPs/InventoryMP1.master" AutoEventWireup="true" CodeFile="ChangedIndent.aspx.cs"
     Inherits="Modules_SCM_ChangedIndent" Title="|| Value App : Inventory || Indent|| " %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="pagehead">
        <tr>
            <td class="pagehead" style="text-align:left">
                Indent
                </td>
            <td style="float:left">
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
    
    <table id="tblmain" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td colspan="3" class="searchhead">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">
                            Indent</td>
                        <td>
                        </td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td style="height: 25px">
                                        <asp:Label id="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList id="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem>--</asp:ListItem>
                                            <asp:ListItem Value="IND_No">Indent No</asp:ListItem>
                                            <asp:ListItem Value="IND_Date">Indent Date</asp:ListItem>
                                            <asp:ListItem Value="DEPT_NAME">Department</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME">Customer Name</asp:ListItem>
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
                                    <td style="height: 25px">
                                        <asp:Label id="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:TextBox id="txtSearchValueFromDate" type="datepic" runat="server" EnableTheming="True" Visible="False"
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
                                        <asp:TextBox id="txtSearchValueToDate" type="datepic" runat="server" EnableTheming="True" Visible="False"
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
            <td colspan="3">
                <asp:GridView id="gvIndentDetails" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="sdsIndentDetails" SelectedRowStyle-BackColor="#c0c0c0" OnRowDataBound="gvIndentDetails_RowDataBound" AllowSorting="True">
                    <columns>
<asp:BoundField DataField="IND_ID" SortExpression="IND_ID" HeaderText="IndentIdHidden"></asp:BoundField>
<asp:TemplateField HeaderText="Indent No" SortExpression="IND_NO"><EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("IND_NO") %>' ID="TextBox1"></asp:TextBox>
                            
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnIndentNo" onclick="lbtnIndentNo_Click" runat="server" ForeColor="#0066ff" Text='<%# Eval("IND_NO") %>' CausesValidation="False" __designer:wfdid="w11"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HtmlEncode="False" SortExpression="IND_DATE" DataFormatString="{0:dd/MM/yyyy}" DataField="IND_DATE" HeaderText="Indent Date">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CUST_NAME" SortExpression="CUST_NAME" HeaderText="Customer Name">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="PREPAREDBY" SortExpression="PREPAREDBY" HeaderText="Prepared By"></asp:BoundField>
<asp:BoundField DataField="APPROVEDBY" SortExpression="APPROVEDBY" HeaderText="Approved By"></asp:BoundField>
<asp:BoundField DataField="STATUS" SortExpression="STATUS" HeaderText="Status"></asp:BoundField>
</columns>
                    <emptydatatemplate>
No Data Exist!
</emptydatatemplate>
                </asp:GridView>
                <asp:SqlDataSource id="sdsIndentDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SCM_INDENT_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="EMPID" ControlID="lblEmpIdHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>
</selectparameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: center">
                <table id="Table1" align="center">
                    <tr>
                        <td>
                            <asp:Button id="btnNew" runat="server" CausesValidation="False" onclick="btnNew_Click"
                                Text="New" /></td>
                        <td>
                            <asp:Button id="btnEdit" runat="server" CausesValidation="False" onclick="btnEdit_Click"
                                Text="Edit" /></td>
                        <td>
                            <asp:Button id="btnDelete" runat="server" CausesValidation="False" onclick="btnDelete_Click"
                                Text="Delete" /></td>
                        <td>
                                        <asp:Button id="btnPrint" runat="server" CausesValidation="False" onclick="btnPrint_Click"
                                            Text="Print" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: center">
                <table id="tblIndentDetails" runat="server" border="0" cellpadding="0" cellspacing="0"
                    visible="false" width="100%">
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            General Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label20" runat="server" Text="Indent For"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:RadioButtonList id="rdblIndentfor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdblIndentfor_SelectedIndexChanged"
                                RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Selected="True">Self</asp:ListItem>
                                <asp:ListItem>Customer</asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblIndentNo" runat="server" Text="Indent No"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtIndentNo" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label id="lblPRDate" runat="server" Text="Indent Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtIndentDate" runat="server">
                            </asp:TextBox><asp:Image id="imgIndentDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender
                                ID="ceIndentDate" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgIndentDate"
                                TargetControlID="txtIndentDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditIndentDate" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtIndentDate"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblDepartment" runat="server" Text="Department" Width="103px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlDepartment" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                            </asp:DropDownList><asp:Label id="Label12" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                    id="RequiredFieldValidator5" runat="server" ControlToValidate="ddlDepartment"
                                    ErrorMessage="Please Select the Department" InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label id="lblFollowUp" runat="server" Text="Employee Name" Width="110px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlFollowUp" runat="server">
                                <asp:ListItem>--</asp:ListItem>
                                <asp:ListItem>sdfsf</asp:ListItem>
                            </asp:DropDownList><asp:Label id="Label11" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                    id="RequiredFieldValidator6" runat="server" ControlToValidate="ddlFollowUp" ErrorMessage="Please Select the Employee Name"
                                    InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left">
                            <table id="tblPoDetails" runat="server" border="0" cellpadding="0" cellspacing="0"
                                visible="false" width="100%">
                                <tr>
                                    <td class="profilehead" colspan="3" style="height: 20px">
                            Purchase Order Details :</td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align: center">
                                        <table width="100%">
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label id="Label22" runat="server" Text="Search"></asp:Label></td>
                                                <td style="text-align: left">
                                                    <asp:TextBox id="TextBox2" runat="server">
                            </asp:TextBox><asp:Button id="btngo2" runat="server" BorderStyle="None" CausesValidation="False"
                                                        CssClass="gobutton" EnableTheming="False" OnClick="btngo2_Click" Text="Go" /><asp:SqlDataSource
                                                            id="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                                            SelectCommand="SP_Customer_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                                            <selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" Name="SearchValue" ControlID="TextBox2"></asp:ControlParameter>
</selectparameters>
                                                        </asp:SqlDataSource></td>
                                                <td style="text-align: right">
                                                </td>
                                                <td style="text-align: left">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label id="Label21" runat="server" Text="Requried For"></asp:Label></td>
                                                <td style="text-align: left">
                            <asp:DropDownList id="ddlSupplierName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSupplierName_SelectedIndexChanged" EnableTheming="False" Width="218px">
                            </asp:DropDownList></td>
                                                <td style="text-align: right">
                            <asp:Label id="Label7" runat="server" Text="Purchase Order No"></asp:Label></td>
                                                <td style="text-align: left">
                            <asp:DropDownList id="ddlOrderAcceptance" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOrderAcceptance_SelectedIndexChanged" EnableTheming="False" Width="91px">
                            </asp:DropDownList></td>
                                            </tr>
                                        </table>
                                        <asp:GridView id="gvSalesOrderItems" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSalesOrderItems_RowDataBound" Width="100%">
                                            <footerstyle backcolor="#1AA8BE" />
                                            <columns>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
<asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField DataField="Currency" HeaderText="Currency"></asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
<asp:BoundField HeaderText="Amount"></asp:BoundField>
<asp:BoundField DataField="Price" HeaderText="Spl Price"></asp:BoundField>
<asp:BoundField DataField="Specifications" HeaderText="Specifications"></asp:BoundField>
<asp:BoundField DataFormatString="{0:dd/MM/YYYY}" DataField="DeliveryDate" HeaderText="Delivery Date">
<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Room" HeaderText="Room"></asp:BoundField>
<asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
<asp:BoundField DataField="ColorId" HeaderText="Color Id"></asp:BoundField>
<asp:BoundField DataField="Brand" HeaderText="Brand"></asp:BoundField>
<asp:TemplateField HeaderText="Check"><ItemTemplate>
<asp:CheckBox id="ChkItemSelect" runat="server" __designer:wfdid="w1"></asp:CheckBox> 
</ItemTemplate>
</asp:TemplateField>
</columns>
                                        </asp:GridView>
                            <asp:Button id="btnGo" runat="server" CausesValidation="False" onclick="btnGo_Click"
                                Text="Go" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            &nbsp;Item Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblSearch" runat="server" Text="Search:" Width="84px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtSearchModel" runat="server">
                            </asp:TextBox><asp:RequiredFieldValidator id="RequiredFieldValidator17" runat="server"
                                ControlToValidate="txtSearchModel" ErrorMessage="Please Enter ModelNo For Search"
                                ValidationGroup="Search">*</asp:RequiredFieldValidator><asp:Button id="btnSearchModelNo"
                                    runat="server" BorderStyle="None" CausesValidation="False" CssClass="gobutton"
                                    EnableTheming="False" onclick="btnSearchModelNo_Click" Text="Go" ValidationGroup="Search" /><asp:SqlDataSource
                                        id="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                        SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure"><selectparameters>
<asp:Parameter Type="Int64" DefaultValue="0" Name="BranbId"></asp:Parameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtSearchModel"></asp:ControlParameter>
</selectparameters>
                                    </asp:SqlDataSource>
                        </td>
                        <td style="text-align: right">
                            <asp:Label id="Label8" runat="server" Text="Search By Brand"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlBrandselect" runat="server" OnSelectedIndexChanged="ddlBrandselect_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label id="lblModelName" runat="server" Text="Model No" Width="81px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList id="ddlItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged">
                            </asp:DropDownList><asp:Label id="Label13" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                    id="RequiredFieldValidator1" runat="server" ControlToValidate="ddlItemType" ErrorMessage="Please Select the Model No"
                                    InitialValue="0" ValidationGroup="id">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right;">
                            <asp:Label id="Label2" runat="server" Text="Model Name"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox id="txtModelName" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label id="Label15" runat="server" Text="Item Category"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                            <asp:TextBox id="txtItemCategory" runat="server">
                            </asp:TextBox></td>
                        <td style="height: 24px; text-align: right">
                            <asp:Label id="lblItemGroup" runat="server" Text="Item SubCategory" Width="117px"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                            <asp:TextBox id="txtItemSubCategory" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label19" runat="server" Text=" Color"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlColor" runat="server" OnSelectedIndexChanged="ddlColor_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label id="Label3" runat="server" Text="UOM"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtItemUOM" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label4" runat="server" Text="Quantity"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtQuantity" runat="server">
                            </asp:TextBox><asp:Label id="Label16" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                    id="RequiredFieldValidator3" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Please Enter the Quantity Required"
                                    ValidationGroup="id">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender ID="ftxtQuantity"
                                        runat="server" FilterType="Numbers" TargetControlID="txtQuantity">
                                    </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label id="Label1" runat="server" Text="Quantity In Hand"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtQuantityInHand" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblPriority" runat="server" Text="Priority" Width="65px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlItemPriority" runat="server">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem>Low</asp:ListItem>
                                <asp:ListItem>Medium</asp:ListItem>
                                <asp:ListItem>High</asp:ListItem>
                            </asp:DropDownList><asp:Label id="Label17" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label></td>
                        <td style="text-align: right">
                            <asp:Label id="Label6" runat="server" Text="Brand"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtBrand" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                        <td style="text-align: right">
                            <asp:Label id="Label9" runat="server" Text="Required by Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtReqByDate" runat="server">
                            </asp:TextBox><asp:Image id="imgReqByDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><asp:Label
                                id="Label18" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                    id="RequiredFieldValidator4" runat="server" ControlToValidate="txtReqByDate"
                                    ErrorMessage="Please Select Required By Date" ValidationGroup="id">*</asp:RequiredFieldValidator><asp:CustomValidator
                                        id="CustomValidator5" runat="server" ClientValidationFunction="DateCustomValidate"
                                        ControlToValidate="txtReqByDate" ErrorMessage="Please Enter Required By Date not Less Than the Present Date"
                                        SetFocusOnError="True" ValidationGroup="id">*</asp:CustomValidator><asp:CustomValidator
                                            id="CustomValidator1" runat="server" ClientValidationFunction="DateCustomValidate"
                                            ControlToValidate="txtReqByDate" ErrorMessage="Please Enter the Required By Date in DD/MM/YYYY Format or Check  Year in 2009 to 2099 Range or not"
                                            SetFocusOnError="True" ValidationGroup="id">*</asp:CustomValidator><cc1:CalendarExtender
                                                ID="ceReqByDate" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgReqByDate"
                                                TargetControlID="txtReqByDate">
                                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskededitReqByDate" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtReqByDate"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label10" runat="server" Text="Specification"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox id="txtSpecification" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="88%">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblItemImage" runat="server" Text="Item Image"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:Image id="Image1" runat="server" Height="85px" ImageUrl="~/Images/noimage400x300.gif"
                                Width="140px">
                            </asp:Image></td>
                        <td style="text-align: right">
                            <asp:Label id="Label5" runat="server" Text="Balance Quantity" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtBalanceQty" runat="server" ReadOnly="True" Visible="False">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: right">
                            <asp:Button id="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" onclick="btnAdd_Click" Text="Add"
                                ValidationGroup="id" /></td>
                        <td style="text-align: left">
                            <asp:Button id="btnItemRefresh" runat="server" BackColor="Transparent" BorderStyle="None"
                                CausesValidation="False" CssClass="loginbutton" EnableTheming="False" onclick="btnItemRefresh_Click"
                                Text="Refresh" /></td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView id="gvItemDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvItemDetails_RowDataBound"
                                OnRowDeleting="gvItemDetails_RowDeleting" OnRowEditing="gvItemDetails_RowEditing">
                                <columns>
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
<ItemStyle HorizontalAlign="Center"></ItemStyle>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
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
</columns>
                                <emptydatatemplate>
<SPAN style="COLOR: #ff0000">No Data to Display</SPAN>
</emptydatatemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Reference Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: left">
                        </td>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlPreparedBy" runat="server" Enabled="False" Width="60px">
                                <asp:ListItem>--</asp:ListItem>
                                <asp:ListItem>werwer</asp:ListItem>
                                <asp:ListItem>
                                </asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label id="lblApprovedBy" runat="server" Text="Approved By" Width="96px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlApprovedBy" runat="server" Enabled="False">
                                <asp:ListItem>--</asp:ListItem>
                                <asp:ListItem>abc</asp:ListItem>
                                <asp:ListItem>
                                </asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table id="tblButtons">
                                <tr>
                                    <td>
                                        <asp:Button id="btnSave" runat="server" onclick="btnSave_Click" Text="Save" /></td>
                                    <td>
                                        <asp:Button id="btnRefresh" runat="server" CausesValidation="False" onclick="btnRefresh_Click"
                                            Text="Refresh" /></td>
                                    <td>
                                        <asp:Button id="btnClose" runat="server" CausesValidation="False" onclick="btnClose_Click"
                                            Text="Close" /></td>
                                    <td>
                                        &nbsp;</td>
                                    <td style="width: 3px">
                                        <asp:Button id="btnApprove" runat="server" CausesValidation="False" onclick="btnApprove_Click"
                                            Text="Approve" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 8px">
                        </td>
                    </tr>
                </table>
                <asp:ValidationSummary id="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False">
                </asp:ValidationSummary>
                <asp:ValidationSummary id="ValidationSummary2" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="id">
                </asp:ValidationSummary>

            </td>
        </tr>
    </table>
</asp:Content>


 

