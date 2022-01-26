<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.master" AutoEventWireup="true" CodeFile="DailyReportM.aspx.cs" Inherits="dev_pages_DailyReportM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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


    <asp:UpdatePanel ID="UpdatePanel34" runat="server">
        <ContentTemplate>
            <div class="page-header">
                <div class="page-title">
                    <h3>Daily Report</h3>
                </div>
            </div>
          <%--  <div class="breadcrumb-line" style="margin:5px">
                <ul class="breadcrumb">
                    <li><a href="Home.aspx">Menu</a></li>
                    <li><a href="DailyReportM.aspx">Daily Report</a></li>
                    <li class="active"><a href="ToDoList1.aspx">To Do List</a></li>
                    <li class="active"><a href="Emp_CR.aspx">Complaint Register</a></li>
                    <li class="active"><a href="DataView.aspx">ToDo List View</a></li>


                </ul>
            </div>--%>

            <div class="form-horizontal">

            <div class="panel panel-default">
                 <div class="panel-heading">
                    <h6 class="panel-title">Daily Report Details</h6>
                </div>
                <div class="panel-body">


                    <div class="form-group">
                            <label class="col-sm-2 control-label text-right">Date & Time :</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtDateTime" runat ="server" CssClass="form-control" type="datepic"></asp:TextBox>
                            <asp:RequiredFieldValidator
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
                            <label class="col-sm-2 control-label text-right">In Time :</label>
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
                            <label class="col-sm-2 control-label text-right">Out Time :</label>
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
                            <label class="col-sm-2 control-label text-right">Client's Name :</label>
                            <div class="col-sm-4">
                                  <asp:TextBox ID="txtClientsName" onkeypress="return check(event)" runat ="server" CssClass ="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtClientsName"
                                            ErrorMessage="Please Enter Client Name">*</asp:RequiredFieldValidator>
                            </div>

                        <label class="col-sm-2 control-label text-right">Phone No :</label>
                            <div class="col-sm-4">
                                 <asp:TextBox ID="txtPhoneNo" runat ="server" CssClass ="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator
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
                            <%--<asp:DropDownList runat ="server" ID="txtReference" Width="100%" CssClass ="select-full">
                            <asp:ListItem>Executive</asp:ListItem>
                            <asp:ListItem>Walk-In</asp:ListItem>
                            <asp:ListItem>Architect</asp:ListItem>
                            <asp:ListItem>Web Site</asp:ListItem>
                            <asp:ListItem>Exhibition</asp:ListItem>
                            <asp:ListItem>Existing Customer</asp:ListItem>
                            <asp:ListItem>Cold Calling</asp:ListItem>
                            <asp:ListItem>Site Visit</asp:ListItem>
                            <asp:ListItem>Recce Of Area</asp:ListItem>
                            <%--<asp:ListItem>Recce Of Area</asp:ListItem>

                            </asp:DropDownList>--%>
                                <asp:DropDownList ID="ddlreference" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>

                        <label class="col-sm-2 control-label text-right">Architect/Executive Name :</label>
                            <div class="col-sm-4">
                                 <asp:TextBox ID="txtArchitect" onkeypress="return check(event)" runat ="server" CssClass ="form-control"></asp:TextBox>
                            </div>
                           
                        </div>

                    <div class="form-group">
                            <label class="col-sm-2 control-label text-right">Address :</label>
                            <div class="col-sm-4">
                                   <asp:TextBox ID="txtAddress" TextMode="MultiLine" onkeypress="return check(event)" runat ="server" CssClass ="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAddress"
                                            ErrorMessage="Please Enter Address" ValidationGroup="a">*</asp:RequiredFieldValidator>
                            </div>

                        
                           
                            <label class="col-sm-2 control-label text-right">Email :</label>
                        <div class="col-sm-4">
                                 <asp:TextBox ID="txtEmail"   runat ="server" CssClass ="form-control"></asp:TextBox>

                        </div>
                        <label class="col-sm-2 control-label text-right"  >Plan of Action :<asp:Label
                                        ID="Label3" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*" ValidationGroup="a"></asp:Label></label>
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
                        <label class="col-sm-2 control-label text-right">Purpose :</label>
                            <div class="col-sm-7">
                                 <asp:TextBox ID="txtPurpose" TextMode="MultiLine" onkeypress="return check(event)" runat ="server" CssClass ="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPurpose"
                                            ErrorMessage="Please Enter Purpose" ValidationGroup="a">*</asp:RequiredFieldValidator>
                            </div>
                          

                    </div>
                    <div class="form-group">

                           <label class="col-sm-2 control-label text-right">Remarks :</label>
                            <div class="col-sm-7">
                                     <asp:TextBox ID="txtRemarks" TextMode="MultiLine" onkeypress="return check(event)" runat ="server" CssClass ="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRemarks"
                                            ErrorMessage="Please Enter Remarks" ValidationGroup="a">*</asp:RequiredFieldValidator>
                            </div>
                            

                            </div>
                    <div class="form-group">

                         <label class="col-sm-2 control-label text-right" >What did you achive Yesterday ? :<asp:Label
                                        ID="Label1" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*" ValidationGroup="a"></asp:Label></label>
                            <div class="col-sm-7">
                                <asp:TextBox ID="txtAchiveYesterday" runat="server" onkeypress="return check(event)" TextMode="MultiLine" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtAchiveYesterday"
                                            ErrorMessage="Please Enter Yesterday Achivements" ValidationGroup="a">*</asp:RequiredFieldValidator>
                            </div>

                        </div>
                    <div class="form-group">

                         <label class="col-sm-2 control-label text-right"  >What do you plan to achive Today ? :<asp:Label
                                        ID="Label2" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*" ValidationGroup="a"></asp:Label></label>
                            <div class="col-sm-7">
                                <asp:TextBox ID="txtachiveToday" runat="server" onkeypress="return check(event)" TextMode="MultiLine" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtachiveToday"
                                            ErrorMessage="Please Enter Today Achivements" ValidationGroup="a">*</asp:RequiredFieldValidator>
                            </div>

                        </div>
                    <div class ="form-group ">
                        <label class="col-sm-2 control-label text-right">Requirement For :</label>

                        <div class="col-sm-4">
                            <asp:CheckBoxList ID="CheckBoxList1" ClientIDMode="AutoID" runat="server" CssClass="checkbox-inline" OnSelectedIndexChanged ="CheckBoxList1_SelectedIndexChanged" AutoPostBack ="True" 
                             RepeatColumns="2" RepeatDirection="Horizontal" >
                                <asp:ListItem >Not Selected</asp:ListItem>
                                    </asp:CheckBoxList>
                        </div>

                           <label class="col-sm-2 control-label text-right">Looking For :</label>
                             <div class="col-sm-4">
                                <asp:ListBox ID="ListBox1" runat="server" CssClass ="form-control" Rows="5" SelectionMode="Multiple">
                                <asp:ListItem >Not Selected</asp:ListItem>
                                </asp:ListBox>
                       <asp:Label ID="lblLstselect" runat ="server" CssClass="caption text-danger" Text ="Please Select Required Multiple Brands By Clicking Ctrl + Mouse Click" ></asp:Label>
                      
                        </div>
                       
                    </div>
                    <div class="form-group">
                            <label class="col-sm-2 control-label text-right">Attended By :</label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID ="ddlAttendedBy" runat ="server"   CssClass ="form-control"></asp:DropDownList> 
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlAttendedBy"
                                        ErrorMessage="Please Slelect Attended By" InitialValue="0">*</asp:RequiredFieldValidator>
                           
                            </div>
                           <label class="col-sm-2 control-label text-right">Backup / Assisted By :</label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID ="ddlBackup" runat ="server"  CssClass ="form-control"></asp:DropDownList>

                            </div>
                     </div>
                  
                    <div class="form-actions text-right">
                              <asp:Button ID="btnSave" runat="server" CssClass="btn btn-danger" style="border-color:red" OnClick ="btnSave_Click" Text="Save" ValidationGroup="a" />

                    </div>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ValidationGroup="a"></asp:ValidationSummary>

    <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>



                 
            </div>
                
            </div>

                </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

