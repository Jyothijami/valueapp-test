<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Indent.aspx.cs" Inherits="Modules_SCM_Indent" Title="|| YANTRA : Purchasing Management : Indent ||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <%--<script type="text/javascript" language="javascript">
function balqtycalc()
{
    var req_qty,hand_qty;
    req_qty=document.getElementById('<%=txtQuantity.ClientID %>').value;
    hand_qty=document.getElementById('<%=txtQuantityInHand.ClientID %>').value;
    if(req_qty=="" || req_qty=="0")
    {
        document.getElementById('<%=txtBalanceQty.ClientID %>').value="0";
    }
    else if(req_qty>0)
    {
        if(hand_qty>0)
        {
            document.getElementById('<%=txtBalanceQty.ClientID %>').value=hand_qty-req_qty;
        }
        else if(hand_qty==0)
        {
            document.getElementById('<%=txtBalanceQty.ClientID %>').value="0";
        }
    }
}

    </script>
--%>
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                INDENT</td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" id="table" runat="server" visible="true"
        style="width: 750px">
        <tr>
            <td class="searchhead" colspan="4">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">
                            Indent</td>
                        <td>
                        </td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="height: 25px">
                                        <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem>--</asp:ListItem>
                                            <asp:ListItem Value="IND_No">Indent 
                                            </asp:ListItem>
                                            <asp:ListItem Value="IND_Date">Indent Date</asp:ListItem>
                                            <asp:ListItem Value="DEPT_NAME">Department</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="height: 25px">
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
                                    <td style="height: 25px">
                                        <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchValueFromDate" runat="server" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox><asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender  Format="dd/MM/yyyy"  ID="ceSearchFrom" runat="server" Enabled="False" PopupButtonID="imgFromDate"
                                            TargetControlID="txtSearchValueFromDate">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender  Format="dd/MM/yyyy"  ID="ceSearchValueToDate" runat="server" Enabled="False" PopupButtonID="imgToDate"
                                            TargetControlID="txtSearchText">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                                </tr>
                                </table>
                            <asp:Label id="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label><asp:Label id="lblEmpIdHidden" runat="server" Visible="False"></asp:Label><asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <asp:GridView id="gvIndentDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="sdsIndentDetails" OnRowDataBound="gvIndentDetails_RowDataBound">
                    <columns>
<asp:BoundField DataField="IND_ID" HeaderText="IndentIdHidden"></asp:BoundField>
<asp:TemplateField HeaderText="Indent No"><EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("IND_NO") %>' ID="TextBox1"></asp:TextBox>
                            
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnIndentNo" onclick="lbtnIndentNo_Click" runat="server" Text='<%# Eval("IND_NO") %>' CausesValidation="False"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="IND_DATE" HeaderText="Indent Date">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="DEPT_NAME" HeaderText="Department">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="PREPAREDBY" HeaderText="Prepared By"></asp:BoundField>
<asp:BoundField DataField="APPROVEDBY" HeaderText="Approved By"></asp:BoundField>
<asp:BoundField DataField="STATUS" HeaderText="Status"></asp:BoundField>
</columns>
                    <emptydatatemplate>
No Data Exist!
</emptydatatemplate>
                </asp:GridView>&nbsp;
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
            <td style="text-align: right;">
            </td>
            <td style="text-align: left;">
            </td>
            <td style="text-align: right;">
            </td>
            <td style="text-align: right;">
            </td>
            <td style="text-align: left;">
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table id="Table1">
                    <tr>
                        <td>
                            <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CausesValidation="False" /></td>
                        <td>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CausesValidation="False" /></td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"
                                CausesValidation="False" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table border="0" cellpadding="0" cellspacing="0" id="tblIndentDetails" runat="server"
                    visible="false" width="100%">
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">
                            General Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="width: 321px; text-align: left">
                        </td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 20px;">
                            <asp:Label id="Label8" runat="server" Text="Indent For"></asp:Label></td>
                        <td style="text-align: left; width: 321px; height: 20px;">
                            <asp:RadioButton id="rdbself" runat="server" Text="For Self" OnCheckedChanged="rdbself_CheckedChanged" Checked="True" GroupName="a" AutoPostBack="True">
                            </asp:RadioButton>
                            <asp:RadioButton id="rdbCustomer" runat="server" Text="For Customer" OnCheckedChanged="rdbCustomer_CheckedChanged" GroupName="a" AutoPostBack="True">
                            </asp:RadioButton></td>
                        <td style="text-align: right; height: 20px;">
                        </td>
                        <td style="text-align: left; height: 20px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblIndentNo" runat="server" Text="Indent No"></asp:Label></td>
                        <td style="text-align: left; width: 321px;">
                            <asp:TextBox ID="txtIndentNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblPRDate" runat="server" Text="Indent Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtIndentDate" runat="server"></asp:TextBox><asp:Image ID="imgIndentDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender
                            Format="dd/MM/yyyy"    ID="ceIndentDate" runat="server" Enabled="True" PopupButtonID="imgIndentDate"
                                TargetControlID="txtIndentDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditIndentDate" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtIndentDate"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblDepartment" runat="server" Text="Department" Width="103px"></asp:Label></td>
                        <td style="text-align: left; width: 321px;">
                            <asp:DropDownList ID="ddlDepartment" runat="server">
                            </asp:DropDownList><asp:Label id="Label12" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                ControlToValidate="ddlDepartment" ErrorMessage="Please Select the Department"
                                InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblFollowUp" runat="server" Text="Employee Name" Width="110px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlFollowUp" runat="server">
                                <asp:ListItem>--</asp:ListItem>
                                <asp:ListItem>sdfsf</asp:ListItem>
                            </asp:DropDownList><asp:Label id="Label11" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                ControlToValidate="ddlFollowUp" ErrorMessage="Please Select the Employee Name" InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left">
                            <table id="tblPoDetails" runat="server" border="0" cellpadding="0" cellspacing="0" visible="false">
                                <tr>
                                    <td class="profilehead" colspan="4">
                            Purchase Order Details :</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label id="Label20" runat="server" Text="Requried For"></asp:Label></td>
                                    <td style="width: 20px; text-align: left">
                                        <asp:DropDownList id="ddlSupplierName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSupplierName_SelectedIndexChanged">
                                        </asp:DropDownList></td>
                                    <td style="text-align: right">
                                        <asp:Label id="Label21" runat="server" Text="Purchase Order No" Width="132px">
                            </asp:Label></td>
                                    <td style="width: 100px; text-align: left">
                                        <asp:DropDownList id="ddlOrderAcceptance" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOrderAcceptance_SelectedIndexChanged">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: right">
                                        <asp:GridView id="gvOrderAcceptanceItems" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvOrderAcceptanceItems_RowDataBound1"
                                            OnRowDeleting="gvOrderAcceptanceItems_RowDeleting" OnRowEditing="gvOrderAcceptanceItems_RowEditing"
                                            Width="100%">
                                            <columns>
<asp:CommandField ShowEditButton="True"></asp:CommandField>
<asp:CommandField ShowDeleteButton="True"></asp:CommandField>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
<asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
<asp:BoundField HeaderText="Amount"></asp:BoundField>
<asp:BoundField DataField="Specifications" HeaderText="Specifications"></asp:BoundField>
<asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
<asp:BoundField DataField="Priority" HeaderText="Priority"></asp:BoundField>
<asp:BoundField DataField="DeliveryDate" HeaderText="Delivery Date"></asp:BoundField>
<asp:BoundField DataField="Room" HeaderText="Room"></asp:BoundField>
<asp:BoundField DataField="Price" HeaderText="Price"></asp:BoundField>
<asp:BoundField DataField="Brand" HeaderText="Brand"></asp:BoundField>
<asp:BoundField HeaderText="Ordered Qty"></asp:BoundField>
<asp:BoundField HeaderText="Pending Qty"></asp:BoundField>
<asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
<asp:BoundField DataField="ColorId" HeaderText="ColorId"></asp:BoundField>
<asp:TemplateField><ItemTemplate>
<asp:CheckBox id="ChkItemSelect" runat="server"></asp:CheckBox> 
</ItemTemplate>
</asp:TemplateField>
</columns>
                                            <emptydatatemplate>
No Data to Display.&nbsp; 
</emptydatatemplate>
                                        </asp:GridView><asp:Button id="btnGo" runat="server" CausesValidation="False" onclick="btnGo_Click"
                                            Text="Go" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">
                            &nbsp;
                            &nbsp;Item Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;">
                            <asp:Label id="lblSearch" runat="server" Text="Search:" Width="84px"></asp:Label></td>
                        <td style="text-align: left; height: 19px; width: 321px;">
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
                        <td style="text-align: right; height: 19px;">
                        </td>
                        <td style="text-align: left; height: 19px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblModelName" runat="server" Text="Model No" Width="81px"></asp:Label></td>
                        <td style="text-align: left; width: 321px;">
                            <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged">
                            </asp:DropDownList><asp:Label id="Label13" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="ddlItemType" ErrorMessage="Please Select the Model No" InitialValue="0"
                                ValidationGroup="id">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label2" runat="server" Text="Model Name"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtModelName" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label id="Label15" runat="server" Text="Item Category"></asp:Label></td>
                        <td style="text-align: left; width: 321px; height: 24px;">
                            <asp:TextBox id="txtItemCategory" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label ID="lblItemGroup" runat="server" Text="Item SubCategory" Width="117px"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox id="txtItemSubCategory" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label19" runat="server" Text=" Color"></asp:Label></td>
                        <td style="width: 321px; text-align: left">
                            <asp:DropDownList id="ddlColor" runat="server">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label3" runat="server" Text="UOM"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtItemUOM" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label4" runat="server" Text="Quantity"></asp:Label></td>
                        <td style="text-align: left; width: 321px;">
                            <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox><asp:Label id="Label16" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Please Enter the Quantity Required"
                                ValidationGroup="id">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender ID="ftxtQuantity" runat="server" FilterType="Numbers"
                                TargetControlID="txtQuantity">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label1" runat="server" Text="Quantity In Hand"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtQuantityInHand" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblPriority" runat="server" Text="Priority" Width="65px"></asp:Label></td>
                        <td style="text-align: left; width: 321px;">
                            <asp:DropDownList ID="ddlItemPriority" runat="server">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem>Low</asp:ListItem>
                                <asp:ListItem>Medium</asp:ListItem>
                                <asp:ListItem>High</asp:ListItem>
                            </asp:DropDownList><asp:Label id="Label17" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label6" runat="server" Text="Brand"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtBrand" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            </td>
                        <td style="text-align: left; width: 321px;">
                            </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label9" runat="server" Text="Required by Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtReqByDate" runat="server"></asp:TextBox><asp:Image ID="imgReqByDate"
                                runat="server" ImageUrl="~/Images/Calendar.png" /><asp:Label id="Label18" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                    runat="server" ControlToValidate="txtReqByDate" ErrorMessage="Please Select Required By Date"
                                    ValidationGroup="id">*</asp:RequiredFieldValidator><asp:CustomValidator id="CustomValidator5"
                                        runat="server" ClientValidationFunction="DateCustomValidate" ControlToValidate="txtReqByDate"
                                        ErrorMessage="Please Enter Required By Date not Less Than the Present Date" SetFocusOnError="True"
                                        ValidationGroup="id">*</asp:CustomValidator><asp:CustomValidator id="CustomValidator1"
                                            runat="server" ClientValidationFunction="DateCustomValidate" ControlToValidate="txtReqByDate"
                                            ErrorMessage="Please Enter the Required By Date in DD/MM/YYYY Format or Check  Year in 2009 to 2099 Range or not"
                                            SetFocusOnError="True" ValidationGroup="id">*</asp:CustomValidator><cc1:CalendarExtender  Format="dd/MM/yyyy"  ID="ceReqByDate" runat="server" Enabled="True" PopupButtonID="imgReqByDate"
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
                            <asp:Label ID="Label10" runat="server" Text="Specification"></asp:Label></td>
                        <td style="text-align: left" colspan="3">
                            <asp:TextBox ID="txtSpecification" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="88%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblItemImage" runat="server" Text="Item Image"></asp:Label></td>
                        <td style="text-align: left; width: 321px;">
                            <asp:Image id="Image1" runat="server" Height="85px" ImageUrl="~/Images/noimage400x300.gif"
                                Width="140px">
                            </asp:Image></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label5" runat="server" Text="Balance Quantity" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtBalanceQty" runat="server" ReadOnly="True" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                        </td>
                        <td style="text-align: right; width: 321px;">
                            <asp:Button ID="btnAdd" runat="server" Text="Add" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" OnClick="btnAdd_Click" ValidationGroup="id" /></td>
                        <td style="text-align: left;">
                            <asp:Button ID="btnItemRefresh" runat="server" Text="Refresh" BackColor="Transparent"
                                BorderStyle="None" CssClass="loginbutton" EnableTheming="False" OnClick="btnItemRefresh_Click" CausesValidation="False" /></td>
                        <td style="text-align: left;">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left; width: 321px;">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="gvItemDetails" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvItemDetails_RowDeleting" OnRowDataBound="gvItemDetails_RowDataBound" OnRowEditing="gvItemDetails_RowEditing">
                                <Columns>
<asp:CommandField ShowEditButton="True"></asp:CommandField>
<asp:CommandField ShowDeleteButton="True"></asp:CommandField>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Model No">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ItemType" HeaderText="Model Name"></asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity">
<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
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
</Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;">
                        </td>
                        <td style="text-align: left; height: 19px; width: 321px;">
                        </td>
                        <td style="text-align: right; height: 19px;">
                        </td>
                        <td style="text-align: left; height: 19px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">
                            Reference Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: left; width: 321px;">
                        </td>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left; width: 321px;">
                            <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False" Width="60px">
                                <asp:ListItem>--</asp:ListItem>
                                <asp:ListItem>werwer</asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By" Width="96px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlApprovedBy" runat="server" Enabled="False">
                                <asp:ListItem>--</asp:ListItem>
                                <asp:ListItem>abc</asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;">
                        </td>
                        <td style="text-align: left; height: 19px; width: 321px;">
                        </td>
                        <td style="text-align: right; height: 19px;">
                        </td>
                        <td style="text-align: left; height: 19px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 61px">
                            <table id="tblButtons">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click"
                                            CausesValidation="False" /></td>
                                    <td>
                                        <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" CausesValidation="False" /></td>
                                    <td>
                                        <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Print" CausesValidation="False" /></td>
                                    <td style="width: 3px">
                                        <asp:Button ID="btnApprove" runat="server" OnClick="btnApprove_Click" Text="Approve" CausesValidation="False" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False"></asp:ValidationSummary>
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="id"></asp:ValidationSummary>
                            </td>
        </tr>
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
    </table>
</asp:Content>

 
