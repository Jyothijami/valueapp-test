<%@ Control Language="C#" AutoEventWireup="true" CodeFile="stockReport.ascx.cs" Inherits="Modules_widgets_stockReport" %>
<%--		<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>--%>
<%@ OutputCache Duration="300" VaryByParam="none" %>

		<script type="text/javascript">
		    $(function () {
		        var SQchart;
		        $(document).ready(function () {
		            SQchart = new Highcharts.Chart({
		                chart: {
		                    renderTo: 'iAcontainer',
		                    type: 'column'
		                },
		                title: {
		                    text: 'Stock Report'
		                },
		                subtitle: {
		                    text: 'Item Stock Report'
		                },
		                xAxis: {
		                    title: {
		                        text: 'Locations'
		                    },
		                    categories: [<%= getLocation()%>]
		                },
		                yAxis: {
		                    min: 0,
		                    title: {
		                        text: 'Item Availabilty'
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
                                    this.x + ': ' + this.y + ' Items';
		                    }
		                },
		                plotOptions: {
		                    column: {
		                        pointPadding: 0.2,
		                        borderWidth: 0
		                    }
		                },
		                series: [<%= getItemQty()%>],
		            });
		        });

		    });
		</script>
<%--<script src="../../Highcharts-2.3.5/js/highcharts.js"></script>
<script src="../../Highcharts-2.3.5/js/modules/exporting.js"></script>--%>

<div id="iAcontainer" class="graphsdiv"  style="width: 600px; height: 300px;"></div>
