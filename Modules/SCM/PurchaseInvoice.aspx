<%@ Page Language="C#" MasterPageFile="~/MPs/PurchaseMP1.master" AutoEventWireup="true" 
    CodeFile="PurchaseInvoice.aspx.cs" Inherits="Modules_SCM_PurchaseInvoice" Title="|| Value App : SM : Purchase Invoice ||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">

    <script type="text/javascript" language="javascript">
function amtcalc()
{
   
    var req_qty,rate,vat,cst,excise;
    req_qty=document.getElementById('<%=txtQuantity.ClientID %>').value;
    rate=document.getElementById('<%=txtRate.ClientID %>').value;
    cst=document.getElementById('<%=txtCST.ClientID %>').value;
    vat=document.getElementById('<%=txtVAT.ClientID %>').value;
    //excise=document.getElementById('<%=txtExcise.ClientID %>').value;
    if(cst=="" || cst=="0" || isNaN(cst)){cst="0";}
    if(vat=="" || vat=="0" || isNaN(vat)){vat="0";}
    if(excise=="" || excise=="0" || isNaN(excise)){excise="0";}
    
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
        document.getElementById('<%=txtAmount.ClientID %>').value=(rate*req_qty)+parseFloat(cst*(rate*req_qty)/100)+parseFloat(vat*(rate*req_qty)/100)+parseFloat(excise*(rate*req_qty)/100);
    }
   
}   

   function grosscalc()
    {
        var disc,grossamt,misc,transchargs,TOTAL;
        misc=parseFloat(document.getElementById('<%=txtMiscelleneous.ClientID %>').value);
        disc=parseFloat(document.getElementById('<%=txtDiscount.ClientID %>').value);
        grossamt=parseFloat(document.getElementById('<%=txtGrossTotalAmtHidden.ClientID %>').value);
        if(grossamt=="" || grossamt=="0" || isNaN(grossamt)){grossamt="0";}
        if(misc=="" || misc=="0" || isNaN(misc)){misc="0";}
        if(disc=="" || disc=="0" || isNaN(disc)){disc="0";}
        TOTAL=parseFloat(grossamt)+parseFloat(misc);
        TOTAL=TOTAL-((disc*TOTAL)/100);
        document.getElementById('<%=txtGrossAmount.ClientID %>').value= TOTAL;
    }
    
    function rbVATCSTEnableDisable()
    {
        if(document.getElementById('<%=rbVAT.ClientID %>').checked==true)
        {
            document.getElementById('<%=txtVAT.ClientID %>').style.display=document.getElementById('<%=lblVAT.ClientID %>').style.display ="";
            document.getElementById('<%=txtCST.ClientID %>').style.display=document.getElementById('<%=lblCSTax.ClientID %>').style.display ="none";
            document.getElementById('<%=txtVAT.ClientID %>').focus();
        }  
        if(document.getElementById('<%=rbCST.ClientID %>').checked==true)
        {
            document.getElementById('<%=txtVAT.ClientID %>').style.display=document.getElementById('<%=lblVAT.ClientID %>').style.display ="none";
            document.getElementById('<%=txtCST.ClientID %>').style.display = document.getElementById('<%=lblCSTax.ClientID %>').style.display ="";
            document.getElementById('<%=txtCST.ClientID %>').focus();       
        } 
    document.getElementById('<%=txtVAT.ClientID %>').value="";
    document.getElementById('<%=txtCST.ClientID %>').value="";
//    document.getElementById('<%=txtExcise.ClientID %>').value="";
   
       amtcalc();
    }
    </script>

    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                Purchase
                Invoice</td>
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
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" id="TABLE2" width="100%">
                    <tr>
                        <td style="text-align: left" colspan="2">
                                    Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
                                </td>

                        
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td style="height: 25px">
                                        <asp:Label ID="Label20" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By" Width="75px"></asp:Label>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="PI_NO">Invoice No</asp:ListItem>
                                            <asp:ListItem Value="PI_DATE">Invoice Date</asp:ListItem>
                                            <%--<asp:ListItem Value="CUST_NAME">Customer</asp:ListItem>
                                            <asp:ListItem Value="CUST_CONTACT_PERSON">Contact Person</asp:ListItem>--%>
                                            <asp:ListItem Value="PI_AMOUNT">Amount</asp:ListItem>
                                            <%--<asp:ListItem Value="PI_STATUS">Status</asp:ListItem>--%>
                                            <asp:ListItem Value="SUP_NAME">Supplier Name</asp:ListItem>

                                        </asp:DropDownList>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:DropDownList ID="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                            EnableTheming="False" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged"
                                            Visible="False" Width="50px">
                                            <asp:ListItem Selected="True">=</asp:ListItem>
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem>&gt;</asp:ListItem>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <asp:ListItem>&gt;=</asp:ListItem>
                                            <asp:ListItem>R</asp:ListItem>
                                        </asp:DropDownList>
                                  </td>
                                    <td style="height: 25px;">
                                        <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label>
                                  </td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchValueFromDate" runat="server" type="date" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox>
                                        <%--<asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False" />
                                        <cc1:CalendarExtender  Format="dd/MM/yyyy"  ID="ceSearchFrom" runat="server" enabled="False" popupbuttonid="imgFromDate"
                                            targetcontrolid="txtSearchValueFromDate"></cc1:calendarextender>
                                        <cc1:maskededitextender id="MaskedEditSearchFromDate" runat="server" displaymoney="Left"
                                            enabled="False" mask="99/99/9999" masktype="Date" targetcontrolid="txtSearchValueFromDate"
                                            userdateformat="MonthDayYear"></cc1:maskededitextender>--%>
                                  </td>
                                    <td style="height: 25px">                                  
                                        <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label>
                                     </td>
                                     <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchValueToDate" runat="server" type="date" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox></td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><%--<asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False" />
                                        <cc1:CalendarExtender  Format="dd/MM/yyyy" ID="ceSearchValueToDate" runat="server" enabled="False" popupbuttonid="imgToDate"
                                            targetcontrolid="txtSearchText"></cc1:calendarextender>
                                        <cc1:maskededitextender id="MaskedEditSearchToDate" runat="server" displaymoney="Left"
                                            enabled="False" mask="99/99/9999" masktype="Date" targetcontrolid="txtSearchText"
                                            userdateformat="MonthDayYear"></cc1:maskededitextender>--%>
                                    </td>
                                    <td style="height: 25px">                                    
                                        <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" />
                                        </td>
                                </tr>
                                </table>
                            <asp:Label id="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label><asp:Label id="lblEmpIdHidden" runat="server" Visible="False"></asp:Label><asp:Label
                                    id="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                    Visible="False"></asp:Label><asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
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
            <td colspan="4" id="Td20">
                <asp:GridView id="gvInvoiceDetails" runat="server" AllowPaging="True" AllowSorting="True"
                    Width="100%"  AutoGenerateColumns="False" SelectedRowStyle-BackColor="#c0c0c0" DataSourceID="sdsInvoiceDetails" OnRowDataBound="gvInvoiceDetails_RowDataBound">
                    <columns>
<asp:BoundField DataField="PI_ID" HeaderText="InvoiceIDHidden"></asp:BoundField>
<asp:TemplateField HeaderText="Invoice No" SortExpression="PI_NO"><EditItemTemplate>
<asp:TextBox runat="server" Text='<%# Bind("PI_NO") %>' id="TextBox1"></asp:TextBox>
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnInvoiceNo" onclick="lbtnInvoiceNo_Click" runat="server" ForeColor="#0066ff" Text='<%# Bind("PI_NO") %>' CausesValidation="False" __designer:wfdid="w18"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HtmlEncode="False" SortExpression="PI_DATE" DataFormatString="{0:dd/MM/yyyy}" DataField="PI_DATE" HeaderText="Invoice Date"></asp:BoundField>
<asp:BoundField DataField="FPO_ID" HeaderText="Pur. Order No."></asp:BoundField>
<asp:BoundField DataField="SUP_NAME" SortExpression="SUP_NAME" HeaderText="Supplier"></asp:BoundField>
<asp:BoundField DataField="SUP_CONTACT_PERSON" SortExpression="SUP_CONTACT_PERSON" HeaderText="Contact Person"></asp:BoundField>
<asp:BoundField DataField="PI_GROSS_AMT" HeaderText="Amount" SortExpression="PI_GROSS_AMT"></asp:BoundField>
<asp:BoundField DataField="PI_STATUS" HeaderText="Status" SortExpression="PI_STATUS">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="EMPPREPAREDBY" HeaderText="Prepared By" SortExpression="EMPPREPAREDBY"></asp:BoundField>
<asp:BoundField DataField="EMPAPPROVEDBY" HeaderText="Approved By" SortExpression="EMPAPPROVEDBY"></asp:BoundField>
</columns>
                </asp:GridView><asp:SqlDataSource id="sdsInvoiceDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SCM_PURCHASEINVOICE_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="CPID" ControlID="lblCPID"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="EMPID" ControlID="lblEmpIdHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>
</selectparameters>
                    </asp:SqlDataSource>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table id="Table1" align="center">
                    <tr>
                        <td>
                            <asp:Button ID="btnNew" runat="server" Text="New" CausesValidation="False" OnClick="btnNew_Click" /></td>
                        <td>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CausesValidation="False" OnClick="btnEdit_Click" /></td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CausesValidation="False" OnClick="btnDelete_Click" /></td>
                    </tr>
                </table>
             
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td style="text-align: right;">
            </td>
            <td style="text-align: center;"><table border="0" cellpadding="0" cellspacing="0" id="tblPIDetails" runat="server" visible="false" width="100%">
                <tr>
            <td colspan="4" style="text-align: left" class="profilehead">
                General Details</td>
                </tr>
                <tr>
            <td style="text-align: right">
            </td>
            <td style="text-align: left">
            </td>
            <td style="text-align: right;">
            </td>
            <td style="text-align: left;">
            </td>
                </tr>
                <tr>
            <td style="text-align: right">
                <asp:Label id="lblSalesOrderNo" runat="server" Text="PO No" Width="54px"></asp:Label></td>
            <td style="text-align: left"><asp:DropDownList ID="ddlPONo" runat="server" Width="147px" AutoPostBack="True" OnSelectedIndexChanged="ddlPONo_SelectedIndexChanged">
            </asp:DropDownList></td>
            <td style="text-align: right;">
                <asp:Label id="lblSalesOrderDate" runat="server" Text="PO Date" Width="64px"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox id="txtPODate" runat="server"></asp:TextBox>&nbsp;<asp:Image id="imgCurrentDayTasksFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                    ></asp:Image>&nbsp;
                <cc1:CalendarExtender  Format="dd/MM/yyyy" ID="cePODate" runat="server" enabled="True" popupbuttonid="imgSalesOrderDate"
                                            targetcontrolid="txtPODate">
                    </cc1:CalendarExtender>
                <cc1:maskededitextender id="meePODate" runat="server" displaymoney="Left"
                                            enabled="True" mask="99/99/9999" masktype="Date" targetcontrolid="txtPODate"
                                            userdateformat="MonthDayYear">
                </cc1:MaskedEditExtender>
            </td>
                </tr>
                <tr>
            <td style="text-align: right">
                <asp:Label id="lblSupplierName" runat="server" Text="Supplier Name"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtSupplierName" runat="server" ReadOnly="True"></asp:TextBox><asp:DropDownList ID="ddlSupplierName" runat="server" Visible="False">
            </asp:DropDownList></td>
            <td style="text-align: right;">
                <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtContactPerson" runat="server" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr>
            <td style="text-align: right;">
                <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" ReadOnly="True"></asp:TextBox></td>
            <td style="text-align: right;">
                <asp:Label ID="lblPhone" runat="server" Text="Phone No."></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtPhoneNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr>
            <td style="text-align: right;">
                <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True"></asp:TextBox></td>
            <td style="text-align: right;">
                <asp:Label ID="lblMobileNo" runat="server" Text="Mobile No"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtMobileNo" runat="server" ReadOnly="True"></asp:TextBox></td>
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
            <td colspan="4" style="text-align: right">
            </td>
                </tr>
                <tr>
            <td colspan="4" style="text-align: center">
                <asp:GridView id="gvItDetails" runat="server" AutoGenerateColumns="False"
                    Width="100%" OnRowDataBound="gvItDetails_RowDataBound" OnRowEditing="gvItDetails_RowEditing"><columns>
<asp:CommandField ShowEditButton="True"></asp:CommandField>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
<asp:BoundField DataField="ItemType" HeaderText="Model No"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Model Name"></asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField HeaderText="Amount"></asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="DeliveryDate" HeaderText="Delivery Date"></asp:BoundField>
<asp:BoundField DataField="Specifications" HeaderText="Specifications">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Remarks" HeaderText="Remarks">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ItemTypeId" HeaderText="Item Type Id"></asp:BoundField>
</columns>
                    <emptydatatemplate>
<SPAN style="COLOR: #ff0000">No Data Exists</SPAN>&nbsp; 
</emptydatatemplate>
                </asp:GridView></td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center">
                        </td>
                </tr>
                <tr>
            <td class="profilehead" colspan="4" style="text-align: left">
                invoice Details</td>
                </tr>
                <tr>
            <td style="text-align: right">
            </td>
            <td style="text-align: left">
            </td>
            <td style="text-align: right;">
            </td>
            <td style="text-align: left;">
            </td>
                </tr>
                <tr>
            <td style="text-align: right">
                <asp:Label ID="Label3" runat="server" Text="Pur. Voc. No."></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtInvoiceNo" runat="server" ReadOnly="True"></asp:TextBox></td>
            <td style="text-align: right;">
                <asp:Label ID="Label6" runat="server" Text="Pur. Voc.  Date"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox id="txtInvoiceDate" runat="server"></asp:TextBox>&nbsp;<asp:Image id="imgInvoiceDate" runat="server" ImageUrl="~/Images/Calendar.png"
                    ></asp:Image>&nbsp;
                <cc1:CalendarExtender  Format="dd/MM/yyyy" ID="ceInvoiceDate" runat="server" enabled="True" popupbuttonid="imgInvoiceDate"
                                            targetcontrolid="txtInvoiceDate">
                    </cc1:CalendarExtender>
                <cc1:maskededitextender id="meeInvoiceDate" runat="server" displaymoney="Left"
                                            enabled="True" mask="99/99/9999" masktype="Date" targetcontrolid="txtInvoiceDate"
                                            userdateformat="MonthDayYear">
                </cc1:MaskedEditExtender>
            </td>
                </tr>
                <tr>
                    <td style="text-align: right"><asp:Label ID="Label8" runat="server" Text="Supp. Inv. No."></asp:Label></td>
                    <td style="text-align: left"><asp:TextBox ID="txtCustInvNo" runat="server">
                    </asp:TextBox></td>
                    <td style="text-align: right;"><asp:Label ID="Label29" runat="server" Text="Supp. Inv. Date"></asp:Label></td>
                    <td style="text-align: left"><asp:TextBox id="txtCustInvDate" runat="server">
                    </asp:TextBox>&nbsp;<asp:Image id="imgCustInvDate" runat="server" ImageUrl="~/Images/Calendar.png"
                    ></asp:Image><cc1:CalendarExtender  Format="dd/MM/yyyy" ID="CalendarExtender1" runat="server" enabled="True" popupbuttonid="imgCustInvDate"
                                            targetcontrolid="txtCustInvDate">
                    </cc1:CalendarExtender>
                        <cc1:maskededitextender id="Maskededitextender1" runat="server" displaymoney="Left"
                                            enabled="True" mask="99/99/9999" masktype="Date" targetcontrolid="txtCustInvDate"
                                            userdateformat="MonthDayYear">
                        </cc1:MaskedEditExtender>
                    </td>
                </tr>
                <tr>
            <td style="height: 19px; text-align: right">
                <asp:Label ID="Label5" runat="server" Text="Invoice Type"></asp:Label></td>
            <td style="height: 19px; text-align: left"><asp:DropDownList ID="ddlInvoiceType" runat="server">
                <asp:ListItem Value="0">--</asp:ListItem>
                <asp:ListItem>Invoice</asp:ListItem>
                <asp:ListItem>Invoice Cum DC</asp:ListItem>
                <asp:ListItem>Tax Invoice</asp:ListItem>
                <asp:ListItem>Bill</asp:ListItem>
                <asp:ListItem>Commercial Invoice</asp:ListItem>
            </asp:DropDownList></td>
            <td style="height: 19px; text-align: right;">
                <asp:Label ID="Label1" runat="server" Text="Despatch Mode"></asp:Label></td>
            <td style="height: 19px; text-align: left;"><asp:DropDownList ID="ddlDespatchMode" runat="server">
            </asp:DropDownList>
                <asp:Label id="Label36" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="ddlDespatchMode" ErrorMessage="Please Select the Despatch Mode" InitialValue="0">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="Label2" runat="server" Text="Terms of Delivery"></asp:Label></td>
                    <td colspan="3" style="text-align: left">
                        <asp:TextBox ID="txtTermsOfDelivery" runat="server" CssClass="multilinetext" EnableTheming="False" TextMode="MultiLine" Width="81%"></asp:TextBox></td>
                </tr>
                <tr>
            <td colspan="4" style="text-align: left" class="profilehead">
                Items Details</td>
                </tr>
                <tr>
                    <td style="height: 20px; text-align: right">
                    </td>
                    <td style="height: 20px; text-align: left">
                    </td>
                    <td style="height: 20px; text-align: right;">
                        <asp:Label id="Label35" runat="server" Text="Search By Brand"></asp:Label></td>
                    <td style="height: 20px; text-align: left">
                        <asp:DropDownList id="ddlBrandselect" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrandselect_SelectedIndexChanged">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label id="lblItemCode" runat="server" Text="Model No"></asp:Label>
                        </td>
                        <td style="height: 24px; text-align: left;">
                            <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label id="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="ddlItemType" ErrorMessage="Please Select the Item Type" InitialValue="0"
                                ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
                        <td style="height: 24px; text-align: right;">
                            <asp:Label id="Label13" runat="server" Text="Model Name"></asp:Label></td>
                        <td style="height: 24px; text-align: left;">
                            <asp:TextBox id="txtModelName" runat="server">
                            </asp:TextBox></td>
                </tr>
                <tr>
                    <td style="height: 24px; text-align: right">
                        <asp:Label id="Label30" runat="server" Text="Item Category"></asp:Label></td>
                    <td style="height: 24px; text-align: left">
                        <asp:TextBox id="txtItemCategory" runat="server">
                            </asp:TextBox></td>
                    <td style="height: 24px; text-align: right;">
                        <asp:Label id="Label31" runat="server" Text="ItemSubCategory"></asp:Label></td>
                    <td style="height: 24px; text-align: left">
                        <asp:TextBox id="txtItemSubCategory" runat="server">
                            </asp:TextBox></td>
                </tr>
                <tr>
                        <td style="text-align: right; height: 21px;">
                           <asp:Label id="lblUOM" runat="server" Text="UOM" Width="50px"></asp:Label></td>
                        <td style="height: 21px; text-align: left;">
                            <asp:TextBox ID="txtUOM" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right; height: 21px;">
                            <asp:Label id="Label33" runat="server" Text="Color"></asp:Label></td>
                        <td style="height: 21px; text-align: left;">
                            <asp:TextBox id="txtColor" runat="server">
                            </asp:TextBox></td>
                </tr>
                <tr>
                    <td style="height: 21px; text-align: right">
                        <asp:Label id="Label34" runat="server" Text="Brand"></asp:Label></td>
                    <td style="height: 21px; text-align: left">
                        <asp:TextBox id="txtBrand" runat="server">
                            </asp:TextBox></td>
                    <td style="height: 21px; text-align: right">
                        <asp:Label id="Label32" runat="server" Text="Ordered Quantity" Visible="False"></asp:Label></td>
                    <td style="height: 21px; text-align: left">
                        <asp:TextBox id="txtOrderedQuantity" runat="server" Visible="False"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="height: 21px; text-align: right">
                        <asp:Label ID="Label23" runat="server" Text="Item Specification"></asp:Label></td>
                    <td colspan="3" style="height: 21px; text-align: left">
                        <asp:TextBox ID="txtItemSpec" runat="server" CssClass="multilinetext" EnableTheming="False"
                            ReadOnly="True" TextMode="MultiLine" Width="81%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                    </td>
                    <td style="text-align: left">
                        <asp:RadioButton id="rbVAT" runat="server" Checked="True" GroupName="vatcst" Text="VAT">
                        </asp:RadioButton><asp:RadioButton id="rbCST" runat="server" GroupName="vatcst" Text="C.S. Tax"></asp:RadioButton></td>
                    <td style="text-align: right;">
                        <asp:Label id="Label24" runat="server" Text="Excise" Visible="False"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox id="txtExcise" runat="server" Visible="False">0</asp:TextBox><asp:Label id="Label27" runat="server" EnableTheming="False" Font-Bold="False"
                            Font-Names="Verdana" Font-Size="Smaller" Text="%" Visible="False"></asp:Label>
                        <br />
                        <cc1:FilteredTextBoxExtender ID="ftxteExcise" runat="server" TargetControlID="txtExcise"
                            ValidChars=".0123456789">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                            <asp:Label id="lblCSTax" runat="server" Text="CST"></asp:Label><asp:Label id="lblVAT" runat="server" Text="VAT"></asp:Label></td>
                    <td style="text-align: left">
                            <asp:TextBox id="txtVAT" runat="server">0</asp:TextBox><asp:TextBox id="txtCST" runat="server">0</asp:TextBox><asp:Label id="Label25" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" Text="%"></asp:Label><asp:Label id="Label17"
                                    runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana" Font-Size="Smaller"
                                    ForeColor="Red" Text="*"></asp:Label>&nbsp;
                        <cc1:FilteredTextBoxExtender ID="ftxteVat" runat="server" TargetControlID="txtVAT"
                                ValidChars=".0123456789">
                        </cc1:FilteredTextBoxExtender>
                    <cc1:FilteredTextBoxExtender ID="ftxteCST" runat="server" TargetControlID="txtCST"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                    </td>
                    <td style="text-align: right;">
                            <asp:Label id="lblRate" runat="server" Text="Rate"></asp:Label></td>
                    <td style="text-align: left">
                            <asp:TextBox id="txtRate" runat="server">
                            </asp:TextBox><asp:Label id="Label21" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                ControlToValidate="txtRate" ErrorMessage="Please Enter the Rate" ValidationGroup="ip">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender ID="ftxteRate" runat="server" TargetControlID="txtRate"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblQuantity" runat="server" Text="Receiving Quantity"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox id="txtQuantity" runat="server" Width="139px">
                            </asp:TextBox><asp:Label id="Label19" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                ControlToValidate="txtQuantity" ErrorMessage="Please Enter the Quantity" ValidationGroup="ip">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender ID="ftxteQuantity" runat="server" FilterType="Numbers"
                                TargetControlID="txtQuantity" ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label id="lblAmount" runat="server" Text="Amount"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtAmount" runat="server" ReadOnly="True"></asp:TextBox>
                            &nbsp;
                        </td>
                </tr>
                <tr>
                        <td style="height: 19px; text-align: right;">
                            <asp:Label id="Label14" runat="server" Text="ItemImage"></asp:Label></td>
                        <td style="height: 19px; text-align: left;">
                            <asp:Image id="Image1" runat="server" Height="85px" ImageUrl="~/Images/noimage400x300.gif"
                                Width="140px">
                            </asp:Image></td>
                        <td style="height: 19px;">
                        </td>
                        <td style="height: 19px">
                        </td>
                </tr>
                <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" OnClick="btnAdd_Click" Text="Add" /><asp:Button
                                    ID="btnItemRefresh" runat="server" BackColor="Transparent" BorderStyle="None"
                                    CssClass="loginbutton" EnableTheming="False" OnClick="btnItemRefresh_Click" Text="Refresh" /></td>
                </tr>
                <tr>
                        <td style="height: 16px">
                        </td>
                        <td style="height: 16px">
                        </td>
                        <td style="height: 16px;">
                        </td>
                        <td style="height: 16px">
                        </td>
                </tr>
                <tr>
            <td colspan="4" style="text-align: right">
            </td>
                </tr>
                <tr>
            <td style="text-align: center;" colspan="4">
                <asp:GridView id="gvItemDetails" runat="server" AutoGenerateColumns="False"
                    OnRowDeleting="gvItemDetails_RowDeleting" Width="100%" OnRowDataBound="gvItemDetails_RowDataBound" OnRowEditing="gvItemDetails_RowEditing"><columns>
<asp:CommandField ShowEditButton="True"></asp:CommandField>
<asp:CommandField ShowDeleteButton="True"></asp:CommandField>
<asp:BoundField DataField="ItemCode" HeaderText="ItemCode"></asp:BoundField>
<asp:BoundField DataField="ItemType" HeaderText="Model No"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
<asp:BoundField DataField="VAT" HeaderText="VAT"></asp:BoundField>
<asp:BoundField DataField="cst" HeaderText="CST"></asp:BoundField>
<asp:BoundField DataField="Excise" HeaderText="Excise"></asp:BoundField>
<asp:BoundField HeaderText="Amount"></asp:BoundField>
<asp:BoundField DataField="ItemTypeId" HeaderText="ItemTypeIdHidden"></asp:BoundField>
</columns>
                    <emptydatatemplate>
<SPAN style="COLOR: #ff0033">No Data Exits</SPAN>
</emptydatatemplate>
                </asp:GridView>
            </td>
                </tr>
                <tr>
                    <td class="profilehead" colspan="4" style="text-align: left">
                        invoice Details</td>
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
                    <td style="text-align: right">
                        <asp:Label id="Label37" runat="server" Text="Pay By LC"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox id="txtpaybylc" runat="server">
                        </asp:TextBox></td>
                    <td style="text-align: right">
                    </td>
                    <td style="text-align: left">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label id="Label38" runat="server" Text="LC Date"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox id="txtLCdate" runat="server">
                        </asp:TextBox>
                        <asp:Image id="Image2" runat="server" ImageUrl="~/Images/Calendar.png">
                        </asp:Image>
                        <cc1:CalendarExtender  Format="dd/MM/yyyy" ID="CalendarExtender2" runat="server" enabled="True" popupbuttonid="Image2"
                                            targetcontrolid="txtLCdate">
                        </cc1:CalendarExtender>
                    </td>
                    <td style="text-align: right">
                        <asp:Label id="Label39" runat="server" Text="LC Exp Date"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox id="txtlcexpdate" runat="server">
                        </asp:TextBox>
                        <asp:Image id="Image3" runat="server" ImageUrl="~/Images/Calendar.png">
                        </asp:Image>
                        <cc1:CalendarExtender  Format="dd/MM/yyyy" ID="CalendarExtender3" runat="server" enabled="True" popupbuttonid="Image3"
                                            targetcontrolid="txtlcexpdate">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label id="Label40" runat="server" Text="Pay By TT"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox id="txtPaybytt" runat="server">
                        </asp:TextBox></td>
                    <td style="text-align: right">
                        <asp:Label id="Label41" runat="server" Text="TT Date"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox id="txtttdate" runat="server">
                        </asp:TextBox>
                        <asp:Image id="Image4" runat="server" ImageUrl="~/Images/Calendar.png">
                        </asp:Image>
                        <cc1:CalendarExtender  Format="dd/MM/yyyy" ID="CalendarExtender4" runat="server" enabled="True" popupbuttonid="Image4"
                                            targetcontrolid="txtttdate">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label id="Label18" runat="server" Text="Cheque No"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox id="txtChequeNo" runat="server"></asp:TextBox></td>
                    <td style="text-align: right;">
                        <asp:Label id="Label26" runat="server" Text="Cheque Date"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox id="txtDate" runat="server">
                        </asp:TextBox>&nbsp;<asp:Image id="imgDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>
                        &nbsp;&nbsp;
                        <cc1:CalendarExtender ID="ceeDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                            PopupButtonID="imgDate" TargetControlID="txtDate">
                        </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="meeChequeDate" runat="server" DisplayMoney="Left" Enabled="True"
                            Mask="99/99/9999" MaskType="Date" TargetControlID="txtDate" UserDateFormat="MonthDayYear">
                        </cc1:MaskedEditExtender>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label id="Label28" runat="server" Text="Bank"></asp:Label></td>
                    <td colspan="3" style="text-align: left">
                        <asp:TextBox ID="txtBank" runat="server" CssClass="multilinetext" EnableTheming="False" TextMode="MultiLine" Width="81%">
                        </asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                    </td>
                    <td colspan="3" style="text-align: left">
                    </td>
                </tr>
                <tr>
            <td class="profilehead" colspan="4">
                other charges</td>
                </tr>
                <tr>
            <td style="height: 19px; text-align: right">
            </td>
            <td style="height: 19px; text-align: left">
            </td>
            <td style="height: 19px; text-align: right;">
            </td>
            <td style="height: 19px; text-align: left;">
            </td>
                </tr>
                <tr>
            <td style="text-align: right">
                <asp:Label ID="Label11" runat="server" Text="Miscelleneous"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtMiscelleneous" runat="server"></asp:TextBox><cc1:FilteredTextBoxExtender ID="ftxteMiscelleneous" runat="server" TargetControlID="txtMiscelleneous"
                                ValidChars=".0123456789">
                </cc1:FilteredTextBoxExtender>
            </td>
            <td style="text-align: right;">
                <asp:Label ID="Label9" runat="server" Text="Discount"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtDiscount" runat="server"></asp:TextBox><asp:Label id="Label22" runat="server" Text="%"></asp:Label><cc1:FilteredTextBoxExtender ID="ftxteDiscount" runat="server" TargetControlID="txtDiscount"
                                ValidChars=".0123456789">
                </cc1:FilteredTextBoxExtender>
            </td>
                </tr>
                <tr>
            <td style="text-align: right">
                <asp:Label ID="Label10" runat="server" Text="Gross Amount"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtGrossAmount" runat="server" ReadOnly="True"></asp:TextBox><asp:HiddenField ID="txtGrossTotalAmtHidden" runat="server" />
            </td>
            <td style="text-align: right;">
                </td>
            <td style="text-align: left;">
            </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="Label4" runat="server" Text="Remarks"></asp:Label></td>
                    <td colspan="3" style="text-align: left">
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="multilinetext" EnableTheming="False" TextMode="MultiLine" Width="81%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                <asp:Label ID="Label12" runat="server" Text="Packing Charges  " Visible="False"></asp:Label></td>
                    <td colspan="1" style="text-align: left">
                <asp:TextBox ID="txtPackingCharges" runat="server" Visible="False"></asp:TextBox><asp:Label ID="Label16" runat="server" Text="Insurance" Visible="False"></asp:Label><asp:TextBox ID="txtInsurance" runat="server" Visible="False"></asp:TextBox><cc1:FilteredTextBoxExtender ID="ftxtePackingCharges" runat="server" TargetControlID="txtPackingCharges"
                                ValidChars=".0123456789">
                </cc1:FilteredTextBoxExtender>
                    </td>
                    <td colspan="3" style="text-align: left">
                        <asp:Label ID="Label15" runat="server" Text="Transportation Charges" Width="148px" Visible="False"></asp:Label><asp:TextBox ID="txtTranportationCharges" runat="server" Visible="False"></asp:TextBox><cc1:FilteredTextBoxExtender ID="ftxteTrasncharges" runat="server" TargetControlID="txtTranportationCharges"
                                ValidChars=".0123456789">
                </cc1:FilteredTextBoxExtender>
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
            <td style="height: 19px; text-align: right;">
            </td>
            <td style="height: 19px; text-align: left;">
            </td>
                </tr>
                <tr>
            <td style="text-align: right">
                <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
            <td style="text-align: left"><asp:DropDownList id="ddlPreparedBy" runat="server" Enabled="False">
            </asp:DropDownList></td>
            <td style="text-align: right;">
                <asp:Label id="lblApprovedBy" runat="server" Text="Approved By"></asp:Label></td>
            <td style="text-align: left;">
                <asp:DropDownList id="ddlApprovedBy" runat="server" Enabled="False">
                </asp:DropDownList></td>
                </tr>
                <tr>
            <td style="text-align: right">
                </td>
            <td style="text-align: left">
                </td>
            <td style="text-align: right;">
            </td>
            <td style="text-align: left;">
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
            <td colspan="4" style="height: 47px">
                <table id="tblButtons" align="center">
                    <tr>
                        <td>
                            <asp:Button id="btnSave" runat="server" Text="Save" CausesValidation="False" OnClick="btnSave_Click" /></td>
                        <td>
                            <asp:Button id="btnApprove" runat="server" Text="Approve" CausesValidation="False" OnClick="btnApprove_Click" /></td>
                        <td><asp:Button id="btnRefresh" runat="server" Text="Refresh" CausesValidation="False" OnClick="btnRefresh_Click" /></td>
                        <td>
                            <asp:Button id="btnClose" runat="server" Text="Close" CausesValidation="False" OnClick="btnClose_Click" /></td>
                        <td>
                            <asp:Button id="btnPrint" runat="server" Text="Print" CausesValidation="False" OnClick="btnPrint_Click" /></td>
                    </tr>
                </table>
            </td>
                </tr>
            </table>
                <asp:ValidationSummary id="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False">
                </asp:ValidationSummary>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td style="text-align: right">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td style="text-align: right">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td style="text-align: right">
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>


 
