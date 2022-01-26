<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/SalesMP1.master" AutoEventWireup="true" CodeFile="Customer_Info_Log_Activity.aspx.cs" Inherits="Modules_SM_Customer_Info_Log_Activity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
<!--
    function Check_Click(objRef) {
        //Get the Row based on checkbox
        var row = objRef.parentNode.parentNode;

        //Get the reference of GridView
        var GridView = row.parentNode;

        //Get all input elements in Gridview
        var inputList = GridView.getElementsByTagName("input");

        for (var i = 0; i < inputList.length; i++) {
            //The First element is the Header Checkbox
            var headerCheckBox = inputList[0];

            //Based on all or none checkboxes
            //are checked check/uncheck Header Checkbox
            var checked = true;
            if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                if (!inputList[i].checked) {
                    checked = false;
                    break;
                }
            }
        }
        headerCheckBox.checked = checked;

    }
    function checkAll(objRef) {
        var GridView = objRef.parentNode.parentNode.parentNode;
        var inputList = GridView.getElementsByTagName("input");
        for (var i = 0; i < inputList.length; i++) {
            var row = inputList[i].parentNode.parentNode;
            if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                if (objRef.checked) {
                    inputList[i].checked = true;
                }
                else {
                    if (row.rowIndex % 2 == 0) {
                        //row.style.backgroundColor = "#C2D69B";
                    }
                    else {
                        //row.style.backgroundColor = "white";
                    }
                    inputList[i].checked = false;
                }
            }
        }
    }
    //-->
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table style ="width :100%">
        <tr>
                    <td style="text-align: center; font-size: medium;">
                       <asp:LinkButton ID="lnkPOAmen" runat="server" OnClick="lnkPOAmen_Click" Font-Underline="True">PO Amendments</asp:LinkButton>
                        &nbsp;||&nbsp;
                        <asp:LinkButton ID="lnkSalesReturn" runat="server" OnClick="lnkSalesReturn_Click" Font-Underline="True">Sales/Sample Return Note</asp:LinkButton>
                    </td>
                </tr>
    </table>
    <asp:Panel runat="server" ID="POAmendment">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>PO Amendment</td>
            <td>
                <asp:DropDownList ID="ddlNoOfRecords" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged">
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>75</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>

                </asp:DropDownList></td>
        </tr>
    </table>
<table style="width:100%;">
     <tr>
            <td id="TD1"></td>
            <td>
                <asp:Label ID="lblTtl" runat="server" Visible="false"></asp:Label></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td id="TD9" class="searchhead" colspan="4">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left" colspan="2">Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                            <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
                        </td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td style="height: 25px">
                                        <asp:Label ID="Label20" CssClass="label" runat="server" EnableTheming="False" Font-Bold="True" Text="Search By" meta:resourcekey="Label20Resource1" Height="16px"></asp:Label>

                                    </td>
                                    <td style="height: 25px">
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged" meta:resourcekey="ddlSearchByResource1">
                                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource1">--</asp:ListItem>
                                            <asp:ListItem Value="Amendment_No" meta:resourcekey="ListItemResource2">Amendment NO.</asp:ListItem>
                                            <asp:ListItem Value="Date" meta:resourcekey="ListItemResource3">Date</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME" meta:resourcekey="ListItemResource4">Customer Name</asp:ListItem>
                                            <%--<asp:ListItem Value="CUST_CONTACT_PERSON" meta:resourcekey="ListItemResource5">Contact Person</asp:ListItem>--%>
                                            <%--<asp:ListItem Value="SUP_EMAIL" meta:resourcekey="ListItemResource6">Enquiry From</asp:ListItem>--%>
                                        </asp:DropDownList></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList ID="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                            EnableTheming="False" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged"
                                            Visible="False" Width="50px" meta:resourcekey="ddlSymbolsResource1">
                                            <asp:ListItem Selected="True" meta:resourcekey="ListItemResource7">=</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource8">&lt;</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource9">&gt;</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource10">&lt;=</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource11">&gt;=</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource12">R</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="height: 25px">
                                        <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False" meta:resourcekey="lblCurrentFromDateResource1"></asp:Label>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchValueFromDate" runat="server" type="datepic" EnableTheming="True" Visible="False"
                                            Width="106px" meta:resourcekey="txtSearchValueFromDateResource1"></asp:TextBox></td>
                                    <td style="height: 25px">
                                        <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False" meta:resourcekey="lblCurrentToDateResource1"></asp:Label>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchValueToDate" runat="server" type="datepic" EnableTheming="True" Visible="False"
                                            Width="106px" meta:resourcekey="txtSearchValueFromDateResource1"></asp:TextBox></td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px" meta:resourcekey="txtSearchTextResource1"></asp:TextBox>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" meta:resourcekey="btnSearchGoResource1" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False" meta:resourcekey="lblEmpIdHiddenResource1"></asp:Label>
                            <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1" Visible="False"></asp:Label>
                            <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1" Visible="False"></asp:Label><asp:Label ID="lblSearchItemHidden" runat="server" Visible="False" meta:resourcekey="lblSearchItemHiddenResource1"></asp:Label>
                            <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False" meta:resourcekey="lblSearchTypeHiddenResource1"></asp:Label>
                            <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False" meta:resourcekey="lblSearchValueFromHiddenResource1"></asp:Label>
                            <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False" meta:resourcekey="lblSearchValueHiddenResource2"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td id="TD3" runat="server" colspan="4" style="text-align: center">
                <asp:GridView ID="gvProformaInvoice" runat="server" AllowPaging="True" AutoGenerateColumns="false"
                    Width="100%" DataSourceID="sqlPISearchDetails" SelectedRowStyle-BackColor="#c0c0c0"
                    AllowSorting="True" OnPageIndexChanging ="gvProformaInvoice_PageIndexChanging"
                    OnRowDataBound="gvProformaInvoice_RowDataBound" >
                    <Columns>
                        <asp:BoundField DataField="Amendment_Id" SortExpression="Amendment_Id" HeaderText="Amendment_Id" meta:resourceKey="BoundFieldResource1" />
                        <asp:TemplateField SortExpression="Amendment_NO" HeaderText="Amendmnet No" meta:resourceKey="TemplateFieldResource1">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("Amendment_NO") %>' ForeColor="#0066ff" ID="TextBox1" meta:resourceKey="TextBox1Resource1">"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnPINO" OnClick="lbtnPINO_Click" runat="server" ForeColor="#0066ff" Text='<%# Eval("Amendment_NO") %>' CausesValidation="False" meta:resourcekey="lbtnPINOResource1" __designer:wfdid="w1"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="DATE" SortExpression="DATE" HeaderText="Date" meta:resourceKey="BoundFieldResource2">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CUST_NAME" SortExpression="CUST_NAME" HeaderText="Customer Name" meta:resourceKey="BoundFieldResource3">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="SO_NO" SortExpression="SO_NO" HeaderText="SO No" meta:resourceKey="BoundFieldResource4">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="EMP_FIRST_NAME" SortExpression="EMP_FIRST_NAME" HeaderText="Prepared By" meta:resourceKey="BoundFieldResource11" />
                        <asp:BoundField DataField="CP_SHORT_NAME" SortExpression="CP_SHORT_NAME" HeaderText="Company" meta:resourceKey="BoundFieldResource5">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        <span style="color: #ff0000">No Record Found! </span>
                    </EmptyDataTemplate>
                </asp:GridView>
                <asp:SqlDataSource ID="sqlPISearchDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="[SP_Amendment_PO_SEARCH_SELECT]" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="EMPID" ControlID="lblEmpIdHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="CPID" ControlID="lblCPID" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td id="TD4" colspan="4" style="text-align: center">
                <table id="Table1" align="center">
                    <tr>
                        <td>
                            <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CausesValidation="False" meta:resourcekey="btnNewResource1" /></td>
                        <td>
                            <%--<asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CausesValidation="False" meta:resourcekey="btnEditResource1" />--%></td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" /></td>
                        <td><asp:Button ID="btnPrint" runat ="server" Text ="Print" OnClick ="btnPrint_Click" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    <tr>
        <td colspan ="4">
            <asp:UpdatePanel runat ="server"  >
                <ContentTemplate >

               
            <table id="tbldet" style ="width :100%" runat ="server" visible ="false" >
                <tr>
            <%--<asp:Label ID="lblCPID" runat ="server" Visible ="false"></asp:Label>--%>
            <td colspan ="4" style="text-align: left"  class ="profilehead "><h4>Amendment in the Purchase Order</h4></td>
        </tr>
    
    <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label3" runat="server" Text="Amendment No"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtInvoiceNo" runat="server" ReadOnly="True">
                                    </asp:TextBox><asp:Label ID="Label12" runat="server" EnableTheming="False" Font-Bold="False"
                                        Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                            ID="rfvInvoiceNo" runat="server" ControlToValidate="txtInvoiceNo" ErrorMessage="Please Enter the Delivery Challan No">*</asp:RequiredFieldValidator></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label6" runat="server" Text="Amendment Date"></asp:Label></td>
                                <td style="text-align: left; width: 439px;">
                                    <asp:TextBox ID="txtInvoiceDate"  runat="server" type="datepic">
                                    </asp:TextBox>&nbsp;
                            <asp:Label ID="Label15" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvInvoiceDate" runat="server" ControlToValidate="txtInvoiceDate"
                                        ErrorMessage="Please Enter the Delivery Challan No">*</asp:RequiredFieldValidator>
                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="DateCustomValidate"
                                        ControlToValidate="txtInvoiceDate" ErrorMessage="Please Enter the Invoice Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                        SetFocusOnError="True">*</asp:CustomValidator>
                                    <%--   <cc1:CalendarExtender Format="dd/MM/yyyy" ID="CeInvoiceDate" runat="server" Enabled="True"
                                PopupButtonID="imgInvoiceDate" TargetControlID="txtInvoiceDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MeeInvoiceDate" runat="server" DisplayMoney="Left" Enabled="True"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtInvoiceDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                                </td>
                            </tr>

    <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblSearch" runat="server" Text="Search"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtSearchModel" runat="server">
                                    </asp:TextBox><asp:Button ID="btnSearchModelNo" runat="server" BorderStyle="None"
                                        CausesValidation="False" CssClass="gobutton" EnableTheming="False" OnClick="btnSearchModelNo_Click"
                                        Text="Go" />
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                        SelectCommand="SP_Customer_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="txtSearchModel" Name="SearchValue" PropertyName="Text"
                                                Type="String" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                                <td style="text-align: right"></td>
                                <td style="text-align: left; width: 439px;"></td>
                            </tr>
     <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblCustomer" runat="server" Text="Customer Name"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomerName_SelectedIndexChanged">
                                    </asp:DropDownList>&nbsp;<asp:Label ID="Label16" runat="server" EnableTheming="False" Font-Bold="False"
                                        Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>&nbsp;<asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCustomerName"
                                            ErrorMessage="Please Enter the Customer Name" InitialValue="0" ValidationGroup="main">*</asp:RequiredFieldValidator><asp:TextBox ID="txtCustomerName" runat="server" ReadOnly="True" Visible="False"></asp:TextBox></td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblRegion" runat="server" Text="Region"></asp:Label></td>
                                <td style="text-align: left; width: 439px;">
                                    <asp:TextBox ID="txtRegion" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
    <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" ReadOnly="True">
                                    </asp:TextBox></td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblPhone" runat="server" Text="Phone"></asp:Label></td>
                                <td style="text-align: left; width: 439px;">
                                    <asp:TextBox ID="txtPhone" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblMobile" runat="server" Text="Mobile"></asp:Label></td>
                                <td style="text-align: left; width: 439px;">
                                    <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 5px;">
                                    <asp:Label ID="lblSalesOrderNo" runat="server" Text="Sales Order No" Width="101px"></asp:Label></td>
                                <td style="text-align: left; height: 5px;">
                                    <asp:DropDownList ID="ddlSalesOrderNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSalesOrderNo_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:Label ID="Label14" runat="server" EnableTheming="False" Font-Bold="False"
                                        Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSalesOrderNo"
                                        ErrorMessage="Please Enter the Sales Order No" InitialValue="0" ValidationGroup="main">*</asp:RequiredFieldValidator></td>
                                <td style="text-align: right; height: 5px;">
                                    <asp:Label ID="lblSalesOrderDate" runat="server" Text="Sales Order Date" Width="114px">
                                    </asp:Label></td>
                                <td style="text-align: left; height: 5px; width: 439px;">
                                    <asp:TextBox ID="txtSalesOrderDate" runat="server" ReadOnly="True" type="datepic">
                                    </asp:TextBox>
                                    
                                </td>
                            </tr>
                           <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblDeliveryAdd" runat="server" Text="Delivery Address"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtDeliveryAdd" runat="server" TextMode="MultiLine" ReadOnly="True">
                                    </asp:TextBox></td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblBillingAdd" runat="server" Text="Billing Address"></asp:Label></td>
                                <td style="text-align: left; width: 439px;">
                                    <asp:TextBox ID="txtBillAdd" TextMode="MultiLine" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
        <tr>
            <td colspan ="4" style="text-align: left"  class ="profilehead ">Amendment Item Selection</td>
        </tr>
        <tr>
           <td style="text-align: center" colspan="4">
               <asp:GridView ID="gvItemPriceUpdate" runat="server" ShowFooter="true" OnRowDataBound ="gvItemPriceUpdate_RowDataBound" AutoGenerateColumns="False" Width="100%" >
                   <FooterStyle ForeColor="#0066ff" />
                   <Columns >
                       <asp:BoundField DataField ="Slno" HeaderText ="Sl No" />
                       <asp:BoundField DataField ="ITEM_CODE" HeaderText ="Item Code" />
                       <asp:BoundField DataField ="ITEM_MODEL_NO" HeaderText ="Model No." />
                       <asp:BoundField DataField ="ITEM_SPEC" HeaderText ="Description" />
                       <asp:BoundField DataField ="SO_DET_QTY" HeaderText ="PO Qty" />
                       <asp:BoundField DataField ="SO_RATE" HeaderText ="Price" />
                       <asp:BoundField DataField ="SO_DET_PRICE" HeaderText ="Amount" />
                       <asp:BoundField  HeaderText ="GST" />
                       <asp:TemplateField HeaderText="Replacement ModelNo.">
                            <ItemTemplate>
                                <asp:TextBox ID="txtNewModelNo" TextMode ="MultiLine" runat="server" Text='<%#Bind("NewModel_No")%>' ></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Replacement Reason">
                            <ItemTemplate>
                                <asp:TextBox ID="txtReason" TextMode ="MultiLine"  runat="server" Text='<%#Bind("Reason")%>' ></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Qty">
                            <ItemTemplate>
                                <asp:TextBox ID="txtNewQty" AutoPostBack ="true" Text='<%#Bind("Qty")%>'  runat="server" OnTextChanged ="txtNewQty_TextChanged" ></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Price">
                            <ItemTemplate>
                                <asp:TextBox ID="txtNewPrice" AutoPostBack ="true" Text='<%#Bind("Rate")%>'  runat="server" OnTextChanged ="txtNewPrice_TextChanged" ></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                               <asp:Label ID="lblNewAmt" runat ="server" Text='<%#Bind("Amount")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="GST">
                            <ItemTemplate>
                               <asp:Label ID="lblNewGst" runat ="server" Text='<%#Bind("GST")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      <%-- <asp:BoundField DataField ="BlockedQty" HeaderText ="Blocked Qty" />
                       <asp:BoundField DataField ="IND_DET_QTY" HeaderText ="Ind Qty" />
                       <asp:BoundField DataField ="FPO_DET_QTY" HeaderText ="FPO Qty" />
                       <asp:BoundField DataField ="PI_DET_QTY" HeaderText ="PI Qty" />--%>
                       <asp:BoundField DataField ="BalanceQty" HeaderText ="Bal Qty" />
                   </Columns>
               </asp:GridView>
           </td>
        </tr>
    <tr>
                                <td style="text-align: right;"></td>
                                <td style="text-align: left;"></td>
                                <td style="text-align: right;"></td>
                                <td style="text-align: left; width: 439px;"></td>
                            </tr>
            <tr>
                                <td colspan="4">
                                    <table id="tblbuttons" >
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="main" /></td>
                                            
                                                </tr>
                                    </table>
                                </td>
                </tr>
            </table>
                    </ContentTemplate>
            </asp:UpdatePanel>
        </td>
                     
    </tr>
        
        
    </table>
        </asp:Panel> 
    <asp:Panel runat ="server" ID ="ReturnNote" Visible ="false" >
        
        <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>Sales Return Note</td>
            <td>
                <asp:DropDownList ID="ddlReturnNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlReturnNo_SelectedIndexChanged">
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>75</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>

                </asp:DropDownList></td>
        </tr>
    </table>
        <table style="width:100%;">
            <tr>
            <td id="TD2"></td>
            <td>
                <asp:Label ID="lblReturnTtl" runat="server" Visible="false"></asp:Label></td>
            <td></td>
            <td></td>
        </tr>
             <tr>
            <td id="TD9" class="searchhead" colspan="4">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left" colspan="2">Go To Page :
                                    <asp:TextBox ID="TextBox2" Width="100px" runat="server"></asp:TextBox>
                            <asp:Button ID="Button1" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
                        </td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td style="height: 25px">
                                        <asp:Label ID="Label1" CssClass="label" runat="server" EnableTheming="False" Font-Bold="True" Text="Search By" meta:resourcekey="Label20Resource1" Height="16px"></asp:Label>

                                    </td>
                                    <td style="height: 25px">
                                        <asp:DropDownList ID="ddlReturnSearch" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlReturnSearch_SelectedIndexChanged" meta:resourcekey="ddlSearchByResource1">
                                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource1">--</asp:ListItem>
                                            <asp:ListItem Value="SR_NO" meta:resourcekey="ListItemResource2">Return NO.</asp:ListItem>
                                            <asp:ListItem Value="Date" meta:resourcekey="ListItemResource3">Date</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME" meta:resourcekey="ListItemResource4">Customer Name</asp:ListItem>
                                            <%--<asp:ListItem Value="CUST_CONTACT_PERSON" meta:resourcekey="ListItemResource5">Contact Person</asp:ListItem>--%>
                                            <%--<asp:ListItem Value="SUP_EMAIL" meta:resourcekey="ListItemResource6">Enquiry From</asp:ListItem>--%>
                                        </asp:DropDownList></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList ID="ddlSymbols1" runat="server" AutoPostBack="True" CssClass="textbox"
                                            EnableTheming="False" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged"
                                            Visible="False" Width="50px" meta:resourcekey="ddlSymbolsResource1">
                                            <asp:ListItem Selected="True" meta:resourcekey="ListItemResource7">=</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource8">&lt;</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource9">&gt;</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource10">&lt;=</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource11">&gt;=</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource12">R</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="height: 25px">
                                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="From" Visible="False" meta:resourcekey="lblCurrentFromDateResource1"></asp:Label>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="TextBox3" runat="server" type="datepic" EnableTheming="True" Visible="False"
                                            Width="106px" meta:resourcekey="txtSearchValueFromDateResource1"></asp:TextBox></td>
                                    <td style="height: 25px">
                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="To " Visible="False" meta:resourcekey="lblCurrentToDateResource1"></asp:Label>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="TextBox4" runat="server" type="datepic" EnableTheming="True" Visible="False"
                                            Width="106px" meta:resourcekey="txtSearchValueFromDateResource1"></asp:TextBox></td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="TextBox5" runat="server" EnableTheming="True" Width="118px" meta:resourcekey="txtSearchTextResource1"></asp:TextBox>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Button ID="btnReturnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" OnClick="btnReturnSearchGo_Click" Text="Go" meta:resourcekey="btnSearchGoResource1" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="Label5" runat="server" Visible="False" meta:resourcekey="lblEmpIdHiddenResource1"></asp:Label>
                            <asp:Label ID="Label7" runat="server" meta:resourcekey="lblSearchValueHiddenResource1" Visible="False"></asp:Label>
                            <asp:Label ID="Label8" runat="server" meta:resourcekey="lblSearchValueHiddenResource1" Visible="False"></asp:Label><asp:Label ID="Label9" runat="server" Visible="False" meta:resourcekey="lblSearchItemHiddenResource1"></asp:Label>
                            <asp:Label ID="Label10" runat="server" Visible="False" meta:resourcekey="lblSearchTypeHiddenResource1"></asp:Label>
                            <asp:Label ID="Label11" runat="server" Visible="False" meta:resourcekey="lblSearchValueFromHiddenResource1"></asp:Label>
                            <asp:Label ID="Label13" runat="server" Visible="False" meta:resourcekey="lblSearchValueHiddenResource2"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
            <tr>
                                    <td colspan="3">
                                        <asp:GridView ID="gvSalesReturnDetails" SelectedRowStyle-BackColor="#c0c0c0" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            OnRowDataBound="gvSalesReturnDetails_RowDataBound" Width="100%" DataSourceID="SqlDataSource2" AllowSorting="True">
                                            <Columns>
                                                <asp:BoundField DataField="SR_ID" SortExpression="SR_ID" HeaderText="SalesReturnIdHidden"></asp:BoundField>
                                                <asp:TemplateField HeaderText="DC No" SortExpression="DC_NO">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnDCNo" OnClick="lbtnDCNo_Click" ForeColor="#0066ff" runat="server" Text='<%# Bind("DC_NO") %>' CausesValidation="False" __designer:wfdid="w4"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="SR_NO" HeaderText="Sales Return No">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("SR_NO") %>' __designer:wfdid="w27"></asp:TextBox>
                                                    </EditItemTemplate>

                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnSalesReturnNo" OnClick="lbtnSalesReturnNo_Click" ForeColor="#0066ff" runat="server" Text='<%# Bind("SR_NO") %>' CausesValidation="False" __designer:wfdid="w23"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HtmlEncode="False" SortExpression="SR_DATE" DataFormatString="{0:dd/MM/yyyy}" DataField="SR_DATE" HeaderText="Return Date"></asp:BoundField>
                                                <asp:BoundField DataField="DC_ID" SortExpression="DC_ID" HeaderText="DCIdHidden"></asp:BoundField>
                                                <asp:BoundField DataField="CUST_NAME" SortExpression="CUST_NAME" HeaderText="Customer Name">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:BoundField>
                                                <%--<asp:BoundField DataField="SRGrossAmt" SortExpression="SI_GROSS_AMT" HeaderText="Amount"></asp:BoundField>--%>
                                                <asp:BoundField DataField="EMP_FIRST_NAME" SortExpression="Prepared_By" HeaderText="Prepared By"></asp:BoundField>
                                                <%--<asp:BoundField DataField="Approved_By" SortExpression="Approved_By" HeaderText="Approved By"></asp:BoundField>--%>
                                                <asp:BoundField DataField="CP_SHORT_NAME" SortExpression="CP_SHORT_NAME" HeaderText="COMPANY"></asp:BoundField>

                                            </Columns>
                                            <EmptyDataTemplate>
                                                <span style="color: #ff0000">No Data Exist</span>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                        <asp:SqlDataSource id="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                            SelectCommand="SP_INVENTORY_RETURNNOTE_SEARCH_SELECT" SelectCommandType="StoredProcedure"><selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
</selectparameters></asp:SqlDataSource>
                                    </td>
                                </tr>
            <tr>
            <td id="TD6" colspan="4" style="text-align: center">
                <table id="tbl" align="center">
                    <tr>
                        <td>
                            <asp:Button ID="btnReturnNew" runat="server" Text="New" OnClick="btnReturnNew_Click" CausesValidation="False" meta:resourcekey="btnNewResource1" /></td>
                        <td><asp:Button ID="btnReturnPrint" runat ="server" Text ="Print" OnClick ="btnReturnPrint_Click"/></td>
                        <td>
                            <%--<asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CausesValidation="False" meta:resourcekey="btnEditResource1" />--%></td>
                        <td>
                            <asp:Button ID="btnReturnDelete" runat="server" Text="Delete" OnClick="btnReturnDelete_Click" /></td>
                        
                    </tr>
                </table>
            </td>
        </tr>
            <tr>
 <td colspan ="4">
            <asp:UpdatePanel runat ="server"  >
                <ContentTemplate >
                    <table id="tblreturnDet" style ="width :100%" runat ="server" visible ="false"  >
                         <tr>
            
            <td colspan ="4" style="text-align: left"  class ="profilehead ">
                <asp:Label ID="lblReturnCPID" runat ="server" Visible ="false"></asp:Label><h4>Sales Return Note</h4></td>
        </tr>
                        <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label17" runat="server" Text="Return Note No"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtReturnNo" runat="server" ReadOnly="True">
                                    </asp:TextBox><asp:Label ID="Label18" runat="server" EnableTheming="False" Font-Bold="False"
                                        Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtReturnNo" ErrorMessage="Please Enter the Delivery Challan No">*</asp:RequiredFieldValidator></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label19" runat="server" Text="Return Note Date"></asp:Label></td>
                                <td style="text-align: left; width: 439px;">
                                    <asp:TextBox ID="txtReturnDt"  runat="server" type="datepic">
                                    </asp:TextBox>&nbsp;
                            <asp:Label ID="Label21" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtInvoiceDate"
                                        ErrorMessage="Please Enter the Delivery Challan No">*</asp:RequiredFieldValidator>
                                    <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="DateCustomValidate"
                                        ControlToValidate="txtReturnDt" ErrorMessage="Please Enter the Invoice Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                        SetFocusOnError="True">*</asp:CustomValidator>
                                    <%--   <cc1:CalendarExtender Format="dd/MM/yyyy" ID="CeInvoiceDate" runat="server" Enabled="True"
                                PopupButtonID="imgInvoiceDate" TargetControlID="txtInvoiceDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MeeInvoiceDate" runat="server" DisplayMoney="Left" Enabled="True"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtInvoiceDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                                </td>
                            </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lblCustSearch" runat ="server" Text ="Customer Search" ></asp:Label>
                            </td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtCusSearch" runat ="server" ></asp:TextBox>
                                <asp:Button ID="btnReturnCustSearch" runat="server" BorderStyle="None"
                                        CausesValidation="False" CssClass="gobutton" EnableTheming="False" OnClick="btnReturnCustSearch_Click"
                                        Text="Go" />
                                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                        SelectCommand="SP_Customer_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="txtCusSearch" Name="SearchValue" PropertyName="Text"
                                                Type="String" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                            </td>

                        </tr>
                        <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label22" runat="server" Text="Customer Name"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlReturnCustName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlReturnCustName_SelectedIndexChanged">
                                    </asp:DropDownList>&nbsp;<asp:Label ID="Label23" runat="server" EnableTheming="False" Font-Bold="False"
                                        Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>&nbsp;<asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlReturnCustName"
                                            ErrorMessage="Please Enter the Customer Name" InitialValue="0" ValidationGroup="main">*</asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtCust" runat="server" ReadOnly="True" Visible="False"></asp:TextBox></td>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label24" runat="server" Text="Region"></asp:Label></td>
                                <td style="text-align: left; width: 439px;">
                                    <asp:TextBox ID="txtRegn" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label25" runat="server" Text="Address"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtCustAdd" runat="server" TextMode="MultiLine" ReadOnly="True">
                                    </asp:TextBox></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label26" runat="server" Text="Phone"></asp:Label></td>
                                <td style="text-align: left; width: 439px;">
                                    <asp:TextBox ID="txtCustPhn" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
                        <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label27" runat="server" Text="Email"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtCustEmail" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label28" runat="server" Text="Mobile"></asp:Label></td>
                                <td style="text-align: left; width: 439px;">
                                    <asp:TextBox ID="txtCustMbl" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 5px;">
                                    <asp:Label ID="Label29" runat="server" Text="DC No" Width="101px"></asp:Label></td>
                                <td style="text-align: left; height: 5px;">
                                    <asp:DropDownList ID="ddlDCNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDCNo_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:Label ID="Label30" runat="server" EnableTheming="False" Font-Bold="False"
                                        Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSalesOrderNo"
                                        ErrorMessage="Please Enter the Sales Order No" InitialValue="0" ValidationGroup="main">*</asp:RequiredFieldValidator></td>
                                <td style="text-align: right; height: 5px;">
                                    <asp:Label ID="Label31" runat="server" Text="Sales Order Date" Width="114px">
                                    </asp:Label></td>
                                <td style="text-align: left; height: 5px; width: 439px;">
                                    <asp:TextBox ID="txtDCDt" runat="server" ReadOnly="True" type="datepic">
                                    </asp:TextBox>
                                    
                                </td>
                            </tr>
                           <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label32" runat="server" Text="Delivery Address"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtDelAdd" runat="server" TextMode="MultiLine" ReadOnly="True">
                                    </asp:TextBox></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label33" runat="server" Text="Billing Address"></asp:Label></td>
                                <td style="text-align: left; width: 439px;">
                                    <asp:TextBox ID="txtBillingAdd" TextMode="MultiLine" runat="server" ReadOnly="True">
                                    </asp:TextBox></td>
                            </tr>
                        <tr>
                            <td style="text-align: right">
                                    <asp:Label ID="Label34" runat="server" Text="Reason for return"></asp:Label></td>
                                <td style="text-align: left" colspan ="2">
                                    <asp:TextBox ID="txtReason" Width ="500px"   runat="server"  TextMode="MultiLine">
                                    </asp:TextBox></td>
                        </tr>
                        <tr>
                                <td colspan="4" style="text-align: center"></td>
                            </tr>
                            <tr>
                                <td class="profilehead" colspan="4" style="text-align: left">Delivery Challan Extra Items</td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <asp:GridView ID="gvExtraItems" runat="server" AutoGenerateColumns="False"
                                        Width="100%">
                                        <Columns>
                                             <asp:TemplateField>
        <HeaderTemplate>
            <asp:CheckBox ID="chkAll" runat="server" onclick = "checkAll(this);" 
            AutoPostBack = "true"  OnCheckedChanged = "CheckBox_CheckChanged"/>
        </HeaderTemplate> 
        <ItemTemplate>
            <asp:CheckBox ID="chk" runat="server" onclick = "Check_Click(this)" 
            AutoPostBack = "true"  OnCheckedChanged = "CheckBox_CheckChanged" />
        </ItemTemplate>
    </asp:TemplateField>
                                            <asp:BoundField DataField="DC No" HeaderText="DC No"></asp:BoundField>
                                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                            <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                                            <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                            <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                            
                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
                                            <%--<asp:BoundField DataField="GST_Tax" HeaderText="GST(*)"></asp:BoundField>--%>
                                           
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <span style="color: #ff0033">No Data to Dispaly</span>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                        <tr>
                            <td colspan="4" style="text-align: center"></td></tr>
                             <tr>
                                <td class="profilehead" colspan="4" style="text-align: left">Add Return Items</td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <asp:GridView ID="gvitems" runat="server" AutoGenerateColumns="False"
                                        Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="DC No" HeaderText="DC No"></asp:BoundField>
                                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                            <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                                            <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                            <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                            <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDetQty" runat="server" Text='<%# Bind("Quantity") %>' AutoPostBack="true"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                            <%--<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>--%>
                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
                                            
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <span style="color: #ff0033">No Data to Dispaly</span>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                         <tr>
                                <td class="profilehead" colspan="4" style="text-align: left; height: 14px;">Reference Details</td>
                            </tr>
                        <tr>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                                <td style="text-align: right"></td>
                                <td style="text-align: left; width: 439px;"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                                    </asp:DropDownList></td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By" ></asp:Label></td>
                                <td style="text-align: left; width: 439px;">
                                    <asp:DropDownList ID="ddlApprovedBy" runat="server"  >
                                    </asp:DropDownList></td>
                            </tr>
                        <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label35" runat="server" Text="Recived By"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlRecivedBy" runat="server" >
                                    </asp:DropDownList></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label36" runat="server" Text="Authorised By" ></asp:Label></td>
                                <td style="text-align: left; width: 439px;">
                                    <asp:DropDownList ID="ddlAuthorisedBy" runat="server"  >
                                    </asp:DropDownList></td>
                            </tr>
                        <tr>
            <td id="TD7" colspan="4" style="text-align: center">
                <table id="tbl1" align="center">
                    <tr>
                        <td>
                            <asp:Button ID="btnReturnSave" runat="server" Text="Save" OnClick="btnReturnSave_Click" CausesValidation="False" meta:resourcekey="btnNewResource1" /></td>
                        
                    </tr>
                </table>
            </td>
        </tr>
                    </table>
                    </ContentTemplate>
                </asp:UpdatePanel> 
     </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>


 
