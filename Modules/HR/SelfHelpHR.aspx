<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/HR/HRMP1.master" AutoEventWireup="true" CodeFile="SelfHelpHR.aspx.cs" Inherits="Modules_HR_SelfHeplHR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
      <h3 style="color: #3333FF"><span style="font-weight: normal">Employee Personal Details</span></h3>
     <table class="pagehead">
        <%--<tr style="padding-left: 10px">
            <td colspan="4" style="vertical-align:top; text-align:left;">
               Employee Personal Details</td>
        </tr>--%>
         </table>
      <table>
      <tr  style="text-align:left">
          <td>
                  <asp:Button ID="btnUpdatePersonalEMpDeatils" runat="server" Text="Permission" Width="130px" PostBackUrl="~/Modules/HR/Emp_PersonalDetailsUpdate.aspx" />
                <br />
               <asp:Button ID="btnOnDuty" runat="server" Text="On Duty" Width="130px" PostBackUrl="~/Modules/HR/EmpOnDutyFormApprove.aspx" />
                <br />
              <asp:Button ID="btnOneHourPermission" runat="server" Text="One Hour Permission" Width="130px" PostBackUrl="~/Modules/HR/EmpOneHourPermissionApprove.aspx" />
                <br />
              <asp:Button ID="btnOverTime" runat="server" Text="OverTime" Width="130px" PostBackUrl="~/Modules/HR/EmpOverTimeFormApprove.aspx" />
                <br />
               <asp:Button ID="btnShiftChange" runat="server" Text="Shift Change" Width="130px" PostBackUrl="~/Modules/HR/EmpShiftChangeApprove.aspx" />
                <br />
               <asp:Button ID="btnTicket" runat="server" Text="Tickets" Width="130px" PostBackUrl="~/Modules/HR/TicketDetailsApprove.aspx" />
                
          </td>
          <td>
              &nbsp;</td>
      </tr>
  </table>
     
     <table style="width:50%">
        <tr>
            <td colspan="2" style="text-align:center">
                <asp:Label ID="lblEmpId" runat="server" Visible="false"></asp:Label>

            </td>
        </tr>
    </table>
    <br />
     <br />
      <table id="tblDetails" style="width:75%;" runat="server" visible="false">
          <tr>
              <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>

