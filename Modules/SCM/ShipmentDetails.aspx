<%@ Page Language="C#" MasterPageFile="~/MPs/PurchaseMP1.master" AutoEventWireup="true" CodeFile="ShipmentDetails.aspx.cs"
     Inherits="Modules_SCM_Shipment_Details" Title="|| Value App : Purchasing Management : Shipment Details ||" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table id="tblHeading" border="0" cellpadding="0" cellspacing="0" class="pagehead"
        width="100%">
        <tr>
            <td colspan="3">
                Shipment Details</td>
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
    <table id="tblMain" border="0" cellpadding="0" cellspacing="0" style="width:100%">
        <tr>
            <td class="searchhead" colspan="3">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left" colspan="2">
                                    Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
                                </td>

                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td>
                                        <asp:Label id="Label7" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td style="width: 40px">
                                        <asp:DropDownList id="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:TextBox id="txtSearchText" runat="server" CssClass="textbox">
                                        </asp:TextBox>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Button id="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" onclick="btnSearchGo_Click" Text="Go" /></td>
                                </tr>
                                </table>
                            <asp:Label id="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: center;">
                <asp:GridView id="gvShippingDetails" runat="server" SelectedRowStyle-BackColor="#c0c0c0" AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
                    Width="100%" OnRowDataBound="gvShippingDetails_RowDataBound" AllowPaging="True" AllowSorting="True">
                    <columns>
<asp:BoundField DataField="SD_ID" SortExpression="SD_ID" HeaderText="HiddenField"></asp:BoundField>
<asp:TemplateField HeaderText="Shipping Details No" SortExpression="SD_NO"><ItemTemplate>
<asp:LinkButton id="lbtnSdNo" onclick="lbtnSdNo_Click" ForeColor="#0066ff" runat="server" Text='<%# Bind("SD_NO") %>' __designer:wfdid="w10"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="FPO_NO" SortExpression="FPO_NO" HeaderText="Purchase Invoice No"></asp:BoundField>
<asp:BoundField HtmlEncode="False" SortExpression="DATE_ARRIVAL" DataFormatString="{0:dd/MM/yyyy}" DataField="DATE_ARRIVAL" HeaderText="Arrival Date"></asp:BoundField>
<asp:BoundField HtmlEncode="False" SortExpression="DATE_OF_SHIPPING" DataFormatString="{0:dd/MM/yyyy}" DataField="DATE_OF_SHIPPING" HeaderText="Shipping Date"></asp:BoundField>
</columns>
                    <emptydatatemplate>
<SPAN style="COLOR: #ff0033">No Data to Display</SPAN>
</emptydatatemplate>
                </asp:GridView><asp:SqlDataSource id="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SCM_SHIPINGDETAILS_SEARCH_SELECT" SelectCommandType="StoredProcedure"><selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
</selectparameters>
                </asp:SqlDataSource></td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: center">
                <table align="center">
                    <tr>
                        <td>
                            <asp:Button id="btnAdd" runat="server" OnClick="btnAdd_Click" Text="New" /></td>
                        <td>
                            <asp:Button id="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" /></td>
                        <td>
                            <asp:Button id="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" /></td>
                        <td>
                            <asp:Button id="btnFollowup" runat="server" OnClick="btnFollowup_Click" Text="FollowUp" Visible="False" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="height: 19px">
                <table id="tblDetails" runat="server" border="0" cellpadding="0" cellspacing="0"
                    visible="false">
                    <tr>
                        <td class="profilehead" colspan="4">
                            General Details</td>
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
                            <asp:Label id="lblSalesOrderNo" runat="server" Text="PI No" Width="54px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlPONo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPONo_SelectedIndexChanged"
                                Width="147px">
                            </asp:DropDownList></td>
                        <td style="text-align: right;">
                            <asp:Label id="lblSalesOrderDate" runat="server" Text="PI Date" Width="64px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtPIDate" runat="server"></asp:TextBox>&nbsp;<asp:Image id="imgCurrentDayTasksFromDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label id="Label20" runat="server" Text="PI Amount"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox id="txtPiAount" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label id="Label22" runat="server" Text="PO Date"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox id="txtPoDate" runat="server">
                            </asp:TextBox>
                            <asp:Image id="ImgPoDate" runat="server" ImageUrl="~/Images/Calendar.png">
                            </asp:Image></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label21" runat="server" Text="PO No"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtPoNo" runat="server">
                            </asp:TextBox>
                            <asp:Label ID="lblPONo" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label id="Label23" runat="server" Text="PO Amount"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtPOamount" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblSupplierName" runat="server" Text="Supplier Name" Width="98px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtSupplierName" runat="server" ReadOnly="True">
                            </asp:TextBox><asp:DropDownList id="ddlSupplierName" runat="server" Visible="False">
                            </asp:DropDownList></td>
                        <td style="text-align: right;">
                            <asp:Label id="lblContactPerson" runat="server" Text="Contact Person" Width="102px">
                            </asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtContactPerson" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label id="lblAddress" runat="server" Text="Address"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox id="txtAddress" runat="server" ReadOnly="True" TextMode="MultiLine">
                            </asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label id="lblPhone" runat="server" Text="Phone No."></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox id="txtPhoneNo" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblEmail" runat="server" Text="Email"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtEmail" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label id="lblMobileNo" runat="server" Text="Mobile No"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtMobileNo" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label29" runat="server" Text="Invoice No"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtInvoiceno" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label id="Label30" runat="server" Text="Invoice Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtInvoicedate" runat="server">
                            </asp:TextBox>
                            <asp:Image id="Image2" runat="server" ImageUrl="~/Images/Calendar.png">
                            </asp:Image></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label35" runat="server" Text="Invoice Value"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtInvoiceValue" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            &nbsp;<asp:GridView id="gvItemDetails" runat="server" AutoGenerateColumns="False"
                                OnRowDataBound="gvItemDetails_RowDataBound" OnRowDeleting="gvItemDetails_RowDeleting"
                                OnRowEditing="gvItemDetails_RowEditing" Width="100%"><columns>
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
                            </asp:GridView></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4">
                            Forwarding details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            </td>
                        <td style="text-align: left">
                            </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label id="Label31" runat="server" Text="Forwarder"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList id="ddlForwarder" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlForwarder_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label17" runat="server" Text="Phone/Mobile"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtshipingDetailsPhone" runat="server"></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label id="Label19" runat="server" Text="E Mail"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtShippingdetailsEmail" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label18" runat="server" Text="Address"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtShipmentDetailsAddress" runat="server" EnableTheming="False" Height="54px" TextMode="MultiLine"
                                Width="226px" ></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label id="Label3" runat="server" Text="Forwarding Through"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtFortrough" runat="server" EnableTheming="False" Height="54px" TextMode="MultiLine"
                                Width="226px">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="height: 6px; text-align: left">
                            Shipment Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                        <td style="text-align: right;">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label1" runat="server" Text="Bill of Lading/Air Way Bill"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtShipmentNo" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label id="Label2" runat="server" Text="Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtShippingdate" runat="server">
                            </asp:TextBox>
                            <asp:Image id="imgDate" runat="server" ImageUrl="~/Images/Calendar.png">
                            </asp:Image></td>
                    </tr>
                    <tr>
                        <td rowspan="1" style="text-align: right">
                            <asp:Label id="Label26" runat="server" Text="Container"></asp:Label></td>
                        <td rowspan="1" style="text-align: left">
                            <asp:TextBox id="txtContainer" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label id="Label9" runat="server" Text="Shipment Details "></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtShipmentDetails" runat="server" EnableTheming="False" Height="52px" TextMode="MultiLine"
                                Width="212px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td rowspan="1" style="text-align: right">
                            <asp:Label id="Label28" runat="server" Text="Volume"></asp:Label></td>
                        <td rowspan="1" style="text-align: left">
                            <asp:TextBox id="txtVolume" runat="server">
                            </asp:TextBox></td>
                        <td style="height: 19px; text-align: right;">
                            <asp:Label id="Label13" runat="server" Text="Date Of Shipping"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:TextBox id="txtdateofshipping" runat="server">
                            </asp:TextBox>
                            <asp:Image id="ImgShip" runat="server" ImageUrl="~/Images/Calendar.png">
                            </asp:Image></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"><asp:Label id="Label27" runat="server" Text="Weight"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtWeight" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label id="Label25" runat="server" Text="Remittence Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtRemittenceDate" runat="server"></asp:TextBox>
                            <asp:Image id="imgRemittanceDate" runat="server" ImageUrl="~/Images/Calendar.png">
                            </asp:Image></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label id="Label10" runat="server" Text="Packing Charges"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox id="txtPackingCharges" runat="server"></asp:TextBox></td>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label id="Label6" runat="server" Text="Exp Date of Arrival"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox id="txtdateArrival" runat="server"></asp:TextBox>
                            <asp:Image id="imgDateofarrival" runat="server" ImageUrl="~/Images/Calendar.png">
                            </asp:Image></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label24" runat="server" Text="Remittence Amount"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtRemittenceAmount" runat="server"></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label id="Label12" runat="server" Text="Material Receipt Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtMaterialReceiptDate" runat="server"></asp:TextBox>
                            <asp:Image id="imgMatRecDate" runat="server" ImageUrl="~/Images/Calendar.png">
                            </asp:Image></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label5" runat="server" Text="Duty Excise"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtDutyExcise" runat="server"></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label id="Label11" runat="server" Text="Custom Clearance"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtCustomClearance" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label4" runat="server" Text="Insurance  Policy No"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtInsurance" runat="server"></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label id="Label32" runat="server" Text="Insurance Name"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlInsuranceCompany" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label33" runat="server" Text="Insurance Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtInsuranceDate" runat="server">
                            </asp:TextBox>
                            <asp:Image id="Image1" runat="server" ImageUrl="~/Images/Calendar.png">
                            </asp:Image></td>
                        <td style="text-align: right">
                            <asp:Label id="Label34" runat="server" Text="Insurance Amount"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtInsuranceAmount" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center; height: 49px;">
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Button id="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                    <td>
                                        <asp:Button id="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" /></td>
                                    <td>
                                        <asp:Button id="btnclose" runat="server" OnClick="btnclose_Click" Text="Close" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 6px">
                        </td>
                    </tr>
                </table>
                <table id="tblFollowup" runat="server" border="0" cellpadding="0" cellspacing="0"
                    visible="false">
                    <tr>
                        <td class="profilehead" colspan="4" style="height: 19px">
                            Follow up details</td>
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
                            <asp:Label id="Label14" runat="server" Text="Name"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtFollowname" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label id="Label16" runat="server" Text="Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtFollowDate" runat="server">
                            </asp:TextBox>
                            <asp:Image id="imgInvoiceDate" runat="server" ImageUrl="~/Images/Calendar.png">
                            </asp:Image></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label15" runat="server" Text="Follow Up Details"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox id="txtFollowupDetails" runat="server" EnableTheming="False" TextMode="MultiLine"
                                Width="559px" Height="200px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td colspan="3" style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Button id="btnFollowSave" runat="server" Text="Save" OnClick="btnFollowSave_Click" /></td>
                                    <td>
                                        <asp:Button id="btnFollowRefresh" runat="server" Text="Refresh" OnClick="btnFollowRefresh_Click" /></td>
                                    <td>
                                        <asp:Button id="btnHistory" runat="server" onclick="btnHistory_Click" Text="History" /></td>
                                    <td>
                                        <asp:Button id="btnFollowClose" runat="server" onclick="btnFollowClose_Click" Text="Close" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <table id="tblfollowupgrid" runat="server" border="0" cellpadding="0" cellspacing="0"
                                visible="false">
                                <tr>
                                    <td class="profilehead" colspan="3" style="width: 540px">
                                        follow up history</td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="width: 540px">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="width: 540px">
                                        <asp:GridView id="gvFollowuphistory" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" Width="98%">
                                            <columns>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="FU_DATE" HeaderText="Date"></asp:BoundField>
<asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Name"></asp:BoundField>
<asp:BoundField DataField="FU_DESC" HeaderText="Description"></asp:BoundField>
</columns>
                                        </asp:GridView><asp:SqlDataSource id="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                            SelectCommand="SELECT YANTRA_EMPLOYEE_MAST.EMP_FIRST_NAME, YANTRA_SHIPPING_DETAILS_FOLLOWUP.FU_DATE, YANTRA_SHIPPING_DETAILS_FOLLOWUP.FU_DESC, YANTRA_SHIPPING_DETAILS_FOLLOWUP.SHIPPING_DETAILS_FOLLOWUP_DET_ID FROM YANTRA_SHIPPING_DETAILS_FOLLOWUP INNER JOIN YANTRA_EMPLOYEE_MAST ON YANTRA_SHIPPING_DETAILS_FOLLOWUP.FU_NAME = YANTRA_EMPLOYEE_MAST.EMP_ID"></asp:SqlDataSource><table align="center">
                                                <tr>
                                                    <td>
                                                        <asp:Button id="BtnClose1" runat="server" Text="Close" OnClick="BtnClose1_Click" /></td>
                                                    <td>
                                                        <asp:Button id="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" /></td>
                                                </tr>
                                            </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <cc1:CalendarExtender ID="ceInvoiceDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                    PopupButtonID="imgInvoiceDate" TargetControlID="txtFollowDate">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                    PopupButtonID="imgDate" TargetControlID="txtShippingdate">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                    PopupButtonID="ImgShip" TargetControlID="txtdateofshipping">
                </cc1:CalendarExtender><cc1:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy"
                    PopupButtonID="imgDateofarrival" TargetControlID="txtdateArrival">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy"
                    PopupButtonID="imgMatRecDate" TargetControlID="txtMaterialReceiptDate">
                </cc1:CalendarExtender><cc1:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy"
                    PopupButtonID="imgRemittanceDate" TargetControlID="txtRemittenceDate">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender6" runat="server" Enabled="True" Format="dd/MM/yyyy"
                    PopupButtonID="ImgPoDate" TargetControlID="txtPoDate">
                </cc1:CalendarExtender><cc1:CalendarExtender ID="CalendarExtender7" runat="server" Enabled="True" Format="dd/MM/yyyy"
                    PopupButtonID="Image1" TargetControlID="txtInsuranceDate">
                </cc1:CalendarExtender><cc1:CalendarExtender ID="CalendarExtender8" runat="server" Enabled="True" Format="dd/MM/yyyy"
                    PopupButtonID="Image2" TargetControlID="txtInvoicedate">
                </cc1:CalendarExtender>
            </td>
        </tr>
    </table>

 








</asp:Content>


 
