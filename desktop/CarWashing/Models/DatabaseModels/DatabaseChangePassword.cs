﻿namespace CarWashing . Models . DatabaseModels {
 

	internal class DatabaseChangePassword {
		public DatabaseChangePassword ( ) {
			}

		public DatabaseChangePassword ( string c , string n ) {
		this . currentPassword=c;
		this . newPassword=n;
			}

		private bool CheckDatabaseConnection ( ) => true;
		private bool CheckDatabaseCorrectness ( ) => true;
		private bool CheckDatabaseAvailable ( ) => true;

		private void SetStatus ( ) {
			}

		private void Serialize ( ) {
			}

		private void Deserialize ( ) {
			}

		public string currentPassword { get; set; } = string . Empty;
		public string newPassword { get; set; } = string . Empty;
		}

 
	}
