<%@ Page Title="" Language="C#" MasterPageFile="~/dev_pages/MPage1.master" AutoEventWireup="true" CodeFile="ExcelPriceUodate.aspx.cs" Inherits="dev_pages_ExcelPriceUodate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <style>

        div.scroll { 
                margin:4px, 4px; 
                padding:4px; 
               
                /*background-color: green;*/ 
                /*width: 500px; */
                height: 800px; 
                overflow-x: hidden;  
                overflow-x: auto; 
                text-align:justify; 
            } 
    </style>

 


    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
        <ContentTemplate>
             <div class="page-header">
                <div class="page-title"><h3>Price  Update</h3>
                    <div class="row">
                    <div class="col-sm-6">
                        <h4><asp:LinkButton ID="lnkExcel" runat ="server" OnClick ="lnkExcel_Click">Select Excel Files to Update</asp:LinkButton></h4>
                    </div>
                    <div class="col-sm-6">
                        <h4><asp:LinkButton ID="lnkBalcheck" runat ="server" OnClick ="lnkBalcheck_Click">Check lastdate of price Updates</asp:LinkButton></h4>
                    </div>
                    <h4><asp:LinkButton ID="lnkDifCheck" Visible ="false"  runat ="server" OnClick ="lnkDifCheck_Click">Check Differnce of File Updates</asp:LinkButton></h4>
                        </div>
                </div>
            </div>

            <div class="breadcrumb-line">
                <ul class="breadcrumb">
                    <li><a href="ExcelPriceUodate.aspx">Price Update </a></li>
                    <li class="active">Price</li>
                </ul>
            </div>

            <asp:Panel ID="pnlUpload" runat ="server" >
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
                <p>Total No Of not Matched excel codes with app codes : &nbsp;&nbsp;</p>
            </td>
            <td>
                <p><asp:Label ID="lblTotalTicketsRaised" Font-Bold="true" runat="server"></asp:Label>
                     <asp:Label Visible ="false"  ID="lblTtl" Font-Bold="true" runat ="server" ></asp:Label></p>
            </td>
        </tr>
                <asp:Label ID="lblBrandID" Visible="false" runat ="server"  ></asp:Label>

                    <tr>
            <td>
                <p>Total No Of  Matched excel Prices with app Prices : &nbsp;&nbsp;</p>
            </td>
            <td>
                <asp:Label ID="Label1" Font-Bold="true" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
        <div>
        <asp:GridView ID="gvExcel" runat ="server" AutoGenerateColumns="False" OnRowDataBound ="gvExcel_RowDataBound"  DataSourceID="SqlDataSource1" Width ="100%" >
            <Columns>
                <asp:BoundField DataField="ItemCode" HeaderText="ItemCode" SortExpression="ItemCode" />
                <asp:BoundField DataField="Series" HeaderText="Series" SortExpression="Series" />
                <asp:BoundField DataField="ModelNo" HeaderText="ModelNo" SortExpression="ModelNo" />
                <asp:BoundField DataField="Item_Price" HeaderText="Price" SortExpression="Item_Price" />
                <asp:BoundField DataField="MulFactor" HeaderText="Factor" SortExpression="MulFactor" />
                <asp:BoundField DataField="AppPrice" HeaderText="AppPrice" SortExpression="AppPrice" />
                <asp:BoundField DataField="ExcelPrice" HeaderText="ExcelPrice" SortExpression="ExcelPrice" />
                <asp:BoundField DataField="Brand_ID" HeaderText="Brand_ID" SortExpression="Brand_ID" />
                <asp:BoundField DataField="filename" HeaderText="filename" SortExpression="filename" />

            </Columns>
        </asp:GridView>
        <%--<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT YANTRA_ITEM_MAST.ITEM_CODE AS ItemCode, YANTRA_ITEM_MAST.ITEM_NAME AS Series, Excel_Price.Item_Model_No AS ModelNo, Excel_Price.Price AS ExcelPrice, YANTRA_ITEM_PRICE.GrossAmount AS AppPrice,YANTRA_ITEM_PRICE.Item_Price ,Item_Price as AppMRP,Item_Price ,MulFactor FROM YANTRA_ITEM_MAST INNER JOIN Excel_Price ON YANTRA_ITEM_MAST.ITEM_MODEL_NO = Excel_Price.Item_Model_No INNER JOIN YANTRA_ITEM_PRICE ON YANTRA_ITEM_MAST.ITEM_CODE = YANTRA_ITEM_PRICE.Item_Code where   Excel_Price.Price !=YANTRA_ITEM_PRICE.GrossAmount Or Excel_Price .Price !=YANTRA_ITEM_PRICE .Item_Price "></asp:SqlDataSource>--%>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT YANTRA_ITEM_MAST.ITEM_CODE AS ItemCode, YANTRA_ITEM_MAST.ITEM_NAME AS Series, Excel_Price.Item_Model_No AS ModelNo, Excel_Price.Price AS ExcelPrice, YANTRA_ITEM_PRICE.GrossAmount AS AppPrice,YANTRA_ITEM_PRICE.Item_Price ,Item_Price as AppMRP,Item_Price ,MulFactor,YANTRA_ITEM_MAST.Brand_Id,filename FROM YANTRA_ITEM_MAST INNER JOIN Excel_Price ON YANTRA_ITEM_MAST.ITEM_MODEL_NO = Excel_Price.Item_Model_No INNER JOIN YANTRA_ITEM_PRICE ON YANTRA_ITEM_MAST.ITEM_CODE = YANTRA_ITEM_PRICE.Item_Code where   Excel_Price.Price !=YANTRA_ITEM_PRICE.GrossAmount "></asp:SqlDataSource>
    </div>
                <div>
                    
        <asp:GridView ID="GridView1" Visible ="false"  runat ="server" AutoGenerateColumns="False" OnRowDataBound ="GridView1_RowDataBound"   DataSourceID="SqlDataSource4" Width ="100%" >
            <Columns>
                <asp:BoundField DataField="ItemCode" HeaderText="ItemCode" SortExpression="ItemCode" />
                <asp:BoundField DataField="Series" HeaderText="Series" SortExpression="Series" />
                <asp:BoundField DataField="ModelNo" HeaderText="ModelNo" SortExpression="ModelNo" />
                <asp:BoundField DataField="Item_Price" HeaderText="Price" SortExpression="Item_Price" />
                <asp:BoundField DataField="MulFactor" HeaderText="Factor" SortExpression="MulFactor" />
                <asp:BoundField DataField="AppPrice" HeaderText="AppPrice" SortExpression="AppPrice" />
                <asp:BoundField DataField="ExcelPrice" HeaderText="ExcelPrice" SortExpression="ExcelPrice" />
                <asp:BoundField DataField="Brand_ID" HeaderText="Brand_ID" SortExpression="Brand_ID" />

            </Columns>
        </asp:GridView>
        <%--<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT YANTRA_ITEM_MAST.ITEM_CODE AS ItemCode, YANTRA_ITEM_MAST.ITEM_NAME AS Series, Excel_Price.Item_Model_No AS ModelNo, Excel_Price.Price AS ExcelPrice, YANTRA_ITEM_PRICE.GrossAmount AS AppPrice,YANTRA_ITEM_PRICE.Item_Price ,Item_Price as AppMRP,Item_Price ,MulFactor FROM YANTRA_ITEM_MAST INNER JOIN Excel_Price ON YANTRA_ITEM_MAST.ITEM_MODEL_NO = Excel_Price.Item_Model_No INNER JOIN YANTRA_ITEM_PRICE ON YANTRA_ITEM_MAST.ITEM_CODE = YANTRA_ITEM_PRICE.Item_Code where   Excel_Price.Price !=YANTRA_ITEM_PRICE.GrossAmount Or Excel_Price .Price !=YANTRA_ITEM_PRICE .Item_Price "></asp:SqlDataSource>--%>

        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT YANTRA_ITEM_MAST.ITEM_CODE AS ItemCode, YANTRA_ITEM_MAST.ITEM_NAME AS Series, Excel_Price.Item_Model_No AS ModelNo, Excel_Price.Price AS ExcelPrice, YANTRA_ITEM_PRICE.GrossAmount AS AppPrice,YANTRA_ITEM_PRICE.Item_Price ,Item_Price as AppMRP,Item_Price ,MulFactor,YANTRA_ITEM_MAST.Brand_Id FROM YANTRA_ITEM_MAST INNER JOIN Excel_Price ON YANTRA_ITEM_MAST.ITEM_MODEL_NO = Excel_Price.Item_Model_No INNER JOIN YANTRA_ITEM_PRICE ON YANTRA_ITEM_MAST.ITEM_CODE = YANTRA_ITEM_PRICE.Item_Code where Excel_Price .Price =YANTRA_ITEM_PRICE .GrossAmount  "></asp:SqlDataSource>
    </div>
             </asp:Panel> 
            <asp:Panel ID="pnlbal" runat ="server" Visible ="false" >
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="form-group">
                    <label class="col-sm-2 control-label text-right">Model No.</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtSearchModel" CssClass ="form-control" runat="server"> </asp:TextBox>
<asp:Button ID="btnSearchModelNo" runat="server" BorderStyle="None" CausesValidation="False" CssClass="gobutton" EnableTheming="False" OnClick="btnSearchModelNo_Click1" Text="Go" />
<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
<SelectParameters>
<asp:Parameter Type="Int64" DefaultValue="0" Name="BranbId"></asp:Parameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtSearchModel"></asp:ControlParameter>
</SelectParameters>
</asp:SqlDataSource>
                    </div>
                                    
                    </div>
                  
                                
                                <div class="form-group">
                                    <label class="col-sm-2 control-label text-right">Brand :</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlBrand1" CssClass ="form-control" Width ="100%" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand1_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <label class="col-sm-2 control-label text-right">Category :</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlCategory1" CssClass ="form-control" Width ="100%" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory1_SelectedIndexChanged"></asp:DropDownList>                                    
                                    </div>
                                     </div>
                                     
                                <div class="form-group">
                                    <label class="col-sm-2 control-label text-right">Sub Category :</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlSubCategory1" CssClass ="form-control" Width ="100%" runat="server" OnSelectedIndexChanged="ddlSubCategory1_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                    <label class="col-sm-2 control-label text-right">Model No :</label>
                                    <div class="col-sm-4">
                                       <asp:DropDownList ID="ddlModelNo1" CssClass ="form-control" Width ="100%" runat="server"></asp:DropDownList> 
                                    </div>
                                </div>
                            <div class="form-group">
                                <div class="col-sm-8">
                                    <label class="col-sm-2 control-label text-right"></label>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                </div>
                                    <label class="col-sm-2 control-label text-right">No Of Records :</label>

                                <div class="col-sm-2">
                                     <asp:DropDownList ID="ddlNoOfRecord2" runat="server" OnSelectedIndexChanged="ddlNoOfRecord2_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>25</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>75</asp:ListItem>
                            <asp:ListItem>100</asp:ListItem>
                        </asp:DropDownList>

                                </div>
                            </div>
                   
                        </div>
                        <div>
                            <asp:GridView ID="gvItemPriceUpdate" runat ="server" OnPageIndexChanging ="gvItemPriceUpdate_PageIndexChanging" AutoGenerateColumns="False" Width="100%" AllowPaging="True" >
                                <Columns >
                                    <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
                                    <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="Model No" />
                                    <asp:BoundField DataField="Item_Name" HeaderText="Item_Name" SortExpression="Item_Name" />
                        <asp:BoundField DataField="GrossAmount" HeaderText="Gross Amount" SortExpression="GrossAmount" />
                        <asp:BoundField DataField="MulFactor" HeaderText="MulFactor" />
                        <asp:BoundField DataField="Item_Price" HeaderText="Item Price" SortExpression="Item_Price" />

                        <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />

                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </asp:Panel>


          

            <asp:Panel ID="pnlexcl" runat ="server" Visible ="false" >
                <div class="form-horizontal">
        <div class="panel panel-default">
            <div class="panel-body">

                 <div class="form-group">
                    <label class="col-sm-4 control-label text-right">Select Uploaded Excel File</label>
                    <div class="col-sm-6">
                        <asp:DropDownList ID="ddlExcel" AutoPostBack ="true"  CssClass="form-control"  OnSelectedIndexChanged ="ddlExcel_SelectedIndexChanged" runat ="server" ></asp:DropDownList>
                   
                         </div>
                   <div class ="col-sm-2">
                     <asp:Button ID="btnPriceUpdate" runat="server" Text="Price Update" OnClick="btnPriceUpdate_Click" />

                   </div>
                  </div>
               <hr />
                 <div class="row">
                <div class="col-md-9 scroll" >
                    <h4>App Prices yet to be update count of <asp:Label ID="lblappcount" runat ="server" ></asp:Label> 
                        Out Of <asp:Label ID="lblAppttl" runat ="server" ></asp:Label></h4><hr />
                        <asp:GridView Width ="100%" ID="gvExcelFile" AutoGenerateColumns ="False" OnRowDataBound ="gvExcelFile_RowDataBound"  runat ="server">
                            <columns>
                                
                                <asp:BoundField DataField ="Item_Code" HeaderText ="Item Code" SortExpression ="ITEM_CODE" />
                                <asp:BoundField DataField ="ModelNo" HeaderText ="ModelNo" SortExpression ="ModelNo" />
                                <asp:BoundField DataField ="Item_Spec" HeaderText ="Desc" SortExpression ="Item_Spec" />

                                <asp:BoundField DataField ="Brand" HeaderText ="Brand" SortExpression ="Brand" />
                                <asp:BoundField DataField ="GrossAmount" HeaderText ="Gross Amt" SortExpression ="GrossAmount" />
                                <asp:BoundField DataField ="MULFactor" HeaderText ="Factor" SortExpression ="MULFactor" />
                                
                                <asp:TemplateField HeaderText ="Price"  >
                                    <ItemTemplate >
                                        <asp:TextBox ID="txtPrice" runat ="server" Text='<%# Bind("Item_Price") %>' CssClass ="form-control" Width ="70%"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField ="Updated_dt" HeaderText ="lst Updated" SortExpression ="Updated_dt" />
                                <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkhdr" runat="server" AutoPostBack="True" OnClick="selectAll(this)" />
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="Chk" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                <asp:BoundField DataField ="F2" HeaderText ="Discontinued" />
                            </columns>
                        </asp:GridView>

                </div>
                 <div class="col-md-3 scroll">
                    <h4>Excel Prices list count of <asp:Label ID="lblExcelCount" runat ="server" ></asp:Label></h4><hr />

                         <asp:GridView ID="gvExl"  Width ="100%" runat ="server" ></asp:GridView>

                </div>


            </div>
                </div>
            </div>
           </div>
            </asp:Panel>








            <asp:Panel ID ="pnlDiff" runat ="server" Visible ="false" >
                <table>
                    <tr>
            <td>
                <p>Total No Of  App codes and not in Excel  : &nbsp;&nbsp;</p>
            </td>
            <td>
                <asp:Label ID="Label3" Font-Bold="true" runat="server"></asp:Label>

            </td>
        </tr>
        <tr>
            <td>
                <p>Total No Of  not uploaded codes from excel to app : &nbsp;&nbsp;</p>
            </td>
            <td>
                <asp:Label ID="Label2" Font-Bold="true" runat="server"></asp:Label>

            </td>
        </tr>
                    
    </table>
                <table>
                        <tr>
                            
                            <td>
                <asp:GridView ID="gvAppDiff" runat ="server" AutoGenerateColumns="False"   >
                    <Columns>
                    <asp:BoundField DataField="ITEM_CODE" HeaderText="ITEM_CODE" SortExpression="ITEM_CODE" />
                    <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="ITEM_MODEL_NO" SortExpression="ITEM_MODEL_NO" />
                    <asp:BoundField DataField="ITEM_PRICE" HeaderText="ITEM_PRICE" SortExpression="ITEM_PRICE" />
                </Columns>
                    <EmptyDataTemplate >No Records To Display</EmptyDataTemplate>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="select YANTRA_ITEM_MAST.ITEM_CODE ,ITEM_MODEL_NO,Item_Price from YANTRA_ITEM_MAST ,YANTRA_ITEM_PRICE 
                where ITEM_MODEL_NO not in (select Item_Model_No from excel_price)
                and YANTRA_ITEM_MAST .ITEM_CODE =YANTRA_ITEM_PRICE .Item_Code and YANTRA_ITEM_MAST.Brand_Id =@Brand_ID ">
                   <SelectParameters>
                            <asp:ControlParameter ControlID="lblBrandId" Name="Brand_ID" PropertyName="Text" Type="Int64" />
                        </SelectParameters>
                </asp:SqlDataSource>
                            </td>
                            
                        </tr>
                    </table>
                      <table >
                          <tr>
                              <td>
                                 <asp:GridView ID="gvDiff" runat ="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource3" >

                    <Columns>
                        <asp:BoundField DataField="Item_Model_No" HeaderText="Item_Model_No" SortExpression="Item_Model_No" />
                        <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
                    </Columns>

                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT Excel_Price.ITEM_MODEL_NO, [Price] FROM [Excel_Price] RIGHT outer join YANTRA_ITEM_MAST on YANTRA_ITEM_MAST .ITEM_MODEL_NO =Excel_Price .Item_Model_No where YANTRA_ITEM_MAST.ITEM_MODEL_NO is null"></asp:SqlDataSource>

                            </td>
                          </tr>
                      </table>     
            </asp:Panel>


              </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnfileUpload" />
        </Triggers>
    </asp:UpdatePanel>
   
</asp:Content>

