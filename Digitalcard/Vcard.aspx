<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vcard.aspx.cs" Inherits="Vcard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

     <style>
/*body {
  background-image: url('19.png');
  background-repeat: no-repeat;
  background-size: cover;
}*/


@font-face {
    font-family: 'adam.cg_proregular';
    src: url('adam.cg_pro.woff2') format('woff2'),
         url('adam.cg_pro.woff') format('woff');
    font-weight: normal;
    font-style: normal;

}

@font-face {
    font-family: 'avantgargotitcteeregular';
    src: url('ae10013t.woff2') format('woff2'),
         url('ae10013t.woff') format('woff');
    font-weight: normal;
    font-style: normal;

}


</style> 

    <link href="css/bootstrap.css" rel="stylesheet" />
    <script src="js/bootstrap.bundle.js"></script>


</head>
<body >
    <form id="form1" runat="server">
        <div id="Grid">
        <asp:Panel ID="Panel1" runat="server" Width="160px">
            <div style="width: 793.7007874px; height: 1122.519685px; background-image: url('19.png'); background-repeat: no-repeat; background-size: cover;">


                <div class="row ">


                    <div class="col-sm-6" style="text-align: left; margin-top: 25px; padding-left: 35px">

                        <img src="1.png" style="height: 60%" class="img-fluid" />
                        <%-- <h1>   Digital V-Card</h1>--%>
                    </div>


                    <div class="col-sm-6" style="text-align: right; margin-top: 45px; padding-right: 35px">

                        <img src="2.png" style="height: 50%" />
                        <%-- <h1>   Digital V-Card</h1>--%>
                    </div>

                </div>


                <div class="row">

                    <div class="col-sm-12" style="text-align: center">

                        <h1 class="display-5" style="font-weight: bold; font-family: 'ADAM.CG PRO'">
                            <asp:Label ID="lblName" runat="server"></asp:Label></h1>
                        <h3 style="font-family: AvantGarGotItcTEE; text-transform: initial; font-size: 1.75rem">
                            <asp:Label ID="lblDept" runat="server"></asp:Label></h3>
                        <br />

                    </div>

                </div>



                <div class="container-fluid">
                    <div class="row" style="margin-top: 50px">


                        <div class="col-sm-12" style="margin-top: 10px; margin-bottom: 20px; margin-left: 30px;">

                            <h4 style="font-weight: 100; font-family: AvantGarGotItcTEE">| Touch the icons to get more information</h4>

                        </div>


                        <div class="row">

                            <div class="col-sm-3">
                                <a href="https://valueline.in/about-valueline/">
                                    <img src="4.png" class="img-fluid" /></a>
                            </div>

                            <div class="col-sm-3">
                                <a href="https://valueline.in/">
                                    <img src="5.png" class="img-fluid" />
                                </a>
                            </div>

                            <div class="col-sm-3">
                                <a href="mailto:anamol@alumil.in" id="mail" runat="server">
                                    <img src="6.png" class="img-fluid" />
                                </a>
                            </div>
                            <div class="col-sm-3">
                                <a href="tel:7702300826" id="Phn" runat="server">
                                    <img src="7.png" class="img-fluid" /></a>
                            </div>
                        </div>


                        <div class="col-sm-12" style="margin-top: 30px; margin-bottom: 20px; margin-left: 30px;">

                            <h4 style="font-weight: 100; font-family: AvantGarGotItcTEE">| Tap to follow us on our social networks</h4>

                        </div>


                        <div class="row">



                            <div class="col-sm-3">
                                <a href="https://www.facebook.com/ValuelineIndia/">
                                    <img src="9.png" class="img-fluid" /></a>
                            </div>

                            <div class="col-sm-3">
                                <a href="https://youtu.be/JXKPAaF-0hk">
                                    <img src="10.png" class="img-fluid" /></a>
                            </div>
                            <div class="col-sm-3">
                                <a href="https://www.instagram.com/valuelineIndia/">
                                    <img src="11.png" class="img-fluid" /></a>
                            </div>
                            <div class="col-sm-3">
                                <a href="https://wa.me/917702300826" id="wa" runat="server">
                                    <img src="12.png" class="img-fluid" />
                                </a>
                            </div>
                        </div>


                        <div class="col-sm-12" style="margin-top: 30px; margin-bottom: 20px; margin-left: 30px;">

                            <h4 style="font-weight: 100; font-family: AvantGarGotItcTEE">| Click to find your way to us</h4>

                        </div>


                        <div class="row">




                            <div class="col-sm-2" style="width: 145px; height: 130px">
                                <a href="https://g.page/Valueline-Hyderabad">
                                    <img src="14.png" class="img-fluid" /></a>
                            </div>
                            <div class="col-sm-2" style="width: 145px; height: 130px">
                                <a href="https://www.google.com/maps/place/Valueline+-+Bangalore+Sanitary+Ware,+Bathroom+fittings+%26+Tiles,+Jacuzzi+Bathtub,+Showers,+Home+Automation/@12.9685073,77.6132963,13z/data=!4m5!3m4!1s0x3bae178b3951c0c9:0x947175a05980a0ab!8m2!3d12.9702673!4d77.6396176">
                                    <img src="15.png" class="img-fluid" /></a>
                            </div>
                            <div class="col-sm-2" style="width: 145px; height: 130px">
                                <a href="https://www.google.com/maps/place/Valueline/@13.0641395,80.2493195,17z/data=!4m5!3m4!1s0x3a526707a7cca831:0x15b9eecdd2312b98!8m2!3d13.0642735!4d80.2493605?shorturl=1">
                                    <img src="16.png" class="img-fluid" /></a>
                            </div>

                            <div class="col-sm-2" style="width: 145px; height: 130px">
                                <a href="https://www.google.com/maps/place/Valueline/@17.7377004,83.2935601,17z/data=!3m1!4b1!4m5!3m4!1s0x3a3943c6a0af3a4b:0x78229e7b9bb3d922!8m2!3d17.7376953!4d83.2957488?shorturl=1">
                                    <img src="17.png" class="img-fluid" /></a>
                            </div>
                            <div class="col-sm-2" style="width: 145px; height: 130px">
                                <a href="https://www.google.com/maps/place//data=!4m2!3m1!1s0x3a35e5beb042c823:0x4102b1aef751ac45?source=g.page.share">
                                    <img src="18.png" class="img-fluid" /></a>
                            </div>




                        </div>

                    </div>
                </div>




            </div>
            <asp:HiddenField ID="hfGridHtml" runat="server" />
        </asp:Panel>
                <asp:Button ID="btnExport" runat="server" Visible ="false"  OnClick="btnExport_Click" Text="Export" />


        </div>


        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    $(function () {
        $("[id*=btnExport]").click(function () {
            $("[id*=hfGridHtml]").val($("#Grid").html());
        });
    });
</script>




    </form>
</body>
</html>
