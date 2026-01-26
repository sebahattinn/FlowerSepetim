using FluentValidation;
using CicekSepeti.Application.DTOs;

namespace CicekSepeti.Application.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserValidator()
        {
            // 1. İsim Kontrolü
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad alanı boş bırakılamaz.")
                .Length(3, 50).WithMessage("Ad en az 3, en fazla 50 karakter olabilir.")
                .Must(IsValidName).WithMessage("Adınızda geçersiz karakterler var."); // Özel metod çağırıyoruz

            // 2. Soyisim Kontrolü
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad alanı boş bırakılamaz.")
                .Length(2, 50).WithMessage("Soyad en az 2 karakter olmalıdır.");

            // 3. Email Kontrolü
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email adresi gereklidir.")
                .EmailAddress().WithMessage("Lütfen geçerli bir email adresi giriniz.");

            // 4. ŞİFRE KONTROLÜ (Senior Güvenlik) 
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre gereklidir.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.")
                .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
                .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
                .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir.");
            // İstersen özel karakter de zorunlu kılabilirsin: .Matches("[^a-zA-Z0-9]")
        }

        // Helper Metot: Sadece harf olsun (Opsiyonel)
        private bool IsValidName(string name)
        {
            // Basitçe: Sayı içermesin diyebiliriz veya regex ile daha katı olabiliriz.
            return name.All(char.IsLetter);
        }
    }
}