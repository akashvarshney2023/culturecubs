using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureCubs.TenantAndUser.Functions.Configurations
{
    public static class Helper
    {
        private static readonly PasswordHasher _passwordHasher = new PasswordHasher();

        public static string ProcessUserData(string userPassword)
        {
            if (string.IsNullOrEmpty(userPassword))
                return "";

            return _passwordHasher.HashPassword(userPassword);
        }

        public static bool VerifyUserPassword(string userPassword, string passwordHash)
        {
            if (string.IsNullOrEmpty(userPassword))
                return false;

            var result = _passwordHasher.VerifyHashedPassword(passwordHash, userPassword);
            return result == Microsoft.AspNet.Identity.PasswordVerificationResult.Success;
        }
    }
}
