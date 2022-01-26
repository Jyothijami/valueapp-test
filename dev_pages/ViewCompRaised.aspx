<%@ Page Title="" Language="C#" MasterPageFile="~/dev_pages/MPage1.master" AutoEventWireup="true" CodeFile="ViewCompRaised.aspx.cs" Inherits="dev_pages_ViewTicketsRaised" %>

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
            <td colspan="2" style="text-align: left">
            
                <asp:TextBox ID="txtDateWorked" CssClass="datetimetxt" Visible ="false"  runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: left">
           
                <asp:TextBox ID="txtClosed" CssClass="datetimetxt" Visible ="false"  runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <%--<asp:Button ID="btnPostComments" BackColor="SkyBlue" runat="server" Text="Post Comments"  />--%>
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

                       <asp:BoundField DataField ="Ticket_Id" HeaderText ="Id" />
                        <asp:BoundField DataField="Date_Created" HeaderText ="Date Created"/>
                        <asp:BoundField DataField="Date_Closed" HeaderText ="Referred Date"/>
                        <asp:BoundField DataField="Incident_Type" HeaderText="Referred Time"/>
                        <asp:BoundField DataField="User_Affected" HeaderText="Complaint Type"/>
                        <asp:BoundField DataField ="ClientName" HeaderText ="Client Name" />
                        <asp:BoundField DataField="Email" HeaderText ="Contact Person"/>
                        <asp:BoundField DataField="Created_For" HeaderText ="Contact Number"/>
                        <asp:BoundField DataField="Address" HeaderText="Address"/>
                        <asp:BoundField DataField="Summary" HeaderText="Summary"/>
                        <asp:BoundField DataField="Description" HeaderText="Nature of Complaint"/>
                        <asp:BoundField DataField="Comments" HeaderText="Raised By"/>
                        <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Raised To"/>

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
                    SelectCommand="select Ticket_Id,Date_Created,Date_Closed,Incident_Type,User_Affected,ClientName,email,EMP_FIRST_NAME,Created_For,Address,Summary,Description, Comments from Emp_TicketRaised_tbl, YANTRA_EMPLOYEE_MAST where Emp_TicketRaised_tbl .CreatedId =YANTRA_EMPLOYEE_MAST .EMP_ID
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

