<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="dev_pages_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <div class="panel panel-danger">

        <div class="panel-heading">
            <div class="panel-title">
               <h6><i class="icon-paragraph-justify"></i>Mobile Forms</h6>
            </div>
        </div>



        <div class="panel-body">

            <div class="block">

                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <div class="panel-title">
                                       
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="well">




                                <div class="list-group">





                                    <a href="DailyReportM.aspx" class="list-group-item">DailyReport</a>
                                    <a href="EMP_CR.aspx" class="list-group-item">Compalint Register</a>

                                    <a href="ToDoList1.aspx" class="list-group-item">ToDo List</a>
                                    <a href="LeaveApplication.aspx" id="quatationlink" runat="server" class="list-group-item">Leave Application</a>
                                    <a href="MobileApp_StockReport.aspx" class="list-group-item">Stock</a>
                                    <a href="DailyReportView.aspx" class="list-group-item">DailyReport View</a>
                                    <a href="../QrBackCam.aspx" class="list-group-item">QR Code Reader</a>




                                </div>
                            </div>
                                </div>
                            </div>



                           <%-- <span class="subtitle">Sales</span>--%>
                            
                        </div>




        </div>






    </div>













</asp:Content>

