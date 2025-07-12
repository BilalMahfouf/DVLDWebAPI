using Core.Common;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.License
{
    public class LicenseDTO
    {
        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }

        public int DriverID { get; set; }

        public Enums.LicenseClassTypeEnum LicenseClass { get; set; }

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

        public LicenseDTO(int licenseID, int applicationID, int driverID,
            Enums.LicenseClassTypeEnum licenseClass, DateTime issueDate,
            DateTime expirationDate, string? notes, decimal paidFees, 
            bool isActive, Enums.IssueReason issueReason, int createdByUserID)
        {
            LicenseID = licenseID;
            ApplicationID = applicationID;
            DriverID = driverID;
            LicenseClass = licenseClass;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            Notes = notes;
            PaidFees = paidFees;
            IsActive = isActive;
            IssueReason = issueReason;
            CreatedByUserID = createdByUserID;
        }
        public LicenseDTO() { }
    }
}
