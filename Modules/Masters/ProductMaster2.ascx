<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductMaster2.ascx.cs" Inherits="Modules_Masters_ProductMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

    
  
    <style type="text/css">
        .auto-style1 {
            width: 49px;
        }
    </style>

    
  
    <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left; width: 272px;">
                            Product&nbsp; Master</td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td rowspan="3">
                                        <asp:Label id="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By" Width="86px"></asp:Label></td>
                                    <td rowspan="3">
                                        <asp:DropDownList id="ddlSearchBy" runat="server" CssClass="textbox" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="Product_Id">Product Id</asp:ListItem>
                                            <asp:ListItem Value="Product_Code">Product Code</asp:ListItem>
                                            <asp:ListItem Value="Product_Name">Product Name</asp:ListItem>
                                            <asp:ListItem Value="ReorderLevel">Reorder Level</asp:ListItem>
                                            <asp:ListItem Value="Rate">Rate</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td rowspan="3">
                                    </td>
                                    <td rowspan="3">
                                        <asp:TextBox id="txtSearchText" runat="server" CssClass="textbox">
                                        </asp:TextBox><asp:Image id="imgCurrentDayTasksToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image></td>
                                    <td rowspan="3">
                                        <asp:Button id="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnSearchGo_Click" /></td>
                                
                            </table>
                            <asp:Label id="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: left">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 21px; text-align: center">
                <asp:GridView id="gvProductMasterDetails" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" DataSourceID="sdsProductMaster" OnRowDataBound="gvProductMasterDetails_RowDataBound">
                    <columns>
<asp:BoundField DataField="Product_Name" HeaderText="ProductMasterNameHidden"></asp:BoundField>
<asp:BoundField DataField="Product_Id" HeaderText="Sl No">
<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
                        <asp:BoundField DataField="Product_Code" HeaderText="Product Code" />
<asp:TemplateField HeaderText="Product Name"><EditItemTemplate>
<asp:TextBox runat="server" Text='<%# Bind("Item_name") %>' id="TextBox1"></asp:TextBox>
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnProductName" onclick="lbtnItemMasterName_Click" runat="server" Text="<%# Bind('Product_Name') %>" CausesValidation="False"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
                        <asp:BoundField DataField="ReorderLevel" HeaderText="Min Stock Quantity">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
                        <asp:TemplateField HeaderText="Image">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Image") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Image ID="Image" runat="server" Height="33px" ImageUrl="~/Images/noimage400x300.gif"
                                    Width="64px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ProductSpecification" HeaderText="ProductSpecification" />
                        <asp:BoundField DataField="PRODUCT_COMPANY_NAME" HeaderText="Brand Name">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Product_Company" HeaderText="Company Id" />
</columns>
                    <selectedrowstyle backcolor="LightSteelBlue" />
                    <PagerSettings PageButtonCount="8" />
                </asp:GridView>
                <asp:SqlDataSource ID="sdsProductMaster" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_MASTER_PRODUCTMASTER_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName"
                            PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue"
                            PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 51px; text-align: center">
                <table id="Table1" align="center">
                    <tr>
                        <td>
                            <asp:Button id="btnNew" runat="server" Text="New" CausesValidation="False" OnClick="btnNew_Click" /></td>
                        <td>
                            <asp:Button id="btnEdit" runat="server" Text="Edit" CausesValidation="False" OnClick="btnEdit_Click" /></td>
                        <td>
                            <asp:Button id="btnDelete" runat="server" Text="Delete" CausesValidation="False" OnClick="btnDelete_Click" /></td>
                        <td>
                            <asp:Button id="Button1" runat="server" Text="Print" CausesValidation="False" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
             <td colspan="2" style="text-align: center;">
                <table border="0" cellpadding="0" cellspacing="0" id="tblPMDetails" runat="server" visible="false" width="100%">
                    <tr>
            <td colspan="4" style="text-align: left; height: 20px;" class="profilehead">
                General Details</td>
                        <td class="profilehead" colspan="1" style="height: 20px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 22px;">
                            <asp:Label ID="Label3" runat="server" Text="Product Code :"></asp:Label></td>
                        <td style="text-align: left; height: 22px;">
                            <asp:TextBox ID="txtProductCode" runat="server"></asp:TextBox>
                            <asp:Label ID="Label4" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvProductCode" runat="server" ControlToValidate="txtProductCode"
                                ErrorMessage="Please Enter the Product Code">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right; height: 22px;">
                            </td>
                        <td style="text-align: left; height: 22px;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblItemName" runat="server" Text="Product Name :"></asp:Label></td>
                        <td style="text-align: left">
                        <asp:TextBox id="txtProductName" runat="server">
                            </asp:TextBox><asp:Label ID="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RFVDespName" runat="server" ControlToValidate="txtProductName"
                                ErrorMessage="Please Enter the Product Name">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label10" runat="server" Text="Brand :"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="DropDownList1" runat="server">
                            </asp:DropDownList><asp:Label ID="Label11" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*">
                            </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                ControlToValidate="DropDownList1" ErrorMessage="Please Select the Company" InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 24px;">
                <asp:Label id="Label1" runat="server" Text="Min. Stock Quantity :"></asp:Label>
                        </td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox id="txtMinimum" runat="server">
                            </asp:TextBox>
                            <asp:Label ID="Label2" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMinimum"
                                ErrorMessage="Please Enter the Min Stock Quantity">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label ID="lblRate" runat="server" Text="Rate :" Width="30px"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox ID="txtRate" runat="server">
                            </asp:TextBox><asp:Label ID="Label27" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*">
                            </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="txtRate" ErrorMessage="Please Enter the Rate">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtRate"
                                ErrorMessage="Please Enter only Numbers for Rate" ValidationExpression="[0-9.]*$">*</asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
            <td style="text-align: right; height: 18px;">
                <asp:Label ID="Label9" runat="server" Text="Product Specification :"></asp:Label></td>
                        <td align="left">
                            <asp:TextBox ID="txtProductSpecification" runat="server" CssClass="multilinetext"
                                EnableTheming="False" TextMode="MultiLine" Width="89%"></asp:TextBox></td>
                        <td style="height: 18px; text-align: right">
                            <asp:Label ID="Label5" runat="server" Text="Image :" Width="63px"></asp:Label></td>
                        <td style="text-align: left; height: 18px;">
                            <asp:Image ID="Image1" runat="server" Height="66px" ImageUrl="~/Images/noimage400x300.gif"
                                Width="70px" />
                            <asp:Button id="btnUpload" runat="server" Text="Upload" CausesValidation="False" OnClientClick="window.open('../Masters/ProductMasterImageUpload.aspx','resume','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')"  /></td>
                    </tr>
                    <tr>
                        <td style="text-align: left; height: 20px;" class="profilehead" colspan="4">
                            Product Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="lblItemType" runat="server" Text="Item Type :" Width="72px"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged">
                            </asp:DropDownList><asp:Label ID="Label32" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*">
                            </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="ddlItemType" ErrorMessage="Please Select the Item Type" InitialValue="0"
                                ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label8" runat="server" Text="Item Name :"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:DropDownList ID="ddlItemName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="0">-- Select Item Type --</asp:ListItem>
                            </asp:DropDownList><asp:Label ID="Label33" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*">
                            </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                ControlToValidate="ddlItemName" ErrorMessage="Please Select the Item Name" InitialValue="0"
                                ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="Label6" runat="server" Text="Item Specification :"></asp:Label></td>
                        <td style="height: 19px; text-align: left" colspan="3">
                            <asp:TextBox ID="txtItemSpec" runat="server" CssClass="multilinetext" EnableTheming="False"
                                ReadOnly="True" TextMode="MultiLine" Width="89%"></asp:TextBox></td>
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
                        <td colspan="2" style="height: 19px; text-align: right">
                            <asp:Button ID="btnAddProductDetails" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" Text="Add" ValidationGroup="othercorp" OnClick="btnAddProductDetails_Click" /></td>
                        <td colspan="2" style="height: 19px; text-align: left">
                            <asp:Button ID="btnAddProductDetailsRefresh" runat="server" BackColor="Transparent"
                                BorderStyle="None" CausesValidation="False" CssClass="loginbutton" EnableTheming="False"
                                Text="Refresh" OnClick="btnAddProductDetailsRefresh_Click" /></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 19px; ">
                            &nbsp;<asp:GridView ID="gvInterestedProducts" runat="server" AutoGenerateColumns="False"
                                OnRowDataBound="gvInterestedProducts_RowDataBound" OnRowDeleting="gvInterestedProducts_RowDeleting"
                                OnRowEditing="gvInterestedProducts_RowEditing" style="text-align: center" Width="100%">
                                <Columns>
                                    <asp:CommandField ShowEditButton="True" />
                                    <asp:CommandField ShowDeleteButton="True" />
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ItemType" HeaderText="Item Type">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Specifications" HeaderText="Specifications" NullDisplayText="-">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ItemTypeId" HeaderText="ItemTypeIdHidden" />
                                </Columns>
                            </asp:GridView>
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
                        <td colspan="5" style="text-align: center">
                            <table align="center">
                                <tr>
                                    <td>
                            <asp:Button id="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"  /></td>
                                    <td>
                            <asp:Button id="btnRefresh" runat="server" Text="Refresh" CausesValidation="False" OnClick="btnRefresh_Click" /></td>
                                    <td class="auto-style1">
                            <asp:Button id="btnClose" runat="server" Text="Close" CausesValidation="False" OnClick="btnClose_Click" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    </table>
            </td>
        </tr>
        </table>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
    ShowSummary="False" />