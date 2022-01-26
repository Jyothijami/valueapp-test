<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="OLDStock_QRCode.aspx.cs" Inherits="Modules_Warehouse_OLDStock_QRCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">

    <div style="width: 100%">
                <table style="width: 100%" class="pagehead">
                    <tr>
                        <td style="text-align: left;">Stock Report :
                        </td>
                        <%--<td>
                    <asp:HyperLink ID="HyperLink6" runat="server" CssClass="leftmenu" Target="_blank" NavigateUrl="~/dev_Pages/PhyStockVerify.aspx">Physical Stock Verification Page</asp:HyperLink>

                </td>--%>
                        
                    </tr>
                    <tr><td></td>
                        <td style="text-align: right">
                            <asp:DropDownList ID="ddlNoOfRecords" runat="server" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged">
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>75</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>

            <div id="divSearch" style="width: 100%">
                <table style="width: 100%" >
                    <tr>
                        <td style="text-align: right">Model No :</td>
                        <td>
                            <asp:TextBox ID="txtModelNo" runat="server"></asp:TextBox>
                            <asp:Button ID="btnGo0" runat="server" OnClick="btnGo_Click" Text="Search" Width="100px" />
                        </td>
                        <td>
                            &nbsp;</td>
                        <asp:SqlDataSource
                            ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                            SelectCommand="USP_MODEL_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:Parameter Type="Int64" DefaultValue="0" Name="BranbId"></asp:Parameter>
                                <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtModelNo"></asp:ControlParameter>
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <td style="text-align: right">MRN No :</td>
                        <td>
                            <asp:DropDownList ID="ddlMrn" runat ="server" ></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">Company Name :
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlCompany" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="compsds1" DataTextField="COMP_NAME" DataValueField="CP_ID">
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="compsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT a.CP_ID, a.CP_FULL_NAME + ',' + b.locname AS COMP_NAME FROM YANTRA_COMP_PROFILE AS a INNER JOIN location_tbl AS b ON a.locid = b.locid"></asp:SqlDataSource>
                        </td>
                        <td></td>
                        <td style="text-align: right">Location : 
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlLocation" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="locsds1" DataTextField="wh_name" DataValueField="wh_name">
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="locsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT DISTINCT [wh_name] from V_INWARDNew"></asp:SqlDataSource>

                        </td>
                    </tr>                    

                    <tr>
                        <td style="text-align: right;">Brand :
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlBrand" runat="server" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                        </td>
                        <td></td>
                        <td style="text-align: right">Model No : 
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlModelNo" runat="server" OnSelectedIndexChanged="ddlModelNo_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">Category :
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlCategory" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                        </td>
                        <td></td>
                        <td style="text-align: right">Sub Category : 
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlSubCat" runat="server"></asp:DropDownList>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">Color :
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlColor" runat="server"></asp:DropDownList>
                        </td>
                        <td></td>
                        <td style="text-align: right">From Date : 
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtFromDate" type="datepic" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right;">To Date :
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtToDate" type="datepic" runat="server"></asp:TextBox>
                        </td>
                        <td colspan="3"></td>
                    </tr>

                    <tr>
                        <td colspan="5" style="text-align: center">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                            <asp:Button ID="btnQR" runat ="server" Text ="QRCode"  OnClick="btnQR_Click" Visible ="false"  />
                            <asp:Button ID="btnPrint" runat ="server" Text ="Print A4" OnClick ="btnPrint_Click" Visible ="false" />
                            <asp:Button ID="btnPrintRoller" runat ="server" Text ="Print Roller" OnClick ="btnPrintRoller_Click" />
                            <%--<asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click" Visible="False" />--%>
                            <%--<asp:Button ID="btnExport2" runat="server" Text="Export To Excel" OnClick="btnExport2_Click" Visible="False" />--%>
                        </td>
                    </tr>


                </table>
            </div>

            <div id="divStockReport">
                <table style="width: 100%">
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Total No Of Closing Stock :
                            <asp:Label ID="lblCS" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;
                            Total No of Print Qty :
                            <asp:Label ID="lblPrintQty" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;
                            Total No of Items Count :
                            <asp:Label ID="lblItems" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;
                        </td>
          
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvStockReport" runat="server" Width="100%" OnRowDataBound="gvStockReport_RowDataBound" GridLines="None" AllowPaging="true" PageSize="25" OnPageIndexChanging="gvStockReport_PageIndexChanging" Visible="False" AutoGenerateColumns ="false"  >
                                <Columns >
                                    <asp:BoundField DataField ="Item Code" HeaderText ="Item Code" />
                                    <asp:BoundField DataField ="Model No" HeaderText ="Model No" />
                                    <asp:BoundField DataField ="Brand" HeaderText ="Brand" />
                                    <asp:BoundField DataField ="ITEM_SPEC" HeaderText ="Description" />
                                    <asp:BoundField DataField ="Color" HeaderText ="Colour" />
                                    <asp:BoundField DataField ="ClosingStock" HeaderText ="ClosingStock" />
                                    <asp:BoundField DataField ="CHK_NO" HeaderText ="MRN No" />
                                    <asp:BoundField DataField ="MRNDt" HeaderText ="MRN Dt" />
                                    <asp:BoundField DataField ="CHK_INVOICE_NO" HeaderText ="Inv No" />
                                    <asp:BoundField DataField ="InvDt" HeaderText ="Inv Dt" />
                                    <asp:BoundField DataField ="Warehouse Location" HeaderText ="Warehouse Location" />
                                    <asp:BoundField DataField ="MRN_NO" HeaderText ="Det Id" />
                                    <asp:BoundField DataField ="COLOUR_ID" HeaderText ="color id" />
                                    <asp:BoundField DataField ="Item_ID" HeaderText ="Item ID" />
                                    <asp:TemplateField HeaderText ="Qty" >
                                        <ItemTemplate >
                                            <asp:TextBox ID ="txtqtyw" runat ="server" Text ="1" ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText ="No.of Copies" >
                                        <ItemTemplate >
                                            <asp:TextBox ID ="txtqty" runat ="server" Text='<%# Bind("ClosingStock") %>'  ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Image">
                                        <ItemTemplate>
                                            <asp:Image ID="Image" runat="server" EnableTheming="False" Height="100%" ImageUrl='<%# Eval("Image","~/Content/QRCodes/{0}") %>'
                                                Width="100%" /><br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField ="InwardDt" HeaderText ="InwardDt" />
                                    <asp:BoundField DataField ="ISPrint" HeaderText ="IS Print" />
                                    <asp:BoundField DataField ="PrintQty" HeaderText ="Print Qty" />
                                    <asp:BoundField DataField ="Id" HeaderText ="Id" />
                                   <%-- <asp:TemplateField HeaderText ="Id" >
                                        <ItemTemplate >
                                            <asp:Label ID ="txtqrid" runat ="server" Text='<%# Bind("Id") %>'  ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>





                                    <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:CheckBox ID="cbSelectAll" runat="server" Text="All" OnClick="selectAll(this)" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox_row" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:GridView ID="gvModelNoSearch" runat="server" Width="100%" OnRowDataBound="gvModelNoSearch_RowDataBound" GridLines="None" AllowPaging="true" PageSize="25" OnPageIndexChanging="gvModelNoSearch_PageIndexChanging" Visible="False" AutoGenerateColumns ="false"  >
                                <Columns >
                                    <asp:BoundField DataField ="Item Code" HeaderText ="Item Code" />
                                    <asp:BoundField DataField ="Model No" HeaderText ="Model No" />
                                    <asp:BoundField DataField ="Brand" HeaderText ="Brand" />
                                    <asp:BoundField DataField ="ITEM_SPEC" HeaderText ="Description" />
                                    <asp:BoundField DataField ="Color" HeaderText ="Colour" />
                                    <asp:BoundField DataField ="ClosingStock" HeaderText ="ClosingStock" />
                                    <asp:BoundField DataField ="CHK_NO" HeaderText ="MRN No" />
                                    <asp:BoundField DataField ="MRNDt" HeaderText ="MRN Dt" />
                                    <asp:BoundField DataField ="CHK_INVOICE_NO" HeaderText ="Inv No" />
                                    <asp:BoundField DataField ="InvDt" HeaderText ="Inv Dt" />
                                    <asp:BoundField DataField ="Warehouse Location" HeaderText ="Warehouse Location" />
                                    <asp:BoundField DataField ="MRN_NO" HeaderText ="Det Id" />
                                    <asp:BoundField DataField ="COLOUR_ID" HeaderText ="color id" />
                                    <asp:BoundField DataField ="Item_ID" HeaderText ="Item ID" />
                                    <asp:TemplateField HeaderText ="Qty" >
                                        <ItemTemplate >
                                            <asp:TextBox ID ="txtqtyw" runat ="server" Text ="1" ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText ="No.of Copies" >
                                        <ItemTemplate >
                                            <asp:TextBox ID ="txtqty" runat ="server" Text='<%# Bind("ClosingStock") %>'  ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Image">
                                        <ItemTemplate>
                                            <asp:Image ID="Image" runat="server" EnableTheming="False" Height="150px" ImageUrl='<%# Eval("Image","~/Content/QRCodes/{0}") %>'
                                                Width="150px" /><br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField ="InwardDt" HeaderText ="InwardDt" />
                                    <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:CheckBox ID="cbSelectAll" runat="server" Text="All" OnClick="selectAll(this)" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox_row" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        <asp:Label ID="lblCode" runat ="server" Visible ="false" ></asp:Label>
                           
                        </td>
                    </tr>
                </table>
            </div>
</asp:Content>

