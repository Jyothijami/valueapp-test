<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/SalesMP1.master" AutoEventWireup="true" CodeFile="SalesOrder_Search.aspx.cs" Inherits="Modules_Reports_SalesOrder_Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="http://code.jquery.com/ui/1.10.4/themes/ui-lightness/jquery-ui.css" rel="stylesheet">
      <script src="http://code.jquery.com/jquery-1.10.2.js"></script>
      <script src="http://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <style type="text/css">
        .auto-style1 {
            height: 25px;
        }
    </style>


    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>



    <script type="text/javascript">
        // Let's use a lowercase function name to keep with JavaScript conventions
        function selectAll(invoker) {
            // Since ASP.NET checkboxes are really HTML input elements
            //  let's get all the inputs 
            var inputElements = document.getElementsByTagName('input');

            for (var i = 0 ; i < inputElements.length ; i++) {
                var myElement = inputElements[i];

                // Filter through the input types looking for checkboxes
                if (myElement.type === "checkbox") {

                    // Use the invoker (our calling element) as the reference 
                    //  for our checkbox status
                    myElement.checked = invoker.checked;
                }
            }
        }
    </script>
    <script type="text/javascript">
        // set rates codes for Booking Method = GroupRes Confirmation
        <%-- $("#<%= hndtxt_email.ClientID %>").val($("#<%= txtSaledId.ClientID %>").val());
        alert($("#<%= hndtxt_email.ClientID %>").val());--%>


        $(document).ready(function () {
            $('.js-example-basic-single').select2();
        });
</script>
      <%--<script>

          $(function OpenNews() {

              var dialog1 = $(".dialog1");
              var checkbox1 = $(".checkbox1");
              var txtbox1 = $(".txtSaledId");

              dialog1.dialog({
                  appendTo: "#dialogAfterMe",
                  autoOpen: false,
                  modal: true,
                  buttons: {
                      Post: function () { $("[id*=btnpost]").click(); }
                  },
                  title: "Releasing blocked Qty with comments",
                  close: function () { checkbox1.prop('checked', false); },
                  open: function() {
                      $(".hndtxt_email").val((".txtSaledId").val());
                  }
                  
              });
             
              checkbox1.click(function () {
                  if (checkbox1.click("checked")) {
                      dialog1.dialog("open");
                  }
                  else {
                      dialog1.dialog("close")
                  }
              });

          });
          function createShippingAddress() {
              $("#dialog1").dialog("widget").find(".ui-dialog-titlebar-close").hide();
              $("#dialog1").dialog("open");
              return false;
          }
      </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <div id="body" style="width:100%">
          <table class="pagehead">
        <tr>
            <td style="text-align: left">Sales Order Details:</td>
            <td style="text-align: right">
                 <asp:DropDownList ID="ddlNoOfRecords" runat="server"  AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged">
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>75</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>                   
                </asp:DropDownList>
            </td>
        </tr>
    </table>
        <table style="width:100%">
            <tr>
                        <td style="text-align: left" colspan="2">
                                    Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
                                </td>
                    </tr>
            <tr>
                <td class="auto-style1">
                    &nbsp;
                </td>
                <td colspan="3" style="text-align:center" class="auto-style1">
                    <asp:RadioButtonList ID="rblBalQty" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged ="rblBalQty_SelectedIndexChanged" AutoPostBack ="true"   >
                        <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                        <asp:ListItem Value ="3">Balance Qty = 0</asp:ListItem>
                        <asp:ListItem Value="1">Balance Qty != 0</asp:ListItem>
                        <asp:ListItem Value ="2" >Blocked Qty != 0</asp:ListItem>
                        <%--<asp:ListItem Value ="2" >Shipment Track Details</asp:ListItem>--%>
                       <%-- <asp:ListItem Value="4">Running</asp:ListItem>
                        <asp:ListItem Value="5">Closed</asp:ListItem>--%>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align:right">
                    Brand : <asp:DropDownList ID="ddlBrand" runat="server"></asp:DropDownList>
                </td>
                <td style="width:5%">

                </td>
                <td style="width:10%">
                Model No :

                </td>
                <td style="text-align:left">
                     <asp:TextBox ID="txtModelNo" runat="server"></asp:TextBox>
                </td>
            </tr>
           
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align:right">
                    From Date : <asp:TextBox ID="txtFromDate" type="datepic" runat="server"></asp:TextBox>
                </td>
                <td style="width:5%">

                </td>
                <td style="width:10%">To Date : </td>
                <td style="text-align:left">
                    <asp:TextBox ID="txtToDate" type="datepic" runat="server"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td>&nbsp;</td>
            </tr>
             <tr>
                <td style="text-align:right">
                    Purchase Order No : <asp:TextBox ID="txtPurchaseOrderNo" runat="server"></asp:TextBox>
                    </td>
                 <td style="width:5%">

                </td>
                 <td style="width:10%">Customer PO No :</td>
                <td style="text-align:left">
                    <asp:TextBox ID="txtCustomerPO" runat="server"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align:right">
                    Customer Name : <asp:TextBox ID="txtCustName" runat="server"  ></asp:TextBox>
                <asp:DropDownList ID="ddlCust" runat ="server" CssClass="js-example-basic-single"  AutoPostBack ="true" Visible ="false"    ></asp:DropDownList>
                    <asp:DropDownList ID="ddlBlockedCust" runat ="server" AutoPostBack ="true"  Visible ="false"  ></asp:DropDownList>
                    </td>
                <td></td>
                 <td style="width:5%">
                     Executive Name:
                </td>
                <td style="text-align:left" >
                    <asp:DropDownList ID="ddlResponsiblePerson" CssClass="js-example-basic-single" runat="server" OnSelectedIndexChanged ="ddlResponsiblePerson_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList> 
                    <asp:DropDownList ID="ddlInactiveResponsible" CssClass="js-example-basic-single" runat="server" OnSelectedIndexChanged ="ddlInactiveResponsible_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    
                </td>
            </tr>
            <tr><td style="text-align:right"><asp:Label ID="lblstatus" runat ="server" Text="Status" Visible ="false"  ></asp:Label><asp:DropDownList ID="ddlstatus" Visible ="false" runat="server">
                <asp:ListItem Value="0">--</asp:ListItem>
              <%--  <asp:ListItem>New</asp:ListItem>
                <asp:ListItem Value="Open">Running</asp:ListItem>--%>
                <asp:ListItem>ManuallyClosed</asp:ListItem>
                <asp:ListItem>Closed</asp:ListItem>
                </asp:DropDownList>
                </td>
                <td></td>
                 <td style="width:5%">Company Name :</td>
                <td style="text-align:left" >
                   <asp:DropDownList ID="ddlCompany" runat="server" DataSourceID="SqlDataSource1" DataTextField="COMP_NAME" DataValueField="CP_ID" AppendDataBoundItems="True">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT a.CP_ID, a.CP_FULL_NAME + ',' + b.locname AS COMP_NAME FROM YANTRA_COMP_PROFILE AS a INNER JOIN location_tbl AS b ON a.locid = b.locid"></asp:SqlDataSource>
                    
                    
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td style="text-align:left" colspan="2">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" BackColor="#9966FF" EnableTheming="True" ForeColor="White" Width="12%" />
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    <asp:Button ID="btnExprot" runat="server" Text="Export To Excel" OnClick="btnExprot_Click" BackColor="#9966FF" EnableTheming="True" ForeColor="White" Width="25%" />
               &nbsp;
                    &nbsp;
                    &nbsp;
                     <asp:Button ID="btnReserve2" OnClick="btnReserve2_Click" runat="server" Text="Reserve " BackColor="#9966FF" EnableTheming="True" ForeColor="White" Width="25%"  />
                </td>
            </tr>
        </table>
    </div>
    <table>
        <tr>
            <td>
                Total No Of Orderd Quantity : 
            </td>
            <td>
                <asp:Label ID="lblTtlOrderdQty" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;
            </td>
            <td>
                Total No  of Balance Quantity :
            </td>
            <td>
                <asp:Label ID="lblTtlBalnceQty" Font-Bold="true" runat="server"></asp:Label>
            </td>
            <td>
                
            </td>
            <td>
                <asp:Label ID="lblBlockedQty" Visible ="false"  Font-Bold="true" runat="server"></asp:Label>
            </td>

              
            <td>
              
            </td>
            <td>
                <asp:Label ID="lblBlockedAmtTotal" Visible ="false" Font-Bold="true" runat="server"></asp:Label>
            </td>

        </tr>
    </table>
    <br />
    <div id="grid" style="width:100%">
        <asp:GridView ID="gvSalesOrder" Width="100%" runat="server" CssClass="table table-responsive" AllowPaging ="true" OnPageIndexChanging ="gvSalesOrder_PageIndexChanging"  SelectedRowStyle-BackColor="#c0c0c0"  EmptyDataText="No Records To Display" OnRowDataBound="gvSalesOrder_RowDataBound" AutoGenerateColumns ="False" >
            <HeaderStyle HorizontalAlign="Center" />
<SelectedRowStyle BackColor="Silver"></SelectedRowStyle>
            <Columns>
              <asp:TemplateField>
                  <HeaderTemplate>
                      <asp:CheckBox ID="checkbox" class="checkbox1" OnClick="selectAll(this)" runat="server" />
                  </HeaderTemplate>
                      <ItemTemplate>
                           <asp:CheckBox ID="chk" class="checkbox1" runat="server"></asp:CheckBox>
                       </ItemTemplate>
                     </asp:TemplateField>
                <asp:BoundField DataField ="So No" HeaderText ="So No" />
                <asp:BoundField DataField ="SO Date" HeaderText ="SO Date" />
              <%--  <asp:BoundField DataField ="Customer Name" HeaderText ="Customer" />--%>

                <asp:TemplateField HeaderText="Customer Details" HeaderStyle-CssClass="text-center" >
                    <ItemTemplate>
                       





                        <div class="table-responsive">
                            <table class="table" style="color:black">
                                <tbody>
                                    <tr>
                                        <td  >CustomerName :</td>
                                         <td style="color:black;"><%#Eval("Customer Name")%></td>
                                        </tr>


                                    <tr>
                                        <td>PO No :</td>
                                        <td style="color:black"><%#Eval("So No")%></td>
                                         </tr>
                                     <tr>
                                       
                                       <td>Cust Po No:</td>
                                        
                                        <td style="color:black"><%#Eval("Customer PO No")%></td>
                                    </tr>
                                      <tr>
                                        <td>PO Date :</td>
                                        <td style="color:black"><%#Eval("SO Date")%></td>
                                          </tr>
                                   
                                    <tr>
                                       
                                       <td>Executive Name:</td>
                                        
                                        <td style="color:black"><%#Eval("Executive Name")%></td>
                                    </tr>

                                </tbody>
                            </table>
                        </div>







                    </ItemTemplate>
                    <HeaderStyle CssClass="text-center"></HeaderStyle>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Item Details" HeaderStyle-CssClass="text-center">
                    <ItemTemplate>

                        <div class="table-responsive">
                            <table class="table" style="color:black">
                                <tbody>
                                    <tr>
                                        <td  >ModelNo :</td>
                                         <td style="color:black"><%#Eval("Model No")%></td>
                                        </tr>


                                    <tr>
                                        <td>Series:</td>
                                        <td style="color:black"><%#Eval("Series")%></td>
                                         </tr>

                                      <tr>
                                        <td>Brand:</td>
                                        <td style="color:black"><%#Eval("Brand")%></td>
                                          </tr>
                                   
                                    <tr>
                                       
                                       <td>Item Type:</td>
                                        
                                        <td style="color:black"><%#Eval("Item Type")%></td>
                                    </tr>

                                </tbody>
                            </table>
                        </div>
                    </ItemTemplate>
                    <HeaderStyle CssClass="text-center"></HeaderStyle>
                </asp:TemplateField>






                <%--<asp:BoundField DataField ="Model No" HeaderText ="Model No" />--%>
                <%--<asp:BoundField DataField ="Item Type" HeaderText ="Item Type" />--%>
              
                  <asp:TemplateField HeaderText="Order Status" HeaderStyle-CssClass="text-center">
                    <ItemTemplate>


                        <div class="table-responsive">
                            <table class="table">
                                <thead>
                                    <tr>
                                        <th>PO Qty</th>
                                        <th>Bal Qty</th>
                                        <%--<th>Bloc Qty</th>--%>

                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td style="color:black;"><%#Eval("PO Quantity")%></td>
                                        <td style="color:black;"><%#Eval("Balance Quantity")%></td>
                                        <%--<td style="color:black;"><%#Eval("Blocked Qty")%></td>--%>

                                    </tr>


                                </tbody>
                            </table>
                        </div>



                      
                    </ItemTemplate>
                    <HeaderStyle CssClass="text-center"></HeaderStyle>
                </asp:TemplateField>

               
                <asp:BoundField DataField ="PO Quantity" HeaderText ="PO Qty" />
                <asp:BoundField DataField ="Balance Quantity" HeaderText ="Bal Qty" />
                <asp:BoundField  HeaderText ="Bloc Qty" />
                <asp:BoundField  HeaderText ="Price" />
                <%--<asp:BoundField HeaderText ="Blocked Total" />--%>
                <asp:TemplateField>
                    <HeaderStyle HorizontalAlign ="Center"  />
                         <ItemTemplate>
                             <asp:GridView ID="gvBlock" CssClass="subgridviews" OnRowDataBound="gvBlock_RowDataBound"   width="100%" runat ="server" AutoGenerateColumns ="false">
                                 <Columns >
                                     <asp:BoundField DataField ="Blocked_Qty" HeaderText ="Blocked qty" />
                                     <asp:BoundField DataField ="so_det_Price" HeaderText ="Price" />
                                     <asp:BoundField DataField ="SO_DET_QTY" HeaderText ="Qty" />
                                     <asp:BoundField  HeaderText ="Price" />

                                     <asp:BoundField  HeaderText ="Blocked Amount" />
                                     
                                 </Columns>
                             </asp:GridView>
                         </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField ="Series" HeaderText ="Series" />
                <asp:BoundField DataField ="Brand" HeaderText ="Brand" />
                <asp:BoundField DataField ="Customer PO No" HeaderText ="Customer PO No" />
                <asp:BoundField DataField ="Executive Name" HeaderText ="Executive Name" />
                <asp:BoundField DataField ="SO_DET_ID" HeaderText ="SO_DET_ID" />
                <asp:BoundField  HeaderText ="Indnet Qty" />
                <asp:BoundField HeaderText ="Ordered Qty" />
                <asp:BoundField  HeaderText ="Invoiced Qty" />
                <asp:BoundField DataField ="Price" HeaderText ="Price" />
                <asp:BoundField  HeaderText ="Status" />
                <%--<asp:BoundField  HeaderText ="appropriate delivery dt" />--%>

                <asp:TemplateField>
                    <HeaderStyle HorizontalAlign ="Center"  />
                         <ItemTemplate>
                             <asp:GridView ID="gvIndent" CssClass="subgridviews" OnRowDataBound ="gvIndent_RowDataBound"    width="100%" runat ="server" AutoGenerateColumns ="false">
                                 <Columns >
                                     <asp:BoundField DataField ="IND_DET_QTY" HeaderText ="Indent qty" />
                                     <asp:BoundField DataField ="IND_DET_ID" HeaderText ="Indent ID" />
                                     
                                 </Columns>
                             </asp:GridView>
                         </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderStyle HorizontalAlign ="Center"  />
                         <ItemTemplate>
                             <asp:GridView ID="gvFPO" OnRowDataBound ="gvFPO_RowDataBound" CssClass="subgridviews"   width="100%" runat ="server" AutoGenerateColumns ="false">
                                 <Columns >
                                     <asp:BoundField DataField ="FPO_DET_QTY" HeaderText ="FPO qty" />
                                     <asp:BoundField DataField ="FPO_DET_ID" HeaderText ="FPO ID" />
                                     
                                 </Columns>
                             </asp:GridView>
                         </ItemTemplate>
                </asp:TemplateField>


               

                <asp:TemplateField>
                    <HeaderStyle HorizontalAlign ="Center"  />
                         <ItemTemplate>
                             <asp:GridView ID="gvDC" CssClass="subgridviews"   width="100%" runat ="server" AutoGenerateColumns ="false">
                                 <Columns >
                                     <asp:BoundField DataField ="PI_DET_QTY" HeaderText ="Invoiced qty" />
                                     <asp:BoundField DataField ="Status" HeaderText ="Status" />

                                     <asp:BoundField DataField ="AppDeliveryDt" HeaderText ="App Delivery Dt" />

                                 </Columns>
                             </asp:GridView>
                         </ItemTemplate>
                </asp:TemplateField>

                  <%--<asp:TemplateField HeaderText="Tracking" HeaderStyle-CssClass="text-center">
                    <ItemTemplate>

                        <div class="table-responsive">
                            <table class="table">
                                <tbody>
                                    <tr>
                                        <td>App Dt</td>
                                        <td style="color:black;"><%#Eval("AppDeliveryDt")%></td>
                                       
                                    </tr>
                                    <tr>
                                        <td>Status</td>
                                        <td style="color:black;"><%#Eval("Status")%></td>

                                    </tr>


                                </tbody>
                            </table>
                        </div>


                        
                    </ItemTemplate>
                    <HeaderStyle CssClass="text-center"></HeaderStyle>
                </asp:TemplateField>--%>


                 <asp:BoundField DataField ="balanceqty" HeaderText ="Status" >


                <ControlStyle Font-Bold="True" />
                </asp:BoundField>
                <asp:BoundField DataField="APPROVEDBY" HeaderText="Obsolete/Closed By">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>

              <asp:BoundField DataField ="SO_ACCEPTANCE_FLAG" HeaderText ="sTATUS" />
                <asp:BoundField   HeaderText ="FPO DET ID" />
                
                <asp:BoundField DataField ="locid" HeaderText ="locid" />
                <asp:BoundField DataField ="Item_Code" HeaderText ="Item_Code" />
                <asp:BoundField DataField ="COLOR_ID" HeaderText ="COLOR_ID" />
                <asp:BoundField DataField ="SO_Id" HeaderText ="SoId" />
                <asp:BoundField DataField ="SO_CUST_ID" HeaderText ="CustId" />
                <asp:BoundField DataField ="SO_DET_DELIVERY_DATE" HeaderText ="Deli dt"  />
                <asp:BoundField HeaderText ="WO_ID"  />

                <asp:TemplateField>
                    <HeaderStyle HorizontalAlign ="Center"  />
                         <ItemTemplate>
                             <asp:GridView ID="gvStock" CssClass="subgridviews"   width="100%" runat ="server" AutoGenerateColumns ="false">
                                 <Columns >
                                     <asp:TemplateField HeaderText="PO Qty" >
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQuantity" AutoCompleteType="None" Text='<%# Bind("POQty") %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                     <asp:BoundField DataField ="TOTAL_AVALIABLE_STOCK" HeaderText ="Free Stock" />

                                     <asp:BoundField DataField ="locname" HeaderText ="Location" />

                                     

                                 </Columns>
                             </asp:GridView>
                         </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderStyle HorizontalAlign ="Center"  />
                         <ItemTemplate>
                             <asp:GridView ID="gvAnnexure" CssClass="subgridviews"   width="100%" runat ="server" AutoGenerateColumns ="false">
                                 <Columns >
                                     <asp:TemplateField HeaderText="Annexure Qty" >
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAnnexureqty" AutoCompleteType="None" Text='<%# Bind("Annexure_Qty") %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                     <asp:BoundField DataField ="WO_Date" HeaderText ="Annexure Dt" />


                                     

                                 </Columns>
                             </asp:GridView>
                         </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField DataField ="Annexure_Qty" HeaderText ="Annexure Indent" />--%>
                </Columns>
        </asp:GridView>


        <asp:GridView ID="gvShipment" Width ="100%" runat ="server" SelectedRowStyle-BackColor="#c0c0c0" EmptyDataText="No Records To Display" AllowPaging="True" PageSize="8" OnPageIndexChanging="gvShipment_PageIndexChanging" Visible ="false"  >
           
        </asp:GridView>
    </div>
    <div id="dialog" class="dialog1">
        <table visible="false" runat ="server" >
            <tr>
                <td><asp:Label ID="lblRemarks" Text="Remarks:" runat="server"   ></asp:Label></td>
                <td><asp:TextBox ID="txtSaledId" TextMode ="MultiLine" CausesValidation ="false"    runat="server"></asp:TextBox>
                <asp:hiddenfield id="hndtxt_email" ClientIDMode ="Static"  runat="server" />
                
                </td>
                
            </tr>
           
        </table>
    </div>
    
    <table>
         <tr>
                <td colspan ="2"><asp:Button ID="btnpost" runat ="server" Text="Post" UseSubmitBehavior ="false"  OnClick ="btnpost_Click"  BackColor="#9966FF" EnableTheming="True" ForeColor="White" Width="1%"   /></td>
            </tr>
    </table>
    <div id="dialogAfterMe"></div>

    
    

</asp:Content>


 
