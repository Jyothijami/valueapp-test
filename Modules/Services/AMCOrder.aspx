<%@ Page Language="C#" MasterPageFile="~/MPs/ServicesMP1.master" AutoEventWireup="true"
    CodeFile="AMCOrder.aspx.cs" Inherits="Modules_Services_AMCOrder" Title="|| YANTRA : Services : AMC Order ||" %>
<%--<%@ Register Assembly="FUA" Namespace="Subgurim.Controles" TagPrefix="cc2" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <script type="text/javascript" language="javascript">
function amtcalc()
{
    var req_qty,rate;
    req_qty=document.getElementById('<%=txtItemQuantity.ClientID %>').value;
    rate=document.getElementById('<%=txtItemRate.ClientID %>').value;
    
    if(req_qty=="" || req_qty=="0")
    {
        document.getElementById('<%=txtAmount.ClientID %>').value="0";
    }
    else if(rate=="" || rate=="0")
    {
        document.getElementById('<%=txtAmount.ClientID %>').value="0";
    }
    else if(rate>0 && req_qty>0)
    {
        document.getElementById('<%=txtAmount.ClientID %>').value=(rate*req_qty);
    }
}   

   function grosscalc()
    {
        var cst,amount,TOTAL;
        cst=parseFloat(document.getElementById('<%=txtCST.ClientID %>').value);
        amount=parseFloat(document.getElementById('<%=txtSubTotal.ClientID %>').value);
        
       
        if(cst=="" || cst=="0" || isNaN(cst)){cst="0";}
        if(amount=="" ||amount=="0" || isNaN(amount)){amount="0";}
       
        TOTAL=amount+((amount*cst)/100);
        document.getElementById('<%=txtTotal.ClientID %>').value= TOTAL;
    }
    
   
    
    </script>

    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                AMC order</td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" style="Width:100%">
        <tr>
            <td colspan="4" style="text-align: left" class="searchhead">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">
                            AMC Order</td>
                        <td>
                        </td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0" align=right>
                                <tr>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Label ID="Label12" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="AMCO_NO">Sales Order No</asp:ListItem>
                                            <asp:ListItem Value="AMCO_DATE">Sales Order Date</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME">Customer</asp:ListItem>
                                            <asp:ListItem Value="CUST_CONTACT_PERSON">Contact Person</asp:ListItem>
                                            <asp:ListItem Value="CUST_EMAIL">EMail</asp:ListItem>
                                            <asp:ListItem Value="AMCO_ACCEPTANCE_FLAG">Status</asp:ListItem>
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
                                        <cc1:CalendarExtender Format="MM/dd/yyyy" ID="ceSearchFrom" runat="server" Enabled="False"
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
                                        <cc1:CalendarExtender Format="MM/dd/yyyy" ID="ceSearchValueToDate" runat="server"
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
                            <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False">0</asp:Label>
                            <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 19px">
                <asp:GridView ID="gvOrderDetails" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="sdsSalesOrderDetails" OnRowDataBound="gvOrderDetails_RowDataBound" >
                    <Columns>
                        <asp:BoundField DataField="AMCO_ID" HeaderText="AMCOrderIdHidden"></asp:BoundField>
                        <asp:TemplateField HeaderText="AMC Order No">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("AMCO_NO") %>' ID="TextBox1"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnSalesOrderNo" runat="server" Text="<%# BIND('AMCO_NO') %>"
                                    CausesValidation="False" OnClick="lbtnSalesOrderNo_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:MM/dd/yyyy}" ReadOnly="True"
                            DataField="AMCO_DATE" HeaderText="AMC Order Date"></asp:BoundField>
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
                        <asp:BoundField DataField="PREPAREDBY" HeaderText="Prepared By">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="APPROVEDBY" HeaderText="Approved By">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="AMCO_ACCEPTANCE_FLAG" HeaderText="Status">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        No Data Exist!
                    </EmptyDataTemplate>
                    <SelectedRowStyle BackColor="LightSteelBlue" />
                </asp:GridView>
                <asp:SqlDataSource ID="sdsSalesOrderDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SERVICES_AMCORDER_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName"
                            ControlID="lblSearchItemHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType"
                            ControlID="lblSearchTypeHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue"
                            ControlID="lblSearchValueHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom"
                            ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="EMPID"
                            ControlID="lblEmpIdHidden"></asp:ControlParameter>
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 49px">
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
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table border="0" cellpadding="0" cellspacing="0" id="tblSalesOrderDetails" runat="server"
                    visible="false" width="750">
                    <tr>
                        <td class="profilehead" colspan="4">
                            general details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left; width: 269px;">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label1" runat="server" Text="Quotation No" Width="102px"></asp:Label></td>
                        <td style="text-align: left; width: 269px;">
                            <asp:DropDownList ID="ddlQuotationNo" runat="server" OnSelectedIndexChanged="ddlQuotationNo_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:Label ID="Label30" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlQuotationNo"
                                ErrorMessage="Please Select the Quotation No" InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label2" runat="server" Text="Quotation Date" Width="96px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtQuotationDate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox><asp:Image
                                ID="imgQuotationDate" runat="server" ImageUrl="~/Images/Calendar.png" /><cc1:CalendarExtender
                                    Format="dd/MM/yyyy" ID="ceQuotationDate" runat="server" PopupButtonID="imgQuotationDate"
                                    TargetControlID="txtQuotationDate">
                                </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeQuotationDate" runat="server" DisplayMoney="Left"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtQuotationDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td id="TD18" style="height: 22px; text-align: right">
                            <asp:Label ID="lblCustomer" runat="server" Text="Customer"></asp:Label></td>
                        <td style="height: 22px; text-align: left">
                            <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="True" Enabled="False"
                                OnSelectedIndexChanged="ddlCustomerName_SelectedIndexChanged">
                            </asp:DropDownList><asp:Label ID="Label25" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlCustomerName"
                                ErrorMessage="Please Select the Customer" InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="height: 22px; text-align: right">
                            <asp:Label ID="lblRegion" runat="server" Text="Region"></asp:Label></td>
                        <td style="height: 22px; text-align: left">
                            <asp:TextBox ID="txtRegion" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 22px; text-align: right">
                            <asp:Label ID="Label26" runat="server" Text="Industry Type"></asp:Label></td>
                        <td style="height: 22px; text-align: left">
                            <asp:TextBox ID="txtIndustryType" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="height: 22px; text-align: right">
                            <asp:Label ID="lblInitName" runat="server" Text="Unit Name" Width="74px"></asp:Label></td>
                        <td style="height: 22px; text-align: left">
                            <asp:DropDownList ID="ddlUnitName" runat="server" AutoPostBack="True" Enabled="False"
                                OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="0">--Select Customer--</asp:ListItem>
                            </asp:DropDownList><asp:RequiredFieldValidator ID="rfvUnitName" runat="server" ControlToValidate="ddlUnitName"
                                ErrorMessage="Please Select the Unit Name" InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblUnitAddress" runat="server" Text="Unit Address" Width="106px"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtUnitAddress" runat="server" CssClass="multilinetext" EnableTheming="False"
                                Font-Names="Verdana" Font-Size="8pt" ReadOnly="True" TextMode="MultiLine" Width="493px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtContactPerson" runat="server" ReadOnly="True">
                            </asp:TextBox><asp:DropDownList ID="ddlContactPerson" runat="server" AutoPostBack="True"
                                Enabled="False" OnSelectedIndexChanged="ddlContactPerson_SelectedIndexChanged"
                                Visible="False">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="0">--Select Unit Name--</asp:ListItem>
                            </asp:DropDownList><asp:RequiredFieldValidator ID="rfvContactPerson" runat="server"
                                ControlToValidate="ddlContactPerson" ErrorMessage="Please Select the Contact Person"
                                InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td id="TD28" style="text-align: right">
                            <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No" Width="74px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPhoneNo" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label47" runat="server" Text="Mobile" Width="74px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            </td>
                        <td style="text-align: left; width: 269px;">
                            </td>
                        <td style="text-align: right">
                            </td>
                        <td style="text-align: left">
                            </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;">
                        </td>
                        <td style="text-align: left; height: 19px; width: 269px;">
                        </td>
                        <td style="text-align: right; height: 19px;">
                        </td>
                        <td style="text-align: left; height: 19px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="gvQuotationProducts" runat="server" AutoGenerateColumns="False"
                                OnRowDataBound="gvQuotationItems_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Rate" HeaderText="Rate">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Amount">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4">
                            &nbsp;AMC Order details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left; width: 269px;">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblQuotationNo" runat="server" Text="AMC Order No" Width="102px"></asp:Label></td>
                        <td style="text-align: left; width: 269px;">
                            <asp:TextBox ID="txtSalesOrderNo" runat="server"></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblQuotationDate" runat="server" Text="AMC Order Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtSalesOrderDate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox><asp:Image
                                ID="imgSalesOrderDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender
                                    Format="MM/dd/yyyy" ID="ceSalesOrderDate" runat="server" PopupButtonID="imgSalesOrderDate"
                                    TargetControlID="txtSalesOrderDate">
                                </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeSalesOrderDate" runat="server" DisplayMoney="Left"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtSalesOrderDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label51" runat="server" Text="Cust PO No"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtCustPONo" runat="server"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label id="Label52" runat="server" Text="Cust PO Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtCustPODate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox><asp:Image id="imgCustPODate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender ID="ceCustPODate" runat="server"
                                    Format="dd/MM/yyyy" PopupButtonID="imgCustPODate" TargetControlID="txtCustPODate">
                                </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeCustPODate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtCustPODate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            &nbsp;<asp:Label ID="Label29" runat="server" Text="PM Calls"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPMCalls" runat="server"></asp:TextBox><asp:Label ID="Label57"
                                runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana" Font-Size="Smaller"
                                ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator8"
                                    runat="server" ControlToValidate="txtPMCalls" ErrorMessage="Please Enter the PM Calls">*</asp:RequiredFieldValidator>
                            <cc1:FilteredTextBoxExtender ID="ftxteRate" runat="server" TargetControlID="txtPMCalls"
                                ValidChars="0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label58" runat="server" Text="BD Calls"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtBDCalls" runat="server"></asp:TextBox><asp:Label ID="Label59"
                                runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana" Font-Size="Smaller"
                                ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator9"
                                    runat="server" ControlToValidate="txtBDCalls" ErrorMessage="Please Enter the Break Down Calls">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblAmc" runat="server" Text="AMC Till Date"></asp:Label></td>
                        <td style="text-align: left; width: 269px;">
                            <asp:TextBox ID="txtAmcDate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox><asp:Image
                                ID="imgAmcDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender
                                    ID="ceAmcDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgAmcDate"
                                    TargetControlID="txtAmcDate">
                                </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeAmcDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtAmcDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4">
                            item details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left; width: 269px;">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 22px;">
                            <asp:Label ID="Label3" runat="server" Text="Item Type"></asp:Label></td>
                        <td style="text-align: left; height: 22px; width: 269px;">
                            <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label ID="Label31" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlItemType"
                                ErrorMessage="Please Select the Item Type" InitialValue="0" ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right; height: 22px;">
                            <asp:Label ID="Label4" runat="server" Text="Item Name" Width="76px"></asp:Label></td>
                        <td style="text-align: left; height: 22px;">
                            <asp:DropDownList ID="ddlItemName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="0">-- Select Item Type --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="Label32" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlItemName"
                                ErrorMessage="Please Select the Item Name" InitialValue="0" ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="Label23" runat="server" Text="Item Specification"></asp:Label></td>
                        <td colspan="3" style="height: 21px; text-align: left">
                            <asp:TextBox ID="txtItemSpec" runat="server" CssClass="multilinetext" EnableTheming="False"
                                ReadOnly="True" TextMode="MultiLine" Width="547px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="Label22" runat="server" Text="Quantity" Width="57px"></asp:Label></td>
                        <td style="height: 21px; text-align: left; width: 269px;">
                            <asp:TextBox ID="txtItemQuantity" runat="server">
                            </asp:TextBox><asp:Label ID="Label33" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtItemQuantity"
                                    ErrorMessage="Please Enter the Quantity" ValidationGroup="ip">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                        ID="ftxtQuantity" runat="server" FilterType="Numbers" TargetControlID="txtItemQuantity">
                                    </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="Label24" runat="server" Text="Rate"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:TextBox ID="txtItemRate" runat="server"></asp:TextBox><asp:Label ID="Label34"
                                runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana" Font-Size="Smaller"
                                ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                    runat="server" ControlToValidate="txtItemRate" ErrorMessage="Please Enter the Rate"
                                    ValidationGroup="ip">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender ID="ftxteItemRate"
                                        runat="server" FilterType="Numbers" TargetControlID="txtItemRate">
                                    </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="Label39" runat="server" Text="Amount"></asp:Label></td>
                        <td style="width: 269px; height: 21px; text-align: left">
                            <asp:TextBox ID="txtAmount" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="Label21" runat="server" Text="Remarks"></asp:Label></td>
                        <td style="height: 21px; text-align: left;" colspan="3">
                            <asp:TextBox ID="txtItemRemarks" runat="server" TextMode="MultiLine" Width="553px"
                                EnableTheming="False" CssClass="multilinetext"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td colspan="3" style="height: 19px; text-align: right">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" Text="Add" ValidationGroup="ip"
                                OnClick="btnAdd_Click" />
                            <asp:Button ID="btnRefreshItems" runat="server" BackColor="Transparent" BorderStyle="None"
                                CausesValidation="False" CssClass="loginbutton" EnableTheming="False" Text="Refresh"
                                OnClick="btnItemRefresh_Click" />&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left; width: 269px;">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="gvOrderItems" runat="server" AutoGenerateColumns="False" Width="100%"
                                OnRowDeleting="gvOrderItems_RowDeleting" OnRowDataBound="gvOrderItems_RowDataBound">
                                <Columns>
<asp:CommandField ShowDeleteButton="True"></asp:CommandField>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
<asp:BoundField DataField="ItemType" HeaderText="Item Type"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
<asp:BoundField HeaderText="Amount"></asp:BoundField>
<asp:BoundField DataField="Specifications" NullDisplayText="-" HeaderText="Specifications"></asp:BoundField>
<asp:BoundField DataField="Remarks" NullDisplayText="-" HeaderText="Remarks"></asp:BoundField>
<asp:BoundField DataField="Priority" NullDisplayText="-" HeaderText="Priority"></asp:BoundField>
<asp:BoundField DataField="ItemTypeId" HeaderText="ItemTypeIdHidden"></asp:BoundField>
</Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4">
                            terms &amp; conditions</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left; width: 269px;">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label id="Label54" runat="server" Text="Sub Total" Width="74px"></asp:Label></td>
                        <td style="width: 269px; height: 19px; text-align: left">
                            <asp:TextBox id="txtSubTotal" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label8" runat="server" Text="Service Tax"></asp:Label></td>
                        <td style="text-align: left; width: 269px;">
                            <asp:TextBox ID="txtCST" runat="server">
                            </asp:TextBox><asp:Label id="Label53" runat="server" Text="%"></asp:Label><asp:Label ID="Label38" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                    ID="rfvCST" runat="server" ControlToValidate="txtCST" ErrorMessage="Please Enter the CS Tax">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtTotal" runat="server">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label18" runat="server" Text="payment terms"></asp:Label></td>
                        <td colspan="3" style="height: 19px; text-align: left">
                            <asp:TextBox ID="txtPaymentTerms" runat="server" EnableTheming="False" TextMode="MultiLine"
                                Width="547px" CssClass="multilinetext"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label16" runat="server" Text="jurisdiction"></asp:Label></td>
                        <td style="height: 19px; text-align: left; width: 269px;">
                            <asp:TextBox ID="txtJurisdiction" runat="server">
                            </asp:TextBox></td>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label17" runat="server" Text="inspection"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:TextBox ID="txtInspection" runFollow Up Details (at customer place)</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left; width: 269px;">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblResponsiblePerson" runat="server" Text="Responsible Person" Width="134px">
                            </asp:Label></td>
                        <td style="text-align: left; width: 269px;">
                            <asp:TextBox ID="txtResponsiblePerson" runat="server"></asp:TextBox>
                            <asp:Label ID="Label49" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvResPerson" runat="server" ControlToValidate="txtResponsiblePerson"
                                ErrorMessage="Please Enter the Responsible Person" InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblFollowupEmail" runat="server" Text="Email"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtFollowupEmail" runat="server"></asp:TextBox>
                            <asp:Label ID="Label50" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtFollowupEmail"
                                    ErrorMessage="Please Enter the Responsible Person Email" InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label55" runat="server" Text="Consignement To"></asp:Label></td>
                        <td style="text-align: left" colspan="3">
                            <asp:TextBox ID="txtConsignee" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="547px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label46" runat="server" Text="Attachment"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label56" runat="server" Text="Attached File"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:Repeater id="UploadsRepeater" runat="server" DataSourceID="sdsUploads">
                                <itemtemplate>
                                            <asp:LinkButton id="lbtnFileOpener" CausesValidation="False"  runat="server" OnClick="lbtnFileOpener_Click" Text='<%# bind("AMCO_UPLOAD_FILENAME") %>'></asp:LinkButton>
                                        </itemtemplate>
                            </asp:Repeater>
                            <asp:SqlDataSource id="sdsUploads" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                SelectCommand="SELECT * FROM [YANTRA_AMCO_UPLOADS] WHERE AMCO_ID=@AMCO_IDpara">
                                <selectparameters>
<asp:ControlParameter PropertyName="Text" DefaultValue="0" Name="AMCO_IDpara" ControlID="lblAMCOIdHidden"></asp:ControlParameter>
</selectparameters>
                            </asp:SqlDataSource><asp:Label
                                    id="lblAMCOIdHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Reference DetailsOIdHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Reference Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left; width: 269px;">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left; width: 269px;">
                            <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlApprovedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="lblCheckedBy" runat="server" Text="Checked By" Visible="False"></asp:Label></td>
                        <td style="height: 19px; text-align: left; width: 269px;">
                            <asp:DropDownList ID="ddlCheckedBy" runat="server" Enabled="False" Visible="False">
                            </asp:DropDownList></td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px">
                        </td>
                        <td style="height: 19px; width: 269px;">
                        </td>
                        <td style="height: 19px">
                        </td>
                        <td style="height: 19px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 35px">
                            <table id="tblButtons">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnApprove" runat="server" CausesValidation="False" OnClick="btnApprove_Click"
                                            Text="Approve" /></td>
                                    <td>
                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click"
                                            CausesValidation="False" /></td>
                                    <td>
                                        <asp:Button ID="btnSend" runat="server" CausesValidation="False" OnClick="btnSend_Click"
                                            Text="Send" Visible="False" /></td>
                                    <td>
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" Visible="False" /></td>
                                    <td>
                                        <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label5" runat="server" Text="Delivery" Visible="False"></asp:Label></td>
                        <td style="text-align: left; width: 269px;">
                            <asp:TextBox ID="txtDelivery" runat="server" Visible="False"></asp:TextBox>
                            <asp:Label ID="Label36" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvDelivery" runat="server" ControlToValidate="txtDelivery"
                                ErrorMessage="Please Enter the Delivery" Visible="False">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label28" runat="server" Text="Currenct Type" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlCurrencyType" runat="server" Visible="False">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label19" runat="server" Text="packing charges" Visible="False"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 269px;">
                            <asp:TextBox ID="txtPackingCharges" runat="server" Visible="False"></asp:TextBox>
                            <asp:Label ID="Label37" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvPackingChrgs" runat="server" ControlToValidate="txtPackingCharges"
                                ErrorMessage="Please Enter the Packing Charges" Visible="False">*</asp:RequiredFieldValidator>
                            <cc1:FilteredTextBoxExtender ID="ftxtePackingCharges" runat="server" FilterType="Numbers"
                                TargetControlID="txtPackingCharges">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label7" runat="server" Text="Excise Dutry" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtExciseDuty" runat="server" Visible="False"></asp:TextBox><asp:Label
                                ID="Label40" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator
                                    ID="rfvExciseDuty" runat="server" ControlToValidate="txtExciseDuty" ErrorMessage="Please Enter the Excise Duty"
                                    Visible="False">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="Label10" runat="server" Text="Guarantee" Visible="False"></asp:Label></td>
                        <td style="text-align: left; width: 269px;">
                            <asp:TextBox ID="txtGuarantee" runat="server" Visible="False"></asp:TextBox>
                            <asp:Label ID="Label42" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvGuarantee" runat="server" ControlToValidate="txtGuarantee"
                                ErrorMessage="Please Enter the Guarantee" Visible="False">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label9" runat="server" Text="Despatch Mode" Visible="False"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlDespatchMode" runat="server" Visible="False">
                            </asp:DropDownList>
                            <asp:Label ID="Label41" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvDespatchMode" runat="server" ControlToValidate="ddlDespatchMode"
                                ErrorMessage="Please Enter the Despatch Mode" Visible="False">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="Label13" runat="server" Text="Insurance" Visible="False"></asp:Label></td>
                        <td style="text-align: left; width: 269px;">
                            <asp:TextBox ID="txtInsurance" runat="server" Visible="False"></asp:TextBox>
                            <asp:Label ID="Label43" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvInsurance" runat="server" ControlToValidate="txtInsurance"
                                ErrorMessage="Please Enter the Insurance" Visible="False">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label11" runat="server" Text="Transportation Charges" Width="152px"
                                Visible="False"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtTransCharges" runat="server" Visible="False"></asp:TextBox>
                            <asp:Label ID="Label44" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvTransCharges" runat="server" ControlToValidate="txtTransCharges"
                                ErrorMessage="Please Enter the Transportation Charges" Visible="False">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                    ID="ftxteTransCharges" runat="server" FilterType="Numbers" TargetControlID="txtTransCharges">
                                </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label15" runat="server" Text="validity" Visible="False"></asp:Label></td>
                        <td style="text-align: left; width: 269px;">
                            <asp:TextBox ID="txtValidity" runat="server" Visible="False"></asp:TextBox><asp:Label
                                ID="Label48" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator
                                    ID="rfvValidity" runat="server" ControlToValidate="txtValidity" ErrorMessage="Please Enter the Validity"
                                    Visible="False">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender ID="ftxteValidity"
                                        runat="server" FilterType="Numbers" TargetControlID="txtValidity">
                                    </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label14" runat="server" Text="Erection/Commisioning" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtErrection" runat="server" Visible="False"></asp:TextBox>
                            <asp:Label ID="Label45" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvErrection" runat="server" ControlToValidate="txtErrection"
                                ErrorMessage="Please Enter the Erection/Commissioning" Visible="False">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label6" runat="server" Text="other specifications" Visible="False"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtOtherSpecs" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="613px" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="Label27" runat="server" Text="UOM" Visible="False"></asp:Label><asp:TextBox
                                ID="txtItemUOM" runat="server" ReadOnly="True" Visible="False"></asp:TextBox><asp:Label
                                    ID="lblPriority" runat="server" Text="Priority" Visible="False"></asp:Label><asp:DropDownList
                                        ID="ddlItemPriority" runat="server" Visible="False">
                                        <asp:ListItem Value="0">--</asp:ListItem>
                                        <asp:ListItem>Low</asp:ListItem>
                                        <asp:ListItem>Medium</asp:ListItem>
                                        <asp:ListItem>High</asp:ListItem>
                                    </asp:DropDownList><asp:Label ID="Label35" runat="server" EnableTheming="False" Font-Bold="False"
                                        Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator
                                            ID="rfvPriority" runat="server" ControlToValidate="ddlItemPriority" ErrorMessage="Please Select the Priority"
                                            InitialValue="0" ValidationGroup="ip" Visible="False">*</asp:RequiredFieldValidator><asp:Label
                                                ID="Label20" runat="server" Text="Specifications" Visible="False"></asp:Label><asp:TextBox
                                                    ID="txtItemSpecifications" runat="server" TextMode="MultiLine" Visible="False"></asp:TextBox>
                            <asp:DropDownList ID="ddlResponsiblePerson" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlResponsiblePerson_SelectedIndexChanged" Visible="False">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:TextBox ID="txtFollowUpDetails" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Visible="False"></asp:TextBox>
                            <asp:Label ID="lblSalesPerson" runat="server" Text="Service Person" Visible="False"></asp:Label><asp:DropDownList ID="ddlSalesPerson" runat="server" Visible="False">
                            </asp:DropDownList></td>
                    </tr>
                </table>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server"></asp:ValidationSummary>
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="qi"></asp:ValidationSummary>
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
    </table>
</asp:Content>

 
