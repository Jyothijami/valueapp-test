<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SalaryBreakUps.aspx.cs" Inherits="Modules_HR_SalaryBreakUps" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
   
     
         <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:214pt" width="285">
             <colgroup>
                 <col style="mso-width-source:userset;mso-width-alt:10422;width:214pt" width="285" />
             </colgroup>
            
         </table>
   
     
         <table cellpadding="5" cellspacing="0">
             <tr>
                 <td style="width:150px;">

                     Employee Name:</td>
                 <td>

                     <asp:DropDownList ID="ddlEmpName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged">
                     </asp:DropDownList>

                 </td>             
             </tr>
             <tr>
                 <td>

                     Age:</td>
                 <td>

                     <asp:TextBox ID="txtAge" runat="server">
                     </asp:TextBox>

                 </td>             
             </tr>
             <tr>
                 <td>

                     Enter Gross Amount:</td>
                 <td>

                     <asp:TextBox ID="txtGrossAmount" runat="server">
                     </asp:TextBox>&nbsp;</td>             
                 <td>

                     <asp:TextBox ID="txtGrossAmountYear" runat="server">
                     </asp:TextBox>&nbsp;</td>             
             </tr>
             <tr>
                 <td>

                 </td>
                 <td>

                     <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Calculate" />

                 </td>             
             </tr>
         </table>
         <br />
         <h3>Employee Earnings</h3>
         <table cellpadding="5" cellspacing="0">
             <tr>
                 <td style="width: 150px">Basic Salary</td>
                 <td>
                     <asp:TextBox ID="txtBasic1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td>
                     <asp:TextBox ID="txtBasic2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td style="width: 150px">House Rent Allowance</td>
                 <td>
                     <asp:TextBox ID="txtHRA1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td>
                     <asp:TextBox ID="txtHRA2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td>Conveyance Allowance</td>
                <td>
                     <asp:TextBox ID="txtCA1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td>
                     <asp:TextBox ID="txtCA2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td class="auto-style1">Medical Expenses Reimbursement</td>
                 <td class="auto-style1">
                     <asp:TextBox ID="txtMER1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td class="auto-style1">
                     <asp:TextBox ID="txtMER2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td>Other_Allowance</td>
                  <td>
                     <asp:TextBox ID="txtOther1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td>
                     <asp:TextBox ID="txtOther2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td  ><b>Gross</b></td>
                 <td  >
                     <asp:TextBox ID="txtGrossTotal1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td  >
                     <asp:TextBox ID="txtGrossTotal2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
         </table>
         <br />
    <h3>Deductions</h3>
         <h3>Standard Deductions</h3>
         <table cellpadding="5" cellspacing="0">             
             <tr>
                 <td style="width: 150px">PF Contribution (Employee)</td>
                 <td>
                     <asp:TextBox ID="txtPFD1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td>
                     <asp:TextBox ID="txtPFD2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td style="width: 150px">ESIC Contribution (Employee)</td>
                 <td>
                     <asp:TextBox ID="txtESICD1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td>
                     <asp:TextBox ID="txtESICD2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td style="width: 150px">PT</td>
                 <td>
                     <asp:TextBox ID="txtPT1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td>
                     <asp:TextBox ID="txtPT2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
             
             <tr>
                 <td  >Total</td>
                 <td  >
                     <asp:TextBox ID="txtECTotal1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td  >
                     <asp:TextBox ID="txtECTotal2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <%--<td style="border-top-style: solid; border-top-width: 1px; border-top-color: #CCCCCC">Gross(With Deductions) </td>--%>
                 <td  >Gross(With Deductions)</td>
                 <td  >
                     <asp:TextBox ID="txtGrossTotalD1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td  >
                     <asp:TextBox ID="txtGrossTotalD2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
         </table>
         <br />
         <h3>Benefits</h3>
         <table cellpadding="5" cellspacing="0">             
          <tr>
                 <td style="width: 150px">PF Contribution (Employer)</td>
                 <td>
                     <asp:TextBox ID="txtPFB1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td>
                     <asp:TextBox ID="txtPFB2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td style="width: 150px">ESIC Contribution (Employer)</td>
                 <td>
                     <asp:TextBox ID="txtESICB1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td>
                     <asp:TextBox ID="txtESICB2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td style="width: 150px">Group Medical Insurance and Personal Accident Cover Policy - Premium</td>
                 <td>
                     <asp:TextBox ID="txtACCB1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td>
                     <asp:TextBox ID="txtACCB2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
             
             <tr>
                   <td style="width: 150px">Bonus </td>
                 <td>
                     <asp:TextBox ID="txtBONUSB1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td>
                     <asp:TextBox ID="txtBONUSB2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td  >Total</td>
                 <td  >
                     <asp:TextBox ID="txtTotalB1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td  >
                     <asp:TextBox ID="txtTotalB2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
         </table>
         <br />
         <table cellpadding="5" cellspacing="0">
             <tr>
                 <td style="width: 150px">Total CTC<br />
                     (Earnings+Benefits)</td>
                 <td >
                     <asp:TextBox ID="txtCTCPM" runat="server">
                                                 </asp:TextBox>
                 </td>
             <%--</tr>
             <tr>
                 <td>Total CTC(PA)</td>--%>
                 <td >
                     <asp:TextBox ID="txtCTCPA" runat="server"></asp:TextBox>
                 </td>
             </tr>
         </table>
         <br />
         <br />
         <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Print" />
         <br />



<asp:Panel ID="Panel2" runat="server" Width="100%" Visible="false">
         <table width="100%">
             <tr>
                 <td colspan="4">
                     <asp:Panel ID="Panel1" runat="server" Height="50px" Width="125px">
                         <table id="tblMainas">
                             <tr>
                                 <td colspan="4">
                                     <table>
                                         <tr>
                                             <td style="text-align: right">
                                                 Employee Name : 
                                             </td>
                                             <td style="text-align: left">
                                                 <asp:Label ID="lblEmpname" runat="server"></asp:Label>
                                             </td>
                                             </tr>
                                             <tr>
                                             <td style="text-align: right">
                                             Age : </td>
                                             <td style="text-align: left">
                                                 <asp:Label ID="lblAge" runat="server"></asp:Label>
                                             </td>
                                         </tr>
                                          <tr>
                                             <td style="text-align: right">
                                             Net Salary : </td>
                                             <td style="text-align: left">
                                                 <asp:Label ID="lblNetSal" runat="server"></asp:Label>
                                             </td>
                                         </tr>
                                     </table>
                                 </td>
                             </tr>
                             <tr>
                                 <td colspan="4">&nbsp;</td>
                             </tr>
                             <tr>
                                 <td colspan="2">&nbsp;</td>
                                 <td colspan="2" valign="top">&nbsp;</td>
                             </tr>
                             <tr>
                                 <td ></td>
                                 <td colspan="2">&nbsp;</td>
                                 <td ></td>
                             </tr>
                             <tr>
                                 <td ></td>
                                 <td colspan="2">&nbsp;</td>
                                 <td ></td>
                             </tr>
                         </table>
                     </asp:Panel>
                 </td>
             </tr>
             <tr>
                 <td colspan="4"></td>
             </tr>
             <tr>
                 <td colspan="4" style="text-align: center;">&nbsp;</td>
             </tr>
             <tr>
                 <td ></td>
                 <td ></td>
                 <td ></td>
                 <td ></td>
             </tr>
         </table>
     </asp:Panel>
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            height: 39px;
        }
    </style>
    </asp:Content>


