<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RH_Rating.aspx.cs" Inherits="RH_Rating" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
       
</head>
<body>
    <form id="form1" runat="server">

        <div>
            <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
            <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="true" />
            <div class="demo-container no-bg">
                <div class="divRatings">
                    <telerik:RadGrid ID="RadGrid1" runat="server" DataSourceID="SqlDataSource1">
                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                        <MasterTableView AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
                            <Columns>
                                <telerik:GridBoundColumn DataField="RV_Question" FilterControlAltText="Filter RV_Question column" HeaderText="RV_Question" SortExpression="RV_Question" UniqueName="RV_Question">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn>
                                    <ItemTemplate>
                                        <telerik:RadRating RenderMode="Lightweight" ID="RadRating1" runat="server"
                                            Value="3" Precision="Half" DbValue="3">
                                        </telerik:RadRating>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>

                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [RV_Question] FROM [YANTRA_LKUP_REVIEW_QUESTIONS] WHERE ([RV_CAT_ID] = @RV_CAT_ID)">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="1" Name="RV_CAT_ID" Type="Int64" />
                        </SelectParameters>
                    </asp:SqlDataSource>


                </div>
            </div>
        </div>

    </form>
</body>
</html>
