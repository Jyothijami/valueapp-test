<%@ Control Language="C#" AutoEventWireup="true" CodeFile="graph_SalesQuotationsMonthly2.ascx.cs" Inherits="Modules_widgets_graph_SalesQuotationsMonthly2" %>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
		<script type="text/javascript">
$(function () {
    var SalesQMWchart;
    $(document).ready(function() {
        SalesQMWchart = new Highcharts.Chart({
            chart: {
                renderTo: 'SalesQMWcontainer',
                type: 'column'
            },
            title: {
                text: 'Stacked column chart'
            },
            xAxis: {
                categories: ['Apples', 'Oranges', 'Pears', 'Grapes', 'Bananas']
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'Total fruit consumption'
                },
                stackLabels: {
                    enabled: true,
                    style: {
                        fontWeight: 'bold',
                        color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                    }
                }
            },
            legend: {
                align: 'right',
                x: -100,
                verticalAlign: 'top',
                y: 20,
                floating: true,
                backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColorSolid) || 'white',
                borderColor: '#CCC',
                borderWidth: 1,
                shadow: false
            },
            tooltip: {
                formatter: function() {
                    return '<b>'+ this.x +'</b><br/>'+
                        this.series.name +': '+ this.y +'<br/>'+
                        'Total: '+ this.point.stackTotal;
                }
            },
            plotOptions: {
                column: {
                    stacking: 'normal',
                    dataLabels: {
                        enabled: true,
                        color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white'
                    }
                }
            },
            series: [{
                name: 'Srinivas',
                data: [5, 3, 4, 7, 2]
            }, {
                name: 'Das',
                data: [2, 2, 3, 2, 1]
            }, {
                name: 'Jagadish',
                data: [3, 4, 4, 2, 5]
            }]
        });
    });
    
});
		</script>

<div id="SalesQMWcontainer" class="graphsdiv" style="min-width: 400px; height: 300px; "></div>
