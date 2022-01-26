<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DCDoc1.aspx.cs" Inherits="Modules_Inventory_DCDoc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1, maximum-scale=1" />
    <title>Valueline</title>
      <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"  
type="text/javascript"></script>   
<!--include jQuery Validation Plugin-->  
<script src="http://ajax.aspnetcdn.com/ajax/jquery.validate/1.12.0/jquery.validate.min.js"  
type="text/javascript"></script>  
  
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.12/css/jquery.dataTables.min.css" type="text/css"/>

     <link href="/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/css/londinium-theme.css" rel="stylesheet" type="text/css" />
    <link href="/css/styles.css" rel="stylesheet" type="text/css" />
    <link href="/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="/css/select2.min.css" rel="stylesheet" type="text/css" />

       <link rel="stylesheet" href='https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.css'
        media="screen" />
   <%-- <link rel="stylesheet" href='https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.css'
        media="screen" />--%>
     <script type="text/javascript" src="https://code.jquery.com/jquery-1.10.2.js"></script>
     <script type="text/javascript" src="https://cdn.datatables.net/1.10.12/js/jquery.dataTables.min.js"></script>


       
  
     <script type="text/javascript">


         $(document).ready(function () {

             $(function () {
                 initdropdown();

             })

         });



         function initdropdown() {
             $('.select-full').select2();
             //$("#datatable-tasks").dataTable();

         }


    </script>


</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
             <ControlBundles>
             <cc1:ControlBundle Name="Group2" />
             </ControlBundles>
         </cc1:ToolkitScriptManager>

    
    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
        <ContentTemplate>
           
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h6 class="panel-title">DC Details</h6>
                </div>
                <div class="panel-body">
                    <div class ="form-group">
                    <label class="col-sm-2 control-label text-right">DC No:</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtClientsName" CssClass ="form-control " onkeypress="return check(event)" runat="server"></asp:TextBox>
                            </div>
                    <label class="col-sm-2 control-label text-right">DC Date :</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtDCNo" CssClass ="form-control " runat="server"></asp:TextBox>
                            </div>
                </div>
                </div>
            </div>
            <!-- /breadcrumbs line -->

            <div class="panel panel-default">
                <div class="panel-heading">
                    <h6 class="panel-title">Documents </h6>
                </div>
                <div class="panel-body">


                    <div class ="form-group">
                    <label class="col-sm-2 control-label text-right">Doc Uploading Date:</label>
                            <div class="col-sm-4">
                              <asp:TextBox ID="txtfloorplanreceiveddate"  CssClass="form-control" type="datepic" runat="server"></asp:TextBox>
                                        <asp:Image
                                            ID="Image1" class="ui-datepicker-trigger" runat="server" ImageUrl="~/images/calendar.png"></asp:Image>
                                      
                            </div>
                    <label class="col-sm-2 control-label text-right">Document: </label>
                            <div class="col-sm-4">
                               <asp:FileUpload ID="FileUpload2" AllowMultiple="true" CssClass="styled form-control" runat="server" />
                            </div>
                </div>
                    <div class="form-group">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label>Remarks :  </label>
                                        <asp:TextBox ID="txtfloorplanremarks" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions text-right">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSave_Click" />
                            </div>
                    <div class="panel panel-default">
        <div class="panel-heading">
            <h6 class="panel-title">Documnet Details Received</h6>
        </div>
        <div class="panel-body">
            <div class="">
                <asp:GridView ID="gvFloorPlan" CssClass="table table-bordered" Width="100%" runat="server" AutoGenerateColumns="False">
                                    <Columns>

                                        <asp:BoundField DataField="DCDOC_ID" HeaderText="Sl.No" SortExpression="DCDOC_ID">
                                            <HeaderStyle Font-Size="Smaller" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="ISSUEDDATE" HeaderText="Received Date" SortExpression="ISSUEDDATE" DataFormatString="{0:dd/MM/yyyy}">
                                            <HeaderStyle Font-Size="Smaller" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="REMARKS" HeaderText="Remarks" SortExpression="REMARKS">
                                            <HeaderStyle Font-Size="Smaller" />
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Documents" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <span class="text-center">
                                                    <asp:HyperLink ID="lbtnDocuments" runat="server" CssClass="btn btn-icon btn-success" Target="_blank" NavigateUrl='<%# "~/Content/FloorPlanDrawings/" + Eval("FILENAME") %>'><i class="icon-attachment"></i></asp:HyperLink>
                                                </span>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="Smaller" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <span class="text-center">
                                                    <asp:LinkButton ID="lbtnFloorDetails" runat="server" CssClass="btn btn-icon btn-danger" OnClick="lbtnFloorDetails_Click"><i class="icon-remove3"></i></asp:LinkButton>
                                                </span>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>


                </div></div>

             </div>

                    
                </div>
            </div>
        </ContentTemplate>

         <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>


    </asp:UpdatePanel>

    </form>
</body>
</html>