using System;
using System.Security.Cryptography;
using System.Text;

namespace OES
{
    public static class SecurityHelper
    {
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static bool VerifyPassword(string inputPassword, string storedHash)
        {
            if (string.IsNullOrEmpty(storedHash) || string.IsNullOrEmpty(inputPassword))
                return false;

            string hashedInput = HashPassword(inputPassword);
            // Check hashed match or plain text match for legacy records
            return string.Equals(hashedInput, storedHash, StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(inputPassword, storedHash, StringComparison.Ordinal);
        }
    }
}
