<%@ Page Title="||Value App : Services : Customer Information ||" Language="C#" MasterPageFile="~/MPs/ServicesMP1.master" AutoEventWireup="true" CodeFile="Site_Inspection_Report_Details.aspx.cs" Inherits="Modules_Services_Site_Inspection_Report_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <h3 style="padding-left: 70px;">Site Inspect/Progress Report</h3>
    <div id="divServiceCustInfo" align="center">
        <table>
            <tr style="text-align: right">
                <td>Client Name :
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtClientName" runat="server" ReadOnly="false"></asp:TextBox>
                </td>
                <td style="width: 5%"></td>
                <td>Date :
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtQuoDate" type="datepic" ReadOnly="false" runat="server"></asp:TextBox>

                </td>
            </tr>
            <tr><td colspan="5">&nbsp;</td></tr>

            <tr style="text-align: right">
                <td>Customer Name:
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtCustomerName" runat="server"></asp:TextBox>
                </td>
                <td style="width: 5%"></td>
                <td>PO Number :
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlPONo" runat="server" AutoPostBack="True" Visible="true" OnSelectedIndexChanged="ddlPONo_SelectedIndexChanged"></asp:DropDownList>

           
                    <asp:TextBox ID="txtPONumber" Visible="true" ReadOnly="false" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr><td colspan="5">&nbsp;</td></tr>
            <tr style="text-align: right">
                <td>Executive Name  :
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtExecutiveName" runat="server"></asp:TextBox>
                </td>
                <td style="width: 5%"></td>
                <td>Technician Name :
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtTechnicianName" ReadOnly="false" runat="server"></asp:TextBox>

                </td>
            </tr>

            <tr style="text-align: right">
                <td>Site Plumber Name  :
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtSitePlumber" runat="server"></asp:TextBox>
                </td>
                <td style="width: 5%"></td>
                <td>Plumber Mobile No :
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtPlumberMobileNo" ReadOnly="false" runat="server"></asp:TextBox>

                </td>
            </tr>

            <tr style="text-align: right">
                <td>Site Incharge Mobile No  :
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtSiteInchargeName" runat="server"></asp:TextBox>
                </td>
                <td style="width: 5%"></td>
                <td>Architecture Name:
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtArchitectName" ReadOnly="false" runat="server"></asp:TextBox>

                </td>
            </tr>

            <tr style="text-align: right">
                <td>Incharge/Supervisor Name  :
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtProjectManager" runat="server"></asp:TextBox>
                </td>
                <td style="width: 5%"></td>
                <td>Project Manager Mobile:
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtProManagerNo" ReadOnly="false" runat="server"></asp:TextBox>

                </td>
            </tr>

            <tr>
                <td style="text-align: right">Site Address :
                </td>
                <td colspan="4" style="text-align: left">
                    <asp:TextBox runat="server" ID="txtSiteAddress" TextMode="MultiLine" Style="width: 500px" />
                </td>
            </tr>

        </table>
    </div>
    <h3 style="padding-left: 70px;">Site Visiting Details</h3>
    <div id="divVisitDet" align="center">
        <table>

            <tr style="text-align: right">
                <td>Date Of Visit :
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtDate" type="datepic" runat="server" ReadOnly="false"></asp:TextBox>
                </td>
                <td style="width: 5%"></td>
                <td>Visiting ID :
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtVisitingId" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>

            <tr style="text-align: right">
                <td>Attended By :
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtAttendedBy" runat="server"></asp:TextBox>
                </td>
                <td style="width: 5%"></td>
                <td>Site Status :
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtPosition" ReadOnly="false" runat="server"></asp:TextBox>

                </td>
            </tr>

            <%--<tr style="text-align: right">
                <td>Site Incharge Name :
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtSiteIncharge" runat="server"></asp:TextBox>
                </td>
                <td style="width: 5%"></td>
                <td>Visited Person :
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtVisitedPerson" ReadOnly="false" runat="server"></asp:TextBox>

                </td>
            </tr>--%>

            <tr>
                <td style="text-align: right">Visit Report :
                </td>
                <td colspan="4" style="text-align: left">
                    <asp:TextBox runat="server" ID="txtVisitReport" TextMode="MultiLine" Style="width: 500px" />
                </td>
            </tr>

            <tr style="text-align: center">
                <td colspan="5" style="text-align: center">
                    <asp:Button ID="btnAddVisits" runat="server" Text="Add" OnClick="btnAddVisits_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Refresh" OnClick="btnCancel_Click" />

                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="false" OnClick="btnDelete_Click" />

                </td>
            </tr>

        </table>

        <br />
        <asp:GridView ID="gvVisitDetails" EmptyDataText="No Records To Display" runat="server" AutoGenerateColumns="False" Width="100%">
            <Columns>

                <asp:TemplateField HeaderText="Inspection Id" Visible="true" SortExpression="Attended_By">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="lbtnVisitId" ForeColor="#3366ff" Text='<%# Bind("Inspection_Id") %>' CausesValidation="False" OnClick="lbtnVisitId_Click"></asp:LinkButton>

                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Attended By" Visible="true" SortExpression="Attended_By">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="lbtnAttendedBy" Text='<%# Bind("Attended_By") %>' CausesValidation="False"></asp:LinkButton>

                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="Date_Of_Inspection" HeaderText="Date Of Inspection"></asp:BoundField>
                <asp:BoundField DataField="Position" HeaderText="Site Status"></asp:BoundField>
                <asp:BoundField DataField="Visit_Report" HeaderText="Visit Report" />
            </Columns>
        </asp:GridView>

        <br />
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" />
        <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Print" />

    </div>
    <div style="width: 100%">
        <asp:Label ID="lblClientId" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="tempInspectionId" runat="server" Visible="false"></asp:Label>

    </div>

</asp:Content>



