<%@ Page Title="||Value App : Leave Master : Calender ||" Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true" CodeFile="Calender.aspx.cs" Inherits="Modules_HR_Calender" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <table class="pagehead" style="width: 100%">
                <tr>
                    <td colspan="4" style="text-align: left;">Holiday Calender</td>
                </tr>
            </table>
            <div id="HolidayCalender">
                <table align="center" style="text-align: left">
                    <tr>
                        <td>Holiday Description :
                        </td>
                        <td style="width: 5%"></td>
                        <td>
                            <asp:TextBox ID="txtHoliday" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>From Date :
                        </td>
                        <td style="width: 5%"></td>
                        <td>
                            <asp:TextBox ID="txtDateselTbox" runat="server" type="datepic"></asp:TextBox>

                        </td>

                    </tr>
                    <tr>
                        <td>To Date : </td>
                        <td style="width: 5%"></td>
                        <td>
                            <asp:TextBox ID="txtDay" ReadOnly="false" runat="server" type="datepic"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Location :
                        </td>
                        <td style="width: 5%"></td>
                        <td>
                            <asp:DropDownList ID="ddlLocation" runat="server">
                                                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center">
                            <asp:Button ID="btnAddHoliday" runat="server" Text="Add Holiday" OnClick="btnAddHoliday_Click" />
                            <asp:Button ID="btnDelete" runat="server" Text="Delete Holiday" OnClick="btnDelete_Click" Visible="False" />
                            <asp:Label ID="lblHolidayId" Visible="false" runat="server" />
                            <asp:Label ID="lblDay" Visible="False" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div id="HolidayList">
                <asp:GridView ID="gvHolidayList" AutoGenerateColumns="False" runat="server" EmptyDataText="No Records Found" DataSourceID="SqlDataSource1" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="Holiday Id">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnHolidayId" ForeColor="#0066ff" OnClick="lbtnHolidayId_Click" runat="server" Text="<%# Bind('Holiday_Id') %>" CausesValidation="False"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Location Id" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblLocationId" Text='<%# Eval("Location_Id") %>' />

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Holiday Description">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblHolidayDesc" Text='<%# Eval("Holiday_Desc") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="From Date">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDateOfHoliday" Text='<%#Eval("Frm_date")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="To Date">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDayg" Text='<%#Eval("To_date")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Location">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblLocation" Text='<%# Eval("Location") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <SelectedRowStyle BackColor="Silver" />
                </asp:GridView>

                <br />
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SELECT Holiday_Id, Location_Id, Holiday_Desc, CONVERT (varchar, Date_Of_Holiday, 103) AS Frm_date, CONVERT (varchar, Day, 103) AS To_date, Location FROM HolidayCalender_tbl 
where year(Date_Of_Holiday)=year(getdate())   order by Date_Of_Holiday "></asp:SqlDataSource>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


 
