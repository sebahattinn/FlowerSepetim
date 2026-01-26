using CicekSepeti.Domain.Entities;

namespace CicekSepeti.Domain.Interfaces
{
    public interface IUserRepository
    {
        // Login için
        Task<User?> GetByEmailAsync(string email);

        // Kayıt için
        Task<int> AddAsync(User user);

        // 👇 EKSİK OLAN METODLAR EKLENDİ
        // Refresh Token kontrolü ve güncellemesi için şart
        Task<User?> GetByRefreshTokenAsync(string refreshToken);
        Task UpdateRefreshTokenAsync(int userId, string refreshToken, DateTime expiryDate);
    }
}