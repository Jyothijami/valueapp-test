<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SCM.aspx.cs" Inherits="Modules_Reports_SCM" Title="|| Yantra : Reports : SCM||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <script language="javascript" type="text/javascript">
        function PrintDivContent(printdiv) {
            var printContent = document.getElementById(printdiv);
            var WinPrint = window.open('', '', 'left=0,top=0,toolbar=0,sta­tus=0');
            WinPrint.document.write(printContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
        }
    </script>

    <script language="javascript" type="text/javascript">
        function PrintDivContent(Reservestock) {
            var printContent = document.getElementById(Reservestock);
            var WinPrint = window.open('', '', 'left=0,top=0,toolbar=0,sta­tus=0');
            WinPrint.document.write(printContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
        }
    </script>
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td style="text-align: left">SCM Reports</td>
        </tr>
    </table>
    <table>
        <tr>
            <td valign="top">
                <table style="width:175px;height:60px;font-weight:bolder;"  border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="border-bottom: 1px solid #c0c0c0; font-size: 17px; padding-bottom: 11px; text-align: center;"></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnGoodsReceiptOfTheDay" runat="server" CausesValidation="False"
                                CssClass="leftmenu" EnableTheming="False" OnClick="lbtnMenuLinks_Click">Goods Receipt Of A Day</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbntDispatchDetailsForTheDay" runat="server" CausesValidation="False"
                                CssClass="leftmenu" EnableTheming="False" OnClick="lbtnMenuLinks_Click">Dispatch Details Of A Day</asp:LinkButton></td>
                    </tr>


                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnClosingStockReport" runat="server" CausesValidation="False"
                                CssClass="leftmenu" EnableTheming="False" OnClick="lbtnMenuLinks_Click">Blocked Stock Statement</asp:LinkButton></td>
                    </tr>

                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnProformaInvoice" runat="server" CausesValidation="False" 
                                CssClass="leftmenu" EnableTheming="False" OnClick="lbtnMenuLinks_Click">Customer Analysis</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnPurchaseOrderList" runat="server" CausesValidation="False" Visible="false"
                                CssClass="leftmenu" EnableTheming="False" OnClick="lbtnMenuLinks_Click">Purchase Orders List</asp:LinkButton></td>
                    </tr>

                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnPurchaseInvoice" runat="server" CausesValidation="False" Visible="false"
                                CssClass="leftmenu" EnableTheming="False" OnClick="lbtnMenuLinks_Click">Purchase Invoice</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnResverveStock" runat="server" CausesValidation="False"
                                CssClass="leftmenu" EnableTheming="False" OnClick="lbtnMenuLinks_Click">Sales Report</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnShipping" runat="server" CausesValidation="False"
                                CssClass="leftmenu" EnableTheming="False" OnClick="lbtnMenuLinks_Click">Shipment Track Details</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
            <td style="text-align: center" width="100%" valign="top">
                <table width="600" border="0" cellpadding="0" cellspacing="0" id="tblGoodsReceipt" runat="server" visible="false">
                    <tr>
                        <td class="profilehead" colspan="2" style="text-align: left">goods receipt of the day</td>
                        <td style ="text-align :right ">
                <asp:DropDownList ID="ddlNoOfRecords" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged">
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>75</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                </asp:DropDownList>
            </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">Company Name : </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="COMP_NAME" DataValueField="CP_ID">
                                                <asp:ListItem Value ="0">--</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT a.CP_ID, a.CP_FULL_NAME + ',' + b.locname AS COMP_NAME FROM YANTRA_COMP_PROFILE AS a INNER JOIN location_tbl AS b ON a.locid = b.locid"></asp:SqlDataSource>
                                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblPRDate" runat="server" Text="From" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtInvoiceDate" runat="server" type="datepic" CssClass="datetext" OnTextChanged ="txtInvoiceDate_TextChanged" AutoPostBack ="true"  EnableTheming="False"></asp:TextBox><%--<asp:Image ID="imgDate"
                                runat="server" ImageUrl="~/Images/Calendar.png" />--%><asp:RequiredFieldValidator ID="RequiredFieldValidator16"
                                    runat="server" ControlToValidate="txtInvoiceDate" ErrorMessage="Please Select the Invoice Date"
                                    ValidationGroup="gr">*</asp:RequiredFieldValidator><asp:CustomValidator ID="CustomValidator1"
                                        runat="server" ClientValidationFunction="DateCustomValidate" ControlToValidate="txtInvoiceDate"
                                        ErrorMessage="Please Enter the Invoice Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                        SetFocusOnError="True" ValidationGroup="gr">*</asp:CustomValidator><%--<cc1:CalendarExtender ID="ceDate"
                                            runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgDate"
                                            TargetControlID="txtInvoiceDate">
                                        </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditDate" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtInvoiceDate"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        
                        <td style="text-align: right">
                            <asp:Label ID="lblPRdt" runat="server" Text="To " Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtInDt" runat="server" type="datepic" CssClass="datetext" OnTextChanged ="txtInDt_TextChanged" AutoPostBack ="true"  EnableTheming="False"></asp:TextBox><%--<asp:Image ID="imgDate"
                                runat="server" ImageUrl="~/Images/Calendar.png" />--%><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                    runat="server" ControlToValidate="txtInDt" ErrorMessage="Please Select the Invoice Date"
                                    ValidationGroup="gr">*</asp:RequiredFieldValidator><asp:CustomValidator ID="CustomValidator5"
                                        runat="server" ClientValidationFunction="DateCustomValidate" ControlToValidate="txtInDt"
                                        ErrorMessage="Please Enter the Invoice Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                        SetFocusOnError="True" ValidationGroup="gr">*</asp:CustomValidator><%--<cc1:CalendarExtender ID="ceDate"
                                            runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgDate"
                                            TargetControlID="txtInvoiceDate">
                                        </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditDate" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtInvoiceDate"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                                    <asp:ImageButton ID="ibtmImage" runat="server" ImageUrl="~/Images/tick.png"  Width="18px" OnClick ="ibtmImage_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style ="text-align :right">MRN No : </td>
                        <td style ="text-align :left ">
                            <asp:DropDownList ID="ddlMRNNo" runat ="server" AppendDataBoundItems ="true" AutoPostBack ="true" DataSourceID ="SqlDataSource2" DataTextField ="CHK_NO" DataValueField ="CHK_ID" >
                                <asp:ListItem Value ="0" >--</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource2" runat ="server" ConnectionString ="<%$ ConnectionStrings:DBCon %>" SelectCommand ="Select CHK_ID, CHK_NO from YANTRA_CHECKING_FORMAT Order By CHK_ID desc" ></asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td style ="text-align :right ">Invoice No : </td>
                        <td style ="text-align :left ">
                            <asp:TextBox ID="txtinvNo" runat ="server" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style ="text-align :right ">Brand : </td>
                        <td style ="text-align :left ">
                            <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
                                            </asp:DropDownList>
                        </td>
                    </tr>
                     <tr>
                                        <td colspan="2" style="text-align: center">
                                            <asp:Button ID="btnMRNSearch" runat="server" OnClick="btnMRNSearch_Click" Text="Search" />
                                        </td>
                                    </tr>
                    <tr>
                        <td colspan ="2">
                            <table style ="width :100%">
                     <tr>
                        <td  colspan="4">
                            <div id="ptintMrn">
                                <asp:GridView ID="gvItemMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False" ShowFooter="true" Style="text-align: left" Width="100%" OnPageIndexChanging="gvItemMaster_PageIndexChanging" OnRowDataBound="gvItemMaster_RowDataBound">
                        <PagerStyle CssClass="pagination" HorizontalAlign="Left" VerticalAlign="Middle" />
                        <FooterStyle ForeColor="#0066ff" />
                        <Columns>
                            <asp:BoundField DataField ="CHK_NO" HeaderText="MRN No" />
                            <asp:BoundField DataField="chkdt" HeaderText ="MRN Date" />
                            <asp:BoundField DataField="CHK_INVOICE_NO" HeaderText="Inv No" />
                            <asp:BoundField DataField="Invdt" HeaderText="Inv Date" />
                            <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="Model No" />
                            <asp:BoundField DataField="ITEM_NAME"  HeaderText="Item Series" />
                            <asp:BoundField DataField="PRODUCT_COMPANY_NAME" HeaderText="Brand" />
                            <asp:BoundField DataField="CHK_DET_NETQTY" HeaderText="Mrn Qty" />
                            <asp:BoundField DataField="CHK_DET_REMARKS" HeaderText="Remarks" />
                            <%--<asp:BoundField DataField="Item_Price" HeaderText="Price" />--%>
                            <asp:TemplateField HeaderText="Image"   >
                            <ItemTemplate>
                                <asp:Label ID="lblPath" Visible="false" Text='<%#Eval("Item_Image") %>' runat="server" />
                                <asp:Image ID="Image1" runat="server" EnableTheming="False"  ImageUrl = '<%# Eval("Item_Path") %>'
                                    Width="100px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Image" >
                                                <ItemTemplate>
                                                    <asp:Image runat ="server" EnableTheming="False"  ImageUrl = '<%# Eval("Item_Image","http://183.82.108.55/Content/Images/{0}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <span style="color: #FF0000">No Data to Display</span>
                        </EmptyDataTemplate>
                    </asp:GridView>
                               <%-- <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                 SelectCommand="USP_MRN_SEARCH" SelectCommandType="StoredProcedure">
                                        <SelectParameters >
                                            <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="CHKID" ></asp:ControlParameter>
                                        </SelectParameters>
                                    </asp:SqlDataSource>--%>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        
                        <td colspan ="2" >
                            <asp:Button ID="btnExport" Visible ="false" runat ="server" Text="Export Excel" OnClick ="btnExport_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 24px">
                            <asp:Button ID="btnGoodsReceiptRpt" Visible ="false" runat="server" OnClick="btnGoodsReceiptRpt_Click" Text="Run Report" ValidationGroup="gr" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="gr" />
                        </td>
                    </tr>
                </table>
                        </td>
                    </tr>
                    </table>
                
                <table width="600" border="0" cellpadding="0" cellspacing="0" id="tblDespatchDetails" runat="server" visible="false">
                    <tr>
                        <td class="profilehead" colspan="3" style="text-align: left">despatch details for the day</td>
                    </tr>
                    <tr>
                        <td style="height: 19px"></td>
                        <td style="height: 19px"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label1" runat="server" Text="Dispatch Date" Width="128px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDispatchDate" runat="server" type="datepic" CssClass="datetext" EnableTheming="False"></asp:TextBox><%--<asp:Image ID="Image3"
                                runat="server" ImageUrl="~/Images/Calendar.png" />--%><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDispatchDate"
                                    ErrorMessage="Please Select the Dispatch Date" ValidationGroup="dd">*</asp:RequiredFieldValidator><asp:CustomValidator
                                        ID="CustomValidator2" runat="server" ClientValidationFunction="DateCustomValidate"
                                        ControlToValidate="txtDispatchDate" ErrorMessage="Please Enter the Dispatch Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                        SetFocusOnError="True" ValidationGroup="dd">*</asp:CustomValidator><%--<cc1:CalendarExtender ID="CalendarExtender1"
                                            runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="Image3"
                                            TargetControlID="txtDispatchDate">
                                        </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDispatchDate"
                                UserDateFormat="DayMonthYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 24px">
                            <asp:Button ID="btnDespatchDetailsRpt" runat="server" Text="Run Report" ValidationGroup="dd" OnClick="btnDespatchDetailsRpt_Click" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="dd" />
                        </td>
                    </tr>
                </table>
                <table width="600" border="0" cellpadding="0" cellspacing="0" id="tblClosingStock" runat="server" visible="false">
                    <tr>
                        <td class="profilehead" colspan="2" style="text-align: left">&nbsp;Stock Statement</td>
                    </tr>
                    <tr>
                        <td style="height: 19px"></td>
                        <td style="height: 19px"></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right" colspan="2">
                            <div id="divSearch" style="width: 100%">
                                <table style="width: 100%">
                                    <tr>
                                        <td> <asp:TextBox ID="txtItemCode" runat="server"></asp:TextBox>
                                            <asp:Button ID="btnGo" runat="server" Text="GO" OnClick="btnSearch_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">Company Name : </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlCompany" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="compsds1" DataTextField="COMP_NAME" DataValueField="CP_ID">
                                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="compsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT a.CP_ID, a.CP_FULL_NAME + ',' + b.locname AS COMP_NAME FROM YANTRA_COMP_PROFILE AS a INNER JOIN location_tbl AS b ON a.locid = b.locid"></asp:SqlDataSource>
                                        </td>
                                        <td></td>
                                        <td style="text-align: right">Location : </td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="ddlLocation" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="locsds1" DataTextField="wh_name" DataValueField="locid">
                                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="locsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT DISTINCT [locid],[wh_name] from v_inwardNew"></asp:SqlDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">Brand : </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td></td>
                                        <td style="text-align: right">Model No : </td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="ddlModelNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModelNo_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">Color : </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlColor" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td></td>
                                        <td style="text-align: right">From Date : </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtFromDate" runat="server" type="datepic"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">To Date : </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtToDate" runat="server" type="datepic"></asp:TextBox>
                                        </td>
                                        <td colspan="3"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" style="text-align: center">
                                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right" colspan="2">
                            <div id="printdiv">
                                <asp:GridView ID="gvStockReport" runat="server" Width="100%" AutoGenerateColumns ="false" OnRowDataBound ="gvStockReport_RowDataBound" >
                                     <Columns>
                            <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="Model N0" SortExpression="ITEM_MODEL_NO">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PRODUCT_COMPANY_NAME" HeaderText="BRAND" SortExpression="PRODUCT_COMPANY_NAME">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ITEM_NAME" HeaderText="Itwm Series" SortExpression="ITEM_NAME">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ITEM_SPEC" HeaderText="Description" SortExpression="ITEM_SPEC">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="COLOUR_NAME" HeaderText="Color" SortExpression="COLOUR_NAME">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CUST_NAME" HeaderText="Customer Name" SortExpression="CUST_NAME">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <%--<asp:BoundField DataField="BlockedOn" HeaderText="Blocked On" SortExpression="BlockedOn">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Executive" SortExpression="EMP_FIRST_NAME">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                                          <asp:TemplateField HeaderText="Image"   >
                            <ItemTemplate>
                                <asp:Label ID="lblPath" Visible="false" Text='<%#Eval("Item_Image") %>' runat="server" />
                                <asp:Image ID="Image1" runat="server" EnableTheming="False"  ImageUrl = '<%# Eval("Item_Path") %>'
                                    Width="100px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Image" >
                                                <ItemTemplate>
                                                    <asp:Image runat ="server" EnableTheming="False"  ImageUrl = '<%# Eval("Item_Image","http://183.82.108.55/Content/Images/{0}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                        </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="javascript:PrintDivContent('printdiv');" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"></td>
                    </tr>
                </table>
                <table id="tblPOL" runat="server" border="0" cellpadding="0" cellspacing="0" visible="false"
                    width="600">
                    <tr>
                        <td class="profilehead" colspan="3" style="text-align: left">purchase orders list</td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label37" runat="server" Text="Company Name" Width="103px"></asp:Label></td>
                        <td align="left">
                            <asp:DropDownList ID="ddlCompanyPO" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label12" runat="server" Text="From" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPOLFromDate" runat="server" type="datepic" CssClass="datetext" EnableTheming="False"></asp:TextBox><%--<asp:Image
                                ID="imgPOLFromDate" runat="server" ImageUrl="~/Images/Calendar.png" />--%><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPOLFromDate"
                                    ErrorMessage="Please Select the From Date" ValidationGroup="mids">*</asp:RequiredFieldValidator><asp:CustomValidator
                                        ID="CustomValidator3" runat="server" ClientValidationFunction="DateCustomValidate"
                                        ControlToValidate="txtPOLFromDate" ErrorMessage="Please Enter the From Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                        SetFocusOnError="True" ValidationGroup="mids">*</asp:CustomValidator><%--<cc1:CalendarExtender
                                            ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgPOLFromDate"
                                            TargetControlID="txtPOLFromDate">
                                        </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtPOLFromDate"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label6" runat="server" Text="To" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPOLToDate" runat="server" type="datepic" CssClass="datetext" EnableTheming="False"></asp:TextBox><%--<asp:Image
                                ID="imgPOLToDate" runat="server" ImageUrl="~/Images/Calendar.png" />--%><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPOLToDate"
                                    ErrorMessage="Please Select the To Date" ValidationGroup="mids">*</asp:RequiredFieldValidator><asp:CustomValidator
                                        ID="CustomValidator4" runat="server" ClientValidationFunction="DateCustomValidate"
                                        ControlToValidate="txtPOLToDate" ErrorMessage="Please Enter the To Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                        SetFocusOnError="True" ValidationGroup="mids">*</asp:CustomValidator><%--<cc1:CalendarExtender
                                            ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgPOLToDate"
                                            TargetControlID="txtPOLToDate">
                                        </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtPOLToDate"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label39" runat="server" Text="Department" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlDepartmentPO" runat="server" AutoPostBack="True" meta:resourcekey="ddlDepartmentResource1"
                                OnSelectedIndexChanged="ddlDepartmentPO_SelectedIndexChanged" Width="154px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlDepartmentPO"
                                ErrorMessage="Please Select the Quotation Department" ValidationGroup="mids">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label40" runat="server" Text="Employee Name" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlEmployeeNamePO" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlEmployeeNamePO"
                                ErrorMessage="Please Select the Quotation Employee Name" ValidationGroup="mids">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnPOLRpt" runat="server" OnClick="btnPOLRpt_Click" Text="Run Report"
                                ValidationGroup="mids" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="mids" />
                        </td>
                    </tr>
                </table>

                <table id="tblProforma" runat="server" border="0" cellpadding="0" cellspacing="0"
                    visible="false" width="600">
                    <tr>
                        <td class="profilehead" colspan="3" style="height: 20px; text-align: left">Customer Data Analysis</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label137" runat="server" Text="Requriment" Width="103px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlCompanyNameProforma" runat="server" AutoPostBack="True"
                                CausesValidation="True">
                                <asp:ListItem Value ="All">All</asp:ListItem>
                                <asp:ListItem Value ="Others">Others</asp:ListItem>
                            <asp:ListItem Value ="Renovation" >Renovation</asp:ListItem>
                            <asp:ListItem Value ="Replacement" >Replacement</asp:ListItem>
                            <asp:ListItem Value ="New Constructions" >New Constructions</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator122" runat="server" ControlToValidate="ddlCompanyNameProforma"
                                ErrorMessage="Please Select the Company Name " InitialValue="0" ValidationGroup="Proforma" Visible="False">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label38" runat="server" Text="From" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtProformaFrom" runat="server" CausesValidation="True" type="datepic" CssClass="datetext"
                                EnableTheming="False">
                            </asp:TextBox><%--<asp:Image ID="imgProformaFrom" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>&nbsp;--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator123" runat="server" ControlToValidate="txtProformaFrom"
                                ErrorMessage="Please Select the From Date " ValidationGroup="Proforma">*</asp:RequiredFieldValidator>
                            <%-- <cc1:CalendarExtender ID="CalendarExtender12" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                PopupButtonID="imgProformaFrom" TargetControlID="txtProformaFrom">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender15" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtProformaFrom"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label139" runat="server" Text="To" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtproformaTo" runat="server" CausesValidation="True" type="datepic" CssClass="datetext"
                                EnableTheming="False">
                            </asp:TextBox><%--<asp:Image ID="imgProformaTo" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>&nbsp;--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator126" runat="server" ControlToValidate="txtproformaTo"
                                ErrorMessage="Please Select the ToDate " InitialValue="0" ValidationGroup="Proforma">*</asp:RequiredFieldValidator>
                            <%--  <cc1:CalendarExtender ID="CalendarExtender13" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                PopupButtonID="imgProformaTo" TargetControlID="txtproformaTo">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender16" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtproformaTo"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label140" runat="server" Text="Looking For" Width="117px" ></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlDepartmentProforma" runat="server" AutoPostBack="True" CausesValidation="True"
                                meta:resourcekey="ddlDepartmentResource1" 
                                Width="154px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator84" runat="server" ControlToValidate="ddlDepartmentProforma"
                                ErrorMessage="Please select the Department Name " ValidationGroup="Proforma" Visible="False">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label141" runat="server" Text="Brand" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlEmployeeNameProforma" runat="server" AutoPostBack="True"
                                CausesValidation="True" Width="147px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator45" runat="server" ControlToValidate="ddlEmployeeNameProforma"
                                ErrorMessage="Please Select the Employee Name " InitialValue="0" ValidationGroup="Proforma" Visible="False">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label4" runat="server" Text="Reference" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlReference" runat="server" AutoPostBack="True"
                                CausesValidation="True" Width="147px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlReference"
                                ErrorMessage="Please Select the Reference Name " InitialValue="0" ValidationGroup="Proforma" Visible="False">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                                <td colspan="4">
                                    <table id="tblpRint" runat="server" visible="false">
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkOriginal" runat="server" OnCheckedChanged="chkOriginal_CheckedChanged"
                                                    Text="Pie Chart" AutoPostBack="True"></asp:CheckBox></td>
                                            <td>
                                                <asp:CheckBox ID="chkDuplicate" runat="server" Text="Bar Chart" AutoPostBack="True" OnCheckedChanged="chkDuplicate_CheckedChanged"></asp:CheckBox></td>
                                            <td>
                                                <asp:CheckBox ID="chktriplicate"  runat="server" Text="Brand Pie Chart" AutoPostBack="True" OnCheckedChanged="chktriplicate_CheckedChanged"></asp:CheckBox></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnInternal" runat="server" Text="Run Report"
                                ValidationGroup="Proforma" OnClick="btnInternal_Click" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;<asp:ValidationSummary ID="ValidationSummary28" runat="server" ValidationGroup="Proforma"></asp:ValidationSummary>
                        </td>
                    </tr>
                </table>
                <table id="tblShipment" runat="server" border="0" cellpadding="0" cellspacing="0"
                    width="600">
                    <tr>
                        <td class="profilehead" colspan="3" style="height: 20px; text-align: left">Shipment Details Statement</td>
                    </tr>
                    <tr>
                        <td style="height: 19px"></td>
                        <td style="height: 19px"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label22" runat="server" Text="Company Name" Width="103px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlCompanyNameShipment" runat="server" AutoPostBack="True"
                                CausesValidation="True">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlCompanyNameShipment"
                                ErrorMessage="Please Select the Company Name " InitialValue="0" ValidationGroup="shipment" Visible="False">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label23" runat="server" Text="From" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtShipmentFrom" runat="server" CausesValidation="True" type="datepic" CssClass="datetext"
                                EnableTheming="False">
                            </asp:TextBox><%--<asp:Image ID="imgShipmentFrom" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>&nbsp;--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtShipmentFrom"
                                ErrorMessage="Please Select the From Date " ValidationGroup="shipment">*</asp:RequiredFieldValidator>
                            <%--<cc1:CalendarExtender ID="CalendarExtender8" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                PopupButtonID="imgShipmentFrom" TargetControlID="txtShipmentFrom">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender7" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtShipmentFrom"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label24" runat="server" Text="To" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtShipmentTo" runat="server" CausesValidation="True" type="datepic" CssClass="datetext"
                                EnableTheming="False">
                            </asp:TextBox><%--<asp:Image ID="imgShipmentTo" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>&nbsp;--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtShipmentTo"
                                ErrorMessage="Please Select the ToDate " InitialValue="0" ValidationGroup="shipment">*</asp:RequiredFieldValidator>
                            <%--  <cc1:CalendarExtender ID="CalendarExtender9" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                PopupButtonID="imgShipmentTo" TargetControlID="txtShipmentTo">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender8" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtShipmentTo"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label25" runat="server" Text="Brand" Width="117px" ></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlBrand1" runat ="server"></asp:DropDownList> 
                            <asp:DropDownList ID="ddlDepartmentShipment" runat="server" AutoPostBack="True" CausesValidation="True"
                                meta:resourcekey="ddlDepartmentResource1" OnSelectedIndexChanged="ddlDepartmentShipment_SelectedIndexChanged"
                                Width="154px" Visible="False">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlDepartmentShipment"
                                ErrorMessage="Please select the Department Name " ValidationGroup="shipment" Visible="False">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label26" runat="server" Text="Employee Name" Width="117px" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlEmployeeNameShipment" runat="server" AutoPostBack="True"
                                CausesValidation="True" Width="147px" Visible="False">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlEmployeeNameShipment"
                                ErrorMessage="Please Select the Employee Name " InitialValue="0" ValidationGroup="shipment" Visible="False">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnShipment" runat="server" Text="Run Report"
                                ValidationGroup="shipment" OnClick="btnShipment_Click" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;<asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="shipment"></asp:ValidationSummary>
                        </td>
                    </tr>
                </table>
                <table id="tblPI" runat="server" border="0" cellpadding="0" cellspacing="0" visible="false"
                    width="600">
                    <tr>
                        <td class="profilehead" colspan="3" style="height: 20px; text-align: left">Purchase Invoice Statement</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label30" runat="server" Text="Company Name" Width="103px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlCompanyNamePurInvoice" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label31" runat="server" Text="From" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPiFrom" runat="server" type="datepic" CssClass="datetext" EnableTheming="False">
                            </asp:TextBox><%--<asp:Image ID="imgPiFrom" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>--%><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtPiFrom" ErrorMessage="Please Select the Sales Lead Date"
                                ValidationGroup="PI">*</asp:RequiredFieldValidator><asp:CustomValidator ID="CustomValidator9"
                                    runat="server" ClientValidationFunction="DateCustomValidate" ControlToValidate="txtPiFrom"
                                    ErrorMessage="Please Enter the Sales Lead Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                    SetFocusOnError="True" ValidationGroup="PI">*</asp:CustomValidator><%--<cc1:CalendarExtender
                                        ID="CalendarExtender10" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgPiFrom"
                                        TargetControlID="txtPiFrom">
                                    </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender11" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtPiFrom"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label32" runat="server" Text="To" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPiTo" runat="server" type="datepic" CssClass="datetext" EnableTheming="False">
                            </asp:TextBox><%--<asp:Image ID="imgPiTo" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>--%><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtPiTo" ErrorMessage="Please Select the Sales Lead Date"
                                ValidationGroup="PI">*</asp:RequiredFieldValidator><asp:CustomValidator ID="CustomValidator10"
                                    runat="server" ClientValidationFunction="DateCustomValidate" ControlToValidate="txtPiTo"
                                    ErrorMessage="Please Enter the Sales Lead Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                    SetFocusOnError="True" ValidationGroup="SalesLead">*</asp:CustomValidator><%--<cc1:CalendarExtender
                                        ID="CalendarExtender11" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgPiTo"
                                        TargetControlID="txtPiTo">
                                    </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender12" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtPiTo" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label33" runat="server" Text="Department" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlDeptPI" runat="server" AutoPostBack="True" meta:resourcekey="ddlDepartmentResource1"
                                OnSelectedIndexChanged="ddlDeptPI_SelectedIndexChanged" Width="154px">
                            </asp:DropDownList>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label34" runat="server" Text="Employee Name" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlEmployeeNamePI" runat="server" Width="147px">
                            </asp:DropDownList>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnPIStmt" runat="server"
                                Text="Run Report" ValidationGroup="SalesLead" OnClick="btnPIStmt_Click" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="ValidationSummary6" runat="server" ValidationGroup="SalesLead"></asp:ValidationSummary>
                        </td>
                    </tr>
                </table>

                <table id="tblReserveStockHistory" runat="server" border="0" cellpadding="0" cellspacing="0" visible="false"
                    width="600">

                    <tr>
                        <td class="profilehead" style="text-align: left">Sales Report</td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="5" cellspacing="0" >
        <tr>
            <td style="width:150px; text-align :right">Location Name:</td>
                 <td><asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource3" DataTextField="locname" DataValueField="locid">
                </asp:DropDownList>
                     <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [locid], [locname] FROM [location_tbl]"></asp:SqlDataSource>
                 </td>
                 <td style="text-align :right; width:150px  ">From</td>    
                 <td style="text-align:left"><asp:TextBox ID="txtfrom" runat ="server" type="datepic" ></asp:TextBox></td>        
                     
        </tr>
        <tr>
                 <td style="text-align :right" >Company Name : </td>
                 <td style="text-align :left "><asp:DropDownList ID="ddlComp" runat="server" AutoPostBack="True"></asp:DropDownList></td>   
                 <td style="text-align :right ">To</td>   
                 <td>
                     <asp:TextBox ID="txtTo" runat ="server" type="datepic" ></asp:TextBox>
                </td>       
        </tr>
                                <tr>
                                    <td style="text-align: right">
                            <asp:Label ID="Label2" runat="server" Text="Department :"  ></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="True" CausesValidation="True"
                                OnSelectedIndexChanged="ddlDept_SelectedIndexChanged"
                                 >
                            </asp:DropDownList>
                            </td>
                                <td style="text-align: right">
                            <asp:Label ID="Label3" runat="server" Text="Employee Name"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlEmp" runat="server">
                            </asp:DropDownList>
                           </td>
                                </tr>
                                <tr>
                                <td style="text-align :right" >Type of Sales : </td>
                                    <td style ="text-align :left ">
                                        <asp:DropDownList ID="ddlSaleType" runat ="server" >
                                            <asp:ListItem Value ="0">All</asp:ListItem>
                                            <asp:ListItem Value ="1">Purchase order</asp:ListItem>
                                            <asp:ListItem Value ="2">Sample & Cash</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
         <tr>
                <td><asp:Button ID="btnhai" runat ="server" Text="Run Report" OnClick ="btnMIDSRpt_Click" /></td>
                 
             </tr>
        </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>


 
