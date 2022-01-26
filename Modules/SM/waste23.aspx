<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="waste23.aspx.cs" Inherits="Modules_SM_waste23" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div>
        <table>
            <tr>
                <td>Enter Name Name</td>
                <td>:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtCustomerName"/></td>
                <td>
                    <asp:Button Text="Search" ID="btnSearch" OnClick="btnSearch_Click" runat="server" /></td>
            </tr>
        </table>
    </div>
    <div id="searchDetails">
        <table id="tblPopup2" runat="server" visible="false">
            <tr>
                <td colspan="3" rowspan="1" style="text-align: left; height: 40px; width: 100%;"></td>
            </tr>
            <tr>
                <td class="profilehead" colspan="3" rowspan="1" style="text-align: left">
                    <asp:Label ID="Label9" runat="server" Text="CUSTOMER DETAILS:"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: left;">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        Width="100%" meta:resourcekey="gvCustMasterDetailsResource1" Visible="False">
                        <Columns>
                            <asp:BoundField DataField="CUST_CODE" HeaderText="Customer Code" meta:resourceKey="BoundFieldResource2">
                                <ControlStyle Width="100px"></ControlStyle>

                                <ItemStyle Width="110px" HorizontalAlign="Right"></ItemStyle>

                                <HeaderStyle Width="100px" HorizontalAlign="Right"></HeaderStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Customer Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CUST_NAME") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnCustomer" runat="server" Text='<%# Bind("CUST_NAME") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CUST_NAME" HeaderText="Customer Name" />
                            <asp:BoundField DataField="CUST_COMPANY_NAME" HeaderText="Company Name" meta:resourceKey="BoundFieldResource3">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CUST_CONTACT_PERSON" HeaderText="Contact Person" meta:resourceKey="BoundFieldResource5">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CUST_ADDRESS" HeaderText="Customer Address" meta:resourceKey="BoundFieldResource6">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CUST_EMAIL" HeaderText="Email" meta:resourceKey="BoundFieldResource7">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField ReadOnly="True" DataField="CUST_ID" SortExpression="CUST_ID" HeaderText="CUST_ID" meta:resourceKey="BoundFieldResource8"></asp:BoundField>
                            <asp:BoundField DataField="CUST_STATUS" HeaderText="Status" meta:resourceKey="BoundFieldResource9">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:BoundField>
                        </Columns>
                        <EmptyDataTemplate>
                            No Data Exist!
                    
                        </EmptyDataTemplate>
                        <SelectedRowStyle BackColor="LightSteelBlue" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="profilehead" colspan="3" style="text-align: left">SALES LEAD:</td>
            </tr>
            <tr>
                <td colspan="3" rowspan="1" style="text-align: left;">
                    <asp:GridView ID="gvSalesEnquiry" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        DataKeyNames="ENQ_ID" Width="100%" Visible="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Enq No">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ENQ_NO") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnSalesEnq" runat="server" Text='<%# Bind("ENQ_NO") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ENQ_ID" HeaderText="Enq No" />
                            <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="ENQ_DATE" SortExpression="ENQ_DATE" HeaderText="Enq Date">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CUST_NAME" SortExpression="CUST_NAME" HeaderText="Customer Name">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="ENQ_DUE_DATE" SortExpression="ENQ_DUE_DATE" HeaderText="Due Date">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ENQ_STATUS" SortExpression="ENQ_STATUS" HeaderText="Status">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:BoundField>
                        </Columns>
                        <EmptyDataTemplate>
                            No Data Exist!
                    
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="profilehead" colspan="3" rowspan="1" style="text-align: left; height: 17px;">QUOTATION MASTER:</td>
            </tr>
            <tr>
                <td colspan="3" rowspan="1" style="text-align: left;">
                    <asp:GridView ID="gvQuotationDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False" Visible="False" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Quotation No">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("QUOT_NO") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnQuotation" runat="server" Text='<%# Bind("QUOT_NO") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="QUOT_ID" HeaderText="Quotation No" />
                            <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True" DataField="QUOT_DATE" HeaderText="Quotation Date"></asp:BoundField>
                            <asp:BoundField DataField="CUST_NAME" HeaderText="Customer">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CUST_CONTACT_PERSON" HeaderText="Contact Person">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CUST_EMAIL" HeaderText="Email">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PREPAREDBY" SortExpression="PREPAREDBY" HeaderText="Prepared By">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="APPROVEDBY" SortExpression="APPROVEDBY" HeaderText="Approved By">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="QUOT_PO_FLAG" HeaderText="Status">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:BoundField>
                        </Columns>
                        <EmptyDataTemplate>
                            No Data Exist!
                    
                        </EmptyDataTemplate>
                        <SelectedRowStyle BackColor="LightSteelBlue" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="3" rowspan="1" style="text-align: center; height: 24px;">
                    <%--<asp:Button ID="Button1" runat="server" Text="Close" EnableTheming="False" OnClick="Button1_Click" />
                    <asp:Button ID="btnSalesLeadPopUp" runat="server" Text="Sales Lead" OnClick="btnSalesLeadPopUp_Click" EnableTheming="False" />--%>

                </td>
            </tr>
        </table>
    </div>
</asp:Content>


 
