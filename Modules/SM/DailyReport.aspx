<%@ Page Language="C#" MasterPageFile="~/MPs/SalesMP1.master" AutoEventWireup="true" CodeFile="DailyReport.aspx.cs"
    Inherits="Modules_SM_DailyReport" Title="|| Value App : SM : Daily Report ||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

   <%--  <asp:UpdatePanel runat ="server"  >
        <ContentTemplate >--%>

      <script type="text/javascript">
        $(function () {
            var date = new Date();
            var currentMonth = date.getMonth();
            var currentDate = date.getDate();
            var currentYear = date.getFullYear();
            $('input[type=date2]').datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd/mm/yy',
                //minDate: new Date(currentYear, currentMonth, currentDate)

                minDate: -1

            });
        });





        //function chkNext2Days(selectedDate) {
        //    var today = new Date();
        //    if (isEqual(selectedDate, today)) return [false, '', ''];
        //    today.setDate(today.getDate() + 1);
        //    if (isEqual(selectedDate, today)) return [false, '', ''];
        //    today.setDate(today.getDate() + 1);
        //    if (isEqual(selectedDate, today)) return [false, '', ''];
        //    return [true, '', ''];
        //}



    </script>
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
        <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.1/jquery.min.js"></script>--%>
    <link href="../../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../select/select2.css" rel="stylesheet" />
    <script src="../../select/select2.js"></script>
    <%-- <link href="../../css/londinium-theme.css" rel="stylesheet" type="text/css" />--%>

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




           <script>

            $(document).ready(function () {
                $('#<%=Books.ClientID%>').select2({ placeholder: 'Find and Select Books' });


             //$("#ContentPlaceHolder1_Books").select2({ placeholder: 'Find and Select Books' });


         });


         Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
         function EndRequestHandler(sender, args) {
             //Binding Code Again
             $(<%=Books.ClientID%>).select2({ placeholder: 'Find and Select Books' });
              }




         </script>



            <div class="form-horizontal">


                <div class="page-header">
                    <div class="page-title">
                        <h3>Daily Report</h3>
                    </div>
                </div>

                <!-- Breadcrumbs line -->
                <div class="breadcrumb-line">
                    <ul class="breadcrumb">
                        <li><a href="../SM/DailyReportView.aspx">DailyReport View</a></li>
                        <li class="active">
                            <asp:LinkButton ID="lnkPOAmen" runat="server" OnClick="lnkPOAmen_Click" >Daily Report</asp:LinkButton></li>
                        <li>
                            <asp:LinkButton ID="lnkSalesReturn" runat="server" OnClick="lnkSalesReturn_Click1" CausesValidation="false" >To Do List</asp:LinkButton></li>
                    </ul>
                </div>
                <!-- /breadcrumbs line -->


             



  
       <%--     <table style ="width :100%">
        <tr>
                    <td style="text-align: center; font-size: medium;"><a href="../SM/DailyReportView.aspx" >DailyReport View</a>
                       &nbsp;||&nbsp; <asp:LinkButton ID="lnkPOAmen" runat="server" OnClick="lnkPOAmen_Click" Font-Underline="True">Daily Report</asp:LinkButton>
                        &nbsp;||&nbsp;
                        <asp:LinkButton ID="lnkSalesReturn" runat="server" OnClick="lnkSalesReturn_Click1" CausesValidation ="false" Font-Underline="True">To Do List</asp:LinkButton>
                    
                    </td>
                </tr>
    </table>--%>
   

      
            
            <asp:Panel runat="server" ID="POAmendment"  CssClass="panel panel-default" >


                <div class="panel-heading">
                    <h6 class="panel-title">Daily Report Details</h6>
                </div>

                <div class="panel-body">

                <div class="form-group">
                            <label class="col-sm-2 control-label text-right">Date & Time :<asp:Label
                                        ID="Label47" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label></label>
                            <div class="col-sm-4">
                                                 <asp:TextBox ID="txtDateTime" runat="server" CssClass="form-control" type="date2"></asp:TextBox><asp:RequiredFieldValidator
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
                                    </asp:TextBox><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPhoneNo"
                                            ErrorMessage="Please Enter PhoneNo" ValidationGroup="a">*</asp:RequiredFieldValidator>
                                    <cc1:FilteredTextBoxExtender ID="ftxteOtherCorpPhoneNo" runat="server" Enabled="True"
                                        TargetControlID="txtPhoneNo" ValidChars="-0123456789(),">
                                    </cc1:FilteredTextBoxExtender> 
                                                           </div>
                           
                        </div>

                 <div class="form-group">
                            <label class="col-sm-2 control-label text-right">Reference :</label>
                            <div class="col-sm-4">
                            <%--<asp:TextBox ID="txtReference" onkeypress="return check(event)" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlreference" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>

                        <label class="col-sm-2 control-label text-right">Architect/Executive :</label>
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
                       
                           <label class="col-sm-1 control-label text-right">Plan of Action :<asp:Label
                                        ID="Label3" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label></label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID ="ddlAction" runat ="server" CssClass="form-control" >
                                    <asp:ListItem >Not Selected</asp:ListItem>

                                    <asp:ListItem >Phone Cordination</asp:ListItem>
                                     <asp:ListItem >Catalogue to be shared</asp:ListItem>
                                    <asp:ListItem >Quotation to be sent</asp:ListItem>
                                    <asp:ListItem >Follow-up</asp:ListItem>
                                    <asp:ListItem >Dispatch/Delivery Instruction</asp:ListItem>
                                    <asp:ListItem >Payment Related</asp:ListItem>

                                </asp:DropDownList>
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

                         <label class="col-sm-2 control-label text-right" >What did you achive Yesterday ? :<asp:Label
                                        ID="Label1" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label></label>
                            <div class="col-sm-7">
                                <asp:TextBox ID="txtAchiveYesterday" runat="server" onkeypress="return check(event)" TextMode="MultiLine" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtAchiveYesterday"
                                            ErrorMessage="Please Enter Yesterday Achivements" ValidationGroup="a">*</asp:RequiredFieldValidator>
                            </div>

                        </div>
                    <div class="form-group">

                         <label class="col-sm-2 control-label text-right"  >What do you plan to achive Today ? :<asp:Label
                                        ID="Label2" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label></label>
                            <div class="col-sm-7">
                                <asp:TextBox ID="txtachiveToday" runat="server" onkeypress="return check(event)" TextMode="MultiLine" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtachiveToday"
                                            ErrorMessage="Please Enter Today Achivements" ValidationGroup="a">*</asp:RequiredFieldValidator>
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
                  
                      <div class ="form-group ">
                        <label class="col-sm-2 control-label text-right">Requirement For :</label>

                        <div class="col-sm-4">
                            <asp:CheckBoxList ID="CheckBoxList1" ClientIDMode="AutoID" runat="server" CssClass="checkbox-inline" OnSelectedIndexChanged ="CheckBoxList1_SelectedIndexChanged" AutoPostBack ="True" 
                             RepeatColumns="2" RepeatDirection="Horizontal" >
                                <asp:ListItem >Spares</asp:ListItem>
                                    </asp:CheckBoxList>
                        </div>

                           <label class="col-sm-2 control-label text-right">Looking For :</label>
                             <div class="col-sm-4">
                                <asp:ListBox ID="ListBox1" runat="server" CssClass ="form-control" Rows="7" SelectionMode="Multiple"></asp:ListBox>
                       <asp:Label ID="lblLstselect" runat ="server" CssClass="caption text-danger" Text ="Please Select Required Multiple Brands By Clicking Ctrl + Mouse Click" ></asp:Label>
                      
                        </div>
                       
                    </div>


                    <div class="form-group">
                        
                    </div>




                    <div class="form-group">
                            <label class="col-sm-2 control-label text-right"></label>
                            <div class="col-sm-6">
                           <asp:FileUpload ID="Uploadattach" runat="server" Visible ="false"  AllowMultiple="true" />
                                    <asp:Label ID="txtBrowsetxt" runat ="server" Text="If you have any attachments for this dailyreport please upload after saving this record (from dailyreport view documnets section)"  ForeColor ="Red"></asp:Label>
                                    <asp:Label ID="lblAtt" runat ="server" Visible ="false" ></asp:Label>
                            </div>
                           
                        </div>

                    
                    
                    <div class="form-group">

                                <div class="form-actions text-center">

                                   <asp:Button ID="btnADD" runat="server" CssClass="btn btn-danger" Text="Add" OnClick="btnADD_Click" ValidationGroup="a" />
                                </div>
                     </div>

                    <div class="form-group">

                        <asp:GridView ID="gvDailyReport" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvDailyReport_RowDataBound" OnRowDeleting="gvDailyReport_RowDeleting" OnRowEditing="gvDailyReport_RowEditing" Width="100%">
                                        <Columns>
                                            <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                            <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                            <asp:BoundField DataField="CLIENTSNAME" HeaderText="Client's Name">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DATETIME" HeaderText="Date Time">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PURPOSE" HeaderText="Purpose">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="REMARKS" HeaderText="Remarks">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ATTENDEDBY" HeaderText="Attended By Id">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EMPLOYEENAME" NullDisplayText="-" HeaderText="Prepared By Id"></asp:BoundField>
                                            <asp:BoundField DataField="ATTENDEDBYNAME" HeaderText="Attended By"></asp:BoundField>
                                            <asp:BoundField DataField="EMPNAMEFORSHOW" HeaderText="Excutive Name"></asp:BoundField>
                                            <asp:BoundField DataField="HOUR" HeaderText="In Hour"></asp:BoundField>
                                            <asp:BoundField DataField="MIN" HeaderText="In Min"></asp:BoundField>
                                            <asp:BoundField DataField="AMPM" HeaderText="In AMPM"></asp:BoundField>
                                            <asp:BoundField DataField="ADDRESS" HeaderText="Address"></asp:BoundField>
                                            <asp:BoundField DataField="PHONE" HeaderText="Phone"></asp:BoundField>
                                            <asp:BoundField DataField="ref" HeaderText="Reference"></asp:BoundField>
                                            <asp:BoundField DataField="arch" HeaderText="Architect"></asp:BoundField>
                                            <asp:BoundField DataField="OutHOUR" HeaderText="Out Hour"></asp:BoundField>
                                            <asp:BoundField DataField="OutMIN" HeaderText="Out Min"></asp:BoundField>
                                            <asp:BoundField DataField="OutAMPM" HeaderText="Out AMPM"></asp:BoundField>
                                            <asp:BoundField DataField ="UploadDoc" HeaderText ="Attachmnets" />
                                            <asp:BoundField DataField ="BackupName" HeaderText ="BackupName" />
                                            <asp:BoundField DataField ="BackupID" HeaderText ="BackupId" />
                                            <asp:BoundField DataField ="DRType" HeaderText ="DrType" />
                                            <asp:BoundField DataField ="Email" HeaderText ="Email" />

                                        </Columns>
                                        <EmptyDataTemplate>
                                            <span style="color: #ff0033">No Data to Display</span>
                                        </EmptyDataTemplate>
                                    </asp:GridView>

                    </div>


                    <div class="form-group">
                            <label class="col-sm-2 control-label text-right">Excutive Name :</label>
                            <div class="col-sm-4">
                          <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                        </asp:DropDownList>
                            </div>
                           
                        </div>


                    <div class="form-actions text-right">
                                <%--<asp:Button ID="btnPrint" runat="server" CssClass="btn btn-warning" Text="Print" />--%>
                                  <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" CausesValidation="false"  />
                                <asp:Button ID="btnRefresh" runat="server" CssClass="btn btn-danger" Text="Refresh" OnClick="btnRefresh_Click" CausesValidation="False" />
                       </div>

                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ValidationGroup="a"></asp:ValidationSummary>

   </div>
            <table width="100%">
             
                <tr>
                    <td colspan="3">
                        <table style="text-align: right;width: 100%" id="tblhide" runat="server" visible="false">
                            <tr>
                                <td style="text-align: left">Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
                                </td>
                                <td colspan="4">
                                    <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                        Text="Search By"></asp:Label>
                                    <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                        OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--</asp:ListItem>
                                        <asp:ListItem Value="EMP_FIRST_NAME">Executive Name</asp:ListItem>
                                        <asp:ListItem Value="DATETIME"> Date</asp:ListItem>
                                        <asp:ListItem Value="CLIENTSNAME">Client Name </asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                        EnableTheming="False" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged"
                                        Visible="False" Width="50px">
                                        <asp:ListItem Selected="True">=</asp:ListItem>
                                        <asp:ListItem>&lt;</asp:ListItem>
                                        <asp:ListItem>&gt;</asp:ListItem>
                                        <asp:ListItem>&lt;=</asp:ListItem>
                                        <asp:ListItem>&gt;=</asp:ListItem>
                                        <asp:ListItem>R</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                    </asp:Label>
                                    <asp:TextBox ID="txtSearchValueFromDate" runat="server" type="datepic" EnableTheming="True" Visible="False"
                                        Width="106px">
                                    </asp:TextBox><%--<asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchFrom" runat="server" Enabled="False"
                                            PopupButtonID="imgFromDate" TargetControlID="txtSearchValueFromDate">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>--%>
                                    <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                    </asp:Label>
                                    <asp:TextBox ID="txtSearchValueToDate" runat="server" type="datepic" EnableTheming="True" Visible="False"
                                        Width="106px">
                                    </asp:TextBox>
                                    <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Visible="False"></asp:TextBox><%--<asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchValueToDate" runat="server"
                                            Enabled="False" PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>--%>
                                    <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                        CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnSearchGo_Click" /></td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblUserType" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblDeptId" runat ="server" Visible ="false" ></asp:Label>
                                    <asp:Label ID="lblDeptHeadId" runat ="server" Visible ="false" ></asp:Label>
                                    <asp:Label ID="lblDeptHead" runat ="server" Visible ="false"  ></asp:Label>
                                    <asp:Label ID="lblUserId" runat ="server" Visible ="false" ></asp:Label>
                                    <asp:Label ID="lblDrId" runat ="server" Visible ="false" ></asp:Label>

                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        
                        <asp:GridView ID="gvDailyRptDtls" Visible="false" runat="server" AllowPaging="True" OnPageIndexChanging="gvDailyRptDtls_PageIndexChanging" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="gvDailyRptDtls_RowDataBound">
                            <AlternatingRowStyle BackColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <br />
            <table runat ="server" visible ="false" >
                <tr>
                    <td style="text-align: right">From Date :
                    <asp:TextBox ID="txtFromDate" type="datepic" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">To Date : </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtToDate" type="datepic" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">Client Name :
                    <asp:TextBox ID="txtClientName" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">Employee Name :</td>
                    <td style="text-align: left">
                        <%--<asp:TextBox ID="txtEmp_Name" runat="server"></asp:TextBox>--%>
                        <asp:DropDownList ID="ddlSalesPerson" runat="server" AutoPostBack="True"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center" colspan="5">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CausesValidation="False" />
                <asp:Button ID="runDailyReport" runat="server" OnClick="runDailyReport_Click" Text="Run Report" CausesValidation="False" />

                    </td>
                </tr>
            </table>
            <br />
            <div id="grid" style="width: 100%" runat ="server"  visible="false" >
                
                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" CausesValidation="False" />
                <asp:Button ID="btnPostComment" runat="server" Text="HOD Post Comment" OnClick="btnPostComment_Click" CausesValidation="False" />
                <asp:Button ID="btnFollowUp" runat="server" CausesValidation="False" Text="Executive Follow Up" OnClick="btnFollowUp_Click"  />
                <asp:GridView ID="gvDrs" Width="100%" runat="server" SelectedRowStyle-BackColor="#c0c0c0"  AllowPaging="True" OnPageIndexChanging="gvDrs_PageIndexChanging" PageSize="8" AutoGenerateColumns="False" OnRowDataBound="gvDrs_RowDataBound" ShowFooter ="True" >
                     <PagerStyle CssClass="pagination" HorizontalAlign="Left"  />
                        <FooterStyle ForeColor="#0066ff" />
                    <Columns>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkhdr" runat="server" AutoPostBack="True" OnClick="selectAll(this)" />
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="Chk" OnCheckedChanged="Chk_CheckedChanged" AutoPostBack="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Form Id" SortExpression="DAILYREPORTID" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblId" Text='<%#Eval("DAILYREPORTID")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                        </asp:BoundField>

                        <asp:BoundField DataField="In TIME" HeaderText="In TIME" SortExpression="In TIME">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Out TIME" HeaderText="Out TIME" SortExpression="Out TIME">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Client Name" HeaderText="Client" SortExpression="Client Name" ItemStyle-Wrap="false">
                            
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle" Wrap="true" />
                        </asp:BoundField>

                        <asp:BoundField DataField="PHONE" HeaderText="PHONE" SortExpression="PHONE">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Reference" HeaderText="Reference" SortExpression="Reference">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                        </asp:BoundField>

                        <asp:BoundField DataField="Architect" HeaderText="Architect" SortExpression="Architect">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                        </asp:BoundField>

                        <asp:BoundField DataField="ADDRESS" HeaderText="Address" ItemStyle-Wrap="false" SortExpression="ADDRESS">
                            
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                        </asp:BoundField>

                        <asp:BoundField DataField="PURPOSE" HeaderText="Purpose" ItemStyle-Wrap="false" SortExpression="PURPOSE">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                        </asp:BoundField>

                        <asp:BoundField DataField="REMARKS" HeaderText="Remarks"  ItemStyle-Wrap="false" SortExpression="REMARKS">
                            <ControlStyle Width="500px" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="500px"/>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" Width="500px"/>
                        </asp:BoundField>

                        <asp:BoundField DataField="Attended By" HeaderText="Attended By" SortExpression="Attended By" ItemStyle-Wrap="false">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Backup Name" HeaderText="Backup" SortExpression="Backup Name" ItemStyle-Wrap="false">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Comments">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtComment" TextMode="SingleLine" Width="150px" Text='<%#Eval("Comments")%>' runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        <asp:BoundField DataField ="DAILYREPORTID" HeaderText ="Id" />
                        <asp:TemplateField HeaderText="FileName" >
                                                <ItemTemplate>
                                                    <asp:Image runat ="server" EnableTheming="False"  ImageUrl = '<%# Eval("FileName","http://183.82.108.55/YANTRA_DOCUMENTS/SOFiles/{0}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                            <span style="color: #FF0000">No Data to Display</span>
                        </EmptyDataTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="Silver"></SelectedRowStyle>
                </asp:GridView>
                <table id="Table1" align="center">
                    
                    <tr>
            <td>
                <table id="tblFollowUp" runat="server" border="0" cellpadding="0" cellspacing="0"
                    visible="false" width="100%">
                    <tr>
                        <td class="profilehead" colspan="2">follow up details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Clients'Name : 
                            <asp:TextBox ID="txtCustName" runat="server" ></asp:TextBox>
                        </td>
                        <td style="text-align: left">Reference : 
                            <asp:TextBox ID="txtRef" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Date : 
                            <asp:TextBox ID="txtDate" runat ="server" Type="date" ></asp:TextBox>
                        </td>
                        <td style="text-align: left">Adddress : 
                            <asp:TextBox ID="txtAdd" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Purpose :</td>
                        <td style="text-align: left" >
                            <asp:TextBox ID="txtPurps" runat="server" onkeypress="return check(event)" TextMode="MultiLine" Width="560px" EnableTheming="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Remarks</td>
                        <td style="text-align: left" >
                            <asp:TextBox ID="txtRemrks" runat="server" onkeypress="return check(event)" TextMode="MultiLine" Width="560px" EnableTheming="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Follow Up Description
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtFollowUpDesc" runat="server" CssClass="multilinetext" EnableTheming="False"
                                Height="40px" TextMode="MultiLine" Width="560px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"> Commented By</td>
                        <td><asp:DropDownList ID="ddlCommentedBy" runat="server" Enabled="False"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table id="Table2" align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnFollowUpSave" runat="server" CausesValidation="False" OnClick="btnFollowUpSave_Click"
                                            Text="Save" /></td>
                                    <%--<td>
                                        <asp:Button ID="btnFollowUpRefresh" runat="server" CausesValidation="False" OnClick="btnFollowUpRefresh_Click"
                                            Text="Refresh" /></td>
                                    <td>
                                        <asp:Button ID="btnFollowUpHistory" runat="server" CausesValidation="False" OnClick="btnFollowUpHistory_Click"
                                            Text="History" /></td>
                                    <td>
                                        <asp:Button ID="btnFollowUpClose" runat="server" CausesValidation="False" OnClick="btnFollowUpClose_Click"
                                            Text="Close" /></td>--%>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table id="tblFollowUpHistory" runat="server" border="0" cellpadding="0" cellspacing="0"
                                width="100%" >
                                <tr>
                                    <td class="profilehead" colspan="3">Follow Up History</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvFollowUp" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            DataSourceID="sdsFollowUp" Width="100%" OnRowDataBound ="gvFollowUp_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="DATETIME" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date"
                                                    HtmlEncode="False" SortExpression="FU_DATE" />
                                                <asp:BoundField DataField="CLIENTSNAME" HeaderText="Client's Name" SortExpression="CLIENTSNAME">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="REFERENCE" HeaderText="Reference" SortExpression="REFERENCE">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PURPOSE" HeaderText="Purpose" SortExpression="PURPOSE">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="REMARKS" HeaderText="Comments" SortExpression="REMARKS">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Comments" HeaderText="Description" SortExpression="Comments">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Emp_id" HeaderText="Commented By" SortExpression="Emp_id">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="USER_NAME" HeaderText="Commented By" SortExpression="USER_NAME">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="USER_TYPE" HeaderText="Commented By" SortExpression="USER_TYPE">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                            </Columns>
                                            <SelectedRowStyle BackColor="LightSteelBlue" />
                                        </asp:GridView>
                                        <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                                        <br />
                                        <asp:SqlDataSource ID="sdsFollowUp" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                            SelectCommand="select [YANTRA_DAILY_REPORT_DET].[DATETIME],[YANTRA_DAILY_REPORT_DET].CLIENTSNAME,[YANTRA_DAILY_REPORT_DET].[REFERENCE],[YANTRA_DAILY_REPORT_DET].[PURPOSE],[YANTRA_DAILY_REPORT_DET].[REMARKS],[YANTRA_DAILY_REPORT_DET].[Comments],[Emp_id],[USER_NAME],[USER_TYPE] 
from [YANTRA_DAILY_REPORT_DET] &#13;&#10; inner join [YANTRA_USER_DETAILS ] on [YANTRA_DAILY_REPORT_DET] .[CommentedBy]=[YANTRA_USER_DETAILS] .[Emp_id] where [YANTRA_DAILY_REPORT_DET].[DAILYREPORTID]=@ASSIGNID&#13;&#10; order by [YANTRA_DAILY_REPORT_DET].[DAILYREPORTDET_ID] desc ">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="lblId" DefaultValue="0"
                                                    Name="ASSIGNID" PropertyName="Text" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
                </table>
            </div>

                

            </asp:Panel> 
             
            <asp:Panel runat ="server" ID ="ReturnNote" Visible ="false"   >
                 <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
               <div>
   
            <div class="page-header">
                <div class="page-title">
                    <h3>To Do List</h3>
                </div>
            </div>
            <div class="breadcrumb-line">
                <ul class="breadcrumb">
                    <li><a href="DR.aspx">Daily Report</a></li>
                    <li class="active"><a href="ToDoList1.aspx">To Do List</a></li>
                </ul>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h6 class="panel-title">To Do List</h6>
                </div>
                <div class="panel-body">
                    <div class="row">

                    <div class="col-md-1"></div>
                    <div class="col-md-5">
                        <asp:Label ID="Label17" runat ="server"  Text ="Date & Time" ></asp:Label>
                         <asp:TextBox ID="txtIssueddt" runat ="server" CssClass="form-control" type="datepic"></asp:TextBox>
                    </div>
                    </div>
                    <div class ="row">
                        <div class="col-md-1"></div>
                        <div class="col-md-5">
                            <asp:Label ID="lbl2" runat ="server" Text ="Reporting To"></asp:Label>
                            <%-- <asp:DropDownList ID="ddluname1" runat="server" DataSourceID="usersds1" DataTextField="USER_NAME" DataValueField="USER_ID">
                                        </asp:DropDownList>--%>
                                        <select id="Books" class="form-control" runat="server"></select>

                                        <asp:SqlDataSource ID="usersds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [EMP_ID], [EMP_FIRST_NAME] FROM [YANTRA_EMPLOYEE_MAST] where Status =1"></asp:SqlDataSource>
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
                        <asp:Label ID="Label18" runat ="server"  Text ="Subject" ></asp:Label>
                         <asp:TextBox ID="txtSubject" runat ="server" CssClass="form-control" TextMode ="MultiLine" ></asp:TextBox>
                    </div>
                    </div>
                    <div class="row">
                    <div class="col-md-1"></div>
                    <div class="col-md-10">
                        <asp:Label ID="Label19" runat ="server"  Text ="Task Activity Description" ></asp:Label>
                         <asp:TextBox ID="txtDescr" runat ="server" CssClass="form-control" TextMode ="MultiLine" ></asp:TextBox>
                    </div>
                    </div>
                    <div class="row">
                    <div class="col-md-1"></div>
                    <div class="col-md-5">
                        <asp:Label ID="Label20" runat ="server"  Text ="Task Activity Status" ></asp:Label>
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
                                    <asp:Button ID="btnListSave" runat="server" CssClass="btn btn-lg btn-danger " OnClick ="btnListSave_Click" Text="Save" />
                                    <asp:Label ID="Label21" runat="server" Visible="False"></asp:Label>
                               
                                     </div>
                            </div>
                            <div class="col-md-1"></div>
                        </div>
                    </div>
                
            </div>

            <div class="panel panel-default">
                <div class="panel-heading">
                    <h6 class="panel-title">To DO List Details</h6>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <div class="row">

                    <div class="col-md-1"></div>
                    <div class="col-md-5">
                        <asp:Label ID="Label22" runat ="server"  Text ="From Date" ></asp:Label>
                         <asp:TextBox ID="txtListFrom" runat ="server" CssClass="form-control" type="datepic"></asp:TextBox>
                    </div>
                   <div class="col-md-5">
                        <asp:Label ID="Label23" runat ="server"  Text ="To Date" ></asp:Label>
                         <asp:TextBox ID="txtListTo" runat ="server" CssClass="form-control" type="datepic"></asp:TextBox>
                    </div>
                </div>
                        <div class="row">

                    <div class="col-md-1"></div>
                    <div class="col-md-5">
                        <asp:Label ID="Label24" runat ="server"  Text ="Subject" ></asp:Label>
                         <asp:TextBox ID="txtListSubject" runat ="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                   <div class="col-md-5">
                        <asp:Label ID="Label25" runat ="server"  Text ="Executive Name" ></asp:Label>
                         <asp:DropDownList ID="ddlEmp" runat="server" CssClass ="form-control"></asp:DropDownList>
                    </div>
                </div>
                        <div class="form-group">
                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-10">
                                <div class="form-actions text-center">
                        <asp:Button ID="btnListSearch" runat="server" Text="Search" OnClick="btnListSearch_Click" CausesValidation="False" />
                <asp:Button ID="btnListDelete" runat="server" Text="Delete" OnClick="btnListDelete_Click" Visible ="false"  CausesValidation="False" />
                                    <asp:Button ID="btnListUpdate" runat ="server" Text ="Update" OnClick ="btnListUpdate_Click" Visible ="false" CausesValidation ="false" />
<asp:Label ID="lblItem" runat ="server" Visible ="false" ></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-1"></div>
                        </div>
                    </div>
                        
                        <div>
                            <asp:GridView ID="gvList" Width="100%" OnRowDataBound ="gvlist_RowDataBound" runat="server" SelectedRowStyle-BackColor="#c0c0c0" EmptyDataText="No Records To Display" AllowPaging="True" OnPageIndexChanging="gvList_PageIndexChanging" PageSize="8" AutoGenerateColumns="False" >
                    <Columns>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkhdr" runat="server" AutoPostBack="True" OnClick="selectAll(this)" />
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="Chk" OnCheckedChanged="Chk_CheckedChanged" AutoPostBack="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Id" SortExpression="ID" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblId" Text='<%#Eval("ID")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                        </asp:BoundField>
                         <asp:BoundField DataField="IssuedDate" HeaderText="IssuedDate" SortExpression="IssuedDate">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                        </asp:BoundField>
                       

                        <asp:BoundField DataField="Subject" HeaderText="Subject" SortExpression="Subject" ItemStyle-Wrap="false">
                            
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle" Wrap="true" />
                        </asp:BoundField>
                       
                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                        </asp:BoundField>
                         <%-- <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                        </asp:BoundField>--%>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlStatus" runat="server">
                                   <asp:ListItem >To Do</asp:ListItem>
                                <asp:ListItem >In-Progress</asp:ListItem>
                                <asp:ListItem >Completed</asp:ListItem>
                                </asp:DropDownList>
                                <asp:HiddenField ID="cthf1" runat="server" Value='<%# Eval("Status") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PreparedBy" HeaderText="PreparedBy" SortExpression="PreparedBy" ItemStyle-Wrap="false">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                        </asp:BoundField>
                        <asp:BoundField DataField ="ID" HeaderText ="Id" />
                        <asp:TemplateField HeaderText ="Reporting To">
                            <HeaderStyle HorizontalAlign ="Center"  />
                             <ItemTemplate>
                                 <asp:GridView ID="gvChild" CssClass="subgridviews" ShowHeader ="false" EnableTheming ="false" Width ="100%"  runat ="server"  AutoGenerateColumns ="false">
                                                  <Columns >
                                                      <%--<asp:BoundField DataField ="ID" HeaderText ="ID" />--%>
                                                       
                                     <asp:BoundField  DataField ="EMP_FIRST_NAME" HeaderText ="Reporting To" >
                                          <ItemStyle HorizontalAlign ="Center" />
                         <HeaderStyle HorizontalAlign ="Center" /> </asp:BoundField>
                                     
                                                  </Columns>
                                  </asp:GridView>
                             </ItemTemplate>
                        </asp:TemplateField>
                       
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="Silver"></SelectedRowStyle>
                </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
           
    </div>
                </asp:View>
        </asp:MultiView>
             </asp:Panel>
      <%--</ContentTemplate>
         <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
           <asp:PostBackTrigger ControlID ="CheckBoxList1" />
              <asp:PostBackTrigger ControlID ="ListBox1" />
        </Triggers>
    </asp:UpdatePanel>--%>
      
    </div>
</asp:Content>


 
