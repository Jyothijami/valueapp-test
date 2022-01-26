<%@ Page Title="" Language="C#" MasterPageFile="~/dev_pages/MobileApp.master" AutoEventWireup="true" CodeFile="Emp_CR.aspx.cs" Inherits="dev_pages_Emp_CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style>
        .table {
            width: 50%;
        }

        .dropdown {
            width: 200px;
            height: 25px;
        }

        .textbox {
            width: 195px;
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updatepanel1" runat ="server"  >
        <ContentTemplate >
<div class="breadcrumb-line">
                <ul class="breadcrumb">
                    <li><a href="MobileHome.aspx">Menu</a></li>
                  
                    <li><a href="DailyReport.aspx">Daily Report</a></li>
                    <li class="active"><a href="ToDoList1.aspx">To Do List</a></li>
                    <li class="active"><a href="Emp_CR.aspx">Complaint Register</a></li>
                    <li class="active"><a href="DataView.aspx">ToDo List View</a></li>

                </ul>
            </div>
       
    <table class="table">

        <tr>
            <td >Complaint Type :</td>
            <td>
                <asp:DropDownList ID="ddlUserAffctd" CssClass="dropdown" runat="server" >
                    <asp:ListItem>--Select--</asp:ListItem>
                    <asp:ListItem>Complaint</asp:ListItem>
                    <asp:ListItem>Enquiry</asp:ListItem>
                    <asp:ListItem>Guidence</asp:ListItem>
                    <asp:ListItem>Installation</asp:ListItem>
                    <asp:ListItem>AMC</asp:ListItem>
                    <asp:ListItem>Others</asp:ListItem>
                    
                </asp:DropDownList>
            </td>
            
        </tr>
        <tr>
            <td>Region :</td>
            <td>
                <asp:DropDownList ID="ddlRegion" CssClass="dropdown" AutoPostBack ="True" OnSelectedIndexChanged ="ddlRegion_SelectedIndexChanged" runat="server">
                    <asp:ListItem Value ="NotSelected">--Select--</asp:ListItem>
                    <%--<asp:ListItem>All</asp:ListItem>
                    <asp:ListItem>Bangalore</asp:ListItem>
                    <asp:ListItem>Chennai</asp:ListItem>
                    <asp:ListItem>Hyderabad</asp:ListItem>
                    <asp:ListItem>Kerala</asp:ListItem>
                    <asp:ListItem>Vizag</asp:ListItem>--%>
                </asp:DropDownList>
                &nbsp;<asp:Label ID="Label1" runat="server" EnableTheming="False" Font-Bold="False"
                                                    Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>&nbsp;<asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlRegion"
                                                        ErrorMessage="Please Enter the Region First" InitialValue="0" ValidationGroup="main">*</asp:RequiredFieldValidator>
        
            </td>
        </tr>
        <tr>
            <td>Created For :</td>
            <td>
                
                <asp:DropDownList ID="ddlCreatedFor" CssClass="dropdown" AutoPostBack ="true"  runat ="server" OnSelectedIndexChanged ="ddlCreatedFor_SelectedIndexChanged">
                    <asp:ListItem Value ="NotSelected">--Select--</asp:ListItem>
                </asp:DropDownList>
                &nbsp;<asp:Label ID="Label10" runat="server" EnableTheming="False" Font-Bold="False"
                                                    Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>&nbsp;<asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCreatedFor"
                                                        ErrorMessage="Please Enter the Created For" InitialValue="0" ValidationGroup="main">*</asp:RequiredFieldValidator>
                  
            </td>
            
        </tr>
        <tr>
            <td>Assigned Mobile No :</td>
            <td>
                <asp:TextBox ID="txtMobile" placeholder="Mobile  No" runat="server"></asp:TextBox>
            </td>
           
        </tr>
        <tr>
            <td>Complaint Referred Date&Time:</td>
            <td>
                <asp:TextBox ID="txtDate" runat ="server" type="date" ></asp:TextBox>
                <asp:TextBox id="txtTime" runat ="server" ></asp:TextBox>
                <%--<asp:DropDownList ID="ddlIncidentType" CssClass="dropdown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlIncidentType_SelectedIndexChanged">
                    <asp:ListItem>--Select--</asp:ListItem>
                    <asp:ListItem>Application</asp:ListItem>
                    <asp:ListItem>Software</asp:ListItem>
                    <asp:ListItem>Hardware</asp:ListItem>
                    <asp:ListItem>Security</asp:ListItem>
                    <asp:ListItem>Any Others</asp:ListItem>
                </asp:DropDownList>--%>
            </td>
            
        </tr>
        <tr>
            <td>Client Name:</td>
            <td><asp:TextBox id="txtClientName" runat ="server" placeholder="Client Name"></asp:TextBox></td>
        </tr>
        <tr>
             <td>Contact Person :</td>
            <td><asp:TextBox ID="txtEmail" placeholder="Contact Person" runat="server"></asp:TextBox></td>
        </tr>
        <tr><td>Email :</td>
            <td><asp:TextBox ID="txtEmail1" runat ="server" placeholder="Email Id" ></asp:TextBox></td>
        </tr>
        <tr>
            <td>Contact Number:</td>
            <td><asp:TextBox ID="txtCreatedFor" placeholder="Contact Number" runat="server"></asp:TextBox></td>
        </tr>
        
        <tr>
            <td>Client Address:</td>
            <td>
                <asp:TextBox ID="txtModule" placeholder="SM/Purchases etc."  Visible ="false" runat="server"></asp:TextBox>
                 <asp:TextBox ID="txtAddress" placeholder="Customer Address" TextMode="MultiLine" Width="500px"  runat="server"></asp:TextBox>
            </td>
        </tr>
        
        <%--<tr>
            <td>Urgency Level:</td>
            <td>
                <asp:DropDownList ID="ddlUrgencyLevel" CssClass="dropdown" runat="server" OnSelectedIndexChanged="ddlIncidentType_SelectedIndexChanged">
                    <asp:ListItem>--Select--</asp:ListItem>
                    <asp:ListItem>Low</asp:ListItem>
                    <asp:ListItem>Medium</asp:ListItem>
                    <asp:ListItem>High</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>--%>

        <tr>
            <td>Summary:</td>
            <td>
                <asp:TextBox ID="txtSummary" placeholder="Summay" Width="500px" Height="25px" runat="server"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td>Nature of Complaint:</td>
            <td>
                <asp:TextBox ID="txtDescription" placeholder="Nature of Complaint" TextMode="MultiLine" Width="500px" Height="165px" runat="server"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td>Attachments :</td>
            <td>
                <asp:FileUpload ID="requestAttachements" runat="server" AllowMultiple="true" />
            </td>
        </tr>
        
        <%--<tr>
            <td>Assigned Email ID:</td>
            <td>
                <asp:TextBox ID="txtEmail" placeholder="Email" CssClass="textbox" runat="server"></asp:TextBox>
            </td>
        </tr>--%>
        
        <tr>
            <td>Prepared By:</td>
            <td><asp:DropDownList ID="ddlPreparedBy" runat ="server" Enabled ="false"  ></asp:DropDownList></td>
        </tr>
        <tr>
            <td></td>
            <td style="text-align: left">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" BackColor="#66FF66" OnClick="btnSubmit_Click" Style="font-weight: 700" />&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" BackColor="#FF6666" OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>
    <%--<br />--%>
   <%-- <p style="color: brown; text-align: center;">
        *** Date Created and Date Worked will be by defaut "1/1/1900 12:00:00 AM" and will be updated as soon as the admin works on the Request.***
    </p>--%>
   <%-- <br />
    <table>
        <tr>
            <td>
                <p>Total No Of Complaints Raised : &nbsp;&nbsp;</p>
            </td>
            <td>
                <asp:Label ID="lblTotalTicketsRaised" Font-Bold="true" runat="server"></asp:Label>

            </td>
        </tr>
    </table>
    <br />--%>
<%--    <table style="width: 100%">
        <tr>
            <td>
                <asp:GridView ID="gvServiceRequests" Visible ="false" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="Ticket_Id" DataSourceID="SqlDataSource1" OnRowDataBound="gvServiceRequests_RowDataBound" PageSize="100">
                    <Columns>
                        <asp:BoundField DataField="Ticket_Id" HeaderText="Ticket_Id" ReadOnly="True" SortExpression="Ticket_Id" />
                        <asp:BoundField DataField="Date_Created" HeaderText="Date_Created" SortExpression="Date_Created" />
                        <%--<asp:BoundField DataField="User_Affected" HeaderText="User_Affected" SortExpression="User_Affected" />
                        <asp:BoundField DataField="Created_For" HeaderText="Created_For" SortExpression="Created_For" />--%>
                        <%--<asp:BoundField DataField="Region" HeaderText="Region" SortExpression="Region" />--%>
                        <%--<asp:BoundField DataField="Process_Module" HeaderText="Module" SortExpression="Process_Module" />--%>
                        <%--<asp:BoundField DataField="Incident_Type" HeaderText="Incident_Type" SortExpression="Incident_Type" />--%>
                        
                       <%-- <asp:BoundField DataField="Summary" HeaderText="Summary" SortExpression="Summary" />
                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                        <%--<asp:BoundField DataField="Date_Worked" HeaderText="Date_Worked" SortExpression="Date_Worked" />--%>
                        <%--<asp:BoundField DataField="Date_Closed" HeaderText="Date_Closed" SortExpression="Date_Closed" />--%>
                        <%--<asp:BoundField DataField="Comments" HeaderText="Comments" SortExpression="Comments" />

                    </Columns>
                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" ForeColor="#330099" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                    <SortedAscendingCellStyle BackColor="#FEFCEB" />
                    <SortedAscendingHeaderStyle BackColor="#AF0101" />
                    <SortedDescendingCellStyle BackColor="#F6F0C0" />
                    <SortedDescendingHeaderStyle BackColor="#7E0000" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT * FROM [emp_TicketRaised_tbl] order by date_created desc"></asp:SqlDataSource>
            </td>
        </tr>
    </table>--%>
             </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

