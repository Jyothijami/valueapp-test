<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.master" AutoEventWireup="true" CodeFile="SalesReport.aspx.cs" Inherits="dev_pages_SalesReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script lang="javascript" type="text/javascript">
        function OpenPopupCenter(pageURL, title, w, h) {
            var left = (screen.width - w) / 2;
            var top = (screen.height - h) / 4;  // for 25% - devide by 4  |  for 33% - devide by 3
            var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
        }
    </script>

     <!-- Page header -->
    <div class="page-header">
        <div class="page-title">
            <h3>Customer Details</h3>
        </div>
    </div>
    <!-- /page header -->

    <!-- Breadcrumbs line -->
    <div class="breadcrumb-line">
        <ul class="breadcrumb">
            <li><a href="CustomerMaster.aspx">Home</a></li>
            <li class="active">Customer Detail Report</li>
        </ul>
    </div>
    <!-- /breadcrumbs line -->

    <div class="form-horizontal">
        <div class="panel panel-info">
             <div class="panel-heading">
                <h6 class="panel-title"><i class="icon-pencil"></i>Customer Details</h6>
            </div>
            <div class="panel-body">
                <div class="form-group">


                    <label class="col-md-2 control-label text-right">Customer : <span class="mandatory">*</span></label>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlCustomer" TabIndex="2" Width="100%" CssClass="select-full" OnSelectedIndexChanged ="ddlCustomer_SelectedIndexChanged"  AutoPostBack="true" runat="server">
                        </asp:DropDownList>
                    </div>

                    <label class="col-md-2 control-label text-right">Purchase Order : <span class="mandatory">*</span></label>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlproject" TabIndex="2" Width="100%" CssClass="select-full" AutoPostBack="true" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                 <div class="panel panel-danger">
                     <div class="panel-heading">
                        <h6 class="panel-title"><i class="icon-pencil"></i>Customer Sales Information</h6>
                    </div>
                     <div class="panel-body">
                          <ul class="info-blocks">
                              <li class="bg-info">

                                <div class="top-info">

                                    <a href="#">Sales Enquiry</a><hr />

                                    <strong>No.Of Enquires :<asp:Label ID="lblEnquiryCount" runat="server" Text="0"></asp:Label>
                                    </strong>

                                </div>

                                <span class="bottom-info bg-primary" data-toggle="modal" data-target="#myModalSalesEnq">View Details</span>
                            </li>

                              <%-- Sales Quatations --%>

                            <li class="bg-info">

                                <div class="top-info">

                                    <a href="#">Sales Quatation</a><hr />

                                    <strong>No.Of Quatations :<asp:Label ID="lblquationscount" runat="server" Text="0"></asp:Label>
                                    </strong>

                                </div>

                                <span class="bottom-info bg-primary" data-toggle="modal" data-target="#myModal">View Details</span>
                            </li>

                              <%-- Sales Order --%>

                            <li class="bg-info">

                                <div class="top-info">

                                    <a href="#">Sales Order</a><hr />

                                    <strong>Status :<asp:Label ID="lblsalesorderstatus" runat="server" Text="Not Prepared"></asp:Label>
                                    </strong>

                                </div>

                                <span class="bottom-info bg-primary" data-toggle="modal" data-target="#myModalSales">View Details</span>
                            </li>
                          </ul>
                     </div>
                 </div>
            </div>
        </div>
    </div>


    <%-- Sales Enquires Modal --%>
    <div id="myModalSalesEnq" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Enquiry Details</h4>
                </div>
                <div class="modal-body">
                    <asp:Panel ID="Panel1" runat="server">

                        <div class="form-horizontal">
                            <div class="panel panel-default">
                                <div class="panel-body">



                                    <h6 class="heading-hr">Enquiries Prepared for the Project
                                    </h6>



                                    <div class="form-group">

                                        <asp:GridView ID="gvEnquires" runat="server" CssClass="table table-condensed" AutoGenerateColumns="False">


                                            <Columns>

                                                <asp:BoundField DataField="ENQ_NO" HeaderText="Enquiry No" SortExpression="ENQ_NO"></asp:BoundField>

                                                <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="ENQ_DATE" SortExpression="ENQ_DATE" HeaderText="EnqDate">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CUST_CODE" SortExpression="CUST_CODE" HeaderText="CustomerCode">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CUST_NAME" SortExpression="CUST_NAME" HeaderText="CustomerName">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="ENQ_DUE_DATE" SortExpression="ENQ_DUE_DATE" HeaderText="DueDate" Visible="false">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                                <%--<asp:BoundField DataField="PREPAREDBY" SortExpression="PREPAREDBY" HeaderText="PreparedBy"></asp:BoundField>
                                                <asp:BoundField DataField="APPROVEDBY" SortExpression="APPROVEDBY" HeaderText="ApprovedBy"></asp:BoundField>--%>
                                                <asp:BoundField DataField="ENQ_STATUS" SortExpression="ENQ_STATUS" HeaderText="Status">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CP_SHORT_NAME" HeaderText="Company Name">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>

                                                <asp:TemplateField HeaderText="Attachments" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <span class="text-center">


                                                            <asp:HyperLink ID="lbtnfloorplans" runat="server" CssClass="btn btn-icon btn-primary" Target="_blank" NavigateUrl='<%# "~/Modules/Sales/BoQFloorPlans.aspx?Cid=" + Eval("ENQ_ID") %>'>
                                                                <i class="icon-attachment"></i><strong class="label label-danger">
                                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("Cou") %>'></asp:Label></strong>
                                                            </asp:HyperLink>
                                                        </span>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>





                                            </Columns>


                                        </asp:GridView>




                                    </div>


                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>

     <%-- Quatation Details Modal --%>
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Quatation Details</h4>
                </div>
                <div class="modal-body">
                    <asp:Panel ID="QPanel" runat="server">

                        <div class="form-horizontal">
                            <div class="panel panel-default">
                                <div class="panel-body">



                                    <h6 class="heading-hr">Total Quatations Prepared for the Project
                                    </h6>



                                    <div class="form-group">

                                        <asp:GridView ID="gvTotalQuatations" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:BoundField DataField="QUOT_ID" SortExpression="QUOT_ID" HeaderText="ID"></asp:BoundField>
                                                <asp:BoundField DataField="QUOT_NO" SortExpression="QUOT_NO" HeaderText="Quot No"></asp:BoundField>

                                                <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True" DataField="QUOT_DATE" SortExpression="QUOT_DATE" HeaderText="QuotationDate"></asp:BoundField>
                                                <asp:BoundField DataField="CUST_NAME" SortExpression="CUST_NAME" HeaderText="Customer">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CUST_CONTACT_PERSON" SortExpression="CUST_CONTACT_PERSON" HeaderText="ContactPerson">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CUST_EMAIL" SortExpression="CUST_EMAIL" HeaderText="Email">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PREPAREDBY" SortExpression="PREPAREDBY" HeaderText="PreparedBy">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="APPROVEDBY" SortExpression="APPROVEDBY" HeaderText="ApprovedBy">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CheckedBy" SortExpression="CheckedBy" HeaderText="Sales Person">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Quatation_flag" SortExpression="Quatation_flag" HeaderText="Status">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="QUOT_REVISED_KEY" HeaderText="RevisedKeyHidden"></asp:BoundField>
                                                <asp:BoundField DataField="Full_CompName" HeaderText="Company Name"></asp:BoundField>
                                                <asp:TemplateField HeaderText="View" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <span class="text-center">



                                                            <%--  <a runat="server"  href='<%# "~/Modules/Reports/Details/QuatationDetails.aspx?Cid=" + Eval("Quotation_Id") %>'  onclick="window.open(this.href, 'newwindow', 'width=500, height=500'); return false;"> View</a>--%>


                                                            <a runat="server" href='<%# "~/Modules/SM/Quot_Preview.aspx?Cid=" + Eval("Quot_ID") %>' onclick="OpenPopupCenter(this.href, 'newwindow', 1000, 800);return false; ">View</a>


                                                        </span>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="Smaller" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                            </Columns>
                                            <EmptyDataTemplate>
                                                No Data Exist!
                    
                                            </EmptyDataTemplate>
                                            <SelectedRowStyle BackColor="LightSteelBlue" />
                                        </asp:GridView>




                                    </div>


                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>

    <%-- Sales Order Modal --%>
    <div id="myModalSales" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Sales Order Details</h4>
                </div>
                <div class="modal-body">
                    <asp:Panel ID="Panel2" runat="server">

                        <div class="form-horizontal">
                            <div class="panel panel-default">
                                <div class="panel-body">



                                    <div class="form-group">

                                        <asp:GridView ID="gvSalesorder" runat="server" CssClass="table table-bordered" OnRowDataBound ="gvSalesorder_RowDataBound" AutoGenerateColumns="False">
                                            <Columns>

                                                <asp:BoundField DataField="SO_ID" HeaderText="SOID"></asp:BoundField>
                                                <asp:BoundField DataField="SO_NO" HeaderText="SONo"></asp:BoundField>

                                                <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True" DataField="SO_DATE" HeaderText="PurchaseOrderDate"></asp:BoundField>
                                                <asp:BoundField DataField="CUST_NAME" HeaderText="Customer">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CUST_CONTACT_PERSON" HeaderText="ContactPerson">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Executive" HeaderText="Executive Name">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PREPAREDBY" HeaderText="PreparedBy">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="APPROVEDBY" HeaderText="Obsolete/Closed By">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SO_ACCEPTANCE_FLAG" HeaderText="Status">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Full_CompName" HeaderText="Company Name">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                </asp:BoundField>

                                                <asp:BoundField DataField="balanceqty" HeaderText="Status" />

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnsatte" runat="server" CommandName="Statement" Width="100%" CommandArgument='<%# Eval("SO_ID").ToString() %>' Text="statement" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>





                                            </Columns>
                                        </asp:GridView>




                                    </div>


                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>


</asp:Content>

