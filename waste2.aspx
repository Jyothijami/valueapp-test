<%@ Page Language="C#" AutoEventWireup="true" CodeFile="waste2.aspx.cs" Inherits="waste2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/js/jquery-1.9.1.js"></script>
    <script src="/js/jquery-ui-1.10.0.custom.js"></script>
    <link href="/js/ui/1.10.2/themes/smoothness/jquery-ui.css" rel="stylesheet" />
    <script src="/js/ui/1.10.2/jquery-ui.js"></script>
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <link href="/js/ui/1.10.1/themes/base/jquery-ui.css" rel="stylesheet" />
    <!-- noty -->
  <script type="text/javascript" src="noty-master/js/noty/jquery.noty.js"></script>
  <!-- layouts -->
  <script type="text/javascript" src="noty-master/js/noty/layouts/top.js"></script>
  <script type="text/javascript" src="noty-master/js/noty/layouts/topRight.js"></script>
  <!-- themes -->
  <script type="text/javascript" src="noty-master/js/noty/themes/default.js"></script>
  <script type="text/javascript">
      function generate(txt, type) {
          //alert("qweqwe");
          var n = noty({
              text: txt,
              type: type,
              dismissQueue: true,
              layout: 'top',
              theme: 'defaultTheme'
          });
          //console.log('html: ' + n.options.id);
          //alert("asd");
          setTimeout(function () {
              $.noty.close(n.options.id);
          }, 5000)
      }

      function generate_topRight(txt, type) {
          var n = noty({
              text: txt,
              type: type,
              dismissQueue: true,
              layout: 'topRight',
              theme: 'defaultTheme'
          });
          console.log('html: ' + n.options.id);
      }

      function generateAll() {
          generate('alert');
          generate('information');
          generate('error');
          generate('warning');
          generate('notification');
          generate('success');
      }

      $(document).ready(function () {
          //alert("asdasd");
          generateAll();
      });

  </script>
      <!-- noty -->
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
