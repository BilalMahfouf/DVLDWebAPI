using Core.Interfaces;
using Core.Interfaces.Repositories.Applications;
using Core.Interfaces.Repositories.Common;
using DataAccessLayer.Data;
using DataAccessLayer.Repositories.Application;
using DataAccessLayer.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static  class DependencyInjection
    {
        public static IServiceCollection AddDataAccessLayerServices(this IServiceCollection services,
            IConfiguration configuration)
        {
           

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DvldDBContext>(options => options.UseSqlServer(connectionString));


            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IReadUpdateRepository<>), typeof(ReadUpdateRepository<>));
            services.AddScoped< ILocalDrivingLicenseApplicationRepository,LocalDrivingLicenseApplicationRepository>();

            //Unit of work pattern
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}

