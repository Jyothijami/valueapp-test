<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="Temp_Outward.aspx.cs" Inherits="Modules_Warehouse_Temp_Outward" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/jquery-easyui-1.4.1/themes/default/easyui.css" rel="stylesheet" />
    <link href="/jquery-easyui-1.4.1/themes/icon.css" rel="stylesheet" />
    <link href="/jquery-easyui-1.4.1/demo/demo.css" rel="stylesheet" />
    <script src="/jquery-easyui-1.4.1/jquery.easyui.min.js"></script>
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

    <asp:UpdatePanel ID="updatePnl1" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tr class="pagehead">
                    <td style="text-align: left">Temporary Stock Outward :
                    </td>
                </tr>
            </table>
            <br />
            <table style="width: 100%" align="center">
                <tr>
                    <td class="profilehead" colspan="5" style="text-align: left">Item Details 
                    </td>
                </tr>
                <tr>
                    <td colspan="5">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right">Search Model No :
                        <asp:TextBox ID="txtSearchModel" runat="server"></asp:TextBox>

                    </td>
                    <td>
                        <asp:Button ID="btnSearchModelNo" runat="server" BorderStyle="None" CausesValidation="False" CssClass="gobutton"
                            EnableTheming="False" OnClick="btnSearchModelNo_Click" Text="Go" ValidationGroup="Search" />
                    </td>
                    <td colspan="2"></td>
                </tr>
                <tr>
                    <td colspan="5">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right">Brand :
                    
                        <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged"></asp:DropDownList>
                        &nbsp;</td>
                    <td style="width: 5%"></td>
                    <td style="text-align: right">Model No :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlModelNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModelNo_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlModelNo"
                            ErrorMessage="Please Select the Model No." InitialValue="0" ValidationGroup="item">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td colspan="5">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right">Color :
                    
                        <asp:DropDownList ID="ddlColor" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvOriginatedBy" runat="server" ControlToValidate="ddlColor"
                            ErrorMessage="Please Select the Item Color" InitialValue="0" ValidationGroup="item">*</asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="text-align: right">Reference No :
                    </td>
                    <td>
                        <asp:TextBox ID="txtDCNo" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDCNo"
                            ErrorMessage="Please Enter the Reference No." ValidationGroup="item">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td colspan="5">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right">Customer :
                    
                        <asp:DropDownList ID="ddlCustomer" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCustomer"
                            ErrorMessage="Please Select the Customer Name" InitialValue="0" ValidationGroup="item">*</asp:RequiredFieldValidator></td>
                    <td style="width: 5%"></td>
                    <td style="text-align: right">Company :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCompanyProfile1" runat="server" AppendDataBoundItems="True" DataSourceID="compsds1" DataTextField="COMP_NAME" DataValueField="CP_ID">
                            <asp:ListItem Value="0">-- Select --</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCompanyProfile1"
                            ErrorMessage="Please Select the Company Name" InitialValue="0" ValidationGroup="item">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right">Location :
                            <asp:TextBox ID="TextBox2" runat="server" Style="width: 260px;" Text="" Visible="false"></asp:TextBox>
                        <script type="text/javascript">
                            $(document).ready(function () {
                                $('.textbox-text').val($("[name$='TextBox2_text']").val());
                            });

                        </script>
                        <asp:DropDownList ID="ddlLocation" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource22" DataTextField="whname" DataValueField="wh_id">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource22" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="USP_Warehouse_Loc_Select" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="lblCompany" DefaultValue="0" Name="LocID" PropertyName="Text" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <asp:Label ID="lblCompany" runat="server" Visible="False"></asp:Label>

                    </td>
                    <td style="width: 5%"></td>

                    <td style="text-align: right">Quantity :
                    </td>
                    <td>
                        <asp:TextBox ID="txtQty" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtQty"
                            ErrorMessage="Please Enter the Quantity" ValidationGroup="item">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td colspan="5">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="5" style="text-align: center">
                        <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                            CssClass="loginbutton" EnableTheming="False" OnClick="btnAdd_Click" Text="Add" ValidationGroup="item" />
                        <asp:Button ID="btnItemRefresh" runat="server" BackColor="Transparent" BorderStyle="None"
                            CssClass="loginbutton" EnableTheming="False" OnClick="btnItemRefresh_Click" Text="Refresh" CausesValidation="False" /></td>
                </tr>
            </table>
            <br />
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:GridView ID="gvItemDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvItemDetails_RowDataBound"
                            OnRowDeleting="gvItemDetails_RowDeleting" OnRowEditing="gvItemDetails_RowEditing"
                            Width="100%">
                            <Columns>
                                <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                <asp:BoundField DataField="ItemCode" HeaderText="ItemCode"></asp:BoundField>
                                <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                                <asp:BoundField DataField="Brand" HeaderText="Brand"></asp:BoundField>
                                <asp:BoundField DataField="BrandID" HeaderText="Brand Id"></asp:BoundField>
                                <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                                <asp:BoundField DataField="Colorid" HeaderText="Colorid"></asp:BoundField>
                                <asp:BoundField DataField="Reference" HeaderText="Reference No"></asp:BoundField>
                                <asp:BoundField DataField="Customer" HeaderText="Customer"></asp:BoundField>
                                <asp:BoundField DataField="CustomerID" HeaderText="Customer Id"></asp:BoundField>
                                <asp:BoundField DataField="Company" HeaderText="Company"></asp:BoundField>
                                <asp:BoundField DataField="CompanyId" HeaderText="Company Id"></asp:BoundField>
                                <asp:BoundField DataField="LocationId" HeaderText="Location ID"></asp:BoundField>
                                <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                            </Columns>
                            <EmptyDataTemplate>
                                <span style="color: #ff0033">No Data Found</span>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:SqlDataSource
                            ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                            SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter PropertyName="SelectedValue" Type="Int64" DefaultValue="0" Name="BranbId" ControlID="ddlBrand"></asp:ControlParameter>
                                <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtSearchModel"></asp:ControlParameter>
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                    <td>
                        <asp:SqlDataSource ID="compsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT a.CP_ID, a.CP_FULL_NAME + ',' + b.locname AS COMP_NAME FROM YANTRA_COMP_PROFILE AS a INNER JOIN location_tbl AS b ON a.locid = b.locid"></asp:SqlDataSource>
                    </td>
                    <td>
                        <asp:HiddenField ID="TextBox2_value" runat="server" />
                    </td>
                    <td>
                        <asp:HiddenField ID="TextBox2_text" runat="server" />

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>

                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="item"></asp:ValidationSummary>
                    </td>
                </tr>
            </table>
            <br />
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


 
