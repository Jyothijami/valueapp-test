<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/PurchaseMP1.master" AutoEventWireup="true" CodeFile="PurchaseOrder_Report.aspx.cs" Inherits="Modules_Reports_PurchaseOrder_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div id="head" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
            <tr>
                <td style="text-align: left">Purchase Order Details Search</td>
                <td style="text-align: right"><%--Go To Page :
                <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                    <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
               --%> </td>
            </tr>
        </table>
    </div>
    <br />
    <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
        <tr >          
            <td style="text-align: right">
                <asp:Button ID="btnPO" runat="server" Text="Purchase Order" OnClick="btnPO_Click" /></td>
            <td style="text-align: left">
                <asp:Button ID="btnPI" runat="server" Text="Purchase Invoice" OnClick="btnPI_Click" /></td>   
            <td><asp:Button ID="btnSD" runat ="server" Text="Shipmentn Details" /></td>         
        </tr>
    </table>
    <br />
    <asp:Panel ID="pnlPO" runat="server">
        <table style="width: 100%">
            <tr class="profilehead" style="width: 100%">
                <td>PO Search :</td>
                
            </tr>
            <tr>
                <td colspan="4">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align:right">
                    No.Of Records :<asp:DropDownList ID="ddlNoOfRecords" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged">
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>75</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>                   
                </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right; width: 37%;" colspan="2">Model No :
                <asp:TextBox ID="txtModelNo" runat="server"></asp:TextBox>
                </td>

                <td style="text-align: right">Brand :
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlBrand" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right" colspan="2">Customer Name :
                <asp:TextBox ID="txtCustomer" runat="server"></asp:TextBox>
                </td>

                <td style="text-align: right">Supplier Name :
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtSupplierName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right" colspan="2">From Date :
                <asp:TextBox ID="txtFrom" type="datepic" runat="server"></asp:TextBox>
                </td>

                <td style="text-align: right">TO Date :
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtToDate" type="datepic" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right" colspan="2">PO No :
                <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>
                </td>

                <td style="text-align: right"></td>
                <td style="text-align: left">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="75px" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnExportGrid" runat="server" Text="Export To Excel" Width="130px" OnClick="btnExportGrid_Click" />

                </td>
            </tr>

        </table>
        <br />
        <table style="width: 100%">
            <tr>
                <td style="text-align: center">
                    <asp:GridView ID="gvPOSearch" runat="server" AllowPaging="true" Width="100%" OnPageIndexChanging="gvPOSearch_PageIndexChanging" PageSize="20">
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </asp:Panel>

      <asp:Panel ID="pnlPI" runat="server" Visible="false">
        <table style="width: 100%">
            <tr class="profilehead" >
                <td>PI Search :</td>
               
            </tr>
             <tr>
                <td colspan="4">&nbsp;</td>
            </tr>
            <tr>
                <td>
                    No.Of Records :<asp:DropDownList ID="ddlPIgv" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPIgv_SelectedIndexChanged">
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>75</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>                   
                </asp:DropDownList></td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;</td>
            </tr>
            
            <tr>
                <td style="text-align: right; width: 37%;" colspan="2">Model No :
                <asp:TextBox ID="txtPIModelNo" runat="server"></asp:TextBox>
                </td>

                <td style="text-align: right">Brand :
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlPIBrand" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right" colspan="2">Customer Name :
                <asp:TextBox ID="txtPICustName" runat="server"></asp:TextBox>
                </td>

                <td style="text-align: right">Supplier Name :
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtPISupName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right" colspan="2">From Date :
                <asp:TextBox ID="txtPIFromDate" type="datepic" runat="server"></asp:TextBox>
                </td>

                <td style="text-align: right">TO Date :
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtPIToDate" type="datepic" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right" colspan="2">Invoice No :
                <asp:TextBox ID="txtPIInvoiceNo" runat="server"></asp:TextBox>
                </td>
                <td style="text-align: right" >Vehicle No :
                <asp:TextBox ID="txtPIVehicleNo" runat="server"></asp:TextBox>
                </td>
                </tr>
            <tr>
                <td style="text-align: right"></td>
                <td style="text-align: left">
                    <asp:Button ID="btnPISearch" runat="server" Text="Search" Width="75px" OnClick="btnPISearch_Click" />
                    <asp:Button ID="btnPIExport" runat="server" Text="Export To Excel" Width="130px" OnClick="btnPIExport_Click" />

                </td>
            </tr>

        </table>
        <br />
        <table style="width: 100%">
            <tr>
                <td style="text-align: center">
                    <asp:GridView ID="gvPIReport" runat="server" AllowPaging="true" Width="100%" OnPageIndexChanging="gvPIReport_PageIndexChanging" PageSize="20">
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>


 
