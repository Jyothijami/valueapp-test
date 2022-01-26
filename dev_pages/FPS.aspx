<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.master" AutoEventWireup="true" CodeFile="FPS.aspx.cs" Inherits="dev_pages_FPS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
         <asp:UpdatePanel ID="UpdatePanel34" runat="server">
        <ContentTemplate>
                <div class="page-header">
                <div class="page-title">
                    <h3>FPS </h3>
                </div>
            </div>
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                    <h6 class="panel-title">Sales Order Details</h6>
                </div>
                        <div class="panel-body">
                             <div class="form-group">
                                 <label class="col-sm-2 control-label text-right">Quotation No</label>
                                 <div class="col-sm-4">
                                     <asp:DropDownList ID="ddlQuotationNo" runat="server" OnSelectedIndexChanged="ddlQuotationNo_SelectedIndexChanged"
                                     AutoPostBack="True" Enabled="False"></asp:DropDownList>
                                 </div>
                                 <label class="col-sm-2 control-label text-right">Quotation Date</label>
                                 <div class="col-sm-4">
                                    <asp:TextBox ID="txtQuotationDate" runat="server" CssClass="datetext" EnableTheming="False" ReadOnly="True"></asp:TextBox>
                                 </div>
                             </div>

                            <div class="form-group">
                                 <label class="col-sm-2 control-label text-right">Customer</label>
                                 <div class="col-sm-4">
                                     <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="True" Enabled="False"
                                     OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged"></asp:DropDownList>
                                 </div>
                                 <label class="col-sm-2 control-label text-right">Region</label>
                                 <div class="col-sm-4">
                                    <asp:TextBox ID="txtRegion" runat="server" ReadOnly="True"> </asp:TextBox>
                                 </div>
                             </div>

                            <div class="form-group">
                                 <label class="col-sm-2 control-label text-right">Unit Name</label>
                                 <div class="col-sm-4">
                                     <asp:DropDownList ID="ddlUnitName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged">
                                    <asp:ListItem Value="0">--</asp:ListItem>
                                    <asp:ListItem Value="0">--Select Customer--</asp:ListItem></asp:DropDownList>
                                 </div>
                                 <label class="col-sm-2 control-label text-right">Address</label>
                                 <div class="col-sm-4">
                                    <asp:TextBox ID="txtUnitAddress" runat="server" CssClass="multilinetext" EnableTheming="False"
                                    Font-Names="Verdana" Font-Size="8pt" TextMode="MultiLine" Width="620px"></asp:TextBox>
                                 </div>
                             </div>
                            <div class="form-group">
                                 <label class="col-sm-2 control-label text-right">Contact Person</label>
                                 <div class="col-sm-4">
                                     <asp:TextBox ID="txtContactPerson" runat="server" ReadOnly="True"> </asp:TextBox><asp:DropDownList ID="ddlContactPerson" runat="server" AutoPostBack="True"
                                     Enabled="False" OnSelectedIndexChanged="ddlContactPerson_SelectedIndexChanged" Visible="False">
                                    <asp:ListItem Value="0">--</asp:ListItem>
                                    <asp:ListItem Value="0">--Select Unit Name--</asp:ListItem></asp:DropDownList>
                                 </div>
                                 <label class="col-sm-2 control-label text-right">Email</label>
                                 <div class="col-sm-4">
                                    <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True"> </asp:TextBox>
                                 </div>
                             </div>
                            <div class="form-group">
                                 <label class="col-sm-2 control-label text-right">Contact Person</label>
                                 <div class="col-sm-4">
                                      <asp:TextBox ID="txtPhoneNo" runat="server" ReadOnly="True"> </asp:TextBox>
                                      <asp:Label ID="lblQuotRespId" runat="server" Visible="False"></asp:Label>
                                 </div>
                                 <label class="col-sm-2 control-label text-right">Mobile</label>
                                 <div class="col-sm-4">
                                    <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True"> </asp:TextBox>
                                 </div>
                             </div>

                            <div class="form-group">
                                 <label class="col-sm-2 control-label text-right">Executive Name</label>
                                 <div class="col-sm-4">
                                      <asp:TextBox ID="txtExecutiveName" runat="server"></asp:TextBox>
                                 </div>
                                 <label class="col-sm-2 control-label text-right">Phone No</label>
                                 <div class="col-sm-4">
                                    <asp:TextBox ID="txtExePhoneNo" runat="server"></asp:TextBox>
                                 </div>
                             </div>

                            <div class="form-group">
                                 <label class="col-sm-2 control-label text-right">Purchase Order No</label>
                                 <div class="col-sm-4">
                                      <asp:TextBox ID="txtSalesOrderNo" runat="server" ReadOnly="True"></asp:TextBox>
                                 </div>
                                 <label class="col-sm-2 control-label text-right">Purchase Order Date</label>
                                 <div class="col-sm-4">
                                      <asp:TextBox ID="txtSalesOrderDate" runat="server" CssClass="datetext" type="datepic"></asp:TextBox>
                                 </div>
                             </div>

                            <div class="form-group">
                                 <label class="col-sm-2 control-label text-right">Customer PO No.</label>
                                 <div class="col-sm-4">
                                      <asp:TextBox ID="txtCustPONo" runat="server"></asp:TextBox>
                                 </div>
                                 <label class="col-sm-2 control-label text-right">Customer PO Date</label>
                                 <div class="col-sm-4">
                                      <asp:TextBox ID="txtCustPODated" runat="server" CssClass="datetext" EnableTheming="False" type="datepic"></asp:TextBox>
                                 </div>
                             </div>

                            <div class ="form-group">
                                <asp:GridView ID="gvDonepo" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDataBound="gvDonepo_RowDataBound" ShowFooter="True">
                                <FooterStyle BackColor="#1AA8BE" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnDelete" CausesValidation="false" runat="server" __designer:wfdid="w5" >Delete</asp:LinkButton>
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
                                                        OnTextChanged="txtQuantity_TextChanged"></asp:TextBox>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Currency" HeaderText="Currency"></asp:BoundField>
                                    <%--<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>--%>
                                    <asp:TemplateField HeaderText="Rate" ControlStyle-Width="55px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtMRP" runat="server" Text='<%# Bind("Rate") %>' AutoPostBack="true" OnTextChanged ="txtMRP_TextChanged" ></asp:TextBox>
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

                                <asp:Label ID="lblTotalAmt1" runat="server" Text="0" Visible="False"></asp:Label>
                            <asp:Label ID="lblTotalAmt2" runat="server" Text="0" Visible="False"></asp:Label>
                            <asp:Label ID="lblTotalamount" runat="server" Text="0" Visible="False"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate> 
             </asp:UpdatePanel> 
    </asp:Content>

