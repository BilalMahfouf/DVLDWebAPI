using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.Helpers
{
    public static class Constants
    {
        public static async Task<int> LicenseValidityLength(int licenseClassId,IUnitOfWork uow)
        {
            var licenseClass = await uow.licenseClassRepository.FindAsync
                (lc => lc.LicenseClassID == licenseClassId);
            return licenseClass.DefaultValidityLength;
        }
    }
}
