<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true" CodeFile="TourExpansesClaim.aspx.cs" Inherits="Modules_HR_TourExpansesClaim" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/css/londinium-theme.css" rel="stylesheet" type="text/css" />
    <link href="/css/styles.css" rel="stylesheet" type="text/css" />
    <link href="/css/icons.css" rel="stylesheet" type="text/css" />
   <script src="../../js/jquery.min.js"></script>
   
    <script src="../../js/jquery.datetimepicker.js"></script>
    <link href ="../../js/jquery.datetimepicker.css" rel="stylesheet" type="text/css"  />
    <script>
        $('.datetimetxt').datetimepicker({
            dayOfWeekStart: 1,
            lang: 'en'
        });
        $('#datetimepicker').datetimepicker({ value: '2015/04/15 05:03', step: 10 });

    </script>
  
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    
    <div class="page-header">
        <div class="page-title">
            <h3>TOUR EXPENSES CLAIM FORM</h3>
        </div>
    </div>
     <div class="panel panel-default">
         <div class="panel-heading">
             <h6 class="panel-title"><i class="icon-file"></i>Employee Information</h6>
             <span class="pull-right"><asp:Button ID="btnAddnew" CssClass="btn btn-warning" runat="server" EnableTheming ="false"  Text="Add New" OnClick="btnAddnew_Click"/></span>
         </div>
         <div class="panel-body">
             <div class="datatable-tasks">
                 <asp:GridView ID="hai" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
                     <Columns >
                         <asp:BoundField DataField ="TourID" HeaderText ="Tour Id" />
                         <asp:BoundField DataField ="TourNo" HeaderText ="Tour No" />
                        <%-- <asp:BoundField DataField ="EMP_FRST_NAME" HeaderText ="Employee Name" />
                         <asp:BoundField DataField ="Designation" HeaderText ="Desgination" />
                         <asp:BoundField DataField ="Department" HeaderText ="Department" />--%>
                         <asp:TemplateField HeaderText="Local Conveyance" HeaderStyle-CssClass="text-center">
                             <ItemTemplate >
                               <span class="text-center">
                                <asp:LinkButton ID="lbtnLC" runat="server" CssClass="btn btn-icon btn-primary" PostBackUrl='<%# "~/Modules/HR/TourExpansesClaim.aspx?Tid=" + Eval("TourID") %>'><i class="icon-factory"></i></asp:LinkButton>
                               </span>
                             </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="Smaller" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Lodging Expanses" HeaderStyle-CssClass="text-center">
                             <ItemTemplate >
                               <span class="text-center">
                                <asp:LinkButton ID="lbtnLE" runat="server" CssClass="btn btn-icon btn-primary" PostBackUrl='<%# "~/Modules/HR/TourExpansesClaim.aspx?Tid=" + Eval("TourID") %>'><i class="icon-factory"></i></asp:LinkButton>
                               </span>
                             </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="Smaller" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Daily Allowances" HeaderStyle-CssClass="text-center">
                             <ItemTemplate >
                               <span class="text-center">
                                <asp:LinkButton ID="lbtnDA" runat="server" CssClass="btn btn-icon btn-primary" PostBackUrl='<%# "~/Modules/HR/TourExpansesClaim.aspx?Tid=" + Eval("TourID") %>'><i class="icon-factory"></i></asp:LinkButton>
                               </span>
                             </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="Smaller" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="text-center">
                        <ItemTemplate>
                            <span class="text-center">
                                <asp:LinkButton ID="lbtnEdit" runat="server" CssClass="btn btn-icon btn-success" PostBackUrl='<%# "~/Modules/HR/TourExpansesClaim.aspx?Tid=" + Eval("TourID") %>'><i class="icon-wrench2"></i></asp:LinkButton>
                            </span>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="Smaller" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center">
                        <ItemTemplate>
                            <span class="text-center">
                                <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="btn btn-icon btn-danger" OnClick="lbtnDelete_Click"><i class="icon-remove3"></i></asp:LinkButton>
                            </span>
                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                     </Columns>
                 </asp:GridView>

                 <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT * FROM [Tour_Expanses_Claim_tbl]"></asp:SqlDataSource>
            <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
             </div>
             
             <Table ID="tbldet" style ="width :100%"  runat ="server" visible ="false"  >
                 <tr>
                     <td ><div class="panel panel-info">
                    <div class="panel-heading">
                        <h6 class="panel-title"><i class="icon-pencil"></i>Tour Expanses Claim Basic Details</h6>
                    </div>
                         <div class="panel-body">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                             <h6 class="panel-title">Employee Information</h6>
                            </div>
                            <div class="panel-body">
                                <div class="form-group">
                                    <div class="col-md-6">
                                        <label>Employee Name : <span class="mandatory">*</span></label>
                                <asp:DropDownList ID="ddlEmp" TabIndex="2" Width="100%" CssClass="select-full" runat="server">
                                </asp:DropDownList>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Location: <span class="mandatory">*</span></label>
                                <asp:DropDownList ID="ddlLocation" TabIndex="2" Width="100%" CssClass="select-full"  runat="server">
                            </asp:DropDownList>
                                
                            </div>
                                </div>
                                <div class="form-group">
                       
                            <div class="col-md-6">
                                <label>Department : <span class="mandatory">*</span></label>
                                <asp:TextBox ID="txtDept" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>

                            <div class="col-md-6">
                                <label>Designation : <span class="mandatory">*</span></label>
                                <asp:TextBox ID="txtDesg" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        
                    </div>
                                <div class="form-group">
                       
                            <div class="col-md-6">
                                <label>Tour Code : <span class="mandatory">*</span></label>
                                <asp:TextBox ID="txtTourNo" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>

                            <div class="col-md-6">
                                <label>Tour Date : <span class="mandatory">*</span></label>
                                <asp:TextBox ID="txtTourDt" CssClass="form-control" type="datepic" runat="server"></asp:TextBox>
                            </div>
                        
                    </div>
                            </div>

                        </div>
                    </div>
                         <div class ="panel-body">
                             <div  class="panel panel-info">
                                 <div class="panel-heading">
                                    <h6  class="panel-title">Tour Brief</h6>
                                 </div>
                                 <div class ="panel-body">
                                    <div class="form-group">
                       
                                    <div class="col-md-6">
                                <label>Place of Visit : <span class="mandatory">*</span></label>
                                <asp:TextBox ID="txtVisitPlace" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>

                            <div class="col-md-6">
                                <label>No of days of Visit : <span class="mandatory">*</span></label>
                                <asp:TextBox ID="txtDaysCount" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        
                            </div>
                                    <div class="form-group">
                       
                                    <div class="col-md-6">
                                <label>Depature Date & Time  : <span class="mandatory">*</span></label>
                                <asp:TextBox ID="txtDepaDt" CssClass="form-control datetimetxt" runat="server"></asp:TextBox>
                                </div>

                            <div class="col-md-6">
                                <label>Arrival Date & Time: <span class="mandatory">*</span></label>
                                <asp:TextBox ID="txtArrDt" CssClass="datetimetxt form-control" runat="server"></asp:TextBox>
                            </div>
                        
                            </div>
                                 </div>
                             </div>
                         </div>
                         <div class="panel-body">
                             <div class ="panel panel-info">
                                 <div class="panel-heading">
                                    <h6 class="panel-title">A) Travel Expanses</h6>
                                 </div>
                                 <div class ="panel-body">
                                     <div class="form-group">
                       
                                    <div class="col-md-6">
                                <label>From : <span class="mandatory">*</span></label>
                                <asp:TextBox ID="txtFrom" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>

                            <div class="col-md-6">
                                <label>To : <span class="mandatory">*</span></label>
                                <asp:TextBox ID="txtTo" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        
                            </div>
                                     <div class="form-group">
                       
                                    <div class="col-md-6">
                                <label>Mode Of Travel : <span class="mandatory">*</span></label>
                                <asp:DropDownList ID="ddlTravelMode" TabIndex="2" Width="100%" CssClass="select-full" runat="server">
                                    <asp:ListItem >--Select--</asp:ListItem>
                                   <asp:ListItem>Flight</asp:ListItem>
                                    <asp:ListItem>Bus</asp:ListItem>
                                    <asp:ListItem>Train</asp:ListItem>
                                    <asp:ListItem>Ship</asp:ListItem>
                                </asp:DropDownList>
                                </div>

                            <div class="col-md-6">
                                <label>Class : <span class="mandatory">*</span></label>
                                <asp:DropDownList ID="ddlClass" TabIndex="2" Width="100%" CssClass="select-full" runat="server">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                <asp:ListItem Value="1">General</asp:ListItem>
                                <asp:ListItem Value="2">A.C.</asp:ListItem>
                                <asp:ListItem Value="3">Sleeper</asp:ListItem>
                                <asp:ListItem Value="4">Flight</asp:ListItem>
                                <asp:ListItem Value="5">One-Tier</asp:ListItem>
                                <asp:ListItem Value="6">Two-Tier</asp:ListItem>
                                <asp:ListItem Value="7">Three-Tier</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        
                            </div>
                                     <div class="form-group">
                       
                                    <div class="col-md-6">
                                <label>Remarks : <span class="mandatory">*</span></label>
                                <asp:TextBox ID="txtRemarks" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>

                            <div class="col-md-6">
                                <label>Total : <span class="mandatory">*</span></label>
                                <asp:TextBox ID="txtTotal" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        
                            </div>
                                     <div class ="form-group">
                                         <div class="col-md-6">
                                            <asp:Button ID="btnAdd"  CssClass="btn btn-warning" runat="server" OnClick ="btnAdd_Click" EnableTheming ="false"  Text="Add" />
                                            <asp:Label ID="lblDetTxId" runat ="server" Visible ="false" ></asp:Label>&nbsp;<asp:Button ID="btnRefresh"  CssClass="btn btn-warning" runat="server" EnableTheming ="false"  Text="Refresh" />
                    
                                         </div>
                                     </div>
                                     <div class ="form-group">
                                          <div class="col-md-6">
                                              <asp:GridView ID="gvTravelExp" runat ="server" Width ="100%" AutoGenerateColumns ="false" >
                                                  <Columns>
                                                      <asp:BoundField DataField ="DetTXId" HeaderText ="Id" />
                                                      <asp:BoundField DataField ="From" HeaderText ="From" />
                                                      <asp:BoundField DataField ="To" HeaderText ="To" />
                                                      <asp:BoundField DataField ="TravelMode" HeaderText ="Mode of Travel" />
                                                      <asp:BoundField DataField ="Class" HeaderText ="Class" />
                                                      <asp:BoundField DataField ="Total" HeaderText ="Total" />
                                                      <asp:BoundField DataField ="Remarks" HeaderText ="Remarks" />
                                                  </Columns>
                                              </asp:GridView>
                                          </div>
                                      </div>
                                    
                                 </div>
                             </div>
                         </div>
                         
                 </div></td>
                 </tr>
             </Table>
             
         </div>
     </div>
    
</asp:Content>

