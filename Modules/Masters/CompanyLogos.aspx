<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyLogos.aspx.cs" Inherits="Modules_Masters_CompanyLogos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>|| Value App</title>
    <link href="../../App_Themes/Master/Master.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td class="searchhead" colspan="4" style="text-align: left">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 272px; text-align: left">
                                Company Logos</td>
                            <td style="text-align: right">
                                <table border="0" cellpadding="0" cellspacing="0" align="right">
                                    <tr>
                                        <td rowspan="3">
                                            <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                                Text="Search By" Width="90px"></asp:Label></td>
                                        <td rowspan="3">
                                            <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="textbox">
                                                <asp:ListItem Value="0">--</asp:ListItem>
                                                <asp:ListItem Value="CL_ID">Logos Id</asp:ListItem>
                                                <asp:ListItem Value="CL_COMPANY_NAME">Company  Name</asp:ListItem>
                                                <asp:ListItem Value="CL_DESCRIPTION">Description</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td rowspan="3">
                                        </td>
                                        <td rowspan="3">
                                            <asp:TextBox ID="txtSearchText" runat="server" CssClass="textbox">
                                        </asp:TextBox><asp:Image ID="imgCurrentDayTasksToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                                Visible="False" /></td>
                                        <td rowspan="3">
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
                <td colspan="4" style="text-align: left">
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 21px; text-align: center">
                    <asp:GridView ID="gvConpanyLogos" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" DataSourceID="sdsCompanyLogos" OnRowDataBound="gvConpanyLogos_RowDataBound" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="CL_COMPANY_NAME" HeaderText="ConpanyNameHidden" />
                            <asp:BoundField DataField="CL_ID" HeaderText="Conpany Logos Id">
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Company Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("cl_Company_Name") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnCompanyName" runat="server" ForeColor="#0066ff" CausesValidation="False" OnClick="lbtnCompanyName_Click"
                                        Text="<%# Bind('cl_Company_Name') %>"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CL_DESCRIPTION" HeaderText="Description" NullDisplayText="-">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                        <SelectedRowStyle BackColor="LightSteelBlue" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsCompanyLogos" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                        SelectCommand="SP_MASTER_COMPANY_LOGOS_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName"
                                PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue"
                                PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 51px; text-align: center">
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
                    <table id="tblConpanyLogos" runat="server" border="0" cellpadding="0" cellspacing="0"
                        visible="false" width="100%">
                        <tr>
                            <td class="profilehead" colspan="4" style="height: 20px; text-align: left">
                                General Details</td>
                            <td class="profilehead" colspan="1" style="height: 20px; text-align: left">
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                            </td>
                            <td style="text-align: left">
                            </td>
                            <td style="text-align: right">
                            </td>
                            <td style="text-align: left">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 24px; text-align: right">
                                <asp:Label ID="lblCompanyName" runat="server" Text="Company Name"></asp:Label></td>
                            <td style="height: 24px; text-align: left">
                                <asp:TextBox ID="txtCompanyName" runat="server"></asp:TextBox>
                                <asp:Label ID="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCompanyName"
                                    ErrorMessage="Please Enter the Company Name">*</asp:RequiredFieldValidator></td>
                            <td style="height: 24px; text-align: right">
                            </td>
                            <td style="height: 24px; text-align: left">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label></td>
                            <td colspan="3" style="text-align: left">
                                <asp:TextBox ID="txtDescription" runat="server" CssClass="multilinetext" EnableTheming="False"
                                    TextMode="MultiLine" Width="449px"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lblCompanyLogos" runat="server" Text="Company Logos"></asp:Label></td>
                            <td colspan="3" style="text-align: left">
                                <asp:FileUpload ID="FileUpload1" runat="server" EnableTheming="True" Font-Names="Verdana"
                                    Font-Size="8pt" ForeColor="#404040" Style="margin-left: 2px; margin-right: 3px"
                                    Width="478px" />
                                <asp:Label ID="Label1" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="FileUpload1"
                                    ErrorMessage="Please Select Company Logo">*</asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="Label6" runat="server" Text="Attached Files"></asp:Label></td>
                            <td colspan="3" style="text-align: left">
                                <asp:LinkButton ID="lbtnAttachedFile" runat="server"></asp:LinkButton></td>
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
                            <td colspan="5" rowspan="2" style="text-align: center">
                                <table align="center">
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
                        <tr>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="False" />
                </td>
            </tr>
            </table>
    
    </div>
    </form>
</body>
</html>

 
