<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/SalesMP1.master" AutoEventWireup="true" CodeFile="Customer_Info_Log_Activity - Copy.aspx.cs" Inherits="Modules_SM_Customer_Info_Log_Activity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
<table style="width:100%;">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" Visible="true" runat="server" Text="Select User :"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlUser1" Visible="true" runat="server" AppendDataBoundItems="True" DataSourceID="usersds1" DataTextField="USER_NAME" DataValueField="USER_ID">
                                <asp:ListItem Value="0">-- Select / Show All --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="usersds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [USER_ID], [USER_NAME] FROM [YANTRA_USER_DETAILS] where EXPIRY_DATE >='2019-12-31 00:00:00.000' ORDER BY [USER_NAME]"></asp:SqlDataSource>
                        </td>
                        
                        <td>
                            No Of Records :
                <asp:DropDownList ID="ddlNoOfRecords" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged">
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>75</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                   
                </asp:DropDownList>

                        </td>
                        <td>
                            <asp:Button ID="btnShow1" runat="server" Visible="true" OnClick="btnShow1_Click" Text="Show" />
                            <asp:Button ID="btnsave" runat ="server" OnClick ="btnsave_Click" Text ="Status Update" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                                
                <br />

                <asp:GridView ID="GridView1" runat="server" Visible="true" AllowPaging="True" OnRowDataBound ="GridView1_RowDataBound" AutoGenerateColumns="False" DataKeyNames="logid" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging" Width="100%" DataSourceID  ="sdslog">
                    <Columns>
                        <asp:BoundField DataField="logid" HeaderText="logid" ReadOnly="True" SortExpression="logid" />
                        <asp:TemplateField HeaderText="Description" SortExpression="logdesc">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("logdesc") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("SO_NO") %>'></asp:Label>
                                &nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Bind("CUST_NAME", "{0}: ") %>'></asp:Label>
                                &nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Bind("logdesc") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item code - PO QTY" SortExpression="ITEM_MODEL_NO">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("ITEM_MODEL_NO") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label14" runat="server" Text='<%# Bind("ITEM_MODEL_NO", "{0} - ") %>'></asp:Label>
                                &nbsp;<asp:Label ID="Label12" runat="server" Text='<%# Bind("SO_DEt_QTY") %>'></asp:Label>
                                &nbsp;<%--<asp:Label ID="Label11" runat="server" Text='<%# Bind("logdesc") %>'></asp:Label>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category Name" SortExpression="logcatename">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("logcatename") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("logcatename") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date Of Change"  SortExpression="dt_added">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox5" runat="server"  Text='<%# Bind("dt_added","{0:dd/MM/yyyy}") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server"  Text='<%# Bind("dt_added","{0:dd/MM/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" SortExpression="Status">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="txtStatus" TextMode="SingleLine" Width="100px" Text='<%#Eval("logtypeid")%>' runat="server"></asp:TextBox>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkhdr" runat="server" AutoPostBack="True" OnClick="selectAll(this)" />
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="Chk" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource runat ="server" ID="sdslog" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand ="select *  from log_details_tbl1 inner join log_cate_tbl1 on log_details_tbl1 .logcateid =log_cate_tbl1 .logcateid  inner join YANTRA_SO_DET  on log_details_tbl1.User_Id=YANTRA_SO_DET .so_det_id  inner join YANTRA_SO_MAST on YANTRA_SO_DET .SO_ID =YANTRA_SO_MAST .SO_ID inner join YANTRA_ITEM_MAST on YANTRA_SO_DET .ITEM_CODE =YANTRA_ITEM_MAST .ITEM_CODE inner join YANTRA_CUSTOMER_MAST on YANTRA_SO_MAST .SO_CUST_ID =YANTRA_CUSTOMER_MAST.CUST_ID where log_details_tbl1.logcateid =141 order by log_details_tbl1.dt_added desc" SelectCommandType ="Text" >

                </asp:SqlDataSource>
               
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>


 
