using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.License
{
    public class InternationalLicenseDTO
    {
        public int ApplicationID { get; set; }

        public int DriverID { get; set; }

        public int IssuedUsingLocalLicenseID { get; set; }

        public int CreatedByUserID { get; set; }

       
        public InternationalLicenseDTO() { }
    }

    public class ReadInternationalLicenseDTO
    {
        public int InternationalLicenseID { get; set; }
        public int ApplicationID { get; set; }

        public int DriverID { get; set; }

        public int IssuedUsingLocalLicenseID { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public bool IsActive { get; set; }

        public int CreatedByUserID { get; set; }

       
        public ReadInternationalLicenseDTO() { }
    }

}
