<%@ Control Language="C#" AutoEventWireup="true" CodeFile="itemAvailability.ascx.cs" Inherits="Modules_widgets_itemAvailability" %>
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
		                    text: 'Item Trend'
		                },
		                subtitle: {
		                    text: 'Item Trend Report'
		                },
		                xAxis: {
		                    title:{
		                        text: 'Months'

		                    },
		                    categories: [<%= getMonths()%>]
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
		                series: [<%= getData()%>]
		            });
		        });

		    });
		</script>


<%--<script src="../../Highcharts-2.3.5/js/highcharts.js"></script>
<script src="../../Highcharts-2.3.5/js/modules/exporting.js"></script>--%>

<div id="iAcontainer" class="graphsdiv" style="min-width: 600px; height: 300px;"></div>
