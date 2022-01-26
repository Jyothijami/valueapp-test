<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OUTWARD_DC_Update_Stock.aspx.cs" Inherits="dev_pages_OUTWARD_DC_Update_Stock" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DC & Outward Details</title>
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
        
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
    <table>
        <tr>
            <td style ="width :50%">
                <asp:GridView id="gvDCDet" runat ="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="DC_DET_ID" OnPageIndexChanging ="gvDCDet_PageIndexChanging" >
                    <Columns>
                        <asp:BoundField DataField="DC_ID" HeaderText="DC_ID" SortExpression="DC_ID"  />
                        <asp:BoundField DataField="DC_DET_ID" HeaderText="DC_DET_ID" SortExpression="DC_DET_ID" />
                        <asp:BoundField DataField="DC_NO" HeaderText="DC_NO" SortExpression="DC_NO" />
                        <asp:BoundField DataField="DC_DATE" HeaderText="DC_DATE" SortExpression="DC_DATE" />
                        <%--<asp:BoundField DataField="CP_ID" HeaderText="CP_ID" SortExpression="CP_ID" />--%>
                        <%--<asp:BoundField DataField="DC_CUSTOMER_ID" HeaderText="cust_id" SortExpression="DC_CUSTOMER_ID" />--%>
                        <asp:BoundField DataField="CUST_NAME" HeaderText="CUST_NAME" SortExpression="CUST_NAME" />
                        <%--<asp:BoundField DataField="COLOR_ID" HeaderText="COLOR_ID" SortExpression="COLOR_ID" />--%>
                        <%--<asp:BoundField DataField="GODOWN_ID" HeaderText="GODOWN_ID" SortExpression="GODOWN_ID" />--%>
                        <asp:BoundField DataField="ITEM_CODE" HeaderText="ITEM_CODE" SortExpression="ITEM_CODE" />
                        <asp:BoundField DataField="DC_DET_QTY" HeaderText="DC_DET_QTY" SortExpression="DC_DET_QTY" />
                        <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="ITEM_MODEL_NO" SortExpression="ITEM_MODEL_NO" />
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
                
            </td>
        </tr>
    </table>
        <table>
            <tr>
                <td style="width:50%">
                    <asp:GridView runat ="server" ID="gvOutwardBind" OnPageIndexChanging ="gvOutwardBind_PageIndexChanging" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" >
                        <Columns>
                            <asp:BoundField DataField="Item_ID" HeaderText="Item_ID" SortExpression="Item_ID" />
                            <asp:BoundField DataField="DC_NO" HeaderText="DC_NO" SortExpression="DC_NO" />
                            <asp:BoundField DataField="dt_added" HeaderText="dt_added" SortExpression="dt_added" />
                            <asp:BoundField DataField="COLOUR_ID" HeaderText="COLOUR_ID" SortExpression="COLOUR_ID" />
                            <asp:BoundField DataField="PO_NO" HeaderText="PO_NO" SortExpression="PO_NO" />
                            <asp:BoundField DataField="CUSTOMERID" HeaderText="CUSTOMERID" SortExpression="CUSTOMERID" />
                            <asp:BoundField DataField="ITEM_CODE" HeaderText="ITEM_CODE" SortExpression="ITEM_CODE" />
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                            <%--<asp:BoundField DataField ="DC_DET_QTY" HeaderText="DC QTY" SortExpression="DC_DET_QTY" />--%>
                            <%--<asp:BoundField DataField="DC_DET_ID" HeaderText="DC_DET_ID" ReadOnly="True" SortExpression="DC_DET_ID" />--%>
                            <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="ITEM_MODEL_NO" SortExpression="ITEM_MODEL_NO" />
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
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT * FROM [V_Outward_DC_Bind]"></asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </div>
        <div>
            <table>
                <tr>
                    <td>
                        <asp:GridView ID="gvStkMvmtDet" runat ="server" AutoGenerateColumns="False" DataKeyNames="wh_id" >
                            <Columns>
                                <asp:BoundField DataField="SM_DCDET_ID" HeaderText="SM_DCDET_ID" SortExpression="SM_DCDET_ID" />
                                <asp:BoundField DataField="ITEM_CODE" HeaderText="ITEM_CODE" SortExpression="ITEM_CODE" />
                                <asp:BoundField DataField="SM_DC_ID" HeaderText="SM_DC_ID" SortExpression="SM_DC_ID" />
                                <%--<asp:BoundField DataField="COLOR_ID" HeaderText="COLOR_ID" SortExpression="COLOR_ID" />--%>
                                <asp:BoundField DataField="MOVINGTO" HeaderText="MOVINGTO" SortExpression="MOVINGTO" />
                                <asp:BoundField DataField="QUANTITY" HeaderText="QUANTITY" SortExpression="QUANTITY" />
                                <%--<asp:BoundField DataField="REMARKS" HeaderText="REMARKS" SortExpression="REMARKS" />--%>
                                <%--<asp:BoundField DataField="Remark" HeaderText="Remark" SortExpression="Remark" />--%>
                                <asp:BoundField DataField="CLIENT_NAME" HeaderText="CLIENT_NAME" SortExpression="CLIENT_NAME" />
                                <%--<asp:BoundField DataField="INT_IND_ID" HeaderText="INT_IND_ID" SortExpression="INT_IND_ID" />--%>
                                <asp:BoundField DataField="MOVINGTO1" HeaderText="MOVINGTO1" SortExpression="MOVINGTO1" />
                                <asp:BoundField DataField="MOVINGFROM" HeaderText="MOVINGFROM" SortExpression="MOVINGFROM" />
                                <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="ITEM_MODEL_NO" SortExpression="ITEM_MODEL_NO" />
                                <asp:BoundField DataField="COLOUR_NAME" HeaderText="COLOUR_NAME" SortExpression="COLOUR_NAME" />
                                <asp:BoundField DataField="whname" HeaderText="whname" SortExpression="whname" />
                                <asp:BoundField DataField="wh_id" HeaderText="wh_id" ReadOnly="True" SortExpression="wh_id" />
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
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="Usp_StockMovement" SelectCommandType="StoredProcedure">
                            <SelectParameters ></SelectParameters>
                        </asp:SqlDataSource>

                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
