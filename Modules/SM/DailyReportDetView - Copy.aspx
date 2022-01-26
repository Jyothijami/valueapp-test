<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.master" AutoEventWireup="true" CodeFile="DailyReportDetView - Copy.aspx.cs" Inherits="Modules_SM_DailyReportDetView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="form-horizontal">
        <div class="panel panel-default">
            <div class="panel-heading">
                    <h6 class="panel-title">Daily Report Details</h6>
            </div>
            <div class="panel-body">
                <div class="form-group">
                            <label class="col-sm-2 control-label text-right">Purpose :</label>
                            <div class="col-sm-4">
                                 <asp:TextBox ID="txtPurpose" TextMode="MultiLine" onkeypress="return check(event)" runat ="server" CssClass ="form-control"></asp:TextBox>

                            </div>
                </div>
                <div class="form-group">
                            <label class="col-sm-2 control-label text-right">Remarks :</label>
                            <div class="col-sm-10">
                                     <asp:TextBox ID="txtRemarks" TextMode="MultiLine" onkeypress="return check(event)" runat ="server" CssClass ="form-control"></asp:TextBox>
                            </div>

                    </div>
                <div class="form-group">
                            <label class="col-sm-2 control-label text-right">Add Comments :</label>
                            <div class="col-sm-10">
                                     <asp:TextBox ID="txtComments" TextMode="MultiLine" onkeypress="return check(event)" runat ="server" CssClass ="form-control"></asp:TextBox>
                            </div>

                    </div>
                <div class="form-actions text-right">
                              <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" style="border-color:red" OnClick ="btnSave_Click" Text="Save" />

                    </div>
            </div>
        </div>
    </div>
</asp:Content>

