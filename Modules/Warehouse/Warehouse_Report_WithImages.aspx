<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/InventoryMP1.master" AutoEventWireup="true" CodeFile="Warehouse_Report_WithImages.aspx.cs" Inherits="Modules_Warehouse_Warehouse_Report_WithImages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <%--                        <asp:TemplateField HeaderText="Image">
                            <ItemTemplate>
                                <asp:Image ID="Image1" runat="server"  EnableTheming="False" Height="132px" ImageUrl='<%# Eval("Item_Path") %>'
                                    Width="141px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        --%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <%--<asp:UpdatePanel runat="server">
        <ContentTemplate>--%>
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

            <asp:Panel runat="server" EnableTheming="False">
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: right">Company Name :</td>
                        <td>
                            <asp:DropDownList ID="ddlCompany" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="compsds1" DataTextField="COMP_NAME" DataValueField="CP_ID">
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="compsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT a.CP_ID, a.CP_FULL_NAME + ',' + b.locname AS COMP_NAME FROM YANTRA_COMP_PROFILE AS a INNER JOIN location_tbl AS b ON a.locid = b.locid"></asp:SqlDataSource>
                        </td>
                        <td style="width: 5%">&nbsp;
                        </td>
                        <td style="text-align: right">Model No :</td>
                        <td>
                            <asp:TextBox ID="txtModelNo" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Brand :</td>
                        <td>
                            <asp:DropDownList ID="ddlBrand_WH" runat="server" OnSelectedIndexChanged="ddlBrand_WH_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td></td>
                        <td style="text-align: right">Model No : 
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlModelNo" runat="server"></asp:DropDownList>

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
                        <td style="text-align: right">Color :</td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlcolor" runat="server" >
                            </asp:DropDownList>

                        </td>
                       <td>&nbsp;</td>
                        <td style="text-align: right;">Discontinued Items</td>
                            <td style="text-align: left;"><asp:TextBox ID="txtDisci" runat ="server" ></asp:TextBox>
                                                &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Warehouse Location : </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlLocation" runat="server" AppendDataBoundItems="True" AutoPostBack="false" DataSourceID="locsds1" DataTextField="wh_name" DataValueField="wh_name">
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="locsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT DISTINCT [wh_name] from V_INWARDNew"></asp:SqlDataSource>
                        </td>
                        <td></td>
                        <td style="text-align: right;">Item Quantity Check : </td>
                        <td style="height: 25px">
                            <asp:DropDownList ID="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox" EnableTheming="False" Width="50px">
                                <asp:ListItem Selected="True">--</asp:ListItem>
                                <asp:ListItem>=</asp:ListItem>
                                <asp:ListItem>&lt;</asp:ListItem>
                                <asp:ListItem>&gt;</asp:ListItem>
                                <asp:ListItem>&lt;=</asp:ListItem>
                                <asp:ListItem>&gt;=</asp:ListItem>
                                <%--<asp:ListItem>R</asp:ListItem>--%>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtQtyCheck" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Price Compare : 
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlPrice" AutoPostBack ="true"  runat="server" >
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                <asp:ListItem Value="1">-- Low to High --</asp:ListItem>
                                <asp:ListItem Value="2">-- Hight to Low --</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                   <tr>
                       <td style="text-align: right" ></td>
                       <td style="text-align : left">
                           <asp:LinkButton ID="lnk1" Visible ="false"  runat ="server" Text="Under ₹1000" OnClick ="lnk1_Click"></asp:LinkButton><br />
                            <asp:LinkButton ID="LinkButton1" Visible ="false"  runat ="server" Text="₹1000 - ₹5000" OnClick ="LinkButton1_Click"></asp:LinkButton><br />
                            <asp:LinkButton ID="LinkButton2" Visible ="false" runat ="server" Text="₹5000 - ₹10000" OnClick ="LinkButton2_Click"></asp:LinkButton><br />
                            <asp:LinkButton ID="LinkButton3" Visible ="false" runat ="server" Text="₹10000 - ₹20000" OnClick ="LinkButton3_Click"></asp:LinkButton><br />
                           <asp:LinkButton ID="LinkButton4" Visible ="false" runat ="server" Text="Over ₹20000" OnClick ="LinkButton4_Click"></asp:LinkButton><br />
                           <asp:TextBox ID="txtMin" Visible ="false" Width ="50px" PlaceHolder="₹Min" Font-Bold ="true"   runat ="server" EnableTheming ="false" ></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                           <asp:TextBox ID="txtMax" Visible ="false" runat ="server" Width ="50px" Font-Bold ="true"  PlaceHolder="₹Max" EnableTheming ="false" ></asp:TextBox>
                       </td>
                   </tr>
                    <tr style="width: 100%">
                        <td colspan="5" style="text-align: center">
                            <asp:Button ID="btnSearch_WH" runat="server" Text="Search" OnClick="btnSearch_WH_Click" Width="150px" />
                            <asp:Button ID="btnPrintAll" runat="server" OnClick="btnPrintAll_Click1" Text="Print" Visible="False" Width="150px" />
                            <asp:Button ID="btnExportExcel" runat="server" OnClick="btnExportExcel_Click" Text="Export to Excel" Visible="False" />
                            <asp:Button ID="btnExportAll" runat ="server" OnClick ="btnExportAll_Click" Text ="Export All to Excel" Visible ="false" />
                            <asp:Label ID="lbl1" runat ="server" Visible ="false" ></asp:Label>
                            <asp:Label ID="lbl2" runat ="server" Visible ="false" ></asp:Label>
                            <asp:Label ID="lbl3" runat ="server" Visible ="false" ></asp:Label>
                            <asp:Label ID="lbl4" runat ="server" Visible ="false" ></asp:Label>
                            <asp:Label ID="lbl5" runat ="server" Visible ="false" ></asp:Label>

                        </td>
                    </tr>
                </table>
                <br />
                <asp:GridView ID="gvWarehouseReportImages" runat="server" AlternatingRowStyle-BackColor = "#C2D69B" 
                    AllowPaging="True" AutoGenerateColumns="False" ShowFooter="true" Style="text-align: left"
                    Font-Names = "Arial"  Width="100%" OnPageIndexChanging="gvWarehouseReportImages_PageIndexChanging" OnRowDataBound="gvWarehouseReportImages_RowDataBound" EnableTheming="True">
                    <PagerStyle CssClass="pagination" HorizontalAlign="Left" VerticalAlign="Middle" />
                    <FooterStyle ForeColor="#0066ff" />
                    <Columns>
                        <asp:TemplateField>
            <HeaderTemplate>
                <asp:CheckBox ID="chkAll" runat="server" onclick = "checkAll(this);" />
            </HeaderTemplate> 
            <ItemTemplate>
                <asp:CheckBox   ID="CheckBox1" runat="server" onclick = "Check_Click(this)" />
            </ItemTemplate>
        </asp:TemplateField> 
                        <asp:BoundField DataField="Item Code" HeaderText="Item code" />
                        <asp:BoundField DataField="Brand" HeaderText="Brand" />
                        <asp:BoundField DataField="Model No" HeaderText="Model No" />
                        <asp:BoundField DataField="Series Name" HeaderText="Item Series" />
                        <asp:BoundField DataField="IT_TYPE" HeaderText="Sub Category" />
                        <asp:BoundField DataField="Color" HeaderText="Color" />
                        <asp:BoundField DataField="Wh Name" HeaderText="Warehouse Name" />
                        <asp:BoundField DataField="Total Available Stock" HeaderText="Total Available Stock" />
                        <asp:BoundField DataField="Item_Price" HeaderText="Price" />
                         <asp:TemplateField HeaderText="Image"   >
                            <ItemTemplate>
                                <asp:Label ID="lblPath" Visible="false" Text='<%#Eval("Item_Image") %>' runat="server" />
                                <asp:Image ID="Image1" runat="server" EnableTheming="False"  ImageUrl = '<%# Eval("Item_Path") %>'
                                    Width="100px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Image" >
                                                <ItemTemplate>
                                                    <asp:Image runat ="server" EnableTheming="False"  ImageUrl = '<%# Eval("Item_Image","http://183.82.108.55/Content/Images/{0}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                       <%-- <asp:TemplateField HeaderText="Image">
                            <ItemTemplate>
                                <asp:Image ID="Image" runat="server" EnableTheming="False" Height="132px" ImageUrl='<%# Eval("Item_Image","~/Content/ItemImage/{0}") %>'
                                    Width="141px" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>

<%--                        <asp:TemplateField HeaderText="Image">
                            <ItemTemplate>
                                <asp:Image ID="Image1" runat="server"  EnableTheming="False" Height="132px" ImageUrl='<%# Eval("Item_Path") %>'
                                    Width="141px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        --%>
                        <asp:BoundField DataField ="F2" HeaderText="Discontinued" />
                    </Columns>
                    <AlternatingRowStyle BackColor="#C2D69B"  />
                    <EmptyDataTemplate>
                        <span style="color: #FF0000">No Data to Display</span>
                    </EmptyDataTemplate>
                </asp:GridView>
            </asp:Panel>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>


 
