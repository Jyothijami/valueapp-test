<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/SalesMP1.master" AutoEventWireup="true" CodeFile="Architect.aspx.cs" Inherits="Modules_SM_Architect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">

    <%--<script type="text/javascript">

        $(document).ready(function () {
            $("#<%=txtArchitectName.ClientID%>").keyup(function () {
                var username = $(this).val();

                if (username.length >= 3) {
                    $.ajax({
                        url: 'Architect.asmx/ArchNameExists',
                        method: 'get',
                        data: { userName: username },

                        dataType: 'json',

                        success: function (data) {

                            var divElement = $('#divOutput2');
                            if (data.UserNameInUse) {
                                divElement.text(data.Name + ' already Exist');
                                divElement.css('color', 'red');
                            }
                            else {
                                divElement.text(data.Name + ' available');
                                divElement.css('color', 'green');
                            }
                        },
                        error: function (err) {
                            alert(err);
                        }
                    })
                }

            })
        })
    </script>--%>
    <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="searchhead" colspan="2" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left;">
                            Architect Master</td>
                        <td>
                        </td>
                        <td style="text-align: right;">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td rowspan="3">
                                        <asp:Label id="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td rowspan="3">
                                        <asp:DropDownList id="ddlSearchBy" runat="server" CssClass="textbox" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="Architect_ID">S.No</asp:ListItem>
                                            <asp:ListItem Value="Architect_NAME">Name</asp:ListItem>
                                            <asp:ListItem Value="Architect_Address">Address</asp:ListItem>
                                             <asp:ListItem Value="Architect_Mobile">Mobile</asp:ListItem>
                                            <asp:ListItem Value="Architect_Email">Email</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td rowspan="3">
                                        </td>
                                    <td rowspan="3">
                                        <asp:TextBox id="txtSearchText" runat="server" CssClass="textbox"></asp:TextBox>&nbsp;
                                    </td>
                                    <td rowspan="3">
                                        <asp:Button id="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnSearchGo_Click" /></td>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                </tr>
                            </table>
                            <asp:Label id="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:GridView id="gvArch" SelectedRowStyle-BackColor="#c0c0c0" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnRowDataBound="gvCompanyDetails_RowDataBound" DataSourceID="sdsCategoryDetails" Width="100%">
                    <columns>
<asp:BoundField DataField="Architect_NAME" HeaderText="ArchitectNameHidden"></asp:BoundField>
<asp:BoundField DataField="Architect_ID" HeaderText="S.No">
<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Name"><EditItemTemplate>
<asp:TextBox runat="server" Text='<%# Bind("Architect_NAME") %>' id="TextBox1"></asp:TextBox>
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnCompanyName" ForeColor="#0066ff" onclick="lbtnCompanyName_Click" runat="server" Text="<%# Bind('Architect_NAME') %>" CausesValidation="False"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="Architect_Address" HeaderText="Address" NullDisplayText="-">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Architect_Mobile" HeaderText="Mobile No" NullDisplayText="-">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Architect_EMail" HeaderText="Email" NullDisplayText="-">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
                        <asp:BoundField DataField="Category" HeaderText="Category" NullDisplayText="-">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
                         <asp:BoundField DataField="City" HeaderText="City" NullDisplayText="-">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
                         <asp:BoundField DataField="Pincode" HeaderText="Pincode" NullDisplayText="-">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
</columns>
                    <selectedrowstyle backcolor="LightSteelBlue" />
                    <EmptyDataTemplate>
                        No Record Found
                    </EmptyDataTemplate>
                </asp:GridView><asp:SqlDataSource id="sdsCategoryDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_MASTER_ARCHITECT_SEARCH_SELECT" SelectCommandType="StoredProcedure"><selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
</selectparameters></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 19px; text-align: left">
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center; height: 49px;">
                <table id="Table1" align="center">
                    <tr>
                        <td>
                            <asp:Button id="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CausesValidation="False" Font-Underline="False" /></td>
                        <td>
                            <asp:Button id="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CausesValidation="False" /></td>
                        <td>
                            <asp:Button id="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" CausesValidation="False" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <table style="width: 783px" border="0" cellpadding="0" cellspacing="0" id="tblCompanyDetails" runat="server" visible="false" align="center">
                    <tr>
            <td colspan="4" style="text-align: left" class="profilehead">
               Architect / Interior / Builder Details</td>
                    </tr>
                    <tr>
            <td style="height: 21px; text-align: right">
            </td>
            <td style="height: 21px; text-align: left">
            </td>
                    </tr>
                    <tr>
            <td style="text-align: right"><asp:Label id="Label2" runat="server" Text="Category Type :" Width="153px"></asp:Label></td>
            <td style="text-align: left;"><asp:DropDownList ID="ddlCategory" runat ="server" >
                                          </asp:DropDownList>
               
            </td>
                    </tr>
                    <tr>
            <td style="text-align: right"><asp:Label id="lblArchitectName" runat="server" Text="Name :" Width="153px"></asp:Label></td>
            <td style="text-align: left;"><asp:TextBox id="txtArchitectName" runat="server"  ></asp:TextBox>
                <asp:Label ID="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                <asp:RequiredFieldValidator id="RFVDeptName" runat="server" ControlToValidate="txtArchitectName"
                    ErrorMessage="Please Enter the Category Name">*</asp:RequiredFieldValidator><cc1:filteredtextboxextender id="ftxteDepartmentName" runat="server" filtermode="InvalidChars"
                    invalidchars="'><" targetcontrolid="txtArchitectName"></cc1:filteredtextboxextender>
<%--                <span id="divOutput2"></span>
                                                                <span id="CustomerDetails1"></span>--%>
            </td>
                        <td style="text-align: right"><asp:Label id="Label1" runat="server" Text="Mobile No :" Width="153px"></asp:Label></td>
            <td style="text-align: left;"><asp:TextBox id="txtMobile" runat="server" MaxLength="20" ></asp:TextBox>
                <%--<span id="divOutput1"></span>
                                                                <span id="ArchitectDetails"></span>--%>
            </td>
                    </tr>
                    <tr>
             <td style="text-align: right"><asp:Label id="Label9" runat="server" Text="City :" Width="153px"></asp:Label></td>
            <td style="text-align: left;"><asp:TextBox id="txtCity" runat="server"  ></asp:TextBox>
               
            </td>
                        <td style="text-align: right"><asp:Label id="Label3" runat="server" Text="Email :" Width="153px"></asp:Label></td>
            <td style="text-align: left;"><asp:TextBox id="txtEmail" runat="server"  ></asp:TextBox>
               
            </td>
                  
                    </tr>
                    <tr>
              </tr>

                    <tr>
            <td style="text-align: right">
                <asp:Label id="lblDescription" runat="server" Text="Address :" Width="105px"></asp:Label></td>
            <td style="text-align: left"><asp:TextBox id="txtAddress" runat="server" TextMode="MultiLine"></asp:TextBox></td>
 
                         <td style="text-align: right"><asp:Label id="Label5" runat="server" Text="Organization Name :" Width="153px"></asp:Label></td>
            <td style="text-align: left;"><asp:TextBox id="txtOrgName" runat="server"  ></asp:TextBox>
               
            </td>
                    </tr>
                    <tr>
           <td style="text-align: right"><asp:Label id="Label8" runat="server" Text="PinCode :" Width="153px"></asp:Label></td>
            <td style="text-align: left;"><asp:TextBox id="txtPincode" runat="server" MaxLength ="6"  ></asp:TextBox>
               
            </td>
                         <td style="text-align: right"><asp:Label id="Label6" runat="server" Text="Designation :" Width="153px"></asp:Label></td>
            <td style="text-align: left;"><asp:TextBox id="txtDesg" runat="server"  ></asp:TextBox></td>
                    </tr>
                    <tr>
           
                    </tr>
                    <tr>
                                <td style="text-align: right">Attachment</td>
                                <td style="text-align: left"><asp:FileUpload ID="Uploadattach" runat="server" AllowMultiple="true" />
                                    <asp:Label ID="txtBrowsetxt" runat ="server" Text="Please Upload Related Files (Ex:Visting Card Front)" ForeColor ="Red"></asp:Label>
                                    <asp:Label ID="lblAtt" runat ="server" Visible ="false" ></asp:Label></td>
                         <td style="text-align: right">Attachment</td>
                                <td style="text-align: left"><asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="true" />
                                    <asp:Label ID="Label4" runat ="server" Text="Please Upload Related Files (Ex:Visting Card Back)" ForeColor ="Red"></asp:Label>
                                    <asp:Label ID="lblAtt1" runat ="server" Visible ="false" ></asp:Label></td>
                            </tr>
                    <tr>
                               
                            </tr>
                    <tr>
            <td style="text-align: right; height: 19px;">
            </td>
            <td style="text-align: left; height: 19px;">
            </td>
                    </tr>
                    <tr>
            <td colspan="2" style="text-align: center">
                <table id="tblButtons">
                    <tr>
                        <td>
                            <asp:Button id="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                        </td>
                        <td>
                            <asp:Button id="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" CausesValidation="False" /></td>
                        <td>
                            <asp:Button id="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" /></td>
                    </tr>
                </table>
            </td>
                    </tr>
                </table>
                <asp:ValidationSummary id="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False">
                </asp:ValidationSummary>
            </td>
        </tr>
        <tr>
            <td style="height: 21px">
            </td>
            <td style="height: 21px;">
            </td>
        </tr>
    </table>
</asp:Content>

