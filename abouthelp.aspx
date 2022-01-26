<%@ Page Language="C#" AutoEventWireup="true" CodeFile="abouthelp.aspx.cs" Inherits="abouthelp" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>

  <link rel="stylesheet" href="//code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css"/>
  <script src="//code.jquery.com/jquery-1.10.2.js"></script>
  <script src="//code.jquery.com/ui/1.11.1/jquery-ui.js"></script>
  <link rel="stylesheet" href="/resources/demos/style.css"/>
  <script>
      $(function () {
          $("#tabs").tabs();
      });
  </script>
    <style>
        body{
            font-family:Calibri;
            font-size:11px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
    <div id="tabs">
  <ul>
    <li><a href="#tabs-1">Help</a></li>
    <li><a href="#tabs-2">Page Flow</a></li>
    <li><a href="#tabs-3">Tables Sps Involved</a></li>
  </ul>
  <div id="tabs-1">
    <p>
       
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
              <asp:DataList ID="DataList1" runat="server" DataSourceID="hlpcntsds1" OnCancelCommand="DataList1_CancelCommand" OnEditCommand="DataList1_EditCommand" OnUpdateCommand="DataList1_UpdateCommand">
                  <EditItemTemplate>
                      <table style="width:100%;">
                          <tr>
                              <td>
                                  <cc2:Editor ID="helpcntEditor1" runat="server" Height="285px" Width="666px" Content='<%# Eval("helpcnt") %>' />
                              </td>
                          </tr>
                          <tr>
                              <td>
                                  <asp:Button ID="btnUpdate1" runat="server" CommandName="update" Text="Update" />
                                  &nbsp;<asp:Button ID="btnCancel1" runat="server" CommandName="cancel" Text="Cancel" />
                              </td>
                          </tr>
                          <tr>
                              <td>&nbsp;</td>
                          </tr>
                      </table>
                  </EditItemTemplate>
                  <ItemTemplate>
                      <asp:Label ID="helpcntLabel" runat="server" Text='<%# Eval("helpcnt") %>' />
                <br />
                <br />
                      <asp:LinkButton ID="lkbtEdit1" runat="server" CommandName="edit">Edit</asp:LinkButton>
<br />
                  </ItemTemplate>
              </asp:DataList>
              <asp:SqlDataSource ID="hlpcntsds1" runat="server" ConnectionString="<%$ ConnectionStrings:helpCon %>" SelectCommand="SELECT [helpcnt], [pageflow], [tbls_sps] FROM [helptbl1] WHERE ([pageurl] = @pageurl)">
                  <SelectParameters>
                      <asp:QueryStringParameter Name="pageurl" QueryStringField="purl" Type="String" />
                  </SelectParameters>
              </asp:SqlDataSource>
          </ContentTemplate>
      </asp:UpdatePanel>
      
    </p>
  </div>
  <div id="tabs-2">
    <p>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
          <ContentTemplate>
        <asp:DataList ID="DataList2" runat="server" DataSourceID="hlpcntsds1" OnEditCommand="DataList2_EditCommand" OnCancelCommand="DataList2_CancelCommand" OnUpdateCommand="DataList2_UpdateCommand">
            <EditItemTemplate>
                <table style="width:100%;">
                    <tr>
                        <td>
                            <asp:TextBox ID="tbxpageflow1" runat="server" Height="285px" Text='<%# Eval("pageflow") %>' TextMode="MultiLine" Width="646px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnUpdate2" runat="server" CommandName="update" Text="Update" />
                            &nbsp;<asp:Button ID="btnCancel2" runat="server" CommandName="cancel" Text="Cancel" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="helpcntLabel" runat="server" Text='<%# Eval("pageflow") %>' />
                <br />
                <br />
                <asp:LinkButton ID="lkbtEdit2" runat="server" CommandName="edit">Edit</asp:LinkButton>
<br />
            </ItemTemplate>
        </asp:DataList>
              </ContentTemplate>
            </asp:UpdatePanel>
    </p>
  </div>
  <div id="tabs-3">
    <p>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
          <ContentTemplate>
        <asp:DataList ID="DataList3" runat="server" DataSourceID="hlpcntsds1" OnCancelCommand="DataList3_CancelCommand" OnUpdateCommand="DataList3_UpdateCommand" OnEditCommand="DataList3_EditCommand">
            <EditItemTemplate>
                <table style="width:100%;">
                    <tr>
                        <td>
                            <asp:TextBox ID="tbxhlptblssps1" runat="server" Height="285px" Text='<%# Eval("tbls_sps") %>' TextMode="MultiLine" Width="646px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnUpdate1" runat="server" CommandName="update" Text="Update" />
                            &nbsp;<asp:Button ID="btnCancel1" runat="server" CommandName="cancel" Text="Cancel" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="helpcntLabel" runat="server" Text='<%# Eval("tbls_sps") %>' />
                <br />
                <br />
                <asp:LinkButton ID="lkbtEdit1" runat="server" CommandName="edit">Edit</asp:LinkButton>
<br />
            </ItemTemplate>
        </asp:DataList>
              </ContentTemplate>
            </asp:UpdatePanel>
    </p>
    
  </div>
</div>
    </form>
</body>
</html>

 

