using Core.Interfaces;
using System;
using System.Collections.Generic;

namespace DataAccessLayer;

public partial class Country:IEntity
{
    public int CountryID { get; set; }
    public int ID
    {
        get => CountryID;
        set => CountryID = value;
    }

    public string CountryName { get; set; } = null!;

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
