<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.master" AutoEventWireup="true" CodeFile="Calender.aspx.cs" Inherits="dev_pages_Calender" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div class="row">
        <div class="col-lg-offset-1">
            <!-- Page header -->
            <div class="page-header">
                <div class="page-title">
                    <h3> <small>
                        <asp:Label ID="lblUserName" runat="server"></asp:Label></small></h3>
                </div>
            </div>
        </div>

    </div>
    <div class="form-horizontal">
        <div class="row">
            <div class="panel panel-danger">
                <div class="panel-heading">
                   <h6 class="panel-title">
                            <i class="icon-grid"></i>Scheduler
                        </h6>
                </div>
                <div class="panel-body">
                    <telerik:RadScheduler ID="RadScheduler1" runat="server" DataDescriptionField="Description" DataEndField="End_Time" DataKeyField="Id" DataRecurrenceField="RecurrenceRule" Height="100%"
                        DataRecurrenceParentKeyField="RecurrenceParentID" DataReminderField="Reminder" DataStartField="Start" DataSubjectField="Subject" EnableDescriptionField="True"
                        DayStartTime="09:00:00" DayEndTime="19:00:00" SelectedView="MonthView" WorkDayEndTime="18:00:00" WorkDayStartTime="09:00:00">
                        <AgendaView UserSelectable="true" />
                        <Reminders Enabled="True" />
                        <AdvancedForm Modal="true" />
                        <TimelineView UserSelectable="false"></TimelineView>
                        <AppointmentTemplate>

                            <div class="panel panel-danger">
                                <div class="panel-title">
                                    <div class="panel-heading">
                                        <h6><%# Eval("Subject") %></h6>
                                    </div>
                                    <div class="panel-body">
                                        <b><%# Eval("Description") %></b>
                                    </div>
                                </div>
                            </div>
            

        </AppointmentTemplate>
                    
                    
                    
                    </telerik:RadScheduler>
                    
                </div>
            </div>
        </div>
    </div>



</asp:Content>

