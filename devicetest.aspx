<%@ Page Language="C#" AutoEventWireup="true" CodeFile="devicetest.aspx.cs" Inherits="devicetest" %>

<!DOCTYPE html>

<!DOCTYPE html>
<html lang="en">
<head runat ="server" >
    <meta charset="UTF-8">
    <title></title>
</head>
<body>
<input type="text" placeholder="input for scanner"/>
</body>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.4/jquery.min.js"></script>
<script src="jquery.barcode-scanner-detector.js"></script>
<script>
    $(function () {
        $("input").focus().BarcodeScannerDetector(function (val) {
            console.log("Barcode scanned: " + val);
        })
    })
</script>
</html>

