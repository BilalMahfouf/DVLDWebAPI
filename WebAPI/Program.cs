using AutoMapper;
using BusinessLoginLayer.Profiles;
using Core.Interfaces;
using Core.Interfaces.Repositories.Common;
using Core.Interfaces.Services.People;
using DataAccessLayer;
using DataAccessLayer.Data;
using DataAccessLayer.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IReadUpdateRepository<>), typeof(ReadUpdateRepository<>));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPersonService, BusinessLoginLayer.Services.PersonService>();


// Remove this line as it causes CS0310 due to generic constraint issues:
// builder.Services.AddAutoMapper(cfg => cfg.AddProfile<PersonProfile>());
// Use one of the following correct registrations instead:

// Option 1: Register by type (recommended for most scenarios)
builder.Services.AddAutoMapper(
    cfg=> {
        cfg.AddProfile<PersonProfile>();
        cfg.AddProfile<UserProfile>();
        cfg.AddProfile<TestTypeProfile>();
        cfg.AddProfile<LicenseClassProfile>();
    }
    );
// Replace this line:
// builder.Services.AddAutoMapper(typeof(PersonProfile));

builder.Services.AddDbContext<DvldDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
 
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
