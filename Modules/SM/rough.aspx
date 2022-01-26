<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="rough.aspx.cs" Inherits="Modules_SM_rough" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table>
         <tr>
                        <td colspan="4" style="text-align: left; height: 20px;" class="profilehead" id="TD19">Interested Product</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">&nbsp;</td>
                        <td style="text-align: left">
                            &nbsp;<%--<asp:Label ID="Label50" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>--%></td>
                        <td style="text-align: right">

                        </td>
                        <td style="text-align: left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">Model No :
                            <%--<asp:Label ID="Label45" runat="server" Text="In Favour of :"></asp:Label>--%>

                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtModelNo" runat="server"></asp:TextBox>
                        </td>
                        <td style="text-align: right;">Item Name : <%--<asp:Label ID="Label51" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>--%>

                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtItemName" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Color :
                            <%--<asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label>--%>

                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtColor" runat="server"></asp:TextBox>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label55" runat="server" Text="Brand :"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtBrand" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">&nbsp;</td>
                        <td style="text-align: left;">
                            &nbsp;</td>
                        <td style="text-align: right">Quantity :
                            <%--<asp:Label ID="Label44" runat="server" Text="In Favour of :"></asp:Label>--%>

                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtItemQuantity" runat="server">
                            </asp:TextBox>*
                            <%--<asp:Label ID="Label49" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtItemQuantity"
                                ErrorMessage="Please Enter the Quantity" ValidationGroup="ip">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                    ID="ftxtQuantity" runat="server" FilterType="Numbers" TargetControlID="txtItemQuantity">
                                </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Specifications :
                            <%--<asp:Label ID="Label50" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>--%>

                        </td>
                        <td style="text-align: left" colspan="3">
                            <asp:TextBox ID="txtItemSpecifications" runat="server" TextMode="MultiLine" CssClass="multilinetext"
                                EnableTheming="False" Width="89%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Remarks :
                            <%--<asp:Label ID="Label45" runat="server" Text="In Favour of :"></asp:Label>--%>

                        </td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="multilinetext"
                                EnableTheming="False" Width="89%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="lblPriority" runat="server" Text="Priority :" Visible="False"></asp:Label>
                        </td>
                        <td style="height: 19px; text-align: left;">
                            <asp:DropDownList ID="ddlPriority" runat="server" Visible="False">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem>Low</asp:ListItem>
                                <asp:ListItem>Medium</asp:ListItem>
                                <asp:ListItem>High</asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="height: 19px; text-align: right">Room :
                            <%--<asp:Label ID="Label51" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>--%>

                        </td>
                        <td style="height: 19px; text-align: left">
                            <asp:TextBox ID="txtRoom" runat="server">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" id="tdDocCharges1" runat="server" visible="false">Doc. Charges :
                            <%--<asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label>--%>

                        </td>
                        <td style="text-align: left" id="tdDocCharges2" runat="server" visible="false">
                            <asp:TextBox ID="txtDocCharges" runat="server">
                            </asp:TextBox>*
                            <%--<asp:Label ID="lblApprovedBy" runat="server" Text="Approved By"></asp:Label>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtDocCharges"
                                ErrorMessage="Please Enter the Doc Charges" ValidationGroup="ip">*</asp:RequiredFieldValidator>
                            <cc1:FilteredTextBoxExtender ID="ftxtDOcCharges" runat="server" TargetControlID="txtDocCharges"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right" id="tdInfavourOfDoc1" runat="server" visible="false">In Favour of :
                            <%--<asp:Label ID="Label44" runat="server" Text="In Favour of :"></asp:Label>--%>

                        </td>
                        <td style="text-align: left" id="tdInfavourOfDoc2" runat="server" visible="false">
                            <asp:TextBox ID="txtInFavourofDoc" runat="server">
                            </asp:TextBox>*
                            <%--<asp:Label ID="Label49" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtInFavourofDoc"
                                ErrorMessage="Please Enter the Infavour of Doc" ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right" id="tdEMDCharges1" runat="server" visible="false">EMD Charges :
                            <%--<asp:Label ID="Label42" runat="server" Text="EMD Charges :"></asp:Label>--%>

                        </td>
                        <td style="text-align: left" id="tdEMDCharges2" runat="server" visible="false">
                            <asp:TextBox ID="txtEMDCharges" runat="server">
                            </asp:TextBox>*
                            <%--<asp:Label ID="Label50" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtEMDCharges"
                                ErrorMessage="Please Enter the EMD Charges" ValidationGroup="ip">*</asp:RequiredFieldValidator>
                            <cc1:FilteredTextBoxExtender ID="ftxtEMDCharges" runat="server" TargetControlID="txtEMDCharges"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right" id="tdInfavourOfEMD1" runat="server" visible="false">In Favour of :
                            <%--<asp:Label ID="Label45" runat="server" Text="In Favour of :"></asp:Label>--%>

                        </td>
                        <td style="text-align: left" id="tdInfavourOfEMD2" runat="server" visible="false">
                            <asp:TextBox ID="txtInFavourofEMD" runat="server">
                            </asp:TextBox>*
                            <%--<asp:Label ID="Label51" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtInFavourofEMD"
                                ErrorMessage="Please Enter the Infavour of EMD" ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">&nbsp;</td>
                        <td style="height: 19px; text-align: left;">
                            &nbsp;</td>
                        <td style="height: 19px; text-align: right">&nbsp;</td>
                        <td style="height: 19px; text-align: left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: right;">
                            <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" OnClick="btnAdd_Click" Text="Add"
                                ValidationGroup="ip" /></td>
                        <td style="text-align: left">
                            <asp:Button ID="btnRefreshItems" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" Text="Refresh" CausesValidation="False"
                                OnClick="btnRefreshItems_Click" /></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="height: 34px; text-align: right"></td>
                        <td style="height: 34px; text-align: left;"></td>
                        <td style="height: 34px; text-align: right"></td>
                        <td style="height: 34px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="4" id="TD12" runat="server">
                            <asp:GridView ID="gvInterestedProducts" runat="server" AutoGenerateColumns="False"
                                OnRowDataBound="gvInterestedProducts_RowDataBound" OnRowDeleting="gvInterestedProducts_RowDeleting"
                                OnRowEditing="gvInterestedProducts_RowEditing" Width="100%" OnRowCancelingEdit="gvInterestedProducts_RowCancelingEdit" OnRowUpdating="gvInterestedProducts_RowUpdating">
                                <Columns>
                                    <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                    <asp:BoundField DataField="ItemCode" ReadOnly="true" HeaderText="Item Code">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ModelNo" HeaderText="Model No" ReadOnly="true" >
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name" ReadOnly="true">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Brand" HeaderText="Brand" ReadOnly="true">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        
                                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Specifications" NullDisplayText="-" HeaderText="Specifications" ReadOnly="true"></asp:BoundField>
                                    <asp:BoundField DataField="Remarks" NullDisplayText="-" HeaderText="Remarks"></asp:BoundField>
                                    <asp:BoundField DataField="Priority" HeaderText="Priority" ReadOnly="true"></asp:BoundField>
                                    <asp:BoundField DataField="DocCharges" NullDisplayText="-" HeaderText="Doc Charges"></asp:BoundField>
                                    <asp:BoundField DataField="DocInFavourOf" NullDisplayText="-" HeaderText="Doc In Favour Of"></asp:BoundField>
                                    <asp:BoundField DataField="EMDCharges" NullDisplayText="-" HeaderText="EMD Charges"></asp:BoundField>
                                    <asp:BoundField DataField="EMDInFavourOf" NullDisplayText="-" HeaderText="EMD In Favour Of"></asp:BoundField>
                                    <asp:BoundField DataField="Room" HeaderText="Room"></asp:BoundField>
                                    <asp:BoundField DataField="Color"  HeaderText="Color "></asp:BoundField>
                                    <asp:BoundField DataField="ColorId" HeaderText="ColorId"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                                       
    </table>
</asp:Content>


 
