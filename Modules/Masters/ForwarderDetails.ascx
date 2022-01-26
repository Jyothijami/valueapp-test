<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ForwarderDetails.ascx.cs" Inherits="Modules_Masters_ForwarderDetails" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>

<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td class="searchhead" colspan="2">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="text-align: left">
                        Terms and Conditions</td>
                    <td>
                    </td>
                    <td style="text-align: right">
                        <table border="0" cellpadding="0" cellspacing="0" align="right">
                            <tr>
                                <td rowspan="3">
                                    <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                        Text="Search By"></asp:Label></td>
                                <td rowspan="3">
                                    <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                        OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--</asp:ListItem>
                                        <asp:ListItem Value="ID">ID</asp:ListItem>
                                        <asp:ListItem Value="Title">Title</asp:ListItem>
                                        <asp:ListItem Value="Message">Terms</asp:ListItem>
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
        <td colspan="2" style="text-align: center">
            <asp:GridView ID="gvForwarderDetails" SelectedRowStyle-BackColor="#c0c0c0" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" DataSourceID="sdsForwarderDetails" OnRowDataBound="gvForwarderDetails_RowDataBound" Width="100%">
                <Columns>
                    <%--<asp:BoundField DataField="Forwarder_Name" HeaderText="Name" />--%>
                    <asp:BoundField DataField="Id" HeaderText="S.No">
                        <ItemStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Title">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Title") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnForwarderName" ForeColor="#0066ff" runat="server" CausesValidation="False" OnClick="lbtnCurrencyName_Click"
                                Text='<%# Bind("Title") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Message" HeaderText="Terms and Conditions">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <%--<asp:BoundField DataField="Address" HeaderText="Address" NullDisplayText="-">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Email" HeaderText="Email" />--%>
                </Columns>
                <EmptyDataTemplate>
                    <span style="color: #cc0000">No Record Found </span>
                </EmptyDataTemplate>
                <SelectedRowStyle BackColor="LightSteelBlue" />
            </asp:GridView>
            <asp:SqlDataSource ID="sdsForwarderDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                SelectCommand="SELECT * FROM terms_Conditions" SelectCommandType="Text">
                <SelectParameters>
                    <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName"
                        PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue"
                        PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            &nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: left">
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
                            Text="Edit" Width="37px" /></td>
                    <td>
                        <asp:Button ID="btnDelete" runat="server" CausesValidation="False" OnClick="btnDelete_Click"
                            Text="Delete" /></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="height: 19px; text-align: right">
        </td>
        <td>
            <table id="tblCurrencyDetails" runat="server" border="0" cellpadding="0" cellspacing="0"
                visible="false" width="100%">
                <tr>
                    <td class="profilehead" colspan="2" style="text-align: left">
                        Terms</td>
                </tr>
                <tr>
                    <td style="text-align: right">
                    </td>
                    <td style="text-align: left">
                    </td>
                </tr>
               <%-- <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblCurrencyName" runat="server" Text="Forwarder Name"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtForwarderName" runat="server" MaxLength="20" Width="133px"></asp:TextBox><asp:Label
                            ID="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator ID="RFVCurrencyName" runat="server" ControlToValidate="txtForwarderName"
                            ErrorMessage="Please Enter the Forwarder Name">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblCurrencyFullName" runat="server" Text="Phone"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtPhone" runat="server" Width="144px"></asp:TextBox>
            <asp:Label ID="Label24" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
            <asp:RequiredFieldValidator
                ID="RFVContactNo" runat="server" ControlToValidate="txtPhone" ErrorMessage="Please Enter the phone No ">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                    ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPhone" ErrorMessage="Please Enter only Numbers in phone No"
                    ValidationExpression="^[0-9. ]*$">*</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblDescription" runat="server" Text="Email"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtEmail" runat="server" Width="144px"></asp:TextBox>
                        
            <asp:Label ID="Label4" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
            <asp:RequiredFieldValidator ID="RFVEmail" runat="server" ControlToValidate="txtEmail"
                ErrorMessage="Please Enter the Email">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                    ID="REVEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Please Enter Email in Correct Format(Eg : abc@def.com)"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="Label2" runat="server" Text="Address"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                </tr>--%>
                 <tr>
                <td style="text-align: right">
                    <label>Tiltle:</label>
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txttermtitle"  runat="server"></asp:TextBox>
                </td>
            </tr>

               <tr>
                <td style="text-align: right">
                    <label>Terms & Conditions Points:</label>
                </td>
                <td style="text-align: left;">
                    <FTB:FreeTextBox ID="FreeTextBox1"  EnableHtmlMode="false"  runat="server"></FTB:FreeTextBox>
                </td>
            </tr>
              
                <tr>
                    <td style="height: 19px; text-align: right">
                    </td>
                    <td style="height: 19px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 8px; text-align: center">
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
                <tr>
                    <td colspan="2" style="height: 8px; text-align: center">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
