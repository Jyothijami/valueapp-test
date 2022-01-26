<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="Inward_Management.aspx.cs" Inherits="Modules_Warehouse_Inward_Management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .InvoiceTextBox {
            width: 80px !important;
        }
    </style>
    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <link href="/jquery-easyui-1.4.1/themes/default/easyui.css" rel="stylesheet" />
    <link href="/jquery-easyui-1.4.1/themes/icon.css" rel="stylesheet" />
    <link href="/jquery-easyui-1.4.1/demo/demo.css" rel="stylesheet" />
    <script src="/jquery-easyui-1.4.1/jquery.easyui.min.js"></script>

    

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

    <script type="text/javascript">
        $(document).ready(function () {
            $('[name$="TextBox2"]').combotree({
                url: '/tree_data1.json',
                method: 'get',
                required: true
            });
        });
    </script>




</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

    <div id="head" style="width: 100%">
        <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
            <tr>
                <td style="text-align: left;">Inward Management :</td>

                <td style="text-align: right"></td>
            </tr>
        </table>
    </div>
    <br />
    <div id="body">
        <table style="width: 100%">
            <tr>
                <td>Category :
                </td>
                <td>
                    <asp:DropDownList ID="ddlCategory" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td>Sub Category :
                </td>
                <td>
                    <asp:DropDownList ID="ddlSubCat" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlSubCat_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td>Model No :
                </td>
                <td>
                    <asp:DropDownList ID="ddlModelNo" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlModelNo_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="GO" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <br />
        <table style="width: 100%">
            <tr>
                <td>Search By Model No : 
                    <asp:TextBox ID="txtModelNo" runat="server"></asp:TextBox>
                    Location :
                    <asp:TextBox ID="txtLocation" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                        CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" />
                </td>
            </tr>
        </table>
        <br />
        <table style="width: 100%">
            <tr>
                <td style="text-align: center; font-weight: bold;">
                    <asp:LinkButton ID="lnkMRN" runat="server" OnClick="lnkMRN_Click" Font-Underline="True">MRN</asp:LinkButton>
                    &nbsp;||&nbsp;
                            <asp:LinkButton ID="lnkSalesReturn" runat="server" OnClick="lnkSalesReturn_Click" Font-Underline="True">Sales Return</asp:LinkButton>
                    &nbsp;||&nbsp;
                            <asp:LinkButton ID="lnkSampleReturn" runat="server" OnClick="lnkSampleReturn_Click" Font-Underline="True">Sample Return</asp:LinkButton>
                    &nbsp;||&nbsp;
                            <asp:LinkButton ID="lnkAll" runat="server" OnClick="lnkAll_Click" Font-Underline="True">All</asp:LinkButton>
                    &nbsp;||&nbsp;
                            <asp:LinkButton ID="lnkStockMove" runat="server" OnClick="lnkStockMove_Click" Font-Underline="True">Stock Movement</asp:LinkButton>

                </td>
            </tr>
        </table>
        <br />
        <table style="width: 100%">
            <tr>
                <td>Warehouse Location : 
    <br />
                    <asp:TextBox ID="TextBox2" runat="server" Style="width: 260px;" Text=""></asp:TextBox>
                    <asp:HiddenField ID="TextBox2_value" runat="server" />
                    <asp:HiddenField ID="TextBox2_text" runat="server" />
                    <script type="text/javascript">
                        $(document).ready(function () {
                            $('.textbox-text').val($("[name$='TextBox2_text']").val());
                        });

                    </script>

                    <asp:Button ID="btnSave" runat="server" Text="Save To Warehouse" OnClick="btnSave_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnSavems" runat="server" Visible="false" Text="Move To Warehouse" OnClick="btnSavems_Click" />&nbsp;&nbsp;

                </td>
            </tr>
        </table>
        <br />
        <div style="width:100%; color: #FF6666; font-weight: 700; text-align: center; font-size: medium;" id="msgBox">
            <span>Please Enter upto 100 Quantity at a time for a Model No (For Best Performance)</span>
        </div>
        <br />
        <table style="width: 100%">
            <tr>
                <td style="text-align: center;">
                    <asp:GridView ID="gvInwardItems" AllowPaging="true" AllowSorting="true" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvInwardItems_RowDataBound" OnPageIndexChanging="gvInwardItems_PageIndexChanging" PageSize="5">
                        <Columns>
                            <asp:BoundField HeaderText="Id" DataField="Id">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Reference No" DataField="Reference_No">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="ItemCodeHidden" DataField="ItemCode">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Model No" DataField="ITEM_MODEL_NO">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Category" DataField="Item_Category">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Sub Category" DataField="Item_SubCategory">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Color" DataField="COLOUR_NAME">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Received Quantity">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtReceivedQty" Text='<%# Bind("Balance_Qty") %>' runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Damage Quantity">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDamageQty" Text='<%# Bind("Damage_Qty") %>' runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="ColorId" DataField="Color_Id">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Received QtyHidden" DataField="Balance_Qty">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Damage QtyHidden" DataField="Damage_Qty">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Location" DataField="whname">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:CheckBox ID="cbSelectAll" runat="server" Text="All" OnClick="selectAll(this)" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox_row" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField HeaderText="CustId" DataField="CustId">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                              <asp:BoundField HeaderText="DeliveryDate" DataField="DeliveryDate"  HtmlEncode="false" DataFormatString="{0:MM/dd/yyyy}">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>


                    <asp:GridView ID="gvMovingItems" runat="server" Visible="False" AutoGenerateColumns="False" Style="text-align: center" Width="100%" OnRowDataBound="gvMovingItems_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="SM_DCDET_ID" HeaderText="SMDetId">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ITEM_CODE" HeaderText="Itemcode" />
                            <%--<asp:BoundField DataField="Brand" HeaderText="Brand" />--%>
                            <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="ModelNo">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="COLOUR_NAME" HeaderText="Color">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <%--<asp:BoundField DataField="Qty" HeaderText="Qty" />--%>
                            <asp:TemplateField HeaderText="Quantity">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Bind("QUANTITY") %>'>></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="whname" HeaderText="Moving To">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Remark" HeaderText="Remarks">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CLIENT_NAME" HeaderText="ClientName">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <%--<asp:BoundField DataField="BrandId" HeaderText="BrandId" />--%>
                            <asp:BoundField DataField="COLOR_ID" HeaderText="ColorId" />
                            <asp:BoundField DataField="QUANTITY" HeaderText="QUANTITYHidden" />

                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:CheckBox ID="cbSelectAll" runat="server" Text="All" OnClick="selectAll(this)" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="ChkBox_row" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="MOVINGFROM" HeaderText="LocId" />

                            
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <br />
        <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
            <tr>
                <td style="text-align: left;">Spare Inward Management :</td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td style="text-align: center">
                    <asp:GridView runat="server" ID="gvSpares" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" OnSelectedIndexChanged="gvSpares_SelectedIndexChanged">
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
                    <asp:Button Text="Add New Row" ID="btnAddNewRow" OnClick="btnAddNewRow_Click" runat="server" />
                    <asp:Button Text="Delete Row" runat="server" ID="btnDeleteRow" OnClick="btnDeleteRow_Click" />
                    <asp:Button Text="Save in Warehouse" ID="btnSaveWH" OnClick="btnSaveWH_Click" runat="server" />

                </td>
            </tr>
        </table>
    </div>
    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>


 
