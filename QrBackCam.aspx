<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QrBackCam.aspx.cs" Inherits="QrBackCam" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/webrtc-adapter/3.3.3/adapter.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/vue/2.1.10/vue.min.js"></script>
    <script type="text/javascript" src="https://rawgit.com/schmich/instascan-builds/master/instascan.min.js"></script>
  <%--  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />--%>
       <link href="/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/css/londinium-theme.css" rel="stylesheet" type="text/css" />

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
                            <video id="preview" style="width: 100%; height: 80%"></video>
                        </div>
                        <label class="col-sm-2 control-label text-right">Qr Code :</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtQrcode" CssClass="form-control " AutoPostBack="true" OnTextChanged="txtQrcode_TextChanged" runat="server"></asp:TextBox><br />
                            Moving DC No:<asp:TextBox ID="txtDCNo" CssClass="form-control " runat="server" ReadOnly="True"></asp:TextBox><br />
                            Moving DC Date:
                            <asp:TextBox ID="txtmovingdate" CssClass="form-control " type="datepic" runat="server"></asp:TextBox><br />
                            Movinf From :<asp:DropDownList ID="ddlMovingFrom" CssClass="form-control " runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource1" DataTextField="whname" DataValueField="wh_id">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lblMDate2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlMovingFrom" ErrorMessage="Please Select the Moving From" InitialValue="0" ValidationGroup="id">*</asp:RequiredFieldValidator>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="USP_Warehouse_Loc_Select" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="lblCompany" DefaultValue="0" Name="LocID" PropertyName="Text" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <br />
                            Moving To<asp:DropDownList ID="ddlMovingTo" CssClass="form-control " runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource1" DataTextField="whname" DataValueField="wh_id">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lblMDate3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlMovingTo" ErrorMessage="Please Select the Moving To" InitialValue="0" ValidationGroup="id">*</asp:RequiredFieldValidator>
                            <br />
                            Vehicle No:<asp:TextBox ID="txtVechileNo" CssClass="form-control " runat="server"></asp:TextBox>
                            <asp:Label ID="lblMDate4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtVechileNo" ErrorMessage="Please Enter the Vehicle No." ValidationGroup="id">*</asp:RequiredFieldValidator>

                        </div>
                    </div>
                </div>
                <asp:Label ID="lblCompany" Text="1" runat="server" Visible="False"></asp:Label>

                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="USP_Warehouse_Loc_Select" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblCompany" DefaultValue="0" Name="LocID" PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>

                <div class="panel-body" runat="server">

                    <asp:TextBox ID="txtItemCode" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtBrand" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtModelNo" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtColorName" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtQty" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtRemarks" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtClientName" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtBrandId" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtColorId" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtRemarks1" runat="server" Visible="false"></asp:TextBox>
                    <asp:Label ID="Label3" runat="server" Visible="false"></asp:Label>
                    <asp:Button ID="btnadd" runat="server" OnClick="btnadd_Click" ForeColor ="White"  Text="Button" />

                </div>


                <div class="panel-body">

                    <asp:GridView ID="gvItems" CssClass="table table-striped table-bordered" runat="server" AutoGenerateColumns="False"
                        Width="100%" OnRowDeleting="gvItems_RowDeleting">
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

                            <asp:BoundField DataField="Remarks" HeaderText="Description">
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
                        <EmptyDataTemplate>
                            <div style="text-align: center">
                                <span class="auto-style1" style="color: #CC0000">No Records Added</span>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>


                <div class="text-center">
                    <asp:Button ID="btnSave" runat="server" width="100px"  CssClass="bg-warning" Text="Save" OnClick="btnSave_Click" />
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
    </form>
</body>
</html>
