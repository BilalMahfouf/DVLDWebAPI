using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Detain
{
    public class DetainLicenseDTO
    {
        public int DetainID { get; set; }
        public int LicenseID { get; set; }

        public DateTime DetainDate { get; set; }

        public decimal FineFees { get; set; }

        public int CreatedByUserID { get; set; }

        public bool IsReleased { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public int? ReleasedByUserID { get; set; }

        public int? ReleaseApplicationID { get; set; }

        public DetainLicenseDTO(int detainID, int licenseID, DateTime detainDate,
            decimal fineFees, int createdByUserID, bool isReleased, 
            DateTime? releaseDate, int? releasedByUserID, int? releaseApplicationID)
        {
            DetainID = detainID;
            LicenseID = licenseID;
            DetainDate = detainDate;
            FineFees = fineFees;
            CreatedByUserID = createdByUserID;
            IsReleased = isReleased;
            ReleaseDate = releaseDate;
            ReleasedByUserID = releasedByUserID;
            ReleaseApplicationID = releaseApplicationID;
        }
        public DetainLicenseDTO() { }
    }
}
