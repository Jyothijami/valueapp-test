<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="WorkOrderDetails.aspx.cs" Inherits="Modules_PurchasingManagement_WorkOrderDetails"
    Title="|| YANTRA : Purchasing Management : Work Order Details ||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <script type="text/javascript">
function amtcalc()
{
    var req_qty,rate,tax;
    req_qty=document.getElementById('<%=txtItemQuantity.ClientID %>').value;
    rate=document.getElementById('<%=txtItemRate.ClientID %>').value;  
   
    if(req_qty=="" || req_qty=="0")
    {
        document.getElementById('<%=txtTotal.ClientID %>').value="0";
    }
    else if(rate=="" || rate=="0")
    {
        document.getElementById('<%=txtTotal.ClientID %>').value="0";
    }
    else if(rate>0 && req_qty>0)
    {
        document.getElementById('<%=txtTotal.ClientID %>').value=(rate*req_qty);
    }
}   

function netamtcalc()
{
    var disc,subtot,tax;
    try   { tax=document.getElementById('<%=txtCSTTax.ClientID %>').value; }  catch(e)  { tax="0"; } 
    disc=document.getElementById('<%=txtDiscount.ClientID %>').value;
    subtot=document.getElementById('<%=txtSubTotal.ClientID %>').value;
    if(disc=="" || disc=="0" || isNaN(disc)){disc="0";}    
    if(tax=="" || tax=="0" || isNaN(tax)){tax="0";}    
    if(subtot>0 )
    {
        document.getElementById('<%=txtNetAmount.ClientID %>').value=parseFloat(subtot)+parseFloat(tax*subtot/100)-parseFloat(disc*subtot/100)
    }
}   
    </script>

    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                work ORDER DETAILS</td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td width="750">
            </td>
        </tr>
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">
                            work order</td>
                        <td>
                        </td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Label ID="Label12" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="SUP_WO_NO">PO No.</asp:ListItem>
                                            <asp:ListItem Value="SUP_WO_DATE">PO Date</asp:ListItem>
                                            <asp:ListItem Value="SUP_NAME">Supplier</asp:ListItem>
                                            <asp:ListItem Value="SUP_CONTACT_PERSON">Contact Person</asp:ListItem>
                                            <asp:ListItem Value="SUP_EMAIL">EMail</asp:ListItem>
                                            <asp:ListItem Value="SUP_WO_STATUS">Status</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:DropDownList ID="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                            EnableTheming="False" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged"
                                            Visible="False" Width="50px">
                                            <asp:ListItem Selected="True">=</asp:ListItem>
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem>&gt;</asp:ListItem>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <asp:ListItem>&gt;=</asp:ListItem>
                                            <asp:ListItem>R</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:TextBox ID="txtSearchValueFromDate" runat="server" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox><asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchFrom" runat="server" Enabled="False"
                                            PopupButtonID="imgFromDate" TargetControlID="txtSearchValueFromDate">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchValueToDate" runat="server"
                                            Enabled="False" PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                </tr>
                            </table>
                            <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td id="Td20" style="height: 19px" colspan="4">
                <asp:GridView ID="gvFixedPODetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="sdsFixedPODetails" OnRowDataBound="gvFixedPODetails_RowDataBound">
                    <Columns>
<asp:BoundField DataField="SUP_WO_ID" HeaderText="WOIdHidden"></asp:BoundField>
<asp:TemplateField HeaderText="WO No">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
                                <asp:LinkButton ID="lbtnFixedPONo" runat="server" Text="<%# BIND('SUP_WO_NO') %>"
                                    CausesValidation="False" OnClick="lbtnFixedPONo_Click"></asp:LinkButton>
                            
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True" DataField="SUP_WO_DATE" HeaderText="WO Date"></asp:BoundField>
<asp:BoundField DataField="INDIGENOUS_FOREIGN" HeaderText="Indigenous Or Foreign"></asp:BoundField>
<asp:BoundField DataField="SUP_NAME" HeaderText="Supplier Name">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="SUP_CONTACT_PERSON" HeaderText="Contact Person">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="SUP_EMAIL" HeaderText="Email">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="SUP_WO_STATUS" HeaderText="Status">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="EPreparedBy" SortExpression="EPreparedBy" HeaderText="Prepared By"></asp:BoundField>
<asp:BoundField DataField="EApprovedBy" SortExpression="EApprovedBy" HeaderText="Approved By"></asp:BoundField>
</Columns>
                    <EmptyDataTemplate>
                        No Record Found
                    
</EmptyDataTemplate>
                    <SelectedRowStyle BackColor="LightSteelBlue" />
                </asp:GridView>
                <asp:SqlDataSource ID="sdsFixedPODetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SCM_SUPPLIER_WORKORDER_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName"
                            ControlID="lblSearchItemHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType"
                            ControlID="lblSearchTypeHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue"
                            ControlID="lblSearchValueHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom"
                            ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 19px">
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table id="Table1">
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
                        <td>
                            <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Print" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 19px">
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table border="0" cellpadding="0" cellspacing="0" id="tblFixedPODetails" runat="server"
                    visible="false" width="100%">
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
                            <asp:Label ID="lblPONo" runat="server" Text="WO No"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtWONo" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblQuotationDate" runat="server" Text="WO Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtWODate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox><asp:Image
                                ID="imgPODate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender
                                    Format="dd/MM/yyyy" ID="cePODate" runat="server" PopupButtonID="imgPODate" TargetControlID="txtWODate">
                                </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meePODate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtWODate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label1" runat="server" Text="Delivery Type"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlDespatchMode" runat="server">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label2" runat="server" Text="Contact Person"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlContactPerson" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblPOType" runat="server" Text="Reference" Width="61px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtSuppReference" runat="server"></asp:TextBox></td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">
                            Customer Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:RadioButton ID="rbIndigenous" runat="server" AutoPostBack="True" GroupName="i"
                                OnCheckedChanged="rbIndigenous_CheckedChanged" Text="Indigenous" /><asp:RadioButton
                                    ID="rbForeign" runat="server" AutoPostBack="True" GroupName="i" OnCheckedChanged="rbForeign_CheckedChanged"
                                    Text="Foreign" /></td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label14" runat="server" Text="Supplier Name" Width="95px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtSupplierName" runat="server">
                            </asp:TextBox>
                            <asp:Label ID="Label36" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                            </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSupplierName"
                                ErrorMessage="Please Enter the Supplier Name">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person" Width="95px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtContactPerson" runat="server">
                            </asp:TextBox>
                            <asp:Label ID="Label15" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                            </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtContactPerson"
                                ErrorMessage="Please enter the Contact Person">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblAddress" runat="server" Text="Address" Width="56px"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtAddress" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="571px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblContactNo1" runat="server" Text="Phone No." Width="70px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtContactNo1" runat="server"></asp:TextBox>
                            <asp:Label ID="Label16" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                            </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtContactNo1"
                                ErrorMessage="Please Enter the Phone No">*</asp:RequiredFieldValidator>
                            <cc1:FilteredTextBoxExtender ID="ftxtePhoneNo" runat="server" TargetControlID="txtContactNo1"
                                ValidChars="-0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="lblContactNo2" runat="server" Text="Mobile No." Width="78px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtContactNo2" runat="server"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftxteMobileNo" runat="server" TargetControlID="txtContactNo2"
                                ValidChars="-0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                            <asp:TextBox ID="txtEmail" runat="server">
                            </asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="Please enter the email  in  correct format" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td>
                        <td style="height: 24px; text-align: right">
                            <asp:Label ID="lblFaxNo" runat="server" Text="Fax No" Width="69px"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                            <asp:TextBox ID="txtFaxNo" runat="server"></asp:TextBox><%@ register assembly="AjaxControlToolkit"
                                namespace="AjaxControlToolkit" tagprefix="cc1" %>
                            <cc1:FilteredTextBoxExtender ID="ftxteFaxNo" runat="server" FilterType="Numbers"
                                TargetControlID="txtFaxNo">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4">
                            item details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 22px;">
                            <asp:Label ID="Label11" runat="server" Text="Model No"></asp:Label></td>
                        <td style="text-align: left; height: 22px;">
                            <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged">
                            </asp:DropDownList><asp:Label ID="Label32" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*">
                            </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                ControlToValidate="ddlItemType" ErrorMessage="Please Select the Item Type" InitialValue="0"
                                ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right; height: 22px;">
                            <asp:Label ID="Label3" runat="server" Text="Model  Name" Width="85px"></asp:Label></td>
                        <td style="text-align: left; height: 22px;">
                            <asp:TextBox id="txtModelName" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label6" runat="server" Text="ItemCategory"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtItemCategory" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label id="Label8" runat="server" Text="Item SubCategory"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtItemSubCategory" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label27" runat="server" Text="Color"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtColor" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label id="Label28" runat="server" Text="Brand"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtBrand" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label22" runat="server" Text="UOM"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtItemUOM" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label23" runat="server" Text="Quantity"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtItemQuantity" runat="server">
                            </asp:TextBox>
                            <asp:Label ID="Label5" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtItemQuantity"
                                ErrorMessage="Please Enter the Quantity" ValidationGroup="ip">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                    ID="ftxtQuantity" runat="server" FilterType="Numbers" TargetControlID="txtItemQuantity">
                                </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="Label24" runat="server" Text="Rate"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:TextBox ID="txtItemRate" runat="server">
                            </asp:TextBox>
                            <asp:Label ID="Label4" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtItemRate"
                                ErrorMessage="Please Enter the Rate" ValidationGroup="ip">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                    ID="ftxteItemRate" runat="server" TargetControlID="txtItemRate" ValidChars=".0123456789">
                                </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="Label9" runat="server" Text="Total Amount"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:TextBox ID="txtTotal" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="Label7" runat="server" Text="Delivery Date"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:TextBox ID="txtDeliveryDate" runat="server" EnableTheming="False" CssClass="datetext"></asp:TextBox><asp:Image
                                ID="imgItemDelDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender
                                    Format="dd/MM/yyyy" ID="CalendarExtender1" runat="server" PopupButtonID="imgItemDelDate"
                                    TargetControlID="txtDeliveryDate">
                                </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" DisplayMoney="Left"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtDeliveryDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                        <td style="height: 21px; text-align: right">
                            </td>
                        <td style="height: 21px; text-align: left">
                            </td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="Label20" runat="server" Text="Specifications"></asp:Label></td>
                        <td style="height: 21px; text-align: left" colspan="3">
                            <asp:TextBox ID="txtItemSpecifications" runat="server" TextMode="MultiLine" EnableTheming="False"
                                Width="568px" CssClass="multilinetext"></asp:TextBox>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="Label21" runat="server" Text="Remarks"></asp:Label></td>
                        <td colspan="3" style="height: 21px; text-align: left">
                            <asp:TextBox ID="txtItemRemarks" runat="server" TextMode="MultiLine" EnableTheming="False"
                                Width="566px" CssClass="multilinetext"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label id="Label29" runat="server" Text="Item Image"></asp:Label></td>
                        <td style="height: 19px; text-align: left" colspan="3">
                            <asp:Image id="Image1" runat="server" Height="85px" ImageUrl="~/Images/noimage400x300.gif"
                                Width="140px">
                            </asp:Image>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="4">
                            <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" OnClick="btnAdd_Click" Text="Add"
                                ValidationGroup="ip" /><asp:Button ID="btnRefreshItems" runat="server" BackColor="Transparent"
                                    BorderStyle="None" CausesValidation="False" CssClass="loginbutton" EnableTheming="False"
                                    OnClick="btnItemRefresh_Click" Text="Refresh" /></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="gvPOItems" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvPOItems_RowDeleting"
                                Width="100%" OnRowDataBound="gvPOItems_RowDataBound" OnRowEditing="gvPOItems_RowEditing">
                                <Columns>
<asp:CommandField ShowEditButton="True"></asp:CommandField>
<asp:CommandField ShowDeleteButton="True"></asp:CommandField>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
<asp:BoundField DataField="ItemType" HeaderText="Model No"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Model Name"></asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
<asp:BoundField DataField="Tax" HeaderText="Tax"></asp:BoundField>
<asp:BoundField HeaderText="Amount"></asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:MM/dd/yyyy}" DataField="DeliveryDate" HeaderText="Delivery Date"></asp:BoundField>
<asp:BoundField DataField="Specifications" NullDisplayText="-" HeaderText="Specifications"></asp:BoundField>
<asp:BoundField DataField="Remarks" NullDisplayText="-" HeaderText="Remarks"></asp:BoundField>
<asp:BoundField DataField="ItemTypeId" HeaderText="ItemTypeIdHidden"></asp:BoundField>
</Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;">
                        </td>
                        <td style="text-align: left; height: 19px;">
                        </td>
                        <td style="height: 19px">
                        </td>
                        <td style="text-align: left; height: 19px;">
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4">
                            Terms &amp; conditions</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td colspan="3" style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label13" runat="server" Text="Terms & Conditions"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtTermsConditions" runat="server" CssClass="multilinetext" EnableTheming="False"
                                Height="42px" TextMode="MultiLine" Width="576px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label17" runat="server" Text="Terms Of Delivery"></asp:Label></td>
                        <td colspan="3" style="height: 19px; text-align: left">
                            <asp:TextBox ID="txtTermsOfDelivery" runat="server" CssClass="multilinetext" EnableTheming="False"
                                Height="42px" TextMode="MultiLine" Width="576px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label113" runat="server" Text="Destination"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDestination" runat="server"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label117" runat="server" Text="Insurance"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtInsurance" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label18" runat="server" Text="Freight"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtFreight" runat="server"></asp:TextBox></td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td colspan="3" style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4">
                            Payment terms</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 19px; text-align: left">
                            <asp:CheckBoxList ID="chklIndigenous" runat="server" Width="100%" Visible="False">
                                <asp:ListItem>100% along with PO</asp:ListItem>
                                <asp:ListItem>100% against delivery</asp:ListItem>
                                <asp:ListItem>PDC</asp:ListItem>
                            </asp:CheckBoxList>
                            <asp:CheckBoxList ID="chklForeign" runat="server" Width="100%" Visible="False">
                                <asp:ListItem>75% wire transfer against Pro-forma Invoice and 25% against Delivery</asp:ListItem>
                                <asp:ListItem>100% wire transfer against Pro-forma Invoice</asp:ListItem>
                                <asp:ListItem>100% wire transfer against Delivery</asp:ListItem>
                            </asp:CheckBoxList></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4">
                            Payments</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 30px;">
                            <asp:Label ID="Label19" runat="server" Text="Sub Total" Width="88px"></asp:Label></td>
                        <td style="text-align: left; height: 30px;">
                            <asp:TextBox ID="txtSubTotal" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right; height: 30px;">
                            <asp:Label ID="Label25" runat="server" Text="Discount" Width="88px"></asp:Label></td>
                        <td style="text-align: left; height: 30px;">
                            <asp:TextBox ID="txtDiscount" runat="server"></asp:TextBox><asp:Label ID="Label26"
                                runat="server" Text="%" Width="17px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblTaxCST" runat="server" Text="Taxes -CST" Width="88px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCSTTax" runat="server"></asp:TextBox><asp:Label ID="lblTaxCSTPercent"
                                runat="server" Text="%" Width="17px"></asp:Label></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblNetAmount" runat="server" Text="Net Amount" Width="88px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtNetAmount" runat="server" ReadOnly="True"></asp:TextBox></td>
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
                        <td style="text-align: right">
                            <asp:Label ID="lblAmountInWords" runat="server" Text=" Sub Total Amount In Words" Width="189px"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            &nbsp;<asp:TextBox ID="txtAmountInWords" runat="server" Width="571px" CssClass="textbox"
                                EnableTheming="False" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 20px;">
                        </td>
                        <td style="height: 20px">
                        </td>
                        <td style="height: 20px">
                        </td>
                        <td style="height: 20px">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 21px;">
                            <asp:Label ID="Label10" runat="server" Text="Currency Type"></asp:Label></td>
                        <td style="height: 21px; text-align: left;">
                            <asp:DropDownList ID="ddlCurrencyType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCurrencyType_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="height: 21px; text-align: right;">
                            <asp:Label ID="lblOtherCurrencyValue" runat="server" Text="Net Amount In Other Currency"
                                Width="196px"></asp:Label></td>
                        <td style="height: 21px; text-align: left;">
                            <asp:TextBox ID="txtNetAmtInOtherCurrency" runat="server"></asp:TextBox></td>
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
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Reference Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlApprovedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
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
                        <td colspan="4">
                            <table id="tblButtons">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" /></td>
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
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 23px">
            </td>
            <td style="height: 23px">
            </td>
            <td style="height: 23px">
            </td>
            <td width="750" style="height: 23px">
            </td>
        </tr>
    </table>
    <asp:TextBox ID="txtTax" runat="server" Visible="False"></asp:TextBox><cc1:FilteredTextBoxExtender
        ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtTax" ValidChars=".0123456789">
    </cc1:FilteredTextBoxExtender>
</asp:Content>

 
