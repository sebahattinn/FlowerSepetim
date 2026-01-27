using CicekSepeti.Application.DTOs;
using CicekSepeti.Application.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace CicekSepeti.UnitTests.Validators
{
    public class RegisterUserValidatorTests
    {
        private readonly RegisterUserValidator _validator;

        public RegisterUserValidatorTests()
        {
            _validator = new RegisterUserValidator();
        }

        // === GEÇERLİ DURUMLAR ===
        [Theory]
        [InlineData("normal@mail.com")]
        [InlineData("firstname.lastname@example.com")]
        public void Should_Not_Have_Error_When_Email_Is_Valid(string email)
        {
            var model = new RegisterUserDto("Ad", "Soyad", email, "123456");
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        // === GEÇERSİZ EMAİL & SQL INJECTION TESTLERİ ===
        [Theory]
        [InlineData("hatali_mail")]           // @ yok
        [InlineData("hatali@.com")]          // Domain yok
        [InlineData("@gmail.com")]           // Kullanıcı adı yok
        [InlineData("admin' OR '1'='1")]     // SQL Injection (Boşluk içerdiği ve format bozuk olduğu için yakalanmalı)
        [InlineData("<script>alert()")]      // XSS denemesi
        public void Should_Have_Error_When_Email_Is_Invalid(string email)
        {
            var model = new RegisterUserDto("Ad", "Soyad", email, "123456");
            var result = _validator.TestValidate(model);

            // Beklentimiz: Hata OLMALI
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        // === ŞİFRE TESTLERİ ===
        [Theory]
        [InlineData("123")]          // Çok kısa
        [InlineData("")]             // Boş
        [InlineData(null)]           // Null
        public void Should_Have_Error_When_Password_Is_Weak(string password)
        {
            var model = new RegisterUserDto("Ad", "Soyad", "a@b.com", password);
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        // === İSİM UZUNLUK TESTLERİ ===
        [Fact]
        public void Should_Have_Error_When_Name_Is_Too_Long()
        {
            // 51 karakterlik isim (Sınır değer testi - Validator 50 max diyor)
            string longName = new string('A', 51);

            var model = new RegisterUserDto(longName, "Soyad", "a@b.com", "123456");
            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.FirstName);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var model = new RegisterUserDto("", "Soyad", "a@b.com", "123456");
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.FirstName);
        }
    }
}