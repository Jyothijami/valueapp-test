<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="StockMovement.aspx.cs" Inherits="Modules_Warehouse_StockMovement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

    <table class="stacktable">
        <tr>
            <td colspan="4" class="profilehead">Moving form / Internal DC</td>
            <td style="text-align: right">
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
    <table class="stacktable">
        <tr>
            <td>
                <table class="stacktable">
                    <tr>
                        <td style="text-align: left" colspan="2">Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                            <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                Text="Search By"></asp:Label>
                            <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="INT_DC_NO">DC No.</asp:ListItem>
                                <asp:ListItem Value="IND_NO">Indent No.</asp:ListItem>
                                <asp:ListItem Value="MOVINGDC_DATE">Moving Date</asp:ListItem>
                                <asp:ListItem Value="ITEM_MODEL_NO">Model No.</asp:ListItem>
                            </asp:DropDownList>
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
                            <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                            </asp:Label>
                            <asp:TextBox ID="txtSearchValueFromDate" type="datepic" runat="server" EnableTheming="True" Visible="False"
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
                            <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                            </asp:Label>
                            <asp:TextBox ID="txtSearchValueToDate" runat="server" type="datepic" EnableTheming="True" Visible="False"
                                    Width="106px">
                                </asp:TextBox>
                            <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True">
                            </asp:TextBox><asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                Visible="False"></asp:Image>
                            <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchValueToDate" runat="server"
                                Enabled="False" PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                            <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnSearchGo_Click" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>

            </td>
        </tr>
    </table>
    <table class="stacktable">
        <tr>
            <td colspan="4">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvInternalDCDtls" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    Width="100%" AllowSorting="True" PageSize="8" DataSourceID="SqlDataSource3" OnRowDataBound="gvInternalDCDtls_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="SM_DC_ID" SortExpression="SM_DC_ID" HeaderText="DC Id"></asp:BoundField>
                        <asp:TemplateField HeaderText="DC No" SortExpression="FPO_NO">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("INT_DC_NO") %>' ID="TextBox1"></asp:TextBox>

                            </EditItemTemplate>

                            <ItemStyle Width="100px" HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle Width="100px" HorizontalAlign="Left"></HeaderStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDCNo" runat="server" ForeColor="#0066ff" Text="<%# BIND('INT_DC_NO') %>" CausesValidation="False"
                                    OnClick="lbtnDCNo_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:MM/dd/yyyy}" ReadOnly="True" SortExpression="MOVINGDC_DATE" DataField="MOVINGDC_DATE" HeaderText="DC Date">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="IND_NO" SortExpression="IND_NO" HeaderText="Indent No">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="LocFrom" SortExpression="LocFrom" HeaderText="From">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>

                        <asp:BoundField DataField="LocTo" SortExpression="LocTo" HeaderText="To">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="EMP_FIRST_NAME" SortExpression="EMP_FIRST_NAME" HeaderText="Prepared By">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:BoundField>

                    </Columns>
                    <EmptyDataTemplate>
                        No Record Found
                    
                    </EmptyDataTemplate>
                    <SelectedRowStyle BackColor="LightSteelBlue" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="USP_StockMovement_Search_Select" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchTypeHidden" DefaultValue="0" Name="SearchType" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueFromHidden" DefaultValue="0" Name="SearchValueFrom" PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:Label ID="lblDCId" runat="server" Visible="false"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="4">
                <table align="center">
                    <tr>
                        <td>
                            <asp:Button ID="btnAddnew" runat="server" Text="Add New" OnClick="btnAddnew_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Print" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <table style="width: 100%" runat="server" id="internalDCtbl" visible="false">
                <tr>
                    <td colspan="4" class="profilehead">Internal DC Details</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table class="stacktable">
                            <%--<tr>
                        <td cols
                                pan="4" class="profilehead">Moving Details</td>
                    </tr>--%>
                             <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label20" runat="server" Text="MDC with"></asp:Label></td>
                                <td style="text-align: left; padding-left: 10px;">
                                    <asp:RadioButtonList ID="rdblIndentfor" CssClass="RadioIndentFor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdblIndentfor_SelectedIndexChanged"
                                        RepeatDirection="Horizontal" RepeatLayout="Flow">
                                        <asp:ListItem Selected="True">General</asp:ListItem>
                                        <asp:ListItem>QRCode Scanner</asp:ListItem>
                                    </asp:RadioButtonList></td>
                                <td style="text-align: right"></td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">DC No :
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtDCNo" runat="server" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td style="text-align: right">DC Date :
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtmovingdate" type="datepic" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label1" runat="server" Text="Indent No :"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlIndentno" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlIndentno_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblMDate0" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlIndentno" ErrorMessage="Please Select the Indent No." InitialValue="0" ValidationGroup="id">*</asp:RequiredFieldValidator>--%>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label2" runat="server" Text="Indent Date :"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtIndentdate" type="datepic" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label3" runat="server" Text="Moving From :"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlMovingFrom" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource1" DataTextField="whname" DataValueField="wh_id">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblMDate2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlMovingFrom" ErrorMessage="Please Select the Moving From" InitialValue="0" ValidationGroup="id">*</asp:RequiredFieldValidator>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="USP_Warehouse_Loc_Select" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="lblCompany" DefaultValue="0" Name="LocID" PropertyName="Text" Type="String" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label4" runat="server" Text="Moving To:"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlMovingTo" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource1" DataTextField="whname" DataValueField="wh_id">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblMDate3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlMovingTo" ErrorMessage="Please Select the Moving To" InitialValue="0" ValidationGroup="id">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">Vehicle No:</td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtVechileNo" runat="server"></asp:TextBox>
                                    <asp:Label ID="lblMDate4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtVechileNo" ErrorMessage="Please Enter the Vehicle No." ValidationGroup="id">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="text-align: right">QR Code</td>
                                <td><asp:TextBox ID="txtQR" runat ="server" OnTextChanged ="txtQR_TextChanged" AutoPostBack ="true" Enabled ="false"   ></asp:TextBox>
                               <asp:Label ID="Label5" runat="server" Visible ="false"  ></asp:Label></td>
                            </tr>
                            <tr><td>&nbsp;</td></tr>
                             <tr>
                                <td colspan="4" class="profilehead">DC Item Details</td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:GridView ID="gvDCItems" runat="server" AutoGenerateColumns="False" Style="text-align: center" Width="100%" OnRowDataBound="gvDCItems_RowDataBound">
                                        <Columns>

                                            <asp:BoundField DataField="ItemCode" HeaderText="Itemcode" />
                                            <asp:BoundField DataField="Brand" HeaderText="Brand" />
                                            <asp:BoundField DataField="ModelNo" HeaderText="ModelNo" />
                                            <asp:BoundField DataField="Color" HeaderText="Color" />
                                            <asp:BoundField DataField="Qty" HeaderText="Quantity" />
                                            <asp:BoundField DataField="Remarks" HeaderText="Description" >
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ClientName" HeaderText="ClientName" />
                                            <asp:BoundField DataField="BrandId" HeaderText="BrandId" />
                                            <asp:BoundField DataField="ColorId" HeaderText="ColorId" />
                                            <asp:BoundField DataField="Remark" HeaderText="Remarks" />                                           
                                            <asp:BoundField DataField="DetId" HeaderText="DetId" />  
                                             <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnDelete" CausesValidation="false" ForeColor="Red" runat="server" __designer:wfdid="w5" OnClick="lbtnDelete_Click">Delete</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                         
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr><td>&nbsp;</td></tr>
                            <tr>
                                <td colspan="4" class="profilehead">Indent Item Details</td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <asp:GridView ID="gvIndentdetails" runat="server" AutoGenerateColumns="False" Style="text-align: left" Width="100%" OnRowDataBound="gvIndentdetails_RowDataBound">
                                        <Columns>

                                            <asp:BoundField DataField="ItemCode" HeaderText="Itemcode" />
                                            <asp:BoundField DataField="Brand" HeaderText="Brand" />
                                            <asp:BoundField DataField="ModelNo" HeaderText="ModelNo" />
                                            <asp:BoundField DataField="Color" HeaderText="Color" />
                                            <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                            <asp:BoundField DataField="Remarks" HeaderText="Description" />
                                            <asp:BoundField DataField="ClientName" HeaderText="ClientName" />
                                            <asp:BoundField DataField="BrandId" HeaderText="BrandId" />
                                            <asp:BoundField DataField="ColorId" HeaderText="ColorId" />
                                            <asp:BoundField DataField="Remark" HeaderText="Remarks" />
                                            <asp:TemplateField>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="cbSelectAll" runat="server" Text="All" OnClick="selectAll(this)" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" __designer:wfdid="w4"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <span style="color: #ff0000">No Record Found! </span>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                    <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                
                            </tr>
                            <div>
            <asp:TextBox ID="txtItemCode" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtBrand" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtModelNo" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtColorName" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtQty" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtRemarks" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtClientName" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtBrandId" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtColorId" runat ="server" Visible ="false" ></asp:TextBox>
            <asp:TextBox ID="txtRemarks1" runat ="server" Visible ="false" ></asp:TextBox>
            
        </div>
                            <tr >
                                <td  style="text-align: right">
                                    <asp:FileUpload ID="fileupload1" runat="server"  />

                                </td>
                                <td style="text-align: left">
                                    <asp:Button ID="btnRead" runat="server" onclick="btnRead_Click" Text="Upload Scaned Items" />

                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="4">
<asp:TextBox ID="textBoxContents" runat="server" Visible ="false"  tabIndex="0" height="100px" textMode="MultiLine" width="250px"></asp:TextBox>

                                    <asp:GridView ID="gvMovingItems" runat="server" AutoGenerateColumns="False" Style="text-align: center" Width="100%" OnRowDataBound="gvMovingItems_RowDataBound" OnRowDeleting="gvMovingItems_RowDeleting">
                                        <Columns>

                                            <asp:BoundField DataField="ItemCode" HeaderText="Itemcode" />
                                            <asp:BoundField DataField="Brand" HeaderText="Brand" />
                                            <asp:BoundField DataField="ModelNo" HeaderText="ModelNo" />
                                            <asp:BoundField DataField="Color" HeaderText="Color" />
                                            <%--<asp:BoundField DataField="Qty" HeaderText="Qty" />--%>
                                            <asp:TemplateField HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Bind("Qty") %>'>></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="Moving Location">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddllocation" runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                            <asp:BoundField DataField="Remarks" HeaderText="Description" >
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ClientName" HeaderText="ClientName" />
                                            <asp:BoundField DataField="BrandId" HeaderText="BrandId" />
                                            <asp:BoundField DataField="ColorId" HeaderText="ColorId" />
                                            <asp:TemplateField HeaderText="Remarks" ControlStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRemarks" runat="server" Text='<%# Bind("Remark") %>'>></asp:TextBox>
                                                </ItemTemplate>
                                                <ControlStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: right">Status :</td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlStatus" runat="server">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Open</asp:ListItem>
                            <asp:ListItem Value="2">Close</asp:ListItem>

                        </asp:DropDownList>
                        <asp:Label ID="lblCompany" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="USP_Warehouse_Loc_Select" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="lblCompany" DefaultValue="0" Name="LocID" PropertyName="Text" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click1" ValidationGroup="id" />
                    </td>
                    <td>
                        <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="id" ShowMessageBox="true" ShowSummary="false" />
            
                    </td>
                </tr>
                </table>
        </ContentTemplate>
            <Triggers>  
      
       <asp:PostBackTrigger ControlID="btnRead" />  
      
    </Triggers>  
    </asp:UpdatePanel>
    
</asp:Content>


 
