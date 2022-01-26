<%@ Page Language="C#" MasterPageFile="~/MPs/PurchaseMP1.master" AutoEventWireup="true"
     CodeFile="ItemHistory.aspx.cs" Inherits="Modules_SCM_Item_History" Title="|| Value App : Purchasing Management : Item History ||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="pagehead">
        <tr>
            <td class="pagehead" align="right" style="text-align: left;">Item History

            </td>
            <td>
                <asp:DropDownList ID="ddlNoOfRecords" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged">
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>75</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>

                </asp:DropDownList>
            </td>
        </tr>
    </table>

    <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="profilehead" colspan="4" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left; width: 272px;">General Details
                        </td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td rowspan="3"></td>
                                    <td rowspan="3"></td>
                                    <td rowspan="3"></td>
                                    <td rowspan="3"></td>
                                    <td rowspan="3"></td>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                </tr>
                            </table>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: left;">
                <table id="tblDdl" runat="server" width="800">
                    <tr>
                        <td colspan="2"></td>
                        <td align="right" style="width: 335px">
                            <asp:Label ID="lblCustomer" runat="server" Text="Supplier Name :" Width="107px"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlSupplierName" runat="server" AutoPostBack="True">
                            </asp:DropDownList><asp:Label ID="Label26" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                                    ControlToValidate="ddlSupplierName" ErrorMessage="Please Select The Supplier Name">*</asp:RequiredFieldValidator></td>
                        <td align="right"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="2"></td>
                        <td align="right" style="width: 335px">
                            <asp:Label ID="Label2" runat="server" Text="Brand :" Width="47px"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlBrandName" runat="server" Width="98px" AutoPostBack="True" OnSelectedIndexChanged="ddlBrandName_SelectedIndexChanged">
                            </asp:DropDownList><asp:Label ID="Label28" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*">
                            </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"
                                ControlToValidate="ddlBrandName" ErrorMessage="Please Select the Brand" InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td align="right"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="2"></td>
                        <td align="right" style="width: 335px">
                            <asp:Label ID="Label39" runat="server" Text="Search :" Width="57px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtSearchModel" runat="server">
                            </asp:TextBox>
                            <asp:Button ID="btnSearchModelNo" runat="server" BorderStyle="None" CausesValidation="False"
                                CssClass="gobutton" EnableTheming="False" OnClick="btnSearchModelNo_Click1" Text="Go"
                                ValidationGroup="Search" /></td>
                        <td align="right" style="text-align: left"></td>
                        <td>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter PropertyName="SelectedValue" Type="Int64" DefaultValue="0" Name="BranbId" ControlID="ddlBrandName"></asp:ControlParameter>
                                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtSearchModel"></asp:ControlParameter>
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"></td>
                        <td align="right" style="width: 335px">
                            <asp:Label ID="Label14" runat="server" Text="Model No :" Width="84px"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlItemCode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemCode_SelectedIndexChanged" Width="71px">
                            </asp:DropDownList><asp:Label ID="Label1" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*">
                            </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="ddlItemCode" ErrorMessage="Please Select the Model No" InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td align="right"></td>
                        <td></td>
                    </tr>
                </table>
                <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label><asp:Label
                    ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:GridView ID="gvItemsMasterDetails" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Visible="False">
                    <Columns>
                        <asp:BoundField DataField="ITEM_MODEL_NO" SortExpression="ITEM_MODEL_NO" HeaderText="Model No"></asp:BoundField>
                        <asp:BoundField HtmlEncode="False" SortExpression="SUP_QUOT_DATE" DataFormatString="{0:dd/MM/yyyy}"
                             DataField="SUP_QUOT_DATE" HeaderText="Purchase Date"></asp:BoundField>
                        <asp:BoundField DataField="SUP_QUOT_SPPRICE" SortExpression="SUP_QUOT_SPPRICE" HeaderText="Rate"></asp:BoundField>
                        <asp:BoundField DataField="PRODUCT_COMPANY_NAME" SortExpression="PRODUCT_COMPANY_NAME" HeaderText="Brand"></asp:BoundField>
                        <asp:BoundField DataField="SUP_NAME" SortExpression="SUP_NAME" HeaderText="Supplier"></asp:BoundField>
                        <asp:BoundField DataField="SUP_QUOT_DET_QTY" SortExpression="SUP_QUOT_DET_QTY" HeaderText="QTY"></asp:BoundField>
                    </Columns>
                    <SelectedRowStyle BackColor="LightSteelBlue" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SCM_ITEMHISTORY_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SupId"
                            PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="ItemCode"
                            PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4"></td>
        </tr>
        <tr>
        </tr>
        <tr>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
</asp:Content>


 
