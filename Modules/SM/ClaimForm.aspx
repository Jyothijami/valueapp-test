<%@ Page Language="C#" MasterPageFile="~/MPs/PurchaseMP1.master" AutoEventWireup="true"
    CodeFile="ClaimForm.aspx.cs" Inherits="Modules_SM_ClaimForm" Title="|| Value App : SM : CLAIMFORM ||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <script type="text/javascript">
    function TotalPrice()
    {
        var UnitPrice,Qty;
        Qty=document.getElementById('<%=txtQty.ClientID%>').value;
        UnitPrice=document.getElementById('<%=txtUnitPrice.ClientID%>').value;
        if(Qty=="" || UnitPrice=="")
        {
            document.getElementById('<%=txtTotalPrice.ClientID %>').value="0";
        }
        else if(Qty>0)
        {
            if(UnitPrice>0)
            {
                document.getElementById('<%=txtTotalPrice.ClientID %>').value=parseFloat(Qty)*parseFloat(UnitPrice);
            }
            else if(UnitPrice==0)
            {
                document.getElementById('<%=txtTotalPrice.ClientID %>').value="0";
            }
        }
    }
  
  
    function IRS()
    {
        var Conversion,Asperday;
        Conversion=document.getElementById('<%=txtTota.ClientID%>').value;
        Asperday=document.getElementById('<%=txtDay.ClientID%>').value;
        if(Conversion=="" ||  Asperday=="")
        {
            document.getElementById('<%=txtIrs.ClientID %>').value="0";
        }
        else if(Conversion>0)
        {
            if(Asperday>0)
            {
                document.getElementById('<%=txtIrs.ClientID %>').value=Asperday*Conversion;
            }
            else if(UnitPrice==0)
            {
                document.getElementById('<%=txtIrs.ClientID %>').value="0";
            }
        }

    }
    
    function Total()
    {
        var Charges,TotalEx,Cif,TotalCif,TotalAmt;
        TotalEx=document.getElementById('<%=txtTotal.ClientID%>').value;
        Charges=document.getElementById('<%=txtFob.ClientID%>').value;
        Cif=document.getElementById('<%=txtCif.ClientID%>').value;
        TotalCif=document.getElementById('<%=txtPrice.ClientID%>').value;
       
        if(TotalEx=="" || TotalEx=="0" || isNaN(TotalEx)){TotalEx="0";}
        if(Charges=="" || Charges=="0" || isNaN(Charges)){Charges="0";}
        if(Cif=="" || Cif=="0" || isNaN(Cif)){Cif="0";}
        if(TotalCif=="" || TotalCif=="0" || isNaN(TotalCif)){TotalCif="0";}
        
        TotalAmt=parseFloat(TotalEx)+parseFloat(Charges)+parseFloat(Cif);
        document.getElementById('<%=txtPrice.ClientID%>').value=TotalAmt;
        
    }
   
   
    </script>

    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                Claim Form</td>
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
            <td class="searchhead" colspan="4" style="text-align: left;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left;">
                            Claim Form</td>
                        <td>
                        </td>
                        <td style="text-align: right;">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSearch" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="CF_NO">CF No</asp:ListItem>
                                            <asp:ListItem Value="CF_DATE">CF Date</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME">Customer Name</asp:ListItem>
                                            <asp:ListItem Value="SUP_NAME">Supplier Name</asp:ListItem>
                                            <asp:ListItem Value="CF_PO_REF_NO">Purchase Order No</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td>
                                        <asp:DropDownList ID="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                            EnableTheming="False" Visible="False" Width="50px">
                                            <asp:ListItem Selected="True">=</asp:ListItem>
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem>&gt;</asp:ListItem>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <asp:ListItem>&gt;=</asp:ListItem>
                                            <asp:ListItem>R</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td>
                                        <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtSearchValueFromDate" runat="server" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox><asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender ID="ceSearchFrom" runat="server" Enabled="False" Format="dd/MM/yyyy"
                                            PopupButtonID="imgFromDate" TargetControlID="txtSearchValueFromDate">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender ID="ceSearchValueToDate" runat="server" Enabled="False" Format="dd/MM/yyyy"
                                            PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
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
            <td colspan="4" style="text-align: center">
                <asp:GridView ID="gvClaimForm" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="sdsCliamDetails" SelectedRowStyle-BackColor="#c0c0c0" OnRowDataBound="gvClaimForm_RowDataBound" Width="100%" AllowSorting="True">
                    <columns>
<asp:BoundField DataField="CF_ID" SortExpression="CF_ID" HeaderText="ClaimFormIdHidden">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="CF No" SortExpression="CF_NO"><EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CF_NO") %>'></asp:TextBox>
                            
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnCliamFormNo" onclick="lbtnClaimFormNo_Click" ForeColor="#0066ff" runat="server" Text='<%# Bind("CF_NO") %>' CausesValidation="False" __designer:wfdid="w94"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HtmlEncode="False" SortExpression="CF_DATE" DataFormatString="{0:dd/MM/yyyy}" DataField="CF_DATE" HeaderText="CF Date">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CF_PO_REF_NO" SortExpression="CF_PO_REF_NO"  HeaderText="Purchase Order">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField ReadOnly="True" SortExpression="CUST_NAME" DataField="CUST_NAME" HeaderText="Customer Name">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField ReadOnly="True" SortExpression="SUP_NAME" DataField="SUP_NAME" HeaderText="Supplier Name">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
</columns>
                    <emptydatatemplate>
<SPAN style="COLOR: #ff0033">No Record Found! </SPAN>
</emptydatatemplate>
                </asp:GridView>
                <asp:SqlDataSource ID="sdsCliamDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SM_CALIM_FORM_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <selectparameters>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName"
                            ControlID="lblSearchItemHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType"
                            ControlID="lblSearchTypeHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue"
                            ControlID="lblSearchValueHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom"
                            ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
                    </selectparameters>
                </asp:SqlDataSource>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: left;">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center;">
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
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; height: 471px;">
                <table border="0" cellpadding="0" cellspacing="0" id="tblClaimForm" runat="server"
                    visible="false">
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">
                            General Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right;">
                        </td>
                        <td style="height: 21px; text-align: left;">
                        </td>
                        <td style="height: 21px; text-align: right;">
                        </td>
                        <td style="height: 21px; text-align: left;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblCFNo" runat="server" Text="CF No"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtReference" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblDate" runat="server" Text="CF Date"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtDate" runat="server">
                            </asp:TextBox><asp:Image ID="imgDate" runat="server" ImageUrl="~/Images/Calendar.png">
                            </asp:Image><cc1:CalendarExtender ID="CeDate" runat="server" Format="DD/MM/yyyy"
                                PopupButtonID="imgDate" TargetControlID="txtDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MeeDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblPrincipal" runat="server" Text="Supplier Name" Width="119px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlPrincipal" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPrincipal_SelectedIndexChanged">
                            </asp:DropDownList><asp:Label ID="Label1" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblContact" runat="server" Text="Contact Person" Width="129px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtPrincipalContact" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td id="TD18" style="text-align: right">
                            <asp:Label ID="lblCustomer" runat="server" Text="Customer"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                            </asp:DropDownList><asp:Label ID="Label3" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                    ControlToValidate="ddlCustomer" ErrorMessage="Please Select the Customer" InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblRegion" runat="server" Text="Region"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtRegion" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label4" runat="server" Text="Industry Type"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtIndustryType" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblInitName" runat="server" Text="Unit Name" Width="74px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlUnitName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged">
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
                                Font-Names="Verdana" Font-Size="8pt" TextMode="MultiLine" Width="569px">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtContactPerson" runat="server" ReadOnly="True">
                            </asp:TextBox><asp:DropDownList ID="ddlContactPerson" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlContactPerson_SelectedIndexChanged" Visible="False">
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
                            <asp:Label ID="Label5" runat="server" Text="Mobile" Width="74px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblRef" runat="server" Text="Purchase Invoice No"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtRef" runat="server">
                            </asp:TextBox><asp:Label ID="Label8" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblOrderDate" runat="server" Text="Purchase Invoice  Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtOrderDate" runat="server">
                            </asp:TextBox><asp:Image ID="imgOrderDate" runat="server" ImageUrl="~/Images/Calendar.png">
                            </asp:Image>&nbsp;
                            <cc1:CalendarExtender ID="ceOrderDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgOrderDate"
                                TargetControlID="txtOrderDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeOrderDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtOrderDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">
                            Product Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label id="Label2" runat="server" Text="Search By Brand"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList id="ddlBrandselect" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrandselect_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="text-align: right;">
                            <asp:Label id="Label28" runat="server" Text="Search:" Width="84px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox id="txtSearchModel" runat="server"></asp:TextBox><asp:RequiredFieldValidator id="RequiredFieldValidator17" runat="server"
                                ControlToValidate="txtSearchModel" ErrorMessage="Please Enter ModelNo For Search"
                                ValidationGroup="Search" Visible="False">*</asp:RequiredFieldValidator><asp:Button
                                    id="btnSearchModelNo" runat="server" BorderStyle="None" CausesValidation="False"
                                    CssClass="gobutton" EnableTheming="False" onclick="btnSearchModelNo_Click" Text="Go"
                                    ValidationGroup="Search" />
                            <asp:SqlDataSource id="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                <selectparameters>
<asp:ControlParameter PropertyName="SelectedValue" Type="Int64" DefaultValue="0" Name="BranbId" ControlID="ddlBrandselect"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtSearchModel"></asp:ControlParameter>
</selectparameters>
                            </asp:SqlDataSource></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblDetails" runat="server" Text="Product Details"></asp:Label></td>
                        <td style="text-align: left" colspan="3">
                            <asp:DropDownList ID="ddlDetails" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDetails_SelectedIndexChanged">
                            </asp:DropDownList><asp:Label ID="Label9" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                    ControlToValidate="ddlDetails" ErrorMessage="Please Select the Industry Type"
                                    InitialValue="0" ValidationGroup="Product">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label ID="lblSpecification" runat="server" Text="Specification"></asp:Label></td>
                        <td colspan="3" style="height: 24px; text-align: left">
                            &nbsp;<asp:TextBox ID="txtSpecification" runat="server" TextMode="MultiLine" Width="508px"
                                EnableTheming="False" Font-Names="Verdana" ReadOnly="True"></asp:TextBox><asp:Label
                                    ID="Label12" runat="server" EnableTheming="False" ForeColor="Red" Text="*"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="height: 43px; text-align: right">
                            <asp:Label ID="lblQty" runat="server" Text="QTY"></asp:Label></td>
                        <td style="height: 43px; text-align: left;">
                            <asp:TextBox ID="txtQty" runat="server">
                            </asp:TextBox><asp:Label ID="Label10" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label>
                            <cc1:FilteredTextBoxExtender ID="ftxtQuantity" runat="server" FilterType="Numbers"
                                TargetControlID="txtQty">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="height: 43px; text-align: right;">
                            <asp:Label ID="lblCurrency" runat="server" Text="Currency"></asp:Label></td>
                        <td style="height: 43px; text-align: left">
                            <asp:DropDownList ID="ddlCurrency" runat="server">
                            </asp:DropDownList><asp:Label ID="Label11" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label ID="lblUnitPrice" runat="server" Text="Unit Price"></asp:Label></td>
                        <td style="height: 24px; text-align: left;">
                            <asp:TextBox ID="txtUnitPrice" runat="server">
                            </asp:TextBox><asp:Label ID="Label13" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label>
                            <cc1:FilteredTextBoxExtender ID="ftxtetxtUnitPrice" runat="server" TargetControlID="txtUnitPrice"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="height: 24px; text-align: right;">
                            <asp:Label ID="lblTotalPrice" runat="server" Text="Total Price"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                            <asp:TextBox ID="txtTotalPrice" runat="server" ReadOnly="True">
                            </asp:TextBox><asp:Label ID="Label14" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label><br />
                            <cc1:FilteredTextBoxExtender ID="ftxtetxtTotalPrice" runat="server" TargetControlID="txtTotalPrice"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label id="Label6" runat="server" Text="Remarks"></asp:Label></td>
                        <td colspan="3" style="height: 24px; text-align: left">
                            <asp:TextBox id="txtRemarks" runat="server" EnableTheming="False" TextMode="MultiLine"
                                Width="516px">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 24px; text-align: center">
                            <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" OnClick="btnAdd_Click" Text="Add"
                                ValidationGroup="ip" /><asp:Button ID="btnRefreshItems" runat="server" BackColor="Transparent"
                                    BorderStyle="None" CausesValidation="False" CssClass="loginbutton" EnableTheming="False"
                                    OnClick="btnItemRefresh_Click" Text="Refresh" /></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="gvProductDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvProductDetails_RowDataBound"
                                OnRowDeleting="gvProductDetails_RowDeleting" Width="100%">
                                <columns>
<asp:CommandField ShowDeleteButton="True"></asp:CommandField>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField DataField="UnitPrice" HeaderText="UnitPrice"></asp:BoundField>
<asp:BoundField HeaderText="Amount"></asp:BoundField>
<asp:BoundField DataField="CurrencyName" HeaderText="Currency"></asp:BoundField>
<asp:BoundField DataField="Currency" HeaderText="CurrencyIdHidden"></asp:BoundField>
<asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
</columns>
                                <emptydatatemplate>
<SPAN style="COLOR: #ff0000">NoData Found</SPAN>
</emptydatatemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 24px; text-align: right">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            &nbsp;<asp:Label ID="lblTotal" runat="server" Text="Total ExWorks"></asp:Label></td>
                        <td style="height: 24px; text-align: left;">
                            <asp:TextBox ID="txtTotal" runat="server">
                            </asp:TextBox><asp:Label ID="Label16" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label></td>
                        <td style="height: 24px; text-align: right;">
                            <asp:Label ID="lblFob" runat="server" Text="FOB"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                            <asp:TextBox ID="txtFob" runat="server">
                            </asp:TextBox><asp:Label ID="Label15" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label><cc1:FilteredTextBoxExtender ID="ftxtetxtFobCharges" runat="server"
                                    TargetControlID="txtFob" ValidChars=".0123456789">
                                </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label ID="lblCif" runat="server" Text="CIF Charges"></asp:Label></td>
                        <td style="height: 24px; text-align: left;">
                            <asp:TextBox ID="txtCif" runat="server">
                            </asp:TextBox><asp:Label ID="Label18" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label><cc1:FilteredTextBoxExtender ID="ftxtetxtCifCharges" runat="server" TargetControlID="txtCif"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="height: 24px; text-align: right;">
                            <asp:Label ID="lblPrice" runat="server" Text="Total CIF Price"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                            <asp:TextBox ID="txtPrice" runat="server">
                            </asp:TextBox><asp:Label ID="Label20" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label><cc1:FilteredTextBoxExtender ID="ftxtetxtPrice" runat="server" TargetControlID="txtPrice"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblPort" runat="server" Text="Port Of Destination"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlPort" runat="server">
                            </asp:DropDownList><asp:Label ID="Label17" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label></td>
                        <td style="text-align: right;">
                            </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtTransfer" runat="server" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">
                            Transfer Details</td>
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
                            <asp:Label ID="lblItem" runat="server" Text="Item Name"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlItem" runat="server">
                            </asp:DropDownList><asp:Label ID="Label21" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlItem"
                                ErrorMessage="Please Select Item Name" InitialValue="0" ValidationGroup="Trans">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblValue" runat="server" Text="Value"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtValue" runat="server">
                            </asp:TextBox><asp:Label ID="Label22" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtClaim" runat="server" Visible="False"></asp:TextBox></td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnAdding" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" OnClick="btnAdding_Click" Text="Add"
                                ValidationGroup="ip" /><asp:Button ID="btnItemRefresh" runat="server" BackColor="Transparent"
                                    BorderStyle="None" CausesValidation="False" CssClass="loginbutton" EnableTheming="False"
                                    OnClick="btnItem_Click" Text="Refresh" /></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="gvItemDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvItemDetails_RowDataBound"
                                OnRowDeleting="gvItemDetails_RowDeleting" Width="100%">
                                <columns>
<asp:CommandField ShowDeleteButton="True"></asp:CommandField>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
<asp:BoundField DataField="Value" HeaderText="Value"></asp:BoundField>
<asp:BoundField HeaderText="Claim Value"></asp:BoundField>
</columns>
                                <emptydatatemplate>
<SPAN style="COLOR: #ff0000">No Data Found</SPAN>
</emptydatatemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblTo" runat="server" Text="Total Claim Value"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtTota" runat="server" ReadOnly="True">
                            </asp:TextBox><asp:HiddenField ID="txtTotalAmtHidden" runat="server"></asp:HiddenField>
                        </td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblCurr" runat="server" Text="Conversion Currency"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlConversion" runat="server">
                            </asp:DropDownList><asp:Label ID="Label24" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblDay" runat="server" Text="Currency Value As Per the Day" Width="197px">
                            </asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDay" runat="server">
                            </asp:TextBox><asp:Label ID="Label25" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblIrs" runat="server" Text="IRS"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtIrs" runat="server">
                            </asp:TextBox><asp:Label ID="Label26" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label></td>
                        <td style="text-align: right;">
                            </td>
                        <td style="text-align: left">
                            </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblAddres" runat="server" Text="Consignee Address/Billing Address"
                                Width="220px"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            &nbsp;<asp:TextBox ID="txtConsignee" runat="server" TextMode="MultiLine" Width="508px"
                                EnableTheming="False" Font-Names="Verdana"></asp:TextBox><asp:Label ID="Label29"
                                    runat="server" EnableTheming="False" ForeColor="Red" Text="*"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="height: 66px; text-align: right">
                            <asp:Label ID="lblPayment" runat="server" Text="Payment"></asp:Label></td>
                        <td style="height: 66px; text-align: left;">
                            <asp:TextBox ID="txtPayment" runat="server">
                            </asp:TextBox><br />
                            <cc1:FilteredTextBoxExtender ID="ftxtePayment" runat="server" FilterMode="InvalidChars"
                                InvalidChars="!@#$%^&*()_+|=\./-{}[]:&quot;;'<>?,./" TargetControlID="txtPayment">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="height: 66px; text-align: right;">
                            <asp:Label ID="lblAccount" runat="server" Text="Account No"></asp:Label></td>
                        <td style="height: 66px; text-align: left">
                            <asp:TextBox ID="txtAccount" runat="server">
                            </asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftxtAccountNo" runat="server" FilterMode="InvalidChars"
                                InvalidChars="!@#$%^&*()_+|=\./-{}[]:&quot;;'<>?,./" TargetControlID="txtAccount">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblDelivery" runat="server" Text="Delivery "></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtDelivery" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblSwift" runat="server" Text="Swift"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtSwift" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblInst" runat="server" Text="Delivery Instructions"></asp:Label></td>
                        <td style="text-align: left" colspan="3">
                            &nbsp;<asp:TextBox ID="txtInstructions" runat="server" TextMode="MultiLine" Width="508px"
                                EnableTheming="False" Font-Names="Verdana"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: right; height: 20px;">
                            <table id="tblInsuranceCompany" width="100%">
                                <tr>
                                    <td class="profilehead" colspan="4">
                                        Insurance details</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="Insurance Company"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:DropDownList id="ddlInsuranceCompany" runat="server">
                                        </asp:DropDownList></td>
                                    <td style="width: 100px" align="right">
                                        <asp:Label ID="Label19" runat="server" Text="Telephone"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox id="txtTelephone" runat="server">
                                        </asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label23" runat="server" Text="Contact Person"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox id="txtInsContactperson" runat="server">
                                        </asp:TextBox></td>
                                    <td style="width: 100px" align="right">
                                        <asp:Label ID="Label27" runat="server" Text="Address"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox id="txtAddress" runat="server">
                                        </asp:TextBox></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 20px; text-align: right">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left; height: 20px;" class="profilehead">
                            Reference Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right;">
                        </td>
                        <td style="height: 21px; text-align: left;">
                        </td>
                        <td style="height: 21px; text-align: right;">
                        </td>
                        <td style="height: 21px; text-align: left;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                            </asp:DropDownList><asp:Label ID="Label33" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By" Width="96px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlApprovedBy" runat="server" Enabled="False">
                            </asp:DropDownList><asp:Label ID="Label34" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label></td>
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
                        <td colspan="4" style="height: 68px">
                            <br />
                            <table id="tblButtons">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" Width="55px" /></td>
                                    <td>
                                        <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" /></td>
                                    <td style="width: 3px">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 21px">
            </td>
            <td style="height: 21px">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False">
                </asp:ValidationSummary>
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Product"
                    ShowMessageBox="True" ShowSummary="False">
                </asp:ValidationSummary>
                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="Trans"
                    ShowMessageBox="True" ShowSummary="False">
                </asp:ValidationSummary>
            </td>
            <td style="height: 21px;">
            </td>
            <td style="height: 21px; width: 7px;">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 5px">
            </td>
        </tr>
    </table>
</asp:Content>

 
