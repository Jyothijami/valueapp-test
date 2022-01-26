<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/AdminMP1.master" AutoEventWireup="true" CodeFile="Manage_Locations3.aspx.cs" Inherits="Modules_Warehouse_Manage_Locations3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
    .black_overlay{
        display: none;
        position: absolute;
        top: 0%;
        left: 0%;
        width: 100%;
        height: 100%;
        background-color: black;
        z-index:1001;
        -moz-opacity: 0.8;
        opacity:.80;
        filter: alpha(opacity=80);
    }
    .white_content {
        display: none;
        position: absolute;
        top: 25%;
        left: 25%;
        width: 50%;
        height: 50%;
        padding: 16px;
        border: 1px solid orange;
        background-color: white;
        z-index:1002;
        overflow: auto;
    }
    #contextmenu1 {
        display:none;
    }
    .contextMenu {
        position:absolute;
        background-color:white;
        border:1px solid pink;
        padding:5px;
    }
        .contextMenu li{
            list-style:none;
            text-align:left;
        }
    #MyTreeDiv {
        position:relative;
    }
    #addSubSectiondv {
        display:none;
    }
    #hyplkClose1 {
        padding: 15px;
        position: absolute;
        right: 0;
        top: 0;
    }
</style>
<script type="text/javascript">
    $(document).ready(function () {


        $("#MyTreeDiv td a").parent().hover(
            function () {
                $("<ul class='contextMenu'><li class='copy'><a id='addSubSectionhyplk' href='#'>Add SubSection</a></li><li class='edit'><a id='editSubSectionhyplk' href='#'>Edit Section</a></li> <li class='delete'><a href='#delete'>Delete Section</a></li></ul>").appendTo($(this));
            },
            function () {
                $(this).children(".contextMenu").remove();
            }
        );

        $(document).on("click", "#addSubSectionhyplk", function () {
            $("#addSubSectiondv").css('display', 'block');
            $("#editSubSectiondv").css('display', 'none');

            //alert($(this).parent().parent().parent().children("[id*='TreeView1']").attr("href"));

            var atag = $(this).parent().parent().parent().children("[id*='TreeView1']");

            $("[name$='hfsectionid1']").val($(atag).attr("href"));
            $("[name$='tbxAddSubSection1']").val($(atag).text());

            $(".black_overlay").css('display', 'block');
            $(".white_content").css('display', 'block');
        });

        $(document).on("click", "#editSubSectionhyplk", function () {
            $("#addSubSectiondv").css('display', 'none');
            $("#editSubSectiondv").css('display', 'block');

            var atag = $(this).parent().parent().parent().children("[id*='TreeView1']");

            $("[name$='hfsectionid1']").val($(atag).attr("href"));
            $("[name$='tbxOldSectionName1']").val($(atag).text());

            $(".black_overlay").css('display', 'block');
            $(".white_content").css('display', 'block');
        });

        $(document).on("click", "#MyTreeDiv a", function (e) {
            e.stopPropagation();
            e.preventDefault();

            return false;
        });
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <h3>Manage Locations</h3>
       <div id="light" class="white_content">
           <asp:HiddenField ID="hfsectionid1" runat="server" />
           <div id="addSubSectiondv">
               <h2>Add Sub Section</h2>
               <table>
                   <tr>
                       <td>Section: </td>
                       <td>
                           <asp:TextBox ID="tbxAddSubSection1" runat="server" Enabled="False"></asp:TextBox>                           
                       </td>
                   </tr>
                   <tr>
                       <td>Sub Section:</td>
                       <td>
                           <asp:TextBox ID="tbxSubSection1" runat="server"></asp:TextBox></td>
                   </tr>
                   <tr>
                       <td></td>
                       <td>
                           <asp:Button ID="btnAddSubSection1" runat="server" Text="Add Sub Section" OnClick="btnAddSubSection1_Click" />
                       </td>
                   </tr>
               </table>
           </div>
           <div id="editSubSectiondv">
               <h2>Edit Sub Section</h2>
               <table>
                   <tr>
                       <td>Old Section Name: </td>
                       <td>
                           <asp:TextBox ID="tbxOldSectionName1" runat="server" Enabled="False"></asp:TextBox>                           
                       </td>
                   </tr>
                   <tr>
                       <td>New Section Name:</td>
                       <td>
                           <asp:TextBox ID="tbxNewSectionName1" runat="server"></asp:TextBox></td>
                   </tr>
                   <tr>
                       <td></td>
                       <td>
                           <asp:Button ID="btnEditSection1" runat="server" Text="Edit Section" OnClick="btnEditSection1_Click" />
                       </td>
                   </tr>
               </table>
           </div>

           <a id="hyplkClose1" href = "javascript:void(0)" onclick = "document.getElementById('light').style.display='none';document.getElementById('fade').style.display='none'">Close</a></div>
    <div id="fade" class="black_overlay"></div>
 
    <table style="width:100%;">
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td style="width: 300px">
                            <div id="MyTreeDiv">
                            <asp:TreeView ID="TreeView1" runat="server" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                            </asp:TreeView>
                                </div>
                        </td>
                        <td>
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>


 
