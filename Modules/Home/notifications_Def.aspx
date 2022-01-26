<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/AdminMP1.master" AutoEventWireup="true" CodeFile="notifications_Def.aspx.cs" Inherits="Modules_Home_notifications_Def" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
    <table style="width:100%;" cellpadding="5" cellspacing="5">
        <tr>
            <td style="width: 33%">
                <h4>Circular</h4>
            </td>
            <td style="width: 33%">
                <h4>Memo</h4>
            </td>
            <td style="width: 33%"><h4>Employee DOB</h4> <asp:Label ID="lblIsReadEmpDOB" runat ="server" Visible ="false"  ></asp:Label>
                 <asp:Label ID="lblEmpId" runat ="server" Visible ="false"  ></asp:Label>
                 <asp:Label ID="lblEmpName" runat ="server" Visible ="false"  ></asp:Label>
                <asp:Label ID="lblHRMobileNo" runat="server" Visible ="false"  ></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="Circularsds1" Width="100%" AllowPaging="True" PageSize="6" CellSpacing="-1" ShowHeader="False">
                    <Columns>
                        <asp:TemplateField HeaderText="Circulars" SortExpression="CIR_ID">
                            <ItemTemplate>
                                <div class='<%# getColor(Eval("isread").ToString()) %>'>
                                    <div class="poplink" data-cirno='<%# Eval("CIR_ID", "CIR{0}") %>' data-unqid='<%# Eval("CIR_NO") %>'>
                                        <div style="color: #9b9999">
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("CIR_NO") %>'></asp:Label>
                                            -
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("CIR_DATE", "{0:d}") %>'></asp:Label>
                                        </div>
                                        <asp:Label ID="Label4" runat="server" Text='<%# short_Description(Eval("DESCRIPTION").ToString()) %>'></asp:Label>
                                    </div>
                                </div>
                                <div id='<%# Eval("CIR_ID", "CIR{0}") %>' title="Circular Description" style="display:none;">
                                    <p>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("DESCRIPTION") %>'></asp:Label>
                                    </p>
                                    <p>
                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("CIR_DATE") %>'></asp:Label>
                                    </p>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="Circularsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT YANTRA_HR_CIRCULAR.CIR_ID, YANTRA_HR_CIRCULAR.CIR_NO, YANTRA_HR_CIRCULAR.CIR_DATE, YANTRA_HR_CIRCULAR.DESCRIPTION, ReadRecords_tbl.isread FROM YANTRA_HR_CIRCULAR LEFT OUTER JOIN ReadRecords_tbl ON YANTRA_HR_CIRCULAR.CIR_NO = ReadRecords_tbl.unqid WHERE YANTRA_HR_CIRCULAR.Company_ID not in (55,56,57) and (YANTRA_HR_CIRCULAR.EMP_ID = (SELECT Emp_id FROM YANTRA_USER_DETAILS WHERE (USER_ID = @User_Id)))">
                    <SelectParameters>
                        <asp:SessionParameter Name="User_Id" SessionField="vl_userid" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td style="vertical-align: top">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="Memo_Id" DataSourceID="memosds1" Width="100%" AllowPaging="True" PageSize="6" ShowHeader="False">
                    <Columns>
                        <asp:TemplateField HeaderText="Memo" SortExpression="Memo_Id">
                            <ItemTemplate>
                                <div class="poplink" data-cirno='<%# Eval("Memo_Id", "MEMO{0}") %>' data-unqid='<%# Eval("Memo_No") %>'>
                                    <div style="color: #9b9999">
                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("Memo_No") %>'></asp:Label> - <asp:Label ID="Label3" runat="server" Text='<%# Eval("Memo_Date", "{0:d}") %>'></asp:Label>
                                    </div>
                                    <asp:Label ID="Label9" runat="server" Text='<%# short_Description(Eval("Reason").ToString()) %>'></asp:Label>
                                </div>
                                <div id='<%# Eval("Memo_Id", "MEMO{0}") %>' title="Memo Description" style="display:none;">
                                    <p>
                                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
                                    </p>
                                    <p>
                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("Memo_Date") %>'></asp:Label>
                                    </p>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="memosds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT Yantra_EMP_MEMO.Memo_Id, Yantra_EMP_MEMO.Memo_No, Yantra_EMP_MEMO.Emp_Id, Yantra_EMP_MEMO.Memo_Date, Yantra_EMP_MEMO.Reason, ReadRecords_tbl.isread FROM Yantra_EMP_MEMO LEFT OUTER JOIN ReadRecords_tbl ON Yantra_EMP_MEMO.Memo_No = ReadRecords_tbl.unqid WHERE (Yantra_EMP_MEMO.Emp_Id = (SELECT Emp_id FROM YANTRA_USER_DETAILS WHERE (USER_ID = @USER_ID)))">
                    <SelectParameters>
                        <asp:SessionParameter Name="USER_ID" SessionField="vl_userid" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
           <td style="vertical-align: top">
                <asp:GridView ID="gvEmpBdyReminder" runat="server" AutoGenerateColumns ="false" Width="100%" EmptyDataRowStyle-HorizontalAlign ="Center"  AllowPaging="True" PageSize="6" CellSpacing="-1" ShowHeader="False" >
                    <Columns>
                        <asp:TemplateField HeaderText="Employee DOB Reminder" SortExpression="EMP_ID">
                             <ItemTemplate>
                                 <div class='<%# getColor(Eval("isread").ToString()) %>'>
                                    <div class="poplink" data-cirno='<%# Eval("Emp_Id", "EMP{0}") %>' data-unqid='<%# Eval("Emp_Name") %>'>
                                        <div style="color: #9b9999">
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Emp_Name") %>'></asp:Label>
                                            -
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("EMP_DOB", "{0:d}") %>'></asp:Label>
                                        </div>
                                        <asp:Label ID="Label4" runat="server" Text='<%# short_Description(Eval("DESG_NAME").ToString()) %>'></asp:Label>
                                    </div>
                                </div>
                                <div id='<%# Eval("EMP_ID", "EMP{0}") %>' title="Employee DOB Reminder" style="display:none;">
                                    <p>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("EMP_Name") %>'></asp:Label>
                                    </p>
                                    <p>
                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("EMP_DOB") %>'></asp:Label>
                                    </p>
                                </div>
                             </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField ="EMP_ID" HeaderText ="Emp Id" SortExpression ="EMP_ID"></asp:BoundField>
                        <asp:BoundField DataField ="Emp_Name" HeaderText="Name" SortExpression ="Emp_Name" />
                        <asp:BoundField DataField="EMP_GENDER" HeaderText ="Gender" SortExpression ="EMP_GENDER"/>
                        <asp:BoundField DataField ="EMP_DOB" HeaderText="DOB" SortExpression ="EMP_DOB" />--%>
                    </Columns>

                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="Select YANTRA_EMPLOYEE_MAST.EMP_ID ,EMP_FIRST_NAME +''+ EMP_LAST_NAME as Emp_Name, EMP_GENDER,EMP_DOB,DESG_NAME,ReadRecords_tbl.isread from YANTRA_EMPLOYEE_MAST inner join YANTRA_EMPLOYEE_DET on YANTRA_EMPLOYEE_MAST .EMP_ID =YANTRA_EMPLOYEE_DET .EMP_ID inner join YANTRA_DESG_MAST on YANTRA_EMPLOYEE_DET .DESG_ID =YANTRA_DESG_MAST .DESG_ID left outer join ReadRecords_tbl on YANTRA_EMPLOYEE_MAST.EMP_FIRST_NAME  =ReadRecords_tbl.unqid  where DATEPART(m, EMP_DOB)=DATEPART(m,GETDATE()) AND DATEPART(d, EMP_DOB) = DATEPART(d, GETDATE()) ">
                <SelectParameters>
                        <asp:SessionParameter Name="USER_ID" SessionField="vl_userid" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>


            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>


 
