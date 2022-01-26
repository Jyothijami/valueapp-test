<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/PurchaseMP1.master" AutoEventWireup="true" CodeFile="IndentRecords.aspx.cs" Inherits="Modules_SCM_IndentRecords" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>Indent Records
            </td>
            <td>
                <asp:DropDownList ID="ddlNoOfRecords" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged" DataValueField="5">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>75</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" id="tblIndentApprovalDetails" runat="server"
        visible="true" width="100%">


        <tr align="right">
            <td colspan="4" align="right">

                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr class="searchhead">
                        <td style="text-align: left" colspan="2">Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                            <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
                        </td>

                        <td style="text-align: right">
                            <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True" Text="Search By"></asp:Label>


                            <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="ITEM_NAME">Model No</asp:ListItem>
                                <asp:ListItem Value="IND_DET_REQ_FOR">Customer Name</asp:ListItem>
                                <asp:ListItem Value="IND_DET_BRAND">Brand</asp:ListItem>
                                <asp:ListItem Value="IND_DATE">Indent Date</asp:ListItem>
                                <asp:ListItem Value="IND_DET_REQ_BY_DATE">Required By Date</asp:ListItem>
                                <asp:ListItem Value="COLOUR_NAME">Color</asp:ListItem>
                                <asp:ListItem Value="CP_SHORT_NAME">Company</asp:ListItem>
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
                            <asp:TextBox ID="txtSearchValueFromDate" runat="server" type="datepic" EnableTheming="True" Visible="False" Width="106px">
                            </asp:TextBox>
                            <%--  <cc1:CalendarExtender ID="ceSearchFrom" runat="server" Enabled="False" Format="dd/MM/yyyy" PopupButtonID="imgFromDate" TargetControlID="txtSearchValueFromDate">
                            </cc1:CalendarExtender>
                            <asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False" />--%>
                            <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                            </asp:Label>
                            <asp:TextBox ID="txtSearchValueToDate" runat="server" type="datepic" EnableTheming="True" Visible="False" Width="106px">
                            </asp:TextBox>
                            <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                            </asp:TextBox>
                            <%-- <cc1:CalendarExtender ID="ceSearchValueToDate" runat="server" Enabled="False" Format="dd/MM/yyyy" PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                            </cc1:CalendarExtender>
                            <asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False" />--%>
                            <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False" CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" />
                        </td>
                    </tr>
                </table>

            </td>
        </tr>

        <tr>
            <td colspan="4">&nbsp;
                            <asp:GridView ID="gvIndentDetails" runat="server" AutoGenerateColumns="False"
                                DataSourceID="SqlDataSource1" Width="100%" OnRowDataBound="gvIndentDetails_RowDataBound"
                                AllowPaging="True" AllowSorting="True" PageSize="10">
                                <Columns>

                                    <asp:TemplateField>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="cbSelectAll" runat="server" Text="All" OnClick="selectAll(this)" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="Chk" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" SortExpression="ITEM_CODE" />
                                    <asp:BoundField DataField="IND_ID" HeaderText="Ind Id" />
                                    <asp:BoundField DataField="IND_DET_ID" HeaderText="Ind DetId" />
                                    <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="Item Model No" />
                                    <asp:TemplateField HeaderText="Item Specification" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblItem_Spec" runat="server" Width="100px" Text='<%# Bind("ITEM_SPEC") %>' CausesValidation="False"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:BoundField DataField="IT_TYPE" HeaderText="Sub Category" />
                                    <asp:BoundField DataField="IND_DET_BRAND" HeaderText="Brand" SortExpression="IND_DET_BRAND" />
                                    <asp:BoundField DataField="COLOUR_NAME" HeaderText="Color" />
                                    <asp:BoundField DataField="IND_DET_SUGG_PARTY" HeaderText="Suggested Party" SortExpression="IND_DET_SUGG_PARTY" />
                                    <asp:BoundField DataField="UOM_SHORT_DESC" HeaderText="UOM" />
                                    <asp:BoundField DataField="IND_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Indent Date" SortExpression="IND_DATE" />
                                    <asp:BoundField DataField="IND_DET_REQ_BY_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Indent Req Date" HtmlEncode="False" SortExpression="IND_DET_REQ_BY_DATE" />
                                    <asp:BoundField DataField="IND_DET_QTY" HeaderText="Indent Qty" SortExpression="IND_DET_QTY" />
                                    <asp:BoundField DataField="IND_DET_REM_QTY" HeaderText="Enquired Qty" SortExpression="IND_DET_REM_QTY" />
                                    <asp:BoundField DataField="IND_DET_ORD_QTY" HeaderText="Ordered Qty" SortExpression="IND_DET_ORD_QTY" />
                                </Columns>
                            </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_SCM_INDENTED_ITEMS_RECORDS_SEARCH_SELECT" SelectCommandType="StoredProcedure">
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
            <td>
                <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1" Visible="False"></asp:Label>
                <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1" Visible="False"></asp:Label>
                <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>

            </td>
        </tr>

    </table>
    <table style="width: 100%">
        <tr>
            <td style="text-align: right">
                <asp:Button ID="btnSupplierQuation" runat="server" Text="Supplier Enquiry" OnClick="btnSupplierQuation_Click" />
            </td>
            <td>
                <asp:Button ID="btnPurchaseOrder" runat="server" Text="Purchase Order" OnClick="btnPurchaseOrder_Click" />
                <asp:Button ID="btnExprot" runat="server" Text="Export To Excel" OnClick="btnExprot_Click" BackColor="#9966FF" EnableTheming="True" ForeColor="White" Width="28%" />

            </td>
        </tr>
    </table>

</asp:Content>



