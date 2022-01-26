<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/ServicesMP1.master" AutoEventWireup="true" CodeFile="Services_Dashboard.aspx.cs" Inherits="dboards_Services_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
     <table style="width:100%;" cellpadding="5" cellspacing="5">
        <tr>
            <td style="vertical-align: top; width: 33%;">
    <asp:GridView ID="GridView8" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="CR_ID" DataSourceID="Complaintsds" PageSize="5" Width="100%" OnRowDataBound="GridView8_RowDataBound">
                    <Columns>
                           <asp:TemplateField HeaderText="COMPLAINT REGISTER NO" SortExpression="CR_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("CR_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink7" runat="server" Target="_blank" NavigateUrl='<%# Eval("CR_ID", "~/Modules/Services/ComplaintRegister.aspx") %>' Text='<%# Eval("CR_NO") %>'></asp:HyperLink>

                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:BoundField DataField="CR_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="COMPLAINT DATE" SortExpression="CR_DATE" />
                        <asp:TemplateField HeaderText=" STATUS" SortExpression="CR_STATUS">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("CR_STATUS") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCompStatus" runat="server" Text='<%# Bind("CR_STATUS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="Complaintsds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" 
                    SelectCommand="select CR_ID, CR_NO, CR_DATE, CR_STATUS from YANTRA_COMPLAINT_REGISTER order by CR_ID DESC"></asp:SqlDataSource>
                </td>
            <td style="vertical-align: top; width: 33%;">
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Cust_Id" DataSourceID="ServiceCustsds" PageSize="5" Width="100%" >
                    <Columns>
                         <asp:TemplateField HeaderText="CUSTOMER NAME" SortExpression="Cust_Name">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Cust_Id") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" NavigateUrl='<%# Eval("Cust_Id", "~/Modules/Services/ServiceCustomerInformation.aspx?CustCode={0}") %>' Text='<%# Eval("Cust_Name") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField HeaderText="DC NO" SortExpression="Cust_Company_Name">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Cust_Id") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" NavigateUrl='<%# Eval("Cust_Id", "~/Modules/Services/ServiceCustomerInformation.aspx?CustCode={0}") %>' Text='<%# Eval("Cust_Company_Name") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>  
                         <asp:BoundField DataField="Cust_Contact_Person" HeaderText=" CONTACT PERSON " SortExpression="Cust_Contact_Person" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="ServiceCustsds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" 
                    SelectCommand="select Cust_Id, Cust_Name, Cust_Company_Name, Cust_Contact_Person from Service_Customer_Information order by Cust_Id DESC"></asp:SqlDataSource>
                </td>
                        <td style="vertical-align: top; width: 33%;">
                            <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Client_Id" DataSourceID="ServiceRepsds" PageSize="5" Width="100%" >
                    <Columns>
                         <asp:TemplateField HeaderText="CLIENT NAME" SortExpression="Client_Name">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Client_Id") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" NavigateUrl='<%# Eval("Client_Id", "~/Modules/Services/Site_Inspection_Report_Details.aspx?ClientID={0}") %>' Text='<%# Eval("Client_Name") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>  
                         <asp:BoundField DataField="Quotation_Date" DataFormatString="{0:dd/MM/yyyy}" HeaderText=" Quotation Date " SortExpression="Quotation_Date" />
                        
                         <asp:BoundField DataField="Executive_Name" HeaderText=" EXECUTIVE NAME " SortExpression="Executive_Name" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="ServiceRepsds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" 
                    SelectCommand="select Client_Id, Client_Name, Quotation_Date, Executive_Name from Site_Inspection_Report_tbl order by Client_Id desc"></asp:SqlDataSource>
                </td>
            </tr>
         <tr>
                         <td style="vertical-align: top; width: 33%;">
                            <asp:GridView ID="GridView3" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SR_ID" DataSourceID="SRsds" OnRowDataBound="GridView3_RowDataBound" PageSize="5" Width="100%" >
                    <Columns>
                         <asp:TemplateField HeaderText="CLIENT NAME" SortExpression="SR_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("SR_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" NavigateUrl='<%# Eval("SR_ID", "~/Modules/Services/ServiceReportNew.aspx?srNo={0}") %>' Text='<%# Eval("SR_NO") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>  
                         <asp:BoundField DataField="SR_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText=" SR Date " SortExpression="SR_DATE" />
                        
                         <asp:TemplateField HeaderText=" STATUS" SortExpression="SR_STATUS">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("SR_STATUS") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSRStatus" runat="server" Text='<%# Bind("SR_STATUS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SRsds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" 
                    SelectCommand="select SR_ID, SR_NO, SR_DATE, SR_STATUS  from YANTRA_SERVICE_REPORT_MAST order by SR_ID desc"></asp:SqlDataSource>
                </td>
                         <td style="vertical-align: top; width: 33%;">

                </td>
                         <td style="vertical-align: top; width: 33%;">

                </td>
         </tr>
     </table>
</asp:Content>


 
