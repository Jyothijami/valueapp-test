<%@ Page Language="C#" MasterPageFile="~/MPs/PurchaseMP1.master" AutoEventWireup="true" 
    CodeFile="SuppliersEnquiry.aspx.cs" Inherits="Modules_PurchasingManagement_SuppliersEnquiry" Title="|| Value App : Purchasing Management : Suppliers Enquiry ||" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
        <script>
            $(function () {
                $("[name$='txtEnquiryDate'],[name$='txtEnquiryDueDate'],[name$='txtReqByDate'],[name$='txtPIDate'],[name$='txtPIDate']").datepicker();
            });
    </script>
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                Suppliers Enquiry</td>
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
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td id="Td20" style="height: 19px">
            </td>
            <td style="height: 19px">
            </td>
            <td style="height: 19px">
            </td>
            <td style="height: 19px">
            </td>
        </tr>
        <tr>
            <td id="TD9" class="searchhead" colspan="4">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">
                            Suppliers Enquiry</td>
                        <td>
                        </td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td style="height: 25px">
                                        <asp:Label id="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList id="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="SUP_ENQ_NO">SupEnquiry No.</asp:ListItem>
                                            <asp:ListItem Value="SUP_ENQ_DATE">Enquiry Date</asp:ListItem>
                                            <asp:ListItem Value="SUP_NAME">Supplier Name</asp:ListItem>
                                            <asp:ListItem Value="SUP_CONTACT_PERSON">Contact Person</asp:ListItem>
                                            <asp:ListItem Value="SUP_EMAIL">Email</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList id="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                            EnableTheming="False"
                                            Visible="False" Width="50px" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged">
                                            <asp:ListItem Selected="True">=</asp:ListItem>
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem>&gt;</asp:ListItem>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <asp:ListItem>&gt;=</asp:ListItem>
                                            <asp:ListItem>R</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="height: 25px">
                                        <asp:Label id="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:TextBox id="txtSearchValueFromDate" runat="server" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox>
                                        <asp:Image id="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender  Format="dd/MM/yyyy" ID="ceSearchFrom" runat="server" enabled="False" popupbuttonid="imgFromDate"
                                            targetcontrolid="txtSearchValueFromDate"></cc1:calendarextender>
                                        <cc1:maskededitextender id="MaskedEditSearchFromDate" runat="server" displaymoney="Left"
                                            enabled="False" mask="99/99/9999" masktype="Date" targetcontrolid="txtSearchValueFromDate"
                                            userdateformat="MonthDayYear"></cc1:maskededitextender>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Label id="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:TextBox id="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><asp:Image id="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchValueToDate" runat="server" enabled="False" popupbuttonid="imgToDate"
                                            targetcontrolid="txtSearchText"></cc1:calendarextender>
                                        <cc1:maskededitextender id="MaskedEditSearchToDate" runat="server" displaymoney="Left"
                                            enabled="False" mask="99/99/9999" masktype="Date" targetcontrolid="txtSearchText"
                                            userdateformat="MonthDayYear"></cc1:maskededitextender>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Button id="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" onclick="btnSearchGo_Click" Text="Go" /></td>
                                </tr>
                                </table>
                            <asp:Label id="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label><asp:Label id="lblEmpIdHidden" runat="server" Visible="False"></asp:Label><asp:Label id="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label>
                            <asp:Label id="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td id="TD34" runat="server" colspan="4" style="text-align: center;width:100%;" valign="top">
                <asp:GridView id="gvSupEnqDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="SUP_ENQ_ID"
                    DataSourceID="sdsSuppEnquiry" SelectedRowStyle-BackColor="#c0c0c0" OnRowDataBound="gvSupEnqDetails_RowDataBound" Width="100%" AllowPaging="True" AllowSorting="True">
                    <columns>
<asp:BoundField DataField="SUP_ENQ_ID" SortExpression="SUP_ENQ_ID" HeaderText="SuppEnqIdHidden"></asp:BoundField>
<asp:TemplateField SortExpression="SUP_ENQ_NO" HeaderText="SuppEnqNo"><EditItemTemplate>
<asp:TextBox runat="server" Text='<%# Bind("ENQ_NO") %>' id="TextBox1"></asp:TextBox>
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnSuppEnqNo" onclick="lbtnSuppEnqNo_Click" ForeColor="#0066ff" runat="server" Text='<%# Eval("SUP_ENQ_NO") %>' CausesValidation="False" __designer:wfdid="w2"></asp:LinkButton>&nbsp; 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:MM/dd/yyyy}" DataField="SUP_ENQ_DATE" SortExpression="SUP_ENQ_DATE" HeaderText="EnqDate">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="SUP_NAME" SortExpression="SUP_NAME" HeaderText="SupplierName">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="SUP_CONTACT_PERSON" SortExpression="SUP_CONTACT_PERSON" HeaderText="ContactPerson">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:MM/dd/yyyy}" DataField="SUP_EMAIL" SortExpression="SUP_EMAIL" HeaderText="Email">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
</columns>
                    <emptydatatemplate>
No Record Found!
</emptydatatemplate>
                </asp:GridView>
                <asp:SqlDataSource id="sdsSuppEnquiry" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SCM_SUPPLIERSENQUIRY_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="CPID" ControlID="lblCPID"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>
</selectparameters>
                </asp:SqlDataSource></td>
        </tr>
        <tr style="font-weight: bold">
            <td colspan="4" style="text-align: center">
            </td>
        </tr>
        <tr style="font-weight: bold">
            <td id="TD24" colspan="4" style="text-align: center;">
                <table id="Table1" align="center">
                    <tr>
                        <td>
                            <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CausesValidation="False" /></td>
                        <td style="width: 37px">
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" Visible="false" Width="42px" OnClick="btnEdit_Click" CausesValidation="False" /></td>
                        <td style="width: 47px">
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="false" OnClick="btnDelete_Click" CausesValidation="False" /></td>
                        <td style="width: 34px">
                            <asp:Button id="btnPrint" runat="server" Text="Print" Visible="false" Width="46px" CausesValidation="False" OnClick="btnPrint_Click" />

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" valign="top" style="height: 1px">
                <table border="0" cellpadding="0" cellspacing="0" id="tblSupEnqDetails" runat="server" visible="false" width="100%">
                    <tr>
            <td id="TD10" class="profilehead" colspan="4" style="text-align: left">
                <span>Supplier Enquiry Details</span></td>
                    </tr>
                    <tr>
            <td id="TD27" style="text-align: right">
            </td>
            <td style="text-align: left">
            </td>
            <td style="text-align: right">
            </td>
            <td style="text-align: left">
            </td>
                    </tr>
                    <tr>
            <td id="Td30" style="text-align: right">
                <asp:Label ID="Label3" runat="server" Text="Enquiry No" Width="76px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtEnquiryNo" runat="server" ReadOnly="True"></asp:TextBox></td>
            <td style="text-align: right">
                <asp:Label ID="Label4" runat="server" Text="Enquiry Date" Width="85px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtEnquiryDate" runat="server"></asp:TextBox>&nbsp;
                <asp:Label id="Label11" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEnquiryDate"
                    ErrorMessage="Please Select the Enquiry Date">*</asp:RequiredFieldValidator>
                
                <cc1:MaskedEditExtender ID="MaskedEditEnquiryDate" runat="server" DisplayMoney="Left"
                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtEnquiryDate"
                    UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
            </td>
                    </tr>
                    <tr>
            <td id="TD23" style="text-align: right">
                <asp:Label ID="lblOriginatedBy" runat="server" Text="Enquiry Originated By" Width="136px" Visible="False"></asp:Label></td>
            <td style="text-align: left">
                <asp:RadioButton ID="rdoDirectSupp" runat="server" GroupName="rbtOriginatedBy" Text="Direct Supplier" Checked="True" Visible="False" />
                <asp:RadioButton ID="rdoConsult" runat="server" GroupName="rbtOriginatedBy" Text="Consultancy" Visible="False" /></td>
            <td style="text-align: right">
            </td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlOriginatedBy" runat="server" AutoPostBack="True" Visible="False">
                    <asp:ListItem Value="0">--</asp:ListItem>
                    <asp:ListItem>DirectSupplier</asp:ListItem>
                    <asp:ListItem>Consultancy</asp:ListItem>
                </asp:DropDownList></td>
                    </tr>
                    <tr>
            <td id="TD11" style="text-align: right">
                </td>
            <td style="text-align: left">
                </td>
            <td style="text-align: right">
                <asp:Label ID="lblEnquiryStatus" runat="server" Text="Enquiry Status" Width="91px" Visible="False"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtEnquiryStatus" runat="server" Visible="False"></asp:TextBox>
                <asp:Label id="Label12" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtEnquiryStatus"
                    ErrorMessage="Please Enter Enquiry Status" Visible="False">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
            <td id="TD15" style="text-align: right">
                <asp:Label ID="lblCriteria" runat="server" Text="Employee Name" Width="103px"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlCriteria" runat="server">
                </asp:DropDownList>
                <asp:Label id="Label1" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlCriteria"
                    ErrorMessage="Please Select the Employee Name" InitialValue="0">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right">
                <asp:Label ID="lblEnquiryDueDate" runat="server" Text="Enquiry Due Date" Width="110px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtEnquiryDueDate" runat="server">
                </asp:TextBox>&nbsp;
                <asp:Label id="Label13" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEnquiryDueDate"
                    ErrorMessage="Please Select the Enquiry Due Date">*</asp:RequiredFieldValidator>  
                             
                <cc1:MaskedEditExtender ID="MaskededitEnquiryDueDate" runat="server" DisplayMoney="Left"
                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtEnquiryDueDate"
                    UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
            </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label23" runat="server" Text="Brand"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlBrand" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                <asp:Label ID="Label2" runat="server" Text="Delivery Type"></asp:Label></td>
                        <td style="text-align: left">
                <asp:DropDownList ID="ddlDeliveryType" runat="server">
                </asp:DropDownList>
                            <asp:Label id="Label15" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlDeliveryType"
                    ErrorMessage="Please Select the Delivery type" InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
            <td id="TD35" style="text-align: right">
                            <asp:Label id="lblIndentApprovalNo" runat="server" Text="Indent  No" Width="128px" Visible="False"></asp:Label>
            </td>
            <td style="text-align: left">
                <asp:DropDownList id="ddlIndentApprovel" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlIndentApprovel_SelectedIndexChanged" Visible="False">
                </asp:DropDownList>&nbsp;<asp:Label id="Label22" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label></td>
            <td>
            </td>
            <td>
            </td>
                    </tr>
                    <tr>
            <td id="TD16" class="profilehead" colspan="4" style="text-align: left">
                Supplier Details</td>
                    </tr>
                    <tr>
            <td id="TD5" style="height: 19px; text-align: right">
            </td>
            <td style="height: 19px; text-align: left">
            </td>
            <td style="height: 19px; text-align: right">
            </td>
            <td style="height: 19px; text-align: left">
            </td>
                    </tr>
                    <tr>
            <td id="TD18" style="text-align: right;">
                <asp:Label ID="lblCustomer" runat="server" Text="Supplier Name"></asp:Label></td>
            <td style="text-align: left;">
                <asp:DropDownList ID="ddlSupplierName" runat="server" OnSelectedIndexChanged="ddlSupplierName_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
                <asp:Label id="Label10" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlSupplierName"
                    ErrorMessage="Please Select the Supplier Name" ValidationGroup="s" InitialValue="0">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right;">
                <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtContactPerson" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
            <td id="TD28" style="text-align: right">
                <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" ReadOnly="True"></asp:TextBox></td>
            <td style="text-align: right">
                <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No" Width="74px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtPhoneNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
            <td id="TD2" style="text-align: right;">
                <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True"></asp:TextBox></td>
            <td style="text-align: right;">
                <asp:Label ID="lblCity" runat="server" Text="Mobile No" Width="81px"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
            <td id="TD36">
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
                    </tr>
                    <tr>
            <td>
            </td>
            <td style="text-align: right">
                <asp:Button ID="btnSuppDetails" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" Text="Add" OnClick="btnSuppDetails_Click" ValidationGroup="s" /></td>
            <td style="text-align: left">
                <asp:Button ID="btnSuppRefresh" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" Text="Refresh" OnClick="btnSuppRefresh_Click" CausesValidation="False" /></td>
            <td>
            </td>
                    </tr>
                    <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
                    </tr>
                    <tr>
            <td id="TD17" class="profilehead" colspan="4" style="text-align: left">
                Supplier Contact Person Details</td>
                    </tr>
                    <tr>
            <td id="TD6" runat="server" colspan="4">
                <asp:GridView id="gvSupplierDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSupplierDetails_RowDataBound" OnRowDeleting="gvSupplierDetails_RowDeleting" Width="100%">
                    <columns>
<asp:CommandField ShowDeleteButton="True"></asp:CommandField>
<asp:BoundField DataField="SuppId" HeaderText="SuppIdHidden"></asp:BoundField>
<asp:BoundField DataField="Name" HeaderText="Name">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ContactPerson" HeaderText="Contact Person">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PhoneNo" HeaderText="Phone No"></asp:BoundField>
<asp:BoundField DataField="Email" HeaderText="Email">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
</columns>
                    <emptydatatemplate>
<SPAN style="COLOR: #ff0000">No Data to Display </SPAN>
</emptydatatemplate>
                </asp:GridView>
            </td>
                    </tr>
                    <tr>
            <td id="TD7">
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4">
                            Indent Approved Items
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:GridView id="gvItem" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvItem_RowDataBound" 
                              >
                                <columns>
<asp:CommandField ShowEditButton="True"></asp:CommandField>
<asp:CommandField ShowDeleteButton="True"></asp:CommandField>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Model No">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ItemType" HeaderText="Model Name"></asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity">
<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Priority" HeaderText="Priority"></asp:BoundField>
<asp:BoundField DataField="Brand" HeaderText="Brand"></asp:BoundField>
<asp:BoundField DataField="ReqFor" HeaderText="Requried for">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ReqFor" HeaderText="Room">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:MM/dd/yyyy}" DataField="ReqDate" HeaderText="Required by Date"></asp:BoundField>
<asp:BoundField DataField="Specification" HeaderText="Specification">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Priority" HeaderText="Priority"></asp:BoundField>
<asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
<asp:BoundField DataField="ColorId" HeaderText="ColorId"></asp:BoundField>
<asp:BoundField DataField="Indetid" HeaderText="Indetid"></asp:BoundField>
<asp:TemplateField><ItemTemplate>
<asp:CheckBox id="chk" runat="server" __designer:wfdid="w8"></asp:CheckBox> 
</ItemTemplate>
</asp:TemplateField>
</columns>
                                <emptydatatemplate>
<SPAN style="COLOR: #ff0033">No Data to Display</SPAN> 
</emptydatatemplate>
                            </asp:GridView><asp:Button id="btnGo" runat="server" CausesValidation="False" onclick="btnGo_Click"
                                Text="Go" /></td>
                    </tr>
                    <tr>
            <td id="TD19" class="profilehead" colspan="4" style="text-align: left; height: 14px;">
                Interested Product</td>
                    </tr>
                    <tr>
            <td id="Td14" style="text-align: right">
            </td>
            <td style="text-align: left">
            </td>
            <td style="text-align: right">
            </td>
            <td style="text-align: left">
            </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: right">
                            <table width="100%">
                                <tr>
                                    <td align="center">
                <asp:GridView id="gvItemDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvItemDetails_RowDataBound"
                    OnRowDeleted="gvItemDetails_RowDeleted" OnRowDeleting="gvItemDetails_RowDeleting"
                    OnRowEditing="gvItemDetails_RowEditing" Width="100%">
                    <columns>
<asp:CommandField ShowEditButton="True"></asp:CommandField>
<asp:CommandField ShowDeleteButton="True"></asp:CommandField>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Model No">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ItemType" HeaderText="Model Name"></asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity">
<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Priority" HeaderText="Priority"></asp:BoundField>
<asp:BoundField DataField="Brand" HeaderText="Brand"></asp:BoundField>
<asp:BoundField DataField="ReqFor" HeaderText="Room">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Specification" HeaderText="Specification">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Priority" HeaderText="Priority"></asp:BoundField>
</columns>
                    <emptydatatemplate>
No Data to Display
</emptydatatemplate>
                </asp:GridView></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
            <td style="text-align: right;">
                <asp:Label ID="Label5" runat="server" Text="Model No" Visible="False"></asp:Label></td>
            <td style="text-align: left;">
                <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged" Visible="False">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlItemType"
                    ErrorMessage="Please Select the No" InitialValue="0" ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right;">
                <asp:Label ID="Label6" runat="server" Text="Model Name" Visible="False"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox id="txtModelName" runat="server" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label16" runat="server" Text="Item Category" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtCategory" runat="server" Visible="False"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label id="Label17" runat="server" Text="Item SubCategory" Width="113px" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtItemSubCategory" runat="server" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label18" runat="server" Text="Color" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtColor" runat="server" Visible="False"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label id="Label19" runat="server" Text="Brand" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtBrand" runat="server" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
            <td style="text-align: right;">
                <asp:Label ID="Label8" runat="server" Text="UOM" Visible="False"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox id="txtUOM" runat="server" ReadOnly="True" Visible="False"></asp:TextBox></td>
            <td style="text-align: right;">
                <asp:Label ID="Label7" runat="server" Text="Quantity" Visible="False"></asp:Label></td>
            <td style="text-align: left;" valign="bottom">
                <asp:TextBox ID="txtQuantity" runat="server" Visible="False"></asp:TextBox>
                <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtQuantity"
                    ErrorMessage="Please Enter the Quantity" ValidationGroup="ip" Visible="False">*</asp:RequiredFieldValidator><br />
                <cc1:FilteredTextBoxExtender ID="ftxteQuantity" runat="server" FilterType="Numbers"
                    TargetControlID="txtQuantity">
                </cc1:FilteredTextBoxExtender>
            </td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                <asp:Label id="lblForDate" runat="server" Text="Required by Date" Visible="False"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtReqByDate" runat="server" Visible="False"></asp:TextBox>
                <asp:Label id="Label20" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator
                        id="RequiredFieldValidator8" runat="server" ControlToValidate="txtReqByDate"
                        ErrorMessage="Please Select Required By Date" ValidationGroup="id" Visible="False">*</asp:RequiredFieldValidator><asp:CustomValidator
                            id="CustomValidator5" runat="server" ClientValidationFunction="DateCustomValidate"
                            ControlToValidate="txtReqByDate" ErrorMessage="Please Enter the Required By Date in DD/MM/YYYY Format or Check  Year in 2009 to 2099 Range or not"
                            SetFocusOnError="True" ValidationGroup="id" Visible="False">*</asp:CustomValidator>
               
                <cc1:MaskedEditExtender ID="MaskededitReqByDate" runat="server" DisplayMoney="Left"
                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtReqByDate"
                    UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
            </td>
            <td style="text-align: right">
                <asp:Label ID="lblPriority" runat="server" Text="Priority" Visible="False"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList id="ddlItemPriority" runat="server" Visible="False">
                    <asp:ListItem Value="0">--</asp:ListItem>
                    <asp:ListItem>Low</asp:ListItem>
                    <asp:ListItem>Medium</asp:ListItem>
                    <asp:ListItem>High</asp:ListItem>
                </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label21" runat="server" Text="Item Image" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:Image id="Image1" runat="server" Height="85px" ImageUrl="~/Images/noimage400x300.gif"
                                Width="140px" Visible="False">
                            </asp:Image></td>
                        <td style="text-align: right">
                <asp:Label ID="Label9" runat="server" Text="Specifications" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                <asp:TextBox ID="txtSpecifications" runat="server" TextMode="MultiLine" ReadOnly="True" Width="156px" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
            <td style="text-align: right">
            </td>
            <td style="text-align: right">
                <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" Text="Add" OnClick="btnAdd_Click" ValidationGroup="ip" Visible="False" /></td>
            <td style="text-align: left">
                <asp:Button ID="btnIntrstProduct" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" Text="Refresh" OnClick="btnIntrstProduct_Click" CausesValidation="False" Visible="False" /></td>
            <td style="text-align: left">
            </td>
                    </tr>
                    <tr>
            <td id="TD26" class="profilehead" colspan="4">
                Reference Details</td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlPreparedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label id="lblApprovedBy" runat="server" Text="Approved By"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlApprovedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
            <td id="TD37" colspan="4">
                <table id="tblButtons" align="center">
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                        <td>
                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" CausesValidation="False" /></td>
                        <td>
                            </td>
                        <td>
                            <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" /></td>
                    </tr>
                </table>
            </td>
                    </tr>
                </table>
                <asp:ValidationSummary id="ValidationSummary1" runat="server">
                </asp:ValidationSummary>
                <asp:ValidationSummary id="ValidationSummary2" runat="server" ValidationGroup="ip">
                </asp:ValidationSummary><asp:ValidationSummary id="ValidationSummary3" runat="server" ValidationGroup="s"></asp:ValidationSummary>
            </td>
        </tr>
        <tr>
            <td id="TD8" style="height: 21px">
                </td>
            <td style="height: 21px">
            </td>
            <td style="height: 21px">
            </td>
            <td style="height: 21px">
            </td>
        </tr>
    </table>
</asp:Content>


 
