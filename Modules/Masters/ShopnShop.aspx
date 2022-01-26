<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ShopnShop.aspx.cs" Inherits="Modules_Masters_ShopnShop" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

     <%--<script lang="javascript" type="text/javascript">
         function OpenPopupCenter(pageURL, title, w, h) {
             var left = (screen.width - w) / 2;
             var top = (screen.height - h) / 4;  // for 25% - devide by 4  |  for 33% - devide by 3
             var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
         }
    </script>--%>

            <%--<script type="text/javascript">
                function DeliveryItemsCheck() {
                    var ReqQty, BalQty;
                    ReqQty = document.getElementById('<%=txtItemQuantity.ClientID%>').value;
                    BalQty = document.getElementById('<%=txtBalanceQtyHidden.ClientID%>').value;
                    document.getElementById('<%=txtBalanceQty.ClientID%>').value = BalQty - ReqQty;
                    qtyinhandItemsCheck();
                    resqtyinhandItemsCheck();
                }



                function qtyinhandItemsCheck() {
                    var iReqQty, iBalQty;
                    iReqQty = document.getElementById('<%=txtQtyInHand.ClientID%>').value;
                    iBalQty = document.getElementById('<%=txtItemQuantity.ClientID%>').value;
                    document.getElementById('<%=txtInhand.ClientID%>').value = iReqQty - iBalQty;
                }

                function resqtyinhandItemsCheck() {
                    var resiReqQty, resiBalQty;
                    resiReqQty = document.getElementById('<%=txtOrderedQty.ClientID%>').value;
                    resiBalQty = document.getElementById('<%=txtItemQuantity.ClientID%>').value;
                    document.getElementById('<%=txtresqty.ClientID%>').value = resiReqQty - resiBalQty;
                }

                function Serialno() {
                    if (document.getElementById('<%=txtSerialNo.ClientID %>').value != "") {
                        document.getElementById('<%=txtItemQuantity.ClientID %>').value = "1";
                        document.getElementById('<%=txtItemQuantity.ClientID %>').readOnly = true;
                    }
                    else {
                        document.getElementById('<%=txtItemQuantity.ClientID %>').value = "";
                        document.getElementById('<%=txtItemQuantity.ClientID %>').readOnly = false;
                    }
                }
            </script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
</asp:Content>

