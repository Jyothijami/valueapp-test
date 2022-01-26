<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true" CodeFile="Emp_InterviewResult.aspx.cs" Inherits="Modules_HR_Emp_InterviewResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">


    <table class="pagehead" style="width: 100%">
        <tr>
            <td colspan="4" style="text-align: left;">Interview Details </td>
            <%--<td><asp:Button ID="btnEmpEnrollment" runat="server" Text="Employee Enrollment " PostBackUrl="~/Modules/HR/EmployeeEnrollment.aspx" /></td>
            <td><asp:Button ID="btnEmpInterview" runat="server" Text="Employee Interview" PostBackUrl="~/Modules/HR/Emp_Interview.aspx" /></td>
            <td><asp:Button ID="btnIntSch" runat="server" Text="Employee Offer Letter " PostBackUrl="~/Modules/HR/Emp_OfferLetter.aspx" /></td>--%>
        </tr>
    </table>
    <br />
    <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left; width: 100%;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">1st-Interview  Details</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; width: 978px;">
                <asp:GridView ID="gv1st" AutoGenerateColumns="False" runat="server" EmptyDataText="No Records Found" DataSourceID="SqlDataSource3" Width="100%">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkhdr1" runat="server" AutoPostBack="True" OnCheckedChanged="chkhdr1_CheckedChanged" />
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="Chk1" AutoPostBack="true" OnCheckedChanged="Chk1_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Enrollment Id" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblNo" Text='<%#Eval("EnrollmentId")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="First Name">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblFirstName" Text='<%#Eval("FirstName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last Name">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblLastName" Text='<%# Eval("LastName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MobileNo">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblMobileNo" Text='<%#Eval("MobileNo")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EmailId">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblEmail" Text='<%#Eval("EmailId")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Education">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblEducation" Text='<%# Eval("Education") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Interviewer Name">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblInterviewerName" Text='<%#Eval("InterviewerName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date Of Interview">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDateOfInterview" Text='<%#Eval("DateOfInterview")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Location">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblLocation" Text='<%# Eval("Location") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Age" Visible="true">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblAge" Text='<%# Eval("Age") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks" Visible="true">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblRemarks" Text='<%# Eval("Remarks") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Interview Type" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblType" Text='<%# Eval("InterviewType") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>

            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td style="width: 58px; text-align: right;">
                <asp:Button ID="btnAccept1" runat="server" Text="Accept" CausesValidation="False" OnClick="btnAccept1_Click" Visible="false" />
            </td>
            <td style="width: 58px">
                <asp:Button ID="btnReject1" runat="server" Text="Reject" CausesValidation="False" OnClick="btnReject2_Click" Visible="false" /></td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <br />

    <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left; width: 100%;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">2nd-Interview  Details</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; width: 978px;">
                <asp:GridView ID="gvEnrollmentDtls" AutoGenerateColumns="False" runat="server" EmptyDataText="No Records Found" DataSourceID="SqlDataSource1" Width="100%">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkhdr" runat="server" AutoPostBack="True" OnCheckedChanged="chkhdr_CheckedChanged" />
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="Chk" AutoPostBack="true" OnCheckedChanged="Chk_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Enrollment Id" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblNo" Text='<%#Eval("EnrollmentId")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="First Name">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblFirstName" Text='<%#Eval("FirstName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last Name">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblLastName" Text='<%# Eval("LastName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MobileNo">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblMobileNo" Text='<%#Eval("MobileNo")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EmailId">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblEmail" Text='<%#Eval("EmailId")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Education">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblEducation" Text='<%# Eval("Education") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Interviewer Name">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblInterviewerName" Text='<%#Eval("InterviewerName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date Of Interview">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDateOfInterview" Text='<%#Eval("DateOfInterview")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Location">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblLocation" Text='<%# Eval("Location") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Age" Visible="true">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblAge" Text='<%# Eval("Age") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks" Visible="true">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblRemarks" Text='<%# Eval("Remarks") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Interview Type" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblType" Text='<%# Eval("InterviewType") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>

            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td style="width: 58px; text-align: right;">
                <asp:Button ID="btnAccept" runat="server" Text="Accept" CausesValidation="False" OnClick="btnAccept_Click" Visible="false" />
            </td>
            <td style="width: 58px">
                <asp:Button ID="btnReject" runat="server" Text="Reject" CausesValidation="False" OnClick="btnReject_Click" Visible="false" /></td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT a.[EnrollmentId], a.[FirstName], a.[LastName], a.[MobileNo], a.[EmailId],
   a.[Education],b.[InterviewerName], convert(varchar(12), b.[DateOfInterview], 103) as DateOfInterview , b.[Location],a.Age,b.InterviewType,b.Remarks
    FROM [Emp_Enrollment] a inner join [Emp_InterviewDetails] b 
    on a.[EnrollmentId]=b.[EnrollmentId] where  b.InterviewStatus='Pending' and b.InterviewType =2   order by b.[EnrollmentId] "></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT a.[EnrollmentId], a.[FirstName], a.[LastName], a.[MobileNo], a.[EmailId],
   a.[Education],b.[InterviewerName], convert(varchar(12), b.[DateOfInterview], 103) as DateOfInterview , b.[Location],a.Age,b.InterviewType,b.Remarks
    FROM [Emp_Enrollment] a inner join [Emp_InterviewDetails] b 
    on a.[EnrollmentId]=b.[EnrollmentId] where  b.InterviewStatus1='Pending' and b.InterviewType =1   order by b.[EnrollmentId] "></asp:SqlDataSource>

    <br />
    <div id="Details" runat="server" visible="false">
        <table style="width: 100%;">
            <tr>
                <td class="profilehead" style="width: 100%;">Salary Details
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>&nbsp;</td>
                <td></td>
            </tr>
        </table>
       
       <table>
                     <tr>
                 <td>

                     Enter Annual Amount:</td>
                 <td>

                     <asp:TextBox ID="txtGrossAmount" runat="server">
                     </asp:TextBox>

                 </td>             
             </tr>
             <tr>
                 <td>

                 </td>
                 <td>

                     <asp:Button ID="btnCal" runat="server" OnClick="btnCal_Click" Text="Calculate" />

                 </td>             
             </tr>
         </table>
         <br />
         <h3>Earnings</h3>
         <table cellpadding="5" cellspacing="0">
             <tr>
                 <td style="width: 150px">Basic</td>
                 <td>
                     <asp:TextBox ID="txtBasic1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td>
                     <asp:TextBox ID="txtBasic2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td style="width: 150px">HRA</td>
                 <td>
                     <asp:TextBox ID="txtHRA1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td>
                     <asp:TextBox ID="txtHRA2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td>Transport Allowance</td>
                 <td>
                     <asp:TextBox ID="txtOther1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td>
                     <asp:TextBox ID="txtOther2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td>Special Allowance</td>
                 <td>
                     <asp:TextBox ID="txtSpl1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td>
                     <asp:TextBox ID="txtSpl2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td style="border-top-style: solid; border-top-width: 1px; border-top-color: #CCCCCC"><b>Gross Total</b></td>
                 <td style="border-top-style: solid; border-top-width: 1px; border-top-color: #CCCCCC">
                     <asp:TextBox ID="txtGrossTotal1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td style="border-top-style: solid; border-top-width: 1px; border-top-color: #CCCCCC">
                     <asp:TextBox ID="txtGrossTotal2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
         </table>
         <br />

         <h3>Statutory Benefits</h3>
         <table cellpadding="5" cellspacing="0">             
             <tr>
                 <td style="width: 150px">PF</td>
                 <td>
                     <asp:TextBox ID="txtPF1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td>
                     <asp:TextBox ID="txtPF2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td>ESI<asp:Label ID="lblStatus" runat="server" Visible="False"></asp:Label>
                 </td>
                 <td>
                     <asp:TextBox ID="txtMedi1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td>
                     <asp:TextBox ID="txtMedi2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td>Performance Incentives</td>
                 <td>
                     <asp:TextBox ID="txtBonus1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td>
                     <asp:TextBox ID="txtBonus2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td style="border-top-style: solid; border-top-width: 1px; border-top-color: #CCCCCC"><strong>Statutory Total</strong></td>
                 <td style="border-top-style: solid; border-top-width: 1px; border-top-color: #CCCCCC">
                     <asp:TextBox ID="txtECTotal1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td style="border-top-style: solid; border-top-width: 1px; border-top-color: #CCCCCC">
                     <asp:TextBox ID="txtECTotal2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
         </table>
         <br />
         <h3>Other Benefits</h3>
         <table cellpadding="5" cellspacing="0">             
             <tr>
                 <td style="width: 150px">Other Benefits</td>
                 <td>
                     <asp:TextBox ID="txtoth1" runat="server">
                                                 </asp:TextBox>
                 </td>
                 <td>
                     <asp:TextBox ID="txtoth2" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
             <%--<tr>
                 <td >Ptax</td>
                 <td>
                     <asp:TextBox ID="txtEDptax" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>--%>
             <%--<tr>
                 <td style="border-top-style: solid; border-top-width: 1px; border-top-color: #CCCCCC">Total </td>
                 <td style="border-top-style: solid; border-top-width: 1px; border-top-color: #CCCCCC">
                     <asp:TextBox ID="txtEDTotal" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>--%>
         </table>
         <br />
         <table cellpadding="5" cellspacing="0">
             <tr>
                 <td style="width: 150px">Total CTC(PM)</td>
                 <td >
                     <asp:TextBox ID="txtCTCPM" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td>Total CTC(PA)</td>
                 <td >
                     <asp:TextBox ID="txtCTCPA" runat="server"></asp:TextBox>
                 </td>
             </tr>
         </table>
         <br />
         <table cellpadding="5" cellspacing="0" id="tblNet" runat="server" visible="false">
            <%-- <tr>
                 <td style="width:150px"><b>Net</b></td>
                 <td >
                     <asp:TextBox ID="txtNetSal" runat="server">
                                                 </asp:TextBox>
                 </td>
             </tr>--%>
              <tr>
                <td style="width:150px">&nbsp;</td>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />

                </td>
            </tr>
         </table>
        <%--<asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Print" Visible="False" />--%>
        <br />
    </div>
    <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
        <%-- <tr>
                 <td align="right" class="auto-style3">
                     <asp:Label ID="Label1" runat="server" Text="Employee Name :" Width="110px"></asp:Label>
                 </td>
                 <td style="text-align: left" class="auto-style4"></td>
                 <td align="right" class="auto-style3">
                     <asp:Label ID="Label2" runat="server" Text="Age :"></asp:Label>
                 </td>
                 <td style="text-align: left" class="auto-style4"><asp:Label ID="lblAge" runat="server" Visible="false"></asp:Label></td>
             </tr>
             <tr>
                 <td style="width: 100px; text-align: right;">
                     <asp:Label ID="Label3" runat="server" Text="Enter Gross Amount :" Width="131px"></asp:Label>
                 </td>
                 <td style="width: 100px; text-align: left">&nbsp;</td>
                 <td style="width: 100px; text-align: center;">&nbsp;</td>
                 <td ></td>
             </tr>--%>
        <tr>
            <td colspan="4">
                <table style="width: 100%">
                    <tr class="profilehead">
            <td style="padding-top:16px;">Employee Interview  Details :
            </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td>
                                        <%--<asp:Label ID="Label27" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label>--%>
                                        Search By &nbsp;
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="textbox">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="Status">InterviewStatus</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td></td>
                                    <td>
                                        <asp:TextBox ID="txtSearchText" runat="server" CssClass="textbox">
                                        </asp:TextBox>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                                </tr>
                            </table>
                            <asp:Label ID="lblSearchItemHidden2" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueHidden2" runat="server" Visible="False"></asp:Label>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="gvHistory" runat="server" EmptyDataText="No Records Found" DataSourceID="SqlDataSource2" Width="100%" AutoGenerateColumns="False" DataKeyNames="EnrollmentId" AllowPaging="True">
                                <Columns>
                                    <asp:BoundField DataField="EnrollmentId" HeaderText="EnrollmentId" ReadOnly="True" SortExpression="EnrollmentId" />
                                    <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                                    <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                                    <asp:BoundField DataField="MobileNo" HeaderText="MobileNo" SortExpression="MobileNo" />
                                    <asp:BoundField DataField="EmailId" HeaderText="EmailId" SortExpression="EmailId" />
                                    <asp:BoundField DataField="Education" HeaderText="Education" SortExpression="Education" />
                                    <asp:BoundField DataField="InterviewerName" HeaderText="InterviewerName" SortExpression="InterviewerName" />
                                    <asp:BoundField DataField="DateOfInterview" HeaderText="DateOfInterview" SortExpression="DateOfInterview" />
                                    <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" />
                                    <asp:BoundField DataField="InterviewStatus" HeaderText="InterviewStatus" SortExpression="InterviewStatus" />
                                </Columns>

                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                SelectCommand="USP_InterviewSearchStatus" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="lblSearchItemHidden2" Name="SearchItemName2" PropertyName="Text" Type="String" DefaultValue="0" />
                                    <asp:ControlParameter ControlID="lblSearchValueHidden2" Name="SearchValue2" PropertyName="Text" Type="String" DefaultValue="0" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>

                </table>
            </td>
        </tr>
    </table>
</asp:Content>


