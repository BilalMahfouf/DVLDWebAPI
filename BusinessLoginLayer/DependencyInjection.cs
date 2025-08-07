using BusinessLoginLayer.Profiles;
using BusinessLoginLayer.Services;
using BusinessLoginLayer.Services.Applications;
using BusinessLoginLayer.Services.Drivers;
using BusinessLoginLayer.Services.Licenses;
using BusinessLoginLayer.Services.Tests;
using Core.Interfaces.Services.Applications;
using Core.Interfaces.Services.Drivers;
using Core.Interfaces.Services.Licenses;
using Core.Interfaces.Services.People;
using Core.Interfaces.Services.Tests;
using Core.Interfaces.Services.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogicLayerServices
            (this IServiceCollection services)
        {
            // registering the profiles for AutoMapper
            services.AddAutoMapper(
     cfg => {
         cfg.AddProfile<PersonProfile>();
         cfg.AddProfile<UserProfile>();
         cfg.AddProfile<ApplicationProfile>();
         cfg.AddProfile<LocalDrivingLicenseApplicationProfile>();
         cfg.AddProfile<ApplicationTypeProfile>();
         cfg.AddProfile<DriverProfile>();
         cfg.AddProfile<TestTypeProfile>();
         cfg.AddProfile<TestProfile>();
         cfg.AddProfile<TestAppointmentProfile>();
         cfg.AddProfile<CountryProfile>();
         cfg.AddProfile<DetainLicenseProfile>();
         cfg.AddProfile<InternationalLicenseProfile>();
         cfg.AddProfile<LicenseProfile>();
         cfg.AddProfile<TestTypeProfile>();
         cfg.AddProfile<LicenseClassProfile>();
     }
     );
            // Registering services for the business logic layer
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<ILocalDrivingLicenseApplicationService, LocalDrivingLicenseApplicationService>();
            services.AddScoped<IDriverService, DriverService>();
            services.AddScoped<IDetainLicenseService, DetainLicenseService>();
            services.AddScoped<IInternationalLicenseService, InternationalLicenseService>();
            services.AddScoped<ILicenseService, LicenseService>();
            services.AddScoped<ITestAppointmentService, TestAppointmentService>();
            services.AddScoped<ITestService, TestService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<TestTypeService>();
            services.AddScoped<CountryService>();
            services.AddScoped<LicenseClassService>();
            services.AddScoped<ApplicationTypeService>();

            return services;
        }
    }
}
