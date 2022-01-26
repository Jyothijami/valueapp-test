<%@ Page Title="" Language="C#" MasterPageFile="~/ModalPop.master" AutoEventWireup="true" CodeFile="Quot_Preview.aspx.cs" Inherits="Modules_SM_Quot_Preview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript">
       

function summarycalc() {

    var stotal, spdiscount, ssubtotal, svat, scst, sgrandvat, sgrandcst;
    stotal = document.getElementById('<%=txttotal.ClientID %>').value;
    spdiscount = document.getElementById('<%=txtspldiscount.ClientID %>').value;
    ssubtotal = document.getElementById('<%=txtsubtotal.ClientID %>').value;
    svat = document.getElementById('<%=txtsummaryvat.ClientID %>').value;



    if (spdiscount != "" && spdiscount != "0") {
        document.getElementById('<%=txtsubtotal.ClientID %>').value = parseFloat(stotal) - parseFloat((((stotal) * spdiscount) / 100));
        ssubtotal = document.getElementById('<%=txtsubtotal.ClientID %>').value;
        document.getElementById('<%=txtsummaryvat.ClientID %>').value = parseFloat((((ssubtotal) * 18) / 100));
        svat = document.getElementById('<%=txtsummaryvat.ClientID %>').value;
        document.getElementById('<%=txtsummaryGtoalvat.ClientID %>').value = parseFloat(ssubtotal) + parseFloat(svat);

    }

}
        //function CloseWindow() {
        //    window.close();
        //}

    </script>

    <script language="javascript" type="text/javascript">
        function check(e) {
            var keynum
            var keychar
            var numcheck
            // For Internet Explorer
            if (window.event) {
                keynum = e.keyCode;
            }
                // For Netscape/Firefox/Opera
            else if (e.which) {
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);
            //List of special characters you want to restrict
            if (keychar == "'" || keychar == "`" || keychar == "&" || keychar == "¬" || keychar == '"') {
                return false;
            } else {
                return true;
            }
        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class ="page-header">
        <div class ="page-title">
            <h3>Quotation Details</h3>
        </div>
    </div>

    <div class="form-horizontal ">
        <div class ="panel panel-default ">
            <div class ="panel-body">

                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h6 class="panel-title">Quotation Details</h6>
                    </div>

                    <div class="panel-body">
                        <div class="form-group">
                            <label class="col-sm-2 control-label text-right">Quotation No :</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtQuotationNo" runat="server" CssClass="form-control " ReadOnly="True"></asp:TextBox>
                            </div>
                            <label class="col-sm-2 control-label text-right">Quotation Date :</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtQuotationDate" runat="server" type="Datepic" CssClass="form-control" EnableTheming="False" Enabled="false"></asp:TextBox>&nbsp;

                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-2 control-label text-right">
                                <%--<asp:Button ID="btnAddnew" CssClass="btn btn-warning" runat="server" Text="New" OnClick="btnAddnew_Click" />--%>
                            Customer :<span class="mandatory" style="font-size: 17px">*</span>
                            </label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlCustomer" Enabled="false" Width="100%" TabIndex="2" CssClass="form-control " runat="server"></asp:DropDownList>
                            </div>
                            <label class="col-sm-2 control-label text-right">Unit Name :<span class="mandatory" style="font-size: 17px">*</span></label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlUnitName" Enabled="false" Width="100%" TabIndex="2" CssClass="form-control " runat="server"></asp:DropDownList>
                            </div>
                        </div>



                    </div>
                </div>

                <div class="panel panel-default">

                    <div class="panel-heading">
                        <div>
                            <h5 class="panel-title">Address & Contact</h5>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <label class="col-sm-2 control-label text-right">Address </label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtUnitAddress" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>

                            </div>
                            <label class="col-sm-2 control-label text-right">Contact No. </label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                </div>

                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div>
                            <h5 class="panel-title">Quotation Product Details</h5>
                        </div>
                    </div>
                    <div class="panel-body">
                        <asp:GridView ID="gvQuotationItems" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"  ShowFooter="True" OnRowDeleting="gvQuotationItems_RowDeleting" OnRowDataBound="gvQuotationItems_RowDataBound" EmptyDataText="No Data FOund">
                            <FooterStyle BackColor="SkyBlue" />
                           
                             <Columns>
                                <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                                <asp:BoundField DataField="ItemName" HeaderText="Item Name">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="UOM" HeaderText="UOM">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDetQty" runat="server" OnTextChanged="txtDetQty_TextChanged" Text='<%# Bind("Quantity") %>' AutoPostBack="true"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="Currency" HeaderText="Currency"></asp:BoundField>
                                <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDetRate" runat="server" Text='<%# Bind("Rate") %>' OnTextChanged="txtDetRate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="Rate" HeaderText="Rate">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                </asp:BoundField>--%>
                                <asp:BoundField HeaderText="Amount">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Discount" NullDisplayText="-" HeaderText="Disc %"></asp:BoundField>
                                <asp:BoundField DataField="SpPrice" NullDisplayText="-" DataFormatString="{0:0.00}" HeaderText="Special Price">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="GSTperc" HeaderText="GST(%)">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="GSTRate" HeaderText="GST Rate">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Room" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDetRoom" runat="server" Width="100%" Text='<%# Bind("Room") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="Room" HeaderText="Room"></asp:BoundField>--%>
                                <asp:BoundField DataField="CurrencyId" HeaderText="Currency Id"></asp:BoundField>
                                <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                                <asp:BoundField DataField="ColorId" HeaderText="ColorId"></asp:BoundField>
                                <asp:BoundField DataField="OptId" HeaderText="Optional Id"></asp:BoundField>
                                <asp:BoundField DataField="ItemType" HeaderText="Item Type"></asp:BoundField>
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
                                <asp:TemplateField HeaderText="Floor" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDetFloor" runat="server" Text='<%# Bind("Floor") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="Floor" HeaderText="Floor"></asp:BoundField>--%>
                                <asp:TemplateField HeaderText="Discount%" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDisc" OnTextChanged="txtDisc_TextChanged" runat="server" AutoPostBack="true"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="SrlOrder" HeaderText="SrlOrder"></asp:BoundField>--%>
                                <asp:TemplateField HeaderText="Srl Order" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSrlOrder" runat="server" Text='<%# Bind("SrlOrder") %>'>></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Image">
                                    <ItemTemplate>
                                        <asp:Image ID="Image" runat="server" EnableTheming="False" Height="132px" ImageUrl='<%# Eval("Item_Image","~/Content/ItemImage/{0}") %>'
                                            Width="141px" /><br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No Data Exist!
                    
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>

                <div class="panel panel-default">

                    <div class="panel-heading">
                        <div>
                            <h5 class="panel-title">Summary</h5>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <label class="col-sm-2 control-label text-right">Total </label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txttotal" runat="server" CssClass="form-control "></asp:TextBox>

                            </div>
                            <label class="col-sm-2 control-label text-right">Special Discount </label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtspldiscount" runat="server" CssClass="form-control " Text="0"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-2 control-label text-right">Sub Total </label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtsubtotal" CssClass="form-control " runat="server"></asp:TextBox>

                            </div>
                            <label class="col-sm-2 control-label text-right">GST 18%  </label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtsummaryvat" CssClass="form-control " runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-2 control-label text-right">GrandToal(GST) </label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtsummaryGtoalvat" CssClass="form-control " runat="server"></asp:TextBox>
                            </div>

                        </div>
                    </div>

                </div>

                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <div>
                            <h5 class="panel-title">Incharge Details of Quotation</h5>
                        </div>
                    </div>
                    <div class ="panel-body">
                        <div class="form-group">
                            <label class="col-sm-2 control-label text-right">Responsible Person </label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlResponsiblePerson" CssClass ="form-control " Enabled ="false"  runat="server" AutoPostBack="True" >
                        </asp:DropDownList>
                            </div>
                            <label class="col-sm-2 control-label text-right">Sales Person </label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlSalesPerson" CssClass ="form-control " Enabled ="false"  runat="server"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Button ID="btnApprove" runat="server" OnClick ="Button1_Click" Text="Approve" Width="100px" />
            </div>

        </div>
    </div>

    

  



</asp:Content>

