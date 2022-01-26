<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopRatingByEmp.ascx.cs" Inherits="Modules_widgets_TopRatingByEmp" %>
		<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
<%@ OutputCache Duration="60" VaryByParam="None" %>
		<script type="text/javascript">
		    $(function () {
		        var TMUchart;
		        $(document).ready(function () {
		            TMUchart = new Highcharts.Chart({
		                chart: {
		                    renderTo: 'TMUcontainer',
		                    type: 'column'
		                },
		                title: {
		                    text: 'Top Avg Ratings Count'
		                },
		                subtitle: {
		                    text: 'Top Avg Rating Received by Employees'
		                },
		                xAxis: {
		                    title: {
		                        text: 'Employees'

		                    },
		                    categories: [<%= getEMPNames()%>]
		                },
		                yAxis: {
		                    min: 0,
		                    title: {
		                        text: 'Avg Rating Counts'
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
		                    shadow: true,
		                    enabled: false
		                },
		                tooltip: {
		                    formatter: function () {
		                        return '' +
                                    this.x + ': ' + this.y + ' Count';
		                    }
		                },
		                plotOptions: {
		                    column: {
		                        pointPadding: 0.2,
		                        borderWidth: 0
		                    }
		                },
		                series: [<%= getData()%>],
		                dataLabels: {
		                    enabled: true,
		                    rotation: -90,
		                    color: '#FFFFFF',
		                    align: 'right',
		                    format: '{point.y:.1f}', // one decimal
		                    y: 10, // 10 pixels down from the top
		                    style: {
		                        fontSize: '13px',
		                        fontFamily: 'Verdana, sans-serif'
		                    }
		                }
		            });
		        });

		    });
		</script>


<div id="TMUcontainer" class="graphsdiv" style="width: 350px; height: 300px;"></div>