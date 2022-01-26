<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/SalesMP1.master" AutoEventWireup="true" CodeFile="Comparing_Quotation.aspx.cs" Inherits="Modules_SM_Comparing_Quotation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div id="head" style="width: 100%">
        <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
            <tr>
                <td style="text-align: left">Comparing Quotation</td>
                <td>
                    <asp:DropDownList ID="ddlNoOfRecords" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged">
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>75</asp:ListItem>
                        <asp:ListItem>100</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <div id="body" style="width: 100%">
        <table style="width: 100%">
            <tr>
                <td colspan="4" style="text-align: left" class="profilehead" id="TD16">Customer Details</td>
            </tr>
            <tr>
                <td style="height: 19px; text-align: right" id="TD5"></td>
                <td style="height: 19px; text-align: left;"></td>
                <td style="height: 19px; text-align: right"></td>
                <td style="height: 19px; text-align: left"></td>
            </tr>
            <tr>
                <td style="text-align: right; height: 22px;" id="TD18">Customer
                            :
                            <%--<asp:Label ID="Label8" runat="server" Text="Mobile" Width="74px"></asp:Label>--%>

                </td>
                <td style="text-align: left; height: 22px;">
                    <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                    </asp:DropDownList>*
                            <%--<asp:Label ID="Label22" runat="server" Text="Search By Brand :" Width="123px"></asp:Label>--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCustomer"
                        ErrorMessage="Please Select the Customer" InitialValue="0" ValidationGroup="main">*</asp:RequiredFieldValidator></td>
                <td style="text-align: right; height: 22px;">Region
                            :
                            <%--<asp:Label ID="Label36" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label>--%>

                </td>
                <td style="text-align: left; height: 22px;">
                    <asp:TextBox ID="txtRegion" runat="server" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="height: 22px; text-align: right">Industry Type
                            :
                            <%--<asp:Label ID="Label58" runat="server" Text="Search For ModelNo:" Width="140px"></asp:Label>--%>

                </td>
                <td style="height: 22px; text-align: left">
                    <asp:TextBox ID="txtIndustryType" runat="server" ReadOnly="True">
                    </asp:TextBox></td>
                <td style="height: 22px; text-align: right">Unit Name
                            :
                            <%--<asp:Label ID="Label11" runat="server" Text="Model No :"></asp:Label>--%>

                </td>
                <td style="height: 22px; text-align: left">
                    <asp:DropDownList ID="ddlUnitName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged">
                        <asp:ListItem Value="0">--</asp:ListItem>
                        <asp:ListItem Value="0">--Select Customer--</asp:ListItem>
                    </asp:DropDownList><asp:RequiredFieldValidator ID="rfvUnitName" runat="server" ControlToValidate="ddlUnitName"
                        ErrorMessage="Please Select the Unit Name" InitialValue="0" ValidationGroup="main">*</asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:Label ID="lblUnitAddress" runat="server" Text="Unit Address :"></asp:Label>

                </td>
                <td colspan="3" style="text-align: left">
                    <asp:TextBox ID="txtUnitAddress" runat="server" EnableTheming="False" TextMode="MultiLine"
                        Width="569px" Font-Names="Verdana" Font-Size="8pt" CssClass="multilinetext"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right">Contact Person
                            :
                            <%--<asp:Label ID="Label4" runat="server" Text="Model Name :" Width="100px"></asp:Label>--%>

                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtContactPerson" runat="server" ReadOnly="True"></asp:TextBox><asp:DropDownList
                        ID="ddlContactPerson" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlContactPerson_SelectedIndexChanged"
                        Visible="False">
                        <asp:ListItem Value="0">--</asp:ListItem>
                        <asp:ListItem Value="0">--Select Unit Name--</asp:ListItem>
                    </asp:DropDownList><asp:RequiredFieldValidator ID="rfvContactPerson" runat="server"
                        ControlToValidate="ddlContactPerson" ErrorMessage="Please Select the Contact Person"
                        InitialValue="0" ValidationGroup="main">*</asp:RequiredFieldValidator></td>
                <td style="text-align: right">Email

                            :

                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtEmail" runat="server" ReadOnly="false"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right" id="TD28">Phone No

                            :&nbsp;

                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtPhoneNo" runat="server" ReadOnly="True">
                    </asp:TextBox></td>
                <td style="text-align: right">Mobile

                            :

                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True">
                    </asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: left" class="profilehead" id="TD33">Compare Quotation Details :</td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">Description  :
                </td>
                <td colspan="3" style="text-align: left">
                    <asp:TextBox ID="txtDescription" runat="server" EnableTheming="False" TextMode="MultiLine"
                        Width="800px" Font-Names="Verdana" Font-Size="8pt">Dear Sir,
                        We thank you for your enquiry and are pleased to submit our best offer quote for supply of the following Sanitaryware. We hope you find our rates competitive and will oblige us with your valuable order.
                    </asp:TextBox></td>
            </tr>
        </table>
    </div>
    <br />
    <table>
        <tr>
            <td colspan="4" style="text-align: left" class="profilehead" id="TD343">Comparing Item Details :</td>
        </tr>
    </table>
    <br />
    <div id="grids" style="width: 100%">
        <table id="grid1" style="width: 33%; float: left;">
            <tr>
                <td>Brand :<asp:DropDownList ID="ddlBrand1" Width="100px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand1_SelectedIndexChanged"></asp:DropDownList>
                    Model No :<asp:DropDownList ID="ddlModel1" Width="100px" runat="server" OnSelectedIndexChanged="ddlModel1_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Desc  :<asp:TextBox ID="txtDesc1" Width="100px" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                    Color :<asp:DropDownList ID="ddlColor1" Width="100px" runat="server" AutoPostBack="True"></asp:DropDownList>

                </td>
            </tr>
            <tr>
                <td>Price  :<asp:TextBox ID="txtPrice1" Width="100px" runat="server"></asp:TextBox>
                    Spl Price :<asp:TextBox ID="txtSpl1" Width="80px" runat="server"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Button ID="btnCompare1" Width="70px" runat="server" Text="Add" OnClick="btnCompare1_Click" />
                </td>
            </tr>
            <tr>
                <td style="background-color: #fea820; font-weight: bold;">Brand -
                    <asp:Label ID="lblBrand1" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gv1" Width="100%" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField HeaderText="Model" DataField="Model" NullDisplayText="-">
                                <ControlStyle />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Description" DataField="Description" NullDisplayText="-">
                                <HeaderStyle HorizontalAlign="Center"  Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Color" DataField="Color" NullDisplayText="N-A">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="Qty" DataField="Qty">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Price" DataField="Price" NullDisplayText="0">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Spl Price" DataField="Spl_Price" NullDisplayText="0">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                            <asp:TemplateField HeaderText="Image">
                                <ItemTemplate>
                                    <asp:Image ID="Image1" Height="70px" Width="70px" runat="server" ImageUrl='<%# Eval("image1_path") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle Height="100px" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <table id="grid2" style="width: 33%; float: left;">
            <tr>
                <td>Brand :<asp:DropDownList ID="ddlBrand2" Width="100px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand2_SelectedIndexChanged"></asp:DropDownList>
                    Model No :<asp:DropDownList ID="ddlModel2" Width="100px" runat="server" OnSelectedIndexChanged="ddlModel2_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Desc  :<asp:TextBox ID="txtDesc2" Width="100px" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                    Color :<asp:DropDownList ID="ddlColor2" Width="100px" runat="server" AutoPostBack="True"></asp:DropDownList>

                </td>
            </tr>
            <tr>
                <td>Price  :<asp:TextBox ID="txtPrice2" Width="100px" runat="server"></asp:TextBox>
                    Spl Price :<asp:TextBox ID="txtSpl2" Width="80px" runat="server"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Button ID="btnCompare2" Width="70px" runat="server" Text="Add" OnClick="btnCompare2_Click" />
                </td>
            </tr>
            <tr>
                <td style="background-color: #a7d793; font-weight: bold;">Brand -
                    <asp:Label ID="lblBrand2" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gv2" Width="100%" runat="server" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField HeaderText="Model" DataField="Model" NullDisplayText="-">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Description" DataField="Description" NullDisplayText="-">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Color" DataField="Color" NullDisplayText="N-A">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="Qty" DataField="Qty">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Price" DataField="Price" NullDisplayText="0">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Spl Price" DataField="Spl_Price" NullDisplayText="0">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Image">
                                <ItemTemplate>
                                    <asp:Image ID="Image2" Height="70px" Width="70px"  runat="server" ImageUrl='<%# Eval("image2_path") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle Height="100px" />
                    </asp:GridView>

                </td>
            </tr>
        </table>
        <table id="grid3" style="width: 33%; float: left;">
            <tr>
                <td>Brand :<asp:DropDownList ID="ddlBrand3" Width="100px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand3_SelectedIndexChanged"></asp:DropDownList>
                    Model No :<asp:DropDownList ID="ddlModel3" Width="100px" runat="server" OnSelectedIndexChanged="ddlModel3_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Desc  :<asp:TextBox ID="txtDesc3" Width="100px" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                    Color :<asp:DropDownList ID="ddlColor3" Width="100px" runat="server" AutoPostBack="True"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Price  :<asp:TextBox ID="txtPrice3" Width="100px" runat="server"></asp:TextBox>
                    Spl Price :<asp:TextBox ID="txtSpl3" Width="80px" runat="server"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Button ID="btnCompare3" Width="70px" runat="server" Text="Add" OnClick="btnCompare3_Click" />
                </td>
            </tr>
            <tr>
                <td style="background-color: #6ab4f3; font-weight: bold;">Brand -
                    <asp:Label ID="lblBrand3" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gv3" Width="100%" AutoGenerateColumns="false" runat="server">
                        <Columns>
                            <asp:BoundField HeaderText="Model" DataField="Model" NullDisplayText="-">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Description" DataField="Description" NullDisplayText="-">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Color" DataField="Color" NullDisplayText="N-A">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="Qty" DataField="Qty">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Price" DataField="Price" NullDisplayText="0">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Spl Price" DataField="Spl_Price" NullDisplayText="0">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Image">
                                <ItemTemplate>
                                    <asp:Image ID="Image3" Height="70px" Width="70px"  runat="server" ImageUrl='<%# Eval("image3_path") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle Height="100px" />
                    </asp:GridView>

                </td>
            </tr>
        </table>
    </div>
    <asp:Label ID="lblImg1" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblImg2" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblImg3" runat="server" Visible="false"></asp:Label>
</asp:Content>


 
