<%@ Page Title="|| Value App : Warehouse : Damage Report ||" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="DamageReport.aspx.cs" Inherits="Modules_Warehouse_DamageReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .InvoiceTextBox {
            width: 80px !important;
        }
    </style>
    
    <script>
        function SellectAll(e) {
            $('#<%=gvInvoiceItems.ClientID%> tr').find('td:eq(0)').find('input[type="checkbox"]').prop('checked', e.childNodes[0].checked);
        }
    </script>
    <script>
        function SellectAllDaily(e) {
            // alert(e.childNodes[0].checked);
            $('#<%=gvDailyReport.ClientID%> tr').find('td:eq(0)').find('input[type="checkbox"]').prop('checked', e.childNodes[0].checked);
        }
    </script>
    <script>
        function SellectAllDamage(e) {
            // alert(e.childNodes[0].checked);
            $('#<%=gvDamageReport.ClientID%> tr').find('td:eq(0)').find('input[type="checkbox"]').prop('checked', e.childNodes[0].checked);
        }
    </script>
    <script language="javascript" type="text/javascript">
        function PrintDivContent(dmgReport) {
            var printContent = document.getElementById(dmgReport);
            var WinPrint = window.open('', '', 'left=0,top=0,toolbar=0,sta­tus=0');
            WinPrint.document.write(printContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
        }
    </script>
    <script type="text/javascript">
        // Let's use a lowercase function name to keep with JavaScript conventions
        function selectAll(invoker) {
            // Since ASP.NET checkboxes are really HTML input elements
            //  let's get all the inputs 
            var inputElements = document.getElementsByTagName('input');

            for (var i = 0 ; i < inputElements.length ; i++) {
                var myElement = inputElements[i];

                // Filter through the input types looking for checkboxes
                if (myElement.type === "checkbox") {

                    // Use the invoker (our calling element) as the reference 
                    //  for our checkbox status
                    myElement.checked = invoker.checked;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="text-align: center; font-weight: bold;">&nbsp;
                            <asp:LinkButton ID="lnkDamageReport" runat="server" OnClick="lnkDamageReport_Click" Font-Underline="True">Damage Report</asp:LinkButton>
                &nbsp;||&nbsp;
                            <asp:LinkButton ID="lnkDailyReport" runat="server" OnClick="lnkDailyReport_Click" Font-Underline="True">Daily Report</asp:LinkButton>

            </td>
        </tr>
    </table>
    <br />
    <asp:Panel runat="server" ID="pnlDamageReport" Visible="false">
        <table style="width: 100%">
            <tr class="pagehead">
                <td style="text-align: left">Damage Report
                </td>
            </tr>
        </table>
        <br />
        <div id="divInvoice">
            <div id="divSubmit" style="float: left; display: inline;">
                <asp:Button Text="Submit Report" ID="btnSubmitReport" OnClick="btnSubmitReport_Click" runat="server" />
            </div>
            <br />
            <table style="float: left; display: none;">
                <tr>
                    <td>Enter Invoice No:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtInvoice" /></td>
                    <td>
                        <asp:Button Text="Get Invoice Items" ID="btnInvoiceItems" OnClick="btnInvoiceItems_Click" runat="server" /></td>
                </tr>
            </table>
        </div>

        <br />
        <table>
            <tr>
                <td>Report Type:</td>
                <td>
                    <asp:RadioButton ID="rbnDamage" Text="Damage" runat="server" Checked="true" GroupName="dmg" />
                    <asp:RadioButton ID="rbnDefect" Text="Defective" runat="server" GroupName="dmg" />
                </td>
                <td></td>
                <td></td>
            </tr>
        </table>
        <br />
        <div id="divInvoiceItems">
            <asp:GridView runat="server" ID="gvInvoiceItems" AutoGenerateColumns="false" CssClass="InvoiceItems" >
                <Columns>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox Text="All" runat="server" ID="chkAll" Width="25px"  />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chk" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Invoice No.">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtInvoiceNo" Text='<%#Eval("Invoice No") %>' CssClass="InvoiceTextBox" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Model No">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtModelNo" CssClass="InvoiceTextBox" Text='<%#Eval("Model No") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Brand">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtBrand" CssClass="InvoiceTextBox" Text='<%#Eval("Brand") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Category">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtCategory" CssClass="InvoiceTextBox" Text='<%#Eval("Category") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Sub Category">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtSubCategory" CssClass="InvoiceTextBox" Text='<%#Eval("Sub Category") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Quantity as per Invoice">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtQuantity" CssClass="InvoiceTextBox" Text='<%#Eval("Quantity") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Colour">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtColor" CssClass="InvoiceTextBox" Text='<%#Eval("Color") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Damage/Defective Qty">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtDamage" CssClass="InvoiceTextBox" Text='<%#Eval("Damage") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Excess">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtExcess" CssClass="InvoiceTextBox" Text='<%#Eval("Excess") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Shortage">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtShortage" CssClass="InvoiceTextBox" Text='<%#Eval("Shortage") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Width="125px" Text='<%#Eval("Remarks") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--<asp:TemplateField HeaderText ="Upload Images">
                        <ItemTemplate >
                            <asp:DataList ID="DLAttachments1" runat="server" DataKeyField="msgattid">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%# Eval("attfilename", "~/Content/messagesAttachments/{0}") %>' Target="_blank" Text='<%# Eval("attname") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:DataList>
                           <asp:SqlDataSource ID="msgsattachsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT * FROM [msgs_Attachments_tbl]"></asp:SqlDataSource>

                            <asp:FileUpload ID="fileupload1" runat ="server" Width ="100px"/>

                             &nbsp;<asp:Button ID="btnAttach1" CommandName ="attach" runat="server" EnableTheming ="false" CssClass ="loginbutton " Text="Attach" />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
            <asp:Button Text="Add New Row" ID="btnAddNewRow" OnClick="btnAddNewRow_Click" runat="server" />
            <asp:Button Text="Delete Row" runat="server" ID="btnDeleteRow" OnClick="btnDeleteRow_Click" />
        </div>
        <br />
        <div id="grdDiv" runat="server" style="width: 100%">
            <table style="width: 100%">
                <tr class="profilehead">
                    <td>Damage Report Details
                    </td>
                    
                </tr>
            </table>
            <br />
            <table style="width: 100%">
                <tr>
                    <td colspan ="2"></td>
                    <td colspan ="2" style="text-align: right">No Of Records :
                            <asp:DropDownList ID="ddlNoOfRecords" runat="server" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged">
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>75</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                </tr>
                <tr>
                    <td style="text-align: right">Invoice No :    
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtInvNo" runat="server"></asp:TextBox>

                    </td>
                    <td style="width: 5%">&nbsp;
                    </td>
                    <td colspan="2">Model No :<asp:TextBox ID="txtModelNo" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">To Date :    
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtToDate" type="datepic" runat="server"></asp:TextBox>

                    </td>
                    <td style="width: 5%">&nbsp;
                    </td>
                    <td colspan="2">From Date :<asp:TextBox ID="txtFromDate" type="datepic" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                    </td>
                    <td colspan="2">
                        <asp:Button ID="btnPrint" runat="server" Text="Print" Visible ="false"  OnClientClick="javascript:PrintDivContent('dmgReport');" />
                            <asp:Button ID="btnExportExcel" runat="server" OnClick ="btnExportExcel_Click" Text="Export to Excel"  />

                    </td>
                </tr>
                
            </table>
            <br />
            <table style="width: 100%">
                <tr>
                    <td><asp:Button ID="btnPostComment" runat="server" Text="Post Comment" OnClick="btnPostComment_Click" CausesValidation="False"  />
                            <asp:Button ID="btnFollowUp" runat="server" CausesValidation="False" Text="Follow Up" OnClick="btnFollowUp_Click"  /></td>
                </tr>
                <tr>
                    <td>
                        <div id="dmgReport">
                
                            <asp:GridView ID="gvDamageReport" Width="100%" OnPageIndexChanging ="gvDamageReport_PageIndexChanging"  AllowPaging="True" OnRowDataBound ="gvDamageReport_RowDataBound" runat="server">
                                <Columns >
                                   <%-- <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkhdr" runat="server"  OnClick="selectAll(this)" />
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="Chk"  />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" onclick="Check_Click(this)" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comments">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtComment" TextMode="Multiline" Width="150px" Text='<%#Eval("Barcode")%>' runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Form Id" SortExpression="DAILYREPORTID" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblId" Text='<%#Eval("ItemID")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlDailyReport" Visible="false">
        <table style="width: 100%">
            <tr class="pagehead">
                <td style="text-align: left">Daily Report
                </td>
            </tr>
        </table>
        <div id="divInvoiceDaily">
            <div id="divSubmitDaily" style="float: left; display: inline;">
                <asp:Button Text="Submit Report" ID="btnSubmitReportDaily" OnClick="btnSubmitReportDaily_Click" runat="server" />
            </div>
            <br />
        </div>
        <br />
        <table>
            <tr>
                <td>Report Type:</td>
                <td>
                    <asp:RadioButton ID="rbnDaily" Text="Daily Report" runat="server" Checked="true" GroupName="daily" />
                </td>
                <td></td>
                <td></td>
            </tr>
        </table>
        <br />

        <div id="divInvoiceItemsDaily">
            <asp:GridView runat="server" ID="gvDailyReport" AutoGenerateColumns="false" CssClass="InvoiceItems">
                <Columns>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox Text="All" runat="server" ID="chkAll" Width="25px" onchange="javscript:SellectAllDaily(this)"  OnClick="selectAll(this)" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chk" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Invoice No.">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtInvoiceNo" Text='<%#Eval("Invoice No") %>' CssClass="InvoiceTextBox" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Model No">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtModelNo" CssClass="InvoiceTextBox" Text='<%#Eval("Model No") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Brand">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtBrand" CssClass="InvoiceTextBox" Text='<%#Eval("Brand") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Item Description">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtItemDescription" CssClass="InvoiceTextBox" Text='<%#Eval("Description") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Supplier Name">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtSupplier" CssClass="InvoiceTextBox" Text='<%#Eval("Supplier") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Quantity as per Invoice">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtQuantity" CssClass="InvoiceTextBox" Text='<%#Eval("Quantity") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Colour">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtColor" CssClass="InvoiceTextBox" Text='<%#Eval("Color") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Excess">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtExcess" CssClass="InvoiceTextBox" Text='<%#Eval("Excess") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Shortage">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtShortage" CssClass="InvoiceTextBox" Text='<%#Eval("Shortage") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Width="125px" Text='<%#Eval("Remarks") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Button Text="Add New Row" ID="btnAddNewRowDaily" OnClick="btnAddNewRowDaily_Click" runat="server" />
            <asp:Button Text="Delete Row" runat="server" ID="btnDeleteNewRowDaily" OnClick="btnDeleteNewRowDaily_Click" />
        </div>
        <br />
        <div id="Div1" runat="server" style="width: 100%">
            <table style="width: 100%">
                <tr class="profilehead">
                    <td>Daily Report Details
                    </td>
                </tr>
            </table>
            <br />
            <table style="width: 100%">
                <tr>
                    <td style="text-align: right">Invoice No :    
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtInvNoDaily" runat="server"></asp:TextBox>

                    </td>
                    <td style="width: 5%">&nbsp;
                    </td>
                    <td colspan="2">Model No :<asp:TextBox ID="txtModelNoDaily" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">To Date :    
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtFromDateDaily" type="date" runat="server"></asp:TextBox>

                    </td>
                    <td style="width: 5%">&nbsp;
                    </td>
                    <td colspan="2">From Date :<asp:TextBox ID="txtToDateDaily" type="date" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right">
                        <asp:Button ID="btnSearchDaily" runat="server" Text="Search" OnClick="btnSearchDaily_Click" />
                    </td>
                    <td colspan="2">
                        <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click" />
                    </td>
                </tr>
            </table>
            <br />
            <table style="width: 100%">
                <tr>
                    <td>
                        <div id="dailyReport">
                            <asp:GridView ID="gvDailyReportSearch" Width="100%" runat="server" AllowPaging ="true" OnRowDataBound ="gvDailyReportSearch_RowDataBound" OnPageIndexChanging ="gvDailyReportSearch_PageIndexChanging">
                                <Columns >
                                    <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkhdr" runat="server" AutoPostBack="True" OnClick="selectAll(this)" />
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="Chk" OnCheckedChanged="Chk_CheckedChanged" AutoPostBack="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comments">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtComment" TextMode="Multiline" Width="150px" Text='<%#Eval("Barcode")%>' runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Form Id" SortExpression="DAILYREPORTID" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblId" Text='<%#Eval("ItemID")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
</asp:Content>


 
