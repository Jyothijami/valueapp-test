<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ToDoList1.aspx.cs" Inherits="ToDoList1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>To DO List</title>
   
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.1/jquery.min.js"></script>
    <link href="../../css/bootstrap.min.css" rel="stylesheet" />
    
    <link href="../../select/select2.css" rel="stylesheet" />
    <script src="../../select/select2.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
   
            <div class="page-header">
                <div class="page-title">
                    <h3>To Do List</h3>
                </div>
            </div>
            <div class="breadcrumb-line">
                <ul class="breadcrumb">
                    <li><a href="DR.aspx">Daily Report</a></li>
                    <li class="active"><a href="ToDoList1.aspx">To Do List</a></li>
                </ul>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h6 class="panel-title">To Do List</h6>
                </div>
                <div class="panel-body">
                    <div class="row">

                    <div class="col-md-1"></div>
                    <div class="col-md-5">
                        <asp:Label ID="Label1" runat ="server"  Text ="Date & Time" ></asp:Label>
                         <asp:TextBox ID="txtDateTime" runat ="server" CssClass="form-control" type="date"></asp:TextBox>
                    </div>
                    </div>
                    <div class ="row">
                        <div class="col-md-1"></div>
                        <div class="col-md-5">
                            <asp:Label ID="lbl2" runat ="server" Text ="Reporting To"></asp:Label>
                            <%-- <asp:DropDownList ID="ddluname1" runat="server" DataSourceID="usersds1" DataTextField="USER_NAME" DataValueField="USER_ID">
                                        </asp:DropDownList>--%>
                                        <select id="Books" class="form-control" runat="server"></select>

                                        <asp:SqlDataSource ID="usersds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [USER_ID], [USER_NAME] FROM [YANTRA_USER_DETAILS] where EXPIRY_DATE >='2019-12-31 00:00:00.000' ORDER BY [USER_NAME]"></asp:SqlDataSource>
                                        <asp:HiddenField ID="hffromuid1" runat="server" />


                                           <script>
                                               $(document).ready(function () {
                                                   $("#Books").select2({ placeholder: 'Find and Select Books' });
                                               });
                                               </script>
                        </div>
                    </div>
                    <div class="row">
                    <div class="col-md-1"></div>
                    <div class="col-md-10">
                        <asp:Label ID="Label2" runat ="server"  Text ="Subject" ></asp:Label>
                         <asp:TextBox ID="txtSubject" runat ="server" CssClass="form-control" TextMode ="MultiLine" ></asp:TextBox>
                    </div>
                    </div>
                    <div class="row">
                    <div class="col-md-1"></div>
                    <div class="col-md-10">
                        <asp:Label ID="Label3" runat ="server"  Text ="Task Activity Description" ></asp:Label>
                         <asp:TextBox ID="txtDescr" runat ="server" CssClass="form-control" TextMode ="MultiLine" ></asp:TextBox>
                    </div>
                    </div>
                    <div class="row">
                    <div class="col-md-1"></div>
                    <div class="col-md-5">
                        <asp:Label ID="Label4" runat ="server"  Text ="Task Activity Status" ></asp:Label>
                         <asp:DropDownList ID="ddlActivity" runat ="server" CssClass ="form-control" >
                                <asp:ListItem Value ="55" >To Do</asp:ListItem>
                                <asp:ListItem Value ="56" >In-Progress</asp:ListItem>
                                <asp:ListItem Value ="57">Completed</asp:ListItem>
                            </asp:DropDownList>
                    </div>
                    </div>
                </div>
                <div class="form-group">
                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-10">
                                <div class="form-actions text-right">
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-lg btn-danger " OnClick ="btnSave_Click" Text="Save" />
                                    <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                               
                                     </div>
                            </div>
                            <div class="col-md-1"></div>
                        </div>
                    </div>
                
            </div>

            <div class="panel panel-default">
                <div class="panel-heading">
                    <h6 class="panel-title">To DO List Details</h6>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <div class="row">

                    <div class="col-md-1"></div>
                    <div class="col-md-5">
                        <asp:Label ID="Label12" runat ="server"  Text ="From Date" ></asp:Label>
                         <asp:TextBox ID="txtFromDate" runat ="server" CssClass="form-control" type="datepic"></asp:TextBox>
                    </div>
                   <div class="col-md-5">
                        <asp:Label ID="Label13" runat ="server"  Text ="To Date" ></asp:Label>
                         <asp:TextBox ID="txtToDate" runat ="server" CssClass="form-control" type="datepic"></asp:TextBox>
                    </div>
                </div>
                        <div class="row">

                    <div class="col-md-1"></div>
                    <div class="col-md-5">
                        <asp:Label ID="Label14" runat ="server"  Text ="Client Name" ></asp:Label>
                         <asp:TextBox ID="txtClientName" runat ="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                   <div class="col-md-5">
                        <asp:Label ID="Label15" runat ="server"  Text ="Executive Name" ></asp:Label>
                         <asp:DropDownList ID="ddlSalesPerson" runat="server" CssClass ="form-control"></asp:DropDownList>
                    </div>
                </div>
                        <div class="form-group">
                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-10">
                                <div class="form-actions text-center">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CausesValidation="False" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" Visible ="false"  CausesValidation="False" />

                                </div>
                            </div>
                            <div class="col-md-1"></div>
                        </div>
                    </div>
                        
                        <div>
                            <asp:GridView ID="gvDrs" Width="100%" runat="server" SelectedRowStyle-BackColor="#c0c0c0" EmptyDataText="No Records To Display" AllowPaging="True" OnPageIndexChanging="gvDrs_PageIndexChanging" PageSize="8" AutoGenerateColumns="False" >
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
                          <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="PreparedBy" HeaderText="PreparedBy" SortExpression="PreparedBy" ItemStyle-Wrap="false">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                        </asp:BoundField>

                       
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="Silver"></SelectedRowStyle>
                </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <div id="lables">
                <asp:Label ID="lblUserType" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="Label5" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblDeptId" runat ="server" Visible ="false" ></asp:Label>
                                    <asp:Label ID="lblDeptHead" runat ="server" Visible ="false"  ></asp:Label>
                                    <asp:Label ID="lblDeptHeadId" runat ="server" Visible ="false" ></asp:Label>


            </div>
       
    </div>
    </form>
</body>
</html>
