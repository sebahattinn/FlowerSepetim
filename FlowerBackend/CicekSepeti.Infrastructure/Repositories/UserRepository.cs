using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using CicekSepeti.Domain.Entities;
using CicekSepeti.Domain.Interfaces;

namespace CicekSepeti.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        // 1️⃣ USER EKLE
        public async Task<int> AddAsync(User user)
        {
            var sql = @"INSERT INTO Users 
                        (RoleId, FirstName, LastName, Email, PasswordHash, PasswordSalt, IsActive, CreatedAt) 
                        VALUES 
                        (@RoleId, @FirstName, @LastName, @Email, @PasswordHash, @PasswordSalt, @IsActive, @CreatedAt);
                        SELECT SCOPE_IDENTITY();";

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@RoleId", user.RoleId);
            command.Parameters.AddWithValue("@FirstName", user.FirstName);
            command.Parameters.AddWithValue("@LastName", user.LastName);
            command.Parameters.AddWithValue("@Email", user.Email);

            // 👇 DÜZELTME 1: Byte[] -> String (Base64) Çevrimi (DB nvarchar olduğu için)
            command.Parameters.AddWithValue("@PasswordHash", Convert.ToBase64String(user.PasswordHash));
            command.Parameters.AddWithValue("@PasswordSalt", Convert.ToBase64String(user.PasswordSalt));

            command.Parameters.AddWithValue("@IsActive", user.IsActive);
            command.Parameters.AddWithValue("@CreatedAt", user.CreatedAt);

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        // 2️⃣ EMAIL İLE USER GETİR (Load Metodu ile)
        public async Task<User?> GetByEmailAsync(string email)
        {
            var sql = "SELECT * FROM Users WHERE Email = @Email";

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Email", email);

            using var reader = await command.ExecuteReaderAsync();
            if (!await reader.ReadAsync()) return null;

            // 👇 DÜZELTME 2: String (Base64) -> Byte[] Çevrimi
            // Veritabanından gelen string'i tekrar byte dizisine çeviriyoruz.
            byte[] hashBytes = Convert.FromBase64String(reader["PasswordHash"].ToString()!);
            byte[] saltBytes = Convert.FromBase64String(reader["PasswordSalt"].ToString()!);

            return User.Load(
                id: Convert.ToInt32(reader["Id"]),
                roleId: Convert.ToInt32(reader["RoleId"]),
                firstName: reader["FirstName"].ToString()!,
                lastName: reader["LastName"].ToString()!,
                email: reader["Email"].ToString()!,
                passwordHash: hashBytes, // Artık cast hatası vermez
                passwordSalt: saltBytes, // Artık cast hatası vermez
                isActive: Convert.ToBoolean(reader["IsActive"]),
                createdAt: Convert.ToDateTime(reader["CreatedAt"]),
                refreshToken: reader["RefreshToken"] != DBNull.Value ? reader["RefreshToken"].ToString() : null,
                refreshTokenExpiryTime: reader["RefreshTokenExpiryTime"] != DBNull.Value ? Convert.ToDateTime(reader["RefreshTokenExpiryTime"]) : null
            );
        }

        // 3️⃣ REFRESH TOKEN İLE USER GETİR (Load Metodu ile)
        public async Task<User?> GetByRefreshTokenAsync(string refreshToken)
        {
            var sql = "SELECT * FROM Users WHERE RefreshToken = @RefreshToken";

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@RefreshToken", refreshToken);

            using var reader = await command.ExecuteReaderAsync();
            if (!await reader.ReadAsync()) return null;

            // 👇 DÜZELTME 3: Burada da çevrim yapıyoruz
            byte[] hashBytes = Convert.FromBase64String(reader["PasswordHash"].ToString()!);
            byte[] saltBytes = Convert.FromBase64String(reader["PasswordSalt"].ToString()!);

            return User.Load(
                id: Convert.ToInt32(reader["Id"]),
                roleId: Convert.ToInt32(reader["RoleId"]),
                firstName: reader["FirstName"].ToString()!,
                lastName: reader["LastName"].ToString()!,
                email: reader["Email"].ToString()!,
                passwordHash: hashBytes,
                passwordSalt: saltBytes,
                isActive: Convert.ToBoolean(reader["IsActive"]),
                createdAt: Convert.ToDateTime(reader["CreatedAt"]),
                refreshToken: reader["RefreshToken"] != DBNull.Value ? reader["RefreshToken"].ToString() : null,
                refreshTokenExpiryTime: reader["RefreshTokenExpiryTime"] != DBNull.Value ? Convert.ToDateTime(reader["RefreshTokenExpiryTime"]) : null
            );
        }

        // 4️⃣ REFRESH TOKEN GÜNCELLE
        public async Task UpdateRefreshTokenAsync(int userId, string refreshToken, DateTime expiryTime)
        {
            var sql = @"UPDATE Users 
                        SET RefreshToken = @RefreshToken,
                            RefreshTokenExpiryTime = @ExpiryTime
                        WHERE Id = @UserId";

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@RefreshToken", refreshToken);
            command.Parameters.AddWithValue("@ExpiryTime", expiryTime);
            command.Parameters.AddWithValue("@UserId", userId);

            await command.ExecuteNonQueryAsync();
        }
    }
}