<%@ Page Language="C#" MasterPageFile="~/MPs/SalesMP1.master" AutoEventWireup="true" CodeFile="ProcessStatus.aspx.cs"
     Inherits="Modules_SM_ProcessStatus" Title="|| Value App : S&M : Process Status ||" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">

    
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                Process status</td>
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
    <table width="100%">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
                   
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; " colspan="2" >
                            <asp:Label ID="Label2" runat="server" Text="Employee Name : "></asp:Label>
                            <asp:DropDownList ID="ddlEmployeeName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEmployeeName_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label1" runat="server" Text="Name : "></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlNoHeads" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNoHeads_SelectedIndexChanged">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="sl">SL No.</asp:ListItem>
                                <asp:ListItem Value="qtn">QTN No.</asp:ListItem>
                                <asp:ListItem Value="so">PO No.</asp:ListItem>
                                <asp:ListItem Value="op">IO No.</asp:ListItem>
                                <asp:ListItem Value="dc">DC No.</asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label3" runat="server" Text="No : "></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlNos" runat="server">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="0">--Select Name--</asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="text-align: left">
                            <asp:Button ID="btnGo" runat="server"  CausesValidation="False"
                                OnClick="btnGo_Click" Text="Go" /></td>
                    </tr>
                    <tr>
                        <td style="height: 19px">
                        </td>
                        <td style="height: 19px; text-align: left;">
                            <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label></td>
                        <td style="height: 19px">
                        </td>
                        <td style="height: 19px; text-align: left">
                            <asp:Label ID="lblNameHidden" runat="server" Visible="False"></asp:Label></td>
                        <td style="height: 19px">
                        </td>
                        <td style="height: 19px; text-align: left">
                            <asp:Label ID="lblNoHidden" runat="server" Visible="False"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AllowSorting="True" SelectedRowStyle-BackColor="#c0c0c0">
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                SelectCommand="SP_PROCESS_STATUS_SELECT" SelectCommandType="StoredProcedure" >
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="lblEmpIdHidden" DefaultValue="0" Name="EmpId" PropertyName="Text"
                                        Type="String" />
                                    <asp:ControlParameter ControlID="lblNameHidden" DefaultValue="0" Name="SearchItemName"
                                        PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="lblNoHidden" DefaultValue="0" Name="SearchValue"
                                        PropertyName="Text" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>


 
