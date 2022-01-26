<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/AdminMP1.master" AutoEventWireup="true" CodeFile="view_message.aspx.cs" Inherits="Modules_Home_view_message" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("click", "[id$='replylkbt1']", function (e) {
                $(".smesgPnl2").css({ 'display': 'inline' });
                e.preventDefault();
            });
            $(document).on("click", "[id$='btnDiscard1']", function (e) {
                $(".smesgPnl2").css({ 'display': 'none' });
                e.preventDefault();
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
            <script language="javascript">
                function toggleSaveButton(checked, buttonID) {
                    if ((o = document.getElementById(buttonID)) != null) {
                        o.disabled = !checked;
                    }
                }

</script>  
                                        <div class="btn-group" role="group" aria-label="...">
                    <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/Modules/Home/sendMessage.aspx"
                    CssClass="btn btn-default">Send Message</asp:HyperLink>
                    <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="~/Modules/Home/my_inbox.aspx"
                    CssClass="btn btn-default">My 
                Inbox</asp:HyperLink>
                                    <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/Modules/Home/my_outbox.aspx"
                    CssClass="btn btn-default">My 
                Sent Mail/ToDo list</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/Modules/Home/TimeSheet.aspx"
                    CssClass="btn btn-default">Send
                To Do List</asp:HyperLink>
</div>
<br />
    <br />
    <table width="100%" cellpadding="0" cellspacing="0"
        style="border: 1px solid #CCCCCC">
        <tr>
            <td style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #CCCCCC;">
                <table cellpadding="3" cellspacing="3" style="width: 100%;">
                    <tr>
                        <td>
                            <asp:HyperLink ID="back_folder_hypl51" runat="server" CssClass="mdlinks">[back_folder_hypl51]</asp:HyperLink>
                        </td>
                        <td style="text-align: right">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="replylkbt1" runat="server" CssClass="mdlinks">Reply</asp:LinkButton>
                                    &nbsp;|
                                    <asp:LinkButton ID="lkbtForward1" runat="server" OnClick="lkbtForward1_Click">Forward</asp:LinkButton>
                                    &nbsp;|
                            <asp:LinkButton ID="delmesglkbt1" runat="server" CssClass="mdlinks" OnClick="delmesglkbt1_Click">Delete 
                            Message</asp:LinkButton>
                                    <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server"
                                        TargetControlID="delmesglkbt1" ConfirmText="Are You Sure ?">
                                    </cc1:ConfirmButtonExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="send_mesg_panel1" runat="server">
                    <asp:UpdatePanel ID="upnl1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table style="width: 100%; text-align:left;">
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Label ID="Label1" runat="server" __designer:wfdid="w31"
                                            BackColor="#FFFFC0"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="smesgPnl2" CssClass="smesgPnl2" runat="server" Style="display: none">
                                            <table style="width: 100%; text-align:left;">
                                                <tr>
                                                    <td>
                                                        <div class="keeprightinline">
                                                            <asp:LinkButton ID="msgexitlkbt1" runat="server" CssClass="msgexit"
                                                                OnClientClick="return false;">X</asp:LinkButton>
                                                        </div>
                                                        <asp:Label ID="Label2" runat="server" CssClass="msgh1" Text="Send Reply :"></asp:Label>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table style="width: 100%; text-align:left;">
                                                            <tr>
                                                                <td class="style1">Subject :
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="sub_tb1" runat="server" Width="75%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" class="style1">Message :
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="msg_tb1" runat="server" Height="155px" TextMode="MultiLine"
                                                                        Width="85%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style1"></td>
                                                                <td>
                                                                    <asp:Button ID="Button1" runat="server" Text="Send Message" OnClick="Button1_Click" />
                                                                    &nbsp;<asp:Button ID="btnDiscard1" runat="server" Text="Discard" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center">
                                                        <hr size="1px" style="color: #CCCCCC" title="Send Message" />
                                                    </td>
                                                </tr>
                                            </table>

                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <cc1:UpdatePanelAnimationExtender ID="upaext1" runat="server"
                        TargetControlID="upnl1">
                        <Animations>
<OnUpdating>
<Sequence>
	<EnableAction AnimationTarget="Button1" Enabled="false" />
</Sequence>
</OnUpdating></Animations>
                    </cc1:UpdatePanelAnimationExtender>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DataList ID="DataList1" runat="server" DataKeyField="msgid" DataSourceID="msgsds1"
                    Width="100%">
                    <ItemTemplate>
                        <table style="width: 100%; text-align:left;">
                            <tr>
                                <td style="text-align: left">
                                    <table>
                                        <tr>
                                            <td class="style2">
                                                <asp:Label ID="Label3" runat="server" CssClass="msgdimtext" Text="From :"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" Text='<%# getulink(Eval("uid").ToString()) %>' Font-Bold="True"></asp:Label>
                                                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("uid") %>' />
                                                <asp:HiddenField ID="frmhf1" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style2">
                                                <asp:Label ID="Label4" runat="server" CssClass="msgdimtext" Text="To :"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="HyperLink2" runat="server" Font-Bold="True"
                                                    NavigateUrl='#'
                                                    Text='<%# getUserName(Eval("frndid").ToString()) %>'></asp:HyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style2">
                                                <asp:Label ID="Label5" runat="server" CssClass="msgdimtext" Text="Date :"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="posted_dateLabel" runat="server"
                                                    Text='<%# get_date(Convert.ToDateTime(Eval("posted_date"))) %>' CssClass="message-dt-time"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style2">
                                                <asp:Label ID="Label6" runat="server" CssClass="msgdimtext" Text="Subject :"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="smsgLabel" runat="server" Font-Bold="True"
                                                    Text='<%# Eval("smsg") %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label7" runat="server" CssClass="msgdimtext" Text="Message : "></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding:20px;">
                                    <asp:Label ID="msgLabel" runat="server"
                                        Text='<%# Eval("msg").ToString().Replace("" + (char)13, "<br />" ) %>'></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
                <asp:SqlDataSource ID="msgsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT * FROM [msgs_tbl] WHERE ([msgid] = @msgid)">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="msgid" QueryStringField="msgid" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td style="padding: 20px; text-align: left">
                <asp:DataList ID="DataList2" runat="server" DataKeyField="msgattid" DataSourceID="msgattachsds1">
                    <HeaderTemplate>
                        <span class="auto-style1"><strong>Attachments</strong></span>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%# Eval("attfilename", "~/Content/messagesAttachments/{0}") %>' Target="_blank" Text='<%# Eval("attname") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:DataList>
                <asp:SqlDataSource ID="msgattachsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT * FROM [msgs_Attachments_tbl] WHERE ([msgid] = @msgid)">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="msgid" QueryStringField="msgid" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>


 
