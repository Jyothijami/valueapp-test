<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FPSEdit.aspx.cs" Inherits="FPSEdit" %>

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

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
             <ContentTemplate>
                 <script type="text/javascript">
                     Sys.Application.add_load(initdropdown);
                 </script>

                 <div class="form-horizontal">
                     <div class="panel panel-default">
                         <div class="panel-heading">
                            <h6 class="panel-title">FPS Details</h6>
                         </div>
                         <div class="panel-body">
                             <div class="form-group">
                                 <label class="col-sm-2 control-label text-right"><asp:Label
                                        ID="Label47" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label></label>
                                 <div class="col-sm-4">
                                     <asp:DropDownList ID="ddlCustomerName" Visible ="false"  CssClass ="form-control" runat="server" AutoPostBack="True" ></asp:DropDownList>
                                 </div>
                                 <%--<label class="col-sm-2 control-label text-right">Sales Order No</label>--%>
                                 <div class="col-sm-4">
                                    <asp:DropDownList CssClass ="form-control" Visible ="false"  ID="ddlSalesOrderNo" runat="server" AutoPostBack="True" >
                                    </asp:DropDownList>
                                 </div>
                             </div>

                             <div class="form-group">
                                 <label class="col-sm-2 control-label text-right">Architect Name</label>
                                 <div class="col-sm-4">
                                     <asp:DropDownList ID="ddlArchitect" CssClass ="form-control" runat="server" 
                                     AutoPostBack="True"   ></asp:DropDownList>
                                     <asp:TextBox ID="txtArchitect" CssClass ="form-control" runat ="server" ></asp:TextBox>
                                 </div>
                                 <label class="col-sm-2 control-label text-right">Gross</label>
                                 <div class="col-sm-4">
                                    <asp:TextBox ID="txtPOAmt1" CssClass ="form-control" runat="server" ></asp:TextBox>
                                 </div>
                             </div>
                             <div class="form-group">
                                 <label class="col-sm-2 control-label text-right">NET</label>
                                 <div class="col-sm-4">
                                     <asp:TextBox ID="txtPOAmt" runat ="server" CssClass ="form-control" ></asp:TextBox>
                                 </div>
                                 <label class="col-sm-2 control-label text-right">Percentage</label>
                                 <div class="col-sm-4">
                                    <asp:TextBox ID="txtPerc" CssClass ="form-control" runat="server"  >0</asp:TextBox>
                                 </div>
                             </div>
                            <div class="form-group">
                                 <label class="col-sm-2 control-label text-right">Amount</label>
                                 <div class="col-sm-4">
                                     <asp:TextBox ID="txtArAmount" runat ="server" CssClass ="form-control" ></asp:TextBox>
                                     
                                 </div>
                                <label class="col-sm-2 control-label text-right" >Status</label>
                                 <div class="col-sm-4">
                                     <asp:DropDownList ID="ddlStatus" runat ="server" CssClass ="form-control" >
                                         <asp:ListItem >Paid</asp:ListItem>
                                         <asp:ListItem >Partially Paid</asp:ListItem>
                                         <asp:ListItem >Open</asp:ListItem>

                                     </asp:DropDownList>
                                     
                                 </div>
                                 
                             </div>
                            <div class="form-group">
                                 <label class="col-sm-2 control-label text-right">Remarks</label>
                                 <div class="col-sm-6">
                                     <asp:TextBox ID="txtArRemarks" runat ="server" CssClass ="form-control" TextMode ="MultiLine"  ></asp:TextBox>
                                 </div>
                                 
                             </div>
                             <div class="form-group">
                                 <div class="col-sm-8" style ="align-items :center ">
                                    <asp:Button ID ="btnSave" CssClass ="form-control " runat ="server" Text ="Update" OnClick ="btnSave_Click" />
                                 
                                 </div>
                                 
                             </div>
                         </div>
                     </div>
                 </div>

             </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
