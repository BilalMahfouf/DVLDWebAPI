using System;
using System.Collections.Generic;

namespace DataAccessLayer;

public partial class Person
{
    public int PersonID { get; set; }

    public string NationalNo { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string SecondName { get; set; } = null!;

    public string? ThirdName { get; set; }

    public string LastName { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// 0 Male , 1 Femail
    /// </summary>
    public byte Gender { get; set; }

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Email { get; set; }

    public int NationalityCountryID { get; set; }

    public string? ImagePath { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();

    public virtual Country NationalityCountry { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
