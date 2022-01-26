<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/SalesMP1.master" AutoEventWireup="true" CodeFile="SalesOrder_Search - Copy.aspx.cs" Inherits="Modules_Reports_SalesOrder_Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="http://code.jquery.com/ui/1.10.4/themes/ui-lightness/jquery-ui.css" rel="stylesheet">
      <script src="http://code.jquery.com/jquery-1.10.2.js"></script>
      <script src="http://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>




      <link rel="stylesheet" href="https://cdn.datatables.net/1.10.12/css/jquery.dataTables.min.css" type="text/css" />
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.12/js/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />



      <script type="text/javascript">
          $(document).ready(function () {
              //fnPageLoad();
          });
          function fnPageLoad() {
              $('#<%=gvSalesOrder.ClientID%>').prepend($("<thead></thead>").append($('#<%=gvSalesOrder.ClientID%>').find("tr:first"))).DataTable({

                bSort: true,
                dom: '<"html5buttons"B>lTfgitp',
                lengthChange: false,
                pageLength: 50,

                bStateSave: true,
                order: [[0, 'desc']],

                //scrollX: true,
                // fixedHeader : true,


                fixedHeader: {
                    header: true,
                    footer: true
                }

            });
        }
    </script>










    <style type="text/css">
        .auto-style1 {
            height: 25px;
        }
    </style>
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
</script>
      <script>

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
      </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <div id="body" style="width:100%">
          <table class="pagehead">
        <tr>
            <td style="text-align: left">Sales Order Details:</td>
            <td style="text-align: right">
                 <asp:DropDownList ID="ddlNoOfRecords" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged">
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
                    <asp:RadioButtonList ID="rblBalQty" runat="server" RepeatDirection="Horizontal" >
                        <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                        <asp:ListItem Value ="3">Balance Qty = 0</asp:ListItem>
                        <asp:ListItem Value="1">Balance Qty != 0</asp:ListItem>
                        <asp:ListItem Value ="2" >Blocked Qty != 0</asp:ListItem>
                        <%--<asp:ListItem Value ="2" >Shipment Track Details</asp:ListItem>--%>
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
                    Customer Name : <asp:TextBox ID="txtCustName" runat="server"></asp:TextBox>
                    </td>
                <td></td>
                 <td style="width:5%">
                     Executive Name:
                </td>
                <td style="text-align:left" >
                    <asp:DropDownList ID="ddlResponsiblePerson" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    
                </td>
            </tr>
            <tr><td></td>
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
                Total No  of Blocked Quantity :
            </td>
            <td>
                <asp:Label ID="lblBlockedQty" Font-Bold="true" runat="server"></asp:Label>
            </td>
             
            <td>
                Total No  of Blocked Amount :
            </td>
            <td>
                <asp:Label ID="lblBlockedAmtTotal" Font-Bold="true" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <div id="grid" style="width:100%">



        <asp:GridView ID="gvSalesOrder" Width="100%" runat="server" SelectedRowStyle-BackColor="#c0c0c0" EmptyDataText="No Records To Display"  OnRowDataBound="gvSalesOrder_RowDataBound" AutoGenerateColumns ="false" >
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
                <asp:BoundField DataField ="Model No" HeaderText ="Model No" />
                <asp:BoundField DataField ="Series" HeaderText ="Series" />
                <asp:BoundField DataField ="Item Type" HeaderText ="Item Type" />
                <asp:BoundField DataField ="PO Quantity" HeaderText ="PO Quantity" />
                <asp:BoundField DataField ="Balance Quantity" HeaderText ="Balance Quantity" />
                <asp:BoundField DataField ="Blocked Qty" HeaderText ="Blocked Qty" />
                <asp:BoundField  HeaderText ="Price" />
                <asp:BoundField HeaderText ="Blocked Total" />
                <asp:BoundField DataField ="Customer Name" HeaderText ="Customer Name" />
                <asp:BoundField DataField ="Brand" HeaderText ="Brand" />
                <asp:BoundField DataField ="Customer PO No" HeaderText ="Customer PO No" />
                <asp:BoundField DataField ="Executive Name" HeaderText ="Executive Name" />
                <asp:BoundField DataField ="SO_DET_ID" HeaderText ="SO_DET_ID" />
                <asp:BoundField DataField ="Indnet Qty" HeaderText ="Indnet Qty" />
                <asp:BoundField DataField ="Ordered Qty" HeaderText ="Ordered Qty" />
                <asp:BoundField DataField ="Invoiced Qty" HeaderText ="Invoiced Qty" />
                <asp:BoundField DataField ="Price" HeaderText ="Price" />
                <asp:BoundField DataField ="Status" HeaderText ="Status" />
                <asp:BoundField DataField ="AppDeliveryDt" HeaderText ="appropriate delivery dt" />
                </Columns>
        </asp:GridView>
        <asp:GridView ID="gvShipment" Width ="100%" runat ="server" SelectedRowStyle-BackColor="#c0c0c0" EmptyDataText="No Records To Display" AllowPaging="True" PageSize="8" OnPageIndexChanging="gvShipment_PageIndexChanging" Visible ="false"  >

        </asp:GridView>
    </div>
    <div id="dialog" class="dialog1">
        <table>
            <tr>
                <td><asp:Label ID="lblRemarks" Text="Remarks:" runat="server"></asp:Label></td>
                <td><asp:TextBox ID="txtSaledId" TextMode ="MultiLine" CausesValidation ="false"  runat="server"></asp:TextBox>
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


 
