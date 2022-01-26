<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true" CodeFile="NewEmpRating1.aspx.cs" Inherits="Survey_NewEmpRating" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <title></title>
     <link rel="stylesheet" href="https://cdn.datatables.net/1.10.12/css/jquery.dataTables.min.css" type="text/css" />
    <%--<script type="text/javascript" src="https://code.jquery.com/jquery-1.10.2.js"></script>--%>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.12/js/jquery.dataTables.min.js"></script>
    <%-- <link href="../../css/bootstrap.min.css" rel="stylesheet" />--%>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
   <%-- <link href="https://cdn.datatables.net/fixedheader/3.1.5/css/fixedHeader.dataTables.min.css" rel="stylesheet"/>--%>
       

     <script type="text/javascript">
         $(function () {
             $("#btnCustSave").click(function () {
                 $("#myModal").modal("hide");
             });
         });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    
    <asp:GridView ID="gvEmployeeMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False" AllowSorting="true"
        DataKeyNames="EMP_ID" meta:resourcekey="gvEmployeeMasterResource1" DataSourceID="sdsEmployeeMaster" Width="100%">
        <Columns>
            <asp:BoundField HeaderText="Sl.No" Visible="false" SortExpression="Sl.No" meta:resourceKey="BoundFieldResource1"></asp:BoundField>
            <asp:BoundField DataField="EMP_ID" SortExpression="" Visible="false" HeaderText="Emp Id">
                <ItemStyle HorizontalAlign="Right"></ItemStyle>

                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
            </asp:BoundField>
            <asp:BoundField ReadOnly="True" DataField="EMP_ID" SortExpression="EMP_ID" Visible="false" HeaderText="EMPIDHidden" meta:resourceKey="BoundFieldResource2"></asp:BoundField>
            <%-- <asp:TemplateField HeaderText="Employee Name">
                            

                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemTemplate>
                                        <asp:LinkButton ID="lbtnEmpFirstName" OnClick="lbtnEmpFirstName_Click" runat="server" Text='<%# Bind("Emp_Name") %>' CausesValidation="False" ForeColor="Blue" meta:resourcekey="lbtnEmpFirstNameResource1" __designer:wfdid="w6"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
            <asp:BoundField DataField="Emp_Name" HeaderText="Emp Name" />

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
                                <label class="col-sm-2 control-label text-right">
                                    <button type="button" class="btn btn-danger" data-toggle="modal" data-target="#myModal">Vote</button><%--<asp:Button ID="btnAddnew" CssClass="btn btn-warning" runat="server" Text="New" OnClick="btnAddnew_Click" />--%>
                                    <%--Customer :<span class="mandatory" style="font-size:17px">*</span>--%>
                                </label>

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


    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Appraisals</h4>
                </div>
                <div class="modal-body">
                    <asp:Panel ID="QPanel" runat="server">
                        <div class="form-horizontal">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <h3 class="panel-title">Reporting Head Apprisals</h3>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnCustSave" class="btn btn-danger" runat="server" Text="Save" />
                    <button type="button" id="btndiaclose" runat="server" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

