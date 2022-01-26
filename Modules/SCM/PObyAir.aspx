<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/PurchaseMP1.master" AutoEventWireup="true" CodeFile="PObyAir.aspx.cs" Inherits="Modules_SCM_PObyAir" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <asp:UpdatePanel ID="staffAdvancepnl" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tr class="pagehead">
                    <td style="text-align: left">PO by Air</td>
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
                        <asp:TextBox ID="txtRevisedDate" runat="server" type="date" Width="204px"></asp:TextBox>
                        &nbsp;
                    </td>
                    <td style="width: 5%"></td>
                    <td style="text-align: right" >Shipper&#39;s Name : 
                    </td>
                    <td style="margin-left: 40px">
                        <asp:TextBox ID="txtShipperName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr><td colspan="4">&nbsp;</td></tr>

                <tr>
                    <td style="text-align: right">Terms:
                        <asp:TextBox ID="txtTerms" runat="server" TextMode="MultiLine"></asp:TextBox>
                        &nbsp;</td>
                    <td style="width: 5%"></td>
                    <td style="text-align: right" >&nbsp;
                        Airfreight +1000 kgs: 
                    </td>
                    <td style="margin-left: 40px">
                        <asp:TextBox ID="txtAirFreight" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr><td colspan="4">&nbsp;</td></tr>

                <tr>
                    <td style="text-align: right">
                        Fuel:
                        <asp:TextBox ID="txtFuel" runat="server"></asp:TextBox>
                        &nbsp;</td>
                    <td style="width: 5%"></td>
                    <td style="text-align: right" >&nbsp; 
                        Ssc: 
                    </td>
                    <td style="margin-left: 40px">
                        <asp:TextBox ID="txtSsc" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr><td colspan="4">&nbsp;</td></tr>

                <tr>
                    <td style="text-align: right">&nbsp; 
                        Exworks:
                        <asp:TextBox ID="txtExworks" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="text-align: right" >&nbsp; 
                        All in charges :
                    </td>
                    <td style="margin-left: 40px">
                        <asp:TextBox ID="txtAllInCharges" runat="server"></asp:TextBox>
                    </td>
                </tr>
                 <tr><td colspan="4">&nbsp;</td></tr>

                <tr>
                    <td style="text-align: right"> 
                        HAWB fee:
                        <asp:TextBox ID="txtHAWBfee" runat="server" ></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="text-align: right" > 
                        Airliner DO :
                    </td>
                    <td style="margin-left: 40px">
                        <asp:TextBox ID="txtAirlinerDO" runat="server"></asp:TextBox>
                    </td>
                </tr>
                 <tr><td colspan="4">&nbsp;</td></tr>

                <tr>
                    <td style="text-align: right"> 
                        CC fee:
                        <asp:TextBox ID="txtCCfee" runat="server" ></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="text-align: right" > 
                        IGM fee :
                    </td>
                    <td style="margin-left: 40px">
                        <asp:TextBox ID="txtIGMfee" runat="server"></asp:TextBox>
                    </td>
                </tr>
                 <tr><td colspan="4">&nbsp;</td></tr>

                <tr>
                    <td style="text-align: right"> 
                        Service tax :
                        <asp:TextBox ID="txtServiceTax" runat="server" ></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    
                </tr>
                 <tr><td colspan="4">&nbsp;</td></tr>
                <tr><td>
                    <table id="tblAir" runat="server" visible="false">
                         <tr>
                    <td style="text-align: right" > 
                        Ex1:
                   
                        <asp:TextBox ID="txtEx1" runat="server"></asp:TextBox>
                    </td>
                </tr>
                 <tr><td colspan="4">&nbsp;</td></tr>
                <tr>
                    <td style="text-align: right"> 
                        Ex2 :
                        <asp:TextBox ID="txtEx2" runat="server" ></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="text-align: right" >
                        Ex3 :
                    </td>
                    <td  >
                        <asp:TextBox ID="txtEx3" runat="server"></asp:TextBox>
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
                        <asp:GridView ID="gvPObyAir" runat="server" Width="100%" AutoGenerateColumns="False" SelectedRowStyle-BackColor="#c0c0c0" DataSourceID="SqlDataSource1">
                            <Columns>
                                <%--<asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />--%>
                                 <asp:TemplateField HeaderText="Id">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnId" runat="server" ForeColor="#0066FF" OnClick="lbtnId_Click" Text='<%# Bind("Id") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                                
                                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="PO_No" HeaderText="PO No" SortExpression="PO_No" />
                                <asp:BoundField DataField="Ref" HeaderText="Reference" SortExpression="Ref" />
                                <asp:BoundField DataField="Quot_Revised_Date" HeaderText="Quotation Revised Date" SortExpression="Quot_Revised_Date" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="Shipper_Name" HeaderText="Shipper Name" SortExpression="Shipper_Name" />
                                <asp:BoundField DataField="Terms" HeaderText="Terms" SortExpression="Terms" />
                                <asp:BoundField DataField="AirFreight" HeaderText="AirFreight" Visible="false" SortExpression="AirFreight" />
                                <asp:BoundField DataField="Fuel" HeaderText="Fuel" Visible="false" SortExpression="Fuel" />
                                <asp:BoundField DataField="Ssc" HeaderText="Ssc" Visible="false" SortExpression="Ssc" />
                                <asp:BoundField DataField="Exworks" HeaderText="Exworks" Visible="false" SortExpression="Exworks" />
                                <asp:BoundField DataField="All_In_Charge" HeaderText="All_In_Charge" Visible="false" SortExpression="All_In_Charge" />
                                <asp:BoundField DataField="HAWB_fee" HeaderText="HAWB_fee" Visible="false" SortExpression="HAWB_fee" />
                                <asp:BoundField DataField="Airliner_DO" HeaderText="Airliner_DO" Visible="false" SortExpression="Airliner_DO" />
                                <asp:BoundField DataField="CC_fee" HeaderText="CC_fee" Visible="false" SortExpression="CC_fee" />
                                <asp:BoundField DataField="IGM_fee" HeaderText="IGM_fee" Visible="false" SortExpression="IGM_fee" />
                                <asp:BoundField DataField="Service_Tax" HeaderText="Service_Tax" Visible="false" SortExpression="Service_Tax" />
                                <asp:BoundField DataField="Ex1" HeaderText="Ex1" Visible="false" SortExpression="Ex1" />
                                <asp:BoundField DataField="Ex2" HeaderText="Ex2" Visible="false" SortExpression="Ex2" />
                                <asp:BoundField DataField="Ex3" HeaderText="Ex3" Visible="false" SortExpression="Ex3" />
                                <asp:BoundField DataField="CP_ID" HeaderText="Company Id" SortExpression="CPID" />
                                <asp:BoundField DataField="Id" HeaderText="Hidden" />
                            </Columns>
                            <SelectedRowStyle BackColor="Silver" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT * FROM [PObyAir] order by Id desc"></asp:SqlDataSource>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


 
