<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="empProfile.aspx.cs" Inherits="Modules_Profiles_empProfile" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    
    <h3 style="color: #3333FF"><span style="font-weight: normal">Employee Profile Details</span></h3>

    <table>
      <tr  style="text-align:left">
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
  </table>
    <table style="z-index: 1; left: 755px; top: 210px; position: absolute; height: 62px; width: 107px">
        <tr>
            <td style="text-align: left">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/noimage400x300.gif" Width="200px"></asp:Image>
            </td>
        </tr>
    </table>

    <table>
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:Label ID="lblEmpId" runat="server" Visible="false"></asp:Label>
            </td>
            
        </tr>
    </table>
    <%--<a href="#" onclick="window.open('/sendMessage.aspx?uid=<%= Request.QueryString["id"] %>', 'Send Message', 'width=550,height=450');">
        Personal Message
    </a>--%>
    <table>
        <tr>
            <td>
                <%--Change Profile Pic :--%>
                <asp:FileUpload ID="fileProfilePic" visible="false" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnProfileChange" runat="server" visible="false" Text="Upload" OnClick="btnProfileChange_Click" />
            </td>
        </tr>
    </table>
</asp:Content>


 
