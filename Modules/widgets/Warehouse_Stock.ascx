<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Warehouse_Stock.ascx.cs"
     Inherits="Modules_widgets_Warehouse_Stock" %>

<%@ OutputCache Duration="20" VaryByParam="None" %>
		<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
		<script type="text/javascript">
		    $(function () {
		        var chart;
		        $(document).ready(function () {
		            chart = new Highcharts.Chart({
		                chart: {
		                    renderTo: 'container',
		                    plotBackgroundColor: null,
		                    plotBorderWidth: null,
		                    plotShadow: false
		                },
		                title: {
		                    text: 'Stock Report'
		                },
		                tooltip: {
		                    pointFormat: '{series.name}: <b>{point.percentage}%</b>',
		                    percentageDecimals: 1
		                },
		                plotOptions: {
		                    pie: {
		                        allowPointSelect: true,
		                        cursor: 'pointer',
		                        dataLabels: {
		                            enabled: true,
		                            color: '#000000',
		                            connectorColor: '#000000',
		                            formatter: function () {
		                                return '<b>' + this.point.name + '</b>: ' +  + this.point.count;
		                            }
		                        }
		                    }
		                },
		                series: [{
		                    type: 'pie',
		                    name: 'Stock Report',
		                    data: [
                                <%=getData()%>
		                    ]
		                }]
		            });
		        });7

		    });
		</script>


<script src="../../Highcharts-2.3.5/js/highcharts.js"></script>
<script src="../../Highcharts-2.3.5/js/modules/exporting.js"></script>

<div id="container" runat="server" class="graphdiv_Ajay" style="width: 500px;text-align:center; height: 300px;"></div>

