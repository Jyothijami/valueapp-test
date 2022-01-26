<%@ Control Language="C#" AutoEventWireup="true" CodeFile="graph_topMemoUsers.ascx.cs" Inherits="Modules_widgets_graph_topMemoUsers" %>
<%--		<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>--%>
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
		                    text: 'Top Memo Count'
		                },
		                subtitle: {
		                    text: 'Top Memos Received by Employees'
		                },
		                xAxis: {
		                    title:{
		                        text: 'Employees'

		                    },
		                    categories: [<%= getEMPNames()%>]
		                },
		                yAxis: {
		                    min: 0,
		                    title: {
		                        text: 'Memo Counts'
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
                                    this.x + ': ' + this.y + ' Memos';
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
