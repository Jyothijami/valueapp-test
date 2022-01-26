<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/InventoryMP1.master" AutoEventWireup="true" CodeFile="ItemsReport.aspx.cs" Inherits="Modules_Warehouse_ItemsReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function PrintDivContent(printdiv) {
            var printContent = document.getElementById(printdiv);
            var prtContent = document.getElementById('<%= gvItemMaster.ClientID %>');

            var WinPrint = window.open('', '', 'left=0,top=0,toolbar=0,sta­tus=0');
            WinPrint.document.write(printContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
        }
    </script>
    <script>
        function doPrint() {
            var prtContent = document.getElementById('<%= gvItemMaster.ClientID %>');
            prtContent.border = 0; //set no border here

            var WinPrint = window.open('', '', 'left=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,status=0,resizable=1,paging=false');
            WinPrint.document.write(prtContent.outerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            //WinPrint.close();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <h2>Item Master With Images</h2>
    <div style="width: 100%">
                <table style="width: 100%" class="pagehead">
                    <tr>
                        <td style="text-align: left;">Available Stock Report With Images :
                        </td>

                        <td style="text-align: right">No Of Records :
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
    <div id="divSearch" style="width: 100%" runat="server" visible="true">
        <table style="width: 100%">


            <tr>
                <td style="text-align: right;">Brand :
                </td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="ddlBrand2" runat="server" OnSelectedIndexChanged="ddlBrand2_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                </td>
                <td></td>

                <td style="text-align: right;">Category :
                </td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="ddlCategory" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                </td>

            </tr>
            <tr>

                <td style="text-align: right">Sub Category : 
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlSubCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubCat_SelectedIndexChanged"></asp:DropDownList>

                </td>
                <td></td>


                <td style="text-align: right">Model No : 
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlModelNo" runat="server" AutoPostBack="True"></asp:DropDownList>

                </td>
            </tr>


            <tr>
                <td colspan="5" style="text-align: center">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />&nbsp;
                    <asp:Button ID="btnPrint" runat="server" Text="Print Current Page" OnClientClick="doPrint()" Visible="False" Width="150px" />

                </td>
            </tr>
            <tr >
                <td colspan="2" style="text-align: left">
                    Model No : &nbsp; <asp:TextBox ID="txtModelNo" runat="server"></asp:TextBox> &nbsp;&nbsp;
                    <asp:Button ID="btnModelSearch" OnClick="btnModelSearch_Click" runat="server" Text="Search Model No" />
                </td>
                <td colspan="2" style="text-align :left">Essential Model No : &nbsp; 
                    <asp:TextBox ID="txtEssModelNo" runat="server" ></asp:TextBox>
                    <asp:Button ID="btnEssModelSearch" runat="server" OnClick ="btnEssModelSearch_Click" Text ="Search Essentail Model No" CausesValidation="False" ValidationGroup="Search" />
                </td>
                <td style="text-align :left">Item Series:
                    <asp:TextBox ID="txtseries" runat ="server" ></asp:TextBox>
                    <asp:Button ID="btnSeries" runat ="server" OnClick ="btnSeries_Click" Text="search Series" CausesValidation="False" ValidationGroup="Search" />
                </td>
            </tr>
            <tr>
                <td colspan="5"><asp:DropDownList ID="ddlModelNo1" runat="server" AutoPostBack="True" Visible ="false" OnSelectedIndexChanged="ddlModelNo1_SelectedIndexChanged">
                                    </asp:DropDownList><asp:Label ID="lblItemCode" runat ="server" Visible ="false"></asp:Label>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                        SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:ControlParameter PropertyName="SelectedValue" Type="Int64" DefaultValue="0" Name="BranbId" ControlID="ddlBrand"></asp:ControlParameter>
                                            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtEssModelNo"></asp:ControlParameter>
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                </td>
            </tr>

        </table>
        <table style="width: 100%; text-align: center" align="center">
            <tr>
                <td>
                    <%--<asp:Button ID="btnPrint" runat="server" Text="Print All Pages" OnClientClick="javascript:PrintDivContent('printdiv');" Visible="False" Width="150px" />--%>
                    <%--<asp:Button ID="btnPrint" runat="server" Text="Print All Pages" OnClientClick="doprint()" Visible="False" Width="150px" />--%>
                    <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click"  Width="150px" />
                </td>
            </tr>
        </table>
    </div>

    <table style="width: 100%">
        <tr>
            <td style="text-align: left">Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <%--Brand--%>
                <asp:DropDownList ID="ddlBrand" Visible="false" runat="server" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                <asp:Button ID="btnAll" runat="server" Text="All" OnClick="btnAll_Click" />
                <asp:Button ID="btnPrintAll" runat="server" OnClick="btnPrintAll_Click1" Text="Print" Visible="False" Width="150px" />
            </td>
            <td></td>

        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td colspan="4">
                <div id="printdiv">
                    <asp:GridView ID="gvItemMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="ITEM_CODE" OnRowCommand="gvItemMaster_RowCommand" ShowFooter="true" Style="text-align: left" Width="100%" OnPageIndexChanging="gvItemMaster_PageIndexChanging" OnRowDataBound="gvItemMaster_RowDataBound">
                        <PagerStyle CssClass="pagination" HorizontalAlign="Left" VerticalAlign="Middle" />
                        <FooterStyle ForeColor="#0066ff" />
                        <Columns>
                            <asp:BoundField DataField ="HSN_Code" HeaderText="HSN Code" />
                            <asp:BoundField DataField="GST Tax" HeaderText ="GST Rate" />
                            <asp:BoundField DataField="ITEM_CODE" HeaderText="Item code" />
                            <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="Model No" />
                            <asp:BoundField DataField="ITEM_NAME" HeaderText="Item Name" />
                            <asp:BoundField DataField="ITEM_SERIES" Visible="false" HeaderText="Item Series" />
                            <asp:BoundField DataField="ITEM_CATEGORY_NAME" HeaderText="Category" />
                            <asp:BoundField DataField="IT_TYPE" HeaderText="Sub Category" />
                            <asp:BoundField DataField="PRODUCT_COMPANY_NAME" HeaderText="Brand" />
                            <asp:BoundField DataField="Item_Price" HeaderText="Price" />
                            <asp:BoundField DataField="COLOUR_NAME" HeaderText="COLOUR NAME" />

                            <%--<asp:BoundField DataField="Image" HeaderText="Image" />--%>
                            <%-- <asp:TemplateField HeaderText="History">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="Item_Path" Width="18px" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Image">
                                <ItemTemplate>
                                    <asp:Image ID="Image" runat="server" EnableTheming="False" Height="132px" ImageUrl='<%# Eval("Item_Image","~/Content/ItemImage/{0}") %>'
                                        Width="141px" /><br />
                                    <asp:FileUpload ID="fileupload1" runat ="server" Width ="100px"/>
                                    <asp:ImageButton ID="ibtmImage" runat="server" ImageUrl="~/Images/tick.png" CommandName ="Save" Width="18px" CommandArgument='<%# Eval("ITEM_CODE").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tech Image">
                                <ItemTemplate>
                                    <asp:Image ID="Image_Spec" runat="server" EnableTheming="False" Height="132px" ImageUrl='<%# Eval("Item_Specification_Image","~/Content/ItemDrawings/{0}") %>'
                                        Width="141px" /><br />
                                    <asp:FileUpload ID="fileupload2" runat ="server" Width ="100px"/>
                                    <asp:ImageButton ID="ibtmImage1" runat="server" ImageUrl="~/Images/tick.png" CommandName ="TechSave" Width="18px" CommandArgument='<%# Eval("ITEM_CODE").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Spare Image">
                                <ItemTemplate>
                                    <asp:Image ID="Image_Spare" runat="server" EnableTheming="False" Height="132px" ImageUrl='<%# Eval("Item_attachments","~/Content/ItemAttachments/{0}") %>'
                                        Width="141px" /><br />
                                    <asp:FileUpload ID="fileupload3" runat ="server" Width ="100px"/>
                                    <asp:ImageButton ID="ibtmImage3" runat="server" ImageUrl="~/Images/tick.png" CommandName ="TechSave" Width="18px" CommandArgument='<%# Eval("ITEM_CODE").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:BoundField DataField ="F2" HeaderText ="Discontinued" />
                        </Columns>
                        <EmptyDataTemplate>
                            <span style="color: #FF0000">No Data to Display</span>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>



