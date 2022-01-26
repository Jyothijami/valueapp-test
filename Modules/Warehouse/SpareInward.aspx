<%@ Page Title="|| Value App : Warehouse : Spare Inward ||" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="SpareInward.aspx.cs" Inherits="Modules_Warehouse_SpareInward" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .InvoiceTextBox {
            width: 80px !important;
        }
    </style>
    <script src="../../js/jquery-1.9.1.js"></script>

    <script>
        function SellectAll(e) {
            // alert(e.childNodes[0].checked);
            $('#<%=gvSpares.ClientID%> tr').find('td:eq(0)').find('input[type="checkbox"]').prop('checked', e.childNodes[0].checked);

        }
    </script>
    <script src="../../js/jquery-1.9.1.js"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <div id="divSparesInward" style="width: 100%" class="pagehead">
        <table>
            <tr>
                <td style="text-align: left;">Spares Inward
                </td>
            </tr>
        </table>
    </div>

    <div id="divInvoiceItems">
        <br />
        <table style="width: 100%">
            <tr>
                <td style="text-align: right">Location :
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlMainLocation" runat="server" DataSourceID="SqlDataSource1" DataTextField="whname" DataValueField="wh_id" AppendDataBoundItems="True">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="USP_Warehouse_Loc_Select" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="lblCPID" DefaultValue="0" Name="LocID" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>

                <td style="text-align: right">
                    <%--Sub Location :--%>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlSubLocation" Visible="false" runat="server" AutoPostBack="True">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView runat="server" ID="gvSpares" AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox Text="All" runat="server" ID="chkAll" Width="25px" onchange="javscript:SellectAll(this)" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chk" runat="server" Text='<%#Eval("SNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Invoice No">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtInvoiceNo" Text='<%#Eval("Invoice No") %>' CssClass="InvoiceTextBox" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Model No">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtItemModelNo" CssClass="InvoiceTextBox" Text='<%#Eval("Item Model No") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Spare Model No">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtSpareModelNo" CssClass="InvoiceTextBox" Text='<%#Eval("Spare Model No") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sub Category">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtSubCategory" CssClass="InvoiceTextBox" Text='<%#Eval("Sub Category") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Brand">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtBrand" CssClass="InvoiceTextBox" Text='<%#Eval("Brand") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Color">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtColor" CssClass="InvoiceTextBox" Text='<%#Eval("Color") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtQuantity" CssClass="InvoiceTextBox" Text='<%#Eval("Quantity") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Remarks">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Width="400px" Text='<%#Eval("Remarks") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblCPID" runat="server" Visible="false"></asp:Label>
        <asp:Button Text="Save in Warehouse" ID="btnSaveWH" OnClick="btnSaveWH_Click" runat="server" />
        <asp:Button Text="Add New Row" ID="btnAddNewRow" OnClick="btnAddNewRow_Click" runat="server" />
        <asp:Button Text="Delete Row" runat="server" ID="btnDeleteRow" OnClick="btnDeleteRow_Click" />
    </div>
    <br />

    <div id="grid" style="width: 100%">
        <table>
            <tr>
                <td style="width: 100%" class="profilehead">Spare Inward Search
                </td>
            </tr>
        </table>
    </div>
    <%--<asp:UpdatePanel runat="server">
        <ContentTemplate>--%>
    <table style="width: 100%">
        <tr>
            <td>

                <table style="width: 100%">

                    <tr>
                        <td colspan="2" style="text-align: right">Invoice No :
                            <asp:TextBox ID="txtInvNo" runat="server"></asp:TextBox></td>

                        <td style="width: 5%">&nbsp;</td>
                        <td style="text-align: right">Brand : </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtBrand" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">&nbsp;</td>

                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: right">Model No :
                            <asp:TextBox ID="txtModelNo" runat="server"></asp:TextBox></td>
                        <td style="width: 5%">&nbsp;</td>
                        <td style="text-align: right">Location : </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtLocation" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">&nbsp;</td>

                    </tr>
                    <tr>
                        <td colspan="5">
                            <table style="width: 100%" runat="server" visible="false">
                                <tr>
                                    <td colspan="2" style="text-align: right">From Date :
                            <asp:TextBox ID="txtFromDate" type="date" runat="server"></asp:TextBox>
                                        <%--<asp:TextBox ID="txtFromDate" runat="server"  EnableTheming="True" >
                                        </asp:TextBox><asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            ></asp:Image>
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchFrom" runat="server" Enabled="False"
                                            PopupButtonID="imgFromDate" TargetControlID="txtFromDate">
                                        </cc1:CalendarExtender>--%>
                                    </td>
                                    <td style="width: 5%">&nbsp;</td>
                                    <td style="text-align: right">To Date : </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtToDate" type="date" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>


                    <tr>
                        <td colspan="5">&nbsp;</td>

                    </tr>
                </table>


                <table style="width: 100%">
                    <tr>
                        <td colspan="5" style="text-align: center; width: 100%">
                            <asp:Button ID="btnSave" runat="server" Text="Search" OnClick="btnSave_Click" />
                            <asp:Button ID="btnExport" runat="server" Visible="false" Text="Export" />
                        </td>

                    </tr>
                    <tr>
                        <td colspan="5" style="text-align: center; width: 100%">
                            <asp:GridView ID="gvSpareInward" Width="100%" runat="server" EmptyDataText="No Reccords To Display" AllowPaging="True" PageSize="30"></asp:GridView>

                        </td>
                    </tr>
                </table>

            </td>
        </tr>
    </table>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>


 
