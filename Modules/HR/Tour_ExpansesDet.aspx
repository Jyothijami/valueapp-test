<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true" CodeFile="Tour_ExpansesDet.aspx.cs" Inherits="Modules_HR_Tour_ExpansesDet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table style ="width:100%">
        <tr>
            <td style ="text-align :right ">
                TourClaim No : 
            </td>
            <td style ="text-align :left "><asp:TextBox ID ="txtTourNo" runat ="server" Enabled ="false"  ></asp:TextBox></td>
            <td style ="text-align :right ">TourClaim Date : </td><td style ="text-align :left "> <asp:TextBox ID="txtTourDate" runat ="server" type="datepic" ></asp:TextBox></td>
        </tr>
        <tr>
            <td style ="text-align :right ">Employee Name : </td><td style ="text-align :left "> <asp:DropDownList ID="ddlEmpName" AutoPostBack ="true"  runat ="server" OnSelectedIndexChanged ="ddlEmpName_SelectedIndexChanged"></asp:DropDownList></td>
            <td style ="text-align :right ">Designation : </td><td style ="text-align :left "> <asp:TextBox ID="txtDesg" runat ="server" Enabled ="false" ></asp:TextBox></td>
        </tr>
        <tr>
            <td style ="text-align :right ">Company Name : </td><td style ="text-align :left "> <asp:TextBox ID="txtComp" runat ="server" Enabled ="false" ></asp:TextBox></td>
            <td style ="text-align :right ">Department : </td><td style ="text-align :left "> <asp:TextBox ID="txtDept" runat ="server" Enabled ="false" ></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan ="4" class="profilehead" >Tour Brief</td>
        </tr>
        <tr>
            <td style ="text-align :right ">From Location : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtTravelFromLoc" runat ="server" ></asp:TextBox></td>
            
        </tr>
        <tr>
            <td style ="text-align :right ">Place Of Visit : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtPlaceOfVisit" runat ="server" ></asp:TextBox></td>
            <td style ="text-align :right ">No Of Days Visit : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtNoOfDays" runat ="server" ></asp:TextBox></td>
        </tr>
        <tr>
            <td style ="text-align :right ">Depature : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtDepature" type="datepic" runat ="server" ></asp:TextBox></td>
            <td style ="text-align :right ">Arrival : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtArrival" type="datepic" runat ="server" ></asp:TextBox></td>
        </tr>

    </table>
    <table style ="width:100%">
        <tr >
            <td colspan ="4" class="profilehead" >Travel Expanses</td>
        </tr>
        <tr>
            <td style ="text-align :right ">From : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtTravelFrom" runat ="server" ></asp:TextBox></td>
            <td style ="text-align :right ">To : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtTravelTo" runat ="server" ></asp:TextBox></td>
        </tr>
        <tr>
            <td style ="text-align :right ">Mode Of Travel : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtTravelMOT" runat ="server" ></asp:TextBox></td>
            <td style ="text-align :right  "> Class : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtTravelClass" runat ="server" ></asp:TextBox></td>
        </tr><tr>
            <td style ="text-align :right ">Reamrks : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtRemarks" TextMode ="MultiLine"  runat ="server" ></asp:TextBox></td>
            <td style ="text-align :right ">Amount : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtTravelAmt" runat ="server" ></asp:TextBox></td>
           
        </tr>
        <tr>
            <td colspan="4">&nbsp;</td>
        </tr>
        <tr>
             <td colspan="4" style="text-align: center">
                 <asp:Button ID="btnAddTravel" runat="server" Text="Add" OnClick="btnAddTravel_Click" />
                 <asp:Button ID="btnRefreshTravel" runat="server" Text="Refresh" OnClick="btnRefreshTravel_Click" />
                 <asp:Label ID="lblTravelAmt" runat="server" Text="" Visible="False"></asp:Label>
                 <asp:Label ID="lblTourId" runat="server" Text="" Visible="False"></asp:Label>

             </td>
        </tr>
        <tr>
            <td colspan ="4">
                <asp:GridView ID="gvTravelExp" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDataBound ="gvTravelExp_RowDataBound" ShowFooter="true" >
                                <Columns>
                                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                    <asp:BoundField DataField="TourId" HeaderText="TourId" />
                                    <asp:BoundField DataField="FromLoc" HeaderText="FromLoc" />
                                    <asp:BoundField DataField="ToLoc" HeaderText="ToLoc" />
                                    <asp:BoundField DataField="ModeOfTravel" HeaderText="ModeOfTravel" />
                                    <asp:BoundField DataField="Class" HeaderText="Class" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                </Columns>
                 </asp:GridView>
            </td>
        </tr>

    </table>
    <table style ="width:100%">
        <tr >
            <td colspan ="4" class="profilehead" >Local Conveyance</td>
        </tr>
        <tr>
            <td style ="text-align :right ">From : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtLocalFrom" runat ="server" ></asp:TextBox></td>
            <td style ="text-align :right ">To : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtLocalTo" runat ="server" ></asp:TextBox></td>
        </tr>
        <tr>
            <td style ="text-align :right ">Mode Of Travel : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtLocalMOT" runat ="server" ></asp:TextBox></td>
            <td style ="text-align :right  "> Disyance KMs : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtKMS" runat ="server" ></asp:TextBox></td>
        </tr><tr>
            <td style ="text-align :right ">Reamrks : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtLocalRemarks" TextMode ="MultiLine"  runat ="server" ></asp:TextBox></td>
            <td style ="text-align :right ">Amount : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtLocalAmt" runat ="server" ></asp:TextBox></td>
           
        </tr>
        <tr>
            <td colspan="4">&nbsp;</td>
        </tr>
        <tr>
             <td colspan="4" style="text-align: center">
                 <asp:Button ID="btnLocalAdd" runat="server" Text="Add" OnClick="btnLocalAdd_Click" />
                 <asp:Button ID="btnLocalRefresh" runat="server" Text="Refresh" OnClick="btnLocalRefresh_Click" />
                 <asp:Label ID="lblLocalAmt" runat="server" Text="" Visible="False"></asp:Label>
             </td>
        </tr>
        <tr>
            <td colspan ="4">
                <asp:GridView ID="gvLocalConv" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="true" OnRowDataBound ="gvLocalConv_RowDataBound" >
                                <Columns>
                                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                    <asp:BoundField DataField="TourId" HeaderText="TourId" />
                                    <asp:BoundField DataField="FromLoc" HeaderText="FromLoc" />
                                    <asp:BoundField DataField="ToLoc" HeaderText="ToLoc" />
                                    <asp:BoundField DataField="ModeOfTravel" HeaderText="ModeOfTravel" />
                                    <asp:BoundField DataField="Kms" HeaderText="Kms" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                </Columns>
                 </asp:GridView>
            </td>
        </tr>

    </table>
    <table style ="width:100%">
        <tr >
            <td colspan ="4" class="profilehead" >Lodging Expanses</td>
        </tr>
        <tr>
            <td style ="text-align :right ">Hotel Name : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtHtlName" runat ="server" ></asp:TextBox></td>
            <td style ="text-align :right ">Hotel Address : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtHtlAddress" runat ="server" ></asp:TextBox></td>
        </tr>
        <tr>
            <td style ="text-align :right ">Hotel Stay Trafiee/day : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtTrafieeday" runat ="server" ></asp:TextBox></td>
            <td style ="text-align :right  ">Hotel Stay Trafiee Eligibility : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtTrafieeElegi" runat ="server" ></asp:TextBox></td>
        </tr>
        <tr>
            <td style ="text-align :right ">No Of Nights : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtNoOfNights" runat ="server" ></asp:TextBox></td>
            <td style ="text-align :right ">Amount : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtLodgingAmt" runat ="server" ></asp:TextBox></td>
        </tr>
        <tr>
            <td style ="text-align :right ">Reamrks : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtLodgeRemarks" TextMode ="MultiLine"  runat ="server" ></asp:TextBox></td>
            
           
        </tr>
        <tr>
            <td colspan="4">&nbsp;</td>
        </tr>
        <tr>
             <td colspan="4" style="text-align: center">
                 <asp:Button ID="btnLodgeAdd" runat="server" Text="Add" OnClick="btnLodgeAdd_Click" />
                 <asp:Button ID="btnLodgeRefresh" runat="server" Text="Refresh" OnClick="btnLodgeRefresh_Click" />
                 <asp:Label ID="lblLodgeAmt" runat="server" Text="" Visible="False"></asp:Label>
             </td>
        </tr>
        <tr>
            <td colspan ="4">
                <asp:GridView ID="gvLodging" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="true" OnRowDataBound ="gvLodging_RowDataBound" >
                                <Columns>
                                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                    <asp:BoundField DataField="TourId" HeaderText="TourId" />
                                    <asp:BoundField DataField="HotelName" HeaderText="HotelName" />
                                    <asp:BoundField DataField="HotelAddress" HeaderText="HotelAddress" />
                                    <asp:BoundField DataField="DayTrafiee" HeaderText="DayTrafiee" />
                                    <asp:BoundField DataField="EligibleTrafiee" HeaderText="EligibleTrafiee" />
                                    <asp:BoundField DataField="NoOfDays" HeaderText="NoOfDays" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" />

                                </Columns>
                 </asp:GridView>
            </td>
        </tr>

    </table>
    <table style ="width:100%">
        <tr >
            <td colspan ="4" class="profilehead" >Daily Allowance / Incidental Expanses</td>
        </tr>
        <tr>
            <td style ="text-align :right ">From : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtFrmDt" type="datepic" runat ="server" ></asp:TextBox></td>
            <td style ="text-align :right ">To : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtToDt" type="datepic" runat ="server" ></asp:TextBox></td>
        </tr>
        <tr>
            <td style ="text-align :right ">Time in Hrs : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtTime" runat ="server" ></asp:TextBox></td>
            
        </tr>
        <tr>
            <td style ="text-align :right  "> Daily Allowances : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtDA" runat ="server" ></asp:TextBox></td>
            <td style ="text-align :right  "> Incidental : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtInci" runat ="server" ></asp:TextBox></td>
        </tr>
        <tr>
            <td style ="text-align :right ">Reamrks : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtDARemarks" TextMode ="MultiLine"  runat ="server" ></asp:TextBox></td>
            <td style ="text-align :right ">Amount : </td>
            <td style ="text-align :left "><asp:TextBox ID="txtDAAmt" runat ="server" ></asp:TextBox></td>
           
        </tr>
        <tr>
            <td colspan="4">&nbsp;</td>
        </tr>
        <tr>
             <td colspan="4" style="text-align: center">
                 <asp:Button ID="btnDAAdd" runat="server" Text="Add" OnClick="btnDAAdd_Click" />
                 <asp:Button ID="btnDARefresh" runat="server" Text="Refresh" OnClick="btnDARefresh_Click" />
                 <asp:Label ID="lblDAAmt" runat="server" Text="" Visible="False"></asp:Label>
             </td>
        </tr>
        <tr>
            <td colspan ="4">
                <asp:GridView ID="gvDA" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="true" OnRowDataBound ="gvDA_RowDataBound" >
                                <Columns>
                                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                    <asp:BoundField DataField="TourId" HeaderText="TourId" />
                                    <asp:BoundField DataField="FromDate" HeaderText="FromDate" />
                                    <asp:BoundField DataField="ToDate" HeaderText="ToDate" />
                                    <asp:BoundField DataField="TotalHrs" HeaderText="TotalHrs" />
                                    <asp:BoundField DataField="DA" HeaderText="DA" />
                                    <asp:BoundField DataField="Incidental" HeaderText="Incidental" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                </Columns>
                 </asp:GridView>
            </td>
        </tr>

    </table>

    <br />
    <table style ="width :100%">
        <tr>
            <td colspan ="3" style ="text-align :right ">Total Tour Expanses Incurred :</td>
            <td style ="text-align :left "><asp:TextBox ID="txtTotalTourExap" runat ="server" Enabled ="false"></asp:TextBox>
                <asp:Label ID="lblTotalAmt" runat ="server" Visible ="false" ></asp:Label>
            </td>
            
        </tr>
        <tr>
            <td colspan ="3" style ="text-align :right ">Less Tickets arranged by the Company :</td>
            <td style ="text-align :left "><asp:TextBox ID="txtTicktsAmt" runat ="server" ></asp:TextBox></td>
            </tr><tr>
            <td colspan ="3" style ="text-align :right ">Less Hotel bills paid by the Company :</td>
            <td style ="text-align :left "><asp:TextBox ID="txtHtlBills" runat ="server" ></asp:TextBox></td>
     
        </tr>
        <tr>
            <td colspan ="3" style ="text-align :right ">Less Advance Taken :</td>
            <td style ="text-align :left "><asp:TextBox ID="txtAdvance" runat ="server" ></asp:TextBox></td>
            </tr><tr>
            <td colspan ="3" style ="text-align :right ">Balance AMount :</td>
            <td style ="text-align :left "><asp:TextBox ID="txtBalAmt" runat ="server" ></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan ="3" style ="text-align :right ">Prepared By:</td>
            <td style ="text-align :left " colspan ="3">
                <asp:DropDownList ID="ddlPreparedBy" runat ="server" Enabled ="false" ></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan ="4">
                <asp:Button ID="btnSave" runat ="server" Text ="Save" OnClick ="btnSave_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

