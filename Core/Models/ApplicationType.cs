using Core.Interfaces;
using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccessLayer;

public partial class ApplicationType
{
    public int ApplicationTypeID { get; set; }

    

    public string ApplicationTypeTitle { get; set; } = null!;

    public decimal ApplicationFees { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
}
