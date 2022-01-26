<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="Stock_Delete.aspx.cs" Inherits="Modules_Warehouse_Stock_Delete" %>

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
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                    <td class="profilehead">Available Stock For Deletion
                    </td>
                </tr>
                <tr>
                    <td>Model No :
                        <asp:TextBox ID="txtItemCode" runat="server"></asp:TextBox>
                       
                        Location : 
                        <asp:TextBox ID="txtLocation" runat="server"></asp:TextBox>
                   
                        <asp:Button ID="btnSearch" runat="server" Text="GO" OnClick="btnSearch_Click" />
                        <asp:Button ID="btnHistory" runat ="server" Text ="History" OnClick ="btnHistory_Click" />
                    </td>
                </tr>
                <tr>
                    <td>Remarks :
                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td  style="text-align:center">
                        <asp:Label ID="Label1" runat="server" Text="Only Available Stock will be displayed Here." Font-Bold="True" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:center">
                        <asp:Button Text="Delete" ID="btnDelete" OnClick="btnDelete_Click" runat="server" />

                    </td>
                </tr>
            </table>
            <br />
            <asp:GridView ID="gvAvailableStock" AllowPaging="True" AllowSorting="True" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvAvailableStock_RowDataBound" OnPageIndexChanging="gvAvailableStock_PageIndexChanging">
                <Columns>
                    <asp:BoundField HeaderText="Id" DataField="Item_Id"></asp:BoundField>
                    <asp:BoundField HeaderText="Model No" DataField="ITEM_MODEL_NO"></asp:BoundField>
                    <asp:BoundField HeaderText="Color Name" DataField="COLOUR_NAME"></asp:BoundField>
                    <asp:BoundField HeaderText="Location" DataField="Location"></asp:BoundField>
                    
                    <asp:BoundField HeaderText="Total Qty" DataField="Avl_Qty"></asp:BoundField>
                    <asp:TemplateField HeaderText="Available Quantity">
                        <ControlStyle Width="100px" />
                        <ItemTemplate>
                            <asp:TextBox ID="txtDeleteQty" Text='<%# Bind("Quantity") %>' runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="All">
                        <HeaderTemplate>
                            <asp:CheckBox ID="cbSelectAll" runat="server" Text="All" OnClick="selectAll(this)" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox_row" runat="server"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText ="TotalQty" DataField ="TotalQty"></asp:BoundField>

                </Columns>
            </asp:GridView>
            <br />
            
            <table id="tblHisty" runat ="server" visible ="false">
                <tr>
                    <td>
                        
                 <h4>Deleted Stock History</h4>
            <asp:GridView ID="gvHistory" runat ="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" >
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                    <asp:BoundField DataField="dt_added" HeaderText="dt_added" SortExpression="dt_added" />
                    <asp:BoundField DataField="ITEM_CODE" HeaderText="ITEM_CODE" SortExpression="ITEM_CODE" />
                    <asp:BoundField DataField="Colour_Id" HeaderText="Colour_Id" SortExpression="Colour_Id" />
                    <asp:BoundField DataField="whlocid" HeaderText="whlocid" SortExpression="whlocid" />
                    <asp:BoundField DataField="Qty_InHand" HeaderText="Qty_InHand" SortExpression="Qty_InHand" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" />
                    <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="PreparedBy" SortExpression="PreparedBy" />
                </Columns>
            </asp:GridView>
            

           
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [Id], CONVERT(varchar(50),dt_added,103)as dt_added, [ITEM_CODE], [Colour_Id], [whlocid], [Qty_InHand], [Quantity], [Remarks], [EMP_FIRST_NAME] FROM [Outward_History] iNNER JOIN YANTRA_EMPLOYEE_MAST ON OUTWARD_HISTORY.PreparedBy=YANTRA_EMPLOYEE_MAST.EMP_ID WHERE ([ITEM_CODE] = @ITEM_CODE) ORDER BY [Id] DESC">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="txtItemCode" Name="ITEM_CODE" PropertyName="Text" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
            

           
                    </td>
                </tr>
            </table>

            <table id="tblHistorySearch" runat ="server" visible ="false" >
                <tr>
                    <td><h4>Deleted Stock History</h4>
                        <asp:GridView ID="gvHstrysearch" AllowPaging ="true" OnPageIndexChanging ="gvHstrysearch_PageIndexChanging" runat ="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" >
                             <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                    <asp:BoundField DataField="dt_added" HeaderText="Date" SortExpression="dt_added" />
                    <asp:BoundField DataField="ITEM_CODE" HeaderText="Model No" SortExpression="ITEM_CODE" />
                    <asp:BoundField DataField="Colour_Id" HeaderText="Colour" SortExpression="Colour_Id" />
                    <asp:BoundField DataField="whlocid" HeaderText="Location" SortExpression="whlocid" />
                    <asp:BoundField DataField="Qty_InHand" HeaderText="Qty_InHand" SortExpression="Qty_InHand" />
                    <asp:BoundField DataField="Quantity" HeaderText="Deleted Qty" SortExpression="Quantity" />
                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" />
                    <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="PreparedBy" SortExpression="PreparedBy" />
                </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="select * from Outward_History inner join YANTRA_EMPLOYEE_MAST on Outward_History .PreparedBy =YANTRA_EMPLOYEE_MAST .EMP_ID    order by ID desc"></asp:SqlDataSource>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


 
