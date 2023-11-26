

using ContentManagement.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Diagnostics.Metrics;

namespace ContentManagement.DataContext
{
    public class ContentManagementDataContext : DbContext
    {
        public ContentManagementDataContext(DbContextOptions<ContentManagementDataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
       
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = tcp:sql - cctexibhit - dev - 001.database.windows.net, 1433; Initial Catalog = sqldb - cctexibhit - dev - 001; Persist Security Info = False; User ID = ccadmin; Password = culturecubs@123; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");
            }           
           
        }
        public DbSet<ContentManagement.Model.ContentManagement> Content { get; set; } = default!;

        public DbSet<ContentManagement.Model.Microsite> Microsite { get; set; } = default!;

        public DbSet<ContentManagement.Model.Page> Page { get; set; } = default!;

    }
}

