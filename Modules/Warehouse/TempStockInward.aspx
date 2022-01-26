<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="TempStockInward.aspx.cs" Inherits="Modules_Warehouse_TempStockInward" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <table style="width: 100%">
                <tr>
                    <td class="profilehead" colspan="4" style="height: 22px; text-align: left">Item Details
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <asp:Label ID="Label12" runat="server" Text="Search By Brand"></asp:Label></td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
                        </asp:DropDownList></td>
                    <td style="text-align: right;">&nbsp;<asp:Label ID="Label39" runat="server" Text="Search:" Width="84px"></asp:Label></td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtSearchModel" runat="server">
                        </asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server"
                            ControlToValidate="txtSearchModel" ErrorMessage="Please Enter ModelNo For Search"
                            ValidationGroup="Search">*</asp:RequiredFieldValidator><asp:Button ID="btnSearchModelNo"
                                runat="server" BorderStyle="None" CausesValidation="False" CssClass="gobutton"
                                EnableTheming="False" OnClick="btnSearchModelNo_Click" Text="Go" ValidationGroup="Search" /><asp:SqlDataSource
                                    ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                    SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:ControlParameter PropertyName="SelectedValue" Type="Int64" DefaultValue="0" Name="BranbId" ControlID="ddlBrand"></asp:ControlParameter>
                                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtSearchModel"></asp:ControlParameter>
                                    </SelectParameters>
                                </asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblModel" runat="server" Text="ModelNo / Model Name" Width="157px"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlItemName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged">
                            <asp:ListItem Value="0">--</asp:ListItem>
                            <asp:ListItem Value="0">-- Select Item Type --</asp:ListItem>
                        </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                            ControlToValidate="ddlItemName" ErrorMessage="Please Select the Model/Item Name"
                            InitialValue="0" SetFocusOnError="True" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                    <td style="text-align: right">
                        <asp:Label ID="lblInstrument" runat="server" Text="Item Name"
                            Width="90px"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtItemName" runat="server">
                        </asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <asp:Label ID="Label32" runat="server" Text="Item Category :"></asp:Label></td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtItemCategory" runat="server" ReadOnly="True">
                        </asp:TextBox></td>
                    <td style="text-align: right;">
                        <asp:Label ID="Label33" runat="server" Text="Item SubCategory :"></asp:Label></td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtItemSubCategory" runat="server" ReadOnly="True">
                        </asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right; height: 10px;">
                        <asp:Label ID="Label54" runat="server" Text="Color :"></asp:Label></td>
                    <td style="text-align: left; height: 10px;">&nbsp;<asp:DropDownList ID="ddlcolor" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlcolor" ErrorMessage="Please Select the Color" InitialValue="0" ValidationGroup="id">*</asp:RequiredFieldValidator>
                    </td>
                    <td style="text-align: right; height: 10px;">
                        <asp:Label ID="Label55" runat="server" Text="Brand :"></asp:Label></td>
                    <td style="text-align: left; height: 10px;">
                        <asp:TextBox ID="txtManufacturer" runat="server">
                        </asp:TextBox></td>
                </tr>

                <tr>
                    <td style="text-align: right;">
                        <asp:Label ID="Label8" runat="server" Text="Rate" Visible="false"></asp:Label></td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtRate" runat="server" Visible="false">
                        </asp:TextBox></td>
                    <td style="text-align: right;">
                        <asp:Label ID="Label9" runat="server" Text="Location"></asp:Label></td>
                    <td style="text-align: left;">
                        <%--<asp:DropDownList ID="ddlgodown" runat="server" AutoPostBack="True">
                </asp:DropDownList>--%>
                        <asp:DropDownList ID="ddlgodown" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlDataSource22" DataTextField="whname" DataValueField="wh_id">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlgodown" ErrorMessage="Please Select the Location" InitialValue="0" ValidationGroup="id" ForeColor="Red">*</asp:RequiredFieldValidator>
                        <asp:SqlDataSource ID="SqlDataSource22" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="USP_Warehouse_Loc_Select" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="lblCPID" DefaultValue="0" Name="LocID" PropertyName="Text" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>

                    </td>
                </tr>

                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="Label6" runat="server" Text="Accepted Quantity:"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtAcceptedqty" runat="server">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtAcceptedqty" ErrorMessage="Please Enter the Quantity" ValidationGroup="id" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                    <td style="text-align: right">
                        <asp:Label ID="Label7" runat="server" Text="Rejected Quantity:"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtRejectedqty" runat="server">0</asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right; height: 9px;">
                        <asp:Label ID="lblSerialNo" runat="server" Text="Reference No" Visible="true"></asp:Label></td>
                    <td style="text-align: left; height: 9px;">
                        <asp:TextBox ID="txtReferenceNo" runat="server" Visible="true">OS</asp:TextBox></td>
                    <td style="text-align: right; height: 9px;"></td>
                    <td style="text-align: left; height: 9px;">&nbsp;
                <asp:Label ID="lblCPID" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="Label13" runat="server" Text="Remarks"></asp:Label></td>
                    <td style="text-align: left" colspan="3">
                        <asp:TextBox ID="txtRemarks" runat="server" Height="53px" TextMode="MultiLine" Width="606px" >-</asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center">
                        <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                            CssClass="loginbutton" EnableTheming="False" OnClick="btnAdd_Click" Text="Add" ValidationGroup="id" /><asp:Button
                                ID="btnItemRefresh" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" OnClick="btnItemRefresh_Click" Text="Refresh" CausesValidation="False" /></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center">
                        <asp:GridView ID="gvItemDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvItemDetails_RowDataBound"
                            OnRowDeleting="gvItemDetails_RowDeleting" OnRowEditing="gvItemDetails_RowEditing"
                            Width="100%">
                            <Columns>
                                <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                <asp:BoundField DataField="ItemCode" HeaderText="ItemCode"></asp:BoundField>
                                <asp:BoundField DataField="ItemType" HeaderText="Model No"></asp:BoundField>
                                <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                <asp:BoundField DataField="Godown" HeaderText="Location"></asp:BoundField>
                                <asp:BoundField DataField="NetQty" HeaderText="Net Qty"></asp:BoundField>
                                <asp:BoundField DataField="ReQuantity" HeaderText="Rejected Qty"></asp:BoundField>
                                <asp:BoundField DataField="Godownid" HeaderText="Godownid"></asp:BoundField>
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
                                <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                                <asp:BoundField DataField="Colorid" HeaderText="Colorid"></asp:BoundField>
                                <asp:BoundField DataField="RefNo" HeaderText="Reference No"></asp:BoundField>
                            </Columns>
                            <EmptyDataTemplate>
                                <span style="color: #ff0033">No Data Found</span>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                </tr>
            </table>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


 
