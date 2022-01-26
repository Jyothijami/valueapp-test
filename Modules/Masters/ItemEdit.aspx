<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/AdminMP1.master" AutoEventWireup="true" CodeFile="ItemEdit.aspx.cs" Inherits="MASTERS_ItemEdit" %>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .auto-style1 {
            height: 22px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
         <ContentTemplate>--%>

    <table style="width: 100%">
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="width: 100%" runat="server" id="tblDetails">
                    <tr>
                        <td class="profilehead" colspan="4"><b>Edit Item Master Details</b></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label1" runat="server" Text="Item Model No :"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtItemModelNo" runat="server"></asp:TextBox>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label2" runat="server" Text="Item Name :"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <%--  <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" DisplayMoney="Left"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtDate" UserDateFormat="MonthDayYear" >
                            </cc1:MaskedEditExtender>--%>
                            <asp:TextBox ID="txtItemName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label3" runat="server" Text="Item Category :"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlItemCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged">
                            </asp:DropDownList>
                            
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label4" runat="server" Text="Item Sub Category :"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlItemSubCategory" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label5" runat="server" Text="Brand :"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label7" runat="server" Text="Supplier ID :"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPrincipalName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label6" runat="server" Text="Color :"></asp:Label>
                        </td>
                        <td style="text-align: left" colspan="3">
                            <asp:CheckBoxList ID="chkItemColor" runat="server" RepeatColumns="5" RepeatDirection="Horizontal">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label8" runat="server" Text="Item Specification :"></asp:Label>
                        </td>
                        <td style="text-align: left" >
                            <asp:TextBox ID="txtItemSpecification" runat="server" TextMode="MultiLine" Width="424px"></asp:TextBox>
                        </td>
                        <td style="text-align: right"><asp:CheckBox ID="chkForDisc" OnCheckedChanged ="chkForDisc_CheckedChanged" AutoPostBack ="true"  runat ="server" /></td>
                        <td style="text-align: left; color :Red " >Check here for Discontinued Item 
                        <asp:Label ID="lblforChk" runat ="server" Visible ="false" ></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label9" runat="server" Text="Purchase Specification :"></asp:Label>
                        </td>
                        <td style="text-align: left" colspan="3">
                            <asp:TextBox ID="txtPurchaseSpecification" runat="server" TextMode="MultiLine" Width="423px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label10" runat="server" Text="Material Type :"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtMaterialtype" runat="server"></asp:TextBox>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label11" runat="server" Text="Uom :"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlUom" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label16" runat="server" Text="Tally Description :"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtitemseries" runat="server"></asp:TextBox>
                        </td>

                        <td align="right">

                            <asp:Label ID="Label13" runat="server" Text="HSN Code :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHSNCode" runat="server"></asp:TextBox>

                        </td>
                    </tr>

                    <tr>
                        <td align="right" class="auto-style3">
                            <asp:Label ID="Label21" runat="server" Text="Item GST TAX :"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtItemTAX" runat="server"></asp:TextBox>
                        </td>
                        <td align="right">

                            <asp:Label ID="Label22" runat="server" Text="Remarks :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRemarks" runat="server"></asp:TextBox>
                            <asp:Label ID="lblEmpIdHidden" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblItemCode" runat ="server" Visible ="false" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <table style="width: 100%;">
                                <tr>
                                    <td colspan="3" style="font-weight: 700; font-size: large">Attachments</td>
                                </tr>
                                <tr>
                                    <td colspan="3">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="font-weight: 700; text-align: left; width: 400px;">Item Images</td>
                                    
                                    <td style="font-weight: 700; text-align: left; width: 400px;">Item Drawings</td>
                                    <td style="font-weight: 700; text-align: left; width: 400px;">Spare Images</td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;">
                                        <asp:Label ID="Label17" runat="server" Text="Add Item Images :"></asp:Label>
                                        <asp:FileUpload ID="itemimages" runat="server" AllowMultiple="true" />

                                    </td>
                                    

                                    <td style="text-align: left;">
                                        <asp:Label ID="Label19" runat="server" Text="Add Item Drawings:"></asp:Label>
                                        <asp:FileUpload ID="ItemDrawings" runat="server" AllowMultiple="true" />

                                    </td>
                                    <td style="text-align: left;">
                                        <asp:Label ID="Label12" runat="server" Text="Add Item Spare Image:"></asp:Label>
                                        <asp:FileUpload ID="SpareImages" runat="server" AllowMultiple="true" />
                                    </td>
                                </tr>

                                <tr>
                                    <td style="text-align: left;">
                                        <asp:DataList ID="DataList1" runat="server" CellPadding="10"
                                            DataKeyField="Item_Image_Id" DataSourceID="itemimagessds1"
                                            RepeatColumns="2" OnDeleteCommand="DataList1_DeleteCommand"
                                            RepeatDirection="Horizontal" Width="100%">
                                            <HeaderStyle BackColor="#CCCCCC" />
                                            <HeaderTemplate>

                                                <table width="100%">

                                                    <tr align="left">

                                                        <th width="15%" style="text-align: center">ID

                                                        </th>

                                                        <th width="20%" style="text-align: center">Image

                                                        </th>
                                                        <th>&nbsp;</th>

                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table>
                                                    <tr align="left">

                                                        <td>

                                                            <%# DataBinder.Eval(Container.DataItem, "Item_Image_Id") %>

                                                        </td>

                                                        <td>
                                                            <asp:ImageButton ID="Image1" runat="server" ImageUrl='<%# Eval("Item_Image", "~/Content/ItemImage/{0}") %>' Width="100px" />

                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="delete"> Delete </asp:LinkButton>

                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>

                                        </asp:DataList>
                                    </td>
                                   

                                    <td style="text-align: left">
                                        <asp:DataList ID="DataList2" runat="server" CellPadding="10" DataKeyField="Item_Specification_Id" DataSourceID="itemimagessds2" RepeatColumns="2" OnDeleteCommand="DataList2_DeleteCommand" RepeatDirection="Horizontal" Width="100%">
                                            <HeaderStyle BackColor="#CCCCCC" />
                                            <HeaderTemplate>

                                                <table width="100%">

                                                    <tr align="left">

                                                        <th width="15%" style="text-align: center">ID

                                                        </th>

                                                        <th width="20%" style="text-align: center">Image

                                                        </th>
                                                        <th>&nbsp;</th>

                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table>
                                                    <tr align="left">

                                                        <td>

                                                            <%# DataBinder.Eval(Container.DataItem, "Item_Specification_Id") %>

                                                        </td>

                                                        <td>
                                                            <asp:ImageButton ID="Image1" runat="server" ImageUrl='<%# Eval("Item_Specification_Image", "~/Content/ItemDrawings/{0}") %>' Width="100px" />

                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="delete"> Delete </asp:LinkButton>

                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>

                                        </asp:DataList>
                                    </td>
                                     <td style="text-align: left">
                                        <asp:DataList ID="DataList3" runat="server" CellPadding="10" DataKeyField="Item_attachmentId" DataSourceID="itemimagessds3" RepeatColumns="2" OnDeleteCommand="DataList3_DeleteCommand" RepeatDirection="Horizontal" Width="100%">
                                            <HeaderStyle BackColor="#CCCCCC" />
                                            <HeaderTemplate>

                                                <table width="100%">

                                                    <tr align="left">

                                                        <th width="15%" style="text-align: center">ID

                                                        </th>

                                                        <th width="20%" style="text-align: center">Image

                                                        </th>
                                                        <th>&nbsp;</th>

                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table>
                                                    <tr align="left">

                                                        <td>

                                                            <%# DataBinder.Eval(Container.DataItem, "Item_attachmentId") %>

                                                        </td>

                                                        <td>
                                                            <asp:ImageButton ID="Image1" runat="server" ImageUrl='<%# Eval("Item_attachments", "~/Content/ItemAttachments/{0}") %>' Width="100px" />

                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="delete"> Delete </asp:LinkButton>

                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>

                                        </asp:DataList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan ="2" style="text-align:left ; font-weight: bold; " >
                            <asp:LinkButton ID="lnkSpareItem" runat="server" OnClick="lnkSpareItem_Click" Font-Underline="True">Add/View Spare Part Details</asp:LinkButton>
                        </td>
                    </tr>
                    </table>
                <asp:Panel runat="server" ID="pnlSP" Visible="false">
                    
                        <table style="width: 100%" >
                            <tr>
            <td   colspan="4 " class="profilehead">Add Item Spare Part Details</td>
                    </tr>
 <tr>
                        <td align ="right" class="auto-style3"  >
                            <asp:Label ID="Label24" runat="server" Text="Spare Part Code :"></asp:Label>
                           </td>
                        <td style="text-align: left" >
                            <asp:TextBox ID="txtsp" runat="server"></asp:TextBox>
                        </td>
                        <td align ="right">
                            
                            <asp:Label ID="Label25" runat="server" Text="Description :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtspDisc" TextMode ="MultiLine"  runat="server"></asp:TextBox>

                        </td>
                    </tr>
                            <tr>
                                            <td style="text-align: right; height: 19px;"></td>
                                            <td style="text-align: right; height: 19px;">
                                                <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    CssClass="loginbutton" EnableTheming="False" Text="Add" ValidationGroup="item"
                                                    OnClick="btnAdd_Click1" /></td>
                                            <td style="text-align: left; height: 19px;">
                                                <asp:Button ID="btnRefreshItems" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    CausesValidation="False" CssClass="loginbutton" EnableTheming="False" Text="Refresh"
                                                    OnClick="btnRefreshItems_Click" /></td>
                                            <td style="text-align: left; height: 19px;"></td>
                                        </tr>
                            <tr>
                                <td colspan ="3">
                                    <asp:GridView ID="gvSp" runat ="server" AutoGenerateColumns ="false" OnRowDeleting ="gvSp_RowDeleting"  >
                                        <Columns >
                                            <asp:BoundField DataField ="Item_Model_No" HeaderText ="article Code" />
                                            <asp:BoundField DataField ="Item_SpareModelNo" HeaderText ="Item Spare Code" />
                                            <asp:BoundField DataField ="Item_SpareDisc" HeaderText ="Discreption" />
                                            <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                  
                </asp:Panel>
                <table style ="width :100%">
                    <tr>
                        <td colspan="5" style="text-align: center">
                            <table align="center" style="width: 100%">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Update" OnClick="btnSave_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnExit" runat="server" Text="Exit" OnClick="btnExit_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="itemimagessds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
        SelectCommand="SELECT * FROM [YANTRA_ITEM_IMAGE] WHERE ([Item_Code] = @Item_Code)">
        <SelectParameters>
            <asp:QueryStringParameter Name="Item_Code" QueryStringField="Cid" Type="Int64" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="itemimagessds2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT * FROM YANTRA_ITEM_SPECIFICATION_IMAGE WHERE ([Item_Code] = @Item_Code)">

        <SelectParameters>
            <asp:QueryStringParameter Name="Item_Code" QueryStringField="Cid" Type="Int64" />
        </SelectParameters>
    </asp:SqlDataSource>
<asp:SqlDataSource ID="itemimagessds3" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT * FROM YANTRA_ITEM_ATTACHMENTS WHERE ([Item_Code] = @Item_Code)">

        <SelectParameters>
            <asp:QueryStringParameter Name="Item_Code" QueryStringField="Cid" Type="Int64" />
        </SelectParameters>
    </asp:SqlDataSource>

    <%--</ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
        </Triggers>
        </asp:UpdatePanel>--%>
</asp:Content>






