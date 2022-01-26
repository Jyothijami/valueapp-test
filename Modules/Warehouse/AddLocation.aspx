<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddLocation.aspx.cs" Inherits="Modules_Warehouse_AddLocation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../js/jquery-1.9.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            //alert($('.tvLocations_0').closest('td').children('a').html());
            
            $('td a.tvLocations_0').closest('td').hover(
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
            var treeViewData = window["tvLocations" + "_Data"];
            if (treeViewData.selectedNodeID.value != "") {
                var selectedNode = document.getElementById(treeViewData.selectedNodeID.value);
                var value = selectedNode.href.substring(selectedNode.href.indexOf(",")+3, selectedNode.href.length - 2);
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
            $('#hdnParentID').val(arrNode[1]);
            $('#lblLoc').text(arrNode[0]);
            ShowDialog(false, 'removeDialog');
        }
        function Add(e) {
            $('#' + e.id).parent('div').siblings('a').click();
            //alert($('#'+e.id).parent('div').siblings('a').attr('href'));
            var node = GetSelectedNode();
            var arrNode = node.split(',');
            $('#hdnParentID').val(arrNode[1]);
            //alert($('#hdnParentID').val());
            ShowDialog(false,'addDialog');
        }
        function NodeClicked(mEvent,e) {
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
        $("#btnClose").click(function (e) {
            HideDialog(id);
            e.preventDefault();
        });
        function ShowDialog(modal,id) {
            $("#overlay").show();
            $("#"+id).fadeIn(300);

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
            $("#"+id).fadeOut(300);
        }

    </script>
    <style type="text/css">
         #overlay{
        display:none;
        background-color:#000000;
        opacity:0.4;
        width:100%;height:100%;
        position:absolute;
        top:0px;left:0px;z-index:2;
    }
    .dialog{
        display:none;
        position:absolute;
        /*top:50%;left:50%;*/
        top:100px;
        left:0px;bottom:0px;right:0px;        
        width:300px;height:200px;
        margin:0px auto;
        background-color: #ffffff;
        border: 2px solid #336699;
        padding: 0px;
        z-index:3;
    }
        #divAdd{float:left;height:13px; width:13px; background:url('../../Images/Arithmetics.jpg') -1px -1px;cursor:pointer;}
        #divRemove{margin-left:3px;float:left;height:13px; width:13px;background:url('../../Images/Arithmetics.jpg') -17px -16px;cursor:pointer; }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td></td>
            </tr>
        </table>
        <!--onclick="javascript:return NodeClicked(event,this); "-->
            <!---->
        <asp:TreeView ID="tvLocations" runat="server" >
            <SelectedNodeStyle BackColor="#c0c0c0"/>
        </asp:TreeView>
    </div>
        <!-- visible when user clicks on edit button-->
        <div id="overlay" class="web_dialog_overlay"></div>

    <div id="addDialog" class="dialog">
        <table style="margin: 0px auto;">
                <tr>
                    <td colspan="3" style="text-align: center">
                        <h2>Add New Location</h2>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td><asp:HiddenField ID="hdnParentID" runat="server"/></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Location Name</td>
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
                    <td  style="text-align: center">
                        <h2>Delete Location</h2>
                    </td>
                </tr>
                <tr>
                    <td><asp:HiddenField ID="HiddenField1" runat="server"/></td>
                </tr>
                <tr>
                    <td style="text-align:center">
                        Are you sure want to delete Location " <b><asp:Label Text="" runat="server" ID="lblLoc" ForeColor="Blue"/></b>" and its child Locations</td>
                </tr>                
                
                <tr>
                    <td style="text-align:center">
                        <asp:Button Text="Delete" runat="server" ID="btnDelete" OnClick="btnDelete_Click" />
                        <asp:Button Text="Cancel" runat="server" ID="btnCancel" OnClientClick="return HideDialog('removeDialog');"/>
                    </td>
                </tr>
            </table>
    </div>
    </form>
</body>
</html>

 
