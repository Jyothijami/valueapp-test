<%@ Page Title="" Language="C#" MasterPageFile="~/dev_pages/MPage1.master" AutoEventWireup="true" CodeFile="ToDoList.aspx.cs" Inherits="dev_pages_ToDoList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.1/jquery.min.js"></script>
    
    <link href="../select/select2.css" rel="stylesheet" />
    <script src="../select/select2.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel34" runat="server">
        <ContentTemplate>
            <div class="page-header">
                <div class="page-title">
                    <h3>To Do List</h3>
                </div>
            </div>
            <div class="breadcrumb-line">
                <ul class="breadcrumb">
                    <li><a href="DailyReport.aspx">Daily Report</a></li>
                    <li class="active"><a href="ToDoList.aspx">To Do List</a></li>
                </ul>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h6 class="panel-title">To Do List Details</h6>
                </div>
                <div class="panel-body">
                    <div class="row">

                    <div class="col-md-1"></div>
                    <div class="col-md-5">
                        <asp:Label ID="Label1" runat ="server"  Text ="Date & Time" ></asp:Label>
                         <asp:TextBox ID="txtDateTime" runat ="server" CssClass="form-control" type="datepic"></asp:TextBox>
                    </div>
                    </div>
                    <div class ="row">
                        <div class="col-md-1"></div>
                        <div class="col-md-5">
                            <asp:Label ID="lbl2" runat ="server" Text ="Reporting To"></asp:Label>
                             <asp:DropDownList ID="ddluname1" runat="server" DataSourceID="usersds1" DataTextField="USER_NAME" DataValueField="USER_ID">
                                        </asp:DropDownList>
                                        <select id="Books" style="width:300px" runat="server"></select>

                                        <asp:SqlDataSource ID="usersds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [USER_ID], [USER_NAME] FROM [YANTRA_USER_DETAILS] where EXPIRY_DATE >='2019-12-31 00:00:00.000' ORDER BY [USER_NAME]"></asp:SqlDataSource>
                                        <asp:HiddenField ID="hffromuid1" runat="server" />


                                           <script>
                                               $(document).ready(function () {
                                                   $("#Books").select2({ placeholder: 'Find and Select Books' });
                                               });
                                               </script>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

