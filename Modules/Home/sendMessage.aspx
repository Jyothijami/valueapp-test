<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="sendMessage.aspx.cs" Inherits="Modules_sendMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
   <%-- <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.1/jquery.min.js"></script>
    <link href="~/select/select2.css" rel="stylesheet" runat="server" type="text/css" />
    <script src="~/select/select2.js" ></script>--%>
    <%--<link href= "~/css/styles.css" rel="stylesheet" runat="server" type="text/css" />--%>
                    <div class="btn-group" role="group" aria-label="...">
                    <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/Modules/Home/sendMessage.aspx"
                    CssClass="btn btn-default">Send Message</asp:HyperLink>
                    <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="~/Modules/Home/my_inbox.aspx"
                    CssClass="btn btn-default">My 
                Inbox/Todo List Inbox</asp:HyperLink>
                                    <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/Modules/Home/my_outbox.aspx"
                    CssClass="btn btn-default">My 
                Sent Mail/Todo List Outbox</asp:HyperLink>
                         <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Modules/Home/TimeSheet.aspx"
                    CssClass="btn btn-default">Send 
                To Do List</asp:HyperLink>

</div>
    <div>
        
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

                                        <asp:SqlDataSource ID="usersds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [USER_ID], [USER_NAME] FROM [YANTRA_USER_DETAILS],YANTRA_EMPLOYEE_MAST where YANTRA_USER_DETAILS .Emp_id =YANTRA_EMPLOYEE_MAST .EMP_ID and STATUS !=0  ORDER BY [USER_NAME]"></asp:SqlDataSource>
                                        <asp:HiddenField ID="hffromuid1" runat="server" />


                                           
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
               <%-- <script>
                    $(document).ready(function () {
                        $("#Books").select2({ placeholder: 'Find and Select Books' });
                    });
</script>--%>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <div>
                    Message Sent Successfully<br />
                    <br />
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Modules/Home/my_inbox.aspx">GoTo Inbox</asp:HyperLink>
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
</asp:Content>


 
