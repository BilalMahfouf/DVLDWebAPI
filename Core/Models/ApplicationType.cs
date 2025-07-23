using Core.Interfaces;
using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccessLayer;

public partial class ApplicationType: IEntity
{
    public int ApplicationTypeID { get; set; }

    public int ID
    {
        get => ApplicationTypeID;
        set => ApplicationTypeID = value;
    }

    public string ApplicationTypeTitle { get; set; } = null!;

    public decimal ApplicationFees { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
}
