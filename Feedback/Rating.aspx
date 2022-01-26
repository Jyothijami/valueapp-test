<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rating.aspx.cs" Inherits="Rating" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat ="server" >
        <meta charset="utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <title>Customer Feedback Form</title>
        <!-- Latest compiled and minified CSS -->
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
        <!-- Optional theme -->
        <%--<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" />--%>
        <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>--%>
       <%-- <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" 
            crossorigin="anonymous"></script>--%>
        <link rel="stylesheet" href="form.css" />
        <%--<script src="form.js"></script>--%>

        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <script src="https://unpkg.com/jquery"></script>
        <script src="https://surveyjs.azureedge.net/1.0.69/survey.jquery.js"></script>
        <link href="https://surveyjs.azureedge.net/1.0.69/survey.css" type="text/css" rel="stylesheet" />
        <link rel="stylesheet" href="./index.css" />
         <script src="https://unpkg.com/jquery-bar-rating"></script>
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css" />
        <!-- Themes -->
        <link rel="stylesheet" href="https://unpkg.com/jquery-bar-rating@1.2.2/dist/themes/bars-1to10.css" />
        <link rel="stylesheet" href="https://unpkg.com/jquery-bar-rating@1.2.2/dist/themes/bars-movie.css" />
        <link rel="stylesheet" href="https://unpkg.com/jquery-bar-rating@1.2.2/dist/themes/bars-square.css" />
        <link rel="stylesheet" href="https://unpkg.com/jquery-bar-rating@1.2.2/dist/themes/bars-pill.css" />
        <link rel="stylesheet" href="https://unpkg.com/jquery-bar-rating@1.2.2/dist/themes/bars-reversed.css" />
        <link rel="stylesheet" href="https://unpkg.com/jquery-bar-rating@1.2.2/dist/themes/bars-horizontal.css" />

        <link rel="stylesheet" href="https://unpkg.com/jquery-bar-rating@1.2.2/dist/themes/fontawesome-stars.css" />
        <link rel="stylesheet" href="https://unpkg.com/jquery-bar-rating@1.2.2/dist/themes/css-stars.css" />
        <link rel="stylesheet" href="https://unpkg.com/jquery-bar-rating@1.2.2/dist/themes/bootstrap-stars.css" />
        <link rel="stylesheet" href="https://unpkg.com/jquery-bar-rating@1.2.2/dist/themes/fontawesome-stars-o.css" />
        <script src="https://unpkg.com/surveyjs-widgets/surveyjs-widgets.js"></script>

        <script src="https://cdnjs.cloudflare.com/ajax/libs/velocity/1.1.0/velocity.min.js"></script>
        <script src="emotion-ratings.js"></script>
       
    </head>
    <body runat ="server"  >
        <div class="container">
            <div class="span7">
                <%--<marquee direction="scroll" style="height: 25px">--%>
               <%-- <ul>--%>
                    <a style ="color:white; text-align :right  " href="http://Valueline.in/" target="_blank">www.valueline.in</a>
                <%--</ul>--%>
                    <%--</marquee>--%>
            </div>
            <%--<div class="container-box rotated">
                <button type="button" class="btn btn-info btn-lg turned-button" data-toggle="modal" data-target="#myModal">Rating Us</button>
            </div>--%>
            <!-- Modal -->
            <%--<div id="myModal" class="modal fade" role="dialog">--%>
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header" role="dialog"  data-dismiss="modal">
                            <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                            <h4 class="modal-title">
                                Your Feedback
                            </h4>
                        </div>
                        <div class="modal-body">
                            <form role="form" method="post" id="reused_form">
                                 <div id="surveyElement"></div>
                                    <div id="surveyResult"></div>

                                    <script type="text/javascript" src="./index.js"></script>
                            </form>
                           
                        </div>
                    </div>
                </div>
            <%--</div>--%>

        </div>
    </body>

</html>
