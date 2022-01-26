<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Employee_Late_Report.aspx.cs" Inherits="Modules_HR_Employee_Late_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table style="width:100%">
        <tr>
            <td colspan="4" class="profilehead">
                Employee Attedance :
            </td>
        </tr>
        <tr>
              <td style="text-align: center">Month  :
                <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" AppendDataBoundItems="True" DataSourceID="SqlDataSource2" DataTextField="Month" DataValueField="ID" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                </asp:DropDownList>
                            
                        </td>
            <td>
                   <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT * FROM [Month_Calendar_tbl]">
                       
                   </asp:SqlDataSource>
            </td>
        </tr>
            </table>
    <table style="width:100%">
        
        <tr>
            <td>
                <asp:GridView ID="gvEmpLate" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="Employee Name" HeaderText="Employee Name" SortExpression="Employee Name" />
                        <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                        <asp:BoundField DataField="mon" HeaderText="Month" ReadOnly="True" SortExpression="mon" />
                        <asp:BoundField DataField="CP_FULL_NAME" HeaderText="Company Name" SortExpression="CP_FULL_NAME" />
                        <asp:BoundField DataField="Days" HeaderText="No. of Late Days" ReadOnly="True" SortExpression="Days" />
                    </Columns>

                </asp:GridView>
                <br />
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="USP_LATE" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblMonthName" DefaultValue="0" Name="month" PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:Label ID="lblMonthName" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

