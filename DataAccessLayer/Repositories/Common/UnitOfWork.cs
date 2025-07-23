using Core.Interfaces;
using Core.Interfaces.Repositories.Common;
using Core.Shared;
using DataAccessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DvldDBContext _context;

        public UnitOfWork(DvldDBContext context)
        {
            _context = context;
        }
        private IReadUpdateRepository<Country>? _countryRepository;
        private IReadUpdateRepository<ApplicationType>? _applicationTypeRepository;
        private IReadUpdateRepository<TestType>? _testTypeRepository;
        private IReadUpdateRepository<LicenseClass>? _licenseClassRepository;
        private IRepository<DataAccessLayer.Person>? _personRepository;
        private IRepository<DataAccessLayer.User>? _userRepository;
        private IRepository<DataAccessLayer.Application>? _applicationRepository;
        private IRepository<Test>? _testRepository;
        private IRepository<DetainedLicense>? _detainedLicenseRepository;
        private IRepository<Driver>? _driverRepository;
        private IRepository<InternationalLicense>? _internationalLicenseRepository;
        private IRepository<DataAccessLayer.License>? _licenseRepository;
        private IRepository<LocalDrivingLicenseApplication>? _localDrivingLicenseApplicationRepository;
        private IRepository<TestAppointment>? _testAppointmentRepository;

        public IReadUpdateRepository<Country> countryRepository 
            => _countryRepository ??= new ReadUpdateRepository<Country>(_context);


        public IReadUpdateRepository<ApplicationType> applicationTypeRepository => 
        _applicationTypeRepository??= new ReadUpdateRepository<ApplicationType>(_context);

        public IReadUpdateRepository<TestType> testTypeRepository 
            =>_testTypeRepository??= new ReadUpdateRepository<TestType>(_context);

        public IReadUpdateRepository<LicenseClass> licenseClassRepository 
            => _licenseClassRepository ??= new ReadUpdateRepository<LicenseClass>(_context);

        public IRepository<DataAccessLayer.Person> personRepository 
            => _personRepository ??= new Repository<DataAccessLayer.Person>(_context);

        public IRepository<DataAccessLayer.User> userRepository 
            => _userRepository ??= new Repository<DataAccessLayer.User>(_context);

        public IRepository<DataAccessLayer.Application> applicationRepository 
            => _applicationRepository ??= new Repository<DataAccessLayer.Application>(_context);

        public IRepository<Test> testRepository
            => _testRepository ??= new Repository<Test>(_context);

        public IRepository<DetainedLicense> detainedLicenseRepository
            => _detainedLicenseRepository ??= new Repository<DetainedLicense>(_context);

        public IRepository<Driver> driverRepository 
            => _driverRepository??= new Repository<Driver>(_context);

        public IRepository<InternationalLicense> internationalLicenseRepository 
            => _internationalLicenseRepository ??= new Repository<InternationalLicense>(_context);

        public IRepository<DataAccessLayer.License> licenseRepository 
            =>  _licenseRepository ??= new Repository<DataAccessLayer.License>(_context);

        public IRepository<LocalDrivingLicenseApplication> 
            localDrivingLicenseApplicationRepository 
            => _localDrivingLicenseApplicationRepository??= new Repository<LocalDrivingLicenseApplication>(_context);

        public IRepository<TestAppointment> testAppointmentRepository
            => _testAppointmentRepository??= new Repository<TestAppointment>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this); // Optional: prevents finalizer from being called
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
