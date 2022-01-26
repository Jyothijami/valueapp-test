<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/AdminMP1.master" AutoEventWireup="true" CodeFile="my_outbox1.aspx.cs" Inherits="Modules_my_outbox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table class="mainframe" width="100%">
        <tr>
            <td style="text-align: left">
                <div class="btn-group" role="group" aria-label="...">
                    <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/Modules/Home/sendMessage.aspx"
                    CssClass="btn btn-default">Send Message</asp:HyperLink>
                    <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="~/Modules/Home/my_inbox.aspx"
                    CssClass="btn btn-default">My 
                Inbox/Todo List Inbox</asp:HyperLink>
                                    <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/Modules/Home/my_outbox.aspx"
                    CssClass="btn btn-default">My 
                Sent Mail/ToDo list Outbox</asp:HyperLink>
                    <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/Modules/Home/TimeSheet.aspx"
                    CssClass="btn btn-default">Send
                To Do List</asp:HyperLink>
</div>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:LinkButton ID="lnkbtnmsgs" runat ="server" OnClick ="lnkbtnmsgs_Click">My Sent List</asp:LinkButton>
                <%--<asp:Label ID="Label4" runat="server" BackColor="#FFFFC0"></asp:Label>--%>||
                <asp:LinkButton ID="lnkbtnTasklist" runat ="server" OnClick ="lnkbtnTasklist_Click">ToDo Activity List</asp:LinkButton>

                </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:HyperLink ID="HyperLink2" runat="server">Newer</asp:HyperLink>
                <asp:HyperLink ID="HyperLink3" runat="server">Older</asp:HyperLink></td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlTasklist" runat ="server" Visible ="false" Width ="100%" >
                    <asp:UpdatePanel ID="UpdatePanel2" runat ="server" UpdateMode ="Conditional" >
                        <ContentTemplate >
                            <asp:DataList ID="DataList2"   runat ="server" Width ="100%" DataKeyField ="CIR_ID">
                            <HeaderTemplate >
                                <table style ="background-image :url('/images/inbox_back.PNG'); height: 30px;" width="100%">
                                    <tbody >
                                        <td style="width: 50%">Task Subject</td>
                                            <td style="width: 40%; text-align:right">Sent to | On Date</td>
                                    </tbody>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate >
                                <table class="content" width="100%" cellpadding="5">
                                    <tbody >
                                        <tr>
                                            <td style="width: 50%; text-align:left;">
                                                <asp:HyperLink ID="HyperLink1" runat="server"
                                                    NavigateUrl='<%# Eval("CIR_ID","~/Modules/Home/view_TaskList.aspx?CIR_ID={0}") %>'
                                                    Text='<%# Eval("CIR_NO") %>' CssClass="message-link"></asp:HyperLink></td>
                                            <td style="text-align: right" width="40%">
                                                <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl='#'
                                                    Text='<%# getUserName1(Eval("EMP_ID").ToString()) %>'></asp:HyperLink>
                                                &nbsp;|
                                                <asp:Label ID="Label2" runat="server" Text='<%# get_date(Convert.ToDateTime(Eval("CIR_DATE"))) %>' CssClass="message-dt-time"></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                        <asp:Panel ID="notaskpnl" runat="server" Visible="False">
                            No Todo List in your Outbox
                        </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td> <asp:Label ID="lblEmpIdHidden" runat ="server" Visible ="false"  ></asp:Label></td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DataList ID="DataList1" runat="server" Width="100%" DataKeyField="msgid">
                            <HeaderTemplate>
                                <table style="background-image: url('/images/inbox_back.PNG'); height: 30px;"
                                    width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="width: 50%">Subject</td>
                                            <td style="width: 40%; text-align:right">Sent to | On Date</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table class="content" width="100%" cellpadding="5">
                                    <tbody>
                                        <tr>
                                            <td style="width: 50%; text-align:left;">
                                                <asp:HyperLink ID="HyperLink1" runat="server"
                                                    NavigateUrl='<%# Eval("msgid","~/Modules/Home/view_message.aspx?msgid={0}") %>'
                                                    Text='<%# Eval("smsg") %>' CssClass="message-link"></asp:HyperLink></td>
                                            <td style="text-align: right" width="40%">
                                                <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl='#'
                                                    Text='<%# getUserName(Eval("frndid").ToString()) %>'></asp:HyperLink>
                                                &nbsp;|
                                                <asp:Label ID="Label2" runat="server" Text='<%# get_date(Convert.ToDateTime(Eval("posted_date"))) %>' CssClass="message-dt-time"></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>                        
                        <asp:Panel ID="nomsgsPanel1" runat="server" Visible="False">
                            No Messages in your Outbox
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:HyperLink ID="HyperLink4" runat="server">Newer</asp:HyperLink>
                <asp:HyperLink ID="HyperLink5" runat="server">Older</asp:HyperLink></td>
        </tr>
    </table>
</asp:Content>


 
