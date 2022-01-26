<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/InventoryMP1.master" AutoEventWireup="true" CodeFile="testerror.aspx.cs" Inherits="Modules_Inventory_testerror" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
     <asp:UpdatePanel runat="server">
                                <ContentTemplate >
    <asp:GridView ID="gvDonepo" runat="server" AutoGenerateColumns="False" Width="100%"  ShowFooter="True">
                                <FooterStyle BackColor="#1AA8BE" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnDelete" CausesValidation="false" runat="server"  OnClick="lbtnDelete_Click1">Delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Slno" HeaderText="Slno"></asp:BoundField>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                    <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                    <%--<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>--%>
                                    <asp:TemplateField HeaderText="Quantity" ControlStyle-Width="50px">
                                        <ItemTemplate>
                                                    <asp:TextBox ID="txtQuantity" runat="server" onkeyup="DueAmt()" Text='<%# Bind("Quantity") %>' AutoPostBack="true" 
                                                        ></asp:TextBox>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Currency" HeaderText="Currency"></asp:BoundField>
                                    <%--<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>--%>
                                    <asp:TemplateField HeaderText="Rate" ControlStyle-Width="55px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtMRP" runat="server" Text='<%# Bind("Rate") %>' AutoPostBack="true"  ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField HeaderText="UnitPrice"></asp:BoundField>--%>
                                    <asp:TemplateField HeaderText="Unit Price" ControlStyle-Width="55px" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnitPrice"  runat="server"  ></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <%--<asp:BoundField HeaderText="UnitPrice"></asp:BoundField>--%>
                                    <asp:TemplateField HeaderText="Spl Price" ControlStyle-Width="55" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSplPrice" Text='<%#Bind("Price") %>' runat="server"  ></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="Price" HeaderText="Spl Price"></asp:BoundField>--%>
                                    <asp:BoundField DataField="Specifications" HeaderText="Specifications"></asp:BoundField>
                                    <asp:BoundField DataFormatString="{0:dd/MM/YYYY}" DataField="DeliveryDate" HeaderText="Delivery Date">
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Room" HeaderText="Brand"></asp:BoundField>
                                    <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                                    <asp:BoundField DataField="ColorId" HeaderText="Color Id"></asp:BoundField>
                                    <asp:BoundField DataField="Sales" HeaderText="Sales"></asp:BoundField>
                                    <%--<asp:TemplateField HeaderText="Discount" ControlStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvDiscount" runat="server" AutoPostBack="true" OnTextChanged="txtDiscount_TextChanged" ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Balance Qty" ControlStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtBalanceQty" Text='<%#Bind("BalanceQty") %>' runat="server" ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField ="SODetId" HeaderText ="detId" />
                                     <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkhdr" runat="server" OnClick="selectAll(this)" />
                                            <%--<asp:CheckBox ID="chkhdr" runat="server" AutoPostBack="True" OnClick="selectAll(this)" OnCheckedChanged="chkhdr_CheckedChanged" />--%>
                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkItemSelect" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
 </ContentTemplate> 
         </asp:UpdatePanel> 

</asp:Content>

