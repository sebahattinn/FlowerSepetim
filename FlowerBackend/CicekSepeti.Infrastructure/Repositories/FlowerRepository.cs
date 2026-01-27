using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using CicekSepeti.Domain.Entities;
using CicekSepeti.Domain.Interfaces;
using CicekSepeti.Infrastructure.Helpers;
using Dapper;

namespace CicekSepeti.Infrastructure.Repositories
{
    public class FlowerRepository : IFlowerRepository
    {
        private readonly string _connectionString;

        public FlowerRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        // 1️. ADD FLOWER
        public async Task<int> AddAsync(Flower flower)
        {
            var sql = @"INSERT INTO Flowers 
                        (CategoryId, Name, Description, Color, ImageUrl, Price, StockQuantity, IsActive, IsFeatured) 
                        VALUES 
                        (@CategoryId, @Name, @Description, @Color, @ImageUrl, @Price, @Stock, @IsActive, @IsFeatured);
                        SELECT SCOPE_IDENTITY();";

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@CategoryId", flower.CategoryId);
            command.Parameters.AddWithValue("@Name", flower.Name);
            command.Parameters.AddWithValue("@Description", (object?)flower.Description ?? DBNull.Value);
            command.Parameters.AddWithValue("@Color", (object?)flower.Color ?? DBNull.Value);
            command.Parameters.AddWithValue("@ImageUrl", (object?)flower.ImageUrl ?? DBNull.Value);
            command.Parameters.AddWithValue("@Price", flower.Price);
            command.Parameters.AddWithValue("@Stock", flower.StockQuantity);
            command.Parameters.AddWithValue("@IsActive", flower.IsActive);
            command.Parameters.AddWithValue("@IsFeatured", flower.IsFeatured);

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        // 2️. GET ALL FLOWERS
        public async Task<IEnumerable<Flower>> GetAllAsync()
        {
            var sql = "SELECT * FROM Flowers";
            var flowers = new List<Flower>();

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(sql, connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                flowers.Add(Flower.Load(
                    id: Convert.ToInt32(reader["Id"]),
                    name: reader["Name"].ToString()!,
                    description: reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                    color: reader["Color"] != DBNull.Value ? reader["Color"].ToString() : null,
                    imageUrl: reader["ImageUrl"] != DBNull.Value ? reader["ImageUrl"].ToString() : null,
                    price: Convert.ToDecimal(reader["Price"]),
                    stockQuantity: Convert.ToInt32(reader["StockQuantity"]),
                    categoryId: Convert.ToInt32(reader["CategoryId"]),
                    isActive: Convert.ToBoolean(reader["IsActive"]),
                    isFeatured: Convert.ToBoolean(reader["IsFeatured"])
                ));
            }

            return flowers;
        }

        // 3️. GET FLOWER BY ID
        public async Task<Flower?> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Flowers WHERE Id = @Id";

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return Flower.Load(
                    id: Convert.ToInt32(reader["Id"]),
                    name: reader["Name"].ToString()!,
                    description: reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                    color: reader["Color"] != DBNull.Value ? reader["Color"].ToString() : null,
                    imageUrl: reader["ImageUrl"] != DBNull.Value ? reader["ImageUrl"].ToString() : null,
                    price: Convert.ToDecimal(reader["Price"]),
                    stockQuantity: Convert.ToInt32(reader["StockQuantity"]),
                    categoryId: Convert.ToInt32(reader["CategoryId"]),
                    isActive: Convert.ToBoolean(reader["IsActive"]),
                    isFeatured: Convert.ToBoolean(reader["IsFeatured"])
                );
            }

            return null;
        }

        // 4️. UPDATE FLOWER
        public async Task UpdateAsync(Flower flower)
        {
            var sql = @"UPDATE Flowers 
                        SET Name = @Name, 
                            Description = @Description, 
                            Price = @Price, 
                            StockQuantity = @StockQuantity, 
                            ImageUrl = @ImageUrl, 
                            Color = @Color,
                            IsActive = @IsActive,
                            IsFeatured = @IsFeatured
                        WHERE Id = @Id";

            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(sql, new
            {
                flower.Id,
                flower.Name,
                flower.Description,
                flower.Price,
                flower.StockQuantity,
                flower.ImageUrl,
                flower.Color,
                flower.IsActive,
                flower.IsFeatured
            });
        }

        // 5️. DELETE (SOFT DELETE)
        public async Task DeleteAsync(int id)
        {
            var sql = "UPDATE Flowers SET IsActive = 0 WHERE Id = @Id";
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(sql, new { Id = id });
        }

        // 6️. GET FEATURED FLOWERS
        public async Task<IEnumerable<Flower>> GetFeaturedFlowersAsync()
        {
            var sql = "SELECT * FROM Flowers WHERE IsFeatured = 1 AND IsActive = 1";
            return await GetAllBySqlAsync(sql);
        }

        // 7️. GET CATEGORIES
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var sql = "SELECT * FROM Categories";
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Category>(sql);
        }

        // 8️. ADD CATEGORY
        public async Task<int> AddCategoryAsync(string categoryName)
        {
            var slug = categoryName.ToSlug();

            var sql = @"INSERT INTO Categories (Name, Slug, IsActive) 
                        VALUES (@Name, @Slug, 1);
                        SELECT SCOPE_IDENTITY();";

            using var connection = new SqlConnection(_connectionString);
            var result = await connection.ExecuteScalarAsync(sql, new
            {
                Name = categoryName,
                Slug = slug
            });

            return Convert.ToInt32(result);
        }


      
        public async Task<bool> GetMaintenanceStateAsync()
        {
            var sql = "SELECT TOP 1 IsMaintenanceMode FROM SiteSettings";

            using var connection = new SqlConnection(_connectionString);
            var result = await connection.ExecuteScalarAsync<bool?>(sql);

            return result ?? false;
        }

    
        public async Task UpdateMaintenanceStateAsync(bool isMaintenance)
        {
            var sql = @"
                IF EXISTS (SELECT 1 FROM SiteSettings)
                    UPDATE SiteSettings SET IsMaintenanceMode = @State
                ELSE
                    INSERT INTO SiteSettings (IsMaintenanceMode) VALUES (@State)";

            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(sql, new { State = isMaintenance });
        }

       
        private async Task<IEnumerable<Flower>> GetAllBySqlAsync(string sql)
        {
            var flowers = new List<Flower>();

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new SqlCommand(sql, connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                flowers.Add(Flower.Load(
                    id: Convert.ToInt32(reader["Id"]),
                    name: reader["Name"].ToString()!,
                    description: reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                    color: reader["Color"] != DBNull.Value ? reader["Color"].ToString() : null,
                    imageUrl: reader["ImageUrl"] != DBNull.Value ? reader["ImageUrl"].ToString() : null,
                    price: Convert.ToDecimal(reader["Price"]),
                    stockQuantity: Convert.ToInt32(reader["StockQuantity"]),
                    categoryId: Convert.ToInt32(reader["CategoryId"]),
                    isActive: Convert.ToBoolean(reader["IsActive"]),
                    isFeatured: Convert.ToBoolean(reader["IsFeatured"])
                ));
            }

            return flowers;
        }
    }
}