using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.DTOs.Application
{
    public class LocalDrivingLicenseDTO
    {
        public int LocalDrivingLicenseID { get; set; }
        public int ApplicationID { get; set; }
        public Enums.LicenseClassTypeEnum LicenseClassID { get; set; }

        public LocalDrivingLicenseDTO(int localDrivingLicenseID, int applicationID
            , Enums.LicenseClassTypeEnum licenseClass)
        {
            LocalDrivingLicenseID = localDrivingLicenseID;
            ApplicationID = applicationID;
            LicenseClassID = licenseClass;
        }
        public LocalDrivingLicenseDTO()
        {
            // Default constructor for serialization/deserialization
        }
    }
}
