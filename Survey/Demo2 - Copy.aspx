<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Demo2 - Copy.aspx.cs" Inherits="Demo2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
    <style>
@import url(http://fonts.googleapis.com/css?family=Roboto:500,100,300,700,400);

* {
  margin: 0;
  padding: 0;
  font-family: roboto;
}

body { background: #000; }

.cont {
  width: 93%;
  max-width: 350px;
  text-align: center;
  margin: 4% auto;
  padding: 30px 0;
  background: #111;
  color: #EEE;
  border-radius: 5px;
  border: thin solid #444;
  overflow: hidden;
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
  color: #FE7;
  text-shadow: 0 0 20px #952;
}

input.star-1:checked ~ label.star:before { color: #F62; }

label.star:hover { transform: rotate(-15deg) scale(1.3); }

label.star:before {
  content: '\f006';
  font-family: FontAwesome;
}
</style>
   <link href="http://www.cssscript.com/wp-includes/css/sticky.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="http://netdna.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" />

     <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
   
</head>
<body>
     
   
            <form runat="server"  >
                <script type="text/javascript"  >
                    function ShowHideDiv() {
                        var rdbNo = document.getElementById("<%= rdbNo.ClientID %>");
                        var rdbybph = document.getElementById("<%= rdbybph.ClientID %>");
                        var dvNo = document.getElementById("dvNo");
                        var dvybph = document.getElementById("dvybph");
                        dvNo.style.display = rdbNo.checked ? "block" : "none";
                        dvybph.style.display = rdbybph.checked ? "block" : "none";

         }
    </script>
                <script type="text/javascript">
                    function ShowHideDiv1() {
                        var rdbBehave = document.getElementById("<%= rdbBehave.ClientID %>")
                        var rdbBehaveBad = document.getElementById("<%= rdbBehaveBad.ClientID %>")
                        var dvBehave = document.getElementById("dvBehave");
                        dvBehave.style.display = rdbBehaveBad.checked ? "block" : "none";
                    }
                </script>

                         <div class="wrapper">
                   <div id="wizard">
 
        		<!-- SECTION 1 -->
                <h2></h2>
                <section>
                    <div class="inner">
						<div class="image-holder">
							<img src="images/1.png" alt=""/>
						</div>
						<div class="form-content" >
							<div class="form-header">
								<h3>Feedback</h3>
							</div>
							<p>1. Did you find what you were looking for?</p>
							<div class="form-row">
								<div class="form-holder w-100">
                                    
                                    <div >
                                        <asp:RadioButton ID="rdbLooking" onclick="ShowHideDiv()" runat ="server" Text ="Yes" EnableTheming ="false"  CssClass ="checkmark" GroupName ="LookinFor" /><br /><br />
                                        <asp:RadioButton ID="rdbNo" onclick="ShowHideDiv()" EnableTheming ="false"  CssClass ="checkmark" runat ="server" Text ="No" GroupName ="LookinFor" />

                                        <asp:RadioButton ID="rdbybph" onclick="ShowHideDiv()" CssClass ="chkbox" runat ="server" Visible ="false"  Text ="Yes, but price is high" GroupName ="LookinFor" />

                                           <%-- <asp:RadioButtonList ID="rdbLooking" AutoPostBack ="true"  CssClass="male" OnSelectedIndexChanged ="rdbLooking_SelectedIndexChanged" runat ="server"  >
                                                <asp:ListItem >Yes</asp:ListItem>
                                                <asp:ListItem >No</asp:ListItem>
                                                <asp:ListItem >Yes, but price is high</asp:ListItem>
                                            </asp:RadioButtonList>--%>
                                            <%--<span class ="checkmark"></span>--%>
                                        </div> 
								</div>
                                
                                <%--<div class="form-holder">
									<input type="text" placeholder="Last Name" class="form-control">
								</div>--%>
							</div>
                            <div class="form-holder" style="align-self: flex-end; transform: translateY(4px);">
									<div class="checkbox-tick">
										<label class="male">
											<input type="radio" name="gender" value="male" checked /> Yes<br />
											<span class="checkmark"></span>
										</label>
                                        </div> 
                                    </div> 
                            <div id="dvNo" style="display: none" >
                                 <div class ="form-row">
                                    <div class="form-holder w-100">
                                        <%--<asp:TextBox ID="txtNo"  runat ="server" CssClass ="form-control" Placeholder="Please let us know what you were looking for"  ></asp:TextBox>--%>
									<textarea name="" runat ="server"  id="txtNo" placeholder="Please let us know what you were looking for?" class="form-control" style="height: 89px;"></textarea>

                                    </div>
                            </div>

                                        </div>
                           <div id="dvybph" style ="display: none">
                               <div class ="form-row">
                                    <div class="form-holder w-100">
                                        <asp:TextBox ID="txtYbprh"  runat ="server" CssClass ="form-control" placeholder="Please mention the product name and your price range below" ></asp:TextBox>
                                    </div>
                            </div>
                           </div>
						    
							<%--<div class="form-row">
								<div class="form-holder">
									<input type="text" placeholder="Your Email" class="form-control">
								</div>
								<div class="form-holder">
									<input type="text" placeholder="Phone Number" class="form-control">
								</div>
							</div>
							<div class="form-row">
								<div class="form-holder">
									<input type="text" placeholder="Age" class="form-control">
								</div>
								<div class="form-holder" style="align-self: flex-end; transform: translateY(4px);">
									<div class="checkbox-tick">
										<label class="male">
											<input type="radio" name="gender" value="male" checked> Male<br>
											<span class="checkmark"></span>
										</label>
										<label class="female">
											<input type="radio" name="gender" value="female"> Female<br>
											<span class="checkmark"></span>
										</label>
                                       
									</div>
                                     
								</div>
							</div>
							<div class="checkbox-circle">
								<label>
									<input type="checkbox" checked> Nor again is there anyone who loves or pursues or desires to obtaini.
									<span class="checkmark"></span>
								</label>
							</div>--%>
						</div>
					</div>
                </section>

				<!-- SECTION 2 -->
                <h2></h2>
                <section>
                    <div class="inner">
						<div class="image-holder">
							<img src="images/2.png" alt="">
						</div>
						<div class="form-content">
							<div class="form-header">
								<h3>Feedback</h3>
							</div>
							<p>2. Please rate our sales representative on the following</p>
							<div class="form-row">
								
									<div class="form-holder">
                                        <div class="select">
										<div class="select-control">Behaviour</div>
										<i class="zmdi zmdi-caret-down"></i>
									</div>
                                        <asp:RadioButton ID="rdbBehave" runat ="server" Text ="Good" GroupName ="Yes" CssClass="male"  onclick="ShowHideDiv1()"  /><br />
                                        <asp:RadioButton ID="rdbBehaveBad" runat ="server" Text ="Bad" GroupName ="Yes" CssClass="male" onclick="ShowHideDiv1()" />

                                   <%-- <asp:RadioButtonList ID="rdbBehave" OnSelectedIndexChanged ="rdbBehave_SelectedIndexChanged" AutoPostBack ="true" runat ="server" >
                                        <asp:ListItem >Good</asp:ListItem>
                                        <asp:ListItem >Bad</asp:ListItem>
                                    </asp:RadioButtonList>--%>
                                </div> 
								    <div class="form-holder">
                                        <div class="select">
										<div class="select-control">Knowledge</div>
										<i class="zmdi zmdi-caret-down"></i>
									</div>
									<asp:RadioButtonList ID="rdbKnowledge" runat ="server" >
                                        <asp:ListItem >Good</asp:ListItem>
                                        <asp:ListItem >Average</asp:ListItem>
                                        <asp:ListItem >Bad</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div> 
							</div>
                            <div id="dvBehave" style ="display: none" >
                            <div class ="form-row">
                                    <div class="form-holder w-100">
                                        <%--<asp:TextBox ID="txtBad" runat ="server" CssClass ="form-control" Placeholder="Please let us know what went wrong"  ></asp:TextBox>--%>
									<textarea name="" runat ="server"  id="txtBad" placeholder="Please let us know what went wrong?" class="form-control" style="height: 69px;"></textarea>

                                    </div>
                            </div>
                                </div>
                            <div class="form-row">
								
									
								
								<%--<div class="form-holder"></div>--%>
							</div>
						</div>
					</div>
                </section>
                <!-- SECTION 3 -->
                <h2></h2>
                <section>
                    <div class="inner">
						<div class="image-holder">
							<img src="images/3.png" alt=""/>
						</div>
						<div class="form-content" >
							<div class="form-header">
								<h3>Feedback</h3>
							</div>
							<p>3. What do you feel about our showroom ambience? </p>
							<div class="form-row">
								<div class="form-holder w-100" style="align-self: flex-end; transform: translateY(4px);">
									<div class="checkbox-tick">
										<label class="male">
											<input type="radio" name="rdbShowroom" value="Yes" /> Excellent<br />
											<span class="checkmark"></span>
										</label>
										<label class="male">
											<input type="radio" name="rdbShowroom" value="Maybe" />Good<br />
											<span class="checkmark"></span>
										</label>
                                       <label class="male">
											<input type="radio" name="rdbShowroom" value="No" /> Average<br />
											<span class="checkmark"></span>
										</label>
                                        <label class="female">
											<input type="radio" name="rdbShowroom" value="No"> Bad<br />
											<span class="checkmark"></span>
										</label>
									</div>
                                     
								</div>
							</div>
						</div>
					</div>
                </section>
                <!-- SECTION 4 -->
                <h2></h2>
                <section>
                    <div class="inner">
						<div class="image-holder">
							<img src="images/4.png" alt=""/>
						</div>
						<div class="form-content" >
							<div class="form-header">
								<h3>Feedback</h3>
							</div>
							<p>4. Would you like to visit us again for future requirement?</p>
							<div class="form-row">
								<div class="form-holder w-100" style="align-self: flex-end; transform: translateY(4px);">
									<div class="checkbox-tick">
										<label class="male">
											<input type="radio" name="rdbvisit" value="Yes" /> Yes<br />
											<span class="checkmark"></span>
										</label>
										<label class="male">
											<input type="radio" name="rdbvisit" value="Maybe" />Maybe<br />
											<span class="checkmark"></span>
										</label>
                                       <label class="female">
											<input type="radio" name="rdbvisit" value="No" /> No<br />
											<span class="checkmark"></span>
										</label>
									</div>
                                     
								</div>
							</div>
						</div>
					</div>
                </section>
                <!-- SECTION 5 -->
                <h2></h2>
                <section>
                    <div class="inner">
						<div class="image-holder">
							<img src="images/5.png" alt=""/>
						</div>
						<div class="form-content" >
							<div class="form-header">
								<h3>Feedback</h3>
							</div>
							<p>5. How Satisfied are you with our service</p>
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
       <asp:TextBox CssClass ="star star-5"  runat ="server" ></asp:TextBox>
      <input class="star star-5" id="star-5-2" type="radio" name="star" value ="5"/>

      <label class="star star-5" for="star-5-2"></label>
      <input class="star star-4" id="star-4-2" type="radio" name="star" value ="4"/>
      <label class="star star-4" for="star-4-2"></label>
      <input class="star star-3" id="star-3-2" type="radio" name="star" value ="3"/>
      <label class="star star-3" for="star-3-2"></label>
      <input class="star star-2" id="star-2-2" type="radio" name="star" value ="2"/>
      <label class="star star-2" for="star-2-2"></label>
      <input class="star star-1" id="star-1-2" type="radio" name="star" value ="1"/>
      <label class="star star-1" for="star-1-2"></label>
  </div>
                                        </div> 
								</div>
                              
<script>
    (function (i, s, o, g, r, a, m) {
        i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
            (i[r].q = i[r].q || []).push(arguments)
        }, i[r].l = 1 * new Date(); a = s.createElement(o),
        m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
    })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

    ga('create', 'UA-46156385-1', 'cssscript.com');
    ga('send', 'pageview');

</script>
							</div>
                            <div class="checkbox-circle">
								<label>
									<input type="checkbox" name="chktest" value ="Yes" checked> can we use your review as testimonial.
									<span class="checkmark"></span>
								</label>
							</div>
						</div>
                        
					</div>
                </section>
                
                <!-- SECTION 6 -->
                <h2></h2>
                <section>
                    <div class="inner">
						<div class="image-holder">
							<img src="images/6.png" alt="">
						</div>
						<div class="form-content">
							<div class="form-header">
								<h3>Feedback</h3>
							</div>
							<p>6. If you have any other comments please mention below</p>
							<div class="form-row">
								<div class="form-holder w-100">
                                    <%--<asp:TextBox ID="txtComments" runat ="server" placeholder="Your message here!" CssClass ="form-control" Height ="55px" ></asp:TextBox>--%>
									<textarea name="" runat ="server"  id="txtComments" placeholder="Your message here!" class="form-control" style="height: 89px;"></textarea>
								</div>
							</div>
                             <div class="form-row">
								<div class="form-holder">
                                   <asp:TextBox ID="txtCustName" runat ="server" Placeholder="Full Name" CssClass ="form-control" ></asp:TextBox>
								</div>
                                <div class="form-holder">
                                   <asp:TextBox ID="txtCustMobile" runat ="server" Placeholder="Mobile Number" CssClass ="form-control" ></asp:TextBox>

                                </div>
							</div>
                            <div class ="form-row">
                                
                                    <div class="form-holder w-100">
                                        <asp:TextBox ID="txtCustEmail"  runat ="server" CssClass ="form-control" Placeholder="Email"  ></asp:TextBox>
                                     <div id="dialog" class="dialog1">
                        <asp:Button ID="btnsubmit1" runat ="server" Width ="1px" Text =""  UseSubmitBehavior ="false" OnClick ="btnsubmit1_Click" />
                    </div>
                                    </div>
                   

                            </div>

							</div>
                       					</div>
                   <%-- <script type="text/javascript">
                        $(document).ready(function () {
                            $('#finish').click(function () {
                                //do something
                                alert("The paragraph was clicked.");
                            });
                        });
</script>--%>
                </section>



                        </div>
                             </div>
               
              
            </form>
  <%-- </div>--%>



    <!-- JQUERY -->
		<script src="js/jquery-3.3.1.min.js"></script>

		<!-- JQUERY STEP -->
		<script src="js/jquery.steps.js"></script>
		<script src="js/main.js"></script>
		<!-- Template created and distributed by Colorlib -->



</body>
</html>