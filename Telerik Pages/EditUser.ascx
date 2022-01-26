<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditUser.ascx.cs" Inherits="Telerik_Pages_EditUser" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<link href="../css/bootstrap.min.css" rel="stylesheet" />
<link href="../css/styles.css" rel="stylesheet" />
<link href="../css/londinium-theme.css" rel="stylesheet" />
<div class="form-horizontal ">
    <div class ="panel panel-default ">
        <div class ="panel-body">
            <div class="panel panel-default">
                <div class="panel-heading">
                        <h6 class="panel-title">Item Details</h6>
                    </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label class="col-sm-1 control-label text-right">Search Model No :</label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtSearchModel" runat="server" CssClass="form-control " Enabled ="false" ></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                ServiceMethod="AutoCompleteAjaxRequest"
                                ServicePath="~/Search.asmx"
                                MinimumPrefixLength="2"
                                CompletionInterval="100"
                                EnableCaching="false"
                                CompletionSetCount="10"
                                TargetControlID="txtSearchModel"
                                FirstRowSelected="false">
                            </cc1:AutoCompleteExtender>
                            <asp:Button ID="btnSearchModelNo"
                                runat="server" BorderStyle="None" CausesValidation="False" Enabled ="false"  OnClick="btnSearchModelNo_Click"
                                EnableTheming="False" Text="Go" ValidationGroup="Search"
                                 />
                                    <asp:Label ID="lblItemCode" runat="server" Visible="False"></asp:Label>

                        </div>
                        <label class="col-sm-1 control-label text-right">Search By Brand :</label>
                        <div class="col-sm-2">
                            <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="True" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <label class="col-sm-1 control-label text-right">Model Name :</label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtItemName" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <label class="col-sm-1 control-label text-right">Item Category :</label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtItemCategory" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-1 control-label text-right">Select Model :</label>
                        <div class="col-sm-2">
                            <asp:RadioButton ID="rdbAll" runat="server" Text="All" AutoPostBack="True" GroupName="a" OnCheckedChanged ="rdbAll_CheckedChanged" Checked="false"></asp:RadioButton>
                            <asp:RadioButton ID="rdbOnlyfromLead" runat="server" Text="Only from Lead" AutoPostBack="True" OnCheckedChanged ="rdbOnlyfromLead_CheckedChanged" GroupName="a" Checked="false"></asp:RadioButton>
                        </div>
                        <label class="col-sm-1 control-label text-right">Model No :</label>
                        <div class="col-sm-2">
                            <asp:DropDownList ID="ddlModelNo" Enabled="false" Width="100%" TabIndex="2" CssClass="form-control " OnSelectedIndexChanged ="ddlModelNo_SelectedIndexChanged" runat="server"></asp:DropDownList>
                        </div>
                        <label class="col-sm-1 control-label text-right">Brand :</label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtBrand" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <label class="col-sm-1 control-label text-right">Item SubCategory :</label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtItemSubCategory" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-1 control-label text-right">Essentials :</label>
                        <div class="col-sm-2">
                            <asp:CheckBoxList ID="chklitemcolor" runat="server" CellPadding="0" CellSpacing="0"
                                RepeatColumns="4" RepeatDirection="Horizontal" AutoPostBack="True">
                            </asp:CheckBoxList>
                            <asp:DropDownList ID="ddlEssentials" CssClass ="form-control " runat="server" AutoPostBack="True"></asp:DropDownList>
                        </div>
                        <label class="col-sm-1 control-label text-right">Color :</label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtColor" runat="server" ReadOnly="True" Visible="False"></asp:TextBox>
                            <asp:DropDownList ID="ddlColor" CssClass ="form-control" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvColor" runat="server" ControlToValidate="ddlColor" InitialValue="0" ErrorMessage="Please Select Item Color" Text="*" ValidationGroup="qi"></asp:RequiredFieldValidator>
                        </div>
                        <label class="col-sm-1 control-label text-right">Room :</label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtRoom" Text='<%# Bind( "QUOT_ROOM") %>' CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        
                        <label class="col-sm-1 control-label text-right">Item Specification :</label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtItemSpec" runat="server" CssClass="multilinetext" EnableTheming="False" ReadOnly="True" TextMode="MultiLine" Width="300px"></asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="form-group">
                        <label class="col-sm-1 control-label text-right">Quantity :</label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtQunatity" Text='<%# Bind( "QUOT_DET_QTY") %>' CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <label class="col-sm-1 control-label text-right">Rate :</label>
                        <div class="col-sm-2">
                           <asp:DropDownList ID="ddlRate" runat="server" EnableTheming="False" Width="67px" CssClass="dropdownlist" Enabled="False">
                                    </asp:DropDownList><asp:TextBox ID="txtRate" Text='<%# Bind( "QUOT_RATE") %>' runat="server" Width="88px" CssClass="textboxqt" EnableTheming="False"></asp:TextBox>
                        </div>
                        <label class="col-sm-1 control-label text-right">Floor :</label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtFloor" Text='<%# Bind( "QUOT_FLOOR") %>' CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <label class="col-sm-1 control-label text-right">UOM :</label>
                        <div class="col-sm-2">
                             <asp:TextBox ID="txtItemUOM" CssClass ="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                        </div>
                        
                    </div>
                    <div class="form-group">
                        <label class="col-sm-1 control-label text-right">Discount :</label>
                        <div class="col-sm-2">
                        <asp:TextBox ID="txtDiscount" Text='<%# Bind( "QUOT_DISC") %>' CssClass ="form-control " runat="server">0</asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server"
                                        ControlToValidate="txtDiscount" ErrorMessage="Please Enter the Discount" ValidationGroup="qi">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                            ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtDiscount" ValidChars=".0123456789">
                                        </cc1:FilteredTextBoxExtender>
                            </div> 
                        <label class="col-sm-1 control-label text-right">Special Price :</label>
                        <div class="col-sm-2">
                        <asp:TextBox ID="txtSpPrice" Text='<%# Bind( "QUOT_SPPRICE") %>' CssClass ="form-control " runat="server"></asp:TextBox>&nbsp;
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server"
                                        ControlToValidate="txtSpPrice" ErrorMessage="Please Enter the Special Price"
                                        ValidationGroup="qi">*</asp:RequiredFieldValidator>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2"
                                        runat="server" TargetControlID="txtSpPrice" ValidChars=".0123456789">
                                    </cc1:FilteredTextBoxExtender>
                            </div> 
                        <label class="col-sm-1 control-label text-right">GST (%) :</label>
                        <div class="col-sm-2">
                        <asp:TextBox ID="txtGST_Perc" Text='<%# Bind( "QUOT_DET_GST") %>' CssClass ="form-control " runat="server"></asp:TextBox>&nbsp;
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server"
                                        ControlToValidate="txtGST_Perc" ErrorMessage="Please Enter the GST" ValidationGroup="qi">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                            ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtGST_Perc" ValidChars=".0123456789">
                                        </cc1:FilteredTextBoxExtender>
                            </div> 
                        <label class="col-sm-1 control-label text-right">GST Amount :</label>
                        <div class="col-sm-2">
                        <asp:TextBox ID="txtGST_Amt" Text='<%# Bind( "QUOT_DET_GSTRATE") %>' CssClass ="form-control " runat="server"></asp:TextBox>&nbsp;
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server"
                                        ControlToValidate="txtGST_Amt" ErrorMessage="Please Enter the GST Tax Amount"
                                        ValidationGroup="qi">*</asp:RequiredFieldValidator>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10"
                                        runat="server" TargetControlID="txtGST_Amt" ValidChars=".0123456789">
                                    </cc1:FilteredTextBoxExtender>
                            </div> 
                    </div>
                    <div class="form-group">
                        <label class="col-sm-1 control-label text-right">Item Image :</label>
                        <div class="col-sm-2">
                            <asp:Image ID="Image1" runat="server" Height="85px" ImageUrl="~/Images/noimage400x300.gif" Width="140px"></asp:Image>
                        </div>
                        <label class="col-sm-1 control-label text-right">Technical Drawings :</label>
                        <div class="col-sm-2">
                            <asp:Image ID="Image2" runat="server" Height="85px" ImageUrl="~/Images/noimage400x300.gif" Width="140px"></asp:Image>
                        </div>
                        <label class="col-sm-1 control-label text-right">Technical Drawings :</label>
                        <div class="col-sm-2">
                           <asp:TextBox ID="txtSrlOrderNo" Text='<%# Bind( "Quot_OrderNo") %>' CssClass ="form-control " runat="server"></asp:TextBox>
                        </div>
                        <asp:Button ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>&nbsp;
                                    <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                        CommandName="Cancel"></asp:Button>
                    </div>
                   
                </div>
                <asp:SqlDataSource
                ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:Parameter Type="Int64" DefaultValue="0" Name="BranbId"></asp:Parameter>
                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue"
                        ControlID="txtSearchModel"></asp:ControlParameter>
                </SelectParameters>
            </asp:SqlDataSource>
            </div>
        </div>
    </div>
</div>