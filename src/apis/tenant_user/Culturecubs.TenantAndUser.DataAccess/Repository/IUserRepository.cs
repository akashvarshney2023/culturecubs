using Culturecubs.TenantAndUser.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturecubs.TenantAndUser.DataAccess.Repository
{
    public interface IUserRepository
    {
        Task<User?> GetUsersByUserName(string userName);
    }
}
