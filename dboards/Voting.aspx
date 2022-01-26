<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/AdminMP1.master" AutoEventWireup="true" CodeFile="Voting.aspx.cs" Inherits="dboards_Voting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <link href="../css/DLstyle.css" rel="stylesheet" />
    <h2>Vote your co-worker</h2>

    <table style="width: 100%" class="pagehead">
        <tr>
            <td style ="text-align :right "><asp:Label ID="lbl1" runat ="server" Text ="Select Vote Type :" ></asp:Label></td>
            <td style ="text-align :left "><asp:DropDownList ID="ddlVoteType" runat ="server" >
                <asp:ListItem>Impecable Work</asp:ListItem>
                <asp:ListItem>Team Player</asp:ListItem>
                <asp:ListItem>Excellent Work</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan ="2" style ="text-align :right "><asp:Label ID="lbl2" runat ="server" Visible ="false"  ></asp:Label></td>
        </tr>
    </table>
    <div>
        <asp:DataList ID="DataList1" runat="server" Font-Names="Verdana" Font-Size="Small"
            RepeatColumns="6" RepeatDirection="Horizontal" OnItemCommand ="DataList1_ItemCommand" >
            <ItemStyle ForeColor="Black"/>
            <ItemTemplate>
                <div id="pricePlans">
                    <ul id="plans">
                        <li class="plan">
                            <ul class="planContainer">
                                <li class="title">
                                    <h6>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("EmpName") %>'></asp:Label></h6>
                                </li>
                                <li class="title">
                                    <asp:Image ID="imgPhoto" runat="server" Width="100px" Height="100px" ImageUrl='<%# Bind("EMP_PHOTO","~/Content/EmployeeImage/{0}") %>' />
                                </li>
                                <li>
                                    <ul class="options">
                                        <li><span>
                                            <asp:Label ID="lblCName" runat="server" Text='<%# Bind("DOJ") %>'></asp:Label></span></li>
                                        <li><span>
                                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("DEPT_NAME") %>'></asp:Label></span></li>
                                        <li><span>
                                            <asp:Label ID="lblCity" runat="server" Text=' <%# Bind("DESG_NAME") %>'></asp:Label></span></li>
                                        
                                    </ul>
                                </li>
                                <li> <asp:Button ID="btnVote" runat="server" Width ="50%" CssClass ="button "
Font-Underline="false" style="font-weight: 700; color: Black" CommandName="Vote" CommandArgument='<%#Eval("Emp_Id") %>' BackColor="#FF9933" Text ="Vote" /></li>
                            </ul>
                        </li>
                    </ul>
                </div>
                <%--   <asp:Image ID="imgEmp" runat="server" Width="100px" Height="120px" ImageUrl='<%# Bind("PhotoPath","~/photo/{0}") %>' style="padding-left:40px"/><br />
                <b>Employee Name:</b>
                <asp:Label ID="lblCName" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                <br />
                <b>Designation:</b>
                <asp:Label ID="lblName" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                <br />
               <b> City:</b>
                <asp:Label ID="lblCity" runat="server" Text=' <%# Bind("City") %>'></asp:Label>
                <br />
                <b>Country:</b>
                <asp:Label ID="lblCountry" runat="server" Text='<%# Bind("Country") %>'></asp:Label>
                <br />--%>
            </ItemTemplate>
        </asp:DataList>
    </div>
</asp:Content>

