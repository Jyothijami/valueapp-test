<%@ Page Title="" Language="C#" MasterPageFile="~/dev_pages/MobileApp.master" AutoEventWireup="true" CodeFile="DailyReport.aspx.cs" Inherits="dev_pages_DailyReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <script lang="javascript" type="text/javascript">
        function check(e) {
            var keynum
            var keychar
            var numcheck
            // For Internet Explorer
            if (window.event) {
                keynum = e.keyCode;
            }
                // For Netscape/Firefox/Opera
            else if (e.which) {
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);
            //List of special characters you want to restrict
            if (keychar == "'" || keychar == "`" || keychar == "&" || keychar == "¬" || keychar == '"') {
                return false;
            } else {
                return true;
            }
        }
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <asp:UpdatePanel ID="UpdatePanel34" runat="server">
        <ContentTemplate>
            <div class="page-header">
                <div class="page-title">
                    <h3>Daily Report</h3>
                </div>
            </div>
            <div class="breadcrumb-line">
                <ul class="breadcrumb">
                    <li><a href="MobileHome.aspx">Menu</a></li>
                  
                    <li><a href="DailyReport.aspx">Daily Report</a></li>
                    <li class="active"><a href="ToDoList1.aspx">To Do List</a></li>
                    <li class="active"><a href="Emp_CR.aspx">Complaint Register</a></li>
                    <li class="active"><a href="DataView.aspx">ToDo List View</a></li>


                </ul>
            </div>



            <div class="panel panel-default">
                 <div class="panel-heading">
                    <h6 class="panel-title">Daily Report Details</h6>
                </div>
                <div class="panel-body">
                <div class="form-group">
                <div class="row">

                    <div class="col-md-1"></div>
                    <div class="col-md-5">
                        <asp:Label ID="Label1" runat ="server"  Text ="Date & Time" ></asp:Label>
                         <asp:TextBox ID="txtDateTime" runat ="server" CssClass="form-control" type="datepic"></asp:TextBox>
                    </div>
                   
                </div>
                <div class="row">
                        <div class="col-md-1"></div>
                         <div class="col-md-5">
                        <asp:Label ID="Label2" runat ="server"  Text ="In Time" ></asp:Label>
                         <table style="width: 64px">
                                        <tr>
                                            <td style="width: 100px">
                                                <asp:DropDownList
                                                    ID="ddlHour" runat="server" CssClass="form-control" TabIndex="3" Width="60px" EnableTheming="False">
                                                    <asp:ListItem>1</asp:ListItem>
                                                    <asp:ListItem>2</asp:ListItem>
                                                    <asp:ListItem>3</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                    <asp:ListItem>5</asp:ListItem>
                                                    <asp:ListItem>6</asp:ListItem>
                                                    <asp:ListItem>7</asp:ListItem>
                                                    <asp:ListItem>8</asp:ListItem>
                                                    <asp:ListItem>9</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>11</asp:ListItem>
                                                    <asp:ListItem>12</asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td style="width: 100px">
                                                <asp:DropDownList ID="ddlMin" runat="server" CssClass="form-control" TabIndex="4" Width="60px" EnableTheming="False">
                                                    <asp:ListItem>00</asp:ListItem>
                                                    <asp:ListItem>05</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>25</asp:ListItem>
                                                    <asp:ListItem>30</asp:ListItem>
                                                    <asp:ListItem>35</asp:ListItem>
                                                    <asp:ListItem>40</asp:ListItem>
                                                    <asp:ListItem>45</asp:ListItem>
                                                    <asp:ListItem>50</asp:ListItem>
                                                    <asp:ListItem>55</asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td style="width: 100px">
                                                <asp:DropDownList ID="ddlAMPM" runat="server" CssClass="form-control" TabIndex="5" Width="60px" EnableTheming="False">
                                                    <asp:ListItem>AM</asp:ListItem>
                                                    <asp:ListItem>PM</asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>
                                    </table>
                    </div>
                        <div class="col-md-5">
                        <asp:Label ID="Label3" runat ="server"  Text ="Out Time" ></asp:Label>
                            <table style="width: 64px; height: 32px;">
                                        <tr>
                                            <td style="width: 100px">
                                                <asp:DropDownList
                                                    ID="ddlOutHour" runat="server" CssClass="form-control" TabIndex="3" Width="60px" EnableTheming="False">
                                                    <asp:ListItem>1</asp:ListItem>
                                                    <asp:ListItem>2</asp:ListItem>
                                                    <asp:ListItem>3</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                    <asp:ListItem>5</asp:ListItem>
                                                    <asp:ListItem>6</asp:ListItem>
                                                    <asp:ListItem>7</asp:ListItem>
                                                    <asp:ListItem>8</asp:ListItem>
                                                    <asp:ListItem>9</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>11</asp:ListItem>
                                                    <asp:ListItem>12</asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td style="width: 100px">
                                                <asp:DropDownList ID="ddlOutMin" runat="server" CssClass="form-control" TabIndex="4" Width="60px" EnableTheming="False">
                                                    <asp:ListItem>00</asp:ListItem>
                                                    <asp:ListItem>05</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>25</asp:ListItem>
                                                    <asp:ListItem>30</asp:ListItem>
                                                    <asp:ListItem>35</asp:ListItem>
                                                    <asp:ListItem>40</asp:ListItem>
                                                    <asp:ListItem>45</asp:ListItem>
                                                    <asp:ListItem>50</asp:ListItem>
                                                    <asp:ListItem>55</asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td style="width: 100px">
                                                <asp:DropDownList ID="ddlOutAMPM" runat="server" CssClass="form-control" TabIndex="5" Width="60px" EnableTheming="False">
                                                    <asp:ListItem>AM</asp:ListItem>
                                                    <asp:ListItem>PM</asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>
                                    </table>
                        </div>
                    </div>
               <div class="row">
                   <div class="col-md-1"></div>
                   <div class="col-md-5">
                        <asp:Label ID="Label4" runat ="server"  Text ="Client's Name" ></asp:Label>
                         <asp:TextBox ID="txtClientsName" onkeypress="return check(event)" runat ="server" CssClass ="form-control"></asp:TextBox>
                    </div>
               </div>
               <div class="row">
                   <div class="col-md-1"></div>
                   <div class="col-md-5">
                        <asp:Label ID="Label5" runat ="server"  Text ="Phone No" ></asp:Label>
                         <asp:TextBox ID="txtPhoneNo" runat ="server" CssClass ="form-control"></asp:TextBox>
                    </div>
               </div>
                    <div class="row">
                   <div class="col-md-1"></div>
                   <div class="col-md-5">
                        <asp:Label ID="Label6" runat ="server"  Text ="Reference" ></asp:Label>
                        <asp:DropDownList runat ="server" ID="txtReference" CssClass ="form-control">
                            <asp:ListItem>Executive</asp:ListItem>
                            <asp:ListItem>Walk-In</asp:ListItem>
                            <asp:ListItem>Architect</asp:ListItem>
                            <asp:ListItem>Web Site</asp:ListItem>
                            <asp:ListItem>Exhibition</asp:ListItem>
                            <asp:ListItem>Existing Customer</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                        <div class="col-md-5">
                        <asp:Label ID="Label7" runat ="server"  Text ="Architect/Executive Name" ></asp:Label>
                         <asp:TextBox ID="txtArchitect" onkeypress="return check(event)" runat ="server" CssClass ="form-control"></asp:TextBox>
                            </div> 
               </div>
               <div class="row">
                        <div class="col-md-1"></div>
                   <div class="col-md-10">
                        <asp:Label ID="Label8" runat ="server"  Text ="Address" ></asp:Label>
                         <asp:TextBox ID="txtAddress" TextMode="MultiLine" onkeypress="return check(event)" runat ="server" CssClass ="form-control"></asp:TextBox>

                   </div>
                </div>
                <div class="row">
                        <div class="col-md-1"></div>
                   <div class="col-md-10">
                        <asp:Label ID="Label9" runat ="server"  Text ="Purpose" ></asp:Label>
                         <asp:TextBox ID="txtPurpose" TextMode="MultiLine" onkeypress="return check(event)" runat ="server" CssClass ="form-control"></asp:TextBox>

                   </div>
                </div>
                    <div class="row">
                        <div class="col-md-1"></div>
                   <div class="col-md-10">
                        <asp:Label ID="Label10" runat ="server"  Text ="Remarks" ></asp:Label>
                         <asp:TextBox ID="txtRemarks" TextMode="MultiLine" onkeypress="return check(event)" runat ="server" CssClass ="form-control"></asp:TextBox>

                   </div>
                </div>
                    <div class="row">
                        <div class="col-md-1"></div>
                   <div class="col-md-5">
                        <asp:Label ID="Label11" runat ="server"  Text ="Attended By" ></asp:Label>
                       <asp:DropDownList ID ="ddlAttendedBy" runat ="server" Enabled ="false"  CssClass ="form-control">

                       </asp:DropDownList>
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
                
            </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>

