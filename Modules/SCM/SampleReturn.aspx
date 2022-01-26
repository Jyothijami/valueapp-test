<%@ Page Language="C#" MasterPageFile="~/MPs/FinanceMP1.master" AutoEventWireup="true" 
    CodeFile="SampleReturn.aspx.cs" Inherits="Modules_SCM_SampleReturn" Title="|| Value Appp : Finance : Sample Return ||" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    
      <%--  <script>
            $(function () {
                
                $("[name$='txtSalesReturndate'],[name$='txtChallanDate'],[name$='txtSalesInvoiceDate']").datepicker();
            });
    </script>--%>
 <script type="text/javascript" language="javascript">
function amtcalc()
{
    var req_qty,rate;
    req_qty=document.getElementById('<%=txtQuantity.ClientID %>').value;
    rate=document.getElementById('<%=txtRate.ClientID %>').value;
    
    
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

    function Include()
  {
    var Inc,Gross,Tot;
     Gross = parseFloat(document.getElementById('<%=txtAfteronemonthHidden.ClientID %>').value); 
     
    inc = document.getElementById('<%=txtIncludeVat.ClientID %>').value;
        
    if(Inc == "" || Inc == "0" || isNaN(Inc))
    {
    Inc = "0";
    }
    if(Gross == "" || Gross == "0" || isNaN(Gross))
    {
    Gross = "0";
    }
     Tot = (Number(Gross))*((Number(inc))/100)
    var Tot = Tot.toFixed(2);
    document.getElementById('<%=txtTotalAmt.ClientID %>').value = (Number(Gross) + Number(Tot));
   
  } 


   function grosscalc()
    {
        var vat,cst,disc,grossamt,misc,transchargs,TOTAL;
        cst=document.getElementById('<%=txtCST.ClientID %>').value;
        vat=document.getElementById('<%=txtVAT.ClientID %>').value;
        misc=parseFloat(document.getElementById('<%=txtMiscelleneous.ClientID %>').value);
        disc=parseFloat(document.getElementById('<%=txtDiscount.ClientID %>').value);
        grossamt=parseFloat(document.getElementById('<%=txtGrossTotalAmtHidden.ClientID %>').value);
        if(cst=="" || cst=="0" || isNaN(cst)){cst="0";}
        if(vat=="" || vat=="0" || isNaN(vat)){vat="0";}
        if(grossamt=="" || grossamt=="0" || isNaN(grossamt)){grossamt="0";}
        if(misc=="" || misc=="0" || isNaN(misc)){misc="0";}
        if(disc=="" || disc=="0" || isNaN(disc)){disc="0";}
        TOTAL=parseFloat(grossamt);
        TOTAL=TOTAL+((vat*TOTAL)/100);
        TOTAL=TOTAL+((cst*TOTAL)/100);
        
        TOTAL=TOTAL+parseFloat(misc);        
        TOTAL=TOTAL-((disc*TOTAL)/100);
        document.getElementById('<%=txtGrossAmount.ClientID %>').value= parseInt(TOTAL);
    }
    
    function rbVATCSTEnableDisable()
    {
//        if(document.getElementById('<%=rbVAT.ClientID %>').checked==true)
//        {
//            document.getElementById('<%=txtVAT.ClientID %>').style.display=document.getElementById('<%=lblVAT.ClientID %>').style.display ="";
//            document.getElementById('<%=txtCST.ClientID %>').style.display=document.getElementById('<%=lblCSTax.ClientID %>').style.display ="none";
//            document.getElementById('<%=txtVAT.ClientID %>').focus();
//        }  
//        if(document.getElementById('<%=rbCST.ClientID %>').checked==true)
//        {
//            document.getElementById('<%=txtVAT.ClientID %>').style.display=document.getElementById('<%=lblVAT.ClientID %>').style.display ="none";
//            document.getElementById('<%=txtCST.ClientID %>').style.display = document.getElementById('<%=lblCSTax.ClientID %>').style.display ="";
//            document.getElementById('<%=txtCST.ClientID %>').focus();       
//        } 
    document.getElementById('<%=txtVAT.ClientID %>').value="";
    document.getElementById('<%=txtCST.ClientID %>').value="";
       grosscalc();
    }
    </script>

    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td style="text-align:left" colspan="3">
                Sample &amp; Cash
                Sales Return</td>
             <td align="right">
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
    <table style="width: 100%">
        <tr>
            <td colspan="3">
                <table id="tblSIDetails" runat="server" border="0" cellpadding="0" cellspacing="0"
                    visible="true" width="100%">
                    <tr>
                        <td colspan="4" style="text-align: center; ">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tblMain">
                                <tr>
                                    <td class="searchhead" colspan="3">
                                        <table id="TABLE2" border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="text-align: left">
                                                    Sales Return</td>
                                                <td>
                                                </td>
                                                <td style="text-align: right">
                                                    <table border="0" cellpadding="0" cellspacing="0" align="right">
                                                        <tr>
                                                            <td style="height: 25px">
                                                                <asp:Label id="Label20" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                                                    Text="Search By"></asp:Label></td>
                                                            <td style="height: 25px">
                                                                <asp:DropDownList id="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                                                    OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">--</asp:ListItem>
                                                                    <asp:ListItem Value="CUST_NAME">Customer</asp:ListItem>
                                                                    <asp:ListItem Value="ITEM_MODEL_NO">Model No</asp:ListItem>
                                                                </asp:DropDownList></td>
                                                            <td style="height: 25px">
                                                                <asp:DropDownList id="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                                                    EnableTheming="False" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged"
                                                                    Visible="False" Width="50px">
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
                                        </asp:TextBox><asp:Image id="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                                                    Visible="False"></asp:Image>
                                                                <cc1:CalendarExtender ID="ceSearchFrom" runat="server" Enabled="False" Format="dd/MM/yyyy"
                                                                    PopupButtonID="imgFromDate" TargetControlID="txtSearchValueFromDate">
                                                                </cc1:CalendarExtender>
                                                                <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server" DisplayMoney="Left"
                                                                    Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate"
                                                                    UserDateFormat="MonthDayYear">
                                                                </cc1:MaskedEditExtender>
                                                            </td>
                                                            <td style="height: 25px">
                                                                <asp:Label id="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                                            <td style="height: 25px">
                                                                <asp:TextBox id="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><asp:Image id="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                                                    Visible="False"></asp:Image>
                                                                <cc1:CalendarExtender ID="ceSearchValueToDate" runat="server" Enabled="False" Format="dd/MM/yyyy"
                                                                    PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                                                                </cc1:CalendarExtender>
                                                                <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                                                    Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                                                    UserDateFormat="MonthDayYear">
                                                                </cc1:MaskedEditExtender>
                                                            </td>
                                                            <td style="height: 25px">
                                                                <asp:Button id="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                                                    CssClass="gobutton" EnableTheming="False" onclick="btnSearchGo_Click" Text="Go" /></td>
                                                        </tr>
                                                        </table>
                                                    <asp:Label id="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                                        Visible="False"></asp:Label><asp:Label id="lblEmpIdHidden" runat="server" Visible="False"></asp:Label><asp:Label
                                                            id="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                                                    <asp:Label id="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                                                    <asp:Label id="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                                                    <asp:Label id="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:GridView id="gvSalesReturnDetails" SelectedRowStyle-BackColor="#c0c0c0" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            OnRowDataBound="gvSalesReturnDetails_RowDataBound" Width="100%" DataSourceID="SqlDataSource1">
                                            <columns>
<asp:BoundField DataField="SR_ID" SortExpression="SR_ID" HeaderText="SalesReturnIdHidden"></asp:BoundField>
<asp:TemplateField HeaderText="DC No" SortExpression="DC_NO">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnDCNo" onclick="lbtnDCNo_Click" ForeColor="#0066ff" runat="server" Text='<%# Bind("DC_NO") %>' CausesValidation="False" __designer:wfdid="w3"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Sales Return No" SortExpression="SI_NO"><EditItemTemplate>
<asp:TextBox id="TextBox1" runat="server" Text='<%# Bind("SI_NO") %>' __designer:wfdid="w5"></asp:TextBox> 
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnSalesReturnNo" onclick="lbtnSalesReturnNo_Click" ForeColor="#0066ff" runat="server" Text='<%# Bind("SR_NO") %>' CausesValidation="False" __designer:wfdid="w4"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HtmlEncode="False" SortExpression="SR_DATE" DataFormatString="{0:dd/MM/yyyy}" DataField="SR_DATE" HeaderText="Return Date"></asp:BoundField>
<asp:BoundField DataField="DC_ID" SortExpression="DC_ID" HeaderText="DCIdHidden"></asp:BoundField>
<asp:BoundField DataField="CUST_NAME" SortExpression="CUST_NAME" HeaderText="Customer Name">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SI_GROSS_AMT" SortExpression="SI_GROSS_AMT" HeaderText="Amount"></asp:BoundField>
<asp:BoundField DataField="EMP_FIRST_NAME" SortExpression="EMP_FIRST_NAME" HeaderText="Approved By"></asp:BoundField>
<asp:BoundField DataField="COMPANY" SortExpression="COMPANY" HeaderText="COMPANY"></asp:BoundField>

</columns>
                                            <emptydatatemplate>
<SPAN style="COLOR: #ff0000">No Data Exist</SPAN>
</emptydatatemplate>
                                        </asp:GridView><asp:SqlDataSource id="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                            SelectCommand="SP_INVENTORY_SampleSALESRETURN_SEARCH_SELECT" SelectCommandType="StoredProcedure"><selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
</selectparameters></asp:SqlDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align: center">
                                        <table id="Table1">
                                            <tr>
                                                <td>
                                                    <asp:Button id="btnNew" runat="server" CausesValidation="False" onclick="btnNew_Click"
                                                        Text="New" /></td>
                                                <td>
                                                    <asp:Button id="btnEdit" runat="server" CausesValidation="False" onclick="btnEdit_Click"
                                                        Text="Edit" /></td>
                                                <td style="width: 58px">
                                                    <asp:Button id="btnDelete" runat="server" CausesValidation="False" onclick="btnDelete_Click"
                                                        Text="Delete" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td colspan="3" style="height: 7px">
                                        <table id="tblsr" runat="server" border="0" cellpadding="0" cellspacing="0"
                    visible="true" width="100%">
                                            <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Sales Return Details</td>
                                            </tr>
                                            <tr>
                        <td style="text-align: right">
                            </td>
                        <td style="text-align: left">
                            </td>
                        <td style="text-align: right">
                            &nbsp;</td>
                        <td style="text-align: left">
                            </td>
                                            </tr>
                                            <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label5" runat="server" Text="Sales Return No"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtSalesReturnNo" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label id="Label6" runat="server" Text="Sales Return Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtSalesReturndate" runat="server" type="date">
                            </asp:TextBox></td>
                                            </tr>
                                            <tr>
                        <td style="text-align: left" class="profilehead" colspan="4">
                            General Details</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                            <asp:Label id="Label1" runat="server" Text="Delivery Challan No" Width="127px"></asp:Label></td>
                                                <td style="text-align: left">
                            <asp:DropDownList id="ddlDeviveryNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDeviveryNo_SelectedIndexChanged">
                            </asp:DropDownList><asp:Label id="Label26" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                    id="RequiredFieldValidator9" runat="server" ControlToValidate="ddlDeviveryNo"
                                    ErrorMessage="Please Select the Delivery Challan No" InitialValue="0">*</asp:RequiredFieldValidator></td>
                                                <td style="font-size: 12pt; color: #000000; font-family: Times New Roman; text-align: right">
                            <asp:Label id="Label2" runat="server" Text="Delivery Challan Date" Width="146px"></asp:Label></td>
                                                <td style="font-size: 12pt; font-family: Times New Roman; text-align: left">
                            <asp:TextBox id="txtChallanDate" runat="server" type="date" ReadOnly="True">
                            </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 19px; text-align: right">
                                                    <asp:Label id="Label8" runat="server" Text="Sales Invoice No"></asp:Label></td>
                                                <td style="height: 19px; text-align: left">
                                                    <asp:DropDownList id="ddlSalesInvoiceNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSalesInvoiceNo_SelectedIndexChanged">
                                                    </asp:DropDownList></td>
                                                <td style="font-size: 12pt; color: #000000; font-family: Times New Roman; height: 19px;
                                                    text-align: right">
                                                    <asp:Label id="Label12" runat="server" Text="Sales Invoice Date"></asp:Label></td>
                                                <td style="font-size: 12pt; font-family: Times New Roman; height: 19px; text-align: left">
                                                    <asp:TextBox id="txtSalesInvoiceDate" runat="server" type="date">
                                                    </asp:TextBox></td>
                                            </tr>
                                            <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblCustomer" runat="server" Text="Customer Name"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox
                                    id="txtCustomerName" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="font-size: 12pt; color: #000000; font-family: Times New Roman; text-align: right">
                            <asp:Label id="lblRegion" runat="server" Text="Region"></asp:Label></td>
                        <td style="font-size: 12pt; font-family: Times New Roman; text-align: left">
                            <asp:TextBox id="txtRegion" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                                            </tr>
                                            <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblAddress" runat="server" Text="Address"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtAddress" runat="server" ReadOnly="True" TextMode="MultiLine">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label id="lblPhone" runat="server" Text="Phone"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtPhone" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                                            </tr>
                                            <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblEmail" runat="server" Text="Email"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtEmail" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label id="lblMobile" runat="server" Text="Mobile"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox id="txtMobile" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
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
                        <td class="profilehead" colspan="4" style="text-align: left; height: 20px;">
                            Delivery Challan Items</td>
                                            </tr>
                                            <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:GridView id="gvDeliveryChallanItems" runat="server" AutoGenerateColumns="False"
                                Width="100%">
                                <columns>
<asp:BoundField DataField="DC No" HeaderText="DC No"></asp:BoundField>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
<asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
</columns>
                                <emptydatatemplate>
<SPAN style="COLOR: #ff0033">No Data to Dispaly</SPAN>
</emptydatatemplate>
                            </asp:GridView></td>
                                            </tr>
                                            <tr>
                                                <td class="profilehead" colspan="4" style="text-align: left">
                                                    Sales Invoice</td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="text-align: center">
                                                    <asp:GridView id="gvSalesInvoice" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSalesInvoice_RowDataBound" Width="100%">
                                                        <columns>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
<asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
<asp:BoundField DataField="Vat" HeaderText="Vat"></asp:BoundField>
<asp:BoundField DataField="CST" HeaderText="CST"></asp:BoundField>
<asp:BoundField DataField="Excise" HeaderText="Excise"></asp:BoundField>
<asp:BoundField HeaderText="Amount"></asp:BoundField>
<asp:BoundField DataField="SPPrice" HeaderText="Special Price"></asp:BoundField>
</columns>
                                                        <emptydatatemplate>
<SPAN style="COLOR: #ff0033">No Data to Dispaly</SPAN>
</emptydatatemplate>
                                                    </asp:GridView></td>
                                            </tr>
                                            <tr>
                                                <td class="profilehead" colspan="4">
                                                    Sales Return</td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="text-align: center">
                            <asp:GridView id="gvItmDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvItmDetails_RowDataBound"
                                OnRowDeleting="gvItmDetails_RowDeleting" OnRowEditing="gvItmDetails_RowEditing" Width="100%">
                                <columns>
<asp:CommandField ShowEditButton="True"></asp:CommandField>
<asp:CommandField ShowDeleteButton="True"></asp:CommandField>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
<asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
<asp:BoundField DataField="Vat" HeaderText="Vat"></asp:BoundField>
<asp:BoundField DataField="CST" HeaderText="CST"></asp:BoundField>
<asp:BoundField DataField="Excise" HeaderText="Excise"></asp:BoundField>
<asp:BoundField HeaderText="Amount"></asp:BoundField>
</columns>
                                <emptydatatemplate>
<SPAN style="COLOR: #ff0033">No Data to Dispaly</SPAN>
</emptydatatemplate>
                            </asp:GridView></td>
                                            </tr>
                                            <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Items Details</td>
                                            </tr>
                                            <tr>
                        <td colspan="4" style="text-align: right">
                        </td>
                                            </tr>
                                            <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label4" runat="server" Text="Model No :"></asp:Label></td>
                        <td style="text-align: left">
                            &nbsp;<asp:DropDownList id="ddlModelNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModelNo_SelectedIndexChanged"></asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label id="Label7" runat="server" Text="Item Name" Width="76px"></asp:Label></td>
                        <td style="text-align: left">
                            &nbsp;<asp:TextBox id="txtItemname" runat="server"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label27" runat="server" Text="UOM"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtItemUOM" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label id="Label59" runat="server" Text="Color :"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlColor" runat="server">
                            </asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label id="Label47" runat="server" Text="Company"></asp:Label></td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList id="ddlCompany" runat="server" AutoPostBack="True" meta:resourcekey="ddlCompanyResource1"
                                                        OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged1">
                                                    </asp:DropDownList></td>
                                                <td style="text-align: right">
                                                    <asp:Label id="Label45" runat="server" Text="Godown"></asp:Label></td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList id="ddllocation" runat="server">
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label23" runat="server" Text="Item Specification"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox id="txtItemSpec" runat="server" CssClass="multilinetext" EnableTheming="False"
                                ReadOnly="True" TextMode="MultiLine" Width="90%"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblRate" runat="server" Text="Rate"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtRate" runat="server"></asp:TextBox>
                            <asp:Label id="Label19" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                            <asp:RequiredFieldValidator id="RequiredFieldValidator6" runat="server" ControlToValidate="txtRate"
                                ErrorMessage="Please Enter the Rate" ValidationGroup="id" Visible="False">*</asp:RequiredFieldValidator><cc1:filteredtextboxextender
                                    id="ftxteRate" runat="server" enabled="False" targetcontrolid="txtRate" validchars=".0123456789"> </cc1:filteredtextboxextender></td>
                        <td style="text-align: right">
                            <asp:Label id="lblQuantity" runat="server" Text="Quantity"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtQuantity" runat="server" Width="139px"></asp:TextBox>
                            <asp:Label id="Label18" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                            <asp:RequiredFieldValidator id="RequiredFieldValidator7" runat="server" ControlToValidate="txtQuantity"
                                ErrorMessage="Please Enter the Quantity" ValidationGroup="id" Visible="False">*</asp:RequiredFieldValidator><cc1:filteredtextboxextender
                                    id="ftxteQuantity" runat="server" enabled="False" filtertype="Numbers" targetcontrolid="txtQuantity"
                                    validchars="."> </cc1:filteredtextboxextender></td>
                                            </tr>
                                            <tr>
                        <td style="text-align: right;" class="auto-style1">
                            <asp:Label id="lblAmount" runat="server" Text="Amount"></asp:Label></td>
                        <td style="text-align: left;" class="auto-style1">
                            <asp:TextBox id="txtAmount" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right;" class="auto-style1">
                        </td>
                        <td style="text-align: left;" class="auto-style1">
                            <asp:TextBox id="txtSpprice" runat="server" Visible="False"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtDeliDate" runat="server" Visible="False">
                            </asp:TextBox></td>
                                            </tr>
                                            <tr>
                        <td colspan="4">
                            <asp:Button id="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" onclick="btnAdd_Click" Text="Add"
                                ValidationGroup="id" /><asp:Button id="btnItemRefresh" runat="server"
                                    BackColor="Transparent" BorderStyle="None" CausesValidation="False" CssClass="loginbutton"
                                    EnableTheming="False" onclick="btnItemRefresh_Click" Text="Refresh" /></td>
                                            </tr>
                                            <tr>
                        <td colspan="4" style="text-align: center; height: 19px;">
                            &nbsp;<asp:GridView id="gvsales" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvsales_RowDataBound"
                                OnRowDeleting="gvsales_RowDeleting" ShowFooter="True" Width="100%">
                                <footerstyle backcolor="#1AA8BE" borderstyle="None" />
                                <columns>
<asp:CommandField ShowEditButton="True"></asp:CommandField>
<asp:CommandField ShowDeleteButton="True"></asp:CommandField>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
<asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
<asp:BoundField DataField="Vat" HeaderText="Vat"></asp:BoundField>
<asp:BoundField DataField="CST" HeaderText="CST"></asp:BoundField>
<asp:BoundField DataField="Excise" HeaderText="Excise"></asp:BoundField>
<asp:BoundField HeaderText="Amount"></asp:BoundField>
<asp:BoundField DataField="SPPrice" HeaderText="Special Price"></asp:BoundField>
<asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
<asp:BoundField DataField="ColorId" HeaderText="ColorId"></asp:BoundField>
<asp:BoundField DataField="GodownId" HeaderText="GodownId"></asp:BoundField>
<asp:BoundField DataField="CompanyId" HeaderText="CompanyId"></asp:BoundField>
</columns>
                                <emptydatatemplate>
<SPAN style="COLOR: #ff0033">No Data to Dispaly</SPAN>
</emptydatatemplate>
                            </asp:GridView></td>
                                            </tr>
                                            <tr>
                        <td class="profilehead" colspan="4">
                            Other Charges</td>
                                            </tr>
                                            <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label24" runat="server" Text="After One Month"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtTotalAmt" runat="server" ReadOnly="True">
                            </asp:TextBox><cc1:filteredtextboxextender id="FilteredTextBoxExtender1" runat="server"
                                targetcontrolid="txtMiscelleneous" validchars=".0123456789"> </cc1:filteredtextboxextender><asp:HiddenField id="txtAfteronemonthHidden" runat="server">
                                </asp:HiddenField>
                        </td>
                        <td style="text-align: right">
                            <asp:Label id="Label13" runat="server" Text="Include Vat"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtIncludeVat" runat="server">
                            </asp:TextBox>
                            <asp:Label id="Label15" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" Text="%"></asp:Label>
                            <asp:RadioButton id="rbVAT" runat="server" Checked="True" GroupName="vatcst" Text="VAT" Visible="False">
                            </asp:RadioButton><asp:RadioButton id="rbCST" runat="server" GroupName="vatcst" Text="C.S. Tax" Visible="False"></asp:RadioButton></td>
                                            </tr>
                                            <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label10" runat="server" Text="Within One Month"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtGrossAmount" runat="server" Width="349px">
                            </asp:TextBox><asp:HiddenField id="txtGrossTotalAmtHidden" runat="server">
                            </asp:HiddenField>
                        </td>
                        <td style="text-align: right">
                            <asp:Label id="lblVAT" runat="server" Text="VAT"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtVAT" runat="server">
                            </asp:TextBox>
                            <asp:Label id="Label3" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" Text="%"></asp:Label></td>
                                            </tr>
                                            <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label11" runat="server" Text="Miscelleneous"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtMiscelleneous" runat="server">
                            </asp:TextBox><cc1:filteredtextboxextender id="ftxteMiscelleneous" runat="server" targetcontrolid="txtMiscelleneous"
                                validchars=".0123456789"></cc1:filteredtextboxextender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label id="lblCSTax"
                                runat="server" Text="C.S. Tax"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtCST" runat="server" Width="149px"></asp:TextBox><asp:Label id="Label25" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" Text="%"></asp:Label><cc1:filteredtextboxextender id="ftxteVat" runat="server"
                                        targetcontrolid="txtVAT" validchars=".0123456789"> </cc1:filteredtextboxextender><cc1:filteredtextboxextender
                                            id="ftxteCST" runat="server" targetcontrolid="txtCST" validchars=".0123456789"></cc1:filteredtextboxextender></td>
                                            </tr>
                                            <tr>
                        <td style="text-align: right">
                            </td>
                        <td style="text-align: left">
                            &nbsp;
                        </td>
                        <td style="text-align: right">
                            <asp:Label id="Label9" runat="server" Text="Discount"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtDiscount" runat="server">
                            </asp:TextBox>
                            <asp:Label id="Label29" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" Text="%"></asp:Label>
                            <cc1:filteredtextboxextender id="ftxteDiscount" runat="server" targetcontrolid="txtDiscount"
                                validchars=".0123456789"></cc1:filteredtextboxextender>
                        </td>
                                            </tr>
                                            <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblRemarks" runat="server" Text="Remarks"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox id="txtRemarks" runat="server" CssClass="textbox" EnableTheming="False"
                                Height="53px" TextMode="MultiLine" Width="673px">
                            </asp:TextBox></td>
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
                        <td colspan="4">
                            <table id="tblButtons">
                                <tr>
                                    <td style="height: 26px">
                                        <asp:Button id="btnSave" runat="server" onclick="btnSave_Click" Text="Save" /></td>
                                    <td style="height: 26px">
                                        <asp:Button id="btnApprove" runat="server" CausesValidation="False" onclick="btnApprove_Click"
                                            Text="Approve" /></td>
                                    <td style="height: 26px">
                                        <asp:Button id="btnRefresh" runat="server" CausesValidation="False" onclick="btnRefresh_Click"
                                            Text="Refresh" /></td>
                                    <td style="height: 26px">
                                        </td>
                                    <td style="height: 26px">
                                        </td>
                                    <td style="width: 3px; height: 26px;">
                                        </td>
                                </tr>
                            </table>
                            <asp:HiddenField id="txtVatHidden" runat="server">
                            </asp:HiddenField>
                            <asp:HiddenField id="txtCstHidden" runat="server">
                            </asp:HiddenField>
                            </td>
                                            </tr>
                                        </table>
                                    <asp:Button id="btnGo" runat="server" CausesValidation="False" 
                                Text="Go" Visible="False" />
                                        <asp:Button id="btngo1" runat="server"  Text="Go" Visible="False" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }
    </style>
</asp:Content>



 
