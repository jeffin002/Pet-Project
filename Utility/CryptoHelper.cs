using System.Security.Cryptography;
using System.Text;

namespace Utility
{
    public class CryptoHelper
    {
        const int keySize = 64;
        const int iterations = 350000;
        static HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        /// <summary>
        /// Takes in a clear text password and returns the first base64string as PasswordHash and second as Password Salt
        /// </summary>
        /// <param name="clearTextPassword"></param>
        /// <returns></returns>
        public static (string, string) GeneratePasswordHashAndSalt(string clearTextPassword)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(clearTextPassword),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            return (Convert.ToBase64String(hash), Convert.ToBase64String(salt));

        }
        public static bool VerifyPassword(string password, string hash, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);
            var generatedHash = Rfc2898DeriveBytes.Pbkdf2(password, saltBytes, iterations, hashAlgorithm, keySize);
            return CryptographicOperations.FixedTimeEquals(generatedHash, Convert.FromBase64String(hash));
        }
    }
}