

using ContentManagement.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics.Metrics;

namespace ContentManagement.DataContext
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<Model.ContentManagement> Content { get; set; } = default!;

        public virtual DbSet<Microsite> Microsite { get; set; } = default!;

        public virtual DbSet<Page> Page { get; set; } = default!;


    }
}

