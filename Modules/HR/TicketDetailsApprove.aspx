<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TicketDetailsApprove.aspx.cs" Inherits="Modules_HR_TicketDetailsApprove" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">

  <table id="TblMain" style="width: 100%">
        <tr>
            <td class="searchhead" colspan="2 " style="height: 30px; font-size: 16px"><b>Ticket Details</b></td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <%--<asp:GridView ID="gvTicketdetails" runat="server" DataSourceID="SqlDataSource1" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="EMP_ID1,DEPT_ID1,DESG_ID1,REG_ID" Width="100%" SelectedRowStyle-BackColor="#c0c0c0" AllowSorting="True">--%>
                     <asp:GridView ID="gvTicketdetails" runat="server" DataSourceID="SqlDataSource1" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="EMP_ID1,DEPT_ID1,DESG_ID1" Width="100%" SelectedRowStyle-BackColor="#c0c0c0" AllowSorting="True">
                    <Columns>
                        <asp:BoundField DataField="TicketDetails_Id" HeaderText="Slno" />
                        <asp:TemplateField HeaderText="Employee Name" SortExpression="EMP_FIRST_NAME">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("EMP_FIRST_NAME") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnTicketDetails" runat="server" OnClick="lbtnTicketDetails_Click" ForeColor="#0066FF" Text="<%# Bind('EMP_FIRST_NAME') %>"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="EMP_PHONE" HeaderText="Emp Mobile" SortExpression="EMP_PHONE" />
                        <asp:BoundField DataField="Moving_Date" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Moving_Date" HtmlEncode="False" SortExpression="Moving_Date" />
                        <asp:BoundField DataField="Mode_Travel" HeaderText="Mode_Travel" SortExpression="Mode_Travel" />
                        <asp:BoundField DataField="Destination" HeaderText="Destination" SortExpression="Destination" />
                        <asp:BoundField DataField="APPROVEDBY" HeaderText="Approved_By" SortExpression="Approved_By" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_HR_TICKETDETAILS_Aprrove_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="0" Name="SearchItemName" Type="String" />
                        <asp:Parameter DefaultValue="0" Name="SearchValue" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <table align="center">
                    <tr>
                        <td>
                            <asp:Button ID="btnAdd" runat="server" Text="Add New" OnClick="btnAdd_Click" Visible="False" Enabled="False" />
                        </td>
                        <td>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" Visible="False" Enabled="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="width: 100%" runat="server" visible="false" id="tblDetails">
                    <tr>
                        <td colspan="4" class="profilehead">Employee Ticket Details</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td align="right">
                            <asp:Label ID="Label5" runat="server" Text="Date :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDate" runat="server" type="date"></asp:TextBox>
                           
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label3" runat="server" Text="Department :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlDepartment" runat="server" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label1" runat="server" Text="EmployeeName"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlEmployee" runat="server" Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label2" runat="server" Text="Designation :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlDesignation" runat="server" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label4" runat="server" Text="From Location :"></asp:Label>
                        </td>
                        <td align="left">
                            <%--<asp:DropDownList ID="ddlLocation" runat="server">
                            </asp:DropDownList>--%>
                            <asp:TextBox ID="txtLocation" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label6" runat="server" Text="Moving Date :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtMovingdate" runat="server" type="date"></asp:TextBox>
                            
                        </td>
                        <td align="right">
                            <asp:Label ID="Label8" runat="server" Text="Mode of Travel :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtModeoftravel" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label12" runat="server" Text="Destination :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDestination" runat="server"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label13" runat="server" Text="Eligibility :"></asp:Label>
                        </td>
                        <td align="left">
                           <asp:DropDownList ID="ddlEligibility" runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                <asp:ListItem Value="1">General</asp:ListItem>
                                <asp:ListItem Value="2">A.C.</asp:ListItem>
                                <asp:ListItem Value="3">Sleeper</asp:ListItem>
                                <asp:ListItem Value="4">Flight</asp:ListItem>
                                <asp:ListItem Value="5">One-Tier</asp:ListItem>
                                <asp:ListItem Value="6">Two-Tier</asp:ListItem>
                                <asp:ListItem Value="7">Three-Tier</asp:ListItem>
                            </asp:DropDownList>

                             <br />
                             <asp:TextBox ID="txtEligibility" runat="server" Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label14" runat="server" Text="Idproof  :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlIdProof" runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                <asp:ListItem Value="1">Aadhar </asp:ListItem>
                                <asp:ListItem Value="2">Driving Licence</asp:ListItem>
                                <asp:ListItem Value="3">PAN Card</asp:ListItem>
                                <asp:ListItem Value="4">Voter ID</asp:ListItem>
                                <asp:ListItem Value="5">PassPort</asp:ListItem>
                                <asp:ListItem Value="6">Bank PassBook</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label9" runat="server" Text="Age :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtAge" runat="server"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                     <td align="right">
                            <asp:Label ID="Label10" runat="server" Text="Idproof Number :"></asp:Label>
                        </td>
                        <td align="left">
                             <asp:TextBox ID="txtIdproofno" runat="server"></asp:TextBox>
                            </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        </tr>
                     <tr>
                        <td align="right">
                            <asp:Label ID="Label7" runat="server" Text="Mobile  Number :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtMobileNo" runat="server"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label11" runat="server" Text="Approved By :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlApprovedby" runat="server" Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">&nbsp;</td>
                        <td align="left">&nbsp;</td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Visible="False" Enabled="False" />
                                    </td>
                                    <td style="width: 4px">
                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" Visible="False" Enabled="False" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnExit" runat="server" Text="Exit" OnClick="btnExit_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnApprove" runat="server" OnClick="btnApprove_Click" Text="Approve" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>

