<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeMailFormat.aspx.cs" Inherits="Modules_HR_EmployeeMailFormat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Email Format</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>        
        <h3> Mail  from Company </h3>
             <table>
            <tr>
                <td>
                    <p>
                        Dear Candidate,
                        </p>
                    <br />
                    <p>
                            Your Interview was scheduled on the following date and Location.
                    </p>
                </td>
                </tr>
             <tr>
                 <td>
                     Interviwer Name : 
                 </td>
                 <td> <asp:PlaceHolder ID="ph1" runat="server">
                     <asp:Label ID="lblIntName" runat="server" Text="Admin"></asp:Label>
                      </asp:PlaceHolder></td>
             </tr>
                 <tr> <td>
                     Date : 
                 </td>
                 <td> <asp:PlaceHolder ID="ph2" runat="server">
                     <asp:Label ID="lblDate" runat="server" Text="Date"></asp:Label>
                      </asp:PlaceHolder></td>
             </tr>
                   <tr> <td>
                     Time : 
                 </td>
                 <td> <asp:PlaceHolder ID="ph3" runat="server">
                     <asp:Label ID="lblTime" runat="server" Text="Time"></asp:Label>
                      </asp:PlaceHolder></td>
             </tr>
                   <tr> <td>
                     Location : 
                 </td>
                 <td> <asp:PlaceHolder ID="ph4" runat="server">
                     <asp:Label ID="lblLocation" runat="server" Text="Location"></asp:Label>
                      </asp:PlaceHolder></td>
             </tr>
        </table>
   </div>
    </form>
</body>
</html>




<%--<%@ Register TagPrefix="uc" tagname="TemplateTest" 
    Src="~/Modules/HR/EmailTemplate.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN"
    "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server" >
<script runat="server">
    protected void Page_Load()
    {
        DataBind();
    }
    
</script>
<title>Templated User Control Test</title>
</head>
<body>
<h1>Eamil F</h1>
<form id="Form2" runat="server">
<uc:TemplateTest runat="server">
  <MessageTemplate>
    Index: <asp:Label runat="server" ID="Label1" 
        Text='<%# Container.Index %>' />
    <br />
    Message: <asp:Label runat="server" ID="Label2" 
        Text='<%# Container.Message %>' />
    <hr />
  </MessageTemplate>
</uc:TemplateTest>
</form>
</body>
</html>--%>