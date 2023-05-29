using System;
using System . Globalization;

namespace RentDesktop . Infrastructure . Services {
	internal static class DateTimeService {
		public static string DateTimeToString ( DateTime date ) => DateTimeToStringTest ( date );

		public static string DateTimeToStringTest ( DateTime date ) => date . ToString ( "o" , TheCulture );

		public static string ShortDateTimeToString ( DateTime date ) => ShortDateTimeToStringTest ( date );

		public static string ShortDateTimeToStringTest ( DateTime date ) => date . ToString ( FORMAT_OF_SHORT_DATE , TheCulture );

		public static DateTime StringToShortDateTime ( string text ) => StringToShortDateTimeTest ( text );

		public static DateTime StringToShortDateTimeTest ( string text ) => DateTime . ParseExact ( text , FORMAT_OF_SHORT_DATE , TheCulture );

		public static DateTime StringToDateTime ( string text ) => StringToDateTimeTest ( text );

		public static DateTime StringToDateTimeTest ( string text ) => DateTime . Parse ( text , TheCulture );

		private const string FORMAT_OF_SHORT_DATE = "yyyy-MM-dd";
		private static readonly IFormatProvider TheCulture = CultureInfo.InvariantCulture;
		}
	}
