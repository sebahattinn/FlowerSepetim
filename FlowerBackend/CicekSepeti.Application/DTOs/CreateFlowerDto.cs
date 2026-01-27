namespace CicekSepeti.Application.DTOs
{
   
    public record CreateFlowerDto(
        string Name,
        string? Description,
        string? Color,
        string? ImageUrl,
        decimal Price,
        int StockQuantity,
        int CategoryId,
        bool IsFeatured 
    );
}