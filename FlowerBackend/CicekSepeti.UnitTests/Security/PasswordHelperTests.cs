using CicekSepeti.Infrastructure.Security;
using FluentAssertions;
using Xunit;

namespace CicekSepeti.UnitTests.Security
{
    public class PasswordHelperTests
    {
       
        [Fact]
        public void CreatePasswordHash_ShouldGenerateValidHashAndSalt()
        {
            string password = "güçlü_bir_şifre_123!";

            PasswordHelper.CreatePasswordHash(password, out byte[] hash, out byte[] salt);

            // Hash ve Salt oluşmuş mu?
            hash.Should().NotBeNull();
            salt.Should().NotBeNull();

            // HMACSHA512 standartlarına uygun mu?
            hash.Length.Should().Be(64);  // 512 bit = 64 byte
            salt.Length.Should().Be(128); // Genelde 128 byte salt kullanılır (Implementation'a göre değişebilir, kontrol et)
        }

        [Fact]
        public void VerifyPasswordHash_ShouldReturnTrue_WhenPasswordIsCorrect()
        {
            string password = "gizli_sifre";
            PasswordHelper.CreatePasswordHash(password, out byte[] hash, out byte[] salt);

            var result = PasswordHelper.VerifyPasswordHash(password, hash, salt);
            result.Should().BeTrue();
        }

      

        [Fact]
        public void VerifyPasswordHash_ShouldReturnFalse_WhenPasswordIsWrong()
        {
            string password = "dogru_sifre";
            PasswordHelper.CreatePasswordHash(password, out byte[] hash, out byte[] salt);

            var result = PasswordHelper.VerifyPasswordHash("yanlis_sifre", hash, salt);
            result.Should().BeFalse();
        }

        [Fact]
        public void VerifyPasswordHash_ShouldReturnFalse_WhenSaltIsTampered()
        {
            // Şifre doğru, Hash doğru ama SALT değiştirilmiş (Man in the middle)
            string password = "test";
            PasswordHelper.CreatePasswordHash(password, out byte[] hash, out byte[] salt);

           
            salt[0] = (byte)(salt[0] + 1);

            var result = PasswordHelper.VerifyPasswordHash(password, hash, salt);
            result.Should().BeFalse();
        }

        [Fact]
        public void VerifyPasswordHash_ShouldReturnFalse_WhenHashIsTampered()
        {
            // Şifre doğru, Salt doğru ama HASH veritabanında bozulmuş
            string password = "test";
            PasswordHelper.CreatePasswordHash(password, out byte[] hash, out byte[] salt);

            // Hash'i bozuyoruz
            hash[0] = (byte)(hash[0] + 1);

            var result = PasswordHelper.VerifyPasswordHash(password, hash, salt);
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void VerifyPasswordHash_ShouldHandleEmptyOrNullInputs(string invalidPassword)
        {
           

            byte[] dummyHash = new byte[64];
            byte[] dummySalt = new byte[128];

           
            try
            {
                var result = PasswordHelper.VerifyPasswordHash(invalidPassword, dummyHash, dummySalt);
                result.Should().BeFalse(); // Eğer exception atmazsa False dönmeli
            }
            catch (Exception ex)
            {
                ex.Should().BeAssignableTo<ArgumentException>();
            }
        }

        [Fact]
        public void VerifyPasswordHash_ShouldThrowException_WhenArraysAreEmpty()
        {
            // Boş byte array gönderilirse
            Action act = () => PasswordHelper.VerifyPasswordHash("pass", new byte[0], new byte[0]);

            act.Should().Throw<ArgumentException>(); // "Hash length must be 64" gibi bir hata bekleriz
        }
    }
}