<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendMail.aspx.cs" Inherits="dev_pages_SendMail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat ="server" >
    <title></title>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <link rel="stylesheet" type="text/css" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
    <link rel="stylesheet" href="custom.css">
    <meta name="viewport" content="width=device-width,initial-scale=1" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.1/jquery.min.js"></script>
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../select/select2.css" rel="stylesheet" />
    <script src="../select/select2.js"></script>
   <style type ="text/css" >
       body{
 background:#e5e5e5; 
 font-family: sans-serif;
}

.msg_box{
 position:fixed;
 bottom:-5px;
 width:450px;
 background:white;
 border-radius:5px 5px 0px 0px;
}

.msg_head{ 
 background:black;
 color:white;
 padding:8px;
 font-weight:bold;
 cursor:pointer;
 border-radius:5px 5px 0px 0px;
}

.msg_body{
 background:white;
 height:200px;
 width :100%;
 font-size:15px;
 padding:15px;
 overflow:auto;
 overflow-x: hidden;
}
.msg_input{
 width:100%;
 height: 55px;
 border: 2px solid white;
 border-top:2px solid #DDDDDD;
 -webkit-box-sizing: border-box; 
 -moz-box-sizing: border-box;   
 box-sizing: border-box;  
}

.close{
 float:right;
 cursor:pointer;
}
.minimize{
 float:right;
 cursor:pointer;
 padding-right:5px;
 
}

.msg-left{
 position:relative;
 background:#e2e2e2;
 padding:5px;
 min-height:10px;
 margin-bottom:5px;
 margin-right:10px;
 border-radius:5px;
 word-break: break-all;
}

.msg-right{
 background:#d4e7fa;
 padding:5px;
 min-height:15px;
 margin-bottom:5px;
 position:relative;
 margin-left:10px;
 border-radius:5px;
 word-break: break-all;
}
/**** Slider Layout Popup *********/
</style>
    <script type ="text/javascript" >
        $(document).ready(function () {

            var arr = []; // List of users 

            $(document).on('click', '.msg_head', function () {
                var chatbox = $(this).parents().attr("rel");
                $('#msg_wrap').show();
                $('[rel="' + chatbox + '"] .msg_wrap').slideToggle('fast');
               
                return false;
            });


            $(document).on('click', '.close', function () {
                var chatbox = $(this).parents().parents().attr("rel");
                $('#msg_wrap').hide();
                //arr.splice($.inArray(chatbox, arr), 1);
                ////displayChatBox();
                //return false;
                //$('#msg_wrap').hide();
            });

            //$(document).on('click', '#sidebar-user-box', function () {

            //    var userID = $(this).attr("class");
            //    var username = $(this).children().text();

            //    if ($.inArray(userID, arr) != -1) {
            //        arr.splice($.inArray(userID, arr), 1);
            //    }

            //    arr.unshift(userID);
            //    chatPopup = '<div class="msg_box" style="right:270px" rel="' + userID + '">' +
            //       '<div class="msg_head">' + username +
            //       '<div class="close">x</div> </div>' +
            //       '<div class="msg_wrap"> <div class="msg_body"> <div class="msg_push"></div> </div>' +
            //       '<div class="msg_footer"><textarea class="msg_input" rows="4"></textarea></div>  </div>  </div>';
                
            //    $("body").append(chatPopup);
            //    displayChatBox();
            //});


            //function displayChatBox() {
            //    i = 0; 
            //    j = 0; 

            //    $.each(arr, function (index, value) {
            //        if (index < 4) {
            //            $('[rel="' + value + '"]').css("right", i);
            //            $('[rel="' + value + '"]').show();
            //            i = i + j;
            //        }
            //        else {
            //            $('[rel="' + value + '"]').hide();
            //        }
            //    });
            //}

        });
    </script>

</head>
<body runat ="server" >
    <form id="form1" runat="server">
     <script>

         $(document).ready(function () {
             $('#<%=Books.ClientID%>').select2({ placeholder: 'Find and Select Books' });


                //$("#ContentPlaceHolder1_Books").select2({ placeholder: 'Find and Select Books' });


            });


            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                //Binding Code Again
                $(<%=Books.ClientID%>).select2({ placeholder: 'Find and Select Books' });
         }




         </script>
    <script>

        $(document).ready(function () {
            $('#<%=Books1.ClientID%>').select2({ placeholder: 'Find and Select Books1' });


             //$("#ContentPlaceHolder1_Books").select2({ placeholder: 'Find and Select Books' });


         });


         Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
         function EndRequestHandler(sender, args) {
             //Binding Code Again
             $(<%=Books1.ClientID%>).select2({ placeholder: 'Find and Select Books' });
            }




         </script>
    
    <div id="msg_box"  class="msg_box" style="right:10px" rel="' + userID + '">
        <div class="msg_head">Send Message
         <div class="close">x</div> </div>
         <div class="msg_wrap" style="display: none"> 
             <div class="msg_body"  >
                       <div class="msg_push" ></div> 

                 <div class ="row">
                     <div class="col-md-11">
                <select id="Books" class="form-control" style ="width :400px" runat="server"></select>
                         <asp:SqlDataSource ID="usersds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [USER_ID], [USER_NAME] FROM [YANTRA_USER_DETAILS] where EXPIRY_DATE >='2019-12-31 00:00:00.000' ORDER BY [USER_NAME]"></asp:SqlDataSource>
                                        <asp:HiddenField ID="hffromuid1" runat="server" />
                <script>
                $(document).ready(function () {
                $("#Books").select2({ placeholder: 'Send Message To' });
                });
                </script>
            </div> 
                     </div> 
                <br />
                 <div class ="row">
                     <div class="col-md-11">
                         <select id="Books1" class="form-control" style ="width :400px" runat="server"></select>

                                           <script>
                                               $(document).ready(function () {
                                                   $("#Books1").select2({ placeholder: 'CC : ' });
                                               });
                                               </script>
                     </div>
                 </div>
             <br />
                 <div class ="row">
                     <div class="col-md-12">
                         <asp:TextBox class ="form-control" TextMode ="MultiLine"  runat ="server" id="txtsub" placeholder ="Subject" ></asp:TextBox>
                     </div>
                 </div>
               </div>
         <div class="msg_footer"><asp:TextBox class="msg_input" id="txtmsg" TextMode ="MultiLine" runat ="server"  rows="4"></asp:TextBox>
             <asp:Button id="btnsend" runat ="server"  CssClass="btn btn-lg btn-danger "   OnClick ="btnsend_Click" Text ="send" EnableTheming ="false"  ></asp:Button>
         </div>  

         </div>  

    </div>
   </form> 
</body>
</html>
