<%@ Page Title="|| Value Line App ||" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="Change_Stock_Internal_Location2.aspx.cs" Inherits="Modules_Warehouse_Change_Stock_Internal_Location" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


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
    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <div id="head" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
                    <tr>
                        <td style="text-align: left">Internal Stock Position Management</td>
                        <td style="text-align: right">
                                    <asp:DropDownList ID="ddlNoOfRecords" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged"   >
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
            <table style="width: 100%">

                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 37%;">Model No :
                <asp:TextBox ID="txtModelNo" runat="server"></asp:TextBox>
                    </td>

                    <td style="text-align: right">Sub-Location Name :
                    </td>
                    <td style="text-align: left">
                         <asp:DropDownList ID="ddlSubLocName" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="whLocName" DataValueField="whLocName">
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="select distinct whLocName from V_StockPositionMgnt where Qty >0 order by whLocName"></asp:SqlDataSource>

                        <asp:TextBox ID="txtSubLoc" Visible="false" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">Main Location :
                        <asp:DropDownList ID="ddlLocation" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="locsds1" DataTextField="wh_name" DataValueField="wh_name">
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="locsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="select distinct Location as [wh_name] from V_StockPositionMgnt where Qty >0 order by Location"></asp:SqlDataSource>

                <asp:TextBox ID="txtMainLoc" Visible="false" runat="server"></asp:TextBox>
                    </td>

                    <td style="text-align: right">Color Name :
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtColor" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">From Date :
                <asp:TextBox ID="txtFrom" type="date" runat="server"></asp:TextBox>
                    </td>

                    <td style="text-align: right">TO Date :
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtToDate" type="date" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center">

                        <asp:Label ID="Label1" runat="server" Text="Only Available Stock will be displayed Here." Font-Bold="True" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center" colspan="3">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" Width="75px" OnClick="btnSearch_Click" />
                        <%--<asp:Button ID="btnExportGrid" runat="server" Text="Export To Excel" Width="130px" OnClick="btnExportGrid_Click" />--%>

                    </td>

                </tr>

            </table>
            <br />


            <table style="width: 100%">
                <tr>
                    <td style="text-align: center">
                       
                    </td>
                </tr>

                <asp:GridView ID="gvStock" AllowPaging="true" AllowSorting="true" runat="server" Width="100%"
                    AutoGenerateColumns="False" OnPageIndexChanging="gvStock_PageIndexChanging1"
                    Style="text-align: center" OnRowDataBound="gvStock_RowDataBound">
                    <Columns>

                        <asp:BoundField HeaderText="Item Code" DataField="item_code">
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Model No" DataField="ITEM_MODEL_NO">
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Colour" DataField="COLOUR_NAME">
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="Stock Quantity">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtQuantity" Text='<%# Bind("Qty") %>' runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField HeaderText="Sub Location" DataField="whLocName">
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Location" DataField="Location">
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Location Id(6)" DataField="whLocId">
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
                    </Columns>
                </asp:GridView>

            </table>

        </ContentTemplate>
    </asp:UpdatePanel>
    <table>
        <tr>
            <td>Warehouse Sub Location : &nbsp;&nbsp;
   
                <asp:TextBox ID="TextBox2" runat="server" Style="width: 260px;" Text=""></asp:TextBox>
                <asp:HiddenField ID="TextBox2_value" runat="server" />
                <asp:HiddenField ID="TextBox2_text" runat="server" />
                <script type="text/javascript">
                    $(document).ready(function () {
                        $('.textbox-text').val($("[name$='TextBox2_text']").val());
                    });

                </script>

                <asp:Button ID="btnSave" runat="server" Text="Change Location" OnClick="btnSave_Click" />&nbsp;&nbsp;

            </td>
        </tr>
    </table>
</asp:Content>



