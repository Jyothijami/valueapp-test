<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true" CodeFile="Asset.aspx.cs" Inherits="Modules_HR_Asset" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../select/select2.css" rel="stylesheet" />
    <script src="../../select/select2.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">

    <div class="form-horizontal">
        <div class="panel panel-default">
            <div class="panel-heading">
                   <h6 class="panel-title">Asset Master</h6>
             </div>
            <div class="panel-body">
                <div class="form-group">
                    <table border="0" cellpadding="0" cellspacing="0" align="right">
                            <tr>
                                <td rowspan="3" style="height: 25px">
                                    <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                        Text="Search By" meta:resourcekey="Label14Resource1"></asp:Label></td>
                                <td rowspan="3" style="height: 25px; width: 123px;">
                                    <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="textbox" AutoPostBack="True" meta:resourcekey="ddlSearchByResource1" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                        <asp:ListItem Value="0" meta:resourcekey="ListItemResource1">--</asp:ListItem>
                                        <asp:ListItem Value="Product_Name" meta:resourcekey="ListItemResource2">Asset Name</asp:ListItem>
                                        <asp:ListItem Value="IT_TYPE" meta:resourcekey="ListItemResource3">Sub Category Name</asp:ListItem>
                                        <asp:ListItem Value="ITEM_CATEGORY_NAME">Category Name</asp:ListItem>
                                        <asp:ListItem Value="Status" meta:resourcekey="ListItemResource4">Status</asp:ListItem>
                                        <asp:ListItem Value="EMP_FIRST_NAME" meta:resourcekey="ListItemResource4">Assigned To</asp:ListItem>

                                    </asp:DropDownList></td>
                                <td rowspan="3" style="width: 17px; height: 25px;">
                                    <asp:Label ID="lblSearchtext" runat="server" CssClass="label" Font-Bold="True" Text="Text" Width="37px" EnableTheming="False" meta:resourcekey="lblSearchtextResource1"></asp:Label></td>
                                <td rowspan="3" style="width: 150px; height: 25px;">
                                    <asp:TextBox ID="txtSearchText" runat="server" CssClass="textbox" Width="111px" meta:resourcekey="txtSearchTextResource1"></asp:TextBox><asp:Image ID="imgCurrentDayTasksToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                        Visible="False" meta:resourcekey="imgCurrentDayTasksToDateResource1"></asp:Image></td>
                                <td rowspan="3" style="height: 25px">
                                    <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                        CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnSearchGo_Click" meta:resourcekey="btnSearchGoResource1" /></td>
                            </tr>
                            <tr>
                            </tr>
                            <tr>
                            </tr>
                         <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False" meta:resourcekey="lblSearchItemHiddenResource1"></asp:Label>
                        <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False" meta:resourcekey="lblSearchValueHiddenResource1"></asp:Label></td>
      
                        </table>
                    <asp:GridView ID="gvAsset" SelectedRowStyle-BackColor="#c0c0c0" runat="server" AutoGenerateColumns="False"
                DataSourceID="sdsItemTypeDetails" AllowPaging="True"  AllowSorting="True" meta:resourcekey="gvItemTypeDetailsResource1" Width="100%">
                <Columns>
                    <asp:BoundField DataField="Asset_ID" HeaderText="Sr.No" meta:resourcekey="BoundFieldResource1"></asp:BoundField>
                    <asp:BoundField DataField="Vendor" HeaderText="Vendor" meta:resourcekey="BoundFieldResource2"></asp:BoundField>
                    <asp:TemplateField HeaderText="Product Name" meta:resourcekey="TemplateFieldResource1">
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("Product_Name") %>' ID="TextBox1" meta:resourcekey="TextBox1Resource1"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnAssetName" ForeColor="#0066ff" runat="server" OnClick="lbtnAssetName_Click" Text="<%# Bind('Product_Name') %>" CausesValidation="False" meta:resourcekey="lbtnItemTypeNameResource1"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="ITEM_CATEGORY_NAME" HeaderText="Category Name">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="IT_TYPE" HeaderText="Sub Category Name">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ExpiryDt" HeaderText="Expiry dt" meta:resourcekey="BoundFieldResource3" NullDisplayText="-">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Warrenty" HeaderText="Warranty" meta:resourcekey="BoundFieldResource3" NullDisplayText="-">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                     <asp:BoundField DataField="Location" HeaderText="Asset Location" meta:resourcekey="BoundFieldResource3" NullDisplayText="-">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                     <asp:BoundField DataField="EMP_NAME" HeaderText="Asset Managed By" meta:resourcekey="BoundFieldResource3" NullDisplayText="-">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                </Columns>
                <SelectedRowStyle BackColor="LightSteelBlue" />
                <EmptyDataTemplate>
                    No Record Found
                </EmptyDataTemplate>
            </asp:GridView>
                    <asp:SqlDataSource ID="sdsItemTypeDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                SelectCommand="SP_MASTER_Asset_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName"
                        PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue"
                        PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
                </div>

                <div class ="form-group">
                    <asp:Button ID="btnNew" runat ="server" OnClick ="btnNew_Click" Text="New"  />
                    <asp:Button ID="btnEdit" runat ="server" OnClick ="btnEdit_Click" Text="Edit" />
                    <asp:Button ID="btnDelete" runat ="server" OnClick ="btnDelete_Click" Text ="Delete" /> 
                </div>
            </div>
        </div>
    </div>
             
    <asp:Panel ID="pnlAssetDet" runat="server" Visible ="false"  >
        <div class="form-horizontal">
         <div class="panel panel-default">
             <div class="panel-heading">
                   <h6 class="panel-title">Asset Details</h6>
             </div>
             <div class="panel-body">
                 <div class="form-group">
                     <label class="col-sm-2 control-label text-right">
                         Asset No<asp:Label
                             ID="Label47" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                             Font-Size="Smaller" ForeColor="Red" ></asp:Label></label>
                     <div class="col-sm-4">
                         <asp:TextBox ID="txtAssetNo" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                     <label class="col-sm-2 control-label text-right">
                         Status<asp:Label
                             ID="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                             Font-Size="Smaller" ForeColor="Red" ></asp:Label></label>
                     <div class="col-sm-4">
                         <asp:DropDownList CssClass="form-control" ID="ddlStatus" runat="server">
                             <asp:ListItem>Active</asp:ListItem>
                             <asp:ListItem>In-Active</asp:ListItem>
                         </asp:DropDownList>
                     </div>
                 </div>
                 <div class="form-group">
                     <label class="col-sm-2 control-label text-right">
                         Product<asp:Label
                             ID="Label1" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                             Font-Size="Smaller" ForeColor="Red" ></asp:Label></label>
                     <div class="col-sm-4">
                         <asp:TextBox ID="txtProduct" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                     <label class="col-sm-2 control-label text-right">Description</label>
                     <div class="col-sm-4">
                         <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                 </div>
                 <div class="form-group">
                     <label class="col-sm-2 control-label text-right">
                         Manufacturer<asp:Label
                             ID="Label2" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                             Font-Size="Smaller" ForeColor="Red" ></asp:Label></label>
                     <div class="col-sm-4">
                         <asp:TextBox ID="txtManfacturer" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                     <label class="col-sm-2 control-label text-right">Vendor</label>
                     <div class="col-sm-4">
                         <asp:TextBox ID="txtVendor" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                 </div>
                 <div class="form-group">
                     
                     <label class="col-sm-2 control-label text-right">
                        Asset Category<asp:Label
                             ID="Label6" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                             Font-Size="Smaller" ForeColor="Red" ></asp:Label></label>
                     <div class="col-sm-4">
                         <asp:DropDownList CssClass="form-control" ID="ddlAssetCategory" OnSelectedIndexChanged="ddlAssetCategory_SelectedIndexChanged" AutoPostBack ="true" runat="server"></asp:DropDownList>
                 </div>
                     <label class="col-sm-2 control-label text-right">Asset Sub Category</label>
                     <div class="col-sm-4">
                         <asp:DropDownList CssClass="form-control" ID="ddlProductType" runat="server"></asp:DropDownList>
                     </div>
                     </div>
                 
                 <div class="form-group">
                     <label class="col-sm-2 control-label text-right">
                         Purchase Order No<asp:Label
                             ID="Label5" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                             Font-Size="Smaller" ForeColor="Red" ></asp:Label></label>
                     <div class="col-sm-4">
                         <asp:TextBox ID="txtPONo" runat="server" CssClass="form-control"></asp:TextBox>
                         
                     </div>
                     <label class="col-sm-2 control-label text-right">Purchase On</label>
                     <div class="col-sm-4">
                         <asp:TextBox ID="txtPoDt" runat="server" type="datepic" CssClass="form-control"></asp:TextBox>
                     </div>
                 </div>

                 <div class="form-group">
                     <label class="col-sm-2 control-label text-right">
                         Warranty<asp:Label
                             ID="Label4" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                             Font-Size="Smaller" ForeColor="Red" ></asp:Label></label>
                     <div class="col-sm-4">
                         <asp:TextBox ID="txtWarrenty" runat="server" CssClass="form-control"></asp:TextBox>
                         
                     </div>
                     <label class="col-sm-2 control-label text-right">Expiry Date</label>
                     <div class="col-sm-4">
                         <asp:TextBox ID="txtExpiryDt" runat="server" type="datepic" CssClass="form-control"></asp:TextBox>
                     </div>
                 </div>

                 <div class="form-group">
                     
                         <label class="col-sm-2 control-label text-right">Product Cost</label>
                     <div class="col-sm-4">
                         <asp:TextBox ID="txtCost" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                    
                     <label class="col-sm-2 control-label text-right">Discount</label>
                     <div class="col-sm-4">
                         <asp:TextBox ID="txtDisc" runat="server" CssClass="form-control"></asp:TextBox>

                     </div>
                 </div>
                 <div class="form-group">
                     
                         <label class="col-sm-2 control-label text-right">Total Cost</label>
                     <div class="col-sm-4">
                         <asp:TextBox ID="txtTotalCost" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                    
                     <label class="col-sm-2 control-label text-right">
                         Asset State<asp:Label
                             ID="Label3" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                             Font-Size="Smaller" ForeColor="Red" ></asp:Label></label>
                     <div class="col-sm-4">
                         <asp:DropDownList ID="ddlLocation" CssClass="form-control" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="locsds1" DataTextField="wh_name" DataValueField="wh_name">
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="locsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT DISTINCT [wh_name] from V_INWARDNew"></asp:SqlDataSource>

                     </div>
                 </div>
                 <div class="form-group">
                     
                         <label class="col-sm-2 control-label text-right">Barcode</label>
                     <div class="col-sm-4">
                         <asp:TextBox ID="txtBarcode" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                    
                     <label class="col-sm-2 control-label text-right">Asset Managed By</label>
                     <div class="col-sm-4">
                          <asp:DropDownList ID="ddlReportingTo" CssClass="form-control" runat ="server" ></asp:DropDownList>
                     </div>
                 </div>
                <div class="form-group">
                     <label class="col-sm-2 control-label text-right">Remarks</label>
                     <div class="col-sm-6">
                         <asp:TextBox ID="txtRemarks" runat="server" TextMode ="MultiLine" CssClass="form-control"></asp:TextBox>
                     </div>
                </div>
                  <div class="form-group">

                                <div class="form-actions text-center">
                                    <asp:Button ID="btnSave" runat ="server" Text="Save" OnClick ="btnSave_Click" />
                                </div>
                     </div>
             </div>
         </div>
    </div>
    </asp:Panel>
</asp:Content>

