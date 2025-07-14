using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Detain
{
    public class UpdateDetainedLicenseDTO
    {
        public int DetainID { get; set; }
        public int ReleasedByUserID { get; set; }

        public int ReleaseApplicationID { get; set; }

        
        public UpdateDetainedLicenseDTO()
        {
        }
    }
}
