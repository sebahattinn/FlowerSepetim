using CicekSepeti.Domain.Entities;
using CicekSepeti.Domain.Exceptions; 
using FluentAssertions;
using Xunit;

namespace CicekSepeti.UnitTests.Domain
{
    public class FlowerTests
    {
       
        [Fact]
        public void Create_ShouldWork_WhenDataIsValid()
        {
            var flower = new Flower("Gül", "Desc", "Red", "url", 100, 10, 1, false);

            flower.Should().NotBeNull();
            flower.Price.Should().Be(100);
            flower.IsActive.Should().BeTrue(); // Default değer kontrolü
            flower.IsFeatured.Should().BeFalse();
        }



        [Theory] // Birden fazla durumu tek testte denemek için
        [InlineData(0)]   // Fiyat 0 olamaz
        [InlineData(-1)]  // Fiyat negatif olamaz
        [InlineData(-1000)] // Aşırı negatif
        public void Create_ShouldThrowException_WhenPriceIsInvalid(decimal invalidPrice)
        {
            // Action ile constructor çağrısını sarmalıyoruz
            Action act = () => new Flower("Gül", "Desc", "Red", "url", invalidPrice, 10, 1, false);

            act.Should().Throw<DomainException>()
               .WithMessage("*Fiyat*"); // Hata mesajında "Fiyat" kelimesi geçmeli
        }

        [Theory]
        [InlineData("")]       
        //[InlineData(null)]      // Null
        [InlineData("   ")]     
        public void Create_ShouldThrowException_WhenNameIsInvalid(string invalidName)
        {
            Action act = () => new Flower(invalidName, "Desc", "Red", "url", 100, 10, 1, false);

            act.Should().Throw<DomainException>()
               .WithMessage("*isim*"); // "Çiçek ismi boş olamaz" hatasını yakalar
        }

        [Theory]
        [InlineData(0)]   // ID 0 olamaz
        [InlineData(-1)]  // ID negatif olamaz
        public void Create_ShouldThrowException_WhenCategoryIdIsInvalid(int invalidCategoryId)
        {
            Action act = () => new Flower("Gül", "Desc", "Red", "url", 100, 10, invalidCategoryId, false);

            act.Should().Throw<DomainException>()
               .WithMessage("*kategori*"); // "Geçerli bir kategori ID girilmelidir" hatası
        }

        [Fact]
        public void ChangeFeaturedStatus_ShouldToggleStatus()
        {
            // Vitrin durumunu değiştirme davranışını test ediyoruz
            var flower = new Flower("Gül", "Desc", "Red", "url", 100, 10, 1, false);

            // False -> True
            flower.ChangeFeaturedStatus(true);
            flower.IsFeatured.Should().BeTrue();

            // True -> False
            flower.ChangeFeaturedStatus(false);
            flower.IsFeatured.Should().BeFalse();
        }
    }
}