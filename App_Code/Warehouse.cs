using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using VlineDAL;
using System.Data;
using System.Data.SqlClient;
using vllib;


/// <summary>
/// Summary description for Warehouse
/// </summary>
public class Warehouse
{
	public Warehouse()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public class Items
    {
        DataCommand dcmd; List<SqlParameter> param;

        public DataTable GetItems(ItemEntity item)
        {
            dcmd = new DataCommand("Usp_Get_WH_Items", CommandType.StoredProcedure);
            param = new List<SqlParameter>();
            param.Add(new SqlParameter("@ProductCode", item.ProductCode));
            param.Add(new SqlParameter("@ItemName", item.ItemName));
            param.Add(new SqlParameter("@Brand", item.Brand));
            param.Add(new SqlParameter("@Color", item.Color));
            param.Add(new SqlParameter("@StockLocation", item.StockLocation));
            param.Add(new SqlParameter("@Category", item.Category));
            param.Add(new SqlParameter("@FromDate", item.FromDate));
            param.Add(new SqlParameter("@ToDate", item.ToDate));
            param.Add(new SqlParameter("@MRN", item.MRN));
            
            dcmd.InputParameters = param;
            return (DataTable)new SqlDataEngine(dcmd).ExecuteDataTable();
        }
        public void EditItem(ItemEntity item)
        { }
        public void DeleteItem(ItemEntity item)
        { }
        public void AddItem(ItemEntity item)
        { }

        #region "Damage Report"
        //for DamageReport page
        public DataTable GetInvoiceItems(int invoiceNo)
        {
            dcmd = new DataCommand("Usp_Invoice_CRUD", CommandType.StoredProcedure);
            param = new List<SqlParameter>();
            param.Add(new SqlParameter("@InvoiceNo", invoiceNo));
            param.Add(new SqlParameter("@Option", 2));

            dcmd.InputParameters = param;
            return (DataTable)new SqlDataEngine(dcmd).ExecuteDataTable();
        }
        public int EditInvoiceItems(DataTable dt, int invoiceNo, string preparedBy)
        {
            dcmd = new DataCommand("Usp_Invoice_CRUD", CommandType.StoredProcedure);
            param = new List<SqlParameter>();
            param.Add(new SqlParameter("@InvoiceNo", invoiceNo));
            param.Add(new SqlParameter("@Option", 3));
            param.Add(new SqlParameter("@PreparedBy", preparedBy));
            param.Add(new SqlParameter("@InvoiceItems", dt));

            dcmd.InputParameters = param;
            return new SqlDataEngine(dcmd).ExecuteDml();
        }
        
        public int InsertDamageReport(DataTable dt, string preparedBy)
        {
            dcmd = new DataCommand("Usp_DamageReport_CRUD", CommandType.StoredProcedure);
            param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Option", 1));
            param.Add(new SqlParameter("@PreparedBy", preparedBy));
            param.Add(new SqlParameter("@DamageReport", dt));

            dcmd.InputParameters = param;
            return new SqlDataEngine(dcmd).ExecuteDml();
        }
        #endregion
        #region "Spares"
        public int AddSpares(DataTable dt,int LocId)
        {
            dcmd = new DataCommand("Usp_Spares_CRUD", CommandType.StoredProcedure);
            param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Spares", dt));
            param.Add(new SqlParameter("@Option", 1));
            param.Add(new SqlParameter("@Location", LocId));
            
            dcmd.InputParameters = param;
            return new SqlDataEngine(dcmd).ExecuteDml();
        }
        #endregion
        public int DispatchItems(DataTable dt,int QuotID,string comments)
        {
            dcmd = new DataCommand("[Usp_Update_Wh_Items]", CommandType.StoredProcedure);
            param = new List<SqlParameter>();
            param.Add(new SqlParameter("@DispatchItems", dt));
            param.Add(new SqlParameter("@QuotID", QuotID));
            param.Add(new SqlParameter("@Comments", comments));
            dcmd.InputParameters = param;
            return new SqlDataEngine(dcmd).ExecuteDml();
        }
    }

    public class Quotations
    {
        DataCommand dcmd; List<SqlParameter> param;
        public DataSet GetQuotations(int QuotID)
        {
            dcmd = new DataCommand("Usp_Quotation_Details", CommandType.StoredProcedure);
            param = new List<SqlParameter>();
            param.Add(new SqlParameter("@QuotID", QuotID));
            dcmd.InputParameters = param;
            return (DataSet)new SqlDataEngine(dcmd).Execute(DataReturnType.DataSet);
        }
        public DataTable GetWarehouseItemsByQuotations(DataTable dt)
        {
            dcmd = new DataCommand("[Usp_Get_Wh_Items_by_Quotation]", CommandType.StoredProcedure);
            param = new List<SqlParameter>();
            param.Add(new SqlParameter("@QuotItems", dt));
            dcmd.InputParameters = param;
            return (DataTable)new SqlDataEngine(dcmd).ExecuteDataTable();
        }
    }
    public class Locations
    {
        DataCommand dcmd; List<SqlParameter> param;
        public int AddLocation(LocationEntity loc)
        {
            dcmd = new DataCommand("Usp_Manage_Locations", CommandType.StoredProcedure);
            param = new List<SqlParameter>();
            param.Add(new SqlParameter("@LocationName", loc.LocationName));
            param.Add(new SqlParameter("@ParentID", loc.ParentId));
            param.Add(new SqlParameter("@Option", 1));
            param.Add(new SqlParameter("@wh_id", loc.Wh_id));
            dcmd.InputParameters = param;
            return new SqlDataEngine(dcmd).ExecuteDml();
        }
        public DataSet GetLocations(LocationEntity loc)
        {
            dcmd = new DataCommand("Usp_Manage_Locations", CommandType.StoredProcedure);
            param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Option", 2));
            param.Add(new SqlParameter("@ParentID", loc.ParentId));

            dcmd.InputParameters = param;
            return (DataSet)new SqlDataEngine(dcmd).Execute(DataReturnType.DataSet);
        }
        public int EditLocation(LocationEntity loc)
        {
            dcmd = new DataCommand("Usp_Manage_Locations", CommandType.StoredProcedure);
            param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Option", 3));
            param.Add(new SqlParameter("@LocID", loc.LocId));
            param.Add(new SqlParameter("@LocationName", loc.LocationName));
            dcmd.InputParameters = param;
            return new SqlDataEngine(dcmd).ExecuteDml();
        }
        public int DeleteLocation(LocationEntity loc,string stockLocations)
        {
            dcmd = new DataCommand("Usp_Manage_Locations", CommandType.StoredProcedure);
            param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Option", 4));
            param.Add(new SqlParameter("@LocId", loc.LocId));
            dcmd.InputParameters = param;
            return new SqlDataEngine(dcmd).ExecuteDml();
        }
    }
    
    public class Category
    {
        DataCommand dcmd; List<SqlParameter> param;        

        /// <summary>
        /// options are useful to choose commands to execute CRUD
        /// 1.Create/Insert.  2.Read/Select.  3.Update.  4.Delete
        /// </summary>
        /// <param name="catID"></param>
        /// <returns></returns>
        public DataTable GetCategories(int catID)
        {
            dcmd = new DataCommand("Usp_Category_CRUD", CommandType.StoredProcedure);
            param = new List<SqlParameter>();
            if (catID != 0)
            {
                param.Add(new SqlParameter("@CatID", catID));                
            }
            param.Add(new SqlParameter("@Option", 2));
            dcmd.InputParameters = param;
            return (DataTable)new SqlDataEngine(dcmd).ExecuteDataTable();
        }
        public int AddCategory(string category)
        {
            dcmd = new DataCommand("Usp_Category_CRUD", CommandType.StoredProcedure);
            param = new List<SqlParameter>();
            if (category != "")
            {
                param.Add(new SqlParameter("@CategoryName", category));
                param.Add(new SqlParameter("@Option", 1));
                dcmd.InputParameters = param;
                return new SqlDataEngine(dcmd).ExecuteDml();
            }
            return 0;
        }
        public int EditCategory(int catID,string catName)
        {
            dcmd = new DataCommand("Usp_Category_CRUD", CommandType.StoredProcedure);
            param = new List<SqlParameter>();
            if (catName.Trim() != "" && catID > 0)
            {
                param.Add(new SqlParameter("@CatID", catID));
                param.Add(new SqlParameter("@CategoryName", catName));
                param.Add(new SqlParameter("@Option", 3));
                dcmd.InputParameters = param;
                return new SqlDataEngine(dcmd).ExecuteDml();
            }
            return 0;
        }
        public int DeleteCategory(int catID)
        {
            dcmd = new DataCommand("Usp_Category_CRUD", CommandType.StoredProcedure);
            param = new List<SqlParameter>();
            if (catID > 0)
            {
                param.Add(new SqlParameter("@CatID", catID));
                param.Add(new SqlParameter("@Option", 4));
                dcmd.InputParameters = param;
                return new SqlDataEngine(dcmd).ExecuteDml();
            }
            return 0;
        }
    }
}