<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/ServicesMP1.master" AutoEventWireup="true" CodeFile="SiteInspection_Report.aspx.cs" Inherits="Modules_Services_SiteInspection_Report" %>

<script runat="server">

protected void Chk_CheckedChanged(object sender, EventArgs e)
{

}
</script>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            font-size: medium;
        }
    </style>
        <script type="text/javascript">
            // Let's use a lowercase function name to keep with JavaScript conventions
            function selectAll(invoker) {
                // Since ASP.NET checkboxes are really HTML input elements
                //  let's get all the inputs 
                var inputElements = document.getElementsByTagName('input');

                for (var i = 0 ; i < inputElements.length ; i++) {
                    var myElement = inputElements[i];

                    // Filter through the input types looking for checkboxes
                    if (myElement.type === "checkbox") {

                        // Use the invoker (our calling element) as the reference 
                        //  for our checkbox status
                        myElement.checked = invoker.checked;
                    }
                }
            }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div id="divCustInfo">
        <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
            <tr>
                <td style="text-align: left">Site Inspection Report</td>
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
                <td style="text-align:center">
                    <span class="auto-style1">Client Name :</span>
                    <asp:TextBox ID="txtSeearchText" runat="server"></asp:TextBox>
               
                    <span class="auto-style1">Client ID :</span>
                    <asp:TextBox ID="txtClientId" runat="server"></asp:TextBox>&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="GO" OnClick="btnSearch_Click" />

                </td>

            </tr>
            
            <tr>
                <td>
                    <asp:Button ID="btnNew" runat="server" Text="NEW" OnClick="btnNew_Click" />
                    &nbsp;<asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete" />

                </td>
            </tr>
        </table>
    </div>
    <div id="divGvDetails">

        <asp:GridView ID="gvSiteReport" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            Width="100%" SelectedRowStyle-BackColor="#808080" EmptyDataText="No Records To Display" AllowSorting="True" OnPageIndexChanging="gvSiteReport_PageIndexChanging">
            <Columns>
                <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkhdr" runat="server" AutoPostBack="True" OnClick="selectAll(this)" />
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="Chk" AutoPostBack="true" OnCheckedChanged="Chk_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>

                <asp:TemplateField HeaderText="Client Id" Visible="true" SortExpression="Client_Id">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblClientId" runat="server" Text="<%# Bind('Client_Id') %>"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                               

                <asp:TemplateField HeaderText="Client Name" SortExpression="Client_Name">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnClientName" OnClick="lbtnClientName_Click" ForeColor="#3366ff" runat="server" Text="<%# Bind('Client_Name') %>" CausesValidation="False" __designer:wfdid="w2"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="Site_Address" HeaderText="Site Address" SortExpression="Site_Address">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>

                <asp:BoundField DataField="Executive_Name" HeaderText="Executive Name" SortExpression="Executive_Name">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>

                <asp:BoundField DataField="Technician_Name" HeaderText="Technician Name" SortExpression="Technician_Name">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundField>

                <asp:BoundField DataField="Quotation_Date" HeaderText="Quotation Date" SortExpression="Quotation_Date">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundField>

                <asp:BoundField DataField="Customer_Name" HeaderText="Customer Name" SortExpression="Customer_Name">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>


            </Columns>

        </asp:GridView>
        <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT * FROM [Site_Inspection_Report_tbl]"></asp:SqlDataSource>--%>
    </div>
</asp:Content>


 
