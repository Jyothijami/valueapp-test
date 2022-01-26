<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Manage_UserLinks_Sequence.aspx.cs" Inherits="dev_pages_Manage_UserLinks_Sequence" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
    <div>
    
        <table style="width:100%;">
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <cc1:ReorderList ID="ReorderList1" runat="server" AllowReorder="True" DataSourceID="sqds1" PostBackOnReorder="False" SortOrderField="seqno">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("pagename") %>'></asp:Label>
                                    &nbsp;-
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("PRIVILEGE_NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </cc1:ReorderList>
                            <asp:SqlDataSource ID="sqds1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:DBCon %>" DeleteCommand="DELETE FROM [YANTRA_LKUP_PRIVILEGES] WHERE [PRIVILEGE_ID] = @original_PRIVILEGE_ID AND (([PRIVILEGE_NAME] = @original_PRIVILEGE_NAME) OR ([PRIVILEGE_NAME] IS NULL AND @original_PRIVILEGE_NAME IS NULL)) AND (([seqno] = @original_seqno) OR ([seqno] IS NULL AND @original_seqno IS NULL))" InsertCommand="INSERT INTO [YANTRA_LKUP_PRIVILEGES] ([PRIVILEGE_ID], [PRIVILEGE_NAME], [seqno]) VALUES (@PRIVILEGE_ID, @PRIVILEGE_NAME, @seqno)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT PRIVILEGE_ID, PRIVILEGE_NAME, seqno, pagename FROM YANTRA_LKUP_PRIVILEGES ORDER BY seqno, PRIVILEGE_NAME" UpdateCommand="UPDATE [YANTRA_LKUP_PRIVILEGES] SET [PRIVILEGE_NAME] = @PRIVILEGE_NAME, [seqno] = @seqno WHERE [PRIVILEGE_ID] = @original_PRIVILEGE_ID AND (([PRIVILEGE_NAME] = @original_PRIVILEGE_NAME) OR ([PRIVILEGE_NAME] IS NULL AND @original_PRIVILEGE_NAME IS NULL)) AND (([seqno] = @original_seqno) OR ([seqno] IS NULL AND @original_seqno IS NULL))">
                                <DeleteParameters>
                                    <asp:Parameter Name="original_PRIVILEGE_ID" Type="Int64" />
                                    <asp:Parameter Name="original_PRIVILEGE_NAME" Type="String" />
                                    <asp:Parameter Name="original_seqno" Type="Int32" />
                                </DeleteParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="PRIVILEGE_ID" Type="Int64" />
                                    <asp:Parameter Name="PRIVILEGE_NAME" Type="String" />
                                    <asp:Parameter Name="seqno" Type="Int32" />
                                </InsertParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="PRIVILEGE_NAME" Type="String" />
                                    <asp:Parameter Name="seqno" Type="Int32" />
                                    <asp:Parameter Name="original_PRIVILEGE_ID" Type="Int64" />
                                    <asp:Parameter Name="original_PRIVILEGE_NAME" Type="String" />
                                    <asp:Parameter Name="original_seqno" Type="Int32" />
                                </UpdateParameters>
                            </asp:SqlDataSource>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

 
