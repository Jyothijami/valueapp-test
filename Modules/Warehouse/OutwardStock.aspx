<%@ Page Title="|| Value App : Warehouse : Outward Stock ||" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="OutwardStock.aspx.cs" Inherits="Modules_Warehouse_OutwardStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../../js/jquery-1.9.1.js"></script>
    <script>
        function SellectAll(e) {
            // alert(e.childNodes[0].checked);
            $('#<%=gvQuotDeatils.ClientID%> tr').find('td:eq(0)').find('input[type="checkbox"]').prop('checked', e.childNodes[0].checked);

        }
        function EnableQuantity(e) {
            if (e.childNodes[0].checked) {
                $(e).parent('td').next('td').find('input[type="text"]').prop('disabled', false);
            }
            else {
                $(e).parent('td').next('td').find('input[type="text"]').prop('disabled', true).val('');
            }
            
        }
        function CheckRange(e) {
            //alert($(e).parent('td').parent('tr').find('td:eq(10)').html() * 1);
            var rowCells = $(e).parent('td').parent('tr').find('td');
            //alert(rowCells[10].innerText);
            if (rowCells[10].innerText * 1 < e.value) {
                alert("dispatch items should be less than or equal to stock available");
                e.focus();
                return false;
            }
            else {
                //debugger;
                var table = document.getElementById('<%=gvQuotDeatils.ClientID%>');
                for (var i = 1,row; row = table.rows[i];i++) {
                    //alert(QuotRow.innerHTML);
                    //var cells = QuotRow.getElementsByTagName('td');
                    //alert(row.cells[1].innerText + ' == ' + rowCells[3].innerText + ' && ' + row.cells[2].innerText + ' == ' + rowCells[6].innerText + ' && ' + row.cells[3].innerText + ' == ' + rowCells[5].innerText);
                    if (row.cells[1].innerText==rowCells[3].innerText&&row.cells[2].innerText==rowCells[6].innerText&&row.cells[3].innerText==rowCells[5].innerText) {
                        //alert(row.cells[5].innerText + '---' + row.cells[5].innerText * 1 + e.value);

                        row.cells[5].innerText = row.cells[5].innerText * 1 + e.value;
                    }

                }
                return true;
            }
        }
        function Validate() {
            if (isNaN($('#<%=txtQuotation.ClientID%>').val()) || $.trim($('#<%=txtQuotation.ClientID%>').val()) == "") {
                alert("Quotation Number is Invalid"); $('#<%=txtQuotation.ClientID%>').focus();
                return false;
            }
            if ($.trim($('#<%=txtComments.ClientID%>').val())=="") {
                alert("please enter comments"); $('#<%=txtComments.ClientID%>').focus(); return false;
            }
            return true;
        }
        function CheckQuotNo() {
            if (isNaN($('#<%=txtQuotation.ClientID%>').val()) || $.trim($('#<%=txtQuotation.ClientID%>').val()) == "") {
                alert("Quotation Number is Invalid"); $('#<%=txtQuotation.ClientID%>').focus();
                 return false;
            }
            return true;
        }
    </script>
    <style>
        .DispatchQuantity{  
            width:100px !important;            
        }
    </style>
    <!--overriding the boot strap margin-bottom style to 0 pixels-->
    <style>
        select, textarea, input[type="text"], input[type="password"], input[type="datetime"], input[type="datetime-local"], input[type="date"],
        input[type="month"], input[type="time"], input[type="week"], input[type="number"], input[type="email"], input[type="url"],
        input[type="search"], input[type="tel"], input[type="color"], .uneditable-input {
            margin-bottom: auto !important;
        }
        label{display:inline-block !important;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <asp:Panel runat="server" ID="pnlContet" Visible="false"></asp:Panel>
    <div id="divContent" runat="server">
        <div id="divQuotation">
            <table>
                <tr>
                    <td>Enter Indent NO</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtQuotation"/>  </td>
                    <td>
                        <asp:Button Text="Get Details" ID="btnGetDetails" runat="server" OnClientClick="return CheckQuotNo();" OnClick="btnGetDetails_Click"/></td>
                </tr>
            </table>
        </div>
        <hr />
        <div id="divQuotDetails" runat="server" visible="false">
            <table>
                <tr>
                    <td style="padding-right:20px;vertical-align:top;">
                        <asp:GridView runat="server" ID="gvQuotDeatils" AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox Text="All" ID="chkAll" runat="server" onchange="javscript:SellectAll(this)"/>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox Text="" ID="chk" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                <asp:BoundField DataField="Color" HeaderText="Color" />
                                <asp:BoundField DataField="Brand" HeaderText="Brand" />
                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                <asp:TemplateField HeaderText="Dispatch Quantity" Visible="false">
                                    <ItemTemplate>
                                        0
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td style="vertical-align:top;""><asp:Button Text="Get Stock" ID="btnGetStock" runat="server" OnClick="btnGetStock_Click"/></td>
                    <td style="vertical-align:top;padding-left:20px;">
                        <table>
                            <tr>
                                <td>Store Address</td> <td><asp:TextBox runat="server" ID="txtStoreAddress" TextMode="MultiLine" Rows="4"/></td>                        
                            </tr><tr>
                                <td>Comments</td><td><asp:TextBox runat="server" ID="txtComments" TextMode="MultiLine"/></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td><asp:Button Text="Dispatch Items" ID="btnDispatchItems" OnClientClick="return Validate();" OnClick="btnDispatchItems_Click" runat="server" Visible="false"/>
                                </td>

                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <hr />
        <div id="divWh_Items" runat="server" visible="false">
            <asp:GridView runat="server" ID="gvWH_Items" AutoGenerateColumns="false" Width="100%">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox Text="All" ID="chkAll" runat="server" onchange="javscript:SellectAll(this)" Visible="false"/>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox Text="" ID="chk" runat="server" onchange="javascript:return EnableQuantity(this);"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Dispatch Quantity">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtDispatchQuantity" Enabled="false" CssClass="DispatchQuantity" onblur="CheckRange(this);"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ItemID" HeaderText="Item ID" />
                    <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                    <asp:BoundField DataField="ProductCode" HeaderText="Product Code" />
                    <asp:BoundField DataField="Brand" HeaderText="Brand" />
                    <asp:BoundField DataField="Color" HeaderText="Color" />
                    <asp:BoundField DataField="OpeningStock" HeaderText="Opening Stock" />
                    <asp:BoundField DataField="ClosingStock" HeaderText="Closing Stock" />
                    <asp:BoundField DataField="DefectiveStock" HeaderText="Defective Stock" />
                    <asp:BoundField DataField="StockAvailable" HeaderText="Stock Available" />
                    <asp:BoundField DataField="StockLocation" HeaderText="Stock Location" />
                </Columns>
            </asp:GridView>
        </div>
        <div id="divSubmit">
            
        </div>
    </div>
    
</asp:Content>


 
