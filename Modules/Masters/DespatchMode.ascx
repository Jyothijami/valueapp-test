<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DespatchMode.ascx.cs" Inherits="Modules_Masters_DespatchMode" %>

    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="searchhead" colspan="2" style="text-align: left;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left;">
                            Despatch Mode Master Details</td>
                        <td>
                        </td>
                        <td style="text-align: right;">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Label id="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:DropDownList id="ddlSearchBy" runat="server" CssClass="textbox" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="DESPM_ID">Despatch ID</asp:ListItem>
                                            <asp:ListItem Value="DESPM_NAME">Despatch Name</asp:ListItem>
                                            <asp:ListItem Value="DESPM_DESC">Description</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <%--<td rowspan="3" style="width: 17px; height: 25px;">
                                        <asp:Label id="lblSearchtext" runat="server" CssClass="label" Font-Bold="True" Text="Text" Width="37px" EnableTheming="False"></asp:Label></td>--%>
                                    <td rowspan="3" style="width: 150px; height: 25px;">
                                        <asp:TextBox id="txtSearchText" runat="server" CssClass="textbox"></asp:TextBox><asp:Image id="imgCurrentDayTasksToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image></td>
                                    <td rowspan="3" style="height: 25px">
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
                <asp:GridView id="gvDespatchDetails" SelectedRowStyle-BackColor="#c0c0c0" runat="server" AutoGenerateColumns="False"
                    DataSourceID="sdsDespatchDetails" AllowPaging="True" OnRowDataBound="gvDespatchDetails_RowDataBound" AllowSorting="True" Width="100%">
                    <columns>
<asp:BoundField DataField="DESPM_NAME" HeaderText="DespatchNameHidden"></asp:BoundField>
<asp:BoundField DataField="DESPM_ID" HeaderText="Despatch ID"></asp:BoundField>
<asp:TemplateField HeaderText="Despatch Name"><EditItemTemplate>
<asp:TextBox runat="server" Text='<%# Bind("Despm_name") %>' id="TextBox1"></asp:TextBox>
</EditItemTemplate>
<ItemTemplate>
<asp:LinkButton id="lbtnDespatchName" runat="server" ForeColor="#0066ff" Text="<%# Bind('Despm_Name') %>" OnClick="lbtnDespatchName_Click" CausesValidation="False"></asp:LinkButton> 
</ItemTemplate>
    <ItemStyle HorizontalAlign="Left" />
</asp:TemplateField>
<asp:BoundField DataField="DESPM_DESC" HeaderText="Description" NullDisplayText="-">
    <ItemStyle HorizontalAlign="Left" />
</asp:BoundField>
</columns>
                    <selectedrowstyle backcolor="LightSteelBlue" />
                    <EmptyDataTemplate>
                        No Record Found
                    </EmptyDataTemplate>
                </asp:GridView>
                <asp:SqlDataSource ID="sdsDespatchDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_MASTER_DESPMODE_SEARCH_SELECT" SelectCommandType="StoredProcedure">
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
                <table border="0" cellpadding="0" cellspacing="0" id="tblDespatchDetails"  runat="server" visible="false" width="100%">
                    <tr>
                        <td id="tblDespmDetails" runat="server" colspan="2">
                            <table  border="0" cellpadding="0" cellspacing="0" id="Table2" runat="server" visible="true" width="100%">
                <tr>
            <td colspan="2" style="text-align: left;" class="profilehead">
                General Details</td>
                </tr>
                <tr>
            <td style="height: 21px; text-align: right">
            </td>
            <td style="height: 21px; text-align: left">
            </td>
                </tr>
                <tr>
            <td style="text-align: right; height: 24px;">
                &nbsp; &nbsp; &nbsp;&nbsp;
                <asp:Label id="lblDespatchName" runat="server" Text="Despatch Name" Width="101px"></asp:Label></td>
            <td style="text-align: left; height: 24px;"><asp:TextBox id="txtDespatchName" runat="server" Width="172px" MaxLength="20"></asp:TextBox>
                <asp:Label ID="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                <asp:RequiredFieldValidator ID="RFVDespName" runat="server" ControlToValidate="txtDespatchName"
                    ErrorMessage="Please Enter the Despatch Name">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
            <td style="text-align: right">
                <asp:Label id="lblDescription" runat="server" Text="Description" Width="79px"></asp:Label></td>
            <td style="text-align: left"><asp:TextBox id="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox></td>
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
            </td>
        </tr>
        <tr>
            <td >
            </td>
            <td style="width: 750px">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" />
            </td>
        </tr>
    </table>
       