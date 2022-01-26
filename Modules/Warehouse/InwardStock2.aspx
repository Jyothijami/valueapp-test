<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="InwardStock2.aspx.cs" Inherits="Modules_Warehouse_InwardStock2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .table_border tr td:hover {
            cursor:pointer;
        }
        .ui-autocomplete { max-height: 200px; overflow-y: scroll; overflow-x: hidden;}
    </style>
    <script>
        $(document).ready(function () {
            $('.table_border tr').click(function (event) {
                if (event.target.type == 'number') {
                    e.preventDefault();
                }else if (event.target.type !== 'checkbox') {
                    $(':checkbox', this).trigger('click');
                }
            });

            $("input[type='checkbox']").change(function (e) {
                if ($(this).is(":checked")) { //If the checkbox is checked
                    $(this).closest('tr').addClass("highlight_row");
                    //Add class on checkbox checked
                } else {
                    $(this).closest('tr').removeClass("highlight_row");
                    //Remove class on checkbox uncheck
                }
            });
        });

        //******* Auto Complete ************************************************
        $(document).ready(function () {
            //$.ajax({
            //    type: "POST",
            //    url: "/acomp.asmx/GetItemsWithAbbr",
            //    dataType: "json",
            //    data: "{ itemstr: '" + $("[name$='txtSearchText']").val() + "', itemtype: 'ITEM_NAME' }",
            //    contentType: "application/json; charset=utf-8",
            //    success: function (data) {
            //        alert(data.d);
            //        $("[name$='txtSearchText']").autocomplete({
            //            minLength: 0,
            //            source: data.d,
            //            focus: function (event, ui) {
            //                alert("asd");
            //                $("[id$='txtSearchText']").val(ui.item.Name);
            //                return false;
            //            },
            //            select: function (event, ui) {
            //                alert("asd");
            //                $("[id$='txtSearchText']").val(ui.item.Name);
            //                //$('#selectedValue').text("Selected value:" + ui.item.Abbreviation);
            //                return false;
            //            }
            //        });
            //    },
            //    error: function (XMLHttpRequest, textStatus, errorThrown) {
            //        alert(textStatus + XMLHttpRequest.responseText + errorThrown);
            //    }
            //});

            //$("[name$='txtSearchText']").autocomplete({
            //    source: function (request, response) {
            //        $.ajax({
            //            type: "POST",
            //            url: "/acomp.asmx/GetItemsWithAbbr",
            //            dataType: "json",
            //            data: "{ itemstr: '" + $("[id$='txtSearchText']").val() + "', itemtype: '" + $("[id$='ddlSearchBy']").val() + "' }",
            //            contentType: "application/json; charset=utf-8",
            //            success: function (data) {
            //                response(data.d);
            //            },
            //            failure: function (data) {
            //                alert("failed");
            //            },
            //            error: function (XMLHttpRequest, textStatus, errorThrown) {
            //                alert(textStatus + XMLHttpRequest.responseText + errorThrown);
            //            }
            //        });
            //    },
            //    minLength: 2,
            //    select: function (event, ui) {
            //        //$('#state_id').val(ui.item.id);
            //        //$('#abbrev').val(ui.item.abbrev);
            //    }
            //});
        });
    </script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <table class="stacktable">
        <tr>
            <td>
                <table border="0" cellpadding="3" cellspacing="3" align="right">
                    <tr>
                        <td>
                            <asp:Label ID="Label27" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                Text="Search By"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="textbox">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="ITEM_NAME">Item Name</asp:ListItem>
                                <asp:ListItem Value="ITEM_MODEL_NO">Model No</asp:ListItem>
                                <asp:ListItem Value="PRODUCT_COMPANY_NAME">Brand</asp:ListItem>
                                <asp:ListItem Value="SUP_NAME">Supplier Name</asp:ListItem>
                                <asp:ListItem Value="FPO_NO">PO</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td></td>
                        <td>
                            <asp:TextBox ID="txtSearchText" runat="server" CssClass="textbox">
                            </asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvIS2" runat="server" AutoGenerateColumns="False" DataKeyNames="FPO_DET_ID,PRODUCT_COMPANY_ID,FPO_ID1" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:CheckBox ID="Chkselect" runat="server" style="text-align: center" Checked='<%# bool.Parse(Eval("chk").ToString()) %>' />
                                <asp:HiddenField ID="HFrid1" runat="server" Value='<%# Eval("rid") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" SortExpression="ITEM_CODE" />
                        <asp:BoundField DataField="ITEM_NAME" HeaderText="Item Name" SortExpression="ITEM_NAME" />
                        <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="Model No" SortExpression="ITEM_MODEL_NO" />
                        <asp:BoundField DataField="PRODUCT_COMPANY_NAME" HeaderText="Brand" SortExpression="PRODUCT_COMPANY_NAME" />
                         <asp:BoundField DataField="FPO_DET_QTY" HeaderText="Ordered" SortExpression="FPO_DET_QTY" />
                        <asp:TemplateField HeaderText="Received" SortExpression="NewQty">
                            <ItemTemplate>
                                <asp:TextBox ID="txtnewqty" runat="server" Text='<%# Bind("NewQty") %>' type="number"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rejected" SortExpression="RejectedQty">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRejqty" runat="server" Text='<%# Bind("RejectedQty") %>' type="number"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="FPO_DET_REMARKS" HeaderText="REMARKS" SortExpression="FPO_DET_REMARKS" />
                        <asp:BoundField DataField="SUP_NAME" HeaderText="Suplier" />
                        <asp:BoundField DataField="FPO_NO" HeaderText="PO Number" />
                        <asp:BoundField DataField="COLOUR_NAME" HeaderText="Color" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_Inwardstock2_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                &nbsp;<asp:Button ID="btnCreateMRN1" runat="server" OnClick="btnCreateMRN1_Click" Text="Create MRN" />
            </td>
        </tr>
    </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


 
