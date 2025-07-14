using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.License
{
    public class ReadLicenseDTO
    {
        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }

        public int DriverID { get; set; }

        public Enums.LicenseClassTypeEnum LicenseClass { get; set; }
        public LicenseClassDTO LicenseClassNavigation { get; set; } = null!;

        public DateTime IssueDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string? Notes { get; set; }

        public decimal PaidFees { get; set; }

        public bool IsActive { get; set; }

        /// <summary>
        /// 1-FirstTime, 2-Renew, 3-Replacement for Damaged, 4- Replacement for Lost.
        /// </summary>
        public Enums.IssueReason IssueReason { get; set; }

        public int CreatedByUserID { get; set; }

       
        public ReadLicenseDTO() { }
    }
}
