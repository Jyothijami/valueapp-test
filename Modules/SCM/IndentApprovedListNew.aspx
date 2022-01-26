<%@ Page Title="|| YANTRA : Purchasing Management : IndentApprovalList ||" Language="C#" MasterPageFile="~/MPs/AdminMP1.master" AutoEventWireup="true" CodeFile="IndentApprovedListNew.aspx.cs" Inherits="Modules_SCM_IndentApprovedListNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 25px;
            width: 206px;
        }
        .auto-style2 {
            height: 25px;
        }
    </style>
</asp:Content>
  
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
      

   <%-- <div>--%>
        <table border="0" cellpadding="0" cellspacing="0" id="tblIndentApprovalDetails" runat="server"
                    visible="false" width="100%">
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">General Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right" colspan="4">
                            <table align="right" border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="text-align: left">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td style="text-align: right">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    
                    <tr align ="right">
                        <td colspan="4" align="right">
                            
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr class="searchhead">
                                    <td>
                                           Indent Details 
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True" Text="Search By"></asp:Label>
                                    
                                    
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="ITEM_NAME">Item Name</asp:ListItem>
                                            <asp:ListItem Value="IND_DET_REQ_FOR">Required For</asp:ListItem>
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
                                        <asp:TextBox ID="txtSearchValueFromDate" runat="server" EnableTheming="True" Visible="False" Width="106px">
                                        </asp:TextBox>
                                        <cc1:CalendarExtender ID="ceSearchFrom" runat="server" Enabled="False" Format="dd/MM/yyyy" PopupButtonID="imgFromDate" TargetControlID="txtSearchValueFromDate">
                                        </cc1:CalendarExtender>
                                        <asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False" />
                                        <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label>
                                        <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox>
                                        <cc1:CalendarExtender ID="ceSearchValueToDate" runat="server" Enabled="False" Format="dd/MM/yyyy" PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                                        </cc1:CalendarExtender>
                                        <asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False" />
                                        <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False" CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" />
                                    </td>
                                </tr>
                            </table>
                            
                        </td>
                    </tr>

                    <tr>
                        <td colspan="4">&nbsp;
                            <asp:GridView ID="gvIndentDetails" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" OnRowDataBound="gvIndentDetails_RowDataBound" Width="100%">
                                <Columns>
                                    <asp:CommandField DeleteText="" InsertText="" NewText="" SelectText="" ShowEditButton="True" UpdateText="Update" />
                                    <asp:CommandField ShowDeleteButton="True" />
                                    <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" ReadOnly="true">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ITEM_NAME" HeaderText="Item Name" ReadOnly="True" SortExpression="ITEM_NAME" />
                                    <asp:BoundField DataField="IT_TYPE" HeaderText="Item Type" ReadOnly="True" />
                                    <asp:BoundField DataField="IND_ID" HeaderText="Indent Id" ReadOnly="true" />
                                    <asp:BoundField DataField="UOM_SHORT_DESC" HeaderText="UOM" ReadOnly="True" SortExpression="UOM_SHORT_DESC" />
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty" runat="server" Text='<%# Eval("IND_DET_QTY") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="IND_DET_BRAND" HeaderText="Item Brand" ReadOnly="true">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="IND_DET_REQ_FOR" HeaderText="Required For" ReadOnly="true" />
                                    <asp:BoundField DataField="COLOUR_NAME" HeaderText="Color" ReadOnly="true">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="COLOR_ID" HeaderText="Color Id" ReadOnly="true">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="IND_DET_ID" HeaderText="IND_DET_ID" ReadOnly="True" SortExpression="IND_DET_ID" />
                                    <asp:BoundField DataField="IND_DET_REM_QTY" HeaderText="IND_DET_REM_QTY" ReadOnly="True" SortExpression="IND_DET_REM_QTY" />
                                    <asp:BoundField DataField="IND_DET_ORD_QTY" HeaderText="IND_DET_ORD_QTY" ReadOnly="True" SortExpression="IND_DET_ORD_QTY" />

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
                        <td colspan="4" style="text-align: center">
                            <asp:Button ID="btnGo" runat="server" CausesValidation="False" OnClick="btnGo_Click"
                                Text="Go" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left">
                            <table border="0" cellpadding="0" cellspacing="0" id="Table2" runat="server" visible="true"
                                width="100%">
                                <tr>
                                    <td colspan="4" style="text-align: left" class="profilehead">Indent Approval Details</td>
                                </tr>
                                <tr>
                                    <td style="height: 21px; text-align: right"></td>
                                    <td style="height: 21px; text-align: left"></td>
                                    <td style="height: 21px; text-align: right"></td>
                                    <td style="height: 21px; text-align: left; width: 324px;"></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblIndentApprovalNo" runat="server" Text="Indent Approval  No :" Width="128px"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtApprovalNo" runat="server">
                                        </asp:TextBox></td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblApprovalDate" runat="server" Text="Indent Approval Date :" Width="139px"></asp:Label></td>
                                    <td style="text-align: left; width: 324px;">
                                        <asp:TextBox ID="txtIndentApprovalDate" runat="server">
                                        </asp:TextBox><asp:Image ID="imgIndentApprovalDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>&nbsp;&nbsp;&nbsp;
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="CeIndentApprovalDate" runat="server"
                                            Enabled="True" PopupButtonID="imgIndentApprovalDate" TargetControlID="txtIndentApprovalDate">
                                        </cc1:CalendarExtender>
                                       <%-- <cc1:MaskedEditExtender ID="MaskedEditIndentApprovalDate" runat="server" DisplayMoney="Left"
                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtIndentApprovalDate"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblDepartment" runat="server" Text="Department :" Width="103px"></asp:Label></td>
                                    <td style="text-align: left;">
                                        <asp:DropDownList ID="ddlDepart" runat="server">
                                        </asp:DropDownList><asp:Label ID="Label3" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlDepart"
                                                ErrorMessage="Please Select the Department" InitialValue="0">*</asp:RequiredFieldValidator></td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblFollowUp" runat="server" Text="Employee Name :" Width="111px"></asp:Label></td>
                                    <td style="text-align: left; width: 324px;">
                                        <asp:DropDownList ID="ddlFollowUp" runat="server">
                                            <asp:ListItem>--</asp:ListItem>
                                            <asp:ListItem>sdfsf</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="Label4" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlFollowUp"
                                            ErrorMessage="Please Select the Follow Up" InitialValue="0">*</asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right"></td>
                                    <td style="text-align: left"></td>
                                    <td style="text-align: right"></td>
                                    <td style="text-align: left; width: 324px;"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">&nbsp;Item Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;"></td>
                        <td style="text-align: left; height: 19px;"></td>
                        <td style="text-align: right; height: 19px; width: 157px;"></td>
                        <td style="text-align: left; height: 19px;"></td>
                    </tr>
                    <%-- <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblPriority" runat="server" Text="Priority" Width="65px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlItemPriority" runat="server">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem>Low</asp:ListItem>
                                <asp:ListItem>Medium</asp:ListItem>
                                <asp:ListItem>High</asp:ListItem>
                            </asp:DropDownList><asp:Label ID="Label10" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label></td>
                        <td style="text-align: right; width: 157px;">
                            <asp:Label ID="lblSpecification" runat="server" Text="Specification"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtSpecification" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="47%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblForDate" runat="server" Text="Required by Date"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtReqByDate" runat="server"></asp:TextBox><asp:Image ID="imgReqByDate"
                                runat="server" ImageUrl="~/Images/Calendar.png" /><asp:Label ID="Label11" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtReqByDate"
                                ErrorMessage="Please Select Required By Date" ValidationGroup="id">*</asp:RequiredFieldValidator><asp:CustomValidator
                                    id="CustomValidator5" runat="server" ClientValidationFunction="DateCustomValidate"
                                    ControlToValidate="txtReqByDate" ErrorMessage="Please Enter the Required By Date in DD/MM/YYYY Format or Check  Year in 2009 to 2099 Range or not"
                                    SetFocusOnError="True" ValidationGroup="id">*</asp:CustomValidator><cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceReqByDate" runat="server" Enabled="True"
                                PopupButtonID="imgReqByDate" TargetControlID="txtReqByDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskededitReqByDate" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtReqByDate"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                            &nbsp;</td>
                        <td style="text-align: right; width: 157px;">
                            <asp:Label ID="Label12" runat="server" Text="Requried For"></asp:Label>
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList id="ddlSuppliers" runat="server">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtSupplierName" runat="server" ReadOnly="True" Visible="False"></asp:TextBox></td>
                    </tr>--%>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left;"></td>
                        <td style="text-align: right; width: 157px;"></td>
                        <td style="text-align: left;"></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="gvApprlItemDetails" runat="server" AutoGenerateColumns="False"
                                OnRowDeleting="gvApprlItemDetails_RowDeleting" OnRowDataBound="gvApprlItemDetails_RowDataBound" Width="100%">
                                <Columns>
                                    <%--<asp:BoundField DataField="ReqFor" HeaderText="Room">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:/MM/dd/yyyy}" DataField="ReqDate" SortExpression="ReqDate" HeaderText="Required by Date"></asp:BoundField>
<asp:BoundField DataField="Specification" HeaderText="Specification">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Priority" HeaderText="Priority"></asp:BoundField>
<asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
<asp:BoundField DataField="ColorId" HeaderText="ColorId"></asp:BoundField>--%>
                                    <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Itemname" HeaderText="Item Name">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Itemtype" HeaderText="Item Type" />
                                    <asp:BoundField DataField="Indentid" HeaderText="Indent Id">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM" />
                                    <asp:BoundField DataField="Quantity" HeaderText="Item Quantity"></asp:BoundField>
                                    <asp:BoundField DataField="Brand" HeaderText="Item Brand"></asp:BoundField>
                                    <asp:BoundField DataField="Requiredfor" HeaderText="Required For"></asp:BoundField>
                                    <asp:BoundField DataField="Color" HeaderText="Color">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>

                                    <asp:BoundField DataField="ColorId" HeaderText="ColorId">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="IndentdetId" HeaderText="Indentdet Id">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RemQty" HeaderText="RemQty"></asp:BoundField>
                                    <asp:BoundField DataField="OrdQty" HeaderText="OrdQty"></asp:BoundField>

                                </Columns>
                            </asp:GridView>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table style="width: 100%">
                                <tr>
                                    <td id="TD16" class="profilehead" colspan="4" style="text-align: left">Supplier Details</td>
                                </tr>
                                <tr>
                                    <td id="TD5" style="text-align: right" class="auto-style1"></td>
                                    <td style="text-align: left" class="auto-style1"></td>
                                    <td style="text-align: right" class="auto-style1"></td>
                                    <td style="text-align: left" class="auto-style1"></td>
                                </tr>
                                <tr>
                                    <td id="TD18" style="text-align: right;">
                                        <asp:Label ID="lblCustomer" runat="server" Text="Supplier Name"></asp:Label></td>
                                    <td style="text-align: left;">
                                        <asp:DropDownList ID="ddlSupplierName" runat="server" OnSelectedIndexChanged="ddlSupplierName_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:Label ID="Label10" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlSupplierName"
                                            ErrorMessage="Please Select the Supplier Name" ValidationGroup="s" InitialValue="0">*</asp:RequiredFieldValidator></td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person"></asp:Label></td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtContactPerson" runat="server" ReadOnly="True"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td id="TD28" style="text-align: right">
                                        <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" ReadOnly="True"></asp:TextBox></td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No" Width="74px"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtPhoneNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td id="TD2" style="text-align: right;">
                                        <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True"></asp:TextBox></td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblCity" runat="server" Text="Mobile No" Width="81px"></asp:Label></td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td id="TD36"></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td style="text-align: right">
                                        <asp:Button ID="btnSuppDetails" runat="server" BackColor="Transparent" BorderStyle="None"
                                            CssClass="loginbutton" EnableTheming="False" Text="Add" OnClick="btnSuppDetails_Click" ValidationGroup="s" /></td>
                                    <td style="text-align: left">
                                        <asp:Button ID="btnSuppRefresh" runat="server" BackColor="Transparent" BorderStyle="None"
                                            CssClass="loginbutton" EnableTheming="False" Text="Refresh" OnClick="btnSuppRefresh_Click" CausesValidation="False" /></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td id="TD17" class="profilehead" colspan="4" style="text-align: left">Supplier Contact Person Details</td>
                                </tr>
                                <tr>
                                    <td id="TD6" runat="server" colspan="4">
                                        <asp:GridView ID="gvSupplierDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSupplierDetails_RowDataBound" OnRowDeleting="gvSupplierDetails_RowDeleting" Width="100%">
                                            <Columns>
                                                <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                                <asp:BoundField DataField="SuppId" HeaderText="SuppIdHidden"></asp:BoundField>
                                                <asp:BoundField DataField="Name" HeaderText="Name">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PhoneNo" HeaderText="Phone No"></asp:BoundField>
                                                <asp:BoundField DataField="Email" HeaderText="Email">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <span style="color: #ff0000">No Data to Display </span>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">Reference Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left;"></td>
                        <td style="text-align: right; width: 157px;"></td>
                        <td style="text-align: left;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                                <asp:ListItem>--</asp:ListItem>
                                <asp:ListItem>werwer</asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="text-align: right; width: 157px;">
                            <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By" Width="96px" Visible="False"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlApprovedBy" runat="server" Enabled="False" Visible="False">
                                <asp:ListItem>--</asp:ListItem>
                                <asp:ListItem>abc</asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;"></td>
                        <td style="text-align: left; height: 19px;"></td>
                        <td style="text-align: right; height: 19px; width: 157px;"></td>
                        <td style="text-align: left; height: 19px;"></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table id="tblButtons" align="center">
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
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False"></asp:ValidationSummary>
         <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="id"></asp:ValidationSummary>
         
             <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1" Visible="False"></asp:Label>
             <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1" Visible="False"></asp:Label>
             <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
             <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
             <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
             <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
             <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
                                                            
   <%-- </div>--%>
    
</asp:Content>
       

 
