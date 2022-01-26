<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmpData.aspx.cs" Inherits="Modules_HR_EmpData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">

    <title>Untitled Page</title>
   <style type="text/css">
        
        #mask
        {
            position: fixed;
            left: 0px;
            top: 0px;
            z-index: 4;
            opacity: 0.4;
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=40)"; /* first!*/
            filter: alpha(opacity=40); /* second!*/
            background-color: gray;
            display: none;
            width: 100%;
            height: 100%;
        }
    </style>
    
   <!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css" />

<!-- jQuery library -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js" type="text/javascript"></script>

<!-- Latest compiled JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"  type="text/javascript"></script> 


    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    
</head>
<body>
    <script src="Scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function ShowPopup() {
            $('#mask').show();
            $('#<%=pnlpopup.ClientID %>').show();
        }
        function HidePopup() {
            $('#mask').hide();
            $('#<%=pnlpopup.ClientID %>').hide();
        }
        $(".btnClose").live('click', function () {
            HidePopup();
        });
    </script>
    <form id="form1" runat="server">
    <div>
        <div id="mask"></div>
        <asp:Panel ID="pnlpopup" runat="server"  BackColor="White"
             Style="z-index:111;background-color: White; position: absolute; left: 35%; top: 12%; border: outset 2px gray;padding:5px;display:none">
            <table>
                <tr>
                    <td style ="color :red " ><h4>Select Attachment type:</h4></td>
                    <td>
                        <asp:RadioButtonList ID="rdbDocType" runat ="server" RepeatDirection ="Horizontal" RepeatLayout="Flow"   >
                           <asp:ListItem>VoterId Card</asp:ListItem>
                                                    <asp:ListItem>Driving License</asp:ListItem>
                                                    <asp:ListItem>Ration Card</asp:ListItem>
                                                    <asp:ListItem>PassPort</asp:ListItem>
                                                    <asp:ListItem>Pan</asp:ListItem>
                                                    <asp:ListItem>Adhar Card</asp:ListItem>
                                                    <asp:ListItem>Telephone Bill</asp:ListItem>
                                                    <asp:ListItem>Bank Pass Book</asp:ListItem>
                        </asp:RadioButtonList><asp:Label ID="lblempid" runat ="server" Visible ="false" ></asp:Label>
                        <asp:Label ID="lbldocid" runat ="server" Visible ="false"  ></asp:Label>

                    </td>
                </tr>
                <tr>
                                            <td style="text-align: right">Upload Attachments:</td>
                                            <td>
                                                <asp:FileUpload ID="Uploadattach" runat="server" AllowMultiple="true" />
                                                <asp:ImageButton ID="ibtmImage" runat="server" ImageUrl="~/Images/tick.png" Width="18px" OnClick ="ibtmImage_Click" />
                                            </td>

                                        </tr>
                          <tr style ="text-align :right ">
                              <td>
                        <input type="button" class="btnClose" value="Close" />
                    </td>
                          </tr>              
            </table>
            <asp:GridView Width ="100%" ID="gvempdoc" OnRowDataBound ="gvempdoc_RowDataBound" AutoGenerateColumns ="false"  runat ="server" DataKeyNames ="EMP_ID" OnRowCommand ="gvempdoc_RowCommand" >
                  <Columns >
                      <asp:TemplateField HeaderText="Emp ID">
                    <ItemTemplate>
                        <asp:Label ID="lblstudent_Id" runat="server" Text='<%# Eval("EMP_ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                      <asp:TemplateField HeaderText="doc ID">
                    <ItemTemplate>
                        <asp:Label ID="lbldocid1" runat="server" Text='<%# Eval("Doc_ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                      <asp:BoundField DataField ="EMP_FIRST_NAME" HeaderText ="Emp Name" />
                      <%--<asp:BoundField DataField ="Document_Submitted" HeaderText ="Doc Name" />--%>
                      <asp:BoundField DataField ="Date_submitted" HeaderText ="Date Added" />
                      <asp:BoundField DataField ="Emp_Doc_Id" HeaderText ="Doc Name" />
                      <asp:TemplateField HeaderText="Image">
                                <ItemTemplate>
                                    <asp:Image ID="Image" runat="server" EnableTheming="False" Height="132px" ImageUrl='<%# Eval("Document_Submitted","~/Content/EmployeeDocuments/{0}") %>'
                                        Width="141px" /><br />
                                    <asp:FileUpload ID="fileupload1" runat ="server" Width ="100px"/>
                                    <asp:ImageButton ID="ibtmImage" runat="server" ImageUrl="~/Images/tick.png" CommandName ="Save" Width="18px" CommandArgument='<%# Eval("EMP_ID").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                  </Columns>
              </asp:GridView>
            
        </asp:Panel>


        <table width="100%">
            <tr>
                <td colspan="3">
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 335px">
                    <asp:GridView ID="gvEmployeeMaster" runat="server" AutoGenerateColumns="False" AllowSorting="true" DataKeyNames="EMP_ID"
                        DataSourceID="sdsEmployeeMaster" meta:resourcekey="gvEmployeeMasterResource1"
                        OnRowDataBound="gvEmployeeMaster_RowDataBound" CssClass="Grid" OnRowCommand ="gvEmployeeMaster_RowCommand" >
                        <Columns>
                            
                            <asp:TemplateField HeaderText="Documents">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkBtnEdit" runat="server" CommandName="ShowPopup"
                            CommandArgument='<%#Eval("Emp_ID") %>'>Edit</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                            <asp:BoundField HeaderText="Sl.No" meta:resourceKey="BoundFieldResource1" />
                            <asp:BoundField DataField="EMP_ID" HeaderText="EMPIDHidden" meta:resourceKey="BoundFieldResource2"
                                ReadOnly="True" SortExpression="EMP_ID" />
                            <asp:BoundField DataField="ASSIGNED_EMPID" SortExpression="ASSIGNED_EMPID" HeaderText="Assigned Emp Id">
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="FirstName" />
                            <asp:BoundField DataField="EMP_MIDDLE_NAME" HeaderText="Middle Name" meta:resourceKey="BoundFieldResource3"
                                SortExpression="EMP_MIDDLE_NAME" Visible="False">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EMP_LAST_NAME" HeaderText="Last Name" meta:resourceKey="BoundFieldResource4"
                                SortExpression="EMP_LAST_NAME">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EMP_DOB" DataFormatString="{0:dd/MM/yyyy}" HeaderText="DOB"
                                HtmlEncode="False" SortExpression="EMP_DOB" />
                            <asp:BoundField DataField="EMP_GENDER" HeaderText="Gender" meta:resourceKey="BoundFieldResource5"
                                SortExpression="EMP_GENDER">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EMP_MOBILE" HeaderText="Emp Phone No" meta:resourceKey="BoundFieldResource6"
                                SortExpression="EMP_MOBILE" />
                            <asp:BoundField DataField="ASSIGNED_MOBILENO" HeaderText="A.Mobile No" meta:resourceKey="BoundFieldResource7"
                                SortExpression="ASSIGNED_MOBILENO" />
                            <asp:BoundField DataField="CP_FULL_NAME" HeaderText="Company" meta:resourceKey="BoundFieldResource9">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DEPT_NAME" HeaderText="DeptName" />
                            <asp:BoundField DataField="DESG_NAME" HeaderText="Designation" />
                            <asp:BoundField DataField="EMP_DET_DOJ" HeaderText="DoJ" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" />
                            <asp:BoundField DataField="EMP_DET_DOT" DataFormatString="{0:dd/MM/yyyy}" HeaderText="DOT"
                                HtmlEncode="False" SortExpression="EMP_DET_DOT" />
                            <asp:BoundField DataField="GROSS_SAL" HeaderText="GrossSal" />
                            <asp:BoundField DataField="ASSIGNED_ACC_NO" HeaderText="AccountNo" SortExpression="ASSIGNED_ACC_NO" />
                            <asp:BoundField DataField="INSURANCE_INFO" HeaderText="Insurance" SortExpression="INSURANCE_INFO" />
                            <asp:BoundField DataField="PERMANET_PHONENO" HeaderText="PF.No" SortExpression="PERMANET_PHONENO" />
                            <asp:BoundField DataField="EMP_ADDRESS" HeaderText="ADDRESS" SortExpression="EMP_ADDRESS" />
                            <asp:BoundField DataField="CP_SHORT_NAME" HeaderText="Company" SortExpression="CP_SHORT_NAME" />
                          <%--  <asp:CommandField ShowSelectButton ="true" />--%>

                        </Columns>
                        <SelectedRowStyle BackColor="LightSteelBlue" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsEmployeeMaster" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                        SelectCommand="SP_HR_EMPLOYEEMASTERdata_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    </asp:SqlDataSource>
                </td>
                
            </tr>
            <tr>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Excel" /></td>
                <td style="width: 100px">
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
