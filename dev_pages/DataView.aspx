<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataView.aspx.cs" Inherits="dev_pages_DataView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Data View</title>
      <meta name="viewport" content="width=device-width,initial-scale=1" />
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.1/jquery.min.js"></script>
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <script type="text/javascript">
            // Let's use a lowercase function name to keep with JavaScript conventions
            function selectAll(invoker) {
                // Since ASP.NET checkboxes are really HTML input elements
                //  let's get all the inputs 
                var inputElements = document.getElementsByTagName('input');

                for (var i = 0 ; i < inputElements.length ; i++) {
                    var myElement = inputElements[i];

                    // Filter through the input types looking for checkboxes
                    if (myElement.type === "checkbox") {

                        // Use the invoker (our calling element) as the reference 
                        //  for our checkbox status
                        myElement.checked = invoker.checked;
                    }
                }
            }
    </script>
    <div >
    <div class="page-header">
                <div class="page-title">
                    <h3>To do List Data View</h3>
                </div>
            </div>
            <div class="breadcrumb-line">
                <ul class="breadcrumb">
                   <li><a href="MobileHome.aspx">Menu</a></li>
                  
                    <li><a href="DailyReport.aspx">Daily Report</a></li>
                    <li class="active"><a href="ToDoList1.aspx">To Do List</a></li>
                    <li class="active"><a href="Emp_CR.aspx">Complaint Register</a></li>
                    <li class="active"><a href="DataView.aspx">ToDo List View</a></li>

                </ul>
            </div>
        <div class="panel panel-default">
            <div class="panel-body">
                <div class="form-group">
                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-10">
                                <div class="form-actions text-center">
                        <%--<asp:Button ID="btnListSearch" runat="server" Text="Search" OnClick="btnListSearch_Click" CausesValidation="False" />--%>
                <%--<asp:Button ID="btnListDelete" runat="server" Text="Delete" OnClick="btnListDelete_Click" Visible ="false"  CausesValidation="False" />--%>
                                    <asp:Button ID="btnListUpdate" runat ="server" Text ="Update" OnClick ="btnListUpdate_Click" Visible ="false" CausesValidation ="false" />
<asp:Label ID="lblItem" runat ="server" Text="Please change the status and update here" ForeColor ="Red" Visible ="false"  ></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-1"></div>
                        </div>
                    </div>
                <asp:GridView ID="gvList" Width="100%" OnRowDataBound ="gvlist_RowDataBound" runat="server" SelectedRowStyle-BackColor="#c0c0c0" EmptyDataText="No Records To Display" AllowPaging="True" OnPageIndexChanging="gvList_PageIndexChanging" PageSize="8" AutoGenerateColumns="False" >
                    <Columns>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkhdr" runat="server" AutoPostBack="True" OnClick="selectAll(this)" />
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="Chk" OnCheckedChanged="Chk_CheckedChanged" AutoPostBack="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Id" SortExpression="ID" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblId" Text='<%#Eval("ID")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                        </asp:BoundField>
                         <asp:BoundField DataField="IssuedDate" HeaderText="IssuedDate" SortExpression="IssuedDate">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                        </asp:BoundField>
                       

                        <asp:BoundField DataField="Subject" HeaderText="Subject" SortExpression="Subject" ItemStyle-Wrap="false">
                            
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle" Wrap="true" />
                        </asp:BoundField>
                       
                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                        </asp:BoundField>
                         <%-- <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                        </asp:BoundField>--%>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlStatus" runat="server">
                                   <asp:ListItem >To Do</asp:ListItem>
                                <asp:ListItem >In-Progress</asp:ListItem>
                                <asp:ListItem >Completed</asp:ListItem>
                                </asp:DropDownList>
                                <asp:HiddenField ID="cthf1" runat="server" Value='<%# Eval("Status") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PreparedBy" HeaderText="PreparedBy" SortExpression="PreparedBy" ItemStyle-Wrap="false">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                        </asp:BoundField>
                        <asp:BoundField DataField ="ID" HeaderText ="Id" />
                        <asp:TemplateField HeaderText ="Reporting To">
                            <HeaderStyle HorizontalAlign ="Center"  />
                             <ItemTemplate>
                                 <asp:GridView ID="gvChild" CssClass="subgridviews" ShowHeader ="false" EnableTheming ="false" Width ="100%"  runat ="server"  AutoGenerateColumns ="false">
                                                  <Columns >
                                                      <%--<asp:BoundField DataField ="ID" HeaderText ="ID" />--%>
                                                       
                                     <asp:BoundField  DataField ="EMP_FIRST_NAME" HeaderText ="Reporting To" >
                                          <ItemStyle HorizontalAlign ="Center" />
                         <HeaderStyle HorizontalAlign ="Center" /> </asp:BoundField>
                                     
                                                  </Columns>
                                             </asp:GridView>
                             </ItemTemplate>
                        </asp:TemplateField>
                       
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="Silver"></SelectedRowStyle>
                </asp:GridView>
                                    <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>

            </div>
        </div>
    </div>
    </form>
</body>
</html>
