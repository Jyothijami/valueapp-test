<%@ Page Language="C#" MasterPageFile="~/MPs/PurchaseMP1.master" AutoEventWireup="true"
    CodeFile="SupplierMaster.aspx.cs" Inherits="Modules_Scm_SupplierMaster" Title="|| Value App : Scm : Supplier Master ||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                <%--<span style="font-size: 10pt">Supplier Master</span>--%>
                Supplier Master
            </td>
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
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td id="Td20" colspan="4"></td>
        </tr>
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left; height: 25px;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
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
                                    <asp:ListItem Value="SUP_ID">Supplier Id</asp:ListItem>
                                    <asp:ListItem Value="SUP_NAME">Supplier Name</asp:ListItem>
                                    <asp:ListItem Value="SUP_CONTACT_PERSON">Contact Person</asp:ListItem>
                                    <asp:ListItem Value="SUP_ADDRESS">Address</asp:ListItem>
                                    <asp:ListItem Value="EMP_PHONE">Phone No</asp:ListItem>
                                    <asp:ListItem Value="SUP_MOBILE">Mobile No</asp:ListItem>
                                    <asp:ListItem Value="SUP_EMAIL">Email</asp:ListItem>
                                    <asp:ListItem Value="SUP_FAXNO">FaxNo</asp:ListItem>
                                    <asp:ListItem Value="SUP_RANKING">Ranking</asp:ListItem>
                                    <asp:ListItem Value="PRODUCT_COMPANY_NAME">Brand</asp:ListItem>

                                </asp:DropDownList>
                            <%--<asp:Label ID="lblSearchtext" runat="server" CssClass="label"
                                    EnableTheming="False" Font-Bold="True" Text="Text" Height="17px"></asp:Label>--%>
                            <asp:TextBox ID="txtSearchText" runat="server" CssClass="textbox" Width="109px"></asp:TextBox>
                            <asp:Image ID="imgCurrentDayTasksToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image><asp:Button ID="btnSearchGo" runat="server" BorderStyle="None"
                                                CausesValidation="False" CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click"
                                                Text="Go" /></td>
                    </tr>
                    <tr>
                        <td style="text-align: left; height: 16px;"></td>
                        <td colspan="2" style="text-align: right; height: 16px;">
                            <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label><asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label><asp:Label ID="lbl" runat="server" Font-Names="Verdana"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: left">
                <itemtemplate></itemtemplate>
            </td>
        </tr>
        <tr>
            <td style="text-align: center;" colspan="4">
                <asp:GridView ID="gvSupplierDetails" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" DataSourceID="sdsSupplierDetails" OnRowDataBound="gvSuppliersMasterDetails_RowDataBound" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="SUP_NAME" SortExpression="SUP_NAME" HeaderText="SupplierNameHidden"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" DataField="SUP_ID" SortExpression="SUP_ID" HeaderText="SupplierID"></asp:BoundField>
                        <asp:TemplateField HeaderText="SupplierName">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("SUP_name") %>' ID="TextBox1"></asp:TextBox>
                            </EditItemTemplate>

                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnSupplierNameName" OnClick="lbtnSupplierName_Click" ForeColor="#0066ff" runat="server" Text="<%# Bind('Sup_Name') %>"
                                    CausesValidation="False" __designer:wfdid="w9"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SUP_CONTACT_PERSON" SortExpression="SUP_CONTACT_PERSON" HeaderText="ContactPerson">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="SUP_ADDRESS" SortExpression="SUP_ADDRESS" HeaderText="Address">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="SUP_PHONE" SortExpression="SUP_PHONE" HeaderText="PhoneNo">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="SUP_MOBILE" SortExpression="SUP_MOBILE" HeaderText="MobileNo">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="SUP_EMAIL" SortExpression="SUP_EMAIL" HeaderText="Email">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="SUP_FAXNO" SortExpression="SUP_FAXNO" HeaderText="FaxNo">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="SUP_RANKING" SortExpression="SUP_RANKING" HeaderText="Ranking">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PRODUCT_COMPANY_NAME" SortExpression="PRODUCT_COMPANY_NAME" HeaderText="Brand">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                    <SelectedRowStyle BackColor="LightSteelBlue" />
                </asp:GridView>
                <asp:SqlDataSource ID="sdsSupplierDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SCM_SUPPLIER_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; height: 75px;">
                <table id="Table1" align="center">
                    <tr>
                        <td>
                            <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CausesValidation="False" /></td>
                        <td>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CausesValidation="False" Visible="False" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td id="tblSupDetails" runat="server" visible="false" colspan="4" style="text-align: center">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">&nbsp;</td>
        </tr>
        <tr>
            <td style="height: 21px"></td>
            <td style="height: 21px;"></td>
            <td style="height: 21px;"></td>
            <td style="height: 21px"></td>
        </tr>
    </table>
</asp:Content>
 
