<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true" CodeFile="TourExpansesClaimDetails.aspx.cs" Inherits="Modules_HR_TourExpansesClaimDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <div class="page-header">
        <div class="page-title">
            <h3>Employee Details</h3>
        </div>
    </div>
    <div class="breadcrumb-line">
        <ul class="breadcrumb">
            <li><a href="TourExpansesClaim.aspx">TourExpansesClaim</a></li>
            <li class="active">TourExpanses Details</li>
        </ul>
    </div>
    <div class="panel panel-info">
        <div class="panel-body">
             <div class="panel panel-info">
                 
                  <div id="tblDetails" runat="server" width="100%">
                      <table style ="width :100%">
                          <tr>
                        <td style="text-align: right">Voucher No :
                        <asp:TextBox ID="txtVoucherNo" Enabled="false" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 5%"></td>
                        <td style="text-align: right">Date : 
                        </td>
                        <td style="text-align: left">

                            <asp:TextBox ID="txtDate" type="datepic" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                          <tr>
                        <td colspan="5">&nbsp;</td>

                    </tr>
                          <tr>
                        <td colspan="5" style="text-align: left;">
                            <i class="icon-pencil"></i><h5>Employee Details : </h5>
                        </td>
                    </tr>
                          <tr>
                              <td style ="text-align :right "><asp:Label runat ="server" Text ="Employee Name"  ></asp:Label></td>
                              <td  style ="text-align :left "><asp:DropDownList runat ="server" ID="ddlEmpName"></asp:DropDownList></td>
                              <td style="width: 5%"></td>
                              <td style ="text-align :right "></td>
                              <td style ="text-align :left "></td>
                          </tr>
                      </table>

                  </div>
             </div>
        </div>
    </div>
</asp:Content>

