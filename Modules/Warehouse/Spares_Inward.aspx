<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="Spares_Inward.aspx.cs" Inherits="Modules_Warehouse_Spares_Inward" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function SellectAll(e) {
            // alert(e.childNodes[0].checked);
            $('#<%=gvSparesInward.ClientID%> tr').find('td:eq(0)').find('input[type="checkbox"]').prop('checked', e.childNodes[0].checked);

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div id="divSparesInward" style="width: 100%">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" class="pagehead">
                    <tr>
                        <td class="pagehead" style="text-align: left;">Spares Inward
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlNoOfRecords" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
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
            <br />
            <div id="divSparesGrid" style="width: 100%">
                <table style="width: 100%">
                    <tr class="searchhead" style="width: 100%">
                        <td style="text-align: left">Spares Inward Item Details 
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView runat="server" ID="gvSparesInward" AutoGenerateColumns="false" Width="100%">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox Text="All" runat="server" ID="chkAll" Width="25px" onchange="javscript:SellectAll(this)" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Invoice No">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtInvoiceNo" CssClass="InvoiceTextBox" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Model No">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtItemModelNo" CssClass="InvoiceTextBox" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Spare Model No">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtSpareModelNo" CssClass="InvoiceTextBox" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sub Category">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtSubCategory" CssClass="InvoiceTextBox" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Brand">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtBrand" CssClass="InvoiceTextBox" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Color">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtColor" CssClass="InvoiceTextBox" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtQuantity" CssClass="InvoiceTextBox" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Width="400px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Button Text="Add New Row" ID="btnAddNewRow" OnClick="btnAddNewRow_Click" runat="server" />
                            <asp:Button Text="Delete Row" runat="server" ID="btnDeleteRow" OnClick="btnDeleteRow_Click" />
                        </td>
                    </tr>
                </table>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


 
