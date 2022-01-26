<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/PurchaseMP1.master" AutoEventWireup="true" CodeFile="SupplierQuationDetails.aspx.cs" Inherits="Modules_SCM_SupplierQuationDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
     <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td style="text-align:left">Supplier Enquiry</td>
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
     <table border="0" cellpadding="0" cellspacing="0" width="100%">
           <tr>
            <td></td>
            <td></td>
            <td></td>
            <td width="750"></td>
        </tr>
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left" colspan="2">
                                    Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
                                </td>

                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td style="height: 25px">
                                        <asp:Label ID="Label12" runat="server" CssClass="label"  Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="textbox" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem Value="0">--</asp:ListItem>

                                            <asp:ListItem Value="IND_APPRL_NO">Sup Enquiry No.</asp:ListItem>
                                            <asp:ListItem Value="DEPT_NAME">Department</asp:ListItem>
                                             <asp:ListItem Value="SUP_NAME">Supplier Name</asp:ListItem>
                                            <asp:ListItem Value="IND_APPRL_DATE">Sup Enq Date</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList ID="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"                                             
                                            Visible="False" Width="50px" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged">
                                            <asp:ListItem Selected="True">=</asp:ListItem>
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem>&gt;</asp:ListItem>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <asp:ListItem>&gt;=</asp:ListItem>
                                            <asp:ListItem>R</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="height: 25px">
                                                <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                                </asp:Label></td>
                                            <td style="height: 25px">
                                                <asp:TextBox ID="txtSearchValueFromDate" type="date" runat="server" EnableTheming="True" Visible="False"
                                                    Width="106px">
                                                </asp:TextBox>
                                                  </td>
                                            <td style="height: 25px">
                                                <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                                </asp:Label></td>
                                            <td style="height: 25px">
                                                <asp:TextBox ID="txtSearchValueToDate" type="date" runat="server" EnableTheming="True" Visible="False"
                                                    Width="106px">
                                                </asp:TextBox>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                                </asp:TextBox>
                                                </td>
                                    <td style="height: 25px">
                                        <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false"
                                            CssClass="gobutton" Text="Go" OnClick="btnSearchGo_Click" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label><asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label><asp:Label
                                    ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                    Visible="False"></asp:Label><asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td id="Td20" colspan="4">
                <asp:GridView ID="gvSupEnquiryDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="SqlDataSource1" Width="100%"
                    AllowSorting="True" OnRowDataBound="gvSupEnquiryDetails_RowDataBound" PageSize="8">
                    <Columns>
                        <asp:BoundField DataField="IND_APPRL_ID" SortExpression="IND_APPRL_ID" HeaderText="INDAPPRLIDHidden"></asp:BoundField>
                        <asp:TemplateField HeaderText="Sup Enq No" SortExpression="IND_APPRL_NO">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("SO_NO") %>' ID="TextBox1"></asp:TextBox>

                            </EditItemTemplate>

                            <ItemStyle Width="100px" HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle Width="100px" HorizontalAlign="Left"></HeaderStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnSupEnqNo" runat="server" ForeColor="#0066ff" Text="<%# BIND('IND_APPRL_NO') %>" CausesValidation="False" OnClick="lbtnSupEnqNo_Click"></asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True" SortExpression="IND_APPRL_DATE" DataField="IND_APPRL_DATE" HeaderText="Sup Enq Date">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                         <asp:BoundField DataField="SUP_NAME" SortExpression="SUP_NAME" HeaderText="Supplier Name">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <%--<asp:BoundField DataField="INDIGENOUS_FOREIGN" SortExpression="INDIGENOUS_FOREIGN" HeaderText="IndigenousOrForeign"></asp:BoundField>--%>
                        <asp:BoundField DataField="DEPT_NAME" SortExpression="DEPT_NAME" HeaderText="Department Name">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <%--<asp:BoundField DataField="SUP_CONTACT_PERSON" SortExpression="SUP_CONTACT_PERSON" HeaderText="ContactPerson">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>--%>
                        <asp:BoundField DataField="EMP_FIRST_NAME" SortExpression="EMP_FIRST_NAME" HeaderText="FollowUp Name">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                     </Columns>
                    <EmptyDataTemplate>
                        No Record Found
                    
                    </EmptyDataTemplate>
                    <SelectedRowStyle BackColor="LightSteelBlue" />
                </asp:GridView>
                
                
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_Supenq_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchTypeHidden" DefaultValue="0" Name="SearchType" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue" PropertyName="Text" Type="String" />
                         <asp:ControlParameter ControlID="lblSearchValueFromHidden" DefaultValue="0" Name="SearchValueFrom" PropertyName="Text" Type="String" />
                         <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="CpId" ControlID="lblCPID"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="EmpId" ControlID="lblEmpIdHidden"></asp:ControlParameter>

                        <asp:ControlParameter ControlID="lblUserType" DefaultValue="0" Name="UserType" PropertyName="Text" Type="Int64" />

                    </SelectParameters>
                </asp:SqlDataSource>
                
                
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table id="Table1" align="center">
                    <tr>
                        <td>
                            <%--<asp:Button ID="btnNew" runat="server" CausesValidation="False" Text="New" OnClick="btnNew_Click" />--%></td>
                        <td>
                            <asp:Button ID="btnEdit" runat="server" CausesValidation="False" Text="Edit" OnClick="btnEdit_Click" /></td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" CausesValidation="False" Text="Delete" OnClick="btnDelete_Click" /></td>
                        <td>
                            <asp:Button ID="btnPrint" runat="server" Text="Print" CausesValidation="False" OnClick="btnPrint_Click" /></td>
                    </tr>
                </table>
            </td>
        </tr>
         </table>
</asp:Content>


 
