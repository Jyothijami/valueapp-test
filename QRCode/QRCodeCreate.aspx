<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QRCodeCreate.aspx.cs" Inherits="QRCode_QRCodeCreate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Generate QR Code</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <table width="100%">
                 <tr>
                    <td colspan="2" style ="text-align :center " class="profilehead">GENERATE QR CODE</td>
                </tr>
                 <tr>
                     <td colspan="2" style="height: 1px">
                         <table width="100%">
                             <tr>
                                 <td  style ="text-align :right ">Name :</td>
                                 <td><asp:TextBox ID="txtName" CssClass ="" runat ="server" ></asp:TextBox></td>
                             </tr>
                             <tr>
                                 <td style ="text-align :right ">Mobile No :</td>
                                 <td><asp:TextBox ID="txtMobile" runat ="server" ></asp:TextBox></td>
                             </tr>
                             <tr>
                                 <td  style ="text-align :right ">Upload User Image :</td>
                                 <td><asp:FileUpload ID="Uploadattach" runat ="server"  />
                                     <asp:Label ID="lblAtt" runat ="server" Visible ="false" ></asp:Label></td>
                             </tr>
                             <tr>
                                 <td colspan ="2"  style ="text-align :center ">
                                     <asp:Button ID="btnSave" CssClass ="loginbutton " runat ="server" Text ="Generate QR Code" OnClick ="btnSave_Click"  />
                                 </td>
                             </tr>
                         </table>
                     </td>
                     
                 </tr>
                 <tr>
                     <td colspan="2" style="height: 1px">
                          <table width="100%">
                              <tr>
                                  <td colspan ="2"  style ="text-align :center "><asp:Image ID="imgQRCode" Width="100px" Height="100px" runat="server" Visible="false" /></td>
                              </tr>
                          </table>
                     </td>
                 </tr>
                 <tr>
                     <td colspan="2" style="height: 1px">
                          <table width="100%">
                              <tr>
                                <td colspan="2" style ="text-align :center " class="profilehead">Share QR Code via Whatsapp</td>
                              </tr>
                              <tr>
                                  <td style ="text-align :right ">To :</td>
                                  <td><asp:TextBox ID="txtToNo" runat ="server" ></asp:TextBox></td>
                              </tr>
                              <tr>
                                  <td style ="text-align :right ">Message :</td>
                                  <td>
                                  <asp:TextBox ID="txtMessage" TextMode ="MultiLine"  runat ="server" ></asp:TextBox>
                                  </td>
                              </tr>
                              <tr>
                                 <td colspan ="2"  style ="text-align :center ">
                                     <asp:Button ID="btnSend" CssClass ="loginbutton " runat ="server" Text ="Share QR Code" OnClick ="btnSend_Click"  />
                                 </td>
                             </tr>
                          </table>
                     </td>
                 </tr>
             </table>
        </div>
   
    </form>
</body>
</html>
