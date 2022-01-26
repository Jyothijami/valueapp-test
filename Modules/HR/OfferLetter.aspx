<%@ Page Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true" CodeFile="OfferLetter.aspx.cs" Inherits="Modules_HR_OfferLetter" Title="HR || OfferLetter" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td class="searchhead" colspan="2">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left;width:50%">
                            &nbsp;Offer Letter &nbsp;</td>
                        <td>
                        </td>
                      <%--  <td>
                              <asp:Label id="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td style="text-align:right">
                          
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:Label id="Label27" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList id="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="APP_ID">Sl No</asp:ListItem>
                                            <asp:ListItem Value="DEPT_NAME">Department Name</asp:ListItem>
                                            <asp:ListItem Value="CP_FULL_NAME">Company Name</asp:ListItem>
                                            <%--<asp:ListItem Value="OfferStatus">Status</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:TextBox id="txtSearchText" runat="server" CssClass="textbox">
                                    </asp:TextBox>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Button id="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" onclick="btnSearchGo_Click" Text="Go" /></td>
                                </tr>
                                </table>
                            
                            </td>--%>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                &nbsp;

                <asp:GridView ID="gvOfferLetter" AutoGenerateColumns="False" runat="server" EmptyDataText="No Records Found" DataSourceID="SqlDataSource1" Width="100%" OnRowCommand="gvOfferLetter_RowCommand" AllowSorting="True">
                    <Columns>
                        
                        <asp:TemplateField HeaderText="Enrollment Id" Visible="false" >
                                <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblNo" Text='<%#Eval("EnrollmentId")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="First Name" SortExpression="FirstName">
                                <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblFirstName" Text='<%#Eval("FirstName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last Name" SortExpression="LastName">
                                <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblLastName" Text='<%# Eval("LastName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mobile Number" SortExpression="MobileNo">
                                <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblMobileNo" Text='<%#Eval("MobileNo")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email Id" SortExpression="EmailId">
                                <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblEmail" Text='<%#Eval("EmailId")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Company Name" SortExpression="CompanyName">
                                <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCompanyName" Text='<%#Eval("CompanyName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                 
                        <asp:TemplateField HeaderText="Date Of Joining" SortExpression="DOJ">
                                <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDateOfJoining" Text='<%#Eval("DOJ")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department" SortExpression="Department">
                                <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDepartment" Text='<%# Eval("Department") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Designation" SortExpression="Designation">
                                <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDesignation" Text='<%# Eval("Designation") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>       
                        
                        <asp:TemplateField >
                           <ItemTemplate>
                               <asp:Button ID="btnApprove" runat="server" Text="Approve" CommandArgument='<%# Eval("EnrollmentId") %>' CommandName="Approve" Width="80px" />
                           </ItemTemplate>
                        </asp:TemplateField>    
                           <asp:TemplateField >
                         <ItemTemplate>
                               <asp:Button ID="btnReject" runat="server" Text="Reject" CommandArgument='<%# Eval("EnrollmentId") %>' CommandName="Reject" Width="80px" />
                         </ItemTemplate>
                        </asp:TemplateField>                 
                    </Columns>
                </asp:GridView>



                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT a.[EnrollmentId], a.[FirstName], a.[LastName], a.[MobileNo],
                     a.[EmailId], b.[CompanyName],convert(varchar(10),b.[DOJ],103) as DOJ ,b.[Department],b.[Designation] FROM [Emp_Enrollment] a inner join [Emp_OfferLetterDetails] b 
    on a.[EnrollmentId]=b.[EnrollmentId] where b.[OfferStatus]='Pending'"></asp:SqlDataSource>






             <%--   <asp:GridView id="gvCircular" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="SqlDataSource1" OnRowDataBound="gvCircular_RowDataBound"
                    Width="100%"><columns>
<asp:BoundField DataField="APP_ID" HeaderText="S.No">
<ItemStyle HorizontalAlign="Center"></ItemStyle>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="APP_NO"><ItemTemplate>
<asp:LinkButton id="lbtnCirNo" onclick="lbtnCirNo_Click" runat="server" ForeColor="Blue" Text='<%# Bind("APP_NO") %>' __designer:wfdid="w4"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="EMP_NAME" HeaderText="EMP NAME"></asp:BoundField>
<asp:BoundField ReadOnly="True" DataField="DEPT_NAME" HeaderText="DEPARTMENT NAME">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CP_FULL_NAME" HeaderText="COMPANY NAME">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
   <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<%--<asp:BoundField DataField="OfferStatus" HeaderText="Status">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
</columns>
                    <emptydatatemplate>
                    No Record Found
                
</emptydatatemplate>
                    <selectedrowstyle backcolor="LightSteelBlue" />
                </asp:GridView>
                <asp:SqlDataSource id="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_HR_OFFER_SEARCH_SELECT" SelectCommandType="StoredProcedure"><selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
</selectparameters></asp:SqlDataSource>&nbsp;--%>
                    <%--#########--%>
                  <%--  </asp:GridView><asp:SqlDataSource id="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="[SP_HR_OFFER_SEARCH_SELECTWITHSTATUS]" SelectCommandType="StoredProcedure"><selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
</selectparameters></asp:SqlDataSource>&nbsp;--%>
            </td>
        </tr>
       <%-- <tr>
            <td id="tblempInfo" colspan="2" style="text-align: center">
                <table id="Table1">
                    <tr>
                        <td>
                            <asp:Button id="btnNew" runat="server" CausesValidation="False" onclick="btnNew_Click"
                                Text="New" /></td>
                        <td>
                            <asp:Button id="btnEdit" runat="server" CausesValidation="False" onclick="btnEdit_Click"
                                Text="Edit" /></td>
                        <td style="width: 58px">
                            <asp:Button id="btnDelete" runat="server" CausesValidation="False" onclick="btnDelete_Click"
                                Text="Delete" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <table id="tblemp" runat="server" visible="false" width="100%">
                    <tr>
                        <td class="profilehead" colspan="4">
                            Employee Details
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label2" runat="server" Text="Company Name "></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlCompanyid" runat="server" AutoPostBack="True">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label id="Label3" runat="server" Text="Department"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlDept" runat="server" AutoPostBack="True" Font-Bold="False">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label1" runat="server" Text="Employe Name "></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtempname" runat="server"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label id="Label4" runat="server" Text="Designation "></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtDesignation" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label5" runat="server" Text="Mobile No "></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtMobileno" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label28" runat="server" Text="Email :"></asp:Label>
                            </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                            </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td id="Image1" colspan="2">
                <table id="tblcircular" runat="server" border="0" cellpadding="0" cellspacing="0"
                    visible="false" width="100%">
                    <tr>
                        <td class="profilehead" colspan="2" style="text-align: left">
                            offer &nbsp;Details
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label7" runat="server" Text="App  No"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtcirNo" runat="server" EnableTheming="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label6" runat="server" Text="Issued Date "></asp:Label>&nbsp;</td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtDate" runat="server">
                            </asp:TextBox><asp:Image id="imgInstallDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image></td>
                    </tr>
                    <tr>
                        <td style="text-align: right" valign="top">
                            <asp:Label id="lblDesignationName" runat="server" Height="25px" Text="Description"
                                Width="140px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtdescription" runat="server" CssClass="textbox" EnableTheming="False"
                                Height="315px" MaxLength="20" TextMode="MultiLine" Width="500px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 49px; text-align: center">
                            <table id="tblButtons">
                                <tr>
                                    <td>
                                        <asp:Button id="btnSave" runat="server" onclick="btnSave_Click" Text="Save" /></td>
                                    <td>
                                        <asp:Button id="btnRefresh" runat="server" CausesValidation="False" onclick="btnRefresh_Click"
                                            Text="Refresh" /></td>
                                    <td style="width: 52px">
                                        <asp:Button id="btnClose" runat="server" CausesValidation="False" onclick="btnClose_Click"
                                            Text="Close" /></td>
                                    <td style="width: 18px">
                                        <asp:Button id="btnPrint" runat="server" CausesValidation="False" onclick="btnPrint_Click"
                                            Text="Email" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:ValidationSummary id="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False">
                </asp:ValidationSummary>
                <cc1:calendarextender id="ceDateOfIssued" runat="server" format="MM/dd/yyyy" popupbuttonid="imgInstallDate"
                    targetcontrolid="txtDate"></cc1:calendarextender>
                <cc1:maskededitextender id="MaskedEditInstallDate" runat="server" displaymoney="Left"
                    mask="99/99/9999" masktype="Date" targetcontrolid="txtDate" userdateformat="MonthDayYear"></cc1:maskededitextender>
                &nbsp; &nbsp; &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 153px">
            </td>
            <td style="width: 9px">
            </td>
        </tr>--%>
    </table>
</asp:Content>



