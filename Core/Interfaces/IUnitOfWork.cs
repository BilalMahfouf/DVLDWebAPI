using Core.Interfaces.Repositories.Common;
using Core.Shared;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IReadUpdateRepository<Country> countryRepository { get; }
        IReadUpdateRepository<ApplicationType> applicationTypeRepository { get; }
        IReadUpdateRepository<TestType> testTypeRepository { get; }
        IReadUpdateRepository<LicenseClass> licenseClassRepository { get; }
        IRepository<Person> personRepository { get; }
        IRepository<User> userRepository { get; }
        IRepository<Application> applicationRepository { get; }
        IRepository<Test> testRepository { get; }
        IRepository<DetainedLicense> detainedLicenseRepository { get; }
        IRepository<Driver> driverRepository { get; }
        IRepository <InternationalLicense> internationalLicenseRepository { get; }
        IRepository<License> licenseRepository { get; }
        IRepository<LocalDrivingLicenseApplication> localDrivingLicenseApplicationRepository { get;}
        IRepository<TestAppointment> testAppointmentRepository { get; }
        

        Task<bool> SaveChangesAsync();
    }
}
