<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/PurchaseMP1.master" AutoEventWireup="true" CodeFile="PObySea.aspx.cs" Inherits="Modules_SCM_PObySea" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">

     <asp:UpdatePanel ID="staffAdvancepnl" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tr class="pagehead">
                    <td style="text-align: left">PO by Sea</td>
                    <td style="text-align: right"></td>
                </tr>
            </table>
            <br />
            <table style="width: 100%">
                <tr>
                    <td style="text-align: right">Name :
                        <asp:TextBox ID="txtName" runat="server" TextMode="MultiLine"></asp:TextBox>
                        &nbsp;</td>
                    <td style="width: 5%"></td>
                    <td style="text-align: right">Date : 
                    </td>
                    <td>

                        <asp:TextBox ID="txtDate" type="date" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr><td colspan="4">&nbsp;</td></tr>
                <tr>
                    <td style="text-align: right">PO No. :
                        <asp:TextBox ID="txtPONo" runat="server" ></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="text-align: right">Ref :</td>
                    <td>
                        <asp:TextBox ID="txtRef" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr><td colspan="4">&nbsp;</td></tr>

                <tr>
                    <td style="text-align: right">Quotation Revised Date :
                        <asp:TextBox ID="txtRevisedDate" runat="server" type="date"></asp:TextBox>
                        &nbsp;
                    </td>
                    <td style="width: 5%"></td>
                    <td style="text-align: right" >Shipper&#39;s Name : 
                    </td>
                    <td  >
                        <asp:TextBox ID="txtShipperName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr><td colspan="4">&nbsp;</td></tr>

                <tr>
                    <td style="text-align: right">Terms:
                        <asp:TextBox ID="txtTerms" runat="server" TextMode="MultiLine"></asp:TextBox>
                        &nbsp;</td>
                    <td style="width: 5%"></td>
                    <td style="text-align: right" >
                        Ocean Freight: 
                    </td>
                    <td  >
                        <asp:TextBox ID="txtOceanFreight" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr><td colspan="4">&nbsp;</td></tr>

                <tr>
                    <td style="text-align: right"> 
                        Low Sulphur Surcharge:
                        <asp:TextBox ID="txtLowSulphurSurcharge" runat="server"></asp:TextBox>
                        &nbsp;</td>
                    <td style="width: 5%"></td>
                    <td style="text-align: right" >&nbsp; 
                        Exworks : 
                    </td>
                    <td  >
                        <asp:TextBox ID="txtExworks" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr><td colspan="4">&nbsp;</td></tr>

                <tr>
                    <td style="text-align: right">&nbsp; 
                        Destination Charges:
                        <asp:TextBox ID="txtDestinationCharges" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="text-align: right" >&nbsp; 
                         Shipping Line Charges :
                    </td>
                    <td  >
                        <asp:TextBox ID="txtShippingLineCharges" runat="server"></asp:TextBox>
                    </td>
                </tr>
                 <tr><td colspan="4">&nbsp;</td></tr>

                <tr>
                    <td style="text-align: right"> 
                        DO charges:
                        <asp:TextBox ID="txtDOCharges" runat="server" ></asp:TextBox>
                    </td>
                    <td colspan="3" style="width: 5%"></td>
                    
                </tr>
                 <tr><td colspan="4">&nbsp;</td></tr>
                <tr><td>
                    <table id="tblSea" runat="server" visible="false">
                         <tr>
                    <td style="text-align: right" > 
                        Ex1 :
                    
                        <asp:TextBox ID="txtEx1" runat="server"></asp:TextBox>
                    </td>
                </tr>
                 <tr><td colspan="4">&nbsp;</td></tr>
                <tr>
                    <td style="text-align: right">&nbsp; 
                        Ex2 :
                        <asp:TextBox ID="txtEx2" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="text-align: right" >&nbsp; 
                        Ex3 :
                    </td>
                    <td  >
                        <asp:TextBox ID="txtEx3" runat="server"></asp:TextBox>
                    </td>
                </tr> <tr><td colspan="4">&nbsp;</td></tr>

                <tr>
                    <td style="text-align: right"> 
                        Ex4 :
                        <asp:TextBox ID="txtEx4" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="text-align: right" >&nbsp; 
                        Ex5 :
                    </td>
                    <td  >
                        <asp:TextBox ID="txtEx5" runat="server"></asp:TextBox>
                    </td>
                </tr>
                    </table>
                    </td></tr>
               
                <tr><td colspan="4">&nbsp;</td></tr>
                <tr><td colspan="4" style="text-align:center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" />

                    </td></tr>
                <tr>
                    <td colspan="4" style="text-align:center">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align:center">&nbsp;</td>
                </tr>
            </table>
            <br />
            <table style="width:100%">
                <tr>
                    <td>
                        <asp:GridView ID="gvPObySea" runat="server" Width="100%" AutoGenerateColumns="False" SelectedRowStyle-BackColor="#c0c0c0" DataSourceID="SqlDataSource1">
                            <Columns>
                                <%--<asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />--%>
                                <asp:TemplateField HeaderText="Id">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnId" runat="server" ForeColor="#0066FF" OnClick="lbtnId_Click" Text="<%# Bind('Id') %>"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />

                                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" DataFormatString="{0:d}"  />
                                <asp:BoundField DataField="PO_No" HeaderText="PO No" SortExpression="PO_No" />
                                <asp:BoundField DataField="Ref" HeaderText="Reference" SortExpression="Ref" />
                                <asp:BoundField DataField="Quot_Revised_Date" HeaderText="Quotation Revised Date" SortExpression="Quot_Revised_Date" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="Shipper_Name" HeaderText="Shipper Name" SortExpression="Shipper_Name" />
                                <asp:BoundField DataField="Terms" HeaderText="Terms" SortExpression="Terms" />
                                <asp:BoundField DataField="Ocean_Freight" HeaderText="Ocean_Freight" Visible="false" SortExpression="Ocean_Freight" />
                                <asp:BoundField DataField="Low_Sulphur_Surcharge" HeaderText="Low_Sulphur_Surcharge" Visible="false" SortExpression="Low_Sulphur_Surcharge" />
                                <asp:BoundField DataField="Exworks" HeaderText="Exworks" Visible="false" SortExpression="Exworks" />
                                <asp:BoundField DataField="Destination_Charges" HeaderText="Destination_Charges" Visible="false" SortExpression="Destination_Charges" />
                                <asp:BoundField DataField="ShippingLine_Charges" HeaderText="ShippingLine_Charges" Visible="false" SortExpression="ShippingLine_Charges" />
                                <asp:BoundField DataField="DO_Charges" HeaderText="DO_Charges" Visible="false" SortExpression="DO_Charges" />
                                <asp:BoundField DataField="Ex1" HeaderText="Ex1" Visible="false" SortExpression="Ex1" />
                                <asp:BoundField DataField="Ex2" HeaderText="Ex2" Visible="false" SortExpression="Ex2" />
                                <asp:BoundField DataField="Ex3" HeaderText="Ex3" Visible="false" SortExpression="Ex3" />
                                <asp:BoundField DataField="Ex4" HeaderText="Ex4" Visible="false" SortExpression="Ex4" />
                                <asp:BoundField DataField="Ex5" HeaderText="Ex5" Visible="false" SortExpression="Ex5" />
                                <asp:BoundField DataField="CP_ID" HeaderText="Company Id" SortExpression="CPID" />
                                <asp:BoundField DataField="Id" HeaderText="Hidden" />
                            </Columns>
                            <SelectedRowStyle BackColor="Silver" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT * FROM [PObySea]  order by Id desc"></asp:SqlDataSource>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


 
