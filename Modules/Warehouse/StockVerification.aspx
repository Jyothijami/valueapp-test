<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="StockVerification.aspx.cs" Inherits="Modules_Warehouse_StockVerification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">

    <div style="width: 100%">
                <table style="width: 100%" class="pagehead">
                    <tr>
                        <td style="text-align: left;">Physical Stock Verification Page :
                        </td>

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
        <table style ="width :100%">
            <tr>
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
                <td style="text-align: right">Upload File : </td>
                <td style="text-align: left">
                        <asp:FileUpload ID="FileUpload1" type="file" CssClass="styled" runat="server" />

                </td>
                <td>
                      <asp:Button ID="btnfileUpload" Text="Upload" CssClass="btn btn-danger" OnClick="btnfileUpload_Click" runat="server" />
                </td>
            </tr>
        </table>
        
    </div>
    <div>
        <table style="width: 100%">
            <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvPhyVerif" runat="server" DataSourceID="SqlDataSource1" Width="100%" GridLines="None" AllowPaging="true" PageSize="25" ></asp:GridView>
                        
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" 
           SelectCommand="select ID,phyStockverification_tbl.Item_code as Code,ITEM_MODEL_NO as ModelNo ,PRODUCT_COMPANY_NAME as Brand,COLOUR_NAME as Colour ,wh_id as Location,AppOS,AppInward,AppOutward,AppBlocked,AppAvailStock,AppCS,CONVERT(Nvarchar(50),AppOnDate,103)as AppDate,ExcelOS,ExcelInward,ExcelOutward,ExcelBlocked,ExcelAvailStock,ExcelCS,ExcelLocation,CONVERT(Nvarchar(50),ExcelOnDate,103)as ExcelDt from phyStockverification_tbl inner join YANTRA_ITEM_MAST on phyStockverification_tbl.item_code=YANTRA_ITEM_MAST .ITEM_CODE inner join YANTRA_LKUP_COLOR_MAST on phyStockverification_tbl.color_id=YANTRA_LKUP_COLOR_MAST .COLOUR_ID inner join YANTRA_LKUP_PRODUCT_COMPANY on phyStockverification_tbl.brand_id=YANTRA_LKUP_PRODUCT_COMPANY .PRODUCT_COMPANY_ID  order by ID desc"></asp:SqlDataSource>
                        </td>
                    </tr> 
        </table>
    </div>
</asp:Content>

