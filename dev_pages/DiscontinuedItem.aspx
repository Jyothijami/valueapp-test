<%@ Page Title="" Language="C#" MasterPageFile="~/dev_pages/MPage1.master" AutoEventWireup="true" CodeFile="DiscontinuedItem.aspx.cs" Inherits="dev_pages_DiscontinuedItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="page-header">
        <div class="page-title"><h3>Discontinued  Items</h3>
            <div class="row">
                    <div class="col-sm-6">
                        <h4><asp:LinkButton ID="lnkDisc" runat ="server" OnClick="lnkDisc_Click">Click here to get Discontinued Items</asp:LinkButton></h4>
                    </div>
                </div> 
        </div> 
    </div>
    <div class="breadcrumb-line">
       <ul class="breadcrumb">
           <li><a href="../Modules/Masters/ItemMasterDetails.aspx">Item Master </a></li>
           <li class="active">Discontinued Item Details</li>
       </ul>
   </div>
    <asp:Panel ID="pnlUpload" runat ="server">
        <div class="form-horizontal">
        <div class="panel panel-default">
            <div class="panel-body">

                 <div class="form-group">
                    <label class="col-sm-2 control-label text-right">Upload File:</label>
                    <div class="col-sm-4">
                        <asp:FileUpload ID="FileUpload1" type="file" CssClass="styled" runat="server" />
                    </div>
                    <div class="col-sm-4">
                        <asp:Button ID="btnfileUpload" Text="Upload" CssClass="btn btn-danger" OnClick="btnfileUpload_Click" runat="server" />
                        <asp:Button ID="btnUpdate" Text="Update" CssClass ="btn btn-danger " OnClick ="btnUpdate_Click" runat ="server"  />
                    </div>
                  </div>

                </div>
            </div>
           </div>
        <table>
        <tr>
            <td>
                <p>Total No Of Matched excel codes with app codes : &nbsp;&nbsp;</p>
            </td>
            <td>
                <p><asp:Label ID="lblTotalTicketsRaised" Font-Bold="true" runat="server"></asp:Label>
                     <asp:Label Visible ="false"  ID="lblTtl" Font-Bold="true" runat ="server" ></asp:Label></p>
            </td>
        </tr>
                <asp:Label ID="lblBrandID" Visible="false" runat ="server"  ></asp:Label>

                    <tr>
            <td>
                <p>Total No Of not Matched excel Codes with app Codes : &nbsp;&nbsp;</p>
            </td>
            <td>
                <asp:Label ID="Label1" Font-Bold="true" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
        <br />

        <div>
            <asp:GridView ID="gvMatchedItems" runat ="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" Width ="100%"   >
                <Columns>
                    <asp:BoundField DataField ="Item_Code" HeaderText ="Item Code" />
                    <asp:BoundField DataField ="Item_Model_No" HeaderText ="Model No" />
                    <asp:BoundField DataField ="Item_Name" HeaderText ="Series" />
                    <asp:BoundField DataField ="Item_Spec" HeaderText ="Description" />
                    <%--<asp:BoundField DataField ="Item_Code" HeaderText ="Item Code" />--%>

                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="select  Item_Code,YANTRA_ITEM_MAST.Item_Model_No,Item_Name,Item_Spec,F2 from Discontinued_Items inner join YANTRA_ITEM_MAST on Discontinued_Items.Item_model_no=YANTRA_ITEM_MAST .ITEM_MODEL_NO where YANTRA_ITEM_MAST.F2 is  null "></asp:SqlDataSource>
        
        <asp:GridView ID="gvNotMatched" runat="server" Visible ="false"  DataSourceID="SqlDataSource3" AutoGenerateColumns="False" Width ="100%"    >
            <Columns> 
                    <asp:BoundField DataField ="Item_Model_No" HeaderText ="Model No" />
                    <asp:BoundField DataField ="Brand" HeaderText ="Series" />
                    <%--<asp:BoundField DataField ="Item_Code" HeaderText ="Item Code" />--%>

                </Columns>
        </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="select  * from Discontinued_Items where Discontinued_Items.item_Model_No  not in (select Item_model_no from YANTRA_ITEM_MAST where ITEM_MODEL_NO is not null )"></asp:SqlDataSource>
        
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlDiscon" runat ="server" Visible ="false"  >
        <div class="breadcrumb-line">
       <ul class="breadcrumb">
           <li><a href="DiscontinuedItem.aspx">Discontinued Items Upload </a></li>
           <li class="active">Discontinued Item Details</li>
       </ul>
   </div>
         <table>
        <tr>
            <td>
                <p>Total No Of Discontinued Items count in item Master : &nbsp;&nbsp;</p>
            </td>
            <td>
                <p><asp:Label ID="Label2" Font-Bold="true" runat="server"></asp:Label>
                     <asp:Label Visible ="false"  ID="Label3" Font-Bold="true" runat ="server" ></asp:Label></p>
            </td>
            </tr> 
             </table> 
         <asp:GridView ID="GridView1" runat ="server" DataSourceID="SqlDataSource2" AutoGenerateColumns="False" Width ="100%"   >
                <Columns>
                    <asp:BoundField DataField ="Item_Code" HeaderText ="Item Code" />
                    <asp:BoundField DataField ="Item_Model_No" HeaderText ="Model No" />
                    <asp:BoundField DataField ="Item_Name" HeaderText ="Series" />
                    <asp:BoundField DataField ="Item_Spec" HeaderText ="Description" />
                    <%--<asp:BoundField DataField ="Item_Code" HeaderText ="Item Code" />--%>

                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT * FROM YANTRA_ITEM_MAST WHERE F2='Discontinued'"></asp:SqlDataSource>
    </asp:Panel>
</asp:Content>

