<%@ Page Title="|| Value Line App : Warehouse||" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="Warehouse_Admin_Reports2.aspx.cs" Inherits="Modules_Warehouse_Warehouse_Admin_Reports" %>

<%@ Register Src="~/Modules/widgets/stockReport.ascx" TagPrefix="uc1" TagName="stockReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="text-align: center; font-weight: bold;">&nbsp;
                            <asp:LinkButton ID="lnkMRN" runat="server" OnClick="lnkMRN_Click" Font-Underline="True">Stock Report Search</asp:LinkButton>
                &nbsp;||&nbsp;
                            <asp:LinkButton ID="lnkDC" runat="server" OnClick="lnkDC_Click" Font-Underline="True">Customer Blocked Search</asp:LinkButton>
                &nbsp;||&nbsp;
                <asp:LinkButton ID="lnkOtherStock" runat="server" OnClick="lnkOtherStock_Click" Font-Underline="True">Branch Blocked Search</asp:LinkButton>
                 &nbsp;||&nbsp;
                <asp:LinkButton ID="lnkReleaseBlockedStk" runat="server" OnClick="lnkReleaseBlockedStk_Click" Font-Underline="True">Release Blocked Stock</asp:LinkButton>
                
            </td>
        </tr>
    </table>
    <asp:Panel runat="server" ID="pnlMRN" Visible="false">
    <div style="width: 100%" id="top" runat="server">
        <table style="width: 100%">
            <tr>
                <td class="profilehead">Stock Report
                </td>
            </tr>

        </table>
    </div>

    <div id="data" runat="server" style="width: 100%">
        <table style="width: 100%">
            <tr>
                <td style="text-align: right">Model No :
                    <asp:TextBox ID="txtModelNo" runat="server"></asp:TextBox>
                </td>
                <td style="width: 5%"></td>
                <td style="text-align: right">Brand :
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlBrand" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: right">Category :
                    <asp:DropDownList ID="ddlCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCat_SelectedIndexChanged"></asp:DropDownList>

                </td>
                <td style="width: 5%"></td>
                <td style="text-align: right">Sub Category :
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlSubCat" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right"><%--Executive :--%>
                    <asp:DropDownList ID="ddlExecutive" runat="server" AutoPostBack="True" Visible ="false"></asp:DropDownList>

                </td>
                <td style="width: 5%"></td>
                <%--<td style="text-align: right">Sub Category :
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="DropDownList2" runat="server"></asp:DropDownList>
                </td>--%>
            </tr>
            <tr>
                <td colspan="4">&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: center">
                    <asp:Button ID="btnSearch" runat="server" Width="10%" Text="Search" BackColor="#CBC9AB" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnExprot" runat="server" Text="Export To Excel" OnClick="btnExprot_Click" BackColor="#CBC9AB" EnableTheming="True" Width="10%" />
                    <asp:Label ID="lblLocId" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div style="width: 100%" id="grid" runat="server">
        <table style="width: 100%">
            <tr>
                <td style="text-align: center">
                    <asp:GridView ID="gvWarehouseReport" runat="server" EmptyDataText="No Records To Display" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvWarehouseReport_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="locname" HeaderText="Location" SortExpression="locname">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="InwardStock" HeaderText="Inward Stock" SortExpression="InwardStock">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Blocked Stock">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnBlockStock" OnClick="lbtnBlockStock_Click" ForeColor="#0066ff" runat="server" Text='<%# Eval("BlockedStock") %>' CausesValidation="False" __designer:wfdid="w2"></asp:LinkButton>&nbsp; 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="OutWardStock" HeaderText="Outward Stock" SortExpression="OutWardStock">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TOTAL_AVALIABLE_STOCK" HeaderText="Available Stock" SortExpression="TOTAL_AVALIABLE_STOCK">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="locid" HeaderText="LocId" SortExpression="locid">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>

    </div>
    <br />
    <table style="width: 100%" id="tblBlockedItems" runat="server" visible="false">
        <tr>
            <td class="profilehead">Blocked Items
            </td>
        </tr>
        <tr>
            <td>                <br />
                <div id="gridBlocked" runat="server" style="width: 100%">
                    <asp:GridView ID="gvBlockedItems" runat="server" EmptyDataText="No Records To Display" AllowPaging ="true" PageSize ="10" Width="100%" AutoGenerateColumns="False" OnPageIndexChanging ="gvBlockedItems_PageIndexChanging" OnRowDataBound ="gvBlockedItems_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="Model No" SortExpression="ITEM_MODEL_NO">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PRODUCT_COMPANY_NAME" HeaderText="BRAND" SortExpression="PRODUCT_COMPANY_NAME">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ITEM_NAME" HeaderText="Item Series" SortExpression="ITEM_NAME">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ITEM_SPEC" HeaderText="Description" SortExpression="ITEM_SPEC">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="COLOUR_NAME" HeaderText="Color" SortExpression="COLOUR_NAME">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CUST_NAME" HeaderText="Customer Name" SortExpression="CUST_NAME">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Delivery_Date" HeaderText="Delivery Date" SortExpression="Delivery_Date">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Executive" SortExpression="EMP_FIRST_NAME">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
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
                        </Columns>

                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    </asp:Panel> 
    <asp:Panel runat="server" ID="pnlDC" Visible="false">
    <table style="width: 100%" id="tblCust" runat="server" visible="true">
        <tr>
            <td colspan="4" class="profilehead"> Blocked Items for Customer
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
        <tr >
            <%--<td style="text-align: right">Customer Name :</td>--%>
            <td>
                <asp:TextBox ID="txtCustomer" runat="server" Visible ="false" ></asp:TextBox>
                <asp:Button ID="btnCustSearch" runat="server" Text="Search" OnClick="btnCustSearch_Click" Visible ="false"  />
            </td>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td style ="text-align :right">Search by only Blocked Customer</td>
            <td colspan ="3">
                <asp:DropDownList ID="ddlCust" runat ="server" OnSelectedIndexChanged ="ddlCust_SelectedIndexChanged" AutoPostBack ="true"  ></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4">                <br />
                <div id="Div1" runat="server" style="width: 100%">
                    <asp:GridView ID="gvCustItems" runat="server" EmptyDataText="No Records To Display" Width="100%" AutoGenerateColumns="true">                      

                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
        </asp:Panel> 

    <asp:Panel runat="server" ID="pnlOtherStock" Visible="false">
    <table style="width:100%" id="tblCustPo" runat="server" visible ="true">
        <tr><td colspan="4" class="profilehead"> Company wise Blocked Items
            </td></tr>
        <tr><td>&nbsp;</td></tr>
        <tr>
            <td style="text-align: right">Company :</td>
            <td style="text-align: left" colspan ="3"><asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="true" AppendDataBoundItems="True" DataSourceID="compsds1" DataTextField="CP_SHORT_NAME" DataValueField="CP_ID" >
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource ID="compsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT CP_ID, CP_SHORT_NAME from YANTRA_COMP_PROFILE "></asp:SqlDataSource>&nbsp;Locatin :
                  <asp:DropDownList ID="ddlLoc" AutoPostBack="true" runat="server" AppendDataBoundItems="True" DataSourceID ="locsds1" DataTextField ="locname" DataValueField ="locid"><asp:ListItem Value="0">-- Select --</asp:ListItem></asp:DropDownList>
                  <asp:SqlDataSource ID="locsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [locid], [locname] FROM [location_tbl]"></asp:SqlDataSource>    
                  <asp:Button ID="btnCompSearch" runat="server" Text="Search" OnClick="btnCompSearch_Click" />
                   <asp:Button ID="btnBlockExport" runat="server" Text="Export To Excel" OnClick="btnBlockExport_Click" BackColor="#CBC9AB" EnableTheming="True"  Width="10%" />
            </td>
            
        </tr>
        <tr>
            <td colspan="4"><br />
                <div id="Div2" runat="server" style="width: 100%">
                    <asp:GridView ID="gvCpBlocked" runat="server" EmptyDataText="No Records To Display" Width="100%" AutoGenerateColumns="true">                      

                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
        </asp:Panel> 

    <asp:Panel ID="pnlReleaseStock" runat ="server" Visible ="false" >
        <table style="width:100%" id="Table1" runat="server" visible ="true">
        <tr><td colspan="4" class="profilehead"> Release Blocked Stock Details 
            </td></tr>

        <tr><td>&nbsp;</td></tr>
            <tr>
            <td >Search by only Blocked Customer : 
                <asp:DropDownList ID="ddlReleaseCust" runat ="server" OnSelectedIndexChanged ="ddlReleaseCust_SelectedIndexChanged" AutoPostBack ="true"  ></asp:DropDownList>

            </td>
           
        </tr>
            <tr>
                    <td>Remarks :
                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                <asp:Button ID="btnReleaseBlock" runat="server" Visible="false" Text="Release Items" OnClick="btnReleaseBlock_Click" />

                    </td>
                 
                                           
                </tr>

            <tr>
            <td colspan="4">                <br />
                <div id="Div3" runat="server" style="width: 100%">
                    <asp:GridView ID="gvReservedStock" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDataBound="gvReservedStock_RowDataBound">
                                                    <Columns>
                                                        <%--<asp:BoundField DataField="Item_ID" HeaderText="Item_ID" />--%>

                                                        <asp:BoundField DataField="Item_Code" HeaderText="Item Code" />
                                                        <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="Model No" />
                                                        <%--<asp:BoundField DataField="PO_Id" HeaderText="PO No" />--%>
                                                        <%--<asp:BoundField DataField="dt_added" HeaderText="Blocked Date" />--%>
                                                        <asp:BoundField DataField="COLOUR_NAME" HeaderText="Color" />
                                                        <asp:BoundField DataField="Quantity" HeaderText="Blocked Quantity" />

                                                        <%-- <asp:TemplateField HeaderText="Total Reserved Stock">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQty" Text='<%# Bind("Total_Block_Stock") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                                        <asp:BoundField DataField="CUST_NAME" HeaderText="Customer Name" />
                                                        <asp:BoundField DataField="Delivery_Date" HeaderText="Delivery Date" DataFormatString = "{0:dd/MM/yyyy}" />
                                                        <asp:BoundField DataField="whname" HeaderText="Location" />
                                                        <asp:TemplateField HeaderText="Quantity">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk" AutoPostBack="true" runat="server" OnCheckedChanged="chk_CheckedChanged"></asp:CheckBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="COLOUR_ID" HeaderText="COLOUR ID" />
                                                        <asp:BoundField DataField="Customer_Id" HeaderText="Customer Id" />
                                                         <asp:BoundField DataField="whLocId" HeaderText="whLocId" />


                                                    </Columns>
                                                </asp:GridView>

                   
                </div>
            </td>

        </tr>
             <tr>
                                <td colspan="4">
                                    <%-- <asp:UpdatePanel runat="server">
                                <ContentTemplate>--%>
                                    <table id="tblReleasedItems" runat="server" visible="false">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gvReleasedItems" runat="server" AutoGenerateColumns="False" Width="100%" >
                                                    <Columns>
                                                        <asp:BoundField DataField="Item_Code" HeaderText="Item Code" />
                                                        <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="Model No" />
                                                        <asp:BoundField DataField="COLOUR_NAME" HeaderText="Color" />
                                                        <asp:BoundField DataField="Quantity" HeaderText="Blocked Quantity" />
                                                        <asp:BoundField DataField="CUST_NAME" HeaderText="Customer Name" />
                                                        <asp:BoundField DataField="Delivery_Date" HeaderText="Delivery Date" />
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                    <%-- </ContentTemplate>
                            </asp:UpdatePanel>--%>
                                </td>
                            </tr>
            </table> 
    </asp:Panel>

    <div id="barChart" runat="server" style="width: 100%">
        <uc1:stockreport runat="server" Visible="false" id="stockReport" />

    </div>
</asp:Content>


 
