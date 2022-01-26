<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="Modules_Masters_Default3" Title="|| Value App : Product Master ||" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td style="text-align: left">Product Master</td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td colspan="2" style="text-align: center">
                <table id="tblPMDetails" runat="server" border="0" cellpadding="0" cellspacing="0"
                    visible="true" width="100%">
                    <tr>
                        <td class="searchhead" colspan="4" style="text-align: left; width: 100%;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="text-align: left" colspan="2">Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                                        <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
                                    </td>

                                    <td style="text-align: right">
                                        <table border="0" cellpadding="0" cellspacing="0" align="right">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                                        Text="Search By"></asp:Label></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                                        OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">--</asp:ListItem>
                                                        <asp:ListItem Value="ITEM_MODEL_NO">Model No</asp:ListItem>
                                                        <asp:ListItem Value="PRODUCT_COMPANY_NAME">Brand</asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td></td>
                                                <td>
                                                    <asp:TextBox ID="txtSearchText" runat="server" CssClass="textbox">
                                                    </asp:TextBox>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                                        CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                                            </tr>
                                        </table>
                                        <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                                        <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td colspan="4" style="text-align: center">
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>

                                                <asp:GridView ID="gvProductmaster" Width="75%" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                    DataSourceID="SqlDataSource1" OnRowDataBound="gvProductmaster_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Model No">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnModelNo" OnClick="lbtnModelNo_Click" runat="server" ForeColor="#0066ff" Text='<%# Bind("ITEM_MODEL_NO") %>' __designer:wfdid="w2"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ITEM_NAME" HeaderText="Model Name">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PRODUCT_COMPANY_NAME" HeaderText="Brand">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ITEM_CODE" HeaderText="itemcode">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PRODUCT_COMPANY_ID" HeaderText="BrandId">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <SelectedRowStyle BackColor="Silver" />
                                                </asp:GridView>
                                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                                    SelectCommand="SP_MASTER_PRODUCT_MASTER_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                                    <SelectParameters>
                                                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
                                                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
                                                    </SelectParameters>
                                                </asp:SqlDataSource>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align: center;">
                                        <table style="margin: 0px auto;">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnNew" runat="server" OnClick="Button1_Click" Text="New" /></td>
                                                <td>
                                                    <asp:Button ID="Button1" runat="server" Text="Edit" OnClick="Button1_Click1" /></td>
                                                <td>
                                                    <asp:Button ID="brndelete" runat="server" Text="Delete" OnClick="brndelete_Click" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <table id="tblMainDetails" runat="server" border="0" cellpadding="0" cellspacing="0"
                                        visible="false" width="100%">
                                        <tr>
                                            <td class="profilehead" colspan="4" style="text-align: left">Product Master Details :</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: left"></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label2" runat="server" Text="Brand"></asp:Label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label39" runat="server" Text="Search:" Width="84px"></asp:Label></td>
                                            <td>
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
                                            <td style="text-align: right">
                                                <asp:Label ID="Label41" runat="server" Text="Search:" Width="74px"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtPrintSearch" runat="server">
                                                </asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server"
                                                    ControlToValidate="txtPrintSearch" ErrorMessage="Please Enter ModelNo For Search"
                                                    ValidationGroup="Search">*</asp:RequiredFieldValidator><asp:Button ID="btnSearchEssential"
                                                        runat="server" BorderStyle="None" CausesValidation="False" CssClass="gobutton"
                                                        EnableTheming="False" OnClick="btnSearchEssential_Click" Text="Go" ValidationGroup="Search" /><asp:SqlDataSource
                                                            ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                                            SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                                            <SelectParameters>
                                                                <asp:ControlParameter PropertyName="SelectedValue" Type="Int64" DefaultValue="0" Name="BranbId" ControlID="ddlBrand"></asp:ControlParameter>
                                                                <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtPrintSearch"></asp:ControlParameter>
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label3" runat="server" Text="Model No :"></asp:Label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlModelNo" runat="server">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlModelNo" ErrorMessage="Please Select Model No" InitialValue="0" ValidationGroup="sa">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label4" runat="server" Text="Essential No :"></asp:Label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlEssentialNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEssentialNo_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="ddlEssentialNo" ErrorMessage="Please select Essential No" InitialValue="0" ValidationGroup="sa">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label1" runat="server" Text="Item Name :"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtModelName" runat="server"></asp:TextBox>
                                                <asp:Label ID="lblRate" runat="server" Visible="False"></asp:Label>
                                                <asp:Label ID="lblDate" runat ="server" Visible ="false" ></asp:Label>
                                                <asp:Label ID ="lblPreparedBy" runat ="server" Visible ="false" ></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label6" runat="server" Text="Model Specification :"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtModelSpecification" runat="server" CssClass="multilinetext" EnableTheming="False"
                                                    ReadOnly="True" TextMode="MultiLine" Width="89%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right"></td>
                                            <td style="text-align: left"></td>
                                            <td style="text-align: right"></td>
                                            <td style="text-align: left"></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                Quantity :
                                            </td>
                                            <td style="margin-left: 40px">
                                                <asp:TextBox ID="txtQuantity" runat="server">1</asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Please Enter Quantity" ValidationGroup="sa">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td style="text-align: right">
                                            </td>

                                            <td>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right"></td>
                                            <td style="text-align: left"></td>
                                            <td style="text-align: right"></td>
                                            <td style="text-align: left"></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td style="text-align: right">
                                                <asp:Button ID="btnAddProductDetails" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    CssClass="loginbutton" EnableTheming="False" OnClick="btnAddProductDetails_Click"
                                                    Text="Add" ValidationGroup="sa" /></td>
                                            <td style="text-align: left">
                                                <asp:Button ID="btnAddProductDetailsRefresh" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" CausesValidation="False" CssClass="loginbutton" EnableTheming="False"
                                                    OnClick="btnAddProductDetailsRefresh_Click" Text="Refresh" /></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td style="text-align: right"></td>
                                            <td style="text-align: left"></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: center">
                                                <asp:GridView ID="gvInterestedProducts" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvInterestedProducts_RowDeleting"
                                                    OnRowEditing="gvInterestedProducts_RowEditing" OnRowDataBound="gvInterestedProducts_RowDataBound" Width="75%" ShowFooter="True">
                                                    <FooterStyle BackColor="#1AA8BE" />

                                                    <Columns>
                                                        <%--<asp:CommandField ShowEditButton="True"></asp:CommandField>
                                                <asp:CommandField ShowDeleteButton="True"></asp:CommandField>--%>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton Text="Edit" ID="lnkEdit" CommandName="Edit" runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <ItemTemplate>

                                                                <asp:LinkButton Text="Delete" ID="lnkDelete" runat="server" CommandName="Delete" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:BoundField DataField="EssentialCode" HeaderText="Item Code(Sub Item)">
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>

                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        </asp:BoundField>

                                                        <%--<asp:BoundField DataField="EssentialName" HeaderText="Model No">--%>
                                                        <asp:BoundField DataField="EssentialName" HeaderText="Model No">

                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>

                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="ModelNo" HeaderText="Model No">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        </asp:BoundField>

                                                        
                                                        <asp:BoundField DataField="Specification" HeaderText="Specification">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="ItemCode" NullDisplayText="-" HeaderText="Item Code">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="BrandId" HeaderText="Brand_Id"></asp:BoundField>
                                                        <asp:BoundField DataField="EssentialId" HeaderText="Essential_ID"></asp:BoundField>

                                                        <asp:BoundField DataField="ITEM_RATE" HeaderText="Rate">
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>

                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="Qty" HeaderText="Quantity">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="Total"  HeaderText="Total">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Date"  HeaderText="Date">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PreparedBy"  HeaderText="PreparedBy">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <span style="color: #ff0000">No Data to Display!</span>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="4" style="height: 24px; text-align: center"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: center">
                                                <table>
                                                    <tr>
                                                        <td style="height: 26px">
                                                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" /></td>
                                                        <td style="height: 26px">
                                                            <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" OnClick="btnRefresh_Click"
                                                                Text="Refresh" /></td>
                                                        <td style="height: 26px">
                                                            <asp:Button ID="btnClose" runat="server" CausesValidation="False" OnClick="btnClose_Click"
                                                                Text="Close" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left; height: 18px;">
                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="sa" ShowMessageBox="True" ShowSummary="False" ShowValidationErrors="True" ShowModelStateErrors="True"></asp:ValidationSummary>
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="search" ShowMessageBox="True" ShowSummary="false" ShowValidationErrors="True" ShowModelStateErrors="False"></asp:ValidationSummary>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>





</asp:Content>


 
