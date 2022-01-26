<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreditApproval.aspx.cs" Inherits="Modules_SM_CreditApproval" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1, maximum-scale=1" />
    <title>Valueline</title>
     

    <link href="/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/css/londinium-theme.css" rel="stylesheet" type="text/css" />
    <link href="/css/styles.css" rel="stylesheet" type="text/css" />
    <link href="/css/icons.css" rel="stylesheet" type="text/css" />
   

       
  
  
     <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"  type="text/javascript"></script>   

    <script src="http://ajax.aspnetcdn.com/ajax/jquery.validate/1.12.0/jquery.validate.min.js"  type="text/javascript"></script>  
  

</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
             <ControlBundles>
             <cc1:ControlBundle Name="Group2" />
             </ControlBundles>
         </cc1:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"  >
            <ContentTemplate>
        <%--<script type="text/javascript">
            Sys.Application.add_load(initdropdown);
        </script>--%>

                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h6 class="panel-title">Dispatch Details</h6>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <label class="col-sm-2 control-label text-right">Credit APproval No. :<asp:Label
                                        ID="Label47" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label></label>
                                <div class="col-sm-4">
                                        <asp:TextBox ID="txtCRANo" CssClass ="form-control" runat ="server" ReadOnly ="true"  ></asp:TextBox>
                                    
                                </div>
                                <label class="col-sm-2 control-label text-right"> :</label>
                            </div>
                        </div>

                        <div class="panel-body">
                            <div class="form-group">
                                <label class="col-sm-2 control-label text-right">Cust Name. :<asp:Label
                                        ID="Label1" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label></label>
                                <div class="col-sm-4">
                                        <asp:TextBox ID="txtCustName" CssClass ="form-control" runat ="server" ReadOnly ="true"  ></asp:TextBox>
                                    
                                </div>
                                <label class="col-sm-2 control-label text-right">Customer Mobile :</label>
                                  <div class="col-sm-4">
                                        <asp:TextBox ID="txtCustomerMobile" CssClass ="form-control" runat ="server" ReadOnly ="true"  ></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>

                        <div class="panel-body">
                            <div class="form-group">
                                <label class="col-sm-2 control-label text-right">Cust Email. :<asp:Label
                                        ID="Label2" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label></label>
                                <div class="col-sm-4">
                                        <asp:TextBox ID="txtcustomerEmail" CssClass ="form-control" runat ="server" ReadOnly ="true"  ></asp:TextBox>
                                    
                                </div>
                                <label class="col-sm-2 control-label text-right">"Customer Address :</label>
                                  <div class="col-sm-4">
                                        <asp:TextBox ID="txtcustAddress" CssClass ="form-control" runat ="server" ReadOnly ="true"  ></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <label class="col-sm-2 control-label text-right">PO Value. :<asp:Label
                                        ID="Label3" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label></label>
                                <div class="col-sm-4">
                                        <asp:TextBox ID="txtPOValue" CssClass ="form-control" runat ="server" ReadOnly ="true"  ></asp:TextBox>
                                    
                                </div>
                                <label class="col-sm-2 control-label text-right">"Total Dispatch Value :</label>
                                  <div class="col-sm-4">
                                        <asp:TextBox ID="txtDispatchValue" CssClass ="form-control" runat ="server" ></asp:TextBox>
                                    <asp:Label runat ="server" Text="Incl GST" ForeColor ="Red" ></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <label class="col-sm-2 control-label text-right">Credit Approval Amount :<asp:Label
                                        ID="Label4" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label></label>
                                <div class="col-sm-4">
                                        <asp:Label ID="lblPaymentTerms" Visible ="false"  runat ="server" ForeColor ="Red"></asp:Label>
                                    <asp:TextBox ID="txtCreditAmt" CssClass ="form-control " runat ="server" ></asp:TextBox>
                                </div>
                                <label class="col-sm-2 control-label text-right">"will collect the payment on :</label>
                                  <div class="col-sm-4">
                                         <asp:TextBox ID="txtDays" CssClass ="form-control" runat ="server" ></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="panel-heading">
                            <h6 class="panel-title">Account Details</h6>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <label class="col-sm-2 control-label text-right">Cr/DR Balance as on Date :<asp:Label
                                        ID="Label5" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label></label>
                                <div class="col-sm-4">
                                        <asp:TextBox ID="txtCR" CssClass ="form-control " runat ="server"  ></asp:TextBox>
                                    <asp:RadioButton ID="rdbWithPo" runat="server" AutoPostBack="True"
                                        GroupName="as"  Text="DR"></asp:RadioButton>
                                        <asp:RadioButton ID="rdbWithoutPo" runat="server" AutoPostBack="True" GroupName="as"
                                         Text="CR" Checked="True"></asp:RadioButton>
                                </div>
                                <label class="col-sm-2 control-label text-right">Approx. Value of Unbiled DC's :</label>
                                  <div class="col-sm-4">
                                        <asp:TextBox ID="txtUDC" CssClass ="form-control" runat ="server"  ></asp:TextBox>
                                      <asp:RadioButton ID="rdbDRUnbilledDcs" runat="server" AutoPostBack="True"
                                        GroupName="as1"  Text="DR"></asp:RadioButton>
                                        <asp:RadioButton ID="rdbCRUnbilledDcs" runat="server" AutoPostBack="True" GroupName="as1"
                                         Text="CR" Checked="True"></asp:RadioButton>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <label class="col-sm-2 control-label text-right">Cr/Dr Balance of Other Branches :<asp:Label
                                        ID="Label6" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label></label>
                                <div class="col-sm-4">
                                        <asp:TextBox ID="txtOther" CssClass ="form-control" runat ="server"  ></asp:TextBox>
                                    <asp:RadioButton ID="rdbDROtherBranches" runat="server" AutoPostBack="True"
                                        GroupName="as2"  Text="DR"></asp:RadioButton>
                                        <asp:RadioButton ID="rdbCROtherBranches" runat="server" AutoPostBack="True" GroupName="as2"
                                         Text="CR" Checked="True"></asp:RadioButton>
                                </div>
                                <label class="col-sm-2 control-label text-right">>Cr/Dr Balance of Other Projects :</label>
                                  <div class="col-sm-4">
                                        <asp:TextBox ID="txtOtherProjects" CssClass ="form-control" runat ="server" ></asp:TextBox>
                                    <asp:RadioButton ID="rdbDROtherPro" runat="server" AutoPostBack="True"
                                        GroupName="as3"  Text="DR"></asp:RadioButton>
                                        <asp:RadioButton ID="rdbCROtherPro" runat="server" AutoPostBack="True" GroupName="as3"
                                         Text="CR" Checked="True"></asp:RadioButton>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <label class="col-sm-2 control-label text-right">Prepared By :<asp:Label
                                        ID="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label></label>
                                <div class="col-sm-4">
                                       <asp:DropDownList ID="ddlAccId" CssClass ="form-control" runat ="server"  ></asp:DropDownList>
                                    
                                </div>
                                <label class="col-sm-2 control-label text-right">Managemnet :</label>
                                  <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlCMD" CssClass ="form-control" runat ="server"  ></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <label class="col-sm-2 control-label text-right">Remarks :<asp:Label
                                        ID="Label8" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label></label>
                                <div class="col-sm-6">
                                       <asp:TextBox ID="txtRmks" runat ="server" CssClass ="form-control " TextMode ="MultiLine"  ></asp:TextBox>
                                </div>
                                
                            </div>
                        </div>
                        <div>
                                        <asp:Button ID="btnSavepopup" OnClick ="btnSavepopup_Click" CssClass ="btn btn-success" runat="server" Text="Save" EnableTheming="False" />
                                        <asp:Button ID="btnPrint" runat="server" CssClass ="btn btn-info" Text="Print" EnableTheming="False" OnClick="btnPrint_Click" />

                        </div>
                        <div>
                            <asp:DropDownList ID="ddlExec" runat ="server" Visible ="false" ></asp:DropDownList>
                            <asp:DropDownList ID="ddlCust" CssClass ="form-control " Visible ="false"  runat ="server" ></asp:DropDownList>
                            <asp:DropDownList ID="ddlComp" runat ="server"  Visible ="false"></asp:DropDownList>
                            <asp:DropDownList ID="ddlSO" runat ="server" Visible ="false" ></asp:DropDownList>

                        </div>
                    </div>
                </div>
                </ContentTemplate> 
            </asp:UpdatePanel> 
    </form>
</body>
</html>
