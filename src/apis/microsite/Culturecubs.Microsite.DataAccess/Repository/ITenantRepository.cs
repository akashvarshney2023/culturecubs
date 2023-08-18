using Culturecubs.Microsite.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturecubs.Microsite.DataAccess.Repository
{
    public interface ITenantRepository
    {
        ThemeConfiguration ValidateTenant(string code);
    }
}
