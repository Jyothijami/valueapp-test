<%@ Page Title="" Language="C#" MasterPageFile="~/dev_pages/MPage1.master" AutoEventWireup="true" CodeFile="ViewTicketsRaised.aspx.cs" Inherits="dev_pages_ViewTicketsRaised" %>

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
    <table style="width: 100%">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Raised Tickets" Font-Size="X-Large" Font-Underline="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: left">Date Worked : 
            
                <asp:TextBox ID="txtDateWorked" CssClass="datetimetxt" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: left">Date Closed : &nbsp;
           
                <asp:TextBox ID="txtClosed" CssClass="datetimetxt" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnPostComments" BackColor="SkyBlue" runat="server" Text="Post Comments" OnClick="btnPostComments_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                
                <asp:HyperLink ID="HyperLink1" runat="server" Font-Underline="True" NavigateUrl="~/Content/ServiceRequest/" Target="_blank">View Attachments</asp:HyperLink>

            </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>

        <tr>
            <td>
                <asp:GridView ID="gvRaisedTickets" runat="server" AllowPaging="True" AllowSorting="True" 
                    AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="4" DataKeyNames="Ticket_Id" DataSourceID="SqlDataSource1" 
                    OnRowDataBound="gvRaisedTickets_RowDataBound" PageSize="100">
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

                        <asp:TemplateField HeaderText="Ticket_Id" SortExpression="Ticket_Id">

                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbtnTicketID" ForeColor="#0066ff" Text='<%#Eval("Ticket_Id")%>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>

                        <%--<asp:BoundField DataField="Urgency_Level" HeaderText="Urgency_Level" SortExpression="Urgency_Level" />--%>
                        <%--<asp:BoundField DataField="Date_Created" HeaderText="Date_Created" SortExpression="Date_Created" />--%>
                        <%--<asp:BoundField DataField="Date_Worked" HeaderText="Date_Worked" SortExpression="Date_Worked" />--%>
                        <asp:BoundField DataField="Summary" HeaderText="Summary" SortExpression="Summary" />
                        <asp:TemplateField HeaderText="Description" SortExpression="Description">

                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDescription" ForeColor="#0066ff" Width="400px" Text='<%#Eval("Description")%>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status" SortExpression="Status">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="txtStatus" TextMode="SingleLine" Width="100px" Text='<%#Eval("Status")%>' runat="server"></asp:TextBox>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Comments" SortExpression="Comments">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="txtComment" TextMode="MultiLine" Width="200px" Text='<%#Eval("Comments")%>' runat="server"></asp:TextBox>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Date_Closed" HeaderText="Date_Closed" SortExpression="Date_Closed" />

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
                    SelectCommand="SELECT [Ticket_Id], [Status], [Date_Worked], [Date_Closed], [User_Affected], [Summary],
                     [Description], [Urgency_Level], [Comments], [Date_Created] FROM [Service_Requests_tbl] 
                    order by [Date_Created] desc"></asp:SqlDataSource>
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

