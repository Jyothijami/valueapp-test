<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SalesLead_OpenVsClose.ascx.cs" Inherits="Modules_widgets_SalesLead_OpenVsClose" %>
<%@ OutputCache Duration="300" VaryByParam="none" %>
<%--		<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>--%>
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
		                    text: 'Open , Closed & New Sales Leads'
		                },
		                tooltip: {
		                    pointFormat: '{series.name}: <b>{point.percentage}%</b>',
		                    percentageDecimals: 5
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
		                                return '<b>' + this.point.name + '</b>: ' + Math.round(this.percentage) + ' %';
		                            }
		                        }
		                    }
		                },
		                series: [{
		                    type: 'pie',
		                    name: 'Sales Leads share',
		                    data: [
                                <%=getData()%>
		                    ]
		                }]
		            });
		        });

		    });
		</script>


<div id="container" class="graphsdiv" style="width: 350px; height: 300px;"></div>
