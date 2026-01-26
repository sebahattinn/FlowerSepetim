namespace CicekSepeti.Application.DTOs
{
    // Controller içindeki record'u buraya taşıdık.
    // Artık projenin her yerinden erişilebilir.
    public record CreateFlowerDto(
        string Name,
        string? Description,
        string? Color,
        string? ImageUrl,
        decimal Price,
        int Stock,
        int CategoryId,
        bool IsFeatured 
    );
}