<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewEmpRating-copy.aspx.cs"  Inherits="Survey_NewEmpRating" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="stylesheet" href="https://cdn.datatables.net/1.10.12/css/jquery.dataTables.min.css" type="text/css" />
    <%--<script type="text/javascript" src="https://code.jquery.com/jquery-1.10.2.js"></script>--%>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.12/js/jquery.dataTables.min.js"></script>
    <%-- <link href="../../css/bootstrap.min.css" rel="stylesheet" />--%>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
   <%-- <link href="https://cdn.datatables.net/fixedheader/3.1.5/css/fixedHeader.dataTables.min.css" rel="stylesheet"/>--%>
       

    <script lang="javascript" type="text/javascript">
        function OpenPopupCenter(pageURL, title, w, h) {
            var left = (screen.width - w) / 2;
            var top = (screen.height - h) / 4;  // for 25% - devide by 4  |  for 33% - devide by 3
            var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gvEmployeeMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False" AllowSorting="true" 
                            DataKeyNames="EMP_ID"  meta:resourcekey="gvEmployeeMasterResource1" DataSourceID ="sdsEmployeeMaster" Width="100%" >
                            <Columns>
                                <asp:BoundField HeaderText="Sl.No" Visible ="false"  SortExpression="Sl.No" meta:resourceKey="BoundFieldResource1"></asp:BoundField>
                                <asp:BoundField DataField="EMP_ID" SortExpression="" Visible ="false"  HeaderText="Emp Id">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField ReadOnly="True" DataField="EMP_ID" SortExpression="EMP_ID" Visible ="false"  HeaderText="EMPIDHidden" meta:resourceKey="BoundFieldResource2"></asp:BoundField>
                              <%-- <asp:TemplateField HeaderText="Employee Name">
                            

                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemTemplate>
                                        <asp:LinkButton ID="lbtnEmpFirstName" OnClick="lbtnEmpFirstName_Click" runat="server" Text='<%# Bind("Emp_Name") %>' CausesValidation="False" ForeColor="Blue" meta:resourcekey="lbtnEmpFirstNameResource1" __designer:wfdid="w6"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                <asp:BoundField DataField ="Emp_Name" HeaderText ="Emp Name" />
                                
                                <asp:BoundField DataField="Emp_Dept" SortExpression="Emp_Dept" HeaderText="Department" meta:resourceKey="BoundFieldResource5">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EMp_Desg" SortExpression="EMp_Desg" HeaderText="Designation" meta:resourceKey="BoundFieldResource6"></asp:BoundField>
                                <asp:BoundField DataField="Emp_DOJ" SortExpression="Emp_DOJ" HeaderText="DOJ" meta:resourceKey="BoundFieldResource7"></asp:BoundField>
                                <asp:BoundField DataField="Emp_Comp" SortExpression="Emp_Comp" HeaderText="Company" meta:resourceKey="BoundFieldResource9">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="locname" SortExpression="locname" HeaderText="locname"></asp:BoundField>
                               <asp:TemplateField HeaderText="Vote">
                <ItemTemplate>
                    <div class="row">
                        <div class="col-md-12 ">
                            <span class="text-center">
                        <a runat="server" class="btn btn-icon btn-primary " href='<%# "~/Survey/EmpRating.aspx?Cid=" + Eval("EMP_ID") %>' onclick="OpenPopupCenter(this.href, 'newwindow', 800, 500);return false; ">
                           <%-- <i class="icsw16-speech-bubbles"></i><span class="badge badge-important">
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("Cou") %>'></asp:Label></strong></span>--%>
                            <span style="color:white"><strong> Vote</strong> </span>

                        </a>

                    </span>
                            </div>
                    </div>

                </ItemTemplate>
            </asp:TemplateField>
                            </Columns>
                            <SelectedRowStyle BackColor="LightSteelBlue" />
                        </asp:GridView>
         <asp:SqlDataSource ID="sdsEmployeeMaster" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                            SelectCommand="[USP_Emp_Master_Search]" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                               <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="ReportingTo" ControlID="lblEmpIdHidden"></asp:ControlParameter>
                            </SelectParameters>
                        </asp:SqlDataSource>
                                    <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
    </div>
    </form>
</body>
</html>
