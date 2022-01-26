<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/AdminMP1.master" AutoEventWireup="true" CodeFile="Reward_Def.aspx.cs" Inherits="Modules_Home_Reward_Def" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
   

    <style>
        .ui-icon {
            margin-left: -8px !important;
            margin-top : -8px !important;
        }
        .readcls{
            background-color:#edebeb;
            margin: -5px;
            padding: 5px;
        }
        .unreadcls{
            background-color:#ffffff;
            margin: -5px;
            padding: 5px;
        }
    </style>

    <script>
        $(document).ready(function () {
            $(".poplink").on("click", function () {
                $cno = $(this).data('cirno');
                $unqid = $(this).data('unqid');

                $('#' + $cno).dialog({
                    
                    minWidth: 700,
                    modal: true,
                    buttons: {
                        Ok: function () {
                            $(this).dialog("close");
                        }
                    }
                });
                markRead($unqid);
            });

            function markRead(unqid) {
                $.ajax({
                    type: "POST",
                    url: "/notif_tasks.asmx/markRead",
                    dataType: "json",
                    data: "{ unqid: '" + unqid + "' }",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        //response(data.d);
                    },
                    failure: function (data) {
                        alert("failed");
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(textStatus + XMLHttpRequest.responseText + errorThrown);
                    }
                });
            }

        });
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">

    <div class="panel panel-info">

        <div class="panel-heading">
            <div class="panel-title">
                <span class="h4">Review</span>
            </div>
        </div>
        <div class="panel-body">

            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="Reward_Id" DataSourceID="Rewardsds1" Width="100%" AllowPaging="True" PageSize="6" ShowHeader="False">
                    <Columns>
                        <asp:TemplateField HeaderText="Reward" SortExpression="Reward_Id">
                            <ItemTemplate>
                               
                                

                                 
                                <div class="poplink" data-cirno='<%# Eval("Reward_Id", "Reward{0}") %>' data-unqid='<%# Eval("Reward_No") %>' style="cursor:pointer">
                                    <div style="color: #9b9999">
                                       
                                        <asp:Label  ID="Label8" runat="server" Text='<%# Bind("Reward_No") %>'></asp:Label> - <asp:Label ID="Label3" runat="server" Text='<%# Eval("Reward_Date", "{0:d}") %>'></asp:Label>
                                        
                                        
                                         </div>
                                    <asp:Label ID="Label9" runat="server" Text='<%# short_Description(Eval("Reason").ToString()) %>'></asp:Label>
                                </div>



                                <div id='<%# Eval("Reward_Id", "Reward{0}") %>' title="" style="display:none;">

                                    <div class="panel panel-success">
                                        <div class="panel-heading">
                                            <div class="panel-title"><h4>Reward Description</h4></div>
                                        </div>

                                        <div class="panel-body">

                                            <div class="col-md-12">
                                                 <%--<asp:Image ID="Image" runat="server" CssClass="img-responsive" EnableTheming="False"  ImageUrl='<%# Eval("Document_submitted","~/Content/EmployeeDocuments/{0}") %>' />--%>
                                                <iframe runat="server" src='<%# Eval("Document_submitted","~/Content/EmployeeDocuments/{0}") %>' style="width:600px; height:500px;" ></iframe>

                                            </div>
                                            <div class="col-md-12 text-center">
                                                <asp:Label ID="Label7" CssClass="text-info text-center" runat="server" Text='<%# Bind("Reason") %>'></asp:Label>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                    
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>


        </div>



    </div>

      <asp:SqlDataSource ID="Rewardsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT distinct Yantra_EMP_Reward.Reward_Id, Yantra_EMP_Reward.Reward_No, Yantra_EMP_Reward.Emp_Id, Yantra_EMP_Reward.Reward_Date,Document_Submitted, Yantra_EMP_Reward.Reason, ReadRecords_tbl.isread FROM Yantra_EMP_Reward LEFT OUTER JOIN ReadRecords_tbl ON Yantra_EMP_Reward.Reward_No = ReadRecords_tbl.unqid left outer join Emp_Documents_Submitted on yantra_emp_reward.Reward_Id  =Emp_Documents_Submitted.reward_id  WHERE (Yantra_EMP_Reward.Emp_Id = (SELECT Emp_id FROM YANTRA_USER_DETAILS WHERE (USER_ID = @USER_ID)))">
                    <SelectParameters>
                        <asp:SessionParameter Name="USER_ID" SessionField="vl_userid" />
                    </SelectParameters>
                </asp:SqlDataSource>





   
</asp:Content>

