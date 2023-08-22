using Culturecubs.Microsite.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Culturecubs.Microsite.DataAccess.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<TenantContest> TenantContests { get; set; } = null!;
        public virtual DbSet<Contest> Contests { get; set; } = null!;
        public virtual DbSet<ThemeConfiguration> ThemeConfigurations { get; set; } = null!;
        public virtual DbSet<ContestTab> ContestTabs { get; set; } = null!;
        public virtual DbSet<CandidateRegistration> CandidateRegistration { get; set; } = null!;
        public virtual DbSet<Tenant> Tenants { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

    }
}
