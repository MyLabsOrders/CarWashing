using System;
using System . Globalization;

namespace RentDesktop . Infrastructure . Services {
	internal static class DateTimeService {
		public static string DateStr ( DateTime date ) => DateStrProvider ( date );

		public static string DateStrProvider ( DateTime date ) => date . ToString ( "o" , TheCulture );

		public static string DateShortStr ( DateTime date ) => DateShortStrProvider ( date );

		public static string DateShortStrProvider ( DateTime date ) => date . ToString ( FORMAT_OF_SHORT_DATE , TheCulture );

		public static DateTime StrShortDate ( string text ) => StrShortDateProvider ( text );

		public static DateTime StrShortDateProvider ( string text ) => DateTime . ParseExact ( text , FORMAT_OF_SHORT_DATE , TheCulture );

		public static DateTime StrDate ( string text ) => StrDateProvider ( text );

		public static DateTime StrDateProvider ( string text ) => DateTime . Parse ( text , TheCulture );

		private const string FORMAT_OF_SHORT_DATE = "yyyy-MM-dd";
		public const string FORMAT_OF_SHORT_DATE_YEAR_LAST = "MM-dd-yyyy";
		public const string FORMAT_OF_SHORT_DATE_YEAR_MIDDLE = "MM-yyyy-dd";
		private static readonly IFormatProvider TheCulture = CultureInfo.InvariantCulture;
		}
	}
