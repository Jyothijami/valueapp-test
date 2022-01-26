 <%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AgentMaster.aspx.cs" Inherits="Modules_SM_AgentMaster" Title="|| YANTRA : SM : Agent Master ||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                <span style="font-size: 10pt">Agent Master</span></td>
        </tr>
    </table>
    <table  border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td id="Td20">
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left;">
                            
                            Agent Master</td>
                        <td style="text-align: right" colspan="2">
                            <asp:Label id="lblSearchBy" runat="server" CssClass="label" EnableTheming="False"
                                Font-Bold="True" Text="Search By" Height="17px"></asp:Label><asp:DropDownList id="ddlSearchBy"
                                    runat="server" CssClass="textbox" AutoPostBack="True"><asp:ListItem Value="0">--</asp:ListItem>
                                    <asp:ListItem Value="AGENT_ID">Agent Id</asp:ListItem>
                                    <asp:ListItem Value="AGENT_NAME">Agent Name</asp:ListItem>
                                    <asp:ListItem Value="AGENT_CONTACT_PERSON">Contact Person</asp:ListItem>
                                    <asp:ListItem Value="AGENT_ADDRESS">Address</asp:ListItem>
                                    <asp:ListItem Value="AGENT_PHONE">Phone No</asp:ListItem>
                                    <asp:ListItem Value="AGENT_MOBILE">Mobile No</asp:ListItem>
                                    <asp:ListItem Value="AGENT_EMAIL">Email</asp:ListItem>
                                    <asp:ListItem Value="AGENT_FAXNO">FaxNo</asp:ListItem>
                                </asp:DropDownList><asp:Label id="lblSearchtext" runat="server" CssClass="label"
                                    EnableTheming="False" Font-Bold="True" Text="Text" Height="17px"></asp:Label><asp:TextBox
                                        id="txtSearchText" runat="server" CssClass="textbox" Width="109px"></asp:TextBox><asp:Image id="imgCurrentDayTasksToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                        Visible="False"></asp:Image><asp:Button id="btnSearchGo" runat="server" BorderStyle="None"
                                            CausesValidation="False" CssClass="gobutton" EnableTheming="False" onclick="btnSearchGo_Click"
                                            Text="Go" /></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                        </td>
                        <td colspan="2" style="text-align: right">
                            <asp:Label id="lblSearchValueHidden" runat="server" Visible="False"></asp:Label><asp:Label id="lblSearchItemHidden" runat="server" Visible="False"></asp:Label><asp:Label ID="lbl" runat="server" Font-Names="Verdana"></asp:Label></td>
                    </tr>
                </table>
                </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: left">
                <itemtemplate></itemtemplate>
            </td>
        </tr>
        <tr>
            <td style="text-align: center;" colspan="4" rowspan="2">
                <asp:GridView id="gvAgentDetails" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" DataSourceID="sdsAgentDetails" OnRowDataBound="gvAgentMasterDetails_RowDataBound">
                    <columns>
<asp:BoundField DataField="AGENT_NAME" HeaderText="AgentNameHidden"></asp:BoundField>
<asp:BoundField ReadOnly="True" DataField="AGENT_ID" HeaderText="Agent ID"></asp:BoundField>
<asp:TemplateField HeaderText="Agent Name"><EditItemTemplate>
<asp:TextBox id="TextBox1" runat="server" Text='<%# Bind("Agent_name") %>' __designer:wfdid="w2"></asp:TextBox> 
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnAgentNameName" onclick="lbtnAgentName_Click" runat="server" Text="<%# Bind('Agent_Name') %>" CausesValidation="False" __designer:wfdid="w1"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="AGENT_CONTACT_PERSON" HeaderText="Contact Person">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AGENT_ADDRESS" HeaderText="Address">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AGENT_PHONE" HeaderText="Phone NO">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AGENT_MOBILE" HeaderText="Mobile NO">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AGENT_EMAIL" HeaderText="Email">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AGENT_FAXNO" HeaderText="Fax NO">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:BoundField>
</columns>
                    <emptydatatemplate>
No Data Exist!
</emptydatatemplate>
                    <selectedrowstyle backcolor="LightSteelBlue" />
                </asp:GridView><asp:SqlDataSource id="sdsAgentDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SM_AGENT_SEARCH_SELECT" SelectCommandType="StoredProcedure"><selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
</selectparameters></asp:SqlDataSource>
               </td>
        </tr>
        <tr>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; height: 75px;">
                <table id="Table1">
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
            <td id="tblAgentDetails" runat="server" visible="false" colspan="4" rowspan="2" style="height: 281px; text-align: center">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
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
                <asp:Label id="lblAgentName" runat="server" Text="Agent Name" Width="119px"></asp:Label></td>
            <td style="text-align: left;"><asp:TextBox id="txtAgentName" runat="server"></asp:TextBox>
                <asp:Label id="Label36" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                            </asp:Label>
                <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtAgentName"
                    ErrorMessage="Please Enter the Agent Name">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator id="RegularExpressionValidator2" runat="server" ControlToValidate="txtAgentName"
                    ErrorMessage="Plese Enter  Only Alphabets" ValidationExpression="^[a-zA-Z]+$">*</asp:RegularExpressionValidator></td>
            <td style="text-align: right;"><asp:Label id="lblContactPerson" runat="server" Text="Contact Person" Width="119px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtContactPerson" runat="server">
            </asp:TextBox>
                <asp:Label id="Label4" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                            </asp:Label>
                <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtContactPerson"
                    ErrorMessage="Please enter the Contact Person">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                        id="RegularExpressionValidator3" runat="server" ControlToValidate="txtContactPerson"
                        ErrorMessage="Plese Enter Only Alphabets" ValidationExpression="^[a-zA-Z]+$">*</asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                <asp:Label id="lblAddress" runat="server" Text="Address" Width="105px"></asp:Label></td>
            <td style="text-align: left"><asp:TextBox id="txtAddress" runat="server" TextMode="MultiLine">
            </asp:TextBox></td>
            <td style="text-align: right">
                <asp:Label id="lblContactPersonDetails" runat="server" Text="Contact Person Details" Width="152px"></asp:Label></td>
            <td style="text-align: left"><asp:TextBox id="txtContactPersonDetails" runat="server" TextMode="MultiLine">
            </asp:TextBox></td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                <asp:Label id="lblContactNo1" runat="server" Text="Phone No." Width="96px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtContactNo1" runat="server"></asp:TextBox>
                <asp:Label id="Label6" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                            </asp:Label>
                <asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" ControlToValidate="txtContactNo1"
                    ErrorMessage="Please Enter the Phone No">*</asp:RequiredFieldValidator>
                <cc1:FilteredTextBoxExtender ID="ftxtePhoneNo" runat="server" TargetControlID="txtContactNo1"
                    ValidChars="-0123456789">
                </cc1:FilteredTextBoxExtender>
            </td>
            <td style="text-align: right">
                <asp:Label id="lblContactNo2" runat="server" Text="Mobile No." Width="96px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtContactNo2" runat="server"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="ftxteMobileNo" runat="server" TargetControlID="txtContactNo2"
                    ValidChars="-0123456789">
                </cc1:FilteredTextBoxExtender>
            </td>
                    </tr>
                    <tr>
            <td style="text-align: right; height: 24px;">
                <asp:Label id="lblEmail" runat="server" Text="Email"></asp:Label></td>
            <td style="text-align: left; height: 24px;">
                <asp:TextBox id="txtEmail" runat="server">
            </asp:TextBox>
                <asp:Label id="Label7" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                </asp:Label><asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server"
                    ControlToValidate="txtEmail" ErrorMessage="please Enter Eamil Id">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="Please enter the email  in  correct format" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td>
            <td style="text-align: right; height: 24px;">
                <asp:Label id="lblFaxNo" runat="server" Text="Fax No" Width="119px"></asp:Label></td>
            <td style="text-align: left; height: 24px;">
                <asp:TextBox id="txtFaxNo" runat="server"></asp:TextBox><%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit"
                    tagprefix="cc1" %>
                <cc1:FilteredTextBoxExtender ID="ftxteFaxNo" runat="server" FilterType="Numbers"
                    TargetControlID="txtFaxNo">
                </cc1:FilteredTextBoxExtender>
            </td>
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
                        <td colspan="4" style="height: 19px; text-align: center">
                <table id="tblButtons">
                    <tr>
                        <td>
                            <asp:Button id="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"  /></td>
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
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <asp:ValidationSummary id="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False">
                </asp:ValidationSummary>
            </td>
        </tr>
        <tr>
            <td style="height: 21px">
            </td>
            <td style="height: 21px;">
                <table id="tblHiddenFields" style="width: 107px" runat="server" visible="false">
                    <tr>
                        <td>
                <asp:Label id="lblPANNo" runat="server" Text="PAN No" Width="108px" Visible="False"></asp:Label></td>
                        <td>
                <asp:TextBox id="txtPANNo" runat="server" Visible="False"></asp:TextBox><cc1:FilteredTextBoxExtender ID="ftxtePANNo" runat="server" FilterType="Numbers"
                    TargetControlID="txtPANNo">
                </cc1:FilteredTextBoxExtender>
                        </td>
                        <td>
                <asp:Label id="lblCSTNo" runat="server" Text="CST No" Width="148px" Visible="False"></asp:Label></td>
                        <td style="width: 20px">
                <asp:TextBox id="txtCSTNo" runat="server" Visible="False"></asp:TextBox><cc1:FilteredTextBoxExtender ID="ftxteCSTNo" runat="server" FilterType="Numbers"
                    TargetControlID="txtCSTNo">
                </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                <asp:Label id="lblVATNo" runat="server" Text="VAT No" Width="96px" Visible="False"></asp:Label></td>
                        <td>
                <asp:TextBox id="txtVATNo" runat="server" Visible="False"></asp:TextBox><cc1:FilteredTextBoxExtender ID="ftxteVATNo" runat="server" FilterType="Numbers"
                    TargetControlID="txtVATNo">
                </cc1:FilteredTextBoxExtender>
                        </td>
                        <td>
                <asp:Label id="lblECCNo" runat="server" Text="ECC No" Width="96px" Visible="False"></asp:Label></td>
                        <td style="width: 20px">
                <asp:TextBox id="txtECCNo" runat="server" Visible="False"></asp:TextBox><cc1:FilteredTextBoxExtender ID="ftxteECCNo" runat="server" FilterType="Numbers"
                    TargetControlID="txtECCNo">
                </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                <asp:Label id="lblRanking" runat="server" Text="Ranking" Width="96px" Visible="False"></asp:Label></td>
                        <td>
                <asp:TextBox id="txtRanking" runat="server" Visible="False"></asp:TextBox><cc1:FilteredTextBoxExtender ID="ftxteRanking" runat="server" FilterType="Numbers"
                    TargetControlID="txtRanking">
                </cc1:FilteredTextBoxExtender>
                        </td>
                        <td>
                            <asp:Label id="Label1" runat="server" Text="Brand" Width="96px" Visible="False"></asp:Label></td>
                        <td style="width: 20px">
                            <asp:TextBox id="txtBrand" runat="server" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td style="width: 20px">
                        </td>
                    </tr>
                    <tr>
                        <td>
                        <asp:Label id="Label3" runat="server" Text="Item Type" Width="96px" Visible="False"></asp:Label></td>
                        <td>
                <asp:DropDownList id="ddlItemType" runat="server" AutoPostBack="True" Visible="False">
                </asp:DropDownList></td>
                        <td>
                            <asp:Label id="Label2" runat="server" Text="Item Name" Width="96px" Visible="False"></asp:Label></td>
                        <td style="width: 20px">
                <asp:DropDownList id="ddlItemName" runat="server" AutoPostBack="True" Visible="False">
                </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label id="Label5" runat="server" Text="UOM" Visible="False"></asp:Label></td>
                        <td>
                            <asp:TextBox id="txtItemUOM" runat="server" ReadOnly="True" Visible="False"></asp:TextBox></td>
                        <td>
                        </td>
                        <td style="width: 20px">
                        </td>
                    </tr>
                </table>
            </td>
            <td style="height: 21px;">
            </td>
            <td style="height: 21px">
            </td>
        </tr>
    </table>
</asp:Content>


 
