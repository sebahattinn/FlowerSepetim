using FluentValidation;
using CicekSepeti.Application.DTOs; // DTO'yu buradan alıyor

namespace CicekSepeti.Application.Validators
{
    public class CreateFlowerValidator : AbstractValidator<CreateFlowerDto>
    {
        public CreateFlowerValidator()
        {
            // 1. İsim Kontrolü
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Çiçek adı boş bırakılamaz.")
                .Length(3, 100).WithMessage("Çiçek adı en az 3, en fazla 100 karakter olmalıdır.");

            // 2. Fiyat Kontrolü
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır.")
                .PrecisionScale(18, 2, false).WithMessage("Fiyat formatı hatalı (Örn: 150.90).");

            // 3. Stok Kontrolü
            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stok miktarı negatif olamaz.");

            // 4. Kategori Kontrolü
            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Lütfen geçerli bir kategori seçiniz.");

            // 5. Görsel URL Kontrolü (Daha detaylı)
            RuleFor(x => x.ImageUrl)
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(x => !string.IsNullOrEmpty(x.ImageUrl)) // Doluysa kontrol et
                .WithMessage("Lütfen geçerli bir URL formatı giriniz (http://...).");

            // 6. Açıklama (Opsiyonel ama çok uzun olmasın)
            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Açıklama çok uzun (Max 1000 karakter).");
        }
    }
}