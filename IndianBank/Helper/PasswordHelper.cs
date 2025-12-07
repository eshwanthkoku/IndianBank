using System.Security.Cryptography;
using System.Text;

namespace IndianBank.Helper
{
    public static class PasswordHelper
    {
        public static string Hash(string planePassword) 
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(planePassword);
            var hash = sha256.ComputeHash(bytes);
            return System.Convert.ToBase64String(hash);
        }

        public static bool verify(string planePassword,string storedHash)
        {
            var hashedPassword = Hash(planePassword);
            if (hashedPassword == storedHash) 
            { 
                return true;
            }
            return false;
        }
    }
}
