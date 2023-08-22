using Culturecubs.TenantAndUser.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturecubs.TenantAndUser.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _ctx;
        public UserRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<User?> GetUsersByUserName(string userName)
        {
            User? user = await _ctx.Users.SingleOrDefaultAsync(u => u.UserName == userName);
            return user;
        }

    }
}
