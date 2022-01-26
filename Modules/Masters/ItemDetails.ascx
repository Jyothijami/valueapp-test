<%@ Control Language="C#"  AutoEventWireup="true" CodeFile="ItemDetails.ascx.cs" Inherits="Modules_Masters_ItemDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

    <table style="width: 127px">
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
            <td class="searchhead" colspan="2 " style="height:30px;font-size:16px"><b>Item Details</b></td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td rowspan="3">
                                        <asp:Label id="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By" Width="86px"></asp:Label></td>
                                    <td rowspan="3">
                                        <asp:DropDownList id="ddlSearchBy" runat="server" CssClass="textbox">
                                            <asp:ListItem>--</asp:ListItem>
                                            <asp:ListItem Value="ITEM_DET_ID">sno</asp:ListItem>
                                            <asp:ListItem Value="ITEM_CODE">Item Code</asp:ListItem>
                                            <asp:ListItem Value="ITEM_DET_MANUFACTURER">Manufacturing</asp:ListItem>
                                            <asp:ListItem Value="ITEM_DET_MFG_DATE">Manufacturing Date</asp:ListItem>
                                            <asp:ListItem Value="ITEM_DET_EXP_DATE">Expire Date</asp:ListItem>
                                            <asp:ListItem Value="ITEM_DET_BATCH_NO">Batch No</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td rowspan="3">
                                    </td>
                                    <td rowspan="3">
                                        <asp:TextBox id="txtSearchText" runat="server" CssClass="textbox">
                                        </asp:TextBox><asp:Image id="imgCurrentDayTasksToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image></td>
                                    <td rowspan="3">
                                        <asp:Button id="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" onclick="btnSearchGo_Click" Text="Go" /></td>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                </tr>
                            </table>
                            <asp:Label id="lblSearchItemHidden" runat="server" Visible="False"></asp:Label><asp:Label
                                id="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label><asp:Label id="lblSearchValueFromHidden"
                                    runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: left; height: 21px;">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 21px; text-align: center">
                &nbsp;
                <asp:GridView id="gvItemDetails" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" DataSourceID="sdsItemDetailsMaster" OnRowDataBound="gvItemDetails_RowDataBound">
                    <columns>
<asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code Hidden">
<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
<asp:BoundField ReadOnly="True" DataField="ITEM_DET_ID" HeaderText="Item NO"></asp:BoundField>
<asp:TemplateField HeaderText="Item Code"><EditItemTemplate>
<asp:TextBox runat="server" Text='<%# Bind("Item_Code") %>' id="TextBox1"></asp:TextBox>
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnItemCode" runat="server" Text="<%# Bind('Item_Code') %>" CausesValidation="False" __designer:wfdid="w4" OnClick="lbtnItemCode_Click"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="ITEM_DET_MANUFACTURER" HeaderText="ItemManufacture"></asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:MM/dd/yyyy}" DataField="ITEM_DET_MFG_DATE" HeaderText="Manufacturing Date">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:MM/dd/yyyy}" DataField="ITEM_DET_EXP_DATE" HeaderText="Expire Date"></asp:BoundField>
<asp:BoundField DataField="ITEM_DET_BATCH_NO" HeaderText="Item Batch No"></asp:BoundField>
</columns>
                    <selectedrowstyle backcolor="LightSteelBlue" />
                </asp:GridView><asp:SqlDataSource id="sdsItemDetailsMaster" runat="server" ConnectionString="<%$ ConnectionStrings:DbCon %>"
                     SelectCommand="SP_MASTER_ITEMDETAILS_SEARCH_SELECT" SelectCommandType="StoredProcedure"><selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
</selectparameters></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 51px; text-align: center">
                <table id="Table1">
                    <tr>
                        <td>
                            <asp:Button id="btnNew" runat="server" Text="New" CausesValidation="False" OnClick="btnNew_Click" /></td>
                        <td>
                            <asp:Button id="btnEdit" runat="server" Text="Edit" CausesValidation="False" OnClick="btnEdit_Click" /></td>
                        <td>
                            <asp:Button id="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" CausesValidation="False" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
             <td colspan="2" style="text-align: center; height: 203px;">
                <table style="width: 783px" border="0" cellpadding="0" cellspacing="0" id="tblItemDetails" runat="server" visible="false">
                    <tr>
            <td colspan="5" style="text-align: left; height: 20px;" class="profilehead">
                General Details</td>
                    </tr>
                    <tr>
                        <td style="width: 307px; text-align: right; height: 22px;">
                            &nbsp;<asp:Label id="lblItemCode" runat="server" Text="Item Code"></asp:Label>
                            </td>
                        <td style="text-align: left; height: 22px;">
                            <asp:DropDownList id="ddlItemCode" runat="server">
                                <asp:ListItem>--</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>
                                </asp:ListItem>
                            </asp:DropDownList></td>
            <td style="text-align: left; width: 89px; height: 22px;">
             </td>
                        <td style="width: 131px; text-align: right; height: 22px;">
                            <asp:Label id="lblExpireDate" runat="server" Text="Expire Date"></asp:Label></td>
                        <td style="width: 321px; text-align: left; height: 22px;">
                            <asp:TextBox id="txtExpireDate" runat="server" type="date">
                            </asp:TextBox></td>
                        <td style="text-align: left; width: 4284px; height: 22px;">
                           
                        </td>
                    </tr>
                    <tr>
            <td style="text-align: right; width: 307px;">
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:Label id="lblManufacturur" runat="server"
                    Text="Manufacturur"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtManufacturing" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: left; width: 89px;">
                            </td>
                        <td style="width: 131px; text-align: right">
                            &nbsp;<asp:Label id="Label2" runat="server" Text="Batch No"></asp:Label></td>
                        <td style="width: 321px; text-align: left">
                            <asp:TextBox id="txtBatchNo" runat="server">
                            </asp:TextBox></td>
            <td style="text-align: left; width: 4284px;">
                </td>
                    </tr>
                    <tr>
            <td style="text-align: right; width: 307px; height: 18px;">
                <asp:Label id="Label1" runat="server" Text="Manufacturing Date"></asp:Label></td>
                        <td style="height: 18px; text-align: left">
                            <asp:TextBox id="txtManufacturingDate" runat="server" type="date" Width="109px"></asp:TextBox>
                           
                            </td>
                        <td style="text-align: left; width: 89px; height: 18px;">
                            &nbsp;</td>
                        <td style="width: 131px; height: 18px; text-align: right">
                            &nbsp;
                        </td>
                        <td style="width: 321px; text-align: left; height: 18px;">
                            &nbsp;</td>
            <td style="text-align: left; width: 4284px; height: 18px;">
                </td>
                    </tr>
                    <tr>
                        <td style="width: 307px; text-align: right;">
                           
                        <td style="text-align: left">
                            </td>
                        <td style="width: 89px; text-align: left;">
                            </td>
                        <td style="width: 131px; text-align: left">
                        </td>
                        <td style="width: 321px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 307px; height: 1px">
                          
                        </td>
                        <td style="height: 1px; text-align: left">
                        </td>
                        <td style="width: 89px; height: 1px; text-align: left">
                            </td>
                        <td style="width: 131px; height: 1px; text-align: left">
                        </td>
                        <td style="width: 321px; height: 1px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 307px">
                            
                        </td>
                        <td style="text-align: left">
                        </td>
                        <td style="width: 89px; text-align: left">
                            </td>
                        <td style="width: 131px; text-align: left">
                        </td>
                        <td style="width: 321px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 307px; height: 24px;">
                            
                        </td>
                        <td style="height: 24px; text-align: left">
                        </td>
                        <td style="width: 89px; height: 24px; text-align: left;">
                        </td>
                        <td style="width: 131px; height: 24px; text-align: left">
                        </td>
                        <td style="width: 321px; height: 24px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 307px">
                        </td>
                        <td>
                        </td>
                        <td style="width: 89px">
                        </td>
                        <td style="width: 131px">
                        </td>
                        <td style="width: 321px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" rowspan="2" style="text-align: center">
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            <asp:Button id="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CausesValidation="False"  />
                            <asp:Button id="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" CausesValidation="False" />
                            <asp:Button id="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" />
                            &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                        </td>
                        <td style="width: 4284px">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; width: 4284px;">
                            </td>
                    </tr>
                    <tr>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 615px;" colspan="3" rowspan="3">
                </td>
        </tr>
        <tr>
        </tr>
        <tr>
        </tr>
    </table>

