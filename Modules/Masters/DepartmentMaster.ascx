<%@ Control Language="C#"  AutoEventWireup="true" CodeFile="DepartmentMaster.ascx.cs" Inherits="Modules_Masters_DepartmentMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%--<link href="../../App_Themes/Master/Master.css" rel="stylesheet" type="text/css" />--%> 

 <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="searchhead" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left;">
                            Department Master</td>
                        <td>
                        </td>
                        <td style="text-align: right;">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td rowspan="3">
                                        <asp:Label id="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td rowspan="3">
                                        <asp:DropDownList id="ddlSearchBy" runat="server" CssClass="textbox" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="DEPT_ID">Department ID</asp:ListItem>
                                            <asp:ListItem Value="DEPT_NAME">Department Name</asp:ListItem>
                                            <asp:ListItem Value="DEPT_HEAD">Head Of The Department</asp:ListItem>
                                            <asp:ListItem Value="DEPT_DESC">Description</asp:ListItem>
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td rowspan="3">
                                        </td>
                                    <td rowspan="3">
                                        <asp:TextBox id="txtSearchText" runat="server" CssClass="textbox"></asp:TextBox>&nbsp;
                                    </td>
                                    <td rowspan="3">
                                        <asp:Button id="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnSearchGo_Click" /></td>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                </tr>
                            </table>
                            <asp:Label id="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:GridView id="gvDepartmentDetails" SelectedRowStyle-BackColor="#c0c0c0" runat="server" AutoGenerateColumns="False"
                    DataSourceID="sdsDepartmentDetails" AllowPaging="True" OnRowDataBound="gvDepartmentDetails_RowDataBound" Width="100%">
                    <columns>
                        <asp:BoundField DataField="DEPT_NAME" HeaderText="DepartmentNameHidden" SortExpression="DEPT_NAME" />
<asp:BoundField DataField="Dept_id" HeaderText="S.No">
<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Department Name"><EditItemTemplate>
<asp:TextBox runat="server" Text='<%# Bind("Dept_name") %>' id="TextBox1"></asp:TextBox>
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnDepartmentName" onclick="lbtnDepartmentName_Click" ForeColor="#0066ff" runat="server" Text="<%# Bind('Dept_Name') %>" CausesValidation="False"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Head Of The Department">
    <ItemStyle HorizontalAlign="Left" />
    <HeaderStyle HorizontalAlign="Left" />
</asp:BoundField>
<asp:BoundField DataField="Dept_desc" HeaderText="Description" NullDisplayText="-">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
</columns>
                    <selectedrowstyle backcolor="LightSteelBlue" />
                    <EmptyDataTemplate>
                        No Record Found
                    </EmptyDataTemplate>
                </asp:GridView><asp:SqlDataSource id="sdsDepartmentDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_MASTER_DEPT_SEARCH_SELECT" SelectCommandType="StoredProcedure"><selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
</selectparameters></asp:SqlDataSource>
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
                            <asp:Button id="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CausesValidation="False" /></td>
                        <td>
                            <asp:Button id="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CausesValidation="False" /></td>
                        <td>
                            <asp:Button id="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" CausesValidation="False" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <table style="width: 100%" border="0" cellpadding="0" cellspacing="0" id="tblDeptDetails" runat="server" visible="false">
                    <tr>
            <td colspan="2" style="text-align: left" class="profilehead">
                General Details</td>
                    </tr>
                    <tr>
            <td style="height: 21px; text-align: right">
            </td>
            <td style="height: 21px; text-align: left">
            </td>
                    </tr>
                    <tr>
            <td style="text-align: right"><asp:Label id="lblDepartmentName" runat="server" Text="Department Name" Width="153px"></asp:Label></td>
            <td style="text-align: left;"><asp:TextBox id="txtDepartmentName" runat="server" ></asp:TextBox>
                <asp:Label ID="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                <asp:RequiredFieldValidator id="RFVDeptName" runat="server" ControlToValidate="txtDepartmentName"
                    ErrorMessage="Please Enter the Department Name">*</asp:RequiredFieldValidator><cc1:filteredtextboxextender id="ftxteDepartmentName" runat="server" filtermode="InvalidChars"
                    invalidchars="0123456789~!@#$%^=&*()_+|}{&quot;:';/.,?><" targetcontrolid="txtDepartmentName"></cc1:filteredtextboxextender>
            </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblDepartmentHead" runat="server" Text="Department Head" Width="153px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlDeptHead" runat="server" Width="151px">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                <asp:Label id="lblDescription" runat="server" Text="Description" Width="105px"></asp:Label></td>
            <td style="text-align: left"><asp:TextBox id="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                    </tr>
                    <tr>
            <td style="text-align: right; height: 19px;">
            </td>
            <td style="text-align: left; height: 19px;">
            </td>
                    </tr>
                    <tr>
            <td colspan="2" style="text-align: center">
                <table id="tblButtons" align="center">
                    <tr>
                        <td>
                            <asp:Button id="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                        <td>
                            <asp:Button id="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" CausesValidation="False" /></td>
                        <td>
                            <asp:Button id="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" /></td>
                    </tr>
                </table>
            </td>
                    </tr>
                </table>
                <asp:ValidationSummary id="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False">
                </asp:ValidationSummary>
            </td>
        </tr>
        <tr>
            <td style="height: 21px">
            </td>
            <td style="height: 21px;">
            </td>
        </tr>
    </table>