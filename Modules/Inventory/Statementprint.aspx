<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Statementprint.aspx.cs" Inherits="Modules_Inventory_Statementprint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="tblPrint" visible="false" width="100%">
            <tr>
                <td colspan="4" style="width: 1024px">
                    <strong>Statement of Account </strong>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 27px; text-align: center; width: 1024px;">
                    <table style="text-align: center">
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lblSearch" runat="server" Text="Search Customer" Width="108px"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtSearchModel" runat="server">
                            </asp:TextBox></td>
                            <td style="text-align: left">
                                <asp:Button ID="btnSearchModelNo" runat="server" BorderStyle="None" CausesValidation="False"
                                    CssClass="gobutton" EnableTheming="False" OnClick="btnSearchModelNo_Click" Text="Go" /></td>
                            <td>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                    SelectCommand="SP_Customer_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txtSearchModel" Name="SearchValue" PropertyName="Text"
                                            Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 45px; text-align: right">
                                <asp:Label ID="lblCustomer" runat="server" Text="Customer Name" Width="104px"></asp:Label></td>
                            <td style="height: 45px; text-align: left">
                                <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomerName_SelectedIndexChanged">
                                </asp:DropDownList></td>
                            <td style="height: 45px">
                                <asp:Label ID="lblRegion" runat="server" Text="Region"></asp:Label></td>
                            <td style="height: 45px; text-align: left">
                                <asp:TextBox ID="txtRegion" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtAddress" runat="server" ReadOnly="True" TextMode="MultiLine">
                            </asp:TextBox></td>
                            <td>
                                <asp:Label ID="lblPhone" runat="server" Text="Phone"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtPhone" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                            <td>
                                <asp:Label ID="lblMobile" runat="server" Text="Mobile"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="Label5" runat="server" Text="Purchase Order No" Width="130px"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="ddlPONO" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPONO_SelectedIndexChanged">
                                </asp:DropDownList></td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 27px; text-align: left; width: 1024px;">
               <div id="divExport" runat="server"> 
                    <asp:Panel ID="Panel1" runat="server" Height="50px" Width="100%">
                    <table id="tblhai" width="100%">
                        <tr>
                            <td colspan="3" style="text-align: left">
                                Purchase Order Items&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: left; height: 130px;">
                                <asp:GridView ID="gvItemDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvItemDetails_RowDataBound"
                                    ShowFooter="True" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                                        <asp:BoundField DataField="ModelNo" HeaderText="Model No" />
                                        <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                        <asp:BoundField DataField="UOM" HeaderText="UOM" />
                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                        <asp:BoundField DataField="Rate" HeaderText="Rate" />
                                        <asp:BoundField HeaderText="UnitPrice" />
                                        <asp:BoundField HeaderText="Amount" />
                                        <asp:BoundField DataField="Price" HeaderText="SpPrice" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <span style="color: #ff0000">No Data Exits</span>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                <asp:Label ID="Label31" runat="server" Text="PO Terms & Conditions :"></asp:Label><asp:Label
                                    ID="lblTerms" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: left">
                                Pending Materials</td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: left">
                                <asp:GridView ID="gvPendingMaterial" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvPendingMaterial_RowDataBound"
                                    ShowFooter="True" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                                        <asp:BoundField DataField="ModelNo" HeaderText="Model No" />
                                        <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                        <asp:BoundField DataField="UOM" HeaderText="UOM" />
                                        <asp:BoundField DataField="BalanceQty" HeaderText="Quantity" />
                                        <asp:BoundField DataField="Rate" HeaderText="Rate" />
                                        <asp:BoundField HeaderText="UnitPrice" />
                                        <asp:BoundField HeaderText="Amount" />
                                        <asp:BoundField DataField="Price" HeaderText="SpPrice" />
                                        <asp:BoundField DataField="Quantity" HeaderText="PoQty" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <span style="color: #ff0000">No Data Exits</span>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: left">
                                Supplied Material</td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: center">
                                <asp:GridView ID="GvSuppliedMaterial" runat="server" AutoGenerateColumns="False"
                                    OnRowDataBound="GvSuppliedMaterial_RowDataBound" ShowFooter="True" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="DCNo" HeaderText="DcNo" />
                                        <asp:BoundField DataField="DCDate" HeaderText="DCDate" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                        <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                        <asp:BoundField DataField="POQty" HeaderText="POQty" />
                                        <asp:BoundField DataField="Rate" HeaderText="Rate" />
                                        <asp:BoundField DataField="SPPrice" HeaderText="SpecialPrice " />
                                        <asp:BoundField HeaderText="UnitPrice" />
                                        <asp:BoundField HeaderText="Amount" />
                                        <asp:BoundField DataField="ODcNo" HeaderText="ODcNo" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <span style="color: #ff0000">No Data Exits</span>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: left">
                                Extra Supplied Material</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:GridView ID="gvSupliedMaterialExtra" runat="server" AutoGenerateColumns="False"
                                    OnRowDataBound="gvSupliedMaterialExtra_RowDataBound" ShowFooter="True" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="DCNo" HeaderText="DcNo" />
                                        <asp:BoundField DataField="DCDate" HeaderText="DCDate" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                        <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                        <asp:BoundField DataField="POQty" HeaderText="POQty" />
                                        <asp:BoundField DataField="Rate" HeaderText="Rate" />
                                        <asp:BoundField DataField="SPPrice" HeaderText="SpecialPrice " />
                                        <asp:BoundField HeaderText="UnitPrice" />
                                        <asp:BoundField HeaderText="Amount" />
                                        <asp:BoundField DataField="ODcNo" HeaderText="ODcNo" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <span style="color: #ff0000">No Data Exits</span>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: left">
                                Invoiced Material</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:GridView ID="gvInvoiceMaterial" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvInvoiceMaterial_RowDataBound1"
                                    ShowFooter="True" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="DC No" HeaderText=" Ref:Inv_No" />
                                        <asp:BoundField DataField="ODcNo" HeaderText="Ref:DcNo" />
                                        <asp:BoundField DataField="Remarks" HeaderText="Description" />
                                        <asp:BoundField DataField="UOM" HeaderText="UOM" />
                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                        <asp:BoundField HeaderText="Amount" />
                                        <asp:BoundField DataField="DcNO" HeaderText="DcNO" />
                                        <asp:BoundField DataField="InvoiceNo" HeaderText="InvoiceNo" />
                                        <asp:BoundField DataField="Rate" HeaderText="Rate" />
                                        <asp:BoundField DataField="SIDate" HeaderText="SIDate" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <span style="color: #ff0000">No Data Exits</span>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: left">
                                Return Material</td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: center">
                                <asp:GridView ID="gvreturnmaterial" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvreturnmaterial_RowDataBound"
                                    ShowFooter="True" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                                        <asp:BoundField DataField="ModelNo" HeaderText="Model No" />
                                        <asp:BoundField DataField="ItemName" HeaderText="Item Name">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UOM" HeaderText="UOM">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                        <asp:BoundField DataField="Rate" HeaderText="Rate" />
                                        <asp:BoundField DataField="Vat" HeaderText="Vat" />
                                        <asp:BoundField DataField="CST" HeaderText="CST" />
                                        <asp:BoundField DataField="Excise" HeaderText="Excise" />
                                        <asp:BoundField HeaderText="Amount" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <span style="color: #ff0033">No Data Exists</span>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    </asp:Panel>
               </div>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="4" style="height: 27px; text-align: center; width: 1024px;">
                </td>
            </tr>
            <tr>
                
            </tr>
            <tr>
                <td colspan="4" style="height: 25px; text-align: center; width: 1024px;">
                    <asp:Button ID="btnPrint1" runat="server" OnClick="btnPrint1_Click" Text="Print" />
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Export excel" /></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

 
