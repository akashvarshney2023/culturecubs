

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
                optionsBuilder.UseSqlServer("Server=DESKTOP-2JBM5V2\\SQLEXPRESS;Database=CCubAPI;User Id =admin;Password=admin@123;Trusted_Connection=True;MultipleActiveResultSets=true");
            }           
           
        }
        public DbSet<ContentManagement.Model.ContentManagement> Content { get; set; } = default!;

        public DbSet<ContentManagement.Model.Microsite> Microsite { get; set; } = default!;

        public DbSet<ContentManagement.Model.Page> Page { get; set; } = default!;

    }
}

