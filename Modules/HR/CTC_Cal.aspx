<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true" CodeFile="CTC_Cal.aspx.cs" Inherits="Modules_HR_CTC_Cal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table cellpadding="5" cellspacing="0">
             <tr>
                 <td style="width:150px;">

                     Employee Name:</td>
                 <td>

                     <asp:DropDownList ID="ddlEmpName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged">
                     </asp:DropDownList>

                 </td>             
             </tr>
            <%-- <tr >
                 <td>

                     Age:</td>
                 <td>

                     <asp:TextBox ID="txtAge" runat="server">
                     </asp:TextBox>

                 </td>             
             </tr>--%>
             <tr>
                 <td>

                     Enter Annual Amount:</td>
                 <td>

                     <asp:TextBox ID="txtGrossAmount" runat="server">
                     </asp:TextBox>

                 </td>             
             </tr>
             <tr>
                 <td>

                 </td>
                 <td>

                     <asp:Button ID="btnCal" runat="server" OnClick="btnCal_Click" Text="Calculate" />

                 </td>             
             </tr>
         </table>
         <br />
         <h3>Earnings</h3>
         <table cellpadding="5" cellspacing="0">
             <tr>
                 <td style="width: 150px">Basic</td>
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
                 <td style="width: 150px">HRA</td>
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
                 <td>Transport Allowance</td>
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
                 <td>Special Allowance</td>
                 <td>
                     <asp:TextBox ID="txtSpl1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td>
                     <asp:TextBox ID="txtSpl2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td style="border-top-style: solid; border-top-width: 1px; border-top-color: #CCCCCC"><b>Gross Total</b></td>
                 <td style="border-top-style: solid; border-top-width: 1px; border-top-color: #CCCCCC">
                     <asp:TextBox ID="txtGrossTotal1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td style="border-top-style: solid; border-top-width: 1px; border-top-color: #CCCCCC">
                     <asp:TextBox ID="txtGrossTotal2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
         </table>
         <br />

         <h3>Statutory Benefits</h3>
         <table cellpadding="5" cellspacing="0">             
             <tr>
                 <td style="width: 150px">PF</td>
                 <td>
                     <asp:TextBox ID="txtPF1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td>
                     <asp:TextBox ID="txtPF2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td>ESI</td>
                 <td>
                     <asp:TextBox ID="txtMedi1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td>
                     <asp:TextBox ID="txtMedi2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td>Performance Incentives</td>
                 <td>
                     <asp:TextBox ID="txtBonus1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td>
                     <asp:TextBox ID="txtBonus2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td style="border-top-style: solid; border-top-width: 1px; border-top-color: #CCCCCC"><strong>Statutory Total</strong></td>
                 <td style="border-top-style: solid; border-top-width: 1px; border-top-color: #CCCCCC">
                     <asp:TextBox ID="txtECTotal1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td style="border-top-style: solid; border-top-width: 1px; border-top-color: #CCCCCC">
                     <asp:TextBox ID="txtECTotal2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
         </table>
         <br />
         <h3>Other Benefits</h3>
         <table cellpadding="5" cellspacing="0">             
             <tr>
                 <td style="width: 150px">Other Benefits</td>
                 <td>
                     <asp:TextBox ID="txtoth1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td>
                     <asp:TextBox ID="txtoth2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
             <%--<tr>
                 <td >Ptax</td>
                 <td>
                     <asp:TextBox ID="txtEDptax" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>--%>
             <%--<tr>
                 <td style="border-top-style: solid; border-top-width: 1px; border-top-color: #CCCCCC">Total </td>
                 <td style="border-top-style: solid; border-top-width: 1px; border-top-color: #CCCCCC">
                     <asp:TextBox ID="txtEDTotal" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>--%>
         </table>
         <br />
         <table cellpadding="5" cellspacing="0">
             <tr>
                 <td style="width: 150px">Total CTC(PM)</td>
                 <td >
                     <asp:TextBox ID="txtCTCPM" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td>Total CTC(PA)</td>
                 <td >
                     <asp:TextBox ID="txtCTCPA" runat="server"></asp:TextBox>
                 </td>
             </tr>
         </table>
         <br />
         <table cellpadding="5" cellspacing="0" id="tblNet" runat="server" visible="false">
             <tr>
                 <td style="width:150px"><b>Net</b></td>
                 <td >
                     <asp:TextBox ID="txtNetSal" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
         </table>
         <br />
         <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Print" Visible="False" />
         <asp:Label ID="lblStatus" runat="server" Visible="false"></asp:Label><br />




</asp:Content>

