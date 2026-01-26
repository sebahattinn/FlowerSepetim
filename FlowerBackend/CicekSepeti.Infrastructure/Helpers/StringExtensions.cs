using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace CicekSepeti.Infrastructure.Helpers
{
    public static class StringExtensions
    {
        public static string ToSlug(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC)
                .ToLower(new CultureInfo("en-US")) // Türkçe karakterleri düzelt
                .Replace("ı", "i")
                .Replace("ğ", "g")
                .Replace("ü", "u")
                .Replace("ş", "s")
                .Replace("ö", "o")
                .Replace("ç", "c")
                .Trim() // Baştaki sondaki boşlukları sil
                .Replace(" ", "-") // Boşlukları tire yap
                .Replace("--", "-"); // Çift tireleri tek yap
        }
    }
}