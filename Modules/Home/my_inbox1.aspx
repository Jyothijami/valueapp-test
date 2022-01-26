<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/AdminMP1.master" AutoEventWireup="true" CodeFile="my_inbox1.aspx.cs" Inherits="Modules_Home_my_inbox" %>

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
                Sent Mail/Todo List Outbox</asp:HyperLink>
                     <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/Modules/Home/TimeSheet.aspx"
                    CssClass="btn btn-default">Send 
                To Do List</asp:HyperLink>
</div>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:LinkButton ID="lnkbtnmsgs" runat ="server" OnClick ="lnkbtnmsgs_Click">My Inbox</asp:LinkButton>
                <%--<asp:Label ID="Label4" runat="server" BackColor="#FFFFC0"></asp:Label>--%>
                ||
               <asp:LinkButton ID="lnkbtnTasklist" runat ="server" OnClick ="lnkbtnTasklist_Click">ToDo Activity List</asp:LinkButton>


            </td>
        </tr>
            <tr>
            <td>
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="smsg">Subject</asp:ListItem>
                                            <asp:ListItem Value="posted_date">Date</asp:ListItem>
                                            <asp:ListItem Value="USER_NAME">Executive</asp:ListItem>
                                            <asp:ListItem Value="read_msg">Unread{0}/Read{1}</asp:ListItem>
                                            <%--<asp:ListItem Value="CUST_EMAIL">EMail</asp:ListItem>--%>
                                            <%--<asp:ListItem Value="QUOT_PO_FLAG">Status</asp:ListItem>--%>
                                        </asp:DropDownList></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList ID="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                            EnableTheming="False" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged"
                                            Visible="False" Width="50px">
                                            <asp:ListItem Selected="True">=</asp:ListItem>
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem>&gt;</asp:ListItem>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <asp:ListItem>&gt;=</asp:ListItem>
                                            <asp:ListItem>R</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="height: 25px">
                                        <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchValueFromDate" type="datepic" runat="server" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox><%--<asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchFrom" runat="server" Enabled="False"
                                            PopupButtonID="imgFromDate" TargetControlID="txtSearchValueFromDate">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>--%>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchValueToDate" type="datepic" runat="server" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><%--<asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchValueToDate" runat="server"
                                            Enabled="False" PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>--%>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                                <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False">0</asp:Label>
                <asp:Label ID="lblSearchItemHidden"  Text="0" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchTypeHidden"  runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchValueHidden"  Text="0" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblShow1" runat ="server" Visible ="false"></asp:Label>
                                    <asp:Label ID="lblfrndid" runat ="server" Visible ="false"></asp:Label>
                                </tr>
                            </table>


                        </td>
                </tr>
        <tr>
            <td style="padding: 5px; text-align: right">
                <asp:HyperLink ID="HyperLink2" runat="server">Newer</asp:HyperLink>
                <asp:HyperLink ID="HyperLink3" runat="server">Older</asp:HyperLink></td>
        </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlTasklist" runat ="server" Visible ="false" >
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DataList ID="DataList2" runat="server" Width="100%" DataKeyField="CIR_ID" >
                            <HeaderTemplate>
                                <table width="100%" style="background-image: url('/images/inbox_back.PNG'); height: 30px;">
                                    <tr>
                                        <td>Subject</td>
                                        <td style="text-align: right">Sent by | On Date</td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Panel ID="pl1" runat="server" BackColor='<%# color_msg(Convert.ToBoolean(Eval("read_msg"))) %>'>

                                    <table class="content" width="100%" cellpadding="5">
                                        <tr>
                                            <td style="text-align:left;">
                                                <asp:HyperLink ID="HyperLink1" runat="server"
                                                    NavigateUrl='<%# Eval("CIR_ID","~/Modules/Home/view_TaskList.aspx?CIR_ID={0}") %>' Text='<%# Eval("CIR_NO") %>' CssClass="message-link"></asp:HyperLink>
                                            </td>
                                            <td style="text-align: right" width="40%">
                                                <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl='#'
                                                    Text='<%# getUserName1(Eval("DEPT_ID").ToString()) %>'></asp:HyperLink>
                                                &nbsp;|
                <asp:Label ID="Label2" runat="server" Text='<%# get_date(Convert.ToDateTime(Eval("CIR_DATE"))) %>' CssClass="message-dt-time"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    </asp:Panel> 
                            </ItemTemplate>
                        </asp:DataList>
                        <asp:Panel ID="notaskpnl" runat="server" Visible="False">
                            No Messages in your inbox
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:SqlDataSource ID="SqlDataSource2" runat ="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_Task_Search" SelectCommandType="StoredProcedure" >
                    <SelectParameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName ="Text" Type ="String" DefaultValue ="0" Name ="Show1" ControlID ="lblShow1" />
                        <asp:ControlParameter PropertyName ="Text" Type ="String" Name ="frnd" DefaultValue ="0" ControlID ="lblfrndid" />
<%--<asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="USERTYPE" ControlID="lblUserType"></asp:ControlParameter>--%>
</SelectParameters>
                </asp:SqlDataSource>
                    </asp:Panel>
                </td>
            </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DataList ID="DataList1" runat="server" Width="100%" DataKeyField="msgid" >
                            <HeaderTemplate>
                                <table width="100%" style="background-image: url('/images/inbox_back.PNG'); height: 30px;">
                                    <tr>
                                        <td>Subject</td>
                                        <td style="text-align: right">Sent by | On Date</td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Panel ID="pl1" runat="server" BackColor='<%# color_msg(Convert.ToBoolean(Eval("read_msg"))) %>'>
                                    <table class="content" width="100%" cellpadding="5">
                                        <tr>
                                            <td style="text-align:left;">
                                                <asp:HyperLink ID="HyperLink1" runat="server"
                                                    NavigateUrl='<%# Eval("msgid","~/Modules/Home/view_message.aspx?msgid={0}") %>' Text='<%# Eval("smsg") %>' CssClass="message-link"></asp:HyperLink>
                                            </td>
                                            <td style="text-align: right" width="40%">
                                                <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl='#'
                                                    Text='<%# getUserName(Eval("uid").ToString()) %>'></asp:HyperLink>
                                                &nbsp;|
                <asp:Label ID="Label2" runat="server" Text='<%# get_date(Convert.ToDateTime(Eval("posted_date"))) %>' CssClass="message-dt-time"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </ItemTemplate>
                        </asp:DataList>
                        <asp:Panel ID="nomsgsPanel1" runat="server" Visible="False">
                            No Messages in your inbox
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:SqlDataSource ID="SqlDataSource1" runat ="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_MSG_Search" SelectCommandType="StoredProcedure" >
                    <SelectParameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName ="Text" Type ="String" DefaultValue ="0" Name ="Show1" ControlID ="lblShow1" />
                        <asp:ControlParameter PropertyName ="Text" Type ="String" Name ="frnd" DefaultValue ="0" ControlID ="lblfrndid" />
<%--<asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="USERTYPE" ControlID="lblUserType"></asp:ControlParameter>--%>
</SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td style="padding: 5px; text-align: right">
                <asp:HyperLink ID="HyperLink4" runat="server">Newer</asp:HyperLink>
                <asp:HyperLink ID="HyperLink5" runat="server">Older</asp:HyperLink></td>
        </tr>
    </table>

</asp:Content>


 
