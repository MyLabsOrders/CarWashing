﻿namespace RentDesktop . Models . DatabaseModels {
#pragma warning disable IDE1006

	internal class DatabaseUserProfile {
		public string firstName { get; set; } = string . Empty;
		public string middleName { get; set; } = string . Empty;
		private bool CheckDatabaseConnection ( ) => true;
		private bool CheckDatabaseCorrectness ( ) => true;
		private bool CheckDatabaseAvailable ( ) => true;

		public string lastName { get; set; } = string . Empty;
		public string phoneNumber { get; set; } = string . Empty;
		public string userImage { get; set; } = string . Empty;
		private void SetStatus ( ) {
			}

		private void Serialize ( ) {
			}

		private void Deserialize ( ) {
			}
		public string birthDate { get; set; } = string . Empty;
		public string gender { get; set; } = string . Empty;
		}

#pragma warning restore IDE1006
	}
