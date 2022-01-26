<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmpMemo.aspx.cs" Inherits="HR_EmpMemo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <%--<script>
        $(function () {
            $("[name$='txtDate']").datepicker();
        });
    </script>--%>
    <script src="//tinymce.cachefly.net/4.0/tinymce.min.js"></script>

    <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="searchhead" colspan="2">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">Memo Details
                        </td>
                        <td></td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label27" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="Memo_Id">Sl No</asp:ListItem>
                                            <asp:ListItem Value="EMP_FIRST_NAME">Employee  Name</asp:ListItem>
                                            <asp:ListItem Value="DEPT_NAME">Department Name</asp:ListItem>
                                            <asp:ListItem Value="CP_FULL_NAME">Company Name</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td></td>
                                    <td>
                                        <asp:TextBox ID="txtSearchText" runat="server" CssClass="textbox">
                                        </asp:TextBox>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                                </tr>
                            </table>
                            <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                        <asp:Label ID="lblUserType" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center" aria-sort="none">
                <asp:GridView ID="gvMemoDetails" runat="server" AutoGenerateColumns="False"
                    DataSourceID="sdsDesignationDetails" AllowPaging="True" OnRowDataBound="gvMemoDetails_RowDataBound" Width="100%" SelectedRowStyle-BackColor="#c0c0c0">
                    <Columns>
                        <asp:BoundField DataField="Memo_Id" HeaderText="S.No">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Memo_No" HeaderText="Memo_No"></asp:BoundField>
                        <asp:BoundField DataField="EMP_ID" HeaderText=" Emp Id">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Employee Name">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("Desg_name") %>' ID="TextBox1"></asp:TextBox>

                            </EditItemTemplate>

                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnEmpName" runat="server" Text='<%# Bind("EMP_FIRST_NAME") %>' OnClick="lbtnEmpName_Click" ForeColor="#0066ff" CausesValidation="False" __designer:wfdid="w3"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Employee Name ">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField ReadOnly="True" DataField="DEPT_ID" HeaderText="Dept Id"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" DataField="DEPT_NAME" HeaderText="Department Name ">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CP_FULL_NAME" HeaderText="Company Name">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="Memo_Date" HeaderText="Date  ">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Reason" HeaderText="Reason"></asp:BoundField>
                        <asp:BoundField DataField="COMPANY_ID" HeaderText="CPID"></asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        No Record Found
                
                    </EmptyDataTemplate>
                    <SelectedRowStyle BackColor="LightSteelBlue" />
                </asp:GridView>
                <asp:SqlDataSource ID="sdsDesignationDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_MASTER_MEMO_SEARCH_SELECT_TOTAL" SelectCommandType="StoredProcedure">
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
            <td colspan="2" style="text-align: center" id="tblempInfo">
                <table id="Table1" align="center">
                    <tr>
                        <td>
                            <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CausesValidation="False" /></td>
                        <td>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CausesValidation="False" Visible="False" /></td>
                        <td style="width: 58px">
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"
                                CausesValidation="False" Visible="False" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center; height: 160px;">
                <table id="tblemp" runat="server" width="100%" visible="false">
                    <tr>
                        <td class="profilehead" colspan="4">Employee Details
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label2" runat="server" Text="Company Name "></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlCompanyid" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompanyid_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label1" runat="server" Text="Employe Name "></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlEmployee" runat="server" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label3" runat="server" Text="Department"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDepartment" runat="server"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label4" runat="server" Text="Designation "></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDesignation" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label5" runat="server" Text="Mobile No "></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtMobileno" runat="server"></asp:TextBox></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" id="Image1">
                <table border="0" cellpadding="0" cellspacing="0" id="tblMemoDetails" runat="server"
                    visible="false" width="100%">
                    <tr>
                        <td colspan="2" style="text-align: left" class="profilehead">Memo Details
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label7" runat="server" Text="Memo No"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtMemo" runat="server" EnableTheming="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label6" runat="server" Text="Issued Date "></asp:Label>&nbsp;</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDate" runat="server" type="datepic"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" valign="top">
                            <asp:Label ID="lblDesignationName" runat="server" Text="Reason for  the Memo " Width="140px" Height="25px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtreason" runat="server" MaxLength="20" Height="315px" Width="500px" TextMode="MultiLine" CssClass="textbox" EnableTheming="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" colspan="2"></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center"></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; height: 49px;">
                            <table id="tblButtons">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click"
                                            CausesValidation="False" /></td>
                                    <td style="width: 52px">
                                        <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" /></td>
                                    <td style="width: 18px">
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" CausesValidation="False" /></td>
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
            <td style="width: 153px"></td>
            <td style="width: 9px"></td>
        </tr>
    </table>
</asp:Content>

