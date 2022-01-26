<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/AdminMP1.master" AutoEventWireup="true" CodeFile="ItemHistory.aspx.cs" Inherits="Modules_Masters_ItemHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .pagehead {
            text-align: center;
        }
        .auto-style1 {
            font-size: large;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table style="width: 100%">
        <tr class="pagehead">
            <td style="text-align: left">Item History
            </td>
        </tr>
    </table>
    <div id="divLeaveApp">
        <table align="center">

            <tr style="text-align: left">
                <td>Item Model No  :
                </td>
                <td>
                    <asp:Label ID="lblItemModelNo" runat="server" Text=""></asp:Label>
                </td>

                <td style="width: 35%"></td>

                <td>Item Name :
                </td>
                <td>
                    <asp:Label ID="lblItemName" runat="server" Text=""></asp:Label>
                </td>

            </tr>
            <tr style="text-align: left">
                <td>Item Category  :
                </td>
                <td>
                    <asp:Label ID="lblItemCategory" runat="server" Text=""></asp:Label>
                </td>

                <td style="width: 35%"></td>

                <td>Item Sub Category :
                </td>
                <td>
                    <asp:Label ID="lblSubCategory" runat="server" Text=""></asp:Label>
                </td>

            </tr>
            <tr style="text-align: left">
                <td>Brand  :
                </td>
                <td>
                    <asp:Label ID="lblBrand" runat="server" Text=""></asp:Label>
                </td>

                <td style="width: 35%"></td>

                <td>Principal Name :
                </td>
                <td>
                    <asp:Label ID="lblPrincipalName" runat="server" Text=""></asp:Label>
                </td>

            </tr>
            <tr style="text-align: left">
                <td>Color :
                </td>
                <td>
                    <asp:Label ID="lblColor" runat="server" Text=""></asp:Label>
                </td>

                <td style="width: 35%"></td>

                <td>Item Series :
                </td>
                <td>
                    <asp:Label ID="lblItemSeries" runat="server" Text=""></asp:Label>
                </td>

            </tr>                     
            <tr style="text-align: left">
                <td>Material Type  :
                </td>
                <td>
                    <asp:Label ID="lblMaterialType" runat="server" Text=""></asp:Label>
                </td>

                <td style="width: 35%"></td>

                <td>UOM :
                </td>
                <td>
                    <asp:Label ID="lblUOM" runat="server" Text=""></asp:Label>
                </td>

            </tr>            
            <tr style="text-align: left">
                <td>Item Specification  :
                </td>
                <td colspan="4">
                    <asp:Label ID="lblItemSpecification" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="text-align: left">
                <td>Purchase Specification  :
                </td>
                <td colspan="4">
                    <asp:Label ID="lblPurchaseSpec" runat="server" Text=""></asp:Label>
                </td>
            </tr>

        </table>
        <br />
        <table style="width: 100%; text-align: center">
            
            <tr >
                <td style="width:100%;text-align: center;font-size: x-large; ">Item Images</td>
            </tr>
            <tr >
                <td style="text-align: center">
                    <asp:DataList ID="DataList1" runat="server" CellPadding="5" DataSourceID="itemimagessds1" RepeatColumns="6" Width="100%">
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("Item_Image", "~/Content/ItemImage/{0}") %>' Width="100px" />
                        </ItemTemplate>
                    </asp:DataList>
                    <asp:SqlDataSource ID="itemimagessds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT * FROM [YANTRA_ITEM_IMAGE] WHERE ([Item_Code] = @Item_Code)">
                                            <SelectParameters>
                                                <asp:QueryStringParameter Name="Item_Code" QueryStringField="Cid" Type="Int64" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td  style="text-align: center; font-size: x-large; ">Item Drawings</td>
            </tr>
            <tr >
                <td style="text-align: center">
                    <asp:DataList ID="DataList2" runat="server" CellPadding="5" DataSourceID="itemimagessds2" RepeatColumns="6" Width="100%">
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("Item_Image", "~/Content/ItemImage/{0}") %>' Width="100px" />
                        </ItemTemplate>
                    </asp:DataList>
                   <asp:SqlDataSource ID="itemimagessds2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT * FROM [YANTRA_ITEM_IMAGE] WHERE ([Item_Code] = @Item_Code)">
                                            <SelectParameters>
                                                <asp:QueryStringParameter Name="Item_Code" QueryStringField="Cid" Type="Int64" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                </td>
            </tr>
            <tr >
                <td align="center">
                    <span class="auto-style1">I</span><strong><span class="auto-style1">tem Price History</span></strong></td>
            </tr>
        </table>
    </div>
    <div id="itemHistoryGrid">
        <table style="text-align:center;width:100%;">
            <tr>
                <td >
                    <asp:GridView ID="gvItemPriceHostory" runat="server" Width="100%" DataSourceID="SqlDataSource1" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="Item_Code" HeaderText="Code" SortExpression="Item_Code" />
                            <asp:BoundField DataField="Old_Price" HeaderText="Old Price" SortExpression="Old_Price" />
                            <asp:BoundField DataField="New_Price" HeaderText="New Price" SortExpression="New_Price" />
                            <asp:BoundField DataField="Percent_Increase" HeaderText="% of Increase" SortExpression="Percent_Increase" />
                            <asp:BoundField DataField="Date_Modified" HeaderText="Modified Date" SortExpression="Date_Modified" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" />
                        </Columns>
                        
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT * FROM YANTRA_ITEM_PRICE_HISTORY WHERE (Item_Code = @Item_Code)">
                                            <SelectParameters>
                                                <asp:QueryStringParameter Name="Item_Code" QueryStringField="Cid" Type="Int64" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td class="auto-style1" >
                    <strong>Available Stock</strong></td>
            </tr>
            <tr>
                <td >
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ITEM_QTY_ID" DataSourceID="SqlDataSource2" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="Model No" SortExpression="ITEM_MODEL_NO" />
                            <asp:BoundField DataField="ITEM_NAME" HeaderText="Item Name" SortExpression="ITEM_NAME" />
                            <asp:BoundField DataField="COLOUR_NAME" HeaderText="Color Name" SortExpression="COLOUR_NAME" />
                            <asp:BoundField DataField="ITEM_QTY_IN_HAND" HeaderText="Qty In Hand" SortExpression="ITEM_QTY_IN_HAND" />
                            <asp:BoundField DataField="GODOWN_NAME" HeaderText="Godown Name" SortExpression="GODOWN_NAME" />
                            <asp:BoundField DataField="CP_FULL_NAME" HeaderText="Company Name" SortExpression="CP_FULL_NAME" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand=" select CP_FULL_NAME,GODOWN_NAME,COLOUR_NAME,YANTRA_ITEM_QTY.ITEM_QTY_IN_HAND,dbo.YANTRA_ITEM_MAST.ITEM_NAME, dbo.YANTRA_ITEM_MAST.ITEM_MODEL_NO, dbo.YANTRA_ITEM_MAST.BRAND_ID, dbo.YANTRA_ITEM_QTY.*,dbo.YANTRA_LKUP_ITEM_TYPE.IT_TYPE,dbo.YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_NAME,dbo.YANTRA_LKUP_ITEM_CATEGORY.ITEM_CATEGORY_NAME  from  YANTRA_COMP_PROFILE,YANTRA_LKUP_COLOR_MAST,YANTRA_LKUP_GODOWN,[YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],YANTRA_LKUP_ITEM_CATEGORY,YANTRA_LKUP_PRODUCT_COMPANY,YANTRA_ITEM_QTY    WHERE YANTRA_ITEM_QTY.COLOUR_ID=YANTRA_LKUP_COLOR_MAST.COLOUR_ID AND YANTRA_ITEM_QTY.GODOWN_ID=YANTRA_LKUP_GODOWN.GODOWN_ID AND YANTRA_COMP_PROFILE.CP_ID=YANTRA_ITEM_QTY.CP_ID AND YANTRA_ITEM_QTY.ITEM_CODE = [YANTRA_ITEM_MAST].ITEM_CODE and [YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID AND  YANTRA_ITEM_MAST.IC_ID = YANTRA_LKUP_ITEM_CATEGORY.ITEM_CATEGORY_ID and YANTRA_ITEM_MAST.BRAND_ID = YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID  AND (YANTRA_ITEM_MAST.ITEM_CODE = @Item_Code) ORDER BY [YANTRA_ITEM_MAST].ITEM_CODE DESC">  <SelectParameters>
                                                <asp:QueryStringParameter Name="Item_Code" QueryStringField="Cid" Type="Int64" />
                                            </SelectParameters></asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td >
                    &nbsp;</td>
            </tr>
        </table>

    </div>
</asp:Content>


 
