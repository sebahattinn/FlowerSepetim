using System.Threading.Tasks;


namespace CicekSepeti.Application.Interfaces
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, int expirationInMinutes = 60);
        Task RemoveAsync(string key);
    }
}