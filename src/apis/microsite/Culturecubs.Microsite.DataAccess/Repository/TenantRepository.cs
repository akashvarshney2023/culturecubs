using Culturecubs.Microsite.DataAccess.DataContext;
using Culturecubs.Microsite.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturecubs.Microsite.DataAccess.Repository
{
    public class TenantRepository : ITenantRepository
    {
        private readonly ApplicationDbContext _ctx;
        public TenantRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public ThemeConfiguration ValidateTenant(string code)
        {
            throw new NotImplementedException();
        }
    }
}
