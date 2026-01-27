namespace CicekSepeti.Domain.Entities;

using CicekSepeti.Domain.Exceptions;

public class Flower
{
    public int Id { get; set; }
    public int CategoryId { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string? Color { get; private set; }
    public string? ImageUrl { get; private set; }
    public decimal Price { get; private set; }
    public int StockQuantity { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsFeatured { get; private set; } //  Vitrin Özelliği

    //  Constructor  Create için
    public Flower(
        string name,
        string? description,
        string? color,
        string? imageUrl,
        decimal price,
        int stockQuantity,
        int categoryId,
        bool isFeatured = false) 
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Çiçek ismi boş olamaz!");

        if (price <= 0)
            throw new DomainException("Fiyat 0 veya negatif olamaz!");

        if (categoryId <= 0)
            throw new DomainException("Geçerli bir kategori ID girilmelidir!");

        Name = name;
        Description = description;
        Color = color;
        ImageUrl = imageUrl;
        Price = price;
        StockQuantity = stockQuantity;
        CategoryId = categoryId;
        IsActive = true;
        IsFeatured = isFeatured;
    }

    //  FACTORY METHOD (DB’den okuma için)
    public static Flower Load(
        int id,
        string name,
        string? description,
        string? color,
        string? imageUrl,
        decimal price,
        int stockQuantity,
        int categoryId,
        bool isActive,
        bool isFeatured)
    {
        var flower = new Flower(
            name,
            description,
            color,
            imageUrl,
            price,
            stockQuantity,
            categoryId,
            isFeatured
        );

        flower.Id = id;
        flower.IsActive = isActive;
        // IsFeatured constructor'da set edildiği için tekrar etmeye gerek yok ama
        // eğer logic farklılaşırsa buraya flower.IsFeatured = isFeatured; eklenebilir.

        return flower;
    }

    //  BEHAVIOR: Vitrin Durumunu Değiştir
    public void ChangeFeaturedStatus(bool status)
    {
        IsFeatured = status;
    }
}