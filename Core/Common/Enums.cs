using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common
{
    public static class Enums
    {
        public enum LicenseClassTypeEnum
        {
            SmallMotorcycle = 1, MotorcycleLicense=2
                , OrdinaryDrivingLicense=3, Commercial=4, Agricultural=5,
            SmallMediumBus=6, TruckedHeavyVehicle=7
        };
        public enum TestTypeEnum
        {
            VisionTest=1, WrittenTest = 2, StreetTest = 3
        };
        public enum ApplicationTypeEnum
        {
            NewLocalDrivingLicense=1, 
            RenewDrivingLicense =2, 
            ReplacementForLostDrivingLicense=3,
            ReplacementForDamagedDrivingLicense=4, 
            ReleaseDetainedDrivingLicense=5,
            NewInternationalLicense=6,
            RetakeTest=7
        };

    }
}
