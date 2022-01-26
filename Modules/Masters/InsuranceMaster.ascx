<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InsuranceMaster.ascx.cs" Inherits="Modules_Masters_InsuranceMaster" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td class="searchhead" colspan="2" style="text-align: left;">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="text-align: left">
                        Insurance Master Details</td>
                    <td>
                    </td>
                    <td style="text-align: right">
                        <table border="0" cellpadding="0" cellspacing="0" align="right">
                            <tr>
                                <td rowspan="3" style="height: 25px">
                                    <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                        Text="Search By"></asp:Label></td>
                                <td rowspan="3" style="height: 25px">
                                    <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                        OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--</asp:ListItem>
                                        <asp:ListItem Value="Insurance_Master_id">Id</asp:ListItem>
                                        <asp:ListItem Value="Insurance_Name">Insurance Company</asp:ListItem>
                                        <asp:ListItem Value="Address">Address</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td rowspan="3" style="width: 17px; height: 25px">
                                    <asp:Label ID="lblSearchtext" runat="server" CssClass="label" EnableTheming="False"
                                        Font-Bold="True" Text="Text" Width="37px"></asp:Label></td>
                                <td rowspan="3" style="width: 150px; height: 25px">
                                    <asp:TextBox ID="txtSearchText" runat="server" CssClass="textbox" Width="111px"></asp:TextBox><asp:Image
                                        ID="imgCurrentDayTasksToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                        Visible="False" /></td>
                                <td rowspan="3" style="height: 25px">
                                    <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                        CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                            </tr>
                            <tr>
                            </tr>
                            <tr>
                            </tr>
                        </table>
                        <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: center">
            <asp:GridView ID="gvDespatchDetails" SelectedRowStyle-BackColor="#c0c0c0" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" DataSourceID="sdsInsuranceMaster" OnRowDataBound="gvDespatchDetails_RowDataBound"
                Width="100%">
                <Columns>
                    <asp:BoundField DataField="Insurance_Name" HeaderText="NameHidden" />
                    <asp:BoundField DataField="Insurance_Master_id" HeaderText="Insurance Id">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Insurance Company">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Despm_name") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnDespatchName" ForeColor="#0066ff" runat="server" CausesValidation="False" OnClick="lbtnDespatchName_Click"
                                Text='<%# Bind("Insurance_Name") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Address" HeaderText="Address" NullDisplayText="-">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
                <EmptyDataTemplate>
                    No Record Found
                </EmptyDataTemplate>
                <SelectedRowStyle BackColor="LightSteelBlue" />
            </asp:GridView>
            <asp:SqlDataSource ID="sdsInsuranceMaster" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                SelectCommand="SP_MASTER_INSURANCEMASTER_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName"
                        PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue"
                        PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2" style="height: 19px; text-align: left">
        </td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: center">
            <table id="Table1" align="center">
                <tr>
                    <td>
                        <asp:Button ID="btnNew" runat="server" CausesValidation="False" OnClick="btnNew_Click"
                            Text="New" /></td>
                    <td>
                        <asp:Button ID="btnEdit" runat="server" CausesValidation="False" OnClick="btnEdit_Click"
                            Text="Edit" /></td>
                    <td>
                        <asp:Button ID="btnDelete" runat="server" CausesValidation="False" OnClick="btnDelete_Click"
                            Text="Delete" /></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: center">
            <table id="tblDespatchDetails" runat="server" border="0" cellpadding="0" cellspacing="0"
                visible="false" width="100%">
                <tr>
                    <td id="tblDespmDetails" runat="server" colspan="2">
                        <table id="Table2" runat="server" border="0" cellpadding="0" cellspacing="0" visible="true"
                            width="100%">
                            <tr>
                                <td class="profilehead" colspan="2" style="text-align: left">
                                    General Details</td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: right">
                                </td>
                                <td style="height: 21px; text-align: left">
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 24px; text-align: right">
                                    &nbsp; &nbsp; &nbsp;&nbsp;
                                    <asp:Label ID="lblDespatchName" runat="server" Text="Insurance Company" Width="146px"></asp:Label></td>
                                <td style="height: 24px; text-align: left">
                                    <asp:TextBox ID="txtInsurancecompany" runat="server" MaxLength="20" Width="172px"></asp:TextBox>
                                    <asp:Label ID="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RFVDespName" runat="server" ControlToValidate="txtInsurancecompany"
                                        ErrorMessage="Please Enter the CompanyName">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td style="height: 38px; text-align: right">
                                    <asp:Label ID="lblDescription" runat="server" Text="Address" Width="79px"></asp:Label></td>
                                <td style="height: 38px; text-align: left">
                                    <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                </tr>
                <tr>
                    <td style="height: 19px; text-align: right">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <table id="tblButtons" align="center">
                            <tr>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" /></td>
                                <td>
                                    <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" OnClick="btnRefresh_Click"
                                        Text="Refresh" /></td>
                                <td>
                                    <asp:Button ID="btnClose" runat="server" CausesValidation="False" OnClick="btnClose_Click"
                                        Text="Close" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td style="width: 750px">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ShowSummary="False" />
        </td>
    </tr>
</table>
