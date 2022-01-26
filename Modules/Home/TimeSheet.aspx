<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TimeSheet.aspx.cs" Inherits="Modules_Home_TimeSheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%--<link href="/css/bootstrap.min.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="/css/londinium-theme.css" rel="stylesheet" type="text/css" />--%>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
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
            <td colspan="2" style="height: 160px; text-align: center">
                <table id="tblemp" runat="server" width="100%">
                    <tr>
                        <td class="profilehead" colspan="4">&nbsp;Employee Details
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label2" Visible ="false"  runat="server" Text="Company Name :"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlCompanyid" Visible ="false"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompanyid_SelectedIndexChanged">
                            </asp:DropDownList></td>

                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label3" runat="server" Text="Department :"></asp:Label></td>
                        <td style="text-align: left" colspan="3">
                            <asp:DropDownList ID="ddlDept" runat="server" Visible="false" AutoPostBack="True" Font-Bold="False"
                                OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:CheckBoxList ID="CheckBoxList1" Width="100%" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="DEPT_NAME" DataValueField="DEPT_ID" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged" RepeatColumns="7" RepeatDirection="Horizontal">
                                    </asp:CheckBoxList>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [DEPT_ID], [DEPT_NAME] FROM [YANTRA_DEPT_MAST] where DEPT_ID not in (7,3,13,12,13.14,15,23,25,27,34,35,36,37,38)"></asp:SqlDataSource>
                                    <br />

                                    <asp:ListBox ID="ListBox1" runat="server" Rows="5" SelectionMode="Multiple"></asp:ListBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        
                    </tr>
                    
                    <%--<tr>
                        <td style="text-align: right">
                            <asp:Label id="Label8" runat="server" Text="Departments :"></asp:Label></td>
                        <td style="text-align: left" colspan="3">
                            <asp:CheckBoxList ID="chkDept" runat="server" RepeatDirection="Horizontal" RepeatColumns="5" Width="90%">
                                <asp:ListItem>CEO</asp:ListItem>
                                <asp:ListItem>CMD</asp:ListItem>
                                <asp:ListItem>Customer Care</asp:ListItem>
                                <asp:ListItem>EDP</asp:ListItem>
                                <asp:ListItem>Finance</asp:ListItem>
                                <asp:ListItem>General</asp:ListItem>
                                <asp:ListItem>HR &amp; Admin</asp:ListItem>
                                <asp:ListItem>Office Assistance</asp:ListItem>
                                <asp:ListItem>Purchases</asp:ListItem>
                                <asp:ListItem>Sales-Marketing</asp:ListItem>
                                <asp:ListItem>Secretariat</asp:ListItem>
                                <asp:ListItem>Stores</asp:ListItem>
                                <asp:ListItem>Technical</asp:ListItem>
                            </asp:CheckBoxList>
                            </td>
                       
                    </tr>--%>

                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label1" Visible="false" runat="server" Text="Employe Name :"></asp:Label>
                            <asp:Label ID="Label4" runat="server" Visible="false" Text="Designation :"></asp:Label>

                        </td>


                        <td style="text-align: left">
                            <asp:TextBox ID="txtDesignation" Visible="false" runat="server">
                            </asp:TextBox>
                            <asp:DropDownList ID="ddlEmployee" Visible="false" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                            </asp:DropDownList>

                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label5" runat="server" Visible="false" Text="Mobile No :"></asp:Label>

                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtMobileno" Visible="false" runat="server">
                            </asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right"><asp:Label ID="lblEmpIdHidden" runat ="server" Visible ="false" ></asp:Label></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                </table>
            </td>
        </tr>
                    <tr>
            <td id="Image1" colspan="2">
                <table id="tblcircular" runat="server" border="0" cellpadding="0" cellspacing="0"
                     width="100%">
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">Task Details
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label7" runat="server" Text="Subject :"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtcirNo" runat="server" TextMode ="MultiLine"  EnableTheming="True"></asp:TextBox></td>
                         <td style="text-align: right">
                            <asp:Label ID="Label6" runat="server" Text="Issued Date :"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDate" runat="server" type="datepic">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right" valign="top">
                            <asp:Label ID="lblDesignationName" runat="server" Height="22px" Text="Task Activity Description :"
                                Width="80px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtdescription" runat="server" EnableTheming="True" TextMode="MultiLine" Width="500px"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label8" runat="server" Text="Task Activity Status:"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlActivity" runat ="server" >
                                <asp:ListItem Value ="55" >To Do</asp:ListItem>
                                <asp:ListItem Value ="56" >In-Progress</asp:ListItem>
                                <asp:ListItem Value ="57">Completed</asp:ListItem>
                            </asp:DropDownList>

                        </td>

                    </tr>
                    
                   
                    <tr>
                        <td colspan="4" style="height: 49px; text-align: center">
                            <table id="tblButtons" align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick ="btnSave_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" 
                                            Text="Refresh" /></td>
                                    <td style="width: 52px">
                                        <asp:Button ID="btnClose" runat="server" CausesValidation="False" 
                                            Text="Close" /></td>
                                    <td style="width: 18px">&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False"></asp:ValidationSummary>
                

            </td>
        </tr>
                </table>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>

