using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.DTOs.Detain
{
    public class UpdateDetainedLicenseDTO
    {
        public bool IsReleased { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public int? ReleasedByUserID { get; set; }

        public int? ReleaseApplicationID { get; set; }

        public UpdateDetainedLicenseDTO(bool isReleased, DateTime? releaseDate,
            int? releasedByUserID, int? releaseApplicationID)
        {
            IsReleased = isReleased;
            ReleaseDate = releaseDate;
            ReleasedByUserID = releasedByUserID;
            ReleaseApplicationID = releaseApplicationID;
        }
        public UpdateDetainedLicenseDTO()
        {
        }
    }
}
