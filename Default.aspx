<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>Valueline</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <meta name="author" content="" />
    <link href="https://fonts.googleapis.com/css?family=Quicksand:300,400,500,700" rel="stylesheet" />
    <!-- Place favicon.ico and apple-touch-icon.png in the root directory -->
    <link rel="shortcut icon" href="favicon.ico" />

    <!-- Animate.css -->
    <link rel="stylesheet" href="ocss/animate.css" />
    <!-- Icomoon Icon Fonts-->
    <link rel="stylesheet" href="ocss/icomoon.css" />
    <!-- Bootstrap  -->
    <link rel="stylesheet" href="ocss/bootstrap.css" />
    <!-- Flexslider  -->
    <link rel="stylesheet" href="ocss/flexslider.css" />
    <!-- Flaticons  -->
    <link rel="stylesheet" href="ofonts/flaticon/font/flaticon.css" />
    <!-- Owl Carousel -->
    <link rel="stylesheet" href="ocss/owl.carousel.min.css" />
    <link rel="stylesheet" href="ocss/owl.theme.default.min.css" />
    <!-- Theme style  -->
    <link rel="stylesheet" href="ocss/style.css" />
    <script src="ojs/modernizr-2.6.2.min.js"></script>
    <script type="text/javascript">
        var totalCount = 5;
        function ChangeIt() {
            var num = Math.ceil(Math.random() * totalCount);
            document.body.background = 'images2/' + num + '.jpg';
            document.body.style.backgroundRepeat = "repeat";// Background repeat
        }
</script>
    
    
</head>
<body>
    <form id="form1" runat="server">
        
    <div id="colorlib-page">
    <div class="row navbar-fixed-top" style="background-color:#578bb9;height:40px">
       
        <a href="Default1.aspx"  style="margin-left: 90%;color:#fff; font-size:x-large; font-family :Roboto Condensed,Arial,sans-serif"  >Login</a>
         </div>
        <aside id="colorlib-aside" style ="background-color :#578bb9; color:#fff; font-size:large; font-family :Roboto Condensed,Arial,sans-serif" role ="complementary" class="border js-fullheight" >
        <a href="#" class="js-colorlib-nav-toggle colorlib-nav-toggle"><i></i></a>
        <%--<h1 id="colorlib-logo" style ="color:#fff; font-size:x-large ;  font-family :Roboto Condensed,Arial,sans-serif" "><a href="http://valueline.in/" style ="background-image: url(images2/valuelinelogo.png);" ></a></h1>--%>
            <div class="colorlib-logo"><asp:Image Width ="150px" Height ="50" ID="img" runat ="server" ImageUrl ="~/images2/valuelinelogo.png" /></div>
            <br />
        <nav id="colorlib-main-menu" style ="color:#fff; font-size:large; font-family :Roboto Condensed,Arial,sans-serif" " role="navigation">
                      <ul>
                    <li class="colorlib-active;" ><a href="Default.aspx">Home</a></li>
                    <li><a href="http://valueline.in/sanitary-ware-bath-fittings-tiles-aluminium-products/">Our Products</a></li>
                    <li><a href="#">Download Catalog</a></li>
                    <li><a href="http://valueline.in/valueline-projects/">Projects</a></li>
                    <li><a href="http://valueline.in/valueline-clients/">Clients</a></li>
                    <li><a href="#">Contact Us</a></li>
            </ul>
            </nav>
            <div class="colorlib-footer" >
                <ul>
                    <li><a href="https://www.facebook.com/ValuelineIndia/"><i class="icon-facebook2" style ="color:#fff"></i></a></li>
                    <li><a href="https://twitter.com/ValuelineIndia"><i class="icon-twitter2" style ="color :#fff "></i></a></li>
                    <li><a href="https://www.instagram.com/valuelineIndia/"><i class="icon-instagram" style ="color :#fff "></i></a></li>
                    <li><a href="https://www.linkedin.com/company/valuelineindia/?viewAsMember=true" style ="color :#fff "><i class="icon-linkedin2"></i></a></li>
                </ul>
            </div>
            </aside>
        <div id="colorlib-main">
            <aside id="colorlib-hero"  class="js-fullheight" >
                <div class="flexslider js-fullheight">
                    <ul class="slides">
                        <li style="background-image: url(images2/DSC04641.JPG)">
                            <div class="overlay" ></div>
                            <div class="container-fluid" >
                                <div class="row" >
                                    <div class="col-md-6 col-md-offset-3 col-md-push-3 col-sm-12 col-xs-12 js-fullheight slider-text" >
                                        <div class="slider-text-inner">
                                            <div class="desc" style ="background-color :#578bb9">
                                                <h1 style ="color :#fff">The Company</h1>
                                                <h2  style ="color :#fff">We are thinkers, innovators, creators, designers. We are people who nurture a deep appreciation for art, nature, and aesthetic. We are people with a history of talent and intuition.</h2>
                                                <!--<p><a class="btn btn-primary btn-learn">View Project <i class="icon-arrow-right3"></i></a></p>-->
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </li>
                        <li style="background-image: url(images2/DSC04642.JPG);">
                            <div class="overlay"></div>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-md-6 col-md-offset-3 col-md-push-3 col-sm-12 col-xs-12 js-fullheight slider-text" >
                                        <div class="slider-text-inner" >
                                            <div class="desc" style ="background-color :#578bb9">
                                                <h1  style ="color :#fff">Our Mission</h1>
                                                <h2  style ="color :#fff">Our mission is to improve the quality of people’s lives by enhancing the performance of their buildings, with products of the highest quality, technology and aesthetics.</h2>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </li>

                        <li style="background-image: url(images2/DSC04425.JPG);">
                            <div class="overlay"></div>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-md-6 col-md-offset-3 col-md-push-3 col-sm-12 col-xs-12 js-fullheight slider-text">
                                        <div class="slider-text-inner">
                                            <div class="desc" style ="background-color :#578bb9">
                                                <h1  style ="color :#fff">Our Vision</h1>
                                                <h2  style ="color :#fff">Our vision is to be a leading company in developing and producing Sanitary systems for architectural applications.</h2>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </li>
                        <li style="background-image: url(images2/DSC04424.JPG);">

                            <div class="overlay"></div>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-md-6 col-md-offset-3 col-md-push-3 col-sm-12 col-xs-12 js-fullheight slider-text">
                                        <div class="slider-text-inner">
                                            <div class="desc" style ="background-color :#578bb9">
                                                <h1  style ="color :#fff">Our Values</h1>
                                                <h2  style ="color :#fff">Our values are the essence of our overall business philosophy and reflect the way we approach our customers and stakeholders.</h2>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </li>
                        <li style="background-image: url(images2/DSC04261.JPG);">

                            <div class="overlay"></div>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-md-6 col-md-offset-3 col-md-push-3 col-sm-12 col-xs-12 js-fullheight slider-text">
                                        <div class="slider-text-inner">
                                            
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </li>
                        <li style="background-image: url(images2/DSC04270.JPG);">

                            <div class="overlay"></div>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-md-6 col-md-offset-3 col-md-push-3 col-sm-12 col-xs-12 js-fullheight slider-text">
                                        <div class="slider-text-inner">
                                            
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </li>
                    </ul>
                </div>
            </aside>
            <div class="colorlib-work">
                <div class="colorlib-narrow-content">
                    <div class="row">
                        <div class="col-md-6 col-md-offset-3 col-md-pull-3 animate-box" data-animate-effect="fadeInLeft">
                            <span class="heading-meta">Updates</span>
                            <h2 class="colorlib-heading animate-box">Recent Updates</h2>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 animate-box" data-animate-effect="fadeInLeft">
                            <div class="project" style="background-image: url(images2/Toss.jpeg);">
                                <div class="desc">
                                    <div class="con">
                                        <h3><a href="work.html">Cricket Tournment Toss Time 2021-2022</a></h3>
                                        <span>Valueline Vs Alumil</span>
                                        <p class="icon">
                                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                            <script type="text/javascript">ChangeIt();</script>
                                            <span><a href="#"><i class="icon-share3"></i></a></span>
                                            <span><a href="#"><i class="icon-eye"></i>100</a></span>
                                            <span><a href="#"><i class="icon-heart"></i>49</a></span>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 animate-box" data-animate-effect="fadeInLeft">
                            <div class="project" style="background-image: url(images2/1.jpeg);">
                                <div class="desc">
                                    <div class="con">
                                        <h3><a href="work.html">Runners</a></h3>
                                        <span>Valueline</span>
                                        <p class="icon">
                                            <span><a href="#"><i class="icon-share3"></i></a></span>
                                            <span><a href="#"><i class="icon-eye"></i>100</a></span>
                                            <span><a href="#"><i class="icon-heart"></i>49</a></span>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 animate-box" data-animate-effect="fadeInLeft">
                            <div class="project" style="background-image: url(images2/man.jpeg);">
                                <div class="desc">
                                    <div class="con">
                                        <h3><a href="work.html">Man of the Tournment</a></h3>
                                        <span>Mr. Suresh</span>
                                        <p class="icon">
                                            <span><a href="#"><i class="icon-share3"></i></a></span>
                                            <span><a href="#"><i class="icon-eye"></i>100</a></span>
                                            <span><a href="#"><i class="icon-heart"></i>49</a></span>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 animate-box" data-animate-effect="fadeInLeft">
                            <div class="project" style="background-image: url(images2/3.jpeg);">
                                <div class="desc">
                                    <div class="con">
                                        <h3><a href="work.html">Best Bowler</a></h3>
                                        <span>Mr. Palash Ghughu </span>
                                        <p class="icon">
                                            <span><a href="#"><i class="icon-share3"></i></a></span>
                                            <span><a href="#"><i class="icon-eye"></i>100</a></span>
                                            <span><a href="#"><i class="icon-heart"></i>49</a></span>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 animate-box" data-animate-effect="fadeInLeft">
                            <div class="project" style="background-image: url(images2/4.jpeg);">
                                <div class="desc">
                                    <div class="con">
                                        <h3><a href="work.html">Bats Man</a></h3>
                                        <span>Mr. Bhanu Prasad </span>
                                        <p class="icon">
                                            <span><a href="#"><i class="icon-share3"></i></a></span>
                                            <span><a href="#"><i class="icon-eye"></i>100</a></span>
                                            <span><a href="#"><i class="icon-heart"></i>49</a></span>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6 animate-box" data-animate-effect="fadeInLeft">
                            <div class="project" style="background-image: url(images2/DSC04641.jpeg);">
                                <div class="desc">
                                    <div class="con">
                                        <h3><a href="work.html">Winners</a></h3>
                                        <span>ALUMIL</span>
                                        <p class="icon">
                                            <span><a href="#"><i class="icon-share3"></i></a></span>
                                            <span><a href="#"><i class="icon-eye"></i>100</a></span>
                                            <span><a href="#"><i class="icon-heart"></i>49</a></span>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                </div>
            </div>
        </div>
    </div>
        </div> 
        <!-- jQuery -->
    <script src="ojs/jquery.min.js"></script>
    <!-- jQuery Easing -->
    <script src="ojs/jquery.easing.1.3.js"></script>
    <!-- Bootstrap -->
    <script src="ojs/bootstrap.min.js"></script>
    <!-- Waypoints -->
    <script src="ojs/jquery.waypoints.min.js"></script>
    <!-- Flexslider -->
    <script src="ojs/jquery.flexslider-min.js"></script>
    <!-- Sticky Kit -->
    <script src="ojs/sticky-kit.min.js"></script>
    <!-- Owl carousel -->
    <script src="ojs/owl.carousel.min.js"></script>
    <!-- Counters -->
    <script src="ojs/jquery.countTo.js"></script>

    <!-- MAIN JS -->
    <script src="ojs/main.js"></script>
       

    </form>
</body>
</html>
