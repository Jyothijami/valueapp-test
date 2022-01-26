<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalesLead.aspx.cs" Inherits="Modules_SM_SalesLead" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../css/styles.css" rel="stylesheet" />
    <link href="../../css/select2.min.css" rel="stylesheet" />
    <link href="../../css/londinium-theme.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.datatables.net/rowreorder/1.2.7/css/rowReorder.dataTables.min.css" type="text/css"/>
     <link rel="stylesheet" href="https://cdn.datatables.net/responsive/2.2.7/css/responsive.dataTables.min.css" type="text/css"/>
     <script type="text/javascript" src="https://code.jquery.com/jquery-1.10.2.js"></script>

      <script type="text/javascript" src="https://cdn.datatables.net/1.10.12/js/jquery.dataTables.min.js"></script>
    
</head>
<body>
    <form id="form1" runat="server">
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
    <div class="form-horizontal ">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        
                                                 <div class="form-group">
                                                    <label class="col-sm-1 control-label text-right">Brand :</label>
                                                    <div class="col-sm-2">
                                                         <asp:DropDownList ID="ddlBrand2" CssClass="form-control " runat="server" OnSelectedIndexChanged="ddlBrand2_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                    </div>
                                                    <label class="col-sm-1 control-label text-right">Category :</label>
                                                    <div class="col-sm-2">
                                                        <asp:DropDownList ID="ddlCategory" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>

                                                    </div>
                                                     <label class="col-sm-1 control-label text-right">Sub Category :</label>
                                                    <div class="col-sm-2">
                                                        <asp:DropDownList ID="ddlSubCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubCat_SelectedIndexChanged"></asp:DropDownList>

                                                    </div>
                                                </div >
                                                 <div class="form-group">
                                                    <div class="datatable">
                                                        <asp:GridView ID="gvItemMaster" runat="server" OnRowDataBound ="gvItemMaster_RowDataBound" AutoGenerateColumns="False" Width="100%" >
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

                                                              
                                                                <asp:TemplateField HeaderText="Image">
                                                                    <ItemTemplate>
                                                                        <asp:Image ID="Image" runat="server" EnableTheming="False" Height="132px" ImageUrl='<%# Eval("Item_Image","~/Content/ItemImage/{0}") %>' Width="141px" />
                                                                          </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Tech Image">
                                                                    <ItemTemplate>
                                                                        <asp:Image ID="Image_Spec" runat="server" EnableTheming="False" Height="132px" ImageUrl='<%# Eval("Item_Specification_Image","~/Content/ItemDrawings/{0}") %>'
                                                                            Width="141px" />
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <%--<asp:TemplateField HeaderText="Spare Image">
                                <ItemTemplate>
                                    <asp:Image ID="Image_Spare" runat="server" EnableTheming="False" Height="132px" ImageUrl='<%# Eval("Item_attachments","~/Content/ItemAttachments/{0}") %>'
                                        Width="141px" /><br />
                                    <asp:FileUpload ID="fileupload3" runat ="server" Width ="100px"/>
                                    <asp:ImageButton ID="ibtmImage3" runat="server" ImageUrl="~/Images/tick.png" CommandName ="TechSave" Width="18px" CommandArgument='<%# Eval("ITEM_CODE").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                                               
                                                                <asp:BoundField DataField="F2" HeaderText="Discontinued" />
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <span style="color: #FF0000">No Data to Display</span>
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                        
                                                    </div>
                                                     
                                                 </div>
                                            
                                    </div>
                                </div>
                            </div>
    </form>
</body>
</html>
