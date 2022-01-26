using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LocationEntity
/// </summary>
public class LocationEntity
{
    public int LocId { get; set; }
    public int ParentId { get; set; }
    public string LocationName { get; set; }

    public string Wh_id { get; set; }
}