<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true" CodeFile="CV1.aspx.cs" Inherits="Modules_HR_CV1" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
 
    <table style="width: 100%">
        <tr class="pagehead">
            <td style="text-align: left">Convenience Voucher</td>
            <td style="text-align: right"></td>
        </tr>
    </table>
    <br />
   <asp:UpdatePanel runat="server">
            <ContentTemplate>
                
    <table style="width: 100%">
        <tr>
            <td>Employee Name :
                        <asp:TextBox ID="txtEmpSearch" runat="server"></asp:TextBox>
                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="GO" OnClick="btnSearch_Click" Width="50px" />
            </td>
        </tr>
       
        <tr>
            <td>
                <asp:GridView ID="gvConvenienceVoucher" runat="server" AutoGenerateColumns="False" OnDataBound="gvConvenienceVoucher_DataBound" SelectedRowStyle-BackColor="#c0c0c0" Width="100%" EmptyDataText="No Records Found" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvConvenienceVoucher_PageIndexChanging">
                    <FooterStyle BackColor="#1AA8BE" />
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Si.No." SortExpression="Id">
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Voucher No">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ControlStyle Width="100px"></ControlStyle>
                            <ItemStyle Width="100px" HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle Width="100px" HorizontalAlign="Left"></HeaderStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnVoucherN0" OnClick="lbtnVoucherNo_Click" ForeColor="#0066ff" runat="server" Text='<%# Bind("Voucher_No") %>' CausesValidation="False"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:d}" SortExpression="Date">
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Exe_Name" HeaderText="Execute Name" SortExpression="Exe_Name">
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Employee Name" SortExpression="EMP_FIRST_NAME">
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>

                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="USP_Conv_Voucher" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblEmpIdHidden" DefaultValue="0" Name="EmpId" PropertyName="Text" Type="Int64" />
                        <asp:ControlParameter ControlID="lblUserType" DefaultValue="0" Name="userType" PropertyName="Text" Type="Int64" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
         
        <tr>
            <td>
                <table align="center">
                    <tr>
                        <td>
                            <%--<asp:Button ID="btnAddnew" runat="server" Text="Add New" OnClick="btnAddnew_Click" />--%>
                        </td>
                        <%--<td>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" />
                                </td>--%>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="true" OnClick="btnDelete_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" />
                            <asp:Label ID="lblUserType" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
               <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
    </table>
     </ContentTemplate>
    </asp:UpdatePanel>
    <table align="center">
        <tr>
            <td style="align-content: center">
                <asp:Button ID="btnAddnew" runat="server" Text="Add New" OnClick="btnAddnew_Click" />

            </td>
        </tr>
    </table>
    <br />
   <%-- <asp:UpdatePanel runat="server">
        <ContentTemplate>--%>
    <div id="tblDetails" runat="server" width="100%" visible="false">
        <table style="width: 100%">
            <tr>
                <td style="text-align: right">Voucher No. :
                        <asp:TextBox ID="txtVoucherNo" runat="server" Enabled="False"></asp:TextBox>
                    &nbsp;</td>
                <td style="width: 5%"></td>
                <td style="text-align: right">Date : 
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtDate" type="date" runat="server" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="5">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">Executive Name :
                        <asp:TextBox ID="txtExeName" runat="server" ValidationGroup="B"></asp:TextBox>
                </td>
                <td style="width: 5%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtExeName" ErrorMessage="*" ForeColor="Red" ToolTip="Please Provide Details" ValidationGroup="B"></asp:RequiredFieldValidator>
                </td>
                <td style="text-align: right">&nbsp; 
                </td>
                <td>
                    <asp:Label runat="server" ID="lblCPID" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="5">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: left; height: 20px;" class="auto-style1">
                    <strong>Voucher Details : </strong>
                </td>
            </tr>
            <tr>
                <td colspan="5">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">Site Name :
                        <asp:TextBox ID="txtSiteName" runat="server" ValidationGroup="A"></asp:TextBox>
                </td>
                <td style="width: 5%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSiteName" ErrorMessage="*" ForeColor="Red" ToolTip="Please Provide Details" ValidationGroup="A"></asp:RequiredFieldValidator>
                </td>
                <td style="text-align: right">Purpose : 
                </td>
                <td style="text-align: left">

                    <asp:TextBox ID="txtPurpose" runat="server" ValidationGroup="A"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPurpose" ErrorMessage="*" ForeColor="Red" ToolTip="Please Provide Details" ValidationGroup="A"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="5">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">From :
                        <asp:TextBox ID="txtFromLoc" runat="server" ValidationGroup="A"></asp:TextBox>
                </td>
                <td style="width: 5%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtFromLoc" ErrorMessage="*" ForeColor="Red" ToolTip="Please Provide Details" ValidationGroup="A"></asp:RequiredFieldValidator>
                </td>
                <td style="text-align: right">To : 
                </td>
                <td style="text-align: left">

                    <asp:TextBox ID="txtToLoc" runat="server" ValidationGroup="A"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtToLoc" ErrorMessage="*" ForeColor="Red" ToolTip="Please Provide Details" ValidationGroup="A"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="5">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">From Time :
                        <asp:TextBox ID="txtFromTime" runat="server" ValidationGroup="A"></asp:TextBox>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" DisplayMoney="Left"
                        Mask="99:99" AcceptAMPM="true" ClearMaskOnLostFocus="false" AutoComplete="false" MaskType="Time" TargetControlID="txtFromTime" UserDateFormat="None" Enabled="True">
                    </cc1:MaskedEditExtender>
                </td>
                <td style="width: 5%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtFromTime" ErrorMessage="*" ForeColor="Red" ToolTip="Please Provide Details" ValidationGroup="A"></asp:RequiredFieldValidator>
                </td>
                <td style="text-align: right">To Time : 
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtToTime" runat="server" ValidationGroup="A"></asp:TextBox>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" DisplayMoney="Left"
                        Mask="99:99" AcceptAMPM="true" ClearMaskOnLostFocus="false" AutoComplete="false" MaskType="Time" TargetControlID="txtToTime" UserDateFormat="None" Enabled="True">
                    </cc1:MaskedEditExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtToTime" ErrorMessage="*" ForeColor="Red" ToolTip="Please Provide Details" ValidationGroup="A"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="5">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">KMs :
                        <asp:TextBox ID="txtKMs" runat="server" ValidationGroup="A"></asp:TextBox>
                </td>
                <td style="width: 5%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtKMs" ErrorMessage="*" ForeColor="Red" ToolTip="Please Provide Details" ValidationGroup="A"></asp:RequiredFieldValidator>
                </td>
                <td style="text-align: right;">On Date : </td>
                <td>
                    <asp:TextBox ID="txtOnDate" runat="server" type="date" ValidationGroup="A"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtOnDate" ErrorMessage="*" ForeColor="Red" ToolTip="Please Provide Details" ValidationGroup="A"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="5">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="5" style="text-align: center">
                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" ValidationGroup="A" />
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" />
                    <asp:Label ID="lblTotalKMs" runat="server" Text="" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>

        <br />
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:GridView ID="gvVoucherDetails" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDeleting="gvVoucherDetails_RowDeleting" ShowFooter="true" OnRowDataBound="gvVoucherDetails_RowDataBound">
                        <Columns>
                            <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                            <asp:BoundField DataField="Site_Name" HeaderText="Site Name" />
                            <asp:BoundField DataField="Purpose" HeaderText="Purpose" />
                            <asp:BoundField DataField="From_Loc_Id" HeaderText="From" />
                            <asp:BoundField DataField="To_Loc_Id" HeaderText="To" />
                            <asp:BoundField DataField="From_Time" HeaderText="From Time" />
                            <asp:BoundField DataField="To_Time" HeaderText="To Time" />
                            <asp:BoundField DataField="KMs" HeaderText="KMs" />
                            <asp:BoundField DataField="On_Date" HeaderText="On Date" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="5">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label10" runat="server" Text="Prepared By :"></asp:Label>
                    <asp:DropDownList ID="ddlPreparedBy" runat="server" Style="margin-left: 0px" Enabled="False">
                    </asp:DropDownList></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="5">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="5" style="text-align: center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="B" />
                    <asp:Button ID="btnMainRefresh" runat="server" Text="Refresh" OnClick="btnMainRefresh_Click" />

                </td>
            </tr>
        </table>
    </div>
   
</asp:Content>

