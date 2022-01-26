<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductMaster.ascx.cs" Inherits="Modules_Masters_ProductMaster1" %>
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td colspan="2" style="text-align: center">
            <table id="tblPMDetails" runat="server" border="0" cellpadding="0" cellspacing="0"
                visible="true" width="100%">
                <tr>
                    <td class="profilehead" colspan="4" style="height: 20px; text-align: left">
                        Product Master :</td>
                    <td class="profilehead" colspan="1" style="height: 20px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td align="right" style="height: 21px">
                        <asp:Label ID="Label3" runat="server" Text="Model No :"></asp:Label></td>
                    <td align="left" style="height: 21px">
                        <asp:DropDownList ID="ddlModelNo" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                        </td>
                    <td align="right" style="height: 21px">
                        <asp:Label ID="Label4" runat="server" Text="Essential No :"></asp:Label></td>
                    <td align="left" style="height: 21px">
                        &nbsp;<asp:DropDownList ID="ddlEssentialNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEssentialNo_SelectedIndexChanged">
                        </asp:DropDownList>
                        </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="Label1" runat="server" Text="Model Name :"></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="txtModelName" runat="server"></asp:TextBox></td>
                    <td style="text-align: right">
                    </td>
                    <td style="text-align: left">
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="Label6" runat="server" Text="Model Specification :"></asp:Label>&nbsp;</td>
                    <td colspan="3" align="left">
                        <asp:TextBox ID="txtModelSpecification" runat="server" CssClass="multilinetext" EnableTheming="False"
                            ReadOnly="True" TextMode="MultiLine" Width="89%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 19px; text-align: right">
                        <asp:Button ID="btnAddProductDetails" runat="server" BackColor="Transparent" BorderStyle="None"
                            CssClass="loginbutton" EnableTheming="False" OnClick="btnAddProductDetails_Click"
                            Text="Add" ValidationGroup="othercorp" /></td>
                    <td colspan="2" style="height: 19px; text-align: left">
                        <asp:Button ID="btnAddProductDetailsRefresh" runat="server" BackColor="Transparent"
                            BorderStyle="None" CausesValidation="False" CssClass="loginbutton" EnableTheming="False"
                            OnClick="btnAddProductDetailsRefresh_Click" Text="Refresh" /></td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center">
                        &nbsp;<asp:GridView ID="gvInterestedProducts" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvInterestedProducts_RowDeleting"
                            OnRowEditing="gvInterestedProducts_RowEditing" Width="100%">
                            <Columns>
                                <asp:CommandField ShowEditButton="True" />
                                <asp:CommandField ShowDeleteButton="True" />
                                <asp:BoundField DataField="EssentialCode" HeaderText="Essential Code">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="EssentialName" HeaderText="Essential Name ">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ModelNo" HeaderText="Model No">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ItemCode" HeaderText="Item Code" NullDisplayText="-">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="height: 19px; text-align: right">
                    </td>
                    <td style="height: 19px; text-align: left">
                    </td>
                    <td style="height: 19px; text-align: right">
                    </td>
                    <td style="height: 19px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="text-align: center">
                        <table align="center">
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
        </td>
    </tr>
    </table>
