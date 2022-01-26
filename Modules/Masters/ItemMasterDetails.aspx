<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/AdminMP1.master" AutoEventWireup="true" CodeFile="ItemMasterDetails.aspx.cs" Inherits="MASTERS_ItemMasterDetails" %>


<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .searchhead {
            text-align: right;
        }

        .pagination span {
            background-color: #FF0000 !important;
            color: blue !important;
            font-size: 18px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <table style="width: 100%">
        <tr>
            <td colspan="2 " style="height: 30px; text-align: left; font-size: 16px"><b>Item Details</b>
                <asp:ImageButton ID="ImageButton1" runat="server" Height="42px" ImageUrl="~/Images/AddNew.png" Width="48px" OnClick="ImageButton1_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <b>Discontinued Items Upload</b>
                <asp:ImageButton ID="ImageButton3" runat="server" Height="42px" ImageUrl="~/Images/AddNew.png" Width="48px" OnClick="ImageButton3_Click" />
              
            </td>
            <td style="text-align: right">
                <asp:DropDownList ID="ddlNoOfRecords" runat="server" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>75</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr style="text-align: center">
            <td style="text-align: left">Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
            </td>
            <td style="text-align: right">
                <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                    Text="Search By"></asp:Label>
                <%----%>
                <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="textbox" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                    <asp:ListItem Value="0">--</asp:ListItem>
                    <asp:ListItem Value="ITEM_CODE">Item Code</asp:ListItem>
                    <asp:ListItem Value="ITEM_NAME">Item Name</asp:ListItem>
                    <asp:ListItem Value="ITEM_MODEL_NO">Model No</asp:ListItem>

                    <asp:ListItem Value="PRODUCT_COMPANY_NAME">Brand</asp:ListItem>
                    <asp:ListItem Value="IT_TYPE">Sub Category</asp:ListItem>
                    <asp:ListItem Value="ITEM_CATEGORY_NAME">Category</asp:ListItem>
                    <asp:ListItem Value="HSN_Code">HSN Code</asp:ListItem>
                    <asp:ListItem Value="Discontinued">Discontinued Items</asp:ListItem>

                </asp:DropDownList>
                <asp:TextBox ID="txtSearchText1" runat="server" Width="111px"></asp:TextBox>
                <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                    CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnSearchGo_Click" />
            </td>
        </tr>

        <tr>

            <td style="text-align: left" colspan="2">

                <div style="overflow: scroll">

                    <asp:GridView ID="gvItemMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        ShowFooter="true" DataSourceID="SqlDataSource1" Style="text-align: left" Width="100%"
                        OnRowDataBound="gvItemMaster_RowDataBound">
                        <PagerStyle CssClass="pagination" HorizontalAlign="Left" VerticalAlign="Middle" />
                        <FooterStyle ForeColor="#0066ff" />
                        <Columns>
                            <asp:BoundField DataField="ITEM_CODE" HeaderText="Item code" />
                            <asp:BoundField DataField="HSN_Code" HeaderText="HSN Code" />
                            <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="Model No" />
                            <asp:BoundField DataField="ITEM_NAME" HeaderText="Item Name" />
                            <asp:BoundField DataField="ITEM_SERIES" Visible="false" HeaderText="Item Series" />
                            <asp:BoundField DataField="ITEM_CATEGORY_NAME" HeaderText="Category" />
                            <asp:BoundField DataField="IT_TYPE" HeaderText="Sub Category" />
                            <asp:BoundField DataField="PRODUCT_COMPANY_NAME" HeaderText="Brand" />
                            
                            
                            <asp:BoundField DataField="Prepared By" HeaderText="Prepared By" />
                            <asp:BoundField DataField="dt_added" HeaderText="Date Added" />
                            <asp:BoundField DataField="F1" HeaderText="Updated By" />
                            <asp:BoundField DataField="dt_updated" HeaderText="Date Updated" />

                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibtmImage" runat="server" ImageUrl="~/Images/Edit.png" Width="18px" OnCommand="ibtmImage_Command" CommandArgument='<%# Eval("ITEM_CODE").ToString() %>' />
                                    <%--<asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/Edit.png" Width="18px" PostBackUrl='<%# "~/Modules/MASTERS/ItemEdit.aspx?Cid=" + Eval("ITEM_CODE") %>' />--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton2" runat="server" ForeColor="Red" BackColor="WhiteSmoke" ImageUrl="~/Images/Delete.png" Width="18px" OnClick="ImageButton2_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="History" Visible="false">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/Edit.png" Width="18px" PostBackUrl='<%# "~/Modules/MASTERS/ItemHistory.aspx?Cid=" + Eval("ITEM_CODE") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField ="f2" HeaderText ="Discontinued" />
                        </Columns>
                        <EmptyDataTemplate>
                            <span style="color: #FF0000">No Data to Display</span>
                        </EmptyDataTemplate>
                    </asp:GridView>

                </div>

            </td>
        </tr>

    </table>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_Masters_ITEMMASTER_SEARCH_SELECT" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
</asp:Content>






