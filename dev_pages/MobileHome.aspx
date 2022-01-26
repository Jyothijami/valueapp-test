<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobileHome.aspx.cs" Inherits="dev_pages_MobileHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
       <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1, maximum-scale=1" />


   <style type="text/css">
       /*-------------------------
Please follow me @maridlcrmn
/*-------------------------*/

body {
    padding-top: 50px;
}

.material-button-anim {
  position: relative;
  padding: 127px 15px 27px;
  text-align: center;
  max-width: 550px;
  margin: 0 auto 20px;
}

.material-button {
    position: relative;
    top: 0;
    z-index: 1;
    width: 60px;
    height: 70px;
    font-size: 1.5em;
    color: #fff;
    background: #2C98DE;
    border: none;
    border-radius: 50%;
    box-shadow: 0 3px 6px rgba(0,0,0,.275);
    outline: none;
}
.material-button-toggle {
    z-index: 4;
    width: 100px;
    height: 100px;
    margin: 0 auto;
}
.material-button-toggle span {
    -webkit-transform: none;
    transform:         none;
    -webkit-transition: -webkit-transform .175s cubic-bazier(.175,.67,.83,.67);
    transition:         transform .175s cubic-bazier(.175,.67,.83,.67);
}
.material-button-toggle.open {
    -webkit-transform: scale(1.3,1.3);
    transform:         scale(1.3,1.3);
    -webkit-animation: toggleBtnAnim .175s;
    animation:         toggleBtnAnim .175s;
}
.material-button-toggle.open span {
    -webkit-transform: rotate(45deg);
    transform:         rotate(45deg);
    -webkit-transition: -webkit-transform .175s cubic-bazier(.175,.67,.83,.67);
    transition:         transform .175s cubic-bazier(.175,.67,.83,.67);
}

#options {
    /*width: 120px;*/

  height: 100px;
 
}
.option {
    position: relative;
}
.option .option1,
.option .option2,
.option .option3,
.option .option4,
.option .option5,
.option .option6
 {
    filter: blur(5px);
    -webkit-filter: blur(5px);
    -webkit-transition: all .175s;
    transition:         all .175s;
}
.option .option1 {
    -webkit-transform: translate3d(90px,90px,0) scale(.8,.8);
    transform:         translate3d(90px,90px,0) scale(.8,.8);
}
.option .option2 {
    -webkit-transform: translate3d(0,90px,0) scale(.8,.8);
    transform:         translate3d(0,90px,0) scale(.8,.8);
}
.option .option3 {
    -webkit-transform: translate3d(-90px,90px,0) scale(.8,.8);
    transform:         translate3d(-90px,90px,0) scale(.8,.8);
}
.option .option4 {
    -webkit-transform: translate3d(-90px,90px,0) scale(.8,.8);
    transform:         translate3d(-90px,90px,0) scale(.8,.8);
}
.option .option5 {
    -webkit-transform: translate3d(-90px,90px,0) scale(.8,.8);
    transform:         translate3d(-90px,90px,0) scale(.8,.8);
}
.option .option6 {
    -webkit-transform: translate3d(-90px,90px,0) scale(.8,.8);
    transform:         translate3d(-90px,90px,0) scale(.8,.8);
}
.option.scale-on .option1, 
.option.scale-on .option2,
.option.scale-on .option3,
.option.scale-on .option4,
.option.scale-on .option5,
.option.scale-on .option6 {
    filter: blur(0);
    -webkit-filter: blur(0);
    -webkit-transform: none;
    transform:         none;
    -webkit-transition: all .175s;
    transition:         all .175s;
}
.option.scale-on .option2 {
    -webkit-transform: translateY(-28px) translateZ(0);
    transform:         translateY(-28px) translateZ(0);
    -webkit-transition: all .175s;
    transition:         all .175s;
}

@keyframes toggleBtnAnim {
    0% {
        -webkit-transform: scale(1,1);
        transform:         scale(1,1);
    }
    25% {
        -webkit-transform: scale(1.4,1.4);
        transform:         scale(1.4,1.4); 
    }
    75% {
        -webkit-transform: scale(1.2,1.2);
        transform:         scale(1.2,1.2);
    }
    100% {
        -webkit-transform: scale(1.3,1.3);
        transform:         scale(1.3,1.3);
    }
}
@-webkit-keyframes toggleBtnAnim {
    0% {
        -webkit-transform: scale(1,1);
        transform:         scale(1,1);
    }
    25% {
        -webkit-transform: scale(1.4,1.4);
        transform:         scale(1.4,1.4); 
    }
    75% {
        -webkit-transform: scale(1.2,1.2);
        transform:         scale(1.2,1.2);
    }
    100% {
        -webkit-transform: scale(1.3,1.3);
        transform:         scale(1.3,1.3);
    }
}
   </style>













  <link href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.0/css/bootstrap.min.css" rel="stylesheet"/>
<script src="//maxcdn.bootstrapcdn.com/bootstrap/3.3.0/js/bootstrap.min.js"></script>
<script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
<!------ Include the above in your HEAD tag ---------->

<link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/4.1.0/css/font-awesome.min.css" />
</head>
<body>
    <form id="form1" runat="server">
  <div>
        <script type="text/javascript">
            $(document).ready(function () {
                $('.material-button-toggle').on("click", function () {
                    $(this).toggleClass('open');
                    $('.option').toggleClass('scale-on');
                });
            });</script>

<div class="container">
	<div class="row">
    
        <div class="material-button-anim">
          <ul class="list-inline" id="options">
            <li class="option">

                <a href = "DailyReport.aspx">
                <button class="material-button option1" type="button" >
                <span class="fa" aria-hidden="true" style="font-size:smaller"> Daily <br />Report  </span> 
                     </button>
                <%--  <a href = "wasteel1.aspx" </a>--%>
            
              <%--  <a href = "wasteel1.aspx" class="material-button option1" type="button"><span class="fa fa-phone" aria-hidden="true"></span></a>--%>
                </a>
            </li>
              
            <li class="option">
                <a href = "EMP_CR.aspx">

              <button class="material-button option2" type="button">
                <span class="fa" aria-hidden="true" style="font-size:smaller">Comp <br />Register</span>
              </button>
                    </a> 
            </li>
              <li class="option">

                <a href = "ToDoList1.aspx">
                <button class="material-button option4" type="button" >
                <span class="fa" aria-hidden="true" style="font-size:smaller"> <br />ToDo <br />List  </span> 
                     </button>
                <%--  <a href = "wasteel1.aspx" </a>--%>
            
              <%--  <a href = "wasteel1.aspx" class="material-button option1" type="button"><span class="fa fa-phone" aria-hidden="true"></span></a>--%>
                </a>
            </li>
              
            <li class="option">
                <a href = "LeaveApplication.aspx">

              <button class="material-button option3" type="button">
                <span class="fa " aria-hidden="true" style="font-size:smaller">Leave <br />Application</span>
              </button>
                    </a> 
            </li>
              <li class="option">

                <a href = "MobileApp_StockReport.aspx">
                <button class="material-button option5" type="button" >
                <span class="fa" aria-hidden="true" style="font-size:smaller"> <br />Warehouse <br />Stock  </span> 
                     </button>
                <%--  <a href = "wasteel1.aspx" </a>--%>
            
              <%--  <a href = "wasteel1.aspx" class="material-button option1" type="button"><span class="fa fa-phone" aria-hidden="true"></span></a>--%>
                </a>
            </li>
              <li class="option">

                <a href = "DailyReportM.aspx">
                <button class="material-button option5" type="button" >
                <span class="fa" aria-hidden="true" style="font-size:smaller"> <br />Daily Report <br />View  </span> 
                     </button>
                <%--  <a href = "wasteel1.aspx" </a>--%>
            
              <%--  <a href = "wasteel1.aspx" class="material-button option1" type="button"><span class="fa fa-phone" aria-hidden="true"></span></a>--%>
                </a>
            </li>
          </ul>
          <button class="material-button material-button-toggle" type="button">
            <span class="fa fa-plus" aria-hidden="true"><br />Home</span>
          </button>
        </div>
     
	</div>
</div>



        
    </div>
    </form>
</body>
</html>
