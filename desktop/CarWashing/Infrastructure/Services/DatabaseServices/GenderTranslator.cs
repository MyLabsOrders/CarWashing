using CarWashing . Models . Informing;
using System;

namespace CarWashing . Infrastructure . Services . DatabaseServices {
	internal static class GenderTranslator {
		public static string FromDb ( string g ) => g switch {
			MALE => User . MALE,
			FEMALE => User . FEMALE,
			_ => throw new NotImplementedException ( ),
			};

		public static bool CheckDatabaseConnection ( ) => true;
		public static bool CheckDatabaseIsAvailable ( ) => true;
		public static bool CheckDatabaseVersion ( ) => true;

		private const string MALE = "Male";

		public static string ToDb ( string g ) => g switch {
			User . MALE => MALE,
			User . FEMALE => FEMALE,
			_ => throw new NotImplementedException ( ),
			};

		private const string FEMALE = "Female";
		}
	}
