<%@ Page Title="||Value App : Services : Customer Information ||" Language="C#" MasterPageFile="~/MPs/ServicesMP1.master" AutoEventWireup="true" CodeFile="ServiceCustomerInformation.aspx.cs" Inherits="Modules_Services_ServiceCustomerInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <h3 style="padding-left: 70px;">Service Customer Information</h3>
            <div id="divServiceCustInfo" align="center">
                <table>
                    <tr style="text-align: right">
                        <td>Customer Code :
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCustCode" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="width: 5%"></td>
                        <td>PO No Search :</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtSearchModel" runat="server">
                                    </asp:TextBox><asp:Button ID="btnSearchModelNo" runat="server" BorderStyle="None"
                                        CausesValidation="False" CssClass="gobutton" EnableTheming="False" OnClick="btnSearchModelNo_Click"
                                        Text="Go" ValidationGroup="Search" />
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                        SelectCommand="SP_PONO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:Parameter Type="Int64" DefaultValue="0" Name="BranbId"></asp:Parameter>
                                            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtSearchModel"></asp:ControlParameter>
                                        </SelectParameters>
                                    </asp:SqlDataSource><%--<asp:TextBox ID="txtSearchCust" ReadOnly="false" runat="server"></asp:TextBox><asp:Button
                                                    ID="btnSearchModelNo" runat="server" BorderStyle="None" CausesValidation="False"
                                                    CssClass="gobutton" EnableTheming="False" OnClick="btnSearchModelNo_Click" Text="Go" />--%></td>
                    </tr>
                   <tr style="text-align: right">
                        <td>Customer Name :</td>
                        <td style="text-align: left">
                            <%--<asp:DropDownList ID ="ddlCust" runat ="server"  ></asp:DropDownList>--%>
                            <asp:TextBox ID="txtCustName" runat ="server" ></asp:TextBox>
                        </td>
                        <td style="width: 5%"></td>
                        <td>PO NO:</td>
                        <td style="text-align: left">
                            <asp:DropDownList ID ="ddlPONo" runat ="server" AutoPostBack="True" OnSelectedIndexChanged ="ddlPONo_SelectedIndexChanged1"></asp:DropDownList>

                        </td>
                    </tr>
                    <tr style="text-align: right">
                        <td>Company Name  :
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCompanyName" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 5%"></td>
                        <td>Contact Person :
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtContactPerson" ReadOnly="false" runat="server"></asp:TextBox>

                        </td>
                    </tr>
                    <tr style="text-align: right">
                        <td>Customer Contact No :
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox runat="server" ID="txtContactNo" ReadOnly="false" />
                        </td>
                        <td style="width: 5%"></td>
                        <td>Email :
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox runat="server" ID="txtEmail" ReadOnly="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Customer Address :
                        </td>
                        <td colspan="4" style="text-align: left">
                            <asp:TextBox runat="server" ID="txtCustAddress" TextMode="MultiLine" Style="width: 400px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Any Reference :
                        </td>
                        <td colspan="4" style="text-align: left">
                            <asp:TextBox runat="server" ID="txtReference" TextMode="MultiLine" Style="width: 400px" />
                        </td>
                    </tr>

                </table>
            </div>
            <h3 style="padding-left: 70px;">Customer Unit Details</h3>
            <div id="divCustUnitDet" align="center">
                <table>
                    <tr>
                        <td style="text-align: right">&nbsp;</td>
                        <td colspan="4" style="text-align: left">&nbsp;</td>
                    </tr>
                    <tr style="text-align: right">
                        <td>Unit Name :
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox runat="server" ID="txtUnitName" TextMode="SingleLine" />
                        </td>
                        <td style="width: 5%">&nbsp;</td>
                        <td>Unit Contact Person :
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtUnitContactPerson" runat="server"></asp:TextBox>

                        </td>
                    </tr>
                    <tr style="text-align: right">
                        <td>Email Id :</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtUnitEmail" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 5%"></td>
                        <td>Contact Person No:
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtUnitContactNo" ReadOnly="false" runat="server"></asp:TextBox>

                        </td>
                    </tr>
                    <tr style="text-align: right">
                        <td>Unit Address :</td>
                        <td style="text-align: left">
                            <asp:TextBox runat="server" ID="txtUnitAddress" TextMode="MultiLine" Style="width: 400px" />
                        </td>
                        <td style="width: 5%">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td style="text-align: left">&nbsp;</td>
                    </tr>
                    <tr style="text-align: center">
                        <td colspan="5" style="text-align: center">
                            <asp:Button ID="btnAddUnit" runat="server" Text="Add" OnClick="btnAddUnit_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Refresh" OnClick="btnCancel_Click" />


                        </td>
                    </tr>
                </table>
                <br />
                <asp:GridView ID="gvCustUnitDetails" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDataBound="gvCustUnitDetails_RowDataBound" OnRowDeleting="gvCustUnitDetails_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                        
                        <asp:BoundField DataField="Cust_Unit_Name" HeaderText="Unit Name"></asp:BoundField>
                        <asp:BoundField DataField="Cust_Unit_Address" HeaderText="Unit Address"></asp:BoundField>
                        <asp:BoundField DataField="Unit_Contact_Person" HeaderText="Contact Person"></asp:BoundField>
                        <asp:BoundField DataField="Contact_Mobile" HeaderText="Contact Person No"></asp:BoundField>
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                    </Columns>
                </asp:GridView>
                <br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDataBound="gvCustUnitDetails_RowDataBound" >
                    <Columns>
                        
                        <asp:BoundField DataField="Cust_Unit_Name" HeaderText="Unit Name"></asp:BoundField>
                        <asp:BoundField DataField="Cust_Unit_Address" HeaderText="Unit Address"></asp:BoundField>
                        <asp:BoundField DataField="Unit_Contact_Person" HeaderText="Contact Person"></asp:BoundField>
                        <asp:BoundField DataField="Contact_Mobile" HeaderText="Contact Person No"></asp:BoundField>
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                    </Columns>
                </asp:GridView>
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" />
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


 
