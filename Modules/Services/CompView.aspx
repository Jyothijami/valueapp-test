<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/ServicesMP1.master" AutoEventWireup="true" 
    CodeFile="CompView.aspx.cs" Inherits="Modules_Services_CompView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead" style="width: 100%">
        <tr><td style="text-align: left">URL Raised Complaints</td></tr>
        <tr><td style="text-align: right">
                <asp:DropDownList ID="ddlNoOfRecords" runat="server" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>75</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                </asp:DropDownList>
            </td></tr>
    </table> 
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
                    <td colspan="4" style="text-align: left" class="searchhead">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%" id="TABLE3" onclick="return TABLE3_onclick()">
                            <tr>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" align="right" width="100%">
                                        <tr>
                                            <td style="text-align: left">Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                                                <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
                                            </td>

                                            <td style="height: 25px; text-align: right;">
                                                <asp:Label ID="Label112" CssClass="label" runat="server" EnableTheming="False" Font-Bold="True"
                                                    Text="Search By"></asp:Label>
                                                <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                                    OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">--</asp:ListItem>
                                                    <%--<asp:ListItem Value="CR_NO">CR No</asp:ListItem>--%>
                                                    <asp:ListItem Value="CR_DATE">CR Date</asp:ListItem>
                                                    <asp:ListItem Value="User_Affected">Call Type</asp:ListItem>
                                                    <asp:ListItem Value="ClientName">Customer</asp:ListItem>
                                                    <asp:ListItem Value="Email">Contact Person</asp:ListItem>
                                                    <%--<asp:ListItem Value="CR_STATUS">Status</asp:ListItem>--%>
                                                    <asp:ListItem Value="Mobile">Mobile No.</asp:ListItem>
                                                    <asp:ListItem Value="EMP_FIRST_NAME" >Prepared By</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                                    EnableTheming="False" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged"
                                                    Visible="False" Width="50px">
                                                    <asp:ListItem Selected="True">=</asp:ListItem>
                                                    <asp:ListItem>&lt;</asp:ListItem>
                                                    <asp:ListItem>&gt;</asp:ListItem>
                                                    <asp:ListItem>&lt;=</asp:ListItem>
                                                    <asp:ListItem>&gt;=</asp:ListItem>
                                                    <asp:ListItem>R</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False"></asp:Label>
                                                <asp:TextBox ID="txtSearchValueFromDate" runat="server" type="datepic" EnableTheming="True" Visible="False"
                                                    Width="106px"></asp:TextBox>
                                                <%-- <asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                    <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchFrom" runat="server" Enabled="False"
                                        PopupButtonID="imgFromDate" TargetControlID="txtSearchValueFromDate">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server" Enabled="False"
                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate" UserDateFormat="MonthDayYear"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="">
                                    </cc1:MaskedEditExtender>--%>
                                                <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False"></asp:Label>
                                                <asp:TextBox ID="txtSearchValueToDate" runat="server" type="datepic" EnableTheming="True" Visible="False"
                                                    Width="106px"></asp:TextBox>
                                                <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px"></asp:TextBox>
                                                <%--<asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False"></asp:Image>
                                    <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchValueToDate" runat="server"
                                        Enabled="False" PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" Enabled="False"
                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText" UserDateFormat="MonthDayYear"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="">
                                    </cc1:MaskedEditExtender>--%>
                                                <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                                    CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                                        </tr>
                                    </table>

                                    <asp:Label ID="Label1" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                        Visible="False"></asp:Label><asp:Label ID="Label2" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="Label3" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                        Visible="False"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
        <tr>
            <td><asp:Label ID="lblCp_Ids" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1" Visible="False"></asp:Label>
                <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1" Visible ="false"></asp:Label>
                <asp:Label ID="lblUserName" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblUserId" runat="server" Visible="false"></asp:Label>
            </td>

        </tr>
      </table>
    <br />
    <table>
        <asp:GridView runat ="server" ID="gvCompView" AutoGenerateColumns="False" AllowSorting="True" Width="100%" SelectedRowStyle-BackColor="#c0c0c0" AllowPaging="True" OnPageIndexChanging ="gvCompView_PageIndexChanging">
            <Columns >
                <asp:BoundField DataField ="Ticket_Id" HeaderText ="Id" />
                        <asp:BoundField DataField="Date_Created" HeaderText ="Date Created"/>
                        <asp:BoundField DataField="Date_Closed" HeaderText ="Referred Date"/>
                        <asp:BoundField DataField="Incident_Type" HeaderText="Referred Time"/>
                        <asp:BoundField DataField="User_Affected" HeaderText="Complaint Type"/>
                        <asp:BoundField DataField ="ClientName" HeaderText ="Client Name" />
                        <asp:BoundField DataField="Email" HeaderText ="Contact Person"/>
                        <asp:BoundField DataField="Created_For" HeaderText ="Contact Number"/>
                        <asp:BoundField DataField="Address" HeaderText="Address"/>
                        <asp:BoundField DataField="Summary" HeaderText="Summary"/>
                        <asp:BoundField DataField="Description" HeaderText="Nature of Complaint"/>
                        <asp:BoundField DataField="Comments" HeaderText="Launched By"/>
                        <asp:BoundField DataField="EmailID" HeaderText="EmailID"/>

            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="sdsComplaintRegister" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="[SP_URL_COMPLAINT_VIEW]" SelectCommandType="StoredProcedure">
            <SelectParameters >
                 <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblSearchTypeHidden" DefaultValue="0" Name="SearchType" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblSearchValueFromHidden" DefaultValue="0" Name="SearchValueFrom" PropertyName="Text" Type="String" />
                            <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>
                                <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="EmpId" ControlID="lblEmpIdHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName ="Text" DefaultValue ="0" Name ="CPID" ControlID ="lblCp_Ids" />
            </SelectParameters> 
        </asp:SqlDataSource>
    </table> 
</asp:Content>

