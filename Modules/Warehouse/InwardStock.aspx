<%@ Page Title="|| Value App : Warehouse : Inward Stock ||" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="InwardStock.aspx.cs" Inherits="Modules_Warehouse_InwardStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/jquery-1.9.1.js"></script>
    <script>
        function SellectAll(e) {
           // alert(e.childNodes[0].checked);
                $('#<%=gvMrnItems.ClientID%> tr').find('td:eq(0)').find('input[type="checkbox"]').prop('checked', e.childNodes[0].checked);
            
        }
    </script>
    <style>
        .LocBreadCrumb {
            list-style: none;
            overflow: hidden;
            font: 18px Helvetica, Arial, Sans-Serif;
        }

        .LocBreadCrumb {
            float: left;
        }

        .LocBreadCrumb a {
            color: white;
            text-decoration: none;
            padding: 7px 0px 7px 55px;
            background: brown; /* fallback color */
            background: hsla(34,85%,35%,1);
            position: relative;
            display: block;
            float: left;
        }

        .LocBreadCrumb a:after {
            content: " ";
            display: block;
            width: 0;
            height: 0;
            border-top: 50px solid transparent; /* Go big on the size, and let overflow hide */
            border-bottom: 50px solid transparent;
            border-left: 30px solid hsla(34,85%,35%,1);
            position: absolute;
            top: 50%;
            margin-top: -50px;
            left: 100%;
            z-index: 2;
        }

        .LocBreadCrumb a:before {
            content: " ";
            display: block;
            width: 0;
            height: 0;
            border-top: 50px solid transparent; /* Go big on the size, and let overflow hide */
            border-bottom: 50px solid transparent;
            border-left: 30px solid white;
            position: absolute;
            top: 50%;
            margin-top: -50px;
            margin-left: 1px;
            left: 100%;
            z-index: 1;
        }

        .LocBreadCrumb a:first-child {
            padding-left: 10px;
        }

        .LocBreadCrumb a:nth-child(2) {
            background: hsla(34,85%,45%,1);
        }

        .LocBreadCrumb a:nth-child(2):after {
            border-left-color: hsla(34,85%,45%,1);
        }

        .LocBreadCrumb a:nth-child(3) {
            background: hsla(34,85%,55%,1);
        }

        .LocBreadCrumb a:nth-child(3):after {
            border-left-color: hsla(34,85%,55%,1);
        }

        .LocBreadCrumb a:nth-child(4) {
            background: hsla(34,85%,65%,1);
        }

        .LocBreadCrumb a:nth-child(4):after {
            border-left-color: hsla(34,85%,65%,1);
        }

        .LocBreadCrumb a:nth-child(5) {
            background: hsla(34,85%,75%,1);
        }

        .LocBreadCrumb a:nth-child(5):after {
            border-left-color: hsla(34,85%,75%,1);
        }

        .LocBreadCrumb a:last-child {
            background: #F6F6F6 !important;
            color: black;
            pointer-events: none;
            cursor: default;
            padding-right: 10px;
            /*border:none;*/
        }

        .LocBreadCrumb a:last-child:after {
            border: 0px;
        }

        .LocBreadCrumb a:hover {
            background: hsla(34,85%,25%,1);
        }

        .LocBreadCrumb a:hover:after {
            border-left-color: hsla(34,85%,25%,1) !important;
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div>
        <div id="MrnDiv">
            <table style="margin: 0px 0px auto">
                <tr>
                    <td>Enter MRN/Moving DC No :
                    </td>
                    <td>
                        <asp:TextBox ID="txtMRN" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button Text="Go" ID="btnMRNGo" OnClick="btnMRNGo_Click" runat="server" /></td>
                </tr>
            </table>
        </div>
        <div id="Search">
            <table align="left">
                <tr>
                    <td></td>
                </tr>
            </table>
            <table style="margin: 0px 0px auto;" align="right">
                <tr style="border: 1px solid #818181">
                    <td style="background-color: #808080; color: #ffffff">Search By :
                    </td>

                    <td>Product Code :
                    </td>
                    <td>
                        <asp:TextBox ID="txtProductCode" runat="server"></asp:TextBox>
                    </td>
                    <td>Item Name :
                    </td>
                    <td>
                        <asp:TextBox ID="txtItemName" runat="server"></asp:TextBox>

                    </td>
                    <%--<td>Category :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCategory" runat="server"></asp:DropDownList>
                    </td>--%>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <br /><br />
        <div id="UserMsg">
            <p style="margin: 1px 0px 10px;">
                Please Select Items in Grid and Proper Location To Save in Warehouse 
            </p>
        </div>
        <div id="Location">
            <table>
                <tr>
                    <td>
                        <asp:TreeView ID="tvLocations" runat="server" onclcik="return false;" Visible="false">
                        </asp:TreeView>
                        
                        <asp:Panel CssClass="LocBreadCrumb" runat="server" ID="pnlBreadCrumb" EnableViewState="false"></asp:Panel>
                        <asp:DropDownList runat="server" ID="ddlLocations" AutoPostBack="true" OnSelectedIndexChanged="ddlLocations_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Button Text="Save in Warehouse" ID="btnSaveWH" OnClick="btnSaveWH_Click" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="GridView">
            <asp:GridView runat="server" ID="gvMrnItems" AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField ControlStyle-Width="">
                        <ItemTemplate>
                            <asp:CheckBox Text="" runat="server" ID="chk" />
                        </ItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox Text="All" runat="server" ID="chkAll" Width="25px" onchange="javscript:SellectAll(this)"/>
                        </HeaderTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ItemId" HeaderText="Item ID" />
                    <asp:BoundField DataField="ProductCode" HeaderText="Product Code" />
                    <asp:BoundField DataField="ItemName" HeaderText="ItemName" />
                    <asp:BoundField DataField="Brand" HeaderText="Brand" />
                    <asp:BoundField DataField="Color" HeaderText="Color" />
                    <asp:BoundField DataField="AddedDate" HeaderText="Added Date" />
                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                    
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>


 
