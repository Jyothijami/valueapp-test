<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdvertisingMagzines.ascx.cs" Inherits="Modules_SM_AdvertisingMagzines" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="../../App_Themes/Master/Master.css" rel="stylesheet" type="text/css" /> 
 <table style="width: 783px" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="searchhead" colspan="2" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">
                            Magzines</td>
                        <td>
                        </td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td rowspan="3">
                                        <asp:Label id="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td rowspan="3">
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="AM_ID">S.No</asp:ListItem>
                                            <asp:ListItem Value="AM_NAME">Magzine  Name</asp:ListItem>
                                            <asp:ListItem Value="AM_DESC">Description</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td rowspan="3">
                                        </td>
                                    <td rowspan="3">
                                        <asp:TextBox id="txtSearchText" runat="server" CssClass="textbox"></asp:TextBox>&nbsp;
                                    </td>
                                    <td rowspan="3">
                                        <asp:Button id="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnSearchGo_Click" /></td>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                </tr>
                            </table>
                            <asp:Label id="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" style="height: 19px; text-align: center">
                &nbsp;<asp:GridView ID="gvMagzine" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="sdsMagzineDetails" OnRowDataBound="gvMagzine_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="AM_NAME" HeaderText="MagzinesNameHidden" />
                        <asp:BoundField DataField="AM_ID" HeaderText="S.No">
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Magzines Name">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("AM_NAME") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnMagzineName" runat="server" CausesValidation="False" OnClick="lbtnMagzineName_Click"
                                    Text="<%# Bind('AM_NAME') %>"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="AM_DESC" HeaderText="Description" NullDisplayText="-">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        No Record Found
                    </EmptyDataTemplate>
                    <SelectedRowStyle BackColor="LightSteelBlue" />
                </asp:GridView>
                <asp:SqlDataSource ID="sdsMagzineDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SM_ADVERTISING_MAGZINE_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName"
                            PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue"
                            PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <table id="Table1">
                    <tr>
                        <td>
                            <asp:Button id="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CausesValidation="False" /></td>
                        <td>
                            <asp:Button id="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CausesValidation="False" /></td>
                        <td>
                            <asp:Button id="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" CausesValidation="False" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                &nbsp;<table id="tblMagzine" runat="server" border="0" cellpadding="0" cellspacing="0"
                    style="text-align: center" visible="false">
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                General Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: left">
                        </td>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label ID="lblMagzine" runat="server" Text="Magzine"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                            <asp:TextBox ID="txtMagzine" runat="server"></asp:TextBox><br />
                            <cc1:FilteredTextBoxExtender ID="ftxteMagzine" runat="server" FilterMode="InvalidChars"
                                InvalidChars="0123456789~!@#$%^=&*()_+|}{&quot;:';/.,?><" TargetControlID="txtMagzine">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="height: 24px; text-align: right">
                        </td>
                        <td style="height: 24px; text-align: left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 22px; text-align: right">
                            <asp:Label ID="lblDescription" runat="server" Text="Description" Width="224px"></asp:Label></td>
                        <td style="height: 22px; text-align: left">
                            <asp:TextBox ID="txtDescription" runat="server" EnableTheming="False" TextMode="MultiLine"></asp:TextBox></td>
                        <td style="height: 22px; text-align: right">
                        </td>
                        <td style="height: 22px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" rowspan="8" style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 68px">
                            <br />
                            <table id="tblButtons" style="text-align: center">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" /></td>
                                    <td style="width: 69px">
                                        <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" Text="Refresh" /></td>
                                    <td style="width: 4px">
                                        <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" /></td>
                                    <td style="width: 3px">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="height: 21px">
            </td>
            <td style="height: 21px;">
            </td>
        </tr>
    </table>