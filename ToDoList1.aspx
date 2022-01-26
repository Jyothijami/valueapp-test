<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ToDoList1.aspx.cs" Inherits="ToDoList1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>To DO List</title>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.1/jquery.min.js"></script>
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../select/select2.css" rel="stylesheet" />
    <script src="../select/select2.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
   
            <div class="page-header">
                <div class="page-title">
                    <h3>To Do List</h3>
                </div>
            </div>
            <div class="breadcrumb-line">
                <ul class="breadcrumb">
                    <li><a href="dev_pages/DailyReport.aspx">Daily Report</a></li>
                    <li class="active"><a href="ToDoList1.aspx">To Do List</a></li>
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
                         <asp:TextBox ID="txtDateTime" runat ="server" CssClass="form-control" type="date"></asp:TextBox>
                    </div>
                    </div>
                    <div class ="row">
                        <div class="col-md-1"></div>
                        <div class="col-md-5">
                            <asp:Label ID="lbl2" runat ="server" Text ="Reporting To"></asp:Label>
                            <%-- <asp:DropDownList ID="ddluname1" runat="server" DataSourceID="usersds1" DataTextField="USER_NAME" DataValueField="USER_ID">
                                        </asp:DropDownList>--%>
                                        <select id="Books" class="form-control" runat="server"></select>

                                        <asp:SqlDataSource ID="usersds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [USER_ID], [USER_NAME] FROM [YANTRA_USER_DETAILS] where EXPIRY_DATE >='2019-12-31 00:00:00.000' ORDER BY [USER_NAME]"></asp:SqlDataSource>
                                        <asp:HiddenField ID="hffromuid1" runat="server" />


                                           <script>
                                               $(document).ready(function () {
                                                   $("#Books").select2({ placeholder: 'Find and Select Books' });
                                               });
                                               </script>
                        </div>
                    </div>
                    <div class="row">
                    <div class="col-md-1"></div>
                    <div class="col-md-10">
                        <asp:Label ID="Label2" runat ="server"  Text ="Subject" ></asp:Label>
                         <asp:TextBox ID="txtSubject" runat ="server" CssClass="form-control" TextMode ="MultiLine" ></asp:TextBox>
                    </div>
                    </div>
                    <div class="row">
                    <div class="col-md-1"></div>
                    <div class="col-md-10">
                        <asp:Label ID="Label3" runat ="server"  Text ="Task Activity Description" ></asp:Label>
                         <asp:TextBox ID="txtDescr" runat ="server" CssClass="form-control" TextMode ="MultiLine" ></asp:TextBox>
                    </div>
                    </div>
                    <div class="row">
                    <div class="col-md-1"></div>
                    <div class="col-md-5">
                        <asp:Label ID="Label4" runat ="server"  Text ="Task Activity Status" ></asp:Label>
                         <asp:DropDownList ID="ddlActivity" runat ="server" CssClass ="form-control" >
                                <asp:ListItem Value ="55" >To Do</asp:ListItem>
                                <asp:ListItem Value ="56" >In-Progress</asp:ListItem>
                                <asp:ListItem Value ="57">Completed</asp:ListItem>
                            </asp:DropDownList>
                    </div>
                    </div>
                </div>
                <div class="form-group">
                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-10">
                                <div class="form-actions text-right">
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-lg btn-danger " OnClick ="btnSave_Click" Text="Save" />
                                </div>
                            </div>
                            <div class="col-md-1"></div>
                        </div>
                    </div>
            </div>
       
    </div>
    </form>
</body>
</html>
