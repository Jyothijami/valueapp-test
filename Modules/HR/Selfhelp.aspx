<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Selfhelp.aspx.cs" Inherits="Modules_HR_Selfhelp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <h3 style="color: #3333FF"><span style="font-weight: normal">Employee Personal Details</span></h3>
     <table class="pagehead">
        <%--<tr style="padding-left: 10px">
            <td colspan="4" style="vertical-align:top; text-align:left;">
               Employee Personal Details</td>
        </tr>--%>
         </table>
  <table>
      <tr  style="text-align:left">
          <td>
              <asp:Button ID="btnReview" runat ="server" Text ="Emp Review" Width ="160px" PostBackUrl ="~/Survey/NewEmpRating.aspx" /> 
              <br />
                <asp:Button ID="btnMemo" runat="server" Text="Memos" Width="160px" PostBackUrl="~/Modules/HR/EmpMemo1.aspx" />
              <br />
                <asp:Button ID="btnCircular" runat="server" Text="Circulars" Width="160px" PostBackUrl="~/Modules/HR/Circular1.aspx" />
              <br />
              
                <asp:Button ID="btnStaffAdv" runat="server" Enabled ="false"  Text="Salary Advance" Width="160px" PostBackUrl="~/Modules/HR/AdvanceSalaryReqForm.aspx" />
                <br />
              <asp:Button ID="btnStaffTour" runat="server" Text="Tour Advance" Width="160px" PostBackUrl="~/Modules/HR/Staff_Tour_Advance.aspx" />
                <br />
              <asp:Button ID="btnMobileAdv" runat="server" Text="Mobile Advance" Width="160px" PostBackUrl="~/Modules/HR/MobileAdvanceReqForm.aspx" />
                <br />
              <%--<asp:Button ID="btnConv_Voucher" runat="server" Text="Convenience Voucher "  Width="160px" PostBackUrl="~/Modules/HR/CV1.aspx" />--%>
              <asp:Button ID="btnConv_Voucher" runat="server" Text="Conveyance Voucher "  Width="160px" PostBackUrl="~/Modules/HR/Convenience_Voucher.aspx" />
                <br />
                <asp:Button ID="btnApplications" runat="server" Text="Applications" Width="160px" OnClick="btnApplications_Click" />
                <br />
                <div style="text-align:left">
                    <asp:Panel ID="Panel1" runat="server" Width="160px">
                        <asp:Menu ID="PanelApps" runat="server" BackColor="#F7F6F3" DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#7C6F57" StaticSubMenuIndent="10px" Width="100%" Visible="False">
                            <DynamicHoverStyle BackColor="#7C6F57" ForeColor="White" />
                            <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                            <DynamicMenuStyle BackColor="#F7F6F3" />
                            <DynamicSelectedStyle BackColor="#5D7B9D" />
                            <Items>
                                <asp:MenuItem NavigateUrl="~/Modules/HR/EmpOnDutyForm.aspx" Enabled ="false"  Text="On Duty Form" Value="On Duty Form"></asp:MenuItem>
                                <asp:MenuItem NavigateUrl="~/Modules/HR/EmpOneHourPermission.aspx" Enabled ="false"  Text="One Hour Permission" Value="One Hour Permission"></asp:MenuItem>
                                <asp:MenuItem NavigateUrl="~/Modules/HR/EmpOverTimeForm.aspx" Enabled ="false"  Text="Over Time" Value="Over Time"></asp:MenuItem>
                                <asp:MenuItem NavigateUrl="~/Modules/HR/EmpShiftChange.aspx" Text="Shift Change" Value="Shift Change"></asp:MenuItem>
                                <asp:MenuItem NavigateUrl="~/Modules/HR/LeaveApplication.aspx" Text="Apply Leave" Value="Apply Leave"></asp:MenuItem>
                                <asp:MenuItem NavigateUrl="~/Modules/HR/TicketDetails.aspx" Text="Ticket Details" Value="Ticket Details"></asp:MenuItem>
                                <asp:MenuItem NavigateUrl="~/Modules/HR/PaySlip.aspx" Text="pay Slip" Value="pay Slip"></asp:MenuItem>
                                
                            </Items>
                            <StaticHoverStyle BackColor="#7C6F57" ForeColor="White" />
                            <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                            <StaticSelectedStyle BackColor="#5D7B9D" />
                            <StaticItemTemplate>
                                <%# Eval("Text") %>
                            </StaticItemTemplate>
                        </asp:Menu>
                        <br />
                    </asp:Panel>
                </div>
          </td>
          <td>
<asp:DetailsView ID="dlt" runat="server" CellPadding="4" ForeColor="Black" GridLines="None" Height="50px" Width="500px" AutoGenerateRows="False">
         <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
         <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
         <EditRowStyle BackColor="#999999" />
         <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
         <Fields>
             <asp:TemplateField HeaderText="First Name :">
                 <ItemTemplate>
                     <asp:Label ID="lblFName" runat="server" Text='<%# Eval("First Name") %>'></asp:Label>
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="Last Name :">
                 <ItemTemplate>
                     <asp:Label ID="lblLName" runat="server" Text='<%# Eval("Last Name") %>'></asp:Label>
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="Father/Husband Name :">
                 <ItemTemplate>
                     <asp:Label ID="lblFatherName" runat="server" Text='<%# Eval("Father Name") %>'></asp:Label>
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="Date Of Birth :" >
                 <ItemTemplate>
                     <asp:Label ID="lblDOB" runat="server" Text='<%# Eval("Date Of Birth") %>'></asp:Label>
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="Mobile No :">
                 <ItemTemplate>
                     &nbsp;<asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("Mobile No") %>'></asp:Label>
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="Address">
                 <ItemTemplate>
                     &nbsp;<asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="Email :">        
                          <ItemTemplate>
                     <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                 </ItemTemplate>
             </asp:TemplateField>
         </Fields>
         <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
         <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
         <PagerStyle BackColor="#284775" ForeColor="#284775" HorizontalAlign="Left" />
         <RowStyle BackColor="#F7F6F3" ForeColor="#284775" />
     </asp:DetailsView>
          </td>
          
      </tr>
      <tr>
                        
                        <td style="text-align: center"><asp:Label ID="Label56" runat="server" Visible="false" Text="Attached File"></asp:Label>&nbsp;
                            <asp:LinkButton ID="lbtnAttachedFiles" runat="server" Visible="False"
                                OnClick="lbtnAttachedFiles_Click"></asp:LinkButton>
                            <asp:Repeater ID="UploadsRepeater" Visible="false" runat="server" DataSourceID="sdsUploads">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnFileOpener" CausesValidation="False" runat="server"
                                        OnClick="lbtnFileOpener_Click" Text='<%# Bind("Document_Submitted") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:SqlDataSource ID="sdsUploads" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                SelectCommand="SELECT * FROM [Emp_Documents_Submitted] WHERE (EMP_ID=@SO_IDpara) Order by Doc_Id Desc">
                                <SelectParameters>
                                    <asp:ControlParameter PropertyName="Text" DefaultValue="0" Name="SO_IDpara"
                                        ControlID="lblSOIdHidden"></asp:ControlParameter>
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:Label ID="lblSOIdHidden" runat="server" Visible="False"></asp:Label></td>

                    </tr>
      <tr>
          <td style="text-align: right">Attachments :</td>
          <td  style="text-align: center">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:Repeater ID="Repeater1" DataSourceID="sdsUploads" runat="server">
                                        <HeaderTemplate>
                                            <table cellspacing="0" rules="all" border="1">
                                                <tr>
                                                    <th scope="col" style="width: 120px">File Id
                                                    </th>
                                                    <th scope="col" style="width: 100px">File Name
                                                    </th>
                                                    <th scope="col" style="width: 100px">Doc Type
                                                    </th>
                                                    <%--<th scope="col" style="width: 80px"></th>--%>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_So_Upload_Id" runat="server" Text='<%# Eval("DOC_ID") %>' Visible="true" />

                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lbtnFileOpener2" CausesValidation="False" runat="server"
                                                        OnClick="lbtnFileOpener2_Click" Text='<%# Bind("Document_Submitted") %>'></asp:LinkButton>

                                                    <%--<asp:Label ID="lblCountry" runat="server" Text='<%# Eval("Country") %>' />--%>
                                            
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label26" runat="server" Text='<%# Eval("Emp_Doc_ID") %>' Visible="true" />
                                                </td>
                                               <%-- <td>
                                                    <asp:LinkButton ID="lnkDelete" OnClientClick='javascript:return confirm("Are you sure you want to delete?")'  Text="Delete" runat="server" OnClick="lnkDelete_Click" CausesValidation="false" />


                                                </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
      </tr>
      
  </table>
     
     <table style="width:50%">
        <tr>
            <td colspan="2" style="text-align:center">
                <asp:Label ID="lblEmpId" runat="server" Visible="false"></asp:Label>
     <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Edit"  />

            </td>
        </tr>
    </table>
    <br />
     <br />
     <br />
    <asp:Label ID="lblUserName" runat="server"  Visible="False"></asp:Label>
    <br />
   
  <table id="tblDetails" style="width:75%;" runat="server" visible="false">
      <tr>
          <td colspan="2" class="profilehead" >
            
                Personal Details :
       
          </td>
      </tr>
      <tr>
          <td>&nbsp;</td>
      </tr>
        <tr>
            <td>
                <asp:Label ID="lblFirstName" runat="server" Text="First Name :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblLastName2" runat="server" Text="Last Name :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblFatherName1" runat="server" Text="Parents/Spouse Name :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFatherName" runat="server"></asp:TextBox>
            </td>
            </tr>
        <tr>
            <td>
                <asp:Label ID="lblDOB" runat="server" Text="Date Of Birth :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDOB" runat="server" type="date"></asp:TextBox>
            </td>
        </tr>
            <tr>
            <td>
                <asp:Label ID="lblMobileNo1" runat="server" Text="Mobile No :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMobileNo" runat="server"></asp:TextBox>
            </td>
        </tr>
            <tr>
            <td>
                <asp:Label ID="lblAddress1" runat="server" Text="Address :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
            </td>
        </tr>
                <tr>
            <td>
                <asp:Label ID="lblEmail1" runat="server" Text="Email :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td><asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Width="160px" /></td>
        </tr>
    </table>
</asp:Content>

