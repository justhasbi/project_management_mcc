using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.Helper
{
    public class HashGenerator
    {
        public string password;
        public string correctHash;


        public HashGenerator(string password)
        {
            this.password = password;
        }

        public HashGenerator(string password, string correctHash)
        {
            this.password = password;
            this.correctHash = correctHash;
        }

        public static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }

        public static bool ValidatePassword(string password, string correctHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHash);
        }
    }
}
