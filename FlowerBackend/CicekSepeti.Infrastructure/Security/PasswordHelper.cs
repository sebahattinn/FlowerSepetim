using System.Security.Cryptography;
using System.Text;

namespace CicekSepeti.Infrastructure.Security
{
    public static class PasswordHelper
    {
        // 1. Şifreyi Hash'lemek için (String yerine byte[] döner)
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                // Salt: Artık direkt byte[] olarak alıyoruz
                passwordSalt = hmac.Key;

                // Hash: Artık direkt byte[] olarak alıyoruz
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        // 2. Şifreyi Doğrulamak için (String yerine byte[] kabul eder)
        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Byte byte karşılaştırma (En güvenli yöntem)
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }
    }
}