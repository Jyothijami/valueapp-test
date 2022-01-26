<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Apprisal.aspx.cs" Inherits="Modules_HRManagement_Apprisal" Title="|| YANTRA : HR Management : Apprisal ||" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table class="pagehead">
        <tr>
            <td >
                ADDING PERSONAL PARTICULARS</td>
        </tr>
    </table>
    <br />
    <br />
    <table border="0" cellpadding="0" cellspacing="0" style="width: 774px">
        <tr>
            <td class="searchhead" colspan="4" style="height: 21px; text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">
                            Appraisal Details</td>
                        <td>
                        </td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
<%--                                        <asp:Label id="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label>--%>
                                        Search By : 
                                    </td>
                                    <td style="color: #ffffff">
                                        <asp:DropDownList id="ddlCurrentDayTaskSearchBy" runat="server" CssClass="textbox">
                                        </asp:DropDownList></td>
                                    <td>
                                        <asp:DropDownList id="ddlCurrentTasksSymbols" runat="server" AutoPostBack="True"
                                            CssClass="textbox" Visible="False" Width="50px">
                                            <asp:ListItem Selected="True">=</asp:ListItem>
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem>&gt;</asp:ListItem>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <asp:ListItem>&gt;=</asp:ListItem>
                                            <asp:ListItem>R</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td>
                                        <asp:Label id="lblCurrentFromDate" runat="server" CssClass="label" Font-Bold="True"
                                            ForeColor="White" Text="From" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox id="txtCurrentDayTasksFromDate" runat="server" CssClass="textbox" Visible="False">
                                        </asp:TextBox><asp:Image id="Image6" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image></td>
                                    <td>
                                        <asp:Label id="lblCurrentToDate" runat="server" CssClass="label" Font-Bold="True"
                                            ForeColor="White" Text="To " Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox id="txtCurrentDayTaskSearchText" runat="server" CssClass="textbox">
                                        </asp:TextBox><asp:Image id="imgCurrentDayTasksToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image></td>
                                    <td>
                                        <asp:Button id="btnCurrentDayTasksGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" Text="Go" /></td>
                                </tr>
                                </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 21px; text-align: left">
                <table border="0" cellpadding="4" cellspacing="0" style="font-weight: normal; font-size: 8pt;
                    color: black; font-family: Verdana; text-align: left; text-decoration: none"
                    width="100%">
                    <tr>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Emp. No.</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <span style="color: #ffffff"><strong>Emp. Name</strong></span></td>
                        <td colspan="1" style="color: #000000; background-color: #1aa8be">
                            <span style="color: #ffffff"><strong>Present Position</strong></span></td>
                        <td style="color: #000000; background-color: #1aa8be">
                            <span style="color: #ffffff"><strong>DOJ</strong></span></td>
                        <td style="color: #000000; background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Prepared By</span></strong></td>
                        <td style="color: #000000; background-color: #1aa8be">
                            <strong><span style="color: #ffffff">From</span></strong></td>
                        <td style="color: #000000; background-color: #1aa8be">
                            <strong><span style="color: #ffffff">To</span></strong></td>
                        <td style="color: #000000; background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Dept.</span></strong></td>
                        <td style="color: #000000; background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Present Grade</span></strong></td>
                        <td style="color: #000000; background-color: #1aa8be">
                            <strong><span style="color: #ffffff">DOB</span></strong></td>
                        <td style="color: #000000; background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Effective Date</span></strong></td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                            E/789</td>
                        <td style="background-color: #eff3fb">
                            Vijay</td>
                        <td style="background-color: #eff3fb">
                            Manger</td>
                        <td style="background-color: #eff3fb">
                            03/04/2008</td>
                        <td style="background-color: #eff3fb">
                            Kiran</td>
                        <td style="background-color: #eff3fb">
                            23/04/2008</td>
                        <td style="background-color: #eff3fb">
                            12/06/2008</td>
                        <td style="background-color: #eff3fb">
                            Marketing</td>
                        <td style="background-color: #eff3fb">
                            B</td>
                        <td style="background-color: #eff3fb">
                            29/05/1985</td>
                        <td style="background-color: #eff3fb">
                            11/05/2008</td>
                    </tr>
                    <tr>
                        <td style="background-color: white">
                            &nbsp;</td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 21px; text-align: left">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 21px; text-align: center">
                <table id="Table1">
                    <tr>
                        <td>
                            <asp:Button id="btnNew" runat="server" Text="New" /></td>
                        <td>
                            <asp:Button id="Button2" runat="server" Text="Edit" /></td>
                        <td>
                            <asp:Button id="Button11" runat="server" Text="Delete" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: left; height: 21px;" class="profilehead">
                General Details</td>
        </tr>
        <tr>
            <td colspan="5" style="height: 19px; text-align: left">
                
            </td>
        </tr>
        <tr>
            <td colspan="1" style="text-align: left">
            </td>
            <td colspan="1" style="text-align: left">
            </td>
            <td colspan="1" style="text-align: right;">
            <%--<asp:Label id="Label2" runat="server" Text="From"></asp:Label>--%>From : 
            </td>
            <td colspan="1" style="text-align: left;">
                <asp:TextBox id="txtFrom" runat="server" ></asp:TextBox>&nbsp;<asp:Image id="Image7" runat="server" ImageUrl="~/Images/Calendar.png" >
                </asp:Image></td>
            <td colspan="1" style="text-align: left">
                </td>
        </tr>
        <tr>
            <td colspan="1" style="text-align: right; height: 26px;">
                <%--<asp:Label id="Label4" runat="server" Text="Employee Number" ></asp:Label>--%>
                Employee Number : 
            </td>
            <td colspan="1" style="text-align: left; height: 26px;">
                <asp:TextBox id="txtNumber" runat="server">
                </asp:TextBox></td>
            <td colspan="1" style="height: 26px; text-align: right;">
                <%--<asp:Label id="Label3" runat="server" Text="To"></asp:Label>--%>
                To : 
            </td>
            <td colspan="1" style="text-align: left; height: 26px;">
                <asp:TextBox id="txtTo"
                    runat="server" ></asp:TextBox>&nbsp;<asp:Image id="Image1" runat="server" ImageUrl="~/Images/Calendar.png" >
                    </asp:Image></td>
            <td colspan="1" style="text-align: left; height: 26px;">
                </td>
        </tr>
        <tr>
            <td colspan="1" style="text-align: right">
                <%--&nbsp;<asp:Label id="lblEmployeeName" runat="server" Text="Employee Name" ></asp:Label>&nbsp;--%>
                Employee Name : 
            </td>
            <td colspan="1" style="text-align: left">
                <asp:TextBox id="txtEmployeeName1" runat="server"></asp:TextBox></td>
            <td colspan="1" style="text-align: right;">
                <%--<asp:Label id="Label8" runat="server" Text="Department/Divison/Location"></asp:Label>--%>
                Department/Divison/Location : 
            </td>
            <td colspan="1" style="text-align: left;">
                <asp:TextBox id="txtDepartment" runat="server">
                </asp:TextBox></td>
            <td colspan="1" style="text-align: left">
                </td>
        </tr>
        <tr>
            <td colspan="1" style="text-align: right; height: 26px;">
                &nbsp;<%--<asp:Label id="Label5" runat="server" Text="Present Position" ></asp:Label>--%>
                Present Position : 
            </td>
            <td colspan="1" style="text-align: left; height: 26px;">
                <asp:TextBox id="txtPosition" runat="server">
                </asp:TextBox></td>
            <td colspan="1" style="text-align: right; height: 26px;">
                <%--<asp:Label id="Label9" runat="server" Text="Present Grade"></asp:Label>--%>
                Present Grade : 
            </td>
            <td colspan="1" style="text-align: left; height: 26px;">
                <asp:TextBox id="txtGrade" runat="server">
                </asp:TextBox></td>
            <td colspan="1" style="text-align: left; height: 26px;">
                </td>
        </tr>
        <tr>
            <td colspan="1" style="text-align: right">
                <%--<asp:Label id="Label6" runat="server" Text="Date Of Joining The Co." ></asp:Label>--%>
                Date Of Joining The Co. : 
            </td>
            <td colspan="1" style="text-align: left">
                <asp:TextBox id="txtJoining" runat="server">
                </asp:TextBox></td>
            <td colspan="1" style="text-align: right;">
                <%--<asp:Label id="Label10" runat="server" Text="Date Of Birth"></asp:Label>--%>
                Date Of Birth : 
            </td>
            <td colspan="1" style="text-align: left;">
                <asp:TextBox id="txtDOB" runat="server" ></asp:TextBox>&nbsp;<asp:Image id="Image2" runat="server" ImageUrl="~/Images/Calendar.png" >
                </asp:Image></td>
            <td colspan="1" style="text-align: left">
                </td>
        </tr>
        <tr>
            <td colspan="1" style="text-align: right; height: 26px;">
                <%--<asp:Label id="Label7" runat="server" Text="Prepared By" ></asp:Label>--%>
                Prepared By : 
            </td>
            <td colspan="1" style="text-align: left; height: 26px;">
                <asp:TextBox id="txtPreparedBy1" runat="server"></asp:TextBox></td>
            <td colspan="1" style="text-align: right; height: 26px;">
                <%--<asp:Label id="Label11" runat="server" Text="Effective Date"></asp:Label>--%>
                Effective Date : 
            </td>
            <td colspan="1" style="text-align: left; height: 26px;">
                <asp:TextBox id="txtEffectiveDate" runat="server" ></asp:TextBox>&nbsp;<asp:Image id="Image3" runat="server" ImageUrl="~/Images/Calendar.png" >
                </asp:Image></td>
            <td colspan="1" style="text-align: left; height: 26px;">
            </td>
        </tr>
        <tr>
            <td colspan="1" style="text-align: right">
            </td>
            <td colspan="1" style="text-align: left">
            </td>
            <td colspan="1" style="text-align: left;">
            </td>
            <td colspan="1" style="text-align: left;">
            </td>
            <td colspan="1" style="text-align: left">
            </td>
        </tr>
        <tr>
            <td colspan="1" style="height: 26px; text-align: right">
            </td>
            <td colspan="1" style="height: 26px; text-align: left">
            </td>
            <td colspan="1" style="height: 26px; text-align: left;">
            </td>
            <td colspan="1" style="height: 26px; text-align: left;">
                <asp:Button id="btnEdit" runat="server" Text="Edit" EnableTheming="True"  /></td>
            <td colspan="1" style="height: 26px; text-align: left">
                </td>
        </tr>
        <tr>
            <td colspan="1" style="text-align: right">
            </td>
            <td colspan="1" style="text-align: left">
            </td>
            <td colspan="1" style="text-align: left;">
            </td>
            <td colspan="1" style="text-align: left;">
            </td>
            <td colspan="1" style="text-align: left">
            </td>
        </tr>
        <tr>
            <td colspan="5" style="text-align: left" class="profilehead">
                General Details</td>
        </tr>
        <tr>
            <td colspan="1" style="text-align: right; height: 19px;">
            </td>
            <td colspan="1" style="text-align: left; height: 19px;">
            </td>
            <td colspan="1" style="text-align: left; height: 19px;">
            </td>
            <td colspan="1" style="text-align: left; height: 19px;">
            </td>
            <td colspan="1" style="text-align: left; height: 19px;">
            </td>
        </tr>
        <tr>
            <td colspan="1" style="text-align: right">
                <%--<asp:Label id="Label13" runat="server" Text="Employee Name" ></asp:Label>--%>
                Employee Name : 
            </td>
            <td colspan="1" style="text-align: left">
                <asp:TextBox id="txtEmployeeName" runat="server">
                </asp:TextBox></td>
            <td colspan="1" style="text-align: left;">
                </td>
            <td colspan="1" style="text-align: left;">
            </td>
            <td colspan="1" style="text-align: left">
            </td>
        </tr>
        <tr>
            <td colspan="1" style="text-align: right">
            </td>
            <td colspan="1" style="text-align: left">
            </td>
            <td colspan="1" style="text-align: left;">
            </td>
            <td colspan="1" style="text-align: left;">
            </td>
            <td colspan="1" style="text-align: left">
            </td>
        </tr>
        <tr>
            <td colspan="5" style="height: 23px; text-align: left" class="profilehead">
                &nbsp;Agreed Performance Standered Details</td>
        </tr>
        <tr>
            <td colspan="4" style="height: 23px; text-align: center">
                <table border="0" cellpadding="4" cellspacing="0" style="font-weight: normal; font-size: 8pt;
                    color: black; font-family: Verdana; text-align: left; text-decoration: none" width="100%">
                    <tr>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Priority</span></strong></td>
                        <td style="background-color: #1aa8be; text-align: left;">
                            <strong><span style="color: #ffffff">Key Areas</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: white">Performance Standards</span></strong></td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                            &nbsp;</td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                    </tr>
                    <tr style="font-weight: bold">
                        <td style="background-color: white">
                            &nbsp;</td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                            &nbsp;</td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: white">
                            &nbsp;</td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                    </tr>
                </table>
            </td>
            <td colspan="1" style="height: 23px; text-align: left">
            </td>
        </tr>
        <tr>
            <td colspan="1" style="height: 23px; text-align: right">
            </td>
            <td colspan="1" style="height: 23px; text-align: left">
            </td>
            <td colspan="1" style="height: 23px; text-align: left;">
            </td>
            <td colspan="1" style="height: 23px; text-align: left;">
            </td>
            <td colspan="1" style="height: 23px; text-align: left">
            </td>
        </tr>
        <tr>
            <td colspan="1" style="text-align: right">
                <%--<asp:Label id="Label15" runat="server" Text="Apprisees Name"></asp:Label>--%>
                Apprisees Name 
                <%--<asp:Label id="Label16" runat="server" Text="and"></asp:Label>--%>
            </td>
            <td colspan="1" style="text-align: left">
                <asp:TextBox id="txtApprisees" runat="server">
                </asp:TextBox></td>
            <td colspan="1" style="text-align: right;">
                <%--<asp:Label id="Label18" runat="server" Text="Apprisees Name and"></asp:Label>--%>
                Apprisees Name
            </td>
            <td colspan="1" style="text-align: left;">
                <asp:TextBox id="txtApprisees2" runat="server">
                </asp:TextBox>&nbsp;</td>
            <td colspan="1" style="text-align: left">
                </td>
        </tr>
        <tr>
            <td colspan="1" style="text-align: right">
                &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            &nbsp; &nbsp;<%--<asp:Label id="Label17" runat="server" Text="Date"></asp:Label>--%>&nbsp;
                Date : 
            </td>
            <td colspan="1" style="text-align: left">
                <asp:TextBox id="txtDate1" runat="server" >
                </asp:TextBox>&nbsp;<asp:Image id="Image4" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image></td>
            <td colspan="1" style="text-align: right;">
            <%--<asp:Label id="Label20" runat="server" Text="Date"></asp:Label>--%>
                Date : 
            </td>
            <td colspan="1" style="text-align: left;">
                <asp:TextBox id="txtDate2" runat="server" >
                </asp:TextBox>&nbsp;<asp:Image id="Image5" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image></td>
            <td colspan="1" style="text-align: left">
                </td>
        </tr>
        <tr>
            <td colspan="1" style="text-align: right; height: 26px;">
            </td>
            <td colspan="1" style="text-align: left; height: 26px;">
            </td>
            <td colspan="1" style="text-align: right; height: 26px;">
                <%--<asp:Label id="Label21" runat="server" Text="Prepared By"></asp:Label>--%>
                Prepared By : 
            </td>
            <td colspan="1" style="text-align: left; height: 26px;">
                <asp:TextBox id="txtPreparedBy2" runat="server">
                </asp:TextBox></td>
            <td colspan="1" style="text-align: left; height: 26px;">
            </td>
        </tr>
        <tr>
            <td colspan="1" style="text-align: right">
            </td>
            <td colspan="1" style="text-align: left">
            </td>
            <td colspan="1" style="text-align: right;">
            </td>
            <td colspan="1" style="text-align: left;">
            </td>
            <td colspan="1" style="text-align: left">
            </td>
        </tr>
        <tr>
            <td colspan="1" style="text-align: right">
            </td>
            <td colspan="1" style="text-align: left">
            </td>
            <td colspan="1" style="text-align: right;">
            </td>
            <td colspan="1" style="text-align: left;">
                <asp:Button id="Button1" runat="server" Text="Edit" EnableTheming="True"  /></td>
            <td colspan="1" style="text-align: left">
                </td>
        </tr>
        <tr>
            <td colspan="5" style="text-align: left" class="profilehead">
                General Details</td>
        </tr>
        <tr>
            <td colspan="1" style="text-align: right; height: 19px;">
            </td>
            <td colspan="1" style="text-align: left; height: 19px;">
            </td>
            <td colspan="1" style="text-align: right; height: 19px;">
            </td>
            <td colspan="1" style="text-align: left; height: 19px;">
            </td>
            <td colspan="1" style="text-align: left; height: 19px;">
            </td>
        </tr>
        <tr>
            <td colspan="1" style="text-align: right">
                <%--<asp:Label id="Label23" runat="server" Text="Employee Name" ></asp:Label>--%>
                Employee Name : 
            </td>
            <td colspan="1" style="text-align: left">
                <asp:TextBox id="txtEmpName" runat="server">
                </asp:TextBox></td>
            <td colspan="1" style="text-align: right;">
                </td>
            <td colspan="1" style="text-align: left;">
            </td>
            <td colspan="1" style="text-align: left">
            </td>
        </tr>
        <tr>
            <td colspan="1" style="height: 23px; text-align: right">
            </td>
            <td colspan="1" style="height: 23px; text-align: left">
            </td>
            <td colspan="1" style="height: 23px; text-align: right;">
            </td>
            <td colspan="1" style="height: 23px; text-align: left;">
            </td>
            <td colspan="1" style="height: 23px; text-align: left">
            </td>
        </tr>
        <tr>
            <td colspan="5" style="height: 23px; text-align: left" class="profilehead">
                &nbsp;Factors That Have Helped And/Or Hindererd Performence</td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <table border="0" cellpadding="4" cellspacing="0" style="font-weight: normal; font-size: 8pt;
                    color: black; font-family: Verdana; text-align: left; text-decoration: none" width="100%">
                    <tr>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Status</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: #ffffff">External View</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: #ffffff">Internal View</span></strong></td>
                        <td style="background-color: #1aa8be">
                            <strong><span style="color: white">Appraiser's Assessment</span></strong></td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                            &nbsp;</td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                    </tr>
                    <tr style="font-weight: bold">
                        <td style="background-color: white">
                            &nbsp;</td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                            &nbsp;</td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: white">
                            &nbsp;</td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                        <td style="background-color: white">
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                        <td style="background-color: #eff3fb">
                        </td>
                    </tr>
                </table>
            </td>
            <td colspan="1" style="text-align: left">
            </td>
        </tr>
        <tr>
            <td colspan="1" style="text-align: right; height: 33px;">
            </td>
            <td colspan="1" style="text-align: left; height: 33px;">
            </td>
            <td colspan="1" style="text-align: right; height: 33px;">
            </td>
            <td colspan="1" style="text-align: left; height: 33px;">
            </td>
            <td colspan="1" style="text-align: left; height: 33px;">
            </td>
        </tr>
        <tr>
            <td colspan="1" style="text-align: right; height: 24px;">
            </td>
            <td colspan="1" style="text-align: left; height: 24px;">
            </td>
            <td colspan="1" style="text-align: right; height: 24px;">
            </td>
            <td colspan="1" style="text-align: left; height: 24px;">
                <asp:Button id="btnSave" runat="server" Text="Save" /><asp:Button id="btnCancel"
                    runat="server" Text="Cancel" /></td>
            <td colspan="1" style="text-align: left; height: 24px;">
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
 
