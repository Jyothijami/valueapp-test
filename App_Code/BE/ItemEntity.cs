using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ItemEntity
/// </summary>
public class ItemEntity
{
    public string ProductCode { get; set; }
    public string ItemName { get; set; }
    public string Brand { get; set; }
    public string Color { get; set; }
    public int OpeningStock { get; set; }
    public int ClosingStock { get; set; }
    public int StockReceived { get; set; }
    public int StockDespatched { get; set; }
    public int DefectiveStock { get; set; }
    public int BlockedStock { get; set; }
    public int StockAvailable { get; set; }
    public string StockLocation { get; set; }
    public string Remarks { get; set; }
    public string Category { get; set; }
    public string AddedDate { get; set; }
    public string MRN { get; set; }

    //for searching items by date range
    public string FromDate { get; set; }
    public string ToDate { get; set; }
    
}