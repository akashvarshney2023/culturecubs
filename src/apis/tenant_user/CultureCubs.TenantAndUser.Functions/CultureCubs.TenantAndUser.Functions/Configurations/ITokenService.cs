using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureCubs.TenantAndUser.Functions.Configurations
{
    public interface ITokenService
    {
        string GenerateToken(string userName);
    }
}
