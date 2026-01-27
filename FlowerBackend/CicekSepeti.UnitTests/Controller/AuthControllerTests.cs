using CicekSepeti.API.Controllers;
using CicekSepeti.Application.DTOs;
using CicekSepeti.Domain.Entities;
using CicekSepeti.Domain.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CicekSepeti.UnitTests.Controller
{
    public class AuthControllerTests
    {
        private readonly Mock<IUserRepository> _mockRepo;
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mockRepo = new Mock<IUserRepository>();
            _mockConfig = new Mock<IConfiguration>();

            // --- MOCK CONFIGURATION (JWT) ---
            // DÜZELTME 1: SecretKey'i uzattık (HMACSHA512 için en az 64 karakter olmalı)
            var longSecretKey = "bu_cok_gizli_ve_uzun_bir_test_secret_key_degeridir_123456_en_az_64_karakter_olmali_ki_patlamasin_ve_guvenli_olsun";

            var mockSection = new Mock<IConfigurationSection>();
            mockSection.Setup(x => x.Value).Returns(longSecretKey);

            _mockConfig.Setup(c => c.GetSection("JwtSettings")).Returns(mockSection.Object);
            _mockConfig.Setup(c => c.GetSection("JwtSettings:SecretKey")).Returns(mockSection.Object);
            _mockConfig.Setup(c => c.GetSection("JwtSettings:Issuer")).Returns(mockSection.Object);
            _mockConfig.Setup(c => c.GetSection("JwtSettings:Audience")).Returns(mockSection.Object);

            // Indexer Setup'ları
            mockSection.Setup(x => x["SecretKey"]).Returns(longSecretKey); // Uzun key'i buraya da verdik
            mockSection.Setup(x => x["Issuer"]).Returns("www.ciceksepeti.com");
            mockSection.Setup(x => x["Audience"]).Returns("www.ciceksepeti.com");
            mockSection.Setup(x => x["DurationInMinutes"]).Returns("60");

            _controller = new AuthController(_mockRepo.Object, _mockConfig.Object);
        }

        // ==========================================
        // 1️⃣ REGISTER (KAYIT) TESTLERİ
        // ==========================================

        [Fact]
        public async Task Register_ShouldReturnOk_WhenUserIsNew()
        {
            var registerDto = new RegisterUserDto("Sebahattin", "Yazici", "sebo@test.com", "123456");
            _mockRepo.Setup(x => x.GetByEmailAsync(registerDto.Email)).ReturnsAsync((User?)null);
            _mockRepo.Setup(x => x.AddAsync(It.IsAny<User>())).ReturnsAsync(1);

            var result = await _controller.Register(registerDto);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenEmailAlreadyExists()
        {
            var registerDto = new RegisterUserDto("Sebahattin", "Yazici", "varolan@test.com", "123456");
            var existingUser = User.Create("Var", "Olan", "varolan@test.com", new byte[0], new byte[0]);

            _mockRepo.Setup(x => x.GetByEmailAsync(registerDto.Email)).ReturnsAsync(existingUser);

            var result = await _controller.Register(registerDto);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Register_ShouldThrowException_WhenDatabaseFails()
        {
            var registerDto = new RegisterUserDto("Sebahattin", "Yazici", "fail@test.com", "123456");

            _mockRepo.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync((User?)null);
            _mockRepo.Setup(x => x.AddAsync(It.IsAny<User>())).ThrowsAsync(new Exception("DB Connection Failed"));

            await Assert.ThrowsAsync<Exception>(() => _controller.Register(registerDto));
        }

        // ==========================================
        // 2️⃣ LOGIN (GİRİŞ) TESTLERİ
        // ==========================================

        [Fact]
        public async Task Login_ShouldReturnBadRequest_WhenUserDoesNotExist()
        {
            var loginRequest = new LoginRequest("yok@test.com", "123456");
            _mockRepo.Setup(x => x.GetByEmailAsync(loginRequest.Email)).ReturnsAsync((User?)null);

            var result = await _controller.Login(loginRequest);

            result.Should().BeOfType<BadRequestObjectResult>();
            ((BadRequestObjectResult)result).Value.Should().Be("Kullanıcı bulunamadı.");
        }

        [Fact]
        public async Task Login_ShouldReturnBadRequest_WhenPasswordIsWrong()
        {
            var loginRequest = new LoginRequest("sebo@test.com", "YANLIS_SIFRE");

            using var hmac = new HMACSHA512();
            var dbUser = CreateMockUser("sebo@test.com", hmac.ComputeHash(Encoding.UTF8.GetBytes("dogru123")), hmac.Key);

            _mockRepo.Setup(x => x.GetByEmailAsync(loginRequest.Email)).ReturnsAsync(dbUser);

            var result = await _controller.Login(loginRequest);

            result.Should().BeOfType<BadRequestObjectResult>();
            ((BadRequestObjectResult)result).Value.Should().Be("Şifre hatalı!");
        }

        [Fact]
        public async Task Login_ShouldReturnOkWithToken_WhenCredentialsAreCorrect()
        {
            // Bu test artık patlamayacak çünkü key uzunluğunu düzelttik
            var password = "password123";
            var loginRequest = new LoginRequest("dogru@test.com", password);

            using var hmac = new HMACSHA512();
            var dbUser = CreateMockUser(loginRequest.Email, hmac.ComputeHash(Encoding.UTF8.GetBytes(password)), hmac.Key);

            _mockRepo.Setup(x => x.GetByEmailAsync(loginRequest.Email)).ReturnsAsync(dbUser);
            _mockRepo.Setup(x => x.UpdateRefreshTokenAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(Task.CompletedTask);

            var result = await _controller.Login(loginRequest);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var json = System.Text.Json.JsonSerializer.Serialize(okResult.Value);
            json.Should().Contain("token");
        }

        // ==========================================
        // 3️⃣ REFRESH TOKEN TESTLERİ
        // ==========================================

        [Fact]
        public async Task RefreshToken_ShouldReturnBadRequest_WhenTokenIsInvalidOrUserNotFound()
        {
            var request = new RefreshTokenRequest("sahte_token_123");
            _mockRepo.Setup(x => x.GetByRefreshTokenAsync(request.RefreshToken)).ReturnsAsync((User?)null);

            var result = await _controller.RefreshToken(request);

            result.Should().BeOfType<BadRequestObjectResult>();

            // DÜZELTME 2: Assertion hatasını düzelttik. Direkt string kontrolü yapıyoruz.
            var badRequest = result as BadRequestObjectResult;
            badRequest!.Value.ToString().Should().Contain("Oturum süreniz dolmuş");
        }

        [Fact]
        public async Task RefreshToken_ShouldReturnBadRequest_WhenTokenIsExpired()
        {
            var request = new RefreshTokenRequest("eski_token");

            using var hmac = new HMACSHA512();
            var expiredUser = User.Load(1, 2, "Test", "User", "test@test.com", new byte[0], new byte[0], true, DateTime.Now, "eski_token", DateTime.Now.AddDays(-1));

            _mockRepo.Setup(x => x.GetByRefreshTokenAsync(request.RefreshToken)).ReturnsAsync(expiredUser);

            var result = await _controller.RefreshToken(request);

            result.Should().BeOfType<BadRequestObjectResult>();

            // DÜZELTME 2: Assertion hatasını düzelttik.
            var badRequest = result as BadRequestObjectResult;
            badRequest!.Value.ToString().Should().Contain("Oturum süreniz dolmuş");
        }

        [Fact]
        public async Task RefreshToken_ShouldReturnOk_WhenTokenIsValid()
        {
            // Bu test artık patlamayacak çünkü key uzunluğunu düzelttik
            var request = new RefreshTokenRequest("gecerli_token");

            using var hmac = new HMACSHA512();
            var validUser = User.Load(1, 2, "Test", "User", "test@test.com", new byte[0], new byte[0], true, DateTime.Now, "gecerli_token", DateTime.Now.AddDays(1));

            _mockRepo.Setup(x => x.GetByRefreshTokenAsync(request.RefreshToken)).ReturnsAsync(validUser);
            _mockRepo.Setup(x => x.UpdateRefreshTokenAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(Task.CompletedTask);

            var result = await _controller.RefreshToken(request);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var json = System.Text.Json.JsonSerializer.Serialize(okResult.Value);
            json.Should().Contain("token");
            json.Should().Contain("refreshToken");
        }

        // --- YARDIMCI METOT ---
        private User CreateMockUser(string email, byte[] hash, byte[] salt)
        {
            return User.Load(1, 2, "Test", "User", email, hash, salt, true, DateTime.Now, null, null);
        }
    }
}