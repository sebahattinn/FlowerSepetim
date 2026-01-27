using FluentValidation;
using CicekSepeti.Application.DTOs; 

namespace CicekSepeti.Application.Validators
{
    public class CreateFlowerValidator : AbstractValidator<CreateFlowerDto>
    {
        public CreateFlowerValidator()
        {
           
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Çiçek adı boş bırakılamaz.")
                .Length(3, 100).WithMessage("Çiçek adı en az 3, en fazla 100 karakter olmalıdır.");

           
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır.")
                .PrecisionScale(18, 2, false).WithMessage("Fiyat formatı hatalı (Örn: 150.90).");


            RuleFor(x => x.StockQuantity)
             .GreaterThanOrEqualTo(0).WithMessage("Stok miktarı negatif olamaz.");
        


        RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Lütfen geçerli bir kategori seçiniz.");

            
            RuleFor(x => x.ImageUrl)
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(x => !string.IsNullOrEmpty(x.ImageUrl)) 
                .WithMessage("Lütfen geçerli bir URL formatı giriniz (http://...).");

           
            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Açıklama çok uzun (Max 1000 karakter).");
        }
    }
}