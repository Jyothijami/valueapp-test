<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/SalesMP1.master" AutoEventWireup="true" CodeFile="DailyReportDoc.aspx.cs" Inherits="Modules_SM_DailyReportDoc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">


      <link href="/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/css/londinium-theme.css" rel="stylesheet" type="text/css" />
    <link href="/css/styles.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function hi() {
            // event.preventDefault();
            swal({
                title: 'System Meassage',
                text: "Data Submitted Sucessfully",
                type: 'success',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            })
            .then(function () {
                // Set data-confirmed attribute to indicate that the action was confirmed
                window.location = 'DailyReportView.aspx';
            }).catch(function (reason) {
                // The action was canceled by the user
            });

        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
        <ContentTemplate>
           
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h6 class="panel-title">Daily Report Details</h6>
                </div>
                <div class="panel-body">
                    <div class ="form-group">
                    <label class="col-sm-2 control-label text-right">Client's Name :</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtClientsName" CssClass ="form-control " onkeypress="return check(event)" runat="server"></asp:TextBox>
                            </div>
                    <label class="col-sm-2 control-label text-right">Phone No :</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtPhoneNo" CssClass ="form-control " runat="server"></asp:TextBox>
                            </div>
                </div>
                </div>
            </div>
            <!-- /breadcrumbs line -->

            <div class="panel panel-default">
                <div class="panel-heading">
                    <h6 class="panel-title">Documents </h6>
                </div>
                <div class="panel-body">


                    <div class ="form-group">
                    <label class="col-sm-2 control-label text-right">Doc Uploading Date:</label>
                            <div class="col-sm-4">
                              <asp:TextBox ID="txtfloorplanreceiveddate"  CssClass="form-control" type="datepic" runat="server"></asp:TextBox>
                                        <asp:Image
                                            ID="Image1" class="ui-datepicker-trigger" runat="server" ImageUrl="~/images/calendar.png"></asp:Image>
                                      
                            </div>
                    <label class="col-sm-2 control-label text-right">Document: </label>
                            <div class="col-sm-4">
                               <asp:FileUpload ID="FileUpload2" AllowMultiple="true" CssClass="styled form-control" runat="server" />
                            </div>
                </div>
                    <div class="form-group">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label>Remarks :  </label>
                                        <asp:TextBox ID="txtfloorplanremarks" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions text-right">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSave_Click" />
                            </div>
                    <div class="panel panel-default">
        <div class="panel-heading">
            <h6 class="panel-title">Documnet Details Received</h6>
        </div>
        <div class="panel-body">
            <div class="">
                <asp:GridView ID="gvFloorPlan" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False">
                                    <Columns>

                                        <asp:BoundField DataField="FLOORPLAN_ENQID" HeaderText="Sl.No" SortExpression="FLOORPLAN_ENQID">
                                            <HeaderStyle Font-Size="Smaller" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="FLOORPLAN_RECEIVEDDATE" HeaderText="Received Date" SortExpression="FLOORPLAN_RECEIVEDDATE" DataFormatString="{0:dd/MM/yyyy}">
                                            <HeaderStyle Font-Size="Smaller" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="FLOORPLAN_REMARKS" HeaderText="Remarks" SortExpression="FLOORPLAN_REMARKS">
                                            <HeaderStyle Font-Size="Smaller" />
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Documents" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <span class="text-center">
                                                    <asp:HyperLink ID="lbtnDocuments" runat="server" CssClass="btn btn-icon btn-success" Target="_blank" NavigateUrl='<%# "~/Content/FloorPlanDrawings/" + Eval("FLOORPLAN_DOCUMENTS") %>'><i class="icon-attachment"></i></asp:HyperLink>
                                                </span>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="Smaller" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <span class="text-center">
                                                    <asp:LinkButton ID="lbtnFloorDetails" runat="server" CssClass="btn btn-icon btn-danger" OnClick="lbtnFloorDetails_Click"><i class="icon-remove3"></i></asp:LinkButton>
                                                </span>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>


                </div></div>

             </div>

                    
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

