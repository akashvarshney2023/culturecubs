using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureCubs.TenantAndUser.Functions.Configurations
{
    public static class Helper
    {
        public static string ProcessUserData(string userPssword)
        {

            PasswordHasher hash = new PasswordHasher();
            if (string.IsNullOrEmpty(userPssword))
                return "";
            return hash.HashPassword(userPssword);

        }

        public static bool VerifyUserPassword(string userPssword, string passwordHash)
        {

            PasswordHasher hash = new PasswordHasher();
            if (string.IsNullOrEmpty(userPssword))
                return false;
            return hash.VerifyPassword(userPssword, passwordHash);

        }
    }
}
