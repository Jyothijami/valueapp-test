<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="ReadingQRCode.aspx.cs" Inherits="Modules_Warehouse_ReadingQRCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/webrtc-adapter/3.3.3/adapter.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/vue/2.1.10/vue.min.js"></script>
    <script type="text/javascript" src="https://rawgit.com/schmich/instascan-builds/master/instascan.min.js"></script>
  <%--  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />--%>
       <link href="/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/css/londinium-theme.css" rel="stylesheet" type="text/css" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">

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
                            <asp:TextBox ID="txtQR" AutoPostBack="true" OnTextChanged="txtQR_TextChanged" runat="server"></asp:TextBox>
                               <asp:Label ID="Label3" runat="server" Visible ="false"  ></asp:Label>
                       
                        </div>
                   </div>
                </div>



                <div class="panel-body" runat="server">

                     <asp:TextBox ID="txtItemCode" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtBrand" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtModelNo" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtColorName" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtQty" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtRemarks" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtClientName" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtBrandId" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtColorId" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtRemarks1" runat ="server" Visible ="false" ></asp:TextBox>
                    <asp:Button ID="btnadd" runat="server" OnClick="btnAdd_Click" Text="Button" />

                </div>


                <div class="panel-body">

                       <asp:GridView ID="gvMovingItems" runat="server" AutoGenerateColumns="False" Style="text-align: center" Width="100%" >
                                        <Columns>

                                            <asp:BoundField DataField="ItemCode" HeaderText="Itemcode" />
                                            <asp:BoundField DataField="Brand" HeaderText="Brand" />
                                            <asp:BoundField DataField="ModelNo" HeaderText="ModelNo" />
                                            <asp:BoundField DataField="Color" HeaderText="Color" />
                                            <%--<asp:BoundField DataField="Qty" HeaderText="Qty" />--%>
                                            <asp:TemplateField HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Bind("Qty") %>'>></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="Moving Location">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddllocation" runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                            <asp:BoundField DataField="Remarks" HeaderText="Description" >
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ClientName" HeaderText="ClientName" />
                                            <asp:BoundField DataField="BrandId" HeaderText="BrandId" />
                                            <asp:BoundField DataField="ColorId" HeaderText="ColorId" />
                                            <asp:TemplateField HeaderText="Remarks" ControlStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRemarks" runat="server" Text='<%# Bind("Remark") %>'>></asp:TextBox>
                                                </ItemTemplate>
                                                <ControlStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                        </Columns>
                                    </asp:GridView>
                </div>






            </div>
        </div>

        <script type="text/javascript">
            var scanner = new Instascan.Scanner({ video: document.getElementById('preview'), scanPeriod: 5, mirror: false });

            Instascan.Camera.getCameras().then(function (cameras) {
                if (cameras.length > 0) {
                    scanner.start(cameras[1]);
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

</asp:Content>

