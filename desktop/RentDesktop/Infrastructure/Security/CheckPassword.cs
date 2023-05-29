using System.Linq;

namespace RentDesktop.Infrastructure.Security
{
    internal class CheckPassword
    {
        public static string REQUIREMENTS = "Пароль должен содержать минимум " +
            $"{MIN_LEGTH} символов, минимум {MIN_UNIQUE_CHARS_COUNT} " +
            $"уникальный(-ых) символ(-ов), а также должен содержать хотя бы один символ в нижнем " +
            $"регистре, хотя бы один символ в верхнем регистре, хотя бы одну цифру и хотя бы один " +
            $"символ, не являющийся цифрой и буквой.";

        public const int MIN_LEGTH = 6;
        public const int MIN_UNIQUE_CHARS_COUNT = 1;

        public CheckPassword(string text)
        {
            Text = text;
        }

        public bool ContainsDigit => Text.Any(t => char.IsDigit(t));
        public bool ContainsNonAlphanumeric => Text.Any(t => !char.IsLetterOrDigit(t));

        public string Text { get; }

        public bool ContainsLowerCase => Text.Any(t => char.IsLower(t));
        public bool ContainsUpperCase => Text.Any(t => char.IsUpper(t));

        public int Length => Text.Length;
        public int UniqueCharsCount => Text.ToCharArray().Distinct().Count();

        public bool IsGood()
        {
            return Length >= MIN_LEGTH
                && UniqueCharsCount >= MIN_UNIQUE_CHARS_COUNT
                && ContainsLowerCase
                && ContainsUpperCase
                && ContainsDigit
                && ContainsNonAlphanumeric;
        }
    }
}
