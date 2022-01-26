<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerShowroomVisit.aspx.cs" Inherits="Feedback_CustomerShowroomVisit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    
<head runat="server">
    
    <link href="//netdna.bootstrapcdn.com/bootstrap/3.1.0/css/bootstrap.min.css" rel="stylesheet" />
<link href="//netdna.bootstrapcdn.com/font-awesome/4.0.3/css/font-awesome.min.css" rel="stylesheet" />
<%--<script src="https://code.jquery.com/jquery-2.1.0.js"></script>--%>
<script src="//netdna.bootstrapcdn.com/bootstrap/3.1.0/js/bootstrap.min.js"></script>
    <meta charset="utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
        <link rel="stylesheet" href="form.css" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Customer Feedback Form</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <div class="span7">
              <a style ="color:white; text-align :right  " href="http://Valueline.in/" target="_blank">www.valueline.in</a>
        </div>
        <div class="modal-dialog modal-full-height modal-right modal-notify modal-info" role="document">
            <div class="modal-content">
                <div class="modal-header" role="dialog"  data-dismiss="modal">
                    <h4 class="modal-title">Your Feedback</h4>
                </div>
                <div style ="background-color:#F5F5F5" >
                    <div class="row">
                        <div class="alert alert-info "><strong >1.</strong>
                            <asp:Label runat ="server" ID="lbl1" Font-Bold ="true"   Text="Did you find what you were looking for?" ></asp:Label>
                            </div> 
                            <br />
                            <div >
                                
                                    <asp:RadioButtonList ID="rdblIndentfor"  runat="server" AutoPostBack="True" RepeatDirection="Horizontal" 
                                        RepeatLayout="Flow">
                                                 <asp:ListItem >Yes</asp:ListItem>
                                                 <asp:ListItem>No</asp:ListItem>
                                                 <asp:ListItem >Yes, but price is high</asp:ListItem>
                                            </asp:RadioButtonList>
                            </div>
                        <%--</div>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
