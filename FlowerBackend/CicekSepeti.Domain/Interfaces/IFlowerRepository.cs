using CicekSepeti.Domain.Entities;

namespace CicekSepeti.Domain.Interfaces
{
    public interface IFlowerRepository
    {
        Task<IEnumerable<Flower>> GetAllAsync();

        // 👇 Tek ve net tanım (Nullable dönebilir ?)
        Task<Flower?> GetByIdAsync(int id);

        Task<int> AddAsync(Flower flower);

        Task UpdateAsync(Flower flower);
        Task DeleteAsync(int id);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<int> AddCategoryAsync(string categoryName);
        Task<IEnumerable<Flower>> GetFeaturedFlowersAsync();
        Task<bool> GetMaintenanceStateAsync();
        Task UpdateMaintenanceStateAsync(bool isMaintenance);
    }
}