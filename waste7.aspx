<%@ Page Language="C#" AutoEventWireup="true" CodeFile="waste7.aspx.cs" Inherits="waste7" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

        <link href="/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link rel="stylesheet" href="/BAT_files/bootstrap-responsive.css" />
    <!-- iconSweet2 icon pack (16x16) -->
    <link rel="stylesheet" href="/BAT_files/icsw2_16.css" />
    <!-- splashy icon pack -->
    <link rel="stylesheet" href="/BAT_files/splashy.css" />
    <!-- flag icons -->
    <link rel="stylesheet" href="/BAT_files/flags.css" />
    <!-- power tooltips -->
    <link rel="stylesheet" href="/BAT_files/jquery.css" />
    <!-- google web fonts -->
    <link rel="stylesheet" href="/BAT_files/css_002.css" />
    <link rel="stylesheet" href="/BAT_files/css.css" />

    <!-- aditional stylesheets -->
    <!-- colorbox -->
    <link rel="stylesheet" href="/BAT_files/colorbox.css" />
    <!--fullcalendar -->
    <link rel="stylesheet" href="/BAT_files/fullcalendar_beoro.css" />


    <!-- main stylesheet -->
    <link rel="stylesheet" href="/BAT_files/beoro.css" />
    <link href="/site_resources/css/sstyles.css" rel="stylesheet" />

<%--    <script src="js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.8.custom.min.js" type="text/javascript"></script>--%>
    <script src="js/jquery-1.9.1.js"></script>
    <script src="js/jquery-ui-1.10.0.custom.js"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="stylesheet" type="text/css"/>

    <script>
        $(document).ready(function () {
            //$.ajax({
            //    type: "POST",
            //    url: "/acomp.asmx/GetItemsWithAbbr",
            //    dataType: "json",
            //    data: "{ itemstr: '" + $("[name$='txtSearchText']").val() + "', itemtype: 'ITEM_NAME' }",
            //    contentType: "application/json; charset=utf-8",
            //    success: function (data) {
            //        alert(data.d);
            //        $("[name$='txtSearchText']").autocomplete({
            //            minLength: 0,
            //            source: data.d,
            //            focus: function (event, ui) {
            //                alert("asd");
            //                $("[id$='txtSearchText']").val(ui.item.Name);
            //                return false;
            //            },
            //            select: function (event, ui) {
            //                alert("asd");
            //                $("[id$='txtSearchText']").val(ui.item.Name);
            //                //$('#selectedValue').text("Selected value:" + ui.item.Abbreviation);
            //                return false;
            //            }
            //        });
            //    },
            //    error: function (XMLHttpRequest, textStatus, errorThrown) {
            //        alert(textStatus + XMLHttpRequest.responseText + errorThrown);
            //    }
            //});

            $("[name$='txtSearchText']").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        url: "/acomp.asmx/GetItemsWithAbbr",
                        dataType: "json",
                        data: "{ itemstr: 'P', itemtype: 'full' }",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response(data.d);
                        },
                        failure: function (data) {
                            alert("failed");
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus + XMLHttpRequest.responseText + errorThrown);
                        }
                    });
                },
                minLength: 2,
                select: function (event, ui) {
                    log(ui.item ?
                    "Selected: " + ui.item.label :
                    "Nothing selected, input was " + this.value);
                },
                open: function () {
                    $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
                },
                close: function () {
                    $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:panel id="pnl1" runat="server">
    <asp:TextBox ID="txtSearchText" runat="server" Text="afafd" CssClass="textbox">
                            </asp:TextBox><br />
         <asp:TextBox ID="TextBox1" runat="server" Text="afafd" CssClass="textbox">
                            </asp:TextBox><br />
         <asp:TextBox ID="TextBox2" runat="server" Text="afafd" CssClass="textbox">
                            </asp:TextBox><br />
         <asp:TextBox ID="TextBox3" runat="server" Text="afafd" CssClass="textbox">
                            </asp:TextBox><br />
         <asp:TextBox ID="TextBox4" runat="server" Text="afafd" CssClass="textbox">
                            </asp:TextBox>
       </asp:panel>
    </div>
    </form>

    <!-- Common JS -->
        <!-- jQuery framework -->
        <script src="/BAT_files/ga.js" async="" type="text/javascript"></script>
        <%--<script src="/BAT_files/jquery_015.js"></script>
        <script src="/BAT_files/jquery-migrate.js"></script>--%>
        <!-- bootstrap Framework plugins -->
        <%--<script src="/BAT_files/bootstrap.js"></script>--%>
        <script src="bootstrap/js/bootstrap.js"></script>
        <!-- top menu -->
        <script src="/BAT_files/jquery_003.js"></script>
        <!-- top mobile menu -->
        <script src="/BAT_files/selectnav.js"></script>
        <!-- actual width/height of hidden DOM elements -->
        <script src="/BAT_files/jquery_006.js"></script>
        <!-- jquery easing animations -->
        <script src="/BAT_files/jquery_009.js"></script>
        <!-- power tooltips -->
        <script src="/BAT_files/jquery_012.js"></script>
        <!-- date library -->
        <script src="/BAT_files/moment.js"></script>
        <!-- common functions -->
        <script src="/BAT_files/beoro_common.js"></script>



<%--        <script src="/BAT_files/jquery-ui-1.js"></script>--%>

        <script src="/BAT_files/jquery_002.js"></script>

        <script src="/BAT_files/jquery_004.js"></script>

        <script src="/BAT_files/fullcalendar.js"></script>

        <script src="/BAT_files/jquery.js"></script>
        <script src="/BAT_files/jquery_005.js"></script>
        <script src="/BAT_files/jquery_013.js"></script>
        <script src="/BAT_files/jquery_007.js"></script>
        <script src="/BAT_files/jquery_011.js"></script>
        <script src="/BAT_files/jquery_010.js"></script>

        <script src="/BAT_files/plugin.js"></script>

        <script src="/BAT_files/jquery_008.js"></script>
        <script src="/BAT_files/jquery_014.js"></script>

        <script src="/BAT_files/beoro_dashboard.js"></script>

        <link href="/BAT_files/sticky.css" rel="stylesheet" />
        <script src="/BAT_files/sticky.min.js"></script>
        <script src="/BAT_files/bootbox.min.js"></script>
        <script src="/BAT_files/beoro_notifications.js"></script>
</body>
</html>
