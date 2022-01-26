<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CountryMaster.ascx.cs" Inherits="Modules_Masters_CountryMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%--<link href="../../App_Themes/Master/Master.css" rel="stylesheet" type="text/css" />--%> 
<table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td class="searchhead" colspan="2">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="text-align: left">
                        Country Master</td>
                    <td>
                    </td>
                    <td style="text-align: right;">
                        <table border="0" cellpadding="0" cellspacing="0" align="right">
                            <tr>
                                <td rowspan="3">
                                    <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                        Text="Search By"></asp:Label></td>
                                <td rowspan="3">
                                    <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="textbox" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--</asp:ListItem>
                                        <asp:ListItem Value="COUNTRY_ID">S.No</asp:ListItem>
                                        <asp:ListItem Value="COUNTRY_NAME">Country Name</asp:ListItem>
                                        
                                    </asp:DropDownList></td>
                                <td rowspan="3">
                                </td>
                                <td rowspan="3">
                                    <asp:TextBox ID="txtSearchText" runat="server" CssClass="textbox" Width="111px"></asp:TextBox><asp:Image
                                        ID="imgCurrentDayTasksToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                        Visible="False"></asp:Image></td>
                                <td rowspan="3">
                                    <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                        CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnSearchGo_Click" /></td>
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
            <asp:GridView ID="gvCountryDetails" SelectedRowStyle-BackColor="#c0c0c0" runat="server" AutoGenerateColumns="False"
                DataSourceID="sdsDesignationDetails" AllowPaging="True" OnRowDataBound="gvCountryDetails_RowDataBound" Width="100%">
                <Columns>
                    <asp:BoundField DataField="COUNTRY_NAME" HeaderText="Country Name" SortExpression="COUNTRY_NAME" />
                    <asp:BoundField DataField="COUNTRY_ID" HeaderText="S.No">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Country Name">
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("Desg_name") %>' ID="TextBox1"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnCountryName" ForeColor="#0066ff" runat="server" Text="<%# Bind('COUNTRY_NAME') %>"
                                OnClick="lbtnCountryName_Click" CausesValidation="False"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <SelectedRowStyle BackColor="LightSteelBlue" />
                <EmptyDataTemplate>
                    No Record Found
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsDesignationDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                SelectCommand="SP_MASTER_COUNTRY_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName"
                        ControlID="lblSearchItemHidden"></asp:ControlParameter>
                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue"
                        ControlID="lblSearchValueHidden"></asp:ControlParameter>
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="height: 19px; text-align: center">
        </td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: center">
            <table id="Table1" align="center">
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
        <td colspan="2">
            <table border="0" cellpadding="0" cellspacing="0" id="tblCountryDetails" runat="server"
                visible="false" width="100%">
                <tr>
                    <td colspan="2" style="text-align: left" class="profilehead">
                        General Details</td>
                </tr>
                <tr>
                    <td style="text-align: right">
                    </td>
                    <td style="text-align: left">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblDesignationName" runat="server" Text="Country Name" Width="153px"></asp:Label></td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtCountry" runat="server" MaxLength="20"></asp:TextBox>
                        <asp:Label ID="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator ID="RFVDesgName" runat="server" ControlToValidate="txtCountry"
                            ErrorMessage="Please Enter the Country Name">*</asp:RequiredFieldValidator><cc1:filteredtextboxextender id="ftxteDesignationName" runat="server" filtermode="InvalidChars"
                            invalidchars="0123456789~!@#$%^=&*()_+|}{&quot;:';/.,?><" targetcontrolid="txtCountry"></cc1:filteredtextboxextender>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                    </td>
                    <td style="text-align: left">
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <table id="tblButtons" align="center">
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
        <td style="height: 20px">
        </td>
        <td style="height: 20px;">
        </td>
    </tr>
</table>