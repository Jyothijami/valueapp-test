<%@ Page Title="|| Value Appp : Services : Customer Information ||" Language="C#" MasterPageFile="~/MPs/ServicesMP1.master" AutoEventWireup="true"
    CodeFile="Service_Customers.aspx.cs" Inherits="Modules_Services_Service_Customers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div id="divCustInfo">
        <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
            <tr>
                <td style="text-align: left">Service Customer Details</td>
                <td style="text-align: right">
                    <asp:DropDownList ID="ddlNoOfRecords" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged">
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>75</asp:ListItem>
                        <asp:ListItem>100</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnNew" runat="server" Text="NEW" OnClick="btnNew_Click" />
                </td>
            </tr>
            </table>
        <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
            <tr>
                        <td style="text-align: left" >
                                    Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
                                </td>
                 <td style="text-align: right" colspan="2">&nbsp;&nbsp;
                            <asp:Label ID="lblSearchBy" runat="server" CssClass="label" EnableTheming="False"
                                Font-Bold="True" Text="Search By" Height="17px"></asp:Label><asp:DropDownList ID="ddlSearchBy"
                                    runat="server" CssClass="textbox" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                    <asp:ListItem Value="0">--</asp:ListItem>
                                    <asp:ListItem Value="Cust_Name">Customer Name</asp:ListItem>
                                    <asp:ListItem Value="Cust_Address">Customer Address</asp:ListItem>
                                    <asp:ListItem Value="Cust_Company_Name">Company Name</asp:ListItem>
                                    <asp:ListItem Value="Cust_Contact_Person">Contact Person</asp:ListItem>
                                </asp:DropDownList>
                            <%--<asp:Label ID="lblSearchtext" runat="server" CssClass="label"
                                    EnableTheming="False" Font-Bold="True" Text="Text" Height="17px"></asp:Label>--%>
                            <asp:TextBox ID="txtSearchText" runat="server" CssClass="textbox" Width="109px"></asp:TextBox>
                            <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None"
                                                CausesValidation="False" CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click"
                                                Text="Go" /></td>
            </tr>
             <tr>
                        <td style="text-align: left; height: 16px;"></td>
                        <td colspan="2" style="text-align: right; height: 16px;">
                            <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label><asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label><asp:Label ID="lbl" runat="server" Font-Names="Verdana"></asp:Label></td>
                    </tr>
        </table>
    </div>

    <div id="divCustInfoGrid">
        <asp:GridView ID="gvCustMasterDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            Width="100%" EmptyDataText="No Records To Display" DataSourceID="SqlDataSource1" AllowSorting="True">
            <Columns>

                <asp:TemplateField HeaderText="CustomerName" Visible="false" SortExpression="Cust_Name">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblCustId" runat="server" Text='<%# Bind("Cust_Id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="CustomerName" SortExpression="Cust_Name">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnCustMaster" OnClick="lbtnCustMaster_Click" ForeColor="#3366ff" runat="server" Text='<%# Bind("Cust_Name") %>' CausesValidation="False" __designer:wfdid="w2"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField  HeaderText="CompanyName" SortExpression="Cust_Company_Name">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnCustCompName" OnClick="lbtnCustCompName_Click" ForeColor="#3366ff" runat="server" Text='<%# Bind("Cust_Company_Name") %>' CausesValidation="False" __designer:wfdid="w2"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
               <%-- <asp:BoundField DataField="Cust_Company_Name" HeaderText="CompanyName" SortExpression="Cust_Company_Name">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>--%>

                <asp:BoundField DataField="Cust_Contact_Person" HeaderText="ContactPerson" SortExpression="Cust_Contact_Person">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundField>

                <asp:BoundField DataField="Cust_Address" HeaderText="CustomerAddress" SortExpression="Cust_Address">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundField>

                <asp:BoundField DataField="Cust_Email" HeaderText="Email" SortExpression="Cust_Email">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>

                <asp:BoundField DataField="Cust_Mobile" HeaderText="Mobile" SortExpression="Cust_Mobile">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                No Data Exist!
                    
            </EmptyDataTemplate>
            <SelectedRowStyle BackColor="LightSteelBlue" />
        </asp:GridView>


        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="USP_ServiceCustomer_Search_Select" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>


    </div>
</asp:Content>


 
