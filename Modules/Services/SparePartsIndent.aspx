<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/ServicesMP1.master" AutoEventWireup="true" CodeFile="SparePartsIndent.aspx.cs" Inherits="Modules_Services_SparePartsIndent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        #divRecords {
            text-align: center;
        }
        #IndentDetails {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <h3 style="padding-left: 70px; text-decoration: underline;">Spare Parts Indent</h3>
    <div id="divRecords">
        <asp:GridView ID="gvSparePartsIndent" EmptyDataText="No Records To Display" Width="100%" runat="server" AutoGenerateColumns="False" DataKeyNames="Indent_No" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:TemplateField HeaderText="Indent No" Visible="true" SortExpression="Indent_No">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>

                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="lbtnIndentNo"  ForeColor="#3366ff"  Text='<%# Bind("Indent_No") %>' CausesValidation="False" OnClick="lbtnIndentNo_Click"></asp:LinkButton>

                    </ItemTemplate>
                </asp:TemplateField>

                <%--<asp:BoundField DataField="Indent_No" HeaderText="Indent No" ReadOnly="True" SortExpression="Indent_No" />--%>
                <asp:BoundField DataField="IndentDate" HeaderText="Indent Date" SortExpression="IndentDate" />
                <asp:BoundField DataField="Brand" HeaderText="Brand" SortExpression="Brand" />
                <asp:BoundField DataField="Model" HeaderText="Model" SortExpression="Model" />
                <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                <asp:BoundField DataField="ClientAddress" Visible="false" HeaderText="Client Address" SortExpression="ClientAddress" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT * FROM [SparesIndent_tbl] ORDER BY [IndentDate] DESC"></asp:SqlDataSource>
    </div>
    <br />
    <div id="divVisitDet" align="center">
    <h4 style="text-decoration: underline;">New Spare Parts Indent</h4>

        <table>
            <tr style="text-align: right">
                <td>Indent No :
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtIndentNo" runat="server"></asp:TextBox>
                </td>
                <td style="width: 5%"></td>
                <td>Date :
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtIndentDate" ReadOnly="false" runat="server"></asp:TextBox>

                </td>
            </tr>

            <tr style="text-align: right">
                <td>Brand :
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtBrand" runat="server"></asp:TextBox>
                </td>
                <td style="width: 5%"></td>
                <td>Model :
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtModel" ReadOnly="false" runat="server"></asp:TextBox>

                </td>
            </tr>

            <tr>
                <td style="text-align: right">Code :
                </td>
                <td style="text-align: left">
                    <asp:TextBox runat="server" ID="txtCode" />
                </td>
                <td style="width: 5%"></td>
                <td style="text-align: right">Quantity :
                </td>
                <td style="text-align: left">
                    <asp:TextBox runat="server" ID="txtQuantity" />
                </td>
            </tr>

            <tr style="text-align: right">
                <td>Description :
                </td>
                <td style="text-align: left" colspan="4">
                    <asp:TextBox ID="txtDescription" TextMode="MultiLine" style="width:555px" runat="server"></asp:TextBox>
                </td>

            </tr>

            <tr style="text-align: right">
                <td>Client Name & Address :
                </td>
                <td style="text-align: left" colspan="4">
                    <asp:TextBox ID="txtClientAddress" TextMode="MultiLine" style="width:555px" runat="server"></asp:TextBox>
                </td>

            </tr>

            <tr style="text-align: center">
                <td colspan="5" style="text-align: center">
                    <asp:Button ID="btnAddSpareIndent" runat="server" Text="Add" OnClick="btnAddSpareIndent_Click" />
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" />
                </td>
            </tr>

        </table>
    </div>
    <div id="IndentDetails">
        <asp:GridView ID="gvIndentDetails" EmptyDataText="No Records To Display" runat="server" AutoGenerateColumns="False" Width="100%">
            <Columns>

                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description"></asp:BoundField>                
                <asp:BoundField DataField="Brand" HeaderText="Date Of Brand" SortExpression="Brand"></asp:BoundField>                
                <asp:BoundField DataField="Model" HeaderText="Model" SortExpression="Model"></asp:BoundField>                
                <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code"></asp:BoundField>
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity"></asp:BoundField>
                <%--<asp:BoundField DataField="IndentDate" HeaderText="Indent Date" SortExpression="IndentDate"></asp:BoundField>--%>
                <asp:BoundField DataField="ClientAddress" HeaderText="Client Address" SortExpression="ClientAddress"></asp:BoundField>
                
            </Columns>
        </asp:GridView>

        
        <br />
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
        <asp:Button ID="btnRefreshAll" runat="server" Text="Refresh" OnClick="btnRefreshAll_Click" />
    </div>
</asp:Content>


 
