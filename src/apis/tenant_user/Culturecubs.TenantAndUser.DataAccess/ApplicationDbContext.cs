using Culturecubs.TenantAndUser.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturecubs.TenantAndUser.DataAccess
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }   
        public virtual DbSet<Tenant> Tenants { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
    }
}
