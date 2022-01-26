<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmpRating.aspx.cs" Inherits="Survey_EmpRating" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />
    <title>Feedback</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="author" content="colorlib.com" />

    <!-- MATERIAL DESIGN ICONIC FONT -->
    <link rel="stylesheet" href="fonts/material-design-iconic-font/css/material-design-iconic-font.css" />

    <!-- STYLE CSS -->
    <link rel="stylesheet" href="css/style.css" />
        <link rel="stylesheet" href="./index.css" />

    <style>
@import url(http://fonts.googleapis.com/css?family=Roboto:500,100,300,700,400);

* {
  margin: 0;
  padding: 0;
  font-family: roboto;
}

/*body { background: #000; }*/

.cont {
  width: 93%;
  max-width: 350px;
  text-align: center;
  margin: 4% auto;
  padding: 20px 0;
  background: #111;
  color: #EEE;
  border-radius: 5px;
  border: thin solid #444;
  overflow: hidden;
}
.container{
    
   display:inline-block;
  position: relative;
  padding-left: 10px;
  margin-bottom: 10px;
  cursor: pointer;
  font-size: 100%;
     margin-left: 5px;
  margin-top: 2px;
  border-radius: 10px;
  margin:-2px;
  line-height :30px;
 
  
}

hr {
  margin: 20px;
  border: none;
  border-bottom: thin solid rgba(255,255,255,.1);
}

div.title { font-size: 2em; }

h1 span {
  font-weight: 300;
  color: #Fd4;
}

div.stars {
  width: 370px;
  display: inline-block;
}

input.star { display: none; }

label.star {
  float: right;
  padding: 15px;
  font-size: 46px;
  color: #444;
  transition: all .2s;
}

input.star:checked ~ label.star:before {
  content: '\f005';
  color: #FD4;
  transition: all .25s;
}

input.star-5:checked ~ label.star:before {
  color: #6d7f52;
  text-shadow: 0 0 20px #952;
}
input.star-4:checked ~ label.star:before {
  color: lightgreen;
  text-shadow: 0 0 20px #952;
}
input.star-2:checked ~ label.star:before {
  color: #F62;
  /*text-shadow: 0 0 20px #952;*/
}

input.star-1:checked ~ label.star:before { color: Red; }

label.star:hover { transform: rotate(-15deg) scale(1.3); }

label.star:before {
  content: '\f006';
  font-family: FontAwesome;
}
</style>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.validate/1.12.0/jquery.validate.min.js"  
type="text/javascript"></script>  
   <link href="http://www.cssscript.com/wp-includes/css/sticky.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="http://netdna.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" />
        <link href="https://surveyjs.azureedge.net/1.0.69/survey.css" type="text/css" rel="stylesheet" />

     <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
     <script type="text/javascript" src='https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.js'> </script>
    <link rel="stylesheet" href='https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.css'
        media="screen" />
     <script type="text/javascript">
         function hi() {
             // event.preventDefault();
             swal({
                 title: 'Thank You',
                 text: "for your valuable feedback ",
                 type: 'success',
                 confirmButtonColor: '#3085d6',
                 confirmButtonText: 'Ok'
             })


         }


    </script>
   
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="form-content" >
							
             <%--<div class="form-row">
                <div class="form-holder w-100">
                    <div class="form-header">--%>
								<h3>Review</h3>
							
                                <p>1. Ability to Accomplish Responsibilities</p>
							
							<div class="form-row">
								<div class="form-holder w-100">
                                    <div class="radiobtn">
                                            <asp:RadioButtonList ID="rdbRecommend" CssClass="checkbox-circle" EnableTheming ="false"  runat ="server"  >
                                               <%-- <asp:ListItem >Yes</asp:ListItem>
                                                <asp:ListItem >Maybe</asp:ListItem>
                                                <asp:ListItem >No</asp:ListItem>--%>
                                            </asp:RadioButtonList>
                                            <%--<span class ="checkmark"></span>--%>
                                        <div class="stars">
                                            <asp:TextBox CssClass="star star-5" runat="server"></asp:TextBox>
                                            <input class="star star-5" id="star-5-2" type="radio" name="star" value="5" />

                                            <label class="star star-5" for="star-5-2"></label>
                                            <input class="star star-4" id="star-4-2" type="radio" name="star" value="4" />
                                            <label class="star star-4" for="star-4-2"></label>
                                            <input class="star star-3" id="star-3-2" type="radio" name="star" value="3" />
                                            <label class="star star-3" for="star-3-2"></label>
                                            <input class="star star-2" id="star-2-2" type="radio" name="star" value="2" />
                                            <label class="star star-2" for="star-2-2"></label>
                                            <input class="star star-1" id="star-1-2" type="radio" name="star" value="1" />
                                            <label class="star star-1" for="star-1-2"></label>
                                        </div>
                                        </div> 
								</div>
                              
                               
							</div>
                    <p>2. Goal Achievements</p>
                            <div class="form-row">
                                
								<div class="form-holder w-100">
                                    <div class="radiobtn">
                                            <asp:RadioButtonList ID="RadioButtonList1" CssClass="checkbox-circle" EnableTheming ="false"  runat ="server"  >
                                               <%-- <asp:ListItem >Yes</asp:ListItem>
                                                <asp:ListItem >Maybe</asp:ListItem>
                                                <asp:ListItem >No</asp:ListItem>--%>
                                            </asp:RadioButtonList>
                                            <%--<span class ="checkmark"></span>--%>
                                        <div class="stars">
                                            <asp:TextBox CssClass="star star-5" runat="server"></asp:TextBox>
                                            <input class="star star-5" id="star1-5-2" type="radio" name="star1" value="5" />

                                            <label class="star star-5" for="star1-5-2"></label>
                                            <input class="star star-4" id="star1-4-2" type="radio" name="star1" value="4" />
                                            <label class="star star-4" for="star1-4-2"></label>
                                            <input class="star star-3" id="star1-3-2" type="radio" name="star1" value="3" />
                                            <label class="star star-3" for="star1-3-2"></label>
                                            <input class="star star-2" id="star1-2-2" type="radio" name="star1" value="2" />
                                            <label class="star star-2" for="star1-2-2"></label>
                                            <input class="star star-1" id="star1-1-2" type="radio" name="star1" value="1" />
                                            <label class="star star-1" for="star1-1-2"></label>
                                        </div>
                                        </div> 
								</div>
                              
                                
							</div>
                    <p>2. Overal Performance</p>
                            <div class="form-row">
                                
								<div class="form-holder w-100">
                                    <div class="radiobtn">
                                            <asp:RadioButtonList ID="RadioButtonList2" CssClass="checkbox-circle" EnableTheming ="false"  runat ="server"  >
                                               <%-- <asp:ListItem >Yes</asp:ListItem>
                                                <asp:ListItem >Maybe</asp:ListItem>
                                                <asp:ListItem >No</asp:ListItem>--%>
                                            </asp:RadioButtonList>
                                            <%--<span class ="checkmark"></span>--%>
                                        <div class="stars">
                                            <asp:TextBox CssClass="star star-5" runat="server"></asp:TextBox>
                                            <input class="star star-5" id="star2-5-2" type="radio" name="star2" value="5" />

                                            <label class="star star-5" for="star2-5-2"></label>
                                            <input class="star star-4" id="star2-4-2" type="radio" name="star2" value="4" />
                                            <label class="star star-4" for="star2-4-2"></label>
                                            <input class="star star-3" id="star2-3-2" type="radio" name="star2" value="3" />
                                            <label class="star star-3" for="star2-3-2"></label>
                                            <input class="star star-2" id="star2-2-2" type="radio" name="star2" value="2" />
                                            <label class="star star-2" for="star2-2-2"></label>
                                            <input class="star star-1" id="star2-1-2" type="radio" name="star2" value="1" />
                                            <label class="star star-1" for="star2-1-2"></label>
                                        </div>
                                        </div> 
								</div>
                              
                                
							</div>
               <%-- </div>
             </div>--%>
             
                            <div id="dialog" class="dialog1" style="text-align: right">
                                <asp:Button ID="btnsubmit1" runat="server" Text="Submit" BackColor="SkyBlue" OnClick="btnsubmit1_Click"  />
                                <asp:Label ID="lblSurveyId" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>

                            </div>
                            </div>
						<%--</div>--%>
    </div>
    </form>
</body>
</html>
