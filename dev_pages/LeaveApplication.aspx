<%@ Page Title="" Language="C#" MasterPageFile="~/dev_pages/MobileApp.master" AutoEventWireup="true" CodeFile="LeaveApplication.aspx.cs" Inherits="dev_pages_LeaveApplication" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .gvStyle {
            text-align: center;
            width: 100%;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $('.numblock').bind('keyup blur', function () {
                $(this).val($(this).val().replace(/[^a-z]/g, ''));
            });
        });
    </script>
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
                minDate: 2
            });
        });
    </script>

    <%--<asp:UpdatePanel runat="server">
        <ContentTemplate>--%>
<div class="form-horizontal">

    <div class="panel panel-default">
        <div class ="panel-heading"><h6 class="panel-title">Leave Application</h6></div>

        <div class="panel-body">

            <div class="form-group">
				            <label class="col-sm-2 control-label text-right">Employee Name :</label>
				            <div class="col-sm-4">
                               <asp:TextBox ID="txtEmpName" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
				            </div>


                          <label class="col-sm-2 control-label text-right">Designation :</label>
				            <div class="col-sm-4">
                                <asp:TextBox ID="txtDesignation" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
				            </div>
                       
                        </div>


            <div class="form-group">
				            <label class="col-sm-2 control-label text-right">Department :</label>
				            <div class="col-sm-4">
                                <asp:TextBox ID="txtDepartment" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
				            </div>


                          <label class="col-sm-2 control-label text-right">Date Of Applying :</label>
				            <div class="col-sm-4">
                                <asp:TextBox ID="txtDateOfApply" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
				            </div>
                       
                        </div>



            <div class="form-group">
				            <label class="col-sm-2 control-label text-right">Avai CasualLeaves:</label>
				            <div class="col-sm-4">
                               <asp:TextBox runat="server" CssClass="form-control" ID="txtAvailableCasualLeaves" ReadOnly="true" />
				            </div>


                          <label class="col-sm-2 control-label text-right">Avai EarnedLeaves:</label>
				            <div class="col-sm-4">
                               <asp:TextBox runat="server" CssClass="form-control" ID="txtAvailableEarnedLeaves" ReadOnly="true" />
				            </div>
                       
                        </div>


            <div class="form-group">
				            <label class="col-sm-2 control-label text-right">From Date  :</label>
				            <div class="col-sm-4">
                               <asp:TextBox ID="txtFromDate" CssClass="form-control" class="numblock" type="date2" runat="server"></asp:TextBox>
                    *
                     <br />
                    <asp:RadioButtonList ID="rbnFrom" CssClass="form-control" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbnFrom_SelectedIndexChanged">

                        <asp:ListItem Value="0">Session1</asp:ListItem>
                        <asp:ListItem Value="1">Session2</asp:ListItem>
                    </asp:RadioButtonList>				            </div>


                          <label class="col-sm-2 control-label text-right">To date :</label>
				            <div class="col-sm-4">
                              <asp:TextBox ID="txtToDate" CssClass="form-control" class="numblock" type="date2" runat="server"></asp:TextBox>

                    *
                    <br />
                    <asp:RadioButtonList ID="rbnTo" CssClass="form-control" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbnTo_SelectedIndexChanged">

                        <asp:ListItem Value="0">Session1</asp:ListItem>
                        <asp:ListItem Value="1">Session2</asp:ListItem>
                    </asp:RadioButtonList>				            </div>
                       
                        </div>



            <div class="form-group">
				            <label class="col-sm-2 control-label text-right" ></label>
				            <div class="col-sm-4">
                                 <asp:Label ID="lblCoff" Visible="false" CssClass="form-control" runat="server" Font-Bold="True" ForeColor="#CC0000"></asp:Label>&nbsp; 
                                <%--(To avail Spl. Leaves apply leave as Extra Leave)--%>
				            </div>


                          <label class="col-sm-2 control-label text-right">Total Days Of Leave :</label>
				            <div class="col-sm-4">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtTotalDaysOfLeave" ReadOnly="true" OnTextChanged="txtTotalDaysOfLeave_TextChanged" />
				            </div>
                       
                        </div>


            <div class="form-group">
				            <%--<label class="col-sm-2 control-label text-right">Total Days Of Leave :</label>
				            <div class="col-sm-4">
                                   <asp:TextBox runat="server" CssClass="form-control" ID="TextBox2" ReadOnly="true" OnTextChanged="txtTotalDaysOfLeave_TextChanged" />
				            </div>--%>


                          <label class="col-sm-2 control-label text-right">Type Of Leave Applying</label>
				            <div class="col-sm-4">
                             <asp:DropDownList ID="ddlTypeOfLeave" CssClass="form-control" AutoPostBack ="true"  runat="server" OnSelectedIndexChanged="ddlTypeOfLeave_SelectedIndexChanged" >
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        <asp:ListItem Value="1">Casual Leave</asp:ListItem>
                        <asp:ListItem Value="2">Earned Leave</asp:ListItem>
                        <asp:ListItem Value="3">Extra Leave</asp:ListItem>
                    </asp:DropDownList>	

                                 <asp:RadioButtonList ID="rbtnFullLeave" CssClass="form-control" runat="server" Visible="false" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtnFullLeave_SelectedIndexChanged" AutoPostBack="True">

                        <asp:ListItem Value="0">Half Day</asp:ListItem>
                        <asp:ListItem Value="1">Full Day</asp:ListItem>
                    </asp:RadioButtonList>
				            </div>
                       
                        </div>


            <div class="form-group">
				            <label class="col-sm-2 control-label text-right">Reason For Leave :</label>
				            <div class="col-sm-4">
                                 <asp:TextBox runat="server" CssClass="form-control" ID="txtReason" TextMode="MultiLine" Style="width: 400px" />
				            </div>


                          <label class="col-sm-2 control-label text-right">Address In Leave Period :</label>
				            <div class="col-sm-4">
                              <asp:TextBox runat="server" CssClass="form-control" ID="txtAddress" TextMode="MultiLine" Style="width: 400px" />
                    <asp:TextBox ID="txtAvailableExtra" CssClass="form-control" runat="server" Visible="False"></asp:TextBox>
            </div>
                       
                        </div>


            <div class="form-actions col-sm-offset-1">
                             <asp:Button ID="btnCalculate" CssClass="btn-danger"  runat="server" Text="Calculate" OnClick="btnCalculate_Click" />
                    <asp:Button Text="Apply Leave" ID="btnApplyLeave" OnClick="btnApplyLeave_Click" Visible="false" runat="server" />
                    <%--<asp:Button ID="btnLeaveHistory" runat="server" Text="Leave History" OnClick="btnLeaveHistory_Click" />--%>
                    <asp:Button ID="btnDelete" CssClass="btn-success" runat="server" Text="Delete Leave" Visible="false" OnClick="btnDelete_Click" />

                    <br>
                    <asp:Label ID="lblEmpId" Visible="false" runat="server" />
                    <asp:Label ID="lblEmpName" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblMobile" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblHod_Mbl" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblHOD_Id" runat="server" Visible="False"></asp:Label>

                    <asp:Label ID="lblId" Visible="false" runat="server" />
                    <asp:Label ID="lblMsg" Visible="true" runat="server" />
                    <asp:Label ID="lblLeaveId" Visible="false" runat="server" />
                    <asp:Label ID="lblNoOfLeaves" Visible="false" runat="server" />
                    <asp:Label ID="lblTypeOfLeave" Visible="false" runat="server" />

                    <asp:Label ID="lblLeaveIdTemp" Visible="false" runat="server" />
                <asp:Label ID="lblReportingID" runat="server" Visible="False"></asp:Label>

                    <asp:Label ID="lblExtraLeaves" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblDeptId" runat="server" Visible="False"></asp:Label>
                  </div>



        </div>


    </div>






   <div class="panel panel-danger">
<div class="panel-body">


    <asp:GridView ID="gvPendingLeaves" AutoGenerateColumns="False" runat="server" EmptyDataText="No Records Found" Width="100%" SelectedRowStyle-BackColor="#c0c0c0" AllowSorting="True">
            <Columns>

                <asp:TemplateField HeaderText="Leave Id" SortExpression="Leave_Id">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnLeaveId" ForeColor="#0066FF" OnClick="lbtnLeaveId_Click" runat="server" Text="<%# Bind('Leave_Id') %>" CausesValidation="False"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Emp Id" Visible="false">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblEmpId" Text='<%# Eval("Emp_Id") %>' />

                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Applied Date" SortExpression="DateOfApplying">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblAppliedDate" Text='<%# Eval("DateOfApplying") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="From Date" SortExpression="FromDate">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblFromDate" Text='<%#Eval("FromDate")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="From Session" SortExpression="FromSession">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblFromSession" Text='<%#Eval("FromSession")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="To Date" SortExpression="ToDate">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblToDate" Text='<%#Eval("ToDate")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="To Session" SortExpression="ToSession">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblToSession" Text='<%#Eval("ToSession")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="No Of Days Applied" SortExpression="AppliedNoOfLeaves">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblDaysofLeave" Text='<%# Eval("AppliedNoOfLeaves") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Type of Leave" SortExpression="TypeOfLeave">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblLeaveType" Text='<%#Eval("TypeOfLeave")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Reason For Leave" SortExpression="Reason">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblReason" Text='<%# Eval("Reason") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Address In Leave" SortExpression="AddressInLeavePeriod">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblAddressInLeave" Text='<%# Eval("AddressInLeavePeriod") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Status Of Leave" SortExpression="Status1">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblStatus" Text='<%# Eval("Status1") %>' />
                    </ItemTemplate>
                </asp:TemplateField>




            </Columns>
        </asp:GridView>





</div>




   </div>

  
</div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

