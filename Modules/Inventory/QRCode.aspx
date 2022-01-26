<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/InventoryMP1.master" AutoEventWireup="true" CodeFile="QRCode.aspx.cs" Inherits="Modules_Inventory_QRCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <div id="print" runat="server" >
         <div>  
              <table>  
                  <tr>  
                      <td>  
                         <asp:Image ID="lblImage" runat="server" EnableTheming="False" Height="100px" ImageUrl='<%# Eval("Image","~/Content/QRCodes/{0}") %>' Width="100px" />
                      </td>  
                      <td></td>  
                 </tr>  
                 <tr>  
                     <td>Product Name: </td>  
                     <td><asp:Label ID="lblModelNo" runat ="server" ></asp:Label></td>  
                 </tr>  
                 <tr>  
                      <td>Quantity: </td>  
                     <td><asp:Label ID="lblQty" runat ="server" ></asp:Label></td>  
                 </tr>  
                 <tr>  
                      <td>Date of Import: </td>  
                      <td><asp:Label ID="lblmrndt" runat ="server" ></asp:Label></td>  
                 </tr>  
                 <tr>  
                      <td>Colour: </td>  
                     <td><asp:Label ID="lblColor" runat ="server" ></asp:Label></td>  
                 </tr>  
                 <tr>  
                     <td>M.R.P. (Inclusive of all Taxes): </td>  
                    <td></td>  
                 </tr>  
             </table>  
        </div>  
    
    </div>


    <div id="grid" style="width: 100%">
            <asp:GridView ID="gvInventoryInward" Width="100%" runat="server" SelectedRowStyle-BackColor="#c0c0c0" EmptyDataText="No Records To Display" AllowPaging="True"  PageSize="10" >
                <HeaderStyle HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="Silver"></SelectedRowStyle>
                <Columns >
                    <asp:TemplateField >
                        <ItemTemplate >
                            <asp:Button ID="btnQR" runat ="server" Text ="QRCode"  OnClick="btnQR_Click" />
                            <asp:Image ID="Image" runat="server" EnableTheming="False" Height="100px" ImageUrl='<%# Eval("Image","~/Content/QRCodes/{0}") %>'
                                        Width="100px" /> <asp:Button ID="btnPrint" runat ="server" Text="Print" OnClick ="btnPrint_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

         <hr />
    <asp:PlaceHolder ID="plBarCode" runat="server" />
        </div>








</asp:Content>

