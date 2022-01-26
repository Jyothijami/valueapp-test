<%@ Page Title="" Language="C#" MasterPageFile="~/dev_pages/MPage1.master" AutoEventWireup="true" CodeFile="DBoardAlertMessage.aspx.cs" Inherits="dev_pages_DBoardAlertMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>Subject:</td>
                        <td>
                            <asp:TextBox ID="tbxSubject1" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Message:</td>
                        <td>
                            <asp:TextBox ID="tbxMessage1" runat="server" Height="191px" TextMode="MultiLine" Width="604px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Start Time:</td>
                        <td>
                            <asp:TextBox ID="tbxStart1" CssClass="datetimetxt" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>End Time:</td>
                        <td>
                            <asp:TextBox ID="tbxEnd1" CssClass="datetimetxt" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:CheckBox ID="cbxIsEnabled1" runat="server" Text="Is Enabled" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnSubmit1" runat="server" OnClick="btnSubmit1_Click" Text="Submit" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Label ID="lblerr1" runat="server"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Manage Previous Alerts:</td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="alertssds1">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                        <asp:BoundField DataField="messagetitle" HeaderText="messagetitle" SortExpression="messagetitle" />
                        <asp:BoundField DataField="messagedesc" HeaderText="messagedesc" SortExpression="messagedesc" />
                        <asp:BoundField DataField="starttime" HeaderText="starttime" SortExpression="starttime" />
                        <asp:BoundField DataField="endtime" HeaderText="endtime" SortExpression="endtime" />
                        <asp:BoundField DataField="dt_modified" HeaderText="dt_modified" SortExpression="dt_modified" />
                        <asp:CheckBoxField DataField="isEnabled" HeaderText="isEnabled" SortExpression="isEnabled" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="alertssds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT * FROM [dboardAlert_tbl] ORDER BY [dt_modified] DESC"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
        <script src="../js/jquery.min.js"></script>
    <script src="../js/jquery.datetimepicker.js"></script>
    <link href="../js/jquery.datetimepicker.css" rel="stylesheet" />
    <script>
        $('.datetimetxt').datetimepicker({
            dayOfWeekStart: 1,
            lang: 'en'
        });
        $('#datetimepicker').datetimepicker({ value: '2015/04/15 05:03', step: 10 });

    </script>
</asp:Content>


 
