<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/SalesMP1.master" AutoEventWireup="true" CodeFile="PRF.aspx.cs" Inherits="Modules_SM_PRF" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link href="../../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../select/select2.css" rel="stylesheet" />
    <script src="../../select/select2.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
     <div class="form-horizontal">
         <div class="page-header">
                    <div class="page-title">
                        <h3>Project Registration Form</h3>
                    </div>
         </div>
         <div class="breadcrumb-line">
                    <ul class="breadcrumb">
                        <li><a href="../SM/PRFMAST.aspx">PRF View</a></li>
                    </ul>
         </div>
         <div class="panel-heading">
                    <h6 class="panel-title">PRF Details</h6>
         </div>
         <div class="panel-body">
             <div class="form-group">
                 <label class="col-sm-2 control-label text-right">CRM No</label> 
                  <div class="col-sm-4">
                      <asp:TextBox ID="txtCRMNo" runat ="server" CssClass ="form-control"></asp:TextBox>
                  </div>
                 <label class="col-sm-2 control-label text-right">SR. No</label> 
                  <div class="col-sm-4">
                      <asp:TextBox ID="txtSrNo" runat ="server" CssClass ="form-control"></asp:TextBox>
                  </div>
             </div>
             <div class="form-group">
                 <label class="col-sm-2 control-label text-right">Date</label> 
                  <div class="col-sm-4">
                      <asp:TextBox ID="txtDate" runat ="server" type="datepic" CssClass ="form-control"></asp:TextBox>
                  </div>
                 <label class="col-sm-2 control-label text-right">Name Of the Dealer</label> 
                  <div class="col-sm-4">
                      <asp:TextBox ID="txtNOD" runat ="server" CssClass ="form-control"></asp:TextBox>
                  </div>
             </div>
             <div class="form-group">
                 <label class="col-sm-2 control-label text-right">Name Of the Project</label> 
                  <div class="col-sm-4">
                      <asp:TextBox ID="txtNOP" runat ="server" CssClass ="form-control"></asp:TextBox>
                  </div>
                 <label class="col-sm-2 control-label text-right">Commission %</label> 
                  <div class="col-sm-4">
                      <asp:TextBox ID="txtCommission" runat ="server" CssClass ="form-control"></asp:TextBox>
                  </div>
             </div>
             <div class="form-group">
                 <label class="col-sm-2 control-label text-right">Address</label> 
                  <div class="col-sm-8">
                      <asp:TextBox ID="txtAddress" runat ="server" TextMode ="MultiLine"  CssClass ="form-control"></asp:TextBox>
                  </div>
                 
             </div>
             <div class="form-group">
                 <label class="col-sm-2 control-label text-right">Main Contact Person</label> 
                  <div class="col-sm-4">
                      <asp:TextBox ID="txtContactPerson" runat ="server" CssClass ="form-control"></asp:TextBox>
                  </div>
                 <label class="col-sm-2 control-label text-right">Contact Number</label> 
                  <div class="col-sm-4">
                      <asp:TextBox ID="txtMobile" runat ="server" CssClass ="form-control"></asp:TextBox>
                  </div>
             </div>
             <div class="form-group">
                 <label class="col-sm-2 control-label text-right">Kind of Project</label> 
                  <div class="col-sm-4">
                      <asp:RadioButtonList ID="rdbProject" runat ="server" RepeatDirection ="Horizontal" RepeatColumns="4"    >
                          <asp:ListItem >Individual</asp:ListItem>
                          <asp:ListItem >Apartments</asp:ListItem>
                          <asp:ListItem >Malls</asp:ListItem>
                          <asp:ListItem >Airport</asp:ListItem>
                          <asp:ListItem >Cinema / PVR</asp:ListItem>
                          <asp:ListItem >Office Space</asp:ListItem>
                          <asp:ListItem >Showroom</asp:ListItem>

                      </asp:RadioButtonList>
                  </div>
                 <label class="col-sm-2 control-label text-right"> Number</label> 
                  <div class="col-sm-4">
                      <asp:TextBox ID="TextBox2" runat ="server" CssClass ="form-control"></asp:TextBox>
                  </div>
             </div>
         </div>
     </div>
</asp:Content>

