<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Recovery_Status.aspx.cs" Inherits="Modules_Inventory_Recovery_Status" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    <table id="TABLE2" border="0" cellpadding="0" cellspacing="0" width="100%">
        
        <tr>
            <td colspan="2" class="profilehead">Open State Invoice</td>

        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:GridView ID="gvInvoiceStatusOpenDetails" runat="server" Width="100%" PageSize ="10" AllowSorting="true" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnRowDataBound="gvInvoiceStatusOpenDetails_RowDataBound" AllowPaging ="true" >
                    <Columns>
                        
                        <asp:BoundField HeaderText="Invoice ID" SortExpression="SI_ID" DataField="SI_ID" />
                        <asp:BoundField HeaderText="Invoice NO" DataField="SI_NO" SortExpression="SI_NO" />
                        <asp:BoundField HeaderText="PO No" DataField="SO_NO" SortExpression="SO_NO"/>
                        <asp:BoundField HeaderText="Customer Name" DataField="CUST_NAME" SortExpression="CUST_NAME" />
                        <asp:BoundField HeaderText="DC No" DataField="DC_NO" SortExpression="DC_NO"></asp:BoundField>
                        <asp:BoundField HeaderText="Total Amount" DataField="SO_TOTAL_AMT" SortExpression="SO_TOTAL_AMT" />
                        <asp:BoundField HeaderText="Paid Amount" DataField="recive" SortExpression="recive" />
                        <asp:BoundField HeaderText="Invoice Amount" DataField="SI_GROSS_AMT" SortExpression="SI_GROSS_AMT"></asp:BoundField>
                        <asp:BoundField HeaderText="Balance Amount" DataField="Pending Amount" />
                        <asp:BoundField HeaderText="Status"  DataField="SI_STATUS" SortExpression="SI_STATUS" />
                        <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <asp:CheckBox ID="cbSelectAll" runat="server" OnClick="selectAll(this)" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox_row" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" />
                
            </td>
        </tr>
        <tr>
            <td colspan="2" class="profilehead">Closed State Invoice
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvInvoiceStatusClosedDetails" runat="server" Width="100%" AllowPaging ="true" PageSize ="10" AllowSorting="true" AutoGenerateColumns="False" EmptyDataText="No Records Found">
                    <Columns>

                        <asp:BoundField HeaderText="Invoice ID" SortExpression="SI_ID" DataField="SI_ID" />
                        <asp:BoundField HeaderText="Invoice NO" DataField="SI_NO" SortExpression="SI_NO" />
                        <asp:BoundField HeaderText="PO No" DataField="SO_NO" SortExpression="SO_NO"/>
                        <asp:BoundField HeaderText="Customer Name" DataField="CUST_NAME" SortExpression="CUST_NAME" />
                        <asp:BoundField HeaderText="DC No" DataField="DC_NO" SortExpression="DC_NO"></asp:BoundField>
                        <asp:BoundField HeaderText="Total Amount" DataField="SO_TOTAL_AMT" SortExpression="SO_TOTAL_AMT" />
                        <asp:BoundField HeaderText="Paid Amount" DataField="recive" SortExpression="recive" />
                        <asp:BoundField HeaderText="Invoice Amount" DataField="SI_GROSS_AMT" SortExpression="SI_GROSS_AMT"></asp:BoundField>
                        <asp:BoundField HeaderText="Balance Amount" DataField="Pending Amount" />
                        <asp:BoundField HeaderText="Status"  DataField="SI_STATUS" SortExpression="SI_STATUS" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>


 
