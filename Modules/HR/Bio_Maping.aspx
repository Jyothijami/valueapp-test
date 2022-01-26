<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true" CodeFile="Bio_Maping.aspx.cs" Inherits="Modules_HR_Bio_Maping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table>
        <tr>
            <td class="profilehead" style="width:50%">Biometric ID Mapping</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
    <table width="800" align="center" cellpadding="0" cellspacing="0">
       <tr>
        <td height="30" style="text-align: right"> Select your File</td>
        <td style="text-align: left"><asp:FileUpload ID="FileUpload1" runat="server" />&nbsp;
            <asp:Button ID="Button1" runat="server" Text="Submit" onclick="Button1_Click" /></td>
       </tr>
       <tr>
        <td colspan="2" align="center" height="30">&nbsp;</td>
       </tr>
        <tr>
            <td colspan="2" style="text-align:center">
                Employee Name : <asp:TextBox ID="txtEmpName" runat="server"></asp:TextBox>
                <asp:Button ID="btnGo" runat="server" Text="Search" OnClick="btnGo_Click" />
            </td>
        </tr>
         <tr>
        <td colspan="2" align="center" height="30">&nbsp;</td>
       </tr>
       <tr>
        <td colspan="2" align="center" height="30">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True"  OnRowDataBound="GridView1_RowDataBound" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:BoundField DataField="emp_code" HeaderText="Employee Code" SortExpression="emp_code" />
                    <asp:BoundField DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name" />
                    <asp:TemplateField HeaderText="Company Emp List">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlemp" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="USP_Bio_Emp_Search" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT emp_code, emp_name FROM EMP_BIO_MAP WHERE (app_emp_id = '0') AND (app_emp_name = '0') AND (emp_code &lt;&gt; '0')"></asp:SqlDataSource>--%>
    <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>

    <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>

           </td>
       </tr>
 
       <tr>
        <td colspan="2" align="center" height="30">
            <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update" />
           </td>
       </tr>
 
    </table>
</asp:Content>


 
