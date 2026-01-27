using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CicekSepeti.Domain.Entities;
using CicekSepeti.Domain.Interfaces;
using CicekSepeti.Infrastructure.Security;
using CicekSepeti.Application.DTOs; 

//  KRİTİK NOKTA: İsim çakışmasını önlemek için User sınıfına takma ad veriyoruz
using UserEntity = CicekSepeti.Domain.Entities.User;

namespace CicekSepeti.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto request)
        {
            // 1. Email kontrolü
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return BadRequest(new { message = "Bu email adresi zaten kullanılıyor." });
            }

            PasswordHelper.CreatePasswordHash(
                request.Password,
                out byte[] passwordHash,
                out byte[] passwordSalt
            );

          
            var user = UserEntity.Create(
                request.FirstName,
                request.LastName,
                request.Email,
                passwordHash,
                passwordSalt
            );

            await _userRepository.AddAsync(user);

            return Ok(new
            {
                message = "Kayıt başarılı! Giriş yapabilirsiniz."
            });
        }

   
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                return BadRequest("Kullanıcı bulunamadı.");

            if (!PasswordHelper.VerifyPasswordHash(
                request.Password,
                user.PasswordHash,
                user.PasswordSalt))
            {
                return BadRequest("Şifre hatalı!");
            }

            string accessToken = CreateToken(user);
            string refreshToken = GenerateRefreshToken();

            await _userRepository.UpdateRefreshTokenAsync(
                user.Id,
                refreshToken,
                DateTime.Now.AddDays(7)
            );

            return Ok(new
            {
                token = accessToken,
                refreshToken = refreshToken,
                message = "Giriş başarılı!"
            });
        }

       
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var user = await _userRepository.GetByRefreshTokenAsync(request.RefreshToken);

            if (user == null || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Oturum süreniz dolmuş, lütfen tekrar giriş yapın.");
            }

            string newAccessToken = CreateToken(user);
            string newRefreshToken = GenerateRefreshToken();

            await _userRepository.UpdateRefreshTokenAsync(
                user.Id,
                newRefreshToken,
                DateTime.Now.AddDays(7)
            );

            return Ok(new
            {
                token = newAccessToken,
                refreshToken = newRefreshToken
            });
        }

   
        private string CreateToken(UserEntity user) 
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleId == 1 ? "Admin" : "Customer")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(
                    Convert.ToDouble(jwtSettings["DurationInMinutes"])
                ),
                SigningCredentials = creds,
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

   
    public record LoginRequest(string Email, string Password);
    public record RefreshTokenRequest(string RefreshToken);
}