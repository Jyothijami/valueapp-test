<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DailyReportEdit.aspx.cs" Inherits="Modules_SM_DailyReportEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1, maximum-scale=1" />
    <title>Valueline</title>
      <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"  
type="text/javascript"></script>   
<!--include jQuery Validation Plugin-->  
<script src="http://ajax.aspnetcdn.com/ajax/jquery.validate/1.12.0/jquery.validate.min.js"  
type="text/javascript"></script>  
  
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.12/css/jquery.dataTables.min.css" type="text/css"/>

     <link href="/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/css/londinium-theme.css" rel="stylesheet" type="text/css" />
    <link href="/css/styles.css" rel="stylesheet" type="text/css" />
    <link href="/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="/css/select2.min.css" rel="stylesheet" type="text/css" />

       <link rel="stylesheet" href='https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.css'
        media="screen" />
   <%-- <link rel="stylesheet" href='https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.css'
        media="screen" />--%>
     <script type="text/javascript" src="https://code.jquery.com/jquery-1.10.2.js"></script>
     <script type="text/javascript" src="https://cdn.datatables.net/1.10.12/js/jquery.dataTables.min.js"></script>


       
  
     <script type="text/javascript">


         $(document).ready(function () {

             $(function () {
                 initdropdown();

             })

         });



         function initdropdown() {
             $('.select-full').select2();
             //$("#datatable-tasks").dataTable();

         }


    </script>


</head>
<body>
    <form id="form1" runat="server">
      <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
             <ControlBundles>
             <cc1:ControlBundle Name="Group2" />
             </ControlBundles>
         </cc1:ToolkitScriptManager>
      <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"  >
            <ContentTemplate>
        <script type="text/javascript">
            Sys.Application.add_load(initdropdown);
        </script>



                <div class="form-horizontal">
        <div class="panel panel-default">
            <div class="panel-heading">
                    <h6 class="panel-title">Daily Report Details</h6>
            </div>
            <div class="panel-body">
                             <div class="form-group">
                            <label class="col-sm-2 control-label text-right">Date & Time :<asp:Label
                                        ID="Label47" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label></label>
                            <div class="col-sm-4">
                                                 <asp:TextBox ID="txtDateTime" runat="server" CssClass="form-control" type="datepic"></asp:TextBox><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtDateTime"
                                            ErrorMessage="Please Select the  Date" ValidationGroup="a">*</asp:RequiredFieldValidator>
                            </div>
                           <label class="col-sm-2 control-label text-right">Daily Report Type :</label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlDRType" runat ="server" CssClass="form-control" >
                                    <asp:ListItem >Not Selected</asp:ListItem>
                                    <asp:ListItem >In-Showroom</asp:ListItem>
                                    <asp:ListItem >Site Visit</asp:ListItem>
                                    <%--<asp:ListItem >Follow Up</asp:ListItem>--%>
                                    <asp:ListItem >Phone Cordination</asp:ListItem>

                                    <asp:ListItem >Clients / Architect Ofc Visit</asp:ListItem>

                                </asp:DropDownList>  
                            </div>
                 </div>

                <div class="form-group">
                            <label class="col-sm-2 control-label text-right">Start Time :</label>
                            <div class="col-sm-4">
                                <table >
                                        <tr>
                                            <td style="width: 50px">
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
                                            <td style="width: 50px">
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
                                            <td style="width: 50px">
                                                <asp:DropDownList ID="ddlAMPM" runat="server" CssClass="form-control" TabIndex="5" Width="60px" EnableTheming="False">
                                                    <asp:ListItem>AM</asp:ListItem>
                                                    <asp:ListItem>PM</asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>
                                    </table>
                            </div>
                            <label class="col-sm-2 control-label text-right">End Time :</label>
                            <div class="col-sm-4">
                               <table >
                                        <tr>
                                            <td style="width: 50px">
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
                                            <td style="width: 50px">
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
                                            <td style="width: 50px">
                                                <asp:DropDownList ID="ddlOutAMPM" runat="server" CssClass="form-control" TabIndex="5" Width="60px" EnableTheming="False">
                                                    <asp:ListItem>AM</asp:ListItem>
                                                    <asp:ListItem>PM</asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>
                                    </table>
                            </div>
                        </div>
                
                <div class="form-group">
                            <label class="col-sm-2 control-label text-right">Client's Name :<asp:Label
                                        ID="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label></label>
                            <div class="col-sm-4">
                                  <asp:TextBox ID="txtClientsName" CssClass="form-control" onkeypress="return check(event)" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtClientsName"
                                            ErrorMessage="Please Enter Client Name">*</asp:RequiredFieldValidator>
                            </div>

                        <label class="col-sm-2 control-label text-right">Phone No :<asp:Label
                                        ID="Label13" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label></label>
                            <div class="col-sm-4">
                                 <asp:TextBox ID="txtPhoneNo" CssClass="form-control" runat="server">
                                    </asp:TextBox>
                                                           </div>
                           
                        </div>

                 <div class="form-group">
                            <label class="col-sm-2 control-label text-right">Reference :</label>
                            <div class="col-sm-4">
                            <%--<asp:TextBox ID="txtReference" onkeypress="return check(event)" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlreference" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>

                        <label class="col-sm-2 control-label text-right">Architect :</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtArchitect" CssClass="form-control" onkeypress="return check(event)" runat="server"></asp:TextBox>
                            </div>
                           
                        </div>

                    <div class="form-group">
                            <label class="col-sm-2 control-label text-right">Address :<asp:Label
                                        ID="Label12" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label></label>
                            <div class="col-sm-4">
                                    <asp:TextBox ID="txtAddress" runat="server" onkeypress="return check(event)" TextMode="MultiLine" CssClass="form-control" >
                                    </asp:TextBox><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAddress"
                                            ErrorMessage="Please Enter Address" ValidationGroup="a">*</asp:RequiredFieldValidator>
                            </div>
                            <label class="col-sm-2 control-label text-right">Email :</label>
                         <div class="col-sm-4">
                                <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                       
                           
                    </div>
  
                    <div class="form-group">

                         <label class="col-sm-2 control-label text-right">Purpose :<asp:Label
                                        ID="Label8" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label></label>
                            <div class="col-sm-7">
                                  <asp:TextBox ID="txtPurpose" runat="server" onkeypress="return check(event)" TextMode="MultiLine" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPurpose"
                                            ErrorMessage="Please Enter Purpose" ValidationGroup="a">*</asp:RequiredFieldValidator>
                            </div>

                        </div>


                    <div class="form-group">

                         <label class="col-sm-2 control-label text-right">Remarks :<asp:Label
                                        ID="Label9" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label></label>
                            <div class="col-sm-7">
                                <asp:TextBox ID="txtRemarks" runat="server" onkeypress="return check(event)" TextMode="MultiLine" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRemarks"
                                            ErrorMessage="Please Enter Remarks" ValidationGroup="a">*</asp:RequiredFieldValidator>
                            </div>

                        </div>
                    
                    
                    <div class="form-group">
                            <label class="col-sm-2 control-label text-right">Attended By :</label>
                            <div class="col-sm-4">
                             <asp:DropDownList ID="ddlAttendedBy" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlAttendedBy"
                                        ErrorMessage="Please Slelect Attended By" InitialValue="0">*</asp:RequiredFieldValidator>
                            </div>

                        <label class="col-sm-2 control-label text-right">Assisted By :</label>
                            <div class="col-sm-4">
                                 <asp:DropDownList ID="ddlBackup" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlBackup"
                                        ErrorMessage="Please Slelect Backup/Assisted By" InitialValue="0">*</asp:RequiredFieldValidator>
                            </div>
                           
                        </div>
                    <div class="form-group">

                                <div class="form-actions text-center">

                                   <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-danger" Text="Update" OnClick="btnUpdate_Click" ValidationGroup="a" />
                                </div>
                     </div>
                <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lbldt" runat="server" Visible="False"></asp:Label>

            </div>
        </div>
    </div>











                  </ContentTemplate>
        </asp:UpdatePanel>

    </form>



     <script type="text/ecmascript" src='<%= ResolveUrl("~/js/js/bootstrap.min.js") %>'></script>
   
    <script type="text/ecmascript" src='<%= ResolveUrl("~/js/js/plugins/interface/datatables.min.js") %>'></script>
    <script type="text/ecmascript" src='<%= ResolveUrl("~/js/js/plugins/forms/validate.min.js") %>'></script>
    <script type="text/ecmascript" src='<%= ResolveUrl("~/js/js/plugins/forms/select2.min.js") %>'></script>
    <script type="text/javascript" src='https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.js'> </script>
</body>
</html>
