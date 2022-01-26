<%@ Control Language="C#" AutoEventWireup="true" CodeFile="graph_RatingCountAnually.ascx.cs" Inherits="Modules_widgets_graph_RatingCountAnually" %>
<%@ OutputCache Duration="300" VaryByParam="none" %>
		<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
		<script type="text/javascript">
		    $(function () {
		        var SQchart;
		        $(document).ready(function () {
		            SQchart = new Highcharts.Chart({
		                chart: {
		                    renderTo: 'SQcontainer',
		                    type: 'column'
		                },
		                title: {
		                    text: 'Rating Count Rpt'
		                },
		                subtitle: {
		                    text: 'Overal Rating'
		                },
		                xAxis: {
		                    categories: [<%= getMonths()%>]
		                },
		                yAxis: {
		                    min: 0,
		                    title: {
		                        text: 'Rating Count'
		                    }
		                },
		                legend: {
		                    layout: 'vertical',
		                    backgroundColor: '#FFFFFF',
		                    align: 'left',
		                    verticalAlign: 'top',
		                    x: 100,
		                    y: 70,
		                    floating: true,
		                    shadow: true
		                },
		                tooltip: {
		                    formatter: function () {
		                        return '' +
                                    this.x + ': ' + this.y + '  Count';
		                    }
		                },
		                plotOptions: {
		                    column: {
		                        pointPadding: 0.2,
		                        borderWidth: 0
		                    }
		                },
		                series: [<%= getData()%>]
		            });
		        });

		    });
		</script>


<script src="../../Highcharts-2.3.5/js/highcharts.js"></script>
<script src="../../Highcharts-2.3.5/js/modules/exporting.js"></script>

<div id="SQcontainer" class="graphsdiv" style="width: 750px; height: 300px;"></div>
