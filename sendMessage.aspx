<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sendMessage.aspx.cs" Inherits="sendMessage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.1/jquery.min.js"></script>
    <script src="select/select2.js"></script>
    <link href="select/select2.css" rel="stylesheet"/>

</head>
<body>
    <form id="form1" runat="server">
<div id="myModal" class="modal fade" role="dialog">
    <div class="modal-dialog">
        <!-- Modal content-->
<div class="modal-content">
<div class="modal-header">
<button type="button" class="close" data-dismiss="modal">×</button>
<h4 class="modal-title">New Message</h4>
</div>
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
                <table style="width:100%;">
                    <tr>
                        <td><h4>Send Message</h4></td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width:100%;">
                                <tr>
                                    <td>To:</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddluname1" runat="server" DataSourceID="usersds1" DataTextField="USER_NAME" DataValueField="USER_ID">
                                        </asp:DropDownList>
                                        <select id="Books" style="width:300px" runat="server"></select>

                                        <asp:SqlDataSource ID="usersds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [USER_ID], [USER_NAME] FROM [YANTRA_USER_DETAILS] where EXPIRY_DATE >='2019-12-31 00:00:00.000' ORDER BY [USER_NAME]"></asp:SqlDataSource>
                                        <asp:HiddenField ID="hffromuid1" runat="server" />


                                           <script>
                                               $(document).ready(function () {
                                                   $("#Books").select2({ placeholder: 'Find and Select Books' });
                                               });
</script>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Subject:</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="tbxsubj1" runat="server" Width="500px"></asp:TextBox>
                                        <asp:Label ID="lblImp" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxsubj1" ErrorMessage="*" ForeColor="Red" ToolTip="Please Enter Subject"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Message:</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="tbxmessage1" runat="server" Height="170px" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DataList ID="DLAttachments2" runat="server" DataKeyField="msgattid">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%# Eval("attfilename", "~/Content/messagesAttachments/{0}") %>' Target="_blank" Text='<%# Eval("attname") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:DataList>
                            <br />
                            <asp:LinkButton ID="lkbtAddAttachments1" runat="server" OnClick="lkbtAddAttachments1_Click">Add Attachments</asp:LinkButton>
                            <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Text="Before adding attachments please provide the subject"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnSendMessage1" runat="server" OnClick="btnSendMessage1_Click" Text="Send Message" />
                            &nbsp;</td>
                    </tr>
                </table>
                
            </asp:View>
            <asp:View ID="View2" runat="server">
                <div>
                    Message Sent Successfully<br />
                    <br />
                    <a href="javascript:window.close()">Click here to close</a>
                </div>
            </asp:View>
            <asp:View ID="View3" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <h3>Add Attachments</h3>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DataList ID="DLAttachments1" runat="server" DataKeyField="msgattid">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%# Eval("attfilename", "~/Content/messagesAttachments/{0}") %>' Target="_blank" Text='<%# Eval("attname") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:DataList>
                            <asp:SqlDataSource ID="msgsattachsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT * FROM [msgs_Attachments_tbl]"></asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                            &nbsp;<asp:Button ID="btnAttach1" runat="server" OnClick="btnAttach1_Click" Text="Attach" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:Button ID="btnAttachDone1" runat="server" OnClick="btnAttachDone1_Click" Text="Done" />
                        </td>
                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>    

    </div>
    </form>
</body>
    
</html>
