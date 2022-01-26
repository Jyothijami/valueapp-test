<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.master" AutoEventWireup="true" CodeFile="DailyReportView.aspx.cs" Inherits="dev_pages_DailyReportView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   

    <script lang="javascript" type="text/javascript">
        function OpenPopupCenter(pageURL, title, w, h) {
            var left = (screen.width - w) / 2;
            var top = (screen.height - h) / 4;  // for 25% - devide by 4  |  for 33% - devide by 3
            var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
        }
    </script>


    <%--<script type="text/javascript">
        $(document).ready(function () {
            //fnPageLoad();
        });
        function fnPageLoad() {
            $('#<%=gvDrs.ClientID%>').prepend($("<thead></thead>").append($('#<%=gvDrs.ClientID%>').find("tr:first"))).DataTable({

                bSort: true,
                dom: '<"html5buttons"B>lTfgitp',
                lengthChange: false,
                pageLength: 50,

                bStateSave: true,
                order: [[1, 'desc']],

                //scrollX: true,
                // fixedHeader : true,


                fixedHeader: {
                    header: true,
                    footer: true
                }

            });
        }
    </script>--%>


    <%--<script type="text/javascript">
        $(document).ready(function () {
            fnPageLoad1();
        });
        function fnPageLoad1() {
            $('#<%=gvEmplist.ClientID%>').prepend($("<thead></thead>").append($('#<%=gvEmplist.ClientID%>').find("tr:first"))).DataTable({

                bSort: true,
                dom: '<"html5buttons"B>lTfgitp',
                lengthChange: false,
                pageLength: 50,
                info: false,
                bStateSave: true,
                order: [[0, 'desc']],

                //scrollX: true,
                // fixedHeader : true,


                fixedHeader: {
                    header: true,
                    footer: true
                }

            });
        }
    </script>--%>

    


       
   
      <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"  >
            <ContentTemplate>




    <div class ="row">

        <div class="panel panel-default">

            

            <div class="panel-body">
                
        <div class="form-group">
            <label class="col-sm-2 control-label text-right">From Date :</label>
                            <div class="col-sm-4">
                    <asp:TextBox ID="txtFromDate" type="datepic" CssClass="form-control" runat="server"></asp:TextBox>

                            </div>
            <label class="col-sm-2 control-label text-right">To Date :</label>
                            <div class="col-sm-4">
                        <asp:TextBox ID="txtToDate" CssClass="form-control" type="datepic" runat="server"></asp:TextBox>
                            </div>
        </div>

                <div style="margin-top:50px"></div>

        <div class="form-group">
            <label class="col-sm-2 control-label text-right">Clients Name :</label>
                            <div class="col-sm-4">
                                        <asp:TextBox ID="txtClientName" CssClass="form-control" runat="server"></asp:TextBox>


                            </div>
            <label class="col-sm-2 control-label text-right">Employee Name :</label>
                            <div class="col-sm-4">
                                                <asp:DropDownList ID="ddlSalesPerson" CssClass="form-control" runat="server" AutoPostBack="True"></asp:DropDownList>

                            </div>
        </div>
                 <div style="padding-top:50px"></div>
                <div class="form-group text-right">
                              <asp:Button ID="btnSearch" CssClass="btn-small btn-primary" runat="server" Text="Search" OnClick="btnSearch_Click"  />
                 </div>
            </div>
        </div>

      </div>






    <div class="row">


        <%--<div class="col-md-2" >


           

            <div class="panel panel-default">
                <div class="panel-body">
                    <asp:GridView ID="gvEmplist" runat ="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="false"  Width="100%" >
    <Columns >
        <asp:TemplateField HeaderText="" >
                                <ItemTemplate>
                                    <asp:Image ID="Image_Spec" runat="server" EnableTheming="False" Height="80px" ImageUrl='<%# Eval("EMP_PHOTO","~/Content/EmployeeImage/{0}") %>'
                                        Width="80px"  />
                                </ItemTemplate>
                            </asp:TemplateField>
        <asp:TemplateField HeaderText="">
                                
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Text='<%# Eval("EMP_FIRST_NAME") %>' Font-Size="Smaller" Font-Bold="True"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
    </Columns>
</asp:GridView>
                </div>
            </div>





<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                        SelectCommand="SELECT * FROM [YANTRA_EMPLOYEE_MAST] a inner join dbo.YANTRA_EMPLOYEE_DET b on a.EMP_ID=b.EMP_ID inner join YANTRA_DEPT_MAST c on c.DEPT_ID=b.DEPT_ID WHERE a.EMP_ID<>0  and ( c.DEPT_NAME like '%Sales%' or b.DEPT_ID=11) and STATUS !=0  ORDER BY EMP_FIRST_NAME">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="0" Name="EMP_ID" Type="Int64" />
                        </SelectParameters>
                    </asp:SqlDataSource>



        </div>--%>

      
            <asp:GridView ID="gvDrs" Width="100%" runat="server" AllowPaging ="true" PageSize ="10" OnPageIndexChanging ="gvDrs_PageIndexChanging" CssClass="display" SelectedRowStyle-BackColor="#c0c0c0" EmptyDataText="No Records To Display" AutoGenerateColumns="False" OnRowDataBound="gvDrs_RowDataBound" Font-Names="Calibri" Font-Size="Small">
        <Columns>

            <%--   <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkhdr" runat="server" AutoPostBack="True" OnClick="selectAll(this)" />
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="Chk" OnCheckedChanged="Chk_CheckedChanged" AutoPostBack="true" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>

            <asp:TemplateField HeaderText="Form Id" SortExpression="DAILYREPORTID" Visible="false">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblId" Text='<%#Eval("DAILYREPORTID")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date">
                <ControlStyle Font-Bold="True" Font-Size="Medium" />
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
            </asp:BoundField>

           <%-- <asp:TemplateField HeaderText="Date" HeaderStyle-CssClass="text-center">
                                 <ItemTemplate>
                                     <span class="text-center">

                                         <asp:Label runat="server" ID="lbldate" Text="<%#Eval("Date")%>" ></asp:Label>
                                     </span>
                                 </ItemTemplate>
                               
<HeaderStyle CssClass="text-center"></HeaderStyle>
                                 <ItemStyle VerticalAlign="Top" />
                               
                           </asp:TemplateField>--%>

            <asp:TemplateField HeaderText="Customer Details" HeaderStyle-CssClass="text-center">
                <ItemTemplate>

                    <div class="row">

                        <div class="col-md-6">
                            <blockquote>
                                <strong>Start Time :</strong> <%#Eval("In TIME")%> - <strong>End Time :</strong><%#Eval("Out TIME")%><br /><strong>Name :</strong> <%#Eval("Client Name")%>
                                <br />
                                <strong>Phone :</strong> <%#Eval("PHONE")%> &nbsp; <strong>Email :</strong> <%#Eval("Email")%>  
                                <br />
                                <strong>Address :</strong> <%#Eval("ADDRESS")%>
                                <br />
                               <strong> Reference :</strong>  <%#Eval("Reference")%>
                                <br />
                                <strong>Architect :</strong><%#Eval("Architect")%><br /><span class="text-danger"><strong>AttendedBy :</strong> <%#Eval("Attended By")%></span><br />
                                <span class="text-danger"><strong>Backup : </strong><%#Eval("Backup Name")%></span>
                            </blockquote>
                        </div>

                        <div class="col-md-6">

                           
                            <blockquote>
                                <div class="row">
                                    <strong>Purpose :</strong>  <%#Eval("PURPOSE")%>
                                </div>
                                <div class="row" style="padding-top: 5px">
                                    <strong>Remarks :</strong><%#Eval("REMARKS")%></div>
                                <div class="row" style="padding-top: 5px">
                                    <strong>Plan of Action :</strong><%#Eval("FileName")%></div>
                            </blockquote>

                             <blockquote>
                                            <div class="row text-danger">
                                    <strong>What did you achive Yesterday ? :</strong>  <%#Eval("Achiveyesterday")%>
                                </div>
                                <div class="row text-danger" style="padding-top: 5px">
                                    <strong>What do you plan to achive Today ? :</strong><%#Eval("AchiveToday")%></div>
                                        </blockquote>




                        </div>


                    </div>






                </ItemTemplate>

                <HeaderStyle CssClass="text-center"></HeaderStyle>
            </asp:TemplateField>


            <%--<asp:TemplateField HeaderText ="Executive Details" HeaderStyle-CssClass="text-center">
                            <ItemTemplate>

                               
                                
                                <div class="row">
                                   
                                   
                                         </div>

                                <div class="row">
                                    

                                   
                            </ItemTemplate>
                            <HeaderStyle CssClass="text-center"></HeaderStyle>
                            <ItemStyle VerticalAlign="Top" />
                        </asp:TemplateField>--%>


            <%--     <asp:BoundField DataField="In TIME" HeaderText="In TIME" SortExpression="In TIME">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>--%>

            <%--   <asp:BoundField DataField="Out TIME" HeaderText="Out TIME" SortExpression="Out TIME">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>--%>

            <%-- <asp:BoundField DataField="Client Name" HeaderText="Client" SortExpression="Client Name" ItemStyle-Wrap="false">
                            
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle" Wrap="true" />
                        </asp:BoundField>--%>

            <%--  <asp:BoundField DataField="PHONE" HeaderText="PHONE" SortExpression="PHONE">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>--%>

            <%--  <asp:BoundField DataField="Reference" HeaderText="Reference" SortExpression="Reference">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                        </asp:BoundField>--%>

            <%--<asp:BoundField DataField="Architect" HeaderText="Architect" SortExpression="Architect">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                        </asp:BoundField>--%>

            <%--  <asp:BoundField DataField="ADDRESS" HeaderText="Address" ItemStyle-Wrap="false" SortExpression="ADDRESS">
                            
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                        </asp:BoundField>--%>

            <%--    <asp:BoundField DataField="PURPOSE" HeaderText="Purpose" ItemStyle-Wrap="false" SortExpression="PURPOSE">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                        </asp:BoundField>--%>

            <%--   <asp:BoundField DataField="REMARKS" HeaderText="Remarks"  ItemStyle-Wrap="false" SortExpression="REMARKS">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                        </asp:BoundField>--%>
            <%-- <asp:TemplateField HeaderText="Purpose" HeaderStyle-CssClass="text-center">
                                 <ItemTemplate>
                                     <span class="text-center">
                                         <a runat="server" href='<%# "~/Modules/SM/RemarksView.aspx?Cid=" + Eval("DAILYREPORTID") %>' onclick="OpenPopupCenter(this.href, 'newwindow', 800, 500);return false; "><%#Eval("NewPurpose")%>..More</a>
                                     </span>
                                 </ItemTemplate>
                              
<HeaderStyle CssClass="text-center"></HeaderStyle>
                                 <ItemStyle VerticalAlign="Top" />
                              
                           </asp:TemplateField>--%>



            <%--<asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="text-center">
                                 <ItemTemplate>
                                     <span class="text-center">
                                         <a runat="server" href='<%# "~/Modules/SM/RemarksView.aspx?Cid=" + Eval("DAILYREPORTID") %>' onclick="OpenPopupCenter(this.href, 'newwindow', 800, 500);return false; "><%#Eval("NewReamrks")%>..More</a>
                                     </span>
                                 </ItemTemplate>
                               
<HeaderStyle CssClass="text-center"></HeaderStyle>
                                 <ItemStyle VerticalAlign="Top" />
                               
                           </asp:TemplateField>--%>

            <%-- <asp:BoundField DataField="Attended By" HeaderText="Attended By" SortExpression="Attended By" ItemStyle-Wrap="false">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Backup Name" HeaderText="Backup Name" SortExpression="Backup Name" ItemStyle-Wrap="false">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true"/>
                        </asp:BoundField>--%>
            <asp:TemplateField HeaderText="Comments">
                <ItemTemplate>
                    <asp:TextBox ID="txtComment" TextMode="SingleLine" Width="150px" Text='<%#Eval("Comments")%>' runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="DAILYREPORTID" HeaderText="Id" />
            <asp:TemplateField HeaderText="FileName">
                <ItemTemplate>
                    <asp:Image runat="server" EnableTheming="False" ImageUrl='<%# Eval("FileName","http://183.82.108.55/YANTRA_DOCUMENTS/SOFiles/{0}") %>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Comments" HeaderStyle-CssClass="text-center">
                <ItemTemplate>

                              

                    <div class="row">

                        <div class="col-md-12">

                            <%--   <asp:LinkButton ID="lbtnfloorplans" runat="server" CssClass="btn btn-danger" PostBackUrl='<%# "~/Modules/SM/RemarksView.aspx?Cid=" + Eval("DAILYREPORTID") %>' OnClientClick="OpenPopupCenter(this.href, 'newwindow', 800, 500);return false;">
                                    <span style="color:white"> Comments </span>
                                    <strong class="label label-primary">
                                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("Cou") %>'></asp:Label></strong>
                                </asp:LinkButton>--%>

                             <span class="text-center">
                        <a runat="server" class="btn btn-icon btn-success" href='<%# "~/Modules/SM/RemarksView.aspx?Cid=" + Eval("DAILYREPORTID") %>' onclick="OpenPopupCenter(this.href, 'newwindow', 800, 500);return false; ">
                           <%-- <i class="icsw16-speech-bubbles"></i><span class="badge badge-important">
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("Cou") %>'></asp:Label></strong></span>--%>
                            <span style="color:white"><strong> Comments</strong> </span>
                            <asp:Label ID="Label1" CssClass="label label-danger" runat="server" Text='<%#Eval("Cou") %>'></asp:Label>

                        </a>

                    </span>

                        </div>

                        </div>
                    <div class="row" style="margin-top:5px">
                        

                        <div class="col-md-12" >

                            <%--  <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary" PostBackUrl='<%# "~/Modules/SM/DailyReportDoc1.aspx?Cid=" + Eval("DAILYREPORTID") %>' OnClientClick="OpenPopupCenter(this.href, 'newwindow', 800, 500);return false;">
                                  
                                   <span style="color:white"> Documents </span>
                                    <strong class="label label-danger">
                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("DocCou") %>'></asp:Label></strong>
                                </asp:LinkButton>--%>
                             <span class="text-center">
                        <a runat="server" class="btn btn-icon btn-primary" href='<%# "~/Modules/SM/DailyReportDoc1.aspx?Cid=" + Eval("DAILYREPORTID") %>' onclick="OpenPopupCenter(this.href, 'newwindow', 800, 500);return false; ">
                            <span style="color:white"><strong> Documents</strong> </span>
                                <asp:Label ID="Label2"  CssClass="label label-danger"  runat="server" Text='<%#Eval("DocCou") %>'></asp:Label>
                        </a>

                    </span>



                        </div>



                    </div>




                   
                   
                </ItemTemplate>

                <HeaderStyle CssClass="text-center"></HeaderStyle>

            </asp:TemplateField>

             <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <div class="row">
                        <div class="col-md-12 ">
                            <span class="text-center">
                        <asp:HyperLink runat="server" ID ="btnedit1" class="btn btn-icon btn-primary " href='<%# "DailyReportEdit.aspx?Cid=" + Eval("DAILYREPORTID") %>' onclick="OpenPopupCenter(this.href, 'newwindow', 800, 500);return false; ">
                           <%-- <i class="icsw16-speech-bubbles"></i><span class="badge badge-important">
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("Cou") %>'></asp:Label></strong></span>--%>
                            <span style="color:white"><strong> Edit</strong> </span>

                        </asp:HyperLink>

                    </span>
                            </div>
                    </div>

                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField ="DR_FollowUp" HeaderText ="DR_FollowUp" />
            <asp:TemplateField >
                <ItemTemplate >
                    <asp id="lblDttime" runat="server" Text='<%#Eval("DATETIME")%>'></asp>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField >
                <ItemTemplate >
                    <asp id="lblCreatedOn" runat="server" Text='<%# Eval("createdOn").Equals(DateTime.MinValue) ? "" : Eval("createdOn")%>'></asp>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="Silver"></SelectedRowStyle>

    </asp:GridView>
      


<%--         <div class="col-md-2">

        <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>

        </div>--%>



    </div>





    




    <asp:Label ID="lblUserType" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblDeptId" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblDeptHeadId" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblDeptHead" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblUserId" runat="server" Visible="false"></asp:Label>


                </ContentTemplate>
          <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
       
        </Triggers>
          </asp:UpdatePanel>

    

</asp:Content>

