<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/InventoryMP1.master" AutoEventWireup="true" CodeFile="DisplayIndentRequest.aspx.cs" Inherits="Modules_Inventory_DisplayIndentRequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        // Let's use a lowercase function name to keep with JavaScript conventions
        function selectAll(invoker) {
            // Since ASP.NET checkboxes are really HTML input elements
            //  let's get all the inputs 
            var inputElements = document.getElementsByTagName('input');

            for (var i = 0 ; i < inputElements.length ; i++) {
                var myElement = inputElements[i];

                // Filter through the input types looking for checkboxes
                if (myElement.type === "checkbox") {

                    // Use the invoker (our calling element) as the reference 
                    //  for our checkbox status
                    myElement.checked = invoker.checked;
                }
            }
        }
</script>
    <style>
        .RadioIndentFor input {
            margin-left: 10px !important;
        }

        .auto-style1 {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="updatepanel1" runat="server">
        <ContentTemplate>
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
    </script>
    <table id="tblIndentDetails" runat="server" border="0" cellpadding="0" cellspacing="0"
        visible="false" width="100%">
        <tr>
            <td class="profilehead" colspan="4" style="text-align: left">General Details</td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label20" runat="server" Text="Indent For"></asp:Label></td>
            <td style="text-align: left; padding-left: 10px;">
                <asp:RadioButtonList ID="rdblIndentfor" CssClass="RadioIndentFor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdblIndentfor_SelectedIndexChanged"
                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Selected="True">Display</asp:ListItem>
                    <asp:ListItem>Customer</asp:ListItem>
                </asp:RadioButtonList></td>
            <td style="text-align: right"></td>
            <td style="text-align: left"></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="lblIndentNo" runat="server" Text="Indent No"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtIndentNo" runat="server" ReadOnly="True">
                </asp:TextBox>&nbsp;</td>
            <td style="text-align: right">
                <asp:Label ID="lblPRDate" runat="server" Text="Indent Date"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtIndentDate" runat="server" type="datepic">
                </asp:TextBox>
                <%--  <asp:Image ID="imgOnDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>
                        <cc1:CalendarExtender ID="ceReceivedDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgOnDate"
                            TargetControlID="txtIndentDate">
                        </cc1:CalendarExtender>--%>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="lblDepartment" runat="server" Text="Department" Width="103px"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                </asp:DropDownList><asp:Label ID="Label12" runat="server" EnableTheming="False" Font-Bold="False"
                    Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlDepartment"
                        ErrorMessage="Please Select the Department" InitialValue="0" ValidationGroup="main">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right">
                <asp:Label ID="lblFollowUp" runat="server" Text="Employee Name" Width="110px"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlFollowUp" runat="server">
                    <asp:ListItem>--</asp:ListItem>
                    <asp:ListItem>sdfsf</asp:ListItem>
                </asp:DropDownList><asp:Label ID="Label11" runat="server" EnableTheming="False" Font-Bold="False"
                    Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlFollowUp" ErrorMessage="Please Select the Employee Name"
                        InitialValue="0" ValidationGroup="main">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td colspan="4">
                <table style="width: 100%" runat="server" id="tblReleasedItems" visible="false">
                    <tr>
                        <td class="profilehead">Released Items In DC 
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView Width="100%" ID="gvReleasedItems" runat="server"></asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>


        </tr>
        <tr>
            <td colspan="4" style="text-align: left">
                <table id="tblPoDetails" runat="server" border="0" cellpadding="0" cellspacing="0"
                    visible="false" width="100%">
                    <tr>
                        <td class="profilehead" colspan="3" style="height: 20px">Purchase Order Details :</td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center">
                            <table width="100%">
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label22" runat="server" Text="Customer Search :"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="TextBox2" runat="server">
                                        </asp:TextBox><asp:Button ID="btngo2" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" OnClick="btngo2_Click" Text="Go" /><asp:SqlDataSource
                                                ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                                SelectCommand="SP_Customer_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                                <SelectParameters>
                                                    <asp:ControlParameter PropertyName="Text" Type="String" Name="SearchValue" ControlID="TextBox2"></asp:ControlParameter>
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                    </td>
                                    <td style="text-align: right"></td>
                                    <td style="text-align: left"></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label21" runat="server" Text="Requried For"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlSupplierName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSupplierName_SelectedIndexChanged" EnableTheming="False" Width="218px">
                                        </asp:DropDownList></td>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label7" runat="server" Text="Purchase Order No" Visible ="false" ></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlOrderAcceptance" Visible ="false"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOrderAcceptance_SelectedIndexChanged" EnableTheming="False">
                                        </asp:DropDownList></td>
                                </tr>
                            </table>
                            <asp:GridView ID="gvSalesOrderItems" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSalesOrderItems_RowDataBound" Width="100%">
                                <FooterStyle BackColor="#1AA8BE" />
                                <Columns>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                    <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                    <%-- <asp:TemplateField HeaderText="Quantity" ControlStyle-Width="80px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
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
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="cbSelectAll" runat="server" Text="All" OnClick="selectAll(this)" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkItemSelect" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ResStatus" HeaderText="ResStatus"></asp:BoundField>
                                    <asp:BoundField DataField ="SO_Det_Id" HeaderText ="SoDetId" />
                                </Columns>
                            </asp:GridView>
                            <asp:Button ID="btnGo" runat="server" CausesValidation="False" OnClick="btnGo_Click"
                                Text="Go" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="profilehead" colspan="4" style="text-align: left">&nbsp;Item Details</td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="lblSearch" runat="server" Text="Model Search:" Width="84px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtSearchModel" runat="server">
                </asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server"
                    ControlToValidate="txtSearchModel" ErrorMessage="Please Enter ModelNo For Search"
                    ValidationGroup="Search">*</asp:RequiredFieldValidator><asp:Button ID="btnSearchModelNo"
                        runat="server" BorderStyle="None" CausesValidation="False" CssClass="gobutton"
                        EnableTheming="False" OnClick="btnSearchModelNo_Click" Text="Go" ValidationGroup="Search" /><asp:SqlDataSource
                            ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                            SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:Parameter Type="Int64" DefaultValue="0" Name="BranbId"></asp:Parameter>
                                <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtSearchModel"></asp:ControlParameter>
                            </SelectParameters>
                        </asp:SqlDataSource>
            </td>
            <td style="text-align: right">
                <asp:Label ID="Label8" runat="server" Text="Search By Brand"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlBrandselect" runat="server" OnSelectedIndexChanged="ddlBrandselect_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label ID="lblModelName" runat="server" Text="Model No" Width="81px"></asp:Label></td>
            <td style="text-align: left;">
                <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged">
                </asp:DropDownList><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlItemType" ErrorMessage="Please Select the Model No"
                        InitialValue="0" ValidationGroup="item">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right;">
                <asp:Label ID="Label2" runat="server" Text="Model Name"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtModelName" runat="server">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="height: 24px; text-align: right">
                <asp:Label ID="Label15" runat="server" Text="Item Category"></asp:Label></td>
            <td style="height: 24px; text-align: left">
                <asp:TextBox ID="txtItemCategory" runat="server">
                </asp:TextBox></td>
            <td style="height: 24px; text-align: right">
                <asp:Label ID="lblItemGroup" runat="server" Text="Item SubCategory" Width="117px"></asp:Label></td>
            <td style="height: 24px; text-align: left">
                <asp:TextBox ID="txtItemSubCategory" runat="server">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label19" runat="server" Text=" Color"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlColor" runat="server" OnSelectedIndexChanged="ddlColor_SelectedIndexChanged" AutoPostBack="false">
                </asp:DropDownList>
                <asp:RequiredFieldValidator
                    ID="rfv1" runat="server" ControlToValidate="ddlColor" ErrorMessage="Please Enter the Color"
                    ValidationGroup="item">*</asp:RequiredFieldValidator>
            </td>
            <td style="text-align: right">
                <asp:Label ID="Label3" runat="server" Text="UOM"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtItemUOM" runat="server" ReadOnly="True">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right" class="auto-style1">
                <asp:Label ID="Label4" runat="server" Text="Quantity"></asp:Label></td>
            <td style="text-align: left" class="auto-style1">
                <asp:TextBox ID="txtQuantity" runat="server">
                </asp:TextBox><asp:Label ID="Label16" runat="server" EnableTheming="False" Font-Bold="False"
                    Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Please Enter the Quantity Required"
                        ValidationGroup="item">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender ID="ftxtQuantity"
                            runat="server" FilterType="Numbers" TargetControlID="txtQuantity">
                        </cc1:FilteredTextBoxExtender>
            </td>
            <td style="text-align: right" class="auto-style1">
                <asp:Label ID="Label1" runat="server" Text="Quantity In Hand"></asp:Label></td>
            <td style="text-align: left" class="auto-style1">
                <asp:TextBox ID="txtQuantityInHand" runat="server" ReadOnly="True">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="lblPriority" runat="server" Text="Priority" Width="65px"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlItemPriority" runat="server">
                    <asp:ListItem Value="0">--</asp:ListItem>
                    <asp:ListItem>Low</asp:ListItem>
                    <asp:ListItem>Medium</asp:ListItem>
                    <asp:ListItem>High</asp:ListItem>
                </asp:DropDownList><asp:Label ID="Label17" runat="server" EnableTheming="False" Font-Bold="False"
                    Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label></td>
            <td style="text-align: right">
                <asp:Label ID="Label6" runat="server" Text="Brand"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtBrand" runat="server">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">Remarks :</td>
            <td style="text-align: left">
                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Height="70px" Width="430px"></asp:TextBox>
            </td>
            <td style="text-align: right">
                <asp:Label ID="Label9" runat="server" Text="Required by Date"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtReqByDate" type="datepic" runat="server">
                </asp:TextBox>
                <asp:Label ID="Label18" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtReqByDate"
                        ErrorMessage="Please Select Required By Date" ValidationGroup="id">*</asp:RequiredFieldValidator><asp:CustomValidator
                            ID="CustomValidator5" runat="server" ClientValidationFunction="DateCustomValidate"
                            ControlToValidate="txtReqByDate" ErrorMessage="Please Enter Required By Date not Less Than the Present Date"
                            SetFocusOnError="True" ValidationGroup="id">*</asp:CustomValidator><asp:CustomValidator
                                ID="CustomValidator1" runat="server" ClientValidationFunction="DateCustomValidate"
                                ControlToValidate="txtReqByDate" ErrorMessage="Please Enter the Required By Date in DD/MM/YYYY Format or Check  Year in 2009 to 2099 Range or not"
                                SetFocusOnError="True" ValidationGroup="id">*</asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label10" runat="server" Text="Specification"></asp:Label></td>
            <td colspan="3" style="text-align: left">
                <asp:TextBox ID="txtSpecification" runat="server" CssClass="multilinetext" EnableTheming="False"
                    TextMode="MultiLine" Width="88%">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="lblItemImage" runat="server" Text="Item Image"></asp:Label></td>
            <td style="text-align: left">
                <asp:Image ID="Image1" runat="server" Height="85px" ImageUrl="~/Images/noimage400x300.gif"
                    Width="120px"></asp:Image></td>
            <td style="text-align: right">
                <asp:Label ID="Label5" runat="server" Text="Balance Quantity" Visible="False"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtBalanceQty" runat="server" ReadOnly="True" Visible="False">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right"></td>
            <td style="text-align: right">
                <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                    CssClass="loginbutton" EnableTheming="False" OnClick="btnAdd_Click" Text="Add"
                    ValidationGroup="item" /></td>
            <td style="text-align: left">
                <asp:Button ID="btnItemRefresh" runat="server" BackColor="Transparent" BorderStyle="None"
                    CausesValidation="False" CssClass="loginbutton" EnableTheming="False" OnClick="btnItemRefresh_Click"
                    Text="Refresh" /></td>
            <td style="text-align: left"></td>
        </tr>
        <tr>
            <td style="height: 19px; text-align: right"></td>
            <td style="height: 19px; text-align: left"></td>
            <td style="height: 19px; text-align: right"></td>
            <td style="height: 19px; text-align: left"></td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvItemDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvItemDetails_RowDataBound"
                    OnRowDeleting="gvItemDetails_RowDeleting" OnRowEditing="gvItemDetails_RowEditing" Width="100%">
                    <Columns>
                        <%--<asp:CommandField ShowEditButton="True"></asp:CommandField>
                        <asp:CommandField ShowDeleteButton="True"></asp:CommandField>--%>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton Text="Edit" ID="lnkEdit" CommandName="Edit" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton Text="Delete" ID="lnkDelete" runat="server" CommandName="Delete" />
                            </ItemTemplate>
                        </asp:TemplateField>
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
                        <%-- <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </asp:BoundField>--%>
                        <asp:TemplateField HeaderText="Quantity" ControlStyle-Width="80px">
                            <ItemTemplate>
                                <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
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
                        <asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
                        <asp:BoundField DataField ="So_Det_Id" HeaderText ="SoDetId" />
                    </Columns>
                    <EmptyDataTemplate>
                        <span style="color: #ff0000">No Data to Display</span>
                    </EmptyDataTemplate>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="text-align: right"></td>
            <td style="text-align: left"></td>
            <td style="text-align: right"></td>
            <td style="text-align: left"></td>
        </tr>
        <tr>
            <td class="profilehead" colspan="4" style="text-align: left">Reference Details</td>
        </tr>
        <tr>
            <td style="height: 21px; text-align: right"></td>
            <td style="height: 21px; text-align: left"></td>
            <td style="height: 21px; text-align: right"></td>
            <td style="height: 21px; text-align: left"></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False" Width="117px">
                    <asp:ListItem>--</asp:ListItem>
                    <asp:ListItem>werwer</asp:ListItem>
                    <asp:ListItem>
                    </asp:ListItem>
                </asp:DropDownList></td>
            <td style="text-align: right">
                <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By" Width="96px"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlApprovedBy" runat="server" Enabled="False">
                    <asp:ListItem>--</asp:ListItem>
                    <asp:ListItem>abc</asp:ListItem>
                    <asp:ListItem>
                    </asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="text-align: right"></td>
            <td style="text-align: left"></td>
            <td style="text-align: right"></td>
            <td style="text-align: left"></td>
        </tr>
        <tr>
            <td colspan="4">
                <table id="tblButtons" align="center">
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" ValidationGroup="main" /></td>
                        <td>
                            <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" OnClick="btnRefresh_Click"
                                Text="Refresh" /></td>
                        <td>
                            <asp:Button ID="btnClose" runat="server" CausesValidation="False" OnClick="btnClose_Click"
                                Text="Close" /></td>
                        <td>
                            <asp:Button ID="btnPrint" runat="server" CausesValidation="False" OnClick="btnPrint_Click"
                                Text="Print" /></td>
                        <td style="width: 3px">
                            <asp:Button ID="btnApprove" runat="server" CausesValidation="False" OnClick="btnApprove_Click"
                                Text="Approve" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 8px"></td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False"></asp:ValidationSummary>
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="main"></asp:ValidationSummary>
    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="item"></asp:ValidationSummary>
    <asp:SqlDataSource ID="sdsIndentDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
        SelectCommand="SP_SCM_INDENT_SEARCH_SELECT" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="EMPID" ControlID="lblEmpIdHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
        Visible="False"></asp:Label><asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label><asp:Label
            ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
      </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

