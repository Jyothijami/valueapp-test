<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/InventoryMP1.master" AutoEventWireup="true" CodeFile="Inventory_Search - Copy.aspx.cs" Inherits="Modules_Inventory_Inventory_Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="text-align: center; font-weight: bold;">&nbsp;
                            <asp:LinkButton ID="lnkMRN" runat="server" OnClick="lnkMRN_Click" Font-Underline="True">MRN Search</asp:LinkButton>
                &nbsp;||&nbsp;
                            <asp:LinkButton ID="lnkDC" runat="server" OnClick="lnkDC_Click" Font-Underline="True">DC Search</asp:LinkButton>
                &nbsp;||&nbsp;
                <asp:LinkButton ID="lnkOtherStock" runat="server" OnClick="lnkOtherStock_Click" Font-Underline="True">Sales/Sample/OS Search</asp:LinkButton>
                &nbsp;||&nbsp;
                <asp:LinkButton ID="lnkInvoice" runat="server" OnClick="lnkInvoice_Click" Font-Underline="True">Statement Search</asp:LinkButton>
                 &nbsp;||&nbsp;
                <asp:LinkButton ID="lnkDCStat" runat="server" OnClick="lnkDCStat_Click" Font-Underline="True">DC Statement</asp:LinkButton>

            </td>
        </tr>
    </table>
    <br />
    <asp:Panel runat="server" ID="pnlMRN" Visible="false">
        <div id="body" style="width: 100%" runat="server">
            <table style="width: 100%">
                <tr>
                    <td class="profilehead" style="text-align: left">Inventory MRN Details Search
                    </td>

                </tr>
                <tr>
                    <td style="text-align: right">Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                        <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />

                    </td>
                    <td style="text-align: right">
                        <asp:DropDownList ID="ddlNoOfRecords" runat="server" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>25</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>75</asp:ListItem>
                            <asp:ListItem>100</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
            <table>
                <tr>
                <td class="auto-style1">
                    &nbsp;
                </td>
                <td colspan="3" style="text-align:center" class="auto-style1">
                    <asp:RadioButtonList ID="rdbBT" runat="server" RepeatDirection="Horizontal" >
                        <asp:ListItem Value="0" Selected="True">Exclude BranchTransfer</asp:ListItem>
                        <asp:ListItem Value="1">Include BranchTransfer</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td style="text-align: right">Brand :
                    <%--<asp:DropDownList ID="ddlBrand" runat="server"></asp:DropDownList>--%>
                        <asp:TextBox ID="txtBrand" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">Model No :

                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtModelNo" runat="server"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">From Date :
                    <asp:TextBox ID="txtFromDate" type="datepic" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">To Date : </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtToDate" type="datepic" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">MRN No :
                    <asp:TextBox ID="txtMrnNo" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">Colour :</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtColor" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">Category :
                    <asp:TextBox ID="txtCategory" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">Sub Category :</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtSubCat" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">Remarks :
                    <asp:TextBox ID="txtRemarks" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">Company :</td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlCompany" runat="server" DataSourceID="SqlDataSource1" DataTextField="COMP_NAME" DataValueField="CP_ID" AppendDataBoundItems="True">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT a.CP_ID, a.CP_FULL_NAME + ',' + b.locname AS COMP_NAME FROM YANTRA_COMP_PROFILE AS a INNER JOIN location_tbl AS b ON a.locid = b.locid"></asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">Supplier Name :
                    <asp:TextBox ID="txtSupplier" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="text-align: left" colspan="2">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" BackColor="#9966FF" EnableTheming="True" ForeColor="White" Width="18%" />
                        &nbsp;
                    &nbsp;
                    &nbsp;
                    <asp:Button ID="btnExprot" runat="server" Text="Export To Excel" OnClick="btnExprot_Click" BackColor="#9966FF" EnableTheming="True" ForeColor="White" Width="30%" />

                    </td>
                </tr>

            </table>
        </div>
        <table>
        <tr>
            <td>
                Total No Of Inward Quantity :
            </td>
            <td>
                <asp:Label ID="lblInwardQty" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;
            </td>
        </tr>
    </table>
        <br />
        <div id="grid" style="width: 100%">
            <asp:GridView ID="gvInventoryInward" Width="100%" runat="server" SelectedRowStyle-BackColor="#c0c0c0" EmptyDataText="No Records To Display" AllowPaging="True" OnPageIndexChanging="gvInventoryInward_PageIndexChanging1" PageSize="10" OnRowDataBound="gvInventoryInward_RowDataBound">
                <HeaderStyle HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="Silver"></SelectedRowStyle>
            </asp:GridView>
        </div>

    </asp:Panel>

    <asp:Panel runat="server" ID="pnlDC" Visible="false">
        <div id="Div1" style="width: 100%" runat="server">
            <table style="width: 100%">
                <tr>
                    <td class="profilehead" style="text-align: left">Inventory DC Details Search
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">Go To Page :
                                    <asp:TextBox ID="txtGo2" Width="100px" runat="server"></asp:TextBox>
                        <asp:Button ID="btnGo2" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnGo2_Click" />

                    </td>
                    <td style="text-align: right">
                        <asp:DropDownList ID="ddlNoOfRecord2" runat="server" OnSelectedIndexChanged="ddlNoOfRecord2_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>25</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>75</asp:ListItem>
                            <asp:ListItem>100</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
            <table>
                <tr>
                <td class="auto-style1">
                    &nbsp;
                </td>
                <td colspan="3" style="text-align:center" class="auto-style1">
                    <asp:RadioButtonList ID="rbBranchTransfer" runat="server" RepeatDirection="Horizontal" >
                        <asp:ListItem Value="0" Selected="True">Exclude BranchTransfer</asp:ListItem>
                        <asp:ListItem Value="1">Include BranchTransfer</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
                <tr>
                    <td style="text-align: right">Brand :
                    <%--<asp:DropDownList ID="ddlBrand" runat="server"></asp:DropDownList>--%>
                        <asp:TextBox ID="txtBrand2" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">Model No :

                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtModel2" runat="server"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">From Date :
                    <asp:TextBox ID="txtFromDate2" type="datepic" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">To Date : </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtToDate2" type="datepic" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">DC No :
                    <asp:TextBox ID="txtMRN2" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">Colour :</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtColor2" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">Category :
                    <asp:TextBox ID="txtCat2" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">Sub Category :</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtSubCat2" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">Remarks :
                    <asp:TextBox ID="txtRemark2" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">Company :</td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlCompany2" runat="server" DataSourceID="SqlDataSource2" DataTextField="COMP_NAME" DataValueField="CP_ID" AppendDataBoundItems="True">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT a.CP_ID, a.CP_FULL_NAME + ',' + b.locname AS COMP_NAME FROM YANTRA_COMP_PROFILE AS a INNER JOIN location_tbl AS b ON a.locid = b.locid"></asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">Customer Name :
                    <asp:TextBox ID="txtCust" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="text-align: left" colspan="2">
                        <asp:Button ID="btnSearch2" runat="server" Text="Search" OnClick="btnSearch2_Click" BackColor="#9966FF" EnableTheming="True" ForeColor="White" Width="18%" />
                        &nbsp;
                    &nbsp;
                    &nbsp;
                    <asp:Button ID="btnDCExport" runat="server" Text="Export To Excel" OnClick="btnDCExport_Click" BackColor="#9966FF" EnableTheming="True" ForeColor="White" Width="28%" />

                    </td>
                </tr>

            </table>
        </div>
        <table>
        <tr>
            <td>
                Total No Of Outward Quantity :
            </td>
            <td>
                <asp:Label ID="lblOutwardQty" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;
            </td>
        </tr>
    </table>
        <br />
        <div id="gridDC" style="width: 100%">
            <asp:GridView ID="gvDCDetails" Width="100%" runat="server" SelectedRowStyle-BackColor="#c0c0c0" EmptyDataText="No Records To Display" AllowPaging="true" OnPageIndexChanging="gvDCDetails_PageIndexChanging" PageSize="10" OnRowDataBound ="gvDCDetails_RowDataBound">
                <HeaderStyle HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="Silver"></SelectedRowStyle>
            </asp:GridView>
        </div>
    </asp:Panel>

    <asp:Panel runat="server" ID="pnlOtherStock" Visible="false">
        <div id="Div2" style="width: 100%" runat="server">
            <table style="width: 100%">
                <tr>
                    <td class="profilehead" style="text-align: left">Saales/Sample/OS Inward Search
                    </td>

                </tr>
                <tr>
                    <td style="text-align: right">Go To Page :
                                    <asp:TextBox ID="txtGo3" Width="100px" runat="server"></asp:TextBox>
                        <asp:Button ID="btnGOPage3" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnGOPage3_Click" />

                    </td>
                    <td style="text-align: right">
                        <asp:DropDownList ID="ddlNoOfRecords3" runat="server" OnSelectedIndexChanged="ddlNoOfRecords3_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>25</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>75</asp:ListItem>
                            <asp:ListItem>100</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
            <table>
                <tr>
                    <td style="text-align: right">Company :
                         <asp:DropDownList ID="ddlCompName3" runat="server" DataSourceID="SqlDataSource1" DataTextField="COMP_NAME" DataValueField="CP_ID" AppendDataBoundItems="True">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">&nbsp;</td>
                    <td style="text-align: left">
                       
                        
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">Sales/Sample/OS NO :
                    <asp:TextBox ID="txtReference3" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">Model No :

                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtModelNo3" runat="server"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">From Date :
                    <asp:TextBox ID="txtFromDate3" type="datepic" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">To Date : </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtToDate3" type="datepic" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">Brand :
                    
                        <asp:TextBox ID="txtBrand3" runat="server"></asp:TextBox>
                    </td>
                    
                    <td style="width: 5%"></td>
                    <td style="width: 10%">Colour :</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtColor3" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">Category :
                    <asp:TextBox ID="txtCat3" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">Sub Category :</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtSubCat3" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: right">&nbsp;
                    </td>
                    <td style="width: 5%"></td>
                    <td style="text-align: left" colspan="2">
                        <asp:Button ID="btnSearch3" runat="server" Text="Search" OnClick="btnSearch3_Click" BackColor="#9966FF" EnableTheming="True" ForeColor="White" Width="18%" />
                        &nbsp;
                    &nbsp;
                    &nbsp;
                    <asp:Button ID="btnExport3" runat="server" Text="Export To Excel" OnClick="btnExport3_Click" BackColor="#9966FF" EnableTheming="True" ForeColor="White" Width="30%" />

                    </td>
                </tr>

            </table>
        </div>
        <table>
        <tr>
            <td>
                Total No Of Other Quantity :
            </td>
            <td>
                <asp:Label ID="lblOtherQty" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;
            </td>
        </tr>
    </table>
        <br />
        <div id="grid3" style="width: 100%">
            <asp:GridView ID="gvOtherStock" Width="100%" runat="server" SelectedRowStyle-BackColor="#c0c0c0" EmptyDataText="No Records To Display" 
                AllowPaging="True" OnPageIndexChanging="gvOtherStock_PageIndexChanging"  PageSize="10" OnRowDataBound ="gvOtherStock_RowDataBound" AutoGenerateColumns="False" >
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" />
                    <asp:BoundField DataField="Reference No" HeaderText="Reference No" SortExpression="Reference No" />
                    <asp:BoundField DataField="Ref Date" HeaderText="Ref Date" SortExpression="Ref Date" />
                    <asp:BoundField DataField="Item Code" HeaderText="Item Code" SortExpression="Item Code" />
                    <asp:BoundField DataField="Brand" HeaderText="Brand" SortExpression="Brand" />
                    <asp:BoundField DataField="Model No" HeaderText="Model No" SortExpression="Model No" />
                    <asp:BoundField DataField="Series" HeaderText="Series" SortExpression="Series" />
                    <asp:BoundField DataField="Sub-Category" HeaderText="Sub-Category" SortExpression="Sub-Category" />
                    <asp:BoundField DataField="Color" HeaderText="Color" SortExpression="Color" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                    <asp:BoundField DataField="Company" HeaderText="Company" SortExpression="Company" />
                    <asp:BoundField DataField ="Remarks" HeaderText ="Remarks" SortExpression ="Remarks" />
                </Columns>
                <HeaderStyle HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="Silver"></SelectedRowStyle>
            </asp:GridView>
            
        </div>
    </asp:Panel>
    
    <asp:Panel ID="pnlStatment" runat ="server" Visible ="false"  >
        <div id="Div3">
            <table style="width: 100%">
                <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblSearch" runat="server" Text="Search"></asp:Label></td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtSearchModel" runat="server">
                                                </asp:TextBox><asp:Button ID="btnSearchModelNo" runat="server" BorderStyle="None"
                                                    CausesValidation="False" CssClass="gobutton" EnableTheming="False" OnClick="btnSearchModelNo_Click"
                                                    Text="Go" />
                                                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
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
                                            <td style="text-align: right;"><asp:TextBox ID="txtUnitAdd" runat ="server" Visible ="false" ></asp:TextBox></td> 
                                            <td style="text-align: left; width: 439px;"></td> 
                                        </tr>
                <tr>
                    <td style ="text-align :right ">
                        <asp:Label ID="lblpono" runat ="server" Visible ="false"  Text ="Select PO No :" ></asp:Label>
                    </td>
                    <td style ="text-align :left">
                        <asp:CheckBoxList ID="chkPONo" runat ="server"  RepeatColumns="5" AutoPostBack ="true" OnSelectedIndexChanged ="chkPONo_SelectedIndexChanged" ></asp:CheckBoxList>
                        <asp:Label ID="lblchkpoid" runat ="server" Visible ="false" ></asp:Label>
                        <asp:Label ID="lblpodate" runat ="server" Visible ="false" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">&nbsp;
                    </td>
                    <td style="width: 5%"></td>
                     <td style="text-align: left" colspan="2">
                         <asp:Button ID ="btnSearchStat" BackColor="#9966FF" EnableTheming="True" Text ="Search" ForeColor="White" Width="18%"  runat ="server" OnClick ="btnSearchStat_Click" />
                      &nbsp;
                    &nbsp;
                    &nbsp;
                    <asp:Button ID="btnExportStat" runat="server" Text="Export To Excel" OnClick="btnExportStat_Click" BackColor="#9966FF" EnableTheming="True" ForeColor="White" Width="30%" />

                     </td>
                </tr>
            </table>
        </div>
         <div id="grid4" style="width: 100%">
             <asp:GridView id="gvStat" Width="100%" runat="server" EnableTheming ="false"  SelectedRowStyle-BackColor="#c0c0c0" EmptyDataText="No Records To Display" 
                 OnPageIndexChanging ="gvStat_PageIndexChanging" OnRowDataBound ="gvStat_RowDataBound" AutoGenerateColumns ="false"  >
                 <HeaderStyle BackColor ="White"  />
                 <Columns >
                     <asp:BoundField DataField ="ITEM_MODEL_NO" HeaderText ="Code" />
                     <asp:BoundField DataField ="ITEM_SPEC" HeaderText ="Description" />
                     <asp:BoundField DataField ="SO_DET_QTY" HeaderText ="PO Qty" >
                         <ItemStyle HorizontalAlign ="Center" />
                         <HeaderStyle HorizontalAlign ="Center" />
                     </asp:BoundField>
                     <asp:BoundField DataField ="SO_RATE" HeaderText ="Price" >
                         <ItemStyle HorizontalAlign ="Right" />
                         <HeaderStyle HorizontalAlign ="Center" />
                     </asp:BoundField>
                     <asp:BoundField DataField ="SO_DET_PRICE" HeaderText ="Total" >
                         <ItemStyle HorizontalAlign ="Right" />
                         <HeaderStyle HorizontalAlign ="Center" />
                     </asp:BoundField>
                     <asp:BoundField DataField ="SO_Id" HeaderText ="SOID" />
                     <asp:TemplateField HeaderText="DC Details">
                         <ItemTemplate>
                             <asp:GridView ID="gvDC" runat ="server" AutoGenerateColumns ="false" OnRowCommand ="gvDC_RowCommand" >
                                 <Columns >
                                     <asp:BoundField  HeaderText ="DC No" />
                                     <asp:BoundField HeaderText ="DC Date" />
                                 </Columns>
                             </asp:GridView>
                         </ItemTemplate>
                     </asp:TemplateField>
                 </Columns>
                 <HeaderStyle HorizontalAlign="left" />
                <SelectedRowStyle BackColor="Silver"></SelectedRowStyle>
             </asp:GridView>
         </div>
    </asp:Panel>

    <asp:Panel ID ="pnlDCStat" runat ="server" Visible ="false" >
        <div style="width: 100%" runat="server">
            <table style="width: 100%">
                <tr>
                    <td class="profilehead" style="text-align: left">DC Statment Search
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">Go To Page :
                                    <asp:TextBox ID="txtPageSearch" Width="100px" runat="server"></asp:TextBox>
                        <asp:Button ID="btnGo4" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnGo2_Click" />

                    </td>
                    <td style="text-align: right">
                        <asp:DropDownList ID="ddlNoOfRecord4" runat="server" OnSelectedIndexChanged="ddlNoOfRecord4_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>25</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>75</asp:ListItem>
                            <asp:ListItem>100</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
            <table>
                <tr>
                <td class="auto-style1">
                    &nbsp;
                </td>
                <%--<td colspan="3" style="text-align:center" class="auto-style1">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" >
                        <asp:ListItem Value="0" Selected="True">Exclude BranchTransfer</asp:ListItem>
                        <asp:ListItem Value="1">Include BranchTransfer</asp:ListItem>
                    </asp:RadioButtonList>
                </td>--%>
            </tr>
                <tr>
                    <td style="text-align: right">Brand :
                    <%--<asp:DropDownList ID="ddlBrand" runat="server"></asp:DropDownList>--%>
                        <asp:TextBox ID="txtDCBrand" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">Model No :

                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtDCModelNo" runat="server"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">From Date :
                    <asp:TextBox ID="txtDCFrom" type="datepic" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">To Date : </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtDCTo" type="datepic" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">DC No :
                    <asp:TextBox ID="TxtDcNo" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">Colour :</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtDCColor" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">Category :
                    <asp:TextBox ID="txtDcCate" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">Sub Category :</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtDCSub" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">Remarks :
                    <asp:TextBox ID="txtDCRemarks" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="width: 10%">Company :</td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlDCComp" runat="server" DataSourceID="SqlDataSource4" DataTextField="COMP_NAME" DataValueField="CP_ID" AppendDataBoundItems="True">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT a.CP_ID, a.CP_FULL_NAME + ',' + b.locname AS COMP_NAME FROM YANTRA_COMP_PROFILE AS a INNER JOIN location_tbl AS b ON a.locid = b.locid"></asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">Customer Name :
                    <asp:TextBox ID="txtDCCust" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%"></td>
                    <td style="text-align: left" colspan="2">
                        <asp:Button ID="btnSearch4" runat="server" Text="Search" OnClick="btnSearch4_Click" BackColor="#9966FF" EnableTheming="True" ForeColor="White" Width="18%" />
                        &nbsp;
                    &nbsp;
                    &nbsp;
                    <asp:Button ID="Button3" runat="server" Text="Export To Excel" OnClick="btnDCExport_Click" BackColor="#9966FF" EnableTheming="True" ForeColor="White" Width="28%" />

                    </td>
                </tr>

            </table>
            <br />
        <div id="gridDCStat" style="width: 100%">
            <asp:GridView ID="gvDCStat" Width="100%" runat="server" SelectedRowStyle-BackColor="#c0c0c0" EmptyDataText="No Records To Display" AllowPaging="true" OnPageIndexChanging="gvDCDetails_PageIndexChanging" PageSize="10" OnRowDataBound ="gvDCDetails_RowDataBound">
                <HeaderStyle HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="Silver"></SelectedRowStyle>
                <Columns >
                    <asp:BoundField DataField ="DC_NO" HeaderText ="DC No" />
                    <asp:BoundField DataField ="DC_Date" HeaderText ="DC Date" />
                    <asp:BoundField DataField ="CUST_NAME" HeaderText ="Customer Name" />
                    <asp:BoundField DataField ="DC_ID" HeaderText ="DC ID" />

                </Columns>
            </asp:GridView>
        </div>
        </div>
    </asp:Panel>
</asp:Content>



