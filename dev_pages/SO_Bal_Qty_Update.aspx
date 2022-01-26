<%@ Page Title="" Language="C#" MasterPageFile="~/dev_pages/MPage1.master" AutoEventWireup="true" CodeFile="SO_Bal_Qty_Update.aspx.cs" Inherits="dev_pages_SO_Bal_Qty_Update" %>

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <u>
        <h3>Update SO balance Qty Page:</h3>
    </u>
    <table>
        <tr>

            <td>SO No :
            </td>
            <td>
                <asp:TextBox ID="txtSONo" runat="server"></asp:TextBox>

            </td>

            <td>SO Date :
            </td>
            <td>
                <asp:TextBox ID="txtSODate" CssClass="datetimetxt" runat="server"></asp:TextBox>
            </td>


        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>

            <td>DC No :
            </td>
            <td>
                <asp:TextBox ID="txtDcNo" runat="server"></asp:TextBox>

            </td>

            <td>DC Date :
            </td>
            <td>
                <asp:TextBox ID="txtDcDate" CssClass="datetimetxt" runat="server"></asp:TextBox>
            </td>


        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>


        <tr>

            <td>Item Model No :
            </td>
            <td>
                <asp:TextBox ID="txtModelNo" runat="server"></asp:TextBox>

            </td>
            <td>Item Code :
            </td>
            <td>
                <asp:TextBox ID="txtItemCode" runat="server"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>

        <tr>
            <td>Customer Name :
            </td>
            <td>
                <asp:TextBox ID="txtCustname" runat="server"></asp:TextBox>
            </td>
            <td></td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="Search" BackColor="#FF9900" Font-Bold="True" OnClick="btnSearch_Click" />
            </td>

        </tr>

    </table>
    <br />
    <table style="width: 100%">
        <tr>
            <td>
                <asp:Label Text="SO Details Grid :" Font-Underline="true" Font-Bold="true" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnUpdateSOQty"  BackColor="#FF9900" Font-Bold="True" OnClick="btnUpdateSOQty_Click" runat="server" Text="Update SO Bal Quantity" />
            </td>
        </tr>
        <tr>
            <td style="width: 50%;">
                <asp:GridView ID="gvSOBalQty" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="SO_DET_ID" OnPageIndexChanging="gvSOBalQty_PageIndexChanging" >
                    <Columns>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkhdr" runat="server" AutoPostBack="True" OnClick="selectAll(this)" />
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="Chk" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="SO_DET_ID" SortExpression="SO_DET_ID">

                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbtnSO_DET_ID" ForeColor="#0066ff" Text='<%#Eval("SO_DET_ID")%>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="SO_ID" SortExpression="SO_ID">

                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblSO_ID" ForeColor="#0066ff" Text='<%#Eval("SO_ID")%>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SO_NO" HeaderText="SO_NO" SortExpression="SO_NO" />
                        <asp:BoundField DataField="CUST_NAME" HeaderText="CUST_NAME" SortExpression="CUST_NAME" />
                        <asp:BoundField DataField="SO_Date" HeaderText="SO_Date" SortExpression="SO_Date" />
                        <asp:TemplateField HeaderText="ITEM_CODE" SortExpression="ITEM_CODE">

                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblITEM_CODE" ForeColor="#0066ff" Text='<%#Eval("ITEM_CODE")%>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="ITEM_MODEL_NO" SortExpression="ITEM_MODEL_NO" />

                        <asp:BoundField DataField="SO_DET_QTY" HeaderText="SO_DET_QTY" SortExpression="SO_DET_QTY" />

                        <asp:TemplateField HeaderText="Balance Qty" SortExpression="BalanceQty">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="txtBalanceQty" TextMode="SingleLine" Width="100px" Text='<%#Eval("BalanceQty")%>' runat="server"></asp:TextBox>

                            </ItemTemplate>
                        </asp:TemplateField>


                    </Columns>
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" ForeColor="#003399" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                    <SortedDescendingHeaderStyle BackColor="#002876" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SELECT [SO_DET_ID],[SO_ID],[SO_NO],[CUST_NAME],[SO_Date],[ITEM_CODE],[ITEM_MODEL_NO],[SO_DET_QTY],[BalanceQty]  FROM [dbo].[V_1SO_Item_Bal_Qty] order by SO_Date desc"></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label Text="DC Details Grid :" Font-Bold="true" Font-Underline="true" runat="server" />
            </td>

        </tr>
        <tr>

            <td style="width: 50%;">
                <asp:GridView ID="gvDCItemQty" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="DC_DET_ID" OnPageIndexChanging="gvDCItemQty_PageIndexChanging" >
                    <Columns>

                        <asp:TemplateField HeaderText="DC_DET_ID" SortExpression="DC_DET_ID">

                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbtnDC_DET_ID" ForeColor="#0066ff" Text='<%#Eval("DC_DET_ID")%>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="DC_ID" SortExpression="DC_ID">

                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDC_ID" ForeColor="#0066ff" Text='<%#Eval("DC_ID")%>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DC_NO" HeaderText="DC_NO" SortExpression="DC_NO" />
                        <asp:BoundField DataField="SO_NO" HeaderText="SO_NO" SortExpression="SO_NO" />
                        
                        <asp:BoundField DataField="CUST_NAME" HeaderText="CUST_NAME" SortExpression="CUST_NAME" />
                        <asp:BoundField DataField="DC_DATE" HeaderText="DC_DATE" SortExpression="DC_DATE" />
                        <asp:TemplateField HeaderText="ITEM_CODE" SortExpression="ITEM_CODE">

                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblITEM_CODE" ForeColor="#0066ff" Text='<%#Eval("ITEM_CODE")%>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="ITEM_MODEL_NO" SortExpression="ITEM_MODEL_NO" />
                        <asp:BoundField DataField="DC_DET_QTY" HeaderText="DC_DET_QTY" SortExpression="DC_DET_QTY" />

                    </Columns>
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" ForeColor="#003399" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                    <SortedDescendingHeaderStyle BackColor="#002876" />
                </asp:GridView>

                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SELECT DC_DET_ID,DC_ID,DC_NO,CUST_NAME,DC_DATE,ITEM_CODE,[ITEM_MODEL_NO],DC_DET_QTY  FROM V_2DC_Item_Qty_Details order by DC_DATE desc"></asp:SqlDataSource>

            </td>
        </tr>

          <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label Text="DC Summary Grid :" Font-Bold="true" Font-Underline="true" runat="server" />
            </td>

        </tr>
        <tr>
            
                <td style="width: 50%;">
                <asp:GridView ID="gvDcItemSummaryGrid" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="DC_ID" OnPageIndexChanging="gvDcItemSummaryGrid_PageIndexChanging" >
                    <Columns>
                    
                        <asp:TemplateField HeaderText="DC_ID" SortExpression="DC_ID">

                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDC_ID" ForeColor="#0066ff" Text='<%#Eval("DC_ID")%>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DC_NO" HeaderText="DC_NO" SortExpression="DC_NO" />
                        <asp:BoundField DataField="CUST_NAME" HeaderText="CUST_NAME" SortExpression="CUST_NAME" />
                        <asp:BoundField DataField="DC_DATE" HeaderText="DC_DATE" SortExpression="DC_DATE" />
                        <asp:TemplateField HeaderText="ITEM_CODE" SortExpression="ITEM_CODE">

                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblITEM_CODE" ForeColor="#0066ff" Text='<%#Eval("ITEM_CODE")%>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="ITEM_MODEL_NO" SortExpression="ITEM_MODEL_NO" />
                        <asp:BoundField DataField="Sum_Item_Qty" HeaderText="Sum_Item_Qty" SortExpression="Sum_Item_Qty" />

                    </Columns>
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" ForeColor="#003399" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                    <SortedDescendingHeaderStyle BackColor="#002876" />
                </asp:GridView>

                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SELECT DC_ID,DC_NO,CUST_NAME,DC_DATE,ITEM_CODE,ITEM_MODEL_NO,Sum_Item_Qty  FROM ValueAppDb.dbo.V_3DC_Item_Qty_Sum order by ITEM_CODE"></asp:SqlDataSource>

            </td>
           
        </tr>

    </table>


    <script src="../js/jquery.min.js"></script>
    <script src="../js/jquery.datetimepicker.js"></script>
    <link href="../js/jquery.datetimepicker.css" rel="stylesheet" />
    <script>
        $('.datetimetxt').datetimepicker({
            dayOfWeekStart: 1,
            lang: 'en'
        });
        $('#datetimepicker').datetimepicker({ value: '2015/04/15 05:03', step: 10 });

    </script>
</asp:Content>

