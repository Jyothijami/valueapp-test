<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Circular1.aspx.cs" Inherits="Modules_HR_Circular1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">

        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td class="searchhead" colspan="2">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">Circular </td>
                        <td></td>
                        <td>
                          <%--  <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label27" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="CIR_ID">Sl No</asp:ListItem>
                                            <asp:ListItem Value="DEPT_NAME">Department Name</asp:ListItem>
                                            <asp:ListItem Value="CP_FULL_NAME">Company Name</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td></td>
                                    <td>
                                        <asp:TextBox ID="txtSearchText" runat="server" CssClass="textbox">
                                        </asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                                </tr>
                            </table>--%>
                            <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
                              <asp:Label ID="lblUserType" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblCPID" runat="server" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="gvCircular" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="sdsDesignationDetails" OnRowDataBound="gvCircular_RowDataBound" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="CIR_ID" HeaderText="S.No">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Cir_No">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnCirNo" OnClick="lbtnCirNo_Click" ForeColor="Blue" runat="server" Text='<%# Bind("CIR_NO") %>' __designer:wfdid="w4"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField ReadOnly="True" DataField="DEPT_ID" HeaderText="Dept Id"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" DataField="DEPT_NAME" HeaderText="Department Name ">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="COMPANY_ID" HeaderText="Cpid"></asp:BoundField>
                        <asp:BoundField DataField="CP_FULL_NAME" HeaderText="Company Name">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="CIR_DATE" HeaderText="Date  ">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="DESCRIPTION" HeaderText="Description"></asp:BoundField>
                        <asp:BoundField DataField="CIR_NO" SortExpression="CIR_NO" HeaderText="CirNo"></asp:BoundField>
                        <asp:BoundField DataField="EMP_ID" HeaderText="empId"></asp:BoundField>
                        <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Employee Name"></asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        No Record Found
                
                    </EmptyDataTemplate>
                    <SelectedRowStyle BackColor="LightSteelBlue" />
                </asp:GridView>
                <asp:SqlDataSource ID="sdsDesignationDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_HR_CIRCULAR_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
                        <asp:ControlParameter ControlID="lblEmpIdHidden" DefaultValue="0" Name="EmpId" PropertyName="Text" Type="Int64" />
                        <asp:ControlParameter ControlID="lblUserType" DefaultValue="0" Name="userType" PropertyName="Text" Type="Int64" />
                        <asp:ControlParameter ControlID="lblCPID" DefaultValue="0" Name="CpId" PropertyName="Text" Type="Int64" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td id="tblempInfo" colspan="2">
               <%-- <table id="Table1" align="center">
                    <tr>
                        <td>
                            <asp:Button ID="btnNew" runat="server" CausesValidation="False" OnClick="btnNew_Click"
                                Text="New" /></td>
                        <td>
                            <asp:Button ID="btnEdit" runat="server" CausesValidation="False" OnClick="btnEdit_Click"
                                Text="Edit" /></td>
                        <td style="width: 58px">
                            <asp:Button ID="btnDelete" runat="server" CausesValidation="False" OnClick="btnDelete_Click"
                                Text="Delete" /></td>
                    </tr>
                </table>--%>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 160px; text-align: center">
                <table id="tblemp" runat="server" visible="false" width="100%">
                    <tr>
                        <td class="profilehead" colspan="4">&nbsp;Employee Details
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label2" runat="server" Text="Company Name :"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlCompanyid" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompanyid_SelectedIndexChanged">
                            </asp:DropDownList></td>

                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label3" runat="server" Text="Department :"></asp:Label></td>
                        <td style="text-align: left" colspan="3">
                            <asp:DropDownList ID="ddlDept" runat="server" Visible="false" AutoPostBack="True" Font-Bold="False"
                                OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:CheckBoxList ID="CheckBoxList1" Width="100%" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="DEPT_NAME" DataValueField="DEPT_ID" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged" RepeatColumns="7" RepeatDirection="Horizontal">
                                    </asp:CheckBoxList>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [DEPT_ID], [DEPT_NAME] FROM [YANTRA_DEPT_MAST]"></asp:SqlDataSource>
                                    <br />

                                    <asp:ListBox ID="ListBox1" runat="server" Rows="5" SelectionMode="Multiple"></asp:ListBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>

                    <%--<tr>
                        <td style="text-align: right">
                            <asp:Label id="Label8" runat="server" Text="Departments :"></asp:Label></td>
                        <td style="text-align: left" colspan="3">
                            <asp:CheckBoxList ID="chkDept" runat="server" RepeatDirection="Horizontal" RepeatColumns="5" Width="90%">
                                <asp:ListItem>CEO</asp:ListItem>
                                <asp:ListItem>CMD</asp:ListItem>
                                <asp:ListItem>Customer Care</asp:ListItem>
                                <asp:ListItem>EDP</asp:ListItem>
                                <asp:ListItem>Finance</asp:ListItem>
                                <asp:ListItem>General</asp:ListItem>
                                <asp:ListItem>HR &amp; Admin</asp:ListItem>
                                <asp:ListItem>Office Assistance</asp:ListItem>
                                <asp:ListItem>Purchases</asp:ListItem>
                                <asp:ListItem>Sales-Marketing</asp:ListItem>
                                <asp:ListItem>Secretariat</asp:ListItem>
                                <asp:ListItem>Stores</asp:ListItem>
                                <asp:ListItem>Technical</asp:ListItem>
                            </asp:CheckBoxList>
                            </td>
                       
                    </tr>--%>

                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label1" Visible="false" runat="server" Text="Employe Name :"></asp:Label>
                            <asp:Label ID="Label4" runat="server" Visible="false" Text="Designation :"></asp:Label>

                        </td>


                        <td style="text-align: left">
                            <asp:TextBox ID="txtDesignation" Visible="false" runat="server">
                            </asp:TextBox>
                            <asp:DropDownList ID="ddlEmployee" Visible="false" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                            </asp:DropDownList>

                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label5" runat="server" Visible="false" Text="Mobile No :"></asp:Label>

                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtMobileno" Visible="false" runat="server">
                            </asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td id="Image1" colspan="2">
                <table id="tblcircular" runat="server" border="0" cellpadding="0" cellspacing="0"
                    visible="false" width="100%">
                    <tr>
                        <td class="profilehead" colspan="2" style="text-align: left">Circular Details
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label7" runat="server" Text="Circular  No :"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtcirNo" runat="server" EnableTheming="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label6" runat="server" Text="Issued Date :"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDate" runat="server" type="date">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right" valign="top">
                            <asp:Label ID="lblDesignationName" runat="server" Height="22px" Text="Description :"
                                Width="80px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtdescription" runat="server" EnableTheming="True" Width="500px"></asp:TextBox></td>


                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: right"></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center"></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 49px; text-align: center">
                            <%--<table id="tblButtons" align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" /></td>
                                    <td>
                                        <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" OnClick="btnRefresh_Click"
                                            Text="Refresh" /></td>
                                    <td style="width: 52px">
                                        <asp:Button ID="btnClose" runat="server" CausesValidation="False" OnClick="btnClose_Click"
                                            Text="Close" /></td>
                                    <td style="width: 18px">&nbsp;</td>
                                </tr>
                            </table>--%>
                        </td>
                    </tr>
                </table>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False"></asp:ValidationSummary>
                

            </td>
        </tr>
        <tr>
            <td style="width: 153px"></td>
            <td style="width: 9px"></td>
        </tr>
    </table>
</asp:Content>


 
