<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/ServicesMP1.master" AutoEventWireup="true" CodeFile="SparesIndentPrint.aspx.cs" Inherits="Modules_Services_SparesIndentPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function printDiv(PrintDiv) {
            //Get the HTML of div
            var divElements = document.getElementById(PrintDiv).innerHTML;
            //Get the HTML of whole page
            var oldPage = document.body.innerHTML;

            //Reset the page's HTML with div's HTML only
            document.body.innerHTML =
              "<html><head><title></title></head><body>" +
              divElements + "</body>";

            //Print Page
            window.print();

            //Restore orignal HTML
            document.body.innerHTML = oldPage;


        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <input type="button" value="Print" onclick="javascript: printDiv('PrintDiv')" />

    <div id="PrintDiv">
        <h3 style="text-align: center;">SPARE PARTS INDENT</h3>

        <table style="width: 100%; text-align: right;">
            <tr>
                <td style="text-align: left">
                    <%--<asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick = "return PrintPanel();" />--%>
                </td>
                <td>Date :
                    <asp:Label ID="lblDate" Font-Bold="true" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvSpareIndentPrint" EmptyDataText="No Records To Display" Width="100%" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Brand" HeaderText="Brand" SortExpression="Brand">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Model" HeaderText="Model" SortExpression="Model">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Available" HeaderText="Available" SortExpression="Available">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Indent" HeaderText="Indent" SortExpression="Indent">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <br />
        <table style="width: 100%">
            <tr>
                <td style="text-align: left; font-weight: 700;" colspan="6">Client Name & Address : 
                    <asp:TextBox ID="txtClientAddress" TextMode="MultiLine" Width="500px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left; font-weight: 700;" colspan="6">Free :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Chargeble :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Amount Rs.</td>
                
            </tr>
            <tr>
                <td colspan="6">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left;width:100px; font-weight: 700;">Technician Signature : 
                    </td>
                <td style="width:100px">
                    <asp:Label ID="lblTechnician" runat="server"></asp:Label>
                </td>
                <td style="width:70px">

                    <asp:Button ID="btnTechApprove" runat="server" Visible="false" Text="Technicain Approve" OnClick="btnTechApprove_Click" Width="100px" />
                </td>
                <td style="text-align: left;width:100px; font-weight: 700;">Stores : 
                    </td>
                <td style="width:100px">
                    <asp:Label ID="lblStores" runat="server"></asp:Label>
                </td>
                <td >
                    <asp:Button ID="btnStoresApprove" runat="server" Visible="false" Text="Store Approve" OnClick="btnStoresApprove_Click" Width="100px" />

                </td>
            </tr>
            <tr>
                <td colspan="6">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left;width:240px; font-weight: 700;">Technical Head :
                    </td>
                <td style="width:100px">
                    <asp:Label ID="lblHead" runat="server"></asp:Label>
                   </td>
                <td  style="width:70px">
                    <asp:Button ID="btnHeadApprove" runat="server" Visible="false" Text="Head Approve" OnClick="btnHeadApprove_Click" Width="100px" />

                </td>
                <td style="text-align: left;width:100px; font-weight: 700;">Purchase : 
                    </td>
                <td style="width:100px">
                    <asp:Label ID="lblPurchase" runat="server"></asp:Label>
                    </td>
                <td>
                    <asp:Button ID="btnPurchaseApprove" runat="server" Visible="false" Text="Purchase Approve" OnClick="btnPurchaseApprove_Click" Width="100px" />

                </td>
            </tr>
        </table>

        <asp:Label ID="lblIndentNo" Visible="false" runat="server"></asp:Label>

    </div>
</asp:Content>


 
