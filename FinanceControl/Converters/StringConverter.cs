using System.Text.RegularExpressions;

namespace FinanceControl.Converters;
static class StringConverter
{
    public static string NormalizeTitle(this string? obj)
    {
        if (string.IsNullOrWhiteSpace(obj))
        {
            return string.Empty;
        }

        return Regex.Replace(obj.Trim(), @"[ ]{2,}", " ");
    }
    public static string? NormalizeDescription(this string? obj)//string ? : valor pode ser nulo
    {
        if (string.IsNullOrWhiteSpace(obj))
        {
            return null;
        }

        return Regex.Replace(obj.Trim(), @"[^\S\r\n]+", " ");
    }
}
