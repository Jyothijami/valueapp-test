<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/AdminMP1.master" AutoEventWireup="true" CodeFile="ItemMaster.aspx.cs" Inherits="MASTERS_ItemMaster" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table id="TblMain" style="width: 100%">
        <tr>
            <td colspan="2">
                <table style="width: 100%" runat="server"  id="tblDetails">
                    <tr>
            <td   colspan="4 " class="profilehead">Add Item Master Details</td>
                    </tr>
                    <tr>
                        <td align="right" class="auto-style3"  >
                            <asp:Label ID="Label1" runat="server" Text="Item Model No :"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtItemModelNo" runat="server" AutoPostBack="True" OnTextChanged="txtItemModelNo_TextChanged"></asp:TextBox>
                            <asp:Label ID="Label12" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:Label ID="lblChkArtical" runat ="server" Visible ="false" Text ="This Model No is also belongs to the following Articals, Could you please take reference!!" ForeColor ="Red"  ></asp:Label>
                            <asp:CheckBoxList ID="chkArticlNo" runat="server" AutoPostBack ="true"  RepeatColumns="5" Visible ="false" ForeColor ="Blue" OnSelectedIndexChanged="chkArticlNo_SelectedIndexChanged"   RepeatDirection="Horizontal" Height="23px">
                            </asp:CheckBoxList>
                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="Please Enter Item Model No." ValidationGroup="a" ControlToValidate="txtItemModelNo">*</asp:RequiredFieldValidator>
                           
                        </td>
                        <td align ="right">
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
                        <td align ="right" class="auto-style3"  >
                            <asp:Label ID="Label3" runat="server" Text="Item Category :"></asp:Label>
                        </td>
                        <td align ="left">
                            <asp:DropDownList ID="ddlItemCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label ID="Label14" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfv2" runat="server" ErrorMessage="Please Select Item Category" InitialValue="0" ValidationGroup="a" ControlToValidate="ddlItemCategory">*</asp:RequiredFieldValidator>
                       
                        </td>
                        <td align ="right">
                            <asp:Label ID="Label4" runat="server" Text="Item Sub Category :"></asp:Label>
                        </td>
                        <td align ="left">
                            <asp:DropDownList ID="ddlItemSubCategory" runat="server">
                            </asp:DropDownList>
                            <asp:Label ID="Label15" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator ID="rfv3" runat="server" ErrorMessage="Please Select Item Sub Category" InitialValue="0" ValidationGroup="a" ControlToValidate="ddlItemSubCategory">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align ="right" class="auto-style3"  >
                            <asp:Label ID="Label5" runat="server" Text="Brand :"></asp:Label>
                        </td>
                        <td align ="left">
                            <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
                            </asp:DropDownList><asp:Label ID="Label20" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator ID="rfv4" runat="server" ErrorMessage="Please Select Item Brand" InitialValue="0" ValidationGroup="a" ControlToValidate="ddlBrand">*</asp:RequiredFieldValidator>
                        </td>
                        <td align ="right">
                            <asp:Label ID="Label7" runat="server" Text="Principal Name :"></asp:Label>
                        </td>
                        <td align ="left">
                            <asp:TextBox ID="txtPrincipalName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align ="right" class="auto-style3"  >
                            <asp:Label ID="Label6" runat="server" Text="Color :"></asp:Label>
                        </td>
                        <td align ="left" colspan="3">
                            <asp:CheckBoxList ID="chkItemColor" runat="server" RepeatColumns="5" RepeatDirection="Horizontal">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td align ="right" class="auto-style3"  >
                            <asp:Label ID="Label8" runat="server" Text="Item Specification :"></asp:Label>
                        </td>
                        <td align ="left" colspan="3">
                            <asp:TextBox ID="txtItemSpecification" runat="server" TextMode="MultiLine" Width="424px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align ="right" class="auto-style3"  >
                            <asp:Label ID="Label9" runat="server" Text="Purchase Specification :"></asp:Label>
                        </td>
                        <td align ="left" colspan="3">
                            <asp:TextBox ID="txtPurchaseSpecification" runat="server" TextMode="MultiLine" Width="423px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align ="right" class="auto-style2">
                            <asp:Label ID="Label10" runat="server" Text="Material Type :"></asp:Label>
                           </td>
                        <td align ="left" style="height: 23px">
                            <asp:TextBox ID="txtMaterialtype" runat="server"></asp:TextBox>
                            </td>
                        <td align="right">
                            <asp:Label ID="Label11" runat="server" Text="Uom :"></asp:Label>
                        </td>
                        <td style="height: 23px; text-align: left;">
                            <asp:DropDownList ID="ddlUom" runat="server">
                            </asp:DropDownList>
                            <asp:Label ID="Label23" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator ID="rfv5" runat="server" ErrorMessage="Please Select Item UOM" InitialValue="0" ValidationGroup="a" ControlToValidate="ddlUom">*</asp:RequiredFieldValidator>
                            </td>
                    </tr>
                    <tr>
                        <td align ="right" class="auto-style3"  >
                            <asp:Label ID="Label16" runat="server" Text="Item Series :"></asp:Label>
                           </td>
                        <td style="text-align: left" >
                            <asp:TextBox ID="txtitemseries" runat="server"></asp:TextBox>
                        </td>
                        <td align ="right">
                            
                            <asp:Label ID="Label13" runat="server" Text="HSN Code :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHSNCode" runat="server"></asp:TextBox>

                        </td>
                    </tr>
                    
                     <tr>
                        <td align ="right" class="auto-style3"  >
                            <asp:Label ID="Label21" runat="server" Text="Item GST TAX :"></asp:Label>
                           </td>
                        <td style="text-align: left" >
                            <asp:TextBox ID="txtItemTAX" runat="server"></asp:TextBox>
                        </td>
                        <td align ="right">
                            
                            <asp:Label ID="Label22" runat="server" Text="Remarks :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRemarks" runat="server"></asp:TextBox>

                        </td>
                    </tr>
                    
                    <tr>
                        <td align ="right" class="auto-style3"  >
                            <asp:Label ID="Label18" runat="server" Text="Add Item Images :"></asp:Label>
                           </td>
                        <td  style="text-align: left" >
                            <asp:FileUpload ID="itemimages" runat="server" AllowMultiple="true" />
                        <asp:RequiredFieldValidator ID="rfv6" runat="server" ErrorMessage="Please Select Item Image" ValidationGroup="a" ControlToValidate="itemimages" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        <td align ="right" class="auto-style3"  >
                            <asp:Label ID="Label19" runat="server" Text="Add Item Drawings:"></asp:Label>
                           </td>
                        <td  style="text-align: left" >
                            <asp:FileUpload ID="ItemDrawings" runat="server" AllowMultiple="true" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Item Drawing" ValidationGroup="a" ControlToValidate="ItemDrawings" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        
                    </tr>
                    <tr>
                         
                        <td colspan ="2" style="text-align:center; font-weight: bold; " >
                            <asp:LinkButton ID="lnkSpareItem" runat="server" OnClick="lnkSpareItem_Click" Font-Underline="True">Add Spare Part Details</asp:LinkButton>
                        </td>
                        <td align ="right" class="auto-style3"  >
                            <asp:Label ID="Label17" runat="server"  Text="Add Item Spare Images :"></asp:Label>
                           </td>
                        <td  style="text-align: left" >
                            <asp:FileUpload ID="Uploadattach"  runat="server" AllowMultiple="true" />
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
                                    <asp:GridView ID="gvSp" runat ="server" AutoGenerateColumns ="false" OnRowCommand ="gvSp_RowCommand" OnRowDeleting ="gvSp_RowDeleting"  >
                                        <Columns >
                                            <asp:BoundField DataField ="Item_Model_No" HeaderText ="Model No." />
                                            <asp:BoundField DataField ="Item_SpareModelNo" HeaderText ="Item Spare Code" />
                                            <asp:BoundField DataField ="Item_SpareDisc" HeaderText ="Description" />
                                            <asp:TemplateField HeaderText="Image">
                                <ItemTemplate>
                                    
                                    <asp:FileUpload ID="fileupload1" runat ="server" Width ="100px"/>
                                    <asp:ImageButton ID="ibtmImage" runat="server" ImageUrl="~/Images/tick.png" CommandName ="Save" Width="18px" CommandArgument='<%# Eval("Item_SpareModelNo").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                                            <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                  <asp:Label ID="lblSpId" runat ="server" Visible ="false" ></asp:Label>
                </asp:Panel>

                <table style="width: 100%">
                      
                    
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="a" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnExit" runat="server" Text="Exit" OnClick="btnExit_Click" />
                                        <asp:Label ID="lblEmpIdHidden" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID ="lblSpareDisc" runat ="server" Visible ="false" ></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server"  ShowMessageBox="true" ShowSummary="false" ShowValidationErrors="true"/>
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="a"  ShowMessageBox="true" ShowSummary="false" ShowValidationErrors="true"/>

            </td>
            <td></td>
        </tr>
        </table>
    <script src="../site_resources/Scripts/jquery-1.4.min.js"></script>
    <script src="../site_resources/Scripts/jquery.MultiFile.pack.js"></script>
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style2 {
            height: 23px;
            width: 20%;
        }
        .auto-style3 {
            width: 20%;
        }
    </style>
</asp:Content>



 
