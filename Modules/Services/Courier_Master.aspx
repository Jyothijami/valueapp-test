<%@ Page Language="C#" MasterPageFile="~/MPs/ServicesMP1.master" AutoEventWireup="true"
    CodeFile="Courier_Master.aspx.cs" Inherits="Modules_Services_Courier_Master" Title="|| Value Appp : Services : Courier Master ||" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <script>
        $(function () {
            $("[name$='txtDate']").datepicker();
        });
    </script>--%>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <table border="0" cellpadding="0" cellspacing="0" class="pagehead" style="width: 100%">
                <tr>
                    <td style="text-align: left">Courier Details</td>
                </tr>
            </table>
            <div id="divServiceCustInfo" align="center">
                <table>
                    <tr style="text-align: right">
                        <td>Courier Type :
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlCourierType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCourierType_SelectedIndexChanged">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                <asp:ListItem Value="1">Inward</asp:ListItem>
                                <asp:ListItem Value="2">Outward</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 5%"></td>
                        <td>
                            <asp:Label ID="lblDate" runat="server" Text="Date :"></asp:Label>
                            &nbsp;</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDate" ReadOnly="false" type="datepic" runat="server"></asp:TextBox>

                        </td>
                    </tr>
                    <tr style="text-align: right">
                        <td>
                            <asp:Label ID="lblFrom" runat="server" Text="From :"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 5%"></td>
                        <td>
                            <asp:Label ID="lblTo" runat="server" Text="To :"></asp:Label>
                            &nbsp;</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtTo" ReadOnly="false" runat="server"></asp:TextBox>

                        </td>
                    </tr>
                    <tr style="text-align: right">
                        <td>Courier Company Name : </td>
                        <td style="text-align: left">
                            <asp:TextBox runat="server" ID="txtCourierCompName" ReadOnly="false" />
                        </td>
                        <td style="width: 5%"></td>
                        <td>Docket Number :
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox runat="server" ID="txtDocNo" ReadOnly="false" />
                        </td>
                    </tr>
                    <tr style="text-align: right">
                        <td>
                            <asp:Label ID="lblReceivedBy" runat="server" Text="Received By :"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtReceivedBy" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 5%">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td style="text-align: left">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Remarks :</td>
                        <td colspan="4" style="text-align: left">
                            <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Style="width: 400px" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="text-align: center">
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" />
                        </td>
                    </tr>

                </table>
            </div>
            <br />

            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="text-align: left" colspan="2">Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                        <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
                    </td>

                    <td style="text-align: right;">
                        <table border="0" cellpadding="0" cellspacing="0" align="right">
                            <tr>
                                <td>
                                    <asp:Label ID="Label14" runat="server" CssClass="label" Visible="false" EnableTheming="False" Font-Bold="True"
                                        Text="Search By"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                        OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged" Visible="False">
                                        <asp:ListItem Value="0">--</asp:ListItem>
                                        <asp:ListItem Value="Courier_From">Courier From</asp:ListItem>
                                        <asp:ListItem Value="Courier_To">Courier To</asp:ListItem>
                                        <asp:ListItem Value="Courier_CompanyName">Company Name</asp:ListItem>
                                        <asp:ListItem Value="Date">Recieved Date</asp:ListItem>

                                    </asp:DropDownList></td>
                                <td>
                                    <asp:DropDownList ID="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                        EnableTheming="False" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged"
                                        Visible="False" Width="50px">
                                        <asp:ListItem Selected="True">=</asp:ListItem>
                                        <asp:ListItem>&lt;</asp:ListItem>
                                        <asp:ListItem>&gt;</asp:ListItem>
                                        <asp:ListItem>&lt;=</asp:ListItem>
                                        <asp:ListItem>&gt;=</asp:ListItem>
                                        <asp:ListItem>R</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td>
                                    <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                    </asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtSearchValueFromDate" runat="server" EnableTheming="True" Visible="False"
                                        Width="106px">
                                    </asp:TextBox>
                                    <asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                        Visible="False" />
                                    <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchFrom" runat="server" Enabled="False" PopupButtonID="imgFromDate"
                                        TargetControlID="txtSearchValueFromDate">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server" DisplayMoney="Left"
                                        Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate"
                                        UserDateFormat="MonthDayYear">
                                    </cc1:MaskedEditExtender>
                                </td>
                                <td>
                                    <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                    </asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px" Visible="False"></asp:TextBox><asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                        Visible="False" />
                                    <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchValueToDate" runat="server" Enabled="False" PopupButtonID="imgToDate"
                                        TargetControlID="txtSearchText">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                        Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                        UserDateFormat="MonthDayYear">
                                    </cc1:MaskedEditExtender>
                                </td>
                                <td>
                                    <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                        CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" Visible="False" /></td>
                            </tr>
                        </table>
                        <%-- <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label>--%>
                        <%-- <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label>
                            <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False">
                            </asp:Label>--%>
                        <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                </tr>
            </table>
            <br />
            <div id="searchDiv" style="width: 100%">
                <table style="width: 100%">
                    <tr>
                        <td colspan="2" style="text-align: right">Type Of Courier :
                    <asp:DropDownList ID="ddlType" runat="server">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        <asp:ListItem Value="1">Inward</asp:ListItem>
                        <asp:ListItem Value="2">Outward</asp:ListItem>
                    </asp:DropDownList>
                        </td>
                        <td style="width: 5%">&nbsp;</td>
                        <td style="text-align: right">Courier From :
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCurFrom" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: right">Courier To :
                    <asp:TextBox ID="txtCurTo" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 5%">&nbsp;</td>
                        <td style="text-align: right">Companny Name :
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCompanyName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: right">From Date :
                    <asp:TextBox ID="txtFromDate" type="datepic" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 5%">&nbsp;</td>
                        <td style="text-align: right">To Date :
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtToDate" type="datepic" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="text-align: center">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divGrid" align="center" style="width: 100%">

                <asp:GridView Width="100%" ID="gvCourierDetails" runat="server" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" OnPageIndexChanging="gvCourierDetails_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkhdr" runat="server" AutoPostBack="True" OnClick="selectAll(this)" />
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="Chk" OnCheckedChanged="Chk_CheckedChanged" AutoPostBack="true"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Id" SortExpression="Sno" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblId" Text='<%#Eval("Sno")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Courier Date" HeaderText="Courier Date" ReadOnly="True" SortExpression="Courier Date" />
                        <asp:BoundField DataField="Type Of Courier" HeaderText="Type Of Courier" SortExpression="Type Of Courier" />
                        <asp:BoundField DataField="Courier From" HeaderText="Courier From" SortExpression="Courier From" />
                        <asp:BoundField DataField="Courier To" HeaderText="Courier To" SortExpression="Courier To" />
                        <asp:BoundField DataField="Courier Company Name" HeaderText="Courier Company Name" SortExpression="Courier Company Name" />
                        <asp:BoundField DataField="Docket No" HeaderText="Docket No" SortExpression="Docket No" />
                        <asp:BoundField DataField="Received By" HeaderText="Received By" SortExpression="Received By" />
                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" />
                    </Columns>
                </asp:GridView>
                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="USP_Courier_search" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchTypeHidden" DefaultValue="0" Name="SearchType" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueFromHidden" DefaultValue="0" Name="SearchValueFrom" PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


 
