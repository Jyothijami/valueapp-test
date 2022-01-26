<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/SalesMP1.master" AutoEventWireup="true" CodeFile="LeadNew.aspx.cs" Inherits="Modules_SM_LeadNew" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../css/bootstrap.min.css" rel="stylesheet" />
    <%--<link href="../../css/select2.min.css" rel="stylesheet" />--%>
    <%--<link href="../../css/styles.css" rel="stylesheet" />--%>
    <link rel="stylesheet" href="https://cdn.datatables.net/rowreorder/1.2.7/css/rowReorder.dataTables.min.css" type="text/css"/>
     <link rel="stylesheet" href="https://cdn.datatables.net/responsive/2.2.7/css/responsive.dataTables.min.css" type="text/css"/>
     <script type="text/javascript" src="https://code.jquery.com/jquery-1.10.2.js"></script>


      <link rel="stylesheet" href='https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.css'
        media="screen" />
     <script type="text/javascript" src='https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.js'> </script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.12/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //fnPageLoad();
        });

        function fnPageLoad() {
            $('#<%=gvItemMaster.ClientID%>').prepend($("<thead></thead>").append($('#<%=gvItemMaster.ClientID%>').find("tr:first"))).DataTable({

                  bSort: true,
                  dom: '<"html5buttons"B>lTfgitp',
                  //lengthChange: false,
                  pageLength: 10,
                  buttons: ['copyHtml5',
     'excelHtml5',
     'csvHtml5',
     'pdfHtml5'],
                  bStateSave: true,
                  order: [[0, 'desc']],

                  responsive: true,
                  rowReorder: {
                      selector: 'td:nth-child(2)'
                  }


              });
          }
       </script>

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
                window.location = 'SalesQuotation.aspx';
            }).catch(function (reason) {
                // The action was canceled by the user
            });

        }
    </script>

    <script type="text/javascript">
        function Lead() {
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
                //window.location = 'LeadNew.aspx';
            }).catch(function (reason) {
                // The action was canceled by the user
            });

        }
    </script>

    <script type="text/javascript">
        function ISQuot() {
            // event.preventDefault();
            swal({
                title: 'System Meassage',
                text: "Quotation has already prepared for this Sales Lead",
                type: 'success',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            })
            .then(function () {
                // Set data-confirmed attribute to indicate that the action was confirmed
                //window.location = 'LeadNew.aspx';
            }).catch(function (reason) {
                // The action was canceled by the user
            });

        }
    </script>
   
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">



  

       


    <div class="form-horizontal ">
        <div class="panel panel-default">
            <div class="panel-heading">
                 <div class ="form-group">
                    <label class="col-sm-2 control-label text-right">Search Model No :</label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtModelNo" runat="server"></asp:TextBox> &nbsp;&nbsp;
                    
                    </div>
                    <div class="col-sm-2">
                        <asp:Button ID="btnModelSearch" OnClick="btnModelSearch_Click" runat="server" Text="Search Model No" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label text-right">Brand :</label>
                    <div class="col-sm-2">
                        <asp:DropDownList ID="ddlBrand2" CssClass="form-control " runat="server" OnSelectedIndexChanged="ddlBrand2_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                    </div>
                    <label class="col-sm-1 control-label text-right">Category :</label>
                    <div class="col-sm-2">
                        <asp:DropDownList ID="ddlCategory" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>

                    </div>
                    <label class="col-sm-1 control-label text-right">Sub Category :</label>
                    <div class="col-sm-2">
                        <asp:DropDownList ID="ddlSubCat" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubCat_SelectedIndexChanged"></asp:DropDownList>

                    </div>
                </div>
               
                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
    <ContentTemplate>
                <div class="form-group">
                    <div class="datatable">
                        <asp:GridView ID="gvItemMaster" runat="server" CssClass="table table-bordered" OnRowDataBound="gvItemMaster_RowDataBound" AutoGenerateColumns="False" Width="100%" ForeColor="Black">
                            <PagerStyle CssClass="pagination" HorizontalAlign="Left" VerticalAlign="Middle" />
                            <FooterStyle ForeColor="#0066ff" />
                            <Columns>
                                <asp:BoundField DataField="HSN_Code" HeaderText="HSN Code" />
                                <asp:BoundField DataField="GST Tax" HeaderText="GST Rate" />
                                <asp:BoundField DataField="ITEM_CODE" HeaderText="Item code" />
                                <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="Model No" />
                                <asp:BoundField DataField="ITEM_NAME" HeaderText="Item Name" />
                                <asp:BoundField DataField="ITEM_SERIES" Visible="false" HeaderText="Item Series" />
                                <asp:BoundField DataField="ITEM_SPEC" HeaderText="Description" />
                                <asp:BoundField DataField="IT_TYPE" HeaderText="Sub Category" />
                                <asp:BoundField DataField="PRODUCT_COMPANY_NAME" HeaderText="Brand" />
                                <asp:BoundField DataField="COLOUR_NAME" HeaderText="COLOUR NAME" />

                                <asp:BoundField DataField="Item_Price" HeaderText="Price" />


                                <asp:TemplateField HeaderText="Image" HeaderStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Image ID="Image" runat="server" EnableTheming="False" Height="132px" ImageUrl='<%# Eval("Item_Image","~/Content/ItemImage/{0}") %>' Width="151px" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Tech Image">
                                    <ItemTemplate>
                                        <asp:Image ID="Image_Spec" runat="server" EnableTheming="False" Height="132px" ImageUrl='<%# Eval("Item_Specification_Image","~/Content/ItemDrawings/{0}") %>'
                                            Width="141px" />
                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:BoundField DataField="F2" HeaderText="Discontinued" />
                                <asp:BoundField DataField="COLOR_ID" HeaderText="Color Id" />

                                <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDetQty" runat="server" Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discount" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDetDisc" Text="0" Width="50px" runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Room" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDetRoom" Text="-" CssClass="form-control" Width="150px" runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Floor" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDetFloor" Text="-" CssClass="form-control" Width="100px" runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Save" HeaderStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <span class="text-center">
                                            <asp:LinkButton ID="lbtnSave" runat="server" OnClick="lbtnSave_Click" Text="Save" CssClass="btn btn-icon btn-primary"></asp:LinkButton>
                                        </span>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <span style="color: #FF0000">No Data to Display</span>
                            </EmptyDataTemplate>
                        </asp:GridView>


                        <asp:GridView ID="gvInterestedProducts" runat="server" AutoGenerateColumns="False"
                            OnRowDataBound="gvInterestedProducts_RowDataBound" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDelete" CausesValidation="false" Text="Delete" ForeColor="Blue" runat="server" __designer:wfdid="w5" OnClick="lbtnDelete_Click">Delete</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ModelNo" HeaderText="Model No">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Brand" HeaderText="Brand">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="UOM" HeaderText="UOM">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtLeadDetQty" Text='<%# Bind("Quantity") %>' AutoPostBack="true" OnTextChanged="txtLeadDetQty_TextChanged" runat="server" Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disc" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDisc" Text='<%# Bind("DocCharges") %>' AutoPostBack="true" OnTextChanged="txtDisc_TextChanged" runat="server" Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="GrossAmount" HeaderText="GrossAmount"></asp:BoundField>
                                <asp:TemplateField HeaderText="Room" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRomm" Text='<%# Bind("Room") %>' runat="server" Width="150px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Specifications" NullDisplayText="-" HeaderText="Specifications"></asp:BoundField>
                                <asp:BoundField DataField="Remarks" NullDisplayText="-" HeaderText="Remarks"></asp:BoundField>
                                <asp:BoundField DataField="Priority" HeaderText="Priority"></asp:BoundField>

                                <asp:BoundField DataField="DocInFavourOf" NullDisplayText="-" HeaderText="Doc In Favour Of"></asp:BoundField>
                                <asp:BoundField DataField="EMDCharges" NullDisplayText="-" HeaderText="EMD Charges"></asp:BoundField>
                                <asp:BoundField DataField="EMDInFavourOf" NullDisplayText="-" HeaderText="EMD In Favour Of"></asp:BoundField>

                                <%--<asp:BoundField DataField="Room" HeaderText="Room"></asp:BoundField>--%>
                                <asp:BoundField DataField="Color" HeaderText="Color "></asp:BoundField>
                                <asp:BoundField DataField="ColorId" HeaderText="ColorId"></asp:BoundField>
                                <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                <asp:BoundField DataField="ENQ_DET_ID" HeaderText="Det Id"></asp:BoundField>
                                <asp:BoundField HeaderText="Total"></asp:BoundField>
                                <asp:BoundField HeaderText="Sp Price"></asp:BoundField>

                                <asp:BoundField DataField="GST" HeaderText="GST"></asp:BoundField>
                                <asp:BoundField HeaderText="GST Amt"></asp:BoundField>


                                <asp:TemplateField HeaderText="S.No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSrlOrder" runat="server" Text='<%#Container.DataItemIndex+1 %>' CssClass="form-label"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                        </asp:GridView>
                    </div>

                </div>

           </ContentTemplate>
</asp:UpdatePanel>
                <div class="form-group">
                    <asp:Button ID="btnQuot" runat="server" Text="Convert Quotation" OnClick="btnQuot_Click" />
                </div>
            </div>
        </div>
    </div>


      
        
</asp:Content>

