<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/SalesMP1.master" AutoEventWireup="true" CodeFile="List_eQuotations2.aspx.cs" Inherits="Modules_SM_List_eQuotations2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <div style="float:right; display:inline;">
        <asp:FileUpload ID="fupload1" runat="server" />
        <asp:Button ID="btnupload1" runat="server" Text="Upload" OnClick="btnupload1_Click" />
    </div>    <p>
        <asp:Button ID="btnGenerateNew1" runat="server" OnClick="btnGenerateNew1_Click" Text="Generate New" />
    </p>

<br />
    <asp:DataList ID="DataList1" runat="server" DataKeyField="eqhistid" DataSourceID="equotsds1">
        <ItemTemplate>
            <h3>
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# Eval("eqhistname", "~/presentations/{0}") %>' Text='<%# Eval("eqhistname") %>'></asp:HyperLink>
            </h3>
            <asp:Label ID="eqhistdescLabel" runat="server" Text='<%# Eval("eqhistdesc") %>' />
            <br />
            <asp:Label ID="eqdtaddedLabel" runat="server" Text='<%# Eval("eqdtadded", "Updated on - {0:g}") %>' />
        </ItemTemplate>
    </asp:DataList>
    <asp:SqlDataSource ID="equotsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT * FROM [eqhist_tbl] WHERE ([QUOT_ID] = @QUOT_ID) ORDER BY [eqdtadded] DESC">
        <SelectParameters>
            <asp:QueryStringParameter Name="QUOT_ID" QueryStringField="QuoId" Type="Int64" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>


 
