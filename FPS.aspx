<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FPS.aspx.cs" Inherits="FPS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    

    <title></title>
    <link href="../../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../select/select2.css" rel="stylesheet" />
    <script src="../../select/select2.js"></script>

    <script lang="javascript" type="text/javascript">
        function OpenPopupCenter(pageURL, title, w, h) {
            var left = (screen.width - w) / 2;
            var top = (screen.height - h) / 4;  // for 25% - devide by 4  |  for 33% - devide by 3
            var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
        }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    
        <script type="text/javascript">
            function amtcalc() {

                var req_qty, rate, disc, SPprice;
                var gst_Amt, gst_Tax;


                rate = document.getElementById('<%=txtPOAmt1.ClientID %>').value;
            disc = document.getElementById('<%=txtPerc.ClientID %>').value;




            if (rate == "" || rate == "0") {
                document.getElementById('<%=txtArAmount.ClientID %>').value = "0";
            }


            if (disc != "" && disc != "0") {
                document.getElementById('<%=txtArAmount.ClientID %>').value = parseFloat((rate)) - parseFloat((((rate) * disc) / 100));
            }

            SPprice = document.getElementById('<%=txtArAmount.ClientID %>').value;


        }



        function amtcalcDisc() {

            var req_qty, rate, spprice;
            var gst_Amt, gst_Tax;


            rate = document.getElementById('<%=txtPOAmt1 .ClientID %>').value;
            spprice = document.getElementById('<%=txtArAmount.ClientID %>').value;

            if (rate == "" || rate == "0") {
                document.getElementById('<%=txtArAmount.ClientID %>').value = "0";
            }
            else if (rate > 0) {
                document.getElementById('<%=txtPerc.ClientID %>').value = (((rate) - spprice) * 100) / (rate);
    }

    if (disc != "" && disc != "0") {
        document.getElementById('<%=txtArAmount.ClientID %>').value = parseFloat((rate)) - parseFloat((((rate) * disc) / 100));
    }

    SPprice = document.getElementById('<%=txtArAmount.ClientID %>').value;

        }












    </script>

        <div class="form-horizontal">
            <div class="panel panel-default">
                        <div class="panel-heading">
                    <h6 class="panel-title">Architect Details</h6>
                </div>
                        <div class="panel-body">
                            
                            <div class="form-group">
                                 <label class="col-sm-2 control-label text-right">Search By Customer Name</label>
                                 <div class="col-sm-4">
                                     <asp:TextBox ID="txtSearchModel" runat="server">
                                    </asp:TextBox><asp:Button ID="btnSearchModelNo" runat="server" BorderStyle="None"
                                        CausesValidation="False" CssClass="gobutton" EnableTheming="False" OnClick="btnSearchModelNo_Click"
                                        Text="Go" />
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                        SelectCommand="SP_Customer_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="txtSearchModel" Name="SearchValue" PropertyName="Text"
                                                Type="String" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                 </div>
                                
                             </div>
                            <div class="form-group">
                                 <label class="col-sm-2 control-label text-right">Customer Name</label>
                                 <div class="col-sm-4">
                                     <asp:DropDownList ID="ddlCustomerName" CssClass ="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomerName_SelectedIndexChanged"></asp:DropDownList>
                                     <asp:TextBox ID="txtCustomerName" runat="server" ReadOnly="True" Visible="False"></asp:TextBox>
                                 </div>
                                 <label class="col-sm-2 control-label text-right">Sales Order No</label>
                                 <div class="col-sm-4">
                                    <asp:DropDownList CssClass ="form-control" ID="ddlSalesOrderNo" runat="server" AutoPostBack="True" >
                                    </asp:DropDownList>
                                 </div>
                             </div>
                            <div class="form-group">
                                 <label class="col-sm-2 control-label text-right">Architect Name</label>
                                 <div class="col-sm-4">
                                     <asp:DropDownList ID="ddlArchitect" CssClass ="form-control" runat="server" 
                                     AutoPostBack="True" OnSelectedIndexChanged ="ddlArchitect_SelectedIndexChanged"  ></asp:DropDownList>
                                     <asp:TextBox ID="txtArchitect" CssClass ="form-control" runat ="server" ></asp:TextBox>
                                 </div>
                                 <label class="col-sm-2 control-label text-right">Dispatch<br />Gross</label>
                                 <div class="col-sm-4">
                                     <asp:DropDownList ID ="ddlDispatch" CssClass ="form-control " runat ="server" ></asp:DropDownList>
                                    <asp:TextBox ID="txtPOAmt1" CssClass ="form-control" runat="server" ></asp:TextBox>
                                 </div>
                             </div>
                            <div class="form-group">
                                 <label class="col-sm-2 control-label text-right">NET</label>
                                 <div class="col-sm-4">
                                     <asp:TextBox ID="txtPOAmt" runat ="server" CssClass ="form-control" ></asp:TextBox>
                                 </div>
                                 <label class="col-sm-2 control-label text-right">Percentage</label>
                                 <div class="col-sm-4">
                                    <asp:TextBox ID="txtPerc" CssClass ="form-control" runat="server"  >0</asp:TextBox>
                                 </div>
                             </div>
                            <div class="form-group">
                                 <label class="col-sm-2 control-label text-right">Amount</label>
                                 <div class="col-sm-4">
                                     <asp:TextBox ID="txtArAmount" runat ="server" CssClass ="form-control" ></asp:TextBox>
                                     
                                 </div>
                                 <label class="col-sm-2 control-label text-right" >Status</label>
                                 <div class="col-sm-4">
                                     <asp:DropDownList ID="ddlStatus" runat ="server" CssClass ="form-control" >
                                         <asp:ListItem >Open</asp:ListItem>

                                         <asp:ListItem >Paid</asp:ListItem>
                                         <asp:ListItem >Partially Paid</asp:ListItem>

                                     </asp:DropDownList>
                                     
                                 </div>
                             </div>
                            <div class="form-group">
                                 <label class="col-sm-2 control-label text-right">Remarks</label>
                                 <div class="col-sm-4">
                                     <asp:TextBox ID="txtArRemarks" runat ="server" CssClass ="form-control" TextMode ="MultiLine"  ></asp:TextBox>
                                 </div>
                                 <label class="col-sm-2 control-label text-right">Sales Executive</label>
                                 <div class="col-sm-4">
                                     <asp:DropDownList ID="ddlResponsiblePerson" CssClass ="form-control " runat="server"  ></asp:DropDownList>
                                 </div>
                             </div>
                             <div class="form-group">
                                 <div class="col-sm-8" style ="align-items :center ">
                                    <asp:Button ID ="btnSave" CssClass ="form-control " runat ="server" Text ="Save" OnClick ="btnSave_Click" />
                                 
                                 </div>
                                 
                             </div>
                            
                        </div>
                <div class ="panel-body">
                    
        <asp:GridView ID="gvFPS" runat ="server" AutoGenerateColumns="False" AllowSorting="True" Width="100%">
                                    <Columns >
                                        <asp:BoundField DataField="ARCHITECT_NAME" HeaderText="ARCHITECT NAME"></asp:BoundField>
                                    <asp:BoundField DataField="Name" HeaderText="Architect Name"></asp:BoundField>
                                    
                                        <asp:BoundField DataField="SO_NO" HeaderText="PO No"></asp:BoundField>
                                    <asp:BoundField DataField="CUST_NAME" HeaderText="CUST NAME"></asp:BoundField>
                                    <asp:BoundField DataField="PO_Amt1" HeaderText="Net" ></asp:BoundField>
                                    <asp:BoundField DataField="po_Amt" HeaderText="Gross"></asp:BoundField>
                                    <asp:BoundField DataField="Percntage" HeaderText="Percentage"  ></asp:BoundField>
                                    <asp:BoundField DataField="TotalAmt" HeaderText="Total Amt" ></asp:BoundField>
                                    <asp:BoundField DataField="Status" HeaderText="Status"></asp:BoundField>
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
                                    <asp:BoundField DataField="fps_dt" HeaderText="Date Added"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <div class="row">
                        <div class="col-md-12 ">
                            <span class="text-center">
                        <a runat="server" class="btn btn-icon btn-primary " href='<%# "~/FPSEdit.aspx?Cid=" + Eval("FPS_ID") %>' onclick="OpenPopupCenter(this.href, 'newwindow', 800, 500);return false; ">
                           <%-- <i class="icsw16-speech-bubbles"></i><span class="badge badge-important">
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("Cou") %>'></asp:Label></strong></span>--%>
                            <span style="color:white"><strong> Edit</strong> </span>

                        </a>

                    </span>
                            </div>
                    </div>

                </ItemTemplate>
            </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
        <asp:SqlDataSource ID="sdsFPS" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="select FPS_ID,ARCHITECT_NAME,Name ,SO_NO,CUST_NAME ,po_Amt,PO_Amt1,Percntage,TotalAmt,Status,Remarks,fps_dt from fps_tbl left outer join YANTRA_LKUP_ARCHITECT on fps_tbl.Architect_id=YANTRA_LKUP_ARCHITECT .ARCHITECT_ID inner join YANTRA_SO_MAST on fps_tbl.so_Id =YANTRA_SO_MAST .SO_ID inner join YANTRA_CUSTOMER_MAST on YANTRA_SO_MAST .SO_CUST_ID =YANTRA_CUSTOMER_MAST .CUST_ID order by fps_id desc " SelectCommandType="Text">
            
        </asp:SqlDataSource>
    
                </div>
                    </div>
        </div>
    </form>
</body>
</html>
