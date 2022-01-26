<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CS.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 13pt;
        }
        #chart
        {
            width: 500px;
            height: 300px;
        }
        #chart div
        {
            width: 100px;
        }
        #chart span
        {
            color: red;
            font-size: 8pt;
            font-style: italic;
        }
        #chart img
        {
            height: 100px;
            width: 100px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">
        google.load("visualization", "1", { packages: ["orgchart"] });
        google.setOnLoadCallback(drawChart);
        function drawChart() {
            $.ajax({
                type: "POST",
                url: "CS.aspx/GetChartData",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var data = new google.visualization.DataTable();
                    data.addColumn('string', 'Entity');
                    data.addColumn('string', 'ParentEntity');
                    data.addColumn('string', 'ToolTip');
                    for (var i = 0; i < r.d.length; i++) {
                        var employeeId = r.d[i][0].toString();
                        var employeeName = r.d[i][1];
                        var designation = r.d[i][2];
                        var reportingManager = r.d[i][5] != null ? r.d[i][5].toString() : '';
                        var EmpPhoto = r.d[i][4];
                        var department = r.d[i][3];
                        var brk ='<br/>';
                        data.addRows([[{ v: employeeId ,
                            f: employeeName + '<div>(<span>' + designation + '</span>)</div><div>(<span>' + department + '</span>)</div><img src = "http://183.82.108.55/Content/EmployeeImage/' + EmpPhoto + '" />'
                        }, reportingManager, designation]]);
                        
                    }
                    brk;
                    var chart = new google.visualization.OrgChart($("#chart")[0]);
                    chart.draw(data, { allowHtml: true });
                },
                failure: function (r) {
                    alert(r.d);
                },
                error: function (r) {
                    alert(r.d);
                }
            });
        }
    </script>
    <div id="chart">
    </div>
    </form>
</body>
</html>
