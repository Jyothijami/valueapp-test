<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CompanyMachinaryMaster.ascx.cs"
    Inherits="Modules_Masters_CompanyMachinaryMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table border="0" cellpadding="0" cellspacing="0" style="width: 750px">
    <tr>
        <td class="searchhead" colspan="4" style="height: 1px; text-align: left">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="text-align: left">
                        Company Machinery Master</td>
                    <td>
                    </td>
                    <td style="text-align: right">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td rowspan="3" style="height: 25px">
                                    <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                        Text="Search By"></asp:Label></td>
                                <td rowspan="3" style="height: 25px">
                                    <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="textbox" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--</asp:ListItem>
                                        <asp:ListItem Value="CM_ID">S.No.</asp:ListItem>
                                        <asp:ListItem Value="CM_MACHINE_NAME">Machine Name</asp:ListItem>
                                        <asp:ListItem Value="CM_MANUFACT_NAME">Manufacturer</asp:ListItem>
                                        <asp:ListItem Value="CM_INVOICE_NO">Invoice No</asp:ListItem>
                                        <asp:ListItem Value="CM_WARANTY_DET">Warranty</asp:ListItem>
                                        <asp:ListItem Value="CM_INSTALLATION_DATE">Installation Date</asp:ListItem>
                                        <asp:ListItem Value="CM_MANUFACT_DATE">Manufacture Date</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td rowspan="3" style="height: 25px">
                                    <asp:DropDownList ID="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                        Visible="False" Width="50px" EnableTheming="False" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged">
                                        <asp:ListItem Selected="True">=</asp:ListItem>
                                        <asp:ListItem>&lt;</asp:ListItem>
                                        <asp:ListItem>&gt;</asp:ListItem>
                                        <asp:ListItem>&lt;=</asp:ListItem>
                                        <asp:ListItem>&gt;=</asp:ListItem>
                                        <asp:ListItem>R</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td rowspan="3" style="height: 25px">
                                    <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False"></asp:Label></td>
                                <td rowspan="3" style="height: 25px">
                                    <asp:TextBox ID="txtSearchValueFromDate" runat="server" Visible="False" EnableTheming="True"
                                        Width="106px"></asp:TextBox><asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                    <cc1:CalendarExtender  Format="MM/dd/yyyy" ID="ceSearchFrom" runat="server" Enabled="False" PopupButtonID="imgFromDate"
                                        TargetControlID="txtSearchValueFromDate">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server" DisplayMoney="Left"
                                        Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate"
                                        UserDateFormat="MonthDayYear">
                                    </cc1:MaskedEditExtender>
                                </td>
                                <td rowspan="3" style="height: 25px">
                                    <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False"></asp:Label></td>
                                <td rowspan="3" style="height: 25px">
                                    <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px"></asp:TextBox><asp:Image
                                        ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False"></asp:Image>
                                    <cc1:CalendarExtender  Format="MM/dd/yyyy" ID="ceSearchValueToDate" runat="server" Enabled="False" PopupButtonID="imgToDate"
                                        TargetControlID="txtSearchText">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                        Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                        UserDateFormat="MonthDayYear">
                                    </cc1:MaskedEditExtender>
                                </td>
                                <td rowspan="3" style="height: 25px">
                                    <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                        CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnSearchGo_Click" /></td>
                            </tr>
                            <tr>
                            </tr>
                            <tr>
                            </tr>
                        </table>
                        <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="4" style="text-align: center">
            <asp:GridView ID="gvCompMachinary" runat="server" SelectedRowStyle-BackColor="#c0c0c0" AllowPaging="True" AutoGenerateColumns="False"
                DataSourceID="sdsCompanyMachinaryDetails" OnRowDataBound="gvCompMachinary_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="CM_MACHINE_NAME" HeaderText="MachineNameHidden"></asp:BoundField>
                    <asp:BoundField DataField="CM_ID" HeaderText="Machine Code">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Machine Name">
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("CM_MACHINE_NAME") %>' ID="TextBox1"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnCompMachinary" ForeColor="#0066ff" OnClick="lbtnCompMachinary_Click" runat="server"
                                Text="<%# Bind('CM_MACHINE_NAME') %>" CausesValidation="False"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CM_MANUFACT_NAME" HeaderText="Manufacturer">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="CM_DESC" HeaderText="Description">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="CM_WARANTY_DET" HeaderText="Warranty">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField HtmlEncode="False" DataFormatString="{0:MM/dd/yyyy}" DataField="CM_INSTALLATION_DATE"
                        HeaderText="Installation Date">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField HtmlEncode="False" DataFormatString="{0:MM/dd/yyyy}" DataField="CM_MANUFACT_DATE"
                        HeaderText="Mfg Date">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="CM_INVOICE_NO" HeaderText="InvoiceNoHidden">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:BoundField>
                </Columns>
                <SelectedRowStyle BackColor="LightSteelBlue" />
                <EmptyDataTemplate>
                    No Record Found
                </EmptyDataTemplate>
               
            </asp:GridView>
            <asp:SqlDataSource ID="sdsCompanyMachinaryDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                SelectCommand="SP_MASTER_COMPMACHINARY_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName"
                        ControlID="lblSearchItemHidden"></asp:ControlParameter>
                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType"
                        ControlID="lblSearchTypeHidden"></asp:ControlParameter>
                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue"
                        ControlID="lblSearchValueHidden"></asp:ControlParameter>
                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom"
                        ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
    </tr>
    <tr>
        <td colspan="4" style="height: 19px; text-align: left">
        </td>
    </tr>
    <tr>
        <td colspan="4" style="text-align: center">
            <table id="Table1">
                <tr>
                    <td>
                        <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CausesValidation="False" /></td>
                    <td>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CausesValidation="False" /></td>
                    <td>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"
                            CausesValidation="False" /></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <table border="0" cellpadding="0" cellspacing="0" id="tblMachinaryDetails"
                runat="server" visible="false" width="100%">
                <tr>
                    <td colspan="4" style="text-align: left" class="profilehead">
                        General Details</td>
                </tr>
                <tr>
                    <td style="height: 21px; text-align: right">
                    </td>
                    <td style="height: 21px; text-align: left">
                    </td>
                    <td style="height: 21px; text-align: right">
                    </td>
                    <td style="height: 21px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblMachineCode" runat="server" Text="Machine Code" Width="119px"></asp:Label></td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtMachineCode" runat="server" ReadOnly="True" MaxLength="20"></asp:TextBox>&nbsp;</td>
                    <td style="text-align: right;">
                        <asp:Label ID="lblMachineName" runat="server" Text="Machine Name" Width="105px"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtMachineName" runat="server" MaxLength="20"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVMachineName" runat="server" ControlToValidate="txtMachineName"
                            ErrorMessage="Please Enter the Machine Name">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblManufacturerName" runat="server" Text="Manufacturer Name" Width="153px"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtManufacturerName" runat="server" MaxLength="20"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVManufactName" runat="server" ControlToValidate="txtManufacturerName"
                            ErrorMessage="Please Enter the Manufacturer Name">*</asp:RequiredFieldValidator>
                        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Verdana" Font-Size="Smaller"
                            ForeColor="Red" Text="*"></asp:Label></td>
                    <td style="text-align: right">
                        <asp:Label ID="lblInvoiceNo" runat="server" Text="Invoice Number" Width="133px"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtInvoiceNo" runat="server" MaxLength="20"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVInvoiceNo" runat="server" ControlToValidate="txtInvoiceNo"
                            ErrorMessage="Pleaes Enter the Invoice Number">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblWarrantyDetails" runat="server" Text="Warranty Details" Width="105px">
                        </asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtWarrantyDetails" runat="server" MaxLength="20"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVWarranty" runat="server" ControlToValidate="txtWarrantyDetails"
                            ErrorMessage="Please Enter the Warranty Details">*</asp:RequiredFieldValidator></td>
                    <td style="text-align: right">
                        <asp:Label ID="lblInstallationDate" runat="server" Text="Date of Installation" Width="131px">
                        </asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtInstallationDate" runat="server">
                        </asp:TextBox>&nbsp;<asp:Image ID="imgInstallDate" runat="server" ImageUrl="~/Images/Calendar.png">
                        </asp:Image>
                        <asp:RequiredFieldValidator ID="RFVInstDate" runat="server" ControlToValidate="txtInstallationDate"
                            ErrorMessage="Please Select the Installation Date">*</asp:RequiredFieldValidator>
                        <cc1:CalendarExtender  Format="MM/dd/yyyy" ID="ceDateOfInstall" runat="server" PopupButtonID="imgInstallDate"
                            TargetControlID="txtInstallationDate">
                        </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="MaskedEditInstallDate" runat="server" DisplayMoney="Left"
                            Mask="99/99/9999" MaskType="Date" TargetControlID="txtInstallationDate" UserDateFormat="MonthDayYear">
                        </cc1:MaskedEditExtender>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        &nbsp;<asp:Label ID="Label1" runat="server" Text="Description" Width="105px"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                    <td style="text-align: right">
                        <asp:Label ID="lblManufacturingDate" runat="server" Text="Date of Manufacturing"
                            Width="146px"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtManufacturingDate" runat="server">
                        </asp:TextBox>&nbsp;<asp:Image ID="imgManfDate" runat="server" ImageUrl="~/Images/Calendar.png">
                        </asp:Image>
                        <asp:RequiredFieldValidator ID="RFVManufactDate" runat="server" ControlToValidate="txtManufacturingDate"
                            ErrorMessage="Please select the Manufacturing Date">*</asp:RequiredFieldValidator>
                        <cc1:CalendarExtender  Format="MM/dd/yyyy" ID="ceDateOfManf" runat="server" PopupButtonID="imgManfDate"
                            TargetControlID="txtManufacturingDate">
                        </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="MaskedEditManfDate" runat="server" Mask="99/99/9999"
                            MaskType="Date" TargetControlID="txtManufacturingDate" UserDateFormat="MonthDayYear">
                        </cc1:MaskedEditExtender>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; height: 19px;">
                    </td>
                    <td style="text-align: left; height: 19px;">
                    </td>
                    <td style="text-align: right; height: 19px;">
                    </td>
                    <td style="text-align: left; height: 19px;">
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center">
                        <table id="tblButtons">
                            <tr>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                <td>
                                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click"
                                        CausesValidation="False" /></td>
                                <td>
                                    <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ShowSummary="False"></asp:ValidationSummary>
        </td>
    </tr>
    <tr>
        <td style="height: 8px">
        </td>
        <td style="height: 8px;">
        </td>
        <td style="height: 8px;">
        </td>
        <td style="height: 8px">
        </td>
    </tr>
</table>
