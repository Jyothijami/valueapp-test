using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using YantraBLL.Modules;
using Yantra.MessageBox;

public partial class Modules_HR_Tour_ExpansesDet : basePage
{
    decimal TravelTotal, LocalTotal, LodgingTotal, DATotal = 0;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblTravelAmt.Text = lblLocalAmt.Text = lblLodgeAmt.Text = lblDAAmt.Text = "0";
            HR.EmployeeMaster.EmployeeMaster_Select(ddlEmpName);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy );
            ddlPreparedBy.SelectedValue = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblTotalAmt.Text = (Convert.ToDecimal(lblTravelAmt.Text) + Convert.ToDecimal(lblLocalAmt.Text) + Convert.ToDecimal(lblLodgeAmt.Text) + Convert.ToDecimal(lblDAAmt.Text)).ToString();
            txtTotalTourExap.Text = lblTotalAmt.Text;

            if (Request.QueryString["TourId"] != null)
            {
                try
                {
                    HR.Tour_Expanses objTour = new HR.Tour_Expanses();
                    if (objTour.TourExpanses_Select(Request.QueryString["TourId"].ToString()) > 0)
                    {
                       
                        btnSave.Text = "Update";
                        ddlEmpName.SelectedValue = objTour.EmpId;
                        ddlEmpName_SelectedIndexChanged(sender, e);
                        txtTourNo.Text = objTour.TourNo;
                        txtTourDate.Text = objTour.Tour_Date;
                        txtTravelFromLoc.Text = objTour.FromLocation;
                        txtPlaceOfVisit.Text = objTour.PlaceOfVisit;
                        txtNoOfDays.Text = objTour.NoOfDays;
                        txtDepature.Text = objTour.DeptDate;
                        txtArrival.Text = objTour.ArrivalDate;
                        txtTotalTourExap.Text = objTour.TotalTourExpanses;
                        txtTicktsAmt.Text = objTour.TicketsByComp;
                        txtHtlBills.Text = objTour.HotelBillsByComp;
                        txtAdvance.Text = objTour.Advance;
                        txtBalAmt.Text = objTour.BalanceAmt;
                        ddlPreparedBy.SelectedValue = objTour.PreparedBy;

                        objTour.Tour_TravelExpanses_Select(Request.QueryString["TourId"].ToString(), gvTravelExp);
                        objTour.Tour_LocalConveyance_Select(Request.QueryString["TourId"].ToString(), gvLocalConv );
                        objTour.Tour_LodgingExpanses_Select(Request.QueryString["TourId"].ToString(), gvLodging );
                        objTour.Tour_DailyAllowances_Select(Request.QueryString["TourId"].ToString(), gvDA );

                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                txtTourNo.Text = HR.Tour_Expanses.Expanses_AutoGenCode();
            }
        }
    }
    protected void ddlEmpName_SelectedIndexChanged(object sender, EventArgs e)
    {
        HR.EmployeeMaster obj=new HR.EmployeeMaster ();
        if (obj.EmployeeMaster_Select(ddlEmpName.SelectedItem.Value) > 0)
        {
            txtComp.Text = obj.ComapanyName;
            txtDept.Text = obj.DeptName12 ;
            txtDesg.Text = obj.DesgName12;
        }
    }
    protected void btnAddTravel_Click(object sender, EventArgs e)
    {
        DataTable TravelExpanses = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("TourId");
        TravelExpanses.Columns.Add(col);
        col = new DataColumn("FromLoc");
        TravelExpanses.Columns.Add(col);
        col = new DataColumn("ToLoc");
        TravelExpanses.Columns.Add(col);
        col = new DataColumn("ModeOfTravel");
        TravelExpanses.Columns.Add(col);
        col = new DataColumn("Class");
        TravelExpanses.Columns.Add(col);
        col = new DataColumn("Amount");
        TravelExpanses.Columns.Add(col);
        col = new DataColumn("Remarks");
        TravelExpanses.Columns.Add(col);
        if (gvTravelExp.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvTravelExp.Rows)
            {
                if (gvTravelExp.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvTravelExp.SelectedRow.RowIndex)
                    {
                        DataRow dr = TravelExpanses.NewRow();
                        dr["TourId"] = lblTourId.Text;
                        dr["FromLoc"] = txtTravelFrom .Text;
                        dr["ToLoc"] = txtTravelTo.Text;
                        dr["ModeOfTravel"] = txtTravelMOT .Text;
                        dr["Class"] = txtTravelClass .Text;
                        dr["Amount"] = txtTravelAmt .Text;
                        dr["Remarks"] = txtRemarks .Text;

                        TravelExpanses.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = TravelExpanses.NewRow();
                        dr["TourId"] = gvrow.Cells[1].Text;
                        dr["FromLoc"] = gvrow.Cells[2].Text;
                        dr["ToLoc"] = gvrow.Cells[3].Text;
                        dr["ModeOfTravel"] = gvrow.Cells[4].Text;
                        dr["Class"] = gvrow.Cells[5].Text;
                        dr["Amount"] = gvrow.Cells[6].Text;
                        dr["Remarks"] = gvrow.Cells[7].Text;

                        TravelExpanses.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = TravelExpanses.NewRow();
                    dr["TourId"] = gvrow.Cells[1].Text;
                    dr["FromLoc"] = gvrow.Cells[2].Text;
                    dr["ToLoc"] = gvrow.Cells[3].Text;
                    dr["ModeOfTravel"] = gvrow.Cells[4].Text;
                    dr["Class"] = gvrow.Cells[5].Text;
                    dr["Amount"] = gvrow.Cells[6].Text;
                    dr["Remarks"] = gvrow.Cells[7].Text;

                    TravelExpanses.Rows.Add(dr);
                }
            }
        }

        if (gvTravelExp.SelectedIndex == -1)
        {
            DataRow drnew = TravelExpanses.NewRow();
            drnew["TourId"] = lblTourId.Text;
            drnew["FromLoc"] = txtTravelFrom.Text;
            drnew["ToLoc"] = txtTravelTo.Text;
            drnew["ModeOfTravel"] = txtTravelMOT.Text;
            drnew["Class"] = txtTravelClass.Text;
            drnew["Amount"] = txtTravelAmt.Text;
            drnew["Remarks"] = txtRemarks.Text;

            TravelExpanses.Rows.Add(drnew);
        }
        gvTravelExp.DataSource = TravelExpanses;
        gvTravelExp.DataBind();
        gvTravelExp.SelectedIndex = -1;
        btnRefreshTravel_Click(sender, e);
    }
    protected void btnRefreshTravel_Click(object sender, EventArgs e)
    {
        txtTravelFrom.Text = txtTravelTo.Text = txtTravelMOT.Text = txtTravelClass.Text = txtTravelAmt.Text = "";
    }

    protected void btnLocalAdd_Click(object sender, EventArgs e)
    {
        DataTable TravelExpanses = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("TourId");
        TravelExpanses.Columns.Add(col);
        col = new DataColumn("FromLoc");
        TravelExpanses.Columns.Add(col);
        col = new DataColumn("ToLoc");
        TravelExpanses.Columns.Add(col);
        col = new DataColumn("ModeOfTravel");
        TravelExpanses.Columns.Add(col);
        col = new DataColumn("Kms");
        TravelExpanses.Columns.Add(col);
        col = new DataColumn("Amount");
        TravelExpanses.Columns.Add(col);
        col = new DataColumn("Remarks");
        TravelExpanses.Columns.Add(col);
        if (gvLocalConv.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvLocalConv.Rows)
            {
                if (gvLocalConv.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvLocalConv.SelectedRow.RowIndex)
                    {
                        DataRow dr = TravelExpanses.NewRow();
                        dr["TourId"] = lblTourId.Text;
                        dr["FromLoc"] = txtLocalFrom .Text;
                        dr["ToLoc"] = txtLocalTo.Text;
                        dr["ModeOfTravel"] = txtLocalMOT .Text;
                        dr["Kms"] = txtKMS.Text;
                        dr["Amount"] = txtLocalAmt.Text;
                        dr["Remarks"] = txtLocalRemarks .Text;

                        TravelExpanses.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = TravelExpanses.NewRow();
                        dr["TourId"] = gvrow.Cells[1].Text;
                        dr["FromLoc"] = gvrow.Cells[2].Text;
                        dr["ToLoc"] = gvrow.Cells[3].Text;
                        dr["ModeOfTravel"] = gvrow.Cells[4].Text;
                        dr["Kms"] = gvrow.Cells[5].Text;
                        dr["Amount"] = gvrow.Cells[6].Text;
                        dr["Remarks"] = gvrow.Cells[7].Text;

                        TravelExpanses.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = TravelExpanses.NewRow();
                    dr["TourId"] = gvrow.Cells[1].Text;
                    dr["FromLoc"] = gvrow.Cells[2].Text;
                    dr["ToLoc"] = gvrow.Cells[3].Text;
                    dr["ModeOfTravel"] = gvrow.Cells[4].Text;
                    dr["Kms"] = gvrow.Cells[5].Text;
                    dr["Amount"] = gvrow.Cells[6].Text;
                    dr["Remarks"] = gvrow.Cells[7].Text;

                    TravelExpanses.Rows.Add(dr);
                }
            }
        }

        if (gvLocalConv.SelectedIndex == -1)
        {
            DataRow drnew = TravelExpanses.NewRow();
            drnew["TourId"] = lblTourId.Text;
            drnew["FromLoc"] = txtLocalFrom.Text;
            drnew["ToLoc"] = txtLocalTo.Text;
            drnew["ModeOfTravel"] = txtLocalMOT.Text;
            drnew["Kms"] = txtKMS.Text;
            drnew["Amount"] = txtLocalAmt.Text;
            drnew["Remarks"] = txtLocalRemarks.Text;

            TravelExpanses.Rows.Add(drnew);
        }
        gvLocalConv.DataSource = TravelExpanses;
        gvLocalConv.DataBind();
        gvLocalConv.SelectedIndex = -1;
        btnLocalRefresh_Click(sender, e);
    }
    protected void btnLocalRefresh_Click(object sender, EventArgs e)
    {
        txtLocalFrom.Text = txtLocalTo.Text = txtLocalMOT.Text = txtKMS.Text = txtLocalAmt.Text =txtLocalRemarks .Text="";
    }

    protected void btnLodgeAdd_Click(object sender, EventArgs e)
    {
        DataTable TravelExpanses = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("TourId");
        TravelExpanses.Columns.Add(col);
        col = new DataColumn("HotelName");
        TravelExpanses.Columns.Add(col);
        col = new DataColumn("HotelAddress");
        TravelExpanses.Columns.Add(col);
        col = new DataColumn("DayTrafiee");
        TravelExpanses.Columns.Add(col);
        col = new DataColumn("EligibleTrafiee");
        TravelExpanses.Columns.Add(col);
        col = new DataColumn("NoOfDays");
        TravelExpanses.Columns.Add(col);
        col = new DataColumn("Amount");
        TravelExpanses.Columns.Add(col);
        col = new DataColumn("Remarks");
        TravelExpanses.Columns.Add(col);
        if (gvLodging.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvLodging.Rows)
            {
                if (gvLodging.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvLodging.SelectedRow.RowIndex)
                    {
                        DataRow dr = TravelExpanses.NewRow();
                        dr["TourId"] = lblTourId.Text;
                        dr["HotelName"] = txtHtlName .Text;
                        dr["HotelAddress"] = txtHtlAddress .Text;
                        dr["DayTrafiee"] = txtTrafieeday .Text;
                        dr["EligibleTrafiee"] = txtTrafieeElegi .Text;
                        dr["NoOfDays"] = txtNoOfNights .Text;
                        dr["Amount"] = txtLodgingAmt.Text;
                        dr["Remarks"] = txtLodgeRemarks .Text;

                        TravelExpanses.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = TravelExpanses.NewRow();
                        dr["TourId"] = gvrow.Cells[1].Text;
                        dr["HotelName"] = gvrow.Cells[2].Text;
                        dr["HotelAddress"] = gvrow.Cells[3].Text;
                        dr["DayTrafiee"] = gvrow.Cells[4].Text;
                        dr["EligibleTrafiee"] = gvrow.Cells[5].Text;
                        dr["NoOfDays"] = gvrow.Cells[6].Text;
                        dr["Amount"] = gvrow.Cells[7].Text;
                        dr["Remarks"] = gvrow.Cells[8].Text;

                        TravelExpanses.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = TravelExpanses.NewRow();
                    dr["TourId"] = gvrow.Cells[1].Text;
                    dr["HotelName"] = gvrow.Cells[2].Text;
                    dr["HotelAddress"] = gvrow.Cells[3].Text;
                    dr["DayTrafiee"] = gvrow.Cells[4].Text;
                    dr["EligibleTrafiee"] = gvrow.Cells[5].Text;
                    dr["NoOfDays"] = gvrow.Cells[6].Text;
                    dr["Amount"] = gvrow.Cells[7].Text;
                    dr["Remarks"] = gvrow.Cells[8].Text;

                    TravelExpanses.Rows.Add(dr);
                }
            }
        }

        if (gvLodging.SelectedIndex == -1)
        {
            DataRow drnew = TravelExpanses.NewRow();
            drnew["TourId"] = lblTourId.Text;
            drnew["HotelName"] = txtHtlName.Text;
            drnew["HotelAddress"] = txtHtlAddress.Text;
            drnew["DayTrafiee"] = txtTrafieeday.Text;
            drnew["EligibleTrafiee"] = txtTrafieeElegi.Text;
            drnew["NoOfDays"] = txtNoOfNights.Text;
            drnew["Amount"] = txtLodgingAmt.Text;
            drnew["Remarks"] = txtLodgeRemarks.Text;

            TravelExpanses.Rows.Add(drnew);
        }
        gvLodging.DataSource = TravelExpanses;
        gvLodging.DataBind();
        gvLodging.SelectedIndex = -1;
        btnLodgeRefresh_Click(sender, e);
    }
    protected void btnLodgeRefresh_Click(object sender, EventArgs e)
    {
        txtHtlName.Text = txtHtlAddress.Text = txtTrafieeday.Text = txtTrafieeElegi.Text = txtLodgingAmt.Text=txtNoOfNights.Text = txtLodgeRemarks.Text = "";
    }

    protected void btnDAAdd_Click(object sender, EventArgs e)
    {
        DataTable TravelExpanses = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("TourId");
        TravelExpanses.Columns.Add(col);
        col = new DataColumn("FromDate");
        TravelExpanses.Columns.Add(col);
        col = new DataColumn("ToDate");
        TravelExpanses.Columns.Add(col);
        col = new DataColumn("TotalHrs");
        TravelExpanses.Columns.Add(col);
        col = new DataColumn("DA");
        TravelExpanses.Columns.Add(col);
        col = new DataColumn("Incidental");
        TravelExpanses.Columns.Add(col);
        col = new DataColumn("Amount");
        TravelExpanses.Columns.Add(col);
        col = new DataColumn("Remarks");
        TravelExpanses.Columns.Add(col);
        if (gvDA.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvDA.Rows)
            {
                if (gvDA.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvDA.SelectedRow.RowIndex)
                    {
                        DataRow dr = TravelExpanses.NewRow();
                        dr["TourId"] = lblTourId.Text;
                        dr["FromDate"] = txtFrmDt.Text;
                        dr["ToDate"] = txtToDt.Text;
                        dr["TotalHrs"] = txtTime .Text;
                        dr["DA"] = txtDA .Text;
                        dr["Incidental"] = txtInci.Text;
                        dr["Amount"] = txtDAAmt.Text;
                        dr["Remarks"] = txtDARemarks .Text;

                        TravelExpanses.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = TravelExpanses.NewRow();
                        dr["TourId"] = gvrow.Cells[1].Text;
                        dr["FromDate"] = gvrow.Cells[2].Text;
                        dr["ToDate"] = gvrow.Cells[3].Text;
                        dr["TotalHrs"] = gvrow.Cells[4].Text;
                        dr["DA"] = gvrow.Cells[5].Text;
                        dr["Incidental"] = gvrow.Cells[6].Text;
                        dr["Amount"] = gvrow.Cells[7].Text;
                        dr["Remarks"] = gvrow.Cells[8].Text;
                        TravelExpanses.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = TravelExpanses.NewRow();
                    dr["TourId"] = gvrow.Cells[1].Text;
                    dr["FromDate"] = gvrow.Cells[2].Text;
                    dr["ToDate"] = gvrow.Cells[3].Text;
                    dr["TotalHrs"] = gvrow.Cells[4].Text;
                    dr["DA"] = gvrow.Cells[5].Text;
                    dr["Incidental"] = gvrow.Cells[6].Text;
                    dr["Amount"] = gvrow.Cells[7].Text;
                    dr["Remarks"] = gvrow.Cells[8].Text;

                    TravelExpanses.Rows.Add(dr);
                }
            }
        }

        if (gvDA.SelectedIndex == -1)
        {
            DataRow drnew = TravelExpanses.NewRow();
            drnew["TourId"] = lblTourId.Text;
            drnew["FromDate"] = txtFrmDt.Text;
            drnew["ToDate"] = txtToDt.Text;
            drnew["TotalHrs"] = txtTime.Text;
            drnew["DA"] = txtDA.Text;
            drnew["Incidental"] = txtInci.Text;
            drnew["Amount"] = txtDAAmt.Text;
            drnew["Remarks"] = txtDARemarks.Text;

            TravelExpanses.Rows.Add(drnew);
        }
        gvDA.DataSource = TravelExpanses;
        gvDA.DataBind();
        gvDA.SelectedIndex = -1;
        btnDARefresh_Click(sender, e);
    }
    protected void btnDARefresh_Click(object sender, EventArgs e)
    {
        txtFrmDt.Text = txtToDt.Text = txtTime.Text = txtDA.Text = txtInci.Text = txtDAAmt.Text=txtDARemarks.Text= "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            HR.Tour_Expanses obj = new HR.Tour_Expanses();
            obj.Tour_Date = Yantra.Classes.General.toMMDDYYYY(txtTourDate.Text);
            obj.TourNo = txtTourNo.Text;
            obj.EmpId = ddlEmpName.SelectedItem.Value;
            obj.PlaceOfVisit = txtPlaceOfVisit.Text;
            obj.FromLocation = txtTravelFromLoc.Text;
            obj.NoOfDays = txtNoOfDays.Text;
            obj.DeptDate = Yantra.Classes.General.toMMDDYYYY(txtDepature.Text);
            obj.ArrivalDate = Yantra.Classes.General.toMMDDYYYY(txtArrival.Text);
            obj.TotalTourExpanses = txtTotalTourExap.Text ;
            obj.TotalTravel = lblTravelAmt.Text;
            obj.TotalLocalConveyance = lblLocalAmt.Text;
            obj.TotalLodging = lblLodgeAmt.Text;
            obj.TotalFooding = lblDAAmt.Text;
            obj.TicketsByComp = txtTicktsAmt.Text ;
            obj.HotelBillsByComp = txtHtlBills.Text;
            obj.Advance = txtAdvance.Text;
            obj.BalanceAmt = txtBalAmt.Text;
            obj.PreparedBy = ddlPreparedBy.SelectedItem.Value;
            if (btnSave.Text == "Save")
            {
                if (obj.TourExapnses_Save () == "Data Saved Successfully")
                {
                    obj.Tour_TravelExapnses_Delete (obj.TourId );
                    #region TravelDet Save
                    foreach (GridViewRow gvrow in gvTravelExp.Rows)
                    {

                        //obj.TourId  = gvrow.Cells[1].Text;
                        obj.FromLoc  = gvrow.Cells[2].Text;
                        obj.ToLoc  = gvrow.Cells[3].Text;
                        obj.ModeOfTravel  = gvrow.Cells[4].Text;

                        obj.Class  = gvrow.Cells[5].Text;
                        obj.Amount  = gvrow.Cells[6].Text;
                        obj.Remarks  = gvrow.Cells[7].Text;
                        
                        obj.Tour_TravelExpanses_Save ();
                    }
                    #endregion

                    obj.Tour_LocalConveyance_Delete(obj.TourId);
                    #region LocalConv Save
                    foreach (GridViewRow gvrow in gvLocalConv .Rows)
                    {

                        //obj.TourId = gvrow.Cells[1].Text;
                        obj.FromLoc = gvrow.Cells[2].Text;
                        obj.ToLoc = gvrow.Cells[3].Text;
                        obj.ModeOfTravel = gvrow.Cells[4].Text;

                        obj.Kms  = gvrow.Cells[5].Text;
                        obj.Amount = gvrow.Cells[6].Text;
                        obj.Remarks = gvrow.Cells[7].Text;

                        obj.Tour_LocalConveyance_Save();
                    }
                    #endregion

                    obj.Tour_Lodging_Delete (obj.TourId);
                    #region Lodging Save
                    foreach (GridViewRow gvrow in gvLodging.Rows)
                    {

                        //obj.TourId = gvrow.Cells[1].Text;
                        obj.HotelName  = gvrow.Cells[2].Text;
                        obj.HotelAddress  = gvrow.Cells[3].Text;
                        obj.DayTrafiee  = gvrow.Cells[4].Text;

                        obj.EligibleTrafiee  = gvrow.Cells[5].Text;
                        obj.NoOfDays  = gvrow.Cells[6].Text;
                        obj.Amount = gvrow.Cells[7].Text;
                        obj.Remarks = gvrow.Cells[8].Text;

                        obj.Tour_Lodging_Save ();
                    }
                    #endregion

                    obj.Tour_DA_Delete (obj.TourId);
                    #region DA Save
                    foreach (GridViewRow gvrow in gvDA.Rows)
                    {

                        //obj.TourId = gvrow.Cells[1].Text;
                        obj.FromDate = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[2].Text);
                        obj.ToDate = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[3].Text);
                        obj.TotalHrs  = gvrow.Cells[4].Text;
                        obj.DA  = gvrow.Cells[5].Text;

                        obj.Incidental  = gvrow.Cells[6].Text;
                        obj.Amount = gvrow.Cells[7].Text;
                        obj.Remarks = gvrow.Cells[8].Text;

                        obj.Tour_DA_Save ();
                    }
                    #endregion
                    MessageBox.Show(this, "Data Saved Succesfully");
                }
            }
            else
            {

            }
        }

        catch (Exception ex)
        {

        }
        finally
        {
            HR.ClearControls(this);
            //HR.Dispose();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Data Saved sucessfully');window.location ='Tour_ExpansesMast.aspx';", true);
             
        }
    }
    
    protected void gvTravelExp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TravelTotal = TravelTotal + Convert.ToDecimal(e.Row.Cells[6].Text);
            //txtTotalTourExap.Text = TravelTotal + LocalTotal + LodgingTotal + DATotal.ToString ();
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[5].Text = "Total TravelExp Amt :";
            e.Row.Cells[6].Text = lblTravelAmt.Text = TravelTotal.ToString();
            lblTotalAmt.Text = (Convert .ToDecimal (lblTravelAmt.Text) + Convert.ToDecimal(lblLocalAmt.Text) + Convert.ToDecimal(lblLodgeAmt.Text) + Convert.ToDecimal(lblDAAmt.Text)).ToString();
            txtTotalTourExap.Text = lblTotalAmt.Text;
        }
    }
    protected void gvLocalConv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LocalTotal = LocalTotal + Convert.ToDecimal(e.Row.Cells[6].Text);
            //txtTotalTourExap.Text = TravelTotal + LocalTotal + LodgingTotal + DATotal.ToString();

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[5].Text = "Total LocalExp Amt :";
            e.Row.Cells[6].Text = lblLocalAmt.Text = LocalTotal.ToString();
            lblTotalAmt.Text = (Convert .ToDecimal (lblTravelAmt.Text) + Convert.ToDecimal(lblLocalAmt.Text) + Convert.ToDecimal(lblLodgeAmt.Text) + Convert.ToDecimal(lblDAAmt.Text)).ToString();
            txtTotalTourExap.Text = lblTotalAmt.Text;
        
        }
    }
    protected void gvLodging_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LodgingTotal = LodgingTotal + Convert.ToDecimal(e.Row.Cells[7].Text);
            //txtTotalTourExap.Text = TravelTotal + LocalTotal + LodgingTotal + DATotal.ToString();

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            e.Row.Cells[1].Visible = false;
            e.Row.Cells[6].Text = "Total LocalExp Amt :";
            e.Row.Cells[7].Text = lblLodgeAmt.Text = LodgingTotal.ToString();
            lblTotalAmt.Text = (Convert .ToDecimal (lblTravelAmt.Text) + Convert.ToDecimal(lblLocalAmt.Text) + Convert.ToDecimal(lblLodgeAmt.Text) + Convert.ToDecimal(lblDAAmt.Text)).ToString();
            txtTotalTourExap.Text = lblTotalAmt.Text;
        
        }
    }
    protected void gvDA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DATotal = DATotal + Convert.ToDecimal(e.Row.Cells[7].Text);
            //txtTotalTourExap.Text = TravelTotal + LocalTotal + LodgingTotal + DATotal.ToString();

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[6].Text = "Total DailyAllow Amt :";
            e.Row.Cells[7].Text = lblDAAmt.Text = DATotal.ToString();
            lblTotalAmt.Text = (Convert .ToDecimal (lblTravelAmt.Text) + Convert.ToDecimal(lblLocalAmt.Text) + Convert.ToDecimal(lblLodgeAmt.Text) + Convert.ToDecimal(lblDAAmt.Text)).ToString();
            txtTotalTourExap.Text = lblTotalAmt.Text;
        
        }
    }
}