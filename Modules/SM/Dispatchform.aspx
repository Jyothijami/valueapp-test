<%@ Page Language="C#" MasterPageFile="~/MPs/InventoryMP1.master" AutoEventWireup="true" 
    CodeFile="Dispatchform.aspx.cs" Inherits="Modules_SM_Dispatchform" Title="|| Value Appp : Inventory : Dispatch Form ||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">


    <script lang="javascript" type="text/javascript">
        function OpenPopupCenter(pageURL, title, w, h) {
            var left = (screen.width - w) / 2;
            var top = (screen.height - h) / 4;  // for 25% - devide by 4  |  for 33% - devide by 3
            var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
        }
    </script>

     <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <table style ="width :100%">
        <tr>
                    <td style="text-align: center; font-size: medium;">
                       <asp:LinkButton ID="lnkDispatch" runat="server" OnClick="lnkDispatch_Click" Font-Underline="True">Dispatch Instructions</asp:LinkButton>
                        &nbsp;||&nbsp;
                        <asp:LinkButton ID="lnkIndent" runat="server" OnClick="lnkIndent_Click" Font-Underline="True">Display Indent Instructions</asp:LinkButton>
                    </td>
                </tr>
    </table>
            <asp:Panel ID ="pnlIndent" runat ="server" Visible ="false" >
               <table border="0" cellpadding="0" cellspacing="0" width="100%" class="pagehead">
        <tr>
            <td class="pagehead" style="text-align:left">
                Indent
                </td>
            <td style="float:left">
                <asp:DropDownList ID="ddlNoOfRecords1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRecords1_SelectedIndexChanged">
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>75</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                   
                </asp:DropDownList>
            </td>
        </tr>
        </table>
    
    <table id="tblmain" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td colspan="3" class="searchhead">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">
                            Indent</td>
                        <td>
                        </td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td style="height: 25px">
                                        <asp:Label id="Label19" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList id="ddlSearchBy1" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem>--</asp:ListItem>
                                            <asp:ListItem Value="IND_No">Indent No</asp:ListItem>
                                            <asp:ListItem Value="IND_Date">Indent Date</asp:ListItem>
                                            <asp:ListItem Value="DEPT_NAME">Department</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME">Customer Name</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList id="ddlSymbols1" runat="server" AutoPostBack="True" CssClass="textbox"
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
                                        <asp:Label id="lblCurrentFromDate1" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:TextBox id="txtSearchValueFromDate1" type="datepic" runat="server" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox><%--<asp:Image id="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:calendarextender id="ceSearchFrom" runat="server" enabled="False" format="dd/MM/yyyy"
                                            popupbuttonid="imgFromDate" targetcontrolid="txtSearchValueFromDate"></cc1:calendarextender>
                                        <cc1:maskededitextender id="MaskedEditSearchFromDate" runat="server" displaymoney="Left"
                                            enabled="False" mask="99/99/9999" masktype="Date" targetcontrolid="txtSearchValueFromDate"
                                            userdateformat="MonthDayYear"></cc1:maskededitextender>--%>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Label id="lblCurrentToDate1" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:TextBox id="txtSearchValueToDate1" type="datepic" runat="server" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox></td>
                                    <td style="height: 25px">
                                        <asp:TextBox id="txtSearchText1" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><%--<asp:Image id="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:calendarextender id="ceSearchValueToDate" runat="server" enabled="False" format="dd/MM/yyyy"
                                            popupbuttonid="imgToDate" targetcontrolid="txtSearchText"></cc1:calendarextender>
                                        <cc1:maskededitextender id="MaskedEditSearchToDate" runat="server" displaymoney="Left"
                                            enabled="False" mask="99/99/9999" masktype="Date" targetcontrolid="txtSearchText"
                                            userdateformat="MonthDayYear"></cc1:maskededitextender>--%>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Button id="btnSearchGo1" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" onclick="btnSearchGo1_Click" Text="Go" /></td>
                                </tr>
                                </table>
                            <asp:Label id="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label><asp:Label id="lblEmpIdHidden" runat="server" Visible="False"></asp:Label><asp:Label
                                    id="Label22" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="Label23" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="Label24" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="Label25" runat="server" Visible="False"></asp:Label>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:GridView id="gvIndentDetails" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="sdsIndentDetails" SelectedRowStyle-BackColor="#c0c0c0" OnRowDataBound="gvIndentDetails_RowDataBound" AllowSorting="True">
                    <columns>
<asp:BoundField DataField="IND_ID" SortExpression="IND_ID" HeaderText="IndentIdHidden"></asp:BoundField>
<asp:TemplateField HeaderText="Indent No" SortExpression="IND_NO"><EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("IND_NO") %>' ID="TextBox1"></asp:TextBox>
                            
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnIndentNo" onclick="lbtnIndentNo_Click" runat="server" ForeColor="#0066ff" Text='<%# Eval("IND_NO") %>' CausesValidation="False" __designer:wfdid="w11"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HtmlEncode="False" SortExpression="IND_DATE" DataFormatString="{0:dd/MM/yyyy}" DataField="IND_DATE" HeaderText="Indent Date">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CUST_NAME" SortExpression="CUST_NAME" HeaderText="Customer Name">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="PREPAREDBY" SortExpression="PREPAREDBY" HeaderText="Prepared By"></asp:BoundField>
<asp:BoundField DataField="APPROVEDBY" SortExpression="APPROVEDBY" HeaderText="Approved By"></asp:BoundField>
<asp:BoundField DataField="STATUS" SortExpression="STATUS" HeaderText="Status"></asp:BoundField>
</columns>
                    <emptydatatemplate>
No Data Exist!
</emptydatatemplate>
                </asp:GridView>
                <asp:SqlDataSource id="sdsIndentDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SCM_INDENT_REQUEST_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="EMPID" ControlID="lblEmpIdHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>
</selectparameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: center">
                <table id="Table1" align="center">
                    <tr>
                        <td>
                            <asp:Button id="btnNew" runat="server" CausesValidation="False" onclick="btnNew_Click"
                                Text="New" /></td>
                        <td>
                            <asp:Button id="btnEdit1" runat="server" CausesValidation="False" onclick="btnEdit1_Click"
                                Text="Edit" /></td>
                        <td>
                            <asp:Button id="btnDelete1" runat="server" CausesValidation="False" onclick="btnDelete1_Click"
                                Text="Delete" /></td>
                        <td>
                                        <asp:Button id="btnPrint1" runat="server" CausesValidation="False" onclick="btnPrint1_Click"
                                            Text="Print" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
            </asp:Panel>
            <asp:Panel ID="pnlDispatch" runat ="server" >
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead" style="width: 100%">
        <tr>
            <td style="text-align:left">Dispatch Instrutions</td>
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
    <table style="width: 100%">
        <tr>
            <td colspan="5" style="text-align: right">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="searchhead" style="text-align: left">Dispatch Instrutions
                        </td>
                        <td></td>
                        <td class="searchhead" style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td style="height: 25px">
                                        <asp:Label ID="Label17" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True"
                                            CssClass="textbox" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem
                                                Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="Dispatch_id">S.No</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME">CustomerName</asp:ListItem>
                                            <%--<asp:ListItem Value="CUST_UNIT_NAME">UnitName</asp:ListItem>--%>
                                            <asp:ListItem Value="CP_FULL_NAME">Company</asp:ListItem>
                                            <asp:ListItem Value="EMP_FIRST_NAME">ApprovedBy</asp:ListItem>
                                            <asp:ListItem>DeliveryDate</asp:ListItem>
                                            <%--<asp:ListItem Value="ModelNo">Model No</asp:ListItem>--%>
                                        </asp:DropDownList></td>
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
                                        </asp:DropDownList></td>
                                    <td style="height: 25px">
                                        <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchValueFromDate" runat="server" type="date" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox><%--<asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender ID="ceSearchFrom" runat="server" Enabled="False" Format="dd/MM/yyyy"
                                            PopupButtonID="imgFromDate" TargetControlID="txtSearchValueFromDate">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>--%>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchValueToDate" runat="server" type="date" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox></td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px"></asp:TextBox><%--<asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender ID="ceSearchValueToDate" runat="server" Enabled="False" Format="dd/MM/yyyy"
                                            PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>--%>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Button ID="Button1" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                                </tr>
                            </table>
                            <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:GridView ID="gvDispatchIns" runat="server" SelectedRowStyle-BackColor="#c0c0c0" Width="100%" AutoGenerateColumns="False"
                     DataSourceID="SqlDataSource2" AllowPaging="True" OnSelectedIndexChanged="gvDispatchIns_SelectedIndexChanged" OnRowDataBound ="gvDispatchIns_RowDataBound"
                    AllowSorting="True">
                    <Columns>
                        <asp:BoundField DataField="Dispatch_id" SortExpression="Dispatch_id" HeaderText="Id"></asp:BoundField>
                        <asp:TemplateField HeaderText="CustomerName" SortExpression="CUST_NAME">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" OnClick="LinkButton1_Click" ForeColor="#0066ff" runat="server" Text='<%# Bind("CUST_NAME") %>' __designer:wfdid="w35"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CUST_UNIT_NAME" SortExpression="CUST_UNIT_NAME" HeaderText="CustomerUnit"></asp:BoundField>
                        <asp:BoundField DataField="CP_SHORT_NAME" SortExpression="CP_SHORT_NAME" HeaderText="Company"></asp:BoundField>
                        <asp:BoundField HtmlEncode="False" SortExpression="DeliveryDate" DataFormatString="{0:dd/MM/yyyy}" DataField="DeliveryDate" HeaderText="DeliveryDate"></asp:BoundField>
                        <asp:BoundField DataField="EMP_FIRST_NAME" SortExpression="EMP_FIRST_NAME" HeaderText="Executive"></asp:BoundField>
                        <asp:BoundField DataField="Status" SortExpression="Status" HeaderText="Status"></asp:BoundField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="CreatedOn" SortExpression="CreatedOn" HeaderText="Prepared on"></asp:BoundField>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:T}" DataField="CreatedOn" SortExpression="CreatedOn" HeaderText="CreatedTime"></asp:BoundField>
                        <asp:BoundField DataField ="Cust_UnitId" HeaderText ="Cust Id" />
                        <asp:BoundField DataField ="So_Id" HeaderText ="SO Id" />
                        <asp:BoundField DataField ="Executive" HeaderText ="ExeId" />
                        <asp:BoundField DataField ="CP_ID" HeaderText ="CPID" />
                        <asp:TemplateField HeaderText=" Credit Approval">
                <ItemTemplate>
                    <div class="row">
                        <div class="col-md-12 ">
                            <span class="text-center">
                        <a runat="server" class="btn btn-icon btn-primary " href='<%# "~/Modules/SM/CreditApproval.aspx?Cid=" + Eval("Dispatch_id") %>' onclick="OpenPopupCenter(this.href, 'newwindow', 800, 500);return false; ">
                           <%-- <i class="icsw16-speech-bubbles"></i><span class="badge badge-important">
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("Cou") %>'></asp:Label></strong></span>--%>
                            <span style="color:white"><strong> Credit Approval</strong> </span>

                        </a>

                    </span>
                            </div>
                    </div>

                </ItemTemplate>
            </asp:TemplateField>
                        <%--<asp:BoundField DataField ="EMP_FIRST_NAME" HeaderText ="ApprovedBy" />--%>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_Dispatch_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <table align="center">
                    <tr>
                        <td>
                            <asp:Button ID="btnAdd" runat="server" Text="Add New" OnClick="btnAdd_Click" /></td>
                        <td>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" /></td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" /></td>
                        <td>
                            <asp:Button ID="btnCreditApprove" runat="server" Visible ="false"  Text="Credit Approval" CausesValidation="False" OnClick="btnCreditApprove_Click"  />
                            <%--<asp:Button ID="btnCreditApprove" runat ="server" Text="Credit Approve" OnClick="btnCreditApprove_Click" />--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <table id="tblsub2" runat="server" width="100%">
                    
                            <tr>
                            <td colspan="5">
                                <table id="tblsub" runat="server" width="100%">
                                    <tr>
                                        <td colspan="4" style="text-align: left">Add Dispatch Instrutions</td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right" align="right">
                                            <asp:Label ID="lblSearch" runat="server" Text="Search Customer" Width="110px"></asp:Label></td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtSearchModel" runat="server">
                                            </asp:TextBox>
                                            <asp:Button ID="btnSearchModelNo" runat="server" BorderStyle="None" CausesValidation="False"
                                                CssClass="gobutton" EnableTheming="False" OnClick="btnSearchModelNo_Click" Text="Go" />
                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                                SelectCommand="SP_Customer_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                                <SelectParameters>
                                                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtSearchModel"></asp:ControlParameter>
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                        </td>
                                        <td style="text-align: left"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right" align="right">
                                            <asp:Label ID="lblCustomer" runat="server" Text="Customer Name" Width="104px"></asp:Label></td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomerName_SelectedIndexChanged">
                                            </asp:DropDownList></td>
                                        <td style="text-align: right" align="right">
                                            <asp:Label ID="lblRegion" runat="server" Text="Region"></asp:Label></td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtRegion" runat="server" ReadOnly="True">
                                            </asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right" align="right">
                                            <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label></td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtAddress" runat="server" ReadOnly="True" TextMode="MultiLine">
                                            </asp:TextBox></td>
                                        <td style="text-align: right" align="right">
                                            <asp:Label ID="lblPhone" runat="server" Text="Phone"></asp:Label></td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtPhone" runat="server" ReadOnly="True">
                                            </asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right" align="right">
                                            <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True">
                                            </asp:TextBox></td>
                                        <td style="text-align: right" align="right">
                                            <asp:Label ID="lblMobile" runat="server" Text="Mobile"></asp:Label></td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True">
                                            </asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right" align="right">
                                            <asp:Label ID="Label4" runat="server" Text="Unit Name"></asp:Label></td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="ddlUnitName" runat="server">
                                            </asp:DropDownList></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right"></td>
                                        <td style="text-align: left"></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Label ID="lblSalesOrderNo" runat="server" Text="Purchase Order No" Width="130px">
                                            </asp:Label></td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="ddlSalesOrderNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSalesOrderNo_SelectedIndexChanged">
                                            </asp:DropDownList></td>
                                        <td style="text-align: right"></td>
                                        <td style="text-align: left">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: left">Purchase Order Details</td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: center">
                                            <asp:GridView ID="gvItemDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvItemDetails_RowDataBound"
                                                ShowFooter="True" Width="100%">
                                                <Columns>
                                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                                    <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                                    <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                                    <asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
                                                    <asp:BoundField HeaderText="UnitPrice"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Amount"></asp:BoundField>
                                                    <asp:BoundField DataField="Price" HeaderText="SpPrice"></asp:BoundField>
                                                    <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <span style="color: #ff0000">No Data Exits</span>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: center">
                                            <table align="center">
                                                <tr>
                                                    <td style="text-align: right">
                                                        <asp:Label ID="Label12" runat="server" Text="ModelNo"></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtModelNo" runat="server">
                                                        </asp:TextBox></td>
                                                    <td colspan="2" rowspan="4"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right">
                                                        <asp:Label ID="Label13" runat="server" Text="Item Name"></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtItemName" runat="server">
                                                        </asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right">
                                                        <asp:Label ID="Label15" runat="server" Text="Quantity"></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtQty" runat="server">
                                                        </asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right">
                                                        <asp:Label ID="Label16" runat="server" Text="Color"></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtColor" runat="server">
                                                        </asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="btnAd" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                        CssClass="loginbutton" EnableTheming="False" OnClick="btnAd_Click" Text="Add"
                                                                        ValidationGroup="items" /></td>
                                                                <td>
                                                                    <asp:Button ID="btnItemRefresh" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                        CausesValidation="False" CssClass="loginbutton" EnableTheming="False" OnClick="btnItemRefresh_Click"
                                                                        Text="Refresh" /></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: center; height: 21px;"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: center">
                                            <asp:GridView ID="gvDispatch" runat="server" AutoGenerateColumns="False" OnRowEditing="gvDispatch_RowEditing" OnRowDeleting="gvDispatch_RowDeleting" Width="100%" OnRowDataBound="gvDispatch_RowDataBound">
                                                <Columns>
                                                    <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                                    <asp:BoundField DataField="ItemName" HeaderText="ItemName">
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ModelNo" HeaderText="ModelNo"></asp:BoundField>
                                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                                    <asp:BoundField DataField="Colour" HeaderText="Colour"></asp:BoundField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <span style="color: #ff0066">No data Found</span>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Label ID="Label1" runat="server" Text="Delivery Date" Width="102px"></asp:Label></td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtDeliveryDate" runat="server" type="date"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right"> 
                                            <asp:Label ID="Label2" runat="server" Text="Delivery Time" Width="103px"></asp:Label></td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtDeliveryTime" runat="server">
                                            </asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Label ID="Label3" runat="server" Text="Company"></asp:Label></td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="ddlCompany" runat="server">
                                            </asp:DropDownList></td>
                                        <td style="text-align: right"></td>
                                        <td style="text-align: left"></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Label ID="Label5" runat="server" Text="Remarks"></asp:Label></td>
                                        <td colspan="3" style="text-align: left">
                                            <asp:TextBox ID="txtRemarks" runat="server" EnableTheming="False" TextMode="MultiLine"
                                                Width="561px">
                                            </asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Label ID="Label6" runat="server" Text="Old Dues"></asp:Label></td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtOldDues" runat="server">
                                            </asp:TextBox></td>
                                        <td style="text-align: right">
                                            <asp:Label ID="Label11" runat="server" Text="Payment Be Collected" Width="139px"></asp:Label></td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtPaymentsCollected" runat="server">
                                            </asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right" align="right">
                                            <asp:Label ID="Label7" runat="server" Text="Transport Charges" Width="127px"></asp:Label></td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtTransportCharges" runat="server">
                                            </asp:TextBox></td>
                                        <td style="text-align: right" align="right">
                                            <asp:Label ID="Label9" runat="server" Text="Packing Charges" Width="105px"></asp:Label></td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtPackingCharges" runat="server">
                                            </asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right" align="right">
                                            <asp:Label ID="Label8" runat="server" Text="Executive"></asp:Label></td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="ddlExecutive" runat="server">
                                            </asp:DropDownList></td>
                                        <td style="text-align: right"></td>
                                        <td style="text-align: left"></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right"></td>
                                        <td style="text-align: left"></td>
                                        <td style="text-align: right"></td>
                                        <td style="text-align: left"></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right" align="right">
                                            <asp:Label ID="Label10" runat="server" Text="Prepared By"></asp:Label></td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                                            </asp:DropDownList></td>
                                        <td style="text-align: right" align="right">
                                            <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By"></asp:Label></td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="ddlApprovedby" runat="server" Enabled="False">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right"></td>
                                        <td style="text-align: left"></td>
                                        <td style="text-align: right"></td>
                                        <td style="text-align: left"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: center">
                                            <table align="center">
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                                    <td>
                                                        <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" /></td>
                                                    <td>
                                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" /></td>
                                                    <td colspan="2">
                                                        <asp:Button ID="btnExit" runat="server" Text="Exit" OnClick="btnExit_Click" /></td>
                                                    <td colspan="1">
                                                        <asp:Button ID="btnDesPrint" runat="server" OnClick="btnDesPrint_Click" Text="Print" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                            <tr>
                                <td style="width: 100px"></td>
                                <td style="width: 100px"></td>
                                <td style="width: 100px"></td>
                                <td style="width: 100px"></td>
                                <td style="width: 100px"></td>
                            </tr>
                </table>
                </td>
            </tr>
        </table>
                <br />
            <asp:Panel ID="Panel2" runat="server" meta:resourcekey="Panel2Resource2">
                <table border="0" visible="false" runat="server" cellpadding="0" cellspacing="0" style="font-size: 8pt; background-image: url(Images/ConfirmBox2.PNG); background-repeat: repeat; font-family: Verdana"
                    id="tblpopup3">
                    <tr>
                        <td style="height: 313px; text-align: left; background: url('../../Images/ConfirmBox1Sa.PNG'); width: 55px;"></td>
                        <td align="center" background="../../Images/ConfirmBox2sa.PNG" style="height: 313px"
                            valign="top">
                            <table id="tblPopup2" runat="server">
                                <tr>
                                    <td style="text-align: right ; width:30%;"></td>
                                    <td  rowspan="1" style="text-align: left; height: 40px;"></td>
                                    <td style="text-align: right ; width:50%;"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right ; width:30%;"></td>
                                    <td class="profilehead"  rowspan="1" style="text-align: left">
                                        <asp:Label ID="Label14" runat="server" Text="Credit Approval Details:"></asp:Label></td>
                                    <td style="text-align: right ; width:50%;"></td>
                                    <td class="profilehead"  rowspan="1" style="text-align: left"><asp:Label ID="lblAccPart" runat ="server" Text="Accounts Details:" ></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style ="text-align :right"><asp:Label ID="lblNo" runat ="server" Text ="Credit Approval No" ></asp:Label></td>
                               <td  style="text-align: Left;">
                                        <asp:TextBox ID="txtCRANo" runat ="server" ReadOnly ="true"  ></asp:TextBox>
                                    </td>
                                     </tr>
                                <tr>
                                    <td style="text-align: right ; width:30%;"><asp:Label id="lblcustname" runat ="server" Text="Customer Name" ></asp:Label></td>
                                    <td  style="text-align: Left;">
                                        <asp:TextBox ID="txtCustName" runat ="server" ReadOnly ="true"  ></asp:TextBox>
                                    </td>
                                    <td style="text-align: right ;width:50%;"><asp:Label ID="lblCR" Text="Cr/DR Balance as on Date" runat ="server"  ></asp:Label></td>
                                    <td style="text-align: Left;"><asp:TextBox ID="txtCR" runat ="server"  ></asp:TextBox>
                                        <asp:RadioButton ID="rdbWithPo" runat="server" AutoPostBack="True"
                                        GroupName="as"  Text="DR"></asp:RadioButton>
                                        <asp:RadioButton ID="rdbWithoutPo" runat="server" AutoPostBack="True" GroupName="as"
                                         Text="CR" Checked="True"></asp:RadioButton>
                        
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right ; width:30%;"><asp:Label id="lblcustname0" runat ="server" Text="Customer Mobile" ></asp:Label></td>
                                    <td  style="text-align: Left">
                                        <asp:TextBox ID="txtCustomerMobile" runat="server" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right ; width:50%;">
                                        <asp:Label ID="lblcustname1" runat="server" Text="Email Id"></asp:Label>
                                    </td>
                                    <td style="text-align: Left;">
                                        <asp:TextBox ID="txtcustomerEmail" runat="server" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right ; width:30%;">
                                        <asp:Label ID="lblCustAdd" runat="server" Text="Customer Address"></asp:Label>
                                    </td>
                                    <td style="text-align: Left">
                                        <asp:TextBox ID="txtcustAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right ; width:50%;">
                                        <asp:Label ID="lblUDC" runat="server" Text="Approx. Value of Unbiled DC's"></asp:Label>
                                    </td>
                                    <td style="text-align: Left;">
                                        <asp:TextBox ID="txtUDC" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right ; width:30%;"><asp:Label ID="lblPOValue" runat ="server" Text="PO Value" ></asp:Label></td>
                                    <td  style="text-align: Left;"><asp:TextBox ID="txtPOValue" runat ="server" ReadOnly ="true"  ></asp:TextBox></td>
                                    <td style="text-align: right ; width:50%;"><asp:Label ID="lblOther" Text="Cr/Dr Balance of Other Branches" runat ="server"  ></asp:Label></td>
                                    <td style="text-align: Left;"><asp:TextBox ID="txtOther" runat ="server"  ></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right ; width:30%;"><asp:Label ID="lblDispatchValue" runat ="server" Text="Total Dispatch Value" ></asp:Label></td>
                                    <td style="text-align: Left;">
                                        <asp:TextBox ID="txtDispatchValue" runat ="server"></asp:TextBox><asp:Label runat ="server" Text="Incl GST" ForeColor ="Red" ></asp:Label>
                                        <td style="text-align: right ; width:50%;"><asp:Label ID="llbAccId" Text="Accounts" runat ="server"  ></asp:Label></td>
                                    <td style="text-align: Left;"><asp:DropDownList ID="ddlAccId" runat ="server"  ></asp:DropDownList></td>
                                    </td>
                                    
                                </tr>
                                <tr>
                                                                        <td style="text-align: right ; width:30%;"><asp:Label ID="Label18" runat ="server" Text="Payment Terms" ></asp:Label></td>

                                    <td style="text-align: Left;">
                                        <asp:Label ID="lblPaymentTerms" Visible ="false"  runat ="server" ForeColor ="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right ; width:30%;"><asp:Label ID="lblCreditAppAmt" runat ="server" Text="Credit Approval Amount" ></asp:Label></td>
                                    <td  style="text-align: Left;">
                                        <asp:TextBox ID="txtCreditAmt" runat ="server" ></asp:TextBox>
                                    </td>
                                    <td style="text-align: right ; width:50%;"><asp:Label ID="lblCMD" Text="Managemnet" runat ="server"  ></asp:Label></td>
                                    <td style="text-align: Left;"><asp:DropDownList ID="ddlCMD" runat ="server"  ></asp:DropDownList></td>
                                    
                                </tr>
                                <tr>
                                    <td style="text-align: right ;"><asp:Label ID="lblDays" runat ="server" Text="will collect the payment on" ></asp:Label></td>
                                    <td  style="text-align: Left;">
                                        <asp:TextBox ID="txtDays" runat ="server" type ="datepic"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right ; width:50%;"><asp:Label ID="lblRmks" Text="Remarks" runat ="server"  ></asp:Label></td>
                                    <td><asp:TextBox ID="txtRmks" runat ="server" TextMode ="MultiLine"  ></asp:TextBox></td>
                                </tr>
                                
                                <tr>
                                    <td style="text-align: center;">
                                        <asp:Button ID="Button2" runat="server" Text="Close" EnableTheming="False" OnClick="Button2_Click"/>
                                        </td>
                                    <td>
                                        <asp:Button ID="btnSavepopup" OnClick ="btnSavepopup_Click" runat="server" Text="Save" EnableTheming="False" />
                                        </td>
                                    <td>
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" EnableTheming="False" OnClick="btnPrint_Click" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                            &nbsp; &nbsp; &nbsp;
                        </td>
                        <td background="../../Images/ConfirmBox3sa.PNG" style="height: 213px; width: 27px;"></td>
                    </tr>
                </table>
            </asp:Panel>
                </asp:Panel>

            
              </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


 
