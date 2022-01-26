<%@ Page Title="|| Value App : Warehouse : Manage Locations ||" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="Manage_Locations.aspx.cs" Inherits="Modules_Warehouse_Locations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/jquery-1.9.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            
            if ($('#<%=pnlBreadCrumb.ClientID%> a').length < 2) {
                $('.LocImg').css('visibility', 'hidden');
            }
            else {
                $('.LocImg').css('visibility', 'visible');
            }


            //alert($('.tvLocations_0').closest('td').children('a').html());

            $('td a.<%=tvLocations.ClientID%>_0').closest('td').hover(
            function () { $(this).append('<div style="float:right;"><div title="Add" onclick="Add(this)" id="divAdd"></div><br/><div title="Remove" onclick="Remove(this)" id="divRemove"></div></div>'); },
            function () {
                var ancTD = $(this);
                $(this).find('div').remove();
            });
        });
        function GetSelectedNode() {
            var treeViewData = window["<%=tvLocations.ClientID%>" + "_Data"];
            if (treeViewData.selectedNodeID.value != "") {
                var selectedNode = document.getElementById(treeViewData.selectedNodeID.value);
                var value = selectedNode.href.substring(selectedNode.href.indexOf(",") + 3, selectedNode.href.length - 2);
                //var value = selectedNode.href.substring(selectedNode.href.indexOf(",") + 3, selectedNode.href.length - 2);
                var text = selectedNode.innerHTML;
                //alert("Text: " + text + "\r\n" + "Value: " + value.substring(value.lastIndexOf("\\")+1));
            }
            else {
                alert("No node selected.")
            }
            return text + ',' + value.substring(value.lastIndexOf("\\") + 1);
        }

        $('#<%=pnlBreadCrumb.ClientID%>').last('a').dblclick(function () { alert("clicked"); });

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
        function Dialog(e, id) {
            //=================================================================
            var btnText = new Array("Company", "Branch", "Warehouse", "Block", "Section", "Portion");

            var aCount = $('#<%=pnlBreadCrumb.ClientID%> a').length;

            if (e.alt == 'Edit') {
                $('#lblDialogTitle').text(e.alt + " " + btnText[aCount - 1]);

                $('#<%=txtName.ClientID%>').val($('#<%=hdnLocText.ClientID%>').val());
                $('#<%=hdnTitle.ClientID%>').val(e.alt + " " + btnText[aCount - 1]);
            }
            else if (e.alt == 'Add') {
                $('#lblDialogTitle').text(e.alt + " " + btnText[aCount]);

                $('#<%=txtName.ClientID%>').val('');
                $('#<%=hdnTitle.ClientID%>').val(e.alt + " " + btnText[aCount]);
            }
            else if (e.alt == 'Delete') {
                $('#lblDeleteTitle').text(" "+btnText[aCount - 1]);
                $('#<%=lblLoc.ClientID%>').text(" "+btnText[aCount - 1]+" : "+$('#<%=hdnLocText.ClientID%>').val()+" ");
            }
            ShowDialog(false, id);
        }
        function CheckEmpty() {
            if ($('#<%=txtName.ClientID%>').val() == "") {
                alert("Name field is mandatory"); $('#<%=txtName.ClientID%>').focus(); return false;
            }
        }

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
        //var el = null;
        //function EditLoc(e) {
        //    //alert(e.id);
        //     el = e;
        //    var nodeText = $('#'+e.id).text();
        //    //alert( $(e.id).text() + "--" + $(e.id).val() + "--" + $(e.id).html());
        //    $('#' + e.id).css("display", "none");

        //    //alert(nodeText);
        //    $("#editDiv").css("display", "block");
        //    $("#editText").val(nodeText).focus();
        //    return false;
        //}
        //function Rename(e)
        //{
        //    debugger;
        //    $('#' + el.id).css("display", "block")
        //    //el.style.display = "inline";
        //    el.innerHTML = $("#editText").val();
        //    $("#editDiv").css("display", "none");
        //    $('#btnEditLocation').click();
        //    return false;
        //}
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

        .LocImg {
            width: 20px !important;
            margin: 4px 2px !important;
            cursor:pointer;
            
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
            /*content:attr(1)*/
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
        }*/

        .TreeNode a {
            display: inline-block !important;
            color: #000000;
            font-weight: normal;
            padding: 4px;
            margin-top: 2px;
        }

        .TreeNodeSelected a {
            display: inline-block !important;
            background-color: #333;
            border: 1px solid #181818 !important;
            color: #fff;
            text-decoration: none;
        }
        /*a.TreeNodeSelected{
            display: inline-block !important;
            background-color: #ddd !important;
            border: 1px solid #181818 !important;
            color: #000;
            text-decoration: none;
        }*/
        .TreeNodeHover a {
            display: inline-block !important;
            background-color: #333;
            border: 1px solid #181818 !important;
            color: #fff;
            text-decoration: none;
        }
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
padding-right: 80px;
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
        .CategoryCss ul {
            list-style-type: none !important;
            margin: 0px !important;
            border: 0px !important;
        }

        .CategoryCss li {
            display: block !important;
            background-color: #333 !important;
            border-top: 1px outset #808080 !important;
            color: #AB0 !important;
            border-bottom: 1px inset #181818 !important;
            padding: 5px;
            text-align:left !important;
        }

        .CategoryCss label {
            display: inline !important;
            padding-left: 4px;
        }

        .CategoryCss input[type=checkbox] {
            margin: 0px !important;
        }

        .CategoryCss ul li:hover {
            display: block;
            background-color: #282828 !important;
            color: white !important;
            font-weight: bold !important;
            border: none !important;
            padding: 5px !important;
            text-decoration: none !important;
        }

        .CategoryCss li.CategoryCssSelected {
            display: block;
            background-color: #282828;
            color: white;
            font-weight: bold;
            border: none;
            padding: 5px;
            text-decoration: none;
        }
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
                <td>
                    Warehouse
                </td>
                <td>
                    <asp:DropDownList ID="ddlwhlist1" runat="server" AppendDataBoundItems="True" DataSourceID="warehousesds1" DataTextField="whname" DataValueField="wh_id">
                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="warehousesds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [wh_id], [whname] FROM [warehouse_tbl] ORDER BY [whname]"></asp:SqlDataSource>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <h5>Location Tree</h5>
                </td>
                <td>
                    <asp:Panel CssClass="LocBreadCrumb" runat="server" ID="pnlBreadCrumb" EnableViewState="false"></asp:Panel>
                    <img src="../../Images/AddNew.png" width="20" alt="Add" title="Add" onclick="Dialog(this,'Dialog');" class="LocImg" />
                    <img src="../../Images/Edit.png" width="20" alt="Edit" title="Edit" onclick="Dialog(this,'Dialog');" class="LocImg" />
                    <img src="../../Images/Delete.png" width="20" alt="Delete" title="Delete" onclick="Dialog(this,'removeDialog');" class="LocImg" />
                    <div id="editDiv" style="display: block;">
                        <input type="text" id="editText" style="float: left; width: 80px; display: none;" onblur="Rename(this)" />
                        <%--<asp:Button Text="Edit" runat="server" ID="btnEditLocation" style="margin-right:30px;float:right;"/>--%>

                        <asp:DropDownList runat="server" Style="float: right;" ID="ddlLocations" AutoPostBack="true" OnSelectedIndexChanged="ddlLocations_SelectedIndexChanged" AppendDataBoundItems="True">
                            <asp:ListItem Value="0">-- Select --</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
                <td style="vertical-align: top;">

                    <%--<asp:TextBox runat="server" ID="txtNewLocation" /><asp:Button Text="Add" runat="server" ID="btnAddLocation" /><br />--%>
                    <%--<asp:DropDownList runat="server" ID="ddlLocations" AutoPostBack="true" OnSelectedIndexChanged="ddlLocations_SelectedIndexChanged">
                    </asp:DropDownList>--%>
                </td>

            </tr>
            <tr>
                <td></td>
                <td></td>
                <td style="vertical-align: middle;">
                    <%--<asp:DropDownList runat="server" ID="ddlLocations" AutoPostBack="true" OnSelectedIndexChanged="ddlLocations_SelectedIndexChanged">
                    </asp:DropDownList>--%>
                    <%--<asp:Button Text="Add" OnClick="btnSave_Click" ID="btnAdd" runat="server" />--%>

                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <!--onclick="javascript:return NodeClicked(event,this); "-->
                    <!---->
                    <asp:TreeView ID="tvLocations" runat="server" onclcik="return false;" ExpandDepth="0">
                        <NodeStyle CssClass="TreeNode" />
                        <HoverNodeStyle CssClass="TreeNodeHover" />
                        <SelectedNodeStyle CssClass="=TreeNodeSelected" />
                    </asp:TreeView>
                </td>
                <td style="vertical-align: top; padding: 40px 2px 2px 40px;">
                    <%--<asp:GridView runat="server" ID="gvWarehouse"></asp:GridView>--%>

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
                    <h3>Delete <span id="lblDeleteTitle"></span></h3>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: center">Are you sure want to delete <br /> "<b>
                    <asp:Label Text="" runat="server" ID="lblLoc" ForeColor="Blue" /></b>"<br /> and its child Locations</td>
            </tr>

            <tr>
                <td style="text-align: center">
                    <asp:Button Text="Delete" runat="server" ID="btnDelete" OnClick="btnDelete_Click" />
                    <asp:Button Text="Cancel" runat="server" ID="btnCancel" OnClientClick="return HideDialog('removeDialog');" />
                </td>
            </tr>
        </table>
    </div>
    <div id="Dialog" class="dialog">
        <table style="margin: 50px auto;">
            <tr>
                <td colspan="3" style="text-align: center">
                    <b><span id="lblDialogTitle">Title</span></b>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:HiddenField ID="hdnLocText" runat="server" />
                    <asp:HiddenField ID="hdnTitle" runat="server" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td>Name</td>
                <td>:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtName" /></td>
            </tr>

            <tr>
                <td></td>
                <td></td>
                <td style="padding-left: 20px;">
                    <asp:Button Text="Save" runat="server" ID="Button1" OnClientClick="return CheckEmpty();" OnClick="btnAddLocation_Click" />
                    <asp:Button Text="Close" runat="server" ID="Button2" OnClientClick="return HideDialog('Dialog');" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>


 
