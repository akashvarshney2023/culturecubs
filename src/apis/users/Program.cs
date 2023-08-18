using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CCubAPI.Data;
using CCubAPI.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var authority = builder.Configuration.GetSection("Authority").Value;

builder.Services.AddDbContext<CCubAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CCubAPIContext") ?? throw new InvalidOperationException("Connection string 'CCubAPIContext' not found.")));
builder.Services.AddIdentityServer()
    .AddInMemoryClients(IdentityServerConfig.Clients)
    .AddInMemoryIdentityResources(IdentityServerConfig.IdentityResources)
    .AddInMemoryApiResources(IdentityServerConfig.ApiResources)
    .AddInMemoryApiScopes(IdentityServerConfig.ApiScopes)
    //.AddTestUsers(IdentityServerConfig.TestUsers)
    .AddDeveloperSigningCredential();
// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = authority;
                options.Audience = "myApi";
                options.RequireHttpsMetadata = false;
                // options.Audience = "myApi";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ////////////////////////////////////////////////////////
                    // The following made the difference.  
                    ////////////////////////////////////////////////////////
                    ValidateAudience = false,
                };
            });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
app.UseRouting();

app.UseIdentityServer();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
