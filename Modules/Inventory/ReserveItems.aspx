<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/InventoryMP1.master" AutoEventWireup="true" CodeFile="ReserveItems.aspx.cs" Inherits="Modules_Inventory_ReserveItems" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
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
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
                <tr>
                    <td style="text-align: left">Internal
                Order Approval</td>
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
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="searchhead" colspan="4" style="text-align: left">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="text-align: left" colspan="2">Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
                                </td>

                                <td style="text-align: right">
                                    <table border="0" cellpadding="0" cellspacing="0" align="right">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label12" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                                    Text="Search By"></asp:Label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                                    OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">--</asp:ListItem>
                                                    <%--<asp:ListItem Value="WO_NO">IO No</asp:ListItem>--%>
                                                    <asp:ListItem Value="WO_DATE">IO Date</asp:ListItem>
                                                    <asp:ListItem Value="CUST_NAME">Customer Name</asp:ListItem>
                                                    <asp:ListItem Value="CUST_CONTACT_PERSON">Contact Person</asp:ListItem>
                                                    <asp:ListItem Value="CUST_EMAIL">EMail Address</asp:ListItem>
                                                    <asp:ListItem Value="WO_DELIVERY_DATE">Delivery Date</asp:ListItem>
                                                    <%--<asp:ListItem Value="WO_FLAG">Status</asp:ListItem>--%>
                                                </asp:DropDownList></td>
                                            <td>
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
                                            <td>
                                                <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                                </asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtSearchValueFromDate" runat="server" type="date" EnableTheming="True" Visible="False"
                                                    Width="106px">
                                                </asp:TextBox><%--<asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False" />
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchFrom" runat="server" Enabled="False" PopupButtonID="imgFromDate"
                                            TargetControlID="txtSearchValueFromDate">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>--%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                                </asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtSearchValueToDate" runat="server" type="date" EnableTheming="True" Visible="False"
                                                    Width="106px">
                                                </asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                                </asp:TextBox><%--<asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False" />
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchValueToDate" runat="server" Enabled="False" PopupButtonID="imgToDate"
                                            TargetControlID="txtSearchText">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>--%>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                                    CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center">
                        <asp:GridView ID="gvWorkOrderDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            DataSourceID="sdsWorkOrder" OnRowDataBound="gvWorkOrderDetails_RowDataBound" SelectedRowStyle-BackColor="#c0c0c0" AllowSorting="True" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="WO_ID" SortExpression="WO_ID" HeaderText="WOIdHidden">
                                    <ItemStyle Width="200px"></ItemStyle>

                                    <HeaderStyle Width="200px"></HeaderStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="IO No">
                                    <ControlStyle Width="150px"></ControlStyle>

                                    <ItemStyle Width="150px" Wrap="True" HorizontalAlign="Center"></ItemStyle>

                                    <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnWorkOrderNo" OnClick="lbtnWorkOrderNo_Click" ForeColor="#0066ff" runat="server" Text='<%# Eval("WO_NO") %>' CausesValidation="False" __designer:wfdid="w63"></asp:LinkButton>
                                    </ItemTemplate>

                                    <FooterStyle Width="150px"></FooterStyle>
                                </asp:TemplateField>
                                <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="WO_DATE" SortExpression="WO_DATE" HeaderText="IODate">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CUST_NAME" SortExpression="CUST_NAME" HeaderText="Customer">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

                                    <FooterStyle HorizontalAlign="Left"></FooterStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CUST_CONTACT_PERSON" SortExpression="CUST_CONTACT_PERSON" HeaderText="ContactPerson">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CUST_EMAIL" SortExpression="CUST_EMAIL" HeaderText="E-MailAddress"></asp:BoundField>
                                <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="WO_DELIVERY_DATE" SortExpression="WO_DELIVERY_DATE" HeaderText="DeliveryDate">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PREPAREDBY" SortExpression="PREPAREDBY" HeaderText="PreparedBy"></asp:BoundField>
                                <asp:BoundField DataField="APPROVEDBY" SortExpression="APPROVEDBY" HeaderText="ApprovedBy"></asp:BoundField>
                                <asp:BoundField DataField="WO_FLAG" SortExpression="WO_FLAG" HeaderText="Status"></asp:BoundField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:LinkButton ID="lbtnWorkOrderNo" runat="server" Text='<%# Eval("WO_NO") %>'></asp:LinkButton>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <asp:SqlDataSource ID="sdsWorkOrder" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                            SelectCommand="SP_SM_WORKORDER_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
                                <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
                                <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
                                <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
                                <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>
                                <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="EmpId" ControlID="lblEmpIdHidden"></asp:ControlParameter>
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                            Visible="False"></asp:Label>
                        <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                            Visible="False"></asp:Label>
                        <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblRespId" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblIOId" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblCust_Id" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>

                    </td>
                </tr>
            </table>
            <table id="tblWorkOrderDetails" runat="server"
                width="100%" visible="false">
                <tr>
                    <td colspan="4" style="text-align: left" class="profilehead">General Details</td>
                </tr>
                <tr>
                    <td colspan="4">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <asp:Label ID="lblSalesOrderNo" runat="server" Text="Purchase Order No"></asp:Label></td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddlOrderAcceptance" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOrderAcceptance_SelectedIndexChanged" Enabled="False">
                        </asp:DropDownList><asp:Label ID="Label36" runat="server" EnableTheming="False" ForeColor="Red" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlOrderAcceptance"
                            ErrorMessage="Please Select The Order Acceptance No." InitialValue="0" ValidationGroup="a">*</asp:RequiredFieldValidator>

                    </td>
                    <td style="text-align: right;">
                        <asp:Label ID="lblSalesOrderDate" runat="server" Text="Purchase Order Date"></asp:Label></td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtOADate" runat="server" ReadOnly="True" CssClass="datetext" EnableTheming="False"></asp:TextBox>&nbsp;<asp:Image ID="imgOADate"
                            runat="server" ImageUrl="~/Images/Calendar.png" Visible="False"></asp:Image><cc1:CalendarExtender
                                Format="dd/MM/yyyy" ID="ceOADate" runat="server" PopupButtonID="imgOADate" TargetControlID="txtOADate" Enabled="False">
                            </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="meeOADate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                            MaskType="Date" TargetControlID="txtOADate" UserDateFormat="MonthDayYear">
                        </cc1:MaskedEditExtender>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblCustomer" runat="server" Text="Customer Name"></asp:Label></td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtCustName" runat="server" ReadOnly="True"></asp:TextBox></td>
                    <td style="text-align: right">
                        <asp:Label ID="Label45" runat="server" Text="Unit Name"></asp:Label></td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtUnitName" runat="server" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr>
                    <td rowspan="2" style="text-align: right">
                        <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>

                    </td>
                    <td rowspan="2" style="text-align: left;">
                        <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" ReadOnly="True"></asp:TextBox></td>
                    <td style="text-align: right">
                        <asp:Label ID="lblCity" runat="server" Text="Region"></asp:Label></td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtRegion" runat="server" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <asp:Label ID="Label25" runat="server" Text="Phone"></asp:Label></td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtPhone" runat="server" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True"></asp:TextBox>
                        <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: right">
                        <asp:Label ID="Label26" runat="server" Text="Mobile"></asp:Label></td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: left" class="profilehead">Purchase Order Items</td>
                </tr>
                <tr><td>&nbsp;</td></tr>
                <tr>
                    <td colspan="4" class="auto-style1">
                        <asp:GridView ID="gvOrderAcceptanceItems" runat="server" AutoGenerateColumns="False"
                            OnRowDataBound="gvOrderAcceptanceItems_RowDataBound" Width="100%">
                            <Columns>
                                <%-- <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkhdr" runat="server" AutoPostBack="True" OnCheckedChanged="chkhdr_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chk" runat="server" __designer:wfdid="w5"></asp:CheckBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>--%>
                                <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                                <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                <%--<asp:TemplateField HeaderText="Quantity" ControlStyle-Width="80px">
                            <ItemTemplate>
                                <asp:TextBox ID="txtQuantity" Text='<%# Bind("Quantity") %>' runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                <asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
                                <asp:BoundField HeaderText="Amount"></asp:BoundField>
                                <asp:BoundField DataField="Specifications" HeaderText="Specifications"></asp:BoundField>
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
                                <asp:BoundField DataField="Priority" HeaderText="Priority"></asp:BoundField>
                                <asp:BoundField DataField="DeliveryDate" HeaderText="Delivery Date"></asp:BoundField>
                                <asp:BoundField DataField="Room" HeaderText="Room"></asp:BoundField>
                                <asp:BoundField DataField="Price" HeaderText="Price"></asp:BoundField>
                                <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                                <asp:BoundField DataField="ColorId" HeaderText="ColorId"></asp:BoundField>
                                <asp:BoundField DataField="SO_RES_STATUS" HeaderText="ReserveStatus"></asp:BoundField>
                                <asp:BoundField DataField="SODetId" HeaderText="SoDetId"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center">Select Location :
                    <asp:DropDownList ID="ddlLocation" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="locsds1" DataTextField="locname" DataValueField="locid" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                    </asp:DropDownList>
                        <asp:SqlDataSource ID="locsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [locid], [locname] FROM [location_tbl]"></asp:SqlDataSource>

                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
            <asp:Label ID="lblItemCode" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblColor" runat="server" Visible="false"></asp:Label>

                        <asp:Label ID="lblCustQty" runat="server" Visible="false"></asp:Label>

                    </td>

                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gvAvailQty" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDataBound="gvAvailQty_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                                <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                                <%--<asp:BoundField DataField="Quantity" HeaderText="PO Quantity"></asp:BoundField>--%>
                                <asp:TemplateField HeaderText="PO Quantity" ControlStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQuantity" AutoCompleteType="None" Text='<%# Bind("Quantity") %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TtlQty" HeaderText="Total Qty"></asp:BoundField>
                                <asp:BoundField DataField="BlockQty" HeaderText="Blocked Qty"></asp:BoundField>
                                <asp:BoundField DataField="AvailQty" HeaderText="Avaliable Qty"></asp:BoundField>
                                <%--  <asp:TemplateField HeaderText="Avaliable Quantity" ControlStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQuantity" AutoCompleteType="None" Text='<%# Bind("AvailQty") %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:BoundField DataField="CustQty" HeaderText="Cust Blocked Qty"></asp:BoundField>
                                <asp:BoundField DataField="Location" HeaderText="Location"></asp:BoundField>
                                <asp:BoundField DataField="locId" HeaderText="locId"></asp:BoundField>
                                <asp:BoundField DataField="colorId" HeaderText="colorId"></asp:BoundField>
                                <asp:BoundField DataField="Qty" HeaderText="Avaliable Qty"></asp:BoundField>
                                <asp:BoundField DataField="DeliveryDate" HeaderText="Delivery Date"></asp:BoundField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkhdr" runat="server" OnClick="selectAll(this)" />
                                    </HeaderTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="Chk" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DetId" HeaderText="DetID"></asp:BoundField>
                            </Columns>
                        </asp:GridView>

                    </td>
                </tr>
                <tr hidden="hidden">
                    <td style="text-align: center">Total Quantity:<asp:Label ID="lblTtlQty" runat="server"></asp:Label></td>
                    <td style="text-align: center">Blocked Quantity:<asp:Label ID="lblBlockQty" runat="server"></asp:Label></td>
                    <td colspan="2" style="text-align: center">Avaliable Quantity:<asp:Label ID="lblAvaliableQty" runat="server"></asp:Label>
                        <asp:Label ID="lblLocation" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lbllocID" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                  <tr>
                    <td colspan="2" style="text-align: right">
                        <asp:Button ID="btnReserve2" OnClick="btnReserve2_Click" runat="server" Text="Reserve 2" />
                        <asp:Button ID="btnReserve" runat="server" Text="Reserve" Visible="false" OnClick="btnReserve_Click" />
                    </td>
                    <td colspan="2" style="text-align: left">
                        <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" />
                    </td>
                </tr>
                </table>
               
            
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<asp:UpdatePanel runat="server">
        <ContentTemplate>
    <table>
         <tr>
                    <td colspan="2" style="text-align: right">
                        <asp:Button ID="btnReserve" runat="server" Text="Reserve" OnClick="btnReserve_Click" />
                    </td>
                    <td colspan="2" style="text-align: left">
                        <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" />
                    </td>
                </tr>
    </table>
            </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>


 
