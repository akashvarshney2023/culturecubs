using Culturecubs.TenantAndUser.DataAccess;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using Culturecubs.TenantAndUser.DataAccess.Repository;
using CultureCubs.TenantAndUser.Functions.Configurations;
using Microsoft.Extensions.Configuration;

[assembly: FunctionsStartup(typeof(CultureCubs.TenantAndUser.Functions.Startup))]

namespace CultureCubs.TenantAndUser.Functions
{
    public class Startup : FunctionsStartup
    {      
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string connectionString = Environment.GetEnvironmentVariable("ConnectionString", EnvironmentVariableTarget.Process);
            builder.Services.AddDbContext<ApplicationDbContext>(x =>
            {
                x.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());

            });
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<ITokenService, TokenService>();
        }
    }  
}
