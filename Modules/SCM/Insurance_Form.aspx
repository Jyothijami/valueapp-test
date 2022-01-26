<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/PurchaseMP1.master" AutoEventWireup="true" CodeFile="Insurance_Form.aspx.cs" Inherits="Modules_SCM_Insurance_Form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            //Iterate through each Textbox and add keyup event handler
            $(".clsTxtToCalculate").each(function () {
                $(this).keyup(function () {
                    //Initialize total to 0
                    var money, mul_Fact, value, duty;
                    var total = 0;
                    money = document.getElementById('<%=txtMoney.ClientID %>').value;
                    mul_Fact = document.getElementById('<%=txtMulFactor.ClientID %>').value;
                    value_sum = document.getElementById('<%=txtValueOfCons.ClientID %>').value;
                    duty = document.getElementById('<%=txtCustDuty.ClientID %>').value;
            
                    $(".clsTxtToCalculate").each(function () {
                        // Sum only if the text entered is number and greater than 0
                        //if (!isNaN(this.value) && this.value.length != 0) {
                        //    total += parseFloat(this.value);
                        //}
                        if (money == "" || money == "0" || isNaN(money)) { money = "0"; }
                        if (mul_Fact == "" || mul_Fact == "0" || isNaN(mul_Fact)) { mul_Fact = "1"; }
                        else if (money > 0 && mul_Fact > 0) {
                            document.getElementById('<%=txtValueOfCons.ClientID %>').value = (money * mul_Fact);

                            if (value_sum == "" || value_sum == "0" || isNaN(value_sum)) { value_sum = "0"; }
                            if (duty == "" || duty == "0" || isNaN(duty)) { duty = "0"; }

                            else if (value_sum > 0 && duty > 0) {
                                document.getElementById('<%=txtTotal.ClientID %>').value = (parseFloat(value_sum) + parseFloat((value_sum * duty) / 100));

                            }
                        }
                    });
                    //Assign the total to label
                    //.toFixed() method will roundoff the final sum to 2 decimal places
                    // $('#<%=txtTotal.ClientID %>').val(total.toFixed(2));
                });
            });
        });
    </script>
    <style type="text/css">
        .auto-style1 {
            height: 52px;
        }
        .auto-style2 {
            width: 5%;
            height: 52px;
        }
        .auto-style3 {
            height: 52px;
            width: 229px;
        }
        .auto-style4 {
            width: 229px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table style="width: 100%">
        <tr class="pagehead">
            <td style="text-align: left">Insurance Form
            </td>
        </tr>
    </table>
    <br />
    <div id="body" style="width: 100%">
        <table>
            <tr>
                <td style="text-align: right" class="auto-style1">Insurance Company : 
                </td>
                <td style="text-align: left" class="auto-style3">
                    <asp:DropDownList ID="ddlInsurance" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInsurance_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvOriginatedBy" runat="server" ControlToValidate="ddlInsurance" ErrorMessage="Please Select the Insurance Company Name" InitialValue="0" ValidationGroup="ip">*</asp:RequiredFieldValidator>
                </td>
                <td class="auto-style2">&nbsp;
                </td>
                <td class="auto-style1">Insurance Company Address :
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtAddress" Width="400px" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="5">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">Open Cover No : 
                </td>
                <td style="text-align: left" class="auto-style4">
                    <asp:TextBox ID="txtOpenCoverNo" runat="server"></asp:TextBox>
                </td>
                <td style="width: 5%">&nbsp;
                </td>
                <td>Importing Item Details :
                </td>
                <td>
                    <asp:TextBox ID="txtImporting" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="5">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">From/Supplier : 
                </td>
                <td style="text-align: left" class="auto-style4">
                    <asp:TextBox ID="txtSupplier" runat="server"></asp:TextBox>
                </td>
                <td style="width: 5%">&nbsp;
                </td>
                <td>To/CompanyLocation :
                </td>
                <td>
                    <asp:TextBox ID="txtLocation" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="5">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">Mode Of Dispatch : 
                </td>
                <td style="text-align: left" class="auto-style4">
                    <asp:TextBox ID="txtMode" runat="server"></asp:TextBox>
                </td>
                <td style="width: 5%">&nbsp;
                </td>
                <td>via(Location) : 
                </td>
                <td>
                    <asp:TextBox ID="txtVia" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="5">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">Money In : 
                </td>
                <td style="text-align: left" class="auto-style4">
                    <asp:DropDownList ID="ddlMoney" Width="75px" runat="server"></asp:DropDownList>
                    <asp:TextBox ID="txtMoney" Width="125px" CssClass="clsTxtToCalculate" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMoney" ErrorMessage="Please enter money" ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
                <td style="width: 5%">&nbsp;
                </td>
                <td>Multiply Factor :
                </td>
                <td>
                    <asp:TextBox ID="txtMulFactor" runat="server"  CssClass="clsTxtToCalculate" Width="207px" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMulFactor" ErrorMessage="Please Enter Multiplication Factor" ValidationGroup="ip">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="5">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">Value Of Consignment : 
                </td>
                <td style="text-align: left" class="auto-style4">
                    <asp:TextBox ID="txtValueOfCons" runat="server"  CssClass="clsTxtToCalculate"  ></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtValueOfCons" ErrorMessage="Please enter Value of  Consignment" ValidationGroup="ip">*</asp:RequiredFieldValidator> </td>
                <td style="width: 5%">&nbsp;
                </td>
                <td>Customs Duty Amount :
                </td>
                <td>
                    <asp:TextBox ID="txtCustDuty" runat="server"  CssClass="clsTxtToCalculate"  ></asp:TextBox>%
               <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCustDuty" ErrorMessage="Please enter Customs Duty Amount" ValidationGroup="ip">*</asp:RequiredFieldValidator> </td>
            </tr>
            <tr>
                <td colspan="5">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">Total Amount : 
                </td>
                <td style="text-align: left" class="auto-style4">
                    <asp:TextBox ID="txtTotal" runat="server"></asp:TextBox>
                </td>
                <td style="width: 5%">&nbsp;
                </td>
                <td>
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td colspan="5">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">Name Of the Consignor : 
                </td>
                <td style="text-align: left" colspan="4">
                    <asp:TextBox ID="txtConsignor" TextMode="MultiLine" Height="25px" Width="655px" runat="server"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td colspan="5">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">Name Of the Consignee : 
                </td>
                <td style="text-align: left" colspan="4">
                    <asp:TextBox ID="txtConsignee" TextMode="MultiLine" Height="25px" Width="655px" runat="server"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td colspan="5">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">Invoice No : 
                </td>
                <td style="text-align: left" class="auto-style4">
                    <asp:TextBox ID="txtInvNo" runat="server"></asp:TextBox>
                </td>
                <td style="width: 5%">&nbsp;
                </td>
                <td style="text-align:right">Invoice Dated On :
                </td>
                <td>
                    <asp:TextBox ID="txtInvDatedOn" type="datepic" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="5">&nbsp;</td>
            </tr>
             <tr>
                <td style="text-align: right">Bill of Loading No : 
                </td>
                <td style="text-align: left" class="auto-style4">
                    <asp:TextBox ID="txtBillLoadNo" runat="server"></asp:TextBox>
                </td>
                <td style="width: 5%">&nbsp;
                </td>
                <td style="text-align:right">Bill Dated On :
                </td>
                <td>
                    <asp:TextBox ID="txtBillDatedOn" type="datepic" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="5">&nbsp;</td>
            </tr>
             <tr>
                <td style="text-align: right">Vessel and Voyage No :
                </td>
                <td style="text-align: left" class="auto-style4">
                    <asp:TextBox ID="txtVesselNo" runat="server"></asp:TextBox>
                </td>
                <td style="width: 5%">&nbsp;
                </td>
                <td colspan="2">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="ip" />
                    <asp:Button ID="btnSubmit" runat="server" Text="Generate Form" OnClick="btnSubmit_Click" />     
                </td>
            </tr>
            <tr>

                <td colspan="3">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="ip" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>
                </td>
            </tr>
            <tr>
                <td colspan="5">&nbsp;</td>
            </tr>
        </table>
        <br />
        <table style="width:100%">
            <tr>
                <td>
                    <asp:GridView ID="gvInsurance" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" Width="100%" DataSourceID="SqlDataSource1">
                        <Columns>
                            <asp:TemplateField SortExpression="Ins_Id" HeaderText="Insurance Id">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnInsId" OnClick="lbtnInsId_Click" ForeColor="#0066ff" runat="server" Text='<%# Eval("Ins_Id") %>' CausesValidation="False" __designer:wfdid="w2"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                            <asp:BoundField DataField="Ins_Comp_Name" HeaderText="Insurance Company">
                            <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Open_Cover_No" HeaderText="Open Cover No">
                            <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="From_Supplier" HeaderText="Supplier">
                            <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="To_CompanyLocation" HeaderText="To Company">
                            <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Mode_Of_Dispatch" HeaderText="Mode Of Dispatch">
                            <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Name_Of_Consignor" HeaderText="Name Of Consignor">
                            <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Name_Of_Consignee" HeaderText="Name Of Consignee">
                            <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Value_Of_Consignment" HeaderText="Value Of Consignment">
                            <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Total_Amount" HeaderText="Total Amount">
                            <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Ins_Id" HeaderText="Hidden" />
                        </Columns>
                        <SelectedRowStyle BackColor="#CCCCCC" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [Ins_Id], [Ins_Comp_Name], [Open_Cover_No], [From_Supplier], [To_CompanyLocation], [Mode_Of_Dispatch], [Name_Of_Consignor], [Name_Of_Consignee], [Value_Of_Consignment], [Total_Amount], [Invoice_Dated_On] FROM [Insurance_Form_tbl] order by [Invoice_Dated_On]  desc"></asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>


 
