<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QrBackCam.aspx.cs" Inherits="QrBackCam" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/webrtc-adapter/3.3.3/adapter.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/vue/2.1.10/vue.min.js"></script>
    <script type="text/javascript" src="https://rawgit.com/schmich/instascan-builds/master/instascan.min.js"></script>
  <%--  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />--%>
       <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/londinium-theme.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
   
         <div class="page-header">
        <div class="page-title">
            <h3>Qr Code Reading Back Camera</h3>
        </div>
    </div>
    <!-- /page header -->

    <!-- Breadcrumbs line -->
    <div class="breadcrumb-line">
        <ul class="breadcrumb">
            <li class="active">Qr Code</li>
        </ul>
    </div>
    <!-- /breadcrumbs line -->


        <div class="form-horizontal">
            <div class="panel panel-danger">

                <div class="panel-body">
                   <div class="form-group">
                       <label class="col-sm-2 control-label text-right">Preivew :</label>
                       <div class="col-sm-4">
                           <video id="preview" width="100%" height="300px"></video>
                       </div>
                        <label class="col-sm-2 control-label text-right">Qr Code :</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtQrcode" AutoPostBack="true" OnTextChanged="txtQrcode_TextChanged" runat="server"></asp:TextBox>
                       </div>
                   </div>
                </div>



                <div class="panel-body" runat="server">

                    <asp:TextBox ID="txtitemcode" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtcolor" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtUom" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:Button ID="btnadd" runat="server" OnClick="btnadd_Click" Text="Button" />

                </div>


                <div class="panel-body">

                       <asp:GridView ID="gvItems" CssClass="table table-striped table-bordered" runat="server" AutoGenerateColumns="False" 
                                Width="100%" OnRowDeleting="gvItems_RowDeleting">
                                <Columns>
                                    <asp:CommandField ShowDeleteButton="True" />
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                                    <asp:BoundField DataField="Color" HeaderText="Color" />
                                    <asp:BoundField DataField="Uom" HeaderText="Uom" />

                                     <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtitemqty" CssClass="form-control" runat="server" Text='<%# Bind("Qty") %>'></asp:TextBox>
                                                </ItemTemplate>
                                     </asp:TemplateField>



                                </Columns>
                                <EmptyDataTemplate>
                                    <div style="text-align: center">
                                        <span class="auto-style1" style="color: #CC0000">No Records Added</span>
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                </div>






            </div>
        </div>

        <script type="text/javascript">
            var scanner = new Instascan.Scanner({ video: document.getElementById('preview'), scanPeriod: 5, mirror: false });

            Instascan.Camera.getCameras().then(function (cameras) {
                if (cameras.length > 0) {
                    scanner.start(cameras[0]);
                    $('[name="options"]').on('change', function () {
                        if ($(this).val() == 1) {
                            if (cameras[1] != "") {
                                scanner.start(cameras[1]);
                            } else {
                                alert('No Front camera found!');
                            }
                        } else if ($(this).val() == 2) {
                            if (cameras[0] != "") {
                                scanner.start(cameras[0]);
                            } else {
                                alert('No Back camera found!');
                            }
                        }
                    });
                } else {
                    console.error('No cameras found.');
                    alert('No cameras found.');
                }
            }).catch(function (e) {
                console.error(e);
                // alert(e);
            });




            scanner.addListener('scan', function (c) {
                document.getElementById('txtQrcode').value = c;
                var clickButton = document.getElementById("<%= btnadd.ClientID %>");
                clickButton.click();
            });
</script>


         <div class="btn-group btn-group-toggle mb-5" data-toggle="buttons" runat="server" visible="false">
  <label class="btn btn-primary active">
    <input type="radio" name="options" value="1" autocomplete="off" checked/> Front Camera
  </label>
  <label class="btn btn-secondary">
    <input type="radio" name="options" value="2" autocomplete="off"/> Back Camera
  </label>
</div>
    </form>
</body>
</html>
