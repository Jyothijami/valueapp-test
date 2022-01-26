<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CustomerInformation.aspx.cs" Inherits="Modules_SM_CustomerInformation"
    Title="|| ERP : SM : Customer Information ||" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                Customer Details</td>
             <td>
                <asp:DropDownList ID="ddlNoOfRecords" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged">
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>75</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                   
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td colspan="4" style="text-align: left; width: 944px; height: 25px;" class="searchhead">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">
                            Customer Details</td>
                        <td>
                        </td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="height: 25px">
                                        <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By" meta:resourcekey="Label14Resource1"></asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="textbox" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged" OnTextChanged="ddlSearchBy_TextChanged" meta:resourcekey="ddlSearchByResource1">
                                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource1" Text="--"></asp:ListItem>
                                            <asp:ListItem Value="CUST_CODE" meta:resourcekey="ListItemResource2" Text="Customer Code"></asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME" meta:resourcekey="ListItemResource3" Text="Customer Name"></asp:ListItem>
                                            <asp:ListItem Value="REG_NAME" meta:resourcekey="ListItemResource4" Text="Region"></asp:ListItem>
                                            <asp:ListItem Value="CUST_CONTACT_PERSON" meta:resourcekey="ListItemResource5" Text="Contact Person"></asp:ListItem>
                                            <asp:ListItem Value="CUST_ADDRESS" meta:resourcekey="ListItemResource6" Text="Customer Address"></asp:ListItem>
                                            <asp:ListItem Value="CUST_EMAIL" meta:resourcekey="ListItemResource7" Text="Email"></asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="width: 17px; height: 25px;">
                                        <asp:Label ID="lblSearchtext" runat="server" CssClass="label" EnableTheming="False"
                                            Font-Bold="True" Text="Text" Width="37px" meta:resourcekey="lblSearchtextResource1"></asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchText" runat="server" CssClass="textbox" meta:resourcekey="txtSearchTextResource1"></asp:TextBox><asp:Image ID="imgCurrentDayTasksToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False" meta:resourcekey="imgCurrentDayTasksToDateResource1"></asp:Image></td>
                                    <td style="height: 25px">
                                        <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnSearchGo_Click" meta:resourcekey="btnSearchGoResource1" /></td>
                                </tr>
                                </table>
                            <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False" meta:resourcekey="lblSearchItemHiddenResource1"></asp:Label>
                            <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False" meta:resourcekey="lblSearchValueHiddenResource1"></asp:Label>
                            <asp:Label ID="lblCPID" runat="server" Visible="False" meta:resourcekey="lblSearchValueHiddenResource1"></asp:Label>
                            <asp:Label ID="lblUserType" runat="server" Visible="False" meta:resourcekey="lblSearchValueHiddenResource1"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="text-align: center; width: 944px;" colspan="4">
                <asp:GridView ID="gvCustMasterDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    Width="100%" DataSourceID="sdsCustMaster" OnRowDataBound="gvCustMasterDetails_RowDataBound" 
                    meta:resourcekey="gvCustMasterDetailsResource1" AllowSorting="True" >
                    <Columns>
<asp:BoundField DataField="CUST_NAME" SortExpression="CUST_NAME" HeaderText="CustNameHidden" meta:resourceKey="BoundFieldResource1"></asp:BoundField>
<asp:BoundField DataField="CUST_CODE" SortExpression="CUST_CODE" HeaderText="Customer Code" meta:resourceKey="BoundFieldResource2">
<ControlStyle Width="100px"></ControlStyle>

<ItemStyle Width="110px" HorizontalAlign="Right"></ItemStyle>

<HeaderStyle Width="100px" HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Customer Name" SortExpression="CUST_NAME" meta:resourceKey="TemplateFieldResource1"><EditItemTemplate>
<asp:TextBox runat="server" Text='<%# Bind("CUST_NAME") %>' ID="TextBox1" meta:resourceKey="TextBox1Resource1"></asp:TextBox>


                            
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton runat="server" ID="lbtnCustMaster" Text="<%# Bind('CUST_NAME') %>" CausesValidation="False" meta:resourceKey="lbtnCustMasterResource1" OnClick="lbtnCustMaster_Click"></asp:LinkButton>


                            
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="CUST_COMPANY_NAME" SortExpression="CUST_COMPANY_NAME" HeaderText="Company Name" meta:resourceKey="BoundFieldResource3">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="REG_NAME" SortExpression="REG_NAME" HeaderText="Region" meta:resourceKey="BoundFieldResource4">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CUST_CONTACT_PERSON" SortExpression="CUST_CONTACT_PERSON" HeaderText="Contact Person" meta:resourceKey="BoundFieldResource5">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CUST_ADDRESS" SortExpression="CUST_ADDRESS" HeaderText="Customer Address" meta:resourceKey="BoundFieldResource6">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CUST_EMAIL" SortExpression="CUST_EMAIL" HeaderText="Email" meta:resourceKey="BoundFieldResource7">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField ReadOnly="True" DataField="CUST_ID" SortExpression="CUST_ID" HeaderText="CUST_ID" meta:resourceKey="BoundFieldResource8"></asp:BoundField>
<asp:BoundField DataField="CUST_STATUS" SortExpression="CUST_STATUS" HeaderText="Status" meta:resourceKey="BoundFieldResource9">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
</Columns>
                    <EmptyDataTemplate>
                        No Data Exist!
                    
</EmptyDataTemplate>
                    <SelectedRowStyle BackColor="LightSteelBlue" />
                </asp:GridView>
                <asp:SqlDataSource ID="sdsCustMaster" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_MASTER_CUSTOMER_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="CPID" ControlID="lblCPID"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="USERTYPE" ControlID="lblUserType"></asp:ControlParameter>
</SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td style="text-align: center; width: 944px;" colspan="4">
                <table id="Table1">
                    <tr>
                        <td>
                            <asp:Button ID="btnNew" runat="server" OnClick="btnNew_Click" Text="New" CausesValidation="False" meta:resourcekey="btnNewResource1" /></td>
                        <td>
                            <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Edit" CausesValidation="False" meta:resourcekey="btnEditResource1" /></td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete"
                                CausesValidation="False" meta:resourcekey="btnDeleteResource1" /></td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; width: 944px;">
                <table border="0" cellpadding="0" cellspacing="0" id="tblCustomerDetails" runat="server"
                    visible="false" width="100%">
                    <tr>
                        <td style="text-align: left;" class="profilehead" colspan="4">
                            Customer Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                        </td>
                        <td style="text-align: left;">
                        </td>
                        <td style="text-align: right;">
                        </td>
                        <td style="text-align: left;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:RadioButtonList id="rbNewExisting" runat="server" CellPadding="5" RepeatDirection="Horizontal" Visible="False" meta:resourcekey="rbNewExistingResource1">
                                <asp:ListItem Selected="True" meta:resourcekey="ListItemResource8" Text="New"></asp:ListItem>
                                <asp:ListItem meta:resourcekey="ListItemResource9" Text="Existing"></asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td style="text-align: right;">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblCustomerCode" runat="server" Text="Customer Code" Width="99px" meta:resourcekey="lblCustomerCodeResource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCustomerCode" runat="server" ReadOnly="True" meta:resourcekey="txtCustomerCodeResource1"></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblRegion" runat="server" Text="Region" meta:resourcekey="lblRegionResource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlRegion" runat="server" meta:resourcekey="ddlRegionResource1" >
                            </asp:DropDownList>
                            <asp:Label ID="Label13" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*" meta:resourcekey="Label8Resource1"></asp:Label>
                            <asp:RequiredFieldValidator id="RequiredFieldValidator12" runat="server" ControlToValidate="ddlRegion"
                                ErrorMessage="Please Select Region " InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name" Width="106px" meta:resourcekey="lblCustomerNameResource1"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox ID="txtCustomerName" runat="server" meta:resourcekey="txtCustomerNameResource1"></asp:TextBox><asp:Label ID="Label8" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*" meta:resourcekey="Label8Resource1"></asp:Label><asp:RequiredFieldValidator ID="rfvCustName" runat="server"
                                    ControlToValidate="txtCustomerName" ErrorMessage="Please Enter the Customer Name" meta:resourcekey="rfvCustNameResource1" Text="*"></asp:RequiredFieldValidator>
                            <cc1:filteredtextboxextender id="ftxteCustomerName" runat="server"
                                targetcontrolid="txtCustomerName" validchars="abcdefghijklmnopqrstuvwxyz. ABCDEFGHIHKLMNOPQRSTUVWXYZ,&" Enabled="True">
                            </cc1:filteredtextboxextender>
                        </td>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label ID="Label1" runat="server" Text="Company Name" Width="122px" meta:resourcekey="Label1Resource1"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox ID="txtCompanyName" runat="server" meta:resourcekey="txtCompanyNameResource1" ></asp:TextBox>&nbsp;
                            <cc1:filteredtextboxextender id="ftxteCompanyName" runat="server"
                                targetcontrolid="txtCompanyName" validchars="abcdefghijklmnopqrstuvwxyz. ABCDEFGHIHKLMNOPQRSTUVWXYZ,()" Enabled="True">
                           </cc1:filteredtextboxextender> 
                            
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblIndustryType" runat="server" Text="Industry Type" meta:resourcekey="lblIndustryTypeResource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlIndustryType" runat="server" meta:resourcekey="ddlIndustryTypeResource1">
                            </asp:DropDownList><asp:Label ID="Label22" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*" meta:resourcekey="Label22Resource1"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                    ControlToValidate="ddlIndustryType" ErrorMessage="Please Select the Industry Type" InitialValue="0" meta:resourcekey="RequiredFieldValidator9Resource1" Text="*"></asp:RequiredFieldValidator></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblWebsite" runat="server" Text="Website" meta:resourcekey="lblWebsiteResource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtWebsite" runat="server" meta:resourcekey="txtWebsiteResource1"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                ControlToValidate="txtWebsite" ErrorMessage="Please Enter Valid Website Format"
                                ValidationExpression="([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?" meta:resourcekey="RegularExpressionValidator4Resource1" Text="*"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: left" class="profilehead" colspan="4">
                            Corporate Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: right;">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person" Width="96px" meta:resourcekey="lblContactPersonResource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtContactPerson" runat="server" meta:resourcekey="txtContactPersonResource1"></asp:TextBox><asp:Label ID="Label21" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*" meta:resourcekey="Label21Resource1"></asp:Label><asp:RequiredFieldValidator ID="rfvContactName" runat="server"
                                    ControlToValidate="txtContactPerson" ErrorMessage="Please Enter the Contact Person" meta:resourcekey="rfvContactNameResource1" Text="*"></asp:RequiredFieldValidator>
                            <cc1:filteredtextboxextender id="ftxteContactPerson" runat="server"
                                targetcontrolid="txtContactPerson" validchars="qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM. " Enabled="True">
                            
                            </cc1:filteredtextboxextender>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblCustDesignationNo" runat="server" Text="Designation " meta:resourcekey="lblCustDesignationNoResource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlDesignationNo" runat="server" meta:resourcekey="ddlDesignationNoResource1">
                            </asp:DropDownList><asp:Label ID="Label10" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*" meta:resourcekey="Label10Resource1"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                    ControlToValidate="ddlDesignationNo" ErrorMessage="Please Select the Designation" InitialValue="0" meta:resourcekey="RequiredFieldValidator5Resource1" Text="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblCustomerAddress" runat="server" Text=" Address" meta:resourcekey="lblCustomerAddressResource1"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Width="540px" EnableTheming="False"
                                Font-Names="Verdana" Font-Size="8pt" meta:resourcekey="txtAddressResource1"></asp:TextBox>
                            <asp:Label ID="Label29" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*" meta:resourcekey="Label8Resource1"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"
                                    ControlToValidate="txtAddress" ErrorMessage="Please Enter the Address" meta:resourcekey="rfvContactNameResource1" Text="*">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblContactNo1" runat="server" Text="Phone No" meta:resourcekey="lblContactNo1Resource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtContactNo1" runat="server" meta:resourcekey="txtContactNo1Resource1"></asp:TextBox><asp:Label ID="Label11" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*" meta:resourcekey="Label11Resource1"></asp:Label><asp:RequiredFieldValidator ID="rfvContactNo1" runat="server" ControlToValidate="txtContactNo1"
                                ErrorMessage="Please Enter the Phone No" meta:resourcekey="rfvContactNo1Resource1" Text="*"></asp:RequiredFieldValidator><cc1:filteredtextboxextender id="ftxtePhoneNo" runat="server"
                                targetcontrolid="txtContactNo1" validchars="-0123456789()," Enabled="True">
                            </cc1:filteredtextboxextender> 
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblContactNo2" runat="server" Text="Mobile No" Width="86px" meta:resourcekey="lblContactNo2Resource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtContactNo2" runat="server" meta:resourcekey="txtContactNo2Resource1"></asp:TextBox>
                            <asp:Label ID="Label30" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*" meta:resourcekey="Label8Resource1"></asp:Label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtContactNo2"
                                ErrorMessage="Please Enter Valid Mobile No." ValidationExpression="^(00|09|9)(\d{9,})" meta:resourcekey="RegularExpressionValidator5Resource1" Text="*"></asp:RegularExpressionValidator>&nbsp;
                            <cc1:filteredtextboxextender id="ftxteMobileNo" runat="server"
                                targetcontrolid="txtContactNo2" validchars="-0123456789" Enabled="True">
                            </cc1:filteredtextboxextender> 
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblFAXNo" runat="server" Text="FAX No" meta:resourcekey="lblFAXNoResource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtFAXNo" runat="server" meta:resourcekey="txtFAXNoResource1"></asp:TextBox>&nbsp;<cc1:filteredtextboxextender id="ftxtFAXNo" runat="server"
                                targetcontrolid="txtFAXNo" validchars="-0123456789" Enabled="True"> </cc1:filteredtextboxextender></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblEmail" runat="server" Text="Email" meta:resourcekey="lblEmailResource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtEmail" runat="server" meta:resourcekey="txtEmailResource1"></asp:TextBox><asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="Please Enter  Valid Email   Format  in Email of  Corporate Details Block"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" meta:resourcekey="RegularExpressionValidator1Resource1" Text="*"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: right;">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center; height: 9px;">
                            <asp:Button ID="btnOtherCorpDetails" runat="server" OnClick="btnOtherCorpDetails_Click"
                                Text="Add Contacts" CausesValidation="False" Height="25px" Width="122px" meta:resourcekey="btnOtherCorpDetailsResource1" /></td>
                    </tr>
                    <tr>
                                    <td style="text-align: left; height: 23px;" class="profilehead" colspan="4" id="tdOtherCorpDetailsHead" runat="server">
                                        Other Corporate Details</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <table border="0" cellpadding="0" cellspacing="0" id="tblOtherCorpDetails" runat="server"
                                visible="false" width="100%">
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
                                    <td style="text-align: right;">
                                        <asp:Label ID="Label12" runat="server" Text="Contact Person" Width="96px" meta:resourcekey="Label12Resource1"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtOtherCorpContactName" runat="server" meta:resourcekey="txtOtherCorpContactNameResource1"></asp:TextBox><asp:Label
                                            ID="Label15" runat="server" EnableTheming="False" ForeColor="Red" Text="*" meta:resourcekey="Label15Resource1"></asp:Label><asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtOtherCorpContactName"
                                                ErrorMessage="Please Enter the Contact Person" ValidationGroup="othercorp" meta:resourcekey="RequiredFieldValidator6Resource1" Text="*"></asp:RequiredFieldValidator></td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="Label17" runat="server" Text="Designation " meta:resourcekey="Label17Resource1"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlOtherCorpDesignation" runat="server" meta:resourcekey="ddlOtherCorpDesignationResource1">
                                        </asp:DropDownList><asp:Label ID="Label20" runat="server" EnableTheming="False" ForeColor="Red"
                                            Text="*" meta:resourcekey="Label20Resource1"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                ControlToValidate="ddlOtherCorpDesignation" ErrorMessage="Please Select the Designation"
                                                ValidationGroup="othercorp" InitialValue="0" meta:resourcekey="RequiredFieldValidator7Resource1" Text="*"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label23" runat="server" Text="Phone No" meta:resourcekey="Label23Resource1"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtOtherCorpPhoneNo" runat="server" meta:resourcekey="txtOtherCorpPhoneNoResource1"></asp:TextBox><asp:Label ID="Label24"
                                            runat="server" EnableTheming="False" ForeColor="Red" Text="*" meta:resourcekey="Label24Resource1"></asp:Label><asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtOtherCorpPhoneNo"
                                                ErrorMessage="Please Enter the Phone No" ValidationGroup="othercorp" meta:resourcekey="RequiredFieldValidator8Resource1" Text="*"></asp:RequiredFieldValidator>
                                        <cc1:filteredtextboxextender id="ftxteOtherCorpPhoneNo" runat="server"
                                targetcontrolid="txtOtherCorpPhoneNo" validchars="-0123456789()," Enabled="True">
                                        </cc1:filteredtextboxextender> 
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label25" runat="server" Text="Mobile No" Width="86px" meta:resourcekey="Label25Resource1"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtOtherCorpMobileNo" runat="server" meta:resourcekey="txtOtherCorpMobileNoResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtOtherCorpPhoneNo"
                                                ErrorMessage="Please Enter the Mobile No" ValidationGroup="othercorp" meta:resourcekey="RequiredFieldValidator8Resource1" Text="*">
                                        </asp:RequiredFieldValidator>
                                        <cc1:filteredtextboxextender id="ftxteOtherCorpMobileNo" runat="server"
                                targetcontrolid="txtOtherCorpMobileNo" validchars="-0123456789" Enabled="True">
                                        </cc1:filteredtextboxextender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label26" runat="server" Text="FAX No" meta:resourcekey="Label26Resource1"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtOtherCorpFaxNo" runat="server" meta:resourcekey="txtOtherCorpFaxNoResource1"></asp:TextBox>
                                        <cc1:filteredtextboxextender id="ftxteOtherCorpFaxNo" runat="server"
                                targetcontrolid="txtOtherCorpFaxNo" validchars="-0123456789" Enabled="True">
                                        </cc1:filteredtextboxextender> 
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label27" runat="server" Text="Email" meta:resourcekey="Label27Resource1"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtOtherCorpEmail" runat="server" meta:resourcekey="txtOtherCorpEmailResource1"></asp:TextBox>
                                        <asp:RegularExpressionValidator id="RegularExpressionValidator2" runat="server" ControlToValidate="txtOtherCorpEmail"
                                            ErrorMessage="Please Enter  Valid Email   Format  in Email of Other Corporate Details Block"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="othercorp" meta:resourcekey="RegularExpressionValidator2Resource1" Text="*"></asp:RegularExpressionValidator></td>
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
                                    <td colspan="4" style="text-align: center">
                                        <asp:Button ID="btnOtherCorpDetailsAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                            CssClass="loginbutton" EnableTheming="False" OnClick="btnOtherCorpDetailsAdd_Click"
                                            Text="Add" ValidationGroup="othercorp" meta:resourcekey="btnOtherCorpDetailsAddResource1" /><asp:Button ID="btnOtherCorpDetailsRefresh"
                                                runat="server" BackColor="Transparent" BorderStyle="None" CausesValidation="False"
                                                CssClass="loginbutton" EnableTheming="False" OnClick="btnOtherCorpDetailsRefresh_Click"
                                                Text="Refresh" meta:resourcekey="btnOtherCorpDetailsRefreshResource1" /></td>
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
                            </table>
                                        <asp:GridView ID="gvOtherCorpDetails" runat="server" AutoGenerateColumns="False"
                                            OnRowDataBound="gvOtherCorpDetails_RowDataBound" OnRowDeleting="gvOtherCorpDetails_RowDeleting"
                                            Width="100%" OnRowEditing="gvOtherCorpDetails_RowEditing" meta:resourcekey="gvOtherCorpDetailsResource1">
                                            <Columns>
<asp:CommandField ShowEditButton="True" meta:resourcekey="CommandFieldResource1"></asp:CommandField>
<asp:CommandField ShowDeleteButton="True" meta:resourcekey="CommandFieldResource2"></asp:CommandField>
<asp:BoundField DataField="ContactPerson" HeaderText="Contact Person" meta:resourcekey="BoundFieldResource10">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Designation" HeaderText="Designation" meta:resourcekey="BoundFieldResource11"></asp:BoundField>
<asp:BoundField DataField="ContactNo1" HeaderText="Phone No" meta:resourcekey="BoundFieldResource12"></asp:BoundField>
<asp:BoundField DataField="ContactNo2" NullDisplayText="-" HeaderText="Mobile No" meta:resourcekey="BoundFieldResource13"></asp:BoundField>
<asp:BoundField DataField="FaxNo" NullDisplayText="-" HeaderText="FaxNo" meta:resourcekey="BoundFieldResource14"></asp:BoundField>
<asp:BoundField DataField="Email" NullDisplayText="-" HeaderText="Email" meta:resourcekey="BoundFieldResource15"></asp:BoundField>
<asp:BoundField DataField="DesignationId" HeaderText="DesignationIdHidden" meta:resourcekey="BoundFieldResource16"></asp:BoundField>
<asp:BoundField DataField="custdetid" HeaderText="CustDetIdHidden" meta:resourcekey="BoundFieldResource17"></asp:BoundField>
<asp:BoundField DataField="custunitid" HeaderText="CustDelOrNotHidden" meta:resourcekey="BoundFieldResource18"></asp:BoundField>
</Columns>
                                        </asp:GridView>
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
                            <asp:Button ID="btnAddUnits" runat="server" CausesValidation="False" OnClick="btnAddUnits_Click"
                                Text="Add Units" meta:resourcekey="btnAddUnitsResource1" /></td>
                    </tr>
                    <tr>
                        <td style="text-align: left" class="profilehead" colspan="4" id="tdUnitDetailsHead"
                            runat="server">
                            Unit Details</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <table border="0" cellpadding="0" cellspacing="0" id="tblUnitDetails" runat="server"
                                visible="false" width="100%">
                                <tr>
                                    <td style="text-align: left; height: 19px;" colspan="2" rowspan="1">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label2" runat="server" Text="Unit Name" Width="128px" meta:resourcekey="Label2Resource1"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtUnitName" runat="server" EnableTheming="True" meta:resourcekey="txtUnitNameResource1"></asp:TextBox><asp:Label ID="Label4" runat="server" EnableTheming="False" ForeColor="Red" Text="*" meta:resourcekey="Label4Resource1"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUnitName"
                                            ErrorMessage="Please Enter the Unit Name" ValidationGroup="unit" meta:resourcekey="RequiredFieldValidator1Resource1" Text="*"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label7" runat="server" Text=" Address" meta:resourcekey="Label7Resource1"></asp:Label></td>
                                    <td colspan="1" style="text-align: left">
                                        &nbsp;<asp:TextBox ID="txtUnitAddress" runat="server" TextMode="MultiLine" Width="432px"
                                            EnableTheming="False" meta:resourcekey="txtUnitAddressResource1"></asp:TextBox><asp:Label ID="Label5" runat="server" EnableTheming="False" ForeColor="Red" Text="*" meta:resourcekey="Label5Resource1"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUnitAddress"
                                            ErrorMessage="Please Enter the Unit Address" ValidationGroup="unit" meta:resourcekey="RequiredFieldValidator2Resource1" Text="*"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td style="height: 19px; text-align: right">
                                    </td>
                                    <td style="height: 19px; text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center">
                                        <asp:Button ID="btnUnitsAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                            CssClass="loginbutton" EnableTheming="False" OnClick="btnUnitsAdd_Click" Text="Add"
                                            ValidationGroup="unit" meta:resourcekey="btnUnitsAddResource1" />
                                        <asp:Button ID="btnUnitsRefresh" runat="server" BackColor="Transparent" BorderStyle="None"
                                            CausesValidation="False" CssClass="loginbutton" EnableTheming="False" OnClick="btnUnitsRefresh_Click"
                                            Text="Refresh" meta:resourcekey="btnUnitsRefreshResource1" /></td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center">
                                    </td>
                                </tr>
                            </table>
                            <asp:GridView ID="gvUnitDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvUnitDetails_RowDataBound"
                                OnRowDeleting="gvUnitDetails_RowDeleting" Width="100%" OnRowEditing="gvUnitDetails_RowEditing" meta:resourcekey="gvUnitDetailsResource1">
                                <Columns>
<asp:CommandField ShowEditButton="True" meta:resourceKey="CommandFieldResource3"></asp:CommandField>
<asp:CommandField ShowDeleteButton="True" meta:resourceKey="CommandFieldResource4"></asp:CommandField>
<asp:TemplateField HeaderText="Unit Name " meta:resourceKey="TemplateFieldResource2">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton runat="server" ID="lbtnUnitName" Text='<%# Bind("UnitName") %>' CausesValidation="False" meta:resourceKey="lbtnUnitNameResource1" OnClick="lbtnUnitName_Click"></asp:LinkButton>


                                        
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="UnitAddress" HeaderText="Unit Address" meta:resourceKey="BoundFieldResource19">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="UnitName" HeaderText="UnitNameHidden" meta:resourceKey="BoundFieldResource20"></asp:BoundField>
<asp:BoundField DataField="custunitid" HeaderText="CustUnitIdHidden" meta:resourceKey="BoundFieldResource21"></asp:BoundField>
</Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left" class="profilehead" colspan="4" id="tdContactDetailsHead"
                            runat="server">
                            Contact Details</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <table border="0" cellpadding="0" cellspacing="0" id="tblContactDetails" runat="server"
                                visible="false" width="100%">
                                <tr>
                                    <td style="text-align: left; height: 19px;" colspan="4" rowspan="1">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label3" runat="server" Text="Unit Name" meta:resourcekey="Label3Resource1"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlUnitName" runat="server" meta:resourcekey="ddlUnitNameResource1">
                                        </asp:DropDownList><asp:Label ID="Label6" runat="server" EnableTheming="False" ForeColor="Red"
                                            Text="*" meta:resourcekey="Label6Resource1"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlUnitName"
                                            ErrorMessage="Please Select the Unit Name" InitialValue="0" ValidationGroup="cc" meta:resourcekey="RequiredFieldValidator3Resource1" Text="*"></asp:RequiredFieldValidator></td>
                                    <td style="text-align: right">
                                    </td>
                                    <td style="text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblContactPer" runat="server" Text="Contact Person" Width="96px" meta:resourcekey="lblContactPerResource1"></asp:Label></td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtCorpoContactPerson" runat="server" meta:resourcekey="txtCorpoContactPersonResource1"></asp:TextBox><asp:Label ID="Label16" runat="server" EnableTheming="False" ForeColor="Red"
                                            Text="*" meta:resourcekey="Label16Resource1"></asp:Label><asp:RequiredFieldValidator ID="rfvContactPerson" runat="server" ControlToValidate="txtCorpoContactPerson"
                                            ErrorMessage="Please Enter the Contact Person" ValidationGroup="cc" meta:resourcekey="rfvContactPersonResource1" Text="*"></asp:RequiredFieldValidator></td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblCorpDesignationNo" runat="server" Text="Designation " meta:resourcekey="lblCorpDesignationNoResource1"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlCorpoDesignationNo" runat="server" meta:resourcekey="ddlCorpoDesignationNoResource1">
                                        </asp:DropDownList><asp:Label ID="Label18" runat="server" EnableTheming="False" ForeColor="Red"
                                            Text="*" meta:resourcekey="Label18Resource1"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCorpoDesignationNo"
                                            ErrorMessage="Please Select the Designation" InitialValue="0" ValidationGroup="cc" meta:resourcekey="RequiredFieldValidator4Resource1" Text="*"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblCorpContactNo1" runat="server" Text="Phone No" meta:resourcekey="lblCorpContactNo1Resource1"></asp:Label></td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtCorpoContactNo1" runat="server" meta:resourcekey="txtCorpoContactNo1Resource1"></asp:TextBox><asp:Label ID="Label19" runat="server" EnableTheming="False" ForeColor="Red"
                                            Text="*" meta:resourcekey="Label19Resource1"></asp:Label><asp:RequiredFieldValidator ID="rfvContact" runat="server" ControlToValidate="txtCorpoContactNo1"
                                            ErrorMessage="Please Enter the Phone No" ValidationGroup="cc" meta:resourcekey="rfvContactResource1" Text="*"></asp:RequiredFieldValidator><cc1:filteredtextboxextender id="ftxteCorpoContactNo1" runat="server"
                                targetcontrolid="txtCorpoContactNo1" validchars="-0123456789()," Enabled="True">
                                        </cc1:filteredtextboxextender> 
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblCorpContactNo2" runat="server" Text="Mobile No" Width="120px" meta:resourcekey="lblCorpContactNo2Resource1"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtCorpoContactNo2" runat="server" meta:resourcekey="txtCorpoContactNo2Resource1"></asp:TextBox>
                                        <cc1:filteredtextboxextender id="ftxteCorpoContactNo2" runat="server"
                                targetcontrolid="txtCorpoContactNo2" validchars="-0123456789" Enabled="True">
                                        </cc1:filteredtextboxextender> 
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lbCorplFAXNo" runat="server" Text="FAX No" meta:resourcekey="lbCorplFAXNoResource1"></asp:Label></td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtCorpoFaxNo" runat="server" meta:resourcekey="txtCorpoFaxNoResource1"></asp:TextBox>
                                        <cc1:filteredtextboxextender id="ftxteCorpoFaxNo" runat="server"
                                targetcontrolid="txtCorpoFaxNo" validchars="-0123456789" Enabled="True">
                                        </cc1:filteredtextboxextender> 
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblCorpEmail" runat="server" Text="Email" meta:resourcekey="lblCorpEmailResource1"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtCorpoEmail" runat="server" meta:resourcekey="txtCorpoEmailResource1"></asp:TextBox><asp:RegularExpressionValidator id="RegularExpressionValidator3" runat="server" ControlToValidate="txtCorpoEmail"
                                            ErrorMessage="Please Enter  Valid Email   Format  in Email of Contact Details Block"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="cc" meta:resourcekey="RegularExpressionValidator3Resource1" Text="*"></asp:RegularExpressionValidator></td>
                                </tr>
                                <tr>
                                    <td style="height: 16px; text-align: right">
                                    </td>
                                    <td style="height: 16px; text-align: left">
                                    </td>
                                    <td style="height: 16px; text-align: right">
                                    </td>
                                    <td style="height: 16px; text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; height: 19px;">
                                    </td>
                                    <td style="text-align: right; height: 19px;">
                                        <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                            CssClass="loginbutton" EnableTheming="False" OnClick="btnAdd_Click" Text="Add"
                                            ValidationGroup="cc" meta:resourcekey="btnAddResource1" /></td>
                                    <td style="text-align: left; height: 19px;">
                                        <asp:Button ID="btnRefreshItems" runat="server" BackColor="Transparent" BorderStyle="None"
                                            CausesValidation="False" CssClass="loginbutton" EnableTheming="False" OnClick="btnItemRefresh_Click"
                                            Text="Refresh" meta:resourcekey="btnRefreshItemsResource1" /></td>
                                    <td style="text-align: left; height: 19px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 16px; text-align: right">
                                    </td>
                                    <td style="height: 16px; text-align: right">
                                    </td>
                                    <td style="height: 16px; text-align: left">
                                    </td>
                                    <td style="height: 16px; text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: center">
                                    </td>
                                </tr>
                            </table>
                            <asp:GridView ID="gvCustomerItems" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvCustomerItems_RowDataBound"
                                OnRowDeleting="gvCustomerItems_RowDeleting" Width="100%" OnRowEditing="gvCustomerItems_RowEditing" meta:resourcekey="gvCustomerItemsResource1">
                                <Columns>
<asp:CommandField ShowEditButton="True" meta:resourcekey="CommandFieldResource5"></asp:CommandField>
<asp:CommandField ShowDeleteButton="True" meta:resourcekey="CommandFieldResource6"></asp:CommandField>
<asp:BoundField DataField="UnitName" HeaderText="Unit Name" meta:resourcekey="BoundFieldResource22"></asp:BoundField>
<asp:BoundField DataField="ContactPerson" HeaderText="Contact Person" meta:resourcekey="BoundFieldResource23">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Designation" HeaderText="Designation" meta:resourcekey="BoundFieldResource24"></asp:BoundField>
<asp:BoundField DataField="ContactNo1" HeaderText="Phone No" meta:resourcekey="BoundFieldResource25"></asp:BoundField>
<asp:BoundField DataField="ContactNo2" NullDisplayText="-" HeaderText="Mobile No" meta:resourcekey="BoundFieldResource26"></asp:BoundField>
<asp:BoundField DataField="FaxNo" NullDisplayText="-" HeaderText="FaxNo" meta:resourcekey="BoundFieldResource27"></asp:BoundField>
<asp:BoundField DataField="Email" NullDisplayText="-" HeaderText="Email" meta:resourcekey="BoundFieldResource28"></asp:BoundField>
<asp:BoundField DataField="DesignationId" HeaderText="DesignationIdHidden" meta:resourcekey="BoundFieldResource29"></asp:BoundField>
<asp:BoundField DataField="CustUnitId" HeaderText="CustUnitIdHidden" meta:resourcekey="BoundFieldResource30"></asp:BoundField>
<asp:BoundField DataField="custdetid" HeaderText="CustDetIdHidden" meta:resourcekey="BoundFieldResource31"></asp:BoundField>
</Columns>
                            </asp:GridView>
                            <asp:Label ID="lblContectDetailsGridLabel" runat="server" EnableTheming="False" Font-Names="Verdana"
                                Font-Size="8pt" ForeColor="Red" meta:resourcekey="lblContectDetailsGridLabelResource1"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Tax &nbsp;Details</td>
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
                        <td style="text-align: right">
                            <asp:Label ID="lblPANNo" runat="server" Text="PAN No" meta:resourcekey="lblPANNoResource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPANNo" runat="server" meta:resourcekey="txtPANNoResource1"></asp:TextBox>
                            <cc1:filteredtextboxextender id="ftxtePANNo" runat="server"
                                targetcontrolid="txtPANNo" FilterMode="InvalidChars" InvalidChars="!@#$%^&*()_+|=\./-{}[]:&quot;;'<>?,./" Enabled="True">
                            </cc1:filteredtextboxextender>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblECCNo" runat="server" Text="ECC No" meta:resourcekey="lblECCNoResource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtECCNo" runat="server" meta:resourcekey="txtECCNoResource1"></asp:TextBox>
                            <cc1:filteredtextboxextender id="ftxteECCNo" runat="server"
                                targetcontrolid="txtECCNo" FilterMode="InvalidChars" InvalidChars="!@#$%^&*()_+|=\./-{}[]:&quot;;'<>?,./" Enabled="True">
                            </cc1:filteredtextboxextender> 
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblCSTNo" runat="server" Text="CST No" meta:resourcekey="lblCSTNoResource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCSTNo" runat="server" meta:resourcekey="txtCSTNoResource1"></asp:TextBox>
                            <cc1:filteredtextboxextender id="ftxteCSTNo" runat="server"
                                targetcontrolid="txtCSTNo" FilterMode="InvalidChars" InvalidChars="!@#$%^&*()_+|=.{}[]&quot;'<>?,." Enabled="True">
                            </cc1:filteredtextboxextender> 
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblLocalSalesTaxNo" runat="server" Text="TIN No" Width="130px" meta:resourcekey="lblLocalSalesTaxNoResource1"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtLocalSalesTaxNo" runat="server" meta:resourcekey="txtLocalSalesTaxNoResource1"></asp:TextBox>
                            <cc1:filteredtextboxextender id="ftxteLocalSalesTaxNo" runat="server"
                                targetcontrolid="txtLocalSalesTaxNo" FilterMode="InvalidChars" InvalidChars="!@#$%^&*()_+|=\./-{}[]:&quot;;'<>?,./" Enabled="True">
                            </cc1:filteredtextboxextender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblSpecialInstructions" runat="server" Text="Special Instructions" meta:resourcekey="lblSpecialInstructionsResource1"></asp:Label></td>
                        <td colspan="3" rowspan="1" style="text-align: left">
                            <asp:TextBox ID="txtSpecialInstructions" runat="server" TextMode="MultiLine" Height="39px"
                                EnableTheming="False" Width="543px" Font-Names="Verdana" Font-Size="8pt" meta:resourcekey="txtSpecialInstructionsResource1"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;">
                        </td>
                        <td style="height: 19px">
                        </td>
                        <td style="height: 19px;">
                        </td>
                        <td style="height: 19px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 49px">
                            <table id="tblButtons">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" meta:resourcekey="btnSaveResource1" /></td>
                                    <td>
                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click"
                                            CausesValidation="False" meta:resourcekey="btnRefreshResource1" /></td>
                                    <td>
                                        </td>
                                    <td style="width: 52px">
                                        <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" CausesValidation="False" meta:resourcekey="btnCloseResource1" /></td>
                                    <td style="width: 52px">
                                        <asp:Button ID="btnSalesLead" runat="server" Text="Sales Lead"
                                            CausesValidation="False" OnClick="btnSalesLead_Click" meta:resourcekey="btnSalesLeadResource1" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                &nbsp;
                               
                <asp:Panel id="Panel1" runat="server" meta:resourcekey="Panel1Resource1">
                    <table>
                        <tr>
                            <td style="width: 100px">
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="0" cellspacing="0" style="font-size: 8pt; background-image: url(Images/ConfirmBox2.PNG);
                       background-repeat:repeat;  font-family: Verdana" id="tblPopUp1" runat = "server" visible = "false">
                        <tr>
                            <td background="../../Images/ConfirmBox1Sa.PNG" style="height: 300px; text-align: left"
                                width="55">
                            </td>
                            <td align="center" background="../../Images/ConfirmBox2sa.PNG" style="height: 300px"
                                valign="top">
                                 <table>
                                    <tr>
                                        <td colspan="3" rowspan="1" style="text-align: left; height: 40px; width: 400px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="text-align: left; width: 400px;">
                                            <asp:Label id="lblMessage" runat="server" meta:resourcekey="lblMessageResource1" Text="Customer Already Exists With Following Data."></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="text-align: left; width: 400px;">
                                            <asp:Label ID="lblData" runat="server" meta:resourcekey="lblDataResource1"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" rowspan="1" style="text-align: left; width: 400px;">
                                            <asp:Label id="lblData1" runat="server" meta:resourcekey="lblData1Resource1"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" rowspan="1" style="text-align: left; width: 400px;">
                                            <asp:Label id="lblData2" runat="server" meta:resourcekey="lblData2Resource1"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" rowspan="1" style="text-align: left; width: 400px;">
                                            <asp:Label id="lblData3" runat="server" meta:resourcekey="lblData3Resource1"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" rowspan="1" style="text-align: left; width: 400px;">
                                            <asp:Label id="lblData5" runat="server" meta:resourcekey="lblData5Resource1"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" rowspan="1" style="text-align: left; width: 400px;">
                                            <asp:Label id="lblData4" runat="server" meta:resourcekey="lblData4Resource1"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" rowspan="1" style="text-align: left; width: 400px;">
                                            <asp:Label id="lblDo" runat="server" meta:resourcekey="lblDoResource1" Text="What Should I Do?"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="text-align: center; width: 400px;">
                                            <asp:Button id="btnConfirmYes" runat="server" CausesValidation="False" EnableTheming="False"
                                                Font-Names="Verdana" Font-Size="8pt" Height="23px" 
                                                Text="New" Width="80px" OnClick="btnConfirmYes_Click" meta:resourcekey="btnConfirmYesResource1" />
                                            &nbsp;
                                            <asp:Button id="btnConfirmNo" runat="server" CausesValidation="False" EnableTheming="False"
                                                Font-Names="Verdana" Font-Size="8pt" Height="23px" Text="History" Width="80px" meta:resourcekey="btnConfirmNoResource1" OnClick="btnConfirmNo_Click" /></td>
                                    </tr>
                                 </table>
                                &nbsp; &nbsp; &nbsp;
                            </td>
                            <td background="../../Images/ConfirmBox3sa.PNG" style="height: 300px; width: 27px;;background-image: 'url[../../Images/ConfirmBox3sa.PNG]'">
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel id="Panel2" runat="server" meta:resourcekey="Panel2Resource2">
                    <table border="0"   visible = "false" runat ="server" cellpadding="0" cellspacing="0" style="font-size: 8pt; background-image: url(Images/ConfirmBox2.PNG);
                       background-repeat:repeat;  font-family: Verdana" id="tblpopup3">
                        <tr>
                            <td background="../../Images/ConfirmBox1Sa.PNG" style="height: 213px; text-align: left"
                                width="55">
                            </td>
                            <td align="center" background="../../Images/ConfirmBox2sa.PNG" style="height: 213px"
                                valign="top">
                                <table id="tblPopup2" runat="server" >
                                    <tr>
                                        <td colspan="3" rowspan="1" style="text-align: left; height: 40px; width: 158px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="profilehead" colspan="3" rowspan="1" style="text-align: left">
                                            <asp:Label id="Label9" runat="server" Text="CUSTOMER DETAILS:"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="text-align: left;"><asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    Width="100%" meta:resourcekey="gvCustMasterDetailsResource1" OnRowDataBound="GridView1_RowDataBound"  Visible="False">
                                            <columns>
<asp:BoundField DataField="CUST_CODE" HeaderText="Customer Code" meta:resourceKey="BoundFieldResource2">
<ControlStyle Width="100px"></ControlStyle>

<ItemStyle Width="110px" HorizontalAlign="Right"></ItemStyle>

<HeaderStyle Width="100px" HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
                                                <asp:TemplateField HeaderText="Customer Name">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CUST_NAME") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnCustomer" runat="server" OnClick="lbtnCustomer_Click" Text='<%# Bind("CUST_NAME") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CUST_NAME" HeaderText="Customer Name" />
<asp:BoundField DataField="CUST_COMPANY_NAME" HeaderText="Company Name" meta:resourceKey="BoundFieldResource3">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CUST_CONTACT_PERSON" HeaderText="Contact Person" meta:resourceKey="BoundFieldResource5">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CUST_ADDRESS" HeaderText="Customer Address" meta:resourceKey="BoundFieldResource6">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CUST_EMAIL" HeaderText="Email" meta:resourceKey="BoundFieldResource7">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField ReadOnly="True" DataField="CUST_ID" SortExpression="CUST_ID" HeaderText="CUST_ID" meta:resourceKey="BoundFieldResource8"></asp:BoundField>
<asp:BoundField DataField="CUST_STATUS" HeaderText="Status" meta:resourceKey="BoundFieldResource9">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
</columns>
                                            <emptydatatemplate>
                        No Data Exist!
                    
</emptydatatemplate>
                                            <SelectedRowStyle BackColor="LightSteelBlue" />
                                        </asp:GridView></td>
                                    </tr>
                                    <tr>
                                        <td class="profilehead" colspan="3" style="text-align: left">SALES LEAD:</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" rowspan="1" style="text-align: left;">
                                            <asp:GridView id="gvSalesEnquiry" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                DataKeyNames="ENQ_ID" Width="100%" OnRowDataBound="gvSalesEnquiry_RowDataBound" Visible="False">
                                                <columns>
                                                    <asp:TemplateField HeaderText="Enq No">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ENQ_NO") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnSalesEnq" runat="server" OnClick="lbtnSalesEnq_Click" Text='<%# Bind("ENQ_NO") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ENQ_ID" HeaderText="Enq No" />
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="ENQ_DATE" SortExpression="ENQ_DATE" HeaderText="Enq Date">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CUST_NAME" SortExpression="CUST_NAME" HeaderText="Customer Name">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="ENQ_DUE_DATE" SortExpression="ENQ_DUE_DATE" HeaderText="Due Date">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ENQ_STATUS" SortExpression="ENQ_STATUS" HeaderText="Status">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
</columns>
                                                <emptydatatemplate>
                        No Data Exist!
                    
</emptydatatemplate>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="profilehead" colspan="3" rowspan="1" style="text-align: left; height: 17px;">
                                            QUOTATION MASTER:</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" rowspan="1" style="text-align: left;">
                                            <asp:GridView id="gvQuotationDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnRowDataBound="gvQuotationDetails_RowDataBound" Visible="False">
                                                <columns>
                                                    <asp:TemplateField HeaderText="Quotation No">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("QUOT_NO") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnQuotation" runat="server" OnClick="lbtnQuotation_Click" Text='<%# Bind("QUOT_NO") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="QUOT_ID" HeaderText="Quotation No" />
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True" DataField="QUOT_DATE" HeaderText="Quotation Date"></asp:BoundField>
<asp:BoundField DataField="CUST_NAME" HeaderText="Customer">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CUST_CONTACT_PERSON" HeaderText="Contact Person">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CUST_EMAIL" HeaderText="Email">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="PREPAREDBY" SortExpression="PREPAREDBY" HeaderText="Prepared By">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="APPROVEDBY" SortExpression="APPROVEDBY" HeaderText="Approved By">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="QUOT_PO_FLAG" HeaderText="Status">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
</columns>
                                                <emptydatatemplate>
                        No Data Exist!
                    
</emptydatatemplate>
                                                <selectedrowstyle backcolor="LightSteelBlue" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" rowspan="1" style="text-align: center; height: 24px;">
                                            <asp:Button id="Button1" runat="server" Text="Close" EnableTheming="False" OnClick="Button1_Click" />
                                            <asp:Button id="btnSalesLeadPopUp" runat="server" Text="Sales Lead" OnClick="btnSalesLeadPopUp_Click" EnableTheming="False" /></td>
                                    </tr>
                                </table>
                                &nbsp; &nbsp; &nbsp;
                            </td>
                            <td background="../../Images/ConfirmBox3sa.PNG" style="height: 213px; width: 27px;">
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:ModalPopupExtender ID="ModalPopupExtender" runat="server" PopupControlID="Panel1"
                    RepositionMode="None" TargetControlID="btnForApproveHidden" DynamicServicePath="" Enabled="False">
                </cc1:ModalPopupExtender>
                <asp:Button id="btnForApproveHidden" runat="server" CausesValidation="False" Text="for approve hidden" meta:resourcekey="btnForApproveHiddenResource1" />&nbsp;<cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel2"
                    RepositionMode="None" TargetControlID="Button3" DynamicServicePath="" Enabled="False">
                    </cc1:ModalPopupExtender>
                <asp:Button id="Button3" runat="server" CausesValidation="False" Text="for approve hidden" meta:resourcekey="btnForApproveHiddenResource1" />
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 57px; width: 944px;">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" meta:resourcekey="ValidationSummary1Resource1"></asp:ValidationSummary>
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="unit" meta:resourcekey="ValidationSummary2Resource1">
                </asp:ValidationSummary>
                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="cc" meta:resourcekey="ValidationSummary3Resource1"></asp:ValidationSummary>
                <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="othercorp" meta:resourcekey="ValidationSummary4Resource1">
                </asp:ValidationSummary>
            </td>
        </tr>
    </table>
</asp:Content>

 
