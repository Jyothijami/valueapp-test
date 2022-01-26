<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true" CodeFile="Payroll_Statement.aspx.cs" Inherits="Modules_HR_Payroll" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table cellpadding="5" cellspacing="0" >
        <tr>
            <td style="width:150px; text-align :right">Location Name:</td>
                 <td><asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="locname" DataValueField="locid">
                </asp:DropDownList>
                     <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [locid], [locname] FROM [location_tbl]"></asp:SqlDataSource>
                 </td>
                 <td style="text-align :right; width:150px  ">Year</td>    
                 <td style="text-align:left"><asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True"></asp:DropDownList></td>        
                     
        </tr>
        <tr>
                 <td style="text-align :right" >Company Name : </td>
                 <td style="text-align :left "><asp:DropDownList ID="ddlComp" runat="server" AutoPostBack="True"></asp:DropDownList></td>   
                 <td style="text-align :right ">Month</td>   
                 <td>
                     <asp:DropDownList ID="ddlMonth" runat="server"  AppendDataBoundItems="True" AutoPostBack="True"  DataTextField="Month" DataValueField="ID" DataSourceID="SqlDataSource1">
                         <asp:ListItem Value="0">--Select--</asp:ListItem></asp:DropDownList>
                     <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [Month], [ID] FROM [Month_Calendar_tbl]"></asp:SqlDataSource>
                </td>       
        </tr>
         <tr>
                <td><asp:Button ID="btnPrint" runat ="server" OnClick ="btnPrint_Click" Text="Print" /></td>
                 <td><asp:Button ID="btnCalcl" runat="server" OnClick="btnCalcl_Click" Text="Calculate" /></td>       
             <td><asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click" Visible="False" /></td>    
             <td><asp:Button ID="btnExportPDF" runat ="server" OnClick ="btnExportPDF_Click" Text="Export To PDF" Visible ="false" /></td>  
             </tr>
        <tr>
                                <td colspan="4">
                                    <table id="tblpRint" runat="server" visible="false">
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkSalaryPaySheet" runat="server" OnCheckedChanged="chkSalaryPaySheet_CheckedChanged"
                                                    Text="Salary PaySheet" AutoPostBack="True"></asp:CheckBox></td>
                                            <td>
                                                <asp:CheckBox ID="chkPFSheet" runat="server" Text="PF Sheet" AutoPostBack="True" OnCheckedChanged="chkPFSheet_CheckedChanged"></asp:CheckBox></td>
                                            <td>
                                                <asp:CheckBox ID="chkESISheet" runat="server" Text="ESI Sheet" AutoPostBack="True" OnCheckedChanged="chkESISheet_CheckedChanged"></asp:CheckBox></td>
                                            <td><asp:CheckBox ID="chkBankStatement" runat ="server" Text="Bank Statement" AutoPostBack ="true" OnCheckedChanged ="chkBankStatement_CheckedChanged"/></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
    </table>
           <h4 style="text-align :left ">PAY ROLL FOR THE MONTH OF :<asp:Label ID="lblMonth" runat ="server" Font-Bold="True" ForeColor="#9900CC" ></asp:Label> - <asp:Label ID="lblYear" runat ="server" Font-Bold="True" ForeColor="#CC0099" ></asp:Label></h4> 
      <asp:GridView ID="gvPayroll" runat ="server" Width="100%" AutoGenerateColumns ="false" OnRowDataBound ="gvPayroll_RowDataBound">
          <Columns >
             <asp:TemplateField HeaderText="Emp Code">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblEmpId" Text='<%#Eval("Emp_ID")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
               <asp:TemplateField HeaderText="Employee Name">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblEmpName" Text='<%#Eval("Emp_Name")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
              <asp:TemplateField HeaderText="Total Days">
                  <HeaderStyle HorizontalAlign="Center" />
                  <ItemTemplate >
                      <asp:Label  runat ="server" ID="lblNOD" Text='<%#Eval("TotalNOD")%>'></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField>  
              <asp:TemplateField HeaderText ="Payble Days" >
                  <HeaderStyle HorizontalAlign="Center" />
                  <ItemTemplate >
                      <asp:Label  runat ="server" ID="lblPaid" Text='<%#Eval("Paid")%>'></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField>
                      
             <asp:TemplateField HeaderText="LOP">
                  <HeaderStyle HorizontalAlign="Center" />
                  <ItemTemplate >
                      <asp:Label  runat ="server" ID="lblLOP" ></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField>   
              <asp:TemplateField HeaderText="BASIC">
                  <HeaderStyle HorizontalAlign="Center" />
                  <ItemTemplate >
                      <asp:Label  runat ="server" ID="lblBasic" Text='<%#Eval("Basic")%>'></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField>      
              <asp:TemplateField HeaderText="HRA">
                  <HeaderStyle HorizontalAlign="Center" />
                  <ItemTemplate >
                      <asp:Label  runat ="server" ID="lblHRA" Text='<%#Eval("HRA")%>'></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField> 
              <asp:TemplateField HeaderText="Conveyance Allow">
                  <HeaderStyle HorizontalAlign="Center" />
                  <ItemTemplate >
                      <asp:Label  runat ="server" ID="lblCV" Text='<%#Eval("Conveyance")%>'></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField>   
              <asp:TemplateField HeaderText="Medical Allow">
                  <HeaderStyle HorizontalAlign="Center" />
                  <ItemTemplate >
                      <asp:Label  runat ="server" ID="lblMedical" Text='<%#Eval("Medical")%>'></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField> 
              <asp:TemplateField HeaderText="Other Allow">
                  <HeaderStyle HorizontalAlign="Center" />
                  <ItemTemplate >
                      <asp:Label  runat ="server" ID="lblOther" Text='<%#Eval("Other_Allow")%>'></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField> 
              <asp:TemplateField HeaderText="Gross Amount">
                  <HeaderStyle HorizontalAlign="Center" />
                  <ItemTemplate >
                      <asp:Label  runat ="server" ID="lblGross" ></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField> 
              <asp:TemplateField HeaderText="PF">
                  <HeaderStyle HorizontalAlign="Center" />
                  <ItemTemplate >
                      <asp:Label  runat ="server" ID="lblPF" Text='<%#Eval("PF")%>'></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField> 
              <asp:TemplateField HeaderText="Prof.Tax">
                  <HeaderStyle HorizontalAlign="Center" />
                  <ItemTemplate >
                      <asp:Label  runat ="server" ID="lblPTax" Text='<%#Eval("PTax")%>'></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField> 
              <asp:TemplateField HeaderText="ESI">
                  <HeaderStyle HorizontalAlign="Center" />
                  <ItemTemplate >
                      <asp:Label  runat ="server" ID="lblESI" ></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField> 
              <asp:TemplateField HeaderText="TDS">
                  <HeaderStyle HorizontalAlign="Center" />
                  <ItemTemplate >
                      <asp:Label  runat ="server" ID="lblTDS" Text='<%#Eval("TDS")%>'></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField> 
              <asp:TemplateField HeaderText="Other Deductions">
                  <HeaderStyle HorizontalAlign="Center" />
                  <ItemTemplate >
                      <asp:Label  runat ="server" ID="lblOD" Text='<%#Eval("OtherDedc")%>'></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField> 
              <asp:TemplateField HeaderText="Sal.Adv">
                  <HeaderStyle HorizontalAlign="Center" />
                  <ItemTemplate >
                      <asp:Label  runat ="server" ID="lblSalAdv" Text='<%#Eval("Sal_Advance")%>'></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField> 
              <asp:TemplateField HeaderText="Total Deductions">
                  <HeaderStyle HorizontalAlign="Center" />
                  <ItemTemplate >
                      <asp:Label  runat ="server" ID="lblTotlDedu" ></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField> 
              <asp:TemplateField HeaderText="Net Amount">
                  <HeaderStyle HorizontalAlign="Center" />
                  <ItemTemplate >
                      <asp:Label  runat ="server" ID="lnlNet" ></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField> 
          </Columns>
      </asp:GridView>
</asp:Content>




