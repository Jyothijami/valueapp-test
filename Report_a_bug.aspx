<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report_a_bug.aspx.cs" Inherits="Report_a_bug" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h2>Bug Report</h2>
        <table style="width:100%">
            <tr>
                <td>
                    Page Name : 
                </td>
                <td>
                    <asp:TextBox ID="txtPageName" runat="server" Width="200px"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td>
                    Title : 
                    </td>
                <td>
                    <asp:TextBox ID="txtTitle" runat="server" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top">
                    Bug
                    Description :
                </td>
                <td>
                    <asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server" Height="152px" Width="424px"></asp:TextBox>
                </td>
            </tr>
           <tr>
               <td>
                   
               </td>
               <td>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit"  OnClick="btnSubmit_Click"/>
               </td>
           </tr>
        </table>

    </div>
    </form>
</body>
</html>
