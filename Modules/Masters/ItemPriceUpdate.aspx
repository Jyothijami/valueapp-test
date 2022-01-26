<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/AdminMP1.master" AutoEventWireup="true" CodeFile="ItemPriceUpdate.aspx.cs" Inherits="Modules_Masters_ItemPriceUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

.textbox
{
	background-color: White;
	border: solid 1px #27736F;
	font-family: Verdana;
	font-size: 8pt;
	font-weight: normal;
	color: #000040;
}

.gobutton
{
	height: 25px;
	width: 25px;
	background-image: url(../../Images/GoButtonHover.png);
	background-repeat: no-repeat;
	background-color: Transparent;
	text-decoration: none;
	margin-right: 5px;
	margin-left: 5px;
	font-weight: bold;
	font-family: Verdana;
	font-size: 8pt;
	color: White;
}

        .auto-style1 {
            color: #FF0000;
        }

    </style>
    <script type="text/javascript">
        function Calculation() {
            var grid = document.getElementById("<%= gvItemPriceUpdate.ClientID%>");
        for (var i = 0; i < grid.rows.length - 1; i++) {
            var gross = $("input[id*=txtGross]")
            var coefficiant = $("input[id*=txtCoefficient]")
            var mul = $("input[id*=txtMulFactor]")
            var mrp = $("input[id*=txtMRP]")
            var rsp = $("input[id*=txtRSP]")
            if (gross[i].value != '' && coefficiant[i].value!='' && mul[i].value != '') {              

                mrp[i].value = (parseFloat(gross[i].value) + ((parseFloat(gross[i].value) * parseFloat(coefficiant[i].value)) / 100)) * parseFloat(mul[i].value);
                rsp[i].value = mrp[i].value;
                // mrp[i].value = parseFloat(txtAmountReceive[i].value)

                // mul[i].value = gross[i].value;
                // alert(txtAmountReceive[i].value);
                // if (coefficiant[i].value == '') { coefficiant[i].value = 0 }
                // if (mul[i].value == '') { mul[i].value = 0 }



            }
        }
    }
</script>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <div>
        <table class="pagehead">
            <tr>
                <td style="text-align: left">Item Sales Price
                </td>
            </tr>
        </table>
    </div>
    <br />
    <table>
<tr>
<td colspan="5" class="auto-style2"></td>
</tr>
<tr>
    <td>
        &nbsp;
    </td>
<%--<td colspan="5">Search For ModelNo:<asp:TextBox ID="txtSearchModel" runat="server"> </asp:TextBox>
<asp:Button ID="btnSearchModelNo" runat="server" BorderStyle="None" CausesValidation="False" CssClass="gobutton" EnableTheming="False" OnClick="btnSearchModelNo_Click1" Text="Go" />
<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
<SelectParameters>
<asp:Parameter Type="Int64" DefaultValue="0" Name="BranbId"></asp:Parameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtSearchModel"></asp:ControlParameter>
</SelectParameters>
</asp:SqlDataSource>
</td>--%>
</tr>
<tr>
<td colspan="5">
    Search For ModelNo:<asp:TextBox ID="txtSearchModel" runat="server"> </asp:TextBox>
<asp:Button ID="btnSearchModelNo" runat="server" BorderStyle="None" CausesValidation="False" CssClass="gobutton" EnableTheming="False" OnClick="btnSearchModelNo_Click1" Text="Go" />
<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
<SelectParameters>
<asp:Parameter Type="Int64" DefaultValue="0" Name="BranbId"></asp:Parameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtSearchModel"></asp:ControlParameter>
</SelectParameters>
</asp:SqlDataSource>
</td>
</tr>
<tr>
<td style="text-align: right">
<asp:Label ID="Label1" runat="server" Text="Brand :"></asp:Label>
</td>
<td style="text-align: left">
<asp:DropDownList ID="ddlBrand1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand1_SelectedIndexChanged">
</asp:DropDownList>
</td>
<td style="width: 5%"></td>
<td style="text-align: right">
<asp:Label ID="Label2" runat="server" Text="Category :"></asp:Label>
</td>
<td style="text-align: left">
<asp:DropDownList ID="ddlCategory1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory1_SelectedIndexChanged">
</asp:DropDownList>
</td>
</tr>
<tr>
<td style="text-align: right">
<asp:Label ID="Label3" runat="server" Text="Sub Category :"></asp:Label>
</td>
<td style="text-align: left">
<asp:DropDownList ID="ddlSubCategory1" runat="server" OnSelectedIndexChanged="ddlSubCategory1_SelectedIndexChanged" AutoPostBack="True">
</asp:DropDownList>
</td>
<td style="width: 5%"></td>

<td style="text-align: right">
<asp:Label ID="Label4" runat="server" Text="Model No :"></asp:Label>
</td>
<td style="text-align: left">
<asp:DropDownList ID="ddlModelNo1" runat="server">
</asp:DropDownList>
</td>
</tr>
        <tr>
            <td colspan="5">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="5" style="text-align:center">
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
            </td>
        </tr>
</table>
    <br />
    <table class="stacktable">
        <%--<tr>
            <td>&nbsp;</td>
            <td style="text-align: right">
                        <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="Label14" runat="server" EnableTheming="False" Font-Bold="True"
                                        Text="Search By :"></asp:Label>
                                    <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="textbox" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--</asp:ListItem>
                                        <asp:ListItem Value="ITEM_NAME">Item Name</asp:ListItem>
                                        <asp:ListItem Value="ITEM_MODEL_NO">Model No</asp:ListItem>
                                        
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtSearchText" runat="server" CssClass="textbox" Width="111px"></asp:TextBox>
                                    <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                        CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnSearchGo_Click" /></td>
        </tr>--%>
        <tr>
            <td colspan="2">
                <asp:GridView ID="gvItemPriceUpdate" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvItemPriceUpdate_RowDataBound" Width="100%" AllowPaging="True" OnPageIndexChanging="gvItemPriceUpdate_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
                        <asp:BoundField DataField="IC_ID" HeaderText="IC_ID" SortExpression="IC_ID" />
                        <asp:BoundField DataField="BRAND_ID" HeaderText="BRAND ID" SortExpression="BRAND_ID" />
                        <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="Model No" />
                        <asp:BoundField DataField="ITEM_NAME" HeaderText="Item Name" />
                         <asp:TemplateField HeaderText="Currency type" ControlStyle-Width="100px">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlCurrency" runat="server"></asp:DropDownList>
                                <asp:HiddenField ID="cthf1" runat="server" Value='<%# Eval("CurrencyType") %>' />
                            </ItemTemplate>
                          

<ControlStyle Width="100px"></ControlStyle>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Gross" ControlStyle-Width="80px">
                            <ItemTemplate>
                                <asp:TextBox ID="txtGross" runat="server" Text='<%# Bind("GrossAmount") %>' onblur="Calculation(this.value)" ></asp:TextBox>
                            </ItemTemplate>

<ControlStyle Width="80px"></ControlStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="%Coefficient" ControlStyle-Width="80px">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCoefficient" runat="server" Text='<%# Bind("Coefficient") %>' onblur="Calculation(this.value)" ></asp:TextBox>
                            </ItemTemplate>

<ControlStyle Width="80px"></ControlStyle>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="* Factor" ControlStyle-Width="80px">
                            <ItemTemplate>
                                <asp:TextBox ID="txtMulFactor" runat="server" Text='<%# Bind("MulFactor") %>' onblur="Calculation(this.value)" ></asp:TextBox>
                            </ItemTemplate>

<ControlStyle Width="80px"></ControlStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MRP" ControlStyle-Width="80px">
                            <ItemTemplate>
                                <asp:TextBox ID="txtMRP" runat="server" Text='<%# Bind("MRP") %>' onblur="Calculation(this.value)"  ></asp:TextBox>
                            </ItemTemplate>

<ControlStyle Width="80px"></ControlStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RSP" ControlStyle-Width="80px">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRSP" runat="server" Text='<%# Bind("Item_Price") %>' onblur="Calculation(this.value)"  ></asp:TextBox>
                            </ItemTemplate>

<ControlStyle Width="80px"></ControlStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="IT_TYPE_ID" HeaderText="SubCat" />
                        <%--<asp:BoundField DataField="PRODUCT_COMPANY_NAME" HeaderText="Brand" />--%>
                    </Columns>
                    <EmptyDataTemplate>
                        <span class="auto-style1"><strong>No Items found to be Updated</strong></span>
                    </EmptyDataTemplate>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                     ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_MASTER_ITEMPRICE_MODIFY_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue" PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table class="stacktable">
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <table class="stacktable" hidden="hidden">
                                <tr>
                                    <td colspan="2" style="text-align: left" class="profilehead">Price Update By Brand</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label15" runat="server" Text="Brand :"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label34" runat="server" Text="Category :"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                            <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                            </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label35" runat="server" Text="Sub Category :"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlSubCategory" runat="server" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label36" runat="server" Text="Model No :"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlModelNo" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                             <asp:Label id="Label31" runat="server" Text="Percentage :"></asp:Label></td>
                                    <td style="text-align: left">
                             <asp:TextBox ID="txtPercentage" runat="server"></asp:TextBox>%</td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center">
                                        <%--<asp:Button ID="btnBrandUpdate" runat="server" OnClick="btnBrandUpdate_Click" Text="Update" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
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
                <table width="100%">
                    <tr>
                        <td colspan="2" class="text-left">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            &nbsp;</td>
                        <td style="text-align: left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            &nbsp;</td>
                        <td style="text-align: left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                        &nbsp;</td>
        </tr>
    </table>
    <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
                        
</asp:Content>


 
