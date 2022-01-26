<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="FEOrderProfile.aspx.cs" Inherits="Modules_SM_FEOrderProfile" Title="FE Order Profile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <script type="text/javascript">

function totalvaluecalc()
{
    var exworks,exciseduty,packing,cst,educess,seceducess,stamping,totalvalue,disc,fob,cif;
    var exworksvalue="0",excisedutyvalue="0",educessvalue="0",seceducessvalue="0",cstvalue="0";
    try{disc=document.getElementById('<%=txtDiscountedValue.ClientID %>').value;}catch(e){disc="0";}
    try{exworks=document.getElementById('<%=txtTotalPriceTotalHidden.ClientID %>').value;}catch(e){exworks="0";}
    try{exciseduty=document.getElementById('<%=txtExciseDuty.ClientID %>').value;}catch(e){exciseduty="0";}
    try{packing=document.getElementById('<%=txtPacking.ClientID %>').value;}catch(e){packing="0";}
    try{cst=document.getElementById('<%=txtCST.ClientID %>').value;}catch(e){cst="0";}
    try{educess=document.getElementById('<%=txtEduCess.ClientID %>').value;}catch(e){educess="0";}
    try{seceducess=document.getElementById('<%=txtSecEduCess.ClientID %>').value;}catch(e){seceducess="0";}
    try{stamping=document.getElementById('<%=txtStampingCharges.ClientID %>').value;}catch(e){stamping="0";}
    try{fob=document.getElementById('<%=txtFOBCharges.ClientID %>').value;}catch(e){fob="0";}
    try{cif=document.getElementById('<%=txtCIFCharges.ClientID %>').value;}catch(e){cif="0";}
    if(disc=="" || disc=="0" || isNaN(disc) ){disc="0";}
    if(exworks=="" || exworks=="0" || isNaN(exworks) ){exworks="0";}
    if(exciseduty=="" || exciseduty=="0" || isNaN(exciseduty) ){exciseduty="0";}
    if(packing=="" || packing=="0" || isNaN(packing) ){packing="0";}
    if(cst=="" || cst=="0" || isNaN(cst) ){cst="0";}
    if(educess=="" || educess=="0" || isNaN(educess) ){educess="0";}
    if(seceducess=="" || seceducess=="0" || isNaN(seceducess) ){seceducess="0";}
    if(stamping=="" || stamping=="0" || isNaN(stamping) ){stamping="0";}
    exworksvalue =(parseFloat(exworks)-parseFloat(disc));
    document.getElementById('<%=txtExWorksValue.ClientID %>').value=exworksvalue;
    excisedutyvalue=(parseFloat(exworksvalue)*parseFloat(exciseduty)/100);
    educessvalue=(parseFloat(excisedutyvalue)*parseFloat(educess)/100);
    seceducessvalue=(parseFloat(excisedutyvalue)*parseFloat(seceducess)/100);
    totalvalue=parseFloat(exworksvalue)+parseFloat(packing)+parseFloat(excisedutyvalue)+parseFloat(educessvalue)+parseFloat(seceducessvalue);
    cstvalue=(parseFloat(totalvalue)*parseFloat(cst)/100);
    document.getElementById('<%=txtTotalValue.ClientID %>').value=parseFloat(totalvalue)+parseFloat(cstvalue)+parseFloat(stamping)+parseFloat(fob)+parseFloat(cif);
    totalvalue= document.getElementById('<%=txtTotalValue.ClientID %>').value;
    document.getElementById('<%=txtTotalValue.ClientID %>').value= Math.round(totalvalue,3);
    
}   
    </script>

    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                FE Order Profile</td>
        </tr>
    </table>
    <br />
    <table border="0" cellpadding="0" cellspacing="0" width="750">
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">
                            FE order Profile</td>
                        <td>
                        </td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Label id="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:DropDownList id="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="ENQ_NO">Enquiry No.</asp:ListItem>
                                            <asp:ListItem Value="ENQ_DATE">Enquiry Date</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME">Customer Name</asp:ListItem>
                                            <asp:ListItem Value="ENQ_ORIG_BY">Orginated By</asp:ListItem>
                                            <asp:ListItem Value="ENQ_STATUS">Status</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:DropDownList id="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                            EnableTheming="False" Visible="False" Width="50px">
                                            <asp:ListItem Selected="True">=</asp:ListItem>
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem>&gt;</asp:ListItem>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <asp:ListItem>&gt;=</asp:ListItem>
                                            <asp:ListItem>R</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Label id="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
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
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Label id="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
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
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Button id="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" Text="Go" /></td>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                </tr>
                            </table>
                            <asp:Label id="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <asp:GridView id="gvFEOrderProfile" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="sdsFEOrderProfile" Width="100%" OnRowDataBound="gvFEOrderProfile_RowDataBound">
                    <columns>
<asp:BoundField DataField="FEOP_ID" SortExpression="FEOP_ID" HeaderText="FEOPIdHidden"></asp:BoundField>
<asp:TemplateField SortExpression="FEOP_NO" HeaderText="FEOP No.">

<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnFEOrderProfileNo" onclick="lbtnFEOrderProfileNo_Click" runat="server" Text='<%# Eval("FEOP_NO") %>' CausesValidation="False"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="FEOP_DATE" SortExpression="FEOP_DATE" HeaderText="FEOP Date">
<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CUST_NAME" SortExpression="CUST_NAME" HeaderText="Customer ">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CUST_CONTACT_PERSON" SortExpression="CUST_CONTACT_PERSON" HeaderText="Contact Person">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField ReadOnly="True" DataField="PREPAREDBY" SortExpression="PREPAREDBY" HeaderText="Prepared By">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField ReadOnly="True" DataField="APPROVEDBY" SortExpression="APPROVEDBY" HeaderText="Approved By">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
</columns>
                    <emptydatatemplate>
No Data Exist!
</emptydatatemplate>
                </asp:GridView><asp:SqlDataSource id="sdsFEOrderProfile" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SM_FE_ORDER_PROFILE_SEARCH_SELECT" SelectCommandType="StoredProcedure"><selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
</selectparameters>
                </asp:SqlDataSource>&nbsp;&nbsp;</td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: left;">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <table id="Table1">
                    <tr>
                        <td>
                            <asp:Button id="btnNew" runat="server" CausesValidation="False" onclick="btnNew_Click"
                                Text="New" /></td>
                        <td>
                            <asp:Button id="btnEdit" runat="server" CausesValidation="False" onclick="btnEdit_Click"
                                Text="Edit" /></td>
                        <td>
                            <asp:Button id="btnDelete" runat="server" CausesValidation="False" onclick="btnDelete_Click"
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
            <td colspan="4" style="text-align: center">
                <table border="0" cellpadding="0" cellspacing="0" id="tblOrderProfile" runat="server"
                    visible="false">
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">
                            General Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left;">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblProfileRefNo" runat="server" Text="Profile Reference No"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtProfileRefNo" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label id="Label4" runat="server" Text="Profile Reference Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtProfileRefDate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox><asp:Image id="imgProfileRefDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender
                                ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgProfileRefDate"
                                TargetControlID="txtProfileRefDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtProfileRefDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label id="lblDoc" runat="server" Text="DOC Ref. No"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox id="txtDocRefNo" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right;">
                        </td>
                        <td style="text-align: left;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblCustomer" runat="server" Text="Customer Name" Width="119px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList id="ddlCustomer" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label id="lblUnitName" runat="server" Text="Unit Name" Width="129px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlUnitName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblBilling" runat="server" Text="Billing Address"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            &nbsp;<asp:TextBox id="txtBillingAddress" runat="server" TextMode="MultiLine" Width="500px" EnableTheming="False" Font-Names="Verdana" Font-Size="8pt"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label9" runat="server" Text="Despatch / Consignee Address"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            &nbsp;<asp:TextBox id="txtDespatchAddress" runat="server" TextMode="MultiLine" Width="500px" EnableTheming="False" Font-Names="Verdana" Font-Size="8pt"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label id="lblContactPerson" runat="server" Text="Contact Person" Width="119px">
                            </asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList id="ddlContactPerson" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlContactPerson_SelectedIndexChanged">
                            </asp:DropDownList><asp:TextBox ID="txtCustContactPerson" runat="server" Visible="False"></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label id="lblTele" runat="server" Text="Tele Phone No"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox id="txtPhoneNo" runat="server">
                            </asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftxtePhoneNo" runat="server" TargetControlID="txtPhoneNo"
                                ValidChars="-0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label id="lblPurchase" runat="server" Text="Purchase Order Reference"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox id="txtPORefNo" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label id="lblPurchaseDate" runat="server" Text="Purchase Order Date"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox id="txtPORefDate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox><asp:Image id="imgPORefDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender
                                ID="cePORefDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgPORefDate"
                                TargetControlID="txtPORefDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meePORefDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtPORefDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label id="lblMarketing" runat="server" Text="Marketing"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:RadioButtonList id="rbMarketing" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>Direct</asp:ListItem>
                                <asp:ListItem>Distribution</asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td>
                        </td>
                        <td style="text-align: left;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblRegion" runat="server" Text="Region"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtRegion" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label id="lblTerritory" runat="server" Text="Territory"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtTerritory" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label id="lblDivison" runat="server" Text="Divison"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox id="txtDivison" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label id="Label1" runat="server" Text="Market Segment"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox id="txtMarketSegment" runat="server">
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
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Orders Details</td>
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
                            <asp:Label id="lblOrders" runat="server" Text="Orders :"></asp:Label></td>
                        <td colspan="2" style="text-align: left">
                            <asp:RadioButtonList id="rbOrders" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbOrders_SelectedIndexChanged"
                                AutoPostBack="True">
                                <asp:ListItem>For Indent Orders (FE)</asp:ListItem>
                                <asp:ListItem>For Inhouse Orders (INR)</asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0" id="tblFE" runat="server" visible="false"
                                width="100%">
                                <tr>
                                    <td class="profilehead" colspan="4" style="text-align: left">
                                        For Indent Orders ( FE )</td>
                                </tr>
                                <tr>
                                    <td style="height: 19px; text-align: right">
                                    </td>
                                    <td style="height: 19px; text-align: left;">
                                    </td>
                                    <td style="height: 19px; text-align: right">
                                    </td>
                                    <td style="height: 19px; text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label id="lblForward" runat="server" Text="Forwarder"></asp:Label></td>
                                    <td style="text-align: left;">
                                        <asp:TextBox id="txtForwarder" runat="server">
                                        </asp:TextBox></td>
                                    <td style="text-align: right">
                                        <asp:Label id="lblPort" runat="server" Text="Port Of Lading"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox id="txtPort" runat="server">
                                        </asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label id="Label6" runat="server" Text="Port Of Destination"></asp:Label></td>
                                    <td style="text-align: left;">
                                        <asp:TextBox id="txtPortOfDestination" runat="server">
                                        </asp:TextBox></td>
                                    <td style="text-align: right">
                                    </td>
                                    <td style="text-align: left">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" rowspan="2" style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0" id="tblINR" runat="server" visible="false"
                                width="100%">
                                <tr>
                                    <td class="profilehead" colspan="4" style="text-align: left">
                                        For InHouse Orders ( INR )</td>
                                </tr>
                                <tr>
                                    <td style="height: 19px; text-align: right">
                                    </td>
                                    <td style="width: 285px; height: 19px; text-align: left">
                                    </td>
                                    <td style="height: 19px; text-align: right">
                                    </td>
                                    <td style="height: 19px; text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label id="lblDespatch" runat="server" Text="Mode Of Despatch"></asp:Label></td>
                                    <td style="text-align: left; width: 285px;">
                                        <asp:TextBox id="txtDespatchMode" runat="server">
                                        </asp:TextBox></td>
                                    <td style="text-align: right">
                                        <asp:Label id="lblEcc" runat="server" Text="ECC No/Dt."></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox id="txtECCNo" runat="server">
                                        </asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label id="lblCST" runat="server" Text="CST No./Dt."></asp:Label></td>
                                    <td style="width: 285px; text-align: left">
                                        <asp:TextBox id="txtCSTNo" runat="server">
                                        </asp:TextBox></td>
                                    <td style="text-align: right">
                                        <asp:Label id="lblLST" runat="server" Text="LST No./Dt."></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox id="txtLSTNo" runat="server">
                                        </asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label id="lblTIN" runat="server" Text="TIN No./Dt."></asp:Label></td>
                                    <td style="width: 285px; text-align: left">
                                        <asp:TextBox id="txtTINNo" runat="server">
                                        </asp:TextBox></td>
                                    <td style="text-align: right">
                                        <asp:Label id="lblFreight" runat="server" Text="Freight Charges NA"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox id="txtFreightCharges" runat="server">
                                        </asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="ftxteFreightCharges" runat="server"
                                            TargetControlID="txtFreightCharges" ValidChars=".0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label id="lblOct" runat="server" Text="Octroi"></asp:Label></td>
                                    <td style="width: 285px; text-align: left">
                                        <asp:TextBox id="txtOctroi" runat="server">
                                        </asp:TextBox></td>
                                    <td style="text-align: right">
                                        <asp:Label id="lblInsurance" runat="server" Text="Insurance"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox id="txtInsurance" runat="server">
                                        </asp:TextBox></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Permission Details</td>
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
                            <asp:Label id="lblPermitted" runat="server" Text="Partshipment Permitted :"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:RadioButtonList id="rbPartshipment" runat="server" RepeatDirection="Horizontal"
                                Width="71px">
                                <asp:ListItem>Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td style="text-align: right">
                            <asp:Label id="lblRoad" runat="server" Text="Road Premit Required :"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:RadioButtonList id="rbRoadPermitReq" runat="server" RepeatDirection="Horizontal"
                                Width="71px">
                                <asp:ListItem>Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblAre" runat="server" Text="ARE1  Transaction :"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:RadioButtonList id="rbARE1Transaction" runat="server" RepeatDirection="Horizontal"
                                Width="71px">
                                <asp:ListItem>Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td style="text-align: right">
                            <asp:Label id="lblEpTransaction" runat="server" Text="EPCG Transaction :"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:RadioButtonList id="rbEPCGTransaction" runat="server" RepeatDirection="Horizontal"
                                Width="71px">
                                <asp:ListItem>Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblEnclosed" runat="server" Text="Document Enclosed :"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:RadioButtonList id="rbDocEnclosed" runat="server" RepeatDirection="Horizontal"
                                Width="71px">
                                <asp:ListItem>Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label2" runat="server" Text="Document No ,  If Enclosed :"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtDocumentNo" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Product Details</td>
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
                            <asp:Label id="lblProduct" runat="server" Text="Product Details"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlProductName" runat="server">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label id="lblQty" runat="server" Text="QTY"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtProductQty" runat="server">
                            </asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftxteProductQty" runat="server" FilterType="Numbers"
                                TargetControlID="txtProductQty">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblCurrency" runat="server" Text="Currency"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlCurrency" runat="server">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem>EURO</asp:ListItem>
                                <asp:ListItem>IRS</asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label id="lblUnit" runat="server" Text="Unit Price"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtProductUnitPrice" runat="server">
                            </asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftxteProductUnitPrice" runat="server"
                                TargetControlID="txtProductUnitPrice" ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblTotal" runat="server" Text="Total Price" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtProductTotalPrice" runat="server" ReadOnly="True" Visible="False">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
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
                        <td colspan="4">
                            <asp:Button id="btnProductsAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" Text="Add" ValidationGroup="ip"
                                OnClick="btnProductsAdd_Click" /><asp:Button id="btnProductsRefresh" runat="server"
                                    BackColor="Transparent" BorderStyle="None" CausesValidation="False" CssClass="loginbutton"
                                    EnableTheming="False" Text="Refresh" OnClick="btnProductsRefresh_Click" /></td>
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
                        <td colspan="4" style="text-align: center">
                            <asp:GridView id="gvProductDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvProductDetails_RowDataBound"
                                OnRowDeleting="gvProductDetails_RowDeleting" OnRowEditing="gvProductDetails_RowEditing">
                                <columns>
<asp:CommandField ShowEditButton="True"></asp:CommandField>
<asp:CommandField ShowDeleteButton="True"></asp:CommandField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Qty" HeaderText="Qty">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Currency" HeaderText="Currency">
<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="UnitPrice" NullDisplayText="-" HeaderText="Unit Price">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="TotalPrice" NullDisplayText="-" HeaderText="Total Price"></asp:BoundField>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code Hidden"></asp:BoundField>
</columns>
                            </asp:GridView>
                            </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                        <td style="text-align: right">
                            <asp:TextBox ID="txtTotalPriceTotalHidden" runat="server"></asp:TextBox></td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Finance Details</td>
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
                            <asp:Label id="lblValue" runat="server" Text="Discounted Value"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtDiscountedValue" runat="server">
                            </asp:TextBox><cc1:FilteredTextBoxExtender ID="ftxteDiscountedValue" runat="server"
                                TargetControlID="txtDiscountedValue" ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label id="lblEx" runat="server" Text="Ex-Work's Value"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtExWorksValue" runat="server">
                            </asp:TextBox><cc1:FilteredTextBoxExtender ID="ftxteExWorksValue" runat="server"
                                TargetControlID="txtExWorksValue" ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" id="tdfob1" runat="server" visible="false">
                            <asp:Label id="lblFOB" runat="server" Text="FOB Charges"></asp:Label></td>
                        <td style="text-align: left" id="tdfob2" runat="server" visible="false">
                            <asp:TextBox id="txtFOBCharges" runat="server">
                            </asp:TextBox><cc1:FilteredTextBoxExtender ID="ftxteFOBCharges" runat="server" TargetControlID="txtFOBCharges"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right" id="tdcif1" runat="server" visible="false">
                            <asp:Label id="lblCIF" runat="server" Text="CIF Charges"></asp:Label></td>
                        <td style="text-align: left" id="tdcif2" runat="server" visible="false">
                            <asp:TextBox id="txtCIFCharges" runat="server">
                            </asp:TextBox><cc1:FilteredTextBoxExtender ID="ftxteCIFCharges" runat="server" TargetControlID="txtCIFCharges"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" id="tdpack1" runat="server" visible="false">
                            <asp:Label id="lblPacking" runat="server" Text="Packing and Forwarding"></asp:Label></td>
                        <td style="text-align: left" id="tdpack2" runat="server" visible="false">
                            <asp:TextBox id="txtPacking" runat="server">
                            </asp:TextBox><cc1:FilteredTextBoxExtender ID="ftxtePacking" runat="server" FilterType="Numbers"
                                TargetControlID="txtPacking">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right" id="tdexcise1" runat="server" visible="false">
                            <asp:Label id="lblDuty" runat="server" Text="Excise Duty"></asp:Label></td>
                        <td style="text-align: left" id="tdexcise2" runat="server" visible="false">
                            <asp:TextBox id="txtExciseDuty" runat="server">
                            </asp:TextBox><asp:Label id="Label5" runat="server" EnableTheming="True" Text="%"></asp:Label><cc1:FilteredTextBoxExtender
                                ID="ftxteExciseDuty" runat="server" TargetControlID="txtExciseDuty" ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" id="tdeducess1" runat="server" visible="false">
                            <asp:Label id="lblEdu" runat="server" Text="Edu.Cess"></asp:Label></td>
                        <td style="text-align: left;" id="tdeducess2" runat="server" visible="false">
                            <asp:TextBox id="txtEduCess" runat="server">
                            </asp:TextBox><asp:Label id="Label7" runat="server" EnableTheming="True" Text="%"></asp:Label><cc1:FilteredTextBoxExtender
                                ID="ftxteEduCess" runat="server" TargetControlID="txtEduCess" ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right;" id="tdseceducess1" runat="server" visible="false">
                            <asp:Label id="lblCess" runat="server" Text="Secondary Edu.Cess"></asp:Label></td>
                        <td style="text-align: left;" id="tdseceducess2" runat="server" visible="false">
                            <asp:TextBox id="txtSecEduCess" runat="server">
                            </asp:TextBox><asp:Label id="Label8" runat="server" EnableTheming="True" Text="%"></asp:Label>
                            <cc1:FilteredTextBoxExtender ID="ftxteSecEduCess" runat="server" TargetControlID="txtSecEduCess"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" id="tdcst1" runat="server" visible="false">
                            <asp:Label id="lblCs" runat="server" Text="CST"></asp:Label></td>
                        <td style="text-align: left" id="tdcst2" runat="server" visible="false">
                            <asp:TextBox id="txtCST" runat="server">
                            </asp:TextBox><asp:Label id="Label44" runat="server" EnableTheming="True" Text="%"></asp:Label><cc1:FilteredTextBoxExtender
                                ID="ftxteCST" runat="server" TargetControlID="txtCST" ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right" id="tdstamp1" runat="server" visible="false">
                            <asp:Label id="lblIc" runat="server" Text="I & C Stamping Charges"></asp:Label></td>
                        <td style="text-align: left" id="tdstamp2" runat="server" visible="false">
                            <asp:TextBox id="txtStampingCharges" runat="server">
                            </asp:TextBox><cc1:FilteredTextBoxExtender ID="ftxteStampingCharges" runat="server"
                                TargetControlID="txtStampingCharges" ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblTotalVlaue" runat="server" Text="Total Value"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtTotalValue" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblDelivery" runat="server" Text="Delivery Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtDeliveryDate" runat="server">
                            </asp:TextBox><asp:Image id="imgDeliveryDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender
                                ID="ceDeliveryDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgDeliveryDate"
                                TargetControlID="txtDeliveryDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeDeliveryDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtDeliveryDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label id="lblPeriod" runat="server" Text="Warranty Period"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtWarrantyPeriod" runat="server">
                            </asp:TextBox></td>
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
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Buyer Details</td>
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
                            <asp:Label id="Label3" runat="server" Text="Buyer Type :"></asp:Label></td>
                        <td colspan="2" style="text-align: left">
                            <asp:RadioButtonList id="rbBuyerType" runat="server" RepeatDirection="Horizontal"
                                Width="413px">
                                <asp:ListItem>User Buyer</asp:ListItem>
                                <asp:ListItem>Economic Buyer</asp:ListItem>
                                <asp:ListItem>Technical Buyer</asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblContact" runat="server" Text="Contact Person"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlBuyerContactPerson" runat="server">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label id="lblDesg" runat="server" Text="Designation"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtBuyerDesig" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblTeleNo" runat="server" Text="Tele No.Direct"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtBuyerTeleNo" runat="server">
                            </asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftxteBuyerTeleNo" runat="server" TargetControlID="txtBuyerTeleNo"
                                ValidChars="-0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label id="lblMobile" runat="server" Text="Mobile No"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtBuyerMobileNo" runat="server">
                            </asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftxteBuyerMobileNo" runat="server" TargetControlID="txtBuyerMobileNo"
                                ValidChars="-0123456789">
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
                        <td colspan="4">
                            <asp:Button id="btnBuyerAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" Text="Add" ValidationGroup="ip"
                                OnClick="btnBuyerAdd_Click" /><asp:Button id="btnBuyerRefresh" runat="server" BackColor="Transparent"
                                    BorderStyle="None" CausesValidation="False" CssClass="loginbutton" EnableTheming="False"
                                    Text="Refresh" OnClick="btnBuyerRefresh_Click" /></td>
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
                        <td colspan="4" style="height: 19px; text-align: center">
                            <asp:GridView id="gvBuyerDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvBuyerDetails_RowDataBound"
                                OnRowDeleting="gvBuyerDetails_RowDeleting" OnRowEditing="gvBuyerDetails_RowEditing">
                                <columns>
<asp:CommandField ShowEditButton="True"></asp:CommandField>
<asp:CommandField ShowDeleteButton="True"></asp:CommandField>
<asp:BoundField DataField="BuyerType" HeaderText="Buyer Type">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ContactPerson" HeaderText="Contact Person">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Designation" HeaderText="Designation">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="TelNo" HeaderText="Tel No">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="MobileNo" HeaderText="Mobile No">
<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
</columns>
                            </asp:GridView></td>
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
                        <td style="height: 19px; text-align: left" class="profilehead" colspan="4">
                            Other &nbsp;Details</td>
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
                            <asp:Label id="lblPayment" runat="server" Text="Payment Terms"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:DropDownList id="rbPaymentTerms" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbPaymentTerms_SelectedIndexChanged" >
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem>CAD</asp:ListItem>
                                <asp:ListItem>TT</asp:ListItem>
                                <asp:ListItem>LC</asp:ListItem>
                                <asp:ListItem>FDD</asp:ListItem>
                                <asp:ListItem>WT</asp:ListItem>
                                <asp:ListItem>Others</asp:ListItem>
                            </asp:DropDownList><asp:TextBox id="txtOtherPayTerms" runat="server" EnableTheming="True" Width="410px" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblAdvanced" runat="server" Text="Advanced Recd. Details"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtAdvRecdDetails" runat="server">
                            </asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftxteAdvRecdDetails" runat="server"
                                TargetControlID="txtAdvRecdDetails" ValidChars=".0123456789NAna">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label id="lblDD" runat="server" Text="Cheque /DD/E-Payment" Width="167px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtChequePayment" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblBank" runat="server" Text="Full Postal Address of Bank In Case of  Documents Through Bank"
                                Width="217px"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            &nbsp;<asp:TextBox id="txtFullPostalAddress" runat="server" EnableTheming="False"
                                TextMode="MultiLine" Width="522px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblCon" runat="server" Text="Contact Person"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtContactPerson" runat="server">
                            </asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftxteContactPerson" runat="server" FilterMode="InvalidChars"
                                InvalidChars="`1234567890-=+_)(*&^%$#@!~" TargetControlID="txtContactPerson">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label id="lblNoDirect" runat="server" Text="Tele No. Direct"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtTelNo" runat="server">
                            </asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftxteTelNo" runat="server" TargetControlID="txtTelNo"
                                ValidChars="-0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblMobiNo" runat="server" Text="Mobile No"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtMobileNo" runat="server">
                            </asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftxteMobileNo" runat="server" TargetControlID="txtMobileNo"
                                ValidChars="-0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblAny" runat="server" Text="Special Instructions If Any"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            &nbsp;<asp:TextBox id="txtSplInstr" runat="server" EnableTheming="False" TextMode="MultiLine"
                                Width="521px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblName" runat="server" Text="Name" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtName" runat="server" Visible="False"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftxteName" runat="server" FilterMode="InvalidChars"
                                InvalidChars="`1234567890-=+_)(*&^%$#@!~" TargetControlID="txtName">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            </td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtSignature" runat="server" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblPrice" runat="server" Text="Percentage  Discount Offered  Over List Price">
                            </asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtDiscountPrice" runat="server">
                            </asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftxteDiscountPrice" runat="server" FilterType="Numbers"
                                TargetControlID="txtDiscountPrice">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" rowspan="5" style="text-align: right">
                        </td>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">
                            Reference Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: left;">
                        </td>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label id="lblBy" runat="server" Text="Order Booked By"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList id="ddlOrderBookedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                        <td style="text-align: right;">
                            <asp:Label id="lblHead" runat="server" Text="Approved By Business Head" Width="174px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList id="ddlApprovedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
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
                        <td colspan="4">
                            <table id="tblButtons">
                                <tr>
                                    <td>
                                        <asp:Button id="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                    <td>
                                        <asp:Button id="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" /></td>
                                    <td>
                                        <asp:Button id="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" /></td>
                                    <td>
                                        <asp:Button id="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" /></td>
                                    <td>
                                        <asp:Button id="btnClaim" runat="server" Text="Claim Form" Width="79px" OnClick="btnClaim_Click" /></td>
                                    <td>
                                        <asp:Button id="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" /></td>
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
            </td>
            <td style="height: 21px;">
            </td>
            <td style="height: 21px">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 21px">
                <asp:TextBox id="txtTotalItemsValue" runat="server" Visible="False">
                </asp:TextBox></td>
        </tr>
    </table>
</asp:Content>

 
