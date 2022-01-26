<%@ Control Language="C#" AutoEventWireup="true" CodeFile="graph_topEmpLeaves.ascx.cs" Inherits="Modules_widgets_graph_topEmpLeaves" %>
<%@ OutputCache Duration="300" VaryByParam="none" %>
		<script type="text/javascript">
		    $(function () {
		        var TLUchart;
		        $(document).ready(function () {
		            // Create the chart
		            Highcharts.setOptions({
		                lang: {
		                    drillUpText: '<< Back to {series.name}'
		                }
		            });
		            TLUchart = new Highcharts.Chart({
		                chart: {
		                    renderTo: 'TLUcontainer',
		                    type: 'column'
		                },
		                title: {
		                    text: 'Top Leaves Count'
		                },
		                subtitle: {
		                    text: 'Top Leaves Applied by Employees'
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
		                        text: 'Leaves Counts'
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
                                    this.x + ': ' + this.y + ' Leaves';
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
		                },
		                drilldown: {
		                    drillUpButton: {
		                        relativeTo: 'SpacingBox',
		                        position: {
		                            y: 0,
                                    x: 0
		                        },
		                        theme: {
		                            fill: 'white',
		                            'stroke-width': 1,
		                            stroke: 'silver',
		                            r: 0,
		                            states: {
		                                hover: {
                                            fill: '#bada55'
		                                },
		                                select: {
		                                    stroke: '#039',
                                            fill: '#bada55'
		                                }
		                            }
		                        }
		                    },
		                    series: [{
		                        name: 'Emp',
		                        colorByPoint: true,
		                        data: [{

		                        }]
		                    }]
		                }
		            });
		        });

		    });
		</script>


<div id="TLUcontainer" class="graphsdiv" style="width: 750px; height: 300px;"></div>
