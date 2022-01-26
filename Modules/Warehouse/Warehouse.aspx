<%@ Page Title="|| Value App : Warehouse ||" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="Warehouse.aspx.cs" Inherits="Modules_Warehouse_Warehouse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/jquery-1.9.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {


            $('#<%=chkCategoryList.ClientID%> input').click(function () {

                // use this.checked or $(this).is(':checked') to check the element is checked or not
                var isChecked = this.checked;
                //clear all checked items
                $('#<%=chkCategoryList.ClientID%> input:checked').each(function () {
                    $(this).attr('checked', false);
                });

                //changes current item state (checked or unchecked) 
                this.checked = isChecked;
                return true;
            });
            
            //alert($('.tvLocations_0').closest('td').children('a').html());

            $('td a.<%=tvLocations.ClientID%>_0').closest('td').hover(
            function () { $(this).append('<div style="float:right;"><div onclick="Add(this)" id="divAdd"></div><div onclick="Remove(this)" id="divRemove"></div></div>'); },
            function () {
                var ancTD = $(this);
                //anc.siblings('div').hover(
                //    function () {},
                //    function () { anc.siblings('div').remove(); }
                //    );

                //anc.siblings('div').mouseenter(function () { });
                //anc.siblings('div').mouseleave(function () { anc.siblings('div').remove(); });
                $(this).find('div').remove();
            }
            );
        });
        function GetSelectedNode() {
            var treeViewData = window["<%=tvLocations.ClientID%>" + "_Data"];
            if (treeViewData.selectedNodeID.value != "") {
                var selectedNode = document.getElementById(treeViewData.selectedNodeID.value);
                var value = selectedNode.href.substring(selectedNode.href.indexOf(",") + 3, selectedNode.href.length - 2);
                //var value = selectedNode.href.substring(selectedNode.href.indexOf(",") + 3, selectedNode.href.length - 2);
                var text = selectedNode.innerHTML;
                //alert("Text: " + text + "\r\n" + "Value: " + value.substring(value.lastIndexOf("\\")+1));
            } else {
                alert("No node selected.")
            }
            return text + ',' + value.substring(value.lastIndexOf("\\") + 1);
        }

        function TreeOver() {
            alert("Hover");
            $('#Content').fadeIn('2000');
        }
        function TreeOut() {
            alert("Out");
            $('#Content').fadeOut('2000');
        }
        function Remove(e) {
            $('#' + e.id).parent('div').siblings('a').click();
            var node = GetSelectedNode();
            var arrNode = node.split(',');
            $('#<%=hdnParentID.ClientID%>').val(arrNode[1]);
            $('#<%=lblLoc.ClientID%>').text(arrNode[0]);
            ShowDialog(false, 'removeDialog');
        }
        function Add(e) {
            $('#' + e.id).parent('div').siblings('a').click();
            //alert($('#'+e.id).parent('div').siblings('a').attr('href'));
            var node = GetSelectedNode();
            var arrNode = node.split(',');
            $('#<%=hdnParentID.ClientID%>').val(arrNode[1]);
            //alert($('#hdnParentID').val());
            ShowDialog(false, 'addDialog');
        }
        function NodeClicked(mEvent, e) {
            var o;
            // Internet Explorer
            if (mEvent.srcElement) {
                o = mEvent.srcElement;
            }
                // Netscape and Firefox
            else if (mEvent.target) {
                o = mEvent.target;
            }
            //alert(o.innerHTML);
            return true;
        }
        //code for popup modal
        $("#<%=btnClose.ClientID%>").click(function (e) {
            HideDialog(id);
            e.preventDefault();
        });
        function ShowDialog(modal, id) {
            $("#overlay").show();
            $("#" + id).fadeIn(300);

            if (modal) {
                $("#overlay").unbind("click");
            }
            else {
                $("#overlay").click(function (e) {
                    HideDialog(id);
                });
            }
        }

        function HideDialog(id) {
            $("#overlay").hide();
            $("#" + id).fadeOut(300);
        }

        function ShowHideColumns(e) {
           
            i = e.childNodes[0].value;
            $('#<%=gvWarehouse.ClientID%> tr').find('th:eq(' + i + ')').toggle("slow");
            $('#<%=gvWarehouse.ClientID%> tr').find('td:eq(' + i + ')').toggle("slow");
            
        }
       
    </script>
    
    <style type="text/css">
        #overlay {
            display: none;
            background-color: #000000;
            opacity: 0.4;
            width: 100%;
            height: 100%;
            position: absolute;
            top: 0px;
            left: 0px;
            z-index: 2;
        }

        .dialog {
            display: none;
            position: absolute;
            /*top:50%;left:50%;*/
            top: 100px;
            left: 0px;
            bottom: 0px;
            right: 0px;
            width: 300px;
            height: 200px;
            margin: 0px auto;
            background-color: #ffffff;
            border: 2px solid #336699;
            padding: 0px;
            z-index: 3;
        }

        #divAdd {
            float: left;
            height: 13px;
            width: 13px;
            background: url('../../Images/Arithmetics.jpg') -1px -1px;
            cursor: pointer;
        }

        #divRemove {
            margin-left: 3px;
            float: left;
            height: 13px;
            width: 13px;
            background: url('../../Images/Arithmetics.jpg') -17px -16px;
            cursor: pointer;
        }

        /*.LocBreadCrumb {
            font-weight: bold;
        }

        .LocBreadCrumb a {
            display: inline-block !important;
            background-color: #333;
            border: 1px solid #181818;
            border-left: 1px outset #808080;
            border-top: 1px outset #808080;
            color: #fff;
            font-weight: normal;
            padding:5px;
        }

        .LocBreadCrumb a:hover {
            display: inline-block !important;
            background-color: #333;
            border: 1px solid #181818 !important;
            color: #fff;
            text-decoration: none;
        }

        .LocBreadCrumb span:last-child {
            padding:5px;border:1px solid #808080;
        }*/
    </style>
    <!---->
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
    <style>
        .leftMenu ul {
            list-style-type: none;
            margin: 0px !important;
            border: 0px !important;
        }

        .leftMenu li {
            display: block;
            background-color: #333;
            border-top: 1px outset #808080;
            color: #AB0;
            border-bottom: 1px inset #181818;
            padding: 5px;
            text-align:left !important;
        }

        .leftMenu ul li:hover {
            display: block;
            background-color: #282828;
            color: white;
            font-weight: bold;
            border: none;
            padding: 5px;
            text-decoration: none;
        }

        .leftMenu li.leftMenuSelected {
            display: block;
            background-color: #282828;
            color: white;
            font-weight: bold;
            border: none;
            padding: 5px;
            text-decoration: none;
        }
        .CheckList {margin-left:0px !important;text-align:left !important;}
        .CheckList label{display:inline;padding-left:4px !important;}
        .CheckList input[type=checkbox]{margin:0px !important;}
    </style>
    <!--overriding the boot strap margin-bottom style to 0 pixels-->
    <style>
        select, textarea, input[type="text"], input[type="password"], input[type="datetime"], input[type="datetime-local"], input[type="date"],
        input[type="month"], input[type="time"], input[type="week"], input[type="number"], input[type="email"], input[type="url"],
        input[type="search"], input[type="tel"], input[type="color"], .uneditable-input {
            margin-bottom: auto !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div>
        <table>
            <tr>
                <td style="width: 100px;">
                    <%--<h5>Location Tree</h5>--%>
                </td>
                <td>
                    <asp:Panel CssClass="LocBreadCrumb" runat="server" ID="pnlBreadCrumb" EnableViewState="false"></asp:Panel>
                </td>
                <td style="vertical-align: middle;">
                    <asp:DropDownList runat="server" ID="ddlLocations" AutoPostBack="true" OnSelectedIndexChanged="ddlLocations_SelectedIndexChanged">
                    </asp:DropDownList>
                    <%--<asp:LinkButton Text="Test" OnClick="BreadCrumb" ID="lbtnTest" runat="server" Visible="false" />--%>

                </td>
            </tr>
        </table>

        <table>
            <tr>
                <td style="vertical-align: top; padding-top: 20px;">
                    <!--onclick="javascript:return NodeClicked(event,this); "-->
                    <!---->
                    <asp:TreeView ID="tvLocations" runat="server" onclcik="return false;" Visible="false">
                        <SelectedNodeStyle BackColor="#c0c0c0" />
                    </asp:TreeView>
                    <!--warehouse left menu-->
                    <div style="width: 135px;" class="leftMenu CheckList" id="divLeftMenu" runat="server">
                        
                        <asp:CheckBoxList ID="chkCategoryList" runat="server" RepeatLayout="UnorderedList" AutoPostBack="true" OnSelectedIndexChanged="chkCategoryList_SelectedIndexChanged"></asp:CheckBoxList>
                    </div>
                    <div>
                        <asp:CheckBoxList runat="server" ID="chkColumnList" RepeatLayout="OrderedList" CssClass="CheckList"></asp:CheckBoxList>
                    </div>
                </td>
                <td style="vertical-align: top; padding: 20px 2px 2px 10px;">
                    <div>
                        <table style="font-weight:bold;">
                            <tr>
                                <td>Product Code</td><td>:</td><td>
                                    <asp:TextBox runat="server" ID="txtProdCode"/></td><td style="width:10px;"></td>
                                <td>Item Name</td><td>:</td><td>
                                    <asp:TextBox runat="server" ID="txtItemName"/></td><td style="width:10px;"></td>
                                <td>Brand</td><td>:</td><td>
                                    <asp:TextBox runat="server" ID="txtBrand"/></td><td style="width:10px;"></td>
                                <td><asp:Button Text="Search" ID="btnSearch" OnClick="btnSearch_Click" runat="server"/>

                                </td>
                            </tr><tr>
                                <td>Color</td><td>:</td><td>
                                    <asp:TextBox runat="server" ID="txtColor"/></td><td style="width:10px;"></td>
                                <td>From Date</td><td>:</td><td>
                                    <asp:TextBox runat="server" ID="txtFromDate"/></td><td style="width:10px;"></td>
                                <td>To Date</td><td>:</td><td>
                                    <asp:TextBox runat="server" ID="txtToDate"/></td><td style="width:10px;"></td>
                                <td><asp:Button runat="server" ID="btnReset" Text="Reset" OnClick="btnReset_Click" /></td>
                            </tr>
                        </table>
                    </div>
                    <div style="overflow-x: auto; width: 100%;">
                        <asp:GridView runat="server" ID="gvWarehouse"></asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <!-- visible when user clicks on edit button-->
    <div id="overlay" class="web_dialog_overlay"></div>

    <div id="addDialog" class="dialog">
        <table style="margin: 0px auto;">
            <tr>
                <td colspan="3" style="text-align: center">
                    <b>Add New Location</b>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:HiddenField ID="hdnParentID" runat="server" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td>Name</td>
                <td>:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtLocationName" /></td>
            </tr>

            <tr>
                <td></td>
                <td></td>
                <td>
                    <asp:Button Text="Save" runat="server" ID="btnSave" OnClick="btnSave_Click" />
                    <asp:Button Text="Close" runat="server" ID="btnClose" OnClientClick="return HideDialog('addDialog');" />
                </td>
            </tr>
        </table>
    </div>
    <div id="removeDialog" class="dialog">
        <table style="margin: 0px auto;">
            <tr>
                <td style="text-align: center">
                    <h2>Delete Location</h2>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: center">Are you sure want to delete Location " <b>
                    <asp:Label Text="" runat="server" ID="lblLoc" ForeColor="Blue" /></b>" and its child Locations</td>
            </tr>

            <tr>
                <td style="text-align: center">
                    <asp:Button Text="Delete" runat="server" ID="btnDelete" OnClick="btnDelete_Click" />
                    <asp:Button Text="Cancel" runat="server" ID="btnCancel" OnClientClick="return HideDialog('removeDialog');" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>


 
