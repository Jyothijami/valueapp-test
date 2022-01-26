<%@ Page Language="C#" MasterPageFile="~/MPs/SalesMP1.master" AutoEventWireup="true" CodeFile="DailyReport-old.aspx.cs"
    Inherits="Modules_SM_DailyReport" Title="|| Value App : SM : Daily Report ||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="javascript" type="text/javascript">
        function check(e) {
            var keynum
            var keychar
            var numcheck
            // For Internet Explorer
            if (window.event) {
                keynum = e.keyCode;
            }
                // For Netscape/Firefox/Opera
            else if (e.which) {
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);
            //List of special characters you want to restrict
            if (keychar == "'" || keychar == "`" || keychar == "&" || keychar == "�" || keychar == '"') {
                return false;
            } else {
                return true;
            }
        }
    </script>
    <script type="text/javascript">
        // Let's use a lowercase function name to keep with JavaScript conventions
        function selectAll(invoker) {
            // Since ASP.NET checkboxes are really HTML input elements
            //  let's get all the inputs 
            var inputElements = document.getElementsByTagName('input');

            for (var i = 0 ; i < inputElements.length ; i++) {
                var myElement = inputElements[i];

                // Filter through the input types looking for checkboxes
                if (myElement.type === "checkbox") {

                    // Use the invoker (our calling element) as the reference 
                    //  for our checkbox status
                    myElement.checked = invoker.checked;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    
  
            <table style ="width :100%">
        <tr>
                    <td style="text-align: center; font-size: medium;">
                       <asp:LinkButton ID="lnkPOAmen" runat="server" OnClick="lnkPOAmen_Click" Font-Underline="True">Daily Report</asp:LinkButton>
                        &nbsp;||&nbsp;
                        <asp:LinkButton ID="lnkSalesReturn" runat="server" OnClick="lnkSalesReturn_Click1" CausesValidation ="false" Font-Underline="True">To Do List</asp:LinkButton>
                    </td>
                </tr>
    </table>
            
            <asp:Panel runat="server" ID="POAmendment"  >
            <table width="100%">
                <tr>
                    <td colspan="3" class="profilehead">Daily Report</td>
                </tr>
                <tr>
                    <td colspan="3" style="height: 1px">
                        <table width="100%">
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label1" runat="server" Text="Date & Time"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtDateTime" runat="server" type="datepic"></asp:TextBox><asp:Label
                                        ID="Label47" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtDateTime"
                                            ErrorMessage="Please Select the  Date" ValidationGroup="a">*</asp:RequiredFieldValidator>

                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">In Time 
                                </td>
                                <td>
                                    <table style="width: 64px">
                                        <tr>
                                            <td style="width: 100px">
                                                <asp:DropDownList
                                                    ID="ddlHour" runat="server" CssClass="textbox" TabIndex="3" Width="60px" EnableTheming="False">
                                                    <asp:ListItem>1</asp:ListItem>
                                                    <asp:ListItem>2</asp:ListItem>
                                                    <asp:ListItem>3</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                    <asp:ListItem>5</asp:ListItem>
                                                    <asp:ListItem>6</asp:ListItem>
                                                    <asp:ListItem>7</asp:ListItem>
                                                    <asp:ListItem>8</asp:ListItem>
                                                    <asp:ListItem>9</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>11</asp:ListItem>
                                                    <asp:ListItem>12</asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td style="width: 100px">
                                                <asp:DropDownList ID="ddlMin" runat="server" CssClass="textbox" TabIndex="4" Width="60px" EnableTheming="False">
                                                    <asp:ListItem>00</asp:ListItem>
                                                    <asp:ListItem>05</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>25</asp:ListItem>
                                                    <asp:ListItem>30</asp:ListItem>
                                                    <asp:ListItem>35</asp:ListItem>
                                                    <asp:ListItem>40</asp:ListItem>
                                                    <asp:ListItem>45</asp:ListItem>
                                                    <asp:ListItem>50</asp:ListItem>
                                                    <asp:ListItem>55</asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td style="width: 100px">
                                                <asp:DropDownList ID="ddlAMPM" runat="server" CssClass="textbox" TabIndex="5" Width="60px" EnableTheming="False">
                                                    <asp:ListItem>AM</asp:ListItem>
                                                    <asp:ListItem>PM</asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">Out Time 
                                </td>
                                <td>
                                    <table style="width: 64px; height: 32px;">
                                        <tr>
                                            <td style="width: 100px">
                                                <asp:DropDownList
                                                    ID="ddlOutHour" runat="server" CssClass="textbox" TabIndex="3" Width="60px" EnableTheming="False">
                                                    <asp:ListItem>1</asp:ListItem>
                                                    <asp:ListItem>2</asp:ListItem>
                                                    <asp:ListItem>3</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                    <asp:ListItem>5</asp:ListItem>
                                                    <asp:ListItem>6</asp:ListItem>
                                                    <asp:ListItem>7</asp:ListItem>
                                                    <asp:ListItem>8</asp:ListItem>
                                                    <asp:ListItem>9</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>11</asp:ListItem>
                                                    <asp:ListItem>12</asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td style="width: 100px">
                                                <asp:DropDownList ID="ddlOutMin" runat="server" CssClass="textbox" TabIndex="4" Width="60px" EnableTheming="False">
                                                    <asp:ListItem>00</asp:ListItem>
                                                    <asp:ListItem>05</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>25</asp:ListItem>
                                                    <asp:ListItem>30</asp:ListItem>
                                                    <asp:ListItem>35</asp:ListItem>
                                                    <asp:ListItem>40</asp:ListItem>
                                                    <asp:ListItem>45</asp:ListItem>
                                                    <asp:ListItem>50</asp:ListItem>
                                                    <asp:ListItem>55</asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td style="width: 100px">
                                                <asp:DropDownList ID="ddlOutAMPM" runat="server" CssClass="textbox" TabIndex="5" Width="60px" EnableTheming="False">
                                                    <asp:ListItem>AM</asp:ListItem>
                                                    <asp:ListItem>PM</asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label2" runat="server" Text="Client's Name"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtClientsName" onkeypress="return check(event)" runat="server"></asp:TextBox><asp:Label
                                        ID="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtClientsName"
                                            ErrorMessage="Please Enter Client Name">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label10" runat="server" Text="Phone No"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtPhoneNo" runat="server">
                                    </asp:TextBox><asp:Label
                                        ID="Label13" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPhoneNo"
                                            ErrorMessage="Please Enter Purpose" ValidationGroup="a">*</asp:RequiredFieldValidator>
                                    <cc1:FilteredTextBoxExtender ID="ftxteOtherCorpPhoneNo" runat="server" Enabled="True"
                                        TargetControlID="txtPhoneNo" ValidChars="-0123456789(),">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label15" runat="server" Text="Reference"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtReference" onkeypress="return check(event)" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label16" runat="server" Text="Architect"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtArchitect" onkeypress="return check(event)" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label11" runat="server" Text="Address"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtAddress" runat="server" onkeypress="return check(event)" TextMode="MultiLine" Width="355px" EnableTheming="False">
                                    </asp:TextBox><asp:Label
                                        ID="Label12" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAddress"
                                            ErrorMessage="Please Enter Purpose" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label3" runat="server" Text="Purpose"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtPurpose" runat="server" onkeypress="return check(event)" TextMode="MultiLine" Width="356px" EnableTheming="False"></asp:TextBox><asp:Label
                                        ID="Label8" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtClientsName"
                                            ErrorMessage="Please Enter Purpose" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label4" runat="server" Text="Remarks"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtRemarks" runat="server" onkeypress="return check(event)" TextMode="MultiLine" Width="354px" EnableTheming="False"></asp:TextBox><asp:Label
                                        ID="Label9" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtClientsName"
                                            ErrorMessage="Please Enter Remarks" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label5" runat="server" Text="Attended By"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlAttendedBy" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlAttendedBy"
                                        ErrorMessage="Please Slelect Attended By" InitialValue="0">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">Attachment</td>
                                <td style="text-align: left"><asp:FileUpload ID="Uploadattach" runat="server" AllowMultiple="true" />
                                    <asp:Label ID="txtBrowsetxt" runat ="server" Text="Please Upload Customer Related Files (Ex:Visting Card)" ForeColor ="Red"></asp:Label>
                                    <asp:Label ID="lblAtt" runat ="server" Visible ="false" ></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <asp:Button ID="btnADD" runat="server" Text="Add" OnClick="btnADD_Click" ValidationGroup="a" /></td>
                            </tr>
                            <tr>
                                <td colspan="2"></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 48px">
                                    <asp:GridView ID="gvDailyReport" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvDailyReport_RowDataBound" OnRowDeleting="gvDailyReport_RowDeleting" OnRowEditing="gvDailyReport_RowEditing" Width="100%">
                                        <Columns>
                                            <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                            <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                            <asp:BoundField DataField="CLIENTSNAME" HeaderText="Client's Name">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DATETIME" HeaderText="Date Time">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PURPOSE" HeaderText="Purpose">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="REMARKS" HeaderText="Remarks">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ATTENDEDBY" HeaderText="Attended By Id">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EMPLOYEENAME" NullDisplayText="-" HeaderText="Prepared By Id"></asp:BoundField>
                                            <asp:BoundField DataField="ATTENDEDBYNAME" HeaderText="Attended By"></asp:BoundField>
                                            <asp:BoundField DataField="EMPNAMEFORSHOW" HeaderText="Excutive Name"></asp:BoundField>
                                            <asp:BoundField DataField="HOUR" HeaderText="In Hour"></asp:BoundField>
                                            <asp:BoundField DataField="MIN" HeaderText="In Min"></asp:BoundField>
                                            <asp:BoundField DataField="AMPM" HeaderText="In AMPM"></asp:BoundField>
                                            <asp:BoundField DataField="ADDRESS" HeaderText="Address"></asp:BoundField>
                                            <asp:BoundField DataField="PHONE" HeaderText="Phone"></asp:BoundField>
                                            <asp:BoundField DataField="ref" HeaderText="Reference"></asp:BoundField>
                                            <asp:BoundField DataField="arch" HeaderText="Architect"></asp:BoundField>
                                            <asp:BoundField DataField="OutHOUR" HeaderText="Out Hour"></asp:BoundField>
                                            <asp:BoundField DataField="OutMIN" HeaderText="Out Min"></asp:BoundField>
                                            <asp:BoundField DataField="OutAMPM" HeaderText="Out AMPM"></asp:BoundField>
                                            <asp:BoundField DataField ="UploadDoc" HeaderText ="Attachmnets" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <span style="color: #ff0033">No Data to Display</span>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="Label6" runat="server" Text="Excutive Name"></asp:Label></td>
                    <td colspan="2" style="text-align: left">
                        <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td colspan="3"></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table align="center" style="width: 100%">
                            <tr>
                                <td style="text-align: right">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CausesValidation="false"  /></td>
                                <td>
                                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" CausesValidation="False" /></td>
                            </tr>
                        </table>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ValidationGroup="a"></asp:ValidationSummary>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3" class="profilehead">Daily Report Details</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table style="text-align: right;width: 100%" id="tblhide" runat="server" visible="false">
                            <tr>
                                <td style="text-align: left">Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
                                </td>
                                <td colspan="4">
                                    <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                        Text="Search By"></asp:Label>
                                    <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                        OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--</asp:ListItem>
                                        <asp:ListItem Value="EMP_FIRST_NAME">Executive Name</asp:ListItem>
                                        <asp:ListItem Value="DATETIME"> Date</asp:ListItem>
                                        <asp:ListItem Value="CLIENTSNAME">Client Name </asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                        EnableTheming="False" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged"
                                        Visible="False" Width="50px">
                                        <asp:ListItem Selected="True">=</asp:ListItem>
                                        <asp:ListItem>&lt;</asp:ListItem>
                                        <asp:ListItem>&gt;</asp:ListItem>
                                        <asp:ListItem>&lt;=</asp:ListItem>
                                        <asp:ListItem>&gt;=</asp:ListItem>
                                        <asp:ListItem>R</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                    </asp:Label>
                                    <asp:TextBox ID="txtSearchValueFromDate" runat="server" type="datepic" EnableTheming="True" Visible="False"
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
                                    <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                    </asp:Label>
                                    <asp:TextBox ID="txtSearchValueToDate" runat="server" type="datepic" EnableTheming="True" Visible="False"
                                        Width="106px">
                                    </asp:TextBox>
                                    <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Visible="False"></asp:TextBox><%--<asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchValueToDate" runat="server"
                                            Enabled="False" PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>--%>
                                    <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                        CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnSearchGo_Click" /></td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblUserType" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblDeptId" runat ="server" Visible ="false" ></asp:Label>
                                    <asp:Label ID="lblDeptHeadId" runat ="server" Visible ="false" ></asp:Label>
                                    <asp:Label ID="lblDeptHead" runat ="server" Visible ="false"  ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        
                        <asp:GridView ID="gvDailyRptDtls" Visible="false" runat="server" AllowPaging="True" OnPageIndexChanging="gvDailyRptDtls_PageIndexChanging" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="gvDailyRptDtls_RowDataBound">
                            <AlternatingRowStyle BackColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <br />
            <table>
                <tr>
                    <td style="text-align: right">From Date :
                    <asp:TextBox ID="txtFromDate" type="datepic" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">To Date : </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtToDate" type="datepic" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">Client Name :
                    <asp:TextBox ID="txtClientName" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">Employee Name :</td>
                    <td style="text-align: left">
                        <%--<asp:TextBox ID="txtEmp_Name" runat="server"></asp:TextBox>--%>
                        <asp:DropDownList ID="ddlSalesPerson" runat="server" AutoPostBack="True"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center" colspan="5">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CausesValidation="False" />
                    </td>
                </tr>
            </table>
            <br />
            <div id="grid" style="width: 100%">
                
                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" CausesValidation="False" />
                <asp:Button ID="btnPostComment" runat="server" Text="Post Comment" OnClick="btnPostComment_Click" CausesValidation="False" />
                <asp:Button ID="btnFollowUp" runat="server" CausesValidation="False" Text="Follow Up" OnClick="btnFollowUp_Click"  />
                <asp:Button ID="runDailyReport" runat="server" OnClick="runDailyReport_Click" Text="Run Report" CausesValidation="False" />
                <asp:GridView ID="gvDrs" Width="100%" runat="server" SelectedRowStyle-BackColor="#c0c0c0" EmptyDataText="No Records To Display" AllowPaging="True" OnPageIndexChanging="gvDrs_PageIndexChanging" PageSize="8" AutoGenerateColumns="False" OnRowDataBound="gvDrs_RowDataBound">
                    <Columns>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkhdr" runat="server" AutoPostBack="True" OnClick="selectAll(this)" />
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="Chk" OnCheckedChanged="Chk_CheckedChanged" AutoPostBack="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Form Id" SortExpression="DAILYREPORTID" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblId" Text='<%#Eval("DAILYREPORTID")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                        </asp:BoundField>

                        <asp:BoundField DataField="In TIME" HeaderText="In TIME" SortExpression="In TIME">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Out TIME" HeaderText="Out TIME" SortExpression="Out TIME">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Client Name" HeaderText="Client" SortExpression="Client Name" ItemStyle-Wrap="false">
                            
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle" Wrap="true" />
                        </asp:BoundField>

                        <asp:BoundField DataField="PHONE" HeaderText="PHONE" SortExpression="PHONE">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Reference" HeaderText="Reference" SortExpression="Reference">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                        </asp:BoundField>

                        <asp:BoundField DataField="Architect" HeaderText="Architect" SortExpression="Architect">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                        </asp:BoundField>

                        <asp:BoundField DataField="ADDRESS" HeaderText="Address" ItemStyle-Wrap="false" SortExpression="ADDRESS">
                            
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                        </asp:BoundField>

                        <asp:BoundField DataField="PURPOSE" HeaderText="Purpose" ItemStyle-Wrap="false" SortExpression="PURPOSE">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                        </asp:BoundField>

                        <asp:BoundField DataField="REMARKS" HeaderText="Remarks"  ItemStyle-Wrap="false" SortExpression="REMARKS">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                        </asp:BoundField>

                        <asp:BoundField DataField="Attended By" HeaderText="Attended By" SortExpression="Attended By" ItemStyle-Wrap="false">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Executive Name" HeaderText="Executive Name" SortExpression="Executive Name" ItemStyle-Wrap="false">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Comments">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtComment" TextMode="SingleLine" Width="150px" Text='<%#Eval("Comments")%>' runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        <asp:BoundField DataField ="DAILYREPORTID" HeaderText ="Id" />
                        <asp:TemplateField HeaderText="FileName" >
                                                <ItemTemplate>
                                                    <asp:Image runat ="server" EnableTheming="False"  ImageUrl = '<%# Eval("FileName","http://183.82.108.55/YANTRA_DOCUMENTS/SOFiles/{0}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="Silver"></SelectedRowStyle>
                </asp:GridView>
                <table id="Table1" align="center">
                    
                    <tr>
            <td>
                <table id="tblFollowUp" runat="server" border="0" cellpadding="0" cellspacing="0"
                    visible="false" width="100%">
                    <tr>
                        <td class="profilehead" colspan="2">follow up details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Clients'Name : 
                            <asp:TextBox ID="txtCustName" runat="server" ></asp:TextBox>
                        </td>
                        <td style="text-align: left">Reference : 
                            <asp:TextBox ID="txtRef" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Date : 
                            <asp:TextBox ID="txtDate" runat ="server" Type="date" ></asp:TextBox>
                        </td>
                        <td style="text-align: left">Adddress : 
                            <asp:TextBox ID="txtAdd" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Purpose :</td>
                        <td style="text-align: left" >
                            <asp:TextBox ID="txtPurps" runat="server" onkeypress="return check(event)" TextMode="MultiLine" Width="560px" EnableTheming="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Remarks</td>
                        <td style="text-align: left" >
                            <asp:TextBox ID="txtRemrks" runat="server" onkeypress="return check(event)" TextMode="MultiLine" Width="560px" EnableTheming="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Follow Up Description
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtFollowUpDesc" runat="server" CssClass="multilinetext" EnableTheming="False"
                                Height="40px" TextMode="MultiLine" Width="560px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"> Commented By</td>
                        <td><asp:DropDownList ID="ddlCommentedBy" runat="server" Enabled="False"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table id="Table2" align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnFollowUpSave" runat="server" CausesValidation="False" OnClick="btnFollowUpSave_Click"
                                            Text="Save" /></td>
                                    <%--<td>
                                        <asp:Button ID="btnFollowUpRefresh" runat="server" CausesValidation="False" OnClick="btnFollowUpRefresh_Click"
                                            Text="Refresh" /></td>
                                    <td>
                                        <asp:Button ID="btnFollowUpHistory" runat="server" CausesValidation="False" OnClick="btnFollowUpHistory_Click"
                                            Text="History" /></td>
                                    <td>
                                        <asp:Button ID="btnFollowUpClose" runat="server" CausesValidation="False" OnClick="btnFollowUpClose_Click"
                                            Text="Close" /></td>--%>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table id="tblFollowUpHistory" runat="server" border="0" cellpadding="0" cellspacing="0"
                                width="100%" >
                                <tr>
                                    <td class="profilehead" colspan="3">Follow Up History</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvFollowUp" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            DataSourceID="sdsFollowUp" Width="100%" OnRowDataBound ="gvFollowUp_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="DATETIME" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date"
                                                    HtmlEncode="False" SortExpression="FU_DATE" />
                                                <asp:BoundField DataField="CLIENTSNAME" HeaderText="Client's Name" SortExpression="CLIENTSNAME">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="REFERENCE" HeaderText="Reference" SortExpression="REFERENCE">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PURPOSE" HeaderText="Purpose" SortExpression="PURPOSE">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="REMARKS" HeaderText="Comments" SortExpression="REMARKS">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Comments" HeaderText="Description" SortExpression="Comments">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Emp_id" HeaderText="Commented By" SortExpression="Emp_id">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="USER_NAME" HeaderText="Commented By" SortExpression="USER_NAME">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="USER_TYPE" HeaderText="Commented By" SortExpression="USER_TYPE">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                            </Columns>
                                            <SelectedRowStyle BackColor="LightSteelBlue" />
                                        </asp:GridView>
                                        <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                                        <br />
                                        <asp:SqlDataSource ID="sdsFollowUp" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                            SelectCommand="select [YANTRA_DAILY_REPORT_DET].[DATETIME],[YANTRA_DAILY_REPORT_DET].CLIENTSNAME,[YANTRA_DAILY_REPORT_DET].[REFERENCE],[YANTRA_DAILY_REPORT_DET].[PURPOSE],[YANTRA_DAILY_REPORT_DET].[REMARKS],[YANTRA_DAILY_REPORT_DET].[Comments],[Emp_id],[USER_NAME],[USER_TYPE] 
from [YANTRA_DAILY_REPORT_DET] &#13;&#10; inner join [YANTRA_USER_DETAILS ] on [YANTRA_DAILY_REPORT_DET] .[CommentedBy]=[YANTRA_USER_DETAILS] .[Emp_id] where [YANTRA_DAILY_REPORT_DET].[DAILYREPORTID]=@ASSIGNID&#13;&#10; order by [YANTRA_DAILY_REPORT_DET].[DAILYREPORTDET_ID] desc ">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="lblId" DefaultValue="0"
                                                    Name="ASSIGNID" PropertyName="Text" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
                </table>
            </div>
            </asp:Panel> 
             
            <asp:Panel runat ="server" ID ="ReturnNote" Visible ="false"   >
                 <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
               <table style ="width :100%" >
                   <tr>
                        <td colspan ="2">
                            <asp:GridView ID="gvlist" runat ="server" OnRowDataBound ="gvlist_RowDataBound" DataSourceID="sqlPISearchDetails" AllowPaging ="true" AutoGenerateColumns ="false"  >
                    <Columns >
                        <asp:BoundField DataField="CIR_ID" SortExpression="CIR_ID" HeaderText="CIR_ID" meta:resourceKey="BoundFieldResource1" />
                        <asp:TemplateField SortExpression="CIR_NO" HeaderText="Subject" meta:resourceKey="TemplateFieldResource1">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("CIR_NO") %>' ForeColor="#0066ff" ID="TextBox1" meta:resourceKey="TextBox1Resource1">"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnPINO" runat="server" OnClick="lbtnPINO_Click" ForeColor="#0066ff" Text='<%# Eval("CIR_NO") %>' CausesValidation="False" meta:resourcekey="lbtnPINOResource1" __designer:wfdid="w1"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="CIR_DATE" SortExpression="CIR_DATE" HeaderText="CIR_DATE" meta:resourceKey="BoundFieldResource2">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="DESCRIPTION" SortExpression="DESCRIPTION" HeaderText="DESCRIPTION" meta:resourceKey="BoundFieldResource3">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                         <asp:BoundField DataField="Company_ID" SortExpression="Company_ID" HeaderText="Company_ID" meta:resourceKey="BoundFieldResource4">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PreparedBy" SortExpression="PreparedBy" HeaderText="PreparedBy" meta:resourceKey="BoundFieldResource4">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="EMP_FIRST_NAME" SortExpression="EMP_FIRST_NAME" HeaderText="Reporting To" meta:resourceKey="BoundFieldResource11" />
                    
                    </Columns>
                </asp:GridView>
                 <asp:SqlDataSource ID="sqlPISearchDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="[SP_TO_DO_LIST]" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="EMPID" ControlID="lblEmpIdHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>
                        <%--<asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="CPID" ControlID="lblCPID" />--%>
                    </SelectParameters>
                </asp:SqlDataSource>
                        </td>
                   </tr>
               </table>
                 <table style="width:100%;" id="tbldet" runat ="server" >
                    
                   <tr>
            <td colspan="2" style="height: 160px; text-align: center">
                <table id="tblemp" runat="server" width="100%">
                    <tr>
                        <td class="profilehead" colspan="4">&nbsp;Employee Details
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label17" Visible ="false"  runat="server" Text="Company Name :"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlCompanyid" Visible ="false"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompanyid_SelectedIndexChanged">
                            </asp:DropDownList></td>

                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label18" runat="server" Text="Department :"></asp:Label></td>
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
                            <asp:Label ID="Label19" Visible="false" runat="server" Text="Employe Name :"></asp:Label>
                            <asp:Label ID="Label20" runat="server" Visible="false" Text="Designation :"></asp:Label>

                        </td>


                        <td style="text-align: left">
                            <asp:TextBox ID="txtDesignation" Visible="false" runat="server">
                            </asp:TextBox>
                            <asp:DropDownList ID="ddlEmployee" Visible="false" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                            </asp:DropDownList>

                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label21" runat="server" Visible="false" Text="Mobile No :"></asp:Label>

                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtMobileno" Visible="false" runat="server">
                            </asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right"><asp:Label ID="Label22" runat ="server" Visible ="false" ></asp:Label></td>
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
                            <asp:Label ID="Label23" runat="server" Text="Subject :"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtcirNo" runat="server" TextMode ="MultiLine"  EnableTheming="True"></asp:TextBox></td>
                         <td style="text-align: right">
                            <asp:Label ID="Label24" runat="server" Text="Issued Date :"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtcirdate" runat="server" type="datepic">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right" valign="top">
                            <asp:Label ID="lblDesignationName" runat="server" Height="22px" Text="Task Activity Description :"
                                Width="80px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtdescription" runat="server" EnableTheming="True" TextMode="MultiLine" Width="500px"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label25" runat="server" Text="Task Activity Status:"></asp:Label>
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
                                        <asp:Button ID="btnSave1" runat="server" Text="Save" OnClick ="btnSave1_Click" /></td>
                                    <td>
                                        <asp:Button ID="Button2" runat="server" CausesValidation="False" 
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
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                    ShowSummary="False"></asp:ValidationSummary>
                

            </td>
        </tr>
                </table>
                </asp:View>
        </asp:MultiView>
             </asp:Panel>
    
</asp:Content>


 
