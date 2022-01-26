<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageMenuLinks.aspx.cs" Inherits="dev_pages_ManageMenuLinks" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        input[type="text"],.table_border tr td input[type="text"] {
            width:150px !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
        <div>
        
        <asp:Label ID="lblFileExist" ForeColor="Red" runat="server" ></asp:Label><br />
             Ip Address :     <asp:Label ID="lblIP" runat="server" ></asp:Label><br />
    MAC : <asp:Label ID="lblMAC" runat="server" ></asp:Label><br />
        </div>
    <div>
    
        <h1>Manage Pages</h1>
        <br />
        <asp:HyperLink ID="hprAlertMsg" Target="_blank" runat="server" NavigateUrl="~/dev_pages/DBoardAlertMessage.aspx">Alert Message</asp:HyperLink>&nbsp;
        <asp:HyperLink ID="hprMenuOrder" Target="_blank" runat="server" NavigateUrl="~/dev_pages/Manage_UserLinks_Sequence.aspx">Menu Order</asp:HyperLink>&nbsp;
                <asp:HyperLink ID="HyperLink2" Target="_blank" runat="server" NavigateUrl="~/dev_pages/ViewTicketsRaised.aspx">View Service Requests</asp:HyperLink>&nbsp;
                <asp:HyperLink ID="HyperLink1" Target="_blank" runat="server" NavigateUrl="~/dev_pages/ServiceRequest.aspx">Raise Service Requests</asp:HyperLink>&nbsp;

        <br />
        <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="PRIVILEGE_ID" DataSourceID="menulinkssds1" DefaultMode="Insert" Height="50px" OnItemInserted="DetailsView1_ItemInserted" Width="125px" CellPadding="5">
            <Fields>
                <asp:BoundField DataField="PRIVILEGE_ID" HeaderText="PRIVILEGE_ID" ReadOnly="True" SortExpression="PRIVILEGE_ID" />
                <asp:BoundField DataField="PRIVILEGE_NAME" HeaderText="PRIVILEGE_NAME" SortExpression="PRIVILEGE_NAME" />
                <asp:BoundField DataField="PRIVILEGE_DESC" HeaderText="PRIVILEGE_DESC" SortExpression="PRIVILEGE_DESC" />
                <asp:BoundField DataField="pageurl" HeaderText="pageurl" SortExpression="pageurl" />
                <asp:BoundField DataField="catename" HeaderText="catename" SortExpression="catename" />
                <asp:BoundField DataField="pagename" HeaderText="pagename" SortExpression="pagename" />
                <asp:BoundField DataField="seqno" HeaderText="seqno" ReadOnly="True" SortExpression="seqno" />

                <asp:CommandField ShowInsertButton="True" />
            </Fields>
        </asp:DetailsView>
        <br />
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="PRIVILEGE_ID" DataSourceID="menulinkssds1" OnRowDeleting="GridView1_RowDeleting" Width="100%" AllowSorting="True">
            <Columns>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete"></asp:LinkButton>
                        <cc1:ConfirmButtonExtender ID="LinkButton2_ConfirmButtonExtender" runat="server" ConfirmText="Are you Sure ?" Enabled="True" TargetControlID="LinkButton2">
                        </cc1:ConfirmButtonExtender>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PRIVILEGE_ID" HeaderText="PRIVILEGE_ID" ReadOnly="True" SortExpression="PRIVILEGE_ID" />
                <asp:BoundField DataField="PRIVILEGE_NAME" HeaderText="PRIVILEGE_NAME" SortExpression="PRIVILEGE_NAME" />
                <asp:BoundField DataField="PRIVILEGE_DESC" Visible="false" HeaderText="PRIVILEGE_DESC" SortExpression="PRIVILEGE_DESC" />
                <asp:BoundField DataField="pageurl" HeaderText="pageurl" SortExpression="pageurl" />
                <asp:BoundField DataField="catename" HeaderText="catename" SortExpression="catename" />
                <asp:BoundField DataField="pagename" HeaderText="pagename" SortExpression="pagename" />
                <asp:BoundField DataField="seqno" HeaderText="seqno" SortExpression="seqno" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="menulinkssds1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:DBCon %>" DeleteCommand="DELETE FROM [YANTRA_LKUP_PRIVILEGES] WHERE [PRIVILEGE_ID] = @original_PRIVILEGE_ID" InsertCommand="INSERT INTO [YANTRA_LKUP_PRIVILEGES] ([PRIVILEGE_ID], [PRIVILEGE_NAME], [PRIVILEGE_DESC], [pageurl], [catename], [pagename],seqno) VALUES (@PRIVILEGE_ID, @PRIVILEGE_NAME, @PRIVILEGE_DESC, @pageurl, @catename, @pagename,@seqno)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [YANTRA_LKUP_PRIVILEGES] ORDER BY [PRIVILEGE_ID] desc" UpdateCommand="UPDATE [YANTRA_LKUP_PRIVILEGES] SET [PRIVILEGE_NAME] = @PRIVILEGE_NAME, [PRIVILEGE_DESC] = @PRIVILEGE_DESC, [pageurl] = @pageurl, [catename] = @catename, [pagename] = @pagename,seqno=@seqno WHERE [PRIVILEGE_ID] = @original_PRIVILEGE_ID">
            <DeleteParameters>
                <asp:Parameter Name="original_PRIVILEGE_ID" Type="Int64" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="PRIVILEGE_ID" Type="Int64" />
                <asp:Parameter Name="PRIVILEGE_NAME" Type="String" />
                <asp:Parameter Name="PRIVILEGE_DESC" Type="String" />
                <asp:Parameter Name="pageurl" Type="String" />
                <asp:Parameter Name="catename" Type="String" />
                <asp:Parameter Name="pagename" Type="String" />
                <asp:Parameter Name="seqno" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="PRIVILEGE_NAME" Type="String" />
                <asp:Parameter Name="PRIVILEGE_DESC" Type="String" />
                <asp:Parameter Name="pageurl" Type="String" />
                <asp:Parameter Name="catename" Type="String" />
                <asp:Parameter Name="pagename" Type="String" />
                <asp:Parameter Name="seqno" />
                <asp:Parameter Name="original_PRIVILEGE_ID" Type="Int64" />
            </UpdateParameters>
        </asp:SqlDataSource>
        </ContentTemplate>
        </asp:UpdatePanel>
                <br />
    
    </div>
    </form>
</body>
</html>
