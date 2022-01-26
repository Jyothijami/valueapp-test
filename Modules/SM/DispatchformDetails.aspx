<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/InventoryMP1.master" AutoEventWireup="true" CodeFile="DispatchformDetails.aspx.cs" Inherits="Modules_SM_DispatchformDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <script type="text/javascript">
        function amtcalcDisc() {

            var req_qty, rate, spprice, splamt;
            req_qty = document.getElementById('<%=txtItemQuantity.ClientID %>').value;
      rate = document.getElementById('<%=txtRate.ClientID %>').value;
        spprice = document.getElementById('<%=txtSpPrice.ClientID %>').value;

        if (req_qty == "" || req_qty == "0") {
            document.getElementById('<%=txtSpPrice.ClientID %>').value = "0";
       }
       else if (rate == "" || rate == "0") {
           document.getElementById('<%=txtSpPrice.ClientID %>').value = "0";
           }
           else if (rate > 0 && req_qty > 0) {
               document.getElementById('<%=txtDiscount1.ClientID %>').value = (((rate * req_qty) - spprice) * 100) / (rate * req_qty);
            }
    splamt = document.getElementById('<%=txtSpPrice.ClientID %>').value;
        document.getElementById('<%=txtUnitprice.ClientID %>').value = splamt / req_qty;

    }
      </Script>
    <script >
        function amtcalcDisc1() {

            var req_qty, rate, spprice, splamt, unitprice, amt;
            req_qty = document.getElementById('<%=txtItemQuantity.ClientID %>').value;
       rate = document.getElementById('<%=txtRate.ClientID %>').value;
        spprice = document.getElementById('<%=txtSpPrice.ClientID %>').value;
        unitprice = document.getElementById('<%=txtUnitprice.ClientID %>').value;
        if (req_qty == "" || req_qty == "0") {
            document.getElementById('<%=txtSpPrice.ClientID %>').value = "0";
           }
           else if (rate == "" || rate == "0") {
               document.getElementById('<%=txtSpPrice.ClientID %>').value = "0";
            }
            else if (rate > 0 && req_qty > 0) {
                document.getElementById('<%=txtDiscount1.ClientID %>').value = (((rate) - unitprice) * 100) / (rate);

                  disc = document.getElementById('<%=txtDiscount1.ClientID %>').value;
                  amt = unitprice * req_qty;

                <%--document.getElementById('<%=txtSpPrice.ClientID %>').value = amt - (disc * amt) / 100;--%>
                  document.getElementById('<%=txtSpPrice.ClientID %>').value = amt;

              }

  }
        </script>
    <script>
        function amtcalc() {
            var req_qty, rate, amt, disc, splamt;
            req_qty = document.getElementById('<%=txtItemQuantity.ClientID %>').value;
    rate = document.getElementById('<%=txtRate.ClientID %>').value;


        if (req_qty == "" || req_qty == "0") {
            document.getElementById('<%=txtAmount.ClientID %>').value = "0";
    }
    else if (rate == "" || rate == "0") {
        document.getElementById('<%=txtAmount.ClientID %>').value = "0";
                }
                else if (rate > 0 && req_qty > 0) {
                    document.getElementById('<%=txtAmount.ClientID %>').value = (rate * req_qty);
                }
        amt = document.getElementById('<%=txtAmount.ClientID %>').value;
        disc = document.getElementById('<%=txtDiscount1.ClientID %>').value;

        if (amt == "" || amt == "0") {
            document.getElementById('<%=txtSpPrice.ClientID %>').value = "0";
        document.getElementById('<%=txtUnitprice.ClientID %>').value = "0";
    }
    else if (disc == "" || disc == "0") {
        document.getElementById('<%=txtSpPrice.ClientID %>').value = amt;
                }
                else if (disc > 0 && amt > 0) {
                    document.getElementById('<%=txtSpPrice.ClientID %>').value = amt - (disc * amt) / 100;
                }
        splamt = document.getElementById('<%=txtSpPrice.ClientID %>').value;
        document.getElementById('<%=txtUnitprice.ClientID %>').value = splamt / req_qty;
    }
        </script>
    <asp:UpdatePanel ID="ConvenienceVoucherPanel" runat="server">
        <ContentTemplate>

            <table border="0" cellpadding="0" cellspacing="0" class="pagehead" style="width: 100%">
                <tr>
                    <td style="text-align: left">Dispatch Form Details</td>
                </tr>
            </table>
            <table id="tblsub2" runat="server" width="100%">

                <tr>
                    <td colspan="5">
                        <table id="tblsub" runat="server" width="100%">
                            <tr>
                                <td colspan="4" class="profilehead" style="text-align: left">Add Dispatch Instrutions</td>
                            </tr>
                            <tr>
                                <td style="text-align: right" align="right">
                                    <asp:Label ID="lblSearch" runat="server" Text="Search Customer" Width="110px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtSearchModel" runat="server">
                                    </asp:TextBox>
                                    <asp:Button ID="btnSearchModelNo" runat="server" BorderStyle="None" CausesValidation="False"
                                        CssClass="gobutton" EnableTheming="False" OnClick="btnSearchModelNo_Click" Text="Go" />
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                        SelectCommand="SP_Customer_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtSearchModel"></asp:ControlParameter>
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                                <td style="text-align: left"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td style="text-align: right" align="right">
                                    <asp:Label ID="lblCustomer" runat="server" Text="Customer Name" Width="104px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlCustomerName" runat="server" Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomerName_SelectedIndexChanged">
                                    </asp:DropDownList></td>
                                <td style="text-align: right" align="right">
                                    <asp:Label ID="lblRegion" runat="server" Text="Region"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtRegion" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right" align="right">
                                    <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtAddress" runat="server" ReadOnly="True" TextMode="MultiLine">
                                    </asp:TextBox></td>
                                <td style="text-align: right" align="right">
                                    <asp:Label ID="lblPhone" runat="server" Text="Phone"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtPhone" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right" align="right">
                                    <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                                <td style="text-align: right" align="right">
                                    <asp:Label ID="lblMobile" runat="server" Text="Mobile"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right" align="right">
                                    <asp:Label ID="Label4" runat="server" Text="Unit Name"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlUnitName" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged">
                                    </asp:DropDownList></td>
                                <td style="text-align: right " align="right"><asp:Label ID="lblUnitAdd" runat ="server" Text="Unit Address" ></asp:Label></td>
                                <td> <asp:TextBox ID="txtUnitAdd" runat ="server" TextMode ="MultiLine"  ></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblSalesOrderNo" runat="server" Text="Purchase Order No" Width="130px">
                                    </asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlSalesOrderNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSalesOrderNo_SelectedIndexChanged">
                                    </asp:DropDownList></td>
                                <td style="text-align: right"></td>
                                <td style="text-align: left">&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" class="profilehead" style="text-align: left">Purchase Order Details</td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <asp:GridView ID="gvItemDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvItemDetails_RowDataBound"
                                        ShowFooter="True" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                            <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                                            <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                            <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>

                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                            <asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
                                            <asp:BoundField HeaderText="UnitPrice"></asp:BoundField>
                                            <asp:BoundField HeaderText="Amount"></asp:BoundField>
                                            <asp:BoundField DataField="Price" HeaderText="SpPrice"></asp:BoundField>
                                            <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                                            <asp:BoundField DataField="ItemSpec" HeaderText="Item Spec"></asp:BoundField>
                                            <asp:BoundField DataField="HSN_CODE" HeaderText="HSN Code"/>
                                            <asp:BoundField DataField="GST_TAX" HeaderText="GST Rae"/>
                                            <%--<asp:BoundField DataField ="ColorId" HeaderText="Color ID" />--%>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkhdr" runat="server" AutoPostBack="True" OnCheckedChanged="chkhdr_CheckedChanged" />
                                                </HeaderTemplate>
                                                <%-- <EditItemTemplate>
                                            <asp:CheckBox runat="server" ID="CheckBox1"></asp:CheckBox>
                                        </EditItemTemplate>--%>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkItemSelect" runat="server" OnCheckedChanged="chkItemSelect_CheckedChanged"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Balance Quantity" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Bind("BalanceQty") %>'>></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="locid" HeaderText="locid"/>
                                            <asp:BoundField DataField="ColorId" HeaderText="ColorId"/>

                                            <asp:TemplateField>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:GridView ID="gvStock" CssClass="subgridviews" Width="100%" runat="server" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:BoundField DataField ="Cust_BLOCK_Stock" HeaderText ="Cust Blocked Stock" />
                                                            <asp:BoundField DataField="TOTAL_AVALIABLE_STOCK" HeaderText="Free Stock" />
                                                            <asp:BoundField DataField="locname" HeaderText="Location" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <EmptyDataTemplate>
                                            <span style="color: #ff0000">No Data Exits</span>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                    <asp:Button ID="btnGo" runat="server" CausesValidation="False" OnClick="btnGo_Click"
                                        Text="Go" />
                                </td>
                            </tr>
                     <tr><td colspan="4" style="text-align: left" class="profilehead">Items Details</td></tr>
                                        <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label14" runat="server" Text="Search By Brand :"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
                                    </asp:DropDownList></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label38" runat="server" Text="Search By ModelNo :" Width="135px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtModelNo" runat="server">
                                    </asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server"
                                        ControlToValidate="txtModelNo" ErrorMessage="Please Enter ModelNo For Search"
                                        ValidationGroup="Search">*</asp:RequiredFieldValidator><asp:Button ID="btnSearchModelNo1"
                                            runat="server" BorderStyle="None" CausesValidation="False" CssClass="gobutton"
                                            EnableTheming="False" OnClick="btnSearchModelNo1_Click1" Text="Go" ValidationGroup="Search" /></td>
                            </tr>
                                        <tr>
                                <td style="text-align: right; height: 22px;">
                                    <asp:Label ID="Label17" runat="server" Text="Model No :"></asp:Label></td>
                                <td style="text-align: left; height: 22px;">
                                    <asp:DropDownList ID="ddlModelNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModelNo_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:Label ID="Label34" runat="server" EnableTheming="False" ForeColor="Red"
                                        Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="ddlModelNo" ErrorMessage="Please Select the Model No" InitialValue="0"
                                            ValidationGroup="ip">*</asp:RequiredFieldValidator>
                                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                        SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:ControlParameter PropertyName="SelectedValue" Type="Int64" DefaultValue="0" Name="BranbId" ControlID="ddlBrand"></asp:ControlParameter>
                                            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtModelNo"></asp:ControlParameter>
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblItemNmae" runat="server" Text="Item Name :" Width="76px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtItemName" runat="server">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblDesc" runat="server" Text="Description :"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtDescription" runat="server" EnableTheming="False" Height="47px"
                                        TextMode="MultiLine" Width="384px">
                                    </asp:TextBox></td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblBrand" runat="server" Text="Brand :"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtBrand" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                               
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblCategory" runat="server" Text="ItemCategory :"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtItemCategory" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblSubCat" runat="server" Text="Item SubCategory :"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtItemSubCategory" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                            <td style="text-align: right">
                                    <asp:Label ID="lblColr" runat="server" Text="Color :"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlColor" runat="server" AutoPostBack="True" >
                                    </asp:DropDownList></td>
                                            
                                 
                                            <td style="text-align: right; height: 22px;">
                                    <asp:Label ID="Label12" runat="server" Text="HSN Code"></asp:Label>
                                </td>
                                <td style="text-align: left; height: 22px;">
                                    <asp:TextBox ID="txtHSN" runat ="server" ></asp:TextBox>
                                </td>
                               
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblUOM" runat="server" Text="UOM :"></asp:Label>&nbsp;</td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtItemUOM" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                                 <td style="text-align: right">
                                    <asp:Label ID="lblGST" runat="server" Text="GST Rate :" Width="76px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtGST" runat="server">
                                    </asp:TextBox></td>

                               
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblRate" runat="server" Text="Rate : "></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtRate" runat="server"></asp:TextBox>
                                    <asp:Label ID="Label19" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtRate"
                                        ErrorMessage="Please Enter the Rate" ValidationGroup="id">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                            ID="ftxteRate" runat="server" TargetControlID="txtRate" ValidChars=".0123456789" Enabled="False">
                                        </cc1:FilteredTextBoxExtender>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblQty" runat="server" Text="Quantity : "></asp:Label></td>
                                <td style="text-align: left; width: 439px;">
                                    <asp:TextBox ID="txtItemQuantity" runat="server" Width="139px"></asp:TextBox>
                                    <asp:Label ID="Label18" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtItemQuantity"
                                        ErrorMessage="Please Enter the Quantity" ValidationGroup="id">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                            ID="ftxteQuantity" runat="server" FilterType="Numbers" TargetControlID="txtItemQuantity"
                                            ValidChars="." Enabled="False">
                                        </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>   
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label13" runat="server" Text="Discount : "></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtDiscount1" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtDiscount1" ErrorMessage="Please Enter the Discount" ValidationGroup="id">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblAmount" runat="server" Text="Amount : "></asp:Label></td>
                                <td style="text-align: left; width: 439px;">
                                    <asp:TextBox ID="txtAmount" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                                
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label15" runat="server" Text="UnitPrice"></asp:Label></td>
                                <td style="width: 439px; text-align: left">
                                    <asp:TextBox ID="txtUnitprice" runat="server">
                                    </asp:TextBox></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label30" runat="server" Text="Special Price : "></asp:Label></td>
                                <td style="text-align: left; width: 439px;">
                                    <asp:TextBox ID="txtSpPrice" runat="server"></asp:TextBox></td>
                            </tr>
                                        
                                        <tr>
                                <td colspan="4">
                                    <table style="width: 100%">
                                        <tr>
                                            <td colspan="2" style="text-align: right">
                                                <asp:Button ID="btnAd" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    CssClass="loginbutton" EnableTheming="False" OnClick="btnAd_Click" Text="Add"
                                                    ValidationGroup="ip" /></td>
                                            <td colspan="2">
                                                <asp:Button ID="btnItemRefresh" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    CssClass="loginbutton" EnableTheming="False" OnClick="btnItemRefresh_Click" Text="Refresh"
                                                    CausesValidation="False" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center; height: 21px;"></td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <asp:GridView ID="gvDispatch" Width="100%" runat="server" AutoGenerateColumns="False" OnRowEditing="gvDispatch_RowEditing" OnRowDeleting="gvDispatch_RowDeleting" OnRowDataBound="gvDispatch_RowDataBound">
                                        <Columns>
                                            <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                            <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField ="ModelNo" HeaderText="Model No" ></asp:BoundField>
                                            <asp:BoundField DataField ="ItemName" HeaderText="Item Name" />
                                            <asp:BoundField DataField ="UOM" HeaderText ="UOM" />
                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                             <asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
                                            <asp:BoundField HeaderText="UnitPrice"></asp:BoundField>
                                            <asp:BoundField DataField="Price" HeaderText="SpPrice"></asp:BoundField>
                                             <asp:TemplateField HeaderText="Amount" ControlStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                            <asp:BoundField DataField ="Color" HeaderText ="Colour" />
                                            <asp:BoundField HeaderText ="GST Value" />
                                            <%--<asp:BoundField DataField ="HSN_CODE" HeaderText="HSN Code" />--%>
                                            <asp:BoundField DataField="GST_TAX" HeaderText ="GST Rate"/>
                                            <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtQuantity" runat="server" AutoPostBack ="true" OnTextChanged ="txtQuantity_TextChanged" Text='<%# Bind("Quantity") %>'>></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <EmptyDataTemplate>
                                            <span style="color: #ff0066">No data Found</span>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label1" runat="server" Text="Delivery Date" Width="102px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtDeliveryDate" type="datepic" runat="server"></asp:TextBox>
                                    <asp:Label ID="Label16" runat="server" EnableTheming="False" ForeColor="Red"
                                        Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="txtDeliveryDate" ErrorMessage="Please Select Delivery Date" InitialValue="0"
                                            ValidationGroup="ip">*</asp:RequiredFieldValidator>
                                    <%-- <cc1:MaskedEditExtender ID="Maskededitextender1" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDeliveryDate"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label2" runat="server" Text="Delivery Time" Width="103px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtDeliveryTime" runat="server">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label3" runat="server" Text="Company"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlCompany" runat="server" >
                                    </asp:DropDownList></td>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label5" runat="server" Text="Remarks"></asp:Label></td>
                                <td colspan="3" style="text-align: left">
                                    <asp:TextBox ID="txtRemarks" runat="server" EnableTheming="False" TextMode="MultiLine"
                                        Width="561px">
                                    </asp:TextBox></td>
                            </tr>
                            <%--<tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label6" runat="server" Text="Old Dues"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtOldDues" runat="server">
                                    </asp:TextBox></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label11" runat="server" Text="Payment Be Collected" Width="139px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtPaymentsCollected" runat="server">
                                    </asp:TextBox></td>
                            </tr>--%>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label6" runat="server" Text="Payments be Collected"></asp:Label></td>
                                <td style="text-align: left"><asp:TextBox ID="txtPaymentsCollected" runat="server">
                                    </asp:TextBox><asp:Label ID="lblTotalAmt" runat ="server" visible="false"  ></asp:Label>
                                    <asp:Label ID="lblStatus" runat ="server" visible="false"  ></asp:Label>
                                    <asp:HiddenField ID="txtGrossTotalAmtHidden" runat="server" />
                                    </td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label11" runat="server" Text="GST Amount" Width="139px"></asp:Label></td>
                                <td style="text-align: left"><asp:TextBox ID="txtOldDues" runat="server">
                                    </asp:TextBox><asp:Label ID="lblGSTAmt" runat ="server" Visible ="false"  ></asp:Label>
                                    </td>
                            </tr>
                            <tr>
                                <td style="text-align: right" align="right">
                                    <asp:Label ID="Label7" runat="server" Text="Transport Charges" Width="127px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtTransportCharges" runat="server">
                                    </asp:TextBox></td>
                                <td style="text-align: right" align="right">
                                    <asp:Label ID="Label9" runat="server" Text="Total Amount" Width="105px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtPackingCharges" runat="server">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right" align="right">
                                    <asp:Label ID="Label8" runat="server" Text="Executive"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlExecutive" runat="server" >
                                    </asp:DropDownList></td>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right" align="right">
                                    <asp:Label ID="Label10" runat="server" Text="Prepared By"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                                    </asp:DropDownList></td>
                                <td style="text-align: right" align="right">
                                    <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlApprovedby" runat="server" Enabled="False">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                            <td>
                                                <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" /></td>
                                            <td>
                                                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" /></td>
                                            <td colspan="2">
                                                <asp:Button ID="btnExit" runat="server" Text="Exit" OnClick="btnExit_Click" /></td>
                                            <td colspan="1">
                                                <asp:Button ID="btnDesPrint" runat="server" OnClick="btnDesPrint_Click" Text="Print" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px"></td>
                    <td style="width: 100px"></td>
                    <td style="width: 100px"></td>
                    <td style="width: 100px"></td>
                    <td style="width: 100px"></td>
                </tr>
            </table>

            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                SelectCommand="SP_Dispatch_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



