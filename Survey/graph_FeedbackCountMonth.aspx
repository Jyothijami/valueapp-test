<%@ Page Language="C#" AutoEventWireup="true" CodeFile="graph_FeedbackCountMonth.aspx.cs" Inherits="Survey_graph_FeedbackCountMonth" %>
<%@ OutputCache Duration="240" VaryByParam="none" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
</head>
<body>
    <form id="form1" runat="server">
        <script type="text/javascript">
            $(function () {
                var SalesQMchart;
                $(document).ready(function () {
                    // Create the chart
                    Highcharts.setOptions({
                        lang: {
                            drillUpText: '<< Back to {series.name}'
                        }
                    });

                    SalesQMchart = new Highcharts.Chart({
                        chart: {
                            renderTo: 'SalesQMchart',
                            type: 'column'
                        },
                        title: {
                            text: 'Sales Quotation Monthwise'
                        },
                        xAxis: {
                            type: 'category'
                        },
                        yAxis: {
                            min: 0,
                            title: {
                                text: 'Counts'
                            }
                        },
                        legend: {
                            enabled: false
                        },

                        plotOptions: {
                            series: {
                                borderWidth: 0,
                                dataLabels: {
                                    enabled: true,
                                }
                            }
                        },

                        series: [{
                            name: 'Months',
                            colorByPoint: true,
                            data: [{
                                name: 'January',
                                y: <%= getMDataCount("Jan") %>,
                        drilldown: 'January'
                    }, {
                        name: 'February',
                        y: <%= getMDataCount("Feb") %>,
                        drilldown: 'February'
                    }, {
                        name: 'March',
                        y: <%= getMDataCount("Mar") %>,
                        drilldown: 'March'
                    }, {
                        name: 'April',
                        y: <%= getMDataCount("Apr") %>,
                        drilldown: 'April'
                    }, {
                        name: 'May',
                        y: <%= getMDataCount("May") %>,
                        drilldown: 'May'
                    }, {
                        name: 'June',
                        y: <%= getMDataCount("Jun") %>,
                        drilldown: 'June'
                    }, {
                        name: 'July',
                        y: <%= getMDataCount("Jul") %>,
                        drilldown: 'July'
                    }, {
                        name: 'August',
                        y: <%= getMDataCount("Aug") %>,
                        drilldown: 'August'
                    }, {
                        name: 'September',
                        y: <%= getMDataCount("Sep") %>,
                        drilldown: 'September'
                    }, {
                        name: 'October',
                        y: <%= getMDataCount("Oct") %>,
                        drilldown: 'October'
                    }, {
                        name: 'November',
                        y: <%= getMDataCount("Nov") %>,
                        drilldown: 'November'
                    }, {
                        name: 'December',
                        y: <%= getMDataCount("Dec") %>,
                        drilldown: 'December'
                    }
                    ]
                }],
                drilldown: {

                    drillUpButton: {
                        relativeTo: 'spacingBox',
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
                        id: 'January',
                        name: 'January',
                        data: [<%= getMData("Jan") %>
                        ]
                    }, {
                        id: 'February',
                        name: 'February',
                        data: [<%= getMData("Feb") %>
                        ]
                    }, {
                        id: 'March',
                        name: 'March',
                        data: [<%= getMData("Mar") %>
                        ]
                    }, {
                        id: 'April',
                        name: 'April',
                        data: [<%= getMData("Apr") %>
                        ]
                    }, {
                        id: 'May',
                        name: 'May',
                        data: [<%= getMData("May") %>
                        ]
                    }, {
                        id: 'June',
                        name: 'June',
                        data: [<%= getMData("Jun") %>
                        ]
                    }, {
                        id: 'July',
                        name: 'July',
                        data: [<%= getMData("Jul") %>
                        ]
                    }, {
                        id: 'August',
                        name: 'August',
                        data: [<%= getMData("Aug") %>
                        ]
                    }, {
                        id: 'September',
                        name: 'September',
                        data: [<%= getMData("Sep") %>
                        ]
                    }, {
                        id: 'October',
                        name: 'October',
                        data: [<%= getMData("Oct") %>
                        ]
                    }, {
                        id: 'November',
                        name: 'November',
                        data: [<%= getMData("Nov") %>
                        ]
                    }, {
                        id: 'December',
                        name: 'December',
                        data: [<%= getMData("Dec") %>
                        ]
                    }
                    ]
                }
            })
        });

    });
		</script>
<%--<script src="../../Highcharts-2.3.5/js/highcharts.js"></script>--%>
<%--<script src="../../Highcharts-2.3.5/js/modules/data.js"></script>
<script src="../../Highcharts-2.3.5/js/modules/drilldown.js"></script>--%>

<div id="SalesQMchart" class="graphsdiv" style="min-width: 1115px; height: 400px; "></div>
    </form>
</body>
</html>
