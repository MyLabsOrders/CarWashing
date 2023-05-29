using System;
using System.Globalization;

namespace RentDesktop.Infrastructure.Services
{
    internal static class DateTimeService
    {
        private const string FORMAT_OF_SHORT_DATE = "yyyy-MM-dd";
        private static readonly IFormatProvider TheCulture = CultureInfo.InvariantCulture;

        public static string DateTimeToString(DateTime date)
        {
            return date.ToString("o", TheCulture);
        }

        public static string ShortDateTimeToString(DateTime date)
        {
            return date.ToString(FORMAT_OF_SHORT_DATE, TheCulture);
        }

        public static DateTime StringToShortDateTime(string text)
        {
            return DateTime.ParseExact(text, FORMAT_OF_SHORT_DATE, TheCulture);
        }

        public static DateTime StringToDateTime(string text)
        {
            return DateTime.Parse(text, TheCulture);
        }
    }
}
