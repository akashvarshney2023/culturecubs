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
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            builder.ConfigurationBuilder
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
        }
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string connectionString = Environment.GetEnvironmentVariable("ConnectionString", EnvironmentVariableTarget.Process);
            IConfiguration configuration = builder.GetContext().Configuration;
            builder.Services.AddDbContext<ApplicationDbContext>(x =>
            {
                x.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());

            });

            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<ITokenService, TokenService>();

            // Configure JWT settings
            var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();
            builder.Services.AddSingleton(jwtSettings);


        }
    }
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
    }
}
