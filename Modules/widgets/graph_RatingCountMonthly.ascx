<%@ Control Language="C#" AutoEventWireup="true" CodeFile="graph_RatingCountMonthly.ascx.cs" Inherits="Modules_widgets_graph_RatingCountMonthly" %>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
<%@ OutputCache Duration="300" VaryByParam="none" %>
<script type="text/javascript">
    $(function () {
        var QVPchart;
        $(document).ready(function () {
            QVPchart = new Highcharts.Chart({
                chart: {
                    renderTo: 'QVPcontainer',
                    type: 'column'
                },
                title: {
                    text: 'Monthly Rating Count '
                },
                subtitle: {
                    text: '5 Star Vs 4 Star Vs 3 Star Vs 2 Star 1 Star'
                },
                xAxis: {
                    categories: [
                        'Jan',
                        'Feb',
                        'Mar',
                        'Apr',
                        'May',
                        'Jun',
                        'Jul',
                        'Aug',
                        'Sep',
                        'Oct',
                        'Nov',
                        'Dec'
                    ],
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Count'
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:.1f} no.</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true
                },
                plotOptions: {
                    column: {
                        pointPadding: 0.2,
                        borderWidth: 0
                    }
                },
                series: [{
                    name: '5 Star',
                    data: [<%= getQuotData()%>]

        }, {
            name: '4 Star',
            data: [<%= getPOData()%>]
        }, {
            name: '3 Star',
            data: [<%= getRate3Data()%>]

        }, {
            name: '2 Star',
            data: [<%= getRate2Data()%>]

        }, {
            name: '1 Star',
            data: [<%= getRate1Data()%>]

        }
        ]
            });
        });
    });
		</script>

<script src="../../Highcharts-2.3.5/js/highcharts.js"></script>
<script src="../../Highcharts-2.3.5/js/modules/exporting.js"></script>

<div id="QVPcontainer" class="graphsdiv" style="min-width: 1115px; height: 400px;"></div>